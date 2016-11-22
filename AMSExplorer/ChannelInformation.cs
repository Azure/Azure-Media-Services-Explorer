//----------------------------------------------------------------------------------------------
//    Copyright 2016 Microsoft Corporation
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
using System.Net;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text.RegularExpressions;


namespace AMSExplorer
{
    public partial class ChannelInformation : Form
    {
        public IChannel MyChannel;
        public CloudMediaContext MyContext;
        public bool MultipleSelection = false;
        public ExplorerChannelModifications Modifications = new ExplorerChannelModifications();
        private BindingList<IPRange> InputEndpointSettingList = new BindingList<IPRange>();
        private BindingList<IPRange> PreviewEndpointSettingList = new BindingList<IPRange>();
        private Mainform MyMainForm;
        private string defaultEncodingPreset = "";
        private BindingList<ExplorerAudioStream> audiostreams = new BindingList<ExplorerAudioStream>();
        private string defaultAudioStreamCode = null;

        public IList<IPRange> GetInputIPAllowList
        {
            get
            {
                return (checkBoxInputSet.Checked) ? InputEndpointSettingList : null;
            }
        }

        public IList<IPRange> GetPreviewAllowList
        {
            get
            {
                return (checkBoxPreviewSet.Checked) ? PreviewEndpointSettingList : null;
            }
        }

        public string GetChannelDescription
        {
            get
            {
                return textboxchannedesc.Text;
            }
        }

        public string GetChannelClientPolicy
        {
            get { return (checkBoxclientpolicy.Checked) ? textBoxClientPolicy.Text : null; }
        }

        public string GetChannelCrossdomainPolicy
        {
            get { return (checkBoxcrossdomains.Checked) ? textBoxCrossDomPolicy.Text : null; }
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
        }

        public short? HLSFragPerSegment
        {
            get
            {
                return checkBoxHLSFragPerSeg.Checked ? (short?)numericUpDownHLSFragPerSeg.Value : null;
            }
        }

        public string SystemPreset
        {
            get
            {
                return radioButtonCustomPreset.Checked ? textBoxCustomPreset.Text : defaultEncodingPreset;
            }
        }

        public bool Ignore708Captions
        {
            get
            {
                return checkBoxIgnore708.Checked;
            }
        }

        public ReadOnlyCollection<VideoStream> VideoStreamList
        {
            get
            {
                if (MyChannel.Input.StreamingProtocol == StreamingProtocol.RTPMPEG2TS)
                { // RTP
                    List<VideoStream> videostreams = new List<VideoStream>();
                    videostreams.Add(new VideoStream() { Index = (int)numericUpDownVideoStreamIndex.Value });
                    return new ReadOnlyCollection<VideoStream>(videostreams);
                }
                else
                {
                    return null;
                }
            }
        }

        public ReadOnlyCollection<AudioStream> AudioStreamList
        {
            get
            {
                if (
                   MyChannel.Input.StreamingProtocol == StreamingProtocol.RTPMPEG2TS && ((Item)comboBoxAudioLanguageMain.SelectedItem).Value != null // if list does not exist, the user must select a valid default index
                   )
                { // RTP
                    List<AudioStream> audiostreamsl = new List<AudioStream>();
                    audiostreamsl.Add(new AudioStream() { Language = defaultAudioStreamCode, Index = (int)numericUpDownAudioIndexMain.Value });
                    foreach (var s in audiostreams)
                    {
                        audiostreamsl.Add(new AudioStream()
                        {
                            Index = s.Index,
                            Language = s.Code
                        });
                    }
                    return new ReadOnlyCollection<AudioStream>(audiostreamsl);
                }
                else
                {
                    return null;
                }
            }
        }

        public ChannelInformation(Mainform mainform)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            MyMainForm = mainform;
        }

        private void contextMenuStripDG_MouseClick(object sender, MouseEventArgs e)
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


