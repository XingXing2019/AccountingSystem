using AccountingHelper.Helper.DataAnalysisHelper;
using AccountingHelper.Helper.ExcelHelper;
using AccountingHelper.Helper.ModelHelper;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using AccountingDatabase.Entity;
using AccountingDatabase.Services;
using AccountingDatabase.Services.Interface;
using AccountingHelper.Model;
using Microsoft.EntityFrameworkCore.Storage;

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

		public void UploadExcelDataToDatabase(string modelName, string filePath)
		{
			switch (modelName)
			{
				case "Vendor":
					Upload(ModelHelperFactory.CreateModelHelper<VendorModel, Vendor>(), filePath);
					return;
				case "GLAccount":
					Upload(ModelHelperFactory.CreateModelHelper<GLAccountModel, GLAccount>(), filePath);
					return;
				case "Transaction":
					Upload(ModelHelperFactory.CreateModelHelper<TransactionModel, Transaction>(), filePath);
					return;
				default:
					return;
			}
		}
	}
}