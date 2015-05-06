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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateLiveChannel));
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
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.checkBoxStartChannel = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControlLiveChannel = new System.Windows.Forms.TabControl();
            this.TabSettings = new System.Windows.Forms.TabPage();
            this.checkBoxRestrictPreviewIP = new System.Windows.Forms.CheckBox();
            this.textBoxRestrictPreviewIP = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxEncodingType = new System.Windows.Forms.ComboBox();
            this.tabPageLiveEncoding = new System.Windows.Forms.TabPage();
            this.panelRTP = new System.Windows.Forms.Panel();
            this.numericUpDownVideoStreamIndex = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBoxEncodingPreset = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tabPageAudioOptions = new System.Windows.Forms.TabPage();
            this.panelAudioControl = new System.Windows.Forms.Panel();
            this.buttonDelAddOption = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.dataGridViewAudioStreams = new System.Windows.Forms.DataGridView();
            this.comboBoxAudioLanguageMain = new System.Windows.Forms.ComboBox();
            this.buttonAddAudioStream = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.numericUpDownAudioIndexAddition = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.numericUpDownAudioIndexMain = new System.Windows.Forms.NumericUpDown();
            this.comboBoxAudioLanguageAddition = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.tabPageAdConfig = new System.Windows.Forms.TabPage();
            this.panelInsertSlate = new System.Windows.Forms.Panel();
            this.label14 = new System.Windows.Forms.Label();
            this.textBoxJPGSearch = new System.Windows.Forms.TextBox();
            this.listViewJPG1 = new AMSExplorer.ListViewSlateJPG();
            this.label15 = new System.Windows.Forms.Label();
            this.progressBarUpload = new System.Windows.Forms.ProgressBar();
            this.buttonUploadSlate = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.checkBoxInsertSlateOnAdMarker = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.comboBoxAdMarkerSource = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.openFileDialogSlate = new System.Windows.Forms.OpenFileDialog();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHLSFragPerSeg)).BeginInit();
            this.panel1.SuspendLayout();
            this.tabControlLiveChannel.SuspendLayout();
            this.TabSettings.SuspendLayout();
            this.tabPageLiveEncoding.SuspendLayout();
            this.panelRTP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVideoStreamIndex)).BeginInit();
            this.tabPageAudioOptions.SuspendLayout();
            this.panelAudioControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAudioStreams)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAudioIndexAddition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAudioIndexMain)).BeginInit();
            this.tabPageAdConfig.SuspendLayout();
            this.panelInsertSlate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
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
            this.textBoxRestrictIngestIP.Location = new System.Drawing.Point(19, 139);
            this.textBoxRestrictIngestIP.Name = "textBoxRestrictIngestIP";
            this.textBoxRestrictIngestIP.Size = new System.Drawing.Size(239, 20);
            this.textBoxRestrictIngestIP.TabIndex = 6;
            this.textBoxRestrictIngestIP.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxRestrictIP_Validating);
            // 
            // checkBoxRestrictIngestIP
            // 
            this.checkBoxRestrictIngestIP.AutoSize = true;
            this.checkBoxRestrictIngestIP.Location = new System.Drawing.Point(19, 116);
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
            this.textboxchannelname.Validating += new System.ComponentModel.CancelEventHandler(this.textboxchannelname_Validating);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Input Protocol :";
            // 
            // comboBoxProtocolInput
            // 
            this.comboBoxProtocolInput.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxProtocolInput.FormattingEnabled = true;
            this.comboBoxProtocolInput.Location = new System.Drawing.Point(19, 80);
            this.comboBoxProtocolInput.Name = "comboBoxProtocolInput";
            this.comboBoxProtocolInput.Size = new System.Drawing.Size(183, 21);
            this.comboBoxProtocolInput.TabIndex = 0;
            this.comboBoxProtocolInput.SelectedIndexChanged += new System.EventHandler(this.comboBoxProtocolInput_SelectedIndexChanged);
            // 
            // checkBoxKeyFrameIntDefined
            // 
            this.checkBoxKeyFrameIntDefined.AutoSize = true;
            this.checkBoxKeyFrameIntDefined.Location = new System.Drawing.Point(19, 222);
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
            this.checkBoxHLSFragPerSegDefined.Location = new System.Drawing.Point(19, 275);
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
            this.numericUpDownHLSFragPerSeg.Location = new System.Drawing.Point(19, 298);
            this.numericUpDownHLSFragPerSeg.Name = "numericUpDownHLSFragPerSeg";
            this.numericUpDownHLSFragPerSeg.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownHLSFragPerSeg.TabIndex = 4;
            // 
            // textBoxKeyFrame
            // 
            this.textBoxKeyFrame.Enabled = false;
            this.textBoxKeyFrame.Location = new System.Drawing.Point(19, 245);
            this.textBoxKeyFrame.Name = "textBoxKeyFrame";
            this.textBoxKeyFrame.Size = new System.Drawing.Size(121, 20);
            this.textBoxKeyFrame.TabIndex = 2;
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
            this.checkBoxStartChannel.Location = new System.Drawing.Point(30, 478);
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
            this.panel1.Location = new System.Drawing.Point(-2, 514);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(494, 48);
            this.panel1.TabIndex = 59;
            // 
            // tabControlLiveChannel
            // 
            this.tabControlLiveChannel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlLiveChannel.Controls.Add(this.TabSettings);
            this.tabControlLiveChannel.Controls.Add(this.tabPageLiveEncoding);
            this.tabControlLiveChannel.Controls.Add(this.tabPageAudioOptions);
            this.tabControlLiveChannel.Controls.Add(this.tabPageAdConfig);
            this.tabControlLiveChannel.Location = new System.Drawing.Point(30, 120);
            this.tabControlLiveChannel.Name = "tabControlLiveChannel";
            this.tabControlLiveChannel.SelectedIndex = 0;
            this.tabControlLiveChannel.Size = new System.Drawing.Size(429, 352);
            this.tabControlLiveChannel.TabIndex = 60;
            // 
            // TabSettings
            // 
            this.TabSettings.Controls.Add(this.checkBoxRestrictPreviewIP);
            this.TabSettings.Controls.Add(this.textBoxRestrictPreviewIP);
            this.TabSettings.Controls.Add(this.checkBoxKeyFrameIntDefined);
            this.TabSettings.Controls.Add(this.checkBoxHLSFragPerSegDefined);
            this.TabSettings.Controls.Add(this.label2);
            this.TabSettings.Controls.Add(this.label4);
            this.TabSettings.Controls.Add(this.numericUpDownHLSFragPerSeg);
            this.TabSettings.Controls.Add(this.comboBoxEncodingType);
            this.TabSettings.Controls.Add(this.comboBoxProtocolInput);
            this.TabSettings.Controls.Add(this.textBoxKeyFrame);
            this.TabSettings.Controls.Add(this.checkBoxRestrictIngestIP);
            this.TabSettings.Controls.Add(this.textBoxRestrictIngestIP);
            this.TabSettings.Location = new System.Drawing.Point(4, 22);
            this.TabSettings.Name = "TabSettings";
            this.TabSettings.Padding = new System.Windows.Forms.Padding(3);
            this.TabSettings.Size = new System.Drawing.Size(421, 326);
            this.TabSettings.TabIndex = 0;
            this.TabSettings.Text = "Channel Settings";
            this.TabSettings.UseVisualStyleBackColor = true;
            // 
            // checkBoxRestrictPreviewIP
            // 
            this.checkBoxRestrictPreviewIP.AutoSize = true;
            this.checkBoxRestrictPreviewIP.Location = new System.Drawing.Point(19, 169);
            this.checkBoxRestrictPreviewIP.Name = "checkBoxRestrictPreviewIP";
            this.checkBoxRestrictPreviewIP.Size = new System.Drawing.Size(192, 17);
            this.checkBoxRestrictPreviewIP.TabIndex = 48;
            this.checkBoxRestrictPreviewIP.Text = "Restrict preview to this IP address :";
            this.checkBoxRestrictPreviewIP.UseVisualStyleBackColor = true;
            this.checkBoxRestrictPreviewIP.CheckedChanged += new System.EventHandler(this.checkBoxRestrictPreviewIP_CheckedChanged);
            // 
            // textBoxRestrictPreviewIP
            // 
            this.textBoxRestrictPreviewIP.Enabled = false;
            this.textBoxRestrictPreviewIP.Location = new System.Drawing.Point(19, 192);
            this.textBoxRestrictPreviewIP.Name = "textBoxRestrictPreviewIP";
            this.textBoxRestrictPreviewIP.Size = new System.Drawing.Size(239, 20);
            this.textBoxRestrictPreviewIP.TabIndex = 49;
            this.textBoxRestrictPreviewIP.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxRestrictIP_Validating);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(204, 13);
            this.label2.TabIndex = 47;
            this.label2.Text = "Live Encoding (in Azure Media Services) :";
            // 
            // comboBoxEncodingType
            // 
            this.comboBoxEncodingType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEncodingType.FormattingEnabled = true;
            this.comboBoxEncodingType.Location = new System.Drawing.Point(19, 29);
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
            this.tabPageLiveEncoding.Size = new System.Drawing.Size(421, 326);
            this.tabPageLiveEncoding.TabIndex = 1;
            this.tabPageLiveEncoding.Text = "Live Encoding";
            this.tabPageLiveEncoding.UseVisualStyleBackColor = true;
            // 
            // panelRTP
            // 
            this.panelRTP.Controls.Add(this.numericUpDownVideoStreamIndex);
            this.panelRTP.Controls.Add(this.label7);
            this.panelRTP.Location = new System.Drawing.Point(7, 123);
            this.panelRTP.Name = "panelRTP";
            this.panelRTP.Size = new System.Drawing.Size(408, 100);
            this.panelRTP.TabIndex = 70;
            // 
            // numericUpDownVideoStreamIndex
            // 
            this.numericUpDownVideoStreamIndex.Location = new System.Drawing.Point(12, 31);
            this.numericUpDownVideoStreamIndex.Name = "numericUpDownVideoStreamIndex";
            this.numericUpDownVideoStreamIndex.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownVideoStreamIndex.TabIndex = 72;
            this.toolTip1.SetToolTip(this.numericUpDownVideoStreamIndex, resources.GetString("numericUpDownVideoStreamIndex.ToolTip"));
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
            // tabPageAudioOptions
            // 
            this.tabPageAudioOptions.Controls.Add(this.panelAudioControl);
            this.tabPageAudioOptions.Location = new System.Drawing.Point(4, 22);
            this.tabPageAudioOptions.Name = "tabPageAudioOptions";
            this.tabPageAudioOptions.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAudioOptions.Size = new System.Drawing.Size(421, 326);
            this.tabPageAudioOptions.TabIndex = 2;
            this.tabPageAudioOptions.Text = "Audio Options";
            this.tabPageAudioOptions.UseVisualStyleBackColor = true;
            // 
            // panelAudioControl
            // 
            this.panelAudioControl.Controls.Add(this.buttonDelAddOption);
            this.panelAudioControl.Controls.Add(this.label9);
            this.panelAudioControl.Controls.Add(this.checkBox1);
            this.panelAudioControl.Controls.Add(this.dataGridViewAudioStreams);
            this.panelAudioControl.Controls.Add(this.comboBoxAudioLanguageMain);
            this.panelAudioControl.Controls.Add(this.buttonAddAudioStream);
            this.panelAudioControl.Controls.Add(this.label11);
            this.panelAudioControl.Controls.Add(this.numericUpDownAudioIndexAddition);
            this.panelAudioControl.Controls.Add(this.label12);
            this.panelAudioControl.Controls.Add(this.numericUpDownAudioIndexMain);
            this.panelAudioControl.Controls.Add(this.comboBoxAudioLanguageAddition);
            this.panelAudioControl.Controls.Add(this.label13);
            this.panelAudioControl.Location = new System.Drawing.Point(6, 6);
            this.panelAudioControl.Name = "panelAudioControl";
            this.panelAudioControl.Size = new System.Drawing.Size(409, 314);
            this.panelAudioControl.TabIndex = 84;
            // 
            // buttonDelAddOption
            // 
            this.buttonDelAddOption.Location = new System.Drawing.Point(343, 224);
            this.buttonDelAddOption.Name = "buttonDelAddOption";
            this.buttonDelAddOption.Size = new System.Drawing.Size(59, 23);
            this.buttonDelAddOption.TabIndex = 84;
            this.buttonDelAddOption.Text = "Del";
            this.buttonDelAddOption.UseVisualStyleBackColor = true;
            this.buttonDelAddOption.Click += new System.EventHandler(this.buttonDelAddOption_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(227, 18);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(105, 13);
            this.label9.TabIndex = 73;
            this.label9.Text = "Audio Stream Index :";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(26, 96);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(151, 17);
            this.checkBox1.TabIndex = 77;
            this.checkBox1.Text = "Use multiple audio streams";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // dataGridViewAudioStreams
            // 
            this.dataGridViewAudioStreams.AllowUserToAddRows = false;
            this.dataGridViewAudioStreams.AllowUserToDeleteRows = false;
            this.dataGridViewAudioStreams.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewAudioStreams.Location = new System.Drawing.Point(26, 161);
            this.dataGridViewAudioStreams.Name = "dataGridViewAudioStreams";
            this.dataGridViewAudioStreams.ReadOnly = true;
            this.dataGridViewAudioStreams.RowHeadersVisible = false;
            this.dataGridViewAudioStreams.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewAudioStreams.Size = new System.Drawing.Size(308, 86);
            this.dataGridViewAudioStreams.TabIndex = 83;
            this.toolTip1.SetToolTip(this.dataGridViewAudioStreams, "Rule name / IP address, CIDR or subnet mask");
            // 
            // comboBoxAudioLanguageMain
            // 
            this.comboBoxAudioLanguageMain.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAudioLanguageMain.FormattingEnabled = true;
            this.comboBoxAudioLanguageMain.Location = new System.Drawing.Point(26, 34);
            this.comboBoxAudioLanguageMain.Name = "comboBoxAudioLanguageMain";
            this.comboBoxAudioLanguageMain.Size = new System.Drawing.Size(180, 21);
            this.comboBoxAudioLanguageMain.TabIndex = 75;
            // 
            // buttonAddAudioStream
            // 
            this.buttonAddAudioStream.Location = new System.Drawing.Point(343, 132);
            this.buttonAddAudioStream.Name = "buttonAddAudioStream";
            this.buttonAddAudioStream.Size = new System.Drawing.Size(59, 23);
            this.buttonAddAudioStream.TabIndex = 82;
            this.buttonAddAudioStream.Text = "Add";
            this.buttonAddAudioStream.UseVisualStyleBackColor = true;
            this.buttonAddAudioStream.Click += new System.EventHandler(this.buttonAddAudioStream_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(23, 18);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(61, 13);
            this.label11.TabIndex = 76;
            this.label11.Text = "Language :";
            // 
            // numericUpDownAudioIndexAddition
            // 
            this.numericUpDownAudioIndexAddition.Location = new System.Drawing.Point(230, 135);
            this.numericUpDownAudioIndexAddition.Name = "numericUpDownAudioIndexAddition";
            this.numericUpDownAudioIndexAddition.Size = new System.Drawing.Size(102, 20);
            this.numericUpDownAudioIndexAddition.TabIndex = 79;
            this.toolTip1.SetToolTip(this.numericUpDownAudioIndexAddition, resources.GetString("numericUpDownAudioIndexAddition.ToolTip"));
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(23, 118);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(61, 13);
            this.label12.TabIndex = 81;
            this.label12.Text = "Language :";
            // 
            // numericUpDownAudioIndexMain
            // 
            this.numericUpDownAudioIndexMain.Location = new System.Drawing.Point(230, 35);
            this.numericUpDownAudioIndexMain.Name = "numericUpDownAudioIndexMain";
            this.numericUpDownAudioIndexMain.Size = new System.Drawing.Size(102, 20);
            this.numericUpDownAudioIndexMain.TabIndex = 74;
            this.toolTip1.SetToolTip(this.numericUpDownAudioIndexMain, resources.GetString("numericUpDownAudioIndexMain.ToolTip"));
            // 
            // comboBoxAudioLanguageAddition
            // 
            this.comboBoxAudioLanguageAddition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAudioLanguageAddition.FormattingEnabled = true;
            this.comboBoxAudioLanguageAddition.Location = new System.Drawing.Point(26, 134);
            this.comboBoxAudioLanguageAddition.Name = "comboBoxAudioLanguageAddition";
            this.comboBoxAudioLanguageAddition.Size = new System.Drawing.Size(180, 21);
            this.comboBoxAudioLanguageAddition.TabIndex = 80;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(227, 118);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(105, 13);
            this.label13.TabIndex = 78;
            this.label13.Text = "Audio Stream Index :";
            // 
            // tabPageAdConfig
            // 
            this.tabPageAdConfig.Controls.Add(this.panelInsertSlate);
            this.tabPageAdConfig.Controls.Add(this.checkBoxInsertSlateOnAdMarker);
            this.tabPageAdConfig.Controls.Add(this.label8);
            this.tabPageAdConfig.Controls.Add(this.comboBoxAdMarkerSource);
            this.tabPageAdConfig.Controls.Add(this.label1);
            this.tabPageAdConfig.Location = new System.Drawing.Point(4, 22);
            this.tabPageAdConfig.Name = "tabPageAdConfig";
            this.tabPageAdConfig.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAdConfig.Size = new System.Drawing.Size(421, 326);
            this.tabPageAdConfig.TabIndex = 3;
            this.tabPageAdConfig.Text = "Advertising Configuration";
            this.tabPageAdConfig.UseVisualStyleBackColor = true;
            // 
            // panelInsertSlate
            // 
            this.panelInsertSlate.Controls.Add(this.label14);
            this.panelInsertSlate.Controls.Add(this.textBoxJPGSearch);
            this.panelInsertSlate.Controls.Add(this.listViewJPG1);
            this.panelInsertSlate.Controls.Add(this.label15);
            this.panelInsertSlate.Controls.Add(this.progressBarUpload);
            this.panelInsertSlate.Controls.Add(this.buttonUploadSlate);
            this.panelInsertSlate.Controls.Add(this.label10);
            this.panelInsertSlate.Enabled = false;
            this.panelInsertSlate.Location = new System.Drawing.Point(3, 96);
            this.panelInsertSlate.Name = "panelInsertSlate";
            this.panelInsertSlate.Size = new System.Drawing.Size(415, 224);
            this.panelInsertSlate.TabIndex = 76;
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(14, 155);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(108, 13);
            this.label14.TabIndex = 86;
            this.label14.Text = "Search in name or Id:";
            // 
            // textBoxJPGSearch
            // 
            this.textBoxJPGSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxJPGSearch.Location = new System.Drawing.Point(128, 152);
            this.textBoxJPGSearch.Name = "textBoxJPGSearch";
            this.textBoxJPGSearch.Size = new System.Drawing.Size(274, 20);
            this.textBoxJPGSearch.TabIndex = 85;
            this.textBoxJPGSearch.TextChanged += new System.EventHandler(this.textBoxJPGSearch_TextChanged);
            // 
            // listViewJPG1
            // 
            this.listViewJPG1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewJPG1.FullRowSelect = true;
            this.listViewJPG1.HideSelection = false;
            this.listViewJPG1.Location = new System.Drawing.Point(17, 32);
            this.listViewJPG1.MultiSelect = false;
            this.listViewJPG1.Name = "listViewJPG1";
            this.listViewJPG1.Size = new System.Drawing.Size(385, 114);
            this.listViewJPG1.TabIndex = 84;
            this.listViewJPG1.Tag = -1;
            this.listViewJPG1.UseCompatibleStateImageBehavior = false;
            this.listViewJPG1.View = System.Windows.Forms.View.Details;
            this.listViewJPG1.SelectedIndexChanged += new System.EventHandler(this.listViewJPG1_SelectedIndexChanged);
            // 
            // label15
            // 
            this.label15.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label15.Location = new System.Drawing.Point(6, 206);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(396, 13);
            this.label15.TabIndex = 78;
            this.label15.Text = "Image requirements: JPG file, 16:9 aspect ratio, 1920x1080 max and 3 MB max";
            this.label15.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // progressBarUpload
            // 
            this.progressBarUpload.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarUpload.Location = new System.Drawing.Point(128, 180);
            this.progressBarUpload.Name = "progressBarUpload";
            this.progressBarUpload.Size = new System.Drawing.Size(176, 23);
            this.progressBarUpload.TabIndex = 77;
            this.progressBarUpload.Visible = false;
            // 
            // buttonUploadSlate
            // 
            this.buttonUploadSlate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonUploadSlate.Location = new System.Drawing.Point(310, 180);
            this.buttonUploadSlate.Name = "buttonUploadSlate";
            this.buttonUploadSlate.Size = new System.Drawing.Size(92, 23);
            this.buttonUploadSlate.TabIndex = 76;
            this.buttonUploadSlate.Text = "Upload a file...";
            this.buttonUploadSlate.UseVisualStyleBackColor = true;
            this.buttonUploadSlate.Click += new System.EventHandler(this.buttonUploadSlate_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(14, 16);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(103, 13);
            this.label10.TabIndex = 75;
            this.label10.Text = "Default Slate Asset :";
            // 
            // checkBoxInsertSlateOnAdMarker
            // 
            this.checkBoxInsertSlateOnAdMarker.AutoSize = true;
            this.checkBoxInsertSlateOnAdMarker.Location = new System.Drawing.Point(9, 73);
            this.checkBoxInsertSlateOnAdMarker.Name = "checkBoxInsertSlateOnAdMarker";
            this.checkBoxInsertSlateOnAdMarker.Size = new System.Drawing.Size(142, 17);
            this.checkBoxInsertSlateOnAdMarker.TabIndex = 70;
            this.checkBoxInsertSlateOnAdMarker.Text = "Insert Slate on Ad Signal";
            this.checkBoxInsertSlateOnAdMarker.UseVisualStyleBackColor = true;
            this.checkBoxInsertSlateOnAdMarker.CheckedChanged += new System.EventHandler(this.checkBoxAdInsertSlate_CheckedChanged);
            this.checkBoxInsertSlateOnAdMarker.Validating += new System.ComponentModel.CancelEventHandler(this.checkBoxAdInsertSlate_Validating);
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
            // comboBoxAdMarkerSource
            // 
            this.comboBoxAdMarkerSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAdMarkerSource.FormattingEnabled = true;
            this.comboBoxAdMarkerSource.Location = new System.Drawing.Point(9, 33);
            this.comboBoxAdMarkerSource.Name = "comboBoxAdMarkerSource";
            this.comboBoxAdMarkerSource.Size = new System.Drawing.Size(200, 21);
            this.comboBoxAdMarkerSource.TabIndex = 12;
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
            // openFileDialogSlate
            // 
            this.openFileDialogSlate.Filter = "Image|*.jpg|All files (*.*)|*.*";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // CreateLiveChannel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(490, 561);
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
            this.tabPageAudioOptions.ResumeLayout(false);
            this.panelAudioControl.ResumeLayout(false);
            this.panelAudioControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAudioStreams)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAudioIndexAddition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAudioIndexMain)).EndInit();
            this.tabPageAdConfig.ResumeLayout(false);
            this.tabPageAdConfig.PerformLayout();
            this.panelInsertSlate.ResumeLayout(false);
            this.panelInsertSlate.PerformLayout();
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
        private System.Windows.Forms.NumericUpDown numericUpDownVideoStreamIndex;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TabPage tabPageAudioOptions;
        private System.Windows.Forms.TabPage tabPageAdConfig;
        private System.Windows.Forms.Panel panelInsertSlate;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox checkBoxInsertSlateOnAdMarker;
        private System.Windows.Forms.CheckBox checkBoxRestrictPreviewIP;
        private System.Windows.Forms.TextBox textBoxRestrictPreviewIP;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button buttonAddAudioStream;
        private System.Windows.Forms.NumericUpDown numericUpDownAudioIndexAddition;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox comboBoxAudioLanguageAddition;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.NumericUpDown numericUpDownAudioIndexMain;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox comboBoxAudioLanguageMain;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button buttonUploadSlate;
        private System.Windows.Forms.DataGridView dataGridViewAudioStreams;
        private System.Windows.Forms.Panel panelAudioControl;
        private System.Windows.Forms.Button buttonDelAddOption;
        private System.Windows.Forms.OpenFileDialog openFileDialogSlate;
        private System.Windows.Forms.ProgressBar progressBarUpload;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox textBoxJPGSearch;
        private ListViewSlateJPG listViewJPG1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}