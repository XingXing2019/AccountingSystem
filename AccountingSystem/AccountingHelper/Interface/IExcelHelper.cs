using NPOI.SS.UserModel;

namespace AccountingHelper.Interface
{
	public interface IExcelHelper
	{
		IWorkbook ReadExcel(string filePath);

		bool WriteExcel(IWorkbook workBook, string filePath);
	}
}