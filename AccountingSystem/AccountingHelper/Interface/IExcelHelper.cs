using System.Collections.Generic;
using AccountingDatabase.Entity;
using NPOI.SS.UserModel;

namespace AccountingHelper.Interface
{
	public interface IExcelHelper
	{
		//IWorkbook OpenExcel(string filePath);

		//bool WriteExcel(IWorkbook workBook, string filePath);

		List<Transaction> ReadTransactions(string filePath);

		List<GL> ReadGls(string filePath);
	}
}