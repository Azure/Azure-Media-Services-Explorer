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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageLiveEventInfo = new System.Windows.Forms.TabPage();
            this.tabPageSettings = new System.Windows.Forms.TabPage();
            this.buttonAllowAllPreviewIP = new System.Windows.Forms.Button();
            this.buttonAllowAllInputIP = new System.Windows.Forms.Button();
            this.checkBoxKeyFrameIntDefined = new System.Windows.Forms.CheckBox();
            this.textBoxKeyFrame = new System.Windows.Forms.TextBox();
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
            this.tabPagePreview = new System.Windows.Forms.TabPage();
            this.webBrowserPreview = new System.Windows.Forms.WebBrowser();
            this.labelLEName = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonUpdateClose = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelLiveEventStoppedOrStartedSettings = new System.Windows.Forms.Label();
            this.openFileDialogSlate = new System.Windows.Forms.OpenFileDialog();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.DGLiveEvent)).BeginInit();
            this.contextMenuStripDG.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageLiveEventInfo.SuspendLayout();
            this.tabPageSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInputIP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPreviewIP)).BeginInit();
            this.tabPageEncoding.SuspendLayout();
            this.groupBoxEncoding.SuspendLayout();
            this.panelDisplayEncProfile.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewVideoProf)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAudioProf)).BeginInit();
            this.tabPagePolicies.SuspendLayout();
            this.tabPagePreview.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
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
            // tabControl1
            // 
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Controls.Add(this.tabPageLiveEventInfo);
            this.tabControl1.Controls.Add(this.tabPageSettings);
            this.tabControl1.Controls.Add(this.tabPageEncoding);
            this.tabControl1.Controls.Add(this.tabPagePolicies);
            this.tabControl1.Controls.Add(this.tabPagePreview);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
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
            this.tabPageSettings.Controls.Add(this.checkBoxKeyFrameIntDefined);
            this.tabPageSettings.Controls.Add(this.textBoxKeyFrame);
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
            // checkBoxKeyFrameIntDefined
            // 
            resources.ApplyResources(this.checkBoxKeyFrameIntDefined, "checkBoxKeyFrameIntDefined");
            this.checkBoxKeyFrameIntDefined.Name = "checkBoxKeyFrameIntDefined";
            this.checkBoxKeyFrameIntDefined.UseVisualStyleBackColor = true;
            this.checkBoxKeyFrameIntDefined.CheckedChanged += new System.EventHandler(this.checkBoxKeyFrameIntDefined_CheckedChanged);
            // 
            // textBoxKeyFrame
            // 
            resources.ApplyResources(this.textBoxKeyFrame, "textBoxKeyFrame");
            this.textBoxKeyFrame.Name = "textBoxKeyFrame";
            this.textBoxKeyFrame.TextChanged += new System.EventHandler(this.textBoxKeyFrame_TextChanged);
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
            resources.ApplyResources(this.webBrowserPreview, "webBrowserPreview");
            this.webBrowserPreview.Name = "webBrowserPreview";
            this.webBrowserPreview.ScriptErrorsSuppressed = true;
            this.webBrowserPreview.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowserPreview_DocumentCompleted);
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
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.labelLiveEventStoppedOrStartedSettings);
            this.panel1.Controls.Add(this.buttonClose);
            this.panel1.Controls.Add(this.buttonUpdateClose);
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
            // panel2
            // 
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Name = "panel2";
            // 
            // LiveEventInformation
            // 
            this.AcceptButton = this.buttonClose;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonClose;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labelLEName);
            this.Controls.Add(this.tabControl1);
            this.Name = "LiveEventInformation";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LiveEventInformation_FormClosed);
            this.Load += new System.EventHandler(this.LiveEventInformation_Load);
            this.Shown += new System.EventHandler(this.LiveEventInformation_Shown);
            this.DpiChanged += new System.Windows.Forms.DpiChangedEventHandler(this.LiveEventInformation_DpiChanged);
            ((System.ComponentModel.ISupportInitialize)(this.DGLiveEvent)).EndInit();
            this.contextMenuStripDG.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPageLiveEventInfo.ResumeLayout(false);
            this.tabPageSettings.ResumeLayout(false);
            this.tabPageSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInputIP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPreviewIP)).EndInit();
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
            this.tabPagePreview.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView DGLiveEvent;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripDG;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFilesCopyClipboard;
        private System.Windows.Forms.TabControl tabControl1;
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
        private System.Windows.Forms.WebBrowser webBrowserPreview;
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
        private System.Windows.Forms.Panel panel2;
    }
}