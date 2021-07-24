using System.ComponentModel.DataAnnotations;

namespace AccountingDatabase.Entity
{
	public class GLAccount
	{
		[Required, MaxLength(100)]
		public string AccountNumber { get; set; }

		[Required, MaxLength(100)]
		public string Description { get; set; }

		[Required]
		public string Status { get; set; }

		[Required, MaxLength(20)]
		public string Configuration { get; set; }

		[Required, MaxLength(10)]
		public string In { get; set; }

		[Required, MaxLength(10)]
		public string Code { get; set; }
	}
}