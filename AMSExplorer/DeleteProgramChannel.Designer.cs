namespace AMSExplorer
{
    partial class DeleteProgramChannel
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
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelmain = new System.Windows.Forms.Label();
            this.checkBoxDeleteAsset = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(262, 13);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(113, 23);
            this.buttonOk.TabIndex = 10;
            this.buttonOk.Text = "Delete Program(s)";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(381, 13);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(99, 23);
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
            this.checkBoxDeleteAsset.Location = new System.Drawing.Point(30, 68);
            this.checkBoxDeleteAsset.Name = "checkBoxDeleteAsset";
            this.checkBoxDeleteAsset.Size = new System.Drawing.Size(149, 17);
            this.checkBoxDeleteAsset.TabIndex = 49;
            this.checkBoxDeleteAsset.Text = "Delete the related asset(s)";
            this.checkBoxDeleteAsset.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label5.Location = new System.Drawing.Point(185, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(166, 17);
            this.label5.TabIndex = 50;
            this.label5.Text = "(user data will be deleted)";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonOk);
            this.panel1.Location = new System.Drawing.Point(-2, 116);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(494, 48);
            this.panel1.TabIndex = 63;
            // 
            // DeleteProgramChannel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(490, 164);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.checkBoxDeleteAsset);
            this.Controls.Add(this.labelmain);
            this.Name = "DeleteProgramChannel";
            this.Text = "Delete program(s)";
            this.Load += new System.EventHandler(this.DeleteProgramChannel_Load);
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
    }
}