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

		private readonly DataAnalyzer _dataAnalyzer;

		public AccountingUIHelper()
		{
			_dataAnalyzer = new DataAnalyzer();
		}

		public List<string> LoadGroupIDs(string templateId, string sqlId)
		{

			var data = _dataAnalyzer.ExecuteSQLAction(templateId, sqlId);
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

		public bool UploadExcelDataToDatabase(string modelName, string filePath)
		{
			switch (modelName)
			{
				case "Vendor":
					return Upload(new VendorModelHelper(), filePath);
				case "GLAccount":
					return Upload(new GlAccountModelHelper(), filePath);
				case "Transaction":
					return Upload(new TransactionModelHelper(), filePath);
				default:
					return false;
			}
		}

		public bool DownloadDatabaseDataToExcel(DataTable data, string filePath)
		{
			return ExcelWriter.WriteExcel(filePath, data);
		}

		#region Helper

		private bool Upload<S, T>(IModelHelper<S, T> modelHelper, string filePath)
		{
			try
			{
				var isSucceeded = ExcelReader<S>.ReadExcel(filePath, out var models);
				if (!isSucceeded)
				{
					_logger.Error($"Faild to read excel: {filePath}");
					return false;
				}

				isSucceeded = modelHelper.TransformValidModels(models, out var entities);
				if (!isSucceeded)
				{
					_logger.Error($"Faild to transform model to entity");
					return false;
				}

				var service = ServiceFactory.CreateService<T>();
				if (service == null)
				{
					_logger.Error($"Could be create service for {nameof(T)}");
					return false;
				}

				return service.PostAll(entities);
			}
			catch (Exception ex)
			{
				_logger.Error($"Exception happened during uplaod excel data to database. Ex: {ex.Message}");
				return false;
			}
		}

		#endregion
	}
}