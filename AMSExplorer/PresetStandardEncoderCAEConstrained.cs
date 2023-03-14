//----------------------------------------------------------------------------------------------
//    Copyright 2023 Microsoft Corporation
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

using Azure.ResourceManager.Media.Models;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace AMSExplorer
{
    public partial class PresetStandardEncoderCAEConstrained : Form
    {
        public EncoderPresetConfigurations presetConfigurations
        {
            get
            {
                EncoderPresetConfigurations presetConfigurations = new()
                {
                    // Allows you to configure the encoder settings to control the balance between speed and quality. Example: set Complexity as Speed for faster encoding but less compression efficiency.
                    Complexity = checkBoxComplexity.Checked ? (comboBoxComplexity.SelectedItem as Item).Value : null,
                    // The output includes both audio and video.
                    InterleaveOutput = checkBoxInterleave.Checked ? (comboBoxInterleaveOutput.SelectedItem as Item).Value : null,
                    // The key frame interval in seconds. Example: set as 2 to reduce the playback buffering for some players.
                    KeyFrameIntervalInSeconds = checkBoxKeyFrame.Checked ? (int)numericUpDownKeyFrame.Value : null,
                    // The maximum bitrate in bits per second (threshold for the top video layer). Example: set MaxBitrateBps as 6000000 to avoid producing very high bitrate outputs for contents with high complexity.
                    MaxBitrateBps = checkBoxMaxBitrate.Checked ? (int)numericUpDownMaxBitrate.Value : null,
                    // The minimum bitrate in bits per second (threshold for the bottom video layer). Example: set MinBitrateBps as 200000 to have a bottom layer that covers users with low network bandwidth.
                    MinBitrateBps = checkBoxMinBitrate.Checked ? (int)numericUpDownMinBitrate.Value : null,
                    MaxHeight = checkBoxMaxHeight.Checked ? (int)numericUpDownMaxHeight.Value : null,
                    // The minimum height of output video layers. Example: set MinHeight as 360 to avoid output layers of smaller resolutions like 180P.
                    MinHeight = checkBoxMinHeight.Checked ? (int)numericUpDownMinHeight.Value : null,
                    // The maximum number of output video layers. Example: set MaxLayers as 4 to make sure at most 4 output layers are produced to control the overall cost of the encoding job.
                    MaxLayers = checkBoxMaxLayers.Checked ? (int)numericUpDownMaxLayers.Value : null
                };


                return presetConfigurations;
            }
            set
            {
                if (value != null)
                {
                    if (value.Complexity != null)
                    {
                        if (value.Complexity == EncodingComplexity.Speed)
                        {
                            comboBoxComplexity.SelectedIndex = 0;
                        }
                        else if (value.Complexity == EncodingComplexity.Balanced)
                        {
                            comboBoxComplexity.SelectedIndex = 1;
                        }
                        if (value.Complexity == EncodingComplexity.Quality)
                        {
                            comboBoxComplexity.SelectedIndex = 2;
                        }
                    }

                    if (value.InterleaveOutput != null)
                    {
                        if (value.InterleaveOutput == InterleaveOutput.InterleavedOutput)
                        {
                            comboBoxInterleaveOutput.SelectedIndex = 0;
                        }
                        else if (value.InterleaveOutput == InterleaveOutput.NonInterleavedOutput)
                        {
                            comboBoxInterleaveOutput.SelectedIndex = 1;
                        }
                    }

                    if (value.KeyFrameIntervalInSeconds != null)
                    {
                        checkBoxKeyFrame.Checked = true;
                        numericUpDownKeyFrame.Value = (decimal)value.KeyFrameIntervalInSeconds;
                    }

                    if (value.MaxBitrateBps != null)
                    {
                        checkBoxMaxBitrate.Checked = true;
                        numericUpDownMaxBitrate.Value = (decimal)value.MaxBitrateBps;
                    }

                    if (value.MinBitrateBps != null)
                    {
                        checkBoxMinBitrate.Checked = true;
                        numericUpDownMinBitrate.Value = (decimal)value.MinBitrateBps;
                    }

                    if (value.MaxHeight != null)
                    {
                        checkBoxMaxHeight.Checked = true;
                        numericUpDownMaxHeight.Value = (decimal)value.MaxHeight;
                    }

                    if (value.MinHeight != null)
                    {
                        checkBoxMinHeight.Checked = true;
                        numericUpDownMinHeight.Value = (decimal)value.MinHeight;
                    }

                    if (value.MaxLayers != null)
                    {
                        checkBoxMaxLayers.Checked = true;
                        numericUpDownMaxLayers.Value = (decimal)value.MaxLayers;
                    }
                }

            }
        }


        public PresetStandardEncoderCAEConstrained()
        {
            InitializeComponent();
            Icon = Bitmaps.Azure_Explorer_ico;
        }


        private void PresetStandardEncoderCAEConstrained_Load(object sender, EventArgs e)
        {
            comboBoxComplexity.Items.Add(new Item("Speed", EncodingComplexity.Speed.ToString()));
            comboBoxComplexity.Items.Add(new Item("Balanced", EncodingComplexity.Balanced.ToString()));
            comboBoxComplexity.Items.Add(new Item("Quality", EncodingComplexity.Quality.ToString()));
            comboBoxComplexity.SelectedIndex = 1;

            comboBoxInterleaveOutput.Items.Add(new Item("InterleavedOutput", InterleaveOutput.InterleavedOutput.ToString()));
            comboBoxInterleaveOutput.Items.Add(new Item("NonInterleavedOutput", InterleaveOutput.NonInterleavedOutput.ToString()));
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