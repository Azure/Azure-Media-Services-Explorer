namespace AMSExplorer
{
    partial class AddDynamicEncryptionFrame2_PlayReadyKeyConfig
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
            this.checkBoxEncodingSL = new System.Windows.Forms.CheckBox();
            this.panelContentKey = new System.Windows.Forms.Panel();
            this.textBoxContentKeyCalculated = new System.Windows.Forms.TextBox();
            this.panelKeyId = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxCustomAttributes = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label9 = new System.Windows.Forms.Label();
            this.panelPlayReadyTest.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBoxCrypto.SuspendLayout();
            this.panelContentKey.SuspendLayout();
            this.panelKeyId.SuspendLayout();
            this.SuspendLayout();
            // 
            // moreinfotestserver
            // 
            this.moreinfotestserver.AutoSize = true;
            this.moreinfotestserver.Location = new System.Drawing.Point(208, 30);
            this.moreinfotestserver.Name = "moreinfotestserver";
            this.moreinfotestserver.Size = new System.Drawing.Size(163, 15);
            this.moreinfotestserver.TabIndex = 19;
            this.moreinfotestserver.TabStop = true;
            this.moreinfotestserver.Text = "PlayReady test server web site";
            this.moreinfotestserver.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.moreinfotestserver_LinkClicked);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(567, 13);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(107, 27);
            this.buttonCancel.TabIndex = 16;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(205, 9);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(292, 15);
            this.label.TabIndex = 28;
            this.label.Text = "You can specifiy your own data or use the test settings";
            // 
            // buttonPlayReadyTestSettings
            // 
            this.buttonPlayReadyTestSettings.Location = new System.Drawing.Point(13, 10);
            this.buttonPlayReadyTestSettings.Name = "buttonPlayReadyTestSettings";
            this.buttonPlayReadyTestSettings.Size = new System.Drawing.Size(185, 37);
            this.buttonPlayReadyTestSettings.TabIndex = 29;
            this.buttonPlayReadyTestSettings.Text = "Use PlayReady Test Settings";
            this.buttonPlayReadyTestSettings.UseVisualStyleBackColor = true;
            this.buttonPlayReadyTestSettings.Click += new System.EventHandler(this.buttonPlayReadyTestSettings_Click);
            // 
            // textBoxLAurl
            // 
            this.textBoxLAurl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxLAurl.Location = new System.Drawing.Point(13, 207);
            this.textBoxLAurl.Name = "textBoxLAurl";
            this.textBoxLAurl.Size = new System.Drawing.Size(445, 23);
            this.textBoxLAurl.TabIndex = 37;
            this.textBoxLAurl.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // buttongenerateContentKey
            // 
            this.buttongenerateContentKey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttongenerateContentKey.Location = new System.Drawing.Point(544, 44);
            this.buttongenerateContentKey.Name = "buttongenerateContentKey";
            this.buttongenerateContentKey.Size = new System.Drawing.Size(75, 29);
            this.buttongenerateContentKey.TabIndex = 40;
            this.buttongenerateContentKey.Text = "Generate";
            this.buttongenerateContentKey.UseVisualStyleBackColor = true;
            this.buttongenerateContentKey.Click += new System.EventHandler(this.buttongenerateContentKey_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 238);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 15);
            this.label1.TabIndex = 36;
            this.label1.Text = "Key Seed:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 188);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 15);
            this.label2.TabIndex = 35;
            this.label2.Text = "License Acquisition Url:";
            // 
            // textBoxkeyseed
            // 
            this.textBoxkeyseed.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxkeyseed.Location = new System.Drawing.Point(13, 256);
            this.textBoxkeyseed.Name = "textBoxkeyseed";
            this.textBoxkeyseed.Size = new System.Drawing.Size(445, 23);
            this.textBoxkeyseed.TabIndex = 34;
            this.textBoxkeyseed.TextChanged += new System.EventHandler(this.textBoxkeyseed_TextChanged);
            // 
            // buttonGenKeyID
            // 
            this.buttonGenKeyID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGenKeyID.Location = new System.Drawing.Point(544, 30);
            this.buttonGenKeyID.Name = "buttonGenKeyID";
            this.buttonGenKeyID.Size = new System.Drawing.Size(75, 29);
            this.buttonGenKeyID.TabIndex = 33;
            this.buttonGenKeyID.Text = "Generate";
            this.buttonGenKeyID.UseVisualStyleBackColor = true;
            this.buttonGenKeyID.Click += new System.EventHandler(this.buttonGenKeyID_Click_1);
            // 
            // textBoxkeyid
            // 
            this.textBoxkeyid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxkeyid.Location = new System.Drawing.Point(6, 33);
            this.textBoxkeyid.Name = "textBoxkeyid";
            this.textBoxkeyid.Size = new System.Drawing.Size(439, 23);
            this.textBoxkeyid.TabIndex = 32;
            this.textBoxkeyid.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // textBoxcontentkey
            // 
            this.textBoxcontentkey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxcontentkey.Location = new System.Drawing.Point(7, 25);
            this.textBoxcontentkey.Name = "textBoxcontentkey";
            this.textBoxcontentkey.Size = new System.Drawing.Size(438, 23);
            this.textBoxcontentkey.TabIndex = 35;
            this.textBoxcontentkey.TextChanged += new System.EventHandler(this.textBoxcontentkey_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 15);
            this.label5.TabIndex = 31;
            this.label5.Text = "Key ID:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 15);
            this.label4.TabIndex = 34;
            this.label4.Text = "Content key:";
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Image = global::AMSExplorer.Bitmaps.DRM_protection;
            this.buttonOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonOk.Location = new System.Drawing.Point(384, 13);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(176, 27);
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
            this.panelPlayReadyTest.Location = new System.Drawing.Point(13, 28);
            this.panelPlayReadyTest.Name = "panelPlayReadyTest";
            this.panelPlayReadyTest.Size = new System.Drawing.Size(531, 57);
            this.panelPlayReadyTest.TabIndex = 41;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonOk);
            this.panel1.Location = new System.Drawing.Point(-1, 710);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(684, 55);
            this.panel1.TabIndex = 63;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label8.Location = new System.Drawing.Point(465, 210);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(30, 15);
            this.label8.TabIndex = 67;
            this.label8.Text = "(Url)";
            // 
            // radioButtonContentKeyBase64
            // 
            this.radioButtonContentKeyBase64.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonContentKeyBase64.AutoSize = true;
            this.radioButtonContentKeyBase64.Checked = true;
            this.radioButtonContentKeyBase64.Location = new System.Drawing.Point(9, 7);
            this.radioButtonContentKeyBase64.Name = "radioButtonContentKeyBase64";
            this.radioButtonContentKeyBase64.Size = new System.Drawing.Size(61, 19);
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
            this.radioButtonContentKeyHex.Location = new System.Drawing.Point(9, 32);
            this.radioButtonContentKeyHex.Name = "radioButtonContentKeyHex";
            this.radioButtonContentKeyHex.Size = new System.Drawing.Size(45, 19);
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
            this.panel2.Location = new System.Drawing.Point(453, 25);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(87, 55);
            this.panel2.TabIndex = 70;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.radioButtonKeySeedBase64);
            this.panel3.Controls.Add(this.radioButtonKeySeedHex);
            this.panel3.Location = new System.Drawing.Point(465, 238);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(87, 55);
            this.panel3.TabIndex = 71;
            // 
            // radioButtonKeySeedBase64
            // 
            this.radioButtonKeySeedBase64.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonKeySeedBase64.AutoSize = true;
            this.radioButtonKeySeedBase64.Checked = true;
            this.radioButtonKeySeedBase64.Location = new System.Drawing.Point(9, 3);
            this.radioButtonKeySeedBase64.Name = "radioButtonKeySeedBase64";
            this.radioButtonKeySeedBase64.Size = new System.Drawing.Size(61, 19);
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
            this.radioButtonKeySeedHex.Location = new System.Drawing.Point(9, 29);
            this.radioButtonKeySeedHex.Name = "radioButtonKeySeedHex";
            this.radioButtonKeySeedHex.Size = new System.Drawing.Size(45, 19);
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
            this.panel4.Location = new System.Drawing.Point(453, 15);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(87, 55);
            this.panel4.TabIndex = 72;
            // 
            // radioButtonKeyIDGuid
            // 
            this.radioButtonKeyIDGuid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonKeyIDGuid.AutoSize = true;
            this.radioButtonKeyIDGuid.Checked = true;
            this.radioButtonKeyIDGuid.Location = new System.Drawing.Point(9, 3);
            this.radioButtonKeyIDGuid.Name = "radioButtonKeyIDGuid";
            this.radioButtonKeyIDGuid.Size = new System.Drawing.Size(50, 19);
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
            this.radioButtonKeyIDBase64.Location = new System.Drawing.Point(9, 29);
            this.radioButtonKeyIDBase64.Name = "radioButtonKeyIDBase64";
            this.radioButtonKeyIDBase64.Size = new System.Drawing.Size(61, 19);
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
            this.groupBox2.Location = new System.Drawing.Point(14, 82);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(644, 115);
            this.groupBox2.TabIndex = 73;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "CENC Content key\'s generation";
            // 
            // label3
            // 
            this.label3.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label3.Location = new System.Drawing.Point(36, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(575, 22);
            this.label3.TabIndex = 68;
            this.label3.Text = "Explorer will use the existing CENC key attached to the asset. If there is none, " +
    "a key must be created :";
            // 
            // radioButtonKeySpecifiedByUser
            // 
            this.radioButtonKeySpecifiedByUser.AutoSize = true;
            this.radioButtonKeySpecifiedByUser.Location = new System.Drawing.Point(40, 81);
            this.radioButtonKeySpecifiedByUser.Name = "radioButtonKeySpecifiedByUser";
            this.radioButtonKeySpecifiedByUser.Size = new System.Drawing.Size(134, 19);
            this.radioButtonKeySpecifiedByUser.TabIndex = 1;
            this.radioButtonKeySpecifiedByUser.Text = "Specified by the user";
            this.radioButtonKeySpecifiedByUser.UseVisualStyleBackColor = true;
            this.radioButtonKeySpecifiedByUser.CheckedChanged += new System.EventHandler(this.radioButtonKeySpecifiedByUser_CheckedChanged);
            // 
            // radioButtonKeyRandomGeneration
            // 
            this.radioButtonKeyRandomGeneration.AutoSize = true;
            this.radioButtonKeyRandomGeneration.Checked = true;
            this.radioButtonKeyRandomGeneration.Location = new System.Drawing.Point(40, 54);
            this.radioButtonKeyRandomGeneration.Name = "radioButtonKeyRandomGeneration";
            this.radioButtonKeyRandomGeneration.Size = new System.Drawing.Size(130, 19);
            this.radioButtonKeyRandomGeneration.TabIndex = 0;
            this.radioButtonKeyRandomGeneration.TabStop = true;
            this.radioButtonKeyRandomGeneration.Text = "Random generation";
            this.radioButtonKeyRandomGeneration.UseVisualStyleBackColor = true;
            // 
            // groupBoxCrypto
            // 
            this.groupBoxCrypto.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxCrypto.Controls.Add(this.checkBoxEncodingSL);
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
            this.groupBoxCrypto.Location = new System.Drawing.Point(17, 213);
            this.groupBoxCrypto.Name = "groupBoxCrypto";
            this.groupBoxCrypto.Size = new System.Drawing.Size(640, 418);
            this.groupBoxCrypto.TabIndex = 74;
            this.groupBoxCrypto.TabStop = false;
            this.groupBoxCrypto.Text = "Cryptography";
            // 
            // checkBoxEncodingSL
            // 
            this.checkBoxEncodingSL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxEncodingSL.AutoSize = true;
            this.checkBoxEncodingSL.Enabled = false;
            this.checkBoxEncodingSL.Location = new System.Drawing.Point(509, 209);
            this.checkBoxEncodingSL.Name = "checkBoxEncodingSL";
            this.checkBoxEncodingSL.Size = new System.Drawing.Size(109, 19);
            this.checkBoxEncodingSL.TabIndex = 76;
            this.checkBoxEncodingSL.Text = "Encoding for SL";
            this.toolTip1.SetToolTip(this.checkBoxEncodingSL, "& will encoded as %26 for compatibility with Silverlight (breaks compatibility wi" +
        "th DASH)");
            this.checkBoxEncodingSL.UseVisualStyleBackColor = true;
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
            this.panelContentKey.Location = new System.Drawing.Point(7, 300);
            this.panelContentKey.Name = "panelContentKey";
            this.panelContentKey.Size = new System.Drawing.Size(628, 93);
            this.panelContentKey.TabIndex = 75;
            // 
            // textBoxContentKeyCalculated
            // 
            this.textBoxContentKeyCalculated.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxContentKeyCalculated.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.textBoxContentKeyCalculated.Location = new System.Drawing.Point(7, 55);
            this.textBoxContentKeyCalculated.Name = "textBoxContentKeyCalculated";
            this.textBoxContentKeyCalculated.ReadOnly = true;
            this.textBoxContentKeyCalculated.Size = new System.Drawing.Size(438, 23);
            this.textBoxContentKeyCalculated.TabIndex = 73;
            // 
            // panelKeyId
            // 
            this.panelKeyId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelKeyId.Controls.Add(this.label10);
            this.panelKeyId.Controls.Add(this.textBoxkeyid);
            this.panelKeyId.Controls.Add(this.buttonGenKeyID);
            this.panelKeyId.Controls.Add(this.label5);
            this.panelKeyId.Controls.Add(this.panel4);
            this.panelKeyId.Location = new System.Drawing.Point(7, 107);
            this.panelKeyId.Name = "panelKeyId";
            this.panelKeyId.Size = new System.Drawing.Size(628, 77);
            this.panelKeyId.TabIndex = 74;
            // 
            // label10
            // 
            this.label10.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label10.Location = new System.Drawing.Point(52, 15);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(267, 15);
            this.label10.TabIndex = 69;
            this.label10.Text = "If empty, it will be automatically generated";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(27, 646);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(105, 15);
            this.label7.TabIndex = 82;
            this.label7.Text = "Custom attributes:";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label6.Location = new System.Drawing.Point(483, 668);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(156, 15);
            this.label6.TabIndex = 81;
            this.label6.Text = "name1:value1,name2:value2";
            // 
            // textBoxCustomAttributes
            // 
            this.textBoxCustomAttributes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxCustomAttributes.Location = new System.Drawing.Point(24, 665);
            this.textBoxCustomAttributes.Name = "textBoxCustomAttributes";
            this.textBoxCustomAttributes.Size = new System.Drawing.Size(451, 23);
            this.textBoxCustomAttributes.TabIndex = 80;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.label9.Location = new System.Drawing.Point(26, 21);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(264, 42);
            this.label9.TabIndex = 83;
            this.label9.Text = "Step 2\r\nSpecify the PlayReady Content Key";
            // 
            // AddDynamicEncryptionFrame2_PlayReadyKeyConfig
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(681, 763);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.groupBoxCrypto);
            this.Controls.Add(this.textBoxCustomAttributes);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "AddDynamicEncryptionFrame2_PlayReadyKeyConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Dynamic Encryption - Step 2";
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
            this.panelContentKey.ResumeLayout(false);
            this.panelContentKey.PerformLayout();
            this.panelKeyId.ResumeLayout(false);
            this.panelKeyId.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        public System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.TextBox textBoxCustomAttributes;
        private System.Windows.Forms.CheckBox checkBoxEncodingSL;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
    }
}