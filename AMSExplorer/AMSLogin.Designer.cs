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
            this.listBoxAcounts = new System.Windows.Forms.ListBox();
            this.buttonSaveToList = new System.Windows.Forms.Button();
            this.buttonDeleteAccountEntry = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.tabControlAMS = new System.Windows.Forms.TabControl();
            this.tabPageCredentials = new System.Windows.Forms.TabPage();
            this.textBoxRestAPIEndpoint = new System.Windows.Forms.TextBox();
            this.textBoxAADtenant = new System.Windows.Forms.TextBox();
            this.textBoxAccountName = new System.Windows.Forms.TextBox();
            this.labelE1 = new System.Windows.Forms.Label();
            this.labelE2 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.textBoxAccountKey = new System.Windows.Forms.TextBox();
            this.radioButtonAADInteract = new System.Windows.Forms.RadioButton();
            this.radioButtonACS = new System.Windows.Forms.RadioButton();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxBlobKey = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPageEndpoint = new System.Windows.Forms.TabPage();
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
            this.tabControlAMS.SuspendLayout();
            this.tabPageCredentials.SuspendLayout();
            this.tabPageEndpoint.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxJob)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonLogin
            // 
            resources.ApplyResources(this.buttonLogin, "buttonLogin");
            this.errorProvider1.SetError(this.buttonLogin, resources.GetString("buttonLogin.Error"));
            this.errorProvider1.SetIconAlignment(this.buttonLogin, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("buttonLogin.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.buttonLogin, ((int)(resources.GetObject("buttonLogin.IconPadding"))));
            this.buttonLogin.Name = "buttonLogin";
            this.toolTip1.SetToolTip(this.buttonLogin, resources.GetString("buttonLogin.ToolTip"));
            this.buttonLogin.UseVisualStyleBackColor = true;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.errorProvider1.SetError(this.buttonCancel, resources.GetString("buttonCancel.Error"));
            this.errorProvider1.SetIconAlignment(this.buttonCancel, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("buttonCancel.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.buttonCancel, ((int)(resources.GetObject("buttonCancel.IconPadding"))));
            this.buttonCancel.Name = "buttonCancel";
            this.toolTip1.SetToolTip(this.buttonCancel, resources.GetString("buttonCancel.ToolTip"));
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // listBoxAcounts
            // 
            resources.ApplyResources(this.listBoxAcounts, "listBoxAcounts");
            this.errorProvider1.SetError(this.listBoxAcounts, resources.GetString("listBoxAcounts.Error"));
            this.listBoxAcounts.FormattingEnabled = true;
            this.errorProvider1.SetIconAlignment(this.listBoxAcounts, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("listBoxAcounts.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.listBoxAcounts, ((int)(resources.GetObject("listBoxAcounts.IconPadding"))));
            this.listBoxAcounts.Name = "listBoxAcounts";
            this.toolTip1.SetToolTip(this.listBoxAcounts, resources.GetString("listBoxAcounts.ToolTip"));
            this.listBoxAcounts.SelectedIndexChanged += new System.EventHandler(this.listBoxAccounts_SelectedIndexChanged);
            this.listBoxAcounts.DoubleClick += new System.EventHandler(this.listBoxAcounts_DoubleClick);
            // 
            // buttonSaveToList
            // 
            resources.ApplyResources(this.buttonSaveToList, "buttonSaveToList");
            this.errorProvider1.SetError(this.buttonSaveToList, resources.GetString("buttonSaveToList.Error"));
            this.errorProvider1.SetIconAlignment(this.buttonSaveToList, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("buttonSaveToList.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.buttonSaveToList, ((int)(resources.GetObject("buttonSaveToList.IconPadding"))));
            this.buttonSaveToList.Name = "buttonSaveToList";
            this.toolTip1.SetToolTip(this.buttonSaveToList, resources.GetString("buttonSaveToList.ToolTip"));
            this.buttonSaveToList.UseVisualStyleBackColor = true;
            this.buttonSaveToList.Click += new System.EventHandler(this.buttonSaveToList_Click);
            // 
            // buttonDeleteAccountEntry
            // 
            resources.ApplyResources(this.buttonDeleteAccountEntry, "buttonDeleteAccountEntry");
            this.errorProvider1.SetError(this.buttonDeleteAccountEntry, resources.GetString("buttonDeleteAccountEntry.Error"));
            this.errorProvider1.SetIconAlignment(this.buttonDeleteAccountEntry, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("buttonDeleteAccountEntry.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.buttonDeleteAccountEntry, ((int)(resources.GetObject("buttonDeleteAccountEntry.IconPadding"))));
            this.buttonDeleteAccountEntry.Name = "buttonDeleteAccountEntry";
            this.toolTip1.SetToolTip(this.buttonDeleteAccountEntry, resources.GetString("buttonDeleteAccountEntry.ToolTip"));
            this.buttonDeleteAccountEntry.UseVisualStyleBackColor = true;
            this.buttonDeleteAccountEntry.Click += new System.EventHandler(this.buttonDeleteAccount_Click);
            // 
            // buttonClear
            // 
            resources.ApplyResources(this.buttonClear, "buttonClear");
            this.errorProvider1.SetError(this.buttonClear, resources.GetString("buttonClear.Error"));
            this.errorProvider1.SetIconAlignment(this.buttonClear, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("buttonClear.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.buttonClear, ((int)(resources.GetObject("buttonClear.IconPadding"))));
            this.buttonClear.Name = "buttonClear";
            this.toolTip1.SetToolTip(this.buttonClear, resources.GetString("buttonClear.ToolTip"));
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // tabControlAMS
            // 
            resources.ApplyResources(this.tabControlAMS, "tabControlAMS");
            this.tabControlAMS.Controls.Add(this.tabPageCredentials);
            this.tabControlAMS.Controls.Add(this.tabPageEndpoint);
            this.errorProvider1.SetError(this.tabControlAMS, resources.GetString("tabControlAMS.Error"));
            this.errorProvider1.SetIconAlignment(this.tabControlAMS, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("tabControlAMS.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.tabControlAMS, ((int)(resources.GetObject("tabControlAMS.IconPadding"))));
            this.tabControlAMS.Name = "tabControlAMS";
            this.tabControlAMS.SelectedIndex = 0;
            this.toolTip1.SetToolTip(this.tabControlAMS, resources.GetString("tabControlAMS.ToolTip"));
            // 
            // tabPageCredentials
            // 
            resources.ApplyResources(this.tabPageCredentials, "tabPageCredentials");
            this.tabPageCredentials.BackColor = System.Drawing.SystemColors.Window;
            this.tabPageCredentials.Controls.Add(this.textBoxRestAPIEndpoint);
            this.tabPageCredentials.Controls.Add(this.textBoxAADtenant);
            this.tabPageCredentials.Controls.Add(this.textBoxAccountName);
            this.tabPageCredentials.Controls.Add(this.labelE1);
            this.tabPageCredentials.Controls.Add(this.labelE2);
            this.tabPageCredentials.Controls.Add(this.label11);
            this.tabPageCredentials.Controls.Add(this.textBoxAccountKey);
            this.tabPageCredentials.Controls.Add(this.radioButtonAADInteract);
            this.tabPageCredentials.Controls.Add(this.radioButtonACS);
            this.tabPageCredentials.Controls.Add(this.textBoxDescription);
            this.tabPageCredentials.Controls.Add(this.label2);
            this.tabPageCredentials.Controls.Add(this.textBoxBlobKey);
            this.tabPageCredentials.Controls.Add(this.label3);
            this.errorProvider1.SetError(this.tabPageCredentials, resources.GetString("tabPageCredentials.Error"));
            this.errorProvider1.SetIconAlignment(this.tabPageCredentials, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("tabPageCredentials.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.tabPageCredentials, ((int)(resources.GetObject("tabPageCredentials.IconPadding"))));
            this.tabPageCredentials.Name = "tabPageCredentials";
            this.toolTip1.SetToolTip(this.tabPageCredentials, resources.GetString("tabPageCredentials.ToolTip"));
            // 
            // textBoxRestAPIEndpoint
            // 
            resources.ApplyResources(this.textBoxRestAPIEndpoint, "textBoxRestAPIEndpoint");
            this.errorProvider1.SetError(this.textBoxRestAPIEndpoint, resources.GetString("textBoxRestAPIEndpoint.Error"));
            this.errorProvider1.SetIconAlignment(this.textBoxRestAPIEndpoint, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("textBoxRestAPIEndpoint.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.textBoxRestAPIEndpoint, ((int)(resources.GetObject("textBoxRestAPIEndpoint.IconPadding"))));
            this.textBoxRestAPIEndpoint.Name = "textBoxRestAPIEndpoint";
            this.toolTip1.SetToolTip(this.textBoxRestAPIEndpoint, resources.GetString("textBoxRestAPIEndpoint.ToolTip"));
            this.textBoxRestAPIEndpoint.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxRestAPIEndpoint_Validating);
            // 
            // textBoxAADtenant
            // 
            resources.ApplyResources(this.textBoxAADtenant, "textBoxAADtenant");
            this.errorProvider1.SetError(this.textBoxAADtenant, resources.GetString("textBoxAADtenant.Error"));
            this.errorProvider1.SetIconAlignment(this.textBoxAADtenant, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("textBoxAADtenant.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.textBoxAADtenant, ((int)(resources.GetObject("textBoxAADtenant.IconPadding"))));
            this.textBoxAADtenant.Name = "textBoxAADtenant";
            this.toolTip1.SetToolTip(this.textBoxAADtenant, resources.GetString("textBoxAADtenant.ToolTip"));
            this.textBoxAADtenant.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxAADtenant_Validating);
            // 
            // textBoxAccountName
            // 
            resources.ApplyResources(this.textBoxAccountName, "textBoxAccountName");
            this.errorProvider1.SetError(this.textBoxAccountName, resources.GetString("textBoxAccountName.Error"));
            this.errorProvider1.SetIconAlignment(this.textBoxAccountName, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("textBoxAccountName.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.textBoxAccountName, ((int)(resources.GetObject("textBoxAccountName.IconPadding"))));
            this.textBoxAccountName.Name = "textBoxAccountName";
            this.toolTip1.SetToolTip(this.textBoxAccountName, resources.GetString("textBoxAccountName.ToolTip"));
            this.textBoxAccountName.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxAccountName_Validating);
            // 
            // labelE1
            // 
            resources.ApplyResources(this.labelE1, "labelE1");
            this.errorProvider1.SetError(this.labelE1, resources.GetString("labelE1.Error"));
            this.errorProvider1.SetIconAlignment(this.labelE1, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("labelE1.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.labelE1, ((int)(resources.GetObject("labelE1.IconPadding"))));
            this.labelE1.Name = "labelE1";
            this.toolTip1.SetToolTip(this.labelE1, resources.GetString("labelE1.ToolTip"));
            // 
            // labelE2
            // 
            resources.ApplyResources(this.labelE2, "labelE2");
            this.errorProvider1.SetError(this.labelE2, resources.GetString("labelE2.Error"));
            this.errorProvider1.SetIconAlignment(this.labelE2, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("labelE2.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.labelE2, ((int)(resources.GetObject("labelE2.IconPadding"))));
            this.labelE2.Name = "labelE2";
            this.toolTip1.SetToolTip(this.labelE2, resources.GetString("labelE2.ToolTip"));
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.errorProvider1.SetError(this.label11, resources.GetString("label11.Error"));
            this.errorProvider1.SetIconAlignment(this.label11, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label11.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label11, ((int)(resources.GetObject("label11.IconPadding"))));
            this.label11.Name = "label11";
            this.toolTip1.SetToolTip(this.label11, resources.GetString("label11.ToolTip"));
            // 
            // textBoxAccountKey
            // 
            resources.ApplyResources(this.textBoxAccountKey, "textBoxAccountKey");
            this.errorProvider1.SetError(this.textBoxAccountKey, resources.GetString("textBoxAccountKey.Error"));
            this.errorProvider1.SetIconAlignment(this.textBoxAccountKey, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("textBoxAccountKey.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.textBoxAccountKey, ((int)(resources.GetObject("textBoxAccountKey.IconPadding"))));
            this.textBoxAccountKey.Name = "textBoxAccountKey";
            this.toolTip1.SetToolTip(this.textBoxAccountKey, resources.GetString("textBoxAccountKey.ToolTip"));
            this.textBoxAccountKey.UseSystemPasswordChar = true;
            this.textBoxAccountKey.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxAccountKey_Validating);
            // 
            // radioButtonAADInteract
            // 
            resources.ApplyResources(this.radioButtonAADInteract, "radioButtonAADInteract");
            this.radioButtonAADInteract.Checked = true;
            this.errorProvider1.SetError(this.radioButtonAADInteract, resources.GetString("radioButtonAADInteract.Error"));
            this.errorProvider1.SetIconAlignment(this.radioButtonAADInteract, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("radioButtonAADInteract.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.radioButtonAADInteract, ((int)(resources.GetObject("radioButtonAADInteract.IconPadding"))));
            this.radioButtonAADInteract.Name = "radioButtonAADInteract";
            this.radioButtonAADInteract.TabStop = true;
            this.toolTip1.SetToolTip(this.radioButtonAADInteract, resources.GetString("radioButtonAADInteract.ToolTip"));
            this.radioButtonAADInteract.UseVisualStyleBackColor = true;
            this.radioButtonAADInteract.CheckedChanged += new System.EventHandler(this.radioButtonAADInteract_CheckedChanged);
            // 
            // radioButtonACS
            // 
            resources.ApplyResources(this.radioButtonACS, "radioButtonACS");
            this.errorProvider1.SetError(this.radioButtonACS, resources.GetString("radioButtonACS.Error"));
            this.errorProvider1.SetIconAlignment(this.radioButtonACS, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("radioButtonACS.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.radioButtonACS, ((int)(resources.GetObject("radioButtonACS.IconPadding"))));
            this.radioButtonACS.Name = "radioButtonACS";
            this.toolTip1.SetToolTip(this.radioButtonACS, resources.GetString("radioButtonACS.ToolTip"));
            this.radioButtonACS.UseVisualStyleBackColor = true;
            // 
            // textBoxDescription
            // 
            resources.ApplyResources(this.textBoxDescription, "textBoxDescription");
            this.errorProvider1.SetError(this.textBoxDescription, resources.GetString("textBoxDescription.Error"));
            this.errorProvider1.SetIconAlignment(this.textBoxDescription, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("textBoxDescription.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.textBoxDescription, ((int)(resources.GetObject("textBoxDescription.IconPadding"))));
            this.textBoxDescription.Name = "textBoxDescription";
            this.toolTip1.SetToolTip(this.textBoxDescription, resources.GetString("textBoxDescription.ToolTip"));
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.errorProvider1.SetError(this.label2, resources.GetString("label2.Error"));
            this.errorProvider1.SetIconAlignment(this.label2, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label2.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label2, ((int)(resources.GetObject("label2.IconPadding"))));
            this.label2.Name = "label2";
            this.toolTip1.SetToolTip(this.label2, resources.GetString("label2.ToolTip"));
            // 
            // textBoxBlobKey
            // 
            resources.ApplyResources(this.textBoxBlobKey, "textBoxBlobKey");
            this.errorProvider1.SetError(this.textBoxBlobKey, resources.GetString("textBoxBlobKey.Error"));
            this.errorProvider1.SetIconAlignment(this.textBoxBlobKey, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("textBoxBlobKey.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.textBoxBlobKey, ((int)(resources.GetObject("textBoxBlobKey.IconPadding"))));
            this.textBoxBlobKey.Name = "textBoxBlobKey";
            this.toolTip1.SetToolTip(this.textBoxBlobKey, resources.GetString("textBoxBlobKey.ToolTip"));
            this.textBoxBlobKey.UseSystemPasswordChar = true;
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.errorProvider1.SetError(this.label3, resources.GetString("label3.Error"));
            this.errorProvider1.SetIconAlignment(this.label3, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label3.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label3, ((int)(resources.GetObject("label3.IconPadding"))));
            this.label3.Name = "label3";
            this.toolTip1.SetToolTip(this.label3, resources.GetString("label3.ToolTip"));
            // 
            // tabPageEndpoint
            // 
            resources.ApplyResources(this.tabPageEndpoint, "tabPageEndpoint");
            this.tabPageEndpoint.BackColor = System.Drawing.SystemColors.Window;
            this.tabPageEndpoint.Controls.Add(this.textBoxManagementPortal);
            this.tabPageEndpoint.Controls.Add(this.label10);
            this.tabPageEndpoint.Controls.Add(this.comboBoxMappingList);
            this.tabPageEndpoint.Controls.Add(this.buttonAddMapping);
            this.tabPageEndpoint.Controls.Add(this.textBoxAzureEndpoint);
            this.tabPageEndpoint.Controls.Add(this.label9);
            this.tabPageEndpoint.Controls.Add(this.textBoxACSBaseAddress);
            this.tabPageEndpoint.Controls.Add(this.label6);
            this.tabPageEndpoint.Controls.Add(this.textBoxScope);
            this.tabPageEndpoint.Controls.Add(this.label7);
            this.tabPageEndpoint.Controls.Add(this.textBoxAPIServer);
            this.tabPageEndpoint.Controls.Add(this.label8);
            this.tabPageEndpoint.Controls.Add(this.radioButtonOther);
            this.tabPageEndpoint.Controls.Add(this.radioButtonPartner);
            this.tabPageEndpoint.Controls.Add(this.radioButtonProd);
            this.errorProvider1.SetError(this.tabPageEndpoint, resources.GetString("tabPageEndpoint.Error"));
            this.errorProvider1.SetIconAlignment(this.tabPageEndpoint, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("tabPageEndpoint.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.tabPageEndpoint, ((int)(resources.GetObject("tabPageEndpoint.IconPadding"))));
            this.tabPageEndpoint.Name = "tabPageEndpoint";
            this.toolTip1.SetToolTip(this.tabPageEndpoint, resources.GetString("tabPageEndpoint.ToolTip"));
            // 
            // textBoxManagementPortal
            // 
            resources.ApplyResources(this.textBoxManagementPortal, "textBoxManagementPortal");
            this.textBoxManagementPortal.BackColor = System.Drawing.Color.Pink;
            this.errorProvider1.SetError(this.textBoxManagementPortal, resources.GetString("textBoxManagementPortal.Error"));
            this.errorProvider1.SetIconAlignment(this.textBoxManagementPortal, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("textBoxManagementPortal.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.textBoxManagementPortal, ((int)(resources.GetObject("textBoxManagementPortal.IconPadding"))));
            this.textBoxManagementPortal.Name = "textBoxManagementPortal";
            this.toolTip1.SetToolTip(this.textBoxManagementPortal, resources.GetString("textBoxManagementPortal.ToolTip"));
            this.textBoxManagementPortal.TextChanged += new System.EventHandler(this.textBoxURL_Validation);
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.errorProvider1.SetError(this.label10, resources.GetString("label10.Error"));
            this.errorProvider1.SetIconAlignment(this.label10, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label10.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label10, ((int)(resources.GetObject("label10.IconPadding"))));
            this.label10.Name = "label10";
            this.toolTip1.SetToolTip(this.label10, resources.GetString("label10.ToolTip"));
            // 
            // comboBoxMappingList
            // 
            resources.ApplyResources(this.comboBoxMappingList, "comboBoxMappingList");
            this.comboBoxMappingList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.errorProvider1.SetError(this.comboBoxMappingList, resources.GetString("comboBoxMappingList.Error"));
            this.comboBoxMappingList.FormattingEnabled = true;
            this.errorProvider1.SetIconAlignment(this.comboBoxMappingList, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("comboBoxMappingList.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.comboBoxMappingList, ((int)(resources.GetObject("comboBoxMappingList.IconPadding"))));
            this.comboBoxMappingList.Name = "comboBoxMappingList";
            this.toolTip1.SetToolTip(this.comboBoxMappingList, resources.GetString("comboBoxMappingList.ToolTip"));
            // 
            // buttonAddMapping
            // 
            resources.ApplyResources(this.buttonAddMapping, "buttonAddMapping");
            this.errorProvider1.SetError(this.buttonAddMapping, resources.GetString("buttonAddMapping.Error"));
            this.errorProvider1.SetIconAlignment(this.buttonAddMapping, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("buttonAddMapping.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.buttonAddMapping, ((int)(resources.GetObject("buttonAddMapping.IconPadding"))));
            this.buttonAddMapping.Name = "buttonAddMapping";
            this.toolTip1.SetToolTip(this.buttonAddMapping, resources.GetString("buttonAddMapping.ToolTip"));
            this.buttonAddMapping.UseVisualStyleBackColor = true;
            this.buttonAddMapping.Click += new System.EventHandler(this.buttonAddMapping_Click);
            // 
            // textBoxAzureEndpoint
            // 
            resources.ApplyResources(this.textBoxAzureEndpoint, "textBoxAzureEndpoint");
            this.textBoxAzureEndpoint.BackColor = System.Drawing.Color.Pink;
            this.errorProvider1.SetError(this.textBoxAzureEndpoint, resources.GetString("textBoxAzureEndpoint.Error"));
            this.errorProvider1.SetIconAlignment(this.textBoxAzureEndpoint, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("textBoxAzureEndpoint.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.textBoxAzureEndpoint, ((int)(resources.GetObject("textBoxAzureEndpoint.IconPadding"))));
            this.textBoxAzureEndpoint.Name = "textBoxAzureEndpoint";
            this.toolTip1.SetToolTip(this.textBoxAzureEndpoint, resources.GetString("textBoxAzureEndpoint.ToolTip"));
            this.textBoxAzureEndpoint.TextChanged += new System.EventHandler(this.textBoxTXT_Validation);
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.errorProvider1.SetError(this.label9, resources.GetString("label9.Error"));
            this.errorProvider1.SetIconAlignment(this.label9, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label9.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label9, ((int)(resources.GetObject("label9.IconPadding"))));
            this.label9.Name = "label9";
            this.toolTip1.SetToolTip(this.label9, resources.GetString("label9.ToolTip"));
            // 
            // textBoxACSBaseAddress
            // 
            resources.ApplyResources(this.textBoxACSBaseAddress, "textBoxACSBaseAddress");
            this.textBoxACSBaseAddress.BackColor = System.Drawing.Color.Pink;
            this.errorProvider1.SetError(this.textBoxACSBaseAddress, resources.GetString("textBoxACSBaseAddress.Error"));
            this.errorProvider1.SetIconAlignment(this.textBoxACSBaseAddress, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("textBoxACSBaseAddress.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.textBoxACSBaseAddress, ((int)(resources.GetObject("textBoxACSBaseAddress.IconPadding"))));
            this.textBoxACSBaseAddress.Name = "textBoxACSBaseAddress";
            this.toolTip1.SetToolTip(this.textBoxACSBaseAddress, resources.GetString("textBoxACSBaseAddress.ToolTip"));
            this.textBoxACSBaseAddress.TextChanged += new System.EventHandler(this.textBoxURL_Validation);
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.errorProvider1.SetError(this.label6, resources.GetString("label6.Error"));
            this.errorProvider1.SetIconAlignment(this.label6, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label6.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label6, ((int)(resources.GetObject("label6.IconPadding"))));
            this.label6.Name = "label6";
            this.toolTip1.SetToolTip(this.label6, resources.GetString("label6.ToolTip"));
            // 
            // textBoxScope
            // 
            resources.ApplyResources(this.textBoxScope, "textBoxScope");
            this.textBoxScope.BackColor = System.Drawing.Color.Pink;
            this.errorProvider1.SetError(this.textBoxScope, resources.GetString("textBoxScope.Error"));
            this.errorProvider1.SetIconAlignment(this.textBoxScope, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("textBoxScope.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.textBoxScope, ((int)(resources.GetObject("textBoxScope.IconPadding"))));
            this.textBoxScope.Name = "textBoxScope";
            this.toolTip1.SetToolTip(this.textBoxScope, resources.GetString("textBoxScope.ToolTip"));
            this.textBoxScope.TextChanged += new System.EventHandler(this.textBoxTXT_Validation);
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.errorProvider1.SetError(this.label7, resources.GetString("label7.Error"));
            this.errorProvider1.SetIconAlignment(this.label7, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label7.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label7, ((int)(resources.GetObject("label7.IconPadding"))));
            this.label7.Name = "label7";
            this.toolTip1.SetToolTip(this.label7, resources.GetString("label7.ToolTip"));
            // 
            // textBoxAPIServer
            // 
            resources.ApplyResources(this.textBoxAPIServer, "textBoxAPIServer");
            this.textBoxAPIServer.BackColor = System.Drawing.Color.Pink;
            this.errorProvider1.SetError(this.textBoxAPIServer, resources.GetString("textBoxAPIServer.Error"));
            this.errorProvider1.SetIconAlignment(this.textBoxAPIServer, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("textBoxAPIServer.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.textBoxAPIServer, ((int)(resources.GetObject("textBoxAPIServer.IconPadding"))));
            this.textBoxAPIServer.Name = "textBoxAPIServer";
            this.toolTip1.SetToolTip(this.textBoxAPIServer, resources.GetString("textBoxAPIServer.ToolTip"));
            this.textBoxAPIServer.TextChanged += new System.EventHandler(this.textBoxURL_Validation);
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.errorProvider1.SetError(this.label8, resources.GetString("label8.Error"));
            this.errorProvider1.SetIconAlignment(this.label8, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label8.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label8, ((int)(resources.GetObject("label8.IconPadding"))));
            this.label8.Name = "label8";
            this.toolTip1.SetToolTip(this.label8, resources.GetString("label8.ToolTip"));
            // 
            // radioButtonOther
            // 
            resources.ApplyResources(this.radioButtonOther, "radioButtonOther");
            this.errorProvider1.SetError(this.radioButtonOther, resources.GetString("radioButtonOther.Error"));
            this.errorProvider1.SetIconAlignment(this.radioButtonOther, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("radioButtonOther.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.radioButtonOther, ((int)(resources.GetObject("radioButtonOther.IconPadding"))));
            this.radioButtonOther.Name = "radioButtonOther";
            this.toolTip1.SetToolTip(this.radioButtonOther, resources.GetString("radioButtonOther.ToolTip"));
            this.radioButtonOther.UseVisualStyleBackColor = true;
            this.radioButtonOther.CheckedChanged += new System.EventHandler(this.radioButtonOther_CheckedChanged);
            // 
            // radioButtonPartner
            // 
            resources.ApplyResources(this.radioButtonPartner, "radioButtonPartner");
            this.errorProvider1.SetError(this.radioButtonPartner, resources.GetString("radioButtonPartner.Error"));
            this.errorProvider1.SetIconAlignment(this.radioButtonPartner, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("radioButtonPartner.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.radioButtonPartner, ((int)(resources.GetObject("radioButtonPartner.IconPadding"))));
            this.radioButtonPartner.Name = "radioButtonPartner";
            this.toolTip1.SetToolTip(this.radioButtonPartner, resources.GetString("radioButtonPartner.ToolTip"));
            this.radioButtonPartner.UseVisualStyleBackColor = true;
            // 
            // radioButtonProd
            // 
            resources.ApplyResources(this.radioButtonProd, "radioButtonProd");
            this.radioButtonProd.Checked = true;
            this.errorProvider1.SetError(this.radioButtonProd, resources.GetString("radioButtonProd.Error"));
            this.errorProvider1.SetIconAlignment(this.radioButtonProd, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("radioButtonProd.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.radioButtonProd, ((int)(resources.GetObject("radioButtonProd.IconPadding"))));
            this.radioButtonProd.Name = "radioButtonProd";
            this.radioButtonProd.TabStop = true;
            this.toolTip1.SetToolTip(this.radioButtonProd, resources.GetString("radioButtonProd.ToolTip"));
            this.radioButtonProd.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.errorProvider1.SetError(this.label5, resources.GetString("label5.Error"));
            this.label5.ForeColor = System.Drawing.Color.DarkBlue;
            this.errorProvider1.SetIconAlignment(this.label5, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label5.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label5, ((int)(resources.GetObject("label5.IconPadding"))));
            this.label5.Name = "label5";
            this.toolTip1.SetToolTip(this.label5, resources.GetString("label5.ToolTip"));
            // 
            // buttonExport
            // 
            resources.ApplyResources(this.buttonExport, "buttonExport");
            this.errorProvider1.SetError(this.buttonExport, resources.GetString("buttonExport.Error"));
            this.errorProvider1.SetIconAlignment(this.buttonExport, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("buttonExport.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.buttonExport, ((int)(resources.GetObject("buttonExport.IconPadding"))));
            this.buttonExport.Name = "buttonExport";
            this.toolTip1.SetToolTip(this.buttonExport, resources.GetString("buttonExport.ToolTip"));
            this.buttonExport.UseVisualStyleBackColor = true;
            this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // buttonImportAll
            // 
            resources.ApplyResources(this.buttonImportAll, "buttonImportAll");
            this.errorProvider1.SetError(this.buttonImportAll, resources.GetString("buttonImportAll.Error"));
            this.errorProvider1.SetIconAlignment(this.buttonImportAll, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("buttonImportAll.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.buttonImportAll, ((int)(resources.GetObject("buttonImportAll.IconPadding"))));
            this.buttonImportAll.Name = "buttonImportAll";
            this.toolTip1.SetToolTip(this.buttonImportAll, resources.GetString("buttonImportAll.ToolTip"));
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
            this.errorProvider1.SetError(this.accountmgtlink, resources.GetString("accountmgtlink.Error"));
            this.errorProvider1.SetIconAlignment(this.accountmgtlink, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("accountmgtlink.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.accountmgtlink, ((int)(resources.GetObject("accountmgtlink.IconPadding"))));
            this.accountmgtlink.Name = "accountmgtlink";
            this.accountmgtlink.TabStop = true;
            this.toolTip1.SetToolTip(this.accountmgtlink, resources.GetString("accountmgtlink.ToolTip"));
            this.accountmgtlink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.accountmgtlink_LinkClicked);
            // 
            // pictureBoxJob
            // 
            resources.ApplyResources(this.pictureBoxJob, "pictureBoxJob");
            this.errorProvider1.SetError(this.pictureBoxJob, resources.GetString("pictureBoxJob.Error"));
            this.errorProvider1.SetIconAlignment(this.pictureBoxJob, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("pictureBoxJob.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.pictureBoxJob, ((int)(resources.GetObject("pictureBoxJob.IconPadding"))));
            this.pictureBoxJob.Image = global::AMSExplorer.Bitmaps.AzureMedia_Full_Color_64_opaque;
            this.pictureBoxJob.Name = "pictureBoxJob";
            this.pictureBoxJob.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBoxJob, resources.GetString("pictureBoxJob.ToolTip"));
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.labelVersion);
            this.panel1.Controls.Add(this.buttonLogin);
            this.panel1.Controls.Add(this.buttonCancel);
            this.errorProvider1.SetError(this.panel1, resources.GetString("panel1.Error"));
            this.errorProvider1.SetIconAlignment(this.panel1, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("panel1.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.panel1, ((int)(resources.GetObject("panel1.IconPadding"))));
            this.panel1.Name = "panel1";
            this.toolTip1.SetToolTip(this.panel1, resources.GetString("panel1.ToolTip"));
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // labelVersion
            // 
            resources.ApplyResources(this.labelVersion, "labelVersion");
            this.errorProvider1.SetError(this.labelVersion, resources.GetString("labelVersion.Error"));
            this.labelVersion.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.errorProvider1.SetIconAlignment(this.labelVersion, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("labelVersion.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.labelVersion, ((int)(resources.GetObject("labelVersion.IconPadding"))));
            this.labelVersion.Name = "labelVersion";
            this.toolTip1.SetToolTip(this.labelVersion, resources.GetString("labelVersion.ToolTip"));
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            resources.ApplyResources(this.errorProvider1, "errorProvider1");
            // 
            // AMSLogin
            // 
            this.AcceptButton = this.buttonLogin;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
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
            this.Controls.Add(this.listBoxAcounts);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AMSLogin";
            this.toolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.Load += new System.EventHandler(this.AMSLogin_Load);
            this.Shown += new System.EventHandler(this.AMSLogin_Shown);
            this.tabControlAMS.ResumeLayout(false);
            this.tabPageCredentials.ResumeLayout(false);
            this.tabPageCredentials.PerformLayout();
            this.tabPageEndpoint.ResumeLayout(false);
            this.tabPageEndpoint.PerformLayout();
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
        private System.Windows.Forms.TabControl tabControlAMS;
        private System.Windows.Forms.TabPage tabPageCredentials;
        private System.Windows.Forms.TabPage tabPageEndpoint;
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
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.RadioButton radioButtonAADInteract;
        private System.Windows.Forms.RadioButton radioButtonACS;
        private System.Windows.Forms.TextBox textBoxRestAPIEndpoint;
        private System.Windows.Forms.TextBox textBoxAADtenant;
    }
}