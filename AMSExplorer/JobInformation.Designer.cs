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
            this.listBoxOutputs = new System.Windows.Forms.ListBox();
            this.contextMenuStripOutputAsset = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.dataGridInput = new System.Windows.Forms.DataGridView();
            this.listBoxInput = new System.Windows.Forms.ListBox();
            this.contextMenuStripInputAsset = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.assetInformationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.DGOutputs = new System.Windows.Forms.DataGridView();
            this.labelJobNameTitle = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.DGErrors)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGJob)).BeginInit();
            this.contextMenuStripOutputAsset.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridInput)).BeginInit();
            this.contextMenuStripInputAsset.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGOutputs)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // DGErrors
            // 
            resources.ApplyResources(this.DGErrors, "DGErrors");
            this.DGErrors.AllowUserToAddRows = false;
            this.DGErrors.AllowUserToResizeRows = false;
            this.DGErrors.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.DGErrors.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.DGErrors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGErrors.ContextMenuStrip = this.contextMenuStrip;
            this.DGErrors.MultiSelect = false;
            this.DGErrors.Name = "DGErrors";
            this.DGErrors.ReadOnly = true;
            this.DGErrors.RowHeadersVisible = false;
            // 
            // contextMenuStrip
            // 
            resources.ApplyResources(this.contextMenuStrip, "contextMenuStrip");
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemCopyClipboard});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.MouseClick += new System.Windows.Forms.MouseEventHandler(this.contextMenuStrip_MouseClick);
            // 
            // toolStripMenuItemCopyClipboard
            // 
            resources.ApplyResources(this.toolStripMenuItemCopyClipboard, "toolStripMenuItemCopyClipboard");
            this.toolStripMenuItemCopyClipboard.Name = "toolStripMenuItemCopyClipboard";
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
            resources.ApplyResources(this.DGJob, "DGJob");
            this.DGJob.AllowUserToAddRows = false;
            this.DGJob.AllowUserToDeleteRows = false;
            this.DGJob.AllowUserToResizeRows = false;
            this.DGJob.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DGJob.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
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
            // listBoxOutputs
            // 
            resources.ApplyResources(this.listBoxOutputs, "listBoxOutputs");
            this.listBoxOutputs.ContextMenuStrip = this.contextMenuStripOutputAsset;
            this.listBoxOutputs.FormattingEnabled = true;
            this.listBoxOutputs.Name = "listBoxOutputs";
            this.listBoxOutputs.SelectedIndexChanged += new System.EventHandler(this.listBoxOutputs_SelectedIndexChanged);
            // 
            // contextMenuStripOutputAsset
            // 
            resources.ApplyResources(this.contextMenuStripOutputAsset, "contextMenuStripOutputAsset");
            this.contextMenuStripOutputAsset.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.contextMenuStripOutputAsset.Name = "contextMenuStripAsset";
            // 
            // toolStripMenuItem1
            // 
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            this.toolStripMenuItem1.Image = global::AMSExplorer.Bitmaps.Display_information;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // tabControl1
            // 
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabPage1
            // 
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.buttonCopyStats);
            this.tabPage1.Controls.Add(this.DGErrors);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.DGJob);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // tabPage4
            // 
            resources.ApplyResources(this.tabPage4, "tabPage4");
            this.tabPage4.Controls.Add(this.dataGridInput);
            this.tabPage4.Controls.Add(this.listBoxInput);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // dataGridInput
            // 
            resources.ApplyResources(this.dataGridInput, "dataGridInput");
            this.dataGridInput.AllowUserToAddRows = false;
            this.dataGridInput.AllowUserToDeleteRows = false;
            this.dataGridInput.AllowUserToResizeRows = false;
            this.dataGridInput.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridInput.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridInput.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridInput.ColumnHeadersVisible = false;
            this.dataGridInput.ContextMenuStrip = this.contextMenuStrip;
            this.dataGridInput.MultiSelect = false;
            this.dataGridInput.Name = "dataGridInput";
            this.dataGridInput.ReadOnly = true;
            this.dataGridInput.RowHeadersVisible = false;
            // 
            // listBoxInput
            // 
            resources.ApplyResources(this.listBoxInput, "listBoxInput");
            this.listBoxInput.ContextMenuStrip = this.contextMenuStripInputAsset;
            this.listBoxInput.FormattingEnabled = true;
            this.listBoxInput.Name = "listBoxInput";
            this.listBoxInput.SelectedIndexChanged += new System.EventHandler(this.listBoxInput_SelectedIndexChanged);
            // 
            // contextMenuStripInputAsset
            // 
            resources.ApplyResources(this.contextMenuStripInputAsset, "contextMenuStripInputAsset");
            this.contextMenuStripInputAsset.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.assetInformationToolStripMenuItem});
            this.contextMenuStripInputAsset.Name = "contextMenuStripInputAsset";
            // 
            // assetInformationToolStripMenuItem
            // 
            resources.ApplyResources(this.assetInformationToolStripMenuItem, "assetInformationToolStripMenuItem");
            this.assetInformationToolStripMenuItem.Image = global::AMSExplorer.Bitmaps.Display_information;
            this.assetInformationToolStripMenuItem.Name = "assetInformationToolStripMenuItem";
            this.assetInformationToolStripMenuItem.Click += new System.EventHandler(this.assetInformationToolStripMenuItem_Click);
            // 
            // tabPage2
            // 
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Controls.Add(this.DGOutputs);
            this.tabPage2.Controls.Add(this.listBoxOutputs);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // DGOutputs
            // 
            resources.ApplyResources(this.DGOutputs, "DGOutputs");
            this.DGOutputs.AllowUserToAddRows = false;
            this.DGOutputs.AllowUserToDeleteRows = false;
            this.DGOutputs.AllowUserToResizeRows = false;
            this.DGOutputs.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DGOutputs.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
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
            // JobInformation
            // 
            this.AcceptButton = this.buttonClose;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonClose;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labelJobNameTitle);
            this.Controls.Add(this.tabControl1);
            this.Name = "JobInformation";
            this.Load += new System.EventHandler(this.JobInformation_Load);
            this.Shown += new System.EventHandler(this.JobInformation_Shown);
            this.DpiChanged += new System.Windows.Forms.DpiChangedEventHandler(this.JobInformation_DpiChanged);
            ((System.ComponentModel.ISupportInitialize)(this.DGErrors)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGJob)).EndInit();
            this.contextMenuStripOutputAsset.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridInput)).EndInit();
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
        private System.Windows.Forms.DataGridView DGJob;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCopyClipboard;
        private System.Windows.Forms.Button buttonCopyStats;
        private System.Windows.Forms.ListBox listBoxOutputs;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label labelJobNameTitle;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView DGOutputs;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripInputAsset;
        private System.Windows.Forms.ToolStripMenuItem assetInformationToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripOutputAsset;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.DataGridView dataGridInput;
        private System.Windows.Forms.ListBox listBoxInput;
    }
}