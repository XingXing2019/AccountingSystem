using System;
using System.Collections.Generic;
using System.Xml;
using AccountingInitializer.Common;
using AccountingInitializer.Template;

namespace AccountingInitializer.Database
{
	public class DatabaseConnectionList : XmlReaderBase
	{
		private Dictionary<string, DatabaseConnection> _connections;
		public DatabaseConnectionList()
		{
			_connections = new Dictionary<string, DatabaseConnection>();
		}

		public DatabaseConnection GetDatabaseConnection(string id)
		{
			return _connections.ContainsKey(id) ? _connections[id] : null;
		}


		#region Implementation of XmlReaderBase

		protected internal override void ReadXml(XmlNode configNode)
		{
			string expectName = this.GetType().Name;
			if (configNode.Name != expectName)
			{
				throw new ApplicationException(GenerateMissingXmlLog(configNode, expectName));
			}

			var databaseConnections = configNode.SelectNodes(XmlNameTemplate.S_DATABASE_CONNECTION);
			if (databaseConnections == null || databaseConnections.Count == 0)
			{
				throw new ApplicationException(GenerateMissingXmlLog(configNode, XmlNameTemplate.S_DATABASE_CONNECTION));
			}

			foreach (XmlNode node in databaseConnections)
			{
				var connection = new DatabaseConnection(node);
				if (!_connections.ContainsKey(connection.ID))
					_connections.Add(connection.ID, connection);
			}
		}

		#endregion
	}
}