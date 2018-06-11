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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AMSLogin));
            this.buttonLogin = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSaveToList = new System.Windows.Forms.Button();
            this.buttonDeleteAccountEntry = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.tabControlAMS = new System.Windows.Forms.TabControl();
            this.tabPageCredentials = new System.Windows.Forms.TabPage();
            this.linkLabelAADAut = new System.Windows.Forms.LinkLabel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioButtonAADAut = new System.Windows.Forms.RadioButton();
            this.radioButtonACSAut = new System.Windows.Forms.RadioButton();
            this.groupBoxAADMode = new System.Windows.Forms.GroupBox();
            this.radioButtonAADServicePrincipal = new System.Windows.Forms.RadioButton();
            this.radioButtonAADInteractive = new System.Windows.Forms.RadioButton();
            this.textBoxRestAPIEndpoint = new System.Windows.Forms.TextBox();
            this.textBoxAADtenant = new System.Windows.Forms.TextBox();
            this.textBoxAccountName = new System.Windows.Forms.TextBox();
            this.labelE1 = new System.Windows.Forms.Label();
            this.labelE2 = new System.Windows.Forms.Label();
            this.textBoxAccountKey = new System.Windows.Forms.TextBox();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxBlobKey = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPageACS = new System.Windows.Forms.TabPage();
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
            this.tabPageAAD = new System.Windows.Forms.TabPage();
            this.textBoxAADManagementPortal = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxAADMappingList = new System.Windows.Forms.ComboBox();
            this.textBoxAADAzureEndpoint = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.textBoxAADRedirect = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.textBoxAADClienid = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.textBoxAADAMSResource = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.radioButtonAADOther = new System.Windows.Forms.RadioButton();
            this.radioButtonAADProd = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonExport = new System.Windows.Forms.Button();
            this.buttonImportAll = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.accountmgtlink = new System.Windows.Forms.LinkLabel();
            this.pictureBoxJob = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonConnectFullyInteractive = new System.Windows.Forms.Button();
            this.labelVersion = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.listViewAccounts = new System.Windows.Forms.ListView();
            this.tabControlAMS.SuspendLayout();
            this.tabPageCredentials.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBoxAADMode.SuspendLayout();
            this.tabPageACS.SuspendLayout();
            this.tabPageAAD.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxJob)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonLogin
            // 
            resources.ApplyResources(this.buttonLogin, "buttonLogin");
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.UseVisualStyleBackColor = true;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonSaveToList
            // 
            resources.ApplyResources(this.buttonSaveToList, "buttonSaveToList");
            this.buttonSaveToList.Name = "buttonSaveToList";
            this.toolTip1.SetToolTip(this.buttonSaveToList, resources.GetString("buttonSaveToList.ToolTip"));
            this.buttonSaveToList.UseVisualStyleBackColor = true;
            this.buttonSaveToList.Click += new System.EventHandler(this.buttonSaveToList_Click);
            // 
            // buttonDeleteAccountEntry
            // 
            resources.ApplyResources(this.buttonDeleteAccountEntry, "buttonDeleteAccountEntry");
            this.buttonDeleteAccountEntry.Name = "buttonDeleteAccountEntry";
            this.buttonDeleteAccountEntry.UseVisualStyleBackColor = true;
            this.buttonDeleteAccountEntry.Click += new System.EventHandler(this.buttonDeleteAccount_Click);
            // 
            // buttonClear
            // 
            resources.ApplyResources(this.buttonClear, "buttonClear");
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // tabControlAMS
            // 
            resources.ApplyResources(this.tabControlAMS, "tabControlAMS");
            this.tabControlAMS.Controls.Add(this.tabPageCredentials);
            this.tabControlAMS.Controls.Add(this.tabPageACS);
            this.tabControlAMS.Controls.Add(this.tabPageAAD);
            this.tabControlAMS.Name = "tabControlAMS";
            this.tabControlAMS.SelectedIndex = 0;
            // 
            // tabPageCredentials
            // 
            this.tabPageCredentials.BackColor = System.Drawing.SystemColors.Window;
            this.tabPageCredentials.Controls.Add(this.linkLabelAADAut);
            this.tabPageCredentials.Controls.Add(this.groupBox2);
            this.tabPageCredentials.Controls.Add(this.groupBoxAADMode);
            this.tabPageCredentials.Controls.Add(this.textBoxRestAPIEndpoint);
            this.tabPageCredentials.Controls.Add(this.textBoxAADtenant);
            this.tabPageCredentials.Controls.Add(this.textBoxAccountName);
            this.tabPageCredentials.Controls.Add(this.labelE1);
            this.tabPageCredentials.Controls.Add(this.labelE2);
            this.tabPageCredentials.Controls.Add(this.textBoxAccountKey);
            this.tabPageCredentials.Controls.Add(this.textBoxDescription);
            this.tabPageCredentials.Controls.Add(this.label2);
            this.tabPageCredentials.Controls.Add(this.textBoxBlobKey);
            this.tabPageCredentials.Controls.Add(this.label3);
            resources.ApplyResources(this.tabPageCredentials, "tabPageCredentials");
            this.tabPageCredentials.Name = "tabPageCredentials";
            // 
            // linkLabelAADAut
            // 
            resources.ApplyResources(this.linkLabelAADAut, "linkLabelAADAut");
            this.linkLabelAADAut.Name = "linkLabelAADAut";
            this.linkLabelAADAut.TabStop = true;
            this.linkLabelAADAut.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.accountmgtlink_LinkClicked);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioButtonAADAut);
            this.groupBox2.Controls.Add(this.radioButtonACSAut);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // radioButtonAADAut
            // 
            resources.ApplyResources(this.radioButtonAADAut, "radioButtonAADAut");
            this.radioButtonAADAut.Checked = true;
            this.radioButtonAADAut.Name = "radioButtonAADAut";
            this.radioButtonAADAut.TabStop = true;
            this.radioButtonAADAut.UseVisualStyleBackColor = true;
            this.radioButtonAADAut.CheckedChanged += new System.EventHandler(this.radioButtonAADInteract_CheckedChanged);
            // 
            // radioButtonACSAut
            // 
            resources.ApplyResources(this.radioButtonACSAut, "radioButtonACSAut");
            this.radioButtonACSAut.Name = "radioButtonACSAut";
            this.radioButtonACSAut.UseVisualStyleBackColor = true;
            this.radioButtonACSAut.CheckedChanged += new System.EventHandler(this.radioButtonACSAut_CheckedChanged);
            // 
            // groupBoxAADMode
            // 
            this.groupBoxAADMode.Controls.Add(this.radioButtonAADServicePrincipal);
            this.groupBoxAADMode.Controls.Add(this.radioButtonAADInteractive);
            resources.ApplyResources(this.groupBoxAADMode, "groupBoxAADMode");
            this.groupBoxAADMode.Name = "groupBoxAADMode";
            this.groupBoxAADMode.TabStop = false;
            // 
            // radioButtonAADServicePrincipal
            // 
            resources.ApplyResources(this.radioButtonAADServicePrincipal, "radioButtonAADServicePrincipal");
            this.radioButtonAADServicePrincipal.Name = "radioButtonAADServicePrincipal";
            this.radioButtonAADServicePrincipal.UseVisualStyleBackColor = true;
            // 
            // radioButtonAADInteractive
            // 
            resources.ApplyResources(this.radioButtonAADInteractive, "radioButtonAADInteractive");
            this.radioButtonAADInteractive.Checked = true;
            this.radioButtonAADInteractive.Name = "radioButtonAADInteractive";
            this.radioButtonAADInteractive.TabStop = true;
            this.radioButtonAADInteractive.UseVisualStyleBackColor = true;
            // 
            // textBoxRestAPIEndpoint
            // 
            resources.ApplyResources(this.textBoxRestAPIEndpoint, "textBoxRestAPIEndpoint");
            this.textBoxRestAPIEndpoint.Name = "textBoxRestAPIEndpoint";
            this.toolTip1.SetToolTip(this.textBoxRestAPIEndpoint, resources.GetString("textBoxRestAPIEndpoint.ToolTip"));
            this.textBoxRestAPIEndpoint.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxRestAPIEndpoint_Validating);
            // 
            // textBoxAADtenant
            // 
            resources.ApplyResources(this.textBoxAADtenant, "textBoxAADtenant");
            this.textBoxAADtenant.Name = "textBoxAADtenant";
            this.toolTip1.SetToolTip(this.textBoxAADtenant, resources.GetString("textBoxAADtenant.ToolTip"));
            this.textBoxAADtenant.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxAADtenant_Validating);
            // 
            // textBoxAccountName
            // 
            resources.ApplyResources(this.textBoxAccountName, "textBoxAccountName");
            this.textBoxAccountName.Name = "textBoxAccountName";
            this.textBoxAccountName.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxAccountName_Validating);
            // 
            // labelE1
            // 
            resources.ApplyResources(this.labelE1, "labelE1");
            this.labelE1.Name = "labelE1";
            // 
            // labelE2
            // 
            resources.ApplyResources(this.labelE2, "labelE2");
            this.labelE2.Name = "labelE2";
            // 
            // textBoxAccountKey
            // 
            resources.ApplyResources(this.textBoxAccountKey, "textBoxAccountKey");
            this.textBoxAccountKey.Name = "textBoxAccountKey";
            this.textBoxAccountKey.UseSystemPasswordChar = true;
            this.textBoxAccountKey.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxAccountKey_Validating);
            // 
            // textBoxDescription
            // 
            resources.ApplyResources(this.textBoxDescription, "textBoxDescription");
            this.textBoxDescription.Name = "textBoxDescription";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // textBoxBlobKey
            // 
            resources.ApplyResources(this.textBoxBlobKey, "textBoxBlobKey");
            this.textBoxBlobKey.Name = "textBoxBlobKey";
            this.textBoxBlobKey.UseSystemPasswordChar = true;
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // tabPageACS
            // 
            this.tabPageACS.BackColor = System.Drawing.SystemColors.Window;
            this.tabPageACS.Controls.Add(this.textBoxManagementPortal);
            this.tabPageACS.Controls.Add(this.label10);
            this.tabPageACS.Controls.Add(this.comboBoxMappingList);
            this.tabPageACS.Controls.Add(this.buttonAddMapping);
            this.tabPageACS.Controls.Add(this.textBoxAzureEndpoint);
            this.tabPageACS.Controls.Add(this.label9);
            this.tabPageACS.Controls.Add(this.textBoxACSBaseAddress);
            this.tabPageACS.Controls.Add(this.label6);
            this.tabPageACS.Controls.Add(this.textBoxScope);
            this.tabPageACS.Controls.Add(this.label7);
            this.tabPageACS.Controls.Add(this.textBoxAPIServer);
            this.tabPageACS.Controls.Add(this.label8);
            this.tabPageACS.Controls.Add(this.radioButtonOther);
            this.tabPageACS.Controls.Add(this.radioButtonPartner);
            this.tabPageACS.Controls.Add(this.radioButtonProd);
            resources.ApplyResources(this.tabPageACS, "tabPageACS");
            this.tabPageACS.Name = "tabPageACS";
            // 
            // textBoxManagementPortal
            // 
            resources.ApplyResources(this.textBoxManagementPortal, "textBoxManagementPortal");
            this.textBoxManagementPortal.BackColor = System.Drawing.Color.Pink;
            this.textBoxManagementPortal.Name = "textBoxManagementPortal";
            this.textBoxManagementPortal.TextChanged += new System.EventHandler(this.textBoxURL_Validation);
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // comboBoxMappingList
            // 
            this.comboBoxMappingList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.comboBoxMappingList, "comboBoxMappingList");
            this.comboBoxMappingList.FormattingEnabled = true;
            this.comboBoxMappingList.Name = "comboBoxMappingList";
            // 
            // buttonAddMapping
            // 
            resources.ApplyResources(this.buttonAddMapping, "buttonAddMapping");
            this.buttonAddMapping.Name = "buttonAddMapping";
            this.buttonAddMapping.UseVisualStyleBackColor = true;
            this.buttonAddMapping.Click += new System.EventHandler(this.buttonAddMapping_Click);
            // 
            // textBoxAzureEndpoint
            // 
            resources.ApplyResources(this.textBoxAzureEndpoint, "textBoxAzureEndpoint");
            this.textBoxAzureEndpoint.BackColor = System.Drawing.Color.Pink;
            this.textBoxAzureEndpoint.Name = "textBoxAzureEndpoint";
            this.textBoxAzureEndpoint.TextChanged += new System.EventHandler(this.textBoxTXT_Validation);
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // textBoxACSBaseAddress
            // 
            resources.ApplyResources(this.textBoxACSBaseAddress, "textBoxACSBaseAddress");
            this.textBoxACSBaseAddress.BackColor = System.Drawing.Color.Pink;
            this.textBoxACSBaseAddress.Name = "textBoxACSBaseAddress";
            this.textBoxACSBaseAddress.TextChanged += new System.EventHandler(this.textBoxURL_Validation);
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // textBoxScope
            // 
            resources.ApplyResources(this.textBoxScope, "textBoxScope");
            this.textBoxScope.BackColor = System.Drawing.Color.Pink;
            this.textBoxScope.Name = "textBoxScope";
            this.textBoxScope.TextChanged += new System.EventHandler(this.textBoxTXT_Validation);
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // textBoxAPIServer
            // 
            resources.ApplyResources(this.textBoxAPIServer, "textBoxAPIServer");
            this.textBoxAPIServer.BackColor = System.Drawing.Color.Pink;
            this.textBoxAPIServer.Name = "textBoxAPIServer";
            this.textBoxAPIServer.TextChanged += new System.EventHandler(this.textBoxURL_Validation);
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // radioButtonOther
            // 
            resources.ApplyResources(this.radioButtonOther, "radioButtonOther");
            this.radioButtonOther.Name = "radioButtonOther";
            this.radioButtonOther.UseVisualStyleBackColor = true;
            this.radioButtonOther.CheckedChanged += new System.EventHandler(this.radioButtonOther_CheckedChanged);
            // 
            // radioButtonPartner
            // 
            resources.ApplyResources(this.radioButtonPartner, "radioButtonPartner");
            this.radioButtonPartner.Name = "radioButtonPartner";
            this.radioButtonPartner.UseVisualStyleBackColor = true;
            // 
            // radioButtonProd
            // 
            resources.ApplyResources(this.radioButtonProd, "radioButtonProd");
            this.radioButtonProd.Checked = true;
            this.radioButtonProd.Name = "radioButtonProd";
            this.radioButtonProd.TabStop = true;
            this.radioButtonProd.UseVisualStyleBackColor = true;
            // 
            // tabPageAAD
            // 
            this.tabPageAAD.Controls.Add(this.textBoxAADManagementPortal);
            this.tabPageAAD.Controls.Add(this.label4);
            this.tabPageAAD.Controls.Add(this.comboBoxAADMappingList);
            this.tabPageAAD.Controls.Add(this.textBoxAADAzureEndpoint);
            this.tabPageAAD.Controls.Add(this.label12);
            this.tabPageAAD.Controls.Add(this.textBoxAADRedirect);
            this.tabPageAAD.Controls.Add(this.label13);
            this.tabPageAAD.Controls.Add(this.textBoxAADClienid);
            this.tabPageAAD.Controls.Add(this.label14);
            this.tabPageAAD.Controls.Add(this.textBoxAADAMSResource);
            this.tabPageAAD.Controls.Add(this.label15);
            this.tabPageAAD.Controls.Add(this.radioButtonAADOther);
            this.tabPageAAD.Controls.Add(this.radioButtonAADProd);
            resources.ApplyResources(this.tabPageAAD, "tabPageAAD");
            this.tabPageAAD.Name = "tabPageAAD";
            this.tabPageAAD.UseVisualStyleBackColor = true;
            // 
            // textBoxAADManagementPortal
            // 
            resources.ApplyResources(this.textBoxAADManagementPortal, "textBoxAADManagementPortal");
            this.textBoxAADManagementPortal.BackColor = System.Drawing.Color.Pink;
            this.textBoxAADManagementPortal.Name = "textBoxAADManagementPortal";
            this.textBoxAADManagementPortal.TextChanged += new System.EventHandler(this.textBoxURL_Validation);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // comboBoxAADMappingList
            // 
            this.comboBoxAADMappingList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.comboBoxAADMappingList, "comboBoxAADMappingList");
            this.comboBoxAADMappingList.FormattingEnabled = true;
            this.comboBoxAADMappingList.Name = "comboBoxAADMappingList";
            this.comboBoxAADMappingList.SelectedIndexChanged += new System.EventHandler(this.comboBoxAADMappingList_SelectedIndexChanged);
            // 
            // textBoxAADAzureEndpoint
            // 
            resources.ApplyResources(this.textBoxAADAzureEndpoint, "textBoxAADAzureEndpoint");
            this.textBoxAADAzureEndpoint.BackColor = System.Drawing.Color.Pink;
            this.textBoxAADAzureEndpoint.Name = "textBoxAADAzureEndpoint";
            this.textBoxAADAzureEndpoint.TextChanged += new System.EventHandler(this.textBoxURL_Validation);
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // textBoxAADRedirect
            // 
            resources.ApplyResources(this.textBoxAADRedirect, "textBoxAADRedirect");
            this.textBoxAADRedirect.BackColor = System.Drawing.Color.Pink;
            this.textBoxAADRedirect.Name = "textBoxAADRedirect";
            this.textBoxAADRedirect.TextChanged += new System.EventHandler(this.textBoxURL_Validation);
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            // 
            // textBoxAADClienid
            // 
            resources.ApplyResources(this.textBoxAADClienid, "textBoxAADClienid");
            this.textBoxAADClienid.BackColor = System.Drawing.Color.Pink;
            this.textBoxAADClienid.Name = "textBoxAADClienid";
            this.textBoxAADClienid.TextChanged += new System.EventHandler(this.textBoxTXT_Validation);
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            // 
            // textBoxAADAMSResource
            // 
            resources.ApplyResources(this.textBoxAADAMSResource, "textBoxAADAMSResource");
            this.textBoxAADAMSResource.BackColor = System.Drawing.Color.Pink;
            this.textBoxAADAMSResource.Name = "textBoxAADAMSResource";
            this.textBoxAADAMSResource.TextChanged += new System.EventHandler(this.textBoxURL_Validation);
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.Name = "label15";
            // 
            // radioButtonAADOther
            // 
            resources.ApplyResources(this.radioButtonAADOther, "radioButtonAADOther");
            this.radioButtonAADOther.Name = "radioButtonAADOther";
            this.radioButtonAADOther.UseVisualStyleBackColor = true;
            this.radioButtonAADOther.CheckedChanged += new System.EventHandler(this.radioButtonAADOther_CheckedChanged);
            // 
            // radioButtonAADProd
            // 
            resources.ApplyResources(this.radioButtonAADProd, "radioButtonAADProd");
            this.radioButtonAADProd.Checked = true;
            this.radioButtonAADProd.Name = "radioButtonAADProd";
            this.radioButtonAADProd.TabStop = true;
            this.radioButtonAADProd.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.ForeColor = System.Drawing.Color.DarkBlue;
            this.label5.Name = "label5";
            // 
            // buttonExport
            // 
            resources.ApplyResources(this.buttonExport, "buttonExport");
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.UseVisualStyleBackColor = true;
            this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // buttonImportAll
            // 
            resources.ApplyResources(this.buttonImportAll, "buttonImportAll");
            this.buttonImportAll.Name = "buttonImportAll";
            this.buttonImportAll.UseVisualStyleBackColor = true;
            this.buttonImportAll.Click += new System.EventHandler(this.buttonImportAll_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "json";
            resources.ApplyResources(this.openFileDialog1, "openFileDialog1");
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "json";
            resources.ApplyResources(this.saveFileDialog1, "saveFileDialog1");
            // 
            // accountmgtlink
            // 
            resources.ApplyResources(this.accountmgtlink, "accountmgtlink");
            this.accountmgtlink.Name = "accountmgtlink";
            this.accountmgtlink.TabStop = true;
            this.accountmgtlink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.accountmgtlink_LinkClicked);
            // 
            // pictureBoxJob
            // 
            resources.ApplyResources(this.pictureBoxJob, "pictureBoxJob");
            this.pictureBoxJob.Image = global::AMSExplorer.Bitmaps.AzureMedia_Full_Color_64_opaque;
            this.pictureBoxJob.Name = "pictureBoxJob";
            this.pictureBoxJob.TabStop = false;
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.labelVersion);
            this.panel1.Controls.Add(this.buttonLogin);
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Name = "panel1";
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // buttonConnectFullyInteractive
            // 
            resources.ApplyResources(this.buttonConnectFullyInteractive, "buttonConnectFullyInteractive");
            this.buttonConnectFullyInteractive.Name = "buttonConnectFullyInteractive";
            this.buttonConnectFullyInteractive.UseVisualStyleBackColor = true;
            this.buttonConnectFullyInteractive.Click += new System.EventHandler(this.buttonConnectFullyInteractive_Click);
            // 
            // labelVersion
            // 
            resources.ApplyResources(this.labelVersion, "labelVersion");
            this.labelVersion.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.labelVersion.Name = "labelVersion";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // listViewAccounts
            // 
            this.listViewAccounts.FullRowSelect = true;
            resources.ApplyResources(this.listViewAccounts, "listViewAccounts");
            this.listViewAccounts.MultiSelect = false;
            this.listViewAccounts.Name = "listViewAccounts";
            this.listViewAccounts.ShowItemToolTips = true;
            this.listViewAccounts.UseCompatibleStateImageBehavior = false;
            this.listViewAccounts.View = System.Windows.Forms.View.List;
            this.listViewAccounts.SelectedIndexChanged += new System.EventHandler(this.listViewAccounts_SelectedIndexChanged);
            this.listViewAccounts.DoubleClick += new System.EventHandler(this.listBoxAcounts_DoubleClick);
            // 
            // AMSLogin
            // 
            this.AcceptButton = this.buttonLogin;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.buttonConnectFullyInteractive);
            this.Controls.Add(this.listViewAccounts);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBoxJob);
            this.Controls.Add(this.accountmgtlink);
            this.Controls.Add(this.buttonImportAll);
            this.Controls.Add(this.buttonExport);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tabControlAMS);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.buttonDeleteAccountEntry);
            this.Controls.Add(this.buttonSaveToList);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AMSLogin";
            this.Load += new System.EventHandler(this.AMSLogin_Load);
            this.Shown += new System.EventHandler(this.AMSLogin_Shown);
            this.tabControlAMS.ResumeLayout(false);
            this.tabPageCredentials.ResumeLayout(false);
            this.tabPageCredentials.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBoxAADMode.ResumeLayout(false);
            this.groupBoxAADMode.PerformLayout();
            this.tabPageACS.ResumeLayout(false);
            this.tabPageACS.PerformLayout();
            this.tabPageAAD.ResumeLayout(false);
            this.tabPageAAD.PerformLayout();
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
        private System.Windows.Forms.Button buttonSaveToList;
        private System.Windows.Forms.Button buttonDeleteAccountEntry;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.TabControl tabControlAMS;
        private System.Windows.Forms.TabPage tabPageCredentials;
        private System.Windows.Forms.TabPage tabPageACS;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxBlobKey;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxAccountKey;
        private System.Windows.Forms.Label labelE2;
        private System.Windows.Forms.TextBox textBoxAccountName;
        private System.Windows.Forms.Label labelE1;
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
        private System.Windows.Forms.RadioButton radioButtonAADAut;
        private System.Windows.Forms.RadioButton radioButtonACSAut;
        private System.Windows.Forms.TextBox textBoxRestAPIEndpoint;
        private System.Windows.Forms.TextBox textBoxAADtenant;
        private System.Windows.Forms.TabPage tabPageAAD;
        private System.Windows.Forms.TextBox textBoxAADManagementPortal;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxAADMappingList;
        private System.Windows.Forms.TextBox textBoxAADAzureEndpoint;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBoxAADRedirect;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBoxAADClienid;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox textBoxAADAMSResource;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.RadioButton radioButtonAADOther;
        private System.Windows.Forms.RadioButton radioButtonAADProd;
        private System.Windows.Forms.GroupBox groupBoxAADMode;
        private System.Windows.Forms.RadioButton radioButtonAADServicePrincipal;
        private System.Windows.Forms.RadioButton radioButtonAADInteractive;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.LinkLabel linkLabelAADAut;
        private System.Windows.Forms.ListView listViewAccounts;
        private System.Windows.Forms.Button buttonConnectFullyInteractive;
    }
}