namespace AMSExplorer
{
    partial class AMSLogin
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
            this.buttonLogin = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.listBoxAcounts = new System.Windows.Forms.ListBox();
            this.buttonSaveToList = new System.Windows.Forms.Button();
            this.buttonDeleteAccountEntry = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.textBoxAccountID = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxBlobKey = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxAccountKey = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxAccountName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.textBoxManagementPortal = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.comboBoxMappingList = new System.Windows.Forms.ComboBox();
            this.buttonAddMapping = new System.Windows.Forms.Button();
            this.textBoxAzureEndpoint = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxACSBaseAddress = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxScope = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxAPIServer = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.radioButtonOther = new System.Windows.Forms.RadioButton();
            this.radioButtonPartner = new System.Windows.Forms.RadioButton();
            this.radioButtonProd = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonExport = new System.Windows.Forms.Button();
            this.buttonImportAll = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.accountmgtlink = new System.Windows.Forms.LinkLabel();
            this.pictureBoxJob = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelVersion = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxJob)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonLogin
            // 
            this.buttonLogin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonLogin.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.buttonLogin.Location = new System.Drawing.Point(554, 15);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(105, 27);
            this.buttonLogin.TabIndex = 4;
            this.buttonLogin.Text = "Connect";
            this.buttonLogin.UseVisualStyleBackColor = true;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.buttonCancel.Location = new System.Drawing.Point(666, 15);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(105, 27);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // listBoxAcounts
            // 
            this.listBoxAcounts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxAcounts.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.listBoxAcounts.FormattingEnabled = true;
            this.listBoxAcounts.ItemHeight = 15;
            this.listBoxAcounts.Location = new System.Drawing.Point(17, 50);
            this.listBoxAcounts.Name = "listBoxAcounts";
            this.listBoxAcounts.Size = new System.Drawing.Size(255, 349);
            this.listBoxAcounts.TabIndex = 10;
            this.listBoxAcounts.SelectedIndexChanged += new System.EventHandler(this.listBoxAccounts_SelectedIndexChanged);
            // 
            // buttonSaveToList
            // 
            this.buttonSaveToList.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.buttonSaveToList.Location = new System.Drawing.Point(316, 407);
            this.buttonSaveToList.Name = "buttonSaveToList";
            this.buttonSaveToList.Size = new System.Drawing.Size(142, 27);
            this.buttonSaveToList.TabIndex = 14;
            this.buttonSaveToList.Text = "<-- Save to the list";
            this.toolTip1.SetToolTip(this.buttonSaveToList, "Credentials are saved in clear in your user profile. Use Bitlocker or do not save" +
        " them if your PC is unsecured.");
            this.buttonSaveToList.UseVisualStyleBackColor = true;
            this.buttonSaveToList.Click += new System.EventHandler(this.buttonSaveToList_Click);
            // 
            // buttonDeleteAccountEntry
            // 
            this.buttonDeleteAccountEntry.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDeleteAccountEntry.Enabled = false;
            this.buttonDeleteAccountEntry.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.buttonDeleteAccountEntry.Location = new System.Drawing.Point(17, 407);
            this.buttonDeleteAccountEntry.Name = "buttonDeleteAccountEntry";
            this.buttonDeleteAccountEntry.Size = new System.Drawing.Size(87, 27);
            this.buttonDeleteAccountEntry.TabIndex = 15;
            this.buttonDeleteAccountEntry.Text = "Delete entry";
            this.buttonDeleteAccountEntry.UseVisualStyleBackColor = true;
            this.buttonDeleteAccountEntry.Click += new System.EventHandler(this.buttonDeleteAccount_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.buttonClear.Location = new System.Drawing.Point(464, 407);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(120, 27);
            this.buttonClear.TabIndex = 19;
            this.buttonClear.Text = "Clear fields";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tabControl1.Location = new System.Drawing.Point(292, 51);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(467, 348);
            this.tabControl1.TabIndex = 20;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Window;
            this.tabPage1.Controls.Add(this.textBoxAccountID);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.textBoxDescription);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.textBoxBlobKey);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.textBoxAccountKey);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.textBoxAccountName);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(459, 320);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Credentials";
            // 
            // textBoxAccountID
            // 
            this.textBoxAccountID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxAccountID.Location = new System.Drawing.Point(20, 221);
            this.textBoxAccountID.Name = "textBoxAccountID";
            this.textBoxAccountID.Size = new System.Drawing.Size(417, 23);
            this.textBoxAccountID.TabIndex = 37;
            this.textBoxAccountID.Validating += new System.ComponentModel.CancelEventHandler(this.CheckTextBoxGuid);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(17, 203);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(273, 15);
            this.label11.TabIndex = 38;
            this.label11.Text = "Media Service Account ID (optional, for Telemetry)";
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDescription.Location = new System.Drawing.Point(20, 280);
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(417, 23);
            this.textBoxDescription.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 262);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 15);
            this.label2.TabIndex = 36;
            this.label2.Text = "Description (optional)";
            // 
            // textBoxBlobKey
            // 
            this.textBoxBlobKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxBlobKey.Location = new System.Drawing.Point(20, 162);
            this.textBoxBlobKey.Name = "textBoxBlobKey";
            this.textBoxBlobKey.Size = new System.Drawing.Size(417, 23);
            this.textBoxBlobKey.TabIndex = 2;
            this.textBoxBlobKey.UseSystemPasswordChar = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 144);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(408, 15);
            this.label3.TabIndex = 34;
            this.label3.Text = "Default Storage Account Access Key (optional, for Azure Storage operations)";
            // 
            // textBoxAccountKey
            // 
            this.textBoxAccountKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxAccountKey.Location = new System.Drawing.Point(20, 103);
            this.textBoxAccountKey.Name = "textBoxAccountKey";
            this.textBoxAccountKey.Size = new System.Drawing.Size(417, 23);
            this.textBoxAccountKey.TabIndex = 1;
            this.textBoxAccountKey.UseSystemPasswordChar = true;
            this.textBoxAccountKey.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxAccountKey_Validating);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(150, 15);
            this.label4.TabIndex = 32;
            this.label4.Text = "Media Service Account Key";
            // 
            // textBoxAccountName
            // 
            this.textBoxAccountName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxAccountName.Location = new System.Drawing.Point(20, 44);
            this.textBoxAccountName.Name = "textBoxAccountName";
            this.textBoxAccountName.Size = new System.Drawing.Size(417, 23);
            this.textBoxAccountName.TabIndex = 0;
            this.textBoxAccountName.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxAccountName_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(163, 15);
            this.label1.TabIndex = 30;
            this.label1.Text = "Media Service Account Name";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Window;
            this.tabPage2.Controls.Add(this.textBoxManagementPortal);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.comboBoxMappingList);
            this.tabPage2.Controls.Add(this.buttonAddMapping);
            this.tabPage2.Controls.Add(this.textBoxAzureEndpoint);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.textBoxACSBaseAddress);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.textBoxScope);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.textBoxAPIServer);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.radioButtonOther);
            this.tabPage2.Controls.Add(this.radioButtonPartner);
            this.tabPage2.Controls.Add(this.radioButtonProd);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(459, 320);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Endpoint";
            // 
            // textBoxManagementPortal
            // 
            this.textBoxManagementPortal.BackColor = System.Drawing.Color.Pink;
            this.textBoxManagementPortal.Enabled = false;
            this.textBoxManagementPortal.Location = new System.Drawing.Point(36, 291);
            this.textBoxManagementPortal.Name = "textBoxManagementPortal";
            this.textBoxManagementPortal.Size = new System.Drawing.Size(409, 23);
            this.textBoxManagementPortal.TabIndex = 51;
            this.textBoxManagementPortal.TextChanged += new System.EventHandler(this.textBoxURL_Validation);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(37, 273);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(112, 15);
            this.label10.TabIndex = 50;
            this.label10.Text = "Management Portal";
            // 
            // comboBoxMappingList
            // 
            this.comboBoxMappingList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMappingList.Enabled = false;
            this.comboBoxMappingList.FormattingEnabled = true;
            this.comboBoxMappingList.Location = new System.Drawing.Point(160, 72);
            this.comboBoxMappingList.Name = "comboBoxMappingList";
            this.comboBoxMappingList.Size = new System.Drawing.Size(175, 23);
            this.comboBoxMappingList.TabIndex = 49;
            // 
            // buttonAddMapping
            // 
            this.buttonAddMapping.Enabled = false;
            this.buttonAddMapping.Location = new System.Drawing.Point(341, 72);
            this.buttonAddMapping.Name = "buttonAddMapping";
            this.buttonAddMapping.Size = new System.Drawing.Size(104, 23);
            this.buttonAddMapping.TabIndex = 48;
            this.buttonAddMapping.Text = "Insert settings";
            this.buttonAddMapping.UseVisualStyleBackColor = true;
            this.buttonAddMapping.Click += new System.EventHandler(this.buttonAddMapping_Click);
            // 
            // textBoxAzureEndpoint
            // 
            this.textBoxAzureEndpoint.BackColor = System.Drawing.Color.Pink;
            this.textBoxAzureEndpoint.Enabled = false;
            this.textBoxAzureEndpoint.Location = new System.Drawing.Point(36, 246);
            this.textBoxAzureEndpoint.Name = "textBoxAzureEndpoint";
            this.textBoxAzureEndpoint.Size = new System.Drawing.Size(409, 23);
            this.textBoxAzureEndpoint.TabIndex = 43;
            this.textBoxAzureEndpoint.TextChanged += new System.EventHandler(this.textBoxTXT_Validation);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(37, 228);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(88, 15);
            this.label9.TabIndex = 42;
            this.label9.Text = "Azure Endpoint";
            // 
            // textBoxACSBaseAddress
            // 
            this.textBoxACSBaseAddress.BackColor = System.Drawing.Color.Pink;
            this.textBoxACSBaseAddress.Enabled = false;
            this.textBoxACSBaseAddress.Location = new System.Drawing.Point(36, 201);
            this.textBoxACSBaseAddress.Name = "textBoxACSBaseAddress";
            this.textBoxACSBaseAddress.Size = new System.Drawing.Size(409, 23);
            this.textBoxACSBaseAddress.TabIndex = 41;
            this.textBoxACSBaseAddress.TextChanged += new System.EventHandler(this.textBoxURL_Validation);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(37, 183);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(134, 15);
            this.label6.TabIndex = 40;
            this.label6.Text = "Azure ACS Base Address";
            // 
            // textBoxScope
            // 
            this.textBoxScope.BackColor = System.Drawing.Color.Pink;
            this.textBoxScope.Enabled = false;
            this.textBoxScope.Location = new System.Drawing.Point(36, 156);
            this.textBoxScope.Name = "textBoxScope";
            this.textBoxScope.Size = new System.Drawing.Size(409, 23);
            this.textBoxScope.TabIndex = 39;
            this.textBoxScope.TextChanged += new System.EventHandler(this.textBoxTXT_Validation);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(37, 138);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 15);
            this.label7.TabIndex = 38;
            this.label7.Text = "Media Scope";
            // 
            // textBoxAPIServer
            // 
            this.textBoxAPIServer.BackColor = System.Drawing.Color.Pink;
            this.textBoxAPIServer.Enabled = false;
            this.textBoxAPIServer.Location = new System.Drawing.Point(36, 111);
            this.textBoxAPIServer.Name = "textBoxAPIServer";
            this.textBoxAPIServer.Size = new System.Drawing.Size(409, 23);
            this.textBoxAPIServer.TabIndex = 37;
            this.textBoxAPIServer.TextChanged += new System.EventHandler(this.textBoxURL_Validation);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(37, 93);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(96, 15);
            this.label8.TabIndex = 36;
            this.label8.Text = "Media API Server";
            // 
            // radioButtonOther
            // 
            this.radioButtonOther.AutoSize = true;
            this.radioButtonOther.Location = new System.Drawing.Point(19, 65);
            this.radioButtonOther.Name = "radioButtonOther";
            this.radioButtonOther.Size = new System.Drawing.Size(58, 19);
            this.radioButtonOther.TabIndex = 34;
            this.radioButtonOther.Text = "Other:";
            this.radioButtonOther.UseVisualStyleBackColor = true;
            this.radioButtonOther.CheckedChanged += new System.EventHandler(this.radioButtonOther_CheckedChanged);
            // 
            // radioButtonPartner
            // 
            this.radioButtonPartner.AutoSize = true;
            this.radioButtonPartner.Location = new System.Drawing.Point(19, 38);
            this.radioButtonPartner.Name = "radioButtonPartner";
            this.radioButtonPartner.Size = new System.Drawing.Size(131, 19);
            this.radioButtonPartner.TabIndex = 33;
            this.radioButtonPartner.Text = "Partner Deployment";
            this.radioButtonPartner.UseVisualStyleBackColor = true;
            // 
            // radioButtonProd
            // 
            this.radioButtonProd.AutoSize = true;
            this.radioButtonProd.Checked = true;
            this.radioButtonProd.Location = new System.Drawing.Point(19, 12);
            this.radioButtonProd.Name = "radioButtonProd";
            this.radioButtonProd.Size = new System.Drawing.Size(141, 19);
            this.radioButtonProd.TabIndex = 32;
            this.radioButtonProd.TabStop = true;
            this.radioButtonProd.Text = "Default (Azure Global)";
            this.radioButtonProd.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DarkBlue;
            this.label5.Location = new System.Drawing.Point(14, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(270, 20);
            this.label5.TabIndex = 31;
            this.label5.Text = "Select or enter a Media Service account";
            // 
            // buttonExport
            // 
            this.buttonExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonExport.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.buttonExport.Location = new System.Drawing.Point(112, 407);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(75, 27);
            this.buttonExport.TabIndex = 32;
            this.buttonExport.Text = "Export";
            this.buttonExport.UseVisualStyleBackColor = true;
            this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // buttonImportAll
            // 
            this.buttonImportAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonImportAll.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.buttonImportAll.Location = new System.Drawing.Point(194, 407);
            this.buttonImportAll.Name = "buttonImportAll";
            this.buttonImportAll.Size = new System.Drawing.Size(79, 27);
            this.buttonImportAll.TabIndex = 33;
            this.buttonImportAll.Text = "Import";
            this.buttonImportAll.UseVisualStyleBackColor = true;
            this.buttonImportAll.Click += new System.EventHandler(this.buttonImportAll_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "json";
            this.openFileDialog1.Filter = "Json files|*.json|Xml files|*.xml";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "json";
            this.saveFileDialog1.Filter = "json file|*.json";
            // 
            // accountmgtlink
            // 
            this.accountmgtlink.AutoSize = true;
            this.accountmgtlink.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.accountmgtlink.Location = new System.Drawing.Point(412, 19);
            this.accountmgtlink.Name = "accountmgtlink";
            this.accountmgtlink.Size = new System.Drawing.Size(147, 15);
            this.accountmgtlink.TabIndex = 34;
            this.accountmgtlink.TabStop = true;
            this.accountmgtlink.Text = "How to Create an Account";
            this.accountmgtlink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.accountmgtlink_LinkClicked);
            // 
            // pictureBoxJob
            // 
            this.pictureBoxJob.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxJob.Image = global::AMSExplorer.Bitmaps.AzureMedia_Full_Color_64_opaque;
            this.pictureBoxJob.Location = new System.Drawing.Point(565, 5);
            this.pictureBoxJob.Name = "pictureBoxJob";
            this.pictureBoxJob.Size = new System.Drawing.Size(194, 64);
            this.pictureBoxJob.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxJob.TabIndex = 42;
            this.pictureBoxJob.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.labelVersion);
            this.panel1.Controls.Add(this.buttonLogin);
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Location = new System.Drawing.Point(-1, 467);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(786, 55);
            this.panel1.TabIndex = 52;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // labelVersion
            // 
            this.labelVersion.AutoSize = true;
            this.labelVersion.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.labelVersion.Location = new System.Drawing.Point(15, 21);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(78, 15);
            this.labelVersion.TabIndex = 6;
            this.labelVersion.Text = "Version ??????";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // AMSLogin
            // 
            this.AcceptButton = this.buttonLogin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(784, 523);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBoxJob);
            this.Controls.Add(this.accountmgtlink);
            this.Controls.Add(this.buttonImportAll);
            this.Controls.Add(this.buttonExport);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.buttonDeleteAccountEntry);
            this.Controls.Add(this.buttonSaveToList);
            this.Controls.Add(this.listBoxAcounts);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AMSLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Azure Media Services Explorer";
            this.Load += new System.EventHandler(this.AMSLogin_Load);
            this.Shown += new System.EventHandler(this.AMSLogin_Shown);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxJob)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ListBox listBoxAcounts;
        private System.Windows.Forms.Button buttonSaveToList;
        private System.Windows.Forms.Button buttonDeleteAccountEntry;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxBlobKey;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxAccountKey;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxAccountName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxACSBaseAddress;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxScope;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxAPIServer;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.RadioButton radioButtonOther;
        private System.Windows.Forms.RadioButton radioButtonPartner;
        private System.Windows.Forms.RadioButton radioButtonProd;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonExport;
        private System.Windows.Forms.Button buttonImportAll;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.LinkLabel accountmgtlink;
        private System.Windows.Forms.PictureBox pictureBoxJob;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBoxAzureEndpoint;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboBoxMappingList;
        private System.Windows.Forms.Button buttonAddMapping;
        private System.Windows.Forms.TextBox textBoxManagementPortal;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.TextBox textBoxAccountID;
        private System.Windows.Forms.Label label11;
    }
}