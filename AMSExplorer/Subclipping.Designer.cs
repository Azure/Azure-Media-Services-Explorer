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
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.textBoxFilterName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.checkBoxStartTime = new System.Windows.Forms.CheckBox();
            this.checkBoxEndTime = new System.Windows.Forms.CheckBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.moreinfoprofilelink = new System.Windows.Forms.LinkLabel();
            this.labelFilterTitle = new System.Windows.Forms.Label();
            this.textBoxAssetName = new System.Windows.Forms.TextBox();
            this.labelassetname = new System.Windows.Forms.Label();
            this.textBoxAssetDuration = new System.Windows.Forms.TextBox();
            this.labelassetduration = new System.Windows.Forms.Label();
            this.textBoxFilterTimeScale = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxOffset = new System.Windows.Forms.TextBox();
            this.labelOffset = new System.Windows.Forms.Label();
            this.checkBoxRawMode = new System.Windows.Forms.CheckBox();
            this.tabPageTRRaw = new System.Windows.Forms.TabPage();
            this.textBoxRawTimescale = new System.Windows.Forms.TextBox();
            this.textBoxRawBackoff = new System.Windows.Forms.TextBox();
            this.textBoxRawDVR = new System.Windows.Forms.TextBox();
            this.textBoxRawEnd = new System.Windows.Forms.TextBox();
            this.textBoxRawStart = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPageTR = new System.Windows.Forms.TabPage();
            this.radioButtonClipWithReencode = new System.Windows.Forms.RadioButton();
            this.radioButtonClipAllBitrates = new System.Windows.Forms.RadioButton();
            this.radioButtonClipTopBitrate = new System.Windows.Forms.RadioButton();
            this.radioButtonArchiveTopBitrate = new System.Windows.Forms.RadioButton();
            this.labelDefaultEnd = new System.Windows.Forms.Label();
            this.labelStartTimeDefault = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.timeControlStart = new AMSExplorer.TimeControl();
            this.timeControlEnd = new AMSExplorer.TimeControl();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.buttonJobOptions = new AMSExplorer.ButtonJobOptions();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.tabPageTRRaw.SuspendLayout();
            this.tabPageTR.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonOk);
            this.panel1.Location = new System.Drawing.Point(0, 613);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1008, 48);
            this.panel1.TabIndex = 60;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(896, 12);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(98, 23);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Enabled = false;
            this.buttonOk.Location = new System.Drawing.Point(792, 12);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(98, 23);
            this.buttonOk.TabIndex = 4;
            this.buttonOk.Text = "Create Filter";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // textBoxFilterName
            // 
            this.textBoxFilterName.Location = new System.Drawing.Point(12, 55);
            this.textBoxFilterName.Name = "textBoxFilterName";
            this.textBoxFilterName.Size = new System.Drawing.Size(239, 20);
            this.textBoxFilterName.TabIndex = 62;
            this.textBoxFilterName.TextChanged += new System.EventHandler(this.textBoxFilterName_TextChanged);
            this.textBoxFilterName.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxFilterName_Validating);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 13);
            this.label4.TabIndex = 61;
            this.label4.Text = "Filter name :";
            // 
            // checkBoxStartTime
            // 
            this.checkBoxStartTime.AutoSize = true;
            this.checkBoxStartTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxStartTime.Location = new System.Drawing.Point(334, 160);
            this.checkBoxStartTime.Name = "checkBoxStartTime";
            this.checkBoxStartTime.Size = new System.Drawing.Size(88, 17);
            this.checkBoxStartTime.TabIndex = 103;
            this.checkBoxStartTime.Text = "Start time :\r";
            this.toolTip1.SetToolTip(this.checkBoxStartTime, "Live and VOD. Value rounded to the closest next GOP start.");
            this.checkBoxStartTime.UseVisualStyleBackColor = true;
            this.checkBoxStartTime.CheckedChanged += new System.EventHandler(this.checkBoxStartTime_CheckedChanged);
            // 
            // checkBoxEndTime
            // 
            this.checkBoxEndTime.AutoSize = true;
            this.checkBoxEndTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxEndTime.Location = new System.Drawing.Point(334, 262);
            this.checkBoxEndTime.Name = "checkBoxEndTime";
            this.checkBoxEndTime.Size = new System.Drawing.Size(83, 17);
            this.checkBoxEndTime.TabIndex = 104;
            this.checkBoxEndTime.Text = "End time :\r\n";
            this.toolTip1.SetToolTip(this.checkBoxEndTime, "VOD (ignored for Live but applies to archive). Value rounded to the closest next " +
        "GOP start.");
            this.checkBoxEndTime.UseVisualStyleBackColor = true;
            this.checkBoxEndTime.CheckedChanged += new System.EventHandler(this.checkBoxEndTime_CheckedChanged);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // moreinfoprofilelink
            // 
            this.moreinfoprofilelink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.moreinfoprofilelink.AutoSize = true;
            this.moreinfoprofilelink.Location = new System.Drawing.Point(911, 14);
            this.moreinfoprofilelink.Name = "moreinfoprofilelink";
            this.moreinfoprofilelink.Size = new System.Drawing.Size(85, 13);
            this.moreinfoprofilelink.TabIndex = 80;
            this.moreinfoprofilelink.TabStop = true;
            this.moreinfoprofilelink.Text = "More information";
            this.moreinfoprofilelink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.moreinfoprofilelink_LinkClicked);
            // 
            // labelFilterTitle
            // 
            this.labelFilterTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelFilterTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFilterTitle.Location = new System.Drawing.Point(8, 9);
            this.labelFilterTitle.Name = "labelFilterTitle";
            this.labelFilterTitle.Size = new System.Drawing.Size(882, 18);
            this.labelFilterTitle.TabIndex = 81;
            this.labelFilterTitle.Text = "Global Filter";
            // 
            // textBoxAssetName
            // 
            this.textBoxAssetName.Location = new System.Drawing.Point(384, 55);
            this.textBoxAssetName.Name = "textBoxAssetName";
            this.textBoxAssetName.ReadOnly = true;
            this.textBoxAssetName.Size = new System.Drawing.Size(215, 20);
            this.textBoxAssetName.TabIndex = 83;
            this.textBoxAssetName.Visible = false;
            // 
            // labelassetname
            // 
            this.labelassetname.AutoSize = true;
            this.labelassetname.Location = new System.Drawing.Point(381, 39);
            this.labelassetname.Name = "labelassetname";
            this.labelassetname.Size = new System.Drawing.Size(68, 13);
            this.labelassetname.TabIndex = 82;
            this.labelassetname.Text = "Asset name :";
            this.labelassetname.Visible = false;
            // 
            // textBoxAssetDuration
            // 
            this.textBoxAssetDuration.Location = new System.Drawing.Point(605, 55);
            this.textBoxAssetDuration.Name = "textBoxAssetDuration";
            this.textBoxAssetDuration.ReadOnly = true;
            this.textBoxAssetDuration.Size = new System.Drawing.Size(114, 20);
            this.textBoxAssetDuration.TabIndex = 85;
            this.textBoxAssetDuration.Visible = false;
            // 
            // labelassetduration
            // 
            this.labelassetduration.AutoSize = true;
            this.labelassetduration.Location = new System.Drawing.Point(602, 39);
            this.labelassetduration.Name = "labelassetduration";
            this.labelassetduration.Size = new System.Drawing.Size(80, 13);
            this.labelassetduration.TabIndex = 84;
            this.labelassetduration.Text = "Asset duration :";
            this.labelassetduration.Visible = false;
            // 
            // textBoxFilterTimeScale
            // 
            this.textBoxFilterTimeScale.Location = new System.Drawing.Point(280, 55);
            this.textBoxFilterTimeScale.Name = "textBoxFilterTimeScale";
            this.textBoxFilterTimeScale.ReadOnly = true;
            this.textBoxFilterTimeScale.Size = new System.Drawing.Size(98, 20);
            this.textBoxFilterTimeScale.TabIndex = 87;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(280, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 86;
            this.label2.Text = "Filter timescale :";
            // 
            // textBoxOffset
            // 
            this.textBoxOffset.Location = new System.Drawing.Point(725, 55);
            this.textBoxOffset.Name = "textBoxOffset";
            this.textBoxOffset.ReadOnly = true;
            this.textBoxOffset.Size = new System.Drawing.Size(106, 20);
            this.textBoxOffset.TabIndex = 89;
            this.textBoxOffset.Visible = false;
            // 
            // labelOffset
            // 
            this.labelOffset.AutoSize = true;
            this.labelOffset.Location = new System.Drawing.Point(722, 39);
            this.labelOffset.Name = "labelOffset";
            this.labelOffset.Size = new System.Drawing.Size(68, 13);
            this.labelOffset.TabIndex = 88;
            this.labelOffset.Text = "Asset offset :";
            this.labelOffset.Visible = false;
            // 
            // checkBoxRawMode
            // 
            this.checkBoxRawMode.AutoSize = true;
            this.checkBoxRawMode.Location = new System.Drawing.Point(857, 57);
            this.checkBoxRawMode.Name = "checkBoxRawMode";
            this.checkBoxRawMode.Size = new System.Drawing.Size(133, 17);
            this.checkBoxRawMode.TabIndex = 90;
            this.checkBoxRawMode.Text = "Time range Raw mode";
            this.checkBoxRawMode.UseVisualStyleBackColor = true;
            this.checkBoxRawMode.CheckedChanged += new System.EventHandler(this.checkBoxRawMode_CheckedChanged);
            // 
            // tabPageTRRaw
            // 
            this.tabPageTRRaw.Controls.Add(this.textBoxRawTimescale);
            this.tabPageTRRaw.Controls.Add(this.textBoxRawBackoff);
            this.tabPageTRRaw.Controls.Add(this.textBoxRawDVR);
            this.tabPageTRRaw.Controls.Add(this.textBoxRawEnd);
            this.tabPageTRRaw.Controls.Add(this.textBoxRawStart);
            this.tabPageTRRaw.Controls.Add(this.label9);
            this.tabPageTRRaw.Controls.Add(this.label8);
            this.tabPageTRRaw.Controls.Add(this.label7);
            this.tabPageTRRaw.Controls.Add(this.label5);
            this.tabPageTRRaw.Controls.Add(this.label3);
            this.tabPageTRRaw.Location = new System.Drawing.Point(4, 22);
            this.tabPageTRRaw.Name = "tabPageTRRaw";
            this.tabPageTRRaw.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTRRaw.Size = new System.Drawing.Size(974, 483);
            this.tabPageTRRaw.TabIndex = 2;
            this.tabPageTRRaw.Text = "Time Range (Raw)";
            this.tabPageTRRaw.UseVisualStyleBackColor = true;
            // 
            // textBoxRawTimescale
            // 
            this.textBoxRawTimescale.Location = new System.Drawing.Point(186, 27);
            this.textBoxRawTimescale.Name = "textBoxRawTimescale";
            this.textBoxRawTimescale.Size = new System.Drawing.Size(239, 20);
            this.textBoxRawTimescale.TabIndex = 120;
            // 
            // textBoxRawBackoff
            // 
            this.textBoxRawBackoff.Location = new System.Drawing.Point(186, 251);
            this.textBoxRawBackoff.Name = "textBoxRawBackoff";
            this.textBoxRawBackoff.Size = new System.Drawing.Size(239, 20);
            this.textBoxRawBackoff.TabIndex = 118;
            // 
            // textBoxRawDVR
            // 
            this.textBoxRawDVR.Location = new System.Drawing.Point(186, 195);
            this.textBoxRawDVR.Name = "textBoxRawDVR";
            this.textBoxRawDVR.Size = new System.Drawing.Size(239, 20);
            this.textBoxRawDVR.TabIndex = 117;
            // 
            // textBoxRawEnd
            // 
            this.textBoxRawEnd.Location = new System.Drawing.Point(186, 139);
            this.textBoxRawEnd.Name = "textBoxRawEnd";
            this.textBoxRawEnd.Size = new System.Drawing.Size(239, 20);
            this.textBoxRawEnd.TabIndex = 116;
            // 
            // textBoxRawStart
            // 
            this.textBoxRawStart.Location = new System.Drawing.Point(186, 83);
            this.textBoxRawStart.Name = "textBoxRawStart";
            this.textBoxRawStart.Size = new System.Drawing.Size(239, 20);
            this.textBoxRawStart.TabIndex = 91;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(20, 30);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(72, 13);
            this.label9.TabIndex = 119;
            this.label9.Text = "Timescale :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(23, 254);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(87, 13);
            this.label8.TabIndex = 115;
            this.label8.Text = "Live Backoff :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(23, 198);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(90, 13);
            this.label7.TabIndex = 114;
            this.label7.Text = "DVR Window :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(23, 142);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(102, 13);
            this.label5.TabIndex = 113;
            this.label5.Text = "End time (VOD) :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(20, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(160, 13);
            this.label3.TabIndex = 91;
            this.label3.Text = "Start time (Live and VOD) :";
            // 
            // tabPageTR
            // 
            this.tabPageTR.Controls.Add(this.buttonJobOptions);
            this.tabPageTR.Controls.Add(this.radioButtonClipWithReencode);
            this.tabPageTR.Controls.Add(this.radioButtonClipAllBitrates);
            this.tabPageTR.Controls.Add(this.radioButtonClipTopBitrate);
            this.tabPageTR.Controls.Add(this.radioButtonArchiveTopBitrate);
            this.tabPageTR.Controls.Add(this.labelDefaultEnd);
            this.tabPageTR.Controls.Add(this.labelStartTimeDefault);
            this.tabPageTR.Controls.Add(this.pictureBox3);
            this.tabPageTR.Controls.Add(this.timeControlStart);
            this.tabPageTR.Controls.Add(this.timeControlEnd);
            this.tabPageTR.Controls.Add(this.checkBoxEndTime);
            this.tabPageTR.Controls.Add(this.checkBoxStartTime);
            this.tabPageTR.Location = new System.Drawing.Point(4, 22);
            this.tabPageTR.Name = "tabPageTR";
            this.tabPageTR.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTR.Size = new System.Drawing.Size(974, 483);
            this.tabPageTR.TabIndex = 0;
            this.tabPageTR.Text = "Time Range";
            this.tabPageTR.UseVisualStyleBackColor = true;
            this.tabPageTR.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // radioButtonClipWithReencode
            // 
            this.radioButtonClipWithReencode.AutoSize = true;
            this.radioButtonClipWithReencode.Location = new System.Drawing.Point(73, 92);
            this.radioButtonClipWithReencode.Name = "radioButtonClipWithReencode";
            this.radioButtonClipWithReencode.Size = new System.Drawing.Size(112, 17);
            this.radioButtonClipWithReencode.TabIndex = 129;
            this.radioButtonClipWithReencode.Text = "Clip with reencode";
            this.radioButtonClipWithReencode.UseVisualStyleBackColor = true;
            // 
            // radioButtonClipAllBitrates
            // 
            this.radioButtonClipAllBitrates.AutoSize = true;
            this.radioButtonClipAllBitrates.Location = new System.Drawing.Point(73, 69);
            this.radioButtonClipAllBitrates.Name = "radioButtonClipAllBitrates";
            this.radioButtonClipAllBitrates.Size = new System.Drawing.Size(94, 17);
            this.radioButtonClipAllBitrates.TabIndex = 128;
            this.radioButtonClipAllBitrates.Text = "Clip All Bitrates";
            this.radioButtonClipAllBitrates.UseVisualStyleBackColor = true;
            // 
            // radioButtonClipTopBitrate
            // 
            this.radioButtonClipTopBitrate.AutoSize = true;
            this.radioButtonClipTopBitrate.Location = new System.Drawing.Point(73, 46);
            this.radioButtonClipTopBitrate.Name = "radioButtonClipTopBitrate";
            this.radioButtonClipTopBitrate.Size = new System.Drawing.Size(97, 17);
            this.radioButtonClipTopBitrate.TabIndex = 127;
            this.radioButtonClipTopBitrate.Text = "Clip Top Bitrate";
            this.radioButtonClipTopBitrate.UseVisualStyleBackColor = true;
            // 
            // radioButtonArchiveTopBitrate
            // 
            this.radioButtonArchiveTopBitrate.AutoSize = true;
            this.radioButtonArchiveTopBitrate.Checked = true;
            this.radioButtonArchiveTopBitrate.Location = new System.Drawing.Point(73, 23);
            this.radioButtonArchiveTopBitrate.Name = "radioButtonArchiveTopBitrate";
            this.radioButtonArchiveTopBitrate.Size = new System.Drawing.Size(114, 17);
            this.radioButtonArchiveTopBitrate.TabIndex = 126;
            this.radioButtonArchiveTopBitrate.TabStop = true;
            this.radioButtonArchiveTopBitrate.Text = "Archve Top Bitrate";
            this.radioButtonArchiveTopBitrate.UseVisualStyleBackColor = true;
            // 
            // labelDefaultEnd
            // 
            this.labelDefaultEnd.AutoSize = true;
            this.labelDefaultEnd.Location = new System.Drawing.Point(353, 295);
            this.labelDefaultEnd.Name = "labelDefaultEnd";
            this.labelDefaultEnd.Size = new System.Drawing.Size(73, 13);
            this.labelDefaultEnd.TabIndex = 123;
            this.labelDefaultEnd.Tag = "(Default: Max)";
            this.labelDefaultEnd.Text = "(Default: Max)";
            // 
            // labelStartTimeDefault
            // 
            this.labelStartTimeDefault.AutoSize = true;
            this.labelStartTimeDefault.Location = new System.Drawing.Point(354, 193);
            this.labelStartTimeDefault.Name = "labelStartTimeDefault";
            this.labelStartTimeDefault.Size = new System.Drawing.Size(62, 13);
            this.labelStartTimeDefault.TabIndex = 86;
            this.labelStartTimeDefault.Tag = "(Default : 0)";
            this.labelStartTimeDefault.Text = "(Default : 0)";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::AMSExplorer.Bitmaps.media_services_subclips_filter;
            this.pictureBox3.Location = new System.Drawing.Point(14, 188);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(309, 74);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 99;
            this.pictureBox3.TabStop = false;
            // 
            // timeControlStart
            // 
            this.timeControlStart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.timeControlStart.BackColor = System.Drawing.SystemColors.Window;
            this.timeControlStart.DisplayTrackBar = false;
            this.timeControlStart.DVRMode = false;
            this.timeControlStart.Enabled = false;
            this.timeControlStart.Location = new System.Drawing.Point(453, 145);
            this.timeControlStart.Max = System.TimeSpan.Parse("10675199.02:48:05.4775807");
            this.timeControlStart.Min = System.TimeSpan.Parse("00:00:00");
            this.timeControlStart.Name = "timeControlStart";
            this.timeControlStart.ScaledFirstTimestampOffset = ((long)(0));
            this.timeControlStart.ScaledTotalDuration = ((long)(-1));
            this.timeControlStart.Size = new System.Drawing.Size(503, 95);
            this.timeControlStart.TabIndex = 122;
            this.timeControlStart.TimeScale = ((long)(10000000));
            this.timeControlStart.ValueChanged += new System.EventHandler(this.timeControlStart_ValueChanged);
            // 
            // timeControlEnd
            // 
            this.timeControlEnd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.timeControlEnd.BackColor = System.Drawing.SystemColors.Window;
            this.timeControlEnd.DisplayTrackBar = false;
            this.timeControlEnd.DVRMode = false;
            this.timeControlEnd.Enabled = false;
            this.timeControlEnd.Location = new System.Drawing.Point(453, 247);
            this.timeControlEnd.Max = System.TimeSpan.Parse("10675199.02:48:05.4775807");
            this.timeControlEnd.Min = System.TimeSpan.Parse("00:00:00");
            this.timeControlEnd.Name = "timeControlEnd";
            this.timeControlEnd.ScaledFirstTimestampOffset = ((long)(0));
            this.timeControlEnd.ScaledTotalDuration = ((long)(-1));
            this.timeControlEnd.Size = new System.Drawing.Size(503, 80);
            this.timeControlEnd.TabIndex = 105;
            this.timeControlEnd.TimeScale = ((long)(10000000));
            this.timeControlEnd.ValueChanged += new System.EventHandler(this.timeControlEnd_ValueChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPageTR);
            this.tabControl1.Controls.Add(this.tabPageTRRaw);
            this.tabControl1.Location = new System.Drawing.Point(12, 91);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(982, 509);
            this.tabControl1.TabIndex = 78;
            // 
            // buttonJobOptions
            // 
            this.buttonJobOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonJobOptions.Location = new System.Drawing.Point(819, 23);
            this.buttonJobOptions.Name = "buttonJobOptions";
            this.buttonJobOptions.Size = new System.Drawing.Size(137, 23);
            this.buttonJobOptions.TabIndex = 130;
            this.buttonJobOptions.Text = "Job options...";
            this.buttonJobOptions.UseVisualStyleBackColor = true;
            // 
            // Subclipping
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1008, 661);
            this.Controls.Add(this.checkBoxRawMode);
            this.Controls.Add(this.textBoxOffset);
            this.Controls.Add(this.labelOffset);
            this.Controls.Add(this.textBoxFilterTimeScale);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxAssetDuration);
            this.Controls.Add(this.labelassetduration);
            this.Controls.Add(this.textBoxAssetName);
            this.Controls.Add(this.labelassetname);
            this.Controls.Add(this.labelFilterTitle);
            this.Controls.Add(this.moreinfoprofilelink);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.textBoxFilterName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.panel1);
            this.Name = "Subclipping";
            this.Text = "Dynamic Manifest Filter";
            this.Load += new System.EventHandler(this.Subclipping_Load);
            this.Shown += new System.EventHandler(this.DynManifestFilter_Shown);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.tabPageTRRaw.ResumeLayout(false);
            this.tabPageTRRaw.PerformLayout();
            this.tabPageTR.ResumeLayout(false);
            this.tabPageTR.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Button buttonCancel;
        public System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.TextBox textBoxFilterName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.LinkLabel moreinfoprofilelink;
        private System.Windows.Forms.Label labelFilterTitle;
        private System.Windows.Forms.TextBox textBoxAssetName;
        private System.Windows.Forms.Label labelassetname;
        private System.Windows.Forms.TextBox textBoxAssetDuration;
        private System.Windows.Forms.Label labelassetduration;
        private System.Windows.Forms.TextBox textBoxFilterTimeScale;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxOffset;
        private System.Windows.Forms.Label labelOffset;
        private System.Windows.Forms.CheckBox checkBoxRawMode;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageTR;
        private System.Windows.Forms.RadioButton radioButtonClipWithReencode;
        private System.Windows.Forms.RadioButton radioButtonClipAllBitrates;
        private System.Windows.Forms.RadioButton radioButtonClipTopBitrate;
        private System.Windows.Forms.RadioButton radioButtonArchiveTopBitrate;
        private System.Windows.Forms.Label labelDefaultEnd;
        private System.Windows.Forms.Label labelStartTimeDefault;
        private System.Windows.Forms.PictureBox pictureBox3;
        private TimeControl timeControlStart;
        private TimeControl timeControlEnd;
        private System.Windows.Forms.CheckBox checkBoxEndTime;
        private System.Windows.Forms.CheckBox checkBoxStartTime;
        private System.Windows.Forms.TabPage tabPageTRRaw;
        private System.Windows.Forms.TextBox textBoxRawTimescale;
        private System.Windows.Forms.TextBox textBoxRawBackoff;
        private System.Windows.Forms.TextBox textBoxRawDVR;
        private System.Windows.Forms.TextBox textBoxRawEnd;
        private System.Windows.Forms.TextBox textBoxRawStart;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private ButtonJobOptions buttonJobOptions;
    }
}