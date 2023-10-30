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
            linkLabelAMSRetire = new System.Windows.Forms.LinkLabel();
            labelVersion = new System.Windows.Forms.Label();
            buttonPickupAccount = new System.Windows.Forms.Button();
            toolTip1 = new System.Windows.Forms.ToolTip(components);
            listViewAccounts = new System.Windows.Forms.ListView();
            linkLabelAMSOfflineDoc = new System.Windows.Forms.LinkLabel();
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            textBoxSubscription = new System.Windows.Forms.TextBox();
            label2 = new System.Windows.Forms.Label();
            errorProvider1 = new System.Windows.Forms.ErrorProvider(components);
            pictureBoxMKIO = new System.Windows.Forms.PictureBox();
            label3 = new System.Windows.Forms.Label();
            groupBoxAADAutMode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxJob).BeginInit();
            panel1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxMKIO).BeginInit();
            SuspendLayout();
            // 
            // buttonLogin
            // 
            resources.ApplyResources(buttonLogin, "buttonLogin");
            buttonLogin.Name = "buttonLogin";
            buttonLogin.UseVisualStyleBackColor = true;
            buttonLogin.Click += ButtonLogin_Click;
            // 
            // buttonCancel
            // 
            resources.ApplyResources(buttonCancel, "buttonCancel");
            buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            buttonCancel.Name = "buttonCancel";
            buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonDeleteAccountEntry
            // 
            resources.ApplyResources(buttonDeleteAccountEntry, "buttonDeleteAccountEntry");
            buttonDeleteAccountEntry.Name = "buttonDeleteAccountEntry";
            buttonDeleteAccountEntry.UseVisualStyleBackColor = true;
            buttonDeleteAccountEntry.Click += ButtonDeleteAccount_Click;
            // 
            // textBoxDescription
            // 
            resources.ApplyResources(textBoxDescription, "textBoxDescription");
            textBoxDescription.Name = "textBoxDescription";
            textBoxDescription.TextChanged += textBoxDescription_TextChanged;
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // groupBoxAADAutMode
            // 
            groupBoxAADAutMode.Controls.Add(radioButtonAADServicePrincipal);
            groupBoxAADAutMode.Controls.Add(radioButtonAADInteractive);
            resources.ApplyResources(groupBoxAADAutMode, "groupBoxAADAutMode");
            groupBoxAADAutMode.Name = "groupBoxAADAutMode";
            groupBoxAADAutMode.TabStop = false;
            // 
            // radioButtonAADServicePrincipal
            // 
            resources.ApplyResources(radioButtonAADServicePrincipal, "radioButtonAADServicePrincipal");
            radioButtonAADServicePrincipal.Name = "radioButtonAADServicePrincipal";
            radioButtonAADServicePrincipal.UseVisualStyleBackColor = true;
            // 
            // radioButtonAADInteractive
            // 
            resources.ApplyResources(radioButtonAADInteractive, "radioButtonAADInteractive");
            radioButtonAADInteractive.Checked = true;
            radioButtonAADInteractive.Name = "radioButtonAADInteractive";
            radioButtonAADInteractive.TabStop = true;
            radioButtonAADInteractive.UseVisualStyleBackColor = true;
            // 
            // textBoxResourceGroup
            // 
            resources.ApplyResources(textBoxResourceGroup, "textBoxResourceGroup");
            textBoxResourceGroup.Name = "textBoxResourceGroup";
            toolTip1.SetToolTip(textBoxResourceGroup, resources.GetString("textBoxResourceGroup.ToolTip"));
            // 
            // textBoxAADtenantId
            // 
            resources.ApplyResources(textBoxAADtenantId, "textBoxAADtenantId");
            textBoxAADtenantId.Name = "textBoxAADtenantId";
            // 
            // labelADTenant
            // 
            resources.ApplyResources(labelADTenant, "labelADTenant");
            labelADTenant.Name = "labelADTenant";
            // 
            // labelE2
            // 
            resources.ApplyResources(labelE2, "labelE2");
            labelE2.Name = "labelE2";
            // 
            // labelenteramsacct
            // 
            resources.ApplyResources(labelenteramsacct, "labelenteramsacct");
            labelenteramsacct.ForeColor = System.Drawing.Color.DarkBlue;
            labelenteramsacct.Name = "labelenteramsacct";
            // 
            // buttonExport
            // 
            resources.ApplyResources(buttonExport, "buttonExport");
            buttonExport.Name = "buttonExport";
            buttonExport.UseVisualStyleBackColor = true;
            buttonExport.Click += ButtonExport_Click;
            // 
            // buttonImportAll
            // 
            resources.ApplyResources(buttonImportAll, "buttonImportAll");
            buttonImportAll.Name = "buttonImportAll";
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
            pictureBoxJob.Name = "pictureBoxJob";
            pictureBoxJob.TabStop = false;
            // 
            // panel1
            // 
            panel1.BackColor = System.Drawing.SystemColors.Control;
            panel1.Controls.Add(linkLabelAMSRetire);
            panel1.Controls.Add(labelVersion);
            panel1.Controls.Add(buttonLogin);
            panel1.Controls.Add(buttonCancel);
            resources.ApplyResources(panel1, "panel1");
            panel1.Name = "panel1";
            panel1.Paint += Panel1_Paint;
            // 
            // linkLabelAMSRetire
            // 
            resources.ApplyResources(linkLabelAMSRetire, "linkLabelAMSRetire");
            linkLabelAMSRetire.LinkColor = System.Drawing.Color.Red;
            linkLabelAMSRetire.Name = "linkLabelAMSRetire";
            linkLabelAMSRetire.TabStop = true;
            linkLabelAMSRetire.LinkClicked += Accountmgtlink_LinkClicked;
            // 
            // labelVersion
            // 
            resources.ApplyResources(labelVersion, "labelVersion");
            labelVersion.ForeColor = System.Drawing.SystemColors.ControlText;
            labelVersion.Name = "labelVersion";
            // 
            // buttonPickupAccount
            // 
            resources.ApplyResources(buttonPickupAccount, "buttonPickupAccount");
            buttonPickupAccount.Name = "buttonPickupAccount";
            buttonPickupAccount.UseVisualStyleBackColor = true;
            buttonPickupAccount.Click += buttonPickupAccount_Click;
            // 
            // listViewAccounts
            // 
            resources.ApplyResources(listViewAccounts, "listViewAccounts");
            listViewAccounts.FullRowSelect = true;
            listViewAccounts.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            listViewAccounts.Name = "listViewAccounts";
            listViewAccounts.ShowItemToolTips = true;
            listViewAccounts.UseCompatibleStateImageBehavior = false;
            listViewAccounts.View = System.Windows.Forms.View.List;
            listViewAccounts.SelectedIndexChanged += ListViewAccounts_SelectedIndexChanged;
            listViewAccounts.DoubleClick += ListBoxAcounts_DoubleClick;
            listViewAccounts.DpiChangedAfterParent += AmsLogin_DpiChangedAfterParent;
            // 
            // linkLabelAMSOfflineDoc
            // 
            resources.ApplyResources(linkLabelAMSOfflineDoc, "linkLabelAMSOfflineDoc");
            linkLabelAMSOfflineDoc.Name = "linkLabelAMSOfflineDoc";
            linkLabelAMSOfflineDoc.TabStop = true;
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
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // textBoxSubscription
            // 
            resources.ApplyResources(textBoxSubscription, "textBoxSubscription");
            textBoxSubscription.Name = "textBoxSubscription";
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // pictureBoxMKIO
            // 
            resources.ApplyResources(pictureBoxMKIO, "pictureBoxMKIO");
            pictureBoxMKIO.Image = Bitmaps.mk_io_blue;
            pictureBoxMKIO.Name = "pictureBoxMKIO";
            pictureBoxMKIO.TabStop = false;
            // 
            // label3
            // 
            resources.ApplyResources(label3, "label3");
            label3.ForeColor = System.Drawing.SystemColors.ControlText;
            label3.Name = "label3";
            // 
            // AmsLogin
            // 
            AcceptButton = buttonLogin;
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            BackColor = System.Drawing.Color.WhiteSmoke;
            CancelButton = buttonCancel;
            Controls.Add(label3);
            Controls.Add(pictureBoxMKIO);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(linkLabelAMSOfflineDoc);
            Controls.Add(listViewAccounts);
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
            ((System.ComponentModel.ISupportInitialize)pictureBoxMKIO).EndInit();
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
        private System.Windows.Forms.ListView listViewAccounts;
        private System.Windows.Forms.Button buttonPickupAccount;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel linkLabelAMSOfflineDoc;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox textBoxSubscription;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel linkLabelAMSRetire;
        private System.Windows.Forms.PictureBox pictureBoxMKIO;
        private System.Windows.Forms.Label label3;
    }
}