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
            this.panelDisplayEncProfile = new System.Windows.Forms.Panel();
            this.dataGridViewVideoProf = new System.Windows.Forms.DataGridView();
            this.dataGridViewAudioProf = new System.Windows.Forms.DataGridView();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxCustomPreset = new System.Windows.Forms.TextBox();
            this.radioButtonCustomPreset = new System.Windows.Forms.RadioButton();
            this.radioButtonDefaultPreset = new System.Windows.Forms.RadioButton();
            this.tabPageAdv = new System.Windows.Forms.TabPage();
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
            this.panelDisplayEncProfile.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewVideoProf)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAudioProf)).BeginInit();
            this.tabPageAdv.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            resources.ApplyResources(this.buttonOk, "buttonOk");
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // textBoxRestrictIngestIP
            // 
            resources.ApplyResources(this.textBoxRestrictIngestIP, "textBoxRestrictIngestIP");
            this.textBoxRestrictIngestIP.Name = "textBoxRestrictIngestIP";
            this.textBoxRestrictIngestIP.TextChanged += new System.EventHandler(this.textBoxIP_TextChanged);
            // 
            // checkBoxRestrictIngestIP
            // 
            resources.ApplyResources(this.checkBoxRestrictIngestIP, "checkBoxRestrictIngestIP");
            this.checkBoxRestrictIngestIP.Name = "checkBoxRestrictIngestIP";
            this.checkBoxRestrictIngestIP.UseVisualStyleBackColor = true;
            this.checkBoxRestrictIngestIP.CheckedChanged += new System.EventHandler(this.checkBoxRestrictIngestIP_CheckedChanged);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // textboxchannelname
            // 
            resources.ApplyResources(this.textboxchannelname, "textboxchannelname");
            this.textboxchannelname.Name = "textboxchannelname";
            this.textboxchannelname.TextChanged += new System.EventHandler(this.textboxchannelname_TextChanged);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // comboBoxProtocolInput
            // 
            this.comboBoxProtocolInput.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxProtocolInput.FormattingEnabled = true;
            resources.ApplyResources(this.comboBoxProtocolInput, "comboBoxProtocolInput");
            this.comboBoxProtocolInput.Name = "comboBoxProtocolInput";
            this.comboBoxProtocolInput.SelectedIndexChanged += new System.EventHandler(this.comboBoxProtocolInput_SelectedIndexChanged);
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // textBoxDescription
            // 
            resources.ApplyResources(this.textBoxDescription, "textBoxDescription");
            this.textBoxDescription.Name = "textBoxDescription";
            // 
            // checkBoxStartChannel
            // 
            resources.ApplyResources(this.checkBoxStartChannel, "checkBoxStartChannel");
            this.checkBoxStartChannel.Name = "checkBoxStartChannel";
            this.checkBoxStartChannel.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonOk);
            this.panel1.Name = "panel1";
            // 
            // tabControlLiveChannel
            // 
            resources.ApplyResources(this.tabControlLiveChannel, "tabControlLiveChannel");
            this.tabControlLiveChannel.Controls.Add(this.TabSettings);
            this.tabControlLiveChannel.Controls.Add(this.tabPageLiveEncoding);
            this.tabControlLiveChannel.Controls.Add(this.tabPageAdv);
            this.tabControlLiveChannel.Name = "tabControlLiveChannel";
            this.tabControlLiveChannel.SelectedIndex = 0;
            // 
            // TabSettings
            // 
            this.TabSettings.Controls.Add(this.groupBox1);
            this.TabSettings.Controls.Add(this.checkBoxRestrictPreviewIP);
            this.TabSettings.Controls.Add(this.textBoxRestrictPreviewIP);
            this.TabSettings.Controls.Add(this.label4);
            this.TabSettings.Controls.Add(this.comboBoxProtocolInput);
            this.TabSettings.Controls.Add(this.checkBoxRestrictIngestIP);
            this.TabSettings.Controls.Add(this.textBoxRestrictIngestIP);
            resources.ApplyResources(this.TabSettings, "TabSettings");
            this.TabSettings.Name = "TabSettings";
            this.TabSettings.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButtonTranscodingPremium);
            this.groupBox1.Controls.Add(this.moreinfoLiveEncodingProfilelink);
            this.groupBox1.Controls.Add(this.moreinfoLiveStreamingProfilelink);
            this.groupBox1.Controls.Add(this.radioButtonTranscodingStd);
            this.groupBox1.Controls.Add(this.radioButtonTranscodingNone);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // radioButtonTranscodingPremium
            // 
            resources.ApplyResources(this.radioButtonTranscodingPremium, "radioButtonTranscodingPremium");
            this.radioButtonTranscodingPremium.Name = "radioButtonTranscodingPremium";
            this.radioButtonTranscodingPremium.UseVisualStyleBackColor = true;
            this.radioButtonTranscodingPremium.CheckedChanged += new System.EventHandler(this.radioButtonTranscodingNone_CheckedChanged);
            // 
            // moreinfoLiveEncodingProfilelink
            // 
            resources.ApplyResources(this.moreinfoLiveEncodingProfilelink, "moreinfoLiveEncodingProfilelink");
            this.moreinfoLiveEncodingProfilelink.Name = "moreinfoLiveEncodingProfilelink";
            this.moreinfoLiveEncodingProfilelink.TabStop = true;
            this.moreinfoLiveEncodingProfilelink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.moreinfoLiveEncodingProfilelink_LinkClicked);
            // 
            // moreinfoLiveStreamingProfilelink
            // 
            resources.ApplyResources(this.moreinfoLiveStreamingProfilelink, "moreinfoLiveStreamingProfilelink");
            this.moreinfoLiveStreamingProfilelink.Name = "moreinfoLiveStreamingProfilelink";
            this.moreinfoLiveStreamingProfilelink.TabStop = true;
            this.moreinfoLiveStreamingProfilelink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.moreinfoLiveEncodingProfilelink_LinkClicked);
            // 
            // radioButtonTranscodingStd
            // 
            resources.ApplyResources(this.radioButtonTranscodingStd, "radioButtonTranscodingStd");
            this.radioButtonTranscodingStd.Name = "radioButtonTranscodingStd";
            this.radioButtonTranscodingStd.UseVisualStyleBackColor = true;
            this.radioButtonTranscodingStd.CheckedChanged += new System.EventHandler(this.radioButtonTranscodingNone_CheckedChanged);
            // 
            // radioButtonTranscodingNone
            // 
            resources.ApplyResources(this.radioButtonTranscodingNone, "radioButtonTranscodingNone");
            this.radioButtonTranscodingNone.Checked = true;
            this.radioButtonTranscodingNone.Name = "radioButtonTranscodingNone";
            this.radioButtonTranscodingNone.TabStop = true;
            this.radioButtonTranscodingNone.UseVisualStyleBackColor = true;
            this.radioButtonTranscodingNone.CheckedChanged += new System.EventHandler(this.radioButtonTranscodingNone_CheckedChanged);
            // 
            // checkBoxRestrictPreviewIP
            // 
            resources.ApplyResources(this.checkBoxRestrictPreviewIP, "checkBoxRestrictPreviewIP");
            this.checkBoxRestrictPreviewIP.Name = "checkBoxRestrictPreviewIP";
            this.checkBoxRestrictPreviewIP.UseVisualStyleBackColor = true;
            this.checkBoxRestrictPreviewIP.CheckedChanged += new System.EventHandler(this.checkBoxRestrictPreviewIP_CheckedChanged);
            // 
            // textBoxRestrictPreviewIP
            // 
            resources.ApplyResources(this.textBoxRestrictPreviewIP, "textBoxRestrictPreviewIP");
            this.textBoxRestrictPreviewIP.Name = "textBoxRestrictPreviewIP";
            this.textBoxRestrictPreviewIP.TextChanged += new System.EventHandler(this.textBoxIP_TextChanged);
            // 
            // tabPageLiveEncoding
            // 
            this.tabPageLiveEncoding.Controls.Add(this.panelDisplayEncProfile);
            this.tabPageLiveEncoding.Controls.Add(this.label6);
            this.tabPageLiveEncoding.Controls.Add(this.textBoxCustomPreset);
            this.tabPageLiveEncoding.Controls.Add(this.radioButtonCustomPreset);
            this.tabPageLiveEncoding.Controls.Add(this.radioButtonDefaultPreset);
            resources.ApplyResources(this.tabPageLiveEncoding, "tabPageLiveEncoding");
            this.tabPageLiveEncoding.Name = "tabPageLiveEncoding";
            this.tabPageLiveEncoding.UseVisualStyleBackColor = true;
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
            // textBoxCustomPreset
            // 
            resources.ApplyResources(this.textBoxCustomPreset, "textBoxCustomPreset");
            this.textBoxCustomPreset.Name = "textBoxCustomPreset";
            this.textBoxCustomPreset.TextChanged += new System.EventHandler(this.textBoxCustomPreset_TextChanged);
            // 
            // radioButtonCustomPreset
            // 
            resources.ApplyResources(this.radioButtonCustomPreset, "radioButtonCustomPreset");
            this.radioButtonCustomPreset.Name = "radioButtonCustomPreset";
            this.radioButtonCustomPreset.UseVisualStyleBackColor = true;
            this.radioButtonCustomPreset.CheckedChanged += new System.EventHandler(this.radioButtonCustomPreset_CheckedChanged);
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
            // tabPageAdv
            // 
            this.tabPageAdv.Controls.Add(this.buttonGenerateToken);
            this.tabPageAdv.Controls.Add(this.checkBoxKeyFrameIntDefined);
            this.tabPageAdv.Controls.Add(this.textBoxKeyFrame);
            this.tabPageAdv.Controls.Add(this.checkBoxLowLatency);
            this.tabPageAdv.Controls.Add(this.checkBoxVanityUrl);
            this.tabPageAdv.Controls.Add(this.label7);
            this.tabPageAdv.Controls.Add(this.textBoxToken);
            resources.ApplyResources(this.tabPageAdv, "tabPageAdv");
            this.tabPageAdv.Name = "tabPageAdv";
            this.tabPageAdv.UseVisualStyleBackColor = true;
            // 
            // buttonGenerateToken
            // 
            resources.ApplyResources(this.buttonGenerateToken, "buttonGenerateToken");
            this.buttonGenerateToken.Name = "buttonGenerateToken";
            this.buttonGenerateToken.UseVisualStyleBackColor = true;
            this.buttonGenerateToken.Click += new System.EventHandler(this.buttonGenerateToken_Click);
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
            // 
            // checkBoxLowLatency
            // 
            resources.ApplyResources(this.checkBoxLowLatency, "checkBoxLowLatency");
            this.checkBoxLowLatency.Name = "checkBoxLowLatency";
            this.checkBoxLowLatency.UseVisualStyleBackColor = true;
            // 
            // checkBoxVanityUrl
            // 
            resources.ApplyResources(this.checkBoxVanityUrl, "checkBoxVanityUrl");
            this.checkBoxVanityUrl.Name = "checkBoxVanityUrl";
            this.checkBoxVanityUrl.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // textBoxToken
            // 
            resources.ApplyResources(this.textBoxToken, "textBoxToken");
            this.textBoxToken.Name = "textBoxToken";
            // 
            // labelWarning
            // 
            resources.ApplyResources(this.labelWarning, "labelWarning");
            this.labelWarning.ForeColor = System.Drawing.Color.Red;
            this.labelWarning.Name = "labelWarning";
            // 
            // linkLabelMoreInfoPrice
            // 
            resources.ApplyResources(this.linkLabelMoreInfoPrice, "linkLabelMoreInfoPrice");
            this.linkLabelMoreInfoPrice.Name = "linkLabelMoreInfoPrice";
            this.linkLabelMoreInfoPrice.TabStop = true;
            this.linkLabelMoreInfoPrice.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.moreinfoLiveEncodingProfilelink_LinkClicked);
            // 
            // openFileDialogSlate
            // 
            resources.ApplyResources(this.openFileDialogSlate, "openFileDialogSlate");
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // LiveEventCreation
            // 
            this.AcceptButton = this.buttonOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
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
            this.Load += new System.EventHandler(this.CreateLiveChannel_Load);
            this.panel1.ResumeLayout(false);
            this.tabControlLiveChannel.ResumeLayout(false);
            this.TabSettings.ResumeLayout(false);
            this.TabSettings.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPageLiveEncoding.ResumeLayout(false);
            this.tabPageLiveEncoding.PerformLayout();
            this.panelDisplayEncProfile.ResumeLayout(false);
            this.panelDisplayEncProfile.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewVideoProf)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAudioProf)).EndInit();
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
        private System.Windows.Forms.Panel panelDisplayEncProfile;
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
    }
}