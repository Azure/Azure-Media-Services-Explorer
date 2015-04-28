//----------------------------------------------------------------------- 
// <copyright file="ChannelRunLocalEncoder.cs" company="Microsoft">Copyright (c) Microsoft Corporation. All rights reserved.</copyright> 
// <license>
// Azure Media Services Explorer Ver. 3.1
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
using System.Collections.Specialized;
using System.Xml;
using System.Xml.Linq;
using System.Diagnostics;
using System.Configuration;
using Microsoft.WindowsAzure.MediaServices.Client;
using System.IO;

namespace AMSExplorer
{
    public partial class ChannelRunOnPremisesEncoder : Form
    {
        CloudMediaContext _context;
        IList<IChannel> _channels;

        public readonly IList<LocalEncoder> Encoders = new List<LocalEncoder> {
            // ffmpg list devices
            new LocalEncoder() {Name="ffmpeg - List devices", Protocol=null,  Folder=@"%ffmpegpath%\",InstallURL=new Uri("https://www.ffmpeg.org/download.html"), CanBeRunLocally=true, EnableSettings=false, Command= @"ffmpeg.exe -list_devices true -f dshow -i dummy", Comment=""}, 
            // ffmpeg RTMP
            new LocalEncoder() {Name="ffmpeg (RTMP)", Protocol=StreamingProtocol.RTMP, Folder=@"%ffmpegpath%\", InstallURL=new Uri("https://www.ffmpeg.org/download.html"), CanBeRunLocally=true, EnableSettings=true, Command= @"ffmpeg.exe -y -loglevel verbose -f dshow -video_size 1280x720 -r 30 -i video=""%videodevicename%"":audio=""%audiodevicename%"" -strict -2 -c:v libx264 -preset faster -g 60 -keyint_min 60 -vsync cfr -b:v %videobitrate%k -maxrate %videobitrate%k -minrate %videobitrate%k -c:v libx264 -c:a aac -b:a %audiobitrate%k -ar 44100 -f flv %output%/MyStream1", Comment=""}, 
            // ffmpeg RTP
            new LocalEncoder() {Name="ffmpeg (RTP MPEG-TS)", Protocol=StreamingProtocol.RTPMPEG2TS ,Folder=@"%ffmpegpath%\",InstallURL=new Uri("https://www.ffmpeg.org/download.html"), CanBeRunLocally=true, EnableSettings=true, Command= @"ffmpeg -y -f dshow -video_size 1280x720 -r 30 -i video=""%videodevicename%"":audio=""%audiodevicename%"" -c:v libx264 -preset ultrafast -bf 0 -g 60  -vsync cfr -b:v %videobitrate%k -minrate %videobitrate%k -maxrate %videobitrate%k -bufsize %videobitrate%k -strict -2 -c:a aac -ac 2 -ar 44100 -b:a %audiobitrate%k -f mpegts %output%", Comment=""}, 
            // VLC
            new LocalEncoder() {Name="VLC (RTMP)", Protocol=StreamingProtocol.RTMP,Folder=@"%vlcpath%\", InstallURL=new Uri("http://www.videolan.org/vlc/"), CanBeRunLocally=true, EnableSettings=true, Command= @"vlc.exe dshow:// :dshow-vdev=""%videodevicename%"" :dshow-adev=""%audiodevicename%"" :dshow-size=320 :live-caching=3000  :sout=""#transcode{vcodec=h264,vb=%videobitrate%,scale=1,fps=30,venc=x264{keyint=60,preset=veryfast,level=-1,profile=baseline,cabac,slices=8,qcomp=0.4,vbv-maxrate=%videobitrate%,vbv-bufsize=%videobitrate%},acodec=aac,aenc=ffmpeg{strict=-2,b:a=%audiobitrate%k,ac=2,ar=44100}}:std{access=rtmp,mux=ffmpeg{mux=flv},dst=%output%/MyStream1}"" :sout-keep :sout-all :sout-mux-caching=5000", Comment=""} ,
            // Azure Media Capture
            new LocalEncoder() {Name="Azure Media Services Capture for Windows Phone (Smooth Streaming)", Protocol=StreamingProtocol.FragmentedMP4, Folder="",InstallURL=new Uri("http://www.windowsphone.com/s?appid=12dc1fcc-c5bd-4af0-afd8-f30745f94b84"), CanBeRunLocally=false, EnableSettings=false, Command="", Comment= @"Install the app http://www.windowsphone.com/s?appid=12dc1fcc-c5bd-4af0-afd8-f30745f94b84 on Windows Phone and enter the input URL %output%"},
           // Wirecast
            new LocalEncoder() {Name="Telestream Wirecast (RTMP)", Protocol=StreamingProtocol.RTMP, Folder=@"%programfiles64%\Telestream\Wirecast\",InstallURL=new Uri("http://www.telestream.net/wirecast"), CanBeRunLocally=true, EnableSettings=false, Command= "wirecast.exe", Comment= @"Follow the instructions on http://azure.microsoft.com/blog/2014/09/18/azure-media-services-rtmp-support-and-live-encoders/"}, 
          // Adobe Media Encoder
            new LocalEncoder() {Name="Adobe Flash Media Live Encoder 3.2 (RTMP)", Protocol=StreamingProtocol.RTMP, Folder=@"%programfiles32%\Adobe\Flash Media Live Encoder 3.2\",InstallURL=new Uri("http://www.adobe.com/products/flash-media-encoder.html"), CanBeRunLocally=true, EnableSettings=false, Command= @"FlashMediaLiveEncoder.exe", Comment= @"Follow the instructions on http://azure.microsoft.com/blog/2014/09/18/azure-media-services-rtmp-support-and-live-encoders/"} 
     
        };


        public ChannelRunOnPremisesEncoder(CloudMediaContext context, IList<IChannel> channels)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            _context = context;
            _channels = channels;
        }


        private void ChannelRunLocalEncoder_Load(object sender, EventArgs e)
        {
            labelChannel.Text = string.Format(labelChannel.Text, _channels.FirstOrDefault().Name, _channels.FirstOrDefault().Input.StreamingProtocol.ToString());
            labelURL.Text = string.Format(labelURL.Text, _channels.FirstOrDefault().Input.Endpoints.FirstOrDefault().Url.ToString());

            textBoxAudioDeviceName.Text = Properties.Settings.Default.DirectShowAudioDevice;
            textBoxVideoDeviceName.Text = Properties.Settings.Default.DirectShowVideoDevice;

            foreach (var encoder in Encoders)
            {
                comboBoxEncoder.Items.Add(encoder.Name);
            }
            comboBoxEncoder.SelectedIndex = 0;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            try
            {
                Properties.Settings.Default.DirectShowAudioDevice = textBoxAudioDeviceName.Text;
                Properties.Settings.Default.DirectShowVideoDevice = textBoxVideoDeviceName.Text;
                Program.SaveAndProtectUserConfig();

                System.Diagnostics.ProcessStartInfo proc = new System.Diagnostics.ProcessStartInfo();
                proc.WorkingDirectory = textBoxFolder.Text;
                proc.FileName = @"cmd.exe";
                proc.Arguments = "/K " + textBoxCommand.Text;
                System.Diagnostics.Process.Start(proc);
            }
            catch (Exception ex)
            {

            }
        }

        private void EncoderSettings_Changed(object sender, EventArgs e)
        {
            BuildArguments();
        }

        private void BuildArguments(bool buildfolder = false)
        {
            LocalEncoder SelectedEncoder = Encoders.Where(m => m.Name == comboBoxEncoder.Text).FirstOrDefault();
            if (SelectedEncoder != null)
            {
                labelWarning.Text = (_channels.FirstOrDefault().State != ChannelState.Running) ? "Channel is not running. " : "";
                if (SelectedEncoder.Protocol != null && ((StreamingProtocol) SelectedEncoder.Protocol!= _channels.FirstOrDefault().Input.StreamingProtocol))
                {
                    labelWarning.Text += "Input protocol is not matching.";
                }

                textBoxCommand.Text = SelectedEncoder.Command.Replace("%output%", _channels.FirstOrDefault().Input.Endpoints.FirstOrDefault().Url.AbsoluteUri)
                                .Replace("%audiodevicename%", textBoxAudioDeviceName.Text.Trim())
                                .Replace("%videodevicename%", textBoxVideoDeviceName.Text.Trim())
                                .Replace("%audiobitrate%", textBoxAudioBitRate.Text.Trim())
                                .Replace("%videobitrate%", textBoxVideoBitRate.Text.Trim());

                textBoxComment.Text = SelectedEncoder.Comment.Replace("%output%", _channels.FirstOrDefault().Input.Endpoints.FirstOrDefault().Url.AbsoluteUri);
                panelAVSettings.Enabled = SelectedEncoder.EnableSettings;

                if (buildfolder)
                {
                    textBoxFolder.Text = SelectedEncoder.Folder
                        .Replace("%ffmpegpath%", Properties.Settings.Default.ffmpegPath)
                         .Replace("%vlcpath%", Properties.Settings.Default.VLCPath)
                        .Replace("%programfiles32%", Environment.GetFolderPath(Environment.Is64BitOperatingSystem ? Environment.SpecialFolder.ProgramFilesX86 : Environment.SpecialFolder.ProgramFiles))
                        .Replace("%programfiles64%", System.Environment.ExpandEnvironmentVariables("%systemdrive%\\Program Files"));
                    buttonOk.Enabled = SelectedEncoder.CanBeRunLocally;
                    linkLabelInstall.Links.Clear();
                    linkLabelInstall.Links.Add(new LinkLabel.Link(0, linkLabelInstall.Text.Length, SelectedEncoder.InstallURL.ToString()));
                }
            }

        }

        private void comboBoxEncoder_SelectedIndexChanged(object sender, EventArgs e)
        {
            BuildArguments(true);
        }

        private void linkLabelInstall_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData as string);
        }
    }
}
