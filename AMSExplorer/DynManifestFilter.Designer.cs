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
            this.textBoxLiveBackoffDuration = new System.Windows.Forms.TextBox();
            this.textBoxPresentationWindowDuration = new System.Windows.Forms.TextBox();
            this.textBoxEndTimestamp = new System.Windows.Forms.TextBox();
            this.textBoxTimescale = new System.Windows.Forms.TextBox();
            this.textBoxStartTimestamp = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.textBoxLabelLiveBackoff = new System.Windows.Forms.TextBox();
            this.textBoxLabelDVR = new System.Windows.Forms.TextBox();
            this.textBoxLabelEnd = new System.Windows.Forms.TextBox();
            this.textBoxLabelStart = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonDeleteCondition = new System.Windows.Forms.Button();
            this.buttonDeleteTrack = new System.Windows.Forms.Button();
            this.buttonAddCondition = new System.Windows.Forms.Button();
            this.buttonAddTrack = new System.Windows.Forms.Button();
            this.listBoxTracks = new System.Windows.Forms.ListBox();
            this.dataGridViewTracks = new System.Windows.Forms.DataGridView();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.buttonInsertSample = new System.Windows.Forms.Button();
            this.moreinfoprofilelink = new System.Windows.Forms.LinkLabel();
            this.labelFilterTitle = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTracks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
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
            this.textBoxFilterName.Location = new System.Drawing.Point(12, 55);
            this.textBoxFilterName.Name = "textBoxFilterName";
            this.textBoxFilterName.Size = new System.Drawing.Size(239, 20);
            this.textBoxFilterName.TabIndex = 62;
            this.textBoxFilterName.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxFilterName_Validating);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 13);
            this.label4.TabIndex = 61;
            this.label4.Text = "Filter name :";
            // 
            // textBoxLiveBackoffDuration
            // 
            this.textBoxLiveBackoffDuration.Location = new System.Drawing.Point(9, 218);
            this.textBoxLiveBackoffDuration.Name = "textBoxLiveBackoffDuration";
            this.textBoxLiveBackoffDuration.Size = new System.Drawing.Size(239, 20);
            this.textBoxLiveBackoffDuration.TabIndex = 70;
            this.toolTip1.SetToolTip(this.textBoxLiveBackoffDuration, "Applies a live presentation backoff, or delay, to the media.");
            this.textBoxLiveBackoffDuration.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // textBoxPresentationWindowDuration
            // 
            this.textBoxPresentationWindowDuration.Location = new System.Drawing.Point(9, 156);
            this.textBoxPresentationWindowDuration.Name = "textBoxPresentationWindowDuration";
            this.textBoxPresentationWindowDuration.Size = new System.Drawing.Size(239, 20);
            this.textBoxPresentationWindowDuration.TabIndex = 68;
            this.toolTip1.SetToolTip(this.textBoxPresentationWindowDuration, "Defines a sliding window at the live edge or end of the presentation. Media withi" +
        "n this sliding window will be included in the playlist (manifest).");
            this.textBoxPresentationWindowDuration.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // textBoxEndTimestamp
            // 
            this.textBoxEndTimestamp.Location = new System.Drawing.Point(9, 101);
            this.textBoxEndTimestamp.Name = "textBoxEndTimestamp";
            this.textBoxEndTimestamp.Size = new System.Drawing.Size(239, 20);
            this.textBoxEndTimestamp.TabIndex = 66;
            this.toolTip1.SetToolTip(this.textBoxEndTimestamp, "Media that ends before this timestamp will be included in the playlist (manifest)" +
        ".");
            this.textBoxEndTimestamp.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // textBoxTimescale
            // 
            this.textBoxTimescale.Location = new System.Drawing.Point(9, 281);
            this.textBoxTimescale.Name = "textBoxTimescale";
            this.textBoxTimescale.Size = new System.Drawing.Size(239, 20);
            this.textBoxTimescale.TabIndex = 76;
            this.toolTip1.SetToolTip(this.textBoxTimescale, "The timescale used by the timestamps and durations specified above. The default t" +
        "imescale is 10000000.  ");
            this.textBoxTimescale.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // textBoxStartTimestamp
            // 
            this.textBoxStartTimestamp.Location = new System.Drawing.Point(9, 49);
            this.textBoxStartTimestamp.Name = "textBoxStartTimestamp";
            this.textBoxStartTimestamp.Size = new System.Drawing.Size(239, 20);
            this.textBoxStartTimestamp.TabIndex = 64;
            this.toolTip1.SetToolTip(this.textBoxStartTimestamp, "Media that starts after this timestamp will be included in the playlist (manifest" +
        ").");
            this.textBoxStartTimestamp.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 91);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(687, 350);
            this.tabControl1.TabIndex = 78;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.textBoxLabelLiveBackoff);
            this.tabPage1.Controls.Add(this.textBoxLabelDVR);
            this.tabPage1.Controls.Add(this.textBoxLabelEnd);
            this.tabPage1.Controls.Add(this.textBoxLabelStart);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label21);
            this.tabPage1.Controls.Add(this.label19);
            this.tabPage1.Controls.Add(this.textBoxStartTimestamp);
            this.tabPage1.Controls.Add(this.label18);
            this.tabPage1.Controls.Add(this.label17);
            this.tabPage1.Controls.Add(this.label16);
            this.tabPage1.Controls.Add(this.textBoxTimescale);
            this.tabPage1.Controls.Add(this.textBoxEndTimestamp);
            this.tabPage1.Controls.Add(this.textBoxPresentationWindowDuration);
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
            // textBoxLabelLiveBackoff
            // 
            this.textBoxLabelLiveBackoff.Location = new System.Drawing.Point(254, 218);
            this.textBoxLabelLiveBackoff.Name = "textBoxLabelLiveBackoff";
            this.textBoxLabelLiveBackoff.ReadOnly = true;
            this.textBoxLabelLiveBackoff.Size = new System.Drawing.Size(85, 20);
            this.textBoxLabelLiveBackoff.TabIndex = 96;
            // 
            // textBoxLabelDVR
            // 
            this.textBoxLabelDVR.Location = new System.Drawing.Point(254, 156);
            this.textBoxLabelDVR.Name = "textBoxLabelDVR";
            this.textBoxLabelDVR.ReadOnly = true;
            this.textBoxLabelDVR.Size = new System.Drawing.Size(85, 20);
            this.textBoxLabelDVR.TabIndex = 95;
            // 
            // textBoxLabelEnd
            // 
            this.textBoxLabelEnd.Location = new System.Drawing.Point(254, 101);
            this.textBoxLabelEnd.Name = "textBoxLabelEnd";
            this.textBoxLabelEnd.ReadOnly = true;
            this.textBoxLabelEnd.Size = new System.Drawing.Size(85, 20);
            this.textBoxLabelEnd.TabIndex = 94;
            // 
            // textBoxLabelStart
            // 
            this.textBoxLabelStart.Location = new System.Drawing.Point(254, 49);
            this.textBoxLabelStart.Name = "textBoxLabelStart";
            this.textBoxLabelStart.ReadOnly = true;
            this.textBoxLabelStart.Size = new System.Drawing.Size(85, 20);
            this.textBoxLabelStart.TabIndex = 93;
            // 
            // label9
            // 
            this.label9.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label9.Location = new System.Drawing.Point(6, 6);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(523, 16);
            this.label9.TabIndex = 87;
            this.label9.Text = "Keep the field empty to not specify the settings. Filter activation can take up t" +
    "o 2 minutes.";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 265);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 13);
            this.label8.TabIndex = 86;
            this.label8.Text = "Timescale :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 202);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(180, 13);
            this.label7.TabIndex = 85;
            this.label7.Text = "Live Backoff Duration (live position) :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 140);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 13);
            this.label5.TabIndex = 84;
            this.label5.Text = "DVR Window :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 13);
            this.label3.TabIndex = 83;
            this.label3.Text = "End Timestamp :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 13);
            this.label2.TabIndex = 80;
            this.label2.Text = "Start Timestamp :";
            // 
            // label21
            // 
            this.label21.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label21.Location = new System.Drawing.Point(345, 281);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(328, 32);
            this.label21.TabIndex = 77;
            this.label21.Text = "Live and VOD";
            // 
            // label19
            // 
            this.label19.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label19.Location = new System.Drawing.Point(345, 156);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(328, 38);
            this.label19.TabIndex = 74;
            this.label19.Text = "Live, but also applies to VOD to enable smooth transitions when the presentation " +
    "ends. Min 120 seconds";
            // 
            // label18
            // 
            this.label18.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label18.Location = new System.Drawing.Point(345, 104);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(314, 36);
            this.label18.TabIndex = 73;
            this.label18.Text = "VOD (ignored for Live but applies to archive), Absolute time.\r\nValue rounded to t" +
    "he closest next GOP start.";
            // 
            // label17
            // 
            this.label17.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label17.Location = new System.Drawing.Point(345, 49);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(262, 36);
            this.label17.TabIndex = 72;
            this.label17.Text = "Live and VOD. Absolute time.\r\nValue rounded to the closest next GOP start.";
            // 
            // label16
            // 
            this.label16.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label16.Location = new System.Drawing.Point(345, 221);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(328, 32);
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
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.buttonDeleteCondition);
            this.tabPage2.Controls.Add(this.buttonDeleteTrack);
            this.tabPage2.Controls.Add(this.buttonAddCondition);
            this.tabPage2.Controls.Add(this.buttonAddTrack);
            this.tabPage2.Controls.Add(this.listBoxTracks);
            this.tabPage2.Controls.Add(this.dataGridViewTracks);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(679, 324);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Tracks";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(142, 7);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 13);
            this.label6.TabIndex = 80;
            this.label6.Text = "Conditions :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 79;
            this.label1.Text = "Tracks rules :";
            // 
            // buttonDeleteCondition
            // 
            this.buttonDeleteCondition.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDeleteCondition.Location = new System.Drawing.Point(615, 293);
            this.buttonDeleteCondition.Name = "buttonDeleteCondition";
            this.buttonDeleteCondition.Size = new System.Drawing.Size(58, 23);
            this.buttonDeleteCondition.TabIndex = 46;
            this.buttonDeleteCondition.Text = "Delete";
            this.buttonDeleteCondition.UseVisualStyleBackColor = true;
            this.buttonDeleteCondition.Click += new System.EventHandler(this.buttonDeleteCondition_Click);
            // 
            // buttonDeleteTrack
            // 
            this.buttonDeleteTrack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDeleteTrack.Location = new System.Drawing.Point(81, 293);
            this.buttonDeleteTrack.Name = "buttonDeleteTrack";
            this.buttonDeleteTrack.Size = new System.Drawing.Size(58, 23);
            this.buttonDeleteTrack.TabIndex = 44;
            this.buttonDeleteTrack.Text = "Delete";
            this.buttonDeleteTrack.UseVisualStyleBackColor = true;
            this.buttonDeleteTrack.Click += new System.EventHandler(this.buttonDeleteTrack_Click);
            // 
            // buttonAddCondition
            // 
            this.buttonAddCondition.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddCondition.Location = new System.Drawing.Point(551, 293);
            this.buttonAddCondition.Name = "buttonAddCondition";
            this.buttonAddCondition.Size = new System.Drawing.Size(58, 23);
            this.buttonAddCondition.TabIndex = 45;
            this.buttonAddCondition.Text = "Add";
            this.buttonAddCondition.UseVisualStyleBackColor = true;
            this.buttonAddCondition.Click += new System.EventHandler(this.buttonAddCondition_Click);
            // 
            // buttonAddTrack
            // 
            this.buttonAddTrack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAddTrack.Location = new System.Drawing.Point(19, 293);
            this.buttonAddTrack.Name = "buttonAddTrack";
            this.buttonAddTrack.Size = new System.Drawing.Size(58, 23);
            this.buttonAddTrack.TabIndex = 43;
            this.buttonAddTrack.Text = "Add";
            this.buttonAddTrack.UseVisualStyleBackColor = true;
            this.buttonAddTrack.Click += new System.EventHandler(this.buttonAddTrack_Click);
            // 
            // listBoxTracks
            // 
            this.listBoxTracks.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxTracks.FormattingEnabled = true;
            this.listBoxTracks.Location = new System.Drawing.Point(19, 23);
            this.listBoxTracks.Name = "listBoxTracks";
            this.listBoxTracks.Size = new System.Drawing.Size(120, 264);
            this.listBoxTracks.TabIndex = 1;
            this.listBoxTracks.SelectedIndexChanged += new System.EventHandler(this.listBoxTracks_SelectedIndexChanged);
            // 
            // dataGridViewTracks
            // 
            this.dataGridViewTracks.AllowUserToAddRows = false;
            this.dataGridViewTracks.AllowUserToDeleteRows = false;
            this.dataGridViewTracks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewTracks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTracks.Location = new System.Drawing.Point(145, 25);
            this.dataGridViewTracks.Name = "dataGridViewTracks";
            this.dataGridViewTracks.RowHeadersVisible = false;
            this.dataGridViewTracks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewTracks.Size = new System.Drawing.Size(524, 262);
            this.dataGridViewTracks.TabIndex = 0;
            this.dataGridViewTracks.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewTracks_CellValueChanged);
            this.dataGridViewTracks.CurrentCellDirtyStateChanged += new System.EventHandler(this.dataGridViewTracks_CurrentCellDirtyStateChanged);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // buttonInsertSample
            // 
            this.buttonInsertSample.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonInsertSample.Location = new System.Drawing.Point(578, 35);
            this.buttonInsertSample.Name = "buttonInsertSample";
            this.buttonInsertSample.Size = new System.Drawing.Size(121, 23);
            this.buttonInsertSample.TabIndex = 79;
            this.buttonInsertSample.Text = "Insert sample";
            this.buttonInsertSample.UseVisualStyleBackColor = true;
            this.buttonInsertSample.Click += new System.EventHandler(this.buttonInsertSample_Click);
            // 
            // moreinfoprofilelink
            // 
            this.moreinfoprofilelink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.moreinfoprofilelink.AutoSize = true;
            this.moreinfoprofilelink.Location = new System.Drawing.Point(614, 74);
            this.moreinfoprofilelink.Name = "moreinfoprofilelink";
            this.moreinfoprofilelink.Size = new System.Drawing.Size(85, 13);
            this.moreinfoprofilelink.TabIndex = 80;
            this.moreinfoprofilelink.TabStop = true;
            this.moreinfoprofilelink.Text = "More information";
            this.moreinfoprofilelink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.moreinfoprofilelink_LinkClicked);
            // 
            // labelFilterTitle
            // 
            this.labelFilterTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelFilterTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFilterTitle.Location = new System.Drawing.Point(8, 9);
            this.labelFilterTitle.Name = "labelFilterTitle";
            this.labelFilterTitle.Size = new System.Drawing.Size(691, 18);
            this.labelFilterTitle.TabIndex = 81;
            this.labelFilterTitle.Text = "Global Filter";
            // 
            // DynManifestFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(713, 502);
            this.Controls.Add(this.labelFilterTitle);
            this.Controls.Add(this.moreinfoprofilelink);
            this.Controls.Add(this.buttonInsertSample);
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
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTracks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
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
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dataGridViewTracks;
        private System.Windows.Forms.ListBox listBoxTracks;
        private System.Windows.Forms.Button buttonDeleteTrack;
        private System.Windows.Forms.Button buttonAddTrack;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonDeleteCondition;
        private System.Windows.Forms.Button buttonAddCondition;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Button buttonInsertSample;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox textBoxStartTimestamp;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox textBoxTimescale;
        private System.Windows.Forms.TextBox textBoxEndTimestamp;
        private System.Windows.Forms.TextBox textBoxPresentationWindowDuration;
        private System.Windows.Forms.TextBox textBoxLiveBackoffDuration;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxLabelLiveBackoff;
        private System.Windows.Forms.TextBox textBoxLabelDVR;
        private System.Windows.Forms.TextBox textBoxLabelEnd;
        private System.Windows.Forms.TextBox textBoxLabelStart;
        private System.Windows.Forms.LinkLabel moreinfoprofilelink;
        private System.Windows.Forms.Label labelFilterTitle;
    }
}