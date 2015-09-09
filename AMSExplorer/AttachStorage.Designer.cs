namespace AMSExplorer
{
    partial class AttachStorage
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
            this.buttonAttach = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxSubId = new System.Windows.Forms.TextBox();
            this.textBoxStorageName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxStorageKey = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxStorageEndPoint = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxCertBody = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.linkLabelAttach = new System.Windows.Forms.LinkLabel();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBoxServiceManagement = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.openFileDialogLoadSubFile = new System.Windows.Forms.OpenFileDialog();
            this.buttonImportSubscriptionFile = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonAttach
            // 
            this.buttonAttach.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAttach.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonAttach.Location = new System.Drawing.Point(537, 15);
            this.buttonAttach.Name = "buttonAttach";
            this.buttonAttach.Size = new System.Drawing.Size(120, 27);
            this.buttonAttach.TabIndex = 5;
            this.buttonAttach.Text = "Attach storage";
            this.buttonAttach.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(664, 15);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(120, 27);
            this.buttonCancel.TabIndex = 6;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 15);
            this.label1.TabIndex = 38;
            this.label1.Text = "Subscription ID :";
            // 
            // textBoxSubId
            // 
            this.textBoxSubId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSubId.BackColor = System.Drawing.Color.Pink;
            this.textBoxSubId.Location = new System.Drawing.Point(10, 51);
            this.textBoxSubId.Name = "textBoxSubId";
            this.textBoxSubId.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.textBoxSubId.Size = new System.Drawing.Size(709, 23);
            this.textBoxSubId.TabIndex = 0;
            this.textBoxSubId.TextChanged += new System.EventHandler(this.textBoxTXT_Validation);
            // 
            // textBoxStorageName
            // 
            this.textBoxStorageName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxStorageName.BackColor = System.Drawing.Color.Pink;
            this.textBoxStorageName.Location = new System.Drawing.Point(10, 47);
            this.textBoxStorageName.Name = "textBoxStorageName";
            this.textBoxStorageName.Size = new System.Drawing.Size(709, 23);
            this.textBoxStorageName.TabIndex = 2;
            this.textBoxStorageName.TextChanged += new System.EventHandler(this.textBoxStorageName_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 15);
            this.label3.TabIndex = 43;
            this.label3.Text = "Name :";
            // 
            // textBoxStorageKey
            // 
            this.textBoxStorageKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxStorageKey.BackColor = System.Drawing.Color.Pink;
            this.textBoxStorageKey.Location = new System.Drawing.Point(10, 92);
            this.textBoxStorageKey.Name = "textBoxStorageKey";
            this.textBoxStorageKey.Size = new System.Drawing.Size(709, 23);
            this.textBoxStorageKey.TabIndex = 3;
            this.textBoxStorageKey.TextChanged += new System.EventHandler(this.textBoxTXT_Validation);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 15);
            this.label4.TabIndex = 45;
            this.label4.Text = "Key :";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.textBoxStorageEndPoint);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBoxStorageName);
            this.groupBox1.Controls.Add(this.textBoxStorageKey);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(36, 385);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(727, 183);
            this.groupBox1.TabIndex = 47;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Storage account to attach";
            // 
            // textBoxStorageEndPoint
            // 
            this.textBoxStorageEndPoint.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxStorageEndPoint.BackColor = System.Drawing.Color.Pink;
            this.textBoxStorageEndPoint.Location = new System.Drawing.Point(10, 138);
            this.textBoxStorageEndPoint.Name = "textBoxStorageEndPoint";
            this.textBoxStorageEndPoint.Size = new System.Drawing.Size(709, 23);
            this.textBoxStorageEndPoint.TabIndex = 4;
            this.textBoxStorageEndPoint.Text = "to be inserted at runtime";
            this.textBoxStorageEndPoint.TextChanged += new System.EventHandler(this.textBoxURL_Validation);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 120);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 15);
            this.label5.TabIndex = 47;
            this.label5.Text = "Endpoint URL :";
            // 
            // textBoxCertBody
            // 
            this.textBoxCertBody.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxCertBody.BackColor = System.Drawing.Color.Pink;
            this.textBoxCertBody.Location = new System.Drawing.Point(10, 111);
            this.textBoxCertBody.Multiline = true;
            this.textBoxCertBody.Name = "textBoxCertBody";
            this.textBoxCertBody.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.textBoxCertBody.Size = new System.Drawing.Size(709, 59);
            this.textBoxCertBody.TabIndex = 1;
            this.textBoxCertBody.TextChanged += new System.EventHandler(this.textBoxTXT_Validation);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(171, 15);
            this.label2.TabIndex = 49;
            this.label2.Text = "Management Certificate Body :";
            // 
            // linkLabelAttach
            // 
            this.linkLabelAttach.AutoSize = true;
            this.linkLabelAttach.Location = new System.Drawing.Point(33, 58);
            this.linkLabelAttach.Name = "linkLabelAttach";
            this.linkLabelAttach.Size = new System.Drawing.Size(287, 15);
            this.linkLabelAttach.TabIndex = 50;
            this.linkLabelAttach.TabStop = true;
            this.linkLabelAttach.Text = "Get your subscription file, save it locally and import it";
            this.linkLabelAttach.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelAttach_LinkClicked);
            // 
            // label6
            // 
            this.label6.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label6.Location = new System.Drawing.Point(33, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(509, 16);
            this.label6.TabIndex = 51;
            this.label6.Text = "This operation cannot be reverted !";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonAttach);
            this.panel1.Location = new System.Drawing.Point(0, 600);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(805, 55);
            this.panel1.TabIndex = 53;
            // 
            // textBoxServiceManagement
            // 
            this.textBoxServiceManagement.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxServiceManagement.BackColor = System.Drawing.Color.Pink;
            this.textBoxServiceManagement.Location = new System.Drawing.Point(10, 203);
            this.textBoxServiceManagement.Name = "textBoxServiceManagement";
            this.textBoxServiceManagement.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.textBoxServiceManagement.Size = new System.Drawing.Size(709, 23);
            this.textBoxServiceManagement.TabIndex = 54;
            this.textBoxServiceManagement.TextChanged += new System.EventHandler(this.textBoxURL_Validation);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 185);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(181, 15);
            this.label7.TabIndex = 55;
            this.label7.Text = "Azure Service Management URL :";
            // 
            // openFileDialogLoadSubFile
            // 
            this.openFileDialogLoadSubFile.DefaultExt = "publishsettings";
            this.openFileDialogLoadSubFile.FileName = "openFileDialog1";
            this.openFileDialogLoadSubFile.Filter = "Subscription file|*.publishsettings|All files|*.*";
            // 
            // buttonImportSubscriptionFile
            // 
            this.buttonImportSubscriptionFile.Location = new System.Drawing.Point(332, 52);
            this.buttonImportSubscriptionFile.Name = "buttonImportSubscriptionFile";
            this.buttonImportSubscriptionFile.Size = new System.Drawing.Size(174, 27);
            this.buttonImportSubscriptionFile.TabIndex = 56;
            this.buttonImportSubscriptionFile.Text = "Import subscription file...";
            this.buttonImportSubscriptionFile.UseVisualStyleBackColor = true;
            this.buttonImportSubscriptionFile.Click += new System.EventHandler(this.buttonImportSubscriptionFile_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.textBoxSubId);
            this.groupBox2.Controls.Add(this.textBoxServiceManagement);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.textBoxCertBody);
            this.groupBox2.Location = new System.Drawing.Point(36, 108);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(727, 252);
            this.groupBox2.TabIndex = 57;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Azure subscription";
            // 
            // AttachStorage
            // 
            this.AcceptButton = this.buttonAttach;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(805, 655);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.buttonImportSubscriptionFile);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.linkLabelAttach);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "AttachStorage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Attach another storage account";
            this.Load += new System.EventHandler(this.AttachStorage_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonAttach;
        private System.Windows.Forms.Button buttonCancel;
        public System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxSubId;
        private System.Windows.Forms.TextBox textBoxStorageName;
        public System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxStorageKey;
        public System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxStorageEndPoint;
        public System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxCertBody;
        public System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel linkLabelAttach;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBoxServiceManagement;
        public System.Windows.Forms.Label label7;
        private System.Windows.Forms.OpenFileDialog openFileDialogLoadSubFile;
        private System.Windows.Forms.Button buttonImportSubscriptionFile;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}