namespace AMSExplorer
{
    partial class BatchUploadFrame2
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
            this.checkedListBoxFiles = new System.Windows.Forms.CheckedListBox();
            this.checkedListBoxFolders = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.buttonFolderDeselAll = new System.Windows.Forms.Button();
            this.buttonFolderSelAll = new System.Windows.Forms.Button();
            this.buttonFileDeselAll = new System.Windows.Forms.Button();
            this.buttonFileSelAll = new System.Windows.Forms.Button();
            this.label33 = new System.Windows.Forms.Label();
            this.comboBoxStorage = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonNext
            // 
            this.buttonNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonNext.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonNext.Image = global::AMSExplorer.Bitmaps.upload;
            this.buttonNext.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonNext.Location = new System.Drawing.Point(381, 13);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(145, 27);
            this.buttonNext.TabIndex = 0;
            this.buttonNext.Text = "Launch upload";
            this.buttonNext.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(533, 13);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(87, 27);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // checkedListBoxFiles
            // 
            this.checkedListBoxFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedListBoxFiles.CheckOnClick = true;
            this.checkedListBoxFiles.FormattingEnabled = true;
            this.checkedListBoxFiles.Location = new System.Drawing.Point(7, 32);
            this.checkedListBoxFiles.Name = "checkedListBoxFiles";
            this.checkedListBoxFiles.Size = new System.Drawing.Size(587, 112);
            this.checkedListBoxFiles.TabIndex = 2;
            // 
            // checkedListBoxFolders
            // 
            this.checkedListBoxFolders.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedListBoxFolders.CheckOnClick = true;
            this.checkedListBoxFolders.FormattingEnabled = true;
            this.checkedListBoxFolders.Location = new System.Drawing.Point(7, 22);
            this.checkedListBoxFolders.Name = "checkedListBoxFolders";
            this.checkedListBoxFolders.Size = new System.Drawing.Size(587, 130);
            this.checkedListBoxFolders.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(211, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "Folders to upload as multi files assets : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(191, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Files to upload as single file assets :";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(14, 14);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.buttonFolderDeselAll);
            this.splitContainer1.Panel1.Controls.Add(this.buttonFolderSelAll);
            this.splitContainer1.Panel1.Controls.Add(this.checkedListBoxFolders);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel1_Paint);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.buttonFileDeselAll);
            this.splitContainer1.Panel2.Controls.Add(this.buttonFileSelAll);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.checkedListBoxFiles);
            this.splitContainer1.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel2_Paint);
            this.splitContainer1.Size = new System.Drawing.Size(607, 405);
            this.splitContainer1.SplitterDistance = 198;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 6;
            // 
            // buttonFolderDeselAll
            // 
            this.buttonFolderDeselAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonFolderDeselAll.Location = new System.Drawing.Point(101, 168);
            this.buttonFolderDeselAll.Name = "buttonFolderDeselAll";
            this.buttonFolderDeselAll.Size = new System.Drawing.Size(87, 27);
            this.buttonFolderDeselAll.TabIndex = 6;
            this.buttonFolderDeselAll.Text = "Deselect all";
            this.buttonFolderDeselAll.UseVisualStyleBackColor = true;
            this.buttonFolderDeselAll.Click += new System.EventHandler(this.buttonFolderDeselAll_Click);
            // 
            // buttonFolderSelAll
            // 
            this.buttonFolderSelAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonFolderSelAll.Location = new System.Drawing.Point(7, 168);
            this.buttonFolderSelAll.Name = "buttonFolderSelAll";
            this.buttonFolderSelAll.Size = new System.Drawing.Size(87, 27);
            this.buttonFolderSelAll.TabIndex = 5;
            this.buttonFolderSelAll.Text = "Select all";
            this.buttonFolderSelAll.UseVisualStyleBackColor = true;
            this.buttonFolderSelAll.Click += new System.EventHandler(this.buttonFolderSelAll_Click);
            // 
            // buttonFileDeselAll
            // 
            this.buttonFileDeselAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonFileDeselAll.Location = new System.Drawing.Point(101, 165);
            this.buttonFileDeselAll.Name = "buttonFileDeselAll";
            this.buttonFileDeselAll.Size = new System.Drawing.Size(87, 27);
            this.buttonFileDeselAll.TabIndex = 8;
            this.buttonFileDeselAll.Text = "Deselect all";
            this.buttonFileDeselAll.UseVisualStyleBackColor = true;
            this.buttonFileDeselAll.Click += new System.EventHandler(this.buttonFileDeselAll_Click);
            // 
            // buttonFileSelAll
            // 
            this.buttonFileSelAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonFileSelAll.Location = new System.Drawing.Point(7, 165);
            this.buttonFileSelAll.Name = "buttonFileSelAll";
            this.buttonFileSelAll.Size = new System.Drawing.Size(87, 27);
            this.buttonFileSelAll.TabIndex = 7;
            this.buttonFileSelAll.Text = "Select all";
            this.buttonFileSelAll.UseVisualStyleBackColor = true;
            this.buttonFileSelAll.Click += new System.EventHandler(this.buttonFileSelAll_Click);
            // 
            // label33
            // 
            this.label33.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(10, 436);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(93, 15);
            this.label33.TabIndex = 55;
            this.label33.Text = "Output storage :";
            // 
            // comboBoxStorage
            // 
            this.comboBoxStorage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBoxStorage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStorage.FormattingEnabled = true;
            this.comboBoxStorage.Location = new System.Drawing.Point(14, 455);
            this.comboBoxStorage.Name = "comboBoxStorage";
            this.comboBoxStorage.Size = new System.Drawing.Size(327, 23);
            this.comboBoxStorage.TabIndex = 54;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonNext);
            this.panel1.Location = new System.Drawing.Point(0, 502);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(636, 55);
            this.panel1.TabIndex = 56;
            // 
            // BatchUploadFrame2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(635, 555);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label33);
            this.Controls.Add(this.comboBoxStorage);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "BatchUploadFrame2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Batch Upload";
            this.Load += new System.EventHandler(this.BathUploadFrame2_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.CheckedListBox checkedListBoxFiles;
        private System.Windows.Forms.CheckedListBox checkedListBoxFolders;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button buttonFolderDeselAll;
        private System.Windows.Forms.Button buttonFolderSelAll;
        private System.Windows.Forms.Button buttonFileDeselAll;
        private System.Windows.Forms.Button buttonFileSelAll;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.ComboBox comboBoxStorage;
        private System.Windows.Forms.Panel panel1;
    }
}