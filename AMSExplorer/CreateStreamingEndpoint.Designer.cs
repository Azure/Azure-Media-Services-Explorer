namespace AMSExplorer
{
    partial class CreateStreamingEndpoint
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
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.numericUpDownRU = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textboxoriginname = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxOriginDescription = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.checkBoxEnableAzureCDN = new System.Windows.Forms.CheckBox();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRU)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(227, 13);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(148, 23);
            this.buttonOk.TabIndex = 3;
            this.buttonOk.Text = "Create streaming endpoint";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(381, 13);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(99, 23);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.checkBoxEnableAzureCDN);
            this.groupBox4.Controls.Add(this.numericUpDownRU);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Location = new System.Drawing.Point(14, 132);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(449, 128);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Settings";
            // 
            // numericUpDownRU
            // 
            this.numericUpDownRU.Location = new System.Drawing.Point(154, 20);
            this.numericUpDownRU.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownRU.Name = "numericUpDownRU";
            this.numericUpDownRU.Size = new System.Drawing.Size(72, 20);
            this.numericUpDownRU.TabIndex = 0;
            this.numericUpDownRU.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Scale units :";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 13);
            this.label3.TabIndex = 45;
            this.label3.Text = "Streaming endpoint name :";
            // 
            // textboxoriginname
            // 
            this.textboxoriginname.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textboxoriginname.Location = new System.Drawing.Point(14, 36);
            this.textboxoriginname.Name = "textboxoriginname";
            this.textboxoriginname.Size = new System.Drawing.Size(449, 20);
            this.textboxoriginname.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 47;
            this.label1.Text = "Description :";
            // 
            // textBoxOriginDescription
            // 
            this.textBoxOriginDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxOriginDescription.Location = new System.Drawing.Point(14, 89);
            this.textBoxOriginDescription.Name = "textBoxOriginDescription";
            this.textBoxOriginDescription.Size = new System.Drawing.Size(449, 20);
            this.textBoxOriginDescription.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonOk);
            this.panel1.Location = new System.Drawing.Point(-2, 393);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(494, 48);
            this.panel1.TabIndex = 62;
            // 
            // checkBoxEnableAzureCDN
            // 
            this.checkBoxEnableAzureCDN.AutoSize = true;
            this.checkBoxEnableAzureCDN.Location = new System.Drawing.Point(10, 57);
            this.checkBoxEnableAzureCDN.Name = "checkBoxEnableAzureCDN";
            this.checkBoxEnableAzureCDN.Size = new System.Drawing.Size(115, 17);
            this.checkBoxEnableAzureCDN.TabIndex = 63;
            this.checkBoxEnableAzureCDN.Text = "Enable Azure CDN";
            this.checkBoxEnableAzureCDN.UseVisualStyleBackColor = true;
            // 
            // CreateStreamingEndpoint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(490, 441);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxOriginDescription);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textboxoriginname);
            this.Controls.Add(this.groupBox4);
            this.Name = "CreateStreamingEndpoint";
            this.Text = "Create a streaming endpoint";
            this.Load += new System.EventHandler(this.CreateOrigin_Load);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRU)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button buttonOk;
        public System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textboxoriginname;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxOriginDescription;
        private System.Windows.Forms.NumericUpDown numericUpDownRU;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox checkBoxEnableAzureCDN;
    }
}