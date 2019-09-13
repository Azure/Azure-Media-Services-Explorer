namespace AMSExplorer
{
    partial class DRM_CENCCBSCDelivery
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DRM_CENCCBSCDelivery));
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBoxPlayReady = new System.Windows.Forms.GroupBox();
            this.checkBoxPlayReady = new System.Windows.Forms.CheckBox();
            this.numericUpDownNbOptionsPlayReady = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBoxWidevine = new System.Windows.Forms.GroupBox();
            this.checkBoxWidevine = new System.Windows.Forms.CheckBox();
            this.numericUpDownNbOptionsWidevine = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBoxFairPlay = new System.Windows.Forms.GroupBox();
            this.panelFairPlayFromAMS = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.TextBoxCertificateFile = new System.Windows.Forms.TextBox();
            this.textBoxASK = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.radioButtonASKBase64 = new System.Windows.Forms.RadioButton();
            this.radioButtonASKHex = new System.Windows.Forms.RadioButton();
            this.label9 = new System.Windows.Forms.Label();
            this.buttonImportPFX = new System.Windows.Forms.Button();
            this.checkBoxFairPlay = new System.Windows.Forms.CheckBox();
            this.numericUpDownNbOptionsFairPlay = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.panel1.SuspendLayout();
            this.groupBoxPlayReady.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNbOptionsPlayReady)).BeginInit();
            this.groupBoxWidevine.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNbOptionsWidevine)).BeginInit();
            this.groupBoxFairPlay.SuspendLayout();
            this.panelFairPlayFromAMS.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNbOptionsFairPlay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
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
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.DarkBlue;
            this.label1.Name = "label1";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonOk);
            this.panel1.Controls.Add(this.buttonCancel);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // groupBoxPlayReady
            // 
            this.groupBoxPlayReady.Controls.Add(this.checkBoxPlayReady);
            this.groupBoxPlayReady.Controls.Add(this.numericUpDownNbOptionsPlayReady);
            this.groupBoxPlayReady.Controls.Add(this.label2);
            this.groupBoxPlayReady.ForeColor = System.Drawing.Color.DarkBlue;
            resources.ApplyResources(this.groupBoxPlayReady, "groupBoxPlayReady");
            this.groupBoxPlayReady.Name = "groupBoxPlayReady";
            this.groupBoxPlayReady.TabStop = false;
            // 
            // checkBoxPlayReady
            // 
            resources.ApplyResources(this.checkBoxPlayReady, "checkBoxPlayReady");
            this.checkBoxPlayReady.Checked = true;
            this.checkBoxPlayReady.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxPlayReady.ForeColor = System.Drawing.SystemColors.WindowText;
            this.checkBoxPlayReady.Name = "checkBoxPlayReady";
            this.checkBoxPlayReady.UseVisualStyleBackColor = true;
            this.checkBoxPlayReady.CheckedChanged += new System.EventHandler(this.CheckBoxPlayReady_CheckedChanged);
            // 
            // numericUpDownNbOptionsPlayReady
            // 
            resources.ApplyResources(this.numericUpDownNbOptionsPlayReady, "numericUpDownNbOptionsPlayReady");
            this.numericUpDownNbOptionsPlayReady.ForeColor = System.Drawing.SystemColors.WindowText;
            this.numericUpDownNbOptionsPlayReady.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownNbOptionsPlayReady.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownNbOptionsPlayReady.Name = "numericUpDownNbOptionsPlayReady";
            this.numericUpDownNbOptionsPlayReady.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label2.Name = "label2";
            // 
            // label8
            // 
            this.label8.ForeColor = System.Drawing.SystemColors.WindowFrame;
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // groupBoxWidevine
            // 
            this.groupBoxWidevine.Controls.Add(this.checkBoxWidevine);
            this.groupBoxWidevine.Controls.Add(this.numericUpDownNbOptionsWidevine);
            this.groupBoxWidevine.Controls.Add(this.label6);
            this.groupBoxWidevine.ForeColor = System.Drawing.Color.DarkBlue;
            resources.ApplyResources(this.groupBoxWidevine, "groupBoxWidevine");
            this.groupBoxWidevine.Name = "groupBoxWidevine";
            this.groupBoxWidevine.TabStop = false;
            // 
            // checkBoxWidevine
            // 
            resources.ApplyResources(this.checkBoxWidevine, "checkBoxWidevine");
            this.checkBoxWidevine.Checked = true;
            this.checkBoxWidevine.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxWidevine.ForeColor = System.Drawing.SystemColors.WindowText;
            this.checkBoxWidevine.Name = "checkBoxWidevine";
            this.checkBoxWidevine.UseVisualStyleBackColor = true;
            this.checkBoxWidevine.CheckedChanged += new System.EventHandler(this.CheckBoxWidevine_CheckedChanged);
            // 
            // numericUpDownNbOptionsWidevine
            // 
            resources.ApplyResources(this.numericUpDownNbOptionsWidevine, "numericUpDownNbOptionsWidevine");
            this.numericUpDownNbOptionsWidevine.ForeColor = System.Drawing.SystemColors.WindowText;
            this.numericUpDownNbOptionsWidevine.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownNbOptionsWidevine.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownNbOptionsWidevine.Name = "numericUpDownNbOptionsWidevine";
            this.numericUpDownNbOptionsWidevine.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label6.Name = "label6";
            // 
            // groupBoxFairPlay
            // 
            this.groupBoxFairPlay.Controls.Add(this.panelFairPlayFromAMS);
            this.groupBoxFairPlay.Controls.Add(this.checkBoxFairPlay);
            this.groupBoxFairPlay.Controls.Add(this.numericUpDownNbOptionsFairPlay);
            this.groupBoxFairPlay.Controls.Add(this.label3);
            resources.ApplyResources(this.groupBoxFairPlay, "groupBoxFairPlay");
            this.groupBoxFairPlay.ForeColor = System.Drawing.Color.DarkBlue;
            this.groupBoxFairPlay.Name = "groupBoxFairPlay";
            this.groupBoxFairPlay.TabStop = false;
            // 
            // panelFairPlayFromAMS
            // 
            resources.ApplyResources(this.panelFairPlayFromAMS, "panelFairPlayFromAMS");
            this.panelFairPlayFromAMS.Controls.Add(this.label10);
            this.panelFairPlayFromAMS.Controls.Add(this.TextBoxCertificateFile);
            this.panelFairPlayFromAMS.Controls.Add(this.textBoxASK);
            this.panelFairPlayFromAMS.Controls.Add(this.panel2);
            this.panelFairPlayFromAMS.Controls.Add(this.label9);
            this.panelFairPlayFromAMS.Controls.Add(this.buttonImportPFX);
            this.panelFairPlayFromAMS.Name = "panelFairPlayFromAMS";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label10.Name = "label10";
            // 
            // TextBoxCertificateFile
            // 
            resources.ApplyResources(this.TextBoxCertificateFile, "TextBoxCertificateFile");
            this.TextBoxCertificateFile.Name = "TextBoxCertificateFile";
            this.TextBoxCertificateFile.ReadOnly = true;
            // 
            // textBoxASK
            // 
            resources.ApplyResources(this.textBoxASK, "textBoxASK");
            this.textBoxASK.Name = "textBoxASK";
            this.textBoxASK.TextChanged += new System.EventHandler(this.TextBoxASK_TextChanged);
            // 
            // panel2
            // 
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Controls.Add(this.radioButtonASKBase64);
            this.panel2.Controls.Add(this.radioButtonASKHex);
            this.panel2.Name = "panel2";
            // 
            // radioButtonASKBase64
            // 
            resources.ApplyResources(this.radioButtonASKBase64, "radioButtonASKBase64");
            this.radioButtonASKBase64.ForeColor = System.Drawing.SystemColors.WindowText;
            this.radioButtonASKBase64.Name = "radioButtonASKBase64";
            this.radioButtonASKBase64.UseVisualStyleBackColor = true;
            this.radioButtonASKBase64.CheckedChanged += new System.EventHandler(this.RadioButtonASKBase64_CheckedChanged);
            // 
            // radioButtonASKHex
            // 
            resources.ApplyResources(this.radioButtonASKHex, "radioButtonASKHex");
            this.radioButtonASKHex.Checked = true;
            this.radioButtonASKHex.ForeColor = System.Drawing.SystemColors.WindowText;
            this.radioButtonASKHex.Name = "radioButtonASKHex";
            this.radioButtonASKHex.TabStop = true;
            this.radioButtonASKHex.UseVisualStyleBackColor = true;
            this.radioButtonASKHex.CheckedChanged += new System.EventHandler(this.RadioButtonASKHex_CheckedChanged);
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label9.Name = "label9";
            // 
            // buttonImportPFX
            // 
            this.buttonImportPFX.ForeColor = System.Drawing.SystemColors.WindowText;
            resources.ApplyResources(this.buttonImportPFX, "buttonImportPFX");
            this.buttonImportPFX.Name = "buttonImportPFX";
            this.buttonImportPFX.UseVisualStyleBackColor = true;
            this.buttonImportPFX.Click += new System.EventHandler(this.ButtonImportPFX_Click);
            // 
            // checkBoxFairPlay
            // 
            resources.ApplyResources(this.checkBoxFairPlay, "checkBoxFairPlay");
            this.checkBoxFairPlay.Checked = true;
            this.checkBoxFairPlay.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxFairPlay.ForeColor = System.Drawing.SystemColors.WindowText;
            this.checkBoxFairPlay.Name = "checkBoxFairPlay";
            this.checkBoxFairPlay.UseVisualStyleBackColor = true;
            this.checkBoxFairPlay.CheckedChanged += new System.EventHandler(this.CheckBoxFairPlay_CheckedChanged);
            // 
            // numericUpDownNbOptionsFairPlay
            // 
            resources.ApplyResources(this.numericUpDownNbOptionsFairPlay, "numericUpDownNbOptionsFairPlay");
            this.numericUpDownNbOptionsFairPlay.ForeColor = System.Drawing.SystemColors.WindowText;
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
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label3.Name = "label3";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // DRM_CENCDelivery
            // 
            this.AcceptButton = this.buttonOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.groupBoxFairPlay);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.groupBoxWidevine);
            this.Controls.Add(this.groupBoxPlayReady);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Name = "DRM_CENCDelivery";
            this.Load += new System.EventHandler(this.DRM_CENCCBCSDelivery_Load);
            this.DpiChanged += new System.Windows.Forms.DpiChangedEventHandler(this.DRM_CENCCBCSDelivery_DpiChanged);
            this.panel1.ResumeLayout(false);
            this.groupBoxPlayReady.ResumeLayout(false);
            this.groupBoxPlayReady.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNbOptionsPlayReady)).EndInit();
            this.groupBoxWidevine.ResumeLayout(false);
            this.groupBoxWidevine.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNbOptionsWidevine)).EndInit();
            this.groupBoxFairPlay.ResumeLayout(false);
            this.groupBoxFairPlay.PerformLayout();
            this.panelFairPlayFromAMS.ResumeLayout(false);
            this.panelFairPlayFromAMS.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
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
        private System.Windows.Forms.GroupBox groupBoxPlayReady;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDownNbOptionsPlayReady;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBoxWidevine;
        private System.Windows.Forms.NumericUpDown numericUpDownNbOptionsWidevine;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox checkBoxPlayReady;
        private System.Windows.Forms.CheckBox checkBoxWidevine;
        private System.Windows.Forms.GroupBox groupBoxFairPlay;
        private System.Windows.Forms.CheckBox checkBoxFairPlay;
        private System.Windows.Forms.NumericUpDown numericUpDownNbOptionsFairPlay;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panelFairPlayFromAMS;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox TextBoxCertificateFile;
        private System.Windows.Forms.TextBox textBoxASK;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton radioButtonASKBase64;
        private System.Windows.Forms.RadioButton radioButtonASKHex;
        public System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button buttonImportPFX;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}