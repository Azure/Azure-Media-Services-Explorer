﻿namespace AMSExplorer
{
    partial class PresetStandardEncoder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PresetStandardEncoder));
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.labelMES = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.moreinfoprofilelink = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxTransformName = new System.Windows.Forms.TextBox();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.richTextBoxDesc = new System.Windows.Forms.RichTextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.listboxPresets = new System.Windows.Forms.ListBox();
            this.radioButtonBuiltin = new System.Windows.Forms.RadioButton();
            this.radioButtonCustomCopy = new System.Windows.Forms.RadioButton();
            this.buttonCustomPresetThumbnail = new System.Windows.Forms.Button();
            this.radioButtonThumbnail = new System.Windows.Forms.RadioButton();
            this.labelCodec = new System.Windows.Forms.Label();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonConstrainedCAE = new System.Windows.Forms.Button();
            this.checkBoxCAEConstrained = new System.Windows.Forms.CheckBox();
            this.panelConfigureConstrained = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelConfigureConstrained.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Name = "buttonCancel";
            this.toolTip1.SetToolTip(this.buttonCancel, resources.GetString("buttonCancel.ToolTip"));
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOk
            // 
            resources.ApplyResources(this.buttonOk, "buttonOk");
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Name = "buttonOk";
            this.toolTip1.SetToolTip(this.buttonOk, resources.GetString("buttonOk.ToolTip"));
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // labelMES
            // 
            resources.ApplyResources(this.labelMES, "labelMES");
            this.labelMES.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(128)))), ((int)(((byte)(171)))));
            this.labelMES.Name = "labelMES";
            this.toolTip1.SetToolTip(this.labelMES, resources.GetString("labelMES.ToolTip"));
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonOk);
            this.panel1.Name = "panel1";
            this.toolTip1.SetToolTip(this.panel1, resources.GetString("panel1.ToolTip"));
            // 
            // moreinfoprofilelink
            // 
            resources.ApplyResources(this.moreinfoprofilelink, "moreinfoprofilelink");
            this.moreinfoprofilelink.Name = "moreinfoprofilelink";
            this.moreinfoprofilelink.TabStop = true;
            this.toolTip1.SetToolTip(this.moreinfoprofilelink, resources.GetString("moreinfoprofilelink.ToolTip"));
            this.moreinfoprofilelink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.moreinfoprofilelink_LinkClicked);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            this.toolTip1.SetToolTip(this.label1, resources.GetString("label1.ToolTip"));
            // 
            // textBoxTransformName
            // 
            resources.ApplyResources(this.textBoxTransformName, "textBoxTransformName");
            this.textBoxTransformName.Name = "textBoxTransformName";
            this.toolTip1.SetToolTip(this.textBoxTransformName, resources.GetString("textBoxTransformName.ToolTip"));
            // 
            // textBoxDescription
            // 
            resources.ApplyResources(this.textBoxDescription, "textBoxDescription");
            this.textBoxDescription.Name = "textBoxDescription";
            this.toolTip1.SetToolTip(this.textBoxDescription, resources.GetString("textBoxDescription.ToolTip"));
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            this.toolTip1.SetToolTip(this.label2, resources.GetString("label2.ToolTip"));
            // 
            // richTextBoxDesc
            // 
            this.richTextBoxDesc.AcceptsTab = true;
            resources.ApplyResources(this.richTextBoxDesc, "richTextBoxDesc");
            this.richTextBoxDesc.Name = "richTextBoxDesc";
            this.richTextBoxDesc.ReadOnly = true;
            this.toolTip1.SetToolTip(this.richTextBoxDesc, resources.GetString("richTextBoxDesc.ToolTip"));
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBox1, resources.GetString("pictureBox1.ToolTip"));
            // 
            // listboxPresets
            // 
            resources.ApplyResources(this.listboxPresets, "listboxPresets");
            this.listboxPresets.FormattingEnabled = true;
            this.listboxPresets.Name = "listboxPresets";
            this.toolTip1.SetToolTip(this.listboxPresets, resources.GetString("listboxPresets.ToolTip"));
            this.listboxPresets.SelectedIndexChanged += new System.EventHandler(this.listboxPresets_SelectedIndexChanged);
            // 
            // radioButtonBuiltin
            // 
            resources.ApplyResources(this.radioButtonBuiltin, "radioButtonBuiltin");
            this.radioButtonBuiltin.Checked = true;
            this.radioButtonBuiltin.Name = "radioButtonBuiltin";
            this.radioButtonBuiltin.TabStop = true;
            this.toolTip1.SetToolTip(this.radioButtonBuiltin, resources.GetString("radioButtonBuiltin.ToolTip"));
            this.radioButtonBuiltin.UseVisualStyleBackColor = true;
            this.radioButtonBuiltin.CheckedChanged += new System.EventHandler(this.radioButtonBuiltin_CheckedChanged);
            // 
            // radioButtonCustomCopy
            // 
            resources.ApplyResources(this.radioButtonCustomCopy, "radioButtonCustomCopy");
            this.radioButtonCustomCopy.Name = "radioButtonCustomCopy";
            this.toolTip1.SetToolTip(this.radioButtonCustomCopy, resources.GetString("radioButtonCustomCopy.ToolTip"));
            this.radioButtonCustomCopy.UseVisualStyleBackColor = true;
            this.radioButtonCustomCopy.CheckedChanged += new System.EventHandler(this.RadioButtonCustom_CheckedChanged);
            // 
            // buttonCustomPresetThumbnail
            // 
            resources.ApplyResources(this.buttonCustomPresetThumbnail, "buttonCustomPresetThumbnail");
            this.buttonCustomPresetThumbnail.Name = "buttonCustomPresetThumbnail";
            this.toolTip1.SetToolTip(this.buttonCustomPresetThumbnail, resources.GetString("buttonCustomPresetThumbnail.ToolTip"));
            this.buttonCustomPresetThumbnail.UseVisualStyleBackColor = true;
            this.buttonCustomPresetThumbnail.Click += new System.EventHandler(this.buttonCustomPresetCopyEdit_Click);
            // 
            // radioButtonThumbnail
            // 
            resources.ApplyResources(this.radioButtonThumbnail, "radioButtonThumbnail");
            this.radioButtonThumbnail.Name = "radioButtonThumbnail";
            this.toolTip1.SetToolTip(this.radioButtonThumbnail, resources.GetString("radioButtonThumbnail.ToolTip"));
            this.radioButtonThumbnail.UseVisualStyleBackColor = true;
            this.radioButtonThumbnail.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // labelCodec
            // 
            resources.ApplyResources(this.labelCodec, "labelCodec");
            this.labelCodec.Name = "labelCodec";
            this.toolTip1.SetToolTip(this.labelCodec, resources.GetString("labelCodec.ToolTip"));
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
            // buttonConstrainedCAE
            // 
            resources.ApplyResources(this.buttonConstrainedCAE, "buttonConstrainedCAE");
            this.buttonConstrainedCAE.Name = "buttonConstrainedCAE";
            this.toolTip1.SetToolTip(this.buttonConstrainedCAE, resources.GetString("buttonConstrainedCAE.ToolTip"));
            this.buttonConstrainedCAE.UseVisualStyleBackColor = true;
            this.buttonConstrainedCAE.Click += new System.EventHandler(this.buttonConstrainedCAE_Click);
            // 
            // checkBoxCAEConstrained
            // 
            resources.ApplyResources(this.checkBoxCAEConstrained, "checkBoxCAEConstrained");
            this.checkBoxCAEConstrained.Name = "checkBoxCAEConstrained";
            this.toolTip1.SetToolTip(this.checkBoxCAEConstrained, resources.GetString("checkBoxCAEConstrained.ToolTip"));
            this.checkBoxCAEConstrained.UseVisualStyleBackColor = true;
            this.checkBoxCAEConstrained.CheckedChanged += new System.EventHandler(this.checkBoxCAEConstrained_CheckedChanged);
            // 
            // panelConfigureConstrained
            // 
            resources.ApplyResources(this.panelConfigureConstrained, "panelConfigureConstrained");
            this.panelConfigureConstrained.Controls.Add(this.checkBoxCAEConstrained);
            this.panelConfigureConstrained.Controls.Add(this.buttonConstrainedCAE);
            this.panelConfigureConstrained.Name = "panelConfigureConstrained";
            this.toolTip1.SetToolTip(this.panelConfigureConstrained, resources.GetString("panelConfigureConstrained.ToolTip"));
            // 
            // PresetStandardEncoder
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.panelConfigureConstrained);
            this.Controls.Add(this.labelCodec);
            this.Controls.Add(this.radioButtonThumbnail);
            this.Controls.Add(this.buttonCustomPresetThumbnail);
            this.Controls.Add(this.radioButtonCustomCopy);
            this.Controls.Add(this.radioButtonBuiltin);
            this.Controls.Add(this.listboxPresets);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.richTextBoxDesc);
            this.Controls.Add(this.textBoxDescription);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxTransformName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.moreinfoprofilelink);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labelMES);
            this.Name = "PresetStandardEncoder";
            this.toolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.Load += new System.EventHandler(this.PresetStandardEncoder_Load);
            this.Shown += new System.EventHandler(this.PresetStandardEncoder_Shown);
            this.DpiChanged += new System.Windows.Forms.DpiChangedEventHandler(this.PresetStandardEncoder_DpiChanged);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelConfigureConstrained.ResumeLayout(false);
            this.panelConfigureConstrained.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.Button buttonOk;
        public System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        public System.Windows.Forms.Label labelMES;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.LinkLabel moreinfoprofilelink;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxTransformName;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox richTextBoxDesc;
        private System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.ListBox listboxPresets;
        private System.Windows.Forms.RadioButton radioButtonBuiltin;
        private System.Windows.Forms.RadioButton radioButtonCustomCopy;
        private System.Windows.Forms.Button buttonCustomPresetThumbnail;
        private System.Windows.Forms.RadioButton radioButtonThumbnail;
        private System.Windows.Forms.Label labelCodec;
        private System.Windows.Forms.Button buttonConstrainedCAE;
        private System.Windows.Forms.CheckBox checkBoxCAEConstrained;
        private System.Windows.Forms.Panel panelConfigureConstrained;
    }
}