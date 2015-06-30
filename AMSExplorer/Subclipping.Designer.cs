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
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
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
            this.textBoxRawEnd = new System.Windows.Forms.TextBox();
            this.textBoxRawStart = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPageTR = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.radioButtonArchiveTopBitrate = new System.Windows.Forms.RadioButton();
            this.radioButtonArchiveAllBitrate = new System.Windows.Forms.RadioButton();
            this.radioButtonClipWithReencode = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.timeControlStart = new AMSExplorer.TimeControl();
            this.timeControlEnd = new AMSExplorer.TimeControl();
            this.checkBoxTrimming = new System.Windows.Forms.CheckBox();
            this.buttonJobOptions = new AMSExplorer.ButtonJobOptions();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.tabPageTRRaw.SuspendLayout();
            this.tabPageTR.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
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
            this.buttonOk.Location = new System.Drawing.Point(792, 12);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(98, 23);
            this.buttonOk.TabIndex = 4;
            this.buttonOk.Text = "Subclip";
            this.buttonOk.UseVisualStyleBackColor = true;
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
            this.labelFilterTitle.Text = "Rendered subclipping";
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
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 86;
            this.label2.Text = "Asset timescale :";
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
            this.tabPageTRRaw.Controls.Add(this.textBoxRawEnd);
            this.tabPageTRRaw.Controls.Add(this.textBoxRawStart);
            this.tabPageTRRaw.Controls.Add(this.label9);
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
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(23, 142);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 13);
            this.label5.TabIndex = 113;
            this.label5.Text = "End time :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(20, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 91;
            this.label3.Text = "Start time :";
            // 
            // tabPageTR
            // 
            this.tabPageTR.Controls.Add(this.groupBox2);
            this.tabPageTR.Controls.Add(this.groupBox1);
            this.tabPageTR.Controls.Add(this.buttonJobOptions);
            this.tabPageTR.Location = new System.Drawing.Point(4, 22);
            this.tabPageTR.Name = "tabPageTR";
            this.tabPageTR.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTR.Size = new System.Drawing.Size(974, 483);
            this.tabPageTR.TabIndex = 0;
            this.tabPageTR.Text = "Time Range";
            this.tabPageTR.UseVisualStyleBackColor = true;
            this.tabPageTR.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.radioButtonArchiveTopBitrate);
            this.groupBox2.Controls.Add(this.radioButtonArchiveAllBitrate);
            this.groupBox2.Controls.Add(this.radioButtonClipWithReencode);
            this.groupBox2.Location = new System.Drawing.Point(20, 13);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(683, 110);
            this.groupBox2.TabIndex = 133;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Subclipping mode";
            // 
            // label15
            // 
            this.label15.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label15.Location = new System.Drawing.Point(522, 74);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(133, 19);
            this.label15.TabIndex = 138;
            this.label15.Text = "Trimming is frame accurate";
            // 
            // label14
            // 
            this.label14.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label14.Location = new System.Drawing.Point(522, 51);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(133, 19);
            this.label14.TabIndex = 137;
            this.label14.Text = "Trimming is GOP accurate";
            // 
            // label13
            // 
            this.label13.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label13.Location = new System.Drawing.Point(522, 28);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(133, 19);
            this.label13.TabIndex = 136;
            this.label13.Text = "Trimming is GOP accurate";
            // 
            // label12
            // 
            this.label12.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label12.Location = new System.Drawing.Point(151, 74);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(339, 19);
            this.label12.TabIndex = 135;
            this.label12.Text = "Reencode the top bitrate with any Media Encoder Standard preset";
            // 
            // label11
            // 
            this.label11.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label11.Location = new System.Drawing.Point(151, 51);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(284, 19);
            this.label11.TabIndex = 134;
            this.label11.Text = "Generate multiple MP4 files";
            // 
            // label10
            // 
            this.label10.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label10.Location = new System.Drawing.Point(151, 28);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(245, 19);
            this.label10.TabIndex = 133;
            this.label10.Text = "Generate a single MP4 file from the top bitrate";
            // 
            // radioButtonArchiveTopBitrate
            // 
            this.radioButtonArchiveTopBitrate.AutoSize = true;
            this.radioButtonArchiveTopBitrate.Checked = true;
            this.radioButtonArchiveTopBitrate.Location = new System.Drawing.Point(18, 26);
            this.radioButtonArchiveTopBitrate.Name = "radioButtonArchiveTopBitrate";
            this.radioButtonArchiveTopBitrate.Size = new System.Drawing.Size(116, 17);
            this.radioButtonArchiveTopBitrate.TabIndex = 126;
            this.radioButtonArchiveTopBitrate.TabStop = true;
            this.radioButtonArchiveTopBitrate.Text = "Archive Top Bitrate";
            this.radioButtonArchiveTopBitrate.UseVisualStyleBackColor = true;
            // 
            // radioButtonArchiveAllBitrate
            // 
            this.radioButtonArchiveAllBitrate.AutoSize = true;
            this.radioButtonArchiveAllBitrate.Location = new System.Drawing.Point(18, 49);
            this.radioButtonArchiveAllBitrate.Name = "radioButtonArchiveAllBitrate";
            this.radioButtonArchiveAllBitrate.Size = new System.Drawing.Size(113, 17);
            this.radioButtonArchiveAllBitrate.TabIndex = 128;
            this.radioButtonArchiveAllBitrate.Text = "Archive All Bitrates";
            this.radioButtonArchiveAllBitrate.UseVisualStyleBackColor = true;
            // 
            // radioButtonClipWithReencode
            // 
            this.radioButtonClipWithReencode.AutoSize = true;
            this.radioButtonClipWithReencode.Location = new System.Drawing.Point(18, 72);
            this.radioButtonClipWithReencode.Name = "radioButtonClipWithReencode";
            this.radioButtonClipWithReencode.Size = new System.Drawing.Size(75, 17);
            this.radioButtonClipWithReencode.TabIndex = 129;
            this.radioButtonClipWithReencode.Text = "Reencode";
            this.radioButtonClipWithReencode.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.timeControlStart);
            this.groupBox1.Controls.Add(this.timeControlEnd);
            this.groupBox1.Controls.Add(this.checkBoxTrimming);
            this.groupBox1.Location = new System.Drawing.Point(20, 150);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(936, 267);
            this.groupBox1.TabIndex = 131;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Trimming";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 165);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 13);
            this.label6.TabIndex = 124;
            this.label6.Text = "End time :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 123;
            this.label1.Text = "Start time :";
            // 
            // timeControlStart
            // 
            this.timeControlStart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.timeControlStart.BackColor = System.Drawing.SystemColors.Window;
            this.timeControlStart.DisplayTrackBar = false;
            this.timeControlStart.DVRMode = false;
            this.timeControlStart.Enabled = false;
            this.timeControlStart.Location = new System.Drawing.Point(6, 68);
            this.timeControlStart.Max = System.TimeSpan.Parse("10675199.02:48:05.4775807");
            this.timeControlStart.Min = System.TimeSpan.Parse("00:00:00");
            this.timeControlStart.Name = "timeControlStart";
            this.timeControlStart.ScaledFirstTimestampOffset = ((long)(0));
            this.timeControlStart.ScaledTotalDuration = ((long)(-1));
            this.timeControlStart.Size = new System.Drawing.Size(897, 80);
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
            this.timeControlEnd.Location = new System.Drawing.Point(6, 181);
            this.timeControlEnd.Max = System.TimeSpan.Parse("10675199.02:48:05.4775807");
            this.timeControlEnd.Min = System.TimeSpan.Parse("00:00:00");
            this.timeControlEnd.Name = "timeControlEnd";
            this.timeControlEnd.ScaledFirstTimestampOffset = ((long)(0));
            this.timeControlEnd.ScaledTotalDuration = ((long)(-1));
            this.timeControlEnd.Size = new System.Drawing.Size(897, 80);
            this.timeControlEnd.TabIndex = 105;
            this.timeControlEnd.TimeScale = ((long)(10000000));
            this.timeControlEnd.ValueChanged += new System.EventHandler(this.timeControlEnd_ValueChanged);
            // 
            // checkBoxTrimming
            // 
            this.checkBoxTrimming.AutoSize = true;
            this.checkBoxTrimming.Location = new System.Drawing.Point(18, 19);
            this.checkBoxTrimming.Name = "checkBoxTrimming";
            this.checkBoxTrimming.Size = new System.Drawing.Size(228, 17);
            this.checkBoxTrimming.TabIndex = 132;
            this.checkBoxTrimming.Text = "Select a start and end time from the source";
            this.checkBoxTrimming.UseVisualStyleBackColor = true;
            this.checkBoxTrimming.CheckedChanged += new System.EventHandler(this.checkBoxTrimming_CheckedChanged);
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
            this.Controls.Add(this.panel1);
            this.Name = "Subclipping";
            this.Text = "Rendered Subclip";
            this.Load += new System.EventHandler(this.Subclipping_Load);
            this.Shown += new System.EventHandler(this.DynManifestFilter_Shown);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.tabPageTRRaw.ResumeLayout(false);
            this.tabPageTRRaw.PerformLayout();
            this.tabPageTR.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Button buttonCancel;
        public System.Windows.Forms.Button buttonOk;
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
        private System.Windows.Forms.RadioButton radioButtonArchiveAllBitrate;
        private System.Windows.Forms.RadioButton radioButtonArchiveTopBitrate;
        private TimeControl timeControlStart;
        private TimeControl timeControlEnd;
        private System.Windows.Forms.TabPage tabPageTRRaw;
        private System.Windows.Forms.TextBox textBoxRawTimescale;
        private System.Windows.Forms.TextBox textBoxRawEnd;
        private System.Windows.Forms.TextBox textBoxRawStart;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private ButtonJobOptions buttonJobOptions;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkBoxTrimming;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
    }
}