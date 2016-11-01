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
            this.errorProvider1.SetError(this.buttonCancel, resources.GetString("buttonCancel.Error"));
            this.errorProvider1.SetIconAlignment(this.buttonCancel, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("buttonCancel.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.buttonCancel, ((int)(resources.GetObject("buttonCancel.IconPadding"))));
            this.buttonCancel.Name = "buttonCancel";
            this.toolTip1.SetToolTip(this.buttonCancel, resources.GetString("buttonCancel.ToolTip"));
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOk
            // 
            resources.ApplyResources(this.buttonOk, "buttonOk");
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.errorProvider1.SetError(this.buttonOk, resources.GetString("buttonOk.Error"));
            this.errorProvider1.SetIconAlignment(this.buttonOk, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("buttonOk.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.buttonOk, ((int)(resources.GetObject("buttonOk.IconPadding"))));
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Tag = "";
            this.toolTip1.SetToolTip(this.buttonOk, resources.GetString("buttonOk.ToolTip"));
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonOk);
            this.errorProvider1.SetError(this.panel1, resources.GetString("panel1.Error"));
            this.errorProvider1.SetIconAlignment(this.panel1, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("panel1.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.panel1, ((int)(resources.GetObject("panel1.IconPadding"))));
            this.panel1.Name = "panel1";
            this.toolTip1.SetToolTip(this.panel1, resources.GetString("panel1.ToolTip"));
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.errorProvider1.SetError(this.label3, resources.GetString("label3.Error"));
            this.label3.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.errorProvider1.SetIconAlignment(this.label3, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label3.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label3, ((int)(resources.GetObject("label3.IconPadding"))));
            this.label3.Name = "label3";
            this.toolTip1.SetToolTip(this.label3, resources.GetString("label3.ToolTip"));
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
            this.errorProvider1.SetError(this.groupBoxCrypto, resources.GetString("groupBoxCrypto.Error"));
            this.errorProvider1.SetIconAlignment(this.groupBoxCrypto, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("groupBoxCrypto.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.groupBoxCrypto, ((int)(resources.GetObject("groupBoxCrypto.IconPadding"))));
            this.groupBoxCrypto.Name = "groupBoxCrypto";
            this.groupBoxCrypto.TabStop = false;
            this.toolTip1.SetToolTip(this.groupBoxCrypto, resources.GetString("groupBoxCrypto.ToolTip"));
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.errorProvider1.SetError(this.label2, resources.GetString("label2.Error"));
            this.label2.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.errorProvider1.SetIconAlignment(this.label2, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label2.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label2, ((int)(resources.GetObject("label2.IconPadding"))));
            this.label2.Name = "label2";
            this.toolTip1.SetToolTip(this.label2, resources.GetString("label2.ToolTip"));
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.errorProvider1.SetError(this.label1, resources.GetString("label1.Error"));
            this.errorProvider1.SetIconAlignment(this.label1, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label1.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label1, ((int)(resources.GetObject("label1.IconPadding"))));
            this.label1.Name = "label1";
            this.toolTip1.SetToolTip(this.label1, resources.GetString("label1.ToolTip"));
            // 
            // textBoxContentKey
            // 
            resources.ApplyResources(this.textBoxContentKey, "textBoxContentKey");
            this.errorProvider1.SetError(this.textBoxContentKey, resources.GetString("textBoxContentKey.Error"));
            this.errorProvider1.SetIconAlignment(this.textBoxContentKey, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("textBoxContentKey.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.textBoxContentKey, ((int)(resources.GetObject("textBoxContentKey.IconPadding"))));
            this.textBoxContentKey.Name = "textBoxContentKey";
            this.toolTip1.SetToolTip(this.textBoxContentKey, resources.GetString("textBoxContentKey.ToolTip"));
            // 
            // buttongenerateContentKey
            // 
            resources.ApplyResources(this.buttongenerateContentKey, "buttongenerateContentKey");
            this.errorProvider1.SetError(this.buttongenerateContentKey, resources.GetString("buttongenerateContentKey.Error"));
            this.errorProvider1.SetIconAlignment(this.buttongenerateContentKey, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("buttongenerateContentKey.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.buttongenerateContentKey, ((int)(resources.GetObject("buttongenerateContentKey.IconPadding"))));
            this.buttongenerateContentKey.Name = "buttongenerateContentKey";
            this.toolTip1.SetToolTip(this.buttongenerateContentKey, resources.GetString("buttongenerateContentKey.ToolTip"));
            this.buttongenerateContentKey.UseVisualStyleBackColor = true;
            this.buttongenerateContentKey.Click += new System.EventHandler(this.buttongenerateContentKey_Click);
            // 
            // panel3
            // 
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Controls.Add(this.radioButtonContentKeyBase64);
            this.panel3.Controls.Add(this.radioButtonContentKeyHex);
            this.errorProvider1.SetError(this.panel3, resources.GetString("panel3.Error"));
            this.errorProvider1.SetIconAlignment(this.panel3, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("panel3.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.panel3, ((int)(resources.GetObject("panel3.IconPadding"))));
            this.panel3.Name = "panel3";
            this.toolTip1.SetToolTip(this.panel3, resources.GetString("panel3.ToolTip"));
            // 
            // radioButtonContentKeyBase64
            // 
            resources.ApplyResources(this.radioButtonContentKeyBase64, "radioButtonContentKeyBase64");
            this.radioButtonContentKeyBase64.Checked = true;
            this.errorProvider1.SetError(this.radioButtonContentKeyBase64, resources.GetString("radioButtonContentKeyBase64.Error"));
            this.errorProvider1.SetIconAlignment(this.radioButtonContentKeyBase64, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("radioButtonContentKeyBase64.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.radioButtonContentKeyBase64, ((int)(resources.GetObject("radioButtonContentKeyBase64.IconPadding"))));
            this.radioButtonContentKeyBase64.Name = "radioButtonContentKeyBase64";
            this.radioButtonContentKeyBase64.TabStop = true;
            this.toolTip1.SetToolTip(this.radioButtonContentKeyBase64, resources.GetString("radioButtonContentKeyBase64.ToolTip"));
            this.radioButtonContentKeyBase64.UseVisualStyleBackColor = true;
            this.radioButtonContentKeyBase64.CheckedChanged += new System.EventHandler(this.radioButtonContentKeyBase64_CheckedChanged_1);
            // 
            // radioButtonContentKeyHex
            // 
            resources.ApplyResources(this.radioButtonContentKeyHex, "radioButtonContentKeyHex");
            this.errorProvider1.SetError(this.radioButtonContentKeyHex, resources.GetString("radioButtonContentKeyHex.Error"));
            this.errorProvider1.SetIconAlignment(this.radioButtonContentKeyHex, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("radioButtonContentKeyHex.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.radioButtonContentKeyHex, ((int)(resources.GetObject("radioButtonContentKeyHex.IconPadding"))));
            this.radioButtonContentKeyHex.Name = "radioButtonContentKeyHex";
            this.toolTip1.SetToolTip(this.radioButtonContentKeyHex, resources.GetString("radioButtonContentKeyHex.ToolTip"));
            this.radioButtonContentKeyHex.UseVisualStyleBackColor = true;
            this.radioButtonContentKeyHex.CheckedChanged += new System.EventHandler(this.radioButtonContentKeyHex_CheckedChanged_1);
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.errorProvider1.SetError(this.label10, resources.GetString("label10.Error"));
            this.label10.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.errorProvider1.SetIconAlignment(this.label10, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label10.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label10, ((int)(resources.GetObject("label10.IconPadding"))));
            this.label10.Name = "label10";
            this.toolTip1.SetToolTip(this.label10, resources.GetString("label10.ToolTip"));
            // 
            // textBoxkeyid
            // 
            resources.ApplyResources(this.textBoxkeyid, "textBoxkeyid");
            this.errorProvider1.SetError(this.textBoxkeyid, resources.GetString("textBoxkeyid.Error"));
            this.errorProvider1.SetIconAlignment(this.textBoxkeyid, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("textBoxkeyid.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.textBoxkeyid, ((int)(resources.GetObject("textBoxkeyid.IconPadding"))));
            this.textBoxkeyid.Name = "textBoxkeyid";
            this.toolTip1.SetToolTip(this.textBoxkeyid, resources.GetString("textBoxkeyid.ToolTip"));
            this.textBoxkeyid.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // buttonGenKeyID
            // 
            resources.ApplyResources(this.buttonGenKeyID, "buttonGenKeyID");
            this.errorProvider1.SetError(this.buttonGenKeyID, resources.GetString("buttonGenKeyID.Error"));
            this.errorProvider1.SetIconAlignment(this.buttonGenKeyID, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("buttonGenKeyID.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.buttonGenKeyID, ((int)(resources.GetObject("buttonGenKeyID.IconPadding"))));
            this.buttonGenKeyID.Name = "buttonGenKeyID";
            this.toolTip1.SetToolTip(this.buttonGenKeyID, resources.GetString("buttonGenKeyID.ToolTip"));
            this.buttonGenKeyID.UseVisualStyleBackColor = true;
            this.buttonGenKeyID.Click += new System.EventHandler(this.buttonGenKeyID_Click_1);
            // 
            // panel4
            // 
            resources.ApplyResources(this.panel4, "panel4");
            this.panel4.Controls.Add(this.radioButtonKeyIDGuid);
            this.panel4.Controls.Add(this.radioButtonKeyIDBase64);
            this.errorProvider1.SetError(this.panel4, resources.GetString("panel4.Error"));
            this.errorProvider1.SetIconAlignment(this.panel4, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("panel4.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.panel4, ((int)(resources.GetObject("panel4.IconPadding"))));
            this.panel4.Name = "panel4";
            this.toolTip1.SetToolTip(this.panel4, resources.GetString("panel4.ToolTip"));
            // 
            // radioButtonKeyIDGuid
            // 
            resources.ApplyResources(this.radioButtonKeyIDGuid, "radioButtonKeyIDGuid");
            this.radioButtonKeyIDGuid.Checked = true;
            this.errorProvider1.SetError(this.radioButtonKeyIDGuid, resources.GetString("radioButtonKeyIDGuid.Error"));
            this.errorProvider1.SetIconAlignment(this.radioButtonKeyIDGuid, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("radioButtonKeyIDGuid.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.radioButtonKeyIDGuid, ((int)(resources.GetObject("radioButtonKeyIDGuid.IconPadding"))));
            this.radioButtonKeyIDGuid.Name = "radioButtonKeyIDGuid";
            this.radioButtonKeyIDGuid.TabStop = true;
            this.toolTip1.SetToolTip(this.radioButtonKeyIDGuid, resources.GetString("radioButtonKeyIDGuid.ToolTip"));
            this.radioButtonKeyIDGuid.UseVisualStyleBackColor = true;
            this.radioButtonKeyIDGuid.CheckedChanged += new System.EventHandler(this.radioButtonKeyIDGuid_CheckedChanged);
            // 
            // radioButtonKeyIDBase64
            // 
            resources.ApplyResources(this.radioButtonKeyIDBase64, "radioButtonKeyIDBase64");
            this.errorProvider1.SetError(this.radioButtonKeyIDBase64, resources.GetString("radioButtonKeyIDBase64.Error"));
            this.errorProvider1.SetIconAlignment(this.radioButtonKeyIDBase64, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("radioButtonKeyIDBase64.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.radioButtonKeyIDBase64, ((int)(resources.GetObject("radioButtonKeyIDBase64.IconPadding"))));
            this.radioButtonKeyIDBase64.Name = "radioButtonKeyIDBase64";
            this.toolTip1.SetToolTip(this.radioButtonKeyIDBase64, resources.GetString("radioButtonKeyIDBase64.ToolTip"));
            this.radioButtonKeyIDBase64.UseVisualStyleBackColor = true;
            this.radioButtonKeyIDBase64.CheckedChanged += new System.EventHandler(this.radioButtonKeyIDBase64_CheckedChanged);
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.errorProvider1.SetError(this.label5, resources.GetString("label5.Error"));
            this.errorProvider1.SetIconAlignment(this.label5, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label5.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label5, ((int)(resources.GetObject("label5.IconPadding"))));
            this.label5.Name = "label5";
            this.toolTip1.SetToolTip(this.label5, resources.GetString("label5.ToolTip"));
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.errorProvider1.SetError(this.label9, resources.GetString("label9.Error"));
            this.label9.ForeColor = System.Drawing.Color.DarkBlue;
            this.errorProvider1.SetIconAlignment(this.label9, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label9.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label9, ((int)(resources.GetObject("label9.IconPadding"))));
            this.label9.Name = "label9";
            this.toolTip1.SetToolTip(this.label9, resources.GetString("label9.ToolTip"));
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            resources.ApplyResources(this.errorProvider1, "errorProvider1");
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
            this.toolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
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