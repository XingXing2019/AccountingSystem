using System;
using System.Collections.Generic;
using System.Linq;
using AccountingDatabase.Entity;
using AccountingDatabase.Repository.Interface;
using NLog;

namespace AccountingDatabase.Repository.Implementation
{
	public class VendorService : IVendorService
	{
		private readonly Logger _logger = LogManager.GetCurrentClassLogger();
		public Vendor Get(string id)
		{
			try
			{
				using var context = new AccountingDBContext();
				return context.Vendors.Find(id);
			}
			catch (Exception ex)
			{
				_logger.Error($"Failed to get vendor: {id}. Ex: {ex.Message}");
				_logger.Error($"Inner Ex: {ex.InnerException}");
			}

			return null;
		}

		public List<Vendor> GetAll()
		{
			try
			{
				using var context = new AccountingDBContext();
				return context.Vendors.ToList();
			}
			catch (Exception ex)
			{
				_logger.Error($"Failed to get all vendors. Ex: {ex.Message}");
				_logger.Error($"Inner Ex: {ex.InnerException}");
			}

			return null;
		}

		public bool Post(Vendor item)
		{
			try
			{
				using var context = new AccountingDBContext();
				context.Vendors.Add(item);
				var count = context.SaveChanges();
				_logger.Info($"Succesed post vendor: {item.VendorID}. {count} row affected");
				return true;
			}
			catch (Exception ex)
			{
				_logger.Error($"Failed to post vendor: {item.VendorID}. Ex: {ex.Message}");
				_logger.Error($"Inner Ex: {ex.InnerException}");
			}

			return false;
		}

		public bool PostAll(IList<Vendor> items)
		{
			try
			{
				using var context = new AccountingDBContext();
				context.Vendors.AddRange(items);
				var count = context.SaveChanges();
				_logger.Info($"Succesed to post vendors. {count} row affected");
				return true;
			}
			catch (Exception ex)
			{
				_logger.Error($"Failed to post vendors. Ex: {ex.Message}");
				_logger.Error($"Inner Ex: {ex.InnerException}");
			}

			return false;
		}
	}
}