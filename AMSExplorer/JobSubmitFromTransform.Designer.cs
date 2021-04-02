namespace AMSExplorer
{
    partial class JobSubmitFromTransform
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JobSubmitFromTransform));
            this.label = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.openFileDialogWorkflow = new System.Windows.Forms.OpenFileDialog();
            this.buttonDeleteTemplate = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
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
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.buttonDown = new System.Windows.Forms.Button();
            this.dataGridViewEDL = new System.Windows.Forms.DataGridView();
            this.buttonUp = new System.Windows.Forms.Button();
            this.buttonDelEntry = new System.Windows.Forms.Button();
            this.buttonAddEDLEntry = new System.Windows.Forms.Button();
            this.labelInfoSeveralAssetStitching = new System.Windows.Forms.Label();
            this.labelInputAsset = new System.Windows.Forms.Label();
            this.comboBoxSourceAsset = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.checkBoxSourceTrimmingEnd = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxSourceDurationTime = new System.Windows.Forms.TextBox();
            this.checkBoxSourceTrimmingStart = new System.Windows.Forms.CheckBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.textBoxNewAssetNameSyntax = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panelSelectAsset = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonSearchExactAssetName = new System.Windows.Forms.Button();
            this.textBoxExactAssetName = new System.Windows.Forms.TextBox();
            this.radioButtonExistingAsset = new System.Windows.Forms.RadioButton();
            this.radioButtonNewAsset = new System.Windows.Forms.RadioButton();
            this.panelEditingList = new System.Windows.Forms.Panel();
            this.listViewTransforms = new AMSExplorer.ListViewTransforms();
            this.timeControlEndTime = new AMSExplorer.TimeControl();
            this.timeControlStartTime = new AMSExplorer.TimeControl();
            this.listViewAssets1 = new AMSExplorer.ListViewAssets();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEDL)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.panelSelectAsset.SuspendLayout();
            this.panelEditingList.SuspendLayout();
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
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
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
            this.buttonDeleteTemplate.Click += new System.EventHandler(this.ButtonDeleteTemplate_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonOk);
            this.panel1.Controls.Add(this.buttonCancel);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
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
            this.tabControl1.Controls.Add(this.tabPage3);
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
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panelEditingList);
            this.tabPage2.Controls.Add(this.labelInfoSeveralAssetStitching);
            this.tabPage2.Controls.Add(this.labelInputAsset);
            this.tabPage2.Controls.Add(this.comboBoxSourceAsset);
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
            // buttonDown
            // 
            resources.ApplyResources(this.buttonDown, "buttonDown");
            this.buttonDown.Name = "buttonDown";
            this.buttonDown.UseVisualStyleBackColor = true;
            this.buttonDown.Click += new System.EventHandler(this.buttonDown_Click);
            // 
            // dataGridViewEDL
            // 
            this.dataGridViewEDL.AllowUserToAddRows = false;
            this.dataGridViewEDL.AllowUserToDeleteRows = false;
            resources.ApplyResources(this.dataGridViewEDL, "dataGridViewEDL");
            this.dataGridViewEDL.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewEDL.Name = "dataGridViewEDL";
            this.dataGridViewEDL.ReadOnly = true;
            this.dataGridViewEDL.RowHeadersVisible = false;
            this.dataGridViewEDL.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewEDL.SelectionChanged += new System.EventHandler(this.dataGridViewEDL_SelectionChanged);
            // 
            // buttonUp
            // 
            resources.ApplyResources(this.buttonUp, "buttonUp");
            this.buttonUp.Name = "buttonUp";
            this.buttonUp.UseVisualStyleBackColor = true;
            this.buttonUp.Click += new System.EventHandler(this.buttonUp_Click);
            // 
            // buttonDelEntry
            // 
            resources.ApplyResources(this.buttonDelEntry, "buttonDelEntry");
            this.buttonDelEntry.Image = global::AMSExplorer.Bitmaps.delete;
            this.buttonDelEntry.Name = "buttonDelEntry";
            this.buttonDelEntry.UseVisualStyleBackColor = true;
            this.buttonDelEntry.Click += new System.EventHandler(this.buttonDelEntry_Click);
            // 
            // buttonAddEDLEntry
            // 
            resources.ApplyResources(this.buttonAddEDLEntry, "buttonAddEDLEntry");
            this.buttonAddEDLEntry.Name = "buttonAddEDLEntry";
            this.buttonAddEDLEntry.UseVisualStyleBackColor = true;
            this.buttonAddEDLEntry.Click += new System.EventHandler(this.buttonAddEDLEntry_Click);
            // 
            // labelInfoSeveralAssetStitching
            // 
            resources.ApplyResources(this.labelInfoSeveralAssetStitching, "labelInfoSeveralAssetStitching");
            this.labelInfoSeveralAssetStitching.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.labelInfoSeveralAssetStitching.Name = "labelInfoSeveralAssetStitching";
            // 
            // labelInputAsset
            // 
            resources.ApplyResources(this.labelInputAsset, "labelInputAsset");
            this.labelInputAsset.Name = "labelInputAsset";
            // 
            // comboBoxSourceAsset
            // 
            resources.ApplyResources(this.comboBoxSourceAsset, "comboBoxSourceAsset");
            this.comboBoxSourceAsset.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSourceAsset.FormattingEnabled = true;
            this.comboBoxSourceAsset.Name = "comboBoxSourceAsset";
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
            this.checkBoxSourceTrimmingEnd.UseVisualStyleBackColor = true;
            this.checkBoxSourceTrimmingEnd.CheckedChanged += new System.EventHandler(this.checkBoxSourceTrimmingEnd_CheckedChanged);
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
            this.checkBoxSourceTrimmingStart.UseVisualStyleBackColor = true;
            this.checkBoxSourceTrimmingStart.CheckStateChanged += new System.EventHandler(this.checkBoxSourceTrimmingStart_CheckStateChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.textBoxNewAssetNameSyntax);
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Controls.Add(this.panelSelectAsset);
            this.tabPage3.Controls.Add(this.radioButtonExistingAsset);
            this.tabPage3.Controls.Add(this.radioButtonNewAsset);
            resources.ApplyResources(this.tabPage3, "tabPage3");
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // textBoxNewAssetNameSyntax
            // 
            resources.ApplyResources(this.textBoxNewAssetNameSyntax, "textBoxNewAssetNameSyntax");
            this.textBoxNewAssetNameSyntax.Name = "textBoxNewAssetNameSyntax";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label4.Name = "label4";
            // 
            // panelSelectAsset
            // 
            resources.ApplyResources(this.panelSelectAsset, "panelSelectAsset");
            this.panelSelectAsset.Controls.Add(this.label2);
            this.panelSelectAsset.Controls.Add(this.listViewAssets1);
            this.panelSelectAsset.Controls.Add(this.buttonSearchExactAssetName);
            this.panelSelectAsset.Controls.Add(this.textBoxExactAssetName);
            this.panelSelectAsset.Name = "panelSelectAsset";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // buttonSearchExactAssetName
            // 
            resources.ApplyResources(this.buttonSearchExactAssetName, "buttonSearchExactAssetName");
            this.buttonSearchExactAssetName.Name = "buttonSearchExactAssetName";
            this.buttonSearchExactAssetName.UseVisualStyleBackColor = true;
            this.buttonSearchExactAssetName.Click += new System.EventHandler(this.buttonSearchExactAssetName_Click);
            // 
            // textBoxExactAssetName
            // 
            resources.ApplyResources(this.textBoxExactAssetName, "textBoxExactAssetName");
            this.textBoxExactAssetName.Name = "textBoxExactAssetName";
            // 
            // radioButtonExistingAsset
            // 
            resources.ApplyResources(this.radioButtonExistingAsset, "radioButtonExistingAsset");
            this.radioButtonExistingAsset.Name = "radioButtonExistingAsset";
            this.radioButtonExistingAsset.UseVisualStyleBackColor = true;
            this.radioButtonExistingAsset.CheckedChanged += new System.EventHandler(this.radioButtonExistingAsset_CheckedChanged);
            // 
            // radioButtonNewAsset
            // 
            resources.ApplyResources(this.radioButtonNewAsset, "radioButtonNewAsset");
            this.radioButtonNewAsset.Checked = true;
            this.radioButtonNewAsset.Name = "radioButtonNewAsset";
            this.radioButtonNewAsset.TabStop = true;
            this.radioButtonNewAsset.UseVisualStyleBackColor = true;
            // 
            // panelEditingList
            // 
            resources.ApplyResources(this.panelEditingList, "panelEditingList");
            this.panelEditingList.Controls.Add(this.buttonAddEDLEntry);
            this.panelEditingList.Controls.Add(this.buttonDelEntry);
            this.panelEditingList.Controls.Add(this.buttonDown);
            this.panelEditingList.Controls.Add(this.dataGridViewEDL);
            this.panelEditingList.Controls.Add(this.buttonUp);
            this.panelEditingList.Name = "panelEditingList";
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
            this.listViewTransforms.SelectedIndexChanged += new System.EventHandler(this.Listbox_SelectedIndexChanged);
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
            this.timeControlStartTime.ValueChanged += new System.EventHandler(this.TimeControlStartTime_ValueChanged);
            // 
            // listViewAssets1
            // 
            resources.ApplyResources(this.listViewAssets1, "listViewAssets1");
            this.listViewAssets1.FullRowSelect = true;
            this.listViewAssets1.HideSelection = false;
            this.listViewAssets1.MultiSelect = false;
            this.listViewAssets1.Name = "listViewAssets1";
            this.listViewAssets1.Tag = -1;
            this.listViewAssets1.UseCompatibleStateImageBehavior = false;
            this.listViewAssets1.View = System.Windows.Forms.View.Details;
            // 
            // JobSubmitFromTransform
            // 
            this.AcceptButton = this.buttonOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label);
            this.Name = "JobSubmitFromTransform";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.JobSubmitFromTransform_Load);
            this.DpiChanged += new System.Windows.Forms.DpiChangedEventHandler(this.JobSubmitFromTransform_DpiChanged);
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEDL)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.panelSelectAsset.ResumeLayout(false);
            this.panelSelectAsset.PerformLayout();
            this.panelEditingList.ResumeLayout(false);
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
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.RadioButton radioButtonExistingAsset;
        private System.Windows.Forms.RadioButton radioButtonNewAsset;
        private System.Windows.Forms.Panel panelSelectAsset;
        public System.Windows.Forms.Label label2;
        private ListViewAssets listViewAssets1;
        private System.Windows.Forms.Button buttonSearchExactAssetName;
        private System.Windows.Forms.TextBox textBoxExactAssetName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxNewAssetNameSyntax;
        private System.Windows.Forms.Label labelInputAsset;
        private System.Windows.Forms.ComboBox comboBoxSourceAsset;
        public System.Windows.Forms.Label labelInfoSeveralAssetStitching;
        private System.Windows.Forms.Button buttonDown;
        private System.Windows.Forms.DataGridView dataGridViewEDL;
        private System.Windows.Forms.Button buttonUp;
        private System.Windows.Forms.Button buttonDelEntry;
        private System.Windows.Forms.Button buttonAddEDLEntry;
        private System.Windows.Forms.Panel panelEditingList;
    }
}