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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StreamingEndpointInformation));
            DGOrigin = new System.Windows.Forms.DataGridView();
            contextMenuStripOI = new System.Windows.Forms.ContextMenuStrip(components);
            copyToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            buttonUpdateClose = new System.Windows.Forms.Button();
            tabControlStreamingEndpoint = new System.Windows.Forms.TabControl();
            tabPageInfo = new System.Windows.Forms.TabPage();
            tabPageSettings = new System.Windows.Forms.TabPage();
            moreinfoSE = new System.Windows.Forms.LinkLabel();
            groupBoxTypeScale = new System.Windows.Forms.GroupBox();
            linkLabelAutoscale = new System.Windows.Forms.LinkLabel();
            radioButtonPremium = new System.Windows.Forms.RadioButton();
            radioButtonStandard = new System.Windows.Forms.RadioButton();
            numericUpDownRU = new System.Windows.Forms.NumericUpDown();
            labelcdn = new System.Windows.Forms.Label();
            panelAkamai = new System.Windows.Forms.Panel();
            dataGridViewAkamai = new System.Windows.Forms.DataGridView();
            buttonAddAkamai = new System.Windows.Forms.Button();
            buttonDelAkamai = new System.Windows.Forms.Button();
            checkBoxAkamai = new System.Windows.Forms.CheckBox();
            panelStreamingAllowedIP = new System.Windows.Forms.Panel();
            buttonAllowAllStreamingIP = new System.Windows.Forms.Button();
            dataGridViewIP = new System.Windows.Forms.DataGridView();
            buttonAddIP = new System.Windows.Forms.Button();
            buttonDelIP = new System.Windows.Forms.Button();
            checkBoxStreamingIPlistSet = new System.Windows.Forms.CheckBox();
            panelCustomHostnames = new System.Windows.Forms.Panel();
            dataGridViewCustomHostname = new System.Windows.Forms.DataGridView();
            buttonAddHostName = new System.Windows.Forms.Button();
            buttonDelHostName = new System.Windows.Forms.Button();
            hostnamelink = new System.Windows.Forms.LinkLabel();
            label3 = new System.Windows.Forms.Label();
            labeldesc = new System.Windows.Forms.Label();
            textboxorigindesc = new System.Windows.Forms.TextBox();
            textBoxMaxCacheAge = new System.Windows.Forms.TextBox();
            lblMaxCacheAge = new System.Windows.Forms.Label();
            tabPagePolicies = new System.Windows.Forms.TabPage();
            buttonAddExampleCrossDomainPolicy = new System.Windows.Forms.Button();
            buttonAddExampleClientPolicy = new System.Windows.Forms.Button();
            checkBoxcrossdomain = new System.Windows.Forms.CheckBox();
            textBoxCrossDomPolicy = new System.Windows.Forms.TextBox();
            checkBoxclientpolicy = new System.Windows.Forms.CheckBox();
            textBoxClientPolicy = new System.Windows.Forms.TextBox();
            labelSEName = new System.Windows.Forms.Label();
            buttonClose = new System.Windows.Forms.Button();
            toolTip1 = new System.Windows.Forms.ToolTip(components);
            panel1 = new System.Windows.Forms.Panel();
            errorProvider1 = new System.Windows.Forms.ErrorProvider(components);
            ((System.ComponentModel.ISupportInitialize)DGOrigin).BeginInit();
            contextMenuStripOI.SuspendLayout();
            tabControlStreamingEndpoint.SuspendLayout();
            tabPageInfo.SuspendLayout();
            tabPageSettings.SuspendLayout();
            groupBoxTypeScale.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownRU).BeginInit();
            panelAkamai.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewAkamai).BeginInit();
            panelStreamingAllowedIP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewIP).BeginInit();
            panelCustomHostnames.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewCustomHostname).BeginInit();
            tabPagePolicies.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // DGOrigin
            // 
            DGOrigin.AllowUserToAddRows = false;
            DGOrigin.AllowUserToDeleteRows = false;
            DGOrigin.AllowUserToResizeRows = false;
            resources.ApplyResources(DGOrigin, "DGOrigin");
            DGOrigin.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            DGOrigin.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            DGOrigin.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DGOrigin.ColumnHeadersVisible = false;
            DGOrigin.ContextMenuStrip = contextMenuStripOI;
            DGOrigin.MultiSelect = false;
            DGOrigin.Name = "DGOrigin";
            DGOrigin.ReadOnly = true;
            DGOrigin.RowHeadersVisible = false;
            // 
            // contextMenuStripOI
            // 
            contextMenuStripOI.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { copyToClipboardToolStripMenuItem });
            contextMenuStripOI.Name = "contextMenuStripOI";
            resources.ApplyResources(contextMenuStripOI, "contextMenuStripOI");
            contextMenuStripOI.MouseClick += contextMenuStripOI_MouseClick;
            // 
            // copyToClipboardToolStripMenuItem
            // 
            copyToClipboardToolStripMenuItem.Name = "copyToClipboardToolStripMenuItem";
            resources.ApplyResources(copyToClipboardToolStripMenuItem, "copyToClipboardToolStripMenuItem");
            // 
            // buttonUpdateClose
            // 
            resources.ApplyResources(buttonUpdateClose, "buttonUpdateClose");
            buttonUpdateClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            buttonUpdateClose.Name = "buttonUpdateClose";
            buttonUpdateClose.UseVisualStyleBackColor = true;
            // 
            // tabControlStreamingEndpoint
            // 
            resources.ApplyResources(tabControlStreamingEndpoint, "tabControlStreamingEndpoint");
            tabControlStreamingEndpoint.Controls.Add(tabPageInfo);
            tabControlStreamingEndpoint.Controls.Add(tabPageSettings);
            tabControlStreamingEndpoint.Controls.Add(tabPagePolicies);
            tabControlStreamingEndpoint.Name = "tabControlStreamingEndpoint";
            tabControlStreamingEndpoint.SelectedIndex = 0;
            // 
            // tabPageInfo
            // 
            tabPageInfo.Controls.Add(DGOrigin);
            resources.ApplyResources(tabPageInfo, "tabPageInfo");
            tabPageInfo.Name = "tabPageInfo";
            tabPageInfo.UseVisualStyleBackColor = true;
            // 
            // tabPageSettings
            // 
            tabPageSettings.Controls.Add(moreinfoSE);
            tabPageSettings.Controls.Add(groupBoxTypeScale);
            tabPageSettings.Controls.Add(labelcdn);
            tabPageSettings.Controls.Add(panelAkamai);
            tabPageSettings.Controls.Add(panelStreamingAllowedIP);
            tabPageSettings.Controls.Add(panelCustomHostnames);
            tabPageSettings.Controls.Add(labeldesc);
            tabPageSettings.Controls.Add(textboxorigindesc);
            tabPageSettings.Controls.Add(textBoxMaxCacheAge);
            tabPageSettings.Controls.Add(lblMaxCacheAge);
            resources.ApplyResources(tabPageSettings, "tabPageSettings");
            tabPageSettings.Name = "tabPageSettings";
            tabPageSettings.UseVisualStyleBackColor = true;
            tabPageSettings.Click += tabPage2_Click;
            // 
            // moreinfoSE
            // 
            resources.ApplyResources(moreinfoSE, "moreinfoSE");
            moreinfoSE.Name = "moreinfoSE";
            moreinfoSE.TabStop = true;
            moreinfoSE.LinkClicked += moreinfoSE_LinkClicked;
            // 
            // groupBoxTypeScale
            // 
            groupBoxTypeScale.Controls.Add(linkLabelAutoscale);
            groupBoxTypeScale.Controls.Add(radioButtonPremium);
            groupBoxTypeScale.Controls.Add(radioButtonStandard);
            groupBoxTypeScale.Controls.Add(numericUpDownRU);
            resources.ApplyResources(groupBoxTypeScale, "groupBoxTypeScale");
            groupBoxTypeScale.Name = "groupBoxTypeScale";
            groupBoxTypeScale.TabStop = false;
            // 
            // linkLabelAutoscale
            // 
            resources.ApplyResources(linkLabelAutoscale, "linkLabelAutoscale");
            linkLabelAutoscale.Name = "linkLabelAutoscale";
            linkLabelAutoscale.TabStop = true;
            linkLabelAutoscale.LinkClicked += moreinfoSE_LinkClicked;
            // 
            // radioButtonPremium
            // 
            resources.ApplyResources(radioButtonPremium, "radioButtonPremium");
            radioButtonPremium.Name = "radioButtonPremium";
            toolTip1.SetToolTip(radioButtonPremium, resources.GetString("radioButtonPremium.ToolTip"));
            radioButtonPremium.UseVisualStyleBackColor = true;
            radioButtonPremium.CheckedChanged += radioButtonPremium_CheckedChanged;
            // 
            // radioButtonStandard
            // 
            resources.ApplyResources(radioButtonStandard, "radioButtonStandard");
            radioButtonStandard.Checked = true;
            radioButtonStandard.Name = "radioButtonStandard";
            radioButtonStandard.TabStop = true;
            toolTip1.SetToolTip(radioButtonStandard, resources.GetString("radioButtonStandard.ToolTip"));
            radioButtonStandard.UseVisualStyleBackColor = true;
            radioButtonStandard.CheckedChanged += radioButtonStandard_CheckedChanged;
            // 
            // numericUpDownRU
            // 
            resources.ApplyResources(numericUpDownRU, "numericUpDownRU");
            numericUpDownRU.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDownRU.Name = "numericUpDownRU";
            numericUpDownRU.ReadOnly = true;
            toolTip1.SetToolTip(numericUpDownRU, resources.GetString("numericUpDownRU.ToolTip"));
            numericUpDownRU.Value = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDownRU.ValueChanged += numericUpDownRU_ValueChanged;
            // 
            // labelcdn
            // 
            labelcdn.ForeColor = System.Drawing.SystemColors.WindowFrame;
            resources.ApplyResources(labelcdn, "labelcdn");
            labelcdn.Name = "labelcdn";
            // 
            // panelAkamai
            // 
            panelAkamai.Controls.Add(dataGridViewAkamai);
            panelAkamai.Controls.Add(buttonAddAkamai);
            panelAkamai.Controls.Add(buttonDelAkamai);
            panelAkamai.Controls.Add(checkBoxAkamai);
            resources.ApplyResources(panelAkamai, "panelAkamai");
            panelAkamai.Name = "panelAkamai";
            // 
            // dataGridViewAkamai
            // 
            dataGridViewAkamai.AllowUserToAddRows = false;
            dataGridViewAkamai.AllowUserToDeleteRows = false;
            dataGridViewAkamai.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewAkamai.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewAkamai.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(dataGridViewAkamai, "dataGridViewAkamai");
            dataGridViewAkamai.Name = "dataGridViewAkamai";
            dataGridViewAkamai.RowHeadersVisible = false;
            dataGridViewAkamai.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            toolTip1.SetToolTip(dataGridViewAkamai, resources.GetString("dataGridViewAkamai.ToolTip"));
            // 
            // buttonAddAkamai
            // 
            resources.ApplyResources(buttonAddAkamai, "buttonAddAkamai");
            buttonAddAkamai.Name = "buttonAddAkamai";
            buttonAddAkamai.UseVisualStyleBackColor = true;
            buttonAddAkamai.Click += buttonAddAkamai_Click;
            // 
            // buttonDelAkamai
            // 
            resources.ApplyResources(buttonDelAkamai, "buttonDelAkamai");
            buttonDelAkamai.Name = "buttonDelAkamai";
            buttonDelAkamai.UseVisualStyleBackColor = true;
            buttonDelAkamai.Click += buttonDelAkamai_Click;
            // 
            // checkBoxAkamai
            // 
            resources.ApplyResources(checkBoxAkamai, "checkBoxAkamai");
            checkBoxAkamai.Name = "checkBoxAkamai";
            checkBoxAkamai.UseVisualStyleBackColor = true;
            checkBoxAkamai.CheckedChanged += checkBoxAkamai_CheckedChanged;
            // 
            // panelStreamingAllowedIP
            // 
            panelStreamingAllowedIP.Controls.Add(buttonAllowAllStreamingIP);
            panelStreamingAllowedIP.Controls.Add(dataGridViewIP);
            panelStreamingAllowedIP.Controls.Add(buttonAddIP);
            panelStreamingAllowedIP.Controls.Add(buttonDelIP);
            panelStreamingAllowedIP.Controls.Add(checkBoxStreamingIPlistSet);
            resources.ApplyResources(panelStreamingAllowedIP, "panelStreamingAllowedIP");
            panelStreamingAllowedIP.Name = "panelStreamingAllowedIP";
            // 
            // buttonAllowAllStreamingIP
            // 
            resources.ApplyResources(buttonAllowAllStreamingIP, "buttonAllowAllStreamingIP");
            buttonAllowAllStreamingIP.Name = "buttonAllowAllStreamingIP";
            buttonAllowAllStreamingIP.UseVisualStyleBackColor = true;
            buttonAllowAllStreamingIP.Click += buttonAllowAllStreamingIP_Click;
            // 
            // dataGridViewIP
            // 
            dataGridViewIP.AllowUserToAddRows = false;
            dataGridViewIP.AllowUserToDeleteRows = false;
            dataGridViewIP.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewIP.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewIP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(dataGridViewIP, "dataGridViewIP");
            dataGridViewIP.Name = "dataGridViewIP";
            dataGridViewIP.RowHeadersVisible = false;
            dataGridViewIP.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            toolTip1.SetToolTip(dataGridViewIP, resources.GetString("dataGridViewIP.ToolTip"));
            // 
            // buttonAddIP
            // 
            resources.ApplyResources(buttonAddIP, "buttonAddIP");
            buttonAddIP.Name = "buttonAddIP";
            buttonAddIP.UseVisualStyleBackColor = true;
            buttonAddIP.Click += buttonAddIP_Click;
            // 
            // buttonDelIP
            // 
            resources.ApplyResources(buttonDelIP, "buttonDelIP");
            buttonDelIP.Name = "buttonDelIP";
            buttonDelIP.UseVisualStyleBackColor = true;
            buttonDelIP.Click += buttonDelIP_Click;
            // 
            // checkBoxStreamingIPlistSet
            // 
            resources.ApplyResources(checkBoxStreamingIPlistSet, "checkBoxStreamingIPlistSet");
            checkBoxStreamingIPlistSet.Name = "checkBoxStreamingIPlistSet";
            checkBoxStreamingIPlistSet.UseVisualStyleBackColor = true;
            checkBoxStreamingIPlistSet.CheckedChanged += checkBoxStreamingIPlistSet_CheckedChanged;
            // 
            // panelCustomHostnames
            // 
            panelCustomHostnames.Controls.Add(dataGridViewCustomHostname);
            panelCustomHostnames.Controls.Add(buttonAddHostName);
            panelCustomHostnames.Controls.Add(buttonDelHostName);
            panelCustomHostnames.Controls.Add(hostnamelink);
            panelCustomHostnames.Controls.Add(label3);
            resources.ApplyResources(panelCustomHostnames, "panelCustomHostnames");
            panelCustomHostnames.Name = "panelCustomHostnames";
            // 
            // dataGridViewCustomHostname
            // 
            dataGridViewCustomHostname.AllowUserToAddRows = false;
            dataGridViewCustomHostname.AllowUserToDeleteRows = false;
            dataGridViewCustomHostname.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCustomHostname.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCustomHostname.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCustomHostname.ColumnHeadersVisible = false;
            resources.ApplyResources(dataGridViewCustomHostname, "dataGridViewCustomHostname");
            dataGridViewCustomHostname.Name = "dataGridViewCustomHostname";
            dataGridViewCustomHostname.RowHeadersVisible = false;
            dataGridViewCustomHostname.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            toolTip1.SetToolTip(dataGridViewCustomHostname, resources.GetString("dataGridViewCustomHostname.ToolTip"));
            // 
            // buttonAddHostName
            // 
            resources.ApplyResources(buttonAddHostName, "buttonAddHostName");
            buttonAddHostName.Name = "buttonAddHostName";
            buttonAddHostName.UseVisualStyleBackColor = true;
            buttonAddHostName.Click += buttonAddHostName_Click;
            // 
            // buttonDelHostName
            // 
            resources.ApplyResources(buttonDelHostName, "buttonDelHostName");
            buttonDelHostName.Name = "buttonDelHostName";
            buttonDelHostName.UseVisualStyleBackColor = true;
            buttonDelHostName.Click += buttonDelHostName_Click;
            // 
            // hostnamelink
            // 
            resources.ApplyResources(hostnamelink, "hostnamelink");
            hostnamelink.Name = "hostnamelink";
            hostnamelink.TabStop = true;
            hostnamelink.LinkClicked += hostnamelink_LinkClicked;
            // 
            // label3
            // 
            resources.ApplyResources(label3, "label3");
            label3.Name = "label3";
            // 
            // labeldesc
            // 
            resources.ApplyResources(labeldesc, "labeldesc");
            labeldesc.Name = "labeldesc";
            // 
            // textboxorigindesc
            // 
            resources.ApplyResources(textboxorigindesc, "textboxorigindesc");
            textboxorigindesc.Name = "textboxorigindesc";
            textboxorigindesc.TextChanged += textboxorigindesc_TextChanged;
            // 
            // textBoxMaxCacheAge
            // 
            resources.ApplyResources(textBoxMaxCacheAge, "textBoxMaxCacheAge");
            textBoxMaxCacheAge.Name = "textBoxMaxCacheAge";
            textBoxMaxCacheAge.TextChanged += textBoxMaxCacheAge_TextChanged;
            // 
            // lblMaxCacheAge
            // 
            resources.ApplyResources(lblMaxCacheAge, "lblMaxCacheAge");
            lblMaxCacheAge.Name = "lblMaxCacheAge";
            // 
            // tabPagePolicies
            // 
            tabPagePolicies.Controls.Add(buttonAddExampleCrossDomainPolicy);
            tabPagePolicies.Controls.Add(buttonAddExampleClientPolicy);
            tabPagePolicies.Controls.Add(checkBoxcrossdomain);
            tabPagePolicies.Controls.Add(textBoxCrossDomPolicy);
            tabPagePolicies.Controls.Add(checkBoxclientpolicy);
            tabPagePolicies.Controls.Add(textBoxClientPolicy);
            resources.ApplyResources(tabPagePolicies, "tabPagePolicies");
            tabPagePolicies.Name = "tabPagePolicies";
            tabPagePolicies.UseVisualStyleBackColor = true;
            // 
            // buttonAddExampleCrossDomainPolicy
            // 
            resources.ApplyResources(buttonAddExampleCrossDomainPolicy, "buttonAddExampleCrossDomainPolicy");
            buttonAddExampleCrossDomainPolicy.Name = "buttonAddExampleCrossDomainPolicy";
            buttonAddExampleCrossDomainPolicy.UseVisualStyleBackColor = true;
            buttonAddExampleCrossDomainPolicy.Click += buttonAddExampleCrossDomainPolicy_Click;
            // 
            // buttonAddExampleClientPolicy
            // 
            resources.ApplyResources(buttonAddExampleClientPolicy, "buttonAddExampleClientPolicy");
            buttonAddExampleClientPolicy.Name = "buttonAddExampleClientPolicy";
            buttonAddExampleClientPolicy.UseVisualStyleBackColor = true;
            buttonAddExampleClientPolicy.Click += buttonAddExampleClientPolicy_Click;
            // 
            // checkBoxcrossdomain
            // 
            resources.ApplyResources(checkBoxcrossdomain, "checkBoxcrossdomain");
            checkBoxcrossdomain.Name = "checkBoxcrossdomain";
            checkBoxcrossdomain.UseVisualStyleBackColor = true;
            checkBoxcrossdomain.CheckedChanged += checkBoxcrossdomains_CheckedChanged_1;
            // 
            // textBoxCrossDomPolicy
            // 
            resources.ApplyResources(textBoxCrossDomPolicy, "textBoxCrossDomPolicy");
            textBoxCrossDomPolicy.Name = "textBoxCrossDomPolicy";
            textBoxCrossDomPolicy.TextChanged += textBoxCrossDomPolicy_TextChanged;
            // 
            // checkBoxclientpolicy
            // 
            resources.ApplyResources(checkBoxclientpolicy, "checkBoxclientpolicy");
            checkBoxclientpolicy.Name = "checkBoxclientpolicy";
            checkBoxclientpolicy.UseVisualStyleBackColor = true;
            checkBoxclientpolicy.CheckedChanged += checkBoxclientpolicy_CheckedChanged_1;
            // 
            // textBoxClientPolicy
            // 
            resources.ApplyResources(textBoxClientPolicy, "textBoxClientPolicy");
            textBoxClientPolicy.Name = "textBoxClientPolicy";
            textBoxClientPolicy.TextChanged += textBoxClientPolicy_TextChanged;
            // 
            // labelSEName
            // 
            resources.ApplyResources(labelSEName, "labelSEName");
            labelSEName.ForeColor = System.Drawing.Color.DarkBlue;
            labelSEName.Name = "labelSEName";
            // 
            // buttonClose
            // 
            resources.ApplyResources(buttonClose, "buttonClose");
            buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            buttonClose.Name = "buttonClose";
            buttonClose.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.BackColor = System.Drawing.SystemColors.Control;
            panel1.Controls.Add(buttonUpdateClose);
            panel1.Controls.Add(buttonClose);
            resources.ApplyResources(panel1, "panel1");
            panel1.Name = "panel1";
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // StreamingEndpointInformation
            // 
            AcceptButton = buttonClose;
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            BackColor = System.Drawing.SystemColors.Window;
            CancelButton = buttonClose;
            Controls.Add(panel1);
            Controls.Add(labelSEName);
            Controls.Add(tabControlStreamingEndpoint);
            Name = "StreamingEndpointInformation";
            SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            FormClosed += ChanneltInformation_FormClosed;
            Load += StreamingEndpointInformation_Load;
            Shown += OriginInformation_Shown;
            DpiChanged += StreamingEndpointInformation_DpiChanged;
            ((System.ComponentModel.ISupportInitialize)DGOrigin).EndInit();
            contextMenuStripOI.ResumeLayout(false);
            tabControlStreamingEndpoint.ResumeLayout(false);
            tabPageInfo.ResumeLayout(false);
            tabPageSettings.ResumeLayout(false);
            tabPageSettings.PerformLayout();
            groupBoxTypeScale.ResumeLayout(false);
            groupBoxTypeScale.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownRU).EndInit();
            panelAkamai.ResumeLayout(false);
            panelAkamai.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewAkamai).EndInit();
            panelStreamingAllowedIP.ResumeLayout(false);
            panelStreamingAllowedIP.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewIP).EndInit();
            panelCustomHostnames.ResumeLayout(false);
            panelCustomHostnames.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewCustomHostname).EndInit();
            tabPagePolicies.ResumeLayout(false);
            tabPagePolicies.PerformLayout();
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.DataGridView DGOrigin;
        private System.Windows.Forms.Button buttonUpdateClose;
        private System.Windows.Forms.TabControl tabControlStreamingEndpoint;
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
        private System.Windows.Forms.LinkLabel linkLabelAutoscale;
    }
}