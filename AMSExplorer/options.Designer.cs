namespace AMSExplorer
{
    partial class Options
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Options));
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.checkBoxUseStorageEncryption = new System.Windows.Forms.CheckBox();
            this.checkBoxUseProtectedConfig = new System.Windows.Forms.CheckBox();
            this.checkBoxOneUpDownload = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxNbItems = new System.Windows.Forms.ComboBox();
            this.buttonReset = new System.Windows.Forms.Button();
            this.checkBoxDisplayAssetID = new System.Windows.Forms.CheckBox();
            this.checkBoxDisplayJobID = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxAutoRefresh = new System.Windows.Forms.CheckBox();
            this.comboBoxAutoRefreshTime = new System.Windows.Forms.ComboBox();
            this.checkBoxDisplayOriginID = new System.Windows.Forms.CheckBox();
            this.checkBoxDisplayProgramID = new System.Windows.Forms.CheckBox();
            this.checkBoxDisplayChannelID = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBoxCustomPlayer = new System.Windows.Forms.TextBox();
            this.checkBoxEnableCustomPlayer = new System.Windows.Forms.CheckBox();
            this.numericUpDownPriority = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPriority)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(276, 398);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(113, 32);
            this.buttonOk.TabIndex = 7;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(395, 398);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(113, 32);
            this.buttonCancel.TabIndex = 6;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // checkBoxUseStorageEncryption
            // 
            this.checkBoxUseStorageEncryption.AutoSize = true;
            this.checkBoxUseStorageEncryption.Location = new System.Drawing.Point(42, 29);
            this.checkBoxUseStorageEncryption.Name = "checkBoxUseStorageEncryption";
            this.checkBoxUseStorageEncryption.Size = new System.Drawing.Size(355, 17);
            this.checkBoxUseStorageEncryption.TabIndex = 8;
            this.checkBoxUseStorageEncryption.Text = "Use storage encryption for new assets (exept storage to storage copy)";
            this.checkBoxUseStorageEncryption.UseVisualStyleBackColor = true;
            // 
            // checkBoxUseProtectedConfig
            // 
            this.checkBoxUseProtectedConfig.AutoSize = true;
            this.checkBoxUseProtectedConfig.Location = new System.Drawing.Point(42, 52);
            this.checkBoxUseProtectedConfig.Name = "checkBoxUseProtectedConfig";
            this.checkBoxUseProtectedConfig.Size = new System.Drawing.Size(223, 17);
            this.checkBoxUseProtectedConfig.TabIndex = 9;
            this.checkBoxUseProtectedConfig.Text = "Use protected configuration for new tasks";
            this.checkBoxUseProtectedConfig.UseVisualStyleBackColor = true;
            // 
            // checkBoxOneUpDownload
            // 
            this.checkBoxOneUpDownload.AutoSize = true;
            this.checkBoxOneUpDownload.Location = new System.Drawing.Point(42, 75);
            this.checkBoxOneUpDownload.Name = "checkBoxOneUpDownload";
            this.checkBoxOneUpDownload.Size = new System.Drawing.Size(243, 17);
            this.checkBoxOneUpDownload.TabIndex = 10;
            this.checkBoxOneUpDownload.Text = "One upload/download at a time (use a queue)";
            this.checkBoxOneUpDownload.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(119, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "items displayed per page";
            // 
            // comboBoxNbItems
            // 
            this.comboBoxNbItems.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxNbItems.FormattingEnabled = true;
            this.comboBoxNbItems.Location = new System.Drawing.Point(42, 24);
            this.comboBoxNbItems.Name = "comboBoxNbItems";
            this.comboBoxNbItems.Size = new System.Drawing.Size(71, 21);
            this.comboBoxNbItems.TabIndex = 13;
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(12, 398);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(113, 32);
            this.buttonReset.TabIndex = 14;
            this.buttonReset.Text = "Reset to defaults";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // checkBoxDisplayAssetID
            // 
            this.checkBoxDisplayAssetID.AutoSize = true;
            this.checkBoxDisplayAssetID.Location = new System.Drawing.Point(42, 51);
            this.checkBoxDisplayAssetID.Name = "checkBoxDisplayAssetID";
            this.checkBoxDisplayAssetID.Size = new System.Drawing.Size(101, 17);
            this.checkBoxDisplayAssetID.TabIndex = 15;
            this.checkBoxDisplayAssetID.Text = "Display Asset Id";
            this.checkBoxDisplayAssetID.UseVisualStyleBackColor = true;
            // 
            // checkBoxDisplayJobID
            // 
            this.checkBoxDisplayJobID.AutoSize = true;
            this.checkBoxDisplayJobID.Location = new System.Drawing.Point(42, 74);
            this.checkBoxDisplayJobID.Name = "checkBoxDisplayJobID";
            this.checkBoxDisplayJobID.Size = new System.Drawing.Size(92, 17);
            this.checkBoxDisplayJobID.TabIndex = 16;
            this.checkBoxDisplayJobID.Text = "Display Job Id";
            this.checkBoxDisplayJobID.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxAutoRefresh);
            this.groupBox1.Controls.Add(this.comboBoxAutoRefreshTime);
            this.groupBox1.Controls.Add(this.checkBoxDisplayOriginID);
            this.groupBox1.Controls.Add(this.checkBoxDisplayProgramID);
            this.groupBox1.Controls.Add(this.checkBoxDisplayChannelID);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.checkBoxDisplayJobID);
            this.groupBox1.Controls.Add(this.comboBoxNbItems);
            this.groupBox1.Controls.Add(this.checkBoxDisplayAssetID);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(496, 130);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Grids display";
            // 
            // checkBoxAutoRefresh
            // 
            this.checkBoxAutoRefresh.AutoSize = true;
            this.checkBoxAutoRefresh.Location = new System.Drawing.Point(42, 97);
            this.checkBoxAutoRefresh.Name = "checkBoxAutoRefresh";
            this.checkBoxAutoRefresh.Size = new System.Drawing.Size(132, 17);
            this.checkBoxAutoRefresh.TabIndex = 21;
            this.checkBoxAutoRefresh.Text = "Auto refresh every (s) :";
            this.toolTip1.SetToolTip(this.checkBoxAutoRefresh, "Useful if another program create assets, jobs, etc");
            this.checkBoxAutoRefresh.UseVisualStyleBackColor = true;
            // 
            // comboBoxAutoRefreshTime
            // 
            this.comboBoxAutoRefreshTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAutoRefreshTime.FormattingEnabled = true;
            this.comboBoxAutoRefreshTime.Items.AddRange(new object[] {
            "30",
            "60",
            "300",
            "600"});
            this.comboBoxAutoRefreshTime.Location = new System.Drawing.Point(180, 95);
            this.comboBoxAutoRefreshTime.Name = "comboBoxAutoRefreshTime";
            this.comboBoxAutoRefreshTime.Size = new System.Drawing.Size(62, 21);
            this.comboBoxAutoRefreshTime.TabIndex = 20;
            // 
            // checkBoxDisplayOriginID
            // 
            this.checkBoxDisplayOriginID.AutoSize = true;
            this.checkBoxDisplayOriginID.Location = new System.Drawing.Point(341, 51);
            this.checkBoxDisplayOriginID.Name = "checkBoxDisplayOriginID";
            this.checkBoxDisplayOriginID.Size = new System.Drawing.Size(102, 17);
            this.checkBoxDisplayOriginID.TabIndex = 19;
            this.checkBoxDisplayOriginID.Text = "Display Origin Id";
            this.checkBoxDisplayOriginID.UseVisualStyleBackColor = true;
            // 
            // checkBoxDisplayProgramID
            // 
            this.checkBoxDisplayProgramID.AutoSize = true;
            this.checkBoxDisplayProgramID.Location = new System.Drawing.Point(187, 74);
            this.checkBoxDisplayProgramID.Name = "checkBoxDisplayProgramID";
            this.checkBoxDisplayProgramID.Size = new System.Drawing.Size(114, 17);
            this.checkBoxDisplayProgramID.TabIndex = 18;
            this.checkBoxDisplayProgramID.Text = "Display Program Id";
            this.checkBoxDisplayProgramID.UseVisualStyleBackColor = true;
            // 
            // checkBoxDisplayChannelID
            // 
            this.checkBoxDisplayChannelID.AutoSize = true;
            this.checkBoxDisplayChannelID.Location = new System.Drawing.Point(187, 51);
            this.checkBoxDisplayChannelID.Name = "checkBoxDisplayChannelID";
            this.checkBoxDisplayChannelID.Size = new System.Drawing.Size(114, 17);
            this.checkBoxDisplayChannelID.TabIndex = 17;
            this.checkBoxDisplayChannelID.Text = "Display Channel Id";
            this.checkBoxDisplayChannelID.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.pictureBox2);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.numericUpDownPriority);
            this.groupBox2.Controls.Add(this.checkBoxUseStorageEncryption);
            this.groupBox2.Controls.Add(this.pictureBox1);
            this.groupBox2.Controls.Add(this.checkBoxUseProtectedConfig);
            this.groupBox2.Controls.Add(this.checkBoxOneUpDownload);
            this.groupBox2.Location = new System.Drawing.Point(12, 148);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(496, 135);
            this.groupBox2.TabIndex = 50;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Other settings";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::AMSExplorer.Bitmaps.storage_encryption;
            this.pictureBox1.Location = new System.Drawing.Point(16, 29);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 49;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBoxCustomPlayer);
            this.groupBox3.Controls.Add(this.checkBoxEnableCustomPlayer);
            this.groupBox3.Location = new System.Drawing.Point(12, 289);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(496, 89);
            this.groupBox3.TabIndex = 51;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Custom player";
            // 
            // textBoxCustomPlayer
            // 
            this.textBoxCustomPlayer.Location = new System.Drawing.Point(16, 47);
            this.textBoxCustomPlayer.Name = "textBoxCustomPlayer";
            this.textBoxCustomPlayer.Size = new System.Drawing.Size(474, 20);
            this.textBoxCustomPlayer.TabIndex = 9;
            // 
            // checkBoxEnableCustomPlayer
            // 
            this.checkBoxEnableCustomPlayer.AutoSize = true;
            this.checkBoxEnableCustomPlayer.Location = new System.Drawing.Point(16, 24);
            this.checkBoxEnableCustomPlayer.Name = "checkBoxEnableCustomPlayer";
            this.checkBoxEnableCustomPlayer.Size = new System.Drawing.Size(59, 17);
            this.checkBoxEnableCustomPlayer.TabIndex = 8;
            this.checkBoxEnableCustomPlayer.Text = "Enable";
            this.checkBoxEnableCustomPlayer.UseVisualStyleBackColor = true;
            this.checkBoxEnableCustomPlayer.CheckedChanged += new System.EventHandler(this.checkBoxEnableCustomPlayer_CheckedChanged);
            // 
            // numericUpDownPriority
            // 
            this.numericUpDownPriority.Location = new System.Drawing.Point(140, 98);
            this.numericUpDownPriority.Name = "numericUpDownPriority";
            this.numericUpDownPriority.Size = new System.Drawing.Size(93, 20);
            this.numericUpDownPriority.TabIndex = 50;
            this.numericUpDownPriority.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Default job priority :";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::AMSExplorer.Bitmaps.change_priority;
            this.pictureBox2.Location = new System.Drawing.Point(18, 97);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(16, 16);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 51;
            this.pictureBox2.TabStop = false;
            // 
            // Options
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(520, 441);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.buttonCancel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Options";
            this.Text = "Options";
            this.Load += new System.EventHandler(this.options_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPriority)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Button buttonOk;
        public System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.CheckBox checkBoxUseStorageEncryption;
        private System.Windows.Forms.CheckBox checkBoxUseProtectedConfig;
        private System.Windows.Forms.CheckBox checkBoxOneUpDownload;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxNbItems;
        public System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.CheckBox checkBoxDisplayAssetID;
        private System.Windows.Forms.CheckBox checkBoxDisplayJobID;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkBoxDisplayOriginID;
        private System.Windows.Forms.CheckBox checkBoxDisplayProgramID;
        private System.Windows.Forms.CheckBox checkBoxDisplayChannelID;
        private System.Windows.Forms.CheckBox checkBoxAutoRefresh;
        private System.Windows.Forms.ComboBox comboBoxAutoRefreshTime;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox textBoxCustomPlayer;
        private System.Windows.Forms.CheckBox checkBoxEnableCustomPlayer;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDownPriority;
    }
}