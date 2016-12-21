namespace AMSExplorer
{
    partial class JobOptions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JobOptions));
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label33 = new System.Windows.Forms.Label();
            this.comboBoxStorage = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.numericUpDownPriority = new System.Windows.Forms.NumericUpDown();
            this.checkBoxUseProtectedConfig = new System.Windows.Forms.CheckBox();
            this.checkBoxUseStorageEncryption = new System.Windows.Forms.CheckBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelJobOptions = new System.Windows.Forms.Label();
            this.checkBoxDoNotDeleteOutputAssetOnFailure = new System.Windows.Forms.CheckBox();
            this.checkBoxDoNotCancelOnJobFailure = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBoxFragmented = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPriority)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
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
            // label33
            // 
            resources.ApplyResources(this.label33, "label33");
            this.label33.Name = "label33";
            // 
            // comboBoxStorage
            // 
            this.comboBoxStorage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStorage.FormattingEnabled = true;
            resources.ApplyResources(this.comboBoxStorage, "comboBoxStorage");
            this.comboBoxStorage.Name = "comboBoxStorage";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // numericUpDownPriority
            // 
            resources.ApplyResources(this.numericUpDownPriority, "numericUpDownPriority");
            this.numericUpDownPriority.Name = "numericUpDownPriority";
            // 
            // checkBoxUseProtectedConfig
            // 
            resources.ApplyResources(this.checkBoxUseProtectedConfig, "checkBoxUseProtectedConfig");
            this.checkBoxUseProtectedConfig.Name = "checkBoxUseProtectedConfig";
            this.checkBoxUseProtectedConfig.UseVisualStyleBackColor = true;
            // 
            // checkBoxUseStorageEncryption
            // 
            resources.ApplyResources(this.checkBoxUseStorageEncryption, "checkBoxUseStorageEncryption");
            this.checkBoxUseStorageEncryption.Name = "checkBoxUseStorageEncryption";
            this.checkBoxUseStorageEncryption.UseVisualStyleBackColor = true;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::AMSExplorer.Bitmaps.change_priority;
            resources.ApplyResources(this.pictureBox2, "pictureBox2");
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::AMSExplorer.Bitmaps.storage_encryption;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // dataGridViewTextBoxColumn1
            // 
            resources.ApplyResources(this.dataGridViewTextBoxColumn1, "dataGridViewTextBoxColumn1");
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            resources.ApplyResources(this.dataGridViewTextBoxColumn2, "dataGridViewTextBoxColumn2");
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // labelJobOptions
            // 
            resources.ApplyResources(this.labelJobOptions, "labelJobOptions");
            this.labelJobOptions.ForeColor = System.Drawing.Color.DarkBlue;
            this.labelJobOptions.Name = "labelJobOptions";
            // 
            // checkBoxDoNotDeleteOutputAssetOnFailure
            // 
            resources.ApplyResources(this.checkBoxDoNotDeleteOutputAssetOnFailure, "checkBoxDoNotDeleteOutputAssetOnFailure");
            this.checkBoxDoNotDeleteOutputAssetOnFailure.Name = "checkBoxDoNotDeleteOutputAssetOnFailure";
            this.checkBoxDoNotDeleteOutputAssetOnFailure.UseVisualStyleBackColor = true;
            // 
            // checkBoxDoNotCancelOnJobFailure
            // 
            resources.ApplyResources(this.checkBoxDoNotCancelOnJobFailure, "checkBoxDoNotCancelOnJobFailure");
            this.checkBoxDoNotCancelOnJobFailure.Name = "checkBoxDoNotCancelOnJobFailure";
            this.checkBoxDoNotCancelOnJobFailure.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numericUpDownPriority);
            this.groupBox1.Controls.Add(this.checkBoxDoNotCancelOnJobFailure);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.pictureBox2);
            this.groupBox1.Controls.Add(this.checkBoxUseProtectedConfig);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBoxFragmented);
            this.groupBox2.Controls.Add(this.comboBoxStorage);
            this.groupBox2.Controls.Add(this.label33);
            this.groupBox2.Controls.Add(this.checkBoxDoNotDeleteOutputAssetOnFailure);
            this.groupBox2.Controls.Add(this.pictureBox1);
            this.groupBox2.Controls.Add(this.checkBoxUseStorageEncryption);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // checkBoxFragmented
            // 
            resources.ApplyResources(this.checkBoxFragmented, "checkBoxFragmented");
            this.checkBoxFragmented.Name = "checkBoxFragmented";
            this.checkBoxFragmented.UseVisualStyleBackColor = true;
            // 
            // JobOptions
            // 
            this.AcceptButton = this.buttonOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.labelJobOptions);
            this.Controls.Add(this.panel1);
            this.Name = "JobOptions";
            this.Load += new System.EventHandler(this.JobOptions_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPriority)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button buttonOk;
        public System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.ComboBox comboBoxStorage;
        public System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numericUpDownPriority;
        private System.Windows.Forms.CheckBox checkBoxUseProtectedConfig;
        private System.Windows.Forms.CheckBox checkBoxUseStorageEncryption;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label labelJobOptions;
        private System.Windows.Forms.CheckBox checkBoxDoNotDeleteOutputAssetOnFailure;
        private System.Windows.Forms.CheckBox checkBoxDoNotCancelOnJobFailure;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkBoxFragmented;
    }
}