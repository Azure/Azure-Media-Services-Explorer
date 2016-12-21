namespace AMSExplorer
{
    partial class ConfigureTelemetry
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigureTelemetry));
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonDeleteConfig = new System.Windows.Forms.Button();
            this.label33 = new System.Windows.Forms.Label();
            this.comboBoxStorage = new System.Windows.Forms.ComboBox();
            this.labelTelemetryUI = new System.Windows.Forms.Label();
            this.comboBoxLevelChannel = new System.Windows.Forms.ComboBox();
            this.comboBoxLevelSE = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxTableURL = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.moreinfoLiveEncodingProfilelink = new System.Windows.Forms.LinkLabel();
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
            this.panel1.Controls.Add(this.buttonDeleteConfig);
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonOk);
            this.panel1.Name = "panel1";
            // 
            // buttonDeleteConfig
            // 
            resources.ApplyResources(this.buttonDeleteConfig, "buttonDeleteConfig");
            this.buttonDeleteConfig.DialogResult = System.Windows.Forms.DialogResult.Abort;
            this.buttonDeleteConfig.Name = "buttonDeleteConfig";
            this.buttonDeleteConfig.UseVisualStyleBackColor = true;
            // 
            // label33
            // 
            resources.ApplyResources(this.label33, "label33");
            this.label33.Name = "label33";
            // 
            // comboBoxStorage
            // 
            this.comboBoxStorage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStorage.FormattingEnabled = true;
            resources.ApplyResources(this.comboBoxStorage, "comboBoxStorage");
            this.comboBoxStorage.Name = "comboBoxStorage";
            // 
            // labelTelemetryUI
            // 
            resources.ApplyResources(this.labelTelemetryUI, "labelTelemetryUI");
            this.labelTelemetryUI.ForeColor = System.Drawing.Color.DarkBlue;
            this.labelTelemetryUI.Name = "labelTelemetryUI";
            // 
            // comboBoxLevelChannel
            // 
            this.comboBoxLevelChannel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLevelChannel.FormattingEnabled = true;
            resources.ApplyResources(this.comboBoxLevelChannel, "comboBoxLevelChannel");
            this.comboBoxLevelChannel.Name = "comboBoxLevelChannel";
            // 
            // comboBoxLevelSE
            // 
            this.comboBoxLevelSE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLevelSE.FormattingEnabled = true;
            resources.ApplyResources(this.comboBoxLevelSE, "comboBoxLevelSE");
            this.comboBoxLevelSE.Name = "comboBoxLevelSE";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // textBoxTableURL
            // 
            resources.ApplyResources(this.textBoxTableURL, "textBoxTableURL");
            this.textBoxTableURL.Name = "textBoxTableURL";
            this.textBoxTableURL.ReadOnly = true;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // moreinfoLiveEncodingProfilelink
            // 
            resources.ApplyResources(this.moreinfoLiveEncodingProfilelink, "moreinfoLiveEncodingProfilelink");
            this.moreinfoLiveEncodingProfilelink.Name = "moreinfoLiveEncodingProfilelink";
            this.moreinfoLiveEncodingProfilelink.TabStop = true;
            this.moreinfoLiveEncodingProfilelink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.moreinfoLiveEncodingProfilelink_LinkClicked);
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
            // ConfigureTelemetry
            // 
            this.AcceptButton = this.buttonOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.moreinfoLiveEncodingProfilelink);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxLevelSE);
            this.Controls.Add(this.comboBoxLevelChannel);
            this.Controls.Add(this.labelTelemetryUI);
            this.Controls.Add(this.label33);
            this.Controls.Add(this.comboBoxStorage);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.textBoxTableURL);
            this.Name = "ConfigureTelemetry";
            this.Load += new System.EventHandler(this.ConfigureTelemetry_Load);
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
        private System.Windows.Forms.ComboBox comboBoxStorage;
        private System.Windows.Forms.Label labelTelemetryUI;
        private System.Windows.Forms.ComboBox comboBoxLevelChannel;
        private System.Windows.Forms.ComboBox comboBoxLevelSE;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Button buttonDeleteConfig;
        private System.Windows.Forms.TextBox textBoxTableURL;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel moreinfoLiveEncodingProfilelink;
    }
}