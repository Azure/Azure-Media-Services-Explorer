namespace AMSExplorer
{
    partial class DownloadToLocal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DownloadToLocal));
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBoxOpenFileAfterExport = new System.Windows.Forms.CheckBox();
            this.checkBoxCreateSubfolder = new System.Windows.Forms.CheckBox();
            this.labelAssetName = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonBrowseFile = new System.Windows.Forms.Button();
            this.textBoxFolderPath = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            resources.ApplyResources(this.buttonOk, "buttonOk");
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.UseVisualStyleBackColor = true;
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
            this.groupBox2.Controls.Add(this.checkBoxOpenFileAfterExport);
            this.groupBox2.Controls.Add(this.checkBoxCreateSubfolder);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // checkBoxOpenFileAfterExport
            // 
            resources.ApplyResources(this.checkBoxOpenFileAfterExport, "checkBoxOpenFileAfterExport");
            this.checkBoxOpenFileAfterExport.Checked = true;
            this.checkBoxOpenFileAfterExport.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxOpenFileAfterExport.Name = "checkBoxOpenFileAfterExport";
            this.checkBoxOpenFileAfterExport.UseVisualStyleBackColor = true;
            // 
            // checkBoxCreateSubfolder
            // 
            resources.ApplyResources(this.checkBoxCreateSubfolder, "checkBoxCreateSubfolder");
            this.checkBoxCreateSubfolder.Checked = true;
            this.checkBoxCreateSubfolder.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxCreateSubfolder.Name = "checkBoxCreateSubfolder";
            this.checkBoxCreateSubfolder.UseVisualStyleBackColor = true;
            // 
            // labelAssetName
            // 
            resources.ApplyResources(this.labelAssetName, "labelAssetName");
            this.labelAssetName.Name = "labelAssetName";
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonOk);
            this.panel1.Name = "panel1";
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.buttonBrowseFile);
            this.groupBox1.Controls.Add(this.textBoxFolderPath);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // buttonBrowseFile
            // 
            resources.ApplyResources(this.buttonBrowseFile, "buttonBrowseFile");
            this.buttonBrowseFile.Name = "buttonBrowseFile";
            this.buttonBrowseFile.UseVisualStyleBackColor = true;
            this.buttonBrowseFile.Click += new System.EventHandler(this.buttonBrowseFile_Click);
            // 
            // textBoxFolderPath
            // 
            resources.ApplyResources(this.textBoxFolderPath, "textBoxFolderPath");
            this.textBoxFolderPath.Name = "textBoxFolderPath";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.ForeColor = System.Drawing.Color.DarkBlue;
            this.label5.Name = "label5";
            // 
            // DownloadToLocal
            // 
            this.AcceptButton = this.buttonOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.label5);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labelAssetName);
            this.Controls.Add(this.groupBox2);
            this.Name = "DownloadToLocal";
            this.Load += new System.EventHandler(this.DownloadToLocal_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
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
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonBrowseFile;
        private System.Windows.Forms.TextBox textBoxFolderPath;
        private System.Windows.Forms.CheckBox checkBoxOpenFileAfterExport;
        private System.Windows.Forms.CheckBox checkBoxCreateSubfolder;
        private System.Windows.Forms.Label label5;
    }
}