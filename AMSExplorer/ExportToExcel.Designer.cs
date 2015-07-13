namespace AMSExplorer
{
    partial class ExportToExcel
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioButtonDetailledMode = new System.Windows.Forms.RadioButton();
            this.radioButtonNormalMode = new System.Windows.Forms.RadioButton();
            this.labelAssetName = new System.Windows.Forms.Label();
            this.radioButtonSelectedAssets = new System.Windows.Forms.RadioButton();
            this.radioButtonDisplayedAssets = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.radioButtonAllAssets = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonBrowseFile = new System.Windows.Forms.Button();
            this.textBoxExcelFile = new System.Windows.Forms.TextBox();
            this.progressBarExport = new System.Windows.Forms.ProgressBar();
            this.label2 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.Location = new System.Drawing.Point(329, 14);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(112, 27);
            this.buttonOk.TabIndex = 4;
            this.buttonOk.Text = "Export to Excel";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(448, 14);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(112, 27);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioButtonDetailledMode);
            this.groupBox2.Controls.Add(this.radioButtonNormalMode);
            this.groupBox2.Location = new System.Drawing.Point(14, 178);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(544, 89);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Options";
            // 
            // radioButtonDetailledMode
            // 
            this.radioButtonDetailledMode.AutoSize = true;
            this.radioButtonDetailledMode.Location = new System.Drawing.Point(26, 47);
            this.radioButtonDetailledMode.Name = "radioButtonDetailledMode";
            this.radioButtonDetailledMode.Size = new System.Drawing.Size(105, 19);
            this.radioButtonDetailledMode.TabIndex = 4;
            this.radioButtonDetailledMode.Text = "Detailled mode";
            this.radioButtonDetailledMode.UseVisualStyleBackColor = true;
            // 
            // radioButtonNormalMode
            // 
            this.radioButtonNormalMode.AutoSize = true;
            this.radioButtonNormalMode.Checked = true;
            this.radioButtonNormalMode.Location = new System.Drawing.Point(26, 22);
            this.radioButtonNormalMode.Name = "radioButtonNormalMode";
            this.radioButtonNormalMode.Size = new System.Drawing.Size(99, 19);
            this.radioButtonNormalMode.TabIndex = 3;
            this.radioButtonNormalMode.TabStop = true;
            this.radioButtonNormalMode.Text = "Normal mode";
            this.radioButtonNormalMode.UseVisualStyleBackColor = true;
            // 
            // labelAssetName
            // 
            this.labelAssetName.Location = new System.Drawing.Point(15, 15);
            this.labelAssetName.Name = "labelAssetName";
            this.labelAssetName.Size = new System.Drawing.Size(527, 15);
            this.labelAssetName.TabIndex = 42;
            this.labelAssetName.Text = "Exports asset metadata to an Excel file";
            // 
            // radioButtonSelectedAssets
            // 
            this.radioButtonSelectedAssets.AutoSize = true;
            this.radioButtonSelectedAssets.Checked = true;
            this.radioButtonSelectedAssets.Location = new System.Drawing.Point(26, 22);
            this.radioButtonSelectedAssets.Name = "radioButtonSelectedAssets";
            this.radioButtonSelectedAssets.Size = new System.Drawing.Size(103, 19);
            this.radioButtonSelectedAssets.TabIndex = 0;
            this.radioButtonSelectedAssets.TabStop = true;
            this.radioButtonSelectedAssets.Text = "Selected assets";
            this.radioButtonSelectedAssets.UseVisualStyleBackColor = true;
            // 
            // radioButtonDisplayedAssets
            // 
            this.radioButtonDisplayedAssets.AutoSize = true;
            this.radioButtonDisplayedAssets.Location = new System.Drawing.Point(26, 47);
            this.radioButtonDisplayedAssets.Name = "radioButtonDisplayedAssets";
            this.radioButtonDisplayedAssets.Size = new System.Drawing.Size(130, 19);
            this.radioButtonDisplayedAssets.TabIndex = 1;
            this.radioButtonDisplayedAssets.Text = "Visible assets in grid";
            this.radioButtonDisplayedAssets.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.radioButtonAllAssets);
            this.groupBox4.Controls.Add(this.radioButtonDisplayedAssets);
            this.groupBox4.Controls.Add(this.radioButtonSelectedAssets);
            this.groupBox4.Location = new System.Drawing.Point(14, 55);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(544, 103);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Assets";
            // 
            // radioButtonAllAssets
            // 
            this.radioButtonAllAssets.AutoSize = true;
            this.radioButtonAllAssets.Location = new System.Drawing.Point(26, 72);
            this.radioButtonAllAssets.Name = "radioButtonAllAssets";
            this.radioButtonAllAssets.Size = new System.Drawing.Size(73, 19);
            this.radioButtonAllAssets.TabIndex = 2;
            this.radioButtonAllAssets.Text = "All assets";
            this.radioButtonAllAssets.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonOk);
            this.panel1.Location = new System.Drawing.Point(-2, 553);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(575, 55);
            this.panel1.TabIndex = 60;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "Excel files|*.xls|All files|*.*";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonBrowseFile);
            this.groupBox1.Controls.Add(this.textBoxExcelFile);
            this.groupBox1.Location = new System.Drawing.Point(13, 293);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(545, 100);
            this.groupBox1.TabIndex = 61;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Export to file";
            // 
            // buttonBrowseFile
            // 
            this.buttonBrowseFile.Location = new System.Drawing.Point(433, 33);
            this.buttonBrowseFile.Name = "buttonBrowseFile";
            this.buttonBrowseFile.Size = new System.Drawing.Size(95, 23);
            this.buttonBrowseFile.TabIndex = 1;
            this.buttonBrowseFile.Text = "Browse";
            this.buttonBrowseFile.UseVisualStyleBackColor = true;
            this.buttonBrowseFile.Click += new System.EventHandler(this.buttonBrowseFile_Click);
            // 
            // textBoxExcelFile
            // 
            this.textBoxExcelFile.Location = new System.Drawing.Point(16, 34);
            this.textBoxExcelFile.Name = "textBoxExcelFile";
            this.textBoxExcelFile.Size = new System.Drawing.Size(410, 23);
            this.textBoxExcelFile.TabIndex = 0;
            // 
            // progressBarExport
            // 
            this.progressBarExport.Location = new System.Drawing.Point(29, 464);
            this.progressBarExport.Name = "progressBarExport";
            this.progressBarExport.Size = new System.Drawing.Size(513, 23);
            this.progressBarExport.TabIndex = 62;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 446);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 15);
            this.label2.TabIndex = 63;
            this.label2.Text = "Progress";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // ExportToExcel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(572, 607);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.progressBarExport);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.labelAssetName);
            this.Controls.Add(this.groupBox2);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "ExportToExcel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Export data to Excel";
            this.Load += new System.EventHandler(this.ExportToExcel_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button buttonOk;
        public System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label labelAssetName;
        private System.Windows.Forms.RadioButton radioButtonSelectedAssets;
        private System.Windows.Forms.RadioButton radioButtonDisplayedAssets;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radioButtonDetailledMode;
        private System.Windows.Forms.RadioButton radioButtonNormalMode;
        private System.Windows.Forms.RadioButton radioButtonAllAssets;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonBrowseFile;
        private System.Windows.Forms.TextBox textBoxExcelFile;
        private System.Windows.Forms.ProgressBar progressBarExport;
        private System.Windows.Forms.Label label2;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}