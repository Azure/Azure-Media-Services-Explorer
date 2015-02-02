namespace AMSExplorer
{
    partial class AddDynamicEncryptionFrame2
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
            this.radioButtonNoAuthPolicy = new System.Windows.Forms.RadioButton();
            this.panelAutPol = new System.Windows.Forms.Panel();
            this.labelCertificateFile = new System.Windows.Forms.Label();
            this.buttonImportPFX = new System.Windows.Forms.Button();
            this.radioButtonJWT = new System.Windows.Forms.RadioButton();
            this.radioButtonSWT = new System.Windows.Forms.RadioButton();
            this.dataGridViewTokenClaims = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonDelClaim = new System.Windows.Forms.Button();
            this.buttonAddClaim = new System.Windows.Forms.Button();
            this.textBoxIssuer = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxAudience = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.radioButtonTokenAuthPolicy = new System.Windows.Forms.RadioButton();
            this.radioButtonOpenAuthPolicy = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.openFileDialogCert = new System.Windows.Forms.OpenFileDialog();
            this.groupBoxAuthPol.SuspendLayout();
            this.panelAutPol.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTokenClaims)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(331, 11);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(151, 23);
            this.buttonOk.TabIndex = 17;
            this.buttonOk.Text = "Next";
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
            this.groupBoxAuthPol.Controls.Add(this.radioButtonNoAuthPolicy);
            this.groupBoxAuthPol.Controls.Add(this.panelAutPol);
            this.groupBoxAuthPol.Controls.Add(this.radioButtonTokenAuthPolicy);
            this.groupBoxAuthPol.Controls.Add(this.radioButtonOpenAuthPolicy);
            this.groupBoxAuthPol.Location = new System.Drawing.Point(12, 53);
            this.groupBoxAuthPol.Name = "groupBoxAuthPol";
            this.groupBoxAuthPol.Size = new System.Drawing.Size(553, 487);
            this.groupBoxAuthPol.TabIndex = 47;
            this.groupBoxAuthPol.TabStop = false;
            this.groupBoxAuthPol.Text = "Content key\'s authorization policy";
            // 
            // radioButtonNoAuthPolicy
            // 
            this.radioButtonNoAuthPolicy.AutoSize = true;
            this.radioButtonNoAuthPolicy.Location = new System.Drawing.Point(35, 28);
            this.radioButtonNoAuthPolicy.Name = "radioButtonNoAuthPolicy";
            this.radioButtonNoAuthPolicy.Size = new System.Drawing.Size(340, 17);
            this.radioButtonNoAuthPolicy.TabIndex = 61;
            this.radioButtonNoAuthPolicy.Text = "None - An external PlayReady server is used to deliver the licenses\r\n";
            this.radioButtonNoAuthPolicy.UseVisualStyleBackColor = true;
            this.radioButtonNoAuthPolicy.CheckedChanged += new System.EventHandler(this.radioButtonNoAuthPolicy_CheckedChanged);
            // 
            // panelAutPol
            // 
            this.panelAutPol.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelAutPol.Controls.Add(this.labelCertificateFile);
            this.panelAutPol.Controls.Add(this.buttonImportPFX);
            this.panelAutPol.Controls.Add(this.radioButtonJWT);
            this.panelAutPol.Controls.Add(this.radioButtonSWT);
            this.panelAutPol.Controls.Add(this.dataGridViewTokenClaims);
            this.panelAutPol.Controls.Add(this.label4);
            this.panelAutPol.Controls.Add(this.buttonDelClaim);
            this.panelAutPol.Controls.Add(this.buttonAddClaim);
            this.panelAutPol.Controls.Add(this.textBoxIssuer);
            this.panelAutPol.Controls.Add(this.label3);
            this.panelAutPol.Controls.Add(this.textBoxAudience);
            this.panelAutPol.Controls.Add(this.label2);
            this.panelAutPol.Enabled = false;
            this.panelAutPol.Location = new System.Drawing.Point(35, 90);
            this.panelAutPol.Name = "panelAutPol";
            this.panelAutPol.Size = new System.Drawing.Size(508, 369);
            this.panelAutPol.TabIndex = 60;
            // 
            // labelCertificateFile
            // 
            this.labelCertificateFile.Location = new System.Drawing.Point(283, 306);
            this.labelCertificateFile.Name = "labelCertificateFile";
            this.labelCertificateFile.Size = new System.Drawing.Size(217, 18);
            this.labelCertificateFile.TabIndex = 67;
            this.labelCertificateFile.Text = "(no file selected)";
            // 
            // buttonImportPFX
            // 
            this.buttonImportPFX.Location = new System.Drawing.Point(82, 301);
            this.buttonImportPFX.Name = "buttonImportPFX";
            this.buttonImportPFX.Size = new System.Drawing.Size(195, 23);
            this.buttonImportPFX.TabIndex = 66;
            this.buttonImportPFX.Text = "Import X509 Certificate (.PFX)...";
            this.buttonImportPFX.UseVisualStyleBackColor = true;
            this.buttonImportPFX.Click += new System.EventHandler(this.buttonImportPFX_Click);
            // 
            // radioButtonJWT
            // 
            this.radioButtonJWT.AutoSize = true;
            this.radioButtonJWT.Location = new System.Drawing.Point(19, 277);
            this.radioButtonJWT.Name = "radioButtonJWT";
            this.radioButtonJWT.Size = new System.Drawing.Size(139, 17);
            this.radioButtonJWT.TabIndex = 65;
            this.radioButtonJWT.Text = "Json Web Token (JWT)";
            this.radioButtonJWT.UseVisualStyleBackColor = true;
            // 
            // radioButtonSWT
            // 
            this.radioButtonSWT.AutoSize = true;
            this.radioButtonSWT.Checked = true;
            this.radioButtonSWT.Location = new System.Drawing.Point(19, 254);
            this.radioButtonSWT.Name = "radioButtonSWT";
            this.radioButtonSWT.Size = new System.Drawing.Size(150, 17);
            this.radioButtonSWT.TabIndex = 64;
            this.radioButtonSWT.TabStop = true;
            this.radioButtonSWT.Text = "Simple Web Token (SWT)";
            this.radioButtonSWT.UseVisualStyleBackColor = true;
            // 
            // dataGridViewTokenClaims
            // 
            this.dataGridViewTokenClaims.AllowUserToAddRows = false;
            this.dataGridViewTokenClaims.AllowUserToDeleteRows = false;
            this.dataGridViewTokenClaims.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewTokenClaims.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTokenClaims.Location = new System.Drawing.Point(19, 84);
            this.dataGridViewTokenClaims.Name = "dataGridViewTokenClaims";
            this.dataGridViewTokenClaims.RowHeadersVisible = false;
            this.dataGridViewTokenClaims.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewTokenClaims.Size = new System.Drawing.Size(481, 124);
            this.dataGridViewTokenClaims.TabIndex = 53;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 63;
            this.label4.Text = "Claims :";
            // 
            // buttonDelClaim
            // 
            this.buttonDelClaim.Location = new System.Drawing.Point(100, 214);
            this.buttonDelClaim.Name = "buttonDelClaim";
            this.buttonDelClaim.Size = new System.Drawing.Size(75, 23);
            this.buttonDelClaim.TabIndex = 62;
            this.buttonDelClaim.Text = "Delete";
            this.buttonDelClaim.UseVisualStyleBackColor = true;
            this.buttonDelClaim.Click += new System.EventHandler(this.buttonDelClaim_Click);
            // 
            // buttonAddClaim
            // 
            this.buttonAddClaim.Location = new System.Drawing.Point(19, 214);
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
            this.textBoxIssuer.Size = new System.Drawing.Size(297, 20);
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
            this.textBoxAudience.Size = new System.Drawing.Size(297, 20);
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
            // radioButtonTokenAuthPolicy
            // 
            this.radioButtonTokenAuthPolicy.AutoSize = true;
            this.radioButtonTokenAuthPolicy.Location = new System.Drawing.Point(35, 74);
            this.radioButtonTokenAuthPolicy.Name = "radioButtonTokenAuthPolicy";
            this.radioButtonTokenAuthPolicy.Size = new System.Drawing.Size(56, 17);
            this.radioButtonTokenAuthPolicy.TabIndex = 55;
            this.radioButtonTokenAuthPolicy.Text = "Token";
            this.radioButtonTokenAuthPolicy.UseVisualStyleBackColor = true;
            this.radioButtonTokenAuthPolicy.CheckedChanged += new System.EventHandler(this.radioButtonToken_CheckedChanged);
            // 
            // radioButtonOpenAuthPolicy
            // 
            this.radioButtonOpenAuthPolicy.AutoSize = true;
            this.radioButtonOpenAuthPolicy.Checked = true;
            this.radioButtonOpenAuthPolicy.Location = new System.Drawing.Point(35, 51);
            this.radioButtonOpenAuthPolicy.Name = "radioButtonOpenAuthPolicy";
            this.radioButtonOpenAuthPolicy.Size = new System.Drawing.Size(51, 17);
            this.radioButtonOpenAuthPolicy.TabIndex = 54;
            this.radioButtonOpenAuthPolicy.TabStop = true;
            this.radioButtonOpenAuthPolicy.Text = "Open";
            this.radioButtonOpenAuthPolicy.UseVisualStyleBackColor = true;
            this.radioButtonOpenAuthPolicy.CheckedChanged += new System.EventHandler(this.radioButtonOpen_CheckedChanged);
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
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(217, 26);
            this.label5.TabIndex = 52;
            this.label5.Text = "Step 2 \r\nPlease configure the key authorization policy";
            // 
            // openFileDialogCert
            // 
            this.openFileDialogCert.DefaultExt = "PFX";
            this.openFileDialogCert.DereferenceLinks = false;
            this.openFileDialogCert.Filter = "PFX files|*.pfx|All files|*.*";
            // 
            // AddDynamicEncryptionFrame2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(584, 661);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBoxAuthPol);
            this.Name = "AddDynamicEncryptionFrame2";
            this.Text = "Dynamic Encryption - Step 2";
            this.Load += new System.EventHandler(this.SetupDynEnc_Load);
            this.groupBoxAuthPol.ResumeLayout(false);
            this.groupBoxAuthPol.PerformLayout();
            this.panelAutPol.ResumeLayout(false);
            this.panelAutPol.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTokenClaims)).EndInit();
            this.panel1.ResumeLayout(false);
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
        private System.Windows.Forms.RadioButton radioButtonNoAuthPolicy;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonDelClaim;
        private System.Windows.Forms.Button buttonAddClaim;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dataGridViewTokenClaims;
        private System.Windows.Forms.RadioButton radioButtonJWT;
        private System.Windows.Forms.RadioButton radioButtonSWT;
        private System.Windows.Forms.Label labelCertificateFile;
        private System.Windows.Forms.Button buttonImportPFX;
        private System.Windows.Forms.OpenFileDialog openFileDialogCert;
    }
}