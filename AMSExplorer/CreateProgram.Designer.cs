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
            this.components = new System.ComponentModel.Container();
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
            this.label33 = new System.Windows.Forms.Label();
            this.comboBoxStorage = new System.Windows.Forms.ComboBox();
            this.numericUpDownArchiveMinutes = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label10 = new System.Windows.Forms.Label();
            this.numericUpDownArchiveHours = new System.Windows.Forms.NumericUpDown();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.labelCloneLocators = new System.Windows.Forms.Label();
            this.textBoxManifestName = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
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
            this.checkBoxDynEnc = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.checkBoxStartProgramNow = new System.Windows.Forms.CheckBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownArchiveDays)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownArchiveMinutes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownArchiveHours)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(320, 14);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(115, 27);
            this.buttonOk.TabIndex = 6;
            this.buttonOk.Text = "Create Program";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(446, 14);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(115, 27);
            this.buttonCancel.TabIndex = 7;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // textBoxAssetName
            // 
            this.textBoxAssetName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxAssetName.Location = new System.Drawing.Point(21, 102);
            this.textBoxAssetName.Name = "textBoxAssetName";
            this.textBoxAssetName.Size = new System.Drawing.Size(450, 23);
            this.textBoxAssetName.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 15);
            this.label4.TabIndex = 48;
            this.label4.Text = "New asset name :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(210, 19);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(32, 15);
            this.label9.TabIndex = 60;
            this.label9.Text = "Days";
            // 
            // numericUpDownArchiveDays
            // 
            this.numericUpDownArchiveDays.Location = new System.Drawing.Point(213, 37);
            this.numericUpDownArchiveDays.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownArchiveDays.Name = "numericUpDownArchiveDays";
            this.numericUpDownArchiveDays.Size = new System.Drawing.Size(55, 23);
            this.numericUpDownArchiveDays.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(58, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(140, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "Archive Window Length :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 15);
            this.label3.TabIndex = 45;
            this.label3.Text = "Program name :";
            // 
            // textboxprogramname
            // 
            this.textboxprogramname.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textboxprogramname.Location = new System.Drawing.Point(35, 103);
            this.textboxprogramname.Name = "textboxprogramname";
            this.textboxprogramname.Size = new System.Drawing.Size(504, 23);
            this.textboxprogramname.TabIndex = 0;
            this.textboxprogramname.Validating += new System.ComponentModel.CancelEventHandler(this.textboxprogramname_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 143);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 15);
            this.label1.TabIndex = 47;
            this.label1.Text = "Description :";
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDescription.Location = new System.Drawing.Point(35, 162);
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(506, 23);
            this.textBoxDescription.TabIndex = 1;
            // 
            // checkBoxCreateLocator
            // 
            this.checkBoxCreateLocator.AutoSize = true;
            this.checkBoxCreateLocator.Location = new System.Drawing.Point(35, 512);
            this.checkBoxCreateLocator.Name = "checkBoxCreateLocator";
            this.checkBoxCreateLocator.Size = new System.Drawing.Size(376, 19);
            this.checkBoxCreateLocator.TabIndex = 5;
            this.checkBoxCreateLocator.Text = "Publish this program/asset now (default locator duration: {0} days)";
            this.checkBoxCreateLocator.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label5.Location = new System.Drawing.Point(31, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(509, 57);
            this.label5.TabIndex = 48;
            this.label5.Text = "Live programs will automatically convert to on demand content when the program is" +
    " stopped and will be available on the ASSETS tab.\r\nAll publish URLs remain the s" +
    "ame after the live program completes. ";
            // 
            // checkBoxAddScaleUnit
            // 
            this.checkBoxAddScaleUnit.AutoSize = true;
            this.checkBoxAddScaleUnit.Location = new System.Drawing.Point(35, 457);
            this.checkBoxAddScaleUnit.Name = "checkBoxAddScaleUnit";
            this.checkBoxAddScaleUnit.Size = new System.Drawing.Size(180, 19);
            this.checkBoxAddScaleUnit.TabIndex = 3;
            this.checkBoxAddScaleUnit.Text = "Add one scale streaming unit";
            this.checkBoxAddScaleUnit.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(35, 202);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(506, 238);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label33);
            this.tabPage1.Controls.Add(this.comboBoxStorage);
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
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(498, 210);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Settings";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label33
            // 
            this.label33.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(17, 148);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(93, 15);
            this.label33.TabIndex = 65;
            this.label33.Text = "Output storage :";
            // 
            // comboBoxStorage
            // 
            this.comboBoxStorage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBoxStorage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStorage.FormattingEnabled = true;
            this.comboBoxStorage.Location = new System.Drawing.Point(21, 166);
            this.comboBoxStorage.Name = "comboBoxStorage";
            this.comboBoxStorage.Size = new System.Drawing.Size(327, 23);
            this.comboBoxStorage.TabIndex = 4;
            // 
            // numericUpDownArchiveMinutes
            // 
            this.numericUpDownArchiveMinutes.Location = new System.Drawing.Point(337, 37);
            this.numericUpDownArchiveMinutes.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.numericUpDownArchiveMinutes.Name = "numericUpDownArchiveMinutes";
            this.numericUpDownArchiveMinutes.Size = new System.Drawing.Size(55, 23);
            this.numericUpDownArchiveMinutes.TabIndex = 2;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(334, 19);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(50, 15);
            this.label11.TabIndex = 63;
            this.label11.Text = "Minutes";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::AMSExplorer.Bitmaps.archive;
            this.pictureBox1.Location = new System.Drawing.Point(21, 34);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 49;
            this.pictureBox1.TabStop = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(272, 19);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(39, 15);
            this.label10.TabIndex = 61;
            this.label10.Text = "Hours";
            // 
            // numericUpDownArchiveHours
            // 
            this.numericUpDownArchiveHours.Location = new System.Drawing.Point(275, 37);
            this.numericUpDownArchiveHours.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.numericUpDownArchiveHours.Name = "numericUpDownArchiveHours";
            this.numericUpDownArchiveHours.Size = new System.Drawing.Size(55, 23);
            this.numericUpDownArchiveHours.TabIndex = 1;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.labelCloneLocators);
            this.tabPage3.Controls.Add(this.textBoxManifestName);
            this.tabPage3.Controls.Add(this.label13);
            this.tabPage3.Location = new System.Drawing.Point(4, 24);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(498, 210);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Advanced";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // labelCloneLocators
            // 
            this.labelCloneLocators.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.labelCloneLocators.Location = new System.Drawing.Point(116, 84);
            this.labelCloneLocators.Name = "labelCloneLocators";
            this.labelCloneLocators.Size = new System.Drawing.Size(255, 15);
            this.labelCloneLocators.TabIndex = 72;
            this.labelCloneLocators.Text = "leave empty to auto generate it";
            // 
            // textBoxManifestName
            // 
            this.textBoxManifestName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxManifestName.Location = new System.Drawing.Point(21, 102);
            this.textBoxManifestName.Name = "textBoxManifestName";
            this.textBoxManifestName.Size = new System.Drawing.Size(450, 23);
            this.textBoxManifestName.TabIndex = 49;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(18, 84);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(92, 15);
            this.label13.TabIndex = 50;
            this.label13.Text = "Manifest name :";
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
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(498, 210);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Replica";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // labelURLFileNameWarning
            // 
            this.labelURLFileNameWarning.AutoSize = true;
            this.labelURLFileNameWarning.ForeColor = System.Drawing.Color.Red;
            this.labelURLFileNameWarning.Location = new System.Drawing.Point(20, 133);
            this.labelURLFileNameWarning.Name = "labelURLFileNameWarning";
            this.labelURLFileNameWarning.Size = new System.Drawing.Size(52, 15);
            this.labelURLFileNameWarning.TabIndex = 71;
            this.labelURLFileNameWarning.Text = "Warning";
            // 
            // labelManifestFile
            // 
            this.labelManifestFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelManifestFile.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.labelManifestFile.Location = new System.Drawing.Point(111, 182);
            this.labelManifestFile.Name = "labelManifestFile";
            this.labelManifestFile.Size = new System.Drawing.Size(369, 15);
            this.labelManifestFile.TabIndex = 70;
            this.labelManifestFile.Text = "text";
            // 
            // labelLocatorID
            // 
            this.labelLocatorID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelLocatorID.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.labelLocatorID.Location = new System.Drawing.Point(111, 158);
            this.labelLocatorID.Name = "labelLocatorID";
            this.labelLocatorID.Size = new System.Drawing.Size(369, 15);
            this.labelLocatorID.TabIndex = 69;
            this.labelLocatorID.Text = "text";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(20, 182);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(78, 15);
            this.label12.TabIndex = 68;
            this.label12.Text = "Manifest file :";
            // 
            // label8
            // 
            this.label8.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label8.Location = new System.Drawing.Point(20, 43);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(460, 37);
            this.label8.TabIndex = 67;
            this.label8.Text = "This option is useful if you created a live program in another datacenter and wan" +
    "t to create a program with the same URL path.";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(20, 158);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 15);
            this.label7.TabIndex = 66;
            this.label7.Text = "Locator ID :";
            // 
            // textBoxProgramSourceURL
            // 
            this.textBoxProgramSourceURL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxProgramSourceURL.Enabled = false;
            this.textBoxProgramSourceURL.Location = new System.Drawing.Point(23, 106);
            this.textBoxProgramSourceURL.Name = "textBoxProgramSourceURL";
            this.textBoxProgramSourceURL.Size = new System.Drawing.Size(455, 23);
            this.textBoxProgramSourceURL.TabIndex = 63;
            this.textBoxProgramSourceURL.TextChanged += new System.EventHandler(this.textBoxIProgramSourceURL_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 88);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(220, 15);
            this.label6.TabIndex = 64;
            this.label6.Text = "Output URL of the program to replicate :";
            // 
            // checkBoxReplica
            // 
            this.checkBoxReplica.AutoSize = true;
            this.checkBoxReplica.Location = new System.Drawing.Point(23, 15);
            this.checkBoxReplica.Name = "checkBoxReplica";
            this.checkBoxReplica.Size = new System.Drawing.Size(346, 19);
            this.checkBoxReplica.TabIndex = 62;
            this.checkBoxReplica.Text = "This program is a replica of a program in another datacenter.";
            this.checkBoxReplica.UseVisualStyleBackColor = true;
            this.checkBoxReplica.CheckedChanged += new System.EventHandler(this.checkBoxReplica_CheckedChanged);
            // 
            // checkBoxDynEnc
            // 
            this.checkBoxDynEnc.AutoSize = true;
            this.checkBoxDynEnc.Location = new System.Drawing.Point(35, 485);
            this.checkBoxDynEnc.Name = "checkBoxDynEnc";
            this.checkBoxDynEnc.Size = new System.Drawing.Size(171, 19);
            this.checkBoxDynEnc.TabIndex = 4;
            this.checkBoxDynEnc.Text = "Enable Dynamic Encryption";
            this.checkBoxDynEnc.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonOk);
            this.panel1.Location = new System.Drawing.Point(-3, 563);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(576, 55);
            this.panel1.TabIndex = 61;
            // 
            // checkBoxStartProgramNow
            // 
            this.checkBoxStartProgramNow.AutoSize = true;
            this.checkBoxStartProgramNow.Location = new System.Drawing.Point(35, 539);
            this.checkBoxStartProgramNow.Name = "checkBoxStartProgramNow";
            this.checkBoxStartProgramNow.Size = new System.Drawing.Size(145, 19);
            this.checkBoxStartProgramNow.TabIndex = 62;
            this.checkBoxStartProgramNow.Text = "Start the program now";
            this.checkBoxStartProgramNow.UseVisualStyleBackColor = true;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // CreateProgram
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(572, 617);
            this.Controls.Add(this.checkBoxStartProgramNow);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.checkBoxDynEnc);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.checkBoxAddScaleUnit);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.checkBoxCreateLocator);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxDescription);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textboxprogramname);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "CreateProgram";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Create a new program for channel {0}";
            this.Load += new System.EventHandler(this.CreateLocator_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownArchiveDays)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownArchiveMinutes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownArchiveHours)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
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
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.ComboBox comboBoxStorage;
        private System.Windows.Forms.CheckBox checkBoxDynEnc;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox checkBoxStartProgramNow;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox textBoxManifestName;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label labelCloneLocators;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}