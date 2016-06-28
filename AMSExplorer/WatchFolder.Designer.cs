namespace AMSExplorer
{
    partial class WatchFolder
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
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.radioButtonON = new System.Windows.Forms.RadioButton();
            this.radioButtonOFF = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.buttonSelFolder = new System.Windows.Forms.Button();
            this.textBoxFolder = new System.Windows.Forms.TextBox();
            this.checkBoxDeleteFile = new System.Windows.Forms.CheckBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.checkBoxRunJobTemplate = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.checkBoxPublishOAssets = new System.Windows.Forms.CheckBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.checkBoxSendEMail = new System.Windows.Forms.CheckBox();
            this.textBoxEMail = new System.Windows.Forms.TextBox();
            this.buttonTestEmail = new System.Windows.Forms.Button();
            this.radioButtonInsertWorkflowAsset = new System.Windows.Forms.RadioButton();
            this.radioButtonInsertSelectedAssets = new System.Windows.Forms.RadioButton();
            this.groupBoxProcess = new System.Windows.Forms.GroupBox();
            this.labelWarning = new System.Windows.Forms.Label();
            this.panelInsertAsset = new System.Windows.Forms.Panel();
            this.listViewWorkflows1 = new AMSExplorer.ListViewWorkflows();
            this.checkBoAddAssetsToInput = new System.Windows.Forms.CheckBox();
            this.listViewTemplates = new AMSExplorer.ListViewTemplates();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.label13 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBoxOptions = new System.Windows.Forms.GroupBox();
            this.checkBoxProcessXMLRohzet = new System.Windows.Forms.CheckBox();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.groupBoxProcess.SuspendLayout();
            this.panelInsertAsset.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            this.groupBoxOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(570, 15);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(115, 27);
            this.buttonOk.TabIndex = 17;
            this.buttonOk.Text = "Apply";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(693, 14);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(115, 27);
            this.buttonCancel.TabIndex = 16;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label1.Location = new System.Drawing.Point(17, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(693, 45);
            this.label1.TabIndex = 35;
            this.label1.Text = "Specify a watch folder.\r\nAny file copied to this folder will be uploaded to Azure" +
    " Media Services as a new asset and the specified operations will be executed.\r\nY" +
    "ou must keep the application opened.";
            // 
            // radioButtonON
            // 
            this.radioButtonON.AutoSize = true;
            this.radioButtonON.Location = new System.Drawing.Point(41, 33);
            this.radioButtonON.Name = "radioButtonON";
            this.radioButtonON.Size = new System.Drawing.Size(43, 19);
            this.radioButtonON.TabIndex = 44;
            this.radioButtonON.Text = "ON";
            this.radioButtonON.UseVisualStyleBackColor = true;
            // 
            // radioButtonOFF
            // 
            this.radioButtonOFF.AutoSize = true;
            this.radioButtonOFF.Checked = true;
            this.radioButtonOFF.Location = new System.Drawing.Point(41, 61);
            this.radioButtonOFF.Name = "radioButtonOFF";
            this.radioButtonOFF.Size = new System.Drawing.Size(46, 19);
            this.radioButtonOFF.TabIndex = 46;
            this.radioButtonOFF.TabStop = true;
            this.radioButtonOFF.Text = "OFF";
            this.radioButtonOFF.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.pictureBox1);
            this.groupBox4.Controls.Add(this.radioButtonOFF);
            this.groupBox4.Controls.Add(this.radioButtonON);
            this.groupBox4.Location = new System.Drawing.Point(61, 153);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(156, 99);
            this.groupBox4.TabIndex = 43;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Activation";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::AMSExplorer.Bitmaps.watch_folder;
            this.pictureBox1.Location = new System.Drawing.Point(15, 33);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 49;
            this.pictureBox1.TabStop = false;
            // 
            // buttonSelFolder
            // 
            this.buttonSelFolder.Location = new System.Drawing.Point(26, 93);
            this.buttonSelFolder.Name = "buttonSelFolder";
            this.buttonSelFolder.Size = new System.Drawing.Size(132, 27);
            this.buttonSelFolder.TabIndex = 44;
            this.buttonSelFolder.Text = "Select folder...";
            this.buttonSelFolder.UseVisualStyleBackColor = true;
            this.buttonSelFolder.Click += new System.EventHandler(this.buttonSelFolder_Click);
            // 
            // textBoxFolder
            // 
            this.textBoxFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFolder.Enabled = false;
            this.textBoxFolder.Location = new System.Drawing.Point(166, 96);
            this.textBoxFolder.Name = "textBoxFolder";
            this.textBoxFolder.Size = new System.Drawing.Size(636, 23);
            this.textBoxFolder.TabIndex = 45;
            this.textBoxFolder.TextChanged += new System.EventHandler(this.textBoxFolder_TextChanged);
            // 
            // checkBoxDeleteFile
            // 
            this.checkBoxDeleteFile.AutoSize = true;
            this.checkBoxDeleteFile.Location = new System.Drawing.Point(37, 34);
            this.checkBoxDeleteFile.Name = "checkBoxDeleteFile";
            this.checkBoxDeleteFile.Size = new System.Drawing.Size(160, 19);
            this.checkBoxDeleteFile.TabIndex = 49;
            this.checkBoxDeleteFile.Text = "Delete file once uploaded";
            this.checkBoxDeleteFile.UseVisualStyleBackColor = true;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::AMSExplorer.Bitmaps.delete;
            this.pictureBox2.Location = new System.Drawing.Point(15, 34);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(16, 16);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 50;
            this.pictureBox2.TabStop = false;
            // 
            // checkBoxRunJobTemplate
            // 
            this.checkBoxRunJobTemplate.AutoSize = true;
            this.checkBoxRunJobTemplate.Location = new System.Drawing.Point(61, 281);
            this.checkBoxRunJobTemplate.Name = "checkBoxRunJobTemplate";
            this.checkBoxRunJobTemplate.Size = new System.Drawing.Size(115, 19);
            this.checkBoxRunJobTemplate.TabIndex = 51;
            this.checkBoxRunJobTemplate.Text = "Process the asset";
            this.checkBoxRunJobTemplate.UseVisualStyleBackColor = true;
            this.checkBoxRunJobTemplate.CheckedChanged += new System.EventHandler(this.checkBoxRunJobTemplate_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonOk);
            this.panel1.Location = new System.Drawing.Point(-2, 661);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(823, 55);
            this.panel1.TabIndex = 64;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox3.Image = global::AMSExplorer.Bitmaps.streaming_locator;
            this.pictureBox3.Location = new System.Drawing.Point(26, 587);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(16, 16);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox3.TabIndex = 66;
            this.pictureBox3.TabStop = false;
            // 
            // checkBoxPublishOAssets
            // 
            this.checkBoxPublishOAssets.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxPublishOAssets.AutoSize = true;
            this.checkBoxPublishOAssets.Location = new System.Drawing.Point(61, 588);
            this.checkBoxPublishOAssets.Name = "checkBoxPublishOAssets";
            this.checkBoxPublishOAssets.Size = new System.Drawing.Size(355, 19);
            this.checkBoxPublishOAssets.TabIndex = 65;
            this.checkBoxPublishOAssets.Text = "Publish the output asset(s) - using the default value of {0} days";
            this.checkBoxPublishOAssets.UseVisualStyleBackColor = true;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox4.Image = global::AMSExplorer.Bitmaps.create_outlook_report;
            this.pictureBox4.Location = new System.Drawing.Point(26, 620);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(16, 16);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox4.TabIndex = 68;
            this.pictureBox4.TabStop = false;
            // 
            // checkBoxSendEMail
            // 
            this.checkBoxSendEMail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxSendEMail.AutoSize = true;
            this.checkBoxSendEMail.Location = new System.Drawing.Point(61, 621);
            this.checkBoxSendEMail.Name = "checkBoxSendEMail";
            this.checkBoxSendEMail.Size = new System.Drawing.Size(310, 19);
            this.checkBoxSendEMail.TabIndex = 67;
            this.checkBoxSendEMail.Text = "Send an email when completed or in case of error, to :";
            this.checkBoxSendEMail.UseVisualStyleBackColor = true;
            this.checkBoxSendEMail.CheckedChanged += new System.EventHandler(this.checkBoxSendEMail_CheckedChanged);
            // 
            // textBoxEMail
            // 
            this.textBoxEMail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxEMail.Enabled = false;
            this.textBoxEMail.Location = new System.Drawing.Point(394, 617);
            this.textBoxEMail.Name = "textBoxEMail";
            this.textBoxEMail.Size = new System.Drawing.Size(301, 23);
            this.textBoxEMail.TabIndex = 69;
            // 
            // buttonTestEmail
            // 
            this.buttonTestEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonTestEmail.Enabled = false;
            this.buttonTestEmail.Location = new System.Drawing.Point(715, 615);
            this.buttonTestEmail.Name = "buttonTestEmail";
            this.buttonTestEmail.Size = new System.Drawing.Size(87, 27);
            this.buttonTestEmail.TabIndex = 70;
            this.buttonTestEmail.Text = "Test now";
            this.buttonTestEmail.UseVisualStyleBackColor = true;
            this.buttonTestEmail.Click += new System.EventHandler(this.buttonTestEmail_Click);
            // 
            // radioButtonInsertWorkflowAsset
            // 
            this.radioButtonInsertWorkflowAsset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.radioButtonInsertWorkflowAsset.AutoSize = true;
            this.radioButtonInsertWorkflowAsset.Location = new System.Drawing.Point(16, 51);
            this.radioButtonInsertWorkflowAsset.Name = "radioButtonInsertWorkflowAsset";
            this.radioButtonInsertWorkflowAsset.Size = new System.Drawing.Size(144, 34);
            this.radioButtonInsertWorkflowAsset.TabIndex = 72;
            this.radioButtonInsertWorkflowAsset.Text = "Insert a workflow asset\r\nas first asset";
            this.radioButtonInsertWorkflowAsset.UseVisualStyleBackColor = true;
            this.radioButtonInsertWorkflowAsset.CheckedChanged += new System.EventHandler(this.radioButtonInsertWorkflowAsset_CheckedChanged);
            // 
            // radioButtonInsertSelectedAssets
            // 
            this.radioButtonInsertSelectedAssets.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.radioButtonInsertSelectedAssets.AutoSize = true;
            this.radioButtonInsertSelectedAssets.Checked = true;
            this.radioButtonInsertSelectedAssets.Location = new System.Drawing.Point(16, 9);
            this.radioButtonInsertSelectedAssets.Name = "radioButtonInsertSelectedAssets";
            this.radioButtonInsertSelectedAssets.Size = new System.Drawing.Size(161, 34);
            this.radioButtonInsertSelectedAssets.TabIndex = 73;
            this.radioButtonInsertSelectedAssets.TabStop = true;
            this.radioButtonInsertSelectedAssets.Text = "Insert the selected assets\r\nbefore the uploaded asset";
            this.radioButtonInsertSelectedAssets.UseVisualStyleBackColor = true;
            this.radioButtonInsertSelectedAssets.CheckedChanged += new System.EventHandler(this.radioButtonInsertSelectedAssets_CheckedChanged);
            // 
            // groupBoxProcess
            // 
            this.groupBoxProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxProcess.Controls.Add(this.labelWarning);
            this.groupBoxProcess.Controls.Add(this.panelInsertAsset);
            this.groupBoxProcess.Controls.Add(this.checkBoAddAssetsToInput);
            this.groupBoxProcess.Controls.Add(this.listViewTemplates);
            this.groupBoxProcess.Enabled = false;
            this.groupBoxProcess.Location = new System.Drawing.Point(61, 308);
            this.groupBoxProcess.Name = "groupBoxProcess";
            this.groupBoxProcess.Size = new System.Drawing.Size(742, 257);
            this.groupBoxProcess.TabIndex = 74;
            this.groupBoxProcess.TabStop = false;
            this.groupBoxProcess.Text = "Job template and input assets";
            // 
            // labelWarning
            // 
            this.labelWarning.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelWarning.AutoSize = true;
            this.labelWarning.ForeColor = System.Drawing.Color.Red;
            this.labelWarning.Location = new System.Drawing.Point(211, 106);
            this.labelWarning.Name = "labelWarning";
            this.labelWarning.Size = new System.Drawing.Size(52, 15);
            this.labelWarning.TabIndex = 76;
            this.labelWarning.Text = "Warning";
            // 
            // panelInsertAsset
            // 
            this.panelInsertAsset.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelInsertAsset.Controls.Add(this.listViewWorkflows1);
            this.panelInsertAsset.Controls.Add(this.radioButtonInsertSelectedAssets);
            this.panelInsertAsset.Controls.Add(this.radioButtonInsertWorkflowAsset);
            this.panelInsertAsset.Enabled = false;
            this.panelInsertAsset.Location = new System.Drawing.Point(19, 125);
            this.panelInsertAsset.Name = "panelInsertAsset";
            this.panelInsertAsset.Size = new System.Drawing.Size(723, 126);
            this.panelInsertAsset.TabIndex = 75;
            // 
            // listViewWorkflows1
            // 
            this.listViewWorkflows1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewWorkflows1.Enabled = false;
            this.listViewWorkflows1.FullRowSelect = true;
            this.listViewWorkflows1.HideSelection = false;
            this.listViewWorkflows1.Location = new System.Drawing.Point(196, 6);
            this.listViewWorkflows1.MultiSelect = false;
            this.listViewWorkflows1.Name = "listViewWorkflows1";
            this.listViewWorkflows1.Size = new System.Drawing.Size(520, 116);
            this.listViewWorkflows1.TabIndex = 61;
            this.listViewWorkflows1.Tag = -1;
            this.listViewWorkflows1.UseCompatibleStateImageBehavior = false;
            this.listViewWorkflows1.View = System.Windows.Forms.View.Details;
            // 
            // checkBoAddAssetsToInput
            // 
            this.checkBoAddAssetsToInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoAddAssetsToInput.AutoSize = true;
            this.checkBoAddAssetsToInput.Location = new System.Drawing.Point(19, 106);
            this.checkBoAddAssetsToInput.Name = "checkBoAddAssetsToInput";
            this.checkBoAddAssetsToInput.Size = new System.Drawing.Size(164, 19);
            this.checkBoAddAssetsToInput.TabIndex = 74;
            this.checkBoAddAssetsToInput.Text = "Add asset(s) to input asset";
            this.checkBoAddAssetsToInput.UseVisualStyleBackColor = true;
            this.checkBoAddAssetsToInput.CheckedChanged += new System.EventHandler(this.checkBoAddAssetsToInput_CheckedChanged);
            // 
            // listViewTemplates
            // 
            this.listViewTemplates.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewTemplates.FullRowSelect = true;
            this.listViewTemplates.HideSelection = false;
            this.listViewTemplates.Location = new System.Drawing.Point(19, 22);
            this.listViewTemplates.MultiSelect = false;
            this.listViewTemplates.Name = "listViewTemplates";
            this.listViewTemplates.Size = new System.Drawing.Size(716, 74);
            this.listViewTemplates.TabIndex = 61;
            this.listViewTemplates.Tag = -1;
            this.listViewTemplates.UseCompatibleStateImageBehavior = false;
            this.listViewTemplates.View = System.Windows.Forms.View.Details;
            this.listViewTemplates.SelectedIndexChanged += new System.EventHandler(this.listViewTemplates_SelectedIndexChanged);
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = global::AMSExplorer.Bitmaps.encoding;
            this.pictureBox5.Location = new System.Drawing.Point(26, 282);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(16, 16);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox5.TabIndex = 75;
            this.pictureBox5.TabStop = false;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.DarkBlue;
            this.label13.Location = new System.Drawing.Point(16, 9);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(96, 20);
            this.label13.TabIndex = 78;
            this.label13.Text = "Watch Folder";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Location = new System.Drawing.Point(812, 661);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(23, 44);
            this.panel2.TabIndex = 79;
            // 
            // groupBoxOptions
            // 
            this.groupBoxOptions.Controls.Add(this.checkBoxProcessXMLRohzet);
            this.groupBoxOptions.Controls.Add(this.checkBoxDeleteFile);
            this.groupBoxOptions.Controls.Add(this.pictureBox2);
            this.groupBoxOptions.Location = new System.Drawing.Point(245, 153);
            this.groupBoxOptions.Name = "groupBoxOptions";
            this.groupBoxOptions.Size = new System.Drawing.Size(557, 100);
            this.groupBoxOptions.TabIndex = 80;
            this.groupBoxOptions.TabStop = false;
            this.groupBoxOptions.Text = "Upload Options";
            // 
            // checkBoxProcessXMLRohzet
            // 
            this.checkBoxProcessXMLRohzet.AutoSize = true;
            this.checkBoxProcessXMLRohzet.Location = new System.Drawing.Point(37, 60);
            this.checkBoxProcessXMLRohzet.Name = "checkBoxProcessXMLRohzet";
            this.checkBoxProcessXMLRohzet.Size = new System.Drawing.Size(366, 19);
            this.checkBoxProcessXMLRohzet.TabIndex = 51;
            this.checkBoxProcessXMLRohzet.Text = "Process XML semaphone file (Rhozet) for multi files asset upload";
            this.checkBoxProcessXMLRohzet.UseVisualStyleBackColor = true;
            // 
            // WatchFolder
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(833, 717);
            this.Controls.Add(this.groupBoxOptions);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.pictureBox5);
            this.Controls.Add(this.groupBoxProcess);
            this.Controls.Add(this.buttonTestEmail);
            this.Controls.Add(this.textBoxEMail);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.checkBoxRunJobTemplate);
            this.Controls.Add(this.checkBoxSendEMail);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.checkBoxPublishOAssets);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.textBoxFolder);
            this.Controls.Add(this.buttonSelFolder);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "WatchFolder";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Watch Folder";
            this.Load += new System.EventHandler(this.WatchFolder_Load);
            this.Shown += new System.EventHandler(this.WatchFolder_Shown);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.groupBoxProcess.ResumeLayout(false);
            this.groupBoxProcess.PerformLayout();
            this.panelInsertAsset.ResumeLayout(false);
            this.panelInsertAsset.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            this.groupBoxOptions.ResumeLayout(false);
            this.groupBoxOptions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radioButtonON;
        private System.Windows.Forms.RadioButton radioButtonOFF;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button buttonSelFolder;
        private System.Windows.Forms.TextBox textBoxFolder;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox checkBoxDeleteFile;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxRunJobTemplate;
        private ListViewTemplates listViewTemplates;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.CheckBox checkBoxPublishOAssets;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.CheckBox checkBoxSendEMail;
        private System.Windows.Forms.TextBox textBoxEMail;
        private System.Windows.Forms.Button buttonTestEmail;
        private ListViewWorkflows listViewWorkflows1;
        private System.Windows.Forms.RadioButton radioButtonInsertWorkflowAsset;
        private System.Windows.Forms.RadioButton radioButtonInsertSelectedAssets;
        private System.Windows.Forms.GroupBox groupBoxProcess;
        private System.Windows.Forms.CheckBox checkBoAddAssetsToInput;
        private System.Windows.Forms.Panel panelInsertAsset;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.Label labelWarning;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBoxOptions;
        private System.Windows.Forms.CheckBox checkBoxProcessXMLRohzet;
    }
}