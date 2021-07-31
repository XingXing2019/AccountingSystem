using System;
using System.Data;

namespace AccountingInitializer.SQL
{
	public interface ISQLAction : ICloneable
	{
		void ExecuteSQL();

		DataTable GetSQLResult();
	}
}