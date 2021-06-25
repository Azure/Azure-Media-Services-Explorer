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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateAccount));
            this.buttonCreate = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelAssetName = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.comboBoxAzureLocations = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxAccountName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxStorageId = new System.Windows.Forms.TextBox();
            this.labelErrorMessage = new System.Windows.Forms.Label();
            this.buttonCheckAvail = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxRG = new System.Windows.Forms.TextBox();
            this.progressBarCreation = new System.Windows.Forms.ProgressBar();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonNext
            // 
            resources.ApplyResources(this.buttonCreate, "buttonNext");
            this.buttonCreate.Name = "buttonNext";
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
            // labelAssetName
            // 
            resources.ApplyResources(this.labelAssetName, "labelAssetName");
            this.labelAssetName.Name = "labelAssetName";
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
            // labelErrorMessage
            // 
            resources.ApplyResources(this.labelErrorMessage, "labelErrorMessage");
            this.labelErrorMessage.ForeColor = System.Drawing.Color.Red;
            this.labelErrorMessage.Name = "labelErrorMessage";
            // 
            // buttonCheckAvail
            // 
            resources.ApplyResources(this.buttonCheckAvail, "buttonCheckAvail");
            this.buttonCheckAvail.Name = "buttonCheckAvail";
            this.buttonCheckAvail.UseVisualStyleBackColor = true;
            this.buttonCheckAvail.Click += new System.EventHandler(this.buttonCheckAvail_Click);
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.buttonCheckAvail);
            this.groupBox1.Controls.Add(this.labelErrorMessage);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBoxAccountName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.comboBoxAzureLocations);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
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
            // CreateAccount
            // 
            this.AcceptButton = this.buttonCreate;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.progressBarCreation);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxRG);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxStorageId);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labelAssetName);
            this.Name = "CreateAccount";
            this.Load += new System.EventHandler(this.CreateAccount_Load);
            this.Shown += new System.EventHandler(this.CreateAccount_Shown);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button buttonCreate;
        public System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelAssetName;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.ComboBox comboBoxAzureLocations;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxAccountName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxStorageId;
        private System.Windows.Forms.Label labelErrorMessage;
        private System.Windows.Forms.Button buttonCheckAvail;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxRG;
        private System.Windows.Forms.ProgressBar progressBarCreation;
    }
}