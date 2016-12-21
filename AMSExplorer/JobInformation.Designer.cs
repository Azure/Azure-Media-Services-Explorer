namespace AMSExplorer
{
    partial class JobInformation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JobInformation));
            this.DGErrors = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemCopyClipboard = new System.Windows.Forms.ToolStripMenuItem();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.DGJob = new System.Windows.Forms.DataGridView();
            this.buttonCopyStats = new System.Windows.Forms.Button();
            this.buttonCreateMail = new System.Windows.Forms.Button();
            this.listBoxTasks = new System.Windows.Forms.ListBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.listViewOutputAssets = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStripOutputAsset = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.listViewInputAssets = new System.Windows.Forms.ListView();
            this.ListViewAssetName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ListViewAssetType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStripInputAsset = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.assetInformationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.DGTasks = new System.Windows.Forms.DataGridView();
            this.labelJobNameTitle = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.DGErrors)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGJob)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.contextMenuStripOutputAsset.SuspendLayout();
            this.contextMenuStripInputAsset.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGTasks)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // DGErrors
            // 
            this.DGErrors.AllowUserToAddRows = false;
            this.DGErrors.AllowUserToResizeRows = false;
            resources.ApplyResources(this.DGErrors, "DGErrors");
            this.DGErrors.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.DGErrors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGErrors.ContextMenuStrip = this.contextMenuStrip;
            this.DGErrors.MultiSelect = false;
            this.DGErrors.Name = "DGErrors";
            this.DGErrors.ReadOnly = true;
            this.DGErrors.RowHeadersVisible = false;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemCopyClipboard});
            this.contextMenuStrip.Name = "contextMenuStrip";
            resources.ApplyResources(this.contextMenuStrip, "contextMenuStrip");
            this.contextMenuStrip.MouseClick += new System.Windows.Forms.MouseEventHandler(this.contextMenuStrip_MouseClick);
            // 
            // toolStripMenuItemCopyClipboard
            // 
            this.toolStripMenuItemCopyClipboard.Name = "toolStripMenuItemCopyClipboard";
            resources.ApplyResources(this.toolStripMenuItemCopyClipboard, "toolStripMenuItemCopyClipboard");
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // buttonClose
            // 
            resources.ApplyResources(this.buttonClose, "buttonClose");
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.UseVisualStyleBackColor = true;
            // 
            // DGJob
            // 
            this.DGJob.AllowUserToAddRows = false;
            this.DGJob.AllowUserToDeleteRows = false;
            this.DGJob.AllowUserToResizeRows = false;
            resources.ApplyResources(this.DGJob, "DGJob");
            this.DGJob.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DGJob.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGJob.ColumnHeadersVisible = false;
            this.DGJob.ContextMenuStrip = this.contextMenuStrip;
            this.DGJob.MultiSelect = false;
            this.DGJob.Name = "DGJob";
            this.DGJob.ReadOnly = true;
            this.DGJob.RowHeadersVisible = false;
            // 
            // buttonCopyStats
            // 
            resources.ApplyResources(this.buttonCopyStats, "buttonCopyStats");
            this.buttonCopyStats.Name = "buttonCopyStats";
            this.buttonCopyStats.UseVisualStyleBackColor = true;
            this.buttonCopyStats.Click += new System.EventHandler(this.buttonCopyStats_Click);
            // 
            // buttonCreateMail
            // 
            resources.ApplyResources(this.buttonCreateMail, "buttonCreateMail");
            this.buttonCreateMail.Name = "buttonCreateMail";
            this.buttonCreateMail.UseVisualStyleBackColor = true;
            this.buttonCreateMail.Click += new System.EventHandler(this.buttonCreateMail_Click);
            // 
            // listBoxTasks
            // 
            resources.ApplyResources(this.listBoxTasks, "listBoxTasks");
            this.listBoxTasks.FormattingEnabled = true;
            this.listBoxTasks.Name = "listBoxTasks";
            this.listBoxTasks.SelectedIndexChanged += new System.EventHandler(this.listBoxTasks_SelectedIndexChanged);
            // 
            // tabControl1
            // 
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.buttonCopyStats);
            this.tabPage1.Controls.Add(this.buttonCreateMail);
            this.tabPage1.Controls.Add(this.DGErrors);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.DGJob);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.listViewOutputAssets);
            this.tabPage3.Controls.Add(this.label2);
            this.tabPage3.Controls.Add(this.label1);
            this.tabPage3.Controls.Add(this.listViewInputAssets);
            resources.ApplyResources(this.tabPage3, "tabPage3");
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // listViewOutputAssets
            // 
            resources.ApplyResources(this.listViewOutputAssets, "listViewOutputAssets");
            this.listViewOutputAssets.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listViewOutputAssets.ContextMenuStrip = this.contextMenuStripOutputAsset;
            this.listViewOutputAssets.FullRowSelect = true;
            this.listViewOutputAssets.HideSelection = false;
            this.listViewOutputAssets.Name = "listViewOutputAssets";
            this.listViewOutputAssets.UseCompatibleStateImageBehavior = false;
            this.listViewOutputAssets.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            resources.ApplyResources(this.columnHeader1, "columnHeader1");
            // 
            // columnHeader2
            // 
            resources.ApplyResources(this.columnHeader2, "columnHeader2");
            // 
            // contextMenuStripOutputAsset
            // 
            this.contextMenuStripOutputAsset.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.contextMenuStripOutputAsset.Name = "contextMenuStripAsset";
            resources.ApplyResources(this.contextMenuStripOutputAsset, "contextMenuStripOutputAsset");
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Image = global::AMSExplorer.Bitmaps.Display_information;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // listViewInputAssets
            // 
            resources.ApplyResources(this.listViewInputAssets, "listViewInputAssets");
            this.listViewInputAssets.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ListViewAssetName,
            this.ListViewAssetType});
            this.listViewInputAssets.ContextMenuStrip = this.contextMenuStripInputAsset;
            this.listViewInputAssets.FullRowSelect = true;
            this.listViewInputAssets.HideSelection = false;
            this.listViewInputAssets.Name = "listViewInputAssets";
            this.listViewInputAssets.UseCompatibleStateImageBehavior = false;
            this.listViewInputAssets.View = System.Windows.Forms.View.Details;
            // 
            // ListViewAssetName
            // 
            resources.ApplyResources(this.ListViewAssetName, "ListViewAssetName");
            // 
            // ListViewAssetType
            // 
            resources.ApplyResources(this.ListViewAssetType, "ListViewAssetType");
            // 
            // contextMenuStripInputAsset
            // 
            this.contextMenuStripInputAsset.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.assetInformationToolStripMenuItem});
            this.contextMenuStripInputAsset.Name = "contextMenuStripInputAsset";
            resources.ApplyResources(this.contextMenuStripInputAsset, "contextMenuStripInputAsset");
            // 
            // assetInformationToolStripMenuItem
            // 
            this.assetInformationToolStripMenuItem.Image = global::AMSExplorer.Bitmaps.Display_information;
            this.assetInformationToolStripMenuItem.Name = "assetInformationToolStripMenuItem";
            resources.ApplyResources(this.assetInformationToolStripMenuItem, "assetInformationToolStripMenuItem");
            this.assetInformationToolStripMenuItem.Click += new System.EventHandler(this.assetInformationToolStripMenuItem_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.DGTasks);
            this.tabPage2.Controls.Add(this.listBoxTasks);
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // DGTasks
            // 
            this.DGTasks.AllowUserToAddRows = false;
            this.DGTasks.AllowUserToDeleteRows = false;
            this.DGTasks.AllowUserToResizeRows = false;
            resources.ApplyResources(this.DGTasks, "DGTasks");
            this.DGTasks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DGTasks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGTasks.ColumnHeadersVisible = false;
            this.DGTasks.ContextMenuStrip = this.contextMenuStrip;
            this.DGTasks.MultiSelect = false;
            this.DGTasks.Name = "DGTasks";
            this.DGTasks.ReadOnly = true;
            this.DGTasks.RowHeadersVisible = false;
            this.DGTasks.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGTasks_CellContentClick);
            // 
            // labelJobNameTitle
            // 
            resources.ApplyResources(this.labelJobNameTitle, "labelJobNameTitle");
            this.labelJobNameTitle.Name = "labelJobNameTitle";
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonClose);
            this.panel1.Name = "panel1";
            // 
            // JobInformation
            // 
            this.AcceptButton = this.buttonClose;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonClose;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labelJobNameTitle);
            this.Controls.Add(this.tabControl1);
            this.Name = "JobInformation";
            this.Load += new System.EventHandler(this.JobInformation_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGErrors)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGJob)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.contextMenuStripOutputAsset.ResumeLayout(false);
            this.contextMenuStripInputAsset.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGTasks)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView DGErrors;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.DataGridView DGJob;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCopyClipboard;
        private System.Windows.Forms.Button buttonCopyStats;
        private System.Windows.Forms.Button buttonCreateMail;
        private System.Windows.Forms.ListBox listBoxTasks;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label labelJobNameTitle;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView DGTasks;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ListView listViewOutputAssets;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView listViewInputAssets;
        private System.Windows.Forms.ColumnHeader ListViewAssetName;
        private System.Windows.Forms.ColumnHeader ListViewAssetType;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripInputAsset;
        private System.Windows.Forms.ToolStripMenuItem assetInformationToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripOutputAsset;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
    }
}