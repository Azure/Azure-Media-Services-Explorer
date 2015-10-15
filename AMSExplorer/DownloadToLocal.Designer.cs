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
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBoxOpenFileAfterExport = new System.Windows.Forms.CheckBox();
            this.checkBoxCreateSubfolder = new System.Windows.Forms.CheckBox();
            this.radioButtonAssetId = new System.Windows.Forms.RadioButton();
            this.radioButtonAssetName = new System.Windows.Forms.RadioButton();
            this.labelAssetName = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonBrowseFile = new System.Windows.Forms.Button();
            this.textBoxFolderPath = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(298, 14);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(143, 27);
            this.buttonOk.TabIndex = 4;
            this.buttonOk.Text = "Download to folder";
            this.buttonOk.UseVisualStyleBackColor = true;
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
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.checkBoxOpenFileAfterExport);
            this.groupBox2.Controls.Add(this.checkBoxCreateSubfolder);
            this.groupBox2.Controls.Add(this.radioButtonAssetId);
            this.groupBox2.Controls.Add(this.radioButtonAssetName);
            this.groupBox2.Location = new System.Drawing.Point(14, 186);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(546, 145);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Options";
            // 
            // checkBoxOpenFileAfterExport
            // 
            this.checkBoxOpenFileAfterExport.AutoSize = true;
            this.checkBoxOpenFileAfterExport.Checked = true;
            this.checkBoxOpenFileAfterExport.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxOpenFileAfterExport.Location = new System.Drawing.Point(26, 110);
            this.checkBoxOpenFileAfterExport.Name = "checkBoxOpenFileAfterExport";
            this.checkBoxOpenFileAfterExport.Size = new System.Drawing.Size(336, 19);
            this.checkBoxOpenFileAfterExport.TabIndex = 2;
            this.checkBoxOpenFileAfterExport.Text = "When download is complete, open folder with File Explorer";
            this.checkBoxOpenFileAfterExport.UseVisualStyleBackColor = true;
            // 
            // checkBoxCreateSubfolder
            // 
            this.checkBoxCreateSubfolder.AutoSize = true;
            this.checkBoxCreateSubfolder.Checked = true;
            this.checkBoxCreateSubfolder.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxCreateSubfolder.Location = new System.Drawing.Point(26, 22);
            this.checkBoxCreateSubfolder.Name = "checkBoxCreateSubfolder";
            this.checkBoxCreateSubfolder.Size = new System.Drawing.Size(179, 19);
            this.checkBoxCreateSubfolder.TabIndex = 72;
            this.checkBoxCreateSubfolder.Text = "Create a subfolder based on :";
            this.checkBoxCreateSubfolder.UseVisualStyleBackColor = true;
            // 
            // radioButtonAssetId
            // 
            this.radioButtonAssetId.AutoSize = true;
            this.radioButtonAssetId.Location = new System.Drawing.Point(55, 73);
            this.radioButtonAssetId.Name = "radioButtonAssetId";
            this.radioButtonAssetId.Size = new System.Drawing.Size(66, 19);
            this.radioButtonAssetId.TabIndex = 71;
            this.radioButtonAssetId.Text = "Asset Id";
            this.radioButtonAssetId.UseVisualStyleBackColor = true;
            // 
            // radioButtonAssetName
            // 
            this.radioButtonAssetName.AutoSize = true;
            this.radioButtonAssetName.Checked = true;
            this.radioButtonAssetName.Location = new System.Drawing.Point(55, 48);
            this.radioButtonAssetName.Name = "radioButtonAssetName";
            this.radioButtonAssetName.Size = new System.Drawing.Size(86, 19);
            this.radioButtonAssetName.TabIndex = 4;
            this.radioButtonAssetName.TabStop = true;
            this.radioButtonAssetName.Text = "Asset name";
            this.radioButtonAssetName.UseVisualStyleBackColor = true;
            // 
            // labelAssetName
            // 
            this.labelAssetName.Location = new System.Drawing.Point(15, 50);
            this.labelAssetName.Name = "labelAssetName";
            this.labelAssetName.Size = new System.Drawing.Size(527, 15);
            this.labelAssetName.TabIndex = 42;
            this.labelAssetName.Text = "Download {0} asset{1} to a local folder";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonOk);
            this.panel1.Location = new System.Drawing.Point(-2, 355);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(575, 55);
            this.panel1.TabIndex = 60;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.buttonBrowseFile);
            this.groupBox1.Controls.Add(this.textBoxFolderPath);
            this.groupBox1.Location = new System.Drawing.Point(15, 84);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(545, 81);
            this.groupBox1.TabIndex = 61;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Local folder";
            // 
            // buttonBrowseFile
            // 
            this.buttonBrowseFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBrowseFile.Location = new System.Drawing.Point(433, 33);
            this.buttonBrowseFile.Name = "buttonBrowseFile";
            this.buttonBrowseFile.Size = new System.Drawing.Size(95, 23);
            this.buttonBrowseFile.TabIndex = 1;
            this.buttonBrowseFile.Text = "Browse";
            this.buttonBrowseFile.UseVisualStyleBackColor = true;
            this.buttonBrowseFile.Click += new System.EventHandler(this.buttonBrowseFile_Click);
            // 
            // textBoxFolderPath
            // 
            this.textBoxFolderPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFolderPath.Location = new System.Drawing.Point(16, 34);
            this.textBoxFolderPath.Name = "textBoxFolderPath";
            this.textBoxFolderPath.Size = new System.Drawing.Size(410, 23);
            this.textBoxFolderPath.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DarkBlue;
            this.label5.Location = new System.Drawing.Point(14, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(133, 20);
            this.label5.TabIndex = 72;
            this.label5.Text = "Download Asset(s)";
            // 
            // DownloadToLocal
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(572, 409);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labelAssetName);
            this.Controls.Add(this.groupBox2);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "DownloadToLocal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Download asset(s)";
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
        private System.Windows.Forms.RadioButton radioButtonAssetName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonBrowseFile;
        private System.Windows.Forms.TextBox textBoxFolderPath;
        private System.Windows.Forms.CheckBox checkBoxOpenFileAfterExport;
        private System.Windows.Forms.CheckBox checkBoxCreateSubfolder;
        private System.Windows.Forms.RadioButton radioButtonAssetId;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label5;
    }
}