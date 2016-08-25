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
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonClose.Location = new System.Drawing.Point(748, 12);
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
            this.panel1.Size = new System.Drawing.Size(905, 55);
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
            // moreinfoLiveEncodingProfilelink
            // 
            this.moreinfoLiveEncodingProfilelink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.moreinfoLiveEncodingProfilelink.AutoSize = true;
            this.moreinfoLiveEncodingProfilelink.Location = new System.Drawing.Point(664, 19);
            this.moreinfoLiveEncodingProfilelink.Name = "moreinfoLiveEncodingProfilelink";
            this.moreinfoLiveEncodingProfilelink.Size = new System.Drawing.Size(239, 15);
            this.moreinfoLiveEncodingProfilelink.TabIndex = 62;
            this.moreinfoLiveEncodingProfilelink.TabStop = true;
            this.moreinfoLiveEncodingProfilelink.Text = "More information on telemetry NOT VISIBLE";
            this.moreinfoLiveEncodingProfilelink.Visible = false;
            this.moreinfoLiveEncodingProfilelink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.moreinfoLiveEncodingProfilelink_LinkClicked);
            // 
            // labelTelemetryUI
            // 
            this.labelTelemetryUI.AutoSize = true;
            this.labelTelemetryUI.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTelemetryUI.ForeColor = System.Drawing.Color.DarkBlue;
            this.labelTelemetryUI.Location = new System.Drawing.Point(12, 15);
            this.labelTelemetryUI.Name = "labelTelemetryUI";
            this.labelTelemetryUI.Size = new System.Drawing.Size(170, 20);
            this.labelTelemetryUI.TabIndex = 76;
            this.labelTelemetryUI.Text = "Streaming endpoint : {0}";
            // 
            // dataGridViewTelemetry
            // 
            this.dataGridViewTelemetry.AllowUserToAddRows = false;
            this.dataGridViewTelemetry.AllowUserToDeleteRows = false;
            this.dataGridViewTelemetry.AllowUserToResizeRows = false;
            this.dataGridViewTelemetry.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewTelemetry.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTelemetry.Location = new System.Drawing.Point(16, 53);
            this.dataGridViewTelemetry.Name = "dataGridViewTelemetry";
            this.dataGridViewTelemetry.ReadOnly = true;
            this.dataGridViewTelemetry.RowHeadersVisible = false;
            this.dataGridViewTelemetry.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewTelemetry.Size = new System.Drawing.Size(885, 503);
            this.dataGridViewTelemetry.TabIndex = 77;
            this.dataGridViewTelemetry.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridViewTelemetry_CellFormatting);
            this.dataGridViewTelemetry.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridViewTelemetry_RowPostPaint);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(152, 562);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(130, 27);
            this.button1.TabIndex = 78;
            this.button1.Text = "Set time range...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.buttonRefresh.Image = ((System.Drawing.Image)(resources.GetObject("buttonRefresh.Image")));
            this.buttonRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonRefresh.Location = new System.Drawing.Point(408, 12);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(78, 28);
            this.buttonRefresh.TabIndex = 79;
            this.buttonRefresh.Text = "Refresh";
            this.buttonRefresh.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonRefresh.UseVisualStyleBackColor = true;
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // labelTimeRange
            // 
            this.labelTimeRange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelTimeRange.AutoSize = true;
            this.labelTimeRange.Location = new System.Drawing.Point(288, 568);
            this.labelTimeRange.Name = "labelTimeRange";
            this.labelTimeRange.Size = new System.Drawing.Size(76, 15);
            this.labelTimeRange.TabIndex = 80;
            this.labelTimeRange.Text = "From .... to ...";
            // 
            // checkBoxShowOnlyErrors
            // 
            this.checkBoxShowOnlyErrors.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxShowOnlyErrors.AutoSize = true;
            this.checkBoxShowOnlyErrors.Location = new System.Drawing.Point(16, 567);
            this.checkBoxShowOnlyErrors.Name = "checkBoxShowOnlyErrors";
            this.checkBoxShowOnlyErrors.Size = new System.Drawing.Size(114, 19);
            this.checkBoxShowOnlyErrors.TabIndex = 81;
            this.checkBoxShowOnlyErrors.Text = "Show errors only";
            this.checkBoxShowOnlyErrors.UseVisualStyleBackColor = true;
            this.checkBoxShowOnlyErrors.CheckedChanged += new System.EventHandler(this.checkBoxShowOnlyErrors_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Location = new System.Drawing.Point(892, 595);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(25, 42);
            this.panel2.TabIndex = 110;
            // 
            // DisplayTelemetry
            // 
            this.AcceptButton = this.buttonClose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonClose;
            this.ClientSize = new System.Drawing.Size(915, 647);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.checkBoxShowOnlyErrors);
            this.Controls.Add(this.labelTimeRange);
            this.Controls.Add(this.buttonRefresh);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridViewTelemetry);
            this.Controls.Add(this.labelTelemetryUI);
            this.Controls.Add(this.moreinfoLiveEncodingProfilelink);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "DisplayTelemetry";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Telemetry";
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
    }
}