using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using AccountingDatabase.Entity;
using AccountingDatabase.Repository.Implementation;
using AccountingDatabase.Repository.Interface;
using AccountingHelper.Helper;
using AccountingHelper.Interface;
using NLog;
using NLog.Fluent;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;

namespace AccountingUI
{
	public partial class Form1 : Form
	{
		private readonly Logger _logger = LogManager.GetCurrentClassLogger();
		private readonly IExcelHelper _excelHelper = new ExcelHelper();
		private readonly ITransactionService _transactionService = new TransactionService();
		private readonly IGLService _glService = new GLService();
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
			//var transactions = _excelHelper.ReadGls(@"C:\Users\61425\Desktop\GL.xls");
			var transactions = _excelHelper.ReadTransactions(@"C:\Users\61425\Desktop\Data.xls");

			_transactionService.PostAll(transactions);
		}

		
	}
}
