using Microsoft.WindowsAzure.Storage.Shared.Protocol;

namespace AMSExplorer
{
    partial class SelectDeliveryPolicy
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectDeliveryPolicy));
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSelect = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelStorageAccount = new System.Windows.Forms.Label();
            this.listViewPolicies = new System.Windows.Forms.ListView();
            this.ListViewPoliciesName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ListViewPoliciesType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ListViewPoliciesProtocol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ListViewPoliciesId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DGDelPol = new System.Windows.Forms.DataGridView();
            this.buttonRename = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGDelPol)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonSelect
            // 
            resources.ApplyResources(this.buttonSelect, "buttonSelect");
            this.buttonSelect.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonSelect.Name = "buttonSelect";
            this.buttonSelect.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonSelect);
            this.panel1.Name = "panel1";
            // 
            // labelStorageAccount
            // 
            resources.ApplyResources(this.labelStorageAccount, "labelStorageAccount");
            this.labelStorageAccount.ForeColor = System.Drawing.Color.DarkBlue;
            this.labelStorageAccount.Name = "labelStorageAccount";
            // 
            // listViewPolicies
            // 
            resources.ApplyResources(this.listViewPolicies, "listViewPolicies");
            this.listViewPolicies.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ListViewPoliciesName,
            this.ListViewPoliciesType,
            this.ListViewPoliciesProtocol,
            this.ListViewPoliciesId});
            this.listViewPolicies.FullRowSelect = true;
            this.listViewPolicies.HideSelection = false;
            this.listViewPolicies.Name = "listViewPolicies";
            this.listViewPolicies.UseCompatibleStateImageBehavior = false;
            this.listViewPolicies.View = System.Windows.Forms.View.Details;
            this.listViewPolicies.SelectedIndexChanged += new System.EventHandler(this.listViewFiles_SelectedIndexChanged);
            // 
            // ListViewPoliciesName
            // 
            resources.ApplyResources(this.ListViewPoliciesName, "ListViewPoliciesName");
            // 
            // ListViewPoliciesType
            // 
            resources.ApplyResources(this.ListViewPoliciesType, "ListViewPoliciesType");
            // 
            // ListViewPoliciesProtocol
            // 
            resources.ApplyResources(this.ListViewPoliciesProtocol, "ListViewPoliciesProtocol");
            // 
            // ListViewPoliciesId
            // 
            resources.ApplyResources(this.ListViewPoliciesId, "ListViewPoliciesId");
            // 
            // DGDelPol
            // 
            this.DGDelPol.AllowUserToAddRows = false;
            this.DGDelPol.AllowUserToDeleteRows = false;
            this.DGDelPol.AllowUserToResizeRows = false;
            resources.ApplyResources(this.DGDelPol, "DGDelPol");
            this.DGDelPol.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DGDelPol.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGDelPol.ColumnHeadersVisible = false;
            this.DGDelPol.MultiSelect = false;
            this.DGDelPol.Name = "DGDelPol";
            this.DGDelPol.ReadOnly = true;
            this.DGDelPol.RowHeadersVisible = false;
            // 
            // buttonRename
            // 
            resources.ApplyResources(this.buttonRename, "buttonRename");
            this.buttonRename.Name = "buttonRename";
            this.buttonRename.UseVisualStyleBackColor = true;
            this.buttonRename.Click += new System.EventHandler(this.buttonRename_Click);
            // 
            // buttonDelete
            // 
            resources.ApplyResources(this.buttonDelete, "buttonDelete");
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // panel2
            // 
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Name = "panel2";
            // 
            // SelectDeliveryPolicy
            // 
            this.AcceptButton = this.buttonSelect;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.buttonRename);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.DGDelPol);
            this.Controls.Add(this.listViewPolicies);
            this.Controls.Add(this.labelStorageAccount);
            this.Controls.Add(this.panel1);
            this.Name = "SelectDeliveryPolicy";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.EncodingAMEStandardPickOverlay_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGDelPol)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button buttonSelect;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelStorageAccount;
        private System.Windows.Forms.ListView listViewPolicies;
        private System.Windows.Forms.ColumnHeader ListViewPoliciesName;
        private System.Windows.Forms.ColumnHeader ListViewPoliciesType;
        private System.Windows.Forms.ColumnHeader ListViewPoliciesProtocol;
        private System.Windows.Forms.ColumnHeader ListViewPoliciesId;
        private System.Windows.Forms.DataGridView DGDelPol;
        private System.Windows.Forms.Button buttonRename;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Panel panel2;
    }
}