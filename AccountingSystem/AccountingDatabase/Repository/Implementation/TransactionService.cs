using System;
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
		public async Task<bool> PostTransaction(Transaction transaction)
		{
			try
			{
				using (var context = new AccountingDBContext())
				{
					context.Transactions.Add(transaction);
					await context.SaveChangesAsync();
					return true;
				}
			}
			catch (Exception ex)
			{
				_logger.Error($"Failed to post order line. Ex: {ex.Message}");
			}

			return false;
		}
	}
}