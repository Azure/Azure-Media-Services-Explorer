namespace AMSExplorer
{
    partial class AmsLoginServicePrincipal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AmsLoginServicePrincipal));
            buttonCancel = new System.Windows.Forms.Button();
            panel1 = new System.Windows.Forms.Panel();
            buttonImport = new System.Windows.Forms.Button();
            label2 = new System.Windows.Forms.Label();
            textBoxClientId = new System.Windows.Forms.TextBox();
            textBoxClientSecret = new System.Windows.Forms.TextBox();
            labelE1 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // buttonCancel
            // 
            resources.ApplyResources(buttonCancel, "buttonCancel");
            buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            buttonCancel.Name = "buttonCancel";
            buttonCancel.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.BackColor = System.Drawing.SystemColors.Control;
            panel1.Controls.Add(buttonImport);
            panel1.Controls.Add(buttonCancel);
            resources.ApplyResources(panel1, "panel1");
            panel1.Name = "panel1";
            // 
            // buttonImport
            // 
            resources.ApplyResources(buttonImport, "buttonImport");
            buttonImport.DialogResult = System.Windows.Forms.DialogResult.OK;
            buttonImport.Name = "buttonImport";
            buttonImport.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.ForeColor = System.Drawing.Color.DarkBlue;
            label2.Name = "label2";
            // 
            // textBoxClientId
            // 
            resources.ApplyResources(textBoxClientId, "textBoxClientId");
            textBoxClientId.Name = "textBoxClientId";
            // 
            // textBoxClientSecret
            // 
            resources.ApplyResources(textBoxClientSecret, "textBoxClientSecret");
            textBoxClientSecret.Name = "textBoxClientSecret";
            textBoxClientSecret.UseSystemPasswordChar = true;
            // 
            // labelE1
            // 
            resources.ApplyResources(labelE1, "labelE1");
            labelE1.Name = "labelE1";
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // AmsLoginServicePrincipal
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            BackColor = System.Drawing.SystemColors.Window;
            CancelButton = buttonCancel;
            Controls.Add(label1);
            Controls.Add(labelE1);
            Controls.Add(textBoxClientSecret);
            Controls.Add(textBoxClientId);
            Controls.Add(label2);
            Controls.Add(panel1);
            Name = "AmsLoginServicePrincipal";
            Load += AmsLoginServicePrincipal_Load;
            Shown += AmsLoginServicePrincipal_Shown;
            DpiChanged += AmsLoginServicePrincipal_DpiChanged;
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxClientId;
        private System.Windows.Forms.TextBox textBoxClientSecret;
        private System.Windows.Forms.Label labelE1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonImport;
    }
}