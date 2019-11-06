namespace AMSExplorer
{
    partial class DeleteKeyAndPolicy
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeleteKeyAndPolicy));
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.checkBoxDeleteKeys = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelTitle = new System.Windows.Forms.Label();
            this.checkBoxDeleteAutPol = new System.Windows.Forms.CheckBox();
            this.checkBoxDeleteDeliveryPol = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
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
            // checkBoxDeleteKeys
            // 
            resources.ApplyResources(this.checkBoxDeleteKeys, "checkBoxDeleteKeys");
            this.checkBoxDeleteKeys.Name = "checkBoxDeleteKeys";
            this.checkBoxDeleteKeys.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonOk);
            this.panel1.Name = "panel1";
            // 
            // labelTitle
            // 
            resources.ApplyResources(this.labelTitle, "labelTitle");
            this.labelTitle.ForeColor = System.Drawing.Color.DarkBlue;
            this.labelTitle.Name = "labelTitle";
            // 
            // checkBoxDeleteAutPol
            // 
            resources.ApplyResources(this.checkBoxDeleteAutPol, "checkBoxDeleteAutPol");
            this.checkBoxDeleteAutPol.Name = "checkBoxDeleteAutPol";
            this.checkBoxDeleteAutPol.UseVisualStyleBackColor = true;
            // 
            // checkBoxDeleteDeliveryPol
            // 
            resources.ApplyResources(this.checkBoxDeleteDeliveryPol, "checkBoxDeleteDeliveryPol");
            this.checkBoxDeleteDeliveryPol.Name = "checkBoxDeleteDeliveryPol";
            this.checkBoxDeleteDeliveryPol.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label2.Name = "label2";
            // 
            // DeleteKeyAndPolicy
            // 
            this.AcceptButton = this.buttonOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.checkBoxDeleteDeliveryPol);
            this.Controls.Add(this.checkBoxDeleteAutPol);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.checkBoxDeleteKeys);
            this.Name = "DeleteKeyAndPolicy";
            this.Load += new System.EventHandler(this.DeleteKeyAndPolicy_Load);
            this.DpiChanged += new System.Windows.Forms.DpiChangedEventHandler(this.DeleteKeyAndPolicy_DpiChanged);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button buttonOk;
        public System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.CheckBox checkBoxDeleteKeys;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.CheckBox checkBoxDeleteAutPol;
        private System.Windows.Forms.CheckBox checkBoxDeleteDeliveryPol;
        private System.Windows.Forms.Label label2;
    }
}