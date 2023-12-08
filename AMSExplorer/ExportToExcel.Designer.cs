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
            buttonOk = new System.Windows.Forms.Button();
            buttonCancel = new System.Windows.Forms.Button();
            groupBox2 = new System.Windows.Forms.GroupBox();
            checkBoxLocalTime = new System.Windows.Forms.CheckBox();
            label1 = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            radioButtonDetailledMode = new System.Windows.Forms.RadioButton();
            radioButtonNormalMode = new System.Windows.Forms.RadioButton();
            labelAssetName = new System.Windows.Forms.Label();
            radioButtonSelectedAssets = new System.Windows.Forms.RadioButton();
            groupBox4 = new System.Windows.Forms.GroupBox();
            radioButtonAllAssets = new System.Windows.Forms.RadioButton();
            panel1 = new System.Windows.Forms.Panel();
            saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            groupBox1 = new System.Windows.Forms.GroupBox();
            radioButton1 = new System.Windows.Forms.RadioButton();
            radioButtonFormatExcel = new System.Windows.Forms.RadioButton();
            checkBoxOpenFileAfterExport = new System.Windows.Forms.CheckBox();
            buttonBrowseFile = new System.Windows.Forms.Button();
            textBoxExcelFile = new System.Windows.Forms.TextBox();
            progressBarExport = new System.Windows.Forms.ProgressBar();
            labelProgress = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            groupBox2.SuspendLayout();
            groupBox4.SuspendLayout();
            panel1.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // buttonOk
            // 
            resources.ApplyResources(buttonOk, "buttonOk");
            buttonOk.Name = "buttonOk";
            buttonOk.UseVisualStyleBackColor = true;
            buttonOk.Click += buttonOk_Click;
            // 
            // buttonCancel
            // 
            resources.ApplyResources(buttonCancel, "buttonCancel");
            buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            buttonCancel.Name = "buttonCancel";
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += buttonCancel_Click;
            // 
            // groupBox2
            // 
            resources.ApplyResources(groupBox2, "groupBox2");
            groupBox2.Controls.Add(checkBoxLocalTime);
            groupBox2.Controls.Add(label1);
            groupBox2.Controls.Add(label8);
            groupBox2.Controls.Add(radioButtonDetailledMode);
            groupBox2.Controls.Add(radioButtonNormalMode);
            groupBox2.Name = "groupBox2";
            groupBox2.TabStop = false;
            // 
            // checkBoxLocalTime
            // 
            resources.ApplyResources(checkBoxLocalTime, "checkBoxLocalTime");
            checkBoxLocalTime.Checked = true;
            checkBoxLocalTime.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxLocalTime.Name = "checkBoxLocalTime";
            checkBoxLocalTime.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.ForeColor = System.Drawing.SystemColors.WindowFrame;
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // label8
            // 
            label8.ForeColor = System.Drawing.SystemColors.WindowFrame;
            resources.ApplyResources(label8, "label8");
            label8.Name = "label8";
            // 
            // radioButtonDetailledMode
            // 
            resources.ApplyResources(radioButtonDetailledMode, "radioButtonDetailledMode");
            radioButtonDetailledMode.Name = "radioButtonDetailledMode";
            radioButtonDetailledMode.UseVisualStyleBackColor = true;
            // 
            // radioButtonNormalMode
            // 
            resources.ApplyResources(radioButtonNormalMode, "radioButtonNormalMode");
            radioButtonNormalMode.Checked = true;
            radioButtonNormalMode.Name = "radioButtonNormalMode";
            radioButtonNormalMode.TabStop = true;
            radioButtonNormalMode.UseVisualStyleBackColor = true;
            // 
            // labelAssetName
            // 
            labelAssetName.ForeColor = System.Drawing.SystemColors.WindowFrame;
            resources.ApplyResources(labelAssetName, "labelAssetName");
            labelAssetName.Name = "labelAssetName";
            // 
            // radioButtonSelectedAssets
            // 
            resources.ApplyResources(radioButtonSelectedAssets, "radioButtonSelectedAssets");
            radioButtonSelectedAssets.Checked = true;
            radioButtonSelectedAssets.Name = "radioButtonSelectedAssets";
            radioButtonSelectedAssets.TabStop = true;
            radioButtonSelectedAssets.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            resources.ApplyResources(groupBox4, "groupBox4");
            groupBox4.Controls.Add(radioButtonAllAssets);
            groupBox4.Controls.Add(radioButtonSelectedAssets);
            groupBox4.Name = "groupBox4";
            groupBox4.TabStop = false;
            // 
            // radioButtonAllAssets
            // 
            resources.ApplyResources(radioButtonAllAssets, "radioButtonAllAssets");
            radioButtonAllAssets.Name = "radioButtonAllAssets";
            radioButtonAllAssets.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.BackColor = System.Drawing.SystemColors.Control;
            panel1.Controls.Add(buttonCancel);
            panel1.Controls.Add(buttonOk);
            resources.ApplyResources(panel1, "panel1");
            panel1.Name = "panel1";
            // 
            // saveFileDialog1
            // 
            resources.ApplyResources(saveFileDialog1, "saveFileDialog1");
            // 
            // groupBox1
            // 
            resources.ApplyResources(groupBox1, "groupBox1");
            groupBox1.Controls.Add(radioButton1);
            groupBox1.Controls.Add(radioButtonFormatExcel);
            groupBox1.Controls.Add(checkBoxOpenFileAfterExport);
            groupBox1.Controls.Add(buttonBrowseFile);
            groupBox1.Controls.Add(textBoxExcelFile);
            groupBox1.Name = "groupBox1";
            groupBox1.TabStop = false;
            // 
            // radioButton1
            // 
            resources.ApplyResources(radioButton1, "radioButton1");
            radioButton1.Name = "radioButton1";
            radioButton1.UseVisualStyleBackColor = true;
            radioButton1.CheckedChanged += radioButton1_CheckedChanged;
            // 
            // radioButtonFormatExcel
            // 
            resources.ApplyResources(radioButtonFormatExcel, "radioButtonFormatExcel");
            radioButtonFormatExcel.Checked = true;
            radioButtonFormatExcel.Name = "radioButtonFormatExcel";
            radioButtonFormatExcel.TabStop = true;
            radioButtonFormatExcel.UseVisualStyleBackColor = true;
            // 
            // checkBoxOpenFileAfterExport
            // 
            resources.ApplyResources(checkBoxOpenFileAfterExport, "checkBoxOpenFileAfterExport");
            checkBoxOpenFileAfterExport.Checked = true;
            checkBoxOpenFileAfterExport.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxOpenFileAfterExport.Name = "checkBoxOpenFileAfterExport";
            checkBoxOpenFileAfterExport.UseVisualStyleBackColor = true;
            // 
            // buttonBrowseFile
            // 
            resources.ApplyResources(buttonBrowseFile, "buttonBrowseFile");
            buttonBrowseFile.Name = "buttonBrowseFile";
            buttonBrowseFile.UseVisualStyleBackColor = true;
            buttonBrowseFile.Click += buttonBrowseFile_Click;
            // 
            // textBoxExcelFile
            // 
            resources.ApplyResources(textBoxExcelFile, "textBoxExcelFile");
            textBoxExcelFile.Name = "textBoxExcelFile";
            // 
            // progressBarExport
            // 
            resources.ApplyResources(progressBarExport, "progressBarExport");
            progressBarExport.Name = "progressBarExport";
            // 
            // labelProgress
            // 
            resources.ApplyResources(labelProgress, "labelProgress");
            labelProgress.Name = "labelProgress";
            // 
            // label5
            // 
            resources.ApplyResources(label5, "label5");
            label5.ForeColor = System.Drawing.Color.DarkBlue;
            label5.Name = "label5";
            // 
            // ExportToExcel
            // 
            AcceptButton = buttonOk;
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            BackColor = System.Drawing.SystemColors.Window;
            CancelButton = buttonCancel;
            Controls.Add(label5);
            Controls.Add(labelProgress);
            Controls.Add(progressBarExport);
            Controls.Add(groupBox1);
            Controls.Add(panel1);
            Controls.Add(groupBox4);
            Controls.Add(labelAssetName);
            Controls.Add(groupBox2);
            Name = "ExportToExcel";
            Load += ExportToExcel_Load;
            Shown += ExportToExcel_Shown;
            DpiChanged += ExportToExcel_DpiChanged;
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            panel1.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
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
        private System.Windows.Forms.CheckBox checkBoxOpenFileAfterExport;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxLocalTime;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButtonFormatExcel;
    }
}