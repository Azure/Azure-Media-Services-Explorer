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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddDynamicEncryptionFrame6_WidevineLicense));
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
            this.textBoxPolicyName = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBoxAdvLicense.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSecLevel)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // radioButtonAdvanced
            // 
            resources.ApplyResources(this.radioButtonAdvanced, "radioButtonAdvanced");
            this.radioButtonAdvanced.Name = "radioButtonAdvanced";
            this.radioButtonAdvanced.UseVisualStyleBackColor = true;
            this.radioButtonAdvanced.CheckedChanged += new System.EventHandler(this.radioButtonAdvanced_CheckedChanged);
            // 
            // radioButtonBasic
            // 
            resources.ApplyResources(this.radioButtonBasic, "radioButtonBasic");
            this.radioButtonBasic.Checked = true;
            this.radioButtonBasic.Name = "radioButtonBasic";
            this.radioButtonBasic.TabStop = true;
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
            resources.ApplyResources(this.groupBoxAdvLicense, "groupBoxAdvLicense");
            this.groupBoxAdvLicense.Name = "groupBoxAdvLicense";
            this.groupBoxAdvLicense.TabStop = false;
            // 
            // checkBoxCanPlay
            // 
            resources.ApplyResources(this.checkBoxCanPlay, "checkBoxCanPlay");
            this.checkBoxCanPlay.Checked = true;
            this.checkBoxCanPlay.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxCanPlay.Name = "checkBoxCanPlay";
            this.checkBoxCanPlay.UseVisualStyleBackColor = true;
            this.checkBoxCanPlay.CheckedChanged += new System.EventHandler(this.StateChanged);
            // 
            // checkBoxAllowTrackType
            // 
            resources.ApplyResources(this.checkBoxAllowTrackType, "checkBoxAllowTrackType");
            this.checkBoxAllowTrackType.Checked = true;
            this.checkBoxAllowTrackType.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAllowTrackType.Name = "checkBoxAllowTrackType";
            this.checkBoxAllowTrackType.UseVisualStyleBackColor = true;
            this.checkBoxAllowTrackType.CheckedChanged += new System.EventHandler(this.checkBoxAllowTrackType_CheckedChanged);
            // 
            // checkBoxCanPersist
            // 
            resources.ApplyResources(this.checkBoxCanPersist, "checkBoxCanPersist");
            this.checkBoxCanPersist.Checked = true;
            this.checkBoxCanPersist.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxCanPersist.Name = "checkBoxCanPersist";
            this.checkBoxCanPersist.UseVisualStyleBackColor = true;
            this.checkBoxCanPersist.CheckedChanged += new System.EventHandler(this.StateChanged);
            // 
            // comboBoxAllowedTrackTypes
            // 
            this.comboBoxAllowedTrackTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAllowedTrackTypes.FormattingEnabled = true;
            resources.ApplyResources(this.comboBoxAllowedTrackTypes, "comboBoxAllowedTrackTypes");
            this.comboBoxAllowedTrackTypes.Name = "comboBoxAllowedTrackTypes";
            this.comboBoxAllowedTrackTypes.SelectedIndexChanged += new System.EventHandler(this.StateChanged);
            // 
            // checkBoxTrackType
            // 
            resources.ApplyResources(this.checkBoxTrackType, "checkBoxTrackType");
            this.checkBoxTrackType.Checked = true;
            this.checkBoxTrackType.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxTrackType.Name = "checkBoxTrackType";
            this.checkBoxTrackType.UseVisualStyleBackColor = true;
            this.checkBoxTrackType.CheckedChanged += new System.EventHandler(this.checkBoxTrackType_CheckedChanged);
            // 
            // checkBoxCanRenew
            // 
            resources.ApplyResources(this.checkBoxCanRenew, "checkBoxCanRenew");
            this.checkBoxCanRenew.Name = "checkBoxCanRenew";
            this.checkBoxCanRenew.UseVisualStyleBackColor = true;
            this.checkBoxCanRenew.CheckedChanged += new System.EventHandler(this.StateChanged);
            // 
            // checkBoxSecLevel
            // 
            resources.ApplyResources(this.checkBoxSecLevel, "checkBoxSecLevel");
            this.checkBoxSecLevel.Checked = true;
            this.checkBoxSecLevel.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSecLevel.Name = "checkBoxSecLevel";
            this.checkBoxSecLevel.UseVisualStyleBackColor = true;
            this.checkBoxSecLevel.CheckedChanged += new System.EventHandler(this.checkBoxSecLevel_CheckedChanged);
            // 
            // comboBoxReqOutputProtection
            // 
            this.comboBoxReqOutputProtection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxReqOutputProtection.FormattingEnabled = true;
            resources.ApplyResources(this.comboBoxReqOutputProtection, "comboBoxReqOutputProtection");
            this.comboBoxReqOutputProtection.Name = "comboBoxReqOutputProtection";
            this.comboBoxReqOutputProtection.SelectedIndexChanged += new System.EventHandler(this.StateChanged);
            // 
            // numericUpDownSecLevel
            // 
            resources.ApplyResources(this.numericUpDownSecLevel, "numericUpDownSecLevel");
            this.numericUpDownSecLevel.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDownSecLevel.Name = "numericUpDownSecLevel";
            this.numericUpDownSecLevel.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownSecLevel.ValueChanged += new System.EventHandler(this.StateChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // textBoxTrackType
            // 
            resources.ApplyResources(this.textBoxTrackType, "textBoxTrackType");
            this.textBoxTrackType.Name = "textBoxTrackType";
            this.textBoxTrackType.TextChanged += new System.EventHandler(this.StateChanged);
            // 
            // buttonOk
            // 
            resources.ApplyResources(this.buttonOk, "buttonOk");
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Image = global::AMSExplorer.Bitmaps.DRM_protection;
            this.buttonOk.Name = "buttonOk";
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
            // labelstep
            // 
            resources.ApplyResources(this.labelstep, "labelstep");
            this.labelstep.ForeColor = System.Drawing.Color.DarkBlue;
            this.labelstep.Name = "labelstep";
            // 
            // openFileDialogPreset
            // 
            this.openFileDialogPreset.DefaultExt = "xml";
            resources.ApplyResources(this.openFileDialogPreset, "openFileDialogPreset");
            // 
            // labelWarningJSON
            // 
            resources.ApplyResources(this.labelWarningJSON, "labelWarningJSON");
            this.labelWarningJSON.ForeColor = System.Drawing.Color.Red;
            this.labelWarningJSON.Name = "labelWarningJSON";
            // 
            // textBoxConfiguration
            // 
            resources.ApplyResources(this.textBoxConfiguration, "textBoxConfiguration");
            this.textBoxConfiguration.Name = "textBoxConfiguration";
            this.textBoxConfiguration.TextChanged += new System.EventHandler(this.textBoxConfiguration_TextChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // linkLabelWidevinePolicy
            // 
            resources.ApplyResources(this.linkLabelWidevinePolicy, "linkLabelWidevinePolicy");
            this.linkLabelWidevinePolicy.Name = "linkLabelWidevinePolicy";
            this.linkLabelWidevinePolicy.TabStop = true;
            this.linkLabelWidevinePolicy.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelWidevinePolicy_LinkClicked);
            // 
            // textBoxPolicyName
            // 
            resources.ApplyResources(this.textBoxPolicyName, "textBoxPolicyName");
            this.textBoxPolicyName.Name = "textBoxPolicyName";
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            // 
            // AddDynamicEncryptionFrame6_WidevineLicense
            // 
            this.AcceptButton = this.buttonOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.textBoxPolicyName);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.linkLabelWidevinePolicy);
            this.Controls.Add(this.labelWarningJSON);
            this.Controls.Add(this.textBoxConfiguration);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.radioButtonAdvanced);
            this.Controls.Add(this.labelstep);
            this.Controls.Add(this.radioButtonBasic);
            this.Controls.Add(this.groupBoxAdvLicense);
            this.Controls.Add(this.panel1);
            this.Name = "AddDynamicEncryptionFrame6_WidevineLicense";
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
        private System.Windows.Forms.TextBox textBoxPolicyName;
        private System.Windows.Forms.Label label13;
    }
}