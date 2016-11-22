namespace AMSExplorer
{
    partial class Hyperlapse
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Hyperlapse));
            this.label3 = new System.Windows.Forms.Label();
            this.textboxoutputassetname = new System.Windows.Forms.TextBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelAssetName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxJobName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.panelTimes = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxSourceStartTime = new System.Windows.Forms.TextBox();
            this.textBoxOutputDuration = new System.Windows.Forms.TextBox();
            this.comboBoxFrameRate = new System.Windows.Forms.ComboBox();
            this.textBoxSourceDurationTime = new System.Windows.Forms.TextBox();
            this.labelSourceFrameRate = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.numericUpDownNumFrames = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownStartFrame = new System.Windows.Forms.NumericUpDown();
            this.linkLabelHowItWorks = new System.Windows.Forms.LinkLabel();
            this.numericUpDownSpeed = new System.Windows.Forms.NumericUpDown();
            this.labelspeed = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonOk = new System.Windows.Forms.Button();
            this.label34 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.moreinfoprofilelink = new System.Windows.Forms.LinkLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.labelPreview = new System.Windows.Forms.Label();
            this.labelProcessorVersion = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.checkBoxStabilize = new System.Windows.Forms.CheckBox();
            this.checkBoxLimitNbFrames = new System.Windows.Forms.CheckBox();
            this.tabPageConfig = new System.Windows.Forms.TabPage();
            this.labelWarningJSON = new System.Windows.Forms.Label();
            this.textBoxConfiguration = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.buttonJobOptions = new AMSExplorer.ButtonJobOptions();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelTimes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNumFrames)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStartFrame)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSpeed)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPageConfig.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 440);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 15);
            this.label3.TabIndex = 22;
            this.label3.Text = "Output asset name :";
            // 
            // textboxoutputassetname
            // 
            this.textboxoutputassetname.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textboxoutputassetname.Location = new System.Drawing.Point(17, 459);
            this.textboxoutputassetname.Name = "textboxoutputassetname";
            this.textboxoutputassetname.Size = new System.Drawing.Size(364, 23);
            this.textboxoutputassetname.TabIndex = 21;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(506, 15);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(115, 27);
            this.buttonCancel.TabIndex = 16;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // labelAssetName
            // 
            this.labelAssetName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelAssetName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAssetName.Location = new System.Drawing.Point(112, 59);
            this.labelAssetName.Name = "labelAssetName";
            this.labelAssetName.Size = new System.Drawing.Size(397, 15);
            this.labelAssetName.TabIndex = 50;
            this.labelAssetName.Text = "assetname";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(129, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 15);
            this.label1.TabIndex = 36;
            this.label1.Text = "Start frame :";
            // 
            // textBoxJobName
            // 
            this.textBoxJobName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxJobName.Location = new System.Drawing.Point(17, 403);
            this.textBoxJobName.Name = "textBoxJobName";
            this.textBoxJobName.Size = new System.Drawing.Size(364, 23);
            this.textBoxJobName.TabIndex = 54;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 385);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 15);
            this.label5.TabIndex = 53;
            this.label5.Text = "Job name :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label9.Location = new System.Drawing.Point(6, 119);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(96, 30);
            this.label9.TabIndex = 83;
            this.label9.Text = "1 = video\r\nstabilization only";
            // 
            // panelTimes
            // 
            this.panelTimes.Controls.Add(this.label6);
            this.panelTimes.Controls.Add(this.label8);
            this.panelTimes.Controls.Add(this.textBoxSourceStartTime);
            this.panelTimes.Controls.Add(this.textBoxOutputDuration);
            this.panelTimes.Controls.Add(this.comboBoxFrameRate);
            this.panelTimes.Controls.Add(this.textBoxSourceDurationTime);
            this.panelTimes.Controls.Add(this.labelSourceFrameRate);
            this.panelTimes.Controls.Add(this.label7);
            this.panelTimes.Location = new System.Drawing.Point(305, 74);
            this.panelTimes.Name = "panelTimes";
            this.panelTimes.Size = new System.Drawing.Size(289, 152);
            this.panelTimes.TabIndex = 83;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(150, 7);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 15);
            this.label6.TabIndex = 79;
            this.label6.Text = "Start time :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(150, 107);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(99, 15);
            this.label8.TabIndex = 82;
            this.label8.Text = "Output duration :";
            // 
            // textBoxSourceStartTime
            // 
            this.textBoxSourceStartTime.Location = new System.Drawing.Point(154, 24);
            this.textBoxSourceStartTime.Name = "textBoxSourceStartTime";
            this.textBoxSourceStartTime.ReadOnly = true;
            this.textBoxSourceStartTime.Size = new System.Drawing.Size(123, 23);
            this.textBoxSourceStartTime.TabIndex = 72;
            this.textBoxSourceStartTime.Text = "00:00:02";
            // 
            // textBoxOutputDuration
            // 
            this.textBoxOutputDuration.Location = new System.Drawing.Point(154, 126);
            this.textBoxOutputDuration.Name = "textBoxOutputDuration";
            this.textBoxOutputDuration.ReadOnly = true;
            this.textBoxOutputDuration.Size = new System.Drawing.Size(123, 23);
            this.textBoxOutputDuration.TabIndex = 81;
            this.textBoxOutputDuration.Text = "00:00:02";
            // 
            // comboBoxFrameRate
            // 
            this.comboBoxFrameRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFrameRate.FormattingEnabled = true;
            this.comboBoxFrameRate.Items.AddRange(new object[] {
            "24",
            "25",
            "30",
            "50",
            "60"});
            this.comboBoxFrameRate.Location = new System.Drawing.Point(7, 24);
            this.comboBoxFrameRate.Name = "comboBoxFrameRate";
            this.comboBoxFrameRate.Size = new System.Drawing.Size(101, 23);
            this.comboBoxFrameRate.TabIndex = 76;
            this.comboBoxFrameRate.SelectedIndexChanged += new System.EventHandler(this.control_ValueChanged);
            // 
            // textBoxSourceDurationTime
            // 
            this.textBoxSourceDurationTime.Location = new System.Drawing.Point(154, 73);
            this.textBoxSourceDurationTime.Name = "textBoxSourceDurationTime";
            this.textBoxSourceDurationTime.ReadOnly = true;
            this.textBoxSourceDurationTime.Size = new System.Drawing.Size(123, 23);
            this.textBoxSourceDurationTime.TabIndex = 72;
            this.textBoxSourceDurationTime.Text = "00:00:02";
            // 
            // labelSourceFrameRate
            // 
            this.labelSourceFrameRate.AutoSize = true;
            this.labelSourceFrameRate.Location = new System.Drawing.Point(4, 7);
            this.labelSourceFrameRate.Name = "labelSourceFrameRate";
            this.labelSourceFrameRate.Size = new System.Drawing.Size(103, 15);
            this.labelSourceFrameRate.TabIndex = 78;
            this.labelSourceFrameRate.Text = "Source framerate :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(150, 54);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 15);
            this.label7.TabIndex = 80;
            this.label7.Text = "Duration :";
            // 
            // numericUpDownNumFrames
            // 
            this.numericUpDownNumFrames.Enabled = false;
            this.numericUpDownNumFrames.Location = new System.Drawing.Point(132, 162);
            this.numericUpDownNumFrames.Maximum = new decimal(new int[] {
            3000000,
            0,
            0,
            0});
            this.numericUpDownNumFrames.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownNumFrames.Name = "numericUpDownNumFrames";
            this.numericUpDownNumFrames.Size = new System.Drawing.Size(124, 23);
            this.numericUpDownNumFrames.TabIndex = 44;
            this.numericUpDownNumFrames.Value = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDownNumFrames.ValueChanged += new System.EventHandler(this.control_ValueChanged);
            // 
            // numericUpDownStartFrame
            // 
            this.numericUpDownStartFrame.Location = new System.Drawing.Point(132, 93);
            this.numericUpDownStartFrame.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDownStartFrame.Name = "numericUpDownStartFrame";
            this.numericUpDownStartFrame.Size = new System.Drawing.Size(124, 23);
            this.numericUpDownStartFrame.TabIndex = 43;
            this.numericUpDownStartFrame.ValueChanged += new System.EventHandler(this.control_ValueChanged);
            // 
            // linkLabelHowItWorks
            // 
            this.linkLabelHowItWorks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabelHowItWorks.AutoSize = true;
            this.linkLabelHowItWorks.Location = new System.Drawing.Point(518, 42);
            this.linkLabelHowItWorks.Name = "linkLabelHowItWorks";
            this.linkLabelHowItWorks.Size = new System.Drawing.Size(76, 15);
            this.linkLabelHowItWorks.TabIndex = 71;
            this.linkLabelHowItWorks.TabStop = true;
            this.linkLabelHowItWorks.Text = "How it works";
            this.linkLabelHowItWorks.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelHowItWorks_LinkClicked);
            // 
            // numericUpDownSpeed
            // 
            this.numericUpDownSpeed.Location = new System.Drawing.Point(10, 93);
            this.numericUpDownSpeed.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownSpeed.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownSpeed.Name = "numericUpDownSpeed";
            this.numericUpDownSpeed.Size = new System.Drawing.Size(80, 23);
            this.numericUpDownSpeed.TabIndex = 42;
            this.numericUpDownSpeed.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.numericUpDownSpeed.ValueChanged += new System.EventHandler(this.control_ValueChanged);
            // 
            // labelspeed
            // 
            this.labelspeed.AutoSize = true;
            this.labelspeed.Location = new System.Drawing.Point(6, 74);
            this.labelspeed.Name = "labelspeed";
            this.labelspeed.Size = new System.Drawing.Size(45, 15);
            this.labelspeed.TabIndex = 41;
            this.labelspeed.Tag = "";
            this.labelspeed.Text = "Speed :";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label4.Location = new System.Drawing.Point(6, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(582, 39);
            this.label4.TabIndex = 38;
            this.label4.Text = resources.GetString("label4.Text");
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Image = global::AMSExplorer.Bitmaps.hyperlapse;
            this.buttonOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonOk.Location = new System.Drawing.Point(329, 15);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(170, 27);
            this.buttonOk.TabIndex = 17;
            this.buttonOk.Text = "Launch processing";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // label34
            // 
            this.label34.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.label34.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(128)))), ((int)(((byte)(171)))));
            this.label34.Location = new System.Drawing.Point(288, 14);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(338, 25);
            this.label34.TabIndex = 65;
            this.label34.Text = "Hyperlapse for Azure Media Services";
            this.label34.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonOk);
            this.panel1.Location = new System.Drawing.Point(-2, 508);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(635, 55);
            this.panel1.TabIndex = 66;
            // 
            // moreinfoprofilelink
            // 
            this.moreinfoprofilelink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.moreinfoprofilelink.AutoSize = true;
            this.moreinfoprofilelink.Location = new System.Drawing.Point(525, 86);
            this.moreinfoprofilelink.Name = "moreinfoprofilelink";
            this.moreinfoprofilelink.Size = new System.Drawing.Size(101, 15);
            this.moreinfoprofilelink.TabIndex = 70;
            this.moreinfoprofilelink.TabStop = true;
            this.moreinfoprofilelink.Text = "More information";
            this.moreinfoprofilelink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.moreinfoprofilelink_LinkClicked);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::AMSExplorer.Bitmaps._02_hyperlapse;
            this.pictureBox1.Location = new System.Drawing.Point(14, 14);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(92, 89);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 71;
            this.pictureBox1.TabStop = false;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // labelPreview
            // 
            this.labelPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPreview.AutoSize = true;
            this.labelPreview.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPreview.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.labelPreview.Location = new System.Drawing.Point(567, 39);
            this.labelPreview.Name = "labelPreview";
            this.labelPreview.Size = new System.Drawing.Size(59, 20);
            this.labelPreview.TabIndex = 74;
            this.labelPreview.Text = "Preview";
            this.labelPreview.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelProcessorVersion
            // 
            this.labelProcessorVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelProcessorVersion.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.labelProcessorVersion.Location = new System.Drawing.Point(502, 59);
            this.labelProcessorVersion.Name = "labelProcessorVersion";
            this.labelProcessorVersion.Size = new System.Drawing.Size(124, 20);
            this.labelProcessorVersion.TabIndex = 78;
            this.labelProcessorVersion.Text = "Version {0}";
            this.labelProcessorVersion.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPageConfig);
            this.tabControl1.Location = new System.Drawing.Point(18, 109);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(609, 260);
            this.tabControl1.TabIndex = 109;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.checkBoxStabilize);
            this.tabPage1.Controls.Add(this.checkBoxLimitNbFrames);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.panelTimes);
            this.tabPage1.Controls.Add(this.labelspeed);
            this.tabPage1.Controls.Add(this.numericUpDownNumFrames);
            this.tabPage1.Controls.Add(this.numericUpDownSpeed);
            this.tabPage1.Controls.Add(this.linkLabelHowItWorks);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.numericUpDownStartFrame);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(601, 232);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Settings";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // checkBoxStabilize
            // 
            this.checkBoxStabilize.AutoSize = true;
            this.checkBoxStabilize.Checked = true;
            this.checkBoxStabilize.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxStabilize.Location = new System.Drawing.Point(10, 200);
            this.checkBoxStabilize.Name = "checkBoxStabilize";
            this.checkBoxStabilize.Size = new System.Drawing.Size(69, 19);
            this.checkBoxStabilize.TabIndex = 85;
            this.checkBoxStabilize.Text = "Stabilize";
            this.checkBoxStabilize.UseVisualStyleBackColor = true;
            this.checkBoxStabilize.CheckedChanged += new System.EventHandler(this.control_ValueChanged);
            // 
            // checkBoxLimitNbFrames
            // 
            this.checkBoxLimitNbFrames.AutoSize = true;
            this.checkBoxLimitNbFrames.Location = new System.Drawing.Point(132, 140);
            this.checkBoxLimitNbFrames.Name = "checkBoxLimitNbFrames";
            this.checkBoxLimitNbFrames.Size = new System.Drawing.Size(127, 19);
            this.checkBoxLimitNbFrames.TabIndex = 84;
            this.checkBoxLimitNbFrames.Text = "Frames to process :";
            this.checkBoxLimitNbFrames.UseVisualStyleBackColor = true;
            this.checkBoxLimitNbFrames.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // tabPageConfig
            // 
            this.tabPageConfig.Controls.Add(this.labelWarningJSON);
            this.tabPageConfig.Controls.Add(this.textBoxConfiguration);
            this.tabPageConfig.Controls.Add(this.label12);
            this.tabPageConfig.Location = new System.Drawing.Point(4, 24);
            this.tabPageConfig.Name = "tabPageConfig";
            this.tabPageConfig.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageConfig.Size = new System.Drawing.Size(601, 232);
            this.tabPageConfig.TabIndex = 1;
            this.tabPageConfig.Text = "Generated JSON configuration";
            this.tabPageConfig.UseVisualStyleBackColor = true;
            this.tabPageConfig.Enter += new System.EventHandler(this.control_changed);
            // 
            // labelWarningJSON
            // 
            this.labelWarningJSON.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelWarningJSON.ForeColor = System.Drawing.Color.Red;
            this.labelWarningJSON.Location = new System.Drawing.Point(143, 7);
            this.labelWarningJSON.Name = "labelWarningJSON";
            this.labelWarningJSON.Size = new System.Drawing.Size(448, 19);
            this.labelWarningJSON.TabIndex = 80;
            this.labelWarningJSON.Tag = "JSON Syntax error. {0}";
            this.labelWarningJSON.Text = "JSON Syntax error. {0}";
            this.labelWarningJSON.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelWarningJSON.Visible = false;
            // 
            // textBoxConfiguration
            // 
            this.textBoxConfiguration.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxConfiguration.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxConfiguration.Location = new System.Drawing.Point(6, 29);
            this.textBoxConfiguration.Multiline = true;
            this.textBoxConfiguration.Name = "textBoxConfiguration";
            this.textBoxConfiguration.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxConfiguration.Size = new System.Drawing.Size(585, 199);
            this.textBoxConfiguration.TabIndex = 78;
            this.textBoxConfiguration.TextChanged += new System.EventHandler(this.textBoxConfiguration_TextChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(2, 10);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(135, 15);
            this.label12.TabIndex = 79;
            this.label12.Text = "JSON or XML (editable) :";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Location = new System.Drawing.Point(625, 508);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(25, 42);
            this.panel2.TabIndex = 110;
            // 
            // buttonJobOptions
            // 
            this.buttonJobOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonJobOptions.Location = new System.Drawing.Point(467, 403);
            this.buttonJobOptions.Name = "buttonJobOptions";
            this.buttonJobOptions.Size = new System.Drawing.Size(160, 27);
            this.buttonJobOptions.TabIndex = 72;
            this.buttonJobOptions.Text = "Job options...";
            this.buttonJobOptions.UseVisualStyleBackColor = true;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Key";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Value";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // Hyperlapse
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(645, 564);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.labelProcessorVersion);
            this.Controls.Add(this.labelPreview);
            this.Controls.Add(this.buttonJobOptions);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.moreinfoprofilelink);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label34);
            this.Controls.Add(this.textBoxJobName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.labelAssetName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textboxoutputassetname);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "Hyperlapse";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Hyperlapse processing";
            this.Load += new System.EventHandler(this.Hyperlapse_Load);
            this.panelTimes.ResumeLayout(false);
            this.panelTimes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNumFrames)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStartFrame)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSpeed)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPageConfig.ResumeLayout(false);
            this.tabPageConfig.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox textboxoutputassetname;
        public System.Windows.Forms.Button buttonOk;
        public System.Windows.Forms.Button buttonCancel;
        public System.Windows.Forms.Label labelAssetName;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox textBoxJobName;
        public System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.Label label34;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.LinkLabel moreinfoprofilelink;
        private System.Windows.Forms.Label labelspeed;
        private System.Windows.Forms.NumericUpDown numericUpDownStartFrame;
        private System.Windows.Forms.NumericUpDown numericUpDownSpeed;
        private System.Windows.Forms.NumericUpDown numericUpDownNumFrames;
        private System.Windows.Forms.LinkLabel linkLabelHowItWorks;
        private System.Windows.Forms.PictureBox pictureBox1;
        private ButtonJobOptions buttonJobOptions;
        private System.Windows.Forms.TextBox textBoxSourceStartTime;
        private System.Windows.Forms.ComboBox comboBoxFrameRate;
        private System.Windows.Forms.TextBox textBoxSourceDurationTime;
        public System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxOutputDuration;
        public System.Windows.Forms.Label label7;
        public System.Windows.Forms.Label label6;
        public System.Windows.Forms.Label labelSourceFrameRate;
        private System.Windows.Forms.Panel panelTimes;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        public System.Windows.Forms.Label label9;
        public System.Windows.Forms.Label labelPreview;
        private System.Windows.Forms.Label labelProcessorVersion;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPageConfig;
        private System.Windows.Forms.Label labelWarningJSON;
        private System.Windows.Forms.TextBox textBoxConfiguration;
        public System.Windows.Forms.Label label12;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox checkBoxLimitNbFrames;
        private System.Windows.Forms.CheckBox checkBoxStabilize;
    }
}