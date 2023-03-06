namespace AMSExplorer
{
    partial class AssetInfoAudioTrackCreation
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AssetInfoAudioTrackCreation));
            buttonCancel = new System.Windows.Forms.Button();
            buttonUpdate = new System.Windows.Forms.Button();
            panel1 = new System.Windows.Forms.Panel();
            label2 = new System.Windows.Forms.Label();
            comboBoxTexttrackLanguage = new System.Windows.Forms.ComboBox();
            label3 = new System.Windows.Forms.Label();
            textBoxDisplayName = new System.Windows.Forms.TextBox();
            label4 = new System.Windows.Forms.Label();
            labelBlobName = new System.Windows.Forms.Label();
            checkBoxLanguage = new System.Windows.Forms.CheckBox();
            toolTip1 = new System.Windows.Forms.ToolTip(components);
            checkBoxHLSSetAsDefault = new System.Windows.Forms.CheckBox();
            checkBoxIsHLSDescriptiveAudio = new System.Windows.Forms.CheckBox();
            label1 = new System.Windows.Forms.Label();
            textBoxTrackName = new System.Windows.Forms.TextBox();
            label5 = new System.Windows.Forms.Label();
            comboBoxDashRole = new System.Windows.Forms.ComboBox();
            label6 = new System.Windows.Forms.Label();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // buttonCancel
            // 
            resources.ApplyResources(buttonCancel, "buttonCancel");
            buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            buttonCancel.Name = "buttonCancel";
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += buttonCancel_Click;
            // 
            // buttonUpdate
            // 
            resources.ApplyResources(buttonUpdate, "buttonUpdate");
            buttonUpdate.DialogResult = System.Windows.Forms.DialogResult.OK;
            buttonUpdate.Name = "buttonUpdate";
            buttonUpdate.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.BackColor = System.Drawing.SystemColors.Control;
            panel1.Controls.Add(buttonCancel);
            panel1.Controls.Add(buttonUpdate);
            resources.ApplyResources(panel1, "panel1");
            panel1.Name = "panel1";
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.ForeColor = System.Drawing.Color.DarkBlue;
            label2.Name = "label2";
            // 
            // comboBoxTexttrackLanguage
            // 
            comboBoxTexttrackLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(comboBoxTexttrackLanguage, "comboBoxTexttrackLanguage");
            comboBoxTexttrackLanguage.FormattingEnabled = true;
            comboBoxTexttrackLanguage.Name = "comboBoxTexttrackLanguage";
            // 
            // label3
            // 
            resources.ApplyResources(label3, "label3");
            label3.Name = "label3";
            // 
            // textBoxDisplayName
            // 
            resources.ApplyResources(textBoxDisplayName, "textBoxDisplayName");
            textBoxDisplayName.Name = "textBoxDisplayName";
            // 
            // label4
            // 
            resources.ApplyResources(label4, "label4");
            label4.Name = "label4";
            // 
            // labelBlobName
            // 
            resources.ApplyResources(labelBlobName, "labelBlobName");
            labelBlobName.Name = "labelBlobName";
            // 
            // checkBoxLanguage
            // 
            resources.ApplyResources(checkBoxLanguage, "checkBoxLanguage");
            checkBoxLanguage.Name = "checkBoxLanguage";
            toolTip1.SetToolTip(checkBoxLanguage, resources.GetString("checkBoxLanguage.ToolTip"));
            checkBoxLanguage.UseVisualStyleBackColor = true;
            checkBoxLanguage.CheckedChanged += checkBoxLanguage_CheckedChanged;
            // 
            // checkBoxHLSSetAsDefault
            // 
            resources.ApplyResources(checkBoxHLSSetAsDefault, "checkBoxHLSSetAsDefault");
            checkBoxHLSSetAsDefault.Name = "checkBoxHLSSetAsDefault";
            toolTip1.SetToolTip(checkBoxHLSSetAsDefault, resources.GetString("checkBoxHLSSetAsDefault.ToolTip"));
            checkBoxHLSSetAsDefault.UseVisualStyleBackColor = true;
            // 
            // checkBoxIsHLSDescriptiveAudio
            // 
            resources.ApplyResources(checkBoxIsHLSDescriptiveAudio, "checkBoxIsHLSDescriptiveAudio");
            checkBoxIsHLSDescriptiveAudio.Name = "checkBoxIsHLSDescriptiveAudio";
            toolTip1.SetToolTip(checkBoxIsHLSDescriptiveAudio, resources.GetString("checkBoxIsHLSDescriptiveAudio.ToolTip"));
            checkBoxIsHLSDescriptiveAudio.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // textBoxTrackName
            // 
            resources.ApplyResources(textBoxTrackName, "textBoxTrackName");
            textBoxTrackName.Name = "textBoxTrackName";
            // 
            // label5
            // 
            resources.ApplyResources(label5, "label5");
            label5.Name = "label5";
            // 
            // comboBoxDashRole
            // 
            comboBoxDashRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxDashRole.FormattingEnabled = true;
            resources.ApplyResources(comboBoxDashRole, "comboBoxDashRole");
            comboBoxDashRole.Name = "comboBoxDashRole";
            // 
            // label6
            // 
            resources.ApplyResources(label6, "label6");
            label6.Name = "label6";
            // 
            // AssetInfoAudioTrackCreation
            // 
            AcceptButton = buttonUpdate;
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            BackColor = System.Drawing.SystemColors.Window;
            CancelButton = buttonCancel;
            Controls.Add(checkBoxIsHLSDescriptiveAudio);
            Controls.Add(checkBoxHLSSetAsDefault);
            Controls.Add(label6);
            Controls.Add(comboBoxDashRole);
            Controls.Add(label5);
            Controls.Add(textBoxTrackName);
            Controls.Add(label1);
            Controls.Add(checkBoxLanguage);
            Controls.Add(labelBlobName);
            Controls.Add(label4);
            Controls.Add(textBoxDisplayName);
            Controls.Add(label3);
            Controls.Add(comboBoxTexttrackLanguage);
            Controls.Add(label2);
            Controls.Add(panel1);
            Name = "AssetInfoAudioTrackCreation";
            Load += AssetInfoTextTrackCreation_Load;
            Shown += AssetInfoTextTrackCreation_Shown;
            DpiChanged += AssetInfoTextTrackCreation_DpiChanged;
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
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
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxDashRole;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox checkBoxHLSSetAsDefault;
        private System.Windows.Forms.CheckBox checkBoxIsHLSDescriptiveAudio;
    }
}