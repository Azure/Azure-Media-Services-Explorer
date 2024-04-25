namespace AMSExplorer
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PresetStandardEncoder));
            buttonCancel = new System.Windows.Forms.Button();
            buttonOk = new System.Windows.Forms.Button();
            labelMES = new System.Windows.Forms.Label();
            panel1 = new System.Windows.Forms.Panel();
            toolTip1 = new System.Windows.Forms.ToolTip(components);
            moreinfoprofilelink = new System.Windows.Forms.LinkLabel();
            label1 = new System.Windows.Forms.Label();
            textBoxTransformName = new System.Windows.Forms.TextBox();
            textBoxDescription = new System.Windows.Forms.TextBox();
            label2 = new System.Windows.Forms.Label();
            richTextBoxDesc = new System.Windows.Forms.RichTextBox();
            pictureBox1 = new System.Windows.Forms.PictureBox();
            listboxPresets = new System.Windows.Forms.ListBox();
            radioButtonBuiltin = new System.Windows.Forms.RadioButton();
            radioButtonCustomCopy = new System.Windows.Forms.RadioButton();
            buttonCustomPresetThumbnail = new System.Windows.Forms.Button();
            radioButtonThumbnail = new System.Windows.Forms.RadioButton();
            labelCodec = new System.Windows.Forms.Label();
            buttonConstrainedCAE = new System.Windows.Forms.Button();
            checkBoxCAEConstrained = new System.Windows.Forms.CheckBox();
            panelConfigureConstrained = new System.Windows.Forms.Panel();
            dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panelConfigureConstrained.SuspendLayout();
            SuspendLayout();
            // 
            // buttonCancel
            // 
            resources.ApplyResources(buttonCancel, "buttonCancel");
            buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            buttonCancel.Name = "buttonCancel";
            buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOk
            // 
            resources.ApplyResources(buttonOk, "buttonOk");
            buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            buttonOk.Name = "buttonOk";
            buttonOk.UseVisualStyleBackColor = true;
            // 
            // labelMES
            // 
            resources.ApplyResources(labelMES, "labelMES");
            labelMES.ForeColor = System.Drawing.Color.FromArgb(46, 128, 171);
            labelMES.Name = "labelMES";
            // 
            // panel1
            // 
            panel1.BackColor = System.Drawing.SystemColors.Control;
            panel1.Controls.Add(buttonCancel);
            panel1.Controls.Add(buttonOk);
            resources.ApplyResources(panel1, "panel1");
            panel1.Name = "panel1";
            // 
            // moreinfoprofilelink
            // 
            resources.ApplyResources(moreinfoprofilelink, "moreinfoprofilelink");
            moreinfoprofilelink.Name = "moreinfoprofilelink";
            moreinfoprofilelink.TabStop = true;
            moreinfoprofilelink.LinkClicked += moreinfoprofilelink_LinkClicked;
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // textBoxTransformName
            // 
            resources.ApplyResources(textBoxTransformName, "textBoxTransformName");
            textBoxTransformName.Name = "textBoxTransformName";
            // 
            // textBoxDescription
            // 
            resources.ApplyResources(textBoxDescription, "textBoxDescription");
            textBoxDescription.Name = "textBoxDescription";
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            // 
            // richTextBoxDesc
            // 
            richTextBoxDesc.AcceptsTab = true;
            resources.ApplyResources(richTextBoxDesc, "richTextBoxDesc");
            richTextBoxDesc.Name = "richTextBoxDesc";
            richTextBoxDesc.ReadOnly = true;
            // 
            // pictureBox1
            // 
            resources.ApplyResources(pictureBox1, "pictureBox1");
            pictureBox1.Name = "pictureBox1";
            pictureBox1.TabStop = false;
            // 
            // listboxPresets
            // 
            resources.ApplyResources(listboxPresets, "listboxPresets");
            listboxPresets.FormattingEnabled = true;
            listboxPresets.Name = "listboxPresets";
            listboxPresets.SelectedIndexChanged += listboxPresets_SelectedIndexChanged;
            // 
            // radioButtonBuiltin
            // 
            resources.ApplyResources(radioButtonBuiltin, "radioButtonBuiltin");
            radioButtonBuiltin.Checked = true;
            radioButtonBuiltin.Name = "radioButtonBuiltin";
            radioButtonBuiltin.TabStop = true;
            radioButtonBuiltin.UseVisualStyleBackColor = true;
            radioButtonBuiltin.CheckedChanged += radioButtonBuiltin_CheckedChanged;
            // 
            // radioButtonCustomCopy
            // 
            resources.ApplyResources(radioButtonCustomCopy, "radioButtonCustomCopy");
            radioButtonCustomCopy.Name = "radioButtonCustomCopy";
            radioButtonCustomCopy.UseVisualStyleBackColor = true;
            radioButtonCustomCopy.CheckedChanged += RadioButtonCustom_CheckedChanged;
            // 
            // buttonCustomPresetThumbnail
            // 
            resources.ApplyResources(buttonCustomPresetThumbnail, "buttonCustomPresetThumbnail");
            buttonCustomPresetThumbnail.Name = "buttonCustomPresetThumbnail";
            buttonCustomPresetThumbnail.UseVisualStyleBackColor = true;
            buttonCustomPresetThumbnail.Click += buttonCustomPresetCopyEdit_Click;
            // 
            // radioButtonThumbnail
            // 
            resources.ApplyResources(radioButtonThumbnail, "radioButtonThumbnail");
            radioButtonThumbnail.Name = "radioButtonThumbnail";
            radioButtonThumbnail.UseVisualStyleBackColor = true;
            radioButtonThumbnail.CheckedChanged += radioButton1_CheckedChanged;
            // 
            // labelCodec
            // 
            resources.ApplyResources(labelCodec, "labelCodec");
            labelCodec.Name = "labelCodec";
            // 
            // buttonConstrainedCAE
            // 
            resources.ApplyResources(buttonConstrainedCAE, "buttonConstrainedCAE");
            buttonConstrainedCAE.Name = "buttonConstrainedCAE";
            buttonConstrainedCAE.UseVisualStyleBackColor = true;
            buttonConstrainedCAE.Click += buttonConstrainedCAE_Click;
            // 
            // checkBoxCAEConstrained
            // 
            resources.ApplyResources(checkBoxCAEConstrained, "checkBoxCAEConstrained");
            checkBoxCAEConstrained.Name = "checkBoxCAEConstrained";
            checkBoxCAEConstrained.UseVisualStyleBackColor = true;
            checkBoxCAEConstrained.CheckedChanged += checkBoxCAEConstrained_CheckedChanged;
            // 
            // panelConfigureConstrained
            // 
            panelConfigureConstrained.Controls.Add(checkBoxCAEConstrained);
            panelConfigureConstrained.Controls.Add(buttonConstrainedCAE);
            resources.ApplyResources(panelConfigureConstrained, "panelConfigureConstrained");
            panelConfigureConstrained.Name = "panelConfigureConstrained";
            // 
            // dataGridViewTextBoxColumn1
            // 
            resources.ApplyResources(dataGridViewTextBoxColumn1, "dataGridViewTextBoxColumn1");
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            resources.ApplyResources(dataGridViewTextBoxColumn2, "dataGridViewTextBoxColumn2");
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // PresetStandardEncoder
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            BackColor = System.Drawing.SystemColors.Window;
            CancelButton = buttonCancel;
            Controls.Add(panelConfigureConstrained);
            Controls.Add(labelCodec);
            Controls.Add(radioButtonThumbnail);
            Controls.Add(buttonCustomPresetThumbnail);
            Controls.Add(radioButtonCustomCopy);
            Controls.Add(radioButtonBuiltin);
            Controls.Add(listboxPresets);
            Controls.Add(pictureBox1);
            Controls.Add(richTextBoxDesc);
            Controls.Add(textBoxDescription);
            Controls.Add(label2);
            Controls.Add(textBoxTransformName);
            Controls.Add(label1);
            Controls.Add(moreinfoprofilelink);
            Controls.Add(panel1);
            Controls.Add(labelMES);
            Name = "PresetStandardEncoder";
            Load += PresetStandardEncoder_Load;
            Shown += PresetStandardEncoder_Shown;
            DpiChanged += PresetStandardEncoder_DpiChanged;
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panelConfigureConstrained.ResumeLayout(false);
            panelConfigureConstrained.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
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