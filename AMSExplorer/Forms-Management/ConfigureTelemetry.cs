//----------------------------------------------------------------------------------------------
//    Copyright 2016 Microsoft Corporation
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


namespace AMSExplorer
{
    public partial class ConfigureTelemetry : Form
    {
        CloudMediaContext context;
     

        public ConfigTelemetryVar Config
        {
            get
            {
                return new ConfigTelemetryVar()
                {
                    StorageSelected = ((Item)comboBoxStorage.SelectedItem).Value,
                    MonitorChannel = new MonitorComponent() { Monitored = checkBoxChannels.Checked, MonitoringLevelSetting = (MonitoringLevel)(Enum.Parse(typeof(MonitoringLevel), (string)(comboBoxLevelChannel.SelectedItem as Item).Value)) },
                    MonitorStreamingEndpoint = new MonitorComponent() { Monitored = checkBoxSEs.Checked, MonitoringLevelSetting = (MonitoringLevel)(Enum.Parse(typeof(MonitoringLevel), (string)(comboBoxLevelSE.SelectedItem as Item).Value)) }
                };
            }
           
        }


        public ConfigureTelemetry(CloudMediaContext myContext)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;

            context = myContext;
          
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

            comboBoxLevelChannel.Items.Clear();
            comboBoxLevelSE.Items.Clear();

            comboBoxLevelChannel.Items.Add(new Item("Normal", Enum.GetName(typeof(MonitoringLevel), MonitoringLevel.Normal)));
            comboBoxLevelChannel.Items.Add(new Item("Verbose", Enum.GetName(typeof(MonitoringLevel), MonitoringLevel.Verbose)));
            comboBoxLevelChannel.SelectedIndex = 0;
            comboBoxLevelSE.Items.Add(new Item("Normal", Enum.GetName(typeof(MonitoringLevel), MonitoringLevel.Normal)));
            comboBoxLevelSE.Items.Add(new Item("Verbose", Enum.GetName(typeof(MonitoringLevel), MonitoringLevel.Verbose)));
            comboBoxLevelSE.SelectedIndex = 0;
        }


        private void ConfigureTelemetry_Load(object sender, EventArgs e)
        {
           
        }
     
    }
   
}
