using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using NLog;

namespace AccountingHelper.Helper.ExcelHelper
{
	public class ExcelWriter
	{
		private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
		public static void WriteExcel(string filePath, DataTable data)
		{
			try
			{
				var lines = new List<string>();
				string[] columnNames = data.Columns
					.Cast<DataColumn>()
					.Select(column => column.ColumnName)
					.ToArray();

				var header = string.Join(",", columnNames.Select(name => $"\"{name}\""));
				lines.Add(header);

				var valueLines = data.AsEnumerable().Select(row => string.Join(",", row.ItemArray.Select(val => $"\"{val}\"")));

				lines.AddRange(valueLines);

				File.WriteAllLines(filePath, lines);
			}
			catch (Exception ex)
			{
				_logger.Error($"Exception happened during write excel to {filePath}. Ex: {ex.Message}");
			}
		}
	}
}