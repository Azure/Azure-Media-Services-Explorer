namespace AMSExplorer
{
    partial class Options
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
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.checkBoxUseStorageEncryption = new System.Windows.Forms.CheckBox();
            this.checkBoxUseProtectedConfig = new System.Windows.Forms.CheckBox();
            this.checkBoxOneUpDownload = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxNbItems = new System.Windows.Forms.ComboBox();
            this.buttonReset = new System.Windows.Forms.Button();
            this.checkBoxDisplayAssetID = new System.Windows.Forms.CheckBox();
            this.checkBoxDisplayJobID = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxDisplayAssetStorage = new System.Windows.Forms.CheckBox();
            this.checkBoxAutoRefresh = new System.Windows.Forms.CheckBox();
            this.comboBoxAutoRefreshTime = new System.Windows.Forms.ComboBox();
            this.checkBoxDisplayOriginID = new System.Windows.Forms.CheckBox();
            this.checkBoxDisplayProgramID = new System.Windows.Forms.CheckBox();
            this.checkBoxDisplayChannelID = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDownLocatorDuration = new System.Windows.Forms.NumericUpDown();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownPriority = new System.Windows.Forms.NumericUpDown();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBoxCustomPlayer = new System.Windows.Forms.TextBox();
            this.checkBoxEnableCustomPlayer = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDownAMEPrice = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownLegacyEncodingPrice = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.numericUpDownIndexingPrice = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxCurrency = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.amspriceslink = new System.Windows.Forms.LinkLabel();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.numericUpDownTokenDuration = new System.Windows.Forms.NumericUpDown();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLocatorDuration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPriority)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAMEPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLegacyEncodingPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownIndexingPrice)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTokenDuration)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(306, 13);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(99, 23);
            this.buttonOk.TabIndex = 7;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(411, 13);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(99, 23);
            this.buttonCancel.TabIndex = 6;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // checkBoxUseStorageEncryption
            // 
            this.checkBoxUseStorageEncryption.AutoSize = true;
            this.checkBoxUseStorageEncryption.Location = new System.Drawing.Point(42, 23);
            this.checkBoxUseStorageEncryption.Name = "checkBoxUseStorageEncryption";
            this.checkBoxUseStorageEncryption.Size = new System.Drawing.Size(361, 17);
            this.checkBoxUseStorageEncryption.TabIndex = 8;
            this.checkBoxUseStorageEncryption.Text = "Use storage encryption for new assets (except storage to storage copy)";
            this.checkBoxUseStorageEncryption.UseVisualStyleBackColor = true;
            // 
            // checkBoxUseProtectedConfig
            // 
            this.checkBoxUseProtectedConfig.AutoSize = true;
            this.checkBoxUseProtectedConfig.Location = new System.Drawing.Point(42, 46);
            this.checkBoxUseProtectedConfig.Name = "checkBoxUseProtectedConfig";
            this.checkBoxUseProtectedConfig.Size = new System.Drawing.Size(223, 17);
            this.checkBoxUseProtectedConfig.TabIndex = 9;
            this.checkBoxUseProtectedConfig.Text = "Use protected configuration for new tasks";
            this.checkBoxUseProtectedConfig.UseVisualStyleBackColor = true;
            // 
            // checkBoxOneUpDownload
            // 
            this.checkBoxOneUpDownload.AutoSize = true;
            this.checkBoxOneUpDownload.Location = new System.Drawing.Point(42, 69);
            this.checkBoxOneUpDownload.Name = "checkBoxOneUpDownload";
            this.checkBoxOneUpDownload.Size = new System.Drawing.Size(243, 17);
            this.checkBoxOneUpDownload.TabIndex = 10;
            this.checkBoxOneUpDownload.Text = "One upload/download at a time (use a queue)";
            this.checkBoxOneUpDownload.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(119, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "items displayed per page";
            // 
            // comboBoxNbItems
            // 
            this.comboBoxNbItems.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxNbItems.FormattingEnabled = true;
            this.comboBoxNbItems.Location = new System.Drawing.Point(42, 24);
            this.comboBoxNbItems.Name = "comboBoxNbItems";
            this.comboBoxNbItems.Size = new System.Drawing.Size(71, 21);
            this.comboBoxNbItems.TabIndex = 13;
            // 
            // buttonReset
            // 
            this.buttonReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonReset.Location = new System.Drawing.Point(14, 13);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(113, 23);
            this.buttonReset.TabIndex = 14;
            this.buttonReset.Text = "Reset to defaults";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // checkBoxDisplayAssetID
            // 
            this.checkBoxDisplayAssetID.AutoSize = true;
            this.checkBoxDisplayAssetID.Location = new System.Drawing.Point(42, 51);
            this.checkBoxDisplayAssetID.Name = "checkBoxDisplayAssetID";
            this.checkBoxDisplayAssetID.Size = new System.Drawing.Size(101, 17);
            this.checkBoxDisplayAssetID.TabIndex = 15;
            this.checkBoxDisplayAssetID.Text = "Display Asset Id";
            this.checkBoxDisplayAssetID.UseVisualStyleBackColor = true;
            // 
            // checkBoxDisplayJobID
            // 
            this.checkBoxDisplayJobID.AutoSize = true;
            this.checkBoxDisplayJobID.Location = new System.Drawing.Point(187, 51);
            this.checkBoxDisplayJobID.Name = "checkBoxDisplayJobID";
            this.checkBoxDisplayJobID.Size = new System.Drawing.Size(92, 17);
            this.checkBoxDisplayJobID.TabIndex = 16;
            this.checkBoxDisplayJobID.Text = "Display Job Id";
            this.checkBoxDisplayJobID.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxDisplayAssetStorage);
            this.groupBox1.Controls.Add(this.checkBoxAutoRefresh);
            this.groupBox1.Controls.Add(this.comboBoxAutoRefreshTime);
            this.groupBox1.Controls.Add(this.checkBoxDisplayOriginID);
            this.groupBox1.Controls.Add(this.checkBoxDisplayProgramID);
            this.groupBox1.Controls.Add(this.checkBoxDisplayChannelID);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.checkBoxDisplayJobID);
            this.groupBox1.Controls.Add(this.comboBoxNbItems);
            this.groupBox1.Controls.Add(this.checkBoxDisplayAssetID);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(496, 130);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Grids display";
            // 
            // checkBoxDisplayAssetStorage
            // 
            this.checkBoxDisplayAssetStorage.AutoSize = true;
            this.checkBoxDisplayAssetStorage.Location = new System.Drawing.Point(42, 72);
            this.checkBoxDisplayAssetStorage.Name = "checkBoxDisplayAssetStorage";
            this.checkBoxDisplayAssetStorage.Size = new System.Drawing.Size(129, 17);
            this.checkBoxDisplayAssetStorage.TabIndex = 22;
            this.checkBoxDisplayAssetStorage.Text = "Display Asset Storage";
            this.checkBoxDisplayAssetStorage.UseVisualStyleBackColor = true;
            // 
            // checkBoxAutoRefresh
            // 
            this.checkBoxAutoRefresh.AutoSize = true;
            this.checkBoxAutoRefresh.Location = new System.Drawing.Point(42, 97);
            this.checkBoxAutoRefresh.Name = "checkBoxAutoRefresh";
            this.checkBoxAutoRefresh.Size = new System.Drawing.Size(132, 17);
            this.checkBoxAutoRefresh.TabIndex = 21;
            this.checkBoxAutoRefresh.Text = "Auto refresh every (s) :";
            this.toolTip1.SetToolTip(this.checkBoxAutoRefresh, "Useful if another program create assets, jobs, etc");
            this.checkBoxAutoRefresh.UseVisualStyleBackColor = true;
            // 
            // comboBoxAutoRefreshTime
            // 
            this.comboBoxAutoRefreshTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAutoRefreshTime.FormattingEnabled = true;
            this.comboBoxAutoRefreshTime.Items.AddRange(new object[] {
            "30",
            "60",
            "300",
            "600"});
            this.comboBoxAutoRefreshTime.Location = new System.Drawing.Point(180, 95);
            this.comboBoxAutoRefreshTime.Name = "comboBoxAutoRefreshTime";
            this.comboBoxAutoRefreshTime.Size = new System.Drawing.Size(62, 21);
            this.comboBoxAutoRefreshTime.TabIndex = 20;
            // 
            // checkBoxDisplayOriginID
            // 
            this.checkBoxDisplayOriginID.AutoSize = true;
            this.checkBoxDisplayOriginID.Location = new System.Drawing.Point(187, 72);
            this.checkBoxDisplayOriginID.Name = "checkBoxDisplayOriginID";
            this.checkBoxDisplayOriginID.Size = new System.Drawing.Size(161, 17);
            this.checkBoxDisplayOriginID.TabIndex = 19;
            this.checkBoxDisplayOriginID.Text = "Display Streamin Endpoint Id";
            this.checkBoxDisplayOriginID.UseVisualStyleBackColor = true;
            // 
            // checkBoxDisplayProgramID
            // 
            this.checkBoxDisplayProgramID.AutoSize = true;
            this.checkBoxDisplayProgramID.Location = new System.Drawing.Point(361, 72);
            this.checkBoxDisplayProgramID.Name = "checkBoxDisplayProgramID";
            this.checkBoxDisplayProgramID.Size = new System.Drawing.Size(114, 17);
            this.checkBoxDisplayProgramID.TabIndex = 18;
            this.checkBoxDisplayProgramID.Text = "Display Program Id";
            this.checkBoxDisplayProgramID.UseVisualStyleBackColor = true;
            // 
            // checkBoxDisplayChannelID
            // 
            this.checkBoxDisplayChannelID.AutoSize = true;
            this.checkBoxDisplayChannelID.Location = new System.Drawing.Point(361, 51);
            this.checkBoxDisplayChannelID.Name = "checkBoxDisplayChannelID";
            this.checkBoxDisplayChannelID.Size = new System.Drawing.Size(114, 17);
            this.checkBoxDisplayChannelID.TabIndex = 17;
            this.checkBoxDisplayChannelID.Text = "Display Channel Id";
            this.checkBoxDisplayChannelID.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.numericUpDownTokenDuration);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.numericUpDownLocatorDuration);
            this.groupBox2.Controls.Add(this.pictureBox2);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.numericUpDownPriority);
            this.groupBox2.Controls.Add(this.checkBoxUseStorageEncryption);
            this.groupBox2.Controls.Add(this.pictureBox1);
            this.groupBox2.Controls.Add(this.checkBoxUseProtectedConfig);
            this.groupBox2.Controls.Add(this.checkBoxOneUpDownload);
            this.groupBox2.Location = new System.Drawing.Point(12, 148);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(496, 166);
            this.groupBox2.TabIndex = 50;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Other settings";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(40, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(154, 13);
            this.label3.TabIndex = 52;
            this.label3.Text = "Default locator duration (days) :";
            // 
            // numericUpDownLocatorDuration
            // 
            this.numericUpDownLocatorDuration.Location = new System.Drawing.Point(214, 116);
            this.numericUpDownLocatorDuration.Maximum = new decimal(new int[] {
            36500,
            0,
            0,
            0});
            this.numericUpDownLocatorDuration.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownLocatorDuration.Name = "numericUpDownLocatorDuration";
            this.numericUpDownLocatorDuration.Size = new System.Drawing.Size(93, 20);
            this.numericUpDownLocatorDuration.TabIndex = 53;
            this.numericUpDownLocatorDuration.Value = new decimal(new int[] {
            365,
            0,
            0,
            0});
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::AMSExplorer.Bitmaps.change_priority;
            this.pictureBox2.Location = new System.Drawing.Point(18, 91);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(16, 16);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 51;
            this.pictureBox2.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Default job priority :";
            // 
            // numericUpDownPriority
            // 
            this.numericUpDownPriority.Location = new System.Drawing.Point(214, 90);
            this.numericUpDownPriority.Name = "numericUpDownPriority";
            this.numericUpDownPriority.Size = new System.Drawing.Size(93, 20);
            this.numericUpDownPriority.TabIndex = 50;
            this.numericUpDownPriority.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::AMSExplorer.Bitmaps.storage_encryption;
            this.pictureBox1.Location = new System.Drawing.Point(16, 23);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 49;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBoxCustomPlayer);
            this.groupBox3.Controls.Add(this.checkBoxEnableCustomPlayer);
            this.groupBox3.Location = new System.Drawing.Point(12, 455);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(496, 89);
            this.groupBox3.TabIndex = 51;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Custom player";
            // 
            // textBoxCustomPlayer
            // 
            this.textBoxCustomPlayer.Location = new System.Drawing.Point(16, 47);
            this.textBoxCustomPlayer.Name = "textBoxCustomPlayer";
            this.textBoxCustomPlayer.Size = new System.Drawing.Size(474, 20);
            this.textBoxCustomPlayer.TabIndex = 9;
            // 
            // checkBoxEnableCustomPlayer
            // 
            this.checkBoxEnableCustomPlayer.AutoSize = true;
            this.checkBoxEnableCustomPlayer.Location = new System.Drawing.Point(16, 24);
            this.checkBoxEnableCustomPlayer.Name = "checkBoxEnableCustomPlayer";
            this.checkBoxEnableCustomPlayer.Size = new System.Drawing.Size(59, 17);
            this.checkBoxEnableCustomPlayer.TabIndex = 8;
            this.checkBoxEnableCustomPlayer.Text = "Enable";
            this.checkBoxEnableCustomPlayer.UseVisualStyleBackColor = true;
            this.checkBoxEnableCustomPlayer.CheckedChanged += new System.EventHandler(this.checkBoxEnableCustomPlayer_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonOk);
            this.panel1.Controls.Add(this.buttonReset);
            this.panel1.Location = new System.Drawing.Point(-2, 558);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(523, 48);
            this.panel1.TabIndex = 63;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(40, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(153, 13);
            this.label4.TabIndex = 54;
            this.label4.Text = "Encoding price per output GB :";
            // 
            // numericUpDownAMEPrice
            // 
            this.numericUpDownAMEPrice.DecimalPlaces = 2;
            this.numericUpDownAMEPrice.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDownAMEPrice.Location = new System.Drawing.Point(260, 49);
            this.numericUpDownAMEPrice.Name = "numericUpDownAMEPrice";
            this.numericUpDownAMEPrice.Size = new System.Drawing.Size(63, 20);
            this.numericUpDownAMEPrice.TabIndex = 55;
            this.numericUpDownAMEPrice.Value = new decimal(new int[] {
            199,
            0,
            0,
            131072});
            // 
            // numericUpDownLegacyEncodingPrice
            // 
            this.numericUpDownLegacyEncodingPrice.DecimalPlaces = 2;
            this.numericUpDownLegacyEncodingPrice.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDownLegacyEncodingPrice.Location = new System.Drawing.Point(260, 75);
            this.numericUpDownLegacyEncodingPrice.Name = "numericUpDownLegacyEncodingPrice";
            this.numericUpDownLegacyEncodingPrice.Size = new System.Drawing.Size(63, 20);
            this.numericUpDownLegacyEncodingPrice.TabIndex = 57;
            this.numericUpDownLegacyEncodingPrice.Value = new decimal(new int[] {
            139,
            0,
            0,
            131072});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(40, 77);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(218, 13);
            this.label5.TabIndex = 56;
            this.label5.Text = "Legacy encoding price per input/output GB :";
            // 
            // numericUpDownIndexingPrice
            // 
            this.numericUpDownIndexingPrice.DecimalPlaces = 2;
            this.numericUpDownIndexingPrice.Enabled = false;
            this.numericUpDownIndexingPrice.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDownIndexingPrice.Location = new System.Drawing.Point(260, 101);
            this.numericUpDownIndexingPrice.Name = "numericUpDownIndexingPrice";
            this.numericUpDownIndexingPrice.Size = new System.Drawing.Size(63, 20);
            this.numericUpDownIndexingPrice.TabIndex = 59;
            this.numericUpDownIndexingPrice.Value = new decimal(new int[] {
            199,
            0,
            0,
            131072});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(39, 103);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(194, 13);
            this.label6.TabIndex = 58;
            this.label6.Text = "Content indexer price per content hour :";
            // 
            // textBoxCurrency
            // 
            this.textBoxCurrency.Location = new System.Drawing.Point(260, 23);
            this.textBoxCurrency.Name = "textBoxCurrency";
            this.textBoxCurrency.Size = new System.Drawing.Size(51, 20);
            this.textBoxCurrency.TabIndex = 60;
            this.textBoxCurrency.Text = "$";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.amspriceslink);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.textBoxCurrency);
            this.groupBox4.Controls.Add(this.numericUpDownAMEPrice);
            this.groupBox4.Controls.Add(this.numericUpDownLegacyEncodingPrice);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.numericUpDownIndexingPrice);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Location = new System.Drawing.Point(12, 320);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(496, 129);
            this.groupBox4.TabIndex = 64;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Prices (for job cost estimation)";
            // 
            // amspriceslink
            // 
            this.amspriceslink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.amspriceslink.AutoSize = true;
            this.amspriceslink.Location = new System.Drawing.Point(360, 26);
            this.amspriceslink.Name = "amspriceslink";
            this.amspriceslink.Size = new System.Drawing.Size(115, 13);
            this.amspriceslink.TabIndex = 62;
            this.amspriceslink.TabStop = true;
            this.amspriceslink.Text = "Media Services Pricing";
            this.amspriceslink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.amspriceslink_LinkClicked);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(39, 26);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(90, 13);
            this.label7.TabIndex = 61;
            this.label7.Text = "Currency symbol :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(40, 145);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(163, 13);
            this.label8.TabIndex = 54;
            this.label8.Text = "Default token duration (minutes) :";
            // 
            // numericUpDownTokenDuration
            // 
            this.numericUpDownTokenDuration.Location = new System.Drawing.Point(214, 143);
            this.numericUpDownTokenDuration.Maximum = new decimal(new int[] {
            36500,
            0,
            0,
            0});
            this.numericUpDownTokenDuration.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownTokenDuration.Name = "numericUpDownTokenDuration";
            this.numericUpDownTokenDuration.Size = new System.Drawing.Size(93, 20);
            this.numericUpDownTokenDuration.TabIndex = 55;
            this.numericUpDownTokenDuration.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // Options
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(520, 606);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Options";
            this.Text = "Options";
            this.Load += new System.EventHandler(this.options_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLocatorDuration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPriority)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAMEPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLegacyEncodingPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownIndexingPrice)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTokenDuration)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Button buttonOk;
        public System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.CheckBox checkBoxUseStorageEncryption;
        private System.Windows.Forms.CheckBox checkBoxUseProtectedConfig;
        private System.Windows.Forms.CheckBox checkBoxOneUpDownload;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxNbItems;
        public System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.CheckBox checkBoxDisplayAssetID;
        private System.Windows.Forms.CheckBox checkBoxDisplayJobID;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkBoxDisplayOriginID;
        private System.Windows.Forms.CheckBox checkBoxDisplayProgramID;
        private System.Windows.Forms.CheckBox checkBoxDisplayChannelID;
        private System.Windows.Forms.CheckBox checkBoxAutoRefresh;
        private System.Windows.Forms.ComboBox comboBoxAutoRefreshTime;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox textBoxCustomPlayer;
        private System.Windows.Forms.CheckBox checkBoxEnableCustomPlayer;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDownPriority;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDownLocatorDuration;
        private System.Windows.Forms.CheckBox checkBoxDisplayAssetStorage;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.NumericUpDown numericUpDownAMEPrice;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDownLegacyEncodingPrice;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numericUpDownIndexingPrice;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxCurrency;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.LinkLabel amspriceslink;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numericUpDownTokenDuration;
    }
}