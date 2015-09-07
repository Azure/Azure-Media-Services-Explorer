namespace AMSExplorer
{
    partial class ChooseStreamingEndpoint
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
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.listBoxSE = new System.Windows.Forms.ListBox();
            this.groupBoxForceLocator = new System.Windows.Forms.GroupBox();
            this.listBoxFilter = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtonSmooth = new System.Windows.Forms.RadioButton();
            this.radioButtonHLSv3 = new System.Windows.Forms.RadioButton();
            this.radioButtonHLSv4 = new System.Windows.Forms.RadioButton();
            this.radioButtonHDS = new System.Windows.Forms.RadioButton();
            this.radioButtonDASH = new System.Windows.Forms.RadioButton();
            this.radioButtonSmoothLegacy = new System.Windows.Forms.RadioButton();
            this.textBoxHLSAudioTrackName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.radioButtonHttp = new System.Windows.Forms.RadioButton();
            this.radioButtonHttps = new System.Windows.Forms.RadioButton();
            this.groupBox4.SuspendLayout();
            this.groupBoxForceLocator.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(341, 14);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(112, 27);
            this.buttonOk.TabIndex = 4;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(460, 14);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(112, 27);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.radioButtonHttps);
            this.groupBox4.Controls.Add(this.radioButtonHttp);
            this.groupBox4.Controls.Add(this.listBoxSE);
            this.groupBox4.Location = new System.Drawing.Point(17, 31);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(552, 186);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Streaming Endpoint";
            // 
            // listBoxSE
            // 
            this.listBoxSE.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxSE.FormattingEnabled = true;
            this.listBoxSE.ItemHeight = 15;
            this.listBoxSE.Location = new System.Drawing.Point(13, 33);
            this.listBoxSE.Name = "listBoxSE";
            this.listBoxSE.Size = new System.Drawing.Size(526, 124);
            this.listBoxSE.TabIndex = 0;
            // 
            // groupBoxForceLocator
            // 
            this.groupBoxForceLocator.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxForceLocator.Controls.Add(this.listBoxFilter);
            this.groupBoxForceLocator.Location = new System.Drawing.Point(18, 247);
            this.groupBoxForceLocator.Name = "groupBoxForceLocator";
            this.groupBoxForceLocator.Size = new System.Drawing.Size(207, 242);
            this.groupBoxForceLocator.TabIndex = 3;
            this.groupBoxForceLocator.TabStop = false;
            this.groupBoxForceLocator.Text = "Filter";
            // 
            // listBoxFilter
            // 
            this.listBoxFilter.FormattingEnabled = true;
            this.listBoxFilter.ItemHeight = 15;
            this.listBoxFilter.Location = new System.Drawing.Point(13, 22);
            this.listBoxFilter.Name = "listBoxFilter";
            this.listBoxFilter.Size = new System.Drawing.Size(176, 199);
            this.listBoxFilter.TabIndex = 62;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonOk);
            this.panel1.Location = new System.Drawing.Point(-2, 507);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(587, 55);
            this.panel1.TabIndex = 60;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBoxHLSAudioTrackName);
            this.groupBox1.Controls.Add(this.radioButtonSmoothLegacy);
            this.groupBox1.Controls.Add(this.radioButtonDASH);
            this.groupBox1.Controls.Add(this.radioButtonHDS);
            this.groupBox1.Controls.Add(this.radioButtonHLSv4);
            this.groupBox1.Controls.Add(this.radioButtonHLSv3);
            this.groupBox1.Controls.Add(this.radioButtonSmooth);
            this.groupBox1.Location = new System.Drawing.Point(247, 247);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(323, 242);
            this.groupBox1.TabIndex = 63;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Adaptive Streaming Protocol";
            // 
            // radioButtonSmooth
            // 
            this.radioButtonSmooth.AutoSize = true;
            this.radioButtonSmooth.Checked = true;
            this.radioButtonSmooth.Location = new System.Drawing.Point(23, 39);
            this.radioButtonSmooth.Name = "radioButtonSmooth";
            this.radioButtonSmooth.Size = new System.Drawing.Size(161, 19);
            this.radioButtonSmooth.TabIndex = 0;
            this.radioButtonSmooth.Text = "Auto / Smooth Streaming";
            this.radioButtonSmooth.UseVisualStyleBackColor = true;
            // 
            // radioButtonHLSv3
            // 
            this.radioButtonHLSv3.AutoSize = true;
            this.radioButtonHLSv3.Location = new System.Drawing.Point(23, 89);
            this.radioButtonHLSv3.Name = "radioButtonHLSv3";
            this.radioButtonHLSv3.Size = new System.Drawing.Size(61, 19);
            this.radioButtonHLSv3.TabIndex = 1;
            this.radioButtonHLSv3.Text = "HLS v3";
            this.radioButtonHLSv3.UseVisualStyleBackColor = true;
            this.radioButtonHLSv3.CheckedChanged += new System.EventHandler(this.radioButtonHLSv3_CheckedChanged);
            // 
            // radioButtonHLSv4
            // 
            this.radioButtonHLSv4.AutoSize = true;
            this.radioButtonHLSv4.Location = new System.Drawing.Point(23, 145);
            this.radioButtonHLSv4.Name = "radioButtonHLSv4";
            this.radioButtonHLSv4.Size = new System.Drawing.Size(61, 19);
            this.radioButtonHLSv4.TabIndex = 2;
            this.radioButtonHLSv4.Text = "HLS v4";
            this.radioButtonHLSv4.UseVisualStyleBackColor = true;
            // 
            // radioButtonHDS
            // 
            this.radioButtonHDS.AutoSize = true;
            this.radioButtonHDS.Location = new System.Drawing.Point(23, 195);
            this.radioButtonHDS.Name = "radioButtonHDS";
            this.radioButtonHDS.Size = new System.Drawing.Size(290, 19);
            this.radioButtonHDS.TabIndex = 3;
            this.radioButtonHDS.Text = "HDS (for Adobe PrimeTime/Access licensees only)";
            this.radioButtonHDS.UseVisualStyleBackColor = true;
            // 
            // radioButtonDASH
            // 
            this.radioButtonDASH.AutoSize = true;
            this.radioButtonDASH.Location = new System.Drawing.Point(23, 170);
            this.radioButtonDASH.Name = "radioButtonDASH";
            this.radioButtonDASH.Size = new System.Drawing.Size(93, 19);
            this.radioButtonDASH.TabIndex = 4;
            this.radioButtonDASH.Text = "MPEG-DASH";
            this.radioButtonDASH.UseVisualStyleBackColor = true;
            // 
            // radioButtonSmoothLegacy
            // 
            this.radioButtonSmoothLegacy.AutoSize = true;
            this.radioButtonSmoothLegacy.Location = new System.Drawing.Point(23, 64);
            this.radioButtonSmoothLegacy.Name = "radioButtonSmoothLegacy";
            this.radioButtonSmoothLegacy.Size = new System.Drawing.Size(161, 19);
            this.radioButtonSmoothLegacy.TabIndex = 5;
            this.radioButtonSmoothLegacy.Text = "Smooth Streaming legacy";
            this.radioButtonSmoothLegacy.UseVisualStyleBackColor = true;
            // 
            // textBoxHLSAudioTrackName
            // 
            this.textBoxHLSAudioTrackName.Enabled = false;
            this.textBoxHLSAudioTrackName.Location = new System.Drawing.Point(151, 111);
            this.textBoxHLSAudioTrackName.Name = "textBoxHLSAudioTrackName";
            this.textBoxHLSAudioTrackName.Size = new System.Drawing.Size(150, 23);
            this.textBoxHLSAudioTrackName.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label1.Location = new System.Drawing.Point(40, 114);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 15);
            this.label1.TabIndex = 7;
            this.label1.Text = "Audio track name :";
            // 
            // radioButtonHttp
            // 
            this.radioButtonHttp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.radioButtonHttp.AutoSize = true;
            this.radioButtonHttp.Checked = true;
            this.radioButtonHttp.Location = new System.Drawing.Point(13, 161);
            this.radioButtonHttp.Name = "radioButtonHttp";
            this.radioButtonHttp.Size = new System.Drawing.Size(49, 19);
            this.radioButtonHttp.TabIndex = 62;
            this.radioButtonHttp.TabStop = true;
            this.radioButtonHttp.Text = "Http";
            this.radioButtonHttp.UseVisualStyleBackColor = true;
            // 
            // radioButtonHttps
            // 
            this.radioButtonHttps.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.radioButtonHttps.AutoSize = true;
            this.radioButtonHttps.Location = new System.Drawing.Point(68, 161);
            this.radioButtonHttps.Name = "radioButtonHttps";
            this.radioButtonHttps.Size = new System.Drawing.Size(54, 19);
            this.radioButtonHttps.TabIndex = 63;
            this.radioButtonHttps.Text = "Https";
            this.radioButtonHttps.UseVisualStyleBackColor = true;
            // 
            // ChooseStreamingEndpoint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(584, 561);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBoxForceLocator);
            this.Controls.Add(this.groupBox4);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "ChooseStreamingEndpoint";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Options to generate the streaming URL";
            this.Load += new System.EventHandler(this.ChooseStreamingEndpoint_Load);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBoxForceLocator.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Button buttonOk;
        public System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBoxForceLocator;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListBox listBoxSE;
        private System.Windows.Forms.ListBox listBoxFilter;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxHLSAudioTrackName;
        private System.Windows.Forms.RadioButton radioButtonSmoothLegacy;
        private System.Windows.Forms.RadioButton radioButtonDASH;
        private System.Windows.Forms.RadioButton radioButtonHDS;
        private System.Windows.Forms.RadioButton radioButtonHLSv4;
        private System.Windows.Forms.RadioButton radioButtonHLSv3;
        private System.Windows.Forms.RadioButton radioButtonSmooth;
        private System.Windows.Forms.RadioButton radioButtonHttps;
        private System.Windows.Forms.RadioButton radioButtonHttp;
    }
}