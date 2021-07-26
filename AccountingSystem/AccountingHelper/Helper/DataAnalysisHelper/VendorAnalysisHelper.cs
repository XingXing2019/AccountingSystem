using System.Collections.Generic;
using System.Data;
using AccountingDatabase.SqlExecutor;
using AccountingDatabase.SqlRepo;

namespace AccountingHelper.Helper.DataAnalysisHelper
{
	public class VendorAnalysisHelper : DataAnalysisHelperBase
	{
		public DataTable AnalyzeVendorGroups()
		{
			var selectItems = new List<string> {"DISTINCT GroupID"};
			var orderByItems = new List<string> {"GroupID"};

			var select = GenerateSelectClause(VendorSqls.SELECT_VENDOR, selectItems);
			var orderBy = GenerateOrderByClause(orderByItems);

			var sql = $"{select}{orderBy}";

			return SqlExecutor.ExecuteSelectQuery(sql);
		}
	}
}
