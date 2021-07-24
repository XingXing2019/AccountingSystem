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
			return $"\nWHERE\n\t{string.Join(", ", whereItems)}";
		}

		protected string GenerateGroupByClause(List<string> groupByItems)
		{
			return $"\nGROUP BY\n\t{string.Join(", ", groupByItems)}";
		}

		protected string GenerateOrderByClause(List<string> orderByItems)
		{
			return $"\nORDER BY\n\t{string.Join(", ", orderByItems)}";
		}

		protected string GeneratePaginationClause(int pageSize, int pageNumber)
		{
			return $"\nOFFSET {pageSize * (pageNumber - 1)} ROW FETCH NEXT {pageSize} ROW ONLY";
		}
	}
}