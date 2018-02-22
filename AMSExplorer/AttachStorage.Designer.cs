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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AttachStorage));
            this.buttonAttach = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxSubId = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBoxStorage = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.listViewStorage = new System.Windows.Forms.ListView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.openFileDialogLoadSubFile = new System.Windows.Forms.OpenFileDialog();
            this.groupBoxAMSAcct = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxAMSResourceGroup = new System.Windows.Forms.TextBox();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.textBoxAttachStorage = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBoxStorage.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBoxAMSAcct.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonAttach
            // 
            resources.ApplyResources(this.buttonAttach, "buttonAttach");
            this.buttonAttach.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonAttach.Name = "buttonAttach";
            this.buttonAttach.UseVisualStyleBackColor = true;
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
            this.toolTip1.SetToolTip(this.textBoxSubId, resources.GetString("textBoxSubId.ToolTip"));
            this.textBoxSubId.TextChanged += new System.EventHandler(this.textBoxTXT_Validation);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // groupBoxStorage
            // 
            resources.ApplyResources(this.groupBoxStorage, "groupBoxStorage");
            this.groupBoxStorage.Controls.Add(this.textBoxAttachStorage);
            this.groupBoxStorage.Controls.Add(this.label2);
            this.groupBoxStorage.Controls.Add(this.listViewStorage);
            this.groupBoxStorage.Controls.Add(this.label3);
            this.groupBoxStorage.Name = "groupBoxStorage";
            this.groupBoxStorage.TabStop = false;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // listViewStorage
            // 
            resources.ApplyResources(this.listViewStorage, "listViewStorage");
            this.listViewStorage.CheckBoxes = true;
            this.listViewStorage.FullRowSelect = true;
            this.listViewStorage.GridLines = true;
            this.listViewStorage.Name = "listViewStorage";
            this.listViewStorage.UseCompatibleStateImageBehavior = false;
            this.listViewStorage.View = System.Windows.Forms.View.List;
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.buttonAttach);
            this.panel1.Name = "panel1";
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // openFileDialogLoadSubFile
            // 
            this.openFileDialogLoadSubFile.DefaultExt = "publishsettings";
            resources.ApplyResources(this.openFileDialogLoadSubFile, "openFileDialogLoadSubFile");
            // 
            // groupBoxAMSAcct
            // 
            resources.ApplyResources(this.groupBoxAMSAcct, "groupBoxAMSAcct");
            this.groupBoxAMSAcct.Controls.Add(this.label4);
            this.groupBoxAMSAcct.Controls.Add(this.textBoxAMSResourceGroup);
            this.groupBoxAMSAcct.Controls.Add(this.label1);
            this.groupBoxAMSAcct.Controls.Add(this.textBoxSubId);
            this.groupBoxAMSAcct.Name = "groupBoxAMSAcct";
            this.groupBoxAMSAcct.TabStop = false;
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // textBoxAMSResourceGroup
            // 
            resources.ApplyResources(this.textBoxAMSResourceGroup, "textBoxAMSResourceGroup");
            this.textBoxAMSResourceGroup.BackColor = System.Drawing.Color.Pink;
            this.textBoxAMSResourceGroup.Name = "textBoxAMSResourceGroup";
            this.textBoxAMSResourceGroup.TextChanged += new System.EventHandler(this.textBoxTXT_Validation);
            // 
            // buttonConnect
            // 
            resources.ApplyResources(this.buttonConnect, "buttonConnect");
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // textBoxAttachStorage
            // 
            this.textBoxAttachStorage.AcceptsReturn = true;
            resources.ApplyResources(this.textBoxAttachStorage, "textBoxAttachStorage");
            this.textBoxAttachStorage.Name = "textBoxAttachStorage";
            this.toolTip1.SetToolTip(this.textBoxAttachStorage, resources.GetString("textBoxAttachStorage.ToolTip"));
            // 
            // AttachStorage
            // 
            this.AcceptButton = this.buttonAttach;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.buttonConnect);
            this.Controls.Add(this.groupBoxAMSAcct);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBoxStorage);
            this.Name = "AttachStorage";
            this.Load += new System.EventHandler(this.AttachStorage_Load);
            this.groupBoxStorage.ResumeLayout(false);
            this.groupBoxStorage.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.groupBoxAMSAcct.ResumeLayout(false);
            this.groupBoxAMSAcct.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonAttach;
        public System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxSubId;
        public System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBoxStorage;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.OpenFileDialog openFileDialogLoadSubFile;
        private System.Windows.Forms.GroupBox groupBoxAMSAcct;
        public System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxAMSResourceGroup;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.ListView listViewStorage;
        public System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBoxAttachStorage;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}