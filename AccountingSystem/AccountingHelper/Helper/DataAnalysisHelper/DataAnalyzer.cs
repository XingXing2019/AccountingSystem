using System;
using System.Collections.Generic;
using AccountingInitializer.SQL;
using System.Data;

namespace AccountingHelper.Helper.DataAnalysisHelper
{
	public class DataAnalyzer
	{
		public bool TryExecuteSQLAction(string templateId, string sqlId, out DataTable sqlResult)
		{
			sqlResult = null;
			if (!SQLManager.Instance.TryGetSqlAction(templateId, sqlId, out var sqlAction))
			{
				return false;
			}
			sqlAction.ExecuteSQL();
			sqlResult = sqlAction.GetSQLResult();
			return true;
		}
	}
}