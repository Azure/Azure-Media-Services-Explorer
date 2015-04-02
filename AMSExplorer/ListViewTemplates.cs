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
        private CloudMediaContext _context;
        private IJobTemplate _selectedjobtemplate;
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
            this.Tag = -1;
            this.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(ListViewItemComparer.ListView_ColumnClick);
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

        public void LoadTemplates(CloudMediaContext context, IJobTemplate selectedjobtemplate = null)
        {
            _context = context;
            _selectedjobtemplate = selectedjobtemplate;
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
                if (_selectedjobtemplate != null && _selectedjobtemplate.Id == template.Id) item.Selected = true;
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
    }



    class ListViewWorkflows : ListView
    {
        private CloudMediaContext _context;
        private IAsset _selectedworkflow;
        private System.Windows.Forms.ColumnHeader columnHeaderWorkflowFileName;
        private System.Windows.Forms.ColumnHeader columnHeaderLastModified;
        private System.Windows.Forms.ColumnHeader columnHeaderSize;
        private System.Windows.Forms.ColumnHeader columnHeaderAssetName;
        private System.Windows.Forms.ColumnHeader columnHeaderAssetId;

        public List<IAsset> GetSelectedWorkflow
        {
            get
            {
                List<IAsset> SelecBP = new List<IAsset>();
                if (this.SelectedItems.Count > 0)
                {
                    int indexid = columnHeaderAssetId.Index;

                    foreach (ListViewItem itemw in this.SelectedItems)
                    {
                        string sid = itemw.SubItems[indexid].Text;
                        SelecBP.Add(AssetInfo.GetAsset(itemw.SubItems[indexid].Text, _context));
                    }
                }
                return SelecBP;
            }
        }

        public ListViewWorkflows()
        {
            this.columnHeaderWorkflowFileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderLastModified = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderAssetName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderAssetId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));

            this.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderWorkflowFileName,
            this.columnHeaderLastModified,
            this.columnHeaderSize,
            this.columnHeaderAssetName,
            this.columnHeaderAssetId});
            this.FullRowSelect = true;
            this.HideSelection = false;
            this.Location = new System.Drawing.Point(32, 89);
            this.MultiSelect = true;
            this.Name = "listViewWorkflows";
            this.Size = new System.Drawing.Size(726, 194);
            this.TabIndex = 61;
            this.UseCompatibleStateImageBehavior = false;
            this.View = System.Windows.Forms.View.Details;
            this.Tag = -1;
            this.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(ListViewItemComparer.ListView_ColumnClick);
            // 
            // columnHeaderWorkflowFileName
            // 
            this.columnHeaderWorkflowFileName.Text = "Workflow File Name";
            // 
            // columnHeaderLastModified
            // 
            this.columnHeaderLastModified.Text = "Last modified";
            // 
            // columnHeaderSize
            // 
            this.columnHeaderSize.Text = "Size";
            // 
            // columnHeaderAssetName
            // 
            this.columnHeaderAssetName.Text = "Asset Name";
            // 
            // columnHeaderAssetId
            // 
            this.columnHeaderAssetId.Text = "Asset Id";
        }

        public void LoadWorkflows(CloudMediaContext context, IAsset selectedworkflow = null)
        {
            _context = context;
            _selectedworkflow = selectedworkflow;
            LoadWorkflows();
        }

        private void LoadWorkflows()
        {
            this.BeginUpdate();
            this.Items.Clear();

            var query = _context.Files.ToList().Where(f => (
          f.Name.EndsWith(".xenio", StringComparison.OrdinalIgnoreCase)
          || f.Name.EndsWith(".kayak", StringComparison.OrdinalIgnoreCase)
          || f.Name.EndsWith(".workflow", StringComparison.OrdinalIgnoreCase)
          || f.Name.EndsWith(".blueprint", StringComparison.OrdinalIgnoreCase)
          || f.Name.EndsWith(".graph", StringComparison.OrdinalIgnoreCase)
          || f.Name.EndsWith(".zenium", StringComparison.OrdinalIgnoreCase)
          )).ToArray();

            foreach (IAssetFile file in query)
            {
                if (file.Asset.AssetFiles.Count() == 1)
                {
                    ListViewItem item = new ListViewItem(file.Name, 0);
                    item.SubItems.Add(file.LastModified.ToLocalTime().ToString());
                    item.SubItems.Add(AssetInfo.FormatByteSize(file.ContentFileSize));
                    item.SubItems.Add(file.Asset.Name);
                    item.SubItems.Add(file.Asset.Id);
                    if (_selectedworkflow != null && _selectedworkflow.Id == file.Asset.Id) item.Selected = true;
                    this.Items.Add(item);
                }
            }
            this.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            this.EndUpdate();
        }
    }



    class ListViewJPG : ListView
    {
        private CloudMediaContext _context;
        private IAsset _selectedJPGAsset;
        private System.Windows.Forms.ColumnHeader columnHeaderJPGFileName;
        private System.Windows.Forms.ColumnHeader columnHeaderLastModified;
        private System.Windows.Forms.ColumnHeader columnHeaderSize;
        private System.Windows.Forms.ColumnHeader columnHeaderAssetName;
        private System.Windows.Forms.ColumnHeader columnHeaderAssetId;

        public List<IAsset> GetSelectedJPG
        {
            get
            {
                List<IAsset> SelecBP = new List<IAsset>();
                if (this.SelectedItems.Count > 0)
                {
                    int indexid = columnHeaderAssetId.Index;

                    foreach (ListViewItem itemw in this.SelectedItems)
                    {
                        string sid = itemw.SubItems[indexid].Text;
                        SelecBP.Add(AssetInfo.GetAsset(itemw.SubItems[indexid].Text, _context));
                    }
                }
                return SelecBP;
            }
        }

        public ListViewJPG()
        {
            this.columnHeaderJPGFileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderLastModified = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderAssetName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderAssetId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));

            this.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderJPGFileName,
            this.columnHeaderLastModified,
            this.columnHeaderSize,
            this.columnHeaderAssetName,
            this.columnHeaderAssetId});
            this.FullRowSelect = true;
            this.HideSelection = false;
            this.Location = new System.Drawing.Point(32, 89);
            this.MultiSelect = true;
            this.Name = "listViewWorkflows";
            this.Size = new System.Drawing.Size(726, 194);
            this.TabIndex = 61;
            this.UseCompatibleStateImageBehavior = false;
            this.View = System.Windows.Forms.View.Details;
            this.Tag = -1;
            this.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(ListViewItemComparer.ListView_ColumnClick);
            // 
            // columnHeaderWorkflowFileName
            // 
            this.columnHeaderJPGFileName.Text = "JPG File Name";
            // 
            // columnHeaderLastModified
            // 
            this.columnHeaderLastModified.Text = "Last modified";
            // 
            // columnHeaderSize
            // 
            this.columnHeaderSize.Text = "Size";
            // 
            // columnHeaderAssetName
            // 
            this.columnHeaderAssetName.Text = "Asset Name";
            // 
            // columnHeaderAssetId
            // 
            this.columnHeaderAssetId.Text = "Asset Id";
        }

        public void LoadJPGs(CloudMediaContext context, IAsset selectedJPG = null)
        {
            _context = context;
            _selectedJPGAsset = selectedJPG;
            LoadJPGs();
        }

        public void LoadJPGs(string searchstring = "")
        {
            this.BeginUpdate();
            this.Items.Clear();

            string searchlower = searchstring.ToLower();
            bool bsearchempty = string.IsNullOrEmpty(searchstring);
            var query = _context.Files.ToList().Where(f =>
                ((f.Name.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) || f.Name.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase)) && f.IsPrimary)
                &&
                (
                bsearchempty
                ||
                (f.Name.ToLower().Contains(searchlower) || f.Id.ToLower().Contains(searchlower) || f.Asset.Name.ToLower().Contains(searchlower) || f.Asset.Id.ToLower().Contains(searchlower)))
                )
                .ToArray();

            foreach (IAssetFile file in query)
            {
                if (file.Asset.AssetFiles.Count() == 1)
                {
                    ListViewItem item = new ListViewItem(file.Name, 0);
                    item.SubItems.Add(file.LastModified.ToLocalTime().ToString());
                    item.SubItems.Add(AssetInfo.FormatByteSize(file.ContentFileSize));
                    item.SubItems.Add(file.Asset.Name);
                    item.SubItems.Add(file.Asset.Id);
                    if (_selectedJPGAsset != null && _selectedJPGAsset.Id == file.Asset.Id) item.Selected = true;

                    this.Items.Add(item);
                }
            }
            this.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            this.EndUpdate();
        }
    }


}
