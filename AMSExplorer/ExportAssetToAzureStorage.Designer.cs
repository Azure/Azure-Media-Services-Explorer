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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExportAssetToAzureStorage));
            this.buttonExport = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonExport
            // 
            resources.ApplyResources(this.buttonExport, "buttonExport");
            this.buttonExport.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.UseVisualStyleBackColor = true;
            this.buttonExport.Click += new System.EventHandler(this.buttonCopy_Click);
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.labelContName);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.textBoxNewContainerName);
            this.groupBox1.Controls.Add(this.textBoxSearch);
            this.groupBox1.Controls.Add(this.radioButtonSelectedContainer);
            this.groupBox1.Controls.Add(this.listViewBlobs);
            this.groupBox1.Controls.Add(this.listViewFiles);
            this.groupBox1.Controls.Add(this.radioButtonNewContainer);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // labelContName
            // 
            resources.ApplyResources(this.labelContName, "labelContName");
            this.labelContName.ForeColor = System.Drawing.Color.Red;
            this.labelContName.Name = "labelContName";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // textBoxNewContainerName
            // 
            resources.ApplyResources(this.textBoxNewContainerName, "textBoxNewContainerName");
            this.textBoxNewContainerName.Name = "textBoxNewContainerName";
            this.textBoxNewContainerName.TextChanged += new System.EventHandler(this.textBoxNewAssetName_TextChanged);
            // 
            // textBoxSearch
            // 
            resources.ApplyResources(this.textBoxSearch, "textBoxSearch");
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.TextChanged += new System.EventHandler(this.textBoxSearch_TextChanged);
            // 
            // radioButtonSelectedContainer
            // 
            resources.ApplyResources(this.radioButtonSelectedContainer, "radioButtonSelectedContainer");
            this.radioButtonSelectedContainer.Name = "radioButtonSelectedContainer";
            this.radioButtonSelectedContainer.UseVisualStyleBackColor = true;
            this.radioButtonSelectedContainer.CheckedChanged += new System.EventHandler(this.radioButtonSelectedContainer_CheckedChanged);
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
            // listViewFiles
            // 
            resources.ApplyResources(this.listViewFiles, "listViewFiles");
            this.listViewFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ListViewFilesName,
            this.ListViewFilesLastModified,
            this.ListViewFilesSize});
            this.listViewFiles.FullRowSelect = true;
            this.listViewFiles.HideSelection = false;
            this.listViewFiles.MultiSelect = false;
            this.listViewFiles.Name = "listViewFiles";
            this.listViewFiles.UseCompatibleStateImageBehavior = false;
            this.listViewFiles.View = System.Windows.Forms.View.Details;
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
            // radioButtonNewContainer
            // 
            resources.ApplyResources(this.radioButtonNewContainer, "radioButtonNewContainer");
            this.radioButtonNewContainer.Checked = true;
            this.radioButtonNewContainer.Name = "radioButtonNewContainer";
            this.radioButtonNewContainer.TabStop = true;
            this.radioButtonNewContainer.UseVisualStyleBackColor = true;
            this.radioButtonNewContainer.CheckedChanged += new System.EventHandler(this.radioButtonNewContainer_CheckedChanged);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
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
            // groupBox3
            // 
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Controls.Add(this.listViewAssetFiles);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // listViewAssetFiles
            // 
            resources.ApplyResources(this.listViewAssetFiles, "listViewAssetFiles");
            this.listViewAssetFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.listViewAssetFiles.FullRowSelect = true;
            this.listViewAssetFiles.HideSelection = false;
            this.listViewAssetFiles.Name = "listViewAssetFiles";
            this.listViewAssetFiles.UseCompatibleStateImageBehavior = false;
            this.listViewAssetFiles.View = System.Windows.Forms.View.Details;
            this.listViewAssetFiles.SelectedIndexChanged += new System.EventHandler(this.listViewAssetFiles_SelectedIndexChanged);
            // 
            // columnHeader3
            // 
            resources.ApplyResources(this.columnHeader3, "columnHeader3");
            // 
            // columnHeader4
            // 
            resources.ApplyResources(this.columnHeader4, "columnHeader4");
            // 
            // columnHeader5
            // 
            resources.ApplyResources(this.columnHeader5, "columnHeader5");
            // 
            // labelWarning
            // 
            resources.ApplyResources(this.labelWarning, "labelWarning");
            this.labelWarning.ForeColor = System.Drawing.Color.Red;
            this.labelWarning.Name = "labelWarning";
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonExport);
            this.panel1.Name = "panel1";
            // 
            // panel2
            // 
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Name = "panel2";
            // 
            // ExportAssetToAzureStorage
            // 
            this.AcceptButton = this.buttonExport;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labelWarning);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "ExportAssetToAzureStorage";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
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

        private System.Windows.Forms.Button buttonExport;
        private System.Windows.Forms.Button buttonCancel;
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
        private System.Windows.Forms.Panel panel2;
    }
}