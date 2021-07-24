using System;
using System.ComponentModel.DataAnnotations;

namespace AccountingDatabase.Entity
{
	public class Vendor
	{
		[Required, MaxLength(50)]
		public string VendorID { get; set; }

		[Required, MaxLength(200)]
		public string VendorName { get; set; }

		[MaxLength(50)]
		public string ShortName { get; set; }

		[Required, MaxLength(20)]
		public string IDGRP { get; set; }

		[Required]
		public bool Active { get; set; }

		[Required]
		public bool OnHold { get; set; }

		[Required]
		public string CurrencyCode { get; set; }

		[Required]
		public int TermsCode { get; set; }

		[Required]
		public int TaxClass1 { get; set; }
		
		[Required] 
		public DateTime LastMaintenanceDate { get; set; }

		[Required]
		public DateTime StartDate { get; set; }
		
		[MaxLength(100)]
		public string Address1 { get; set; }

		[MaxLength(100)]
		public string Address2 { get; set; }

		[MaxLength(100)]
		public string Address3 { get; set; }

		[MaxLength(100)]
		public string Address4 { get; set; }

		[MaxLength(100)]
		public string City { get; set; }

		[MaxLength(50)]
		public string State { get; set; }

		[MaxLength(50)]
		public string PostCode { get; set; }

		[MaxLength(50)]
		public string Country { get; set; }

		[MaxLength(50)]
		public string Phone1 { get; set; }

		[MaxLength(50)]
		public string Phone2 { get; set; }
	}
}