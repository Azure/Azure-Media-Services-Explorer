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
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonClose.Location = new System.Drawing.Point(677, 13);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(99, 23);
            this.buttonClose.TabIndex = 3;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            // 
            // DGMetadataVideo
            // 
            this.DGMetadataVideo.AllowUserToAddRows = false;
            this.DGMetadataVideo.AllowUserToDeleteRows = false;
            this.DGMetadataVideo.AllowUserToResizeRows = false;
            this.DGMetadataVideo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DGMetadataVideo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DGMetadataVideo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGMetadataVideo.ColumnHeadersVisible = false;
            this.DGMetadataVideo.ContextMenuStrip = this.contextMenuStripGrid;
            this.DGMetadataVideo.Location = new System.Drawing.Point(10, 43);
            this.DGMetadataVideo.MultiSelect = false;
            this.DGMetadataVideo.Name = "DGMetadataVideo";
            this.DGMetadataVideo.ReadOnly = true;
            this.DGMetadataVideo.RowHeadersVisible = false;
            this.DGMetadataVideo.Size = new System.Drawing.Size(354, 204);
            this.DGMetadataVideo.TabIndex = 1;
            // 
            // contextMenuStripGrid
            // 
            this.contextMenuStripGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemFilesCopyClipboard});
            this.contextMenuStripGrid.Name = "contextMenuStripDG";
            this.contextMenuStripGrid.Size = new System.Drawing.Size(170, 26);
            this.contextMenuStripGrid.MouseClick += new System.Windows.Forms.MouseEventHandler(this.contextMenuStripGrid_MouseClick);
            // 
            // toolStripMenuItemFilesCopyClipboard
            // 
            this.toolStripMenuItemFilesCopyClipboard.Name = "toolStripMenuItemFilesCopyClipboard";
            this.toolStripMenuItemFilesCopyClipboard.Size = new System.Drawing.Size(169, 22);
            this.toolStripMenuItemFilesCopyClipboard.Text = "Copy to clipboard";
            // 
            // DGMetadataAudio
            // 
            this.DGMetadataAudio.AllowUserToAddRows = false;
            this.DGMetadataAudio.AllowUserToDeleteRows = false;
            this.DGMetadataAudio.AllowUserToResizeRows = false;
            this.DGMetadataAudio.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DGMetadataAudio.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DGMetadataAudio.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGMetadataAudio.ColumnHeadersVisible = false;
            this.DGMetadataAudio.ContextMenuStrip = this.contextMenuStripGrid;
            this.DGMetadataAudio.Location = new System.Drawing.Point(6, 43);
            this.DGMetadataAudio.MultiSelect = false;
            this.DGMetadataAudio.Name = "DGMetadataAudio";
            this.DGMetadataAudio.ReadOnly = true;
            this.DGMetadataAudio.RowHeadersVisible = false;
            this.DGMetadataAudio.Size = new System.Drawing.Size(357, 204);
            this.DGMetadataAudio.TabIndex = 1;
            // 
            // labelAssetNameTitle
            // 
            this.labelAssetNameTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelAssetNameTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAssetNameTitle.Location = new System.Drawing.Point(18, 9);
            this.labelAssetNameTitle.Name = "labelAssetNameTitle";
            this.labelAssetNameTitle.Size = new System.Drawing.Size(753, 32);
            this.labelAssetNameTitle.TabIndex = 35;
            this.labelAssetNameTitle.Text = "Metadata for : ";
            this.labelAssetNameTitle.Click += new System.EventHandler(this.labelAssetNameTitle_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.DGMetadataGal);
            this.groupBox1.Location = new System.Drawing.Point(12, 44);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(751, 180);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "General";
            // 
            // DGMetadataGal
            // 
            this.DGMetadataGal.AllowUserToAddRows = false;
            this.DGMetadataGal.AllowUserToDeleteRows = false;
            this.DGMetadataGal.AllowUserToResizeRows = false;
            this.DGMetadataGal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DGMetadataGal.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DGMetadataGal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGMetadataGal.ColumnHeadersVisible = false;
            this.DGMetadataGal.ContextMenuStrip = this.contextMenuStripGrid;
            this.DGMetadataGal.Location = new System.Drawing.Point(10, 19);
            this.DGMetadataGal.MultiSelect = false;
            this.DGMetadataGal.Name = "DGMetadataGal";
            this.DGMetadataGal.ReadOnly = true;
            this.DGMetadataGal.RowHeadersVisible = false;
            this.DGMetadataGal.Size = new System.Drawing.Size(734, 155);
            this.DGMetadataGal.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.DGMetadataVideo);
            this.groupBox2.Controls.Add(this.numericUpDownVideoTrack);
            this.groupBox2.Location = new System.Drawing.Point(12, 244);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(370, 253);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Video";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 38;
            this.label1.Text = "Track :";
            // 
            // numericUpDownVideoTrack
            // 
            this.numericUpDownVideoTrack.Location = new System.Drawing.Point(54, 17);
            this.numericUpDownVideoTrack.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numericUpDownVideoTrack.Name = "numericUpDownVideoTrack";
            this.numericUpDownVideoTrack.ReadOnly = true;
            this.numericUpDownVideoTrack.Size = new System.Drawing.Size(50, 20);
            this.numericUpDownVideoTrack.TabIndex = 37;
            this.numericUpDownVideoTrack.ValueChanged += new System.EventHandler(this.numericUpDownVideoTrack_ValueChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.DGMetadataAudio);
            this.groupBox3.Controls.Add(this.numericUpDownAudioTrack);
            this.groupBox3.Location = new System.Drawing.Point(393, 244);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(370, 253);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Audio";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 40;
            this.label2.Text = "Track :";
            // 
            // numericUpDownAudioTrack
            // 
            this.numericUpDownAudioTrack.Location = new System.Drawing.Point(53, 15);
            this.numericUpDownAudioTrack.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numericUpDownAudioTrack.Name = "numericUpDownAudioTrack";
            this.numericUpDownAudioTrack.ReadOnly = true;
            this.numericUpDownAudioTrack.Size = new System.Drawing.Size(50, 20);
            this.numericUpDownAudioTrack.TabIndex = 39;
            this.numericUpDownAudioTrack.ValueChanged += new System.EventHandler(this.numericUpDownAudioTrack_ValueChanged);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonClose);
            this.panel1.Location = new System.Drawing.Point(-4, 513);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(791, 48);
            this.panel1.TabIndex = 63;
            // 
            // MetadataInformation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.labelAssetNameTitle);
            this.Name = "MetadataInformation";
            this.Text = "Metadata";
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