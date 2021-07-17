using System;
using System.IO;
using AccountingUIHelper.Interface;
using NLog;
using NPOI.SS.UserModel;

namespace AccountingUIHelper.Helper
{
	public class ExcelHelper : IExcelHelper
	{
		private readonly Logger _logger = LogManager.GetCurrentClassLogger();
		public IWorkbook ReadExcel(string filePath)
		{
			try
			{
				return WorkbookFactory.Create(filePath);
			}
			catch (Exception ex)
			{
				_logger.Error($"Failed to read excel file with path: {filePath}. Ex: {ex.Message}");
			}

			return null;
		}

		public bool WriteExcel(IWorkbook workbook, string filePath)
		{
			var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite);
			try
			{
				workbook.Write(fileStream);
				return true;
			}
			catch (Exception ex)
			{
				_logger.Error($"Failed to write excel file with path: {fileStream}. Ex: {ex.Message}");
			}
			finally
			{
				fileStream.Close();
			}

			return false;
		}
	}
}