//----------------------------------------------------------------------------------------------
//    Copyright 2015 Microsoft Corporation
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
        private CloudMediaContext _context;
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

        public JobOptionsVar JobOptions
        {
            get
            {
                return buttonJobOptions.GetSettings();
            }
            set
            {
                buttonJobOptions.SetSettings(value);
            }
        }



        public List<IAsset> SelectedPremiumWorkflows
        {
            get
            {
                return listViewWorkflows.GetSelectedWorkflow;
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



        public EncodingPremium(CloudMediaContext context)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            _context = context;
            buttonJobOptions.Initialize(_context);
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
            moreinfoprofilelink.Links.Add(new LinkLabel.Link(0, moreinfoprofilelink.Text.Length, Constants.LinkMoreInfoPremiumEncoder));


            listViewWorkflows.LoadWorkflows(_context);
            UpdateJobSummary();
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
                    await Task.Factory.StartNew(() => ProcessUploadFile(file));
                }
                progressBarUpload.Visible = false;
                buttonCancel.Enabled = true;
                buttonUpload.Enabled = true;
                listViewWorkflows.LoadWorkflows(_context);
            }
        }


        private void ProcessUploadFile(string fileName, string storageaccount = null)
        {
            string safeFileName = Path.GetFileName(fileName);
            if (storageaccount == null) storageaccount = _context.DefaultStorageAccount.Name; // no storage account or null, then let's take the default one
            IAsset asset = null;
            try
            {
                asset = _context.Assets.CreateFromFile(
                                                      fileName as string,
                                                      storageaccount,
                                                      Properties.Settings.Default.useStorageEncryption ? AssetCreationOptions.StorageEncrypted : AssetCreationOptions.None,
                                                      (af, p) =>
                                                      {
                                                          progressBarUpload.BeginInvoke(new Action(() => progressBarUpload.Value = (int)p.Progress), null);
                                                      }
                                                      );
                AssetInfo.SetFileAsPrimary(asset, safeFileName);
            }
            catch
            {
            }
        }

        private void listViewWorkflows_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonOk.Enabled = listViewWorkflows.SelectedItems.Count > 0;
        }
    }
}
