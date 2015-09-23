namespace AMSExplorer
{
    partial class StorageVersion
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
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxVersion = new System.Windows.Forms.TextBox();
            this.comboBoxVersion = new System.Windows.Forms.ComboBox();
            this.moreinfoLiveStreamingProfilelink = new System.Windows.Forms.LinkLabel();
            this.labelStorageAccount = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 132);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 15);
            this.label1.TabIndex = 38;
            this.label1.Text = "Select a version :";
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
            this.buttonUpdate.Location = new System.Drawing.Point(205, 15);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(132, 27);
            this.buttonUpdate.TabIndex = 39;
            this.buttonUpdate.Text = "Update Version";
            this.buttonUpdate.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonUpdate);
            this.panel1.Location = new System.Drawing.Point(-5, 200);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(476, 55);
            this.panel1.TabIndex = 63;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 15);
            this.label2.TabIndex = 64;
            this.label2.Text = "Current version :";
            // 
            // textBoxVersion
            // 
            this.textBoxVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxVersion.Location = new System.Drawing.Point(30, 89);
            this.textBoxVersion.Name = "textBoxVersion";
            this.textBoxVersion.ReadOnly = true;
            this.textBoxVersion.Size = new System.Drawing.Size(404, 23);
            this.textBoxVersion.TabIndex = 65;
            // 
            // comboBoxVersion
            // 
            this.comboBoxVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxVersion.FormattingEnabled = true;
            this.comboBoxVersion.Location = new System.Drawing.Point(33, 151);
            this.comboBoxVersion.Name = "comboBoxVersion";
            this.comboBoxVersion.Size = new System.Drawing.Size(401, 23);
            this.comboBoxVersion.TabIndex = 66;
            // 
            // moreinfoLiveStreamingProfilelink
            // 
            this.moreinfoLiveStreamingProfilelink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.moreinfoLiveStreamingProfilelink.AutoSize = true;
            this.moreinfoLiveStreamingProfilelink.Location = new System.Drawing.Point(218, 9);
            this.moreinfoLiveStreamingProfilelink.Name = "moreinfoLiveStreamingProfilelink";
            this.moreinfoLiveStreamingProfilelink.Size = new System.Drawing.Size(216, 15);
            this.moreinfoLiveStreamingProfilelink.TabIndex = 67;
            this.moreinfoLiveStreamingProfilelink.TabStop = true;
            this.moreinfoLiveStreamingProfilelink.Text = "Storage Services Versioning information";
            this.moreinfoLiveStreamingProfilelink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.moreinfoLiveStreamingProfilelink_LinkClicked);
            // 
            // labelStorageAccount
            // 
            this.labelStorageAccount.AutoSize = true;
            this.labelStorageAccount.Location = new System.Drawing.Point(30, 36);
            this.labelStorageAccount.Name = "labelStorageAccount";
            this.labelStorageAccount.Size = new System.Drawing.Size(116, 15);
            this.labelStorageAccount.TabIndex = 68;
            this.labelStorageAccount.Text = "Storage account : {0}";
            // 
            // StorageVersion
            // 
            this.AcceptButton = this.buttonUpdate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(469, 256);
            this.Controls.Add(this.labelStorageAccount);
            this.Controls.Add(this.moreinfoLiveStreamingProfilelink);
            this.Controls.Add(this.comboBoxVersion);
            this.Controls.Add(this.textBoxVersion);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "StorageVersion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Azure Storage Services Versioning";
            this.Load += new System.EventHandler(this.StorageVersion_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxVersion;
        private System.Windows.Forms.ComboBox comboBoxVersion;
        private System.Windows.Forms.LinkLabel moreinfoLiveStreamingProfilelink;
        public System.Windows.Forms.Label labelStorageAccount;
    }
}