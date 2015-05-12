﻿namespace AMSExplorer
{
    partial class EncodingPremium
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
            this.label = new System.Windows.Forms.Label();
            this.textBoxJobName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.moreinfoprofilelink = new System.Windows.Forms.LinkLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.textboxoutputassetname = new System.Windows.Forms.TextBox();
            this.radioButtonMultipleJob = new System.Windows.Forms.RadioButton();
            this.radioButtonSingleJob = new System.Windows.Forms.RadioButton();
            this.labelsummaryjob = new System.Windows.Forms.Label();
            this.buttonOk = new System.Windows.Forms.Button();
            this.pictureBoxJob = new System.Windows.Forms.PictureBox();
            this.label32 = new System.Windows.Forms.Label();
            this.comboBoxProcessor = new System.Windows.Forms.ComboBox();
            this.label31 = new System.Windows.Forms.Label();
            this.progressBarUpload = new System.Windows.Forms.ProgressBar();
            this.buttonUpload = new System.Windows.Forms.Button();
            this.openFileDialogWorkflow = new System.Windows.Forms.OpenFileDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.listViewWorkflows = new AMSExplorer.ListViewWorkflows();
            this.buttonJobOptions = new AMSExplorer.ButtonJobOptions();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxJob)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label
            // 
            this.label.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label.Location = new System.Drawing.Point(32, 41);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(606, 17);
            this.label.TabIndex = 0;
            this.label.Text = "label1";
            // 
            // textBoxJobName
            // 
            this.textBoxJobName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxJobName.Location = new System.Drawing.Point(32, 312);
            this.textBoxJobName.Name = "textBoxJobName";
            this.textBoxJobName.Size = new System.Drawing.Size(443, 20);
            this.textBoxJobName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 124);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(340, 26);
            this.label2.TabIndex = 2;
            this.label2.Text = "Please select one or several workflows.\r\nFor each workflow selected, one parallel" +
    " task will be created in the job.";
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(676, 13);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(99, 23);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 296);
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
            this.moreinfoprofilelink.Location = new System.Drawing.Point(673, 58);
            this.moreinfoprofilelink.Name = "moreinfoprofilelink";
            this.moreinfoprofilelink.Size = new System.Drawing.Size(85, 13);
            this.moreinfoprofilelink.TabIndex = 7;
            this.moreinfoprofilelink.TabStop = true;
            this.moreinfoprofilelink.Text = "More information";
            this.moreinfoprofilelink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.moreinfoprofilelink_LinkClicked);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 344);
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
            this.textboxoutputassetname.Location = new System.Drawing.Point(32, 360);
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
            this.radioButtonMultipleJob.Location = new System.Drawing.Point(32, 412);
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
            this.radioButtonSingleJob.Location = new System.Drawing.Point(32, 435);
            this.radioButtonSingleJob.Name = "radioButtonSingleJob";
            this.radioButtonSingleJob.Size = new System.Drawing.Size(400, 17);
            this.radioButtonSingleJob.TabIndex = 13;
            this.radioButtonSingleJob.Text = "Single job (Not supported today - one job and pass all selected assets as inputs)" +
    "";
            this.radioButtonSingleJob.UseVisualStyleBackColor = true;
            // 
            // labelsummaryjob
            // 
            this.labelsummaryjob.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelsummaryjob.Location = new System.Drawing.Point(32, 469);
            this.labelsummaryjob.Name = "labelsummaryjob";
            this.labelsummaryjob.Size = new System.Drawing.Size(228, 22);
            this.labelsummaryjob.TabIndex = 14;
            this.labelsummaryjob.Text = "You will submit n jobs with n tasks";
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Enabled = false;
            this.buttonOk.Image = global::AMSExplorer.Bitmaps.encoding;
            this.buttonOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonOk.Location = new System.Drawing.Point(529, 13);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(141, 23);
            this.buttonOk.TabIndex = 5;
            this.buttonOk.Text = "Launch encoding";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // pictureBoxJob
            // 
            this.pictureBoxJob.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxJob.Image = global::AMSExplorer.Bitmaps.modeltaskxenio2;
            this.pictureBoxJob.Location = new System.Drawing.Point(489, 397);
            this.pictureBoxJob.Name = "pictureBoxJob";
            this.pictureBoxJob.Size = new System.Drawing.Size(269, 110);
            this.pictureBoxJob.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxJob.TabIndex = 41;
            this.pictureBoxJob.TabStop = false;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(32, 68);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(60, 13);
            this.label32.TabIndex = 57;
            this.label32.Text = "Processor :";
            // 
            // comboBoxProcessor
            // 
            this.comboBoxProcessor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxProcessor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxProcessor.FormattingEnabled = true;
            this.comboBoxProcessor.Location = new System.Drawing.Point(32, 84);
            this.comboBoxProcessor.Name = "comboBoxProcessor";
            this.comboBoxProcessor.Size = new System.Drawing.Size(726, 21);
            this.comboBoxProcessor.TabIndex = 56;
            // 
            // label31
            // 
            this.label31.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(128)))), ((int)(((byte)(171)))));
            this.label31.Location = new System.Drawing.Point(425, 9);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(333, 24);
            this.label31.TabIndex = 60;
            this.label31.Text = "Media Encoder Premium Workflow";
            this.label31.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // progressBarUpload
            // 
            this.progressBarUpload.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarUpload.Location = new System.Drawing.Point(529, 124);
            this.progressBarUpload.Name = "progressBarUpload";
            this.progressBarUpload.Size = new System.Drawing.Size(229, 23);
            this.progressBarUpload.TabIndex = 63;
            this.progressBarUpload.Visible = false;
            // 
            // buttonUpload
            // 
            this.buttonUpload.Location = new System.Drawing.Point(384, 125);
            this.buttonUpload.Name = "buttonUpload";
            this.buttonUpload.Size = new System.Drawing.Size(139, 23);
            this.buttonUpload.TabIndex = 62;
            this.buttonUpload.Text = "Upload a new Workflow";
            this.buttonUpload.UseVisualStyleBackColor = true;
            this.buttonUpload.Click += new System.EventHandler(this.buttonUpload_Click);
            // 
            // openFileDialogWorkflow
            // 
            this.openFileDialogWorkflow.Filter = "Worflow files|*.workflow|All files (*.*)|*.*";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonOk);
            this.panel1.Location = new System.Drawing.Point(-3, 513);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(788, 48);
            this.panel1.TabIndex = 66;
            // 
            // listViewWorkflows
            // 
            this.listViewWorkflows.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewWorkflows.FullRowSelect = true;
            this.listViewWorkflows.HideSelection = false;
            this.listViewWorkflows.Location = new System.Drawing.Point(32, 155);
            this.listViewWorkflows.Name = "listViewWorkflows";
            this.listViewWorkflows.Size = new System.Drawing.Size(726, 124);
            this.listViewWorkflows.TabIndex = 61;
            this.listViewWorkflows.Tag = -1;
            this.listViewWorkflows.UseCompatibleStateImageBehavior = false;
            this.listViewWorkflows.View = System.Windows.Forms.View.Details;
            this.listViewWorkflows.SelectedIndexChanged += new System.EventHandler(this.listViewWorkflows_SelectedIndexChanged);
            // 
            // buttonJobOptions
            // 
            this.buttonJobOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonJobOptions.Location = new System.Drawing.Point(621, 312);
            this.buttonJobOptions.Name = "buttonJobOptions";
            this.buttonJobOptions.Size = new System.Drawing.Size(137, 23);
            this.buttonJobOptions.TabIndex = 72;
            this.buttonJobOptions.Text = "Job options...";
            this.buttonJobOptions.UseVisualStyleBackColor = true;
            // 
            // EncodingPremium
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.buttonJobOptions);
            this.Controls.Add(this.listViewWorkflows);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.progressBarUpload);
            this.Controls.Add(this.buttonUpload);
            this.Controls.Add(this.label32);
            this.Controls.Add(this.comboBoxProcessor);
            this.Controls.Add(this.pictureBoxJob);
            this.Controls.Add(this.labelsummaryjob);
            this.Controls.Add(this.radioButtonSingleJob);
            this.Controls.Add(this.radioButtonMultipleJob);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textboxoutputassetname);
            this.Controls.Add(this.moreinfoprofilelink);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxJobName);
            this.Controls.Add(this.label);
            this.Controls.Add(this.label31);
            this.Name = "EncodingPremium";
            this.Text = "Media Encoder Premium Workflow";
            this.Load += new System.EventHandler(this.EncodingPremiumWorkflow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxJob)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label label;
        public System.Windows.Forms.TextBox textBoxJobName;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Button buttonCancel;
        public System.Windows.Forms.Button buttonOk;
        public System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel moreinfoprofilelink;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox textboxoutputassetname;
        private System.Windows.Forms.RadioButton radioButtonMultipleJob;
        private System.Windows.Forms.RadioButton radioButtonSingleJob;
        private System.Windows.Forms.Label labelsummaryjob;
        private System.Windows.Forms.PictureBox pictureBoxJob;
        public System.Windows.Forms.Label label32;
        private System.Windows.Forms.ComboBox comboBoxProcessor;
        public System.Windows.Forms.Label label31;
        private System.Windows.Forms.ProgressBar progressBarUpload;
        private System.Windows.Forms.Button buttonUpload;
        private System.Windows.Forms.OpenFileDialog openFileDialogWorkflow;
        private System.Windows.Forms.Panel panel1;
        private ListViewWorkflows listViewWorkflows;
        private ButtonJobOptions buttonJobOptions;

    }
}