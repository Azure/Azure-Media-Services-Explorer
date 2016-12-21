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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AttachStorage));
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
            resources.ApplyResources(this.buttonAttach, "buttonAttach");
            this.buttonAttach.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonAttach.Name = "buttonAttach";
            this.buttonAttach.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // textBoxSubId
            // 
            resources.ApplyResources(this.textBoxSubId, "textBoxSubId");
            this.textBoxSubId.BackColor = System.Drawing.Color.Pink;
            this.textBoxSubId.Name = "textBoxSubId";
            this.textBoxSubId.TextChanged += new System.EventHandler(this.textBoxTXT_Validation);
            // 
            // textBoxStorageName
            // 
            resources.ApplyResources(this.textBoxStorageName, "textBoxStorageName");
            this.textBoxStorageName.BackColor = System.Drawing.Color.Pink;
            this.textBoxStorageName.Name = "textBoxStorageName";
            this.textBoxStorageName.TextChanged += new System.EventHandler(this.textBoxStorageName_TextChanged);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // textBoxStorageKey
            // 
            resources.ApplyResources(this.textBoxStorageKey, "textBoxStorageKey");
            this.textBoxStorageKey.BackColor = System.Drawing.Color.Pink;
            this.textBoxStorageKey.Name = "textBoxStorageKey";
            this.textBoxStorageKey.TextChanged += new System.EventHandler(this.textBoxTXT_Validation);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.textBoxStorageEndPoint);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBoxStorageName);
            this.groupBox1.Controls.Add(this.textBoxStorageKey);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // textBoxStorageEndPoint
            // 
            resources.ApplyResources(this.textBoxStorageEndPoint, "textBoxStorageEndPoint");
            this.textBoxStorageEndPoint.BackColor = System.Drawing.Color.Pink;
            this.textBoxStorageEndPoint.Name = "textBoxStorageEndPoint";
            this.textBoxStorageEndPoint.TextChanged += new System.EventHandler(this.textBoxURL_Validation);
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // textBoxCertBody
            // 
            resources.ApplyResources(this.textBoxCertBody, "textBoxCertBody");
            this.textBoxCertBody.BackColor = System.Drawing.Color.Pink;
            this.textBoxCertBody.Name = "textBoxCertBody";
            this.textBoxCertBody.TextChanged += new System.EventHandler(this.textBoxTXT_Validation);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // linkLabelAttach
            // 
            resources.ApplyResources(this.linkLabelAttach, "linkLabelAttach");
            this.linkLabelAttach.Name = "linkLabelAttach";
            this.linkLabelAttach.TabStop = true;
            this.linkLabelAttach.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelAttach_LinkClicked);
            // 
            // label6
            // 
            this.label6.ForeColor = System.Drawing.SystemColors.WindowFrame;
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonAttach);
            this.panel1.Name = "panel1";
            // 
            // textBoxServiceManagement
            // 
            resources.ApplyResources(this.textBoxServiceManagement, "textBoxServiceManagement");
            this.textBoxServiceManagement.BackColor = System.Drawing.Color.Pink;
            this.textBoxServiceManagement.Name = "textBoxServiceManagement";
            this.textBoxServiceManagement.TextChanged += new System.EventHandler(this.textBoxURL_Validation);
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // openFileDialogLoadSubFile
            // 
            this.openFileDialogLoadSubFile.DefaultExt = "publishsettings";
            resources.ApplyResources(this.openFileDialogLoadSubFile, "openFileDialogLoadSubFile");
            // 
            // buttonImportSubscriptionFile
            // 
            resources.ApplyResources(this.buttonImportSubscriptionFile, "buttonImportSubscriptionFile");
            this.buttonImportSubscriptionFile.Name = "buttonImportSubscriptionFile";
            this.buttonImportSubscriptionFile.UseVisualStyleBackColor = true;
            this.buttonImportSubscriptionFile.Click += new System.EventHandler(this.buttonImportSubscriptionFile_Click);
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.textBoxSubId);
            this.groupBox2.Controls.Add(this.textBoxServiceManagement);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.textBoxCertBody);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // AttachStorage
            // 
            this.AcceptButton = this.buttonAttach;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.buttonImportSubscriptionFile);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.linkLabelAttach);
            this.Controls.Add(this.groupBox1);
            this.Name = "AttachStorage";
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