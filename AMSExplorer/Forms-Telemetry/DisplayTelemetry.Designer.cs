namespace AMSExplorer
{
    partial class DisplayTelemetry
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DisplayTelemetry));
            this.contextMenuStripDG = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemFilesCopyClipboard = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonClose = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.openFileDialogSlate = new System.Windows.Forms.OpenFileDialog();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.moreinfoLiveEncodingProfilelink = new System.Windows.Forms.LinkLabel();
            this.labelTelemetryUI = new System.Windows.Forms.Label();
            this.dataGridViewTelemetry = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.labelTimeRange = new System.Windows.Forms.Label();
            this.checkBoxShowOnlyErrors = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.radioButtonLocal = new System.Windows.Forms.RadioButton();
            this.radioButtonUTC = new System.Windows.Forms.RadioButton();
            this.contextMenuStripDG.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTelemetry)).BeginInit();
            this.SuspendLayout();
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
            // labelTelemetryUI
            // 
            resources.ApplyResources(this.labelTelemetryUI, "labelTelemetryUI");
            this.labelTelemetryUI.ForeColor = System.Drawing.Color.DarkBlue;
            this.labelTelemetryUI.Name = "labelTelemetryUI";
            // 
            // dataGridViewTelemetry
            // 
            this.dataGridViewTelemetry.AllowUserToAddRows = false;
            this.dataGridViewTelemetry.AllowUserToDeleteRows = false;
            this.dataGridViewTelemetry.AllowUserToResizeRows = false;
            resources.ApplyResources(this.dataGridViewTelemetry, "dataGridViewTelemetry");
            this.dataGridViewTelemetry.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTelemetry.Name = "dataGridViewTelemetry";
            this.dataGridViewTelemetry.ReadOnly = true;
            this.dataGridViewTelemetry.RowHeadersVisible = false;
            this.dataGridViewTelemetry.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewTelemetry.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridViewTelemetry_CellFormatting);
            this.dataGridViewTelemetry.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridViewTelemetry_RowPostPaint);
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonRefresh
            // 
            resources.ApplyResources(this.buttonRefresh, "buttonRefresh");
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.UseVisualStyleBackColor = true;
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // labelTimeRange
            // 
            resources.ApplyResources(this.labelTimeRange, "labelTimeRange");
            this.labelTimeRange.Name = "labelTimeRange";
            // 
            // checkBoxShowOnlyErrors
            // 
            resources.ApplyResources(this.checkBoxShowOnlyErrors, "checkBoxShowOnlyErrors");
            this.checkBoxShowOnlyErrors.Name = "checkBoxShowOnlyErrors";
            this.checkBoxShowOnlyErrors.UseVisualStyleBackColor = true;
            this.checkBoxShowOnlyErrors.CheckedChanged += new System.EventHandler(this.checkBoxShowOnlyErrors_CheckedChanged);
            // 
            // panel2
            // 
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Name = "panel2";
            // 
            // radioButtonLocal
            // 
            resources.ApplyResources(this.radioButtonLocal, "radioButtonLocal");
            this.radioButtonLocal.Checked = true;
            this.radioButtonLocal.Name = "radioButtonLocal";
            this.radioButtonLocal.TabStop = true;
            this.radioButtonLocal.UseVisualStyleBackColor = true;
            this.radioButtonLocal.CheckedChanged += new System.EventHandler(this.radioButtonLocal_CheckedChanged);
            // 
            // radioButtonUTC
            // 
            resources.ApplyResources(this.radioButtonUTC, "radioButtonUTC");
            this.radioButtonUTC.Name = "radioButtonUTC";
            this.radioButtonUTC.UseVisualStyleBackColor = true;
            // 
            // DisplayTelemetry
            // 
            this.AcceptButton = this.buttonClose;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonClose;
            this.Controls.Add(this.radioButtonUTC);
            this.Controls.Add(this.radioButtonLocal);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.checkBoxShowOnlyErrors);
            this.Controls.Add(this.labelTimeRange);
            this.Controls.Add(this.buttonRefresh);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridViewTelemetry);
            this.Controls.Add(this.labelTelemetryUI);
            this.Controls.Add(this.moreinfoLiveEncodingProfilelink);
            this.Controls.Add(this.panel1);
            this.Name = "DisplayTelemetry";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ChannelAdSlateControl_FormClosed);
            this.Load += new System.EventHandler(this.DisplayTelemetry_Load);
            this.Shown += new System.EventHandler(this.DisplayTelemetry_Shown);
            this.contextMenuStripDG.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTelemetry)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStripDG;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFilesCopyClipboard;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.OpenFileDialog openFileDialogSlate;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.LinkLabel moreinfoLiveEncodingProfilelink;
        private System.Windows.Forms.Label labelTelemetryUI;
        private System.Windows.Forms.DataGridView dataGridViewTelemetry;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.Label labelTimeRange;
        private System.Windows.Forms.CheckBox checkBoxShowOnlyErrors;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton radioButtonUTC;
        private System.Windows.Forms.RadioButton radioButtonLocal;
    }
}