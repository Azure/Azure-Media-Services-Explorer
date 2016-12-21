namespace AMSExplorer
{
    partial class BulkUpload
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BulkUpload));
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.openFileDialogAssetFiles = new System.Windows.Forms.OpenFileDialog();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonSplitSelection = new System.Windows.Forms.Button();
            this.buttonGroupSelectionInOneAsset = new System.Windows.Forms.Button();
            this.buttonRemoveAll = new System.Windows.Forms.Button();
            this.labelWarningFiles = new System.Windows.Forms.Label();
            this.buttonSelectFolder = new System.Windows.Forms.Button();
            this.buttonSelectFiles = new System.Windows.Forms.Button();
            this.comboBoxStorageAsset = new System.Windows.Forms.ComboBox();
            this.label33 = new System.Windows.Forms.Label();
            this.buttonDelFiles = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridAssetFiles = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label10 = new System.Windows.Forms.Label();
            this.radioButtonCENCEncryption = new System.Windows.Forms.RadioButton();
            this.radioButtonStorageEncryption = new System.Windows.Forms.RadioButton();
            this.radioButtonEncryptionNone = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonBrowseFile = new System.Windows.Forms.Button();
            this.textBoxFolderPath = new System.Windows.Forms.TextBox();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.linkLabelInfoAzCopy = new System.Windows.Forms.LinkLabel();
            this.checkBoxGenerateAzCopy = new System.Windows.Forms.CheckBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.panelSigniant = new System.Windows.Forms.Panel();
            this.comboBoxSigniantServer = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxSigniantAPIKey = new System.Windows.Forms.TextBox();
            this.linkLabelSigniantRequestKey = new System.Windows.Forms.LinkLabel();
            this.linklabelSigniantMarket = new System.Windows.Forms.LinkLabel();
            this.checkBoxGenerateSigniant = new System.Windows.Forms.CheckBox();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.checkBoxGenerateAspera = new System.Windows.Forms.CheckBox();
            this.linkLabelAspera = new System.Windows.Forms.LinkLabel();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxManifestName = new System.Windows.Forms.TextBox();
            this.comboBoxStorageIngest = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridAssetFiles)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.panelSigniant.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.tabPage3.SuspendLayout();
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
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonOk);
            this.panel1.Name = "panel1";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.ForeColor = System.Drawing.Color.DarkBlue;
            this.label5.Name = "label5";
            // 
            // openFileDialogAssetFiles
            // 
            this.openFileDialogAssetFiles.Multiselect = true;
            // 
            // tabControl1
            // 
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.buttonSplitSelection);
            this.tabPage1.Controls.Add(this.buttonGroupSelectionInOneAsset);
            this.tabPage1.Controls.Add(this.buttonRemoveAll);
            this.tabPage1.Controls.Add(this.labelWarningFiles);
            this.tabPage1.Controls.Add(this.buttonSelectFolder);
            this.tabPage1.Controls.Add(this.buttonSelectFiles);
            this.tabPage1.Controls.Add(this.comboBoxStorageAsset);
            this.tabPage1.Controls.Add(this.label33);
            this.tabPage1.Controls.Add(this.buttonDelFiles);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.dataGridAssetFiles);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label1.Name = "label1";
            // 
            // buttonSplitSelection
            // 
            resources.ApplyResources(this.buttonSplitSelection, "buttonSplitSelection");
            this.buttonSplitSelection.Name = "buttonSplitSelection";
            this.buttonSplitSelection.UseVisualStyleBackColor = true;
            this.buttonSplitSelection.Click += new System.EventHandler(this.buttonSplitSelection_Click);
            // 
            // buttonGroupSelectionInOneAsset
            // 
            resources.ApplyResources(this.buttonGroupSelectionInOneAsset, "buttonGroupSelectionInOneAsset");
            this.buttonGroupSelectionInOneAsset.Name = "buttonGroupSelectionInOneAsset";
            this.buttonGroupSelectionInOneAsset.UseVisualStyleBackColor = true;
            this.buttonGroupSelectionInOneAsset.Click += new System.EventHandler(this.buttonGroupSelectionInOneAsset_Click);
            // 
            // buttonRemoveAll
            // 
            resources.ApplyResources(this.buttonRemoveAll, "buttonRemoveAll");
            this.buttonRemoveAll.Name = "buttonRemoveAll";
            this.buttonRemoveAll.UseVisualStyleBackColor = true;
            this.buttonRemoveAll.Click += new System.EventHandler(this.button1_Click);
            // 
            // labelWarningFiles
            // 
            resources.ApplyResources(this.labelWarningFiles, "labelWarningFiles");
            this.labelWarningFiles.ForeColor = System.Drawing.Color.Red;
            this.labelWarningFiles.Name = "labelWarningFiles";
            this.labelWarningFiles.Tag = "";
            // 
            // buttonSelectFolder
            // 
            resources.ApplyResources(this.buttonSelectFolder, "buttonSelectFolder");
            this.buttonSelectFolder.Name = "buttonSelectFolder";
            this.toolTip1.SetToolTip(this.buttonSelectFolder, resources.GetString("buttonSelectFolder.ToolTip"));
            this.buttonSelectFolder.UseVisualStyleBackColor = true;
            this.buttonSelectFolder.Click += new System.EventHandler(this.buttonSelectFolder_Click);
            // 
            // buttonSelectFiles
            // 
            resources.ApplyResources(this.buttonSelectFiles, "buttonSelectFiles");
            this.buttonSelectFiles.Name = "buttonSelectFiles";
            this.toolTip1.SetToolTip(this.buttonSelectFiles, resources.GetString("buttonSelectFiles.ToolTip"));
            this.buttonSelectFiles.UseVisualStyleBackColor = true;
            this.buttonSelectFiles.Click += new System.EventHandler(this.buttonSelectFiles_Click);
            // 
            // comboBoxStorageAsset
            // 
            resources.ApplyResources(this.comboBoxStorageAsset, "comboBoxStorageAsset");
            this.comboBoxStorageAsset.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStorageAsset.FormattingEnabled = true;
            this.comboBoxStorageAsset.Name = "comboBoxStorageAsset";
            // 
            // label33
            // 
            resources.ApplyResources(this.label33, "label33");
            this.label33.Name = "label33";
            // 
            // buttonDelFiles
            // 
            resources.ApplyResources(this.buttonDelFiles, "buttonDelFiles");
            this.buttonDelFiles.Name = "buttonDelFiles";
            this.buttonDelFiles.UseVisualStyleBackColor = true;
            this.buttonDelFiles.Click += new System.EventHandler(this.buttonDelFiles_Click);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // dataGridAssetFiles
            // 
            this.dataGridAssetFiles.AllowUserToAddRows = false;
            this.dataGridAssetFiles.AllowUserToDeleteRows = false;
            this.dataGridAssetFiles.AllowUserToResizeRows = false;
            resources.ApplyResources(this.dataGridAssetFiles, "dataGridAssetFiles");
            this.dataGridAssetFiles.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridAssetFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridAssetFiles.Name = "dataGridAssetFiles";
            this.dataGridAssetFiles.RowHeadersVisible = false;
            this.dataGridAssetFiles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridAssetFiles.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridAssetFiles_CellEndEdit);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.radioButtonCENCEncryption);
            this.tabPage2.Controls.Add(this.radioButtonStorageEncryption);
            this.tabPage2.Controls.Add(this.radioButtonEncryptionNone);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.buttonBrowseFile);
            this.tabPage2.Controls.Add(this.textBoxFolderPath);
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // radioButtonCENCEncryption
            // 
            resources.ApplyResources(this.radioButtonCENCEncryption, "radioButtonCENCEncryption");
            this.radioButtonCENCEncryption.Name = "radioButtonCENCEncryption";
            this.radioButtonCENCEncryption.TabStop = true;
            this.radioButtonCENCEncryption.UseVisualStyleBackColor = true;
            this.radioButtonCENCEncryption.CheckedChanged += new System.EventHandler(this.radioButtonStorageEncryption_CheckedChanged);
            // 
            // radioButtonStorageEncryption
            // 
            resources.ApplyResources(this.radioButtonStorageEncryption, "radioButtonStorageEncryption");
            this.radioButtonStorageEncryption.Name = "radioButtonStorageEncryption";
            this.radioButtonStorageEncryption.TabStop = true;
            this.radioButtonStorageEncryption.UseVisualStyleBackColor = true;
            this.radioButtonStorageEncryption.CheckedChanged += new System.EventHandler(this.radioButtonStorageEncryption_CheckedChanged);
            // 
            // radioButtonEncryptionNone
            // 
            resources.ApplyResources(this.radioButtonEncryptionNone, "radioButtonEncryptionNone");
            this.radioButtonEncryptionNone.Checked = true;
            this.radioButtonEncryptionNone.Name = "radioButtonEncryptionNone";
            this.radioButtonEncryptionNone.TabStop = true;
            this.radioButtonEncryptionNone.UseVisualStyleBackColor = true;
            this.radioButtonEncryptionNone.CheckedChanged += new System.EventHandler(this.radioButtonStorageEncryption_CheckedChanged);
            // 
            // label4
            // 
            this.label4.ForeColor = System.Drawing.SystemColors.WindowFrame;
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
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
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.linkLabelInfoAzCopy);
            this.tabPage5.Controls.Add(this.checkBoxGenerateAzCopy);
            resources.ApplyResources(this.tabPage5, "tabPage5");
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // linkLabelInfoAzCopy
            // 
            resources.ApplyResources(this.linkLabelInfoAzCopy, "linkLabelInfoAzCopy");
            this.linkLabelInfoAzCopy.Name = "linkLabelInfoAzCopy";
            this.linkLabelInfoAzCopy.TabStop = true;
            this.linkLabelInfoAzCopy.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linklabel_LinkClicked);
            // 
            // checkBoxGenerateAzCopy
            // 
            resources.ApplyResources(this.checkBoxGenerateAzCopy, "checkBoxGenerateAzCopy");
            this.checkBoxGenerateAzCopy.Name = "checkBoxGenerateAzCopy";
            this.checkBoxGenerateAzCopy.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.panelSigniant);
            this.tabPage4.Controls.Add(this.linkLabelSigniantRequestKey);
            this.tabPage4.Controls.Add(this.linklabelSigniantMarket);
            this.tabPage4.Controls.Add(this.checkBoxGenerateSigniant);
            resources.ApplyResources(this.tabPage4, "tabPage4");
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // panelSigniant
            // 
            resources.ApplyResources(this.panelSigniant, "panelSigniant");
            this.panelSigniant.Controls.Add(this.comboBoxSigniantServer);
            this.panelSigniant.Controls.Add(this.label8);
            this.panelSigniant.Controls.Add(this.label9);
            this.panelSigniant.Controls.Add(this.textBoxSigniantAPIKey);
            this.panelSigniant.Name = "panelSigniant";
            // 
            // comboBoxSigniantServer
            // 
            this.comboBoxSigniantServer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSigniantServer.FormattingEnabled = true;
            resources.ApplyResources(this.comboBoxSigniantServer, "comboBoxSigniantServer");
            this.comboBoxSigniantServer.Name = "comboBoxSigniantServer";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // textBoxSigniantAPIKey
            // 
            resources.ApplyResources(this.textBoxSigniantAPIKey, "textBoxSigniantAPIKey");
            this.textBoxSigniantAPIKey.Name = "textBoxSigniantAPIKey";
            // 
            // linkLabelSigniantRequestKey
            // 
            resources.ApplyResources(this.linkLabelSigniantRequestKey, "linkLabelSigniantRequestKey");
            this.linkLabelSigniantRequestKey.Name = "linkLabelSigniantRequestKey";
            this.linkLabelSigniantRequestKey.TabStop = true;
            this.linkLabelSigniantRequestKey.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linklabel_LinkClicked);
            // 
            // linklabelSigniantMarket
            // 
            resources.ApplyResources(this.linklabelSigniantMarket, "linklabelSigniantMarket");
            this.linklabelSigniantMarket.Name = "linklabelSigniantMarket";
            this.linklabelSigniantMarket.TabStop = true;
            this.linklabelSigniantMarket.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linklabel_LinkClicked);
            // 
            // checkBoxGenerateSigniant
            // 
            resources.ApplyResources(this.checkBoxGenerateSigniant, "checkBoxGenerateSigniant");
            this.checkBoxGenerateSigniant.Name = "checkBoxGenerateSigniant";
            this.checkBoxGenerateSigniant.UseVisualStyleBackColor = true;
            this.checkBoxGenerateSigniant.CheckedChanged += new System.EventHandler(this.checkBoxGenerateSigniant_CheckedChanged);
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.checkBoxGenerateAspera);
            this.tabPage6.Controls.Add(this.linkLabelAspera);
            resources.ApplyResources(this.tabPage6, "tabPage6");
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // checkBoxGenerateAspera
            // 
            resources.ApplyResources(this.checkBoxGenerateAspera, "checkBoxGenerateAspera");
            this.checkBoxGenerateAspera.Name = "checkBoxGenerateAspera";
            this.checkBoxGenerateAspera.UseVisualStyleBackColor = true;
            // 
            // linkLabelAspera
            // 
            resources.ApplyResources(this.linkLabelAspera, "linkLabelAspera");
            this.linkLabelAspera.Name = "linkLabelAspera";
            this.linkLabelAspera.TabStop = true;
            this.linkLabelAspera.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linklabel_LinkClicked);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.label6);
            resources.ApplyResources(this.tabPage3, "tabPage3");
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.ForeColor = System.Drawing.SystemColors.WindowFrame;
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // textBoxManifestName
            // 
            resources.ApplyResources(this.textBoxManifestName, "textBoxManifestName");
            this.textBoxManifestName.Name = "textBoxManifestName";
            // 
            // comboBoxStorageIngest
            // 
            resources.ApplyResources(this.comboBoxStorageIngest, "comboBoxStorageIngest");
            this.comboBoxStorageIngest.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStorageIngest.FormattingEnabled = true;
            this.comboBoxStorageIngest.Name = "comboBoxStorageIngest";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // BulkUpload
            // 
            this.AcceptButton = this.buttonOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.comboBoxStorageIngest);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxManifestName);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.panel1);
            this.Name = "BulkUpload";
            this.Load += new System.EventHandler(this.UploadBulk_Load);
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridAssetFiles)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.panelSigniant.ResumeLayout(false);
            this.panelSigniant.PerformLayout();
            this.tabPage6.ResumeLayout(false);
            this.tabPage6.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button buttonOk;
        public System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.OpenFileDialog openFileDialogAssetFiles;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button buttonSelectFiles;
        private System.Windows.Forms.ComboBox comboBoxStorageAsset;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Button buttonDelFiles;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridAssetFiles;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonBrowseFile;
        private System.Windows.Forms.TextBox textBoxFolderPath;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxManifestName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBoxStorageIngest;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button buttonSelectFolder;
        private System.Windows.Forms.Label labelWarningFiles;
        private System.Windows.Forms.Button buttonRemoveAll;
        private System.Windows.Forms.Button buttonGroupSelectionInOneAsset;
        private System.Windows.Forms.Button buttonSplitSelection;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox checkBoxGenerateSigniant;
        private System.Windows.Forms.ComboBox comboBoxSigniantServer;
        private System.Windows.Forms.TextBox textBoxSigniantAPIKey;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.CheckBox checkBoxGenerateAzCopy;
        private System.Windows.Forms.LinkLabel linkLabelSigniantRequestKey;
        private System.Windows.Forms.LinkLabel linklabelSigniantMarket;
        private System.Windows.Forms.LinkLabel linkLabelInfoAzCopy;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.LinkLabel linkLabelAspera;
        private System.Windows.Forms.CheckBox checkBoxGenerateAspera;
        private System.Windows.Forms.Panel panelSigniant;
        private System.Windows.Forms.RadioButton radioButtonStorageEncryption;
        private System.Windows.Forms.RadioButton radioButtonEncryptionNone;
        private System.Windows.Forms.RadioButton radioButtonCENCEncryption;
        private System.Windows.Forms.Label label10;
    }
}