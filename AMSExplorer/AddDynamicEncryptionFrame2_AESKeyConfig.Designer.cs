namespace AMSExplorer
{
    partial class AddDynamicEncryptionFrame2_AESKeyConfig
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
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttongenerateContentKey = new System.Windows.Forms.Button();
            this.textBoxcontentkey = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonOk = new System.Windows.Forms.Button();
            this.radioButtonContentKeyBase64 = new System.Windows.Forms.RadioButton();
            this.radioButtonContentKeyHex = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.radioButtonKeySpecifiedByUser = new System.Windows.Forms.RadioButton();
            this.radioButtonKeyRandomGeneration = new System.Windows.Forms.RadioButton();
            this.groupBoxCrypto = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxkeyid = new System.Windows.Forms.TextBox();
            this.buttonGenKeyID = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.radioButtonKeyIDGuid = new System.Windows.Forms.RadioButton();
            this.radioButtonKeyIDBase64 = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBoxCrypto.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(567, 13);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(107, 27);
            this.buttonCancel.TabIndex = 16;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttongenerateContentKey
            // 
            this.buttongenerateContentKey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttongenerateContentKey.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.buttongenerateContentKey.Location = new System.Drawing.Point(553, 136);
            this.buttongenerateContentKey.Name = "buttongenerateContentKey";
            this.buttongenerateContentKey.Size = new System.Drawing.Size(75, 29);
            this.buttongenerateContentKey.TabIndex = 40;
            this.buttongenerateContentKey.Text = "Generate";
            this.buttongenerateContentKey.UseVisualStyleBackColor = true;
            this.buttongenerateContentKey.Click += new System.EventHandler(this.buttongenerateContentKey_Click);
            // 
            // textBoxcontentkey
            // 
            this.textBoxcontentkey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxcontentkey.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBoxcontentkey.Location = new System.Drawing.Point(16, 131);
            this.textBoxcontentkey.Name = "textBoxcontentkey";
            this.textBoxcontentkey.Size = new System.Drawing.Size(438, 23);
            this.textBoxcontentkey.TabIndex = 35;
            this.textBoxcontentkey.TextChanged += new System.EventHandler(this.textBoxcontentkey_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label4.Location = new System.Drawing.Point(13, 113);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 15);
            this.label4.TabIndex = 34;
            this.label4.Text = "Content key:";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonOk);
            this.panel1.Location = new System.Drawing.Point(-1, 688);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(684, 55);
            this.panel1.TabIndex = 63;
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonOk.Location = new System.Drawing.Point(384, 13);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(176, 27);
            this.buttonOk.TabIndex = 17;
            this.buttonOk.Tag = "";
            this.buttonOk.Text = "Next";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // radioButtonContentKeyBase64
            // 
            this.radioButtonContentKeyBase64.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonContentKeyBase64.AutoSize = true;
            this.radioButtonContentKeyBase64.Checked = true;
            this.radioButtonContentKeyBase64.Location = new System.Drawing.Point(9, 3);
            this.radioButtonContentKeyBase64.Name = "radioButtonContentKeyBase64";
            this.radioButtonContentKeyBase64.Size = new System.Drawing.Size(61, 19);
            this.radioButtonContentKeyBase64.TabIndex = 68;
            this.radioButtonContentKeyBase64.TabStop = true;
            this.radioButtonContentKeyBase64.Text = "Base64";
            this.radioButtonContentKeyBase64.UseVisualStyleBackColor = true;
            this.radioButtonContentKeyBase64.CheckedChanged += new System.EventHandler(this.radioButtonGuid_CheckedChanged);
            // 
            // radioButtonContentKeyHex
            // 
            this.radioButtonContentKeyHex.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonContentKeyHex.AutoSize = true;
            this.radioButtonContentKeyHex.Location = new System.Drawing.Point(9, 29);
            this.radioButtonContentKeyHex.Name = "radioButtonContentKeyHex";
            this.radioButtonContentKeyHex.Size = new System.Drawing.Size(45, 19);
            this.radioButtonContentKeyHex.TabIndex = 69;
            this.radioButtonContentKeyHex.Text = "Hex";
            this.radioButtonContentKeyHex.UseVisualStyleBackColor = true;
            this.radioButtonContentKeyHex.CheckedChanged += new System.EventHandler(this.radioButtonHex_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.radioButtonContentKeyBase64);
            this.panel2.Controls.Add(this.radioButtonContentKeyHex);
            this.panel2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.panel2.Location = new System.Drawing.Point(462, 121);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(87, 55);
            this.panel2.TabIndex = 70;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.radioButtonKeySpecifiedByUser);
            this.groupBox2.Controls.Add(this.radioButtonKeyRandomGeneration);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(14, 114);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(644, 115);
            this.groupBox2.TabIndex = 73;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "AES Key Generation";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label3.Location = new System.Drawing.Point(36, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(575, 22);
            this.label3.TabIndex = 68;
            this.label3.Text = "Explorer will use the existing AES key attached to the asset. If there is none, a" +
    " key must be created :";
            // 
            // radioButtonKeySpecifiedByUser
            // 
            this.radioButtonKeySpecifiedByUser.AutoSize = true;
            this.radioButtonKeySpecifiedByUser.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonKeySpecifiedByUser.Location = new System.Drawing.Point(40, 81);
            this.radioButtonKeySpecifiedByUser.Name = "radioButtonKeySpecifiedByUser";
            this.radioButtonKeySpecifiedByUser.Size = new System.Drawing.Size(134, 19);
            this.radioButtonKeySpecifiedByUser.TabIndex = 1;
            this.radioButtonKeySpecifiedByUser.Text = "Specified by the user";
            this.radioButtonKeySpecifiedByUser.UseVisualStyleBackColor = true;
            // 
            // radioButtonKeyRandomGeneration
            // 
            this.radioButtonKeyRandomGeneration.AutoSize = true;
            this.radioButtonKeyRandomGeneration.Checked = true;
            this.radioButtonKeyRandomGeneration.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonKeyRandomGeneration.Location = new System.Drawing.Point(40, 54);
            this.radioButtonKeyRandomGeneration.Name = "radioButtonKeyRandomGeneration";
            this.radioButtonKeyRandomGeneration.Size = new System.Drawing.Size(259, 19);
            this.radioButtonKeyRandomGeneration.TabIndex = 0;
            this.radioButtonKeyRandomGeneration.TabStop = true;
            this.radioButtonKeyRandomGeneration.Text = "Automatic generation (random content key)";
            this.radioButtonKeyRandomGeneration.UseVisualStyleBackColor = true;
            this.radioButtonKeyRandomGeneration.CheckedChanged += new System.EventHandler(this.radioButtonKeyRandomGeneration_CheckedChanged);
            // 
            // groupBoxCrypto
            // 
            this.groupBoxCrypto.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxCrypto.Controls.Add(this.label2);
            this.groupBoxCrypto.Controls.Add(this.textBoxkeyid);
            this.groupBoxCrypto.Controls.Add(this.buttonGenKeyID);
            this.groupBoxCrypto.Controls.Add(this.label10);
            this.groupBoxCrypto.Controls.Add(this.label5);
            this.groupBoxCrypto.Controls.Add(this.panel4);
            this.groupBoxCrypto.Controls.Add(this.panel2);
            this.groupBoxCrypto.Controls.Add(this.buttongenerateContentKey);
            this.groupBoxCrypto.Controls.Add(this.textBoxcontentkey);
            this.groupBoxCrypto.Controls.Add(this.label4);
            this.groupBoxCrypto.Enabled = false;
            this.groupBoxCrypto.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxCrypto.Location = new System.Drawing.Point(14, 252);
            this.groupBoxCrypto.Name = "groupBoxCrypto";
            this.groupBoxCrypto.Size = new System.Drawing.Size(644, 317);
            this.groupBoxCrypto.TabIndex = 74;
            this.groupBoxCrypto.TabStop = false;
            this.groupBoxCrypto.Text = "Cryptography";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label2.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label2.Location = new System.Drawing.Point(187, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(267, 15);
            this.label2.TabIndex = 77;
            this.label2.Text = "If empty, it will be automatically generated";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textBoxkeyid
            // 
            this.textBoxkeyid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxkeyid.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBoxkeyid.Location = new System.Drawing.Point(15, 56);
            this.textBoxkeyid.Name = "textBoxkeyid";
            this.textBoxkeyid.Size = new System.Drawing.Size(439, 23);
            this.textBoxkeyid.TabIndex = 32;
            // 
            // buttonGenKeyID
            // 
            this.buttonGenKeyID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGenKeyID.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.buttonGenKeyID.Location = new System.Drawing.Point(553, 53);
            this.buttonGenKeyID.Name = "buttonGenKeyID";
            this.buttonGenKeyID.Size = new System.Drawing.Size(75, 29);
            this.buttonGenKeyID.TabIndex = 33;
            this.buttonGenKeyID.Text = "Generate";
            this.buttonGenKeyID.UseVisualStyleBackColor = true;
            this.buttonGenKeyID.Click += new System.EventHandler(this.buttonGenKeyID_Click);
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label10.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label10.Location = new System.Drawing.Point(187, 113);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(267, 15);
            this.label10.TabIndex = 76;
            this.label10.Text = "If empty, it will be automatically generated";
            this.label10.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label5.Location = new System.Drawing.Point(12, 38);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 15);
            this.label5.TabIndex = 31;
            this.label5.Text = "Key ID:";
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.Controls.Add(this.radioButtonKeyIDGuid);
            this.panel4.Controls.Add(this.radioButtonKeyIDBase64);
            this.panel4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.panel4.Location = new System.Drawing.Point(462, 38);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(87, 55);
            this.panel4.TabIndex = 72;
            // 
            // radioButtonKeyIDGuid
            // 
            this.radioButtonKeyIDGuid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonKeyIDGuid.AutoSize = true;
            this.radioButtonKeyIDGuid.Checked = true;
            this.radioButtonKeyIDGuid.Location = new System.Drawing.Point(9, 3);
            this.radioButtonKeyIDGuid.Name = "radioButtonKeyIDGuid";
            this.radioButtonKeyIDGuid.Size = new System.Drawing.Size(50, 19);
            this.radioButtonKeyIDGuid.TabIndex = 68;
            this.radioButtonKeyIDGuid.TabStop = true;
            this.radioButtonKeyIDGuid.Text = "Guid";
            this.radioButtonKeyIDGuid.UseVisualStyleBackColor = true;
            // 
            // radioButtonKeyIDBase64
            // 
            this.radioButtonKeyIDBase64.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonKeyIDBase64.AutoSize = true;
            this.radioButtonKeyIDBase64.Location = new System.Drawing.Point(9, 29);
            this.radioButtonKeyIDBase64.Name = "radioButtonKeyIDBase64";
            this.radioButtonKeyIDBase64.Size = new System.Drawing.Size(61, 19);
            this.radioButtonKeyIDBase64.TabIndex = 69;
            this.radioButtonKeyIDBase64.Text = "Base64";
            this.radioButtonKeyIDBase64.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.DarkBlue;
            this.label1.Location = new System.Drawing.Point(26, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(153, 42);
            this.label1.TabIndex = 75;
            this.label1.Text = "Step 2\r\nSpecify the AES key";
            // 
            // AddDynamicEncryptionFrame2_AESKeyConfig
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(681, 741);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBoxCrypto);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "AddDynamicEncryptionFrame2_AESKeyConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Dynamic Encryption - Step 2";
            this.Load += new System.EventHandler(this.PlayReadyExternalServer_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBoxCrypto.ResumeLayout(false);
            this.groupBoxCrypto.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button buttonCancel;
        public System.Windows.Forms.TextBox textBoxcontentkey;
        public System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttongenerateContentKey;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radioButtonContentKeyBase64;
        private System.Windows.Forms.RadioButton radioButtonContentKeyHex;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton radioButtonKeySpecifiedByUser;
        private System.Windows.Forms.RadioButton radioButtonKeyRandomGeneration;
        private System.Windows.Forms.GroupBox groupBoxCrypto;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Label label10;
        public System.Windows.Forms.TextBox textBoxkeyid;
        private System.Windows.Forms.Button buttonGenKeyID;
        public System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.RadioButton radioButtonKeyIDGuid;
        private System.Windows.Forms.RadioButton radioButtonKeyIDBase64;
        private System.Windows.Forms.Label label2;
    }
}