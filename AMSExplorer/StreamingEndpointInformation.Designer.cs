﻿namespace AMSExplorer
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
            this.buttonUpdateClose = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.labelcdn = new System.Windows.Forms.Label();
            this.panelAkamai = new System.Windows.Forms.Panel();
            this.dataGridViewAkamai = new System.Windows.Forms.DataGridView();
            this.buttonAddAkamai = new System.Windows.Forms.Button();
            this.buttonDelAkamai = new System.Windows.Forms.Button();
            this.checkBoxAkamai = new System.Windows.Forms.CheckBox();
            this.panelStreamingAllowedIP = new System.Windows.Forms.Panel();
            this.buttonAllowAllStreamingIP = new System.Windows.Forms.Button();
            this.dataGridViewIP = new System.Windows.Forms.DataGridView();
            this.buttonAddIP = new System.Windows.Forms.Button();
            this.buttonDelIP = new System.Windows.Forms.Button();
            this.checkBoxStreamingIPlistSet = new System.Windows.Forms.CheckBox();
            this.panelCustomHostnames = new System.Windows.Forms.Panel();
            this.dataGridViewCustomHostname = new System.Windows.Forms.DataGridView();
            this.buttonAddHostName = new System.Windows.Forms.Button();
            this.buttonDelHostName = new System.Windows.Forms.Button();
            this.hostnamelink = new System.Windows.Forms.LinkLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textboxorigindesc = new System.Windows.Forms.TextBox();
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
            this.labelSEName = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.DGOrigin)).BeginInit();
            this.contextMenuStripOI.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panelAkamai.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAkamai)).BeginInit();
            this.panelStreamingAllowedIP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewIP)).BeginInit();
            this.panelCustomHostnames.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCustomHostname)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRU)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
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
            this.DGOrigin.Location = new System.Drawing.Point(10, 7);
            this.DGOrigin.MultiSelect = false;
            this.DGOrigin.Name = "DGOrigin";
            this.DGOrigin.ReadOnly = true;
            this.DGOrigin.RowHeadersVisible = false;
            this.DGOrigin.Size = new System.Drawing.Size(860, 493);
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
            // buttonUpdateClose
            // 
            this.buttonUpdateClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonUpdateClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonUpdateClose.Location = new System.Drawing.Point(596, 15);
            this.buttonUpdateClose.Name = "buttonUpdateClose";
            this.buttonUpdateClose.Size = new System.Drawing.Size(185, 27);
            this.buttonUpdateClose.TabIndex = 3;
            this.buttonUpdateClose.Text = "Update settings and close";
            this.buttonUpdateClose.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(14, 50);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(887, 539);
            this.tabControl1.TabIndex = 34;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.DGOrigin);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(879, 511);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Information";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.labelcdn);
            this.tabPage2.Controls.Add(this.panelAkamai);
            this.tabPage2.Controls.Add(this.panelStreamingAllowedIP);
            this.tabPage2.Controls.Add(this.panelCustomHostnames);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.textboxorigindesc);
            this.tabPage2.Controls.Add(this.textBoxMaxCacheAge);
            this.tabPage2.Controls.Add(this.lblMaxCacheAge);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.numericUpDownRU);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(879, 511);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Settings";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.tabPage2.Click += new System.EventHandler(this.tabPage2_Click);
            // 
            // labelcdn
            // 
            this.labelcdn.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.labelcdn.Location = new System.Drawing.Point(437, 358);
            this.labelcdn.Name = "labelcdn";
            this.labelcdn.Size = new System.Drawing.Size(331, 74);
            this.labelcdn.TabIndex = 69;
            this.labelcdn.Text = "Azure CDN is enabled.\r\n\r\nAs a consequence, Allowed IP addresses, Akamai authentic" +
    "ation, custom host names and 0 unit are disabled.";
            // 
            // panelAkamai
            // 
            this.panelAkamai.Controls.Add(this.dataGridViewAkamai);
            this.panelAkamai.Controls.Add(this.buttonAddAkamai);
            this.panelAkamai.Controls.Add(this.buttonDelAkamai);
            this.panelAkamai.Controls.Add(this.checkBoxAkamai);
            this.panelAkamai.Location = new System.Drawing.Point(436, 130);
            this.panelAkamai.Name = "panelAkamai";
            this.panelAkamai.Size = new System.Drawing.Size(419, 200);
            this.panelAkamai.TabIndex = 65;
            // 
            // dataGridViewAkamai
            // 
            this.dataGridViewAkamai.AllowUserToAddRows = false;
            this.dataGridViewAkamai.AllowUserToDeleteRows = false;
            this.dataGridViewAkamai.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewAkamai.Enabled = false;
            this.dataGridViewAkamai.Location = new System.Drawing.Point(5, 44);
            this.dataGridViewAkamai.Name = "dataGridViewAkamai";
            this.dataGridViewAkamai.RowHeadersVisible = false;
            this.dataGridViewAkamai.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewAkamai.Size = new System.Drawing.Size(414, 122);
            this.dataGridViewAkamai.TabIndex = 51;
            this.toolTip1.SetToolTip(this.dataGridViewAkamai, "Rule name / IP address, CIDR or subnet mask");
            // 
            // buttonAddAkamai
            // 
            this.buttonAddAkamai.Enabled = false;
            this.buttonAddAkamai.Location = new System.Drawing.Point(3, 170);
            this.buttonAddAkamai.Name = "buttonAddAkamai";
            this.buttonAddAkamai.Size = new System.Drawing.Size(87, 27);
            this.buttonAddAkamai.TabIndex = 52;
            this.buttonAddAkamai.Text = "Add";
            this.buttonAddAkamai.UseVisualStyleBackColor = true;
            this.buttonAddAkamai.Click += new System.EventHandler(this.buttonAddAkamai_Click);
            // 
            // buttonDelAkamai
            // 
            this.buttonDelAkamai.Enabled = false;
            this.buttonDelAkamai.Location = new System.Drawing.Point(98, 170);
            this.buttonDelAkamai.Name = "buttonDelAkamai";
            this.buttonDelAkamai.Size = new System.Drawing.Size(87, 27);
            this.buttonDelAkamai.TabIndex = 53;
            this.buttonDelAkamai.Text = "Delete";
            this.buttonDelAkamai.UseVisualStyleBackColor = true;
            this.buttonDelAkamai.Click += new System.EventHandler(this.buttonDelAkamai_Click);
            // 
            // checkBoxAkamai
            // 
            this.checkBoxAkamai.AutoSize = true;
            this.checkBoxAkamai.Location = new System.Drawing.Point(5, 17);
            this.checkBoxAkamai.Name = "checkBoxAkamai";
            this.checkBoxAkamai.Size = new System.Drawing.Size(277, 19);
            this.checkBoxAkamai.TabIndex = 54;
            this.checkBoxAkamai.Text = "Define Akamai Signature Header authentication";
            this.checkBoxAkamai.UseVisualStyleBackColor = true;
            this.checkBoxAkamai.CheckedChanged += new System.EventHandler(this.checkBoxAkamai_CheckedChanged);
            // 
            // panelStreamingAllowedIP
            // 
            this.panelStreamingAllowedIP.Controls.Add(this.buttonAllowAllStreamingIP);
            this.panelStreamingAllowedIP.Controls.Add(this.dataGridViewIP);
            this.panelStreamingAllowedIP.Controls.Add(this.buttonAddIP);
            this.panelStreamingAllowedIP.Controls.Add(this.buttonDelIP);
            this.panelStreamingAllowedIP.Controls.Add(this.checkBoxStreamingIPlistSet);
            this.panelStreamingAllowedIP.Location = new System.Drawing.Point(7, 130);
            this.panelStreamingAllowedIP.Name = "panelStreamingAllowedIP";
            this.panelStreamingAllowedIP.Size = new System.Drawing.Size(422, 200);
            this.panelStreamingAllowedIP.TabIndex = 64;
            // 
            // buttonAllowAllStreamingIP
            // 
            this.buttonAllowAllStreamingIP.Location = new System.Drawing.Point(248, 170);
            this.buttonAllowAllStreamingIP.Name = "buttonAllowAllStreamingIP";
            this.buttonAllowAllStreamingIP.Size = new System.Drawing.Size(154, 27);
            this.buttonAllowAllStreamingIP.TabIndex = 70;
            this.buttonAllowAllStreamingIP.Text = "Allow all IP addresses";
            this.buttonAllowAllStreamingIP.UseVisualStyleBackColor = true;
            this.buttonAllowAllStreamingIP.Click += new System.EventHandler(this.buttonAllowAllStreamingIP_Click);
            // 
            // dataGridViewIP
            // 
            this.dataGridViewIP.AllowUserToAddRows = false;
            this.dataGridViewIP.AllowUserToDeleteRows = false;
            this.dataGridViewIP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewIP.Enabled = false;
            this.dataGridViewIP.Location = new System.Drawing.Point(10, 44);
            this.dataGridViewIP.Name = "dataGridViewIP";
            this.dataGridViewIP.RowHeadersVisible = false;
            this.dataGridViewIP.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewIP.Size = new System.Drawing.Size(392, 122);
            this.dataGridViewIP.TabIndex = 11;
            this.toolTip1.SetToolTip(this.dataGridViewIP, "Rule name / IP address, CIDR or subnet mask");
            // 
            // buttonAddIP
            // 
            this.buttonAddIP.Enabled = false;
            this.buttonAddIP.Location = new System.Drawing.Point(10, 170);
            this.buttonAddIP.Name = "buttonAddIP";
            this.buttonAddIP.Size = new System.Drawing.Size(87, 27);
            this.buttonAddIP.TabIndex = 13;
            this.buttonAddIP.Text = "Add";
            this.buttonAddIP.UseVisualStyleBackColor = true;
            this.buttonAddIP.Click += new System.EventHandler(this.buttonAddIP_Click);
            // 
            // buttonDelIP
            // 
            this.buttonDelIP.Enabled = false;
            this.buttonDelIP.Location = new System.Drawing.Point(105, 170);
            this.buttonDelIP.Name = "buttonDelIP";
            this.buttonDelIP.Size = new System.Drawing.Size(87, 27);
            this.buttonDelIP.TabIndex = 14;
            this.buttonDelIP.Text = "Delete";
            this.buttonDelIP.UseVisualStyleBackColor = true;
            this.buttonDelIP.Click += new System.EventHandler(this.buttonDelIP_Click);
            // 
            // checkBoxStreamingIPlistSet
            // 
            this.checkBoxStreamingIPlistSet.AutoSize = true;
            this.checkBoxStreamingIPlistSet.Location = new System.Drawing.Point(10, 17);
            this.checkBoxStreamingIPlistSet.Name = "checkBoxStreamingIPlistSet";
            this.checkBoxStreamingIPlistSet.Size = new System.Drawing.Size(233, 19);
            this.checkBoxStreamingIPlistSet.TabIndex = 48;
            this.checkBoxStreamingIPlistSet.Text = "Define streaming allowed IP addresses :";
            this.checkBoxStreamingIPlistSet.UseVisualStyleBackColor = true;
            this.checkBoxStreamingIPlistSet.CheckedChanged += new System.EventHandler(this.checkBoxStreamingIPlistSet_CheckedChanged);
            // 
            // panelCustomHostnames
            // 
            this.panelCustomHostnames.Controls.Add(this.dataGridViewCustomHostname);
            this.panelCustomHostnames.Controls.Add(this.buttonAddHostName);
            this.panelCustomHostnames.Controls.Add(this.buttonDelHostName);
            this.panelCustomHostnames.Controls.Add(this.hostnamelink);
            this.panelCustomHostnames.Controls.Add(this.label3);
            this.panelCustomHostnames.Location = new System.Drawing.Point(7, 337);
            this.panelCustomHostnames.Name = "panelCustomHostnames";
            this.panelCustomHostnames.Size = new System.Drawing.Size(422, 165);
            this.panelCustomHostnames.TabIndex = 63;
            // 
            // dataGridViewCustomHostname
            // 
            this.dataGridViewCustomHostname.AllowUserToAddRows = false;
            this.dataGridViewCustomHostname.AllowUserToDeleteRows = false;
            this.dataGridViewCustomHostname.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewCustomHostname.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCustomHostname.ColumnHeadersVisible = false;
            this.dataGridViewCustomHostname.Location = new System.Drawing.Point(9, 21);
            this.dataGridViewCustomHostname.Name = "dataGridViewCustomHostname";
            this.dataGridViewCustomHostname.RowHeadersVisible = false;
            this.dataGridViewCustomHostname.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewCustomHostname.Size = new System.Drawing.Size(392, 89);
            this.dataGridViewCustomHostname.TabIndex = 55;
            this.toolTip1.SetToolTip(this.dataGridViewCustomHostname, "Rule name / IP address, CIDR or subnet mask");
            // 
            // buttonAddHostName
            // 
            this.buttonAddHostName.Location = new System.Drawing.Point(10, 117);
            this.buttonAddHostName.Name = "buttonAddHostName";
            this.buttonAddHostName.Size = new System.Drawing.Size(87, 27);
            this.buttonAddHostName.TabIndex = 57;
            this.buttonAddHostName.Text = "Add";
            this.buttonAddHostName.UseVisualStyleBackColor = true;
            this.buttonAddHostName.Click += new System.EventHandler(this.buttonAddHostName_Click);
            // 
            // buttonDelHostName
            // 
            this.buttonDelHostName.Location = new System.Drawing.Point(104, 117);
            this.buttonDelHostName.Name = "buttonDelHostName";
            this.buttonDelHostName.Size = new System.Drawing.Size(87, 27);
            this.buttonDelHostName.TabIndex = 58;
            this.buttonDelHostName.Text = "Delete";
            this.buttonDelHostName.UseVisualStyleBackColor = true;
            this.buttonDelHostName.Click += new System.EventHandler(this.buttonDelHostName_Click);
            // 
            // hostnamelink
            // 
            this.hostnamelink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.hostnamelink.AutoSize = true;
            this.hostnamelink.Location = new System.Drawing.Point(203, 122);
            this.hostnamelink.Name = "hostnamelink";
            this.hostnamelink.Size = new System.Drawing.Size(192, 15);
            this.hostnamelink.TabIndex = 60;
            this.hostnamelink.TabStop = true;
            this.hostnamelink.Text = "How custom hostname are verified";
            this.hostnamelink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.hostnamelink_LinkClicked);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 15);
            this.label3.TabIndex = 59;
            this.label3.Text = "Custom host names :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 15);
            this.label2.TabIndex = 50;
            this.label2.Text = "Description :";
            // 
            // textboxorigindesc
            // 
            this.textboxorigindesc.Location = new System.Drawing.Point(16, 36);
            this.textboxorigindesc.Name = "textboxorigindesc";
            this.textboxorigindesc.Size = new System.Drawing.Size(523, 23);
            this.textboxorigindesc.TabIndex = 49;
            // 
            // textBoxMaxCacheAge
            // 
            this.textBoxMaxCacheAge.Location = new System.Drawing.Point(178, 99);
            this.textBoxMaxCacheAge.Name = "textBoxMaxCacheAge";
            this.textBoxMaxCacheAge.Size = new System.Drawing.Size(116, 23);
            this.textBoxMaxCacheAge.TabIndex = 9;
            this.textBoxMaxCacheAge.TextChanged += new System.EventHandler(this.textBoxMaxCacheAge_TextChanged);
            // 
            // lblMaxCacheAge
            // 
            this.lblMaxCacheAge.AutoSize = true;
            this.lblMaxCacheAge.Location = new System.Drawing.Point(175, 82);
            this.lblMaxCacheAge.Name = "lblMaxCacheAge";
            this.lblMaxCacheAge.Size = new System.Drawing.Size(111, 15);
            this.lblMaxCacheAge.TabIndex = 8;
            this.lblMaxCacheAge.Text = "Max Cache Age (s) :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Streaming Units : ";
            // 
            // numericUpDownRU
            // 
            this.numericUpDownRU.Location = new System.Drawing.Point(17, 100);
            this.numericUpDownRU.Name = "numericUpDownRU";
            this.numericUpDownRU.ReadOnly = true;
            this.numericUpDownRU.Size = new System.Drawing.Size(76, 23);
            this.numericUpDownRU.TabIndex = 0;
            this.toolTip1.SetToolTip(this.numericUpDownRU, "1 unit = 200 mbps");
            this.numericUpDownRU.ValueChanged += new System.EventHandler(this.numericUpDownRU_ValueChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.buttonAddExampleCrossDomainPolicy);
            this.tabPage3.Controls.Add(this.buttonAddExampleClientPolicy);
            this.tabPage3.Controls.Add(this.checkBoxcrossdomain);
            this.tabPage3.Controls.Add(this.textBoxCrossDomPolicy);
            this.tabPage3.Controls.Add(this.checkBoxclientpolicy);
            this.tabPage3.Controls.Add(this.textBoxClientPolicy);
            this.tabPage3.Location = new System.Drawing.Point(4, 24);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(879, 511);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Policies";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // buttonAddExampleCrossDomainPolicy
            // 
            this.buttonAddExampleCrossDomainPolicy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddExampleCrossDomainPolicy.Enabled = false;
            this.buttonAddExampleCrossDomainPolicy.Location = new System.Drawing.Point(758, 264);
            this.buttonAddExampleCrossDomainPolicy.Name = "buttonAddExampleCrossDomainPolicy";
            this.buttonAddExampleCrossDomainPolicy.Size = new System.Drawing.Size(112, 27);
            this.buttonAddExampleCrossDomainPolicy.TabIndex = 64;
            this.buttonAddExampleCrossDomainPolicy.Text = "Add example";
            this.buttonAddExampleCrossDomainPolicy.UseVisualStyleBackColor = true;
            this.buttonAddExampleCrossDomainPolicy.Click += new System.EventHandler(this.buttonAddExampleCrossDomainPolicy_Click);
            // 
            // buttonAddExampleClientPolicy
            // 
            this.buttonAddExampleClientPolicy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddExampleClientPolicy.Enabled = false;
            this.buttonAddExampleClientPolicy.Location = new System.Drawing.Point(758, 3);
            this.buttonAddExampleClientPolicy.Name = "buttonAddExampleClientPolicy";
            this.buttonAddExampleClientPolicy.Size = new System.Drawing.Size(112, 27);
            this.buttonAddExampleClientPolicy.TabIndex = 63;
            this.buttonAddExampleClientPolicy.Text = "Add example";
            this.buttonAddExampleClientPolicy.UseVisualStyleBackColor = true;
            this.buttonAddExampleClientPolicy.Click += new System.EventHandler(this.buttonAddExampleClientPolicy_Click);
            // 
            // checkBoxcrossdomain
            // 
            this.checkBoxcrossdomain.AutoSize = true;
            this.checkBoxcrossdomain.Location = new System.Drawing.Point(7, 269);
            this.checkBoxcrossdomain.Name = "checkBoxcrossdomain";
            this.checkBoxcrossdomain.Size = new System.Drawing.Size(329, 19);
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
            this.textBoxCrossDomPolicy.Location = new System.Drawing.Point(7, 295);
            this.textBoxCrossDomPolicy.Multiline = true;
            this.textBoxCrossDomPolicy.Name = "textBoxCrossDomPolicy";
            this.textBoxCrossDomPolicy.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxCrossDomPolicy.Size = new System.Drawing.Size(863, 207);
            this.textBoxCrossDomPolicy.TabIndex = 61;
            // 
            // checkBoxclientpolicy
            // 
            this.checkBoxclientpolicy.AutoSize = true;
            this.checkBoxclientpolicy.Location = new System.Drawing.Point(7, 7);
            this.checkBoxclientpolicy.Name = "checkBoxclientpolicy";
            this.checkBoxclientpolicy.Size = new System.Drawing.Size(328, 19);
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
            this.textBoxClientPolicy.Location = new System.Drawing.Point(7, 33);
            this.textBoxClientPolicy.Multiline = true;
            this.textBoxClientPolicy.Name = "textBoxClientPolicy";
            this.textBoxClientPolicy.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxClientPolicy.Size = new System.Drawing.Size(863, 212);
            this.textBoxClientPolicy.TabIndex = 59;
            // 
            // labelSEName
            // 
            this.labelSEName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelSEName.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSEName.ForeColor = System.Drawing.Color.DarkBlue;
            this.labelSEName.Location = new System.Drawing.Point(21, 13);
            this.labelSEName.Name = "labelSEName";
            this.labelSEName.Size = new System.Drawing.Size(868, 23);
            this.labelSEName.TabIndex = 37;
            this.labelSEName.Text = "Streaming endpoint : {0}";
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.Location = new System.Drawing.Point(789, 15);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(115, 27);
            this.buttonClose.TabIndex = 39;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonUpdateClose);
            this.panel1.Controls.Add(this.buttonClose);
            this.panel1.Location = new System.Drawing.Point(-3, 592);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(922, 55);
            this.panel1.TabIndex = 64;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // StreamingEndpointInformation
            // 
            this.AcceptButton = this.buttonClose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonClose;
            this.ClientSize = new System.Drawing.Size(915, 647);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labelSEName);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "StreamingEndpointInformation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Streaming endpoint information";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ChanneltInformation_FormClosed);
            this.Load += new System.EventHandler(this.StreamingEndpointInformation_Load);
            this.Shown += new System.EventHandler(this.OriginInformation_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.DGOrigin)).EndInit();
            this.contextMenuStripOI.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.panelAkamai.ResumeLayout(false);
            this.panelAkamai.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAkamai)).EndInit();
            this.panelStreamingAllowedIP.ResumeLayout(false);
            this.panelStreamingAllowedIP.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewIP)).EndInit();
            this.panelCustomHostnames.ResumeLayout(false);
            this.panelCustomHostnames.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCustomHostname)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRU)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView DGOrigin;
        private System.Windows.Forms.Button buttonUpdateClose;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label labelSEName;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripOI;
        private System.Windows.Forms.ToolStripMenuItem copyToClipboardToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDownRU;
        private System.Windows.Forms.Button buttonClose;
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
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelCustomHostnames;
        private System.Windows.Forms.Panel panelAkamai;
        private System.Windows.Forms.Panel panelStreamingAllowedIP;
        private System.Windows.Forms.Label labelcdn;
        private System.Windows.Forms.Button buttonAllowAllStreamingIP;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}