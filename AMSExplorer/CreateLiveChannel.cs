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
using Microsoft.WindowsAzure.MediaServices.Client;
using System.Net;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text.RegularExpressions;
using System.IO;
using System.Diagnostics;

namespace AMSExplorer
{
    public partial class CreateLiveChannel : Form
    {
        CloudMediaContext MyContext;
        private bool EncodingTabDisplayed = false;
        private bool InitPhase = true;
        private BindingList<ExplorerAudioStream> audiostreams = new BindingList<ExplorerAudioStream>();
        private string defaultEncodingPreset = "";
        private string defaultLanguageString = "und";
        private string _radioButtonDefaultPreset;

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
                    AdMarkerSource = (AdMarkerSource)(Enum.Parse(typeof(AdMarkerSource), ((Item)comboBoxAdMarkerSource.SelectedItem).Value)),
                    IgnoreCea708ClosedCaptions = checkBoxIgnore708.Checked
                };
                if (this.Protocol == StreamingProtocol.RTPMPEG2TS)
                { // RTP
                    List<VideoStream> videostreams = new List<VideoStream>();
                    videostreams.Add(new VideoStream() { Index = (int)numericUpDownVideoStreamIndex.Value });
                    encodingoption.VideoStreams = new ReadOnlyCollection<VideoStream>(videostreams);

                    List<AudioStream> audiostreamsl = new List<AudioStream>();
                    audiostreamsl.Add(new AudioStream() { Language = ((Item)comboBoxAudioLanguageMain.SelectedItem).Value, Index = (int)numericUpDownAudioIndexMain.Value });
                    foreach (var s in audiostreams)
                    {
                        audiostreamsl.Add(new AudioStream()
                        {
                            Index = s.Index,
                            Language = s.Code// CultureInfo.GetCultures(CultureTypes.NeutralCultures).Where(c => c.DisplayName == s.Language).FirstOrDefault().ThreeLetterISOLanguageName
                        });
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

                try
                {
                    if (checkBoxRestrictIngestIP.Checked)
                    {
                        ip = new IPRange() { Name = AMSExplorer.Properties.Resources.CreateLiveChannel_inputIPAllow_Default, Address = IPAddress.Parse(textBoxRestrictIngestIP.Text) };
                    }
                    else
                    {
                        ip = new IPRange() { Name = AMSExplorer.Properties.Resources.ChannelInformation_buttonAllowAllInputIP_Click_AllowAll, Address = IPAddress.Parse("0.0.0.0"), SubnetPrefixLength = 0 };
                    }
                    ips.Add(ip);
                    return ips;
                }
                catch
                {
                    throw;
                }

            }
        }

