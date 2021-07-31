using System;
using System.IO;
using System.Reflection;
using System.Xml;
using AccountingInitializer.Common;
using AccountingInitializer.Database;
using AccountingInitializer.SQL;
using AccountingInitializer.Template;
using NLog;

namespace AccountingInitializer
{
	public class AppInitializer : XmlReaderBase
	{
		private readonly Logger _logger = LogManager.GetCurrentClassLogger();
		public void Exxcute()
		{
			var path = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			Directory.SetCurrentDirectory(path);
			var appName = AppDomain.CurrentDomain.FriendlyName;
			var configPath = @$"{path}\{appName}.Config.xml";
			if (!File.Exists(configPath))
			{
				throw new ApplicationException($"Config file: {configPath} does not exist");
			}

			LoadConfigFromXmlFile(configPath);
		}

		private void LoadConfigFromXmlFile(string configPath)
		{
			try
			{
				var xmlDoc = new XmlDocument();
				xmlDoc.Load(configPath);
				ReadXml(xmlDoc.DocumentElement);
			}
			catch (Exception ex)
			{
				_logger.Error($"Exception happened during loading config from xml file: {configPath}. Ex: {ex.Message}");
			}
		}


		protected internal override void ReadXml(XmlNode configNode)
		{
			var expectedName = XmlNameTemplate.S_CONFIG;
			if (configNode.Name != expectedName)
			{
				throw new ApplicationException(GenerateMissingXmlLog(configNode, expectedName));
			}

			// Init DatabaseManager
			var node = configNode.SelectSingleNode(XmlNameTemplate.S_DATABASE_CONNECTION_LIST);
			if (node == null)
			{
				throw new ApplicationException(GenerateMissingXmlLog(configNode, XmlNameTemplate.S_DATABASE_CONNECTION_LIST));
			}
			DatabaseManager.Instance.ReadXml(node);

			// Init SQLTemplateList
			node = configNode.SelectSingleNode(XmlNameTemplate.S_SQL_TEMPLATE_LIST);
			if (node == null)
			{
				throw new ApplicationException(GenerateMissingXmlLog(configNode, XmlNameTemplate.S_SQL_TEMPLATE_LIST));
			}
			SQLManager.Instance.ReadXml(node);
		}
	}
}