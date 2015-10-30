namespace AMSExplorer
{
    partial class UploadBulk
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
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBoxCreateSubfolder = new System.Windows.Forms.CheckBox();
            this.radioButtonAssetId = new System.Windows.Forms.RadioButton();
            this.radioButtonAssetName = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridAssetFiles = new System.Windows.Forms.DataGridView();
            this.buttonAddFiles = new System.Windows.Forms.Button();
            this.buttonDelFiles = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxAssetName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridAssetFiles)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(510, 14);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(143, 27);
            this.buttonOk.TabIndex = 4;
            this.buttonOk.Text = "Generate Upload URL";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(660, 14);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(112, 27);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.checkBoxCreateSubfolder);
            this.groupBox2.Controls.Add(this.radioButtonAssetId);
            this.groupBox2.Controls.Add(this.radioButtonAssetName);
            this.groupBox2.Location = new System.Drawing.Point(14, 365);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(758, 104);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Options";
            // 
            // checkBoxCreateSubfolder
            // 
            this.checkBoxCreateSubfolder.AutoSize = true;
            this.checkBoxCreateSubfolder.Checked = true;
            this.checkBoxCreateSubfolder.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxCreateSubfolder.Location = new System.Drawing.Point(26, 22);
            this.checkBoxCreateSubfolder.Name = "checkBoxCreateSubfolder";
            this.checkBoxCreateSubfolder.Size = new System.Drawing.Size(179, 19);
            this.checkBoxCreateSubfolder.TabIndex = 72;
            this.checkBoxCreateSubfolder.Text = "Create a subfolder based on :";
            this.checkBoxCreateSubfolder.UseVisualStyleBackColor = true;
            // 
            // radioButtonAssetId
            // 
            this.radioButtonAssetId.AutoSize = true;
            this.radioButtonAssetId.Location = new System.Drawing.Point(55, 73);
            this.radioButtonAssetId.Name = "radioButtonAssetId";
            this.radioButtonAssetId.Size = new System.Drawing.Size(66, 19);
            this.radioButtonAssetId.TabIndex = 71;
            this.radioButtonAssetId.Text = "Asset Id";
            this.radioButtonAssetId.UseVisualStyleBackColor = true;
            // 
            // radioButtonAssetName
            // 
            this.radioButtonAssetName.AutoSize = true;
            this.radioButtonAssetName.Checked = true;
            this.radioButtonAssetName.Location = new System.Drawing.Point(55, 48);
            this.radioButtonAssetName.Name = "radioButtonAssetName";
            this.radioButtonAssetName.Size = new System.Drawing.Size(86, 19);
            this.radioButtonAssetName.TabIndex = 4;
            this.radioButtonAssetName.TabStop = true;
            this.radioButtonAssetName.Text = "Asset name";
            this.radioButtonAssetName.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonOk);
            this.panel1.Location = new System.Drawing.Point(-2, 507);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(787, 55);
            this.panel1.TabIndex = 60;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dataGridAssetFiles);
            this.groupBox1.Controls.Add(this.buttonAddFiles);
            this.groupBox1.Controls.Add(this.buttonDelFiles);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBoxAssetName);
            this.groupBox1.Location = new System.Drawing.Point(15, 51);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(757, 289);
            this.groupBox1.TabIndex = 61;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Local folder";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(22, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 15);
            this.label2.TabIndex = 77;
            this.label2.Text = "Files :";
            // 
            // dataGridAssetFiles
            // 
            this.dataGridAssetFiles.AllowUserToAddRows = false;
            this.dataGridAssetFiles.AllowUserToDeleteRows = false;
            this.dataGridAssetFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridAssetFiles.Location = new System.Drawing.Point(22, 104);
            this.dataGridAssetFiles.Name = "dataGridAssetFiles";
            this.dataGridAssetFiles.RowHeadersVisible = false;
            this.dataGridAssetFiles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridAssetFiles.Size = new System.Drawing.Size(392, 122);
            this.dataGridAssetFiles.TabIndex = 74;
            // 
            // buttonAddFiles
            // 
            this.buttonAddFiles.Location = new System.Drawing.Point(22, 230);
            this.buttonAddFiles.Name = "buttonAddFiles";
            this.buttonAddFiles.Size = new System.Drawing.Size(87, 27);
            this.buttonAddFiles.TabIndex = 75;
            this.buttonAddFiles.Text = "Add";
            this.buttonAddFiles.UseVisualStyleBackColor = true;
            this.buttonAddFiles.Click += new System.EventHandler(this.buttonAddFiles_Click);
            // 
            // buttonDelFiles
            // 
            this.buttonDelFiles.Location = new System.Drawing.Point(117, 230);
            this.buttonDelFiles.Name = "buttonDelFiles";
            this.buttonDelFiles.Size = new System.Drawing.Size(87, 27);
            this.buttonDelFiles.TabIndex = 76;
            this.buttonDelFiles.Text = "Delete";
            this.buttonDelFiles.UseVisualStyleBackColor = true;
            this.buttonDelFiles.Click += new System.EventHandler(this.buttonDelFiles_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(22, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 15);
            this.label1.TabIndex = 73;
            this.label1.Text = "Asset Name :";
            // 
            // textBoxAssetName
            // 
            this.textBoxAssetName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxAssetName.Location = new System.Drawing.Point(22, 47);
            this.textBoxAssetName.Name = "textBoxAssetName";
            this.textBoxAssetName.Size = new System.Drawing.Size(622, 23);
            this.textBoxAssetName.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DarkBlue;
            this.label5.Location = new System.Drawing.Point(14, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(286, 20);
            this.label5.TabIndex = 72;
            this.label5.Text = "Upload Asset(s) with an external uploader";
            // 
            // UploadBulk
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox2);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "UploadBulk";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Upload asset(s)";
            this.Load += new System.EventHandler(this.UploadBulk_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridAssetFiles)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button buttonOk;
        public System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radioButtonAssetName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxAssetName;
        private System.Windows.Forms.CheckBox checkBoxCreateSubfolder;
        private System.Windows.Forms.RadioButton radioButtonAssetId;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridAssetFiles;
        private System.Windows.Forms.Button buttonAddFiles;
        private System.Windows.Forms.Button buttonDelFiles;
    }
}