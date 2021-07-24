using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using NLog;

namespace AccountingDatabase.SqlExecutor
{
	public class SqlExecutor
	{
		private const string ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=AccountingSystem";
		private readonly Logger _logger = LogManager.GetCurrentClassLogger();

		public Dictionary<string, List<string>> ExecuteSelectQuery(string selectSql)
		{
			try
			{
				_logger.Debug($"Start execute sql: {selectSql}");
				var columnValues = new Dictionary<string, List<string>>();
				using var sqlConnection = new SqlConnection(ConnectionString);
				if (sqlConnection.State != ConnectionState.Open)
					sqlConnection.Open();

				var command = sqlConnection.CreateCommand();
				command.CommandText = selectSql;
				var reader = command.ExecuteReader();

				var dataTable = new DataTable();
				if (!reader.HasRows)
				{
					_logger.Debug($"No record return from sql: {selectSql}");
					return columnValues;
				}

				dataTable.Load(reader);
				reader.Close();

				var titleColumns = new Dictionary<int, string>();
				for (int i = 0; i < dataTable.Columns.Count; i++)
				{
					var title = dataTable.Columns[i].ToString();
					titleColumns[i] = title;
					columnValues[title] = new List<string>();
				}
				
				foreach (DataRow row in dataTable.Rows)
				{
					for (int i = 0; i < row.ItemArray.Length; i++)
					{
						var title = titleColumns[i];
						columnValues[title].Add(row.ItemArray[i].ToString());
					}
				}

				return columnValues;
			}
			catch (Exception ex)
			{
				_logger.Error($"Exception happened during executing select query: {selectSql}. Ex: {ex.Message}");
				return null;
			}
		}

		public void ExecuteDeleteUpdateInsertQuery(string sql)
		{
			try
			{
				_logger.Debug($"Start execute sql: {sql}");
				using var sqlConnection = new SqlConnection(ConnectionString);
				if (sqlConnection.State != ConnectionState.Open)
					sqlConnection.Open();

				var command = sqlConnection.CreateCommand();
				command.CommandText = sql;
				var count = command.ExecuteNonQuery();
				_logger.Debug($"Successed execute sql: {sql}. {count} rows affected");
			}
			catch (Exception ex)
			{
				_logger.Error($"Exception happened during executing select query: {sql}. Ex: {ex.Message}");
			}
		}
	}
}