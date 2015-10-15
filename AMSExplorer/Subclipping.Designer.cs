namespace AMSExplorer
{
    partial class Subclipping
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.moreinfoprofilelink = new System.Windows.Forms.LinkLabel();
            this.textBoxAssetName = new System.Windows.Forms.TextBox();
            this.labelassetname = new System.Windows.Forms.Label();
            this.textBoxAssetDuration = new System.Windows.Forms.TextBox();
            this.labelassetduration = new System.Windows.Forms.Label();
            this.textBoxFilterTimeScale = new System.Windows.Forms.TextBox();
            this.labelAssetTimescale = new System.Windows.Forms.Label();
            this.textBoxOffset = new System.Windows.Forms.TextBox();
            this.labelOffset = new System.Windows.Forms.Label();
            this.tabPageXML = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxConfiguration = new System.Windows.Forms.TextBox();
            this.tabPageTR = new System.Windows.Forms.TabPage();
            this.panelAssetInfo = new System.Windows.Forms.Panel();
            this.groupBoxTrimming = new System.Windows.Forms.GroupBox();
            this.textBoxDurationTime = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.labelAccurate = new System.Windows.Forms.Label();
            this.webBrowserPreview2 = new System.Windows.Forms.WebBrowser();
            this.timeControlStart = new AMSExplorer.TimeControl();
            this.timeControlEnd = new AMSExplorer.TimeControl();
            this.checkBoxPreviewStream = new System.Windows.Forms.CheckBox();
            this.checkBoxTrimming = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.radioButtonAssetFilter = new System.Windows.Forms.RadioButton();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.radioButtonArchiveTopBitrate = new System.Windows.Forms.RadioButton();
            this.radioButtonArchiveAllBitrate = new System.Windows.Forms.RadioButton();
            this.radioButtonClipWithReencode = new System.Windows.Forms.RadioButton();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.labeloutoutputasset = new System.Windows.Forms.Label();
            this.textboxoutputassetname = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxJobName = new System.Windows.Forms.TextBox();
            this.panelJob = new System.Windows.Forms.Panel();
            this.buttonJobOptions = new AMSExplorer.ButtonJobOptions();
            this.label34 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.tabPageXML.SuspendLayout();
            this.tabPageTR.SuspendLayout();
            this.panelAssetInfo.SuspendLayout();
            this.groupBoxTrimming.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.panelJob.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonClose);
            this.panel1.Controls.Add(this.buttonOk);
            this.panel1.Location = new System.Drawing.Point(0, 707);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(915, 55);
            this.panel1.TabIndex = 60;
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.Location = new System.Drawing.Point(784, 14);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(114, 27);
            this.buttonClose.TabIndex = 5;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.Location = new System.Drawing.Point(663, 14);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(114, 27);
            this.buttonOk.TabIndex = 4;
            this.buttonOk.Tag = "Subclip";
            this.buttonOk.Text = "Subclip";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // moreinfoprofilelink
            // 
            this.moreinfoprofilelink.AutoSize = true;
            this.moreinfoprofilelink.Location = new System.Drawing.Point(14, 14);
            this.moreinfoprofilelink.Name = "moreinfoprofilelink";
            this.moreinfoprofilelink.Size = new System.Drawing.Size(101, 15);
            this.moreinfoprofilelink.TabIndex = 80;
            this.moreinfoprofilelink.TabStop = true;
            this.moreinfoprofilelink.Text = "More information";
            this.moreinfoprofilelink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.moreinfoprofilelink_LinkClicked);
            // 
            // textBoxAssetName
            // 
            this.textBoxAssetName.Location = new System.Drawing.Point(27, 45);
            this.textBoxAssetName.Name = "textBoxAssetName";
            this.textBoxAssetName.ReadOnly = true;
            this.textBoxAssetName.Size = new System.Drawing.Size(254, 23);
            this.textBoxAssetName.TabIndex = 83;
            // 
            // labelassetname
            // 
            this.labelassetname.AutoSize = true;
            this.labelassetname.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.labelassetname.Location = new System.Drawing.Point(28, 27);
            this.labelassetname.Name = "labelassetname";
            this.labelassetname.Size = new System.Drawing.Size(74, 15);
            this.labelassetname.TabIndex = 82;
            this.labelassetname.Text = "Asset name :";
            // 
            // textBoxAssetDuration
            // 
            this.textBoxAssetDuration.Location = new System.Drawing.Point(27, 97);
            this.textBoxAssetDuration.Name = "textBoxAssetDuration";
            this.textBoxAssetDuration.ReadOnly = true;
            this.textBoxAssetDuration.Size = new System.Drawing.Size(254, 23);
            this.textBoxAssetDuration.TabIndex = 85;
            this.textBoxAssetDuration.Visible = false;
            // 
            // labelassetduration
            // 
            this.labelassetduration.AutoSize = true;
            this.labelassetduration.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.labelassetduration.Location = new System.Drawing.Point(28, 78);
            this.labelassetduration.Name = "labelassetduration";
            this.labelassetduration.Size = new System.Drawing.Size(89, 15);
            this.labelassetduration.TabIndex = 84;
            this.labelassetduration.Text = "Asset duration :";
            this.labelassetduration.Visible = false;
            // 
            // textBoxFilterTimeScale
            // 
            this.textBoxFilterTimeScale.Location = new System.Drawing.Point(27, 149);
            this.textBoxFilterTimeScale.Name = "textBoxFilterTimeScale";
            this.textBoxFilterTimeScale.ReadOnly = true;
            this.textBoxFilterTimeScale.Size = new System.Drawing.Size(254, 23);
            this.textBoxFilterTimeScale.TabIndex = 87;
            this.textBoxFilterTimeScale.Visible = false;
            // 
            // labelAssetTimescale
            // 
            this.labelAssetTimescale.AutoSize = true;
            this.labelAssetTimescale.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.labelAssetTimescale.Location = new System.Drawing.Point(28, 130);
            this.labelAssetTimescale.Name = "labelAssetTimescale";
            this.labelAssetTimescale.Size = new System.Drawing.Size(94, 15);
            this.labelAssetTimescale.TabIndex = 86;
            this.labelAssetTimescale.Text = "Asset timescale :";
            this.labelAssetTimescale.Visible = false;
            // 
            // textBoxOffset
            // 
            this.textBoxOffset.Location = new System.Drawing.Point(27, 201);
            this.textBoxOffset.Name = "textBoxOffset";
            this.textBoxOffset.ReadOnly = true;
            this.textBoxOffset.Size = new System.Drawing.Size(254, 23);
            this.textBoxOffset.TabIndex = 89;
            this.textBoxOffset.Visible = false;
            // 
            // labelOffset
            // 
            this.labelOffset.AutoSize = true;
            this.labelOffset.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.labelOffset.Location = new System.Drawing.Point(28, 182);
            this.labelOffset.Name = "labelOffset";
            this.labelOffset.Size = new System.Drawing.Size(74, 15);
            this.labelOffset.TabIndex = 88;
            this.labelOffset.Text = "Asset offset :";
            this.labelOffset.Visible = false;
            // 
            // tabPageXML
            // 
            this.tabPageXML.Controls.Add(this.label3);
            this.tabPageXML.Controls.Add(this.textBoxConfiguration);
            this.tabPageXML.Location = new System.Drawing.Point(4, 24);
            this.tabPageXML.Name = "tabPageXML";
            this.tabPageXML.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageXML.Size = new System.Drawing.Size(879, 509);
            this.tabPageXML.TabIndex = 2;
            this.tabPageXML.Text = "Generated JSON Configuration";
            this.tabPageXML.UseVisualStyleBackColor = true;
            this.tabPageXML.Enter += new System.EventHandler(this.tabPageXML_Enter);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 15);
            this.label3.TabIndex = 40;
            this.label3.Text = "JSON (editable):";
            // 
            // textBoxConfiguration
            // 
            this.textBoxConfiguration.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxConfiguration.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxConfiguration.Location = new System.Drawing.Point(22, 35);
            this.textBoxConfiguration.Multiline = true;
            this.textBoxConfiguration.Name = "textBoxConfiguration";
            this.textBoxConfiguration.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxConfiguration.Size = new System.Drawing.Size(833, 440);
            this.textBoxConfiguration.TabIndex = 39;
            // 
            // tabPageTR
            // 
            this.tabPageTR.Controls.Add(this.panelAssetInfo);
            this.tabPageTR.Controls.Add(this.groupBoxTrimming);
            this.tabPageTR.Controls.Add(this.groupBox2);
            this.tabPageTR.Location = new System.Drawing.Point(4, 24);
            this.tabPageTR.Name = "tabPageTR";
            this.tabPageTR.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTR.Size = new System.Drawing.Size(879, 509);
            this.tabPageTR.TabIndex = 0;
            this.tabPageTR.Text = "Settings";
            this.tabPageTR.UseVisualStyleBackColor = true;
            this.tabPageTR.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // panelAssetInfo
            // 
            this.panelAssetInfo.Controls.Add(this.labelassetname);
            this.panelAssetInfo.Controls.Add(this.textBoxAssetDuration);
            this.panelAssetInfo.Controls.Add(this.textBoxOffset);
            this.panelAssetInfo.Controls.Add(this.labelOffset);
            this.panelAssetInfo.Controls.Add(this.textBoxFilterTimeScale);
            this.panelAssetInfo.Controls.Add(this.textBoxAssetName);
            this.panelAssetInfo.Controls.Add(this.labelassetduration);
            this.panelAssetInfo.Controls.Add(this.labelAssetTimescale);
            this.panelAssetInfo.Location = new System.Drawing.Point(1, 260);
            this.panelAssetInfo.Name = "panelAssetInfo";
            this.panelAssetInfo.Size = new System.Drawing.Size(329, 240);
            this.panelAssetInfo.TabIndex = 134;
            // 
            // groupBoxTrimming
            // 
            this.groupBoxTrimming.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxTrimming.Controls.Add(this.textBoxDurationTime);
            this.groupBoxTrimming.Controls.Add(this.label7);
            this.groupBoxTrimming.Controls.Add(this.labelAccurate);
            this.groupBoxTrimming.Controls.Add(this.webBrowserPreview2);
            this.groupBoxTrimming.Controls.Add(this.timeControlStart);
            this.groupBoxTrimming.Controls.Add(this.timeControlEnd);
            this.groupBoxTrimming.Controls.Add(this.checkBoxPreviewStream);
            this.groupBoxTrimming.Controls.Add(this.checkBoxTrimming);
            this.groupBoxTrimming.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.groupBoxTrimming.Location = new System.Drawing.Point(337, 7);
            this.groupBoxTrimming.Name = "groupBoxTrimming";
            this.groupBoxTrimming.Size = new System.Drawing.Size(537, 491);
            this.groupBoxTrimming.TabIndex = 131;
            this.groupBoxTrimming.TabStop = false;
            this.groupBoxTrimming.Text = "Trimming";
            // 
            // textBoxDurationTime
            // 
            this.textBoxDurationTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDurationTime.Enabled = false;
            this.textBoxDurationTime.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBoxDurationTime.Location = new System.Drawing.Point(394, 462);
            this.textBoxDurationTime.Name = "textBoxDurationTime";
            this.textBoxDurationTime.ReadOnly = true;
            this.textBoxDurationTime.Size = new System.Drawing.Size(123, 23);
            this.textBoxDurationTime.TabIndex = 135;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label7.Location = new System.Drawing.Point(329, 465);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 15);
            this.label7.TabIndex = 134;
            this.label7.Text = "Duration :";
            // 
            // labelAccurate
            // 
            this.labelAccurate.AutoSize = true;
            this.labelAccurate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelAccurate.Location = new System.Drawing.Point(208, 23);
            this.labelAccurate.Name = "labelAccurate";
            this.labelAccurate.Size = new System.Drawing.Size(77, 15);
            this.labelAccurate.TabIndex = 133;
            this.labelAccurate.Tag = "({0} accurate)";
            this.labelAccurate.Text = "({0} accurate)";
            // 
            // webBrowserPreview2
            // 
            this.webBrowserPreview2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowserPreview2.Location = new System.Drawing.Point(17, 42);
            this.webBrowserPreview2.MinimumSize = new System.Drawing.Size(23, 23);
            this.webBrowserPreview2.Name = "webBrowserPreview2";
            this.webBrowserPreview2.Size = new System.Drawing.Size(500, 217);
            this.webBrowserPreview2.TabIndex = 4;
            // 
            // timeControlStart
            // 
            this.timeControlStart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.timeControlStart.BackColor = System.Drawing.SystemColors.Window;
            this.timeControlStart.DisplayTrackBar = true;
            this.timeControlStart.Enabled = false;
            this.timeControlStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeControlStart.Label1 = "";
            this.timeControlStart.Label2 = "Start time :";
            this.timeControlStart.Location = new System.Drawing.Point(7, 286);
            this.timeControlStart.Max = System.TimeSpan.Parse("10675199.02:48:05.4775807");
            this.timeControlStart.Min = System.TimeSpan.Parse("00:00:00");
            this.timeControlStart.Name = "timeControlStart";
            this.timeControlStart.ScaledFirstTimestampOffset = ((ulong)(0ul));
            this.timeControlStart.Size = new System.Drawing.Size(511, 92);
            this.timeControlStart.TabIndex = 122;
            this.timeControlStart.TimeScale = null;
            this.timeControlStart.TotalDuration = System.TimeSpan.Parse("00:00:00");
            this.timeControlStart.ValueChanged += new System.EventHandler(this.timeControlStart_ValueChanged);
            // 
            // timeControlEnd
            // 
            this.timeControlEnd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.timeControlEnd.BackColor = System.Drawing.SystemColors.Window;
            this.timeControlEnd.DisplayTrackBar = true;
            this.timeControlEnd.Enabled = false;
            this.timeControlEnd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeControlEnd.Label1 = "";
            this.timeControlEnd.Label2 = "End time :";
            this.timeControlEnd.Location = new System.Drawing.Point(7, 382);
            this.timeControlEnd.Max = System.TimeSpan.Parse("10675199.02:48:05.4775807");
            this.timeControlEnd.Min = System.TimeSpan.Parse("00:00:00");
            this.timeControlEnd.Name = "timeControlEnd";
            this.timeControlEnd.ScaledFirstTimestampOffset = ((ulong)(0ul));
            this.timeControlEnd.Size = new System.Drawing.Size(511, 87);
            this.timeControlEnd.TabIndex = 105;
            this.timeControlEnd.TimeScale = null;
            this.timeControlEnd.TotalDuration = System.TimeSpan.Parse("00:00:00");
            this.timeControlEnd.ValueChanged += new System.EventHandler(this.timeControlEnd_ValueChanged);
            // 
            // checkBoxPreviewStream
            // 
            this.checkBoxPreviewStream.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxPreviewStream.AutoSize = true;
            this.checkBoxPreviewStream.Checked = true;
            this.checkBoxPreviewStream.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxPreviewStream.Enabled = false;
            this.checkBoxPreviewStream.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.checkBoxPreviewStream.Location = new System.Drawing.Point(416, 23);
            this.checkBoxPreviewStream.Name = "checkBoxPreviewStream";
            this.checkBoxPreviewStream.Size = new System.Drawing.Size(102, 19);
            this.checkBoxPreviewStream.TabIndex = 5;
            this.checkBoxPreviewStream.Text = "Playback asset";
            this.checkBoxPreviewStream.UseVisualStyleBackColor = true;
            this.checkBoxPreviewStream.CheckedChanged += new System.EventHandler(this.checkBoxPreviewStream_CheckedChanged_1);
            // 
            // checkBoxTrimming
            // 
            this.checkBoxTrimming.AutoSize = true;
            this.checkBoxTrimming.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.checkBoxTrimming.Location = new System.Drawing.Point(17, 22);
            this.checkBoxTrimming.Name = "checkBoxTrimming";
            this.checkBoxTrimming.Size = new System.Drawing.Size(173, 19);
            this.checkBoxTrimming.TabIndex = 132;
            this.checkBoxTrimming.Text = "Trim the live archive/stream";
            this.checkBoxTrimming.UseVisualStyleBackColor = true;
            this.checkBoxTrimming.CheckedChanged += new System.EventHandler(this.checkBoxTrimming_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.radioButtonAssetFilter);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.radioButtonArchiveTopBitrate);
            this.groupBox2.Controls.Add(this.radioButtonArchiveAllBitrate);
            this.groupBox2.Controls.Add(this.radioButtonClipWithReencode);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(7, 7);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(323, 246);
            this.groupBox2.TabIndex = 133;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Subclipping mode";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label2.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label2.Location = new System.Drawing.Point(43, 212);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 15);
            this.label2.TabIndex = 137;
            this.label2.Text = "create an asset filter";
            // 
            // radioButtonAssetFilter
            // 
            this.radioButtonAssetFilter.AutoSize = true;
            this.radioButtonAssetFilter.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.radioButtonAssetFilter.Location = new System.Drawing.Point(21, 189);
            this.radioButtonAssetFilter.Name = "radioButtonAssetFilter";
            this.radioButtonAssetFilter.Size = new System.Drawing.Size(75, 19);
            this.radioButtonAssetFilter.TabIndex = 136;
            this.radioButtonAssetFilter.Text = "Trim only";
            this.radioButtonAssetFilter.UseVisualStyleBackColor = true;
            this.radioButtonAssetFilter.CheckedChanged += new System.EventHandler(this.radioButtonClipWithReencode_CheckedChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label12.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label12.Location = new System.Drawing.Point(43, 159);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(219, 15);
            this.label12.TabIndex = 135;
            this.label12.Text = "with any Media Encoder Standard preset";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label11.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label11.Location = new System.Drawing.Point(43, 106);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(116, 15);
            this.label11.TabIndex = 134;
            this.label11.Text = "to multiple MP4 files";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label10.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label10.Location = new System.Drawing.Point(43, 53);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(107, 15);
            this.label10.TabIndex = 133;
            this.label10.Text = "to a single MP4 file";
            // 
            // radioButtonArchiveTopBitrate
            // 
            this.radioButtonArchiveTopBitrate.AutoSize = true;
            this.radioButtonArchiveTopBitrate.Checked = true;
            this.radioButtonArchiveTopBitrate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.radioButtonArchiveTopBitrate.Location = new System.Drawing.Point(21, 30);
            this.radioButtonArchiveTopBitrate.Name = "radioButtonArchiveTopBitrate";
            this.radioButtonArchiveTopBitrate.Size = new System.Drawing.Size(125, 19);
            this.radioButtonArchiveTopBitrate.TabIndex = 126;
            this.radioButtonArchiveTopBitrate.TabStop = true;
            this.radioButtonArchiveTopBitrate.Text = "Archive Top Bitrate";
            this.radioButtonArchiveTopBitrate.UseVisualStyleBackColor = true;
            this.radioButtonArchiveTopBitrate.CheckedChanged += new System.EventHandler(this.radioButtonClipWithReencode_CheckedChanged);
            // 
            // radioButtonArchiveAllBitrate
            // 
            this.radioButtonArchiveAllBitrate.AutoSize = true;
            this.radioButtonArchiveAllBitrate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.radioButtonArchiveAllBitrate.Location = new System.Drawing.Point(21, 83);
            this.radioButtonArchiveAllBitrate.Name = "radioButtonArchiveAllBitrate";
            this.radioButtonArchiveAllBitrate.Size = new System.Drawing.Size(124, 19);
            this.radioButtonArchiveAllBitrate.TabIndex = 128;
            this.radioButtonArchiveAllBitrate.Text = "Archive All Bitrates";
            this.radioButtonArchiveAllBitrate.UseVisualStyleBackColor = true;
            this.radioButtonArchiveAllBitrate.CheckedChanged += new System.EventHandler(this.radioButtonClipWithReencode_CheckedChanged);
            // 
            // radioButtonClipWithReencode
            // 
            this.radioButtonClipWithReencode.AutoSize = true;
            this.radioButtonClipWithReencode.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.radioButtonClipWithReencode.Location = new System.Drawing.Point(21, 136);
            this.radioButtonClipWithReencode.Name = "radioButtonClipWithReencode";
            this.radioButtonClipWithReencode.Size = new System.Drawing.Size(137, 19);
            this.radioButtonClipWithReencode.TabIndex = 129;
            this.radioButtonClipWithReencode.Text = "Reencode Top Bitrate";
            this.radioButtonClipWithReencode.UseVisualStyleBackColor = true;
            this.radioButtonClipWithReencode.CheckedChanged += new System.EventHandler(this.radioButtonClipWithReencode_CheckedChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPageTR);
            this.tabControl1.Controls.Add(this.tabPageXML);
            this.tabControl1.Location = new System.Drawing.Point(14, 42);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(887, 537);
            this.tabControl1.TabIndex = 78;
            // 
            // labeloutoutputasset
            // 
            this.labeloutoutputasset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labeloutoutputasset.AutoSize = true;
            this.labeloutoutputasset.Location = new System.Drawing.Point(31, 70);
            this.labeloutoutputasset.Name = "labeloutoutputasset";
            this.labeloutoutputasset.Size = new System.Drawing.Size(126, 15);
            this.labeloutoutputasset.TabIndex = 134;
            this.labeloutoutputasset.Tag = "Output asset(s) name :";
            this.labeloutoutputasset.Text = "Output asset(s) name :";
            // 
            // textboxoutputassetname
            // 
            this.textboxoutputassetname.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textboxoutputassetname.Location = new System.Drawing.Point(34, 89);
            this.textboxoutputassetname.Name = "textboxoutputassetname";
            this.textboxoutputassetname.Size = new System.Drawing.Size(490, 23);
            this.textboxoutputassetname.TabIndex = 133;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(31, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 15);
            this.label5.TabIndex = 132;
            this.label5.Text = "Job(s) name :";
            // 
            // textBoxJobName
            // 
            this.textBoxJobName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxJobName.Location = new System.Drawing.Point(34, 36);
            this.textBoxJobName.Name = "textBoxJobName";
            this.textBoxJobName.Size = new System.Drawing.Size(492, 23);
            this.textBoxJobName.TabIndex = 131;
            // 
            // panelJob
            // 
            this.panelJob.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelJob.Controls.Add(this.textboxoutputassetname);
            this.panelJob.Controls.Add(this.textBoxJobName);
            this.panelJob.Controls.Add(this.buttonJobOptions);
            this.panelJob.Controls.Add(this.labeloutoutputasset);
            this.panelJob.Controls.Add(this.label5);
            this.panelJob.Location = new System.Drawing.Point(0, 585);
            this.panelJob.Name = "panelJob";
            this.panelJob.Size = new System.Drawing.Size(915, 115);
            this.panelJob.TabIndex = 135;
            // 
            // buttonJobOptions
            // 
            this.buttonJobOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonJobOptions.Location = new System.Drawing.Point(736, 33);
            this.buttonJobOptions.Name = "buttonJobOptions";
            this.buttonJobOptions.Size = new System.Drawing.Size(160, 27);
            this.buttonJobOptions.TabIndex = 130;
            this.buttonJobOptions.Text = "Job options...";
            this.buttonJobOptions.UseVisualStyleBackColor = true;
            // 
            // label34
            // 
            this.label34.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label34.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(128)))), ((int)(((byte)(171)))));
            this.label34.Location = new System.Drawing.Point(601, 14);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(295, 25);
            this.label34.TabIndex = 136;
            this.label34.Text = "Live stream/archive Subclipping";
            this.label34.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // Subclipping
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonClose;
            this.ClientSize = new System.Drawing.Size(915, 763);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label34);
            this.Controls.Add(this.panelJob);
            this.Controls.Add(this.moreinfoprofilelink);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Subclipping";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Live Subclipping";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Subclipping_FormClosed);
            this.Load += new System.EventHandler(this.Subclipping_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.tabPageXML.ResumeLayout(false);
            this.tabPageXML.PerformLayout();
            this.tabPageTR.ResumeLayout(false);
            this.panelAssetInfo.ResumeLayout(false);
            this.panelAssetInfo.PerformLayout();
            this.groupBoxTrimming.ResumeLayout(false);
            this.groupBoxTrimming.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.panelJob.ResumeLayout(false);
            this.panelJob.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.LinkLabel moreinfoprofilelink;
        private System.Windows.Forms.TextBox textBoxAssetName;
        private System.Windows.Forms.Label labelassetname;
        private System.Windows.Forms.TextBox textBoxAssetDuration;
        private System.Windows.Forms.Label labelassetduration;
        private System.Windows.Forms.TextBox textBoxFilterTimeScale;
        private System.Windows.Forms.Label labelAssetTimescale;
        private System.Windows.Forms.TextBox textBoxOffset;
        private System.Windows.Forms.Label labelOffset;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageTR;
        private System.Windows.Forms.RadioButton radioButtonClipWithReencode;
        private System.Windows.Forms.RadioButton radioButtonArchiveAllBitrate;
        private System.Windows.Forms.RadioButton radioButtonArchiveTopBitrate;
        private TimeControl timeControlStart;
        private TimeControl timeControlEnd;
        private System.Windows.Forms.TabPage tabPageXML;
        private ButtonJobOptions buttonJobOptions;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkBoxTrimming;
        private System.Windows.Forms.GroupBox groupBoxTrimming;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        public System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxConfiguration;
        public System.Windows.Forms.Label labeloutoutputasset;
        private System.Windows.Forms.TextBox textboxoutputassetname;
        public System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxJobName;
        private System.Windows.Forms.Panel panelJob;
        public System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label labelAccurate;
        private System.Windows.Forms.CheckBox checkBoxPreviewStream;
        private System.Windows.Forms.WebBrowser webBrowserPreview2;
        private System.Windows.Forms.Panel panelAssetInfo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton radioButtonAssetFilter;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.TextBox textBoxDurationTime;
        public System.Windows.Forms.Label label7;
    }
}