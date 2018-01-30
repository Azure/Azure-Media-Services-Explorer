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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CopyAsset));
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
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
            this.checkBoxCopyAltId = new System.Windows.Forms.CheckBox();
            this.checkBoxUnPublishSourceAsset = new System.Windows.Forms.CheckBox();
            this.checkBoxCloneAssetFilters = new System.Windows.Forms.CheckBox();
            this.labelCloneLocators = new System.Windows.Forms.Label();
            this.checkBoxCloneLocators = new System.Windows.Forms.CheckBox();
            this.checkBoxCopyDynEnc = new System.Windows.Forms.CheckBox();
            this.checkBoxRewriteURL = new System.Windows.Forms.CheckBox();
            this.checkBoxTargetSingleAsset = new System.Windows.Forms.CheckBox();
            this.labelCloneLocatorForPrograms = new System.Windows.Forms.Label();
            this.labelExplanation = new System.Windows.Forms.Label();
            this.labelAssetCopy = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panelStorageAccount.SuspendLayout();
            this.groupBoxOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            resources.ApplyResources(this.buttonOk, "buttonOk");
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // labelWarning
            // 
            resources.ApplyResources(this.labelWarning, "labelWarning");
            this.labelWarning.ForeColor = System.Drawing.Color.Red;
            this.labelWarning.Name = "labelWarning";
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonOk);
            this.panel1.Name = "panel1";
            // 
            // listBoxAccounts
            // 
            resources.ApplyResources(this.listBoxAccounts, "listBoxAccounts");
            this.listBoxAccounts.FormattingEnabled = true;
            this.listBoxAccounts.Name = "listBoxAccounts";
            this.listBoxAccounts.SelectedIndexChanged += new System.EventHandler(this.listBoxAcounts_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.panelStorageAccount);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.listBoxAccounts);
            this.groupBox1.Controls.Add(this.labelWarning);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // panelStorageAccount
            // 
            resources.ApplyResources(this.panelStorageAccount, "panelStorageAccount");
            this.panelStorageAccount.Controls.Add(this.radioButtonDefaultStorage);
            this.panelStorageAccount.Controls.Add(this.radioButtonSpecifyStorage);
            this.panelStorageAccount.Controls.Add(this.labelWarningStorage);
            this.panelStorageAccount.Controls.Add(this.listBoxStorage);
            this.panelStorageAccount.Name = "panelStorageAccount";
            // 
            // radioButtonDefaultStorage
            // 
            resources.ApplyResources(this.radioButtonDefaultStorage, "radioButtonDefaultStorage");
            this.radioButtonDefaultStorage.Checked = true;
            this.radioButtonDefaultStorage.Name = "radioButtonDefaultStorage";
            this.radioButtonDefaultStorage.TabStop = true;
            this.radioButtonDefaultStorage.UseVisualStyleBackColor = true;
            // 
            // radioButtonSpecifyStorage
            // 
            resources.ApplyResources(this.radioButtonSpecifyStorage, "radioButtonSpecifyStorage");
            this.radioButtonSpecifyStorage.Name = "radioButtonSpecifyStorage";
            this.radioButtonSpecifyStorage.UseVisualStyleBackColor = true;
            this.radioButtonSpecifyStorage.CheckedChanged += new System.EventHandler(this.radioButtonSpecify_CheckedChanged);
            // 
            // labelWarningStorage
            // 
            resources.ApplyResources(this.labelWarningStorage, "labelWarningStorage");
            this.labelWarningStorage.ForeColor = System.Drawing.Color.Red;
            this.labelWarningStorage.Name = "labelWarningStorage";
            // 
            // listBoxStorage
            // 
            resources.ApplyResources(this.listBoxStorage, "listBoxStorage");
            this.listBoxStorage.FormattingEnabled = true;
            this.listBoxStorage.Name = "listBoxStorage";
            this.listBoxStorage.SelectedIndexChanged += new System.EventHandler(this.listBoxStorage_SelectedIndexChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // labelnewassetname
            // 
            resources.ApplyResources(this.labelnewassetname, "labelnewassetname");
            this.labelnewassetname.Name = "labelnewassetname";
            // 
            // copyassetname
            // 
            resources.ApplyResources(this.copyassetname, "copyassetname");
            this.copyassetname.Name = "copyassetname";
            // 
            // labelinfo
            // 
            resources.ApplyResources(this.labelinfo, "labelinfo");
            this.labelinfo.Name = "labelinfo";
            // 
            // checkBoxDeleteSource
            // 
            resources.ApplyResources(this.checkBoxDeleteSource, "checkBoxDeleteSource");
            this.checkBoxDeleteSource.Name = "checkBoxDeleteSource";
            this.checkBoxDeleteSource.UseVisualStyleBackColor = true;
            // 
            // groupBoxOptions
            // 
            resources.ApplyResources(this.groupBoxOptions, "groupBoxOptions");
            this.groupBoxOptions.Controls.Add(this.checkBoxCopyAltId);
            this.groupBoxOptions.Controls.Add(this.checkBoxUnPublishSourceAsset);
            this.groupBoxOptions.Controls.Add(this.checkBoxCloneAssetFilters);
            this.groupBoxOptions.Controls.Add(this.labelCloneLocators);
            this.groupBoxOptions.Controls.Add(this.checkBoxCloneLocators);
            this.groupBoxOptions.Controls.Add(this.checkBoxCopyDynEnc);
            this.groupBoxOptions.Controls.Add(this.checkBoxRewriteURL);
            this.groupBoxOptions.Controls.Add(this.checkBoxTargetSingleAsset);
            this.groupBoxOptions.Controls.Add(this.checkBoxDeleteSource);
            this.groupBoxOptions.Controls.Add(this.labelCloneLocatorForPrograms);
            this.groupBoxOptions.Name = "groupBoxOptions";
            this.groupBoxOptions.TabStop = false;
            // 
            // checkBoxCopyAltId
            // 
            resources.ApplyResources(this.checkBoxCopyAltId, "checkBoxCopyAltId");
            this.checkBoxCopyAltId.Name = "checkBoxCopyAltId";
            this.checkBoxCopyAltId.UseVisualStyleBackColor = true;
            // 
            // checkBoxUnPublishSourceAsset
            // 
            resources.ApplyResources(this.checkBoxUnPublishSourceAsset, "checkBoxUnPublishSourceAsset");
            this.checkBoxUnPublishSourceAsset.Name = "checkBoxUnPublishSourceAsset";
            this.checkBoxUnPublishSourceAsset.UseVisualStyleBackColor = true;
            // 
            // checkBoxCloneAssetFilters
            // 
            this.checkBoxCloneAssetFilters.Checked = true;
            this.checkBoxCloneAssetFilters.CheckState = System.Windows.Forms.CheckState.Checked;
            resources.ApplyResources(this.checkBoxCloneAssetFilters, "checkBoxCloneAssetFilters");
            this.checkBoxCloneAssetFilters.Name = "checkBoxCloneAssetFilters";
            this.toolTip1.SetToolTip(this.checkBoxCloneAssetFilters, resources.GetString("checkBoxCloneAssetFilters.ToolTip"));
            this.checkBoxCloneAssetFilters.UseVisualStyleBackColor = true;
            // 
            // labelCloneLocators
            // 
            this.labelCloneLocators.ForeColor = System.Drawing.SystemColors.WindowFrame;
            resources.ApplyResources(this.labelCloneLocators, "labelCloneLocators");
            this.labelCloneLocators.Name = "labelCloneLocators";
            // 
            // checkBoxCloneLocators
            // 
            this.checkBoxCloneLocators.Checked = true;
            this.checkBoxCloneLocators.CheckState = System.Windows.Forms.CheckState.Checked;
            resources.ApplyResources(this.checkBoxCloneLocators, "checkBoxCloneLocators");
            this.checkBoxCloneLocators.Name = "checkBoxCloneLocators";
            this.toolTip1.SetToolTip(this.checkBoxCloneLocators, resources.GetString("checkBoxCloneLocators.ToolTip"));
            this.checkBoxCloneLocators.UseVisualStyleBackColor = true;
            this.checkBoxCloneLocators.CheckedChanged += new System.EventHandler(this.checkBoxCloneLocators_CheckedChanged);
            // 
            // checkBoxCopyDynEnc
            // 
            resources.ApplyResources(this.checkBoxCopyDynEnc, "checkBoxCopyDynEnc");
            this.checkBoxCopyDynEnc.Checked = true;
            this.checkBoxCopyDynEnc.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxCopyDynEnc.Name = "checkBoxCopyDynEnc";
            this.checkBoxCopyDynEnc.UseVisualStyleBackColor = true;
            this.checkBoxCopyDynEnc.CheckedChanged += new System.EventHandler(this.checkBoxCopyDynEnc_CheckedChanged);
            // 
            // checkBoxRewriteURL
            // 
            resources.ApplyResources(this.checkBoxRewriteURL, "checkBoxRewriteURL");
            this.checkBoxRewriteURL.Checked = true;
            this.checkBoxRewriteURL.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxRewriteURL.Name = "checkBoxRewriteURL";
            this.toolTip1.SetToolTip(this.checkBoxRewriteURL, resources.GetString("checkBoxRewriteURL.ToolTip"));
            this.checkBoxRewriteURL.UseVisualStyleBackColor = true;
            // 
            // checkBoxTargetSingleAsset
            // 
            resources.ApplyResources(this.checkBoxTargetSingleAsset, "checkBoxTargetSingleAsset");
            this.checkBoxTargetSingleAsset.Name = "checkBoxTargetSingleAsset";
            this.checkBoxTargetSingleAsset.UseVisualStyleBackColor = true;
            this.checkBoxTargetSingleAsset.CheckedChanged += new System.EventHandler(this.checkBoxTargetSingleAsset_CheckedChanged);
            // 
            // labelCloneLocatorForPrograms
            // 
            this.labelCloneLocatorForPrograms.ForeColor = System.Drawing.SystemColors.WindowFrame;
            resources.ApplyResources(this.labelCloneLocatorForPrograms, "labelCloneLocatorForPrograms");
            this.labelCloneLocatorForPrograms.Name = "labelCloneLocatorForPrograms";
            // 
            // labelExplanation
            // 
            this.labelExplanation.ForeColor = System.Drawing.SystemColors.WindowFrame;
            resources.ApplyResources(this.labelExplanation, "labelExplanation");
            this.labelExplanation.Name = "labelExplanation";
            // 
            // labelAssetCopy
            // 
            resources.ApplyResources(this.labelAssetCopy, "labelAssetCopy");
            this.labelAssetCopy.ForeColor = System.Drawing.Color.DarkBlue;
            this.labelAssetCopy.Name = "labelAssetCopy";
            // 
            // panel2
            // 
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Name = "panel2";
            // 
            // CopyAsset
            // 
            this.AcceptButton = this.buttonOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.labelAssetCopy);
            this.Controls.Add(this.labelExplanation);
            this.Controls.Add(this.groupBoxOptions);
            this.Controls.Add(this.labelinfo);
            this.Controls.Add(this.labelnewassetname);
            this.Controls.Add(this.copyassetname);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Name = "CopyAsset";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
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
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox checkBoxUnPublishSourceAsset;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label labelCloneLocatorForPrograms;
        private System.Windows.Forms.CheckBox checkBoxCopyAltId;
    }
}