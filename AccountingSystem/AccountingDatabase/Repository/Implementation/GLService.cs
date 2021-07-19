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

		public GL Get(string glCode)
		{
			try
			{
				using var context = new AccountingDBContext();
				return context.Gls.Find(glCode);
			}
			catch (Exception ex)
			{
				_logger.Error($"Failed to get GL: {glCode}. Ex: {ex.Message}");
				_logger.Error($"Inner Ex: {ex.InnerException}");
			}

			return null;
		}

		public List<GL> GetAll()
		{
			try
			{
				using var context = new AccountingDBContext();
				return context.Gls.ToList();
			}
			catch (Exception ex)
			{
				_logger.Error($"Failed to get all GL. Ex: {ex.Message}");
				_logger.Error($"Inner Ex: {ex.InnerException}");
			}

			return null;
		}

		public bool Post(GL gl)
		{
			try
			{
				using var context = new AccountingDBContext();
				context.Gls.Add(gl);
				var count = context.SaveChanges();
				_logger.Info($"Succesed post gl: {gl.GLCode}. {count} row affected");
				return true;
			}
			catch (Exception ex)
			{
				_logger.Error($"Failed to post transaction: {gl.GLCode}. Ex: {ex.Message}");
				_logger.Error($"Inner Ex: {ex.InnerException}");
			}

			return false;
		}

		public bool PostAll(IList<GL> gls)
		{
			try
			{
				using var context = new AccountingDBContext();
				context.Gls.AddRange(gls);
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