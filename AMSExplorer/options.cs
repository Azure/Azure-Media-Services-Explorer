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
using System.Diagnostics;
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
            Properties.Settings.Default.DisplayAssetAltIDinGrid = checkBoxDisplayAssetAltId.Checked;
            Properties.Settings.Default.DisplayAssetStorageinGrid = checkBoxDisplayAssetStorage.Checked;
            Properties.Settings.Default.AutoRefresh = checkBoxAutoRefresh.Checked;
            Properties.Settings.Default.AutoRefreshTime = Convert.ToInt16(comboBoxAutoRefreshTime.SelectedItem);

            Properties.Settings.Default.OutputAssetsAdaptiveStreamingFormat = checkBoxUseAdaptiveStreamingFormat.Checked;

            Properties.Settings.Default.AssetAnalysisStart = (int)numericUpDownAssetAnalysisStart.Value;
            Properties.Settings.Default.AssetAnalysisStep = (int)numericUpDownAssetAnalysisStep.Value;

            Properties.Settings.Default.CustomPlayerUrl = textBoxCustomPlayer.Text;
            Properties.Settings.Default.CustomPlayerEnabled = checkBoxEnableCustomPlayer.Checked;

            Properties.Settings.Default.DefaultLocatorDurationDaysNew = (int)numericUpDownLocatorDuration.Value;
            Properties.Settings.Default.DefaultSASDurationInHours = (int)numericUpDownSASDuration.Value;

            Properties.Settings.Default.DefaultTokenDurationInMin = (int)numericUpDownTokenDuration.Value;
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
            checkBoxAutoRefresh.Checked = false;
            comboBoxAutoRefreshTime.SelectedItem = "60";

            checkBoxUseAdaptiveStreamingFormat.Checked = false;
            checkBoxHideTaskbarNotifications.Checked = false;

            textBoxCustomPlayer.Text = string.Format(Constants.PlayerAMPinOptions, Constants.NameconvManifestURL);
            checkBoxEnableCustomPlayer.Checked = false;

            textBoxCurrency.Text = "$";
            numericUpDownLocatorDuration.Value = 3650;
            numericUpDownSASDuration.Value = 24;
            numericUpDownTokenDuration.Value = 60;
            numericUpDownMESPrice.Value = ((decimal)0.015);

            numericUpDownAssetAnalysisStart.Value = 10;
            numericUpDownAssetAnalysisStep.Value = 20;

            textBoxffmpegPath.Text = @"%programfiles32%\ffmpeg\bin";
            textBoxVLCPath.Text = @"%programfiles32%\VideoLAN\VLC";

            Properties.Settings.Default.PremiumWorkflowPresetXMLFilesCurrentFolder = Application.StartupPath + Constants.PathPremiumWorkflowFiles;
            Properties.Settings.Default.DefaultSlateCurrentFolder = Application.StartupPath + Constants.PathDefaultSlateJPG;

            Properties.Settings.Default.DynEncTokenIssuerv3 = "http://testacs";
            Properties.Settings.Default.DynEncTokenAudiencev3 = "urn:test";

            Program.SaveAndProtectUserConfig();
        }

        private void options_Load(object sender, EventArgs e)
        {
            checkBoxDisplayAssetID.Checked = Properties.Settings.Default.DisplayAssetIDinGrid;
            checkBoxDisplayAssetAltId.Checked = Properties.Settings.Default.DisplayAssetAltIDinGrid;
            checkBoxDisplayAssetStorage.Checked = Properties.Settings.Default.DisplayAssetStorageinGrid;
            checkBoxAutoRefresh.Checked = Properties.Settings.Default.AutoRefresh;
            comboBoxAutoRefreshTime.SelectedItem = Properties.Settings.Default.AutoRefreshTime.ToString();

            checkBoxUseAdaptiveStreamingFormat.Checked = Properties.Settings.Default.OutputAssetsAdaptiveStreamingFormat;

            textBoxCustomPlayer.Text = Properties.Settings.Default.CustomPlayerUrl;
            checkBoxEnableCustomPlayer.Checked = Properties.Settings.Default.CustomPlayerEnabled;
            textBoxCustomPlayer.Enabled = checkBoxEnableCustomPlayer.Checked;

            numericUpDownLocatorDuration.Value = Properties.Settings.Default.DefaultLocatorDurationDaysNew;
            numericUpDownSASDuration.Value = Properties.Settings.Default.DefaultSASDurationInHours;
            numericUpDownTokenDuration.Value = Properties.Settings.Default.DefaultTokenDurationInMin;
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
