using System.Xml;
using AccountingInitializer.Common;

namespace AccountingInitializer.Database
{
	public class DatabaseManager : XmlReaderBase
	{
		private static readonly object _locker = new object();
		private readonly DatabaseConnectionList _databaseConnectionList;

		private static DatabaseManager _instance;
		public static DatabaseManager Instance
		{
			get
			{
				lock (_locker)
				{
					if (_instance == null)
					{
						_instance = new DatabaseManager();
					}

					return _instance;
				}
			}
		}

		private DatabaseManager()
		{
			_databaseConnectionList = new DatabaseConnectionList();
		}

		public static void Cleanup()
		{
			lock (_locker)
			{
				_instance = null;
			}
		}

		public string GetConnectionString(string id)
		{
			return _databaseConnectionList.GetDatabaseConnection(id)?.ConnectionString;
		}


		#region Implementation of XmlReaderBase

		protected internal override void ReadXml(XmlNode configNode)
		{
			_databaseConnectionList.ReadXml(configNode);
		}

		#endregion
	}
}