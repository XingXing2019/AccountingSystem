using AccountingInitializer.Template;
using System.Xml;

namespace AccountingInitializer.Common
{
	public abstract class XmlReaderBase
	{
		protected internal abstract void ReadXml(XmlNode configNode);

		protected string GenerateMissingXmlLog(XmlNode configNode, string element)
		{
			return string.Format(LoggerTemplate.S_MISSING_XML_ELEMENT, $"{GetAncestorNodesName(configNode)}->{element}");
		}
		

		#region Helper

		/// <summary>
		/// Recursively get name of ancestor nodes
		/// </summary>
		/// <param name="configNode"></param>
		/// <returns></returns>
		private string GetAncestorNodesName(XmlNode configNode)
		{
			if (configNode?.ParentNode == null)
				return string.Empty;
			string parentNodeName = GetAncestorNodesName(configNode.ParentNode);
			return string.IsNullOrEmpty(parentNodeName) ? configNode.Name : $"{parentNodeName}->{configNode.Name}";
		}

		#endregion
	}
}
