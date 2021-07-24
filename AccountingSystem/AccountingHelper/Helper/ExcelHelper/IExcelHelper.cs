using System.Collections.Generic;

namespace AccountingHelper.Helper.ExcelHelper
{
	public interface IExcelHelper<T>
	{
		List<T> ReadExcel(string filePath);
	}
}