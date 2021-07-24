using System;
using System.Collections.Generic;
using System.Linq;
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
		public Transaction GetByID(string id)
		{
			try
			{
				using var context = new AccountingDBContext();
				return context.Transactions.Find(id);
			}
			catch (Exception ex)
			{
				_logger.Error($"Failed to get transaction: {id}. Ex: {ex.Message}");
				_logger.Error($"Inner Ex: {ex.InnerException}");
			}

			return null;
		}

		public Transaction GetByTransactionInfo(DateTime transactionDate, string glAccount, int postSequence, string batchEntry, string sourceCode)
		{
			try
			{
				using var context = new AccountingDBContext();
				return context.Transactions
					.FirstOrDefault(x => x.TransactionDate == transactionDate &&
					                     x.GLAccount == glAccount &&
					                     x.PostSequence == postSequence &&
					                     x.BatchEntry == batchEntry &&
					                     x.SourceCode == sourceCode);
			}
			catch (Exception ex)
			{
				_logger.Error($"Failed to get transaction with transaction info TransactionDate: {transactionDate}, GLAccount: {glAccount}, PostSequence: {postSequence}, BatchEntry: {batchEntry}, SourceCode: {sourceCode}. Ex: {ex.Message}");
				_logger.Error($"Inner Ex: {ex.InnerException}");
			}

			return null;
		}

		public List<Transaction> GetAll()
		{
			try
			{
				using var context = new AccountingDBContext();
				return context.Transactions.ToList();
			}
			catch (Exception ex)
			{
				_logger.Error($"Failed to get all transactions. Ex: {ex.Message}");
				_logger.Error($"Inner Ex: {ex.InnerException}");
			}

			return null;
		}

		public bool Post(Transaction item)
		{
			try
			{
				using var context = new AccountingDBContext();
				context.Transactions.Add(item);
				var count = context.SaveChanges();
				_logger.Info($"Succesed post transaction: {item.BatchEntry}. {count} row affected");
				return true;
			}
			catch (Exception ex)
			{
				_logger.Error($"Failed to post transaction: {item.BatchEntry}. Ex: {ex.Message}");
				_logger.Error($"Inner Ex: {ex.InnerException}");
			}

			return false;
		}

		public bool PostAll(IList<Transaction> items)
		{
			try
			{
				using var context = new AccountingDBContext();
				context.Transactions.AddRange(items);
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