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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddDynamicEncryptionFrame2_CENC_Cbcs_KeyConfig));
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
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOk
            // 
            resources.ApplyResources(this.buttonOk, "buttonOk");
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Tag = "";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonOk);
            this.panel1.Name = "panel1";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label3.Name = "label3";
            // 
            // groupBoxCrypto
            // 
            resources.ApplyResources(this.groupBoxCrypto, "groupBoxCrypto");
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
            this.groupBoxCrypto.Name = "groupBoxCrypto";
            this.groupBoxCrypto.TabStop = false;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // textBoxContentKey
            // 
            resources.ApplyResources(this.textBoxContentKey, "textBoxContentKey");
            this.textBoxContentKey.Name = "textBoxContentKey";
            // 
            // buttongenerateContentKey
            // 
            resources.ApplyResources(this.buttongenerateContentKey, "buttongenerateContentKey");
            this.buttongenerateContentKey.Name = "buttongenerateContentKey";
            this.buttongenerateContentKey.UseVisualStyleBackColor = true;
            this.buttongenerateContentKey.Click += new System.EventHandler(this.buttongenerateContentKey_Click);
            // 
            // panel3
            // 
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Controls.Add(this.radioButtonContentKeyBase64);
            this.panel3.Controls.Add(this.radioButtonContentKeyHex);
            this.panel3.Name = "panel3";
            // 
            // radioButtonContentKeyBase64
            // 
            resources.ApplyResources(this.radioButtonContentKeyBase64, "radioButtonContentKeyBase64");
            this.radioButtonContentKeyBase64.Checked = true;
            this.radioButtonContentKeyBase64.Name = "radioButtonContentKeyBase64";
            this.radioButtonContentKeyBase64.TabStop = true;
            this.radioButtonContentKeyBase64.UseVisualStyleBackColor = true;
            this.radioButtonContentKeyBase64.CheckedChanged += new System.EventHandler(this.radioButtonContentKeyBase64_CheckedChanged_1);
            // 
            // radioButtonContentKeyHex
            // 
            resources.ApplyResources(this.radioButtonContentKeyHex, "radioButtonContentKeyHex");
            this.radioButtonContentKeyHex.Name = "radioButtonContentKeyHex";
            this.radioButtonContentKeyHex.UseVisualStyleBackColor = true;
            this.radioButtonContentKeyHex.CheckedChanged += new System.EventHandler(this.radioButtonContentKeyHex_CheckedChanged_1);
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label10.Name = "label10";
            // 
            // textBoxkeyid
            // 
            resources.ApplyResources(this.textBoxkeyid, "textBoxkeyid");
            this.textBoxkeyid.Name = "textBoxkeyid";
            this.textBoxkeyid.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // buttonGenKeyID
            // 
            resources.ApplyResources(this.buttonGenKeyID, "buttonGenKeyID");
            this.buttonGenKeyID.Name = "buttonGenKeyID";
            this.buttonGenKeyID.UseVisualStyleBackColor = true;
            this.buttonGenKeyID.Click += new System.EventHandler(this.buttonGenKeyID_Click_1);
            // 
            // panel4
            // 
            resources.ApplyResources(this.panel4, "panel4");
            this.panel4.Controls.Add(this.radioButtonKeyIDGuid);
            this.panel4.Controls.Add(this.radioButtonKeyIDBase64);
            this.panel4.Name = "panel4";
            // 
            // radioButtonKeyIDGuid
            // 
            resources.ApplyResources(this.radioButtonKeyIDGuid, "radioButtonKeyIDGuid");
            this.radioButtonKeyIDGuid.Checked = true;
            this.radioButtonKeyIDGuid.Name = "radioButtonKeyIDGuid";
            this.radioButtonKeyIDGuid.TabStop = true;
            this.radioButtonKeyIDGuid.UseVisualStyleBackColor = true;
            this.radioButtonKeyIDGuid.CheckedChanged += new System.EventHandler(this.radioButtonKeyIDGuid_CheckedChanged);
            // 
            // radioButtonKeyIDBase64
            // 
            resources.ApplyResources(this.radioButtonKeyIDBase64, "radioButtonKeyIDBase64");
            this.radioButtonKeyIDBase64.Name = "radioButtonKeyIDBase64";
            this.radioButtonKeyIDBase64.UseVisualStyleBackColor = true;
            this.radioButtonKeyIDBase64.CheckedChanged += new System.EventHandler(this.radioButtonKeyIDBase64_CheckedChanged);
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.ForeColor = System.Drawing.Color.DarkBlue;
            this.label9.Name = "label9";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // AddDynamicEncryptionFrame2_CENC_Cbcs_KeyConfig
            // 
            this.AcceptButton = this.buttonOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.label9);
            this.Controls.Add(this.groupBoxCrypto);
            this.Controls.Add(this.panel1);
            this.Name = "AddDynamicEncryptionFrame2_CENC_Cbcs_KeyConfig";
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