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
		public DataTable AnalyzeTransactionsInYearPeriod(SqlEntity entity,  DateTime startPeriod, DateTime endPeriod)
		{
			var select = GenerateSelectClause(TransactionSqls.SELECT_TRANSACTIONS, entity.SelectItems);
			var join = GenerateJoinClause(entity.JoinItems);
			var where = GenerateWhereClause(entity.WhereItems);
			var groupBy = GenerateGroupByClause(entity.GroupByItems);
			var col = GenerateYearPeriodColumns(startPeriod, endPeriod);
			var orderBy = GenerateOrderByClause(entity.OrderByItems);
			var pagination = GeneratePaginationClause(entity.PageSize, entity.PageNumber);

			var sql = $"WITH Data AS ({select}{join}{where}{groupBy})\n{string.Format(TransactionSqls.PIVOT_COLUMN_ROW, "SUM", "YearPeriod", col)}{orderBy}{pagination}";

			var values = SqlExecutor.ExecuteSelectQuery(sql);
			return values;
		}

		#region Helper
		
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

		#endregion
	}
}