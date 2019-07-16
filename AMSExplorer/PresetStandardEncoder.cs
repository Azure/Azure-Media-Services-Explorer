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

using Microsoft.Azure.Management.Media.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace AMSExplorer
{

    public partial class PresetStandardEncoder : Form
    {
        private string _unique;

        public readonly IList<Profile> Profiles = new List<Profile> {
            new Profile() {Prof=@"AdaptiveStreaming", Desc="Auto-generate a bitrate ladder (bitrate-resolution pairs) based on the input resolution and bitrate. This built-in encoder setting, or preset, will never exceed the input resolution and bitrate. For example, if the input is 720p at 3 Mbps, output remains 720p at best, and will start at rates lower than 3 Mbps. The output contains an audio-only MP4 file with stereo audio encoded at 128 kbps.", Automatic=true},
            new Profile() {Prof=@"ContentAwareEncodingExperimental", Desc="Exposes an experimental preset for content-aware encoding. Given any input content, the service attempts to automatically determine the optimal number of layers, appropriate bitrate and resolution settings for delivery by adaptive streaming. The underlying algorithms will continue to evolve over time. The output will contain MP4 files with video and audio interleaved.", Automatic=true},
            new Profile() {Prof=@"H264SingleBitrateSD", Desc="Produces an MP4 file where the video is encoded with H.264 codec at 2200 kbps and a picture height of 480 pixels, and the stereo audio is encoded with AAC-LC codec at 64 kbps.", Automatic=true},
            new Profile() {Prof=@"H264SingleBitrate1080p", Desc="Produces an MP4 file where the video is encoded with H.264 codec at 6750 kbps and a picture height of 1080 pixels, and the stereo audio is encoded with AAC-LC codec at 64 kbps.", Automatic=true},
            new Profile() {Prof=@"H264SingleBitrate720p", Desc="Produces an MP4 file where the video is encoded with H.264 codec at 4500 kbps and a picture height of 720 pixels, and the stereo audio is encoded with AAC-LC codec at 64 kbps.", Automatic=true},
            new Profile() {Prof=@"AACGoodQualityAudio", Desc="Produces a single MP4 file containing only stereo audio encoded at 192 kbps.", Automatic=false},
            new Profile() {Prof=@"H264MultipleBitrate1080p", Desc="Produces a set of 8 GOP-aligned MP4 files, ranging from 6000 kbps to 400 kbps, and stereo AAC audio.Resolution starts at 1080p and goes down to 360p.", Automatic=false},
            new Profile() {Prof=@"H264MultipleBitrate720p", Desc="Produces a set of 6 GOP-aligned MP4 files, ranging from 3400 kbps to 400 kbps, and stereo AAC audio. Resolution starts at 720p and goes down to 360p", Automatic=false},
            new Profile() {Prof=@"H264MultipleBitrateSD", Desc="Produces a set of 5 GOP-aligned MP4 files, ranging from 1600kbps to 400 kbps, and stereo AAC audio. Resolution starts at 480p and goes down to 360p.", Automatic=false},
                    };

        private Profile ReturnProfile(string name)
        {
            return Profiles.Where(p => p.Prof == name).FirstOrDefault();
        }

        public EncoderNamedPreset BuiltInPreset
        {
            get
            {
                return (EncoderNamedPreset)((listboxPresets.SelectedItem as Item).Value);

            }
        }

        public string TransformName
        {
            get
            {
                return textBoxTransformName.Text;
            }
        }

        public string Description
        {
            get
            {
                return string.IsNullOrWhiteSpace(textBoxDescription.Text) ? null : textBoxDescription.Text;
            }
        }

        public PresetStandardEncoder()
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            _unique = Guid.NewGuid().ToString().Substring(0, 13);
        }

        private void PresetStandardEncoder_Load(object sender, EventArgs e)
        {
            listboxPresets.Items.Add(new Item(EncoderNamedPreset.AdaptiveStreaming, EncoderNamedPreset.AdaptiveStreaming));
            listboxPresets.Items.Add(new Item(EncoderNamedPreset.ContentAwareEncodingExperimental, EncoderNamedPreset.ContentAwareEncodingExperimental));
            listboxPresets.Items.Add(new Item(EncoderNamedPreset.H264MultipleBitrate1080p, EncoderNamedPreset.H264MultipleBitrate1080p));
            listboxPresets.Items.Add(new Item(EncoderNamedPreset.H264MultipleBitrate720p, EncoderNamedPreset.H264MultipleBitrate720p));
            listboxPresets.Items.Add(new Item(EncoderNamedPreset.H264MultipleBitrateSD, EncoderNamedPreset.H264MultipleBitrateSD));
            listboxPresets.Items.Add(new Item(EncoderNamedPreset.H264SingleBitrate1080p, EncoderNamedPreset.H264SingleBitrate1080p));
            listboxPresets.Items.Add(new Item(EncoderNamedPreset.H264SingleBitrate720p, EncoderNamedPreset.H264SingleBitrate720p));
            listboxPresets.Items.Add(new Item(EncoderNamedPreset.H264SingleBitrateSD, EncoderNamedPreset.H264SingleBitrateSD));
            listboxPresets.Items.Add(new Item(EncoderNamedPreset.AACGoodQualityAudio, EncoderNamedPreset.AACGoodQualityAudio));

            listboxPresets.SelectedIndex = 0;

            moreinfoprofilelink.Links.Add(new LinkLabel.Link(0, moreinfoprofilelink.Text.Length, Constants.LinkMoreInfoMediaEncoderBuiltIn));
            UpdateTransformLabel();
        }

        private void moreinfoprofilelink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Send the URL to the operating system.
            Process.Start(e.Link.LinkData as string);
        }

        private void UpdateTransformLabel()
        {

            textBoxTransformName.Text = "StandardEncoder-" + BuiltInPreset.ToString();// + "-" + _unique;
        }

        private void listboxPresets_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateTransformLabel();

            var profile = Profiles.Where(p => p.Prof == listboxPresets.SelectedItem.ToString()).FirstOrDefault();
            if (profile != null)
            {
                richTextBoxDesc.Text = profile.Desc;
            }
            else
            {
                richTextBoxDesc.Text = string.Empty;
            }
        }
    }

    public class Profile
    {
        public string Prof { get; set; }
        public string Desc { get; set; }
        public bool Automatic { get; set; }
    }
}
