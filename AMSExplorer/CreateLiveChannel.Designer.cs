namespace AMSExplorer
{
    partial class CreateLiveChannel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateLiveChannel));
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.textBoxRestrictIngestIP = new System.Windows.Forms.TextBox();
            this.checkBoxRestrictIngestIP = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textboxchannelname = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxProtocol = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numericUpDownHLSFragPerSeg = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxKeyFrame = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.labelWarning = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.checkBoxStartChannel = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHLSFragPerSeg)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(136, 448);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(113, 32);
            this.buttonOk.TabIndex = 8;
            this.buttonOk.Text = "Create Channel";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(270, 448);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(113, 32);
            this.buttonCancel.TabIndex = 9;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // textBoxRestrictIngestIP
            // 
            this.textBoxRestrictIngestIP.Enabled = false;
            this.textBoxRestrictIngestIP.Location = new System.Drawing.Point(23, 202);
            this.textBoxRestrictIngestIP.Name = "textBoxRestrictIngestIP";
            this.textBoxRestrictIngestIP.Size = new System.Drawing.Size(239, 20);
            this.textBoxRestrictIngestIP.TabIndex = 6;
            this.textBoxRestrictIngestIP.TextChanged += new System.EventHandler(this.textBoxRestrictIngestIP_TextChanged);
            // 
            // checkBoxRestrictIngestIP
            // 
            this.checkBoxRestrictIngestIP.AutoSize = true;
            this.checkBoxRestrictIngestIP.Location = new System.Drawing.Point(23, 179);
            this.checkBoxRestrictIngestIP.Name = "checkBoxRestrictIngestIP";
            this.checkBoxRestrictIngestIP.Size = new System.Drawing.Size(183, 17);
            this.checkBoxRestrictIngestIP.TabIndex = 5;
            this.checkBoxRestrictIngestIP.Text = "Restrict ingest to this IP address :";
            this.checkBoxRestrictIngestIP.UseVisualStyleBackColor = true;
            this.checkBoxRestrictIngestIP.CheckedChanged += new System.EventHandler(this.checkBoxRestrictIngestIP_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 13);
            this.label3.TabIndex = 45;
            this.label3.Text = "Channel name :";
            // 
            // textboxchannelname
            // 
            this.textboxchannelname.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textboxchannelname.Location = new System.Drawing.Point(30, 32);
            this.textboxchannelname.Name = "textboxchannelname";
            this.textboxchannelname.Size = new System.Drawing.Size(429, 20);
            this.textboxchannelname.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Protocol :";
            // 
            // comboBoxProtocol
            // 
            this.comboBoxProtocol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxProtocol.FormattingEnabled = true;
            this.comboBoxProtocol.Location = new System.Drawing.Point(23, 39);
            this.comboBoxProtocol.Name = "comboBoxProtocol";
            this.comboBoxProtocol.Size = new System.Drawing.Size(121, 21);
            this.comboBoxProtocol.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numericUpDownHLSFragPerSeg);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBoxKeyFrame);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.labelWarning);
            this.groupBox1.Controls.Add(this.textBoxRestrictIngestIP);
            this.groupBox1.Controls.Add(this.comboBoxProtocol);
            this.groupBox1.Controls.Add(this.checkBoxRestrictIngestIP);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(30, 126);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(429, 271);
            this.groupBox1.TabIndex = 46;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings";
            // 
            // numericUpDownHLSFragPerSeg
            // 
            this.numericUpDownHLSFragPerSeg.Location = new System.Drawing.Point(23, 144);
            this.numericUpDownHLSFragPerSeg.Name = "numericUpDownHLSFragPerSeg";
            this.numericUpDownHLSFragPerSeg.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownHLSFragPerSeg.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 127);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(144, 13);
            this.label2.TabIndex = 48;
            this.label2.Text = "HLS fragments per segment :";
            // 
            // textBoxKeyFrame
            // 
            this.textBoxKeyFrame.Location = new System.Drawing.Point(23, 91);
            this.textBoxKeyFrame.Name = "textBoxKeyFrame";
            this.textBoxKeyFrame.Size = new System.Drawing.Size(121, 20);
            this.textBoxKeyFrame.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 13);
            this.label1.TabIndex = 46;
            this.label1.Text = "Key frame interval (s) :";
            // 
            // labelWarning
            // 
            this.labelWarning.AutoSize = true;
            this.labelWarning.ForeColor = System.Drawing.Color.Red;
            this.labelWarning.Location = new System.Drawing.Point(25, 234);
            this.labelWarning.Name = "labelWarning";
            this.labelWarning.Size = new System.Drawing.Size(47, 13);
            this.labelWarning.TabIndex = 45;
            this.labelWarning.Text = "Warning";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(27, 63);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 13);
            this.label5.TabIndex = 48;
            this.label5.Text = "Description :";
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDescription.Location = new System.Drawing.Point(30, 79);
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(429, 20);
            this.textBoxDescription.TabIndex = 1;
            // 
            // checkBoxStartChannel
            // 
            this.checkBoxStartChannel.AutoSize = true;
            this.checkBoxStartChannel.Location = new System.Drawing.Point(30, 414);
            this.checkBoxStartChannel.Name = "checkBoxStartChannel";
            this.checkBoxStartChannel.Size = new System.Drawing.Size(153, 17);
            this.checkBoxStartChannel.TabIndex = 7;
            this.checkBoxStartChannel.Text = "Start the new channel now";
            this.checkBoxStartChannel.UseVisualStyleBackColor = true;
            // 
            // CreateLiveChannel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(490, 492);
            this.Controls.Add(this.checkBoxStartChannel);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxDescription);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textboxchannelname);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.buttonCancel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CreateLiveChannel";
            this.Text = "Create a live channel";
            this.Load += new System.EventHandler(this.CreateLocator_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHLSFragPerSeg)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button buttonOk;
        public System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textboxchannelname;
        private System.Windows.Forms.TextBox textBoxRestrictIngestIP;
        private System.Windows.Forms.CheckBox checkBoxRestrictIngestIP;
        private System.Windows.Forms.ComboBox comboBoxProtocol;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelWarning;
        private System.Windows.Forms.TextBox textBoxKeyFrame;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDownHLSFragPerSeg;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.CheckBox checkBoxStartChannel;
    }
}