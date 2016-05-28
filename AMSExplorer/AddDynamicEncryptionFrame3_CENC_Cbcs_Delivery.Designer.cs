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
            this.components = new System.ComponentModel.Container();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBoxFairPlay = new System.Windows.Forms.GroupBox();
            this.panelFairPlayFromAMS = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.radioButtonHttps = new System.Windows.Forms.RadioButton();
            this.radioButtonSkd = new System.Windows.Forms.RadioButton();
            this.labelCertificateFile = new System.Windows.Forms.Label();
            this.buttonImportPFX = new System.Windows.Forms.Button();
            this.panelExternalFairPlay = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.radioButtonIVBase64 = new System.Windows.Forms.RadioButton();
            this.radioButtonIVHex = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxIV = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxFairPlayLAurl = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.radioButtonDeliverFairPlayfromAMS = new System.Windows.Forms.RadioButton();
            this.radioButtonExternalFairPlayServer = new System.Windows.Forms.RadioButton();
            this.numericUpDownNbOptionsFairPlay = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.textBoxASK = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.radioButtonASKBase64 = new System.Windows.Forms.RadioButton();
            this.radioButtonASKHex = new System.Windows.Forms.RadioButton();
            this.label9 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.groupBoxFairPlay.SuspendLayout();
            this.panelFairPlayFromAMS.SuspendLayout();
            this.panelExternalFairPlay.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNbOptionsFairPlay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.panel2.SuspendLayout();
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
            // groupBoxFairPlay
            // 
            this.groupBoxFairPlay.Controls.Add(this.panelFairPlayFromAMS);
            this.groupBoxFairPlay.Controls.Add(this.panelExternalFairPlay);
            this.groupBoxFairPlay.Controls.Add(this.radioButtonDeliverFairPlayfromAMS);
            this.groupBoxFairPlay.Controls.Add(this.radioButtonExternalFairPlayServer);
            this.groupBoxFairPlay.Controls.Add(this.numericUpDownNbOptionsFairPlay);
            this.groupBoxFairPlay.Controls.Add(this.label2);
            this.groupBoxFairPlay.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxFairPlay.Location = new System.Drawing.Point(12, 87);
            this.groupBoxFairPlay.Name = "groupBoxFairPlay";
            this.groupBoxFairPlay.Size = new System.Drawing.Size(645, 562);
            this.groupBoxFairPlay.TabIndex = 52;
            this.groupBoxFairPlay.TabStop = false;
            this.groupBoxFairPlay.Text = "Apple FairPlay Streaming";
            // 
            // panelFairPlayFromAMS
            // 
            this.panelFairPlayFromAMS.Controls.Add(this.textBoxASK);
            this.panelFairPlayFromAMS.Controls.Add(this.panel2);
            this.panelFairPlayFromAMS.Controls.Add(this.label9);
            this.panelFairPlayFromAMS.Controls.Add(this.label6);
            this.panelFairPlayFromAMS.Controls.Add(this.radioButtonHttps);
            this.panelFairPlayFromAMS.Controls.Add(this.radioButtonSkd);
            this.panelFairPlayFromAMS.Controls.Add(this.labelCertificateFile);
            this.panelFairPlayFromAMS.Controls.Add(this.buttonImportPFX);
            this.panelFairPlayFromAMS.Location = new System.Drawing.Point(41, 63);
            this.panelFairPlayFromAMS.Name = "panelFairPlayFromAMS";
            this.panelFairPlayFromAMS.Size = new System.Drawing.Size(597, 230);
            this.panelFairPlayFromAMS.TabIndex = 85;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(26, 148);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(177, 15);
            this.label6.TabIndex = 78;
            this.label6.Text = "License Acquisition Url scheme :";
            // 
            // radioButtonHttps
            // 
            this.radioButtonHttps.AutoSize = true;
            this.radioButtonHttps.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonHttps.Location = new System.Drawing.Point(29, 191);
            this.radioButtonHttps.Name = "radioButtonHttps";
            this.radioButtonHttps.Size = new System.Drawing.Size(65, 19);
            this.radioButtonHttps.TabIndex = 73;
            this.radioButtonHttps.Text = "https://";
            this.radioButtonHttps.UseVisualStyleBackColor = true;
            // 
            // radioButtonSkd
            // 
            this.radioButtonSkd.AutoSize = true;
            this.radioButtonSkd.Checked = true;
            this.radioButtonSkd.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonSkd.Location = new System.Drawing.Point(29, 166);
            this.radioButtonSkd.Name = "radioButtonSkd";
            this.radioButtonSkd.Size = new System.Drawing.Size(56, 19);
            this.radioButtonSkd.TabIndex = 72;
            this.radioButtonSkd.TabStop = true;
            this.radioButtonSkd.Text = "skd://";
            this.radioButtonSkd.UseVisualStyleBackColor = true;
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
            this.panelExternalFairPlay.Controls.Add(this.panel3);
            this.panelExternalFairPlay.Controls.Add(this.label4);
            this.panelExternalFairPlay.Controls.Add(this.textBoxIV);
            this.panelExternalFairPlay.Controls.Add(this.label5);
            this.panelExternalFairPlay.Controls.Add(this.textBoxFairPlayLAurl);
            this.panelExternalFairPlay.Controls.Add(this.label7);
            this.panelExternalFairPlay.Controls.Add(this.label3);
            this.panelExternalFairPlay.Enabled = false;
            this.panelExternalFairPlay.Location = new System.Drawing.Point(41, 342);
            this.panelExternalFairPlay.Name = "panelExternalFairPlay";
            this.panelExternalFairPlay.Size = new System.Drawing.Size(597, 171);
            this.panelExternalFairPlay.TabIndex = 81;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.radioButtonIVBase64);
            this.panel3.Controls.Add(this.radioButtonIVHex);
            this.panel3.Location = new System.Drawing.Point(466, 88);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(87, 55);
            this.panel3.TabIndex = 92;
            // 
            // radioButtonIVBase64
            // 
            this.radioButtonIVBase64.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonIVBase64.AutoSize = true;
            this.radioButtonIVBase64.Checked = true;
            this.radioButtonIVBase64.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.radioButtonIVBase64.Location = new System.Drawing.Point(9, 7);
            this.radioButtonIVBase64.Name = "radioButtonIVBase64";
            this.radioButtonIVBase64.Size = new System.Drawing.Size(61, 19);
            this.radioButtonIVBase64.TabIndex = 68;
            this.radioButtonIVBase64.TabStop = true;
            this.radioButtonIVBase64.Text = "Base64";
            this.radioButtonIVBase64.UseVisualStyleBackColor = true;
            this.radioButtonIVBase64.CheckedChanged += new System.EventHandler(this.radioButtonIVBase64_CheckedChanged);
            // 
            // radioButtonIVHex
            // 
            this.radioButtonIVHex.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonIVHex.AutoSize = true;
            this.radioButtonIVHex.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.radioButtonIVHex.Location = new System.Drawing.Point(9, 32);
            this.radioButtonIVHex.Name = "radioButtonIVHex";
            this.radioButtonIVHex.Size = new System.Drawing.Size(45, 19);
            this.radioButtonIVHex.TabIndex = 69;
            this.radioButtonIVHex.Text = "Hex";
            this.radioButtonIVHex.UseVisualStyleBackColor = true;
            this.radioButtonIVHex.CheckedChanged += new System.EventHandler(this.radioButtonIVHex_CheckedChanged);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label4.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label4.Location = new System.Drawing.Point(145, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(267, 15);
            this.label4.TabIndex = 95;
            this.label4.Text = "If empty, it will be automatically generated";
            // 
            // textBoxIV
            // 
            this.textBoxIV.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxIV.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBoxIV.Location = new System.Drawing.Point(29, 106);
            this.textBoxIV.Name = "textBoxIV";
            this.textBoxIV.Size = new System.Drawing.Size(416, 23);
            this.textBoxIV.TabIndex = 94;
            this.textBoxIV.TextChanged += new System.EventHandler(this.textBoxIV_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label5.Location = new System.Drawing.Point(26, 88);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(113, 15);
            this.label5.TabIndex = 93;
            this.label5.Text = "Initialization Vector :";
            // 
            // textBoxFairPlayLAurl
            // 
            this.textBoxFairPlayLAurl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFairPlayLAurl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxFairPlayLAurl.Location = new System.Drawing.Point(29, 33);
            this.textBoxFairPlayLAurl.Name = "textBoxFairPlayLAurl";
            this.textBoxFairPlayLAurl.Size = new System.Drawing.Size(416, 23);
            this.textBoxFairPlayLAurl.TabIndex = 78;
            this.textBoxFairPlayLAurl.TextChanged += new System.EventHandler(this.textBoxFairPlayLAurl_TextChanged);
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
            this.label3.Location = new System.Drawing.Point(26, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 15);
            this.label3.TabIndex = 77;
            this.label3.Text = "License Acquisition Url :";
            // 
            // radioButtonDeliverPRfromAMS
            // 
            this.radioButtonDeliverFairPlayfromAMS.AutoSize = true;
            this.radioButtonDeliverFairPlayfromAMS.Checked = true;
            this.radioButtonDeliverFairPlayfromAMS.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonDeliverFairPlayfromAMS.Location = new System.Drawing.Point(41, 34);
            this.radioButtonDeliverFairPlayfromAMS.Name = "radioButtonDeliverPRfromAMS";
            this.radioButtonDeliverFairPlayfromAMS.Size = new System.Drawing.Size(193, 19);
            this.radioButtonDeliverFairPlayfromAMS.TabIndex = 63;
            this.radioButtonDeliverFairPlayfromAMS.TabStop = true;
            this.radioButtonDeliverFairPlayfromAMS.Text = "From Azure Media Services with";
            this.radioButtonDeliverFairPlayfromAMS.UseVisualStyleBackColor = true;
            // 
            // radioButtonExternalFairPlayServer
            // 
            this.radioButtonExternalFairPlayServer.AutoSize = true;
            this.radioButtonExternalFairPlayServer.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonExternalFairPlayServer.Location = new System.Drawing.Point(41, 320);
            this.radioButtonExternalFairPlayServer.Name = "radioButtonExternalFairPlayServer";
            this.radioButtonExternalFairPlayServer.Size = new System.Drawing.Size(201, 19);
            this.radioButtonExternalFairPlayServer.TabIndex = 62;
            this.radioButtonExternalFairPlayServer.Text = "An external FairPlay server is used";
            this.radioButtonExternalFairPlayServer.UseVisualStyleBackColor = true;
            this.radioButtonExternalFairPlayServer.CheckedChanged += new System.EventHandler(this.radioButtonExternalPRServer_CheckedChanged);
            // 
            // numericUpDownNbOptionsPlayReady
            // 
            this.numericUpDownNbOptionsFairPlay.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownNbOptionsFairPlay.Location = new System.Drawing.Point(240, 34);
            this.numericUpDownNbOptionsFairPlay.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownNbOptionsFairPlay.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownNbOptionsFairPlay.Name = "numericUpDownNbOptionsPlayReady";
            this.numericUpDownNbOptionsFairPlay.Size = new System.Drawing.Size(54, 23);
            this.numericUpDownNbOptionsFairPlay.TabIndex = 64;
            this.numericUpDownNbOptionsFairPlay.Value = new decimal(new int[] {
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
            this.label8.Location = new System.Drawing.Point(50, 630);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(574, 42);
            this.label8.TabIndex = 69;
            this.label8.Text = "Having more than one option is useful if you want to support several types of tok" +
    "ens, or want to deliver various FairPlay licenses based on token claims.";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // textBoxASK
            // 
            this.textBoxASK.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxASK.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxASK.Location = new System.Drawing.Point(29, 93);
            this.textBoxASK.Name = "textBoxASK";
            this.textBoxASK.Size = new System.Drawing.Size(416, 23);
            this.textBoxASK.TabIndex = 89;
            this.textBoxASK.TextChanged += new System.EventHandler(this.textBoxASK_TextChanged);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.radioButtonASKBase64);
            this.panel2.Controls.Add(this.radioButtonASKHex);
            this.panel2.Location = new System.Drawing.Point(466, 75);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(87, 55);
            this.panel2.TabIndex = 90;
            // 
            // radioButtonASKBase64
            // 
            this.radioButtonASKBase64.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonASKBase64.AutoSize = true;
            this.radioButtonASKBase64.Checked = true;
            this.radioButtonASKBase64.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.radioButtonASKBase64.Location = new System.Drawing.Point(9, 7);
            this.radioButtonASKBase64.Name = "radioButtonASKBase64";
            this.radioButtonASKBase64.Size = new System.Drawing.Size(61, 19);
            this.radioButtonASKBase64.TabIndex = 68;
            this.radioButtonASKBase64.TabStop = true;
            this.radioButtonASKBase64.Text = "Base64";
            this.radioButtonASKBase64.UseVisualStyleBackColor = true;
            this.radioButtonASKBase64.CheckedChanged += new System.EventHandler(this.radioButtonASKBase64_CheckedChanged);
            // 
            // radioButtonASKHex
            // 
            this.radioButtonASKHex.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonASKHex.AutoSize = true;
            this.radioButtonASKHex.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.radioButtonASKHex.Location = new System.Drawing.Point(9, 32);
            this.radioButtonASKHex.Name = "radioButtonASKHex";
            this.radioButtonASKHex.Size = new System.Drawing.Size(45, 19);
            this.radioButtonASKHex.TabIndex = 69;
            this.radioButtonASKHex.Text = "Hex";
            this.radioButtonASKHex.UseVisualStyleBackColor = true;
            this.radioButtonASKHex.CheckedChanged += new System.EventHandler(this.radioButtonASKHex_CheckedChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(26, 75);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(163, 15);
            this.label9.TabIndex = 88;
            this.label9.Text = "Application Secret Key (ASK) :";
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
            this.Controls.Add(this.groupBoxFairPlay);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "AddDynamicEncryptionFrame3_CENC_Cbcs_Delivery";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Dynamic Encryption - Step 3";
            this.Load += new System.EventHandler(this.AddDynamicEncryptionFrame3_Load);
            this.panel1.ResumeLayout(false);
            this.groupBoxFairPlay.ResumeLayout(false);
            this.groupBoxFairPlay.PerformLayout();
            this.panelFairPlayFromAMS.ResumeLayout(false);
            this.panelFairPlayFromAMS.PerformLayout();
            this.panelExternalFairPlay.ResumeLayout(false);
            this.panelExternalFairPlay.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNbOptionsFairPlay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button buttonOk;
        public System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBoxFairPlay;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDownNbOptionsFairPlay;
        private System.Windows.Forms.RadioButton radioButtonDeliverFairPlayfromAMS;
        private System.Windows.Forms.RadioButton radioButtonExternalFairPlayServer;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxFairPlayLAurl;
        public System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panelExternalFairPlay;
        private System.Windows.Forms.Button buttonImportPFX;
        private System.Windows.Forms.Label labelCertificateFile;
        private System.Windows.Forms.Panel panelFairPlayFromAMS;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton radioButtonIVBase64;
        private System.Windows.Forms.RadioButton radioButtonIVHex;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxIV;
        public System.Windows.Forms.Label label5;
        public System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton radioButtonHttps;
        private System.Windows.Forms.RadioButton radioButtonSkd;
        private System.Windows.Forms.TextBox textBoxASK;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton radioButtonASKBase64;
        private System.Windows.Forms.RadioButton radioButtonASKHex;
        public System.Windows.Forms.Label label9;
    }
}