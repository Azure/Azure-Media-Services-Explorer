
namespace AMSExplorer
{
    partial class StorageSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StorageSettings));
            this.label1 = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.comboBoxVersion = new System.Windows.Forms.ComboBox();
            this.moreinfoLiveStreamingProfilelink = new System.Windows.Forms.LinkLabel();
            this.comboBoxMetrics = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.linkLabelStorageAnalytics = new System.Windows.Forms.LinkLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numericUpDownRetention = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.labelStorageAccount = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxStorageId = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRetention)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonUpdate
            // 
            resources.ApplyResources(this.buttonUpdate, "buttonUpdate");
            this.buttonUpdate.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonUpdate);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // comboBoxVersion
            // 
            resources.ApplyResources(this.comboBoxVersion, "comboBoxVersion");
            this.comboBoxVersion.FormattingEnabled = true;
            this.comboBoxVersion.Name = "comboBoxVersion";
            // 
            // moreinfoLiveStreamingProfilelink
            // 
            resources.ApplyResources(this.moreinfoLiveStreamingProfilelink, "moreinfoLiveStreamingProfilelink");
            this.moreinfoLiveStreamingProfilelink.Name = "moreinfoLiveStreamingProfilelink";
            this.moreinfoLiveStreamingProfilelink.TabStop = true;
            this.moreinfoLiveStreamingProfilelink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.moreinfoLiveStreamingProfilelink_LinkClicked);
            // 
            // comboBoxMetrics
            // 
            resources.ApplyResources(this.comboBoxMetrics, "comboBoxMetrics");
            this.comboBoxMetrics.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMetrics.FormattingEnabled = true;
            this.comboBoxMetrics.Name = "comboBoxMetrics";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label8.Name = "label8";
            // 
            // linkLabelStorageAnalytics
            // 
            resources.ApplyResources(this.linkLabelStorageAnalytics, "linkLabelStorageAnalytics");
            this.linkLabelStorageAnalytics.Name = "linkLabelStorageAnalytics";
            this.linkLabelStorageAnalytics.TabStop = true;
            this.linkLabelStorageAnalytics.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.moreinfoLiveStreamingProfilelink_LinkClicked);
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.numericUpDownRetention);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.linkLabelStorageAnalytics);
            this.groupBox1.Controls.Add(this.comboBoxMetrics);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // numericUpDownRetention
            // 
            resources.ApplyResources(this.numericUpDownRetention, "numericUpDownRetention");
            this.numericUpDownRetention.Maximum = new decimal(new int[] {
            365,
            0,
            0,
            0});
            this.numericUpDownRetention.Name = "numericUpDownRetention";
            this.numericUpDownRetention.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // labelStorageAccount
            // 
            resources.ApplyResources(this.labelStorageAccount, "labelStorageAccount");
            this.labelStorageAccount.ForeColor = System.Drawing.Color.DarkBlue;
            this.labelStorageAccount.Name = "labelStorageAccount";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label4.Name = "label4";
            // 
            // textBoxStorageId
            // 
            resources.ApplyResources(this.textBoxStorageId, "textBoxStorageId");
            this.textBoxStorageId.Name = "textBoxStorageId";
            this.textBoxStorageId.ReadOnly = true;
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // StorageSettings
            // 
            this.AcceptButton = this.buttonUpdate;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxStorageId);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.labelStorageAccount);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.moreinfoLiveStreamingProfilelink);
            this.Controls.Add(this.comboBoxVersion);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Name = "StorageSettings";
            this.Load += new System.EventHandler(this.StorageVersion_Load);
            this.DpiChanged += new System.Windows.Forms.DpiChangedEventHandler(this.StorageSettings_DpiChanged);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRetention)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox comboBoxVersion;
        private System.Windows.Forms.LinkLabel moreinfoLiveStreamingProfilelink;
        private System.Windows.Forms.ComboBox comboBoxMetrics;
        public System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.LinkLabel linkLabelStorageAnalytics;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown numericUpDownRetention;
        public System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelStorageAccount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxStorageId;
        public System.Windows.Forms.Label label5;
    }
}