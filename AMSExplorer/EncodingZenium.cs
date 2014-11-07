//----------------------------------------------------------------------- 
// <copyright file="EncodingZenium.cs" company="Microsoft">Copyright (c) Microsoft Corporation. All rights reserved.</copyright> 
// <license>
// Azure Media Services Explorer Ver. 3.0
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


namespace AMSExplorer
{
    public partial class EncodingZenium : Form
    {
        private int numberofinputassets;

        private Bitmap bitmap_multitasksinglejob = Bitmaps.modeltaskxenio1;
        private Bitmap bitmap_multitasksmultijobs = Bitmaps.modeltaskxenio2;

        private List<IAsset> listblueprints = new List<IAsset>();
        private CloudMediaContext _context;


        public List<IAsset> ZeniumBlueprints
        {
            set
            {
                foreach (IAsset blueprint in value)
                {
                    listbox.Items.Add(blueprint.Name);
                    listblueprints.Add(blueprint);
                }
            }

        }
        public string StorageSelected
        {
            get
            {
                return ((Item)comboBoxStorage.SelectedItem).Value;
            }
        }

        public List<IAsset> SelectedZeniumBlueprints
        {
            get
            {
                List<IAsset> SelecBP = new List<IAsset>();
                if (listbox.SelectedItems.Count > 0)
                {
                    foreach (int index in listbox.SelectedIndices)
                        SelecBP.Add(listblueprints[index]);
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

        public string EncodingProcessorName
        {
            set
            {
                processorlabel.Text = value;
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




        public EncodingZenium(CloudMediaContext context)
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
            + (listbox.SelectedIndices.Count == 1 ? "1 task" : listbox.SelectedIndices.Count.ToString() + " tasks")
            ;

            pictureBoxJob.Image = radioButtonMultipleJob.Checked ? bitmap_multitasksmultijobs : bitmap_multitasksinglejob;

        }

        private void listbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateJobSummary();
        }

        private void EncodingXenio_Load(object sender, EventArgs e)
        {
            moreinfoprofilelink.Links.Add(new LinkLabel.Link(0, moreinfoprofilelink.Text.Length, "http://www.digitalrapids.com/en/Products/KayakMP/FeaturesFormats.aspx"));

            foreach (var storage in _context.StorageAccounts)
            {
                comboBoxStorage.Items.Add(new Item(string.Format("{0} {1}", storage.Name, storage.IsDefault ? "(default)" : ""), storage.Name));
                if (storage.Name == _context.DefaultStorageAccount.Name) comboBoxStorage.SelectedIndex = comboBoxStorage.Items.Count - 1;
            }

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
    }
}
