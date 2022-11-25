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
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.textBoxAttachStorage = new System.Windows.Forms.TextBox();
            this.labelAttachFromList = new System.Windows.Forms.Label();
            this.listViewDetachStorage = new System.Windows.Forms.ListView();
            this.label2 = new System.Windows.Forms.Label();
            this.labelAssetCopy = new System.Windows.Forms.Label();
            this.listViewAttachStorage = new System.Windows.Forms.ListView();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonAttach
            // 
            resources.ApplyResources(this.buttonAttach, "buttonAttach");
            this.buttonAttach.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonAttach.Name = "buttonAttach";
            this.toolTip1.SetToolTip(this.buttonAttach, resources.GetString("buttonAttach.ToolTip"));
            this.buttonAttach.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.buttonAttach);
            this.panel1.Name = "panel1";
            this.toolTip1.SetToolTip(this.panel1, resources.GetString("panel1.ToolTip"));
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Name = "button1";
            this.toolTip1.SetToolTip(this.button1, resources.GetString("button1.ToolTip"));
            this.button1.UseVisualStyleBackColor = true;
            // 
            // textBoxAttachStorage
            // 
            this.textBoxAttachStorage.AcceptsReturn = true;
            resources.ApplyResources(this.textBoxAttachStorage, "textBoxAttachStorage");
            this.textBoxAttachStorage.Name = "textBoxAttachStorage";
            this.toolTip1.SetToolTip(this.textBoxAttachStorage, resources.GetString("textBoxAttachStorage.ToolTip"));
            // 
            // labelAttachFromList
            // 
            resources.ApplyResources(this.labelAttachFromList, "labelAttachFromList");
            this.labelAttachFromList.Name = "labelAttachFromList";
            this.toolTip1.SetToolTip(this.labelAttachFromList, resources.GetString("labelAttachFromList.ToolTip"));
            // 
            // listViewDetachStorage
            // 
            resources.ApplyResources(this.listViewDetachStorage, "listViewDetachStorage");
            this.listViewDetachStorage.CheckBoxes = true;
            this.listViewDetachStorage.FullRowSelect = true;
            this.listViewDetachStorage.GridLines = true;
            this.listViewDetachStorage.Name = "listViewDetachStorage";
            this.listViewDetachStorage.ShowItemToolTips = true;
            this.toolTip1.SetToolTip(this.listViewDetachStorage, resources.GetString("listViewDetachStorage.ToolTip"));
            this.listViewDetachStorage.UseCompatibleStateImageBehavior = false;
            this.listViewDetachStorage.View = System.Windows.Forms.View.List;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            this.toolTip1.SetToolTip(this.label2, resources.GetString("label2.ToolTip"));
            // 
            // labelAssetCopy
            // 
            resources.ApplyResources(this.labelAssetCopy, "labelAssetCopy");
            this.labelAssetCopy.ForeColor = System.Drawing.Color.DarkBlue;
            this.labelAssetCopy.Name = "labelAssetCopy";
            this.toolTip1.SetToolTip(this.labelAssetCopy, resources.GetString("labelAssetCopy.ToolTip"));
            // 
            // listViewAttachStorage
            // 
            resources.ApplyResources(this.listViewAttachStorage, "listViewAttachStorage");
            this.listViewAttachStorage.CheckBoxes = true;
            this.listViewAttachStorage.FullRowSelect = true;
            this.listViewAttachStorage.GridLines = true;
            this.listViewAttachStorage.Name = "listViewAttachStorage";
            this.listViewAttachStorage.ShowItemToolTips = true;
            this.toolTip1.SetToolTip(this.listViewAttachStorage, resources.GetString("listViewAttachStorage.ToolTip"));
            this.listViewAttachStorage.UseCompatibleStateImageBehavior = false;
            this.listViewAttachStorage.View = System.Windows.Forms.View.List;
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            this.toolTip1.SetToolTip(this.label5, resources.GetString("label5.ToolTip"));
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.listViewDetachStorage);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            this.toolTip1.SetToolTip(this.groupBox1, resources.GetString("groupBox1.ToolTip"));
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.labelAttachFromList);
            this.groupBox2.Controls.Add(this.listViewAttachStorage);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.textBoxAttachStorage);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            this.toolTip1.SetToolTip(this.groupBox2, resources.GetString("groupBox2.ToolTip"));
            // 
            // AttachStorage
            // 
            this.AcceptButton = this.buttonAttach;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.labelAssetCopy);
            this.Controls.Add(this.panel1);
            this.Name = "AttachStorage";
            this.toolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.Load += new System.EventHandler(this.AttachStorage_Load);
            this.Shown += new System.EventHandler(this.AttachStorage_Shown);
            this.DpiChanged += new System.Windows.Forms.DpiChangedEventHandler(this.AttachStorage_DpiChanged);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonAttach;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolTip toolTip1;
        public System.Windows.Forms.Label labelAttachFromList;
        private System.Windows.Forms.ListView listViewDetachStorage;
        public System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxAttachStorage;
        private System.Windows.Forms.Label labelAssetCopy;
        private System.Windows.Forms.ListView listViewAttachStorage;
        public System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}