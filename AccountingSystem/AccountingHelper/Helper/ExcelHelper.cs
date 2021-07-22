using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using AccountingHelper.Interface;
using ExcelDataReader;
using NLog;

namespace AccountingHelper.Helper
{
	public class ExcelHelper<T> : IExcelHelper<T>
	{
		private readonly Logger _logger = LogManager.GetCurrentClassLogger();
		
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
			var propertyNames = propertyInfos.Select(x => x.Name).ToArray();
			var requiredProperties = GetRequiredPropertyNames(propertyInfos);
			
			foreach (var table in tables)
			{
				var findTitle = false;
				for (int i = 0; i < table.Rows.Count; i++)
				{
					var cells = table.Rows[i].ItemArray;
					if (!findTitle && IsTitleRow(cells, propertyNames))
					{
						for (int col = 0; col < cells.Length; col++)
						{
							var title = TrimTitle(cells[col].ToString());
							titleColumns.Add(col, title);
						}

						_logger.Debug($"Generate excel column title and column number mapping.");
						findTitle = true;
						continue;
					}

					if (!findTitle) continue;

					var hasAllRequiredProperties = true;

					// Set property value using reflection
					var instance = Activator.CreateInstance(type);
					for (int col = 0; col < cells.Length; col++)
					{
						var propertyName = titleColumns[col];
						var propertyInfo = propertyInfos.FirstOrDefault(x => string.Equals(x.Name, propertyName, StringComparison.CurrentCultureIgnoreCase));
						if (propertyInfo == null) continue;

						var propertyType = propertyInfo.PropertyType;

						if (requiredProperties.Contains(propertyName))
						{
							if (cells[col] is DBNull || propertyType != cells[col].GetType())
							{
								hasAllRequiredProperties = false;
								_logger.Debug($"Row: {i} does not contain all the required properties. Property type: {propertyType} does not match the type in column: {col} ");
								break;
							}
						}

						var value = Convert.ChangeType(cells[col], propertyType);
						propertyInfo.SetValue(instance, value);
					}

					if (hasAllRequiredProperties)
						results.Add((T) instance);
				}
			}

			return results;
		}
		
		#region Helper

		private string TrimTitle(string title)
		{
			return string.IsNullOrEmpty(title) ? string.Empty : string.Join("", title.Where(x => char.IsLetter(x) || char.IsDigit(x)));
		}

		private bool IsTitleRow(object[] cells, string[] propertyNames)
		{
			var cellValues = cells.Cast<string>().Select(x => TrimTitle(x).ToLower()).ToList();
			return propertyNames.All(x => cellValues.Contains(x.ToLower()));
		}

		private List<string> GetRequiredPropertyNames(PropertyInfo[] propertyInfos)
		{
			var requiredPropertyNames = new List<string>();
			foreach (var property in propertyInfos)
			{
				var attributes = property.GetCustomAttributes(false);
				if(attributes.Any(x => x is RequiredAttribute))
					requiredPropertyNames.Add(property.Name);
			}

			return requiredPropertyNames;
		}

		#endregion
	}
}