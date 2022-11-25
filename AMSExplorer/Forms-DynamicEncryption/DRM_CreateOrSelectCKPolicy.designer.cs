namespace AMSExplorer
{
    partial class DRM_CreateOrSelectCKPolicy
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DRM_CreateOrSelectCKPolicy));
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelstep = new System.Windows.Forms.Label();
            this.listViewContentKeyPolicies = new AMSExplorer.ListViewContentKeyPolicies();
            this.radioButtonCreate = new System.Windows.Forms.RadioButton();
            this.radioButtonSelect = new System.Windows.Forms.RadioButton();
            this.textBoxPolicyName = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOk
            // 
            resources.ApplyResources(this.buttonOk, "buttonOk");
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonOk);
            this.panel1.Name = "panel1";
            // 
            // labelstep
            // 
            resources.ApplyResources(this.labelstep, "labelstep");
            this.labelstep.ForeColor = System.Drawing.Color.DarkBlue;
            this.labelstep.Name = "labelstep";
            // 
            // listViewContentKeyPolicies
            // 
            resources.ApplyResources(this.listViewContentKeyPolicies, "listViewContentKeyPolicies");
            this.listViewContentKeyPolicies.FullRowSelect = true;
            this.listViewContentKeyPolicies.MultiSelect = false;
            this.listViewContentKeyPolicies.Name = "listViewContentKeyPolicies";
            this.listViewContentKeyPolicies.Tag = -1;
            this.listViewContentKeyPolicies.UseCompatibleStateImageBehavior = false;
            this.listViewContentKeyPolicies.View = System.Windows.Forms.View.Details;
            this.listViewContentKeyPolicies.SelectedIndexChanged += new System.EventHandler(this.listViewContentKeyPolicies_SelectedIndexChanged);
            // 
            // radioButtonCreate
            // 
            resources.ApplyResources(this.radioButtonCreate, "radioButtonCreate");
            this.radioButtonCreate.Checked = true;
            this.radioButtonCreate.Name = "radioButtonCreate";
            this.radioButtonCreate.TabStop = true;
            this.radioButtonCreate.UseVisualStyleBackColor = true;
            // 
            // radioButtonSelect
            // 
            resources.ApplyResources(this.radioButtonSelect, "radioButtonSelect");
            this.radioButtonSelect.Name = "radioButtonSelect";
            this.radioButtonSelect.UseVisualStyleBackColor = true;
            this.radioButtonSelect.CheckedChanged += new System.EventHandler(this.radioButtonSelect_CheckedChanged);
            // 
            // textBoxPolicyName
            // 
            resources.ApplyResources(this.textBoxPolicyName, "textBoxPolicyName");
            this.textBoxPolicyName.Name = "textBoxPolicyName";
            // 
            // DRM_CreateOrSelectCKPolicy
            // 
            this.AcceptButton = this.buttonOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.textBoxPolicyName);
            this.Controls.Add(this.radioButtonSelect);
            this.Controls.Add(this.radioButtonCreate);
            this.Controls.Add(this.listViewContentKeyPolicies);
            this.Controls.Add(this.labelstep);
            this.Controls.Add(this.panel1);
            this.Name = "DRM_CreateOrSelectCKPolicy";
            this.Load += new System.EventHandler(this.DRM_CreateOrSelectCKPolicy_Load);
            this.Shown += new System.EventHandler(this.DRM_CreateOrSelectCKPolicy_Shown);
            this.DpiChanged += new System.Windows.Forms.DpiChangedEventHandler(this.DRM_CreateOrSelectCKPolicy_DpiChanged);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.Button buttonOk;
        public System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelstep;
        private ListViewContentKeyPolicies listViewContentKeyPolicies;
        private System.Windows.Forms.RadioButton radioButtonCreate;
        private System.Windows.Forms.RadioButton radioButtonSelect;
        private System.Windows.Forms.TextBox textBoxPolicyName;
    }
}