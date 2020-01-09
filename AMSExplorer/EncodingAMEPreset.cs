//----------------------------------------------------------------------------------------------
//    Copyright 2020 Microsoft Corporation
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
    public partial class EncodingAMEPreset : Form
    {
        private List<IMediaProcessor> Procs;
        private CloudMediaContext _context;


        public readonly IList<Profile> Profiles = new List<Profile> {
        new Profile() {Prof=@"AAC Good Quality Audio", Desc="Produces an MP4 file containing 44.1 kHz 16 bits/sample stereo audio CBR encoded at 192 kbps using AAC. Use this preset name to produce an audio-only file for music services. The output file extension is *.mp4."}, 
        new Profile() {Prof=@"H264 Adaptive Bitrate MP4 Set 1080p", Desc="Produces an asset with multiple GOP-aligned MP4 files:\n\n\t• 44.1 kHz 16 bits/sample stereo audio CBR encoded at 128 kbps using AAC\n\t• 1080p video CBR encoded at 8 bitrates ranging from 6000 kbps to 400 kbps using H.264 High Profile, and two second GOPs\n\nUse this preset name to produce an asset from 1080p (16:9 aspect ratio) content for delivery via one of many adaptive streaming technologies after suitable packaging. If the source frame size is not 1920x1080, will stretch the video at the highest bitrate horizontally to 1920 pixels, and the height will increase/decrease correspondingly. Videos at lower bitrates will be down-scaled to one of 75%, 50% or 25% of the highest bitrate video."}, 
        new Profile() {Prof=@"H264 Adaptive Bitrate MP4 Set 720p", Desc="Produces an asset with multiple GOP-aligned MP4 files:\n\n\t• 44.1 kHz 16 bits/sample stereo audio CBR encoded at 96 kbps using AAC\n\t• 720p video CBR encoded at 6 bitrates ranging from 3400 kbps to 400 kbps using H.264 Main Profile, and two second GOPs\n\nUse this preset name to produce an asset from 720p (16:9 aspect ratio) content for delivery via one of many adaptive streaming technologies after suitable packaging. If the source frame size is not 1280x720, will stretch the video at the highest bitrate horizontally to 1280 pixels, and the height will increase/decrease correspondingly. Videos at lower bitrates will be down-scaled to one of 75%, 50% or 25% of the highest bitrate video."}, 
        new Profile() {Prof=@"H264 Adaptive Bitrate MP4 Set SD 16x9", Desc="Produces an asset with multiple GOP-aligned MP4 files:\n\n\t• A separate audio-only stream 44.1 kHz 16 bits/sample stereo audio CBR encoded at 96 kbps using AAC\n\t• A separate audio-only stream 44.1 kHz 16 bits/sample stereo audio CBR encoded at 56 kbps using AAC\n\t• SD video CBR encoded at 5 bitrates ranging from 1900 kbps to 400 kbps using H.264 Main Profile, and two second GOPs\n\nUse this preset name to produce an asset from SD (16:9 aspect ratio) content for delivery via one of many adaptive streaming technologies after suitable packaging. If the source frame size is not 852x480, will stretch the video at the highest bitrate horizontally to 852 pixels, and the height will increase/decrease correspondingly. Videos at lower bitrates will be down-scaled to one of 75%, 50% or 25% of the highest bitrate video."}, 
        new Profile() {Prof=@"H264 Adaptive Bitrate MP4 Set SD 4x3", Desc="Produces an asset with multiple GOP-aligned MP4 files:\n\n\t• A separate audio-only stream 44.1 kHz 16 bits/sample stereo audio CBR encoded at 96 kbps using AAC\n\t• A separate audio-only stream 44.1 kHz 16 bits/sample stereo audio CBR encoded at 56 kbps using AAC\n\t• SD video CBR encoded at 5 bitrates ranging from 1600 kbps to 400 kbps using H.264 Main Profile, and two second GOPs\n\nUse this preset name to produce an asset from SD (4:3 aspect ratio) content for delivery via one of many adaptive streaming technologies after suitable packaging. If the source frame size is not 640x480, will stretch the video at the highest bitrate horizontally to 640 pixels, and the height will increase/decrease correspondingly. Videos at lower bitrates will be down-scaled to one of 75%, 50% or 25% of the highest bitrate video."},
        new Profile() {Prof=@"H264 Broadband 1080p", Desc="Produces a single MP4 file with:\n\n\t• 44.1 kHz 16 bits/sample stereo audio CBR encoded at 128 kbps using AAC\n\t• 1080p video CBR encoded at 6750 kbps using H.264 High Profile\n\nUse this preset name to produce a downloadable file for 1080p (16:9 aspect ratio) content for delivery over broadband connections. The output file extension is *.mp4. If the source frame size is not 1920x1080, the video will be scaled horizontally to the width of the profile target of 1920 pixels, and its height will be scaled to match the aspect ratio of the source."}, 
        new Profile() {Prof=@"H264 Broadband 720p", Desc="Produces a single MP4 file with:\n\n\t• 44.1 kHz 16 bits/sample stereo audio CBR encoded at 128 kbps using AAC\n\t• 720p video CBR encoded at 4500 kbps using H.264 Main Profile\n\nUse this preset name to produce a downloadable file for 720p (16:9 aspect ratio) content for delivery over broadband connections. The output file extension is *. mp4. If the source frame size is not 1280x720, the video will be scaled horizontally to the width of the profile target of 1280 pixels, and its height will be scaled to match the aspect ratio of the source."}, 
        new Profile() {Prof=@"H264 Broadband SD 16x9", Desc="Produces a single MP4 file with:\n\n\t• 44.1 kHz 16 bits/sample stereo audio CBR encoded at 128 kbps using AAC\n\t• SD video VBR encoded at 2200 kbps using H.264 Main Profile\n\nUse this preset name to produce a downloadable file for SD (16:9 aspect ratio) content for delivery over broadband connections. The output file extension is *. mp4. If the source frame size is not 852x480, the video will be scaled horizontally to the width of the profile target of 852 pixels, and its height will be scaled to match the aspect ratio of the source."}, 
        new Profile() {Prof=@"H264 Broadband SD 4x3", Desc="Produces a single MP4 file with:\n\n\t• 44.1 kHz 16 bits/sample stereo audio CBR encoded at 128 kbps using AAC\n\t• SD video VBR encoded at 1800 kbps using H.264 Main Profile\n\nUse this preset name to produce a downloadable file for SD (4:3 aspect ratio) content for delivery over broadband connections. The output file extension is *. mp4. If the source frame size is not 640x480, the video will be scaled horizontally to the width of the profile target of 640 pixels, and its height will be scaled to match the aspect ratio of the source."}, 
        new Profile() {Prof=@"H264 Smooth Streaming 1080p", Desc="Produces a Smooth Streaming asset with:\n\n\t• 44.1 kHz 16 bits/sample stereo audio CBR encoded at 128 kbps using AAC\n\t• 1080p video CBR encoded at 8 bitrates ranging from 6000 kbps to 400 kbps using H.264 High Profile, and two second GOPs\n\nUse this preset name to produce an asset from 1080p (16:9 aspect ratio) content for delivery via IIS Smooth Streaming. If the source frame size is not 1920x1080, will stretch the video at the highest bitrate horizontally to 1920 pixels, and the height will increase/decrease correspondingly. Videos at lower bitrates will be down-scaled to one of 75%, 50% or 25% of the highest bitrate video."}, 
        new Profile() {Prof=@"H264 Smooth Streaming 720p", Desc="Produces a Smooth Streaming asset with:\n\n\t• 44.1 kHz 16 bits/sample stereo audio CBR encoded at 96 kbps using AAC\n\t• 720p video CBR encoded at 6 bitrates ranging from 3400 kbps to 400 kbps using H.264 Main Profile, and two second GOPs\n\nUse this preset name to produce an asset from 720p (16:9 aspect ratio) content for delivery via IIS Smooth Streaming. If the source frame size is not 1280x720, will stretch the video at the highest bitrate horizontally to 1280 pixels, and the height will increase/decrease correspondingly. Videos at lower bitrates will be down-scaled to one of 75%, 50% or 25% of the highest bitrate video."}, 
        new Profile() {Prof=@"H264 Smooth Streaming 720p for 3G or 4G", Desc="Produces a Smooth Streaming asset with:\n\n\t• 44.1 kHz 16 bits/sample stereo audio CBR encoded at 56 kbps using AAC\n\t• 720p video CBR encoded at 8 bitrates ranging from 3400 kbps to 150 kbps using H.264 Main Profile, and two second GOPs\n\nSame as H264 Smooth Streaming 720p, with audio lowered to 56 kbps, and two additional lower bitrate video layers added at 250 kbps and 150 kbps. These lowest bitrate encodes should help when streaming over 3G or 4G connections to mobile devices"}, 
        new Profile() {Prof=@"H264 Smooth Streaming SD 16x9", Desc="Produces a Smooth Streaming asset with:\n\n\t• 44.1 kHz 16 bits/sample stereo audio CBR encoded at 96 kbps using AAC\n\t• SD video CBR encoded at 5 bitrates ranging from 1900 kbps to 400 kbps using H.264 Main Profile, and two second GOPs\n\nUse this preset name to produce an asset from SD (16:9 aspect ratio) content for delivery via IIS Smooth Streaming. If the source frame size is not 852x480, will stretch the video at the highest bitrate horizontally to 852 pixels, and the height will increase/decrease correspondingly. Videos at lower bitrates will be down-scaled to one of 75%, 50% or 25% of the highest bitrate video."}, 
        new Profile() {Prof=@"H264 Smooth Streaming SD 4x3", Desc="Produces a Smooth Streaming asset with:\n\n\t• 44.1 kHz 16 bits/sample stereo audio CBR encoded at 96 kbps using AAC\n\t• SD video CBR encoded at 5 bitrates ranging from 1600 kbps to 400 kbps using H.264 Main Profile, and two second GOPs\n\nUse this preset name to produce an asset from SD (4:3 aspect ratio) content for delivery via IIS Smooth Streaming. If the source frame size is not 640x480, will stretch the video at the highest bitrate horizontally to 640 pixels, and the height will increase/decrease correspondingly. Videos at lower bitrates will be down-scaled to one of 75%, 50% or 25% of the highest bitrate video."}, 
        new Profile() {Prof=@"H264 Smooth Streaming Windows Phone 7 Series", Desc="Produces an asset with multiple GOP-aligned MP4 files:\n\n\t• 44.1 kHz 16 bits/sample stereo audio CBR encoded at 64 kbps using HE-AAC Level 1\n\t• SD video CBR encoded at 5 bitrates ranging from 1000 kbps to 200 kbps using H.264 Main Profile, and two second GOPs\n\nUse this preset name to produce an asset from SD (16:9 aspect ratio) content for delivery via IIS Smooth Streaming to Windows Phone 7 Series devices. If the source frame size is not 640x360, will stretch the video at all bitrates horizontally to 640 pixels, and the height will increase/decrease correspondingly."}, 
        new Profile() {Prof=@"Thumbnails", Desc="Produces a series of JPEG thumbnails 5 seconds apart, 300 pixels wide. The height is determined by the source frame size. Use this preset name to generate a series of thumbnails for use in Xbox Live Applications. For information about providing a custom configuration file, see Task Preset for Thumbnail Generation."}, 
        new Profile() {Prof=@"VC1 Broadband 1080p", Desc="Produces a single Windows Media file with:\n\n\t• 44.1 kHz 16 bits/sample stereo audio CBR encoded at 128 kbps using WMA Pro\n\t• 1080p video VBR encoded at 6750 kbps using VC-1 Advanced Profile\n\nUse this preset name to produce a downloadable file for 1080p (16:9 aspect ratio) content for delivery over broadband connections. The output file extension is *.wmv. If the source frame size is not 1920x1080, the video will be scaled horizontally to the width of the profile target (e.g. 1920, 1280, 852 or 640 pixels), and its height will be scaled to match the aspect ratio of the source."}, 
        new Profile() {Prof=@"VC1 Broadband 720p", Desc="Produces a single Windows Media file with:\n\n\t• 44.1 kHz 16 bits/sample stereo audio CBR encoded at 128 kbps using WMA Pro\n\t• 720p video VBR encoded at 4500 kbps using VC-1 Advanced Profile\n\nUse this preset name to produce a downloadable file for 720p (16:9 aspect ratio) content for delivery over broadband connections. The output file extension is *.wmv. If the source frame size is not 1280x720, the video will be scaled horizontally to the width of the profile target of 1280 pixels, and its height will be scaled to match the aspect ratio of the source."}, 
        new Profile() {Prof=@"VC1 Broadband SD 16x9", Desc="Produces a single Windows Media file with:\n\n\t• 44.1 kHz 16 bits/sample stereo audio CBR encoded at 128 kbps using WMA Pro\n\t• SD video VBR encoded at 2200 kbps using VC-1 Advanced Profile\n\nUse this preset name to produce a downloadable file for SD (16:9 aspect ratio) content for delivery over broadband connections. The output file extension is *.wmv. If the source frame size is not 852x480, the video will be scaled horizontally to the width of the profile target of 852 pixels, and its height will be scaled to match the aspect ratio of the source."}, 
        new Profile() {Prof=@"VC1 Broadband SD 4x3", Desc="Produces a single Windows Media file with:\n\n\t• 44.1 kHz 16 bits/sample stereo audio CBR encoded at 128 kbps using WMA Pro\n\t• SD video VBR encoded at 1800 kbps using VC-1 Advanced Profile\n\nUse this preset name to produce a downloadable file for SD (4:3 aspect ratio) content for delivery over broadband connections. The output file extension is *.wmv. If the source frame size is not 640x480, the video will be scaled horizontally to the width of the profile target of 640 pixels, and its height will be scaled to match the aspect ratio of the source."}, 
        new Profile() {Prof=@"VC1 Smooth Streaming 1080p", Desc="Produces a Smooth Streaming asset with:\n\n\t• 44.1 kHz 16 bits/sample stereo audio CBR encoded at 128 kbps using WMA Pro\n\t• 1080p video VBR encoded at 8 bitrates ranging from 6000 kbps to 400 kbps using VC-1 Advanced Profile, and two second GOPs\n\nUse this preset name to produce an asset from 1080p (16:9 aspect ratio) content for delivery via IIS Smooth Streaming. If the source frame size is not 1920x1080, will stretch the video at the highest bitrate horizontally to 1920 pixels, and the height will increase/decrease correspondingly. Videos at lower bitrates will be down-scaled respectively."}, 
        new Profile() {Prof=@"VC1 Smooth Streaming 720p", Desc="Produces a Smooth Streaming asset with:\n\n\t• 44.1 kHz 16 bits/sample stereo audio CBR encoded at 128 kbps using WMA Pro\n\t• 720p video VBR encoded at 6 bitrates ranging from 3400 kbps to 400 kbps using VC-1 Advanced Profile, and two second GOPs\n\nUse this preset name to produce an asset from 720p (16:9 aspect ratio) content for delivery via IIS Smooth Streaming. If the source frame size is not 1280x720, will stretch the video at the highest bitrate horizontally to 1280 pixels, and the height will increase/decrease correspondingly. Videos at lower bitrates will be down-scaled respectively."}, 
        new Profile() {Prof=@"VC1 Smooth Streaming 720p Xbox Live ADK", Desc="Produces a Smooth Streaming asset with:\n\n\t• 44.1 kHz 16 bits/sample stereo audio CBR encoded at 128 kbps using WMA Pro\n\t• 720p video VBR encoded at 8 bitrates ranging from 4500 kbps to 350 kbps using VC-1 Advanced Profile, and two second GOPs\n\nUse this preset name to produce an asset from 720p (16:9 aspect ratio) content for delivery via IIS Smooth Streaming to Xbox Live Applications. If the source frame size is not 1280x720, will stretch the video at the highest bitrate horizontally to 1280 pixels, and the height will increase/decrease correspondingly. Videos at lower bitrates will be down-scaled respectively."}, 
        new Profile() {Prof=@"VC1 Smooth Streaming SD 16x9", Desc="Produces a Smooth Streaming asset with:\n\n\t• 44.1 kHz 16 bits/sample stereo audio CBR encoded at 64 kbps using WMA Pro\n\t• SD video VBR encoded at 5 bitrates ranging from 1900 kbps to 400 kbps using VC-1 Advanced Profile, and two second GOPs\n\nUse this preset name to produce an asset from SD (16:9 aspect ratio) content for delivery via IIS Smooth Streaming. If the source frame size is not 852x480, will stretch the video at the highest bitrate horizontally to 852 pixels, and the height will increase/decrease correspondingly. Videos at lower bitrates will be down-scaled respectively."}, 
        new Profile() {Prof=@"VC1 Smooth Streaming SD 4x3", Desc="Produces a Smooth Streaming asset with:\n\n\t• 44.1 kHz 16 bits/sample stereo audio CBR encoded at 64 kbps using WMA Pro\n\t• SD video VBR encoded at 5 bitrates ranging from 1600 kbps to 400 kbps using VC-1 Advanced Profile, and two second GOPs\n\nUse this preset name to produce an asset from SD (4:3 aspect ratio) content for delivery via IIS Smooth Streaming. If the source frame size is not 640x480, will stretch the video at the highest bitrate horizontally to 640 pixels, and the height will increase/decrease correspondingly. Videos at lower bitrates will be down-scaled respectively."}, 
        new Profile() {Prof=@"H264 Adaptive Bitrate MP4 Set 720p for iOS Cellular Only", Desc="Produces an asset with multiple GOP-aligned MP4 files:\n\n\t• A separate audio-only stream 44.1 kHz 16 bits/sample stereo audio CBR encoded at 56 kbps using AAC\n\t• 720p video CBR encoded at 6 bitrates ranging from 3400 kbps to 400 kbps using H.264 Main Profile, and two second GOPs\n\nUse this preset name to produce an asset from 720p (16:9 aspect ratio) content for delivery via one of many adaptive streaming technologies after suitable packaging. If the source frame size is not 1280x720, will stretch the video at the highest bitrate horizontally to 1280 pixels, and the height will increase/decrease correspondingly. Videos at lower bitrates will be down-scaled to one of 75%, 50% or 25% of the highest bitrate video."}, 
        new Profile() {Prof=@"H264 Adaptive Bitrate MP4 Set 1080p for iOS Cellular Only", Desc="Produces an asset with multiple GOP-aligned MP4 files:\n\n\t• A separate audio-only stream 44.1 kHz 16 bits/sample stereo audio CBR encoded at 56 kbps using AAC\n\t• 1080p video CBR encoded at 8 bitrates ranging from 6000 kbps to 400 kbps using H.264 High Profile, and two second GOPs\n\nUse this preset name to produce an asset from 1080p (16:9 aspect ratio) content for delivery via one of many adaptive streaming technologies after suitable packaging for iOS cellular devices. If the source frame size is not 1920x1080, will stretch the video at the highest bitrate horizontally to 1920 pixels, and the height will increase/decrease correspondingly. Videos at lower bitrates will be down-scaled to one of 75%, 50% or 25% of the highest bitrate video."}, 
        new Profile() {Prof=@"H264 Adaptive Bitrate MP4 Set SD 4x3 for iOS Cellular Only", Desc="Produces an asset with multiple GOP-aligned MP4 files:\n\n\t• A separate audio-only stream 44.1 kHz 16 bits/sample stereo audio CBR encoded at 56 kbps using AAC\n\t• SD video CBR encoded at 5 bitrates ranging from 1600 kbps to 400 kbps using H.264 Main Profile, and two second GOPs\n\nUse this preset name to produce an asset from SD (4:3 aspect ratio) content for delivery via one of many adaptive streaming technologies after suitable packaging for iOS cellular devices. If the source frame size is not 640x480, will stretch the video at the highest bitrate horizontally to 640 pixels, and the height will increase/decrease correspondingly. Videos at lower bitrates will be down-scaled to one of 75%, 50% or 25% of the highest bitrate video."}, 
        new Profile() {Prof=@"H264 Adaptive Bitrate MP4 Set SD 16x9 for iOS Cellular Only", Desc="Produces an asset with multiple GOP-aligned MP4 files:\n\n\t• A separate audio-only stream 44.1 kHz 16 bits/sample stereo audio CBR encoded at 56 kbps using AAC\n\t• SD video CBR encoded at 5 bitrates ranging from 1900 kbps to 400 kbps using H.264 Main Profile, and two second GOPs\n\nUse this preset name to produce an asset from SD (16:9 aspect ratio) content for delivery via one of many adaptive streaming technologies after suitable packaging for iOS cellular devices. If the source frame size is not 852x480, will stretch the video at the highest bitrate horizontally to 852 pixels, and the height will increase/decrease correspondingly. Videos at lower bitrates will be down-scaled to one of 75%, 50% or 25% of the highest bitrate video."}, 
        new Profile() {Prof=@"H264 Smooth Streaming 720p Xbox Live ADK", Desc="Produces a Smooth Streaming asset with:\n\n\t• 44.1 kHz 16 bits/sample stereo audio CBR encoded at 96 kbps using AAC\n\t• 720p video CBR encoded at 8 bitrates ranging from 4500 kbps to 350 kbps using H.264 High Profile, and two second GOPs\n\nUse this preset name to produce an asset from 720p (16:9 aspect ratio) content for delivery via IIS Smooth Streaming to Xbox Live Applications. If the source frame size is not 1280x720, will stretch the video at the highest bitrate horizontally to 1280 pixels, and the height will increase/decrease correspondingly. Videos at lower bitrates will be down-scaled respectively."}, 
        new Profile() {Prof=@"WMA High Quality Audio", Desc="Produces a Windows Media file 44.1 kHz 16 bits/sample stereo audio encoded using WMA.Use this preset name to produce an audio-only file for music services. The output file extension is *.wma."}
                    };

        public List<IMediaProcessor> EncodingProcessorsList
        {
            set
            {
                foreach (IMediaProcessor pr in value)
                    comboBoxProcessor.Items.Add(string.Format("{0} {1} Version {2} ({3})", pr.Vendor, pr.Name, pr.Version, pr.Description));
                comboBoxProcessor.SelectedIndex = 0;
                Procs = value;
            }
        }

        public JobOptionsVar JobOptions
        {
            get
            {
                return buttonJobOptions.GetSettings();
            }
            set
            {
                buttonJobOptions.SetSettings(value);
            }
        }

        public IMediaProcessor EncodingProcessorSelected
        {
            get
            {
                return Procs[comboBoxProcessor.SelectedIndex];
            }
        }

        public string EncodingJobName
        {
            get
            {
                return textBoxJobName.Text;
            }
            set
            {
                textBoxJobName.Text = value;
            }
        }



        public string EncodingLabel1
        {
            set
            {
                label.Text = value;
            }
        }


        public string EncodingOutputAssetName
        {
            get
            {
                return outputassetname.Text;
            }
            set
            {
                outputassetname.Text = value;
            }
        }


        public List<string> EncodingSelectedPreset
        {
            get
            {
                List<string> ListOfPresets = new List<string>();
                foreach (var item in listbox.SelectedItems)
                    ListOfPresets.Add(item.ToString());

                return ListOfPresets;
            }
        }


        public EncodingAMEPreset(CloudMediaContext context)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            _context = context;
            buttonJobOptions.Initialize(_context);
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {

        }

        private void moreinfoprofilelink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Send the URL to the operating system.
            Process.Start(e.Link.LinkData as string);
        }



        private void EncodingPreset_Load(object sender, EventArgs e)
        {
            moreinfoame.Links.Add(new LinkLabel.Link(0, moreinfoame.Text.Length, Constants.LinkMoreInfoAME));
            moreinfopresetslink.Links.Add(new LinkLabel.Link(0, moreinfopresetslink.Text.Length, Constants.LinkMorePresetsAME));

            // Populate the combo box with 
            // encoder task presets.
            /* listbox.Items.AddRange(
                 typeof(MediaEncoderTaskPresetStrings)
                 .GetFields()
                 .Select(i => i.GetValue(null) as string)
                 .ToArray()
                 );

             listbox.SelectedItem = listbox.Items.Cast<string>()
                 .SingleOrDefault(i => i == MediaEncoderTaskPresetStrings.H264AdaptiveBitrateMP4Set720p);
             * */

            foreach (var t in Profiles)
            {
                listbox.Items.Add(t.Prof);
            }
            listbox.SelectedItem = listbox.Items.Cast<string>()
                .SingleOrDefault(i => i == MediaEncoderTaskPresetStrings.H264AdaptiveBitrateMP4Set720p);

        }

        private void moreinfoame_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData as string);

        }

        private void listbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listbox.SelectedIndices.Count == 1)
            {
                richTextBoxDesc.Text = Profiles.Where(p => p.Prof == listbox.SelectedItem.ToString()).FirstOrDefault().Desc;

            }
            else
            {
                richTextBoxDesc.Text = "";
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
