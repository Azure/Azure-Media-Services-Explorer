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
            this.label3 = new System.Windows.Forms.Label();
            this.groupBoxStorage = new System.Windows.Forms.GroupBox();
            this.textBoxAttachStorage = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.listViewStorage = new System.Windows.Forms.ListView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBoxStorage.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonAttach
            // 
            resources.ApplyResources(this.buttonAttach, "buttonAttach");
            this.buttonAttach.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonAttach.Name = "buttonAttach";
            this.buttonAttach.UseVisualStyleBackColor = true;
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
            // textBoxAttachStorage
            // 
            this.textBoxAttachStorage.AcceptsReturn = true;
            resources.ApplyResources(this.textBoxAttachStorage, "textBoxAttachStorage");
            this.textBoxAttachStorage.Name = "textBoxAttachStorage";
            this.toolTip1.SetToolTip(this.textBoxAttachStorage, resources.GetString("textBoxAttachStorage.ToolTip"));
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
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // AttachStorage
            // 
            this.AcceptButton = this.buttonAttach;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBoxStorage);
            this.Name = "AttachStorage";
            this.Load += new System.EventHandler(this.AttachStorage_Load);
            this.groupBoxStorage.ResumeLayout(false);
            this.groupBoxStorage.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonAttach;
        public System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBoxStorage;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListView listViewStorage;
        public System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBoxAttachStorage;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}