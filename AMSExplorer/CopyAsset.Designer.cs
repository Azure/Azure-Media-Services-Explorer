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
            this.labelWarning = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.listBoxAccounts = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panelStorageAccount = new System.Windows.Forms.Panel();
            this.radioButtonDefaultStorage = new System.Windows.Forms.RadioButton();
            this.radioButtonSpecifyStorage = new System.Windows.Forms.RadioButton();
            this.labelWarningStorage = new System.Windows.Forms.Label();
            this.listBoxStorage = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.labelnewassetname = new System.Windows.Forms.Label();
            this.copyassetname = new System.Windows.Forms.TextBox();
            this.labelinfo = new System.Windows.Forms.Label();
            this.checkBoxDeleteSource = new System.Windows.Forms.CheckBox();
            this.groupBoxOptions = new System.Windows.Forms.GroupBox();
            this.labelCloneLocators = new System.Windows.Forms.Label();
            this.checkBoxCloneLocators = new System.Windows.Forms.CheckBox();
            this.checkBoxCopyDynEnc = new System.Windows.Forms.CheckBox();
            this.checkBoxRewriteURL = new System.Windows.Forms.CheckBox();
            this.checkBoxTargetSingleAsset = new System.Windows.Forms.CheckBox();
            this.labelExplanation = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panelStorageAccount.SuspendLayout();
            this.groupBoxOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(576, 12);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(96, 23);
            this.buttonOk.TabIndex = 4;
            this.buttonOk.Text = "Copy Asset{0}";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(678, 12);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(96, 23);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // labelDescription
            // 
            this.labelDescription.AutoSize = true;
            this.labelDescription.Location = new System.Drawing.Point(32, 298);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(58, 13);
            this.labelDescription.TabIndex = 35;
            this.labelDescription.Text = "description";
            // 
            // labelWarning
            // 
            this.labelWarning.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelWarning.AutoSize = true;
            this.labelWarning.ForeColor = System.Drawing.Color.Red;
            this.labelWarning.Location = new System.Drawing.Point(32, 321);
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
            this.panel1.Location = new System.Drawing.Point(-2, 514);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(787, 48);
            this.panel1.TabIndex = 60;
            // 
            // listBoxAccounts
            // 
            this.listBoxAccounts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxAccounts.FormattingEnabled = true;
            this.listBoxAccounts.Location = new System.Drawing.Point(22, 47);
            this.listBoxAccounts.Name = "listBoxAccounts";
            this.listBoxAccounts.Size = new System.Drawing.Size(210, 238);
            this.listBoxAccounts.TabIndex = 61;
            this.listBoxAccounts.SelectedIndexChanged += new System.EventHandler(this.listBoxAcounts_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.panelStorageAccount);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.listBoxAccounts);
            this.groupBox1.Controls.Add(this.labelDescription);
            this.groupBox1.Controls.Add(this.labelWarning);
            this.groupBox1.Location = new System.Drawing.Point(15, 56);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(554, 357);
            this.groupBox1.TabIndex = 63;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Destination";
            // 
            // panelStorageAccount
            // 
            this.panelStorageAccount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelStorageAccount.Controls.Add(this.radioButtonDefaultStorage);
            this.panelStorageAccount.Controls.Add(this.radioButtonSpecifyStorage);
            this.panelStorageAccount.Controls.Add(this.labelWarningStorage);
            this.panelStorageAccount.Controls.Add(this.listBoxStorage);
            this.panelStorageAccount.Location = new System.Drawing.Point(237, 13);
            this.panelStorageAccount.Name = "panelStorageAccount";
            this.panelStorageAccount.Size = new System.Drawing.Size(311, 338);
            this.panelStorageAccount.TabIndex = 70;
            // 
            // radioButtonDefaultStorage
            // 
            this.radioButtonDefaultStorage.AutoSize = true;
            this.radioButtonDefaultStorage.Checked = true;
            this.radioButtonDefaultStorage.Location = new System.Drawing.Point(13, 34);
            this.radioButtonDefaultStorage.Name = "radioButtonDefaultStorage";
            this.radioButtonDefaultStorage.Size = new System.Drawing.Size(139, 17);
            this.radioButtonDefaultStorage.TabIndex = 62;
            this.radioButtonDefaultStorage.TabStop = true;
            this.radioButtonDefaultStorage.Text = "Default storage account";
            this.radioButtonDefaultStorage.UseVisualStyleBackColor = true;
            // 
            // radioButtonSpecifyStorage
            // 
            this.radioButtonSpecifyStorage.AutoSize = true;
            this.radioButtonSpecifyStorage.Location = new System.Drawing.Point(13, 57);
            this.radioButtonSpecifyStorage.Name = "radioButtonSpecifyStorage";
            this.radioButtonSpecifyStorage.Size = new System.Drawing.Size(66, 17);
            this.radioButtonSpecifyStorage.TabIndex = 63;
            this.radioButtonSpecifyStorage.Text = "Specify :";
            this.radioButtonSpecifyStorage.UseVisualStyleBackColor = true;
            this.radioButtonSpecifyStorage.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // labelWarningStorage
            // 
            this.labelWarningStorage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelWarningStorage.AutoSize = true;
            this.labelWarningStorage.ForeColor = System.Drawing.Color.Red;
            this.labelWarningStorage.Location = new System.Drawing.Point(32, 308);
            this.labelWarningStorage.Name = "labelWarningStorage";
            this.labelWarningStorage.Size = new System.Drawing.Size(47, 13);
            this.labelWarningStorage.TabIndex = 65;
            this.labelWarningStorage.Text = "Warning";
            // 
            // listBoxStorage
            // 
            this.listBoxStorage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxStorage.Enabled = false;
            this.listBoxStorage.FormattingEnabled = true;
            this.listBoxStorage.Location = new System.Drawing.Point(13, 86);
            this.listBoxStorage.Name = "listBoxStorage";
            this.listBoxStorage.Size = new System.Drawing.Size(295, 186);
            this.listBoxStorage.TabIndex = 64;
            this.listBoxStorage.SelectedIndexChanged += new System.EventHandler(this.listBoxStorage_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(129, 13);
            this.label2.TabIndex = 69;
            this.label2.Text = "Media Services Account :";
            // 
            // labelnewassetname
            // 
            this.labelnewassetname.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelnewassetname.AutoSize = true;
            this.labelnewassetname.Location = new System.Drawing.Point(20, 451);
            this.labelnewassetname.Name = "labelnewassetname";
            this.labelnewassetname.Size = new System.Drawing.Size(95, 13);
            this.labelnewassetname.TabIndex = 65;
            this.labelnewassetname.Text = "New Asset Name :";
            // 
            // copyassetname
            // 
            this.copyassetname.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.copyassetname.Location = new System.Drawing.Point(15, 467);
            this.copyassetname.Name = "copyassetname";
            this.copyassetname.Size = new System.Drawing.Size(757, 20);
            this.copyassetname.TabIndex = 64;
            // 
            // labelinfo
            // 
            this.labelinfo.Location = new System.Drawing.Point(20, 22);
            this.labelinfo.Name = "labelinfo";
            this.labelinfo.Size = new System.Drawing.Size(327, 17);
            this.labelinfo.TabIndex = 66;
            this.labelinfo.Text = "{0} asset{1} selected.";
            // 
            // checkBoxDeleteSource
            // 
            this.checkBoxDeleteSource.AutoSize = true;
            this.checkBoxDeleteSource.Location = new System.Drawing.Point(19, 26);
            this.checkBoxDeleteSource.Name = "checkBoxDeleteSource";
            this.checkBoxDeleteSource.Size = new System.Drawing.Size(134, 17);
            this.checkBoxDeleteSource.TabIndex = 67;
            this.checkBoxDeleteSource.Text = "Delete source asset{0}";
            this.checkBoxDeleteSource.UseVisualStyleBackColor = true;
            // 
            // groupBoxOptions
            // 
            this.groupBoxOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxOptions.Controls.Add(this.labelCloneLocators);
            this.groupBoxOptions.Controls.Add(this.checkBoxCloneLocators);
            this.groupBoxOptions.Controls.Add(this.checkBoxCopyDynEnc);
            this.groupBoxOptions.Controls.Add(this.checkBoxRewriteURL);
            this.groupBoxOptions.Controls.Add(this.checkBoxTargetSingleAsset);
            this.groupBoxOptions.Controls.Add(this.checkBoxDeleteSource);
            this.groupBoxOptions.Location = new System.Drawing.Point(575, 56);
            this.groupBoxOptions.Name = "groupBoxOptions";
            this.groupBoxOptions.Size = new System.Drawing.Size(197, 357);
            this.groupBoxOptions.TabIndex = 68;
            this.groupBoxOptions.TabStop = false;
            this.groupBoxOptions.Text = "Options";
            // 
            // labelCloneLocators
            // 
            this.labelCloneLocators.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.labelCloneLocators.Location = new System.Drawing.Point(16, 191);
            this.labelCloneLocators.Name = "labelCloneLocators";
            this.labelCloneLocators.Size = new System.Drawing.Size(175, 32);
            this.labelCloneLocators.TabIndex = 71;
            this.labelCloneLocators.Text = "Requires that destination account is in a different datacenter";
            this.labelCloneLocators.Visible = false;
            // 
            // checkBoxCloneLocators
            // 
            this.checkBoxCloneLocators.Checked = true;
            this.checkBoxCloneLocators.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxCloneLocators.Location = new System.Drawing.Point(19, 171);
            this.checkBoxCloneLocators.Name = "checkBoxCloneLocators";
            this.checkBoxCloneLocators.Size = new System.Drawing.Size(144, 17);
            this.checkBoxCloneLocators.TabIndex = 71;
            this.checkBoxCloneLocators.Text = "Clone streaming locators";
            this.checkBoxCloneLocators.UseVisualStyleBackColor = true;
            this.checkBoxCloneLocators.Visible = false;
            // 
            // checkBoxCopyDynEnc
            // 
            this.checkBoxCopyDynEnc.AutoSize = true;
            this.checkBoxCopyDynEnc.Checked = true;
            this.checkBoxCopyDynEnc.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxCopyDynEnc.Location = new System.Drawing.Point(19, 99);
            this.checkBoxCopyDynEnc.Name = "checkBoxCopyDynEnc";
            this.checkBoxCopyDynEnc.Size = new System.Drawing.Size(144, 17);
            this.checkBoxCopyDynEnc.TabIndex = 70;
            this.checkBoxCopyDynEnc.Text = "Copy dynamic encryption";
            this.checkBoxCopyDynEnc.UseVisualStyleBackColor = true;
            this.checkBoxCopyDynEnc.CheckedChanged += new System.EventHandler(this.checkBoxCopyDynEnc_CheckedChanged);
            // 
            // checkBoxRewriteURL
            // 
            this.checkBoxRewriteURL.AutoSize = true;
            this.checkBoxRewriteURL.Checked = true;
            this.checkBoxRewriteURL.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxRewriteURL.Location = new System.Drawing.Point(39, 122);
            this.checkBoxRewriteURL.Name = "checkBoxRewriteURL";
            this.checkBoxRewriteURL.Size = new System.Drawing.Size(143, 30);
            this.checkBoxRewriteURL.TabIndex = 69;
            this.checkBoxRewriteURL.Text = "Rewrite\r\nLicence Acquisition URL";
            this.checkBoxRewriteURL.UseVisualStyleBackColor = true;
            // 
            // checkBoxTargetSingleAsset
            // 
            this.checkBoxTargetSingleAsset.AutoSize = true;
            this.checkBoxTargetSingleAsset.Location = new System.Drawing.Point(19, 49);
            this.checkBoxTargetSingleAsset.Name = "checkBoxTargetSingleAsset";
            this.checkBoxTargetSingleAsset.Size = new System.Drawing.Size(102, 30);
            this.checkBoxTargetSingleAsset.TabIndex = 68;
            this.checkBoxTargetSingleAsset.Text = "Merge all files to\r\none single asset";
            this.checkBoxTargetSingleAsset.UseVisualStyleBackColor = true;
            this.checkBoxTargetSingleAsset.CheckedChanged += new System.EventHandler(this.checkBoxTargetSingleAsset_CheckedChanged);
            // 
            // labelExplanation
            // 
            this.labelExplanation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelExplanation.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.labelExplanation.Location = new System.Drawing.Point(353, 24);
            this.labelExplanation.Name = "labelExplanation";
            this.labelExplanation.Size = new System.Drawing.Size(419, 29);
            this.labelExplanation.TabIndex = 70;
            this.labelExplanation.Text = "On-Demand and Live archive are supported. Assets should not be statically protect" +
    "ed.";
            this.labelExplanation.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // CopyAsset
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.labelExplanation);
            this.Controls.Add(this.groupBoxOptions);
            this.Controls.Add(this.labelinfo);
            this.Controls.Add(this.labelnewassetname);
            this.Controls.Add(this.copyassetname);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Name = "CopyAsset";
            this.Text = "Copy asset(s)";
            this.Load += new System.EventHandler(this.CopyAsset_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panelStorageAccount.ResumeLayout(false);
            this.panelStorageAccount.PerformLayout();
            this.groupBoxOptions.ResumeLayout(false);
            this.groupBoxOptions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button buttonOk;
        public System.Windows.Forms.Button buttonCancel;
        public System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.Label labelWarning;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListBox listBoxAccounts;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.Label labelnewassetname;
        public System.Windows.Forms.TextBox copyassetname;
        public System.Windows.Forms.Label labelinfo;
        private System.Windows.Forms.ListBox listBoxStorage;
        private System.Windows.Forms.RadioButton radioButtonSpecifyStorage;
        private System.Windows.Forms.RadioButton radioButtonDefaultStorage;
        private System.Windows.Forms.Label labelWarningStorage;
        public System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBoxDeleteSource;
        private System.Windows.Forms.GroupBox groupBoxOptions;
        private System.Windows.Forms.CheckBox checkBoxTargetSingleAsset;
        private System.Windows.Forms.CheckBox checkBoxRewriteURL;
        private System.Windows.Forms.Label labelExplanation;
        private System.Windows.Forms.CheckBox checkBoxCopyDynEnc;
        private System.Windows.Forms.Label labelCloneLocators;
        private System.Windows.Forms.CheckBox checkBoxCloneLocators;
        private System.Windows.Forms.Panel panelStorageAccount;
    }
}