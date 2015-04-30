﻿namespace AMSExplorer
{
    partial class ChannelInformation
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
            this.DGChannel = new System.Windows.Forms.DataGridView();
            this.contextMenuStripDG = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemFilesCopyClipboard = new System.Windows.Forms.ToolStripMenuItem();
            this.folderBrowserDialogDownload = new System.Windows.Forms.FolderBrowserDialog();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageChannelInfo = new System.Windows.Forms.TabPage();
            this.tabPageSettings = new System.Windows.Forms.TabPage();
            this.buttonAllowAllPreviewIP = new System.Windows.Forms.Button();
            this.buttonAllowAllInputIP = new System.Windows.Forms.Button();
            this.numericUpDownHLSFragPerSeg = new System.Windows.Forms.NumericUpDown();
            this.checkBoxHLSFragPerSeg = new System.Windows.Forms.CheckBox();
            this.checkBoxKeyFrameIntDefined = new System.Windows.Forms.CheckBox();
            this.textBoxKeyFrame = new System.Windows.Forms.TextBox();
            this.checkBoxInputSet = new System.Windows.Forms.CheckBox();
            this.checkBoxPreviewSet = new System.Windows.Forms.CheckBox();
            this.dataGridViewInputIP = new System.Windows.Forms.DataGridView();
            this.dataGridViewPreviewIP = new System.Windows.Forms.DataGridView();
            this.buttonDelPreviewIP = new System.Windows.Forms.Button();
            this.buttonDelInputIP = new System.Windows.Forms.Button();
            this.buttonAddPreviewIP = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonAddInputIP = new System.Windows.Forms.Button();
            this.textboxchannedesc = new System.Windows.Forms.TextBox();
            this.tabPagePolicies = new System.Windows.Forms.TabPage();
            this.checkBoxcrossdomains = new System.Windows.Forms.CheckBox();
            this.textBoxCrossDomPolicy = new System.Windows.Forms.TextBox();
            this.checkBoxclientpolicy = new System.Windows.Forms.CheckBox();
            this.textBoxClientPolicy = new System.Windows.Forms.TextBox();
            this.tabPagePreview = new System.Windows.Forms.TabPage();
            this.webBrowserPreview = new System.Windows.Forms.WebBrowser();
            this.labelChannelName = new System.Windows.Forms.Label();
            this.buttonDisregard = new System.Windows.Forms.Button();
            this.buttonApplyClose = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.openFileDialogSlate = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.DGChannel)).BeginInit();
            this.contextMenuStripDG.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageChannelInfo.SuspendLayout();
            this.tabPageSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHLSFragPerSeg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInputIP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPreviewIP)).BeginInit();
            this.tabPagePolicies.SuspendLayout();
            this.tabPagePreview.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // DGChannel
            // 
            this.DGChannel.AllowUserToAddRows = false;
            this.DGChannel.AllowUserToDeleteRows = false;
            this.DGChannel.AllowUserToResizeRows = false;
            this.DGChannel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DGChannel.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DGChannel.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGChannel.ColumnHeadersVisible = false;
            this.DGChannel.ContextMenuStrip = this.contextMenuStripDG;
            this.DGChannel.Location = new System.Drawing.Point(6, 6);
            this.DGChannel.MultiSelect = false;
            this.DGChannel.Name = "DGChannel";
            this.DGChannel.ReadOnly = true;
            this.DGChannel.RowHeadersVisible = false;
            this.DGChannel.Size = new System.Drawing.Size(740, 429);
            this.DGChannel.TabIndex = 0;
            // 
            // contextMenuStripDG
            // 
            this.contextMenuStripDG.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemFilesCopyClipboard});
            this.contextMenuStripDG.Name = "contextMenuStripDG";
            this.contextMenuStripDG.Size = new System.Drawing.Size(170, 26);
            this.contextMenuStripDG.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripDG_Opening);
            this.contextMenuStripDG.MouseClick += new System.Windows.Forms.MouseEventHandler(this.contextMenuStripDG_MouseClick);
            // 
            // toolStripMenuItemFilesCopyClipboard
            // 
            this.toolStripMenuItemFilesCopyClipboard.Name = "toolStripMenuItemFilesCopyClipboard";
            this.toolStripMenuItemFilesCopyClipboard.Size = new System.Drawing.Size(169, 22);
            this.toolStripMenuItemFilesCopyClipboard.Text = "Copy to clipboard";
            this.toolStripMenuItemFilesCopyClipboard.Click += new System.EventHandler(this.toolStripMenuItemFilesCopyClipboard_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPageChannelInfo);
            this.tabControl1.Controls.Add(this.tabPageSettings);
            this.tabControl1.Controls.Add(this.tabPagePolicies);
            this.tabControl1.Controls.Add(this.tabPagePreview);
            this.tabControl1.Location = new System.Drawing.Point(12, 43);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(760, 467);
            this.tabControl1.TabIndex = 34;
            // 
            // tabPageChannelInfo
            // 
            this.tabPageChannelInfo.Controls.Add(this.DGChannel);
            this.tabPageChannelInfo.Location = new System.Drawing.Point(4, 22);
            this.tabPageChannelInfo.Name = "tabPageChannelInfo";
            this.tabPageChannelInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageChannelInfo.Size = new System.Drawing.Size(752, 441);
            this.tabPageChannelInfo.TabIndex = 0;
            this.tabPageChannelInfo.Text = "Channel information";
            this.tabPageChannelInfo.UseVisualStyleBackColor = true;
            // 
            // tabPageSettings
            // 
            this.tabPageSettings.Controls.Add(this.buttonAllowAllPreviewIP);
            this.tabPageSettings.Controls.Add(this.buttonAllowAllInputIP);
            this.tabPageSettings.Controls.Add(this.numericUpDownHLSFragPerSeg);
            this.tabPageSettings.Controls.Add(this.checkBoxHLSFragPerSeg);
            this.tabPageSettings.Controls.Add(this.checkBoxKeyFrameIntDefined);
            this.tabPageSettings.Controls.Add(this.textBoxKeyFrame);
            this.tabPageSettings.Controls.Add(this.checkBoxInputSet);
            this.tabPageSettings.Controls.Add(this.checkBoxPreviewSet);
            this.tabPageSettings.Controls.Add(this.dataGridViewInputIP);
            this.tabPageSettings.Controls.Add(this.dataGridViewPreviewIP);
            this.tabPageSettings.Controls.Add(this.buttonDelPreviewIP);
            this.tabPageSettings.Controls.Add(this.buttonDelInputIP);
            this.tabPageSettings.Controls.Add(this.buttonAddPreviewIP);
            this.tabPageSettings.Controls.Add(this.label2);
            this.tabPageSettings.Controls.Add(this.buttonAddInputIP);
            this.tabPageSettings.Controls.Add(this.textboxchannedesc);
            this.tabPageSettings.Location = new System.Drawing.Point(4, 22);
            this.tabPageSettings.Name = "tabPageSettings";
            this.tabPageSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSettings.Size = new System.Drawing.Size(752, 441);
            this.tabPageSettings.TabIndex = 1;
            this.tabPageSettings.Text = "Settings";
            this.tabPageSettings.UseVisualStyleBackColor = true;
            // 
            // buttonAllowAllPreviewIP
            // 
            this.buttonAllowAllPreviewIP.Location = new System.Drawing.Point(580, 226);
            this.buttonAllowAllPreviewIP.Name = "buttonAllowAllPreviewIP";
            this.buttonAllowAllPreviewIP.Size = new System.Drawing.Size(132, 23);
            this.buttonAllowAllPreviewIP.TabIndex = 57;
            this.buttonAllowAllPreviewIP.Text = "Allow all IP addresses";
            this.buttonAllowAllPreviewIP.UseVisualStyleBackColor = true;
            this.buttonAllowAllPreviewIP.Click += new System.EventHandler(this.buttonAllowAllPreviewIP_Click);
            // 
            // buttonAllowAllInputIP
            // 
            this.buttonAllowAllInputIP.Location = new System.Drawing.Point(223, 226);
            this.buttonAllowAllInputIP.Name = "buttonAllowAllInputIP";
            this.buttonAllowAllInputIP.Size = new System.Drawing.Size(132, 23);
            this.buttonAllowAllInputIP.TabIndex = 56;
            this.buttonAllowAllInputIP.Text = "Allow all IP addresses";
            this.buttonAllowAllInputIP.UseVisualStyleBackColor = true;
            this.buttonAllowAllInputIP.Click += new System.EventHandler(this.buttonAllowAllInputIP_Click);
            // 
            // numericUpDownHLSFragPerSeg
            // 
            this.numericUpDownHLSFragPerSeg.Enabled = false;
            this.numericUpDownHLSFragPerSeg.Location = new System.Drawing.Point(19, 375);
            this.numericUpDownHLSFragPerSeg.Name = "numericUpDownHLSFragPerSeg";
            this.numericUpDownHLSFragPerSeg.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownHLSFragPerSeg.TabIndex = 55;
            this.numericUpDownHLSFragPerSeg.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // checkBoxHLSFragPerSeg
            // 
            this.checkBoxHLSFragPerSeg.AutoSize = true;
            this.checkBoxHLSFragPerSeg.Location = new System.Drawing.Point(19, 352);
            this.checkBoxHLSFragPerSeg.Name = "checkBoxHLSFragPerSeg";
            this.checkBoxHLSFragPerSeg.Size = new System.Drawing.Size(163, 17);
            this.checkBoxHLSFragPerSeg.TabIndex = 54;
            this.checkBoxHLSFragPerSeg.Text = "HLS fragments per segment :";
            this.checkBoxHLSFragPerSeg.UseVisualStyleBackColor = true;
            this.checkBoxHLSFragPerSeg.CheckedChanged += new System.EventHandler(this.checkBoxHLSFragPerSeg_CheckedChanged);
            // 
            // checkBoxKeyFrameIntDefined
            // 
            this.checkBoxKeyFrameIntDefined.AutoSize = true;
            this.checkBoxKeyFrameIntDefined.Location = new System.Drawing.Point(19, 294);
            this.checkBoxKeyFrameIntDefined.Name = "checkBoxKeyFrameIntDefined";
            this.checkBoxKeyFrameIntDefined.Size = new System.Drawing.Size(130, 17);
            this.checkBoxKeyFrameIntDefined.TabIndex = 52;
            this.checkBoxKeyFrameIntDefined.Text = "Key frame interval (s) :";
            this.checkBoxKeyFrameIntDefined.UseVisualStyleBackColor = true;
            this.checkBoxKeyFrameIntDefined.CheckedChanged += new System.EventHandler(this.checkBoxKeyFrameIntDefined_CheckedChanged);
            // 
            // textBoxKeyFrame
            // 
            this.textBoxKeyFrame.Enabled = false;
            this.textBoxKeyFrame.Location = new System.Drawing.Point(19, 314);
            this.textBoxKeyFrame.Name = "textBoxKeyFrame";
            this.textBoxKeyFrame.Size = new System.Drawing.Size(121, 20);
            this.textBoxKeyFrame.TabIndex = 51;
            // 
            // checkBoxInputSet
            // 
            this.checkBoxInputSet.AutoSize = true;
            this.checkBoxInputSet.Location = new System.Drawing.Point(19, 72);
            this.checkBoxInputSet.Name = "checkBoxInputSet";
            this.checkBoxInputSet.Size = new System.Drawing.Size(202, 17);
            this.checkBoxInputSet.TabIndex = 48;
            this.checkBoxInputSet.Text = "Define INPUT allowed IP addresses :";
            this.checkBoxInputSet.UseVisualStyleBackColor = true;
            this.checkBoxInputSet.CheckedChanged += new System.EventHandler(this.checkBoxInputSet_CheckedChanged);
            // 
            // checkBoxPreviewSet
            // 
            this.checkBoxPreviewSet.AutoSize = true;
            this.checkBoxPreviewSet.Location = new System.Drawing.Point(383, 72);
            this.checkBoxPreviewSet.Name = "checkBoxPreviewSet";
            this.checkBoxPreviewSet.Size = new System.Drawing.Size(219, 17);
            this.checkBoxPreviewSet.TabIndex = 42;
            this.checkBoxPreviewSet.Text = "Define PREVIEW allowed IP addresses :";
            this.checkBoxPreviewSet.UseVisualStyleBackColor = true;
            this.checkBoxPreviewSet.CheckedChanged += new System.EventHandler(this.checkBoxPreviewSet_CheckedChanged);
            // 
            // dataGridViewInputIP
            // 
            this.dataGridViewInputIP.AllowUserToAddRows = false;
            this.dataGridViewInputIP.AllowUserToDeleteRows = false;
            this.dataGridViewInputIP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewInputIP.Enabled = false;
            this.dataGridViewInputIP.Location = new System.Drawing.Point(19, 92);
            this.dataGridViewInputIP.Name = "dataGridViewInputIP";
            this.dataGridViewInputIP.RowHeadersVisible = false;
            this.dataGridViewInputIP.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewInputIP.Size = new System.Drawing.Size(336, 128);
            this.dataGridViewInputIP.TabIndex = 38;
            this.dataGridViewInputIP.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridViewInputIP_CellValidating);
            // 
            // dataGridViewPreviewIP
            // 
            this.dataGridViewPreviewIP.AllowUserToAddRows = false;
            this.dataGridViewPreviewIP.AllowUserToDeleteRows = false;
            this.dataGridViewPreviewIP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPreviewIP.Enabled = false;
            this.dataGridViewPreviewIP.Location = new System.Drawing.Point(383, 95);
            this.dataGridViewPreviewIP.Name = "dataGridViewPreviewIP";
            this.dataGridViewPreviewIP.RowHeadersVisible = false;
            this.dataGridViewPreviewIP.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewPreviewIP.Size = new System.Drawing.Size(329, 125);
            this.dataGridViewPreviewIP.TabIndex = 38;
            // 
            // buttonDelPreviewIP
            // 
            this.buttonDelPreviewIP.Enabled = false;
            this.buttonDelPreviewIP.Location = new System.Drawing.Point(465, 226);
            this.buttonDelPreviewIP.Name = "buttonDelPreviewIP";
            this.buttonDelPreviewIP.Size = new System.Drawing.Size(75, 23);
            this.buttonDelPreviewIP.TabIndex = 41;
            this.buttonDelPreviewIP.Text = "Delete";
            this.buttonDelPreviewIP.UseVisualStyleBackColor = true;
            this.buttonDelPreviewIP.Click += new System.EventHandler(this.buttonDelPreviewIP_Click);
            // 
            // buttonDelInputIP
            // 
            this.buttonDelInputIP.Enabled = false;
            this.buttonDelInputIP.Location = new System.Drawing.Point(100, 226);
            this.buttonDelInputIP.Name = "buttonDelInputIP";
            this.buttonDelInputIP.Size = new System.Drawing.Size(75, 23);
            this.buttonDelInputIP.TabIndex = 41;
            this.buttonDelInputIP.Text = "Delete";
            this.buttonDelInputIP.UseVisualStyleBackColor = true;
            this.buttonDelInputIP.Click += new System.EventHandler(this.buttonDelIngestIP_Click);
            // 
            // buttonAddPreviewIP
            // 
            this.buttonAddPreviewIP.Enabled = false;
            this.buttonAddPreviewIP.Location = new System.Drawing.Point(383, 226);
            this.buttonAddPreviewIP.Name = "buttonAddPreviewIP";
            this.buttonAddPreviewIP.Size = new System.Drawing.Size(75, 23);
            this.buttonAddPreviewIP.TabIndex = 40;
            this.buttonAddPreviewIP.Text = "Add";
            this.buttonAddPreviewIP.UseVisualStyleBackColor = true;
            this.buttonAddPreviewIP.Click += new System.EventHandler(this.buttonAddPreviewIP_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 47;
            this.label2.Text = "Description :";
            // 
            // buttonAddInputIP
            // 
            this.buttonAddInputIP.Enabled = false;
            this.buttonAddInputIP.Location = new System.Drawing.Point(19, 226);
            this.buttonAddInputIP.Name = "buttonAddInputIP";
            this.buttonAddInputIP.Size = new System.Drawing.Size(75, 23);
            this.buttonAddInputIP.TabIndex = 40;
            this.buttonAddInputIP.Text = "Add";
            this.buttonAddInputIP.UseVisualStyleBackColor = true;
            this.buttonAddInputIP.Click += new System.EventHandler(this.buttonAddIngestIP_Click);
            // 
            // textboxchannedesc
            // 
            this.textboxchannedesc.Location = new System.Drawing.Point(19, 36);
            this.textboxchannedesc.Name = "textboxchannedesc";
            this.textboxchannedesc.Size = new System.Drawing.Size(693, 20);
            this.textboxchannedesc.TabIndex = 46;
            // 
            // tabPagePolicies
            // 
            this.tabPagePolicies.Controls.Add(this.checkBoxcrossdomains);
            this.tabPagePolicies.Controls.Add(this.textBoxCrossDomPolicy);
            this.tabPagePolicies.Controls.Add(this.checkBoxclientpolicy);
            this.tabPagePolicies.Controls.Add(this.textBoxClientPolicy);
            this.tabPagePolicies.Location = new System.Drawing.Point(4, 22);
            this.tabPagePolicies.Name = "tabPagePolicies";
            this.tabPagePolicies.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePolicies.Size = new System.Drawing.Size(752, 441);
            this.tabPagePolicies.TabIndex = 2;
            this.tabPagePolicies.Text = "Policies";
            this.tabPagePolicies.UseVisualStyleBackColor = true;
            // 
            // checkBoxcrossdomains
            // 
            this.checkBoxcrossdomains.AutoSize = true;
            this.checkBoxcrossdomains.Location = new System.Drawing.Point(3, 234);
            this.checkBoxcrossdomains.Name = "checkBoxcrossdomains";
            this.checkBoxcrossdomains.Size = new System.Drawing.Size(157, 17);
            this.checkBoxcrossdomains.TabIndex = 66;
            this.checkBoxcrossdomains.Text = "Define cross domains policy";
            this.checkBoxcrossdomains.UseVisualStyleBackColor = true;
            this.checkBoxcrossdomains.CheckedChanged += new System.EventHandler(this.checkBoxcrossdomains_CheckedChanged_1);
            // 
            // textBoxCrossDomPolicy
            // 
            this.textBoxCrossDomPolicy.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxCrossDomPolicy.Enabled = false;
            this.textBoxCrossDomPolicy.Location = new System.Drawing.Point(3, 257);
            this.textBoxCrossDomPolicy.Multiline = true;
            this.textBoxCrossDomPolicy.Name = "textBoxCrossDomPolicy";
            this.textBoxCrossDomPolicy.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxCrossDomPolicy.Size = new System.Drawing.Size(743, 178);
            this.textBoxCrossDomPolicy.TabIndex = 65;
            // 
            // checkBoxclientpolicy
            // 
            this.checkBoxclientpolicy.AutoSize = true;
            this.checkBoxclientpolicy.Location = new System.Drawing.Point(6, 6);
            this.checkBoxclientpolicy.Name = "checkBoxclientpolicy";
            this.checkBoxclientpolicy.Size = new System.Drawing.Size(121, 17);
            this.checkBoxclientpolicy.TabIndex = 64;
            this.checkBoxclientpolicy.Text = "Define client policy :";
            this.checkBoxclientpolicy.UseVisualStyleBackColor = true;
            this.checkBoxclientpolicy.CheckedChanged += new System.EventHandler(this.checkBoxclientpolicy_CheckedChanged_1);
            // 
            // textBoxClientPolicy
            // 
            this.textBoxClientPolicy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxClientPolicy.Enabled = false;
            this.textBoxClientPolicy.Location = new System.Drawing.Point(6, 29);
            this.textBoxClientPolicy.Multiline = true;
            this.textBoxClientPolicy.Name = "textBoxClientPolicy";
            this.textBoxClientPolicy.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxClientPolicy.Size = new System.Drawing.Size(743, 189);
            this.textBoxClientPolicy.TabIndex = 63;
            // 
            // tabPagePreview
            // 
            this.tabPagePreview.Controls.Add(this.webBrowserPreview);
            this.tabPagePreview.Location = new System.Drawing.Point(4, 22);
            this.tabPagePreview.Name = "tabPagePreview";
            this.tabPagePreview.Size = new System.Drawing.Size(752, 441);
            this.tabPagePreview.TabIndex = 3;
            this.tabPagePreview.Text = "Preview";
            this.tabPagePreview.UseVisualStyleBackColor = true;
            this.tabPagePreview.Enter += new System.EventHandler(this.tabPage4_Enter);
            this.tabPagePreview.Leave += new System.EventHandler(this.tabPage4_Leave);
            // 
            // webBrowserPreview
            // 
            this.webBrowserPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserPreview.Location = new System.Drawing.Point(0, 0);
            this.webBrowserPreview.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserPreview.Name = "webBrowserPreview";
            this.webBrowserPreview.Size = new System.Drawing.Size(752, 441);
            this.webBrowserPreview.TabIndex = 0;
            // 
            // labelChannelName
            // 
            this.labelChannelName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelChannelName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelChannelName.Location = new System.Drawing.Point(18, 9);
            this.labelChannelName.Name = "labelChannelName";
            this.labelChannelName.Size = new System.Drawing.Size(744, 20);
            this.labelChannelName.TabIndex = 37;
            this.labelChannelName.Text = "Channel : ";
            // 
            // buttonDisregard
            // 
            this.buttonDisregard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDisregard.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonDisregard.Location = new System.Drawing.Point(668, 10);
            this.buttonDisregard.Name = "buttonDisregard";
            this.buttonDisregard.Size = new System.Drawing.Size(106, 23);
            this.buttonDisregard.TabIndex = 41;
            this.buttonDisregard.Text = "Close";
            this.buttonDisregard.UseVisualStyleBackColor = true;
            // 
            // buttonApplyClose
            // 
            this.buttonApplyClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonApplyClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonApplyClose.Location = new System.Drawing.Point(503, 10);
            this.buttonApplyClose.Name = "buttonApplyClose";
            this.buttonApplyClose.Size = new System.Drawing.Size(159, 23);
            this.buttonApplyClose.TabIndex = 40;
            this.buttonApplyClose.Text = "Update settings and close";
            this.buttonApplyClose.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonDisregard);
            this.panel1.Controls.Add(this.buttonApplyClose);
            this.panel1.Location = new System.Drawing.Point(-2, 516);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(790, 48);
            this.panel1.TabIndex = 58;
            // 
            // openFileDialogSlate
            // 
            this.openFileDialogSlate.Filter = "Image|*.jpg|All files (*.*)|*.*";
            // 
            // ChannelInformation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labelChannelName);
            this.Controls.Add(this.tabControl1);
            this.Name = "ChannelInformation";
            this.Text = "Channel Information";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ChanneltInformation_FormClosed);
            this.Load += new System.EventHandler(this.ChannelInformation_Load);
            this.Shown += new System.EventHandler(this.ChannelInformation_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.DGChannel)).EndInit();
            this.contextMenuStripDG.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPageChannelInfo.ResumeLayout(false);
            this.tabPageSettings.ResumeLayout(false);
            this.tabPageSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHLSFragPerSeg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInputIP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPreviewIP)).EndInit();
            this.tabPagePolicies.ResumeLayout(false);
            this.tabPagePolicies.PerformLayout();
            this.tabPagePreview.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView DGChannel;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripDG;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFilesCopyClipboard;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogDownload;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageChannelInfo;
        private System.Windows.Forms.Label labelChannelName;
        private System.Windows.Forms.TabPage tabPageSettings;
        private System.Windows.Forms.DataGridView dataGridViewPreviewIP;
        private System.Windows.Forms.Button buttonDelPreviewIP;
        private System.Windows.Forms.Button buttonAddPreviewIP;
        private System.Windows.Forms.DataGridView dataGridViewInputIP;
        private System.Windows.Forms.Button buttonDelInputIP;
        private System.Windows.Forms.Button buttonAddInputIP;
        private System.Windows.Forms.Button buttonDisregard;
        private System.Windows.Forms.Button buttonApplyClose;
        private System.Windows.Forms.CheckBox checkBoxPreviewSet;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textboxchannedesc;
        private System.Windows.Forms.TabPage tabPagePolicies;
        private System.Windows.Forms.CheckBox checkBoxcrossdomains;
        private System.Windows.Forms.TextBox textBoxCrossDomPolicy;
        private System.Windows.Forms.CheckBox checkBoxclientpolicy;
        private System.Windows.Forms.TextBox textBoxClientPolicy;
        private System.Windows.Forms.CheckBox checkBoxInputSet;
        private System.Windows.Forms.CheckBox checkBoxKeyFrameIntDefined;
        private System.Windows.Forms.TextBox textBoxKeyFrame;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox checkBoxHLSFragPerSeg;
        private System.Windows.Forms.NumericUpDown numericUpDownHLSFragPerSeg;
        private System.Windows.Forms.Button buttonAllowAllInputIP;
        private System.Windows.Forms.Button buttonAllowAllPreviewIP;
        private System.Windows.Forms.TabPage tabPagePreview;
        private System.Windows.Forms.WebBrowser webBrowserPreview;
        private System.Windows.Forms.OpenFileDialog openFileDialogSlate;
    }
}