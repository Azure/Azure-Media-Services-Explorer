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
            this.groupBoxOther = new System.Windows.Forms.GroupBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBoxOther.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(288, 15);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(115, 27);
            this.buttonCancel.TabIndex = 16;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonOk.Location = new System.Drawing.Point(166, 15);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(115, 27);
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
            this.panel1.Location = new System.Drawing.Point(-2, 189);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(418, 55);
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
            this.checkBoxTTML.Location = new System.Drawing.Point(34, 38);
            this.checkBoxTTML.Name = "checkBoxTTML";
            this.checkBoxTTML.Size = new System.Drawing.Size(57, 19);
            this.checkBoxTTML.TabIndex = 67;
            this.checkBoxTTML.Text = "TTML";
            this.checkBoxTTML.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxWEBVTT);
            this.groupBox1.Controls.Add(this.checkBoxSAMI);
            this.groupBox1.Controls.Add(this.checkBoxTTML);
            this.groupBox1.Location = new System.Drawing.Point(12, 39);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(163, 136);
            this.groupBox1.TabIndex = 68;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Caption Formats";
            // 
            // checkBoxWEBVTT
            // 
            this.checkBoxWEBVTT.AutoSize = true;
            this.checkBoxWEBVTT.Checked = true;
            this.checkBoxWEBVTT.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxWEBVTT.Location = new System.Drawing.Point(34, 91);
            this.checkBoxWEBVTT.Name = "checkBoxWEBVTT";
            this.checkBoxWEBVTT.Size = new System.Drawing.Size(71, 19);
            this.checkBoxWEBVTT.TabIndex = 69;
            this.checkBoxWEBVTT.Text = "WebVTT";
            this.checkBoxWEBVTT.UseVisualStyleBackColor = true;
            // 
            // checkBoxSAMI
            // 
            this.checkBoxSAMI.AutoSize = true;
            this.checkBoxSAMI.Checked = true;
            this.checkBoxSAMI.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSAMI.Location = new System.Drawing.Point(34, 65);
            this.checkBoxSAMI.Name = "checkBoxSAMI";
            this.checkBoxSAMI.Size = new System.Drawing.Size(54, 19);
            this.checkBoxSAMI.TabIndex = 68;
            this.checkBoxSAMI.Text = "SAMI";
            this.checkBoxSAMI.UseVisualStyleBackColor = true;
            // 
            // checkBoxAIB
            // 
            this.checkBoxAIB.AutoSize = true;
            this.checkBoxAIB.Checked = true;
            this.checkBoxAIB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAIB.Location = new System.Drawing.Point(27, 38);
            this.checkBoxAIB.Name = "checkBoxAIB";
            this.checkBoxAIB.Size = new System.Drawing.Size(145, 19);
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
            this.checkBoxKeywords.Location = new System.Drawing.Point(27, 65);
            this.checkBoxKeywords.Name = "checkBoxKeywords";
            this.checkBoxKeywords.Size = new System.Drawing.Size(77, 19);
            this.checkBoxKeywords.TabIndex = 71;
            this.checkBoxKeywords.Text = "Keywords";
            this.checkBoxKeywords.UseVisualStyleBackColor = true;
            // 
            // groupBoxOther
            // 
            this.groupBoxOther.Controls.Add(this.checkBoxKeywords);
            this.groupBoxOther.Controls.Add(this.checkBoxAIB);
            this.groupBoxOther.Location = new System.Drawing.Point(190, 39);
            this.groupBoxOther.Name = "groupBoxOther";
            this.groupBoxOther.Size = new System.Drawing.Size(211, 136);
            this.groupBoxOther.TabIndex = 70;
            this.groupBoxOther.TabStop = false;
            this.groupBoxOther.Text = "Other";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DarkBlue;
            this.label5.Location = new System.Drawing.Point(12, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(138, 20);
            this.label5.TabIndex = 74;
            this.label5.Text = "Generation Options";
            // 
            // IndexerOptions
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(415, 245);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.groupBoxOther);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "IndexerOptions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Generation Options";
            this.Load += new System.EventHandler(this.IndexerOptions_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBoxOther.ResumeLayout(false);
            this.groupBoxOther.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.GroupBox groupBoxOther;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label5;
    }
}