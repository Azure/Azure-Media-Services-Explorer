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
            this.components = new System.ComponentModel.Container();
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
            this.labelCloneFilters = new System.Windows.Forms.Label();
            this.checkBoxCloneAssetFilters = new System.Windows.Forms.CheckBox();
            this.labelCloneLocators = new System.Windows.Forms.Label();
            this.checkBoxCloneLocators = new System.Windows.Forms.CheckBox();
            this.checkBoxCopyDynEnc = new System.Windows.Forms.CheckBox();
            this.checkBoxRewriteURL = new System.Windows.Forms.CheckBox();
            this.checkBoxTargetSingleAsset = new System.Windows.Forms.CheckBox();
            this.labelExplanation = new System.Windows.Forms.Label();
            this.labelAssetCopy = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.checkBoxUnPublishSourceAsset = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.labelCloneLocatorForPrograms = new System.Windows.Forms.Label();
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
            this.buttonOk.Location = new System.Drawing.Point(528, 14);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(112, 27);
            this.buttonOk.TabIndex = 4;
            this.buttonOk.Text = "Copy Asset{0}";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(647, 14);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(112, 27);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // labelDescription
            // 
            this.labelDescription.AutoSize = true;
            this.labelDescription.Location = new System.Drawing.Point(37, 344);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(66, 15);
            this.labelDescription.TabIndex = 35;
            this.labelDescription.Text = "description";
            // 
            // labelWarning
            // 
            this.labelWarning.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelWarning.AutoSize = true;
            this.labelWarning.ForeColor = System.Drawing.Color.Red;
            this.labelWarning.Location = new System.Drawing.Point(37, 291);
            this.labelWarning.Name = "labelWarning";
            this.labelWarning.Size = new System.Drawing.Size(52, 15);
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
            this.panel1.Location = new System.Drawing.Point(-2, 507);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(774, 55);
            this.panel1.TabIndex = 60;
            // 
            // listBoxAccounts
            // 
            this.listBoxAccounts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxAccounts.FormattingEnabled = true;
            this.listBoxAccounts.ItemHeight = 15;
            this.listBoxAccounts.Location = new System.Drawing.Point(26, 54);
            this.listBoxAccounts.Name = "listBoxAccounts";
            this.listBoxAccounts.Size = new System.Drawing.Size(176, 229);
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
            this.groupBox1.Location = new System.Drawing.Point(17, 91);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(515, 326);
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
            this.panelStorageAccount.Location = new System.Drawing.Point(208, 15);
            this.panelStorageAccount.Name = "panelStorageAccount";
            this.panelStorageAccount.Size = new System.Drawing.Size(297, 304);
            this.panelStorageAccount.TabIndex = 70;
            // 
            // radioButtonDefaultStorage
            // 
            this.radioButtonDefaultStorage.AutoSize = true;
            this.radioButtonDefaultStorage.Checked = true;
            this.radioButtonDefaultStorage.Location = new System.Drawing.Point(15, 39);
            this.radioButtonDefaultStorage.Name = "radioButtonDefaultStorage";
            this.radioButtonDefaultStorage.Size = new System.Drawing.Size(151, 19);
            this.radioButtonDefaultStorage.TabIndex = 62;
            this.radioButtonDefaultStorage.TabStop = true;
            this.radioButtonDefaultStorage.Text = "Default storage account";
            this.radioButtonDefaultStorage.UseVisualStyleBackColor = true;
            // 
            // radioButtonSpecifyStorage
            // 
            this.radioButtonSpecifyStorage.AutoSize = true;
            this.radioButtonSpecifyStorage.Location = new System.Drawing.Point(15, 66);
            this.radioButtonSpecifyStorage.Name = "radioButtonSpecifyStorage";
            this.radioButtonSpecifyStorage.Size = new System.Drawing.Size(69, 19);
            this.radioButtonSpecifyStorage.TabIndex = 63;
            this.radioButtonSpecifyStorage.Text = "Specify :";
            this.radioButtonSpecifyStorage.UseVisualStyleBackColor = true;
            this.radioButtonSpecifyStorage.CheckedChanged += new System.EventHandler(this.radioButtonSpecify_CheckedChanged);
            // 
            // labelWarningStorage
            // 
            this.labelWarningStorage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelWarningStorage.AutoSize = true;
            this.labelWarningStorage.ForeColor = System.Drawing.Color.Red;
            this.labelWarningStorage.Location = new System.Drawing.Point(37, 275);
            this.labelWarningStorage.Name = "labelWarningStorage";
            this.labelWarningStorage.Size = new System.Drawing.Size(52, 15);
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
            this.listBoxStorage.ItemHeight = 15;
            this.listBoxStorage.Location = new System.Drawing.Point(15, 99);
            this.listBoxStorage.Name = "listBoxStorage";
            this.listBoxStorage.Size = new System.Drawing.Size(277, 169);
            this.listBoxStorage.TabIndex = 64;
            this.listBoxStorage.SelectedIndexChanged += new System.EventHandler(this.listBoxStorage_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(139, 15);
            this.label2.TabIndex = 69;
            this.label2.Text = "Media Services Account :";
            // 
            // labelnewassetname
            // 
            this.labelnewassetname.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelnewassetname.AutoSize = true;
            this.labelnewassetname.Location = new System.Drawing.Point(14, 435);
            this.labelnewassetname.Name = "labelnewassetname";
            this.labelnewassetname.Size = new System.Drawing.Size(103, 15);
            this.labelnewassetname.TabIndex = 65;
            this.labelnewassetname.Text = "New Asset Name :";
            // 
            // copyassetname
            // 
            this.copyassetname.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.copyassetname.Location = new System.Drawing.Point(17, 453);
            this.copyassetname.Name = "copyassetname";
            this.copyassetname.Size = new System.Drawing.Size(751, 23);
            this.copyassetname.TabIndex = 64;
            // 
            // labelinfo
            // 
            this.labelinfo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelinfo.Location = new System.Drawing.Point(14, 62);
            this.labelinfo.Name = "labelinfo";
            this.labelinfo.Size = new System.Drawing.Size(756, 20);
            this.labelinfo.TabIndex = 66;
            this.labelinfo.Text = "{0} asset{1} selected.";
            // 
            // checkBoxDeleteSource
            // 
            this.checkBoxDeleteSource.AutoSize = true;
            this.checkBoxDeleteSource.Location = new System.Drawing.Point(22, 30);
            this.checkBoxDeleteSource.Name = "checkBoxDeleteSource";
            this.checkBoxDeleteSource.Size = new System.Drawing.Size(140, 19);
            this.checkBoxDeleteSource.TabIndex = 67;
            this.checkBoxDeleteSource.Text = "Delete source asset{0}";
            this.checkBoxDeleteSource.UseVisualStyleBackColor = true;
            // 
            // groupBoxOptions
            // 
            this.groupBoxOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxOptions.Controls.Add(this.checkBoxUnPublishSourceAsset);
            this.groupBoxOptions.Controls.Add(this.labelCloneFilters);
            this.groupBoxOptions.Controls.Add(this.checkBoxCloneAssetFilters);
            this.groupBoxOptions.Controls.Add(this.labelCloneLocators);
            this.groupBoxOptions.Controls.Add(this.checkBoxCloneLocators);
            this.groupBoxOptions.Controls.Add(this.checkBoxCopyDynEnc);
            this.groupBoxOptions.Controls.Add(this.checkBoxRewriteURL);
            this.groupBoxOptions.Controls.Add(this.checkBoxTargetSingleAsset);
            this.groupBoxOptions.Controls.Add(this.checkBoxDeleteSource);
            this.groupBoxOptions.Controls.Add(this.labelCloneLocatorForPrograms);
            this.groupBoxOptions.Location = new System.Drawing.Point(540, 91);
            this.groupBoxOptions.Name = "groupBoxOptions";
            this.groupBoxOptions.Size = new System.Drawing.Size(230, 326);
            this.groupBoxOptions.TabIndex = 68;
            this.groupBoxOptions.TabStop = false;
            this.groupBoxOptions.Text = "Options";
            // 
            // labelCloneFilters
            // 
            this.labelCloneFilters.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.labelCloneFilters.Location = new System.Drawing.Point(19, 203);
            this.labelCloneFilters.Name = "labelCloneFilters";
            this.labelCloneFilters.Size = new System.Drawing.Size(204, 23);
            this.labelCloneFilters.TabIndex = 73;
            this.labelCloneFilters.Text = "Start and end times will be removed";
            // 
            // checkBoxCloneAssetFilters
            // 
            this.checkBoxCloneAssetFilters.Checked = true;
            this.checkBoxCloneAssetFilters.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxCloneAssetFilters.Location = new System.Drawing.Point(22, 180);
            this.checkBoxCloneAssetFilters.Name = "checkBoxCloneAssetFilters";
            this.checkBoxCloneAssetFilters.Size = new System.Drawing.Size(168, 20);
            this.checkBoxCloneAssetFilters.TabIndex = 72;
            this.checkBoxCloneAssetFilters.Text = "Clone asset filters";
            this.checkBoxCloneAssetFilters.UseVisualStyleBackColor = true;
            // 
            // labelCloneLocators
            // 
            this.labelCloneLocators.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.labelCloneLocators.Location = new System.Drawing.Point(31, 282);
            this.labelCloneLocators.Name = "labelCloneLocators";
            this.labelCloneLocators.Size = new System.Drawing.Size(193, 37);
            this.labelCloneLocators.TabIndex = 71;
            this.labelCloneLocators.Text = "Required if source and destination are in the same region";
            // 
            // checkBoxCloneLocators
            // 
            this.checkBoxCloneLocators.Checked = true;
            this.checkBoxCloneLocators.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxCloneLocators.Location = new System.Drawing.Point(22, 234);
            this.checkBoxCloneLocators.Name = "checkBoxCloneLocators";
            this.checkBoxCloneLocators.Size = new System.Drawing.Size(168, 20);
            this.checkBoxCloneLocators.TabIndex = 71;
            this.checkBoxCloneLocators.Text = "Clone streaming locators";
            this.toolTip1.SetToolTip(this.checkBoxCloneLocators, "ID, Name, Start time and Expiration time will be copied");
            this.checkBoxCloneLocators.UseVisualStyleBackColor = true;
            this.checkBoxCloneLocators.CheckedChanged += new System.EventHandler(this.checkBoxCloneLocators_CheckedChanged);
            // 
            // checkBoxCopyDynEnc
            // 
            this.checkBoxCopyDynEnc.AutoSize = true;
            this.checkBoxCopyDynEnc.Checked = true;
            this.checkBoxCopyDynEnc.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxCopyDynEnc.Location = new System.Drawing.Point(22, 114);
            this.checkBoxCopyDynEnc.Name = "checkBoxCopyDynEnc";
            this.checkBoxCopyDynEnc.Size = new System.Drawing.Size(163, 19);
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
            this.checkBoxRewriteURL.Location = new System.Drawing.Point(45, 136);
            this.checkBoxRewriteURL.Name = "checkBoxRewriteURL";
            this.checkBoxRewriteURL.Size = new System.Drawing.Size(153, 34);
            this.checkBoxRewriteURL.TabIndex = 69;
            this.checkBoxRewriteURL.Text = "Rewrite\r\nLicence Acquisition URL";
            this.toolTip1.SetToolTip(this.checkBoxRewriteURL, "Rewrite URL to use the name of the server");
            this.checkBoxRewriteURL.UseVisualStyleBackColor = true;
            // 
            // checkBoxTargetSingleAsset
            // 
            this.checkBoxTargetSingleAsset.AutoSize = true;
            this.checkBoxTargetSingleAsset.Location = new System.Drawing.Point(22, 60);
            this.checkBoxTargetSingleAsset.Name = "checkBoxTargetSingleAsset";
            this.checkBoxTargetSingleAsset.Size = new System.Drawing.Size(113, 34);
            this.checkBoxTargetSingleAsset.TabIndex = 68;
            this.checkBoxTargetSingleAsset.Text = "Merge all files to\r\none single asset";
            this.checkBoxTargetSingleAsset.UseVisualStyleBackColor = true;
            this.checkBoxTargetSingleAsset.CheckedChanged += new System.EventHandler(this.checkBoxTargetSingleAsset_CheckedChanged);
            // 
            // labelExplanation
            // 
            this.labelExplanation.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.labelExplanation.Location = new System.Drawing.Point(12, 39);
            this.labelExplanation.Name = "labelExplanation";
            this.labelExplanation.Size = new System.Drawing.Size(756, 23);
            this.labelExplanation.TabIndex = 70;
            this.labelExplanation.Text = "On-Demand and Live archive are supported. Assets should not be statically protect" +
    "ed.";
            // 
            // labelAssetCopy
            // 
            this.labelAssetCopy.AutoSize = true;
            this.labelAssetCopy.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAssetCopy.ForeColor = System.Drawing.Color.DarkBlue;
            this.labelAssetCopy.Location = new System.Drawing.Point(13, 12);
            this.labelAssetCopy.Name = "labelAssetCopy";
            this.labelAssetCopy.Size = new System.Drawing.Size(82, 20);
            this.labelAssetCopy.TabIndex = 71;
            this.labelAssetCopy.Text = "Asset Copy";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Location = new System.Drawing.Point(763, 507);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(24, 41);
            this.panel2.TabIndex = 77;
            // 
            // checkBoxUnPublishSourceAsset
            // 
            this.checkBoxUnPublishSourceAsset.AutoSize = true;
            this.checkBoxUnPublishSourceAsset.Location = new System.Drawing.Point(45, 260);
            this.checkBoxUnPublishSourceAsset.Name = "checkBoxUnPublishSourceAsset";
            this.checkBoxUnPublishSourceAsset.Size = new System.Drawing.Size(147, 19);
            this.checkBoxUnPublishSourceAsset.TabIndex = 74;
            this.checkBoxUnPublishSourceAsset.Text = "Unpublish source asset";
            this.checkBoxUnPublishSourceAsset.UseVisualStyleBackColor = true;
            // 
            // labelCloneLocatorForPrograms
            // 
            this.labelCloneLocatorForPrograms.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.labelCloneLocatorForPrograms.Location = new System.Drawing.Point(35, 255);
            this.labelCloneLocatorForPrograms.Name = "labelCloneLocatorForPrograms";
            this.labelCloneLocatorForPrograms.Size = new System.Drawing.Size(193, 37);
            this.labelCloneLocatorForPrograms.TabIndex = 75;
            this.labelCloneLocatorForPrograms.Text = "Source and destination must be in different regions";
            this.labelCloneLocatorForPrograms.Visible = false;
            // 
            // CopyAsset
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.labelAssetCopy);
            this.Controls.Add(this.labelExplanation);
            this.Controls.Add(this.groupBoxOptions);
            this.Controls.Add(this.labelinfo);
            this.Controls.Add(this.labelnewassetname);
            this.Controls.Add(this.copyassetname);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "CopyAsset";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
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
        private System.Windows.Forms.Label labelAssetCopy;
        private System.Windows.Forms.CheckBox checkBoxCloneAssetFilters;
        private System.Windows.Forms.Label labelCloneFilters;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox checkBoxUnPublishSourceAsset;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label labelCloneLocatorForPrograms;
    }
}