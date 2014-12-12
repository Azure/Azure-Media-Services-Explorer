namespace AMSExplorer
{
    partial class ChangeEncodingRU
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
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxSubId = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numericUpDownRUNumber = new System.Windows.Forms.NumericUpDown();
            this.textBoxCertThumbprint = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.linkLabelAttach = new System.Windows.Forms.LinkLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRUNumber)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonUpdate.Location = new System.Drawing.Point(462, 12);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(103, 23);
            this.buttonUpdate.TabIndex = 3;
            this.buttonUpdate.Text = "Scale units";
            this.buttonUpdate.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(571, 12);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(103, 23);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 13);
            this.label1.TabIndex = 38;
            this.label1.Text = "Azure Subscription ID :";
            // 
            // textBoxSubId
            // 
            this.textBoxSubId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSubId.Location = new System.Drawing.Point(31, 52);
            this.textBoxSubId.Name = "textBoxSubId";
            this.textBoxSubId.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.textBoxSubId.Size = new System.Drawing.Size(617, 20);
            this.textBoxSubId.TabIndex = 0;
            this.textBoxSubId.TextChanged += new System.EventHandler(this.textBoxURL_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 43;
            this.label3.Text = "Number :";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.numericUpDownRUNumber);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(31, 161);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(617, 158);
            this.groupBox1.TabIndex = 47;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Encoding Reserved Units";
            // 
            // numericUpDownRUNumber
            // 
            this.numericUpDownRUNumber.Location = new System.Drawing.Point(63, 25);
            this.numericUpDownRUNumber.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownRUNumber.Name = "numericUpDownRUNumber";
            this.numericUpDownRUNumber.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownRUNumber.TabIndex = 2;
            // 
            // textBoxCertThumbprint
            // 
            this.textBoxCertThumbprint.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxCertThumbprint.Location = new System.Drawing.Point(31, 104);
            this.textBoxCertThumbprint.Name = "textBoxCertThumbprint";
            this.textBoxCertThumbprint.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.textBoxCertThumbprint.Size = new System.Drawing.Size(617, 20);
            this.textBoxCertThumbprint.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(181, 13);
            this.label2.TabIndex = 49;
            this.label2.Text = "Management Certificate Thumbprint :";
            // 
            // linkLabelAttach
            // 
            this.linkLabelAttach.AutoSize = true;
            this.linkLabelAttach.Location = new System.Drawing.Point(28, 127);
            this.linkLabelAttach.Name = "linkLabelAttach";
            this.linkLabelAttach.Size = new System.Drawing.Size(272, 13);
            this.linkLabelAttach.TabIndex = 50;
            this.linkLabelAttach.TabStop = true;
            this.linkLabelAttach.Text = "See how to create and upload a management certificate";
            this.linkLabelAttach.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelAttach_LinkClicked);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonUpdate);
            this.panel1.Location = new System.Drawing.Point(-2, 344);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(689, 48);
            this.panel1.TabIndex = 57;
            // 
            // ChangeEncodingRU
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(684, 391);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.linkLabelAttach);
            this.Controls.Add(this.textBoxCertThumbprint);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.textBoxSubId);
            this.Controls.Add(this.label1);
            this.Name = "ChangeEncodingRU";
            this.Text = "Change number of encoding/processing reserved units";
            this.Load += new System.EventHandler(this.AttachStorage_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRUNumber)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.Button buttonCancel;
        public System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxSubId;
        public System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxCertThumbprint;
        public System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel linkLabelAttach;
        private System.Windows.Forms.NumericUpDown numericUpDownRUNumber;
        private System.Windows.Forms.Panel panel1;
    }
}