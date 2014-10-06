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
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(111, 392);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(113, 32);
            this.buttonOk.TabIndex = 17;
            this.buttonOk.Text = "Create Locator";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(245, 392);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(113, 32);
            this.buttonCancel.TabIndex = 16;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 346);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(410, 13);
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
            this.groupBox1.Location = new System.Drawing.Point(245, 154);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(220, 180);
            this.groupBox1.TabIndex = 37;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "End date/time";
            // 
            // radioButtonEndUnlimited
            // 
            this.radioButtonEndUnlimited.AutoSize = true;
            this.radioButtonEndUnlimited.Location = new System.Drawing.Point(9, 157);
            this.radioButtonEndUnlimited.Name = "radioButtonEndUnlimited";
            this.radioButtonEndUnlimited.Size = new System.Drawing.Size(71, 17);
            this.radioButtonEndUnlimited.TabIndex = 35;
            this.radioButtonEndUnlimited.Text = "100 years";
            this.radioButtonEndUnlimited.UseVisualStyleBackColor = true;
            // 
            // radioButtonEndYear
            // 
            this.radioButtonEndYear.AutoSize = true;
            this.radioButtonEndYear.Location = new System.Drawing.Point(9, 128);
            this.radioButtonEndYear.Name = "radioButtonEndYear";
            this.radioButtonEndYear.Size = new System.Drawing.Size(54, 17);
            this.radioButtonEndYear.TabIndex = 34;
            this.radioButtonEndYear.Text = "1 year";
            this.radioButtonEndYear.UseVisualStyleBackColor = true;
            // 
            // radioButtonEndCustom
            // 
            this.radioButtonEndCustom.AutoSize = true;
            this.radioButtonEndCustom.Checked = true;
            this.radioButtonEndCustom.Location = new System.Drawing.Point(9, 26);
            this.radioButtonEndCustom.Name = "radioButtonEndCustom";
            this.radioButtonEndCustom.Size = new System.Drawing.Size(87, 17);
            this.radioButtonEndCustom.TabIndex = 33;
            this.radioButtonEndCustom.TabStop = true;
            this.radioButtonEndCustom.Text = "Custom date:";
            this.radioButtonEndCustom.UseVisualStyleBackColor = true;
            this.radioButtonEndCustom.CheckedChanged += new System.EventHandler(this.radioButtonEndCustom_CheckedChanged);
            // 
            // dateTimePickerEndTime
            // 
            this.dateTimePickerEndTime.CustomFormat = "";
            this.dateTimePickerEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePickerEndTime.Location = new System.Drawing.Point(9, 86);
            this.dateTimePickerEndTime.MinDate = new System.DateTime(2014, 1, 1, 0, 0, 0, 0);
            this.dateTimePickerEndTime.Name = "dateTimePickerEndTime";
            this.dateTimePickerEndTime.ShowUpDown = true;
            this.dateTimePickerEndTime.Size = new System.Drawing.Size(200, 20);
            this.dateTimePickerEndTime.TabIndex = 32;
            this.dateTimePickerEndTime.ValueChanged += new System.EventHandler(this.dateTimePickerEndTime_ValueChanged);
            // 
            // dateTimePickerEndDate
            // 
            this.dateTimePickerEndDate.CustomFormat = "";
            this.dateTimePickerEndDate.Location = new System.Drawing.Point(9, 60);
            this.dateTimePickerEndDate.MinDate = new System.DateTime(2014, 1, 1, 0, 0, 0, 0);
            this.dateTimePickerEndDate.Name = "dateTimePickerEndDate";
            this.dateTimePickerEndDate.Size = new System.Drawing.Size(200, 20);
            this.dateTimePickerEndDate.TabIndex = 30;
            this.dateTimePickerEndDate.ValueChanged += new System.EventHandler(this.dateTimePickerEndDate_ValueChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBoxStartDate);
            this.groupBox2.Controls.Add(this.dateTimePickerStartTime);
            this.groupBox2.Controls.Add(this.dateTimePickerStartDate);
            this.groupBox2.Location = new System.Drawing.Point(12, 154);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(220, 180);
            this.groupBox2.TabIndex = 38;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Start date/time";
            // 
            // checkBoxStartDate
            // 
            this.checkBoxStartDate.AutoSize = true;
            this.checkBoxStartDate.Location = new System.Drawing.Point(10, 26);
            this.checkBoxStartDate.Name = "checkBoxStartDate";
            this.checkBoxStartDate.Size = new System.Drawing.Size(144, 17);
            this.checkBoxStartDate.TabIndex = 40;
            this.checkBoxStartDate.Text = "Specify a start date/time:";
            this.checkBoxStartDate.UseVisualStyleBackColor = true;
            this.checkBoxStartDate.CheckedChanged += new System.EventHandler(this.checkBoxStartDate_CheckedChanged_1);
            // 
            // dateTimePickerStartTime
            // 
            this.dateTimePickerStartTime.CustomFormat = "";
            this.dateTimePickerStartTime.Enabled = false;
            this.dateTimePickerStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePickerStartTime.Location = new System.Drawing.Point(11, 86);
            this.dateTimePickerStartTime.MinDate = new System.DateTime(2014, 1, 1, 0, 0, 0, 0);
            this.dateTimePickerStartTime.Name = "dateTimePickerStartTime";
            this.dateTimePickerStartTime.ShowUpDown = true;
            this.dateTimePickerStartTime.Size = new System.Drawing.Size(200, 20);
            this.dateTimePickerStartTime.TabIndex = 39;
            this.dateTimePickerStartTime.ValueChanged += new System.EventHandler(this.dateTimePickerStartTime_ValueChanged);
            // 
            // dateTimePickerStartDate
            // 
            this.dateTimePickerStartDate.Enabled = false;
            this.dateTimePickerStartDate.Location = new System.Drawing.Point(11, 60);
            this.dateTimePickerStartDate.Name = "dateTimePickerStartDate";
            this.dateTimePickerStartDate.Size = new System.Drawing.Size(200, 20);
            this.dateTimePickerStartDate.TabIndex = 37;
            this.dateTimePickerStartDate.ValueChanged += new System.EventHandler(this.dateTimePickerStartDate_ValueChanged);
            // 
            // labelAssetName
            // 
            this.labelAssetName.Location = new System.Drawing.Point(13, 13);
            this.labelAssetName.Name = "labelAssetName";
            this.labelAssetName.Size = new System.Drawing.Size(452, 13);
            this.labelAssetName.TabIndex = 42;
            this.labelAssetName.Text = "label2";
            // 
            // radioButtonOrigin
            // 
            this.radioButtonOrigin.AutoSize = true;
            this.radioButtonOrigin.Checked = true;
            this.radioButtonOrigin.Location = new System.Drawing.Point(35, 29);
            this.radioButtonOrigin.Name = "radioButtonOrigin";
            this.radioButtonOrigin.Size = new System.Drawing.Size(205, 17);
            this.radioButtonOrigin.TabIndex = 44;
            this.radioButtonOrigin.TabStop = true;
            this.radioButtonOrigin.Text = "Streaming locator (adaptive streaming)";
            this.radioButtonOrigin.UseVisualStyleBackColor = true;
            // 
            // radioButtonSAS
            // 
            this.radioButtonSAS.AutoSize = true;
            this.radioButtonSAS.Location = new System.Drawing.Point(35, 53);
            this.radioButtonSAS.Name = "radioButtonSAS";
            this.radioButtonSAS.Size = new System.Drawing.Size(193, 17);
            this.radioButtonSAS.TabIndex = 46;
            this.radioButtonSAS.Text = "SAS locator (progressive download)";
            this.radioButtonSAS.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.pictureBox2);
            this.groupBox4.Controls.Add(this.pictureBox1);
            this.groupBox4.Controls.Add(this.radioButtonSAS);
            this.groupBox4.Controls.Add(this.radioButtonOrigin);
            this.groupBox4.Location = new System.Drawing.Point(12, 48);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(453, 86);
            this.groupBox4.TabIndex = 43;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Locator type";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::AMSExplorer.Bitmaps.SAS_locator;
            this.pictureBox2.Location = new System.Drawing.Point(9, 54);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(16, 16);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 50;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::AMSExplorer.Bitmaps.streaming_locator;
            this.pictureBox1.Location = new System.Drawing.Point(9, 30);
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
            this.labelWarning.Location = new System.Drawing.Point(32, 369);
            this.labelWarning.Name = "labelWarning";
            this.labelWarning.Size = new System.Drawing.Size(47, 13);
            this.labelWarning.TabIndex = 44;
            this.labelWarning.Text = "Warning";
            // 
            // CreateLocator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(490, 441);
            this.Controls.Add(this.labelWarning);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.labelAssetName);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.buttonCancel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CreateLocator";
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
    }
}