namespace AMSExplorer
{
    partial class AddAMSAccount2Manual
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddAMSAccount2Manual));
            this.buttonNext = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.textBoxAMSResourceId = new System.Windows.Forms.TextBox();
            this.groupBoxAADAutMode = new System.Windows.Forms.GroupBox();
            this.radioButtonAADServicePrincipal = new System.Windows.Forms.RadioButton();
            this.radioButtonAADInteractive = new System.Windows.Forms.RadioButton();
            this.textBoxAADtenantId = new System.Windows.Forms.TextBox();
            this.labelADTenant = new System.Windows.Forms.Label();
            this.labelE2 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.groupBoxAADAutMode.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonNext
            // 
            resources.ApplyResources(this.buttonNext, "buttonNext");
            this.buttonNext.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonNext.Name = "buttonNext";
            this.toolTip1.SetToolTip(this.buttonNext, resources.GetString("buttonNext.ToolTip"));
            this.buttonNext.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Name = "buttonCancel";
            this.toolTip1.SetToolTip(this.buttonCancel, resources.GetString("buttonCancel.ToolTip"));
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonNext);
            this.panel1.Name = "panel1";
            this.toolTip1.SetToolTip(this.panel1, resources.GetString("panel1.ToolTip"));
            // 
            // textBoxAMSResourceId
            // 
            resources.ApplyResources(this.textBoxAMSResourceId, "textBoxAMSResourceId");
            this.textBoxAMSResourceId.Name = "textBoxAMSResourceId";
            this.toolTip1.SetToolTip(this.textBoxAMSResourceId, resources.GetString("textBoxAMSResourceId.ToolTip"));
            // 
            // groupBoxAADAutMode
            // 
            resources.ApplyResources(this.groupBoxAADAutMode, "groupBoxAADAutMode");
            this.groupBoxAADAutMode.Controls.Add(this.radioButtonAADServicePrincipal);
            this.groupBoxAADAutMode.Controls.Add(this.radioButtonAADInteractive);
            this.groupBoxAADAutMode.Name = "groupBoxAADAutMode";
            this.groupBoxAADAutMode.TabStop = false;
            this.toolTip1.SetToolTip(this.groupBoxAADAutMode, resources.GetString("groupBoxAADAutMode.ToolTip"));
            // 
            // radioButtonAADServicePrincipal
            // 
            resources.ApplyResources(this.radioButtonAADServicePrincipal, "radioButtonAADServicePrincipal");
            this.radioButtonAADServicePrincipal.Name = "radioButtonAADServicePrincipal";
            this.toolTip1.SetToolTip(this.radioButtonAADServicePrincipal, resources.GetString("radioButtonAADServicePrincipal.ToolTip"));
            this.radioButtonAADServicePrincipal.UseVisualStyleBackColor = true;
            // 
            // radioButtonAADInteractive
            // 
            resources.ApplyResources(this.radioButtonAADInteractive, "radioButtonAADInteractive");
            this.radioButtonAADInteractive.Checked = true;
            this.radioButtonAADInteractive.Name = "radioButtonAADInteractive";
            this.radioButtonAADInteractive.TabStop = true;
            this.toolTip1.SetToolTip(this.radioButtonAADInteractive, resources.GetString("radioButtonAADInteractive.ToolTip"));
            this.radioButtonAADInteractive.UseVisualStyleBackColor = true;
            // 
            // textBoxAADtenantId
            // 
            resources.ApplyResources(this.textBoxAADtenantId, "textBoxAADtenantId");
            this.textBoxAADtenantId.Name = "textBoxAADtenantId";
            this.toolTip1.SetToolTip(this.textBoxAADtenantId, resources.GetString("textBoxAADtenantId.ToolTip"));
            // 
            // labelADTenant
            // 
            resources.ApplyResources(this.labelADTenant, "labelADTenant");
            this.labelADTenant.Name = "labelADTenant";
            this.toolTip1.SetToolTip(this.labelADTenant, resources.GetString("labelADTenant.ToolTip"));
            // 
            // labelE2
            // 
            resources.ApplyResources(this.labelE2, "labelE2");
            this.labelE2.Name = "labelE2";
            this.toolTip1.SetToolTip(this.labelE2, resources.GetString("labelE2.ToolTip"));
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label2.Name = "label2";
            this.toolTip1.SetToolTip(this.label2, resources.GetString("label2.ToolTip"));
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label1.Name = "label1";
            this.toolTip1.SetToolTip(this.label1, resources.GetString("label1.ToolTip"));
            // 
            // AddAMSAccount2Manual
            // 
            this.AcceptButton = this.buttonNext;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.groupBoxAADAutMode);
            this.Controls.Add(this.textBoxAMSResourceId);
            this.Controls.Add(this.textBoxAADtenantId);
            this.Controls.Add(this.labelADTenant);
            this.Controls.Add(this.labelE2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Name = "AddAMSAccount2Manual";
            this.toolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.Load += new System.EventHandler(this.AddAMSAccount2_Load);
            this.panel1.ResumeLayout(false);
            this.groupBoxAADAutMode.ResumeLayout(false);
            this.groupBoxAADAutMode.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button buttonNext;
        public System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox groupBoxAADAutMode;
        private System.Windows.Forms.Label labelADTenant;
        private System.Windows.Forms.Label labelE2;
        public System.Windows.Forms.RadioButton radioButtonAADServicePrincipal;
        public System.Windows.Forms.RadioButton radioButtonAADInteractive;
        public System.Windows.Forms.TextBox textBoxAMSResourceId;
        public System.Windows.Forms.TextBox textBoxAADtenantId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}