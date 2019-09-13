﻿namespace AMSExplorer
{
    partial class DRM_FairPlayLicense
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DRM_FairPlayLicense));
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelstep = new System.Windows.Forms.Label();
            this.openFileDialogPreset = new System.Windows.Forms.OpenFileDialog();
            this.radioButtonNonPersistent = new System.Windows.Forms.RadioButton();
            this.radioButtonPersistent = new System.Windows.Forms.RadioButton();
            this.numericUpDownRentalHours = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBoxLimited = new System.Windows.Forms.CheckBox();
            this.panelPersistent = new System.Windows.Forms.Panel();
            this.textBoxPolicyName = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRentalHours)).BeginInit();
            this.panelPersistent.SuspendLayout();
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
            // radioButtonNonPersistent
            // 
            resources.ApplyResources(this.radioButtonNonPersistent, "radioButtonNonPersistent");
            this.radioButtonNonPersistent.Checked = true;
            this.radioButtonNonPersistent.Name = "radioButtonNonPersistent";
            this.radioButtonNonPersistent.TabStop = true;
            this.radioButtonNonPersistent.UseVisualStyleBackColor = true;
            // 
            // radioButtonPersistent
            // 
            resources.ApplyResources(this.radioButtonPersistent, "radioButtonPersistent");
            this.radioButtonPersistent.Name = "radioButtonPersistent";
            this.radioButtonPersistent.UseVisualStyleBackColor = true;
            this.radioButtonPersistent.CheckedChanged += new System.EventHandler(this.radioButtonPersistent_CheckedChanged);
            // 
            // numericUpDownRentalHours
            // 
            resources.ApplyResources(this.numericUpDownRentalHours, "numericUpDownRentalHours");
            this.numericUpDownRentalHours.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDownRentalHours.Name = "numericUpDownRentalHours";
            this.numericUpDownRentalHours.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // checkBoxLimited
            // 
            resources.ApplyResources(this.checkBoxLimited, "checkBoxLimited");
            this.checkBoxLimited.Name = "checkBoxLimited";
            this.checkBoxLimited.UseVisualStyleBackColor = true;
            this.checkBoxLimited.CheckedChanged += new System.EventHandler(this.checkBoxLimited_CheckedChanged);
            // 
            // panelPersistent
            // 
            this.panelPersistent.Controls.Add(this.numericUpDownRentalHours);
            this.panelPersistent.Controls.Add(this.checkBoxLimited);
            this.panelPersistent.Controls.Add(this.label3);
            resources.ApplyResources(this.panelPersistent, "panelPersistent");
            this.panelPersistent.Name = "panelPersistent";
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
            // DRM_FairPlayLicense
            // 
            this.AcceptButton = this.buttonOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.textBoxPolicyName);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.panelPersistent);
            this.Controls.Add(this.radioButtonPersistent);
            this.Controls.Add(this.radioButtonNonPersistent);
            this.Controls.Add(this.labelstep);
            this.Controls.Add(this.panel1);
            this.Name = "DRM_FairPlayLicense";
            this.Load += new System.EventHandler(this.DRM_FairPlayLicense_Load);
            this.DpiChanged += new System.Windows.Forms.DpiChangedEventHandler(this.DRM_FairPlayLicense_DpiChanged);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRentalHours)).EndInit();
            this.panelPersistent.ResumeLayout(false);
            this.panelPersistent.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.Button buttonOk;
        public System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelstep;
        private System.Windows.Forms.OpenFileDialog openFileDialogPreset;
        private System.Windows.Forms.RadioButton radioButtonNonPersistent;
        private System.Windows.Forms.RadioButton radioButtonPersistent;
        private System.Windows.Forms.NumericUpDown numericUpDownRentalHours;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBoxLimited;
        private System.Windows.Forms.Panel panelPersistent;
        private System.Windows.Forms.TextBox textBoxPolicyName;
        private System.Windows.Forms.Label label13;
    }
}