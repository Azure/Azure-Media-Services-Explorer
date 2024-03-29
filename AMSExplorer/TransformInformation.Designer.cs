﻿namespace AMSExplorer
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
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemCopyClipboard = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonClose = new System.Windows.Forms.Button();
            this.DGTransform = new System.Windows.Forms.DataGridView();
            this.listBoxOutputs = new System.Windows.Forms.ListBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonCopyStats = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.DGOutputs = new System.Windows.Forms.DataGridView();
            this.textBoxPresetJson = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.contextMenuStripOutputAsset = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripInputAsset = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.assetInformationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.labelJobNameTitle = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.contextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGTransform)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGOutputs)).BeginInit();
            this.contextMenuStripOutputAsset.SuspendLayout();
            this.contextMenuStripInputAsset.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip
            // 
            resources.ApplyResources(this.contextMenuStrip, "contextMenuStrip");
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemCopyClipboard});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ContextMenuStrip_MouseClick);
            // 
            // toolStripMenuItemCopyClipboard
            // 
            resources.ApplyResources(this.toolStripMenuItemCopyClipboard, "toolStripMenuItemCopyClipboard");
            this.toolStripMenuItemCopyClipboard.Name = "toolStripMenuItemCopyClipboard";
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
            resources.ApplyResources(this.DGTransform, "DGTransform");
            this.DGTransform.AllowUserToAddRows = false;
            this.DGTransform.AllowUserToDeleteRows = false;
            this.DGTransform.AllowUserToResizeRows = false;
            this.DGTransform.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DGTransform.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
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
            this.listBoxOutputs.SelectedIndexChanged += new System.EventHandler(this.ListBoxOutputs_SelectedIndexChanged);
            // 
            // tabControl1
            // 
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabPage1
            // 
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.buttonCopyStats);
            this.tabPage1.Controls.Add(this.DGTransform);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // buttonCopyStats
            // 
            resources.ApplyResources(this.buttonCopyStats, "buttonCopyStats");
            this.buttonCopyStats.Name = "buttonCopyStats";
            this.buttonCopyStats.UseVisualStyleBackColor = true;
            this.buttonCopyStats.Click += new System.EventHandler(this.buttonCopyStats_Click);
            // 
            // tabPage2
            // 
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Controls.Add(this.tableLayoutPanel1);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.DGOutputs, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.listBoxOutputs, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.textBoxPresetJson, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
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
            // textBoxPresetJson
            // 
            resources.ApplyResources(this.textBoxPresetJson, "textBoxPresetJson");
            this.textBoxPresetJson.Name = "textBoxPresetJson";
            this.textBoxPresetJson.ReadOnly = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
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
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.ToolStripMenuItem1_Click);
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
            this.assetInformationToolStripMenuItem.Name = "assetInformationToolStripMenuItem";
            this.assetInformationToolStripMenuItem.Click += new System.EventHandler(this.AssetInformationToolStripMenuItem_Click);
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
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonClose;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labelJobNameTitle);
            this.Controls.Add(this.tabControl1);
            this.Name = "TransformInformation";
            this.Load += new System.EventHandler(this.TransformInformation_Load);
            this.Shown += new System.EventHandler(this.TransformInformation_Shown);
            this.DpiChanged += new System.Windows.Forms.DpiChangedEventHandler(this.TransformInformation_DpiChanged);
            this.contextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGTransform)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGOutputs)).EndInit();
            this.contextMenuStripOutputAsset.ResumeLayout(false);
            this.contextMenuStripInputAsset.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
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
        private System.Windows.Forms.ContextMenuStrip contextMenuStripInputAsset;
        private System.Windows.Forms.ToolStripMenuItem assetInformationToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripOutputAsset;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox textBoxPresetJson;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonCopyStats;
    }
}