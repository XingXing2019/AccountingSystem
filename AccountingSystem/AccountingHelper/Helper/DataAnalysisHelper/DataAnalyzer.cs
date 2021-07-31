using AccountingInitializer.SQL;
using System.Data;

namespace AccountingHelper.Helper.DataAnalysisHelper
{
	public class DataAnalyzer
	{
		public DataTable ExecuteSQLAction(string templateId, string sqlId)
		{
			if (!SQLManager.Instance.TryGetSqlAction(templateId, sqlId, out var sqlAction))
			{
				return null;
			}

			sqlAction.ExecuteSQL();
			return sqlAction.GetSQLResult();
		}
	}
}