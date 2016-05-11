namespace AMSExplorer
{
    partial class AddDynamicEncryptionFrame3_CENC_Cbcs_Delivery
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
            this.panelFairPlayFromAMS = new System.Windows.Forms.Panel();
            this.labelCertificateFile = new System.Windows.Forms.Label();
            this.buttonImportPFX = new System.Windows.Forms.Button();
            this.panelExternalFairPlay = new System.Windows.Forms.Panel();
            this.textBoxFairPlayLAurl = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.radioButtonDeliverPRfromAMS = new System.Windows.Forms.RadioButton();
            this.radioButtonExternalPRServer = new System.Windows.Forms.RadioButton();
            this.numericUpDownNbOptionsPlayReady = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.groupBoxPlayReady.SuspendLayout();
            this.panelFairPlayFromAMS.SuspendLayout();
            this.panelExternalFairPlay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNbOptionsPlayReady)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Enabled = false;
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
            this.groupBoxPlayReady.Controls.Add(this.panelFairPlayFromAMS);
            this.groupBoxPlayReady.Controls.Add(this.panelExternalFairPlay);
            this.groupBoxPlayReady.Controls.Add(this.radioButtonDeliverPRfromAMS);
            this.groupBoxPlayReady.Controls.Add(this.radioButtonExternalPRServer);
            this.groupBoxPlayReady.Controls.Add(this.numericUpDownNbOptionsPlayReady);
            this.groupBoxPlayReady.Controls.Add(this.label2);
            this.groupBoxPlayReady.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxPlayReady.Location = new System.Drawing.Point(12, 87);
            this.groupBoxPlayReady.Name = "groupBoxPlayReady";
            this.groupBoxPlayReady.Size = new System.Drawing.Size(645, 275);
            this.groupBoxPlayReady.TabIndex = 52;
            this.groupBoxPlayReady.TabStop = false;
            this.groupBoxPlayReady.Text = "Apple FairPlay Streaming";
            // 
            // panelFairPlayFromAMS
            // 
            this.panelFairPlayFromAMS.Controls.Add(this.labelCertificateFile);
            this.panelFairPlayFromAMS.Controls.Add(this.buttonImportPFX);
            this.panelFairPlayFromAMS.Location = new System.Drawing.Point(41, 63);
            this.panelFairPlayFromAMS.Name = "panelFairPlayFromAMS";
            this.panelFairPlayFromAMS.Size = new System.Drawing.Size(597, 59);
            this.panelFairPlayFromAMS.TabIndex = 85;
            // 
            // labelCertificateFile
            // 
            this.labelCertificateFile.Location = new System.Drawing.Point(259, 23);
            this.labelCertificateFile.Name = "labelCertificateFile";
            this.labelCertificateFile.Size = new System.Drawing.Size(335, 21);
            this.labelCertificateFile.TabIndex = 71;
            this.labelCertificateFile.Text = "(no file selected)";
            // 
            // buttonImportPFX
            // 
            this.buttonImportPFX.Location = new System.Drawing.Point(26, 17);
            this.buttonImportPFX.Name = "buttonImportPFX";
            this.buttonImportPFX.Size = new System.Drawing.Size(227, 27);
            this.buttonImportPFX.TabIndex = 70;
            this.buttonImportPFX.Text = "Import App Certificate (.PFX)...";
            this.buttonImportPFX.UseVisualStyleBackColor = true;
            this.buttonImportPFX.Click += new System.EventHandler(this.buttonImportPFX_Click);
            // 
            // panelExternalFairPlay
            // 
            this.panelExternalFairPlay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelExternalFairPlay.Controls.Add(this.textBoxFairPlayLAurl);
            this.panelExternalFairPlay.Controls.Add(this.label7);
            this.panelExternalFairPlay.Controls.Add(this.label3);
            this.panelExternalFairPlay.Enabled = false;
            this.panelExternalFairPlay.Location = new System.Drawing.Point(41, 179);
            this.panelExternalFairPlay.Name = "panelExternalFairPlay";
            this.panelExternalFairPlay.Size = new System.Drawing.Size(597, 74);
            this.panelExternalFairPlay.TabIndex = 81;
            // 
            // textBoxFairPlayLAurl
            // 
            this.textBoxFairPlayLAurl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFairPlayLAurl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxFairPlayLAurl.Location = new System.Drawing.Point(26, 33);
            this.textBoxFairPlayLAurl.Name = "textBoxFairPlayLAurl";
            this.textBoxFairPlayLAurl.Size = new System.Drawing.Size(419, 23);
            this.textBoxFairPlayLAurl.TabIndex = 78;
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
            // radioButtonDeliverPRfromAMS
            // 
            this.radioButtonDeliverPRfromAMS.AutoSize = true;
            this.radioButtonDeliverPRfromAMS.Checked = true;
            this.radioButtonDeliverPRfromAMS.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonDeliverPRfromAMS.Location = new System.Drawing.Point(41, 34);
            this.radioButtonDeliverPRfromAMS.Name = "radioButtonDeliverPRfromAMS";
            this.radioButtonDeliverPRfromAMS.Size = new System.Drawing.Size(193, 19);
            this.radioButtonDeliverPRfromAMS.TabIndex = 63;
            this.radioButtonDeliverPRfromAMS.TabStop = true;
            this.radioButtonDeliverPRfromAMS.Text = "From Azure Media Services with";
            this.radioButtonDeliverPRfromAMS.UseVisualStyleBackColor = true;
            // 
            // radioButtonExternalPRServer
            // 
            this.radioButtonExternalPRServer.AutoSize = true;
            this.radioButtonExternalPRServer.Enabled = false;
            this.radioButtonExternalPRServer.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonExternalPRServer.Location = new System.Drawing.Point(41, 157);
            this.radioButtonExternalPRServer.Name = "radioButtonExternalPRServer";
            this.radioButtonExternalPRServer.Size = new System.Drawing.Size(201, 19);
            this.radioButtonExternalPRServer.TabIndex = 62;
            this.radioButtonExternalPRServer.Text = "An external FairPlay server is used";
            this.radioButtonExternalPRServer.UseVisualStyleBackColor = true;
            this.radioButtonExternalPRServer.CheckedChanged += new System.EventHandler(this.radioButtonExternalPRServer_CheckedChanged);
            // 
            // numericUpDownNbOptionsPlayReady
            // 
            this.numericUpDownNbOptionsPlayReady.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownNbOptionsPlayReady.Location = new System.Drawing.Point(240, 34);
            this.numericUpDownNbOptionsPlayReady.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownNbOptionsPlayReady.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownNbOptionsPlayReady.Name = "numericUpDownNbOptionsPlayReady";
            this.numericUpDownNbOptionsPlayReady.Size = new System.Drawing.Size(54, 23);
            this.numericUpDownNbOptionsPlayReady.TabIndex = 64;
            this.numericUpDownNbOptionsPlayReady.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(300, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 15);
            this.label2.TabIndex = 65;
            this.label2.Text = "option(s)";
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label8.Location = new System.Drawing.Point(50, 566);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(574, 42);
            this.label8.TabIndex = 69;
            this.label8.Text = "Having more than one option is useful if you want to support several types of tok" +
    "ens, or want to deliver various FairPlay licenses based on token claims.";
            // 
            // AddDynamicEncryptionFrame3_CENC_Cbcs_Delivery
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(681, 741);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.groupBoxPlayReady);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "AddDynamicEncryptionFrame3_CENC_Cbcs_Delivery";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Dynamic Encryption - Step 3";
            this.Load += new System.EventHandler(this.AddDynamicEncryptionFrame3_Load);
            this.panel1.ResumeLayout(false);
            this.groupBoxPlayReady.ResumeLayout(false);
            this.groupBoxPlayReady.PerformLayout();
            this.panelFairPlayFromAMS.ResumeLayout(false);
            this.panelExternalFairPlay.ResumeLayout(false);
            this.panelExternalFairPlay.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNbOptionsPlayReady)).EndInit();
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
        private System.Windows.Forms.NumericUpDown numericUpDownNbOptionsPlayReady;
        private System.Windows.Forms.RadioButton radioButtonDeliverPRfromAMS;
        private System.Windows.Forms.RadioButton radioButtonExternalPRServer;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxFairPlayLAurl;
        public System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panelExternalFairPlay;
        private System.Windows.Forms.Button buttonImportPFX;
        private System.Windows.Forms.Label labelCertificateFile;
        private System.Windows.Forms.Panel panelFairPlayFromAMS;
    }
}