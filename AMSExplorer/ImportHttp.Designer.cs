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
            this.buttonImport = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxURL = new System.Windows.Forms.TextBox();
            this.labelExamples = new System.Windows.Forms.Label();
            this.textBoxAssetName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxAssetFileName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.labelURLFileNameWarning = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelTitle = new System.Windows.Forms.Label();
            this.labelSASListExample = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonImport
            // 
            this.buttonImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonImport.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonImport.Enabled = false;
            this.buttonImport.Location = new System.Drawing.Point(545, 15);
            this.buttonImport.Name = "buttonImport";
            this.buttonImport.Size = new System.Drawing.Size(115, 27);
            this.buttonImport.TabIndex = 3;
            this.buttonImport.Text = "Import";
            this.buttonImport.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(667, 15);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(115, 27);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(185, 15);
            this.label1.TabIndex = 38;
            this.label1.Text = "Enter the HTTP or HTTPS Source :";
            // 
            // textBoxURL
            // 
            this.textBoxURL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxURL.Location = new System.Drawing.Point(36, 63);
            this.textBoxURL.Name = "textBoxURL";
            this.textBoxURL.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.textBoxURL.Size = new System.Drawing.Size(719, 23);
            this.textBoxURL.TabIndex = 0;
            this.textBoxURL.TextChanged += new System.EventHandler(this.textBoxURL_TextChanged);
            // 
            // labelExamples
            // 
            this.labelExamples.AutoSize = true;
            this.labelExamples.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.labelExamples.Location = new System.Drawing.Point(33, 115);
            this.labelExamples.Name = "labelExamples";
            this.labelExamples.Size = new System.Drawing.Size(396, 60);
            this.labelExamples.TabIndex = 42;
            this.labelExamples.Text = "Examples :\r\nhttp://login:password@hostname.com/path\r\nAmazon : http://awskey:awsse" +
    "cretkey@bucket.s3.amazonaws.com/object\r\nDropbox : http://dl.dropbox.com/object\r\n" +
    "";
            // 
            // textBoxAssetName
            // 
            this.textBoxAssetName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxAssetName.Location = new System.Drawing.Point(10, 44);
            this.textBoxAssetName.Name = "textBoxAssetName";
            this.textBoxAssetName.Size = new System.Drawing.Size(702, 23);
            this.textBoxAssetName.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 15);
            this.label3.TabIndex = 43;
            this.label3.Text = "Name :";
            // 
            // textBoxAssetFileName
            // 
            this.textBoxAssetFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxAssetFileName.Location = new System.Drawing.Point(10, 96);
            this.textBoxAssetFileName.Name = "textBoxAssetFileName";
            this.textBoxAssetFileName.Size = new System.Drawing.Size(702, 23);
            this.textBoxAssetFileName.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 15);
            this.label4.TabIndex = 45;
            this.label4.Text = "File name :";
            // 
            // labelURLFileNameWarning
            // 
            this.labelURLFileNameWarning.AutoSize = true;
            this.labelURLFileNameWarning.ForeColor = System.Drawing.Color.Red;
            this.labelURLFileNameWarning.Location = new System.Drawing.Point(36, 89);
            this.labelURLFileNameWarning.Name = "labelURLFileNameWarning";
            this.labelURLFileNameWarning.Size = new System.Drawing.Size(52, 15);
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
            this.groupBox1.Location = new System.Drawing.Point(36, 186);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(720, 137);
            this.groupBox1.TabIndex = 47;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Asset creation";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonImport);
            this.panel1.Location = new System.Drawing.Point(1, 354);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 55);
            this.panel1.TabIndex = 63;
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.ForeColor = System.Drawing.Color.DarkBlue;
            this.labelTitle.Location = new System.Drawing.Point(32, 9);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(124, 20);
            this.labelTitle.TabIndex = 73;
            this.labelTitle.Text = "Import from Http";
            // 
            // labelSASListExample
            // 
            this.labelSASListExample.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelSASListExample.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.labelSASListExample.Location = new System.Drawing.Point(36, 115);
            this.labelSASListExample.Name = "labelSASListExample";
            this.labelSASListExample.Size = new System.Drawing.Size(719, 60);
            this.labelSASListExample.TabIndex = 74;
            this.labelSASListExample.Text = "Example :\r\nhttps://mediasvcs8gbv2zdblgmj.blob.core.windows.net/asset-d8a38aa5-68f" +
    "6-47e3-a780-c9f27fd7a3b3?sv=2015-12-11&sr=c&si=testpolicy&sig=vgl5aBUUZ4K%2Buv8V" +
    "5pLfrR8tJIXYbux5ryaznzPc9nM%3D";
            this.labelSASListExample.Visible = false;
            // 
            // ImportHttp
            // 
            this.AcceptButton = this.buttonImport;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(798, 410);
            this.Controls.Add(this.labelSASListExample);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.labelURLFileNameWarning);
            this.Controls.Add(this.labelExamples);
            this.Controls.Add(this.textBoxURL);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "ImportHttp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Import from HTTP";
            this.Load += new System.EventHandler(this.ImportHttp_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonImport;
        private System.Windows.Forms.Button buttonCancel;
        public System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxURL;
        public System.Windows.Forms.Label labelExamples;
        private System.Windows.Forms.TextBox textBoxAssetName;
        public System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxAssetFileName;
        public System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelURLFileNameWarning;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelTitle;
        public System.Windows.Forms.Label labelSASListExample;
    }
}