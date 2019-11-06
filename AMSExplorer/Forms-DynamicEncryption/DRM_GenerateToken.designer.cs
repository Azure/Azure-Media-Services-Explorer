namespace AMSExplorer
{
    partial class DRM_GenerateToken
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DRM_GenerateToken));
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelstep = new System.Windows.Forms.Label();
            this.openFileDialogPreset = new System.Windows.Forms.OpenFileDialog();
            this.label8 = new System.Windows.Forms.Label();
            this.numericUpDownTokenDuration = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBoxTokenUse = new System.Windows.Forms.CheckBox();
            this.numericUpDownTokenUse = new System.Windows.Forms.NumericUpDown();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTokenDuration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTokenUse)).BeginInit();
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
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // numericUpDownTokenDuration
            // 
            resources.ApplyResources(this.numericUpDownTokenDuration, "numericUpDownTokenDuration");
            this.numericUpDownTokenDuration.Maximum = new decimal(new int[] {
            36500,
            0,
            0,
            0});
            this.numericUpDownTokenDuration.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownTokenDuration.Name = "numericUpDownTokenDuration";
            this.numericUpDownTokenDuration.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // checkBoxTokenUse
            // 
            resources.ApplyResources(this.checkBoxTokenUse, "checkBoxTokenUse");
            this.checkBoxTokenUse.Name = "checkBoxTokenUse";
            this.checkBoxTokenUse.UseVisualStyleBackColor = true;
            this.checkBoxTokenUse.CheckedChanged += new System.EventHandler(this.CheckBoxTokenUse_CheckedChanged);
            // 
            // numericUpDownTokenUse
            // 
            resources.ApplyResources(this.numericUpDownTokenUse, "numericUpDownTokenUse");
            this.numericUpDownTokenUse.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownTokenUse.Name = "numericUpDownTokenUse";
            this.numericUpDownTokenUse.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // DRM_GenerateToken
            // 
            this.AcceptButton = this.buttonOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.numericUpDownTokenUse);
            this.Controls.Add(this.checkBoxTokenUse);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.numericUpDownTokenDuration);
            this.Controls.Add(this.labelstep);
            this.Controls.Add(this.panel1);
            this.Name = "DRM_GenerateToken";
            this.Load += new System.EventHandler(this.DRM_WidevineLicense_Load);
            this.DpiChanged += new System.Windows.Forms.DpiChangedEventHandler(this.DRM_GenerateToken_DpiChanged);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTokenDuration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTokenUse)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.Button buttonOk;
        public System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelstep;
        private System.Windows.Forms.OpenFileDialog openFileDialogPreset;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numericUpDownTokenDuration;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBoxTokenUse;
        private System.Windows.Forms.NumericUpDown numericUpDownTokenUse;
    }
}