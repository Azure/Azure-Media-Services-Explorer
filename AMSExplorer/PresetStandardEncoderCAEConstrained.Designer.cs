namespace AMSExplorer
{
    partial class PresetStandardEncoderCAEConstrained
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PresetStandardEncoderCAEConstrained));
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.labelMES = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.moreinfoprofilelink = new System.Windows.Forms.LinkLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.numericUpDownKeyFrame = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxInterleave = new System.Windows.Forms.CheckBox();
            this.checkBoxComplexity = new System.Windows.Forms.CheckBox();
            this.comboBoxInterleaveOutput = new System.Windows.Forms.ComboBox();
            this.comboBoxComplexity = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBoxKeyFrame = new System.Windows.Forms.CheckBox();
            this.checkBoxMaxLayers = new System.Windows.Forms.CheckBox();
            this.checkBoxMaxHeight = new System.Windows.Forms.CheckBox();
            this.checkBoxMinHeight = new System.Windows.Forms.CheckBox();
            this.checkBoxMaxBitrate = new System.Windows.Forms.CheckBox();
            this.checkBoxMinBitrate = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.numericUpDownMaxLayers = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownMinBitrate = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownMinHeight = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownMaxBitrate = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownMaxHeight = new System.Windows.Forms.NumericUpDown();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownKeyFrame)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxLayers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinBitrate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxBitrate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxHeight)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Name = "buttonCancel";
            this.toolTip1.SetToolTip(this.buttonCancel, resources.GetString("buttonCancel.ToolTip"));
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOk
            // 
            resources.ApplyResources(this.buttonOk, "buttonOk");
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Name = "buttonOk";
            this.toolTip1.SetToolTip(this.buttonOk, resources.GetString("buttonOk.ToolTip"));
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // labelMES
            // 
            resources.ApplyResources(this.labelMES, "labelMES");
            this.labelMES.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(128)))), ((int)(((byte)(171)))));
            this.labelMES.Name = "labelMES";
            this.toolTip1.SetToolTip(this.labelMES, resources.GetString("labelMES.ToolTip"));
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonOk);
            this.panel1.Name = "panel1";
            this.toolTip1.SetToolTip(this.panel1, resources.GetString("panel1.ToolTip"));
            // 
            // moreinfoprofilelink
            // 
            resources.ApplyResources(this.moreinfoprofilelink, "moreinfoprofilelink");
            this.moreinfoprofilelink.Name = "moreinfoprofilelink";
            this.moreinfoprofilelink.TabStop = true;
            this.toolTip1.SetToolTip(this.moreinfoprofilelink, resources.GetString("moreinfoprofilelink.ToolTip"));
            this.moreinfoprofilelink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.moreinfoprofilelink_LinkClicked);
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBox1, resources.GetString("pictureBox1.ToolTip"));
            // 
            // numericUpDownKeyFrame
            // 
            resources.ApplyResources(this.numericUpDownKeyFrame, "numericUpDownKeyFrame");
            this.numericUpDownKeyFrame.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownKeyFrame.Name = "numericUpDownKeyFrame";
            this.toolTip1.SetToolTip(this.numericUpDownKeyFrame, resources.GetString("numericUpDownKeyFrame.ToolTip"));
            this.numericUpDownKeyFrame.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.checkBoxInterleave);
            this.groupBox1.Controls.Add(this.checkBoxComplexity);
            this.groupBox1.Controls.Add(this.comboBoxInterleaveOutput);
            this.groupBox1.Controls.Add(this.comboBoxComplexity);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            this.toolTip1.SetToolTip(this.groupBox1, resources.GetString("groupBox1.ToolTip"));
            // 
            // checkBoxInterleave
            // 
            resources.ApplyResources(this.checkBoxInterleave, "checkBoxInterleave");
            this.checkBoxInterleave.Name = "checkBoxInterleave";
            this.toolTip1.SetToolTip(this.checkBoxInterleave, resources.GetString("checkBoxInterleave.ToolTip"));
            this.checkBoxInterleave.UseVisualStyleBackColor = true;
            this.checkBoxInterleave.CheckedChanged += new System.EventHandler(this.checkBoxInterleave_CheckedChanged);
            // 
            // checkBoxComplexity
            // 
            resources.ApplyResources(this.checkBoxComplexity, "checkBoxComplexity");
            this.checkBoxComplexity.Name = "checkBoxComplexity";
            this.toolTip1.SetToolTip(this.checkBoxComplexity, resources.GetString("checkBoxComplexity.ToolTip"));
            this.checkBoxComplexity.UseVisualStyleBackColor = true;
            this.checkBoxComplexity.CheckedChanged += new System.EventHandler(this.checkBoxComplexity_CheckedChanged);
            // 
            // comboBoxInterleaveOutput
            // 
            resources.ApplyResources(this.comboBoxInterleaveOutput, "comboBoxInterleaveOutput");
            this.comboBoxInterleaveOutput.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxInterleaveOutput.FormattingEnabled = true;
            this.comboBoxInterleaveOutput.Name = "comboBoxInterleaveOutput";
            this.toolTip1.SetToolTip(this.comboBoxInterleaveOutput, resources.GetString("comboBoxInterleaveOutput.ToolTip"));
            // 
            // comboBoxComplexity
            // 
            resources.ApplyResources(this.comboBoxComplexity, "comboBoxComplexity");
            this.comboBoxComplexity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxComplexity.FormattingEnabled = true;
            this.comboBoxComplexity.Name = "comboBoxComplexity";
            this.toolTip1.SetToolTip(this.comboBoxComplexity, resources.GetString("comboBoxComplexity.ToolTip"));
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.checkBoxKeyFrame);
            this.groupBox2.Controls.Add(this.checkBoxMaxLayers);
            this.groupBox2.Controls.Add(this.checkBoxMaxHeight);
            this.groupBox2.Controls.Add(this.checkBoxMinHeight);
            this.groupBox2.Controls.Add(this.checkBoxMaxBitrate);
            this.groupBox2.Controls.Add(this.checkBoxMinBitrate);
            this.groupBox2.Controls.Add(this.numericUpDownKeyFrame);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.numericUpDownMaxLayers);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.numericUpDownMinBitrate);
            this.groupBox2.Controls.Add(this.numericUpDownMinHeight);
            this.groupBox2.Controls.Add(this.numericUpDownMaxBitrate);
            this.groupBox2.Controls.Add(this.numericUpDownMaxHeight);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            this.toolTip1.SetToolTip(this.groupBox2, resources.GetString("groupBox2.ToolTip"));
            // 
            // checkBoxKeyFrame
            // 
            resources.ApplyResources(this.checkBoxKeyFrame, "checkBoxKeyFrame");
            this.checkBoxKeyFrame.Name = "checkBoxKeyFrame";
            this.toolTip1.SetToolTip(this.checkBoxKeyFrame, resources.GetString("checkBoxKeyFrame.ToolTip"));
            this.checkBoxKeyFrame.UseVisualStyleBackColor = true;
            this.checkBoxKeyFrame.CheckedChanged += new System.EventHandler(this.checkBoxKeyFrame_CheckedChanged);
            // 
            // checkBoxMaxLayers
            // 
            resources.ApplyResources(this.checkBoxMaxLayers, "checkBoxMaxLayers");
            this.checkBoxMaxLayers.Name = "checkBoxMaxLayers";
            this.toolTip1.SetToolTip(this.checkBoxMaxLayers, resources.GetString("checkBoxMaxLayers.ToolTip"));
            this.checkBoxMaxLayers.UseVisualStyleBackColor = true;
            this.checkBoxMaxLayers.CheckedChanged += new System.EventHandler(this.checkBoxMaxLayers_CheckedChanged);
            // 
            // checkBoxMaxHeight
            // 
            resources.ApplyResources(this.checkBoxMaxHeight, "checkBoxMaxHeight");
            this.checkBoxMaxHeight.Name = "checkBoxMaxHeight";
            this.toolTip1.SetToolTip(this.checkBoxMaxHeight, resources.GetString("checkBoxMaxHeight.ToolTip"));
            this.checkBoxMaxHeight.UseVisualStyleBackColor = true;
            this.checkBoxMaxHeight.CheckedChanged += new System.EventHandler(this.checkBoxMaxHeight_CheckedChanged);
            // 
            // checkBoxMinHeight
            // 
            resources.ApplyResources(this.checkBoxMinHeight, "checkBoxMinHeight");
            this.checkBoxMinHeight.Name = "checkBoxMinHeight";
            this.toolTip1.SetToolTip(this.checkBoxMinHeight, resources.GetString("checkBoxMinHeight.ToolTip"));
            this.checkBoxMinHeight.UseVisualStyleBackColor = true;
            this.checkBoxMinHeight.CheckedChanged += new System.EventHandler(this.checkBoxMinHeight_CheckedChanged);
            // 
            // checkBoxMaxBitrate
            // 
            resources.ApplyResources(this.checkBoxMaxBitrate, "checkBoxMaxBitrate");
            this.checkBoxMaxBitrate.Name = "checkBoxMaxBitrate";
            this.toolTip1.SetToolTip(this.checkBoxMaxBitrate, resources.GetString("checkBoxMaxBitrate.ToolTip"));
            this.checkBoxMaxBitrate.UseVisualStyleBackColor = true;
            this.checkBoxMaxBitrate.CheckedChanged += new System.EventHandler(this.checkBoxMaxBitrate_CheckedChanged);
            // 
            // checkBoxMinBitrate
            // 
            resources.ApplyResources(this.checkBoxMinBitrate, "checkBoxMinBitrate");
            this.checkBoxMinBitrate.Name = "checkBoxMinBitrate";
            this.toolTip1.SetToolTip(this.checkBoxMinBitrate, resources.GetString("checkBoxMinBitrate.ToolTip"));
            this.checkBoxMinBitrate.UseVisualStyleBackColor = true;
            this.checkBoxMinBitrate.CheckedChanged += new System.EventHandler(this.checkBoxMinBitrate_CheckedChanged);
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label6.Name = "label6";
            this.toolTip1.SetToolTip(this.label6, resources.GetString("label6.ToolTip"));
            // 
            // numericUpDownMaxLayers
            // 
            resources.ApplyResources(this.numericUpDownMaxLayers, "numericUpDownMaxLayers");
            this.numericUpDownMaxLayers.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownMaxLayers.Name = "numericUpDownMaxLayers";
            this.toolTip1.SetToolTip(this.numericUpDownMaxLayers, resources.GetString("numericUpDownMaxLayers.ToolTip"));
            this.numericUpDownMaxLayers.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label2.Name = "label2";
            this.toolTip1.SetToolTip(this.label2, resources.GetString("label2.ToolTip"));
            // 
            // numericUpDownMinBitrate
            // 
            resources.ApplyResources(this.numericUpDownMinBitrate, "numericUpDownMinBitrate");
            this.numericUpDownMinBitrate.Maximum = new decimal(new int[] {
            600000000,
            0,
            0,
            0});
            this.numericUpDownMinBitrate.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownMinBitrate.Name = "numericUpDownMinBitrate";
            this.toolTip1.SetToolTip(this.numericUpDownMinBitrate, resources.GetString("numericUpDownMinBitrate.ToolTip"));
            this.numericUpDownMinBitrate.Value = new decimal(new int[] {
            2000000,
            0,
            0,
            0});
            // 
            // numericUpDownMinHeight
            // 
            resources.ApplyResources(this.numericUpDownMinHeight, "numericUpDownMinHeight");
            this.numericUpDownMinHeight.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownMinHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownMinHeight.Name = "numericUpDownMinHeight";
            this.toolTip1.SetToolTip(this.numericUpDownMinHeight, resources.GetString("numericUpDownMinHeight.ToolTip"));
            this.numericUpDownMinHeight.Value = new decimal(new int[] {
            240,
            0,
            0,
            0});
            // 
            // numericUpDownMaxBitrate
            // 
            resources.ApplyResources(this.numericUpDownMaxBitrate, "numericUpDownMaxBitrate");
            this.numericUpDownMaxBitrate.Maximum = new decimal(new int[] {
            600000000,
            0,
            0,
            0});
            this.numericUpDownMaxBitrate.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownMaxBitrate.Name = "numericUpDownMaxBitrate";
            this.toolTip1.SetToolTip(this.numericUpDownMaxBitrate, resources.GetString("numericUpDownMaxBitrate.ToolTip"));
            this.numericUpDownMaxBitrate.Value = new decimal(new int[] {
            6000000,
            0,
            0,
            0});
            // 
            // numericUpDownMaxHeight
            // 
            resources.ApplyResources(this.numericUpDownMaxHeight, "numericUpDownMaxHeight");
            this.numericUpDownMaxHeight.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownMaxHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownMaxHeight.Name = "numericUpDownMaxHeight";
            this.toolTip1.SetToolTip(this.numericUpDownMaxHeight, resources.GetString("numericUpDownMaxHeight.ToolTip"));
            this.numericUpDownMaxHeight.Value = new decimal(new int[] {
            720,
            0,
            0,
            0});
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
            // PresetStandardEncoderCAEConstrained
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.moreinfoprofilelink);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labelMES);
            this.Name = "PresetStandardEncoderCAEConstrained";
            this.toolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.Load += new System.EventHandler(this.PresetStandardEncoderCAEConstrained_Load);
            this.Shown += new System.EventHandler(this.PresetStandardEncoderThumbnail_Shown);
            this.DpiChanged += new System.Windows.Forms.DpiChangedEventHandler(this.PresetStandardEncoderThumbnail_DpiChanged);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownKeyFrame)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxLayers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinBitrate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxBitrate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxHeight)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.Button buttonOk;
        public System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        public System.Windows.Forms.Label labelMES;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.LinkLabel moreinfoprofilelink;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.NumericUpDown numericUpDownKeyFrame;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox comboBoxInterleaveOutput;
        private System.Windows.Forms.ComboBox comboBoxComplexity;
        public System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numericUpDownMaxLayers;
        public System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDownMinBitrate;
        private System.Windows.Forms.NumericUpDown numericUpDownMinHeight;
        private System.Windows.Forms.NumericUpDown numericUpDownMaxBitrate;
        private System.Windows.Forms.NumericUpDown numericUpDownMaxHeight;
        private System.Windows.Forms.CheckBox checkBoxKeyFrame;
        private System.Windows.Forms.CheckBox checkBoxMaxLayers;
        private System.Windows.Forms.CheckBox checkBoxMaxHeight;
        private System.Windows.Forms.CheckBox checkBoxMinHeight;
        private System.Windows.Forms.CheckBox checkBoxMaxBitrate;
        private System.Windows.Forms.CheckBox checkBoxMinBitrate;
        private System.Windows.Forms.CheckBox checkBoxInterleave;
        private System.Windows.Forms.CheckBox checkBoxComplexity;
    }
}