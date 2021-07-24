using System;
using System.Collections.Generic;
using System.Text;
using AccountingDatabase.SqlExecutor;
using AccountingDatabase.SqlRepo;

namespace AccountingHelper.Helper.DataAnalysisHelper
{
	public class TransactionAnalysisHelper : DataAnalysisHelperBase
	{
		public Dictionary<string, List<string>> AnalysisTransactionsInYearPeriod(List<string> selectItems, List<string> whereItems, List<string> groupByItems, List<string> orderByItems, DateTime startPeriod, DateTime endPeriod, int pageSize, int pageNumber)
		{
			var select = GenerateSelectClause(TransactionSqls.SELECT_TRANSACTIONS, selectItems);
			var where = GenerateWhereClause(whereItems);
			var groupBy = GenerateGroupByClause(groupByItems);
			var col = GenerateYearPeriodColumns(startPeriod, endPeriod);
			var orderBy = GenerateOrderByClause(orderByItems);
			var pagination = GeneratePaginationClause(pageSize, pageNumber);

			var sql = $"WITH Data AS ({select}{where}{groupBy})\n{string.Format(TransactionSqls.PIVOT_COLUMN_ROW, "SUM", "YearPeriod", col)}{orderBy}{pagination}";
			var values = new SqlExecutor().ExecuteSelectQuery(sql);
			return values;
		}

		private string GenerateYearPeriodColumns(DateTime startPeriod, DateTime endPeriod)
		{
			var cols = new StringBuilder();
			for (DateTime i = startPeriod; i <= endPeriod; i = i.AddMonths(1))
			{
				var col = i.ToString("yyyy-MM-dd HH:mm:ss.0000000");
				cols.Append($"[{col}]");
				if (i != endPeriod) cols.Append(',');
			}

			return cols.ToString();
		}
	}
}