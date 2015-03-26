//----------------------------------------------------------------------- 
// <copyright file="GenericProcessor.cs" company="Microsoft">Copyright (c) Microsoft Corporation. All rights reserved.</copyright> 
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
using System.Xml;
using System.Xml.Linq;
using Microsoft.WindowsAzure.MediaServices.Client;
using System.Reflection;

namespace AMSExplorer
{
    public partial class GenericProcessor : Form
    {
        public XDocument doc;
        public List<IAsset> SelectedAssets;

        private bool init = true;

        private Bitmap bitmap_multitasksinglejob = Bitmaps.modetaskjob1;
        private Bitmap bitmap_multitasksmultijobs = Bitmaps.modetaskjob2;
        private Bitmap bitmap_singletasksinglejob = Bitmaps.modetaskjob3;

        private List<IMediaProcessor> Procs;
        private CloudMediaContext _context;
        private IJob _myJob;

        private int numberoftasks = 1;

        private new List<List<GenericTaskAsset>> listofinputassets;  // for each task

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
        public string StorageSelected
        {
            get
            {
                return ((Item)comboBoxStorage.SelectedItem).Value;
            }
        }


        public List<IMediaProcessor> EncodingProcessorsList
        {
            set
            {
                for (int i = 1; i < 6; i++)
                {
                    ListView mylistview = (ListView)this.Controls.Find("listViewProcessors" + i.ToString(), true).FirstOrDefault();
                    mylistview.BeginUpdate();

                    foreach (IMediaProcessor proc in value)
                    {
                        ListViewItem item = new ListViewItem(proc.Vendor, 0);
                        item.SubItems.Add(proc.Name);
                        item.SubItems.Add(proc.Version);
                        item.SubItems.Add(proc.Description);
                        mylistview.Items.Add(item);
                    }

                    mylistview.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                    mylistview.EndUpdate();
                }

                Procs = value;

                if (_myJob != null) // we are in resubmit mode
                {
                    IMediaProcessor mp = _context.MediaProcessors.Where(p => p.Id == _myJob.Tasks.FirstOrDefault().MediaProcessorId).FirstOrDefault();
                    if (mp != null)
                    {
                        int indexmp = Procs.IndexOf(mp);
                        if (indexmp > -1) // processor found
                        {
                            listViewProcessors1.Items[indexmp].Selected = true;
                            listViewProcessors1.Select();
                        }
                    }
                }

            }
        }

