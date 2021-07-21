using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using AccountingDatabase.Entity;
using AccountingHelper.Interface;
using ExcelDataReader;
using NLog;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace AccountingHelper.Helper
{
	public class ExcelHelper<T> : IExcelHelper<T>
	{
		private readonly Logger _logger = LogManager.GetCurrentClassLogger();

		public List<Transaction> ReadTransactions(string filePath)
		{
			var transactions = new List<Transaction>();
			var workbook = OpenExcel(filePath);
			if (workbook == null)
			{
				_logger.Error($"Failed to read excel file with path: {filePath}.");
				return null;
			}

			const string tobeIgnored = "Net Change and Ending Balance for Fiscal Period";

			using var sheetReader = workbook.GetEnumerator();
			while (sheetReader.MoveNext())
			{
				var sheet = sheetReader.Current;
				if (sheet == null) continue;

				var reader = sheet.GetEnumerator();
				var glCode = string.Empty;
				//var glDescription = string.Empty;
				var year = string.Empty;
				while (reader.MoveNext())
				{
					var row = reader.Current as HSSFRow;
					if (row == null) 
						continue;
					
					if (row.Cells.Count == 0 || row.Cells[0].Address.Column != 0 || row.Cells[0].Address.Column == 0 && row.Cells[0].CellType == CellType.Blank)
						continue;
					if (row.Cells.Count > 0 && row.Cells[0].CellType == CellType.String)
					{
						if (row.Cells[0].Address.Column == 0 && IsGLCode(row.Cells[0].StringCellValue))
						{
							glCode = row.Cells[0].StringCellValue;
							//glDescription = row.Cells[3].StringCellValue;
							continue;
						}

						if (row.Cells[0].Address.Column == 0 && row.Cells[0].StringCellValue.Length == 4 && row.Cells[0].StringCellValue.StartsWith("20"))
						{
							year = row.Cells[0].StringCellValue;
							continue;
						}
					}

					var trans = new Transaction { GLCode = glCode };
					foreach (var cell in row.Cells)
					{
						if (cell.CellType == CellType.Blank)
							continue;

						if (cell.Address.Column == 0)
						{
							var month = int.Parse(cell.StringCellValue);
							var date = new DateTime(int.Parse(year) - 1, 6, 1);
							trans.TransactionDate = date.AddMonths(month);
						}
						else if (cell.Address.Column == 1)
						{
							trans.SourceCode = cell.StringCellValue;
						}
						else if (cell.Address.Column == 2)
						{
							trans.DocDate = cell.DateCellValue;
						}
						else if (cell.Address.Column == 3)
						{
							if (trans.SourceCode.StartsWith("AP") && !trans.SourceCode.EndsWith("AD"))
								trans.VendorCode = cell.StringCellValue.Split("*")[1];
							else
								trans.Description = cell.StringCellValue;
						}
						else if (cell.Address.Column == 4)
						{
							if (cell.CellType == CellType.String && !cell.StringCellValue.StartsWith(tobeIgnored))
								trans.Description = $"*{cell.StringCellValue}";
							if (trans.SourceCode.StartsWith("AP"))
							{
								trans.InvoiceNo = cell.StringCellValue.Split("*")[0];
							}
						}
						else if (cell.Address.Column == 5 && cell.CellType == CellType.Numeric)
						{
							trans.PostingSeq = cell.NumericCellValue.ToString();
						}
						else if (cell.Address.Column == 6 && cell.CellType == CellType.String)
						{
							trans.BatchEntry = cell.StringCellValue;
						}
						else if (cell.Address.Column == 7 && cell.CellType == CellType.Numeric)
						{
							trans.Debit = (decimal) cell.NumericCellValue;
						}
						else if (cell.Address.Column == 8 && cell.CellType == CellType.Numeric)
						{
							trans.Credit = (decimal) cell.NumericCellValue;
						}
					}
					transactions.Add(trans);
				}
			}

			return transactions;
		}
		
		public List<T> ReadExcel(string filePath)
		{
			System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
			using var stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
			var reader = ExcelReaderFactory.CreateReader(stream);
			var tables = reader.AsDataSet().Tables.Cast<DataTable>();

			var results = new List<T>();
			var titleColumns = new Dictionary<int, string>();
			var type = typeof(T);
			var propertyInfos = type.GetProperties();
			foreach (var table in tables)
			{
				for (int i = 0; i < table.Rows.Count; i++)
				{
					var cells = table.Rows[i].ItemArray;
					if (i == 0)
					{
						for (int col = 0; col < cells.Length; col++)
						{
							var title = cells[col].ToString()?.Replace(" ", "").Replace(".", "");
							titleColumns.Add(col, title);
						}

						_logger.Debug($"Generate excel column title and column number mapping.");
						continue;
					}

					// Set property value using reflection
					var instance = Activator.CreateInstance(type);
					for (int col = 0; col < cells.Length; col++)
					{
						var propertyName = titleColumns[col];
						var propertyInfo = propertyInfos.FirstOrDefault(x => string.Equals(x.Name, propertyName, StringComparison.CurrentCultureIgnoreCase));
						if (propertyInfo == null) 
							continue;
						
						var propertyType = propertyInfo.PropertyType;
						var value = Convert.ChangeType(cells[col], propertyType);
						propertyInfo.SetValue(instance, value);
					}
					
					results.Add((T)instance);
				}
			}

			return results;
		}

		private bool IsGLCode(string code)
		{
			var parts = code.Split("-");
			if (parts.Length == 1 && parts[0].Length == 6 && parts[0].All(x => char.IsDigit(x)))
				return true;
			if (parts.Length == 3)
			{
				if (parts[0].Length != 6 || parts[0].Any(x => !char.IsDigit(x)))
					return false;
				if (parts[1].Length != 4 || parts[1].Any(x => !char.IsLetter(x)))
					return false;
				if (parts[2].Length != 4 || parts[2].Any(x => !char.IsLetter(x)))
					return false;
				return true;
			}
			return false;
		}

		private IWorkbook OpenExcel(string filePath)
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
	}
}