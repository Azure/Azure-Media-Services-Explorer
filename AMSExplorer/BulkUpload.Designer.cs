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
            this.toolTip1.SetToolTip(this.buttonOk, resources.GetString("buttonOk.ToolTip"));
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Name = "buttonCancel";
            this.toolTip1.SetToolTip(this.buttonCancel, resources.GetString("buttonCancel.ToolTip"));
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
            this.toolTip1.SetToolTip(this.panel1, resources.GetString("panel1.ToolTip"));
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.ForeColor = System.Drawing.Color.DarkBlue;
            this.label5.Name = "label5";
            this.toolTip1.SetToolTip(this.label5, resources.GetString("label5.ToolTip"));
            // 
            // openFileDialogAssetFiles
            // 
            resources.ApplyResources(this.openFileDialogAssetFiles, "openFileDialogAssetFiles");
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
            this.toolTip1.SetToolTip(this.tabControl1, resources.GetString("tabControl1.ToolTip"));
            // 
            // tabPage1
            // 
            resources.ApplyResources(this.tabPage1, "tabPage1");
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
            this.tabPage1.Name = "tabPage1";
            this.toolTip1.SetToolTip(this.tabPage1, resources.GetString("tabPage1.ToolTip"));
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label1.Name = "label1";
            this.toolTip1.SetToolTip(this.label1, resources.GetString("label1.ToolTip"));
            // 
            // buttonSplitSelection
            // 
            resources.ApplyResources(this.buttonSplitSelection, "buttonSplitSelection");
            this.buttonSplitSelection.Name = "buttonSplitSelection";
            this.toolTip1.SetToolTip(this.buttonSplitSelection, resources.GetString("buttonSplitSelection.ToolTip"));
            this.buttonSplitSelection.UseVisualStyleBackColor = true;
            this.buttonSplitSelection.Click += new System.EventHandler(this.buttonSplitSelection_Click);
            // 
            // buttonGroupSelectionInOneAsset
            // 
            resources.ApplyResources(this.buttonGroupSelectionInOneAsset, "buttonGroupSelectionInOneAsset");
            this.buttonGroupSelectionInOneAsset.Name = "buttonGroupSelectionInOneAsset";
            this.toolTip1.SetToolTip(this.buttonGroupSelectionInOneAsset, resources.GetString("buttonGroupSelectionInOneAsset.ToolTip"));
            this.buttonGroupSelectionInOneAsset.UseVisualStyleBackColor = true;
            this.buttonGroupSelectionInOneAsset.Click += new System.EventHandler(this.buttonGroupSelectionInOneAsset_Click);
            // 
            // buttonRemoveAll
            // 
            resources.ApplyResources(this.buttonRemoveAll, "buttonRemoveAll");
            this.buttonRemoveAll.Name = "buttonRemoveAll";
            this.toolTip1.SetToolTip(this.buttonRemoveAll, resources.GetString("buttonRemoveAll.ToolTip"));
            this.buttonRemoveAll.UseVisualStyleBackColor = true;
            this.buttonRemoveAll.Click += new System.EventHandler(this.button1_Click);
            // 
            // labelWarningFiles
            // 
            resources.ApplyResources(this.labelWarningFiles, "labelWarningFiles");
            this.labelWarningFiles.ForeColor = System.Drawing.Color.Red;
            this.labelWarningFiles.Name = "labelWarningFiles";
            this.labelWarningFiles.Tag = "";
            this.toolTip1.SetToolTip(this.labelWarningFiles, resources.GetString("labelWarningFiles.ToolTip"));
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
            this.toolTip1.SetToolTip(this.comboBoxStorageAsset, resources.GetString("comboBoxStorageAsset.ToolTip"));
            // 
            // label33
            // 
            resources.ApplyResources(this.label33, "label33");
            this.label33.Name = "label33";
            this.toolTip1.SetToolTip(this.label33, resources.GetString("label33.ToolTip"));
            // 
            // buttonDelFiles
            // 
            resources.ApplyResources(this.buttonDelFiles, "buttonDelFiles");
            this.buttonDelFiles.Name = "buttonDelFiles";
            this.toolTip1.SetToolTip(this.buttonDelFiles, resources.GetString("buttonDelFiles.ToolTip"));
            this.buttonDelFiles.UseVisualStyleBackColor = true;
            this.buttonDelFiles.Click += new System.EventHandler(this.buttonDelFiles_Click);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            this.toolTip1.SetToolTip(this.label2, resources.GetString("label2.ToolTip"));
            // 
            // dataGridAssetFiles
            // 
            resources.ApplyResources(this.dataGridAssetFiles, "dataGridAssetFiles");
            this.dataGridAssetFiles.AllowUserToAddRows = false;
            this.dataGridAssetFiles.AllowUserToDeleteRows = false;
            this.dataGridAssetFiles.AllowUserToResizeRows = false;
            this.dataGridAssetFiles.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridAssetFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridAssetFiles.Name = "dataGridAssetFiles";
            this.dataGridAssetFiles.RowHeadersVisible = false;
            this.dataGridAssetFiles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.toolTip1.SetToolTip(this.dataGridAssetFiles, resources.GetString("dataGridAssetFiles.ToolTip"));
            this.dataGridAssetFiles.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridAssetFiles_CellEndEdit);
            // 
            // tabPage2
            // 
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.radioButtonCENCEncryption);
            this.tabPage2.Controls.Add(this.radioButtonStorageEncryption);
            this.tabPage2.Controls.Add(this.radioButtonEncryptionNone);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.buttonBrowseFile);
            this.tabPage2.Controls.Add(this.textBoxFolderPath);
            this.tabPage2.Name = "tabPage2";
            this.toolTip1.SetToolTip(this.tabPage2, resources.GetString("tabPage2.ToolTip"));
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            this.toolTip1.SetToolTip(this.label10, resources.GetString("label10.ToolTip"));
            // 
            // radioButtonCENCEncryption
            // 
            resources.ApplyResources(this.radioButtonCENCEncryption, "radioButtonCENCEncryption");
            this.radioButtonCENCEncryption.Name = "radioButtonCENCEncryption";
            this.radioButtonCENCEncryption.TabStop = true;
            this.toolTip1.SetToolTip(this.radioButtonCENCEncryption, resources.GetString("radioButtonCENCEncryption.ToolTip"));
            this.radioButtonCENCEncryption.UseVisualStyleBackColor = true;
            this.radioButtonCENCEncryption.CheckedChanged += new System.EventHandler(this.radioButtonStorageEncryption_CheckedChanged);
            // 
            // radioButtonStorageEncryption
            // 
            resources.ApplyResources(this.radioButtonStorageEncryption, "radioButtonStorageEncryption");
            this.radioButtonStorageEncryption.Name = "radioButtonStorageEncryption";
            this.radioButtonStorageEncryption.TabStop = true;
            this.toolTip1.SetToolTip(this.radioButtonStorageEncryption, resources.GetString("radioButtonStorageEncryption.ToolTip"));
            this.radioButtonStorageEncryption.UseVisualStyleBackColor = true;
            this.radioButtonStorageEncryption.CheckedChanged += new System.EventHandler(this.radioButtonStorageEncryption_CheckedChanged);
            // 
            // radioButtonEncryptionNone
            // 
            resources.ApplyResources(this.radioButtonEncryptionNone, "radioButtonEncryptionNone");
            this.radioButtonEncryptionNone.Checked = true;
            this.radioButtonEncryptionNone.Name = "radioButtonEncryptionNone";
            this.radioButtonEncryptionNone.TabStop = true;
            this.toolTip1.SetToolTip(this.radioButtonEncryptionNone, resources.GetString("radioButtonEncryptionNone.ToolTip"));
            this.radioButtonEncryptionNone.UseVisualStyleBackColor = true;
            this.radioButtonEncryptionNone.CheckedChanged += new System.EventHandler(this.radioButtonStorageEncryption_CheckedChanged);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label4.Name = "label4";
            this.toolTip1.SetToolTip(this.label4, resources.GetString("label4.ToolTip"));
            // 
            // buttonBrowseFile
            // 
            resources.ApplyResources(this.buttonBrowseFile, "buttonBrowseFile");
            this.buttonBrowseFile.Name = "buttonBrowseFile";
            this.toolTip1.SetToolTip(this.buttonBrowseFile, resources.GetString("buttonBrowseFile.ToolTip"));
            this.buttonBrowseFile.UseVisualStyleBackColor = true;
            this.buttonBrowseFile.Click += new System.EventHandler(this.buttonBrowseFile_Click);
            // 
            // textBoxFolderPath
            // 
            resources.ApplyResources(this.textBoxFolderPath, "textBoxFolderPath");
            this.textBoxFolderPath.Name = "textBoxFolderPath";
            this.toolTip1.SetToolTip(this.textBoxFolderPath, resources.GetString("textBoxFolderPath.ToolTip"));
            // 
            // tabPage5
            // 
            resources.ApplyResources(this.tabPage5, "tabPage5");
            this.tabPage5.Controls.Add(this.linkLabelInfoAzCopy);
            this.tabPage5.Controls.Add(this.checkBoxGenerateAzCopy);
            this.tabPage5.Name = "tabPage5";
            this.toolTip1.SetToolTip(this.tabPage5, resources.GetString("tabPage5.ToolTip"));
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // linkLabelInfoAzCopy
            // 
            resources.ApplyResources(this.linkLabelInfoAzCopy, "linkLabelInfoAzCopy");
            this.linkLabelInfoAzCopy.Name = "linkLabelInfoAzCopy";
            this.linkLabelInfoAzCopy.TabStop = true;
            this.toolTip1.SetToolTip(this.linkLabelInfoAzCopy, resources.GetString("linkLabelInfoAzCopy.ToolTip"));
            this.linkLabelInfoAzCopy.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linklabel_LinkClicked);
            // 
            // checkBoxGenerateAzCopy
            // 
            resources.ApplyResources(this.checkBoxGenerateAzCopy, "checkBoxGenerateAzCopy");
            this.checkBoxGenerateAzCopy.Name = "checkBoxGenerateAzCopy";
            this.toolTip1.SetToolTip(this.checkBoxGenerateAzCopy, resources.GetString("checkBoxGenerateAzCopy.ToolTip"));
            this.checkBoxGenerateAzCopy.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            resources.ApplyResources(this.tabPage4, "tabPage4");
            this.tabPage4.Controls.Add(this.panelSigniant);
            this.tabPage4.Controls.Add(this.linkLabelSigniantRequestKey);
            this.tabPage4.Controls.Add(this.linklabelSigniantMarket);
            this.tabPage4.Controls.Add(this.checkBoxGenerateSigniant);
            this.tabPage4.Name = "tabPage4";
            this.toolTip1.SetToolTip(this.tabPage4, resources.GetString("tabPage4.ToolTip"));
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
            this.toolTip1.SetToolTip(this.panelSigniant, resources.GetString("panelSigniant.ToolTip"));
            // 
            // comboBoxSigniantServer
            // 
            resources.ApplyResources(this.comboBoxSigniantServer, "comboBoxSigniantServer");
            this.comboBoxSigniantServer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSigniantServer.FormattingEnabled = true;
            this.comboBoxSigniantServer.Name = "comboBoxSigniantServer";
            this.toolTip1.SetToolTip(this.comboBoxSigniantServer, resources.GetString("comboBoxSigniantServer.ToolTip"));
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            this.toolTip1.SetToolTip(this.label8, resources.GetString("label8.ToolTip"));
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            this.toolTip1.SetToolTip(this.label9, resources.GetString("label9.ToolTip"));
            // 
            // textBoxSigniantAPIKey
            // 
            resources.ApplyResources(this.textBoxSigniantAPIKey, "textBoxSigniantAPIKey");
            this.textBoxSigniantAPIKey.Name = "textBoxSigniantAPIKey";
            this.toolTip1.SetToolTip(this.textBoxSigniantAPIKey, resources.GetString("textBoxSigniantAPIKey.ToolTip"));
            // 
            // linkLabelSigniantRequestKey
            // 
            resources.ApplyResources(this.linkLabelSigniantRequestKey, "linkLabelSigniantRequestKey");
            this.linkLabelSigniantRequestKey.Name = "linkLabelSigniantRequestKey";
            this.linkLabelSigniantRequestKey.TabStop = true;
            this.toolTip1.SetToolTip(this.linkLabelSigniantRequestKey, resources.GetString("linkLabelSigniantRequestKey.ToolTip"));
            this.linkLabelSigniantRequestKey.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linklabel_LinkClicked);
            // 
            // linklabelSigniantMarket
            // 
            resources.ApplyResources(this.linklabelSigniantMarket, "linklabelSigniantMarket");
            this.linklabelSigniantMarket.Name = "linklabelSigniantMarket";
            this.linklabelSigniantMarket.TabStop = true;
            this.toolTip1.SetToolTip(this.linklabelSigniantMarket, resources.GetString("linklabelSigniantMarket.ToolTip"));
            this.linklabelSigniantMarket.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linklabel_LinkClicked);
            // 
            // checkBoxGenerateSigniant
            // 
            resources.ApplyResources(this.checkBoxGenerateSigniant, "checkBoxGenerateSigniant");
            this.checkBoxGenerateSigniant.Name = "checkBoxGenerateSigniant";
            this.toolTip1.SetToolTip(this.checkBoxGenerateSigniant, resources.GetString("checkBoxGenerateSigniant.ToolTip"));
            this.checkBoxGenerateSigniant.UseVisualStyleBackColor = true;
            this.checkBoxGenerateSigniant.CheckedChanged += new System.EventHandler(this.checkBoxGenerateSigniant_CheckedChanged);
            // 
            // tabPage6
            // 
            resources.ApplyResources(this.tabPage6, "tabPage6");
            this.tabPage6.Controls.Add(this.checkBoxGenerateAspera);
            this.tabPage6.Controls.Add(this.linkLabelAspera);
            this.tabPage6.Name = "tabPage6";
            this.toolTip1.SetToolTip(this.tabPage6, resources.GetString("tabPage6.ToolTip"));
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // checkBoxGenerateAspera
            // 
            resources.ApplyResources(this.checkBoxGenerateAspera, "checkBoxGenerateAspera");
            this.checkBoxGenerateAspera.Name = "checkBoxGenerateAspera";
            this.toolTip1.SetToolTip(this.checkBoxGenerateAspera, resources.GetString("checkBoxGenerateAspera.ToolTip"));
            this.checkBoxGenerateAspera.UseVisualStyleBackColor = true;
            // 
            // linkLabelAspera
            // 
            resources.ApplyResources(this.linkLabelAspera, "linkLabelAspera");
            this.linkLabelAspera.Name = "linkLabelAspera";
            this.linkLabelAspera.TabStop = true;
            this.toolTip1.SetToolTip(this.linkLabelAspera, resources.GetString("linkLabelAspera.ToolTip"));
            this.linkLabelAspera.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linklabel_LinkClicked);
            // 
            // tabPage3
            // 
            resources.ApplyResources(this.tabPage3, "tabPage3");
            this.tabPage3.Controls.Add(this.label6);
            this.tabPage3.Name = "tabPage3";
            this.toolTip1.SetToolTip(this.tabPage3, resources.GetString("tabPage3.ToolTip"));
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label6.Name = "label6";
            this.toolTip1.SetToolTip(this.label6, resources.GetString("label6.ToolTip"));
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            this.toolTip1.SetToolTip(this.label3, resources.GetString("label3.ToolTip"));
            // 
            // textBoxManifestName
            // 
            resources.ApplyResources(this.textBoxManifestName, "textBoxManifestName");
            this.textBoxManifestName.Name = "textBoxManifestName";
            this.toolTip1.SetToolTip(this.textBoxManifestName, resources.GetString("textBoxManifestName.ToolTip"));
            // 
            // comboBoxStorageIngest
            // 
            resources.ApplyResources(this.comboBoxStorageIngest, "comboBoxStorageIngest");
            this.comboBoxStorageIngest.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStorageIngest.FormattingEnabled = true;
            this.comboBoxStorageIngest.Name = "comboBoxStorageIngest";
            this.toolTip1.SetToolTip(this.comboBoxStorageIngest, resources.GetString("comboBoxStorageIngest.ToolTip"));
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            this.toolTip1.SetToolTip(this.label7, resources.GetString("label7.ToolTip"));
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
            this.toolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
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