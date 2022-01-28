namespace AMSExplorer
{
    partial class CreateAccount
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateAccount));
            this.buttonCreate = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.comboBoxAzureLocations = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxAccountName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxStorageId = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.linkLabelManagedIdentities = new System.Windows.Forms.LinkLabel();
            this.linkLabelCustomerManagedKeys = new System.Windows.Forms.LinkLabel();
            this.checkBoxManagedIdentity = new System.Windows.Forms.CheckBox();
            this.labelOkAMSAccount = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxRG = new System.Windows.Forms.TextBox();
            this.progressBarCreation = new System.Windows.Forms.ProgressBar();
            this.checkBoxCreateRG = new System.Windows.Forms.CheckBox();
            this.checkBoxCreateStorage = new System.Windows.Forms.CheckBox();
            this.textBoxNewStorageName = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxStorageType = new System.Windows.Forms.ComboBox();
            this.labelOkStorageAccount = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.linkLabelAvailZone = new System.Windows.Forms.LinkLabel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonCreate
            // 
            resources.ApplyResources(this.buttonCreate, "buttonCreate");
            this.errorProvider1.SetError(this.buttonCreate, resources.GetString("buttonCreate.Error"));
            this.errorProvider1.SetIconAlignment(this.buttonCreate, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("buttonCreate.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.buttonCreate, ((int)(resources.GetObject("buttonCreate.IconPadding"))));
            this.buttonCreate.Name = "buttonCreate";
            this.toolTip1.SetToolTip(this.buttonCreate, resources.GetString("buttonCreate.ToolTip"));
            this.buttonCreate.UseVisualStyleBackColor = true;
            this.buttonCreate.Click += new System.EventHandler(this.buttonNext_Click);
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
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonCreate);
            this.errorProvider1.SetError(this.panel1, resources.GetString("panel1.Error"));
            this.errorProvider1.SetIconAlignment(this.panel1, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("panel1.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.panel1, ((int)(resources.GetObject("panel1.IconPadding"))));
            this.panel1.Name = "panel1";
            this.toolTip1.SetToolTip(this.panel1, resources.GetString("panel1.ToolTip"));
            // 
            // comboBoxAzureLocations
            // 
            resources.ApplyResources(this.comboBoxAzureLocations, "comboBoxAzureLocations");
            this.comboBoxAzureLocations.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.errorProvider1.SetError(this.comboBoxAzureLocations, resources.GetString("comboBoxAzureLocations.Error"));
            this.comboBoxAzureLocations.FormattingEnabled = true;
            this.errorProvider1.SetIconAlignment(this.comboBoxAzureLocations, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("comboBoxAzureLocations.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.comboBoxAzureLocations, ((int)(resources.GetObject("comboBoxAzureLocations.IconPadding"))));
            this.comboBoxAzureLocations.Name = "comboBoxAzureLocations";
            this.toolTip1.SetToolTip(this.comboBoxAzureLocations, resources.GetString("comboBoxAzureLocations.ToolTip"));
            this.comboBoxAzureLocations.SelectedIndexChanged += new System.EventHandler(this.comboBoxAzureLocations_SelectedIndexChanged);
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
            // textBoxAccountName
            // 
            resources.ApplyResources(this.textBoxAccountName, "textBoxAccountName");
            this.errorProvider1.SetError(this.textBoxAccountName, resources.GetString("textBoxAccountName.Error"));
            this.errorProvider1.SetIconAlignment(this.textBoxAccountName, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("textBoxAccountName.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.textBoxAccountName, ((int)(resources.GetObject("textBoxAccountName.IconPadding"))));
            this.textBoxAccountName.Name = "textBoxAccountName";
            this.toolTip1.SetToolTip(this.textBoxAccountName, resources.GetString("textBoxAccountName.ToolTip"));
            this.textBoxAccountName.TextChanged += new System.EventHandler(this.textBoxAccountName_TextChanged);
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
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.errorProvider1.SetError(this.label3, resources.GetString("label3.Error"));
            this.errorProvider1.SetIconAlignment(this.label3, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label3.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label3, ((int)(resources.GetObject("label3.IconPadding"))));
            this.label3.Name = "label3";
            this.toolTip1.SetToolTip(this.label3, resources.GetString("label3.ToolTip"));
            // 
            // textBoxStorageId
            // 
            resources.ApplyResources(this.textBoxStorageId, "textBoxStorageId");
            this.errorProvider1.SetError(this.textBoxStorageId, resources.GetString("textBoxStorageId.Error"));
            this.errorProvider1.SetIconAlignment(this.textBoxStorageId, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("textBoxStorageId.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.textBoxStorageId, ((int)(resources.GetObject("textBoxStorageId.IconPadding"))));
            this.textBoxStorageId.Name = "textBoxStorageId";
            this.toolTip1.SetToolTip(this.textBoxStorageId, resources.GetString("textBoxStorageId.ToolTip"));
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.groupBox1.Controls.Add(this.linkLabelManagedIdentities);
            this.groupBox1.Controls.Add(this.linkLabelCustomerManagedKeys);
            this.groupBox1.Controls.Add(this.checkBoxManagedIdentity);
            this.groupBox1.Controls.Add(this.labelOkAMSAccount);
            this.groupBox1.Controls.Add(this.textBoxAccountName);
            this.groupBox1.Controls.Add(this.label2);
            this.errorProvider1.SetError(this.groupBox1, resources.GetString("groupBox1.Error"));
            this.errorProvider1.SetIconAlignment(this.groupBox1, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("groupBox1.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.groupBox1, ((int)(resources.GetObject("groupBox1.IconPadding"))));
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            this.toolTip1.SetToolTip(this.groupBox1, resources.GetString("groupBox1.ToolTip"));
            // 
            // linkLabelManagedIdentities
            // 
            resources.ApplyResources(this.linkLabelManagedIdentities, "linkLabelManagedIdentities");
            this.errorProvider1.SetError(this.linkLabelManagedIdentities, resources.GetString("linkLabelManagedIdentities.Error"));
            this.errorProvider1.SetIconAlignment(this.linkLabelManagedIdentities, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("linkLabelManagedIdentities.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.linkLabelManagedIdentities, ((int)(resources.GetObject("linkLabelManagedIdentities.IconPadding"))));
            this.linkLabelManagedIdentities.Name = "linkLabelManagedIdentities";
            this.linkLabelManagedIdentities.TabStop = true;
            this.toolTip1.SetToolTip(this.linkLabelManagedIdentities, resources.GetString("linkLabelManagedIdentities.ToolTip"));
            this.linkLabelManagedIdentities.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_LinkClicked);
            // 
            // linkLabelCustomerManagedKeys
            // 
            resources.ApplyResources(this.linkLabelCustomerManagedKeys, "linkLabelCustomerManagedKeys");
            this.errorProvider1.SetError(this.linkLabelCustomerManagedKeys, resources.GetString("linkLabelCustomerManagedKeys.Error"));
            this.errorProvider1.SetIconAlignment(this.linkLabelCustomerManagedKeys, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("linkLabelCustomerManagedKeys.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.linkLabelCustomerManagedKeys, ((int)(resources.GetObject("linkLabelCustomerManagedKeys.IconPadding"))));
            this.linkLabelCustomerManagedKeys.Name = "linkLabelCustomerManagedKeys";
            this.linkLabelCustomerManagedKeys.TabStop = true;
            this.toolTip1.SetToolTip(this.linkLabelCustomerManagedKeys, resources.GetString("linkLabelCustomerManagedKeys.ToolTip"));
            this.linkLabelCustomerManagedKeys.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_LinkClicked);
            // 
            // checkBoxManagedIdentity
            // 
            resources.ApplyResources(this.checkBoxManagedIdentity, "checkBoxManagedIdentity");
            this.errorProvider1.SetError(this.checkBoxManagedIdentity, resources.GetString("checkBoxManagedIdentity.Error"));
            this.errorProvider1.SetIconAlignment(this.checkBoxManagedIdentity, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("checkBoxManagedIdentity.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.checkBoxManagedIdentity, ((int)(resources.GetObject("checkBoxManagedIdentity.IconPadding"))));
            this.checkBoxManagedIdentity.Name = "checkBoxManagedIdentity";
            this.toolTip1.SetToolTip(this.checkBoxManagedIdentity, resources.GetString("checkBoxManagedIdentity.ToolTip"));
            this.checkBoxManagedIdentity.UseVisualStyleBackColor = true;
            // 
            // labelOkAMSAccount
            // 
            resources.ApplyResources(this.labelOkAMSAccount, "labelOkAMSAccount");
            this.errorProvider1.SetError(this.labelOkAMSAccount, resources.GetString("labelOkAMSAccount.Error"));
            this.errorProvider1.SetIconAlignment(this.labelOkAMSAccount, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("labelOkAMSAccount.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.labelOkAMSAccount, ((int)(resources.GetObject("labelOkAMSAccount.IconPadding"))));
            this.labelOkAMSAccount.Name = "labelOkAMSAccount";
            this.labelOkAMSAccount.Tag = "✓";
            this.toolTip1.SetToolTip(this.labelOkAMSAccount, resources.GetString("labelOkAMSAccount.ToolTip"));
            this.labelOkAMSAccount.UseCompatibleTextRendering = true;
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.errorProvider1.SetError(this.label4, resources.GetString("label4.Error"));
            this.errorProvider1.SetIconAlignment(this.label4, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label4.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label4, ((int)(resources.GetObject("label4.IconPadding"))));
            this.label4.Name = "label4";
            this.toolTip1.SetToolTip(this.label4, resources.GetString("label4.ToolTip"));
            // 
            // textBoxRG
            // 
            resources.ApplyResources(this.textBoxRG, "textBoxRG");
            this.errorProvider1.SetError(this.textBoxRG, resources.GetString("textBoxRG.Error"));
            this.errorProvider1.SetIconAlignment(this.textBoxRG, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("textBoxRG.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.textBoxRG, ((int)(resources.GetObject("textBoxRG.IconPadding"))));
            this.textBoxRG.Name = "textBoxRG";
            this.toolTip1.SetToolTip(this.textBoxRG, resources.GetString("textBoxRG.ToolTip"));
            // 
            // progressBarCreation
            // 
            resources.ApplyResources(this.progressBarCreation, "progressBarCreation");
            this.errorProvider1.SetError(this.progressBarCreation, resources.GetString("progressBarCreation.Error"));
            this.errorProvider1.SetIconAlignment(this.progressBarCreation, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("progressBarCreation.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.progressBarCreation, ((int)(resources.GetObject("progressBarCreation.IconPadding"))));
            this.progressBarCreation.MarqueeAnimationSpeed = 30;
            this.progressBarCreation.Name = "progressBarCreation";
            this.progressBarCreation.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.toolTip1.SetToolTip(this.progressBarCreation, resources.GetString("progressBarCreation.ToolTip"));
            // 
            // checkBoxCreateRG
            // 
            resources.ApplyResources(this.checkBoxCreateRG, "checkBoxCreateRG");
            this.checkBoxCreateRG.Checked = true;
            this.checkBoxCreateRG.CheckState = System.Windows.Forms.CheckState.Checked;
            this.errorProvider1.SetError(this.checkBoxCreateRG, resources.GetString("checkBoxCreateRG.Error"));
            this.errorProvider1.SetIconAlignment(this.checkBoxCreateRG, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("checkBoxCreateRG.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.checkBoxCreateRG, ((int)(resources.GetObject("checkBoxCreateRG.IconPadding"))));
            this.checkBoxCreateRG.Name = "checkBoxCreateRG";
            this.toolTip1.SetToolTip(this.checkBoxCreateRG, resources.GetString("checkBoxCreateRG.ToolTip"));
            this.checkBoxCreateRG.UseVisualStyleBackColor = true;
            // 
            // checkBoxCreateStorage
            // 
            resources.ApplyResources(this.checkBoxCreateStorage, "checkBoxCreateStorage");
            this.checkBoxCreateStorage.Checked = true;
            this.checkBoxCreateStorage.CheckState = System.Windows.Forms.CheckState.Checked;
            this.errorProvider1.SetError(this.checkBoxCreateStorage, resources.GetString("checkBoxCreateStorage.Error"));
            this.errorProvider1.SetIconAlignment(this.checkBoxCreateStorage, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("checkBoxCreateStorage.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.checkBoxCreateStorage, ((int)(resources.GetObject("checkBoxCreateStorage.IconPadding"))));
            this.checkBoxCreateStorage.Name = "checkBoxCreateStorage";
            this.toolTip1.SetToolTip(this.checkBoxCreateStorage, resources.GetString("checkBoxCreateStorage.ToolTip"));
            this.checkBoxCreateStorage.UseVisualStyleBackColor = true;
            this.checkBoxCreateStorage.CheckedChanged += new System.EventHandler(this.checkBoxCreateStorage_CheckedChanged);
            // 
            // textBoxNewStorageName
            // 
            resources.ApplyResources(this.textBoxNewStorageName, "textBoxNewStorageName");
            this.errorProvider1.SetError(this.textBoxNewStorageName, resources.GetString("textBoxNewStorageName.Error"));
            this.errorProvider1.SetIconAlignment(this.textBoxNewStorageName, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("textBoxNewStorageName.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.textBoxNewStorageName, ((int)(resources.GetObject("textBoxNewStorageName.IconPadding"))));
            this.textBoxNewStorageName.Name = "textBoxNewStorageName";
            this.toolTip1.SetToolTip(this.textBoxNewStorageName, resources.GetString("textBoxNewStorageName.ToolTip"));
            this.textBoxNewStorageName.TextChanged += new System.EventHandler(this.textBoxNewStorageName_TextChanged);
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.comboBoxStorageType);
            this.groupBox2.Controls.Add(this.labelOkStorageAccount);
            this.groupBox2.Controls.Add(this.checkBoxCreateStorage);
            this.groupBox2.Controls.Add(this.textBoxNewStorageName);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.textBoxStorageId);
            this.errorProvider1.SetError(this.groupBox2, resources.GetString("groupBox2.Error"));
            this.errorProvider1.SetIconAlignment(this.groupBox2, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("groupBox2.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.groupBox2, ((int)(resources.GetObject("groupBox2.IconPadding"))));
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            this.toolTip1.SetToolTip(this.groupBox2, resources.GetString("groupBox2.ToolTip"));
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.errorProvider1.SetError(this.label5, resources.GetString("label5.Error"));
            this.errorProvider1.SetIconAlignment(this.label5, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label5.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label5, ((int)(resources.GetObject("label5.IconPadding"))));
            this.label5.Name = "label5";
            this.toolTip1.SetToolTip(this.label5, resources.GetString("label5.ToolTip"));
            // 
            // comboBoxStorageType
            // 
            resources.ApplyResources(this.comboBoxStorageType, "comboBoxStorageType");
            this.comboBoxStorageType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.errorProvider1.SetError(this.comboBoxStorageType, resources.GetString("comboBoxStorageType.Error"));
            this.comboBoxStorageType.FormattingEnabled = true;
            this.errorProvider1.SetIconAlignment(this.comboBoxStorageType, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("comboBoxStorageType.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.comboBoxStorageType, ((int)(resources.GetObject("comboBoxStorageType.IconPadding"))));
            this.comboBoxStorageType.Name = "comboBoxStorageType";
            this.toolTip1.SetToolTip(this.comboBoxStorageType, resources.GetString("comboBoxStorageType.ToolTip"));
            // 
            // labelOkStorageAccount
            // 
            resources.ApplyResources(this.labelOkStorageAccount, "labelOkStorageAccount");
            this.errorProvider1.SetError(this.labelOkStorageAccount, resources.GetString("labelOkStorageAccount.Error"));
            this.errorProvider1.SetIconAlignment(this.labelOkStorageAccount, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("labelOkStorageAccount.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.labelOkStorageAccount, ((int)(resources.GetObject("labelOkStorageAccount.IconPadding"))));
            this.labelOkStorageAccount.Name = "labelOkStorageAccount";
            this.labelOkStorageAccount.Tag = "✓";
            this.toolTip1.SetToolTip(this.labelOkStorageAccount, resources.GetString("labelOkStorageAccount.ToolTip"));
            this.labelOkStorageAccount.UseCompatibleTextRendering = true;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            resources.ApplyResources(this.errorProvider1, "errorProvider1");
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.errorProvider1.SetError(this.label6, resources.GetString("label6.Error"));
            this.label6.ForeColor = System.Drawing.Color.DarkBlue;
            this.errorProvider1.SetIconAlignment(this.label6, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label6.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label6, ((int)(resources.GetObject("label6.IconPadding"))));
            this.label6.Name = "label6";
            this.toolTip1.SetToolTip(this.label6, resources.GetString("label6.ToolTip"));
            // 
            // linkLabelAvailZone
            // 
            resources.ApplyResources(this.linkLabelAvailZone, "linkLabelAvailZone");
            this.errorProvider1.SetError(this.linkLabelAvailZone, resources.GetString("linkLabelAvailZone.Error"));
            this.errorProvider1.SetIconAlignment(this.linkLabelAvailZone, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("linkLabelAvailZone.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.linkLabelAvailZone, ((int)(resources.GetObject("linkLabelAvailZone.IconPadding"))));
            this.linkLabelAvailZone.Name = "linkLabelAvailZone";
            this.linkLabelAvailZone.TabStop = true;
            this.toolTip1.SetToolTip(this.linkLabelAvailZone, resources.GetString("linkLabelAvailZone.ToolTip"));
            this.linkLabelAvailZone.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_LinkClicked);
            // 
            // CreateAccount
            // 
            this.AcceptButton = this.buttonCreate;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.linkLabelAvailZone);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.checkBoxCreateRG);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progressBarCreation);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxRG);
            this.Controls.Add(this.comboBoxAzureLocations);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Name = "CreateAccount";
            this.toolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.Load += new System.EventHandler(this.CreateAccount_Load);
            this.Shown += new System.EventHandler(this.CreateAccount_Shown);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button buttonCreate;
        public System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.ComboBox comboBoxAzureLocations;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxAccountName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxStorageId;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxRG;
        private System.Windows.Forms.ProgressBar progressBarCreation;
        private System.Windows.Forms.CheckBox checkBoxCreateRG;
        private System.Windows.Forms.CheckBox checkBoxCreateStorage;
        private System.Windows.Forms.TextBox textBoxNewStorageName;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label labelOkAMSAccount;
        private System.Windows.Forms.Label labelOkStorageAccount;
        private System.Windows.Forms.ComboBox comboBoxStorageType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.LinkLabel linkLabelAvailZone;
        private System.Windows.Forms.CheckBox checkBoxManagedIdentity;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.LinkLabel linkLabelManagedIdentities;
        private System.Windows.Forms.LinkLabel linkLabelCustomerManagedKeys;
    }
}