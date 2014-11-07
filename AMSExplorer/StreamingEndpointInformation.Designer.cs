namespace AMSExplorer
{
    partial class StreamingEndpointInformation
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
            this.DGOrigin = new System.Windows.Forms.DataGridView();
            this.contextMenuStripOI = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonApplyClose = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.hostnamelink = new System.Windows.Forms.LinkLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonDelHostName = new System.Windows.Forms.Button();
            this.buttonAddHostName = new System.Windows.Forms.Button();
            this.dataGridViewCustomHostname = new System.Windows.Forms.DataGridView();
            this.checkBoxAkamai = new System.Windows.Forms.CheckBox();
            this.buttonDelAkamai = new System.Windows.Forms.Button();
            this.buttonAddAkamai = new System.Windows.Forms.Button();
            this.dataGridViewAkamai = new System.Windows.Forms.DataGridView();
            this.checkBoxStreamingIPlistSet = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textboxorigindesc = new System.Windows.Forms.TextBox();
            this.buttonDelIP = new System.Windows.Forms.Button();
            this.buttonAddIP = new System.Windows.Forms.Button();
            this.dataGridViewIP = new System.Windows.Forms.DataGridView();
            this.textBoxMaxCacheAge = new System.Windows.Forms.TextBox();
            this.lblMaxCacheAge = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownRU = new System.Windows.Forms.NumericUpDown();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.buttonAddExampleCrossDomainPolicy = new System.Windows.Forms.Button();
            this.buttonAddExampleClientPolicy = new System.Windows.Forms.Button();
            this.checkBoxcrossdomain = new System.Windows.Forms.CheckBox();
            this.textBoxCrossDomPolicy = new System.Windows.Forms.TextBox();
            this.checkBoxclientpolicy = new System.Windows.Forms.CheckBox();
            this.textBoxClientPolicy = new System.Windows.Forms.TextBox();
            this.labelOriginName = new System.Windows.Forms.Label();
            this.buttonDisregard = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.DGOrigin)).BeginInit();
            this.contextMenuStripOI.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCustomHostname)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAkamai)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewIP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRU)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // DGOrigin
            // 
            this.DGOrigin.AllowUserToAddRows = false;
            this.DGOrigin.AllowUserToDeleteRows = false;
            this.DGOrigin.AllowUserToResizeRows = false;
            this.DGOrigin.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DGOrigin.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DGOrigin.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGOrigin.ColumnHeadersVisible = false;
            this.DGOrigin.ContextMenuStrip = this.contextMenuStripOI;
            this.DGOrigin.Location = new System.Drawing.Point(9, 6);
            this.DGOrigin.MultiSelect = false;
            this.DGOrigin.Name = "DGOrigin";
            this.DGOrigin.ReadOnly = true;
            this.DGOrigin.RowHeadersVisible = false;
            this.DGOrigin.Size = new System.Drawing.Size(737, 429);
            this.DGOrigin.TabIndex = 0;
            // 
            // contextMenuStripOI
            // 
            this.contextMenuStripOI.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToClipboardToolStripMenuItem});
            this.contextMenuStripOI.Name = "contextMenuStripOI";
            this.contextMenuStripOI.Size = new System.Drawing.Size(170, 26);
            this.contextMenuStripOI.MouseClick += new System.Windows.Forms.MouseEventHandler(this.contextMenuStripOI_MouseClick);
            // 
            // copyToClipboardToolStripMenuItem
            // 
            this.copyToClipboardToolStripMenuItem.Name = "copyToClipboardToolStripMenuItem";
            this.copyToClipboardToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.copyToClipboardToolStripMenuItem.Text = "Copy to clipboard";
            // 
            // buttonApplyClose
            // 
            this.buttonApplyClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonApplyClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonApplyClose.Location = new System.Drawing.Point(501, 526);
            this.buttonApplyClose.Name = "buttonApplyClose";
            this.buttonApplyClose.Size = new System.Drawing.Size(159, 23);
            this.buttonApplyClose.TabIndex = 3;
            this.buttonApplyClose.Text = "Update settings and close";
            this.buttonApplyClose.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 43);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(760, 467);
            this.tabControl1.TabIndex = 34;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.DGOrigin);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(752, 441);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Information";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.hostnamelink);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.buttonDelHostName);
            this.tabPage2.Controls.Add(this.buttonAddHostName);
            this.tabPage2.Controls.Add(this.dataGridViewCustomHostname);
            this.tabPage2.Controls.Add(this.checkBoxAkamai);
            this.tabPage2.Controls.Add(this.buttonDelAkamai);
            this.tabPage2.Controls.Add(this.buttonAddAkamai);
            this.tabPage2.Controls.Add(this.dataGridViewAkamai);
            this.tabPage2.Controls.Add(this.checkBoxStreamingIPlistSet);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.textboxorigindesc);
            this.tabPage2.Controls.Add(this.buttonDelIP);
            this.tabPage2.Controls.Add(this.buttonAddIP);
            this.tabPage2.Controls.Add(this.dataGridViewIP);
            this.tabPage2.Controls.Add(this.textBoxMaxCacheAge);
            this.tabPage2.Controls.Add(this.lblMaxCacheAge);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.numericUpDownRU);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(752, 441);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Settings";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // hostnamelink
            // 
            this.hostnamelink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.hostnamelink.AutoSize = true;
            this.hostnamelink.Location = new System.Drawing.Point(176, 417);
            this.hostnamelink.Name = "hostnamelink";
            this.hostnamelink.Size = new System.Drawing.Size(170, 13);
            this.hostnamelink.TabIndex = 60;
            this.hostnamelink.TabStop = true;
            this.hostnamelink.Text = "How custom hostname are verified";
            this.hostnamelink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.hostnamelink_LinkClicked);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 313);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 13);
            this.label3.TabIndex = 59;
            this.label3.Text = "Custom hostnames :";
            // 
            // buttonDelHostName
            // 
            this.buttonDelHostName.Location = new System.Drawing.Point(95, 412);
            this.buttonDelHostName.Name = "buttonDelHostName";
            this.buttonDelHostName.Size = new System.Drawing.Size(75, 23);
            this.buttonDelHostName.TabIndex = 58;
            this.buttonDelHostName.Text = "Delete";
            this.buttonDelHostName.UseVisualStyleBackColor = true;
            this.buttonDelHostName.Click += new System.EventHandler(this.buttonDelHostName_Click);
            // 
            // buttonAddHostName
            // 
            this.buttonAddHostName.Location = new System.Drawing.Point(14, 412);
            this.buttonAddHostName.Name = "buttonAddHostName";
            this.buttonAddHostName.Size = new System.Drawing.Size(75, 23);
            this.buttonAddHostName.TabIndex = 57;
            this.buttonAddHostName.Text = "Add";
            this.buttonAddHostName.UseVisualStyleBackColor = true;
            this.buttonAddHostName.Click += new System.EventHandler(this.buttonAddHostName_Click);
            // 
            // dataGridViewCustomHostname
            // 
            this.dataGridViewCustomHostname.AllowUserToAddRows = false;
            this.dataGridViewCustomHostname.AllowUserToDeleteRows = false;
            this.dataGridViewCustomHostname.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewCustomHostname.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCustomHostname.ColumnHeadersVisible = false;
            this.dataGridViewCustomHostname.Location = new System.Drawing.Point(14, 329);
            this.dataGridViewCustomHostname.Name = "dataGridViewCustomHostname";
            this.dataGridViewCustomHostname.RowHeadersVisible = false;
            this.dataGridViewCustomHostname.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewCustomHostname.Size = new System.Drawing.Size(336, 77);
            this.dataGridViewCustomHostname.TabIndex = 55;
            this.toolTip1.SetToolTip(this.dataGridViewCustomHostname, "Rule name / IP address, CIDR or subnet mask");
            // 
            // checkBoxAkamai
            // 
            this.checkBoxAkamai.AutoSize = true;
            this.checkBoxAkamai.Location = new System.Drawing.Point(377, 128);
            this.checkBoxAkamai.Name = "checkBoxAkamai";
            this.checkBoxAkamai.Size = new System.Drawing.Size(251, 17);
            this.checkBoxAkamai.TabIndex = 54;
            this.checkBoxAkamai.Text = "Define Akamai Signature Header authentication";
            this.checkBoxAkamai.UseVisualStyleBackColor = true;
            this.checkBoxAkamai.CheckedChanged += new System.EventHandler(this.checkBoxAkamai_CheckedChanged);
            // 
            // buttonDelAkamai
            // 
            this.buttonDelAkamai.Enabled = false;
            this.buttonDelAkamai.Location = new System.Drawing.Point(458, 263);
            this.buttonDelAkamai.Name = "buttonDelAkamai";
            this.buttonDelAkamai.Size = new System.Drawing.Size(75, 23);
            this.buttonDelAkamai.TabIndex = 53;
            this.buttonDelAkamai.Text = "Delete";
            this.buttonDelAkamai.UseVisualStyleBackColor = true;
            this.buttonDelAkamai.Click += new System.EventHandler(this.buttonDelAkamai_Click);
            // 
            // buttonAddAkamai
            // 
            this.buttonAddAkamai.Enabled = false;
            this.buttonAddAkamai.Location = new System.Drawing.Point(377, 263);
            this.buttonAddAkamai.Name = "buttonAddAkamai";
            this.buttonAddAkamai.Size = new System.Drawing.Size(75, 23);
            this.buttonAddAkamai.TabIndex = 52;
            this.buttonAddAkamai.Text = "Add";
            this.buttonAddAkamai.UseVisualStyleBackColor = true;
            this.buttonAddAkamai.Click += new System.EventHandler(this.buttonAddAkamai_Click);
            // 
            // dataGridViewAkamai
            // 
            this.dataGridViewAkamai.AllowUserToAddRows = false;
            this.dataGridViewAkamai.AllowUserToDeleteRows = false;
            this.dataGridViewAkamai.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewAkamai.Enabled = false;
            this.dataGridViewAkamai.Location = new System.Drawing.Point(378, 151);
            this.dataGridViewAkamai.Name = "dataGridViewAkamai";
            this.dataGridViewAkamai.RowHeadersVisible = false;
            this.dataGridViewAkamai.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewAkamai.Size = new System.Drawing.Size(355, 106);
            this.dataGridViewAkamai.TabIndex = 51;
            this.toolTip1.SetToolTip(this.dataGridViewAkamai, "Rule name / IP address, CIDR or subnet mask");
            // 
            // checkBoxStreamingIPlistSet
            // 
            this.checkBoxStreamingIPlistSet.AutoSize = true;
            this.checkBoxStreamingIPlistSet.Location = new System.Drawing.Point(14, 128);
            this.checkBoxStreamingIPlistSet.Name = "checkBoxStreamingIPlistSet";
            this.checkBoxStreamingIPlistSet.Size = new System.Drawing.Size(214, 17);
            this.checkBoxStreamingIPlistSet.TabIndex = 48;
            this.checkBoxStreamingIPlistSet.Text = "Define streaming allowed IP addresses :";
            this.checkBoxStreamingIPlistSet.UseVisualStyleBackColor = true;
            this.checkBoxStreamingIPlistSet.CheckedChanged += new System.EventHandler(this.checkBoxStreamingIPlistSet_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 50;
            this.label2.Text = "Description :";
            // 
            // textboxorigindesc
            // 
            this.textboxorigindesc.Location = new System.Drawing.Point(14, 31);
            this.textboxorigindesc.Name = "textboxorigindesc";
            this.textboxorigindesc.Size = new System.Drawing.Size(449, 20);
            this.textboxorigindesc.TabIndex = 49;
            // 
            // buttonDelIP
            // 
            this.buttonDelIP.Enabled = false;
            this.buttonDelIP.Location = new System.Drawing.Point(95, 263);
            this.buttonDelIP.Name = "buttonDelIP";
            this.buttonDelIP.Size = new System.Drawing.Size(75, 23);
            this.buttonDelIP.TabIndex = 14;
            this.buttonDelIP.Text = "Delete";
            this.buttonDelIP.UseVisualStyleBackColor = true;
            this.buttonDelIP.Click += new System.EventHandler(this.buttonDelIP_Click);
            // 
            // buttonAddIP
            // 
            this.buttonAddIP.Enabled = false;
            this.buttonAddIP.Location = new System.Drawing.Point(15, 263);
            this.buttonAddIP.Name = "buttonAddIP";
            this.buttonAddIP.Size = new System.Drawing.Size(75, 23);
            this.buttonAddIP.TabIndex = 13;
            this.buttonAddIP.Text = "Add";
            this.buttonAddIP.UseVisualStyleBackColor = true;
            this.buttonAddIP.Click += new System.EventHandler(this.buttonAddIP_Click);
            // 
            // dataGridViewIP
            // 
            this.dataGridViewIP.AllowUserToAddRows = false;
            this.dataGridViewIP.AllowUserToDeleteRows = false;
            this.dataGridViewIP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewIP.Enabled = false;
            this.dataGridViewIP.Location = new System.Drawing.Point(14, 151);
            this.dataGridViewIP.Name = "dataGridViewIP";
            this.dataGridViewIP.RowHeadersVisible = false;
            this.dataGridViewIP.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewIP.Size = new System.Drawing.Size(336, 106);
            this.dataGridViewIP.TabIndex = 11;
            this.toolTip1.SetToolTip(this.dataGridViewIP, "Rule name / IP address, CIDR or subnet mask");
            // 
            // textBoxMaxCacheAge
            // 
            this.textBoxMaxCacheAge.Location = new System.Drawing.Point(153, 86);
            this.textBoxMaxCacheAge.Name = "textBoxMaxCacheAge";
            this.textBoxMaxCacheAge.Size = new System.Drawing.Size(100, 20);
            this.textBoxMaxCacheAge.TabIndex = 9;
            // 
            // lblMaxCacheAge
            // 
            this.lblMaxCacheAge.AutoSize = true;
            this.lblMaxCacheAge.Location = new System.Drawing.Point(150, 71);
            this.lblMaxCacheAge.Name = "lblMaxCacheAge";
            this.lblMaxCacheAge.Size = new System.Drawing.Size(97, 13);
            this.lblMaxCacheAge.TabIndex = 8;
            this.lblMaxCacheAge.Text = "MaxCacheAge (s) :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Scale units : ";
            // 
            // numericUpDownRU
            // 
            this.numericUpDownRU.Location = new System.Drawing.Point(15, 87);
            this.numericUpDownRU.Name = "numericUpDownRU";
            this.numericUpDownRU.Size = new System.Drawing.Size(65, 20);
            this.numericUpDownRU.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.buttonAddExampleCrossDomainPolicy);
            this.tabPage3.Controls.Add(this.buttonAddExampleClientPolicy);
            this.tabPage3.Controls.Add(this.checkBoxcrossdomain);
            this.tabPage3.Controls.Add(this.textBoxCrossDomPolicy);
            this.tabPage3.Controls.Add(this.checkBoxclientpolicy);
            this.tabPage3.Controls.Add(this.textBoxClientPolicy);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(752, 441);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Policies";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // buttonAddExampleCrossDomainPolicy
            // 
            this.buttonAddExampleCrossDomainPolicy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddExampleCrossDomainPolicy.Enabled = false;
            this.buttonAddExampleCrossDomainPolicy.Location = new System.Drawing.Point(650, 229);
            this.buttonAddExampleCrossDomainPolicy.Name = "buttonAddExampleCrossDomainPolicy";
            this.buttonAddExampleCrossDomainPolicy.Size = new System.Drawing.Size(96, 23);
            this.buttonAddExampleCrossDomainPolicy.TabIndex = 64;
            this.buttonAddExampleCrossDomainPolicy.Text = "Add example";
            this.buttonAddExampleCrossDomainPolicy.UseVisualStyleBackColor = true;
            this.buttonAddExampleCrossDomainPolicy.Click += new System.EventHandler(this.buttonAddExampleCrossDomainPolicy_Click);
            // 
            // buttonAddExampleClientPolicy
            // 
            this.buttonAddExampleClientPolicy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddExampleClientPolicy.Enabled = false;
            this.buttonAddExampleClientPolicy.Location = new System.Drawing.Point(650, 3);
            this.buttonAddExampleClientPolicy.Name = "buttonAddExampleClientPolicy";
            this.buttonAddExampleClientPolicy.Size = new System.Drawing.Size(96, 23);
            this.buttonAddExampleClientPolicy.TabIndex = 63;
            this.buttonAddExampleClientPolicy.Text = "Add example";
            this.buttonAddExampleClientPolicy.UseVisualStyleBackColor = true;
            this.buttonAddExampleClientPolicy.Click += new System.EventHandler(this.buttonAddExampleClientPolicy_Click);
            // 
            // checkBoxcrossdomain
            // 
            this.checkBoxcrossdomain.AutoSize = true;
            this.checkBoxcrossdomain.Location = new System.Drawing.Point(6, 233);
            this.checkBoxcrossdomain.Name = "checkBoxcrossdomain";
            this.checkBoxcrossdomain.Size = new System.Drawing.Size(299, 17);
            this.checkBoxcrossdomain.TabIndex = 62;
            this.checkBoxcrossdomain.Text = "Define cross domain access policy for Adobe Flash clients";
            this.checkBoxcrossdomain.UseVisualStyleBackColor = true;
            this.checkBoxcrossdomain.CheckedChanged += new System.EventHandler(this.checkBoxcrossdomains_CheckedChanged_1);
            // 
            // textBoxCrossDomPolicy
            // 
            this.textBoxCrossDomPolicy.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxCrossDomPolicy.Enabled = false;
            this.textBoxCrossDomPolicy.Font = new System.Drawing.Font("Consolas", 8.25F);
            this.textBoxCrossDomPolicy.Location = new System.Drawing.Point(6, 256);
            this.textBoxCrossDomPolicy.Multiline = true;
            this.textBoxCrossDomPolicy.Name = "textBoxCrossDomPolicy";
            this.textBoxCrossDomPolicy.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxCrossDomPolicy.Size = new System.Drawing.Size(740, 182);
            this.textBoxCrossDomPolicy.TabIndex = 61;
            // 
            // checkBoxclientpolicy
            // 
            this.checkBoxclientpolicy.AutoSize = true;
            this.checkBoxclientpolicy.Location = new System.Drawing.Point(6, 6);
            this.checkBoxclientpolicy.Name = "checkBoxclientpolicy";
            this.checkBoxclientpolicy.Size = new System.Drawing.Size(294, 17);
            this.checkBoxclientpolicy.TabIndex = 60;
            this.checkBoxclientpolicy.Text = "Define client access policy for Microsoft Silverlight clients";
            this.checkBoxclientpolicy.UseVisualStyleBackColor = true;
            this.checkBoxclientpolicy.CheckedChanged += new System.EventHandler(this.checkBoxclientpolicy_CheckedChanged_1);
            // 
            // textBoxClientPolicy
            // 
            this.textBoxClientPolicy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxClientPolicy.Enabled = false;
            this.textBoxClientPolicy.Font = new System.Drawing.Font("Consolas", 8.25F);
            this.textBoxClientPolicy.Location = new System.Drawing.Point(6, 29);
            this.textBoxClientPolicy.Multiline = true;
            this.textBoxClientPolicy.Name = "textBoxClientPolicy";
            this.textBoxClientPolicy.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxClientPolicy.Size = new System.Drawing.Size(740, 184);
            this.textBoxClientPolicy.TabIndex = 59;
            // 
            // labelOriginName
            // 
            this.labelOriginName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelOriginName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelOriginName.Location = new System.Drawing.Point(18, 9);
            this.labelOriginName.Name = "labelOriginName";
            this.labelOriginName.Size = new System.Drawing.Size(744, 20);
            this.labelOriginName.TabIndex = 37;
            this.labelOriginName.Text = "Streaming endpoint : ";
            // 
            // buttonDisregard
            // 
            this.buttonDisregard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDisregard.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonDisregard.Location = new System.Drawing.Point(666, 526);
            this.buttonDisregard.Name = "buttonDisregard";
            this.buttonDisregard.Size = new System.Drawing.Size(106, 23);
            this.buttonDisregard.TabIndex = 39;
            this.buttonDisregard.Text = "Close";
            this.buttonDisregard.UseVisualStyleBackColor = true;
            // 
            // StreamingEndpointInformation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.buttonDisregard);
            this.Controls.Add(this.labelOriginName);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.buttonApplyClose);
            this.Name = "StreamingEndpointInformation";
            this.Text = "Streaming endpoint information";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ChanneltInformation_FormClosed);
            this.Load += new System.EventHandler(this.OriginInformation_Load);
            this.Shown += new System.EventHandler(this.OriginInformation_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.DGOrigin)).EndInit();
            this.contextMenuStripOI.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCustomHostname)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAkamai)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewIP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRU)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView DGOrigin;
        private System.Windows.Forms.Button buttonApplyClose;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripLocators;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCopy;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripDG;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFilesCopyClipboard;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemPlaybackFlash;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemPlaybackSilverlight;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDASHIF;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemPlaybackMP4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemOpen;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripFiles;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemOpenFile;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDownloadFile;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogDownload;
        private System.Windows.Forms.ToolStripMenuItem makeItPrimaryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteFileToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDASHAZURE;
        private System.Windows.Forms.ToolStripMenuItem duplicateFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteLocatorToolStripMenuItem;
        private System.Windows.Forms.Label labelOriginName;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripOI;
        private System.Windows.Forms.ToolStripMenuItem copyToClipboardToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDownRU;
        private System.Windows.Forms.Button buttonDisregard;
        private System.Windows.Forms.Label lblMaxCacheAge;
        private System.Windows.Forms.TextBox textBoxMaxCacheAge;
        private System.Windows.Forms.DataGridView dataGridViewIP;
        private System.Windows.Forms.Button buttonDelIP;
        private System.Windows.Forms.Button buttonAddIP;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox checkBoxStreamingIPlistSet;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textboxorigindesc;
        private System.Windows.Forms.CheckBox checkBoxAkamai;
        private System.Windows.Forms.Button buttonDelAkamai;
        private System.Windows.Forms.Button buttonAddAkamai;
        private System.Windows.Forms.DataGridView dataGridViewAkamai;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.CheckBox checkBoxcrossdomain;
        private System.Windows.Forms.TextBox textBoxCrossDomPolicy;
        private System.Windows.Forms.CheckBox checkBoxclientpolicy;
        private System.Windows.Forms.TextBox textBoxClientPolicy;
        private System.Windows.Forms.DataGridView dataGridViewCustomHostname;
        private System.Windows.Forms.Button buttonDelHostName;
        private System.Windows.Forms.Button buttonAddHostName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel hostnamelink;
        private System.Windows.Forms.Button buttonAddExampleCrossDomainPolicy;
        private System.Windows.Forms.Button buttonAddExampleClientPolicy;
    }
}