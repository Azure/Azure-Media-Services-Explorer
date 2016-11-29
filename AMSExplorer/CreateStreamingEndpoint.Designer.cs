namespace AMSExplorer
{
    partial class CreateStreamingEndpoint
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateStreamingEndpoint));
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.labelCloneFilters = new System.Windows.Forms.Label();
            this.checkBoxEnableAzureCDN = new System.Windows.Forms.CheckBox();
            this.numericUpDownRU = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textboxSEName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxOriginDescription = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRU)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            resources.ApplyResources(this.buttonOk, "buttonOk");
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.errorProvider1.SetError(this.buttonOk, resources.GetString("buttonOk.Error"));
            this.errorProvider1.SetIconAlignment(this.buttonOk, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("buttonOk.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.buttonOk, ((int)(resources.GetObject("buttonOk.IconPadding"))));
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
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
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // groupBox4
            // 
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Controls.Add(this.labelCloneFilters);
            this.groupBox4.Controls.Add(this.checkBoxEnableAzureCDN);
            this.groupBox4.Controls.Add(this.numericUpDownRU);
            this.groupBox4.Controls.Add(this.label2);
            this.errorProvider1.SetError(this.groupBox4, resources.GetString("groupBox4.Error"));
            this.errorProvider1.SetIconAlignment(this.groupBox4, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("groupBox4.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.groupBox4, ((int)(resources.GetObject("groupBox4.IconPadding"))));
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            // 
            // labelCloneFilters
            // 
            resources.ApplyResources(this.labelCloneFilters, "labelCloneFilters");
            this.errorProvider1.SetError(this.labelCloneFilters, resources.GetString("labelCloneFilters.Error"));
            this.labelCloneFilters.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.errorProvider1.SetIconAlignment(this.labelCloneFilters, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("labelCloneFilters.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.labelCloneFilters, ((int)(resources.GetObject("labelCloneFilters.IconPadding"))));
            this.labelCloneFilters.Name = "labelCloneFilters";
            // 
            // checkBoxEnableAzureCDN
            // 
            resources.ApplyResources(this.checkBoxEnableAzureCDN, "checkBoxEnableAzureCDN");
            this.errorProvider1.SetError(this.checkBoxEnableAzureCDN, resources.GetString("checkBoxEnableAzureCDN.Error"));
            this.errorProvider1.SetIconAlignment(this.checkBoxEnableAzureCDN, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("checkBoxEnableAzureCDN.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.checkBoxEnableAzureCDN, ((int)(resources.GetObject("checkBoxEnableAzureCDN.IconPadding"))));
            this.checkBoxEnableAzureCDN.Name = "checkBoxEnableAzureCDN";
            this.checkBoxEnableAzureCDN.UseVisualStyleBackColor = true;
            // 
            // numericUpDownRU
            // 
            resources.ApplyResources(this.numericUpDownRU, "numericUpDownRU");
            this.errorProvider1.SetError(this.numericUpDownRU, resources.GetString("numericUpDownRU.Error"));
            this.errorProvider1.SetIconAlignment(this.numericUpDownRU, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("numericUpDownRU.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.numericUpDownRU, ((int)(resources.GetObject("numericUpDownRU.IconPadding"))));
            this.numericUpDownRU.Name = "numericUpDownRU";
            this.numericUpDownRU.ReadOnly = true;
            this.numericUpDownRU.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownRU.ValueChanged += new System.EventHandler(this.numericUpDownRU_ValueChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.errorProvider1.SetError(this.label2, resources.GetString("label2.Error"));
            this.errorProvider1.SetIconAlignment(this.label2, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label2.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label2, ((int)(resources.GetObject("label2.IconPadding"))));
            this.label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.errorProvider1.SetError(this.label3, resources.GetString("label3.Error"));
            this.errorProvider1.SetIconAlignment(this.label3, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label3.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label3, ((int)(resources.GetObject("label3.IconPadding"))));
            this.label3.Name = "label3";
            // 
            // textboxSEName
            // 
            resources.ApplyResources(this.textboxSEName, "textboxSEName");
            this.errorProvider1.SetError(this.textboxSEName, resources.GetString("textboxSEName.Error"));
            this.errorProvider1.SetIconAlignment(this.textboxSEName, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("textboxSEName.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.textboxSEName, ((int)(resources.GetObject("textboxSEName.IconPadding"))));
            this.textboxSEName.Name = "textboxSEName";
            this.textboxSEName.TextChanged += new System.EventHandler(this.textboxSEName_TextChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.errorProvider1.SetError(this.label1, resources.GetString("label1.Error"));
            this.errorProvider1.SetIconAlignment(this.label1, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label1.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label1, ((int)(resources.GetObject("label1.IconPadding"))));
            this.label1.Name = "label1";
            // 
            // textBoxOriginDescription
            // 
            resources.ApplyResources(this.textBoxOriginDescription, "textBoxOriginDescription");
            this.errorProvider1.SetError(this.textBoxOriginDescription, resources.GetString("textBoxOriginDescription.Error"));
            this.errorProvider1.SetIconAlignment(this.textBoxOriginDescription, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("textBoxOriginDescription.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.textBoxOriginDescription, ((int)(resources.GetObject("textBoxOriginDescription.IconPadding"))));
            this.textBoxOriginDescription.Name = "textBoxOriginDescription";
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonOk);
            this.errorProvider1.SetError(this.panel1, resources.GetString("panel1.Error"));
            this.errorProvider1.SetIconAlignment(this.panel1, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("panel1.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.panel1, ((int)(resources.GetObject("panel1.IconPadding"))));
            this.panel1.Name = "panel1";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            resources.ApplyResources(this.errorProvider1, "errorProvider1");
            // 
            // CreateStreamingEndpoint
            // 
            this.AcceptButton = this.buttonOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxOriginDescription);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textboxSEName);
            this.Controls.Add(this.groupBox4);
            this.Name = "CreateStreamingEndpoint";
            this.Load += new System.EventHandler(this.CreateStreamingEndpoint_Load);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRU)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button buttonOk;
        public System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textboxSEName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxOriginDescription;
        private System.Windows.Forms.NumericUpDown numericUpDownRU;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox checkBoxEnableAzureCDN;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label labelCloneFilters;
    }
}