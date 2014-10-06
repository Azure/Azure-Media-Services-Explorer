namespace AMSExplorer
{
    partial class BathUploadFrame1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BathUploadFrame1));
            this.buttonNext = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSelectFolder = new System.Windows.Forms.Button();
            this.textBoxFolder = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxSubFolder = new System.Windows.Forms.CheckBox();
            this.checkBoxProcessFiles = new System.Windows.Forms.CheckBox();
            this.checkBoxOneUpDownload = new System.Windows.Forms.CheckBox();
            this.checkBoxUseStorageEncryption = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonNext
            // 
            this.buttonNext.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonNext.Location = new System.Drawing.Point(398, 326);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(124, 23);
            this.buttonNext.TabIndex = 0;
            this.buttonNext.Text = "Next";
            this.buttonNext.UseVisualStyleBackColor = true;
            this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(317, 326);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonSelectFolder
            // 
            this.buttonSelectFolder.Location = new System.Drawing.Point(103, 54);
            this.buttonSelectFolder.Name = "buttonSelectFolder";
            this.buttonSelectFolder.Size = new System.Drawing.Size(140, 23);
            this.buttonSelectFolder.TabIndex = 2;
            this.buttonSelectFolder.Text = "Select a folder...";
            this.buttonSelectFolder.UseVisualStyleBackColor = true;
            this.buttonSelectFolder.Click += new System.EventHandler(this.buttonSelectFolder_Click);
            // 
            // textBoxFolder
            // 
            this.textBoxFolder.Location = new System.Drawing.Point(103, 28);
            this.textBoxFolder.Name = "textBoxFolder";
            this.textBoxFolder.Size = new System.Drawing.Size(419, 20);
            this.textBoxFolder.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Enter a path :";
            // 
            // checkBoxSubFolder
            // 
            this.checkBoxSubFolder.AutoSize = true;
            this.checkBoxSubFolder.Checked = true;
            this.checkBoxSubFolder.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSubFolder.Location = new System.Drawing.Point(18, 42);
            this.checkBoxSubFolder.Name = "checkBoxSubFolder";
            this.checkBoxSubFolder.Size = new System.Drawing.Size(308, 17);
            this.checkBoxSubFolder.TabIndex = 5;
            this.checkBoxSubFolder.Text = "Process subfolders - each subfolder will be a multi files asset";
            this.checkBoxSubFolder.UseVisualStyleBackColor = true;
            // 
            // checkBoxProcessFiles
            // 
            this.checkBoxProcessFiles.AutoSize = true;
            this.checkBoxProcessFiles.Checked = true;
            this.checkBoxProcessFiles.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxProcessFiles.Location = new System.Drawing.Point(18, 19);
            this.checkBoxProcessFiles.Name = "checkBoxProcessFiles";
            this.checkBoxProcessFiles.Size = new System.Drawing.Size(125, 17);
            this.checkBoxProcessFiles.TabIndex = 6;
            this.checkBoxProcessFiles.Text = "Process files in folder";
            this.checkBoxProcessFiles.UseVisualStyleBackColor = true;
            // 
            // checkBoxOneUpDownload
            // 
            this.checkBoxOneUpDownload.AutoSize = true;
            this.checkBoxOneUpDownload.Enabled = false;
            this.checkBoxOneUpDownload.Location = new System.Drawing.Point(18, 19);
            this.checkBoxOneUpDownload.Name = "checkBoxOneUpDownload";
            this.checkBoxOneUpDownload.Size = new System.Drawing.Size(192, 17);
            this.checkBoxOneUpDownload.TabIndex = 11;
            this.checkBoxOneUpDownload.Text = "One upload at a time (use a queue)";
            this.checkBoxOneUpDownload.UseVisualStyleBackColor = true;
            // 
            // checkBoxUseStorageEncryption
            // 
            this.checkBoxUseStorageEncryption.AutoSize = true;
            this.checkBoxUseStorageEncryption.Enabled = false;
            this.checkBoxUseStorageEncryption.Location = new System.Drawing.Point(18, 42);
            this.checkBoxUseStorageEncryption.Name = "checkBoxUseStorageEncryption";
            this.checkBoxUseStorageEncryption.Size = new System.Drawing.Size(206, 17);
            this.checkBoxUseStorageEncryption.TabIndex = 12;
            this.checkBoxUseStorageEncryption.Text = "Use storage encryption for new assets";
            this.checkBoxUseStorageEncryption.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxOneUpDownload);
            this.groupBox1.Controls.Add(this.checkBoxUseStorageEncryption);
            this.groupBox1.Location = new System.Drawing.Point(26, 199);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(496, 77);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Application options";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBoxProcessFiles);
            this.groupBox2.Controls.Add(this.checkBoxSubFolder);
            this.groupBox2.Location = new System.Drawing.Point(26, 114);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(496, 69);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Path processing";
            // 
            // BathUploadFrame1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(544, 361);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxFolder);
            this.Controls.Add(this.buttonSelectFolder);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonNext);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BathUploadFrame1";
            this.Text = "Batch Upload";
            this.Load += new System.EventHandler(this.BathUploadFrame1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSelectFolder;
        private System.Windows.Forms.TextBox textBoxFolder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxSubFolder;
        private System.Windows.Forms.CheckBox checkBoxProcessFiles;
        private System.Windows.Forms.CheckBox checkBoxOneUpDownload;
        private System.Windows.Forms.CheckBox checkBoxUseStorageEncryption;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}