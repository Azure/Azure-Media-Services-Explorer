namespace AMSExplorer
{
    partial class ImportHttp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportHttp));
            this.buttonImport = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxURL = new System.Windows.Forms.TextBox();
            this.labelExamples = new System.Windows.Forms.Label();
            this.labelURLFileNameWarning = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonAdvancedOptions = new System.Windows.Forms.Button();
            this.label33 = new System.Windows.Forms.Label();
            this.comboBoxStorage = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelTitle = new System.Windows.Forms.Label();
            this.labelSASListExample = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonImport
            // 
            resources.ApplyResources(this.buttonImport, "buttonImport");
            this.buttonImport.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.errorProvider1.SetError(this.buttonImport, resources.GetString("buttonImport.Error"));
            this.errorProvider1.SetIconAlignment(this.buttonImport, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("buttonImport.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.buttonImport, ((int)(resources.GetObject("buttonImport.IconPadding"))));
            this.buttonImport.Name = "buttonImport";
            this.buttonImport.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.errorProvider1.SetError(this.buttonCancel, resources.GetString("buttonCancel.Error"));
            this.errorProvider1.SetIconAlignment(this.buttonCancel, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("buttonCancel.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.buttonCancel, ((int)(resources.GetObject("buttonCancel.IconPadding"))));
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.errorProvider1.SetError(this.label1, resources.GetString("label1.Error"));
            this.errorProvider1.SetIconAlignment(this.label1, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label1.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label1, ((int)(resources.GetObject("label1.IconPadding"))));
            this.label1.Name = "label1";
            // 
            // textBoxURL
            // 
            resources.ApplyResources(this.textBoxURL, "textBoxURL");
            this.errorProvider1.SetError(this.textBoxURL, resources.GetString("textBoxURL.Error"));
            this.errorProvider1.SetIconAlignment(this.textBoxURL, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("textBoxURL.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.textBoxURL, ((int)(resources.GetObject("textBoxURL.IconPadding"))));
            this.textBoxURL.Name = "textBoxURL";
            this.textBoxURL.TextChanged += new System.EventHandler(this.textBoxURL_TextChanged);
            // 
            // labelExamples
            // 
            resources.ApplyResources(this.labelExamples, "labelExamples");
            this.errorProvider1.SetError(this.labelExamples, resources.GetString("labelExamples.Error"));
            this.labelExamples.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.errorProvider1.SetIconAlignment(this.labelExamples, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("labelExamples.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.labelExamples, ((int)(resources.GetObject("labelExamples.IconPadding"))));
            this.labelExamples.Name = "labelExamples";
            // 
            // labelURLFileNameWarning
            // 
            resources.ApplyResources(this.labelURLFileNameWarning, "labelURLFileNameWarning");
            this.errorProvider1.SetError(this.labelURLFileNameWarning, resources.GetString("labelURLFileNameWarning.Error"));
            this.labelURLFileNameWarning.ForeColor = System.Drawing.Color.Red;
            this.errorProvider1.SetIconAlignment(this.labelURLFileNameWarning, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("labelURLFileNameWarning.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.labelURLFileNameWarning, ((int)(resources.GetObject("labelURLFileNameWarning.IconPadding"))));
            this.labelURLFileNameWarning.Name = "labelURLFileNameWarning";
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.buttonAdvancedOptions);
            this.groupBox1.Controls.Add(this.label33);
            this.groupBox1.Controls.Add(this.comboBoxStorage);
            this.errorProvider1.SetError(this.groupBox1, resources.GetString("groupBox1.Error"));
            this.errorProvider1.SetIconAlignment(this.groupBox1, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("groupBox1.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.groupBox1, ((int)(resources.GetObject("groupBox1.IconPadding"))));
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // buttonAdvancedOptions
            // 
            resources.ApplyResources(this.buttonAdvancedOptions, "buttonAdvancedOptions");
            this.errorProvider1.SetError(this.buttonAdvancedOptions, resources.GetString("buttonAdvancedOptions.Error"));
            this.errorProvider1.SetIconAlignment(this.buttonAdvancedOptions, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("buttonAdvancedOptions.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.buttonAdvancedOptions, ((int)(resources.GetObject("buttonAdvancedOptions.IconPadding"))));
            this.buttonAdvancedOptions.Name = "buttonAdvancedOptions";
            this.buttonAdvancedOptions.UseVisualStyleBackColor = true;
            this.buttonAdvancedOptions.Click += new System.EventHandler(this.ButtonAdvancedOptions_Click);
            // 
            // label33
            // 
            resources.ApplyResources(this.label33, "label33");
            this.errorProvider1.SetError(this.label33, resources.GetString("label33.Error"));
            this.errorProvider1.SetIconAlignment(this.label33, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label33.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label33, ((int)(resources.GetObject("label33.IconPadding"))));
            this.label33.Name = "label33";
            // 
            // comboBoxStorage
            // 
            resources.ApplyResources(this.comboBoxStorage, "comboBoxStorage");
            this.comboBoxStorage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.errorProvider1.SetError(this.comboBoxStorage, resources.GetString("comboBoxStorage.Error"));
            this.comboBoxStorage.FormattingEnabled = true;
            this.errorProvider1.SetIconAlignment(this.comboBoxStorage, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("comboBoxStorage.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.comboBoxStorage, ((int)(resources.GetObject("comboBoxStorage.IconPadding"))));
            this.comboBoxStorage.Name = "comboBoxStorage";
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonImport);
            this.errorProvider1.SetError(this.panel1, resources.GetString("panel1.Error"));
            this.errorProvider1.SetIconAlignment(this.panel1, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("panel1.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.panel1, ((int)(resources.GetObject("panel1.IconPadding"))));
            this.panel1.Name = "panel1";
            // 
            // labelTitle
            // 
            resources.ApplyResources(this.labelTitle, "labelTitle");
            this.errorProvider1.SetError(this.labelTitle, resources.GetString("labelTitle.Error"));
            this.labelTitle.ForeColor = System.Drawing.Color.DarkBlue;
            this.errorProvider1.SetIconAlignment(this.labelTitle, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("labelTitle.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.labelTitle, ((int)(resources.GetObject("labelTitle.IconPadding"))));
            this.labelTitle.Name = "labelTitle";
            // 
            // labelSASListExample
            // 
            resources.ApplyResources(this.labelSASListExample, "labelSASListExample");
            this.errorProvider1.SetError(this.labelSASListExample, resources.GetString("labelSASListExample.Error"));
            this.labelSASListExample.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.errorProvider1.SetIconAlignment(this.labelSASListExample, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("labelSASListExample.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.labelSASListExample, ((int)(resources.GetObject("labelSASListExample.IconPadding"))));
            this.labelSASListExample.Name = "labelSASListExample";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            resources.ApplyResources(this.errorProvider1, "errorProvider1");
            // 
            // ImportHttp
            // 
            this.AcceptButton = this.buttonImport;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.labelSASListExample);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.labelURLFileNameWarning);
            this.Controls.Add(this.labelExamples);
            this.Controls.Add(this.textBoxURL);
            this.Controls.Add(this.label1);
            this.Name = "ImportHttp";
            this.Load += new System.EventHandler(this.ImportHttp_Load);
            this.DpiChanged += new System.Windows.Forms.DpiChangedEventHandler(this.ImportHttp_DpiChanged);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelTitle;
        public System.Windows.Forms.Label labelSASListExample;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.ComboBox comboBoxStorage;
        private System.Windows.Forms.Button buttonAdvancedOptions;
    }
}