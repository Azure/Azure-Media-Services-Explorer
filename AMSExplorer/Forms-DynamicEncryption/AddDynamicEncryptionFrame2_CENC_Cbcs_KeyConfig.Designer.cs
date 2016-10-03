namespace AMSExplorer
{
    partial class AddDynamicEncryptionFrame2_CENC_Cbcs_KeyConfig
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
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBoxCrypto = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxContentKey = new System.Windows.Forms.TextBox();
            this.buttongenerateContentKey = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.radioButtonContentKeyBase64 = new System.Windows.Forms.RadioButton();
            this.radioButtonContentKeyHex = new System.Windows.Forms.RadioButton();
            this.label10 = new System.Windows.Forms.Label();
            this.textBoxkeyid = new System.Windows.Forms.TextBox();
            this.buttonGenKeyID = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.radioButtonKeyIDGuid = new System.Windows.Forms.RadioButton();
            this.radioButtonKeyIDBase64 = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label9 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.panel1.SuspendLayout();
            this.groupBoxCrypto.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
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
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label3.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label3.Location = new System.Drawing.Point(19, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(575, 22);
            this.label3.TabIndex = 68;
            this.label3.Text = "Explorer will use the existing CENC cbcs key attached to the asset. If there is n" +
    "one, a key must be created :";
            // 
            // groupBoxCrypto
            // 
            this.groupBoxCrypto.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxCrypto.Controls.Add(this.label2);
            this.groupBoxCrypto.Controls.Add(this.label1);
            this.groupBoxCrypto.Controls.Add(this.textBoxContentKey);
            this.groupBoxCrypto.Controls.Add(this.buttongenerateContentKey);
            this.groupBoxCrypto.Controls.Add(this.panel3);
            this.groupBoxCrypto.Controls.Add(this.label3);
            this.groupBoxCrypto.Controls.Add(this.label10);
            this.groupBoxCrypto.Controls.Add(this.textBoxkeyid);
            this.groupBoxCrypto.Controls.Add(this.buttonGenKeyID);
            this.groupBoxCrypto.Controls.Add(this.panel4);
            this.groupBoxCrypto.Controls.Add(this.label5);
            this.groupBoxCrypto.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxCrypto.Location = new System.Drawing.Point(17, 119);
            this.groupBoxCrypto.Name = "groupBoxCrypto";
            this.groupBoxCrypto.Size = new System.Drawing.Size(640, 255);
            this.groupBoxCrypto.TabIndex = 74;
            this.groupBoxCrypto.TabStop = false;
            this.groupBoxCrypto.Text = "CENC cbcs Content Key";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label2.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label2.Location = new System.Drawing.Point(102, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(267, 15);
            this.label2.TabIndex = 92;
            this.label2.Text = "If empty, it will be automatically generated";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label1.Location = new System.Drawing.Point(19, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 15);
            this.label1.TabIndex = 88;
            this.label1.Text = "Content key :";
            // 
            // textBoxContentKey
            // 
            this.textBoxContentKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxContentKey.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBoxContentKey.Location = new System.Drawing.Point(22, 103);
            this.textBoxContentKey.Name = "textBoxContentKey";
            this.textBoxContentKey.Size = new System.Drawing.Size(416, 23);
            this.textBoxContentKey.TabIndex = 89;
            // 
            // buttongenerateContentKey
            // 
            this.buttongenerateContentKey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttongenerateContentKey.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.buttongenerateContentKey.Location = new System.Drawing.Point(549, 97);
            this.buttongenerateContentKey.Name = "buttongenerateContentKey";
            this.buttongenerateContentKey.Size = new System.Drawing.Size(75, 29);
            this.buttongenerateContentKey.TabIndex = 90;
            this.buttongenerateContentKey.Text = "Generate";
            this.buttongenerateContentKey.UseVisualStyleBackColor = true;
            this.buttongenerateContentKey.Click += new System.EventHandler(this.buttongenerateContentKey_Click);
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.radioButtonContentKeyBase64);
            this.panel3.Controls.Add(this.radioButtonContentKeyHex);
            this.panel3.Location = new System.Drawing.Point(455, 85);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(87, 55);
            this.panel3.TabIndex = 91;
            // 
            // radioButtonContentKeyBase64
            // 
            this.radioButtonContentKeyBase64.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonContentKeyBase64.AutoSize = true;
            this.radioButtonContentKeyBase64.Checked = true;
            this.radioButtonContentKeyBase64.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.radioButtonContentKeyBase64.Location = new System.Drawing.Point(9, 7);
            this.radioButtonContentKeyBase64.Name = "radioButtonContentKeyBase64";
            this.radioButtonContentKeyBase64.Size = new System.Drawing.Size(61, 19);
            this.radioButtonContentKeyBase64.TabIndex = 68;
            this.radioButtonContentKeyBase64.TabStop = true;
            this.radioButtonContentKeyBase64.Text = "Base64";
            this.radioButtonContentKeyBase64.UseVisualStyleBackColor = true;
            this.radioButtonContentKeyBase64.CheckedChanged += new System.EventHandler(this.radioButtonContentKeyBase64_CheckedChanged_1);
            // 
            // radioButtonContentKeyHex
            // 
            this.radioButtonContentKeyHex.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonContentKeyHex.AutoSize = true;
            this.radioButtonContentKeyHex.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.radioButtonContentKeyHex.Location = new System.Drawing.Point(9, 32);
            this.radioButtonContentKeyHex.Name = "radioButtonContentKeyHex";
            this.radioButtonContentKeyHex.Size = new System.Drawing.Size(45, 19);
            this.radioButtonContentKeyHex.TabIndex = 69;
            this.radioButtonContentKeyHex.Text = "Hex";
            this.radioButtonContentKeyHex.UseVisualStyleBackColor = true;
            this.radioButtonContentKeyHex.CheckedChanged += new System.EventHandler(this.radioButtonContentKeyHex_CheckedChanged_1);
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label10.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label10.Location = new System.Drawing.Point(71, 168);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(267, 15);
            this.label10.TabIndex = 69;
            this.label10.Text = "If empty, it will be automatically generated";
            // 
            // textBoxkeyid
            // 
            this.textBoxkeyid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxkeyid.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBoxkeyid.Location = new System.Drawing.Point(22, 186);
            this.textBoxkeyid.Name = "textBoxkeyid";
            this.textBoxkeyid.Size = new System.Drawing.Size(416, 23);
            this.textBoxkeyid.TabIndex = 32;
            this.textBoxkeyid.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // buttonGenKeyID
            // 
            this.buttonGenKeyID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGenKeyID.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.buttonGenKeyID.Location = new System.Drawing.Point(549, 183);
            this.buttonGenKeyID.Name = "buttonGenKeyID";
            this.buttonGenKeyID.Size = new System.Drawing.Size(75, 29);
            this.buttonGenKeyID.TabIndex = 33;
            this.buttonGenKeyID.Text = "Generate";
            this.buttonGenKeyID.UseVisualStyleBackColor = true;
            this.buttonGenKeyID.Click += new System.EventHandler(this.buttonGenKeyID_Click_1);
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.Controls.Add(this.radioButtonKeyIDGuid);
            this.panel4.Controls.Add(this.radioButtonKeyIDBase64);
            this.panel4.Location = new System.Drawing.Point(458, 168);
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
            this.radioButtonKeyIDGuid.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.radioButtonKeyIDGuid.Location = new System.Drawing.Point(9, 3);
            this.radioButtonKeyIDGuid.Name = "radioButtonKeyIDGuid";
            this.radioButtonKeyIDGuid.Size = new System.Drawing.Size(50, 19);
            this.radioButtonKeyIDGuid.TabIndex = 68;
            this.radioButtonKeyIDGuid.TabStop = true;
            this.radioButtonKeyIDGuid.Text = "Guid";
            this.radioButtonKeyIDGuid.UseVisualStyleBackColor = true;
            this.radioButtonKeyIDGuid.CheckedChanged += new System.EventHandler(this.radioButtonKeyIDGuid_CheckedChanged);
            // 
            // radioButtonKeyIDBase64
            // 
            this.radioButtonKeyIDBase64.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonKeyIDBase64.AutoSize = true;
            this.radioButtonKeyIDBase64.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.radioButtonKeyIDBase64.Location = new System.Drawing.Point(9, 29);
            this.radioButtonKeyIDBase64.Name = "radioButtonKeyIDBase64";
            this.radioButtonKeyIDBase64.Size = new System.Drawing.Size(61, 19);
            this.radioButtonKeyIDBase64.TabIndex = 69;
            this.radioButtonKeyIDBase64.Text = "Base64";
            this.radioButtonKeyIDBase64.UseVisualStyleBackColor = true;
            this.radioButtonKeyIDBase64.CheckedChanged += new System.EventHandler(this.radioButtonKeyIDBase64_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label5.Location = new System.Drawing.Point(19, 168);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 15);
            this.label5.TabIndex = 31;
            this.label5.Text = "Key ID :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.label9.ForeColor = System.Drawing.Color.DarkBlue;
            this.label9.Location = new System.Drawing.Point(26, 21);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(267, 42);
            this.label9.TabIndex = 83;
            this.label9.Text = "Step 2\r\nSpecify the CENC cbcs Content Key";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // AddDynamicEncryptionFrame2_CENC_Cbcs_KeyConfig
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(681, 741);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.groupBoxCrypto);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "AddDynamicEncryptionFrame2_CENC_Cbcs_KeyConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Dynamic Encryption - Step 2";
            this.Load += new System.EventHandler(this.AddDynamicEncryptionFrame2_CENC_Cbcs_KeyConfig_Load);
            this.panel1.ResumeLayout(false);
            this.groupBoxCrypto.ResumeLayout(false);
            this.groupBoxCrypto.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.Button buttonOk;
        public System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBoxCrypto;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBoxkeyid;
        private System.Windows.Forms.Button buttonGenKeyID;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.RadioButton radioButtonKeyIDGuid;
        private System.Windows.Forms.RadioButton radioButtonKeyIDBase64;
        public System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxContentKey;
        private System.Windows.Forms.Button buttongenerateContentKey;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton radioButtonContentKeyBase64;
        private System.Windows.Forms.RadioButton radioButtonContentKeyHex;
    }
}