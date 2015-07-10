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
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(329, 14);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(112, 27);
            this.buttonOk.TabIndex = 4;
            this.buttonOk.Text = "Create Locator";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(448, 14);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(112, 27);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 495);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(455, 15);
            this.label1.TabIndex = 35;
            this.label1.Text = "Locators URL will be copied to the clipboard and can be seen in the asset informa" +
    "tion";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButtonEndUnlimited);
            this.groupBox1.Controls.Add(this.radioButtonEndYear);
            this.groupBox1.Controls.Add(this.radioButtonEndCustom);
            this.groupBox1.Controls.Add(this.dateTimePickerEndTime);
            this.groupBox1.Controls.Add(this.dateTimePickerEndDate);
            this.groupBox1.Location = new System.Drawing.Point(286, 178);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(257, 208);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "End date/time";
            // 
            // radioButtonEndUnlimited
            // 
            this.radioButtonEndUnlimited.AutoSize = true;
            this.radioButtonEndUnlimited.Location = new System.Drawing.Point(10, 181);
            this.radioButtonEndUnlimited.Name = "radioButtonEndUnlimited";
            this.radioButtonEndUnlimited.Size = new System.Drawing.Size(73, 19);
            this.radioButtonEndUnlimited.TabIndex = 4;
            this.radioButtonEndUnlimited.Text = "100 years";
            this.radioButtonEndUnlimited.UseVisualStyleBackColor = true;
            // 
            // radioButtonEndYear
            // 
            this.radioButtonEndYear.AutoSize = true;
            this.radioButtonEndYear.Location = new System.Drawing.Point(10, 148);
            this.radioButtonEndYear.Name = "radioButtonEndYear";
            this.radioButtonEndYear.Size = new System.Drawing.Size(56, 19);
            this.radioButtonEndYear.TabIndex = 3;
            this.radioButtonEndYear.Text = "1 year";
            this.radioButtonEndYear.UseVisualStyleBackColor = true;
            // 
            // radioButtonEndCustom
            // 
            this.radioButtonEndCustom.AutoSize = true;
            this.radioButtonEndCustom.Checked = true;
            this.radioButtonEndCustom.Location = new System.Drawing.Point(10, 30);
            this.radioButtonEndCustom.Name = "radioButtonEndCustom";
            this.radioButtonEndCustom.Size = new System.Drawing.Size(96, 19);
            this.radioButtonEndCustom.TabIndex = 0;
            this.radioButtonEndCustom.TabStop = true;
            this.radioButtonEndCustom.Text = "Custom date:";
            this.radioButtonEndCustom.UseVisualStyleBackColor = true;
            this.radioButtonEndCustom.CheckedChanged += new System.EventHandler(this.radioButtonEndCustom_CheckedChanged);
            // 
            // dateTimePickerEndTime
            // 
            this.dateTimePickerEndTime.CustomFormat = "";
            this.dateTimePickerEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePickerEndTime.Location = new System.Drawing.Point(10, 99);
            this.dateTimePickerEndTime.MinDate = new System.DateTime(2014, 1, 1, 0, 0, 0, 0);
            this.dateTimePickerEndTime.Name = "dateTimePickerEndTime";
            this.dateTimePickerEndTime.ShowUpDown = true;
            this.dateTimePickerEndTime.Size = new System.Drawing.Size(233, 23);
            this.dateTimePickerEndTime.TabIndex = 2;
            this.dateTimePickerEndTime.ValueChanged += new System.EventHandler(this.dateTimePickerEndTime_ValueChanged);
            // 
            // dateTimePickerEndDate
            // 
            this.dateTimePickerEndDate.CustomFormat = "";
            this.dateTimePickerEndDate.Location = new System.Drawing.Point(10, 69);
            this.dateTimePickerEndDate.MinDate = new System.DateTime(2014, 1, 1, 0, 0, 0, 0);
            this.dateTimePickerEndDate.Name = "dateTimePickerEndDate";
            this.dateTimePickerEndDate.Size = new System.Drawing.Size(233, 23);
            this.dateTimePickerEndDate.TabIndex = 1;
            this.dateTimePickerEndDate.ValueChanged += new System.EventHandler(this.dateTimePickerEndDate_ValueChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBoxStartDate);
            this.groupBox2.Controls.Add(this.dateTimePickerStartTime);
            this.groupBox2.Controls.Add(this.dateTimePickerStartDate);
            this.groupBox2.Location = new System.Drawing.Point(14, 178);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(257, 208);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Start date/time";
            // 
            // checkBoxStartDate
            // 
            this.checkBoxStartDate.AutoSize = true;
            this.checkBoxStartDate.Location = new System.Drawing.Point(12, 30);
            this.checkBoxStartDate.Name = "checkBoxStartDate";
            this.checkBoxStartDate.Size = new System.Drawing.Size(157, 19);
            this.checkBoxStartDate.TabIndex = 0;
            this.checkBoxStartDate.Text = "Specify a start date/time:";
            this.checkBoxStartDate.UseVisualStyleBackColor = true;
            this.checkBoxStartDate.CheckedChanged += new System.EventHandler(this.checkBoxStartDate_CheckedChanged_1);
            // 
            // dateTimePickerStartTime
            // 
            this.dateTimePickerStartTime.CustomFormat = "";
            this.dateTimePickerStartTime.Enabled = false;
            this.dateTimePickerStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePickerStartTime.Location = new System.Drawing.Point(13, 99);
            this.dateTimePickerStartTime.MinDate = new System.DateTime(2014, 1, 1, 0, 0, 0, 0);
            this.dateTimePickerStartTime.Name = "dateTimePickerStartTime";
            this.dateTimePickerStartTime.ShowUpDown = true;
            this.dateTimePickerStartTime.Size = new System.Drawing.Size(233, 23);
            this.dateTimePickerStartTime.TabIndex = 2;
            this.dateTimePickerStartTime.ValueChanged += new System.EventHandler(this.dateTimePickerStartTime_ValueChanged);
            // 
            // dateTimePickerStartDate
            // 
            this.dateTimePickerStartDate.Enabled = false;
            this.dateTimePickerStartDate.Location = new System.Drawing.Point(13, 69);
            this.dateTimePickerStartDate.Name = "dateTimePickerStartDate";
            this.dateTimePickerStartDate.Size = new System.Drawing.Size(233, 23);
            this.dateTimePickerStartDate.TabIndex = 1;
            this.dateTimePickerStartDate.ValueChanged += new System.EventHandler(this.dateTimePickerStartDate_ValueChanged);
            // 
            // labelAssetName
            // 
            this.labelAssetName.Location = new System.Drawing.Point(15, 15);
            this.labelAssetName.Name = "labelAssetName";
            this.labelAssetName.Size = new System.Drawing.Size(527, 15);
            this.labelAssetName.TabIndex = 42;
            this.labelAssetName.Text = "label2";
            // 
            // radioButtonOrigin
            // 
            this.radioButtonOrigin.AutoSize = true;
            this.radioButtonOrigin.Checked = true;
            this.radioButtonOrigin.Location = new System.Drawing.Point(41, 33);
            this.radioButtonOrigin.Name = "radioButtonOrigin";
            this.radioButtonOrigin.Size = new System.Drawing.Size(231, 19);
            this.radioButtonOrigin.TabIndex = 0;
            this.radioButtonOrigin.TabStop = true;
            this.radioButtonOrigin.Text = "Streaming locator (adaptive streaming)";
            this.radioButtonOrigin.UseVisualStyleBackColor = true;
            this.radioButtonOrigin.CheckedChanged += new System.EventHandler(this.UpdateLocatorGUID_CheckedChanged);
            // 
            // radioButtonSAS
            // 
            this.radioButtonSAS.AutoSize = true;
            this.radioButtonSAS.Location = new System.Drawing.Point(41, 61);
            this.radioButtonSAS.Name = "radioButtonSAS";
            this.radioButtonSAS.Size = new System.Drawing.Size(212, 19);
            this.radioButtonSAS.TabIndex = 1;
            this.radioButtonSAS.Text = "SAS locator (progressive download)";
            this.radioButtonSAS.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.pictureBox2);
            this.groupBox4.Controls.Add(this.pictureBox1);
            this.groupBox4.Controls.Add(this.radioButtonSAS);
            this.groupBox4.Controls.Add(this.radioButtonOrigin);
            this.groupBox4.Location = new System.Drawing.Point(14, 55);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(528, 99);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Locator type";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::AMSExplorer.Bitmaps.SAS_locator;
            this.pictureBox2.Location = new System.Drawing.Point(10, 62);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(16, 16);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 50;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::AMSExplorer.Bitmaps.streaming_locator;
            this.pictureBox1.Location = new System.Drawing.Point(10, 35);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 48;
            this.pictureBox1.TabStop = false;
            // 
            // labelWarning
            // 
            this.labelWarning.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelWarning.AutoSize = true;
            this.labelWarning.ForeColor = System.Drawing.Color.Red;
            this.labelWarning.Location = new System.Drawing.Point(37, 524);
            this.labelWarning.Name = "labelWarning";
            this.labelWarning.Size = new System.Drawing.Size(52, 15);
            this.labelWarning.TabIndex = 44;
            this.labelWarning.Text = "Warning";
            // 
            // groupBoxForceLocator
            // 
            this.groupBoxForceLocator.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxForceLocator.Controls.Add(this.label8);
            this.groupBoxForceLocator.Controls.Add(this.textBoxLocatorGUID);
            this.groupBoxForceLocator.Controls.Add(this.checkBoxForLocatorGUID);
            this.groupBoxForceLocator.Location = new System.Drawing.Point(14, 392);
            this.groupBoxForceLocator.Name = "groupBoxForceLocator";
            this.groupBoxForceLocator.Size = new System.Drawing.Size(528, 91);
            this.groupBoxForceLocator.TabIndex = 3;
            this.groupBoxForceLocator.TabStop = false;
            this.groupBoxForceLocator.Text = "Advanced user only";
            // 
            // label8
            // 
            this.label8.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label8.Location = new System.Drawing.Point(184, 24);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(331, 18);
            this.label8.TabIndex = 68;
            this.label8.Text = "format: nb:lid:UUID:96687412-6d...  or 96687412-6d...";
            // 
            // textBoxLocatorGUID
            // 
            this.textBoxLocatorGUID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxLocatorGUID.Enabled = false;
            this.textBoxLocatorGUID.Location = new System.Drawing.Point(13, 52);
            this.textBoxLocatorGUID.Name = "textBoxLocatorGUID";
            this.textBoxLocatorGUID.Size = new System.Drawing.Size(502, 23);
            this.textBoxLocatorGUID.TabIndex = 1;
            // 
            // checkBoxForLocatorGUID
            // 
            this.checkBoxForLocatorGUID.AutoSize = true;
            this.checkBoxForLocatorGUID.Location = new System.Drawing.Point(13, 23);
            this.checkBoxForLocatorGUID.Name = "checkBoxForLocatorGUID";
            this.checkBoxForLocatorGUID.Size = new System.Drawing.Size(156, 19);
            this.checkBoxForLocatorGUID.TabIndex = 0;
            this.checkBoxForLocatorGUID.Text = "Force a locator ID/GUID :";
            this.checkBoxForLocatorGUID.UseVisualStyleBackColor = true;
            this.checkBoxForLocatorGUID.CheckedChanged += new System.EventHandler(this.UpdateLocatorGUID_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonOk);
            this.panel1.Location = new System.Drawing.Point(-2, 553);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(575, 55);
            this.panel1.TabIndex = 60;
            // 
            // CreateLocator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(572, 607);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBoxForceLocator);
            this.Controls.Add(this.labelWarning);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.labelAssetName);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "CreateLocator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Publish an asset";
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