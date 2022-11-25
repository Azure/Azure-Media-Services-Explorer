namespace AMSExplorer
{
    partial class PresetVideoAnalyzer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PresetVideoAnalyzer));
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.labelAVAnalyzer = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.comboBoxLanguage = new System.Windows.Forms.ComboBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.checkBoxAutoLanguage = new System.Windows.Forms.CheckBox();
            this.moreinfoprofilelink = new System.Windows.Forms.LinkLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxTransformName = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtonVideoOnly = new System.Windows.Forms.RadioButton();
            this.radioButtonAudioAndVideo = new System.Windows.Forms.RadioButton();
            this.radioButtonAudioOnly = new System.Windows.Forms.RadioButton();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBoxAudioMode = new System.Windows.Forms.GroupBox();
            this.radioButtonAudioStandard = new System.Windows.Forms.RadioButton();
            this.radioButtonAudioBasic = new System.Windows.Forms.RadioButton();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBoxAudioMode.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOk
            // 
            resources.ApplyResources(this.buttonOk, "buttonOk");
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // labelAVAnalyzer
            // 
            resources.ApplyResources(this.labelAVAnalyzer, "labelAVAnalyzer");
            this.labelAVAnalyzer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(128)))), ((int)(((byte)(171)))));
            this.labelAVAnalyzer.Name = "labelAVAnalyzer";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonOk);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // comboBoxLanguage
            // 
            this.comboBoxLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLanguage.FormattingEnabled = true;
            resources.ApplyResources(this.comboBoxLanguage, "comboBoxLanguage");
            this.comboBoxLanguage.Name = "comboBoxLanguage";
            this.toolTip1.SetToolTip(this.comboBoxLanguage, resources.GetString("comboBoxLanguage.ToolTip"));
            this.comboBoxLanguage.SelectedIndexChanged += new System.EventHandler(this.comboBoxLanguage_SelectedIndexChanged);
            // 
            // checkBoxAutoLanguage
            // 
            resources.ApplyResources(this.checkBoxAutoLanguage, "checkBoxAutoLanguage");
            this.checkBoxAutoLanguage.Name = "checkBoxAutoLanguage";
            this.toolTip1.SetToolTip(this.checkBoxAutoLanguage, resources.GetString("checkBoxAutoLanguage.ToolTip"));
            this.checkBoxAutoLanguage.UseVisualStyleBackColor = true;
            this.checkBoxAutoLanguage.CheckedChanged += new System.EventHandler(this.checkBoxAutoLanguage_CheckedChanged);
            // 
            // moreinfoprofilelink
            // 
            resources.ApplyResources(this.moreinfoprofilelink, "moreinfoprofilelink");
            this.moreinfoprofilelink.Name = "moreinfoprofilelink";
            this.moreinfoprofilelink.TabStop = true;
            this.moreinfoprofilelink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.moreinfoprofilelink_LinkClicked);
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // textBoxTransformName
            // 
            resources.ApplyResources(this.textBoxTransformName, "textBoxTransformName");
            this.textBoxTransformName.Name = "textBoxTransformName";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButtonVideoOnly);
            this.groupBox1.Controls.Add(this.radioButtonAudioAndVideo);
            this.groupBox1.Controls.Add(this.radioButtonAudioOnly);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // radioButtonVideoOnly
            // 
            resources.ApplyResources(this.radioButtonVideoOnly, "radioButtonVideoOnly");
            this.radioButtonVideoOnly.Name = "radioButtonVideoOnly";
            this.radioButtonVideoOnly.UseVisualStyleBackColor = true;
            this.radioButtonVideoOnly.CheckedChanged += new System.EventHandler(this.radioButtonAudioAndVideo_CheckedChanged);
            // 
            // radioButtonAudioAndVideo
            // 
            resources.ApplyResources(this.radioButtonAudioAndVideo, "radioButtonAudioAndVideo");
            this.radioButtonAudioAndVideo.Checked = true;
            this.radioButtonAudioAndVideo.Name = "radioButtonAudioAndVideo";
            this.radioButtonAudioAndVideo.TabStop = true;
            this.radioButtonAudioAndVideo.UseVisualStyleBackColor = true;
            this.radioButtonAudioAndVideo.CheckedChanged += new System.EventHandler(this.radioButtonAudioAndVideo_CheckedChanged);
            // 
            // radioButtonAudioOnly
            // 
            resources.ApplyResources(this.radioButtonAudioOnly, "radioButtonAudioOnly");
            this.radioButtonAudioOnly.Name = "radioButtonAudioOnly";
            this.radioButtonAudioOnly.UseVisualStyleBackColor = true;
            this.radioButtonAudioOnly.CheckedChanged += new System.EventHandler(this.radioButtonAudioAndVideo_CheckedChanged);
            // 
            // textBoxDescription
            // 
            resources.ApplyResources(this.textBoxDescription, "textBoxDescription");
            this.textBoxDescription.Name = "textBoxDescription";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.comboBoxLanguage);
            this.groupBox2.Controls.Add(this.checkBoxAutoLanguage);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // groupBoxAudioMode
            // 
            this.groupBoxAudioMode.Controls.Add(this.radioButtonAudioStandard);
            this.groupBoxAudioMode.Controls.Add(this.radioButtonAudioBasic);
            resources.ApplyResources(this.groupBoxAudioMode, "groupBoxAudioMode");
            this.groupBoxAudioMode.Name = "groupBoxAudioMode";
            this.groupBoxAudioMode.TabStop = false;
            // 
            // radioButtonAudioStandard
            // 
            resources.ApplyResources(this.radioButtonAudioStandard, "radioButtonAudioStandard");
            this.radioButtonAudioStandard.Checked = true;
            this.radioButtonAudioStandard.Name = "radioButtonAudioStandard";
            this.radioButtonAudioStandard.TabStop = true;
            this.radioButtonAudioStandard.UseVisualStyleBackColor = true;
            // 
            // radioButtonAudioBasic
            // 
            resources.ApplyResources(this.radioButtonAudioBasic, "radioButtonAudioBasic");
            this.radioButtonAudioBasic.Name = "radioButtonAudioBasic";
            this.radioButtonAudioBasic.UseVisualStyleBackColor = true;
            this.radioButtonAudioBasic.CheckedChanged += new System.EventHandler(this.radioButtonAudioBasic_CheckedChanged);
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
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Name = "label3";
            // 
            // PresetVideoAnalyzer
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBoxAudioMode);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.textBoxDescription);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.textBoxTransformName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.moreinfoprofilelink);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labelAVAnalyzer);
            this.Name = "PresetVideoAnalyzer";
            this.Load += new System.EventHandler(this.PresetVideoAnalyzer_Load);
            this.Shown += new System.EventHandler(this.PresetVideoAnalyzer_Shown);
            this.DpiChanged += new System.Windows.Forms.DpiChangedEventHandler(this.PresetVideoAnalyzer_DpiChanged);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBoxAudioMode.ResumeLayout(false);
            this.groupBoxAudioMode.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.Button buttonOk;
        public System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        public System.Windows.Forms.Label labelAVAnalyzer;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox comboBoxLanguage;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.LinkLabel moreinfoprofilelink;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButtonAudioAndVideo;
        private System.Windows.Forms.RadioButton radioButtonAudioOnly;
        private System.Windows.Forms.TextBox textBoxTransformName;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBoxAutoLanguage;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioButtonVideoOnly;
        private System.Windows.Forms.GroupBox groupBoxAudioMode;
        private System.Windows.Forms.RadioButton radioButtonAudioStandard;
        private System.Windows.Forms.RadioButton radioButtonAudioBasic;
        private System.Windows.Forms.Label label3;
    }
}