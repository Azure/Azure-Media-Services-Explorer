namespace AMSExplorer
{
    partial class AddDynamicEncryption
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddDynamicEncryption));
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.radioButtonEnvelope = new System.Windows.Forms.RadioButton();
            this.radioButtonCENCKey = new System.Windows.Forms.RadioButton();
            this.groupBoxKeyType = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.radioButtonDecryptStorage = new System.Windows.Forms.RadioButton();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.checkBoxForceCreateContentKey = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxProtocolSmooth = new System.Windows.Forms.CheckBox();
            this.checkBoxProtocolDASH = new System.Windows.Forms.CheckBox();
            this.checkBoxForceCreateDelPol = new System.Windows.Forms.CheckBox();
            this.checkBoxProtocolHLS = new System.Windows.Forms.CheckBox();
            this.groupBoxAuthPol = new System.Windows.Forms.GroupBox();
            this.panelAutPol = new System.Windows.Forms.Panel();
            this.textBoxIssuer = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxAudience = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.radioButtonToken = new System.Windows.Forms.RadioButton();
            this.radioButtonOpen = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxKeyType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBoxAuthPol.SuspendLayout();
            this.panelAutPol.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(133, 485);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(161, 32);
            this.buttonOk.TabIndex = 17;
            this.buttonOk.Text = "Setup dynamic encryption";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(315, 485);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(161, 32);
            this.buttonCancel.TabIndex = 16;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // radioButtonEnvelope
            // 
            this.radioButtonEnvelope.AutoSize = true;
            this.radioButtonEnvelope.Checked = true;
            this.radioButtonEnvelope.Location = new System.Drawing.Point(35, 51);
            this.radioButtonEnvelope.Name = "radioButtonEnvelope";
            this.radioButtonEnvelope.Size = new System.Drawing.Size(152, 17);
            this.radioButtonEnvelope.TabIndex = 44;
            this.radioButtonEnvelope.TabStop = true;
            this.radioButtonEnvelope.Text = "Envelope encryption (AES)";
            this.radioButtonEnvelope.UseVisualStyleBackColor = true;
            this.radioButtonEnvelope.CheckedChanged += new System.EventHandler(this.radioButtonEnvelope_CheckedChanged);
            // 
            // radioButtonCENCKey
            // 
            this.radioButtonCENCKey.AutoSize = true;
            this.radioButtonCENCKey.Location = new System.Drawing.Point(35, 74);
            this.radioButtonCENCKey.Name = "radioButtonCENCKey";
            this.radioButtonCENCKey.Size = new System.Drawing.Size(187, 17);
            this.radioButtonCENCKey.TabIndex = 46;
            this.radioButtonCENCKey.Text = "Common encryption (PlayReady...)";
            this.radioButtonCENCKey.UseVisualStyleBackColor = true;
            // 
            // groupBoxKeyType
            // 
            this.groupBoxKeyType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxKeyType.Controls.Add(this.pictureBox1);
            this.groupBoxKeyType.Controls.Add(this.radioButtonDecryptStorage);
            this.groupBoxKeyType.Controls.Add(this.pictureBox3);
            this.groupBoxKeyType.Controls.Add(this.pictureBox2);
            this.groupBoxKeyType.Controls.Add(this.checkBoxForceCreateContentKey);
            this.groupBoxKeyType.Controls.Add(this.radioButtonCENCKey);
            this.groupBoxKeyType.Controls.Add(this.radioButtonEnvelope);
            this.groupBoxKeyType.Location = new System.Drawing.Point(12, 56);
            this.groupBoxKeyType.Name = "groupBoxKeyType";
            this.groupBoxKeyType.Size = new System.Drawing.Size(560, 102);
            this.groupBoxKeyType.TabIndex = 43;
            this.groupBoxKeyType.TabStop = false;
            this.groupBoxKeyType.Text = "Encryption mode";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::AMSExplorer.Bitmaps.storage_decryption;
            this.pictureBox1.Location = new System.Drawing.Point(13, 28);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 50;
            this.pictureBox1.TabStop = false;
            // 
            // radioButtonDecryptStorage
            // 
            this.radioButtonDecryptStorage.AutoSize = true;
            this.radioButtonDecryptStorage.Location = new System.Drawing.Point(35, 28);
            this.radioButtonDecryptStorage.Name = "radioButtonDecryptStorage";
            this.radioButtonDecryptStorage.Size = new System.Drawing.Size(295, 17);
            this.radioButtonDecryptStorage.TabIndex = 61;
            this.radioButtonDecryptStorage.Text = "No encryption (stream storage encrypted asset(s) in clear)";
            this.radioButtonDecryptStorage.UseVisualStyleBackColor = true;
            this.radioButtonDecryptStorage.CheckedChanged += new System.EventHandler(this.radioButtonDecryptStorage_CheckedChanged);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::AMSExplorer.Bitmaps.DRM_protection;
            this.pictureBox3.Location = new System.Drawing.Point(13, 74);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(16, 16);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox3.TabIndex = 60;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::AMSExplorer.Bitmaps.envelope_encryption;
            this.pictureBox2.Location = new System.Drawing.Point(13, 52);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(16, 16);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 51;
            this.pictureBox2.TabStop = false;
            // 
            // checkBoxForceCreateContentKey
            // 
            this.checkBoxForceCreateContentKey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxForceCreateContentKey.AutoSize = true;
            this.checkBoxForceCreateContentKey.Location = new System.Drawing.Point(341, 74);
            this.checkBoxForceCreateContentKey.Name = "checkBoxForceCreateContentKey";
            this.checkBoxForceCreateContentKey.Size = new System.Drawing.Size(201, 17);
            this.checkBoxForceCreateContentKey.TabIndex = 59;
            this.checkBoxForceCreateContentKey.Text = "Do not try to use existing content key";
            this.checkBoxForceCreateContentKey.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.checkBoxProtocolSmooth);
            this.groupBox1.Controls.Add(this.checkBoxProtocolDASH);
            this.groupBox1.Controls.Add(this.checkBoxForceCreateDelPol);
            this.groupBox1.Controls.Add(this.checkBoxProtocolHLS);
            this.groupBox1.Location = new System.Drawing.Point(12, 349);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(560, 108);
            this.groupBox1.TabIndex = 47;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Delivery policy protocols";
            // 
            // checkBoxProtocolSmooth
            // 
            this.checkBoxProtocolSmooth.AutoSize = true;
            this.checkBoxProtocolSmooth.Checked = true;
            this.checkBoxProtocolSmooth.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxProtocolSmooth.Location = new System.Drawing.Point(35, 77);
            this.checkBoxProtocolSmooth.Name = "checkBoxProtocolSmooth";
            this.checkBoxProtocolSmooth.Size = new System.Drawing.Size(112, 17);
            this.checkBoxProtocolSmooth.TabIndex = 57;
            this.checkBoxProtocolSmooth.Text = "Smooth Streaming";
            this.checkBoxProtocolSmooth.UseVisualStyleBackColor = true;
            // 
            // checkBoxProtocolDASH
            // 
            this.checkBoxProtocolDASH.AutoSize = true;
            this.checkBoxProtocolDASH.Checked = true;
            this.checkBoxProtocolDASH.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxProtocolDASH.Location = new System.Drawing.Point(35, 54);
            this.checkBoxProtocolDASH.Name = "checkBoxProtocolDASH";
            this.checkBoxProtocolDASH.Size = new System.Drawing.Size(56, 17);
            this.checkBoxProtocolDASH.TabIndex = 56;
            this.checkBoxProtocolDASH.Text = "DASH";
            this.checkBoxProtocolDASH.UseVisualStyleBackColor = true;
            // 
            // checkBoxForceCreateDelPol
            // 
            this.checkBoxForceCreateDelPol.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxForceCreateDelPol.AutoSize = true;
            this.checkBoxForceCreateDelPol.Location = new System.Drawing.Point(331, 77);
            this.checkBoxForceCreateDelPol.Name = "checkBoxForceCreateDelPol";
            this.checkBoxForceCreateDelPol.Size = new System.Drawing.Size(211, 17);
            this.checkBoxForceCreateDelPol.TabIndex = 50;
            this.checkBoxForceCreateDelPol.Text = "Do not try to use existing delivery policy";
            this.checkBoxForceCreateDelPol.UseVisualStyleBackColor = true;
            this.checkBoxForceCreateDelPol.CheckedChanged += new System.EventHandler(this.checkBoxForceCreateDelPol_CheckedChanged);
            // 
            // checkBoxProtocolHLS
            // 
            this.checkBoxProtocolHLS.AutoSize = true;
            this.checkBoxProtocolHLS.Checked = true;
            this.checkBoxProtocolHLS.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxProtocolHLS.Location = new System.Drawing.Point(35, 31);
            this.checkBoxProtocolHLS.Name = "checkBoxProtocolHLS";
            this.checkBoxProtocolHLS.Size = new System.Drawing.Size(47, 17);
            this.checkBoxProtocolHLS.TabIndex = 55;
            this.checkBoxProtocolHLS.Text = "HLS";
            this.checkBoxProtocolHLS.UseVisualStyleBackColor = true;
            // 
            // groupBoxAuthPol
            // 
            this.groupBoxAuthPol.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxAuthPol.Controls.Add(this.panelAutPol);
            this.groupBoxAuthPol.Controls.Add(this.radioButtonToken);
            this.groupBoxAuthPol.Controls.Add(this.radioButtonOpen);
            this.groupBoxAuthPol.Location = new System.Drawing.Point(12, 180);
            this.groupBoxAuthPol.Name = "groupBoxAuthPol";
            this.groupBoxAuthPol.Size = new System.Drawing.Size(560, 146);
            this.groupBoxAuthPol.TabIndex = 47;
            this.groupBoxAuthPol.TabStop = false;
            this.groupBoxAuthPol.Text = "Content key\'s authorization policy";
            // 
            // panelAutPol
            // 
            this.panelAutPol.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelAutPol.Controls.Add(this.textBoxIssuer);
            this.panelAutPol.Controls.Add(this.label3);
            this.panelAutPol.Controls.Add(this.textBoxAudience);
            this.panelAutPol.Controls.Add(this.label2);
            this.panelAutPol.Enabled = false;
            this.panelAutPol.Location = new System.Drawing.Point(35, 66);
            this.panelAutPol.Name = "panelAutPol";
            this.panelAutPol.Size = new System.Drawing.Size(515, 74);
            this.panelAutPol.TabIndex = 60;
            // 
            // textBoxIssuer
            // 
            this.textBoxIssuer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxIssuer.Location = new System.Drawing.Point(203, 8);
            this.textBoxIssuer.Name = "textBoxIssuer";
            this.textBoxIssuer.Size = new System.Drawing.Size(304, 20);
            this.textBoxIssuer.TabIndex = 56;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(178, 13);
            this.label3.TabIndex = 59;
            this.label3.Text = "Audience or scope of the token Url :";
            // 
            // textBoxAudience
            // 
            this.textBoxAudience.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxAudience.Location = new System.Drawing.Point(203, 38);
            this.textBoxAudience.Name = "textBoxAudience";
            this.textBoxAudience.Size = new System.Drawing.Size(304, 20);
            this.textBoxAudience.TabIndex = 57;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 13);
            this.label2.TabIndex = 58;
            this.label2.Text = "Issuer of the token Url :";
            // 
            // radioButtonToken
            // 
            this.radioButtonToken.AutoSize = true;
            this.radioButtonToken.Location = new System.Drawing.Point(35, 50);
            this.radioButtonToken.Name = "radioButtonToken";
            this.radioButtonToken.Size = new System.Drawing.Size(56, 17);
            this.radioButtonToken.TabIndex = 55;
            this.radioButtonToken.Text = "Token";
            this.radioButtonToken.UseVisualStyleBackColor = true;
            this.radioButtonToken.CheckedChanged += new System.EventHandler(this.radioButtonToken_CheckedChanged);
            // 
            // radioButtonOpen
            // 
            this.radioButtonOpen.AutoSize = true;
            this.radioButtonOpen.Checked = true;
            this.radioButtonOpen.Location = new System.Drawing.Point(35, 27);
            this.radioButtonOpen.Name = "radioButtonOpen";
            this.radioButtonOpen.Size = new System.Drawing.Size(51, 17);
            this.radioButtonOpen.TabIndex = 54;
            this.radioButtonOpen.TabStop = true;
            this.radioButtonOpen.Text = "Open";
            this.radioButtonOpen.UseVisualStyleBackColor = true;
            this.radioButtonOpen.CheckedChanged += new System.EventHandler(this.radioButtonOpen_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(421, 13);
            this.label1.TabIndex = 49;
            this.label1.Text = "This will add a dynamic encryption policy and configure the key/license delivery " +
    "service.";
            // 
            // AddDynamicEncryption
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(591, 529);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBoxAuthPol);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBoxKeyType);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.buttonCancel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddDynamicEncryption";
            this.Text = "Dynamic encryption and key/license delivery";
            this.Load += new System.EventHandler(this.SetupDynEnc_Load);
            this.groupBoxKeyType.ResumeLayout(false);
            this.groupBoxKeyType.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBoxAuthPol.ResumeLayout(false);
            this.groupBoxAuthPol.PerformLayout();
            this.panelAutPol.ResumeLayout(false);
            this.panelAutPol.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button buttonOk;
        public System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.RadioButton radioButtonEnvelope;
        private System.Windows.Forms.RadioButton radioButtonCENCKey;
        private System.Windows.Forms.GroupBox groupBoxKeyType;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBoxAuthPol;
        private System.Windows.Forms.CheckBox checkBoxProtocolSmooth;
        private System.Windows.Forms.CheckBox checkBoxProtocolDASH;
        private System.Windows.Forms.CheckBox checkBoxProtocolHLS;
        private System.Windows.Forms.RadioButton radioButtonToken;
        private System.Windows.Forms.RadioButton radioButtonOpen;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxAudience;
        private System.Windows.Forms.TextBox textBoxIssuer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelAutPol;
        private System.Windows.Forms.CheckBox checkBoxForceCreateDelPol;
        private System.Windows.Forms.CheckBox checkBoxForceCreateContentKey;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.RadioButton radioButtonDecryptStorage;
    }
}