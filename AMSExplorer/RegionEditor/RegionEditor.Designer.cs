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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegionEditor));
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
            this.groupBoxRectangleData = new System.Windows.Forms.GroupBox();
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
            this.groupBoxRectangleData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownW)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownX)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOk
            // 
            resources.ApplyResources(this.buttonOk, "buttonOk");
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonOk);
            this.panel1.Name = "panel1";
            // 
            // labelWarningJSON
            // 
            resources.ApplyResources(this.labelWarningJSON, "labelWarningJSON");
            this.labelWarningJSON.ForeColor = System.Drawing.Color.Red;
            this.labelWarningJSON.Name = "labelWarningJSON";
            this.labelWarningJSON.Tag = "";
            // 
            // buttonClearLastRegion
            // 
            resources.ApplyResources(this.buttonClearLastRegion, "buttonClearLastRegion");
            this.buttonClearLastRegion.Name = "buttonClearLastRegion";
            this.buttonClearLastRegion.UseVisualStyleBackColor = true;
            this.buttonClearLastRegion.Click += new System.EventHandler(this.buttonFormat_Click);
            // 
            // labelInfoText
            // 
            resources.ApplyResources(this.labelInfoText, "labelInfoText");
            this.labelInfoText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.labelInfoText.ForeColor = System.Drawing.Color.Black;
            this.labelInfoText.Name = "labelInfoText";
            this.labelInfoText.Tag = "";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelImSize,
            this.toolStripStatusLabelMouseInfo,
            this.toolStripStatusLabelXYRect});
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Name = "statusStrip1";
            // 
            // toolStripStatusLabelImSize
            // 
            this.toolStripStatusLabelImSize.Name = "toolStripStatusLabelImSize";
            resources.ApplyResources(this.toolStripStatusLabelImSize, "toolStripStatusLabelImSize");
            // 
            // toolStripStatusLabelMouseInfo
            // 
            this.toolStripStatusLabelMouseInfo.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripStatusLabelMouseInfo.Image = global::AMSExplorer.Bitmaps.XY;
            this.toolStripStatusLabelMouseInfo.Name = "toolStripStatusLabelMouseInfo";
            resources.ApplyResources(this.toolStripStatusLabelMouseInfo, "toolStripStatusLabelMouseInfo");
            // 
            // toolStripStatusLabelXYRect
            // 
            this.toolStripStatusLabelXYRect.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripStatusLabelXYRect.Image = global::AMSExplorer.Bitmaps.XYRect;
            this.toolStripStatusLabelXYRect.Name = "toolStripStatusLabelXYRect";
            resources.ApplyResources(this.toolStripStatusLabelXYRect, "toolStripStatusLabelXYRect");
            // 
            // buttonClearAllRegions
            // 
            resources.ApplyResources(this.buttonClearAllRegions, "buttonClearAllRegions");
            this.buttonClearAllRegions.Name = "buttonClearAllRegions";
            this.buttonClearAllRegions.UseVisualStyleBackColor = true;
            this.buttonClearAllRegions.Click += new System.EventHandler(this.buttonClearAllRegions_Click);
            // 
            // radioButtonRectangle
            // 
            resources.ApplyResources(this.radioButtonRectangle, "radioButtonRectangle");
            this.radioButtonRectangle.Checked = true;
            this.radioButtonRectangle.Name = "radioButtonRectangle";
            this.radioButtonRectangle.TabStop = true;
            this.radioButtonRectangle.UseVisualStyleBackColor = true;
            // 
            // radioButtonPolygon
            // 
            resources.ApplyResources(this.radioButtonPolygon, "radioButtonPolygon");
            this.radioButtonPolygon.Name = "radioButtonPolygon";
            this.radioButtonPolygon.UseVisualStyleBackColor = true;
            this.radioButtonPolygon.CheckedChanged += new System.EventHandler(this.radioButtonPolygonal_CheckedChanged);
            // 
            // groupBoxShape
            // 
            resources.ApplyResources(this.groupBoxShape, "groupBoxShape");
            this.groupBoxShape.Controls.Add(this.radioButtonRectangle);
            this.groupBoxShape.Controls.Add(this.radioButtonPolygon);
            this.groupBoxShape.Name = "groupBoxShape";
            this.groupBoxShape.TabStop = false;
            // 
            // myPictureBox1
            // 
            resources.ApplyResources(this.myPictureBox1, "myPictureBox1");
            this.myPictureBox1.BackColor = System.Drawing.SystemColors.Window;
            this.myPictureBox1.Cursor = System.Windows.Forms.Cursors.Cross;
            this.myPictureBox1.LastRectangle = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.myPictureBox1.Name = "myPictureBox1";
            this.myPictureBox1.TabStop = false;
            this.myPictureBox1.SizeChanged += new System.EventHandler(this.myPictureBox1_SizeChanged);
            this.myPictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.myPictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.myPictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // dataGridViewTextBoxColumn1
            // 
            resources.ApplyResources(this.dataGridViewTextBoxColumn1, "dataGridViewTextBoxColumn1");
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            resources.ApplyResources(this.dataGridViewTextBoxColumn2, "dataGridViewTextBoxColumn2");
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // buttonPreviousImage
            // 
            resources.ApplyResources(this.buttonPreviousImage, "buttonPreviousImage");
            this.buttonPreviousImage.Name = "buttonPreviousImage";
            this.buttonPreviousImage.UseVisualStyleBackColor = true;
            this.buttonPreviousImage.Click += new System.EventHandler(this.buttonPreviousImage_Click);
            // 
            // buttonNextImage
            // 
            resources.ApplyResources(this.buttonNextImage, "buttonNextImage");
            this.buttonNextImage.Name = "buttonNextImage";
            this.buttonNextImage.UseVisualStyleBackColor = true;
            this.buttonNextImage.Click += new System.EventHandler(this.buttonNextImage_Click);
            // 
            // labelIndexThumbnail
            // 
            resources.ApplyResources(this.labelIndexThumbnail, "labelIndexThumbnail");
            this.labelIndexThumbnail.Name = "labelIndexThumbnail";
            // 
            // groupBoxRectangleData
            // 
            resources.ApplyResources(this.groupBoxRectangleData, "groupBoxRectangleData");
            this.groupBoxRectangleData.Controls.Add(this.label4);
            this.groupBoxRectangleData.Controls.Add(this.label3);
            this.groupBoxRectangleData.Controls.Add(this.label2);
            this.groupBoxRectangleData.Controls.Add(this.label1);
            this.groupBoxRectangleData.Controls.Add(this.numericUpDownH);
            this.groupBoxRectangleData.Controls.Add(this.numericUpDownW);
            this.groupBoxRectangleData.Controls.Add(this.numericUpDownY);
            this.groupBoxRectangleData.Controls.Add(this.numericUpDownX);
            this.groupBoxRectangleData.Name = "groupBoxRectangleData";
            this.groupBoxRectangleData.TabStop = false;
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // numericUpDownH
            // 
            resources.ApplyResources(this.numericUpDownH, "numericUpDownH");
            this.numericUpDownH.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownH.Name = "numericUpDownH";
            this.numericUpDownH.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownH.ValueChanged += new System.EventHandler(this.numericUpDownREct_ValueChanged);
            // 
            // numericUpDownW
            // 
            resources.ApplyResources(this.numericUpDownW, "numericUpDownW");
            this.numericUpDownW.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownW.Name = "numericUpDownW";
            this.numericUpDownW.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownW.ValueChanged += new System.EventHandler(this.numericUpDownREct_ValueChanged);
            // 
            // numericUpDownY
            // 
            resources.ApplyResources(this.numericUpDownY, "numericUpDownY");
            this.numericUpDownY.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownY.Name = "numericUpDownY";
            this.numericUpDownY.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownY.ValueChanged += new System.EventHandler(this.numericUpDownREct_ValueChanged);
            // 
            // numericUpDownX
            // 
            resources.ApplyResources(this.numericUpDownX, "numericUpDownX");
            this.numericUpDownX.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownX.Name = "numericUpDownX";
            this.numericUpDownX.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownX.ValueChanged += new System.EventHandler(this.numericUpDownREct_ValueChanged);
            // 
            // RegionEditor
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.groupBoxRectangleData);
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
            this.Name = "RegionEditor";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.panel1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBoxShape.ResumeLayout(false);
            this.groupBoxShape.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.myPictureBox1)).EndInit();
            this.groupBoxRectangleData.ResumeLayout(false);
            this.groupBoxRectangleData.PerformLayout();
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
        private System.Windows.Forms.GroupBox groupBoxRectangleData;
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