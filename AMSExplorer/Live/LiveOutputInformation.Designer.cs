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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LiveOutputInformation));
            this.contextMenuStripDG = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemFilesCopyClipboard = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPageInfo = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownArchiveMinutes = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDownArchiveHours = new System.Windows.Forms.NumericUpDown();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.DGLiveEvent = new System.Windows.Forms.DataGridView();
            this.buttonDisplayRelatedAsset = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.labelProgramName = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.contextMenuStripDG.SuspendLayout();
            this.tabPageInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownArchiveMinutes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownArchiveHours)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGLiveEvent)).BeginInit();
            this.tabControl1.SuspendLayout();
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
            // tabPageInfo
            // 
            resources.ApplyResources(this.tabPageInfo, "tabPageInfo");
            this.tabPageInfo.Controls.Add(this.label2);
            this.tabPageInfo.Controls.Add(this.numericUpDownArchiveMinutes);
            this.tabPageInfo.Controls.Add(this.label4);
            this.tabPageInfo.Controls.Add(this.numericUpDownArchiveHours);
            this.tabPageInfo.Controls.Add(this.pictureBox1);
            this.tabPageInfo.Controls.Add(this.label5);
            this.tabPageInfo.Controls.Add(this.DGLiveEvent);
            this.tabPageInfo.Name = "tabPageInfo";
            this.tabPageInfo.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // numericUpDownArchiveMinutes
            // 
            resources.ApplyResources(this.numericUpDownArchiveMinutes, "numericUpDownArchiveMinutes");
            this.numericUpDownArchiveMinutes.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.numericUpDownArchiveMinutes.Name = "numericUpDownArchiveMinutes";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // numericUpDownArchiveHours
            // 
            resources.ApplyResources(this.numericUpDownArchiveHours, "numericUpDownArchiveHours");
            this.numericUpDownArchiveHours.Maximum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.numericUpDownArchiveHours.Name = "numericUpDownArchiveHours";
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Image = global::AMSExplorer.Bitmaps.archive;
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // DGLiveEvent
            // 
            resources.ApplyResources(this.DGLiveEvent, "DGLiveEvent");
            this.DGLiveEvent.AllowUserToAddRows = false;
            this.DGLiveEvent.AllowUserToDeleteRows = false;
            this.DGLiveEvent.AllowUserToResizeRows = false;
            this.DGLiveEvent.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DGLiveEvent.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.DGLiveEvent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGLiveEvent.ColumnHeadersVisible = false;
            this.DGLiveEvent.ContextMenuStrip = this.contextMenuStripDG;
            this.DGLiveEvent.MultiSelect = false;
            this.DGLiveEvent.Name = "DGLiveEvent";
            this.DGLiveEvent.ReadOnly = true;
            this.DGLiveEvent.RowHeadersVisible = false;
            // 
            // buttonDisplayRelatedAsset
            // 
            resources.ApplyResources(this.buttonDisplayRelatedAsset, "buttonDisplayRelatedAsset");
            this.buttonDisplayRelatedAsset.Image = global::AMSExplorer.Bitmaps.Display_information;
            this.buttonDisplayRelatedAsset.Name = "buttonDisplayRelatedAsset";
            this.buttonDisplayRelatedAsset.UseVisualStyleBackColor = true;
            this.buttonDisplayRelatedAsset.Click += new System.EventHandler(this.buttonOpenAsset_Click);
            // 
            // tabControl1
            // 
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Controls.Add(this.tabPageInfo);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // labelProgramName
            // 
            resources.ApplyResources(this.labelProgramName, "labelProgramName");
            this.labelProgramName.Name = "labelProgramName";
            this.labelProgramName.Click += new System.EventHandler(this.labelProgramName_Click);
            // 
            // buttonClose
            // 
            resources.ApplyResources(this.buttonClose, "buttonClose");
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonClose);
            this.panel1.Name = "panel1";
            // 
            // LiveOutputInformation
            // 
            this.AcceptButton = this.buttonClose;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonClose;
            this.Controls.Add(this.buttonDisplayRelatedAsset);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labelProgramName);
            this.Controls.Add(this.tabControl1);
            this.Name = "LiveOutputInformation";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.LiveOutputInformation_Load);
            this.Shown += new System.EventHandler(this.ProgramInformation_Shown);
            this.DpiChanged += new System.Windows.Forms.DpiChangedEventHandler(this.LiveOutputInformation_DpiChanged);
            this.contextMenuStripDG.ResumeLayout(false);
            this.tabPageInfo.ResumeLayout(false);
            this.tabPageInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownArchiveMinutes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownArchiveHours)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGLiveEvent)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

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