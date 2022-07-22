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
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.radioButtonPassThroughBasic = new System.Windows.Forms.RadioButton();
            this.radioButtonTranscodingPremium = new System.Windows.Forms.RadioButton();
            this.moreinfoLiveEventTypes = new System.Windows.Forms.LinkLabel();
            this.radioButtonTranscodingStd = new System.Windows.Forms.RadioButton();
            this.radioButtonPassThroughStandard = new System.Windows.Forms.RadioButton();
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
            this.tabPageLiveTranscript = new System.Windows.Forms.TabPage();
            this.linkLabelLiveTranscriptRegions = new System.Windows.Forms.LinkLabel();
            this.comboBoxLanguage = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.linkLabelLiveTranscript = new System.Windows.Forms.LinkLabel();
            this.checkBoxEnableLiveTranscript = new System.Windows.Forms.CheckBox();
            this.tabPageAdv = new System.Windows.Forms.TabPage();
            this.radioButtonLowLatencyV2 = new System.Windows.Forms.RadioButton();
            this.radioButtonLowLatencyV1 = new System.Windows.Forms.RadioButton();
            this.labelStaticHostnamePrefix = new System.Windows.Forms.Label();
            this.textBoxStaticHostname = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.labelUrlSyntax = new System.Windows.Forms.Label();
            this.buttonGenerateToken = new System.Windows.Forms.Button();
            this.checkBoxKeyFrameIntDefined = new System.Windows.Forms.CheckBox();
            this.textBoxInputKeyFrame = new System.Windows.Forms.TextBox();
            this.checkBoxLowLatency = new System.Windows.Forms.CheckBox();
            this.checkBoxVanityUrl = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxInputId = new System.Windows.Forms.TextBox();
            this.tabPageAdvEncoding = new System.Windows.Forms.TabPage();
            this.checkBoxEncodingKeyFrameInterval = new System.Windows.Forms.CheckBox();
            this.textBoxEncodingKeyFrameInterval = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.labelWarning = new System.Windows.Forms.Label();
            this.linkLabelMoreInfoPrice = new System.Windows.Forms.LinkLabel();
            this.openFileDialogSlate = new System.Windows.Forms.OpenFileDialog();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.panel1.SuspendLayout();
            this.tabControlLiveChannel.SuspendLayout();
            this.TabSettings.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.tabPageLiveEncoding.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panelPresetLiveEncoding.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAudioProf)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewVideoProf)).BeginInit();
            this.tabPageLiveTranscript.SuspendLayout();
            this.tabPageAdv.SuspendLayout();
            this.tabPageAdvEncoding.SuspendLayout();
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
            this.textBoxRestrictIngestIP.TextChanged += new System.EventHandler(this.TextBoxRestrictIngestIP_TextChanged);
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
            this.checkBoxRestrictIngestIP.CheckedChanged += new System.EventHandler(this.CheckBoxRestrictIngestIP_CheckedChanged);
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
            this.textboxchannelname.TextChanged += new System.EventHandler(this.Textboxchannelname_TextChanged);
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
            this.comboBoxProtocolInput.SelectedIndexChanged += new System.EventHandler(this.ComboBoxProtocolInput_SelectedIndexChanged);
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
            this.tabControlLiveChannel.Controls.Add(this.tabPageLiveTranscript);
            this.tabControlLiveChannel.Controls.Add(this.tabPageAdv);
            this.tabControlLiveChannel.Controls.Add(this.tabPageAdvEncoding);
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
            this.groupBox1.Controls.Add(this.pictureBox4);
            this.groupBox1.Controls.Add(this.pictureBox3);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.pictureBox2);
            this.groupBox1.Controls.Add(this.radioButtonPassThroughBasic);
            this.groupBox1.Controls.Add(this.radioButtonTranscodingPremium);
            this.groupBox1.Controls.Add(this.moreinfoLiveEventTypes);
            this.groupBox1.Controls.Add(this.radioButtonTranscodingStd);
            this.groupBox1.Controls.Add(this.radioButtonPassThroughStandard);
            this.errorProvider1.SetError(this.groupBox1, resources.GetString("groupBox1.Error"));
            this.errorProvider1.SetIconAlignment(this.groupBox1, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("groupBox1.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.groupBox1, ((int)(resources.GetObject("groupBox1.IconPadding"))));
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            this.toolTip1.SetToolTip(this.groupBox1, resources.GetString("groupBox1.ToolTip"));
            // 
            // pictureBox4
            // 
            resources.ApplyResources(this.pictureBox4, "pictureBox4");
            this.errorProvider1.SetError(this.pictureBox4, resources.GetString("pictureBox4.Error"));
            this.errorProvider1.SetIconAlignment(this.pictureBox4, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("pictureBox4.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.pictureBox4, ((int)(resources.GetObject("pictureBox4.IconPadding"))));
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBox4, resources.GetString("pictureBox4.ToolTip"));
            // 
            // pictureBox3
            // 
            resources.ApplyResources(this.pictureBox3, "pictureBox3");
            this.errorProvider1.SetError(this.pictureBox3, resources.GetString("pictureBox3.Error"));
            this.errorProvider1.SetIconAlignment(this.pictureBox3, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("pictureBox3.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.pictureBox3, ((int)(resources.GetObject("pictureBox3.IconPadding"))));
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBox3, resources.GetString("pictureBox3.ToolTip"));
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.errorProvider1.SetError(this.pictureBox1, resources.GetString("pictureBox1.Error"));
            this.errorProvider1.SetIconAlignment(this.pictureBox1, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("pictureBox1.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.pictureBox1, ((int)(resources.GetObject("pictureBox1.IconPadding"))));
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBox1, resources.GetString("pictureBox1.ToolTip"));
            // 
            // pictureBox2
            // 
            resources.ApplyResources(this.pictureBox2, "pictureBox2");
            this.errorProvider1.SetError(this.pictureBox2, resources.GetString("pictureBox2.Error"));
            this.errorProvider1.SetIconAlignment(this.pictureBox2, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("pictureBox2.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.pictureBox2, ((int)(resources.GetObject("pictureBox2.IconPadding"))));
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBox2, resources.GetString("pictureBox2.ToolTip"));
            // 
            // radioButtonPassThroughBasic
            // 
            resources.ApplyResources(this.radioButtonPassThroughBasic, "radioButtonPassThroughBasic");
            this.errorProvider1.SetError(this.radioButtonPassThroughBasic, resources.GetString("radioButtonPassThroughBasic.Error"));
            this.errorProvider1.SetIconAlignment(this.radioButtonPassThroughBasic, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("radioButtonPassThroughBasic.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.radioButtonPassThroughBasic, ((int)(resources.GetObject("radioButtonPassThroughBasic.IconPadding"))));
            this.radioButtonPassThroughBasic.Name = "radioButtonPassThroughBasic";
            this.toolTip1.SetToolTip(this.radioButtonPassThroughBasic, resources.GetString("radioButtonPassThroughBasic.ToolTip"));
            this.radioButtonPassThroughBasic.UseVisualStyleBackColor = true;
            this.radioButtonPassThroughBasic.CheckedChanged += new System.EventHandler(this.RadioButtonTranscodingNone_CheckedChanged);
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
            this.radioButtonTranscodingPremium.CheckedChanged += new System.EventHandler(this.RadioButtonTranscodingNone_CheckedChanged);
            // 
            // moreinfoLiveEventTypes
            // 
            resources.ApplyResources(this.moreinfoLiveEventTypes, "moreinfoLiveEventTypes");
            this.errorProvider1.SetError(this.moreinfoLiveEventTypes, resources.GetString("moreinfoLiveEventTypes.Error"));
            this.errorProvider1.SetIconAlignment(this.moreinfoLiveEventTypes, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("moreinfoLiveEventTypes.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.moreinfoLiveEventTypes, ((int)(resources.GetObject("moreinfoLiveEventTypes.IconPadding"))));
            this.moreinfoLiveEventTypes.Name = "moreinfoLiveEventTypes";
            this.moreinfoLiveEventTypes.TabStop = true;
            this.toolTip1.SetToolTip(this.moreinfoLiveEventTypes, resources.GetString("moreinfoLiveEventTypes.ToolTip"));
            this.moreinfoLiveEventTypes.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.MoreinfoLiveEncodingProfilelink_LinkClicked);
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
            this.radioButtonTranscodingStd.CheckedChanged += new System.EventHandler(this.RadioButtonTranscodingNone_CheckedChanged);
            // 
            // radioButtonPassThroughStandard
            // 
            resources.ApplyResources(this.radioButtonPassThroughStandard, "radioButtonPassThroughStandard");
            this.radioButtonPassThroughStandard.Checked = true;
            this.errorProvider1.SetError(this.radioButtonPassThroughStandard, resources.GetString("radioButtonPassThroughStandard.Error"));
            this.errorProvider1.SetIconAlignment(this.radioButtonPassThroughStandard, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("radioButtonPassThroughStandard.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.radioButtonPassThroughStandard, ((int)(resources.GetObject("radioButtonPassThroughStandard.IconPadding"))));
            this.radioButtonPassThroughStandard.Name = "radioButtonPassThroughStandard";
            this.radioButtonPassThroughStandard.TabStop = true;
            this.toolTip1.SetToolTip(this.radioButtonPassThroughStandard, resources.GetString("radioButtonPassThroughStandard.ToolTip"));
            this.radioButtonPassThroughStandard.UseVisualStyleBackColor = true;
            this.radioButtonPassThroughStandard.CheckedChanged += new System.EventHandler(this.RadioButtonTranscodingNone_CheckedChanged);
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
            this.checkBoxRestrictPreviewIP.CheckedChanged += new System.EventHandler(this.CheckBoxRestrictPreviewIP_CheckedChanged);
            // 
            // textBoxRestrictPreviewIP
            // 
            resources.ApplyResources(this.textBoxRestrictPreviewIP, "textBoxRestrictPreviewIP");
            this.errorProvider1.SetError(this.textBoxRestrictPreviewIP, resources.GetString("textBoxRestrictPreviewIP.Error"));
            this.errorProvider1.SetIconAlignment(this.textBoxRestrictPreviewIP, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("textBoxRestrictPreviewIP.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.textBoxRestrictPreviewIP, ((int)(resources.GetObject("textBoxRestrictPreviewIP.IconPadding"))));
            this.textBoxRestrictPreviewIP.Name = "textBoxRestrictPreviewIP";
            this.toolTip1.SetToolTip(this.textBoxRestrictPreviewIP, resources.GetString("textBoxRestrictPreviewIP.ToolTip"));
            this.textBoxRestrictPreviewIP.TextChanged += new System.EventHandler(this.TextBoxRestrictPreviewIP_TextChanged);
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
            this.textBoxCustomPreset.TextChanged += new System.EventHandler(this.TextBoxCustomPreset_TextChanged);
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
            this.radioButtonDefaultPreset.CheckedChanged += new System.EventHandler(this.RadioButtonDefaultPreset_CheckedChanged);
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
            this.radioButtonCustomPreset.CheckedChanged += new System.EventHandler(this.RadioButtonCustomPreset_CheckedChanged);
            // 
            // tabPageLiveTranscript
            // 
            resources.ApplyResources(this.tabPageLiveTranscript, "tabPageLiveTranscript");
            this.tabPageLiveTranscript.Controls.Add(this.linkLabelLiveTranscriptRegions);
            this.tabPageLiveTranscript.Controls.Add(this.comboBoxLanguage);
            this.tabPageLiveTranscript.Controls.Add(this.label2);
            this.tabPageLiveTranscript.Controls.Add(this.linkLabelLiveTranscript);
            this.tabPageLiveTranscript.Controls.Add(this.checkBoxEnableLiveTranscript);
            this.errorProvider1.SetError(this.tabPageLiveTranscript, resources.GetString("tabPageLiveTranscript.Error"));
            this.errorProvider1.SetIconAlignment(this.tabPageLiveTranscript, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("tabPageLiveTranscript.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.tabPageLiveTranscript, ((int)(resources.GetObject("tabPageLiveTranscript.IconPadding"))));
            this.tabPageLiveTranscript.Name = "tabPageLiveTranscript";
            this.toolTip1.SetToolTip(this.tabPageLiveTranscript, resources.GetString("tabPageLiveTranscript.ToolTip"));
            this.tabPageLiveTranscript.UseVisualStyleBackColor = true;
            // 
            // linkLabelLiveTranscriptRegions
            // 
            resources.ApplyResources(this.linkLabelLiveTranscriptRegions, "linkLabelLiveTranscriptRegions");
            this.errorProvider1.SetError(this.linkLabelLiveTranscriptRegions, resources.GetString("linkLabelLiveTranscriptRegions.Error"));
            this.errorProvider1.SetIconAlignment(this.linkLabelLiveTranscriptRegions, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("linkLabelLiveTranscriptRegions.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.linkLabelLiveTranscriptRegions, ((int)(resources.GetObject("linkLabelLiveTranscriptRegions.IconPadding"))));
            this.linkLabelLiveTranscriptRegions.Name = "linkLabelLiveTranscriptRegions";
            this.linkLabelLiveTranscriptRegions.TabStop = true;
            this.toolTip1.SetToolTip(this.linkLabelLiveTranscriptRegions, resources.GetString("linkLabelLiveTranscriptRegions.ToolTip"));
            this.linkLabelLiveTranscriptRegions.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.MoreinfoLiveEncodingProfilelink_LinkClicked);
            // 
            // comboBoxLanguage
            // 
            resources.ApplyResources(this.comboBoxLanguage, "comboBoxLanguage");
            this.comboBoxLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.errorProvider1.SetError(this.comboBoxLanguage, resources.GetString("comboBoxLanguage.Error"));
            this.comboBoxLanguage.FormattingEnabled = true;
            this.errorProvider1.SetIconAlignment(this.comboBoxLanguage, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("comboBoxLanguage.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.comboBoxLanguage, ((int)(resources.GetObject("comboBoxLanguage.IconPadding"))));
            this.comboBoxLanguage.Name = "comboBoxLanguage";
            this.toolTip1.SetToolTip(this.comboBoxLanguage, resources.GetString("comboBoxLanguage.ToolTip"));
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.errorProvider1.SetError(this.label2, resources.GetString("label2.Error"));
            this.errorProvider1.SetIconAlignment(this.label2, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label2.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label2, ((int)(resources.GetObject("label2.IconPadding"))));
            this.label2.Name = "label2";
            this.toolTip1.SetToolTip(this.label2, resources.GetString("label2.ToolTip"));
            // 
            // linkLabelLiveTranscript
            // 
            resources.ApplyResources(this.linkLabelLiveTranscript, "linkLabelLiveTranscript");
            this.errorProvider1.SetError(this.linkLabelLiveTranscript, resources.GetString("linkLabelLiveTranscript.Error"));
            this.errorProvider1.SetIconAlignment(this.linkLabelLiveTranscript, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("linkLabelLiveTranscript.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.linkLabelLiveTranscript, ((int)(resources.GetObject("linkLabelLiveTranscript.IconPadding"))));
            this.linkLabelLiveTranscript.Name = "linkLabelLiveTranscript";
            this.linkLabelLiveTranscript.TabStop = true;
            this.toolTip1.SetToolTip(this.linkLabelLiveTranscript, resources.GetString("linkLabelLiveTranscript.ToolTip"));
            this.linkLabelLiveTranscript.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.MoreinfoLiveEncodingProfilelink_LinkClicked);
            // 
            // checkBoxEnableLiveTranscript
            // 
            resources.ApplyResources(this.checkBoxEnableLiveTranscript, "checkBoxEnableLiveTranscript");
            this.errorProvider1.SetError(this.checkBoxEnableLiveTranscript, resources.GetString("checkBoxEnableLiveTranscript.Error"));
            this.errorProvider1.SetIconAlignment(this.checkBoxEnableLiveTranscript, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("checkBoxEnableLiveTranscript.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.checkBoxEnableLiveTranscript, ((int)(resources.GetObject("checkBoxEnableLiveTranscript.IconPadding"))));
            this.checkBoxEnableLiveTranscript.Name = "checkBoxEnableLiveTranscript";
            this.toolTip1.SetToolTip(this.checkBoxEnableLiveTranscript, resources.GetString("checkBoxEnableLiveTranscript.ToolTip"));
            this.checkBoxEnableLiveTranscript.UseVisualStyleBackColor = true;
            this.checkBoxEnableLiveTranscript.CheckedChanged += new System.EventHandler(this.CheckBoxEnableLiveTranscript_CheckedChanged);
            // 
            // tabPageAdv
            // 
            resources.ApplyResources(this.tabPageAdv, "tabPageAdv");
            this.tabPageAdv.Controls.Add(this.radioButtonLowLatencyV2);
            this.tabPageAdv.Controls.Add(this.radioButtonLowLatencyV1);
            this.tabPageAdv.Controls.Add(this.labelStaticHostnamePrefix);
            this.tabPageAdv.Controls.Add(this.textBoxStaticHostname);
            this.tabPageAdv.Controls.Add(this.label1);
            this.tabPageAdv.Controls.Add(this.labelUrlSyntax);
            this.tabPageAdv.Controls.Add(this.buttonGenerateToken);
            this.tabPageAdv.Controls.Add(this.checkBoxKeyFrameIntDefined);
            this.tabPageAdv.Controls.Add(this.textBoxInputKeyFrame);
            this.tabPageAdv.Controls.Add(this.checkBoxLowLatency);
            this.tabPageAdv.Controls.Add(this.checkBoxVanityUrl);
            this.tabPageAdv.Controls.Add(this.label7);
            this.tabPageAdv.Controls.Add(this.textBoxInputId);
            this.errorProvider1.SetError(this.tabPageAdv, resources.GetString("tabPageAdv.Error"));
            this.errorProvider1.SetIconAlignment(this.tabPageAdv, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("tabPageAdv.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.tabPageAdv, ((int)(resources.GetObject("tabPageAdv.IconPadding"))));
            this.tabPageAdv.Name = "tabPageAdv";
            this.toolTip1.SetToolTip(this.tabPageAdv, resources.GetString("tabPageAdv.ToolTip"));
            this.tabPageAdv.UseVisualStyleBackColor = true;
            // 
            // radioButtonLowLatencyV2
            // 
            resources.ApplyResources(this.radioButtonLowLatencyV2, "radioButtonLowLatencyV2");
            this.errorProvider1.SetError(this.radioButtonLowLatencyV2, resources.GetString("radioButtonLowLatencyV2.Error"));
            this.errorProvider1.SetIconAlignment(this.radioButtonLowLatencyV2, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("radioButtonLowLatencyV2.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.radioButtonLowLatencyV2, ((int)(resources.GetObject("radioButtonLowLatencyV2.IconPadding"))));
            this.radioButtonLowLatencyV2.Name = "radioButtonLowLatencyV2";
            this.toolTip1.SetToolTip(this.radioButtonLowLatencyV2, resources.GetString("radioButtonLowLatencyV2.ToolTip"));
            this.radioButtonLowLatencyV2.UseVisualStyleBackColor = true;
            // 
            // radioButtonLowLatencyV1
            // 
            resources.ApplyResources(this.radioButtonLowLatencyV1, "radioButtonLowLatencyV1");
            this.radioButtonLowLatencyV1.Checked = true;
            this.errorProvider1.SetError(this.radioButtonLowLatencyV1, resources.GetString("radioButtonLowLatencyV1.Error"));
            this.errorProvider1.SetIconAlignment(this.radioButtonLowLatencyV1, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("radioButtonLowLatencyV1.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.radioButtonLowLatencyV1, ((int)(resources.GetObject("radioButtonLowLatencyV1.IconPadding"))));
            this.radioButtonLowLatencyV1.Name = "radioButtonLowLatencyV1";
            this.radioButtonLowLatencyV1.TabStop = true;
            this.toolTip1.SetToolTip(this.radioButtonLowLatencyV1, resources.GetString("radioButtonLowLatencyV1.ToolTip"));
            this.radioButtonLowLatencyV1.UseVisualStyleBackColor = true;
            // 
            // labelStaticHostnamePrefix
            // 
            resources.ApplyResources(this.labelStaticHostnamePrefix, "labelStaticHostnamePrefix");
            this.errorProvider1.SetError(this.labelStaticHostnamePrefix, resources.GetString("labelStaticHostnamePrefix.Error"));
            this.errorProvider1.SetIconAlignment(this.labelStaticHostnamePrefix, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("labelStaticHostnamePrefix.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.labelStaticHostnamePrefix, ((int)(resources.GetObject("labelStaticHostnamePrefix.IconPadding"))));
            this.labelStaticHostnamePrefix.Name = "labelStaticHostnamePrefix";
            this.toolTip1.SetToolTip(this.labelStaticHostnamePrefix, resources.GetString("labelStaticHostnamePrefix.ToolTip"));
            // 
            // textBoxStaticHostname
            // 
            resources.ApplyResources(this.textBoxStaticHostname, "textBoxStaticHostname");
            this.errorProvider1.SetError(this.textBoxStaticHostname, resources.GetString("textBoxStaticHostname.Error"));
            this.errorProvider1.SetIconAlignment(this.textBoxStaticHostname, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("textBoxStaticHostname.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.textBoxStaticHostname, ((int)(resources.GetObject("textBoxStaticHostname.IconPadding"))));
            this.textBoxStaticHostname.Name = "textBoxStaticHostname";
            this.toolTip1.SetToolTip(this.textBoxStaticHostname, resources.GetString("textBoxStaticHostname.ToolTip"));
            this.textBoxStaticHostname.TextChanged += new System.EventHandler(this.TextBoxStaticHostname_TextChanged);
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
            this.buttonGenerateToken.Click += new System.EventHandler(this.ButtonGenerateInputId_Click);
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
            this.checkBoxKeyFrameIntDefined.CheckedChanged += new System.EventHandler(this.CheckBoxKeyFrameIntDefined_CheckedChanged);
            // 
            // textBoxInputKeyFrame
            // 
            resources.ApplyResources(this.textBoxInputKeyFrame, "textBoxInputKeyFrame");
            this.errorProvider1.SetError(this.textBoxInputKeyFrame, resources.GetString("textBoxInputKeyFrame.Error"));
            this.errorProvider1.SetIconAlignment(this.textBoxInputKeyFrame, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("textBoxInputKeyFrame.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.textBoxInputKeyFrame, ((int)(resources.GetObject("textBoxInputKeyFrame.IconPadding"))));
            this.textBoxInputKeyFrame.Name = "textBoxInputKeyFrame";
            this.toolTip1.SetToolTip(this.textBoxInputKeyFrame, resources.GetString("textBoxInputKeyFrame.ToolTip"));
            this.textBoxInputKeyFrame.TextChanged += new System.EventHandler(this.TextBoxInputKeyFrame_TextChanged);
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
            this.checkBoxLowLatency.CheckedChanged += new System.EventHandler(this.checkBoxLowLatency_CheckedChanged);
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
            this.checkBoxVanityUrl.CheckedChanged += new System.EventHandler(this.CheckBoxVanityUrl_CheckedChanged);
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
            // textBoxInputId
            // 
            resources.ApplyResources(this.textBoxInputId, "textBoxInputId");
            this.errorProvider1.SetError(this.textBoxInputId, resources.GetString("textBoxInputId.Error"));
            this.errorProvider1.SetIconAlignment(this.textBoxInputId, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("textBoxInputId.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.textBoxInputId, ((int)(resources.GetObject("textBoxInputId.IconPadding"))));
            this.textBoxInputId.Name = "textBoxInputId";
            this.toolTip1.SetToolTip(this.textBoxInputId, resources.GetString("textBoxInputId.ToolTip"));
            this.textBoxInputId.TextChanged += new System.EventHandler(this.TextBoxInputId_TextChanged);
            // 
            // tabPageAdvEncoding
            // 
            resources.ApplyResources(this.tabPageAdvEncoding, "tabPageAdvEncoding");
            this.tabPageAdvEncoding.Controls.Add(this.checkBoxEncodingKeyFrameInterval);
            this.tabPageAdvEncoding.Controls.Add(this.textBoxEncodingKeyFrameInterval);
            this.errorProvider1.SetError(this.tabPageAdvEncoding, resources.GetString("tabPageAdvEncoding.Error"));
            this.errorProvider1.SetIconAlignment(this.tabPageAdvEncoding, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("tabPageAdvEncoding.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.tabPageAdvEncoding, ((int)(resources.GetObject("tabPageAdvEncoding.IconPadding"))));
            this.tabPageAdvEncoding.Name = "tabPageAdvEncoding";
            this.toolTip1.SetToolTip(this.tabPageAdvEncoding, resources.GetString("tabPageAdvEncoding.ToolTip"));
            this.tabPageAdvEncoding.UseVisualStyleBackColor = true;
            // 
            // checkBoxEncodingKeyFrameInterval
            // 
            resources.ApplyResources(this.checkBoxEncodingKeyFrameInterval, "checkBoxEncodingKeyFrameInterval");
            this.errorProvider1.SetError(this.checkBoxEncodingKeyFrameInterval, resources.GetString("checkBoxEncodingKeyFrameInterval.Error"));
            this.errorProvider1.SetIconAlignment(this.checkBoxEncodingKeyFrameInterval, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("checkBoxEncodingKeyFrameInterval.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.checkBoxEncodingKeyFrameInterval, ((int)(resources.GetObject("checkBoxEncodingKeyFrameInterval.IconPadding"))));
            this.checkBoxEncodingKeyFrameInterval.Name = "checkBoxEncodingKeyFrameInterval";
            this.toolTip1.SetToolTip(this.checkBoxEncodingKeyFrameInterval, resources.GetString("checkBoxEncodingKeyFrameInterval.ToolTip"));
            this.checkBoxEncodingKeyFrameInterval.UseVisualStyleBackColor = true;
            this.checkBoxEncodingKeyFrameInterval.CheckedChanged += new System.EventHandler(this.CheckBoxEncodingKeyFrameInterval_CheckedChanged);
            // 
            // textBoxEncodingKeyFrameInterval
            // 
            resources.ApplyResources(this.textBoxEncodingKeyFrameInterval, "textBoxEncodingKeyFrameInterval");
            this.errorProvider1.SetError(this.textBoxEncodingKeyFrameInterval, resources.GetString("textBoxEncodingKeyFrameInterval.Error"));
            this.errorProvider1.SetIconAlignment(this.textBoxEncodingKeyFrameInterval, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("textBoxEncodingKeyFrameInterval.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.textBoxEncodingKeyFrameInterval, ((int)(resources.GetObject("textBoxEncodingKeyFrameInterval.IconPadding"))));
            this.textBoxEncodingKeyFrameInterval.Name = "textBoxEncodingKeyFrameInterval";
            this.toolTip1.SetToolTip(this.textBoxEncodingKeyFrameInterval, resources.GetString("textBoxEncodingKeyFrameInterval.ToolTip"));
            this.textBoxEncodingKeyFrameInterval.TextChanged += new System.EventHandler(this.TextBoxEncodingKeyFrameInterval_TextChanged);
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
            this.linkLabelMoreInfoPrice.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.MoreinfoLiveEncodingProfilelink_LinkClicked);
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
            this.Shown += new System.EventHandler(this.LiveEventCreation_Shown);
            this.panel1.ResumeLayout(false);
            this.tabControlLiveChannel.ResumeLayout(false);
            this.TabSettings.ResumeLayout(false);
            this.TabSettings.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.tabPageLiveEncoding.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panelPresetLiveEncoding.ResumeLayout(false);
            this.panelPresetLiveEncoding.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAudioProf)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewVideoProf)).EndInit();
            this.tabPageLiveTranscript.ResumeLayout(false);
            this.tabPageLiveTranscript.PerformLayout();
            this.tabPageAdv.ResumeLayout(false);
            this.tabPageAdv.PerformLayout();
            this.tabPageAdvEncoding.ResumeLayout(false);
            this.tabPageAdvEncoding.PerformLayout();
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
        private System.Windows.Forms.LinkLabel moreinfoLiveEventTypes;
        private System.Windows.Forms.Label labelWarning;
        private System.Windows.Forms.LinkLabel linkLabelMoreInfoPrice;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TabPage tabPageAdv;
        private System.Windows.Forms.CheckBox checkBoxKeyFrameIntDefined;
        private System.Windows.Forms.TextBox textBoxInputKeyFrame;
        private System.Windows.Forms.CheckBox checkBoxLowLatency;
        private System.Windows.Forms.CheckBox checkBoxVanityUrl;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxInputId;
        private System.Windows.Forms.Button buttonGenerateToken;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButtonTranscodingPremium;
        private System.Windows.Forms.RadioButton radioButtonTranscodingStd;
        private System.Windows.Forms.RadioButton radioButtonPassThroughStandard;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelUrlSyntax;
        private System.Windows.Forms.TableLayoutPanel panelPresetLiveEncoding;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TabPage tabPageLiveTranscript;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel linkLabelLiveTranscript;
        private System.Windows.Forms.CheckBox checkBoxEnableLiveTranscript;
        private System.Windows.Forms.ComboBox comboBoxLanguage;
        private System.Windows.Forms.LinkLabel linkLabelLiveTranscriptRegions;
        private System.Windows.Forms.Label labelStaticHostnamePrefix;
        private System.Windows.Forms.TextBox textBoxStaticHostname;
        private System.Windows.Forms.TabPage tabPageAdvEncoding;
        private System.Windows.Forms.CheckBox checkBoxEncodingKeyFrameInterval;
        private System.Windows.Forms.TextBox textBoxEncodingKeyFrameInterval;
        private System.Windows.Forms.RadioButton radioButtonPassThroughBasic;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.RadioButton radioButtonLowLatencyV2;
        private System.Windows.Forms.RadioButton radioButtonLowLatencyV1;
    }
}