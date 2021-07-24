using System;
using System.ComponentModel.DataAnnotations;

namespace AccountingHelper.Model
{
	public class VendorModel
	{
		[Required]
		public string VendorID { get; set; }

		[Required]
		public string VendName { get; set; }
		
		public string ShortName { get; set; }

		[Required]
		public string IDGRP { get; set; }

		[Required]
		public bool SwActv { get; set; }

		[Required]
		public bool SwHold { get; set; }

		[Required]
		public string CurnCode { get; set; }

		[Required]
		public int TermsCode { get; set; }

		[Required]
		public int TaxClass1 { get; set; }

		[Required]
		public DateTime DateLastMN { get; set; }

		[Required]
		public DateTime DateStart { get; set; }

		public string TextSTRE1 { get; set; }
		
		public string TextSTRE2 { get; set; }

		public string TextSTRE3 { get; set; }

		public string TextSTRE4 { get; set; }

		public string NameCity { get; set; }
		
		public string CodeSTTE { get; set; }
		
		public string CodePSTL { get; set; }

		public string CodeCTRY { get; set; }
		
		public string TextPHON1 { get; set; }

		public string TextPHON2 { get; set; }
	}
}