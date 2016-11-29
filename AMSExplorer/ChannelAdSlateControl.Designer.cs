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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChannelAdSlateControl));
            this.splitContainerBox = new System.Windows.Forms.SplitContainer();
            this.splitContainerPreviews = new System.Windows.Forms.SplitContainer();
            this.checkBoxPreviewStream = new System.Windows.Forms.CheckBox();
            this.webBrowserPreview = new System.Windows.Forms.WebBrowser();
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
            this.contextMenuStripDG = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemFilesCopyClipboard = new System.Windows.Forms.ToolStripMenuItem();
            this.labelChannelName = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.openFileDialogSlate = new System.Windows.Forms.OpenFileDialog();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.moreinfoLiveEncodingProfilelink = new System.Windows.Forms.LinkLabel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
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
            this.contextMenuStripDG.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerBox
            // 
            resources.ApplyResources(this.splitContainerBox, "splitContainerBox");
            this.splitContainerBox.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.errorProvider1.SetError(this.splitContainerBox, resources.GetString("splitContainerBox.Error"));
            this.errorProvider1.SetIconAlignment(this.splitContainerBox, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("splitContainerBox.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.splitContainerBox, ((int)(resources.GetObject("splitContainerBox.IconPadding"))));
            this.splitContainerBox.Name = "splitContainerBox";
            // 
            // splitContainerBox.Panel1
            // 
            resources.ApplyResources(this.splitContainerBox.Panel1, "splitContainerBox.Panel1");
            this.splitContainerBox.Panel1.BackColor = System.Drawing.SystemColors.Window;
            this.splitContainerBox.Panel1.Controls.Add(this.splitContainerPreviews);
            this.errorProvider1.SetError(this.splitContainerBox.Panel1, resources.GetString("splitContainerBox.Panel1.Error"));
            this.errorProvider1.SetIconAlignment(this.splitContainerBox.Panel1, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("splitContainerBox.Panel1.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.splitContainerBox.Panel1, ((int)(resources.GetObject("splitContainerBox.Panel1.IconPadding"))));
            this.toolTip1.SetToolTip(this.splitContainerBox.Panel1, resources.GetString("splitContainerBox.Panel1.ToolTip"));
            // 
            // splitContainerBox.Panel2
            // 
            resources.ApplyResources(this.splitContainerBox.Panel2, "splitContainerBox.Panel2");
            this.splitContainerBox.Panel2.BackColor = System.Drawing.SystemColors.Window;
            this.splitContainerBox.Panel2.Controls.Add(this.groupBox1);
            this.splitContainerBox.Panel2.Controls.Add(this.groupBox2);
            this.errorProvider1.SetError(this.splitContainerBox.Panel2, resources.GetString("splitContainerBox.Panel2.Error"));
            this.errorProvider1.SetIconAlignment(this.splitContainerBox.Panel2, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("splitContainerBox.Panel2.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.splitContainerBox.Panel2, ((int)(resources.GetObject("splitContainerBox.Panel2.IconPadding"))));
            this.toolTip1.SetToolTip(this.splitContainerBox.Panel2, resources.GetString("splitContainerBox.Panel2.ToolTip"));
            this.toolTip1.SetToolTip(this.splitContainerBox, resources.GetString("splitContainerBox.ToolTip"));
            // 
            // splitContainerPreviews
            // 
            resources.ApplyResources(this.splitContainerPreviews, "splitContainerPreviews");
            this.splitContainerPreviews.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.errorProvider1.SetError(this.splitContainerPreviews, resources.GetString("splitContainerPreviews.Error"));
            this.errorProvider1.SetIconAlignment(this.splitContainerPreviews, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("splitContainerPreviews.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.splitContainerPreviews, ((int)(resources.GetObject("splitContainerPreviews.IconPadding"))));
            this.splitContainerPreviews.Name = "splitContainerPreviews";
            // 
            // splitContainerPreviews.Panel1
            // 
            resources.ApplyResources(this.splitContainerPreviews.Panel1, "splitContainerPreviews.Panel1");
            this.splitContainerPreviews.Panel1.BackColor = System.Drawing.SystemColors.Window;
            this.splitContainerPreviews.Panel1.Controls.Add(this.checkBoxPreviewStream);
            this.splitContainerPreviews.Panel1.Controls.Add(this.webBrowserPreview);
            this.errorProvider1.SetError(this.splitContainerPreviews.Panel1, resources.GetString("splitContainerPreviews.Panel1.Error"));
            this.errorProvider1.SetIconAlignment(this.splitContainerPreviews.Panel1, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("splitContainerPreviews.Panel1.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.splitContainerPreviews.Panel1, ((int)(resources.GetObject("splitContainerPreviews.Panel1.IconPadding"))));
            this.toolTip1.SetToolTip(this.splitContainerPreviews.Panel1, resources.GetString("splitContainerPreviews.Panel1.ToolTip"));
            this.splitContainerPreviews.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer2_Panel1_Paint);
            // 
            // splitContainerPreviews.Panel2
            // 
            resources.ApplyResources(this.splitContainerPreviews.Panel2, "splitContainerPreviews.Panel2");
            this.splitContainerPreviews.Panel2.BackColor = System.Drawing.SystemColors.Window;
            this.splitContainerPreviews.Panel2.Controls.Add(this.checkBoxPreviewSlate);
            this.splitContainerPreviews.Panel2.Controls.Add(this.labelSlatePreviewInfo);
            this.splitContainerPreviews.Panel2.Controls.Add(this.pictureBoxPreviewSlate);
            this.errorProvider1.SetError(this.splitContainerPreviews.Panel2, resources.GetString("splitContainerPreviews.Panel2.Error"));
            this.errorProvider1.SetIconAlignment(this.splitContainerPreviews.Panel2, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("splitContainerPreviews.Panel2.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.splitContainerPreviews.Panel2, ((int)(resources.GetObject("splitContainerPreviews.Panel2.IconPadding"))));
            this.toolTip1.SetToolTip(this.splitContainerPreviews.Panel2, resources.GetString("splitContainerPreviews.Panel2.ToolTip"));
            this.toolTip1.SetToolTip(this.splitContainerPreviews, resources.GetString("splitContainerPreviews.ToolTip"));
            // 
            // checkBoxPreviewStream
            // 
            resources.ApplyResources(this.checkBoxPreviewStream, "checkBoxPreviewStream");
            this.errorProvider1.SetError(this.checkBoxPreviewStream, resources.GetString("checkBoxPreviewStream.Error"));
            this.errorProvider1.SetIconAlignment(this.checkBoxPreviewStream, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("checkBoxPreviewStream.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.checkBoxPreviewStream, ((int)(resources.GetObject("checkBoxPreviewStream.IconPadding"))));
            this.checkBoxPreviewStream.Name = "checkBoxPreviewStream";
            this.toolTip1.SetToolTip(this.checkBoxPreviewStream, resources.GetString("checkBoxPreviewStream.ToolTip"));
            this.checkBoxPreviewStream.UseVisualStyleBackColor = true;
            this.checkBoxPreviewStream.CheckedChanged += new System.EventHandler(this.checkBoxPreview_CheckedChanged);
            // 
            // webBrowserPreview
            // 
            resources.ApplyResources(this.webBrowserPreview, "webBrowserPreview");
            this.errorProvider1.SetError(this.webBrowserPreview, resources.GetString("webBrowserPreview.Error"));
            this.errorProvider1.SetIconAlignment(this.webBrowserPreview, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("webBrowserPreview.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.webBrowserPreview, ((int)(resources.GetObject("webBrowserPreview.IconPadding"))));
            this.webBrowserPreview.Name = "webBrowserPreview";
            this.webBrowserPreview.ScriptErrorsSuppressed = true;
            this.toolTip1.SetToolTip(this.webBrowserPreview, resources.GetString("webBrowserPreview.ToolTip"));
            this.webBrowserPreview.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowserPreview2_DocumentCompleted);
            // 
            // checkBoxPreviewSlate
            // 
            resources.ApplyResources(this.checkBoxPreviewSlate, "checkBoxPreviewSlate");
            this.errorProvider1.SetError(this.checkBoxPreviewSlate, resources.GetString("checkBoxPreviewSlate.Error"));
            this.errorProvider1.SetIconAlignment(this.checkBoxPreviewSlate, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("checkBoxPreviewSlate.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.checkBoxPreviewSlate, ((int)(resources.GetObject("checkBoxPreviewSlate.IconPadding"))));
            this.checkBoxPreviewSlate.Name = "checkBoxPreviewSlate";
            this.toolTip1.SetToolTip(this.checkBoxPreviewSlate, resources.GetString("checkBoxPreviewSlate.ToolTip"));
            this.checkBoxPreviewSlate.UseVisualStyleBackColor = true;
            this.checkBoxPreviewSlate.CheckedChanged += new System.EventHandler(this.checkBoxPreviewSlate_CheckedChanged);
            // 
            // labelSlatePreviewInfo
            // 
            resources.ApplyResources(this.labelSlatePreviewInfo, "labelSlatePreviewInfo");
            this.errorProvider1.SetError(this.labelSlatePreviewInfo, resources.GetString("labelSlatePreviewInfo.Error"));
            this.errorProvider1.SetIconAlignment(this.labelSlatePreviewInfo, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("labelSlatePreviewInfo.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.labelSlatePreviewInfo, ((int)(resources.GetObject("labelSlatePreviewInfo.IconPadding"))));
            this.labelSlatePreviewInfo.Name = "labelSlatePreviewInfo";
            this.toolTip1.SetToolTip(this.labelSlatePreviewInfo, resources.GetString("labelSlatePreviewInfo.ToolTip"));
            // 
            // pictureBoxPreviewSlate
            // 
            resources.ApplyResources(this.pictureBoxPreviewSlate, "pictureBoxPreviewSlate");
            this.errorProvider1.SetError(this.pictureBoxPreviewSlate, resources.GetString("pictureBoxPreviewSlate.Error"));
            this.errorProvider1.SetIconAlignment(this.pictureBoxPreviewSlate, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("pictureBoxPreviewSlate.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.pictureBoxPreviewSlate, ((int)(resources.GetObject("pictureBoxPreviewSlate.IconPadding"))));
            this.pictureBoxPreviewSlate.Name = "pictureBoxPreviewSlate";
            this.pictureBoxPreviewSlate.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBoxPreviewSlate, resources.GetString("pictureBoxPreviewSlate.ToolTip"));
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.buttongenerateContentKey);
            this.groupBox1.Controls.Add(this.buttonInsertAD);
            this.groupBox1.Controls.Add(this.buttonInsertAdAndSlate);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBoxADSignalDuration);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBoxCueId);
            this.errorProvider1.SetError(this.groupBox1, resources.GetString("groupBox1.Error"));
            this.errorProvider1.SetIconAlignment(this.groupBox1, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("groupBox1.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.groupBox1, ((int)(resources.GetObject("groupBox1.IconPadding"))));
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            this.toolTip1.SetToolTip(this.groupBox1, resources.GetString("groupBox1.ToolTip"));
            // 
            // buttongenerateContentKey
            // 
            resources.ApplyResources(this.buttongenerateContentKey, "buttongenerateContentKey");
            this.errorProvider1.SetError(this.buttongenerateContentKey, resources.GetString("buttongenerateContentKey.Error"));
            this.errorProvider1.SetIconAlignment(this.buttongenerateContentKey, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("buttongenerateContentKey.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.buttongenerateContentKey, ((int)(resources.GetObject("buttongenerateContentKey.IconPadding"))));
            this.buttongenerateContentKey.Name = "buttongenerateContentKey";
            this.toolTip1.SetToolTip(this.buttongenerateContentKey, resources.GetString("buttongenerateContentKey.ToolTip"));
            this.buttongenerateContentKey.UseVisualStyleBackColor = true;
            this.buttongenerateContentKey.Click += new System.EventHandler(this.buttongenerateContentKey_Click);
            // 
            // buttonInsertAD
            // 
            resources.ApplyResources(this.buttonInsertAD, "buttonInsertAD");
            this.errorProvider1.SetError(this.buttonInsertAD, resources.GetString("buttonInsertAD.Error"));
            this.errorProvider1.SetIconAlignment(this.buttonInsertAD, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("buttonInsertAD.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.buttonInsertAD, ((int)(resources.GetObject("buttonInsertAD.IconPadding"))));
            this.buttonInsertAD.Image = global::AMSExplorer.Bitmaps.create;
            this.buttonInsertAD.Name = "buttonInsertAD";
            this.toolTip1.SetToolTip(this.buttonInsertAD, resources.GetString("buttonInsertAD.ToolTip"));
            this.buttonInsertAD.UseVisualStyleBackColor = true;
            this.buttonInsertAD.Click += new System.EventHandler(this.buttonInsertAD_Click);
            // 
            // buttonInsertAdAndSlate
            // 
            resources.ApplyResources(this.buttonInsertAdAndSlate, "buttonInsertAdAndSlate");
            this.errorProvider1.SetError(this.buttonInsertAdAndSlate, resources.GetString("buttonInsertAdAndSlate.Error"));
            this.errorProvider1.SetIconAlignment(this.buttonInsertAdAndSlate, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("buttonInsertAdAndSlate.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.buttonInsertAdAndSlate, ((int)(resources.GetObject("buttonInsertAdAndSlate.IconPadding"))));
            this.buttonInsertAdAndSlate.Image = global::AMSExplorer.Bitmaps.thumbnails;
            this.buttonInsertAdAndSlate.Name = "buttonInsertAdAndSlate";
            this.toolTip1.SetToolTip(this.buttonInsertAdAndSlate, resources.GetString("buttonInsertAdAndSlate.ToolTip"));
            this.buttonInsertAdAndSlate.UseVisualStyleBackColor = true;
            this.buttonInsertAdAndSlate.Click += new System.EventHandler(this.buttonInsertAdAndSlate_Click);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.errorProvider1.SetError(this.label3, resources.GetString("label3.Error"));
            this.errorProvider1.SetIconAlignment(this.label3, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label3.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label3, ((int)(resources.GetObject("label3.IconPadding"))));
            this.label3.Name = "label3";
            this.toolTip1.SetToolTip(this.label3, resources.GetString("label3.ToolTip"));
            // 
            // textBoxADSignalDuration
            // 
            resources.ApplyResources(this.textBoxADSignalDuration, "textBoxADSignalDuration");
            this.errorProvider1.SetError(this.textBoxADSignalDuration, resources.GetString("textBoxADSignalDuration.Error"));
            this.errorProvider1.SetIconAlignment(this.textBoxADSignalDuration, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("textBoxADSignalDuration.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.textBoxADSignalDuration, ((int)(resources.GetObject("textBoxADSignalDuration.IconPadding"))));
            this.textBoxADSignalDuration.Name = "textBoxADSignalDuration";
            this.toolTip1.SetToolTip(this.textBoxADSignalDuration, resources.GetString("textBoxADSignalDuration.ToolTip"));
            this.textBoxADSignalDuration.TextChanged += new System.EventHandler(this.textBoxADSignalDuration_TextChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.errorProvider1.SetError(this.label1, resources.GetString("label1.Error"));
            this.errorProvider1.SetIconAlignment(this.label1, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label1.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label1, ((int)(resources.GetObject("label1.IconPadding"))));
            this.label1.Name = "label1";
            this.toolTip1.SetToolTip(this.label1, resources.GetString("label1.ToolTip"));
            // 
            // textBoxCueId
            // 
            resources.ApplyResources(this.textBoxCueId, "textBoxCueId");
            this.errorProvider1.SetError(this.textBoxCueId, resources.GetString("textBoxCueId.Error"));
            this.errorProvider1.SetIconAlignment(this.textBoxCueId, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("textBoxCueId.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.textBoxCueId, ((int)(resources.GetObject("textBoxCueId.IconPadding"))));
            this.textBoxCueId.Name = "textBoxCueId";
            this.toolTip1.SetToolTip(this.textBoxCueId, resources.GetString("textBoxCueId.ToolTip"));
            this.textBoxCueId.TextChanged += new System.EventHandler(this.textBoxCueId_TextChanged);
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
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
            this.errorProvider1.SetError(this.groupBox2, resources.GetString("groupBox2.Error"));
            this.errorProvider1.SetIconAlignment(this.groupBox2, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("groupBox2.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.groupBox2, ((int)(resources.GetObject("groupBox2.IconPadding"))));
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            this.toolTip1.SetToolTip(this.groupBox2, resources.GetString("groupBox2.ToolTip"));
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.errorProvider1.SetError(this.label15, resources.GetString("label15.Error"));
            this.label15.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.errorProvider1.SetIconAlignment(this.label15, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label15.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label15, ((int)(resources.GetObject("label15.IconPadding"))));
            this.label15.Name = "label15";
            this.toolTip1.SetToolTip(this.label15, resources.GetString("label15.ToolTip"));
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.errorProvider1.SetError(this.label8, resources.GetString("label8.Error"));
            this.errorProvider1.SetIconAlignment(this.label8, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label8.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label8, ((int)(resources.GetObject("label8.IconPadding"))));
            this.label8.Name = "label8";
            this.toolTip1.SetToolTip(this.label8, resources.GetString("label8.ToolTip"));
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // textBoxJPGSearch
            // 
            resources.ApplyResources(this.textBoxJPGSearch, "textBoxJPGSearch");
            this.errorProvider1.SetError(this.textBoxJPGSearch, resources.GetString("textBoxJPGSearch.Error"));
            this.errorProvider1.SetIconAlignment(this.textBoxJPGSearch, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("textBoxJPGSearch.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.textBoxJPGSearch, ((int)(resources.GetObject("textBoxJPGSearch.IconPadding"))));
            this.textBoxJPGSearch.Name = "textBoxJPGSearch";
            this.toolTip1.SetToolTip(this.textBoxJPGSearch, resources.GetString("textBoxJPGSearch.ToolTip"));
            this.textBoxJPGSearch.TextChanged += new System.EventHandler(this.textBoxJPGSearch_TextChanged);
            // 
            // progressBarUpload
            // 
            resources.ApplyResources(this.progressBarUpload, "progressBarUpload");
            this.errorProvider1.SetError(this.progressBarUpload, resources.GetString("progressBarUpload.Error"));
            this.errorProvider1.SetIconAlignment(this.progressBarUpload, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("progressBarUpload.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.progressBarUpload, ((int)(resources.GetObject("progressBarUpload.IconPadding"))));
            this.progressBarUpload.Name = "progressBarUpload";
            this.toolTip1.SetToolTip(this.progressBarUpload, resources.GetString("progressBarUpload.ToolTip"));
            this.progressBarUpload.Click += new System.EventHandler(this.progressBarUpload_Click);
            // 
            // buttonUploadSlate
            // 
            resources.ApplyResources(this.buttonUploadSlate, "buttonUploadSlate");
            this.errorProvider1.SetError(this.buttonUploadSlate, resources.GetString("buttonUploadSlate.Error"));
            this.errorProvider1.SetIconAlignment(this.buttonUploadSlate, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("buttonUploadSlate.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.buttonUploadSlate, ((int)(resources.GetObject("buttonUploadSlate.IconPadding"))));
            this.buttonUploadSlate.Name = "buttonUploadSlate";
            this.toolTip1.SetToolTip(this.buttonUploadSlate, resources.GetString("buttonUploadSlate.ToolTip"));
            this.buttonUploadSlate.UseVisualStyleBackColor = true;
            this.buttonUploadSlate.Click += new System.EventHandler(this.buttonUploadSlate_Click);
            // 
            // listViewJPG1
            // 
            resources.ApplyResources(this.listViewJPG1, "listViewJPG1");
            this.errorProvider1.SetError(this.listViewJPG1, resources.GetString("listViewJPG1.Error"));
            this.listViewJPG1.FullRowSelect = true;
            this.listViewJPG1.HideSelection = false;
            this.errorProvider1.SetIconAlignment(this.listViewJPG1, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("listViewJPG1.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.listViewJPG1, ((int)(resources.GetObject("listViewJPG1.IconPadding"))));
            this.listViewJPG1.MultiSelect = false;
            this.listViewJPG1.Name = "listViewJPG1";
            this.listViewJPG1.Tag = -1;
            this.toolTip1.SetToolTip(this.listViewJPG1, resources.GetString("listViewJPG1.ToolTip"));
            this.listViewJPG1.UseCompatibleStateImageBehavior = false;
            this.listViewJPG1.View = System.Windows.Forms.View.Details;
            this.listViewJPG1.SelectedIndexChanged += new System.EventHandler(this.listViewJPG1_SelectedIndexChanged);
            // 
            // buttonHideSlate
            // 
            resources.ApplyResources(this.buttonHideSlate, "buttonHideSlate");
            this.errorProvider1.SetError(this.buttonHideSlate, resources.GetString("buttonHideSlate.Error"));
            this.errorProvider1.SetIconAlignment(this.buttonHideSlate, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("buttonHideSlate.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.buttonHideSlate, ((int)(resources.GetObject("buttonHideSlate.IconPadding"))));
            this.buttonHideSlate.Image = global::AMSExplorer.Bitmaps.cancel;
            this.buttonHideSlate.Name = "buttonHideSlate";
            this.toolTip1.SetToolTip(this.buttonHideSlate, resources.GetString("buttonHideSlate.ToolTip"));
            this.buttonHideSlate.UseVisualStyleBackColor = true;
            this.buttonHideSlate.Click += new System.EventHandler(this.buttonHideSlate_Click);
            // 
            // buttonShowSLate
            // 
            resources.ApplyResources(this.buttonShowSLate, "buttonShowSLate");
            this.errorProvider1.SetError(this.buttonShowSLate, resources.GetString("buttonShowSLate.Error"));
            this.errorProvider1.SetIconAlignment(this.buttonShowSLate, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("buttonShowSLate.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.buttonShowSLate, ((int)(resources.GetObject("buttonShowSLate.IconPadding"))));
            this.buttonShowSLate.Image = global::AMSExplorer.Bitmaps.thumbnails;
            this.buttonShowSLate.Name = "buttonShowSLate";
            this.toolTip1.SetToolTip(this.buttonShowSLate, resources.GetString("buttonShowSLate.ToolTip"));
            this.buttonShowSLate.UseVisualStyleBackColor = true;
            this.buttonShowSLate.Click += new System.EventHandler(this.buttonShowSLate_Click);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.errorProvider1.SetError(this.label4, resources.GetString("label4.Error"));
            this.errorProvider1.SetIconAlignment(this.label4, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label4.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label4, ((int)(resources.GetObject("label4.IconPadding"))));
            this.label4.Name = "label4";
            this.toolTip1.SetToolTip(this.label4, resources.GetString("label4.ToolTip"));
            // 
            // textBoxSlateDuration
            // 
            resources.ApplyResources(this.textBoxSlateDuration, "textBoxSlateDuration");
            this.errorProvider1.SetError(this.textBoxSlateDuration, resources.GetString("textBoxSlateDuration.Error"));
            this.errorProvider1.SetIconAlignment(this.textBoxSlateDuration, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("textBoxSlateDuration.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.textBoxSlateDuration, ((int)(resources.GetObject("textBoxSlateDuration.IconPadding"))));
            this.textBoxSlateDuration.Name = "textBoxSlateDuration";
            this.toolTip1.SetToolTip(this.textBoxSlateDuration, resources.GetString("textBoxSlateDuration.ToolTip"));
            this.textBoxSlateDuration.TextChanged += new System.EventHandler(this.textBoxADSignalDuration_TextChanged);
            // 
            // contextMenuStripDG
            // 
            resources.ApplyResources(this.contextMenuStripDG, "contextMenuStripDG");
            this.errorProvider1.SetError(this.contextMenuStripDG, resources.GetString("contextMenuStripDG.Error"));
            this.errorProvider1.SetIconAlignment(this.contextMenuStripDG, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("contextMenuStripDG.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.contextMenuStripDG, ((int)(resources.GetObject("contextMenuStripDG.IconPadding"))));
            this.contextMenuStripDG.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemFilesCopyClipboard});
            this.contextMenuStripDG.Name = "contextMenuStripDG";
            this.toolTip1.SetToolTip(this.contextMenuStripDG, resources.GetString("contextMenuStripDG.ToolTip"));
            this.contextMenuStripDG.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripDG_Opening);
            this.contextMenuStripDG.MouseClick += new System.Windows.Forms.MouseEventHandler(this.contextMenuStripDG_MouseClick);
            // 
            // toolStripMenuItemFilesCopyClipboard
            // 
            resources.ApplyResources(this.toolStripMenuItemFilesCopyClipboard, "toolStripMenuItemFilesCopyClipboard");
            this.toolStripMenuItemFilesCopyClipboard.Name = "toolStripMenuItemFilesCopyClipboard";
            this.toolStripMenuItemFilesCopyClipboard.Click += new System.EventHandler(this.toolStripMenuItemFilesCopyClipboard_Click);
            // 
            // labelChannelName
            // 
            resources.ApplyResources(this.labelChannelName, "labelChannelName");
            this.errorProvider1.SetError(this.labelChannelName, resources.GetString("labelChannelName.Error"));
            this.errorProvider1.SetIconAlignment(this.labelChannelName, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("labelChannelName.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.labelChannelName, ((int)(resources.GetObject("labelChannelName.IconPadding"))));
            this.labelChannelName.Name = "labelChannelName";
            this.toolTip1.SetToolTip(this.labelChannelName, resources.GetString("labelChannelName.ToolTip"));
            // 
            // buttonClose
            // 
            resources.ApplyResources(this.buttonClose, "buttonClose");
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.errorProvider1.SetError(this.buttonClose, resources.GetString("buttonClose.Error"));
            this.errorProvider1.SetIconAlignment(this.buttonClose, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("buttonClose.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.buttonClose, ((int)(resources.GetObject("buttonClose.IconPadding"))));
            this.buttonClose.Name = "buttonClose";
            this.toolTip1.SetToolTip(this.buttonClose, resources.GetString("buttonClose.ToolTip"));
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonDisregard_Click);
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonClose);
            this.errorProvider1.SetError(this.panel1, resources.GetString("panel1.Error"));
            this.errorProvider1.SetIconAlignment(this.panel1, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("panel1.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.panel1, ((int)(resources.GetObject("panel1.IconPadding"))));
            this.panel1.Name = "panel1";
            this.toolTip1.SetToolTip(this.panel1, resources.GetString("panel1.ToolTip"));
            // 
            // openFileDialogSlate
            // 
            resources.ApplyResources(this.openFileDialogSlate, "openFileDialogSlate");
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            resources.ApplyResources(this.errorProvider1, "errorProvider1");
            // 
            // moreinfoLiveEncodingProfilelink
            // 
            resources.ApplyResources(this.moreinfoLiveEncodingProfilelink, "moreinfoLiveEncodingProfilelink");
            this.errorProvider1.SetError(this.moreinfoLiveEncodingProfilelink, resources.GetString("moreinfoLiveEncodingProfilelink.Error"));
            this.errorProvider1.SetIconAlignment(this.moreinfoLiveEncodingProfilelink, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("moreinfoLiveEncodingProfilelink.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.moreinfoLiveEncodingProfilelink, ((int)(resources.GetObject("moreinfoLiveEncodingProfilelink.IconPadding"))));
            this.moreinfoLiveEncodingProfilelink.Name = "moreinfoLiveEncodingProfilelink";
            this.moreinfoLiveEncodingProfilelink.TabStop = true;
            this.toolTip1.SetToolTip(this.moreinfoLiveEncodingProfilelink, resources.GetString("moreinfoLiveEncodingProfilelink.ToolTip"));
            this.moreinfoLiveEncodingProfilelink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.moreinfoLiveEncodingProfilelink_LinkClicked);
            // 
            // ChannelAdSlateControl
            // 
            this.AcceptButton = this.buttonClose;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonClose;
            this.Controls.Add(this.moreinfoLiveEncodingProfilelink);
            this.Controls.Add(this.splitContainerBox);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labelChannelName);
            this.Name = "ChannelAdSlateControl";
            this.toolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ChannelAdSlateControl_FormClosed);
            this.Load += new System.EventHandler(this.ChannelAdSlateControl_Load);
            this.Shown += new System.EventHandler(this.ChannelInformation_Shown);
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
            this.contextMenuStripDG.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStripDG;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFilesCopyClipboard;
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
        private System.Windows.Forms.WebBrowser webBrowserPreview;
        private System.Windows.Forms.CheckBox checkBoxPreviewSlate;
        private System.Windows.Forms.PictureBox pictureBoxPreviewSlate;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label labelSlatePreviewInfo;
        private System.Windows.Forms.Button buttongenerateContentKey;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.SplitContainer splitContainerPreviews;
        private System.Windows.Forms.LinkLabel moreinfoLiveEncodingProfilelink;
    }
}