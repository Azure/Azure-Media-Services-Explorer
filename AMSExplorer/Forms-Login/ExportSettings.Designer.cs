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
            buttonCancel = new System.Windows.Forms.Button();
            panel1 = new System.Windows.Forms.Panel();
            buttonImport = new System.Windows.Forms.Button();
            label2 = new System.Windows.Forms.Label();
            radioButtonCurrentEntry = new System.Windows.Forms.RadioButton();
            radioButtonAllEntries = new System.Windows.Forms.RadioButton();
            checkBoxIncludeSPSecrets = new System.Windows.Forms.CheckBox();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // buttonCancel
            // 
            resources.ApplyResources(buttonCancel, "buttonCancel");
            buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            buttonCancel.Name = "buttonCancel";
            buttonCancel.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.BackColor = System.Drawing.SystemColors.Control;
            panel1.Controls.Add(buttonImport);
            panel1.Controls.Add(buttonCancel);
            resources.ApplyResources(panel1, "panel1");
            panel1.Name = "panel1";
            // 
            // buttonImport
            // 
            resources.ApplyResources(buttonImport, "buttonImport");
            buttonImport.DialogResult = System.Windows.Forms.DialogResult.OK;
            buttonImport.Name = "buttonImport";
            buttonImport.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.ForeColor = System.Drawing.Color.DarkBlue;
            label2.Name = "label2";
            // 
            // radioButtonCurrentEntry
            // 
            resources.ApplyResources(radioButtonCurrentEntry, "radioButtonCurrentEntry");
            radioButtonCurrentEntry.Checked = true;
            radioButtonCurrentEntry.Name = "radioButtonCurrentEntry";
            radioButtonCurrentEntry.TabStop = true;
            radioButtonCurrentEntry.UseVisualStyleBackColor = true;
            // 
            // radioButtonAllEntries
            // 
            resources.ApplyResources(radioButtonAllEntries, "radioButtonAllEntries");
            radioButtonAllEntries.Name = "radioButtonAllEntries";
            radioButtonAllEntries.UseVisualStyleBackColor = true;
            // 
            // checkBoxIncludeSPSecrets
            // 
            resources.ApplyResources(checkBoxIncludeSPSecrets, "checkBoxIncludeSPSecrets");
            checkBoxIncludeSPSecrets.Name = "checkBoxIncludeSPSecrets";
            checkBoxIncludeSPSecrets.UseVisualStyleBackColor = true;
            // 
            // ExportSettings
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            BackColor = System.Drawing.SystemColors.Window;
            CancelButton = buttonCancel;
            Controls.Add(checkBoxIncludeSPSecrets);
            Controls.Add(radioButtonAllEntries);
            Controls.Add(radioButtonCurrentEntry);
            Controls.Add(label2);
            Controls.Add(panel1);
            Name = "ExportSettings";
            Load += ExportSettings_Load;
            Shown += ExportSettings_Shown;
            DpiChanged += ExportSettings_DpiChanged;
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
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