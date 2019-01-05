namespace AMSExplorer
{
    partial class DeleteLiveOutputEvent
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeleteLiveOutputEvent));
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelmain = new System.Windows.Forms.Label();
            this.checkBoxDeleteAsset = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
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
            // labelmain
            // 
            this.labelmain.ForeColor = System.Drawing.SystemColors.WindowText;
            resources.ApplyResources(this.labelmain, "labelmain");
            this.labelmain.Name = "labelmain";
            // 
            // checkBoxDeleteAsset
            // 
            resources.ApplyResources(this.checkBoxDeleteAsset, "checkBoxDeleteAsset");
            this.checkBoxDeleteAsset.Name = "checkBoxDeleteAsset";
            this.checkBoxDeleteAsset.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.ForeColor = System.Drawing.SystemColors.WindowFrame;
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonOk);
            this.panel1.Name = "panel1";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.DarkBlue;
            this.label1.Name = "label1";
            // 
            // DeleteLiveOutputEvent
            // 
            this.AcceptButton = this.buttonOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.checkBoxDeleteAsset);
            this.Controls.Add(this.labelmain);
            this.Name = "DeleteLiveOutputEvent";
            this.Load += new System.EventHandler(this.DeleteLiveOutputEvent_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button buttonOk;
        public System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelmain;
        private System.Windows.Forms.CheckBox checkBoxDeleteAsset;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
    }
}