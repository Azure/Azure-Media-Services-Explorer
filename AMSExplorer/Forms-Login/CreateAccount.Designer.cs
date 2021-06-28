namespace AMSExplorer
{
    partial class CreateAccount
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateAccount));
            this.buttonCreate = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.comboBoxAzureLocations = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxAccountName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxStorageId = new System.Windows.Forms.TextBox();
            this.buttonCheckAvailAMS = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelOkAMSAccount = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxRG = new System.Windows.Forms.TextBox();
            this.progressBarCreation = new System.Windows.Forms.ProgressBar();
            this.checkBoxCreateRG = new System.Windows.Forms.CheckBox();
            this.checkBoxCreateStorage = new System.Windows.Forms.CheckBox();
            this.textBoxNewStorageName = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxStorageType = new System.Windows.Forms.ComboBox();
            this.labelOkStorageAccount = new System.Windows.Forms.Label();
            this.buttonCheckAvailStorage = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonCreate
            // 
            resources.ApplyResources(this.buttonCreate, "buttonCreate");
            this.buttonCreate.Name = "buttonCreate";
            this.buttonCreate.UseVisualStyleBackColor = true;
            this.buttonCreate.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonCreate);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // comboBoxAzureLocations
            // 
            this.comboBoxAzureLocations.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAzureLocations.FormattingEnabled = true;
            resources.ApplyResources(this.comboBoxAzureLocations, "comboBoxAzureLocations");
            this.comboBoxAzureLocations.Name = "comboBoxAzureLocations";
            this.comboBoxAzureLocations.SelectedIndexChanged += new System.EventHandler(this.comboBoxAzureLocations_SelectedIndexChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // textBoxAccountName
            // 
            resources.ApplyResources(this.textBoxAccountName, "textBoxAccountName");
            this.textBoxAccountName.Name = "textBoxAccountName";
            this.textBoxAccountName.TextChanged += new System.EventHandler(this.textBoxAccountName_TextChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // textBoxStorageId
            // 
            resources.ApplyResources(this.textBoxStorageId, "textBoxStorageId");
            this.textBoxStorageId.Name = "textBoxStorageId";
            // 
            // buttonCheckAvailAMS
            // 
            resources.ApplyResources(this.buttonCheckAvailAMS, "buttonCheckAvailAMS");
            this.buttonCheckAvailAMS.Name = "buttonCheckAvailAMS";
            this.buttonCheckAvailAMS.UseVisualStyleBackColor = true;
            this.buttonCheckAvailAMS.Click += new System.EventHandler(this.buttonCheckAvail_Click);
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.labelOkAMSAccount);
            this.groupBox1.Controls.Add(this.buttonCheckAvailAMS);
            this.groupBox1.Controls.Add(this.textBoxAccountName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // labelOkAMSAccount
            // 
            resources.ApplyResources(this.labelOkAMSAccount, "labelOkAMSAccount");
            this.labelOkAMSAccount.Name = "labelOkAMSAccount";
            this.labelOkAMSAccount.Tag = "✓";
            this.labelOkAMSAccount.UseCompatibleTextRendering = true;
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // textBoxRG
            // 
            resources.ApplyResources(this.textBoxRG, "textBoxRG");
            this.textBoxRG.Name = "textBoxRG";
            // 
            // progressBarCreation
            // 
            resources.ApplyResources(this.progressBarCreation, "progressBarCreation");
            this.progressBarCreation.MarqueeAnimationSpeed = 30;
            this.progressBarCreation.Name = "progressBarCreation";
            this.progressBarCreation.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            // 
            // checkBoxCreateRG
            // 
            resources.ApplyResources(this.checkBoxCreateRG, "checkBoxCreateRG");
            this.checkBoxCreateRG.Checked = true;
            this.checkBoxCreateRG.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxCreateRG.Name = "checkBoxCreateRG";
            this.checkBoxCreateRG.UseVisualStyleBackColor = true;
            // 
            // checkBoxCreateStorage
            // 
            resources.ApplyResources(this.checkBoxCreateStorage, "checkBoxCreateStorage");
            this.checkBoxCreateStorage.Checked = true;
            this.checkBoxCreateStorage.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxCreateStorage.Name = "checkBoxCreateStorage";
            this.checkBoxCreateStorage.UseVisualStyleBackColor = true;
            this.checkBoxCreateStorage.CheckedChanged += new System.EventHandler(this.checkBoxCreateStorage_CheckedChanged);
            // 
            // textBoxNewStorageName
            // 
            resources.ApplyResources(this.textBoxNewStorageName, "textBoxNewStorageName");
            this.textBoxNewStorageName.Name = "textBoxNewStorageName";
            this.textBoxNewStorageName.TextChanged += new System.EventHandler(this.textBoxNewStorageName_TextChanged);
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.comboBoxStorageType);
            this.groupBox2.Controls.Add(this.labelOkStorageAccount);
            this.groupBox2.Controls.Add(this.buttonCheckAvailStorage);
            this.groupBox2.Controls.Add(this.checkBoxCreateStorage);
            this.groupBox2.Controls.Add(this.textBoxNewStorageName);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.textBoxStorageId);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // comboBoxStorageType
            // 
            resources.ApplyResources(this.comboBoxStorageType, "comboBoxStorageType");
            this.comboBoxStorageType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStorageType.FormattingEnabled = true;
            this.comboBoxStorageType.Name = "comboBoxStorageType";
            // 
            // labelOkStorageAccount
            // 
            resources.ApplyResources(this.labelOkStorageAccount, "labelOkStorageAccount");
            this.labelOkStorageAccount.Name = "labelOkStorageAccount";
            this.labelOkStorageAccount.Tag = "✓";
            this.labelOkStorageAccount.UseCompatibleTextRendering = true;
            // 
            // buttonCheckAvailStorage
            // 
            resources.ApplyResources(this.buttonCheckAvailStorage, "buttonCheckAvailStorage");
            this.buttonCheckAvailStorage.Name = "buttonCheckAvailStorage";
            this.buttonCheckAvailStorage.UseVisualStyleBackColor = true;
            this.buttonCheckAvailStorage.Click += new System.EventHandler(this.buttonCheckAvailStorage_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.ForeColor = System.Drawing.Color.DarkBlue;
            this.label6.Name = "label6";
            // 
            // CreateAccount
            // 
            this.AcceptButton = this.buttonCreate;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.label6);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.checkBoxCreateRG);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progressBarCreation);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxRG);
            this.Controls.Add(this.comboBoxAzureLocations);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Name = "CreateAccount";
            this.Load += new System.EventHandler(this.CreateAccount_Load);
            this.Shown += new System.EventHandler(this.CreateAccount_Shown);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button buttonCreate;
        public System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.ComboBox comboBoxAzureLocations;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxAccountName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxStorageId;
        private System.Windows.Forms.Button buttonCheckAvailAMS;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxRG;
        private System.Windows.Forms.ProgressBar progressBarCreation;
        private System.Windows.Forms.CheckBox checkBoxCreateRG;
        private System.Windows.Forms.CheckBox checkBoxCreateStorage;
        private System.Windows.Forms.TextBox textBoxNewStorageName;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonCheckAvailStorage;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label labelOkAMSAccount;
        private System.Windows.Forms.Label labelOkStorageAccount;
        private System.Windows.Forms.ComboBox comboBoxStorageType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}