namespace AMSExplorer
{
    partial class TimeRangeSelection
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtonEndNow = new System.Windows.Forms.RadioButton();
            this.radioButtonEndCustom = new System.Windows.Forms.RadioButton();
            this.dateTimePickerEndTime = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerEndDate = new System.Windows.Forms.DateTimePicker();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dateTimePickerStartTime = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerStartDate = new System.Windows.Forms.DateTimePicker();
            this.labelMain = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label49 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
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
            this.buttonOk.Text = "Set Range";
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButtonEndNow);
            this.groupBox1.Controls.Add(this.radioButtonEndCustom);
            this.groupBox1.Controls.Add(this.dateTimePickerEndTime);
            this.groupBox1.Controls.Add(this.dateTimePickerEndDate);
            this.groupBox1.Location = new System.Drawing.Point(293, 57);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(257, 208);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "End date/time";
            // 
            // radioButtonEndNow
            // 
            this.radioButtonEndNow.AutoSize = true;
            this.radioButtonEndNow.Location = new System.Drawing.Point(10, 148);
            this.radioButtonEndNow.Name = "radioButtonEndNow";
            this.radioButtonEndNow.Size = new System.Drawing.Size(50, 19);
            this.radioButtonEndNow.TabIndex = 3;
            this.radioButtonEndNow.Text = "Now";
            this.radioButtonEndNow.UseVisualStyleBackColor = true;
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
            this.groupBox2.Controls.Add(this.dateTimePickerStartTime);
            this.groupBox2.Controls.Add(this.dateTimePickerStartDate);
            this.groupBox2.Location = new System.Drawing.Point(14, 57);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(257, 208);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Start date/time";
            // 
            // dateTimePickerStartTime
            // 
            this.dateTimePickerStartTime.CustomFormat = "";
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
            this.dateTimePickerStartDate.Location = new System.Drawing.Point(13, 69);
            this.dateTimePickerStartDate.Name = "dateTimePickerStartDate";
            this.dateTimePickerStartDate.Size = new System.Drawing.Size(233, 23);
            this.dateTimePickerStartDate.TabIndex = 1;
            this.dateTimePickerStartDate.ValueChanged += new System.EventHandler(this.dateTimePickerStartDate_ValueChanged);
            // 
            // labelMain
            // 
            this.labelMain.Location = new System.Drawing.Point(15, 15);
            this.labelMain.Name = "labelMain";
            this.labelMain.Size = new System.Drawing.Size(527, 15);
            this.labelMain.TabIndex = 42;
            this.labelMain.Text = "label2";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonOk);
            this.panel1.Location = new System.Drawing.Point(-2, 299);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(575, 55);
            this.panel1.TabIndex = 60;
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label49.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label49.Location = new System.Drawing.Point(241, 273);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(89, 13);
            this.label49.TabIndex = 61;
            this.label49.Text = "Date/time is local";
            // 
            // TimeRangeSelection
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(572, 353);
            this.Controls.Add(this.label49);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labelMain);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "TimeRangeSelection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Set a date/time range";
            this.Load += new System.EventHandler(this.CreateLocator_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button buttonOk;
        public System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dateTimePickerEndTime;
        private System.Windows.Forms.DateTimePicker dateTimePickerEndDate;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DateTimePicker dateTimePickerStartTime;
        private System.Windows.Forms.DateTimePicker dateTimePickerStartDate;
        private System.Windows.Forms.Label labelMain;
        private System.Windows.Forms.RadioButton radioButtonEndNow;
        private System.Windows.Forms.RadioButton radioButtonEndCustom;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label49;
    }
}