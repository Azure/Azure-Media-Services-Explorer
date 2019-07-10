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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BatchUploadFrame2));
            this.buttonUpload = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.checkedListBoxFolders = new System.Windows.Forms.ListView();
            this.buttonFolderDeselAll = new System.Windows.Forms.Button();
            this.buttonFolderSelAll = new System.Windows.Forms.Button();
            this.checkedListBoxFiles = new System.Windows.Forms.ListView();
            this.buttonFileDeselAll = new System.Windows.Forms.Button();
            this.buttonFileSelAll = new System.Windows.Forms.Button();
            this.label33 = new System.Windows.Forms.Label();
            this.comboBoxStorage = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxBlockSize = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonUpload
            // 
            resources.ApplyResources(this.buttonUpload, "buttonUpload");
            this.buttonUpload.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonUpload.Image = global::AMSExplorer.Bitmaps.upload;
            this.buttonUpload.Name = "buttonUpload";
            this.buttonUpload.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.checkedListBoxFolders);
            this.splitContainer1.Panel1.Controls.Add(this.buttonFolderDeselAll);
            this.splitContainer1.Panel1.Controls.Add(this.buttonFolderSelAll);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel1_Paint);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.checkedListBoxFiles);
            this.splitContainer1.Panel2.Controls.Add(this.buttonFileDeselAll);
            this.splitContainer1.Panel2.Controls.Add(this.buttonFileSelAll);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel2_Paint);
            // 
            // checkedListBoxFolders
            // 
            resources.ApplyResources(this.checkedListBoxFolders, "checkedListBoxFolders");
            this.checkedListBoxFolders.CheckBoxes = true;
            this.checkedListBoxFolders.HideSelection = false;
            this.checkedListBoxFolders.Name = "checkedListBoxFolders";
            this.checkedListBoxFolders.UseCompatibleStateImageBehavior = false;
            this.checkedListBoxFolders.View = System.Windows.Forms.View.List;
            // 
            // buttonFolderDeselAll
            // 
            resources.ApplyResources(this.buttonFolderDeselAll, "buttonFolderDeselAll");
            this.buttonFolderDeselAll.Name = "buttonFolderDeselAll";
            this.buttonFolderDeselAll.UseVisualStyleBackColor = true;
            this.buttonFolderDeselAll.Click += new System.EventHandler(this.buttonFolderDeselAll_Click);
            // 
            // buttonFolderSelAll
            // 
            resources.ApplyResources(this.buttonFolderSelAll, "buttonFolderSelAll");
            this.buttonFolderSelAll.Name = "buttonFolderSelAll";
            this.buttonFolderSelAll.UseVisualStyleBackColor = true;
            this.buttonFolderSelAll.Click += new System.EventHandler(this.buttonFolderSelAll_Click);
            // 
            // checkedListBoxFiles
            // 
            resources.ApplyResources(this.checkedListBoxFiles, "checkedListBoxFiles");
            this.checkedListBoxFiles.CheckBoxes = true;
            this.checkedListBoxFiles.GridLines = true;
            this.checkedListBoxFiles.HideSelection = false;
            this.checkedListBoxFiles.Name = "checkedListBoxFiles";
            this.checkedListBoxFiles.UseCompatibleStateImageBehavior = false;
            this.checkedListBoxFiles.View = System.Windows.Forms.View.List;
            // 
            // buttonFileDeselAll
            // 
            resources.ApplyResources(this.buttonFileDeselAll, "buttonFileDeselAll");
            this.buttonFileDeselAll.Name = "buttonFileDeselAll";
            this.buttonFileDeselAll.UseVisualStyleBackColor = true;
            this.buttonFileDeselAll.Click += new System.EventHandler(this.buttonFileDeselAll_Click);
            // 
            // buttonFileSelAll
            // 
            resources.ApplyResources(this.buttonFileSelAll, "buttonFileSelAll");
            this.buttonFileSelAll.Name = "buttonFileSelAll";
            this.buttonFileSelAll.UseVisualStyleBackColor = true;
            this.buttonFileSelAll.Click += new System.EventHandler(this.buttonFileSelAll_Click);
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
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonUpload);
            this.panel1.Name = "panel1";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label4.Name = "label4";
            // 
            // comboBoxBlockSize
            // 
            resources.ApplyResources(this.comboBoxBlockSize, "comboBoxBlockSize");
            this.comboBoxBlockSize.FormattingEnabled = true;
            this.comboBoxBlockSize.Items.AddRange(new object[] {
            resources.GetString("comboBoxBlockSize.Items"),
            resources.GetString("comboBoxBlockSize.Items1"),
            resources.GetString("comboBoxBlockSize.Items2"),
            resources.GetString("comboBoxBlockSize.Items3"),
            resources.GetString("comboBoxBlockSize.Items4"),
            resources.GetString("comboBoxBlockSize.Items5"),
            resources.GetString("comboBoxBlockSize.Items6"),
            resources.GetString("comboBoxBlockSize.Items7")});
            this.comboBoxBlockSize.Name = "comboBoxBlockSize";
            // 
            // BatchUploadFrame2
            // 
            this.AcceptButton = this.buttonUpload;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboBoxBlockSize);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label33);
            this.Controls.Add(this.comboBoxStorage);
            this.Controls.Add(this.splitContainer1);
            this.Name = "BatchUploadFrame2";
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

        private System.Windows.Forms.Button buttonUpload;
        private System.Windows.Forms.Button buttonCancel;
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
        private System.Windows.Forms.ListView checkedListBoxFolders;
        private System.Windows.Forms.ListView checkedListBoxFiles;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxBlockSize;
    }
}