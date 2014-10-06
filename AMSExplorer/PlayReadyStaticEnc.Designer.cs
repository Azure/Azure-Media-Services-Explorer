namespace AMSExplorer
{
    partial class PlayReadyStaticEnc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlayReadyStaticEnc));
            this.label3 = new System.Windows.Forms.Label();
            this.textboxoutputassetname = new System.Windows.Forms.TextBox();
            this.processorlabel = new System.Windows.Forms.Label();
            this.moreinfotestserver = new System.Windows.Forms.LinkLabel();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label = new System.Windows.Forms.Label();
            this.buttonPlayReadyTestSettings = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
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
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.textBoxCustomAttributes = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxServiceID = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.checkBoxadjustSubSamples = new System.Windows.Forms.CheckBox();
            this.checkBoxuseSencBox = new System.Windows.Forms.CheckBox();
            this.checkBoxDeliverLicenses = new System.Windows.Forms.CheckBox();
            this.labelAssetName = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.buttonOk = new System.Windows.Forms.Button();
            this.comboBoxKeyRestriction = new System.Windows.Forms.ComboBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 407);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(174, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "Please enter an output asset name:";
            // 
            // textboxoutputassetname
            // 
            this.textboxoutputassetname.Location = new System.Drawing.Point(27, 426);
            this.textboxoutputassetname.Name = "textboxoutputassetname";
            this.textboxoutputassetname.Size = new System.Drawing.Size(460, 20);
            this.textboxoutputassetname.TabIndex = 21;
            // 
            // processorlabel
            // 
            this.processorlabel.Location = new System.Drawing.Point(24, 458);
            this.processorlabel.Name = "processorlabel";
            this.processorlabel.Size = new System.Drawing.Size(463, 22);
            this.processorlabel.TabIndex = 20;
            this.processorlabel.Text = "processor name";
            // 
            // moreinfotestserver
            // 
            this.moreinfotestserver.AutoSize = true;
            this.moreinfotestserver.Location = new System.Drawing.Point(190, 65);
            this.moreinfotestserver.Name = "moreinfotestserver";
            this.moreinfotestserver.Size = new System.Drawing.Size(152, 13);
            this.moreinfotestserver.TabIndex = 19;
            this.moreinfotestserver.TabStop = true;
            this.moreinfotestserver.Text = "PlayReady test server web site";
            this.moreinfotestserver.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.moreinfotestserver_LinkClicked);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(267, 488);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(208, 32);
            this.buttonCancel.TabIndex = 16;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(188, 47);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(264, 13);
            this.label.TabIndex = 28;
            this.label.Text = "You can specifiy your own data or use the test settings";
            // 
            // buttonPlayReadyTestSettings
            // 
            this.buttonPlayReadyTestSettings.Location = new System.Drawing.Point(23, 48);
            this.buttonPlayReadyTestSettings.Name = "buttonPlayReadyTestSettings";
            this.buttonPlayReadyTestSettings.Size = new System.Drawing.Size(159, 32);
            this.buttonPlayReadyTestSettings.TabIndex = 29;
            this.buttonPlayReadyTestSettings.Text = "Use PlayReady Test Settings";
            this.buttonPlayReadyTestSettings.UseVisualStyleBackColor = true;
            this.buttonPlayReadyTestSettings.Click += new System.EventHandler(this.buttonPlayReadyTestSettings_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(23, 95);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(468, 262);
            this.tabControl1.TabIndex = 33;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.textBoxLAurl);
            this.tabPage1.Controls.Add(this.buttongenerateContentKey);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.textBoxkeyseed);
            this.tabPage1.Controls.Add(this.buttonGenKeyID);
            this.tabPage1.Controls.Add(this.textBoxkeyid);
            this.tabPage1.Controls.Add(this.textBoxcontentkey);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(460, 236);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Basic settings";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // textBoxLAurl
            // 
            this.textBoxLAurl.Location = new System.Drawing.Point(24, 82);
            this.textBoxLAurl.Name = "textBoxLAurl";
            this.textBoxLAurl.Size = new System.Drawing.Size(414, 20);
            this.textBoxLAurl.TabIndex = 37;
            // 
            // buttongenerateContentKey
            // 
            this.buttongenerateContentKey.Location = new System.Drawing.Point(355, 187);
            this.buttongenerateContentKey.Name = "buttongenerateContentKey";
            this.buttongenerateContentKey.Size = new System.Drawing.Size(83, 25);
            this.buttongenerateContentKey.TabIndex = 40;
            this.buttongenerateContentKey.Text = "Generate";
            this.buttongenerateContentKey.UseVisualStyleBackColor = true;
            this.buttongenerateContentKey.Click += new System.EventHandler(this.buttongenerateContentKey_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 120);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 36;
            this.label1.Text = "Key Seed:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 13);
            this.label2.TabIndex = 35;
            this.label2.Text = "License Acquisition Url:";
            // 
            // textBoxkeyseed
            // 
            this.textBoxkeyseed.Location = new System.Drawing.Point(24, 136);
            this.textBoxkeyseed.Name = "textBoxkeyseed";
            this.textBoxkeyseed.Size = new System.Drawing.Size(414, 20);
            this.textBoxkeyseed.TabIndex = 34;
            this.textBoxkeyseed.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // buttonGenKeyID
            // 
            this.buttonGenKeyID.Location = new System.Drawing.Point(355, 25);
            this.buttonGenKeyID.Name = "buttonGenKeyID";
            this.buttonGenKeyID.Size = new System.Drawing.Size(83, 25);
            this.buttonGenKeyID.TabIndex = 33;
            this.buttonGenKeyID.Text = "Generate";
            this.buttonGenKeyID.UseVisualStyleBackColor = true;
            this.buttonGenKeyID.Click += new System.EventHandler(this.buttonGenKeyID_Click_1);
            // 
            // textBoxkeyid
            // 
            this.textBoxkeyid.Location = new System.Drawing.Point(24, 28);
            this.textBoxkeyid.Name = "textBoxkeyid";
            this.textBoxkeyid.Size = new System.Drawing.Size(325, 20);
            this.textBoxkeyid.TabIndex = 32;
            this.textBoxkeyid.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // textBoxcontentkey
            // 
            this.textBoxcontentkey.Location = new System.Drawing.Point(24, 190);
            this.textBoxcontentkey.Name = "textBoxcontentkey";
            this.textBoxcontentkey.Size = new System.Drawing.Size(325, 20);
            this.textBoxcontentkey.TabIndex = 35;
            this.textBoxcontentkey.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(21, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 13);
            this.label5.TabIndex = 31;
            this.label5.Text = "Key ID:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 174);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 34;
            this.label4.Text = "Content key:";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.textBoxCustomAttributes);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.textBoxServiceID);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.checkBoxadjustSubSamples);
            this.tabPage2.Controls.Add(this.checkBoxuseSencBox);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(460, 236);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Advanced settings";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // textBoxCustomAttributes
            // 
            this.textBoxCustomAttributes.Location = new System.Drawing.Point(22, 83);
            this.textBoxCustomAttributes.Name = "textBoxCustomAttributes";
            this.textBoxCustomAttributes.Size = new System.Drawing.Size(414, 20);
            this.textBoxCustomAttributes.TabIndex = 39;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(19, 67);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(92, 13);
            this.label7.TabIndex = 38;
            this.label7.Text = "Custom Attributes:";
            // 
            // textBoxServiceID
            // 
            this.textBoxServiceID.Location = new System.Drawing.Point(22, 31);
            this.textBoxServiceID.Name = "textBoxServiceID";
            this.textBoxServiceID.Size = new System.Drawing.Size(414, 20);
            this.textBoxServiceID.TabIndex = 37;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(19, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 13);
            this.label6.TabIndex = 36;
            this.label6.Text = "Service ID:";
            // 
            // checkBoxadjustSubSamples
            // 
            this.checkBoxadjustSubSamples.AutoSize = true;
            this.checkBoxadjustSubSamples.Checked = true;
            this.checkBoxadjustSubSamples.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxadjustSubSamples.Location = new System.Drawing.Point(22, 148);
            this.checkBoxadjustSubSamples.Name = "checkBoxadjustSubSamples";
            this.checkBoxadjustSubSamples.Size = new System.Drawing.Size(113, 17);
            this.checkBoxadjustSubSamples.TabIndex = 33;
            this.checkBoxadjustSubSamples.Text = "adjustSubSamples";
            this.checkBoxadjustSubSamples.UseVisualStyleBackColor = true;
            // 
            // checkBoxuseSencBox
            // 
            this.checkBoxuseSencBox.AutoSize = true;
            this.checkBoxuseSencBox.Checked = true;
            this.checkBoxuseSencBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxuseSencBox.Location = new System.Drawing.Point(22, 125);
            this.checkBoxuseSencBox.Name = "checkBoxuseSencBox";
            this.checkBoxuseSencBox.Size = new System.Drawing.Size(86, 17);
            this.checkBoxuseSencBox.TabIndex = 32;
            this.checkBoxuseSencBox.Text = "useSencBox";
            this.checkBoxuseSencBox.UseVisualStyleBackColor = true;
            // 
            // checkBoxDeliverLicenses
            // 
            this.checkBoxDeliverLicenses.AutoSize = true;
            this.checkBoxDeliverLicenses.Location = new System.Drawing.Point(27, 375);
            this.checkBoxDeliverLicenses.Name = "checkBoxDeliverLicenses";
            this.checkBoxDeliverLicenses.Size = new System.Drawing.Size(373, 17);
            this.checkBoxDeliverLicenses.TabIndex = 38;
            this.checkBoxDeliverLicenses.Text = "Setup Azure Media Services PlayReady License Delivery. Key restriction :";
            this.checkBoxDeliverLicenses.UseVisualStyleBackColor = true;
            this.checkBoxDeliverLicenses.CheckedChanged += new System.EventHandler(this.checkBoxDeliverLicenses_CheckedChanged);
            // 
            // labelAssetName
            // 
            this.labelAssetName.AutoSize = true;
            this.labelAssetName.Location = new System.Drawing.Point(58, 19);
            this.labelAssetName.Name = "labelAssetName";
            this.labelAssetName.Size = new System.Drawing.Size(58, 13);
            this.labelAssetName.TabIndex = 50;
            this.labelAssetName.Text = "assetname";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::AMSExplorer.Bitmaps.DRM_protection;
            this.pictureBox1.Location = new System.Drawing.Point(27, 19);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 49;
            this.pictureBox1.TabStop = false;
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Image = global::AMSExplorer.Bitmaps.DRM_protection;
            this.buttonOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonOk.Location = new System.Drawing.Point(38, 488);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(208, 32);
            this.buttonOk.TabIndex = 17;
            this.buttonOk.Tag = "Launch encryption";
            this.buttonOk.Text = "Launch encryption";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // comboBoxKeyRestriction
            // 
            this.comboBoxKeyRestriction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxKeyRestriction.Enabled = false;
            this.comboBoxKeyRestriction.FormattingEnabled = true;
            this.comboBoxKeyRestriction.Location = new System.Drawing.Point(406, 373);
            this.comboBoxKeyRestriction.Name = "comboBoxKeyRestriction";
            this.comboBoxKeyRestriction.Size = new System.Drawing.Size(81, 21);
            this.comboBoxKeyRestriction.TabIndex = 54;
            // 
            // PlayReadyStaticEnc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(517, 532);
            this.Controls.Add(this.comboBoxKeyRestriction);
            this.Controls.Add(this.checkBoxDeliverLicenses);
            this.Controls.Add(this.labelAssetName);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.buttonPlayReadyTestSettings);
            this.Controls.Add(this.label);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textboxoutputassetname);
            this.Controls.Add(this.processorlabel);
            this.Controls.Add(this.moreinfotestserver);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.buttonCancel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PlayReadyStaticEnc";
            this.Text = "PlayReady Static Encryption";
            this.Load += new System.EventHandler(this.PlayReadyStaticEnc_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox textboxoutputassetname;
        private System.Windows.Forms.Label processorlabel;
        private System.Windows.Forms.LinkLabel moreinfotestserver;
        public System.Windows.Forms.Button buttonOk;
        public System.Windows.Forms.Button buttonCancel;
        public System.Windows.Forms.Label label;
        private System.Windows.Forms.Button buttonPlayReadyTestSettings;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        public System.Windows.Forms.TextBox textBoxLAurl;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox textBoxkeyseed;
        private System.Windows.Forms.Button buttonGenKeyID;
        public System.Windows.Forms.TextBox textBoxkeyid;
        public System.Windows.Forms.Label label5;
        private System.Windows.Forms.TabPage tabPage2;
        public System.Windows.Forms.TextBox textBoxCustomAttributes;
        public System.Windows.Forms.Label label7;
        public System.Windows.Forms.TextBox textBoxServiceID;
        public System.Windows.Forms.Label label6;
        public System.Windows.Forms.TextBox textBoxcontentkey;
        public System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBoxadjustSubSamples;
        private System.Windows.Forms.CheckBox checkBoxuseSencBox;
        private System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.Label labelAssetName;
        private System.Windows.Forms.CheckBox checkBoxDeliverLicenses;
        private System.Windows.Forms.Button buttongenerateContentKey;
        private System.Windows.Forms.ComboBox comboBoxKeyRestriction;
    }
}