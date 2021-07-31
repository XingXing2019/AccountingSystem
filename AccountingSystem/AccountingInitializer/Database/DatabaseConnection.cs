using System;
using System.Xml;
using AccountingInitializer.Common;
using AccountingInitializer.Template;

namespace AccountingInitializer.Database
{
	public class DatabaseConnection : XmlReaderBase
	{
		public string ID { get; private set; }
		public string DatabaseName { get; private set; }
		public string ConnectionString { get; private set; }

		public DatabaseConnection(XmlNode configNode)
		{
			ReadXml(configNode);
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

			string databaseName = configNode.Attributes?[XmlNameTemplate.S_DATABASE_NAME]?.Value;
			if (string.IsNullOrEmpty(databaseName))
			{
				throw new ApplicationException(GenerateMissingXmlLog(configNode, XmlNameTemplate.S_DATABASE_NAME));
			}
			DatabaseName = databaseName;

			var connectionStrings = configNode.SelectNodes(XmlNameTemplate.S_CONNECTION_STRING);
			if (connectionStrings == null || connectionStrings.Count == 0)
			{
				throw new ApplicationException(GenerateMissingXmlLog(configNode, XmlNameTemplate.S_CONNECTION_STRING));
			}

			if (connectionStrings.Count > 1)
			{
				throw new ApplicationException($"Invalid DatabaseConnection with multiple connection strings");
			}
			ConnectionString = connectionStrings[0].InnerText;
		}

		#endregion
	}
}