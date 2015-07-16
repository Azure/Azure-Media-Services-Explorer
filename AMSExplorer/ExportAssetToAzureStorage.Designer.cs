namespace AMSExplorer
{
    partial class ExportAssetToAzureStorage
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
            this.buttonUpload = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelContName = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxNewContainerName = new System.Windows.Forms.TextBox();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.radioButtonSelectedContainer = new System.Windows.Forms.RadioButton();
            this.listViewBlobs = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listViewFiles = new System.Windows.Forms.ListView();
            this.ListViewFilesName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ListViewFilesLastModified = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ListViewFilesSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.radioButtonNewContainer = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.labelDefaultStorage = new System.Windows.Forms.Label();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxStorageKey = new System.Windows.Forms.TextBox();
            this.textBoxStorageName = new System.Windows.Forms.TextBox();
            this.radioButtonOtherStorage = new System.Windows.Forms.RadioButton();
            this.radioButtonStorageDefault = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.listViewAssetFiles = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.labelWarning = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonUpload
            // 
            this.buttonUpload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonUpload.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonUpload.Enabled = false;
            this.buttonUpload.Location = new System.Drawing.Point(588, 15);
            this.buttonUpload.Name = "buttonUpload";
            this.buttonUpload.Size = new System.Drawing.Size(87, 27);
            this.buttonUpload.TabIndex = 2;
            this.buttonUpload.Text = "Export";
            this.buttonUpload.UseVisualStyleBackColor = true;
            this.buttonUpload.Click += new System.EventHandler(this.buttonCopy_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.Location = new System.Drawing.Point(682, 15);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(87, 27);
            this.buttonClose.TabIndex = 10;
            this.buttonClose.Text = "Cancel";
            this.buttonClose.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.labelContName);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.textBoxNewContainerName);
            this.groupBox1.Controls.Add(this.textBoxSearch);
            this.groupBox1.Controls.Add(this.radioButtonSelectedContainer);
            this.groupBox1.Controls.Add(this.listViewBlobs);
            this.groupBox1.Controls.Add(this.listViewFiles);
            this.groupBox1.Controls.Add(this.radioButtonNewContainer);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(29, 312);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(726, 258);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Copy to";
            // 
            // labelContName
            // 
            this.labelContName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelContName.AutoSize = true;
            this.labelContName.ForeColor = System.Drawing.Color.Red;
            this.labelContName.Location = new System.Drawing.Point(219, 47);
            this.labelContName.Name = "labelContName";
            this.labelContName.Size = new System.Drawing.Size(52, 15);
            this.labelContName.TabIndex = 42;
            this.labelContName.Text = "Warning";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 226);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 15);
            this.label5.TabIndex = 23;
            this.label5.Text = "Search :";
            // 
            // textBoxNewContainerName
            // 
            this.textBoxNewContainerName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxNewContainerName.Location = new System.Drawing.Point(149, 21);
            this.textBoxNewContainerName.Name = "textBoxNewContainerName";
            this.textBoxNewContainerName.Size = new System.Drawing.Size(570, 23);
            this.textBoxNewContainerName.TabIndex = 13;
            this.textBoxNewContainerName.TextChanged += new System.EventHandler(this.textBoxNewAssetName_TextChanged);
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxSearch.Enabled = false;
            this.textBoxSearch.Location = new System.Drawing.Point(83, 223);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(406, 23);
            this.textBoxSearch.TabIndex = 22;
            this.textBoxSearch.TextChanged += new System.EventHandler(this.textBoxSearch_TextChanged);
            // 
            // radioButtonSelectedContainer
            // 
            this.radioButtonSelectedContainer.AutoSize = true;
            this.radioButtonSelectedContainer.Location = new System.Drawing.Point(28, 52);
            this.radioButtonSelectedContainer.Name = "radioButtonSelectedContainer";
            this.radioButtonSelectedContainer.Size = new System.Drawing.Size(161, 19);
            this.radioButtonSelectedContainer.TabIndex = 1;
            this.radioButtonSelectedContainer.Text = "to the selected container :";
            this.radioButtonSelectedContainer.UseVisualStyleBackColor = true;
            this.radioButtonSelectedContainer.CheckedChanged += new System.EventHandler(this.radioButtonSelectedContainer_CheckedChanged);
            // 
            // listViewBlobs
            // 
            this.listViewBlobs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listViewBlobs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listViewBlobs.Enabled = false;
            this.listViewBlobs.FullRowSelect = true;
            this.listViewBlobs.HideSelection = false;
            this.listViewBlobs.Location = new System.Drawing.Point(28, 78);
            this.listViewBlobs.MultiSelect = false;
            this.listViewBlobs.Name = "listViewBlobs";
            this.listViewBlobs.Size = new System.Drawing.Size(461, 136);
            this.listViewBlobs.TabIndex = 21;
            this.listViewBlobs.UseCompatibleStateImageBehavior = false;
            this.listViewBlobs.View = System.Windows.Forms.View.Details;
            this.listViewBlobs.SelectedIndexChanged += new System.EventHandler(this.listViewBlobs_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 25;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Last Modified";
            // 
            // listViewFiles
            // 
            this.listViewFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ListViewFilesName,
            this.ListViewFilesLastModified,
            this.ListViewFilesSize});
            this.listViewFiles.Enabled = false;
            this.listViewFiles.FullRowSelect = true;
            this.listViewFiles.HideSelection = false;
            this.listViewFiles.Location = new System.Drawing.Point(497, 78);
            this.listViewFiles.MultiSelect = false;
            this.listViewFiles.Name = "listViewFiles";
            this.listViewFiles.Size = new System.Drawing.Size(222, 167);
            this.listViewFiles.TabIndex = 20;
            this.listViewFiles.UseCompatibleStateImageBehavior = false;
            this.listViewFiles.View = System.Windows.Forms.View.Details;
            // 
            // ListViewFilesName
            // 
            this.ListViewFilesName.Text = "Name";
            this.ListViewFilesName.Width = 25;
            // 
            // ListViewFilesLastModified
            // 
            this.ListViewFilesLastModified.Text = "Last Modified";
            // 
            // ListViewFilesSize
            // 
            this.ListViewFilesSize.Text = "Size";
            // 
            // radioButtonNewContainer
            // 
            this.radioButtonNewContainer.AutoSize = true;
            this.radioButtonNewContainer.Checked = true;
            this.radioButtonNewContainer.Location = new System.Drawing.Point(28, 22);
            this.radioButtonNewContainer.Name = "radioButtonNewContainer";
            this.radioButtonNewContainer.Size = new System.Drawing.Size(115, 19);
            this.radioButtonNewContainer.TabIndex = 0;
            this.radioButtonNewContainer.TabStop = true;
            this.radioButtonNewContainer.Text = "a new container :";
            this.radioButtonNewContainer.UseVisualStyleBackColor = true;
            this.radioButtonNewContainer.CheckedChanged += new System.EventHandler(this.radioButtonNewContainer_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(497, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 15);
            this.label4.TabIndex = 19;
            this.label4.Text = "Files";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.labelDefaultStorage);
            this.groupBox2.Controls.Add(this.buttonConnect);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.textBoxStorageKey);
            this.groupBox2.Controls.Add(this.textBoxStorageName);
            this.groupBox2.Controls.Add(this.radioButtonOtherStorage);
            this.groupBox2.Controls.Add(this.radioButtonStorageDefault);
            this.groupBox2.Location = new System.Drawing.Point(29, 181);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(726, 123);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Azure Storage Account Destination";
            // 
            // labelDefaultStorage
            // 
            this.labelDefaultStorage.AutoSize = true;
            this.labelDefaultStorage.Location = new System.Drawing.Point(306, 31);
            this.labelDefaultStorage.Name = "labelDefaultStorage";
            this.labelDefaultStorage.Size = new System.Drawing.Size(76, 15);
            this.labelDefaultStorage.TabIndex = 17;
            this.labelDefaultStorage.Text = "storagename";
            // 
            // buttonConnect
            // 
            this.buttonConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonConnect.Enabled = false;
            this.buttonConnect.Location = new System.Drawing.Point(632, 54);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(87, 27);
            this.buttonConnect.TabIndex = 16;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(210, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 15);
            this.label2.TabIndex = 15;
            this.label2.Text = "Access key :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(210, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 15);
            this.label1.TabIndex = 14;
            this.label1.Text = "Account name :";
            // 
            // textBoxStorageKey
            // 
            this.textBoxStorageKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxStorageKey.Enabled = false;
            this.textBoxStorageKey.Location = new System.Drawing.Point(306, 88);
            this.textBoxStorageKey.Name = "textBoxStorageKey";
            this.textBoxStorageKey.Size = new System.Drawing.Size(305, 23);
            this.textBoxStorageKey.TabIndex = 13;
            // 
            // textBoxStorageName
            // 
            this.textBoxStorageName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxStorageName.Enabled = false;
            this.textBoxStorageName.Location = new System.Drawing.Point(306, 58);
            this.textBoxStorageName.Name = "textBoxStorageName";
            this.textBoxStorageName.Size = new System.Drawing.Size(305, 23);
            this.textBoxStorageName.TabIndex = 12;
            // 
            // radioButtonOtherStorage
            // 
            this.radioButtonOtherStorage.AutoSize = true;
            this.radioButtonOtherStorage.Location = new System.Drawing.Point(27, 58);
            this.radioButtonOtherStorage.Name = "radioButtonOtherStorage";
            this.radioButtonOtherStorage.Size = new System.Drawing.Size(144, 19);
            this.radioButtonOtherStorage.TabIndex = 11;
            this.radioButtonOtherStorage.Text = "Another Azure Storage";
            this.radioButtonOtherStorage.UseVisualStyleBackColor = true;
            this.radioButtonOtherStorage.CheckedChanged += new System.EventHandler(this.radioButtonOtherStorage_CheckedChanged);
            // 
            // radioButtonStorageDefault
            // 
            this.radioButtonStorageDefault.AutoSize = true;
            this.radioButtonStorageDefault.Checked = true;
            this.radioButtonStorageDefault.Location = new System.Drawing.Point(27, 31);
            this.radioButtonStorageDefault.Name = "radioButtonStorageDefault";
            this.radioButtonStorageDefault.Size = new System.Drawing.Size(219, 19);
            this.radioButtonStorageDefault.TabIndex = 10;
            this.radioButtonStorageDefault.TabStop = true;
            this.radioButtonStorageDefault.Text = "Default Azure Media Services storage";
            this.radioButtonStorageDefault.UseVisualStyleBackColor = true;
            this.radioButtonStorageDefault.CheckedChanged += new System.EventHandler(this.radioButtonStorageDefault_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.listViewAssetFiles);
            this.groupBox3.Location = new System.Drawing.Point(29, 15);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(726, 159);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Select asset files";
            // 
            // listViewAssetFiles
            // 
            this.listViewAssetFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewAssetFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.listViewAssetFiles.FullRowSelect = true;
            this.listViewAssetFiles.HideSelection = false;
            this.listViewAssetFiles.Location = new System.Drawing.Point(7, 22);
            this.listViewAssetFiles.Name = "listViewAssetFiles";
            this.listViewAssetFiles.Size = new System.Drawing.Size(712, 130);
            this.listViewAssetFiles.TabIndex = 21;
            this.listViewAssetFiles.UseCompatibleStateImageBehavior = false;
            this.listViewAssetFiles.View = System.Windows.Forms.View.Details;
            this.listViewAssetFiles.SelectedIndexChanged += new System.EventHandler(this.listViewAssetFiles_SelectedIndexChanged);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Name";
            this.columnHeader3.Width = 25;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Last Modified";
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Size";
            // 
            // labelWarning
            // 
            this.labelWarning.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelWarning.AutoSize = true;
            this.labelWarning.ForeColor = System.Drawing.Color.Red;
            this.labelWarning.Location = new System.Drawing.Point(26, 579);
            this.labelWarning.Name = "labelWarning";
            this.labelWarning.Size = new System.Drawing.Size(52, 15);
            this.labelWarning.TabIndex = 41;
            this.labelWarning.Text = "Warning";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonClose);
            this.panel1.Controls.Add(this.buttonUpload);
            this.panel1.Location = new System.Drawing.Point(0, 606);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(784, 55);
            this.panel1.TabIndex = 62;
            // 
            // ExportAssetToAzureStorage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(784, 661);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labelWarning);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "ExportAssetToAzureStorage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Export Asset File(s) to Azure Storage";
            this.Load += new System.EventHandler(this.UploadFromBlob_Load);
            this.SizeChanged += new System.EventHandler(this.UploadFromBlob_SizeChanged);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonUpload;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxNewContainerName;
        private System.Windows.Forms.RadioButton radioButtonSelectedContainer;
        private System.Windows.Forms.RadioButton radioButtonNewContainer;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxStorageKey;
        private System.Windows.Forms.TextBox textBoxStorageName;
        private System.Windows.Forms.RadioButton radioButtonOtherStorage;
        private System.Windows.Forms.RadioButton radioButtonStorageDefault;
        private System.Windows.Forms.Label labelDefaultStorage;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListView listViewFiles;
        private System.Windows.Forms.ColumnHeader ListViewFilesName;
        private System.Windows.Forms.ColumnHeader ListViewFilesLastModified;
        private System.Windows.Forms.ColumnHeader ListViewFilesSize;
        private System.Windows.Forms.ListView listViewBlobs;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListView listViewAssetFiles;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.Label labelWarning;
        private System.Windows.Forms.Label labelContName;
        private System.Windows.Forms.Panel panel1;
    }
}