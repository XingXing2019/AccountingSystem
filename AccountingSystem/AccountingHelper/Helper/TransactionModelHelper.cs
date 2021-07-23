using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using AccountingDatabase.Entity;
using AccountingDatabase.Repository.Implementation;
using AccountingDatabase.Repository.Interface;
using AccountingHelper.Interface;
using AccountingHelper.Model;
using NLog;

namespace AccountingHelper.Helper
{
	public class TransactionModelHelper : IModelHelper<TransactionModel, Transaction>
	{
		private readonly IVendorService _vendorService = new VendorService();
		private readonly Logger _logger = LogManager.GetCurrentClassLogger();


		public List<Transaction> Transform(List<TransactionModel> source)
		{
			var transactions = new List<Transaction>();
			foreach (var model in source)
			{
				var trans = new Transaction
				{
					TransactionDate = model.TransDate,
					YearPeriod = DecodeYearPeriod(model.YearPeriod),
					GLAccount = model.GLAccount,
					PostSequence = (int) model.PostSeq,
					BatchEntry = model.BatchEntry,
					SourceCode = model.SourceCode,
					Debit = (decimal) model.Debits,
					Credit = (decimal) model.Credits,
					ExchangeRate = (decimal) model.ExchRate,
					VendorID = DecodeDescription(model.Description)
				};

				if (!string.IsNullOrWhiteSpace(model.Reference))
				{
					_logger.Debug($"Decode invoice information from Reference: {model.Reference}");
					var invoiceInfo = model.Reference.Split('*', StringSplitOptions.RemoveEmptyEntries);

					if (invoiceInfo.Length > 0)
						trans.InvoiceNo = invoiceInfo[0];

					if (invoiceInfo.Length > 1)
						trans.InvoiceDescription = string.Join('*', invoiceInfo, 1, invoiceInfo.Length - 1);
				}

				transactions.Add(trans);
			}

			return transactions;
		}

		private DateTime DecodeYearPeriod(string yearPeriod)
		{
			var yearMonth = yearPeriod.Split(" - ", StringSplitOptions.RemoveEmptyEntries);
			var year = int.Parse(yearMonth[0]) - 1;
			var month = int.Parse(yearMonth[1]);
			return new DateTime(year, month, 1).AddMonths(6);
		}

		private string DecodeDescription(string description)
		{
			var vendorInfo = description.Split('*', StringSplitOptions.RemoveEmptyEntries);
			foreach (var info in vendorInfo)
			{
				if (!IsVendorID(info))
					continue;
				_logger.Debug($"Decode vendor ID: {info} from Description");
				return info;
			}

			_logger.Debug($"Could not decode vendor ID from Description, check it from DB by vendor name");
			foreach (var info in vendorInfo)
			{
				var vendor = _vendorService.GetByVendorName(info);
				if (vendor == null)
					continue;
				_logger.Debug($"Get vendor ID: {vendor.VendorID} by its name: {info}");
				return vendor.VendorID;
			}

			_logger.Debug($"Could not get vendor ID from neither Description nor DB, return an empty string");
			return string.Empty;
		}
		
		private bool IsVendorID(string input)
		{
			var pattern = "^[A-Z][0-9]{5}[A-Z]$";
			return Regex.IsMatch(input, pattern);
		}
	}
}