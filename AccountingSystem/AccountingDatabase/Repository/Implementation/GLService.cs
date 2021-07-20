using System;
using System.Collections.Generic;
using System.Linq;
using AccountingDatabase.Entity;
using AccountingDatabase.Repository.Interface;
using NLog;

namespace AccountingDatabase.Repository.Implementation
{
	public class GLService : IGLService
	{
		private readonly Logger _logger = LogManager.GetCurrentClassLogger();

		public GLAccount Get(string glCode)
		{
			try
			{
				using var context = new AccountingDBContext();
				return context.GlAccounts.Find(glCode);
			}
			catch (Exception ex)
			{
				_logger.Error($"Failed to get GL: {glCode}. Ex: {ex.Message}");
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
				_logger.Error($"Failed to get all GL. Ex: {ex.Message}");
				_logger.Error($"Inner Ex: {ex.InnerException}");
			}

			return null;
		}

		public bool Post(GLAccount gl)
		{
			try
			{
				using var context = new AccountingDBContext();
				context.GlAccounts.Add(gl);
				var count = context.SaveChanges();
				_logger.Info($"Succesed post gl: {gl.AccountNumber}. {count} row affected");
				return true;
			}
			catch (Exception ex)
			{
				_logger.Error($"Failed to post transaction: {gl.AccountNumber}. Ex: {ex.Message}");
				_logger.Error($"Inner Ex: {ex.InnerException}");
			}

			return false;
		}

		public bool PostAll(IList<GLAccount> gls)
		{
			try
			{
				using var context = new AccountingDBContext();
				context.GlAccounts.AddRange(gls);
				var count = context.SaveChanges();
				_logger.Info($"Succesed to post gls. {count} row affected");
				return true;
			}
			catch (Exception ex)
			{
				_logger.Error($"Failed to post gls. Ex: {ex.Message}");
				_logger.Error($"Inner Ex: {ex.InnerException}");
			}

			return false;
		}
	}
}