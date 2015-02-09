namespace AMSExplorer
{
    partial class AddDynamicEncryptionFrame1
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
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.radioButtonEnvelope = new System.Windows.Forms.RadioButton();
            this.radioButtonCENCKey = new System.Windows.Forms.RadioButton();
            this.groupBoxKeyType = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.radioButtonDecryptStorage = new System.Windows.Forms.RadioButton();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxProtocolSmooth = new System.Windows.Forms.CheckBox();
            this.checkBoxProtocolDASH = new System.Windows.Forms.CheckBox();
            this.checkBoxProtocolHLS = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBoxAuthPol = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownNbOptions = new System.Windows.Forms.NumericUpDown();
            this.radioButtonDefineAuthPol = new System.Windows.Forms.RadioButton();
            this.radioButtonNoAuthPolicy = new System.Windows.Forms.RadioButton();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBoxKeyType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBoxAuthPol.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNbOptions)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(331, 11);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(151, 23);
            this.buttonOk.TabIndex = 17;
            this.buttonOk.Text = "Next";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(488, 11);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(92, 23);
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
            this.radioButtonCENCKey.CheckedChanged += new System.EventHandler(this.radioButtonCENCKey_CheckedChanged);
            // 
            // groupBoxKeyType
            // 
            this.groupBoxKeyType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxKeyType.Controls.Add(this.pictureBox1);
            this.groupBoxKeyType.Controls.Add(this.radioButtonDecryptStorage);
            this.groupBoxKeyType.Controls.Add(this.pictureBox3);
            this.groupBoxKeyType.Controls.Add(this.pictureBox2);
            this.groupBoxKeyType.Controls.Add(this.radioButtonCENCKey);
            this.groupBoxKeyType.Controls.Add(this.radioButtonEnvelope);
            this.groupBoxKeyType.Location = new System.Drawing.Point(12, 81);
            this.groupBoxKeyType.Name = "groupBoxKeyType";
            this.groupBoxKeyType.Size = new System.Drawing.Size(553, 102);
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
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.checkBoxProtocolSmooth);
            this.groupBox1.Controls.Add(this.checkBoxProtocolDASH);
            this.groupBox1.Controls.Add(this.checkBoxProtocolHLS);
            this.groupBox1.Location = new System.Drawing.Point(12, 322);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(553, 108);
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(22, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(349, 40);
            this.label1.TabIndex = 49;
            this.label1.Text = "Step 1 \r\nSelect encryption mode and streaming protocols";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonOk);
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Location = new System.Drawing.Point(-1, 615);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(586, 48);
            this.panel1.TabIndex = 51;
            // 
            // groupBoxAuthPol
            // 
            this.groupBoxAuthPol.Controls.Add(this.label8);
            this.groupBoxAuthPol.Controls.Add(this.label2);
            this.groupBoxAuthPol.Controls.Add(this.numericUpDownNbOptions);
            this.groupBoxAuthPol.Controls.Add(this.radioButtonDefineAuthPol);
            this.groupBoxAuthPol.Controls.Add(this.radioButtonNoAuthPolicy);
            this.groupBoxAuthPol.Location = new System.Drawing.Point(12, 202);
            this.groupBoxAuthPol.Name = "groupBoxAuthPol";
            this.groupBoxAuthPol.Size = new System.Drawing.Size(553, 114);
            this.groupBoxAuthPol.TabIndex = 52;
            this.groupBoxAuthPol.TabStop = false;
            this.groupBoxAuthPol.Text = "Key Authorization policy";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(370, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 65;
            this.label2.Text = "option(s)";
            // 
            // numericUpDownNbOptions
            // 
            this.numericUpDownNbOptions.Location = new System.Drawing.Point(318, 27);
            this.numericUpDownNbOptions.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownNbOptions.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownNbOptions.Name = "numericUpDownNbOptions";
            this.numericUpDownNbOptions.Size = new System.Drawing.Size(46, 20);
            this.numericUpDownNbOptions.TabIndex = 64;
            this.numericUpDownNbOptions.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // radioButtonDefineAuthPol
            // 
            this.radioButtonDefineAuthPol.AutoSize = true;
            this.radioButtonDefineAuthPol.Checked = true;
            this.radioButtonDefineAuthPol.Location = new System.Drawing.Point(35, 27);
            this.radioButtonDefineAuthPol.Name = "radioButtonDefineAuthPol";
            this.radioButtonDefineAuthPol.Size = new System.Drawing.Size(278, 17);
            this.radioButtonDefineAuthPol.TabIndex = 63;
            this.radioButtonDefineAuthPol.TabStop = true;
            this.radioButtonDefineAuthPol.Text = "Define an authorization policy for the content key with";
            this.radioButtonDefineAuthPol.UseVisualStyleBackColor = true;
            // 
            // radioButtonNoAuthPolicy
            // 
            this.radioButtonNoAuthPolicy.AutoSize = true;
            this.radioButtonNoAuthPolicy.Enabled = false;
            this.radioButtonNoAuthPolicy.Location = new System.Drawing.Point(35, 84);
            this.radioButtonNoAuthPolicy.Name = "radioButtonNoAuthPolicy";
            this.radioButtonNoAuthPolicy.Size = new System.Drawing.Size(340, 17);
            this.radioButtonNoAuthPolicy.TabIndex = 62;
            this.radioButtonNoAuthPolicy.Text = "None - An external PlayReady server is used to deliver the licenses\r\n";
            this.radioButtonNoAuthPolicy.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label8.Location = new System.Drawing.Point(55, 50);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(492, 36);
            this.label8.TabIndex = 69;
            this.label8.Text = "Having more than one option is useful if you want to support several types of tok" +
    "ens, or want to deliver various PlayReady licenses based on token claims";
            // 
            // AddDynamicEncryptionFrame1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(584, 661);
            this.Controls.Add(this.groupBoxAuthPol);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBoxKeyType);
            this.Name = "AddDynamicEncryptionFrame1";
            this.Text = "Dynamic Encryption - Step 1";
            this.Load += new System.EventHandler(this.SetupDynEnc_Load);
            this.groupBoxKeyType.ResumeLayout(false);
            this.groupBoxKeyType.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.groupBoxAuthPol.ResumeLayout(false);
            this.groupBoxAuthPol.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNbOptions)).EndInit();
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
        private System.Windows.Forms.CheckBox checkBoxProtocolSmooth;
        private System.Windows.Forms.CheckBox checkBoxProtocolDASH;
        private System.Windows.Forms.CheckBox checkBoxProtocolHLS;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.RadioButton radioButtonDecryptStorage;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBoxAuthPol;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDownNbOptions;
        private System.Windows.Forms.RadioButton radioButtonDefineAuthPol;
        private System.Windows.Forms.RadioButton radioButtonNoAuthPolicy;
        private System.Windows.Forms.Label label8;
    }
}