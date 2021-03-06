using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using AccountingInitializer.Common;
using AccountingInitializer.Template;
using NLog;

namespace AccountingInitializer.SQL
{
	public class SQLManager : XmlReaderBase
	{
		private readonly Logger _logger = LogManager.GetCurrentClassLogger();
		private static readonly object _locker = new object();
		private readonly Dictionary<string, SQLTemplate> _sqlTemplateList;

		private static SQLManager _instance;
		public static SQLManager Instance
		{
			get
			{
				lock (_locker)
				{
					if (_instance == null)
					{
						_instance = new SQLManager();
					}

					return _instance;
				}
			}
		}

		public SQLManager()
		{
			_sqlTemplateList = new Dictionary<string, SQLTemplate>();
		}

		public static void Cleanup()
		{
			lock (_locker)
			{
				_instance = null;
			}
		}

		public bool TryGetSqlAction(string templateId, string sqlId, out ISQLAction sqlAction)
		{
			_logger.Info($"Getting SQLAction with SQLTemplate ID: {templateId}, SQL ID: {sqlId}");

			sqlAction = GetSqlAction(templateId, sqlId);

			if (sqlAction == null)
			{
				return false;
			}

			_logger.Info($"Return a copy of SQLAction with SQLTemplate ID: {templateId}, SQL ID: {sqlId}");
			sqlAction = sqlAction.Clone() as ISQLAction;

			return true;
		}

		public bool TrySetSqlActionVariables(string templateId, string sqlId, Dictionary<string, Object> data)
		{
			try
			{
				var sqlAction = GetSqlAction(templateId, sqlId);
				if (sqlAction == null)
				{
					return false;
				}

				_logger.Info($"Setting variables values for SQLAction with SQLTemplate ID: {templateId}, SQL ID: {sqlId}");
				sqlAction.SetSQLVariables(data);
				return true;
			}
			catch (Exception ex)
			{
				_logger.Error($"Exception happened during set variables for sql action with template id: {templateId}, sql id: {sqlId}. Ex: {ex.Message}");
				return false;
			}
		}

		#region Implementation of XmlReaderBase

		protected internal override void ReadXml(XmlNode configNode)
		{
			string expectName = XmlNameTemplate.S_SQL_TEMPLATE_LIST;
			if (configNode.Name != expectName)
			{
				throw new ApplicationException(GenerateMissingXmlLog(configNode, expectName));
			}

			var templates = configNode.SelectNodes(XmlNameTemplate.S_SQL_TEMPLATE);
			if (templates == null || templates.Count == 0)
			{
				throw new ApplicationException(GenerateMissingXmlLog(configNode, XmlNameTemplate.S_SQL_TEMPLATE));
			}

			foreach (XmlNode node in templates)
			{
				string path = node.InnerText;
				if (!File.Exists(path))
				{
					_logger.Error($"File: {path} does not exist");
					continue;
				}

				var xmlDoc = new XmlDocument();
				xmlDoc.Load(path);
				var templateNode = xmlDoc.DocumentElement;
				var template = new SQLTemplate(templateNode);
				_sqlTemplateList.Add(template.ID, template);
			}
		}

		#endregion


		#region Helper

		private ISQLAction GetSqlAction(string templateId, string sqlId)
		{
			_logger.Info($"Getting SQLAction with SQLTemplate ID: {templateId}, SQL ID: {sqlId}");
			
			if (!_sqlTemplateList.ContainsKey(templateId))
			{
				_logger.Error($"Unable to get sql template with SQLTemplate ID: {templateId}");
				return null;
			}

			var sqlTemplate = _sqlTemplateList[templateId];
			if (!sqlTemplate.TryGetSqlAction(sqlId, out var sqlAction))
			{
				_logger.Error($"Unable to get sql action with SQL ID: {sqlId}");
				return null;
			}

			return sqlAction;
		}

		#endregion
	}
}