using System;
using System.Collections.Generic;
using System.Windows.Forms;
using AccountingDatabase.Services;
using AccountingDatabase.Services.Interface;
using AccountingHelper.Helper.DataAnalysisHelper;
using AccountingHelper.Helper.ExcelHelper;
using AccountingHelper.Helper.ModelHelper;
using AccountingHelper.Helper.UIHelper;
using AccountingHelper.Model;
using NLog;

namespace AccountingUI
{
	public partial class MainForm : Form
	{
		private readonly Logger _logger = LogManager.GetCurrentClassLogger();
		
		//private readonly ITransactionService _transactionService = new TransactionService();
		//private readonly IGlAccountService _glAccountService = new GlAccountService();
		//private string path;

		private readonly AccountingUIHelper _uiHelper;
		
		public MainForm()
		{
			InitializeComponent();
			_uiHelper = new AccountingUIHelper();
			this.cmbGroupId.DataSource = _uiHelper.LoadGroupIDs();
		}

		private void btnLoadExcel_Click(object sender, EventArgs e)
		{
			var file = new OpenFileDialog();
			file.ShowDialog();
			this.txtExcelFile.Text = file.SafeFileName;
			//this.path = file.FileName;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			//var vendorPath = @"C:\Users\61425\Desktop\RAL-VENDOR.xlsx";
			//var vendorModels = new ExcelHelper<VendorModel>().ReadExcel(vendorPath);
			//var vendors = new VendorModelHelper().TransformValidModels(vendorModels);
			//new VendorService().PostAll(vendors);

			//var glAccountPath = @"C:\Users\61425\Desktop\GL.xls";
			//var glAccountModels = new ExcelHelper<GLAccountModel>().ReadExcel(glAccountPath);
			//var glAccounts = new GlAccountModelHelper().TransformValidModels(glAccountModels);
			//new GlAccountService().PostAll(glAccounts);

			//var transactionPath = @"C:\Users\61425\Desktop\Data\RAL AP.xls";
			//var transactionModels = new ExcelHelper<TransactionModel>().ReadExcel(transactionPath);
			//var transactions = new TransactionModelHelper().TransformValidModels(transactionModels);
			//new TransactionService().PostAll(transactions);

			var selectItems = new List<string>
			{
				"VendorCode", 
				"VendorName", 
				"COUNT(DISTINCT InvoiceNo) AS Invoices",
				"SUBSTRING(CONVERT(VARCHAR(50), YearPeriod), 1, 7) AS YearPeriod"
			};
			var criterion = new List<string> { "VendorCode IS NOT NULL" };
			var groupByItems = new List<string> { "VendorCode", "VendorName", "YearPeriod" };
			var orderByItems = new List<string> { "VendorCode" };
			var joinItems = new Dictionary<string, string> {{"Vendors", "VendorID = VendorCode"}};

			var startPeriod = new DateTime(2020, 10, 01);
			var endPeriod = new DateTime(2021, 08, 01);
			int pageSize = 10, pageNumber = 2;
			var data = new TransactionAnalysisHelper().AnalyzeTransactionsInYearPeriod(selectItems, joinItems, criterion, groupByItems, orderByItems, startPeriod, endPeriod);


			//var data = new VendorAnalysisHelper().AnalyzeVendorGroups();
			this.dgvTransactionData.DataSource = data;
		}
	}
}
