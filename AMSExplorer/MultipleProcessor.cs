﻿//----------------------------------------------------------------------------------------------
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
using System.Xml;
using System.Xml.Linq;
using Microsoft.WindowsAzure.MediaServices.Client;
using System.Reflection;

namespace AMSExplorer
{
    public partial class MultipleProcessor : Form
    {
        public XDocument doc;
        public List<IAsset> SelectedAssets;
        public IEnumerable<IAsset> VisibleAssets;

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

        public JobOptionsVar SingleTaskOptions
        {
            get
            {
                return buttonTaskOptions1.GetSettings();
            }
            set
            {
                buttonTaskOptions1.SetSettings(value);
                buttonTaskOptions2.SetSettings(value);
                buttonTaskOptions3.SetSettings(value);
                buttonTaskOptions4.SetSettings(value);
                buttonTaskOptions5.SetSettings(value);
            }
        }

        public int JobPriority
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

        public List<IMediaProcessor> EncodingProcessorsList
        {
            set
            {
                for (int i = 1; i < 6; i++)
                {
                    ListView mylistview = ReturnListViewProcessor(i);
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

        public IMediaProcessor SingleEncodingProcessorSelected
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


        public string SingleEncodingConfiguration
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
                    ComboBox mycomboboxassetinput = ReturnComboBoxAssetInput(index_task);

                    GenericTask mytask = new GenericTask()
                    {
                        Processor = Procs[ReturnListViewProcessor(index_task).SelectedIndices[0]],
                        ProcessorConfiguration = ReturnTextBoxConfiguration(index_task).Text,
                        InputAsset = listofinputassets[index_task - 1][mycomboboxassetinput.SelectedIndex].InputAsset,
                        InputAssetType = listofinputassets[index_task - 1][mycomboboxassetinput.SelectedIndex].InputAssetType,
                        TaskOptions = ReturnTaskConfiguration(index_task).GetSettings()
                    };
                    listtasks.Add(mytask);
                }
                return listtasks;
            }
        }

        private ListView ReturnListViewProcessor(int index_task)
        {
            return (ListView)this.Controls.Find("listViewProcessors" + index_task.ToString(), true).FirstOrDefault();
        }

        private ComboBox ReturnComboBoxAssetInput(int index_task)
        {
            return (ComboBox)this.Controls.Find("comboBoxAssetInput" + index_task.ToString(), true).FirstOrDefault();
        }

        private TextBox ReturnTextBoxConfiguration(int index_task)
        {
            return (TextBox)this.Controls.Find("textBoxConfiguration" + index_task.ToString(), true).FirstOrDefault();
        }

        private ButtonJobOptions ReturnTaskConfiguration(int index_task)
        {
            return (ButtonJobOptions)this.Controls.Find("buttonTaskOptions" + index_task.ToString(), true).FirstOrDefault();
        }


        public TaskJobCreationMode EncodingCreationMode
        {
            get
            {
                if (radioButtonOneJobPerSelectedAsset.Checked)
                {
                    return TaskJobCreationMode.OneJobPerInputAsset;
                }
                else if (radioButtonOneJobPerVisibleAsset.Checked)
                {
                    return TaskJobCreationMode.OneJobPerVisibleAsset;
                }
                else
                {
                    return TaskJobCreationMode.SingleJobForAllInputAssets;
                }
            }
            set
            {
                switch (value)
                {
                    case TaskJobCreationMode.OneJobPerInputAsset:
                        radioButtonOneJobPerSelectedAsset.Checked = true;
                        break;

                    case TaskJobCreationMode.SingleJobForAllInputAssets:
                        radioButtonSingleJobForAllInputAssets.Checked = true;
                        break;
                }
            }
        }


        public MultipleProcessor(CloudMediaContext context, IJob myJob = null)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            _context = context;
            _myJob = myJob;
            buttonTaskOptions1.Initialize(_context, true);
            buttonTaskOptions2.Initialize(_context, true);
            buttonTaskOptions3.Initialize(_context, true);
            buttonTaskOptions4.Initialize(_context, true);
            buttonTaskOptions5.Initialize(_context, true);
            buttonTaskOptions1.Click += ButtonTaskOptions_Click;
            buttonTaskOptions2.Click += ButtonTaskOptions_Click;
            buttonTaskOptions3.Click += ButtonTaskOptions_Click;
            buttonTaskOptions4.Click += ButtonTaskOptions_Click;
            buttonTaskOptions5.Click += ButtonTaskOptions_Click;

            if (_myJob != null) // we are in resubmit mode
            {
                textBoxConfiguration1.Text = _myJob.Tasks.FirstOrDefault().GetClearConfiguration();
                radioButtonSingleJobForAllInputAssets.Checked = true;
                panelJobMode.Enabled = false;
                numericUpDownTasks.Enabled = false;
                SelectedAssets = myJob.InputMediaAssets.ToList();
            }

            labelWarningJSON1.Text = labelWarningJSON2.Text = labelWarningJSON3.Text = labelWarningJSON4.Text = labelWarningJSON5.Text = string.Empty;

            init = false;
        }

