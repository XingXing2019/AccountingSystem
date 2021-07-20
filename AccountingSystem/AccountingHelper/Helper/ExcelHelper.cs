﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AccountingDatabase.Entity;
using AccountingHelper.Interface;
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
			var results = new List<T>();
			var workbook = OpenExcel(filePath);

			if (workbook == null)
			{
				_logger.Error($"Failed to read excel file with path: {filePath}.");
				return null;
			}

			var titleColumns = new Dictionary<int, string>();
			var type = typeof(T);
			var propertyInfos = type.GetProperties();

			using var sheetReader = workbook.GetEnumerator();
			while (sheetReader.MoveNext())
			{
				var sheet = sheetReader.Current;
				if (sheet == null) continue;

				var reader = sheet.GetEnumerator();
				while (reader.MoveNext())
				{
					var row = reader.Current as HSSFRow;
					if (row == null) continue;

					// Read excel column title and column number from the first row
					if (row.RowNum == 0)
					{
						foreach (var cell in row.Cells)
						{
							var col = cell.Address.Column;
							var title = cell.StringCellValue.Replace(" ", "").Replace(".", "");
							titleColumns.Add(col, title);
						}
						_logger.Debug($"Generate excel column title and column number mapping.");
						continue;
					}
					
					var unblankCells = row.Cells.Count(x => x.CellType != CellType.Blank);
					if (unblankCells != titleColumns.Count)
					{
						_logger.Debug($"The number of unblank cells: {unblankCells} does not match the required number: {titleColumns.Count}");
						continue;
					}
					
					// Set property value using reflection
					var instance = Activator.CreateInstance(type);
					foreach (var cell in row.Cells)
					{
						if (cell.CellType == CellType.Blank) continue;

						var propertyName = titleColumns[cell.Address.Column];
						var propertyInfo = propertyInfos.FirstOrDefault(x => x.Name == propertyName);
						if (propertyInfo == null) continue;

						object value = null;
						if (cell.CellType == CellType.String)
							value = cell.StringCellValue;
						else if (cell.CellType == CellType.Boolean)
							value = cell.BooleanCellValue;
						else if (cell.CellType == CellType.Numeric)
							value = cell.NumericCellValue;
						
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