using System.ComponentModel.DataAnnotations;

namespace AccountingHelper.Model
{
	public class GLAccountModel
	{
		[Required]
		public string AccountNumber { get; set; }

		[Required]
		public string Description { get; set; }

		[Required]
		public string Status { get; set; }

		[Required]
		public string Config { get; set; }

		[Required]
		public string In { get; set; }

		[Required]
		public string Code { get; set; }
	}
}