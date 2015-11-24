namespace AMSExplorer
{
    partial class AddDynamicEncryptionFrame6_WidevineLicense
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
            this.radioButtonAdvanced = new System.Windows.Forms.RadioButton();
            this.radioButtonBasic = new System.Windows.Forms.RadioButton();
            this.groupBoxAdvLicense = new System.Windows.Forms.GroupBox();
            this.checkBoxCanPlay = new System.Windows.Forms.CheckBox();
            this.checkBoxAllowTrackType = new System.Windows.Forms.CheckBox();
            this.checkBoxCanPersist = new System.Windows.Forms.CheckBox();
            this.comboBoxAllowedTrackTypes = new System.Windows.Forms.ComboBox();
            this.checkBoxTrackType = new System.Windows.Forms.CheckBox();
            this.checkBoxCanRenew = new System.Windows.Forms.CheckBox();
            this.checkBoxSecLevel = new System.Windows.Forms.CheckBox();
            this.comboBoxReqOutputProtection = new System.Windows.Forms.ComboBox();
            this.numericUpDownSecLevel = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxTrackType = new System.Windows.Forms.TextBox();
            this.buttonOk = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelstep = new System.Windows.Forms.Label();
            this.openFileDialogPreset = new System.Windows.Forms.OpenFileDialog();
            this.labelWarningJSON = new System.Windows.Forms.Label();
            this.textBoxConfiguration = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.linkLabelWidevinePolicy = new System.Windows.Forms.LinkLabel();
            this.groupBoxAdvLicense.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSecLevel)).BeginInit();
            this.panel1.SuspendLayout();
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
            // radioButtonAdvanced
            // 
            this.radioButtonAdvanced.AutoSize = true;
            this.radioButtonAdvanced.Location = new System.Drawing.Point(30, 104);
            this.radioButtonAdvanced.Name = "radioButtonAdvanced";
            this.radioButtonAdvanced.Size = new System.Drawing.Size(123, 19);
            this.radioButtonAdvanced.TabIndex = 70;
            this.radioButtonAdvanced.Text = "Advanced license :";
            this.radioButtonAdvanced.UseVisualStyleBackColor = true;
            this.radioButtonAdvanced.CheckedChanged += new System.EventHandler(this.radioButtonAdvanced_CheckedChanged);
            // 
            // radioButtonBasic
            // 
            this.radioButtonBasic.AutoSize = true;
            this.radioButtonBasic.Checked = true;
            this.radioButtonBasic.Location = new System.Drawing.Point(30, 79);
            this.radioButtonBasic.Name = "radioButtonBasic";
            this.radioButtonBasic.Size = new System.Drawing.Size(91, 19);
            this.radioButtonBasic.TabIndex = 69;
            this.radioButtonBasic.TabStop = true;
            this.radioButtonBasic.Text = "Basic license";
            this.radioButtonBasic.UseVisualStyleBackColor = true;
            // 
            // groupBoxAdvLicense
            // 
            this.groupBoxAdvLicense.Controls.Add(this.checkBoxCanPlay);
            this.groupBoxAdvLicense.Controls.Add(this.checkBoxAllowTrackType);
            this.groupBoxAdvLicense.Controls.Add(this.checkBoxCanPersist);
            this.groupBoxAdvLicense.Controls.Add(this.comboBoxAllowedTrackTypes);
            this.groupBoxAdvLicense.Controls.Add(this.checkBoxTrackType);
            this.groupBoxAdvLicense.Controls.Add(this.checkBoxCanRenew);
            this.groupBoxAdvLicense.Controls.Add(this.checkBoxSecLevel);
            this.groupBoxAdvLicense.Controls.Add(this.comboBoxReqOutputProtection);
            this.groupBoxAdvLicense.Controls.Add(this.numericUpDownSecLevel);
            this.groupBoxAdvLicense.Controls.Add(this.label2);
            this.groupBoxAdvLicense.Controls.Add(this.textBoxTrackType);
            this.groupBoxAdvLicense.Enabled = false;
            this.groupBoxAdvLicense.Location = new System.Drawing.Point(30, 139);
            this.groupBoxAdvLicense.Name = "groupBoxAdvLicense";
            this.groupBoxAdvLicense.Size = new System.Drawing.Size(627, 312);
            this.groupBoxAdvLicense.TabIndex = 68;
            this.groupBoxAdvLicense.TabStop = false;
            this.groupBoxAdvLicense.Text = "Advanced license";
            // 
            // checkBoxCanPlay
            // 
            this.checkBoxCanPlay.AutoSize = true;
            this.checkBoxCanPlay.Checked = true;
            this.checkBoxCanPlay.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxCanPlay.Location = new System.Drawing.Point(15, 73);
            this.checkBoxCanPlay.Name = "checkBoxCanPlay";
            this.checkBoxCanPlay.Size = new System.Drawing.Size(72, 19);
            this.checkBoxCanPlay.TabIndex = 47;
            this.checkBoxCanPlay.Text = "Can Play";
            this.checkBoxCanPlay.UseVisualStyleBackColor = true;
            this.checkBoxCanPlay.CheckedChanged += new System.EventHandler(this.StateChanged);
            // 
            // checkBoxAllowTrackType
            // 
            this.checkBoxAllowTrackType.AutoSize = true;
            this.checkBoxAllowTrackType.Checked = true;
            this.checkBoxAllowTrackType.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAllowTrackType.Location = new System.Drawing.Point(11, 36);
            this.checkBoxAllowTrackType.Name = "checkBoxAllowTrackType";
            this.checkBoxAllowTrackType.Size = new System.Drawing.Size(139, 19);
            this.checkBoxAllowTrackType.TabIndex = 67;
            this.checkBoxAllowTrackType.Text = "Allowed Track Types :";
            this.checkBoxAllowTrackType.UseVisualStyleBackColor = true;
            this.checkBoxAllowTrackType.CheckedChanged += new System.EventHandler(this.checkBoxAllowTrackType_CheckedChanged);
            // 
            // checkBoxCanPersist
            // 
            this.checkBoxCanPersist.AutoSize = true;
            this.checkBoxCanPersist.Checked = true;
            this.checkBoxCanPersist.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxCanPersist.Location = new System.Drawing.Point(15, 98);
            this.checkBoxCanPersist.Name = "checkBoxCanPersist";
            this.checkBoxCanPersist.Size = new System.Drawing.Size(84, 19);
            this.checkBoxCanPersist.TabIndex = 48;
            this.checkBoxCanPersist.Text = "Can Persist";
            this.checkBoxCanPersist.UseVisualStyleBackColor = true;
            this.checkBoxCanPersist.CheckedChanged += new System.EventHandler(this.StateChanged);
            // 
            // comboBoxAllowedTrackTypes
            // 
            this.comboBoxAllowedTrackTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAllowedTrackTypes.FormattingEnabled = true;
            this.comboBoxAllowedTrackTypes.Location = new System.Drawing.Point(173, 34);
            this.comboBoxAllowedTrackTypes.Name = "comboBoxAllowedTrackTypes";
            this.comboBoxAllowedTrackTypes.Size = new System.Drawing.Size(207, 23);
            this.comboBoxAllowedTrackTypes.TabIndex = 45;
            this.comboBoxAllowedTrackTypes.SelectedIndexChanged += new System.EventHandler(this.StateChanged);
            // 
            // checkBoxTrackType
            // 
            this.checkBoxTrackType.AutoSize = true;
            this.checkBoxTrackType.Checked = true;
            this.checkBoxTrackType.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxTrackType.Location = new System.Drawing.Point(21, 212);
            this.checkBoxTrackType.Name = "checkBoxTrackType";
            this.checkBoxTrackType.Size = new System.Drawing.Size(88, 19);
            this.checkBoxTrackType.TabIndex = 66;
            this.checkBoxTrackType.Text = "Track Type :";
            this.checkBoxTrackType.UseVisualStyleBackColor = true;
            this.checkBoxTrackType.CheckedChanged += new System.EventHandler(this.checkBoxTrackType_CheckedChanged);
            // 
            // checkBoxCanRenew
            // 
            this.checkBoxCanRenew.AutoSize = true;
            this.checkBoxCanRenew.Location = new System.Drawing.Point(15, 123);
            this.checkBoxCanRenew.Name = "checkBoxCanRenew";
            this.checkBoxCanRenew.Size = new System.Drawing.Size(85, 19);
            this.checkBoxCanRenew.TabIndex = 49;
            this.checkBoxCanRenew.Text = "Can Renew";
            this.checkBoxCanRenew.UseVisualStyleBackColor = true;
            this.checkBoxCanRenew.CheckedChanged += new System.EventHandler(this.StateChanged);
            // 
            // checkBoxSecLevel
            // 
            this.checkBoxSecLevel.AutoSize = true;
            this.checkBoxSecLevel.Checked = true;
            this.checkBoxSecLevel.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSecLevel.Location = new System.Drawing.Point(21, 252);
            this.checkBoxSecLevel.Name = "checkBoxSecLevel";
            this.checkBoxSecLevel.Size = new System.Drawing.Size(104, 19);
            this.checkBoxSecLevel.TabIndex = 65;
            this.checkBoxSecLevel.Text = "Security Level :";
            this.checkBoxSecLevel.UseVisualStyleBackColor = true;
            this.checkBoxSecLevel.CheckedChanged += new System.EventHandler(this.checkBoxSecLevel_CheckedChanged);
            // 
            // comboBoxReqOutputProtection
            // 
            this.comboBoxReqOutputProtection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxReqOutputProtection.FormattingEnabled = true;
            this.comboBoxReqOutputProtection.Location = new System.Drawing.Point(183, 171);
            this.comboBoxReqOutputProtection.Name = "comboBoxReqOutputProtection";
            this.comboBoxReqOutputProtection.Size = new System.Drawing.Size(207, 23);
            this.comboBoxReqOutputProtection.TabIndex = 50;
            this.comboBoxReqOutputProtection.SelectedIndexChanged += new System.EventHandler(this.StateChanged);
            // 
            // numericUpDownSecLevel
            // 
            this.numericUpDownSecLevel.Location = new System.Drawing.Point(183, 252);
            this.numericUpDownSecLevel.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDownSecLevel.Name = "numericUpDownSecLevel";
            this.numericUpDownSecLevel.Size = new System.Drawing.Size(120, 23);
            this.numericUpDownSecLevel.TabIndex = 54;
            this.numericUpDownSecLevel.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownSecLevel.ValueChanged += new System.EventHandler(this.StateChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 174);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(159, 15);
            this.label2.TabIndex = 51;
            this.label2.Text = "Required Output Protection :";
            // 
            // textBoxTrackType
            // 
            this.textBoxTrackType.Location = new System.Drawing.Point(183, 210);
            this.textBoxTrackType.Name = "textBoxTrackType";
            this.textBoxTrackType.Size = new System.Drawing.Size(120, 23);
            this.textBoxTrackType.TabIndex = 53;
            this.textBoxTrackType.Text = "SD";
            this.textBoxTrackType.TextChanged += new System.EventHandler(this.StateChanged);
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Image = global::AMSExplorer.Bitmaps.DRM_protection;
            this.buttonOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonOk.Location = new System.Drawing.Point(384, 13);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(176, 27);
            this.buttonOk.TabIndex = 17;
            this.buttonOk.Text = "Ok";
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
            // labelstep
            // 
            this.labelstep.AutoSize = true;
            this.labelstep.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.labelstep.ForeColor = System.Drawing.Color.DarkBlue;
            this.labelstep.Location = new System.Drawing.Point(26, 21);
            this.labelstep.Name = "labelstep";
            this.labelstep.Size = new System.Drawing.Size(396, 42);
            this.labelstep.TabIndex = 85;
            this.labelstep.Text = "Step {0}\r\nSpecify the Widevine license template for option #{1}";
            // 
            // openFileDialogPreset
            // 
            this.openFileDialogPreset.DefaultExt = "xml";
            this.openFileDialogPreset.Filter = "XML files|*.xml|All files|*.*";
            // 
            // labelWarningJSON
            // 
            this.labelWarningJSON.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelWarningJSON.ForeColor = System.Drawing.Color.Red;
            this.labelWarningJSON.Location = new System.Drawing.Point(126, 465);
            this.labelWarningJSON.Name = "labelWarningJSON";
            this.labelWarningJSON.Size = new System.Drawing.Size(531, 21);
            this.labelWarningJSON.TabIndex = 88;
            this.labelWarningJSON.Tag = "JSON Syntax error. {0}";
            this.labelWarningJSON.Text = "JSON Syntax error. {0}";
            this.labelWarningJSON.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelWarningJSON.Visible = false;
            // 
            // textBoxConfiguration
            // 
            this.textBoxConfiguration.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxConfiguration.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxConfiguration.Location = new System.Drawing.Point(30, 487);
            this.textBoxConfiguration.Multiline = true;
            this.textBoxConfiguration.Name = "textBoxConfiguration";
            this.textBoxConfiguration.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxConfiguration.Size = new System.Drawing.Size(627, 179);
            this.textBoxConfiguration.TabIndex = 86;
            this.textBoxConfiguration.TextChanged += new System.EventHandler(this.textBoxConfiguration_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 468);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 15);
            this.label1.TabIndex = 87;
            this.label1.Text = "JSON (editable) :";
            // 
            // linkLabelWidevinePolicy
            // 
            this.linkLabelWidevinePolicy.AutoSize = true;
            this.linkLabelWidevinePolicy.Location = new System.Drawing.Point(598, 48);
            this.linkLabelWidevinePolicy.Name = "linkLabelWidevinePolicy";
            this.linkLabelWidevinePolicy.Size = new System.Drawing.Size(59, 15);
            this.linkLabelWidevinePolicy.TabIndex = 89;
            this.linkLabelWidevinePolicy.TabStop = true;
            this.linkLabelWidevinePolicy.Text = "More info";
            this.linkLabelWidevinePolicy.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelWidevinePolicy_LinkClicked);
            // 
            // AddDynamicEncryptionFrame6_WidevineLicense
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(681, 741);
            this.Controls.Add(this.linkLabelWidevinePolicy);
            this.Controls.Add(this.labelWarningJSON);
            this.Controls.Add(this.textBoxConfiguration);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.radioButtonAdvanced);
            this.Controls.Add(this.labelstep);
            this.Controls.Add(this.radioButtonBasic);
            this.Controls.Add(this.groupBoxAdvLicense);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "AddDynamicEncryptionFrame6_WidevineLicense";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Dynamic Encryption - Step {0}";
            this.Load += new System.EventHandler(this.AddDynamicEncryptionFrame6_WidevineLicense_Load);
            this.groupBoxAdvLicense.ResumeLayout(false);
            this.groupBoxAdvLicense.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSecLevel)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.Button buttonOk;
        public System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelstep;
        private System.Windows.Forms.OpenFileDialog openFileDialogPreset;
        private System.Windows.Forms.ComboBox comboBoxAllowedTrackTypes;
        private System.Windows.Forms.CheckBox checkBoxCanPlay;
        private System.Windows.Forms.CheckBox checkBoxCanRenew;
        private System.Windows.Forms.CheckBox checkBoxCanPersist;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxReqOutputProtection;
        private System.Windows.Forms.TextBox textBoxTrackType;
        private System.Windows.Forms.NumericUpDown numericUpDownSecLevel;
        private System.Windows.Forms.CheckBox checkBoxSecLevel;
        private System.Windows.Forms.CheckBox checkBoxTrackType;
        private System.Windows.Forms.CheckBox checkBoxAllowTrackType;
        private System.Windows.Forms.RadioButton radioButtonAdvanced;
        private System.Windows.Forms.RadioButton radioButtonBasic;
        private System.Windows.Forms.GroupBox groupBoxAdvLicense;
        private System.Windows.Forms.Label labelWarningJSON;
        private System.Windows.Forms.TextBox textBoxConfiguration;
        public System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel linkLabelWidevinePolicy;
    }
}