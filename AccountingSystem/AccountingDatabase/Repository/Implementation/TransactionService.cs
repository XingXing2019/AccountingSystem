using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AccountingDatabase.Entity;
using AccountingDatabase.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using NLog;

namespace AccountingDatabase.Repository.Implementation
{
	public class TransactionService : ITransactionService
	{
		private readonly Logger _logger = LogManager.GetCurrentClassLogger();
		public bool Post(Transaction transaction)
		{
			try
			{
				using var context = new AccountingDBContext();
				context.Transactions.Add(transaction);
				var count = context.SaveChanges();
				_logger.Info($"Succesed post transaction: {transaction.BatchEntry}. {count} row affected");
				return true;
			}
			catch (Exception ex)
			{
				_logger.Error($"Failed to post transaction: {transaction.BatchEntry}. Ex: {ex.Message}");
				_logger.Error($"Inner Ex: {ex.InnerException}");
			}

			return false;
		}

		public bool PostAll(IList<Transaction> transactions)
		{
			try
			{
				using var context = new AccountingDBContext();
				context.Transactions.AddRange(transactions);
				var count = context.SaveChanges();
				_logger.Info($"Succesed to post transactions. {count} row affected");
				return true;
			}
			catch (Exception ex)
			{
				_logger.Error($"Failed to post transactions. Ex: {ex.Message}");
				_logger.Error($"Inner Ex: {ex.InnerException}");
			}

			return false;
		}
	}
}