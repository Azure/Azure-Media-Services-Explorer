namespace AMSExplorer
{
    partial class AssetInfoTextTrackCreation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AssetInfoTextTrackCreation));
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxTexttrackLanguage = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxDisplayName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.labelBlobName = new System.Windows.Forms.Label();
            this.checkBoxLanguage = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxTrackName = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Name = "buttonCancel";
            this.toolTip1.SetToolTip(this.buttonCancel, resources.GetString("buttonCancel.ToolTip"));
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonUpdate
            // 
            resources.ApplyResources(this.buttonUpdate, "buttonUpdate");
            this.buttonUpdate.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonUpdate.Name = "buttonUpdate";
            this.toolTip1.SetToolTip(this.buttonUpdate, resources.GetString("buttonUpdate.ToolTip"));
            this.buttonUpdate.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonUpdate);
            this.panel1.Name = "panel1";
            this.toolTip1.SetToolTip(this.panel1, resources.GetString("panel1.ToolTip"));
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.ForeColor = System.Drawing.Color.DarkBlue;
            this.label2.Name = "label2";
            this.toolTip1.SetToolTip(this.label2, resources.GetString("label2.ToolTip"));
            // 
            // comboBoxTexttrackLanguage
            // 
            resources.ApplyResources(this.comboBoxTexttrackLanguage, "comboBoxTexttrackLanguage");
            this.comboBoxTexttrackLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTexttrackLanguage.FormattingEnabled = true;
            this.comboBoxTexttrackLanguage.Name = "comboBoxTexttrackLanguage";
            this.toolTip1.SetToolTip(this.comboBoxTexttrackLanguage, resources.GetString("comboBoxTexttrackLanguage.ToolTip"));
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            this.toolTip1.SetToolTip(this.label3, resources.GetString("label3.ToolTip"));
            // 
            // textBoxDisplayName
            // 
            resources.ApplyResources(this.textBoxDisplayName, "textBoxDisplayName");
            this.textBoxDisplayName.Name = "textBoxDisplayName";
            this.toolTip1.SetToolTip(this.textBoxDisplayName, resources.GetString("textBoxDisplayName.ToolTip"));
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            this.toolTip1.SetToolTip(this.label4, resources.GetString("label4.ToolTip"));
            // 
            // labelBlobName
            // 
            resources.ApplyResources(this.labelBlobName, "labelBlobName");
            this.labelBlobName.Name = "labelBlobName";
            this.toolTip1.SetToolTip(this.labelBlobName, resources.GetString("labelBlobName.ToolTip"));
            // 
            // checkBoxLanguage
            // 
            resources.ApplyResources(this.checkBoxLanguage, "checkBoxLanguage");
            this.checkBoxLanguage.Name = "checkBoxLanguage";
            this.toolTip1.SetToolTip(this.checkBoxLanguage, resources.GetString("checkBoxLanguage.ToolTip"));
            this.checkBoxLanguage.UseVisualStyleBackColor = true;
            this.checkBoxLanguage.CheckedChanged += new System.EventHandler(this.checkBoxLanguage_CheckedChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            this.toolTip1.SetToolTip(this.label1, resources.GetString("label1.ToolTip"));
            // 
            // textBoxTrackName
            // 
            resources.ApplyResources(this.textBoxTrackName, "textBoxTrackName");
            this.textBoxTrackName.Name = "textBoxTrackName";
            this.toolTip1.SetToolTip(this.textBoxTrackName, resources.GetString("textBoxTrackName.ToolTip"));
            // 
            // AssetInfoTextTrackCreation
            // 
            this.AcceptButton = this.buttonUpdate;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.textBoxTrackName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBoxLanguage);
            this.Controls.Add(this.labelBlobName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxDisplayName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBoxTexttrackLanguage);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.Name = "AssetInfoTextTrackCreation";
            this.toolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.Load += new System.EventHandler(this.AssetInfoTextTrackCreation_Load);
            this.Shown += new System.EventHandler(this.AssetInfoTextTrackCreation_Shown);
            this.DpiChanged += new System.Windows.Forms.DpiChangedEventHandler(this.AssetInfoTextTrackCreation_DpiChanged);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxTexttrackLanguage;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxDisplayName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelBlobName;
        private System.Windows.Forms.CheckBox checkBoxLanguage;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxTrackName;
    }
}