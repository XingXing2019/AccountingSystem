using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using AccountingDatabase.SqlExecutor;
using AccountingDatabase.SqlRepo;

namespace AccountingHelper.Helper.DataAnalysisHelper
{
	public class TransactionAnalysisHelper : DataAnalysisHelperBase
	{
		public DataTable AnalysisTransactionsInYearPeriod(List<string> selectItems, Dictionary<string, string> joinItems, List<string> whereItems, List<string> groupByItems, List<string> orderByItems, DateTime startPeriod, DateTime endPeriod, int pageSize = 0, int pageNumber = 0)
		{
			var select = GenerateSelectClause(TransactionSqls.SELECT_TRANSACTIONS, selectItems);
			var join = GenerateJoinClause(joinItems);
			var where = GenerateWhereClause(whereItems);
			var groupBy = GenerateGroupByClause(groupByItems);
			var col = GenerateYearPeriodColumns(startPeriod, endPeriod);
			var orderBy = GenerateOrderByClause(orderByItems);

			var sql = $"WITH Data AS ({select}{join}{where}{groupBy})\n{string.Format(TransactionSqls.PIVOT_COLUMN_ROW, "SUM", "YearPeriod", col)}{orderBy}";

			if (pageSize != 0 && pageNumber != 0)
			{
				var pagination = GeneratePaginationClause(pageSize, pageNumber);
				sql += pagination;
			}
			var values = new SqlExecutor().ExecuteSelectQuery(sql);
			return values;
		}

		private string GenerateYearPeriodColumns(DateTime startPeriod, DateTime endPeriod)
		{
			var cols = new StringBuilder();
			for (DateTime i = startPeriod; i <= endPeriod; i = i.AddMonths(1))
			{
				//var col = i.ToString("yyyy-MM-dd HH:mm:ss.0000000");
				var col = $"{i.Year:0000}-{i.Month:00}";
				cols.Append($"[{col}]");
				if (i != endPeriod) cols.Append(',');
			}

			return cols.ToString();
		}
	}
}