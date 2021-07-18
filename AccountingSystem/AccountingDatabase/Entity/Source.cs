using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AccountingDatabase.Entity
{
	public class Source
	{
		public Source()
		{
			Transactions = new List<Transaction>();
		}

		public int SourceId { get; set; }

		[Required, MaxLength(20)]
		public string Code { get; set; }

		public List<Transaction> Transactions { get; set; }
	}
}