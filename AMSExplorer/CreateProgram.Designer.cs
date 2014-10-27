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
            this.textBoxAssetName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.numericUpDownArchiveDays = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textboxprogramname = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.checkBoxCreateLocator = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.checkBoxAddScaleUnit = new System.Windows.Forms.CheckBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.numericUpDownArchiveMinutes = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label10 = new System.Windows.Forms.Label();
            this.numericUpDownArchiveHours = new System.Windows.Forms.NumericUpDown();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.labelURLFileNameWarning = new System.Windows.Forms.Label();
            this.labelManifestFile = new System.Windows.Forms.Label();
            this.labelLocatorID = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxProgramSourceURL = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.checkBoxReplica = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownArchiveDays)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownArchiveMinutes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownArchiveHours)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(121, 471);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(113, 32);
            this.buttonOk.TabIndex = 10;
            this.buttonOk.Text = "Create Program";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(255, 471);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(113, 32);
            this.buttonCancel.TabIndex = 11;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // textBoxAssetName
            // 
            this.textBoxAssetName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxAssetName.Location = new System.Drawing.Point(18, 42);
            this.textBoxAssetName.Name = "textBoxAssetName";
            this.textBoxAssetName.Size = new System.Drawing.Size(386, 20);
            this.textBoxAssetName.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 13);
            this.label4.TabIndex = 48;
            this.label4.Text = "New asset name :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(180, 83);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(31, 13);
            this.label9.TabIndex = 60;
            this.label9.Text = "Days";
            // 
            // numericUpDownArchiveDays
            // 
            this.numericUpDownArchiveDays.Location = new System.Drawing.Point(183, 99);
            this.numericUpDownArchiveDays.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownArchiveDays.Name = "numericUpDownArchiveDays";
            this.numericUpDownArchiveDays.Size = new System.Drawing.Size(47, 20);
            this.numericUpDownArchiveDays.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(50, 99);
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
            this.checkBoxCreateLocator.Location = new System.Drawing.Point(30, 428);
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
            this.checkBoxAddScaleUnit.Location = new System.Drawing.Point(30, 405);
            this.checkBoxAddScaleUnit.Name = "checkBoxAddScaleUnit";
            this.checkBoxAddScaleUnit.Size = new System.Drawing.Size(162, 17);
            this.checkBoxAddScaleUnit.TabIndex = 49;
            this.checkBoxAddScaleUnit.Text = "Add one scale streaming unit";
            this.checkBoxAddScaleUnit.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(30, 184);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(434, 206);
            this.tabControl1.TabIndex = 51;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.textBoxAssetName);
            this.tabPage1.Controls.Add(this.numericUpDownArchiveMinutes);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.pictureBox1);
            this.tabPage1.Controls.Add(this.numericUpDownArchiveDays);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.numericUpDownArchiveHours);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(426, 180);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Settings";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // numericUpDownArchiveMinutes
            // 
            this.numericUpDownArchiveMinutes.Location = new System.Drawing.Point(289, 99);
            this.numericUpDownArchiveMinutes.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.numericUpDownArchiveMinutes.Name = "numericUpDownArchiveMinutes";
            this.numericUpDownArchiveMinutes.Size = new System.Drawing.Size(47, 20);
            this.numericUpDownArchiveMinutes.TabIndex = 4;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(286, 83);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(44, 13);
            this.label11.TabIndex = 63;
            this.label11.Text = "Minutes";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::AMSExplorer.Bitmaps.archive;
            this.pictureBox1.Location = new System.Drawing.Point(18, 96);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 49;
            this.pictureBox1.TabStop = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(233, 83);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(35, 13);
            this.label10.TabIndex = 61;
            this.label10.Text = "Hours";
            // 
            // numericUpDownArchiveHours
            // 
            this.numericUpDownArchiveHours.Location = new System.Drawing.Point(236, 99);
            this.numericUpDownArchiveHours.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.numericUpDownArchiveHours.Name = "numericUpDownArchiveHours";
            this.numericUpDownArchiveHours.Size = new System.Drawing.Size(47, 20);
            this.numericUpDownArchiveHours.TabIndex = 3;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.labelURLFileNameWarning);
            this.tabPage2.Controls.Add(this.labelManifestFile);
            this.tabPage2.Controls.Add(this.labelLocatorID);
            this.tabPage2.Controls.Add(this.label12);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.textBoxProgramSourceURL);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.checkBoxReplica);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(426, 180);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Advanced";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // labelURLFileNameWarning
            // 
            this.labelURLFileNameWarning.AutoSize = true;
            this.labelURLFileNameWarning.ForeColor = System.Drawing.Color.Red;
            this.labelURLFileNameWarning.Location = new System.Drawing.Point(17, 115);
            this.labelURLFileNameWarning.Name = "labelURLFileNameWarning";
            this.labelURLFileNameWarning.Size = new System.Drawing.Size(47, 13);
            this.labelURLFileNameWarning.TabIndex = 71;
            this.labelURLFileNameWarning.Text = "Warning";
            // 
            // labelManifestFile
            // 
            this.labelManifestFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelManifestFile.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.labelManifestFile.Location = new System.Drawing.Point(95, 158);
            this.labelManifestFile.Name = "labelManifestFile";
            this.labelManifestFile.Size = new System.Drawing.Size(316, 13);
            this.labelManifestFile.TabIndex = 70;
            this.labelManifestFile.Text = "text";
            // 
            // labelLocatorID
            // 
            this.labelLocatorID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelLocatorID.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.labelLocatorID.Location = new System.Drawing.Point(95, 137);
            this.labelLocatorID.Name = "labelLocatorID";
            this.labelLocatorID.Size = new System.Drawing.Size(316, 13);
            this.labelLocatorID.TabIndex = 69;
            this.labelLocatorID.Text = "text";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(17, 158);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(69, 13);
            this.label12.TabIndex = 68;
            this.label12.Text = "Manifest file :";
            // 
            // label8
            // 
            this.label8.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label8.Location = new System.Drawing.Point(17, 37);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(394, 32);
            this.label8.TabIndex = 67;
            this.label8.Text = "This option is useful if you created a live program in another datacenter and you" +
    " want to create a program with the same path";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 137);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 13);
            this.label7.TabIndex = 66;
            this.label7.Text = "Locator ID :";
            // 
            // textBoxProgramSourceURL
            // 
            this.textBoxProgramSourceURL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxProgramSourceURL.Enabled = false;
            this.textBoxProgramSourceURL.Location = new System.Drawing.Point(20, 92);
            this.textBoxProgramSourceURL.Name = "textBoxProgramSourceURL";
            this.textBoxProgramSourceURL.Size = new System.Drawing.Size(391, 20);
            this.textBoxProgramSourceURL.TabIndex = 63;
            this.textBoxProgramSourceURL.TextChanged += new System.EventHandler(this.textBoxIProgramSourceURL_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 76);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(196, 13);
            this.label6.TabIndex = 64;
            this.label6.Text = "Output URL of the program to replicate :";
            // 
            // checkBoxReplica
            // 
            this.checkBoxReplica.AutoSize = true;
            this.checkBoxReplica.Location = new System.Drawing.Point(20, 13);
            this.checkBoxReplica.Name = "checkBoxReplica";
            this.checkBoxReplica.Size = new System.Drawing.Size(297, 17);
            this.checkBoxReplica.TabIndex = 62;
            this.checkBoxReplica.Text = "This program is a replica of a program in another account.";
            this.checkBoxReplica.UseVisualStyleBackColor = true;
            this.checkBoxReplica.CheckedChanged += new System.EventHandler(this.checkBoxReplica_CheckedChanged);
            // 
            // CreateProgram
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(490, 524);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.checkBoxAddScaleUnit);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.checkBoxCreateLocator);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxDescription);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textboxprogramname);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.buttonCancel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CreateProgram";
            this.Text = "Create a new program for channel {0}";
            this.Load += new System.EventHandler(this.CreateLocator_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownArchiveDays)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownArchiveMinutes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownArchiveHours)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button buttonOk;
        public System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textboxprogramname;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown numericUpDownArchiveDays;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.CheckBox checkBoxCreateLocator;
        private System.Windows.Forms.TextBox textBoxAssetName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox checkBoxAddScaleUnit;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.NumericUpDown numericUpDownArchiveHours;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown numericUpDownArchiveMinutes;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxProgramSourceURL;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox checkBoxReplica;
        private System.Windows.Forms.Label labelManifestFile;
        private System.Windows.Forms.Label labelLocatorID;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label labelURLFileNameWarning;
    }
}