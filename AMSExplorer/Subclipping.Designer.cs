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
            this.tabPageJSON = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxConfiguration = new System.Windows.Forms.TextBox();
            this.tabPageTR = new System.Windows.Forms.TabPage();
            this.panelAssetInfo = new System.Windows.Forms.Panel();
            this.labelDiscountinuity = new System.Windows.Forms.Label();
            this.groupBoxTrimming = new System.Windows.Forms.GroupBox();
            this.labelAccurate = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.webBrowserPreview = new System.Windows.Forms.WebBrowser();
            this.timeControlStart = new AMSExplorer.TimeControl();
            this.textBoxDurationTime = new System.Windows.Forms.TextBox();
            this.timeControlEnd = new AMSExplorer.TimeControl();
            this.checkBoxPreviewStream = new System.Windows.Forms.CheckBox();
            this.checkBoxTrimming = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.radioButtonAssetFilter = new System.Windows.Forms.RadioButton();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.radioButtonArchiveTopBitrate = new System.Windows.Forms.RadioButton();
            this.radioButtonClipWithReencode = new System.Windows.Forms.RadioButton();
            this.panelEDL = new System.Windows.Forms.Panel();
            this.buttonAddEDLEntry = new System.Windows.Forms.Button();
            this.buttonShowEDL = new AMSExplorer.ButtonEDL();
            this.checkBoxUseEDL = new System.Windows.Forms.CheckBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.labeloutputasset = new System.Windows.Forms.Label();
            this.textboxoutputassetname = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxJobName = new System.Windows.Forms.TextBox();
            this.panelJob = new System.Windows.Forms.Panel();
            this.labelGen = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.tabPageJSON.SuspendLayout();
            this.tabPageTR.SuspendLayout();
            this.panelAssetInfo.SuspendLayout();
            this.groupBoxTrimming.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panelEDL.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.panelJob.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonClose);
            this.panel1.Controls.Add(this.buttonOk);
            this.errorProvider1.SetError(this.panel1, resources.GetString("panel1.Error"));
            this.errorProvider1.SetIconAlignment(this.panel1, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("panel1.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.panel1, ((int)(resources.GetObject("panel1.IconPadding"))));
            this.panel1.Name = "panel1";
            this.toolTip1.SetToolTip(this.panel1, resources.GetString("panel1.ToolTip"));
            // 
            // buttonClose
            // 
            resources.ApplyResources(this.buttonClose, "buttonClose");
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.errorProvider1.SetError(this.buttonClose, resources.GetString("buttonClose.Error"));
            this.errorProvider1.SetIconAlignment(this.buttonClose, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("buttonClose.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.buttonClose, ((int)(resources.GetObject("buttonClose.IconPadding"))));
            this.buttonClose.Name = "buttonClose";
            this.toolTip1.SetToolTip(this.buttonClose, resources.GetString("buttonClose.ToolTip"));
            this.buttonClose.UseVisualStyleBackColor = true;
            // 
            // buttonOk
            // 
            resources.ApplyResources(this.buttonOk, "buttonOk");
            this.errorProvider1.SetError(this.buttonOk, resources.GetString("buttonOk.Error"));
            this.errorProvider1.SetIconAlignment(this.buttonOk, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("buttonOk.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.buttonOk, ((int)(resources.GetObject("buttonOk.IconPadding"))));
            this.buttonOk.Image = global::AMSExplorer.Bitmaps.subclipping;
            this.buttonOk.Name = "buttonOk";
            this.toolTip1.SetToolTip(this.buttonOk, resources.GetString("buttonOk.ToolTip"));
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            resources.ApplyResources(this.errorProvider1, "errorProvider1");
            // 
            // moreinfoprofilelink
            // 
            resources.ApplyResources(this.moreinfoprofilelink, "moreinfoprofilelink");
            this.errorProvider1.SetError(this.moreinfoprofilelink, resources.GetString("moreinfoprofilelink.Error"));
            this.errorProvider1.SetIconAlignment(this.moreinfoprofilelink, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("moreinfoprofilelink.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.moreinfoprofilelink, ((int)(resources.GetObject("moreinfoprofilelink.IconPadding"))));
            this.moreinfoprofilelink.Name = "moreinfoprofilelink";
            this.moreinfoprofilelink.TabStop = true;
            this.toolTip1.SetToolTip(this.moreinfoprofilelink, resources.GetString("moreinfoprofilelink.ToolTip"));
            this.moreinfoprofilelink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.moreinfoprofilelink_LinkClicked);
            // 
            // textBoxAssetName
            // 
            resources.ApplyResources(this.textBoxAssetName, "textBoxAssetName");
            this.errorProvider1.SetError(this.textBoxAssetName, resources.GetString("textBoxAssetName.Error"));
            this.errorProvider1.SetIconAlignment(this.textBoxAssetName, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("textBoxAssetName.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.textBoxAssetName, ((int)(resources.GetObject("textBoxAssetName.IconPadding"))));
            this.textBoxAssetName.Name = "textBoxAssetName";
            this.textBoxAssetName.ReadOnly = true;
            this.toolTip1.SetToolTip(this.textBoxAssetName, resources.GetString("textBoxAssetName.ToolTip"));
            // 
            // labelassetname
            // 
            resources.ApplyResources(this.labelassetname, "labelassetname");
            this.errorProvider1.SetError(this.labelassetname, resources.GetString("labelassetname.Error"));
            this.labelassetname.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.errorProvider1.SetIconAlignment(this.labelassetname, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("labelassetname.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.labelassetname, ((int)(resources.GetObject("labelassetname.IconPadding"))));
            this.labelassetname.Name = "labelassetname";
            this.toolTip1.SetToolTip(this.labelassetname, resources.GetString("labelassetname.ToolTip"));
            // 
            // textBoxAssetDuration
            // 
            resources.ApplyResources(this.textBoxAssetDuration, "textBoxAssetDuration");
            this.errorProvider1.SetError(this.textBoxAssetDuration, resources.GetString("textBoxAssetDuration.Error"));
            this.errorProvider1.SetIconAlignment(this.textBoxAssetDuration, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("textBoxAssetDuration.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.textBoxAssetDuration, ((int)(resources.GetObject("textBoxAssetDuration.IconPadding"))));
            this.textBoxAssetDuration.Name = "textBoxAssetDuration";
            this.textBoxAssetDuration.ReadOnly = true;
            this.toolTip1.SetToolTip(this.textBoxAssetDuration, resources.GetString("textBoxAssetDuration.ToolTip"));
            // 
            // labelassetduration
            // 
            resources.ApplyResources(this.labelassetduration, "labelassetduration");
            this.errorProvider1.SetError(this.labelassetduration, resources.GetString("labelassetduration.Error"));
            this.labelassetduration.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.errorProvider1.SetIconAlignment(this.labelassetduration, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("labelassetduration.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.labelassetduration, ((int)(resources.GetObject("labelassetduration.IconPadding"))));
            this.labelassetduration.Name = "labelassetduration";
            this.toolTip1.SetToolTip(this.labelassetduration, resources.GetString("labelassetduration.ToolTip"));
            // 
            // textBoxFilterTimeScale
            // 
            resources.ApplyResources(this.textBoxFilterTimeScale, "textBoxFilterTimeScale");
            this.errorProvider1.SetError(this.textBoxFilterTimeScale, resources.GetString("textBoxFilterTimeScale.Error"));
            this.errorProvider1.SetIconAlignment(this.textBoxFilterTimeScale, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("textBoxFilterTimeScale.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.textBoxFilterTimeScale, ((int)(resources.GetObject("textBoxFilterTimeScale.IconPadding"))));
            this.textBoxFilterTimeScale.Name = "textBoxFilterTimeScale";
            this.textBoxFilterTimeScale.ReadOnly = true;
            this.toolTip1.SetToolTip(this.textBoxFilterTimeScale, resources.GetString("textBoxFilterTimeScale.ToolTip"));
            // 
            // labelAssetTimescale
            // 
            resources.ApplyResources(this.labelAssetTimescale, "labelAssetTimescale");
            this.errorProvider1.SetError(this.labelAssetTimescale, resources.GetString("labelAssetTimescale.Error"));
            this.labelAssetTimescale.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.errorProvider1.SetIconAlignment(this.labelAssetTimescale, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("labelAssetTimescale.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.labelAssetTimescale, ((int)(resources.GetObject("labelAssetTimescale.IconPadding"))));
            this.labelAssetTimescale.Name = "labelAssetTimescale";
            this.toolTip1.SetToolTip(this.labelAssetTimescale, resources.GetString("labelAssetTimescale.ToolTip"));
            // 
            // textBoxOffset
            // 
            resources.ApplyResources(this.textBoxOffset, "textBoxOffset");
            this.errorProvider1.SetError(this.textBoxOffset, resources.GetString("textBoxOffset.Error"));
            this.errorProvider1.SetIconAlignment(this.textBoxOffset, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("textBoxOffset.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.textBoxOffset, ((int)(resources.GetObject("textBoxOffset.IconPadding"))));
            this.textBoxOffset.Name = "textBoxOffset";
            this.textBoxOffset.ReadOnly = true;
            this.toolTip1.SetToolTip(this.textBoxOffset, resources.GetString("textBoxOffset.ToolTip"));
            // 
            // labelOffset
            // 
            resources.ApplyResources(this.labelOffset, "labelOffset");
            this.errorProvider1.SetError(this.labelOffset, resources.GetString("labelOffset.Error"));
            this.labelOffset.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.errorProvider1.SetIconAlignment(this.labelOffset, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("labelOffset.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.labelOffset, ((int)(resources.GetObject("labelOffset.IconPadding"))));
            this.labelOffset.Name = "labelOffset";
            this.toolTip1.SetToolTip(this.labelOffset, resources.GetString("labelOffset.ToolTip"));
            // 
            // tabPageJSON
            // 
            resources.ApplyResources(this.tabPageJSON, "tabPageJSON");
            this.tabPageJSON.Controls.Add(this.label3);
            this.tabPageJSON.Controls.Add(this.textBoxConfiguration);
            this.errorProvider1.SetError(this.tabPageJSON, resources.GetString("tabPageJSON.Error"));
            this.errorProvider1.SetIconAlignment(this.tabPageJSON, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("tabPageJSON.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.tabPageJSON, ((int)(resources.GetObject("tabPageJSON.IconPadding"))));
            this.tabPageJSON.Name = "tabPageJSON";
            this.toolTip1.SetToolTip(this.tabPageJSON, resources.GetString("tabPageJSON.ToolTip"));
            this.tabPageJSON.UseVisualStyleBackColor = true;
            this.tabPageJSON.Enter += new System.EventHandler(this.tabPageXML_Enter);
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
            // textBoxConfiguration
            // 
            resources.ApplyResources(this.textBoxConfiguration, "textBoxConfiguration");
            this.errorProvider1.SetError(this.textBoxConfiguration, resources.GetString("textBoxConfiguration.Error"));
            this.errorProvider1.SetIconAlignment(this.textBoxConfiguration, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("textBoxConfiguration.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.textBoxConfiguration, ((int)(resources.GetObject("textBoxConfiguration.IconPadding"))));
            this.textBoxConfiguration.Name = "textBoxConfiguration";
            this.toolTip1.SetToolTip(this.textBoxConfiguration, resources.GetString("textBoxConfiguration.ToolTip"));
            // 
            // tabPageTR
            // 
            resources.ApplyResources(this.tabPageTR, "tabPageTR");
            this.tabPageTR.Controls.Add(this.panelAssetInfo);
            this.tabPageTR.Controls.Add(this.groupBoxTrimming);
            this.tabPageTR.Controls.Add(this.groupBox2);
            this.errorProvider1.SetError(this.tabPageTR, resources.GetString("tabPageTR.Error"));
            this.errorProvider1.SetIconAlignment(this.tabPageTR, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("tabPageTR.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.tabPageTR, ((int)(resources.GetObject("tabPageTR.IconPadding"))));
            this.tabPageTR.Name = "tabPageTR";
            this.toolTip1.SetToolTip(this.tabPageTR, resources.GetString("tabPageTR.ToolTip"));
            this.tabPageTR.UseVisualStyleBackColor = true;
            this.tabPageTR.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // panelAssetInfo
            // 
            resources.ApplyResources(this.panelAssetInfo, "panelAssetInfo");
            this.panelAssetInfo.Controls.Add(this.labelDiscountinuity);
            this.panelAssetInfo.Controls.Add(this.labelassetname);
            this.panelAssetInfo.Controls.Add(this.textBoxAssetDuration);
            this.panelAssetInfo.Controls.Add(this.textBoxOffset);
            this.panelAssetInfo.Controls.Add(this.labelOffset);
            this.panelAssetInfo.Controls.Add(this.textBoxFilterTimeScale);
            this.panelAssetInfo.Controls.Add(this.textBoxAssetName);
            this.panelAssetInfo.Controls.Add(this.labelassetduration);
            this.panelAssetInfo.Controls.Add(this.labelAssetTimescale);
            this.errorProvider1.SetError(this.panelAssetInfo, resources.GetString("panelAssetInfo.Error"));
            this.errorProvider1.SetIconAlignment(this.panelAssetInfo, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("panelAssetInfo.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.panelAssetInfo, ((int)(resources.GetObject("panelAssetInfo.IconPadding"))));
            this.panelAssetInfo.Name = "panelAssetInfo";
            this.toolTip1.SetToolTip(this.panelAssetInfo, resources.GetString("panelAssetInfo.ToolTip"));
            // 
            // labelDiscountinuity
            // 
            resources.ApplyResources(this.labelDiscountinuity, "labelDiscountinuity");
            this.errorProvider1.SetError(this.labelDiscountinuity, resources.GetString("labelDiscountinuity.Error"));
            this.labelDiscountinuity.ForeColor = System.Drawing.Color.Red;
            this.errorProvider1.SetIconAlignment(this.labelDiscountinuity, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("labelDiscountinuity.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.labelDiscountinuity, ((int)(resources.GetObject("labelDiscountinuity.IconPadding"))));
            this.labelDiscountinuity.Name = "labelDiscountinuity";
            this.toolTip1.SetToolTip(this.labelDiscountinuity, resources.GetString("labelDiscountinuity.ToolTip"));
            // 
            // groupBoxTrimming
            // 
            resources.ApplyResources(this.groupBoxTrimming, "groupBoxTrimming");
            this.groupBoxTrimming.Controls.Add(this.labelAccurate);
            this.groupBoxTrimming.Controls.Add(this.label7);
            this.groupBoxTrimming.Controls.Add(this.webBrowserPreview);
            this.groupBoxTrimming.Controls.Add(this.timeControlStart);
            this.groupBoxTrimming.Controls.Add(this.textBoxDurationTime);
            this.groupBoxTrimming.Controls.Add(this.timeControlEnd);
            this.groupBoxTrimming.Controls.Add(this.checkBoxPreviewStream);
            this.groupBoxTrimming.Controls.Add(this.checkBoxTrimming);
            this.errorProvider1.SetError(this.groupBoxTrimming, resources.GetString("groupBoxTrimming.Error"));
            this.groupBoxTrimming.ForeColor = System.Drawing.Color.DarkBlue;
            this.errorProvider1.SetIconAlignment(this.groupBoxTrimming, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("groupBoxTrimming.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.groupBoxTrimming, ((int)(resources.GetObject("groupBoxTrimming.IconPadding"))));
            this.groupBoxTrimming.Name = "groupBoxTrimming";
            this.groupBoxTrimming.TabStop = false;
            this.toolTip1.SetToolTip(this.groupBoxTrimming, resources.GetString("groupBoxTrimming.ToolTip"));
            // 
            // labelAccurate
            // 
            resources.ApplyResources(this.labelAccurate, "labelAccurate");
            this.errorProvider1.SetError(this.labelAccurate, resources.GetString("labelAccurate.Error"));
            this.labelAccurate.ForeColor = System.Drawing.SystemColors.WindowText;
            this.errorProvider1.SetIconAlignment(this.labelAccurate, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("labelAccurate.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.labelAccurate, ((int)(resources.GetObject("labelAccurate.IconPadding"))));
            this.labelAccurate.Name = "labelAccurate";
            this.toolTip1.SetToolTip(this.labelAccurate, resources.GetString("labelAccurate.ToolTip"));
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.errorProvider1.SetError(this.label7, resources.GetString("label7.Error"));
            this.label7.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.errorProvider1.SetIconAlignment(this.label7, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label7.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label7, ((int)(resources.GetObject("label7.IconPadding"))));
            this.label7.Name = "label7";
            this.toolTip1.SetToolTip(this.label7, resources.GetString("label7.ToolTip"));
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
            // 
            // timeControlStart
            // 
            resources.ApplyResources(this.timeControlStart, "timeControlStart");
            this.timeControlStart.BackColor = System.Drawing.SystemColors.Window;
            this.timeControlStart.DisplayTrackBar = true;
            this.errorProvider1.SetError(this.timeControlStart, resources.GetString("timeControlStart.Error"));
            this.timeControlStart.ForeColor = System.Drawing.SystemColors.WindowText;
            this.errorProvider1.SetIconAlignment(this.timeControlStart, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("timeControlStart.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.timeControlStart, ((int)(resources.GetObject("timeControlStart.IconPadding"))));
            this.timeControlStart.Label1 = "";
            this.timeControlStart.Label2 = "Start time :";
            this.timeControlStart.Max = System.TimeSpan.Parse("10675199.02:48:05.4775807");
            this.timeControlStart.Min = System.TimeSpan.Parse("00:00:00");
            this.timeControlStart.Name = "timeControlStart";
            this.timeControlStart.ScaledFirstTimestampOffset = ((ulong)(0ul));
            this.timeControlStart.TimeScale = null;
            this.toolTip1.SetToolTip(this.timeControlStart, resources.GetString("timeControlStart.ToolTip"));
            this.timeControlStart.TotalDuration = System.TimeSpan.Parse("00:00:00");
            this.timeControlStart.ValueChanged += new System.EventHandler(this.timeControlStart_ValueChanged);
            // 
            // textBoxDurationTime
            // 
            resources.ApplyResources(this.textBoxDurationTime, "textBoxDurationTime");
            this.errorProvider1.SetError(this.textBoxDurationTime, resources.GetString("textBoxDurationTime.Error"));
            this.errorProvider1.SetIconAlignment(this.textBoxDurationTime, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("textBoxDurationTime.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.textBoxDurationTime, ((int)(resources.GetObject("textBoxDurationTime.IconPadding"))));
            this.textBoxDurationTime.Name = "textBoxDurationTime";
            this.textBoxDurationTime.ReadOnly = true;
            this.toolTip1.SetToolTip(this.textBoxDurationTime, resources.GetString("textBoxDurationTime.ToolTip"));
            // 
            // timeControlEnd
            // 
            resources.ApplyResources(this.timeControlEnd, "timeControlEnd");
            this.timeControlEnd.BackColor = System.Drawing.SystemColors.Window;
            this.timeControlEnd.DisplayTrackBar = true;
            this.errorProvider1.SetError(this.timeControlEnd, resources.GetString("timeControlEnd.Error"));
            this.timeControlEnd.ForeColor = System.Drawing.SystemColors.WindowText;
            this.errorProvider1.SetIconAlignment(this.timeControlEnd, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("timeControlEnd.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.timeControlEnd, ((int)(resources.GetObject("timeControlEnd.IconPadding"))));
            this.timeControlEnd.Label1 = "";
            this.timeControlEnd.Label2 = "End time :";
            this.timeControlEnd.Max = System.TimeSpan.Parse("10675199.02:48:05.4775807");
            this.timeControlEnd.Min = System.TimeSpan.Parse("00:00:00");
            this.timeControlEnd.Name = "timeControlEnd";
            this.timeControlEnd.ScaledFirstTimestampOffset = ((ulong)(0ul));
            this.timeControlEnd.TimeScale = null;
            this.toolTip1.SetToolTip(this.timeControlEnd, resources.GetString("timeControlEnd.ToolTip"));
            this.timeControlEnd.TotalDuration = System.TimeSpan.Parse("00:00:00");
            this.timeControlEnd.ValueChanged += new System.EventHandler(this.timeControlEnd_ValueChanged);
            // 
            // checkBoxPreviewStream
            // 
            resources.ApplyResources(this.checkBoxPreviewStream, "checkBoxPreviewStream");
            this.checkBoxPreviewStream.Checked = true;
            this.checkBoxPreviewStream.CheckState = System.Windows.Forms.CheckState.Checked;
            this.errorProvider1.SetError(this.checkBoxPreviewStream, resources.GetString("checkBoxPreviewStream.Error"));
            this.checkBoxPreviewStream.ForeColor = System.Drawing.SystemColors.WindowText;
            this.errorProvider1.SetIconAlignment(this.checkBoxPreviewStream, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("checkBoxPreviewStream.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.checkBoxPreviewStream, ((int)(resources.GetObject("checkBoxPreviewStream.IconPadding"))));
            this.checkBoxPreviewStream.Name = "checkBoxPreviewStream";
            this.toolTip1.SetToolTip(this.checkBoxPreviewStream, resources.GetString("checkBoxPreviewStream.ToolTip"));
            this.checkBoxPreviewStream.UseVisualStyleBackColor = true;
            this.checkBoxPreviewStream.CheckedChanged += new System.EventHandler(this.checkBoxPreviewStream_CheckedChanged_1);
            // 
            // checkBoxTrimming
            // 
            resources.ApplyResources(this.checkBoxTrimming, "checkBoxTrimming");
            this.errorProvider1.SetError(this.checkBoxTrimming, resources.GetString("checkBoxTrimming.Error"));
            this.checkBoxTrimming.ForeColor = System.Drawing.SystemColors.WindowText;
            this.errorProvider1.SetIconAlignment(this.checkBoxTrimming, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("checkBoxTrimming.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.checkBoxTrimming, ((int)(resources.GetObject("checkBoxTrimming.IconPadding"))));
            this.checkBoxTrimming.Name = "checkBoxTrimming";
            this.toolTip1.SetToolTip(this.checkBoxTrimming, resources.GetString("checkBoxTrimming.ToolTip"));
            this.checkBoxTrimming.UseVisualStyleBackColor = true;
            this.checkBoxTrimming.CheckedChanged += new System.EventHandler(this.checkBoxTrimming_CheckedChanged);
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.radioButtonAssetFilter);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.radioButtonArchiveTopBitrate);
            this.groupBox2.Controls.Add(this.radioButtonClipWithReencode);
            this.errorProvider1.SetError(this.groupBox2, resources.GetString("groupBox2.Error"));
            this.groupBox2.ForeColor = System.Drawing.Color.DarkBlue;
            this.errorProvider1.SetIconAlignment(this.groupBox2, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("groupBox2.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.groupBox2, ((int)(resources.GetObject("groupBox2.IconPadding"))));
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            this.toolTip1.SetToolTip(this.groupBox2, resources.GetString("groupBox2.ToolTip"));
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.errorProvider1.SetError(this.label2, resources.GetString("label2.Error"));
            this.label2.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.errorProvider1.SetIconAlignment(this.label2, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label2.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label2, ((int)(resources.GetObject("label2.IconPadding"))));
            this.label2.Name = "label2";
            this.toolTip1.SetToolTip(this.label2, resources.GetString("label2.ToolTip"));
            // 
            // radioButtonAssetFilter
            // 
            resources.ApplyResources(this.radioButtonAssetFilter, "radioButtonAssetFilter");
            this.errorProvider1.SetError(this.radioButtonAssetFilter, resources.GetString("radioButtonAssetFilter.Error"));
            this.radioButtonAssetFilter.ForeColor = System.Drawing.SystemColors.WindowText;
            this.errorProvider1.SetIconAlignment(this.radioButtonAssetFilter, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("radioButtonAssetFilter.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.radioButtonAssetFilter, ((int)(resources.GetObject("radioButtonAssetFilter.IconPadding"))));
            this.radioButtonAssetFilter.Name = "radioButtonAssetFilter";
            this.toolTip1.SetToolTip(this.radioButtonAssetFilter, resources.GetString("radioButtonAssetFilter.ToolTip"));
            this.radioButtonAssetFilter.UseVisualStyleBackColor = true;
            this.radioButtonAssetFilter.CheckedChanged += new System.EventHandler(this.radioButtonClipWithReencode_CheckedChanged);
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.errorProvider1.SetError(this.label12, resources.GetString("label12.Error"));
            this.label12.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.errorProvider1.SetIconAlignment(this.label12, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label12.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label12, ((int)(resources.GetObject("label12.IconPadding"))));
            this.label12.Name = "label12";
            this.toolTip1.SetToolTip(this.label12, resources.GetString("label12.ToolTip"));
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.errorProvider1.SetError(this.label10, resources.GetString("label10.Error"));
            this.label10.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.errorProvider1.SetIconAlignment(this.label10, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label10.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label10, ((int)(resources.GetObject("label10.IconPadding"))));
            this.label10.Name = "label10";
            this.toolTip1.SetToolTip(this.label10, resources.GetString("label10.ToolTip"));
            // 
            // radioButtonArchiveTopBitrate
            // 
            resources.ApplyResources(this.radioButtonArchiveTopBitrate, "radioButtonArchiveTopBitrate");
            this.radioButtonArchiveTopBitrate.Checked = true;
            this.errorProvider1.SetError(this.radioButtonArchiveTopBitrate, resources.GetString("radioButtonArchiveTopBitrate.Error"));
            this.radioButtonArchiveTopBitrate.ForeColor = System.Drawing.SystemColors.WindowText;
            this.errorProvider1.SetIconAlignment(this.radioButtonArchiveTopBitrate, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("radioButtonArchiveTopBitrate.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.radioButtonArchiveTopBitrate, ((int)(resources.GetObject("radioButtonArchiveTopBitrate.IconPadding"))));
            this.radioButtonArchiveTopBitrate.Name = "radioButtonArchiveTopBitrate";
            this.radioButtonArchiveTopBitrate.TabStop = true;
            this.toolTip1.SetToolTip(this.radioButtonArchiveTopBitrate, resources.GetString("radioButtonArchiveTopBitrate.ToolTip"));
            this.radioButtonArchiveTopBitrate.UseVisualStyleBackColor = true;
            this.radioButtonArchiveTopBitrate.CheckedChanged += new System.EventHandler(this.radioButtonClipWithReencode_CheckedChanged);
            // 
            // radioButtonClipWithReencode
            // 
            resources.ApplyResources(this.radioButtonClipWithReencode, "radioButtonClipWithReencode");
            this.errorProvider1.SetError(this.radioButtonClipWithReencode, resources.GetString("radioButtonClipWithReencode.Error"));
            this.radioButtonClipWithReencode.ForeColor = System.Drawing.SystemColors.WindowText;
            this.errorProvider1.SetIconAlignment(this.radioButtonClipWithReencode, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("radioButtonClipWithReencode.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.radioButtonClipWithReencode, ((int)(resources.GetObject("radioButtonClipWithReencode.IconPadding"))));
            this.radioButtonClipWithReencode.Name = "radioButtonClipWithReencode";
            this.toolTip1.SetToolTip(this.radioButtonClipWithReencode, resources.GetString("radioButtonClipWithReencode.ToolTip"));
            this.radioButtonClipWithReencode.UseVisualStyleBackColor = true;
            this.radioButtonClipWithReencode.CheckedChanged += new System.EventHandler(this.radioButtonClipWithReencode_CheckedChanged);
            // 
            // panelEDL
            // 
            resources.ApplyResources(this.panelEDL, "panelEDL");
            this.panelEDL.Controls.Add(this.buttonAddEDLEntry);
            this.panelEDL.Controls.Add(this.buttonShowEDL);
            this.panelEDL.Controls.Add(this.checkBoxUseEDL);
            this.errorProvider1.SetError(this.panelEDL, resources.GetString("panelEDL.Error"));
            this.errorProvider1.SetIconAlignment(this.panelEDL, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("panelEDL.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.panelEDL, ((int)(resources.GetObject("panelEDL.IconPadding"))));
            this.panelEDL.Name = "panelEDL";
            this.toolTip1.SetToolTip(this.panelEDL, resources.GetString("panelEDL.ToolTip"));
            // 
            // buttonAddEDLEntry
            // 
            resources.ApplyResources(this.buttonAddEDLEntry, "buttonAddEDLEntry");
            this.errorProvider1.SetError(this.buttonAddEDLEntry, resources.GetString("buttonAddEDLEntry.Error"));
            this.errorProvider1.SetIconAlignment(this.buttonAddEDLEntry, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("buttonAddEDLEntry.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.buttonAddEDLEntry, ((int)(resources.GetObject("buttonAddEDLEntry.IconPadding"))));
            this.buttonAddEDLEntry.Name = "buttonAddEDLEntry";
            this.toolTip1.SetToolTip(this.buttonAddEDLEntry, resources.GetString("buttonAddEDLEntry.ToolTip"));
            this.buttonAddEDLEntry.UseVisualStyleBackColor = true;
            this.buttonAddEDLEntry.Click += new System.EventHandler(this.button2_Click);
            // 
            // buttonShowEDL
            // 
            resources.ApplyResources(this.buttonShowEDL, "buttonShowEDL");
            this.errorProvider1.SetError(this.buttonShowEDL, resources.GetString("buttonShowEDL.Error"));
            this.errorProvider1.SetIconAlignment(this.buttonShowEDL, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("buttonShowEDL.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.buttonShowEDL, ((int)(resources.GetObject("buttonShowEDL.IconPadding"))));
            this.buttonShowEDL.Name = "buttonShowEDL";
            this.buttonShowEDL.Offset = System.TimeSpan.Parse("00:00:00");
            this.toolTip1.SetToolTip(this.buttonShowEDL, resources.GetString("buttonShowEDL.ToolTip"));
            this.buttonShowEDL.UseVisualStyleBackColor = true;
            // 
            // checkBoxUseEDL
            // 
            resources.ApplyResources(this.checkBoxUseEDL, "checkBoxUseEDL");
            this.errorProvider1.SetError(this.checkBoxUseEDL, resources.GetString("checkBoxUseEDL.Error"));
            this.errorProvider1.SetIconAlignment(this.checkBoxUseEDL, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("checkBoxUseEDL.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.checkBoxUseEDL, ((int)(resources.GetObject("checkBoxUseEDL.IconPadding"))));
            this.checkBoxUseEDL.Name = "checkBoxUseEDL";
            this.toolTip1.SetToolTip(this.checkBoxUseEDL, resources.GetString("checkBoxUseEDL.ToolTip"));
            this.checkBoxUseEDL.UseVisualStyleBackColor = true;
            this.checkBoxUseEDL.CheckedChanged += new System.EventHandler(this.checkBoxUseEDL_CheckedChanged);
            // 
            // tabControl1
            // 
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Controls.Add(this.tabPageTR);
            this.tabControl1.Controls.Add(this.tabPageJSON);
            this.errorProvider1.SetError(this.tabControl1, resources.GetString("tabControl1.Error"));
            this.errorProvider1.SetIconAlignment(this.tabControl1, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("tabControl1.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.tabControl1, ((int)(resources.GetObject("tabControl1.IconPadding"))));
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.toolTip1.SetToolTip(this.tabControl1, resources.GetString("tabControl1.ToolTip"));
            // 
            // labeloutputasset
            // 
            resources.ApplyResources(this.labeloutputasset, "labeloutputasset");
            this.errorProvider1.SetError(this.labeloutputasset, resources.GetString("labeloutputasset.Error"));
            this.errorProvider1.SetIconAlignment(this.labeloutputasset, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("labeloutputasset.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.labeloutputasset, ((int)(resources.GetObject("labeloutputasset.IconPadding"))));
            this.labeloutputasset.Name = "labeloutputasset";
            this.toolTip1.SetToolTip(this.labeloutputasset, resources.GetString("labeloutputasset.ToolTip"));
            // 
            // textboxoutputassetname
            // 
            resources.ApplyResources(this.textboxoutputassetname, "textboxoutputassetname");
            this.errorProvider1.SetError(this.textboxoutputassetname, resources.GetString("textboxoutputassetname.Error"));
            this.errorProvider1.SetIconAlignment(this.textboxoutputassetname, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("textboxoutputassetname.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.textboxoutputassetname, ((int)(resources.GetObject("textboxoutputassetname.IconPadding"))));
            this.textboxoutputassetname.Name = "textboxoutputassetname";
            this.toolTip1.SetToolTip(this.textboxoutputassetname, resources.GetString("textboxoutputassetname.ToolTip"));
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.errorProvider1.SetError(this.label5, resources.GetString("label5.Error"));
            this.errorProvider1.SetIconAlignment(this.label5, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label5.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label5, ((int)(resources.GetObject("label5.IconPadding"))));
            this.label5.Name = "label5";
            this.toolTip1.SetToolTip(this.label5, resources.GetString("label5.ToolTip"));
            // 
            // textBoxJobName
            // 
            resources.ApplyResources(this.textBoxJobName, "textBoxJobName");
            this.errorProvider1.SetError(this.textBoxJobName, resources.GetString("textBoxJobName.Error"));
            this.errorProvider1.SetIconAlignment(this.textBoxJobName, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("textBoxJobName.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.textBoxJobName, ((int)(resources.GetObject("textBoxJobName.IconPadding"))));
            this.textBoxJobName.Name = "textBoxJobName";
            this.toolTip1.SetToolTip(this.textBoxJobName, resources.GetString("textBoxJobName.ToolTip"));
            // 
            // panelJob
            // 
            resources.ApplyResources(this.panelJob, "panelJob");
            this.panelJob.Controls.Add(this.panelEDL);
            this.panelJob.Controls.Add(this.textboxoutputassetname);
            this.panelJob.Controls.Add(this.textBoxJobName);
            this.panelJob.Controls.Add(this.labeloutputasset);
            this.panelJob.Controls.Add(this.label5);
            this.errorProvider1.SetError(this.panelJob, resources.GetString("panelJob.Error"));
            this.errorProvider1.SetIconAlignment(this.panelJob, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("panelJob.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.panelJob, ((int)(resources.GetObject("panelJob.IconPadding"))));
            this.panelJob.Name = "panelJob";
            this.toolTip1.SetToolTip(this.panelJob, resources.GetString("panelJob.ToolTip"));
            // 
            // labelGen
            // 
            resources.ApplyResources(this.labelGen, "labelGen");
            this.errorProvider1.SetError(this.labelGen, resources.GetString("labelGen.Error"));
            this.labelGen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(128)))), ((int)(((byte)(171)))));
            this.errorProvider1.SetIconAlignment(this.labelGen, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("labelGen.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.labelGen, ((int)(resources.GetObject("labelGen.IconPadding"))));
            this.labelGen.Name = "labelGen";
            this.toolTip1.SetToolTip(this.labelGen, resources.GetString("labelGen.ToolTip"));
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.tabControl1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panelJob, 0, 1);
            this.errorProvider1.SetError(this.tableLayoutPanel1, resources.GetString("tableLayoutPanel1.Error"));
            this.errorProvider1.SetIconAlignment(this.tableLayoutPanel1, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("tableLayoutPanel1.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.tableLayoutPanel1, ((int)(resources.GetObject("tableLayoutPanel1.IconPadding"))));
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.toolTip1.SetToolTip(this.tableLayoutPanel1, resources.GetString("tableLayoutPanel1.ToolTip"));
            // 
            // Subclipping
            // 
            this.AcceptButton = this.buttonOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonClose;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.labelGen);
            this.Controls.Add(this.moreinfoprofilelink);
            this.Controls.Add(this.panel1);
            this.Name = "Subclipping";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.toolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Subclipping_FormClosed);
            this.Load += new System.EventHandler(this.Subclipping_Load);
            this.Shown += new System.EventHandler(this.Subclipping_Shown);
            this.DpiChanged += new System.Windows.Forms.DpiChangedEventHandler(this.Subclipping_DpiChanged);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.tabPageJSON.ResumeLayout(false);
            this.tabPageJSON.PerformLayout();
            this.tabPageTR.ResumeLayout(false);
            this.panelAssetInfo.ResumeLayout(false);
            this.panelAssetInfo.PerformLayout();
            this.groupBoxTrimming.ResumeLayout(false);
            this.groupBoxTrimming.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panelEDL.ResumeLayout(false);
            this.panelEDL.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.panelJob.ResumeLayout(false);
            this.panelJob.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
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
        private System.Windows.Forms.RadioButton radioButtonArchiveTopBitrate;
        private TimeControl timeControlStart;
        private TimeControl timeControlEnd;
        private System.Windows.Forms.TabPage tabPageJSON;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkBoxTrimming;
        private System.Windows.Forms.GroupBox groupBoxTrimming;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label10;
        public System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxConfiguration;
        public System.Windows.Forms.Label labeloutputasset;
        private System.Windows.Forms.TextBox textboxoutputassetname;
        public System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxJobName;
        private System.Windows.Forms.Panel panelJob;
        public System.Windows.Forms.Label labelGen;
        private System.Windows.Forms.Label labelAccurate;
        private System.Windows.Forms.CheckBox checkBoxPreviewStream;
        private System.Windows.Forms.WebBrowser webBrowserPreview;
        private System.Windows.Forms.Panel panelAssetInfo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton radioButtonAssetFilter;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.TextBox textBoxDurationTime;
        public System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button buttonAddEDLEntry;
        private ButtonEDL buttonShowEDL;
        private System.Windows.Forms.CheckBox checkBoxUseEDL;
        private System.Windows.Forms.Panel panelEDL;
        private System.Windows.Forms.Label labelDiscountinuity;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}