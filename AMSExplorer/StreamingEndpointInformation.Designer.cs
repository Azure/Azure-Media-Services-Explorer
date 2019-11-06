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
            resources.ApplyResources(this.DGOrigin, "DGOrigin");
            this.DGOrigin.AllowUserToAddRows = false;
            this.DGOrigin.AllowUserToDeleteRows = false;
            this.DGOrigin.AllowUserToResizeRows = false;
            this.DGOrigin.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DGOrigin.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.DGOrigin.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGOrigin.ColumnHeadersVisible = false;
            this.DGOrigin.ContextMenuStrip = this.contextMenuStripOI;
            this.errorProvider1.SetError(this.DGOrigin, resources.GetString("DGOrigin.Error"));
            this.errorProvider1.SetIconAlignment(this.DGOrigin, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("DGOrigin.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.DGOrigin, ((int)(resources.GetObject("DGOrigin.IconPadding"))));
            this.DGOrigin.MultiSelect = false;
            this.DGOrigin.Name = "DGOrigin";
            this.DGOrigin.ReadOnly = true;
            this.DGOrigin.RowHeadersVisible = false;
            this.toolTip1.SetToolTip(this.DGOrigin, resources.GetString("DGOrigin.ToolTip"));
            // 
            // contextMenuStripOI
            // 
            resources.ApplyResources(this.contextMenuStripOI, "contextMenuStripOI");
            this.errorProvider1.SetError(this.contextMenuStripOI, resources.GetString("contextMenuStripOI.Error"));
            this.errorProvider1.SetIconAlignment(this.contextMenuStripOI, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("contextMenuStripOI.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.contextMenuStripOI, ((int)(resources.GetObject("contextMenuStripOI.IconPadding"))));
            this.contextMenuStripOI.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToClipboardToolStripMenuItem});
            this.contextMenuStripOI.Name = "contextMenuStripOI";
            this.toolTip1.SetToolTip(this.contextMenuStripOI, resources.GetString("contextMenuStripOI.ToolTip"));
            this.contextMenuStripOI.MouseClick += new System.Windows.Forms.MouseEventHandler(this.contextMenuStripOI_MouseClick);
            // 
            // copyToClipboardToolStripMenuItem
            // 
            resources.ApplyResources(this.copyToClipboardToolStripMenuItem, "copyToClipboardToolStripMenuItem");
            this.copyToClipboardToolStripMenuItem.Name = "copyToClipboardToolStripMenuItem";
            // 
            // buttonUpdateClose
            // 
            resources.ApplyResources(this.buttonUpdateClose, "buttonUpdateClose");
            this.buttonUpdateClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.errorProvider1.SetError(this.buttonUpdateClose, resources.GetString("buttonUpdateClose.Error"));
            this.errorProvider1.SetIconAlignment(this.buttonUpdateClose, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("buttonUpdateClose.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.buttonUpdateClose, ((int)(resources.GetObject("buttonUpdateClose.IconPadding"))));
            this.buttonUpdateClose.Name = "buttonUpdateClose";
            this.toolTip1.SetToolTip(this.buttonUpdateClose, resources.GetString("buttonUpdateClose.ToolTip"));
            this.buttonUpdateClose.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Controls.Add(this.tabPageInfo);
            this.tabControl1.Controls.Add(this.tabPageSettings);
            this.tabControl1.Controls.Add(this.tabPagePolicies);
            this.errorProvider1.SetError(this.tabControl1, resources.GetString("tabControl1.Error"));
            this.errorProvider1.SetIconAlignment(this.tabControl1, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("tabControl1.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.tabControl1, ((int)(resources.GetObject("tabControl1.IconPadding"))));
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.toolTip1.SetToolTip(this.tabControl1, resources.GetString("tabControl1.ToolTip"));
            // 
            // tabPageInfo
            // 
            resources.ApplyResources(this.tabPageInfo, "tabPageInfo");
            this.tabPageInfo.Controls.Add(this.DGOrigin);
            this.errorProvider1.SetError(this.tabPageInfo, resources.GetString("tabPageInfo.Error"));
            this.errorProvider1.SetIconAlignment(this.tabPageInfo, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("tabPageInfo.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.tabPageInfo, ((int)(resources.GetObject("tabPageInfo.IconPadding"))));
            this.tabPageInfo.Name = "tabPageInfo";
            this.toolTip1.SetToolTip(this.tabPageInfo, resources.GetString("tabPageInfo.ToolTip"));
            this.tabPageInfo.UseVisualStyleBackColor = true;
            // 
            // tabPageSettings
            // 
            resources.ApplyResources(this.tabPageSettings, "tabPageSettings");
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
            this.errorProvider1.SetError(this.tabPageSettings, resources.GetString("tabPageSettings.Error"));
            this.errorProvider1.SetIconAlignment(this.tabPageSettings, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("tabPageSettings.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.tabPageSettings, ((int)(resources.GetObject("tabPageSettings.IconPadding"))));
            this.tabPageSettings.Name = "tabPageSettings";
            this.toolTip1.SetToolTip(this.tabPageSettings, resources.GetString("tabPageSettings.ToolTip"));
            this.tabPageSettings.UseVisualStyleBackColor = true;
            this.tabPageSettings.Click += new System.EventHandler(this.tabPage2_Click);
            // 
            // moreinfoSE
            // 
            resources.ApplyResources(this.moreinfoSE, "moreinfoSE");
            this.errorProvider1.SetError(this.moreinfoSE, resources.GetString("moreinfoSE.Error"));
            this.errorProvider1.SetIconAlignment(this.moreinfoSE, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("moreinfoSE.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.moreinfoSE, ((int)(resources.GetObject("moreinfoSE.IconPadding"))));
            this.moreinfoSE.Name = "moreinfoSE";
            this.moreinfoSE.TabStop = true;
            this.toolTip1.SetToolTip(this.moreinfoSE, resources.GetString("moreinfoSE.ToolTip"));
            this.moreinfoSE.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.moreinfoSE_LinkClicked);
            // 
            // groupBoxTypeScale
            // 
            resources.ApplyResources(this.groupBoxTypeScale, "groupBoxTypeScale");
            this.groupBoxTypeScale.Controls.Add(this.radioButtonPremium);
            this.groupBoxTypeScale.Controls.Add(this.radioButtonStandard);
            this.groupBoxTypeScale.Controls.Add(this.numericUpDownRU);
            this.errorProvider1.SetError(this.groupBoxTypeScale, resources.GetString("groupBoxTypeScale.Error"));
            this.errorProvider1.SetIconAlignment(this.groupBoxTypeScale, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("groupBoxTypeScale.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.groupBoxTypeScale, ((int)(resources.GetObject("groupBoxTypeScale.IconPadding"))));
            this.groupBoxTypeScale.Name = "groupBoxTypeScale";
            this.groupBoxTypeScale.TabStop = false;
            this.toolTip1.SetToolTip(this.groupBoxTypeScale, resources.GetString("groupBoxTypeScale.ToolTip"));
            // 
            // radioButtonPremium
            // 
            resources.ApplyResources(this.radioButtonPremium, "radioButtonPremium");
            this.errorProvider1.SetError(this.radioButtonPremium, resources.GetString("radioButtonPremium.Error"));
            this.errorProvider1.SetIconAlignment(this.radioButtonPremium, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("radioButtonPremium.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.radioButtonPremium, ((int)(resources.GetObject("radioButtonPremium.IconPadding"))));
            this.radioButtonPremium.Name = "radioButtonPremium";
            this.toolTip1.SetToolTip(this.radioButtonPremium, resources.GetString("radioButtonPremium.ToolTip"));
            this.radioButtonPremium.UseVisualStyleBackColor = true;
            this.radioButtonPremium.CheckedChanged += new System.EventHandler(this.radioButtonPremium_CheckedChanged);
            // 
            // radioButtonStandard
            // 
            resources.ApplyResources(this.radioButtonStandard, "radioButtonStandard");
            this.radioButtonStandard.Checked = true;
            this.errorProvider1.SetError(this.radioButtonStandard, resources.GetString("radioButtonStandard.Error"));
            this.errorProvider1.SetIconAlignment(this.radioButtonStandard, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("radioButtonStandard.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.radioButtonStandard, ((int)(resources.GetObject("radioButtonStandard.IconPadding"))));
            this.radioButtonStandard.Name = "radioButtonStandard";
            this.radioButtonStandard.TabStop = true;
            this.toolTip1.SetToolTip(this.radioButtonStandard, resources.GetString("radioButtonStandard.ToolTip"));
            this.radioButtonStandard.UseVisualStyleBackColor = true;
            this.radioButtonStandard.CheckedChanged += new System.EventHandler(this.radioButtonStandard_CheckedChanged);
            // 
            // numericUpDownRU
            // 
            resources.ApplyResources(this.numericUpDownRU, "numericUpDownRU");
            this.errorProvider1.SetError(this.numericUpDownRU, resources.GetString("numericUpDownRU.Error"));
            this.errorProvider1.SetIconAlignment(this.numericUpDownRU, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("numericUpDownRU.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.numericUpDownRU, ((int)(resources.GetObject("numericUpDownRU.IconPadding"))));
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
            resources.ApplyResources(this.labelcdn, "labelcdn");
            this.errorProvider1.SetError(this.labelcdn, resources.GetString("labelcdn.Error"));
            this.labelcdn.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.errorProvider1.SetIconAlignment(this.labelcdn, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("labelcdn.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.labelcdn, ((int)(resources.GetObject("labelcdn.IconPadding"))));
            this.labelcdn.Name = "labelcdn";
            this.toolTip1.SetToolTip(this.labelcdn, resources.GetString("labelcdn.ToolTip"));
            // 
            // panelAkamai
            // 
            resources.ApplyResources(this.panelAkamai, "panelAkamai");
            this.panelAkamai.Controls.Add(this.dataGridViewAkamai);
            this.panelAkamai.Controls.Add(this.buttonAddAkamai);
            this.panelAkamai.Controls.Add(this.buttonDelAkamai);
            this.panelAkamai.Controls.Add(this.checkBoxAkamai);
            this.errorProvider1.SetError(this.panelAkamai, resources.GetString("panelAkamai.Error"));
            this.errorProvider1.SetIconAlignment(this.panelAkamai, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("panelAkamai.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.panelAkamai, ((int)(resources.GetObject("panelAkamai.IconPadding"))));
            this.panelAkamai.Name = "panelAkamai";
            this.toolTip1.SetToolTip(this.panelAkamai, resources.GetString("panelAkamai.ToolTip"));
            // 
            // dataGridViewAkamai
            // 
            resources.ApplyResources(this.dataGridViewAkamai, "dataGridViewAkamai");
            this.dataGridViewAkamai.AllowUserToAddRows = false;
            this.dataGridViewAkamai.AllowUserToDeleteRows = false;
            this.dataGridViewAkamai.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewAkamai.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridViewAkamai.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.errorProvider1.SetError(this.dataGridViewAkamai, resources.GetString("dataGridViewAkamai.Error"));
            this.errorProvider1.SetIconAlignment(this.dataGridViewAkamai, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("dataGridViewAkamai.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.dataGridViewAkamai, ((int)(resources.GetObject("dataGridViewAkamai.IconPadding"))));
            this.dataGridViewAkamai.Name = "dataGridViewAkamai";
            this.dataGridViewAkamai.RowHeadersVisible = false;
            this.dataGridViewAkamai.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.toolTip1.SetToolTip(this.dataGridViewAkamai, resources.GetString("dataGridViewAkamai.ToolTip"));
            // 
            // buttonAddAkamai
            // 
            resources.ApplyResources(this.buttonAddAkamai, "buttonAddAkamai");
            this.errorProvider1.SetError(this.buttonAddAkamai, resources.GetString("buttonAddAkamai.Error"));
            this.errorProvider1.SetIconAlignment(this.buttonAddAkamai, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("buttonAddAkamai.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.buttonAddAkamai, ((int)(resources.GetObject("buttonAddAkamai.IconPadding"))));
            this.buttonAddAkamai.Name = "buttonAddAkamai";
            this.toolTip1.SetToolTip(this.buttonAddAkamai, resources.GetString("buttonAddAkamai.ToolTip"));
            this.buttonAddAkamai.UseVisualStyleBackColor = true;
            this.buttonAddAkamai.Click += new System.EventHandler(this.buttonAddAkamai_Click);
            // 
            // buttonDelAkamai
            // 
            resources.ApplyResources(this.buttonDelAkamai, "buttonDelAkamai");
            this.errorProvider1.SetError(this.buttonDelAkamai, resources.GetString("buttonDelAkamai.Error"));
            this.errorProvider1.SetIconAlignment(this.buttonDelAkamai, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("buttonDelAkamai.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.buttonDelAkamai, ((int)(resources.GetObject("buttonDelAkamai.IconPadding"))));
            this.buttonDelAkamai.Name = "buttonDelAkamai";
            this.toolTip1.SetToolTip(this.buttonDelAkamai, resources.GetString("buttonDelAkamai.ToolTip"));
            this.buttonDelAkamai.UseVisualStyleBackColor = true;
            this.buttonDelAkamai.Click += new System.EventHandler(this.buttonDelAkamai_Click);
            // 
            // checkBoxAkamai
            // 
            resources.ApplyResources(this.checkBoxAkamai, "checkBoxAkamai");
            this.errorProvider1.SetError(this.checkBoxAkamai, resources.GetString("checkBoxAkamai.Error"));
            this.errorProvider1.SetIconAlignment(this.checkBoxAkamai, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("checkBoxAkamai.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.checkBoxAkamai, ((int)(resources.GetObject("checkBoxAkamai.IconPadding"))));
            this.checkBoxAkamai.Name = "checkBoxAkamai";
            this.toolTip1.SetToolTip(this.checkBoxAkamai, resources.GetString("checkBoxAkamai.ToolTip"));
            this.checkBoxAkamai.UseVisualStyleBackColor = true;
            this.checkBoxAkamai.CheckedChanged += new System.EventHandler(this.checkBoxAkamai_CheckedChanged);
            // 
            // panelStreamingAllowedIP
            // 
            resources.ApplyResources(this.panelStreamingAllowedIP, "panelStreamingAllowedIP");
            this.panelStreamingAllowedIP.Controls.Add(this.buttonAllowAllStreamingIP);
            this.panelStreamingAllowedIP.Controls.Add(this.dataGridViewIP);
            this.panelStreamingAllowedIP.Controls.Add(this.buttonAddIP);
            this.panelStreamingAllowedIP.Controls.Add(this.buttonDelIP);
            this.panelStreamingAllowedIP.Controls.Add(this.checkBoxStreamingIPlistSet);
            this.errorProvider1.SetError(this.panelStreamingAllowedIP, resources.GetString("panelStreamingAllowedIP.Error"));
            this.errorProvider1.SetIconAlignment(this.panelStreamingAllowedIP, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("panelStreamingAllowedIP.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.panelStreamingAllowedIP, ((int)(resources.GetObject("panelStreamingAllowedIP.IconPadding"))));
            this.panelStreamingAllowedIP.Name = "panelStreamingAllowedIP";
            this.toolTip1.SetToolTip(this.panelStreamingAllowedIP, resources.GetString("panelStreamingAllowedIP.ToolTip"));
            // 
            // buttonAllowAllStreamingIP
            // 
            resources.ApplyResources(this.buttonAllowAllStreamingIP, "buttonAllowAllStreamingIP");
            this.errorProvider1.SetError(this.buttonAllowAllStreamingIP, resources.GetString("buttonAllowAllStreamingIP.Error"));
            this.errorProvider1.SetIconAlignment(this.buttonAllowAllStreamingIP, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("buttonAllowAllStreamingIP.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.buttonAllowAllStreamingIP, ((int)(resources.GetObject("buttonAllowAllStreamingIP.IconPadding"))));
            this.buttonAllowAllStreamingIP.Name = "buttonAllowAllStreamingIP";
            this.toolTip1.SetToolTip(this.buttonAllowAllStreamingIP, resources.GetString("buttonAllowAllStreamingIP.ToolTip"));
            this.buttonAllowAllStreamingIP.UseVisualStyleBackColor = true;
            this.buttonAllowAllStreamingIP.Click += new System.EventHandler(this.buttonAllowAllStreamingIP_Click);
            // 
            // dataGridViewIP
            // 
            resources.ApplyResources(this.dataGridViewIP, "dataGridViewIP");
            this.dataGridViewIP.AllowUserToAddRows = false;
            this.dataGridViewIP.AllowUserToDeleteRows = false;
            this.dataGridViewIP.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewIP.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridViewIP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.errorProvider1.SetError(this.dataGridViewIP, resources.GetString("dataGridViewIP.Error"));
            this.errorProvider1.SetIconAlignment(this.dataGridViewIP, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("dataGridViewIP.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.dataGridViewIP, ((int)(resources.GetObject("dataGridViewIP.IconPadding"))));
            this.dataGridViewIP.Name = "dataGridViewIP";
            this.dataGridViewIP.RowHeadersVisible = false;
            this.dataGridViewIP.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.toolTip1.SetToolTip(this.dataGridViewIP, resources.GetString("dataGridViewIP.ToolTip"));
            // 
            // buttonAddIP
            // 
            resources.ApplyResources(this.buttonAddIP, "buttonAddIP");
            this.errorProvider1.SetError(this.buttonAddIP, resources.GetString("buttonAddIP.Error"));
            this.errorProvider1.SetIconAlignment(this.buttonAddIP, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("buttonAddIP.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.buttonAddIP, ((int)(resources.GetObject("buttonAddIP.IconPadding"))));
            this.buttonAddIP.Name = "buttonAddIP";
            this.toolTip1.SetToolTip(this.buttonAddIP, resources.GetString("buttonAddIP.ToolTip"));
            this.buttonAddIP.UseVisualStyleBackColor = true;
            this.buttonAddIP.Click += new System.EventHandler(this.buttonAddIP_Click);
            // 
            // buttonDelIP
            // 
            resources.ApplyResources(this.buttonDelIP, "buttonDelIP");
            this.errorProvider1.SetError(this.buttonDelIP, resources.GetString("buttonDelIP.Error"));
            this.errorProvider1.SetIconAlignment(this.buttonDelIP, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("buttonDelIP.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.buttonDelIP, ((int)(resources.GetObject("buttonDelIP.IconPadding"))));
            this.buttonDelIP.Name = "buttonDelIP";
            this.toolTip1.SetToolTip(this.buttonDelIP, resources.GetString("buttonDelIP.ToolTip"));
            this.buttonDelIP.UseVisualStyleBackColor = true;
            this.buttonDelIP.Click += new System.EventHandler(this.buttonDelIP_Click);
            // 
            // checkBoxStreamingIPlistSet
            // 
            resources.ApplyResources(this.checkBoxStreamingIPlistSet, "checkBoxStreamingIPlistSet");
            this.errorProvider1.SetError(this.checkBoxStreamingIPlistSet, resources.GetString("checkBoxStreamingIPlistSet.Error"));
            this.errorProvider1.SetIconAlignment(this.checkBoxStreamingIPlistSet, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("checkBoxStreamingIPlistSet.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.checkBoxStreamingIPlistSet, ((int)(resources.GetObject("checkBoxStreamingIPlistSet.IconPadding"))));
            this.checkBoxStreamingIPlistSet.Name = "checkBoxStreamingIPlistSet";
            this.toolTip1.SetToolTip(this.checkBoxStreamingIPlistSet, resources.GetString("checkBoxStreamingIPlistSet.ToolTip"));
            this.checkBoxStreamingIPlistSet.UseVisualStyleBackColor = true;
            this.checkBoxStreamingIPlistSet.CheckedChanged += new System.EventHandler(this.checkBoxStreamingIPlistSet_CheckedChanged);
            // 
            // panelCustomHostnames
            // 
            resources.ApplyResources(this.panelCustomHostnames, "panelCustomHostnames");
            this.panelCustomHostnames.Controls.Add(this.dataGridViewCustomHostname);
            this.panelCustomHostnames.Controls.Add(this.buttonAddHostName);
            this.panelCustomHostnames.Controls.Add(this.buttonDelHostName);
            this.panelCustomHostnames.Controls.Add(this.hostnamelink);
            this.panelCustomHostnames.Controls.Add(this.label3);
            this.errorProvider1.SetError(this.panelCustomHostnames, resources.GetString("panelCustomHostnames.Error"));
            this.errorProvider1.SetIconAlignment(this.panelCustomHostnames, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("panelCustomHostnames.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.panelCustomHostnames, ((int)(resources.GetObject("panelCustomHostnames.IconPadding"))));
            this.panelCustomHostnames.Name = "panelCustomHostnames";
            this.toolTip1.SetToolTip(this.panelCustomHostnames, resources.GetString("panelCustomHostnames.ToolTip"));
            // 
            // dataGridViewCustomHostname
            // 
            resources.ApplyResources(this.dataGridViewCustomHostname, "dataGridViewCustomHostname");
            this.dataGridViewCustomHostname.AllowUserToAddRows = false;
            this.dataGridViewCustomHostname.AllowUserToDeleteRows = false;
            this.dataGridViewCustomHostname.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewCustomHostname.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridViewCustomHostname.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCustomHostname.ColumnHeadersVisible = false;
            this.errorProvider1.SetError(this.dataGridViewCustomHostname, resources.GetString("dataGridViewCustomHostname.Error"));
            this.errorProvider1.SetIconAlignment(this.dataGridViewCustomHostname, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("dataGridViewCustomHostname.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.dataGridViewCustomHostname, ((int)(resources.GetObject("dataGridViewCustomHostname.IconPadding"))));
            this.dataGridViewCustomHostname.Name = "dataGridViewCustomHostname";
            this.dataGridViewCustomHostname.RowHeadersVisible = false;
            this.dataGridViewCustomHostname.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.toolTip1.SetToolTip(this.dataGridViewCustomHostname, resources.GetString("dataGridViewCustomHostname.ToolTip"));
            // 
            // buttonAddHostName
            // 
            resources.ApplyResources(this.buttonAddHostName, "buttonAddHostName");
            this.errorProvider1.SetError(this.buttonAddHostName, resources.GetString("buttonAddHostName.Error"));
            this.errorProvider1.SetIconAlignment(this.buttonAddHostName, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("buttonAddHostName.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.buttonAddHostName, ((int)(resources.GetObject("buttonAddHostName.IconPadding"))));
            this.buttonAddHostName.Name = "buttonAddHostName";
            this.toolTip1.SetToolTip(this.buttonAddHostName, resources.GetString("buttonAddHostName.ToolTip"));
            this.buttonAddHostName.UseVisualStyleBackColor = true;
            this.buttonAddHostName.Click += new System.EventHandler(this.buttonAddHostName_Click);
            // 
            // buttonDelHostName
            // 
            resources.ApplyResources(this.buttonDelHostName, "buttonDelHostName");
            this.errorProvider1.SetError(this.buttonDelHostName, resources.GetString("buttonDelHostName.Error"));
            this.errorProvider1.SetIconAlignment(this.buttonDelHostName, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("buttonDelHostName.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.buttonDelHostName, ((int)(resources.GetObject("buttonDelHostName.IconPadding"))));
            this.buttonDelHostName.Name = "buttonDelHostName";
            this.toolTip1.SetToolTip(this.buttonDelHostName, resources.GetString("buttonDelHostName.ToolTip"));
            this.buttonDelHostName.UseVisualStyleBackColor = true;
            this.buttonDelHostName.Click += new System.EventHandler(this.buttonDelHostName_Click);
            // 
            // hostnamelink
            // 
            resources.ApplyResources(this.hostnamelink, "hostnamelink");
            this.errorProvider1.SetError(this.hostnamelink, resources.GetString("hostnamelink.Error"));
            this.errorProvider1.SetIconAlignment(this.hostnamelink, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("hostnamelink.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.hostnamelink, ((int)(resources.GetObject("hostnamelink.IconPadding"))));
            this.hostnamelink.Name = "hostnamelink";
            this.hostnamelink.TabStop = true;
            this.toolTip1.SetToolTip(this.hostnamelink, resources.GetString("hostnamelink.ToolTip"));
            this.hostnamelink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.hostnamelink_LinkClicked);
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
            // labeldesc
            // 
            resources.ApplyResources(this.labeldesc, "labeldesc");
            this.errorProvider1.SetError(this.labeldesc, resources.GetString("labeldesc.Error"));
            this.errorProvider1.SetIconAlignment(this.labeldesc, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("labeldesc.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.labeldesc, ((int)(resources.GetObject("labeldesc.IconPadding"))));
            this.labeldesc.Name = "labeldesc";
            this.toolTip1.SetToolTip(this.labeldesc, resources.GetString("labeldesc.ToolTip"));
            // 
            // textboxorigindesc
            // 
            resources.ApplyResources(this.textboxorigindesc, "textboxorigindesc");
            this.errorProvider1.SetError(this.textboxorigindesc, resources.GetString("textboxorigindesc.Error"));
            this.errorProvider1.SetIconAlignment(this.textboxorigindesc, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("textboxorigindesc.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.textboxorigindesc, ((int)(resources.GetObject("textboxorigindesc.IconPadding"))));
            this.textboxorigindesc.Name = "textboxorigindesc";
            this.toolTip1.SetToolTip(this.textboxorigindesc, resources.GetString("textboxorigindesc.ToolTip"));
            this.textboxorigindesc.TextChanged += new System.EventHandler(this.textboxorigindesc_TextChanged);
            // 
            // textBoxMaxCacheAge
            // 
            resources.ApplyResources(this.textBoxMaxCacheAge, "textBoxMaxCacheAge");
            this.errorProvider1.SetError(this.textBoxMaxCacheAge, resources.GetString("textBoxMaxCacheAge.Error"));
            this.errorProvider1.SetIconAlignment(this.textBoxMaxCacheAge, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("textBoxMaxCacheAge.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.textBoxMaxCacheAge, ((int)(resources.GetObject("textBoxMaxCacheAge.IconPadding"))));
            this.textBoxMaxCacheAge.Name = "textBoxMaxCacheAge";
            this.toolTip1.SetToolTip(this.textBoxMaxCacheAge, resources.GetString("textBoxMaxCacheAge.ToolTip"));
            this.textBoxMaxCacheAge.TextChanged += new System.EventHandler(this.textBoxMaxCacheAge_TextChanged);
            // 
            // lblMaxCacheAge
            // 
            resources.ApplyResources(this.lblMaxCacheAge, "lblMaxCacheAge");
            this.errorProvider1.SetError(this.lblMaxCacheAge, resources.GetString("lblMaxCacheAge.Error"));
            this.errorProvider1.SetIconAlignment(this.lblMaxCacheAge, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblMaxCacheAge.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.lblMaxCacheAge, ((int)(resources.GetObject("lblMaxCacheAge.IconPadding"))));
            this.lblMaxCacheAge.Name = "lblMaxCacheAge";
            this.toolTip1.SetToolTip(this.lblMaxCacheAge, resources.GetString("lblMaxCacheAge.ToolTip"));
            // 
            // tabPagePolicies
            // 
            resources.ApplyResources(this.tabPagePolicies, "tabPagePolicies");
            this.tabPagePolicies.Controls.Add(this.buttonAddExampleCrossDomainPolicy);
            this.tabPagePolicies.Controls.Add(this.buttonAddExampleClientPolicy);
            this.tabPagePolicies.Controls.Add(this.checkBoxcrossdomain);
            this.tabPagePolicies.Controls.Add(this.textBoxCrossDomPolicy);
            this.tabPagePolicies.Controls.Add(this.checkBoxclientpolicy);
            this.tabPagePolicies.Controls.Add(this.textBoxClientPolicy);
            this.errorProvider1.SetError(this.tabPagePolicies, resources.GetString("tabPagePolicies.Error"));
            this.errorProvider1.SetIconAlignment(this.tabPagePolicies, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("tabPagePolicies.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.tabPagePolicies, ((int)(resources.GetObject("tabPagePolicies.IconPadding"))));
            this.tabPagePolicies.Name = "tabPagePolicies";
            this.toolTip1.SetToolTip(this.tabPagePolicies, resources.GetString("tabPagePolicies.ToolTip"));
            this.tabPagePolicies.UseVisualStyleBackColor = true;
            // 
            // buttonAddExampleCrossDomainPolicy
            // 
            resources.ApplyResources(this.buttonAddExampleCrossDomainPolicy, "buttonAddExampleCrossDomainPolicy");
            this.errorProvider1.SetError(this.buttonAddExampleCrossDomainPolicy, resources.GetString("buttonAddExampleCrossDomainPolicy.Error"));
            this.errorProvider1.SetIconAlignment(this.buttonAddExampleCrossDomainPolicy, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("buttonAddExampleCrossDomainPolicy.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.buttonAddExampleCrossDomainPolicy, ((int)(resources.GetObject("buttonAddExampleCrossDomainPolicy.IconPadding"))));
            this.buttonAddExampleCrossDomainPolicy.Name = "buttonAddExampleCrossDomainPolicy";
            this.toolTip1.SetToolTip(this.buttonAddExampleCrossDomainPolicy, resources.GetString("buttonAddExampleCrossDomainPolicy.ToolTip"));
            this.buttonAddExampleCrossDomainPolicy.UseVisualStyleBackColor = true;
            this.buttonAddExampleCrossDomainPolicy.Click += new System.EventHandler(this.buttonAddExampleCrossDomainPolicy_Click);
            // 
            // buttonAddExampleClientPolicy
            // 
            resources.ApplyResources(this.buttonAddExampleClientPolicy, "buttonAddExampleClientPolicy");
            this.errorProvider1.SetError(this.buttonAddExampleClientPolicy, resources.GetString("buttonAddExampleClientPolicy.Error"));
            this.errorProvider1.SetIconAlignment(this.buttonAddExampleClientPolicy, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("buttonAddExampleClientPolicy.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.buttonAddExampleClientPolicy, ((int)(resources.GetObject("buttonAddExampleClientPolicy.IconPadding"))));
            this.buttonAddExampleClientPolicy.Name = "buttonAddExampleClientPolicy";
            this.toolTip1.SetToolTip(this.buttonAddExampleClientPolicy, resources.GetString("buttonAddExampleClientPolicy.ToolTip"));
            this.buttonAddExampleClientPolicy.UseVisualStyleBackColor = true;
            this.buttonAddExampleClientPolicy.Click += new System.EventHandler(this.buttonAddExampleClientPolicy_Click);
            // 
            // checkBoxcrossdomain
            // 
            resources.ApplyResources(this.checkBoxcrossdomain, "checkBoxcrossdomain");
            this.errorProvider1.SetError(this.checkBoxcrossdomain, resources.GetString("checkBoxcrossdomain.Error"));
            this.errorProvider1.SetIconAlignment(this.checkBoxcrossdomain, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("checkBoxcrossdomain.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.checkBoxcrossdomain, ((int)(resources.GetObject("checkBoxcrossdomain.IconPadding"))));
            this.checkBoxcrossdomain.Name = "checkBoxcrossdomain";
            this.toolTip1.SetToolTip(this.checkBoxcrossdomain, resources.GetString("checkBoxcrossdomain.ToolTip"));
            this.checkBoxcrossdomain.UseVisualStyleBackColor = true;
            this.checkBoxcrossdomain.CheckedChanged += new System.EventHandler(this.checkBoxcrossdomains_CheckedChanged_1);
            // 
            // textBoxCrossDomPolicy
            // 
            resources.ApplyResources(this.textBoxCrossDomPolicy, "textBoxCrossDomPolicy");
            this.errorProvider1.SetError(this.textBoxCrossDomPolicy, resources.GetString("textBoxCrossDomPolicy.Error"));
            this.errorProvider1.SetIconAlignment(this.textBoxCrossDomPolicy, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("textBoxCrossDomPolicy.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.textBoxCrossDomPolicy, ((int)(resources.GetObject("textBoxCrossDomPolicy.IconPadding"))));
            this.textBoxCrossDomPolicy.Name = "textBoxCrossDomPolicy";
            this.toolTip1.SetToolTip(this.textBoxCrossDomPolicy, resources.GetString("textBoxCrossDomPolicy.ToolTip"));
            this.textBoxCrossDomPolicy.TextChanged += new System.EventHandler(this.textBoxCrossDomPolicy_TextChanged);
            // 
            // checkBoxclientpolicy
            // 
            resources.ApplyResources(this.checkBoxclientpolicy, "checkBoxclientpolicy");
            this.errorProvider1.SetError(this.checkBoxclientpolicy, resources.GetString("checkBoxclientpolicy.Error"));
            this.errorProvider1.SetIconAlignment(this.checkBoxclientpolicy, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("checkBoxclientpolicy.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.checkBoxclientpolicy, ((int)(resources.GetObject("checkBoxclientpolicy.IconPadding"))));
            this.checkBoxclientpolicy.Name = "checkBoxclientpolicy";
            this.toolTip1.SetToolTip(this.checkBoxclientpolicy, resources.GetString("checkBoxclientpolicy.ToolTip"));
            this.checkBoxclientpolicy.UseVisualStyleBackColor = true;
            this.checkBoxclientpolicy.CheckedChanged += new System.EventHandler(this.checkBoxclientpolicy_CheckedChanged_1);
            // 
            // textBoxClientPolicy
            // 
            resources.ApplyResources(this.textBoxClientPolicy, "textBoxClientPolicy");
            this.errorProvider1.SetError(this.textBoxClientPolicy, resources.GetString("textBoxClientPolicy.Error"));
            this.errorProvider1.SetIconAlignment(this.textBoxClientPolicy, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("textBoxClientPolicy.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.textBoxClientPolicy, ((int)(resources.GetObject("textBoxClientPolicy.IconPadding"))));
            this.textBoxClientPolicy.Name = "textBoxClientPolicy";
            this.toolTip1.SetToolTip(this.textBoxClientPolicy, resources.GetString("textBoxClientPolicy.ToolTip"));
            this.textBoxClientPolicy.TextChanged += new System.EventHandler(this.textBoxClientPolicy_TextChanged);
            // 
            // labelSEName
            // 
            resources.ApplyResources(this.labelSEName, "labelSEName");
            this.errorProvider1.SetError(this.labelSEName, resources.GetString("labelSEName.Error"));
            this.labelSEName.ForeColor = System.Drawing.Color.DarkBlue;
            this.errorProvider1.SetIconAlignment(this.labelSEName, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("labelSEName.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.labelSEName, ((int)(resources.GetObject("labelSEName.IconPadding"))));
            this.labelSEName.Name = "labelSEName";
            this.toolTip1.SetToolTip(this.labelSEName, resources.GetString("labelSEName.ToolTip"));
            // 
            // buttonClose
            // 
            resources.ApplyResources(this.buttonClose, "buttonClose");
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.errorProvider1.SetError(this.buttonClose, resources.GetString("buttonClose.Error"));
            this.errorProvider1.SetIconAlignment(this.buttonClose, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("buttonClose.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.buttonClose, ((int)(resources.GetObject("buttonClose.IconPadding"))));
            this.buttonClose.Name = "buttonClose";
            this.toolTip1.SetToolTip(this.buttonClose, resources.GetString("buttonClose.ToolTip"));
            this.buttonClose.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonUpdateClose);
            this.panel1.Controls.Add(this.buttonClose);
            this.errorProvider1.SetError(this.panel1, resources.GetString("panel1.Error"));
            this.errorProvider1.SetIconAlignment(this.panel1, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("panel1.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.panel1, ((int)(resources.GetObject("panel1.IconPadding"))));
            this.panel1.Name = "panel1";
            this.toolTip1.SetToolTip(this.panel1, resources.GetString("panel1.ToolTip"));
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            resources.ApplyResources(this.errorProvider1, "errorProvider1");
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
            this.toolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
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