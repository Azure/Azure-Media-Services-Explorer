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
            this.tabControlPlayReadySettings = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.checkBoxAllowTrackType = new System.Windows.Forms.CheckBox();
            this.checkBoxTrackType = new System.Windows.Forms.CheckBox();
            this.checkBoxSecLevel = new System.Windows.Forms.CheckBox();
            this.numericUpDownSecLevel = new System.Windows.Forms.NumericUpDown();
            this.textBoxTrackType = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxReqOutputProtection = new System.Windows.Forms.ComboBox();
            this.checkBoxCanRenew = new System.Windows.Forms.CheckBox();
            this.checkBoxCanPersist = new System.Windows.Forms.CheckBox();
            this.checkBoxCanPlay = new System.Windows.Forms.CheckBox();
            this.comboBoxAllowedTrackTypes = new System.Windows.Forms.ComboBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.buttonOk = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelstep = new System.Windows.Forms.Label();
            this.openFileDialogPreset = new System.Windows.Forms.OpenFileDialog();
            this.tabControlPlayReadySettings.SuspendLayout();
            this.tabPage3.SuspendLayout();
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
            // tabControlPlayReadySettings
            // 
            this.tabControlPlayReadySettings.Controls.Add(this.tabPage3);
            this.tabControlPlayReadySettings.Controls.Add(this.tabPage1);
            this.tabControlPlayReadySettings.Location = new System.Drawing.Point(27, 153);
            this.tabControlPlayReadySettings.Name = "tabControlPlayReadySettings";
            this.tabControlPlayReadySettings.SelectedIndex = 0;
            this.tabControlPlayReadySettings.Size = new System.Drawing.Size(640, 510);
            this.tabControlPlayReadySettings.TabIndex = 33;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.checkBoxAllowTrackType);
            this.tabPage3.Controls.Add(this.checkBoxTrackType);
            this.tabPage3.Controls.Add(this.checkBoxSecLevel);
            this.tabPage3.Controls.Add(this.numericUpDownSecLevel);
            this.tabPage3.Controls.Add(this.textBoxTrackType);
            this.tabPage3.Controls.Add(this.label2);
            this.tabPage3.Controls.Add(this.comboBoxReqOutputProtection);
            this.tabPage3.Controls.Add(this.checkBoxCanRenew);
            this.tabPage3.Controls.Add(this.checkBoxCanPersist);
            this.tabPage3.Controls.Add(this.checkBoxCanPlay);
            this.tabPage3.Controls.Add(this.comboBoxAllowedTrackTypes);
            this.tabPage3.Location = new System.Drawing.Point(4, 24);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(632, 482);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Common settings";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // checkBoxAllowTrackType
            // 
            this.checkBoxAllowTrackType.AutoSize = true;
            this.checkBoxAllowTrackType.Checked = true;
            this.checkBoxAllowTrackType.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAllowTrackType.Location = new System.Drawing.Point(31, 52);
            this.checkBoxAllowTrackType.Name = "checkBoxAllowTrackType";
            this.checkBoxAllowTrackType.Size = new System.Drawing.Size(139, 19);
            this.checkBoxAllowTrackType.TabIndex = 67;
            this.checkBoxAllowTrackType.Text = "Allowed Track Types :";
            this.checkBoxAllowTrackType.UseVisualStyleBackColor = true;
            this.checkBoxAllowTrackType.CheckedChanged += new System.EventHandler(this.checkBoxAllowTrackType_CheckedChanged);
            // 
            // checkBoxTrackType
            // 
            this.checkBoxTrackType.AutoSize = true;
            this.checkBoxTrackType.Checked = true;
            this.checkBoxTrackType.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxTrackType.Location = new System.Drawing.Point(31, 306);
            this.checkBoxTrackType.Name = "checkBoxTrackType";
            this.checkBoxTrackType.Size = new System.Drawing.Size(88, 19);
            this.checkBoxTrackType.TabIndex = 66;
            this.checkBoxTrackType.Text = "Track Type :";
            this.checkBoxTrackType.UseVisualStyleBackColor = true;
            this.checkBoxTrackType.CheckedChanged += new System.EventHandler(this.checkBoxTrackType_CheckedChanged);
            // 
            // checkBoxSecLevel
            // 
            this.checkBoxSecLevel.AutoSize = true;
            this.checkBoxSecLevel.Checked = true;
            this.checkBoxSecLevel.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSecLevel.Location = new System.Drawing.Point(31, 346);
            this.checkBoxSecLevel.Name = "checkBoxSecLevel";
            this.checkBoxSecLevel.Size = new System.Drawing.Size(104, 19);
            this.checkBoxSecLevel.TabIndex = 65;
            this.checkBoxSecLevel.Text = "Security Level :";
            this.checkBoxSecLevel.UseVisualStyleBackColor = true;
            this.checkBoxSecLevel.CheckedChanged += new System.EventHandler(this.checkBoxSecLevel_CheckedChanged);
            // 
            // numericUpDownSecLevel
            // 
            this.numericUpDownSecLevel.Location = new System.Drawing.Point(193, 346);
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
            // 
            // textBoxTrackType
            // 
            this.textBoxTrackType.Location = new System.Drawing.Point(193, 304);
            this.textBoxTrackType.Name = "textBoxTrackType";
            this.textBoxTrackType.Size = new System.Drawing.Size(120, 23);
            this.textBoxTrackType.TabIndex = 53;
            this.textBoxTrackType.Text = "SD";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 268);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(159, 15);
            this.label2.TabIndex = 51;
            this.label2.Text = "Required Output Protection :";
            // 
            // comboBoxReqOutputProtection
            // 
            this.comboBoxReqOutputProtection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxReqOutputProtection.FormattingEnabled = true;
            this.comboBoxReqOutputProtection.Location = new System.Drawing.Point(193, 265);
            this.comboBoxReqOutputProtection.Name = "comboBoxReqOutputProtection";
            this.comboBoxReqOutputProtection.Size = new System.Drawing.Size(207, 23);
            this.comboBoxReqOutputProtection.TabIndex = 50;
            // 
            // checkBoxCanRenew
            // 
            this.checkBoxCanRenew.AutoSize = true;
            this.checkBoxCanRenew.Location = new System.Drawing.Point(31, 185);
            this.checkBoxCanRenew.Name = "checkBoxCanRenew";
            this.checkBoxCanRenew.Size = new System.Drawing.Size(85, 19);
            this.checkBoxCanRenew.TabIndex = 49;
            this.checkBoxCanRenew.Text = "Can Renew";
            this.checkBoxCanRenew.UseVisualStyleBackColor = true;
            // 
            // checkBoxCanPersist
            // 
            this.checkBoxCanPersist.AutoSize = true;
            this.checkBoxCanPersist.Checked = true;
            this.checkBoxCanPersist.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxCanPersist.Location = new System.Drawing.Point(31, 160);
            this.checkBoxCanPersist.Name = "checkBoxCanPersist";
            this.checkBoxCanPersist.Size = new System.Drawing.Size(84, 19);
            this.checkBoxCanPersist.TabIndex = 48;
            this.checkBoxCanPersist.Text = "Can Persist";
            this.checkBoxCanPersist.UseVisualStyleBackColor = true;
            // 
            // checkBoxCanPlay
            // 
            this.checkBoxCanPlay.AutoSize = true;
            this.checkBoxCanPlay.Checked = true;
            this.checkBoxCanPlay.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxCanPlay.Location = new System.Drawing.Point(31, 135);
            this.checkBoxCanPlay.Name = "checkBoxCanPlay";
            this.checkBoxCanPlay.Size = new System.Drawing.Size(72, 19);
            this.checkBoxCanPlay.TabIndex = 47;
            this.checkBoxCanPlay.Text = "Can Play";
            this.checkBoxCanPlay.UseVisualStyleBackColor = true;
            // 
            // comboBoxAllowedTrackTypes
            // 
            this.comboBoxAllowedTrackTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAllowedTrackTypes.FormattingEnabled = true;
            this.comboBoxAllowedTrackTypes.Location = new System.Drawing.Point(193, 50);
            this.comboBoxAllowedTrackTypes.Name = "comboBoxAllowedTrackTypes";
            this.comboBoxAllowedTrackTypes.Size = new System.Drawing.Size(207, 23);
            this.comboBoxAllowedTrackTypes.TabIndex = 45;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(632, 482);
            this.tabPage1.TabIndex = 3;
            this.tabPage1.Text = "Advanced settings";
            this.tabPage1.UseVisualStyleBackColor = true;
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
            // AddDynamicEncryptionFrame6_WidevineLicense
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(681, 741);
            this.Controls.Add(this.labelstep);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tabControlPlayReadySettings);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "AddDynamicEncryptionFrame6_WidevineLicense";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Dynamic Encryption - Step {0}";
            this.Load += new System.EventHandler(this.AddDynamicEncryptionFrame6_WidevineLicense_Load);
            this.tabControlPlayReadySettings.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSecLevel)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.Button buttonOk;
        public System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TabControl tabControlPlayReadySettings;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage1;
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
    }
}