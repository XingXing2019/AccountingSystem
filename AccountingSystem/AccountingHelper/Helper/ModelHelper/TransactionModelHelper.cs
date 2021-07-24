using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using AccountingDatabase.Entity;
using AccountingDatabase.Services;
using AccountingDatabase.Services.Interface;
using AccountingHelper.Model;
using NLog;

namespace AccountingHelper.Helper.ModelHelper
{
	public class TransactionModelHelper : IModelHelper<TransactionModel, Transaction>
	{
		private readonly IVendorService _vendorService = new VendorService();
		private readonly ITransactionService _transactionService = new TransactionService();
		private readonly Logger _logger = LogManager.GetCurrentClassLogger();


		public List<Transaction> TransformValidModels(List<TransactionModel> source)
		{
			var transactions = new List<Transaction>();
			foreach (var model in source)
			{
				if (IsDuplicateTransaction(model))
				{
					_logger.Debug($"Transaction with transaction info TransactionDate: {model.TransDate}, GLAccount: {model.GLAccount}, " +
					              $"PostSequence: {model.PostSeq}, BatchEntry: {model.BatchEntry}, SourceCode: {model.SourceCode} is already in DB. Skip this model");
					continue;
				}

				var trans = new Transaction
				{
					TransactionDate = model.TransDate,
					GLAccount = model.GLAccount,
					PostSequence = (int)model.PostSeq,
					BatchEntry = model.BatchEntry,
					SourceCode = model.SourceCode,
					Debit = (decimal)model.Debits,
					Credit = (decimal)model.Credits,
					ExchangeRate = (decimal)model.ExchRate
				};

				if (!TryDecodeYearPeriod(model.YearPeriod, out var yearPeriod))
				{
					_logger.Error($"Decoding YearPeriod faild, check above log for more information");
					continue;
				}
				trans.YearPeriod = yearPeriod;

				if (!TryDecodeVendorID(model.Description, out var vendorID))
				{
					_logger.Error($"Decoding VendorID faild, check above log for more information");
					continue;
				}
				trans.VendorID = vendorID;

				if (!string.IsNullOrWhiteSpace(model.Reference))
				{
					_logger.Debug($"Model has Reference, try to decode invoice info from: {model.Reference}");

					if (!TryDecodeInvoiceNo(model.Reference, out var invoiceNo))
					{
						_logger.Error($"Decoding InvoiceNo faild, check above log for more information");
						continue;
					}
					trans.InvoiceNo = invoiceNo;

					if (TryDecodeInvoiceDescription(model.Reference, out var invoiceDescription))
					{
						_logger.Warn($"Decoding InvoiceDescription faild, check above log for more information");
						trans.InvoiceDescription = invoiceDescription;
					}
				}

				_logger.Debug($"Transformation successed, add result for DB insertion.");
				transactions.Add(trans);
			}

			_logger.Debug($"Transform {transactions.Count} vaild transactions out of {source.Count} input. Insert them into DB");
			return transactions;
		}

		private bool TryDecodeYearPeriod(string input, out DateTime yearPeriod)
		{
			yearPeriod = default(DateTime);
			try
			{
				_logger.Debug($"Start decoding YearPeriod from input: {input}");
				var yearMonth = input.Split(" - ", StringSplitOptions.RemoveEmptyEntries);
				var year = int.Parse(yearMonth[0]) - 1;
				var month = int.Parse(yearMonth[1]);
				yearPeriod = new DateTime(year, month, 1).AddMonths(6);
				_logger.Debug($"Decode input: {input} to YearPeriod: {yearPeriod}");
				return true;
			}
			catch (Exception ex)
			{
				_logger.Error($"Exception happend during decoding YearPeriod from model YearPeriod: {input}. Ex: {ex.Message}");
				return false;
			}
		}

		private bool TryDecodeVendorID(string input, out string vendorID)
		{
			vendorID = default(String);
			try
			{
				_logger.Debug($"Start decoding VendorID from input: {input}");
				var vendorInfo = input.Split('*', StringSplitOptions.RemoveEmptyEntries);
				foreach (var info in vendorInfo)
				{
					if (!IsVendorID(info))
						continue;
					_logger.Debug($"Decode vendor ID: {info} from Description");
					vendorID = info;
					_logger.Debug($"Decode input: {input} to VendorID: {vendorID}");
					return true;
				}

				_logger.Debug($"Could not decode vendor ID from Description, check it from DB by vendor name");
				foreach (var info in vendorInfo)
				{
					var vendor = _vendorService.GetByVendorName(info);
					if (vendor == null)
						continue;
					vendorID = vendor.VendorID;
					_logger.Debug($"Get VendorID: {vendorID} from DB by VendorName: {info}");
					return true;
				}

				_logger.Error($"Could not get Vendor ID from neither Description: {input} nor DB, return false");
				return false;
			}
			catch (Exception ex)
			{
				_logger.Error($"Exception happend during decoding VendorID from model Description: {input}. Ex:{ex.Message}");
				return false;
			}
		}

		private bool TryDecodeInvoiceNo(string input, out string invoiceNo)
		{
			invoiceNo = default(String);
			try
			{
				_logger.Debug($"Start decoding InvoiceNo from input: {input}");
				var invoiceInfo = input.Split('*', StringSplitOptions.RemoveEmptyEntries);

				if (invoiceInfo.Length < 1)
				{
					_logger.Error($"Could not get InvoiceNo from model Reference: {input}");
					return false;
				}

				invoiceNo = invoiceInfo[0];
				_logger.Debug($"Decode input: {input} to InvoiceNo: {invoiceNo}");
				return true;
			}
			catch (Exception ex)
			{
				_logger.Error($"Exception happend during decoding InvoiceNo from model Reference: {input}. Ex: {ex.Message}");
				return false;
			}
		}

		private bool TryDecodeInvoiceDescription(string input, out string invoiceDescription)
		{
			invoiceDescription = default(String);
			try
			{
				_logger.Debug($"Start decoding InvoiceDescription from input: {input}");
				var invoiceInfo = input.Split('*', StringSplitOptions.RemoveEmptyEntries);

				if (invoiceInfo.Length < 2)
				{
					_logger.Warn($"Could not get InvoiceDescription from model Reference: {input}");
					return false;
				}

				invoiceDescription = string.Join('*', invoiceInfo, 1, invoiceInfo.Length - 1);
				_logger.Debug($"Decode input: {input} to InvoiceDescription: {invoiceDescription}");
				return true;
			}
			catch (Exception ex)
			{
				_logger.Error($"Exception happend during decoding InvoiceDescription from model Reference: {input}. Ex: {ex.Message}");
				return false;
			}
		}

		private bool IsDuplicateTransaction(TransactionModel model)
		{
			var transactionDate = model.TransDate;
			var glAccount = model.GLAccount;
			var postSequence = (int) model.PostSeq;
			var batchEntry = model.BatchEntry; 
			var sourceCode = model.SourceCode;
			var transaction = _transactionService.GetByTransactionInfo(transactionDate, glAccount, postSequence, batchEntry, sourceCode);
			return transaction != null;
		}

		private bool IsVendorID(string input)
		{
			var pattern = "^[A-Z]{1,2}[0-9]{4,5}[A-Z]$";
			return Regex.IsMatch(input, pattern);
		}
	}
}