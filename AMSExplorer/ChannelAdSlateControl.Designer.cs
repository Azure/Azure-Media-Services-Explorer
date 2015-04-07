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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.checkBoxPreviewSlate = new System.Windows.Forms.CheckBox();
            this.pictureBoxPreviewSlate = new System.Windows.Forms.PictureBox();
            this.checkBoxPreviewStream = new System.Windows.Forms.CheckBox();
            this.webBrowserPreview2 = new System.Windows.Forms.WebBrowser();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
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
            this.listViewJPG1 = new AMSExplorer.ListViewJPG();
            this.buttonHideSlate = new System.Windows.Forms.Button();
            this.buttonShowSLate = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxSlateDuration = new System.Windows.Forms.TextBox();
            this.labelChannelName = new System.Windows.Forms.Label();
            this.buttonDisregard = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.openFileDialogSlate = new System.Windows.Forms.OpenFileDialog();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.contextMenuStripDG.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
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
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(16, 32);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.checkBoxPreviewSlate);
            this.splitContainer1.Panel1.Controls.Add(this.pictureBoxPreviewSlate);
            this.splitContainer1.Panel1.Controls.Add(this.checkBoxPreviewStream);
            this.splitContainer1.Panel1.Controls.Add(this.webBrowserPreview2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Size = new System.Drawing.Size(756, 466);
            this.splitContainer1.SplitterDistance = 250;
            this.splitContainer1.TabIndex = 3;
            // 
            // checkBoxPreviewSlate
            // 
            this.checkBoxPreviewSlate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxPreviewSlate.AutoSize = true;
            this.checkBoxPreviewSlate.Location = new System.Drawing.Point(10, 303);
            this.checkBoxPreviewSlate.Name = "checkBoxPreviewSlate";
            this.checkBoxPreviewSlate.Size = new System.Drawing.Size(128, 17);
            this.checkBoxPreviewSlate.TabIndex = 5;
            this.checkBoxPreviewSlate.Text = "Display selected slate";
            this.checkBoxPreviewSlate.UseVisualStyleBackColor = true;
            // 
            // pictureBoxPreviewSlate
            // 
            this.pictureBoxPreviewSlate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxPreviewSlate.Location = new System.Drawing.Point(3, 326);
            this.pictureBoxPreviewSlate.Name = "pictureBoxPreviewSlate";
            this.pictureBoxPreviewSlate.Size = new System.Drawing.Size(241, 137);
            this.pictureBoxPreviewSlate.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxPreviewSlate.TabIndex = 4;
            this.pictureBoxPreviewSlate.TabStop = false;
            // 
            // checkBoxPreviewStream
            // 
            this.checkBoxPreviewStream.AutoSize = true;
            this.checkBoxPreviewStream.Location = new System.Drawing.Point(10, 6);
            this.checkBoxPreviewStream.Name = "checkBoxPreviewStream";
            this.checkBoxPreviewStream.Size = new System.Drawing.Size(134, 17);
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
            this.webBrowserPreview2.Location = new System.Drawing.Point(3, 29);
            this.webBrowserPreview2.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserPreview2.Name = "webBrowserPreview2";
            this.webBrowserPreview2.Size = new System.Drawing.Size(241, 254);
            this.webBrowserPreview2.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.buttonInsertAD);
            this.groupBox1.Controls.Add(this.buttonInsertAdAndSlate);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBoxADSignalDuration);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBoxCueId);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(487, 126);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Advertising";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label2.Location = new System.Drawing.Point(161, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 17);
            this.label2.TabIndex = 85;
            this.label2.Text = "(integer)";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonInsertAD
            // 
            this.buttonInsertAD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonInsertAD.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonInsertAD.Image = global::AMSExplorer.Bitmaps.create;
            this.buttonInsertAD.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonInsertAD.Location = new System.Drawing.Point(278, 58);
            this.buttonInsertAD.Name = "buttonInsertAD";
            this.buttonInsertAD.Size = new System.Drawing.Size(182, 23);
            this.buttonInsertAD.TabIndex = 57;
            this.buttonInsertAD.Text = "Insert AD";
            this.buttonInsertAD.UseVisualStyleBackColor = true;
            this.buttonInsertAD.Click += new System.EventHandler(this.buttonInsertAD_Click);
            // 
            // buttonInsertAdAndSlate
            // 
            this.buttonInsertAdAndSlate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonInsertAdAndSlate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonInsertAdAndSlate.Image = global::AMSExplorer.Bitmaps.thumbnails;
            this.buttonInsertAdAndSlate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonInsertAdAndSlate.Location = new System.Drawing.Point(278, 87);
            this.buttonInsertAdAndSlate.Name = "buttonInsertAdAndSlate";
            this.buttonInsertAdAndSlate.Size = new System.Drawing.Size(182, 23);
            this.buttonInsertAdAndSlate.TabIndex = 56;
            this.buttonInsertAdAndSlate.Text = "Insert AD and default Slate";
            this.buttonInsertAdAndSlate.UseVisualStyleBackColor = true;
            this.buttonInsertAdAndSlate.Click += new System.EventHandler(this.buttonInsertAdAndSlate_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(13, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 13);
            this.label3.TabIndex = 55;
            this.label3.Text = "AD Signal duration (s) :";
            // 
            // textBoxADSignalDuration
            // 
            this.textBoxADSignalDuration.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxADSignalDuration.Location = new System.Drawing.Point(13, 90);
            this.textBoxADSignalDuration.Name = "textBoxADSignalDuration";
            this.textBoxADSignalDuration.Size = new System.Drawing.Size(134, 20);
            this.textBoxADSignalDuration.TabIndex = 54;
            this.textBoxADSignalDuration.Text = "30";
            this.textBoxADSignalDuration.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxADSignalDuration_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 53;
            this.label1.Text = "Cue ID :";
            // 
            // textBoxCueId
            // 
            this.textBoxCueId.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxCueId.Location = new System.Drawing.Point(13, 40);
            this.textBoxCueId.Name = "textBoxCueId";
            this.textBoxCueId.Size = new System.Drawing.Size(131, 20);
            this.textBoxCueId.TabIndex = 52;
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
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(3, 145);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(487, 318);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Slate";
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label15.Location = new System.Drawing.Point(211, 230);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(249, 13);
            this.label15.TabIndex = 84;
            this.label15.Text = "Image should be a JPG 1920x1080";
            this.label15.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(10, 207);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(108, 13);
            this.label8.TabIndex = 83;
            this.label8.Text = "Search in name or Id:";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // textBoxJPGSearch
            // 
            this.textBoxJPGSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxJPGSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxJPGSearch.Location = new System.Drawing.Point(124, 204);
            this.textBoxJPGSearch.Name = "textBoxJPGSearch";
            this.textBoxJPGSearch.Size = new System.Drawing.Size(148, 20);
            this.textBoxJPGSearch.TabIndex = 82;
            this.textBoxJPGSearch.TextChanged += new System.EventHandler(this.textBoxJPGSearch_TextChanged);
            // 
            // progressBarUpload
            // 
            this.progressBarUpload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarUpload.Location = new System.Drawing.Point(278, 204);
            this.progressBarUpload.Name = "progressBarUpload";
            this.progressBarUpload.Size = new System.Drawing.Size(84, 23);
            this.progressBarUpload.TabIndex = 81;
            this.progressBarUpload.Visible = false;
            this.progressBarUpload.Click += new System.EventHandler(this.progressBarUpload_Click);
            // 
            // buttonUploadSlate
            // 
            this.buttonUploadSlate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonUploadSlate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonUploadSlate.Location = new System.Drawing.Point(368, 204);
            this.buttonUploadSlate.Name = "buttonUploadSlate";
            this.buttonUploadSlate.Size = new System.Drawing.Size(92, 23);
            this.buttonUploadSlate.TabIndex = 80;
            this.buttonUploadSlate.Text = "Upload a file...";
            this.buttonUploadSlate.UseVisualStyleBackColor = true;
            this.buttonUploadSlate.Click += new System.EventHandler(this.buttonUploadSlate_Click);
            // 
            // listViewJPG1
            // 
            this.listViewJPG1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewJPG1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewJPG1.FullRowSelect = true;
            this.listViewJPG1.HideSelection = false;
            this.listViewJPG1.Location = new System.Drawing.Point(13, 24);
            this.listViewJPG1.MultiSelect = false;
            this.listViewJPG1.Name = "listViewJPG1";
            this.listViewJPG1.Size = new System.Drawing.Size(447, 174);
            this.listViewJPG1.TabIndex = 61;
            this.listViewJPG1.Tag = -1;
            this.listViewJPG1.UseCompatibleStateImageBehavior = false;
            this.listViewJPG1.View = System.Windows.Forms.View.Details;
            this.listViewJPG1.SelectedIndexChanged += new System.EventHandler(this.listViewJPG1_SelectedIndexChanged);
            // 
            // buttonHideSlate
            // 
            this.buttonHideSlate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonHideSlate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonHideSlate.Image = global::AMSExplorer.Bitmaps.cancel;
            this.buttonHideSlate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonHideSlate.Location = new System.Drawing.Point(278, 289);
            this.buttonHideSlate.Name = "buttonHideSlate";
            this.buttonHideSlate.Size = new System.Drawing.Size(182, 23);
            this.buttonHideSlate.TabIndex = 59;
            this.buttonHideSlate.Text = "Hide Slate";
            this.buttonHideSlate.UseVisualStyleBackColor = true;
            this.buttonHideSlate.Click += new System.EventHandler(this.buttonHideSlate_Click);
            // 
            // buttonShowSLate
            // 
            this.buttonShowSLate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonShowSLate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonShowSLate.Image = global::AMSExplorer.Bitmaps.thumbnails;
            this.buttonShowSLate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonShowSLate.Location = new System.Drawing.Point(278, 260);
            this.buttonShowSLate.Name = "buttonShowSLate";
            this.buttonShowSLate.Size = new System.Drawing.Size(182, 23);
            this.buttonShowSLate.TabIndex = 58;
            this.buttonShowSLate.Text = "Show Slate";
            this.buttonShowSLate.UseVisualStyleBackColor = true;
            this.buttonShowSLate.Click += new System.EventHandler(this.buttonShowSLate_Click);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(13, 276);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 13);
            this.label4.TabIndex = 59;
            this.label4.Text = "Slate duration (s) :";
            // 
            // textBoxSlateDuration
            // 
            this.textBoxSlateDuration.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxSlateDuration.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxSlateDuration.Location = new System.Drawing.Point(13, 292);
            this.textBoxSlateDuration.Name = "textBoxSlateDuration";
            this.textBoxSlateDuration.Size = new System.Drawing.Size(121, 20);
            this.textBoxSlateDuration.TabIndex = 58;
            this.textBoxSlateDuration.Text = "30";
            this.textBoxSlateDuration.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxADSignalDuration_Validating);
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
            this.buttonDisregard.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonDisregard.Location = new System.Drawing.Point(656, 10);
            this.buttonDisregard.Name = "buttonDisregard";
            this.buttonDisregard.Size = new System.Drawing.Size(106, 23);
            this.buttonDisregard.TabIndex = 41;
            this.buttonDisregard.Text = "Close";
            this.buttonDisregard.UseVisualStyleBackColor = true;
            this.buttonDisregard.Click += new System.EventHandler(this.buttonDisregard_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonDisregard);
            this.panel1.Location = new System.Drawing.Point(-2, 516);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(790, 48);
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
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labelChannelName);
            this.Name = "ChannelAdSlateControl";
            this.Text = "Advertising and Slate Control";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ChannelAdSlateControl_FormClosed);
            this.Load += new System.EventHandler(this.ChannelAdSlateControl_Load);
            this.Shown += new System.EventHandler(this.ChannelInformation_Shown);
            this.contextMenuStripDG.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
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
        private System.Windows.Forms.Button buttonDisregard;
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
        private ListViewJPG listViewJPG1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxJPGSearch;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.CheckBox checkBoxPreviewStream;
        private System.Windows.Forms.WebBrowser webBrowserPreview2;
        private System.Windows.Forms.CheckBox checkBoxPreviewSlate;
        private System.Windows.Forms.PictureBox pictureBoxPreviewSlate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}