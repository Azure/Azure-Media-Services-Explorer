namespace AMSExplorer
{
    partial class StreamingEndpointCDNEnable
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StreamingEndpointCDNEnable));
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label33 = new System.Windows.Forms.Label();
            this.comboBoxProvider = new System.Windows.Forms.ComboBox();
            this.labelCDNOptions = new System.Windows.Forms.Label();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxProfile = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
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
            // label33
            // 
            resources.ApplyResources(this.label33, "label33");
            this.label33.Name = "label33";
            // 
            // comboBoxProvider
            // 
            resources.ApplyResources(this.comboBoxProvider, "comboBoxProvider");
            this.comboBoxProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxProvider.FormattingEnabled = true;
            this.comboBoxProvider.Name = "comboBoxProvider";
            // 
            // labelCDNOptions
            // 
            resources.ApplyResources(this.labelCDNOptions, "labelCDNOptions");
            this.labelCDNOptions.ForeColor = System.Drawing.Color.DarkBlue;
            this.labelCDNOptions.Name = "labelCDNOptions";
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
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // textBoxProfile
            // 
            resources.ApplyResources(this.textBoxProfile, "textBoxProfile");
            this.textBoxProfile.Name = "textBoxProfile";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label2.Name = "label2";
            // 
            // StreamingEndpointCDNEnable
            // 
            this.AcceptButton = this.buttonOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxProfile);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelCDNOptions);
            this.Controls.Add(this.label33);
            this.Controls.Add(this.comboBoxProvider);
            this.Controls.Add(this.panel1);
            this.Name = "StreamingEndpointCDNEnable";
            this.Load += new System.EventHandler(this.StreamingEndpointCDNEnable_Load);
            this.Shown += new System.EventHandler(this.StreamingEndpointCDNEnable_Shown);
            this.DpiChanged += new System.Windows.Forms.DpiChangedEventHandler(this.StreamingEndpointCDNEnable_DpiChanged);
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
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.ComboBox comboBoxProvider;
        private System.Windows.Forms.Label labelCDNOptions;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxProfile;
        private System.Windows.Forms.Label label2;
    }
}