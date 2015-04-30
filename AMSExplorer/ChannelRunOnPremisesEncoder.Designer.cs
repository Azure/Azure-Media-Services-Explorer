﻿namespace AMSExplorer
{
    partial class ChannelRunOnPremisesEncoder
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelWarning = new System.Windows.Forms.Label();
            this.panelAVSettings = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxAudioDeviceName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxVideoDeviceName = new System.Windows.Forms.TextBox();
            this.textBoxAudioBitRate = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxVideoBitRate = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.linkLabelInstall = new System.Windows.Forms.LinkLabel();
            this.textBoxComment = new System.Windows.Forms.TextBox();
            this.comboBoxEncoder = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.labelcdn = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxCommand = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxFolder = new System.Windows.Forms.TextBox();
            this.labelChannel = new System.Windows.Forms.Label();
            this.labelURL = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panelAVSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.Image = global::AMSExplorer.Bitmaps.encoding;
            this.buttonOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonOk.Location = new System.Drawing.Point(532, 12);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(140, 23);
            this.buttonOk.TabIndex = 4;
            this.buttonOk.Text = "Launch encoder";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(678, 12);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(96, 23);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Close";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonOk);
            this.panel1.Location = new System.Drawing.Point(-2, 514);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(787, 48);
            this.panel1.TabIndex = 60;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.labelWarning);
            this.groupBox1.Controls.Add(this.panelAVSettings);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.linkLabelInstall);
            this.groupBox1.Controls.Add(this.textBoxComment);
            this.groupBox1.Controls.Add(this.comboBoxEncoder);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(15, 88);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(731, 224);
            this.groupBox1.TabIndex = 63;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Encoder settings";
            // 
            // labelWarning
            // 
            this.labelWarning.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelWarning.AutoSize = true;
            this.labelWarning.ForeColor = System.Drawing.Color.Red;
            this.labelWarning.Location = new System.Drawing.Point(423, 16);
            this.labelWarning.Name = "labelWarning";
            this.labelWarning.Size = new System.Drawing.Size(47, 13);
            this.labelWarning.TabIndex = 79;
            this.labelWarning.Text = "Warning";
            // 
            // panelAVSettings
            // 
            this.panelAVSettings.Controls.Add(this.label4);
            this.panelAVSettings.Controls.Add(this.textBoxAudioDeviceName);
            this.panelAVSettings.Controls.Add(this.label5);
            this.panelAVSettings.Controls.Add(this.textBoxVideoDeviceName);
            this.panelAVSettings.Controls.Add(this.textBoxAudioBitRate);
            this.panelAVSettings.Controls.Add(this.label6);
            this.panelAVSettings.Controls.Add(this.label7);
            this.panelAVSettings.Controls.Add(this.textBoxVideoBitRate);
            this.panelAVSettings.Location = new System.Drawing.Point(6, 70);
            this.panelAVSettings.Name = "panelAVSettings";
            this.panelAVSettings.Size = new System.Drawing.Size(388, 148);
            this.panelAVSettings.TabIndex = 85;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 13);
            this.label4.TabIndex = 71;
            this.label4.Text = "Audo device name :";
            // 
            // textBoxAudioDeviceName
            // 
            this.textBoxAudioDeviceName.Location = new System.Drawing.Point(16, 22);
            this.textBoxAudioDeviceName.Name = "textBoxAudioDeviceName";
            this.textBoxAudioDeviceName.Size = new System.Drawing.Size(354, 20);
            this.textBoxAudioDeviceName.TabIndex = 77;
            this.textBoxAudioDeviceName.Text = "Microphone (High Definition Audio Device)";
            this.textBoxAudioDeviceName.TextChanged += new System.EventHandler(this.EncoderSettings_Changed);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 52);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 13);
            this.label5.TabIndex = 78;
            this.label5.Text = "Video device name :";
            // 
            // textBoxVideoDeviceName
            // 
            this.textBoxVideoDeviceName.Location = new System.Drawing.Point(16, 68);
            this.textBoxVideoDeviceName.Name = "textBoxVideoDeviceName";
            this.textBoxVideoDeviceName.Size = new System.Drawing.Size(354, 20);
            this.textBoxVideoDeviceName.TabIndex = 79;
            this.textBoxVideoDeviceName.Text = "USB2.0 HD UVC WebCam";
            this.textBoxVideoDeviceName.TextChanged += new System.EventHandler(this.EncoderSettings_Changed);
            // 
            // textBoxAudioBitRate
            // 
            this.textBoxAudioBitRate.Location = new System.Drawing.Point(204, 123);
            this.textBoxAudioBitRate.Name = "textBoxAudioBitRate";
            this.textBoxAudioBitRate.Size = new System.Drawing.Size(166, 20);
            this.textBoxAudioBitRate.TabIndex = 83;
            this.textBoxAudioBitRate.Text = "96";
            this.textBoxAudioBitRate.TextChanged += new System.EventHandler(this.EncoderSettings_Changed);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 107);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(105, 13);
            this.label6.TabIndex = 80;
            this.label6.Text = "Video Bitrate (kbps) :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(201, 107);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(105, 13);
            this.label7.TabIndex = 82;
            this.label7.Text = "Audio Bitrate (kbps) :";
            // 
            // textBoxVideoBitRate
            // 
            this.textBoxVideoBitRate.Location = new System.Drawing.Point(16, 123);
            this.textBoxVideoBitRate.Name = "textBoxVideoBitRate";
            this.textBoxVideoBitRate.Size = new System.Drawing.Size(156, 20);
            this.textBoxVideoBitRate.TabIndex = 81;
            this.textBoxVideoBitRate.Text = "1500";
            this.textBoxVideoBitRate.TextChanged += new System.EventHandler(this.EncoderSettings_Changed);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(423, 76);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(57, 13);
            this.label8.TabIndex = 80;
            this.label8.Text = "Comment :";
            // 
            // linkLabelInstall
            // 
            this.linkLabelInstall.AutoSize = true;
            this.linkLabelInstall.Location = new System.Drawing.Point(423, 46);
            this.linkLabelInstall.Name = "linkLabelInstall";
            this.linkLabelInstall.Size = new System.Drawing.Size(76, 13);
            this.linkLabelInstall.TabIndex = 84;
            this.linkLabelInstall.TabStop = true;
            this.linkLabelInstall.Text = "Installation link";
            this.linkLabelInstall.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelInstall_LinkClicked);
            // 
            // textBoxComment
            // 
            this.textBoxComment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxComment.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxComment.Location = new System.Drawing.Point(426, 92);
            this.textBoxComment.Multiline = true;
            this.textBoxComment.Name = "textBoxComment";
            this.textBoxComment.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxComment.Size = new System.Drawing.Size(288, 126);
            this.textBoxComment.TabIndex = 79;
            // 
            // comboBoxEncoder
            // 
            this.comboBoxEncoder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEncoder.FormattingEnabled = true;
            this.comboBoxEncoder.Location = new System.Drawing.Point(22, 43);
            this.comboBoxEncoder.Name = "comboBoxEncoder";
            this.comboBoxEncoder.Size = new System.Drawing.Size(354, 21);
            this.comboBoxEncoder.TabIndex = 70;
            this.comboBoxEncoder.SelectedIndexChanged += new System.EventHandler(this.comboBoxEncoder_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 69;
            this.label2.Text = "Encoder :";
            // 
            // labelcdn
            // 
            this.labelcdn.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.labelcdn.Location = new System.Drawing.Point(34, 9);
            this.labelcdn.Name = "labelcdn";
            this.labelcdn.Size = new System.Drawing.Size(734, 19);
            this.labelcdn.TabIndex = 70;
            this.labelcdn.Text = "Run an on-premises live encoder to push a live stream to a channel. You can defin" +
    "e the ffmpeg or VLC installation path in Options.";
            this.labelcdn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 375);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 13);
            this.label1.TabIndex = 74;
            this.label1.Text = "Command (editable):";
            // 
            // textBoxCommand
            // 
            this.textBoxCommand.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxCommand.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxCommand.Location = new System.Drawing.Point(37, 391);
            this.textBoxCommand.Multiline = true;
            this.textBoxCommand.Name = "textBoxCommand";
            this.textBoxCommand.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxCommand.Size = new System.Drawing.Size(709, 101);
            this.textBoxCommand.TabIndex = 73;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 328);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(138, 13);
            this.label3.TabIndex = 75;
            this.label3.Text = "Folder to program (editable):";
            // 
            // textBoxFolder
            // 
            this.textBoxFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFolder.Location = new System.Drawing.Point(37, 344);
            this.textBoxFolder.Name = "textBoxFolder";
            this.textBoxFolder.Size = new System.Drawing.Size(709, 20);
            this.textBoxFolder.TabIndex = 76;
            // 
            // labelChannel
            // 
            this.labelChannel.AutoSize = true;
            this.labelChannel.Location = new System.Drawing.Point(34, 40);
            this.labelChannel.Name = "labelChannel";
            this.labelChannel.Size = new System.Drawing.Size(139, 13);
            this.labelChannel.TabIndex = 77;
            this.labelChannel.Text = "Channel : {0} (protocol : {1})";
            // 
            // labelURL
            // 
            this.labelURL.AutoSize = true;
            this.labelURL.Location = new System.Drawing.Point(34, 62);
            this.labelURL.Name = "labelURL";
            this.labelURL.Size = new System.Drawing.Size(52, 13);
            this.labelURL.TabIndex = 78;
            this.labelURL.Text = "URL : {0}";
            // 
            // ChannelRunOnPremisesEncoder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.labelURL);
            this.Controls.Add(this.labelChannel);
            this.Controls.Add(this.textBoxFolder);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxCommand);
            this.Controls.Add(this.labelcdn);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Name = "ChannelRunOnPremisesEncoder";
            this.Text = "Run an on-premises live encoder";
            this.Load += new System.EventHandler(this.ChannelRunLocalEncoder_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panelAVSettings.ResumeLayout(false);
            this.panelAVSettings.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button buttonOk;
        public System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelcdn;
        private System.Windows.Forms.ComboBox comboBoxEncoder;
        public System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxCommand;
        public System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxFolder;
        private System.Windows.Forms.TextBox textBoxVideoDeviceName;
        public System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxAudioDeviceName;
        public System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelChannel;
        private System.Windows.Forms.TextBox textBoxVideoBitRate;
        public System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxAudioBitRate;
        public System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label labelURL;
        private System.Windows.Forms.LinkLabel linkLabelInstall;
        public System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxComment;
        private System.Windows.Forms.Panel panelAVSettings;
        private System.Windows.Forms.Label labelWarning;
    }
}