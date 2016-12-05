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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportFromAzureStorage));
            this.buttonUpload = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxOneAssetPerFile = new System.Windows.Forms.CheckBox();
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonUpload
            // 
            resources.ApplyResources(this.buttonUpload, "buttonUpload");
            this.buttonUpload.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonUpload.Name = "buttonUpload";
            this.buttonUpload.UseVisualStyleBackColor = true;
            this.buttonUpload.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonClose
            // 
            resources.ApplyResources(this.buttonClose, "buttonClose");
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.checkBoxOneAssetPerFile);
            this.groupBox1.Controls.Add(this.labelSelectedAssetWarning);
            this.groupBox1.Controls.Add(this.labelExistingAssetName);
            this.groupBox1.Controls.Add(this.textBoxNewAssetName);
            this.groupBox1.Controls.Add(this.radioButtonSelectedAsset);
            this.groupBox1.Controls.Add(this.radioButtonNewAsset);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // checkBoxOneAssetPerFile
            // 
            resources.ApplyResources(this.checkBoxOneAssetPerFile, "checkBoxOneAssetPerFile");
            this.checkBoxOneAssetPerFile.Name = "checkBoxOneAssetPerFile";
            this.checkBoxOneAssetPerFile.UseVisualStyleBackColor = true;
            // 
            // labelSelectedAssetWarning
            // 
            resources.ApplyResources(this.labelSelectedAssetWarning, "labelSelectedAssetWarning");
            this.labelSelectedAssetWarning.ForeColor = System.Drawing.Color.Red;
            this.labelSelectedAssetWarning.Name = "labelSelectedAssetWarning";
            // 
            // labelExistingAssetName
            // 
            resources.ApplyResources(this.labelExistingAssetName, "labelExistingAssetName");
            this.labelExistingAssetName.Name = "labelExistingAssetName";
            // 
            // textBoxNewAssetName
            // 
            resources.ApplyResources(this.textBoxNewAssetName, "textBoxNewAssetName");
            this.textBoxNewAssetName.Name = "textBoxNewAssetName";
            // 
            // radioButtonSelectedAsset
            // 
            resources.ApplyResources(this.radioButtonSelectedAsset, "radioButtonSelectedAsset");
            this.radioButtonSelectedAsset.Name = "radioButtonSelectedAsset";
            this.radioButtonSelectedAsset.UseVisualStyleBackColor = true;
            this.radioButtonSelectedAsset.CheckedChanged += new System.EventHandler(this.radioButtonSelectedAsset_CheckedChanged);
            // 
            // radioButtonNewAsset
            // 
            resources.ApplyResources(this.radioButtonNewAsset, "radioButtonNewAsset");
            this.radioButtonNewAsset.Checked = true;
            this.radioButtonNewAsset.Name = "radioButtonNewAsset";
            this.radioButtonNewAsset.TabStop = true;
            this.radioButtonNewAsset.UseVisualStyleBackColor = true;
            this.radioButtonNewAsset.CheckedChanged += new System.EventHandler(this.radioButtonNewAsset_CheckedChanged);
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.labelDefaultStorage);
            this.groupBox2.Controls.Add(this.buttonConnect);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.textBoxStorageKey);
            this.groupBox2.Controls.Add(this.textBoxStorageName);
            this.groupBox2.Controls.Add(this.radioButtonOtherStorage);
            this.groupBox2.Controls.Add(this.radioButtonStorageDefault);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // labelDefaultStorage
            // 
            resources.ApplyResources(this.labelDefaultStorage, "labelDefaultStorage");
            this.labelDefaultStorage.Name = "labelDefaultStorage";
            // 
            // buttonConnect
            // 
            resources.ApplyResources(this.buttonConnect, "buttonConnect");
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // textBoxStorageKey
            // 
            resources.ApplyResources(this.textBoxStorageKey, "textBoxStorageKey");
            this.textBoxStorageKey.Name = "textBoxStorageKey";
            // 
            // textBoxStorageName
            // 
            resources.ApplyResources(this.textBoxStorageName, "textBoxStorageName");
            this.textBoxStorageName.Name = "textBoxStorageName";
            // 
            // radioButtonOtherStorage
            // 
            resources.ApplyResources(this.radioButtonOtherStorage, "radioButtonOtherStorage");
            this.radioButtonOtherStorage.Name = "radioButtonOtherStorage";
            this.radioButtonOtherStorage.UseVisualStyleBackColor = true;
            this.radioButtonOtherStorage.CheckedChanged += new System.EventHandler(this.radioButtonOtherStorage_CheckedChanged);
            // 
            // radioButtonStorageDefault
            // 
            resources.ApplyResources(this.radioButtonStorageDefault, "radioButtonStorageDefault");
            this.radioButtonStorageDefault.Checked = true;
            this.radioButtonStorageDefault.Name = "radioButtonStorageDefault";
            this.radioButtonStorageDefault.TabStop = true;
            this.radioButtonStorageDefault.UseVisualStyleBackColor = true;
            this.radioButtonStorageDefault.CheckedChanged += new System.EventHandler(this.radioButtonStorageDefault_CheckedChanged);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // listViewFiles
            // 
            resources.ApplyResources(this.listViewFiles, "listViewFiles");
            this.listViewFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ListViewFilesName,
            this.ListViewFilesLastModified,
            this.ListViewFilesSize});
            this.listViewFiles.FullRowSelect = true;
            this.listViewFiles.HideSelection = false;
            this.listViewFiles.Name = "listViewFiles";
            this.listViewFiles.UseCompatibleStateImageBehavior = false;
            this.listViewFiles.View = System.Windows.Forms.View.Details;
            this.listViewFiles.SelectedIndexChanged += new System.EventHandler(this.listViewFiles_SelectedIndexChanged);
            // 
            // ListViewFilesName
            // 
            resources.ApplyResources(this.ListViewFilesName, "ListViewFilesName");
            // 
            // ListViewFilesLastModified
            // 
            resources.ApplyResources(this.ListViewFilesLastModified, "ListViewFilesLastModified");
            // 
            // ListViewFilesSize
            // 
            resources.ApplyResources(this.ListViewFilesSize, "ListViewFilesSize");
            // 
            // listViewBlobs
            // 
            resources.ApplyResources(this.listViewBlobs, "listViewBlobs");
            this.listViewBlobs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listViewBlobs.FullRowSelect = true;
            this.listViewBlobs.HideSelection = false;
            this.listViewBlobs.MultiSelect = false;
            this.listViewBlobs.Name = "listViewBlobs";
            this.listViewBlobs.UseCompatibleStateImageBehavior = false;
            this.listViewBlobs.View = System.Windows.Forms.View.Details;
            this.listViewBlobs.SelectedIndexChanged += new System.EventHandler(this.listViewBlobs_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            resources.ApplyResources(this.columnHeader1, "columnHeader1");
            // 
            // columnHeader2
            // 
            resources.ApplyResources(this.columnHeader2, "columnHeader2");
            // 
            // textBoxSearch
            // 
            resources.ApplyResources(this.textBoxSearch, "textBoxSearch");
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.TextChanged += new System.EventHandler(this.textBoxSearch_TextChanged);
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonUpload);
            this.panel1.Controls.Add(this.buttonClose);
            this.panel1.Name = "panel1";
            // 
            // panel2
            // 
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Name = "panel2";
            // 
            // ImportFromAzureStorage
            // 
            this.AcceptButton = this.buttonUpload;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonClose;
            this.Controls.Add(this.panel2);
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
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
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
        private System.Windows.Forms.CheckBox checkBoxOneAssetPerFile;
        private System.Windows.Forms.Panel panel2;
    }
}