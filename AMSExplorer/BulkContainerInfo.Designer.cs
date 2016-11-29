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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BulkContainerInfo));
            this.contextMenuStripDG = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemFilesCopyClipboard = new System.Windows.Forms.ToolStripMenuItem();
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
            resources.ApplyResources(this.contextMenuStripDG, "contextMenuStripDG");
            this.contextMenuStripDG.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemFilesCopyClipboard});
            this.contextMenuStripDG.Name = "contextMenuStripDG";
            this.contextMenuStripDG.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripDG_Opening);
            this.contextMenuStripDG.MouseClick += new System.Windows.Forms.MouseEventHandler(this.contextMenuStripDG_MouseClick);
            // 
            // toolStripMenuItemFilesCopyClipboard
            // 
            resources.ApplyResources(this.toolStripMenuItemFilesCopyClipboard, "toolStripMenuItemFilesCopyClipboard");
            this.toolStripMenuItemFilesCopyClipboard.Name = "toolStripMenuItemFilesCopyClipboard";
            // 
            // tabPage1
            // 
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Controls.Add(this.DGBulkManifest);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // DGBulkManifest
            // 
            resources.ApplyResources(this.DGBulkManifest, "DGBulkManifest");
            this.DGBulkManifest.AllowUserToAddRows = false;
            this.DGBulkManifest.AllowUserToDeleteRows = false;
            this.DGBulkManifest.AllowUserToResizeRows = false;
            this.DGBulkManifest.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DGBulkManifest.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGBulkManifest.ColumnHeadersVisible = false;
            this.DGBulkManifest.ContextMenuStrip = this.contextMenuStripDG;
            this.DGBulkManifest.MultiSelect = false;
            this.DGBulkManifest.Name = "DGBulkManifest";
            this.DGBulkManifest.ReadOnly = true;
            this.DGBulkManifest.RowHeadersVisible = false;
            // 
            // tabControl1
            // 
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabPage2
            // 
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.textBoxName);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // textBoxName
            // 
            resources.ApplyResources(this.textBoxName, "textBoxName");
            this.textBoxName.Name = "textBoxName";
            // 
            // tabPage3
            // 
            resources.ApplyResources(this.tabPage3, "tabPage3");
            this.tabPage3.Controls.Add(this.DGAssetManifest);
            this.tabPage3.Controls.Add(this.listViewAssetManifests);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // DGAssetManifest
            // 
            resources.ApplyResources(this.DGAssetManifest, "DGAssetManifest");
            this.DGAssetManifest.AllowUserToAddRows = false;
            this.DGAssetManifest.AllowUserToDeleteRows = false;
            this.DGAssetManifest.AllowUserToResizeRows = false;
            this.DGAssetManifest.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DGAssetManifest.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGAssetManifest.ColumnHeadersVisible = false;
            this.DGAssetManifest.ContextMenuStrip = this.contextMenuStripDG;
            this.DGAssetManifest.MultiSelect = false;
            this.DGAssetManifest.Name = "DGAssetManifest";
            this.DGAssetManifest.ReadOnly = true;
            this.DGAssetManifest.RowHeadersVisible = false;
            // 
            // listViewAssetManifests
            // 
            resources.ApplyResources(this.listViewAssetManifests, "listViewAssetManifests");
            this.listViewAssetManifests.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listViewAssetManifests.FullRowSelect = true;
            this.listViewAssetManifests.HideSelection = false;
            this.listViewAssetManifests.MultiSelect = false;
            this.listViewAssetManifests.Name = "listViewAssetManifests";
            this.listViewAssetManifests.UseCompatibleStateImageBehavior = false;
            this.listViewAssetManifests.View = System.Windows.Forms.View.Details;
            this.listViewAssetManifests.SelectedIndexChanged += new System.EventHandler(this.listViewAssetManifest_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            resources.ApplyResources(this.columnHeader1, "columnHeader1");
            // 
            // labelPBulkName
            // 
            resources.ApplyResources(this.labelPBulkName, "labelPBulkName");
            this.labelPBulkName.Name = "labelPBulkName";
            this.labelPBulkName.Click += new System.EventHandler(this.labelProgramName_Click);
            // 
            // buttonClose
            // 
            resources.ApplyResources(this.buttonClose, "buttonClose");
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.UseVisualStyleBackColor = true;
            // 
            // buttonUpdateClose
            // 
            resources.ApplyResources(this.buttonUpdateClose, "buttonUpdateClose");
            this.buttonUpdateClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonUpdateClose.Name = "buttonUpdateClose";
            this.buttonUpdateClose.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonUpdateClose);
            this.panel1.Controls.Add(this.buttonClose);
            this.panel1.Name = "panel1";
            // 
            // BulkContainerInfo
            // 
            this.AcceptButton = this.buttonClose;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonClose;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labelPBulkName);
            this.Controls.Add(this.tabControl1);
            this.Name = "BulkContainerInfo";
            this.Load += new System.EventHandler(this.BulkContainerInfo_Load);
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