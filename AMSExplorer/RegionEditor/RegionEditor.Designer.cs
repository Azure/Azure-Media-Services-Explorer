namespace AMSExplorer
{
    partial class RegionEditor
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
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelWarningJSON = new System.Windows.Forms.Label();
            this.buttonCopyClipboard = new System.Windows.Forms.Button();
            this.buttonClearLastRegion = new System.Windows.Forms.Button();
            this.labelInfoText = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelMouseInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelXYRect = new System.Windows.Forms.ToolStripStatusLabel();
            this.buttonClearAllRegions = new System.Windows.Forms.Button();
            this.myPictureBox1 = new AMSExplorer.myPictureBox();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.radioButtonRectangle = new System.Windows.Forms.RadioButton();
            this.radioButtonPolygonal = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.myPictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(654, 15);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(115, 27);
            this.buttonCancel.TabIndex = 16;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonOk.Location = new System.Drawing.Point(532, 15);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(115, 27);
            this.buttonOk.TabIndex = 17;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonOk);
            this.panel1.Location = new System.Drawing.Point(0, 484);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(784, 55);
            this.panel1.TabIndex = 66;
            // 
            // labelWarningJSON
            // 
            this.labelWarningJSON.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelWarningJSON.ForeColor = System.Drawing.Color.Red;
            this.labelWarningJSON.Location = new System.Drawing.Point(694, 439);
            this.labelWarningJSON.Name = "labelWarningJSON";
            this.labelWarningJSON.Size = new System.Drawing.Size(78, 21);
            this.labelWarningJSON.TabIndex = 78;
            this.labelWarningJSON.Tag = "";
            this.labelWarningJSON.Text = "XML Syntax error. {0}";
            this.labelWarningJSON.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonCopyClipboard
            // 
            this.buttonCopyClipboard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCopyClipboard.Image = global::AMSExplorer.Bitmaps.copy_to_clipboard;
            this.buttonCopyClipboard.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonCopyClipboard.Location = new System.Drawing.Point(626, 13);
            this.buttonCopyClipboard.Name = "buttonCopyClipboard";
            this.buttonCopyClipboard.Size = new System.Drawing.Size(149, 23);
            this.buttonCopyClipboard.TabIndex = 80;
            this.buttonCopyClipboard.Text = "Copy to clipboard";
            this.buttonCopyClipboard.UseVisualStyleBackColor = true;
            this.buttonCopyClipboard.Click += new System.EventHandler(this.buttonCopyClipboard_Click);
            // 
            // buttonClearLastRegion
            // 
            this.buttonClearLastRegion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClearLastRegion.Location = new System.Drawing.Point(626, 71);
            this.buttonClearLastRegion.Name = "buttonClearLastRegion";
            this.buttonClearLastRegion.Size = new System.Drawing.Size(149, 23);
            this.buttonClearLastRegion.TabIndex = 81;
            this.buttonClearLastRegion.Text = "Clear last region";
            this.buttonClearLastRegion.UseVisualStyleBackColor = true;
            this.buttonClearLastRegion.Click += new System.EventHandler(this.buttonFormat_Click);
            // 
            // labelInfoText
            // 
            this.labelInfoText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelInfoText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.labelInfoText.ForeColor = System.Drawing.Color.Black;
            this.labelInfoText.Location = new System.Drawing.Point(626, 107);
            this.labelInfoText.Name = "labelInfoText";
            this.labelInfoText.Size = new System.Drawing.Size(149, 80);
            this.labelInfoText.TabIndex = 82;
            this.labelInfoText.Tag = "";
            this.labelInfoText.Text = "This is an information text";
            this.labelInfoText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelInfoText.Visible = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelMouseInfo,
            this.toolStripStatusLabelXYRect});
            this.statusStrip1.Location = new System.Drawing.Point(0, 539);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(784, 22);
            this.statusStrip1.TabIndex = 84;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabelMouseInfo
            // 
            this.toolStripStatusLabelMouseInfo.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripStatusLabelMouseInfo.Image = global::AMSExplorer.Bitmaps.XY;
            this.toolStripStatusLabelMouseInfo.Name = "toolStripStatusLabelMouseInfo";
            this.toolStripStatusLabelMouseInfo.Size = new System.Drawing.Size(41, 17);
            this.toolStripStatusLabelMouseInfo.Text = "1, 1";
            // 
            // toolStripStatusLabelXYRect
            // 
            this.toolStripStatusLabelXYRect.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripStatusLabelXYRect.Image = global::AMSExplorer.Bitmaps.XYRect;
            this.toolStripStatusLabelXYRect.Name = "toolStripStatusLabelXYRect";
            this.toolStripStatusLabelXYRect.Size = new System.Drawing.Size(46, 17);
            this.toolStripStatusLabelXYRect.Text = "1 x 1";
            this.toolStripStatusLabelXYRect.Visible = false;
            // 
            // buttonClearAllRegions
            // 
            this.buttonClearAllRegions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClearAllRegions.Location = new System.Drawing.Point(626, 42);
            this.buttonClearAllRegions.Name = "buttonClearAllRegions";
            this.buttonClearAllRegions.Size = new System.Drawing.Size(149, 23);
            this.buttonClearAllRegions.TabIndex = 85;
            this.buttonClearAllRegions.Text = "Clear all regions";
            this.buttonClearAllRegions.UseVisualStyleBackColor = true;
            this.buttonClearAllRegions.Click += new System.EventHandler(this.buttonClearAllRegions_Click);
            // 
            // myPictureBox1
            // 
            this.myPictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.myPictureBox1.BackColor = System.Drawing.SystemColors.Window;
            this.myPictureBox1.Location = new System.Drawing.Point(12, 13);
            this.myPictureBox1.Name = "myPictureBox1";
            this.myPictureBox1.Size = new System.Drawing.Size(608, 465);
            this.myPictureBox1.TabIndex = 83;
            this.myPictureBox1.TabStop = false;
            this.myPictureBox1.SizeChanged += new System.EventHandler(this.myPictureBox1_SizeChanged);
            this.myPictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.myPictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.myPictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Key";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Value";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // radioButtonRectangle
            // 
            this.radioButtonRectangle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonRectangle.AutoSize = true;
            this.radioButtonRectangle.Checked = true;
            this.radioButtonRectangle.Location = new System.Drawing.Point(645, 220);
            this.radioButtonRectangle.Name = "radioButtonRectangle";
            this.radioButtonRectangle.Size = new System.Drawing.Size(77, 19);
            this.radioButtonRectangle.TabIndex = 86;
            this.radioButtonRectangle.TabStop = true;
            this.radioButtonRectangle.Text = "Rectangle";
            this.radioButtonRectangle.UseVisualStyleBackColor = true;
            // 
            // radioButtonPolygonal
            // 
            this.radioButtonPolygonal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonPolygonal.AutoSize = true;
            this.radioButtonPolygonal.Location = new System.Drawing.Point(645, 245);
            this.radioButtonPolygonal.Name = "radioButtonPolygonal";
            this.radioButtonPolygonal.Size = new System.Drawing.Size(78, 19);
            this.radioButtonPolygonal.TabIndex = 87;
            this.radioButtonPolygonal.Text = "Polygonal";
            this.radioButtonPolygonal.UseVisualStyleBackColor = true;
            this.radioButtonPolygonal.CheckedChanged += new System.EventHandler(this.radioButtonPolygonal_CheckedChanged);
            // 
            // RegionEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.radioButtonPolygonal);
            this.Controls.Add(this.radioButtonRectangle);
            this.Controls.Add(this.buttonClearAllRegions);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.myPictureBox1);
            this.Controls.Add(this.labelInfoText);
            this.Controls.Add(this.buttonClearLastRegion);
            this.Controls.Add(this.buttonCopyClipboard);
            this.Controls.Add(this.labelWarningJSON);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "RegionEditor";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Regions";
            this.Load += new System.EventHandler(this.EditorXMLJSON_Load);
            this.panel1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.myPictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button buttonOk;
        public System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelWarningJSON;
        private System.Windows.Forms.Button buttonCopyClipboard;
        private System.Windows.Forms.Button buttonClearLastRegion;
        private System.Windows.Forms.Label labelInfoText;
        private myPictureBox myPictureBox1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelMouseInfo;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelXYRect;
        private System.Windows.Forms.Button buttonClearAllRegions;
        private System.Windows.Forms.RadioButton radioButtonRectangle;
        private System.Windows.Forms.RadioButton radioButtonPolygonal;
    }
}