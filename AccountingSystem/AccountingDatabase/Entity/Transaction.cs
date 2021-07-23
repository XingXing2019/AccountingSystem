using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountingDatabase.Entity
{
	public class Transaction
	{
		public int TransactionID { get; set; }

		[Required]
		public DateTime TransactionDate { get; set; }

		// Decode from YearPeriod string in TransactionModel
		[Required]
		public DateTime YearPeriod { get; set; }

		[Required, MaxLength(100)]
		public string GLAccount { get; set; }

		[Required]
		public int PostSequence { get; set; }

		[Required, MaxLength(50)]
		public string BatchEntry { get; set; }

		[Required, MaxLength(20)]
		public string SourceCode { get; set; }
		
		[Required]
		public decimal Debit { get; set; }

		[Required]
		public decimal Credit { get; set; }

		[Required]
		public decimal ExchangeRate { get; set; }
		
		// Decode from Reference[0] in excel
		[MaxLength(100)]
		public string InvoiceNo { get; set; }
		
		// Decode from Reference[1] in excel
		[MaxLength(200)]
		public string InvoiceDescription { get; set; }
		
		// Decode from Description[1] in excel
		[Required, MaxLength(50)]
		public string VendorID { get; set; }
	}
}