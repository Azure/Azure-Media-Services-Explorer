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
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Name = "buttonCancel";
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
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label4.Name = "label4";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label2.Name = "label2";
            // 
            // radioButtonPremium
            // 
            resources.ApplyResources(this.radioButtonPremium, "radioButtonPremium");
            this.radioButtonPremium.Name = "radioButtonPremium";
            this.toolTip1.SetToolTip(this.radioButtonPremium, resources.GetString("radioButtonPremium.ToolTip"));
            this.radioButtonPremium.UseVisualStyleBackColor = true;
            this.radioButtonPremium.CheckedChanged += new System.EventHandler(this.radioButtonPremium_CheckedChanged);
            // 
            // radioButtonStandard
            // 
            resources.ApplyResources(this.radioButtonStandard, "radioButtonStandard");
            this.radioButtonStandard.Checked = true;
            this.radioButtonStandard.Name = "radioButtonStandard";
            this.radioButtonStandard.TabStop = true;
            this.toolTip1.SetToolTip(this.radioButtonStandard, resources.GetString("radioButtonStandard.ToolTip"));
            this.radioButtonStandard.UseVisualStyleBackColor = true;
            // 
            // labelCloneFilters
            // 
            resources.ApplyResources(this.labelCloneFilters, "labelCloneFilters");
            this.labelCloneFilters.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.labelCloneFilters.Name = "labelCloneFilters";
            // 
            // checkBoxEnableAzureCDN
            // 
            resources.ApplyResources(this.checkBoxEnableAzureCDN, "checkBoxEnableAzureCDN");
            this.checkBoxEnableAzureCDN.Name = "checkBoxEnableAzureCDN";
            this.checkBoxEnableAzureCDN.UseVisualStyleBackColor = true;
            // 
            // numericUpDownUnits
            // 
            resources.ApplyResources(this.numericUpDownUnits, "numericUpDownUnits");
            this.numericUpDownUnits.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownUnits.Name = "numericUpDownUnits";
            this.numericUpDownUnits.ReadOnly = true;
            this.numericUpDownUnits.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // textboxSEName
            // 
            resources.ApplyResources(this.textboxSEName, "textboxSEName");
            this.textboxSEName.Name = "textboxSEName";
            this.textboxSEName.TextChanged += new System.EventHandler(this.textboxSEName_TextChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // textBoxOriginDescription
            // 
            resources.ApplyResources(this.textBoxOriginDescription, "textBoxOriginDescription");
            this.textBoxOriginDescription.Name = "textBoxOriginDescription";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonOk);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // moreinfoSE
            // 
            resources.ApplyResources(this.moreinfoSE, "moreinfoSE");
            this.moreinfoSE.Name = "moreinfoSE";
            this.moreinfoSE.TabStop = true;
            this.moreinfoSE.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.moreinfoame_LinkClicked);
            // 
            // CreateStreamingEndpoint
            // 
            this.AcceptButton = this.buttonOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
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
            this.Load += new System.EventHandler(this.CreateStreamingEndpoint_Load);
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