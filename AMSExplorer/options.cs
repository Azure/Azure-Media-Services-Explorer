//----------------------------------------------------------------------- 
// <copyright file="options.cs" company="Microsoft">Copyright (c) Microsoft Corporation. All rights reserved.</copyright> 
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

namespace AMSExplorer
{
    public partial class Options : Form
    {
        public Options()
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.DisplayAssetIDinGrid = checkBoxDisplayAssetID.Checked;
            Properties.Settings.Default.DisplayAssetStorageinGrid = checkBoxDisplayAssetStorage.Checked;
            Properties.Settings.Default.DisplayJobIDinGrid = checkBoxDisplayJobID.Checked;
            Properties.Settings.Default.DisplayLiveChannelIDinGrid = checkBoxDisplayChannelID.Checked;
            Properties.Settings.Default.DisplayLiveProgramIDinGrid = checkBoxDisplayProgramID.Checked;
            Properties.Settings.Default.DisplayOriginIDinGrid = checkBoxDisplayOriginID.Checked;
            Properties.Settings.Default.AutoRefresh = checkBoxAutoRefresh.Checked;
            Properties.Settings.Default.AutoRefreshTime = Convert.ToInt16(comboBoxAutoRefreshTime.SelectedItem);

            Properties.Settings.Default.useProtectedConfiguration = checkBoxUseProtectedConfig.Checked;
            Properties.Settings.Default.useStorageEncryption = checkBoxUseStorageEncryption.Checked;
            Properties.Settings.Default.useTransferQueue = checkBoxOneUpDownload.Checked;
            Properties.Settings.Default.NbItemsDisplayedInGrid = Convert.ToInt16(comboBoxNbItems.SelectedItem.ToString());

            Properties.Settings.Default.CustomPlayerUrl = textBoxCustomPlayer.Text;
            Properties.Settings.Default.CustomPlayerEnabled = checkBoxEnableCustomPlayer.Checked;

            Properties.Settings.Default.DefaultJobPriority = (int)numericUpDownPriority.Value;
            Properties.Settings.Default.DefaultLocatorDurationDays = (int)numericUpDownLocatorDuration.Value;

            Properties.Settings.Default.Save();
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            checkBoxDisplayAssetID.Checked = false;
            checkBoxDisplayAssetStorage.Checked = false;
            checkBoxDisplayJobID.Checked = false;
            checkBoxDisplayChannelID.Checked = false;
            checkBoxDisplayOriginID.Checked = false;
            checkBoxDisplayProgramID.Checked = false;
            checkBoxAutoRefresh.Checked = false;
            comboBoxAutoRefreshTime.SelectedItem = "60";

            checkBoxUseProtectedConfig.Checked = false;
            checkBoxUseStorageEncryption.Checked = false;
            checkBoxOneUpDownload.Checked = true;

            int indexc = comboBoxNbItems.Items.IndexOf("50");
            if (indexc == -1) indexc = 1; // not found!
            comboBoxNbItems.SelectedIndex = indexc;

            textBoxCustomPlayer.Text = Constants.AMSPlayer + Constants.NameconvManifestURL;
            checkBoxEnableCustomPlayer.Checked = false;

            numericUpDownPriority.Value = 10;
            numericUpDownLocatorDuration.Value = 365;

            Properties.Settings.Default.WAMEPresetXMLFilesCurrentFolder = Application.StartupPath + Constants.PathAMEFiles; // we reset the XML files folders
            Properties.Settings.Default.PremiumWorkflowPresetXMLFilesCurrentFolder = Application.StartupPath + Constants.PathPremiumWorkflowFiles;
            Properties.Settings.Default.Save();
        }

        private void options_Load(object sender, EventArgs e)
        {
            comboBoxNbItems.Items.AddRange(new string[] { "25", "50", "75", "100" });
            int indexc = comboBoxNbItems.Items.IndexOf(Properties.Settings.Default.NbItemsDisplayedInGrid.ToString());
            if (indexc == -1) indexc = 1; // not found!
            comboBoxNbItems.SelectedIndex = indexc;

            checkBoxDisplayAssetID.Checked = Properties.Settings.Default.DisplayAssetIDinGrid;
            checkBoxDisplayAssetStorage.Checked = Properties.Settings.Default.DisplayAssetStorageinGrid;
            checkBoxDisplayJobID.Checked = Properties.Settings.Default.DisplayJobIDinGrid;
            checkBoxDisplayChannelID.Checked = Properties.Settings.Default.DisplayLiveChannelIDinGrid;
            checkBoxDisplayProgramID.Checked = Properties.Settings.Default.DisplayLiveProgramIDinGrid;
            checkBoxDisplayOriginID.Checked = Properties.Settings.Default.DisplayOriginIDinGrid;
            checkBoxAutoRefresh.Checked = Properties.Settings.Default.AutoRefresh;
            comboBoxAutoRefreshTime.SelectedItem = Properties.Settings.Default.AutoRefreshTime.ToString();

            checkBoxUseProtectedConfig.Checked = Properties.Settings.Default.useProtectedConfiguration;
            checkBoxUseStorageEncryption.Checked = Properties.Settings.Default.useStorageEncryption;
            checkBoxOneUpDownload.Checked = Properties.Settings.Default.useTransferQueue;

            textBoxCustomPlayer.Text = Properties.Settings.Default.CustomPlayerUrl;
            checkBoxEnableCustomPlayer.Checked = Properties.Settings.Default.CustomPlayerEnabled;
            textBoxCustomPlayer.Enabled = checkBoxEnableCustomPlayer.Checked;

            numericUpDownPriority.Value = Properties.Settings.Default.DefaultJobPriority;
            numericUpDownLocatorDuration.Value = Properties.Settings.Default.DefaultLocatorDurationDays;
        }

        private void checkBoxEnableCustomPlayer_CheckedChanged(object sender, EventArgs e)
        {
            textBoxCustomPlayer.Enabled = checkBoxEnableCustomPlayer.Checked;
        }
    }
}
