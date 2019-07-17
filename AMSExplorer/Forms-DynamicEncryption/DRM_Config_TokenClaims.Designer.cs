namespace AMSExplorer
{
    partial class DRM_Config_TokenClaims
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DRM_Config_TokenClaims));
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
            this.checkBoxRequiresContentKeyIdentifierClaim = new System.Windows.Forms.CheckBox();
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
            resources.ApplyResources(this.buttonOk, "buttonOk");
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // groupBoxAuthPol
            // 
            resources.ApplyResources(this.groupBoxAuthPol, "groupBoxAuthPol");
            this.groupBoxAuthPol.Controls.Add(this.radioButtonTokenAuthPolicy);
            this.groupBoxAuthPol.Controls.Add(this.radioButtonOpenAuthPolicy);
            this.groupBoxAuthPol.Name = "groupBoxAuthPol";
            this.groupBoxAuthPol.TabStop = false;
            // 
            // radioButtonTokenAuthPolicy
            // 
            resources.ApplyResources(this.radioButtonTokenAuthPolicy, "radioButtonTokenAuthPolicy");
            this.radioButtonTokenAuthPolicy.Checked = true;
            this.radioButtonTokenAuthPolicy.Name = "radioButtonTokenAuthPolicy";
            this.radioButtonTokenAuthPolicy.TabStop = true;
            this.radioButtonTokenAuthPolicy.UseVisualStyleBackColor = true;
            this.radioButtonTokenAuthPolicy.CheckedChanged += new System.EventHandler(this.radioButtonToken_CheckedChanged);
            // 
            // radioButtonOpenAuthPolicy
            // 
            resources.ApplyResources(this.radioButtonOpenAuthPolicy, "radioButtonOpenAuthPolicy");
            this.radioButtonOpenAuthPolicy.Name = "radioButtonOpenAuthPolicy";
            this.radioButtonOpenAuthPolicy.UseVisualStyleBackColor = true;
            this.radioButtonOpenAuthPolicy.CheckedChanged += new System.EventHandler(this.radioButtonOpen_CheckedChanged);
            // 
            // panelSymKey
            // 
            this.panelSymKey.Controls.Add(this.label1);
            this.panelSymKey.Controls.Add(this.panel2);
            this.panelSymKey.Controls.Add(this.textBoxSymKey);
            this.panelSymKey.Controls.Add(this.buttongenerateContentKey);
            resources.ApplyResources(this.panelSymKey, "panelSymKey");
            this.panelSymKey.Name = "panelSymKey";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // panel2
            // 
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Controls.Add(this.radioButtonContentKeyBase64);
            this.panel2.Controls.Add(this.radioButtonContentKeyHex);
            this.panel2.Name = "panel2";
            // 
            // radioButtonContentKeyBase64
            // 
            resources.ApplyResources(this.radioButtonContentKeyBase64, "radioButtonContentKeyBase64");
            this.radioButtonContentKeyBase64.Checked = true;
            this.radioButtonContentKeyBase64.Name = "radioButtonContentKeyBase64";
            this.radioButtonContentKeyBase64.TabStop = true;
            this.radioButtonContentKeyBase64.UseVisualStyleBackColor = true;
            this.radioButtonContentKeyBase64.CheckedChanged += new System.EventHandler(this.radioButtonContentKeyBase64_CheckedChanged);
            // 
            // radioButtonContentKeyHex
            // 
            resources.ApplyResources(this.radioButtonContentKeyHex, "radioButtonContentKeyHex");
            this.radioButtonContentKeyHex.Name = "radioButtonContentKeyHex";
            this.radioButtonContentKeyHex.UseVisualStyleBackColor = true;
            this.radioButtonContentKeyHex.CheckedChanged += new System.EventHandler(this.radioButtonContentKeyHex_CheckedChanged);
            // 
            // textBoxSymKey
            // 
            resources.ApplyResources(this.textBoxSymKey, "textBoxSymKey");
            this.textBoxSymKey.Name = "textBoxSymKey";
            // 
            // buttongenerateContentKey
            // 
            resources.ApplyResources(this.buttongenerateContentKey, "buttongenerateContentKey");
            this.buttongenerateContentKey.Name = "buttongenerateContentKey";
            this.buttongenerateContentKey.UseVisualStyleBackColor = true;
            this.buttongenerateContentKey.Click += new System.EventHandler(this.buttongenerateContentKey_Click);
            // 
            // checkBoxRequiresContentKeyIdentifierClaim
            // 
            resources.ApplyResources(this.checkBoxRequiresContentKeyIdentifierClaim, "checkBoxRequiresContentKeyIdentifierClaim");
            this.checkBoxRequiresContentKeyIdentifierClaim.Checked = true;
            this.checkBoxRequiresContentKeyIdentifierClaim.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxRequiresContentKeyIdentifierClaim.Name = "checkBoxRequiresContentKeyIdentifierClaim";
            this.checkBoxRequiresContentKeyIdentifierClaim.UseVisualStyleBackColor = true;
            // 
            // radioButtonJWTSymmetric
            // 
            resources.ApplyResources(this.radioButtonJWTSymmetric, "radioButtonJWTSymmetric");
            this.radioButtonJWTSymmetric.Checked = true;
            this.radioButtonJWTSymmetric.Name = "radioButtonJWTSymmetric";
            this.radioButtonJWTSymmetric.TabStop = true;
            this.radioButtonJWTSymmetric.UseVisualStyleBackColor = true;
            this.radioButtonJWTSymmetric.CheckedChanged += new System.EventHandler(this.radioButtonTokenType_CheckedChanged);
            // 
            // panelJWT
            // 
            this.panelJWT.Controls.Add(this.moreinfocGenX509);
            this.panelJWT.Controls.Add(this.buttonImportPFX);
            this.panelJWT.Controls.Add(this.labelCertificateFile);
            resources.ApplyResources(this.panelJWT, "panelJWT");
            this.panelJWT.Name = "panelJWT";
            // 
            // moreinfocGenX509
            // 
            resources.ApplyResources(this.moreinfocGenX509, "moreinfocGenX509");
            this.moreinfocGenX509.Name = "moreinfocGenX509";
            this.moreinfocGenX509.TabStop = true;
            // 
            // buttonImportPFX
            // 
            resources.ApplyResources(this.buttonImportPFX, "buttonImportPFX");
            this.buttonImportPFX.Name = "buttonImportPFX";
            this.buttonImportPFX.UseVisualStyleBackColor = true;
            this.buttonImportPFX.Click += new System.EventHandler(this.buttonImportPFX_Click);
            // 
            // labelCertificateFile
            // 
            resources.ApplyResources(this.labelCertificateFile, "labelCertificateFile");
            this.labelCertificateFile.Name = "labelCertificateFile";
            // 
            // radioButtonJWTX509
            // 
            resources.ApplyResources(this.radioButtonJWTX509, "radioButtonJWTX509");
            this.radioButtonJWTX509.Name = "radioButtonJWTX509";
            this.radioButtonJWTX509.UseVisualStyleBackColor = true;
            this.radioButtonJWTX509.CheckedChanged += new System.EventHandler(this.radioButtonTokenType_CheckedChanged);
            // 
            // radioButtonSWT
            // 
            resources.ApplyResources(this.radioButtonSWT, "radioButtonSWT");
            this.radioButtonSWT.Name = "radioButtonSWT";
            this.radioButtonSWT.UseVisualStyleBackColor = true;
            this.radioButtonSWT.CheckedChanged += new System.EventHandler(this.radioButtonTokenType_CheckedChanged);
            // 
            // dataGridViewTokenClaims
            // 
            this.dataGridViewTokenClaims.AllowUserToAddRows = false;
            this.dataGridViewTokenClaims.AllowUserToDeleteRows = false;
            resources.ApplyResources(this.dataGridViewTokenClaims, "dataGridViewTokenClaims");
            this.dataGridViewTokenClaims.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewTokenClaims.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTokenClaims.Name = "dataGridViewTokenClaims";
            this.dataGridViewTokenClaims.RowHeadersVisible = false;
            this.dataGridViewTokenClaims.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // buttonDelClaim
            // 
            resources.ApplyResources(this.buttonDelClaim, "buttonDelClaim");
            this.buttonDelClaim.Name = "buttonDelClaim";
            this.buttonDelClaim.UseVisualStyleBackColor = true;
            this.buttonDelClaim.Click += new System.EventHandler(this.buttonDelClaim_Click);
            // 
            // buttonAddClaim
            // 
            resources.ApplyResources(this.buttonAddClaim, "buttonAddClaim");
            this.buttonAddClaim.Name = "buttonAddClaim";
            this.buttonAddClaim.UseVisualStyleBackColor = true;
            this.buttonAddClaim.Click += new System.EventHandler(this.buttonAddClaim_Click);
            // 
            // textBoxIssuer
            // 
            resources.ApplyResources(this.textBoxIssuer, "textBoxIssuer");
            this.textBoxIssuer.Name = "textBoxIssuer";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // textBoxAudience
            // 
            resources.ApplyResources(this.textBoxAudience, "textBoxAudience");
            this.textBoxAudience.Name = "textBoxAudience";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonOk);
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Name = "panel1";
            // 
            // labelStep
            // 
            resources.ApplyResources(this.labelStep, "labelStep");
            this.labelStep.ForeColor = System.Drawing.Color.DarkBlue;
            this.labelStep.Name = "labelStep";
            // 
            // tabControlTokenType
            // 
            resources.ApplyResources(this.tabControlTokenType, "tabControlTokenType");
            this.tabControlTokenType.Controls.Add(this.tabPageTokenType);
            this.tabControlTokenType.Controls.Add(this.tabPageTokenSymmetric);
            this.tabControlTokenType.Controls.Add(this.tabPageTokenX509);
            this.tabControlTokenType.Controls.Add(this.tabPageOpenId);
            this.tabControlTokenType.Name = "tabControlTokenType";
            this.tabControlTokenType.SelectedIndex = 0;
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
            resources.ApplyResources(this.tabPageTokenType, "tabPageTokenType");
            this.tabPageTokenType.Name = "tabPageTokenType";
            this.tabPageTokenType.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.ForeColor = System.Drawing.SystemColors.WindowFrame;
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // radioButtonJWTOpenId
            // 
            resources.ApplyResources(this.radioButtonJWTOpenId, "radioButtonJWTOpenId");
            this.radioButtonJWTOpenId.Name = "radioButtonJWTOpenId";
            this.radioButtonJWTOpenId.UseVisualStyleBackColor = true;
            this.radioButtonJWTOpenId.CheckedChanged += new System.EventHandler(this.radioButtonTokenType_CheckedChanged);
            // 
            // label7
            // 
            this.label7.ForeColor = System.Drawing.SystemColors.WindowFrame;
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label6
            // 
            this.label6.ForeColor = System.Drawing.SystemColors.WindowFrame;
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label5
            // 
            this.label5.ForeColor = System.Drawing.SystemColors.WindowFrame;
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // tabPageTokenSymmetric
            // 
            this.tabPageTokenSymmetric.Controls.Add(this.panelSymKey);
            resources.ApplyResources(this.tabPageTokenSymmetric, "tabPageTokenSymmetric");
            this.tabPageTokenSymmetric.Name = "tabPageTokenSymmetric";
            this.tabPageTokenSymmetric.UseVisualStyleBackColor = true;
            // 
            // tabPageTokenX509
            // 
            this.tabPageTokenX509.Controls.Add(this.panelJWT);
            resources.ApplyResources(this.tabPageTokenX509, "tabPageTokenX509");
            this.tabPageTokenX509.Name = "tabPageTokenX509";
            this.tabPageTokenX509.UseVisualStyleBackColor = true;
            // 
            // tabPageOpenId
            // 
            this.tabPageOpenId.Controls.Add(this.comboBoxMappingList);
            this.tabPageOpenId.Controls.Add(this.buttonAddMapping);
            this.tabPageOpenId.Controls.Add(this.label9);
            this.tabPageOpenId.Controls.Add(this.textBoxOpenIdDocument);
            resources.ApplyResources(this.tabPageOpenId, "tabPageOpenId");
            this.tabPageOpenId.Name = "tabPageOpenId";
            this.tabPageOpenId.UseVisualStyleBackColor = true;
            // 
            // comboBoxMappingList
            // 
            resources.ApplyResources(this.comboBoxMappingList, "comboBoxMappingList");
            this.comboBoxMappingList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMappingList.FormattingEnabled = true;
            this.comboBoxMappingList.Name = "comboBoxMappingList";
            // 
            // buttonAddMapping
            // 
            resources.ApplyResources(this.buttonAddMapping, "buttonAddMapping");
            this.buttonAddMapping.Name = "buttonAddMapping";
            this.buttonAddMapping.UseVisualStyleBackColor = true;
            this.buttonAddMapping.Click += new System.EventHandler(this.buttonAddMapping_Click);
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // textBoxOpenIdDocument
            // 
            resources.ApplyResources(this.textBoxOpenIdDocument, "textBoxOpenIdDocument");
            this.textBoxOpenIdDocument.Name = "textBoxOpenIdDocument";
            // 
            // tabControlTokenProperties
            // 
            resources.ApplyResources(this.tabControlTokenProperties, "tabControlTokenProperties");
            this.tabControlTokenProperties.Controls.Add(this.tabPage2);
            this.tabControlTokenProperties.Controls.Add(this.tabPage3);
            this.tabControlTokenProperties.Name = "tabControlTokenProperties";
            this.tabControlTokenProperties.SelectedIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label11);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.textBoxAudience);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.textBoxIssuer);
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.ForeColor = System.Drawing.SystemColors.WindowFrame;
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // label10
            // 
            this.label10.ForeColor = System.Drawing.SystemColors.WindowFrame;
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Controls.Add(this.checkBoxRequiresContentKeyIdentifierClaim);
            this.tabPage3.Controls.Add(this.dataGridViewTokenClaims);
            this.tabPage3.Controls.Add(this.buttonAddClaim);
            this.tabPage3.Controls.Add(this.buttonDelClaim);
            resources.ApplyResources(this.tabPage3, "tabPage3");
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.ForeColor = System.Drawing.SystemColors.WindowFrame;
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // DRM_Config_TokenClaims
            // 
            this.AcceptButton = this.buttonOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.tabControlTokenProperties);
            this.Controls.Add(this.tabControlTokenType);
            this.Controls.Add(this.labelStep);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBoxAuthPol);
            this.Name = "DRM_Config_TokenClaims";
            this.Load += new System.EventHandler(this.DRM_Config_TokenClaims_Load);
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
        private System.Windows.Forms.CheckBox checkBoxRequiresContentKeyIdentifierClaim;
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