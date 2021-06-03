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
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.radioButtonPremium = new System.Windows.Forms.RadioButton();
            this.radioButtonStandard = new System.Windows.Forms.RadioButton();
            this.labelCloneFilters = new System.Windows.Forms.Label();
            this.checkBoxEnableAzureCDN = new System.Windows.Forms.CheckBox();
            this.numericUpDownUnits = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.textboxSEName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxOriginDescription = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.moreinfoSE = new System.Windows.Forms.LinkLabel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownUnits)).BeginInit();
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
            this.toolTip1.SetToolTip(this.buttonOk, resources.GetString("buttonOk.ToolTip"));
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
            this.toolTip1.SetToolTip(this.buttonCancel, resources.GetString("buttonCancel.ToolTip"));
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // groupBox4
            // 
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.radioButtonPremium);
            this.groupBox4.Controls.Add(this.radioButtonStandard);
            this.groupBox4.Controls.Add(this.labelCloneFilters);
            this.groupBox4.Controls.Add(this.checkBoxEnableAzureCDN);
            this.groupBox4.Controls.Add(this.numericUpDownUnits);
            this.errorProvider1.SetError(this.groupBox4, resources.GetString("groupBox4.Error"));
            this.errorProvider1.SetIconAlignment(this.groupBox4, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("groupBox4.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.groupBox4, ((int)(resources.GetObject("groupBox4.IconPadding"))));
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            this.toolTip1.SetToolTip(this.groupBox4, resources.GetString("groupBox4.ToolTip"));
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.errorProvider1.SetError(this.label4, resources.GetString("label4.Error"));
            this.label4.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.errorProvider1.SetIconAlignment(this.label4, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label4.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label4, ((int)(resources.GetObject("label4.IconPadding"))));
            this.label4.Name = "label4";
            this.toolTip1.SetToolTip(this.label4, resources.GetString("label4.ToolTip"));
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.errorProvider1.SetError(this.label2, resources.GetString("label2.Error"));
            this.label2.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.errorProvider1.SetIconAlignment(this.label2, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label2.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label2, ((int)(resources.GetObject("label2.IconPadding"))));
            this.label2.Name = "label2";
            this.toolTip1.SetToolTip(this.label2, resources.GetString("label2.ToolTip"));
            // 
            // radioButtonPremium
            // 
            resources.ApplyResources(this.radioButtonPremium, "radioButtonPremium");
            this.errorProvider1.SetError(this.radioButtonPremium, resources.GetString("radioButtonPremium.Error"));
            this.errorProvider1.SetIconAlignment(this.radioButtonPremium, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("radioButtonPremium.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.radioButtonPremium, ((int)(resources.GetObject("radioButtonPremium.IconPadding"))));
            this.radioButtonPremium.Name = "radioButtonPremium";
            this.toolTip1.SetToolTip(this.radioButtonPremium, resources.GetString("radioButtonPremium.ToolTip"));
            this.radioButtonPremium.UseVisualStyleBackColor = true;
            this.radioButtonPremium.CheckedChanged += new System.EventHandler(this.radioButtonPremium_CheckedChanged);
            // 
            // radioButtonStandard
            // 
            resources.ApplyResources(this.radioButtonStandard, "radioButtonStandard");
            this.radioButtonStandard.Checked = true;
            this.errorProvider1.SetError(this.radioButtonStandard, resources.GetString("radioButtonStandard.Error"));
            this.errorProvider1.SetIconAlignment(this.radioButtonStandard, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("radioButtonStandard.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.radioButtonStandard, ((int)(resources.GetObject("radioButtonStandard.IconPadding"))));
            this.radioButtonStandard.Name = "radioButtonStandard";
            this.radioButtonStandard.TabStop = true;
            this.toolTip1.SetToolTip(this.radioButtonStandard, resources.GetString("radioButtonStandard.ToolTip"));
            this.radioButtonStandard.UseVisualStyleBackColor = true;
            // 
            // labelCloneFilters
            // 
            resources.ApplyResources(this.labelCloneFilters, "labelCloneFilters");
            this.errorProvider1.SetError(this.labelCloneFilters, resources.GetString("labelCloneFilters.Error"));
            this.labelCloneFilters.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.errorProvider1.SetIconAlignment(this.labelCloneFilters, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("labelCloneFilters.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.labelCloneFilters, ((int)(resources.GetObject("labelCloneFilters.IconPadding"))));
            this.labelCloneFilters.Name = "labelCloneFilters";
            this.toolTip1.SetToolTip(this.labelCloneFilters, resources.GetString("labelCloneFilters.ToolTip"));
            // 
            // checkBoxEnableAzureCDN
            // 
            resources.ApplyResources(this.checkBoxEnableAzureCDN, "checkBoxEnableAzureCDN");
            this.errorProvider1.SetError(this.checkBoxEnableAzureCDN, resources.GetString("checkBoxEnableAzureCDN.Error"));
            this.errorProvider1.SetIconAlignment(this.checkBoxEnableAzureCDN, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("checkBoxEnableAzureCDN.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.checkBoxEnableAzureCDN, ((int)(resources.GetObject("checkBoxEnableAzureCDN.IconPadding"))));
            this.checkBoxEnableAzureCDN.Name = "checkBoxEnableAzureCDN";
            this.toolTip1.SetToolTip(this.checkBoxEnableAzureCDN, resources.GetString("checkBoxEnableAzureCDN.ToolTip"));
            this.checkBoxEnableAzureCDN.UseVisualStyleBackColor = true;
            // 
            // numericUpDownUnits
            // 
            resources.ApplyResources(this.numericUpDownUnits, "numericUpDownUnits");
            this.errorProvider1.SetError(this.numericUpDownUnits, resources.GetString("numericUpDownUnits.Error"));
            this.errorProvider1.SetIconAlignment(this.numericUpDownUnits, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("numericUpDownUnits.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.numericUpDownUnits, ((int)(resources.GetObject("numericUpDownUnits.IconPadding"))));
            this.numericUpDownUnits.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownUnits.Name = "numericUpDownUnits";
            this.numericUpDownUnits.ReadOnly = true;
            this.toolTip1.SetToolTip(this.numericUpDownUnits, resources.GetString("numericUpDownUnits.ToolTip"));
            this.numericUpDownUnits.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.errorProvider1.SetError(this.label3, resources.GetString("label3.Error"));
            this.errorProvider1.SetIconAlignment(this.label3, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label3.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label3, ((int)(resources.GetObject("label3.IconPadding"))));
            this.label3.Name = "label3";
            this.toolTip1.SetToolTip(this.label3, resources.GetString("label3.ToolTip"));
            // 
            // textboxSEName
            // 
            resources.ApplyResources(this.textboxSEName, "textboxSEName");
            this.errorProvider1.SetError(this.textboxSEName, resources.GetString("textboxSEName.Error"));
            this.errorProvider1.SetIconAlignment(this.textboxSEName, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("textboxSEName.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.textboxSEName, ((int)(resources.GetObject("textboxSEName.IconPadding"))));
            this.textboxSEName.Name = "textboxSEName";
            this.toolTip1.SetToolTip(this.textboxSEName, resources.GetString("textboxSEName.ToolTip"));
            this.textboxSEName.TextChanged += new System.EventHandler(this.textboxSEName_TextChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.errorProvider1.SetError(this.label1, resources.GetString("label1.Error"));
            this.errorProvider1.SetIconAlignment(this.label1, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label1.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label1, ((int)(resources.GetObject("label1.IconPadding"))));
            this.label1.Name = "label1";
            this.toolTip1.SetToolTip(this.label1, resources.GetString("label1.ToolTip"));
            // 
            // textBoxOriginDescription
            // 
            resources.ApplyResources(this.textBoxOriginDescription, "textBoxOriginDescription");
            this.errorProvider1.SetError(this.textBoxOriginDescription, resources.GetString("textBoxOriginDescription.Error"));
            this.errorProvider1.SetIconAlignment(this.textBoxOriginDescription, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("textBoxOriginDescription.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.textBoxOriginDescription, ((int)(resources.GetObject("textBoxOriginDescription.IconPadding"))));
            this.textBoxOriginDescription.Name = "textBoxOriginDescription";
            this.toolTip1.SetToolTip(this.textBoxOriginDescription, resources.GetString("textBoxOriginDescription.ToolTip"));
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
            this.toolTip1.SetToolTip(this.panel1, resources.GetString("panel1.ToolTip"));
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            resources.ApplyResources(this.errorProvider1, "errorProvider1");
            // 
            // moreinfoSE
            // 
            resources.ApplyResources(this.moreinfoSE, "moreinfoSE");
            this.errorProvider1.SetError(this.moreinfoSE, resources.GetString("moreinfoSE.Error"));
            this.errorProvider1.SetIconAlignment(this.moreinfoSE, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("moreinfoSE.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.moreinfoSE, ((int)(resources.GetObject("moreinfoSE.IconPadding"))));
            this.moreinfoSE.Name = "moreinfoSE";
            this.moreinfoSE.TabStop = true;
            this.toolTip1.SetToolTip(this.moreinfoSE, resources.GetString("moreinfoSE.ToolTip"));
            this.moreinfoSE.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.moreinfoame_LinkClicked);
            // 
            // CreateStreamingEndpoint
            // 
            this.AcceptButton = this.buttonOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.moreinfoSE);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxOriginDescription);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textboxSEName);
            this.Controls.Add(this.groupBox4);
            this.Name = "CreateStreamingEndpoint";
            this.toolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.Load += new System.EventHandler(this.CreateStreamingEndpoint_Load);
            this.Shown += new System.EventHandler(this.CreateStreamingEndpoint_Shown);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownUnits)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button buttonOk;
        public System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textboxSEName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxOriginDescription;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox checkBoxEnableAzureCDN;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label labelCloneFilters;
        private System.Windows.Forms.RadioButton radioButtonPremium;
        private System.Windows.Forms.RadioButton radioButtonStandard;
        private System.Windows.Forms.NumericUpDown numericUpDownUnits;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.LinkLabel moreinfoSE;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}