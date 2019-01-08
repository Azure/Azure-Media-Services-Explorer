namespace AMSExplorer
{
    partial class TransformInformation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TransformInformation));
            this.DGErrors = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemCopyClipboard = new System.Windows.Forms.ToolStripMenuItem();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.DGTransform = new System.Windows.Forms.DataGridView();
            this.listBoxOutputs = new System.Windows.Forms.ListBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.listViewOutputs = new System.Windows.Forms.ListView();
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
            this.DGOutputs = new System.Windows.Forms.DataGridView();
            this.labelJobNameTitle = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.DGErrors)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGTransform)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.contextMenuStripOutputAsset.SuspendLayout();
            this.contextMenuStripInputAsset.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGOutputs)).BeginInit();
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
            // DGTransform
            // 
            this.DGTransform.AllowUserToAddRows = false;
            this.DGTransform.AllowUserToDeleteRows = false;
            this.DGTransform.AllowUserToResizeRows = false;
            resources.ApplyResources(this.DGTransform, "DGTransform");
            this.DGTransform.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DGTransform.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGTransform.ColumnHeadersVisible = false;
            this.DGTransform.ContextMenuStrip = this.contextMenuStrip;
            this.DGTransform.MultiSelect = false;
            this.DGTransform.Name = "DGTransform";
            this.DGTransform.ReadOnly = true;
            this.DGTransform.RowHeadersVisible = false;
            // 
            // listBoxOutputs
            // 
            resources.ApplyResources(this.listBoxOutputs, "listBoxOutputs");
            this.listBoxOutputs.FormattingEnabled = true;
            this.listBoxOutputs.Name = "listBoxOutputs";
            this.listBoxOutputs.SelectedIndexChanged += new System.EventHandler(this.listBoxOutputs_SelectedIndexChanged);
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
            this.tabPage1.Controls.Add(this.DGErrors);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.DGTransform);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.listViewOutputs);
            this.tabPage3.Controls.Add(this.label2);
            this.tabPage3.Controls.Add(this.label1);
            this.tabPage3.Controls.Add(this.listViewInputAssets);
            resources.ApplyResources(this.tabPage3, "tabPage3");
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // listViewOutputs
            // 
            resources.ApplyResources(this.listViewOutputs, "listViewOutputs");
            this.listViewOutputs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listViewOutputs.ContextMenuStrip = this.contextMenuStripOutputAsset;
            this.listViewOutputs.FullRowSelect = true;
            this.listViewOutputs.HideSelection = false;
            this.listViewOutputs.Name = "listViewOutputs";
            this.listViewOutputs.UseCompatibleStateImageBehavior = false;
            this.listViewOutputs.View = System.Windows.Forms.View.Details;
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
            this.tabPage2.Controls.Add(this.DGOutputs);
            this.tabPage2.Controls.Add(this.listBoxOutputs);
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // DGOutputs
            // 
            this.DGOutputs.AllowUserToAddRows = false;
            this.DGOutputs.AllowUserToDeleteRows = false;
            this.DGOutputs.AllowUserToResizeRows = false;
            resources.ApplyResources(this.DGOutputs, "DGOutputs");
            this.DGOutputs.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DGOutputs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGOutputs.ColumnHeadersVisible = false;
            this.DGOutputs.ContextMenuStrip = this.contextMenuStrip;
            this.DGOutputs.MultiSelect = false;
            this.DGOutputs.Name = "DGOutputs";
            this.DGOutputs.ReadOnly = true;
            this.DGOutputs.RowHeadersVisible = false;
            this.DGOutputs.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGTasks_CellContentClick);
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
            // TransformInformation
            // 
            this.AcceptButton = this.buttonClose;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonClose;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labelJobNameTitle);
            this.Controls.Add(this.tabControl1);
            this.Name = "TransformInformation";
            this.Load += new System.EventHandler(this.JobInformation_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGErrors)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGTransform)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.contextMenuStripOutputAsset.ResumeLayout(false);
            this.contextMenuStripInputAsset.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGOutputs)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView DGErrors;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.DataGridView DGTransform;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCopyClipboard;
        private System.Windows.Forms.ListBox listBoxOutputs;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label labelJobNameTitle;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView DGOutputs;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ListView listViewOutputs;
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