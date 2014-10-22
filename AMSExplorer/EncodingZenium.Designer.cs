namespace AMSExplorer
{
    partial class EncodingZenium
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EncodingZenium));
            this.label = new System.Windows.Forms.Label();
            this.textBoxJobName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.listbox = new System.Windows.Forms.ListBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.moreinfoprofilelink = new System.Windows.Forms.LinkLabel();
            this.processorlabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textboxoutputassetname = new System.Windows.Forms.TextBox();
            this.radioButtonMultipleJob = new System.Windows.Forms.RadioButton();
            this.radioButtonSingleJob = new System.Windows.Forms.RadioButton();
            this.labelsummaryjob = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.numericUpDownPriority = new System.Windows.Forms.NumericUpDown();
            this.buttonOk = new System.Windows.Forms.Button();
            this.pictureBoxXenio = new System.Windows.Forms.PictureBox();
            this.pictureBoxJob = new System.Windows.Forms.PictureBox();
            this.label33 = new System.Windows.Forms.Label();
            this.comboBoxStorage = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPriority)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxXenio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxJob)).BeginInit();
            this.SuspendLayout();
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(32, 23);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(35, 13);
            this.label.TabIndex = 0;
            this.label.Text = "label1";
            // 
            // textBoxJobName
            // 
            this.textBoxJobName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxJobName.Location = new System.Drawing.Point(32, 262);
            this.textBoxJobName.Name = "textBoxJobName";
            this.textBoxJobName.Size = new System.Drawing.Size(440, 20);
            this.textBoxJobName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(338, 26);
            this.label2.TabIndex = 2;
            this.label2.Text = "Please select one or several blueprints.\r\nFor each blueprint selected, one parall" +
    "el task will be created in the job.";
            // 
            // listbox
            // 
            this.listbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listbox.FormattingEnabled = true;
            this.listbox.Location = new System.Drawing.Point(32, 83);
            this.listbox.Name = "listbox";
            this.listbox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listbox.Size = new System.Drawing.Size(440, 147);
            this.listbox.TabIndex = 3;
            this.listbox.SelectedIndexChanged += new System.EventHandler(this.listbox_SelectedIndexChanged);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(336, 510);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(113, 32);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 246);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Job(s) name :";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // moreinfoprofilelink
            // 
            this.moreinfoprofilelink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.moreinfoprofilelink.AutoSize = true;
            this.moreinfoprofilelink.Location = new System.Drawing.Point(579, 150);
            this.moreinfoprofilelink.Name = "moreinfoprofilelink";
            this.moreinfoprofilelink.Size = new System.Drawing.Size(107, 13);
            this.moreinfoprofilelink.TabIndex = 7;
            this.moreinfoprofilelink.TabStop = true;
            this.moreinfoprofilelink.Text = "More info on features";
            this.moreinfoprofilelink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.moreinfoprofilelink_LinkClicked);
            // 
            // processorlabel
            // 
            this.processorlabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.processorlabel.Location = new System.Drawing.Point(26, 463);
            this.processorlabel.Name = "processorlabel";
            this.processorlabel.Size = new System.Drawing.Size(446, 22);
            this.processorlabel.TabIndex = 8;
            this.processorlabel.Text = "processor name";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(29, 294);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Output asset(s) name :";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // textboxoutputassetname
            // 
            this.textboxoutputassetname.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textboxoutputassetname.Location = new System.Drawing.Point(29, 310);
            this.textboxoutputassetname.Name = "textboxoutputassetname";
            this.textboxoutputassetname.Size = new System.Drawing.Size(443, 20);
            this.textboxoutputassetname.TabIndex = 9;
            this.textboxoutputassetname.TextChanged += new System.EventHandler(this.outputassetname_TextChanged);
            // 
            // radioButtonMultipleJob
            // 
            this.radioButtonMultipleJob.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.radioButtonMultipleJob.AutoSize = true;
            this.radioButtonMultipleJob.Checked = true;
            this.radioButtonMultipleJob.Location = new System.Drawing.Point(29, 362);
            this.radioButtonMultipleJob.Name = "radioButtonMultipleJob";
            this.radioButtonMultipleJob.Size = new System.Drawing.Size(211, 17);
            this.radioButtonMultipleJob.TabIndex = 12;
            this.radioButtonMultipleJob.TabStop = true;
            this.radioButtonMultipleJob.Text = "Multiple jobs (a job for each input asset)";
            this.radioButtonMultipleJob.UseVisualStyleBackColor = true;
            this.radioButtonMultipleJob.CheckedChanged += new System.EventHandler(this.radioButtonMultipleJob_CheckedChanged);
            // 
            // radioButtonSingleJob
            // 
            this.radioButtonSingleJob.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.radioButtonSingleJob.AutoSize = true;
            this.radioButtonSingleJob.Location = new System.Drawing.Point(29, 385);
            this.radioButtonSingleJob.Name = "radioButtonSingleJob";
            this.radioButtonSingleJob.Size = new System.Drawing.Size(400, 17);
            this.radioButtonSingleJob.TabIndex = 13;
            this.radioButtonSingleJob.Text = "Single job (Not supported today - one job and pass all selected assets as inputs)" +
    "";
            this.radioButtonSingleJob.UseVisualStyleBackColor = true;
            // 
            // labelsummaryjob
            // 
            this.labelsummaryjob.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelsummaryjob.Location = new System.Drawing.Point(26, 419);
            this.labelsummaryjob.Name = "labelsummaryjob";
            this.labelsummaryjob.Size = new System.Drawing.Size(228, 22);
            this.labelsummaryjob.TabIndex = 14;
            this.labelsummaryjob.Text = "You will submit n jobs with n tasks";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(489, 246);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 13);
            this.label5.TabIndex = 36;
            this.label5.Text = "Priority :";
            // 
            // numericUpDownPriority
            // 
            this.numericUpDownPriority.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownPriority.Location = new System.Drawing.Point(489, 262);
            this.numericUpDownPriority.Name = "numericUpDownPriority";
            this.numericUpDownPriority.Size = new System.Drawing.Size(58, 20);
            this.numericUpDownPriority.TabIndex = 35;
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Image = global::AMSExplorer.Bitmaps.encoding;
            this.buttonOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonOk.Location = new System.Drawing.Point(189, 510);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(141, 32);
            this.buttonOk.TabIndex = 5;
            this.buttonOk.Text = "Launch encoding";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // pictureBoxXenio
            // 
            this.pictureBoxXenio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxXenio.Image = global::AMSExplorer.Bitmaps.xenio;
            this.pictureBoxXenio.Location = new System.Drawing.Point(582, 23);
            this.pictureBoxXenio.Name = "pictureBoxXenio";
            this.pictureBoxXenio.Size = new System.Drawing.Size(188, 124);
            this.pictureBoxXenio.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxXenio.TabIndex = 42;
            this.pictureBoxXenio.TabStop = false;
            // 
            // pictureBoxJob
            // 
            this.pictureBoxJob.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxJob.Image = global::AMSExplorer.Bitmaps.modeltaskxenio2;
            this.pictureBoxJob.Location = new System.Drawing.Point(490, 348);
            this.pictureBoxJob.Name = "pictureBoxJob";
            this.pictureBoxJob.Size = new System.Drawing.Size(280, 211);
            this.pictureBoxJob.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxJob.TabIndex = 41;
            this.pictureBoxJob.TabStop = false;
            // 
            // label33
            // 
            this.label33.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(489, 294);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(83, 13);
            this.label33.TabIndex = 55;
            this.label33.Text = "Output storage :";
            // 
            // comboBoxStorage
            // 
            this.comboBoxStorage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxStorage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStorage.FormattingEnabled = true;
            this.comboBoxStorage.Location = new System.Drawing.Point(489, 310);
            this.comboBoxStorage.Name = "comboBoxStorage";
            this.comboBoxStorage.Size = new System.Drawing.Size(281, 21);
            this.comboBoxStorage.TabIndex = 54;
            // 
            // EncodingZenium
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.label33);
            this.Controls.Add(this.comboBoxStorage);
            this.Controls.Add(this.pictureBoxXenio);
            this.Controls.Add(this.pictureBoxJob);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.numericUpDownPriority);
            this.Controls.Add(this.labelsummaryjob);
            this.Controls.Add(this.radioButtonSingleJob);
            this.Controls.Add(this.radioButtonMultipleJob);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textboxoutputassetname);
            this.Controls.Add(this.processorlabel);
            this.Controls.Add(this.moreinfoprofilelink);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.listbox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxJobName);
            this.Controls.Add(this.label);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EncodingZenium";
            this.Text = "Encoding with Zenium";
            this.Load += new System.EventHandler(this.EncodingXenio_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPriority)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxXenio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxJob)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label label;
        public System.Windows.Forms.TextBox textBoxJobName;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.ListBox listbox;
        public System.Windows.Forms.Button buttonCancel;
        public System.Windows.Forms.Button buttonOk;
        public System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel moreinfoprofilelink;
        private System.Windows.Forms.Label processorlabel;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox textboxoutputassetname;
        private System.Windows.Forms.RadioButton radioButtonMultipleJob;
        private System.Windows.Forms.RadioButton radioButtonSingleJob;
        private System.Windows.Forms.Label labelsummaryjob;
        public System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numericUpDownPriority;
        private System.Windows.Forms.PictureBox pictureBoxJob;
        private System.Windows.Forms.PictureBox pictureBoxXenio;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.ComboBox comboBoxStorage;

    }
}