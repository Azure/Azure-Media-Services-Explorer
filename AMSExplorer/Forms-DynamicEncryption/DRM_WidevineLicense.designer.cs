namespace AMSExplorer
{
    partial class DRM_WidevineLicense
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DRM_WidevineLicense));
            this.buttonCancel = new System.Windows.Forms.Button();
            this.radioButtonAdvanced = new System.Windows.Forms.RadioButton();
            this.radioButtonBasic = new System.Windows.Forms.RadioButton();
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
            this.radioButtonAdvanced.Checked = true;
            this.radioButtonAdvanced.Name = "radioButtonAdvanced";
            this.radioButtonAdvanced.TabStop = true;
            this.radioButtonAdvanced.UseVisualStyleBackColor = true;
            this.radioButtonAdvanced.CheckedChanged += new System.EventHandler(this.radioButtonAdvanced_CheckedChanged);
            // 
            // radioButtonBasic
            // 
            resources.ApplyResources(this.radioButtonBasic, "radioButtonBasic");
            this.radioButtonBasic.Name = "radioButtonBasic";
            this.radioButtonBasic.UseVisualStyleBackColor = true;
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
            // DRM_WidevineLicense
            // 
            this.AcceptButton = this.buttonOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
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
            this.Controls.Add(this.panel1);
            this.Name = "DRM_WidevineLicense";
            this.Load += new System.EventHandler(this.DRM_WidevineLicense_Load);
            this.DpiChanged += new System.Windows.Forms.DpiChangedEventHandler(this.DRM_WidevineLicense_DpiChanged);
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
        private System.Windows.Forms.RadioButton radioButtonAdvanced;
        private System.Windows.Forms.RadioButton radioButtonBasic;
        private System.Windows.Forms.Label labelWarningJSON;
        private System.Windows.Forms.TextBox textBoxConfiguration;
        public System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel linkLabelWidevinePolicy;
        private System.Windows.Forms.TextBox textBoxPolicyName;
        private System.Windows.Forms.Label label13;
    }
}