
namespace AccountingUI
{
	partial class Form1
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
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.txtExcelFile);
			this.Controls.Add(this.btnLoadExcel);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnLoadExcel;
		private System.Windows.Forms.TextBox txtExcelFile;
	}
}

