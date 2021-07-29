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
			var whereItems = new List<string> { "VendorCode IS NOT NULL" };
			var groupByItems = new List<string> { "VendorCode", "VendorName", "GroupID", "YearPeriod" };
			var orderByItems = new List<string> { "VendorCode" };
			var joinItems = new Dictionary<string, string> { { "Vendors", "VendorID = VendorCode" } };

			//int pageSize = 10, pageNumber = 2;


			var startPeriod = new DateTime(2020, 10, 01);
			var endPeriod = new DateTime(2021, 08, 01);

			var sqlEntity = new SqlEntity(selectItems, joinItems, whereItems, groupByItems, orderByItems);


			var data = new TransactionAnalysisHelper().AnalyzeTransactionsInYearPeriod(sqlEntity, startPeriod, endPeriod);


			//var data = new VendorAnalysisHelper().AnalyzeVendorGroups();
			this.dgvTransactionData.DataSource = data;
		}

		private void button2_Click(object sender, EventArgs e)
		{
			var saveFileDialog = new SaveFileDialog();
			saveFileDialog.Filter = "CSV file (*.csv)|*.csv| All Files (*.*)|*.*";

			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				var filePath = saveFileDialog.FileName;
				var data = this.dgvTransactionData.DataSource as DataTable;
				ExcelWriter.WriteExcel(filePath, data);
			}
		}
		
		private void txtExcelFile_Click(object sender, EventArgs e)
		{
			var file = new OpenFileDialog();
			file.ShowDialog();
			this.txtExcelUploadFile.Text = file.SafeFileName;
			this.path = file.FileName;
		}
	}
}
