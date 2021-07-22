using System;
using System.Collections.Generic;
using AccountingDatabase.Entity;
using AccountingHelper.Interface;
using AccountingHelper.Model;

namespace AccountingHelper.Helper
{
	public class TransactionModelHelper : IModelHelper<TransactionModel, Transaction>
	{
		public List<Transaction> Transform(List<TransactionModel> source)
		{
			var transactions = new List<Transaction>();
			foreach (var model in source)
			{
				var trans = new Transaction
				{
					TransDate = model.TransDate,
					GLAccount = model.GLAccount,
					PostSeq = (int) model.PostSeq,
					BatchEntry = model.BatchEntry,
					SourceCode = model.SourceCode,
					Debit = (decimal) model.Debits,
					Credit = (decimal) model.Credits,
					ExchRate = (decimal) model.ExchRate
				};
				var yearMonth = model.YearPeriod.Split(" - ", StringSplitOptions.RemoveEmptyEntries);
				var year = int.Parse(yearMonth[0]) - 1;
				var month = int.Parse(yearMonth[1]);
				trans.YearPeriod = new DateTime(year, month, 1).AddMonths(6);

				if (!string.IsNullOrWhiteSpace(model.Reference))
				{
					var reference = model.Reference.Split("*", StringSplitOptions.RemoveEmptyEntries);

					trans.InvoiceNo = reference[0];

					var invoiceReceiveDate = reference[1].Split('/', StringSplitOptions.RemoveEmptyEntries);
					var receiveDay = int.Parse(invoiceReceiveDate[0]);
					var receiveMonth = int.Parse(invoiceReceiveDate[1]);
					var receiveYear = int.Parse(invoiceReceiveDate[2]);
					trans.InvoiceReceiveDate = new DateTime(receiveYear, receiveMonth, receiveDay);
				}

				var description = model.Description.Split('*', StringSplitOptions.RemoveEmptyEntries);
				trans.VendName = description[0];
				trans.VendorID = description[1];

				transactions.Add(trans);
			}
			
			return transactions;
		}
	}
}