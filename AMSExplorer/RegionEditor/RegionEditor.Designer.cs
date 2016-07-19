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
            this.buttonClearLastRegion = new System.Windows.Forms.Button();
            this.labelInfoText = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelImSize = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelMouseInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelXYRect = new System.Windows.Forms.ToolStripStatusLabel();
            this.buttonClearAllRegions = new System.Windows.Forms.Button();
            this.radioButtonRectangle = new System.Windows.Forms.RadioButton();
            this.radioButtonPolygon = new System.Windows.Forms.RadioButton();
            this.groupBoxShape = new System.Windows.Forms.GroupBox();
            this.myPictureBox1 = new AMSExplorer.myPictureBox();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonPreviousImage = new System.Windows.Forms.Button();
            this.buttonNextImage = new System.Windows.Forms.Button();
            this.labelIndexThumbnail = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownH = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownW = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownY = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownX = new System.Windows.Forms.NumericUpDown();
            this.panel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBoxShape.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.myPictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownW)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownX)).BeginInit();
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
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
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
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
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
            this.labelWarningJSON.Location = new System.Drawing.Point(626, 420);
            this.labelWarningJSON.Name = "labelWarningJSON";
            this.labelWarningJSON.Size = new System.Drawing.Size(78, 21);
            this.labelWarningJSON.TabIndex = 78;
            this.labelWarningJSON.Tag = "";
            this.labelWarningJSON.Text = "XML Syntax error. {0}";
            this.labelWarningJSON.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonClearLastRegion
            // 
            this.buttonClearLastRegion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClearLastRegion.Location = new System.Drawing.Point(627, 42);
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
            this.labelInfoText.Location = new System.Drawing.Point(626, 334);
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
            this.toolStripStatusLabelImSize,
            this.toolStripStatusLabelMouseInfo,
            this.toolStripStatusLabelXYRect});
            this.statusStrip1.Location = new System.Drawing.Point(0, 539);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(784, 22);
            this.statusStrip1.TabIndex = 84;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabelImSize
            // 
            this.toolStripStatusLabelImSize.Name = "toolStripStatusLabelImSize";
            this.toolStripStatusLabelImSize.Size = new System.Drawing.Size(100, 17);
            this.toolStripStatusLabelImSize.Text = "Image size 20 x 20";
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
            this.buttonClearAllRegions.Location = new System.Drawing.Point(627, 13);
            this.buttonClearAllRegions.Name = "buttonClearAllRegions";
            this.buttonClearAllRegions.Size = new System.Drawing.Size(149, 23);
            this.buttonClearAllRegions.TabIndex = 85;
            this.buttonClearAllRegions.Text = "Clear all regions";
            this.buttonClearAllRegions.UseVisualStyleBackColor = true;
            this.buttonClearAllRegions.Click += new System.EventHandler(this.buttonClearAllRegions_Click);
            // 
            // radioButtonRectangle
            // 
            this.radioButtonRectangle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonRectangle.AutoSize = true;
            this.radioButtonRectangle.Checked = true;
            this.radioButtonRectangle.Location = new System.Drawing.Point(19, 22);
            this.radioButtonRectangle.Name = "radioButtonRectangle";
            this.radioButtonRectangle.Size = new System.Drawing.Size(77, 19);
            this.radioButtonRectangle.TabIndex = 86;
            this.radioButtonRectangle.TabStop = true;
            this.radioButtonRectangle.Text = "Rectangle";
            this.radioButtonRectangle.UseVisualStyleBackColor = true;
            // 
            // radioButtonPolygon
            // 
            this.radioButtonPolygon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonPolygon.AutoSize = true;
            this.radioButtonPolygon.Location = new System.Drawing.Point(19, 47);
            this.radioButtonPolygon.Name = "radioButtonPolygon";
            this.radioButtonPolygon.Size = new System.Drawing.Size(69, 19);
            this.radioButtonPolygon.TabIndex = 87;
            this.radioButtonPolygon.Text = "Polygon";
            this.radioButtonPolygon.UseVisualStyleBackColor = true;
            this.radioButtonPolygon.CheckedChanged += new System.EventHandler(this.radioButtonPolygonal_CheckedChanged);
            // 
            // groupBoxShape
            // 
            this.groupBoxShape.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxShape.Controls.Add(this.radioButtonRectangle);
            this.groupBoxShape.Controls.Add(this.radioButtonPolygon);
            this.groupBoxShape.Location = new System.Drawing.Point(629, 83);
            this.groupBoxShape.Name = "groupBoxShape";
            this.groupBoxShape.Size = new System.Drawing.Size(147, 84);
            this.groupBoxShape.TabIndex = 88;
            this.groupBoxShape.TabStop = false;
            this.groupBoxShape.Text = "Shape";
            // 
            // myPictureBox1
            // 
            this.myPictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.myPictureBox1.BackColor = System.Drawing.SystemColors.Window;
            this.myPictureBox1.Cursor = System.Windows.Forms.Cursors.Cross;
            this.myPictureBox1.LastRectangle = new System.Drawing.Rectangle(0, 0, 0, 0);
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
            // buttonPreviousImage
            // 
            this.buttonPreviousImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPreviousImage.Location = new System.Drawing.Point(629, 455);
            this.buttonPreviousImage.Name = "buttonPreviousImage";
            this.buttonPreviousImage.Size = new System.Drawing.Size(26, 23);
            this.buttonPreviousImage.TabIndex = 89;
            this.buttonPreviousImage.Text = "←";
            this.buttonPreviousImage.UseVisualStyleBackColor = true;
            this.buttonPreviousImage.Click += new System.EventHandler(this.buttonPreviousImage_Click);
            // 
            // buttonNextImage
            // 
            this.buttonNextImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonNextImage.Location = new System.Drawing.Point(749, 455);
            this.buttonNextImage.Name = "buttonNextImage";
            this.buttonNextImage.Size = new System.Drawing.Size(26, 23);
            this.buttonNextImage.TabIndex = 90;
            this.buttonNextImage.Text = "→";
            this.buttonNextImage.UseVisualStyleBackColor = true;
            this.buttonNextImage.Click += new System.EventHandler(this.buttonNextImage_Click);
            // 
            // labelIndexThumbnail
            // 
            this.labelIndexThumbnail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelIndexThumbnail.Location = new System.Drawing.Point(661, 459);
            this.labelIndexThumbnail.Name = "labelIndexThumbnail";
            this.labelIndexThumbnail.Size = new System.Drawing.Size(82, 19);
            this.labelIndexThumbnail.TabIndex = 91;
            this.labelIndexThumbnail.Text = "thumbnail 1/2";
            this.labelIndexThumbnail.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.numericUpDownH);
            this.groupBox1.Controls.Add(this.numericUpDownW);
            this.groupBox1.Controls.Add(this.numericUpDownY);
            this.groupBox1.Controls.Add(this.numericUpDownX);
            this.groupBox1.Location = new System.Drawing.Point(629, 173);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(147, 147);
            this.groupBox1.TabIndex = 92;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Rectangle";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 111);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 15);
            this.label4.TabIndex = 7;
            this.label4.Text = "Height :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "Width :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Y :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "X :";
            // 
            // numericUpDownH
            // 
            this.numericUpDownH.Location = new System.Drawing.Point(63, 109);
            this.numericUpDownH.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownH.Name = "numericUpDownH";
            this.numericUpDownH.Size = new System.Drawing.Size(79, 23);
            this.numericUpDownH.TabIndex = 3;
            this.numericUpDownH.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownH.ValueChanged += new System.EventHandler(this.numericUpDownREct_ValueChanged);
            // 
            // numericUpDownW
            // 
            this.numericUpDownW.Location = new System.Drawing.Point(63, 80);
            this.numericUpDownW.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownW.Name = "numericUpDownW";
            this.numericUpDownW.Size = new System.Drawing.Size(79, 23);
            this.numericUpDownW.TabIndex = 2;
            this.numericUpDownW.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownW.ValueChanged += new System.EventHandler(this.numericUpDownREct_ValueChanged);
            // 
            // numericUpDownY
            // 
            this.numericUpDownY.Location = new System.Drawing.Point(63, 51);
            this.numericUpDownY.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownY.Name = "numericUpDownY";
            this.numericUpDownY.Size = new System.Drawing.Size(79, 23);
            this.numericUpDownY.TabIndex = 1;
            this.numericUpDownY.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownY.ValueChanged += new System.EventHandler(this.numericUpDownREct_ValueChanged);
            // 
            // numericUpDownX
            // 
            this.numericUpDownX.Location = new System.Drawing.Point(63, 22);
            this.numericUpDownX.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownX.Name = "numericUpDownX";
            this.numericUpDownX.Size = new System.Drawing.Size(79, 23);
            this.numericUpDownX.TabIndex = 0;
            this.numericUpDownX.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownX.ValueChanged += new System.EventHandler(this.numericUpDownREct_ValueChanged);
            // 
            // RegionEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.labelIndexThumbnail);
            this.Controls.Add(this.buttonNextImage);
            this.Controls.Add(this.buttonPreviousImage);
            this.Controls.Add(this.groupBoxShape);
            this.Controls.Add(this.buttonClearAllRegions);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.myPictureBox1);
            this.Controls.Add(this.labelInfoText);
            this.Controls.Add(this.buttonClearLastRegion);
            this.Controls.Add(this.labelWarningJSON);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "RegionEditor";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Regions Editor";
            this.Load += new System.EventHandler(this.RegionEditor_Load);
            this.panel1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBoxShape.ResumeLayout(false);
            this.groupBoxShape.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.myPictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownW)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownX)).EndInit();
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
        private System.Windows.Forms.Button buttonClearLastRegion;
        private System.Windows.Forms.Label labelInfoText;
        private myPictureBox myPictureBox1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelMouseInfo;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelXYRect;
        private System.Windows.Forms.Button buttonClearAllRegions;
        private System.Windows.Forms.RadioButton radioButtonRectangle;
        private System.Windows.Forms.RadioButton radioButtonPolygon;
        private System.Windows.Forms.GroupBox groupBoxShape;
        private System.Windows.Forms.Button buttonPreviousImage;
        private System.Windows.Forms.Button buttonNextImage;
        private System.Windows.Forms.Label labelIndexThumbnail;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDownH;
        private System.Windows.Forms.NumericUpDown numericUpDownW;
        private System.Windows.Forms.NumericUpDown numericUpDownY;
        private System.Windows.Forms.NumericUpDown numericUpDownX;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelImSize;
    }
}