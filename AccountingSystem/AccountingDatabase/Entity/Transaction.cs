using System;
using System.ComponentModel.DataAnnotations;

namespace AccountingDatabase.Entity
{
	public class Transaction
	{
		public int TransactionID { get; set; }

		[Required]
		public DateTime TransDate { get; set; }

		// Convert from YearPeriod string in TransactionModel
		[Required]
		public DateTime YearPeriod { get; set; }

		[Required, MaxLength(100)]
		public string GLAccount { get; set; }

		[Required]
		public int PostSeq { get; set; }

		[Required, MaxLength(50)]
		public string BatchEntry { get; set; }

		[Required, MaxLength(20)]
		public string SourceCode { get; set; }
		
		public decimal Debit { get; set; }
		public decimal Credit { get; set; }

		[Required]
		public decimal ExchRate { get; set; }


		// Decode from Reference[0] in excel
		[MaxLength(100)]
		public string InvoiceNo { get; set; }

		// Decode from Reference[1] in excel
		public DateTime? InvoiceReceiveDate { get; set; }

		// Decode from Reference[2] in excel
		[MaxLength(200)]
		public string Description { get; set; }


		// Decode from Description[0] in excel
		[Required, MaxLength(50)]
		public string VendorID { get; set; }

		// Decode from Description[1] in excel
		[Required, MaxLength(200)]
		public string VendName { get; set; }
	}
}