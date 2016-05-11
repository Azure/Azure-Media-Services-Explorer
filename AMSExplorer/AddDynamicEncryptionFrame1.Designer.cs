namespace AMSExplorer
{
    partial class AddDynamicEncryptionFrame1
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
            this.radioButtonAESClearKey = new System.Windows.Forms.RadioButton();
            this.radioButtonCENCKey = new System.Windows.Forms.RadioButton();
            this.groupBoxKeyType = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.radioButtonCENCCbcsKey = new System.Windows.Forms.RadioButton();
            this.radioButtonNoDynEnc = new System.Windows.Forms.RadioButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.radioButtonDecryptStorage = new System.Windows.Forms.RadioButton();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.groupBoxDelivery = new System.Windows.Forms.GroupBox();
            this.panelDynEnc = new System.Windows.Forms.Panel();
            this.checkBoxProtocolProgressiveDownload = new System.Windows.Forms.CheckBox();
            this.panelPackaging = new System.Windows.Forms.Panel();
            this.panelPackagingCENC = new System.Windows.Forms.Panel();
            this.checkBoxPlayReadyPackaging = new System.Windows.Forms.CheckBox();
            this.textBoxCustomAttributes = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.checkBoxWidevinePackaging = new System.Windows.Forms.CheckBox();
            this.checkBoxFairPlayPackaging = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBoxProtocolHLS = new System.Windows.Forms.CheckBox();
            this.checkBoxProtocolDASH = new System.Windows.Forms.CheckBox();
            this.checkBoxProtocolSmooth = new System.Windows.Forms.CheckBox();
            this.checkBoxEnableDynEnc = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBoxKeyType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.groupBoxDelivery.SuspendLayout();
            this.panelDynEnc.SuspendLayout();
            this.panelPackaging.SuspendLayout();
            this.panelPackagingCENC.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
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
            // radioButtonAESClearKey
            // 
            this.radioButtonAESClearKey.AutoSize = true;
            this.radioButtonAESClearKey.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonAESClearKey.Location = new System.Drawing.Point(52, 32);
            this.radioButtonAESClearKey.Name = "radioButtonAESClearKey";
            this.radioButtonAESClearKey.Size = new System.Drawing.Size(122, 19);
            this.radioButtonAESClearKey.TabIndex = 44;
            this.radioButtonAESClearKey.Text = "Envelope clear key";
            this.radioButtonAESClearKey.UseVisualStyleBackColor = true;
            this.radioButtonAESClearKey.CheckedChanged += new System.EventHandler(this.radioButtonCENCKey_CheckedChanged);
            // 
            // radioButtonCENCKey
            // 
            this.radioButtonCENCKey.AutoSize = true;
            this.radioButtonCENCKey.Checked = true;
            this.radioButtonCENCKey.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonCENCKey.Location = new System.Drawing.Point(52, 58);
            this.radioButtonCENCKey.Name = "radioButtonCENCKey";
            this.radioButtonCENCKey.Size = new System.Drawing.Size(265, 19);
            this.radioButtonCENCKey.TabIndex = 46;
            this.radioButtonCENCKey.TabStop = true;
            this.radioButtonCENCKey.Text = "Common encryption (PlayReady, Widevine...)";
            this.radioButtonCENCKey.UseVisualStyleBackColor = true;
            this.radioButtonCENCKey.CheckedChanged += new System.EventHandler(this.radioButtonCENCKey_CheckedChanged);
            // 
            // groupBoxKeyType
            // 
            this.groupBoxKeyType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxKeyType.Controls.Add(this.label9);
            this.groupBoxKeyType.Controls.Add(this.label8);
            this.groupBoxKeyType.Controls.Add(this.label3);
            this.groupBoxKeyType.Controls.Add(this.label2);
            this.groupBoxKeyType.Controls.Add(this.pictureBox4);
            this.groupBoxKeyType.Controls.Add(this.radioButtonCENCCbcsKey);
            this.groupBoxKeyType.Controls.Add(this.radioButtonNoDynEnc);
            this.groupBoxKeyType.Controls.Add(this.pictureBox1);
            this.groupBoxKeyType.Controls.Add(this.radioButtonDecryptStorage);
            this.groupBoxKeyType.Controls.Add(this.pictureBox3);
            this.groupBoxKeyType.Controls.Add(this.pictureBox2);
            this.groupBoxKeyType.Controls.Add(this.radioButtonCENCKey);
            this.groupBoxKeyType.Controls.Add(this.radioButtonAESClearKey);
            this.groupBoxKeyType.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxKeyType.Location = new System.Drawing.Point(14, 93);
            this.groupBoxKeyType.Name = "groupBoxKeyType";
            this.groupBoxKeyType.Size = new System.Drawing.Size(645, 171);
            this.groupBoxKeyType.TabIndex = 43;
            this.groupBoxKeyType.TabStop = false;
            this.groupBoxKeyType.Text = "Protection";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label8.Location = new System.Drawing.Point(323, 60);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(228, 15);
            this.label8.TabIndex = 88;
            this.label8.Text = "IE 11, Edge, Chrome, Android, Silverlight...";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label3.Location = new System.Drawing.Point(323, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(168, 15);
            this.label3.TabIndex = 87;
            this.label3.Text = "iOS, Apple TV, Safari on OS X...";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label2.Location = new System.Drawing.Point(323, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(219, 15);
            this.label2.TabIndex = 86;
            this.label2.Text = "IE 11, Edge, Chrome, iOS, Firefox, Flash...";
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::AMSExplorer.Bitmaps.DRM_protection;
            this.pictureBox4.Location = new System.Drawing.Point(26, 83);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(16, 16);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox4.TabIndex = 64;
            this.pictureBox4.TabStop = false;
            // 
            // radioButtonCENCCbcsKey
            // 
            this.radioButtonCENCCbcsKey.AutoSize = true;
            this.radioButtonCENCCbcsKey.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonCENCCbcsKey.Location = new System.Drawing.Point(52, 83);
            this.radioButtonCENCCbcsKey.Name = "radioButtonCENCCbcsKey";
            this.radioButtonCENCCbcsKey.Size = new System.Drawing.Size(215, 19);
            this.radioButtonCENCCbcsKey.TabIndex = 63;
            this.radioButtonCENCCbcsKey.Text = "Common encryption cbcs (FairPlay)";
            this.radioButtonCENCCbcsKey.UseVisualStyleBackColor = true;
            this.radioButtonCENCCbcsKey.CheckedChanged += new System.EventHandler(this.radioButtonCENCKey_CheckedChanged);
            // 
            // radioButtonNoDynEnc
            // 
            this.radioButtonNoDynEnc.AutoSize = true;
            this.radioButtonNoDynEnc.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonNoDynEnc.Location = new System.Drawing.Point(52, 133);
            this.radioButtonNoDynEnc.Name = "radioButtonNoDynEnc";
            this.radioButtonNoDynEnc.Size = new System.Drawing.Size(224, 19);
            this.radioButtonNoDynEnc.TabIndex = 62;
            this.radioButtonNoDynEnc.Text = "None - Asset already CENC encrypted";
            this.radioButtonNoDynEnc.UseVisualStyleBackColor = true;
            this.radioButtonNoDynEnc.CheckedChanged += new System.EventHandler(this.radioButtonCENCKey_CheckedChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::AMSExplorer.Bitmaps.storage_decryption;
            this.pictureBox1.Location = new System.Drawing.Point(26, 111);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 50;
            this.pictureBox1.TabStop = false;
            // 
            // radioButtonDecryptStorage
            // 
            this.radioButtonDecryptStorage.AutoSize = true;
            this.radioButtonDecryptStorage.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonDecryptStorage.Location = new System.Drawing.Point(52, 108);
            this.radioButtonDecryptStorage.Name = "radioButtonDecryptStorage";
            this.radioButtonDecryptStorage.Size = new System.Drawing.Size(311, 19);
            this.radioButtonDecryptStorage.TabIndex = 61;
            this.radioButtonDecryptStorage.Text = "Decryption (stream storage encrypted asset(s) in clear)";
            this.radioButtonDecryptStorage.UseVisualStyleBackColor = true;
            this.radioButtonDecryptStorage.CheckedChanged += new System.EventHandler(this.radioButtonCENCKey_CheckedChanged);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::AMSExplorer.Bitmaps.DRM_protection;
            this.pictureBox3.Location = new System.Drawing.Point(26, 58);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(16, 16);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox3.TabIndex = 60;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::AMSExplorer.Bitmaps.envelope_encryption;
            this.pictureBox2.Location = new System.Drawing.Point(26, 33);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(16, 16);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 51;
            this.pictureBox2.TabStop = false;
            // 
            // groupBoxDelivery
            // 
            this.groupBoxDelivery.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxDelivery.Controls.Add(this.panelDynEnc);
            this.groupBoxDelivery.Controls.Add(this.checkBoxEnableDynEnc);
            this.groupBoxDelivery.Location = new System.Drawing.Point(14, 287);
            this.groupBoxDelivery.Name = "groupBoxDelivery";
            this.groupBoxDelivery.Size = new System.Drawing.Size(645, 367);
            this.groupBoxDelivery.TabIndex = 47;
            this.groupBoxDelivery.TabStop = false;
            this.groupBoxDelivery.Text = "Delivery";
            // 
            // panelDynEnc
            // 
            this.panelDynEnc.Controls.Add(this.checkBoxProtocolProgressiveDownload);
            this.panelDynEnc.Controls.Add(this.panelPackaging);
            this.panelDynEnc.Controls.Add(this.label4);
            this.panelDynEnc.Controls.Add(this.checkBoxProtocolHLS);
            this.panelDynEnc.Controls.Add(this.checkBoxProtocolDASH);
            this.panelDynEnc.Controls.Add(this.checkBoxProtocolSmooth);
            this.panelDynEnc.Location = new System.Drawing.Point(6, 47);
            this.panelDynEnc.Name = "panelDynEnc";
            this.panelDynEnc.Size = new System.Drawing.Size(633, 314);
            this.panelDynEnc.TabIndex = 86;
            // 
            // checkBoxProtocolProgressiveDownload
            // 
            this.checkBoxProtocolProgressiveDownload.AutoSize = true;
            this.checkBoxProtocolProgressiveDownload.Location = new System.Drawing.Point(37, 115);
            this.checkBoxProtocolProgressiveDownload.Name = "checkBoxProtocolProgressiveDownload";
            this.checkBoxProtocolProgressiveDownload.Size = new System.Drawing.Size(143, 19);
            this.checkBoxProtocolProgressiveDownload.TabIndex = 87;
            this.checkBoxProtocolProgressiveDownload.Text = "Progressive Download";
            this.checkBoxProtocolProgressiveDownload.UseVisualStyleBackColor = true;
            this.checkBoxProtocolProgressiveDownload.Visible = false;
            // 
            // panelPackaging
            // 
            this.panelPackaging.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelPackaging.Controls.Add(this.panelPackagingCENC);
            this.panelPackaging.Controls.Add(this.checkBoxFairPlayPackaging);
            this.panelPackaging.Controls.Add(this.label5);
            this.panelPackaging.Location = new System.Drawing.Point(5, 146);
            this.panelPackaging.Name = "panelPackaging";
            this.panelPackaging.Size = new System.Drawing.Size(625, 165);
            this.panelPackaging.TabIndex = 86;
            // 
            // panelPackagingCENC
            // 
            this.panelPackagingCENC.Controls.Add(this.checkBoxPlayReadyPackaging);
            this.panelPackagingCENC.Controls.Add(this.textBoxCustomAttributes);
            this.panelPackagingCENC.Controls.Add(this.label6);
            this.panelPackagingCENC.Controls.Add(this.label7);
            this.panelPackagingCENC.Controls.Add(this.checkBoxWidevinePackaging);
            this.panelPackagingCENC.Location = new System.Drawing.Point(7, 28);
            this.panelPackagingCENC.Name = "panelPackagingCENC";
            this.panelPackagingCENC.Size = new System.Drawing.Size(615, 109);
            this.panelPackagingCENC.TabIndex = 87;
            // 
            // checkBoxPlayReadyPackaging
            // 
            this.checkBoxPlayReadyPackaging.AutoSize = true;
            this.checkBoxPlayReadyPackaging.Checked = true;
            this.checkBoxPlayReadyPackaging.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxPlayReadyPackaging.Location = new System.Drawing.Point(25, 4);
            this.checkBoxPlayReadyPackaging.Name = "checkBoxPlayReadyPackaging";
            this.checkBoxPlayReadyPackaging.Size = new System.Drawing.Size(109, 19);
            this.checkBoxPlayReadyPackaging.TabIndex = 58;
            this.checkBoxPlayReadyPackaging.Text = "PlayReady DRM";
            this.checkBoxPlayReadyPackaging.UseVisualStyleBackColor = true;
            this.checkBoxPlayReadyPackaging.CheckedChanged += new System.EventHandler(this.checkBoxPlayReadyPackaging_CheckedChanged);
            // 
            // textBoxCustomAttributes
            // 
            this.textBoxCustomAttributes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxCustomAttributes.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxCustomAttributes.Location = new System.Drawing.Point(45, 44);
            this.textBoxCustomAttributes.Name = "textBoxCustomAttributes";
            this.textBoxCustomAttributes.Size = new System.Drawing.Size(534, 23);
            this.textBoxCustomAttributes.TabIndex = 83;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label6.Location = new System.Drawing.Point(423, 70);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(156, 15);
            this.label6.TabIndex = 84;
            this.label6.Text = "name1:value1,name2:value2";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(42, 26);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(108, 15);
            this.label7.TabIndex = 85;
            this.label7.Text = "Custom attributes :";
            // 
            // checkBoxWidevinePackaging
            // 
            this.checkBoxWidevinePackaging.AutoSize = true;
            this.checkBoxWidevinePackaging.Checked = true;
            this.checkBoxWidevinePackaging.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxWidevinePackaging.Location = new System.Drawing.Point(25, 87);
            this.checkBoxWidevinePackaging.Name = "checkBoxWidevinePackaging";
            this.checkBoxWidevinePackaging.Size = new System.Drawing.Size(220, 19);
            this.checkBoxWidevinePackaging.TabIndex = 59;
            this.checkBoxWidevinePackaging.Text = "Widevine Modular DRM (DASH only)";
            this.checkBoxWidevinePackaging.UseVisualStyleBackColor = true;
            // 
            // checkBoxFairPlayPackaging
            // 
            this.checkBoxFairPlayPackaging.AutoSize = true;
            this.checkBoxFairPlayPackaging.Checked = true;
            this.checkBoxFairPlayPackaging.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxFairPlayPackaging.Enabled = false;
            this.checkBoxFairPlayPackaging.Location = new System.Drawing.Point(32, 143);
            this.checkBoxFairPlayPackaging.Name = "checkBoxFairPlayPackaging";
            this.checkBoxFairPlayPackaging.Size = new System.Drawing.Size(218, 19);
            this.checkBoxFairPlayPackaging.TabIndex = 86;
            this.checkBoxFairPlayPackaging.Text = "Apple FairPlay Streaming (HLS Only)";
            this.checkBoxFairPlayPackaging.UseVisualStyleBackColor = true;
            this.checkBoxFairPlayPackaging.Visible = false;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(4, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(618, 18);
            this.label5.TabIndex = 72;
            this.label5.Text = "Packaging";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(6, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(621, 18);
            this.label4.TabIndex = 71;
            this.label4.Text = "Delivery protocols";
            // 
            // checkBoxProtocolHLS
            // 
            this.checkBoxProtocolHLS.AutoSize = true;
            this.checkBoxProtocolHLS.Checked = true;
            this.checkBoxProtocolHLS.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxProtocolHLS.Location = new System.Drawing.Point(37, 40);
            this.checkBoxProtocolHLS.Name = "checkBoxProtocolHLS";
            this.checkBoxProtocolHLS.Size = new System.Drawing.Size(47, 19);
            this.checkBoxProtocolHLS.TabIndex = 55;
            this.checkBoxProtocolHLS.Text = "HLS";
            this.checkBoxProtocolHLS.UseVisualStyleBackColor = true;
            // 
            // checkBoxProtocolDASH
            // 
            this.checkBoxProtocolDASH.AutoSize = true;
            this.checkBoxProtocolDASH.Checked = true;
            this.checkBoxProtocolDASH.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxProtocolDASH.Location = new System.Drawing.Point(37, 65);
            this.checkBoxProtocolDASH.Name = "checkBoxProtocolDASH";
            this.checkBoxProtocolDASH.Size = new System.Drawing.Size(57, 19);
            this.checkBoxProtocolDASH.TabIndex = 56;
            this.checkBoxProtocolDASH.Text = "DASH";
            this.checkBoxProtocolDASH.UseVisualStyleBackColor = true;
            this.checkBoxProtocolDASH.CheckedChanged += new System.EventHandler(this.checkBoxProtocolDASH_CheckedChanged);
            // 
            // checkBoxProtocolSmooth
            // 
            this.checkBoxProtocolSmooth.AutoSize = true;
            this.checkBoxProtocolSmooth.Checked = true;
            this.checkBoxProtocolSmooth.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxProtocolSmooth.Location = new System.Drawing.Point(37, 90);
            this.checkBoxProtocolSmooth.Name = "checkBoxProtocolSmooth";
            this.checkBoxProtocolSmooth.Size = new System.Drawing.Size(125, 19);
            this.checkBoxProtocolSmooth.TabIndex = 57;
            this.checkBoxProtocolSmooth.Text = "Smooth Streaming";
            this.checkBoxProtocolSmooth.UseVisualStyleBackColor = true;
            // 
            // checkBoxEnableDynEnc
            // 
            this.checkBoxEnableDynEnc.AutoSize = true;
            this.checkBoxEnableDynEnc.Checked = true;
            this.checkBoxEnableDynEnc.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxEnableDynEnc.Location = new System.Drawing.Point(12, 22);
            this.checkBoxEnableDynEnc.Name = "checkBoxEnableDynEnc";
            this.checkBoxEnableDynEnc.Size = new System.Drawing.Size(170, 19);
            this.checkBoxEnableDynEnc.TabIndex = 74;
            this.checkBoxEnableDynEnc.Text = "Enable dynamic encryption";
            this.checkBoxEnableDynEnc.UseVisualStyleBackColor = true;
            this.checkBoxEnableDynEnc.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkBlue;
            this.label1.Location = new System.Drawing.Point(26, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(364, 42);
            this.label1.TabIndex = 49;
            this.label1.Text = "Step 1 \r\nSelect protection mode and dynamic encryption";
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
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(269, 85);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(48, 15);
            this.label9.TabIndex = 89;
            this.label9.Text = "Preview";
            // 
            // AddDynamicEncryptionFrame1
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(681, 741);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBoxDelivery);
            this.Controls.Add(this.groupBoxKeyType);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "AddDynamicEncryptionFrame1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Dynamic Encryption - Step 1";
            this.Load += new System.EventHandler(this.SetupDynEnc_Load);
            this.groupBoxKeyType.ResumeLayout(false);
            this.groupBoxKeyType.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.groupBoxDelivery.ResumeLayout(false);
            this.groupBoxDelivery.PerformLayout();
            this.panelDynEnc.ResumeLayout(false);
            this.panelDynEnc.PerformLayout();
            this.panelPackaging.ResumeLayout(false);
            this.panelPackaging.PerformLayout();
            this.panelPackagingCENC.ResumeLayout(false);
            this.panelPackagingCENC.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button buttonOk;
        public System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.RadioButton radioButtonAESClearKey;
        private System.Windows.Forms.RadioButton radioButtonCENCKey;
        private System.Windows.Forms.GroupBox groupBoxKeyType;
        private System.Windows.Forms.GroupBox groupBoxDelivery;
        private System.Windows.Forms.CheckBox checkBoxProtocolSmooth;
        private System.Windows.Forms.CheckBox checkBoxProtocolDASH;
        private System.Windows.Forms.CheckBox checkBoxProtocolHLS;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.RadioButton radioButtonDecryptStorage;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radioButtonNoDynEnc;
        private System.Windows.Forms.CheckBox checkBoxEnableDynEnc;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBoxWidevinePackaging;
        private System.Windows.Forms.CheckBox checkBoxPlayReadyPackaging;
        public System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxCustomAttributes;
        private System.Windows.Forms.Panel panelDynEnc;
        private System.Windows.Forms.Panel panelPackaging;
        private System.Windows.Forms.CheckBox checkBoxProtocolProgressiveDownload;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.RadioButton radioButtonCENCCbcsKey;
        private System.Windows.Forms.CheckBox checkBoxFairPlayPackaging;
        private System.Windows.Forms.Panel panelPackagingCENC;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label9;
    }
}