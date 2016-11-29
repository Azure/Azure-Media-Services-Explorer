using Microsoft.WindowsAzure.Storage.Shared.Protocol;

namespace AMSExplorer
{
    partial class EncodingMESPickOverlay
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EncodingMESPickOverlay));
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSelect = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelStorageAccount = new System.Windows.Forms.Label();
            this.listViewFiles = new System.Windows.Forms.ListView();
            this.ListViewFilesName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ListViewFilesSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.progressBarUpload = new System.Windows.Forms.ProgressBar();
            this.buttonUpload = new System.Windows.Forms.Button();
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
            // buttonSelect
            // 
            resources.ApplyResources(this.buttonSelect, "buttonSelect");
            this.buttonSelect.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonSelect.Name = "buttonSelect";
            this.buttonSelect.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonSelect);
            this.panel1.Name = "panel1";
            // 
            // labelStorageAccount
            // 
            resources.ApplyResources(this.labelStorageAccount, "labelStorageAccount");
            this.labelStorageAccount.ForeColor = System.Drawing.Color.DarkBlue;
            this.labelStorageAccount.Name = "labelStorageAccount";
            // 
            // listViewFiles
            // 
            resources.ApplyResources(this.listViewFiles, "listViewFiles");
            this.listViewFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ListViewFilesName,
            this.ListViewFilesSize});
            this.listViewFiles.FullRowSelect = true;
            this.listViewFiles.HideSelection = false;
            this.listViewFiles.MultiSelect = false;
            this.listViewFiles.Name = "listViewFiles";
            this.listViewFiles.UseCompatibleStateImageBehavior = false;
            this.listViewFiles.View = System.Windows.Forms.View.Details;
            this.listViewFiles.SelectedIndexChanged += new System.EventHandler(this.listViewFiles_SelectedIndexChanged);
            // 
            // ListViewFilesName
            // 
            resources.ApplyResources(this.ListViewFilesName, "ListViewFilesName");
            // 
            // ListViewFilesSize
            // 
            resources.ApplyResources(this.ListViewFilesSize, "ListViewFilesSize");
            // 
            // progressBarUpload
            // 
            resources.ApplyResources(this.progressBarUpload, "progressBarUpload");
            this.progressBarUpload.Name = "progressBarUpload";
            // 
            // buttonUpload
            // 
            resources.ApplyResources(this.buttonUpload, "buttonUpload");
            this.buttonUpload.Name = "buttonUpload";
            this.buttonUpload.UseVisualStyleBackColor = true;
            this.buttonUpload.Click += new System.EventHandler(this.buttonUpload_Click);
            // 
            // EncodingMESPickOverlay
            // 
            this.AcceptButton = this.buttonSelect;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.progressBarUpload);
            this.Controls.Add(this.buttonUpload);
            this.Controls.Add(this.listViewFiles);
            this.Controls.Add(this.labelStorageAccount);
            this.Controls.Add(this.panel1);
            this.Name = "EncodingMESPickOverlay";
            this.Load += new System.EventHandler(this.EncodingAMEStandardPickOverlay_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button buttonSelect;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelStorageAccount;
        private System.Windows.Forms.ListView listViewFiles;
        private System.Windows.Forms.ColumnHeader ListViewFilesName;
        private System.Windows.Forms.ColumnHeader ListViewFilesSize;
        private System.Windows.Forms.ProgressBar progressBarUpload;
        private System.Windows.Forms.Button buttonUpload;
    }
}