
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
			this.btnLoadExcel = new System.Windows.Forms.Button();
			this.txtExcelFile = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.dgvTransactionData = new System.Windows.Forms.DataGridView();
			this.cmbGroupId = new System.Windows.Forms.ComboBox();
			this.button2 = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dgvTransactionData)).BeginInit();
			this.SuspendLayout();
			// 
			// btnLoadExcel
			// 
			this.btnLoadExcel.Location = new System.Drawing.Point(202, 46);
			this.btnLoadExcel.Name = "btnLoadExcel";
			this.btnLoadExcel.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.btnLoadExcel.Size = new System.Drawing.Size(75, 23);
			this.btnLoadExcel.TabIndex = 0;
			this.btnLoadExcel.Text = "Load";
			this.btnLoadExcel.UseVisualStyleBackColor = true;
			this.btnLoadExcel.Click += new System.EventHandler(this.btnLoadExcel_Click);
			// 
			// txtExcelFile
			// 
			this.txtExcelFile.Location = new System.Drawing.Point(64, 47);
			this.txtExcelFile.Name = "txtExcelFile";
			this.txtExcelFile.Size = new System.Drawing.Size(100, 23);
			this.txtExcelFile.TabIndex = 1;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(305, 46);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 2;
			this.button1.Text = "button1";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// dgvTransactionData
			// 
			this.dgvTransactionData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvTransactionData.Location = new System.Drawing.Point(12, 103);
			this.dgvTransactionData.Name = "dgvTransactionData";
			this.dgvTransactionData.RowTemplate.Height = 25;
			this.dgvTransactionData.Size = new System.Drawing.Size(1175, 286);
			this.dgvTransactionData.TabIndex = 3;
			// 
			// cmbGroupId
			// 
			this.cmbGroupId.FormattingEnabled = true;
			this.cmbGroupId.Location = new System.Drawing.Point(418, 46);
			this.cmbGroupId.Name = "cmbGroupId";
			this.cmbGroupId.Size = new System.Drawing.Size(121, 23);
			this.cmbGroupId.TabIndex = 4;
			this.cmbGroupId.Text = "Group IDs";
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(575, 43);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 23);
			this.button2.TabIndex = 5;
			this.button2.Text = "button2";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1252, 450);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.cmbGroupId);
			this.Controls.Add(this.dgvTransactionData);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.txtExcelFile);
			this.Controls.Add(this.btnLoadExcel);
			this.Name = "MainForm";
			this.Text = "Form1";
			((System.ComponentModel.ISupportInitialize)(this.dgvTransactionData)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnLoadExcel;
		private System.Windows.Forms.TextBox txtExcelFile;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.DataGridView dgvTransactionData;
		private System.Windows.Forms.ComboBox cmbGroupId;
		private System.Windows.Forms.Button button2;
	}
}

