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
        CloudMediaContext _context;
        private IMonitoringConfiguration _monitorconfig;

        public ConfigTelemetryVar Config
        {
            get
            {
                return new ConfigTelemetryVar()
                {
                    StorageSelected = comboBoxStorage.SelectedItem != null ? ((Item)comboBoxStorage.SelectedItem).Value : null,
                    MonitorChannel = new MonitorComponent() { Monitored = checkBoxChannels.Checked, MonitoringLevelSetting = (MonitoringLevel)(Enum.Parse(typeof(MonitoringLevel), (string)comboBoxLevelChannel.SelectedItem)) },
                    MonitorStreamingEndpoint = new MonitorComponent() { Monitored = checkBoxSEs.Checked, MonitoringLevelSetting = (MonitoringLevel)(Enum.Parse(typeof(MonitoringLevel), (string)comboBoxLevelSE.SelectedItem)) }

                };
            }

        }


        public ConfigureTelemetry(CloudMediaContext myContext, IMonitoringConfiguration monitorconfig)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;

            _context = myContext;
            _monitorconfig = monitorconfig;

            PrepareControls();

        }

        private void PrepareControls()
        {
            comboBoxLevelChannel.Items.Clear();
            comboBoxLevelSE.Items.Clear();

            comboBoxLevelChannel.Items.AddRange(Enum.GetNames(typeof(MonitoringLevel)));
            comboBoxLevelSE.Items.AddRange(Enum.GetNames(typeof(MonitoringLevel)));

            if (_monitorconfig == null) // new telemetry config
            {
                comboBoxStorage.Items.Clear();
                foreach (var storage in _context.StorageAccounts)
                {
                    comboBoxStorage.Items.Add(new Item(string.Format("{0} {1}", storage.Name, storage.IsDefault ? "(default)" : ""), storage.Name));
                    if (storage.Name == _context.DefaultStorageAccount.Name) comboBoxStorage.SelectedIndex = comboBoxStorage.Items.Count - 1;
                }

                comboBoxLevelChannel.Text = comboBoxLevelSE.Text = MonitoringLevel.Normal.ToString();
            }
            else // current telemetry config is displayed
            {
                var currentConfig = _context.NotificationEndPoints.Where(n => n.Id == _monitorconfig.NotificationEndPointId).FirstOrDefault();
                textBoxTableURL.Text = currentConfig.EndPointAddress;
                textBoxTableURL.Visible = true;
                comboBoxStorage.Visible = false;
                buttonDeleteConfig.Visible = true;
                buttonOk.Text = "Update";
                labelTelemetryUI.Text = "Current Telemetry Settings";

                checkBoxChannels.Checked = _monitorconfig.Settings.ToList().Any(s => s.Component == MonitoringComponent.Channel);
                if (checkBoxChannels.Checked)
                {
                    var level = _monitorconfig.Settings.ToList().Where(s => s.Component == MonitoringComponent.Channel).FirstOrDefault().Level;
                    comboBoxLevelChannel.Text = level.ToString();
                }
                checkBoxSEs.Checked = _monitorconfig.Settings.ToList().Any(s => s.Component == MonitoringComponent.StreamingEndpoint);
                if (checkBoxSEs.Checked)
                {
                    var level = _monitorconfig.Settings.ToList().Where(s => s.Component == MonitoringComponent.StreamingEndpoint).FirstOrDefault().Level;
                    comboBoxLevelSE.Text = level.ToString();
                }
            }
        }


        private void ConfigureTelemetry_Load(object sender, EventArgs e)
        {

        }

    }

}
