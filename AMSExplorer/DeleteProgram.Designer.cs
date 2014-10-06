namespace AMSExplorer
{
    partial class DeleteProgram
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeleteProgram));
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelmain = new System.Windows.Forms.Label();
            this.checkBoxDeleteAsset = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(111, 109);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(113, 32);
            this.buttonOk.TabIndex = 10;
            this.buttonOk.Text = "Delete Program(s)";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(245, 109);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(113, 32);
            this.buttonCancel.TabIndex = 11;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // labelmain
            // 
            this.labelmain.ForeColor = System.Drawing.SystemColors.WindowText;
            this.labelmain.Location = new System.Drawing.Point(27, 19);
            this.labelmain.Name = "labelmain";
            this.labelmain.Size = new System.Drawing.Size(436, 33);
            this.labelmain.TabIndex = 48;
            this.labelmain.Text = "text";
            // 
            // checkBoxDeleteAsset
            // 
            this.checkBoxDeleteAsset.AutoSize = true;
            this.checkBoxDeleteAsset.Location = new System.Drawing.Point(84, 70);
            this.checkBoxDeleteAsset.Name = "checkBoxDeleteAsset";
            this.checkBoxDeleteAsset.Size = new System.Drawing.Size(149, 17);
            this.checkBoxDeleteAsset.TabIndex = 49;
            this.checkBoxDeleteAsset.Text = "Delete the related asset(s)";
            this.checkBoxDeleteAsset.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label5.Location = new System.Drawing.Point(239, 70);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(166, 17);
            this.label5.TabIndex = 50;
            this.label5.Text = "(user data will be deleted)";
            // 
            // DeleteProgram
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(490, 164);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.checkBoxDeleteAsset);
            this.Controls.Add(this.labelmain);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.buttonCancel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DeleteProgram";
            this.Text = "Delete program(s)";
            this.Load += new System.EventHandler(this.DeleteProgram_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button buttonOk;
        public System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelmain;
        private System.Windows.Forms.CheckBox checkBoxDeleteAsset;
        private System.Windows.Forms.Label label5;
    }
}