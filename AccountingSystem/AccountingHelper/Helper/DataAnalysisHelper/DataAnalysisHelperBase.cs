using System.Collections.Generic;

namespace AccountingHelper.Helper.DataAnalysisHelper
{
	public class DataAnalysisHelperBase
	{
		protected string GenerateSelectClause(string select, List<string> selectItems)
		{
			if (selectItems == null || selectItems.Count == 0)
				return string.Format(select, "*");
			return $"{string.Format(select, string.Join(", ", selectItems))}";
		}

		protected string GenerateJoinClause(Dictionary<string, string> joinItems)
		{
			var join = "";
			foreach (var item in joinItems)
				join += $"\nJOIN\n\t{item.Key} ON {item.Value}";

			return join;
		}

		protected string GenerateWhereClause(List<string> whereItems)
		{
			if (whereItems == null || whereItems.Count == 0)
				return string.Empty;
			return $"\nWHERE\n\t{string.Join(", ", whereItems)}";
		}

		protected string GenerateGroupByClause(List<string> groupByItems)
		{
			if (groupByItems == null || groupByItems.Count == 0)
				return string.Empty;
			return $"\nGROUP BY\n\t{string.Join(", ", groupByItems)}";
		}

		protected string GenerateOrderByClause(List<string> orderByItems)
		{
			if (orderByItems == null || orderByItems.Count == 0)
				return string.Empty;
			return $"\nORDER BY\n\t{string.Join(", ", orderByItems)}";
		}

		protected string GeneratePaginationClause(int pageSize, int pageNumber)
		{
			if (pageSize == 0 || pageNumber == 0)
				return string.Empty;
			return $"\nOFFSET {pageSize * (pageNumber - 1)} ROW FETCH NEXT {pageSize} ROW ONLY";
		}
	}
}