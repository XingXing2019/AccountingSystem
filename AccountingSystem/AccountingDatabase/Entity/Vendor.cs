using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AccountingDatabase.Entity
{
	public class Vendor
	{
		[Required, MaxLength(50)]
		public string VendorID { get; set; }

		[Required, MaxLength(200)]
		public string VendName { get; set; }

		[MaxLength(50)]
		public string ShortName { get; set; }

		[Required, MaxLength(20)]
		public string IDGRP { get; set; }

		[Required]
		public bool SwActv { get; set; }

		[Required]
		public bool SwHold { get; set; }

		[Required, MaxLength(5)]
		public string CurnCode { get; set; }

		[Required]
		public int TermsCode { get; set; }

		[Required]
		public int TaxClass1 { get; set; }
		
		[Required] 
		public DateTime DateLastMN { get; set; }

		[Required]
		public DateTime DateStart { get; set; }
		
		[MaxLength(100)]
		public string TextSTRE1 { get; set; }

		[MaxLength(100)]
		public string TextSTRE2 { get; set; }

		[MaxLength(100)]
		public string TextSTRE3 { get; set; }

		[MaxLength(100)]
		public string TextSTRE4 { get; set; }

		[MaxLength(100)]
		public string NameCity { get; set; }

		[MaxLength(50)]
		public string CodeSTTE { get; set; }

		[MaxLength(50)]
		public string CodePSTL { get; set; }

		[MaxLength(50)]
		public string CodeCTRY { get; set; }

		[MaxLength(50)]
		public string TextPHON1 { get; set; }

		[MaxLength(50)]
		public string TextPHON2 { get; set; }
	}
}