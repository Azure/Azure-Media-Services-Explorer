namespace AMSExplorer
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
            this.buttonOk = new System.Windows.Forms.Button();
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
            this.buttonCancel.Location = new System.Drawing.Point(256, 353);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(120, 32);
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
            // textBoxLAurl
            // 
            this.textBoxLAurl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxLAurl.Location = new System.Drawing.Point(23, 188);
            this.textBoxLAurl.Name = "textBoxLAurl";
            this.textBoxLAurl.Size = new System.Drawing.Size(414, 20);
            this.textBoxLAurl.TabIndex = 37;
            this.textBoxLAurl.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // buttongenerateContentKey
            // 
            this.buttongenerateContentKey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttongenerateContentKey.Location = new System.Drawing.Point(354, 293);
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
            this.label1.Location = new System.Drawing.Point(20, 226);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 36;
            this.label1.Text = "Key Seed:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 172);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 13);
            this.label2.TabIndex = 35;
            this.label2.Text = "License Acquisition Url:";
            // 
            // textBoxkeyseed
            // 
            this.textBoxkeyseed.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxkeyseed.Location = new System.Drawing.Point(23, 242);
            this.textBoxkeyseed.Name = "textBoxkeyseed";
            this.textBoxkeyseed.Size = new System.Drawing.Size(414, 20);
            this.textBoxkeyseed.TabIndex = 34;
            this.textBoxkeyseed.TextChanged += new System.EventHandler(this.textBoxkeyseed_TextChanged);
            // 
            // buttonGenKeyID
            // 
            this.buttonGenKeyID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGenKeyID.Location = new System.Drawing.Point(354, 131);
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
            this.textBoxkeyid.Location = new System.Drawing.Point(23, 134);
            this.textBoxkeyid.Name = "textBoxkeyid";
            this.textBoxkeyid.Size = new System.Drawing.Size(325, 20);
            this.textBoxkeyid.TabIndex = 32;
            this.textBoxkeyid.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // textBoxcontentkey
            // 
            this.textBoxcontentkey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxcontentkey.Location = new System.Drawing.Point(23, 296);
            this.textBoxcontentkey.Name = "textBoxcontentkey";
            this.textBoxcontentkey.Size = new System.Drawing.Size(325, 20);
            this.textBoxcontentkey.TabIndex = 35;
            this.textBoxcontentkey.TextChanged += new System.EventHandler(this.textBoxcontentkey_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 118);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 13);
            this.label5.TabIndex = 31;
            this.label5.Text = "Key ID:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 280);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 34;
            this.label4.Text = "Content key:";
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Enabled = false;
            this.buttonOk.Image = global::AMSExplorer.Bitmaps.DRM_protection;
            this.buttonOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonOk.Location = new System.Drawing.Point(115, 353);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(120, 32);
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
            this.ClientSize = new System.Drawing.Size(479, 410);
            this.Controls.Add(this.textBoxLAurl);
            this.Controls.Add(this.buttongenerateContentKey);
            this.Controls.Add(this.buttonPlayReadyTestSettings);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.moreinfotestserver);
            this.Controls.Add(this.textBoxkeyseed);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.buttonGenKeyID);
            this.Controls.Add(this.textBoxkeyid);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.textBoxcontentkey);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Name = "PlayReadyExternalServer";
            this.Text = "PlayReady External Server Configuration";
            this.Load += new System.EventHandler(this.PlayReadyExternalServer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel moreinfotestserver;
        public System.Windows.Forms.Button buttonOk;
        public System.Windows.Forms.Button buttonCancel;
        public System.Windows.Forms.Label label;
        private System.Windows.Forms.Button buttonPlayReadyTestSettings;
        public System.Windows.Forms.TextBox textBoxLAurl;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox textBoxkeyseed;
        private System.Windows.Forms.Button buttonGenKeyID;
        public System.Windows.Forms.TextBox textBoxkeyid;
        public System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox textBoxcontentkey;
        public System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttongenerateContentKey;
    }
}