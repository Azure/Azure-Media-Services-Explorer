namespace AMSExplorer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EncodingPremium));
            this.label = new System.Windows.Forms.Label();
            this.textBoxJobName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.moreinfoprofilelink = new System.Windows.Forms.LinkLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.textboxoutputassetname = new System.Windows.Forms.TextBox();
            this.labelsummaryjob = new System.Windows.Forms.Label();
            this.buttonOk = new System.Windows.Forms.Button();
            this.pictureBoxJob = new System.Windows.Forms.PictureBox();
            this.label31 = new System.Windows.Forms.Label();
            this.progressBarUpload = new System.Windows.Forms.ProgressBar();
            this.buttonUpload = new System.Windows.Forms.Button();
            this.openFileDialogWorkflow = new System.Windows.Forms.OpenFileDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labelProcessorVersion = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.buttonPremiumXMLData = new AMSExplorer.ButtonPremiumXMLData();
            this.buttonJobOptions = new AMSExplorer.ButtonJobOptions();
            this.listViewWorkflows = new AMSExplorer.ListViewWorkflows();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxWorkflowAssetId = new System.Windows.Forms.TextBox();
            this.buttonLoadWorkflow = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxJob)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label
            // 
            resources.ApplyResources(this.label, "label");
            this.label.Name = "label";
            // 
            // textBoxJobName
            // 
            resources.ApplyResources(this.textBoxJobName, "textBoxJobName");
            this.textBoxJobName.Name = "textBoxJobName";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // moreinfoprofilelink
            // 
            resources.ApplyResources(this.moreinfoprofilelink, "moreinfoprofilelink");
            this.moreinfoprofilelink.Name = "moreinfoprofilelink";
            this.moreinfoprofilelink.TabStop = true;
            this.moreinfoprofilelink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.moreinfoprofilelink_LinkClicked);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // textboxoutputassetname
            // 
            resources.ApplyResources(this.textboxoutputassetname, "textboxoutputassetname");
            this.textboxoutputassetname.Name = "textboxoutputassetname";
            this.textboxoutputassetname.TextChanged += new System.EventHandler(this.outputassetname_TextChanged);
            // 
            // labelsummaryjob
            // 
            resources.ApplyResources(this.labelsummaryjob, "labelsummaryjob");
            this.labelsummaryjob.Name = "labelsummaryjob";
            // 
            // buttonOk
            // 
            resources.ApplyResources(this.buttonOk, "buttonOk");
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Image = global::AMSExplorer.Bitmaps.encoding;
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // pictureBoxJob
            // 
            resources.ApplyResources(this.pictureBoxJob, "pictureBoxJob");
            this.pictureBoxJob.Image = global::AMSExplorer.Bitmaps.modeltaskxenio2;
            this.pictureBoxJob.Name = "pictureBoxJob";
            this.pictureBoxJob.TabStop = false;
            // 
            // label31
            // 
            resources.ApplyResources(this.label31, "label31");
            this.label31.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(128)))), ((int)(((byte)(171)))));
            this.label31.Name = "label31";
            // 
            // progressBarUpload
            // 
            resources.ApplyResources(this.progressBarUpload, "progressBarUpload");
            this.progressBarUpload.Name = "progressBarUpload";
            // 
            // buttonUpload
            // 
            resources.ApplyResources(this.buttonUpload, "buttonUpload");
            this.buttonUpload.Name = "buttonUpload";
            this.buttonUpload.UseVisualStyleBackColor = true;
            this.buttonUpload.Click += new System.EventHandler(this.buttonUpload_Click);
            // 
            // openFileDialogWorkflow
            // 
            resources.ApplyResources(this.openFileDialogWorkflow, "openFileDialogWorkflow");
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonOk);
            this.panel1.Name = "panel1";
            // 
            // panel2
            // 
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Name = "panel2";
            // 
            // labelProcessorVersion
            // 
            resources.ApplyResources(this.labelProcessorVersion, "labelProcessorVersion");
            this.labelProcessorVersion.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.labelProcessorVersion.Name = "labelProcessorVersion";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::AMSExplorer.Bitmaps.encoding_large;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // buttonPremiumXMLData
            // 
            resources.ApplyResources(this.buttonPremiumXMLData, "buttonPremiumXMLData");
            this.buttonPremiumXMLData.Name = "buttonPremiumXMLData";
            this.buttonPremiumXMLData.UseVisualStyleBackColor = true;
            // 
            // buttonJobOptions
            // 
            resources.ApplyResources(this.buttonJobOptions, "buttonJobOptions");
            this.buttonJobOptions.Name = "buttonJobOptions";
            this.buttonJobOptions.UseVisualStyleBackColor = true;
            // 
            // listViewWorkflows
            // 
            resources.ApplyResources(this.listViewWorkflows, "listViewWorkflows");
            this.listViewWorkflows.FullRowSelect = true;
            this.listViewWorkflows.HideSelection = false;
            this.listViewWorkflows.Name = "listViewWorkflows";
            this.listViewWorkflows.Tag = -1;
            this.listViewWorkflows.UseCompatibleStateImageBehavior = false;
            this.listViewWorkflows.View = System.Windows.Forms.View.Details;
            this.listViewWorkflows.SelectedIndexChanged += new System.EventHandler(this.listViewWorkflows_SelectedIndexChanged);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // textBoxWorkflowAssetId
            // 
            resources.ApplyResources(this.textBoxWorkflowAssetId, "textBoxWorkflowAssetId");
            this.textBoxWorkflowAssetId.Name = "textBoxWorkflowAssetId";
            // 
            // buttonLoadWorkflow
            // 
            resources.ApplyResources(this.buttonLoadWorkflow, "buttonLoadWorkflow");
            this.buttonLoadWorkflow.Name = "buttonLoadWorkflow";
            this.buttonLoadWorkflow.UseVisualStyleBackColor = true;
            this.buttonLoadWorkflow.Click += new System.EventHandler(this.ButtonLoadWorkflow_Click);
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.textBoxWorkflowAssetId);
            this.groupBox1.Controls.Add(this.buttonLoadWorkflow);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.progressBarUpload);
            this.groupBox1.Controls.Add(this.buttonUpload);
            this.groupBox1.Controls.Add(this.listViewWorkflows);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // EncodingPremium
            // 
            this.AcceptButton = this.buttonOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.labelProcessorVersion);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.buttonPremiumXMLData);
            this.Controls.Add(this.buttonJobOptions);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBoxJob);
            this.Controls.Add(this.labelsummaryjob);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textboxoutputassetname);
            this.Controls.Add(this.moreinfoprofilelink);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxJobName);
            this.Controls.Add(this.label);
            this.Controls.Add(this.label31);
            this.Name = "EncodingPremium";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.EncodingPremiumWorkflow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxJob)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
        private System.Windows.Forms.Label labelsummaryjob;
        private System.Windows.Forms.PictureBox pictureBoxJob;
        public System.Windows.Forms.Label label31;
        private System.Windows.Forms.ProgressBar progressBarUpload;
        private System.Windows.Forms.Button buttonUpload;
        private System.Windows.Forms.OpenFileDialog openFileDialogWorkflow;
        private System.Windows.Forms.Panel panel1;
        private ListViewWorkflows listViewWorkflows;
        private ButtonJobOptions buttonJobOptions;
        private ButtonPremiumXMLData buttonPremiumXMLData;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label labelProcessorVersion;
        private System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox textBoxWorkflowAssetId;
        private System.Windows.Forms.Button buttonLoadWorkflow;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.Label label5;
    }
}