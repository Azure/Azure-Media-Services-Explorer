namespace AMSExplorer
{
    partial class ChooseStreamingEndpoint
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChooseStreamingEndpoint));
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.radioButtonHttps = new System.Windows.Forms.RadioButton();
            this.radioButtonHttp = new System.Windows.Forms.RadioButton();
            this.listBoxSE = new System.Windows.Forms.ListBox();
            this.groupBoxForceLocator = new System.Windows.Forms.GroupBox();
            this.listViewFilters = new System.Windows.Forms.ListView();
            this.label8 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.comboBoxBrowser = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtonHLSCMAF = new System.Windows.Forms.RadioButton();
            this.checkBoxNoAudioOnly = new System.Windows.Forms.CheckBox();
            this.labelaudiotrackname = new System.Windows.Forms.Label();
            this.textBoxHLSAudioTrackName = new System.Windows.Forms.TextBox();
            this.radioButtonSmoothLegacy = new System.Windows.Forms.RadioButton();
            this.radioButtonDASHCSF = new System.Windows.Forms.RadioButton();
            this.radioButtonDASHCMAF = new System.Windows.Forms.RadioButton();
            this.radioButtonHLSv4 = new System.Windows.Forms.RadioButton();
            this.radioButtonHLSv3 = new System.Windows.Forms.RadioButton();
            this.radioButtonSmooth = new System.Windows.Forms.RadioButton();
            this.label = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxPreviewURL = new System.Windows.Forms.TextBox();
            this.groupBox4.SuspendLayout();
            this.groupBoxForceLocator.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            resources.ApplyResources(this.buttonOk, "buttonOk");
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Controls.Add(this.radioButtonHttps);
            this.groupBox4.Controls.Add(this.radioButtonHttp);
            this.groupBox4.Controls.Add(this.listBoxSE);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            // 
            // radioButtonHttps
            // 
            resources.ApplyResources(this.radioButtonHttps, "radioButtonHttps");
            this.radioButtonHttps.Checked = true;
            this.radioButtonHttps.Name = "radioButtonHttps";
            this.radioButtonHttps.TabStop = true;
            this.radioButtonHttps.UseVisualStyleBackColor = true;
            // 
            // radioButtonHttp
            // 
            resources.ApplyResources(this.radioButtonHttp, "radioButtonHttp");
            this.radioButtonHttp.Name = "radioButtonHttp";
            this.radioButtonHttp.UseVisualStyleBackColor = true;
            this.radioButtonHttp.CheckedChanged += new System.EventHandler(this.radioButtonHttp_CheckedChanged);
            // 
            // listBoxSE
            // 
            resources.ApplyResources(this.listBoxSE, "listBoxSE");
            this.listBoxSE.FormattingEnabled = true;
            this.listBoxSE.Name = "listBoxSE";
            this.listBoxSE.SelectedIndexChanged += new System.EventHandler(this.listBoxSE_SelectedIndexChanged);
            // 
            // groupBoxForceLocator
            // 
            resources.ApplyResources(this.groupBoxForceLocator, "groupBoxForceLocator");
            this.groupBoxForceLocator.Controls.Add(this.listViewFilters);
            this.groupBoxForceLocator.Controls.Add(this.label8);
            this.groupBoxForceLocator.Name = "groupBoxForceLocator";
            this.groupBoxForceLocator.TabStop = false;
            // 
            // listViewFilters
            // 
            resources.ApplyResources(this.listViewFilters, "listViewFilters");
            this.listViewFilters.CheckBoxes = true;
            this.listViewFilters.FullRowSelect = true;
            this.listViewFilters.GridLines = true;
            this.listViewFilters.Name = "listViewFilters";
            this.listViewFilters.UseCompatibleStateImageBehavior = false;
            this.listViewFilters.View = System.Windows.Forms.View.List;
            this.listViewFilters.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listViewFilters_ItemChecked);
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label8.Name = "label8";
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.comboBoxBrowser);
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonOk);
            this.panel1.Name = "panel1";
            // 
            // comboBoxBrowser
            // 
            resources.ApplyResources(this.comboBoxBrowser, "comboBoxBrowser");
            this.comboBoxBrowser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBrowser.FormattingEnabled = true;
            this.comboBoxBrowser.Name = "comboBoxBrowser";
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.radioButtonHLSCMAF);
            this.groupBox1.Controls.Add(this.checkBoxNoAudioOnly);
            this.groupBox1.Controls.Add(this.labelaudiotrackname);
            this.groupBox1.Controls.Add(this.textBoxHLSAudioTrackName);
            this.groupBox1.Controls.Add(this.radioButtonSmoothLegacy);
            this.groupBox1.Controls.Add(this.radioButtonDASHCSF);
            this.groupBox1.Controls.Add(this.radioButtonDASHCMAF);
            this.groupBox1.Controls.Add(this.radioButtonHLSv4);
            this.groupBox1.Controls.Add(this.radioButtonHLSv3);
            this.groupBox1.Controls.Add(this.radioButtonSmooth);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // radioButtonHLSCMAF
            // 
            resources.ApplyResources(this.radioButtonHLSCMAF, "radioButtonHLSCMAF");
            this.radioButtonHLSCMAF.Name = "radioButtonHLSCMAF";
            this.radioButtonHLSCMAF.UseVisualStyleBackColor = true;
            this.radioButtonHLSCMAF.CheckedChanged += new System.EventHandler(this.radioButtonHLSCMAF_CheckedChanged);
            // 
            // checkBoxNoAudioOnly
            // 
            resources.ApplyResources(this.checkBoxNoAudioOnly, "checkBoxNoAudioOnly");
            this.checkBoxNoAudioOnly.Name = "checkBoxNoAudioOnly";
            this.checkBoxNoAudioOnly.UseVisualStyleBackColor = true;
            this.checkBoxNoAudioOnly.CheckedChanged += new System.EventHandler(this.checkBoxNoAudioOnly_CheckedChanged);
            // 
            // labelaudiotrackname
            // 
            resources.ApplyResources(this.labelaudiotrackname, "labelaudiotrackname");
            this.labelaudiotrackname.Name = "labelaudiotrackname";
            // 
            // textBoxHLSAudioTrackName
            // 
            resources.ApplyResources(this.textBoxHLSAudioTrackName, "textBoxHLSAudioTrackName");
            this.textBoxHLSAudioTrackName.Name = "textBoxHLSAudioTrackName";
            this.textBoxHLSAudioTrackName.TextChanged += new System.EventHandler(this.textBoxHLSAudioTrackName_TextChanged);
            // 
            // radioButtonSmoothLegacy
            // 
            resources.ApplyResources(this.radioButtonSmoothLegacy, "radioButtonSmoothLegacy");
            this.radioButtonSmoothLegacy.Name = "radioButtonSmoothLegacy";
            this.radioButtonSmoothLegacy.UseVisualStyleBackColor = true;
            this.radioButtonSmoothLegacy.CheckedChanged += new System.EventHandler(this.radioButtonSmooth_CheckedChanged);
            // 
            // radioButtonDASHCSF
            // 
            resources.ApplyResources(this.radioButtonDASHCSF, "radioButtonDASHCSF");
            this.radioButtonDASHCSF.Name = "radioButtonDASHCSF";
            this.radioButtonDASHCSF.UseVisualStyleBackColor = true;
            this.radioButtonDASHCSF.CheckedChanged += new System.EventHandler(this.radioButtonSmooth_CheckedChanged);
            // 
            // radioButtonDASHCMAF
            // 
            resources.ApplyResources(this.radioButtonDASHCMAF, "radioButtonDASHCMAF");
            this.radioButtonDASHCMAF.Name = "radioButtonDASHCMAF";
            this.radioButtonDASHCMAF.UseVisualStyleBackColor = true;
            this.radioButtonDASHCMAF.CheckedChanged += new System.EventHandler(this.radioButtonSmooth_CheckedChanged);
            // 
            // radioButtonHLSv4
            // 
            resources.ApplyResources(this.radioButtonHLSv4, "radioButtonHLSv4");
            this.radioButtonHLSv4.Name = "radioButtonHLSv4";
            this.radioButtonHLSv4.UseVisualStyleBackColor = true;
            this.radioButtonHLSv4.CheckedChanged += new System.EventHandler(this.radioButtonSmooth_CheckedChanged);
            // 
            // radioButtonHLSv3
            // 
            resources.ApplyResources(this.radioButtonHLSv3, "radioButtonHLSv3");
            this.radioButtonHLSv3.Name = "radioButtonHLSv3";
            this.radioButtonHLSv3.UseVisualStyleBackColor = true;
            this.radioButtonHLSv3.CheckedChanged += new System.EventHandler(this.radioButtonHLSv3_CheckedChanged);
            // 
            // radioButtonSmooth
            // 
            resources.ApplyResources(this.radioButtonSmooth, "radioButtonSmooth");
            this.radioButtonSmooth.Checked = true;
            this.radioButtonSmooth.Name = "radioButtonSmooth";
            this.radioButtonSmooth.TabStop = true;
            this.radioButtonSmooth.UseVisualStyleBackColor = true;
            this.radioButtonSmooth.CheckedChanged += new System.EventHandler(this.radioButtonSmooth_CheckedChanged);
            // 
            // label
            // 
            resources.ApplyResources(this.label, "label");
            this.label.Name = "label";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.ForeColor = System.Drawing.Color.DarkBlue;
            this.label5.Name = "label5";
            // 
            // textBoxPreviewURL
            // 
            resources.ApplyResources(this.textBoxPreviewURL, "textBoxPreviewURL");
            this.textBoxPreviewURL.Name = "textBoxPreviewURL";
            this.textBoxPreviewURL.ReadOnly = true;
            // 
            // ChooseStreamingEndpoint
            // 
            this.AcceptButton = this.buttonOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.textBoxPreviewURL);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBoxForceLocator);
            this.Controls.Add(this.groupBox4);
            this.Name = "ChooseStreamingEndpoint";
            this.Load += new System.EventHandler(this.ChooseStreamingEndpoint_Load);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBoxForceLocator.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button buttonOk;
        public System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBoxForceLocator;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListBox listBoxSE;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelaudiotrackname;
        private System.Windows.Forms.TextBox textBoxHLSAudioTrackName;
        private System.Windows.Forms.RadioButton radioButtonSmoothLegacy;
        private System.Windows.Forms.RadioButton radioButtonDASHCSF;
        private System.Windows.Forms.RadioButton radioButtonDASHCMAF;
        private System.Windows.Forms.RadioButton radioButtonHLSv4;
        private System.Windows.Forms.RadioButton radioButtonHLSv3;
        private System.Windows.Forms.RadioButton radioButtonSmooth;
        private System.Windows.Forms.RadioButton radioButtonHttps;
        private System.Windows.Forms.RadioButton radioButtonHttp;
        public System.Windows.Forms.Label label;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox checkBoxNoAudioOnly;
        private System.Windows.Forms.TextBox textBoxPreviewURL;
        private System.Windows.Forms.ListView listViewFilters;
        private System.Windows.Forms.ComboBox comboBoxBrowser;
        private System.Windows.Forms.RadioButton radioButtonHLSCMAF;
    }
}