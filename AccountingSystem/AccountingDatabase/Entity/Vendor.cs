using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AccountingDatabase.Entity
{
	public class Vendor
	{
		[Required, MaxLength(50)]
		public string VendorCode { get; set; }
		
		[Required, MaxLength(200)]
		public string VendorName { get; set; }
	}
}