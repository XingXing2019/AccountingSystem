using System.Collections.Generic;

namespace AccountingHelper.Helper.DataAnalysisHelper
{
	public class DataAnalysisHelperBase
	{
		protected string GenerateSelectClause(string select, List<string> selectItems)
		{
			return $"{string.Format(select, string.Join(", ", selectItems))}";
		}

		protected string GenerateWhereClause(List<string> whereItems)
		{
			return $"WHERE\n\t{string.Join(", ", whereItems)}";
		}

		protected string GenerateGroupByClause(List<string> groupbyItems)
		{
			return $"\nGROUP BY\n\t{string.Join(", ", groupbyItems)}";
		}
	}
}