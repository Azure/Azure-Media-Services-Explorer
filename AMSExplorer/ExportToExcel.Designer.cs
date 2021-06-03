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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExportToExcel));
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBoxLocalTime = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.radioButtonDetailledMode = new System.Windows.Forms.RadioButton();
            this.radioButtonNormalMode = new System.Windows.Forms.RadioButton();
            this.labelAssetName = new System.Windows.Forms.Label();
            this.radioButtonSelectedAssets = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.radioButtonAllAssets = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButtonFormatExcel = new System.Windows.Forms.RadioButton();
            this.checkBoxOpenFileAfterExport = new System.Windows.Forms.CheckBox();
            this.buttonBrowseFile = new System.Windows.Forms.Button();
            this.textBoxExcelFile = new System.Windows.Forms.TextBox();
            this.progressBarExport = new System.Windows.Forms.ProgressBar();
            this.labelProgress = new System.Windows.Forms.Label();
            this.backgroundWorkerExcel = new System.ComponentModel.BackgroundWorker();
            this.label5 = new System.Windows.Forms.Label();
            this.backgroundWorkerCSV = new System.ComponentModel.BackgroundWorker();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            resources.ApplyResources(this.buttonOk, "buttonOk");
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.checkBoxLocalTime);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.radioButtonDetailledMode);
            this.groupBox2.Controls.Add(this.radioButtonNormalMode);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // checkBoxLocalTime
            // 
            resources.ApplyResources(this.checkBoxLocalTime, "checkBoxLocalTime");
            this.checkBoxLocalTime.Checked = true;
            this.checkBoxLocalTime.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxLocalTime.Name = "checkBoxLocalTime";
            this.checkBoxLocalTime.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label1.Name = "label1";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label8.Name = "label8";
            // 
            // radioButtonDetailledMode
            // 
            resources.ApplyResources(this.radioButtonDetailledMode, "radioButtonDetailledMode");
            this.radioButtonDetailledMode.Name = "radioButtonDetailledMode";
            this.radioButtonDetailledMode.UseVisualStyleBackColor = true;
            // 
            // radioButtonNormalMode
            // 
            resources.ApplyResources(this.radioButtonNormalMode, "radioButtonNormalMode");
            this.radioButtonNormalMode.Checked = true;
            this.radioButtonNormalMode.Name = "radioButtonNormalMode";
            this.radioButtonNormalMode.TabStop = true;
            this.radioButtonNormalMode.UseVisualStyleBackColor = true;
            // 
            // labelAssetName
            // 
            resources.ApplyResources(this.labelAssetName, "labelAssetName");
            this.labelAssetName.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.labelAssetName.Name = "labelAssetName";
            // 
            // radioButtonSelectedAssets
            // 
            resources.ApplyResources(this.radioButtonSelectedAssets, "radioButtonSelectedAssets");
            this.radioButtonSelectedAssets.Checked = true;
            this.radioButtonSelectedAssets.Name = "radioButtonSelectedAssets";
            this.radioButtonSelectedAssets.TabStop = true;
            this.radioButtonSelectedAssets.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Controls.Add(this.radioButtonAllAssets);
            this.groupBox4.Controls.Add(this.radioButtonSelectedAssets);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            // 
            // radioButtonAllAssets
            // 
            resources.ApplyResources(this.radioButtonAllAssets, "radioButtonAllAssets");
            this.radioButtonAllAssets.Name = "radioButtonAllAssets";
            this.radioButtonAllAssets.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonOk);
            this.panel1.Name = "panel1";
            // 
            // saveFileDialog1
            // 
            resources.ApplyResources(this.saveFileDialog1, "saveFileDialog1");
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Controls.Add(this.radioButtonFormatExcel);
            this.groupBox1.Controls.Add(this.checkBoxOpenFileAfterExport);
            this.groupBox1.Controls.Add(this.buttonBrowseFile);
            this.groupBox1.Controls.Add(this.textBoxExcelFile);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // radioButton1
            // 
            resources.ApplyResources(this.radioButton1, "radioButton1");
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioButtonFormatExcel
            // 
            resources.ApplyResources(this.radioButtonFormatExcel, "radioButtonFormatExcel");
            this.radioButtonFormatExcel.Checked = true;
            this.radioButtonFormatExcel.Name = "radioButtonFormatExcel";
            this.radioButtonFormatExcel.TabStop = true;
            this.radioButtonFormatExcel.UseVisualStyleBackColor = true;
            // 
            // checkBoxOpenFileAfterExport
            // 
            resources.ApplyResources(this.checkBoxOpenFileAfterExport, "checkBoxOpenFileAfterExport");
            this.checkBoxOpenFileAfterExport.Checked = true;
            this.checkBoxOpenFileAfterExport.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxOpenFileAfterExport.Name = "checkBoxOpenFileAfterExport";
            this.checkBoxOpenFileAfterExport.UseVisualStyleBackColor = true;
            // 
            // buttonBrowseFile
            // 
            resources.ApplyResources(this.buttonBrowseFile, "buttonBrowseFile");
            this.buttonBrowseFile.Name = "buttonBrowseFile";
            this.buttonBrowseFile.UseVisualStyleBackColor = true;
            this.buttonBrowseFile.Click += new System.EventHandler(this.buttonBrowseFile_Click);
            // 
            // textBoxExcelFile
            // 
            resources.ApplyResources(this.textBoxExcelFile, "textBoxExcelFile");
            this.textBoxExcelFile.Name = "textBoxExcelFile";
            // 
            // progressBarExport
            // 
            resources.ApplyResources(this.progressBarExport, "progressBarExport");
            this.progressBarExport.Name = "progressBarExport";
            // 
            // labelProgress
            // 
            resources.ApplyResources(this.labelProgress, "labelProgress");
            this.labelProgress.Name = "labelProgress";
            // 
            // backgroundWorkerExcel
            // 
            this.backgroundWorkerExcel.WorkerReportsProgress = true;
            this.backgroundWorkerExcel.WorkerSupportsCancellation = true;
            this.backgroundWorkerExcel.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerExcel_DoWork);
            this.backgroundWorkerExcel.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorkerExcel.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.ForeColor = System.Drawing.Color.DarkBlue;
            this.label5.Name = "label5";
            // 
            // backgroundWorkerCSV
            // 
            this.backgroundWorkerCSV.WorkerReportsProgress = true;
            this.backgroundWorkerCSV.WorkerSupportsCancellation = true;
            this.backgroundWorkerCSV.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerCSV_DoWork);
            this.backgroundWorkerCSV.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorkerCSV.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // ExportToExcel
            // 
            this.AcceptButton = this.buttonOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.label5);
            this.Controls.Add(this.labelProgress);
            this.Controls.Add(this.progressBarExport);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.labelAssetName);
            this.Controls.Add(this.groupBox2);
            this.Name = "ExportToExcel";
            this.Load += new System.EventHandler(this.ExportToExcel_Load);
            this.Shown += new System.EventHandler(this.ExportToExcel_Shown);
            this.DpiChanged += new System.Windows.Forms.DpiChangedEventHandler(this.ExportToExcel_DpiChanged);
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
        private System.Windows.Forms.Label labelProgress;
        private System.ComponentModel.BackgroundWorker backgroundWorkerExcel;
        private System.Windows.Forms.CheckBox checkBoxOpenFileAfterExport;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxLocalTime;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButtonFormatExcel;
        private System.ComponentModel.BackgroundWorker backgroundWorkerCSV;
    }
}