using System.Collections.Generic;

namespace AccountingHelper.Helper.DataAnalysisHelper
{
	public class SqlEntity
	{
		public List<string> SelectItems { get; set; }
		public Dictionary<string, string> JoinItems { get; set; }
		public List<string> WhereItems { get; set; }
		public List<string> GroupByItems { get; set; }
		public List<string> OrderByItems { get; set; }
		public int PageSize { get; set; }
		public int PageNumber { get; set; }

		public SqlEntity(List<string> selectItems, Dictionary<string, string> joinItems, List<string> whereItems, List<string> groupByItems, List<string> orderByItems, int pageSize = 0, int pageNumber = 0)
		{
			SelectItems = selectItems;
			JoinItems = joinItems;
			WhereItems = whereItems;
			GroupByItems = groupByItems;
			OrderByItems = orderByItems;
			PageSize = pageSize;
			PageNumber = pageNumber;
		}
	}
}