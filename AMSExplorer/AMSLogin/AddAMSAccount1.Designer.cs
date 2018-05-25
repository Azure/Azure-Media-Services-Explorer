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
            this.panel1.SuspendLayout();
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
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonNext);
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
            // AddAMSAccount1
            // 
            this.AcceptButton = this.buttonNext;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.checkBoxSelectUser);
            this.Controls.Add(this.radioButtonAddAMSAccount);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labelAssetName);
            this.Name = "AddAMSAccount1";
            this.Load += new System.EventHandler(this.AddAMSAccount1_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button buttonNext;
        public System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelAssetName;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radioButtonAddAMSAccount;
        private System.Windows.Forms.CheckBox checkBoxSelectUser;
    }
}