using System;
using System.Linq;
using System.Reflection;
using AccountingDatabase.Services.Interface;
using NLog;

namespace AccountingDatabase.Services
{
	public class ServiceFactory
	{
		private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
		public static IService<T> CreateService<T>()
		{
			var serviceName = $"{typeof(T).Name}Service";
			var assembly = Assembly.Load("AccountingDatabase");
			var type = assembly.DefinedTypes.FirstOrDefault(x => x.Name.Equals(serviceName, StringComparison.CurrentCultureIgnoreCase));

			if (type == null)
			{
				_logger.Error($"Could be get service from its name: {serviceName}");
				return null;
			}

			return Activator.CreateInstance(type) as IService<T>;
		}
	}
}