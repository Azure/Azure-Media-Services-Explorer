namespace AMSExplorer
{
    partial class DynManifestFilter
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.textBoxFilterName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.textBoxStartTimestamp = new System.Windows.Forms.TextBox();
            this.textBoxEndTimestamp = new System.Windows.Forms.TextBox();
            this.textBoxPresentationWindowDuration = new System.Windows.Forms.TextBox();
            this.textBoxLiveBackoffDuration = new System.Windows.Forms.TextBox();
            this.textBoxTimescale = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label21 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dataGridViewTracks = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTracks)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonOk);
            this.panel1.Location = new System.Drawing.Point(0, 454);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(713, 48);
            this.panel1.TabIndex = 60;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(601, 12);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(98, 23);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(497, 12);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(98, 23);
            this.buttonOk.TabIndex = 4;
            this.buttonOk.Text = "Create Filter";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // textBoxFilterName
            // 
            this.textBoxFilterName.Location = new System.Drawing.Point(12, 35);
            this.textBoxFilterName.Name = "textBoxFilterName";
            this.textBoxFilterName.Size = new System.Drawing.Size(239, 20);
            this.textBoxFilterName.TabIndex = 62;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 13);
            this.label4.TabIndex = 61;
            this.label4.Text = "Filter name :";
            // 
            // textBoxStartTimestamp
            // 
            this.textBoxStartTimestamp.Location = new System.Drawing.Point(9, 49);
            this.textBoxStartTimestamp.Name = "textBoxStartTimestamp";
            this.textBoxStartTimestamp.Size = new System.Drawing.Size(239, 20);
            this.textBoxStartTimestamp.TabIndex = 64;
            this.toolTip1.SetToolTip(this.textBoxStartTimestamp, "Media that starts after this timestamp will be included in the playlist (manifest" +
        ").");
            // 
            // textBoxEndTimestamp
            // 
            this.textBoxEndTimestamp.Location = new System.Drawing.Point(9, 101);
            this.textBoxEndTimestamp.Name = "textBoxEndTimestamp";
            this.textBoxEndTimestamp.Size = new System.Drawing.Size(239, 20);
            this.textBoxEndTimestamp.TabIndex = 66;
            this.toolTip1.SetToolTip(this.textBoxEndTimestamp, "Media that ends before this timestamp will be included in the playlist (manifest)" +
        ".");
            // 
            // textBoxPresentationWindowDuration
            // 
            this.textBoxPresentationWindowDuration.Location = new System.Drawing.Point(9, 156);
            this.textBoxPresentationWindowDuration.Name = "textBoxPresentationWindowDuration";
            this.textBoxPresentationWindowDuration.Size = new System.Drawing.Size(239, 20);
            this.textBoxPresentationWindowDuration.TabIndex = 68;
            this.toolTip1.SetToolTip(this.textBoxPresentationWindowDuration, "Defines a sliding window at the live edge or end of the presentation. Media withi" +
        "n this sliding window will be included in the playlist (manifest).");
            // 
            // textBoxLiveBackoffDuration
            // 
            this.textBoxLiveBackoffDuration.Location = new System.Drawing.Point(9, 218);
            this.textBoxLiveBackoffDuration.Name = "textBoxLiveBackoffDuration";
            this.textBoxLiveBackoffDuration.Size = new System.Drawing.Size(239, 20);
            this.textBoxLiveBackoffDuration.TabIndex = 70;
            this.toolTip1.SetToolTip(this.textBoxLiveBackoffDuration, "Applies a live presentation backoff, or delay, to the media.");
            // 
            // textBoxTimescale
            // 
            this.textBoxTimescale.Location = new System.Drawing.Point(9, 281);
            this.textBoxTimescale.Name = "textBoxTimescale";
            this.textBoxTimescale.Size = new System.Drawing.Size(239, 20);
            this.textBoxTimescale.TabIndex = 76;
            this.toolTip1.SetToolTip(this.textBoxTimescale, "The timescale used by the timestamps and durations specified above. The default t" +
        "imescale is 10000000.  ");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 65;
            this.label2.Text = "End Timestamp :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 140);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 13);
            this.label3.TabIndex = 67;
            this.label3.Text = "DVR Window :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 202);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(180, 13);
            this.label5.TabIndex = 69;
            this.label5.Text = "Live Backoff Duration (live position) :";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 265);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(61, 13);
            this.label11.TabIndex = 75;
            this.label11.Text = "Timescale :";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 79);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(687, 350);
            this.tabControl1.TabIndex = 78;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label21);
            this.tabPage1.Controls.Add(this.label12);
            this.tabPage1.Controls.Add(this.label19);
            this.tabPage1.Controls.Add(this.textBoxStartTimestamp);
            this.tabPage1.Controls.Add(this.label18);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label17);
            this.tabPage1.Controls.Add(this.label16);
            this.tabPage1.Controls.Add(this.textBoxTimescale);
            this.tabPage1.Controls.Add(this.textBoxEndTimestamp);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.textBoxPresentationWindowDuration);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.textBoxLiveBackoffDuration);
            this.tabPage1.Controls.Add(this.textBox7);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(679, 324);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Presentation Time Range";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label21
            // 
            this.label21.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label21.Location = new System.Drawing.Point(254, 281);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(388, 32);
            this.label21.TabIndex = 77;
            this.label21.Text = "Live and VOD";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(9, 33);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(89, 13);
            this.label12.TabIndex = 63;
            this.label12.Text = "Start Timestamp :";
            this.label12.Click += new System.EventHandler(this.label12_Click);
            // 
            // label19
            // 
            this.label19.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label19.Location = new System.Drawing.Point(254, 156);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(346, 38);
            this.label19.TabIndex = 74;
            this.label19.Text = "Live, but also applies to VOD to enable smooth transitions when the presentation " +
    "ends. Min 120 seconds";
            // 
            // label18
            // 
            this.label18.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label18.Location = new System.Drawing.Point(254, 104);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(297, 36);
            this.label18.TabIndex = 73;
            this.label18.Text = "VOD (ignored for Live but applies to archive), Absolute time.\r\nValue rounded to t" +
    "he closest next GOP start.";
            // 
            // label17
            // 
            this.label17.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label17.Location = new System.Drawing.Point(254, 52);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(323, 36);
            this.label17.TabIndex = 72;
            this.label17.Text = "Live and VOD. Absolute time.\r\nValue rounded to the closest next GOP start.";
            // 
            // label16
            // 
            this.label16.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label16.Location = new System.Drawing.Point(254, 221);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(388, 32);
            this.label16.TabIndex = 71;
            this.label16.Text = "Live only, but silently ignored for VOD to enable smooth transitions when the pre" +
    "sentation ends. Max 60 seconds";
            // 
            // textBox7
            // 
            this.textBox7.Enabled = false;
            this.textBox7.Location = new System.Drawing.Point(9, 218);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(239, 20);
            this.textBox7.TabIndex = 70;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dataGridViewTracks);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(679, 324);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Tracks";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dataGridViewTracks
            // 
            this.dataGridViewTracks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTracks.Location = new System.Drawing.Point(7, 20);
            this.dataGridViewTracks.Name = "dataGridViewTracks";
            this.dataGridViewTracks.Size = new System.Drawing.Size(666, 282);
            this.dataGridViewTracks.TabIndex = 0;
            // 
            // DynManifestFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(713, 502);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.textBoxFilterName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.panel1);
            this.Name = "DynManifestFilter";
            this.Text = "Dynamic Manifest Filter";
            this.Load += new System.EventHandler(this.DynManifestFilter_Load);
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTracks)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Button buttonCancel;
        public System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.TextBox textBoxFilterName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TextBox textBoxStartTimestamp;
        private System.Windows.Forms.TextBox textBoxEndTimestamp;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxPresentationWindowDuration;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxLiveBackoffDuration;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxTimescale;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dataGridViewTracks;
    }
}