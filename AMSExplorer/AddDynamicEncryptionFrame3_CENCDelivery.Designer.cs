namespace AMSExplorer
{
    partial class AddDynamicEncryptionFrame3_CENCDelivery
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
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBoxPlayReady = new System.Windows.Forms.GroupBox();
            this.panelExternalPlayReady = new System.Windows.Forms.Panel();
            this.textBoxPRLAurl = new System.Windows.Forms.TextBox();
            this.buttonPlayReadyTestSettings = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.checkBoxEncodingSL = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownNbOptionsPR = new System.Windows.Forms.NumericUpDown();
            this.radioButtonDeliverPRfromAMS = new System.Windows.Forms.RadioButton();
            this.radioButtonExternalPRServer = new System.Windows.Forms.RadioButton();
            this.groupBoxWidevine = new System.Windows.Forms.GroupBox();
            this.radioButtonExternalWVServer = new System.Windows.Forms.RadioButton();
            this.radioButtonDeliverWVFromAMS = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.LAURLWidevine = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.numericUpDownNbOptionsWV = new System.Windows.Forms.NumericUpDown();
            this.panel1.SuspendLayout();
            this.groupBoxPlayReady.SuspendLayout();
            this.panelExternalPlayReady.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNbOptionsPR)).BeginInit();
            this.groupBoxWidevine.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNbOptionsWV)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkBlue;
            this.label1.Location = new System.Drawing.Point(26, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 42);
            this.label1.TabIndex = 49;
            this.label1.Text = "Step 3\r\nLicense delivery";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonOk);
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Location = new System.Drawing.Point(-1, 688);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(684, 55);
            this.panel1.TabIndex = 51;
            // 
            // groupBoxPlayReady
            // 
            this.groupBoxPlayReady.Controls.Add(this.panelExternalPlayReady);
            this.groupBoxPlayReady.Controls.Add(this.label8);
            this.groupBoxPlayReady.Controls.Add(this.label2);
            this.groupBoxPlayReady.Controls.Add(this.numericUpDownNbOptionsPR);
            this.groupBoxPlayReady.Controls.Add(this.radioButtonDeliverPRfromAMS);
            this.groupBoxPlayReady.Controls.Add(this.radioButtonExternalPRServer);
            this.groupBoxPlayReady.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxPlayReady.Location = new System.Drawing.Point(12, 91);
            this.groupBoxPlayReady.Name = "groupBoxPlayReady";
            this.groupBoxPlayReady.Size = new System.Drawing.Size(645, 265);
            this.groupBoxPlayReady.TabIndex = 52;
            this.groupBoxPlayReady.TabStop = false;
            this.groupBoxPlayReady.Text = "PlayReady";
            // 
            // panelExternalPlayReady
            // 
            this.panelExternalPlayReady.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelExternalPlayReady.Controls.Add(this.textBoxPRLAurl);
            this.panelExternalPlayReady.Controls.Add(this.buttonPlayReadyTestSettings);
            this.panelExternalPlayReady.Controls.Add(this.label7);
            this.panelExternalPlayReady.Controls.Add(this.checkBoxEncodingSL);
            this.panelExternalPlayReady.Controls.Add(this.label3);
            this.panelExternalPlayReady.Enabled = false;
            this.panelExternalPlayReady.Location = new System.Drawing.Point(41, 122);
            this.panelExternalPlayReady.Name = "panelExternalPlayReady";
            this.panelExternalPlayReady.Size = new System.Drawing.Size(597, 127);
            this.panelExternalPlayReady.TabIndex = 81;
            // 
            // textBoxPRLAurl
            // 
            this.textBoxPRLAurl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPRLAurl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPRLAurl.Location = new System.Drawing.Point(26, 33);
            this.textBoxPRLAurl.Name = "textBoxPRLAurl";
            this.textBoxPRLAurl.Size = new System.Drawing.Size(419, 23);
            this.textBoxPRLAurl.TabIndex = 78;
            // 
            // buttonPlayReadyTestSettings
            // 
            this.buttonPlayReadyTestSettings.Location = new System.Drawing.Point(26, 62);
            this.buttonPlayReadyTestSettings.Name = "buttonPlayReadyTestSettings";
            this.buttonPlayReadyTestSettings.Size = new System.Drawing.Size(185, 37);
            this.buttonPlayReadyTestSettings.TabIndex = 29;
            this.buttonPlayReadyTestSettings.Text = "Use PlayReady Test Server";
            this.buttonPlayReadyTestSettings.UseVisualStyleBackColor = true;
            this.buttonPlayReadyTestSettings.Click += new System.EventHandler(this.buttonPlayReadyTestSettings_Click);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label7.Location = new System.Drawing.Point(453, 36);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(30, 15);
            this.label7.TabIndex = 79;
            this.label7.Text = "(Url)";
            // 
            // checkBoxEncodingSL
            // 
            this.checkBoxEncodingSL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxEncodingSL.AutoSize = true;
            this.checkBoxEncodingSL.Enabled = false;
            this.checkBoxEncodingSL.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxEncodingSL.Location = new System.Drawing.Point(485, 37);
            this.checkBoxEncodingSL.Name = "checkBoxEncodingSL";
            this.checkBoxEncodingSL.Size = new System.Drawing.Size(109, 19);
            this.checkBoxEncodingSL.TabIndex = 80;
            this.checkBoxEncodingSL.Text = "Encoding for SL";
            this.checkBoxEncodingSL.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(23, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 15);
            this.label3.TabIndex = 77;
            this.label3.Text = "License Acquisition Url :";
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label8.Location = new System.Drawing.Point(64, 58);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(574, 42);
            this.label8.TabIndex = 69;
            this.label8.Text = "Having more than one option is useful if you want to support several types of tok" +
    "ens, or want to deliver various PlayReady licenses based on token claims";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(521, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 15);
            this.label2.TabIndex = 65;
            this.label2.Text = "option(s)";
            // 
            // numericUpDownNbOptionsPR
            // 
            this.numericUpDownNbOptionsPR.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownNbOptionsPR.Location = new System.Drawing.Point(461, 31);
            this.numericUpDownNbOptionsPR.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownNbOptionsPR.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownNbOptionsPR.Name = "numericUpDownNbOptionsPR";
            this.numericUpDownNbOptionsPR.Size = new System.Drawing.Size(54, 23);
            this.numericUpDownNbOptionsPR.TabIndex = 64;
            this.numericUpDownNbOptionsPR.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // radioButtonDeliverPRfromAMS
            // 
            this.radioButtonDeliverPRfromAMS.AutoSize = true;
            this.radioButtonDeliverPRfromAMS.Checked = true;
            this.radioButtonDeliverPRfromAMS.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonDeliverPRfromAMS.Location = new System.Drawing.Point(41, 31);
            this.radioButtonDeliverPRfromAMS.Name = "radioButtonDeliverPRfromAMS";
            this.radioButtonDeliverPRfromAMS.Size = new System.Drawing.Size(414, 19);
            this.radioButtonDeliverPRfromAMS.TabIndex = 63;
            this.radioButtonDeliverPRfromAMS.TabStop = true;
            this.radioButtonDeliverPRfromAMS.Text = "From Azure Media Services - Authorization policy for the content key with";
            this.radioButtonDeliverPRfromAMS.UseVisualStyleBackColor = true;
            // 
            // radioButtonExternalPRServer
            // 
            this.radioButtonExternalPRServer.AutoSize = true;
            this.radioButtonExternalPRServer.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonExternalPRServer.Location = new System.Drawing.Point(41, 97);
            this.radioButtonExternalPRServer.Name = "radioButtonExternalPRServer";
            this.radioButtonExternalPRServer.Size = new System.Drawing.Size(330, 19);
            this.radioButtonExternalPRServer.TabIndex = 62;
            this.radioButtonExternalPRServer.Text = "An external PlayReady server is used to deliver the licenses";
            this.radioButtonExternalPRServer.UseVisualStyleBackColor = true;
            this.radioButtonExternalPRServer.CheckedChanged += new System.EventHandler(this.radioButtonExternalPRServer_CheckedChanged);
            // 
            // groupBoxWidevine
            // 
            this.groupBoxWidevine.Controls.Add(this.label6);
            this.groupBoxWidevine.Controls.Add(this.numericUpDownNbOptionsWV);
            this.groupBoxWidevine.Controls.Add(this.radioButtonExternalWVServer);
            this.groupBoxWidevine.Controls.Add(this.radioButtonDeliverWVFromAMS);
            this.groupBoxWidevine.Controls.Add(this.label5);
            this.groupBoxWidevine.Controls.Add(this.label11);
            this.groupBoxWidevine.Controls.Add(this.LAURLWidevine);
            this.groupBoxWidevine.Controls.Add(this.label4);
            this.groupBoxWidevine.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxWidevine.Location = new System.Drawing.Point(12, 400);
            this.groupBoxWidevine.Name = "groupBoxWidevine";
            this.groupBoxWidevine.Size = new System.Drawing.Size(645, 203);
            this.groupBoxWidevine.TabIndex = 71;
            this.groupBoxWidevine.TabStop = false;
            this.groupBoxWidevine.Text = "Widevine";
            // 
            // radioButtonExternalWVServer
            // 
            this.radioButtonExternalWVServer.AutoSize = true;
            this.radioButtonExternalWVServer.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonExternalWVServer.Location = new System.Drawing.Point(58, 75);
            this.radioButtonExternalWVServer.Name = "radioButtonExternalWVServer";
            this.radioButtonExternalWVServer.Size = new System.Drawing.Size(325, 19);
            this.radioButtonExternalWVServer.TabIndex = 82;
            this.radioButtonExternalWVServer.Text = "An external Widevine server is used to deliver the licenses";
            this.radioButtonExternalWVServer.UseVisualStyleBackColor = true;
            // 
            // radioButtonDeliverWVFromAMS
            // 
            this.radioButtonDeliverWVFromAMS.AutoSize = true;
            this.radioButtonDeliverWVFromAMS.Checked = true;
            this.radioButtonDeliverWVFromAMS.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonDeliverWVFromAMS.Location = new System.Drawing.Point(58, 13);
            this.radioButtonDeliverWVFromAMS.Name = "radioButtonDeliverWVFromAMS";
            this.radioButtonDeliverWVFromAMS.Size = new System.Drawing.Size(414, 19);
            this.radioButtonDeliverWVFromAMS.TabIndex = 82;
            this.radioButtonDeliverWVFromAMS.TabStop = true;
            this.radioButtonDeliverWVFromAMS.Text = "From Azure Media Services - Authorization policy for the content key with";
            this.radioButtonDeliverWVFromAMS.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(64, 99);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(133, 15);
            this.label5.TabIndex = 81;
            this.label5.Text = "License Acquisition Url :";
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label11.Location = new System.Drawing.Point(494, 120);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(30, 15);
            this.label11.TabIndex = 86;
            this.label11.Text = "(Url)";
            // 
            // LAURLWidevine
            // 
            this.LAURLWidevine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LAURLWidevine.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.LAURLWidevine.Location = new System.Drawing.Point(67, 117);
            this.LAURLWidevine.Name = "LAURLWidevine";
            this.LAURLWidevine.Size = new System.Drawing.Size(419, 23);
            this.LAURLWidevine.TabIndex = 85;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label4.Location = new System.Drawing.Point(38, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(583, 23);
            this.label4.TabIndex = 70;
            this.label4.Text = "As of today, Widevine licenses must be delivered by a third party.";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(535, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 15);
            this.label6.TabIndex = 83;
            this.label6.Text = "option(s)";
            // 
            // numericUpDownNbOptionsWV
            // 
            this.numericUpDownNbOptionsWV.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownNbOptionsWV.Location = new System.Drawing.Point(475, 13);
            this.numericUpDownNbOptionsWV.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownNbOptionsWV.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownNbOptionsWV.Name = "numericUpDownNbOptionsWV";
            this.numericUpDownNbOptionsWV.Size = new System.Drawing.Size(54, 23);
            this.numericUpDownNbOptionsWV.TabIndex = 82;
            this.numericUpDownNbOptionsWV.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // AddDynamicEncryptionFrame3_CENCDelivery
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(681, 741);
            this.Controls.Add(this.groupBoxWidevine);
            this.Controls.Add(this.groupBoxPlayReady);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "AddDynamicEncryptionFrame3_CENCDelivery";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Dynamic Encryption - Step 3";
            this.Load += new System.EventHandler(this.SetupDynEnc_Load);
            this.panel1.ResumeLayout(false);
            this.groupBoxPlayReady.ResumeLayout(false);
            this.groupBoxPlayReady.PerformLayout();
            this.panelExternalPlayReady.ResumeLayout(false);
            this.panelExternalPlayReady.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNbOptionsPR)).EndInit();
            this.groupBoxWidevine.ResumeLayout(false);
            this.groupBoxWidevine.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNbOptionsWV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button buttonOk;
        public System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBoxPlayReady;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDownNbOptionsPR;
        private System.Windows.Forms.RadioButton radioButtonDeliverPRfromAMS;
        private System.Windows.Forms.RadioButton radioButtonExternalPRServer;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBoxWidevine;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxPRLAurl;
        private System.Windows.Forms.CheckBox checkBoxEncodingSL;
        public System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox LAURLWidevine;
        private System.Windows.Forms.Button buttonPlayReadyTestSettings;
        private System.Windows.Forms.Panel panelExternalPlayReady;
        private System.Windows.Forms.RadioButton radioButtonExternalWVServer;
        private System.Windows.Forms.RadioButton radioButtonDeliverWVFromAMS;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numericUpDownNbOptionsWV;
    }
}