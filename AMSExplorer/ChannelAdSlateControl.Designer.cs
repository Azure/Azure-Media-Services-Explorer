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
            // 
            // splitContainerPreviews
            // 
            this.splitContainerPreviews.BackColor = System.Drawing.SystemColors.ScrollBar;
            resources.ApplyResources(this.splitContainerPreviews, "splitContainerPreviews");
            this.splitContainerPreviews.Name = "splitContainerPreviews";
            // 
            // splitContainerPreviews.Panel1
            // 
            this.splitContainerPreviews.Panel1.BackColor = System.Drawing.SystemColors.Window;
            this.splitContainerPreviews.Panel1.Controls.Add(this.checkBoxPreviewStream);
            this.splitContainerPreviews.Panel1.Controls.Add(this.webBrowserPreview);
            this.splitContainerPreviews.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer2_Panel1_Paint);
            // 
            // splitContainerPreviews.Panel2
            // 
            this.splitContainerPreviews.Panel2.BackColor = System.Drawing.SystemColors.Window;
            this.splitContainerPreviews.Panel2.Controls.Add(this.checkBoxPreviewSlate);
            this.splitContainerPreviews.Panel2.Controls.Add(this.labelSlatePreviewInfo);
            this.splitContainerPreviews.Panel2.Controls.Add(this.pictureBoxPreviewSlate);
            // 
            // checkBoxPreviewStream
            // 
            resources.ApplyResources(this.checkBoxPreviewStream, "checkBoxPreviewStream");
            this.checkBoxPreviewStream.Name = "checkBoxPreviewStream";
            this.checkBoxPreviewStream.UseVisualStyleBackColor = true;
            this.checkBoxPreviewStream.CheckedChanged += new System.EventHandler(this.checkBoxPreview_CheckedChanged);
            // 
            // webBrowserPreview
            // 
            resources.ApplyResources(this.webBrowserPreview, "webBrowserPreview");
            this.webBrowserPreview.Name = "webBrowserPreview";
            this.webBrowserPreview.ScriptErrorsSuppressed = true;
            this.webBrowserPreview.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowserPreview2_DocumentCompleted);
            // 
            // checkBoxPreviewSlate
            // 
            resources.ApplyResources(this.checkBoxPreviewSlate, "checkBoxPreviewSlate");
            this.checkBoxPreviewSlate.Name = "checkBoxPreviewSlate";
            this.checkBoxPreviewSlate.UseVisualStyleBackColor = true;
            this.checkBoxPreviewSlate.CheckedChanged += new System.EventHandler(this.checkBoxPreviewSlate_CheckedChanged);
            // 
            // labelSlatePreviewInfo
            // 
            resources.ApplyResources(this.labelSlatePreviewInfo, "labelSlatePreviewInfo");
            this.labelSlatePreviewInfo.Name = "labelSlatePreviewInfo";
            // 
            // pictureBoxPreviewSlate
            // 
            resources.ApplyResources(this.pictureBoxPreviewSlate, "pictureBoxPreviewSlate");
            this.pictureBoxPreviewSlate.Name = "pictureBoxPreviewSlate";
            this.pictureBoxPreviewSlate.TabStop = false;
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
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // buttongenerateContentKey
            // 
            resources.ApplyResources(this.buttongenerateContentKey, "buttongenerateContentKey");
            this.buttongenerateContentKey.Name = "buttongenerateContentKey";
            this.toolTip1.SetToolTip(this.buttongenerateContentKey, resources.GetString("buttongenerateContentKey.ToolTip"));
            this.buttongenerateContentKey.UseVisualStyleBackColor = true;
            this.buttongenerateContentKey.Click += new System.EventHandler(this.buttongenerateContentKey_Click);
            // 
            // buttonInsertAD
            // 
            resources.ApplyResources(this.buttonInsertAD, "buttonInsertAD");
            this.buttonInsertAD.Image = global::AMSExplorer.Bitmaps.create;
            this.buttonInsertAD.Name = "buttonInsertAD";
            this.toolTip1.SetToolTip(this.buttonInsertAD, resources.GetString("buttonInsertAD.ToolTip"));
            this.buttonInsertAD.UseVisualStyleBackColor = true;
            this.buttonInsertAD.Click += new System.EventHandler(this.buttonInsertAD_Click);
            // 
            // buttonInsertAdAndSlate
            // 
            resources.ApplyResources(this.buttonInsertAdAndSlate, "buttonInsertAdAndSlate");
            this.buttonInsertAdAndSlate.Image = global::AMSExplorer.Bitmaps.thumbnails;
            this.buttonInsertAdAndSlate.Name = "buttonInsertAdAndSlate";
            this.toolTip1.SetToolTip(this.buttonInsertAdAndSlate, resources.GetString("buttonInsertAdAndSlate.ToolTip"));
            this.buttonInsertAdAndSlate.UseVisualStyleBackColor = true;
            this.buttonInsertAdAndSlate.Click += new System.EventHandler(this.buttonInsertAdAndSlate_Click);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // textBoxADSignalDuration
            // 
            resources.ApplyResources(this.textBoxADSignalDuration, "textBoxADSignalDuration");
            this.textBoxADSignalDuration.Name = "textBoxADSignalDuration";
            this.toolTip1.SetToolTip(this.textBoxADSignalDuration, resources.GetString("textBoxADSignalDuration.ToolTip"));
            this.textBoxADSignalDuration.TextChanged += new System.EventHandler(this.textBoxADSignalDuration_TextChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // textBoxCueId
            // 
            resources.ApplyResources(this.textBoxCueId, "textBoxCueId");
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
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label15.Name = "label15";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // textBoxJPGSearch
            // 
            resources.ApplyResources(this.textBoxJPGSearch, "textBoxJPGSearch");
            this.textBoxJPGSearch.Name = "textBoxJPGSearch";
            this.textBoxJPGSearch.TextChanged += new System.EventHandler(this.textBoxJPGSearch_TextChanged);
            // 
            // progressBarUpload
            // 
            resources.ApplyResources(this.progressBarUpload, "progressBarUpload");
            this.progressBarUpload.Name = "progressBarUpload";
            this.progressBarUpload.Click += new System.EventHandler(this.progressBarUpload_Click);
            // 
            // buttonUploadSlate
            // 
            resources.ApplyResources(this.buttonUploadSlate, "buttonUploadSlate");
            this.buttonUploadSlate.Name = "buttonUploadSlate";
            this.toolTip1.SetToolTip(this.buttonUploadSlate, resources.GetString("buttonUploadSlate.ToolTip"));
            this.buttonUploadSlate.UseVisualStyleBackColor = true;
            this.buttonUploadSlate.Click += new System.EventHandler(this.buttonUploadSlate_Click);
            // 
            // listViewJPG1
            // 
            resources.ApplyResources(this.listViewJPG1, "listViewJPG1");
            this.listViewJPG1.FullRowSelect = true;
            this.listViewJPG1.HideSelection = false;
            this.listViewJPG1.MultiSelect = false;
            this.listViewJPG1.Name = "listViewJPG1";
            this.listViewJPG1.Tag = -1;
            this.listViewJPG1.UseCompatibleStateImageBehavior = false;
            this.listViewJPG1.View = System.Windows.Forms.View.Details;
            this.listViewJPG1.SelectedIndexChanged += new System.EventHandler(this.listViewJPG1_SelectedIndexChanged);
            // 
            // buttonHideSlate
            // 
            resources.ApplyResources(this.buttonHideSlate, "buttonHideSlate");
            this.buttonHideSlate.Image = global::AMSExplorer.Bitmaps.cancel;
            this.buttonHideSlate.Name = "buttonHideSlate";
            this.toolTip1.SetToolTip(this.buttonHideSlate, resources.GetString("buttonHideSlate.ToolTip"));
            this.buttonHideSlate.UseVisualStyleBackColor = true;
            this.buttonHideSlate.Click += new System.EventHandler(this.buttonHideSlate_Click);
            // 
            // buttonShowSLate
            // 
            resources.ApplyResources(this.buttonShowSLate, "buttonShowSLate");
            this.buttonShowSLate.Image = global::AMSExplorer.Bitmaps.thumbnails;
            this.buttonShowSLate.Name = "buttonShowSLate";
            this.toolTip1.SetToolTip(this.buttonShowSLate, resources.GetString("buttonShowSLate.ToolTip"));
            this.buttonShowSLate.UseVisualStyleBackColor = true;
            this.buttonShowSLate.Click += new System.EventHandler(this.buttonShowSLate_Click);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // textBoxSlateDuration
            // 
            resources.ApplyResources(this.textBoxSlateDuration, "textBoxSlateDuration");
            this.textBoxSlateDuration.Name = "textBoxSlateDuration";
            this.toolTip1.SetToolTip(this.textBoxSlateDuration, resources.GetString("textBoxSlateDuration.ToolTip"));
            this.textBoxSlateDuration.TextChanged += new System.EventHandler(this.textBoxADSignalDuration_TextChanged);
            // 
            // contextMenuStripDG
            // 
            this.contextMenuStripDG.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemFilesCopyClipboard});
            this.contextMenuStripDG.Name = "contextMenuStripDG";
            resources.ApplyResources(this.contextMenuStripDG, "contextMenuStripDG");
            this.contextMenuStripDG.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripDG_Opening);
            this.contextMenuStripDG.MouseClick += new System.Windows.Forms.MouseEventHandler(this.contextMenuStripDG_MouseClick);
            // 
            // toolStripMenuItemFilesCopyClipboard
            // 
            this.toolStripMenuItemFilesCopyClipboard.Name = "toolStripMenuItemFilesCopyClipboard";
            resources.ApplyResources(this.toolStripMenuItemFilesCopyClipboard, "toolStripMenuItemFilesCopyClipboard");
            this.toolStripMenuItemFilesCopyClipboard.Click += new System.EventHandler(this.toolStripMenuItemFilesCopyClipboard_Click);
            // 
            // labelChannelName
            // 
            resources.ApplyResources(this.labelChannelName, "labelChannelName");
            this.labelChannelName.Name = "labelChannelName";
            // 
            // buttonClose
            // 
            resources.ApplyResources(this.buttonClose, "buttonClose");
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonDisregard_Click);
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonClose);
            this.panel1.Name = "panel1";
            // 
            // openFileDialogSlate
            // 
            resources.ApplyResources(this.openFileDialogSlate, "openFileDialogSlate");
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // moreinfoLiveEncodingProfilelink
            // 
            resources.ApplyResources(this.moreinfoLiveEncodingProfilelink, "moreinfoLiveEncodingProfilelink");
            this.moreinfoLiveEncodingProfilelink.Name = "moreinfoLiveEncodingProfilelink";
            this.moreinfoLiveEncodingProfilelink.TabStop = true;
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