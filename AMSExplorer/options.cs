//----------------------------------------------------------------------------------------------
//    Copyright 2019 Microsoft Corporation
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
            Properties.Settings.Default.DisplayAssetAltIDinGrid = checkBoxDisplayAssetAltId.Checked;
            Properties.Settings.Default.DisplayAssetStorageinGrid = checkBoxDisplayAssetStorage.Checked;
            Properties.Settings.Default.DisplayJobIDinGrid = checkBoxDisplayJobID.Checked;
            Properties.Settings.Default.DisplayLiveChannelIDinGrid = checkBoxDisplayChannelID.Checked;
            Properties.Settings.Default.DisplayLiveProgramIDinGrid = checkBoxDisplayProgramID.Checked;
            Properties.Settings.Default.DisplayOriginIDinGrid = checkBoxDisplayOriginID.Checked;
            Properties.Settings.Default.AutoRefresh = checkBoxAutoRefresh.Checked;
            Properties.Settings.Default.AutoRefreshTime = Convert.ToInt16(comboBoxAutoRefreshTime.SelectedItem);

            Properties.Settings.Default.OutputAssetsAdaptiveStreamingFormat = checkBoxUseAdaptiveStreamingFormat.Checked;

            Properties.Settings.Default.AssetAnalysisStart = (int)numericUpDownAssetAnalysisStart.Value;
            Properties.Settings.Default.AssetAnalysisStep = (int)numericUpDownAssetAnalysisStep.Value;

            Properties.Settings.Default.CustomPlayerUrl = textBoxCustomPlayer.Text;
            Properties.Settings.Default.CustomPlayerEnabled = checkBoxEnableCustomPlayer.Checked;

            Properties.Settings.Default.DefaultJobPriority = (int)numericUpDownPriority.Value;
            Properties.Settings.Default.DefaultLocatorDurationDaysNew = (int)numericUpDownLocatorDuration.Value;
            Properties.Settings.Default.DefaultTokenDuration = (int)numericUpDownTokenDuration.Value;
            Properties.Settings.Default.HideTaskbarNotifications = checkBoxHideTaskbarNotifications.Checked;
            Properties.Settings.Default.MESPricePerMin = numericUpDownMESPrice.Value;
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
            checkBoxDisplayJobID.Checked = false;
            checkBoxDisplayChannelID.Checked = false;
            checkBoxDisplayOriginID.Checked = false;
            checkBoxDisplayProgramID.Checked = false;
            checkBoxAutoRefresh.Checked = false;
            comboBoxAutoRefreshTime.SelectedItem = "60";

            checkBoxUseAdaptiveStreamingFormat.Checked = false;
            checkBoxHideTaskbarNotifications.Checked = false;

            textBoxCustomPlayer.Text = string.Format(Constants.PlayerAMPinOptions, Constants.NameconvManifestURL);
            checkBoxEnableCustomPlayer.Checked = false;

            numericUpDownPriority.Value = 10;
            textBoxCurrency.Text = "$";
            numericUpDownLocatorDuration.Value = 3650;
            numericUpDownTokenDuration.Value = 60;
            numericUpDownMESPrice.Value = ((decimal)0.015);

            numericUpDownAssetAnalysisStart.Value = 10;
            numericUpDownAssetAnalysisStep.Value = 20;

            textBoxffmpegPath.Text = @"%programfiles32%\ffmpeg\bin";
            textBoxVLCPath.Text = @"%programfiles32%\VideoLAN\VLC";

            Properties.Settings.Default.WAMEPresetXMLFilesCurrentFolder = Application.StartupPath + Constants.PathAMEFiles; // we reset the XML files folders
            Properties.Settings.Default.MESPresetFilesCurrentFolder = Application.StartupPath + Constants.PathMESFiles; // we reset the XML files folders
            Properties.Settings.Default.PremiumWorkflowPresetXMLFilesCurrentFolder = Application.StartupPath + Constants.PathPremiumWorkflowFiles;
            Properties.Settings.Default.DefaultSlateCurrentFolder = Application.StartupPath + Constants.PathDefaultSlateJPG;

            Properties.Settings.Default.DynEncTokenIssuer = "http://testacs";
            Properties.Settings.Default.DynEncTokenAudience = "urn:test";

            Program.SaveAndProtectUserConfig();
        }

        private void options_Load(object sender, EventArgs e)
        {
            checkBoxDisplayAssetID.Checked = Properties.Settings.Default.DisplayAssetIDinGrid;
            checkBoxDisplayAssetAltId.Checked = Properties.Settings.Default.DisplayAssetAltIDinGrid;
            checkBoxDisplayAssetStorage.Checked = Properties.Settings.Default.DisplayAssetStorageinGrid;
            checkBoxDisplayJobID.Checked = Properties.Settings.Default.DisplayJobIDinGrid;
            checkBoxDisplayChannelID.Checked = Properties.Settings.Default.DisplayLiveChannelIDinGrid;
            checkBoxDisplayProgramID.Checked = Properties.Settings.Default.DisplayLiveProgramIDinGrid;
            checkBoxDisplayOriginID.Checked = Properties.Settings.Default.DisplayOriginIDinGrid;
            checkBoxAutoRefresh.Checked = Properties.Settings.Default.AutoRefresh;
            comboBoxAutoRefreshTime.SelectedItem = Properties.Settings.Default.AutoRefreshTime.ToString();

            checkBoxUseAdaptiveStreamingFormat.Checked = Properties.Settings.Default.OutputAssetsAdaptiveStreamingFormat;

            textBoxCustomPlayer.Text = Properties.Settings.Default.CustomPlayerUrl;
            checkBoxEnableCustomPlayer.Checked = Properties.Settings.Default.CustomPlayerEnabled;
            textBoxCustomPlayer.Enabled = checkBoxEnableCustomPlayer.Checked;

            numericUpDownPriority.Value = Properties.Settings.Default.DefaultJobPriority;
            numericUpDownLocatorDuration.Value = Properties.Settings.Default.DefaultLocatorDurationDaysNew;
            numericUpDownTokenDuration.Value = Properties.Settings.Default.DefaultTokenDuration;
            checkBoxHideTaskbarNotifications.Checked = Properties.Settings.Default.HideTaskbarNotifications;

            numericUpDownAssetAnalysisStart.Value = Properties.Settings.Default.AssetAnalysisStart;
            numericUpDownAssetAnalysisStep.Value = Properties.Settings.Default.AssetAnalysisStep;

            textBoxCurrency.Text = Properties.Settings.Default.Currency;
            numericUpDownMESPrice.Value = Properties.Settings.Default.MESPricePerMin;

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
