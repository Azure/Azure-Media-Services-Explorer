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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StreamingEndpointInformation));
            this.DGOrigin = new System.Windows.Forms.DataGridView();
            this.contextMenuStripOI = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonUpdateClose = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageInfo = new System.Windows.Forms.TabPage();
            this.tabPageSettings = new System.Windows.Forms.TabPage();
            this.moreinfoSE = new System.Windows.Forms.LinkLabel();
            this.groupBoxTypeScale = new System.Windows.Forms.GroupBox();
            this.radioButtonPremium = new System.Windows.Forms.RadioButton();
            this.radioButtonStandard = new System.Windows.Forms.RadioButton();
            this.numericUpDownRU = new System.Windows.Forms.NumericUpDown();
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
            this.labeldesc = new System.Windows.Forms.Label();
            this.textboxorigindesc = new System.Windows.Forms.TextBox();
            this.textBoxMaxCacheAge = new System.Windows.Forms.TextBox();
            this.lblMaxCacheAge = new System.Windows.Forms.Label();
            this.tabPagePolicies = new System.Windows.Forms.TabPage();
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
            this.tabPageInfo.SuspendLayout();
            this.tabPageSettings.SuspendLayout();
            this.groupBoxTypeScale.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRU)).BeginInit();
            this.panelAkamai.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAkamai)).BeginInit();
            this.panelStreamingAllowedIP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewIP)).BeginInit();
            this.panelCustomHostnames.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCustomHostname)).BeginInit();
            this.tabPagePolicies.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // DGOrigin
            // 
            this.DGOrigin.AllowUserToAddRows = false;
            this.DGOrigin.AllowUserToDeleteRows = false;
            this.DGOrigin.AllowUserToResizeRows = false;
            resources.ApplyResources(this.DGOrigin, "DGOrigin");
            this.DGOrigin.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DGOrigin.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.DGOrigin.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGOrigin.ColumnHeadersVisible = false;
            this.DGOrigin.ContextMenuStrip = this.contextMenuStripOI;
            this.DGOrigin.MultiSelect = false;
            this.DGOrigin.Name = "DGOrigin";
            this.DGOrigin.ReadOnly = true;
            this.DGOrigin.RowHeadersVisible = false;
            // 
            // contextMenuStripOI
            // 
            this.contextMenuStripOI.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToClipboardToolStripMenuItem});
            this.contextMenuStripOI.Name = "contextMenuStripOI";
            resources.ApplyResources(this.contextMenuStripOI, "contextMenuStripOI");
            this.contextMenuStripOI.MouseClick += new System.Windows.Forms.MouseEventHandler(this.contextMenuStripOI_MouseClick);
            // 
            // copyToClipboardToolStripMenuItem
            // 
            this.copyToClipboardToolStripMenuItem.Name = "copyToClipboardToolStripMenuItem";
            resources.ApplyResources(this.copyToClipboardToolStripMenuItem, "copyToClipboardToolStripMenuItem");
            // 
            // buttonUpdateClose
            // 
            resources.ApplyResources(this.buttonUpdateClose, "buttonUpdateClose");
            this.buttonUpdateClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonUpdateClose.Name = "buttonUpdateClose";
            this.buttonUpdateClose.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Controls.Add(this.tabPageInfo);
            this.tabControl1.Controls.Add(this.tabPageSettings);
            this.tabControl1.Controls.Add(this.tabPagePolicies);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabPageInfo
            // 
            this.tabPageInfo.Controls.Add(this.DGOrigin);
            resources.ApplyResources(this.tabPageInfo, "tabPageInfo");
            this.tabPageInfo.Name = "tabPageInfo";
            this.tabPageInfo.UseVisualStyleBackColor = true;
            // 
            // tabPageSettings
            // 
            this.tabPageSettings.Controls.Add(this.moreinfoSE);
            this.tabPageSettings.Controls.Add(this.groupBoxTypeScale);
            this.tabPageSettings.Controls.Add(this.labelcdn);
            this.tabPageSettings.Controls.Add(this.panelAkamai);
            this.tabPageSettings.Controls.Add(this.panelStreamingAllowedIP);
            this.tabPageSettings.Controls.Add(this.panelCustomHostnames);
            this.tabPageSettings.Controls.Add(this.labeldesc);
            this.tabPageSettings.Controls.Add(this.textboxorigindesc);
            this.tabPageSettings.Controls.Add(this.textBoxMaxCacheAge);
            this.tabPageSettings.Controls.Add(this.lblMaxCacheAge);
            resources.ApplyResources(this.tabPageSettings, "tabPageSettings");
            this.tabPageSettings.Name = "tabPageSettings";
            this.tabPageSettings.UseVisualStyleBackColor = true;
            this.tabPageSettings.Click += new System.EventHandler(this.tabPage2_Click);
            // 
            // moreinfoSE
            // 
            resources.ApplyResources(this.moreinfoSE, "moreinfoSE");
            this.moreinfoSE.Name = "moreinfoSE";
            this.moreinfoSE.TabStop = true;
            this.moreinfoSE.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.moreinfoSE_LinkClicked);
            // 
            // groupBoxTypeScale
            // 
            this.groupBoxTypeScale.Controls.Add(this.radioButtonPremium);
            this.groupBoxTypeScale.Controls.Add(this.radioButtonStandard);
            this.groupBoxTypeScale.Controls.Add(this.numericUpDownRU);
            resources.ApplyResources(this.groupBoxTypeScale, "groupBoxTypeScale");
            this.groupBoxTypeScale.Name = "groupBoxTypeScale";
            this.groupBoxTypeScale.TabStop = false;
            // 
            // radioButtonPremium
            // 
            resources.ApplyResources(this.radioButtonPremium, "radioButtonPremium");
            this.radioButtonPremium.Name = "radioButtonPremium";
            this.toolTip1.SetToolTip(this.radioButtonPremium, resources.GetString("radioButtonPremium.ToolTip"));
            this.radioButtonPremium.UseVisualStyleBackColor = true;
            this.radioButtonPremium.CheckedChanged += new System.EventHandler(this.radioButtonPremium_CheckedChanged);
            // 
            // radioButtonStandard
            // 
            resources.ApplyResources(this.radioButtonStandard, "radioButtonStandard");
            this.radioButtonStandard.Checked = true;
            this.radioButtonStandard.Name = "radioButtonStandard";
            this.radioButtonStandard.TabStop = true;
            this.toolTip1.SetToolTip(this.radioButtonStandard, resources.GetString("radioButtonStandard.ToolTip"));
            this.radioButtonStandard.UseVisualStyleBackColor = true;
            this.radioButtonStandard.CheckedChanged += new System.EventHandler(this.radioButtonStandard_CheckedChanged);
            // 
            // numericUpDownRU
            // 
            resources.ApplyResources(this.numericUpDownRU, "numericUpDownRU");
            this.numericUpDownRU.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownRU.Name = "numericUpDownRU";
            this.numericUpDownRU.ReadOnly = true;
            this.toolTip1.SetToolTip(this.numericUpDownRU, resources.GetString("numericUpDownRU.ToolTip"));
            this.numericUpDownRU.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownRU.ValueChanged += new System.EventHandler(this.numericUpDownRU_ValueChanged);
            // 
            // labelcdn
            // 
            this.labelcdn.ForeColor = System.Drawing.SystemColors.WindowFrame;
            resources.ApplyResources(this.labelcdn, "labelcdn");
            this.labelcdn.Name = "labelcdn";
            // 
            // panelAkamai
            // 
            this.panelAkamai.Controls.Add(this.dataGridViewAkamai);
            this.panelAkamai.Controls.Add(this.buttonAddAkamai);
            this.panelAkamai.Controls.Add(this.buttonDelAkamai);
            this.panelAkamai.Controls.Add(this.checkBoxAkamai);
            resources.ApplyResources(this.panelAkamai, "panelAkamai");
            this.panelAkamai.Name = "panelAkamai";
            // 
            // dataGridViewAkamai
            // 
            this.dataGridViewAkamai.AllowUserToAddRows = false;
            this.dataGridViewAkamai.AllowUserToDeleteRows = false;
            this.dataGridViewAkamai.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewAkamai.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridViewAkamai.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.dataGridViewAkamai, "dataGridViewAkamai");
            this.dataGridViewAkamai.Name = "dataGridViewAkamai";
            this.dataGridViewAkamai.RowHeadersVisible = false;
            this.dataGridViewAkamai.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.toolTip1.SetToolTip(this.dataGridViewAkamai, resources.GetString("dataGridViewAkamai.ToolTip"));
            // 
            // buttonAddAkamai
            // 
            resources.ApplyResources(this.buttonAddAkamai, "buttonAddAkamai");
            this.buttonAddAkamai.Name = "buttonAddAkamai";
            this.buttonAddAkamai.UseVisualStyleBackColor = true;
            this.buttonAddAkamai.Click += new System.EventHandler(this.buttonAddAkamai_Click);
            // 
            // buttonDelAkamai
            // 
            resources.ApplyResources(this.buttonDelAkamai, "buttonDelAkamai");
            this.buttonDelAkamai.Name = "buttonDelAkamai";
            this.buttonDelAkamai.UseVisualStyleBackColor = true;
            this.buttonDelAkamai.Click += new System.EventHandler(this.buttonDelAkamai_Click);
            // 
            // checkBoxAkamai
            // 
            resources.ApplyResources(this.checkBoxAkamai, "checkBoxAkamai");
            this.checkBoxAkamai.Name = "checkBoxAkamai";
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
            resources.ApplyResources(this.panelStreamingAllowedIP, "panelStreamingAllowedIP");
            this.panelStreamingAllowedIP.Name = "panelStreamingAllowedIP";
            // 
            // buttonAllowAllStreamingIP
            // 
            resources.ApplyResources(this.buttonAllowAllStreamingIP, "buttonAllowAllStreamingIP");
            this.buttonAllowAllStreamingIP.Name = "buttonAllowAllStreamingIP";
            this.buttonAllowAllStreamingIP.UseVisualStyleBackColor = true;
            this.buttonAllowAllStreamingIP.Click += new System.EventHandler(this.buttonAllowAllStreamingIP_Click);
            // 
            // dataGridViewIP
            // 
            this.dataGridViewIP.AllowUserToAddRows = false;
            this.dataGridViewIP.AllowUserToDeleteRows = false;
            this.dataGridViewIP.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewIP.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridViewIP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.dataGridViewIP, "dataGridViewIP");
            this.dataGridViewIP.Name = "dataGridViewIP";
            this.dataGridViewIP.RowHeadersVisible = false;
            this.dataGridViewIP.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.toolTip1.SetToolTip(this.dataGridViewIP, resources.GetString("dataGridViewIP.ToolTip"));
            // 
            // buttonAddIP
            // 
            resources.ApplyResources(this.buttonAddIP, "buttonAddIP");
            this.buttonAddIP.Name = "buttonAddIP";
            this.buttonAddIP.UseVisualStyleBackColor = true;
            this.buttonAddIP.Click += new System.EventHandler(this.buttonAddIP_Click);
            // 
            // buttonDelIP
            // 
            resources.ApplyResources(this.buttonDelIP, "buttonDelIP");
            this.buttonDelIP.Name = "buttonDelIP";
            this.buttonDelIP.UseVisualStyleBackColor = true;
            this.buttonDelIP.Click += new System.EventHandler(this.buttonDelIP_Click);
            // 
            // checkBoxStreamingIPlistSet
            // 
            resources.ApplyResources(this.checkBoxStreamingIPlistSet, "checkBoxStreamingIPlistSet");
            this.checkBoxStreamingIPlistSet.Name = "checkBoxStreamingIPlistSet";
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
            resources.ApplyResources(this.panelCustomHostnames, "panelCustomHostnames");
            this.panelCustomHostnames.Name = "panelCustomHostnames";
            // 
            // dataGridViewCustomHostname
            // 
            this.dataGridViewCustomHostname.AllowUserToAddRows = false;
            this.dataGridViewCustomHostname.AllowUserToDeleteRows = false;
            this.dataGridViewCustomHostname.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewCustomHostname.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridViewCustomHostname.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCustomHostname.ColumnHeadersVisible = false;
            resources.ApplyResources(this.dataGridViewCustomHostname, "dataGridViewCustomHostname");
            this.dataGridViewCustomHostname.Name = "dataGridViewCustomHostname";
            this.dataGridViewCustomHostname.RowHeadersVisible = false;
            this.dataGridViewCustomHostname.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.toolTip1.SetToolTip(this.dataGridViewCustomHostname, resources.GetString("dataGridViewCustomHostname.ToolTip"));
            // 
            // buttonAddHostName
            // 
            resources.ApplyResources(this.buttonAddHostName, "buttonAddHostName");
            this.buttonAddHostName.Name = "buttonAddHostName";
            this.buttonAddHostName.UseVisualStyleBackColor = true;
            this.buttonAddHostName.Click += new System.EventHandler(this.buttonAddHostName_Click);
            // 
            // buttonDelHostName
            // 
            resources.ApplyResources(this.buttonDelHostName, "buttonDelHostName");
            this.buttonDelHostName.Name = "buttonDelHostName";
            this.buttonDelHostName.UseVisualStyleBackColor = true;
            this.buttonDelHostName.Click += new System.EventHandler(this.buttonDelHostName_Click);
            // 
            // hostnamelink
            // 
            resources.ApplyResources(this.hostnamelink, "hostnamelink");
            this.hostnamelink.Name = "hostnamelink";
            this.hostnamelink.TabStop = true;
            this.hostnamelink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.hostnamelink_LinkClicked);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // labeldesc
            // 
            resources.ApplyResources(this.labeldesc, "labeldesc");
            this.labeldesc.Name = "labeldesc";
            // 
            // textboxorigindesc
            // 
            resources.ApplyResources(this.textboxorigindesc, "textboxorigindesc");
            this.textboxorigindesc.Name = "textboxorigindesc";
            this.textboxorigindesc.TextChanged += new System.EventHandler(this.textboxorigindesc_TextChanged);
            // 
            // textBoxMaxCacheAge
            // 
            resources.ApplyResources(this.textBoxMaxCacheAge, "textBoxMaxCacheAge");
            this.textBoxMaxCacheAge.Name = "textBoxMaxCacheAge";
            this.textBoxMaxCacheAge.TextChanged += new System.EventHandler(this.textBoxMaxCacheAge_TextChanged);
            // 
            // lblMaxCacheAge
            // 
            resources.ApplyResources(this.lblMaxCacheAge, "lblMaxCacheAge");
            this.lblMaxCacheAge.Name = "lblMaxCacheAge";
            // 
            // tabPagePolicies
            // 
            this.tabPagePolicies.Controls.Add(this.buttonAddExampleCrossDomainPolicy);
            this.tabPagePolicies.Controls.Add(this.buttonAddExampleClientPolicy);
            this.tabPagePolicies.Controls.Add(this.checkBoxcrossdomain);
            this.tabPagePolicies.Controls.Add(this.textBoxCrossDomPolicy);
            this.tabPagePolicies.Controls.Add(this.checkBoxclientpolicy);
            this.tabPagePolicies.Controls.Add(this.textBoxClientPolicy);
            resources.ApplyResources(this.tabPagePolicies, "tabPagePolicies");
            this.tabPagePolicies.Name = "tabPagePolicies";
            this.tabPagePolicies.UseVisualStyleBackColor = true;
            // 
            // buttonAddExampleCrossDomainPolicy
            // 
            resources.ApplyResources(this.buttonAddExampleCrossDomainPolicy, "buttonAddExampleCrossDomainPolicy");
            this.buttonAddExampleCrossDomainPolicy.Name = "buttonAddExampleCrossDomainPolicy";
            this.buttonAddExampleCrossDomainPolicy.UseVisualStyleBackColor = true;
            this.buttonAddExampleCrossDomainPolicy.Click += new System.EventHandler(this.buttonAddExampleCrossDomainPolicy_Click);
            // 
            // buttonAddExampleClientPolicy
            // 
            resources.ApplyResources(this.buttonAddExampleClientPolicy, "buttonAddExampleClientPolicy");
            this.buttonAddExampleClientPolicy.Name = "buttonAddExampleClientPolicy";
            this.buttonAddExampleClientPolicy.UseVisualStyleBackColor = true;
            this.buttonAddExampleClientPolicy.Click += new System.EventHandler(this.buttonAddExampleClientPolicy_Click);
            // 
            // checkBoxcrossdomain
            // 
            resources.ApplyResources(this.checkBoxcrossdomain, "checkBoxcrossdomain");
            this.checkBoxcrossdomain.Name = "checkBoxcrossdomain";
            this.checkBoxcrossdomain.UseVisualStyleBackColor = true;
            this.checkBoxcrossdomain.CheckedChanged += new System.EventHandler(this.checkBoxcrossdomains_CheckedChanged_1);
            // 
            // textBoxCrossDomPolicy
            // 
            resources.ApplyResources(this.textBoxCrossDomPolicy, "textBoxCrossDomPolicy");
            this.textBoxCrossDomPolicy.Name = "textBoxCrossDomPolicy";
            this.textBoxCrossDomPolicy.TextChanged += new System.EventHandler(this.textBoxCrossDomPolicy_TextChanged);
            // 
            // checkBoxclientpolicy
            // 
            resources.ApplyResources(this.checkBoxclientpolicy, "checkBoxclientpolicy");
            this.checkBoxclientpolicy.Name = "checkBoxclientpolicy";
            this.checkBoxclientpolicy.UseVisualStyleBackColor = true;
            this.checkBoxclientpolicy.CheckedChanged += new System.EventHandler(this.checkBoxclientpolicy_CheckedChanged_1);
            // 
            // textBoxClientPolicy
            // 
            resources.ApplyResources(this.textBoxClientPolicy, "textBoxClientPolicy");
            this.textBoxClientPolicy.Name = "textBoxClientPolicy";
            this.textBoxClientPolicy.TextChanged += new System.EventHandler(this.textBoxClientPolicy_TextChanged);
            // 
            // labelSEName
            // 
            resources.ApplyResources(this.labelSEName, "labelSEName");
            this.labelSEName.ForeColor = System.Drawing.Color.DarkBlue;
            this.labelSEName.Name = "labelSEName";
            // 
            // buttonClose
            // 
            resources.ApplyResources(this.buttonClose, "buttonClose");
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonUpdateClose);
            this.panel1.Controls.Add(this.buttonClose);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // StreamingEndpointInformation
            // 
            this.AcceptButton = this.buttonClose;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonClose;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labelSEName);
            this.Controls.Add(this.tabControl1);
            this.Name = "StreamingEndpointInformation";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ChanneltInformation_FormClosed);
            this.Load += new System.EventHandler(this.StreamingEndpointInformation_Load);
            this.Shown += new System.EventHandler(this.OriginInformation_Shown);
            this.DpiChanged += new System.Windows.Forms.DpiChangedEventHandler(this.StreamingEndpointInformation_DpiChanged);
            ((System.ComponentModel.ISupportInitialize)(this.DGOrigin)).EndInit();
            this.contextMenuStripOI.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPageInfo.ResumeLayout(false);
            this.tabPageSettings.ResumeLayout(false);
            this.tabPageSettings.PerformLayout();
            this.groupBoxTypeScale.ResumeLayout(false);
            this.groupBoxTypeScale.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRU)).EndInit();
            this.panelAkamai.ResumeLayout(false);
            this.panelAkamai.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAkamai)).EndInit();
            this.panelStreamingAllowedIP.ResumeLayout(false);
            this.panelStreamingAllowedIP.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewIP)).EndInit();
            this.panelCustomHostnames.ResumeLayout(false);
            this.panelCustomHostnames.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCustomHostname)).EndInit();
            this.tabPagePolicies.ResumeLayout(false);
            this.tabPagePolicies.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView DGOrigin;
        private System.Windows.Forms.Button buttonUpdateClose;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageInfo;
        private System.Windows.Forms.TabPage tabPageSettings;
        private System.Windows.Forms.Label labelSEName;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripOI;
        private System.Windows.Forms.ToolStripMenuItem copyToClipboardToolStripMenuItem;
        private System.Windows.Forms.NumericUpDown numericUpDownRU;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Label lblMaxCacheAge;
        private System.Windows.Forms.TextBox textBoxMaxCacheAge;
        private System.Windows.Forms.DataGridView dataGridViewIP;
        private System.Windows.Forms.Button buttonDelIP;
        private System.Windows.Forms.Button buttonAddIP;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox checkBoxStreamingIPlistSet;
        private System.Windows.Forms.Label labeldesc;
        private System.Windows.Forms.TextBox textboxorigindesc;
        private System.Windows.Forms.CheckBox checkBoxAkamai;
        private System.Windows.Forms.Button buttonDelAkamai;
        private System.Windows.Forms.Button buttonAddAkamai;
        private System.Windows.Forms.DataGridView dataGridViewAkamai;
        private System.Windows.Forms.TabPage tabPagePolicies;
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
        private System.Windows.Forms.GroupBox groupBoxTypeScale;
        private System.Windows.Forms.RadioButton radioButtonPremium;
        private System.Windows.Forms.RadioButton radioButtonStandard;
        private System.Windows.Forms.LinkLabel moreinfoSE;
    }
}