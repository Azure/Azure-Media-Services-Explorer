namespace AMSExplorer
{
    partial class ChooseStreamingEndpoint
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
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.radioButtonHttps = new System.Windows.Forms.RadioButton();
            this.radioButtonHttp = new System.Windows.Forms.RadioButton();
            this.listBoxSE = new System.Windows.Forms.ListBox();
            this.groupBoxForceLocator = new System.Windows.Forms.GroupBox();
            this.listViewFilters = new System.Windows.Forms.ListView();
            this.label8 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxNoAudioOnly = new System.Windows.Forms.CheckBox();
            this.labelaudiotrackname = new System.Windows.Forms.Label();
            this.textBoxHLSAudioTrackName = new System.Windows.Forms.TextBox();
            this.radioButtonSmoothLegacy = new System.Windows.Forms.RadioButton();
            this.radioButtonDASH = new System.Windows.Forms.RadioButton();
            this.radioButtonHDS = new System.Windows.Forms.RadioButton();
            this.radioButtonHLSv4 = new System.Windows.Forms.RadioButton();
            this.radioButtonHLSv3 = new System.Windows.Forms.RadioButton();
            this.radioButtonSmooth = new System.Windows.Forms.RadioButton();
            this.label = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxPreviewURL = new System.Windows.Forms.TextBox();
            this.groupBox4.SuspendLayout();
            this.groupBoxForceLocator.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(341, 14);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(112, 27);
            this.buttonOk.TabIndex = 0;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(460, 14);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(112, 27);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.radioButtonHttps);
            this.groupBox4.Controls.Add(this.radioButtonHttp);
            this.groupBox4.Controls.Add(this.listBoxSE);
            this.groupBox4.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(17, 66);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(552, 139);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Streaming Endpoint";
            // 
            // radioButtonHttps
            // 
            this.radioButtonHttps.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.radioButtonHttps.AutoSize = true;
            this.radioButtonHttps.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.radioButtonHttps.Location = new System.Drawing.Point(68, 114);
            this.radioButtonHttps.Name = "radioButtonHttps";
            this.radioButtonHttps.Size = new System.Drawing.Size(54, 19);
            this.radioButtonHttps.TabIndex = 63;
            this.radioButtonHttps.Text = "Https";
            this.radioButtonHttps.UseVisualStyleBackColor = true;
            // 
            // radioButtonHttp
            // 
            this.radioButtonHttp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.radioButtonHttp.AutoSize = true;
            this.radioButtonHttp.Checked = true;
            this.radioButtonHttp.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.radioButtonHttp.Location = new System.Drawing.Point(13, 114);
            this.radioButtonHttp.Name = "radioButtonHttp";
            this.radioButtonHttp.Size = new System.Drawing.Size(49, 19);
            this.radioButtonHttp.TabIndex = 62;
            this.radioButtonHttp.TabStop = true;
            this.radioButtonHttp.Text = "Http";
            this.radioButtonHttp.UseVisualStyleBackColor = true;
            this.radioButtonHttp.CheckedChanged += new System.EventHandler(this.radioButtonHttp_CheckedChanged);
            // 
            // listBoxSE
            // 
            this.listBoxSE.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxSE.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.listBoxSE.FormattingEnabled = true;
            this.listBoxSE.ItemHeight = 15;
            this.listBoxSE.Location = new System.Drawing.Point(13, 33);
            this.listBoxSE.Name = "listBoxSE";
            this.listBoxSE.Size = new System.Drawing.Size(526, 64);
            this.listBoxSE.TabIndex = 0;
            this.listBoxSE.SelectedIndexChanged += new System.EventHandler(this.listBoxSE_SelectedIndexChanged);
            // 
            // groupBoxForceLocator
            // 
            this.groupBoxForceLocator.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxForceLocator.Controls.Add(this.listViewFilters);
            this.groupBoxForceLocator.Controls.Add(this.label8);
            this.groupBoxForceLocator.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxForceLocator.Location = new System.Drawing.Point(17, 229);
            this.groupBoxForceLocator.Name = "groupBoxForceLocator";
            this.groupBoxForceLocator.Size = new System.Drawing.Size(207, 228);
            this.groupBoxForceLocator.TabIndex = 3;
            this.groupBoxForceLocator.TabStop = false;
            this.groupBoxForceLocator.Text = "Filters";
            // 
            // listViewFilters
            // 
            this.listViewFilters.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewFilters.CheckBoxes = true;
            this.listViewFilters.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewFilters.FullRowSelect = true;
            this.listViewFilters.GridLines = true;
            this.listViewFilters.Location = new System.Drawing.Point(13, 22);
            this.listViewFilters.Name = "listViewFilters";
            this.listViewFilters.Size = new System.Drawing.Size(179, 180);
            this.listViewFilters.TabIndex = 71;
            this.listViewFilters.UseCompatibleStateImageBehavior = false;
            this.listViewFilters.View = System.Windows.Forms.View.List;
            this.listViewFilters.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listViewFilters_ItemChecked);
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label8.Location = new System.Drawing.Point(6, 205);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(195, 16);
            this.label8.TabIndex = 70;
            this.label8.Text = "You can select up to 3 filters";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonOk);
            this.panel1.Location = new System.Drawing.Point(-2, 507);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(587, 55);
            this.panel1.TabIndex = 60;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.checkBoxNoAudioOnly);
            this.groupBox1.Controls.Add(this.labelaudiotrackname);
            this.groupBox1.Controls.Add(this.textBoxHLSAudioTrackName);
            this.groupBox1.Controls.Add(this.radioButtonSmoothLegacy);
            this.groupBox1.Controls.Add(this.radioButtonDASH);
            this.groupBox1.Controls.Add(this.radioButtonHDS);
            this.groupBox1.Controls.Add(this.radioButtonHLSv4);
            this.groupBox1.Controls.Add(this.radioButtonHLSv3);
            this.groupBox1.Controls.Add(this.radioButtonSmooth);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(246, 229);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(323, 228);
            this.groupBox1.TabIndex = 63;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Adaptive Streaming Protocol";
            // 
            // checkBoxNoAudioOnly
            // 
            this.checkBoxNoAudioOnly.AutoSize = true;
            this.checkBoxNoAudioOnly.Enabled = false;
            this.checkBoxNoAudioOnly.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxNoAudioOnly.Location = new System.Drawing.Point(49, 116);
            this.checkBoxNoAudioOnly.Name = "checkBoxNoAudioOnly";
            this.checkBoxNoAudioOnly.Size = new System.Drawing.Size(135, 19);
            this.checkBoxNoAudioOnly.TabIndex = 8;
            this.checkBoxNoAudioOnly.Text = "No audio only mode";
            this.checkBoxNoAudioOnly.UseVisualStyleBackColor = true;
            this.checkBoxNoAudioOnly.CheckedChanged += new System.EventHandler(this.checkBoxNoAudioOnly_CheckedChanged);
            // 
            // labelaudiotrackname
            // 
            this.labelaudiotrackname.AutoSize = true;
            this.labelaudiotrackname.Enabled = false;
            this.labelaudiotrackname.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelaudiotrackname.Location = new System.Drawing.Point(46, 94);
            this.labelaudiotrackname.Name = "labelaudiotrackname";
            this.labelaudiotrackname.Size = new System.Drawing.Size(107, 15);
            this.labelaudiotrackname.TabIndex = 7;
            this.labelaudiotrackname.Text = "Audio track name :";
            // 
            // textBoxHLSAudioTrackName
            // 
            this.textBoxHLSAudioTrackName.Enabled = false;
            this.textBoxHLSAudioTrackName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBoxHLSAudioTrackName.Location = new System.Drawing.Point(159, 91);
            this.textBoxHLSAudioTrackName.Name = "textBoxHLSAudioTrackName";
            this.textBoxHLSAudioTrackName.Size = new System.Drawing.Size(150, 23);
            this.textBoxHLSAudioTrackName.TabIndex = 6;
            this.textBoxHLSAudioTrackName.TextChanged += new System.EventHandler(this.textBoxHLSAudioTrackName_TextChanged);
            // 
            // radioButtonSmoothLegacy
            // 
            this.radioButtonSmoothLegacy.AutoSize = true;
            this.radioButtonSmoothLegacy.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.radioButtonSmoothLegacy.Location = new System.Drawing.Point(23, 47);
            this.radioButtonSmoothLegacy.Name = "radioButtonSmoothLegacy";
            this.radioButtonSmoothLegacy.Size = new System.Drawing.Size(161, 19);
            this.radioButtonSmoothLegacy.TabIndex = 5;
            this.radioButtonSmoothLegacy.Text = "Smooth Streaming legacy";
            this.radioButtonSmoothLegacy.UseVisualStyleBackColor = true;
            this.radioButtonSmoothLegacy.CheckedChanged += new System.EventHandler(this.radioButtonSmooth_CheckedChanged);
            // 
            // radioButtonDASH
            // 
            this.radioButtonDASH.AutoSize = true;
            this.radioButtonDASH.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.radioButtonDASH.Location = new System.Drawing.Point(23, 170);
            this.radioButtonDASH.Name = "radioButtonDASH";
            this.radioButtonDASH.Size = new System.Drawing.Size(93, 19);
            this.radioButtonDASH.TabIndex = 4;
            this.radioButtonDASH.Text = "MPEG-DASH";
            this.radioButtonDASH.UseVisualStyleBackColor = true;
            this.radioButtonDASH.CheckedChanged += new System.EventHandler(this.radioButtonSmooth_CheckedChanged);
            // 
            // radioButtonHDS
            // 
            this.radioButtonHDS.AutoSize = true;
            this.radioButtonHDS.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.radioButtonHDS.Location = new System.Drawing.Point(23, 195);
            this.radioButtonHDS.Name = "radioButtonHDS";
            this.radioButtonHDS.Size = new System.Drawing.Size(290, 19);
            this.radioButtonHDS.TabIndex = 3;
            this.radioButtonHDS.Text = "HDS (for Adobe PrimeTime/Access licensees only)";
            this.radioButtonHDS.UseVisualStyleBackColor = true;
            this.radioButtonHDS.CheckedChanged += new System.EventHandler(this.radioButtonSmooth_CheckedChanged);
            // 
            // radioButtonHLSv4
            // 
            this.radioButtonHLSv4.AutoSize = true;
            this.radioButtonHLSv4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.radioButtonHLSv4.Location = new System.Drawing.Point(23, 145);
            this.radioButtonHLSv4.Name = "radioButtonHLSv4";
            this.radioButtonHLSv4.Size = new System.Drawing.Size(61, 19);
            this.radioButtonHLSv4.TabIndex = 2;
            this.radioButtonHLSv4.Text = "HLS v4";
            this.radioButtonHLSv4.UseVisualStyleBackColor = true;
            this.radioButtonHLSv4.CheckedChanged += new System.EventHandler(this.radioButtonSmooth_CheckedChanged);
            // 
            // radioButtonHLSv3
            // 
            this.radioButtonHLSv3.AutoSize = true;
            this.radioButtonHLSv3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.radioButtonHLSv3.Location = new System.Drawing.Point(23, 72);
            this.radioButtonHLSv3.Name = "radioButtonHLSv3";
            this.radioButtonHLSv3.Size = new System.Drawing.Size(61, 19);
            this.radioButtonHLSv3.TabIndex = 1;
            this.radioButtonHLSv3.Text = "HLS v3";
            this.radioButtonHLSv3.UseVisualStyleBackColor = true;
            this.radioButtonHLSv3.CheckedChanged += new System.EventHandler(this.radioButtonHLSv3_CheckedChanged);
            // 
            // radioButtonSmooth
            // 
            this.radioButtonSmooth.AutoSize = true;
            this.radioButtonSmooth.Checked = true;
            this.radioButtonSmooth.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.radioButtonSmooth.Location = new System.Drawing.Point(23, 22);
            this.radioButtonSmooth.Name = "radioButtonSmooth";
            this.radioButtonSmooth.Size = new System.Drawing.Size(161, 19);
            this.radioButtonSmooth.TabIndex = 0;
            this.radioButtonSmooth.TabStop = true;
            this.radioButtonSmooth.Text = "Auto / Smooth Streaming";
            this.radioButtonSmooth.UseVisualStyleBackColor = true;
            this.radioButtonSmooth.CheckedChanged += new System.EventHandler(this.radioButtonSmooth_CheckedChanged);
            // 
            // label
            // 
            this.label.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label.Location = new System.Drawing.Point(18, 40);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(551, 20);
            this.label.TabIndex = 64;
            this.label.Text = "Asset : \'{0}\'";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DarkBlue;
            this.label5.Location = new System.Drawing.Point(13, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(184, 20);
            this.label5.TabIndex = 65;
            this.label5.Text = "Streaming URL Generation";
            // 
            // textBoxPreviewURL
            // 
            this.textBoxPreviewURL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPreviewURL.Enabled = false;
            this.textBoxPreviewURL.Font = new System.Drawing.Font("Segoe UI Semilight", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPreviewURL.Location = new System.Drawing.Point(17, 463);
            this.textBoxPreviewURL.Multiline = true;
            this.textBoxPreviewURL.Name = "textBoxPreviewURL";
            this.textBoxPreviewURL.ReadOnly = true;
            this.textBoxPreviewURL.Size = new System.Drawing.Size(553, 35);
            this.textBoxPreviewURL.TabIndex = 9;
            // 
            // ChooseStreamingEndpoint
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(584, 561);
            this.Controls.Add(this.textBoxPreviewURL);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBoxForceLocator);
            this.Controls.Add(this.groupBox4);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "ChooseStreamingEndpoint";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Options to generate the streaming URL";
            this.Load += new System.EventHandler(this.ChooseStreamingEndpoint_Load);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBoxForceLocator.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button buttonOk;
        public System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBoxForceLocator;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListBox listBoxSE;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelaudiotrackname;
        private System.Windows.Forms.TextBox textBoxHLSAudioTrackName;
        private System.Windows.Forms.RadioButton radioButtonSmoothLegacy;
        private System.Windows.Forms.RadioButton radioButtonDASH;
        private System.Windows.Forms.RadioButton radioButtonHDS;
        private System.Windows.Forms.RadioButton radioButtonHLSv4;
        private System.Windows.Forms.RadioButton radioButtonHLSv3;
        private System.Windows.Forms.RadioButton radioButtonSmooth;
        private System.Windows.Forms.RadioButton radioButtonHttps;
        private System.Windows.Forms.RadioButton radioButtonHttp;
        public System.Windows.Forms.Label label;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox checkBoxNoAudioOnly;
        private System.Windows.Forms.TextBox textBoxPreviewURL;
        private System.Windows.Forms.ListView listViewFilters;
    }
}