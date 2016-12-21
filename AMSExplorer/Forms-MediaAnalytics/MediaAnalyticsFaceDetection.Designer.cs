namespace AMSExplorer
{
    partial class MediaAnalyticsFaceDetection
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MediaAnalyticsFaceDetection));
            this.label3 = new System.Windows.Forms.Label();
            this.textboxoutputassetname = new System.Windows.Forms.TextBox();
            this.labelProcessorVersion = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelAssetName = new System.Windows.Forms.Label();
            this.textBoxJobName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonOk = new System.Windows.Forms.Button();
            this.labelProcessorName = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.moreinfoprofilelink = new System.Windows.Forms.LinkLabel();
            this.labelPreview = new System.Windows.Forms.Label();
            this.groupBoxAggregateSettings = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownAggregateInterval = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownAggregateWindow = new System.Windows.Forms.NumericUpDown();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.radioButtonFaceDetection = new System.Windows.Forms.RadioButton();
            this.radioButtonAggregateEmotionDetection = new System.Windows.Forms.RadioButton();
            this.radioButtonPerFaceEmotion = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonJobOptions = new AMSExplorer.ButtonJobOptions();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPageConfig = new System.Windows.Forms.TabPage();
            this.labelWarningJSON = new System.Windows.Forms.Label();
            this.textBoxConfiguration = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.groupBoxAggregateSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAggregateInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAggregateWindow)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPageConfig.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // textboxoutputassetname
            // 
            resources.ApplyResources(this.textboxoutputassetname, "textboxoutputassetname");
            this.textboxoutputassetname.Name = "textboxoutputassetname";
            // 
            // labelProcessorVersion
            // 
            resources.ApplyResources(this.labelProcessorVersion, "labelProcessorVersion");
            this.labelProcessorVersion.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.labelProcessorVersion.Name = "labelProcessorVersion";
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // labelAssetName
            // 
            resources.ApplyResources(this.labelAssetName, "labelAssetName");
            this.labelAssetName.Name = "labelAssetName";
            // 
            // textBoxJobName
            // 
            resources.ApplyResources(this.textBoxJobName, "textBoxJobName");
            this.textBoxJobName.Name = "textBoxJobName";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // buttonOk
            // 
            resources.ApplyResources(this.buttonOk, "buttonOk");
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Image = global::AMSExplorer.Bitmaps.hyperlapse;
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // labelProcessorName
            // 
            resources.ApplyResources(this.labelProcessorName, "labelProcessorName");
            this.labelProcessorName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(128)))), ((int)(((byte)(171)))));
            this.labelProcessorName.Name = "labelProcessorName";
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonOk);
            this.panel1.Name = "panel1";
            // 
            // moreinfoprofilelink
            // 
            resources.ApplyResources(this.moreinfoprofilelink, "moreinfoprofilelink");
            this.moreinfoprofilelink.Name = "moreinfoprofilelink";
            this.moreinfoprofilelink.TabStop = true;
            this.moreinfoprofilelink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.moreinfoprofilelink_LinkClicked);
            // 
            // labelPreview
            // 
            resources.ApplyResources(this.labelPreview, "labelPreview");
            this.labelPreview.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(128)))), ((int)(((byte)(171)))));
            this.labelPreview.Name = "labelPreview";
            // 
            // groupBoxAggregateSettings
            // 
            this.groupBoxAggregateSettings.Controls.Add(this.label4);
            this.groupBoxAggregateSettings.Controls.Add(this.label2);
            this.groupBoxAggregateSettings.Controls.Add(this.numericUpDownAggregateInterval);
            this.groupBoxAggregateSettings.Controls.Add(this.numericUpDownAggregateWindow);
            resources.ApplyResources(this.groupBoxAggregateSettings, "groupBoxAggregateSettings");
            this.groupBoxAggregateSettings.Name = "groupBoxAggregateSettings";
            this.groupBoxAggregateSettings.TabStop = false;
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // numericUpDownAggregateInterval
            // 
            resources.ApplyResources(this.numericUpDownAggregateInterval, "numericUpDownAggregateInterval");
            this.numericUpDownAggregateInterval.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownAggregateInterval.Minimum = new decimal(new int[] {
            250,
            0,
            0,
            0});
            this.numericUpDownAggregateInterval.Name = "numericUpDownAggregateInterval";
            this.toolTip1.SetToolTip(this.numericUpDownAggregateInterval, resources.GetString("numericUpDownAggregateInterval.ToolTip"));
            this.numericUpDownAggregateInterval.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numericUpDownAggregateInterval.ValueChanged += new System.EventHandler(this.control_changed);
            // 
            // numericUpDownAggregateWindow
            // 
            resources.ApplyResources(this.numericUpDownAggregateWindow, "numericUpDownAggregateWindow");
            this.numericUpDownAggregateWindow.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.numericUpDownAggregateWindow.Minimum = new decimal(new int[] {
            250,
            0,
            0,
            0});
            this.numericUpDownAggregateWindow.Name = "numericUpDownAggregateWindow";
            this.toolTip1.SetToolTip(this.numericUpDownAggregateWindow, resources.GetString("numericUpDownAggregateWindow.ToolTip"));
            this.numericUpDownAggregateWindow.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numericUpDownAggregateWindow.ValueChanged += new System.EventHandler(this.control_changed);
            // 
            // radioButtonFaceDetection
            // 
            resources.ApplyResources(this.radioButtonFaceDetection, "radioButtonFaceDetection");
            this.radioButtonFaceDetection.Checked = true;
            this.radioButtonFaceDetection.Name = "radioButtonFaceDetection";
            this.radioButtonFaceDetection.TabStop = true;
            this.toolTip1.SetToolTip(this.radioButtonFaceDetection, resources.GetString("radioButtonFaceDetection.ToolTip"));
            this.radioButtonFaceDetection.UseVisualStyleBackColor = true;
            this.radioButtonFaceDetection.CheckedChanged += new System.EventHandler(this.radioButtonDetectionMode_CheckedChanged);
            // 
            // radioButtonAggregateEmotionDetection
            // 
            resources.ApplyResources(this.radioButtonAggregateEmotionDetection, "radioButtonAggregateEmotionDetection");
            this.radioButtonAggregateEmotionDetection.Name = "radioButtonAggregateEmotionDetection";
            this.toolTip1.SetToolTip(this.radioButtonAggregateEmotionDetection, resources.GetString("radioButtonAggregateEmotionDetection.ToolTip"));
            this.radioButtonAggregateEmotionDetection.UseVisualStyleBackColor = true;
            this.radioButtonAggregateEmotionDetection.CheckedChanged += new System.EventHandler(this.radioButtonDetectionMode_CheckedChanged);
            // 
            // radioButtonPerFaceEmotion
            // 
            resources.ApplyResources(this.radioButtonPerFaceEmotion, "radioButtonPerFaceEmotion");
            this.radioButtonPerFaceEmotion.Name = "radioButtonPerFaceEmotion";
            this.radioButtonPerFaceEmotion.UseVisualStyleBackColor = true;
            this.radioButtonPerFaceEmotion.CheckedChanged += new System.EventHandler(this.radioButtonDetectionMode_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButtonPerFaceEmotion);
            this.groupBox1.Controls.Add(this.radioButtonFaceDetection);
            this.groupBox1.Controls.Add(this.radioButtonAggregateEmotionDetection);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // buttonJobOptions
            // 
            resources.ApplyResources(this.buttonJobOptions, "buttonJobOptions");
            this.buttonJobOptions.Name = "buttonJobOptions";
            this.buttonJobOptions.UseVisualStyleBackColor = true;
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
            // pictureBox1
            // 
            this.pictureBox1.Image = global::AMSExplorer.Bitmaps._04_face_detection;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // tabControl1
            // 
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPageConfig);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.groupBoxAggregateSettings);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPageConfig
            // 
            this.tabPageConfig.Controls.Add(this.labelWarningJSON);
            this.tabPageConfig.Controls.Add(this.textBoxConfiguration);
            this.tabPageConfig.Controls.Add(this.label9);
            resources.ApplyResources(this.tabPageConfig, "tabPageConfig");
            this.tabPageConfig.Name = "tabPageConfig";
            this.tabPageConfig.UseVisualStyleBackColor = true;
            this.tabPageConfig.Enter += new System.EventHandler(this.control_changed);
            // 
            // labelWarningJSON
            // 
            resources.ApplyResources(this.labelWarningJSON, "labelWarningJSON");
            this.labelWarningJSON.ForeColor = System.Drawing.Color.Red;
            this.labelWarningJSON.Name = "labelWarningJSON";
            this.labelWarningJSON.Tag = "JSON Syntax error. {0}";
            // 
            // textBoxConfiguration
            // 
            resources.ApplyResources(this.textBoxConfiguration, "textBoxConfiguration");
            this.textBoxConfiguration.Name = "textBoxConfiguration";
            this.textBoxConfiguration.TextChanged += new System.EventHandler(this.textBoxConfiguration_TextChanged);
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // panel2
            // 
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Name = "panel2";
            // 
            // MediaAnalyticsFaceDetection
            // 
            this.AcceptButton = this.buttonOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.labelPreview);
            this.Controls.Add(this.buttonJobOptions);
            this.Controls.Add(this.moreinfoprofilelink);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labelProcessorName);
            this.Controls.Add(this.textBoxJobName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.labelAssetName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textboxoutputassetname);
            this.Controls.Add(this.labelProcessorVersion);
            this.Name = "MediaAnalyticsFaceDetection";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.MediaAnalyticsFaceDetection_Load);
            this.panel1.ResumeLayout(false);
            this.groupBoxAggregateSettings.ResumeLayout(false);
            this.groupBoxAggregateSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAggregateInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAggregateWindow)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPageConfig.ResumeLayout(false);
            this.tabPageConfig.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox textboxoutputassetname;
        private System.Windows.Forms.Label labelProcessorVersion;
        public System.Windows.Forms.Button buttonOk;
        public System.Windows.Forms.Button buttonCancel;
        public System.Windows.Forms.Label labelAssetName;
        public System.Windows.Forms.TextBox textBoxJobName;
        public System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        public System.Windows.Forms.Label labelProcessorName;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.LinkLabel moreinfoprofilelink;
        private ButtonJobOptions buttonJobOptions;
        public System.Windows.Forms.Label labelPreview;
        private System.Windows.Forms.GroupBox groupBoxAggregateSettings;
        private System.Windows.Forms.NumericUpDown numericUpDownAggregateInterval;
        private System.Windows.Forms.NumericUpDown numericUpDownAggregateWindow;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButtonFaceDetection;
        private System.Windows.Forms.RadioButton radioButtonAggregateEmotionDetection;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPageConfig;
        private System.Windows.Forms.Label labelWarningJSON;
        private System.Windows.Forms.TextBox textBoxConfiguration;
        public System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton radioButtonPerFaceEmotion;
    }
}