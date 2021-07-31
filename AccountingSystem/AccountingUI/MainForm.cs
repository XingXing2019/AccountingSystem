using AccountingHelper.Helper.UIHelper;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

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
			if (_accountingUIHelper.TryLoadGroupIDs(templateId, sqlId, out var groupIds))
			{
				cmbGroupId.DataSource = groupIds;
			}

			cmbTransAnalysisType.DataSource = new List<string> {"Invoice Count", "Total Balance"};
			
			
			cmbModelName.DataSource = _accountingUIHelper.LoadModelNames();
		}

		private void btnLoadExcel_Click(object sender, EventArgs e)
		{
			var path = this.path;
			var modelName = this.cmbModelName.Text;
			if (!_accountingUIHelper.UploadExcelDataToDatabase(modelName, path))
				MessageBox.Show("Failed to upload excel data to database");
			else
				MessageBox.Show($"Save {modelName}s to database");
		}

		private void btnAnalyzeTransactions_Click(object sender, EventArgs e)
		{
			string templateId = "DataAnalysis", sqlId = cmbTransAnalysisType.Text;
			if (!_accountingUIHelper.AnalyseTransactions(templateId, sqlId, out var result))
			{
				_logger.Error($"Unable to analyse transaction data");
				MessageBox.Show($"Unable to analyse transaction data");
			}

			this.dgvTransactionData.DataSource = result;
		}

		private void btnSaveAnalysisRes_Click(object sender, EventArgs e)
		{
			if (this.dgvTransactionData.DataSource == null)
			{
				MessageBox.Show("There is no data to save");
				return;
			}

			var saveFileDialog = new SaveFileDialog();
			saveFileDialog.Filter = "CSV file (*.csv)|*.csv| All Files (*.*)|*.*";

			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				var filePath = saveFileDialog.FileName;
				var data = this.dgvTransactionData.DataSource as DataTable;
				this.dgvTransactionData.DataSource = null;
				_accountingUIHelper.DownloadDatabaseDataToExcel(data, filePath);
				MessageBox.Show($"Excel saved");
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
