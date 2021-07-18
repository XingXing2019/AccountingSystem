using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using AccountingDatabase.Entity;
using AccountingHelper.Helper;
using AccountingHelper.Interface;
using NLog;
using NLog.Fluent;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;

namespace AccountingUI
{
	public partial class Form1 : Form
	{
		private readonly Logger _logger = LogManager.GetCurrentClassLogger();
		private readonly IExcelHelper _excelHelper = new ExcelHelper();
		private string path;

		
		public Form1()
		{
			InitializeComponent();
		}

		private void btnLoadExcel_Click(object sender, EventArgs e)
		{
			var file = new OpenFileDialog();
			file.ShowDialog();
			this.txtExcelFile.Text = file.SafeFileName;
			this.path = file.FileName;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			GetTransactions(@"C:\Users\61425\Desktop\Data - Copy.xls");
		}

		private List<Transaction> GetTransactions(string filePath)
		{
			var transactions = new List<Transaction>();
			var workbook = _excelHelper.ReadExcel(filePath);
			if (workbook != null)
			{
				using var sheetReader = workbook.GetEnumerator();
				while (sheetReader.MoveNext())
				{
					var sheet = sheetReader.Current;
					if (sheet != null)
					{
						var reader = sheet.GetEnumerator();
						var glCode = string.Empty;
						var glDescription = string.Empty;
						var year = string.Empty;
						while (reader.MoveNext())
						{
							var row = reader.Current as HSSFRow;
							if (row != null)
							{
								if(row.Cells.Count == 0 || row.Cells[0].Address.Column != 0 || row.Cells[0].Address.Column == 0 && row.Cells[0].CellType == CellType.Blank)
									continue;
								if (row.Cells.Count > 0 && row.Cells[0].CellType == CellType.String)
								{
									if (row.Cells[0].Address.Column == 0 && IsGLCode(row.Cells[0].StringCellValue))
									{
										glCode = row.Cells[0].StringCellValue;
										glDescription = row.Cells[3].StringCellValue;
										continue;
									}

									if (row.Cells[0].Address.Column == 0 && row.Cells[0].StringCellValue.Length == 4 && row.Cells[0].StringCellValue.StartsWith("20"))
									{
										year = row.Cells[0].StringCellValue;
										continue;
									}
								}

								var trans = new Transaction{GLCode = glCode, GLDescription = glDescription};
								foreach (var cell in row.Cells)
								{
									if(cell.CellType == CellType.Blank)
										continue;
									switch (cell.Address.Column)
									{
										case 0:
											trans.TransactionDate = new DateTime(int.Parse(year), int.Parse(cell.StringCellValue), 1);
											break;
										case 1:
											trans.Source = new Source {Code = cell.StringCellValue};
											break;
										case 2:
											trans.DocDate = cell.DateCellValue;
											break;
										case 3:
											trans.Description = cell.StringCellValue;
											break;
										case 4:
											if(cell.CellType == CellType.String && !cell.StringCellValue.StartsWith("Net Change and Ending Balance for Fiscal Period"))
												trans.Description += $"*{cell.StringCellValue}";
											break;
										case 5:
											trans.PostingSeq = cell.NumericCellValue.ToString();
											break;
										case 6:
											trans.BatchEntry = cell.StringCellValue;
											break;
										case 7:
											trans.Debit = (decimal) cell.NumericCellValue;
											break;
										case 8:
											trans.Credit = (decimal) cell.NumericCellValue;
											break;
									}
								}
								transactions.Add(trans);
							}
						}
					}
				}
			}

			return transactions;
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
	}
}
