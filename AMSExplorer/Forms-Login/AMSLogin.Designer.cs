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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AmsLogin));
            buttonLogin = new System.Windows.Forms.Button();
            buttonCancel = new System.Windows.Forms.Button();
            buttonDeleteAccountEntry = new System.Windows.Forms.Button();
            textBoxDescription = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            linkLabelAADAut = new System.Windows.Forms.LinkLabel();
            groupBoxAADAutMode = new System.Windows.Forms.GroupBox();
            radioButtonAADServicePrincipal = new System.Windows.Forms.RadioButton();
            radioButtonAADInteractive = new System.Windows.Forms.RadioButton();
            textBoxResourceGroup = new System.Windows.Forms.TextBox();
            textBoxAADtenantId = new System.Windows.Forms.TextBox();
            labelADTenant = new System.Windows.Forms.Label();
            labelE2 = new System.Windows.Forms.Label();
            labelenteramsacct = new System.Windows.Forms.Label();
            buttonExport = new System.Windows.Forms.Button();
            buttonImportAll = new System.Windows.Forms.Button();
            openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            pictureBoxJob = new System.Windows.Forms.PictureBox();
            panel1 = new System.Windows.Forms.Panel();
            labelVersion = new System.Windows.Forms.Label();
            buttonPickupAccount = new System.Windows.Forms.Button();
            toolTip1 = new System.Windows.Forms.ToolTip(components);
            listViewAccounts = new System.Windows.Forms.ListView();
            linkLabelAMSOfflineDoc = new System.Windows.Forms.LinkLabel();
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            textBoxSubscription = new System.Windows.Forms.TextBox();
            label2 = new System.Windows.Forms.Label();
            errorProvider1 = new System.Windows.Forms.ErrorProvider(components);
            groupBoxAADAutMode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxJob).BeginInit();
            panel1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // buttonLogin
            // 
            resources.ApplyResources(buttonLogin, "buttonLogin");
            errorProvider1.SetError(buttonLogin, resources.GetString("buttonLogin.Error"));
            errorProvider1.SetIconAlignment(buttonLogin, (System.Windows.Forms.ErrorIconAlignment)resources.GetObject("buttonLogin.IconAlignment"));
            errorProvider1.SetIconPadding(buttonLogin, (int)resources.GetObject("buttonLogin.IconPadding"));
            buttonLogin.Name = "buttonLogin";
            toolTip1.SetToolTip(buttonLogin, resources.GetString("buttonLogin.ToolTip"));
            buttonLogin.UseVisualStyleBackColor = true;
            buttonLogin.Click += ButtonLogin_Click;
            // 
            // buttonCancel
            // 
            resources.ApplyResources(buttonCancel, "buttonCancel");
            buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            errorProvider1.SetError(buttonCancel, resources.GetString("buttonCancel.Error"));
            errorProvider1.SetIconAlignment(buttonCancel, (System.Windows.Forms.ErrorIconAlignment)resources.GetObject("buttonCancel.IconAlignment"));
            errorProvider1.SetIconPadding(buttonCancel, (int)resources.GetObject("buttonCancel.IconPadding"));
            buttonCancel.Name = "buttonCancel";
            toolTip1.SetToolTip(buttonCancel, resources.GetString("buttonCancel.ToolTip"));
            buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonDeleteAccountEntry
            // 
            resources.ApplyResources(buttonDeleteAccountEntry, "buttonDeleteAccountEntry");
            errorProvider1.SetError(buttonDeleteAccountEntry, resources.GetString("buttonDeleteAccountEntry.Error"));
            errorProvider1.SetIconAlignment(buttonDeleteAccountEntry, (System.Windows.Forms.ErrorIconAlignment)resources.GetObject("buttonDeleteAccountEntry.IconAlignment"));
            errorProvider1.SetIconPadding(buttonDeleteAccountEntry, (int)resources.GetObject("buttonDeleteAccountEntry.IconPadding"));
            buttonDeleteAccountEntry.Name = "buttonDeleteAccountEntry";
            toolTip1.SetToolTip(buttonDeleteAccountEntry, resources.GetString("buttonDeleteAccountEntry.ToolTip"));
            buttonDeleteAccountEntry.UseVisualStyleBackColor = true;
            buttonDeleteAccountEntry.Click += ButtonDeleteAccount_Click;
            // 
            // textBoxDescription
            // 
            resources.ApplyResources(textBoxDescription, "textBoxDescription");
            errorProvider1.SetError(textBoxDescription, resources.GetString("textBoxDescription.Error"));
            errorProvider1.SetIconAlignment(textBoxDescription, (System.Windows.Forms.ErrorIconAlignment)resources.GetObject("textBoxDescription.IconAlignment"));
            errorProvider1.SetIconPadding(textBoxDescription, (int)resources.GetObject("textBoxDescription.IconPadding"));
            textBoxDescription.Name = "textBoxDescription";
            toolTip1.SetToolTip(textBoxDescription, resources.GetString("textBoxDescription.ToolTip"));
            textBoxDescription.TextChanged += textBoxDescription_TextChanged;
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            errorProvider1.SetError(label1, resources.GetString("label1.Error"));
            errorProvider1.SetIconAlignment(label1, (System.Windows.Forms.ErrorIconAlignment)resources.GetObject("label1.IconAlignment"));
            errorProvider1.SetIconPadding(label1, (int)resources.GetObject("label1.IconPadding"));
            label1.Name = "label1";
            toolTip1.SetToolTip(label1, resources.GetString("label1.ToolTip"));
            // 
            // linkLabelAADAut
            // 
            resources.ApplyResources(linkLabelAADAut, "linkLabelAADAut");
            errorProvider1.SetError(linkLabelAADAut, resources.GetString("linkLabelAADAut.Error"));
            errorProvider1.SetIconAlignment(linkLabelAADAut, (System.Windows.Forms.ErrorIconAlignment)resources.GetObject("linkLabelAADAut.IconAlignment"));
            errorProvider1.SetIconPadding(linkLabelAADAut, (int)resources.GetObject("linkLabelAADAut.IconPadding"));
            linkLabelAADAut.Name = "linkLabelAADAut";
            linkLabelAADAut.TabStop = true;
            toolTip1.SetToolTip(linkLabelAADAut, resources.GetString("linkLabelAADAut.ToolTip"));
            linkLabelAADAut.LinkClicked += Accountmgtlink_LinkClicked;
            // 
            // groupBoxAADAutMode
            // 
            resources.ApplyResources(groupBoxAADAutMode, "groupBoxAADAutMode");
            groupBoxAADAutMode.Controls.Add(radioButtonAADServicePrincipal);
            groupBoxAADAutMode.Controls.Add(radioButtonAADInteractive);
            errorProvider1.SetError(groupBoxAADAutMode, resources.GetString("groupBoxAADAutMode.Error"));
            errorProvider1.SetIconAlignment(groupBoxAADAutMode, (System.Windows.Forms.ErrorIconAlignment)resources.GetObject("groupBoxAADAutMode.IconAlignment"));
            errorProvider1.SetIconPadding(groupBoxAADAutMode, (int)resources.GetObject("groupBoxAADAutMode.IconPadding"));
            groupBoxAADAutMode.Name = "groupBoxAADAutMode";
            groupBoxAADAutMode.TabStop = false;
            toolTip1.SetToolTip(groupBoxAADAutMode, resources.GetString("groupBoxAADAutMode.ToolTip"));
            // 
            // radioButtonAADServicePrincipal
            // 
            resources.ApplyResources(radioButtonAADServicePrincipal, "radioButtonAADServicePrincipal");
            errorProvider1.SetError(radioButtonAADServicePrincipal, resources.GetString("radioButtonAADServicePrincipal.Error"));
            errorProvider1.SetIconAlignment(radioButtonAADServicePrincipal, (System.Windows.Forms.ErrorIconAlignment)resources.GetObject("radioButtonAADServicePrincipal.IconAlignment"));
            errorProvider1.SetIconPadding(radioButtonAADServicePrincipal, (int)resources.GetObject("radioButtonAADServicePrincipal.IconPadding"));
            radioButtonAADServicePrincipal.Name = "radioButtonAADServicePrincipal";
            toolTip1.SetToolTip(radioButtonAADServicePrincipal, resources.GetString("radioButtonAADServicePrincipal.ToolTip"));
            radioButtonAADServicePrincipal.UseVisualStyleBackColor = true;
            // 
            // radioButtonAADInteractive
            // 
            resources.ApplyResources(radioButtonAADInteractive, "radioButtonAADInteractive");
            radioButtonAADInteractive.Checked = true;
            errorProvider1.SetError(radioButtonAADInteractive, resources.GetString("radioButtonAADInteractive.Error"));
            errorProvider1.SetIconAlignment(radioButtonAADInteractive, (System.Windows.Forms.ErrorIconAlignment)resources.GetObject("radioButtonAADInteractive.IconAlignment"));
            errorProvider1.SetIconPadding(radioButtonAADInteractive, (int)resources.GetObject("radioButtonAADInteractive.IconPadding"));
            radioButtonAADInteractive.Name = "radioButtonAADInteractive";
            radioButtonAADInteractive.TabStop = true;
            toolTip1.SetToolTip(radioButtonAADInteractive, resources.GetString("radioButtonAADInteractive.ToolTip"));
            radioButtonAADInteractive.UseVisualStyleBackColor = true;
            // 
            // textBoxResourceGroup
            // 
            resources.ApplyResources(textBoxResourceGroup, "textBoxResourceGroup");
            errorProvider1.SetError(textBoxResourceGroup, resources.GetString("textBoxResourceGroup.Error"));
            errorProvider1.SetIconAlignment(textBoxResourceGroup, (System.Windows.Forms.ErrorIconAlignment)resources.GetObject("textBoxResourceGroup.IconAlignment"));
            errorProvider1.SetIconPadding(textBoxResourceGroup, (int)resources.GetObject("textBoxResourceGroup.IconPadding"));
            textBoxResourceGroup.Name = "textBoxResourceGroup";
            toolTip1.SetToolTip(textBoxResourceGroup, resources.GetString("textBoxResourceGroup.ToolTip"));
            // 
            // textBoxAADtenantId
            // 
            resources.ApplyResources(textBoxAADtenantId, "textBoxAADtenantId");
            errorProvider1.SetError(textBoxAADtenantId, resources.GetString("textBoxAADtenantId.Error"));
            errorProvider1.SetIconAlignment(textBoxAADtenantId, (System.Windows.Forms.ErrorIconAlignment)resources.GetObject("textBoxAADtenantId.IconAlignment"));
            errorProvider1.SetIconPadding(textBoxAADtenantId, (int)resources.GetObject("textBoxAADtenantId.IconPadding"));
            textBoxAADtenantId.Name = "textBoxAADtenantId";
            toolTip1.SetToolTip(textBoxAADtenantId, resources.GetString("textBoxAADtenantId.ToolTip"));
            // 
            // labelADTenant
            // 
            resources.ApplyResources(labelADTenant, "labelADTenant");
            errorProvider1.SetError(labelADTenant, resources.GetString("labelADTenant.Error"));
            errorProvider1.SetIconAlignment(labelADTenant, (System.Windows.Forms.ErrorIconAlignment)resources.GetObject("labelADTenant.IconAlignment"));
            errorProvider1.SetIconPadding(labelADTenant, (int)resources.GetObject("labelADTenant.IconPadding"));
            labelADTenant.Name = "labelADTenant";
            toolTip1.SetToolTip(labelADTenant, resources.GetString("labelADTenant.ToolTip"));
            // 
            // labelE2
            // 
            resources.ApplyResources(labelE2, "labelE2");
            errorProvider1.SetError(labelE2, resources.GetString("labelE2.Error"));
            errorProvider1.SetIconAlignment(labelE2, (System.Windows.Forms.ErrorIconAlignment)resources.GetObject("labelE2.IconAlignment"));
            errorProvider1.SetIconPadding(labelE2, (int)resources.GetObject("labelE2.IconPadding"));
            labelE2.Name = "labelE2";
            toolTip1.SetToolTip(labelE2, resources.GetString("labelE2.ToolTip"));
            // 
            // labelenteramsacct
            // 
            resources.ApplyResources(labelenteramsacct, "labelenteramsacct");
            errorProvider1.SetError(labelenteramsacct, resources.GetString("labelenteramsacct.Error"));
            labelenteramsacct.ForeColor = System.Drawing.Color.DarkBlue;
            errorProvider1.SetIconAlignment(labelenteramsacct, (System.Windows.Forms.ErrorIconAlignment)resources.GetObject("labelenteramsacct.IconAlignment"));
            errorProvider1.SetIconPadding(labelenteramsacct, (int)resources.GetObject("labelenteramsacct.IconPadding"));
            labelenteramsacct.Name = "labelenteramsacct";
            toolTip1.SetToolTip(labelenteramsacct, resources.GetString("labelenteramsacct.ToolTip"));
            // 
            // buttonExport
            // 
            resources.ApplyResources(buttonExport, "buttonExport");
            errorProvider1.SetError(buttonExport, resources.GetString("buttonExport.Error"));
            errorProvider1.SetIconAlignment(buttonExport, (System.Windows.Forms.ErrorIconAlignment)resources.GetObject("buttonExport.IconAlignment"));
            errorProvider1.SetIconPadding(buttonExport, (int)resources.GetObject("buttonExport.IconPadding"));
            buttonExport.Name = "buttonExport";
            toolTip1.SetToolTip(buttonExport, resources.GetString("buttonExport.ToolTip"));
            buttonExport.UseVisualStyleBackColor = true;
            buttonExport.Click += ButtonExport_Click;
            // 
            // buttonImportAll
            // 
            resources.ApplyResources(buttonImportAll, "buttonImportAll");
            errorProvider1.SetError(buttonImportAll, resources.GetString("buttonImportAll.Error"));
            errorProvider1.SetIconAlignment(buttonImportAll, (System.Windows.Forms.ErrorIconAlignment)resources.GetObject("buttonImportAll.IconAlignment"));
            errorProvider1.SetIconPadding(buttonImportAll, (int)resources.GetObject("buttonImportAll.IconPadding"));
            buttonImportAll.Name = "buttonImportAll";
            toolTip1.SetToolTip(buttonImportAll, resources.GetString("buttonImportAll.ToolTip"));
            buttonImportAll.UseVisualStyleBackColor = true;
            buttonImportAll.Click += ButtonImportAll_Click;
            // 
            // openFileDialog1
            // 
            openFileDialog1.DefaultExt = "json";
            resources.ApplyResources(openFileDialog1, "openFileDialog1");
            // 
            // saveFileDialog1
            // 
            saveFileDialog1.DefaultExt = "json";
            resources.ApplyResources(saveFileDialog1, "saveFileDialog1");
            // 
            // pictureBoxJob
            // 
            resources.ApplyResources(pictureBoxJob, "pictureBoxJob");
            errorProvider1.SetError(pictureBoxJob, resources.GetString("pictureBoxJob.Error"));
            errorProvider1.SetIconAlignment(pictureBoxJob, (System.Windows.Forms.ErrorIconAlignment)resources.GetObject("pictureBoxJob.IconAlignment"));
            errorProvider1.SetIconPadding(pictureBoxJob, (int)resources.GetObject("pictureBoxJob.IconPadding"));
            pictureBoxJob.Name = "pictureBoxJob";
            pictureBoxJob.TabStop = false;
            toolTip1.SetToolTip(pictureBoxJob, resources.GetString("pictureBoxJob.ToolTip"));
            // 
            // panel1
            // 
            resources.ApplyResources(panel1, "panel1");
            panel1.BackColor = System.Drawing.SystemColors.Control;
            panel1.Controls.Add(labelVersion);
            panel1.Controls.Add(buttonLogin);
            panel1.Controls.Add(buttonCancel);
            errorProvider1.SetError(panel1, resources.GetString("panel1.Error"));
            errorProvider1.SetIconAlignment(panel1, (System.Windows.Forms.ErrorIconAlignment)resources.GetObject("panel1.IconAlignment"));
            errorProvider1.SetIconPadding(panel1, (int)resources.GetObject("panel1.IconPadding"));
            panel1.Name = "panel1";
            toolTip1.SetToolTip(panel1, resources.GetString("panel1.ToolTip"));
            panel1.Paint += Panel1_Paint;
            // 
            // labelVersion
            // 
            resources.ApplyResources(labelVersion, "labelVersion");
            errorProvider1.SetError(labelVersion, resources.GetString("labelVersion.Error"));
            labelVersion.ForeColor = System.Drawing.SystemColors.ControlText;
            errorProvider1.SetIconAlignment(labelVersion, (System.Windows.Forms.ErrorIconAlignment)resources.GetObject("labelVersion.IconAlignment"));
            errorProvider1.SetIconPadding(labelVersion, (int)resources.GetObject("labelVersion.IconPadding"));
            labelVersion.Name = "labelVersion";
            toolTip1.SetToolTip(labelVersion, resources.GetString("labelVersion.ToolTip"));
            // 
            // buttonPickupAccount
            // 
            resources.ApplyResources(buttonPickupAccount, "buttonPickupAccount");
            errorProvider1.SetError(buttonPickupAccount, resources.GetString("buttonPickupAccount.Error"));
            errorProvider1.SetIconAlignment(buttonPickupAccount, (System.Windows.Forms.ErrorIconAlignment)resources.GetObject("buttonPickupAccount.IconAlignment"));
            errorProvider1.SetIconPadding(buttonPickupAccount, (int)resources.GetObject("buttonPickupAccount.IconPadding"));
            buttonPickupAccount.Name = "buttonPickupAccount";
            toolTip1.SetToolTip(buttonPickupAccount, resources.GetString("buttonPickupAccount.ToolTip"));
            buttonPickupAccount.UseVisualStyleBackColor = true;
            buttonPickupAccount.Click += buttonPickupAccount_Click;
            // 
            // listViewAccounts
            // 
            resources.ApplyResources(listViewAccounts, "listViewAccounts");
            errorProvider1.SetError(listViewAccounts, resources.GetString("listViewAccounts.Error"));
            listViewAccounts.FullRowSelect = true;
            listViewAccounts.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            errorProvider1.SetIconAlignment(listViewAccounts, (System.Windows.Forms.ErrorIconAlignment)resources.GetObject("listViewAccounts.IconAlignment"));
            errorProvider1.SetIconPadding(listViewAccounts, (int)resources.GetObject("listViewAccounts.IconPadding"));
            listViewAccounts.Name = "listViewAccounts";
            listViewAccounts.ShowItemToolTips = true;
            toolTip1.SetToolTip(listViewAccounts, resources.GetString("listViewAccounts.ToolTip"));
            listViewAccounts.UseCompatibleStateImageBehavior = false;
            listViewAccounts.View = System.Windows.Forms.View.List;
            listViewAccounts.SelectedIndexChanged += ListViewAccounts_SelectedIndexChanged;
            listViewAccounts.DoubleClick += ListBoxAcounts_DoubleClick;
            listViewAccounts.DpiChangedAfterParent += AmsLogin_DpiChangedAfterParent;
            // 
            // linkLabelAMSOfflineDoc
            // 
            resources.ApplyResources(linkLabelAMSOfflineDoc, "linkLabelAMSOfflineDoc");
            errorProvider1.SetError(linkLabelAMSOfflineDoc, resources.GetString("linkLabelAMSOfflineDoc.Error"));
            errorProvider1.SetIconAlignment(linkLabelAMSOfflineDoc, (System.Windows.Forms.ErrorIconAlignment)resources.GetObject("linkLabelAMSOfflineDoc.IconAlignment"));
            errorProvider1.SetIconPadding(linkLabelAMSOfflineDoc, (int)resources.GetObject("linkLabelAMSOfflineDoc.IconPadding"));
            linkLabelAMSOfflineDoc.Name = "linkLabelAMSOfflineDoc";
            linkLabelAMSOfflineDoc.TabStop = true;
            toolTip1.SetToolTip(linkLabelAMSOfflineDoc, resources.GetString("linkLabelAMSOfflineDoc.ToolTip"));
            linkLabelAMSOfflineDoc.LinkClicked += linkLabelAMSOfflineDoc_LinkClicked;
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(tableLayoutPanel1, "tableLayoutPanel1");
            tableLayoutPanel1.Controls.Add(textBoxSubscription, 0, 3);
            tableLayoutPanel1.Controls.Add(label2, 0, 2);
            tableLayoutPanel1.Controls.Add(textBoxDescription, 0, 7);
            tableLayoutPanel1.Controls.Add(label1, 0, 6);
            tableLayoutPanel1.Controls.Add(textBoxResourceGroup, 0, 5);
            tableLayoutPanel1.Controls.Add(labelE2, 0, 4);
            tableLayoutPanel1.Controls.Add(labelADTenant, 0, 0);
            tableLayoutPanel1.Controls.Add(textBoxAADtenantId, 0, 1);
            errorProvider1.SetError(tableLayoutPanel1, resources.GetString("tableLayoutPanel1.Error"));
            errorProvider1.SetIconAlignment(tableLayoutPanel1, (System.Windows.Forms.ErrorIconAlignment)resources.GetObject("tableLayoutPanel1.IconAlignment"));
            errorProvider1.SetIconPadding(tableLayoutPanel1, (int)resources.GetObject("tableLayoutPanel1.IconPadding"));
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            toolTip1.SetToolTip(tableLayoutPanel1, resources.GetString("tableLayoutPanel1.ToolTip"));
            // 
            // textBoxSubscription
            // 
            resources.ApplyResources(textBoxSubscription, "textBoxSubscription");
            errorProvider1.SetError(textBoxSubscription, resources.GetString("textBoxSubscription.Error"));
            errorProvider1.SetIconAlignment(textBoxSubscription, (System.Windows.Forms.ErrorIconAlignment)resources.GetObject("textBoxSubscription.IconAlignment"));
            errorProvider1.SetIconPadding(textBoxSubscription, (int)resources.GetObject("textBoxSubscription.IconPadding"));
            textBoxSubscription.Name = "textBoxSubscription";
            toolTip1.SetToolTip(textBoxSubscription, resources.GetString("textBoxSubscription.ToolTip"));
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            errorProvider1.SetError(label2, resources.GetString("label2.Error"));
            errorProvider1.SetIconAlignment(label2, (System.Windows.Forms.ErrorIconAlignment)resources.GetObject("label2.IconAlignment"));
            errorProvider1.SetIconPadding(label2, (int)resources.GetObject("label2.IconPadding"));
            label2.Name = "label2";
            toolTip1.SetToolTip(label2, resources.GetString("label2.ToolTip"));
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            resources.ApplyResources(errorProvider1, "errorProvider1");
            // 
            // AmsLogin
            // 
            AcceptButton = buttonLogin;
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            BackColor = System.Drawing.Color.WhiteSmoke;
            CancelButton = buttonCancel;
            Controls.Add(tableLayoutPanel1);
            Controls.Add(linkLabelAMSOfflineDoc);
            Controls.Add(listViewAccounts);
            Controls.Add(linkLabelAADAut);
            Controls.Add(groupBoxAADAutMode);
            Controls.Add(panel1);
            Controls.Add(buttonPickupAccount);
            Controls.Add(pictureBoxJob);
            Controls.Add(buttonImportAll);
            Controls.Add(buttonExport);
            Controls.Add(labelenteramsacct);
            Controls.Add(buttonDeleteAccountEntry);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AmsLogin";
            toolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
            Load += AMSLogin_Load;
            Shown += AMSLogin_ShownAsync;
            DpiChanged += AmsLogin_DpiChanged;
            groupBoxAADAutMode.ResumeLayout(false);
            groupBoxAADAutMode.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxJob).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
            PerformLayout();
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
        private System.Windows.Forms.PictureBox pictureBoxJob;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.TextBox textBoxResourceGroup;
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
        private System.Windows.Forms.TextBox textBoxSubscription;
        private System.Windows.Forms.Label label2;
    }
}