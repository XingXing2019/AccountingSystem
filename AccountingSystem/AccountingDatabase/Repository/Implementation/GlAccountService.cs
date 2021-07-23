using System;
using System.Collections.Generic;
using System.Linq;
using AccountingDatabase.Entity;
using AccountingDatabase.Repository.Interface;
using NLog;

namespace AccountingDatabase.Repository.Implementation
{
	public class GlAccountService : IGlAccountService
	{
		private readonly Logger _logger = LogManager.GetCurrentClassLogger();

		public GLAccount GetByID(string id)
		{
			try
			{
				using var context = new AccountingDBContext();
				return context.GlAccounts.Find(id);
			}
			catch (Exception ex)
			{
				_logger.Error($"Failed to get Gl account: {id}. Ex: {ex.Message}");
				_logger.Error($"Inner Ex: {ex.InnerException}");
			}

			return null;
		}

		public List<GLAccount> GetAll()
		{
			try
			{
				using var context = new AccountingDBContext();
				return context.GlAccounts.ToList();
			}
			catch (Exception ex)
			{
				_logger.Error($"Failed to get all Gl accounts. Ex: {ex.Message}");
				_logger.Error($"Inner Ex: {ex.InnerException}");
			}

			return null;
		}

		public bool Post(GLAccount item)
		{
			try
			{
				using var context = new AccountingDBContext();
				context.GlAccounts.Add(item);
				var count = context.SaveChanges();
				_logger.Info($"Succesed post Gl account: {item.AccountNumber}. {count} row affected");
				return true;
			}
			catch (Exception ex)
			{
				_logger.Error($"Failed to post Gl account: {item.AccountNumber}. Ex: {ex.Message}");
				_logger.Error($"Inner Ex: {ex.InnerException}");
			}

			return false;
		}

		public bool PostAll(IList<GLAccount> items)
		{
			try
			{
				using var context = new AccountingDBContext();
				context.GlAccounts.AddRange(items);
				var count = context.SaveChanges();
				_logger.Info($"Succesed to post Gl accounts. {count} row affected");
				return true;
			}
			catch (Exception ex)
			{
				_logger.Error($"Failed to post Gl accounts. Ex: {ex.Message}");
				_logger.Error($"Inner Ex: {ex.InnerException}");
			}

			return false;
		}
	}
}