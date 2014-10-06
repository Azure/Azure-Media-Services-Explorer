namespace AMSExplorer
{
    partial class PlayReadyLicense
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlayReadyLicense));
            this.moreinfocompliance = new System.Windows.Forms.LinkLabel();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.textBoxPolicyName = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.numericUpDownFPExpMinutes = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.numericUpDownFPExpHours = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownFPExpDays = new System.Windows.Forms.NumericUpDown();
            this.checkBoxFPExp = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.comboBoxType = new System.Windows.Forms.ComboBox();
            this.checkBoxEndDate = new System.Windows.Forms.CheckBox();
            this.dateTimePickerEndTime = new System.Windows.Forms.DateTimePicker();
            this.checkBoxStartDate = new System.Windows.Forms.CheckBox();
            this.dateTimePickerEndDate = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerStartTime = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerStartDate = new System.Windows.Forms.DateTimePicker();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.numericUpDownAnalogVideoOPL = new System.Windows.Forms.NumericUpDown();
            this.checkBoxAnalogVideoOPL = new System.Windows.Forms.CheckBox();
            this.numericUpDownUncompressedDigitalVideoOPL = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownUncompressedDigitalAudioOPL = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownCompressedDigitalVideoOPL = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownCompressedDigitalAudioOPL = new System.Windows.Forms.NumericUpDown();
            this.checkBoxAllowTestDevices = new System.Windows.Forms.CheckBox();
            this.checkBoxUncompressedDigitalVideoOPL = new System.Windows.Forms.CheckBox();
            this.checkBoxUncompressedDigitalAudioOPL = new System.Windows.Forms.CheckBox();
            this.checkBoxCompressedDigitalVideoOPL = new System.Windows.Forms.CheckBox();
            this.checkBoxCompressedDigitalAudioOPL = new System.Windows.Forms.CheckBox();
            this.checkBoxImageConstraintForAnalogComputerMonitorRestriction = new System.Windows.Forms.CheckBox();
            this.checkBoxImageConstraintForAnalogComponentVideoRestriction = new System.Windows.Forms.CheckBox();
            this.checkBoxDigitalVideoOnlyContentRestriction = new System.Windows.Forms.CheckBox();
            this.labelAllowPassingVideoContentToUnknownOuput = new System.Windows.Forms.Label();
            this.comboBoxAllowPassingVideoContentUnknownOutput = new System.Windows.Forms.ComboBox();
            this.buttonOk = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.labelWarning = new System.Windows.Forms.Label();
            this.groupBoxTimeValues = new System.Windows.Forms.GroupBox();
            this.tabControl1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFPExpMinutes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFPExpHours)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFPExpDays)).BeginInit();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAnalogVideoOPL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownUncompressedDigitalVideoOPL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownUncompressedDigitalAudioOPL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCompressedDigitalVideoOPL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCompressedDigitalAudioOPL)).BeginInit();
            this.groupBoxTimeValues.SuspendLayout();
            this.SuspendLayout();
            // 
            // moreinfocompliance
            // 
            this.moreinfocompliance.AutoSize = true;
            this.moreinfocompliance.Location = new System.Drawing.Point(276, 30);
            this.moreinfocompliance.Name = "moreinfocompliance";
            this.moreinfocompliance.Size = new System.Drawing.Size(215, 13);
            this.moreinfocompliance.TabIndex = 19;
            this.moreinfocompliance.TabStop = true;
            this.moreinfocompliance.Text = "PlayReady compliance and robustness rules";
            this.moreinfocompliance.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.moreinfotestserver_LinkClicked);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(267, 488);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(113, 32);
            this.buttonCancel.TabIndex = 16;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(23, 64);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(468, 376);
            this.tabControl1.TabIndex = 33;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.groupBoxTimeValues);
            this.tabPage3.Controls.Add(this.textBoxPolicyName);
            this.tabPage3.Controls.Add(this.label13);
            this.tabPage3.Controls.Add(this.label8);
            this.tabPage3.Controls.Add(this.comboBoxType);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(460, 350);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Common settings";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // textBoxPolicyName
            // 
            this.textBoxPolicyName.Location = new System.Drawing.Point(12, 32);
            this.textBoxPolicyName.Name = "textBoxPolicyName";
            this.textBoxPolicyName.Size = new System.Drawing.Size(132, 20);
            this.textBoxPolicyName.TabIndex = 55;
            this.textBoxPolicyName.TextChanged += new System.EventHandler(this.value_SelectedIndexChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(9, 16);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(192, 13);
            this.label13.TabIndex = 54;
            this.label13.Text = "Content key authorization policy name :";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(132, 152);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(44, 13);
            this.label11.TabIndex = 51;
            this.label11.Text = "Minutes";
            // 
            // numericUpDownFPExpMinutes
            // 
            this.numericUpDownFPExpMinutes.Enabled = false;
            this.numericUpDownFPExpMinutes.Location = new System.Drawing.Point(135, 168);
            this.numericUpDownFPExpMinutes.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.numericUpDownFPExpMinutes.Name = "numericUpDownFPExpMinutes";
            this.numericUpDownFPExpMinutes.Size = new System.Drawing.Size(47, 20);
            this.numericUpDownFPExpMinutes.TabIndex = 49;
            this.numericUpDownFPExpMinutes.ValueChanged += new System.EventHandler(this.value_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(79, 152);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(35, 13);
            this.label10.TabIndex = 48;
            this.label10.Text = "Hours";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(26, 152);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(31, 13);
            this.label9.TabIndex = 47;
            this.label9.Text = "Days";
            // 
            // numericUpDownFPExpHours
            // 
            this.numericUpDownFPExpHours.Enabled = false;
            this.numericUpDownFPExpHours.Location = new System.Drawing.Point(82, 168);
            this.numericUpDownFPExpHours.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.numericUpDownFPExpHours.Name = "numericUpDownFPExpHours";
            this.numericUpDownFPExpHours.Size = new System.Drawing.Size(47, 20);
            this.numericUpDownFPExpHours.TabIndex = 46;
            this.numericUpDownFPExpHours.ValueChanged += new System.EventHandler(this.value_SelectedIndexChanged);
            // 
            // numericUpDownFPExpDays
            // 
            this.numericUpDownFPExpDays.Enabled = false;
            this.numericUpDownFPExpDays.Location = new System.Drawing.Point(29, 168);
            this.numericUpDownFPExpDays.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownFPExpDays.Name = "numericUpDownFPExpDays";
            this.numericUpDownFPExpDays.Size = new System.Drawing.Size(47, 20);
            this.numericUpDownFPExpDays.TabIndex = 45;
            this.numericUpDownFPExpDays.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDownFPExpDays.ValueChanged += new System.EventHandler(this.value_SelectedIndexChanged);
            // 
            // checkBoxFPExp
            // 
            this.checkBoxFPExp.AutoSize = true;
            this.checkBoxFPExp.Location = new System.Drawing.Point(7, 132);
            this.checkBoxFPExp.Name = "checkBoxFPExp";
            this.checkBoxFPExp.Size = new System.Drawing.Size(165, 17);
            this.checkBoxFPExp.TabIndex = 44;
            this.checkBoxFPExp.Text = "Specify a first play expiration :";
            this.checkBoxFPExp.UseVisualStyleBackColor = true;
            this.checkBoxFPExp.CheckedChanged += new System.EventHandler(this.checkBoxFPExp_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 64);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(73, 13);
            this.label8.TabIndex = 42;
            this.label8.Text = "License type :";
            // 
            // comboBoxType
            // 
            this.comboBoxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxType.FormattingEnabled = true;
            this.comboBoxType.Location = new System.Drawing.Point(11, 80);
            this.comboBoxType.Name = "comboBoxType";
            this.comboBoxType.Size = new System.Drawing.Size(133, 21);
            this.comboBoxType.TabIndex = 41;
            this.comboBoxType.SelectedIndexChanged += new System.EventHandler(this.comboBoxType_SelectedIndexChanged);
            // 
            // checkBoxEndDate
            // 
            this.checkBoxEndDate.AutoSize = true;
            this.checkBoxEndDate.Location = new System.Drawing.Point(227, 28);
            this.checkBoxEndDate.Name = "checkBoxEndDate";
            this.checkBoxEndDate.Size = new System.Drawing.Size(142, 17);
            this.checkBoxEndDate.TabIndex = 40;
            this.checkBoxEndDate.Text = "Specify a end date/time:";
            this.checkBoxEndDate.UseVisualStyleBackColor = true;
            this.checkBoxEndDate.CheckedChanged += new System.EventHandler(this.checkBoxEndDate_CheckedChanged);
            // 
            // dateTimePickerEndTime
            // 
            this.dateTimePickerEndTime.CustomFormat = "";
            this.dateTimePickerEndTime.Enabled = false;
            this.dateTimePickerEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePickerEndTime.Location = new System.Drawing.Point(228, 77);
            this.dateTimePickerEndTime.MinDate = new System.DateTime(2014, 1, 1, 0, 0, 0, 0);
            this.dateTimePickerEndTime.Name = "dateTimePickerEndTime";
            this.dateTimePickerEndTime.ShowUpDown = true;
            this.dateTimePickerEndTime.Size = new System.Drawing.Size(200, 20);
            this.dateTimePickerEndTime.TabIndex = 39;
            this.dateTimePickerEndTime.ValueChanged += new System.EventHandler(this.dateTimePickerEndTime_ValueChanged);
            // 
            // checkBoxStartDate
            // 
            this.checkBoxStartDate.AutoSize = true;
            this.checkBoxStartDate.Location = new System.Drawing.Point(6, 28);
            this.checkBoxStartDate.Name = "checkBoxStartDate";
            this.checkBoxStartDate.Size = new System.Drawing.Size(144, 17);
            this.checkBoxStartDate.TabIndex = 40;
            this.checkBoxStartDate.Text = "Specify a start date/time:";
            this.checkBoxStartDate.UseVisualStyleBackColor = true;
            this.checkBoxStartDate.CheckedChanged += new System.EventHandler(this.checkBoxStartDate_CheckedChanged);
            // 
            // dateTimePickerEndDate
            // 
            this.dateTimePickerEndDate.Enabled = false;
            this.dateTimePickerEndDate.Location = new System.Drawing.Point(228, 51);
            this.dateTimePickerEndDate.Name = "dateTimePickerEndDate";
            this.dateTimePickerEndDate.Size = new System.Drawing.Size(200, 20);
            this.dateTimePickerEndDate.TabIndex = 37;
            this.dateTimePickerEndDate.ValueChanged += new System.EventHandler(this.dateTimePickerEndDate_ValueChanged);
            // 
            // dateTimePickerStartTime
            // 
            this.dateTimePickerStartTime.CustomFormat = "";
            this.dateTimePickerStartTime.Enabled = false;
            this.dateTimePickerStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePickerStartTime.Location = new System.Drawing.Point(7, 77);
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
            this.dateTimePickerStartDate.Location = new System.Drawing.Point(7, 51);
            this.dateTimePickerStartDate.Name = "dateTimePickerStartDate";
            this.dateTimePickerStartDate.Size = new System.Drawing.Size(200, 20);
            this.dateTimePickerStartDate.TabIndex = 37;
            this.dateTimePickerStartDate.ValueChanged += new System.EventHandler(this.dateTimePickerStartDate_ValueChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.numericUpDownAnalogVideoOPL);
            this.tabPage1.Controls.Add(this.checkBoxAnalogVideoOPL);
            this.tabPage1.Controls.Add(this.numericUpDownUncompressedDigitalVideoOPL);
            this.tabPage1.Controls.Add(this.numericUpDownUncompressedDigitalAudioOPL);
            this.tabPage1.Controls.Add(this.numericUpDownCompressedDigitalVideoOPL);
            this.tabPage1.Controls.Add(this.numericUpDownCompressedDigitalAudioOPL);
            this.tabPage1.Controls.Add(this.checkBoxAllowTestDevices);
            this.tabPage1.Controls.Add(this.checkBoxUncompressedDigitalVideoOPL);
            this.tabPage1.Controls.Add(this.checkBoxUncompressedDigitalAudioOPL);
            this.tabPage1.Controls.Add(this.checkBoxCompressedDigitalVideoOPL);
            this.tabPage1.Controls.Add(this.checkBoxCompressedDigitalAudioOPL);
            this.tabPage1.Controls.Add(this.checkBoxImageConstraintForAnalogComputerMonitorRestriction);
            this.tabPage1.Controls.Add(this.checkBoxImageConstraintForAnalogComponentVideoRestriction);
            this.tabPage1.Controls.Add(this.checkBoxDigitalVideoOnlyContentRestriction);
            this.tabPage1.Controls.Add(this.labelAllowPassingVideoContentToUnknownOuput);
            this.tabPage1.Controls.Add(this.comboBoxAllowPassingVideoContentUnknownOutput);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(460, 350);
            this.tabPage1.TabIndex = 3;
            this.tabPage1.Text = "Advanced settings";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // numericUpDownAnalogVideoOPL
            // 
            this.numericUpDownAnalogVideoOPL.Enabled = false;
            this.numericUpDownAnalogVideoOPL.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericUpDownAnalogVideoOPL.Location = new System.Drawing.Point(225, 266);
            this.numericUpDownAnalogVideoOPL.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownAnalogVideoOPL.Name = "numericUpDownAnalogVideoOPL";
            this.numericUpDownAnalogVideoOPL.Size = new System.Drawing.Size(91, 20);
            this.numericUpDownAnalogVideoOPL.TabIndex = 70;
            this.numericUpDownAnalogVideoOPL.ValueChanged += new System.EventHandler(this.value_SelectedIndexChanged);
            // 
            // checkBoxAnalogVideoOPL
            // 
            this.checkBoxAnalogVideoOPL.AutoSize = true;
            this.checkBoxAnalogVideoOPL.Location = new System.Drawing.Point(19, 267);
            this.checkBoxAnalogVideoOPL.Name = "checkBoxAnalogVideoOPL";
            this.checkBoxAnalogVideoOPL.Size = new System.Drawing.Size(119, 17);
            this.checkBoxAnalogVideoOPL.TabIndex = 69;
            this.checkBoxAnalogVideoOPL.Text = "Analog Video OPL :";
            this.checkBoxAnalogVideoOPL.UseVisualStyleBackColor = true;
            this.checkBoxAnalogVideoOPL.CheckedChanged += new System.EventHandler(this.checkBoxAnalogVideoOPL_CheckedChanged);
            // 
            // numericUpDownUncompressedDigitalVideoOPL
            // 
            this.numericUpDownUncompressedDigitalVideoOPL.Enabled = false;
            this.numericUpDownUncompressedDigitalVideoOPL.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericUpDownUncompressedDigitalVideoOPL.Location = new System.Drawing.Point(225, 240);
            this.numericUpDownUncompressedDigitalVideoOPL.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownUncompressedDigitalVideoOPL.Name = "numericUpDownUncompressedDigitalVideoOPL";
            this.numericUpDownUncompressedDigitalVideoOPL.Size = new System.Drawing.Size(91, 20);
            this.numericUpDownUncompressedDigitalVideoOPL.TabIndex = 68;
            this.numericUpDownUncompressedDigitalVideoOPL.ValueChanged += new System.EventHandler(this.value_SelectedIndexChanged);
            // 
            // numericUpDownUncompressedDigitalAudioOPL
            // 
            this.numericUpDownUncompressedDigitalAudioOPL.Enabled = false;
            this.numericUpDownUncompressedDigitalAudioOPL.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericUpDownUncompressedDigitalAudioOPL.Location = new System.Drawing.Point(225, 214);
            this.numericUpDownUncompressedDigitalAudioOPL.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownUncompressedDigitalAudioOPL.Name = "numericUpDownUncompressedDigitalAudioOPL";
            this.numericUpDownUncompressedDigitalAudioOPL.Size = new System.Drawing.Size(91, 20);
            this.numericUpDownUncompressedDigitalAudioOPL.TabIndex = 67;
            this.numericUpDownUncompressedDigitalAudioOPL.ValueChanged += new System.EventHandler(this.value_SelectedIndexChanged);
            // 
            // numericUpDownCompressedDigitalVideoOPL
            // 
            this.numericUpDownCompressedDigitalVideoOPL.Enabled = false;
            this.numericUpDownCompressedDigitalVideoOPL.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericUpDownCompressedDigitalVideoOPL.Location = new System.Drawing.Point(225, 188);
            this.numericUpDownCompressedDigitalVideoOPL.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownCompressedDigitalVideoOPL.Name = "numericUpDownCompressedDigitalVideoOPL";
            this.numericUpDownCompressedDigitalVideoOPL.Size = new System.Drawing.Size(91, 20);
            this.numericUpDownCompressedDigitalVideoOPL.TabIndex = 66;
            this.numericUpDownCompressedDigitalVideoOPL.ValueChanged += new System.EventHandler(this.value_SelectedIndexChanged);
            // 
            // numericUpDownCompressedDigitalAudioOPL
            // 
            this.numericUpDownCompressedDigitalAudioOPL.Enabled = false;
            this.numericUpDownCompressedDigitalAudioOPL.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericUpDownCompressedDigitalAudioOPL.Location = new System.Drawing.Point(225, 162);
            this.numericUpDownCompressedDigitalAudioOPL.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownCompressedDigitalAudioOPL.Name = "numericUpDownCompressedDigitalAudioOPL";
            this.numericUpDownCompressedDigitalAudioOPL.Size = new System.Drawing.Size(91, 20);
            this.numericUpDownCompressedDigitalAudioOPL.TabIndex = 65;
            this.numericUpDownCompressedDigitalAudioOPL.ValueChanged += new System.EventHandler(this.value_SelectedIndexChanged);
            // 
            // checkBoxAllowTestDevices
            // 
            this.checkBoxAllowTestDevices.AutoSize = true;
            this.checkBoxAllowTestDevices.Location = new System.Drawing.Point(19, 314);
            this.checkBoxAllowTestDevices.Name = "checkBoxAllowTestDevices";
            this.checkBoxAllowTestDevices.Size = new System.Drawing.Size(111, 17);
            this.checkBoxAllowTestDevices.TabIndex = 43;
            this.checkBoxAllowTestDevices.Text = "Allow test devices";
            this.checkBoxAllowTestDevices.UseVisualStyleBackColor = true;
            this.checkBoxAllowTestDevices.CheckedChanged += new System.EventHandler(this.value_SelectedIndexChanged);
            // 
            // checkBoxUncompressedDigitalVideoOPL
            // 
            this.checkBoxUncompressedDigitalVideoOPL.AutoSize = true;
            this.checkBoxUncompressedDigitalVideoOPL.Location = new System.Drawing.Point(19, 241);
            this.checkBoxUncompressedDigitalVideoOPL.Name = "checkBoxUncompressedDigitalVideoOPL";
            this.checkBoxUncompressedDigitalVideoOPL.Size = new System.Drawing.Size(189, 17);
            this.checkBoxUncompressedDigitalVideoOPL.TabIndex = 64;
            this.checkBoxUncompressedDigitalVideoOPL.Text = "Uncompressed Digital Video OPL :";
            this.checkBoxUncompressedDigitalVideoOPL.UseVisualStyleBackColor = true;
            this.checkBoxUncompressedDigitalVideoOPL.CheckedChanged += new System.EventHandler(this.checkBoxUncompressedDigitalVideoOPL_CheckedChanged);
            // 
            // checkBoxUncompressedDigitalAudioOPL
            // 
            this.checkBoxUncompressedDigitalAudioOPL.AutoSize = true;
            this.checkBoxUncompressedDigitalAudioOPL.Location = new System.Drawing.Point(19, 215);
            this.checkBoxUncompressedDigitalAudioOPL.Name = "checkBoxUncompressedDigitalAudioOPL";
            this.checkBoxUncompressedDigitalAudioOPL.Size = new System.Drawing.Size(189, 17);
            this.checkBoxUncompressedDigitalAudioOPL.TabIndex = 62;
            this.checkBoxUncompressedDigitalAudioOPL.Text = "Uncompressed Digital Audio OPL :";
            this.checkBoxUncompressedDigitalAudioOPL.UseVisualStyleBackColor = true;
            this.checkBoxUncompressedDigitalAudioOPL.CheckedChanged += new System.EventHandler(this.checkBoxUncompressedDigitalAudioOPL_CheckedChanged);
            // 
            // checkBoxCompressedDigitalVideoOPL
            // 
            this.checkBoxCompressedDigitalVideoOPL.AutoSize = true;
            this.checkBoxCompressedDigitalVideoOPL.Location = new System.Drawing.Point(19, 189);
            this.checkBoxCompressedDigitalVideoOPL.Name = "checkBoxCompressedDigitalVideoOPL";
            this.checkBoxCompressedDigitalVideoOPL.Size = new System.Drawing.Size(176, 17);
            this.checkBoxCompressedDigitalVideoOPL.TabIndex = 60;
            this.checkBoxCompressedDigitalVideoOPL.Text = "Compressed Digital Video OPL :";
            this.checkBoxCompressedDigitalVideoOPL.UseVisualStyleBackColor = true;
            this.checkBoxCompressedDigitalVideoOPL.CheckedChanged += new System.EventHandler(this.checkBoxCompressedDigitalVideoOPL_CheckedChanged);
            // 
            // checkBoxCompressedDigitalAudioOPL
            // 
            this.checkBoxCompressedDigitalAudioOPL.AutoSize = true;
            this.checkBoxCompressedDigitalAudioOPL.Location = new System.Drawing.Point(19, 163);
            this.checkBoxCompressedDigitalAudioOPL.Name = "checkBoxCompressedDigitalAudioOPL";
            this.checkBoxCompressedDigitalAudioOPL.Size = new System.Drawing.Size(176, 17);
            this.checkBoxCompressedDigitalAudioOPL.TabIndex = 58;
            this.checkBoxCompressedDigitalAudioOPL.Text = "Compressed Digital Audio OPL :";
            this.checkBoxCompressedDigitalAudioOPL.UseVisualStyleBackColor = true;
            this.checkBoxCompressedDigitalAudioOPL.CheckedChanged += new System.EventHandler(this.checkBoxCompressedDigitalAudioOPL_CheckedChanged);
            // 
            // checkBoxImageConstraintForAnalogComputerMonitorRestriction
            // 
            this.checkBoxImageConstraintForAnalogComputerMonitorRestriction.AutoSize = true;
            this.checkBoxImageConstraintForAnalogComputerMonitorRestriction.Location = new System.Drawing.Point(19, 137);
            this.checkBoxImageConstraintForAnalogComputerMonitorRestriction.Name = "checkBoxImageConstraintForAnalogComputerMonitorRestriction";
            this.checkBoxImageConstraintForAnalogComputerMonitorRestriction.Size = new System.Drawing.Size(298, 17);
            this.checkBoxImageConstraintForAnalogComputerMonitorRestriction.TabIndex = 47;
            this.checkBoxImageConstraintForAnalogComputerMonitorRestriction.Text = "Image Constraint For Analog Computer Monitor Restriction";
            this.checkBoxImageConstraintForAnalogComputerMonitorRestriction.UseVisualStyleBackColor = true;
            this.checkBoxImageConstraintForAnalogComputerMonitorRestriction.CheckedChanged += new System.EventHandler(this.value_SelectedIndexChanged);
            // 
            // checkBoxImageConstraintForAnalogComponentVideoRestriction
            // 
            this.checkBoxImageConstraintForAnalogComponentVideoRestriction.AutoSize = true;
            this.checkBoxImageConstraintForAnalogComponentVideoRestriction.Location = new System.Drawing.Point(19, 111);
            this.checkBoxImageConstraintForAnalogComponentVideoRestriction.Name = "checkBoxImageConstraintForAnalogComponentVideoRestriction";
            this.checkBoxImageConstraintForAnalogComponentVideoRestriction.Size = new System.Drawing.Size(299, 17);
            this.checkBoxImageConstraintForAnalogComponentVideoRestriction.TabIndex = 46;
            this.checkBoxImageConstraintForAnalogComponentVideoRestriction.Text = "Image Constraint For Analog Component Video Restriction";
            this.checkBoxImageConstraintForAnalogComponentVideoRestriction.UseVisualStyleBackColor = true;
            this.checkBoxImageConstraintForAnalogComponentVideoRestriction.CheckedChanged += new System.EventHandler(this.value_SelectedIndexChanged);
            // 
            // checkBoxDigitalVideoOnlyContentRestriction
            // 
            this.checkBoxDigitalVideoOnlyContentRestriction.AutoSize = true;
            this.checkBoxDigitalVideoOnlyContentRestriction.Location = new System.Drawing.Point(19, 85);
            this.checkBoxDigitalVideoOnlyContentRestriction.Name = "checkBoxDigitalVideoOnlyContentRestriction";
            this.checkBoxDigitalVideoOnlyContentRestriction.Size = new System.Drawing.Size(202, 17);
            this.checkBoxDigitalVideoOnlyContentRestriction.TabIndex = 45;
            this.checkBoxDigitalVideoOnlyContentRestriction.Text = "Digital Video Only Content Restriction";
            this.checkBoxDigitalVideoOnlyContentRestriction.UseVisualStyleBackColor = true;
            this.checkBoxDigitalVideoOnlyContentRestriction.CheckedChanged += new System.EventHandler(this.value_SelectedIndexChanged);
            // 
            // labelAllowPassingVideoContentToUnknownOuput
            // 
            this.labelAllowPassingVideoContentToUnknownOuput.AutoSize = true;
            this.labelAllowPassingVideoContentToUnknownOuput.Location = new System.Drawing.Point(16, 23);
            this.labelAllowPassingVideoContentToUnknownOuput.Name = "labelAllowPassingVideoContentToUnknownOuput";
            this.labelAllowPassingVideoContentToUnknownOuput.Size = new System.Drawing.Size(226, 13);
            this.labelAllowPassingVideoContentToUnknownOuput.TabIndex = 44;
            this.labelAllowPassingVideoContentToUnknownOuput.Text = "Allow passing Video content to unknow output";
            // 
            // comboBoxAllowPassingVideoContentUnknownOutput
            // 
            this.comboBoxAllowPassingVideoContentUnknownOutput.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAllowPassingVideoContentUnknownOutput.FormattingEnabled = true;
            this.comboBoxAllowPassingVideoContentUnknownOutput.Location = new System.Drawing.Point(17, 39);
            this.comboBoxAllowPassingVideoContentUnknownOutput.Name = "comboBoxAllowPassingVideoContentUnknownOutput";
            this.comboBoxAllowPassingVideoContentUnknownOutput.Size = new System.Drawing.Size(178, 21);
            this.comboBoxAllowPassingVideoContentUnknownOutput.TabIndex = 43;
            this.comboBoxAllowPassingVideoContentUnknownOutput.SelectedIndexChanged += new System.EventHandler(this.value_SelectedIndexChanged);
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Image = global::AMSExplorer.Bitmaps.DRM_protection;
            this.buttonOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonOk.Location = new System.Drawing.Point(100, 488);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(146, 32);
            this.buttonOk.TabIndex = 17;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(229, 13);
            this.label1.TabIndex = 34;
            this.label1.Text = "Please specify the PlayReady template settings";
            // 
            // labelWarning
            // 
            this.labelWarning.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelWarning.AutoSize = true;
            this.labelWarning.ForeColor = System.Drawing.Color.Red;
            this.labelWarning.Location = new System.Drawing.Point(24, 456);
            this.labelWarning.Name = "labelWarning";
            this.labelWarning.Size = new System.Drawing.Size(47, 13);
            this.labelWarning.TabIndex = 45;
            this.labelWarning.Text = "Warning";
            // 
            // groupBoxTimeValues
            // 
            this.groupBoxTimeValues.Controls.Add(this.checkBoxStartDate);
            this.groupBoxTimeValues.Controls.Add(this.dateTimePickerStartDate);
            this.groupBoxTimeValues.Controls.Add(this.dateTimePickerStartTime);
            this.groupBoxTimeValues.Controls.Add(this.label11);
            this.groupBoxTimeValues.Controls.Add(this.checkBoxFPExp);
            this.groupBoxTimeValues.Controls.Add(this.checkBoxEndDate);
            this.groupBoxTimeValues.Controls.Add(this.dateTimePickerEndTime);
            this.groupBoxTimeValues.Controls.Add(this.numericUpDownFPExpMinutes);
            this.groupBoxTimeValues.Controls.Add(this.dateTimePickerEndDate);
            this.groupBoxTimeValues.Controls.Add(this.numericUpDownFPExpDays);
            this.groupBoxTimeValues.Controls.Add(this.label10);
            this.groupBoxTimeValues.Controls.Add(this.numericUpDownFPExpHours);
            this.groupBoxTimeValues.Controls.Add(this.label9);
            this.groupBoxTimeValues.Location = new System.Drawing.Point(11, 107);
            this.groupBoxTimeValues.Name = "groupBoxTimeValues";
            this.groupBoxTimeValues.Size = new System.Drawing.Size(440, 231);
            this.groupBoxTimeValues.TabIndex = 56;
            this.groupBoxTimeValues.TabStop = false;
            // 
            // PlayReadyLicense
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(517, 532);
            this.Controls.Add(this.labelWarning);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.moreinfocompliance);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.buttonCancel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PlayReadyLicense";
            this.Text = "PlayReady License Definition";
            this.Load += new System.EventHandler(this.PlayReadyLicense_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFPExpMinutes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFPExpHours)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFPExpDays)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAnalogVideoOPL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownUncompressedDigitalVideoOPL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownUncompressedDigitalAudioOPL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCompressedDigitalVideoOPL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCompressedDigitalAudioOPL)).EndInit();
            this.groupBoxTimeValues.ResumeLayout(false);
            this.groupBoxTimeValues.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel moreinfocompliance;
        public System.Windows.Forms.Button buttonOk;
        public System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.CheckBox checkBoxEndDate;
        private System.Windows.Forms.DateTimePicker dateTimePickerEndTime;
        private System.Windows.Forms.CheckBox checkBoxStartDate;
        private System.Windows.Forms.DateTimePicker dateTimePickerEndDate;
        private System.Windows.Forms.DateTimePicker dateTimePickerStartTime;
        private System.Windows.Forms.DateTimePicker dateTimePickerStartDate;
        private System.Windows.Forms.ComboBox comboBoxType;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox checkBoxAllowTestDevices;
        private System.Windows.Forms.CheckBox checkBoxFPExp;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown numericUpDownFPExpMinutes;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown numericUpDownFPExpHours;
        private System.Windows.Forms.NumericUpDown numericUpDownFPExpDays;
        private System.Windows.Forms.TextBox textBoxPolicyName;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.CheckBox checkBoxUncompressedDigitalVideoOPL;
        private System.Windows.Forms.CheckBox checkBoxUncompressedDigitalAudioOPL;
        private System.Windows.Forms.CheckBox checkBoxCompressedDigitalVideoOPL;
        private System.Windows.Forms.CheckBox checkBoxCompressedDigitalAudioOPL;
        private System.Windows.Forms.CheckBox checkBoxImageConstraintForAnalogComputerMonitorRestriction;
        private System.Windows.Forms.CheckBox checkBoxImageConstraintForAnalogComponentVideoRestriction;
        private System.Windows.Forms.CheckBox checkBoxDigitalVideoOnlyContentRestriction;
        private System.Windows.Forms.Label labelAllowPassingVideoContentToUnknownOuput;
        private System.Windows.Forms.ComboBox comboBoxAllowPassingVideoContentUnknownOutput;
        private System.Windows.Forms.NumericUpDown numericUpDownUncompressedDigitalVideoOPL;
        private System.Windows.Forms.NumericUpDown numericUpDownUncompressedDigitalAudioOPL;
        private System.Windows.Forms.NumericUpDown numericUpDownCompressedDigitalVideoOPL;
        private System.Windows.Forms.NumericUpDown numericUpDownCompressedDigitalAudioOPL;
        private System.Windows.Forms.NumericUpDown numericUpDownAnalogVideoOPL;
        private System.Windows.Forms.CheckBox checkBoxAnalogVideoOPL;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelWarning;
        private System.Windows.Forms.GroupBox groupBoxTimeValues;
    }
}