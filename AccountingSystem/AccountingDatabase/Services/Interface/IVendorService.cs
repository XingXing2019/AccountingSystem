using System.Collections.Generic;
using System.Linq;
using AccountingDatabase.Entity;

namespace AccountingDatabase.Services.Interface
{
	public interface IVendorService
	{
		Vendor GetByID(string id);

		Vendor GetByVendorName(string vendorName);

		IQueryable<Vendor> GetAll();

		bool Post(Vendor item);

		bool PostAll(IList<Vendor> items);
	}
}