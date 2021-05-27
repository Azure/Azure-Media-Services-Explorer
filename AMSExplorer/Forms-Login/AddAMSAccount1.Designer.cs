namespace AMSExplorer
{
    partial class AddAMSAccount1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddAMSAccount1));
            this.buttonNext = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelAssetName = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radioButtonAddAMSAccount = new System.Windows.Forms.RadioButton();
            this.checkBoxSelectUser = new System.Windows.Forms.CheckBox();
            this.radioButtonJsonCliOutput = new System.Windows.Forms.RadioButton();
            this.radioButtonAddManual = new System.Windows.Forms.RadioButton();
            this.comboBoxAADMappingList = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.linkLabelAzCliDoc = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.panelEnv = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panelEnv.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonNext
            // 
            resources.ApplyResources(this.buttonNext, "buttonNext");
            this.buttonNext.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // labelAssetName
            // 
            resources.ApplyResources(this.labelAssetName, "labelAssetName");
            this.labelAssetName.Name = "labelAssetName";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonNext);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // radioButtonAddAMSAccount
            // 
            resources.ApplyResources(this.radioButtonAddAMSAccount, "radioButtonAddAMSAccount");
            this.radioButtonAddAMSAccount.Checked = true;
            this.radioButtonAddAMSAccount.Name = "radioButtonAddAMSAccount";
            this.radioButtonAddAMSAccount.TabStop = true;
            this.radioButtonAddAMSAccount.UseVisualStyleBackColor = true;
            // 
            // checkBoxSelectUser
            // 
            resources.ApplyResources(this.checkBoxSelectUser, "checkBoxSelectUser");
            this.checkBoxSelectUser.Name = "checkBoxSelectUser";
            this.checkBoxSelectUser.UseVisualStyleBackColor = true;
            // 
            // radioButtonJsonCliOutput
            // 
            resources.ApplyResources(this.radioButtonJsonCliOutput, "radioButtonJsonCliOutput");
            this.radioButtonJsonCliOutput.Name = "radioButtonJsonCliOutput";
            this.radioButtonJsonCliOutput.UseVisualStyleBackColor = true;
            this.radioButtonJsonCliOutput.CheckedChanged += new System.EventHandler(this.RadioButtonJsonCliOutput_CheckedChanged);
            // 
            // radioButtonAddManual
            // 
            resources.ApplyResources(this.radioButtonAddManual, "radioButtonAddManual");
            this.radioButtonAddManual.Name = "radioButtonAddManual";
            this.radioButtonAddManual.UseVisualStyleBackColor = true;
            // 
            // comboBoxAADMappingList
            // 
            this.comboBoxAADMappingList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAADMappingList.FormattingEnabled = true;
            resources.ApplyResources(this.comboBoxAADMappingList, "comboBoxAADMappingList");
            this.comboBoxAADMappingList.Name = "comboBoxAADMappingList";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.linkLabelAzCliDoc);
            this.groupBox1.Controls.Add(this.radioButtonAddAMSAccount);
            this.groupBox1.Controls.Add(this.checkBoxSelectUser);
            this.groupBox1.Controls.Add(this.radioButtonJsonCliOutput);
            this.groupBox1.Controls.Add(this.radioButtonAddManual);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // linkLabelAzCliDoc
            // 
            resources.ApplyResources(this.linkLabelAzCliDoc, "linkLabelAzCliDoc");
            this.linkLabelAzCliDoc.Name = "linkLabelAzCliDoc";
            this.linkLabelAzCliDoc.TabStop = true;
            this.linkLabelAzCliDoc.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelAzCliDoc_LinkClicked);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // panelEnv
            // 
            this.panelEnv.Controls.Add(this.label1);
            this.panelEnv.Controls.Add(this.comboBoxAADMappingList);
            resources.ApplyResources(this.panelEnv, "panelEnv");
            this.panelEnv.Name = "panelEnv";
            // 
            // AddAMSAccount1
            // 
            this.AcceptButton = this.buttonNext;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.panelEnv);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labelAssetName);
            this.Name = "AddAMSAccount1";
            this.Load += new System.EventHandler(this.AddAMSAccount1_Load);
            this.Shown += new System.EventHandler(this.AddAMSAccount1_Shown);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panelEnv.ResumeLayout(false);
            this.panelEnv.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Button buttonNext;
        public System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelAssetName;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radioButtonAddAMSAccount;
        private System.Windows.Forms.CheckBox checkBoxSelectUser;
        private System.Windows.Forms.RadioButton radioButtonJsonCliOutput;
        private System.Windows.Forms.RadioButton radioButtonAddManual;
        public System.Windows.Forms.ComboBox comboBoxAADMappingList;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.LinkLabel linkLabelAzCliDoc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelEnv;
        private System.Windows.Forms.Label label2;
    }
}