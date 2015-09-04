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
using Microsoft.WindowsAzure.MediaServices.Client;
using System.Net;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text.RegularExpressions;
using System.IO;


namespace AMSExplorer
{
    public partial class CreateLiveChannel : Form
    {
        CloudMediaContext MyContext;
        private bool EncodingTabDisplayed = false;
        private bool InitPhase = true;
        private BindingList<AudioStream> audiostreams = new BindingList<AudioStream>();
        private string defaultEncodingPreset = "";

        public readonly List<LiveProfile> Profiles = new List<LiveProfile>
        {
            new LiveProfile()
            {
                Type = ChannelEncodingType.Standard,
                Name ="Default720p",
                Video = new List<LiveVideoProfile>()
                {
                    new LiveVideoProfile(){Codec = "H.264", Bitrate= 3500, Width= 1280, Height= 720, Profile= "High", OutputStreamName= "Video_1280x720_30fps_3500kbps"},
                    new LiveVideoProfile(){Codec = "H.264", Bitrate= 2200, Width= 960, Height= 540, Profile= "Main", OutputStreamName= "Video_960x540_30fps_2200kbps"},
                    new LiveVideoProfile(){Codec = "H.264", Bitrate= 1350, Width= 704, Height= 396, Profile= "Main", OutputStreamName= "Video_704x396_30fps_1350kbps"},
                    new LiveVideoProfile(){Codec = "H.264", Bitrate= 850, Width= 512, Height= 288, Profile= "Main", OutputStreamName= "Video_512x288_30fps_850kbps"},
                    new LiveVideoProfile(){Codec = "H.264", Bitrate= 550, Width= 384, Height= 216, Profile= "Main", OutputStreamName= "Video_384x216_30fps_550kbps"},
                    new LiveVideoProfile(){Codec = "H.264", Bitrate= 350, Width= 340, Height= 192, Profile= "Baseline", OutputStreamName= "Video_340x192_30fps_350kbps"},
                    new LiveVideoProfile(){Codec = "H.264", Bitrate= 200, Width= 340, Height= 192, Profile= "Baseline", OutputStreamName= "Video_340x192_30fps_200kbps"},
                        },
                Audio = new LiveAudioProfile()
                    {
                    Codec= "HE-AAC v1", Bitrate= 64, SamplingRate= 44.1, Channels= "Stereo"
                    }
            },
             new LiveProfile()
            {
                Type = ChannelEncodingType.Premium,
                Name ="Default1080p",
                Video = new List<LiveVideoProfile>()
                {
                    new LiveVideoProfile(){Codec = "H.264", Bitrate= 6000, Width= 1920, Height= 1080, Profile= "High", OutputStreamName= "Video_1920x1080_30fps_6000kbps"},
                    new LiveVideoProfile(){Codec = "H.264", Bitrate= 3500, Width= 1280, Height= 720, Profile= "High", OutputStreamName= "Video_1280x720_30fps_3500kbps"},
                    new LiveVideoProfile(){Codec = "H.264", Bitrate= 2200, Width= 960, Height= 540, Profile= "Main", OutputStreamName= "Video_960x540_30fps_2200kbps"},
                    new LiveVideoProfile(){Codec = "H.264", Bitrate= 1350, Width= 704, Height= 396, Profile= "Main", OutputStreamName= "Video_704x396_30fps_1350kbps"},
                    new LiveVideoProfile(){Codec = "H.264", Bitrate= 850, Width= 512, Height= 288, Profile= "Main", OutputStreamName= "Video_512x288_30fps_850kbps"},
                    new LiveVideoProfile(){Codec = "H.264", Bitrate= 550, Width= 384, Height= 216, Profile= "Main", OutputStreamName= "Video_384x216_30fps_550kbps"},
                    new LiveVideoProfile(){Codec = "H.264", Bitrate= 350, Width= 340, Height= 192, Profile= "Baseline", OutputStreamName= "Video_340x192_30fps_350kbps"},
                    new LiveVideoProfile(){Codec = "H.264", Bitrate= 200, Width= 340, Height= 192, Profile= "Baseline", OutputStreamName= "Video_340x192_30fps_200kbps"},
                        },
                Audio = new LiveAudioProfile()
                    {
                    Codec= "HE-AAC v1", Bitrate= 64, SamplingRate= 44.1, Channels= "Stereo"
                    }
            }
        };


