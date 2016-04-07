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
            Properties.Settings.Default.DisplayAssetAltIDinGrid= checkBoxDisplayAssetAltId.Checked;
            Properties.Settings.Default.DisplayAssetStorageinGrid = checkBoxDisplayAssetStorage.Checked;
            Properties.Settings.Default.DisplayIngestManifestIDinGrid = checkBoxDisplayBulkContId.Checked;
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
            Properties.Settings.Default.DefaultLocatorDurationDaysNew = (int)numericUpDownLocatorDuration.Value;
            Properties.Settings.Default.DefaultTokenDuration = (int)numericUpDownTokenDuration.Value;
            Properties.Settings.Default.AMEPrice = numericUpDownMESPrice.Value;
            Properties.Settings.Default.MEPremiumWorkflowPrice = numericUpDownPremiumWorkflowPrice.Value;
            Properties.Settings.Default.IndexingPricePerMin = numericUpDownIndexingPrice.Value;
            Properties.Settings.Default.Currency = textBoxCurrency.Text;

            Properties.Settings.Default.ffmpegPath = textBoxffmpegPath.Text;
            Properties.Settings.Default.VLCPath = textBoxVLCPath.Text;


            Program.SaveAndProtectUserConfig();
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            checkBoxDisplayAssetID.Checked = false;
            checkBoxDisplayAssetAltId.Checked = false;
            checkBoxDisplayAssetStorage.Checked = false;
            checkBoxDisplayBulkContId.Checked = false;
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

            textBoxCustomPlayer.Text = string.Format(Constants.PlayerAMPinOptions, Constants.NameconvManifestURL);
            checkBoxEnableCustomPlayer.Checked = false;

            numericUpDownPriority.Value = 10;
            textBoxCurrency.Text = "$";
            numericUpDownLocatorDuration.Value = 3650;
            numericUpDownTokenDuration.Value = 60;
            numericUpDownMESPrice.Value = ((decimal)1.99);
            numericUpDownPremiumWorkflowPrice.Value = ((decimal)3.99);
            numericUpDownIndexingPrice.Value = ((decimal)0.05);

            textBoxffmpegPath.Text = @"%programfiles32%\ffmpeg\bin";
            textBoxVLCPath.Text = @"%programfiles32%\VideoLAN\VLC";

            Properties.Settings.Default.WAMEPresetXMLFilesCurrentFolder = Application.StartupPath + Constants.PathAMEFiles; // we reset the XML files folders
            Properties.Settings.Default.AMEStandardPresetXMLFilesCurrentFolder = Application.StartupPath + Constants.PathAMEStdFiles; // we reset the XML files folders
            Properties.Settings.Default.PremiumWorkflowPresetXMLFilesCurrentFolder = Application.StartupPath + Constants.PathPremiumWorkflowFiles;
            Properties.Settings.Default.DefaultSlateCurrentFolder = Application.StartupPath + Constants.PathDefaultSlateJPG;

            Properties.Settings.Default.DynEncTokenIssuer = "http://testacs";
            Properties.Settings.Default.DynEncTokenAudience = "urn:test";

            Program.SaveAndProtectUserConfig();
        }

        private void options_Load(object sender, EventArgs e)
        {
            comboBoxNbItems.Items.AddRange(new string[] { "25", "50", "75", "100", "150" });
            int indexc = comboBoxNbItems.Items.IndexOf(Properties.Settings.Default.NbItemsDisplayedInGrid.ToString());
            if (indexc == -1) indexc = 1; // not found!
            comboBoxNbItems.SelectedIndex = indexc;

            checkBoxDisplayAssetID.Checked = Properties.Settings.Default.DisplayAssetIDinGrid;
            checkBoxDisplayAssetAltId.Checked = Properties.Settings.Default.DisplayAssetAltIDinGrid;
            checkBoxDisplayAssetStorage.Checked = Properties.Settings.Default.DisplayAssetStorageinGrid;
            checkBoxDisplayBulkContId.Checked = Properties.Settings.Default.DisplayIngestManifestIDinGrid;
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
            numericUpDownLocatorDuration.Value = Properties.Settings.Default.DefaultLocatorDurationDaysNew;
            numericUpDownTokenDuration.Value = Properties.Settings.Default.DefaultTokenDuration;

            textBoxCurrency.Text = Properties.Settings.Default.Currency;
            numericUpDownMESPrice.Value = Properties.Settings.Default.AMEPrice;
            numericUpDownPremiumWorkflowPrice.Value = Properties.Settings.Default.MEPremiumWorkflowPrice;
            numericUpDownIndexingPrice.Value = Properties.Settings.Default.IndexingPricePerMin;

            textBoxffmpegPath.Text = Properties.Settings.Default.ffmpegPath;
            textBoxVLCPath.Text = Properties.Settings.Default.VLCPath;

            amspriceslink.Links.Add(new LinkLabel.Link(0, amspriceslink.Text.Length, "http://azure.microsoft.com/en-us/pricing/details/media-services/"));

        }

        private void checkBoxEnableCustomPlayer_CheckedChanged(object sender, EventArgs e)
        {
            textBoxCustomPlayer.Enabled = checkBoxEnableCustomPlayer.Checked;
        }

        private void amspriceslink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData as string);
        }
    }
}
