﻿namespace AMSExplorer
{
    partial class AddDynamicEncryptionFrame4
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
            this.groupBoxAuthPol = new System.Windows.Forms.GroupBox();
            this.radioButtonTokenAuthPolicy = new System.Windows.Forms.RadioButton();
            this.radioButtonOpenAuthPolicy = new System.Windows.Forms.RadioButton();
            this.panelSymKey = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.radioButtonContentKeyBase64 = new System.Windows.Forms.RadioButton();
            this.radioButtonContentKeyHex = new System.Windows.Forms.RadioButton();
            this.textBoxSymKey = new System.Windows.Forms.TextBox();
            this.buttongenerateContentKey = new System.Windows.Forms.Button();
            this.checkBoxAddContentKeyIdentifierClaim = new System.Windows.Forms.CheckBox();
            this.radioButtonJWTSymmetric = new System.Windows.Forms.RadioButton();
            this.panelJWT = new System.Windows.Forms.Panel();
            this.moreinfocGenX509 = new System.Windows.Forms.LinkLabel();
            this.buttonImportPFX = new System.Windows.Forms.Button();
            this.labelCertificateFile = new System.Windows.Forms.Label();
            this.radioButtonJWTX509 = new System.Windows.Forms.RadioButton();
            this.radioButtonSWT = new System.Windows.Forms.RadioButton();
            this.dataGridViewTokenClaims = new System.Windows.Forms.DataGridView();
            this.buttonDelClaim = new System.Windows.Forms.Button();
            this.buttonAddClaim = new System.Windows.Forms.Button();
            this.textBoxIssuer = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxAudience = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelStep = new System.Windows.Forms.Label();
            this.tabControlTokenType = new System.Windows.Forms.TabControl();
            this.tabPageTokenType = new System.Windows.Forms.TabPage();
            this.label8 = new System.Windows.Forms.Label();
            this.radioButtonJWTOpenId = new System.Windows.Forms.RadioButton();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tabPageTokenSymmetric = new System.Windows.Forms.TabPage();
            this.tabPageTokenX509 = new System.Windows.Forms.TabPage();
            this.tabPageOpenId = new System.Windows.Forms.TabPage();
            this.comboBoxMappingList = new System.Windows.Forms.ComboBox();
            this.buttonAddMapping = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxOpenIdDocument = new System.Windows.Forms.TextBox();
            this.tabControlTokenProperties = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBoxAuthPol.SuspendLayout();
            this.panelSymKey.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panelJWT.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTokenClaims)).BeginInit();
            this.panel1.SuspendLayout();
            this.tabControlTokenType.SuspendLayout();
            this.tabPageTokenType.SuspendLayout();
            this.tabPageTokenSymmetric.SuspendLayout();
            this.tabPageTokenX509.SuspendLayout();
            this.tabPageOpenId.SuspendLayout();
            this.tabControlTokenProperties.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(386, 13);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(176, 27);
            this.buttonOk.TabIndex = 17;
            this.buttonOk.Text = "Ok";
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
            // groupBoxAuthPol
            // 
            this.groupBoxAuthPol.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxAuthPol.Controls.Add(this.radioButtonTokenAuthPolicy);
            this.groupBoxAuthPol.Controls.Add(this.radioButtonOpenAuthPolicy);
            this.groupBoxAuthPol.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxAuthPol.Location = new System.Drawing.Point(14, 84);
            this.groupBoxAuthPol.Name = "groupBoxAuthPol";
            this.groupBoxAuthPol.Size = new System.Drawing.Size(653, 90);
            this.groupBoxAuthPol.TabIndex = 47;
            this.groupBoxAuthPol.TabStop = false;
            this.groupBoxAuthPol.Text = "Content key\'s authorization policy option";
            // 
            // radioButtonTokenAuthPolicy
            // 
            this.radioButtonTokenAuthPolicy.AutoSize = true;
            this.radioButtonTokenAuthPolicy.Checked = true;
            this.radioButtonTokenAuthPolicy.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.radioButtonTokenAuthPolicy.Location = new System.Drawing.Point(23, 25);
            this.radioButtonTokenAuthPolicy.Name = "radioButtonTokenAuthPolicy";
            this.radioButtonTokenAuthPolicy.Size = new System.Drawing.Size(57, 19);
            this.radioButtonTokenAuthPolicy.TabIndex = 55;
            this.radioButtonTokenAuthPolicy.TabStop = true;
            this.radioButtonTokenAuthPolicy.Text = "Token";
            this.radioButtonTokenAuthPolicy.UseVisualStyleBackColor = true;
            this.radioButtonTokenAuthPolicy.CheckedChanged += new System.EventHandler(this.radioButtonToken_CheckedChanged);
            // 
            // radioButtonOpenAuthPolicy
            // 
            this.radioButtonOpenAuthPolicy.AutoSize = true;
            this.radioButtonOpenAuthPolicy.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.radioButtonOpenAuthPolicy.Location = new System.Drawing.Point(23, 52);
            this.radioButtonOpenAuthPolicy.Name = "radioButtonOpenAuthPolicy";
            this.radioButtonOpenAuthPolicy.Size = new System.Drawing.Size(54, 19);
            this.radioButtonOpenAuthPolicy.TabIndex = 54;
            this.radioButtonOpenAuthPolicy.Text = "Open";
            this.radioButtonOpenAuthPolicy.UseVisualStyleBackColor = true;
            this.radioButtonOpenAuthPolicy.CheckedChanged += new System.EventHandler(this.radioButtonOpen_CheckedChanged);
            // 
            // panelSymKey
            // 
            this.panelSymKey.Controls.Add(this.label1);
            this.panelSymKey.Controls.Add(this.panel2);
            this.panelSymKey.Controls.Add(this.textBoxSymKey);
            this.panelSymKey.Controls.Add(this.buttongenerateContentKey);
            this.panelSymKey.Location = new System.Drawing.Point(21, 29);
            this.panelSymKey.Name = "panelSymKey";
            this.panelSymKey.Size = new System.Drawing.Size(602, 73);
            this.panelSymKey.TabIndex = 76;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 15);
            this.label1.TabIndex = 75;
            this.label1.Text = "Symmetric key:";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.radioButtonContentKeyBase64);
            this.panel2.Controls.Add(this.radioButtonContentKeyHex);
            this.panel2.Location = new System.Drawing.Point(427, 6);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(87, 55);
            this.panel2.TabIndex = 74;
            // 
            // radioButtonContentKeyBase64
            // 
            this.radioButtonContentKeyBase64.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonContentKeyBase64.AutoSize = true;
            this.radioButtonContentKeyBase64.Checked = true;
            this.radioButtonContentKeyBase64.Location = new System.Drawing.Point(9, 7);
            this.radioButtonContentKeyBase64.Name = "radioButtonContentKeyBase64";
            this.radioButtonContentKeyBase64.Size = new System.Drawing.Size(61, 19);
            this.radioButtonContentKeyBase64.TabIndex = 68;
            this.radioButtonContentKeyBase64.TabStop = true;
            this.radioButtonContentKeyBase64.Text = "Base64";
            this.radioButtonContentKeyBase64.UseVisualStyleBackColor = true;
            this.radioButtonContentKeyBase64.CheckedChanged += new System.EventHandler(this.radioButtonContentKeyBase64_CheckedChanged);
            // 
            // radioButtonContentKeyHex
            // 
            this.radioButtonContentKeyHex.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonContentKeyHex.AutoSize = true;
            this.radioButtonContentKeyHex.Location = new System.Drawing.Point(9, 32);
            this.radioButtonContentKeyHex.Name = "radioButtonContentKeyHex";
            this.radioButtonContentKeyHex.Size = new System.Drawing.Size(45, 19);
            this.radioButtonContentKeyHex.TabIndex = 69;
            this.radioButtonContentKeyHex.Text = "Hex";
            this.radioButtonContentKeyHex.UseVisualStyleBackColor = true;
            this.radioButtonContentKeyHex.CheckedChanged += new System.EventHandler(this.radioButtonContentKeyHex_CheckedChanged);
            // 
            // textBoxSymKey
            // 
            this.textBoxSymKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSymKey.Location = new System.Drawing.Point(12, 24);
            this.textBoxSymKey.Name = "textBoxSymKey";
            this.textBoxSymKey.Size = new System.Drawing.Size(408, 23);
            this.textBoxSymKey.TabIndex = 72;
            // 
            // buttongenerateContentKey
            // 
            this.buttongenerateContentKey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttongenerateContentKey.Location = new System.Drawing.Point(521, 21);
            this.buttongenerateContentKey.Name = "buttongenerateContentKey";
            this.buttongenerateContentKey.Size = new System.Drawing.Size(75, 29);
            this.buttongenerateContentKey.TabIndex = 73;
            this.buttongenerateContentKey.Text = "Generate";
            this.buttongenerateContentKey.UseVisualStyleBackColor = true;
            this.buttongenerateContentKey.Click += new System.EventHandler(this.buttongenerateContentKey_Click);
            // 
            // checkBoxAddContentKeyIdentifierClaim
            // 
            this.checkBoxAddContentKeyIdentifierClaim.AutoSize = true;
            this.checkBoxAddContentKeyIdentifierClaim.Checked = true;
            this.checkBoxAddContentKeyIdentifierClaim.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAddContentKeyIdentifierClaim.Location = new System.Drawing.Point(24, 17);
            this.checkBoxAddContentKeyIdentifierClaim.Name = "checkBoxAddContentKeyIdentifierClaim";
            this.checkBoxAddContentKeyIdentifierClaim.Size = new System.Drawing.Size(200, 19);
            this.checkBoxAddContentKeyIdentifierClaim.TabIndex = 70;
            this.checkBoxAddContentKeyIdentifierClaim.Text = "Add Content Key Identifier Claim";
            this.checkBoxAddContentKeyIdentifierClaim.UseVisualStyleBackColor = true;
            // 
            // radioButtonJWTSymmetric
            // 
            this.radioButtonJWTSymmetric.AutoSize = true;
            this.radioButtonJWTSymmetric.Checked = true;
            this.radioButtonJWTSymmetric.Location = new System.Drawing.Point(24, 42);
            this.radioButtonJWTSymmetric.Name = "radioButtonJWTSymmetric";
            this.radioButtonJWTSymmetric.Size = new System.Drawing.Size(148, 19);
            this.radioButtonJWTSymmetric.TabIndex = 69;
            this.radioButtonJWTSymmetric.TabStop = true;
            this.radioButtonJWTSymmetric.Text = "JWT - JSON Web Token";
            this.radioButtonJWTSymmetric.UseVisualStyleBackColor = true;
            this.radioButtonJWTSymmetric.CheckedChanged += new System.EventHandler(this.radioButtonTokenType_CheckedChanged);
            // 
            // panelJWT
            // 
            this.panelJWT.Controls.Add(this.moreinfocGenX509);
            this.panelJWT.Controls.Add(this.buttonImportPFX);
            this.panelJWT.Controls.Add(this.labelCertificateFile);
            this.panelJWT.Enabled = false;
            this.panelJWT.Location = new System.Drawing.Point(10, 22);
            this.panelJWT.Name = "panelJWT";
            this.panelJWT.Size = new System.Drawing.Size(612, 72);
            this.panelJWT.TabIndex = 68;
            // 
            // moreinfocGenX509
            // 
            this.moreinfocGenX509.AutoSize = true;
            this.moreinfocGenX509.Location = new System.Drawing.Point(3, 46);
            this.moreinfocGenX509.Name = "moreinfocGenX509";
            this.moreinfocGenX509.Size = new System.Drawing.Size(230, 15);
            this.moreinfocGenX509.TabIndex = 69;
            this.moreinfocGenX509.TabStop = true;
            this.moreinfocGenX509.Text = "How to generate a X509 certificate for JWT";
            // 
            // buttonImportPFX
            // 
            this.buttonImportPFX.Location = new System.Drawing.Point(3, 3);
            this.buttonImportPFX.Name = "buttonImportPFX";
            this.buttonImportPFX.Size = new System.Drawing.Size(227, 27);
            this.buttonImportPFX.TabIndex = 66;
            this.buttonImportPFX.Text = "Import X509 Certificate (.PFX)...";
            this.buttonImportPFX.UseVisualStyleBackColor = true;
            this.buttonImportPFX.Click += new System.EventHandler(this.buttonImportPFX_Click);
            // 
            // labelCertificateFile
            // 
            this.labelCertificateFile.Location = new System.Drawing.Point(238, 9);
            this.labelCertificateFile.Name = "labelCertificateFile";
            this.labelCertificateFile.Size = new System.Drawing.Size(306, 21);
            this.labelCertificateFile.TabIndex = 67;
            this.labelCertificateFile.Text = "(no file selected)";
            // 
            // radioButtonJWTX509
            // 
            this.radioButtonJWTX509.AutoSize = true;
            this.radioButtonJWTX509.Location = new System.Drawing.Point(24, 68);
            this.radioButtonJWTX509.Name = "radioButtonJWTX509";
            this.radioButtonJWTX509.Size = new System.Drawing.Size(148, 19);
            this.radioButtonJWTX509.TabIndex = 65;
            this.radioButtonJWTX509.Text = "JWT - JSON Web Token";
            this.radioButtonJWTX509.UseVisualStyleBackColor = true;
            this.radioButtonJWTX509.CheckedChanged += new System.EventHandler(this.radioButtonTokenType_CheckedChanged);
            // 
            // radioButtonSWT
            // 
            this.radioButtonSWT.AutoSize = true;
            this.radioButtonSWT.Location = new System.Drawing.Point(24, 15);
            this.radioButtonSWT.Name = "radioButtonSWT";
            this.radioButtonSWT.Size = new System.Drawing.Size(158, 19);
            this.radioButtonSWT.TabIndex = 64;
            this.radioButtonSWT.Text = "SWT - Simple Web Token";
            this.radioButtonSWT.UseVisualStyleBackColor = true;
            this.radioButtonSWT.CheckedChanged += new System.EventHandler(this.radioButtonTokenType_CheckedChanged);
            // 
            // dataGridViewTokenClaims
            // 
            this.dataGridViewTokenClaims.AllowUserToAddRows = false;
            this.dataGridViewTokenClaims.AllowUserToDeleteRows = false;
            this.dataGridViewTokenClaims.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewTokenClaims.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewTokenClaims.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTokenClaims.Location = new System.Drawing.Point(24, 70);
            this.dataGridViewTokenClaims.Name = "dataGridViewTokenClaims";
            this.dataGridViewTokenClaims.RowHeadersVisible = false;
            this.dataGridViewTokenClaims.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewTokenClaims.Size = new System.Drawing.Size(602, 100);
            this.dataGridViewTokenClaims.TabIndex = 53;
            // 
            // buttonDelClaim
            // 
            this.buttonDelClaim.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDelClaim.Location = new System.Drawing.Point(119, 176);
            this.buttonDelClaim.Name = "buttonDelClaim";
            this.buttonDelClaim.Size = new System.Drawing.Size(87, 27);
            this.buttonDelClaim.TabIndex = 62;
            this.buttonDelClaim.Text = "Delete";
            this.buttonDelClaim.UseVisualStyleBackColor = true;
            this.buttonDelClaim.Click += new System.EventHandler(this.buttonDelClaim_Click);
            // 
            // buttonAddClaim
            // 
            this.buttonAddClaim.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAddClaim.Location = new System.Drawing.Point(24, 176);
            this.buttonAddClaim.Name = "buttonAddClaim";
            this.buttonAddClaim.Size = new System.Drawing.Size(87, 27);
            this.buttonAddClaim.TabIndex = 61;
            this.buttonAddClaim.Text = "Add";
            this.buttonAddClaim.UseVisualStyleBackColor = true;
            this.buttonAddClaim.Click += new System.EventHandler(this.buttonAddClaim_Click);
            // 
            // textBoxIssuer
            // 
            this.textBoxIssuer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxIssuer.Location = new System.Drawing.Point(24, 47);
            this.textBoxIssuer.Name = "textBoxIssuer";
            this.textBoxIssuer.Size = new System.Drawing.Size(598, 23);
            this.textBoxIssuer.TabIndex = 56;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 15);
            this.label3.TabIndex = 59;
            this.label3.Text = "Audience or scope :";
            // 
            // textBoxAudience
            // 
            this.textBoxAudience.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxAudience.Location = new System.Drawing.Point(24, 118);
            this.textBoxAudience.Name = "textBoxAudience";
            this.textBoxAudience.Size = new System.Drawing.Size(598, 23);
            this.textBoxAudience.TabIndex = 57;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 15);
            this.label2.TabIndex = 58;
            this.label2.Text = "Issuer :";
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
            // labelStep
            // 
            this.labelStep.AutoSize = true;
            this.labelStep.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.labelStep.ForeColor = System.Drawing.Color.DarkBlue;
            this.labelStep.Location = new System.Drawing.Point(26, 21);
            this.labelStep.Name = "labelStep";
            this.labelStep.Size = new System.Drawing.Size(464, 42);
            this.labelStep.TabIndex = 84;
            this.labelStep.Text = "Step {0}\r\nSpecify the content key Authorization Policy Option #{1} for {2}";
            // 
            // tabControlTokenType
            // 
            this.tabControlTokenType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlTokenType.Controls.Add(this.tabPageTokenType);
            this.tabControlTokenType.Controls.Add(this.tabPageTokenSymmetric);
            this.tabControlTokenType.Controls.Add(this.tabPageTokenX509);
            this.tabControlTokenType.Controls.Add(this.tabPageOpenId);
            this.tabControlTokenType.Location = new System.Drawing.Point(14, 198);
            this.tabControlTokenType.Name = "tabControlTokenType";
            this.tabControlTokenType.SelectedIndex = 0;
            this.tabControlTokenType.Size = new System.Drawing.Size(653, 159);
            this.tabControlTokenType.TabIndex = 85;
            // 
            // tabPageTokenType
            // 
            this.tabPageTokenType.Controls.Add(this.label8);
            this.tabPageTokenType.Controls.Add(this.radioButtonJWTOpenId);
            this.tabPageTokenType.Controls.Add(this.label7);
            this.tabPageTokenType.Controls.Add(this.label6);
            this.tabPageTokenType.Controls.Add(this.label5);
            this.tabPageTokenType.Controls.Add(this.radioButtonJWTSymmetric);
            this.tabPageTokenType.Controls.Add(this.radioButtonSWT);
            this.tabPageTokenType.Controls.Add(this.radioButtonJWTX509);
            this.tabPageTokenType.Location = new System.Drawing.Point(4, 24);
            this.tabPageTokenType.Name = "tabPageTokenType";
            this.tabPageTokenType.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTokenType.Size = new System.Drawing.Size(645, 131);
            this.tabPageTokenType.TabIndex = 0;
            this.tabPageTokenType.Text = "Token Type";
            this.tabPageTokenType.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label8.Location = new System.Drawing.Point(206, 97);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(294, 22);
            this.label8.TabIndex = 74;
            this.label8.Text = "with OpenId (JSON Web Keys)";
            // 
            // radioButtonJWTOpenId
            // 
            this.radioButtonJWTOpenId.AutoSize = true;
            this.radioButtonJWTOpenId.Location = new System.Drawing.Point(24, 95);
            this.radioButtonJWTOpenId.Name = "radioButtonJWTOpenId";
            this.radioButtonJWTOpenId.Size = new System.Drawing.Size(148, 19);
            this.radioButtonJWTOpenId.TabIndex = 73;
            this.radioButtonJWTOpenId.Text = "JWT - JSON Web Token";
            this.radioButtonJWTOpenId.UseVisualStyleBackColor = true;
            this.radioButtonJWTOpenId.CheckedChanged += new System.EventHandler(this.radioButtonTokenType_CheckedChanged);
            // 
            // label7
            // 
            this.label7.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label7.Location = new System.Drawing.Point(206, 70);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(294, 22);
            this.label7.TabIndex = 72;
            this.label7.Text = "with asymmetric key (X509 Certificate)";
            // 
            // label6
            // 
            this.label6.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label6.Location = new System.Drawing.Point(206, 44);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(294, 22);
            this.label6.TabIndex = 71;
            this.label6.Text = "with symmetric key";
            // 
            // label5
            // 
            this.label5.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label5.Location = new System.Drawing.Point(206, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(294, 22);
            this.label5.TabIndex = 70;
            this.label5.Text = "with symmetric key";
            // 
            // tabPageTokenSymmetric
            // 
            this.tabPageTokenSymmetric.Controls.Add(this.panelSymKey);
            this.tabPageTokenSymmetric.Location = new System.Drawing.Point(4, 24);
            this.tabPageTokenSymmetric.Name = "tabPageTokenSymmetric";
            this.tabPageTokenSymmetric.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTokenSymmetric.Size = new System.Drawing.Size(645, 131);
            this.tabPageTokenSymmetric.TabIndex = 1;
            this.tabPageTokenSymmetric.Text = "Symmetric token key";
            this.tabPageTokenSymmetric.UseVisualStyleBackColor = true;
            // 
            // tabPageTokenX509
            // 
            this.tabPageTokenX509.Controls.Add(this.panelJWT);
            this.tabPageTokenX509.Location = new System.Drawing.Point(4, 24);
            this.tabPageTokenX509.Name = "tabPageTokenX509";
            this.tabPageTokenX509.Size = new System.Drawing.Size(645, 131);
            this.tabPageTokenX509.TabIndex = 2;
            this.tabPageTokenX509.Text = "X509 Certificate";
            this.tabPageTokenX509.UseVisualStyleBackColor = true;
            // 
            // tabPageOpenId
            // 
            this.tabPageOpenId.Controls.Add(this.comboBoxMappingList);
            this.tabPageOpenId.Controls.Add(this.buttonAddMapping);
            this.tabPageOpenId.Controls.Add(this.label9);
            this.tabPageOpenId.Controls.Add(this.textBoxOpenIdDocument);
            this.tabPageOpenId.Location = new System.Drawing.Point(4, 24);
            this.tabPageOpenId.Name = "tabPageOpenId";
            this.tabPageOpenId.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageOpenId.Size = new System.Drawing.Size(645, 131);
            this.tabPageOpenId.TabIndex = 3;
            this.tabPageOpenId.Text = "OpenId";
            this.tabPageOpenId.UseVisualStyleBackColor = true;
            // 
            // comboBoxMappingList
            // 
            this.comboBoxMappingList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxMappingList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMappingList.FormattingEnabled = true;
            this.comboBoxMappingList.Location = new System.Drawing.Point(336, 35);
            this.comboBoxMappingList.Name = "comboBoxMappingList";
            this.comboBoxMappingList.Size = new System.Drawing.Size(175, 23);
            this.comboBoxMappingList.TabIndex = 62;
            // 
            // buttonAddMapping
            // 
            this.buttonAddMapping.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddMapping.Location = new System.Drawing.Point(519, 32);
            this.buttonAddMapping.Name = "buttonAddMapping";
            this.buttonAddMapping.Size = new System.Drawing.Size(104, 27);
            this.buttonAddMapping.TabIndex = 61;
            this.buttonAddMapping.Text = "Insert Url";
            this.buttonAddMapping.UseVisualStyleBackColor = true;
            this.buttonAddMapping.Click += new System.EventHandler(this.buttonAddMapping_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(21, 47);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(213, 15);
            this.label9.TabIndex = 60;
            this.label9.Text = "OpenId Connect Discovery Document :";
            // 
            // textBoxOpenIdDocument
            // 
            this.textBoxOpenIdDocument.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxOpenIdDocument.Location = new System.Drawing.Point(24, 66);
            this.textBoxOpenIdDocument.Name = "textBoxOpenIdDocument";
            this.textBoxOpenIdDocument.Size = new System.Drawing.Size(598, 23);
            this.textBoxOpenIdDocument.TabIndex = 59;
            // 
            // tabControlTokenProperties
            // 
            this.tabControlTokenProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlTokenProperties.Controls.Add(this.tabPage2);
            this.tabControlTokenProperties.Controls.Add(this.tabPage3);
            this.tabControlTokenProperties.Location = new System.Drawing.Point(14, 381);
            this.tabControlTokenProperties.Name = "tabControlTokenProperties";
            this.tabControlTokenProperties.SelectedIndex = 0;
            this.tabControlTokenProperties.Size = new System.Drawing.Size(653, 248);
            this.tabControlTokenProperties.TabIndex = 86;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label11);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.textBoxAudience);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.textBoxIssuer);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(645, 220);
            this.tabPage2.TabIndex = 0;
            this.tabPage2.Text = "Token Properties";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label11.Location = new System.Drawing.Point(329, 144);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(294, 22);
            this.label11.TabIndex = 75;
            this.label11.Text = "Example : urn:test";
            this.label11.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label10
            // 
            this.label10.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label10.Location = new System.Drawing.Point(329, 74);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(294, 22);
            this.label10.TabIndex = 74;
            this.label10.Text = "Example : http://testacs.com";
            this.label10.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Controls.Add(this.checkBoxAddContentKeyIdentifierClaim);
            this.tabPage3.Controls.Add(this.dataGridViewTokenClaims);
            this.tabPage3.Controls.Add(this.buttonAddClaim);
            this.tabPage3.Controls.Add(this.buttonDelClaim);
            this.tabPage3.Location = new System.Drawing.Point(4, 24);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(645, 220);
            this.tabPage3.TabIndex = 1;
            this.tabPage3.Text = "Claims";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label4.Location = new System.Drawing.Point(21, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(294, 22);
            this.label4.TabIndex = 73;
            this.label4.Text = "Other claims :";
            // 
            // AddDynamicEncryptionFrame4
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(681, 741);
            this.Controls.Add(this.tabControlTokenProperties);
            this.Controls.Add(this.tabControlTokenType);
            this.Controls.Add(this.labelStep);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBoxAuthPol);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "AddDynamicEncryptionFrame4";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Dynamic Encryption - Step {0}";
            this.Load += new System.EventHandler(this.SetupDynEnc_Load);
            this.groupBoxAuthPol.ResumeLayout(false);
            this.groupBoxAuthPol.PerformLayout();
            this.panelSymKey.ResumeLayout(false);
            this.panelSymKey.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panelJWT.ResumeLayout(false);
            this.panelJWT.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTokenClaims)).EndInit();
            this.panel1.ResumeLayout(false);
            this.tabControlTokenType.ResumeLayout(false);
            this.tabPageTokenType.ResumeLayout(false);
            this.tabPageTokenType.PerformLayout();
            this.tabPageTokenSymmetric.ResumeLayout(false);
            this.tabPageTokenX509.ResumeLayout(false);
            this.tabPageOpenId.ResumeLayout(false);
            this.tabPageOpenId.PerformLayout();
            this.tabControlTokenProperties.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button buttonOk;
        public System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.GroupBox groupBoxAuthPol;
        private System.Windows.Forms.RadioButton radioButtonTokenAuthPolicy;
        private System.Windows.Forms.RadioButton radioButtonOpenAuthPolicy;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxAudience;
        private System.Windows.Forms.TextBox textBoxIssuer;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonDelClaim;
        private System.Windows.Forms.Button buttonAddClaim;
        private System.Windows.Forms.DataGridView dataGridViewTokenClaims;
        private System.Windows.Forms.RadioButton radioButtonJWTX509;
        private System.Windows.Forms.RadioButton radioButtonSWT;
        private System.Windows.Forms.Label labelCertificateFile;
        private System.Windows.Forms.Button buttonImportPFX;
        private System.Windows.Forms.Panel panelJWT;
        private System.Windows.Forms.LinkLabel moreinfocGenX509;
        private System.Windows.Forms.RadioButton radioButtonJWTSymmetric;
        private System.Windows.Forms.CheckBox checkBoxAddContentKeyIdentifierClaim;
        private System.Windows.Forms.Label labelStep;
        public System.Windows.Forms.TextBox textBoxSymKey;
        private System.Windows.Forms.Button buttongenerateContentKey;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton radioButtonContentKeyBase64;
        private System.Windows.Forms.RadioButton radioButtonContentKeyHex;
        private System.Windows.Forms.Panel panelSymKey;
        public System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControlTokenType;
        private System.Windows.Forms.TabPage tabPageTokenType;
        private System.Windows.Forms.TabPage tabPageTokenSymmetric;
        private System.Windows.Forms.TabPage tabPageTokenX509;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TabControl tabControlTokenProperties;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.RadioButton radioButtonJWTOpenId;
        private System.Windows.Forms.TabPage tabPageOpenId;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxOpenIdDocument;
        private System.Windows.Forms.ComboBox comboBoxMappingList;
        private System.Windows.Forms.Button buttonAddMapping;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
    }
}