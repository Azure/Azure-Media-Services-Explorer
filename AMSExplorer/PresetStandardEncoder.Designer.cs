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
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
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
            this.buttonOk.Image = global::AMSExplorer.Bitmaps.index;
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // labelMES
            // 
            resources.ApplyResources(this.labelMES, "labelMES");
            this.labelMES.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(128)))), ((int)(((byte)(171)))));
            this.labelMES.Name = "labelMES";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonOk);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // moreinfoprofilelink
            // 
            resources.ApplyResources(this.moreinfoprofilelink, "moreinfoprofilelink");
            this.moreinfoprofilelink.Name = "moreinfoprofilelink";
            this.moreinfoprofilelink.TabStop = true;
            this.moreinfoprofilelink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.moreinfoprofilelink_LinkClicked);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // textBoxTransformName
            // 
            resources.ApplyResources(this.textBoxTransformName, "textBoxTransformName");
            this.textBoxTransformName.Name = "textBoxTransformName";
            // 
            // textBoxDescription
            // 
            resources.ApplyResources(this.textBoxDescription, "textBoxDescription");
            this.textBoxDescription.Name = "textBoxDescription";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // richTextBoxDesc
            // 
            this.richTextBoxDesc.AcceptsTab = true;
            resources.ApplyResources(this.richTextBoxDesc, "richTextBoxDesc");
            this.richTextBoxDesc.Name = "richTextBoxDesc";
            this.richTextBoxDesc.ReadOnly = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::AMSExplorer.Bitmaps.encoding_large;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // listboxPresets
            // 
            resources.ApplyResources(this.listboxPresets, "listboxPresets");
            this.listboxPresets.FormattingEnabled = true;
            this.listboxPresets.Name = "listboxPresets";
            this.listboxPresets.SelectedIndexChanged += new System.EventHandler(this.listboxPresets_SelectedIndexChanged);
            // 
            // radioButtonBuiltin
            // 
            resources.ApplyResources(this.radioButtonBuiltin, "radioButtonBuiltin");
            this.radioButtonBuiltin.Checked = true;
            this.radioButtonBuiltin.Name = "radioButtonBuiltin";
            this.radioButtonBuiltin.TabStop = true;
            this.radioButtonBuiltin.UseVisualStyleBackColor = true;
            // 
            // radioButtonCustomCopy
            // 
            resources.ApplyResources(this.radioButtonCustomCopy, "radioButtonCustomCopy");
            this.radioButtonCustomCopy.Name = "radioButtonCustomCopy";
            this.radioButtonCustomCopy.UseVisualStyleBackColor = true;
            this.radioButtonCustomCopy.CheckedChanged += new System.EventHandler(this.RadioButtonCustom_CheckedChanged);
            // 
            // buttonCustomPresetThumbnail
            // 
            resources.ApplyResources(this.buttonCustomPresetThumbnail, "buttonCustomPresetThumbnail");
            this.buttonCustomPresetThumbnail.Name = "buttonCustomPresetThumbnail";
            this.buttonCustomPresetThumbnail.UseVisualStyleBackColor = true;
            this.buttonCustomPresetThumbnail.Click += new System.EventHandler(this.buttonCustomPresetCopyEdit_Click);
            // 
            // radioButtonThumbnail
            // 
            resources.ApplyResources(this.radioButtonThumbnail, "radioButtonThumbnail");
            this.radioButtonThumbnail.Name = "radioButtonThumbnail";
            this.radioButtonThumbnail.UseVisualStyleBackColor = true;
            this.radioButtonThumbnail.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
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
            // PresetStandardEncoder
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
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
            this.Load += new System.EventHandler(this.PresetStandardEncoder_Load);
            this.DpiChanged += new System.Windows.Forms.DpiChangedEventHandler(this.PresetStandardEncoder_DpiChanged);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
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
    }
}