using System;
using System.Collections.Generic;
using System.Linq;
using AccountingDatabase.Entity;
using AccountingDatabase.Services.Interface;
using NLog;

namespace AccountingDatabase.Services
{
	public class VendorService : IVendorService
	{
		private readonly Logger _logger = LogManager.GetCurrentClassLogger();
		public Vendor GetByID(string id)
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

		public Vendor GetByVendorName(string vendorName)
		{
			try
			{
				using var context = new AccountingDBContext();
				return context.Vendors.FirstOrDefault(x => x.VendorName == vendorName);
			}
			catch (Exception ex)
			{
				_logger.Error($"Failed to get vendor: {vendorName}. Ex: {ex.Message}");
				_logger.Error($"Inner Ex: {ex.InnerException}");
			}

			return null;
		}

		public IQueryable<Vendor> GetAll()
		{
			try
			{
				var context = new AccountingDBContext();
				return context.Vendors;
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