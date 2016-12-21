﻿namespace AMSExplorer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateLocator));
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtonEndUnlimited = new System.Windows.Forms.RadioButton();
            this.radioButtonEndYear = new System.Windows.Forms.RadioButton();
            this.radioButtonEndCustom = new System.Windows.Forms.RadioButton();
            this.dateTimePickerEndTime = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerEndDate = new System.Windows.Forms.DateTimePicker();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBoxStartDate = new System.Windows.Forms.CheckBox();
            this.dateTimePickerStartTime = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerStartDate = new System.Windows.Forms.DateTimePicker();
            this.labelAssetName = new System.Windows.Forms.Label();
            this.radioButtonOrigin = new System.Windows.Forms.RadioButton();
            this.radioButtonSAS = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelWarning = new System.Windows.Forms.Label();
            this.groupBoxForceLocator = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxLocatorGUID = new System.Windows.Forms.TextBox();
            this.checkBoxForLocatorGUID = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBoxForceLocator.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            resources.ApplyResources(this.buttonOk, "buttonOk");
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButtonEndUnlimited);
            this.groupBox1.Controls.Add(this.radioButtonEndYear);
            this.groupBox1.Controls.Add(this.radioButtonEndCustom);
            this.groupBox1.Controls.Add(this.dateTimePickerEndTime);
            this.groupBox1.Controls.Add(this.dateTimePickerEndDate);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // radioButtonEndUnlimited
            // 
            resources.ApplyResources(this.radioButtonEndUnlimited, "radioButtonEndUnlimited");
            this.radioButtonEndUnlimited.Name = "radioButtonEndUnlimited";
            this.radioButtonEndUnlimited.UseVisualStyleBackColor = true;
            // 
            // radioButtonEndYear
            // 
            resources.ApplyResources(this.radioButtonEndYear, "radioButtonEndYear");
            this.radioButtonEndYear.Name = "radioButtonEndYear";
            this.radioButtonEndYear.UseVisualStyleBackColor = true;
            // 
            // radioButtonEndCustom
            // 
            resources.ApplyResources(this.radioButtonEndCustom, "radioButtonEndCustom");
            this.radioButtonEndCustom.Checked = true;
            this.radioButtonEndCustom.Name = "radioButtonEndCustom";
            this.radioButtonEndCustom.TabStop = true;
            this.radioButtonEndCustom.UseVisualStyleBackColor = true;
            this.radioButtonEndCustom.CheckedChanged += new System.EventHandler(this.radioButtonEndCustom_CheckedChanged);
            // 
            // dateTimePickerEndTime
            // 
            resources.ApplyResources(this.dateTimePickerEndTime, "dateTimePickerEndTime");
            this.dateTimePickerEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePickerEndTime.MinDate = new System.DateTime(2014, 1, 1, 0, 0, 0, 0);
            this.dateTimePickerEndTime.Name = "dateTimePickerEndTime";
            this.dateTimePickerEndTime.ShowUpDown = true;
            this.dateTimePickerEndTime.ValueChanged += new System.EventHandler(this.dateTimePickerEndTime_ValueChanged);
            // 
            // dateTimePickerEndDate
            // 
            resources.ApplyResources(this.dateTimePickerEndDate, "dateTimePickerEndDate");
            this.dateTimePickerEndDate.MinDate = new System.DateTime(2014, 1, 1, 0, 0, 0, 0);
            this.dateTimePickerEndDate.Name = "dateTimePickerEndDate";
            this.dateTimePickerEndDate.ValueChanged += new System.EventHandler(this.dateTimePickerEndDate_ValueChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBoxStartDate);
            this.groupBox2.Controls.Add(this.dateTimePickerStartTime);
            this.groupBox2.Controls.Add(this.dateTimePickerStartDate);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // checkBoxStartDate
            // 
            resources.ApplyResources(this.checkBoxStartDate, "checkBoxStartDate");
            this.checkBoxStartDate.Name = "checkBoxStartDate";
            this.checkBoxStartDate.UseVisualStyleBackColor = true;
            this.checkBoxStartDate.CheckedChanged += new System.EventHandler(this.checkBoxStartDate_CheckedChanged_1);
            // 
            // dateTimePickerStartTime
            // 
            resources.ApplyResources(this.dateTimePickerStartTime, "dateTimePickerStartTime");
            this.dateTimePickerStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePickerStartTime.MinDate = new System.DateTime(2014, 1, 1, 0, 0, 0, 0);
            this.dateTimePickerStartTime.Name = "dateTimePickerStartTime";
            this.dateTimePickerStartTime.ShowUpDown = true;
            this.dateTimePickerStartTime.ValueChanged += new System.EventHandler(this.dateTimePickerStartTime_ValueChanged);
            // 
            // dateTimePickerStartDate
            // 
            resources.ApplyResources(this.dateTimePickerStartDate, "dateTimePickerStartDate");
            this.dateTimePickerStartDate.Name = "dateTimePickerStartDate";
            this.dateTimePickerStartDate.ValueChanged += new System.EventHandler(this.dateTimePickerStartDate_ValueChanged);
            // 
            // labelAssetName
            // 
            resources.ApplyResources(this.labelAssetName, "labelAssetName");
            this.labelAssetName.Name = "labelAssetName";
            // 
            // radioButtonOrigin
            // 
            resources.ApplyResources(this.radioButtonOrigin, "radioButtonOrigin");
            this.radioButtonOrigin.Checked = true;
            this.radioButtonOrigin.Name = "radioButtonOrigin";
            this.radioButtonOrigin.TabStop = true;
            this.radioButtonOrigin.UseVisualStyleBackColor = true;
            this.radioButtonOrigin.CheckedChanged += new System.EventHandler(this.UpdateLocatorGUID_CheckedChanged);
            // 
            // radioButtonSAS
            // 
            resources.ApplyResources(this.radioButtonSAS, "radioButtonSAS");
            this.radioButtonSAS.Name = "radioButtonSAS";
            this.radioButtonSAS.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.pictureBox2);
            this.groupBox4.Controls.Add(this.pictureBox1);
            this.groupBox4.Controls.Add(this.radioButtonSAS);
            this.groupBox4.Controls.Add(this.radioButtonOrigin);
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::AMSExplorer.Bitmaps.SAS_locator;
            resources.ApplyResources(this.pictureBox2, "pictureBox2");
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::AMSExplorer.Bitmaps.streaming_locator;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // labelWarning
            // 
            resources.ApplyResources(this.labelWarning, "labelWarning");
            this.labelWarning.ForeColor = System.Drawing.Color.Red;
            this.labelWarning.Name = "labelWarning";
            // 
            // groupBoxForceLocator
            // 
            resources.ApplyResources(this.groupBoxForceLocator, "groupBoxForceLocator");
            this.groupBoxForceLocator.Controls.Add(this.label8);
            this.groupBoxForceLocator.Controls.Add(this.textBoxLocatorGUID);
            this.groupBoxForceLocator.Controls.Add(this.checkBoxForLocatorGUID);
            this.groupBoxForceLocator.Name = "groupBoxForceLocator";
            this.groupBoxForceLocator.TabStop = false;
            // 
            // label8
            // 
            this.label8.ForeColor = System.Drawing.SystemColors.WindowFrame;
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // textBoxLocatorGUID
            // 
            resources.ApplyResources(this.textBoxLocatorGUID, "textBoxLocatorGUID");
            this.textBoxLocatorGUID.Name = "textBoxLocatorGUID";
            // 
            // checkBoxForLocatorGUID
            // 
            resources.ApplyResources(this.checkBoxForLocatorGUID, "checkBoxForLocatorGUID");
            this.checkBoxForLocatorGUID.Name = "checkBoxForLocatorGUID";
            this.checkBoxForLocatorGUID.UseVisualStyleBackColor = true;
            this.checkBoxForLocatorGUID.CheckedChanged += new System.EventHandler(this.UpdateLocatorGUID_CheckedChanged);
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonOk);
            this.panel1.Name = "panel1";
            // 
            // CreateLocator
            // 
            this.AcceptButton = this.buttonOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBoxForceLocator);
            this.Controls.Add(this.labelWarning);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.labelAssetName);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Name = "CreateLocator";
            this.Load += new System.EventHandler(this.CreateLocator_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBoxForceLocator.ResumeLayout(false);
            this.groupBoxForceLocator.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button buttonOk;
        public System.Windows.Forms.Button buttonCancel;
        public System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dateTimePickerEndTime;
        private System.Windows.Forms.DateTimePicker dateTimePickerEndDate;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkBoxStartDate;
        private System.Windows.Forms.DateTimePicker dateTimePickerStartTime;
        private System.Windows.Forms.DateTimePicker dateTimePickerStartDate;
        private System.Windows.Forms.Label labelAssetName;
        private System.Windows.Forms.RadioButton radioButtonOrigin;
        private System.Windows.Forms.RadioButton radioButtonSAS;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton radioButtonEndUnlimited;
        private System.Windows.Forms.RadioButton radioButtonEndYear;
        private System.Windows.Forms.RadioButton radioButtonEndCustom;
        private System.Windows.Forms.Label labelWarning;
        private System.Windows.Forms.GroupBox groupBoxForceLocator;
        private System.Windows.Forms.CheckBox checkBoxForLocatorGUID;
        private System.Windows.Forms.TextBox textBoxLocatorGUID;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel1;
    }
}