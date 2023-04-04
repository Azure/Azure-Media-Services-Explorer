namespace AMSExplorer
{
    partial class LiveOutputInformation
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LiveOutputInformation));
            contextMenuStripDG = new System.Windows.Forms.ContextMenuStrip(components);
            toolStripMenuItemFilesCopyClipboard = new System.Windows.Forms.ToolStripMenuItem();
            tabPageInfo = new System.Windows.Forms.TabPage();
            label2 = new System.Windows.Forms.Label();
            numericUpDownArchiveMinutes = new System.Windows.Forms.NumericUpDown();
            label4 = new System.Windows.Forms.Label();
            numericUpDownArchiveHours = new System.Windows.Forms.NumericUpDown();
            pictureBox1 = new System.Windows.Forms.PictureBox();
            label5 = new System.Windows.Forms.Label();
            DGLiveEvent = new System.Windows.Forms.DataGridView();
            buttonDisplayRelatedAsset = new System.Windows.Forms.Button();
            tabControl1 = new System.Windows.Forms.TabControl();
            labelProgramName = new System.Windows.Forms.Label();
            buttonClose = new System.Windows.Forms.Button();
            panel1 = new System.Windows.Forms.Panel();
            contextMenuStripDG.SuspendLayout();
            tabPageInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownArchiveMinutes).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownArchiveHours).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)DGLiveEvent).BeginInit();
            tabControl1.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // contextMenuStripDG
            // 
            contextMenuStripDG.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripMenuItemFilesCopyClipboard });
            contextMenuStripDG.Name = "contextMenuStripDG";
            resources.ApplyResources(contextMenuStripDG, "contextMenuStripDG");
            contextMenuStripDG.Opening += contextMenuStripDG_Opening;
            contextMenuStripDG.MouseClick += contextMenuStripDG_MouseClick;
            // 
            // toolStripMenuItemFilesCopyClipboard
            // 
            toolStripMenuItemFilesCopyClipboard.Name = "toolStripMenuItemFilesCopyClipboard";
            resources.ApplyResources(toolStripMenuItemFilesCopyClipboard, "toolStripMenuItemFilesCopyClipboard");
            // 
            // tabPageInfo
            // 
            tabPageInfo.Controls.Add(label2);
            tabPageInfo.Controls.Add(numericUpDownArchiveMinutes);
            tabPageInfo.Controls.Add(label4);
            tabPageInfo.Controls.Add(numericUpDownArchiveHours);
            tabPageInfo.Controls.Add(pictureBox1);
            tabPageInfo.Controls.Add(label5);
            tabPageInfo.Controls.Add(DGLiveEvent);
            resources.ApplyResources(tabPageInfo, "tabPageInfo");
            tabPageInfo.Name = "tabPageInfo";
            tabPageInfo.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            // 
            // numericUpDownArchiveMinutes
            // 
            resources.ApplyResources(numericUpDownArchiveMinutes, "numericUpDownArchiveMinutes");
            numericUpDownArchiveMinutes.Maximum = new decimal(new int[] { 59, 0, 0, 0 });
            numericUpDownArchiveMinutes.Name = "numericUpDownArchiveMinutes";
            // 
            // label4
            // 
            resources.ApplyResources(label4, "label4");
            label4.Name = "label4";
            // 
            // numericUpDownArchiveHours
            // 
            resources.ApplyResources(numericUpDownArchiveHours, "numericUpDownArchiveHours");
            numericUpDownArchiveHours.Maximum = new decimal(new int[] { 25, 0, 0, 0 });
            numericUpDownArchiveHours.Name = "numericUpDownArchiveHours";
            // 
            // pictureBox1
            // 
            resources.ApplyResources(pictureBox1, "pictureBox1");
            pictureBox1.Name = "pictureBox1";
            pictureBox1.TabStop = false;
            // 
            // label5
            // 
            resources.ApplyResources(label5, "label5");
            label5.Name = "label5";
            // 
            // DGLiveEvent
            // 
            DGLiveEvent.AllowUserToAddRows = false;
            DGLiveEvent.AllowUserToDeleteRows = false;
            DGLiveEvent.AllowUserToResizeRows = false;
            resources.ApplyResources(DGLiveEvent, "DGLiveEvent");
            DGLiveEvent.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            DGLiveEvent.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            DGLiveEvent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DGLiveEvent.ColumnHeadersVisible = false;
            DGLiveEvent.ContextMenuStrip = contextMenuStripDG;
            DGLiveEvent.MultiSelect = false;
            DGLiveEvent.Name = "DGLiveEvent";
            DGLiveEvent.ReadOnly = true;
            DGLiveEvent.RowHeadersVisible = false;
            // 
            // buttonDisplayRelatedAsset
            // 
            resources.ApplyResources(buttonDisplayRelatedAsset, "buttonDisplayRelatedAsset");
            buttonDisplayRelatedAsset.Name = "buttonDisplayRelatedAsset";
            buttonDisplayRelatedAsset.UseVisualStyleBackColor = true;
            buttonDisplayRelatedAsset.Click += buttonOpenAsset_Click;
            // 
            // tabControl1
            // 
            resources.ApplyResources(tabControl1, "tabControl1");
            tabControl1.Controls.Add(tabPageInfo);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            // 
            // labelProgramName
            // 
            resources.ApplyResources(labelProgramName, "labelProgramName");
            labelProgramName.Name = "labelProgramName";
            labelProgramName.Click += labelProgramName_Click;
            // 
            // buttonClose
            // 
            resources.ApplyResources(buttonClose, "buttonClose");
            buttonClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            buttonClose.Name = "buttonClose";
            buttonClose.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.BackColor = System.Drawing.SystemColors.Control;
            panel1.Controls.Add(buttonClose);
            resources.ApplyResources(panel1, "panel1");
            panel1.Name = "panel1";
            // 
            // LiveOutputInformation
            // 
            AcceptButton = buttonClose;
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            BackColor = System.Drawing.SystemColors.Window;
            CancelButton = buttonClose;
            Controls.Add(buttonDisplayRelatedAsset);
            Controls.Add(panel1);
            Controls.Add(labelProgramName);
            Controls.Add(tabControl1);
            Name = "LiveOutputInformation";
            SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            Load += LiveOutputInformation_Load;
            Shown += ProgramInformation_Shown;
            DpiChanged += LiveOutputInformation_DpiChanged;
            contextMenuStripDG.ResumeLayout(false);
            tabPageInfo.ResumeLayout(false);
            tabPageInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownArchiveMinutes).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownArchiveHours).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)DGLiveEvent).EndInit();
            tabControl1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStripDG;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFilesCopyClipboard;
        private System.Windows.Forms.TabPage tabPageInfo;
        private System.Windows.Forms.DataGridView DGLiveEvent;
        private System.Windows.Forms.Button buttonDisplayRelatedAsset;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Label labelProgramName;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDownArchiveMinutes;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDownArchiveHours;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label5;
    }
}