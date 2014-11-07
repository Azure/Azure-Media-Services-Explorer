﻿namespace AMSExplorer
{
    partial class PlayReadyExternalServer
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
            this.moreinfotestserver = new System.Windows.Forms.LinkLabel();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label = new System.Windows.Forms.Label();
            this.buttonPlayReadyTestSettings = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.textBoxLAurl = new System.Windows.Forms.TextBox();
            this.buttongenerateContentKey = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxkeyseed = new System.Windows.Forms.TextBox();
            this.buttonGenKeyID = new System.Windows.Forms.Button();
            this.textBoxkeyid = new System.Windows.Forms.TextBox();
            this.textBoxcontentkey = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.buttonOk = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // moreinfotestserver
            // 
            this.moreinfotestserver.AutoSize = true;
            this.moreinfotestserver.Location = new System.Drawing.Point(190, 65);
            this.moreinfotestserver.Name = "moreinfotestserver";
            this.moreinfotestserver.Size = new System.Drawing.Size(152, 13);
            this.moreinfotestserver.TabIndex = 19;
            this.moreinfotestserver.TabStop = true;
            this.moreinfotestserver.Text = "PlayReady test server web site";
            this.moreinfotestserver.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.moreinfotestserver_LinkClicked);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(267, 396);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(208, 32);
            this.buttonCancel.TabIndex = 16;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(188, 47);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(264, 13);
            this.label.TabIndex = 28;
            this.label.Text = "You can specifiy your own data or use the test settings";
            // 
            // buttonPlayReadyTestSettings
            // 
            this.buttonPlayReadyTestSettings.Location = new System.Drawing.Point(23, 48);
            this.buttonPlayReadyTestSettings.Name = "buttonPlayReadyTestSettings";
            this.buttonPlayReadyTestSettings.Size = new System.Drawing.Size(159, 32);
            this.buttonPlayReadyTestSettings.TabIndex = 29;
            this.buttonPlayReadyTestSettings.Text = "Use PlayReady Test Settings";
            this.buttonPlayReadyTestSettings.UseVisualStyleBackColor = true;
            this.buttonPlayReadyTestSettings.Click += new System.EventHandler(this.buttonPlayReadyTestSettings_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(23, 95);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(468, 262);
            this.tabControl1.TabIndex = 33;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.textBoxLAurl);
            this.tabPage1.Controls.Add(this.buttongenerateContentKey);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.textBoxkeyseed);
            this.tabPage1.Controls.Add(this.buttonGenKeyID);
            this.tabPage1.Controls.Add(this.textBoxkeyid);
            this.tabPage1.Controls.Add(this.textBoxcontentkey);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(460, 236);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Basic settings";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // textBoxLAurl
            // 
            this.textBoxLAurl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxLAurl.Location = new System.Drawing.Point(24, 82);
            this.textBoxLAurl.Name = "textBoxLAurl";
            this.textBoxLAurl.Size = new System.Drawing.Size(414, 20);
            this.textBoxLAurl.TabIndex = 37;
            // 
            // buttongenerateContentKey
            // 
            this.buttongenerateContentKey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttongenerateContentKey.Location = new System.Drawing.Point(355, 187);
            this.buttongenerateContentKey.Name = "buttongenerateContentKey";
            this.buttongenerateContentKey.Size = new System.Drawing.Size(83, 25);
            this.buttongenerateContentKey.TabIndex = 40;
            this.buttongenerateContentKey.Text = "Generate";
            this.buttongenerateContentKey.UseVisualStyleBackColor = true;
            this.buttongenerateContentKey.Click += new System.EventHandler(this.buttongenerateContentKey_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 120);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 36;
            this.label1.Text = "Key Seed:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 13);
            this.label2.TabIndex = 35;
            this.label2.Text = "License Acquisition Url:";
            // 
            // textBoxkeyseed
            // 
            this.textBoxkeyseed.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxkeyseed.Location = new System.Drawing.Point(24, 136);
            this.textBoxkeyseed.Name = "textBoxkeyseed";
            this.textBoxkeyseed.Size = new System.Drawing.Size(414, 20);
            this.textBoxkeyseed.TabIndex = 34;
            this.textBoxkeyseed.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // buttonGenKeyID
            // 
            this.buttonGenKeyID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGenKeyID.Location = new System.Drawing.Point(355, 25);
            this.buttonGenKeyID.Name = "buttonGenKeyID";
            this.buttonGenKeyID.Size = new System.Drawing.Size(83, 25);
            this.buttonGenKeyID.TabIndex = 33;
            this.buttonGenKeyID.Text = "Generate";
            this.buttonGenKeyID.UseVisualStyleBackColor = true;
            this.buttonGenKeyID.Click += new System.EventHandler(this.buttonGenKeyID_Click_1);
            // 
            // textBoxkeyid
            // 
            this.textBoxkeyid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxkeyid.Location = new System.Drawing.Point(24, 28);
            this.textBoxkeyid.Name = "textBoxkeyid";
            this.textBoxkeyid.Size = new System.Drawing.Size(325, 20);
            this.textBoxkeyid.TabIndex = 32;
            this.textBoxkeyid.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // textBoxcontentkey
            // 
            this.textBoxcontentkey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxcontentkey.Location = new System.Drawing.Point(24, 190);
            this.textBoxcontentkey.Name = "textBoxcontentkey";
            this.textBoxcontentkey.Size = new System.Drawing.Size(325, 20);
            this.textBoxcontentkey.TabIndex = 35;
            this.textBoxcontentkey.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(21, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 13);
            this.label5.TabIndex = 31;
            this.label5.Text = "Key ID:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 174);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 34;
            this.label4.Text = "Content key:";
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(460, 236);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Advanced settings";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Image = global::AMSExplorer.Bitmaps.DRM_protection;
            this.buttonOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonOk.Location = new System.Drawing.Point(38, 396);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(208, 32);
            this.buttonOk.TabIndex = 17;
            this.buttonOk.Tag = "";
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // PlayReadyExternalServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(517, 454);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.buttonPlayReadyTestSettings);
            this.Controls.Add(this.label);
            this.Controls.Add(this.moreinfotestserver);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.buttonCancel);
            this.Name = "PlayReadyExternalServer";
            this.Text = "PlayReady External Server Configuration";
            this.Load += new System.EventHandler(this.PlayReadyExternalServer_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel moreinfotestserver;
        public System.Windows.Forms.Button buttonOk;
        public System.Windows.Forms.Button buttonCancel;
        public System.Windows.Forms.Label label;
        private System.Windows.Forms.Button buttonPlayReadyTestSettings;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        public System.Windows.Forms.TextBox textBoxLAurl;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox textBoxkeyseed;
        private System.Windows.Forms.Button buttonGenKeyID;
        public System.Windows.Forms.TextBox textBoxkeyid;
        public System.Windows.Forms.Label label5;
        private System.Windows.Forms.TabPage tabPage2;
        public System.Windows.Forms.TextBox textBoxcontentkey;
        public System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttongenerateContentKey;
    }
}