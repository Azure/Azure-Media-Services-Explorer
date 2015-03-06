namespace AMSExplorer
{
    partial class CopyAsset
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
            this.labelDescription = new System.Windows.Forms.Label();
            this.radioButtonOnDemandAsset = new System.Windows.Forms.RadioButton();
            this.radioButtonLiveArchive = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.labelWarning = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.listBoxAcounts = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(282, 12);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(96, 23);
            this.buttonOk.TabIndex = 4;
            this.buttonOk.Text = "Copy Asset";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(384, 12);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(96, 23);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // labelDescription
            // 
            this.labelDescription.AutoSize = true;
            this.labelDescription.Location = new System.Drawing.Point(32, 223);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(58, 13);
            this.labelDescription.TabIndex = 35;
            this.labelDescription.Text = "description";
            // 
            // radioButtonOnDemandAsset
            // 
            this.radioButtonOnDemandAsset.AutoSize = true;
            this.radioButtonOnDemandAsset.Checked = true;
            this.radioButtonOnDemandAsset.Location = new System.Drawing.Point(35, 29);
            this.radioButtonOnDemandAsset.Name = "radioButtonOnDemandAsset";
            this.radioButtonOnDemandAsset.Size = new System.Drawing.Size(80, 17);
            this.radioButtonOnDemandAsset.TabIndex = 0;
            this.radioButtonOnDemandAsset.TabStop = true;
            this.radioButtonOnDemandAsset.Text = "On-demand";
            this.radioButtonOnDemandAsset.UseVisualStyleBackColor = true;
            this.radioButtonOnDemandAsset.CheckedChanged += new System.EventHandler(this.UpdateLocatorGUID_CheckedChanged);
            // 
            // radioButtonLiveArchive
            // 
            this.radioButtonLiveArchive.AutoSize = true;
            this.radioButtonLiveArchive.Location = new System.Drawing.Point(35, 53);
            this.radioButtonLiveArchive.Name = "radioButtonLiveArchive";
            this.radioButtonLiveArchive.Size = new System.Drawing.Size(130, 17);
            this.radioButtonLiveArchive.TabIndex = 1;
            this.radioButtonLiveArchive.Text = "Live archive (fragblob)";
            this.radioButtonLiveArchive.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.radioButtonLiveArchive);
            this.groupBox4.Controls.Add(this.radioButtonOnDemandAsset);
            this.groupBox4.Location = new System.Drawing.Point(15, 22);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(463, 86);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Asset type";
            // 
            // labelWarning
            // 
            this.labelWarning.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelWarning.AutoSize = true;
            this.labelWarning.ForeColor = System.Drawing.Color.Red;
            this.labelWarning.Location = new System.Drawing.Point(32, 255);
            this.labelWarning.Name = "labelWarning";
            this.labelWarning.Size = new System.Drawing.Size(47, 13);
            this.labelWarning.TabIndex = 44;
            this.labelWarning.Text = "Warning";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonOk);
            this.panel1.Location = new System.Drawing.Point(-2, 479);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(493, 48);
            this.panel1.TabIndex = 60;
            // 
            // listBoxAcounts
            // 
            this.listBoxAcounts.FormattingEnabled = true;
            this.listBoxAcounts.Location = new System.Drawing.Point(35, 34);
            this.listBoxAcounts.Name = "listBoxAcounts";
            this.listBoxAcounts.Size = new System.Drawing.Size(219, 186);
            this.listBoxAcounts.TabIndex = 61;
            this.listBoxAcounts.SelectedIndexChanged += new System.EventHandler(this.listBoxAcounts_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listBoxAcounts);
            this.groupBox1.Controls.Add(this.labelDescription);
            this.groupBox1.Controls.Add(this.labelWarning);
            this.groupBox1.Location = new System.Drawing.Point(15, 126);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(463, 284);
            this.groupBox1.TabIndex = 63;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Media Services Account Destination";
            // 
            // CopyAsset
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(490, 526);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox4);
            this.Name = "CopyAsset";
            this.Text = "Copy an asset";
            this.Load += new System.EventHandler(this.CopyAsset_Load);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Button buttonOk;
        public System.Windows.Forms.Button buttonCancel;
        public System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.RadioButton radioButtonOnDemandAsset;
        private System.Windows.Forms.RadioButton radioButtonLiveArchive;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label labelWarning;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListBox listBoxAcounts;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}