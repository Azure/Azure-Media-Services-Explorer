using Microsoft.WindowsAzure.Storage.Shared.Protocol;

namespace AMSExplorer
{
    partial class SelectAutPolicy
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectAutPolicy));
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSelect = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelStorageAccount = new System.Windows.Forms.Label();
            this.listViewPolicies = new System.Windows.Forms.ListView();
            this.ListViewPoliciesName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ListViewPoliciesId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label3 = new System.Windows.Forms.Label();
            this.dataGridViewAutPolOption = new System.Windows.Forms.DataGridView();
            this.listViewAutPolOptions = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonRename = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAutPolOption)).BeginInit();
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
            // ListViewPoliciesId
            // 
            resources.ApplyResources(this.ListViewPoliciesId, "ListViewPoliciesId");
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // dataGridViewAutPolOption
            // 
            this.dataGridViewAutPolOption.AllowUserToAddRows = false;
            this.dataGridViewAutPolOption.AllowUserToDeleteRows = false;
            this.dataGridViewAutPolOption.AllowUserToResizeRows = false;
            resources.ApplyResources(this.dataGridViewAutPolOption, "dataGridViewAutPolOption");
            this.dataGridViewAutPolOption.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewAutPolOption.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewAutPolOption.ColumnHeadersVisible = false;
            this.dataGridViewAutPolOption.MultiSelect = false;
            this.dataGridViewAutPolOption.Name = "dataGridViewAutPolOption";
            this.dataGridViewAutPolOption.ReadOnly = true;
            this.dataGridViewAutPolOption.RowHeadersVisible = false;
            this.dataGridViewAutPolOption.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewAutPolOption_CellContentClick);
            // 
            // listViewAutPolOptions
            // 
            resources.ApplyResources(this.listViewAutPolOptions, "listViewAutPolOptions");
            this.listViewAutPolOptions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3});
            this.listViewAutPolOptions.FullRowSelect = true;
            this.listViewAutPolOptions.HideSelection = false;
            this.listViewAutPolOptions.MultiSelect = false;
            this.listViewAutPolOptions.Name = "listViewAutPolOptions";
            this.listViewAutPolOptions.UseCompatibleStateImageBehavior = false;
            this.listViewAutPolOptions.View = System.Windows.Forms.View.Details;
            this.listViewAutPolOptions.SelectedIndexChanged += new System.EventHandler(this.listViewAutPolOptions_SelectedIndexChanged);
            // 
            // columnHeader3
            // 
            resources.ApplyResources(this.columnHeader3, "columnHeader3");
            // 
            // buttonDelete
            // 
            resources.ApplyResources(this.buttonDelete, "buttonDelete");
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonRename
            // 
            resources.ApplyResources(this.buttonRename, "buttonRename");
            this.buttonRename.Name = "buttonRename";
            this.buttonRename.UseVisualStyleBackColor = true;
            this.buttonRename.Click += new System.EventHandler(this.buttonRename_Click);
            // 
            // panel2
            // 
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Name = "panel2";
            // 
            // SelectAutPolicy
            // 
            this.AcceptButton = this.buttonSelect;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.buttonRename);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dataGridViewAutPolOption);
            this.Controls.Add(this.listViewAutPolOptions);
            this.Controls.Add(this.listViewPolicies);
            this.Controls.Add(this.labelStorageAccount);
            this.Controls.Add(this.panel1);
            this.Name = "SelectAutPolicy";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.EncodingAMEStandardPickOverlay_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAutPolOption)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonSelect;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelStorageAccount;
        private System.Windows.Forms.ListView listViewPolicies;
        private System.Windows.Forms.ColumnHeader ListViewPoliciesName;
        private System.Windows.Forms.ColumnHeader ListViewPoliciesId;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dataGridViewAutPolOption;
        private System.Windows.Forms.ListView listViewAutPolOptions;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonRename;
        private System.Windows.Forms.Panel panel2;
    }
}