        public List<IPRange> previewIPAllow
        {
            get
            {
                List<IPRange> ips = new List<IPRange>();

                if (checkBoxRestrictPreviewIP.Checked)
                {
                    try
                    {
                        IPRange ip = new IPRange() { Name = AMSExplorer.Properties.Resources.CreateLiveChannel_inputIPAllow_Default, Address = IPAddress.Parse(textBoxRestrictPreviewIP.Text) };
                        ips.Add(ip);
                    }
                    catch
                    {
                        throw;
                    }
                }
                else
                {
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
            _radioButtonDefaultPreset = radioButtonDefaultPreset.Text;

            FillComboProtocols(false);

            //comboBoxEncodingType.Items.AddRange(Enum.GetNames(typeof(ChannelEncodingType)).ToArray()); // live encoding type
            comboBoxEncodingType.Items.Add(new Item(AMSExplorer.Properties.Resources.CreateLiveChannel_CreateLiveChannel_Load_None, Enum.GetName(typeof(ChannelEncodingType), ChannelEncodingType.None)));
            comboBoxEncodingType.Items.Add(new Item(AMSExplorer.Properties.Resources.CreateLiveChannel_CreateLiveChannel_Load_Standard, Enum.GetName(typeof(ChannelEncodingType), ChannelEncodingType.Standard)));
            if (Properties.Settings.Default.ShowLivePremiumChannel)
            {
                comboBoxEncodingType.Items.Add(new Item(AMSExplorer.Properties.Resources.CreateLiveChannel_CreateLiveChannel_Load_PremiumPreview, Enum.GetName(typeof(ChannelEncodingType), ChannelEncodingType.Premium)));
            }
            comboBoxEncodingType.SelectedIndex = 0;

            tabControlLiveChannel.TabPages.Remove(tabPageLiveEncoding);
            tabControlLiveChannel.TabPages.Remove(tabPageAudioOptions);
            tabControlLiveChannel.TabPages.Remove(tabPageAdConfig);

            dataGridViewAudioStreams.DataSource = audiostreams;
            dataGridViewAudioStreams.DataError += new DataGridViewDataErrorEventHandler(dataGridView_DataError);

            Item myitem = new Item(AMSExplorer.Properties.Resources.CreateLiveChannel_CreateLiveChannel_Load_Undefined, defaultLanguageString);
            comboBoxAudioLanguageMain.Items.Add(myitem);
            comboBoxAudioLanguageMain.SelectedItem = myitem;
            comboBoxAudioLanguageAddition.Items.Add(myitem);
            comboBoxAudioLanguageAddition.SelectedItem = myitem;

            foreach (CultureInfo ci in CultureInfo.GetCultures(CultureTypes.NeutralCultures).OrderBy(c => c.DisplayName))
            {
                myitem = new Item(ci.DisplayName, ci.ThreeLetterISOLanguageName);
                comboBoxAudioLanguageMain.Items.Add(myitem);
                comboBoxAudioLanguageAddition.Items.Add(myitem);
            }

            moreinfoLiveEncodingProfilelink.Links.Add(new LinkLabel.Link(0, moreinfoLiveEncodingProfilelink.Text.Length, Constants.LinkMoreInfoLiveEncoding));
            moreinfoLiveStreamingProfilelink.Links.Add(new LinkLabel.Link(0, moreinfoLiveStreamingProfilelink.Text.Length, Constants.LinkMoreInfoLiveStreaming));
            linkLabelMoreInfoPrice.Links.Add(new LinkLabel.Link(0, linkLabelMoreInfoPrice.Text.Length, Constants.LinkMoreInfoPricing));

            checkChannelName();
            InitPhase = false;
        }
        void dataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show(AMSExplorer.Properties.Resources.CreateLiveChannel_dataGridView_DataError_WrongFormat);
        }

        private void checkBoxRestrictIngestIP_CheckedChanged(object sender, EventArgs e)
        {
            textBoxRestrictIngestIP.Enabled = checkBoxRestrictIngestIP.Checked;
            if (!checkBoxRestrictIngestIP.Checked)
            {
                errorProvider1.SetError(textBoxRestrictIngestIP, String.Empty);
            }
            else
            {
                checkIPAddress(textBoxRestrictIngestIP);
            }
        }

        private void checkBoxHLSFragPerSegDefined_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownHLSFragPerSeg.Enabled = checkBoxHLSFragPerSegDefined.Checked;
        }

        private void checkBoxKeyFrameIntDefined_CheckedChanged(object sender, EventArgs e)
        {
            textBoxKeyFrame.Enabled = checkBoxKeyFrameIntDefined.Checked;
            checkKeyFrameValue();
        }

