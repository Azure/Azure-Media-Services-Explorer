namespace AMSExplorer
{
    partial class ImportHttp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportHttp));
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxURL = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxAssetName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxAssetFileName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.labelURLFileNameWarning = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonUpdate.Location = new System.Drawing.Point(234, 307);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(103, 23);
            this.buttonUpdate.TabIndex = 3;
            this.buttonUpdate.Text = "Import";
            this.buttonUpdate.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(353, 307);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(103, 23);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(202, 13);
            this.label1.TabIndex = 38;
            this.label1.Text = "Enter the HTTP, HTTPS or FTP Source :";
            // 
            // textBoxURL
            // 
            this.textBoxURL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxURL.Location = new System.Drawing.Point(31, 43);
            this.textBoxURL.Name = "textBoxURL";
            this.textBoxURL.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.textBoxURL.Size = new System.Drawing.Size(617, 20);
            this.textBoxURL.TabIndex = 0;
            this.textBoxURL.TextChanged += new System.EventHandler(this.textBoxURL_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(363, 52);
            this.label2.TabIndex = 42;
            this.label2.Text = "Examples :\r\nhttp://login:password@hostname.com/path\r\nAmazon : http://awskey:awsse" +
    "cretkey@bucket.s3.amazonaws.com/object\r\nDropbox : http://dl.dropbox.com/object\r\n" +
    "";
            // 
            // textBoxAssetName
            // 
            this.textBoxAssetName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxAssetName.Location = new System.Drawing.Point(9, 41);
            this.textBoxAssetName.Name = "textBoxAssetName";
            this.textBoxAssetName.Size = new System.Drawing.Size(602, 20);
            this.textBoxAssetName.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 43;
            this.label3.Text = "Name :";
            // 
            // textBoxAssetFileName
            // 
            this.textBoxAssetFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxAssetFileName.Location = new System.Drawing.Point(9, 80);
            this.textBoxAssetFileName.Name = "textBoxAssetFileName";
            this.textBoxAssetFileName.Size = new System.Drawing.Size(602, 20);
            this.textBoxAssetFileName.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 45;
            this.label4.Text = "File name :";
            // 
            // labelURLFileNameWarning
            // 
            this.labelURLFileNameWarning.AutoSize = true;
            this.labelURLFileNameWarning.ForeColor = System.Drawing.Color.Red;
            this.labelURLFileNameWarning.Location = new System.Drawing.Point(31, 66);
            this.labelURLFileNameWarning.Name = "labelURLFileNameWarning";
            this.labelURLFileNameWarning.Size = new System.Drawing.Size(47, 13);
            this.labelURLFileNameWarning.TabIndex = 46;
            this.labelURLFileNameWarning.Text = "Warning";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBoxAssetName);
            this.groupBox1.Controls.Add(this.textBoxAssetFileName);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(31, 161);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(617, 119);
            this.groupBox1.TabIndex = 47;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Asset creation";
            // 
            // ImportHttp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(684, 355);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.labelURLFileNameWarning);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxURL);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonUpdate);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ImportHttp";
            this.Text = "Import from HTTP/HTTPS/FTP";
            this.Load += new System.EventHandler(this.ImportHttp_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.Button buttonCancel;
        public System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxURL;
        public System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxAssetName;
        public System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxAssetFileName;
        public System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelURLFileNameWarning;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}