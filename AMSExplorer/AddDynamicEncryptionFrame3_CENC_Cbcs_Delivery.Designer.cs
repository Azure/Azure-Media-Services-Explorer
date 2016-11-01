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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddDynamicEncryptionFrame3_CENC_Cbcs_Delivery));
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBoxFairPlay = new System.Windows.Forms.GroupBox();
            this.moreinfoFairPlaylink = new System.Windows.Forms.LinkLabel();
            this.panelFairPlayFromAMS = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.TextBoxCertificateFile = new System.Windows.Forms.TextBox();
            this.textBoxASK = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.radioButtonASKBase64 = new System.Windows.Forms.RadioButton();
            this.radioButtonASKHex = new System.Windows.Forms.RadioButton();
            this.label9 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.radioButtonHttps = new System.Windows.Forms.RadioButton();
            this.radioButtonSkd = new System.Windows.Forms.RadioButton();
            this.buttonImportPFX = new System.Windows.Forms.Button();
            this.panelExternalFairPlay = new System.Windows.Forms.Panel();
            this.checkBoxFinalExtURL = new System.Windows.Forms.CheckBox();
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
            this.panel1.SuspendLayout();
            this.groupBoxFairPlay.SuspendLayout();
            this.panelFairPlayFromAMS.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panelExternalFairPlay.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNbOptionsFairPlay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            resources.ApplyResources(this.buttonOk, "buttonOk");
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.errorProvider1.SetError(this.buttonOk, resources.GetString("buttonOk.Error"));
            this.errorProvider1.SetIconAlignment(this.buttonOk, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("buttonOk.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.buttonOk, ((int)(resources.GetObject("buttonOk.IconPadding"))));
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.errorProvider1.SetError(this.buttonCancel, resources.GetString("buttonCancel.Error"));
            this.errorProvider1.SetIconAlignment(this.buttonCancel, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("buttonCancel.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.buttonCancel, ((int)(resources.GetObject("buttonCancel.IconPadding"))));
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.errorProvider1.SetError(this.label1, resources.GetString("label1.Error"));
            this.label1.ForeColor = System.Drawing.Color.DarkBlue;
            this.errorProvider1.SetIconAlignment(this.label1, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label1.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label1, ((int)(resources.GetObject("label1.IconPadding"))));
            this.label1.Name = "label1";
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonOk);
            this.panel1.Controls.Add(this.buttonCancel);
            this.errorProvider1.SetError(this.panel1, resources.GetString("panel1.Error"));
            this.errorProvider1.SetIconAlignment(this.panel1, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("panel1.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.panel1, ((int)(resources.GetObject("panel1.IconPadding"))));
            this.panel1.Name = "panel1";
            // 
            // groupBoxFairPlay
            // 
            resources.ApplyResources(this.groupBoxFairPlay, "groupBoxFairPlay");
            this.groupBoxFairPlay.Controls.Add(this.moreinfoFairPlaylink);
            this.groupBoxFairPlay.Controls.Add(this.panelFairPlayFromAMS);
            this.groupBoxFairPlay.Controls.Add(this.panelExternalFairPlay);
            this.groupBoxFairPlay.Controls.Add(this.radioButtonDeliverFairPlayfromAMS);
            this.groupBoxFairPlay.Controls.Add(this.radioButtonExternalFairPlayServer);
            this.groupBoxFairPlay.Controls.Add(this.numericUpDownNbOptionsFairPlay);
            this.groupBoxFairPlay.Controls.Add(this.label2);
            this.errorProvider1.SetError(this.groupBoxFairPlay, resources.GetString("groupBoxFairPlay.Error"));
            this.errorProvider1.SetIconAlignment(this.groupBoxFairPlay, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("groupBoxFairPlay.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.groupBoxFairPlay, ((int)(resources.GetObject("groupBoxFairPlay.IconPadding"))));
            this.groupBoxFairPlay.Name = "groupBoxFairPlay";
            this.groupBoxFairPlay.TabStop = false;
            // 
            // moreinfoFairPlaylink
            // 
            resources.ApplyResources(this.moreinfoFairPlaylink, "moreinfoFairPlaylink");
            this.errorProvider1.SetError(this.moreinfoFairPlaylink, resources.GetString("moreinfoFairPlaylink.Error"));
            this.errorProvider1.SetIconAlignment(this.moreinfoFairPlaylink, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("moreinfoFairPlaylink.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.moreinfoFairPlaylink, ((int)(resources.GetObject("moreinfoFairPlaylink.IconPadding"))));
            this.moreinfoFairPlaylink.Name = "moreinfoFairPlaylink";
            this.moreinfoFairPlaylink.TabStop = true;
            this.moreinfoFairPlaylink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.moreinfoFairPlaylink_LinkClicked);
            // 
            // panelFairPlayFromAMS
            // 
            resources.ApplyResources(this.panelFairPlayFromAMS, "panelFairPlayFromAMS");
            this.panelFairPlayFromAMS.Controls.Add(this.label10);
            this.panelFairPlayFromAMS.Controls.Add(this.TextBoxCertificateFile);
            this.panelFairPlayFromAMS.Controls.Add(this.textBoxASK);
            this.panelFairPlayFromAMS.Controls.Add(this.panel2);
            this.panelFairPlayFromAMS.Controls.Add(this.label9);
            this.panelFairPlayFromAMS.Controls.Add(this.label6);
            this.panelFairPlayFromAMS.Controls.Add(this.radioButtonHttps);
            this.panelFairPlayFromAMS.Controls.Add(this.radioButtonSkd);
            this.panelFairPlayFromAMS.Controls.Add(this.buttonImportPFX);
            this.errorProvider1.SetError(this.panelFairPlayFromAMS, resources.GetString("panelFairPlayFromAMS.Error"));
            this.errorProvider1.SetIconAlignment(this.panelFairPlayFromAMS, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("panelFairPlayFromAMS.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.panelFairPlayFromAMS, ((int)(resources.GetObject("panelFairPlayFromAMS.IconPadding"))));
            this.panelFairPlayFromAMS.Name = "panelFairPlayFromAMS";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.errorProvider1.SetError(this.label10, resources.GetString("label10.Error"));
            this.label10.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.errorProvider1.SetIconAlignment(this.label10, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label10.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label10, ((int)(resources.GetObject("label10.IconPadding"))));
            this.label10.Name = "label10";
            // 
            // TextBoxCertificateFile
            // 
            resources.ApplyResources(this.TextBoxCertificateFile, "TextBoxCertificateFile");
            this.errorProvider1.SetError(this.TextBoxCertificateFile, resources.GetString("TextBoxCertificateFile.Error"));
            this.errorProvider1.SetIconAlignment(this.TextBoxCertificateFile, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("TextBoxCertificateFile.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.TextBoxCertificateFile, ((int)(resources.GetObject("TextBoxCertificateFile.IconPadding"))));
            this.TextBoxCertificateFile.Name = "TextBoxCertificateFile";
            this.TextBoxCertificateFile.ReadOnly = true;
            // 
            // textBoxASK
            // 
            resources.ApplyResources(this.textBoxASK, "textBoxASK");
            this.errorProvider1.SetError(this.textBoxASK, resources.GetString("textBoxASK.Error"));
            this.errorProvider1.SetIconAlignment(this.textBoxASK, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("textBoxASK.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.textBoxASK, ((int)(resources.GetObject("textBoxASK.IconPadding"))));
            this.textBoxASK.Name = "textBoxASK";
            this.textBoxASK.TextChanged += new System.EventHandler(this.textBoxASK_TextChanged);
            // 
            // panel2
            // 
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Controls.Add(this.radioButtonASKBase64);
            this.panel2.Controls.Add(this.radioButtonASKHex);
            this.errorProvider1.SetError(this.panel2, resources.GetString("panel2.Error"));
            this.errorProvider1.SetIconAlignment(this.panel2, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("panel2.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.panel2, ((int)(resources.GetObject("panel2.IconPadding"))));
            this.panel2.Name = "panel2";
            // 
            // radioButtonASKBase64
            // 
            resources.ApplyResources(this.radioButtonASKBase64, "radioButtonASKBase64");
            this.errorProvider1.SetError(this.radioButtonASKBase64, resources.GetString("radioButtonASKBase64.Error"));
            this.errorProvider1.SetIconAlignment(this.radioButtonASKBase64, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("radioButtonASKBase64.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.radioButtonASKBase64, ((int)(resources.GetObject("radioButtonASKBase64.IconPadding"))));
            this.radioButtonASKBase64.Name = "radioButtonASKBase64";
            this.radioButtonASKBase64.UseVisualStyleBackColor = true;
            this.radioButtonASKBase64.CheckedChanged += new System.EventHandler(this.radioButtonASKBase64_CheckedChanged);
            // 
            // radioButtonASKHex
            // 
            resources.ApplyResources(this.radioButtonASKHex, "radioButtonASKHex");
            this.radioButtonASKHex.Checked = true;
            this.errorProvider1.SetError(this.radioButtonASKHex, resources.GetString("radioButtonASKHex.Error"));
            this.errorProvider1.SetIconAlignment(this.radioButtonASKHex, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("radioButtonASKHex.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.radioButtonASKHex, ((int)(resources.GetObject("radioButtonASKHex.IconPadding"))));
            this.radioButtonASKHex.Name = "radioButtonASKHex";
            this.radioButtonASKHex.TabStop = true;
            this.radioButtonASKHex.UseVisualStyleBackColor = true;
            this.radioButtonASKHex.CheckedChanged += new System.EventHandler(this.radioButtonASKHex_CheckedChanged);
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.errorProvider1.SetError(this.label9, resources.GetString("label9.Error"));
            this.errorProvider1.SetIconAlignment(this.label9, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label9.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label9, ((int)(resources.GetObject("label9.IconPadding"))));
            this.label9.Name = "label9";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.errorProvider1.SetError(this.label6, resources.GetString("label6.Error"));
            this.errorProvider1.SetIconAlignment(this.label6, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label6.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label6, ((int)(resources.GetObject("label6.IconPadding"))));
            this.label6.Name = "label6";
            // 
            // radioButtonHttps
            // 
            resources.ApplyResources(this.radioButtonHttps, "radioButtonHttps");
            this.errorProvider1.SetError(this.radioButtonHttps, resources.GetString("radioButtonHttps.Error"));
            this.errorProvider1.SetIconAlignment(this.radioButtonHttps, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("radioButtonHttps.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.radioButtonHttps, ((int)(resources.GetObject("radioButtonHttps.IconPadding"))));
            this.radioButtonHttps.Name = "radioButtonHttps";
            this.radioButtonHttps.UseVisualStyleBackColor = true;
            // 
            // radioButtonSkd
            // 
            resources.ApplyResources(this.radioButtonSkd, "radioButtonSkd");
            this.radioButtonSkd.Checked = true;
            this.errorProvider1.SetError(this.radioButtonSkd, resources.GetString("radioButtonSkd.Error"));
            this.errorProvider1.SetIconAlignment(this.radioButtonSkd, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("radioButtonSkd.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.radioButtonSkd, ((int)(resources.GetObject("radioButtonSkd.IconPadding"))));
            this.radioButtonSkd.Name = "radioButtonSkd";
            this.radioButtonSkd.TabStop = true;
            this.radioButtonSkd.UseVisualStyleBackColor = true;
            // 
            // buttonImportPFX
            // 
            resources.ApplyResources(this.buttonImportPFX, "buttonImportPFX");
            this.errorProvider1.SetError(this.buttonImportPFX, resources.GetString("buttonImportPFX.Error"));
            this.errorProvider1.SetIconAlignment(this.buttonImportPFX, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("buttonImportPFX.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.buttonImportPFX, ((int)(resources.GetObject("buttonImportPFX.IconPadding"))));
            this.buttonImportPFX.Name = "buttonImportPFX";
            this.buttonImportPFX.UseVisualStyleBackColor = true;
            this.buttonImportPFX.Click += new System.EventHandler(this.buttonImportPFX_Click);
            // 
            // panelExternalFairPlay
            // 
            resources.ApplyResources(this.panelExternalFairPlay, "panelExternalFairPlay");
            this.panelExternalFairPlay.Controls.Add(this.checkBoxFinalExtURL);
            this.panelExternalFairPlay.Controls.Add(this.panel3);
            this.panelExternalFairPlay.Controls.Add(this.label4);
            this.panelExternalFairPlay.Controls.Add(this.textBoxIV);
            this.panelExternalFairPlay.Controls.Add(this.label5);
            this.panelExternalFairPlay.Controls.Add(this.textBoxFairPlayLAurl);
            this.panelExternalFairPlay.Controls.Add(this.label7);
            this.panelExternalFairPlay.Controls.Add(this.label3);
            this.errorProvider1.SetError(this.panelExternalFairPlay, resources.GetString("panelExternalFairPlay.Error"));
            this.errorProvider1.SetIconAlignment(this.panelExternalFairPlay, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("panelExternalFairPlay.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.panelExternalFairPlay, ((int)(resources.GetObject("panelExternalFairPlay.IconPadding"))));
            this.panelExternalFairPlay.Name = "panelExternalFairPlay";
            // 
            // checkBoxFinalExtURL
            // 
            resources.ApplyResources(this.checkBoxFinalExtURL, "checkBoxFinalExtURL");
            this.errorProvider1.SetError(this.checkBoxFinalExtURL, resources.GetString("checkBoxFinalExtURL.Error"));
            this.errorProvider1.SetIconAlignment(this.checkBoxFinalExtURL, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("checkBoxFinalExtURL.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.checkBoxFinalExtURL, ((int)(resources.GetObject("checkBoxFinalExtURL.IconPadding"))));
            this.checkBoxFinalExtURL.Name = "checkBoxFinalExtURL";
            this.checkBoxFinalExtURL.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Controls.Add(this.radioButtonIVBase64);
            this.panel3.Controls.Add(this.radioButtonIVHex);
            this.errorProvider1.SetError(this.panel3, resources.GetString("panel3.Error"));
            this.errorProvider1.SetIconAlignment(this.panel3, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("panel3.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.panel3, ((int)(resources.GetObject("panel3.IconPadding"))));
            this.panel3.Name = "panel3";
            // 
            // radioButtonIVBase64
            // 
            resources.ApplyResources(this.radioButtonIVBase64, "radioButtonIVBase64");
            this.radioButtonIVBase64.Checked = true;
            this.errorProvider1.SetError(this.radioButtonIVBase64, resources.GetString("radioButtonIVBase64.Error"));
            this.errorProvider1.SetIconAlignment(this.radioButtonIVBase64, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("radioButtonIVBase64.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.radioButtonIVBase64, ((int)(resources.GetObject("radioButtonIVBase64.IconPadding"))));
            this.radioButtonIVBase64.Name = "radioButtonIVBase64";
            this.radioButtonIVBase64.TabStop = true;
            this.radioButtonIVBase64.UseVisualStyleBackColor = true;
            this.radioButtonIVBase64.CheckedChanged += new System.EventHandler(this.radioButtonIVBase64_CheckedChanged);
            // 
            // radioButtonIVHex
            // 
            resources.ApplyResources(this.radioButtonIVHex, "radioButtonIVHex");
            this.errorProvider1.SetError(this.radioButtonIVHex, resources.GetString("radioButtonIVHex.Error"));
            this.errorProvider1.SetIconAlignment(this.radioButtonIVHex, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("radioButtonIVHex.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.radioButtonIVHex, ((int)(resources.GetObject("radioButtonIVHex.IconPadding"))));
            this.radioButtonIVHex.Name = "radioButtonIVHex";
            this.radioButtonIVHex.UseVisualStyleBackColor = true;
            this.radioButtonIVHex.CheckedChanged += new System.EventHandler(this.radioButtonIVHex_CheckedChanged);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.errorProvider1.SetError(this.label4, resources.GetString("label4.Error"));
            this.label4.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.errorProvider1.SetIconAlignment(this.label4, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label4.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label4, ((int)(resources.GetObject("label4.IconPadding"))));
            this.label4.Name = "label4";
            // 
            // textBoxIV
            // 
            resources.ApplyResources(this.textBoxIV, "textBoxIV");
            this.errorProvider1.SetError(this.textBoxIV, resources.GetString("textBoxIV.Error"));
            this.errorProvider1.SetIconAlignment(this.textBoxIV, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("textBoxIV.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.textBoxIV, ((int)(resources.GetObject("textBoxIV.IconPadding"))));
            this.textBoxIV.Name = "textBoxIV";
            this.textBoxIV.TextChanged += new System.EventHandler(this.textBoxIV_TextChanged);
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.errorProvider1.SetError(this.label5, resources.GetString("label5.Error"));
            this.errorProvider1.SetIconAlignment(this.label5, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label5.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label5, ((int)(resources.GetObject("label5.IconPadding"))));
            this.label5.Name = "label5";
            // 
            // textBoxFairPlayLAurl
            // 
            resources.ApplyResources(this.textBoxFairPlayLAurl, "textBoxFairPlayLAurl");
            this.errorProvider1.SetError(this.textBoxFairPlayLAurl, resources.GetString("textBoxFairPlayLAurl.Error"));
            this.errorProvider1.SetIconAlignment(this.textBoxFairPlayLAurl, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("textBoxFairPlayLAurl.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.textBoxFairPlayLAurl, ((int)(resources.GetObject("textBoxFairPlayLAurl.IconPadding"))));
            this.textBoxFairPlayLAurl.Name = "textBoxFairPlayLAurl";
            this.textBoxFairPlayLAurl.TextChanged += new System.EventHandler(this.textBoxFairPlayLAurl_TextChanged);
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.errorProvider1.SetError(this.label7, resources.GetString("label7.Error"));
            this.label7.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.errorProvider1.SetIconAlignment(this.label7, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label7.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label7, ((int)(resources.GetObject("label7.IconPadding"))));
            this.label7.Name = "label7";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.errorProvider1.SetError(this.label3, resources.GetString("label3.Error"));
            this.errorProvider1.SetIconAlignment(this.label3, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label3.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label3, ((int)(resources.GetObject("label3.IconPadding"))));
            this.label3.Name = "label3";
            // 
            // radioButtonDeliverFairPlayfromAMS
            // 
            resources.ApplyResources(this.radioButtonDeliverFairPlayfromAMS, "radioButtonDeliverFairPlayfromAMS");
            this.radioButtonDeliverFairPlayfromAMS.Checked = true;
            this.errorProvider1.SetError(this.radioButtonDeliverFairPlayfromAMS, resources.GetString("radioButtonDeliverFairPlayfromAMS.Error"));
            this.errorProvider1.SetIconAlignment(this.radioButtonDeliverFairPlayfromAMS, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("radioButtonDeliverFairPlayfromAMS.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.radioButtonDeliverFairPlayfromAMS, ((int)(resources.GetObject("radioButtonDeliverFairPlayfromAMS.IconPadding"))));
            this.radioButtonDeliverFairPlayfromAMS.Name = "radioButtonDeliverFairPlayfromAMS";
            this.radioButtonDeliverFairPlayfromAMS.TabStop = true;
            this.radioButtonDeliverFairPlayfromAMS.UseVisualStyleBackColor = true;
            // 
            // radioButtonExternalFairPlayServer
            // 
            resources.ApplyResources(this.radioButtonExternalFairPlayServer, "radioButtonExternalFairPlayServer");
            this.errorProvider1.SetError(this.radioButtonExternalFairPlayServer, resources.GetString("radioButtonExternalFairPlayServer.Error"));
            this.errorProvider1.SetIconAlignment(this.radioButtonExternalFairPlayServer, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("radioButtonExternalFairPlayServer.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.radioButtonExternalFairPlayServer, ((int)(resources.GetObject("radioButtonExternalFairPlayServer.IconPadding"))));
            this.radioButtonExternalFairPlayServer.Name = "radioButtonExternalFairPlayServer";
            this.radioButtonExternalFairPlayServer.UseVisualStyleBackColor = true;
            this.radioButtonExternalFairPlayServer.CheckedChanged += new System.EventHandler(this.radioButtonExternalPRServer_CheckedChanged);
            // 
            // numericUpDownNbOptionsFairPlay
            // 
            resources.ApplyResources(this.numericUpDownNbOptionsFairPlay, "numericUpDownNbOptionsFairPlay");
            this.errorProvider1.SetError(this.numericUpDownNbOptionsFairPlay, resources.GetString("numericUpDownNbOptionsFairPlay.Error"));
            this.errorProvider1.SetIconAlignment(this.numericUpDownNbOptionsFairPlay, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("numericUpDownNbOptionsFairPlay.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.numericUpDownNbOptionsFairPlay, ((int)(resources.GetObject("numericUpDownNbOptionsFairPlay.IconPadding"))));
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
            this.numericUpDownNbOptionsFairPlay.Name = "numericUpDownNbOptionsFairPlay";
            this.numericUpDownNbOptionsFairPlay.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.errorProvider1.SetError(this.label2, resources.GetString("label2.Error"));
            this.errorProvider1.SetIconAlignment(this.label2, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label2.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label2, ((int)(resources.GetObject("label2.IconPadding"))));
            this.label2.Name = "label2";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.errorProvider1.SetError(this.label8, resources.GetString("label8.Error"));
            this.label8.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.errorProvider1.SetIconAlignment(this.label8, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label8.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label8, ((int)(resources.GetObject("label8.IconPadding"))));
            this.label8.Name = "label8";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            resources.ApplyResources(this.errorProvider1, "errorProvider1");
            // 
            // AddDynamicEncryptionFrame3_CENC_Cbcs_Delivery
            // 
            this.AcceptButton = this.buttonOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.label8);
            this.Controls.Add(this.groupBoxFairPlay);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Name = "AddDynamicEncryptionFrame3_CENC_Cbcs_Delivery";
            this.Load += new System.EventHandler(this.AddDynamicEncryptionFrame3_Load);
            this.panel1.ResumeLayout(false);
            this.groupBoxFairPlay.ResumeLayout(false);
            this.groupBoxFairPlay.PerformLayout();
            this.panelFairPlayFromAMS.ResumeLayout(false);
            this.panelFairPlayFromAMS.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panelExternalFairPlay.ResumeLayout(false);
            this.panelExternalFairPlay.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNbOptionsFairPlay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
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
        private System.Windows.Forms.CheckBox checkBoxFinalExtURL;
        private System.Windows.Forms.TextBox TextBoxCertificateFile;
        private System.Windows.Forms.LinkLabel moreinfoFairPlaylink;
        private System.Windows.Forms.Label label10;
    }
}