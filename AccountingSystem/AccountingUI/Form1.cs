using System;
using System.Windows.Forms;
using AccountingDatabase.Entity;
using AccountingDatabase.Repository.Implementation;
using AccountingDatabase.Repository.Interface;
using AccountingHelper.Helper;
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
			var vendorPath = @"C:\Users\61425\Desktop\RAL-VENDOR.xlsx";
			var vendors = new ExcelHelper<Vendor>().ReadExcel(vendorPath);
			new VendorService().PostAll(vendors);
			
			var glAccountPath = @"C:\Users\61425\Desktop\GL.xls";
			var glAccounts = new ExcelHelper<GLAccount>().ReadExcel(glAccountPath);
			new GlAccountService().PostAll(glAccounts);
		}
	}
}
