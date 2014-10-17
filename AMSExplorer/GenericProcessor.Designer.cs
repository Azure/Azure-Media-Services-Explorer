namespace AMSExplorer
{
    partial class GenericProcessor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GenericProcessor));
            this.label1 = new System.Windows.Forms.Label();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.textBoxJobName = new System.Windows.Forms.TextBox();
            this.openFileDialogPreset = new System.Windows.Forms.OpenFileDialog();
            this.processorlabel = new System.Windows.Forms.Label();
            this.numericUpDownPriority = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.radioButtonSingleTaskSingleJob = new System.Windows.Forms.RadioButton();
            this.radioButtonMultipleTasksMultipleJobs = new System.Windows.Forms.RadioButton();
            this.listViewProcessors = new System.Windows.Forms.ListView();
            this.ListViewVendor = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ListViewName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ListViewVersion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ListViewDesc = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textboxoutputassetname = new System.Windows.Forms.TextBox();
            this.listViewInputAssets = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.pictureBoxJob = new System.Windows.Forms.PictureBox();
            this.labelsummaryjob = new System.Windows.Forms.Label();
            this.radioButtonMultipleTasksSingleJob = new System.Windows.Forms.RadioButton();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.textBoxConfiguration = new System.Windows.Forms.TextBox();
            this.labelWarning = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPriority)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxJob)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 159);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Job Name :";
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Enabled = false;
            this.buttonOk.Location = new System.Drawing.Point(293, 517);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(113, 32);
            this.buttonOk.TabIndex = 17;
            this.buttonOk.Text = "Submit job(s)";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(416, 517);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(113, 32);
            this.buttonCancel.TabIndex = 16;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // textBoxJobName
            // 
            this.textBoxJobName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxJobName.Location = new System.Drawing.Point(16, 176);
            this.textBoxJobName.Name = "textBoxJobName";
            this.textBoxJobName.Size = new System.Drawing.Size(704, 20);
            this.textBoxJobName.TabIndex = 13;
            // 
            // openFileDialogPreset
            // 
            this.openFileDialogPreset.FileName = "*.xml";
            this.openFileDialogPreset.Filter = "Preset files|*.xml|All files|*.*";
            // 
            // processorlabel
            // 
            this.processorlabel.Location = new System.Drawing.Point(22, 7);
            this.processorlabel.Name = "processorlabel";
            this.processorlabel.Size = new System.Drawing.Size(162, 22);
            this.processorlabel.TabIndex = 31;
            this.processorlabel.Text = "Select a processor :";
            // 
            // numericUpDownPriority
            // 
            this.numericUpDownPriority.Location = new System.Drawing.Point(16, 229);
            this.numericUpDownPriority.Name = "numericUpDownPriority";
            this.numericUpDownPriority.Size = new System.Drawing.Size(58, 20);
            this.numericUpDownPriority.TabIndex = 33;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 212);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 13);
            this.label5.TabIndex = 34;
            this.label5.Text = "Priority :";
            // 
            // radioButtonSingleTaskSingleJob
            // 
            this.radioButtonSingleTaskSingleJob.AutoSize = true;
            this.radioButtonSingleTaskSingleJob.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.radioButtonSingleTaskSingleJob.Location = new System.Drawing.Point(16, 92);
            this.radioButtonSingleTaskSingleJob.Name = "radioButtonSingleTaskSingleJob";
            this.radioButtonSingleTaskSingleJob.Size = new System.Drawing.Size(367, 56);
            this.radioButtonSingleTaskSingleJob.TabIndex = 36;
            this.radioButtonSingleTaskSingleJob.Text = resources.GetString("radioButtonSingleTaskSingleJob.Text");
            this.radioButtonSingleTaskSingleJob.UseVisualStyleBackColor = true;
            this.radioButtonSingleTaskSingleJob.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioButtonMultipleTasksMultipleJobs
            // 
            this.radioButtonMultipleTasksMultipleJobs.AutoSize = true;
            this.radioButtonMultipleTasksMultipleJobs.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.radioButtonMultipleTasksMultipleJobs.Checked = true;
            this.radioButtonMultipleTasksMultipleJobs.Location = new System.Drawing.Point(16, 17);
            this.radioButtonMultipleTasksMultipleJobs.Name = "radioButtonMultipleTasksMultipleJobs";
            this.radioButtonMultipleTasksMultipleJobs.Size = new System.Drawing.Size(377, 30);
            this.radioButtonMultipleTasksMultipleJobs.TabIndex = 35;
            this.radioButtonMultipleTasksMultipleJobs.TabStop = true;
            this.radioButtonMultipleTasksMultipleJobs.Text = "Multiple tasks, multiple jobs\r\n(a task created for each input asset, each task su" +
    "bmitted in a separate job)";
            this.radioButtonMultipleTasksMultipleJobs.UseVisualStyleBackColor = true;
            this.radioButtonMultipleTasksMultipleJobs.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // listViewProcessors
            // 
            this.listViewProcessors.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewProcessors.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ListViewVendor,
            this.ListViewName,
            this.ListViewVersion,
            this.ListViewDesc});
            this.listViewProcessors.FullRowSelect = true;
            this.listViewProcessors.HideSelection = false;
            this.listViewProcessors.Location = new System.Drawing.Point(21, 32);
            this.listViewProcessors.MultiSelect = false;
            this.listViewProcessors.Name = "listViewProcessors";
            this.listViewProcessors.Size = new System.Drawing.Size(742, 143);
            this.listViewProcessors.TabIndex = 38;
            this.listViewProcessors.UseCompatibleStateImageBehavior = false;
            this.listViewProcessors.View = System.Windows.Forms.View.Details;
            this.listViewProcessors.SelectedIndexChanged += new System.EventHandler(this.listViewProcessors_SelectedIndexChanged);
            // 
            // ListViewVendor
            // 
            this.ListViewVendor.Text = "Vendor";
            this.ListViewVendor.Width = 25;
            // 
            // ListViewName
            // 
            this.ListViewName.Text = "Name";
            // 
            // ListViewVersion
            // 
            this.ListViewVersion.Text = "Version";
            // 
            // ListViewDesc
            // 
            this.ListViewDesc.Text = "Description";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(21, 191);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(744, 309);
            this.tabControl1.TabIndex = 39;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.label6);
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Controls.Add(this.textboxoutputassetname);
            this.tabPage3.Controls.Add(this.listViewInputAssets);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(736, 283);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Asset(s)";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(504, 181);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(223, 13);
            this.label6.TabIndex = 41;
            this.label6.Text = "Note: with Zenium, blueprint must be asset #0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(125, 13);
            this.label4.TabIndex = 40;
            this.label4.Text = "Input asset(s) : (readonly)";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 197);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "Output asset name :";
            // 
            // textboxoutputassetname
            // 
            this.textboxoutputassetname.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textboxoutputassetname.Location = new System.Drawing.Point(17, 214);
            this.textboxoutputassetname.Name = "textboxoutputassetname";
            this.textboxoutputassetname.Size = new System.Drawing.Size(702, 20);
            this.textboxoutputassetname.TabIndex = 21;
            // 
            // listViewInputAssets
            // 
            this.listViewInputAssets.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewInputAssets.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listViewInputAssets.HideSelection = false;
            this.listViewInputAssets.Location = new System.Drawing.Point(17, 32);
            this.listViewInputAssets.MultiSelect = false;
            this.listViewInputAssets.Name = "listViewInputAssets";
            this.listViewInputAssets.Size = new System.Drawing.Size(702, 146);
            this.listViewInputAssets.TabIndex = 39;
            this.listViewInputAssets.UseCompatibleStateImageBehavior = false;
            this.listViewInputAssets.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Position";
            this.columnHeader1.Width = 25;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Name";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.pictureBoxJob);
            this.tabPage1.Controls.Add(this.labelsummaryjob);
            this.tabPage1.Controls.Add(this.radioButtonMultipleTasksSingleJob);
            this.tabPage1.Controls.Add(this.radioButtonSingleTaskSingleJob);
            this.tabPage1.Controls.Add(this.textBoxJobName);
            this.tabPage1.Controls.Add(this.radioButtonMultipleTasksMultipleJobs);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.numericUpDownPriority);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage1.Size = new System.Drawing.Size(736, 283);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Job(s)";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // pictureBoxJob
            // 
            this.pictureBoxJob.Image = global::AMSExplorer.Bitmaps.modetaskjob1;
            this.pictureBoxJob.Location = new System.Drawing.Point(416, 5);
            this.pictureBoxJob.Name = "pictureBoxJob";
            this.pictureBoxJob.Size = new System.Drawing.Size(315, 165);
            this.pictureBoxJob.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxJob.TabIndex = 39;
            this.pictureBoxJob.TabStop = false;
            // 
            // labelsummaryjob
            // 
            this.labelsummaryjob.Location = new System.Drawing.Point(14, 259);
            this.labelsummaryjob.Name = "labelsummaryjob";
            this.labelsummaryjob.Size = new System.Drawing.Size(365, 22);
            this.labelsummaryjob.TabIndex = 38;
            this.labelsummaryjob.Text = "You will submit n jobs with n tasks";
            // 
            // radioButtonMultipleTasksSingleJob
            // 
            this.radioButtonMultipleTasksSingleJob.AutoSize = true;
            this.radioButtonMultipleTasksSingleJob.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.radioButtonMultipleTasksSingleJob.Location = new System.Drawing.Point(16, 54);
            this.radioButtonMultipleTasksSingleJob.Name = "radioButtonMultipleTasksSingleJob";
            this.radioButtonMultipleTasksSingleJob.Size = new System.Drawing.Size(305, 30);
            this.radioButtonMultipleTasksSingleJob.TabIndex = 37;
            this.radioButtonMultipleTasksSingleJob.Text = "Multiple tasks, single job\r\n (a task created for each input asset, a single job s" +
    "ubmitted)";
            this.radioButtonMultipleTasksSingleJob.UseVisualStyleBackColor = true;
            this.radioButtonMultipleTasksSingleJob.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Controls.Add(this.textBoxConfiguration);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage2.Size = new System.Drawing.Size(736, 283);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Processor Configuration";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 13);
            this.label2.TabIndex = 38;
            this.label2.Text = "String or XML (editable) :";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 11);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(135, 23);
            this.button1.TabIndex = 25;
            this.button1.Text = "Load a custom XML file";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBoxConfiguration
            // 
            this.textBoxConfiguration.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxConfiguration.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxConfiguration.Location = new System.Drawing.Point(13, 61);
            this.textBoxConfiguration.Multiline = true;
            this.textBoxConfiguration.Name = "textBoxConfiguration";
            this.textBoxConfiguration.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxConfiguration.Size = new System.Drawing.Size(711, 211);
            this.textBoxConfiguration.TabIndex = 27;
            this.textBoxConfiguration.TextChanged += new System.EventHandler(this.textBoxConfiguration_TextChanged);
            // 
            // labelWarning
            // 
            this.labelWarning.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelWarning.AutoSize = true;
            this.labelWarning.ForeColor = System.Drawing.Color.Red;
            this.labelWarning.Location = new System.Drawing.Point(22, 527);
            this.labelWarning.Name = "labelWarning";
            this.labelWarning.Size = new System.Drawing.Size(47, 13);
            this.labelWarning.TabIndex = 40;
            this.labelWarning.Text = "Warning";
            // 
            // GenericProcessor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.labelWarning);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.listViewProcessors);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.processorlabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GenericProcessor";
            this.Text = "Generic processor call";
            this.Load += new System.EventHandler(this.GenericProcessor_Load);
            this.Shown += new System.EventHandler(this.GenericProcessor_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPriority)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxJob)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Button buttonOk;
        public System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.OpenFileDialog openFileDialogPreset;
        private System.Windows.Forms.Label processorlabel;
        private System.Windows.Forms.NumericUpDown numericUpDownPriority;
        public System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton radioButtonSingleTaskSingleJob;
        private System.Windows.Forms.RadioButton radioButtonMultipleTasksMultipleJobs;
        private System.Windows.Forms.ListView listViewProcessors;
        private System.Windows.Forms.ColumnHeader ListViewVendor;
        private System.Windows.Forms.ColumnHeader ListViewName;
        private System.Windows.Forms.ColumnHeader ListViewVersion;
        private System.Windows.Forms.ColumnHeader ListViewDesc;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        public System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListView listViewInputAssets;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        public System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabPage tabPage2;
        public System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBoxConfiguration;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.RadioButton radioButtonMultipleTasksSingleJob;
        public System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label labelsummaryjob;
        private System.Windows.Forms.Label labelWarning;
        private System.Windows.Forms.PictureBox pictureBoxJob;
        private System.Windows.Forms.TextBox textBoxJobName;
        private System.Windows.Forms.TextBox textboxoutputassetname;
    }
}