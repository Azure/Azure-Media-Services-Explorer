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
            this.radioButtonNoDynEnc = new System.Windows.Forms.RadioButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.radioButtonDecryptStorage = new System.Windows.Forms.RadioButton();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.groupBoxDelPolProtocols = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxCustomAttributes = new System.Windows.Forms.TextBox();
            this.checkBoxEnableDynEnc = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBoxWidevinePackaging = new System.Windows.Forms.CheckBox();
            this.checkBoxPlayReadyPackaging = new System.Windows.Forms.CheckBox();
            this.checkBoxProtocolSmooth = new System.Windows.Forms.CheckBox();
            this.checkBoxProtocolDASH = new System.Windows.Forms.CheckBox();
            this.checkBoxProtocolHLS = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelDynEnc = new System.Windows.Forms.Panel();
            this.groupBoxKeyType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.groupBoxDelPolProtocols.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panelDynEnc.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(386, 13);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(176, 27);
            this.buttonOk.TabIndex = 17;
            this.buttonOk.Text = "Next";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(569, 13);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(107, 27);
            this.buttonCancel.TabIndex = 16;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // radioButtonEnvelope
            // 
            this.radioButtonEnvelope.AutoSize = true;
            this.radioButtonEnvelope.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonEnvelope.Location = new System.Drawing.Point(52, 32);
            this.radioButtonEnvelope.Name = "radioButtonEnvelope";
            this.radioButtonEnvelope.Size = new System.Drawing.Size(156, 19);
            this.radioButtonEnvelope.TabIndex = 44;
            this.radioButtonEnvelope.Text = "AES Clear Key (Envelope)";
            this.radioButtonEnvelope.UseVisualStyleBackColor = true;
            this.radioButtonEnvelope.CheckedChanged += new System.EventHandler(this.radioButtonEnvelope_CheckedChanged);
            // 
            // radioButtonCENCKey
            // 
            this.radioButtonCENCKey.AutoSize = true;
            this.radioButtonCENCKey.Checked = true;
            this.radioButtonCENCKey.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonCENCKey.Location = new System.Drawing.Point(52, 58);
            this.radioButtonCENCKey.Name = "radioButtonCENCKey";
            this.radioButtonCENCKey.Size = new System.Drawing.Size(265, 19);
            this.radioButtonCENCKey.TabIndex = 46;
            this.radioButtonCENCKey.TabStop = true;
            this.radioButtonCENCKey.Text = "Common encryption (PlayReady, Widevine...)";
            this.radioButtonCENCKey.UseVisualStyleBackColor = true;
            this.radioButtonCENCKey.CheckedChanged += new System.EventHandler(this.radioButtonCENCKey_CheckedChanged);
            // 
            // groupBoxKeyType
            // 
            this.groupBoxKeyType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxKeyType.Controls.Add(this.radioButtonNoDynEnc);
            this.groupBoxKeyType.Controls.Add(this.pictureBox1);
            this.groupBoxKeyType.Controls.Add(this.radioButtonDecryptStorage);
            this.groupBoxKeyType.Controls.Add(this.pictureBox3);
            this.groupBoxKeyType.Controls.Add(this.pictureBox2);
            this.groupBoxKeyType.Controls.Add(this.radioButtonCENCKey);
            this.groupBoxKeyType.Controls.Add(this.radioButtonEnvelope);
            this.groupBoxKeyType.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxKeyType.Location = new System.Drawing.Point(14, 93);
            this.groupBoxKeyType.Name = "groupBoxKeyType";
            this.groupBoxKeyType.Size = new System.Drawing.Size(645, 149);
            this.groupBoxKeyType.TabIndex = 43;
            this.groupBoxKeyType.TabStop = false;
            this.groupBoxKeyType.Text = "Protection";
            // 
            // radioButtonNoDynEnc
            // 
            this.radioButtonNoDynEnc.AutoSize = true;
            this.radioButtonNoDynEnc.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonNoDynEnc.Location = new System.Drawing.Point(52, 111);
            this.radioButtonNoDynEnc.Name = "radioButtonNoDynEnc";
            this.radioButtonNoDynEnc.Size = new System.Drawing.Size(224, 19);
            this.radioButtonNoDynEnc.TabIndex = 62;
            this.radioButtonNoDynEnc.Text = "None - Asset already CENC encrypted";
            this.radioButtonNoDynEnc.UseVisualStyleBackColor = true;
            this.radioButtonNoDynEnc.CheckedChanged += new System.EventHandler(this.radioButtonNoDynEnc_CheckedChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::AMSExplorer.Bitmaps.storage_decryption;
            this.pictureBox1.Location = new System.Drawing.Point(26, 85);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 50;
            this.pictureBox1.TabStop = false;
            // 
            // radioButtonDecryptStorage
            // 
            this.radioButtonDecryptStorage.AutoSize = true;
            this.radioButtonDecryptStorage.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonDecryptStorage.Location = new System.Drawing.Point(52, 85);
            this.radioButtonDecryptStorage.Name = "radioButtonDecryptStorage";
            this.radioButtonDecryptStorage.Size = new System.Drawing.Size(311, 19);
            this.radioButtonDecryptStorage.TabIndex = 61;
            this.radioButtonDecryptStorage.Text = "Decryption (stream storage encrypted asset(s) in clear)";
            this.radioButtonDecryptStorage.UseVisualStyleBackColor = true;
            this.radioButtonDecryptStorage.CheckedChanged += new System.EventHandler(this.radioButtonDecryptStorage_CheckedChanged);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::AMSExplorer.Bitmaps.DRM_protection;
            this.pictureBox3.Location = new System.Drawing.Point(26, 58);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(16, 16);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox3.TabIndex = 60;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::AMSExplorer.Bitmaps.envelope_encryption;
            this.pictureBox2.Location = new System.Drawing.Point(26, 33);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(16, 16);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 51;
            this.pictureBox2.TabStop = false;
            // 
            // groupBoxDelPolProtocols
            // 
            this.groupBoxDelPolProtocols.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxDelPolProtocols.Controls.Add(this.panelDynEnc);
            this.groupBoxDelPolProtocols.Controls.Add(this.checkBoxEnableDynEnc);
            this.groupBoxDelPolProtocols.Location = new System.Drawing.Point(14, 261);
            this.groupBoxDelPolProtocols.Name = "groupBoxDelPolProtocols";
            this.groupBoxDelPolProtocols.Size = new System.Drawing.Size(645, 367);
            this.groupBoxDelPolProtocols.TabIndex = 47;
            this.groupBoxDelPolProtocols.TabStop = false;
            this.groupBoxDelPolProtocols.Text = "Delivery";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(54, 170);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(108, 15);
            this.label7.TabIndex = 85;
            this.label7.Text = "Custom attributes :";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label6.Location = new System.Drawing.Point(458, 214);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(156, 15);
            this.label6.TabIndex = 84;
            this.label6.Text = "name1:value1,name2:value2";
            // 
            // textBoxCustomAttributes
            // 
            this.textBoxCustomAttributes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxCustomAttributes.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxCustomAttributes.Location = new System.Drawing.Point(57, 188);
            this.textBoxCustomAttributes.Name = "textBoxCustomAttributes";
            this.textBoxCustomAttributes.Size = new System.Drawing.Size(557, 23);
            this.textBoxCustomAttributes.TabIndex = 83;
            // 
            // checkBoxEnableDynEnc
            // 
            this.checkBoxEnableDynEnc.AutoSize = true;
            this.checkBoxEnableDynEnc.Checked = true;
            this.checkBoxEnableDynEnc.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxEnableDynEnc.Location = new System.Drawing.Point(6, 22);
            this.checkBoxEnableDynEnc.Name = "checkBoxEnableDynEnc";
            this.checkBoxEnableDynEnc.Size = new System.Drawing.Size(170, 19);
            this.checkBoxEnableDynEnc.TabIndex = 74;
            this.checkBoxEnableDynEnc.Text = "Enable dynamic encryption";
            this.checkBoxEnableDynEnc.UseVisualStyleBackColor = true;
            this.checkBoxEnableDynEnc.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(2, 116);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(628, 18);
            this.label5.TabIndex = 72;
            this.label5.Text = "Packaging";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(627, 18);
            this.label4.TabIndex = 71;
            this.label4.Text = "Delivery protocols";
            // 
            // checkBoxWidevinePackaging
            // 
            this.checkBoxWidevinePackaging.AutoSize = true;
            this.checkBoxWidevinePackaging.Location = new System.Drawing.Point(37, 229);
            this.checkBoxWidevinePackaging.Name = "checkBoxWidevinePackaging";
            this.checkBoxWidevinePackaging.Size = new System.Drawing.Size(220, 19);
            this.checkBoxWidevinePackaging.TabIndex = 59;
            this.checkBoxWidevinePackaging.Text = "Widevine Modular DRM (DASH only)";
            this.checkBoxWidevinePackaging.UseVisualStyleBackColor = true;
            // 
            // checkBoxPlayReadyPackaging
            // 
            this.checkBoxPlayReadyPackaging.AutoSize = true;
            this.checkBoxPlayReadyPackaging.Checked = true;
            this.checkBoxPlayReadyPackaging.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxPlayReadyPackaging.Location = new System.Drawing.Point(37, 143);
            this.checkBoxPlayReadyPackaging.Name = "checkBoxPlayReadyPackaging";
            this.checkBoxPlayReadyPackaging.Size = new System.Drawing.Size(109, 19);
            this.checkBoxPlayReadyPackaging.TabIndex = 58;
            this.checkBoxPlayReadyPackaging.Text = "PlayReady DRM";
            this.checkBoxPlayReadyPackaging.UseVisualStyleBackColor = true;
            this.checkBoxPlayReadyPackaging.CheckedChanged += new System.EventHandler(this.checkBoxPlayReadyPackaging_CheckedChanged);
            // 
            // checkBoxProtocolSmooth
            // 
            this.checkBoxProtocolSmooth.AutoSize = true;
            this.checkBoxProtocolSmooth.Checked = true;
            this.checkBoxProtocolSmooth.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxProtocolSmooth.Location = new System.Drawing.Point(37, 90);
            this.checkBoxProtocolSmooth.Name = "checkBoxProtocolSmooth";
            this.checkBoxProtocolSmooth.Size = new System.Drawing.Size(125, 19);
            this.checkBoxProtocolSmooth.TabIndex = 57;
            this.checkBoxProtocolSmooth.Text = "Smooth Streaming";
            this.checkBoxProtocolSmooth.UseVisualStyleBackColor = true;
            // 
            // checkBoxProtocolDASH
            // 
            this.checkBoxProtocolDASH.AutoSize = true;
            this.checkBoxProtocolDASH.Checked = true;
            this.checkBoxProtocolDASH.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxProtocolDASH.Location = new System.Drawing.Point(37, 65);
            this.checkBoxProtocolDASH.Name = "checkBoxProtocolDASH";
            this.checkBoxProtocolDASH.Size = new System.Drawing.Size(57, 19);
            this.checkBoxProtocolDASH.TabIndex = 56;
            this.checkBoxProtocolDASH.Text = "DASH";
            this.checkBoxProtocolDASH.UseVisualStyleBackColor = true;
            this.checkBoxProtocolDASH.CheckedChanged += new System.EventHandler(this.checkBoxProtocolDASH_CheckedChanged);
            // 
            // checkBoxProtocolHLS
            // 
            this.checkBoxProtocolHLS.AutoSize = true;
            this.checkBoxProtocolHLS.Checked = true;
            this.checkBoxProtocolHLS.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxProtocolHLS.Location = new System.Drawing.Point(37, 40);
            this.checkBoxProtocolHLS.Name = "checkBoxProtocolHLS";
            this.checkBoxProtocolHLS.Size = new System.Drawing.Size(47, 19);
            this.checkBoxProtocolHLS.TabIndex = 55;
            this.checkBoxProtocolHLS.Text = "HLS";
            this.checkBoxProtocolHLS.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkBlue;
            this.label1.Location = new System.Drawing.Point(26, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(364, 42);
            this.label1.TabIndex = 49;
            this.label1.Text = "Step 1 \r\nSelect protection mode and dynamic encryption";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonOk);
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Location = new System.Drawing.Point(-1, 710);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(684, 55);
            this.panel1.TabIndex = 51;
            // 
            // panelDynEnc
            // 
            this.panelDynEnc.Controls.Add(this.label4);
            this.panelDynEnc.Controls.Add(this.label6);
            this.panelDynEnc.Controls.Add(this.label7);
            this.panelDynEnc.Controls.Add(this.checkBoxProtocolHLS);
            this.panelDynEnc.Controls.Add(this.checkBoxProtocolDASH);
            this.panelDynEnc.Controls.Add(this.textBoxCustomAttributes);
            this.panelDynEnc.Controls.Add(this.checkBoxProtocolSmooth);
            this.panelDynEnc.Controls.Add(this.checkBoxPlayReadyPackaging);
            this.panelDynEnc.Controls.Add(this.label5);
            this.panelDynEnc.Controls.Add(this.checkBoxWidevinePackaging);
            this.panelDynEnc.Location = new System.Drawing.Point(6, 47);
            this.panelDynEnc.Name = "panelDynEnc";
            this.panelDynEnc.Size = new System.Drawing.Size(633, 264);
            this.panelDynEnc.TabIndex = 86;
            // 
            // AddDynamicEncryptionFrame1
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(681, 741);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBoxDelPolProtocols);
            this.Controls.Add(this.groupBoxKeyType);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "AddDynamicEncryptionFrame1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Dynamic Encryption - Step 1";
            this.Load += new System.EventHandler(this.SetupDynEnc_Load);
            this.groupBoxKeyType.ResumeLayout(false);
            this.groupBoxKeyType.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.groupBoxDelPolProtocols.ResumeLayout(false);
            this.groupBoxDelPolProtocols.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panelDynEnc.ResumeLayout(false);
            this.panelDynEnc.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button buttonOk;
        public System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.RadioButton radioButtonEnvelope;
        private System.Windows.Forms.RadioButton radioButtonCENCKey;
        private System.Windows.Forms.GroupBox groupBoxKeyType;
        private System.Windows.Forms.GroupBox groupBoxDelPolProtocols;
        private System.Windows.Forms.CheckBox checkBoxProtocolSmooth;
        private System.Windows.Forms.CheckBox checkBoxProtocolDASH;
        private System.Windows.Forms.CheckBox checkBoxProtocolHLS;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.RadioButton radioButtonDecryptStorage;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radioButtonNoDynEnc;
        private System.Windows.Forms.CheckBox checkBoxEnableDynEnc;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBoxWidevinePackaging;
        private System.Windows.Forms.CheckBox checkBoxPlayReadyPackaging;
        public System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxCustomAttributes;
        private System.Windows.Forms.Panel panelDynEnc;
    }
}