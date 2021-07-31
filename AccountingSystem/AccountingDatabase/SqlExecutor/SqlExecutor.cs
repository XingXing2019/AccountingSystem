using System;
using System.Collections.Generic;
using System.Data;
using AccountingInitializer.Database;
using Microsoft.Data.SqlClient;
using NLog;

namespace AccountingDatabase.SqlExecutor
{
	public class SqlExecutor
	{
		private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
		private const string DatabaseName = "AccountingSystem";
		private static string _connectionString = DatabaseManager.Instance.GetConnectionString(DatabaseName);

		public static DataTable ExecuteSelectQuery(string selectSql)
		{
			try
			{
				_logger.Debug($"Start execute sql: {selectSql}");
				var columnValues = new Dictionary<string, List<string>>();
				using var sqlConnection = new SqlConnection(_connectionString);
				if (sqlConnection.State != ConnectionState.Open)
					sqlConnection.Open();

				var command = sqlConnection.CreateCommand();
				command.CommandText = selectSql;
				var reader = command.ExecuteReader();

				var dataTable = new DataTable();
				if (!reader.HasRows)
				{
					_logger.Debug($"No record return from sql: {selectSql}");
					return dataTable;
				}

				dataTable.Load(reader);
				reader.Close();

				return dataTable;
			}
			catch (Exception ex)
			{
				_logger.Error($"Exception happened during executing select query: {selectSql}. Ex: {ex.Message}");
				return null;
			}
		}

		public static void ExecuteDeleteUpdateInsertQuery(string sql)
		{
			try
			{
				_logger.Debug($"Start execute sql: {sql}");
				using var sqlConnection = new SqlConnection(_connectionString);
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