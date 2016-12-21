namespace AMSExplorer
{
    partial class Subclipping
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Subclipping));
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.moreinfoprofilelink = new System.Windows.Forms.LinkLabel();
            this.textBoxAssetName = new System.Windows.Forms.TextBox();
            this.labelassetname = new System.Windows.Forms.Label();
            this.textBoxAssetDuration = new System.Windows.Forms.TextBox();
            this.labelassetduration = new System.Windows.Forms.Label();
            this.textBoxFilterTimeScale = new System.Windows.Forms.TextBox();
            this.labelAssetTimescale = new System.Windows.Forms.Label();
            this.textBoxOffset = new System.Windows.Forms.TextBox();
            this.labelOffset = new System.Windows.Forms.Label();
            this.tabPageXML = new System.Windows.Forms.TabPage();
            this.labelNoJSONBecauseReencoding = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxConfiguration = new System.Windows.Forms.TextBox();
            this.tabPageTR = new System.Windows.Forms.TabPage();
            this.panelAssetInfo = new System.Windows.Forms.Panel();
            this.groupBoxTrimming = new System.Windows.Forms.GroupBox();
            this.panelEDL = new System.Windows.Forms.Panel();
            this.buttonAddEDLEntry = new System.Windows.Forms.Button();
            this.buttonShowEDL = new AMSExplorer.ButtonEDL();
            this.checkBoxUseEDL = new System.Windows.Forms.CheckBox();
            this.textBoxDurationTime = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.labelAccurate = new System.Windows.Forms.Label();
            this.webBrowserPreview = new System.Windows.Forms.WebBrowser();
            this.timeControlStart = new AMSExplorer.TimeControl();
            this.timeControlEnd = new AMSExplorer.TimeControl();
            this.checkBoxPreviewStream = new System.Windows.Forms.CheckBox();
            this.checkBoxTrimming = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.radioButtonAssetFilter = new System.Windows.Forms.RadioButton();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.radioButtonArchiveTopBitrate = new System.Windows.Forms.RadioButton();
            this.radioButtonArchiveAllBitrate = new System.Windows.Forms.RadioButton();
            this.radioButtonClipWithReencode = new System.Windows.Forms.RadioButton();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.labeloutoutputasset = new System.Windows.Forms.Label();
            this.textboxoutputassetname = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxJobName = new System.Windows.Forms.TextBox();
            this.panelJob = new System.Windows.Forms.Panel();
            this.buttonJobOptions = new AMSExplorer.ButtonJobOptions();
            this.label34 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.tabPageXML.SuspendLayout();
            this.tabPageTR.SuspendLayout();
            this.panelAssetInfo.SuspendLayout();
            this.groupBoxTrimming.SuspendLayout();
            this.panelEDL.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.panelJob.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonClose);
            this.panel1.Controls.Add(this.buttonOk);
            this.panel1.Name = "panel1";
            // 
            // buttonClose
            // 
            resources.ApplyResources(this.buttonClose, "buttonClose");
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.UseVisualStyleBackColor = true;
            // 
            // buttonOk
            // 
            resources.ApplyResources(this.buttonOk, "buttonOk");
            this.buttonOk.Image = global::AMSExplorer.Bitmaps.subclipping;
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Tag = "Subclip";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // moreinfoprofilelink
            // 
            resources.ApplyResources(this.moreinfoprofilelink, "moreinfoprofilelink");
            this.moreinfoprofilelink.Name = "moreinfoprofilelink";
            this.moreinfoprofilelink.TabStop = true;
            this.moreinfoprofilelink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.moreinfoprofilelink_LinkClicked);
            // 
            // textBoxAssetName
            // 
            resources.ApplyResources(this.textBoxAssetName, "textBoxAssetName");
            this.textBoxAssetName.Name = "textBoxAssetName";
            this.textBoxAssetName.ReadOnly = true;
            // 
            // labelassetname
            // 
            resources.ApplyResources(this.labelassetname, "labelassetname");
            this.labelassetname.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.labelassetname.Name = "labelassetname";
            // 
            // textBoxAssetDuration
            // 
            resources.ApplyResources(this.textBoxAssetDuration, "textBoxAssetDuration");
            this.textBoxAssetDuration.Name = "textBoxAssetDuration";
            this.textBoxAssetDuration.ReadOnly = true;
            // 
            // labelassetduration
            // 
            resources.ApplyResources(this.labelassetduration, "labelassetduration");
            this.labelassetduration.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.labelassetduration.Name = "labelassetduration";
            // 
            // textBoxFilterTimeScale
            // 
            resources.ApplyResources(this.textBoxFilterTimeScale, "textBoxFilterTimeScale");
            this.textBoxFilterTimeScale.Name = "textBoxFilterTimeScale";
            this.textBoxFilterTimeScale.ReadOnly = true;
            // 
            // labelAssetTimescale
            // 
            resources.ApplyResources(this.labelAssetTimescale, "labelAssetTimescale");
            this.labelAssetTimescale.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.labelAssetTimescale.Name = "labelAssetTimescale";
            // 
            // textBoxOffset
            // 
            resources.ApplyResources(this.textBoxOffset, "textBoxOffset");
            this.textBoxOffset.Name = "textBoxOffset";
            this.textBoxOffset.ReadOnly = true;
            // 
            // labelOffset
            // 
            resources.ApplyResources(this.labelOffset, "labelOffset");
            this.labelOffset.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.labelOffset.Name = "labelOffset";
            // 
            // tabPageXML
            // 
            this.tabPageXML.Controls.Add(this.labelNoJSONBecauseReencoding);
            this.tabPageXML.Controls.Add(this.label3);
            this.tabPageXML.Controls.Add(this.textBoxConfiguration);
            resources.ApplyResources(this.tabPageXML, "tabPageXML");
            this.tabPageXML.Name = "tabPageXML";
            this.tabPageXML.UseVisualStyleBackColor = true;
            this.tabPageXML.Enter += new System.EventHandler(this.tabPageXML_Enter);
            // 
            // labelNoJSONBecauseReencoding
            // 
            resources.ApplyResources(this.labelNoJSONBecauseReencoding, "labelNoJSONBecauseReencoding");
            this.labelNoJSONBecauseReencoding.Name = "labelNoJSONBecauseReencoding";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // textBoxConfiguration
            // 
            resources.ApplyResources(this.textBoxConfiguration, "textBoxConfiguration");
            this.textBoxConfiguration.Name = "textBoxConfiguration";
            // 
            // tabPageTR
            // 
            this.tabPageTR.Controls.Add(this.panelAssetInfo);
            this.tabPageTR.Controls.Add(this.groupBoxTrimming);
            this.tabPageTR.Controls.Add(this.groupBox2);
            resources.ApplyResources(this.tabPageTR, "tabPageTR");
            this.tabPageTR.Name = "tabPageTR";
            this.tabPageTR.UseVisualStyleBackColor = true;
            this.tabPageTR.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // panelAssetInfo
            // 
            this.panelAssetInfo.Controls.Add(this.labelassetname);
            this.panelAssetInfo.Controls.Add(this.textBoxAssetDuration);
            this.panelAssetInfo.Controls.Add(this.textBoxOffset);
            this.panelAssetInfo.Controls.Add(this.labelOffset);
            this.panelAssetInfo.Controls.Add(this.textBoxFilterTimeScale);
            this.panelAssetInfo.Controls.Add(this.textBoxAssetName);
            this.panelAssetInfo.Controls.Add(this.labelassetduration);
            this.panelAssetInfo.Controls.Add(this.labelAssetTimescale);
            resources.ApplyResources(this.panelAssetInfo, "panelAssetInfo");
            this.panelAssetInfo.Name = "panelAssetInfo";
            // 
            // groupBoxTrimming
            // 
            resources.ApplyResources(this.groupBoxTrimming, "groupBoxTrimming");
            this.groupBoxTrimming.Controls.Add(this.panelEDL);
            this.groupBoxTrimming.Controls.Add(this.textBoxDurationTime);
            this.groupBoxTrimming.Controls.Add(this.label7);
            this.groupBoxTrimming.Controls.Add(this.labelAccurate);
            this.groupBoxTrimming.Controls.Add(this.webBrowserPreview);
            this.groupBoxTrimming.Controls.Add(this.timeControlStart);
            this.groupBoxTrimming.Controls.Add(this.timeControlEnd);
            this.groupBoxTrimming.Controls.Add(this.checkBoxPreviewStream);
            this.groupBoxTrimming.Controls.Add(this.checkBoxTrimming);
            this.groupBoxTrimming.Name = "groupBoxTrimming";
            this.groupBoxTrimming.TabStop = false;
            // 
            // panelEDL
            // 
            resources.ApplyResources(this.panelEDL, "panelEDL");
            this.panelEDL.Controls.Add(this.buttonAddEDLEntry);
            this.panelEDL.Controls.Add(this.buttonShowEDL);
            this.panelEDL.Controls.Add(this.checkBoxUseEDL);
            this.panelEDL.Name = "panelEDL";
            // 
            // buttonAddEDLEntry
            // 
            resources.ApplyResources(this.buttonAddEDLEntry, "buttonAddEDLEntry");
            this.buttonAddEDLEntry.Name = "buttonAddEDLEntry";
            this.buttonAddEDLEntry.UseVisualStyleBackColor = true;
            this.buttonAddEDLEntry.Click += new System.EventHandler(this.button2_Click);
            // 
            // buttonShowEDL
            // 
            resources.ApplyResources(this.buttonShowEDL, "buttonShowEDL");
            this.buttonShowEDL.Name = "buttonShowEDL";
            this.buttonShowEDL.Offset = System.TimeSpan.Parse("00:00:00");
            this.buttonShowEDL.UseVisualStyleBackColor = true;
            // 
            // checkBoxUseEDL
            // 
            resources.ApplyResources(this.checkBoxUseEDL, "checkBoxUseEDL");
            this.checkBoxUseEDL.Name = "checkBoxUseEDL";
            this.checkBoxUseEDL.UseVisualStyleBackColor = true;
            this.checkBoxUseEDL.CheckedChanged += new System.EventHandler(this.checkBoxUseEDL_CheckedChanged);
            // 
            // textBoxDurationTime
            // 
            resources.ApplyResources(this.textBoxDurationTime, "textBoxDurationTime");
            this.textBoxDurationTime.Name = "textBoxDurationTime";
            this.textBoxDurationTime.ReadOnly = true;
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label7.Name = "label7";
            // 
            // labelAccurate
            // 
            resources.ApplyResources(this.labelAccurate, "labelAccurate");
            this.labelAccurate.Name = "labelAccurate";
            this.labelAccurate.Tag = "({0} accurate)";
            // 
            // webBrowserPreview
            // 
            resources.ApplyResources(this.webBrowserPreview, "webBrowserPreview");
            this.webBrowserPreview.Name = "webBrowserPreview";
            this.webBrowserPreview.ScriptErrorsSuppressed = true;
            // 
            // timeControlStart
            // 
            resources.ApplyResources(this.timeControlStart, "timeControlStart");
            this.timeControlStart.BackColor = System.Drawing.SystemColors.Window;
            this.timeControlStart.DisplayTrackBar = true;
            this.timeControlStart.Label1 = "";
            this.timeControlStart.Label2 = "Start time :";
            this.timeControlStart.Max = System.TimeSpan.Parse("10675199.02:48:05.4775807");
            this.timeControlStart.Min = System.TimeSpan.Parse("00:00:00");
            this.timeControlStart.Name = "timeControlStart";
            this.timeControlStart.ScaledFirstTimestampOffset = ((ulong)(0ul));
            this.timeControlStart.TimeScale = null;
            this.timeControlStart.TotalDuration = System.TimeSpan.Parse("00:00:00");
            this.timeControlStart.ValueChanged += new System.EventHandler(this.timeControlStart_ValueChanged);
            // 
            // timeControlEnd
            // 
            resources.ApplyResources(this.timeControlEnd, "timeControlEnd");
            this.timeControlEnd.BackColor = System.Drawing.SystemColors.Window;
            this.timeControlEnd.DisplayTrackBar = true;
            this.timeControlEnd.Label1 = "";
            this.timeControlEnd.Label2 = "End time :";
            this.timeControlEnd.Max = System.TimeSpan.Parse("10675199.02:48:05.4775807");
            this.timeControlEnd.Min = System.TimeSpan.Parse("00:00:00");
            this.timeControlEnd.Name = "timeControlEnd";
            this.timeControlEnd.ScaledFirstTimestampOffset = ((ulong)(0ul));
            this.timeControlEnd.TimeScale = null;
            this.timeControlEnd.TotalDuration = System.TimeSpan.Parse("00:00:00");
            this.timeControlEnd.ValueChanged += new System.EventHandler(this.timeControlEnd_ValueChanged);
            // 
            // checkBoxPreviewStream
            // 
            resources.ApplyResources(this.checkBoxPreviewStream, "checkBoxPreviewStream");
            this.checkBoxPreviewStream.Checked = true;
            this.checkBoxPreviewStream.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxPreviewStream.Name = "checkBoxPreviewStream";
            this.checkBoxPreviewStream.UseVisualStyleBackColor = true;
            this.checkBoxPreviewStream.CheckedChanged += new System.EventHandler(this.checkBoxPreviewStream_CheckedChanged_1);
            // 
            // checkBoxTrimming
            // 
            resources.ApplyResources(this.checkBoxTrimming, "checkBoxTrimming");
            this.checkBoxTrimming.Name = "checkBoxTrimming";
            this.checkBoxTrimming.UseVisualStyleBackColor = true;
            this.checkBoxTrimming.CheckedChanged += new System.EventHandler(this.checkBoxTrimming_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.radioButtonAssetFilter);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.radioButtonArchiveTopBitrate);
            this.groupBox2.Controls.Add(this.radioButtonArchiveAllBitrate);
            this.groupBox2.Controls.Add(this.radioButtonClipWithReencode);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label2.Name = "label2";
            // 
            // radioButtonAssetFilter
            // 
            resources.ApplyResources(this.radioButtonAssetFilter, "radioButtonAssetFilter");
            this.radioButtonAssetFilter.Name = "radioButtonAssetFilter";
            this.radioButtonAssetFilter.UseVisualStyleBackColor = true;
            this.radioButtonAssetFilter.CheckedChanged += new System.EventHandler(this.radioButtonClipWithReencode_CheckedChanged);
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label12.Name = "label12";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label11.Name = "label11";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label10.Name = "label10";
            // 
            // radioButtonArchiveTopBitrate
            // 
            resources.ApplyResources(this.radioButtonArchiveTopBitrate, "radioButtonArchiveTopBitrate");
            this.radioButtonArchiveTopBitrate.Checked = true;
            this.radioButtonArchiveTopBitrate.Name = "radioButtonArchiveTopBitrate";
            this.radioButtonArchiveTopBitrate.TabStop = true;
            this.radioButtonArchiveTopBitrate.UseVisualStyleBackColor = true;
            this.radioButtonArchiveTopBitrate.CheckedChanged += new System.EventHandler(this.radioButtonClipWithReencode_CheckedChanged);
            // 
            // radioButtonArchiveAllBitrate
            // 
            resources.ApplyResources(this.radioButtonArchiveAllBitrate, "radioButtonArchiveAllBitrate");
            this.radioButtonArchiveAllBitrate.Name = "radioButtonArchiveAllBitrate";
            this.radioButtonArchiveAllBitrate.UseVisualStyleBackColor = true;
            this.radioButtonArchiveAllBitrate.CheckedChanged += new System.EventHandler(this.radioButtonClipWithReencode_CheckedChanged);
            // 
            // radioButtonClipWithReencode
            // 
            resources.ApplyResources(this.radioButtonClipWithReencode, "radioButtonClipWithReencode");
            this.radioButtonClipWithReencode.Name = "radioButtonClipWithReencode";
            this.radioButtonClipWithReencode.UseVisualStyleBackColor = true;
            this.radioButtonClipWithReencode.CheckedChanged += new System.EventHandler(this.radioButtonClipWithReencode_CheckedChanged);
            // 
            // tabControl1
            // 
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Controls.Add(this.tabPageTR);
            this.tabControl1.Controls.Add(this.tabPageXML);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // labeloutoutputasset
            // 
            resources.ApplyResources(this.labeloutoutputasset, "labeloutoutputasset");
            this.labeloutoutputasset.Name = "labeloutoutputasset";
            this.labeloutoutputasset.Tag = "Output asset(s) name :";
            // 
            // textboxoutputassetname
            // 
            resources.ApplyResources(this.textboxoutputassetname, "textboxoutputassetname");
            this.textboxoutputassetname.Name = "textboxoutputassetname";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // textBoxJobName
            // 
            resources.ApplyResources(this.textBoxJobName, "textBoxJobName");
            this.textBoxJobName.Name = "textBoxJobName";
            // 
            // panelJob
            // 
            resources.ApplyResources(this.panelJob, "panelJob");
            this.panelJob.Controls.Add(this.textboxoutputassetname);
            this.panelJob.Controls.Add(this.textBoxJobName);
            this.panelJob.Controls.Add(this.buttonJobOptions);
            this.panelJob.Controls.Add(this.labeloutoutputasset);
            this.panelJob.Controls.Add(this.label5);
            this.panelJob.Name = "panelJob";
            // 
            // buttonJobOptions
            // 
            resources.ApplyResources(this.buttonJobOptions, "buttonJobOptions");
            this.buttonJobOptions.Name = "buttonJobOptions";
            this.buttonJobOptions.UseVisualStyleBackColor = true;
            // 
            // label34
            // 
            resources.ApplyResources(this.label34, "label34");
            this.label34.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(128)))), ((int)(((byte)(171)))));
            this.label34.Name = "label34";
            // 
            // panel2
            // 
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Name = "panel2";
            // 
            // Subclipping
            // 
            this.AcceptButton = this.buttonOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonClose;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label34);
            this.Controls.Add(this.panelJob);
            this.Controls.Add(this.moreinfoprofilelink);
            this.Controls.Add(this.panel1);
            this.Name = "Subclipping";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Subclipping_FormClosed);
            this.Load += new System.EventHandler(this.Subclipping_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.tabPageXML.ResumeLayout(false);
            this.tabPageXML.PerformLayout();
            this.tabPageTR.ResumeLayout(false);
            this.panelAssetInfo.ResumeLayout(false);
            this.panelAssetInfo.PerformLayout();
            this.groupBoxTrimming.ResumeLayout(false);
            this.groupBoxTrimming.PerformLayout();
            this.panelEDL.ResumeLayout(false);
            this.panelEDL.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.panelJob.ResumeLayout(false);
            this.panelJob.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.LinkLabel moreinfoprofilelink;
        private System.Windows.Forms.TextBox textBoxAssetName;
        private System.Windows.Forms.Label labelassetname;
        private System.Windows.Forms.TextBox textBoxAssetDuration;
        private System.Windows.Forms.Label labelassetduration;
        private System.Windows.Forms.TextBox textBoxFilterTimeScale;
        private System.Windows.Forms.Label labelAssetTimescale;
        private System.Windows.Forms.TextBox textBoxOffset;
        private System.Windows.Forms.Label labelOffset;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageTR;
        private System.Windows.Forms.RadioButton radioButtonClipWithReencode;
        private System.Windows.Forms.RadioButton radioButtonArchiveAllBitrate;
        private System.Windows.Forms.RadioButton radioButtonArchiveTopBitrate;
        private TimeControl timeControlStart;
        private TimeControl timeControlEnd;
        private System.Windows.Forms.TabPage tabPageXML;
        private ButtonJobOptions buttonJobOptions;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkBoxTrimming;
        private System.Windows.Forms.GroupBox groupBoxTrimming;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        public System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxConfiguration;
        public System.Windows.Forms.Label labeloutoutputasset;
        private System.Windows.Forms.TextBox textboxoutputassetname;
        public System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxJobName;
        private System.Windows.Forms.Panel panelJob;
        public System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label labelAccurate;
        private System.Windows.Forms.CheckBox checkBoxPreviewStream;
        private System.Windows.Forms.WebBrowser webBrowserPreview;
        private System.Windows.Forms.Panel panelAssetInfo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton radioButtonAssetFilter;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.TextBox textBoxDurationTime;
        public System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label labelNoJSONBecauseReencoding;
        private System.Windows.Forms.Button buttonAddEDLEntry;
        private ButtonEDL buttonShowEDL;
        private System.Windows.Forms.CheckBox checkBoxUseEDL;
        private System.Windows.Forms.Panel panelEDL;
        private System.Windows.Forms.Panel panel2;
    }
}