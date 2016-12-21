namespace AMSExplorer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChannelRunOnPremisesEncoder));
            this.buttonLaunch = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
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
            // buttonLaunch
            // 
            resources.ApplyResources(this.buttonLaunch, "buttonLaunch");
            this.buttonLaunch.Image = global::AMSExplorer.Bitmaps.encoding;
            this.buttonLaunch.Name = "buttonLaunch";
            this.buttonLaunch.UseVisualStyleBackColor = true;
            this.buttonLaunch.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonClose
            // 
            resources.ApplyResources(this.buttonClose, "buttonClose");
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonClose);
            this.panel1.Controls.Add(this.buttonLaunch);
            this.panel1.Name = "panel1";
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.labelWarning);
            this.groupBox1.Controls.Add(this.panelAVSettings);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.linkLabelInstall);
            this.groupBox1.Controls.Add(this.textBoxComment);
            this.groupBox1.Controls.Add(this.comboBoxEncoder);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // labelWarning
            // 
            resources.ApplyResources(this.labelWarning, "labelWarning");
            this.labelWarning.ForeColor = System.Drawing.Color.Red;
            this.labelWarning.Name = "labelWarning";
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
            resources.ApplyResources(this.panelAVSettings, "panelAVSettings");
            this.panelAVSettings.Name = "panelAVSettings";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // textBoxAudioDeviceName
            // 
            resources.ApplyResources(this.textBoxAudioDeviceName, "textBoxAudioDeviceName");
            this.textBoxAudioDeviceName.Name = "textBoxAudioDeviceName";
            this.textBoxAudioDeviceName.TextChanged += new System.EventHandler(this.EncoderSettings_Changed);
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // textBoxVideoDeviceName
            // 
            resources.ApplyResources(this.textBoxVideoDeviceName, "textBoxVideoDeviceName");
            this.textBoxVideoDeviceName.Name = "textBoxVideoDeviceName";
            this.textBoxVideoDeviceName.TextChanged += new System.EventHandler(this.EncoderSettings_Changed);
            // 
            // textBoxAudioBitRate
            // 
            resources.ApplyResources(this.textBoxAudioBitRate, "textBoxAudioBitRate");
            this.textBoxAudioBitRate.Name = "textBoxAudioBitRate";
            this.textBoxAudioBitRate.TextChanged += new System.EventHandler(this.EncoderSettings_Changed);
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // textBoxVideoBitRate
            // 
            resources.ApplyResources(this.textBoxVideoBitRate, "textBoxVideoBitRate");
            this.textBoxVideoBitRate.Name = "textBoxVideoBitRate";
            this.textBoxVideoBitRate.TextChanged += new System.EventHandler(this.EncoderSettings_Changed);
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // linkLabelInstall
            // 
            resources.ApplyResources(this.linkLabelInstall, "linkLabelInstall");
            this.linkLabelInstall.Name = "linkLabelInstall";
            this.linkLabelInstall.TabStop = true;
            this.linkLabelInstall.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelInstall_LinkClicked);
            // 
            // textBoxComment
            // 
            resources.ApplyResources(this.textBoxComment, "textBoxComment");
            this.textBoxComment.Name = "textBoxComment";
            // 
            // comboBoxEncoder
            // 
            this.comboBoxEncoder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEncoder.FormattingEnabled = true;
            resources.ApplyResources(this.comboBoxEncoder, "comboBoxEncoder");
            this.comboBoxEncoder.Name = "comboBoxEncoder";
            this.comboBoxEncoder.SelectedIndexChanged += new System.EventHandler(this.comboBoxEncoder_SelectedIndexChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // labelcdn
            // 
            this.labelcdn.ForeColor = System.Drawing.SystemColors.WindowFrame;
            resources.ApplyResources(this.labelcdn, "labelcdn");
            this.labelcdn.Name = "labelcdn";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // textBoxCommand
            // 
            resources.ApplyResources(this.textBoxCommand, "textBoxCommand");
            this.textBoxCommand.Name = "textBoxCommand";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // textBoxFolder
            // 
            resources.ApplyResources(this.textBoxFolder, "textBoxFolder");
            this.textBoxFolder.Name = "textBoxFolder";
            // 
            // labelChannel
            // 
            resources.ApplyResources(this.labelChannel, "labelChannel");
            this.labelChannel.Name = "labelChannel";
            // 
            // labelURL
            // 
            resources.ApplyResources(this.labelURL, "labelURL");
            this.labelURL.Name = "labelURL";
            // 
            // ChannelRunOnPremisesEncoder
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonClose;
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

        public System.Windows.Forms.Button buttonLaunch;
        public System.Windows.Forms.Button buttonClose;
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