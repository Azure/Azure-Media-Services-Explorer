namespace AMSExplorer
{
    partial class CreateLiveChannel
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
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.textBoxRestrictIngestIP = new System.Windows.Forms.TextBox();
            this.checkBoxRestrictIngestIP = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textboxchannelname = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxProtocolInput = new System.Windows.Forms.ComboBox();
            this.checkBoxKeyFrameIntDefined = new System.Windows.Forms.CheckBox();
            this.checkBoxHLSFragPerSegDefined = new System.Windows.Forms.CheckBox();
            this.numericUpDownHLSFragPerSeg = new System.Windows.Forms.NumericUpDown();
            this.textBoxKeyFrame = new System.Windows.Forms.TextBox();
            this.labelWarning = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.checkBoxStartChannel = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControlLiveChannel = new System.Windows.Forms.TabControl();
            this.TabSettings = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxEncodingType = new System.Windows.Forms.ComboBox();
            this.tabPageLiveEncoding = new System.Windows.Forms.TabPage();
            this.label8 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxAdMarkerSource = new System.Windows.Forms.ComboBox();
            this.comboBoxEncodingPreset = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.panelRTP = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.numericUpDownVideoStreamIndex = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxVideoStreamName = new System.Windows.Forms.TextBox();
            this.tabPageAudioOptions = new System.Windows.Forms.TabPage();
            this.tabPageAdConfig = new System.Windows.Forms.TabPage();
            this.checkBoxAdInsertSlate = new System.Windows.Forms.CheckBox();
            this.textBoxSlateImage = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHLSFragPerSeg)).BeginInit();
            this.panel1.SuspendLayout();
            this.tabControlLiveChannel.SuspendLayout();
            this.TabSettings.SuspendLayout();
            this.tabPageLiveEncoding.SuspendLayout();
            this.panelRTP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVideoStreamIndex)).BeginInit();
            this.tabPageAdConfig.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(278, 12);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(98, 23);
            this.buttonOk.TabIndex = 4;
            this.buttonOk.Text = "Create Channel";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(382, 12);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(98, 23);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // textBoxRestrictIngestIP
            // 
            this.textBoxRestrictIngestIP.Enabled = false;
            this.textBoxRestrictIngestIP.Location = new System.Drawing.Point(18, 179);
            this.textBoxRestrictIngestIP.Name = "textBoxRestrictIngestIP";
            this.textBoxRestrictIngestIP.Size = new System.Drawing.Size(239, 20);
            this.textBoxRestrictIngestIP.TabIndex = 6;
            this.textBoxRestrictIngestIP.TextChanged += new System.EventHandler(this.textBoxRestrictIngestIP_TextChanged);
            // 
            // checkBoxRestrictIngestIP
            // 
            this.checkBoxRestrictIngestIP.AutoSize = true;
            this.checkBoxRestrictIngestIP.Location = new System.Drawing.Point(18, 156);
            this.checkBoxRestrictIngestIP.Name = "checkBoxRestrictIngestIP";
            this.checkBoxRestrictIngestIP.Size = new System.Drawing.Size(183, 17);
            this.checkBoxRestrictIngestIP.TabIndex = 5;
            this.checkBoxRestrictIngestIP.Text = "Restrict ingest to this IP address :";
            this.checkBoxRestrictIngestIP.UseVisualStyleBackColor = true;
            this.checkBoxRestrictIngestIP.CheckedChanged += new System.EventHandler(this.checkBoxRestrictIngestIP_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 13);
            this.label3.TabIndex = 45;
            this.label3.Text = "Channel name :";
            // 
            // textboxchannelname
            // 
            this.textboxchannelname.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textboxchannelname.Location = new System.Drawing.Point(30, 32);
            this.textboxchannelname.Name = "textboxchannelname";
            this.textboxchannelname.Size = new System.Drawing.Size(429, 20);
            this.textboxchannelname.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Input Protocol :";
            // 
            // comboBoxProtocolInput
            // 
            this.comboBoxProtocolInput.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxProtocolInput.FormattingEnabled = true;
            this.comboBoxProtocolInput.Location = new System.Drawing.Point(18, 31);
            this.comboBoxProtocolInput.Name = "comboBoxProtocolInput";
            this.comboBoxProtocolInput.Size = new System.Drawing.Size(183, 21);
            this.comboBoxProtocolInput.TabIndex = 0;
            this.comboBoxProtocolInput.SelectedIndexChanged += new System.EventHandler(this.comboBoxProtocolInput_SelectedIndexChanged);
            // 
            // checkBoxKeyFrameIntDefined
            // 
            this.checkBoxKeyFrameIntDefined.AutoSize = true;
            this.checkBoxKeyFrameIntDefined.Location = new System.Drawing.Point(252, 11);
            this.checkBoxKeyFrameIntDefined.Name = "checkBoxKeyFrameIntDefined";
            this.checkBoxKeyFrameIntDefined.Size = new System.Drawing.Size(130, 17);
            this.checkBoxKeyFrameIntDefined.TabIndex = 1;
            this.checkBoxKeyFrameIntDefined.Text = "Key frame interval (s) :";
            this.checkBoxKeyFrameIntDefined.UseVisualStyleBackColor = true;
            this.checkBoxKeyFrameIntDefined.CheckedChanged += new System.EventHandler(this.checkBoxKeyFrameIntDefined_CheckedChanged);
            // 
            // checkBoxHLSFragPerSegDefined
            // 
            this.checkBoxHLSFragPerSegDefined.AutoSize = true;
            this.checkBoxHLSFragPerSegDefined.Location = new System.Drawing.Point(252, 64);
            this.checkBoxHLSFragPerSegDefined.Name = "checkBoxHLSFragPerSegDefined";
            this.checkBoxHLSFragPerSegDefined.Size = new System.Drawing.Size(163, 17);
            this.checkBoxHLSFragPerSegDefined.TabIndex = 3;
            this.checkBoxHLSFragPerSegDefined.Text = "HLS fragments per segment :";
            this.checkBoxHLSFragPerSegDefined.UseVisualStyleBackColor = true;
            this.checkBoxHLSFragPerSegDefined.CheckedChanged += new System.EventHandler(this.checkBoxHLSFragPerSegDefined_CheckedChanged);
            // 
            // numericUpDownHLSFragPerSeg
            // 
            this.numericUpDownHLSFragPerSeg.Enabled = false;
            this.numericUpDownHLSFragPerSeg.Location = new System.Drawing.Point(252, 84);
            this.numericUpDownHLSFragPerSeg.Name = "numericUpDownHLSFragPerSeg";
            this.numericUpDownHLSFragPerSeg.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownHLSFragPerSeg.TabIndex = 4;
            // 
            // textBoxKeyFrame
            // 
            this.textBoxKeyFrame.Enabled = false;
            this.textBoxKeyFrame.Location = new System.Drawing.Point(252, 31);
            this.textBoxKeyFrame.Name = "textBoxKeyFrame";
            this.textBoxKeyFrame.Size = new System.Drawing.Size(121, 20);
            this.textBoxKeyFrame.TabIndex = 2;
            // 
            // labelWarning
            // 
            this.labelWarning.AutoSize = true;
            this.labelWarning.ForeColor = System.Drawing.Color.Red;
            this.labelWarning.Location = new System.Drawing.Point(20, 211);
            this.labelWarning.Name = "labelWarning";
            this.labelWarning.Size = new System.Drawing.Size(47, 13);
            this.labelWarning.TabIndex = 45;
            this.labelWarning.Text = "Warning";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(27, 63);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 13);
            this.label5.TabIndex = 48;
            this.label5.Text = "Description :";
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDescription.Location = new System.Drawing.Point(30, 79);
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(429, 20);
            this.textBoxDescription.TabIndex = 1;
            // 
            // checkBoxStartChannel
            // 
            this.checkBoxStartChannel.AutoSize = true;
            this.checkBoxStartChannel.Location = new System.Drawing.Point(30, 414);
            this.checkBoxStartChannel.Name = "checkBoxStartChannel";
            this.checkBoxStartChannel.Size = new System.Drawing.Size(153, 17);
            this.checkBoxStartChannel.TabIndex = 3;
            this.checkBoxStartChannel.Text = "Start the new channel now";
            this.checkBoxStartChannel.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonOk);
            this.panel1.Location = new System.Drawing.Point(-2, 445);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(494, 48);
            this.panel1.TabIndex = 59;
            // 
            // tabControlLiveChannel
            // 
            this.tabControlLiveChannel.Controls.Add(this.TabSettings);
            this.tabControlLiveChannel.Controls.Add(this.tabPageLiveEncoding);
            this.tabControlLiveChannel.Controls.Add(this.tabPageAudioOptions);
            this.tabControlLiveChannel.Controls.Add(this.tabPageAdConfig);
            this.tabControlLiveChannel.Location = new System.Drawing.Point(30, 120);
            this.tabControlLiveChannel.Name = "tabControlLiveChannel";
            this.tabControlLiveChannel.SelectedIndex = 0;
            this.tabControlLiveChannel.Size = new System.Drawing.Size(429, 272);
            this.tabControlLiveChannel.TabIndex = 60;
            // 
            // TabSettings
            // 
            this.TabSettings.Controls.Add(this.checkBoxKeyFrameIntDefined);
            this.TabSettings.Controls.Add(this.checkBoxHLSFragPerSegDefined);
            this.TabSettings.Controls.Add(this.label2);
            this.TabSettings.Controls.Add(this.label4);
            this.TabSettings.Controls.Add(this.numericUpDownHLSFragPerSeg);
            this.TabSettings.Controls.Add(this.comboBoxEncodingType);
            this.TabSettings.Controls.Add(this.comboBoxProtocolInput);
            this.TabSettings.Controls.Add(this.textBoxKeyFrame);
            this.TabSettings.Controls.Add(this.labelWarning);
            this.TabSettings.Controls.Add(this.checkBoxRestrictIngestIP);
            this.TabSettings.Controls.Add(this.textBoxRestrictIngestIP);
            this.TabSettings.Location = new System.Drawing.Point(4, 22);
            this.TabSettings.Name = "TabSettings";
            this.TabSettings.Padding = new System.Windows.Forms.Padding(3);
            this.TabSettings.Size = new System.Drawing.Size(421, 246);
            this.TabSettings.TabIndex = 0;
            this.TabSettings.Text = "Channel Settings";
            this.TabSettings.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(204, 13);
            this.label2.TabIndex = 47;
            this.label2.Text = "Live Encoding (in Azure Media Services) :";
            // 
            // comboBoxEncodingType
            // 
            this.comboBoxEncodingType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEncodingType.FormattingEnabled = true;
            this.comboBoxEncodingType.Location = new System.Drawing.Point(18, 80);
            this.comboBoxEncodingType.Name = "comboBoxEncodingType";
            this.comboBoxEncodingType.Size = new System.Drawing.Size(183, 21);
            this.comboBoxEncodingType.TabIndex = 46;
            this.comboBoxEncodingType.SelectedIndexChanged += new System.EventHandler(this.comboBoxEncodingType_SelectedIndexChanged);
            // 
            // tabPageLiveEncoding
            // 
            this.tabPageLiveEncoding.Controls.Add(this.panelRTP);
            this.tabPageLiveEncoding.Controls.Add(this.comboBoxEncodingPreset);
            this.tabPageLiveEncoding.Controls.Add(this.label6);
            this.tabPageLiveEncoding.Location = new System.Drawing.Point(4, 22);
            this.tabPageLiveEncoding.Name = "tabPageLiveEncoding";
            this.tabPageLiveEncoding.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLiveEncoding.Size = new System.Drawing.Size(421, 246);
            this.tabPageLiveEncoding.TabIndex = 1;
            this.tabPageLiveEncoding.Text = "Live Encoding";
            this.tabPageLiveEncoding.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label8.Location = new System.Drawing.Point(215, 36);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(190, 14);
            this.label8.TabIndex = 69;
            this.label8.Text = "SCTE-35 only available for RTP input";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Ad marker source :";
            // 
            // comboBoxAdMarkerSource
            // 
            this.comboBoxAdMarkerSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAdMarkerSource.FormattingEnabled = true;
            this.comboBoxAdMarkerSource.Location = new System.Drawing.Point(9, 33);
            this.comboBoxAdMarkerSource.Name = "comboBoxAdMarkerSource";
            this.comboBoxAdMarkerSource.Size = new System.Drawing.Size(200, 21);
            this.comboBoxAdMarkerSource.TabIndex = 12;
            // 
            // comboBoxEncodingPreset
            // 
            this.comboBoxEncodingPreset.FormattingEnabled = true;
            this.comboBoxEncodingPreset.Location = new System.Drawing.Point(19, 30);
            this.comboBoxEncodingPreset.Name = "comboBoxEncodingPreset";
            this.comboBoxEncodingPreset.Size = new System.Drawing.Size(200, 21);
            this.comboBoxEncodingPreset.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 14);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Preset :";
            // 
            // panelRTP
            // 
            this.panelRTP.Controls.Add(this.textBoxVideoStreamName);
            this.panelRTP.Controls.Add(this.label9);
            this.panelRTP.Controls.Add(this.numericUpDownVideoStreamIndex);
            this.panelRTP.Controls.Add(this.label7);
            this.panelRTP.Location = new System.Drawing.Point(7, 123);
            this.panelRTP.Name = "panelRTP";
            this.panelRTP.Size = new System.Drawing.Size(408, 100);
            this.panelRTP.TabIndex = 70;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 14);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(105, 13);
            this.label7.TabIndex = 71;
            this.label7.Text = "Video Stream Index :";
            // 
            // numericUpDownVideoStreamIndex
            // 
            this.numericUpDownVideoStreamIndex.Location = new System.Drawing.Point(12, 31);
            this.numericUpDownVideoStreamIndex.Name = "numericUpDownVideoStreamIndex";
            this.numericUpDownVideoStreamIndex.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownVideoStreamIndex.TabIndex = 72;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 64);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(153, 13);
            this.label9.TabIndex = 73;
            this.label9.Text = "Video Stream Name (optional) :";
            // 
            // textBoxVideoStreamName
            // 
            this.textBoxVideoStreamName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxVideoStreamName.Location = new System.Drawing.Point(12, 77);
            this.textBoxVideoStreamName.Name = "textBoxVideoStreamName";
            this.textBoxVideoStreamName.Size = new System.Drawing.Size(393, 20);
            this.textBoxVideoStreamName.TabIndex = 61;
            // 
            // tabPageAudioOptions
            // 
            this.tabPageAudioOptions.Location = new System.Drawing.Point(4, 22);
            this.tabPageAudioOptions.Name = "tabPageAudioOptions";
            this.tabPageAudioOptions.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAudioOptions.Size = new System.Drawing.Size(421, 246);
            this.tabPageAudioOptions.TabIndex = 2;
            this.tabPageAudioOptions.Text = "Audio Options";
            this.tabPageAudioOptions.UseVisualStyleBackColor = true;
            // 
            // tabPageAdConfig
            // 
            this.tabPageAdConfig.Controls.Add(this.panel2);
            this.tabPageAdConfig.Controls.Add(this.checkBoxAdInsertSlate);
            this.tabPageAdConfig.Controls.Add(this.label8);
            this.tabPageAdConfig.Controls.Add(this.comboBoxAdMarkerSource);
            this.tabPageAdConfig.Controls.Add(this.label1);
            this.tabPageAdConfig.Location = new System.Drawing.Point(4, 22);
            this.tabPageAdConfig.Name = "tabPageAdConfig";
            this.tabPageAdConfig.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAdConfig.Size = new System.Drawing.Size(421, 246);
            this.tabPageAdConfig.TabIndex = 3;
            this.tabPageAdConfig.Text = "Advertising Configuration";
            this.tabPageAdConfig.UseVisualStyleBackColor = true;
            // 
            // checkBoxAdInsertSlate
            // 
            this.checkBoxAdInsertSlate.AutoSize = true;
            this.checkBoxAdInsertSlate.Location = new System.Drawing.Point(9, 73);
            this.checkBoxAdInsertSlate.Name = "checkBoxAdInsertSlate";
            this.checkBoxAdInsertSlate.Size = new System.Drawing.Size(142, 17);
            this.checkBoxAdInsertSlate.TabIndex = 70;
            this.checkBoxAdInsertSlate.Text = "Insert Slate on Ad Signal";
            this.checkBoxAdInsertSlate.UseVisualStyleBackColor = true;
            // 
            // textBoxSlateImage
            // 
            this.textBoxSlateImage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSlateImage.Location = new System.Drawing.Point(17, 29);
            this.textBoxSlateImage.Name = "textBoxSlateImage";
            this.textBoxSlateImage.Size = new System.Drawing.Size(385, 20);
            this.textBoxSlateImage.TabIndex = 74;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(14, 16);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(117, 13);
            this.label10.TabIndex = 75;
            this.label10.Text = "Default Slate Asset ID :";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.textBoxSlateImage);
            this.panel2.Location = new System.Drawing.Point(3, 96);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(415, 100);
            this.panel2.TabIndex = 76;
            // 
            // CreateLiveChannel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(490, 492);
            this.Controls.Add(this.tabControlLiveChannel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.checkBoxStartChannel);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxDescription);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textboxchannelname);
            this.Name = "CreateLiveChannel";
            this.Text = "Create a live channel";
            this.Load += new System.EventHandler(this.CreateLiveChannel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHLSFragPerSeg)).EndInit();
            this.panel1.ResumeLayout(false);
            this.tabControlLiveChannel.ResumeLayout(false);
            this.TabSettings.ResumeLayout(false);
            this.TabSettings.PerformLayout();
            this.tabPageLiveEncoding.ResumeLayout(false);
            this.tabPageLiveEncoding.PerformLayout();
            this.panelRTP.ResumeLayout(false);
            this.panelRTP.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVideoStreamIndex)).EndInit();
            this.tabPageAdConfig.ResumeLayout(false);
            this.tabPageAdConfig.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
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
        private System.Windows.Forms.Label labelWarning;
        private System.Windows.Forms.TextBox textBoxKeyFrame;
        private System.Windows.Forms.NumericUpDown numericUpDownHLSFragPerSeg;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.CheckBox checkBoxStartChannel;
        private System.Windows.Forms.CheckBox checkBoxHLSFragPerSegDefined;
        private System.Windows.Forms.CheckBox checkBoxKeyFrameIntDefined;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControlLiveChannel;
        private System.Windows.Forms.TabPage TabSettings;
        private System.Windows.Forms.TabPage tabPageLiveEncoding;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxEncodingType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBoxEncodingPreset;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxAdMarkerSource;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panelRTP;
        private System.Windows.Forms.TextBox textBoxVideoStreamName;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown numericUpDownVideoStreamIndex;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TabPage tabPageAudioOptions;
        private System.Windows.Forms.TabPage tabPageAdConfig;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBoxSlateImage;
        private System.Windows.Forms.CheckBox checkBoxAdInsertSlate;
    }
}