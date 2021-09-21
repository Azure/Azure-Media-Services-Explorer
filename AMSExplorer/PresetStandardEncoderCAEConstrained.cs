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

using Microsoft.Azure.Management.Media.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace AMSExplorer
{

    public partial class PresetStandardEncoderCAEConstrained : Form
    {

        public PresetConfigurations presetConfigurations
        {
            get
            {
                PresetConfigurations presetConfigurations = new PresetConfigurations(
       // Allows you to configure the encoder settings to control the balance between speed and quality. Example: set Complexity as Speed for faster encoding but less compression efficiency.
       complexity: checkBoxComplexity.Checked ? (comboBoxComplexity.SelectedItem as Item).Value : null,
       // The output includes both audio and video.
       interleaveOutput: checkBoxInterleave.Checked ? (comboBoxInterleaveOutput.SelectedItem as Item).Value : null,
       // The key frame interval in seconds. Example: set as 2 to reduce the playback buffering for some players.
       keyFrameIntervalInSeconds: checkBoxKeyFrame.Checked ? (int)numericUpDownKeyFrame.Value : null,
       // The maximum bitrate in bits per second (threshold for the top video layer). Example: set MaxBitrateBps as 6000000 to avoid producing very high bitrate outputs for contents with high complexity.
       maxBitrateBps: checkBoxMaxBitrate.Checked ? (int)numericUpDownMaxBitrate.Value : null,
       // The minimum bitrate in bits per second (threshold for the bottom video layer). Example: set MinBitrateBps as 200000 to have a bottom layer that covers users with low network bandwidth.
       minBitrateBps: checkBoxMinBitrate.Checked ? (int)numericUpDownMinBitrate.Value : null,
       maxHeight: checkBoxMaxHeight.Checked ? (int)numericUpDownMaxHeight.Value : null,
       // The minimum height of output video layers. Example: set MinHeight as 360 to avoid output layers of smaller resolutions like 180P.
       minHeight: checkBoxMinHeight.Checked ? (int)numericUpDownMinHeight.Value : null,
       // The maximum number of output video layers. Example: set MaxLayers as 4 to make sure at most 4 output layers are produced to control the overall cost of the encoding job.
       maxLayers: checkBoxMaxLayers.Checked ? (int)numericUpDownMaxLayers.Value : null
    );

                return presetConfigurations;
            }
        }




        public PresetStandardEncoderCAEConstrained()
        {
            InitializeComponent();
            Icon = Bitmaps.Azure_Explorer_ico;
        }

        private void PresetStandardEncoderCAEConstrained_Load(object sender, EventArgs e)
        {
            comboBoxComplexity.Items.Add(new Item("Speed", Complexity.Speed));
            comboBoxComplexity.Items.Add(new Item("Balanced", Complexity.Balanced));
            comboBoxComplexity.Items.Add(new Item("Quality", Complexity.Quality));
            comboBoxComplexity.SelectedIndex = 1;

            comboBoxInterleaveOutput.Items.Add(new Item("InterleavedOutput", InterleaveOutput.InterleavedOutput));
            comboBoxInterleaveOutput.Items.Add(new Item("NonInterleavedOutput", InterleaveOutput.NonInterleavedOutput));
            comboBoxInterleaveOutput.SelectedIndex = 0;

            moreinfoprofilelink.Links.Clear();
            moreinfoprofilelink.Links.Add(new LinkLabel.Link(0, moreinfoprofilelink.Text.Length, Constants.LinkMoreInfoMediaEncoderThumbnail));
        }

        private void moreinfoprofilelink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Send the URL to the operating system.
            var p = new Process
            {
                StartInfo = new ProcessStartInfo { FileName = e.Link.LinkData as string, UseShellExecute = true }
            }; p.Start();
        }


        private void PresetStandardEncoderThumbnail_DpiChanged(object sender, DpiChangedEventArgs e)
        {
            // DpiUtils.UpdatedSizeFontAfterDPIChange(labelMES, e);
        }



        private void PresetStandardEncoderThumbnail_Shown(object sender, EventArgs e)
        {
            Telemetry.TrackPageView(this.Name);
        }

        private void checkBoxMinBitrate_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownMinBitrate.Enabled = checkBoxMinBitrate.Checked;
        }

        private void checkBoxMaxBitrate_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownMaxBitrate.Enabled = checkBoxMaxBitrate.Checked;
        }

        private void checkBoxMinHeight_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownMinHeight.Enabled = checkBoxMinHeight.Checked;
        }

        private void checkBoxMaxHeight_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownMaxHeight.Enabled = checkBoxMaxHeight.Checked;
        }

        private void checkBoxMaxLayers_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownMaxLayers.Enabled = checkBoxMaxLayers.Checked;
        }

        private void checkBoxKeyFrame_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownKeyFrame.Enabled = checkBoxMaxLayers.Checked;
        }

        private void checkBoxComplexity_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxComplexity.Enabled = checkBoxComplexity.Checked;
        }

        private void checkBoxInterleave_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxInterleaveOutput.Enabled = checkBoxInterleave.Checked;
        }
    }
}
