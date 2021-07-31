using System;
using System.Collections.Generic;
using System.Xml;
using AccountingInitializer.Common;
using AccountingInitializer.Template;

namespace AccountingInitializer.SQL
{
	public class SQLTemplate : XmlReaderBase
	{
		private readonly Dictionary<string, ISQLAction> _sqlActions;

		public string ID { get; set; }

		public SQLTemplate(XmlNode configNode)
		{
			_sqlActions = new Dictionary<string, ISQLAction>();
			ReadXml(configNode);
		}

		public bool TryGetSqlAction(string id, out ISQLAction sqlAction)
		{
			sqlAction = null;

			if (!_sqlActions.ContainsKey(id))
			{
				return false;
			}

			sqlAction = _sqlActions[id];
			return true;
		}

		#region Implementation of XmlReaderBase

		protected internal sealed override void ReadXml(XmlNode configNode)
		{
			string expectName = this.GetType().Name;
			if (configNode.Name != expectName)
			{
				throw new ApplicationException(GenerateMissingXmlLog(configNode, expectName));
			}

			string id = configNode.Attributes?[XmlNameTemplate.S_ID]?.Value;
			if (string.IsNullOrEmpty(id))
			{
				throw new ApplicationException(GenerateMissingXmlLog(configNode, XmlNameTemplate.S_ID));
			}
			ID = id;

			var sqlActions = configNode.SelectNodes(XmlNameTemplate.S_SQL_ACTION);

			if (sqlActions == null || sqlActions.Count == 0)
			{
				throw new ApplicationException(GenerateMissingXmlLog(configNode, XmlNameTemplate.S_SQL_ACTION));
			}

			foreach (XmlNode node in sqlActions)
			{
				var sqlAction = new SQLAction(node);
				if (!_sqlActions.ContainsKey(sqlAction.ID))
					_sqlActions.Add(sqlAction.ID, sqlAction);
			}
		}

		#endregion
	}
}