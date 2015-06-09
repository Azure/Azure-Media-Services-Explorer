namespace AMSExplorer
{
    partial class IndexerOptions
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
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.checkBoxTTML = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxWEBVTT = new System.Windows.Forms.CheckBox();
            this.checkBoxSAMI = new System.Windows.Forms.CheckBox();
            this.checkBoxAIB = new System.Windows.Forms.CheckBox();
            this.checkBoxKeywords = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(247, 13);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(99, 23);
            this.buttonCancel.TabIndex = 16;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonOk.Location = new System.Drawing.Point(142, 13);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(99, 23);
            this.buttonOk.TabIndex = 17;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonOk);
            this.panel1.Location = new System.Drawing.Point(-2, 164);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(358, 48);
            this.panel1.TabIndex = 66;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Key";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Value";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // checkBoxTTML
            // 
            this.checkBoxTTML.AutoSize = true;
            this.checkBoxTTML.Checked = true;
            this.checkBoxTTML.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxTTML.Location = new System.Drawing.Point(29, 33);
            this.checkBoxTTML.Name = "checkBoxTTML";
            this.checkBoxTTML.Size = new System.Drawing.Size(55, 17);
            this.checkBoxTTML.TabIndex = 67;
            this.checkBoxTTML.Text = "TTML";
            this.checkBoxTTML.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxWEBVTT);
            this.groupBox1.Controls.Add(this.checkBoxSAMI);
            this.groupBox1.Controls.Add(this.checkBoxTTML);
            this.groupBox1.Location = new System.Drawing.Point(12, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(140, 118);
            this.groupBox1.TabIndex = 68;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Caption Formats";
            // 
            // checkBoxWEBVTT
            // 
            this.checkBoxWEBVTT.AutoSize = true;
            this.checkBoxWEBVTT.Checked = true;
            this.checkBoxWEBVTT.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxWEBVTT.Location = new System.Drawing.Point(29, 79);
            this.checkBoxWEBVTT.Name = "checkBoxWEBVTT";
            this.checkBoxWEBVTT.Size = new System.Drawing.Size(70, 17);
            this.checkBoxWEBVTT.TabIndex = 69;
            this.checkBoxWEBVTT.Text = "WebVTT";
            this.checkBoxWEBVTT.UseVisualStyleBackColor = true;
            // 
            // checkBoxSAMI
            // 
            this.checkBoxSAMI.AutoSize = true;
            this.checkBoxSAMI.Checked = true;
            this.checkBoxSAMI.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSAMI.Location = new System.Drawing.Point(29, 56);
            this.checkBoxSAMI.Name = "checkBoxSAMI";
            this.checkBoxSAMI.Size = new System.Drawing.Size(52, 17);
            this.checkBoxSAMI.TabIndex = 68;
            this.checkBoxSAMI.Text = "SAMI";
            this.checkBoxSAMI.UseVisualStyleBackColor = true;
            // 
            // checkBoxAIB
            // 
            this.checkBoxAIB.AutoSize = true;
            this.checkBoxAIB.Checked = true;
            this.checkBoxAIB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAIB.Location = new System.Drawing.Point(23, 38);
            this.checkBoxAIB.Name = "checkBoxAIB";
            this.checkBoxAIB.Size = new System.Drawing.Size(132, 17);
            this.checkBoxAIB.TabIndex = 70;
            this.checkBoxAIB.Text = "Audio Index Blob (AIB)";
            this.toolTip1.SetToolTip(this.checkBoxAIB, "For use with SQL Server and the customer Indexer IFilter");
            this.checkBoxAIB.UseVisualStyleBackColor = true;
            // 
            // checkBoxKeywords
            // 
            this.checkBoxKeywords.AutoSize = true;
            this.checkBoxKeywords.Checked = true;
            this.checkBoxKeywords.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxKeywords.Location = new System.Drawing.Point(23, 61);
            this.checkBoxKeywords.Name = "checkBoxKeywords";
            this.checkBoxKeywords.Size = new System.Drawing.Size(72, 17);
            this.checkBoxKeywords.TabIndex = 71;
            this.checkBoxKeywords.Text = "Keywords";
            this.checkBoxKeywords.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBoxKeywords);
            this.groupBox2.Controls.Add(this.checkBoxAIB);
            this.groupBox2.Location = new System.Drawing.Point(158, 24);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(181, 118);
            this.groupBox2.TabIndex = 70;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Other";
            // 
            // IndexerOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(356, 212);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Name = "IndexerOptions";
            this.Text = "Generation Options";
            this.Load += new System.EventHandler(this.IndexerOptions_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Button buttonOk;
        public System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox checkBoxTTML;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBoxWEBVTT;
        private System.Windows.Forms.CheckBox checkBoxSAMI;
        private System.Windows.Forms.CheckBox checkBoxAIB;
        private System.Windows.Forms.CheckBox checkBoxKeywords;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}