        private void ButtonTaskOptions_Click(object sender, EventArgs e)
        {
            UpdateGeneralWarning();
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
                    TextBox mytextboxconfig = ReturnTextBoxConfiguration(Convert.ToInt16(index));
                    mytextboxconfig.Text = doc.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
                UpdateGeneralWarning();
            }
        }


        private void GenericProcessor_Load(object sender, EventArgs e)
        {
            UpdateJobSummary();
            UpdateGeneralWarning();
        }

        private void listViewProcessors_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (init) return; // we don't want to run the code below if the form is in init mode (no input assets defined)

            bool allprocessorsselected = true;

            for (int index_task = 1; index_task <= numericUpDownTasks.Value; index_task++)
            {
                ListView mylistview = ReturnListViewProcessor(index_task);
                TextBox mytextboxconfig = ReturnTextBoxConfiguration(index_task);

                if (mylistview.SelectedItems.Count == 0)
                {
                    allprocessorsselected = false;
                }
                else
                {
                    if (Procs[mylistview.SelectedIndices[0]].Name == Constants.AzureMediaEncoderPremiumWorkflow)
                    {
                        if (SelectedAssets.Count < 2)
                        {
                            MessageBox.Show("You must at least have two input assets : the workflow file and the video file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    else
                    {
                        mytextboxconfig.Enabled = true;
                    }
                }
            }
            buttonOk.Enabled = allprocessorsselected;
            UpdateGeneralWarning();
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            UpdateJobSummary();
            UpdateInputAssetsInTasks();
        }

        private void UpdateJobSummary()
        {
            int nbjobs = 1;

            if (radioButtonOneJobPerSelectedAsset.Checked)
            {
                nbjobs = SelectedAssets.Count;
            }
            else if (radioButtonOneJobPerVisibleAsset.Checked)
            {
                nbjobs = VisibleAssets.Count();
            }

            labelsummaryjob.Text = string.Format(labelsummaryjob.Tag as string,
                nbjobs,
                nbjobs > 1 ? "s" : "",
                numberoftasks,
                 numberoftasks > 1 ? "s" : ""
                 );

            if (radioButtonSingleJobForAllInputAssets.Checked)
            {
                pictureBoxJob.Image = bitmap_singletasksinglejob;
            }
            else
            {
                pictureBoxJob.Image = bitmap_multitasksmultijobs;
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {

        }


        private void UpdateGeneralWarning()
        {
            if (init) return; // we don't want to run the code below if the form is in init mode

            int nbtaskwithemptyconfig = 0;
            int nbDecryptTaskWithOutputStoragEncrypted = 0;

            for (int index_task = 1; index_task <= numericUpDownTasks.Value; index_task++)
            {
                TextBox mytextboxconfig = ReturnTextBoxConfiguration(index_task);
                var processorcontrol = ReturnListViewProcessor(index_task);

                string processorname = processorcontrol.SelectedIndices.Count > 0 ? Procs[processorcontrol.SelectedIndices[0]].Name : string.Empty;
                if (string.IsNullOrEmpty(mytextboxconfig.Text) && processorname != Constants.AzureMediaEncoderPremiumWorkflow && processorname != MediaProcessorNames.StorageDecryption)
                {
                    nbtaskwithemptyconfig++;
                }
                if (ReturnTaskConfiguration(index_task).GetSettings().OutputAssetsCreationOptions == AssetCreationOptions.StorageEncrypted && processorname == MediaProcessorNames.StorageDecryption)
                {
                    nbDecryptTaskWithOutputStoragEncrypted++;
                }
            }
            string separator = " | ";
            if (nbtaskwithemptyconfig > 1)
            {
                labelWarning.Text = "Several processor configuration are empty";
            }
            else if (nbtaskwithemptyconfig > 0)
            {
                labelWarning.Text = "One processor configuration is empty";
            }
            else
            {
                labelWarning.Text = string.Empty;
                separator = "";
            }

            if (nbDecryptTaskWithOutputStoragEncrypted > 0)
            {
                labelWarning.Text += separator + "Decryptor task(s) setting error: output asset should be decrypted";
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
                ComboBox mycombobox = ReturnComboBoxAssetInput(index_task);

                mycombobox.Items.Clear();

                // multiple input assets and one job only
                if (SelectedAssets.Count > 1 && radioButtonSingleJobForAllInputAssets.Checked)
                {
                    mycombobox.Items.Add("All input assets");
                    listinputpertask.Add(new GenericTaskAsset() { InputAssetType = TypeInputAssetGeneric.InputJobAssets });

                    foreach (IAsset asset in SelectedAssets)
                    {
                        mycombobox.Items.Add("Input asset: " + asset.Name);
                        listinputpertask.Add(new GenericTaskAsset() { InputAssetType = TypeInputAssetGeneric.SpecificAssetID, InputAsset = asset.Id });
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
            UpdateGeneralWarning();
        }

        private void textBoxConfiguration1_TextChanged(object sender, EventArgs e)
        {
            labelWarningJSON1.Text = Program.AnalyzeTextAndReportSyntaxError(((TextBox)sender).Text);
            UpdateGeneralWarning();
        }

        private void textBoxConfiguration2_TextChanged(object sender, EventArgs e)
        {
            labelWarningJSON2.Text = Program.AnalyzeTextAndReportSyntaxError(((TextBox)sender).Text);
            UpdateGeneralWarning();
        }

        private void textBoxConfiguration3_TextChanged(object sender, EventArgs e)
        {
            labelWarningJSON3.Text = Program.AnalyzeTextAndReportSyntaxError(((TextBox)sender).Text);
            UpdateGeneralWarning();
        }

        private void textBoxConfiguration4_TextChanged(object sender, EventArgs e)
        {
            labelWarningJSON4.Text = Program.AnalyzeTextAndReportSyntaxError(((TextBox)sender).Text);
            UpdateGeneralWarning();
        }

        private void textBoxConfiguration5_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
