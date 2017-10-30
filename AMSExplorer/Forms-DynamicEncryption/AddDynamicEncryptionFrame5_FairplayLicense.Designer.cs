namespace AMSExplorer
{
    partial class AddDynamicEncryptionFrame5_FairplayLicense
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddDynamicEncryptionFrame5_FairplayLicense));
            this.buttonCancel = new System.Windows.Forms.Button();
            this.checkBoxEnableUnlimitedPersistent = new System.Windows.Forms.CheckBox();
            this.buttonOk = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelstep = new System.Windows.Forms.Label();
            this.openFileDialogPreset = new System.Windows.Forms.OpenFileDialog();
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
            // checkBoxEnableUnlimitedPersistent
            // 
            resources.ApplyResources(this.checkBoxEnableUnlimitedPersistent, "checkBoxEnableUnlimitedPersistent");
            this.checkBoxEnableUnlimitedPersistent.Name = "checkBoxEnableUnlimitedPersistent";
            this.checkBoxEnableUnlimitedPersistent.UseVisualStyleBackColor = true;
            this.checkBoxEnableUnlimitedPersistent.CheckedChanged += new System.EventHandler(this.checkBoxSecLevel_CheckedChanged);
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
            // AddDynamicEncryptionFrame5_FairplayLicense
            // 
            this.AcceptButton = this.buttonOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.labelstep);
            this.Controls.Add(this.checkBoxEnableUnlimitedPersistent);
            this.Controls.Add(this.panel1);
            this.Name = "AddDynamicEncryptionFrame5_FairplayLicense";
            this.Load += new System.EventHandler(this.AddDynamicEncryptionFrame5_FairplayLicense_Load);
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
        private System.Windows.Forms.CheckBox checkBoxEnableUnlimitedPersistent;
    }
}