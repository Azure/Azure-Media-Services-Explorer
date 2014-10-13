namespace AMSExplorer
{
    partial class AssetInformation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AssetInformation));
            this.DGAsset = new System.Windows.Forms.DataGridView();
            this.contextMenuStripDG = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemFilesCopyClipboard = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonClose = new System.Windows.Forms.Button();
            this.contextMenuStripLocators = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteLocatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemPlaybackFlashAzure = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemPlaybackSilverlightMonitoring = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemDASHIF = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemDASHAzure = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemPlaybackMP4 = new System.Windows.Forms.ToolStripMenuItem();
            this.TreeViewLocators = new System.Windows.Forms.TreeView();
            this.DGFiles = new System.Windows.Forms.DataGridView();
            this.contextMenuStripFiles = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.makeItPrimaryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemOpenFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemDownloadFile = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.duplicateFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uploadASmallFileInTheAssetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.folderBrowserDialogDownload = new System.Windows.Forms.FolderBrowserDialog();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonCreateMail = new System.Windows.Forms.Button();
            this.buttonCopyStats = new System.Windows.Forms.Button();
            this.buttonSetPrimary = new System.Windows.Forms.Button();
            this.buttonDeleteFile = new System.Windows.Forms.Button();
            this.buttonDownloadFile = new System.Windows.Forms.Button();
            this.buttonOpenFile = new System.Windows.Forms.Button();
            this.listViewFiles = new System.Windows.Forms.ListView();
            this.ListViewFilesName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ListViewFilesSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.progressBarUpload = new System.Windows.Forms.ProgressBar();
            this.buttonUpload = new System.Windows.Forms.Button();
            this.buttonDuplicate = new System.Windows.Forms.Button();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.dataGridViewAutPol = new System.Windows.Forms.DataGridView();
            this.listViewAutPol = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.dataGridViewKeys = new System.Windows.Forms.DataGridView();
            this.listViewKeys = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.buttonRemovePol = new System.Windows.Forms.Button();
            this.DGDelPol = new System.Windows.Forms.DataGridView();
            this.listViewDelPol = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxStreamingEndpoint = new System.Windows.Forms.ComboBox();
            this.buttonOpen = new System.Windows.Forms.Button();
            this.buttonDel = new System.Windows.Forms.Button();
            this.buttonDashAzure = new System.Windows.Forms.Button();
            this.buttonSLMonitor = new System.Windows.Forms.Button();
            this.buttonHTML = new System.Windows.Forms.Button();
            this.buttonDASH = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonFlash = new System.Windows.Forms.Button();
            this.labelAssetNameTitle = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.buttonGetTestToken = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DGAsset)).BeginInit();
            this.contextMenuStripDG.SuspendLayout();
            this.contextMenuStripLocators.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGFiles)).BeginInit();
            this.contextMenuStripFiles.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAutPol)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewKeys)).BeginInit();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGDelPol)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // DGAsset
            // 
            this.DGAsset.AllowUserToAddRows = false;
            this.DGAsset.AllowUserToDeleteRows = false;
            this.DGAsset.AllowUserToResizeRows = false;
            this.DGAsset.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DGAsset.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DGAsset.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGAsset.ColumnHeadersVisible = false;
            this.DGAsset.ContextMenuStrip = this.contextMenuStripDG;
            this.DGAsset.Location = new System.Drawing.Point(6, 6);
            this.DGAsset.MultiSelect = false;
            this.DGAsset.Name = "DGAsset";
            this.DGAsset.ReadOnly = true;
            this.DGAsset.RowHeadersVisible = false;
            this.DGAsset.Size = new System.Drawing.Size(740, 409);
            this.DGAsset.TabIndex = 0;
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
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonClose.Location = new System.Drawing.Point(681, 526);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 3;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // contextMenuStripLocators
            // 
            this.contextMenuStripLocators.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemCopy,
            this.deleteLocatorToolStripMenuItem,
            this.toolStripMenuItemOpen,
            this.toolStripMenuItemPlaybackFlashAzure,
            this.toolStripMenuItemPlaybackSilverlightMonitoring,
            this.toolStripMenuItemDASHIF,
            this.toolStripMenuItemDASHAzure,
            this.toolStripMenuItemPlaybackMP4});
            this.contextMenuStripLocators.Name = "contextMenuStripLocators";
            this.contextMenuStripLocators.Size = new System.Drawing.Size(323, 180);
            this.contextMenuStripLocators.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripLocators_Opening);
            // 
            // toolStripMenuItemCopy
            // 
            this.toolStripMenuItemCopy.Name = "toolStripMenuItemCopy";
            this.toolStripMenuItemCopy.Size = new System.Drawing.Size(322, 22);
            this.toolStripMenuItemCopy.Text = "Copy to clipboard";
            this.toolStripMenuItemCopy.Click += new System.EventHandler(this.toolStripMenuItemCopy_Click);
            // 
            // deleteLocatorToolStripMenuItem
            // 
            this.deleteLocatorToolStripMenuItem.Name = "deleteLocatorToolStripMenuItem";
            this.deleteLocatorToolStripMenuItem.Size = new System.Drawing.Size(322, 22);
            this.deleteLocatorToolStripMenuItem.Text = "Delete locator";
            this.deleteLocatorToolStripMenuItem.Click += new System.EventHandler(this.deleteLocatorToolStripMenuItem_Click);
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
            // toolStripMenuItemDASHAzure
            // 
            this.toolStripMenuItemDASHAzure.Name = "toolStripMenuItemDASHAzure";
            this.toolStripMenuItemDASHAzure.Size = new System.Drawing.Size(322, 22);
            this.toolStripMenuItemDASHAzure.Text = "Playback with MPEG-DASH Azure Player";
            this.toolStripMenuItemDASHAzure.Click += new System.EventHandler(this.playbackWithToolStripMenuItem_Click);
            // 
            // toolStripMenuItemPlaybackMP4
            // 
            this.toolStripMenuItemPlaybackMP4.Enabled = false;
            this.toolStripMenuItemPlaybackMP4.Name = "toolStripMenuItemPlaybackMP4";
            this.toolStripMenuItemPlaybackMP4.Size = new System.Drawing.Size(322, 22);
            this.toolStripMenuItemPlaybackMP4.Text = "Playback with HTML Player (MP4)";
            this.toolStripMenuItemPlaybackMP4.Click += new System.EventHandler(this.toolStripMenuItemPlaybackMP4_Click);
            // 
            // TreeViewLocators
            // 
            this.TreeViewLocators.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TreeViewLocators.ContextMenuStrip = this.contextMenuStripLocators;
            this.TreeViewLocators.Location = new System.Drawing.Point(6, 31);
            this.TreeViewLocators.Name = "TreeViewLocators";
            this.TreeViewLocators.Size = new System.Drawing.Size(740, 384);
            this.TreeViewLocators.TabIndex = 19;
            this.TreeViewLocators.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeViewLocators_AfterSelect);
            // 
            // DGFiles
            // 
            this.DGFiles.AllowUserToAddRows = false;
            this.DGFiles.AllowUserToDeleteRows = false;
            this.DGFiles.AllowUserToResizeRows = false;
            this.DGFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DGFiles.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DGFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGFiles.ColumnHeadersVisible = false;
            this.DGFiles.ContextMenuStrip = this.contextMenuStripDG;
            this.DGFiles.Location = new System.Drawing.Point(330, 6);
            this.DGFiles.MultiSelect = false;
            this.DGFiles.Name = "DGFiles";
            this.DGFiles.ReadOnly = true;
            this.DGFiles.RowHeadersVisible = false;
            this.DGFiles.Size = new System.Drawing.Size(410, 409);
            this.DGFiles.TabIndex = 20;
            // 
            // contextMenuStripFiles
            // 
            this.contextMenuStripFiles.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.makeItPrimaryToolStripMenuItem,
            this.toolStripMenuItemOpenFile,
            this.toolStripMenuItemDownloadFile,
            this.deleteFileToolStripMenuItem,
            this.duplicateFileToolStripMenuItem,
            this.uploadASmallFileInTheAssetToolStripMenuItem});
            this.contextMenuStripFiles.Name = "contextMenuStripFiles";
            this.contextMenuStripFiles.Size = new System.Drawing.Size(234, 136);
            // 
            // makeItPrimaryToolStripMenuItem
            // 
            this.makeItPrimaryToolStripMenuItem.Name = "makeItPrimaryToolStripMenuItem";
            this.makeItPrimaryToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
            this.makeItPrimaryToolStripMenuItem.Text = "Set as Primary";
            this.makeItPrimaryToolStripMenuItem.Click += new System.EventHandler(this.makeItPrimaryToolStripMenuItem_Click);
            // 
            // toolStripMenuItemOpenFile
            // 
            this.toolStripMenuItemOpenFile.Name = "toolStripMenuItemOpenFile";
            this.toolStripMenuItemOpenFile.Size = new System.Drawing.Size(233, 22);
            this.toolStripMenuItemOpenFile.Text = "Open file";
            this.toolStripMenuItemOpenFile.Click += new System.EventHandler(this.toolStripMenuItemOpenFile_Click);
            // 
            // toolStripMenuItemDownloadFile
            // 
            this.toolStripMenuItemDownloadFile.Name = "toolStripMenuItemDownloadFile";
            this.toolStripMenuItemDownloadFile.Size = new System.Drawing.Size(233, 22);
            this.toolStripMenuItemDownloadFile.Text = "Download file to a local folder";
            this.toolStripMenuItemDownloadFile.Click += new System.EventHandler(this.toolStripMenuItemDownloadFile_Click);
            // 
            // deleteFileToolStripMenuItem
            // 
            this.deleteFileToolStripMenuItem.Name = "deleteFileToolStripMenuItem";
            this.deleteFileToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
            this.deleteFileToolStripMenuItem.Text = "Delete file";
            this.deleteFileToolStripMenuItem.Click += new System.EventHandler(this.deleteFileToolStripMenuItem_Click);
            // 
            // duplicateFileToolStripMenuItem
            // 
            this.duplicateFileToolStripMenuItem.Name = "duplicateFileToolStripMenuItem";
            this.duplicateFileToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
            this.duplicateFileToolStripMenuItem.Text = "Duplicate file";
            this.duplicateFileToolStripMenuItem.Click += new System.EventHandler(this.duplicateFileToolStripMenuItem_Click);
            // 
            // uploadASmallFileInTheAssetToolStripMenuItem
            // 
            this.uploadASmallFileInTheAssetToolStripMenuItem.Name = "uploadASmallFileInTheAssetToolStripMenuItem";
            this.uploadASmallFileInTheAssetToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
            this.uploadASmallFileInTheAssetToolStripMenuItem.Text = "Upload a small file in the asset";
            this.uploadASmallFileInTheAssetToolStripMenuItem.Click += new System.EventHandler(this.uploadASmallFileInTheAssetToolStripMenuItem_Click);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 426);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 26;
            this.label4.Text = "Asset report:";
            // 
            // buttonCreateMail
            // 
            this.buttonCreateMail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCreateMail.Location = new System.Drawing.Point(188, 421);
            this.buttonCreateMail.Name = "buttonCreateMail";
            this.buttonCreateMail.Size = new System.Drawing.Size(138, 23);
            this.buttonCreateMail.TabIndex = 25;
            this.buttonCreateMail.Text = "Create new Outlook email";
            this.buttonCreateMail.UseVisualStyleBackColor = true;
            this.buttonCreateMail.Click += new System.EventHandler(this.buttonCreateMail_Click);
            // 
            // buttonCopyStats
            // 
            this.buttonCopyStats.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCopyStats.Location = new System.Drawing.Point(78, 421);
            this.buttonCopyStats.Name = "buttonCopyStats";
            this.buttonCopyStats.Size = new System.Drawing.Size(104, 23);
            this.buttonCopyStats.TabIndex = 24;
            this.buttonCopyStats.Text = "Copy to clipboard";
            this.buttonCopyStats.UseVisualStyleBackColor = true;
            this.buttonCopyStats.Click += new System.EventHandler(this.buttonCopyStats_Click);
            // 
            // buttonSetPrimary
            // 
            this.buttonSetPrimary.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSetPrimary.Enabled = false;
            this.buttonSetPrimary.Location = new System.Drawing.Point(9, 421);
            this.buttonSetPrimary.Name = "buttonSetPrimary";
            this.buttonSetPrimary.Size = new System.Drawing.Size(88, 23);
            this.buttonSetPrimary.TabIndex = 27;
            this.buttonSetPrimary.Text = "Set as Primary";
            this.buttonSetPrimary.UseVisualStyleBackColor = true;
            this.buttonSetPrimary.Click += new System.EventHandler(this.buttonSetPrimary_Click);
            // 
            // buttonDeleteFile
            // 
            this.buttonDeleteFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDeleteFile.Enabled = false;
            this.buttonDeleteFile.Location = new System.Drawing.Point(231, 421);
            this.buttonDeleteFile.Name = "buttonDeleteFile";
            this.buttonDeleteFile.Size = new System.Drawing.Size(54, 23);
            this.buttonDeleteFile.TabIndex = 28;
            this.buttonDeleteFile.Text = "Delete";
            this.toolTip1.SetToolTip(this.buttonDeleteFile, "Delete the selected file");
            this.buttonDeleteFile.UseVisualStyleBackColor = true;
            this.buttonDeleteFile.Click += new System.EventHandler(this.buttonDeleteFile_Click);
            // 
            // buttonDownloadFile
            // 
            this.buttonDownloadFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDownloadFile.Enabled = false;
            this.buttonDownloadFile.Location = new System.Drawing.Point(154, 421);
            this.buttonDownloadFile.Name = "buttonDownloadFile";
            this.buttonDownloadFile.Size = new System.Drawing.Size(71, 23);
            this.buttonDownloadFile.TabIndex = 29;
            this.buttonDownloadFile.Text = "Download";
            this.buttonDownloadFile.UseVisualStyleBackColor = true;
            this.buttonDownloadFile.Click += new System.EventHandler(this.buttonDownloadFile_Click);
            // 
            // buttonOpenFile
            // 
            this.buttonOpenFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonOpenFile.Enabled = false;
            this.buttonOpenFile.Location = new System.Drawing.Point(103, 421);
            this.buttonOpenFile.Name = "buttonOpenFile";
            this.buttonOpenFile.Size = new System.Drawing.Size(45, 23);
            this.buttonOpenFile.TabIndex = 30;
            this.buttonOpenFile.Text = "Open";
            this.toolTip1.SetToolTip(this.buttonOpenFile, "Open the selected file in a browser");
            this.buttonOpenFile.UseVisualStyleBackColor = true;
            this.buttonOpenFile.Click += new System.EventHandler(this.buttonOpenFile_Click);
            // 
            // listViewFiles
            // 
            this.listViewFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ListViewFilesName,
            this.ListViewFilesSize});
            this.listViewFiles.ContextMenuStrip = this.contextMenuStripFiles;
            this.listViewFiles.FullRowSelect = true;
            this.listViewFiles.HideSelection = false;
            this.listViewFiles.Location = new System.Drawing.Point(9, 6);
            this.listViewFiles.MultiSelect = false;
            this.listViewFiles.Name = "listViewFiles";
            this.listViewFiles.Size = new System.Drawing.Size(315, 409);
            this.listViewFiles.TabIndex = 31;
            this.listViewFiles.UseCompatibleStateImageBehavior = false;
            this.listViewFiles.View = System.Windows.Forms.View.Details;
            this.listViewFiles.SelectedIndexChanged += new System.EventHandler(this.listViewFiles_SelectedIndexChanged);
            // 
            // ListViewFilesName
            // 
            this.ListViewFilesName.Text = "Name";
            this.ListViewFilesName.Width = 25;
            // 
            // ListViewFilesSize
            // 
            this.ListViewFilesSize.Text = "Size";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 44);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(760, 476);
            this.tabControl1.TabIndex = 34;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.DGAsset);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.buttonCopyStats);
            this.tabPage1.Controls.Add(this.buttonCreateMail);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(752, 450);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Asset information";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.progressBarUpload);
            this.tabPage2.Controls.Add(this.buttonUpload);
            this.tabPage2.Controls.Add(this.buttonDuplicate);
            this.tabPage2.Controls.Add(this.buttonDeleteFile);
            this.tabPage2.Controls.Add(this.buttonOpenFile);
            this.tabPage2.Controls.Add(this.buttonDownloadFile);
            this.tabPage2.Controls.Add(this.buttonSetPrimary);
            this.tabPage2.Controls.Add(this.DGFiles);
            this.tabPage2.Controls.Add(this.listViewFiles);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(752, 450);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Asset Files";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // progressBarUpload
            // 
            this.progressBarUpload.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarUpload.Location = new System.Drawing.Point(441, 420);
            this.progressBarUpload.Name = "progressBarUpload";
            this.progressBarUpload.Size = new System.Drawing.Size(299, 23);
            this.progressBarUpload.TabIndex = 34;
            this.progressBarUpload.Visible = false;
            // 
            // buttonUpload
            // 
            this.buttonUpload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonUpload.Enabled = false;
            this.buttonUpload.Location = new System.Drawing.Point(366, 421);
            this.buttonUpload.Name = "buttonUpload";
            this.buttonUpload.Size = new System.Drawing.Size(69, 23);
            this.buttonUpload.TabIndex = 33;
            this.buttonUpload.Text = "Upload";
            this.toolTip1.SetToolTip(this.buttonUpload, "Upload file(s) to the asset. Use for small file(s) only.");
            this.buttonUpload.UseVisualStyleBackColor = true;
            this.buttonUpload.Click += new System.EventHandler(this.button2_Click);
            // 
            // buttonDuplicate
            // 
            this.buttonDuplicate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDuplicate.Enabled = false;
            this.buttonDuplicate.Location = new System.Drawing.Point(291, 421);
            this.buttonDuplicate.Name = "buttonDuplicate";
            this.buttonDuplicate.Size = new System.Drawing.Size(69, 23);
            this.buttonDuplicate.TabIndex = 32;
            this.buttonDuplicate.Text = "Duplicate";
            this.toolTip1.SetToolTip(this.buttonDuplicate, "Duplicate the selected file");
            this.buttonDuplicate.UseVisualStyleBackColor = true;
            this.buttonDuplicate.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.buttonGetTestToken);
            this.tabPage5.Controls.Add(this.label3);
            this.tabPage5.Controls.Add(this.dataGridViewAutPol);
            this.tabPage5.Controls.Add(this.listViewAutPol);
            this.tabPage5.Controls.Add(this.dataGridViewKeys);
            this.tabPage5.Controls.Add(this.listViewKeys);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(752, 450);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Content keys";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(233, 236);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 13);
            this.label3.TabIndex = 40;
            this.label3.Text = "Authorization policies";
            // 
            // dataGridViewAutPol
            // 
            this.dataGridViewAutPol.AllowUserToAddRows = false;
            this.dataGridViewAutPol.AllowUserToDeleteRows = false;
            this.dataGridViewAutPol.AllowUserToResizeRows = false;
            this.dataGridViewAutPol.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewAutPol.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewAutPol.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewAutPol.ColumnHeadersVisible = false;
            this.dataGridViewAutPol.ContextMenuStrip = this.contextMenuStripDG;
            this.dataGridViewAutPol.Location = new System.Drawing.Point(461, 252);
            this.dataGridViewAutPol.MultiSelect = false;
            this.dataGridViewAutPol.Name = "dataGridViewAutPol";
            this.dataGridViewAutPol.ReadOnly = true;
            this.dataGridViewAutPol.RowHeadersVisible = false;
            this.dataGridViewAutPol.Size = new System.Drawing.Size(281, 192);
            this.dataGridViewAutPol.TabIndex = 38;
            // 
            // listViewAutPol
            // 
            this.listViewAutPol.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listViewAutPol.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3});
            this.listViewAutPol.FullRowSelect = true;
            this.listViewAutPol.HideSelection = false;
            this.listViewAutPol.Location = new System.Drawing.Point(236, 252);
            this.listViewAutPol.MultiSelect = false;
            this.listViewAutPol.Name = "listViewAutPol";
            this.listViewAutPol.Size = new System.Drawing.Size(219, 192);
            this.listViewAutPol.TabIndex = 39;
            this.listViewAutPol.UseCompatibleStateImageBehavior = false;
            this.listViewAutPol.View = System.Windows.Forms.View.Details;
            this.listViewAutPol.SelectedIndexChanged += new System.EventHandler(this.listViewAutPol_SelectedIndexChanged);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Name";
            this.columnHeader3.Width = 25;
            // 
            // dataGridViewKeys
            // 
            this.dataGridViewKeys.AllowUserToAddRows = false;
            this.dataGridViewKeys.AllowUserToDeleteRows = false;
            this.dataGridViewKeys.AllowUserToResizeRows = false;
            this.dataGridViewKeys.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewKeys.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewKeys.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewKeys.ColumnHeadersVisible = false;
            this.dataGridViewKeys.ContextMenuStrip = this.contextMenuStripDG;
            this.dataGridViewKeys.Location = new System.Drawing.Point(236, 6);
            this.dataGridViewKeys.MultiSelect = false;
            this.dataGridViewKeys.Name = "dataGridViewKeys";
            this.dataGridViewKeys.ReadOnly = true;
            this.dataGridViewKeys.RowHeadersVisible = false;
            this.dataGridViewKeys.Size = new System.Drawing.Size(506, 224);
            this.dataGridViewKeys.TabIndex = 35;
            // 
            // listViewKeys
            // 
            this.listViewKeys.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.listViewKeys.FullRowSelect = true;
            this.listViewKeys.HideSelection = false;
            this.listViewKeys.Location = new System.Drawing.Point(11, 6);
            this.listViewKeys.MultiSelect = false;
            this.listViewKeys.Name = "listViewKeys";
            this.listViewKeys.Size = new System.Drawing.Size(219, 224);
            this.listViewKeys.TabIndex = 36;
            this.listViewKeys.UseCompatibleStateImageBehavior = false;
            this.listViewKeys.View = System.Windows.Forms.View.Details;
            this.listViewKeys.SelectedIndexChanged += new System.EventHandler(this.listViewKeys_SelectedIndexChanged);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Name";
            this.columnHeader2.Width = 25;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.buttonRemovePol);
            this.tabPage4.Controls.Add(this.DGDelPol);
            this.tabPage4.Controls.Add(this.listViewDelPol);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(752, 450);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Delivery policies";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // buttonRemovePol
            // 
            this.buttonRemovePol.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonRemovePol.Enabled = false;
            this.buttonRemovePol.Location = new System.Drawing.Point(9, 421);
            this.buttonRemovePol.Name = "buttonRemovePol";
            this.buttonRemovePol.Size = new System.Drawing.Size(60, 23);
            this.buttonRemovePol.TabIndex = 34;
            this.buttonRemovePol.Text = "Remove";
            this.toolTip1.SetToolTip(this.buttonRemovePol, "Delete the selected file");
            this.buttonRemovePol.UseVisualStyleBackColor = true;
            this.buttonRemovePol.Click += new System.EventHandler(this.buttonRemovePol_Click);
            // 
            // DGDelPol
            // 
            this.DGDelPol.AllowUserToAddRows = false;
            this.DGDelPol.AllowUserToDeleteRows = false;
            this.DGDelPol.AllowUserToResizeRows = false;
            this.DGDelPol.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DGDelPol.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DGDelPol.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGDelPol.ColumnHeadersVisible = false;
            this.DGDelPol.ContextMenuStrip = this.contextMenuStripDG;
            this.DGDelPol.Location = new System.Drawing.Point(330, 6);
            this.DGDelPol.MultiSelect = false;
            this.DGDelPol.Name = "DGDelPol";
            this.DGDelPol.ReadOnly = true;
            this.DGDelPol.RowHeadersVisible = false;
            this.DGDelPol.Size = new System.Drawing.Size(410, 409);
            this.DGDelPol.TabIndex = 32;
            this.DGDelPol.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // listViewDelPol
            // 
            this.listViewDelPol.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listViewDelPol.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listViewDelPol.FullRowSelect = true;
            this.listViewDelPol.HideSelection = false;
            this.listViewDelPol.Location = new System.Drawing.Point(9, 6);
            this.listViewDelPol.MultiSelect = false;
            this.listViewDelPol.Name = "listViewDelPol";
            this.listViewDelPol.Size = new System.Drawing.Size(315, 409);
            this.listViewDelPol.TabIndex = 33;
            this.listViewDelPol.UseCompatibleStateImageBehavior = false;
            this.listViewDelPol.View = System.Windows.Forms.View.Details;
            this.listViewDelPol.SelectedIndexChanged += new System.EventHandler(this.listViewDelPol_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 25;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.label2);
            this.tabPage3.Controls.Add(this.comboBoxStreamingEndpoint);
            this.tabPage3.Controls.Add(this.buttonOpen);
            this.tabPage3.Controls.Add(this.buttonDel);
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
            this.tabPage3.Size = new System.Drawing.Size(752, 450);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Locators";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 13);
            this.label2.TabIndex = 30;
            this.label2.Text = "Streaming endpoint :";
            // 
            // comboBoxStreamingEndpoint
            // 
            this.comboBoxStreamingEndpoint.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStreamingEndpoint.FormattingEnabled = true;
            this.comboBoxStreamingEndpoint.Location = new System.Drawing.Point(117, 4);
            this.comboBoxStreamingEndpoint.Name = "comboBoxStreamingEndpoint";
            this.comboBoxStreamingEndpoint.Size = new System.Drawing.Size(121, 21);
            this.comboBoxStreamingEndpoint.TabIndex = 29;
            this.comboBoxStreamingEndpoint.SelectedIndexChanged += new System.EventHandler(this.comboBoxStreamingEndpoint_SelectedIndexChanged);
            // 
            // buttonOpen
            // 
            this.buttonOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonOpen.Enabled = false;
            this.buttonOpen.Location = new System.Drawing.Point(69, 421);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(50, 23);
            this.buttonOpen.TabIndex = 25;
            this.buttonOpen.Text = "Open";
            this.buttonOpen.UseVisualStyleBackColor = true;
            this.buttonOpen.Click += new System.EventHandler(this.buttonOpen_Click);
            // 
            // buttonDel
            // 
            this.buttonDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDel.Enabled = false;
            this.buttonDel.Location = new System.Drawing.Point(3, 421);
            this.buttonDel.Name = "buttonDel";
            this.buttonDel.Size = new System.Drawing.Size(57, 23);
            this.buttonDel.TabIndex = 28;
            this.buttonDel.Text = "Delete";
            this.buttonDel.UseVisualStyleBackColor = true;
            this.buttonDel.Click += new System.EventHandler(this.buttonDel_Click);
            // 
            // buttonDashAzure
            // 
            this.buttonDashAzure.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDashAzure.Enabled = false;
            this.buttonDashAzure.Location = new System.Drawing.Point(517, 421);
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
            this.buttonSLMonitor.Location = new System.Drawing.Point(290, 421);
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
            this.buttonHTML.Location = new System.Drawing.Point(635, 421);
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
            this.buttonDASH.Location = new System.Drawing.Point(399, 421);
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
            this.label1.Location = new System.Drawing.Point(125, 426);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "Playback with:";
            // 
            // buttonFlash
            // 
            this.buttonFlash.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonFlash.Enabled = false;
            this.buttonFlash.Location = new System.Drawing.Point(207, 421);
            this.buttonFlash.Name = "buttonFlash";
            this.buttonFlash.Size = new System.Drawing.Size(77, 23);
            this.buttonFlash.TabIndex = 20;
            this.buttonFlash.Text = "Flash OSMF";
            this.buttonFlash.UseVisualStyleBackColor = true;
            this.buttonFlash.Click += new System.EventHandler(this.buttonFlash_Click);
            // 
            // labelAssetNameTitle
            // 
            this.labelAssetNameTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelAssetNameTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAssetNameTitle.Location = new System.Drawing.Point(18, 9);
            this.labelAssetNameTitle.Name = "labelAssetNameTitle";
            this.labelAssetNameTitle.Size = new System.Drawing.Size(753, 32);
            this.labelAssetNameTitle.TabIndex = 35;
            this.labelAssetNameTitle.Text = "Asset : ";
            // 
            // buttonGetTestToken
            // 
            this.buttonGetTestToken.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonGetTestToken.Location = new System.Drawing.Point(126, 421);
            this.buttonGetTestToken.Name = "buttonGetTestToken";
            this.buttonGetTestToken.Size = new System.Drawing.Size(104, 23);
            this.buttonGetTestToken.TabIndex = 41;
            this.buttonGetTestToken.Text = "Get Test Token";
            this.buttonGetTestToken.UseVisualStyleBackColor = true;
            this.buttonGetTestToken.Click += new System.EventHandler(this.buttonGetTestToken_Click);
            // 
            // AssetInformation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.labelAssetNameTitle);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.buttonClose);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AssetInformation";
            this.Text = "Asset Information";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AssetInformation_FormClosed);
            this.Load += new System.EventHandler(this.AssetInformation_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGAsset)).EndInit();
            this.contextMenuStripDG.ResumeLayout(false);
            this.contextMenuStripLocators.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGFiles)).EndInit();
            this.contextMenuStripFiles.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAutPol)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewKeys)).EndInit();
            this.tabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGDelPol)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView DGAsset;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripLocators;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCopy;
        private System.Windows.Forms.TreeView TreeViewLocators;
        private System.Windows.Forms.DataGridView DGFiles;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripDG;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFilesCopyClipboard;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemPlaybackFlashAzure;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemPlaybackSilverlightMonitoring;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDASHIF;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemPlaybackMP4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemOpen;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripFiles;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemOpenFile;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDownloadFile;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogDownload;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonCreateMail;
        private System.Windows.Forms.Button buttonCopyStats;
        private System.Windows.Forms.ToolStripMenuItem makeItPrimaryToolStripMenuItem;
        private System.Windows.Forms.Button buttonSetPrimary;
        private System.Windows.Forms.Button buttonDeleteFile;
        private System.Windows.Forms.ToolStripMenuItem deleteFileToolStripMenuItem;
        private System.Windows.Forms.Button buttonDownloadFile;
        private System.Windows.Forms.Button buttonOpenFile;
        private System.Windows.Forms.ListView listViewFiles;
        private System.Windows.Forms.ColumnHeader ListViewFilesName;
        private System.Windows.Forms.ColumnHeader ListViewFilesSize;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button buttonHTML;
        private System.Windows.Forms.Button buttonDASH;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonFlash;
        private System.Windows.Forms.Button buttonOpen;
        private System.Windows.Forms.Button buttonSLMonitor;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDASHAzure;
        private System.Windows.Forms.Button buttonDashAzure;
        private System.Windows.Forms.Button buttonDuplicate;
        private System.Windows.Forms.ToolStripMenuItem duplicateFileToolStripMenuItem;
        private System.Windows.Forms.Button buttonDel;
        private System.Windows.Forms.ToolStripMenuItem deleteLocatorToolStripMenuItem;
        private System.Windows.Forms.Label labelAssetNameTitle;
        private System.Windows.Forms.Button buttonUpload;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripMenuItem uploadASmallFileInTheAssetToolStripMenuItem;
        private System.Windows.Forms.ProgressBar progressBarUpload;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxStreamingEndpoint;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.DataGridView DGDelPol;
        private System.Windows.Forms.ListView listViewDelPol;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Button buttonRemovePol;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.DataGridView dataGridViewKeys;
        private System.Windows.Forms.ListView listViewKeys;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.DataGridView dataGridViewAutPol;
        private System.Windows.Forms.ListView listViewAutPol;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonGetTestToken;
    }
}