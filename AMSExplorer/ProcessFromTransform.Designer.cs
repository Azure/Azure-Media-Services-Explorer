namespace AMSExplorer
{
    partial class ProcessFromTransform
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProcessFromTransform));
            this.label = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.openFileDialogWorkflow = new System.Windows.Forms.OpenFileDialog();
            this.buttonDeleteTemplate = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labelTitle = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.buttonCreateNewTransform = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelURLFileNameWarning = new System.Windows.Forms.Label();
            this.textBoxURL = new System.Windows.Forms.TextBox();
            this.radioButtonHttpSource = new System.Windows.Forms.RadioButton();
            this.radioButtonSelectedAssets = new System.Windows.Forms.RadioButton();
            this.listViewTransforms = new AMSExplorer.ListViewTransforms();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.checkBoxSourceTrimmingEnd = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxSourceDurationTime = new System.Windows.Forms.TextBox();
            this.checkBoxSourceTrimmingStart = new System.Windows.Forms.CheckBox();
            this.timeControlEndTime = new AMSExplorer.TimeControl();
            this.timeControlStartTime = new AMSExplorer.TimeControl();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label
            // 
            resources.ApplyResources(this.label, "label");
            this.label.Name = "label";
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
            this.buttonOk.Image = global::AMSExplorer.Bitmaps.encoding;
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // openFileDialogWorkflow
            // 
            resources.ApplyResources(this.openFileDialogWorkflow, "openFileDialogWorkflow");
            // 
            // buttonDeleteTemplate
            // 
            resources.ApplyResources(this.buttonDeleteTemplate, "buttonDeleteTemplate");
            this.buttonDeleteTemplate.Name = "buttonDeleteTemplate";
            this.buttonDeleteTemplate.UseVisualStyleBackColor = true;
            this.buttonDeleteTemplate.Click += new System.EventHandler(this.buttonDeleteTemplate_Click);
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonOk);
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Name = "panel1";
            // 
            // panel2
            // 
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Name = "panel2";
            // 
            // labelTitle
            // 
            resources.ApplyResources(this.labelTitle, "labelTitle");
            this.labelTitle.ForeColor = System.Drawing.Color.DarkBlue;
            this.labelTitle.Name = "labelTitle";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // tabControl1
            // 
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.buttonCreateNewTransform);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.listViewTransforms);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.buttonDeleteTemplate);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // buttonCreateNewTransform
            // 
            resources.ApplyResources(this.buttonCreateNewTransform, "buttonCreateNewTransform");
            this.buttonCreateNewTransform.Name = "buttonCreateNewTransform";
            this.buttonCreateNewTransform.UseVisualStyleBackColor = true;
            this.buttonCreateNewTransform.Click += new System.EventHandler(this.ButtonCreateNewTransform_Click);
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.labelURLFileNameWarning);
            this.groupBox1.Controls.Add(this.textBoxURL);
            this.groupBox1.Controls.Add(this.radioButtonHttpSource);
            this.groupBox1.Controls.Add(this.radioButtonSelectedAssets);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // labelURLFileNameWarning
            // 
            resources.ApplyResources(this.labelURLFileNameWarning, "labelURLFileNameWarning");
            this.labelURLFileNameWarning.ForeColor = System.Drawing.Color.Red;
            this.labelURLFileNameWarning.Name = "labelURLFileNameWarning";
            // 
            // textBoxURL
            // 
            resources.ApplyResources(this.textBoxURL, "textBoxURL");
            this.textBoxURL.Name = "textBoxURL";
            this.textBoxURL.TextChanged += new System.EventHandler(this.textBoxURL_TextChanged);
            // 
            // radioButtonHttpSource
            // 
            resources.ApplyResources(this.radioButtonHttpSource, "radioButtonHttpSource");
            this.radioButtonHttpSource.Name = "radioButtonHttpSource";
            this.radioButtonHttpSource.UseVisualStyleBackColor = true;
            this.radioButtonHttpSource.CheckedChanged += new System.EventHandler(this.radioButtonHttpSource_CheckedChanged);
            // 
            // radioButtonSelectedAssets
            // 
            resources.ApplyResources(this.radioButtonSelectedAssets, "radioButtonSelectedAssets");
            this.radioButtonSelectedAssets.Checked = true;
            this.radioButtonSelectedAssets.Name = "radioButtonSelectedAssets";
            this.radioButtonSelectedAssets.TabStop = true;
            this.radioButtonSelectedAssets.UseVisualStyleBackColor = true;
            // 
            // listViewTransforms
            // 
            resources.ApplyResources(this.listViewTransforms, "listViewTransforms");
            this.listViewTransforms.FullRowSelect = true;
            this.listViewTransforms.HideSelection = false;
            this.listViewTransforms.MultiSelect = false;
            this.listViewTransforms.Name = "listViewTransforms";
            this.listViewTransforms.Tag = -1;
            this.listViewTransforms.UseCompatibleStateImageBehavior = false;
            this.listViewTransforms.View = System.Windows.Forms.View.Details;
            this.listViewTransforms.SelectedIndexChanged += new System.EventHandler(this.listbox_SelectedIndexChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.checkBoxSourceTrimmingEnd);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.textBoxSourceDurationTime);
            this.tabPage2.Controls.Add(this.checkBoxSourceTrimmingStart);
            this.tabPage2.Controls.Add(this.timeControlEndTime);
            this.tabPage2.Controls.Add(this.timeControlStartTime);
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label6.Name = "label6";
            // 
            // checkBoxSourceTrimmingEnd
            // 
            resources.ApplyResources(this.checkBoxSourceTrimmingEnd, "checkBoxSourceTrimmingEnd");
            this.checkBoxSourceTrimmingEnd.Name = "checkBoxSourceTrimmingEnd";
            this.checkBoxSourceTrimmingEnd.ThreeState = true;
            this.checkBoxSourceTrimmingEnd.UseVisualStyleBackColor = true;
            this.checkBoxSourceTrimmingEnd.CheckStateChanged += new System.EventHandler(this.checkBoxSourceTrimmingEnd_CheckStateChanged);
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // textBoxSourceDurationTime
            // 
            resources.ApplyResources(this.textBoxSourceDurationTime, "textBoxSourceDurationTime");
            this.textBoxSourceDurationTime.Name = "textBoxSourceDurationTime";
            this.textBoxSourceDurationTime.ReadOnly = true;
            // 
            // checkBoxSourceTrimmingStart
            // 
            resources.ApplyResources(this.checkBoxSourceTrimmingStart, "checkBoxSourceTrimmingStart");
            this.checkBoxSourceTrimmingStart.Name = "checkBoxSourceTrimmingStart";
            this.checkBoxSourceTrimmingStart.ThreeState = true;
            this.checkBoxSourceTrimmingStart.UseVisualStyleBackColor = true;
            this.checkBoxSourceTrimmingStart.CheckStateChanged += new System.EventHandler(this.checkBoxSourceTrimmingStart_CheckStateChanged);
            // 
            // timeControlEndTime
            // 
            resources.ApplyResources(this.timeControlEndTime, "timeControlEndTime");
            this.timeControlEndTime.BackColor = System.Drawing.SystemColors.Window;
            this.timeControlEndTime.DisplayTrackBar = false;
            this.timeControlEndTime.Label1 = "";
            this.timeControlEndTime.Label2 = "End time";
            this.timeControlEndTime.Max = System.TimeSpan.Parse("10675199.02:48:05.4775807");
            this.timeControlEndTime.Min = System.TimeSpan.Parse("00:00:00");
            this.timeControlEndTime.Name = "timeControlEndTime";
            this.timeControlEndTime.ScaledFirstTimestampOffset = ((ulong)(0ul));
            this.timeControlEndTime.TimeScale = null;
            this.timeControlEndTime.TotalDuration = System.TimeSpan.Parse("1.00:00:00");
            this.timeControlEndTime.ValueChanged += new System.EventHandler(this.timeControlEndTime_ValueChanged);
            // 
            // timeControlStartTime
            // 
            resources.ApplyResources(this.timeControlStartTime, "timeControlStartTime");
            this.timeControlStartTime.BackColor = System.Drawing.SystemColors.Window;
            this.timeControlStartTime.DisplayTrackBar = false;
            this.timeControlStartTime.Label1 = "";
            this.timeControlStartTime.Label2 = "Start time";
            this.timeControlStartTime.Max = System.TimeSpan.Parse("10675199.02:48:05.4775807");
            this.timeControlStartTime.Min = System.TimeSpan.Parse("00:00:00");
            this.timeControlStartTime.Name = "timeControlStartTime";
            this.timeControlStartTime.ScaledFirstTimestampOffset = ((ulong)(0ul));
            this.timeControlStartTime.TimeScale = null;
            this.timeControlStartTime.TotalDuration = System.TimeSpan.Parse("1.00:00:00");
            this.timeControlStartTime.ValueChanged += new System.EventHandler(this.timeControlStartTime_ValueChanged);
            // 
            // ProcessFromTransform
            // 
            this.AcceptButton = this.buttonOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label);
            this.Name = "ProcessFromTransform";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.ProcessFromJobTemplate_Load);
            this.DpiChanged += new System.Windows.Forms.DpiChangedEventHandler(this.ProcessFromTransform_DpiChanged);
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label label;
        public System.Windows.Forms.Button buttonCancel;
        public System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.OpenFileDialog openFileDialogWorkflow;
        private System.Windows.Forms.Button buttonDeleteTemplate;
        private ListViewTransforms listViewTransforms;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label labelTitle;
        public System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private TimeControl timeControlEndTime;
        private System.Windows.Forms.CheckBox checkBoxSourceTrimmingEnd;
        public System.Windows.Forms.Label label7;
        private TimeControl timeControlStartTime;
        private System.Windows.Forms.TextBox textBoxSourceDurationTime;
        private System.Windows.Forms.CheckBox checkBoxSourceTrimmingStart;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButtonHttpSource;
        private System.Windows.Forms.RadioButton radioButtonSelectedAssets;
        private System.Windows.Forms.TextBox textBoxURL;
        private System.Windows.Forms.Label labelURLFileNameWarning;
        private System.Windows.Forms.Button buttonCreateNewTransform;
    }
}