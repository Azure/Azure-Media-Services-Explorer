//----------------------------------------------------------------------------------------------
//    Copyright 2019 Microsoft Corporation
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
//---------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAzure.MediaServices.Client;
using System.Drawing;
using System.IO;


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
                item.SubItems.Add(template.LastModified.ToLocalTime().ToString("G"));
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

        public bool PartialQueryLast2Months = false;
        public string ErrorQuery = null;


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

        private void LoadWorkflows() // return true if no error or false if partial query was done
        {
            this.BeginUpdate();
            this.Items.Clear();

            // Server side request
            IAssetFile[] query = new IAssetFile[] { };

            if (_context.Files.Count() < 1000000)
            {
                try
                {
                    query = _context.Files.Where(f => (
                                   f.Name.EndsWith(".workflow")
                                     )).ToArray();
                }

                catch (Exception ex)
                {
                    ErrorQuery = Program.GetErrorMessage(ex);
                }
            }
            else // to many files. In that case let's try to look up only the last two months
            {
                try
                {
                    query = _context.Files.Where(f =>
                    (f.LastModified > DateTime.UtcNow.AddMonths(-2))
                    &&
                    (f.Name.EndsWith(".workflow"))
                                     ).ToArray();
                    PartialQueryLast2Months = true;
                }

                catch (Exception ex)
                {
                    ErrorQuery = Program.GetErrorMessage(ex);
                }
            }


            if (ErrorQuery == null)
            {
                foreach (IAssetFile file in query)
                {
                    if (file.Asset.AssetFiles.Count() == 1)
                    {
                        ListViewItem item = new ListViewItem(file.Name, 0);
                        item.SubItems.Add(file.LastModified.ToLocalTime().ToString("G"));
                        item.SubItems.Add(AssetInfo.FormatByteSize(file.ContentFileSize));
                        item.SubItems.Add(file.Asset.Name);
                        item.SubItems.Add(file.Asset.Id);
                        if (_selectedworkflow != null && _selectedworkflow.Id == file.Asset.Id) item.Selected = true;
                        this.Items.Add(item);
                    }
                }
            }

            this.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            this.EndUpdate();

        }
    }



    class ListViewSlateJPG : ListView
    {
        private CloudMediaContext _context;
        private IAsset _selectedJPGAsset;
        private ChannelSlate _channelslate;
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

        public ListViewSlateJPG()
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

        public void LoadJPGs(CloudMediaContext context, IAsset selectedJPG = null, ChannelSlate channelslate = null)
        {
            _context = context;
            _selectedJPGAsset = selectedJPG;
            _channelslate = channelslate;
            LoadJPGs();
        }

        public void LoadJPGs(string searchstring = "")
        {
            this.BeginUpdate();
            this.Items.Clear();

            string searchlower = searchstring.ToLower();
            bool bsearchempty = string.IsNullOrEmpty(searchstring);

            // this query is done in the back-end
            var query = _context.Files.Where(f =>
                        f.Name.EndsWith(Constants.SlateJPGExtension)
                        &&
                        f.IsPrimary
                        &&
                        f.ContentFileSize <= Constants.maxSlateJPGFileSize
                        &&
                        (bsearchempty || f.Name.Contains(searchlower))
                        ).AsEnumerable();

            // local query
            query = query.Where(f =>
            bsearchempty || (f.Id.ToLower().Contains(searchlower) || f.Asset.Name.ToLower().Contains(searchlower) || f.Asset.Id.ToLower().Contains(searchlower)));

            string defaultslateassetid = null;
            if (_channelslate != null && _channelslate.DefaultSlateAssetId != null)
            {
                defaultslateassetid = _channelslate.DefaultSlateAssetId;
            }

            foreach (IAssetFile file in query)
            {
                if (file.Asset.AssetFiles.Count() == 1)
                {
                    bool bdefaultchannelslate = defaultslateassetid == file.ParentAssetId;

                    ListViewItem item = new ListViewItem(file.Name + ((bdefaultchannelslate) ? " (default channel slate)" : string.Empty), 0);
                    item.SubItems.Add(file.LastModified.ToLocalTime().ToString("G"));
                    item.SubItems.Add(AssetInfo.FormatByteSize(file.ContentFileSize));
                    item.SubItems.Add(file.Asset.Name);
                    item.SubItems.Add(file.Asset.Id);
                    if (_selectedJPGAsset != null && _selectedJPGAsset.Id == file.Asset.Id) item.Selected = true;
                    if (bdefaultchannelslate) item.ForeColor = Color.Blue;
                    this.Items.Add(item);
                }
            }
            this.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            this.EndUpdate();
        }

        public static bool AreClose(double value1, double value2)
        {
            const double DBL_EPSILON = 1e-6;
            //in case they are Infinities (then epsilon check does not work)
            if (value1 == value2) return true;
            // This computes (|value1-value2| / (|value1| + |value2| + 10.0)) < DBL_EPSILON
            double eps = (Math.Abs(value1) + Math.Abs(value2) + 10.0) * DBL_EPSILON;
            double delta = value1 - value2;
            return (-eps < delta) && (eps > delta);
        }

        public static string CheckSlateFile(string file) // return null if ok. Otherwise, error is the string.
        {
            string returnString = null;

            bool Error = false;
            FileInfo fileInfo = null;
            Image fileImage = null;
            double aspectRatioImage = 0d;

            try
            {
                fileInfo = new FileInfo(file);
                fileImage = Image.FromFile(file);
                aspectRatioImage = (double)fileImage.Size.Width / (double)fileImage.Size.Height;
            }
            catch
            {
                Error = true;
                returnString = string.Format("Error when accessing the file\n'{0}'.", file);
            }
            if (!Error)
            {
                if (fileInfo.Extension.ToLower() != Constants.SlateJPGExtension)  // file has not an .jpg extension
                {
                    returnString = string.Format("The file\n'{0}'\nhas not a {1} extension", file, Constants.SlateJPGExtension);
                }
                else if (!fileImage.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Jpeg))  // file is not a JPEG
                {
                    returnString = string.Format("The file\n'{0}'\nis not a JPEG file", file);
                }
                else if (fileInfo.Length > Constants.maxSlateJPGFileSize)  // file size > 3 MB, not ok
                {
                    returnString = string.Format("The file\n'{0}'\nhas a size of {1} which is larger than {2}", file, AssetInfo.FormatByteSize(fileInfo.Length), AssetInfo.FormatByteSize(Constants.maxSlateJPGFileSize));
                }
                else if (fileImage.Size.Width > Constants.maxSlateJPGHorizontalResolution || fileImage.Size.Height > Constants.maxSlateJPGVerticalResolution)
                {
                    returnString = string.Format("The file\n'{0}'\nhas a resolution  of {1}x{2} which is larger than {3}x{4}", file, fileImage.Size.Width, fileImage.Size.Height, Constants.maxSlateJPGHorizontalResolution, Constants.maxSlateJPGVerticalResolution);
                }
                else if (!AreClose(aspectRatioImage, Constants.SlateJPGAspectRatio))
                {
                    returnString = string.Format("The file\n'{0}'\nhas an aspect ratio of {1:0.000} which is different from {2:0.000} (16:9)", file, aspectRatioImage, Constants.SlateJPGAspectRatio);
                }
            }
            return returnString;
        }
    }
}
