//----------------------------------------------------------------------- 
// <copyright file="EncodingPremium.cs" company="Microsoft">Copyright (c) Microsoft Corporation. All rights reserved.</copyright> 
// <license>
// Azure Media Services Explorer Ver. 3.1
// Licensed under the Apache License, Version 2.0 (the "License"); 
// you may not use this file except in compliance with the License. 
// You may obtain a copy of the License at 
//  
// http://www.apache.org/licenses/LICENSE-2.0 
//  
// Unless required by applicable law or agreed to in writing, software 
// distributed under the License is distributed on an "AS IS" BASIS, 
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
// See the License for the specific language governing permissions and 
// limitations under the License. 
// </license> 

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using Microsoft.WindowsAzure.MediaServices.Client;
using System.Collections;
using System.IO;

namespace AMSExplorer
{
    public partial class EncodingPremium : Form
    {
        private int numberofinputassets;
        private List<IMediaProcessor> Procs;

        private Bitmap bitmap_multitasksinglejob = Bitmaps.modeltaskxenio1;
        private Bitmap bitmap_multitasksmultijobs = Bitmaps.modeltaskxenio2;

        //private List<IAsset> listblueprints = new List<IAsset>();
        private CloudMediaContext _context;

        private int sortColumn = -1;
        public string EncodingPremiumWorkflowPresetXMLFiles;

        public List<IMediaProcessor> EncodingProcessorsList
        {
            set
            {
                foreach (IMediaProcessor pr in value)
                    comboBoxProcessor.Items.Add(string.Format("{0} {1} Version {2}", pr.Vendor, pr.Name, pr.Version));
                comboBoxProcessor.SelectedIndex = 0;
                Procs = value;
            }
        }

        public IMediaProcessor EncodingProcessorSelected
        {
            get
            {
                return Procs[comboBoxProcessor.SelectedIndex];
            }
        }


        public string StorageSelected
        {
            get
            {
                return ((Item)comboBoxStorage.SelectedItem).Value;
            }
        }

        public List<IAsset> SelectedPremiumWorkflows
        {
            get
            {
                List<IAsset> SelecBP = new List<IAsset>();
                if (listViewWorkflows.SelectedItems.Count > 0)
                {
                    int indexid = columnHeaderAssetId.Index;

                    foreach (ListViewItem itemw in listViewWorkflows.SelectedItems)
                    {
                        string sid = itemw.SubItems[indexid].Text;
                        SelecBP.Add(AssetInfo.GetAsset(itemw.SubItems[indexid].Text, _context));
                    }
                }
                return SelecBP;
            }
        }

        public bool EncodingMultipleJobs
        {
            get
            {
                return !radioButtonSingleJob.Checked;
            }
            set
            {
                radioButtonSingleJob.Checked = !value;
                radioButtonMultipleJob.Checked = value;
            }
        }


        public int EncodingNumberInputAssets
        {
            set
            {
                if (value == 1)
                {
                    radioButtonMultipleJob.Enabled = false;
                    radioButtonSingleJob.Enabled = false;
                }
                else
                {
                    radioButtonMultipleJob.Checked = true;

                }
                numberofinputassets = value;
            }
        }
        public string EncodingJobName
        {
            get
            {
                return textBoxJobName.Text;
            }
            set
            {
                textBoxJobName.Text = value;
            }
        }


        public string EncodingOutputAssetName
        {
            get
            {
                return textboxoutputassetname.Text;
            }
            set
            {
                textboxoutputassetname.Text = value;
            }
        }


        public string EncodingPromptText
        {
            set
            {
                label.Text = value;
            }
        }

        public int EncodingPriority
        {
            get
            {
                return (int)numericUpDownPriority.Value;
            }
            set
            {
                numericUpDownPriority.Value = value;
            }
        }

        public EncodingPremium(CloudMediaContext context)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            _context = context;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {

        }

        private void moreinfoprofilelink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Send the URL to the operating system.
            Process.Start(e.Link.LinkData as string);
        }

        private void radioButtonMultipleJob_CheckedChanged(object sender, EventArgs e)
        {
            UpdateJobSummary();
        }

        private void UpdateJobSummary()
        {
            labelsummaryjob.Text = "You are going to submit "
                + (radioButtonMultipleJob.Checked ? numberofinputassets.ToString() + " jobs" : "1 job")
            + " with "
            + (listViewWorkflows.SelectedIndices.Count == 1 ? "1 task" : listViewWorkflows.SelectedIndices.Count.ToString() + " tasks")
            ;

            pictureBoxJob.Image = radioButtonMultipleJob.Checked ? bitmap_multitasksmultijobs : bitmap_multitasksinglejob;

        }

