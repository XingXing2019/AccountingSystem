using System.Collections.Generic;
using AccountingDatabase.Entity;
using NPOI.SS.UserModel;

namespace AccountingHelper.Interface
{
	public interface IExcelHelper<T>
	{
		//IWorkbook OpenExcel(string filePath);

		//bool WriteExcel(IWorkbook workBook, string filePath);

		List<Transaction> ReadTransactions(string filePath);

		List<T> ReadExcel(string filePath);
	}
}