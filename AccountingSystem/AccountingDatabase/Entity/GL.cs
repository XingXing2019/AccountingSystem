using System.ComponentModel.DataAnnotations;

namespace AccountingDatabase.Entity
{
	public class GL
	{
		[Key]
		public string GLCode { get; set; }

		[Required, MaxLength(100)]
		public string GLDescription { get; set; }

		[Required]
		public GLStatus Status { get; set; }

		[Required, MaxLength(20)]
		public string Configuration { get; set; }

		[Required, MaxLength(10)]
		public string In { get; set; }

		[Required, MaxLength(10)]
		public string Code { get; set; }
	}

	public enum GLStatus
	{
		Active, Inactive
	}
}