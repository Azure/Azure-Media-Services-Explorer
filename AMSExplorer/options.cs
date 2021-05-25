//----------------------------------------------------------------------------------------------
//    Copyright 2021 Microsoft Corporation
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

using Microsoft.Azure.Storage.DataMovement;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;


namespace AMSExplorer
{
    public partial class Options : Form
    {
        public Options()
        {
            InitializeComponent();
            Icon = Bitmaps.Azure_Explorer_ico;
        }

        private void options_Load(object sender, EventArgs e)
        {
            // DpiUtils.InitPerMonitorDpi(this);
            checkBoxDisplayAssetID.Checked = Properties.Settings.Default.DisplayAssetIDinGrid;
            checkBoxDisplayAssetAltId.Checked = Properties.Settings.Default.DisplayAssetAltIDinGrid;
            checkBoxDisplayAssetStorage.Checked = Properties.Settings.Default.DisplayAssetStorageinGrid;
            checkBoxAutoRefresh.Checked = Properties.Settings.Default.AutoRefresh;
            comboBoxAutoRefreshTime.SelectedItem = Properties.Settings.Default.AutoRefreshTime.ToString();

            textBoxCustomPlayer.Text = Properties.Settings.Default.CustomPlayerUrl;
            checkBoxEnableCustomPlayer.Checked = Properties.Settings.Default.CustomPlayerEnabled;
            textBoxCustomPlayer.Enabled = checkBoxEnableCustomPlayer.Checked;

            numericUpDownLocatorDuration.Value = Properties.Settings.Default.DefaultLocatorDurationDaysNew;
            numericUpDownSASDuration.Value = Properties.Settings.Default.DefaultSASDurationInHours;
            checkBoxHideTaskbarNotifications.Checked = Properties.Settings.Default.HideTaskbarNotifications;

            numericUpDownAssetAnalysisStart.Value = Properties.Settings.Default.AssetAnalysisStart;
            numericUpDownAssetAnalysisStep.Value = Properties.Settings.Default.AssetAnalysisStep;

            textBoxffmpegPath.Text = Properties.Settings.Default.ffmpegPath;
            textBoxVLCPath.Text = Properties.Settings.Default.VLCPath;

            if (Properties.Settings.Default.DataMovementParallelOperations == -1)
            {
                checkBoxAutoParOpe.Checked = true;
                numericUpDownDataMovNumbParallelOp.Value = TransferManager.Configurations.ParallelOperations;
            }
            else
            {
                checkBoxAutoParOpe.Checked = false;
                numericUpDownDataMovNumbParallelOp.Value = Properties.Settings.Default.DataMovementParallelOperations;
            }

            List<int> listInt = new() { 4, 8, 16, 32, 64, 100 };
            comboBoxBlockSize.Items.Clear();
            listInt.ForEach(l => comboBoxBlockSize.Items.Add(l.ToString()));

            comboBoxBlockSize.SelectedIndex = listInt.IndexOf(Properties.Settings.Default.DataMovementBlockSize);

            checkBoxDisableMD5Check.Checked = Properties.Settings.Default.DataMovementNoMD5Check;
            checkBoxDoNotIncreaseHTTPLimit.Checked = Properties.Settings.Default.DataMovementDoNotIncreaseHttpLimit;

            checkBoxDisableTelemetry.Checked = !Properties.Settings.Default.Telemetry;
        }


        private void buttonOk_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.DisplayAssetIDinGrid = checkBoxDisplayAssetID.Checked;
            Properties.Settings.Default.DisplayAssetAltIDinGrid = checkBoxDisplayAssetAltId.Checked;
            Properties.Settings.Default.DisplayAssetStorageinGrid = checkBoxDisplayAssetStorage.Checked;
            Properties.Settings.Default.AutoRefresh = checkBoxAutoRefresh.Checked;
            Properties.Settings.Default.AutoRefreshTime = Convert.ToInt16(comboBoxAutoRefreshTime.SelectedItem);

            Properties.Settings.Default.AssetAnalysisStart = (int)numericUpDownAssetAnalysisStart.Value;
            Properties.Settings.Default.AssetAnalysisStep = (int)numericUpDownAssetAnalysisStep.Value;

            Properties.Settings.Default.CustomPlayerUrl = textBoxCustomPlayer.Text;
            Properties.Settings.Default.CustomPlayerEnabled = checkBoxEnableCustomPlayer.Checked;

