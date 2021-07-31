using System;
using System.Collections.Generic;
using System.Data;

namespace AccountingInitializer.SQL
{
	public interface ISQLAction : ICloneable
	{
		void ExecuteSQL();

		void SetSQLVariables(Dictionary<string, object> data);

		DataTable GetSQLResult();
	}
}