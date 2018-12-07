namespace AMSExplorer
{
    partial class HttpSource
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HttpSource));
            this.buttonImport = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxURL = new System.Windows.Forms.TextBox();
            this.labelExamples = new System.Windows.Forms.Label();
            this.labelURLFileNameWarning = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelTitle = new System.Windows.Forms.Label();
            this.labelSASListExample = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonImport
            // 
            resources.ApplyResources(this.buttonImport, "buttonImport");
            this.buttonImport.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonImport.Name = "buttonImport";
            this.buttonImport.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // textBoxURL
            // 
            resources.ApplyResources(this.textBoxURL, "textBoxURL");
            this.textBoxURL.Name = "textBoxURL";
            this.textBoxURL.TextChanged += new System.EventHandler(this.textBoxURL_TextChanged);
            // 
            // labelExamples
            // 
            resources.ApplyResources(this.labelExamples, "labelExamples");
            this.labelExamples.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.labelExamples.Name = "labelExamples";
            // 
            // labelURLFileNameWarning
            // 
            resources.ApplyResources(this.labelURLFileNameWarning, "labelURLFileNameWarning");
            this.labelURLFileNameWarning.ForeColor = System.Drawing.Color.Red;
            this.labelURLFileNameWarning.Name = "labelURLFileNameWarning";
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonImport);
            this.panel1.Name = "panel1";
            // 
            // labelTitle
            // 
            resources.ApplyResources(this.labelTitle, "labelTitle");
            this.labelTitle.ForeColor = System.Drawing.Color.DarkBlue;
            this.labelTitle.Name = "labelTitle";
            // 
            // labelSASListExample
            // 
            resources.ApplyResources(this.labelSASListExample, "labelSASListExample");
            this.labelSASListExample.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.labelSASListExample.Name = "labelSASListExample";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // HttpSource
            // 
            this.AcceptButton = this.buttonImport;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.labelSASListExample);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labelURLFileNameWarning);
            this.Controls.Add(this.labelExamples);
            this.Controls.Add(this.textBoxURL);
            this.Controls.Add(this.label1);
            this.Name = "HttpSource";
            this.Load += new System.EventHandler(this.HttpSource_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonImport;
        private System.Windows.Forms.Button buttonCancel;
        public System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxURL;
        public System.Windows.Forms.Label labelExamples;
        private System.Windows.Forms.Label labelURLFileNameWarning;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelTitle;
        public System.Windows.Forms.Label labelSASListExample;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}