        private void comboBoxProtocolInput_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxAdMarkerSource.Items.Clear();
            comboBoxAdMarkerSource.Items.Add(new Item(AMSExplorer.Properties.Resources.CreateLiveChannel_comboBoxProtocolInput_SelectedIndexChanged_APIDefault, Enum.GetName(typeof(AdMarkerSource), AdMarkerSource.Api)));
            // SCTE-35 only available or RTP input
            if (this.Protocol == StreamingProtocol.RTPMPEG2TS)
            { // RTP
                comboBoxAdMarkerSource.Items.Add(new Item(AMSExplorer.Properties.Resources.CreateLiveChannel_comboBoxProtocolInput_SelectedIndexChanged_SCTE35CueMessages, Enum.GetName(typeof(AdMarkerSource), AdMarkerSource.Scte35)));
                panelRTP.Enabled = panelAudioControl.Enabled = true;
                labelRTPWarning.Visible = true;
            }
            else
            {
                panelRTP.Enabled = panelAudioControl.Enabled = false;
                labelRTPWarning.Visible = false;
            }
            comboBoxAdMarkerSource.SelectedIndex = 0;
        }

        private void comboBoxEncodingType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!InitPhase)
            {
                moreinfoLiveEncodingProfilelink.Visible = !(EncodingType == ChannelEncodingType.None);
                moreinfoLiveStreamingProfilelink.Visible = !moreinfoLiveEncodingProfilelink.Visible;

                // let's display the encoding tab if encoding has been choosen
                if (EncodingType == ChannelEncodingType.None) 
                {
                    if (EncodingTabDisplayed)
                    {
                        tabControlLiveChannel.TabPages.Remove(tabPageLiveEncoding);
                        tabControlLiveChannel.TabPages.Remove(tabPageAudioOptions);
                        tabControlLiveChannel.TabPages.Remove(tabPageAdConfig);
                        EncodingTabDisplayed = false;
                    }
                    FillComboProtocols(false);
                }
                else
                {
                    FillComboProtocols(false);
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
            var profileliveselected = AMSEXPlorerLiveProfile.Profiles.Where(p => p.Type == EncodingType).FirstOrDefault();
            if (profileliveselected != null)
            {
                defaultEncodingPreset = profileliveselected.Name;
                radioButtonDefaultPreset.Text = string.Format(_radioButtonDefaultPreset, defaultEncodingPreset);
            }
        }


        private void checkBoxRestrictPreviewIP_CheckedChanged(object sender, EventArgs e)
        {
            textBoxRestrictPreviewIP.Enabled = checkBoxRestrictPreviewIP.Checked;
            if (!checkBoxRestrictPreviewIP.Checked)
            {
                errorProvider1.SetError(textBoxRestrictPreviewIP, String.Empty);
            }
            else
            {
                checkIPAddress(textBoxRestrictPreviewIP);
            }
        }

        private void buttonAddAudioStream_Click(object sender, EventArgs e)
        {
            if (numericUpDownAudioIndexMain.Value != numericUpDownAudioIndexAddition.Value
                && !audiostreams.Select(a => a.Index).ToList().Contains((int)numericUpDownAudioIndexAddition.Value)
                && audiostreams.Count < 7 //8 max audio streams
                )
            {
                var selected = (Item)comboBoxAudioLanguageAddition.SelectedItem;
                audiostreams.Add(new ExplorerAudioStream()
                {
                    Language = selected.Name,
                    Index = (int)numericUpDownAudioIndexAddition.Value,
                    Code = selected.Value
                });
                UpdateProfileGrids();
            }
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
                    MessageBox.Show(errorString, AMSExplorer.Properties.Resources.AMSLogin_buttonExport_Click_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                errorProvider1.SetError(checkBoxInsertSlateOnAdMarker, AMSExplorer.Properties.Resources.CreateLiveChannel_checkBoxAdInsertSlate_Validating_NoJPGSelected);
            }
            else
            {
                errorProvider1.SetError(checkBoxInsertSlateOnAdMarker, String.Empty);
            }
        }

        private void textBoxRestrictIP_Validating(object sender, CancelEventArgs e)
        {
            checkIPAddress((TextBox)sender);
        }

        private void checkIPAddress(TextBox tb)
        {
            bool Error = false;
            try
            {
                IPRange ip = new IPRange() { Name = AMSExplorer.Properties.Resources.CreateLiveChannel_inputIPAllow_Default, Address = IPAddress.Parse(tb.Text) };
            }
            catch
            {
                errorProvider1.SetError(tb, AMSExplorer.Properties.Resources.CreateLiveChannel_checkIPAddress_IncorrectIPAddress);
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



        private string ReturnLiveEncodingProfile()
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
            if (encodingprofile != null)
            {
                var profileliveselected = AMSEXPlorerLiveProfile.Profiles.Where(p => p.Name == encodingprofile).FirstOrDefault();
                if (profileliveselected != null)
                {
                    dataGridViewVideoProf.DataSource = profileliveselected.Video;
                    List<AMSEXPlorerLiveProfile.LiveAudioProfile> profmultiaudio = new List<AMSEXPlorerLiveProfile.LiveAudioProfile>();

                    var option = this.EncodingOptions;
                    if (option != null && option.AudioStreams != null)
                    {
                        foreach (var audiostream in this.EncodingOptions.AudioStreams)
                        {
                            profmultiaudio.Add(new AMSEXPlorerLiveProfile.LiveAudioProfile() { Language = audiostream.Language, Bitrate = profileliveselected.Audio.Bitrate, Channels = profileliveselected.Audio.Channels, Codec = profileliveselected.Audio.Codec, SamplingRate = profileliveselected.Audio.SamplingRate });
                        }
                    }
                    else // no specific audio language specified
                    {
                        profmultiaudio.Add(new AMSEXPlorerLiveProfile.LiveAudioProfile() { Language = defaultLanguageString, Bitrate = profileliveselected.Audio.Bitrate, Channels = profileliveselected.Audio.Channels, Codec = profileliveselected.Audio.Codec, SamplingRate = profileliveselected.Audio.SamplingRate });
                    }

                    dataGridViewAudioProf.DataSource = profmultiaudio;
                    panelDisplayEncProfile.Visible = true;
                }
                else
                {
                    dataGridViewVideoProf.DataSource = null;
                    dataGridViewAudioProf.DataSource = null;
                    panelDisplayEncProfile.Visible = false; 
                }
            }
        }

        private void comboBoxAudioLanguageMain_SelectedIndexChanged(object sender, EventArgs e)
        {
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

        private void moreinfoLiveEncodingProfilelink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Send the URL to the operating system.
            Process.Start(e.Link.LinkData as string);
        }

        private void textboxchannelname_TextChanged(object sender, EventArgs e)
        {
            checkChannelName();
        }

        private void checkChannelName()
        {
            TextBox tb = textboxchannelname;

            if (!IsChannelNameValid(tb.Text))
            {
                errorProvider1.SetError(tb, AMSExplorer.Properties.Resources.CreateLiveChannel_checkChannelName_ChannelNameIsNotValid);
            }
            else
            {
                errorProvider1.SetError(tb, String.Empty);
            }
        }

        private void checkKeyFrameValue()
        {
            if (checkBoxKeyFrameIntDefined.Checked && KeyframeInterval == null)
            {
                errorProvider1.SetError(textBoxKeyFrame, AMSExplorer.Properties.Resources.ChannelInformation_checkKeyFrameValue_ValueIsNotValid);
            }
            else
            {
                errorProvider1.SetError(textBoxKeyFrame, String.Empty);
            }
        }

        private void textBoxKeyFrame_TextChanged(object sender, EventArgs e)
        {
            checkKeyFrameValue();
        }

        private void textBoxIP_TextChanged(object sender, EventArgs e)
        {
            checkIPAddress((TextBox)sender);
        }

        private void numericUpDownAudioIndexMain_ValueChanged(object sender, EventArgs e)
        {
            var defaultaudiostream = audiostreams.Where(a => a.Index == numericUpDownAudioIndexMain.Value).FirstOrDefault();
            if (defaultaudiostream != null)
            {
                errorProvider1.SetError(numericUpDownAudioIndexMain, string.Format(AMSExplorer.Properties.Resources.ChannelInformation_numericUpDownAudioIndexMain_ValueChanged_TheAudioStreamIndex0IsRepeated, defaultaudiostream.Index));
            }
            else
            {
                errorProvider1.SetError(numericUpDownAudioIndexMain, String.Empty);
            }
        }

        private void numericUpDownAudioIndexAddition_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDownAudioIndexMain.Value == numericUpDownAudioIndexAddition.Value
            || audiostreams.Select(a => a.Index).ToList().Contains((int)numericUpDownAudioIndexAddition.Value))
            {
                errorProvider1.SetError(numericUpDownAudioIndexAddition, string.Format(AMSExplorer.Properties.Resources.ChannelInformation_numericUpDownAudioIndexMain_ValueChanged_TheAudioStreamIndex0IsRepeated, numericUpDownAudioIndexAddition.Value));
            }
            else
            {
                errorProvider1.SetError(numericUpDownAudioIndexAddition, String.Empty);
            }
        }

        private void radioButtonDefaultPreset_CheckedChanged(object sender, EventArgs e)
        {

        }
    }


}
