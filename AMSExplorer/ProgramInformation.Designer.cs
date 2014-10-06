namespace AMSExplorer
{
    partial class ProgramInformation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProgramInformation));
            this.contextMenuStripDG = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemFilesCopyClipboard = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripLocators = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemPlaybackFlashAzure = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemPlaybackSilverlightMonitoring = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemDASHIF = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemDASHAZURE = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemPlaybackMP4 = new System.Windows.Forms.ToolStripMenuItem();
            this.folderBrowserDialogDownload = new System.Windows.Forms.FolderBrowserDialog();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.buttonOpen = new System.Windows.Forms.Button();
            this.buttonDashAzure = new System.Windows.Forms.Button();
            this.buttonSLMonitor = new System.Windows.Forms.Button();
            this.buttonHTML = new System.Windows.Forms.Button();
            this.buttonDASH = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonFlash = new System.Windows.Forms.Button();
            this.TreeViewLocators = new System.Windows.Forms.TreeView();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.DGChannel = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonCopyStats = new System.Windows.Forms.Button();
            this.buttonCreateMail = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.labelProgramName = new System.Windows.Forms.Label();
            this.buttonDisregard = new System.Windows.Forms.Button();
            this.buttonApplyClose = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.numericUpDownArchiveMinutes = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.numericUpDownArchiveHours = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownArchiveDays = new System.Windows.Forms.NumericUpDown();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.contextMenuStripDG.SuspendLayout();
            this.contextMenuStripLocators.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGChannel)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownArchiveMinutes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownArchiveHours)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownArchiveDays)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
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
            // 
            // contextMenuStripLocators
            // 
            this.contextMenuStripLocators.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemCopy,
            this.toolStripMenuItemOpen,
            this.toolStripMenuItemPlaybackFlashAzure,
            this.toolStripMenuItemPlaybackSilverlightMonitoring,
            this.toolStripMenuItemDASHIF,
            this.toolStripMenuItemDASHAZURE,
            this.toolStripMenuItemPlaybackMP4});
            this.contextMenuStripLocators.Name = "contextMenuStripLocators";
            this.contextMenuStripLocators.Size = new System.Drawing.Size(323, 158);
            this.contextMenuStripLocators.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripLocators_Opening);
            // 
            // toolStripMenuItemCopy
            // 
            this.toolStripMenuItemCopy.Name = "toolStripMenuItemCopy";
            this.toolStripMenuItemCopy.Size = new System.Drawing.Size(322, 22);
            this.toolStripMenuItemCopy.Text = "Copy to clipboard";
            this.toolStripMenuItemCopy.Click += new System.EventHandler(this.toolStripMenuItemCopy_Click);
            // 
            // toolStripMenuItemOpen
            // 
            this.toolStripMenuItemOpen.Enabled = false;
            this.toolStripMenuItemOpen.Name = "toolStripMenuItemOpen";
            this.toolStripMenuItemOpen.Size = new System.Drawing.Size(322, 22);
            this.toolStripMenuItemOpen.Text = "Open the file";
            this.toolStripMenuItemOpen.Click += new System.EventHandler(this.toolStripMenuItemOpen_Click);
            // 
            // toolStripMenuItemPlaybackFlashAzure
            // 
            this.toolStripMenuItemPlaybackFlashAzure.Enabled = false;
            this.toolStripMenuItemPlaybackFlashAzure.Name = "toolStripMenuItemPlaybackFlashAzure";
            this.toolStripMenuItemPlaybackFlashAzure.Size = new System.Drawing.Size(322, 22);
            this.toolStripMenuItemPlaybackFlashAzure.Text = "Playback with Flash OSMF Azure Player";
            this.toolStripMenuItemPlaybackFlashAzure.Click += new System.EventHandler(this.toolStripMenuItemPlaybackFlash_Click);
            // 
            // toolStripMenuItemPlaybackSilverlightMonitoring
            // 
            this.toolStripMenuItemPlaybackSilverlightMonitoring.Enabled = false;
            this.toolStripMenuItemPlaybackSilverlightMonitoring.Name = "toolStripMenuItemPlaybackSilverlightMonitoring";
            this.toolStripMenuItemPlaybackSilverlightMonitoring.Size = new System.Drawing.Size(322, 22);
            this.toolStripMenuItemPlaybackSilverlightMonitoring.Text = "Playback with Silverlight Monitoring Player";
            this.toolStripMenuItemPlaybackSilverlightMonitoring.Click += new System.EventHandler(this.toolStripMenuItemPlaybackSilverlight_Click);
            // 
            // toolStripMenuItemDASHIF
            // 
            this.toolStripMenuItemDASHIF.Enabled = false;
            this.toolStripMenuItemDASHIF.Name = "toolStripMenuItemDASHIF";
            this.toolStripMenuItemDASHIF.Size = new System.Drawing.Size(322, 22);
            this.toolStripMenuItemDASHIF.Text = "Playback with MPEG-DASH-IF Reference Player";
            this.toolStripMenuItemDASHIF.Click += new System.EventHandler(this.toolStripMenuItemDASHIF_Click);
            // 
            // toolStripMenuItemDASHAZURE
            // 
            this.toolStripMenuItemDASHAZURE.Name = "toolStripMenuItemDASHAZURE";
            this.toolStripMenuItemDASHAZURE.Size = new System.Drawing.Size(322, 22);
            this.toolStripMenuItemDASHAZURE.Text = "Playback with MPEG-DASH Azure Player";
            this.toolStripMenuItemDASHAZURE.Click += new System.EventHandler(this.playbackWithToolStripMenuItem_Click);
            // 
            // toolStripMenuItemPlaybackMP4
            // 
            this.toolStripMenuItemPlaybackMP4.Enabled = false;
            this.toolStripMenuItemPlaybackMP4.Name = "toolStripMenuItemPlaybackMP4";
            this.toolStripMenuItemPlaybackMP4.Size = new System.Drawing.Size(322, 22);
            this.toolStripMenuItemPlaybackMP4.Text = "Playback with HTML Player (MP4)";
            this.toolStripMenuItemPlaybackMP4.Click += new System.EventHandler(this.toolStripMenuItemPlaybackMP4_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.buttonOpen);
            this.tabPage3.Controls.Add(this.buttonDashAzure);
            this.tabPage3.Controls.Add(this.buttonSLMonitor);
            this.tabPage3.Controls.Add(this.buttonHTML);
            this.tabPage3.Controls.Add(this.buttonDASH);
            this.tabPage3.Controls.Add(this.label1);
            this.tabPage3.Controls.Add(this.buttonFlash);
            this.tabPage3.Controls.Add(this.TreeViewLocators);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(752, 438);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Locators";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // buttonOpen
            // 
            this.buttonOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonOpen.Enabled = false;
            this.buttonOpen.Location = new System.Drawing.Point(68, 405);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(50, 23);
            this.buttonOpen.TabIndex = 25;
            this.buttonOpen.Text = "Open";
            this.buttonOpen.UseVisualStyleBackColor = true;
            this.buttonOpen.Click += new System.EventHandler(this.buttonOpen_Click);
            // 
            // buttonDashAzure
            // 
            this.buttonDashAzure.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDashAzure.Enabled = false;
            this.buttonDashAzure.Location = new System.Drawing.Point(516, 405);
            this.buttonDashAzure.Name = "buttonDashAzure";
            this.buttonDashAzure.Size = new System.Drawing.Size(112, 23);
            this.buttonDashAzure.TabIndex = 27;
            this.buttonDashAzure.Text = "DASH Azure Player";
            this.buttonDashAzure.UseVisualStyleBackColor = true;
            this.buttonDashAzure.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonSLMonitor
            // 
            this.buttonSLMonitor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSLMonitor.Enabled = false;
            this.buttonSLMonitor.Location = new System.Drawing.Point(289, 405);
            this.buttonSLMonitor.Name = "buttonSLMonitor";
            this.buttonSLMonitor.Size = new System.Drawing.Size(103, 23);
            this.buttonSLMonitor.TabIndex = 26;
            this.buttonSLMonitor.Text = "Silverlight Monitor";
            this.buttonSLMonitor.UseVisualStyleBackColor = true;
            this.buttonSLMonitor.Click += new System.EventHandler(this.buttonSLMonitor_Click);
            // 
            // buttonHTML
            // 
            this.buttonHTML.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonHTML.Enabled = false;
            this.buttonHTML.Location = new System.Drawing.Point(634, 405);
            this.buttonHTML.Name = "buttonHTML";
            this.buttonHTML.Size = new System.Drawing.Size(111, 23);
            this.buttonHTML.TabIndex = 24;
            this.buttonHTML.Text = "HTML Player (MP4)";
            this.buttonHTML.UseVisualStyleBackColor = true;
            this.buttonHTML.Click += new System.EventHandler(this.buttonHTML_Click);
            // 
            // buttonDASH
            // 
            this.buttonDASH.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDASH.Enabled = false;
            this.buttonDASH.Location = new System.Drawing.Point(398, 405);
            this.buttonDASH.Name = "buttonDASH";
            this.buttonDASH.Size = new System.Drawing.Size(112, 23);
            this.buttonDASH.TabIndex = 23;
            this.buttonDASH.Text = "DASH-IF Ref player";
            this.buttonDASH.UseVisualStyleBackColor = true;
            this.buttonDASH.Click += new System.EventHandler(this.button3_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(124, 410);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "Playback with:";
            // 
            // buttonFlash
            // 
            this.buttonFlash.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonFlash.Enabled = false;
            this.buttonFlash.Location = new System.Drawing.Point(206, 405);
            this.buttonFlash.Name = "buttonFlash";
            this.buttonFlash.Size = new System.Drawing.Size(77, 23);
            this.buttonFlash.TabIndex = 20;
            this.buttonFlash.Text = "Flash OSMF";
            this.buttonFlash.UseVisualStyleBackColor = true;
            this.buttonFlash.Click += new System.EventHandler(this.buttonFlash_Click);
            // 
            // TreeViewLocators
            // 
            this.TreeViewLocators.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TreeViewLocators.ContextMenuStrip = this.contextMenuStripLocators;
            this.TreeViewLocators.Location = new System.Drawing.Point(6, 6);
            this.TreeViewLocators.Name = "TreeViewLocators";
            this.TreeViewLocators.Size = new System.Drawing.Size(740, 393);
            this.TreeViewLocators.TabIndex = 19;
            this.TreeViewLocators.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeViewLocators_AfterSelect);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.DGChannel);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.buttonCopyStats);
            this.tabPage1.Controls.Add(this.buttonCreateMail);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(752, 438);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Program information";
            this.tabPage1.UseVisualStyleBackColor = true;
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
            this.DGChannel.Size = new System.Drawing.Size(740, 392);
            this.DGChannel.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 409);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 26;
            this.label4.Text = "Asset report:";
            // 
            // buttonCopyStats
            // 
            this.buttonCopyStats.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCopyStats.Location = new System.Drawing.Point(78, 404);
            this.buttonCopyStats.Name = "buttonCopyStats";
            this.buttonCopyStats.Size = new System.Drawing.Size(104, 23);
            this.buttonCopyStats.TabIndex = 24;
            this.buttonCopyStats.Text = "Copy to clipboard";
            this.buttonCopyStats.UseVisualStyleBackColor = true;
            this.buttonCopyStats.Click += new System.EventHandler(this.buttonCopyStats_Click);
            // 
            // buttonCreateMail
            // 
            this.buttonCreateMail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCreateMail.Location = new System.Drawing.Point(188, 404);
            this.buttonCreateMail.Name = "buttonCreateMail";
            this.buttonCreateMail.Size = new System.Drawing.Size(138, 23);
            this.buttonCreateMail.TabIndex = 25;
            this.buttonCreateMail.Text = "Create new Outlook email";
            this.buttonCreateMail.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 46);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(760, 464);
            this.tabControl1.TabIndex = 34;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.textBoxDescription);
            this.tabPage2.Controls.Add(this.groupBox4);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(752, 438);
            this.tabPage2.TabIndex = 3;
            this.tabPage2.Text = "Settings";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // labelProgramName
            // 
            this.labelProgramName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelProgramName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelProgramName.Location = new System.Drawing.Point(18, 9);
            this.labelProgramName.Name = "labelProgramName";
            this.labelProgramName.Size = new System.Drawing.Size(744, 20);
            this.labelProgramName.TabIndex = 38;
            this.labelProgramName.Text = "Program : ";
            this.labelProgramName.Click += new System.EventHandler(this.labelProgramName_Click);
            // 
            // buttonDisregard
            // 
            this.buttonDisregard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDisregard.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonDisregard.Location = new System.Drawing.Point(662, 526);
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
            this.buttonApplyClose.Location = new System.Drawing.Point(497, 526);
            this.buttonApplyClose.Name = "buttonApplyClose";
            this.buttonApplyClose.Size = new System.Drawing.Size(159, 23);
            this.buttonApplyClose.TabIndex = 40;
            this.buttonApplyClose.Text = "Update settings and close";
            this.buttonApplyClose.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 52;
            this.label2.Text = "Description :";
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDescription.Location = new System.Drawing.Point(20, 34);
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(449, 20);
            this.textBoxDescription.TabIndex = 51;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.numericUpDownArchiveMinutes);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.numericUpDownArchiveHours);
            this.groupBox4.Controls.Add(this.numericUpDownArchiveDays);
            this.groupBox4.Controls.Add(this.pictureBox2);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Location = new System.Drawing.Point(19, 96);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(449, 128);
            this.groupBox4.TabIndex = 50;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Settings";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(284, 21);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(44, 13);
            this.label11.TabIndex = 63;
            this.label11.Text = "Minutes";
            // 
            // numericUpDownArchiveMinutes
            // 
            this.numericUpDownArchiveMinutes.Location = new System.Drawing.Point(287, 37);
            this.numericUpDownArchiveMinutes.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.numericUpDownArchiveMinutes.Name = "numericUpDownArchiveMinutes";
            this.numericUpDownArchiveMinutes.Size = new System.Drawing.Size(47, 20);
            this.numericUpDownArchiveMinutes.TabIndex = 62;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(231, 21);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(35, 13);
            this.label10.TabIndex = 61;
            this.label10.Text = "Hours";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(178, 21);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(31, 13);
            this.label9.TabIndex = 60;
            this.label9.Text = "Days";
            // 
            // numericUpDownArchiveHours
            // 
            this.numericUpDownArchiveHours.Location = new System.Drawing.Point(234, 37);
            this.numericUpDownArchiveHours.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.numericUpDownArchiveHours.Name = "numericUpDownArchiveHours";
            this.numericUpDownArchiveHours.Size = new System.Drawing.Size(47, 20);
            this.numericUpDownArchiveHours.TabIndex = 59;
            // 
            // numericUpDownArchiveDays
            // 
            this.numericUpDownArchiveDays.Location = new System.Drawing.Point(181, 37);
            this.numericUpDownArchiveDays.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownArchiveDays.Name = "numericUpDownArchiveDays";
            this.numericUpDownArchiveDays.Size = new System.Drawing.Size(47, 20);
            this.numericUpDownArchiveDays.TabIndex = 58;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::AMSExplorer.Bitmaps.archive;
            this.pictureBox2.Location = new System.Drawing.Point(16, 34);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(16, 16);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 49;
            this.pictureBox2.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(48, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(127, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Archive Window Length :";
            // 
            // ProgramInformation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.buttonDisregard);
            this.Controls.Add(this.buttonApplyClose);
            this.Controls.Add(this.labelProgramName);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ProgramInformation";
            this.Text = "Program Information";
            this.Load += new System.EventHandler(this.ProgramInformation_Load_1);
            this.Shown += new System.EventHandler(this.ProgramInformation_Shown);
            this.contextMenuStripDG.ResumeLayout(false);
            this.contextMenuStripLocators.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGChannel)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownArchiveMinutes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownArchiveHours)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownArchiveDays)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStripLocators;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCopy;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripDG;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFilesCopyClipboard;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemPlaybackFlashAzure;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemPlaybackSilverlightMonitoring;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDASHIF;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemPlaybackMP4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemOpen;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogDownload;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDASHAZURE;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button buttonOpen;
        private System.Windows.Forms.Button buttonDashAzure;
        private System.Windows.Forms.Button buttonSLMonitor;
        private System.Windows.Forms.Button buttonHTML;
        private System.Windows.Forms.Button buttonDASH;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonFlash;
        private System.Windows.Forms.TreeView TreeViewLocators;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView DGChannel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonCopyStats;
        private System.Windows.Forms.Button buttonCreateMail;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Label labelProgramName;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button buttonDisregard;
        private System.Windows.Forms.Button buttonApplyClose;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown numericUpDownArchiveMinutes;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown numericUpDownArchiveHours;
        private System.Windows.Forms.NumericUpDown numericUpDownArchiveDays;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label3;
    }
}