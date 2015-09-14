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
            this.labelWarningJSON = new System.Windows.Forms.Label();
            this.moreinfopresetslink = new System.Windows.Forms.LinkLabel();
            this.richTextBoxDesc = new System.Windows.Forms.RichTextBox();
            this.label4KWarning = new System.Windows.Forms.Label();
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
            this.moreinfoame = new System.Windows.Forms.LinkLabel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.Preset = new System.Windows.Forms.TabPage();
            this.Advanced = new System.Windows.Forms.TabPage();
            this.checkBoxAddAutomatic = new System.Windows.Forms.CheckBox();
            this.groupBoxTrim = new System.Windows.Forms.GroupBox();
            this.textBoxSourceDurationTime = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.timeControlEndTime = new AMSExplorer.TimeControl();
            this.timeControlStartTime = new AMSExplorer.TimeControl();
            this.checkBoxSourceTrimming = new System.Windows.Forms.CheckBox();
            this.buttonJobOptions = new AMSExplorer.ButtonJobOptions();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.Preset.SuspendLayout();
            this.Advanced.SuspendLayout();
            this.groupBoxTrim.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 557);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(126, 15);
            this.label3.TabIndex = 22;
            this.label3.Text = "Output asset(s) name :";
            // 
            // textboxoutputassetname
            // 
            this.textboxoutputassetname.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textboxoutputassetname.Location = new System.Drawing.Point(27, 575);
            this.textboxoutputassetname.Name = "textboxoutputassetname";
            this.textboxoutputassetname.Size = new System.Drawing.Size(521, 23);
            this.textboxoutputassetname.TabIndex = 21;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 509);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 15);
            this.label1.TabIndex = 18;
            this.label1.Text = "Job(s) name :";
            // 
            // textBoxJobName
            // 
            this.textBoxJobName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxJobName.Location = new System.Drawing.Point(28, 527);
            this.textBoxJobName.Name = "textBoxJobName";
            this.textBoxJobName.Size = new System.Drawing.Size(522, 23);
            this.textBoxJobName.TabIndex = 13;
            // 
            // labelWarningJSON
            // 
            this.labelWarningJSON.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelWarningJSON.ForeColor = System.Drawing.Color.Red;
            this.labelWarningJSON.Location = new System.Drawing.Point(175, 157);
            this.labelWarningJSON.Name = "labelWarningJSON";
            this.labelWarningJSON.Size = new System.Drawing.Size(530, 21);
            this.labelWarningJSON.TabIndex = 77;
            this.labelWarningJSON.Tag = "JSON Syntax error. {0}";
            this.labelWarningJSON.Text = "JSON Syntax error. {0}";
            this.labelWarningJSON.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelWarningJSON.Visible = false;
            // 
            // moreinfopresetslink
            // 
            this.moreinfopresetslink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.moreinfopresetslink.AutoSize = true;
            this.moreinfopresetslink.Location = new System.Drawing.Point(547, 84);
            this.moreinfopresetslink.Name = "moreinfopresetslink";
            this.moreinfopresetslink.Size = new System.Drawing.Size(158, 15);
            this.moreinfopresetslink.TabIndex = 76;
            this.moreinfopresetslink.TabStop = true;
            this.moreinfopresetslink.Text = "More information on presets";
            this.moreinfopresetslink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.moreinfopresetslink_LinkClicked);
            // 
            // richTextBoxDesc
            // 
            this.richTextBoxDesc.AcceptsTab = true;
            this.richTextBoxDesc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxDesc.Location = new System.Drawing.Point(318, 29);
            this.richTextBoxDesc.Name = "richTextBoxDesc";
            this.richTextBoxDesc.ReadOnly = true;
            this.richTextBoxDesc.Size = new System.Drawing.Size(387, 52);
            this.richTextBoxDesc.TabIndex = 75;
            this.richTextBoxDesc.Text = "";
            // 
            // label4KWarning
            // 
            this.label4KWarning.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4KWarning.ForeColor = System.Drawing.Color.Red;
            this.label4KWarning.Location = new System.Drawing.Point(318, 99);
            this.label4KWarning.Name = "label4KWarning";
            this.label4KWarning.Size = new System.Drawing.Size(387, 23);
            this.label4KWarning.TabIndex = 73;
            this.label4KWarning.Tag = "Warning : you should use a Premium Encoding RU for 4K";
            this.label4KWarning.Text = "Warning : you should use a Premium Encoding RU for 4K";
            this.label4KWarning.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(187, 15);
            this.label4.TabIndex = 44;
            this.label4.Text = "Select a preset or load a JSON file :";
            // 
            // listboxPresets
            // 
            this.listboxPresets.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listboxPresets.FormattingEnabled = true;
            this.listboxPresets.ItemHeight = 15;
            this.listboxPresets.Location = new System.Drawing.Point(14, 29);
            this.listboxPresets.Name = "listboxPresets";
            this.listboxPresets.Size = new System.Drawing.Size(285, 109);
            this.listboxPresets.TabIndex = 43;
            this.listboxPresets.SelectedIndexChanged += new System.EventHandler(this.listboxPresets_SelectedIndexChanged);
            // 
            // buttonSaveXML
            // 
            this.buttonSaveXML.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSaveXML.Location = new System.Drawing.Point(548, 127);
            this.buttonSaveXML.Name = "buttonSaveXML";
            this.buttonSaveXML.Size = new System.Drawing.Size(157, 27);
            this.buttonSaveXML.TabIndex = 42;
            this.buttonSaveXML.Text = "Save edited JSON...";
            this.buttonSaveXML.UseVisualStyleBackColor = true;
            this.buttonSaveXML.Click += new System.EventHandler(this.buttonSaveXML_Click);
            // 
            // buttonLoadXML
            // 
            this.buttonLoadXML.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonLoadXML.Location = new System.Drawing.Point(385, 127);
            this.buttonLoadXML.Name = "buttonLoadXML";
            this.buttonLoadXML.Size = new System.Drawing.Size(157, 27);
            this.buttonLoadXML.TabIndex = 41;
            this.buttonLoadXML.Text = "Load a preset JSON file...";
            this.buttonLoadXML.UseVisualStyleBackColor = true;
            this.buttonLoadXML.Click += new System.EventHandler(this.buttonLoadJSON_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 160);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 15);
            this.label2.TabIndex = 38;
            this.label2.Text = "JSON (editable):";
            // 
            // textBoxConfiguration
            // 
            this.textBoxConfiguration.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxConfiguration.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxConfiguration.Location = new System.Drawing.Point(14, 179);
            this.textBoxConfiguration.Multiline = true;
            this.textBoxConfiguration.Name = "textBoxConfiguration";
            this.textBoxConfiguration.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxConfiguration.Size = new System.Drawing.Size(691, 138);
            this.textBoxConfiguration.TabIndex = 27;
            this.textBoxConfiguration.TextChanged += new System.EventHandler(this.textBoxConfiguration_TextChanged);
            // 
            // comboBoxProcessor
            // 
            this.comboBoxProcessor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxProcessor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxProcessor.FormattingEnabled = true;
            this.comboBoxProcessor.Location = new System.Drawing.Point(23, 68);
            this.comboBoxProcessor.Name = "comboBoxProcessor";
            this.comboBoxProcessor.Size = new System.Drawing.Size(733, 23);
            this.comboBoxProcessor.TabIndex = 32;
            // 
            // processorlabel
            // 
            this.processorlabel.Location = new System.Drawing.Point(20, 51);
            this.processorlabel.Name = "processorlabel";
            this.processorlabel.Size = new System.Drawing.Size(73, 25);
            this.processorlabel.TabIndex = 31;
            this.processorlabel.Text = "Processor:";
            // 
            // label
            // 
            this.label.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label.Location = new System.Drawing.Point(20, 21);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(498, 23);
            this.label.TabIndex = 41;
            this.label.Text = "label1";
            // 
            // label34
            // 
            this.label34.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.label34.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(128)))), ((int)(((byte)(171)))));
            this.label34.Location = new System.Drawing.Point(524, 13);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(232, 25);
            this.label34.TabIndex = 63;
            this.label34.Text = "Media Encoder Standard";
            this.label34.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(656, 15);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(115, 27);
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
            this.buttonOk.Location = new System.Drawing.Point(487, 15);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(163, 27);
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
            this.panel1.Location = new System.Drawing.Point(1, 606);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(783, 55);
            this.panel1.TabIndex = 66;
            // 
            // openFileDialogPreset
            // 
            this.openFileDialogPreset.DefaultExt = "xml";
            this.openFileDialogPreset.Filter = "Preset files|*.json|All files|*.*";
            // 
            // saveFileDialogPreset
            // 
            this.saveFileDialogPreset.DefaultExt = "xml";
            this.saveFileDialogPreset.Filter = "Preset file|*.json|All files|*.*";
            // 
            // moreinfoame
            // 
            this.moreinfoame.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.moreinfoame.AutoSize = true;
            this.moreinfoame.Location = new System.Drawing.Point(655, 38);
            this.moreinfoame.Name = "moreinfoame";
            this.moreinfoame.Size = new System.Drawing.Size(101, 15);
            this.moreinfoame.TabIndex = 73;
            this.moreinfoame.TabStop = true;
            this.moreinfoame.Text = "More information";
            this.moreinfoame.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.moreinfoame_LinkClicked);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.Preset);
            this.tabControl1.Controls.Add(this.Advanced);
            this.tabControl1.Location = new System.Drawing.Point(24, 111);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(732, 389);
            this.tabControl1.TabIndex = 74;
            // 
            // Preset
            // 
            this.Preset.Controls.Add(this.labelWarningJSON);
            this.Preset.Controls.Add(this.label4);
            this.Preset.Controls.Add(this.moreinfopresetslink);
            this.Preset.Controls.Add(this.textBoxConfiguration);
            this.Preset.Controls.Add(this.richTextBoxDesc);
            this.Preset.Controls.Add(this.label2);
            this.Preset.Controls.Add(this.label4KWarning);
            this.Preset.Controls.Add(this.buttonLoadXML);
            this.Preset.Controls.Add(this.buttonSaveXML);
            this.Preset.Controls.Add(this.listboxPresets);
            this.Preset.Location = new System.Drawing.Point(4, 24);
            this.Preset.Name = "Preset";
            this.Preset.Padding = new System.Windows.Forms.Padding(3);
            this.Preset.Size = new System.Drawing.Size(724, 361);
            this.Preset.TabIndex = 0;
            this.Preset.Text = "Preset";
            this.Preset.UseVisualStyleBackColor = true;
            // 
            // Advanced
            // 
            this.Advanced.Controls.Add(this.checkBoxAddAutomatic);
            this.Advanced.Controls.Add(this.groupBoxTrim);
            this.Advanced.Location = new System.Drawing.Point(4, 24);
            this.Advanced.Name = "Advanced";
            this.Advanced.Padding = new System.Windows.Forms.Padding(3);
            this.Advanced.Size = new System.Drawing.Size(724, 361);
            this.Advanced.TabIndex = 1;
            this.Advanced.Text = "Advanced";
            this.Advanced.UseVisualStyleBackColor = true;
            // 
            // checkBoxAddAutomatic
            // 
            this.checkBoxAddAutomatic.AutoSize = true;
            this.checkBoxAddAutomatic.Checked = true;
            this.checkBoxAddAutomatic.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAddAutomatic.Location = new System.Drawing.Point(16, 13);
            this.checkBoxAddAutomatic.Name = "checkBoxAddAutomatic";
            this.checkBoxAddAutomatic.Size = new System.Drawing.Size(266, 19);
            this.checkBoxAddAutomatic.TabIndex = 4;
            this.checkBoxAddAutomatic.Text = "Automatically update the JSON configuration";
            this.checkBoxAddAutomatic.UseVisualStyleBackColor = true;
            this.checkBoxAddAutomatic.CheckedChanged += new System.EventHandler(this.checkBoxAddAutomatic_CheckedChanged);
            // 
            // groupBoxTrim
            // 
            this.groupBoxTrim.Controls.Add(this.textBoxSourceDurationTime);
            this.groupBoxTrim.Controls.Add(this.label7);
            this.groupBoxTrim.Controls.Add(this.timeControlEndTime);
            this.groupBoxTrim.Controls.Add(this.timeControlStartTime);
            this.groupBoxTrim.Controls.Add(this.checkBoxSourceTrimming);
            this.groupBoxTrim.Location = new System.Drawing.Point(16, 38);
            this.groupBoxTrim.Name = "groupBoxTrim";
            this.groupBoxTrim.Size = new System.Drawing.Size(672, 169);
            this.groupBoxTrim.TabIndex = 3;
            this.groupBoxTrim.TabStop = false;
            // 
            // textBoxSourceDurationTime
            // 
            this.textBoxSourceDurationTime.Enabled = false;
            this.textBoxSourceDurationTime.Location = new System.Drawing.Point(517, 123);
            this.textBoxSourceDurationTime.Name = "textBoxSourceDurationTime";
            this.textBoxSourceDurationTime.ReadOnly = true;
            this.textBoxSourceDurationTime.Size = new System.Drawing.Size(123, 23);
            this.textBoxSourceDurationTime.TabIndex = 88;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(514, 105);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 15);
            this.label7.TabIndex = 82;
            this.label7.Text = "Duration :";
            // 
            // timeControlEndTime
            // 
            this.timeControlEndTime.BackColor = System.Drawing.SystemColors.Window;
            this.timeControlEndTime.DisplayTrackBar = false;
            this.timeControlEndTime.Enabled = false;
            this.timeControlEndTime.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeControlEndTime.Label1 = "";
            this.timeControlEndTime.Label2 = "End time";
            this.timeControlEndTime.Location = new System.Drawing.Point(44, 105);
            this.timeControlEndTime.Max = System.TimeSpan.Parse("10675199.02:48:05.4775807");
            this.timeControlEndTime.Min = System.TimeSpan.Parse("00:00:00");
            this.timeControlEndTime.Name = "timeControlEndTime";
            this.timeControlEndTime.ScaledFirstTimestampOffset = ((ulong)(0ul));
            this.timeControlEndTime.Size = new System.Drawing.Size(441, 58);
            this.timeControlEndTime.TabIndex = 7;
            this.timeControlEndTime.TimeScale = null;
            this.timeControlEndTime.TotalDuration = System.TimeSpan.Parse("1.00:00:00");
            this.timeControlEndTime.ValueChanged += new System.EventHandler(this.timeControlDuration_ValueChanged);
            // 
            // timeControlStartTime
            // 
            this.timeControlStartTime.BackColor = System.Drawing.SystemColors.Window;
            this.timeControlStartTime.DisplayTrackBar = false;
            this.timeControlStartTime.Enabled = false;
            this.timeControlStartTime.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeControlStartTime.Label1 = "";
            this.timeControlStartTime.Label2 = "Start time";
            this.timeControlStartTime.Location = new System.Drawing.Point(44, 47);
            this.timeControlStartTime.Max = System.TimeSpan.Parse("10675199.02:48:05.4775807");
            this.timeControlStartTime.Min = System.TimeSpan.Parse("00:00:00");
            this.timeControlStartTime.Name = "timeControlStartTime";
            this.timeControlStartTime.ScaledFirstTimestampOffset = ((ulong)(0ul));
            this.timeControlStartTime.Size = new System.Drawing.Size(441, 58);
            this.timeControlStartTime.TabIndex = 6;
            this.timeControlStartTime.TimeScale = null;
            this.timeControlStartTime.TotalDuration = System.TimeSpan.Parse("1.00:00:00");
            this.timeControlStartTime.ValueChanged += new System.EventHandler(this.timeControlStartTime_ValueChanged);
            // 
            // checkBoxSourceTrimming
            // 
            this.checkBoxSourceTrimming.AutoSize = true;
            this.checkBoxSourceTrimming.Location = new System.Drawing.Point(16, 22);
            this.checkBoxSourceTrimming.Name = "checkBoxSourceTrimming";
            this.checkBoxSourceTrimming.Size = new System.Drawing.Size(117, 19);
            this.checkBoxSourceTrimming.TabIndex = 5;
            this.checkBoxSourceTrimming.Text = "Source Trimming";
            this.checkBoxSourceTrimming.UseVisualStyleBackColor = true;
            this.checkBoxSourceTrimming.CheckedChanged += new System.EventHandler(this.checkBoxSourceTrimming_CheckedChanged);
            // 
            // buttonJobOptions
            // 
            this.buttonJobOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonJobOptions.Location = new System.Drawing.Point(596, 527);
            this.buttonJobOptions.Name = "buttonJobOptions";
            this.buttonJobOptions.Size = new System.Drawing.Size(160, 27);
            this.buttonJobOptions.TabIndex = 75;
            this.buttonJobOptions.Text = "Job options...";
            this.buttonJobOptions.UseVisualStyleBackColor = true;
            // 
            // EncodingAMEStandard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(784, 661);
            this.Controls.Add(this.buttonJobOptions);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textboxoutputassetname);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxJobName);
            this.Controls.Add(this.comboBoxProcessor);
            this.Controls.Add(this.processorlabel);
            this.Controls.Add(this.label);
            this.Controls.Add(this.label34);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.moreinfoame);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EncodingAMEStandard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Media Encoder Standard";
            this.Load += new System.EventHandler(this.EncodingAMEStandard_Load);
            this.Shown += new System.EventHandler(this.EncodingAMEStandard_Shown);
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.Preset.ResumeLayout(false);
            this.Preset.PerformLayout();
            this.Advanced.ResumeLayout(false);
            this.Advanced.PerformLayout();
            this.groupBoxTrim.ResumeLayout(false);
            this.groupBoxTrim.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label label1;
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
        private System.Windows.Forms.Button buttonSaveXML;
        private System.Windows.Forms.Button buttonLoadXML;
        private System.Windows.Forms.OpenFileDialog openFileDialogPreset;
        private System.Windows.Forms.SaveFileDialog saveFileDialogPreset;
        public System.Windows.Forms.ListBox listboxPresets;
        public System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label4KWarning;
        private System.Windows.Forms.LinkLabel moreinfoame;
        private System.Windows.Forms.RichTextBox richTextBoxDesc;
        private System.Windows.Forms.LinkLabel moreinfopresetslink;
        private System.Windows.Forms.Label labelWarningJSON;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage Preset;
        private System.Windows.Forms.TabPage Advanced;
        private System.Windows.Forms.GroupBox groupBoxTrim;
        private System.Windows.Forms.CheckBox checkBoxAddAutomatic;
        private System.Windows.Forms.CheckBox checkBoxSourceTrimming;
        private TimeControl timeControlEndTime;
        private TimeControl timeControlStartTime;
        private ButtonJobOptions buttonJobOptions;
        public System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxSourceDurationTime;
    }
}