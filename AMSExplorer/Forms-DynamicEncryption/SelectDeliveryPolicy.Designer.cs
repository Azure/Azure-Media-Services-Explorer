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
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGDelPol)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(659, 15);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(115, 27);
            this.buttonCancel.TabIndex = 40;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonSelect
            // 
            this.buttonSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSelect.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonSelect.Enabled = false;
            this.buttonSelect.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSelect.Location = new System.Drawing.Point(533, 15);
            this.buttonSelect.Name = "buttonSelect";
            this.buttonSelect.Size = new System.Drawing.Size(119, 27);
            this.buttonSelect.TabIndex = 39;
            this.buttonSelect.Text = "Select";
            this.buttonSelect.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonSelect);
            this.panel1.Location = new System.Drawing.Point(-5, 505);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(791, 55);
            this.panel1.TabIndex = 63;
            // 
            // labelStorageAccount
            // 
            this.labelStorageAccount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelStorageAccount.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStorageAccount.ForeColor = System.Drawing.Color.DarkBlue;
            this.labelStorageAccount.Location = new System.Drawing.Point(15, 20);
            this.labelStorageAccount.Name = "labelStorageAccount";
            this.labelStorageAccount.Size = new System.Drawing.Size(746, 23);
            this.labelStorageAccount.TabIndex = 74;
            this.labelStorageAccount.Text = "Select a delivery policy";
            // 
            // listViewPolicies
            // 
            this.listViewPolicies.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewPolicies.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ListViewPoliciesName,
            this.ListViewPoliciesType,
            this.ListViewPoliciesProtocol,
            this.ListViewPoliciesId});
            this.listViewPolicies.FullRowSelect = true;
            this.listViewPolicies.HideSelection = false;
            this.listViewPolicies.Location = new System.Drawing.Point(19, 58);
            this.listViewPolicies.Name = "listViewPolicies";
            this.listViewPolicies.Size = new System.Drawing.Size(257, 389);
            this.listViewPolicies.TabIndex = 75;
            this.listViewPolicies.UseCompatibleStateImageBehavior = false;
            this.listViewPolicies.View = System.Windows.Forms.View.Details;
            this.listViewPolicies.SelectedIndexChanged += new System.EventHandler(this.listViewFiles_SelectedIndexChanged);
            // 
            // ListViewPoliciesName
            // 
            this.ListViewPoliciesName.Text = "Name";
            this.ListViewPoliciesName.Width = 25;
            // 
            // ListViewPoliciesType
            // 
            this.ListViewPoliciesType.Text = "Type";
            // 
            // ListViewPoliciesProtocol
            // 
            this.ListViewPoliciesProtocol.Text = "Protocol";
            // 
            // ListViewPoliciesId
            // 
            this.ListViewPoliciesId.Text = "Id";
            // 
            // DGDelPol
            // 
            this.DGDelPol.AllowUserToAddRows = false;
            this.DGDelPol.AllowUserToDeleteRows = false;
            this.DGDelPol.AllowUserToResizeRows = false;
            this.DGDelPol.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DGDelPol.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DGDelPol.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGDelPol.ColumnHeadersVisible = false;
            this.DGDelPol.Location = new System.Drawing.Point(282, 58);
            this.DGDelPol.MultiSelect = false;
            this.DGDelPol.Name = "DGDelPol";
            this.DGDelPol.ReadOnly = true;
            this.DGDelPol.RowHeadersVisible = false;
            this.DGDelPol.Size = new System.Drawing.Size(487, 423);
            this.DGDelPol.TabIndex = 76;
            // 
            // buttonRename
            // 
            this.buttonRename.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonRename.Enabled = false;
            this.buttonRename.Location = new System.Drawing.Point(149, 453);
            this.buttonRename.Name = "buttonRename";
            this.buttonRename.Size = new System.Drawing.Size(91, 28);
            this.buttonRename.TabIndex = 82;
            this.buttonRename.Text = "Rename";
            this.buttonRename.UseVisualStyleBackColor = true;
            this.buttonRename.Click += new System.EventHandler(this.buttonRename_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDelete.Enabled = false;
            this.buttonDelete.Location = new System.Drawing.Point(19, 453);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(124, 28);
            this.buttonDelete.TabIndex = 81;
            this.buttonDelete.Text = "Delete selection";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // SelectDeliveryPolicy
            // 
            this.AcceptButton = this.buttonSelect;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.buttonRename);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.DGDelPol);
            this.Controls.Add(this.listViewPolicies);
            this.Controls.Add(this.labelStorageAccount);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "SelectDeliveryPolicy";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Policy Selection";
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
    }
}