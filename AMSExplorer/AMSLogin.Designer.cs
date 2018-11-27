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
            this.groupBoxAADMode = new System.Windows.Forms.GroupBox();
            this.radioButtonAADServicePrincipal = new System.Windows.Forms.RadioButton();
            this.radioButtonAADInteractive = new System.Windows.Forms.RadioButton();
            this.textBoxAMSResourceId = new System.Windows.Forms.TextBox();
            this.textBoxAADtenant = new System.Windows.Forms.TextBox();
            this.labelE1 = new System.Windows.Forms.Label();
            this.labelE2 = new System.Windows.Forms.Label();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxLocation = new System.Windows.Forms.TextBox();
            this.labelLocation = new System.Windows.Forms.Label();
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
            this.labelVersion = new System.Windows.Forms.Label();
            this.buttonConnectFullyInteractive = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.listViewAccounts = new System.Windows.Forms.ListView();
            this.buttonManualEntry = new System.Windows.Forms.Button();
            this.tabControlAMS.SuspendLayout();
            this.tabPageCredentials.SuspendLayout();
            this.groupBoxAADMode.SuspendLayout();
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
            this.tabControlAMS.Controls.Add(this.tabPageAAD);
            this.tabControlAMS.Name = "tabControlAMS";
            this.tabControlAMS.SelectedIndex = 0;
            // 
            // tabPageCredentials
            // 
            this.tabPageCredentials.BackColor = System.Drawing.SystemColors.Window;
            this.tabPageCredentials.Controls.Add(this.linkLabelAADAut);
            this.tabPageCredentials.Controls.Add(this.groupBoxAADMode);
            this.tabPageCredentials.Controls.Add(this.textBoxAMSResourceId);
            this.tabPageCredentials.Controls.Add(this.textBoxAADtenant);
            this.tabPageCredentials.Controls.Add(this.labelE1);
            this.tabPageCredentials.Controls.Add(this.labelE2);
            this.tabPageCredentials.Controls.Add(this.textBoxDescription);
            this.tabPageCredentials.Controls.Add(this.label2);
            this.tabPageCredentials.Controls.Add(this.textBoxLocation);
            this.tabPageCredentials.Controls.Add(this.labelLocation);
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
            // textBoxAMSResourceId
            // 
            resources.ApplyResources(this.textBoxAMSResourceId, "textBoxAMSResourceId");
            this.textBoxAMSResourceId.Name = "textBoxAMSResourceId";
            this.toolTip1.SetToolTip(this.textBoxAMSResourceId, resources.GetString("textBoxAMSResourceId.ToolTip"));
            this.textBoxAMSResourceId.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxRestAPIEndpoint_Validating);
            // 
            // textBoxAADtenant
            // 
            resources.ApplyResources(this.textBoxAADtenant, "textBoxAADtenant");
            this.textBoxAADtenant.Name = "textBoxAADtenant";
            this.toolTip1.SetToolTip(this.textBoxAADtenant, resources.GetString("textBoxAADtenant.ToolTip"));
            this.textBoxAADtenant.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxAADtenant_Validating);
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
            // textBoxLocation
            // 
            resources.ApplyResources(this.textBoxLocation, "textBoxLocation");
            this.textBoxLocation.Name = "textBoxLocation";
            this.toolTip1.SetToolTip(this.textBoxLocation, resources.GetString("textBoxLocation.ToolTip"));
            // 
            // labelLocation
            // 
            resources.ApplyResources(this.labelLocation, "labelLocation");
            this.labelLocation.Name = "labelLocation";
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
            // labelVersion
            // 
            resources.ApplyResources(this.labelVersion, "labelVersion");
            this.labelVersion.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.labelVersion.Name = "labelVersion";
            // 
            // buttonConnectFullyInteractive
            // 
            resources.ApplyResources(this.buttonConnectFullyInteractive, "buttonConnectFullyInteractive");
            this.buttonConnectFullyInteractive.Name = "buttonConnectFullyInteractive";
            this.buttonConnectFullyInteractive.UseVisualStyleBackColor = true;
            this.buttonConnectFullyInteractive.Click += new System.EventHandler(this.buttonConnectFullyInteractive_Click);
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
            // buttonManualEntry
            // 
            resources.ApplyResources(this.buttonManualEntry, "buttonManualEntry");
            this.buttonManualEntry.Name = "buttonManualEntry";
            this.buttonManualEntry.UseVisualStyleBackColor = true;
            this.buttonManualEntry.Click += new System.EventHandler(this.buttonManualEntry_Click);
            // 
            // AMSLogin
            // 
            this.AcceptButton = this.buttonLogin;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.buttonManualEntry);
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
            this.groupBoxAADMode.ResumeLayout(false);
            this.groupBoxAADMode.PerformLayout();
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
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxLocation;
        private System.Windows.Forms.Label labelLocation;
        private System.Windows.Forms.Label labelE2;
        private System.Windows.Forms.Label labelE1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonExport;
        private System.Windows.Forms.Button buttonImportAll;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.LinkLabel accountmgtlink;
        private System.Windows.Forms.PictureBox pictureBoxJob;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.TextBox textBoxAMSResourceId;
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
        private System.Windows.Forms.LinkLabel linkLabelAADAut;
        private System.Windows.Forms.ListView listViewAccounts;
        private System.Windows.Forms.Button buttonConnectFullyInteractive;
        private System.Windows.Forms.Button buttonManualEntry;
    }
}