using Microsoft.WindowsAzure.Storage.Shared.Protocol;

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
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRetention)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 15);
            this.label1.TabIndex = 38;
            this.label1.Text = "Default services version :";
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(344, 15);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(115, 27);
            this.buttonCancel.TabIndex = 40;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonUpdate.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonUpdate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonUpdate.Location = new System.Drawing.Point(218, 15);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(119, 27);
            this.buttonUpdate.TabIndex = 39;
            this.buttonUpdate.Text = "Update";
            this.buttonUpdate.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonUpdate);
            this.panel1.Location = new System.Drawing.Point(-5, 296);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(476, 55);
            this.panel1.TabIndex = 63;
            // 
            // comboBoxVersion
            // 
            this.comboBoxVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxVersion.FormattingEnabled = true;
            this.comboBoxVersion.Location = new System.Drawing.Point(20, 85);
            this.comboBoxVersion.Name = "comboBoxVersion";
            this.comboBoxVersion.Size = new System.Drawing.Size(434, 23);
            this.comboBoxVersion.TabIndex = 66;
            // 
            // moreinfoLiveStreamingProfilelink
            // 
            this.moreinfoLiveStreamingProfilelink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.moreinfoLiveStreamingProfilelink.AutoSize = true;
            this.moreinfoLiveStreamingProfilelink.Location = new System.Drawing.Point(238, 67);
            this.moreinfoLiveStreamingProfilelink.Name = "moreinfoLiveStreamingProfilelink";
            this.moreinfoLiveStreamingProfilelink.Size = new System.Drawing.Size(216, 15);
            this.moreinfoLiveStreamingProfilelink.TabIndex = 67;
            this.moreinfoLiveStreamingProfilelink.TabStop = true;
            this.moreinfoLiveStreamingProfilelink.Text = "Storage Services Versioning information";
            this.moreinfoLiveStreamingProfilelink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.moreinfoLiveStreamingProfilelink_LinkClicked);
            // 
            // comboBoxMetrics
            // 
            this.comboBoxMetrics.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxMetrics.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMetrics.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.comboBoxMetrics.FormattingEnabled = true;
            this.comboBoxMetrics.Location = new System.Drawing.Point(14, 44);
            this.comboBoxMetrics.Name = "comboBoxMetrics";
            this.comboBoxMetrics.Size = new System.Drawing.Size(413, 23);
            this.comboBoxMetrics.TabIndex = 70;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label3.Location = new System.Drawing.Point(11, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 15);
            this.label3.TabIndex = 69;
            this.label3.Text = "Level :";
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label8.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label8.Location = new System.Drawing.Point(140, 107);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(287, 18);
            this.label8.TabIndex = 71;
            this.label8.Text = "Put 0 for no retention, 5 is a common setting";
            // 
            // linkLabelStorageAnalytics
            // 
            this.linkLabelStorageAnalytics.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabelStorageAnalytics.AutoSize = true;
            this.linkLabelStorageAnalytics.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.linkLabelStorageAnalytics.Location = new System.Drawing.Point(287, 25);
            this.linkLabelStorageAnalytics.Name = "linkLabelStorageAnalytics";
            this.linkLabelStorageAnalytics.Size = new System.Drawing.Size(140, 15);
            this.linkLabelStorageAnalytics.TabIndex = 72;
            this.linkLabelStorageAnalytics.TabStop = true;
            this.linkLabelStorageAnalytics.Text = "Storage Analytics Metrics";
            this.linkLabelStorageAnalytics.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.moreinfoLiveStreamingProfilelink_LinkClicked);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.numericUpDownRetention);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.linkLabelStorageAnalytics);
            this.groupBox1.Controls.Add(this.comboBoxMetrics);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(19, 136);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(435, 143);
            this.groupBox1.TabIndex = 73;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Metrics";
            // 
            // numericUpDownRetention
            // 
            this.numericUpDownRetention.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.numericUpDownRetention.Location = new System.Drawing.Point(14, 105);
            this.numericUpDownRetention.Maximum = new decimal(new int[] {
            365,
            0,
            0,
            0});
            this.numericUpDownRetention.Name = "numericUpDownRetention";
            this.numericUpDownRetention.Size = new System.Drawing.Size(120, 23);
            this.numericUpDownRetention.TabIndex = 74;
            this.numericUpDownRetention.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label2.Location = new System.Drawing.Point(11, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 15);
            this.label2.TabIndex = 73;
            this.label2.Text = "Retention (days) :";
            // 
            // labelStorageAccount
            // 
            this.labelStorageAccount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelStorageAccount.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStorageAccount.ForeColor = System.Drawing.Color.DarkBlue;
            this.labelStorageAccount.Location = new System.Drawing.Point(15, 20);
            this.labelStorageAccount.Name = "labelStorageAccount";
            this.labelStorageAccount.Size = new System.Drawing.Size(431, 23);
            this.labelStorageAccount.TabIndex = 74;
            this.labelStorageAccount.Text = "Settings for storage account : {0}";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label4.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label4.Location = new System.Drawing.Point(213, 111);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(241, 18);
            this.label4.TabIndex = 75;
            this.label4.Text = "2011-08-18 or higher is recommended";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // StorageSettings
            // 
            this.AcceptButton = this.buttonUpdate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(469, 352);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.labelStorageAccount);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.moreinfoLiveStreamingProfilelink);
            this.Controls.Add(this.comboBoxVersion);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "StorageSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Azure Storage Settings";
            this.Load += new System.EventHandler(this.StorageVersion_Load);
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
    }
}