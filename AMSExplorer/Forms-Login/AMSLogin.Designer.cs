namespace AMSExplorer
{
    partial class AmsLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AmsLogin));
            this.buttonLogin = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonDeleteAccountEntry = new System.Windows.Forms.Button();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.linkLabelAADAut = new System.Windows.Forms.LinkLabel();
            this.groupBoxAADAutMode = new System.Windows.Forms.GroupBox();
            this.radioButtonAADServicePrincipal = new System.Windows.Forms.RadioButton();
            this.radioButtonAADInteractive = new System.Windows.Forms.RadioButton();
            this.textBoxAMSResourceId = new System.Windows.Forms.TextBox();
            this.textBoxAADtenantId = new System.Windows.Forms.TextBox();
            this.labelADTenant = new System.Windows.Forms.Label();
            this.labelE2 = new System.Windows.Forms.Label();
            this.labelenteramsacct = new System.Windows.Forms.Label();
            this.buttonExport = new System.Windows.Forms.Button();
            this.buttonImportAll = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.accountmgtlink = new System.Windows.Forms.LinkLabel();
            this.pictureBoxJob = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelVersion = new System.Windows.Forms.Label();
            this.buttonPickupAccount = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.listViewAccounts = new System.Windows.Forms.ListView();
            this.linkLabelAMSOfflineDoc = new System.Windows.Forms.LinkLabel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBoxAADAutMode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxJob)).BeginInit();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
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
            this.buttonLogin.Click += new System.EventHandler(this.ButtonLogin_Click);
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
            // buttonDeleteAccountEntry
            // 
            resources.ApplyResources(this.buttonDeleteAccountEntry, "buttonDeleteAccountEntry");
            this.errorProvider1.SetError(this.buttonDeleteAccountEntry, resources.GetString("buttonDeleteAccountEntry.Error"));
            this.errorProvider1.SetIconAlignment(this.buttonDeleteAccountEntry, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("buttonDeleteAccountEntry.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.buttonDeleteAccountEntry, ((int)(resources.GetObject("buttonDeleteAccountEntry.IconPadding"))));
            this.buttonDeleteAccountEntry.Name = "buttonDeleteAccountEntry";
            this.toolTip1.SetToolTip(this.buttonDeleteAccountEntry, resources.GetString("buttonDeleteAccountEntry.ToolTip"));
            this.buttonDeleteAccountEntry.UseVisualStyleBackColor = true;
            this.buttonDeleteAccountEntry.Click += new System.EventHandler(this.ButtonDeleteAccount_Click);
            // 
            // textBoxDescription
            // 
            resources.ApplyResources(this.textBoxDescription, "textBoxDescription");
            this.errorProvider1.SetError(this.textBoxDescription, resources.GetString("textBoxDescription.Error"));
            this.errorProvider1.SetIconAlignment(this.textBoxDescription, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("textBoxDescription.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.textBoxDescription, ((int)(resources.GetObject("textBoxDescription.IconPadding"))));
            this.textBoxDescription.Name = "textBoxDescription";
            this.toolTip1.SetToolTip(this.textBoxDescription, resources.GetString("textBoxDescription.ToolTip"));
            this.textBoxDescription.TextChanged += new System.EventHandler(this.textBoxDescription_TextChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.errorProvider1.SetError(this.label1, resources.GetString("label1.Error"));
            this.errorProvider1.SetIconAlignment(this.label1, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label1.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label1, ((int)(resources.GetObject("label1.IconPadding"))));
            this.label1.Name = "label1";
            this.toolTip1.SetToolTip(this.label1, resources.GetString("label1.ToolTip"));
            // 
            // linkLabelAADAut
            // 
            resources.ApplyResources(this.linkLabelAADAut, "linkLabelAADAut");
            this.errorProvider1.SetError(this.linkLabelAADAut, resources.GetString("linkLabelAADAut.Error"));
            this.errorProvider1.SetIconAlignment(this.linkLabelAADAut, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("linkLabelAADAut.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.linkLabelAADAut, ((int)(resources.GetObject("linkLabelAADAut.IconPadding"))));
            this.linkLabelAADAut.Name = "linkLabelAADAut";
            this.linkLabelAADAut.TabStop = true;
            this.toolTip1.SetToolTip(this.linkLabelAADAut, resources.GetString("linkLabelAADAut.ToolTip"));
            this.linkLabelAADAut.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Accountmgtlink_LinkClicked);
            // 
            // groupBoxAADAutMode
            // 
            resources.ApplyResources(this.groupBoxAADAutMode, "groupBoxAADAutMode");
            this.groupBoxAADAutMode.Controls.Add(this.radioButtonAADServicePrincipal);
            this.groupBoxAADAutMode.Controls.Add(this.radioButtonAADInteractive);
            this.errorProvider1.SetError(this.groupBoxAADAutMode, resources.GetString("groupBoxAADAutMode.Error"));
            this.errorProvider1.SetIconAlignment(this.groupBoxAADAutMode, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("groupBoxAADAutMode.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.groupBoxAADAutMode, ((int)(resources.GetObject("groupBoxAADAutMode.IconPadding"))));
            this.groupBoxAADAutMode.Name = "groupBoxAADAutMode";
            this.groupBoxAADAutMode.TabStop = false;
            this.toolTip1.SetToolTip(this.groupBoxAADAutMode, resources.GetString("groupBoxAADAutMode.ToolTip"));
            // 
            // radioButtonAADServicePrincipal
            // 
            resources.ApplyResources(this.radioButtonAADServicePrincipal, "radioButtonAADServicePrincipal");
            this.errorProvider1.SetError(this.radioButtonAADServicePrincipal, resources.GetString("radioButtonAADServicePrincipal.Error"));
            this.errorProvider1.SetIconAlignment(this.radioButtonAADServicePrincipal, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("radioButtonAADServicePrincipal.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.radioButtonAADServicePrincipal, ((int)(resources.GetObject("radioButtonAADServicePrincipal.IconPadding"))));
            this.radioButtonAADServicePrincipal.Name = "radioButtonAADServicePrincipal";
            this.toolTip1.SetToolTip(this.radioButtonAADServicePrincipal, resources.GetString("radioButtonAADServicePrincipal.ToolTip"));
            this.radioButtonAADServicePrincipal.UseVisualStyleBackColor = true;
            // 
            // radioButtonAADInteractive
            // 
            resources.ApplyResources(this.radioButtonAADInteractive, "radioButtonAADInteractive");
            this.radioButtonAADInteractive.Checked = true;
            this.errorProvider1.SetError(this.radioButtonAADInteractive, resources.GetString("radioButtonAADInteractive.Error"));
            this.errorProvider1.SetIconAlignment(this.radioButtonAADInteractive, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("radioButtonAADInteractive.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.radioButtonAADInteractive, ((int)(resources.GetObject("radioButtonAADInteractive.IconPadding"))));
            this.radioButtonAADInteractive.Name = "radioButtonAADInteractive";
            this.radioButtonAADInteractive.TabStop = true;
            this.toolTip1.SetToolTip(this.radioButtonAADInteractive, resources.GetString("radioButtonAADInteractive.ToolTip"));
            this.radioButtonAADInteractive.UseVisualStyleBackColor = true;
            // 
            // textBoxAMSResourceId
            // 
            resources.ApplyResources(this.textBoxAMSResourceId, "textBoxAMSResourceId");
            this.errorProvider1.SetError(this.textBoxAMSResourceId, resources.GetString("textBoxAMSResourceId.Error"));
            this.errorProvider1.SetIconAlignment(this.textBoxAMSResourceId, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("textBoxAMSResourceId.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.textBoxAMSResourceId, ((int)(resources.GetObject("textBoxAMSResourceId.IconPadding"))));
            this.textBoxAMSResourceId.Name = "textBoxAMSResourceId";
            this.toolTip1.SetToolTip(this.textBoxAMSResourceId, resources.GetString("textBoxAMSResourceId.ToolTip"));
            // 
            // textBoxAADtenantId
            // 
            resources.ApplyResources(this.textBoxAADtenantId, "textBoxAADtenantId");
            this.errorProvider1.SetError(this.textBoxAADtenantId, resources.GetString("textBoxAADtenantId.Error"));
            this.errorProvider1.SetIconAlignment(this.textBoxAADtenantId, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("textBoxAADtenantId.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.textBoxAADtenantId, ((int)(resources.GetObject("textBoxAADtenantId.IconPadding"))));
            this.textBoxAADtenantId.Name = "textBoxAADtenantId";
            this.toolTip1.SetToolTip(this.textBoxAADtenantId, resources.GetString("textBoxAADtenantId.ToolTip"));
            // 
            // labelADTenant
            // 
            resources.ApplyResources(this.labelADTenant, "labelADTenant");
            this.errorProvider1.SetError(this.labelADTenant, resources.GetString("labelADTenant.Error"));
            this.errorProvider1.SetIconAlignment(this.labelADTenant, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("labelADTenant.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.labelADTenant, ((int)(resources.GetObject("labelADTenant.IconPadding"))));
            this.labelADTenant.Name = "labelADTenant";
            this.toolTip1.SetToolTip(this.labelADTenant, resources.GetString("labelADTenant.ToolTip"));
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
            // labelenteramsacct
            // 
            resources.ApplyResources(this.labelenteramsacct, "labelenteramsacct");
            this.errorProvider1.SetError(this.labelenteramsacct, resources.GetString("labelenteramsacct.Error"));
            this.labelenteramsacct.ForeColor = System.Drawing.Color.DarkBlue;
            this.errorProvider1.SetIconAlignment(this.labelenteramsacct, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("labelenteramsacct.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.labelenteramsacct, ((int)(resources.GetObject("labelenteramsacct.IconPadding"))));
            this.labelenteramsacct.Name = "labelenteramsacct";
            this.toolTip1.SetToolTip(this.labelenteramsacct, resources.GetString("labelenteramsacct.ToolTip"));
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
            this.buttonExport.Click += new System.EventHandler(this.ButtonExport_Click);
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
            this.buttonImportAll.Click += new System.EventHandler(this.ButtonImportAll_Click);
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
            this.accountmgtlink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Accountmgtlink_LinkClicked);
            // 
            // pictureBoxJob
            // 
            resources.ApplyResources(this.pictureBoxJob, "pictureBoxJob");
            this.errorProvider1.SetError(this.pictureBoxJob, resources.GetString("pictureBoxJob.Error"));
            this.errorProvider1.SetIconAlignment(this.pictureBoxJob, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("pictureBoxJob.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.pictureBoxJob, ((int)(resources.GetObject("pictureBoxJob.IconPadding"))));
            this.pictureBoxJob.Image = global::AMSExplorer.Bitmaps.AzureMedia_Full_Color_new;
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
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel1_Paint);
            // 
            // labelVersion
            // 
            resources.ApplyResources(this.labelVersion, "labelVersion");
            this.errorProvider1.SetError(this.labelVersion, resources.GetString("labelVersion.Error"));
            this.labelVersion.ForeColor = System.Drawing.SystemColors.ControlText;
            this.errorProvider1.SetIconAlignment(this.labelVersion, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("labelVersion.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.labelVersion, ((int)(resources.GetObject("labelVersion.IconPadding"))));
            this.labelVersion.Name = "labelVersion";
            this.toolTip1.SetToolTip(this.labelVersion, resources.GetString("labelVersion.ToolTip"));
            // 
            // buttonPickupAccount
            // 
            resources.ApplyResources(this.buttonPickupAccount, "buttonPickupAccount");
            this.errorProvider1.SetError(this.buttonPickupAccount, resources.GetString("buttonPickupAccount.Error"));
            this.errorProvider1.SetIconAlignment(this.buttonPickupAccount, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("buttonPickupAccount.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.buttonPickupAccount, ((int)(resources.GetObject("buttonPickupAccount.IconPadding"))));
            this.buttonPickupAccount.Name = "buttonPickupAccount";
            this.toolTip1.SetToolTip(this.buttonPickupAccount, resources.GetString("buttonPickupAccount.ToolTip"));
            this.buttonPickupAccount.UseVisualStyleBackColor = true;
            this.buttonPickupAccount.Click += new System.EventHandler(this.buttonPickupAccount_Click);
            // 
            // listViewAccounts
            // 
            resources.ApplyResources(this.listViewAccounts, "listViewAccounts");
            this.errorProvider1.SetError(this.listViewAccounts, resources.GetString("listViewAccounts.Error"));
            this.listViewAccounts.FullRowSelect = true;
            this.listViewAccounts.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewAccounts.HideSelection = false;
            this.errorProvider1.SetIconAlignment(this.listViewAccounts, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("listViewAccounts.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.listViewAccounts, ((int)(resources.GetObject("listViewAccounts.IconPadding"))));
            this.listViewAccounts.Name = "listViewAccounts";
            this.listViewAccounts.ShowItemToolTips = true;
            this.toolTip1.SetToolTip(this.listViewAccounts, resources.GetString("listViewAccounts.ToolTip"));
            this.listViewAccounts.UseCompatibleStateImageBehavior = false;
            this.listViewAccounts.View = System.Windows.Forms.View.List;
            this.listViewAccounts.SelectedIndexChanged += new System.EventHandler(this.ListViewAccounts_SelectedIndexChanged);
            this.listViewAccounts.DoubleClick += new System.EventHandler(this.ListBoxAcounts_DoubleClick);
            this.listViewAccounts.DpiChangedAfterParent += new System.EventHandler(this.AmsLogin_DpiChangedAfterParent);
            // 
            // linkLabelAMSOfflineDoc
            // 
            resources.ApplyResources(this.linkLabelAMSOfflineDoc, "linkLabelAMSOfflineDoc");
            this.errorProvider1.SetError(this.linkLabelAMSOfflineDoc, resources.GetString("linkLabelAMSOfflineDoc.Error"));
            this.errorProvider1.SetIconAlignment(this.linkLabelAMSOfflineDoc, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("linkLabelAMSOfflineDoc.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.linkLabelAMSOfflineDoc, ((int)(resources.GetObject("linkLabelAMSOfflineDoc.IconPadding"))));
            this.linkLabelAMSOfflineDoc.Name = "linkLabelAMSOfflineDoc";
            this.linkLabelAMSOfflineDoc.TabStop = true;
            this.toolTip1.SetToolTip(this.linkLabelAMSOfflineDoc, resources.GetString("linkLabelAMSOfflineDoc.ToolTip"));
            this.linkLabelAMSOfflineDoc.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelAMSOfflineDoc_LinkClicked);
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.labelE2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.textBoxAMSResourceId, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelADTenant, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.textBoxAADtenantId, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.textBoxDescription, 0, 5);
            this.errorProvider1.SetError(this.tableLayoutPanel1, resources.GetString("tableLayoutPanel1.Error"));
            this.errorProvider1.SetIconAlignment(this.tableLayoutPanel1, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("tableLayoutPanel1.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.tableLayoutPanel1, ((int)(resources.GetObject("tableLayoutPanel1.IconPadding"))));
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.toolTip1.SetToolTip(this.tableLayoutPanel1, resources.GetString("tableLayoutPanel1.ToolTip"));
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            resources.ApplyResources(this.errorProvider1, "errorProvider1");
            // 
            // AmsLogin
            // 
            this.AcceptButton = this.buttonLogin;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.linkLabelAMSOfflineDoc);
            this.Controls.Add(this.listViewAccounts);
            this.Controls.Add(this.linkLabelAADAut);
            this.Controls.Add(this.groupBoxAADAutMode);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.buttonPickupAccount);
            this.Controls.Add(this.pictureBoxJob);
            this.Controls.Add(this.accountmgtlink);
            this.Controls.Add(this.buttonImportAll);
            this.Controls.Add(this.buttonExport);
            this.Controls.Add(this.labelenteramsacct);
            this.Controls.Add(this.buttonDeleteAccountEntry);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AmsLogin";
            this.toolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.Load += new System.EventHandler(this.AMSLogin_Load);
            this.Shown += new System.EventHandler(this.AMSLogin_ShownAsync);
            this.DpiChanged += new System.Windows.Forms.DpiChangedEventHandler(this.AmsLogin_DpiChanged);
            this.groupBoxAADAutMode.ResumeLayout(false);
            this.groupBoxAADAutMode.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxJob)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonDeleteAccountEntry;
        private System.Windows.Forms.Label labelE2;
        private System.Windows.Forms.Label labelADTenant;
        private System.Windows.Forms.Label labelenteramsacct;
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
        private System.Windows.Forms.TextBox textBoxAADtenantId;
        private System.Windows.Forms.GroupBox groupBoxAADAutMode;
        private System.Windows.Forms.RadioButton radioButtonAADServicePrincipal;
        private System.Windows.Forms.RadioButton radioButtonAADInteractive;
        private System.Windows.Forms.LinkLabel linkLabelAADAut;
        private System.Windows.Forms.ListView listViewAccounts;
        private System.Windows.Forms.Button buttonPickupAccount;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel linkLabelAMSOfflineDoc;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}