using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Xml;
using AccountingInitializer.Common;
using AccountingInitializer.Database;
using AccountingInitializer.Template;
using Microsoft.Data.SqlClient;
using NLog;

namespace AccountingInitializer.SQL
{
	public class SQLAction : XmlReaderBase, ISQLAction, ICloneable
	{
		private readonly Logger _logger = LogManager.GetCurrentClassLogger();
		private class SQLField
		{
			public Type Type { get; private set; }
			public string Name { get; private set; }
			public Object Value { get; set; }

			public SQLField(Type type, string name, Object value = null)
			{
				Type = type;
				Name = name;
				Value = value;
			}
		}

		public string ID { get; private set; }


		private string _databaseID;
		private string _sql;
		private readonly Dictionary<string, SQLField> _variables;
		private Dictionary<string, List<SQLField>> _sqlResults;

		public SQLAction()
		{
			_variables = new Dictionary<string, SQLField>();
		}

		public SQLAction(XmlNode configNode)
		{
			_variables = new Dictionary<string, SQLField>();
			ReadXml(configNode);
		}

		public void SetSQLVariables(Dictionary<string, object> data)
		{
			if (_variables.Any(x => !data.ContainsKey(x.Key)) || data.Any(x => !_variables.ContainsKey(x.Key)))
			{
				throw new ApplicationException($"The provided data does not fulfill the requirement of _variables");
			}

			foreach (var variableName in data.Keys)
			{
				_variables[variableName].Value = data[variableName];
			}
		}

		public List<Object> GetSqlResult(string fieldName)
		{
			return !_sqlResults.ContainsKey(fieldName) ? null : _sqlResults[fieldName].Select(x => x.Value).ToList();
		}


		#region Implementation of XmlReaderBase

		protected internal sealed override void ReadXml(XmlNode configNode)
		{
			string expectedName = this.GetType().Name;
			if (configNode.Name != expectedName)
			{
				throw new ApplicationException(GenerateMissingXmlLog(configNode, expectedName));
			}

			string id = configNode.Attributes?[XmlNameTemplate.S_ID]?.Value;
			if (string.IsNullOrEmpty(id))
			{
				throw new ApplicationException(GenerateMissingXmlLog(configNode, XmlNameTemplate.S_ID));
			}
			ID = id;

			string databaseID = configNode.Attributes?[XmlNameTemplate.S_DATABASE_ID].Value;
			if (string.IsNullOrEmpty(databaseID))
			{
				throw new ApplicationException(GenerateMissingXmlLog(configNode, XmlNameTemplate.S_DATABASE_ID));
			}
			_databaseID = databaseID;

			string sql = configNode.SelectSingleNode(XmlNameTemplate.S_SQL)?.InnerText;
			if (string.IsNullOrEmpty(sql))
			{
				throw new ApplicationException(GenerateMissingXmlLog(configNode, XmlNameTemplate.S_SQL));
			}
			_sql = sql;

			var variables = configNode.SelectNodes(XmlNameTemplate.S_SQL_VAR);
			if (variables == null || variables.Count == 0)
				return;

			foreach (XmlNode node in variables)
			{
				string typeStr = node.Attributes?[XmlNameTemplate.S_TYPE]?.Value;
				if (string.IsNullOrEmpty(typeStr))
				{
					throw new ApplicationException(GenerateMissingXmlLog(configNode, XmlNameTemplate.S_TYPE));
				}
				var type = Type.GetType($"System.{typeStr}");

				string name = node.Attributes?[XmlNameTemplate.S_NAME]?.Value;
				if (string.IsNullOrEmpty(name))
				{
					throw new ApplicationException(GenerateMissingXmlLog(configNode, XmlNameTemplate.S_NAME));
				}

				_variables.Add(name, new SQLField(type, name));
			}
		}

		#endregion


		#region Implementation of IAction

		/// <summary>
		/// Execute the sql and fill the result into variables dictionary
		/// </summary>
		public void ExecuteSQL()
		{
			try
			{
				var watch = Stopwatch.StartNew();
				var connectionString = DatabaseManager.Instance.GetConnectionString(this._databaseID);
				using (var sqlConnection = new SqlConnection(connectionString))
				{
					if (sqlConnection.State != ConnectionState.Open)
						sqlConnection.Open();

					var commend = sqlConnection.CreateCommand();
					var sql = GetExecutableSQL();
					commend.CommandText = sql;
					var reader = commend.ExecuteReader();

					watch.Stop();
					if (watch.ElapsedMilliseconds > 1000)
						_logger.Warn($"SQL Executed for {watch.ElapsedMilliseconds} ms");
					else
						_logger.Debug($"SQL Executed for {watch.ElapsedMilliseconds} ms");

					if (!reader.HasRows)
					{
						_logger.Error($"No result return from sql: {sql}");
						return;
					}

					FillSQLResults(reader);
				}
			}
			catch (Exception ex)
			{
				_logger.Error($"Exception happened during executing sql: {this._sql}. Ex: {ex.Message}");
			}
		}

		#endregion


		#region IMplementation of ICloneable

		/// <summary>
		/// Clone an new instance of this sql action to avoid direct manipulation on this instance. 
		/// </summary>
		/// <returns></returns>
		public object Clone()
		{
			var type = this.GetType();
			var newSqlAction = (SQLAction)Activator.CreateInstance(type);
			newSqlAction.ID = this.ID;
			newSqlAction._sql = this._sql;
			newSqlAction._databaseID = this._databaseID;

			foreach (var variable in this._variables)
			{
				var sqlField = variable.Value;
				newSqlAction._variables.Add(variable.Key, new SQLField(sqlField.Type, sqlField.Name));
			}

			return newSqlAction;
		}

		#endregion


		#region Helper

		/// <summary>
		/// Replace the place holder in the sql with variables
		/// </summary>
		/// <returns></returns>
		private string GetExecutableSQL()
		{
			var sql = this._sql;
			foreach (var variable in _variables)
			{
				sql = sql.Replace($"%%%{variable.Value.Name}%%%", $"'{variable.Value.Value}'");
			}

			return sql;
		}

		/// <summary>
		/// Fill sql results into _sqlResults
		/// </summary>
		/// <param name="reader"></param>
		private void FillSQLResults(SqlDataReader reader)
		{
			_sqlResults = new Dictionary<string, List<SQLField>>();
			while (reader.Read())
			{
				for (int i = 0; i < reader.FieldCount; i++)
				{
					var name = reader.GetName(i);
					var value = reader.GetValue(i);
					var type = reader.GetFieldType(i);

					if (!_sqlResults.ContainsKey(name))
						_sqlResults[name] = new List<SQLField>();
					_sqlResults[name].Add(new SQLField(type, name, value));
				}
			}
		}

		#endregion
	}
}