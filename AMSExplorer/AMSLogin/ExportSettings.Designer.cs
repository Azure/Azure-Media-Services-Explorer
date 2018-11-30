namespace AMSExplorer
{
    partial class ExportSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExportSettings));
            this.buttonCancel = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonImport = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.radioButtonCurrentEntry = new System.Windows.Forms.RadioButton();
            this.radioButtonAllEntries = new System.Windows.Forms.RadioButton();
            this.checkBoxIncludeSPSecrets = new System.Windows.Forms.CheckBox();
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
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonImport);
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Name = "panel1";
            // 
            // buttonImport
            // 
            resources.ApplyResources(this.buttonImport, "buttonImport");
            this.buttonImport.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonImport.Name = "buttonImport";
            this.buttonImport.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.ForeColor = System.Drawing.Color.DarkBlue;
            this.label2.Name = "label2";
            // 
            // radioButtonCurrentEntry
            // 
            resources.ApplyResources(this.radioButtonCurrentEntry, "radioButtonCurrentEntry");
            this.radioButtonCurrentEntry.Checked = true;
            this.radioButtonCurrentEntry.Name = "radioButtonCurrentEntry";
            this.radioButtonCurrentEntry.TabStop = true;
            this.radioButtonCurrentEntry.UseVisualStyleBackColor = true;
            // 
            // radioButtonAllEntries
            // 
            resources.ApplyResources(this.radioButtonAllEntries, "radioButtonAllEntries");
            this.radioButtonAllEntries.Name = "radioButtonAllEntries";
            this.radioButtonAllEntries.UseVisualStyleBackColor = true;
            // 
            // checkBoxIncludeSPSecrets
            // 
            resources.ApplyResources(this.checkBoxIncludeSPSecrets, "checkBoxIncludeSPSecrets");
            this.checkBoxIncludeSPSecrets.Name = "checkBoxIncludeSPSecrets";
            this.checkBoxIncludeSPSecrets.UseVisualStyleBackColor = true;
            // 
            // ExportSettings
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.checkBoxIncludeSPSecrets);
            this.Controls.Add(this.radioButtonAllEntries);
            this.Controls.Add(this.radioButtonCurrentEntry);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.Name = "ExportSettings";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonImport;
        public System.Windows.Forms.RadioButton radioButtonCurrentEntry;
        public System.Windows.Forms.RadioButton radioButtonAllEntries;
        public System.Windows.Forms.CheckBox checkBoxIncludeSPSecrets;
    }
}