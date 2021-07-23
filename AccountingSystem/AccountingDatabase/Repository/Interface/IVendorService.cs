using System.Collections.Generic;
using AccountingDatabase.Entity;

namespace AccountingDatabase.Repository.Interface
{
	public interface IVendorService
	{
		Vendor GetByID(string id);

		Vendor GetByVendorName(string vendorName);

		List<Vendor> GetAll();

		bool Post(Vendor item);

		bool PostAll(IList<Vendor> items);
	}
}