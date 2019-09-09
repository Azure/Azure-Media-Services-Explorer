namespace AMSExplorer
{
    partial class TimeControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TimeControl));
            this.label42 = new System.Windows.Forms.Label();
            this.numericUpDownSeconds = new System.Windows.Forms.NumericUpDown();
            this.label39 = new System.Windows.Forms.Label();
            this.numericUpDownMinutes = new System.Windows.Forms.NumericUpDown();
            this.label41 = new System.Windows.Forms.Label();
            this.numericUpDownHours = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownDays = new System.Windows.Forms.NumericUpDown();
            this.label40 = new System.Windows.Forms.Label();
            this.trackBarTime = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSeconds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinutes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHours)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDays)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarTime)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label42
            // 
            resources.ApplyResources(this.label42, "label42");
            this.label42.Name = "label42";
            // 
            // numericUpDownSeconds
            // 
            resources.ApplyResources(this.numericUpDownSeconds, "numericUpDownSeconds");
            this.numericUpDownSeconds.DecimalPlaces = 2;
            this.numericUpDownSeconds.Maximum = new decimal(new int[] {
            1610612735,
            1960709702,
            -1042360779,
            1769472});
            this.numericUpDownSeconds.Name = "numericUpDownSeconds";
            this.numericUpDownSeconds.ValueChanged += new System.EventHandler(this.HandleValueChanged);
            // 
            // label39
            // 
            resources.ApplyResources(this.label39, "label39");
            this.label39.Name = "label39";
            // 
            // numericUpDownMinutes
            // 
            resources.ApplyResources(this.numericUpDownMinutes, "numericUpDownMinutes");
            this.numericUpDownMinutes.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.numericUpDownMinutes.Name = "numericUpDownMinutes";
            this.numericUpDownMinutes.ValueChanged += new System.EventHandler(this.HandleValueChanged);
            // 
            // label41
            // 
            resources.ApplyResources(this.label41, "label41");
            this.label41.Name = "label41";
            // 
            // numericUpDownHours
            // 
            resources.ApplyResources(this.numericUpDownHours, "numericUpDownHours");
            this.numericUpDownHours.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.numericUpDownHours.Name = "numericUpDownHours";
            this.numericUpDownHours.ValueChanged += new System.EventHandler(this.HandleValueChanged);
            // 
            // numericUpDownDays
            // 
            resources.ApplyResources(this.numericUpDownDays, "numericUpDownDays");
            this.numericUpDownDays.Maximum = new decimal(new int[] {
            1874919424,
            2328306,
            0,
            0});
            this.numericUpDownDays.Name = "numericUpDownDays";
            this.numericUpDownDays.ValueChanged += new System.EventHandler(this.HandleValueChanged);
            // 
            // label40
            // 
            resources.ApplyResources(this.label40, "label40");
            this.label40.Name = "label40";
            // 
            // trackBarTime
            // 
            resources.ApplyResources(this.trackBarTime, "trackBarTime");
            this.trackBarTime.BackColor = System.Drawing.SystemColors.Window;
            this.tableLayoutPanel1.SetColumnSpan(this.trackBarTime, 5);
            this.trackBarTime.Maximum = 1000;
            this.trackBarTime.Name = "trackBarTime";
            this.trackBarTime.ValueChanged += new System.EventHandler(this.HandleTrackBarValueChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.numericUpDownSeconds, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.label42, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.numericUpDownMinutes, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.label41, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.numericUpDownHours, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.numericUpDownDays, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label39, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label40, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.trackBarTime, 0, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // TimeControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "TimeControl";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSeconds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinutes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHours)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDays)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarTime)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.NumericUpDown numericUpDownSeconds;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.NumericUpDown numericUpDownMinutes;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.NumericUpDown numericUpDownHours;
        private System.Windows.Forms.NumericUpDown numericUpDownDays;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.TrackBar trackBarTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
