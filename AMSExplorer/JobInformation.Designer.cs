﻿namespace AMSExplorer
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
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.labelJobNameTitle = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.DGTasks = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.DGErrors)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGJob)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGTasks)).BeginInit();
            this.SuspendLayout();
            // 
            // DGErrors
            // 
            this.DGErrors.AllowUserToAddRows = false;
            this.DGErrors.AllowUserToResizeRows = false;
            this.DGErrors.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DGErrors.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.DGErrors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGErrors.ContextMenuStrip = this.contextMenuStrip;
            this.DGErrors.Location = new System.Drawing.Point(10, 240);
            this.DGErrors.MultiSelect = false;
            this.DGErrors.Name = "DGErrors";
            this.DGErrors.ReadOnly = true;
            this.DGErrors.RowHeadersVisible = false;
            this.DGErrors.Size = new System.Drawing.Size(729, 143);
            this.DGErrors.TabIndex = 14;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemCopyClipboard});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(170, 26);
            this.contextMenuStrip.MouseClick += new System.Windows.Forms.MouseEventHandler(this.contextMenuStrip_MouseClick);
            // 
            // toolStripMenuItemCopyClipboard
            // 
            this.toolStripMenuItemCopyClipboard.Name = "toolStripMenuItemCopyClipboard";
            this.toolStripMenuItemCopyClipboard.Size = new System.Drawing.Size(169, 22);
            this.toolStripMenuItemCopyClipboard.Text = "Copy to clipboard";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 221);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 15);
            this.label3.TabIndex = 13;
            this.label3.Text = "Errors";
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonClose.Location = new System.Drawing.Point(656, 15);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(115, 27);
            this.buttonClose.TabIndex = 11;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            // 
            // DGJob
            // 
            this.DGJob.AllowUserToAddRows = false;
            this.DGJob.AllowUserToDeleteRows = false;
            this.DGJob.AllowUserToResizeRows = false;
            this.DGJob.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DGJob.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DGJob.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGJob.ColumnHeadersVisible = false;
            this.DGJob.ContextMenuStrip = this.contextMenuStrip;
            this.DGJob.Location = new System.Drawing.Point(10, 7);
            this.DGJob.MultiSelect = false;
            this.DGJob.Name = "DGJob";
            this.DGJob.ReadOnly = true;
            this.DGJob.RowHeadersVisible = false;
            this.DGJob.Size = new System.Drawing.Size(729, 211);
            this.DGJob.TabIndex = 8;
            // 
            // buttonCopyStats
            // 
            this.buttonCopyStats.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCopyStats.Location = new System.Drawing.Point(80, 390);
            this.buttonCopyStats.Name = "buttonCopyStats";
            this.buttonCopyStats.Size = new System.Drawing.Size(121, 27);
            this.buttonCopyStats.TabIndex = 15;
            this.buttonCopyStats.Text = "Copy to clipboard";
            this.buttonCopyStats.UseVisualStyleBackColor = true;
            this.buttonCopyStats.Click += new System.EventHandler(this.buttonCopyStats_Click);
            // 
            // buttonCreateMail
            // 
            this.buttonCreateMail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCreateMail.Location = new System.Drawing.Point(209, 390);
            this.buttonCreateMail.Name = "buttonCreateMail";
            this.buttonCreateMail.Size = new System.Drawing.Size(161, 27);
            this.buttonCreateMail.TabIndex = 16;
            this.buttonCreateMail.Text = "Create new Outlook email";
            this.buttonCreateMail.UseVisualStyleBackColor = true;
            this.buttonCreateMail.Click += new System.EventHandler(this.buttonCreateMail_Click);
            // 
            // listBoxTasks
            // 
            this.listBoxTasks.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxTasks.FormattingEnabled = true;
            this.listBoxTasks.HorizontalScrollbar = true;
            this.listBoxTasks.ItemHeight = 15;
            this.listBoxTasks.Location = new System.Drawing.Point(7, 7);
            this.listBoxTasks.Name = "listBoxTasks";
            this.listBoxTasks.Size = new System.Drawing.Size(733, 94);
            this.listBoxTasks.TabIndex = 22;
            this.listBoxTasks.SelectedIndexChanged += new System.EventHandler(this.listBoxTasks_SelectedIndexChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(14, 46);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(756, 455);
            this.tabControl1.TabIndex = 35;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.buttonCopyStats);
            this.tabPage1.Controls.Add(this.buttonCreateMail);
            this.tabPage1.Controls.Add(this.DGErrors);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.DGJob);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(748, 427);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Job information";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 395);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 15);
            this.label5.TabIndex = 26;
            this.label5.Text = "Job report:";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.DGTasks);
            this.tabPage2.Controls.Add(this.listBoxTasks);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(748, 427);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Tasks";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // labelJobNameTitle
            // 
            this.labelJobNameTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelJobNameTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelJobNameTitle.Location = new System.Drawing.Point(20, 10);
            this.labelJobNameTitle.Name = "labelJobNameTitle";
            this.labelJobNameTitle.Size = new System.Drawing.Size(738, 23);
            this.labelJobNameTitle.TabIndex = 36;
            this.labelJobNameTitle.Text = "Job : ";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonClose);
            this.panel1.Location = new System.Drawing.Point(-2, 506);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(786, 55);
            this.panel1.TabIndex = 63;
            // 
            // DGTasks
            // 
            this.DGTasks.AllowUserToAddRows = false;
            this.DGTasks.AllowUserToDeleteRows = false;
            this.DGTasks.AllowUserToResizeRows = false;
            this.DGTasks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DGTasks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DGTasks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGTasks.ColumnHeadersVisible = false;
            this.DGTasks.ContextMenuStrip = this.contextMenuStrip;
            this.DGTasks.Location = new System.Drawing.Point(7, 130);
            this.DGTasks.MultiSelect = false;
            this.DGTasks.Name = "DGTasks";
            this.DGTasks.ReadOnly = true;
            this.DGTasks.RowHeadersVisible = false;
            this.DGTasks.Size = new System.Drawing.Size(733, 291);
            this.DGTasks.TabIndex = 10;
            this.DGTasks.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGTasks_CellContentClick);
            // 
            // JobInformation
            // 
            this.AcceptButton = this.buttonClose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonClose;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labelJobNameTitle);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "JobInformation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Job Information";
            this.Load += new System.EventHandler(this.JobInformation_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGErrors)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGJob)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGTasks)).EndInit();
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
    }
}