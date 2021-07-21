using System.Collections.Generic;
using AccountingDatabase.Entity;

namespace AccountingDatabase.Repository.Interface
{
	public interface IVendorService
	{
		Vendor Get(string id);
		List<Vendor> GetAll();

		bool Post(Vendor item);
		bool PostAll(IList<Vendor> items);
	}
}