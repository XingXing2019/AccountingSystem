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
			string templateId = "DataAnalysis", sqlId = "VendorAnalysis";
			cmbGroupId.DataSource = _accountingUIHelper.LoadGroupIDs(templateId, sqlId);
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
			string templateId = "DataAnalysis", sqlId = "TransactionAnalysis";
			this.dgvTransactionData.DataSource = new DataAnalyzer().ExecuteSQLAction(templateId, sqlId);
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
