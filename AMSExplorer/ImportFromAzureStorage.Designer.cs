namespace AMSExplorer
{
    partial class ImportFromAzureStorage
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
            this.labelSelectedAssetWarning = new System.Windows.Forms.Label();
            this.labelExistingAssetName = new System.Windows.Forms.Label();
            this.textBoxNewAssetName = new System.Windows.Forms.TextBox();
            this.radioButtonSelectedAsset = new System.Windows.Forms.RadioButton();
            this.radioButtonNewAsset = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.labelDefaultStorage = new System.Windows.Forms.Label();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxStorageKey = new System.Windows.Forms.TextBox();
            this.textBoxStorageName = new System.Windows.Forms.TextBox();
            this.radioButtonOtherStorage = new System.Windows.Forms.RadioButton();
            this.radioButtonStorageDefault = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.listViewFiles = new System.Windows.Forms.ListView();
            this.ListViewFilesName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ListViewFilesLastModified = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ListViewFilesSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listViewBlobs = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonUpload
            // 
            this.buttonUpload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonUpload.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonUpload.Enabled = false;
            this.buttonUpload.Location = new System.Drawing.Point(564, 12);
            this.buttonUpload.Name = "buttonUpload";
            this.buttonUpload.Size = new System.Drawing.Size(99, 23);
            this.buttonUpload.TabIndex = 2;
            this.buttonUpload.Text = "Upload";
            this.buttonUpload.UseVisualStyleBackColor = true;
            this.buttonUpload.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.Location = new System.Drawing.Point(675, 12);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(99, 23);
            this.buttonClose.TabIndex = 10;
            this.buttonClose.Text = "Cancel";
            this.buttonClose.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.labelSelectedAssetWarning);
            this.groupBox1.Controls.Add(this.labelExistingAssetName);
            this.groupBox1.Controls.Add(this.textBoxNewAssetName);
            this.groupBox1.Controls.Add(this.radioButtonSelectedAsset);
            this.groupBox1.Controls.Add(this.radioButtonNewAsset);
            this.groupBox1.Location = new System.Drawing.Point(25, 438);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(735, 77);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Copy to Azure Media Services";
            // 
            // labelSelectedAssetWarning
            // 
            this.labelSelectedAssetWarning.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelSelectedAssetWarning.AutoSize = true;
            this.labelSelectedAssetWarning.ForeColor = System.Drawing.Color.Red;
            this.labelSelectedAssetWarning.Location = new System.Drawing.Point(475, 45);
            this.labelSelectedAssetWarning.Name = "labelSelectedAssetWarning";
            this.labelSelectedAssetWarning.Size = new System.Drawing.Size(47, 13);
            this.labelSelectedAssetWarning.TabIndex = 45;
            this.labelSelectedAssetWarning.Text = "Warning";
            // 
            // labelExistingAssetName
            // 
            this.labelExistingAssetName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelExistingAssetName.AutoSize = true;
            this.labelExistingAssetName.Location = new System.Drawing.Point(146, 45);
            this.labelExistingAssetName.Name = "labelExistingAssetName";
            this.labelExistingAssetName.Size = new System.Drawing.Size(58, 13);
            this.labelExistingAssetName.TabIndex = 18;
            this.labelExistingAssetName.Text = "assetname";
            // 
            // textBoxNewAssetName
            // 
            this.textBoxNewAssetName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxNewAssetName.Location = new System.Drawing.Point(149, 19);
            this.textBoxNewAssetName.Name = "textBoxNewAssetName";
            this.textBoxNewAssetName.Size = new System.Drawing.Size(317, 20);
            this.textBoxNewAssetName.TabIndex = 13;
            // 
            // radioButtonSelectedAsset
            // 
            this.radioButtonSelectedAsset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.radioButtonSelectedAsset.AutoSize = true;
            this.radioButtonSelectedAsset.Location = new System.Drawing.Point(23, 43);
            this.radioButtonSelectedAsset.Name = "radioButtonSelectedAsset";
            this.radioButtonSelectedAsset.Size = new System.Drawing.Size(129, 17);
            this.radioButtonSelectedAsset.TabIndex = 1;
            this.radioButtonSelectedAsset.Text = "to the selected asset: ";
            this.radioButtonSelectedAsset.UseVisualStyleBackColor = true;
            this.radioButtonSelectedAsset.CheckedChanged += new System.EventHandler(this.radioButtonSelectedAsset_CheckedChanged);
            // 
            // radioButtonNewAsset
            // 
            this.radioButtonNewAsset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.radioButtonNewAsset.AutoSize = true;
            this.radioButtonNewAsset.Checked = true;
            this.radioButtonNewAsset.Location = new System.Drawing.Point(23, 19);
            this.radioButtonNewAsset.Name = "radioButtonNewAsset";
            this.radioButtonNewAsset.Size = new System.Drawing.Size(99, 17);
            this.radioButtonNewAsset.TabIndex = 0;
            this.radioButtonNewAsset.TabStop = true;
            this.radioButtonNewAsset.Text = "as a new asset:";
            this.radioButtonNewAsset.UseVisualStyleBackColor = true;
            this.radioButtonNewAsset.CheckedChanged += new System.EventHandler(this.radioButtonNewAsset_CheckedChanged);
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
            this.groupBox2.Location = new System.Drawing.Point(25, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(735, 107);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Azure Storage Source";
            // 
            // labelDefaultStorage
            // 
            this.labelDefaultStorage.AutoSize = true;
            this.labelDefaultStorage.Location = new System.Drawing.Point(262, 27);
            this.labelDefaultStorage.Name = "labelDefaultStorage";
            this.labelDefaultStorage.Size = new System.Drawing.Size(68, 13);
            this.labelDefaultStorage.TabIndex = 17;
            this.labelDefaultStorage.Text = "storagename";
            // 
            // buttonConnect
            // 
            this.buttonConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonConnect.Enabled = false;
            this.buttonConnect.Location = new System.Drawing.Point(654, 50);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(75, 23);
            this.buttonConnect.TabIndex = 16;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(189, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Access key:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(180, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Account name:";
            // 
            // textBoxStorageKey
            // 
            this.textBoxStorageKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxStorageKey.Enabled = false;
            this.textBoxStorageKey.Location = new System.Drawing.Point(262, 76);
            this.textBoxStorageKey.Name = "textBoxStorageKey";
            this.textBoxStorageKey.Size = new System.Drawing.Size(374, 20);
            this.textBoxStorageKey.TabIndex = 13;
            // 
            // textBoxStorageName
            // 
            this.textBoxStorageName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxStorageName.Enabled = false;
            this.textBoxStorageName.Location = new System.Drawing.Point(262, 50);
            this.textBoxStorageName.Name = "textBoxStorageName";
            this.textBoxStorageName.Size = new System.Drawing.Size(374, 20);
            this.textBoxStorageName.TabIndex = 12;
            // 
            // radioButtonOtherStorage
            // 
            this.radioButtonOtherStorage.AutoSize = true;
            this.radioButtonOtherStorage.Location = new System.Drawing.Point(23, 50);
            this.radioButtonOtherStorage.Name = "radioButtonOtherStorage";
            this.radioButtonOtherStorage.Size = new System.Drawing.Size(132, 17);
            this.radioButtonOtherStorage.TabIndex = 11;
            this.radioButtonOtherStorage.Text = "Another Azure Storage";
            this.radioButtonOtherStorage.UseVisualStyleBackColor = true;
            this.radioButtonOtherStorage.CheckedChanged += new System.EventHandler(this.radioButtonOtherStorage_CheckedChanged);
            // 
            // radioButtonStorageDefault
            // 
            this.radioButtonStorageDefault.AutoSize = true;
            this.radioButtonStorageDefault.Checked = true;
            this.radioButtonStorageDefault.Location = new System.Drawing.Point(23, 27);
            this.radioButtonStorageDefault.Name = "radioButtonStorageDefault";
            this.radioButtonStorageDefault.Size = new System.Drawing.Size(203, 17);
            this.radioButtonStorageDefault.TabIndex = 10;
            this.radioButtonStorageDefault.TabStop = true;
            this.radioButtonStorageDefault.Text = "Default Azure Media Services storage";
            this.radioButtonStorageDefault.UseVisualStyleBackColor = true;
            this.radioButtonStorageDefault.CheckedChanged += new System.EventHandler(this.radioButtonStorageDefault_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 134);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Containers";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(438, 134);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Files";
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
            this.listViewFiles.FullRowSelect = true;
            this.listViewFiles.HideSelection = false;
            this.listViewFiles.Location = new System.Drawing.Point(441, 150);
            this.listViewFiles.Name = "listViewFiles";
            this.listViewFiles.Size = new System.Drawing.Size(313, 276);
            this.listViewFiles.TabIndex = 20;
            this.listViewFiles.UseCompatibleStateImageBehavior = false;
            this.listViewFiles.View = System.Windows.Forms.View.Details;
            this.listViewFiles.SelectedIndexChanged += new System.EventHandler(this.listViewFiles_SelectedIndexChanged);
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
            // listViewBlobs
            // 
            this.listViewBlobs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listViewBlobs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listViewBlobs.FullRowSelect = true;
            this.listViewBlobs.HideSelection = false;
            this.listViewBlobs.Location = new System.Drawing.Point(25, 150);
            this.listViewBlobs.MultiSelect = false;
            this.listViewBlobs.Name = "listViewBlobs";
            this.listViewBlobs.Size = new System.Drawing.Size(410, 249);
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
            // textBoxSearch
            // 
            this.textBoxSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxSearch.Location = new System.Drawing.Point(72, 406);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(363, 20);
            this.textBoxSearch.TabIndex = 22;
            this.textBoxSearch.TextChanged += new System.EventHandler(this.textBoxSearch_TextChanged);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 413);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 13);
            this.label5.TabIndex = 23;
            this.label5.Text = "Search:";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonUpload);
            this.panel1.Controls.Add(this.buttonClose);
            this.panel1.Location = new System.Drawing.Point(-2, 529);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(791, 48);
            this.panel1.TabIndex = 63;
            // 
            // ImportFromAzureStorage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(784, 576);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxSearch);
            this.Controls.Add(this.listViewBlobs);
            this.Controls.Add(this.listViewFiles);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "ImportFromAzureStorage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Import from Azure Storage";
            this.Load += new System.EventHandler(this.UploadFromBlob_Load);
            this.SizeChanged += new System.EventHandler(this.UploadFromBlob_SizeChanged);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonUpload;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxNewAssetName;
        private System.Windows.Forms.RadioButton radioButtonSelectedAsset;
        private System.Windows.Forms.RadioButton radioButtonNewAsset;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxStorageKey;
        private System.Windows.Forms.TextBox textBoxStorageName;
        private System.Windows.Forms.RadioButton radioButtonOtherStorage;
        private System.Windows.Forms.RadioButton radioButtonStorageDefault;
        private System.Windows.Forms.Label labelDefaultStorage;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelExistingAssetName;
        private System.Windows.Forms.ListView listViewFiles;
        private System.Windows.Forms.ColumnHeader ListViewFilesName;
        private System.Windows.Forms.ColumnHeader ListViewFilesLastModified;
        private System.Windows.Forms.ColumnHeader ListViewFilesSize;
        private System.Windows.Forms.ListView listViewBlobs;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labelSelectedAssetWarning;
        private System.Windows.Forms.Panel panel1;
    }
}