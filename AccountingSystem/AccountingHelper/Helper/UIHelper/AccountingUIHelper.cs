using AccountingHelper.Helper.DataAnalysisHelper;
using AccountingHelper.Helper.ExcelHelper;
using AccountingHelper.Helper.ModelHelper;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using AccountingDatabase.Services;
using AccountingDatabase.Services.Interface;

namespace AccountingHelper.Helper.UIHelper
{
	public class AccountingUIHelper
	{
		private readonly Logger _logger = LogManager.GetCurrentClassLogger();

		private readonly TransactionAnalysisHelper _transactionAnalyzer;
		private readonly VendorAnalysisHelper _vendorAnalyzer;

		public AccountingUIHelper()
		{
			_transactionAnalyzer = new TransactionAnalysisHelper();
			_vendorAnalyzer = new VendorAnalysisHelper();
		}

		public List<string> LoadGroupIDs()
		{
			var data = _vendorAnalyzer.AnalyzeVendorGroups();
			var groupIds = new List<string>();
			foreach (DataRow row in data.Rows)
			{
				groupIds.Add(row.ItemArray[0].ToString());
			}

			return groupIds;
		}

		public void UploadExcelDataToDB<S, T>(IModelHelper<S, T> modelHelper, string filePath)
		{
			try
			{
				var models = ExcelReader<S>.ReadExcel(filePath);
				var entities = modelHelper.TransformValidModels(models);
				var serviceName = $"{typeof(T).Name}Service";

				var assembly = Assembly.Load("AccountingDatabase");
				var type = assembly.DefinedTypes.FirstOrDefault(x => x.Name == serviceName);
				
				if (type == null)
				{
					_logger.Error($"Could be get service from its name: {serviceName}");
					return;
				}

				if (Activator.CreateInstance(type) is IService<T> service) 
					service.PostAll(entities);
			}
			catch (Exception ex)
			{
				_logger.Error($"Exception happened during uplaod excel data to database. Ex: {ex.Message}");
			}
		}
	}
}