            Properties.Settings.Default.DefaultLocatorDurationDaysNew = (int)numericUpDownLocatorDuration.Value;
            Properties.Settings.Default.DefaultSASDurationInHours = (int)numericUpDownSASDuration.Value;

            Properties.Settings.Default.HideTaskbarNotifications = checkBoxHideTaskbarNotifications.Checked;

            Properties.Settings.Default.ffmpegPath = textBoxffmpegPath.Text;
            Properties.Settings.Default.VLCPath = textBoxVLCPath.Text;

            Properties.Settings.Default.DataMovementParallelOperations = (int)(checkBoxAutoParOpe.Checked ? -1 : numericUpDownDataMovNumbParallelOp.Value);
            Properties.Settings.Default.DataMovementNoMD5Check = checkBoxDisableMD5Check.Checked;
            Properties.Settings.Default.DataMovementDoNotIncreaseHttpLimit = checkBoxDoNotIncreaseHTTPLimit.Checked;

            bool success = int.TryParse(comboBoxBlockSize.Text, out int x);
            Properties.Settings.Default.DataMovementBlockSize = success ? x : 8;

            Properties.Settings.Default.Telemetry = !checkBoxDisableTelemetry.Checked;

            Program.SaveAndProtectUserConfig();
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            checkBoxDisplayAssetID.Checked = false;
            checkBoxDisplayAssetAltId.Checked = false;
            checkBoxDisplayAssetStorage.Checked = false;
            checkBoxAutoRefresh.Checked = false;
            comboBoxAutoRefreshTime.SelectedItem = "60";

            checkBoxHideTaskbarNotifications.Checked = false;

            textBoxCustomPlayer.Text = string.Format(Constants.PlayerAMPinOptions, Constants.NameconvManifestURL);
            checkBoxEnableCustomPlayer.Checked = false;

            numericUpDownLocatorDuration.Value = 3650;
            numericUpDownSASDuration.Value = 24;

            numericUpDownAssetAnalysisStart.Value = 10;
            numericUpDownAssetAnalysisStep.Value = 20;

            textBoxffmpegPath.Text = @"%programfiles32%\ffmpeg\bin";
            textBoxVLCPath.Text = @"%programfiles32%\VideoLAN\VLC";

            Properties.Settings.Default.DefaultSlateCurrentFolder = Path.Combine(Application.StartupPath, Constants.PathDefaultSlateJPG);

            Properties.Settings.Default.DynEncTokenIssuerv3 = "http://testacs";
            Properties.Settings.Default.DynEncTokenAudiencev3 = "urn:test";

            //Properties.Settings.Default.DataMovementParallelOperations = -1;
            numericUpDownDataMovNumbParallelOp.Value = Environment.ProcessorCount * 8; ;
            checkBoxAutoParOpe.Checked = true;
            checkBoxDisableMD5Check.Checked = false;
            checkBoxDoNotIncreaseHTTPLimit.Checked = false;
            comboBoxBlockSize.SelectedIndex = 1;

            //Properties.Settings.Default.DataMovementNoMD5Check = false;
            //Properties.Settings.Default.DataMovementDoNotIncreaseHttpLimit = false;

            checkBoxDisableTelemetry.Checked = false;

            Program.SaveAndProtectUserConfig();
        }


        private void checkBoxEnableCustomPlayer_CheckedChanged(object sender, EventArgs e)
        {
            textBoxCustomPlayer.Enabled = checkBoxEnableCustomPlayer.Checked;
        }

        private void amspriceslink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var p = new Process
            {
                StartInfo = new ProcessStartInfo { FileName = e.Link.LinkData as string, UseShellExecute = true }
            }; p.Start();
        }

        private void Options_DpiChanged(object sender, DpiChangedEventArgs e)
        {
            //   // DpiUtils.UpdatedSizeFontAfterDPIChange(labelTitle, e);
        }

        private void checkBoxAutoParOpe_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownDataMovNumbParallelOp.Enabled = !checkBoxAutoParOpe.Checked;
        }

        private void comboBoxBlockSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool success = int.TryParse(comboBoxBlockSize.Text, out int x);
            if (success)
            {
                var sizeText = AssetTools.FormatByteSize(1024 * 1024L * x * 50000);
                labelBlobSizeMax.Text = string.Format((string)labelBlobSizeMax.Tag, sizeText);
            }
            else
            {
                labelBlobSizeMax.Text = string.Empty;
            }
        }
    }
}
