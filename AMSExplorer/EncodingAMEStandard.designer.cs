namespace AMSExplorer
{
    partial class EncodingAMEStandard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EncodingAMEStandard));
            this.label3 = new System.Windows.Forms.Label();
            this.textboxoutputassetname = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxJobName = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.listboxPresets = new System.Windows.Forms.ListBox();
            this.buttonSaveXML = new System.Windows.Forms.Button();
            this.buttonLoadXML = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxConfiguration = new System.Windows.Forms.TextBox();
            this.comboBoxProcessor = new System.Windows.Forms.ComboBox();
            this.processorlabel = new System.Windows.Forms.Label();
            this.label = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.openFileDialogPreset = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialogPreset = new System.Windows.Forms.SaveFileDialog();
            this.buttonJobOptions = new AMSExplorer.ButtonJobOptions();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 463);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "Output asset(s) name :";
            // 
            // textboxoutputassetname
            // 
            this.textboxoutputassetname.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textboxoutputassetname.Location = new System.Drawing.Point(20, 479);
            this.textboxoutputassetname.Name = "textboxoutputassetname";
            this.textboxoutputassetname.Size = new System.Drawing.Size(573, 20);
            this.textboxoutputassetname.TabIndex = 21;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 416);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Job(s) name :";
            // 
            // textBoxJobName
            // 
            this.textBoxJobName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxJobName.Location = new System.Drawing.Point(19, 435);
            this.textBoxJobName.Name = "textBoxJobName";
            this.textBoxJobName.Size = new System.Drawing.Size(574, 20);
            this.textBoxJobName.TabIndex = 13;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.listboxPresets);
            this.groupBox1.Controls.Add(this.buttonSaveXML);
            this.groupBox1.Controls.Add(this.buttonLoadXML);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBoxConfiguration);
            this.groupBox1.Location = new System.Drawing.Point(21, 102);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(738, 297);
            this.groupBox1.TabIndex = 29;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Encoder Configuration";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(159, 13);
            this.label4.TabIndex = 44;
            this.label4.Text = "Select a preset or load an XML :";
            // 
            // listboxPresets
            // 
            this.listboxPresets.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listboxPresets.FormattingEnabled = true;
            this.listboxPresets.Location = new System.Drawing.Point(16, 32);
            this.listboxPresets.Name = "listboxPresets";
            this.listboxPresets.Size = new System.Drawing.Size(413, 108);
            this.listboxPresets.TabIndex = 43;
            this.listboxPresets.SelectedIndexChanged += new System.EventHandler(this.listboxPresets_SelectedIndexChanged);
            // 
            // buttonSaveXML
            // 
            this.buttonSaveXML.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSaveXML.Location = new System.Drawing.Point(586, 117);
            this.buttonSaveXML.Name = "buttonSaveXML";
            this.buttonSaveXML.Size = new System.Drawing.Size(135, 23);
            this.buttonSaveXML.TabIndex = 42;
            this.buttonSaveXML.Text = "Save edited XML...";
            this.buttonSaveXML.UseVisualStyleBackColor = true;
            this.buttonSaveXML.Click += new System.EventHandler(this.buttonSaveXML_Click);
            // 
            // buttonLoadXML
            // 
            this.buttonLoadXML.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonLoadXML.Location = new System.Drawing.Point(445, 117);
            this.buttonLoadXML.Name = "buttonLoadXML";
            this.buttonLoadXML.Size = new System.Drawing.Size(135, 23);
            this.buttonLoadXML.TabIndex = 41;
            this.buttonLoadXML.Text = "Load a preset XML file...";
            this.buttonLoadXML.UseVisualStyleBackColor = true;
            this.buttonLoadXML.Click += new System.EventHandler(this.buttonLoadXML_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 146);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 38;
            this.label2.Text = "XML (editable):";
            // 
            // textBoxConfiguration
            // 
            this.textBoxConfiguration.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxConfiguration.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxConfiguration.Location = new System.Drawing.Point(16, 162);
            this.textBoxConfiguration.Multiline = true;
            this.textBoxConfiguration.Name = "textBoxConfiguration";
            this.textBoxConfiguration.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxConfiguration.Size = new System.Drawing.Size(705, 129);
            this.textBoxConfiguration.TabIndex = 27;
            this.textBoxConfiguration.TextChanged += new System.EventHandler(this.textBoxConfiguration_TextChanged);
            // 
            // comboBoxProcessor
            // 
            this.comboBoxProcessor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxProcessor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxProcessor.FormattingEnabled = true;
            this.comboBoxProcessor.Location = new System.Drawing.Point(20, 59);
            this.comboBoxProcessor.Name = "comboBoxProcessor";
            this.comboBoxProcessor.Size = new System.Drawing.Size(739, 21);
            this.comboBoxProcessor.TabIndex = 32;
            // 
            // processorlabel
            // 
            this.processorlabel.Location = new System.Drawing.Point(17, 44);
            this.processorlabel.Name = "processorlabel";
            this.processorlabel.Size = new System.Drawing.Size(63, 22);
            this.processorlabel.TabIndex = 31;
            this.processorlabel.Text = "Processor:";
            // 
            // label
            // 
            this.label.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label.Location = new System.Drawing.Point(18, 11);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(432, 20);
            this.label.TabIndex = 41;
            this.label.Text = "label1";
            // 
            // label34
            // 
            this.label34.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label34.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(128)))), ((int)(((byte)(171)))));
            this.label34.Location = new System.Drawing.Point(456, 11);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(303, 24);
            this.label34.TabIndex = 63;
            this.label34.Text = "Azure Media Encoder Standard";
            this.label34.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(674, 13);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(99, 23);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Image = global::AMSExplorer.Bitmaps.encoding;
            this.buttonOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonOk.Location = new System.Drawing.Point(528, 13);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(140, 23);
            this.buttonOk.TabIndex = 5;
            this.buttonOk.Text = "Launch encoding";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonOk);
            this.panel1.Location = new System.Drawing.Point(-2, 513);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(786, 48);
            this.panel1.TabIndex = 66;
            // 
            // openFileDialogPreset
            // 
            this.openFileDialogPreset.DefaultExt = "xml";
            this.openFileDialogPreset.Filter = "Preset files|*.xml|All files|*.*";
            // 
            // saveFileDialogPreset
            // 
            this.saveFileDialogPreset.DefaultExt = "xml";
            this.saveFileDialogPreset.Filter = "Preset file|*.xml|All files|*.*";
            // 
            // buttonJobOptions
            // 
            this.buttonJobOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonJobOptions.Location = new System.Drawing.Point(622, 432);
            this.buttonJobOptions.Name = "buttonJobOptions";
            this.buttonJobOptions.Size = new System.Drawing.Size(137, 23);
            this.buttonJobOptions.TabIndex = 72;
            this.buttonJobOptions.Text = "Job options...";
            this.buttonJobOptions.UseVisualStyleBackColor = true;
            // 
            // EncodingAMEStandard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.buttonJobOptions);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label34);
            this.Controls.Add(this.label);
            this.Controls.Add(this.comboBoxProcessor);
            this.Controls.Add(this.processorlabel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textboxoutputassetname);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxJobName);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EncodingAMEStandard";
            this.Text = "Media Encoder Standard";
            this.Load += new System.EventHandler(this.EncodingAMEStandard_Load);
            this.Shown += new System.EventHandler(this.EncodingAMEStandard_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxConfiguration;
        public System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxProcessor;
        private System.Windows.Forms.Label processorlabel;
        private System.Windows.Forms.TextBox textboxoutputassetname;
        private System.Windows.Forms.TextBox textBoxJobName;
        public System.Windows.Forms.Label label;
        public System.Windows.Forms.Label label34;
        public System.Windows.Forms.Button buttonCancel;
        public System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Panel panel1;
        private ButtonJobOptions buttonJobOptions;
        private System.Windows.Forms.Button buttonSaveXML;
        private System.Windows.Forms.Button buttonLoadXML;
        private System.Windows.Forms.OpenFileDialog openFileDialogPreset;
        private System.Windows.Forms.SaveFileDialog saveFileDialogPreset;
        public System.Windows.Forms.ListBox listboxPresets;
        public System.Windows.Forms.Label label4;
    }
}