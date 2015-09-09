namespace AMSExplorer
{
    partial class ChannelAdSlateControl
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
            this.contextMenuStripDG = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemFilesCopyClipboard = new System.Windows.Forms.ToolStripMenuItem();
            this.folderBrowserDialogDownload = new System.Windows.Forms.FolderBrowserDialog();
            this.splitContainerBox = new System.Windows.Forms.SplitContainer();
            this.splitContainerPreviews = new System.Windows.Forms.SplitContainer();
            this.checkBoxPreviewStream = new System.Windows.Forms.CheckBox();
            this.webBrowserPreview2 = new System.Windows.Forms.WebBrowser();
            this.checkBoxPreviewSlate = new System.Windows.Forms.CheckBox();
            this.labelSlatePreviewInfo = new System.Windows.Forms.Label();
            this.pictureBoxPreviewSlate = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttongenerateContentKey = new System.Windows.Forms.Button();
            this.buttonInsertAD = new System.Windows.Forms.Button();
            this.buttonInsertAdAndSlate = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxADSignalDuration = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxCueId = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxJPGSearch = new System.Windows.Forms.TextBox();
            this.progressBarUpload = new System.Windows.Forms.ProgressBar();
            this.buttonUploadSlate = new System.Windows.Forms.Button();
            this.listViewJPG1 = new AMSExplorer.ListViewSlateJPG();
            this.buttonHideSlate = new System.Windows.Forms.Button();
            this.buttonShowSLate = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxSlateDuration = new System.Windows.Forms.TextBox();
            this.labelChannelName = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.openFileDialogSlate = new System.Windows.Forms.OpenFileDialog();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.contextMenuStripDG.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerBox)).BeginInit();
            this.splitContainerBox.Panel1.SuspendLayout();
            this.splitContainerBox.Panel2.SuspendLayout();
            this.splitContainerBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerPreviews)).BeginInit();
            this.splitContainerPreviews.Panel1.SuspendLayout();
            this.splitContainerPreviews.Panel2.SuspendLayout();
            this.splitContainerPreviews.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreviewSlate)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
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
            this.toolStripMenuItemFilesCopyClipboard.Click += new System.EventHandler(this.toolStripMenuItemFilesCopyClipboard_Click);
            // 
            // splitContainerBox
            // 
            this.splitContainerBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerBox.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.splitContainerBox.Location = new System.Drawing.Point(19, 37);
            this.splitContainerBox.Name = "splitContainerBox";
            // 
            // splitContainerBox.Panel1
            // 
            this.splitContainerBox.Panel1.BackColor = System.Drawing.SystemColors.Window;
            this.splitContainerBox.Panel1.Controls.Add(this.splitContainerPreviews);
            // 
            // splitContainerBox.Panel2
            // 
            this.splitContainerBox.Panel2.BackColor = System.Drawing.SystemColors.Window;
            this.splitContainerBox.Panel2.Controls.Add(this.groupBox1);
            this.splitContainerBox.Panel2.Controls.Add(this.groupBox2);
            this.splitContainerBox.Size = new System.Drawing.Size(882, 538);
            this.splitContainerBox.SplitterDistance = 291;
            this.splitContainerBox.SplitterWidth = 5;
            this.splitContainerBox.TabIndex = 3;
            // 
            // splitContainerPreviews
            // 
            this.splitContainerPreviews.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.splitContainerPreviews.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerPreviews.Location = new System.Drawing.Point(0, 0);
            this.splitContainerPreviews.Name = "splitContainerPreviews";
            this.splitContainerPreviews.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerPreviews.Panel1
            // 
            this.splitContainerPreviews.Panel1.BackColor = System.Drawing.SystemColors.Window;
            this.splitContainerPreviews.Panel1.Controls.Add(this.checkBoxPreviewStream);
            this.splitContainerPreviews.Panel1.Controls.Add(this.webBrowserPreview2);
            this.splitContainerPreviews.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer2_Panel1_Paint);
            // 
            // splitContainerPreviews.Panel2
            // 
            this.splitContainerPreviews.Panel2.BackColor = System.Drawing.SystemColors.Window;
            this.splitContainerPreviews.Panel2.Controls.Add(this.checkBoxPreviewSlate);
            this.splitContainerPreviews.Panel2.Controls.Add(this.labelSlatePreviewInfo);
            this.splitContainerPreviews.Panel2.Controls.Add(this.pictureBoxPreviewSlate);
            this.splitContainerPreviews.Size = new System.Drawing.Size(291, 538);
            this.splitContainerPreviews.SplitterDistance = 326;
            this.splitContainerPreviews.SplitterWidth = 5;
            this.splitContainerPreviews.TabIndex = 61;
            // 
            // checkBoxPreviewStream
            // 
            this.checkBoxPreviewStream.AutoSize = true;
            this.checkBoxPreviewStream.Location = new System.Drawing.Point(7, 3);
            this.checkBoxPreviewStream.Name = "checkBoxPreviewStream";
            this.checkBoxPreviewStream.Size = new System.Drawing.Size(147, 19);
            this.checkBoxPreviewStream.TabIndex = 3;
            this.checkBoxPreviewStream.Text = "Display preview stream";
            this.checkBoxPreviewStream.UseVisualStyleBackColor = true;
            this.checkBoxPreviewStream.CheckedChanged += new System.EventHandler(this.checkBoxPreview_CheckedChanged);
            // 
            // webBrowserPreview2
            // 
            this.webBrowserPreview2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowserPreview2.Location = new System.Drawing.Point(7, 30);
            this.webBrowserPreview2.MinimumSize = new System.Drawing.Size(23, 23);
            this.webBrowserPreview2.Name = "webBrowserPreview2";
            this.webBrowserPreview2.Size = new System.Drawing.Size(280, 292);
            this.webBrowserPreview2.TabIndex = 2;
            this.webBrowserPreview2.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowserPreview2_DocumentCompleted);
            // 
            // checkBoxPreviewSlate
            // 
            this.checkBoxPreviewSlate.AutoSize = true;
            this.checkBoxPreviewSlate.Location = new System.Drawing.Point(7, 3);
            this.checkBoxPreviewSlate.Name = "checkBoxPreviewSlate";
            this.checkBoxPreviewSlate.Size = new System.Drawing.Size(137, 19);
            this.checkBoxPreviewSlate.TabIndex = 5;
            this.checkBoxPreviewSlate.Text = "Display selected slate";
            this.checkBoxPreviewSlate.UseVisualStyleBackColor = true;
            this.checkBoxPreviewSlate.CheckedChanged += new System.EventHandler(this.checkBoxPreviewSlate_CheckedChanged);
            // 
            // labelSlatePreviewInfo
            // 
            this.labelSlatePreviewInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelSlatePreviewInfo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelSlatePreviewInfo.Location = new System.Drawing.Point(3, 186);
            this.labelSlatePreviewInfo.Name = "labelSlatePreviewInfo";
            this.labelSlatePreviewInfo.Size = new System.Drawing.Size(288, 16);
            this.labelSlatePreviewInfo.TabIndex = 60;
            this.labelSlatePreviewInfo.Text = "Resolution : {0}x{1}, Aspect Ratio : {2:0.000}";
            this.labelSlatePreviewInfo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.labelSlatePreviewInfo.Visible = false;
            // 
            // pictureBoxPreviewSlate
            // 
            this.pictureBoxPreviewSlate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxPreviewSlate.Location = new System.Drawing.Point(7, 23);
            this.pictureBoxPreviewSlate.Name = "pictureBoxPreviewSlate";
            this.pictureBoxPreviewSlate.Size = new System.Drawing.Size(280, 159);
            this.pictureBoxPreviewSlate.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxPreviewSlate.TabIndex = 4;
            this.pictureBoxPreviewSlate.TabStop = false;
            this.pictureBoxPreviewSlate.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.buttongenerateContentKey);
            this.groupBox1.Controls.Add(this.buttonInsertAD);
            this.groupBox1.Controls.Add(this.buttonInsertAdAndSlate);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBoxADSignalDuration);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBoxCueId);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(9, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(567, 145);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Advertising";
            // 
            // buttongenerateContentKey
            // 
            this.buttongenerateContentKey.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttongenerateContentKey.Location = new System.Drawing.Point(175, 44);
            this.buttongenerateContentKey.Name = "buttongenerateContentKey";
            this.buttongenerateContentKey.Size = new System.Drawing.Size(76, 27);
            this.buttongenerateContentKey.TabIndex = 59;
            this.buttongenerateContentKey.Text = "Generate";
            this.toolTip1.SetToolTip(this.buttongenerateContentKey, "Generates another random integer");
            this.buttongenerateContentKey.UseVisualStyleBackColor = true;
            this.buttongenerateContentKey.Click += new System.EventHandler(this.buttongenerateContentKey_Click);
            // 
            // buttonInsertAD
            // 
            this.buttonInsertAD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonInsertAD.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonInsertAD.Image = global::AMSExplorer.Bitmaps.create;
            this.buttonInsertAD.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonInsertAD.Location = new System.Drawing.Point(337, 67);
            this.buttonInsertAD.Name = "buttonInsertAD";
            this.buttonInsertAD.Size = new System.Drawing.Size(212, 27);
            this.buttonInsertAD.TabIndex = 57;
            this.buttonInsertAD.Text = "Insert AD";
            this.toolTip1.SetToolTip(this.buttonInsertAD, "Insert an ad in the live stream");
            this.buttonInsertAD.UseVisualStyleBackColor = true;
            this.buttonInsertAD.Click += new System.EventHandler(this.buttonInsertAD_Click);
            // 
            // buttonInsertAdAndSlate
            // 
            this.buttonInsertAdAndSlate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonInsertAdAndSlate.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonInsertAdAndSlate.Image = global::AMSExplorer.Bitmaps.thumbnails;
            this.buttonInsertAdAndSlate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonInsertAdAndSlate.Location = new System.Drawing.Point(337, 100);
            this.buttonInsertAdAndSlate.Name = "buttonInsertAdAndSlate";
            this.buttonInsertAdAndSlate.Size = new System.Drawing.Size(212, 27);
            this.buttonInsertAdAndSlate.TabIndex = 56;
            this.buttonInsertAdAndSlate.Text = "Insert AD and default Slate";
            this.toolTip1.SetToolTip(this.buttonInsertAdAndSlate, "Insert an ad and the default slate as defined in the channel (asset in blue)");
            this.buttonInsertAdAndSlate.UseVisualStyleBackColor = true;
            this.buttonInsertAdAndSlate.Click += new System.EventHandler(this.buttonInsertAdAndSlate_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(15, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 15);
            this.label3.TabIndex = 55;
            this.label3.Text = "AD Signal duration (s) :";
            // 
            // textBoxADSignalDuration
            // 
            this.textBoxADSignalDuration.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxADSignalDuration.Location = new System.Drawing.Point(15, 104);
            this.textBoxADSignalDuration.Name = "textBoxADSignalDuration";
            this.textBoxADSignalDuration.Size = new System.Drawing.Size(156, 23);
            this.textBoxADSignalDuration.TabIndex = 54;
            this.textBoxADSignalDuration.Text = "30";
            this.toolTip1.SetToolTip(this.textBoxADSignalDuration, "Ad duration in seconds (decimal is supported)");
            this.textBoxADSignalDuration.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxADSignalDuration_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(15, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 15);
            this.label1.TabIndex = 53;
            this.label1.Text = "Cue ID :";
            // 
            // textBoxCueId
            // 
            this.textBoxCueId.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxCueId.Location = new System.Drawing.Point(15, 46);
            this.textBoxCueId.Name = "textBoxCueId";
            this.textBoxCueId.Size = new System.Drawing.Size(152, 23);
            this.textBoxCueId.TabIndex = 52;
            this.toolTip1.SetToolTip(this.textBoxCueId, "An integer that identifies the ad");
            this.textBoxCueId.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxCueId_Validating);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.textBoxJPGSearch);
            this.groupBox2.Controls.Add(this.progressBarUpload);
            this.groupBox2.Controls.Add(this.buttonUploadSlate);
            this.groupBox2.Controls.Add(this.listViewJPG1);
            this.groupBox2.Controls.Add(this.buttonHideSlate);
            this.groupBox2.Controls.Add(this.buttonShowSLate);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.textBoxSlateDuration);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.groupBox2.Location = new System.Drawing.Point(9, 167);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(567, 367);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Slate";
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label15.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label15.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label15.Location = new System.Drawing.Point(28, 265);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(521, 15);
            this.label15.TabIndex = 84;
            this.label15.Text = "Requirements : JPG file, 16:9 aspect ratio, 1920x1080 max and 3 MB max";
            this.label15.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label8.Location = new System.Drawing.Point(12, 239);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(121, 15);
            this.label8.TabIndex = 83;
            this.label8.Text = "Search in name or Id :";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // textBoxJPGSearch
            // 
            this.textBoxJPGSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxJPGSearch.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBoxJPGSearch.Location = new System.Drawing.Point(145, 235);
            this.textBoxJPGSearch.Name = "textBoxJPGSearch";
            this.textBoxJPGSearch.Size = new System.Drawing.Size(185, 23);
            this.textBoxJPGSearch.TabIndex = 82;
            this.textBoxJPGSearch.TextChanged += new System.EventHandler(this.textBoxJPGSearch_TextChanged);
            // 
            // progressBarUpload
            // 
            this.progressBarUpload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarUpload.Location = new System.Drawing.Point(337, 235);
            this.progressBarUpload.Name = "progressBarUpload";
            this.progressBarUpload.Size = new System.Drawing.Size(98, 27);
            this.progressBarUpload.TabIndex = 81;
            this.progressBarUpload.Visible = false;
            this.progressBarUpload.Click += new System.EventHandler(this.progressBarUpload_Click);
            // 
            // buttonUploadSlate
            // 
            this.buttonUploadSlate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonUploadSlate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.buttonUploadSlate.Location = new System.Drawing.Point(442, 235);
            this.buttonUploadSlate.Name = "buttonUploadSlate";
            this.buttonUploadSlate.Size = new System.Drawing.Size(107, 27);
            this.buttonUploadSlate.TabIndex = 80;
            this.buttonUploadSlate.Text = "Upload a file...";
            this.toolTip1.SetToolTip(this.buttonUploadSlate, "Upload a new slate image");
            this.buttonUploadSlate.UseVisualStyleBackColor = true;
            this.buttonUploadSlate.Click += new System.EventHandler(this.buttonUploadSlate_Click);
            // 
            // listViewJPG1
            // 
            this.listViewJPG1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewJPG1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.listViewJPG1.FullRowSelect = true;
            this.listViewJPG1.HideSelection = false;
            this.listViewJPG1.Location = new System.Drawing.Point(15, 28);
            this.listViewJPG1.MultiSelect = false;
            this.listViewJPG1.Name = "listViewJPG1";
            this.listViewJPG1.Size = new System.Drawing.Size(534, 200);
            this.listViewJPG1.TabIndex = 61;
            this.listViewJPG1.Tag = -1;
            this.listViewJPG1.UseCompatibleStateImageBehavior = false;
            this.listViewJPG1.View = System.Windows.Forms.View.Details;
            this.listViewJPG1.SelectedIndexChanged += new System.EventHandler(this.listViewJPG1_SelectedIndexChanged);
            // 
            // buttonHideSlate
            // 
            this.buttonHideSlate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonHideSlate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.buttonHideSlate.Image = global::AMSExplorer.Bitmaps.cancel;
            this.buttonHideSlate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonHideSlate.Location = new System.Drawing.Point(337, 333);
            this.buttonHideSlate.Name = "buttonHideSlate";
            this.buttonHideSlate.Size = new System.Drawing.Size(212, 27);
            this.buttonHideSlate.TabIndex = 59;
            this.buttonHideSlate.Text = "Hide Slate";
            this.toolTip1.SetToolTip(this.buttonHideSlate, "Hide the current slate now");
            this.buttonHideSlate.UseVisualStyleBackColor = true;
            this.buttonHideSlate.Click += new System.EventHandler(this.buttonHideSlate_Click);
            // 
            // buttonShowSLate
            // 
            this.buttonShowSLate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonShowSLate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.buttonShowSLate.Image = global::AMSExplorer.Bitmaps.thumbnails;
            this.buttonShowSLate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonShowSLate.Location = new System.Drawing.Point(337, 300);
            this.buttonShowSLate.Name = "buttonShowSLate";
            this.buttonShowSLate.Size = new System.Drawing.Size(212, 27);
            this.buttonShowSLate.TabIndex = 58;
            this.buttonShowSLate.Text = "Show Slate";
            this.toolTip1.SetToolTip(this.buttonShowSLate, "Show the selected slate now");
            this.buttonShowSLate.UseVisualStyleBackColor = true;
            this.buttonShowSLate.Click += new System.EventHandler(this.buttonShowSLate_Click);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label4.Location = new System.Drawing.Point(15, 318);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 15);
            this.label4.TabIndex = 59;
            this.label4.Text = "Slate duration (s) :";
            // 
            // textBoxSlateDuration
            // 
            this.textBoxSlateDuration.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxSlateDuration.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBoxSlateDuration.Location = new System.Drawing.Point(15, 337);
            this.textBoxSlateDuration.Name = "textBoxSlateDuration";
            this.textBoxSlateDuration.Size = new System.Drawing.Size(140, 23);
            this.textBoxSlateDuration.TabIndex = 58;
            this.textBoxSlateDuration.Text = "30";
            this.toolTip1.SetToolTip(this.textBoxSlateDuration, "Slate duration in seconds (decimal is supported)");
            this.textBoxSlateDuration.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxADSignalDuration_Validating);
            // 
            // labelChannelName
            // 
            this.labelChannelName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelChannelName.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelChannelName.Location = new System.Drawing.Point(21, 10);
            this.labelChannelName.Name = "labelChannelName";
            this.labelChannelName.Size = new System.Drawing.Size(868, 23);
            this.labelChannelName.TabIndex = 37;
            this.labelChannelName.Text = "Channel : ";
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonClose.Location = new System.Drawing.Point(765, 12);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(124, 27);
            this.buttonClose.TabIndex = 41;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonDisregard_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonClose);
            this.panel1.Location = new System.Drawing.Point(-2, 595);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(922, 55);
            this.panel1.TabIndex = 58;
            // 
            // openFileDialogSlate
            // 
            this.openFileDialogSlate.Filter = "Image|*.jpg|All files (*.*)|*.*";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // ChannelAdSlateControl
            // 
            this.AcceptButton = this.buttonClose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonClose;
            this.ClientSize = new System.Drawing.Size(915, 647);
            this.Controls.Add(this.splitContainerBox);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labelChannelName);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "ChannelAdSlateControl";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Advertising and Slate Control";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ChannelAdSlateControl_FormClosed);
            this.Load += new System.EventHandler(this.ChannelAdSlateControl_Load);
            this.Shown += new System.EventHandler(this.ChannelInformation_Shown);
            this.contextMenuStripDG.ResumeLayout(false);
            this.splitContainerBox.Panel1.ResumeLayout(false);
            this.splitContainerBox.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerBox)).EndInit();
            this.splitContainerBox.ResumeLayout(false);
            this.splitContainerPreviews.Panel1.ResumeLayout(false);
            this.splitContainerPreviews.Panel1.PerformLayout();
            this.splitContainerPreviews.Panel2.ResumeLayout(false);
            this.splitContainerPreviews.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerPreviews)).EndInit();
            this.splitContainerPreviews.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreviewSlate)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStripDG;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFilesCopyClipboard;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogDownload;
        private System.Windows.Forms.Label labelChannelName;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxSlateDuration;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonInsertAD;
        private System.Windows.Forms.Button buttonInsertAdAndSlate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxADSignalDuration;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxCueId;
        private System.Windows.Forms.ProgressBar progressBarUpload;
        private System.Windows.Forms.Button buttonUploadSlate;
        private System.Windows.Forms.Button buttonHideSlate;
        private System.Windows.Forms.Button buttonShowSLate;
        private System.Windows.Forms.OpenFileDialog openFileDialogSlate;
        private ListViewSlateJPG listViewJPG1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxJPGSearch;
        private System.Windows.Forms.SplitContainer splitContainerBox;
        private System.Windows.Forms.CheckBox checkBoxPreviewStream;
        private System.Windows.Forms.WebBrowser webBrowserPreview2;
        private System.Windows.Forms.CheckBox checkBoxPreviewSlate;
        private System.Windows.Forms.PictureBox pictureBoxPreviewSlate;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label labelSlatePreviewInfo;
        private System.Windows.Forms.Button buttongenerateContentKey;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.SplitContainer splitContainerPreviews;
    }
}