        public string ChannelName
        {
            get { return textboxchannelname.Text; }
            set { textboxchannelname.Text = value; }
        }


        public string ChannelDescription
        {
            get { return textBoxDescription.Text; }
            set { textBoxDescription.Text = value; }
        }


        public ChannelEncodingType EncodingType
        {
            get
            {
                return (ChannelEncodingType)(Enum.Parse(typeof(ChannelEncodingType), (string)(comboBoxEncodingType.SelectedItem as Item).Value));
            }
        }

        public ChannelEncoding EncodingOptions
        {
            get
            {
                ChannelEncoding encodingoption = new ChannelEncoding()
                {
                    SystemPreset = radioButtonCustomPreset.Checked ? textBoxCustomPreset.Text : defaultEncodingPreset, // default preset or custom
                    AdMarkerSource = (AdMarkerSource)(Enum.Parse(typeof(AdMarkerSource), ((Item)comboBoxAdMarkerSource.SelectedItem).Value))
                };
                if (this.Protocol == StreamingProtocol.RTPMPEG2TS)
                { // RTP
                    List<VideoStream> videostreams = new List<VideoStream>();
                    videostreams.Add(new VideoStream() { Index = (int)numericUpDownVideoStreamIndex.Value });
                    encodingoption.VideoStreams = new ReadOnlyCollection<VideoStream>(videostreams);

                    List<AudioStream> audiostreamsl = new List<AudioStream>();
                    audiostreamsl.Add(new AudioStream() { Language = ((Item)comboBoxAudioLanguageMain.SelectedItem).Value, Index = (int)numericUpDownAudioIndexMain.Value });
                    if (checkBoxEnableMultiAudio.Checked)
                    {
                        audiostreamsl.AddRange(audiostreams);
                    }
                    encodingoption.AudioStreams = new ReadOnlyCollection<AudioStream>(audiostreamsl);
                }

                return encodingoption;
            }
        }

        public ChannelSlate Slate
        {
            get
            {
                ChannelSlate myslate = null;
                if (checkBoxInsertSlateOnAdMarker.Checked)
                {
                    myslate = new ChannelSlate()
                    {
                        InsertSlateOnAdMarker = checkBoxInsertSlateOnAdMarker.Checked,
                        DefaultSlateAssetId = listViewJPG1.GetSelectedJPG.FirstOrDefault() != null ? listViewJPG1.GetSelectedJPG.FirstOrDefault().Id : null,
                    };
                }
                return myslate;
            }
        }

        public StreamingProtocol Protocol
        {
            get
            {
                return (StreamingProtocol)(Enum.Parse(typeof(StreamingProtocol), (comboBoxProtocolInput.SelectedItem as Item).Value));
            }
        }

        public short? HLSFragmentPerSegment
        {
            get
            {
                return checkBoxHLSFragPerSegDefined.Checked ? (short?)numericUpDownHLSFragPerSeg.Value : null;
            }
            set
            {
                if (value != null)
                    numericUpDownHLSFragPerSeg.Value = (short)value;
            }
        }

        public TimeSpan? KeyframeInterval
        {
            get
            {
                TimeSpan? ts = null;
                if (checkBoxKeyFrameIntDefined.Checked)
                {
                    try
                    {
                        ts = TimeSpan.FromSeconds(Convert.ToDouble(textBoxKeyFrame.Text));
                    }
                    catch
                    {
                    }
                }
                return ts;
            }
            set
            {
                textBoxKeyFrame.Text = value.Value.TotalSeconds.ToString();
            }
        }


