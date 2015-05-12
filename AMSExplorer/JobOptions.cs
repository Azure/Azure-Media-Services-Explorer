//----------------------------------------------------------------------- 
// <copyright file="JobOptions.cs" company="Microsoft">Copyright (c) Microsoft Corporation. All rights reserved.</copyright> 
// <license>
// Azure Media Services Explorer Ver. 3.2
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
    public partial class JobOptions : Form
    {
        CloudMediaContext context;
        JobOptionsVar defaultSettings = new JobOptionsVar()
        {
            Priority = Properties.Settings.Default.DefaultJobPriority,
            StorageSelected = string.Empty,
            TasksOptionsSetting = Properties.Settings.Default.useProtectedConfiguration ? TaskOptions.ProtectedConfiguration : TaskOptions.None,
            OutputAssetsCreationOptions = Properties.Settings.Default.useStorageEncryption ? AssetCreationOptions.StorageEncrypted : AssetCreationOptions.None
        };
        JobOptionsVar savedSettings;

        public JobOptionsVar Options
        {
            get
            {
                return new JobOptionsVar()
                {
                    StorageSelected = ((Item)comboBoxStorage.SelectedItem).Value,
                    Priority = (int)numericUpDownPriority.Value,
                    TasksOptionsSetting = checkBoxUseProtectedConfig.Checked ? TaskOptions.ProtectedConfiguration : TaskOptions.None,
                    OutputAssetsCreationOptions = checkBoxUseStorageEncryption.Checked ? AssetCreationOptions.StorageEncrypted : AssetCreationOptions.None
                };
            }
            set
            {
                defaultSettings = value;
                savedSettings = value;
                ControlsResetToDefault();
            }
        }


        public JobOptions(CloudMediaContext myContext)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;

            context = myContext;
            savedSettings = defaultSettings;

            ControlsResetToDefault();

        }

        private void ControlsResetToDefault()
        {
            comboBoxStorage.Items.Clear();
            foreach (var storage in context.StorageAccounts)
            {
                comboBoxStorage.Items.Add(new Item(string.Format("{0} {1}", storage.Name, storage.IsDefault ? "(default)" : ""), storage.Name));
                if (storage.Name == context.DefaultStorageAccount.Name) comboBoxStorage.SelectedIndex = comboBoxStorage.Items.Count - 1;
            }

            numericUpDownPriority.Value = defaultSettings.Priority;
            checkBoxUseProtectedConfig.Checked = defaultSettings.TasksOptionsSetting == TaskOptions.ProtectedConfiguration;
            checkBoxUseStorageEncryption.Checked = defaultSettings.OutputAssetsCreationOptions == AssetCreationOptions.StorageEncrypted;
        }


        private void JobOptions_Load(object sender, EventArgs e)
        {

        }

        public DialogResult Display()
        {
            DialogResult DR = this.ShowDialog();
            if (DR == DialogResult.OK)
            {
                savedSettings.StorageSelected = ((Item)comboBoxStorage.SelectedItem).Value;
                savedSettings.Priority = (int)numericUpDownPriority.Value;
                savedSettings.TasksOptionsSetting = checkBoxUseProtectedConfig.Checked ? TaskOptions.ProtectedConfiguration : TaskOptions.None;
                savedSettings.OutputAssetsCreationOptions = checkBoxUseStorageEncryption.Checked ? AssetCreationOptions.StorageEncrypted : AssetCreationOptions.None;
            }
            else // let's reset the controls to default
            {
                ControlsResetToDefault();
            }
            return DR;
        }
    }

    class ButtonJobOptions : Button
    {
        JobOptions myjoboptions;

        public ButtonJobOptions()
        {
            this.Click += ButtonJobOptions_Click;
        }


        public void Initialize(CloudMediaContext mycontext)
        {
            myjoboptions = new JobOptions(mycontext);
        }

        void ButtonJobOptions_Click(object sender, EventArgs e)
        {
            myjoboptions.Display();
        }

        public JobOptionsVar GetSettings()
        {
            return myjoboptions.Options;
        }

        public void SetSettings(JobOptionsVar settings)
        {
            myjoboptions.Options = settings;
        }

    }
}
