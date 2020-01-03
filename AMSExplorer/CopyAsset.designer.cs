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
            this.labelDescription = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listViewAccounts = new System.Windows.Forms.ListView();
            this.panelStorageAccount = new System.Windows.Forms.Panel();
            this.listBoxStorage = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.labelnewassetname = new System.Windows.Forms.Label();
            this.copyassetname = new System.Windows.Forms.TextBox();
            this.labelinfo = new System.Windows.Forms.Label();
            this.checkBoxDeleteSource = new System.Windows.Forms.CheckBox();
            this.groupBoxOptions = new System.Windows.Forms.GroupBox();
            this.checkBoxTargetSingleAsset = new System.Windows.Forms.CheckBox();
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
            // labelDescription
            // 
            resources.ApplyResources(this.labelDescription, "labelDescription");
            this.labelDescription.Name = "labelDescription";
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonOk);
            this.panel1.Name = "panel1";
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.listViewAccounts);
            this.groupBox1.Controls.Add(this.panelStorageAccount);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.labelDescription);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // listViewAccounts
            // 
            resources.ApplyResources(this.listViewAccounts, "listViewAccounts");
            this.listViewAccounts.FullRowSelect = true;
            this.listViewAccounts.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewAccounts.HideSelection = false;
            this.listViewAccounts.Name = "listViewAccounts";
            this.listViewAccounts.ShowItemToolTips = true;
            this.listViewAccounts.UseCompatibleStateImageBehavior = false;
            this.listViewAccounts.View = System.Windows.Forms.View.List;
            this.listViewAccounts.SelectedIndexChanged += new System.EventHandler(this.listViewAccounts_SelectedIndexChanged);
            // 
            // panelStorageAccount
            // 
            resources.ApplyResources(this.panelStorageAccount, "panelStorageAccount");
            this.panelStorageAccount.Controls.Add(this.listBoxStorage);
            this.panelStorageAccount.Name = "panelStorageAccount";
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
            this.groupBoxOptions.Controls.Add(this.checkBoxTargetSingleAsset);
            this.groupBoxOptions.Controls.Add(this.checkBoxDeleteSource);
            this.groupBoxOptions.Name = "groupBoxOptions";
            this.groupBoxOptions.TabStop = false;
            // 
            // checkBoxTargetSingleAsset
            // 
            resources.ApplyResources(this.checkBoxTargetSingleAsset, "checkBoxTargetSingleAsset");
            this.checkBoxTargetSingleAsset.Name = "checkBoxTargetSingleAsset";
            this.checkBoxTargetSingleAsset.UseVisualStyleBackColor = true;
            this.checkBoxTargetSingleAsset.CheckedChanged += new System.EventHandler(this.checkBoxTargetSingleAsset_CheckedChanged);
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
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
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
            this.DpiChanged += new System.Windows.Forms.DpiChangedEventHandler(this.CopyAsset_DpiChanged);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panelStorageAccount.ResumeLayout(false);
            this.groupBoxOptions.ResumeLayout(false);
            this.groupBoxOptions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button buttonOk;
        public System.Windows.Forms.Button buttonCancel;
        public System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.Label labelnewassetname;
        public System.Windows.Forms.TextBox copyassetname;
        public System.Windows.Forms.Label labelinfo;
        private System.Windows.Forms.ListBox listBoxStorage;
        public System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBoxDeleteSource;
        private System.Windows.Forms.GroupBox groupBoxOptions;
        private System.Windows.Forms.CheckBox checkBoxTargetSingleAsset;
        private System.Windows.Forms.Label labelExplanation;
        private System.Windows.Forms.Panel panelStorageAccount;
        private System.Windows.Forms.Label labelAssetCopy;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ListView listViewAccounts;
    }
}