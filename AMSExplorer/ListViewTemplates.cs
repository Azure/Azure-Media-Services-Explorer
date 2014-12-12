using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAzure.MediaServices.Client;


namespace AMSExplorer
{
    class ListViewTemplates : ListView
    {
        private int sortColumn = -1;
        private CloudMediaContext _context;

        private System.Windows.Forms.ColumnHeader columnHeaderType;
        private System.Windows.Forms.ColumnHeader columnHeaderTemplateName;
        private System.Windows.Forms.ColumnHeader columnHeaderTemplateDate;
        private System.Windows.Forms.ColumnHeader columnHeaderNbInputAssets;
        private System.Windows.Forms.ColumnHeader columnHeaderJobTemplatetId;

        public IJobTemplate GetSelectedJobTemplate
        {
            get
            {
                if (this.SelectedItems.Count > 0)
                {
                    int indexid = columnHeaderJobTemplatetId.Index;
                    IJobTemplate jobtemplate = _context.JobTemplates.Where(j => j.Id == this.SelectedItems[0].SubItems[indexid].Text).FirstOrDefault();
                    return jobtemplate;
                }
                else
                {
                    return null;
                }
            }
        }

        public ListViewTemplates()
        {
            this.columnHeaderTemplateName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderTemplateDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderNbInputAssets = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderJobTemplatetId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));

            this.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderTemplateName,
            this.columnHeaderTemplateDate,
            this.columnHeaderNbInputAssets,
            this.columnHeaderType,
            this.columnHeaderJobTemplatetId});
            this.FullRowSelect = true;
            this.HideSelection = false;
            this.Location = new System.Drawing.Point(32, 89);
            this.MultiSelect = false;
            this.Name = "listViewTemplates";
            this.Size = new System.Drawing.Size(726, 194);
            this.TabIndex = 61;
            this.UseCompatibleStateImageBehavior = false;
            this.View = System.Windows.Forms.View.Details;
            this.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listViewInputAssets_ColumnClick);
            // 
            // columnHeaderTemplateName
            // 
            this.columnHeaderTemplateName.Text = "Template Name";
            // 
            // columnHeaderTemplateDate
            // 
            this.columnHeaderTemplateDate.Text = "Last modified";
            // 
            // columnHeaderNbInputAssets
            // 
            this.columnHeaderNbInputAssets.Text = "Number input asset(s)";
            // 
            // columnHeaderType
            // 
            this.columnHeaderType.Text = "Type";
            this.columnHeaderType.Width = 55;
            // 
            // columnHeaderJobTemplatetId
            // 
            this.columnHeaderJobTemplatetId.Text = "Id";
        }

        public void LoadTemplates(CloudMediaContext context)
        {
            _context = context;
            LoadTemplates();

        }
        private void LoadTemplates()
        {

            this.BeginUpdate();
            this.Items.Clear();
            foreach (var template in _context.JobTemplates)
            {
                ListViewItem item = new ListViewItem(template.Name);
                item.SubItems.Add(template.LastModified.ToLocalTime().ToString());
                item.SubItems.Add(template.NumberofInputAssets.ToString());
                item.SubItems.Add(template.TemplateType.ToString());
                item.SubItems.Add(template.Id);
                this.Items.Add(item);
            }
            this.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            this.EndUpdate();
        }

        public void DeleteSelectedTemplate()
        {
            IJobTemplate jobtemp = this.GetSelectedJobTemplate;
            if (jobtemp != null)
            {
                if (MessageBox.Show(string.Format("Do you want to delete the job template '{0}' ?", jobtemp.Name), "Job template deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    jobtemp.Delete();
                    this.LoadTemplates();
                }
            }
        }

        private void listViewInputAssets_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Determine whether the column is the same as the last column clicked.
            if (e.Column != sortColumn)
            {
                // Set the sort column to the new column.
                sortColumn = e.Column;
                // Set the sort order to ascending by default.
                this.Sorting = SortOrder.Ascending;
            }
            else
            {
                // Determine what the last sort order was and change it.
                if (this.Sorting == SortOrder.Ascending)
                    this.Sorting = SortOrder.Descending;
                else
                    this.Sorting = SortOrder.Ascending;
            }

            // Call the sort method to manually sort.
            this.Sort();
            // Set the ListViewItemSorter property to a new ListViewItemComparer
            // object.
            this.ListViewItemSorter = new ListViewItemComparer(e.Column,
                                                              this.Sorting);

        }
    }
}
