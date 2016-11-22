namespace AMSExplorer
{
    partial class UploadOptions
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
            this.buttonOk = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label33 = new System.Windows.Forms.Label();
            this.comboBoxStorage = new System.Windows.Forms.ComboBox();
            this.checkBoxUseStorageEncryption = new System.Windows.Forms.CheckBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelJobOptions = new System.Windows.Forms.Label();
            this.radioButtonOneAssetPerFile = new System.Windows.Forms.RadioButton();
            this.radioButtonSingleAsset = new System.Windows.Forms.RadioButton();
            this.groupBoxMultifiles = new System.Windows.Forms.GroupBox();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBoxMultifiles.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(273, 15);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(115, 27);
            this.buttonCancel.TabIndex = 16;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonOk.Location = new System.Drawing.Point(151, 15);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(115, 27);
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
            this.panel1.Location = new System.Drawing.Point(-2, 257);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(403, 55);
            this.panel1.TabIndex = 66;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(44, 45);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(93, 15);
            this.label33.TabIndex = 70;
            this.label33.Text = "Output storage :";
            // 
            // comboBoxStorage
            // 
            this.comboBoxStorage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStorage.FormattingEnabled = true;
            this.comboBoxStorage.Location = new System.Drawing.Point(45, 63);
            this.comboBoxStorage.Name = "comboBoxStorage";
            this.comboBoxStorage.Size = new System.Drawing.Size(221, 23);
            this.comboBoxStorage.TabIndex = 69;
            // 
            // checkBoxUseStorageEncryption
            // 
            this.checkBoxUseStorageEncryption.AutoSize = true;
            this.checkBoxUseStorageEncryption.Location = new System.Drawing.Point(45, 101);
            this.checkBoxUseStorageEncryption.Name = "checkBoxUseStorageEncryption";
            this.checkBoxUseStorageEncryption.Size = new System.Drawing.Size(171, 19);
            this.checkBoxUseStorageEncryption.TabIndex = 72;
            this.checkBoxUseStorageEncryption.Text = "Storage encrypt the asset(s)";
            this.checkBoxUseStorageEncryption.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::AMSExplorer.Bitmaps.storage_encryption;
            this.pictureBox1.Location = new System.Drawing.Point(14, 101);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 73;
            this.pictureBox1.TabStop = false;
            // 
            // labelJobOptions
            // 
            this.labelJobOptions.AutoSize = true;
            this.labelJobOptions.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelJobOptions.ForeColor = System.Drawing.Color.DarkBlue;
            this.labelJobOptions.Location = new System.Drawing.Point(12, 9);
            this.labelJobOptions.Name = "labelJobOptions";
            this.labelJobOptions.Size = new System.Drawing.Size(114, 20);
            this.labelJobOptions.TabIndex = 75;
            this.labelJobOptions.Text = "Upload Options";
            // 
            // radioButtonOneAssetPerFile
            // 
            this.radioButtonOneAssetPerFile.AutoSize = true;
            this.radioButtonOneAssetPerFile.Checked = true;
            this.radioButtonOneAssetPerFile.Location = new System.Drawing.Point(25, 34);
            this.radioButtonOneAssetPerFile.Name = "radioButtonOneAssetPerFile";
            this.radioButtonOneAssetPerFile.Size = new System.Drawing.Size(113, 19);
            this.radioButtonOneAssetPerFile.TabIndex = 76;
            this.radioButtonOneAssetPerFile.TabStop = true;
            this.radioButtonOneAssetPerFile.Text = "one asset per file";
            this.radioButtonOneAssetPerFile.UseVisualStyleBackColor = true;
            // 
            // radioButtonSingleAsset
            // 
            this.radioButtonSingleAsset.AutoSize = true;
            this.radioButtonSingleAsset.Location = new System.Drawing.Point(25, 59);
            this.radioButtonSingleAsset.Name = "radioButtonSingleAsset";
            this.radioButtonSingleAsset.Size = new System.Drawing.Size(146, 19);
            this.radioButtonSingleAsset.TabIndex = 77;
            this.radioButtonSingleAsset.Text = "all files in a single asset";
            this.radioButtonSingleAsset.UseVisualStyleBackColor = true;
            // 
            // groupBoxMultifiles
            // 
            this.groupBoxMultifiles.Controls.Add(this.radioButtonOneAssetPerFile);
            this.groupBoxMultifiles.Controls.Add(this.radioButtonSingleAsset);
            this.groupBoxMultifiles.Location = new System.Drawing.Point(16, 138);
            this.groupBoxMultifiles.Name = "groupBoxMultifiles";
            this.groupBoxMultifiles.Size = new System.Drawing.Size(372, 100);
            this.groupBoxMultifiles.TabIndex = 78;
            this.groupBoxMultifiles.TabStop = false;
            this.groupBoxMultifiles.Text = "Multi files";
            this.groupBoxMultifiles.Visible = false;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Key";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Value";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // UploadOptions
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(400, 313);
            this.Controls.Add(this.groupBoxMultifiles);
            this.Controls.Add(this.labelJobOptions);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.checkBoxUseStorageEncryption);
            this.Controls.Add(this.label33);
            this.Controls.Add(this.comboBoxStorage);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "UploadOptions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Upload";
            this.Load += new System.EventHandler(this.UploadOptions_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
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
        private System.Windows.Forms.CheckBox checkBoxUseStorageEncryption;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label labelJobOptions;
        private System.Windows.Forms.RadioButton radioButtonOneAssetPerFile;
        private System.Windows.Forms.RadioButton radioButtonSingleAsset;
        private System.Windows.Forms.GroupBox groupBoxMultifiles;
    }
}