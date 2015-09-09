﻿namespace AMSExplorer
{
    partial class HLSAESStatic
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
            this.textBoxServiceSegment = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxMaxBitrate = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.textBoxKeyURL = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.checkBoxEncrypt = new System.Windows.Forms.CheckBox();
            this.textBoxkey = new System.Windows.Forms.TextBox();
            this.buttonGenKey = new System.Windows.Forms.Button();
            this.labelAssetName = new System.Windows.Forms.Label();
            this.textboxoutputassetname = new System.Windows.Forms.TextBox();
            this.processorlabel = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonOk = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabPage2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxServiceSegment
            // 
            this.textBoxServiceSegment.Location = new System.Drawing.Point(10, 93);
            this.textBoxServiceSegment.Name = "textBoxServiceSegment";
            this.textBoxServiceSegment.Size = new System.Drawing.Size(482, 23);
            this.textBoxServiceSegment.TabIndex = 37;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 75);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 15);
            this.label6.TabIndex = 36;
            this.label6.Text = "Segment :";
            // 
            // textBoxMaxBitrate
            // 
            this.textBoxMaxBitrate.Location = new System.Drawing.Point(10, 35);
            this.textBoxMaxBitrate.Name = "textBoxMaxBitrate";
            this.textBoxMaxBitrate.Size = new System.Drawing.Size(482, 23);
            this.textBoxMaxBitrate.TabIndex = 35;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 15);
            this.label4.TabIndex = 34;
            this.label4.Text = "Max bitrate :";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.textBoxServiceSegment);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.textBoxMaxBitrate);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(504, 274);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Packaging settings";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // textBoxKeyURL
            // 
            this.textBoxKeyURL.Enabled = false;
            this.textBoxKeyURL.Location = new System.Drawing.Point(10, 129);
            this.textBoxKeyURL.Name = "textBoxKeyURL";
            this.textBoxKeyURL.Size = new System.Drawing.Size(482, 23);
            this.textBoxKeyURL.TabIndex = 37;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 15);
            this.label1.TabIndex = 36;
            this.label1.Text = "Key:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 111);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 15);
            this.label2.TabIndex = 35;
            this.label2.Text = "Key Acquisition Url:";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(17, 61);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(512, 302);
            this.tabControl1.TabIndex = 42;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.pictureBox1);
            this.tabPage1.Controls.Add(this.checkBoxEncrypt);
            this.tabPage1.Controls.Add(this.textBoxKeyURL);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.textBoxkey);
            this.tabPage1.Controls.Add(this.buttonGenKey);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(504, 274);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "AES Encryption";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::AMSExplorer.Bitmaps.envelope_encryption;
            this.pictureBox1.Location = new System.Drawing.Point(10, 17);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 49;
            this.pictureBox1.TabStop = false;
            // 
            // checkBoxEncrypt
            // 
            this.checkBoxEncrypt.AutoSize = true;
            this.checkBoxEncrypt.Location = new System.Drawing.Point(36, 17);
            this.checkBoxEncrypt.Name = "checkBoxEncrypt";
            this.checkBoxEncrypt.Size = new System.Drawing.Size(138, 19);
            this.checkBoxEncrypt.TabIndex = 38;
            this.checkBoxEncrypt.Text = "Encrypt with AES-128";
            this.checkBoxEncrypt.UseVisualStyleBackColor = true;
            this.checkBoxEncrypt.CheckedChanged += new System.EventHandler(this.checkBoxEncrypt_CheckedChanged);
            // 
            // textBoxkey
            // 
            this.textBoxkey.Enabled = false;
            this.textBoxkey.Location = new System.Drawing.Point(10, 73);
            this.textBoxkey.Name = "textBoxkey";
            this.textBoxkey.Size = new System.Drawing.Size(371, 23);
            this.textBoxkey.TabIndex = 34;
            // 
            // buttonGenKey
            // 
            this.buttonGenKey.Enabled = false;
            this.buttonGenKey.Location = new System.Drawing.Point(395, 70);
            this.buttonGenKey.Name = "buttonGenKey";
            this.buttonGenKey.Size = new System.Drawing.Size(97, 27);
            this.buttonGenKey.TabIndex = 33;
            this.buttonGenKey.Text = "Generate";
            this.buttonGenKey.UseVisualStyleBackColor = true;
            this.buttonGenKey.Click += new System.EventHandler(this.buttonGenKeyID_Click);
            // 
            // labelAssetName
            // 
            this.labelAssetName.AutoSize = true;
            this.labelAssetName.Location = new System.Drawing.Point(14, 24);
            this.labelAssetName.Name = "labelAssetName";
            this.labelAssetName.Size = new System.Drawing.Size(65, 15);
            this.labelAssetName.TabIndex = 40;
            this.labelAssetName.Text = "Assetname";
            // 
            // textboxoutputassetname
            // 
            this.textboxoutputassetname.Location = new System.Drawing.Point(21, 414);
            this.textboxoutputassetname.Name = "textboxoutputassetname";
            this.textboxoutputassetname.Size = new System.Drawing.Size(504, 23);
            this.textboxoutputassetname.TabIndex = 38;
            // 
            // processorlabel
            // 
            this.processorlabel.Location = new System.Drawing.Point(18, 463);
            this.processorlabel.Name = "processorlabel";
            this.processorlabel.Size = new System.Drawing.Size(426, 25);
            this.processorlabel.TabIndex = 37;
            this.processorlabel.Text = "processor name";
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(425, 15);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(115, 27);
            this.buttonCancel.TabIndex = 34;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 392);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(190, 15);
            this.label3.TabIndex = 39;
            this.label3.Text = "Please enter an output asset name:";
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Image = global::AMSExplorer.Bitmaps.packaging;
            this.buttonOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonOk.Location = new System.Drawing.Point(253, 15);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(164, 27);
            this.buttonOk.TabIndex = 35;
            this.buttonOk.Text = "Launch Packaging";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonOk);
            this.panel1.Location = new System.Drawing.Point(-1, 505);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(559, 55);
            this.panel1.TabIndex = 62;
            // 
            // HLSAESStatic
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(553, 561);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.labelAssetName);
            this.Controls.Add(this.textboxoutputassetname);
            this.Controls.Add(this.processorlabel);
            this.Controls.Add(this.label3);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "HLSAESStatic";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Packaging to HLS v3 (static)";
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox textBoxServiceSegment;
        public System.Windows.Forms.Label label6;
        public System.Windows.Forms.TextBox textBoxMaxBitrate;
        public System.Windows.Forms.Label label4;
        private System.Windows.Forms.TabPage tabPage2;
        public System.Windows.Forms.TextBox textBoxKeyURL;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.CheckBox checkBoxEncrypt;
        public System.Windows.Forms.TextBox textBoxkey;
        private System.Windows.Forms.Button buttonGenKey;
        public System.Windows.Forms.Label labelAssetName;
        public System.Windows.Forms.TextBox textboxoutputassetname;
        private System.Windows.Forms.Label processorlabel;
        public System.Windows.Forms.Button buttonOk;
        public System.Windows.Forms.Button buttonCancel;
        public System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
    }
}