using System;
using System.IO;
using System.Windows.Forms;
using AccountingHelper.Helper;
using AccountingHelper.Interface;
using NLog;
using NLog.Fluent;
using NPOI.SS.UserModel;
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
			_logger.Error("Error");
			var file = new OpenFileDialog();
			file.ShowDialog();
			this.txtExcelFile.Text = file.SafeFileName;
			this.path = file.FileName;
		}

	}
}
