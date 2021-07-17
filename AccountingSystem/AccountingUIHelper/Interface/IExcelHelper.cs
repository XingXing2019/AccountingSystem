using NPOI.SS.UserModel;

namespace AccountingUIHelper.Interface
{
	public interface IExcelHelper
	{
		IWorkbook ReadExcel(string filePath);

		bool WriteExcel(IWorkbook workBook, string filePath);
	}
}