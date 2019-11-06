namespace AMSExplorer
{
    partial class LiveEventCreation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LiveEventCreation));
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.textBoxRestrictIngestIP = new System.Windows.Forms.TextBox();
            this.checkBoxRestrictIngestIP = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textboxchannelname = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxProtocolInput = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.checkBoxStartChannel = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControlLiveChannel = new System.Windows.Forms.TabControl();
            this.TabSettings = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtonTranscodingPremium = new System.Windows.Forms.RadioButton();
            this.moreinfoLiveEncodingProfilelink = new System.Windows.Forms.LinkLabel();
            this.moreinfoLiveStreamingProfilelink = new System.Windows.Forms.LinkLabel();
            this.radioButtonTranscodingStd = new System.Windows.Forms.RadioButton();
            this.radioButtonTranscodingNone = new System.Windows.Forms.RadioButton();
            this.checkBoxRestrictPreviewIP = new System.Windows.Forms.CheckBox();
            this.textBoxRestrictPreviewIP = new System.Windows.Forms.TextBox();
            this.tabPageLiveEncoding = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.textBoxCustomPreset = new System.Windows.Forms.TextBox();
            this.panelPresetLiveEncoding = new System.Windows.Forms.TableLayoutPanel();
            this.dataGridViewAudioProf = new System.Windows.Forms.DataGridView();
            this.dataGridViewVideoProf = new System.Windows.Forms.DataGridView();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.radioButtonDefaultPreset = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.radioButtonCustomPreset = new System.Windows.Forms.RadioButton();
            this.tabPageAdv = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.labelUrlSyntax = new System.Windows.Forms.Label();
            this.buttonGenerateToken = new System.Windows.Forms.Button();
            this.checkBoxKeyFrameIntDefined = new System.Windows.Forms.CheckBox();
            this.textBoxKeyFrame = new System.Windows.Forms.TextBox();
            this.checkBoxLowLatency = new System.Windows.Forms.CheckBox();
            this.checkBoxVanityUrl = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxToken = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.labelWarning = new System.Windows.Forms.Label();
            this.linkLabelMoreInfoPrice = new System.Windows.Forms.LinkLabel();
            this.openFileDialogSlate = new System.Windows.Forms.OpenFileDialog();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.panel1.SuspendLayout();
            this.tabControlLiveChannel.SuspendLayout();
            this.TabSettings.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPageLiveEncoding.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panelPresetLiveEncoding.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAudioProf)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewVideoProf)).BeginInit();
            this.tabPageAdv.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            resources.ApplyResources(this.buttonOk, "buttonOk");
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.errorProvider1.SetError(this.buttonOk, resources.GetString("buttonOk.Error"));
            this.errorProvider1.SetIconAlignment(this.buttonOk, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("buttonOk.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.buttonOk, ((int)(resources.GetObject("buttonOk.IconPadding"))));
            this.buttonOk.Name = "buttonOk";
            this.toolTip1.SetToolTip(this.buttonOk, resources.GetString("buttonOk.ToolTip"));
            this.buttonOk.UseVisualStyleBackColor = true;
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
            // textBoxRestrictIngestIP
            // 
            resources.ApplyResources(this.textBoxRestrictIngestIP, "textBoxRestrictIngestIP");
            this.errorProvider1.SetError(this.textBoxRestrictIngestIP, resources.GetString("textBoxRestrictIngestIP.Error"));
            this.errorProvider1.SetIconAlignment(this.textBoxRestrictIngestIP, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("textBoxRestrictIngestIP.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.textBoxRestrictIngestIP, ((int)(resources.GetObject("textBoxRestrictIngestIP.IconPadding"))));
            this.textBoxRestrictIngestIP.Name = "textBoxRestrictIngestIP";
            this.toolTip1.SetToolTip(this.textBoxRestrictIngestIP, resources.GetString("textBoxRestrictIngestIP.ToolTip"));
            this.textBoxRestrictIngestIP.TextChanged += new System.EventHandler(this.textBoxIP_TextChanged);
            // 
            // checkBoxRestrictIngestIP
            // 
            resources.ApplyResources(this.checkBoxRestrictIngestIP, "checkBoxRestrictIngestIP");
            this.errorProvider1.SetError(this.checkBoxRestrictIngestIP, resources.GetString("checkBoxRestrictIngestIP.Error"));
            this.errorProvider1.SetIconAlignment(this.checkBoxRestrictIngestIP, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("checkBoxRestrictIngestIP.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.checkBoxRestrictIngestIP, ((int)(resources.GetObject("checkBoxRestrictIngestIP.IconPadding"))));
            this.checkBoxRestrictIngestIP.Name = "checkBoxRestrictIngestIP";
            this.toolTip1.SetToolTip(this.checkBoxRestrictIngestIP, resources.GetString("checkBoxRestrictIngestIP.ToolTip"));
            this.checkBoxRestrictIngestIP.UseVisualStyleBackColor = true;
            this.checkBoxRestrictIngestIP.CheckedChanged += new System.EventHandler(this.checkBoxRestrictIngestIP_CheckedChanged);
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
            // textboxchannelname
            // 
            resources.ApplyResources(this.textboxchannelname, "textboxchannelname");
            this.errorProvider1.SetError(this.textboxchannelname, resources.GetString("textboxchannelname.Error"));
            this.errorProvider1.SetIconAlignment(this.textboxchannelname, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("textboxchannelname.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.textboxchannelname, ((int)(resources.GetObject("textboxchannelname.IconPadding"))));
            this.textboxchannelname.Name = "textboxchannelname";
            this.toolTip1.SetToolTip(this.textboxchannelname, resources.GetString("textboxchannelname.ToolTip"));
            this.textboxchannelname.TextChanged += new System.EventHandler(this.textboxchannelname_TextChanged);
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
            // comboBoxProtocolInput
            // 
            resources.ApplyResources(this.comboBoxProtocolInput, "comboBoxProtocolInput");
            this.comboBoxProtocolInput.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.errorProvider1.SetError(this.comboBoxProtocolInput, resources.GetString("comboBoxProtocolInput.Error"));
            this.comboBoxProtocolInput.FormattingEnabled = true;
            this.errorProvider1.SetIconAlignment(this.comboBoxProtocolInput, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("comboBoxProtocolInput.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.comboBoxProtocolInput, ((int)(resources.GetObject("comboBoxProtocolInput.IconPadding"))));
            this.comboBoxProtocolInput.Name = "comboBoxProtocolInput";
            this.toolTip1.SetToolTip(this.comboBoxProtocolInput, resources.GetString("comboBoxProtocolInput.ToolTip"));
            this.comboBoxProtocolInput.SelectedIndexChanged += new System.EventHandler(this.comboBoxProtocolInput_SelectedIndexChanged);
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
            // textBoxDescription
            // 
            resources.ApplyResources(this.textBoxDescription, "textBoxDescription");
            this.errorProvider1.SetError(this.textBoxDescription, resources.GetString("textBoxDescription.Error"));
            this.errorProvider1.SetIconAlignment(this.textBoxDescription, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("textBoxDescription.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.textBoxDescription, ((int)(resources.GetObject("textBoxDescription.IconPadding"))));
            this.textBoxDescription.Name = "textBoxDescription";
            this.toolTip1.SetToolTip(this.textBoxDescription, resources.GetString("textBoxDescription.ToolTip"));
            // 
            // checkBoxStartChannel
            // 
            resources.ApplyResources(this.checkBoxStartChannel, "checkBoxStartChannel");
            this.errorProvider1.SetError(this.checkBoxStartChannel, resources.GetString("checkBoxStartChannel.Error"));
            this.errorProvider1.SetIconAlignment(this.checkBoxStartChannel, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("checkBoxStartChannel.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.checkBoxStartChannel, ((int)(resources.GetObject("checkBoxStartChannel.IconPadding"))));
            this.checkBoxStartChannel.Name = "checkBoxStartChannel";
            this.toolTip1.SetToolTip(this.checkBoxStartChannel, resources.GetString("checkBoxStartChannel.ToolTip"));
            this.checkBoxStartChannel.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonOk);
            this.errorProvider1.SetError(this.panel1, resources.GetString("panel1.Error"));
            this.errorProvider1.SetIconAlignment(this.panel1, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("panel1.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.panel1, ((int)(resources.GetObject("panel1.IconPadding"))));
            this.panel1.Name = "panel1";
            this.toolTip1.SetToolTip(this.panel1, resources.GetString("panel1.ToolTip"));
            // 
            // tabControlLiveChannel
            // 
            resources.ApplyResources(this.tabControlLiveChannel, "tabControlLiveChannel");
            this.tabControlLiveChannel.Controls.Add(this.TabSettings);
            this.tabControlLiveChannel.Controls.Add(this.tabPageLiveEncoding);
            this.tabControlLiveChannel.Controls.Add(this.tabPageAdv);
            this.errorProvider1.SetError(this.tabControlLiveChannel, resources.GetString("tabControlLiveChannel.Error"));
            this.errorProvider1.SetIconAlignment(this.tabControlLiveChannel, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("tabControlLiveChannel.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.tabControlLiveChannel, ((int)(resources.GetObject("tabControlLiveChannel.IconPadding"))));
            this.tabControlLiveChannel.Name = "tabControlLiveChannel";
            this.tabControlLiveChannel.SelectedIndex = 0;
            this.toolTip1.SetToolTip(this.tabControlLiveChannel, resources.GetString("tabControlLiveChannel.ToolTip"));
            // 
            // TabSettings
            // 
            resources.ApplyResources(this.TabSettings, "TabSettings");
            this.TabSettings.Controls.Add(this.groupBox1);
            this.TabSettings.Controls.Add(this.checkBoxRestrictPreviewIP);
            this.TabSettings.Controls.Add(this.textBoxRestrictPreviewIP);
            this.TabSettings.Controls.Add(this.label4);
            this.TabSettings.Controls.Add(this.comboBoxProtocolInput);
            this.TabSettings.Controls.Add(this.checkBoxRestrictIngestIP);
            this.TabSettings.Controls.Add(this.textBoxRestrictIngestIP);
            this.errorProvider1.SetError(this.TabSettings, resources.GetString("TabSettings.Error"));
            this.errorProvider1.SetIconAlignment(this.TabSettings, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("TabSettings.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.TabSettings, ((int)(resources.GetObject("TabSettings.IconPadding"))));
            this.TabSettings.Name = "TabSettings";
            this.toolTip1.SetToolTip(this.TabSettings, resources.GetString("TabSettings.ToolTip"));
            this.TabSettings.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.radioButtonTranscodingPremium);
            this.groupBox1.Controls.Add(this.moreinfoLiveEncodingProfilelink);
            this.groupBox1.Controls.Add(this.moreinfoLiveStreamingProfilelink);
            this.groupBox1.Controls.Add(this.radioButtonTranscodingStd);
            this.groupBox1.Controls.Add(this.radioButtonTranscodingNone);
            this.errorProvider1.SetError(this.groupBox1, resources.GetString("groupBox1.Error"));
            this.errorProvider1.SetIconAlignment(this.groupBox1, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("groupBox1.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.groupBox1, ((int)(resources.GetObject("groupBox1.IconPadding"))));
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            this.toolTip1.SetToolTip(this.groupBox1, resources.GetString("groupBox1.ToolTip"));
            // 
            // radioButtonTranscodingPremium
            // 
            resources.ApplyResources(this.radioButtonTranscodingPremium, "radioButtonTranscodingPremium");
            this.errorProvider1.SetError(this.radioButtonTranscodingPremium, resources.GetString("radioButtonTranscodingPremium.Error"));
            this.errorProvider1.SetIconAlignment(this.radioButtonTranscodingPremium, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("radioButtonTranscodingPremium.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.radioButtonTranscodingPremium, ((int)(resources.GetObject("radioButtonTranscodingPremium.IconPadding"))));
            this.radioButtonTranscodingPremium.Name = "radioButtonTranscodingPremium";
            this.toolTip1.SetToolTip(this.radioButtonTranscodingPremium, resources.GetString("radioButtonTranscodingPremium.ToolTip"));
            this.radioButtonTranscodingPremium.UseVisualStyleBackColor = true;
            this.radioButtonTranscodingPremium.CheckedChanged += new System.EventHandler(this.radioButtonTranscodingNone_CheckedChanged);
            // 
            // moreinfoLiveEncodingProfilelink
            // 
            resources.ApplyResources(this.moreinfoLiveEncodingProfilelink, "moreinfoLiveEncodingProfilelink");
            this.errorProvider1.SetError(this.moreinfoLiveEncodingProfilelink, resources.GetString("moreinfoLiveEncodingProfilelink.Error"));
            this.errorProvider1.SetIconAlignment(this.moreinfoLiveEncodingProfilelink, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("moreinfoLiveEncodingProfilelink.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.moreinfoLiveEncodingProfilelink, ((int)(resources.GetObject("moreinfoLiveEncodingProfilelink.IconPadding"))));
            this.moreinfoLiveEncodingProfilelink.Name = "moreinfoLiveEncodingProfilelink";
            this.moreinfoLiveEncodingProfilelink.TabStop = true;
            this.toolTip1.SetToolTip(this.moreinfoLiveEncodingProfilelink, resources.GetString("moreinfoLiveEncodingProfilelink.ToolTip"));
            this.moreinfoLiveEncodingProfilelink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.moreinfoLiveEncodingProfilelink_LinkClicked);
            // 
            // moreinfoLiveStreamingProfilelink
            // 
            resources.ApplyResources(this.moreinfoLiveStreamingProfilelink, "moreinfoLiveStreamingProfilelink");
            this.errorProvider1.SetError(this.moreinfoLiveStreamingProfilelink, resources.GetString("moreinfoLiveStreamingProfilelink.Error"));
            this.errorProvider1.SetIconAlignment(this.moreinfoLiveStreamingProfilelink, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("moreinfoLiveStreamingProfilelink.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.moreinfoLiveStreamingProfilelink, ((int)(resources.GetObject("moreinfoLiveStreamingProfilelink.IconPadding"))));
            this.moreinfoLiveStreamingProfilelink.Name = "moreinfoLiveStreamingProfilelink";
            this.moreinfoLiveStreamingProfilelink.TabStop = true;
            this.toolTip1.SetToolTip(this.moreinfoLiveStreamingProfilelink, resources.GetString("moreinfoLiveStreamingProfilelink.ToolTip"));
            this.moreinfoLiveStreamingProfilelink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.moreinfoLiveEncodingProfilelink_LinkClicked);
            // 
            // radioButtonTranscodingStd
            // 
            resources.ApplyResources(this.radioButtonTranscodingStd, "radioButtonTranscodingStd");
            this.errorProvider1.SetError(this.radioButtonTranscodingStd, resources.GetString("radioButtonTranscodingStd.Error"));
            this.errorProvider1.SetIconAlignment(this.radioButtonTranscodingStd, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("radioButtonTranscodingStd.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.radioButtonTranscodingStd, ((int)(resources.GetObject("radioButtonTranscodingStd.IconPadding"))));
            this.radioButtonTranscodingStd.Name = "radioButtonTranscodingStd";
            this.toolTip1.SetToolTip(this.radioButtonTranscodingStd, resources.GetString("radioButtonTranscodingStd.ToolTip"));
            this.radioButtonTranscodingStd.UseVisualStyleBackColor = true;
            this.radioButtonTranscodingStd.CheckedChanged += new System.EventHandler(this.radioButtonTranscodingNone_CheckedChanged);
            // 
            // radioButtonTranscodingNone
            // 
            resources.ApplyResources(this.radioButtonTranscodingNone, "radioButtonTranscodingNone");
            this.radioButtonTranscodingNone.Checked = true;
            this.errorProvider1.SetError(this.radioButtonTranscodingNone, resources.GetString("radioButtonTranscodingNone.Error"));
            this.errorProvider1.SetIconAlignment(this.radioButtonTranscodingNone, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("radioButtonTranscodingNone.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.radioButtonTranscodingNone, ((int)(resources.GetObject("radioButtonTranscodingNone.IconPadding"))));
            this.radioButtonTranscodingNone.Name = "radioButtonTranscodingNone";
            this.radioButtonTranscodingNone.TabStop = true;
            this.toolTip1.SetToolTip(this.radioButtonTranscodingNone, resources.GetString("radioButtonTranscodingNone.ToolTip"));
            this.radioButtonTranscodingNone.UseVisualStyleBackColor = true;
            this.radioButtonTranscodingNone.CheckedChanged += new System.EventHandler(this.radioButtonTranscodingNone_CheckedChanged);
            // 
            // checkBoxRestrictPreviewIP
            // 
            resources.ApplyResources(this.checkBoxRestrictPreviewIP, "checkBoxRestrictPreviewIP");
            this.errorProvider1.SetError(this.checkBoxRestrictPreviewIP, resources.GetString("checkBoxRestrictPreviewIP.Error"));
            this.errorProvider1.SetIconAlignment(this.checkBoxRestrictPreviewIP, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("checkBoxRestrictPreviewIP.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.checkBoxRestrictPreviewIP, ((int)(resources.GetObject("checkBoxRestrictPreviewIP.IconPadding"))));
            this.checkBoxRestrictPreviewIP.Name = "checkBoxRestrictPreviewIP";
            this.toolTip1.SetToolTip(this.checkBoxRestrictPreviewIP, resources.GetString("checkBoxRestrictPreviewIP.ToolTip"));
            this.checkBoxRestrictPreviewIP.UseVisualStyleBackColor = true;
            this.checkBoxRestrictPreviewIP.CheckedChanged += new System.EventHandler(this.checkBoxRestrictPreviewIP_CheckedChanged);
            // 
            // textBoxRestrictPreviewIP
            // 
            resources.ApplyResources(this.textBoxRestrictPreviewIP, "textBoxRestrictPreviewIP");
            this.errorProvider1.SetError(this.textBoxRestrictPreviewIP, resources.GetString("textBoxRestrictPreviewIP.Error"));
            this.errorProvider1.SetIconAlignment(this.textBoxRestrictPreviewIP, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("textBoxRestrictPreviewIP.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.textBoxRestrictPreviewIP, ((int)(resources.GetObject("textBoxRestrictPreviewIP.IconPadding"))));
            this.textBoxRestrictPreviewIP.Name = "textBoxRestrictPreviewIP";
            this.toolTip1.SetToolTip(this.textBoxRestrictPreviewIP, resources.GetString("textBoxRestrictPreviewIP.ToolTip"));
            this.textBoxRestrictPreviewIP.TextChanged += new System.EventHandler(this.textBoxIP_TextChanged);
            // 
            // tabPageLiveEncoding
            // 
            resources.ApplyResources(this.tabPageLiveEncoding, "tabPageLiveEncoding");
            this.tabPageLiveEncoding.Controls.Add(this.tableLayoutPanel1);
            this.errorProvider1.SetError(this.tabPageLiveEncoding, resources.GetString("tabPageLiveEncoding.Error"));
            this.errorProvider1.SetIconAlignment(this.tabPageLiveEncoding, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("tabPageLiveEncoding.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.tabPageLiveEncoding, ((int)(resources.GetObject("tabPageLiveEncoding.IconPadding"))));
            this.tabPageLiveEncoding.Name = "tabPageLiveEncoding";
            this.toolTip1.SetToolTip(this.tabPageLiveEncoding, resources.GetString("tabPageLiveEncoding.ToolTip"));
            this.tabPageLiveEncoding.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.textBoxCustomPreset, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panelPresetLiveEncoding, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.radioButtonDefaultPreset, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label6, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.radioButtonCustomPreset, 0, 0);
            this.errorProvider1.SetError(this.tableLayoutPanel1, resources.GetString("tableLayoutPanel1.Error"));
            this.errorProvider1.SetIconAlignment(this.tableLayoutPanel1, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("tableLayoutPanel1.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.tableLayoutPanel1, ((int)(resources.GetObject("tableLayoutPanel1.IconPadding"))));
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.toolTip1.SetToolTip(this.tableLayoutPanel1, resources.GetString("tableLayoutPanel1.ToolTip"));
            // 
            // textBoxCustomPreset
            // 
            resources.ApplyResources(this.textBoxCustomPreset, "textBoxCustomPreset");
            this.errorProvider1.SetError(this.textBoxCustomPreset, resources.GetString("textBoxCustomPreset.Error"));
            this.errorProvider1.SetIconAlignment(this.textBoxCustomPreset, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("textBoxCustomPreset.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.textBoxCustomPreset, ((int)(resources.GetObject("textBoxCustomPreset.IconPadding"))));
            this.textBoxCustomPreset.Name = "textBoxCustomPreset";
            this.toolTip1.SetToolTip(this.textBoxCustomPreset, resources.GetString("textBoxCustomPreset.ToolTip"));
            this.textBoxCustomPreset.TextChanged += new System.EventHandler(this.textBoxCustomPreset_TextChanged);
            // 
            // panelPresetLiveEncoding
            // 
            resources.ApplyResources(this.panelPresetLiveEncoding, "panelPresetLiveEncoding");
            this.panelPresetLiveEncoding.Controls.Add(this.dataGridViewAudioProf, 0, 3);
            this.panelPresetLiveEncoding.Controls.Add(this.dataGridViewVideoProf, 0, 1);
            this.panelPresetLiveEncoding.Controls.Add(this.label16, 0, 0);
            this.panelPresetLiveEncoding.Controls.Add(this.label17, 0, 2);
            this.errorProvider1.SetError(this.panelPresetLiveEncoding, resources.GetString("panelPresetLiveEncoding.Error"));
            this.errorProvider1.SetIconAlignment(this.panelPresetLiveEncoding, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("panelPresetLiveEncoding.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.panelPresetLiveEncoding, ((int)(resources.GetObject("panelPresetLiveEncoding.IconPadding"))));
            this.panelPresetLiveEncoding.Name = "panelPresetLiveEncoding";
            this.toolTip1.SetToolTip(this.panelPresetLiveEncoding, resources.GetString("panelPresetLiveEncoding.ToolTip"));
            // 
            // dataGridViewAudioProf
            // 
            resources.ApplyResources(this.dataGridViewAudioProf, "dataGridViewAudioProf");
            this.dataGridViewAudioProf.AllowUserToAddRows = false;
            this.dataGridViewAudioProf.AllowUserToDeleteRows = false;
            this.dataGridViewAudioProf.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewAudioProf.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridViewAudioProf.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.errorProvider1.SetError(this.dataGridViewAudioProf, resources.GetString("dataGridViewAudioProf.Error"));
            this.errorProvider1.SetIconAlignment(this.dataGridViewAudioProf, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("dataGridViewAudioProf.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.dataGridViewAudioProf, ((int)(resources.GetObject("dataGridViewAudioProf.IconPadding"))));
            this.dataGridViewAudioProf.Name = "dataGridViewAudioProf";
            this.dataGridViewAudioProf.ReadOnly = true;
            this.dataGridViewAudioProf.RowHeadersVisible = false;
            this.toolTip1.SetToolTip(this.dataGridViewAudioProf, resources.GetString("dataGridViewAudioProf.ToolTip"));
            // 
            // dataGridViewVideoProf
            // 
            resources.ApplyResources(this.dataGridViewVideoProf, "dataGridViewVideoProf");
            this.dataGridViewVideoProf.AllowUserToAddRows = false;
            this.dataGridViewVideoProf.AllowUserToDeleteRows = false;
            this.dataGridViewVideoProf.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewVideoProf.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridViewVideoProf.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.errorProvider1.SetError(this.dataGridViewVideoProf, resources.GetString("dataGridViewVideoProf.Error"));
            this.errorProvider1.SetIconAlignment(this.dataGridViewVideoProf, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("dataGridViewVideoProf.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.dataGridViewVideoProf, ((int)(resources.GetObject("dataGridViewVideoProf.IconPadding"))));
            this.dataGridViewVideoProf.Name = "dataGridViewVideoProf";
            this.dataGridViewVideoProf.ReadOnly = true;
            this.dataGridViewVideoProf.RowHeadersVisible = false;
            this.toolTip1.SetToolTip(this.dataGridViewVideoProf, resources.GetString("dataGridViewVideoProf.ToolTip"));
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.errorProvider1.SetError(this.label16, resources.GetString("label16.Error"));
            this.errorProvider1.SetIconAlignment(this.label16, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label16.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label16, ((int)(resources.GetObject("label16.IconPadding"))));
            this.label16.Name = "label16";
            this.toolTip1.SetToolTip(this.label16, resources.GetString("label16.ToolTip"));
            // 
            // label17
            // 
            resources.ApplyResources(this.label17, "label17");
            this.errorProvider1.SetError(this.label17, resources.GetString("label17.Error"));
            this.errorProvider1.SetIconAlignment(this.label17, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label17.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label17, ((int)(resources.GetObject("label17.IconPadding"))));
            this.label17.Name = "label17";
            this.toolTip1.SetToolTip(this.label17, resources.GetString("label17.ToolTip"));
            // 
            // radioButtonDefaultPreset
            // 
            resources.ApplyResources(this.radioButtonDefaultPreset, "radioButtonDefaultPreset");
            this.radioButtonDefaultPreset.Checked = true;
            this.errorProvider1.SetError(this.radioButtonDefaultPreset, resources.GetString("radioButtonDefaultPreset.Error"));
            this.errorProvider1.SetIconAlignment(this.radioButtonDefaultPreset, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("radioButtonDefaultPreset.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.radioButtonDefaultPreset, ((int)(resources.GetObject("radioButtonDefaultPreset.IconPadding"))));
            this.radioButtonDefaultPreset.Name = "radioButtonDefaultPreset";
            this.radioButtonDefaultPreset.TabStop = true;
            this.toolTip1.SetToolTip(this.radioButtonDefaultPreset, resources.GetString("radioButtonDefaultPreset.ToolTip"));
            this.radioButtonDefaultPreset.UseVisualStyleBackColor = true;
            this.radioButtonDefaultPreset.CheckedChanged += new System.EventHandler(this.radioButtonDefaultPreset_CheckedChanged);
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.errorProvider1.SetError(this.label6, resources.GetString("label6.Error"));
            this.label6.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.errorProvider1.SetIconAlignment(this.label6, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label6.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label6, ((int)(resources.GetObject("label6.IconPadding"))));
            this.label6.Name = "label6";
            this.toolTip1.SetToolTip(this.label6, resources.GetString("label6.ToolTip"));
            // 
            // radioButtonCustomPreset
            // 
            resources.ApplyResources(this.radioButtonCustomPreset, "radioButtonCustomPreset");
            this.errorProvider1.SetError(this.radioButtonCustomPreset, resources.GetString("radioButtonCustomPreset.Error"));
            this.errorProvider1.SetIconAlignment(this.radioButtonCustomPreset, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("radioButtonCustomPreset.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.radioButtonCustomPreset, ((int)(resources.GetObject("radioButtonCustomPreset.IconPadding"))));
            this.radioButtonCustomPreset.Name = "radioButtonCustomPreset";
            this.toolTip1.SetToolTip(this.radioButtonCustomPreset, resources.GetString("radioButtonCustomPreset.ToolTip"));
            this.radioButtonCustomPreset.UseVisualStyleBackColor = true;
            this.radioButtonCustomPreset.CheckedChanged += new System.EventHandler(this.radioButtonCustomPreset_CheckedChanged);
            // 
            // tabPageAdv
            // 
            resources.ApplyResources(this.tabPageAdv, "tabPageAdv");
            this.tabPageAdv.Controls.Add(this.label1);
            this.tabPageAdv.Controls.Add(this.labelUrlSyntax);
            this.tabPageAdv.Controls.Add(this.buttonGenerateToken);
            this.tabPageAdv.Controls.Add(this.checkBoxKeyFrameIntDefined);
            this.tabPageAdv.Controls.Add(this.textBoxKeyFrame);
            this.tabPageAdv.Controls.Add(this.checkBoxLowLatency);
            this.tabPageAdv.Controls.Add(this.checkBoxVanityUrl);
            this.tabPageAdv.Controls.Add(this.label7);
            this.tabPageAdv.Controls.Add(this.textBoxToken);
            this.errorProvider1.SetError(this.tabPageAdv, resources.GetString("tabPageAdv.Error"));
            this.errorProvider1.SetIconAlignment(this.tabPageAdv, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("tabPageAdv.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.tabPageAdv, ((int)(resources.GetObject("tabPageAdv.IconPadding"))));
            this.tabPageAdv.Name = "tabPageAdv";
            this.toolTip1.SetToolTip(this.tabPageAdv, resources.GetString("tabPageAdv.ToolTip"));
            this.tabPageAdv.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.errorProvider1.SetError(this.label1, resources.GetString("label1.Error"));
            this.label1.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.errorProvider1.SetIconAlignment(this.label1, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label1.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label1, ((int)(resources.GetObject("label1.IconPadding"))));
            this.label1.Name = "label1";
            this.toolTip1.SetToolTip(this.label1, resources.GetString("label1.ToolTip"));
            // 
            // labelUrlSyntax
            // 
            resources.ApplyResources(this.labelUrlSyntax, "labelUrlSyntax");
            this.errorProvider1.SetError(this.labelUrlSyntax, resources.GetString("labelUrlSyntax.Error"));
            this.labelUrlSyntax.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.errorProvider1.SetIconAlignment(this.labelUrlSyntax, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("labelUrlSyntax.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.labelUrlSyntax, ((int)(resources.GetObject("labelUrlSyntax.IconPadding"))));
            this.labelUrlSyntax.Name = "labelUrlSyntax";
            this.toolTip1.SetToolTip(this.labelUrlSyntax, resources.GetString("labelUrlSyntax.ToolTip"));
            // 
            // buttonGenerateToken
            // 
            resources.ApplyResources(this.buttonGenerateToken, "buttonGenerateToken");
            this.errorProvider1.SetError(this.buttonGenerateToken, resources.GetString("buttonGenerateToken.Error"));
            this.errorProvider1.SetIconAlignment(this.buttonGenerateToken, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("buttonGenerateToken.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.buttonGenerateToken, ((int)(resources.GetObject("buttonGenerateToken.IconPadding"))));
            this.buttonGenerateToken.Name = "buttonGenerateToken";
            this.toolTip1.SetToolTip(this.buttonGenerateToken, resources.GetString("buttonGenerateToken.ToolTip"));
            this.buttonGenerateToken.UseVisualStyleBackColor = true;
            this.buttonGenerateToken.Click += new System.EventHandler(this.buttonGenerateToken_Click);
            // 
            // checkBoxKeyFrameIntDefined
            // 
            resources.ApplyResources(this.checkBoxKeyFrameIntDefined, "checkBoxKeyFrameIntDefined");
            this.errorProvider1.SetError(this.checkBoxKeyFrameIntDefined, resources.GetString("checkBoxKeyFrameIntDefined.Error"));
            this.errorProvider1.SetIconAlignment(this.checkBoxKeyFrameIntDefined, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("checkBoxKeyFrameIntDefined.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.checkBoxKeyFrameIntDefined, ((int)(resources.GetObject("checkBoxKeyFrameIntDefined.IconPadding"))));
            this.checkBoxKeyFrameIntDefined.Name = "checkBoxKeyFrameIntDefined";
            this.toolTip1.SetToolTip(this.checkBoxKeyFrameIntDefined, resources.GetString("checkBoxKeyFrameIntDefined.ToolTip"));
            this.checkBoxKeyFrameIntDefined.UseVisualStyleBackColor = true;
            this.checkBoxKeyFrameIntDefined.CheckedChanged += new System.EventHandler(this.checkBoxKeyFrameIntDefined_CheckedChanged);
            // 
            // textBoxKeyFrame
            // 
            resources.ApplyResources(this.textBoxKeyFrame, "textBoxKeyFrame");
            this.errorProvider1.SetError(this.textBoxKeyFrame, resources.GetString("textBoxKeyFrame.Error"));
            this.errorProvider1.SetIconAlignment(this.textBoxKeyFrame, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("textBoxKeyFrame.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.textBoxKeyFrame, ((int)(resources.GetObject("textBoxKeyFrame.IconPadding"))));
            this.textBoxKeyFrame.Name = "textBoxKeyFrame";
            this.toolTip1.SetToolTip(this.textBoxKeyFrame, resources.GetString("textBoxKeyFrame.ToolTip"));
            // 
            // checkBoxLowLatency
            // 
            resources.ApplyResources(this.checkBoxLowLatency, "checkBoxLowLatency");
            this.errorProvider1.SetError(this.checkBoxLowLatency, resources.GetString("checkBoxLowLatency.Error"));
            this.errorProvider1.SetIconAlignment(this.checkBoxLowLatency, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("checkBoxLowLatency.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.checkBoxLowLatency, ((int)(resources.GetObject("checkBoxLowLatency.IconPadding"))));
            this.checkBoxLowLatency.Name = "checkBoxLowLatency";
            this.toolTip1.SetToolTip(this.checkBoxLowLatency, resources.GetString("checkBoxLowLatency.ToolTip"));
            this.checkBoxLowLatency.UseVisualStyleBackColor = true;
            // 
            // checkBoxVanityUrl
            // 
            resources.ApplyResources(this.checkBoxVanityUrl, "checkBoxVanityUrl");
            this.errorProvider1.SetError(this.checkBoxVanityUrl, resources.GetString("checkBoxVanityUrl.Error"));
            this.errorProvider1.SetIconAlignment(this.checkBoxVanityUrl, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("checkBoxVanityUrl.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.checkBoxVanityUrl, ((int)(resources.GetObject("checkBoxVanityUrl.IconPadding"))));
            this.checkBoxVanityUrl.Name = "checkBoxVanityUrl";
            this.toolTip1.SetToolTip(this.checkBoxVanityUrl, resources.GetString("checkBoxVanityUrl.ToolTip"));
            this.checkBoxVanityUrl.UseVisualStyleBackColor = true;
            this.checkBoxVanityUrl.CheckedChanged += new System.EventHandler(this.checkBoxVanityUrl_CheckedChanged);
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.errorProvider1.SetError(this.label7, resources.GetString("label7.Error"));
            this.errorProvider1.SetIconAlignment(this.label7, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label7.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label7, ((int)(resources.GetObject("label7.IconPadding"))));
            this.label7.Name = "label7";
            this.toolTip1.SetToolTip(this.label7, resources.GetString("label7.ToolTip"));
            // 
            // textBoxToken
            // 
            resources.ApplyResources(this.textBoxToken, "textBoxToken");
            this.errorProvider1.SetError(this.textBoxToken, resources.GetString("textBoxToken.Error"));
            this.errorProvider1.SetIconAlignment(this.textBoxToken, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("textBoxToken.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.textBoxToken, ((int)(resources.GetObject("textBoxToken.IconPadding"))));
            this.textBoxToken.Name = "textBoxToken";
            this.toolTip1.SetToolTip(this.textBoxToken, resources.GetString("textBoxToken.ToolTip"));
            this.textBoxToken.TextChanged += new System.EventHandler(this.textBoxToken_TextChanged);
            // 
            // labelWarning
            // 
            resources.ApplyResources(this.labelWarning, "labelWarning");
            this.errorProvider1.SetError(this.labelWarning, resources.GetString("labelWarning.Error"));
            this.labelWarning.ForeColor = System.Drawing.Color.Red;
            this.errorProvider1.SetIconAlignment(this.labelWarning, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("labelWarning.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.labelWarning, ((int)(resources.GetObject("labelWarning.IconPadding"))));
            this.labelWarning.Name = "labelWarning";
            this.toolTip1.SetToolTip(this.labelWarning, resources.GetString("labelWarning.ToolTip"));
            // 
            // linkLabelMoreInfoPrice
            // 
            resources.ApplyResources(this.linkLabelMoreInfoPrice, "linkLabelMoreInfoPrice");
            this.errorProvider1.SetError(this.linkLabelMoreInfoPrice, resources.GetString("linkLabelMoreInfoPrice.Error"));
            this.errorProvider1.SetIconAlignment(this.linkLabelMoreInfoPrice, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("linkLabelMoreInfoPrice.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.linkLabelMoreInfoPrice, ((int)(resources.GetObject("linkLabelMoreInfoPrice.IconPadding"))));
            this.linkLabelMoreInfoPrice.Name = "linkLabelMoreInfoPrice";
            this.linkLabelMoreInfoPrice.TabStop = true;
            this.toolTip1.SetToolTip(this.linkLabelMoreInfoPrice, resources.GetString("linkLabelMoreInfoPrice.ToolTip"));
            this.linkLabelMoreInfoPrice.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.moreinfoLiveEncodingProfilelink_LinkClicked);
            // 
            // openFileDialogSlate
            // 
            resources.ApplyResources(this.openFileDialogSlate, "openFileDialogSlate");
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            resources.ApplyResources(this.errorProvider1, "errorProvider1");
            // 
            // LiveEventCreation
            // 
            this.AcceptButton = this.buttonOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.linkLabelMoreInfoPrice);
            this.Controls.Add(this.labelWarning);
            this.Controls.Add(this.tabControlLiveChannel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.checkBoxStartChannel);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxDescription);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textboxchannelname);
            this.Name = "LiveEventCreation";
            this.toolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.Load += new System.EventHandler(this.CreateLiveChannel_Load);
            this.panel1.ResumeLayout(false);
            this.tabControlLiveChannel.ResumeLayout(false);
            this.TabSettings.ResumeLayout(false);
            this.TabSettings.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPageLiveEncoding.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panelPresetLiveEncoding.ResumeLayout(false);
            this.panelPresetLiveEncoding.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAudioProf)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewVideoProf)).EndInit();
            this.tabPageAdv.ResumeLayout(false);
            this.tabPageAdv.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button buttonOk;
        public System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textboxchannelname;
        private System.Windows.Forms.TextBox textBoxRestrictIngestIP;
        private System.Windows.Forms.CheckBox checkBoxRestrictIngestIP;
        private System.Windows.Forms.ComboBox comboBoxProtocolInput;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.CheckBox checkBoxStartChannel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControlLiveChannel;
        private System.Windows.Forms.TabPage TabSettings;
        private System.Windows.Forms.TabPage tabPageLiveEncoding;
        private System.Windows.Forms.CheckBox checkBoxRestrictPreviewIP;
        private System.Windows.Forms.TextBox textBoxRestrictPreviewIP;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.OpenFileDialog openFileDialogSlate;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.DataGridView dataGridViewVideoProf;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.DataGridView dataGridViewAudioProf;
        private System.Windows.Forms.TextBox textBoxCustomPreset;
        private System.Windows.Forms.RadioButton radioButtonCustomPreset;
        private System.Windows.Forms.RadioButton radioButtonDefaultPreset;
        private System.Windows.Forms.LinkLabel moreinfoLiveEncodingProfilelink;
        private System.Windows.Forms.LinkLabel moreinfoLiveStreamingProfilelink;
        private System.Windows.Forms.Label labelWarning;
        private System.Windows.Forms.LinkLabel linkLabelMoreInfoPrice;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TabPage tabPageAdv;
        private System.Windows.Forms.CheckBox checkBoxKeyFrameIntDefined;
        private System.Windows.Forms.TextBox textBoxKeyFrame;
        private System.Windows.Forms.CheckBox checkBoxLowLatency;
        private System.Windows.Forms.CheckBox checkBoxVanityUrl;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxToken;
        private System.Windows.Forms.Button buttonGenerateToken;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButtonTranscodingPremium;
        private System.Windows.Forms.RadioButton radioButtonTranscodingStd;
        private System.Windows.Forms.RadioButton radioButtonTranscodingNone;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelUrlSyntax;
        private System.Windows.Forms.TableLayoutPanel panelPresetLiveEncoding;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}