        public List<IPRange> inputIPAllow
        {
            get
            {
                List<IPRange> ips = new List<IPRange>();
                IPRange ip;

                if (checkBoxRestrictIngestIP.Checked)
                {
                    ip = new IPRange() { Name = "default", Address = IPAddress.Parse(textBoxRestrictIngestIP.Text) };
                }
                else
                {
                    ip = new IPRange() { Name = "Allow All", Address = IPAddress.Parse("0.0.0.0"), SubnetPrefixLength = 0 };
                }
                ips.Add(ip);
                return ips;
            }
        }

        public List<IPRange> previewIPAllow
        {
            get
            {
                List<IPRange> ips = new List<IPRange>();

                if (checkBoxRestrictPreviewIP.Checked)
                {
                    IPRange ip = new IPRange() { Name = "default", Address = IPAddress.Parse(textBoxRestrictPreviewIP.Text) };
                    ips.Add(ip);

                }
                else
                {
                    //ip = null;// new IPRange() { Name = "Allow All", Address = IPAddress.Parse("0.0.0.0"), SubnetPrefixLength = 0 };
                    ips = null;
                }
                return ips;
            }
        }


        public bool StartChannelNow
        {
            get { return checkBoxStartChannel.Checked; }
            set { checkBoxStartChannel.Checked = value; }
        }

        public CreateLiveChannel(CloudMediaContext context)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            MyContext = context;
        }

