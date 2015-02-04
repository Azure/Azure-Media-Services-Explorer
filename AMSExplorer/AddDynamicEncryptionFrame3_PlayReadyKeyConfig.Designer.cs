namespace AMSExplorer
{
    partial class AddDynamicEncryptionFrame3_PlayReadyKeyConfig
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
            this.moreinfotestserver = new System.Windows.Forms.LinkLabel();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label = new System.Windows.Forms.Label();
            this.buttonPlayReadyTestSettings = new System.Windows.Forms.Button();
            this.textBoxLAurl = new System.Windows.Forms.TextBox();
            this.buttongenerateContentKey = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxkeyseed = new System.Windows.Forms.TextBox();
            this.buttonGenKeyID = new System.Windows.Forms.Button();
            this.textBoxkeyid = new System.Windows.Forms.TextBox();
            this.textBoxcontentkey = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonOk = new System.Windows.Forms.Button();
            this.panelPlayReadyTest = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.radioButtonContentKeyBase64 = new System.Windows.Forms.RadioButton();
            this.radioButtonContentKeyHex = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.radioButtonKeySeedBase64 = new System.Windows.Forms.RadioButton();
            this.radioButtonKeySeedHex = new System.Windows.Forms.RadioButton();
            this.panel4 = new System.Windows.Forms.Panel();
            this.radioButtonKeyIDGuid = new System.Windows.Forms.RadioButton();
            this.radioButtonKeyIDBase64 = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.radioButtonKeySpecifiedByUser = new System.Windows.Forms.RadioButton();
            this.radioButtonKeyRandomGeneration = new System.Windows.Forms.RadioButton();
            this.groupBoxCrypto = new System.Windows.Forms.GroupBox();
            this.textBoxContentKeyCalculated = new System.Windows.Forms.TextBox();
            this.panelKeyId = new System.Windows.Forms.Panel();
            this.panelContentKey = new System.Windows.Forms.Panel();
            this.panelPlayReadyTest.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBoxCrypto.SuspendLayout();
            this.panelKeyId.SuspendLayout();
            this.panelContentKey.SuspendLayout();
            this.SuspendLayout();
            // 
            // moreinfotestserver
            // 
            this.moreinfotestserver.AutoSize = true;
            this.moreinfotestserver.Location = new System.Drawing.Point(178, 35);
            this.moreinfotestserver.Name = "moreinfotestserver";
            this.moreinfotestserver.Size = new System.Drawing.Size(152, 13);
            this.moreinfotestserver.TabIndex = 19;
            this.moreinfotestserver.TabStop = true;
            this.moreinfotestserver.Text = "PlayReady test server web site";
            this.moreinfotestserver.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.moreinfotestserver_LinkClicked);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(486, 11);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(92, 23);
            this.buttonCancel.TabIndex = 16;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(176, 17);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(264, 13);
            this.label.TabIndex = 28;
            this.label.Text = "You can specifiy your own data or use the test settings";
            // 
            // buttonPlayReadyTestSettings
            // 
            this.buttonPlayReadyTestSettings.Location = new System.Drawing.Point(11, 18);
            this.buttonPlayReadyTestSettings.Name = "buttonPlayReadyTestSettings";
            this.buttonPlayReadyTestSettings.Size = new System.Drawing.Size(159, 32);
            this.buttonPlayReadyTestSettings.TabIndex = 29;
            this.buttonPlayReadyTestSettings.Text = "Use PlayReady Test Settings";
            this.buttonPlayReadyTestSettings.UseVisualStyleBackColor = true;
            this.buttonPlayReadyTestSettings.Click += new System.EventHandler(this.buttonPlayReadyTestSettings_Click);
            // 
            // textBoxLAurl
            // 
            this.textBoxLAurl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxLAurl.Location = new System.Drawing.Point(11, 195);
            this.textBoxLAurl.Name = "textBoxLAurl";
            this.textBoxLAurl.Size = new System.Drawing.Size(382, 20);
            this.textBoxLAurl.TabIndex = 37;
            this.textBoxLAurl.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // buttongenerateContentKey
            // 
            this.buttongenerateContentKey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttongenerateContentKey.Location = new System.Drawing.Point(471, 38);
            this.buttongenerateContentKey.Name = "buttongenerateContentKey";
            this.buttongenerateContentKey.Size = new System.Drawing.Size(64, 25);
            this.buttongenerateContentKey.TabIndex = 40;
            this.buttongenerateContentKey.Text = "Generate";
            this.buttongenerateContentKey.UseVisualStyleBackColor = true;
            this.buttongenerateContentKey.Click += new System.EventHandler(this.buttongenerateContentKey_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 222);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 36;
            this.label1.Text = "Key Seed:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 179);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 13);
            this.label2.TabIndex = 35;
            this.label2.Text = "License Acquisition Url:";
            // 
            // textBoxkeyseed
            // 
            this.textBoxkeyseed.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxkeyseed.Location = new System.Drawing.Point(11, 238);
            this.textBoxkeyseed.Name = "textBoxkeyseed";
            this.textBoxkeyseed.Size = new System.Drawing.Size(382, 20);
            this.textBoxkeyseed.TabIndex = 34;
            this.textBoxkeyseed.TextChanged += new System.EventHandler(this.textBoxkeyseed_TextChanged);
            // 
            // buttonGenKeyID
            // 
            this.buttonGenKeyID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGenKeyID.Location = new System.Drawing.Point(471, 26);
            this.buttonGenKeyID.Name = "buttonGenKeyID";
            this.buttonGenKeyID.Size = new System.Drawing.Size(64, 25);
            this.buttonGenKeyID.TabIndex = 33;
            this.buttonGenKeyID.Text = "Generate";
            this.buttonGenKeyID.UseVisualStyleBackColor = true;
            this.buttonGenKeyID.Click += new System.EventHandler(this.buttonGenKeyID_Click_1);
            // 
            // textBoxkeyid
            // 
            this.textBoxkeyid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxkeyid.Location = new System.Drawing.Point(5, 29);
            this.textBoxkeyid.Name = "textBoxkeyid";
            this.textBoxkeyid.Size = new System.Drawing.Size(382, 20);
            this.textBoxkeyid.TabIndex = 32;
            this.textBoxkeyid.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // textBoxcontentkey
            // 
            this.textBoxcontentkey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxcontentkey.Location = new System.Drawing.Point(6, 22);
            this.textBoxcontentkey.Name = "textBoxcontentkey";
            this.textBoxcontentkey.Size = new System.Drawing.Size(381, 20);
            this.textBoxcontentkey.TabIndex = 35;
            this.textBoxcontentkey.TextChanged += new System.EventHandler(this.textBoxcontentkey_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 13);
            this.label5.TabIndex = 31;
            this.label5.Text = "Key ID:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 34;
            this.label4.Text = "Content key:";
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Image = global::AMSExplorer.Bitmaps.DRM_protection;
            this.buttonOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonOk.Location = new System.Drawing.Point(329, 11);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(151, 23);
            this.buttonOk.TabIndex = 17;
            this.buttonOk.Tag = "";
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // panelPlayReadyTest
            // 
            this.panelPlayReadyTest.Controls.Add(this.label);
            this.panelPlayReadyTest.Controls.Add(this.moreinfotestserver);
            this.panelPlayReadyTest.Controls.Add(this.buttonPlayReadyTestSettings);
            this.panelPlayReadyTest.Location = new System.Drawing.Point(11, 30);
            this.panelPlayReadyTest.Name = "panelPlayReadyTest";
            this.panelPlayReadyTest.Size = new System.Drawing.Size(455, 73);
            this.panelPlayReadyTest.TabIndex = 41;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonOk);
            this.panel1.Location = new System.Drawing.Point(-1, 615);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(586, 48);
            this.panel1.TabIndex = 63;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label8.Location = new System.Drawing.Point(399, 198);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(26, 13);
            this.label8.TabIndex = 67;
            this.label8.Text = "(Url)";
            // 
            // radioButtonContentKeyBase64
            // 
            this.radioButtonContentKeyBase64.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonContentKeyBase64.AutoSize = true;
            this.radioButtonContentKeyBase64.Checked = true;
            this.radioButtonContentKeyBase64.Location = new System.Drawing.Point(8, 6);
            this.radioButtonContentKeyBase64.Name = "radioButtonContentKeyBase64";
            this.radioButtonContentKeyBase64.Size = new System.Drawing.Size(61, 17);
            this.radioButtonContentKeyBase64.TabIndex = 68;
            this.radioButtonContentKeyBase64.TabStop = true;
            this.radioButtonContentKeyBase64.Text = "Base64";
            this.radioButtonContentKeyBase64.UseVisualStyleBackColor = true;
            this.radioButtonContentKeyBase64.CheckedChanged += new System.EventHandler(this.radioButtonGuid_CheckedChanged);
            // 
            // radioButtonContentKeyHex
            // 
            this.radioButtonContentKeyHex.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonContentKeyHex.AutoSize = true;
            this.radioButtonContentKeyHex.Location = new System.Drawing.Point(8, 28);
            this.radioButtonContentKeyHex.Name = "radioButtonContentKeyHex";
            this.radioButtonContentKeyHex.Size = new System.Drawing.Size(44, 17);
            this.radioButtonContentKeyHex.TabIndex = 69;
            this.radioButtonContentKeyHex.Text = "Hex";
            this.radioButtonContentKeyHex.UseVisualStyleBackColor = true;
            this.radioButtonContentKeyHex.CheckedChanged += new System.EventHandler(this.radioButtonHex_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.radioButtonContentKeyBase64);
            this.panel2.Controls.Add(this.radioButtonContentKeyHex);
            this.panel2.Location = new System.Drawing.Point(393, 22);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(75, 48);
            this.panel2.TabIndex = 70;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.radioButtonKeySeedBase64);
            this.panel3.Controls.Add(this.radioButtonKeySeedHex);
            this.panel3.Location = new System.Drawing.Point(399, 222);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(75, 48);
            this.panel3.TabIndex = 71;
            // 
            // radioButtonKeySeedBase64
            // 
            this.radioButtonKeySeedBase64.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonKeySeedBase64.AutoSize = true;
            this.radioButtonKeySeedBase64.Checked = true;
            this.radioButtonKeySeedBase64.Location = new System.Drawing.Point(8, 3);
            this.radioButtonKeySeedBase64.Name = "radioButtonKeySeedBase64";
            this.radioButtonKeySeedBase64.Size = new System.Drawing.Size(61, 17);
            this.radioButtonKeySeedBase64.TabIndex = 68;
            this.radioButtonKeySeedBase64.TabStop = true;
            this.radioButtonKeySeedBase64.Text = "Base64";
            this.radioButtonKeySeedBase64.UseVisualStyleBackColor = true;
            this.radioButtonKeySeedBase64.CheckedChanged += new System.EventHandler(this.radioButtonKeySeedBase64_CheckedChanged);
            // 
            // radioButtonKeySeedHex
            // 
            this.radioButtonKeySeedHex.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonKeySeedHex.AutoSize = true;
            this.radioButtonKeySeedHex.Location = new System.Drawing.Point(8, 25);
            this.radioButtonKeySeedHex.Name = "radioButtonKeySeedHex";
            this.radioButtonKeySeedHex.Size = new System.Drawing.Size(44, 17);
            this.radioButtonKeySeedHex.TabIndex = 69;
            this.radioButtonKeySeedHex.Text = "Hex";
            this.radioButtonKeySeedHex.UseVisualStyleBackColor = true;
            this.radioButtonKeySeedHex.CheckedChanged += new System.EventHandler(this.radioButtonKeySeedHex_CheckedChanged);
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.Controls.Add(this.radioButtonKeyIDGuid);
            this.panel4.Controls.Add(this.radioButtonKeyIDBase64);
            this.panel4.Location = new System.Drawing.Point(393, 13);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(75, 48);
            this.panel4.TabIndex = 72;
            // 
            // radioButtonKeyIDGuid
            // 
            this.radioButtonKeyIDGuid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonKeyIDGuid.AutoSize = true;
            this.radioButtonKeyIDGuid.Checked = true;
            this.radioButtonKeyIDGuid.Location = new System.Drawing.Point(8, 3);
            this.radioButtonKeyIDGuid.Name = "radioButtonKeyIDGuid";
            this.radioButtonKeyIDGuid.Size = new System.Drawing.Size(47, 17);
            this.radioButtonKeyIDGuid.TabIndex = 68;
            this.radioButtonKeyIDGuid.TabStop = true;
            this.radioButtonKeyIDGuid.Text = "Guid";
            this.radioButtonKeyIDGuid.UseVisualStyleBackColor = true;
            this.radioButtonKeyIDGuid.CheckedChanged += new System.EventHandler(this.radioButtonKeyIDGuid_CheckedChanged);
            // 
            // radioButtonKeyIDBase64
            // 
            this.radioButtonKeyIDBase64.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonKeyIDBase64.AutoSize = true;
            this.radioButtonKeyIDBase64.Location = new System.Drawing.Point(8, 25);
            this.radioButtonKeyIDBase64.Name = "radioButtonKeyIDBase64";
            this.radioButtonKeyIDBase64.Size = new System.Drawing.Size(61, 17);
            this.radioButtonKeyIDBase64.TabIndex = 69;
            this.radioButtonKeyIDBase64.Text = "Base64";
            this.radioButtonKeyIDBase64.UseVisualStyleBackColor = true;
            this.radioButtonKeyIDBase64.CheckedChanged += new System.EventHandler(this.radioButtonKeyIDBase64_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.radioButtonKeySpecifiedByUser);
            this.groupBox2.Controls.Add(this.radioButtonKeyRandomGeneration);
            this.groupBox2.Location = new System.Drawing.Point(12, 27);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(552, 100);
            this.groupBox2.TabIndex = 73;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "CENC Content key\'s generation";
            // 
            // label3
            // 
            this.label3.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label3.Location = new System.Drawing.Point(31, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(493, 19);
            this.label3.TabIndex = 68;
            this.label3.Text = "Explorer will use the existing CENC key attached to the asset. If there is none, " +
    "a key must be created :";
            // 
            // radioButtonKeySpecifiedByUser
            // 
            this.radioButtonKeySpecifiedByUser.AutoSize = true;
            this.radioButtonKeySpecifiedByUser.Location = new System.Drawing.Point(34, 70);
            this.radioButtonKeySpecifiedByUser.Name = "radioButtonKeySpecifiedByUser";
            this.radioButtonKeySpecifiedByUser.Size = new System.Drawing.Size(124, 17);
            this.radioButtonKeySpecifiedByUser.TabIndex = 1;
            this.radioButtonKeySpecifiedByUser.Text = "Specified by the user";
            this.radioButtonKeySpecifiedByUser.UseVisualStyleBackColor = true;
            this.radioButtonKeySpecifiedByUser.CheckedChanged += new System.EventHandler(this.radioButtonKeySpecifiedByUser_CheckedChanged);
            // 
            // radioButtonKeyRandomGeneration
            // 
            this.radioButtonKeyRandomGeneration.AutoSize = true;
            this.radioButtonKeyRandomGeneration.Checked = true;
            this.radioButtonKeyRandomGeneration.Location = new System.Drawing.Point(34, 47);
            this.radioButtonKeyRandomGeneration.Name = "radioButtonKeyRandomGeneration";
            this.radioButtonKeyRandomGeneration.Size = new System.Drawing.Size(118, 17);
            this.radioButtonKeyRandomGeneration.TabIndex = 0;
            this.radioButtonKeyRandomGeneration.TabStop = true;
            this.radioButtonKeyRandomGeneration.Text = "Random generation";
            this.radioButtonKeyRandomGeneration.UseVisualStyleBackColor = true;
            // 
            // groupBoxCrypto
            // 
            this.groupBoxCrypto.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxCrypto.Controls.Add(this.panelContentKey);
            this.groupBoxCrypto.Controls.Add(this.panelKeyId);
            this.groupBoxCrypto.Controls.Add(this.panelPlayReadyTest);
            this.groupBoxCrypto.Controls.Add(this.panel3);
            this.groupBoxCrypto.Controls.Add(this.label8);
            this.groupBoxCrypto.Controls.Add(this.label1);
            this.groupBoxCrypto.Controls.Add(this.label2);
            this.groupBoxCrypto.Controls.Add(this.textBoxkeyseed);
            this.groupBoxCrypto.Controls.Add(this.textBoxLAurl);
            this.groupBoxCrypto.Enabled = false;
            this.groupBoxCrypto.Location = new System.Drawing.Point(15, 145);
            this.groupBoxCrypto.Name = "groupBoxCrypto";
            this.groupBoxCrypto.Size = new System.Drawing.Size(549, 363);
            this.groupBoxCrypto.TabIndex = 74;
            this.groupBoxCrypto.TabStop = false;
            this.groupBoxCrypto.Text = "Cryptography";
            // 
            // textBoxContentKeyCalculated
            // 
            this.textBoxContentKeyCalculated.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxContentKeyCalculated.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.textBoxContentKeyCalculated.Location = new System.Drawing.Point(6, 48);
            this.textBoxContentKeyCalculated.Name = "textBoxContentKeyCalculated";
            this.textBoxContentKeyCalculated.ReadOnly = true;
            this.textBoxContentKeyCalculated.Size = new System.Drawing.Size(381, 20);
            this.textBoxContentKeyCalculated.TabIndex = 73;
            // 
            // panelKeyId
            // 
            this.panelKeyId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelKeyId.Controls.Add(this.textBoxkeyid);
            this.panelKeyId.Controls.Add(this.buttonGenKeyID);
            this.panelKeyId.Controls.Add(this.label5);
            this.panelKeyId.Controls.Add(this.panel4);
            this.panelKeyId.Location = new System.Drawing.Point(6, 109);
            this.panelKeyId.Name = "panelKeyId";
            this.panelKeyId.Size = new System.Drawing.Size(543, 67);
            this.panelKeyId.TabIndex = 74;
            // 
            // panelContentKey
            // 
            this.panelContentKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelContentKey.Controls.Add(this.label4);
            this.panelContentKey.Controls.Add(this.textBoxContentKeyCalculated);
            this.panelContentKey.Controls.Add(this.textBoxcontentkey);
            this.panelContentKey.Controls.Add(this.buttongenerateContentKey);
            this.panelContentKey.Controls.Add(this.panel2);
            this.panelContentKey.Location = new System.Drawing.Point(6, 276);
            this.panelContentKey.Name = "panelContentKey";
            this.panelContentKey.Size = new System.Drawing.Size(543, 81);
            this.panelContentKey.TabIndex = 75;
            // 
            // AddDynamicEncryptionFrame3_PlayReadyKeyConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(584, 661);
            this.Controls.Add(this.groupBoxCrypto);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.panel1);
            this.Name = "AddDynamicEncryptionFrame3_PlayReadyKeyConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Dynamic Encryption - Step 3";
            this.Load += new System.EventHandler(this.PlayReadyExternalServer_Load);
            this.panelPlayReadyTest.ResumeLayout(false);
            this.panelPlayReadyTest.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBoxCrypto.ResumeLayout(false);
            this.groupBoxCrypto.PerformLayout();
            this.panelKeyId.ResumeLayout(false);
            this.panelKeyId.PerformLayout();
            this.panelContentKey.ResumeLayout(false);
            this.panelContentKey.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.LinkLabel moreinfotestserver;
        public System.Windows.Forms.Button buttonOk;
        public System.Windows.Forms.Button buttonCancel;
        public System.Windows.Forms.Label label;
        private System.Windows.Forms.Button buttonPlayReadyTestSettings;
        public System.Windows.Forms.TextBox textBoxLAurl;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox textBoxkeyseed;
        private System.Windows.Forms.Button buttonGenKeyID;
        public System.Windows.Forms.TextBox textBoxkeyid;
        public System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox textBoxcontentkey;
        public System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttongenerateContentKey;
        private System.Windows.Forms.Panel panelPlayReadyTest;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.RadioButton radioButtonContentKeyBase64;
        private System.Windows.Forms.RadioButton radioButtonContentKeyHex;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton radioButtonKeySeedBase64;
        private System.Windows.Forms.RadioButton radioButtonKeySeedHex;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.RadioButton radioButtonKeyIDGuid;
        private System.Windows.Forms.RadioButton radioButtonKeyIDBase64;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton radioButtonKeySpecifiedByUser;
        private System.Windows.Forms.RadioButton radioButtonKeyRandomGeneration;
        private System.Windows.Forms.GroupBox groupBoxCrypto;
        public System.Windows.Forms.TextBox textBoxContentKeyCalculated;
        private System.Windows.Forms.Panel panelContentKey;
        private System.Windows.Forms.Panel panelKeyId;
    }
}