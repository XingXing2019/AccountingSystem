using System;
using System.ComponentModel.DataAnnotations;

namespace AccountingHelper.Model
{
	public class TransactionModel
	{
		[Required]
		public DateTime TransDate { get; set; }

		[Required]
		public string YearPeriod { get; set; }

		[Required]
		public string GLAccount { get; set; }

		[Required]
		public double PostSeq { get; set; }

		[Required]
		public string BatchEntry { get; set; }

		[Required]
		public string SourceCode { get; set; }

		public string Reference { get; set; }

		[Required]
		public string Description { get; set; }

		[Required]
		public double Debits { get; set; }

		[Required]
		public double Credits { get; set; }

		[Required]
		public double ExchRate { get; set; }
	}
}