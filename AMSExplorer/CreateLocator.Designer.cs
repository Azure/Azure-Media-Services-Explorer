namespace AMSExplorer
{
    partial class CreateLocator
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateLocator));
            buttonOk = new System.Windows.Forms.Button();
            buttonCancel = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            groupBoxEnd = new System.Windows.Forms.GroupBox();
            radioButtonEndUnlimited = new System.Windows.Forms.RadioButton();
            radioButtonEndYear = new System.Windows.Forms.RadioButton();
            radioButtonEndCustom = new System.Windows.Forms.RadioButton();
            dateTimePickerEndTime = new System.Windows.Forms.DateTimePicker();
            dateTimePickerEndDate = new System.Windows.Forms.DateTimePicker();
            groupBoxStart = new System.Windows.Forms.GroupBox();
            checkBoxStartDate = new System.Windows.Forms.CheckBox();
            dateTimePickerStartTime = new System.Windows.Forms.DateTimePicker();
            dateTimePickerStartDate = new System.Windows.Forms.DateTimePicker();
            labelAssetName = new System.Windows.Forms.Label();
            groupBox4 = new System.Windows.Forms.GroupBox();
            pictureBox9 = new System.Windows.Forms.PictureBox();
            pictureBox8 = new System.Windows.Forms.PictureBox();
            pictureBox7 = new System.Windows.Forms.PictureBox();
            pictureBox6 = new System.Windows.Forms.PictureBox();
            pictureBox5 = new System.Windows.Forms.PictureBox();
            pictureBox4 = new System.Windows.Forms.PictureBox();
            pictureBox3 = new System.Windows.Forms.PictureBox();
            pictureBox2 = new System.Windows.Forms.PictureBox();
            radioButtonDownloadAndClear = new System.Windows.Forms.RadioButton();
            radioButtonDownload = new System.Windows.Forms.RadioButton();
            radioButtonMultiDRM = new System.Windows.Forms.RadioButton();
            radioButtonMultiDRMCENC = new System.Windows.Forms.RadioButton();
            radioButtonClearKey = new System.Windows.Forms.RadioButton();
            radioButtonClearStream = new System.Windows.Forms.RadioButton();
            pictureBox1 = new System.Windows.Forms.PictureBox();
            labelWarning = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            textBoxLocatorGUID = new System.Windows.Forms.TextBox();
            checkBoxForLocatorGUID = new System.Windows.Forms.CheckBox();
            panel1 = new System.Windows.Forms.Panel();
            tabControlLocator = new System.Windows.Forms.TabControl();
            tabPagePolicyAndFilters = new System.Windows.Forms.TabPage();
            groupBoxForceLocator = new System.Windows.Forms.GroupBox();
            listViewFilters = new System.Windows.Forms.ListView();
            labelNoAssetFilter = new System.Windows.Forms.Label();
            tabPageStartEndTime = new System.Windows.Forms.TabPage();
            tabPageAdvanced = new System.Windows.Forms.TabPage();
            label5 = new System.Windows.Forms.Label();
            toolTip1 = new System.Windows.Forms.ToolTip(components);
            groupBoxEnd.SuspendLayout();
            groupBoxStart.SuspendLayout();
            groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox9).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox8).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox7).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel1.SuspendLayout();
            tabControlLocator.SuspendLayout();
            tabPagePolicyAndFilters.SuspendLayout();
            groupBoxForceLocator.SuspendLayout();
            tabPageStartEndTime.SuspendLayout();
            tabPageAdvanced.SuspendLayout();
            SuspendLayout();
            // 
            // buttonOk
            // 
            resources.ApplyResources(buttonOk, "buttonOk");
            buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            buttonOk.Name = "buttonOk";
            buttonOk.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            resources.ApplyResources(buttonCancel, "buttonCancel");
            buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            buttonCancel.Name = "buttonCancel";
            buttonCancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // groupBoxEnd
            // 
            groupBoxEnd.Controls.Add(radioButtonEndUnlimited);
            groupBoxEnd.Controls.Add(radioButtonEndYear);
            groupBoxEnd.Controls.Add(radioButtonEndCustom);
            groupBoxEnd.Controls.Add(dateTimePickerEndTime);
            groupBoxEnd.Controls.Add(dateTimePickerEndDate);
            groupBoxEnd.ForeColor = System.Drawing.Color.DarkBlue;
            resources.ApplyResources(groupBoxEnd, "groupBoxEnd");
            groupBoxEnd.Name = "groupBoxEnd";
            groupBoxEnd.TabStop = false;
            // 
            // radioButtonEndUnlimited
            // 
            resources.ApplyResources(radioButtonEndUnlimited, "radioButtonEndUnlimited");
            radioButtonEndUnlimited.Checked = true;
            radioButtonEndUnlimited.ForeColor = System.Drawing.SystemColors.ControlText;
            radioButtonEndUnlimited.Name = "radioButtonEndUnlimited";
            radioButtonEndUnlimited.TabStop = true;
            radioButtonEndUnlimited.UseVisualStyleBackColor = true;
            // 
            // radioButtonEndYear
            // 
            resources.ApplyResources(radioButtonEndYear, "radioButtonEndYear");
            radioButtonEndYear.ForeColor = System.Drawing.SystemColors.ControlText;
            radioButtonEndYear.Name = "radioButtonEndYear";
            radioButtonEndYear.UseVisualStyleBackColor = true;
            // 
            // radioButtonEndCustom
            // 
            resources.ApplyResources(radioButtonEndCustom, "radioButtonEndCustom");
            radioButtonEndCustom.ForeColor = System.Drawing.SystemColors.ControlText;
            radioButtonEndCustom.Name = "radioButtonEndCustom";
            radioButtonEndCustom.UseVisualStyleBackColor = true;
            radioButtonEndCustom.CheckedChanged += RadioButtonEndCustom_CheckedChanged;
            // 
            // dateTimePickerEndTime
            // 
            resources.ApplyResources(dateTimePickerEndTime, "dateTimePickerEndTime");
            dateTimePickerEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            dateTimePickerEndTime.MinDate = new System.DateTime(2014, 1, 1, 0, 0, 0, 0);
            dateTimePickerEndTime.Name = "dateTimePickerEndTime";
            dateTimePickerEndTime.ShowUpDown = true;
            dateTimePickerEndTime.ValueChanged += DateTimePickerEndTime_ValueChanged;
            // 
            // dateTimePickerEndDate
            // 
            resources.ApplyResources(dateTimePickerEndDate, "dateTimePickerEndDate");
            dateTimePickerEndDate.MinDate = new System.DateTime(2014, 1, 1, 0, 0, 0, 0);
            dateTimePickerEndDate.Name = "dateTimePickerEndDate";
            dateTimePickerEndDate.ValueChanged += DateTimePickerEndDate_ValueChanged;
            // 
            // groupBoxStart
            // 
            groupBoxStart.Controls.Add(checkBoxStartDate);
            groupBoxStart.Controls.Add(dateTimePickerStartTime);
            groupBoxStart.Controls.Add(dateTimePickerStartDate);
            groupBoxStart.ForeColor = System.Drawing.Color.DarkBlue;
            resources.ApplyResources(groupBoxStart, "groupBoxStart");
            groupBoxStart.Name = "groupBoxStart";
            groupBoxStart.TabStop = false;
            // 
            // checkBoxStartDate
            // 
            resources.ApplyResources(checkBoxStartDate, "checkBoxStartDate");
            checkBoxStartDate.ForeColor = System.Drawing.SystemColors.ControlText;
            checkBoxStartDate.Name = "checkBoxStartDate";
            checkBoxStartDate.UseVisualStyleBackColor = true;
            checkBoxStartDate.CheckedChanged += CheckBoxStartDate_CheckedChanged_1;
            // 
            // dateTimePickerStartTime
            // 
            resources.ApplyResources(dateTimePickerStartTime, "dateTimePickerStartTime");
            dateTimePickerStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            dateTimePickerStartTime.MinDate = new System.DateTime(2014, 1, 1, 0, 0, 0, 0);
            dateTimePickerStartTime.Name = "dateTimePickerStartTime";
            dateTimePickerStartTime.ShowUpDown = true;
            dateTimePickerStartTime.ValueChanged += DateTimePickerStartTime_ValueChanged;
            // 
            // dateTimePickerStartDate
            // 
            resources.ApplyResources(dateTimePickerStartDate, "dateTimePickerStartDate");
            dateTimePickerStartDate.Name = "dateTimePickerStartDate";
            dateTimePickerStartDate.ValueChanged += DateTimePickerStartDate_ValueChanged;
            // 
            // labelAssetName
            // 
            resources.ApplyResources(labelAssetName, "labelAssetName");
            labelAssetName.Name = "labelAssetName";
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(pictureBox9);
            groupBox4.Controls.Add(pictureBox8);
            groupBox4.Controls.Add(pictureBox7);
            groupBox4.Controls.Add(pictureBox6);
            groupBox4.Controls.Add(pictureBox5);
            groupBox4.Controls.Add(pictureBox4);
            groupBox4.Controls.Add(pictureBox3);
            groupBox4.Controls.Add(pictureBox2);
            groupBox4.Controls.Add(radioButtonDownloadAndClear);
            groupBox4.Controls.Add(radioButtonDownload);
            groupBox4.Controls.Add(radioButtonMultiDRM);
            groupBox4.Controls.Add(radioButtonMultiDRMCENC);
            groupBox4.Controls.Add(radioButtonClearKey);
            groupBox4.Controls.Add(radioButtonClearStream);
            groupBox4.ForeColor = System.Drawing.Color.DarkBlue;
            resources.ApplyResources(groupBox4, "groupBox4");
            groupBox4.Name = "groupBox4";
            groupBox4.TabStop = false;
            // 
            // pictureBox9
            // 
            resources.ApplyResources(pictureBox9, "pictureBox9");
            pictureBox9.Name = "pictureBox9";
            pictureBox9.TabStop = false;
            pictureBox9.Click += PictureBox9_Click;
            // 
            // pictureBox8
            // 
            resources.ApplyResources(pictureBox8, "pictureBox8");
            pictureBox8.Name = "pictureBox8";
            pictureBox8.TabStop = false;
            pictureBox8.Click += PictureBox8_Click;
            // 
            // pictureBox7
            // 
            resources.ApplyResources(pictureBox7, "pictureBox7");
            pictureBox7.Name = "pictureBox7";
            pictureBox7.TabStop = false;
            pictureBox7.Click += PictureBox7_Click;
            // 
            // pictureBox6
            // 
            resources.ApplyResources(pictureBox6, "pictureBox6");
            pictureBox6.Name = "pictureBox6";
            pictureBox6.TabStop = false;
            pictureBox6.Click += PictureBox6_Click;
            // 
            // pictureBox5
            // 
            resources.ApplyResources(pictureBox5, "pictureBox5");
            pictureBox5.Name = "pictureBox5";
            pictureBox5.TabStop = false;
            // 
            // pictureBox4
            // 
            resources.ApplyResources(pictureBox4, "pictureBox4");
            pictureBox4.Name = "pictureBox4";
            pictureBox4.TabStop = false;
            // 
            // pictureBox3
            // 
            resources.ApplyResources(pictureBox3, "pictureBox3");
            pictureBox3.Name = "pictureBox3";
            pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            resources.ApplyResources(pictureBox2, "pictureBox2");
            pictureBox2.Name = "pictureBox2";
            pictureBox2.TabStop = false;
            // 
            // radioButtonDownloadAndClear
            // 
            resources.ApplyResources(radioButtonDownloadAndClear, "radioButtonDownloadAndClear");
            radioButtonDownloadAndClear.ForeColor = System.Drawing.SystemColors.ControlText;
            radioButtonDownloadAndClear.Name = "radioButtonDownloadAndClear";
            radioButtonDownloadAndClear.UseVisualStyleBackColor = true;
            // 
            // radioButtonDownload
            // 
            resources.ApplyResources(radioButtonDownload, "radioButtonDownload");
            radioButtonDownload.ForeColor = System.Drawing.SystemColors.ControlText;
            radioButtonDownload.Name = "radioButtonDownload";
            radioButtonDownload.UseVisualStyleBackColor = true;
            // 
            // radioButtonMultiDRM
            // 
            resources.ApplyResources(radioButtonMultiDRM, "radioButtonMultiDRM");
            radioButtonMultiDRM.ForeColor = System.Drawing.SystemColors.ControlText;
            radioButtonMultiDRM.Name = "radioButtonMultiDRM";
            toolTip1.SetToolTip(radioButtonMultiDRM, resources.GetString("radioButtonMultiDRM.ToolTip"));
            radioButtonMultiDRM.UseVisualStyleBackColor = true;
            radioButtonMultiDRM.CheckedChanged += RadioButtonMultiDRM_CheckedChanged;
            // 
            // radioButtonMultiDRMCENC
            // 
            resources.ApplyResources(radioButtonMultiDRMCENC, "radioButtonMultiDRMCENC");
            radioButtonMultiDRMCENC.ForeColor = System.Drawing.SystemColors.ControlText;
            radioButtonMultiDRMCENC.Name = "radioButtonMultiDRMCENC";
            toolTip1.SetToolTip(radioButtonMultiDRMCENC, resources.GetString("radioButtonMultiDRMCENC.ToolTip"));
            radioButtonMultiDRMCENC.UseVisualStyleBackColor = true;
            radioButtonMultiDRMCENC.CheckedChanged += RadioButtonMultiDRMCENC_CheckedChanged;
            // 
            // radioButtonClearKey
            // 
            resources.ApplyResources(radioButtonClearKey, "radioButtonClearKey");
            radioButtonClearKey.ForeColor = System.Drawing.SystemColors.ControlText;
            radioButtonClearKey.Name = "radioButtonClearKey";
            toolTip1.SetToolTip(radioButtonClearKey, resources.GetString("radioButtonClearKey.ToolTip"));
            radioButtonClearKey.UseVisualStyleBackColor = true;
            radioButtonClearKey.CheckedChanged += RadioButtonClearKey_CheckedChanged;
            // 
            // radioButtonClearStream
            // 
            resources.ApplyResources(radioButtonClearStream, "radioButtonClearStream");
            radioButtonClearStream.Checked = true;
            radioButtonClearStream.ForeColor = System.Drawing.SystemColors.ControlText;
            radioButtonClearStream.Name = "radioButtonClearStream";
            radioButtonClearStream.TabStop = true;
            radioButtonClearStream.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            resources.ApplyResources(pictureBox1, "pictureBox1");
            pictureBox1.Name = "pictureBox1";
            pictureBox1.TabStop = false;
            // 
            // labelWarning
            // 
            resources.ApplyResources(labelWarning, "labelWarning");
            labelWarning.ForeColor = System.Drawing.Color.Red;
            labelWarning.Name = "labelWarning";
            // 
            // label8
            // 
            label8.ForeColor = System.Drawing.SystemColors.WindowFrame;
            resources.ApplyResources(label8, "label8");
            label8.Name = "label8";
            // 
            // textBoxLocatorGUID
            // 
            resources.ApplyResources(textBoxLocatorGUID, "textBoxLocatorGUID");
            textBoxLocatorGUID.Name = "textBoxLocatorGUID";
            // 
            // checkBoxForLocatorGUID
            // 
            resources.ApplyResources(checkBoxForLocatorGUID, "checkBoxForLocatorGUID");
            checkBoxForLocatorGUID.Name = "checkBoxForLocatorGUID";
            checkBoxForLocatorGUID.UseVisualStyleBackColor = true;
            checkBoxForLocatorGUID.CheckedChanged += UpdateLocatorGUID_CheckedChanged;
            // 
            // panel1
            // 
            panel1.BackColor = System.Drawing.SystemColors.Control;
            panel1.Controls.Add(buttonCancel);
            panel1.Controls.Add(buttonOk);
            resources.ApplyResources(panel1, "panel1");
            panel1.Name = "panel1";
            // 
            // tabControlLocator
            // 
            resources.ApplyResources(tabControlLocator, "tabControlLocator");
            tabControlLocator.Controls.Add(tabPagePolicyAndFilters);
            tabControlLocator.Controls.Add(tabPageStartEndTime);
            tabControlLocator.Controls.Add(tabPageAdvanced);
            tabControlLocator.Name = "tabControlLocator";
            tabControlLocator.SelectedIndex = 0;
            // 
            // tabPagePolicyAndFilters
            // 
            tabPagePolicyAndFilters.Controls.Add(groupBoxForceLocator);
            tabPagePolicyAndFilters.Controls.Add(labelNoAssetFilter);
            tabPagePolicyAndFilters.Controls.Add(groupBox4);
            resources.ApplyResources(tabPagePolicyAndFilters, "tabPagePolicyAndFilters");
            tabPagePolicyAndFilters.Name = "tabPagePolicyAndFilters";
            tabPagePolicyAndFilters.UseVisualStyleBackColor = true;
            // 
            // groupBoxForceLocator
            // 
            resources.ApplyResources(groupBoxForceLocator, "groupBoxForceLocator");
            groupBoxForceLocator.Controls.Add(listViewFilters);
            groupBoxForceLocator.ForeColor = System.Drawing.Color.DarkBlue;
            groupBoxForceLocator.Name = "groupBoxForceLocator";
            groupBoxForceLocator.TabStop = false;
            // 
            // listViewFilters
            // 
            resources.ApplyResources(listViewFilters, "listViewFilters");
            listViewFilters.CheckBoxes = true;
            listViewFilters.FullRowSelect = true;
            listViewFilters.GridLines = true;
            listViewFilters.Name = "listViewFilters";
            listViewFilters.UseCompatibleStateImageBehavior = false;
            listViewFilters.View = System.Windows.Forms.View.List;
            // 
            // labelNoAssetFilter
            // 
            labelNoAssetFilter.ForeColor = System.Drawing.SystemColors.WindowFrame;
            resources.ApplyResources(labelNoAssetFilter, "labelNoAssetFilter");
            labelNoAssetFilter.Name = "labelNoAssetFilter";
            // 
            // tabPageStartEndTime
            // 
            tabPageStartEndTime.Controls.Add(groupBoxStart);
            tabPageStartEndTime.Controls.Add(groupBoxEnd);
            resources.ApplyResources(tabPageStartEndTime, "tabPageStartEndTime");
            tabPageStartEndTime.Name = "tabPageStartEndTime";
            tabPageStartEndTime.UseVisualStyleBackColor = true;
            // 
            // tabPageAdvanced
            // 
            tabPageAdvanced.Controls.Add(label8);
            tabPageAdvanced.Controls.Add(textBoxLocatorGUID);
            tabPageAdvanced.Controls.Add(checkBoxForLocatorGUID);
            resources.ApplyResources(tabPageAdvanced, "tabPageAdvanced");
            tabPageAdvanced.Name = "tabPageAdvanced";
            tabPageAdvanced.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            resources.ApplyResources(label5, "label5");
            label5.ForeColor = System.Drawing.Color.DarkBlue;
            label5.Name = "label5";
            // 
            // CreateLocator
            // 
            AcceptButton = buttonOk;
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            BackColor = System.Drawing.SystemColors.Window;
            CancelButton = buttonCancel;
            Controls.Add(label5);
            Controls.Add(tabControlLocator);
            Controls.Add(panel1);
            Controls.Add(pictureBox1);
            Controls.Add(labelWarning);
            Controls.Add(labelAssetName);
            Controls.Add(label1);
            Name = "CreateLocator";
            Load += CreateLocator_Load;
            Shown += CreateLocator_Shown;
            DpiChanged += CreateLocator_DpiChanged;
            groupBoxEnd.ResumeLayout(false);
            groupBoxEnd.PerformLayout();
            groupBoxStart.ResumeLayout(false);
            groupBoxStart.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox9).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox8).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox7).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel1.ResumeLayout(false);
            tabControlLocator.ResumeLayout(false);
            tabPagePolicyAndFilters.ResumeLayout(false);
            groupBoxForceLocator.ResumeLayout(false);
            tabPageStartEndTime.ResumeLayout(false);
            tabPageAdvanced.ResumeLayout(false);
            tabPageAdvanced.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        public System.Windows.Forms.Button buttonOk;
        public System.Windows.Forms.Button buttonCancel;
        public System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBoxEnd;
        private System.Windows.Forms.DateTimePicker dateTimePickerEndTime;
        private System.Windows.Forms.DateTimePicker dateTimePickerEndDate;
        private System.Windows.Forms.GroupBox groupBoxStart;
        private System.Windows.Forms.CheckBox checkBoxStartDate;
        private System.Windows.Forms.DateTimePicker dateTimePickerStartTime;
        private System.Windows.Forms.DateTimePicker dateTimePickerStartDate;
        private System.Windows.Forms.Label labelAssetName;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton radioButtonEndUnlimited;
        private System.Windows.Forms.RadioButton radioButtonEndYear;
        private System.Windows.Forms.RadioButton radioButtonEndCustom;
        private System.Windows.Forms.Label labelWarning;
        private System.Windows.Forms.CheckBox checkBoxForLocatorGUID;
        private System.Windows.Forms.TextBox textBoxLocatorGUID;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radioButtonDownload;
        private System.Windows.Forms.RadioButton radioButtonMultiDRM;
        private System.Windows.Forms.RadioButton radioButtonMultiDRMCENC;
        private System.Windows.Forms.RadioButton radioButtonClearKey;
        private System.Windows.Forms.RadioButton radioButtonClearStream;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.RadioButton radioButtonDownloadAndClear;
        private System.Windows.Forms.PictureBox pictureBox9;
        private System.Windows.Forms.PictureBox pictureBox8;
        private System.Windows.Forms.PictureBox pictureBox7;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.TabControl tabControlLocator;
        private System.Windows.Forms.TabPage tabPagePolicyAndFilters;
        private System.Windows.Forms.TabPage tabPageStartEndTime;
        private System.Windows.Forms.TabPage tabPageAdvanced;
        private System.Windows.Forms.GroupBox groupBoxForceLocator;
        private System.Windows.Forms.ListView listViewFilters;
        private System.Windows.Forms.Label labelNoAssetFilter;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}