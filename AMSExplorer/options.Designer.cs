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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Options));
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.checkBoxUseStorageEncryption = new System.Windows.Forms.CheckBox();
            this.checkBoxUseProtectedConfig = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxNbItems = new System.Windows.Forms.ComboBox();
            this.buttonReset = new System.Windows.Forms.Button();
            this.checkBoxDisplayAssetID = new System.Windows.Forms.CheckBox();
            this.checkBoxDisplayJobID = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxDisplayAssetAltId = new System.Windows.Forms.CheckBox();
            this.checkBoxDisplayBulkContId = new System.Windows.Forms.CheckBox();
            this.checkBoxDisplayAssetStorage = new System.Windows.Forms.CheckBox();
            this.checkBoxAutoRefresh = new System.Windows.Forms.CheckBox();
            this.comboBoxAutoRefreshTime = new System.Windows.Forms.ComboBox();
            this.checkBoxDisplayOriginID = new System.Windows.Forms.CheckBox();
            this.checkBoxDisplayProgramID = new System.Windows.Forms.CheckBox();
            this.checkBoxDisplayChannelID = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBoxUseAdaptiveStreamingFormat = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.numericUpDownAssetAnalysisStep = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.numericUpDownAssetAnalysisStart = new System.Windows.Forms.NumericUpDown();
            this.checkBoxShowPremiumLiveEncoding = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.numericUpDownTokenDuration = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDownLocatorDuration = new System.Windows.Forms.NumericUpDown();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownPriority = new System.Windows.Forms.NumericUpDown();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.labelcdn = new System.Windows.Forms.Label();
            this.textBoxCustomPlayer = new System.Windows.Forms.TextBox();
            this.checkBoxEnableCustomPlayer = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDownMESPrice = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownIndexingPrice = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxCurrency = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.numericUpDownPremiumWorkflowPrice = new System.Windows.Forms.NumericUpDown();
            this.amspriceslink = new System.Windows.Forms.LinkLabel();
            this.label7 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.textBoxVLCPath = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBoxffmpegPath = new System.Windows.Forms.TextBox();
            this.checkBoxHideTaskbarNotifications = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAssetAnalysisStep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAssetAnalysisStart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTokenDuration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLocatorDuration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPriority)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMESPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownIndexingPrice)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPremiumWorkflowPrice)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            resources.ApplyResources(this.buttonOk, "buttonOk");
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // checkBoxUseStorageEncryption
            // 
            resources.ApplyResources(this.checkBoxUseStorageEncryption, "checkBoxUseStorageEncryption");
            this.checkBoxUseStorageEncryption.Name = "checkBoxUseStorageEncryption";
            this.checkBoxUseStorageEncryption.UseVisualStyleBackColor = true;
            // 
            // checkBoxUseProtectedConfig
            // 
            resources.ApplyResources(this.checkBoxUseProtectedConfig, "checkBoxUseProtectedConfig");
            this.checkBoxUseProtectedConfig.Name = "checkBoxUseProtectedConfig";
            this.checkBoxUseProtectedConfig.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // comboBoxNbItems
            // 
            this.comboBoxNbItems.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.comboBoxNbItems, "comboBoxNbItems");
            this.comboBoxNbItems.FormattingEnabled = true;
            this.comboBoxNbItems.Name = "comboBoxNbItems";
            // 
            // buttonReset
            // 
            resources.ApplyResources(this.buttonReset, "buttonReset");
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // checkBoxDisplayAssetID
            // 
            resources.ApplyResources(this.checkBoxDisplayAssetID, "checkBoxDisplayAssetID");
            this.checkBoxDisplayAssetID.Name = "checkBoxDisplayAssetID";
            this.checkBoxDisplayAssetID.UseVisualStyleBackColor = true;
            // 
            // checkBoxDisplayJobID
            // 
            resources.ApplyResources(this.checkBoxDisplayJobID, "checkBoxDisplayJobID");
            this.checkBoxDisplayJobID.Name = "checkBoxDisplayJobID";
            this.checkBoxDisplayJobID.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxDisplayAssetAltId);
            this.groupBox1.Controls.Add(this.checkBoxDisplayBulkContId);
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
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // checkBoxDisplayAssetAltId
            // 
            resources.ApplyResources(this.checkBoxDisplayAssetAltId, "checkBoxDisplayAssetAltId");
            this.checkBoxDisplayAssetAltId.Name = "checkBoxDisplayAssetAltId";
            this.checkBoxDisplayAssetAltId.UseVisualStyleBackColor = true;
            // 
            // checkBoxDisplayBulkContId
            // 
            resources.ApplyResources(this.checkBoxDisplayBulkContId, "checkBoxDisplayBulkContId");
            this.checkBoxDisplayBulkContId.Name = "checkBoxDisplayBulkContId";
            this.checkBoxDisplayBulkContId.UseVisualStyleBackColor = true;
            // 
            // checkBoxDisplayAssetStorage
            // 
            resources.ApplyResources(this.checkBoxDisplayAssetStorage, "checkBoxDisplayAssetStorage");
            this.checkBoxDisplayAssetStorage.Name = "checkBoxDisplayAssetStorage";
            this.checkBoxDisplayAssetStorage.UseVisualStyleBackColor = true;
            // 
            // checkBoxAutoRefresh
            // 
            resources.ApplyResources(this.checkBoxAutoRefresh, "checkBoxAutoRefresh");
            this.checkBoxAutoRefresh.Name = "checkBoxAutoRefresh";
            this.toolTip1.SetToolTip(this.checkBoxAutoRefresh, resources.GetString("checkBoxAutoRefresh.ToolTip"));
            this.checkBoxAutoRefresh.UseVisualStyleBackColor = true;
            // 
            // comboBoxAutoRefreshTime
            // 
            this.comboBoxAutoRefreshTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.comboBoxAutoRefreshTime, "comboBoxAutoRefreshTime");
            this.comboBoxAutoRefreshTime.FormattingEnabled = true;
            this.comboBoxAutoRefreshTime.Items.AddRange(new object[] {
            resources.GetString("comboBoxAutoRefreshTime.Items"),
            resources.GetString("comboBoxAutoRefreshTime.Items1"),
            resources.GetString("comboBoxAutoRefreshTime.Items2"),
            resources.GetString("comboBoxAutoRefreshTime.Items3")});
            this.comboBoxAutoRefreshTime.Name = "comboBoxAutoRefreshTime";
            // 
            // checkBoxDisplayOriginID
            // 
            resources.ApplyResources(this.checkBoxDisplayOriginID, "checkBoxDisplayOriginID");
            this.checkBoxDisplayOriginID.Name = "checkBoxDisplayOriginID";
            this.checkBoxDisplayOriginID.UseVisualStyleBackColor = true;
            // 
            // checkBoxDisplayProgramID
            // 
            resources.ApplyResources(this.checkBoxDisplayProgramID, "checkBoxDisplayProgramID");
            this.checkBoxDisplayProgramID.Name = "checkBoxDisplayProgramID";
            this.checkBoxDisplayProgramID.UseVisualStyleBackColor = true;
            // 
            // checkBoxDisplayChannelID
            // 
            resources.ApplyResources(this.checkBoxDisplayChannelID, "checkBoxDisplayChannelID");
            this.checkBoxDisplayChannelID.Name = "checkBoxDisplayChannelID";
            this.checkBoxDisplayChannelID.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBoxHideTaskbarNotifications);
            this.groupBox2.Controls.Add(this.checkBoxUseAdaptiveStreamingFormat);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.numericUpDownAssetAnalysisStep);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.numericUpDownAssetAnalysisStart);
            this.groupBox2.Controls.Add(this.checkBoxShowPremiumLiveEncoding);
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
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // checkBoxUseAdaptiveStreamingFormat
            // 
            resources.ApplyResources(this.checkBoxUseAdaptiveStreamingFormat, "checkBoxUseAdaptiveStreamingFormat");
            this.checkBoxUseAdaptiveStreamingFormat.Name = "checkBoxUseAdaptiveStreamingFormat";
            this.checkBoxUseAdaptiveStreamingFormat.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            // 
            // numericUpDownAssetAnalysisStep
            // 
            resources.ApplyResources(this.numericUpDownAssetAnalysisStep, "numericUpDownAssetAnalysisStep");
            this.numericUpDownAssetAnalysisStep.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownAssetAnalysisStep.Name = "numericUpDownAssetAnalysisStep";
            this.numericUpDownAssetAnalysisStep.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // numericUpDownAssetAnalysisStart
            // 
            resources.ApplyResources(this.numericUpDownAssetAnalysisStart, "numericUpDownAssetAnalysisStart");
            this.numericUpDownAssetAnalysisStart.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownAssetAnalysisStart.Name = "numericUpDownAssetAnalysisStart";
            this.numericUpDownAssetAnalysisStart.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // checkBoxShowPremiumLiveEncoding
            // 
            resources.ApplyResources(this.checkBoxShowPremiumLiveEncoding, "checkBoxShowPremiumLiveEncoding");
            this.checkBoxShowPremiumLiveEncoding.Name = "checkBoxShowPremiumLiveEncoding";
            this.checkBoxShowPremiumLiveEncoding.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // numericUpDownTokenDuration
            // 
            resources.ApplyResources(this.numericUpDownTokenDuration, "numericUpDownTokenDuration");
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
            this.numericUpDownTokenDuration.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // numericUpDownLocatorDuration
            // 
            resources.ApplyResources(this.numericUpDownLocatorDuration, "numericUpDownLocatorDuration");
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
            this.numericUpDownLocatorDuration.Value = new decimal(new int[] {
            3650,
            0,
            0,
            0});
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::AMSExplorer.Bitmaps.change_priority;
            resources.ApplyResources(this.pictureBox2, "pictureBox2");
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.TabStop = false;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // numericUpDownPriority
            // 
            resources.ApplyResources(this.numericUpDownPriority, "numericUpDownPriority");
            this.numericUpDownPriority.Name = "numericUpDownPriority";
            this.numericUpDownPriority.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::AMSExplorer.Bitmaps.storage_encryption;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.labelcdn);
            this.groupBox3.Controls.Add(this.textBoxCustomPlayer);
            this.groupBox3.Controls.Add(this.checkBoxEnableCustomPlayer);
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // labelcdn
            // 
            resources.ApplyResources(this.labelcdn, "labelcdn");
            this.labelcdn.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.labelcdn.Name = "labelcdn";
            // 
            // textBoxCustomPlayer
            // 
            resources.ApplyResources(this.textBoxCustomPlayer, "textBoxCustomPlayer");
            this.textBoxCustomPlayer.Name = "textBoxCustomPlayer";
            // 
            // checkBoxEnableCustomPlayer
            // 
            resources.ApplyResources(this.checkBoxEnableCustomPlayer, "checkBoxEnableCustomPlayer");
            this.checkBoxEnableCustomPlayer.Name = "checkBoxEnableCustomPlayer";
            this.checkBoxEnableCustomPlayer.UseVisualStyleBackColor = true;
            this.checkBoxEnableCustomPlayer.CheckedChanged += new System.EventHandler(this.checkBoxEnableCustomPlayer_CheckedChanged);
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonOk);
            this.panel1.Controls.Add(this.buttonReset);
            this.panel1.Name = "panel1";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // numericUpDownMESPrice
            // 
            this.numericUpDownMESPrice.DecimalPlaces = 3;
            resources.ApplyResources(this.numericUpDownMESPrice, "numericUpDownMESPrice");
            this.numericUpDownMESPrice.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numericUpDownMESPrice.Name = "numericUpDownMESPrice";
            this.numericUpDownMESPrice.Value = new decimal(new int[] {
            15,
            0,
            0,
            196608});
            // 
            // numericUpDownIndexingPrice
            // 
            this.numericUpDownIndexingPrice.DecimalPlaces = 2;
            resources.ApplyResources(this.numericUpDownIndexingPrice, "numericUpDownIndexingPrice");
            this.numericUpDownIndexingPrice.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDownIndexingPrice.Name = "numericUpDownIndexingPrice";
            this.numericUpDownIndexingPrice.Value = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // textBoxCurrency
            // 
            resources.ApplyResources(this.textBoxCurrency, "textBoxCurrency");
            this.textBoxCurrency.Name = "textBoxCurrency";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.numericUpDownPremiumWorkflowPrice);
            this.groupBox4.Controls.Add(this.amspriceslink);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.textBoxCurrency);
            this.groupBox4.Controls.Add(this.numericUpDownMESPrice);
            this.groupBox4.Controls.Add(this.numericUpDownIndexingPrice);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.label4);
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // numericUpDownPremiumWorkflowPrice
            // 
            this.numericUpDownPremiumWorkflowPrice.DecimalPlaces = 3;
            resources.ApplyResources(this.numericUpDownPremiumWorkflowPrice, "numericUpDownPremiumWorkflowPrice");
            this.numericUpDownPremiumWorkflowPrice.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numericUpDownPremiumWorkflowPrice.Name = "numericUpDownPremiumWorkflowPrice";
            this.numericUpDownPremiumWorkflowPrice.Value = new decimal(new int[] {
            35,
            0,
            0,
            196608});
            // 
            // amspriceslink
            // 
            resources.ApplyResources(this.amspriceslink, "amspriceslink");
            this.amspriceslink.Name = "amspriceslink";
            this.amspriceslink.TabStop = true;
            this.amspriceslink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.amspriceslink_LinkClicked);
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // tabControl1
            // 
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.groupBox2);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox4);
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.groupBox5);
            this.tabPage3.Controls.Add(this.groupBox3);
            resources.ApplyResources(this.tabPage3, "tabPage3");
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label11);
            this.groupBox5.Controls.Add(this.textBoxVLCPath);
            this.groupBox5.Controls.Add(this.label10);
            this.groupBox5.Controls.Add(this.textBoxffmpegPath);
            resources.ApplyResources(this.groupBox5, "groupBox5");
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.TabStop = false;
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // textBoxVLCPath
            // 
            resources.ApplyResources(this.textBoxVLCPath, "textBoxVLCPath");
            this.textBoxVLCPath.Name = "textBoxVLCPath";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // textBoxffmpegPath
            // 
            resources.ApplyResources(this.textBoxffmpegPath, "textBoxffmpegPath");
            this.textBoxffmpegPath.Name = "textBoxffmpegPath";
            // 
            // checkBoxHideTaskbarNotifications
            // 
            resources.ApplyResources(this.checkBoxHideTaskbarNotifications, "checkBoxHideTaskbarNotifications");
            this.checkBoxHideTaskbarNotifications.Name = "checkBoxHideTaskbarNotifications";
            this.checkBoxHideTaskbarNotifications.UseVisualStyleBackColor = true;
            // 
            // Options
            // 
            this.AcceptButton = this.buttonOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.Name = "Options";
            this.Load += new System.EventHandler(this.options_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAssetAnalysisStep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAssetAnalysisStart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTokenDuration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLocatorDuration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPriority)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMESPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownIndexingPrice)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPremiumWorkflowPrice)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Button buttonOk;
        public System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.CheckBox checkBoxUseStorageEncryption;
        private System.Windows.Forms.CheckBox checkBoxUseProtectedConfig;
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
        private System.Windows.Forms.NumericUpDown numericUpDownMESPrice;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDownIndexingPrice;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxCurrency;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.LinkLabel amspriceslink;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numericUpDownTokenDuration;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown numericUpDownPremiumWorkflowPrice;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBoxVLCPath;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBoxffmpegPath;
        private System.Windows.Forms.Label labelcdn;
        private System.Windows.Forms.CheckBox checkBoxDisplayBulkContId;
        private System.Windows.Forms.CheckBox checkBoxDisplayAssetAltId;
        private System.Windows.Forms.CheckBox checkBoxShowPremiumLiveEncoding;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.NumericUpDown numericUpDownAssetAnalysisStep;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numericUpDownAssetAnalysisStart;
        private System.Windows.Forms.CheckBox checkBoxUseAdaptiveStreamingFormat;
        private System.Windows.Forms.CheckBox checkBoxHideTaskbarNotifications;
    }
}