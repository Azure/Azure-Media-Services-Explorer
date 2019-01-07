namespace AMSExplorer
{
    partial class ProcessFromTransform
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProcessFromTransform));
            this.label = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.openFileDialogWorkflow = new System.Windows.Forms.OpenFileDialog();
            this.buttonDeleteTemplate = new System.Windows.Forms.Button();
            this.labelWarning = new System.Windows.Forms.Label();
            this.listViewTransforms = new AMSExplorer.ListViewTransforms();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labelAssetCopy = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label
            // 
            resources.ApplyResources(this.label, "label");
            this.label.Name = "label";
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOk
            // 
            resources.ApplyResources(this.buttonOk, "buttonOk");
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Image = global::AMSExplorer.Bitmaps.encoding;
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // openFileDialogWorkflow
            // 
            resources.ApplyResources(this.openFileDialogWorkflow, "openFileDialogWorkflow");
            // 
            // buttonDeleteTemplate
            // 
            resources.ApplyResources(this.buttonDeleteTemplate, "buttonDeleteTemplate");
            this.buttonDeleteTemplate.Name = "buttonDeleteTemplate";
            this.buttonDeleteTemplate.UseVisualStyleBackColor = true;
            this.buttonDeleteTemplate.Click += new System.EventHandler(this.buttonDeleteTemplate_Click);
            // 
            // labelWarning
            // 
            resources.ApplyResources(this.labelWarning, "labelWarning");
            this.labelWarning.ForeColor = System.Drawing.Color.Red;
            this.labelWarning.Name = "labelWarning";
            // 
            // listViewTransforms
            // 
            this.listViewTransforms.FullRowSelect = true;
            this.listViewTransforms.HideSelection = false;
            resources.ApplyResources(this.listViewTransforms, "listViewTransforms");
            this.listViewTransforms.MultiSelect = false;
            this.listViewTransforms.Name = "listViewTransforms";
            this.listViewTransforms.Tag = -1;
            this.listViewTransforms.UseCompatibleStateImageBehavior = false;
            this.listViewTransforms.View = System.Windows.Forms.View.Details;
            this.listViewTransforms.SelectedIndexChanged += new System.EventHandler(this.listbox_SelectedIndexChanged);
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonOk);
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Name = "panel1";
            // 
            // panel2
            // 
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Name = "panel2";
            // 
            // labelAssetCopy
            // 
            resources.ApplyResources(this.labelAssetCopy, "labelAssetCopy");
            this.labelAssetCopy.ForeColor = System.Drawing.Color.DarkBlue;
            this.labelAssetCopy.Name = "labelAssetCopy";
            // 
            // ProcessFromTransform
            // 
            this.AcceptButton = this.buttonOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.labelAssetCopy);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.listViewTransforms);
            this.Controls.Add(this.labelWarning);
            this.Controls.Add(this.buttonDeleteTemplate);
            this.Controls.Add(this.label);
            this.Name = "ProcessFromTransform";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.ProcessFromJobTemplate_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label label;
        public System.Windows.Forms.Button buttonCancel;
        public System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.OpenFileDialog openFileDialogWorkflow;
        private System.Windows.Forms.Button buttonDeleteTemplate;
        private System.Windows.Forms.Label labelWarning;
        private ListViewTransforms listViewTransforms;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label labelAssetCopy;
    }
}