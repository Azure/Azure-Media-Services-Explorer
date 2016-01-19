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
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.Location = new System.Drawing.Point(510, 14);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(143, 27);
            this.buttonOk.TabIndex = 4;
            this.buttonOk.Text = "Create Bulk Ingest";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(660, 14);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(112, 27);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonOk);
            this.panel1.Location = new System.Drawing.Point(-2, 507);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(787, 55);
            this.panel1.TabIndex = 60;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DarkBlue;
            this.label5.Location = new System.Drawing.Point(14, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(246, 20);
            this.label5.TabIndex = 72;
            this.label5.Text = "New bulk ingest for external upload";
            // 
            // openFileDialogAssetFiles
            // 
            this.openFileDialogAssetFiles.Multiselect = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 118);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(760, 383);
            this.tabControl1.TabIndex = 82;
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
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(752, 355);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Destination Assets";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label1.Location = new System.Drawing.Point(271, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(315, 15);
            this.label1.TabIndex = 87;
            this.label1.Text = "Asset Name can be edited in the grid";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // buttonSplitSelection
            // 
            this.buttonSplitSelection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSplitSelection.Location = new System.Drawing.Point(592, 106);
            this.buttonSplitSelection.Name = "buttonSplitSelection";
            this.buttonSplitSelection.Size = new System.Drawing.Size(143, 49);
            this.buttonSplitSelection.TabIndex = 86;
            this.buttonSplitSelection.Text = "Split selection to multiple assets";
            this.buttonSplitSelection.UseVisualStyleBackColor = true;
            this.buttonSplitSelection.Click += new System.EventHandler(this.buttonSplitSelection_Click);
            // 
            // buttonGroupSelectionInOneAsset
            // 
            this.buttonGroupSelectionInOneAsset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGroupSelectionInOneAsset.Location = new System.Drawing.Point(592, 161);
            this.buttonGroupSelectionInOneAsset.Name = "buttonGroupSelectionInOneAsset";
            this.buttonGroupSelectionInOneAsset.Size = new System.Drawing.Size(143, 49);
            this.buttonGroupSelectionInOneAsset.TabIndex = 85;
            this.buttonGroupSelectionInOneAsset.Text = "Group selection in a single asset";
            this.buttonGroupSelectionInOneAsset.UseVisualStyleBackColor = true;
            this.buttonGroupSelectionInOneAsset.Click += new System.EventHandler(this.buttonGroupSelectionInOneAsset_Click);
            // 
            // buttonRemoveAll
            // 
            this.buttonRemoveAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRemoveAll.Location = new System.Drawing.Point(592, 249);
            this.buttonRemoveAll.Name = "buttonRemoveAll";
            this.buttonRemoveAll.Size = new System.Drawing.Size(143, 27);
            this.buttonRemoveAll.TabIndex = 84;
            this.buttonRemoveAll.Text = "Remove all";
            this.buttonRemoveAll.UseVisualStyleBackColor = true;
            this.buttonRemoveAll.Click += new System.EventHandler(this.button1_Click);
            // 
            // labelWarningFiles
            // 
            this.labelWarningFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelWarningFiles.ForeColor = System.Drawing.Color.Red;
            this.labelWarningFiles.Location = new System.Drawing.Point(20, 279);
            this.labelWarningFiles.Name = "labelWarningFiles";
            this.labelWarningFiles.Size = new System.Drawing.Size(566, 15);
            this.labelWarningFiles.TabIndex = 83;
            this.labelWarningFiles.Tag = "";
            this.labelWarningFiles.Text = "Warning: two files have the same name";
            this.labelWarningFiles.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // buttonSelectFolder
            // 
            this.buttonSelectFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSelectFolder.Location = new System.Drawing.Point(592, 72);
            this.buttonSelectFolder.Name = "buttonSelectFolder";
            this.buttonSelectFolder.Size = new System.Drawing.Size(143, 27);
            this.buttonSelectFolder.TabIndex = 82;
            this.buttonSelectFolder.Text = "Select a folder...";
            this.toolTip1.SetToolTip(this.buttonSelectFolder, "The folder and each subfolder will be considered as a different asset");
            this.buttonSelectFolder.UseVisualStyleBackColor = true;
            this.buttonSelectFolder.Click += new System.EventHandler(this.buttonSelectFolder_Click);
            // 
            // buttonSelectFiles
            // 
            this.buttonSelectFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSelectFiles.Location = new System.Drawing.Point(592, 39);
            this.buttonSelectFiles.Name = "buttonSelectFiles";
            this.buttonSelectFiles.Size = new System.Drawing.Size(143, 27);
            this.buttonSelectFiles.TabIndex = 81;
            this.buttonSelectFiles.Text = "Select local file(s)...";
            this.toolTip1.SetToolTip(this.buttonSelectFiles, "Each file selected will be declared as a single asset");
            this.buttonSelectFiles.UseVisualStyleBackColor = true;
            this.buttonSelectFiles.Click += new System.EventHandler(this.buttonSelectFiles_Click);
            // 
            // comboBoxStorageAsset
            // 
            this.comboBoxStorageAsset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBoxStorageAsset.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStorageAsset.FormattingEnabled = true;
            this.comboBoxStorageAsset.Location = new System.Drawing.Point(20, 315);
            this.comboBoxStorageAsset.Name = "comboBoxStorageAsset";
            this.comboBoxStorageAsset.Size = new System.Drawing.Size(221, 23);
            this.comboBoxStorageAsset.TabIndex = 78;
            // 
            // label33
            // 
            this.label33.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(17, 297);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(53, 15);
            this.label33.TabIndex = 79;
            this.label33.Text = "Storage :";
            // 
            // buttonDelFiles
            // 
            this.buttonDelFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDelFiles.Location = new System.Drawing.Point(592, 216);
            this.buttonDelFiles.Name = "buttonDelFiles";
            this.buttonDelFiles.Size = new System.Drawing.Size(143, 27);
            this.buttonDelFiles.TabIndex = 76;
            this.buttonDelFiles.Text = "Remove selection";
            this.buttonDelFiles.UseVisualStyleBackColor = true;
            this.buttonDelFiles.Click += new System.EventHandler(this.buttonDelFiles_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(17, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 15);
            this.label2.TabIndex = 77;
            this.label2.Text = "List of files :";
            // 
            // dataGridAssetFiles
            // 
            this.dataGridAssetFiles.AllowUserToAddRows = false;
            this.dataGridAssetFiles.AllowUserToDeleteRows = false;
            this.dataGridAssetFiles.AllowUserToResizeRows = false;
            this.dataGridAssetFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridAssetFiles.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridAssetFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridAssetFiles.Location = new System.Drawing.Point(20, 39);
            this.dataGridAssetFiles.Name = "dataGridAssetFiles";
            this.dataGridAssetFiles.RowHeadersVisible = false;
            this.dataGridAssetFiles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridAssetFiles.Size = new System.Drawing.Size(566, 237);
            this.dataGridAssetFiles.TabIndex = 74;
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
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(752, 355);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Encryption";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(43, 100);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(394, 15);
            this.label10.TabIndex = 86;
            this.label10.Text = "Content will be encrypted locally to AES 256, before upload, to this folder:";
            // 
            // radioButtonCENCEncryption
            // 
            this.radioButtonCENCEncryption.AutoSize = true;
            this.radioButtonCENCEncryption.Location = new System.Drawing.Point(28, 214);
            this.radioButtonCENCEncryption.Name = "radioButtonCENCEncryption";
            this.radioButtonCENCEncryption.Size = new System.Drawing.Size(379, 19);
            this.radioButtonCENCEncryption.TabIndex = 85;
            this.radioButtonCENCEncryption.TabStop = true;
            this.radioButtonCENCEncryption.Text = "Common encryption (content is already encrypted with PlayReady)";
            this.radioButtonCENCEncryption.UseVisualStyleBackColor = true;
            this.radioButtonCENCEncryption.CheckedChanged += new System.EventHandler(this.radioButtonStorageEncryption_CheckedChanged);
            // 
            // radioButtonStorageEncryption
            // 
            this.radioButtonStorageEncryption.AutoSize = true;
            this.radioButtonStorageEncryption.Location = new System.Drawing.Point(28, 67);
            this.radioButtonStorageEncryption.Name = "radioButtonStorageEncryption";
            this.radioButtonStorageEncryption.Size = new System.Drawing.Size(125, 19);
            this.radioButtonStorageEncryption.TabIndex = 84;
            this.radioButtonStorageEncryption.TabStop = true;
            this.radioButtonStorageEncryption.Text = "Storage encryption";
            this.radioButtonStorageEncryption.UseVisualStyleBackColor = true;
            this.radioButtonStorageEncryption.CheckedChanged += new System.EventHandler(this.radioButtonStorageEncryption_CheckedChanged);
            // 
            // radioButtonEncryptionNone
            // 
            this.radioButtonEncryptionNone.AutoSize = true;
            this.radioButtonEncryptionNone.Checked = true;
            this.radioButtonEncryptionNone.Location = new System.Drawing.Point(28, 23);
            this.radioButtonEncryptionNone.Name = "radioButtonEncryptionNone";
            this.radioButtonEncryptionNone.Size = new System.Drawing.Size(216, 19);
            this.radioButtonEncryptionNone.TabIndex = 83;
            this.radioButtonEncryptionNone.TabStop = true;
            this.radioButtonEncryptionNone.Text = "None (no encryption before upload)";
            this.radioButtonEncryptionNone.UseVisualStyleBackColor = true;
            this.radioButtonEncryptionNone.CheckedChanged += new System.EventHandler(this.radioButtonStorageEncryption_CheckedChanged);
            // 
            // label4
            // 
            this.label4.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label4.Location = new System.Drawing.Point(43, 157);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(471, 15);
            this.label4.TabIndex = 82;
            this.label4.Text = "key(s) will be locally generated and copied to the target asset(s) in AMS";
            // 
            // buttonBrowseFile
            // 
            this.buttonBrowseFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBrowseFile.Enabled = false;
            this.buttonBrowseFile.Location = new System.Drawing.Point(645, 118);
            this.buttonBrowseFile.Name = "buttonBrowseFile";
            this.buttonBrowseFile.Size = new System.Drawing.Size(95, 23);
            this.buttonBrowseFile.TabIndex = 3;
            this.buttonBrowseFile.Text = "Browse...";
            this.buttonBrowseFile.UseVisualStyleBackColor = true;
            this.buttonBrowseFile.Click += new System.EventHandler(this.buttonBrowseFile_Click);
            // 
            // textBoxFolderPath
            // 
            this.textBoxFolderPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFolderPath.Enabled = false;
            this.textBoxFolderPath.Location = new System.Drawing.Point(46, 118);
            this.textBoxFolderPath.Name = "textBoxFolderPath";
            this.textBoxFolderPath.Size = new System.Drawing.Size(592, 23);
            this.textBoxFolderPath.TabIndex = 2;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.linkLabelInfoAzCopy);
            this.tabPage5.Controls.Add(this.checkBoxGenerateAzCopy);
            this.tabPage5.Location = new System.Drawing.Point(4, 24);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(752, 355);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Microsoft AzCopy";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // linkLabelInfoAzCopy
            // 
            this.linkLabelInfoAzCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabelInfoAzCopy.AutoSize = true;
            this.linkLabelInfoAzCopy.Location = new System.Drawing.Point(614, 24);
            this.linkLabelInfoAzCopy.Name = "linkLabelInfoAzCopy";
            this.linkLabelInfoAzCopy.Size = new System.Drawing.Size(120, 15);
            this.linkLabelInfoAzCopy.TabIndex = 84;
            this.linkLabelInfoAzCopy.TabStop = true;
            this.linkLabelInfoAzCopy.Text = "More info on AzCopy";
            this.linkLabelInfoAzCopy.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linklabel_LinkClicked);
            // 
            // checkBoxGenerateAzCopy
            // 
            this.checkBoxGenerateAzCopy.AutoSize = true;
            this.checkBoxGenerateAzCopy.Location = new System.Drawing.Point(29, 24);
            this.checkBoxGenerateAzCopy.Name = "checkBoxGenerateAzCopy";
            this.checkBoxGenerateAzCopy.Size = new System.Drawing.Size(197, 19);
            this.checkBoxGenerateAzCopy.TabIndex = 2;
            this.checkBoxGenerateAzCopy.Text = "Generate AzCopy command line";
            this.checkBoxGenerateAzCopy.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.panelSigniant);
            this.tabPage4.Controls.Add(this.linkLabelSigniantRequestKey);
            this.tabPage4.Controls.Add(this.linklabelSigniantMarket);
            this.tabPage4.Controls.Add(this.checkBoxGenerateSigniant);
            this.tabPage4.Location = new System.Drawing.Point(4, 24);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(752, 355);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Signiant Flight";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // panelSigniant
            // 
            this.panelSigniant.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelSigniant.Controls.Add(this.comboBoxSigniantServer);
            this.panelSigniant.Controls.Add(this.label8);
            this.panelSigniant.Controls.Add(this.label9);
            this.panelSigniant.Controls.Add(this.textBoxSigniantAPIKey);
            this.panelSigniant.Enabled = false;
            this.panelSigniant.Location = new System.Drawing.Point(6, 66);
            this.panelSigniant.Name = "panelSigniant";
            this.panelSigniant.Size = new System.Drawing.Size(740, 161);
            this.panelSigniant.TabIndex = 85;
            // 
            // comboBoxSigniantServer
            // 
            this.comboBoxSigniantServer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSigniantServer.FormattingEnabled = true;
            this.comboBoxSigniantServer.Location = new System.Drawing.Point(18, 21);
            this.comboBoxSigniantServer.Name = "comboBoxSigniantServer";
            this.comboBoxSigniantServer.Size = new System.Drawing.Size(247, 23);
            this.comboBoxSigniantServer.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(15, 3);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(382, 15);
            this.label8.TabIndex = 80;
            this.label8.Text = "Select the Signiant region (should be the same than the AMS account) :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(15, 64);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 15);
            this.label9.TabIndex = 81;
            this.label9.Text = "API Key :";
            // 
            // textBoxSigniantAPIKey
            // 
            this.textBoxSigniantAPIKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSigniantAPIKey.Location = new System.Drawing.Point(18, 82);
            this.textBoxSigniantAPIKey.Name = "textBoxSigniantAPIKey";
            this.textBoxSigniantAPIKey.Size = new System.Drawing.Size(553, 23);
            this.textBoxSigniantAPIKey.TabIndex = 82;
            // 
            // linkLabelSigniantRequestKey
            // 
            this.linkLabelSigniantRequestKey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabelSigniantRequestKey.AutoSize = true;
            this.linkLabelSigniantRequestKey.Location = new System.Drawing.Point(632, 48);
            this.linkLabelSigniantRequestKey.Name = "linkLabelSigniantRequestKey";
            this.linkLabelSigniantRequestKey.Size = new System.Drawing.Size(102, 15);
            this.linkLabelSigniantRequestKey.TabIndex = 84;
            this.linkLabelSigniantRequestKey.TabStop = true;
            this.linkLabelSigniantRequestKey.Text = "Request a trial key";
            this.linkLabelSigniantRequestKey.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linklabel_LinkClicked);
            // 
            // linklabelSigniantMarket
            // 
            this.linklabelSigniantMarket.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linklabelSigniantMarket.AutoSize = true;
            this.linklabelSigniantMarket.Location = new System.Drawing.Point(533, 22);
            this.linklabelSigniantMarket.Name = "linklabelSigniantMarket";
            this.linklabelSigniantMarket.Size = new System.Drawing.Size(201, 15);
            this.linklabelSigniantMarket.TabIndex = 83;
            this.linklabelSigniantMarket.TabStop = true;
            this.linklabelSigniantMarket.Text = "Signiant Flight on Azure Marketplace";
            this.linklabelSigniantMarket.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linklabel_LinkClicked);
            // 
            // checkBoxGenerateSigniant
            // 
            this.checkBoxGenerateSigniant.AutoSize = true;
            this.checkBoxGenerateSigniant.Location = new System.Drawing.Point(24, 22);
            this.checkBoxGenerateSigniant.Name = "checkBoxGenerateSigniant";
            this.checkBoxGenerateSigniant.Size = new System.Drawing.Size(257, 19);
            this.checkBoxGenerateSigniant.TabIndex = 1;
            this.checkBoxGenerateSigniant.Text = "Generate Signiant (sigcli.exe) command line";
            this.checkBoxGenerateSigniant.UseVisualStyleBackColor = true;
            this.checkBoxGenerateSigniant.CheckedChanged += new System.EventHandler(this.checkBoxGenerateSigniant_CheckedChanged);
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.checkBoxGenerateAspera);
            this.tabPage6.Controls.Add(this.linkLabelAspera);
            this.tabPage6.Location = new System.Drawing.Point(4, 24);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Size = new System.Drawing.Size(752, 355);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "Aspera SOD";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // checkBoxGenerateAspera
            // 
            this.checkBoxGenerateAspera.AutoSize = true;
            this.checkBoxGenerateAspera.Location = new System.Drawing.Point(29, 22);
            this.checkBoxGenerateAspera.Name = "checkBoxGenerateAspera";
            this.checkBoxGenerateAspera.Size = new System.Drawing.Size(136, 19);
            this.checkBoxGenerateAspera.TabIndex = 85;
            this.checkBoxGenerateAspera.Text = "Generate Aspera URL";
            this.checkBoxGenerateAspera.UseVisualStyleBackColor = true;
            // 
            // linkLabelAspera
            // 
            this.linkLabelAspera.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabelAspera.AutoSize = true;
            this.linkLabelAspera.Location = new System.Drawing.Point(546, 22);
            this.linkLabelAspera.Name = "linkLabelAspera";
            this.linkLabelAspera.Size = new System.Drawing.Size(187, 15);
            this.linkLabelAspera.TabIndex = 84;
            this.linkLabelAspera.TabStop = true;
            this.linkLabelAspera.Text = "Aspera SOD on Azure Marketplace";
            this.linkLabelAspera.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linklabel_LinkClicked);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.label6);
            this.tabPage3.Location = new System.Drawing.Point(4, 24);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(752, 355);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Help";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label6.Location = new System.Drawing.Point(19, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(710, 187);
            this.label6.TabIndex = 77;
            this.label6.Text = resources.GetString("label6.Text");
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(15, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(229, 15);
            this.label3.TabIndex = 84;
            this.label3.Text = "Bulk ingest container name to create :";
            // 
            // textBoxManifestName
            // 
            this.textBoxManifestName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxManifestName.Location = new System.Drawing.Point(16, 71);
            this.textBoxManifestName.Name = "textBoxManifestName";
            this.textBoxManifestName.Size = new System.Drawing.Size(525, 23);
            this.textBoxManifestName.TabIndex = 0;
            // 
            // comboBoxStorageIngest
            // 
            this.comboBoxStorageIngest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxStorageIngest.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStorageIngest.FormattingEnabled = true;
            this.comboBoxStorageIngest.Location = new System.Drawing.Point(547, 71);
            this.comboBoxStorageIngest.Name = "comboBoxStorageIngest";
            this.comboBoxStorageIngest.Size = new System.Drawing.Size(221, 23);
            this.comboBoxStorageIngest.TabIndex = 82;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(544, 53);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 15);
            this.label7.TabIndex = 83;
            this.label7.Text = "Storage :";
            // 
            // BulkUpload
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.comboBoxStorageIngest);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxManifestName);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "BulkUpload";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New bulk Ingest";
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