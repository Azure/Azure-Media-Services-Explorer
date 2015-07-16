namespace AMSExplorer
{
    partial class BatchUploadFrame1
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonNext
            // 
            this.buttonNext.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonNext.Location = new System.Drawing.Point(442, 14);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(87, 27);
            this.buttonNext.TabIndex = 0;
            this.buttonNext.Text = "Next";
            this.buttonNext.UseVisualStyleBackColor = true;
            this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(537, 14);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(87, 27);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonSelectFolder
            // 
            this.buttonSelectFolder.Location = new System.Drawing.Point(120, 62);
            this.buttonSelectFolder.Name = "buttonSelectFolder";
            this.buttonSelectFolder.Size = new System.Drawing.Size(163, 27);
            this.buttonSelectFolder.TabIndex = 2;
            this.buttonSelectFolder.Text = "Select a folder...";
            this.buttonSelectFolder.UseVisualStyleBackColor = true;
            this.buttonSelectFolder.Click += new System.EventHandler(this.buttonSelectFolder_Click);
            // 
            // textBoxFolder
            // 
            this.textBoxFolder.Location = new System.Drawing.Point(120, 32);
            this.textBoxFolder.Name = "textBoxFolder";
            this.textBoxFolder.Size = new System.Drawing.Size(488, 23);
            this.textBoxFolder.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "Enter a path :";
            // 
            // checkBoxSubFolder
            // 
            this.checkBoxSubFolder.AutoSize = true;
            this.checkBoxSubFolder.Checked = true;
            this.checkBoxSubFolder.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSubFolder.Location = new System.Drawing.Point(21, 48);
            this.checkBoxSubFolder.Name = "checkBoxSubFolder";
            this.checkBoxSubFolder.Size = new System.Drawing.Size(343, 19);
            this.checkBoxSubFolder.TabIndex = 5;
            this.checkBoxSubFolder.Text = "Process subfolders - each subfolder will be a multi files asset";
            this.checkBoxSubFolder.UseVisualStyleBackColor = true;
            // 
            // checkBoxProcessFiles
            // 
            this.checkBoxProcessFiles.AutoSize = true;
            this.checkBoxProcessFiles.Checked = true;
            this.checkBoxProcessFiles.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxProcessFiles.Location = new System.Drawing.Point(21, 22);
            this.checkBoxProcessFiles.Name = "checkBoxProcessFiles";
            this.checkBoxProcessFiles.Size = new System.Drawing.Size(137, 19);
            this.checkBoxProcessFiles.TabIndex = 6;
            this.checkBoxProcessFiles.Text = "Process files in folder";
            this.checkBoxProcessFiles.UseVisualStyleBackColor = true;
            // 
            // checkBoxOneUpDownload
            // 
            this.checkBoxOneUpDownload.AutoSize = true;
            this.checkBoxOneUpDownload.Enabled = false;
            this.checkBoxOneUpDownload.Location = new System.Drawing.Point(21, 22);
            this.checkBoxOneUpDownload.Name = "checkBoxOneUpDownload";
            this.checkBoxOneUpDownload.Size = new System.Drawing.Size(211, 19);
            this.checkBoxOneUpDownload.TabIndex = 11;
            this.checkBoxOneUpDownload.Text = "One upload at a time (use a queue)";
            this.checkBoxOneUpDownload.UseVisualStyleBackColor = true;
            // 
            // checkBoxUseStorageEncryption
            // 
            this.checkBoxUseStorageEncryption.AutoSize = true;
            this.checkBoxUseStorageEncryption.Enabled = false;
            this.checkBoxUseStorageEncryption.Location = new System.Drawing.Point(21, 48);
            this.checkBoxUseStorageEncryption.Name = "checkBoxUseStorageEncryption";
            this.checkBoxUseStorageEncryption.Size = new System.Drawing.Size(224, 19);
            this.checkBoxUseStorageEncryption.TabIndex = 12;
            this.checkBoxUseStorageEncryption.Text = "Use storage encryption for new assets";
            this.checkBoxUseStorageEncryption.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxOneUpDownload);
            this.groupBox1.Controls.Add(this.checkBoxUseStorageEncryption);
            this.groupBox1.Location = new System.Drawing.Point(30, 230);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(579, 89);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Application options";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBoxProcessFiles);
            this.groupBox2.Controls.Add(this.checkBoxSubFolder);
            this.groupBox2.Location = new System.Drawing.Point(30, 132);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(579, 80);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Path processing";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonNext);
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Location = new System.Drawing.Point(-3, 362);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(639, 55);
            this.panel1.TabIndex = 54;
            // 
            // BatchUploadFrame1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(635, 417);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxFolder);
            this.Controls.Add(this.buttonSelectFolder);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "BatchUploadFrame1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Batch Upload";
            this.Load += new System.EventHandler(this.BathUploadFrame1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
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
        private System.Windows.Forms.Panel panel1;
    }
}