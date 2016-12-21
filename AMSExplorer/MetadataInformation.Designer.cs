namespace AMSExplorer
{
    partial class MetadataInformation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MetadataInformation));
            this.buttonClose = new System.Windows.Forms.Button();
            this.DGMetadataVideo = new System.Windows.Forms.DataGridView();
            this.contextMenuStripGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemFilesCopyClipboard = new System.Windows.Forms.ToolStripMenuItem();
            this.DGMetadataAudio = new System.Windows.Forms.DataGridView();
            this.labelAssetNameTitle = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.DGMetadataGal = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownVideoTrack = new System.Windows.Forms.NumericUpDown();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownAudioTrack = new System.Windows.Forms.NumericUpDown();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.DGMetadataVideo)).BeginInit();
            this.contextMenuStripGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGMetadataAudio)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGMetadataGal)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVideoTrack)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAudioTrack)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            resources.ApplyResources(this.buttonClose, "buttonClose");
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.UseVisualStyleBackColor = true;
            // 
            // DGMetadataVideo
            // 
            this.DGMetadataVideo.AllowUserToAddRows = false;
            this.DGMetadataVideo.AllowUserToDeleteRows = false;
            this.DGMetadataVideo.AllowUserToResizeRows = false;
            resources.ApplyResources(this.DGMetadataVideo, "DGMetadataVideo");
            this.DGMetadataVideo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DGMetadataVideo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGMetadataVideo.ColumnHeadersVisible = false;
            this.DGMetadataVideo.ContextMenuStrip = this.contextMenuStripGrid;
            this.DGMetadataVideo.MultiSelect = false;
            this.DGMetadataVideo.Name = "DGMetadataVideo";
            this.DGMetadataVideo.ReadOnly = true;
            this.DGMetadataVideo.RowHeadersVisible = false;
            // 
            // contextMenuStripGrid
            // 
            this.contextMenuStripGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemFilesCopyClipboard});
            this.contextMenuStripGrid.Name = "contextMenuStripDG";
            resources.ApplyResources(this.contextMenuStripGrid, "contextMenuStripGrid");
            this.contextMenuStripGrid.MouseClick += new System.Windows.Forms.MouseEventHandler(this.contextMenuStripGrid_MouseClick);
            // 
            // toolStripMenuItemFilesCopyClipboard
            // 
            this.toolStripMenuItemFilesCopyClipboard.Name = "toolStripMenuItemFilesCopyClipboard";
            resources.ApplyResources(this.toolStripMenuItemFilesCopyClipboard, "toolStripMenuItemFilesCopyClipboard");
            // 
            // DGMetadataAudio
            // 
            this.DGMetadataAudio.AllowUserToAddRows = false;
            this.DGMetadataAudio.AllowUserToDeleteRows = false;
            this.DGMetadataAudio.AllowUserToResizeRows = false;
            resources.ApplyResources(this.DGMetadataAudio, "DGMetadataAudio");
            this.DGMetadataAudio.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DGMetadataAudio.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGMetadataAudio.ColumnHeadersVisible = false;
            this.DGMetadataAudio.ContextMenuStrip = this.contextMenuStripGrid;
            this.DGMetadataAudio.MultiSelect = false;
            this.DGMetadataAudio.Name = "DGMetadataAudio";
            this.DGMetadataAudio.ReadOnly = true;
            this.DGMetadataAudio.RowHeadersVisible = false;
            // 
            // labelAssetNameTitle
            // 
            resources.ApplyResources(this.labelAssetNameTitle, "labelAssetNameTitle");
            this.labelAssetNameTitle.Name = "labelAssetNameTitle";
            this.labelAssetNameTitle.Click += new System.EventHandler(this.labelAssetNameTitle_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.DGMetadataGal);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // DGMetadataGal
            // 
            this.DGMetadataGal.AllowUserToAddRows = false;
            this.DGMetadataGal.AllowUserToDeleteRows = false;
            this.DGMetadataGal.AllowUserToResizeRows = false;
            resources.ApplyResources(this.DGMetadataGal, "DGMetadataGal");
            this.DGMetadataGal.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DGMetadataGal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGMetadataGal.ColumnHeadersVisible = false;
            this.DGMetadataGal.ContextMenuStrip = this.contextMenuStripGrid;
            this.DGMetadataGal.MultiSelect = false;
            this.DGMetadataGal.Name = "DGMetadataGal";
            this.DGMetadataGal.ReadOnly = true;
            this.DGMetadataGal.RowHeadersVisible = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.DGMetadataVideo);
            this.groupBox2.Controls.Add(this.numericUpDownVideoTrack);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // numericUpDownVideoTrack
            // 
            resources.ApplyResources(this.numericUpDownVideoTrack, "numericUpDownVideoTrack");
            this.numericUpDownVideoTrack.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numericUpDownVideoTrack.Name = "numericUpDownVideoTrack";
            this.numericUpDownVideoTrack.ReadOnly = true;
            this.numericUpDownVideoTrack.ValueChanged += new System.EventHandler(this.numericUpDownVideoTrack_ValueChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.DGMetadataAudio);
            this.groupBox3.Controls.Add(this.numericUpDownAudioTrack);
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // numericUpDownAudioTrack
            // 
            resources.ApplyResources(this.numericUpDownAudioTrack, "numericUpDownAudioTrack");
            this.numericUpDownAudioTrack.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numericUpDownAudioTrack.Name = "numericUpDownAudioTrack";
            this.numericUpDownAudioTrack.ReadOnly = true;
            this.numericUpDownAudioTrack.ValueChanged += new System.EventHandler(this.numericUpDownAudioTrack_ValueChanged);
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonClose);
            this.panel1.Name = "panel1";
            // 
            // MetadataInformation
            // 
            this.AcceptButton = this.buttonClose;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonClose;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.labelAssetNameTitle);
            this.Name = "MetadataInformation";
            this.Load += new System.EventHandler(this.MetadataInformation_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGMetadataVideo)).EndInit();
            this.contextMenuStripGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGMetadataAudio)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGMetadataGal)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVideoTrack)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAudioTrack)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Label labelAssetNameTitle;
        private System.Windows.Forms.DataGridView DGMetadataVideo;
        private System.Windows.Forms.DataGridView DGMetadataAudio;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView DGMetadataGal;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripGrid;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFilesCopyClipboard;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDownVideoTrack;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDownAudioTrack;
        private System.Windows.Forms.Panel panel1;
    }
}