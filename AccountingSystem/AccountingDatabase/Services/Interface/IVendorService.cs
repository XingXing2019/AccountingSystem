using System.Linq;
using AccountingDatabase.Entity;

namespace AccountingDatabase.Services.Interface
{
	public interface IVendorService : IService<Vendor>
	{
		Vendor GetByVendorName(string vendorName);
	}
}