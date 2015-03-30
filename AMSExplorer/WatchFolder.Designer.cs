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
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.buttonSelFolder = new System.Windows.Forms.Button();
            this.textBoxFolder = new System.Windows.Forms.TextBox();
            this.checkBoxUseQueue = new System.Windows.Forms.CheckBox();
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
            this.panelInsertAsset = new System.Windows.Forms.Panel();
            this.checkBoAddAssetsToInput = new System.Windows.Forms.CheckBox();
            this.listViewWorkflows1 = new AMSExplorer.ListViewWorkflows();
            this.listViewTemplates = new AMSExplorer.ListViewTemplates();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.groupBoxProcess.SuspendLayout();
            this.panelInsertAsset.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(500, 13);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(99, 23);
            this.buttonOk.TabIndex = 17;
            this.buttonOk.Text = "Apply";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(605, 12);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(99, 23);
            this.buttonCancel.TabIndex = 16;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(625, 39);
            this.label1.TabIndex = 35;
            this.label1.Text = "Specify a watch folder.\r\nAny file copied to this folder will be uploaded to Azure" +
    " Media Services as a new asset and the specified operations will be executed.\r\nY" +
    "ou must keep the application opened.";
            // 
            // radioButtonON
            // 
            this.radioButtonON.AutoSize = true;
            this.radioButtonON.Location = new System.Drawing.Point(35, 29);
            this.radioButtonON.Name = "radioButtonON";
            this.radioButtonON.Size = new System.Drawing.Size(41, 17);
            this.radioButtonON.TabIndex = 44;
            this.radioButtonON.Text = "ON";
            this.radioButtonON.UseVisualStyleBackColor = true;
            // 
            // radioButtonOFF
            // 
            this.radioButtonOFF.AutoSize = true;
            this.radioButtonOFF.Checked = true;
            this.radioButtonOFF.Location = new System.Drawing.Point(35, 53);
            this.radioButtonOFF.Name = "radioButtonOFF";
            this.radioButtonOFF.Size = new System.Drawing.Size(45, 17);
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
            this.groupBox4.Location = new System.Drawing.Point(17, 124);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(671, 86);
            this.groupBox4.TabIndex = 43;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Activation";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::AMSExplorer.Bitmaps.watch_folder;
            this.pictureBox1.Location = new System.Drawing.Point(13, 29);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 49;
            this.pictureBox1.TabStop = false;
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // buttonSelFolder
            // 
            this.buttonSelFolder.Location = new System.Drawing.Point(22, 81);
            this.buttonSelFolder.Name = "buttonSelFolder";
            this.buttonSelFolder.Size = new System.Drawing.Size(113, 23);
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
            this.textBoxFolder.Location = new System.Drawing.Point(142, 83);
            this.textBoxFolder.Name = "textBoxFolder";
            this.textBoxFolder.Size = new System.Drawing.Size(546, 20);
            this.textBoxFolder.TabIndex = 45;
            this.textBoxFolder.TextChanged += new System.EventHandler(this.textBoxFolder_TextChanged);
            // 
            // checkBoxUseQueue
            // 
            this.checkBoxUseQueue.AutoSize = true;
            this.checkBoxUseQueue.Location = new System.Drawing.Point(52, 216);
            this.checkBoxUseQueue.Name = "checkBoxUseQueue";
            this.checkBoxUseQueue.Size = new System.Drawing.Size(192, 17);
            this.checkBoxUseQueue.TabIndex = 48;
            this.checkBoxUseQueue.Text = "One upload at a time (use a queue)";
            this.checkBoxUseQueue.UseVisualStyleBackColor = true;
            this.checkBoxUseQueue.CheckedChanged += new System.EventHandler(this.checkBoxParallel_CheckedChanged);
            // 
            // checkBoxDeleteFile
            // 
            this.checkBoxDeleteFile.AutoSize = true;
            this.checkBoxDeleteFile.Location = new System.Drawing.Point(52, 239);
            this.checkBoxDeleteFile.Name = "checkBoxDeleteFile";
            this.checkBoxDeleteFile.Size = new System.Drawing.Size(147, 17);
            this.checkBoxDeleteFile.TabIndex = 49;
            this.checkBoxDeleteFile.Text = "Delete file once uploaded";
            this.checkBoxDeleteFile.UseVisualStyleBackColor = true;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::AMSExplorer.Bitmaps.delete;
            this.pictureBox2.Location = new System.Drawing.Point(22, 239);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(16, 16);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 50;
            this.pictureBox2.TabStop = false;
            // 
            // checkBoxRunJobTemplate
            // 
            this.checkBoxRunJobTemplate.AutoSize = true;
            this.checkBoxRunJobTemplate.Location = new System.Drawing.Point(52, 262);
            this.checkBoxRunJobTemplate.Name = "checkBoxRunJobTemplate";
            this.checkBoxRunJobTemplate.Size = new System.Drawing.Size(110, 17);
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
            this.panel1.Location = new System.Drawing.Point(-2, 573);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(717, 48);
            this.panel1.TabIndex = 64;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox3.Image = global::AMSExplorer.Bitmaps.streaming_locator;
            this.pictureBox3.Location = new System.Drawing.Point(22, 514);
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
            this.checkBoxPublishOAssets.Location = new System.Drawing.Point(52, 514);
            this.checkBoxPublishOAssets.Name = "checkBoxPublishOAssets";
            this.checkBoxPublishOAssets.Size = new System.Drawing.Size(320, 17);
            this.checkBoxPublishOAssets.TabIndex = 65;
            this.checkBoxPublishOAssets.Text = "Publish the output asset(s) - using the default value of {0} days";
            this.checkBoxPublishOAssets.UseVisualStyleBackColor = true;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox4.Image = global::AMSExplorer.Bitmaps.delete;
            this.pictureBox4.Location = new System.Drawing.Point(22, 537);
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
            this.checkBoxSendEMail.Location = new System.Drawing.Point(52, 537);
            this.checkBoxSendEMail.Name = "checkBoxSendEMail";
            this.checkBoxSendEMail.Size = new System.Drawing.Size(280, 17);
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
            this.textBoxEMail.Location = new System.Drawing.Point(338, 535);
            this.textBoxEMail.Name = "textBoxEMail";
            this.textBoxEMail.Size = new System.Drawing.Size(259, 20);
            this.textBoxEMail.TabIndex = 69;
            // 
            // buttonTestEmail
            // 
            this.buttonTestEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonTestEmail.Enabled = false;
            this.buttonTestEmail.Location = new System.Drawing.Point(613, 533);
            this.buttonTestEmail.Name = "buttonTestEmail";
            this.buttonTestEmail.Size = new System.Drawing.Size(75, 23);
            this.buttonTestEmail.TabIndex = 70;
            this.buttonTestEmail.Text = "Test now";
            this.buttonTestEmail.UseVisualStyleBackColor = true;
            this.buttonTestEmail.Click += new System.EventHandler(this.buttonTestEmail_Click);
            // 
            // radioButtonInsertWorkflowAsset
            // 
            this.radioButtonInsertWorkflowAsset.AutoSize = true;
            this.radioButtonInsertWorkflowAsset.Location = new System.Drawing.Point(14, 41);
            this.radioButtonInsertWorkflowAsset.Name = "radioButtonInsertWorkflowAsset";
            this.radioButtonInsertWorkflowAsset.Size = new System.Drawing.Size(133, 30);
            this.radioButtonInsertWorkflowAsset.TabIndex = 72;
            this.radioButtonInsertWorkflowAsset.Text = "Insert a workflow asset\r\nas first asset";
            this.radioButtonInsertWorkflowAsset.UseVisualStyleBackColor = true;
            this.radioButtonInsertWorkflowAsset.CheckedChanged += new System.EventHandler(this.radioButtonInsertWorkflowAsset_CheckedChanged);
            // 
            // radioButtonInsertSelectedAssets
            // 
            this.radioButtonInsertSelectedAssets.AutoSize = true;
            this.radioButtonInsertSelectedAssets.Checked = true;
            this.radioButtonInsertSelectedAssets.Location = new System.Drawing.Point(14, 5);
            this.radioButtonInsertSelectedAssets.Name = "radioButtonInsertSelectedAssets";
            this.radioButtonInsertSelectedAssets.Size = new System.Drawing.Size(148, 30);
            this.radioButtonInsertSelectedAssets.TabIndex = 73;
            this.radioButtonInsertSelectedAssets.TabStop = true;
            this.radioButtonInsertSelectedAssets.Text = "Insert the selected assets\r\nbefore the uploaded asset";
            this.radioButtonInsertSelectedAssets.UseVisualStyleBackColor = true;
            // 
            // groupBoxProcess
            // 
            this.groupBoxProcess.Controls.Add(this.panelInsertAsset);
            this.groupBoxProcess.Controls.Add(this.checkBoAddAssetsToInput);
            this.groupBoxProcess.Controls.Add(this.listViewTemplates);
            this.groupBoxProcess.Enabled = false;
            this.groupBoxProcess.Location = new System.Drawing.Point(52, 285);
            this.groupBoxProcess.Name = "groupBoxProcess";
            this.groupBoxProcess.Size = new System.Drawing.Size(636, 223);
            this.groupBoxProcess.TabIndex = 74;
            this.groupBoxProcess.TabStop = false;
            this.groupBoxProcess.Text = "Job template and input assets";
            // 
            // panelInsertAsset
            // 
            this.panelInsertAsset.Controls.Add(this.listViewWorkflows1);
            this.panelInsertAsset.Controls.Add(this.radioButtonInsertSelectedAssets);
            this.panelInsertAsset.Controls.Add(this.radioButtonInsertWorkflowAsset);
            this.panelInsertAsset.Location = new System.Drawing.Point(16, 126);
            this.panelInsertAsset.Name = "panelInsertAsset";
            this.panelInsertAsset.Size = new System.Drawing.Size(620, 86);
            this.panelInsertAsset.TabIndex = 75;
            // 
            // checkBoAddAssetsToInput
            // 
            this.checkBoAddAssetsToInput.AutoSize = true;
            this.checkBoAddAssetsToInput.Location = new System.Drawing.Point(16, 108);
            this.checkBoAddAssetsToInput.Name = "checkBoAddAssetsToInput";
            this.checkBoAddAssetsToInput.Size = new System.Drawing.Size(150, 17);
            this.checkBoAddAssetsToInput.TabIndex = 74;
            this.checkBoAddAssetsToInput.Text = "Add asset(s) to input asset";
            this.checkBoAddAssetsToInput.UseVisualStyleBackColor = true;
            this.checkBoAddAssetsToInput.CheckedChanged += new System.EventHandler(this.checkBoAddAssetsToInput_CheckedChanged);
            // 
            // listViewWorkflows1
            // 
            this.listViewWorkflows1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewWorkflows1.Enabled = false;
            this.listViewWorkflows1.FullRowSelect = true;
            this.listViewWorkflows1.HideSelection = false;
            this.listViewWorkflows1.Location = new System.Drawing.Point(168, 5);
            this.listViewWorkflows1.MultiSelect = false;
            this.listViewWorkflows1.Name = "listViewWorkflows1";
            this.listViewWorkflows1.Size = new System.Drawing.Size(446, 78);
            this.listViewWorkflows1.TabIndex = 61;
            this.listViewWorkflows1.Tag = -1;
            this.listViewWorkflows1.UseCompatibleStateImageBehavior = false;
            this.listViewWorkflows1.View = System.Windows.Forms.View.Details;
            // 
            // listViewTemplates
            // 
            this.listViewTemplates.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewTemplates.Enabled = false;
            this.listViewTemplates.FullRowSelect = true;
            this.listViewTemplates.HideSelection = false;
            this.listViewTemplates.Location = new System.Drawing.Point(16, 19);
            this.listViewTemplates.MultiSelect = false;
            this.listViewTemplates.Name = "listViewTemplates";
            this.listViewTemplates.Size = new System.Drawing.Size(614, 82);
            this.listViewTemplates.TabIndex = 61;
            this.listViewTemplates.Tag = -1;
            this.listViewTemplates.UseCompatibleStateImageBehavior = false;
            this.listViewTemplates.View = System.Windows.Forms.View.Details;
            this.listViewTemplates.SelectedIndexChanged += new System.EventHandler(this.listViewTemplates_SelectedIndexChanged);
            // 
            // WatchFolder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(714, 621);
            this.Controls.Add(this.groupBoxProcess);
            this.Controls.Add(this.buttonTestEmail);
            this.Controls.Add(this.textBoxEMail);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.checkBoxRunJobTemplate);
            this.Controls.Add(this.checkBoxSendEMail);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.checkBoxPublishOAssets);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.checkBoxDeleteFile);
            this.Controls.Add(this.checkBoxUseQueue);
            this.Controls.Add(this.textBoxFolder);
            this.Controls.Add(this.buttonSelFolder);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.label1);
            this.Name = "WatchFolder";
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radioButtonON;
        private System.Windows.Forms.RadioButton radioButtonOFF;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button buttonSelFolder;
        private System.Windows.Forms.TextBox textBoxFolder;
        private System.Windows.Forms.CheckBox checkBoxUseQueue;
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
    }
}