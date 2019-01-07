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
using Microsoft.WindowsAzure.MediaServices.Client;
using System.IO;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Blob.Protocol;
using System.Web;
using Microsoft.WindowsAzure.MediaServices.Client.ContentKeyAuthorization;
using Microsoft.WindowsAzure.MediaServices.Client.DynamicEncryption;
using Microsoft.WindowsAzure.MediaServices.Client.Metadata;

namespace AMSExplorer
{
    public partial class MetadataInformation : Form
    {
        private AssetFileMetadata _metadata;

        public MetadataInformation(AssetFileMetadata metadata)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            _metadata = metadata;
            BuildGrid();
        }

        private void BuildGrid()
        {
            labelAssetNameTitle.Text += _metadata.Name;

            DGMetadataGal.ColumnCount = 2;
            DGMetadataVideo.ColumnCount = 2;
            DGMetadataAudio.ColumnCount = 2;

            // general metedata
            DGMetadataGal.Columns[0].DefaultCellStyle.BackColor = Color.Gainsboro;
            DGMetadataVideo.Columns[0].DefaultCellStyle.BackColor = Color.Gainsboro;
            DGMetadataAudio.Columns[0].DefaultCellStyle.BackColor = Color.Gainsboro;
            DGMetadataGal.Rows.Add("Name", _metadata.Name);
            DGMetadataGal.Rows.Add("Duration", _metadata.Duration);
            DGMetadataGal.Rows.Add("Size", AssetInfo.FormatByteSize(_metadata.Size));
            if (_metadata.VideoTracks != null) DGMetadataGal.Rows.Add("Video tracks", _metadata.VideoTracks.Count());
            if (_metadata.AudioTracks != null) DGMetadataGal.Rows.Add("Audio tracks", _metadata.AudioTracks.Count());

            foreach (var source in _metadata.Sources)
                DGMetadataGal.Rows.Add("Source", source.Name);

            if (_metadata.VideoTracks != null) numericUpDownVideoTrack.Maximum = _metadata.VideoTracks.Count() - 1;
            if (_metadata.AudioTracks != null) numericUpDownAudioTrack.Maximum = _metadata.AudioTracks.Count() - 1;
            DGMetadataGal.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            BuildGridVideo();
            BuildGridAudio();
        }

        private void BuildGridAudio()
        {
            // audio metadata
            DGMetadataAudio.Rows.Clear();
            if (_metadata.AudioTracks != null)
            {
                var audio = _metadata.AudioTracks.ElementAt((int)numericUpDownAudioTrack.Value);
                DGMetadataAudio.Rows.Add("Codec", audio.Codec);
                DGMetadataAudio.Rows.Add("Bitrate", audio.Bitrate);
                DGMetadataAudio.Rows.Add("BitsPerSample", audio.BitsPerSample);
                DGMetadataAudio.Rows.Add("Channels", audio.Channels);
                DGMetadataAudio.Rows.Add("SamplingRate", audio.SamplingRate);
                DGMetadataAudio.Rows.Add("EncoderVersion", audio.EncoderVersion);
                DGMetadataAudio.Rows.Add("Id", audio.Id);
            }
            DGMetadataAudio.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void BuildGridVideo()
        {
            // video metadata
            DGMetadataVideo.Rows.Clear();
            if (_metadata.VideoTracks!=null)
            {
                var video = _metadata.VideoTracks.ElementAt((int)numericUpDownVideoTrack.Value);
                DGMetadataVideo.Rows.Add("FourCC", video.FourCC);
                DGMetadataVideo.Rows.Add("Bitrate", video.Bitrate);
                DGMetadataVideo.Rows.Add("TargetBitrate", video.TargetBitrate);
                DGMetadataVideo.Rows.Add("Width", video.Width);
                DGMetadataVideo.Rows.Add("Height", video.Height);
                DGMetadataVideo.Rows.Add("DisplayAspectRatioNumerator", video.DisplayAspectRatioNumerator);
                DGMetadataVideo.Rows.Add("DisplayAspectRatioDenominator", video.DisplayAspectRatioDenominator);
                DGMetadataVideo.Rows.Add("Framerate", video.Framerate);
                DGMetadataVideo.Rows.Add("TargetFramerate", video.TargetFramerate);
                DGMetadataVideo.Rows.Add("Id", video.Id);
            }
            DGMetadataVideo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void MetadataInformation_Load(object sender, EventArgs e)
        {

        }

        private void labelAssetNameTitle_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItemFilesCopyClipboard_Click(object sender, EventArgs e)
        {

        }

        private void hScrollBarAudio_Scroll(object sender, ScrollEventArgs e)
        {
            BuildGridAudio();
        }

        private void hScrollBarVideo_Scroll(object sender, ScrollEventArgs e)
        {
            BuildGridVideo();
        }

        private void contextMenuStripGrid_MouseClick(object sender, MouseEventArgs e)
        {
            ContextMenuStrip contextmenu = (ContextMenuStrip)sender;
            DataGridView DG = (DataGridView)contextmenu.SourceControl;
            if (DG.SelectedCells.Count == 1)
            {
                if (DG.SelectedCells[0].Value != null)
                {
                    System.Windows.Forms.Clipboard.SetText(DG.SelectedCells[0].Value.ToString());
                }
                else
                {
                    System.Windows.Forms.Clipboard.Clear();
                }
            }
        }

        private void numericUpDownVideoTrack_ValueChanged(object sender, EventArgs e)
        {
            BuildGridVideo();
        }

        private void numericUpDownAudioTrack_ValueChanged(object sender, EventArgs e)
        {
            BuildGridAudio();
        }
    }
}
