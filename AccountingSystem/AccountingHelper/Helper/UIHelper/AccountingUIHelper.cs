using AccountingDatabase.Entity;
using AccountingDatabase.Services;
using AccountingHelper.Helper.DataAnalysisHelper;
using AccountingHelper.Helper.ExcelHelper;
using AccountingHelper.Helper.ModelHelper;
using AccountingHelper.Model;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

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

		public List<string> LoadModelNames()
		{
			try
			{
				var assemblyName = "AccountingDatabase";
				var ns = "AccountingDatabase.Entity";
				var assembly = Assembly.Load(assemblyName);
				return assembly.GetTypes().Where(x => x.IsClass && x.Namespace == ns).Select(x => x.Name).ToList();
			}
			catch (Exception ex)
			{
				_logger.Error($"Exception happened during load model names using reflection. Ex: {ex.Message}");
				return null;
			}
		}

		public void UploadExcelDataToDatabase(string modelName, string filePath)
		{
			switch (modelName)
			{
				case "Vendor":
					Upload(new VendorModelHelper(), filePath);
					return;
				case "GLAccount":
					Upload(new GlAccountModelHelper(), filePath);
					return;
				case "Transaction":
					Upload(new TransactionModelHelper(), filePath);
					return;
				default:
					return;
			}
		}

		#region Helper

		private void Upload<S, T>(IModelHelper<S, T> modelHelper, string filePath)
		{
			try
			{
				var models = ExcelReader<S>.ReadExcel(filePath);
				var entities = modelHelper.TransformValidModels(models);

				var service = ServiceFactory.CreateService<T>();
				if (service == null)
				{
					_logger.Error($"Could be create service for {nameof(T)}");
					return;
				}

				service.PostAll(entities);
			}
			catch (Exception ex)
			{
				_logger.Error($"Exception happened during uplaod excel data to database. Ex: {ex.Message}");
			}
		}

		#endregion
	}
}