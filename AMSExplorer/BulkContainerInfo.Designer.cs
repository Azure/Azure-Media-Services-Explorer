namespace AMSExplorer
{
    partial class BulkContainerInfo
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
            this.components = new System.ComponentModel.Container();
            this.contextMenuStripDG = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemFilesCopyClipboard = new System.Windows.Forms.ToolStripMenuItem();
            this.folderBrowserDialogDownload = new System.Windows.Forms.FolderBrowserDialog();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.DGBulkManifest = new System.Windows.Forms.DataGridView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.DGAssetManifest = new System.Windows.Forms.DataGridView();
            this.listViewAssetManifests = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.labelPBulkName = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonUpdateClose = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.contextMenuStripDG.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGBulkManifest)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGAssetManifest)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStripDG
            // 
            this.contextMenuStripDG.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemFilesCopyClipboard});
            this.contextMenuStripDG.Name = "contextMenuStripDG";
            this.contextMenuStripDG.Size = new System.Drawing.Size(170, 26);
            this.contextMenuStripDG.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripDG_Opening);
            this.contextMenuStripDG.MouseClick += new System.Windows.Forms.MouseEventHandler(this.contextMenuStripDG_MouseClick);
            // 
            // toolStripMenuItemFilesCopyClipboard
            // 
            this.toolStripMenuItemFilesCopyClipboard.Name = "toolStripMenuItemFilesCopyClipboard";
            this.toolStripMenuItemFilesCopyClipboard.Size = new System.Drawing.Size(169, 22);
            this.toolStripMenuItemFilesCopyClipboard.Text = "Copy to clipboard";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.DGBulkManifest);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(748, 420);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Bulk container information";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // DGBulkManifest
            // 
            this.DGBulkManifest.AllowUserToAddRows = false;
            this.DGBulkManifest.AllowUserToDeleteRows = false;
            this.DGBulkManifest.AllowUserToResizeRows = false;
            this.DGBulkManifest.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DGBulkManifest.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DGBulkManifest.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGBulkManifest.ColumnHeadersVisible = false;
            this.DGBulkManifest.ContextMenuStrip = this.contextMenuStripDG;
            this.DGBulkManifest.Location = new System.Drawing.Point(7, 7);
            this.DGBulkManifest.MultiSelect = false;
            this.DGBulkManifest.Name = "DGBulkManifest";
            this.DGBulkManifest.ReadOnly = true;
            this.DGBulkManifest.RowHeadersVisible = false;
            this.DGBulkManifest.Size = new System.Drawing.Size(732, 402);
            this.DGBulkManifest.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(14, 51);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(756, 448);
            this.tabControl1.TabIndex = 34;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.textBoxName);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(748, 417);
            this.tabPage2.TabIndex = 3;
            this.tabPage2.Text = "Settings";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 15);
            this.label2.TabIndex = 52;
            this.label2.Text = "Name :";
            // 
            // textBoxName
            // 
            this.textBoxName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxName.Location = new System.Drawing.Point(23, 39);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(699, 23);
            this.textBoxName.TabIndex = 51;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.DGAssetManifest);
            this.tabPage3.Controls.Add(this.listViewAssetManifests);
            this.tabPage3.Location = new System.Drawing.Point(4, 24);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(748, 417);
            this.tabPage3.TabIndex = 4;
            this.tabPage3.Text = "Assets";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // DGAssetManifest
            // 
            this.DGAssetManifest.AllowUserToAddRows = false;
            this.DGAssetManifest.AllowUserToDeleteRows = false;
            this.DGAssetManifest.AllowUserToResizeRows = false;
            this.DGAssetManifest.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DGAssetManifest.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DGAssetManifest.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGAssetManifest.ColumnHeadersVisible = false;
            this.DGAssetManifest.ContextMenuStrip = this.contextMenuStripDG;
            this.DGAssetManifest.Location = new System.Drawing.Point(382, 12);
            this.DGAssetManifest.MultiSelect = false;
            this.DGAssetManifest.Name = "DGAssetManifest";
            this.DGAssetManifest.ReadOnly = true;
            this.DGAssetManifest.RowHeadersVisible = false;
            this.DGAssetManifest.Size = new System.Drawing.Size(358, 392);
            this.DGAssetManifest.TabIndex = 34;
            // 
            // listViewAssetManifests
            // 
            this.listViewAssetManifests.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listViewAssetManifests.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listViewAssetManifests.FullRowSelect = true;
            this.listViewAssetManifests.HideSelection = false;
            this.listViewAssetManifests.Location = new System.Drawing.Point(7, 12);
            this.listViewAssetManifests.MultiSelect = false;
            this.listViewAssetManifests.Name = "listViewAssetManifests";
            this.listViewAssetManifests.Size = new System.Drawing.Size(367, 392);
            this.listViewAssetManifests.TabIndex = 35;
            this.listViewAssetManifests.UseCompatibleStateImageBehavior = false;
            this.listViewAssetManifests.View = System.Windows.Forms.View.Details;
            this.listViewAssetManifests.SelectedIndexChanged += new System.EventHandler(this.listViewAssetManifest_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 25;
            // 
            // labelPBulkName
            // 
            this.labelPBulkName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPBulkName.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.labelPBulkName.Location = new System.Drawing.Point(21, 10);
            this.labelPBulkName.Name = "labelPBulkName";
            this.labelPBulkName.Size = new System.Drawing.Size(737, 23);
            this.labelPBulkName.TabIndex = 38;
            this.labelPBulkName.Text = "Bulk container name : ";
            this.labelPBulkName.Click += new System.EventHandler(this.labelProgramName_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.Location = new System.Drawing.Point(659, 15);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(115, 27);
            this.buttonClose.TabIndex = 41;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            // 
            // buttonUpdateClose
            // 
            this.buttonUpdateClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonUpdateClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonUpdateClose.Location = new System.Drawing.Point(466, 15);
            this.buttonUpdateClose.Name = "buttonUpdateClose";
            this.buttonUpdateClose.Size = new System.Drawing.Size(185, 27);
            this.buttonUpdateClose.TabIndex = 40;
            this.buttonUpdateClose.Text = "Update settings and close";
            this.buttonUpdateClose.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonUpdateClose);
            this.panel1.Controls.Add(this.buttonClose);
            this.panel1.Location = new System.Drawing.Point(-5, 506);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(791, 55);
            this.panel1.TabIndex = 63;
            // 
            // BulkContainerInfo
            // 
            this.AcceptButton = this.buttonClose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonClose;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labelPBulkName);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "BulkContainerInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Program Information";
            this.Load += new System.EventHandler(this.ProgramInformation_Load_1);
            this.Shown += new System.EventHandler(this.ProgramInformation_Shown);
            this.contextMenuStripDG.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGBulkManifest)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGAssetManifest)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStripDG;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFilesCopyClipboard;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogDownload;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView DGBulkManifest;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Label labelPBulkName;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonUpdateClose;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView DGAssetManifest;
        private System.Windows.Forms.ListView listViewAssetManifests;
        private System.Windows.Forms.ColumnHeader columnHeader1;
    }
}