        private void CreateLiveChannel_Load(object sender, EventArgs e)
        {
            FillComboProtocols(false);

            //comboBoxEncodingType.Items.AddRange(Enum.GetNames(typeof(ChannelEncodingType)).ToArray()); // live encoding type
            comboBoxEncodingType.Items.Add(new Item("None", Enum.GetName(typeof(ChannelEncodingType), ChannelEncodingType.None)));
            comboBoxEncodingType.Items.Add(new Item("Standard (public preview)", Enum.GetName(typeof(ChannelEncodingType), ChannelEncodingType.Standard)));
            comboBoxEncodingType.Items.Add(new Item("Premium (private preview)", Enum.GetName(typeof(ChannelEncodingType), ChannelEncodingType.Premium)));
            comboBoxEncodingType.SelectedIndex = 0;

            tabControlLiveChannel.TabPages.Remove(tabPageLiveEncoding);
            tabControlLiveChannel.TabPages.Remove(tabPageAudioOptions);
            tabControlLiveChannel.TabPages.Remove(tabPageAdConfig);

            dataGridViewAudioStreams.DataSource = audiostreams;
            dataGridViewAudioStreams.DataError += new DataGridViewDataErrorEventHandler(dataGridView_DataError);

            Item myitem = new Item("Undefined", "und");
            comboBoxAudioLanguageMain.Items.Add(myitem);
            comboBoxAudioLanguageMain.SelectedItem = myitem;
            comboBoxAudioLanguageAddition.Items.Add(myitem);
            comboBoxAudioLanguageAddition.SelectedItem = myitem;

            foreach (CultureInfo ci in CultureInfo.GetCultures(CultureTypes.NeutralCultures))
            {
                myitem = new Item(ci.DisplayName, ci.ThreeLetterISOLanguageName);
                comboBoxAudioLanguageMain.Items.Add(myitem);
                comboBoxAudioLanguageAddition.Items.Add(myitem);
            }
            InitPhase = false;
        }
        void dataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Wrong format");
        }

        private void checkBoxRestrictIngestIP_CheckedChanged(object sender, EventArgs e)
        {
            textBoxRestrictIngestIP.Enabled = checkBoxRestrictIngestIP.Checked;
            if (!checkBoxRestrictIngestIP.Checked) errorProvider1.SetError(textBoxRestrictIngestIP, String.Empty);
        }

        private void checkBoxHLSFragPerSegDefined_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownHLSFragPerSeg.Enabled = checkBoxHLSFragPerSegDefined.Checked;
        }

        private void checkBoxKeyFrameIntDefined_CheckedChanged(object sender, EventArgs e)
        {
            textBoxKeyFrame.Enabled = checkBoxKeyFrameIntDefined.Checked;
        }

        private void comboBoxProtocolInput_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxAdMarkerSource.Items.Clear();
            comboBoxAdMarkerSource.Items.Add(new Item("API (default)", Enum.GetName(typeof(AdMarkerSource), AdMarkerSource.Api)));
            // SCTE-35 only available or RTP input
            if (this.Protocol == StreamingProtocol.RTPMPEG2TS)
            { // RTP
                comboBoxAdMarkerSource.Items.Add(new Item("SCTE-35 Cue Messages", Enum.GetName(typeof(AdMarkerSource), AdMarkerSource.Scte35)));
                panelRTP.Enabled = panelAudioControl.Enabled = true;
            }
            else
            {
                panelRTP.Enabled = panelAudioControl.Enabled = false;
            }
            comboBoxAdMarkerSource.SelectedIndex = 0;
        }

        private void comboBoxEncodingType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!InitPhase)
            {
                // let's display the encoding tab if encoding has been choosen
                if ((EncodingType == ChannelEncodingType.None) && EncodingTabDisplayed)
                {
                    tabControlLiveChannel.TabPages.Remove(tabPageLiveEncoding);
                    tabControlLiveChannel.TabPages.Remove(tabPageAudioOptions);
                    tabControlLiveChannel.TabPages.Remove(tabPageAdConfig);
                    EncodingTabDisplayed = false;
                    FillComboProtocols(false);
                }
                else
                {
                    FillComboProtocols(true);
                    SetLabelDefaultEncLabel();
                    UpdateProfileGrids();
                    if (!EncodingTabDisplayed)
                    {
                        tabControlLiveChannel.TabPages.Add(tabPageLiveEncoding);
                        tabControlLiveChannel.TabPages.Add(tabPageAudioOptions);
                        tabControlLiveChannel.TabPages.Add(tabPageAdConfig);
                        EncodingTabDisplayed = true;
                    }
                }
            }
        }

        private void FillComboProtocols(bool displayrtp)
        {
            comboBoxProtocolInput.Items.Clear();
            comboBoxProtocolInput.Items.Add(new Item(Program.ReturnNameForProtocol(StreamingProtocol.FragmentedMP4), Enum.GetName(typeof(StreamingProtocol), StreamingProtocol.FragmentedMP4)));
            comboBoxProtocolInput.Items.Add(new Item(Program.ReturnNameForProtocol(StreamingProtocol.RTMP), Enum.GetName(typeof(StreamingProtocol), StreamingProtocol.RTMP)));
            if (displayrtp)
            {
                comboBoxProtocolInput.Items.Add(new Item(Program.ReturnNameForProtocol(StreamingProtocol.RTPMPEG2TS), Enum.GetName(typeof(StreamingProtocol), StreamingProtocol.RTPMPEG2TS)));
            }
            comboBoxProtocolInput.SelectedIndex = 0;
        }

        private void SetLabelDefaultEncLabel()
        {
            // default encoding profile name
            var profileliveselected = Profiles.Where(p => p.Type == EncodingType).FirstOrDefault();
            if (profileliveselected != null)
            {
                defaultEncodingPreset = profileliveselected.Name;
                radioButtonDefaultPreset.Text = string.Format((radioButtonDefaultPreset.Tag as string), defaultEncodingPreset);
            }
        }


        private void checkBoxRestrictPreviewIP_CheckedChanged(object sender, EventArgs e)
        {
            textBoxRestrictPreviewIP.Enabled = checkBoxRestrictPreviewIP.Checked;
            if (!checkBoxRestrictPreviewIP.Checked) errorProvider1.SetError(textBoxRestrictPreviewIP, String.Empty);
        }

        private void buttonAddAudioStream_Click(object sender, EventArgs e)
        {
            audiostreams.Add(new AudioStream() { Language = ((Item)comboBoxAudioLanguageAddition.SelectedItem).Value, Index = (int)numericUpDownAudioIndexAddition.Value });
            UpdateProfileGrids();
        }

        internal static bool IsChannelNameValid(string name)
        {
            Regex reg = new Regex(@"^[a-zA-Z0-9]([a-zA-Z0-9-]{0,30}[a-zA-Z0-9])?$", RegexOptions.Compiled);
            return (reg.IsMatch(name));
        }

        private void buttonDelAddOption_Click(object sender, EventArgs e)
        {
            if (dataGridViewAudioStreams.SelectedRows.Count == 1)
            {
                audiostreams.RemoveAt(dataGridViewAudioStreams.SelectedRows[0].Index);
                UpdateProfileGrids();
            }
        }

        private async void buttonUploadSlate_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(Properties.Settings.Default.DefaultSlateCurrentFolder))
            {
                openFileDialogSlate.InitialDirectory = Properties.Settings.Default.DefaultSlateCurrentFolder;
            }

            if (openFileDialogSlate.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.DefaultSlateCurrentFolder = Path.GetDirectoryName(openFileDialogSlate.FileName); // let's save the folder
                Program.SaveAndProtectUserConfig();

                string file = openFileDialogSlate.FileName;
                string errorString = ListViewSlateJPG.CheckSlateFile(file);
                if (!string.IsNullOrEmpty(errorString))
                {
                    MessageBox.Show(errorString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else // file has been validated
                {
                    IAsset asset;
                    progressBarUpload.Value = 0;
                    progressBarUpload.Visible = true;
                    buttonCancel.Enabled = false;
                    buttonUploadSlate.Enabled = false;
                    asset = await Task.Factory.StartNew(() => ProcessUploadFile(Path.GetFileName(file), file));
                    progressBarUpload.Visible = false;
                    buttonCancel.Enabled = true;
                    buttonUploadSlate.Enabled = true;
                    listViewJPG1.LoadJPGs(textBoxJPGSearch.Text);
                }
            }
        }

        private IAsset ProcessUploadFile(string SafeFileName, string FileName, string storageaccount = null)
        {
            if (storageaccount == null) storageaccount = MyContext.DefaultStorageAccount.Name; // no storage account or null, then let's take the default one

            IAsset asset = null;
            IAccessPolicy policy = null;
            ILocator locator = null;

            try
            {
                asset = MyContext.Assets.Create(SafeFileName as string, storageaccount, AssetCreationOptions.None);
                IAssetFile file = asset.AssetFiles.Create(SafeFileName);
                policy = MyContext.AccessPolicies.Create(
                                       SafeFileName,
                                       TimeSpan.FromDays(30),
                                       AccessPermissions.Write | AccessPermissions.List);

                locator = MyContext.Locators.CreateLocator(LocatorType.Sas, asset, policy);
                file.UploadProgressChanged += file_UploadProgressChanged;
                file.Upload(FileName);
                AssetInfo.SetFileAsPrimary(asset, SafeFileName);
            }
            catch
            {
                asset = null;
            }
            finally
            {
                if (locator != null) locator.Delete();
                if (policy != null) policy.Delete();
            }
            return asset;
        }

        private void file_UploadProgressChanged(object sender, Microsoft.WindowsAzure.MediaServices.Client.UploadProgressChangedEventArgs e)
        {
            progressBarUpload.BeginInvoke(new Action(() => progressBarUpload.Value = (int)e.Progress), null);
        }

        private void checkBoxAdInsertSlate_CheckedChanged(object sender, EventArgs e)
        {
            panelInsertSlate.Enabled = checkBoxInsertSlateOnAdMarker.Checked;

            if (checkBoxInsertSlateOnAdMarker.Checked)
            {
                listViewJPG1.LoadJPGs(MyContext);
            }

        }

        private void textBoxJPGSearch_TextChanged(object sender, EventArgs e)
        {
            listViewJPG1.LoadJPGs(textBoxJPGSearch.Text);
        }

        private void checkBoxAdInsertSlate_Validating(object sender, CancelEventArgs e)
        {
            if (checkBoxInsertSlateOnAdMarker.Checked && listViewJPG1.GetSelectedJPG.Count == 0)
            {
                errorProvider1.SetError(checkBoxInsertSlateOnAdMarker, "No JPG selected");
            }
            else
            {
                errorProvider1.SetError(checkBoxInsertSlateOnAdMarker, String.Empty);
            }
        }



        private void textboxchannelname_Validating(object sender, CancelEventArgs e)
        {
            TextBox tb = (TextBox)sender;

            if (!IsChannelNameValid(textboxchannelname.Text))
            {
                errorProvider1.SetError(tb, "Channel name is not valid");
            }
            else
            {
                errorProvider1.SetError(tb, String.Empty);
            }
        }

        private void textBoxRestrictIP_Validating(object sender, CancelEventArgs e)
        {
            TextBox tb = (TextBox)sender;

            bool Error = false;
            try
            {
                IPRange ip = new IPRange() { Name = "default", Address = IPAddress.Parse(tb.Text) };
            }
            catch
            {
                errorProvider1.SetError(tb, "Incorrect IP address");
                Error = true;

            }
            if (!Error)
            {
                errorProvider1.SetError(tb, String.Empty);
            }
        }


        private void listViewJPG1_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider1.SetError(checkBoxInsertSlateOnAdMarker, String.Empty);
        }



        private void comboBoxEncodingPreset_SelectedValueChanged(object sender, EventArgs e)
        {
            UpdateProfileGrids();

        }

        private string ReturnLiveEncodingProfile ()
        {
            if (EncodingType != ChannelEncodingType.None)
            {
                return radioButtonCustomPreset.Checked ? textBoxCustomPreset.Text : defaultEncodingPreset;
            }
            else
            {
                return null;
            }
        }

        private void UpdateProfileGrids()
        {
            string encodingprofile = ReturnLiveEncodingProfile();
            if (encodingprofile!=null)
            {
                var profileliveselected = Profiles.Where(p => p.Name == encodingprofile).FirstOrDefault();
                if (profileliveselected != null)
                {
                    dataGridViewVideoProf.DataSource = profileliveselected.Video;
                    List<LiveAudioProfile> profmultiaudio = new List<LiveAudioProfile>();

                    var option = this.EncodingOptions;
                    if (option != null && option.AudioStreams != null)
                    {
                        foreach (var audiostream in this.EncodingOptions.AudioStreams)
                        {
                            profmultiaudio.Add(new LiveAudioProfile() { Language = audiostream.Language, Bitrate = profileliveselected.Audio.Bitrate, Channels = profileliveselected.Audio.Channels, Codec = profileliveselected.Audio.Codec, SamplingRate = profileliveselected.Audio.SamplingRate });
                        }
                    }
                    else // no specific audio language specified
                    {
                        profmultiaudio.Add(new LiveAudioProfile() { Language = "und", Bitrate = profileliveselected.Audio.Bitrate, Channels = profileliveselected.Audio.Channels, Codec = profileliveselected.Audio.Codec, SamplingRate = profileliveselected.Audio.SamplingRate });
                    }

                    dataGridViewAudioProf.DataSource = profmultiaudio;
                }
                else
                {
                    dataGridViewVideoProf.DataSource = null;
                    dataGridViewAudioProf.DataSource = null;
                }
            }
        }

        private void comboBoxAudioLanguageMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateProfileGrids();
        }

  
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            panelMultiAudio.Enabled = checkBoxEnableMultiAudio.Checked;
            UpdateProfileGrids();
        }

        private void textBoxCustomPreset_TextChanged(object sender, EventArgs e)
        {
            UpdateProfileGrids();

        }

        private void radioButtonCustomPreset_CheckedChanged(object sender, EventArgs e)
        {
            UpdateProfileGrids();
            textBoxCustomPreset.Enabled = radioButtonCustomPreset.Checked;
        }
    }

    public class LiveVideoProfile
    {
        public string Codec { get; set; }
        public int Bitrate { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Profile { get; set; }
        public string OutputStreamName { get; set; }
    }

    public class LiveAudioProfile
    {
        public string Language { get; set; }
        public string Codec { get; set; }
        public int Bitrate { get; set; }
        public double SamplingRate { get; set; }
        public string Channels { get; set; }
    }



    public class LiveProfile
    {
        public string Name { get; set; }
        public ChannelEncodingType Type { get; set; }
        public List<LiveVideoProfile> Video { get; set; }
        public LiveAudioProfile Audio { get; set; }
    }
}
