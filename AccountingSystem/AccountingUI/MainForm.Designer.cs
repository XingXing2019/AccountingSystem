
namespace AccountingUI
{
	partial class MainForm
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.btnUploadExcel = new System.Windows.Forms.Button();
			this.txtExcelUploadFile = new System.Windows.Forms.TextBox();
			this.btnAnalyzeTransactions = new System.Windows.Forms.Button();
			this.dgvTransactionData = new System.Windows.Forms.DataGridView();
			this.cmbGroupId = new System.Windows.Forms.ComboBox();
			this.btnSaveAnalysisRes = new System.Windows.Forms.Button();
			this.cmbModelName = new System.Windows.Forms.ComboBox();
			((System.ComponentModel.ISupportInitialize)(this.dgvTransactionData)).BeginInit();
			this.SuspendLayout();
			// 
			// btnUploadExcel
			// 
			this.btnUploadExcel.Location = new System.Drawing.Point(152, 42);
			this.btnUploadExcel.Name = "btnUploadExcel";
			this.btnUploadExcel.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.btnUploadExcel.Size = new System.Drawing.Size(99, 23);
			this.btnUploadExcel.TabIndex = 0;
			this.btnUploadExcel.Text = "Upload Data";
			this.btnUploadExcel.UseVisualStyleBackColor = true;
			this.btnUploadExcel.Click += new System.EventHandler(this.btnLoadExcel_Click);
			// 
			// txtExcelUploadFile
			// 
			this.txtExcelUploadFile.Location = new System.Drawing.Point(12, 12);
			this.txtExcelUploadFile.Name = "txtExcelUploadFile";
			this.txtExcelUploadFile.Size = new System.Drawing.Size(239, 23);
			this.txtExcelUploadFile.TabIndex = 1;
			this.txtExcelUploadFile.Click += new System.EventHandler(this.txtExcelFile_Click);
			// 
			// btnAnalyzeTransactions
			// 
			this.btnAnalyzeTransactions.Location = new System.Drawing.Point(311, 11);
			this.btnAnalyzeTransactions.Name = "btnAnalyzeTransactions";
			this.btnAnalyzeTransactions.Size = new System.Drawing.Size(120, 23);
			this.btnAnalyzeTransactions.TabIndex = 2;
			this.btnAnalyzeTransactions.Text = "Show Transactions";
			this.btnAnalyzeTransactions.UseVisualStyleBackColor = true;
			this.btnAnalyzeTransactions.Click += new System.EventHandler(this.btnAnalyzeTransactions_Click);
			// 
			// dgvTransactionData
			// 
			this.dgvTransactionData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvTransactionData.Location = new System.Drawing.Point(12, 103);
			this.dgvTransactionData.Name = "dgvTransactionData";
			this.dgvTransactionData.RowTemplate.Height = 25;
			this.dgvTransactionData.Size = new System.Drawing.Size(1175, 486);
			this.dgvTransactionData.TabIndex = 3;
			// 
			// cmbGroupId
			// 
			this.cmbGroupId.FormattingEnabled = true;
			this.cmbGroupId.Location = new System.Drawing.Point(864, 43);
			this.cmbGroupId.Name = "cmbGroupId";
			this.cmbGroupId.Size = new System.Drawing.Size(121, 23);
			this.cmbGroupId.TabIndex = 4;
			this.cmbGroupId.Text = "Group IDs";
			// 
			// btnSaveAnalysisRes
			// 
			this.btnSaveAnalysisRes.Location = new System.Drawing.Point(311, 42);
			this.btnSaveAnalysisRes.Name = "btnSaveAnalysisRes";
			this.btnSaveAnalysisRes.Size = new System.Drawing.Size(120, 23);
			this.btnSaveAnalysisRes.TabIndex = 5;
			this.btnSaveAnalysisRes.Text = "Save To Excel";
			this.btnSaveAnalysisRes.UseVisualStyleBackColor = true;
			this.btnSaveAnalysisRes.Click += new System.EventHandler(this.btnSaveAnalysisRes_Click);
			// 
			// cmbModelName
			// 
			this.cmbModelName.FormattingEnabled = true;
			this.cmbModelName.Location = new System.Drawing.Point(12, 41);
			this.cmbModelName.Name = "cmbModelName";
			this.cmbModelName.Size = new System.Drawing.Size(134, 23);
			this.cmbModelName.TabIndex = 6;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1199, 601);
			this.Controls.Add(this.cmbModelName);
			this.Controls.Add(this.btnSaveAnalysisRes);
			this.Controls.Add(this.cmbGroupId);
			this.Controls.Add(this.dgvTransactionData);
			this.Controls.Add(this.btnAnalyzeTransactions);
			this.Controls.Add(this.txtExcelUploadFile);
			this.Controls.Add(this.btnUploadExcel);
			this.Name = "MainForm";
			this.Text = "Form1";
			((System.ComponentModel.ISupportInitialize)(this.dgvTransactionData)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnUploadExcel;
		private System.Windows.Forms.TextBox txtExcelUploadFile;
		private System.Windows.Forms.Button btnAnalyzeTransactions;
		private System.Windows.Forms.DataGridView dgvTransactionData;
		private System.Windows.Forms.ComboBox cmbGroupId;
		private System.Windows.Forms.Button btnSaveAnalysisRes;
		private System.Windows.Forms.ComboBox cmbModelName;
	}
}

