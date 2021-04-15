namespace AMSExplorer
{
    partial class UploadOptionsUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UploadOptionsUI));
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label33 = new System.Windows.Forms.Label();
            this.comboBoxStorage = new System.Windows.Forms.ComboBox();
            this.labelJobOptions = new System.Windows.Forms.Label();
            this.radioButtonOneAssetPerFile = new System.Windows.Forms.RadioButton();
            this.radioButtonSingleAsset = new System.Windows.Forms.RadioButton();
            this.groupBoxMultifiles = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.labelBlockSize = new System.Windows.Forms.Label();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonAdvancedOptions = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.groupBoxMultifiles.SuspendLayout();
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
            resources.ApplyResources(this.comboBoxStorage, "comboBoxStorage");
            this.comboBoxStorage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStorage.FormattingEnabled = true;
            this.comboBoxStorage.Name = "comboBoxStorage";
            // 
            // labelJobOptions
            // 
            resources.ApplyResources(this.labelJobOptions, "labelJobOptions");
            this.labelJobOptions.ForeColor = System.Drawing.Color.DarkBlue;
            this.labelJobOptions.Name = "labelJobOptions";
            // 
            // radioButtonOneAssetPerFile
            // 
            resources.ApplyResources(this.radioButtonOneAssetPerFile, "radioButtonOneAssetPerFile");
            this.radioButtonOneAssetPerFile.Checked = true;
            this.radioButtonOneAssetPerFile.Name = "radioButtonOneAssetPerFile";
            this.radioButtonOneAssetPerFile.TabStop = true;
            this.radioButtonOneAssetPerFile.UseVisualStyleBackColor = true;
            // 
            // radioButtonSingleAsset
            // 
            resources.ApplyResources(this.radioButtonSingleAsset, "radioButtonSingleAsset");
            this.radioButtonSingleAsset.Name = "radioButtonSingleAsset";
            this.radioButtonSingleAsset.UseVisualStyleBackColor = true;
            // 
            // groupBoxMultifiles
            // 
            resources.ApplyResources(this.groupBoxMultifiles, "groupBoxMultifiles");
            this.groupBoxMultifiles.Controls.Add(this.radioButtonOneAssetPerFile);
            this.groupBoxMultifiles.Controls.Add(this.radioButtonSingleAsset);
            this.groupBoxMultifiles.Name = "groupBoxMultifiles";
            this.groupBoxMultifiles.TabStop = false;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label1.Name = "label1";
            // 
            // labelBlockSize
            // 
            resources.ApplyResources(this.labelBlockSize, "labelBlockSize");
            this.labelBlockSize.Name = "labelBlockSize";
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
            // buttonAdvancedOptions
            // 
            resources.ApplyResources(this.buttonAdvancedOptions, "buttonAdvancedOptions");
            this.buttonAdvancedOptions.Name = "buttonAdvancedOptions";
            this.buttonAdvancedOptions.UseVisualStyleBackColor = true;
            this.buttonAdvancedOptions.Click += new System.EventHandler(this.ButtonAdvancedOptions_Click);
            // 
            // UploadOptionsUI
            // 
            this.AcceptButton = this.buttonOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.buttonAdvancedOptions);
            this.Controls.Add(this.labelBlockSize);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBoxMultifiles);
            this.Controls.Add(this.labelJobOptions);
            this.Controls.Add(this.label33);
            this.Controls.Add(this.comboBoxStorage);
            this.Controls.Add(this.panel1);
            this.Name = "UploadOptionsUI";
            this.Load += new System.EventHandler(this.UploadOptions_Load);
            this.DpiChanged += new System.Windows.Forms.DpiChangedEventHandler(this.UploadOptions_DpiChanged);
            this.panel1.ResumeLayout(false);
            this.groupBoxMultifiles.ResumeLayout(false);
            this.groupBoxMultifiles.PerformLayout();
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
        private System.Windows.Forms.Label labelJobOptions;
        private System.Windows.Forms.RadioButton radioButtonOneAssetPerFile;
        private System.Windows.Forms.RadioButton radioButtonSingleAsset;
        private System.Windows.Forms.GroupBox groupBoxMultifiles;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelBlockSize;
        private System.Windows.Forms.Button buttonAdvancedOptions;
    }
}