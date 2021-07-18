using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AccountingDatabase.Entity
{
	public class Vendor
	{
		public Vendor()
		{
			Transactions = new List<Transaction>();
		}

		public int VendorId { get; set; }

		[Required, MaxLength(50)]
		public string VendorCode { get; set; }
		
		[Required, MaxLength(200)]
		public string VendorName { get; set; }

		public List<Transaction> Transactions { get; set; }
	}
}