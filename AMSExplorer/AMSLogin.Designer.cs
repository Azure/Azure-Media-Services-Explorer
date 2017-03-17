﻿namespace AMSExplorer
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
            // listBoxAcounts
            // 
            resources.ApplyResources(this.listBoxAcounts, "listBoxAcounts");
            this.listBoxAcounts.FormattingEnabled = true;
            this.listBoxAcounts.Name = "listBoxAcounts";
            this.listBoxAcounts.SelectedIndexChanged += new System.EventHandler(this.listBoxAccounts_SelectedIndexChanged);
            this.listBoxAcounts.DoubleClick += new System.EventHandler(this.listBoxAcounts_DoubleClick);
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
            // tabControl1
            // 
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
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
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            // 
            // textBoxAccountID
            // 
            resources.ApplyResources(this.textBoxAccountID, "textBoxAccountID");
            this.textBoxAccountID.Name = "textBoxAccountID";
            this.textBoxAccountID.Validating += new System.ComponentModel.CancelEventHandler(this.CheckTextBoxGuid);
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
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
            // textBoxAccountKey
            // 
            resources.ApplyResources(this.textBoxAccountKey, "textBoxAccountKey");
            this.textBoxAccountKey.Name = "textBoxAccountKey";
            this.textBoxAccountKey.UseSystemPasswordChar = true;
            this.textBoxAccountKey.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxAccountKey_Validating);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // textBoxAccountName
            // 
            resources.ApplyResources(this.textBoxAccountName, "textBoxAccountName");
            this.textBoxAccountName.Name = "textBoxAccountName";
            this.textBoxAccountName.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxAccountName_Validating);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
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
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Name = "tabPage2";
            // 
            // textBoxManagementPortal
            // 
            this.textBoxManagementPortal.BackColor = System.Drawing.Color.Pink;
            resources.ApplyResources(this.textBoxManagementPortal, "textBoxManagementPortal");
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
            this.textBoxAzureEndpoint.BackColor = System.Drawing.Color.Pink;
            resources.ApplyResources(this.textBoxAzureEndpoint, "textBoxAzureEndpoint");
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
            this.textBoxACSBaseAddress.BackColor = System.Drawing.Color.Pink;
            resources.ApplyResources(this.textBoxACSBaseAddress, "textBoxACSBaseAddress");
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
            this.textBoxScope.BackColor = System.Drawing.Color.Pink;
            resources.ApplyResources(this.textBoxScope, "textBoxScope");
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
            this.textBoxAPIServer.BackColor = System.Drawing.Color.Pink;
            resources.ApplyResources(this.textBoxAPIServer, "textBoxAPIServer");
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
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
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
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.buttonDeleteAccountEntry);
            this.Controls.Add(this.buttonSaveToList);
            this.Controls.Add(this.listBoxAcounts);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AMSLogin";
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