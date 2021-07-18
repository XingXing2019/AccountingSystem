﻿using System;
using System.ComponentModel.DataAnnotations;

namespace AccountingDatabase.Entity
{
	public class Transaction
	{
		public int TransactionId { get; set; }

		[Required, MaxLength(50)]
		public string BatchEntry { get; set; }

		[Required, MaxLength(50)]
		public string GLCode { get; set; }

		[Required, MaxLength(100)]
		public string GLDescription { get; set; }

		[Required, MaxLength(50)]
		public string PostingSeq { get; set; }

		[MaxLength(100)]
		public string InvoiceNo { get; set; }

		[MaxLength(200)]
		public string Description { get; set; }
		
		public decimal? Debit { get; set; }
		public decimal? Credit { get; set; }

		[Required]
		public DateTime DocDate { get; set; }

		[Required]
		public DateTime TransactionDate { get; set; }
		public DateTime? InvoiceReceiveDate { get; set; }

		[Required]
		public Vendor Vendor { get; set; }

		public Source Source { get; set; }
	}
}