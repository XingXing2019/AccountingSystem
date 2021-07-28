using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using AccountingHelper.Helper.DataAnalysisHelper;
using AccountingHelper.Helper.ExcelHelper;
using AccountingHelper.Helper.UIHelper;
using NLog;

namespace AccountingUI
{
	public partial class MainForm : Form
	{
		private readonly Logger _logger = LogManager.GetCurrentClassLogger();

		private readonly AccountingUIHelper _accountingUIHelper = new AccountingUIHelper();
		private string path;
		
		public MainForm()
		{
			InitializeComponent();
			cmbGroupId.DataSource = _accountingUIHelper.LoadGroupIDs();
			cmbModelName.DataSource = _accountingUIHelper.LoadModelNames();
		}

		private void btnLoadExcel_Click(object sender, EventArgs e)
		{
			var path = this.path;
			var modelName = this.cmbModelName.Text;
			_accountingUIHelper.UploadExcelDataToDatabase(modelName, path);
		}

		private void button1_Click(object sender, EventArgs e)
		{
			var selectItems = new List<string>
			{
				"VendorCode",
				"VendorName",
				"GroupID",
				"COUNT(DISTINCT InvoiceNo) AS Invoices",
				"SUBSTRING(CONVERT(VARCHAR(50), YearPeriod), 1, 7) AS YearPeriod"
			};
			var criterion = new List<string> { "VendorCode IS NOT NULL" };
			var groupByItems = new List<string> { "VendorCode", "VendorName", "GroupID", "YearPeriod" };
			var orderByItems = new List<string> { "VendorCode" };
			var joinItems = new Dictionary<string, string> { { "Vendors", "VendorID = VendorCode" } };

			var startPeriod = new DateTime(2020, 10, 01);
			var endPeriod = new DateTime(2021, 08, 01);
			int pageSize = 10, pageNumber = 2;
			var data = new TransactionAnalysisHelper().AnalyzeTransactionsInYearPeriod(selectItems, joinItems, criterion, groupByItems, orderByItems, startPeriod, endPeriod);


			//var data = new VendorAnalysisHelper().AnalyzeVendorGroups();
			this.dgvTransactionData.DataSource = data;
		}

		private void button2_Click(object sender, EventArgs e)
		{
			var data = this.dgvTransactionData.DataSource as DataTable;
			var filePath = @"D:\C#\Projects\AccountingSystem\Data\Res.csv";
			ExcelWriter.WriteExcel(filePath, data);
		}
		
		private void txtExcelFile_Click(object sender, EventArgs e)
		{
			var file = new OpenFileDialog();
			file.ShowDialog();
			this.txtExcelFile.Text = file.SafeFileName;
			this.path = file.FileName;
		}
	}
}