        private void ChannelInformation_Load(object sender, EventArgs e)
        {

            if (!MultipleSelection) // one channel
            {
                labelChannelName.Text += MyChannel.Name;

                DGChannel.ColumnCount = 2;

                // channel info
                DGChannel.Columns[0].DefaultCellStyle.BackColor = Color.Gainsboro;
                DGChannel.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_Name, MyChannel.Name);
                DGChannel.Rows.Add("Id", MyChannel.Id);
                DGChannel.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_State, (ChannelState)MyChannel.State);
                DGChannel.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_Created, ((DateTime)MyChannel.Created).ToLocalTime().ToString("G"));
                DGChannel.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_LastModified, ((DateTime)MyChannel.LastModified).ToLocalTime().ToString("G"));
                DGChannel.Rows.Add(AMSExplorer.Properties.Resources.ChannelInformation_ChannelInformation_Load_Description, MyChannel.Description);
                DGChannel.Rows.Add(AMSExplorer.Properties.Resources.ChannelInformation_ChannelInformation_Load_InputProtocol, MyChannel.Input.StreamingProtocol);
                DGChannel.Rows.Add(AMSExplorer.Properties.Resources.ChannelInformation_ChannelInformation_Load_EncodingType, MyChannel.EncodingType);

                if (MyChannel.Encoding != null)
                {
                    DGChannel.Rows.Add(AMSExplorer.Properties.Resources.ChannelInformation_ChannelInformation_Load_EncodingSystemPreset, MyChannel.Encoding.SystemPreset);
                    DGChannel.Rows.Add(AMSExplorer.Properties.Resources.ChannelInformation_ChannelInformation_Load_EncodingIgnoreCEA708, MyChannel.Encoding.IgnoreCea708ClosedCaptions);
                    DGChannel.Rows.Add(AMSExplorer.Properties.Resources.ChannelInformation_ChannelInformation_Load_EncodingVideoStreamsCount, MyChannel.Encoding.VideoStreams.Count);
                    DGChannel.Rows.Add(AMSExplorer.Properties.Resources.ChannelInformation_ChannelInformation_Load_EncodingAudioStreamsCount, MyChannel.Encoding.AudioStreams.Count);
                    DGChannel.Rows.Add(AMSExplorer.Properties.Resources.ChannelInformation_ChannelInformation_Load_EncodingAdMarkerSource, (AdMarkerSource)MyChannel.Encoding.AdMarkerSource);

                    if (MyChannel.Slate != null)
                    {
                        DGChannel.Rows.Add(AMSExplorer.Properties.Resources.ChannelInformation_ChannelInformation_Load_DefaultSlateAssetId, MyChannel.Slate.DefaultSlateAssetId);
                        DGChannel.Rows.Add(AMSExplorer.Properties.Resources.ChannelInformation_ChannelInformation_Load_AutomaticSlateInsertionOnADSignal, MyChannel.Slate.InsertSlateOnAdMarker);
                    }
                    else
                    {
                        DGChannel.Rows.Add(AMSExplorer.Properties.Resources.ChannelInformation_ChannelInformation_Load_SlateSettings, AMSExplorer.Properties.Resources.ChannelInformation_ChannelInformation_Load_None);
                    }
                }


                if (MyChannel.Input.KeyFrameInterval != null)
                {
                    DGChannel.Rows.Add(AMSExplorer.Properties.Resources.ChannelInformation_ChannelInformation_Load_InputKeyFrameIntervalS, ((TimeSpan)MyChannel.Input.KeyFrameInterval).TotalSeconds);
                    checkBoxKeyFrameIntDefined.Checked = true;
                    textBoxKeyFrame.Text = ((TimeSpan)MyChannel.Input.KeyFrameInterval).TotalSeconds.ToString();
                }

                string[] stringnameurl = new string[] { AMSExplorer.Properties.Resources.ChannelInformation_ChannelInformation_Load_Primary, AMSExplorer.Properties.Resources.ChannelInformation_ChannelInformation_Load_Secondary };

                int i = 0;
                foreach (var endpoint in MyChannel.Input.Endpoints)
                {
                    DGChannel.Rows.Add(string.Format(AMSExplorer.Properties.Resources.ChannelInformation_ChannelInformation_Load_0InputURL1, MyChannel.Input.Endpoints.Count == 2 ? stringnameurl[i] : "", endpoint.Protocol), endpoint.Url);
                    if (MyChannel.Input.StreamingProtocol == StreamingProtocol.FragmentedMP4)
                    {
                        DGChannel.Rows.Add(string.Format(AMSExplorer.Properties.Resources.ChannelInformation_ChannelInformation_Load_0InputURL1SSL, MyChannel.Input.Endpoints.Count == 2 ? stringnameurl[i] : "", endpoint.Protocol), endpoint.Url.ToString().Replace("http://", "https://"));
                    }
                    i++;
                }
                foreach (var endpoint in MyChannel.Preview.Endpoints)
                {
                    DGChannel.Rows.Add(string.Format(AMSExplorer.Properties.Resources.ChannelInformation_ChannelInformation_Load_PreviewURL0, endpoint.Protocol), endpoint.Url);
                }
                if (MyChannel.Output != null)
                {
                    if (MyChannel.Output.Hls != null)
                    {
                        if (MyChannel.Output.Hls.FragmentsPerSegment != null)
                        {
                            DGChannel.Rows.Add(AMSExplorer.Properties.Resources.ChannelInformation_ChannelInformation_Load_OutputHLSFragmentsPerSegment, MyChannel.Output.Hls.FragmentsPerSegment);
                            checkBoxHLSFragPerSeg.Checked = true;
                            numericUpDownHLSFragPerSeg.Value = (int)MyChannel.Output.Hls.FragmentsPerSegment;
                        }
                    }
                }
            }
            else // multiselect
            {
                labelChannelName.Text = AMSExplorer.Properties.Resources.ChannelInformation_ChannelInformation_Load_MultipleChannelsHaveBeenSelected;

                tabControl1.TabPages.Remove(tabPageChannelInfo); // no channel info page
                tabControl1.TabPages.Remove(tabPagePreview); // no channel info page

                if (MyChannel.Input.KeyFrameInterval != null)
                {
                    checkBoxKeyFrameIntDefined.Checked = true;
                    textBoxKeyFrame.Text = ((TimeSpan)MyChannel.Input.KeyFrameInterval).TotalSeconds.ToString();
                }

                if (MyChannel.Output != null)
                {
                    if (MyChannel.Output.Hls != null)
                    {
                        if (MyChannel.Output.Hls.FragmentsPerSegment != null)
                        {
                            checkBoxHLSFragPerSeg.Checked = true;
                            numericUpDownHLSFragPerSeg.Value = (int)MyChannel.Output.Hls.FragmentsPerSegment;
                        }
                    }
                }
            }

            // comon code - multiselect or only one channel selected

            // Encoding Settings
            if (MyChannel.EncodingType != ChannelEncodingType.None)
            {
                // default encoding profile name
                var profileliveselected = AMSEXPlorerLiveProfile.Profiles.Where(p => p.Type == MyChannel.EncodingType).FirstOrDefault();
                if (profileliveselected != null)
                {
                    defaultEncodingPreset = profileliveselected.Name;
                    radioButtonDefaultPreset.Text = string.Format((radioButtonDefaultPreset.Tag as string), defaultEncodingPreset);
                }

                if (MyChannel.Encoding != null && MyChannel.Encoding.SystemPreset != null)
                {
                    if (MyChannel.Encoding.SystemPreset == defaultEncodingPreset)
                    {
                        radioButtonDefaultPreset.Checked = true;
                    }
                    else
                    {
                        radioButtonCustomPreset.Checked = true;
                        textBoxCustomPreset.Text = MyChannel.Encoding.SystemPreset;
                    }
                    checkBoxIgnore708.Checked = MyChannel.Encoding.IgnoreCea708ClosedCaptions;
                }
                if (MyChannel.State != ChannelState.Stopped)
                {
                    groupBoxEncoding.Enabled = false; // encoding settings cannot be edited
                    groupBoxVideoStream.Enabled = false;
                    checkBoxIgnore708.Enabled = false;
                    labelChannelMustBeStopped.Visible = true;
                    labelIndexesChannelMustBeStopped.Visible = true;
                    panelStreamIndexes.Enabled = false; // encoding settings cannot be edited
                }

                if (MyChannel.Input.StreamingProtocol != StreamingProtocol.RTPMPEG2TS)
                {
                    tabControl1.TabPages.Remove(tabPageIndexes); // no rtp channel, no indexes
                    groupBoxVideoStream.Visible = false;
                }
                else
                {
                    numericUpDownVideoStreamIndex.Value = MyChannel.Encoding.VideoStreams != null && MyChannel.Encoding.VideoStreams.FirstOrDefault() != null ?
                        MyChannel.Encoding.VideoStreams.FirstOrDefault().Index
                        : 0;

                    Item myitem = new Item("Undefined", "und");
                    comboBoxAudioLanguageMain.Items.Add(myitem);
                    comboBoxAudioLanguageAddition.Items.Add(myitem);
                    comboBoxAudioLanguageAddition.SelectedItem = myitem;

                    foreach (CultureInfo ci in CultureInfo.GetCultures(CultureTypes.NeutralCultures).OrderBy(c => c.DisplayName))
                    {
                        myitem = new Item(ci.DisplayName, ci.ThreeLetterISOLanguageName);
                        comboBoxAudioLanguageMain.Items.Add(myitem);
                        comboBoxAudioLanguageAddition.Items.Add(myitem);
                        if (MyChannel.Encoding.AudioStreams.Count > 0 && MyChannel.Encoding.AudioStreams[0].Language == ci.ThreeLetterISOLanguageName)
                        {
                            comboBoxAudioLanguageMain.SelectedItem = myitem;
                        }
                    }

                    if (MyChannel.Encoding.AudioStreams != null && MyChannel.Encoding.AudioStreams.Count > 0)
                    {

                        if (comboBoxAudioLanguageMain.SelectedItem == null) // code which is not in culture list !
                        {
                            myitem = new Item(MyChannel.Encoding.AudioStreams[0].Language, MyChannel.Encoding.AudioStreams[0].Language);
                            comboBoxAudioLanguageMain.Items.Add(myitem);
                            comboBoxAudioLanguageMain.SelectedItem = myitem;
                        }

                        defaultAudioStreamCode = MyChannel.Encoding.AudioStreams[0].Language; // in order to keep the initial code if needed

                        numericUpDownAudioIndexMain.Value = MyChannel.Encoding.AudioStreams[0].Index;

                        if (MyChannel.Encoding.AudioStreams.Count > 1)
                        {
                            int index = 0;
                            foreach (var audiostream in MyChannel.Encoding.AudioStreams)
                            {
                                if (index > 0)
                                {
                                    var language = CultureInfo.GetCultures(CultureTypes.NeutralCultures).Where(c => c.ThreeLetterISOLanguageName == audiostream.Language).FirstOrDefault();
                                    audiostreams.Add(new ExplorerAudioStream()
                                    {
                                        Language = language != null ? language.DisplayName : audiostream.Language,
                                        Index = audiostream.Index,
                                        Code = audiostream.Language
                                    }
                                        );
                                }
                                index++;
                            }
                        }

                        // let set the next possible index
                        List<int> audioindex = new List<int>() { MyChannel.Encoding.AudioStreams[0].Index };
                        audioindex.AddRange(MyChannel.Encoding.AudioStreams.Select(a => a.Index).ToList());
                        int indexAvailable = 0;
                        while (true)
                        {
                            if (!audioindex.Contains(indexAvailable))
                            {
                                break;
                            }
                            indexAvailable++;
                        }
                        numericUpDownAudioIndexAddition.Value = indexAvailable;

                    }
                    else // no audiostream list !
                    {
                        defaultAudioStreamCode = null;
                        numericUpDownAudioIndexMain.Value = 0;

                        myitem = new Item("<null>", null);
                        comboBoxAudioLanguageMain.Items.Add(myitem);
                        comboBoxAudioLanguageMain.SelectedItem = myitem;
                    }

                    dataGridViewAudioStreams.DataSource = audiostreams;
                    dataGridViewAudioStreams.DataError += new DataGridViewDataErrorEventHandler(dataGridView_DataError);
                }

                UpdateProfileGrids();
            }
            else
            {
                tabControl1.TabPages.Remove(tabPageEncoding); // no encoding channel
                tabControl1.TabPages.Remove(tabPageIndexes); // no encoding channel, no indexes
            }


            if (MyChannel.Input.AccessControl != null)
            {
                if (MyChannel.Input.AccessControl.IPAllowList != null)
                {
                    checkBoxInputSet.Checked = true;
                    foreach (var endpoint in MyChannel.Input.AccessControl.IPAllowList)
                    {
                        InputEndpointSettingList.Add(endpoint);
                    }
                }
            }
            dataGridViewInputIP.DataSource = InputEndpointSettingList;
            dataGridViewInputIP.DataError += new DataGridViewDataErrorEventHandler(dataGridView_DataError);

            if (MyChannel.Preview.AccessControl != null)
            {
                if (MyChannel.Preview.AccessControl.IPAllowList != null)
                {
                    checkBoxPreviewSet.Checked = true;
                    foreach (var endpoint in MyChannel.Preview.AccessControl.IPAllowList)
                    {
                        PreviewEndpointSettingList.Add(endpoint);
                    }
                }
            }
            dataGridViewPreviewIP.DataSource = PreviewEndpointSettingList;
            dataGridViewPreviewIP.DataError += new DataGridViewDataErrorEventHandler(dataGridView_DataError);


            if (MyChannel.CrossSiteAccessPolicies != null)
            {
                if (MyChannel.CrossSiteAccessPolicies.ClientAccessPolicy != null)
                {
                    checkBoxclientpolicy.Checked = true;
                    textBoxClientPolicy.Text = MyChannel.CrossSiteAccessPolicies.ClientAccessPolicy;
                }
                if (MyChannel.CrossSiteAccessPolicies.CrossDomainPolicy != null)
                {
                    checkBoxcrossdomains.Checked = true;
                    textBoxCrossDomPolicy.Text = MyChannel.CrossSiteAccessPolicies.CrossDomainPolicy;
                }
            }
            textboxchannedesc.Text = MyChannel.Description;

            // Channel is not stopped or running. We cannot update settings
            if (MyChannel.State != ChannelState.Stopped && MyChannel.State != ChannelState.Running)
            {
                labelChannelStoppedOrStartedSettings.Visible = true;
                buttonUpdateClose.Enabled = false;
            }

            // let's track when user edit a setting
            Modifications = new ExplorerChannelModifications
            {
                Description = false,
                AudioStreams = false,
                ClientAccessPolicy = false,
                CrossDomainPolicy = false,
                HLSFragPerSegment = false,
                InputIPAllowList = false,
                KeyFrameInterval = false,
                PreviewIPAllowList = false,
                SystemPreset = false,
                VideoStreams = false,
                Ignore708Captions = false
            };
        }

        void dataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

            MessageBox.Show("Wrong format");
        }

        private void contextMenuStripDG_Opening(object sender, CancelEventArgs e)
        {

        }


        private void ChanneltInformation_FormClosed(object sender, FormClosedEventArgs e)
        {
            // let's sure we dispose the webbrowser control
            webBrowserPreview.Url = null;
            webBrowserPreview.Dispose();
        }



        private void toolStripMenuItemFilesCopyClipboard_Click(object sender, EventArgs e)
        {

        }

        private void buttonAddIngestIP_Click(object sender, EventArgs e)
        {
            InputEndpointSettingList.AddNew();
            Modifications.InputIPAllowList = true;
        }

        private void buttonAddPreviewIP_Click(object sender, EventArgs e)
        {
            PreviewEndpointSettingList.AddNew();
            Modifications.PreviewIPAllowList = true;
        }

        private void buttonDelIngestIP_Click(object sender, EventArgs e)
        {
            if (dataGridViewInputIP.SelectedRows.Count == 1)
            {
                InputEndpointSettingList.RemoveAt(dataGridViewInputIP.SelectedRows[0].Index);
                Modifications.InputIPAllowList = true;
            }
        }

        private void buttonDelPreviewIP_Click(object sender, EventArgs e)
        {
            if (dataGridViewPreviewIP.SelectedRows.Count == 1)
            {
                PreviewEndpointSettingList.RemoveAt(dataGridViewPreviewIP.SelectedRows[0].Index);
                Modifications.PreviewIPAllowList = true;
            }
        }


        private void ChannelInformation_Shown(object sender, EventArgs e)
        {

        }

        private void checkBoxPreviewSet_CheckedChanged(object sender, EventArgs e)
        {
            dataGridViewPreviewIP.Enabled = checkBoxPreviewSet.Checked;
            buttonAddPreviewIP.Enabled = checkBoxPreviewSet.Checked;
            buttonDelPreviewIP.Enabled = checkBoxPreviewSet.Checked;
            Modifications.PreviewIPAllowList = true;
        }


        private void checkBoxclientpolicy_CheckedChanged_1(object sender, EventArgs e)
        {
            textBoxClientPolicy.Enabled = checkBoxclientpolicy.Checked;
            Modifications.ClientAccessPolicy = true;
        }

        private void checkBoxcrossdomains_CheckedChanged_1(object sender, EventArgs e)
        {
            textBoxCrossDomPolicy.Enabled = checkBoxcrossdomains.Checked;
            Modifications.CrossDomainPolicy = true;
        }

        private void dataGridViewInputIP_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {

        }

        private void checkBoxInputSet_CheckedChanged(object sender, EventArgs e)
        {
            dataGridViewInputIP.Enabled = checkBoxInputSet.Checked;
            buttonAddInputIP.Enabled = checkBoxInputSet.Checked;
            buttonDelInputIP.Enabled = checkBoxInputSet.Checked;
            Modifications.InputIPAllowList = true;
        }

        private void checkBoxKeyFrameIntDefined_CheckedChanged(object sender, EventArgs e)
        {
            textBoxKeyFrame.Enabled = checkBoxKeyFrameIntDefined.Checked;
            checkKeyFrameValue();
            Modifications.KeyFrameInterval = true;
        }

        private void checkBoxHLSFragPerSeg_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownHLSFragPerSeg.Enabled = checkBoxHLSFragPerSeg.Checked;
            Modifications.HLSFragPerSegment = true;
        }

        private void buttonAllowAllInputIP_Click(object sender, EventArgs e)
        {
            InputEndpointSettingList.Clear();
            InputEndpointSettingList.Add(new IPRange() { Name = AMSExplorer.Properties.Resources.ChannelInformation_buttonAllowAllInputIP_Click_AllowAll, Address = IPAddress.Parse("0.0.0.0"), SubnetPrefixLength = 0 });
            checkBoxInputSet.Checked = true;
            Modifications.InputIPAllowList = true;
        }

        private void buttonAllowAllPreviewIP_Click(object sender, EventArgs e)
        {
            checkBoxPreviewSet.Checked = false;
            PreviewEndpointSettingList.Clear();
            Modifications.PreviewIPAllowList = true;
        }

        private void tabPage4_Enter(object sender, EventArgs e)
        {
            if (MyChannel.State == ChannelState.Running && MyChannel.Preview.Endpoints.FirstOrDefault().Url.AbsoluteUri != null)
            {
                string myurl = AssetInfo.DoPlayBackWithStreamingEndpoint(typeplayer: PlayerType.AzureMediaPlayerFrame, Urlstr: MyChannel.Preview.Endpoints.FirstOrDefault().Url.ToString(), DoNotRewriteURL: true, context: MyContext, formatamp: AzureMediaPlayerFormats.Smooth, technology: AzureMediaPlayerTechnologies.Silverlight, launchbrowser: false, mainForm: MyMainForm);
                webBrowserPreview.Url = new Uri(myurl);
            }
        }

        private void tabPage4_Leave(object sender, EventArgs e)
        {
            webBrowserPreview.Url = null;
        }

        private void textBoxKeyFrame_TextChanged(object sender, EventArgs e)
        {
            checkKeyFrameValue();
            Modifications.KeyFrameInterval = true;
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

        private void radioButtonCustomPreset_CheckedChanged(object sender, EventArgs e)
        {
            UpdateProfileGrids();
            textBoxCustomPreset.Enabled = radioButtonCustomPreset.Checked;
            Modifications.SystemPreset = true;
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

                    var audio = this.AudioStreamList;
                    if (audio != null)
                    {
                        foreach (var audiostream in audio)
                        {
                            profmultiaudio.Add(new AMSEXPlorerLiveProfile.LiveAudioProfile() { Language = audiostream.Language, Bitrate = profileliveselected.Audio.Bitrate, Channels = profileliveselected.Audio.Channels, Codec = profileliveselected.Audio.Codec, SamplingRate = profileliveselected.Audio.SamplingRate });
                        }
                    }
                    else // no specific audio language specified
                    {
                        profmultiaudio.Add(new AMSEXPlorerLiveProfile.LiveAudioProfile() { Language = "und", Bitrate = profileliveselected.Audio.Bitrate, Channels = profileliveselected.Audio.Channels, Codec = profileliveselected.Audio.Codec, SamplingRate = profileliveselected.Audio.SamplingRate });
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

        private string ReturnLiveEncodingProfile()
        {
            if (MyChannel.EncodingType != ChannelEncodingType.None)
            {
                return radioButtonCustomPreset.Checked ? textBoxCustomPreset.Text : defaultEncodingPreset;
            }
            else
            {
                return null;
            }
        }

        private void buttonAddAudioStream_Click(object sender, EventArgs e)
        {
            if (numericUpDownAudioIndexMain.Value != numericUpDownAudioIndexAddition.Value
                && !audiostreams.Select(a => a.Index).ToList().Contains((int)numericUpDownAudioIndexAddition.Value)
                && audiostreams.Count < 7
                && ((Item)comboBoxAudioLanguageMain.SelectedItem).Value != null // if list does not exist, the user must select a valid default index
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
                Modifications.AudioStreams = true;
            }
        }

        private void buttonDelAddOption_Click(object sender, EventArgs e)
        {
            if (dataGridViewAudioStreams.SelectedRows.Count == 1)
            {
                audiostreams.RemoveAt(dataGridViewAudioStreams.SelectedRows[0].Index);
                UpdateProfileGrids();
                Modifications.AudioStreams = true;
            }
        }

        private void comboBoxAudioLanguageMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            string lang = ((Item)comboBoxAudioLanguageMain.SelectedItem).Name;
            var culture = CultureInfo.GetCultures(CultureTypes.NeutralCultures).Where(c => c.DisplayName == lang).FirstOrDefault();
            if (culture != null)
            {
                defaultAudioStreamCode = culture.ThreeLetterISOLanguageName;
            }
            else
            {
                defaultAudioStreamCode = null;
            }
            UpdateProfileGrids();
            Modifications.AudioStreams = true;
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
            UpdateProfileGrids();
            Modifications.AudioStreams = true;
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

        private void numericUpDownVideoStreamIndex_ValueChanged(object sender, EventArgs e)
        {
            Modifications.VideoStreams = true;
        }

        private void textBoxCustomPreset_TextChanged(object sender, EventArgs e)
        {
            UpdateProfileGrids();
            Modifications.SystemPreset = true;
        }

        private void textboxchannedesc_TextChanged(object sender, EventArgs e)
        {
            Modifications.Description = true;
        }

        private void numericUpDownHLSFragPerSeg_ValueChanged(object sender, EventArgs e)
        {
            Modifications.HLSFragPerSegment = true;
        }

        private void radioButtonDefaultPreset_CheckedChanged(object sender, EventArgs e)
        {
            Modifications.SystemPreset = true;
        }

        private void textBoxClientPolicy_TextChanged(object sender, EventArgs e)
        {
            Modifications.ClientAccessPolicy = true;
        }

        private void textBoxCrossDomPolicy_TextChanged(object sender, EventArgs e)
        {
            Modifications.CrossDomainPolicy = true;
        }

        private void checkBoxIgnore708_CheckedChanged(object sender, EventArgs e)
        {
            Modifications.Ignore708Captions = true;
        }
    }

    public class ExplorerAudioStream
    {
        public int Index { get; set; }
        public string Language { get; set; }
        public string Code { get; set; }
    }

    public class ExplorerChannelModifications
    {
        public bool Description { get; set; }
        public bool KeyFrameInterval { get; set; }
        public bool SystemPreset { get; set; }
        public bool AudioStreams { get; set; }
        public bool VideoStreams { get; set; }
        public bool HLSFragPerSegment { get; set; }
        public bool InputIPAllowList { get; set; }
        public bool PreviewIPAllowList { get; set; }
        public bool ClientAccessPolicy { get; set; }
        public bool CrossDomainPolicy { get; set; }
        public bool Ignore708Captions { get; set; }
    }
}
