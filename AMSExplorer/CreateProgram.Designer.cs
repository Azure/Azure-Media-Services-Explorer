namespace AMSExplorer
{
    partial class CreateProgram
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateProgram));
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.textBoxAssetName = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDownArchiveMinutes = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.numericUpDownArchiveHours = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownArchiveDays = new System.Windows.Forms.NumericUpDown();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textboxprogramname = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.checkBoxCreateLocator = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.checkBoxAddScaleUnit = new System.Windows.Forms.CheckBox();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownArchiveMinutes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownArchiveHours)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownArchiveDays)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(111, 392);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(113, 32);
            this.buttonOk.TabIndex = 10;
            this.buttonOk.Text = "Create Program";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(245, 392);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(113, 32);
            this.buttonCancel.TabIndex = 11;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.textBoxAssetName);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.numericUpDownArchiveMinutes);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.numericUpDownArchiveHours);
            this.groupBox4.Controls.Add(this.numericUpDownArchiveDays);
            this.groupBox4.Controls.Add(this.pictureBox1);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Location = new System.Drawing.Point(30, 196);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(433, 100);
            this.groupBox4.TabIndex = 43;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Settings";
            // 
            // textBoxAssetName
            // 
            this.textBoxAssetName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxAssetName.Location = new System.Drawing.Point(119, 24);
            this.textBoxAssetName.Name = "textBoxAssetName";
            this.textBoxAssetName.Size = new System.Drawing.Size(297, 20);
            this.textBoxAssetName.TabIndex = 6;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(292, 51);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(44, 13);
            this.label11.TabIndex = 63;
            this.label11.Text = "Minutes";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 13);
            this.label4.TabIndex = 48;
            this.label4.Text = "New asset name :";
            // 
            // numericUpDownArchiveMinutes
            // 
            this.numericUpDownArchiveMinutes.Location = new System.Drawing.Point(295, 67);
            this.numericUpDownArchiveMinutes.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.numericUpDownArchiveMinutes.Name = "numericUpDownArchiveMinutes";
            this.numericUpDownArchiveMinutes.Size = new System.Drawing.Size(47, 20);
            this.numericUpDownArchiveMinutes.TabIndex = 4;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(239, 51);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(35, 13);
            this.label10.TabIndex = 61;
            this.label10.Text = "Hours";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(186, 51);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(31, 13);
            this.label9.TabIndex = 60;
            this.label9.Text = "Days";
            // 
            // numericUpDownArchiveHours
            // 
            this.numericUpDownArchiveHours.Location = new System.Drawing.Point(242, 67);
            this.numericUpDownArchiveHours.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.numericUpDownArchiveHours.Name = "numericUpDownArchiveHours";
            this.numericUpDownArchiveHours.Size = new System.Drawing.Size(47, 20);
            this.numericUpDownArchiveHours.TabIndex = 3;
            // 
            // numericUpDownArchiveDays
            // 
            this.numericUpDownArchiveDays.Location = new System.Drawing.Point(189, 67);
            this.numericUpDownArchiveDays.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownArchiveDays.Name = "numericUpDownArchiveDays";
            this.numericUpDownArchiveDays.Size = new System.Drawing.Size(47, 20);
            this.numericUpDownArchiveDays.TabIndex = 2;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::AMSExplorer.Bitmaps.archive;
            this.pictureBox1.Location = new System.Drawing.Point(24, 64);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 49;
            this.pictureBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(56, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Archive Window Length :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 13);
            this.label3.TabIndex = 45;
            this.label3.Text = "Program name :";
            // 
            // textboxprogramname
            // 
            this.textboxprogramname.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textboxprogramname.Location = new System.Drawing.Point(30, 98);
            this.textboxprogramname.Name = "textboxprogramname";
            this.textboxprogramname.Size = new System.Drawing.Size(433, 20);
            this.textboxprogramname.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 133);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 47;
            this.label1.Text = "Description :";
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDescription.Location = new System.Drawing.Point(30, 149);
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(434, 20);
            this.textBoxDescription.TabIndex = 1;
            // 
            // checkBoxCreateLocator
            // 
            this.checkBoxCreateLocator.AutoSize = true;
            this.checkBoxCreateLocator.Location = new System.Drawing.Point(30, 340);
            this.checkBoxCreateLocator.Name = "checkBoxCreateLocator";
            this.checkBoxCreateLocator.Size = new System.Drawing.Size(292, 17);
            this.checkBoxCreateLocator.TabIndex = 9;
            this.checkBoxCreateLocator.Text = "Publish this asset now (default locator duration: {0} days)";
            this.checkBoxCreateLocator.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label5.Location = new System.Drawing.Point(27, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(436, 49);
            this.label5.TabIndex = 48;
            this.label5.Text = "Live programs will automatically convert to on demand content when the program is" +
    " stopped and will be available on the ASSETS tab.\r\nAll publish URLs remain the s" +
    "ame after the live program completes. ";
            // 
            // checkBoxAddScaleUnit
            // 
            this.checkBoxAddScaleUnit.AutoSize = true;
            this.checkBoxAddScaleUnit.Location = new System.Drawing.Point(30, 317);
            this.checkBoxAddScaleUnit.Name = "checkBoxAddScaleUnit";
            this.checkBoxAddScaleUnit.Size = new System.Drawing.Size(162, 17);
            this.checkBoxAddScaleUnit.TabIndex = 49;
            this.checkBoxAddScaleUnit.Text = "Add one scale streaming unit";
            this.checkBoxAddScaleUnit.UseVisualStyleBackColor = true;
            // 
            // CreateProgram
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(490, 441);
            this.Controls.Add(this.checkBoxAddScaleUnit);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.checkBoxCreateLocator);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxDescription);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textboxprogramname);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.buttonCancel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CreateProgram";
            this.Text = "Create a new program for channel {0}";
            this.Load += new System.EventHandler(this.CreateLocator_Load);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownArchiveMinutes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownArchiveHours)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownArchiveDays)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button buttonOk;
        public System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textboxprogramname;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown numericUpDownArchiveMinutes;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown numericUpDownArchiveHours;
        private System.Windows.Forms.NumericUpDown numericUpDownArchiveDays;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.CheckBox checkBoxCreateLocator;
        private System.Windows.Forms.TextBox textBoxAssetName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox checkBoxAddScaleUnit;
    }
}