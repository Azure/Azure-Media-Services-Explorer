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
            resources.ApplyResources(this.buttonOk, "buttonOk");
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.errorProvider1.SetError(this.buttonOk, resources.GetString("buttonOk.Error"));
            this.errorProvider1.SetIconAlignment(this.buttonOk, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("buttonOk.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.buttonOk, ((int)(resources.GetObject("buttonOk.IconPadding"))));
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.errorProvider1.SetError(this.buttonCancel, resources.GetString("buttonCancel.Error"));
            this.errorProvider1.SetIconAlignment(this.buttonCancel, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("buttonCancel.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.buttonCancel, ((int)(resources.GetObject("buttonCancel.IconPadding"))));
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.errorProvider1.SetError(this.label1, resources.GetString("label1.Error"));
            this.label1.ForeColor = System.Drawing.Color.DarkBlue;
            this.errorProvider1.SetIconAlignment(this.label1, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label1.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label1, ((int)(resources.GetObject("label1.IconPadding"))));
            this.label1.Name = "label1";
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonOk);
            this.panel1.Controls.Add(this.buttonCancel);
            this.errorProvider1.SetError(this.panel1, resources.GetString("panel1.Error"));
            this.errorProvider1.SetIconAlignment(this.panel1, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("panel1.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.panel1, ((int)(resources.GetObject("panel1.IconPadding"))));
            this.panel1.Name = "panel1";
            // 
            // groupBoxPlayReady
            // 
            resources.ApplyResources(this.groupBoxPlayReady, "groupBoxPlayReady");
            this.groupBoxPlayReady.Controls.Add(this.checkBoxPlayReady);
            this.groupBoxPlayReady.Controls.Add(this.numericUpDownNbOptionsPlayReady);
            this.groupBoxPlayReady.Controls.Add(this.label2);
            this.errorProvider1.SetError(this.groupBoxPlayReady, resources.GetString("groupBoxPlayReady.Error"));
            this.groupBoxPlayReady.ForeColor = System.Drawing.Color.DarkBlue;
            this.errorProvider1.SetIconAlignment(this.groupBoxPlayReady, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("groupBoxPlayReady.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.groupBoxPlayReady, ((int)(resources.GetObject("groupBoxPlayReady.IconPadding"))));
            this.groupBoxPlayReady.Name = "groupBoxPlayReady";
            this.groupBoxPlayReady.TabStop = false;
            // 
            // checkBoxPlayReady
            // 
            resources.ApplyResources(this.checkBoxPlayReady, "checkBoxPlayReady");
            this.checkBoxPlayReady.Checked = true;
            this.checkBoxPlayReady.CheckState = System.Windows.Forms.CheckState.Checked;
            this.errorProvider1.SetError(this.checkBoxPlayReady, resources.GetString("checkBoxPlayReady.Error"));
            this.checkBoxPlayReady.ForeColor = System.Drawing.SystemColors.WindowText;
            this.errorProvider1.SetIconAlignment(this.checkBoxPlayReady, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("checkBoxPlayReady.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.checkBoxPlayReady, ((int)(resources.GetObject("checkBoxPlayReady.IconPadding"))));
            this.checkBoxPlayReady.Name = "checkBoxPlayReady";
            this.checkBoxPlayReady.UseVisualStyleBackColor = true;
            this.checkBoxPlayReady.CheckedChanged += new System.EventHandler(this.CheckBoxPlayReady_CheckedChanged);
            // 
            // numericUpDownNbOptionsPlayReady
            // 
            resources.ApplyResources(this.numericUpDownNbOptionsPlayReady, "numericUpDownNbOptionsPlayReady");
            this.errorProvider1.SetError(this.numericUpDownNbOptionsPlayReady, resources.GetString("numericUpDownNbOptionsPlayReady.Error"));
            this.numericUpDownNbOptionsPlayReady.ForeColor = System.Drawing.SystemColors.WindowText;
            this.errorProvider1.SetIconAlignment(this.numericUpDownNbOptionsPlayReady, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("numericUpDownNbOptionsPlayReady.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.numericUpDownNbOptionsPlayReady, ((int)(resources.GetObject("numericUpDownNbOptionsPlayReady.IconPadding"))));
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
            this.errorProvider1.SetError(this.label2, resources.GetString("label2.Error"));
            this.label2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.errorProvider1.SetIconAlignment(this.label2, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label2.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label2, ((int)(resources.GetObject("label2.IconPadding"))));
            this.label2.Name = "label2";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.errorProvider1.SetError(this.label8, resources.GetString("label8.Error"));
            this.label8.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.errorProvider1.SetIconAlignment(this.label8, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label8.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label8, ((int)(resources.GetObject("label8.IconPadding"))));
            this.label8.Name = "label8";
            // 
            // groupBoxWidevine
            // 
            resources.ApplyResources(this.groupBoxWidevine, "groupBoxWidevine");
            this.groupBoxWidevine.Controls.Add(this.checkBoxWidevine);
            this.groupBoxWidevine.Controls.Add(this.numericUpDownNbOptionsWidevine);
            this.groupBoxWidevine.Controls.Add(this.label6);
            this.errorProvider1.SetError(this.groupBoxWidevine, resources.GetString("groupBoxWidevine.Error"));
            this.groupBoxWidevine.ForeColor = System.Drawing.Color.DarkBlue;
            this.errorProvider1.SetIconAlignment(this.groupBoxWidevine, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("groupBoxWidevine.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.groupBoxWidevine, ((int)(resources.GetObject("groupBoxWidevine.IconPadding"))));
            this.groupBoxWidevine.Name = "groupBoxWidevine";
            this.groupBoxWidevine.TabStop = false;
            // 
            // checkBoxWidevine
            // 
            resources.ApplyResources(this.checkBoxWidevine, "checkBoxWidevine");
            this.checkBoxWidevine.Checked = true;
            this.checkBoxWidevine.CheckState = System.Windows.Forms.CheckState.Checked;
            this.errorProvider1.SetError(this.checkBoxWidevine, resources.GetString("checkBoxWidevine.Error"));
            this.checkBoxWidevine.ForeColor = System.Drawing.SystemColors.WindowText;
            this.errorProvider1.SetIconAlignment(this.checkBoxWidevine, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("checkBoxWidevine.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.checkBoxWidevine, ((int)(resources.GetObject("checkBoxWidevine.IconPadding"))));
            this.checkBoxWidevine.Name = "checkBoxWidevine";
            this.checkBoxWidevine.UseVisualStyleBackColor = true;
            this.checkBoxWidevine.CheckedChanged += new System.EventHandler(this.CheckBoxWidevine_CheckedChanged);
            // 
            // numericUpDownNbOptionsWidevine
            // 
            resources.ApplyResources(this.numericUpDownNbOptionsWidevine, "numericUpDownNbOptionsWidevine");
            this.errorProvider1.SetError(this.numericUpDownNbOptionsWidevine, resources.GetString("numericUpDownNbOptionsWidevine.Error"));
            this.numericUpDownNbOptionsWidevine.ForeColor = System.Drawing.SystemColors.WindowText;
            this.errorProvider1.SetIconAlignment(this.numericUpDownNbOptionsWidevine, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("numericUpDownNbOptionsWidevine.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.numericUpDownNbOptionsWidevine, ((int)(resources.GetObject("numericUpDownNbOptionsWidevine.IconPadding"))));
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
            this.errorProvider1.SetError(this.label6, resources.GetString("label6.Error"));
            this.label6.ForeColor = System.Drawing.SystemColors.WindowText;
            this.errorProvider1.SetIconAlignment(this.label6, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label6.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label6, ((int)(resources.GetObject("label6.IconPadding"))));
            this.label6.Name = "label6";
            // 
            // groupBoxFairPlay
            // 
            resources.ApplyResources(this.groupBoxFairPlay, "groupBoxFairPlay");
            this.groupBoxFairPlay.Controls.Add(this.panelFairPlayFromAMS);
            this.groupBoxFairPlay.Controls.Add(this.checkBoxFairPlay);
            this.groupBoxFairPlay.Controls.Add(this.numericUpDownNbOptionsFairPlay);
            this.groupBoxFairPlay.Controls.Add(this.label3);
            this.errorProvider1.SetError(this.groupBoxFairPlay, resources.GetString("groupBoxFairPlay.Error"));
            this.groupBoxFairPlay.ForeColor = System.Drawing.Color.DarkBlue;
            this.errorProvider1.SetIconAlignment(this.groupBoxFairPlay, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("groupBoxFairPlay.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.groupBoxFairPlay, ((int)(resources.GetObject("groupBoxFairPlay.IconPadding"))));
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
            this.errorProvider1.SetError(this.panelFairPlayFromAMS, resources.GetString("panelFairPlayFromAMS.Error"));
            this.errorProvider1.SetIconAlignment(this.panelFairPlayFromAMS, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("panelFairPlayFromAMS.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.panelFairPlayFromAMS, ((int)(resources.GetObject("panelFairPlayFromAMS.IconPadding"))));
            this.panelFairPlayFromAMS.Name = "panelFairPlayFromAMS";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.errorProvider1.SetError(this.label10, resources.GetString("label10.Error"));
            this.label10.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.errorProvider1.SetIconAlignment(this.label10, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label10.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label10, ((int)(resources.GetObject("label10.IconPadding"))));
            this.label10.Name = "label10";
            // 
            // TextBoxCertificateFile
            // 
            resources.ApplyResources(this.TextBoxCertificateFile, "TextBoxCertificateFile");
            this.errorProvider1.SetError(this.TextBoxCertificateFile, resources.GetString("TextBoxCertificateFile.Error"));
            this.errorProvider1.SetIconAlignment(this.TextBoxCertificateFile, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("TextBoxCertificateFile.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.TextBoxCertificateFile, ((int)(resources.GetObject("TextBoxCertificateFile.IconPadding"))));
            this.TextBoxCertificateFile.Name = "TextBoxCertificateFile";
            this.TextBoxCertificateFile.ReadOnly = true;
            // 
            // textBoxASK
            // 
            resources.ApplyResources(this.textBoxASK, "textBoxASK");
            this.errorProvider1.SetError(this.textBoxASK, resources.GetString("textBoxASK.Error"));
            this.errorProvider1.SetIconAlignment(this.textBoxASK, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("textBoxASK.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.textBoxASK, ((int)(resources.GetObject("textBoxASK.IconPadding"))));
            this.textBoxASK.Name = "textBoxASK";
            this.textBoxASK.TextChanged += new System.EventHandler(this.TextBoxASK_TextChanged);
            // 
            // panel2
            // 
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Controls.Add(this.radioButtonASKBase64);
            this.panel2.Controls.Add(this.radioButtonASKHex);
            this.errorProvider1.SetError(this.panel2, resources.GetString("panel2.Error"));
            this.errorProvider1.SetIconAlignment(this.panel2, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("panel2.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.panel2, ((int)(resources.GetObject("panel2.IconPadding"))));
            this.panel2.Name = "panel2";
            // 
            // radioButtonASKBase64
            // 
            resources.ApplyResources(this.radioButtonASKBase64, "radioButtonASKBase64");
            this.errorProvider1.SetError(this.radioButtonASKBase64, resources.GetString("radioButtonASKBase64.Error"));
            this.radioButtonASKBase64.ForeColor = System.Drawing.SystemColors.WindowText;
            this.errorProvider1.SetIconAlignment(this.radioButtonASKBase64, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("radioButtonASKBase64.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.radioButtonASKBase64, ((int)(resources.GetObject("radioButtonASKBase64.IconPadding"))));
            this.radioButtonASKBase64.Name = "radioButtonASKBase64";
            this.radioButtonASKBase64.UseVisualStyleBackColor = true;
            this.radioButtonASKBase64.CheckedChanged += new System.EventHandler(this.RadioButtonASKBase64_CheckedChanged);
            // 
            // radioButtonASKHex
            // 
            resources.ApplyResources(this.radioButtonASKHex, "radioButtonASKHex");
            this.radioButtonASKHex.Checked = true;
            this.errorProvider1.SetError(this.radioButtonASKHex, resources.GetString("radioButtonASKHex.Error"));
            this.radioButtonASKHex.ForeColor = System.Drawing.SystemColors.WindowText;
            this.errorProvider1.SetIconAlignment(this.radioButtonASKHex, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("radioButtonASKHex.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.radioButtonASKHex, ((int)(resources.GetObject("radioButtonASKHex.IconPadding"))));
            this.radioButtonASKHex.Name = "radioButtonASKHex";
            this.radioButtonASKHex.TabStop = true;
            this.radioButtonASKHex.UseVisualStyleBackColor = true;
            this.radioButtonASKHex.CheckedChanged += new System.EventHandler(this.RadioButtonASKHex_CheckedChanged);
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.errorProvider1.SetError(this.label9, resources.GetString("label9.Error"));
            this.label9.ForeColor = System.Drawing.SystemColors.WindowText;
            this.errorProvider1.SetIconAlignment(this.label9, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label9.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label9, ((int)(resources.GetObject("label9.IconPadding"))));
            this.label9.Name = "label9";
            // 
            // buttonImportPFX
            // 
            resources.ApplyResources(this.buttonImportPFX, "buttonImportPFX");
            this.errorProvider1.SetError(this.buttonImportPFX, resources.GetString("buttonImportPFX.Error"));
            this.buttonImportPFX.ForeColor = System.Drawing.SystemColors.WindowText;
            this.errorProvider1.SetIconAlignment(this.buttonImportPFX, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("buttonImportPFX.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.buttonImportPFX, ((int)(resources.GetObject("buttonImportPFX.IconPadding"))));
            this.buttonImportPFX.Name = "buttonImportPFX";
            this.buttonImportPFX.UseVisualStyleBackColor = true;
            this.buttonImportPFX.Click += new System.EventHandler(this.ButtonImportPFX_Click);
            // 
            // checkBoxFairPlay
            // 
            resources.ApplyResources(this.checkBoxFairPlay, "checkBoxFairPlay");
            this.checkBoxFairPlay.Checked = true;
            this.checkBoxFairPlay.CheckState = System.Windows.Forms.CheckState.Checked;
            this.errorProvider1.SetError(this.checkBoxFairPlay, resources.GetString("checkBoxFairPlay.Error"));
            this.checkBoxFairPlay.ForeColor = System.Drawing.SystemColors.WindowText;
            this.errorProvider1.SetIconAlignment(this.checkBoxFairPlay, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("checkBoxFairPlay.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.checkBoxFairPlay, ((int)(resources.GetObject("checkBoxFairPlay.IconPadding"))));
            this.checkBoxFairPlay.Name = "checkBoxFairPlay";
            this.checkBoxFairPlay.UseVisualStyleBackColor = true;
            this.checkBoxFairPlay.CheckedChanged += new System.EventHandler(this.CheckBoxFairPlay_CheckedChanged);
            // 
            // numericUpDownNbOptionsFairPlay
            // 
            resources.ApplyResources(this.numericUpDownNbOptionsFairPlay, "numericUpDownNbOptionsFairPlay");
            this.errorProvider1.SetError(this.numericUpDownNbOptionsFairPlay, resources.GetString("numericUpDownNbOptionsFairPlay.Error"));
            this.numericUpDownNbOptionsFairPlay.ForeColor = System.Drawing.SystemColors.WindowText;
            this.errorProvider1.SetIconAlignment(this.numericUpDownNbOptionsFairPlay, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("numericUpDownNbOptionsFairPlay.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.numericUpDownNbOptionsFairPlay, ((int)(resources.GetObject("numericUpDownNbOptionsFairPlay.IconPadding"))));
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
            this.errorProvider1.SetError(this.label3, resources.GetString("label3.Error"));
            this.label3.ForeColor = System.Drawing.SystemColors.WindowText;
            this.errorProvider1.SetIconAlignment(this.label3, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label3.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label3, ((int)(resources.GetObject("label3.IconPadding"))));
            this.label3.Name = "label3";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            resources.ApplyResources(this.errorProvider1, "errorProvider1");
            // 
            // DRM_CENCCBSCDelivery
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
            this.Name = "DRM_CENCCBSCDelivery";
            this.Load += new System.EventHandler(this.DRM_CENCCBCSDelivery_Load);
            this.Shown += new System.EventHandler(this.DRM_CENCCBSCDelivery_Shown);
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