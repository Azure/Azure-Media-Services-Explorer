namespace AMSExplorer
{
    partial class AddAMSAccount2Browse
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddAMSAccount2Browse));
            this.buttonNext = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.treeViewAzureSub = new System.Windows.Forms.TreeView();
            this.DGAcct = new System.Windows.Forms.DataGridView();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.comboBoxTenants = new System.Windows.Forms.ComboBox();
            this.labelADTenant = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGAcct)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonNext
            // 
            resources.ApplyResources(this.buttonNext, "buttonNext");
            this.buttonNext.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonNext);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // treeViewAzureSub
            // 
            resources.ApplyResources(this.treeViewAzureSub, "treeViewAzureSub");
            this.treeViewAzureSub.Name = "treeViewAzureSub";
            this.treeViewAzureSub.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.TreeViewAzureSub_BeforeExpand);
            this.treeViewAzureSub.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeViewAzureSub_AfterSelect);
            // 
            // DGAcct
            // 
            this.DGAcct.AllowUserToAddRows = false;
            this.DGAcct.AllowUserToDeleteRows = false;
            this.DGAcct.AllowUserToResizeRows = false;
            resources.ApplyResources(this.DGAcct, "DGAcct");
            this.DGAcct.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DGAcct.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.DGAcct.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGAcct.ColumnHeadersVisible = false;
            this.DGAcct.MultiSelect = false;
            this.DGAcct.Name = "DGAcct";
            this.DGAcct.ReadOnly = true;
            this.DGAcct.RowHeadersVisible = false;
            // 
            // comboBoxTenants
            // 
            resources.ApplyResources(this.comboBoxTenants, "comboBoxTenants");
            this.comboBoxTenants.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTenants.FormattingEnabled = true;
            this.comboBoxTenants.Name = "comboBoxTenants";
            this.comboBoxTenants.SelectedIndexChanged += new System.EventHandler(this.ComboBoxTenants_SelectedIndexChanged);
            // 
            // labelADTenant
            // 
            resources.ApplyResources(this.labelADTenant, "labelADTenant");
            this.labelADTenant.Name = "labelADTenant";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.ForeColor = System.Drawing.Color.DarkBlue;
            this.label2.Name = "label2";
            // 
            // AddAMSAccount2Browse
            // 
            this.AcceptButton = this.buttonNext;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelADTenant);
            this.Controls.Add(this.comboBoxTenants);
            this.Controls.Add(this.DGAcct);
            this.Controls.Add(this.treeViewAzureSub);
            this.Controls.Add(this.panel1);
            this.Name = "AddAMSAccount2Browse";
            this.Load += new System.EventHandler(this.AddAMSAccount2_Load);
            this.Shown += new System.EventHandler(this.AddAMSAccount2Browse_Shown);
            this.DpiChanged += new System.Windows.Forms.DpiChangedEventHandler(this.AddAMSAccount2Browse_DpiChanged);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGAcct)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button buttonNext;
        public System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TreeView treeViewAzureSub;
        private System.Windows.Forms.DataGridView DGAcct;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ComboBox comboBoxTenants;
        private System.Windows.Forms.Label labelADTenant;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}