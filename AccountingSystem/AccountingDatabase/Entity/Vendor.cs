using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountingDatabase.Entity
{
	public class Vendor
	{
		[Required, MaxLength(50)]
		public string VendorID { get; set; }

		[Required, MaxLength(200), Column("VendorName")]
		public string VendName { get; set; }

		[MaxLength(50)]
		public string ShortName { get; set; }

		[Required, MaxLength(20)]
		public string IDGRP { get; set; }

		[Required, Column("Active")]
		public bool SwActv { get; set; }

		[Required, Column("OnHold")]
		public bool SwHold { get; set; }

		[Required, MaxLength(5), Column("CurrencyCode")]
		public string CurnCode { get; set; }

		[Required]
		public int TermsCode { get; set; }

		[Required]
		public int TaxClass1 { get; set; }
		
		[Required, Column("LastMaintenanceDate")] 
		public DateTime DateLastMN { get; set; }

		[Required, Column("StartDate")]
		public DateTime DateStart { get; set; }
		
		[MaxLength(100), Column("Address1")]
		public string TextSTRE1 { get; set; }

		[MaxLength(100), Column("Address2")]
		public string TextSTRE2 { get; set; }

		[MaxLength(100), Column("Address3")]
		public string TextSTRE3 { get; set; }

		[MaxLength(100), Column("Address4")]
		public string TextSTRE4 { get; set; }

		[MaxLength(100), Column("City")]
		public string NameCity { get; set; }

		[MaxLength(50), Column("State")]
		public string CodeSTTE { get; set; }

		[MaxLength(50), Column("PostCode")]
		public string CodePSTL { get; set; }

		[MaxLength(50), Column("Country")]
		public string CodeCTRY { get; set; }

		[MaxLength(50), Column("Phone1")]
		public string TextPHON1 { get; set; }

		[MaxLength(50), Column("Phone2")]
		public string TextPHON2 { get; set; }
	}
}