        private void listbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateJobSummary();
            buttonOk.Enabled = listViewWorkflows.SelectedItems.Count > 0;
        }

        private void EncodingPremiumWorkflow_Load(object sender, EventArgs e)
        {
            moreinfoprofilelink.Links.Add(new LinkLabel.Link(0, moreinfoprofilelink.Text.Length, "http://aka.ms/amspremium"));

            foreach (var storage in _context.StorageAccounts)
            {
                comboBoxStorage.Items.Add(new Item(string.Format("{0} {1}", storage.Name, storage.IsDefault ? "(default)" : ""), storage.Name));
                if (storage.Name == _context.DefaultStorageAccount.Name) comboBoxStorage.SelectedIndex = comboBoxStorage.Items.Count - 1;
            }
            LoadWorkflows();
            UpdateJobSummary();
            listViewWorkflows.Tag = -1;
            listViewWorkflows.ColumnClick += ListViewItemComparer.ListView_ColumnClick;

        }

        private void LoadWorkflows()
        {
            var query = _context.Files.ToList().Where(f => (
                  f.Name.EndsWith(".xenio", StringComparison.OrdinalIgnoreCase)
                  || f.Name.EndsWith(".kayak", StringComparison.OrdinalIgnoreCase)
                  || f.Name.EndsWith(".workflow", StringComparison.OrdinalIgnoreCase)
                  || f.Name.EndsWith(".blueprint", StringComparison.OrdinalIgnoreCase)
                  || f.Name.EndsWith(".graph", StringComparison.OrdinalIgnoreCase)
                  || f.Name.EndsWith(".zenium", StringComparison.OrdinalIgnoreCase)
                  )).ToArray();

            listViewWorkflows.BeginUpdate();
            listViewWorkflows.Items.Clear();
            foreach (IAssetFile file in query)
            {
                if (file.Asset.AssetFiles.Count() == 1)
                {
                    ListViewItem item = new ListViewItem(file.Name, 0);
                    item.SubItems.Add(file.LastModified.ToLocalTime().ToString());
                    item.SubItems.Add(AssetInfo.FormatByteSize(file.ContentFileSize));
                    item.SubItems.Add(file.Asset.Name);
                    item.SubItems.Add(file.Asset.Id);
                    listViewWorkflows.Items.Add(item);
                }
            }
            listViewWorkflows.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            listViewWorkflows.EndUpdate();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void outputassetname_TextChanged(object sender, EventArgs e)
        {

        }


        private void buttonUpload_Click(object sender, EventArgs e)
        {
            DoUpload();
        }

        private async void DoUpload()
        {
            if (Directory.Exists(this.EncodingPremiumWorkflowPresetXMLFiles))
                openFileDialogWorkflow.InitialDirectory = this.EncodingPremiumWorkflowPresetXMLFiles;


            if (openFileDialogWorkflow.ShowDialog() == DialogResult.OK)
            {
                this.EncodingPremiumWorkflowPresetXMLFiles = Path.GetDirectoryName(openFileDialogWorkflow.FileName); // let's save the folder
                progressBarUpload.Value = 0;
                progressBarUpload.Visible = true;
                buttonCancel.Enabled = false;
                buttonUpload.Enabled = false;
                foreach (string file in openFileDialogWorkflow.FileNames)
                {
                    await Task.Factory.StartNew(() => ProcessUploadFile(Path.GetFileName(file), file));
                }
                progressBarUpload.Visible = false;
                buttonCancel.Enabled = true;
                buttonUpload.Enabled = true;
                LoadWorkflows();
            }
        }


        private void ProcessUploadFile(string SafeFileName, string FileName, string storageaccount = null)
        {

            if (storageaccount == null) storageaccount = _context.DefaultStorageAccount.Name; // no storage account or null, then let's take the default one


            bool Error = false;
            IAsset asset = null;
            try
            {
                asset = _context.Assets.CreateFromFile(
                                                      FileName as string,
                                                      storageaccount,
                                                      Properties.Settings.Default.useStorageEncryption ? AssetCreationOptions.StorageEncrypted : AssetCreationOptions.None,
                                                      (af, p) =>
                                                      {
                                                          progressBarUpload.BeginInvoke(new Action(() => progressBarUpload.Value = (int)p.Progress), null);
                                                      }
                                                      );
            }
            catch (Exception e)
            {
                Error = true;

            }
            if (!Error)
            {

            }

        }


    }


}
