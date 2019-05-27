namespace AMSExplorer
{
    partial class LiveOutputCreation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LiveOutputCreation));
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.textBoxAssetName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textboxprogramname = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.checkBoxCreateLocator = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label9 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.comboBoxStorage = new System.Windows.Forms.ComboBox();
            this.numericUpDownArchiveMinutes = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label10 = new System.Windows.Forms.Label();
            this.numericUpDownArchiveHours = new System.Windows.Forms.NumericUpDown();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.textBoxStartRecordTimestamp = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.checkBoxHLSFragPerSegDefined = new System.Windows.Forms.CheckBox();
            this.numericUpDownHLSFragPerSeg = new System.Windows.Forms.NumericUpDown();
            this.textBoxManifestName = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownArchiveMinutes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownArchiveHours)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHLSFragPerSeg)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
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
            // textBoxAssetName
            // 
            resources.ApplyResources(this.textBoxAssetName, "textBoxAssetName");
            this.textBoxAssetName.Name = "textBoxAssetName";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // textboxprogramname
            // 
            resources.ApplyResources(this.textboxprogramname, "textboxprogramname");
            this.textboxprogramname.Name = "textboxprogramname";
            this.textboxprogramname.TextChanged += new System.EventHandler(this.textboxprogramname_TextChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // textBoxDescription
            // 
            resources.ApplyResources(this.textBoxDescription, "textBoxDescription");
            this.textBoxDescription.Name = "textBoxDescription";
            // 
            // checkBoxCreateLocator
            // 
            resources.ApplyResources(this.checkBoxCreateLocator, "checkBoxCreateLocator");
            this.checkBoxCreateLocator.Name = "checkBoxCreateLocator";
            this.checkBoxCreateLocator.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.ForeColor = System.Drawing.SystemColors.WindowFrame;
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // tabControl1
            // 
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.label33);
            this.tabPage1.Controls.Add(this.comboBoxStorage);
            this.tabPage1.Controls.Add(this.textBoxAssetName);
            this.tabPage1.Controls.Add(this.numericUpDownArchiveMinutes);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.pictureBox1);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.numericUpDownArchiveHours);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.ForeColor = System.Drawing.SystemColors.WindowFrame;
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // label33
            // 
            resources.ApplyResources(this.label33, "label33");
            this.label33.Name = "label33";
            // 
            // comboBoxStorage
            // 
            resources.ApplyResources(this.comboBoxStorage, "comboBoxStorage");
            this.comboBoxStorage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStorage.FormattingEnabled = true;
            this.comboBoxStorage.Name = "comboBoxStorage";
            // 
            // numericUpDownArchiveMinutes
            // 
            resources.ApplyResources(this.numericUpDownArchiveMinutes, "numericUpDownArchiveMinutes");
            this.numericUpDownArchiveMinutes.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.numericUpDownArchiveMinutes.Name = "numericUpDownArchiveMinutes";
            this.numericUpDownArchiveMinutes.ReadOnly = true;
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::AMSExplorer.Bitmaps.archive;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // numericUpDownArchiveHours
            // 
            resources.ApplyResources(this.numericUpDownArchiveHours, "numericUpDownArchiveHours");
            this.numericUpDownArchiveHours.Maximum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.numericUpDownArchiveHours.Name = "numericUpDownArchiveHours";
            this.numericUpDownArchiveHours.ReadOnly = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.textBoxStartRecordTimestamp);
            this.tabPage3.Controls.Add(this.label6);
            this.tabPage3.Controls.Add(this.checkBoxHLSFragPerSegDefined);
            this.tabPage3.Controls.Add(this.numericUpDownHLSFragPerSeg);
            this.tabPage3.Controls.Add(this.textBoxManifestName);
            this.tabPage3.Controls.Add(this.label13);
            resources.ApplyResources(this.tabPage3, "tabPage3");
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // textBoxStartRecordTimestamp
            // 
            resources.ApplyResources(this.textBoxStartRecordTimestamp, "textBoxStartRecordTimestamp");
            this.textBoxStartRecordTimestamp.Name = "textBoxStartRecordTimestamp";
            this.textBoxStartRecordTimestamp.TextChanged += new System.EventHandler(this.textBoxStartRecordTimestamp_TextChanged);
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // checkBoxHLSFragPerSegDefined
            // 
            resources.ApplyResources(this.checkBoxHLSFragPerSegDefined, "checkBoxHLSFragPerSegDefined");
            this.checkBoxHLSFragPerSegDefined.Name = "checkBoxHLSFragPerSegDefined";
            this.checkBoxHLSFragPerSegDefined.UseVisualStyleBackColor = true;
            this.checkBoxHLSFragPerSegDefined.CheckedChanged += new System.EventHandler(this.checkBoxHLSFragPerSegDefined_CheckedChanged);
            // 
            // numericUpDownHLSFragPerSeg
            // 
            resources.ApplyResources(this.numericUpDownHLSFragPerSeg, "numericUpDownHLSFragPerSeg");
            this.numericUpDownHLSFragPerSeg.Name = "numericUpDownHLSFragPerSeg";
            this.numericUpDownHLSFragPerSeg.ReadOnly = true;
            // 
            // textBoxManifestName
            // 
            resources.ApplyResources(this.textBoxManifestName, "textBoxManifestName");
            this.textBoxManifestName.Name = "textBoxManifestName";
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonOk);
            this.panel1.Name = "panel1";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // LiveOutputCreation
            // 
            this.AcceptButton = this.buttonOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.checkBoxCreateLocator);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxDescription);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textboxprogramname);
            this.Name = "LiveOutputCreation";
            this.Load += new System.EventHandler(this.LiveOutputCreation_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownArchiveMinutes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownArchiveHours)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHLSFragPerSeg)).EndInit();
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.CheckBox checkBoxCreateLocator;
        private System.Windows.Forms.TextBox textBoxAssetName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.NumericUpDown numericUpDownArchiveHours;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown numericUpDownArchiveMinutes;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.ComboBox comboBoxStorage;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox textBoxManifestName;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox checkBoxHLSFragPerSegDefined;
        private System.Windows.Forms.NumericUpDown numericUpDownHLSFragPerSeg;
        private System.Windows.Forms.TextBox textBoxStartRecordTimestamp;
        private System.Windows.Forms.Label label6;
    }
}