namespace AMSExplorer
{
    partial class AddDynamicEncryptionFrame3
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
            this.panelAutPol = new System.Windows.Forms.Panel();
            this.checkBoxAddContentKeyIdentifierClaim = new System.Windows.Forms.CheckBox();
            this.radioButtonJWTSymmetric = new System.Windows.Forms.RadioButton();
            this.panelJWT = new System.Windows.Forms.Panel();
            this.moreinfocGenX509 = new System.Windows.Forms.LinkLabel();
            this.buttonImportPFX = new System.Windows.Forms.Button();
            this.labelCertificateFile = new System.Windows.Forms.Label();
            this.radioButtonJWTX509 = new System.Windows.Forms.RadioButton();
            this.radioButtonSWT = new System.Windows.Forms.RadioButton();
            this.dataGridViewTokenClaims = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonDelClaim = new System.Windows.Forms.Button();
            this.buttonAddClaim = new System.Windows.Forms.Button();
            this.textBoxIssuer = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxAudience = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelStep = new System.Windows.Forms.Label();
            this.groupBoxAuthPol.SuspendLayout();
            this.panelAutPol.SuspendLayout();
            this.panelJWT.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTokenClaims)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(331, 11);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(151, 23);
            this.buttonOk.TabIndex = 17;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(488, 11);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(92, 23);
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
            this.groupBoxAuthPol.Location = new System.Drawing.Point(12, 73);
            this.groupBoxAuthPol.Name = "groupBoxAuthPol";
            this.groupBoxAuthPol.Size = new System.Drawing.Size(553, 78);
            this.groupBoxAuthPol.TabIndex = 47;
            this.groupBoxAuthPol.TabStop = false;
            this.groupBoxAuthPol.Text = "Content key\'s authorization policy option";
            // 
            // radioButtonTokenAuthPolicy
            // 
            this.radioButtonTokenAuthPolicy.AutoSize = true;
            this.radioButtonTokenAuthPolicy.Checked = true;
            this.radioButtonTokenAuthPolicy.Location = new System.Drawing.Point(20, 22);
            this.radioButtonTokenAuthPolicy.Name = "radioButtonTokenAuthPolicy";
            this.radioButtonTokenAuthPolicy.Size = new System.Drawing.Size(56, 17);
            this.radioButtonTokenAuthPolicy.TabIndex = 55;
            this.radioButtonTokenAuthPolicy.TabStop = true;
            this.radioButtonTokenAuthPolicy.Text = "Token";
            this.radioButtonTokenAuthPolicy.UseVisualStyleBackColor = true;
            this.radioButtonTokenAuthPolicy.CheckedChanged += new System.EventHandler(this.radioButtonToken_CheckedChanged);
            // 
            // radioButtonOpenAuthPolicy
            // 
            this.radioButtonOpenAuthPolicy.AutoSize = true;
            this.radioButtonOpenAuthPolicy.Location = new System.Drawing.Point(20, 45);
            this.radioButtonOpenAuthPolicy.Name = "radioButtonOpenAuthPolicy";
            this.radioButtonOpenAuthPolicy.Size = new System.Drawing.Size(51, 17);
            this.radioButtonOpenAuthPolicy.TabIndex = 54;
            this.radioButtonOpenAuthPolicy.Text = "Open";
            this.radioButtonOpenAuthPolicy.UseVisualStyleBackColor = true;
            this.radioButtonOpenAuthPolicy.CheckedChanged += new System.EventHandler(this.radioButtonOpen_CheckedChanged);
            // 
            // panelAutPol
            // 
            this.panelAutPol.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelAutPol.Controls.Add(this.checkBoxAddContentKeyIdentifierClaim);
            this.panelAutPol.Controls.Add(this.radioButtonJWTSymmetric);
            this.panelAutPol.Controls.Add(this.panelJWT);
            this.panelAutPol.Controls.Add(this.radioButtonJWTX509);
            this.panelAutPol.Controls.Add(this.radioButtonSWT);
            this.panelAutPol.Controls.Add(this.dataGridViewTokenClaims);
            this.panelAutPol.Controls.Add(this.label4);
            this.panelAutPol.Controls.Add(this.buttonDelClaim);
            this.panelAutPol.Controls.Add(this.buttonAddClaim);
            this.panelAutPol.Controls.Add(this.textBoxIssuer);
            this.panelAutPol.Controls.Add(this.label3);
            this.panelAutPol.Controls.Add(this.textBoxAudience);
            this.panelAutPol.Controls.Add(this.label2);
            this.panelAutPol.Location = new System.Drawing.Point(6, 19);
            this.panelAutPol.Name = "panelAutPol";
            this.panelAutPol.Size = new System.Drawing.Size(540, 391);
            this.panelAutPol.TabIndex = 60;
            // 
            // checkBoxAddContentKeyIdentifierClaim
            // 
            this.checkBoxAddContentKeyIdentifierClaim.AutoSize = true;
            this.checkBoxAddContentKeyIdentifierClaim.Checked = true;
            this.checkBoxAddContentKeyIdentifierClaim.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAddContentKeyIdentifierClaim.Location = new System.Drawing.Point(16, 77);
            this.checkBoxAddContentKeyIdentifierClaim.Name = "checkBoxAddContentKeyIdentifierClaim";
            this.checkBoxAddContentKeyIdentifierClaim.Size = new System.Drawing.Size(177, 17);
            this.checkBoxAddContentKeyIdentifierClaim.TabIndex = 70;
            this.checkBoxAddContentKeyIdentifierClaim.Text = "Add Content Key Identifier Claim";
            this.checkBoxAddContentKeyIdentifierClaim.UseVisualStyleBackColor = true;
            // 
            // radioButtonJWTSymmetric
            // 
            this.radioButtonJWTSymmetric.AutoSize = true;
            this.radioButtonJWTSymmetric.Checked = true;
            this.radioButtonJWTSymmetric.Location = new System.Drawing.Point(19, 277);
            this.radioButtonJWTSymmetric.Name = "radioButtonJWTSymmetric";
            this.radioButtonJWTSymmetric.Size = new System.Drawing.Size(314, 17);
            this.radioButtonJWTSymmetric.TabIndex = 69;
            this.radioButtonJWTSymmetric.TabStop = true;
            this.radioButtonJWTSymmetric.Text = "JSON Web Token (JWT) - with autogenerated symmetric key";
            this.radioButtonJWTSymmetric.UseVisualStyleBackColor = true;
            // 
            // panelJWT
            // 
            this.panelJWT.Controls.Add(this.moreinfocGenX509);
            this.panelJWT.Controls.Add(this.buttonImportPFX);
            this.panelJWT.Controls.Add(this.labelCertificateFile);
            this.panelJWT.Enabled = false;
            this.panelJWT.Location = new System.Drawing.Point(31, 333);
            this.panelJWT.Name = "panelJWT";
            this.panelJWT.Size = new System.Drawing.Size(469, 51);
            this.panelJWT.TabIndex = 68;
            // 
            // moreinfocGenX509
            // 
            this.moreinfocGenX509.AutoSize = true;
            this.moreinfocGenX509.Location = new System.Drawing.Point(3, 29);
            this.moreinfocGenX509.Name = "moreinfocGenX509";
            this.moreinfocGenX509.Size = new System.Drawing.Size(213, 13);
            this.moreinfocGenX509.TabIndex = 69;
            this.moreinfocGenX509.TabStop = true;
            this.moreinfocGenX509.Text = "How to generate a X509 certificate for JWT";
            // 
            // buttonImportPFX
            // 
            this.buttonImportPFX.Location = new System.Drawing.Point(3, 3);
            this.buttonImportPFX.Name = "buttonImportPFX";
            this.buttonImportPFX.Size = new System.Drawing.Size(195, 23);
            this.buttonImportPFX.TabIndex = 66;
            this.buttonImportPFX.Text = "Import X509 Certificate (.PFX)...";
            this.buttonImportPFX.UseVisualStyleBackColor = true;
            this.buttonImportPFX.Click += new System.EventHandler(this.buttonImportPFX_Click);
            // 
            // labelCertificateFile
            // 
            this.labelCertificateFile.Location = new System.Drawing.Point(204, 8);
            this.labelCertificateFile.Name = "labelCertificateFile";
            this.labelCertificateFile.Size = new System.Drawing.Size(262, 18);
            this.labelCertificateFile.TabIndex = 67;
            this.labelCertificateFile.Text = "(no file selected)";
            // 
            // radioButtonJWTX509
            // 
            this.radioButtonJWTX509.AutoSize = true;
            this.radioButtonJWTX509.Location = new System.Drawing.Point(19, 300);
            this.radioButtonJWTX509.Name = "radioButtonJWTX509";
            this.radioButtonJWTX509.Size = new System.Drawing.Size(315, 17);
            this.radioButtonJWTX509.TabIndex = 65;
            this.radioButtonJWTX509.Text = "JSON Web Token (JWT) - with user provided asymmetric key";
            this.radioButtonJWTX509.UseVisualStyleBackColor = true;
            this.radioButtonJWTX509.CheckedChanged += new System.EventHandler(this.radioButtonJWT_CheckedChanged);
            // 
            // radioButtonSWT
            // 
            this.radioButtonSWT.AutoSize = true;
            this.radioButtonSWT.Location = new System.Drawing.Point(19, 254);
            this.radioButtonSWT.Name = "radioButtonSWT";
            this.radioButtonSWT.Size = new System.Drawing.Size(319, 17);
            this.radioButtonSWT.TabIndex = 64;
            this.radioButtonSWT.Text = "Simple Web Token (SWT) - with autogenerated symmetric key";
            this.radioButtonSWT.UseVisualStyleBackColor = true;
            this.radioButtonSWT.CheckedChanged += new System.EventHandler(this.radioButtonSWT_CheckedChanged);
            // 
            // dataGridViewTokenClaims
            // 
            this.dataGridViewTokenClaims.AllowUserToAddRows = false;
            this.dataGridViewTokenClaims.AllowUserToDeleteRows = false;
            this.dataGridViewTokenClaims.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewTokenClaims.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTokenClaims.Location = new System.Drawing.Point(16, 113);
            this.dataGridViewTokenClaims.Name = "dataGridViewTokenClaims";
            this.dataGridViewTokenClaims.RowHeadersVisible = false;
            this.dataGridViewTokenClaims.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewTokenClaims.Size = new System.Drawing.Size(516, 93);
            this.dataGridViewTokenClaims.TabIndex = 53;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 97);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 13);
            this.label4.TabIndex = 63;
            this.label4.Text = "Other claims :";
            // 
            // buttonDelClaim
            // 
            this.buttonDelClaim.Location = new System.Drawing.Point(100, 212);
            this.buttonDelClaim.Name = "buttonDelClaim";
            this.buttonDelClaim.Size = new System.Drawing.Size(75, 23);
            this.buttonDelClaim.TabIndex = 62;
            this.buttonDelClaim.Text = "Delete";
            this.buttonDelClaim.UseVisualStyleBackColor = true;
            this.buttonDelClaim.Click += new System.EventHandler(this.buttonDelClaim_Click);
            // 
            // buttonAddClaim
            // 
            this.buttonAddClaim.Location = new System.Drawing.Point(19, 212);
            this.buttonAddClaim.Name = "buttonAddClaim";
            this.buttonAddClaim.Size = new System.Drawing.Size(75, 23);
            this.buttonAddClaim.TabIndex = 61;
            this.buttonAddClaim.Text = "Add";
            this.buttonAddClaim.UseVisualStyleBackColor = true;
            this.buttonAddClaim.Click += new System.EventHandler(this.buttonAddClaim_Click);
            // 
            // textBoxIssuer
            // 
            this.textBoxIssuer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxIssuer.Location = new System.Drawing.Point(203, 8);
            this.textBoxIssuer.Name = "textBoxIssuer";
            this.textBoxIssuer.Size = new System.Drawing.Size(329, 20);
            this.textBoxIssuer.TabIndex = 56;
            this.textBoxIssuer.Text = "http://testacs.com";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(178, 13);
            this.label3.TabIndex = 59;
            this.label3.Text = "Audience or scope of the token Url :";
            // 
            // textBoxAudience
            // 
            this.textBoxAudience.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxAudience.Location = new System.Drawing.Point(203, 38);
            this.textBoxAudience.Name = "textBoxAudience";
            this.textBoxAudience.Size = new System.Drawing.Size(329, 20);
            this.textBoxAudience.TabIndex = 57;
            this.textBoxAudience.Text = "urn:test";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 13);
            this.label2.TabIndex = 58;
            this.label2.Text = "Issuer of the token Url :";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonOk);
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Location = new System.Drawing.Point(-1, 615);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(586, 48);
            this.panel1.TabIndex = 51;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panelAutPol);
            this.groupBox1.Location = new System.Drawing.Point(13, 161);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(552, 432);
            this.groupBox1.TabIndex = 53;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Token properties";
            // 
            // labelStep
            // 
            this.labelStep.AutoSize = true;
            this.labelStep.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStep.Location = new System.Drawing.Point(22, 18);
            this.labelStep.Name = "labelStep";
            this.labelStep.Size = new System.Drawing.Size(399, 40);
            this.labelStep.TabIndex = 84;
            this.labelStep.Text = "Step {0}\r\nSpecify the content key Authorization Policy Option #{1}";
            // 
            // AddDynamicEncryptionFrame3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(584, 661);
            this.Controls.Add(this.labelStep);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBoxAuthPol);
            this.Name = "AddDynamicEncryptionFrame3";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Dynamic Encryption - Step {0}";
            this.Load += new System.EventHandler(this.SetupDynEnc_Load);
            this.groupBoxAuthPol.ResumeLayout(false);
            this.groupBoxAuthPol.PerformLayout();
            this.panelAutPol.ResumeLayout(false);
            this.panelAutPol.PerformLayout();
            this.panelJWT.ResumeLayout(false);
            this.panelJWT.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTokenClaims)).EndInit();
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
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
        private System.Windows.Forms.Panel panelAutPol;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
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
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelStep;
    }
}