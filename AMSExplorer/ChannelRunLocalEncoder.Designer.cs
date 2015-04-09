namespace AMSExplorer
{
    partial class ChannelRunLocalEncoder
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
            this.textBoxVideoDeviceName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxAudioDeviceName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxEncoder = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.labelcdn = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxArguments = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxProgram = new System.Windows.Forms.TextBox();
            this.labelChannel = new System.Windows.Forms.Label();
            this.textBoxVideoBitRate = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxAudioBitRate = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(576, 12);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(96, 23);
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
            this.buttonCancel.Text = "Cancel";
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
            this.groupBox1.Controls.Add(this.textBoxAudioBitRate);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.textBoxVideoBitRate);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.textBoxVideoDeviceName);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.textBoxAudioDeviceName);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.comboBoxEncoder);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(15, 64);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(570, 224);
            this.groupBox1.TabIndex = 63;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Encoder";
            // 
            // textBoxVideoDeviceName
            // 
            this.textBoxVideoDeviceName.Location = new System.Drawing.Point(22, 143);
            this.textBoxVideoDeviceName.Name = "textBoxVideoDeviceName";
            this.textBoxVideoDeviceName.Size = new System.Drawing.Size(522, 20);
            this.textBoxVideoDeviceName.TabIndex = 79;
            this.textBoxVideoDeviceName.Text = "USB2.0 HD UVC WebCam";
            this.textBoxVideoDeviceName.TextChanged += new System.EventHandler(this.EncoderSettings_Changed);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 127);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 13);
            this.label5.TabIndex = 78;
            this.label5.Text = "Video device name :";
            // 
            // textBoxAudioDeviceName
            // 
            this.textBoxAudioDeviceName.Location = new System.Drawing.Point(22, 97);
            this.textBoxAudioDeviceName.Name = "textBoxAudioDeviceName";
            this.textBoxAudioDeviceName.Size = new System.Drawing.Size(522, 20);
            this.textBoxAudioDeviceName.TabIndex = 77;
            this.textBoxAudioDeviceName.Text = "Microphone (High Definition Audio Device)";
            this.textBoxAudioDeviceName.TextChanged += new System.EventHandler(this.EncoderSettings_Changed);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 81);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 13);
            this.label4.TabIndex = 71;
            this.label4.Text = "Audo device name :";
            // 
            // comboBoxEncoder
            // 
            this.comboBoxEncoder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEncoder.FormattingEnabled = true;
            this.comboBoxEncoder.Location = new System.Drawing.Point(22, 43);
            this.comboBoxEncoder.Name = "comboBoxEncoder";
            this.comboBoxEncoder.Size = new System.Drawing.Size(255, 21);
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
            this.labelcdn.Location = new System.Drawing.Point(12, 11);
            this.labelcdn.Name = "labelcdn";
            this.labelcdn.Size = new System.Drawing.Size(459, 19);
            this.labelcdn.TabIndex = 70;
            this.labelcdn.Text = "Run a local encoder to push a live stream to a channel in Azure Media Services";
            this.labelcdn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 351);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 13);
            this.label1.TabIndex = 74;
            this.label1.Text = "Arguments (editable):";
            // 
            // textBoxArguments
            // 
            this.textBoxArguments.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxArguments.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxArguments.Location = new System.Drawing.Point(15, 367);
            this.textBoxArguments.Multiline = true;
            this.textBoxArguments.Name = "textBoxArguments";
            this.textBoxArguments.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxArguments.Size = new System.Drawing.Size(731, 101);
            this.textBoxArguments.TabIndex = 73;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 303);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 13);
            this.label3.TabIndex = 75;
            this.label3.Text = "Program (editable):";
            // 
            // textBoxProgram
            // 
            this.textBoxProgram.Location = new System.Drawing.Point(15, 320);
            this.textBoxProgram.Name = "textBoxProgram";
            this.textBoxProgram.Size = new System.Drawing.Size(731, 20);
            this.textBoxProgram.TabIndex = 76;
            // 
            // labelChannel
            // 
            this.labelChannel.AutoSize = true;
            this.labelChannel.Location = new System.Drawing.Point(15, 40);
            this.labelChannel.Name = "labelChannel";
            this.labelChannel.Size = new System.Drawing.Size(69, 13);
            this.labelChannel.TabIndex = 77;
            this.labelChannel.Text = "Channel : {0}";
            // 
            // textBoxVideoBitRate
            // 
            this.textBoxVideoBitRate.Location = new System.Drawing.Point(22, 198);
            this.textBoxVideoBitRate.Name = "textBoxVideoBitRate";
            this.textBoxVideoBitRate.Size = new System.Drawing.Size(156, 20);
            this.textBoxVideoBitRate.TabIndex = 81;
            this.textBoxVideoBitRate.Text = "1500";
            this.textBoxVideoBitRate.TextChanged += new System.EventHandler(this.EncoderSettings_Changed);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(19, 182);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(105, 13);
            this.label6.TabIndex = 80;
            this.label6.Text = "Video Bitrate (kbps) :";
            // 
            // textBoxAudioBitRate
            // 
            this.textBoxAudioBitRate.Location = new System.Drawing.Point(220, 198);
            this.textBoxAudioBitRate.Name = "textBoxAudioBitRate";
            this.textBoxAudioBitRate.Size = new System.Drawing.Size(156, 20);
            this.textBoxAudioBitRate.TabIndex = 83;
            this.textBoxAudioBitRate.Text = "96";
            this.textBoxAudioBitRate.TextChanged += new System.EventHandler(this.EncoderSettings_Changed);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(217, 182);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(105, 13);
            this.label7.TabIndex = 82;
            this.label7.Text = "Audio Bitrate (kbps) :";
            // 
            // ChannelRunLocalEncoder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.labelChannel);
            this.Controls.Add(this.textBoxProgram);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxArguments);
            this.Controls.Add(this.labelcdn);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Name = "ChannelRunLocalEncoder";
            this.Text = "Run a local encoder";
            this.Load += new System.EventHandler(this.ChannelRunLocalEncoder_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
        private System.Windows.Forms.TextBox textBoxArguments;
        public System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxProgram;
        private System.Windows.Forms.TextBox textBoxVideoDeviceName;
        public System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxAudioDeviceName;
        public System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelChannel;
        private System.Windows.Forms.TextBox textBoxVideoBitRate;
        public System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxAudioBitRate;
        public System.Windows.Forms.Label label7;
    }
}