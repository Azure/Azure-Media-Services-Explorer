namespace AMSExplorer
{
    partial class LiveEventInformation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LiveEventInformation));
            this.DGLiveEvent = new System.Windows.Forms.DataGridView();
            this.contextMenuStripDG = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemFilesCopyClipboard = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControlLiveEvent = new System.Windows.Forms.TabControl();
            this.tabPageLiveEventInfo = new System.Windows.Forms.TabPage();
            this.tabPageSettings = new System.Windows.Forms.TabPage();
            this.buttonAllowAllPreviewIP = new System.Windows.Forms.Button();
            this.buttonAllowAllInputIP = new System.Windows.Forms.Button();
            this.checkBoxInputSet = new System.Windows.Forms.CheckBox();
            this.checkBoxPreviewSet = new System.Windows.Forms.CheckBox();
            this.dataGridViewInputIP = new System.Windows.Forms.DataGridView();
            this.dataGridViewPreviewIP = new System.Windows.Forms.DataGridView();
            this.buttonDelPreviewIP = new System.Windows.Forms.Button();
            this.buttonDelInputIP = new System.Windows.Forms.Button();
            this.buttonAddPreviewIP = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonAddInputIP = new System.Windows.Forms.Button();
            this.textboxchannedesc = new System.Windows.Forms.TextBox();
            this.tabPageLiveTranscript = new System.Windows.Forms.TabPage();
            this.linkLabelLiveTranscriptRegions = new System.Windows.Forms.LinkLabel();
            this.comboBoxLanguage = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.linkLabelLiveTranscript = new System.Windows.Forms.LinkLabel();
            this.checkBoxEnableLiveTranscript = new System.Windows.Forms.CheckBox();
            this.tabPageEncoding = new System.Windows.Forms.TabPage();
            this.checkBoxIgnore708 = new System.Windows.Forms.CheckBox();
            this.labelLiveEventMustBeStopped = new System.Windows.Forms.Label();
            this.groupBoxEncoding = new System.Windows.Forms.GroupBox();
            this.panelDisplayEncProfile = new System.Windows.Forms.Panel();
            this.dataGridViewVideoProf = new System.Windows.Forms.DataGridView();
            this.dataGridViewAudioProf = new System.Windows.Forms.DataGridView();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.radioButtonCustomPreset = new System.Windows.Forms.RadioButton();
            this.textBoxCustomPreset = new System.Windows.Forms.TextBox();
            this.radioButtonDefaultPreset = new System.Windows.Forms.RadioButton();
            this.tabPagePolicies = new System.Windows.Forms.TabPage();
            this.checkBoxcrossdomains = new System.Windows.Forms.CheckBox();
            this.textBoxCrossDomPolicy = new System.Windows.Forms.TextBox();
            this.checkBoxclientpolicy = new System.Windows.Forms.CheckBox();
            this.textBoxClientPolicy = new System.Windows.Forms.TextBox();
            this.tabPageAdvanced = new System.Windows.Forms.TabPage();
            this.panelAdvanced = new System.Windows.Forms.Panel();
            this.radioButtonLowLatencyV2 = new System.Windows.Forms.RadioButton();
            this.radioButtonLowLatencyV1 = new System.Windows.Forms.RadioButton();
            this.checkBoxLowLatency = new System.Windows.Forms.CheckBox();
            this.checkBoxEncodingKeyFrameInterval = new System.Windows.Forms.CheckBox();
            this.textBoxKeyFrame = new System.Windows.Forms.TextBox();
            this.checkBoxKeyFrameIntDefined = new System.Windows.Forms.CheckBox();
            this.textBoxEncodingKeyFrameInterval = new System.Windows.Forms.TextBox();
            this.tabPagePreview = new System.Windows.Forms.TabPage();
            this.webBrowserPreview = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.labelLEName = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonUpdateClose = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelLiveEventStoppedOrStartedSettings = new System.Windows.Forms.Label();
            this.openFileDialogSlate = new System.Windows.Forms.OpenFileDialog();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.pictureBoxLE = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.DGLiveEvent)).BeginInit();
            this.contextMenuStripDG.SuspendLayout();
            this.tabControlLiveEvent.SuspendLayout();
            this.tabPageLiveEventInfo.SuspendLayout();
            this.tabPageSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInputIP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPreviewIP)).BeginInit();
            this.tabPageLiveTranscript.SuspendLayout();
            this.tabPageEncoding.SuspendLayout();
            this.groupBoxEncoding.SuspendLayout();
            this.panelDisplayEncProfile.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewVideoProf)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAudioProf)).BeginInit();
            this.tabPagePolicies.SuspendLayout();
            this.tabPageAdvanced.SuspendLayout();
            this.panelAdvanced.SuspendLayout();
            this.tabPagePreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.webBrowserPreview)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLE)).BeginInit();
            this.SuspendLayout();
            // 
            // DGLiveEvent
            // 
            this.DGLiveEvent.AllowUserToAddRows = false;
            this.DGLiveEvent.AllowUserToDeleteRows = false;
            this.DGLiveEvent.AllowUserToResizeRows = false;
            resources.ApplyResources(this.DGLiveEvent, "DGLiveEvent");
            this.DGLiveEvent.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DGLiveEvent.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.DGLiveEvent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGLiveEvent.ColumnHeadersVisible = false;
            this.DGLiveEvent.ContextMenuStrip = this.contextMenuStripDG;
            this.DGLiveEvent.MultiSelect = false;
            this.DGLiveEvent.Name = "DGLiveEvent";
            this.DGLiveEvent.ReadOnly = true;
            this.DGLiveEvent.RowHeadersVisible = false;
            // 
            // contextMenuStripDG
            // 
            this.contextMenuStripDG.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.contextMenuStripDG.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemFilesCopyClipboard});
            this.contextMenuStripDG.Name = "contextMenuStripDG";
            resources.ApplyResources(this.contextMenuStripDG, "contextMenuStripDG");
            this.contextMenuStripDG.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripDG_Opening);
            this.contextMenuStripDG.MouseClick += new System.Windows.Forms.MouseEventHandler(this.contextMenuStripDG_MouseClick);
            // 
            // toolStripMenuItemFilesCopyClipboard
            // 
            this.toolStripMenuItemFilesCopyClipboard.Name = "toolStripMenuItemFilesCopyClipboard";
            resources.ApplyResources(this.toolStripMenuItemFilesCopyClipboard, "toolStripMenuItemFilesCopyClipboard");
            this.toolStripMenuItemFilesCopyClipboard.Click += new System.EventHandler(this.toolStripMenuItemFilesCopyClipboard_Click);
            // 
            // tabControlLiveEvent
            // 
            resources.ApplyResources(this.tabControlLiveEvent, "tabControlLiveEvent");
            this.tabControlLiveEvent.Controls.Add(this.tabPageLiveEventInfo);
            this.tabControlLiveEvent.Controls.Add(this.tabPageSettings);
            this.tabControlLiveEvent.Controls.Add(this.tabPageLiveTranscript);
            this.tabControlLiveEvent.Controls.Add(this.tabPageEncoding);
            this.tabControlLiveEvent.Controls.Add(this.tabPagePolicies);
            this.tabControlLiveEvent.Controls.Add(this.tabPageAdvanced);
            this.tabControlLiveEvent.Controls.Add(this.tabPagePreview);
            this.tabControlLiveEvent.Name = "tabControlLiveEvent";
            this.tabControlLiveEvent.SelectedIndex = 0;
            // 
            // tabPageLiveEventInfo
            // 
            this.tabPageLiveEventInfo.Controls.Add(this.DGLiveEvent);
            resources.ApplyResources(this.tabPageLiveEventInfo, "tabPageLiveEventInfo");
            this.tabPageLiveEventInfo.Name = "tabPageLiveEventInfo";
            this.tabPageLiveEventInfo.UseVisualStyleBackColor = true;
            // 
            // tabPageSettings
            // 
            this.tabPageSettings.Controls.Add(this.buttonAllowAllPreviewIP);
            this.tabPageSettings.Controls.Add(this.buttonAllowAllInputIP);
            this.tabPageSettings.Controls.Add(this.checkBoxInputSet);
            this.tabPageSettings.Controls.Add(this.checkBoxPreviewSet);
            this.tabPageSettings.Controls.Add(this.dataGridViewInputIP);
            this.tabPageSettings.Controls.Add(this.dataGridViewPreviewIP);
            this.tabPageSettings.Controls.Add(this.buttonDelPreviewIP);
            this.tabPageSettings.Controls.Add(this.buttonDelInputIP);
            this.tabPageSettings.Controls.Add(this.buttonAddPreviewIP);
            this.tabPageSettings.Controls.Add(this.label2);
            this.tabPageSettings.Controls.Add(this.buttonAddInputIP);
            this.tabPageSettings.Controls.Add(this.textboxchannedesc);
            resources.ApplyResources(this.tabPageSettings, "tabPageSettings");
            this.tabPageSettings.Name = "tabPageSettings";
            this.tabPageSettings.UseVisualStyleBackColor = true;
            // 
            // buttonAllowAllPreviewIP
            // 
            resources.ApplyResources(this.buttonAllowAllPreviewIP, "buttonAllowAllPreviewIP");
            this.buttonAllowAllPreviewIP.Name = "buttonAllowAllPreviewIP";
            this.buttonAllowAllPreviewIP.UseVisualStyleBackColor = true;
            this.buttonAllowAllPreviewIP.Click += new System.EventHandler(this.buttonAllowAllPreviewIP_Click);
            // 
            // buttonAllowAllInputIP
            // 
            resources.ApplyResources(this.buttonAllowAllInputIP, "buttonAllowAllInputIP");
            this.buttonAllowAllInputIP.Name = "buttonAllowAllInputIP";
            this.buttonAllowAllInputIP.UseVisualStyleBackColor = true;
            this.buttonAllowAllInputIP.Click += new System.EventHandler(this.buttonAllowAllInputIP_Click);
            // 
            // checkBoxInputSet
            // 
            resources.ApplyResources(this.checkBoxInputSet, "checkBoxInputSet");
            this.checkBoxInputSet.Name = "checkBoxInputSet";
            this.checkBoxInputSet.UseVisualStyleBackColor = true;
            this.checkBoxInputSet.CheckedChanged += new System.EventHandler(this.checkBoxInputSet_CheckedChanged);
            // 
            // checkBoxPreviewSet
            // 
            resources.ApplyResources(this.checkBoxPreviewSet, "checkBoxPreviewSet");
            this.checkBoxPreviewSet.Name = "checkBoxPreviewSet";
            this.checkBoxPreviewSet.UseVisualStyleBackColor = true;
            this.checkBoxPreviewSet.CheckedChanged += new System.EventHandler(this.checkBoxPreviewSet_CheckedChanged);
            // 
            // dataGridViewInputIP
            // 
            this.dataGridViewInputIP.AllowUserToAddRows = false;
            this.dataGridViewInputIP.AllowUserToDeleteRows = false;
            this.dataGridViewInputIP.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewInputIP.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridViewInputIP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.dataGridViewInputIP, "dataGridViewInputIP");
            this.dataGridViewInputIP.Name = "dataGridViewInputIP";
            this.dataGridViewInputIP.RowHeadersVisible = false;
            this.dataGridViewInputIP.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewInputIP.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridViewInputIP_CellValidating);
            // 
            // dataGridViewPreviewIP
            // 
            this.dataGridViewPreviewIP.AllowUserToAddRows = false;
            this.dataGridViewPreviewIP.AllowUserToDeleteRows = false;
            this.dataGridViewPreviewIP.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewPreviewIP.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridViewPreviewIP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.dataGridViewPreviewIP, "dataGridViewPreviewIP");
            this.dataGridViewPreviewIP.Name = "dataGridViewPreviewIP";
            this.dataGridViewPreviewIP.RowHeadersVisible = false;
            this.dataGridViewPreviewIP.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // buttonDelPreviewIP
            // 
            resources.ApplyResources(this.buttonDelPreviewIP, "buttonDelPreviewIP");
            this.buttonDelPreviewIP.Name = "buttonDelPreviewIP";
            this.buttonDelPreviewIP.UseVisualStyleBackColor = true;
            this.buttonDelPreviewIP.Click += new System.EventHandler(this.buttonDelPreviewIP_Click);
            // 
            // buttonDelInputIP
            // 
            resources.ApplyResources(this.buttonDelInputIP, "buttonDelInputIP");
            this.buttonDelInputIP.Name = "buttonDelInputIP";
            this.buttonDelInputIP.UseVisualStyleBackColor = true;
            this.buttonDelInputIP.Click += new System.EventHandler(this.buttonDelIngestIP_Click);
            // 
            // buttonAddPreviewIP
            // 
            resources.ApplyResources(this.buttonAddPreviewIP, "buttonAddPreviewIP");
            this.buttonAddPreviewIP.Name = "buttonAddPreviewIP";
            this.buttonAddPreviewIP.UseVisualStyleBackColor = true;
            this.buttonAddPreviewIP.Click += new System.EventHandler(this.buttonAddPreviewIP_Click);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // buttonAddInputIP
            // 
            resources.ApplyResources(this.buttonAddInputIP, "buttonAddInputIP");
            this.buttonAddInputIP.Name = "buttonAddInputIP";
            this.buttonAddInputIP.UseVisualStyleBackColor = true;
            this.buttonAddInputIP.Click += new System.EventHandler(this.buttonAddIngestIP_Click);
            // 
            // textboxchannedesc
            // 
            resources.ApplyResources(this.textboxchannedesc, "textboxchannedesc");
            this.textboxchannedesc.Name = "textboxchannedesc";
            this.textboxchannedesc.TextChanged += new System.EventHandler(this.textboxchannedesc_TextChanged);
            // 
            // tabPageLiveTranscript
            // 
            this.tabPageLiveTranscript.Controls.Add(this.linkLabelLiveTranscriptRegions);
            this.tabPageLiveTranscript.Controls.Add(this.comboBoxLanguage);
            this.tabPageLiveTranscript.Controls.Add(this.label1);
            this.tabPageLiveTranscript.Controls.Add(this.linkLabelLiveTranscript);
            this.tabPageLiveTranscript.Controls.Add(this.checkBoxEnableLiveTranscript);
            resources.ApplyResources(this.tabPageLiveTranscript, "tabPageLiveTranscript");
            this.tabPageLiveTranscript.Name = "tabPageLiveTranscript";
            this.tabPageLiveTranscript.UseVisualStyleBackColor = true;
            // 
            // linkLabelLiveTranscriptRegions
            // 
            resources.ApplyResources(this.linkLabelLiveTranscriptRegions, "linkLabelLiveTranscriptRegions");
            this.linkLabelLiveTranscriptRegions.Name = "linkLabelLiveTranscriptRegions";
            this.linkLabelLiveTranscriptRegions.TabStop = true;
            this.linkLabelLiveTranscriptRegions.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelLiveTranscript_LinkClicked);
            // 
            // comboBoxLanguage
            // 
            this.comboBoxLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.comboBoxLanguage, "comboBoxLanguage");
            this.comboBoxLanguage.FormattingEnabled = true;
            this.comboBoxLanguage.Name = "comboBoxLanguage";
            this.comboBoxLanguage.SelectedIndexChanged += new System.EventHandler(this.comboBoxLanguage_SelectedIndexChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // linkLabelLiveTranscript
            // 
            resources.ApplyResources(this.linkLabelLiveTranscript, "linkLabelLiveTranscript");
            this.linkLabelLiveTranscript.Name = "linkLabelLiveTranscript";
            this.linkLabelLiveTranscript.TabStop = true;
            this.linkLabelLiveTranscript.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelLiveTranscript_LinkClicked);
            // 
            // checkBoxEnableLiveTranscript
            // 
            resources.ApplyResources(this.checkBoxEnableLiveTranscript, "checkBoxEnableLiveTranscript");
            this.checkBoxEnableLiveTranscript.Name = "checkBoxEnableLiveTranscript";
            this.checkBoxEnableLiveTranscript.UseVisualStyleBackColor = true;
            this.checkBoxEnableLiveTranscript.CheckedChanged += new System.EventHandler(this.checkBoxEnableLiveTranscript_CheckedChanged);
            // 
            // tabPageEncoding
            // 
            this.tabPageEncoding.Controls.Add(this.checkBoxIgnore708);
            this.tabPageEncoding.Controls.Add(this.labelLiveEventMustBeStopped);
            this.tabPageEncoding.Controls.Add(this.groupBoxEncoding);
            resources.ApplyResources(this.tabPageEncoding, "tabPageEncoding");
            this.tabPageEncoding.Name = "tabPageEncoding";
            this.tabPageEncoding.UseVisualStyleBackColor = true;
            // 
            // checkBoxIgnore708
            // 
            resources.ApplyResources(this.checkBoxIgnore708, "checkBoxIgnore708");
            this.checkBoxIgnore708.Name = "checkBoxIgnore708";
            this.checkBoxIgnore708.UseVisualStyleBackColor = true;
            this.checkBoxIgnore708.CheckedChanged += new System.EventHandler(this.checkBoxIgnore708_CheckedChanged);
            // 
            // labelLiveEventMustBeStopped
            // 
            resources.ApplyResources(this.labelLiveEventMustBeStopped, "labelLiveEventMustBeStopped");
            this.labelLiveEventMustBeStopped.Name = "labelLiveEventMustBeStopped";
            // 
            // groupBoxEncoding
            // 
            resources.ApplyResources(this.groupBoxEncoding, "groupBoxEncoding");
            this.groupBoxEncoding.Controls.Add(this.panelDisplayEncProfile);
            this.groupBoxEncoding.Controls.Add(this.label6);
            this.groupBoxEncoding.Controls.Add(this.radioButtonCustomPreset);
            this.groupBoxEncoding.Controls.Add(this.textBoxCustomPreset);
            this.groupBoxEncoding.Controls.Add(this.radioButtonDefaultPreset);
            this.groupBoxEncoding.Name = "groupBoxEncoding";
            this.groupBoxEncoding.TabStop = false;
            // 
            // panelDisplayEncProfile
            // 
            resources.ApplyResources(this.panelDisplayEncProfile, "panelDisplayEncProfile");
            this.panelDisplayEncProfile.Controls.Add(this.dataGridViewVideoProf);
            this.panelDisplayEncProfile.Controls.Add(this.dataGridViewAudioProf);
            this.panelDisplayEncProfile.Controls.Add(this.label16);
            this.panelDisplayEncProfile.Controls.Add(this.label17);
            this.panelDisplayEncProfile.Name = "panelDisplayEncProfile";
            // 
            // dataGridViewVideoProf
            // 
            this.dataGridViewVideoProf.AllowUserToAddRows = false;
            this.dataGridViewVideoProf.AllowUserToDeleteRows = false;
            resources.ApplyResources(this.dataGridViewVideoProf, "dataGridViewVideoProf");
            this.dataGridViewVideoProf.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewVideoProf.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridViewVideoProf.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewVideoProf.Name = "dataGridViewVideoProf";
            this.dataGridViewVideoProf.ReadOnly = true;
            this.dataGridViewVideoProf.RowHeadersVisible = false;
            // 
            // dataGridViewAudioProf
            // 
            this.dataGridViewAudioProf.AllowUserToAddRows = false;
            this.dataGridViewAudioProf.AllowUserToDeleteRows = false;
            resources.ApplyResources(this.dataGridViewAudioProf, "dataGridViewAudioProf");
            this.dataGridViewAudioProf.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewAudioProf.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridViewAudioProf.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewAudioProf.Name = "dataGridViewAudioProf";
            this.dataGridViewAudioProf.ReadOnly = true;
            this.dataGridViewAudioProf.RowHeadersVisible = false;
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.Name = "label16";
            // 
            // label17
            // 
            resources.ApplyResources(this.label17, "label17");
            this.label17.Name = "label17";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label6.Name = "label6";
            // 
            // radioButtonCustomPreset
            // 
            resources.ApplyResources(this.radioButtonCustomPreset, "radioButtonCustomPreset");
            this.radioButtonCustomPreset.Name = "radioButtonCustomPreset";
            this.radioButtonCustomPreset.UseVisualStyleBackColor = true;
            this.radioButtonCustomPreset.CheckedChanged += new System.EventHandler(this.radioButtonCustomPreset_CheckedChanged);
            // 
            // textBoxCustomPreset
            // 
            resources.ApplyResources(this.textBoxCustomPreset, "textBoxCustomPreset");
            this.textBoxCustomPreset.Name = "textBoxCustomPreset";
            this.textBoxCustomPreset.TextChanged += new System.EventHandler(this.textBoxCustomPreset_TextChanged);
            // 
            // radioButtonDefaultPreset
            // 
            resources.ApplyResources(this.radioButtonDefaultPreset, "radioButtonDefaultPreset");
            this.radioButtonDefaultPreset.Checked = true;
            this.radioButtonDefaultPreset.Name = "radioButtonDefaultPreset";
            this.radioButtonDefaultPreset.TabStop = true;
            this.radioButtonDefaultPreset.UseVisualStyleBackColor = true;
            this.radioButtonDefaultPreset.CheckedChanged += new System.EventHandler(this.radioButtonDefaultPreset_CheckedChanged);
            // 
            // tabPagePolicies
            // 
            this.tabPagePolicies.Controls.Add(this.checkBoxcrossdomains);
            this.tabPagePolicies.Controls.Add(this.textBoxCrossDomPolicy);
            this.tabPagePolicies.Controls.Add(this.checkBoxclientpolicy);
            this.tabPagePolicies.Controls.Add(this.textBoxClientPolicy);
            resources.ApplyResources(this.tabPagePolicies, "tabPagePolicies");
            this.tabPagePolicies.Name = "tabPagePolicies";
            this.tabPagePolicies.UseVisualStyleBackColor = true;
            // 
            // checkBoxcrossdomains
            // 
            resources.ApplyResources(this.checkBoxcrossdomains, "checkBoxcrossdomains");
            this.checkBoxcrossdomains.Name = "checkBoxcrossdomains";
            this.checkBoxcrossdomains.UseVisualStyleBackColor = true;
            this.checkBoxcrossdomains.CheckedChanged += new System.EventHandler(this.checkBoxcrossdomains_CheckedChanged_1);
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
            // tabPageAdvanced
            // 
            this.tabPageAdvanced.Controls.Add(this.panelAdvanced);
            resources.ApplyResources(this.tabPageAdvanced, "tabPageAdvanced");
            this.tabPageAdvanced.Name = "tabPageAdvanced";
            this.tabPageAdvanced.UseVisualStyleBackColor = true;
            // 
            // panelAdvanced
            // 
            this.panelAdvanced.Controls.Add(this.radioButtonLowLatencyV2);
            this.panelAdvanced.Controls.Add(this.radioButtonLowLatencyV1);
            this.panelAdvanced.Controls.Add(this.checkBoxLowLatency);
            this.panelAdvanced.Controls.Add(this.checkBoxEncodingKeyFrameInterval);
            this.panelAdvanced.Controls.Add(this.textBoxKeyFrame);
            this.panelAdvanced.Controls.Add(this.checkBoxKeyFrameIntDefined);
            this.panelAdvanced.Controls.Add(this.textBoxEncodingKeyFrameInterval);
            resources.ApplyResources(this.panelAdvanced, "panelAdvanced");
            this.panelAdvanced.Name = "panelAdvanced";
            // 
            // radioButtonLowLatencyV2
            // 
            resources.ApplyResources(this.radioButtonLowLatencyV2, "radioButtonLowLatencyV2");
            this.radioButtonLowLatencyV2.Name = "radioButtonLowLatencyV2";
            this.radioButtonLowLatencyV2.UseVisualStyleBackColor = true;
            this.radioButtonLowLatencyV2.CheckedChanged += new System.EventHandler(this.radioButtonLowLatencyV2_CheckedChanged);
            // 
            // radioButtonLowLatencyV1
            // 
            resources.ApplyResources(this.radioButtonLowLatencyV1, "radioButtonLowLatencyV1");
            this.radioButtonLowLatencyV1.Checked = true;
            this.radioButtonLowLatencyV1.Name = "radioButtonLowLatencyV1";
            this.radioButtonLowLatencyV1.TabStop = true;
            this.radioButtonLowLatencyV1.UseVisualStyleBackColor = true;
            // 
            // checkBoxLowLatency
            // 
            resources.ApplyResources(this.checkBoxLowLatency, "checkBoxLowLatency");
            this.checkBoxLowLatency.Name = "checkBoxLowLatency";
            this.checkBoxLowLatency.UseVisualStyleBackColor = true;
            this.checkBoxLowLatency.CheckedChanged += new System.EventHandler(this.checkBoxLowLatency_CheckedChanged);
            // 
            // checkBoxEncodingKeyFrameInterval
            // 
            resources.ApplyResources(this.checkBoxEncodingKeyFrameInterval, "checkBoxEncodingKeyFrameInterval");
            this.checkBoxEncodingKeyFrameInterval.Name = "checkBoxEncodingKeyFrameInterval";
            this.checkBoxEncodingKeyFrameInterval.UseVisualStyleBackColor = true;
            this.checkBoxEncodingKeyFrameInterval.CheckedChanged += new System.EventHandler(this.checkBoxEncodingKeyFrameInterval_CheckedChanged);
            // 
            // textBoxKeyFrame
            // 
            resources.ApplyResources(this.textBoxKeyFrame, "textBoxKeyFrame");
            this.textBoxKeyFrame.Name = "textBoxKeyFrame";
            this.textBoxKeyFrame.TextChanged += new System.EventHandler(this.textBoxKeyFrame_TextChanged);
            // 
            // checkBoxKeyFrameIntDefined
            // 
            resources.ApplyResources(this.checkBoxKeyFrameIntDefined, "checkBoxKeyFrameIntDefined");
            this.checkBoxKeyFrameIntDefined.Name = "checkBoxKeyFrameIntDefined";
            this.checkBoxKeyFrameIntDefined.UseVisualStyleBackColor = true;
            this.checkBoxKeyFrameIntDefined.CheckedChanged += new System.EventHandler(this.checkBoxKeyFrameIntDefined_CheckedChanged_1);
            this.checkBoxKeyFrameIntDefined.TextChanged += new System.EventHandler(this.checkBoxKeyFrameIntDefined_TextChanged);
            // 
            // textBoxEncodingKeyFrameInterval
            // 
            resources.ApplyResources(this.textBoxEncodingKeyFrameInterval, "textBoxEncodingKeyFrameInterval");
            this.textBoxEncodingKeyFrameInterval.Name = "textBoxEncodingKeyFrameInterval";
            this.textBoxEncodingKeyFrameInterval.TextChanged += new System.EventHandler(this.textBoxEncodingKeyFrameInterval_TextChanged);
            // 
            // tabPagePreview
            // 
            this.tabPagePreview.Controls.Add(this.webBrowserPreview);
            resources.ApplyResources(this.tabPagePreview, "tabPagePreview");
            this.tabPagePreview.Name = "tabPagePreview";
            this.tabPagePreview.UseVisualStyleBackColor = true;
            this.tabPagePreview.Enter += new System.EventHandler(this.tabPage4_Enter);
            this.tabPagePreview.Leave += new System.EventHandler(this.tabPage4_Leave);
            // 
            // webBrowserPreview
            // 
            this.webBrowserPreview.AllowExternalDrop = true;
            this.webBrowserPreview.CreationProperties = null;
            this.webBrowserPreview.DefaultBackgroundColor = System.Drawing.Color.White;
            resources.ApplyResources(this.webBrowserPreview, "webBrowserPreview");
            this.webBrowserPreview.Name = "webBrowserPreview";
            this.webBrowserPreview.ZoomFactor = 1D;
            // 
            // labelLEName
            // 
            resources.ApplyResources(this.labelLEName, "labelLEName");
            this.labelLEName.Name = "labelLEName";
            // 
            // buttonClose
            // 
            resources.ApplyResources(this.buttonClose, "buttonClose");
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.UseVisualStyleBackColor = true;
            // 
            // buttonUpdateClose
            // 
            resources.ApplyResources(this.buttonUpdateClose, "buttonUpdateClose");
            this.buttonUpdateClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonUpdateClose.Name = "buttonUpdateClose";
            this.buttonUpdateClose.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.labelLiveEventStoppedOrStartedSettings);
            this.panel1.Controls.Add(this.buttonClose);
            this.panel1.Controls.Add(this.buttonUpdateClose);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // labelLiveEventStoppedOrStartedSettings
            // 
            resources.ApplyResources(this.labelLiveEventStoppedOrStartedSettings, "labelLiveEventStoppedOrStartedSettings");
            this.labelLiveEventStoppedOrStartedSettings.Name = "labelLiveEventStoppedOrStartedSettings";
            // 
            // openFileDialogSlate
            // 
            resources.ApplyResources(this.openFileDialogSlate, "openFileDialogSlate");
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // pictureBoxLE
            // 
            this.pictureBoxLE.Image = global::AMSExplorer.Bitmaps.encoding;
            resources.ApplyResources(this.pictureBoxLE, "pictureBoxLE");
            this.pictureBoxLE.Name = "pictureBoxLE";
            this.pictureBoxLE.TabStop = false;
            // 
            // LiveEventInformation
            // 
            this.AcceptButton = this.buttonClose;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonClose;
            this.Controls.Add(this.pictureBoxLE);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labelLEName);
            this.Controls.Add(this.tabControlLiveEvent);
            this.Name = "LiveEventInformation";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LiveEventInformation_FormClosed);
            this.Load += new System.EventHandler(this.LiveEventInformation_Load);
            this.Shown += new System.EventHandler(this.LiveEventInformation_Shown);
            this.DpiChanged += new System.Windows.Forms.DpiChangedEventHandler(this.LiveEventInformation_DpiChanged);
            ((System.ComponentModel.ISupportInitialize)(this.DGLiveEvent)).EndInit();
            this.contextMenuStripDG.ResumeLayout(false);
            this.tabControlLiveEvent.ResumeLayout(false);
            this.tabPageLiveEventInfo.ResumeLayout(false);
            this.tabPageSettings.ResumeLayout(false);
            this.tabPageSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInputIP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPreviewIP)).EndInit();
            this.tabPageLiveTranscript.ResumeLayout(false);
            this.tabPageLiveTranscript.PerformLayout();
            this.tabPageEncoding.ResumeLayout(false);
            this.tabPageEncoding.PerformLayout();
            this.groupBoxEncoding.ResumeLayout(false);
            this.groupBoxEncoding.PerformLayout();
            this.panelDisplayEncProfile.ResumeLayout(false);
            this.panelDisplayEncProfile.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewVideoProf)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAudioProf)).EndInit();
            this.tabPagePolicies.ResumeLayout(false);
            this.tabPagePolicies.PerformLayout();
            this.tabPageAdvanced.ResumeLayout(false);
            this.panelAdvanced.ResumeLayout(false);
            this.panelAdvanced.PerformLayout();
            this.tabPagePreview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.webBrowserPreview)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLE)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DGLiveEvent;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripDG;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFilesCopyClipboard;
        private System.Windows.Forms.TabControl tabControlLiveEvent;
        private System.Windows.Forms.TabPage tabPageLiveEventInfo;
        private System.Windows.Forms.Label labelLEName;
        private System.Windows.Forms.TabPage tabPageSettings;
        private System.Windows.Forms.DataGridView dataGridViewPreviewIP;
        private System.Windows.Forms.Button buttonDelPreviewIP;
        private System.Windows.Forms.Button buttonAddPreviewIP;
        private System.Windows.Forms.DataGridView dataGridViewInputIP;
        private System.Windows.Forms.Button buttonDelInputIP;
        private System.Windows.Forms.Button buttonAddInputIP;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonUpdateClose;
        private System.Windows.Forms.CheckBox checkBoxPreviewSet;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textboxchannedesc;
        private System.Windows.Forms.TabPage tabPagePolicies;
        private System.Windows.Forms.CheckBox checkBoxcrossdomains;
        private System.Windows.Forms.TextBox textBoxCrossDomPolicy;
        private System.Windows.Forms.CheckBox checkBoxclientpolicy;
        private System.Windows.Forms.TextBox textBoxClientPolicy;
        private System.Windows.Forms.CheckBox checkBoxInputSet;
        private System.Windows.Forms.CheckBox checkBoxKeyFrameIntDefined;
        private System.Windows.Forms.TextBox textBoxKeyFrame;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonAllowAllInputIP;
        private System.Windows.Forms.Button buttonAllowAllPreviewIP;
        private System.Windows.Forms.TabPage tabPagePreview;
        private System.Windows.Forms.OpenFileDialog openFileDialogSlate;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.GroupBox groupBoxEncoding;
        private System.Windows.Forms.RadioButton radioButtonCustomPreset;
        private System.Windows.Forms.TextBox textBoxCustomPreset;
        private System.Windows.Forms.RadioButton radioButtonDefaultPreset;
        private System.Windows.Forms.Label labelLiveEventMustBeStopped;
        private System.Windows.Forms.Label labelLiveEventStoppedOrStartedSettings;
        private System.Windows.Forms.TabPage tabPageEncoding;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panelDisplayEncProfile;
        private System.Windows.Forms.DataGridView dataGridViewVideoProf;
        private System.Windows.Forms.DataGridView dataGridViewAudioProf;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.CheckBox checkBoxIgnore708;
        private System.Windows.Forms.CheckBox checkBoxEncodingKeyFrameInterval;
        private System.Windows.Forms.TextBox textBoxEncodingKeyFrameInterval;
        private Microsoft.Web.WebView2.WinForms.WebView2 webBrowserPreview;
        private System.Windows.Forms.PictureBox pictureBoxLE;
        private System.Windows.Forms.TabPage tabPageLiveTranscript;
        private System.Windows.Forms.LinkLabel linkLabelLiveTranscriptRegions;
        private System.Windows.Forms.ComboBox comboBoxLanguage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel linkLabelLiveTranscript;
        private System.Windows.Forms.CheckBox checkBoxEnableLiveTranscript;
        private System.Windows.Forms.CheckBox checkBoxLowLatency;
        private System.Windows.Forms.TabPage tabPageAdvanced;
        private System.Windows.Forms.Panel panelAdvanced;
        private System.Windows.Forms.RadioButton radioButtonLowLatencyV2;
        private System.Windows.Forms.RadioButton radioButtonLowLatencyV1;
    }
}