        public IMediaProcessor EncodingProcessorSelected
        {
            get
            {
                return Procs[listViewProcessors1.SelectedIndices[0]];
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


        public string EncodingConfiguration
        {
            get
            {
                return textBoxConfiguration1.Text;
            }
        }

        public List<GenericTask> GetGenericTasks
        {
            get
            {
                List<GenericTask> listtasks = new List<GenericTask>();
                for (int index_task = 1; index_task <= numericUpDownTasks.Value; index_task++)
                {

                    ComboBox mycomboboxassetinput = (ComboBox)this.Controls.Find("comboBoxAssetInput" + index_task.ToString(), true).FirstOrDefault();
                    ListView mylistviewprocessor = (ListView)this.Controls.Find("listViewProcessors" + index_task.ToString(), true).FirstOrDefault();
                    TextBox textBoxConfiguration = (TextBox)this.Controls.Find("textBoxConfiguration" + index_task.ToString(), true).FirstOrDefault();

                    GenericTask mytask = new GenericTask()
                    {
                        Processor = Procs[mylistviewprocessor.SelectedIndices[0]],
                        ProcessorConfiguration = textBoxConfiguration.Text,
                        InputAsset = listofinputassets[index_task - 1][mycomboboxassetinput.SelectedIndex].InputAsset,
                        InputAssetType = listofinputassets[index_task - 1][mycomboboxassetinput.SelectedIndex].InputAssetType
                    };
                    listtasks.Add(mytask);

                }
                return listtasks;
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

        public TaskJobCreationMode EncodingCreationMode
        {
            get
            {
                if (radioButtonOneJobPerInputAsset.Checked) return TaskJobCreationMode.OneJobPerInputAsset;
                else return TaskJobCreationMode.SingleJobForAllInputAssets;
            }
            set
            {
                switch (value)
                {
                    case TaskJobCreationMode.OneJobPerInputAsset:
                        radioButtonOneJobPerInputAsset.Checked = true;
                        break;


                    case TaskJobCreationMode.SingleJobForAllInputAssets:
                        radioButtonSingleJobForAllInputAssets.Checked = true;
                        break;
                }
            }
        }


        public GenericProcessor(CloudMediaContext context, IJob myJob = null)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            _context = context;
            _myJob = myJob;

            if (_myJob != null) // we are in resubmit mode
            {
                textBoxConfiguration1.Text = _myJob.Tasks.FirstOrDefault().GetClearConfiguration();
                radioButtonSingleJobForAllInputAssets.Checked = true;
                panelJobMode.Enabled = false;
                numericUpDownTasks.Enabled = false;
            }
            init = false;
        }

        private void buttonload_Click(object sender, EventArgs e)
        {
            if (openFileDialogPreset.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    doc = XDocument.Load(openFileDialogPreset.FileName);
                    Button button = (Button)sender;
                    string index = button.Name.Substring(button.Name.Length - 1, 1);
                    TextBox mytextboxconfig = (TextBox)this.Controls.Find("textBoxConfiguration" + index, true).FirstOrDefault();
                    mytextboxconfig.Text = doc.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }


        private void GenericProcessor_Load(object sender, EventArgs e)
        {
            UpdateJobSummary();
            UpdateWarning();
            foreach (var storage in _context.StorageAccounts)
            {
                comboBoxStorage.Items.Add(new Item(string.Format("{0} {1}", storage.Name, storage.IsDefault ? "(default)" : ""), storage.Name));
                if (storage.Name == _context.DefaultStorageAccount.Name) comboBoxStorage.SelectedIndex = comboBoxStorage.Items.Count - 1;
            }
        }

        private void listViewProcessors_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool allprocessorsselected = true;

            for (int index_task = 1; index_task <= numericUpDownTasks.Value; index_task++)
            {
                ListView mylistview = (ListView)this.Controls.Find("listViewProcessors" + index_task.ToString(), true).FirstOrDefault();
                if (mylistview.SelectedItems.Count == 0) allprocessorsselected = false;
            }
            buttonOk.Enabled = allprocessorsselected;
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            UpdateJobSummary();
            UpdateInputAssetsInTasks();
        }

        private void UpdateJobSummary()
        {
            int nbjobs = (radioButtonOneJobPerInputAsset.Checked ? SelectedAssets.Count : 1);

            labelsummaryjob.Text = string.Format("You are going to submit {0} job{1} with {2} task{3}",
                nbjobs,
                nbjobs > 1 ? "s" : "",
                numberoftasks,
                 numberoftasks > 1 ? "s" : ""
                 );

            pictureBoxJob.Image = radioButtonSingleJobForAllInputAssets.Checked ? bitmap_singletasksinglejob : radioButtonOneJobPerInputAsset.Checked ? bitmap_multitasksmultijobs : bitmap_multitasksinglejob;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {

        }

        private void textBoxConfiguration_TextChanged(object sender, EventArgs e)
        {
            UpdateWarning();
        }

        private void UpdateWarning()
        {
            int nbtaskwithemptyconfig = 0;
            for (int index_task = 1; index_task <= numericUpDownTasks.Value; index_task++)
            {
                TextBox mytextboxconfig = (TextBox)this.Controls.Find("textBoxConfiguration" + index_task.ToString(), true).FirstOrDefault();
                if (string.IsNullOrEmpty(mytextboxconfig.Text)) nbtaskwithemptyconfig++;
            }
            if (nbtaskwithemptyconfig > 1)
            {
                labelWarning.Text = "Note: several processors configuration string/XML are empty";
            }
            else if (nbtaskwithemptyconfig > 0)
            {
                labelWarning.Text = "Note: one processor configuration string/XML is empty";
            }
            else
            {
                labelWarning.Text = string.Empty;
            }
        }

        private void GenericProcessor_Shown(object sender, EventArgs e)
        {
            int i = 0;
            listViewInputAssets.BeginUpdate();
            foreach (IAsset asset in SelectedAssets)
            {
                ListViewItem item = new ListViewItem(i.ToString(), 0);
                item.SubItems.Add(asset.Name);
                listViewInputAssets.Items.Add(item);
                i++;
            }
            listViewInputAssets.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            listViewInputAssets.EndUpdate();

            tabcontrolgeneric.TabPages.Remove(tabPageTask2);
            tabcontrolgeneric.TabPages.Remove(tabPageTask3);
            tabcontrolgeneric.TabPages.Remove(tabPageTask4);
            tabcontrolgeneric.TabPages.Remove(tabPageTask5);

            UpdateInputAssetsInTasks();
        }

        private void UpdateInputAssetsInTasks()
        {
            if (init) return; // we don't want to run the code below if the form is in init mode (no input assets defined)

            listofinputassets = new List<List<GenericTaskAsset>>();

            for (int index_task = 1; index_task <= numericUpDownTasks.Value; index_task++)
            {
                List<GenericTaskAsset> listinputpertask = new List<GenericTaskAsset>();
                ComboBox mycombobox = (ComboBox)this.Controls.Find("comboBoxAssetInput" + index_task.ToString(), true).FirstOrDefault();

                mycombobox.Items.Clear();

                // multiple input assets and one job only
                if (SelectedAssets.Count > 1 && radioButtonSingleJobForAllInputAssets.Checked)
                {
                    //Item itemasset = new Item("All input assets", "allinput:");
                    mycombobox.Items.Add("All input assets");
                    listinputpertask.Add(new GenericTaskAsset() { InputAssetType = TypeInputAssetGeneric.InputJobAssets });

                    foreach (IAsset asset in SelectedAssets)
                    {
                        //Item item = new Item("Input asset: " + asset.Name, "input:" + asset.Name);
                        mycombobox.Items.Add("Input asset: " + asset.Name);
                        listinputpertask.Add(new GenericTaskAsset() { InputAssetType = TypeInputAssetGeneric.SpecificAssetID, InputAsset = asset.Id });
                        //index_inputasset++;
                    }
                }
                else // single input asset
                {
                    mycombobox.Items.Add("Input asset");
                    listinputpertask.Add(new GenericTaskAsset() { InputAssetType = TypeInputAssetGeneric.InputJobAssets });

                }

                //let's propose the output asset of other tasks
                for (int index2_task = 1; index2_task <= numericUpDownTasks.Value; index2_task++)
                {
                    if (index_task != index2_task) // because a task cannot a have it's own output asset as input
                    {
                        Item item = new Item("Output asset of task #" + index2_task.ToString(), "outputoftask:" + index2_task.ToString());
                        mycombobox.Items.Add(item);
                        listinputpertask.Add(new GenericTaskAsset() { InputAssetType = TypeInputAssetGeneric.TaskOutputAsset, InputAsset = index2_task.ToString() });
                    }
                }
                listofinputassets.Add(listinputpertask);
                mycombobox.SelectedIndex = 0;
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDownTasks.Value > numberoftasks) // increase
            {
                for (int i = numberoftasks + 1; i <= numericUpDownTasks.Value; i++)
                {
                    TabPage mytabpage = null;
                    switch (i)
                    {
                        case 2:
                            mytabpage = tabPageTask2;
                            break;
                        case 3:
                            mytabpage = tabPageTask3;
                            break;
                        case 4:
                            mytabpage = tabPageTask4;
                            break;
                        case 5:
                            mytabpage = tabPageTask5;
                            break;
                        default:
                            break;

                    }
                    if (mytabpage != null) tabcontrolgeneric.TabPages.Add(mytabpage);
                }
            }
            else // decrease
            {
                for (int i = numberoftasks; i > numericUpDownTasks.Value; i--)
                {
                    TabPage mytabpage = null;

                    switch (i)
                    {
                        case 2:
                            mytabpage = tabPageTask2;
                            break;
                        case 3:
                            mytabpage = tabPageTask3;
                            break;
                        case 4:
                            mytabpage = tabPageTask4;
                            break;
                        case 5:
                            mytabpage = tabPageTask5;
                            break;
                        default:
                            break;

                    }
                    if (mytabpage != null) tabcontrolgeneric.TabPages.Remove(mytabpage);
                }
            }
            numberoftasks = (int)numericUpDownTasks.Value;
            UpdateInputAssetsInTasks();
            UpdateJobSummary();
            UpdateWarning();
        }

    }
}
