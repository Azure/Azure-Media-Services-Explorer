namespace AMSExplorer
{
    partial class EditorXMLJSON
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditorXMLJSON));
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBoxConfiguration = new System.Windows.Forms.TextBox();
            this.labelWarningJSON = new System.Windows.Forms.Label();
            this.buttonInsertSample = new System.Windows.Forms.Button();
            this.buttonCopyClipboard = new System.Windows.Forms.Button();
            this.buttonFormat = new System.Windows.Forms.Button();
            this.labelInfoText = new System.Windows.Forms.Label();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOk
            // 
            resources.ApplyResources(this.buttonOk, "buttonOk");
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonOk);
            this.panel1.Name = "panel1";
            // 
            // textBoxConfiguration
            // 
            resources.ApplyResources(this.textBoxConfiguration, "textBoxConfiguration");
            this.textBoxConfiguration.Name = "textBoxConfiguration";
            this.textBoxConfiguration.TextChanged += new System.EventHandler(this.textBoxConfiguration_TextChanged);
            // 
            // labelWarningJSON
            // 
            resources.ApplyResources(this.labelWarningJSON, "labelWarningJSON");
            this.labelWarningJSON.ForeColor = System.Drawing.Color.Red;
            this.labelWarningJSON.Name = "labelWarningJSON";
            this.labelWarningJSON.Tag = "";
            // 
            // buttonInsertSample
            // 
            resources.ApplyResources(this.buttonInsertSample, "buttonInsertSample");
            this.buttonInsertSample.Name = "buttonInsertSample";
            this.buttonInsertSample.UseVisualStyleBackColor = true;
            this.buttonInsertSample.Click += new System.EventHandler(this.buttonInsertSample_Click);
            // 
            // buttonCopyClipboard
            // 
            resources.ApplyResources(this.buttonCopyClipboard, "buttonCopyClipboard");
            this.buttonCopyClipboard.Name = "buttonCopyClipboard";
            this.buttonCopyClipboard.UseVisualStyleBackColor = true;
            this.buttonCopyClipboard.Click += new System.EventHandler(this.buttonCopyClipboard_Click);
            // 
            // buttonFormat
            // 
            resources.ApplyResources(this.buttonFormat, "buttonFormat");
            this.buttonFormat.Name = "buttonFormat";
            this.buttonFormat.UseVisualStyleBackColor = true;
            this.buttonFormat.Click += new System.EventHandler(this.buttonFormat_Click);
            // 
            // labelInfoText
            // 
            resources.ApplyResources(this.labelInfoText, "labelInfoText");
            this.labelInfoText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.labelInfoText.ForeColor = System.Drawing.Color.Black;
            this.labelInfoText.Name = "labelInfoText";
            // 
            // dataGridViewTextBoxColumn1
            // 
            resources.ApplyResources(this.dataGridViewTextBoxColumn1, "dataGridViewTextBoxColumn1");
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            resources.ApplyResources(this.dataGridViewTextBoxColumn2, "dataGridViewTextBoxColumn2");
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // EditorXMLJSON
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.labelInfoText);
            this.Controls.Add(this.buttonFormat);
            this.Controls.Add(this.buttonCopyClipboard);
            this.Controls.Add(this.buttonInsertSample);
            this.Controls.Add(this.labelWarningJSON);
            this.Controls.Add(this.textBoxConfiguration);
            this.Controls.Add(this.panel1);
            this.Name = "EditorXMLJSON";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.EditorXMLJSON_Load);
            this.Shown += new System.EventHandler(this.EditorXMLJSON_Shown);
            this.DpiChanged += new System.Windows.Forms.DpiChangedEventHandler(this.EditorXMLJSON_DpiChanged);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button buttonOk;
        public System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBoxConfiguration;
        private System.Windows.Forms.Label labelWarningJSON;
        private System.Windows.Forms.Button buttonInsertSample;
        private System.Windows.Forms.Button buttonCopyClipboard;
        private System.Windows.Forms.Button buttonFormat;
        private System.Windows.Forms.Label labelInfoText;
    }
}