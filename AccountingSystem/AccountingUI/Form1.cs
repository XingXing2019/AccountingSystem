using System;
using System.Collections.Generic;
using System.Windows.Forms;
using AccountingDatabase.Services;
using AccountingDatabase.Services.Interface;
using AccountingHelper.Helper.DataAnalysisHelper;
using NLog;

namespace AccountingUI
{
	public partial class Form1 : Form
	{
		private readonly Logger _logger = LogManager.GetCurrentClassLogger();
		
		private readonly ITransactionService _transactionService = new TransactionService();
		private readonly IGlAccountService _glAccountService = new GlAccountService();
		private string path;

		
		public Form1()
		{
			InitializeComponent();
		}

		private void btnLoadExcel_Click(object sender, EventArgs e)
		{
			var file = new OpenFileDialog();
			file.ShowDialog();
			this.txtExcelFile.Text = file.SafeFileName;
			this.path = file.FileName;
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
				"VendorID", "COUNT(DISTINCT InvoiceNo) AS Invoices",
				"YearPeriod"
			};
			var criterion = new List<string>{"VendorID IS NOT NULL"};
			var groupbyItems = new List<string> {"VendorID", "YearPeriod"};

			var startPeriod = new DateTime(2020, 10, 01);
			var endPeriod = new DateTime(2021, 08, 01);
			new TransactionAnalysisHelper().AnalysisTransactionsInYearPeriod(selectItems, criterion, groupbyItems, startPeriod, endPeriod);
		}
	}
}
