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
            label = new System.Windows.Forms.Label();
            buttonCancel = new System.Windows.Forms.Button();
            buttonOk = new System.Windows.Forms.Button();
            openFileDialogWorkflow = new System.Windows.Forms.OpenFileDialog();
            buttonDeleteTemplate = new System.Windows.Forms.Button();
            panel1 = new System.Windows.Forms.Panel();
            labelTitle = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            tabControlCreateJob = new System.Windows.Forms.TabControl();
            tabPageSource = new System.Windows.Forms.TabPage();
            buttonCreateNewTransform = new System.Windows.Forms.Button();
            groupBox1 = new System.Windows.Forms.GroupBox();
            labelURLFileNameWarning = new System.Windows.Forms.Label();
            textBoxURL = new System.Windows.Forms.TextBox();
            radioButtonHttpSource = new System.Windows.Forms.RadioButton();
            radioButtonSelectedAssets = new System.Windows.Forms.RadioButton();
            listViewTransforms = new ListViewTransforms();
            tabPageTrimming = new System.Windows.Forms.TabPage();
            buttonImportEDL = new System.Windows.Forms.Button();
            buttonExportEDL = new System.Windows.Forms.Button();
            labelAssetDescription = new System.Windows.Forms.Label();
            textBoxAssetDescription = new System.Windows.Forms.TextBox();
            dataGridViewEDL = new System.Windows.Forms.DataGridView();
            buttonDelEntry = new System.Windows.Forms.Button();
            buttonAddEDLEntry = new System.Windows.Forms.Button();
            buttonDown = new System.Windows.Forms.Button();
            buttonUp = new System.Windows.Forms.Button();
            labelInputAsset = new System.Windows.Forms.Label();
            comboBoxSourceAsset = new System.Windows.Forms.ComboBox();
            label6 = new System.Windows.Forms.Label();
            checkBoxSourceTrimmingEnd = new System.Windows.Forms.CheckBox();
            label7 = new System.Windows.Forms.Label();
            textBoxSourceDurationTime = new System.Windows.Forms.TextBox();
            checkBoxSourceTrimmingStart = new System.Windows.Forms.CheckBox();
            timeControlEndTime = new TimeControl();
            timeControlStartTime = new TimeControl();
            tabPageOutputAsset = new System.Windows.Forms.TabPage();
            listViewAssets1 = new ListViewAssets();
            labelSelectAsset = new System.Windows.Forms.Label();
            buttonSearchExactAssetName = new System.Windows.Forms.Button();
            textBoxNewAssetNameSyntax = new System.Windows.Forms.TextBox();
            textBoxExactAssetName = new System.Windows.Forms.TextBox();
            label4 = new System.Windows.Forms.Label();
            radioButtonExistingAsset = new System.Windows.Forms.RadioButton();
            radioButtonNewAsset = new System.Windows.Forms.RadioButton();
            saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            panel1.SuspendLayout();
            tabControlCreateJob.SuspendLayout();
            tabPageSource.SuspendLayout();
            groupBox1.SuspendLayout();
            tabPageTrimming.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewEDL).BeginInit();
            tabPageOutputAsset.SuspendLayout();
            SuspendLayout();
            // 
            // label
            // 
            resources.ApplyResources(label, "label");
            label.Name = "label";
            // 
            // buttonCancel
            // 
            resources.ApplyResources(buttonCancel, "buttonCancel");
            buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            buttonCancel.Name = "buttonCancel";
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += ButtonCancel_Click;
            // 
            // buttonOk
            // 
            resources.ApplyResources(buttonOk, "buttonOk");
            buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            buttonOk.Name = "buttonOk";
            buttonOk.UseVisualStyleBackColor = true;
            // 
            // openFileDialogWorkflow
            // 
            resources.ApplyResources(openFileDialogWorkflow, "openFileDialogWorkflow");
            // 
            // buttonDeleteTemplate
            // 
            resources.ApplyResources(buttonDeleteTemplate, "buttonDeleteTemplate");
            buttonDeleteTemplate.Name = "buttonDeleteTemplate";
            buttonDeleteTemplate.UseVisualStyleBackColor = true;
            buttonDeleteTemplate.Click += ButtonDeleteTemplate_Click;
            // 
            // panel1
            // 
            panel1.BackColor = System.Drawing.SystemColors.Control;
            panel1.Controls.Add(buttonOk);
            panel1.Controls.Add(buttonCancel);
            resources.ApplyResources(panel1, "panel1");
            panel1.Name = "panel1";
            // 
            // labelTitle
            // 
            resources.ApplyResources(labelTitle, "labelTitle");
            labelTitle.ForeColor = System.Drawing.Color.DarkBlue;
            labelTitle.Name = "labelTitle";
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // tabControlCreateJob
            // 
            resources.ApplyResources(tabControlCreateJob, "tabControlCreateJob");
            tabControlCreateJob.Controls.Add(tabPageSource);
            tabControlCreateJob.Controls.Add(tabPageTrimming);
            tabControlCreateJob.Controls.Add(tabPageOutputAsset);
            tabControlCreateJob.Name = "tabControlCreateJob";
            tabControlCreateJob.SelectedIndex = 0;
            // 
            // tabPageSource
            // 
            tabPageSource.Controls.Add(buttonCreateNewTransform);
            tabPageSource.Controls.Add(groupBox1);
            tabPageSource.Controls.Add(listViewTransforms);
            tabPageSource.Controls.Add(label1);
            tabPageSource.Controls.Add(buttonDeleteTemplate);
            resources.ApplyResources(tabPageSource, "tabPageSource");
            tabPageSource.Name = "tabPageSource";
            tabPageSource.UseVisualStyleBackColor = true;
            // 
            // buttonCreateNewTransform
            // 
            resources.ApplyResources(buttonCreateNewTransform, "buttonCreateNewTransform");
            buttonCreateNewTransform.Name = "buttonCreateNewTransform";
            buttonCreateNewTransform.UseVisualStyleBackColor = true;
            buttonCreateNewTransform.Click += ButtonCreateNewTransform_Click;
            // 
            // groupBox1
            // 
            resources.ApplyResources(groupBox1, "groupBox1");
            groupBox1.Controls.Add(labelURLFileNameWarning);
            groupBox1.Controls.Add(textBoxURL);
            groupBox1.Controls.Add(radioButtonHttpSource);
            groupBox1.Controls.Add(radioButtonSelectedAssets);
            groupBox1.Name = "groupBox1";
            groupBox1.TabStop = false;
            // 
            // labelURLFileNameWarning
            // 
            resources.ApplyResources(labelURLFileNameWarning, "labelURLFileNameWarning");
            labelURLFileNameWarning.ForeColor = System.Drawing.Color.Red;
            labelURLFileNameWarning.Name = "labelURLFileNameWarning";
            // 
            // textBoxURL
            // 
            resources.ApplyResources(textBoxURL, "textBoxURL");
            textBoxURL.Name = "textBoxURL";
            textBoxURL.TextChanged += textBoxURL_TextChanged;
            // 
            // radioButtonHttpSource
            // 
            resources.ApplyResources(radioButtonHttpSource, "radioButtonHttpSource");
            radioButtonHttpSource.Name = "radioButtonHttpSource";
            radioButtonHttpSource.UseVisualStyleBackColor = true;
            radioButtonHttpSource.CheckedChanged += radioButtonHttpSource_CheckedChanged;
            // 
            // radioButtonSelectedAssets
            // 
            resources.ApplyResources(radioButtonSelectedAssets, "radioButtonSelectedAssets");
            radioButtonSelectedAssets.Checked = true;
            radioButtonSelectedAssets.Name = "radioButtonSelectedAssets";
            radioButtonSelectedAssets.TabStop = true;
            radioButtonSelectedAssets.UseVisualStyleBackColor = true;
            // 
            // listViewTransforms
            // 
            resources.ApplyResources(listViewTransforms, "listViewTransforms");
            listViewTransforms.FullRowSelect = true;
            listViewTransforms.MultiSelect = false;
            listViewTransforms.Name = "listViewTransforms";
            listViewTransforms.Tag = -1;
            listViewTransforms.UseCompatibleStateImageBehavior = false;
            listViewTransforms.View = System.Windows.Forms.View.Details;
            listViewTransforms.SelectedIndexChanged += Listbox_SelectedIndexChanged;
            // 
            // tabPageTrimming
            // 
            tabPageTrimming.Controls.Add(buttonImportEDL);
            tabPageTrimming.Controls.Add(buttonExportEDL);
            tabPageTrimming.Controls.Add(labelAssetDescription);
            tabPageTrimming.Controls.Add(textBoxAssetDescription);
            tabPageTrimming.Controls.Add(dataGridViewEDL);
            tabPageTrimming.Controls.Add(buttonDelEntry);
            tabPageTrimming.Controls.Add(buttonAddEDLEntry);
            tabPageTrimming.Controls.Add(buttonDown);
            tabPageTrimming.Controls.Add(buttonUp);
            tabPageTrimming.Controls.Add(labelInputAsset);
            tabPageTrimming.Controls.Add(comboBoxSourceAsset);
            tabPageTrimming.Controls.Add(label6);
            tabPageTrimming.Controls.Add(checkBoxSourceTrimmingEnd);
            tabPageTrimming.Controls.Add(label7);
            tabPageTrimming.Controls.Add(textBoxSourceDurationTime);
            tabPageTrimming.Controls.Add(checkBoxSourceTrimmingStart);
            tabPageTrimming.Controls.Add(timeControlEndTime);
            tabPageTrimming.Controls.Add(timeControlStartTime);
            resources.ApplyResources(tabPageTrimming, "tabPageTrimming");
            tabPageTrimming.Name = "tabPageTrimming";
            tabPageTrimming.UseVisualStyleBackColor = true;
            // 
            // buttonImportEDL
            // 
            resources.ApplyResources(buttonImportEDL, "buttonImportEDL");
            buttonImportEDL.Name = "buttonImportEDL";
            buttonImportEDL.UseVisualStyleBackColor = true;
            buttonImportEDL.Click += buttonImportEDL_Click;
            // 
            // buttonExportEDL
            // 
            resources.ApplyResources(buttonExportEDL, "buttonExportEDL");
            buttonExportEDL.Name = "buttonExportEDL";
            buttonExportEDL.UseVisualStyleBackColor = true;
            buttonExportEDL.Click += buttonExportEDL_Click;
            // 
            // labelAssetDescription
            // 
            resources.ApplyResources(labelAssetDescription, "labelAssetDescription");
            labelAssetDescription.Name = "labelAssetDescription";
            // 
            // textBoxAssetDescription
            // 
            resources.ApplyResources(textBoxAssetDescription, "textBoxAssetDescription");
            textBoxAssetDescription.Name = "textBoxAssetDescription";
            textBoxAssetDescription.ReadOnly = true;
            // 
            // dataGridViewEDL
            // 
            dataGridViewEDL.AllowUserToAddRows = false;
            dataGridViewEDL.AllowUserToDeleteRows = false;
            resources.ApplyResources(dataGridViewEDL, "dataGridViewEDL");
            dataGridViewEDL.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewEDL.Name = "dataGridViewEDL";
            dataGridViewEDL.ReadOnly = true;
            dataGridViewEDL.RowHeadersVisible = false;
            dataGridViewEDL.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dataGridViewEDL.SelectionChanged += dataGridViewEDL_SelectionChanged;
            // 
            // buttonDelEntry
            // 
            resources.ApplyResources(buttonDelEntry, "buttonDelEntry");
            buttonDelEntry.Name = "buttonDelEntry";
            buttonDelEntry.UseVisualStyleBackColor = true;
            buttonDelEntry.Click += buttonDelEntry_Click;
            // 
            // buttonAddEDLEntry
            // 
            resources.ApplyResources(buttonAddEDLEntry, "buttonAddEDLEntry");
            buttonAddEDLEntry.Name = "buttonAddEDLEntry";
            buttonAddEDLEntry.UseVisualStyleBackColor = true;
            buttonAddEDLEntry.Click += buttonAddEDLEntry_Click;
            // 
            // buttonDown
            // 
            resources.ApplyResources(buttonDown, "buttonDown");
            buttonDown.Name = "buttonDown";
            buttonDown.UseVisualStyleBackColor = true;
            buttonDown.Click += buttonDown_Click;
            // 
            // buttonUp
            // 
            resources.ApplyResources(buttonUp, "buttonUp");
            buttonUp.Name = "buttonUp";
            buttonUp.UseVisualStyleBackColor = true;
            buttonUp.Click += buttonUp_Click;
            // 
            // labelInputAsset
            // 
            resources.ApplyResources(labelInputAsset, "labelInputAsset");
            labelInputAsset.Name = "labelInputAsset";
            // 
            // comboBoxSourceAsset
            // 
            resources.ApplyResources(comboBoxSourceAsset, "comboBoxSourceAsset");
            comboBoxSourceAsset.FormattingEnabled = true;
            comboBoxSourceAsset.Name = "comboBoxSourceAsset";
            comboBoxSourceAsset.SelectedIndexChanged += comboBoxSourceAsset_SelectedIndexChanged;
            comboBoxSourceAsset.TextChanged += comboBoxSourceAsset_TextChanged;
            // 
            // label6
            // 
            resources.ApplyResources(label6, "label6");
            label6.ForeColor = System.Drawing.SystemColors.WindowFrame;
            label6.Name = "label6";
            // 
            // checkBoxSourceTrimmingEnd
            // 
            resources.ApplyResources(checkBoxSourceTrimmingEnd, "checkBoxSourceTrimmingEnd");
            checkBoxSourceTrimmingEnd.Name = "checkBoxSourceTrimmingEnd";
            checkBoxSourceTrimmingEnd.UseVisualStyleBackColor = true;
            checkBoxSourceTrimmingEnd.CheckedChanged += checkBoxSourceTrimmingEnd_CheckedChanged;
            checkBoxSourceTrimmingEnd.CheckStateChanged += checkBoxSourceTrimmingEnd_CheckStateChanged;
            // 
            // label7
            // 
            resources.ApplyResources(label7, "label7");
            label7.Name = "label7";
            // 
            // textBoxSourceDurationTime
            // 
            resources.ApplyResources(textBoxSourceDurationTime, "textBoxSourceDurationTime");
            textBoxSourceDurationTime.Name = "textBoxSourceDurationTime";
            textBoxSourceDurationTime.ReadOnly = true;
            // 
            // checkBoxSourceTrimmingStart
            // 
            resources.ApplyResources(checkBoxSourceTrimmingStart, "checkBoxSourceTrimmingStart");
            checkBoxSourceTrimmingStart.Name = "checkBoxSourceTrimmingStart";
            checkBoxSourceTrimmingStart.UseVisualStyleBackColor = true;
            checkBoxSourceTrimmingStart.CheckStateChanged += checkBoxSourceTrimmingStart_CheckStateChanged;
            // 
            // timeControlEndTime
            // 
            resources.ApplyResources(timeControlEndTime, "timeControlEndTime");
            timeControlEndTime.BackColor = System.Drawing.SystemColors.Window;
            timeControlEndTime.DisplayTrackBar = false;
            timeControlEndTime.Label1 = "";
            timeControlEndTime.Label2 = "End time";
            timeControlEndTime.Max = System.TimeSpan.Parse("10675199.02:48:05.4775807");
            timeControlEndTime.Min = System.TimeSpan.Parse("00:00:00");
            timeControlEndTime.Name = "timeControlEndTime";
            timeControlEndTime.ScaledFirstTimestampOffset = 0UL;
            timeControlEndTime.TimeScale = null;
            timeControlEndTime.TotalDuration = System.TimeSpan.Parse("1.00:00:00");
            timeControlEndTime.ValueChanged += timeControlEndTime_ValueChanged;
            // 
            // timeControlStartTime
            // 
            resources.ApplyResources(timeControlStartTime, "timeControlStartTime");
            timeControlStartTime.BackColor = System.Drawing.SystemColors.Window;
            timeControlStartTime.DisplayTrackBar = false;
            timeControlStartTime.Label1 = "";
            timeControlStartTime.Label2 = "Start time";
            timeControlStartTime.Max = System.TimeSpan.Parse("10675199.02:48:05.4775807");
            timeControlStartTime.Min = System.TimeSpan.Parse("00:00:00");
            timeControlStartTime.Name = "timeControlStartTime";
            timeControlStartTime.ScaledFirstTimestampOffset = 0UL;
            timeControlStartTime.TimeScale = null;
            timeControlStartTime.TotalDuration = System.TimeSpan.Parse("1.00:00:00");
            timeControlStartTime.ValueChanged += TimeControlStartTime_ValueChanged;
            // 
            // tabPageOutputAsset
            // 
            tabPageOutputAsset.Controls.Add(listViewAssets1);
            tabPageOutputAsset.Controls.Add(labelSelectAsset);
            tabPageOutputAsset.Controls.Add(buttonSearchExactAssetName);
            tabPageOutputAsset.Controls.Add(textBoxNewAssetNameSyntax);
            tabPageOutputAsset.Controls.Add(textBoxExactAssetName);
            tabPageOutputAsset.Controls.Add(label4);
            tabPageOutputAsset.Controls.Add(radioButtonExistingAsset);
            tabPageOutputAsset.Controls.Add(radioButtonNewAsset);
            resources.ApplyResources(tabPageOutputAsset, "tabPageOutputAsset");
            tabPageOutputAsset.Name = "tabPageOutputAsset";
            tabPageOutputAsset.UseVisualStyleBackColor = true;
            // 
            // listViewAssets1
            // 
            resources.ApplyResources(listViewAssets1, "listViewAssets1");
            listViewAssets1.FullRowSelect = true;
            listViewAssets1.MultiSelect = false;
            listViewAssets1.Name = "listViewAssets1";
            listViewAssets1.Tag = -1;
            listViewAssets1.UseCompatibleStateImageBehavior = false;
            listViewAssets1.View = System.Windows.Forms.View.Details;
            // 
            // labelSelectAsset
            // 
            resources.ApplyResources(labelSelectAsset, "labelSelectAsset");
            labelSelectAsset.Name = "labelSelectAsset";
            // 
            // buttonSearchExactAssetName
            // 
            resources.ApplyResources(buttonSearchExactAssetName, "buttonSearchExactAssetName");
            buttonSearchExactAssetName.Name = "buttonSearchExactAssetName";
            buttonSearchExactAssetName.UseVisualStyleBackColor = true;
            buttonSearchExactAssetName.Click += buttonSearchExactAssetName_Click;
            // 
            // textBoxNewAssetNameSyntax
            // 
            resources.ApplyResources(textBoxNewAssetNameSyntax, "textBoxNewAssetNameSyntax");
            textBoxNewAssetNameSyntax.Name = "textBoxNewAssetNameSyntax";
            // 
            // textBoxExactAssetName
            // 
            resources.ApplyResources(textBoxExactAssetName, "textBoxExactAssetName");
            textBoxExactAssetName.Name = "textBoxExactAssetName";
            // 
            // label4
            // 
            resources.ApplyResources(label4, "label4");
            label4.ForeColor = System.Drawing.SystemColors.WindowFrame;
            label4.Name = "label4";
            // 
            // radioButtonExistingAsset
            // 
            resources.ApplyResources(radioButtonExistingAsset, "radioButtonExistingAsset");
            radioButtonExistingAsset.Name = "radioButtonExistingAsset";
            radioButtonExistingAsset.UseVisualStyleBackColor = true;
            radioButtonExistingAsset.CheckedChanged += radioButtonExistingAsset_CheckedChanged;
            // 
            // radioButtonNewAsset
            // 
            resources.ApplyResources(radioButtonNewAsset, "radioButtonNewAsset");
            radioButtonNewAsset.Checked = true;
            radioButtonNewAsset.Name = "radioButtonNewAsset";
            radioButtonNewAsset.TabStop = true;
            radioButtonNewAsset.UseVisualStyleBackColor = true;
            // 
            // saveFileDialog1
            // 
            resources.ApplyResources(saveFileDialog1, "saveFileDialog1");
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            resources.ApplyResources(openFileDialog1, "openFileDialog1");
            // 
            // JobSubmitFromTransform
            // 
            AcceptButton = buttonOk;
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            BackColor = System.Drawing.SystemColors.Window;
            CancelButton = buttonCancel;
            Controls.Add(tabControlCreateJob);
            Controls.Add(labelTitle);
            Controls.Add(panel1);
            Controls.Add(label);
            Name = "JobSubmitFromTransform";
            SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            Load += JobSubmitFromTransform_Load;
            Shown += JobSubmitFromTransform_Shown;
            DpiChanged += JobSubmitFromTransform_DpiChanged;
            panel1.ResumeLayout(false);
            tabControlCreateJob.ResumeLayout(false);
            tabPageSource.ResumeLayout(false);
            tabPageSource.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            tabPageTrimming.ResumeLayout(false);
            tabPageTrimming.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewEDL).EndInit();
            tabPageOutputAsset.ResumeLayout(false);
            tabPageOutputAsset.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
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
        private System.Windows.Forms.TabControl tabControlCreateJob;
        private System.Windows.Forms.TabPage tabPageSource;
        private System.Windows.Forms.TabPage tabPageTrimming;
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
        private System.Windows.Forms.TabPage tabPageOutputAsset;
        private System.Windows.Forms.RadioButton radioButtonExistingAsset;
        private System.Windows.Forms.RadioButton radioButtonNewAsset;
        public System.Windows.Forms.Label labelSelectAsset;
        private ListViewAssets listViewAssets1;
        private System.Windows.Forms.Button buttonSearchExactAssetName;
        private System.Windows.Forms.TextBox textBoxExactAssetName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxNewAssetNameSyntax;
        private System.Windows.Forms.Label labelInputAsset;
        private System.Windows.Forms.ComboBox comboBoxSourceAsset;
        private System.Windows.Forms.Button buttonDown;
        private System.Windows.Forms.DataGridView dataGridViewEDL;
        private System.Windows.Forms.Button buttonUp;
        private System.Windows.Forms.Button buttonDelEntry;
        private System.Windows.Forms.Button buttonAddEDLEntry;
        private System.Windows.Forms.TextBox textBoxAssetDescription;
        private System.Windows.Forms.Label labelAssetDescription;
        private System.Windows.Forms.Button buttonExportEDL;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button buttonImportEDL;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}