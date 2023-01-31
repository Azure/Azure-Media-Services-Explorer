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

using AMSExplorer.Forms_Live;
using Azure.ResourceManager.Media;
using Azure.ResourceManager.Media.Models;
using Microsoft.Web.WebView2.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace AMSExplorer
{
    public partial class LiveEventInformation : Form
    {
        public MediaLiveEventResource MyLiveEvent;
        public bool MultipleSelection = false;
        public ExplorerLiveEventModifications Modifications = new();
        private readonly BindingList<IPRange> InputEndpointSettingList = new();
        private readonly BindingList<IPRange> PreviewEndpointSettingList = new();
        private readonly Mainform MyMainForm;
        private readonly AMSClientV3 _client;
        private readonly string defaultEncodingPreset = null;
        private readonly BindingList<ExplorerAudioStream> audiostreams = new();
        private string _radioButtonDefaultPreset;

        public IList<IPRange> GetInputAllowList
        {
            get
            {
                IList<IPRange> ipac = new List<IPRange>(InputEndpointSettingList);
                return (checkBoxInputSet.Checked) ? ipac : null;
            }
        }

        public IList<IPRange> GetPreviewAllowList
        {
            get
            {
                IList<IPRange> ipac = new List<IPRange>(PreviewEndpointSettingList);
                return (checkBoxPreviewSet.Checked) ? ipac : null;
            }
        }

        public string GetLiveEventDescription => textboxchannedesc.Text;

        public string GetLiveEventClientPolicy => (checkBoxclientpolicy.Checked) ? textBoxClientPolicy.Text : null;

        public string GetLiveEventCrossdomainPolicy => (checkBoxcrossdomains.Checked) ? textBoxCrossDomPolicy.Text : null;

        public TimeSpan? InputKeyframeIntervalSerialized
        {
            get
            {
                TimeSpan? ts = null;
                if (checkBoxKeyFrameIntDefined.Checked)
                {
                    try
                    {
                        ts = TimeSpan.FromSeconds(double.Parse(textBoxKeyFrame.Text));
                    }
                    catch
                    {
                    }
                }
                return ts;
            }
        }

        public TimeSpan? EncodingKeyframeInterval
        {
            get
            {
                if (checkBoxEncodingKeyFrameInterval.Checked && !string.IsNullOrWhiteSpace(textBoxEncodingKeyFrameInterval.Text))
                {
                    try
                    {
                        return TimeSpan.FromSeconds(double.Parse(textBoxEncodingKeyFrameInterval.Text));
                    }
                    catch
                    {
                        return null;
                    }
                }
                return null;
            }
        }

        public string PresetName => radioButtonCustomPreset.Checked ? textBoxCustomPreset.Text : defaultEncodingPreset;

        public bool Ignore708Captions => checkBoxIgnore708.Checked;

        public bool LiveTranscript
        {
            get => checkBoxEnableLiveTranscript.Checked;
            set => checkBoxEnableLiveTranscript.Checked = value;
        }

        public IList<LiveEventTranscription> LiveTranscriptionList
        {
            get
            {
                IList<LiveEventTranscription> transcriptionList = new List<LiveEventTranscription>
                {
                    new LiveEventTranscription()
                    {
                        Language = ((Item)comboBoxLanguage.SelectedItem).Value
                    }
                };

                return transcriptionList;
            }
        }

        public bool LiveEventLowLatencyV1orV2Mode
        {
            get => checkBoxLowLatency.Checked;
            set => checkBoxLowLatency.Checked = value;
        }

        public bool LiveEventLowLatencyV2
        {
            get => radioButtonLowLatencyV2.Checked;
        }

        public LiveEventInformation(Mainform mainform, AMSClientV3 client)
        {
            InitializeComponent();
            Icon = Bitmaps.Azure_Explorer_ico;
            MyMainForm = mainform;
            _client = client;
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

        private void LiveEventInformation_Load(object sender, EventArgs e)
        {
            // DpiUtils.InitPerMonitorDpi(this);

            linkLabelLiveTranscript.Links.Add(new LinkLabel.Link(0, linkLabelLiveTranscript.Text.Length, Constants.LinkMoreInfoLiveTranscript));

            LiveTranscriptLanguages.Languages.ForEach(c => comboBoxLanguage.Items.Add(new Item((new CultureInfo(c)).DisplayName, c)));
            comboBoxLanguage.SelectedIndex = 0;

            _radioButtonDefaultPreset = radioButtonDefaultPreset.Text;

            if (!MultipleSelection) // one channel
            {
                labelLEName.Text += MyLiveEvent.Data.Name;

                DGLiveEvent.ColumnCount = 2;

                // live event info
                DGLiveEvent.Columns[0].DefaultCellStyle.BackColor = Color.Gainsboro;
                DGLiveEvent.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_Name, MyLiveEvent.Data.Name);
                DGLiveEvent.Rows.Add("Id", MyLiveEvent.Data.Id);
                DGLiveEvent.Rows.Add("Location", MyLiveEvent.Data.Location.DisplayName);
                DGLiveEvent.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_State, (LiveEventResourceState)MyLiveEvent.Data.ResourceState);
                DGLiveEvent.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_Created, MyLiveEvent.Data.CreatedOn?.DateTime.ToLocalTime().ToString("G"));
                DGLiveEvent.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_LastModified, MyLiveEvent.Data.LastModifiedOn?.DateTime.ToLocalTime().ToString("G"));
                DGLiveEvent.Rows.Add(AMSExplorer.Properties.Resources.ChannelInformation_ChannelInformation_Load_Description, MyLiveEvent.Data.Description);
                DGLiveEvent.Rows.Add(AMSExplorer.Properties.Resources.ChannelInformation_ChannelInformation_Load_InputProtocol, MyLiveEvent.Data.Input.StreamingProtocol);

                pictureBoxLE.Image = LiveEventUtil.ReturnChannelBitmap(MyLiveEvent);

                if (MyLiveEvent.Data.Encoding != null)
                {
                    DGLiveEvent.Rows.Add("Live event type", MyLiveEvent.Data.Encoding.EncodingType);
                    DGLiveEvent.Rows.Add("Preset Name", MyLiveEvent.Data.Encoding.PresetName);

                    if (MyLiveEvent.Data.Encoding.KeyFrameInterval != null)
                    {
                        DGLiveEvent.Rows.Add("Encoding Key Frame Interval Duration", MyLiveEvent.Data.Encoding.KeyFrameInterval);

                    }
                }

                if (MyLiveEvent.Data.Input.KeyFrameIntervalDuration.HasValue)
                {
                    DGLiveEvent.Rows.Add("Input Key Frame Interval Duration", MyLiveEvent.Data.Input.KeyFrameIntervalDuration);
                }

                string[] stringnameurl = new string[] { AMSExplorer.Properties.Resources.ChannelInformation_ChannelInformation_Load_Primary, AMSExplorer.Properties.Resources.ChannelInformation_ChannelInformation_Load_Secondary };

                DGLiveEvent.Rows.Add("Vanity Url", MyLiveEvent.Data.UseStaticHostname);

                int i = 0;
                foreach (LiveEventEndpoint endpoint in MyLiveEvent.Data.Input.Endpoints)
                {
                    DGLiveEvent.Rows.Add(string.Format(AMSExplorer.Properties.Resources.ChannelInformation_ChannelInformation_Load_0InputURL1, MyLiveEvent.Data.Input.Endpoints.Count == 2 ? stringnameurl[i] : string.Empty, endpoint.Protocol), endpoint.Uri);
                    if (MyLiveEvent.Data.Input.StreamingProtocol == LiveEventInputProtocol.FragmentedMp4)
                    {
                        DGLiveEvent.Rows.Add(string.Format(AMSExplorer.Properties.Resources.ChannelInformation_ChannelInformation_Load_0InputURL1SSL, MyLiveEvent.Data.Input.Endpoints.Count == 2 ? stringnameurl[i] : string.Empty, endpoint.Protocol), endpoint.Uri.ToString().Replace("http://", "https://"));
                    }
                    i++;
                }
                if (i == 0)
                {
                    DGLiveEvent.Rows.Add("Input url(s)", "(None. Start the live event to get them ?)");
                }

                if (MyLiveEvent.Data.Preview != null)
                {
                    foreach (LiveEventEndpoint endpoint in MyLiveEvent.Data.Preview.Endpoints)
                    {
                        DGLiveEvent.Rows.Add(string.Format(AMSExplorer.Properties.Resources.ChannelInformation_ChannelInformation_Load_PreviewURL0, endpoint.Protocol), endpoint.Uri);
                    }
                }


                // live transcript
                if (MyLiveEvent.Data.Encoding.EncodingType == LiveEventEncodingType.PassthroughBasic)
                {

                }
                else if (MyLiveEvent.Data.Transcriptions != null && MyLiveEvent.Data.Transcriptions.Count > 0)
                {
                    DGLiveEvent.Rows.Add("Live Transcription", "Enabled");
                }
                else
                {
                    DGLiveEvent.Rows.Add("Live Transcription", "Disabled");
                }

            }
            else // multiselect
            {
                labelLEName.Text = AMSExplorer.Properties.Resources.ChannelInformation_ChannelInformation_Load_MultipleChannelsHaveBeenSelected;

                pictureBoxLE.Visible = false;

                tabControlLiveEvent.TabPages.Remove(tabPageLiveEventInfo); // no channel info page
                tabControlLiveEvent.TabPages.Remove(tabPagePreview); // no channel info page

                if (MyLiveEvent.Data.Input.KeyFrameIntervalDuration.HasValue)
                {
                    checkBoxKeyFrameIntDefined.Checked = true;
                    textBoxKeyFrame.Text = MyLiveEvent.Data.Input.KeyFrameIntervalDuration?.TotalSeconds.ToString();
                }
            }

            // comon code - multiselect or only one channel selected

            if (MyLiveEvent.Data.Encoding != null)
            {

                if (MyLiveEvent.Data.Encoding.KeyFrameInterval.HasValue)
                {
                    checkBoxEncodingKeyFrameInterval.Checked = true;
                    textBoxEncodingKeyFrameInterval.Text = MyLiveEvent.Data.Encoding.KeyFrameInterval?.TotalSeconds.ToString();
                }
                //  DGChannel.Rows.Add(AMSExplorer.Properties.Resources.ChannelInformation_ChannelInformation_Load_SlateSettings, AMSExplorer.Properties.Resources.ChannelInformation_ChannelInformation_Load_None);
            }

            if (MyLiveEvent.Data.Input.KeyFrameIntervalDuration.HasValue)
            {
                checkBoxKeyFrameIntDefined.Checked = true;
                textBoxKeyFrame.Text = MyLiveEvent.Data.Input.KeyFrameIntervalDuration?.TotalSeconds.ToString();
            }


            // live transcript
            if (MyLiveEvent.Data.Encoding.EncodingType == LiveEventEncodingType.PassthroughBasic)
            {
                tabControlLiveEvent.TabPages.Remove(tabPageLiveTranscript);
            }
            else if (MyLiveEvent.Data.Transcriptions != null && MyLiveEvent.Data.Transcriptions.Count > 0)
            {
                checkBoxEnableLiveTranscript.Checked = true;

                foreach (LiveEventTranscription transcript in MyLiveEvent.Data.Transcriptions)
                {
                    DGLiveEvent.Rows.Add("Live Transcription language", transcript.Language);
                }


                int index = 0;
                foreach (var c in comboBoxLanguage.Items)
                {
                    if (((Item)c).Value == MyLiveEvent.Data.Transcriptions.First().Language)
                    {
                        index = comboBoxLanguage.Items.IndexOf(c);
                    }
                }
                comboBoxLanguage.SelectedIndex = index;
            }
            else
            {
                checkBoxEnableLiveTranscript.Checked = false;
            }

            // low latency
            if (MyLiveEvent.Data.StreamOptions != null && (MyLiveEvent.Data.StreamOptions.Contains(StreamOptionsFlag.LowLatency) || MyLiveEvent.Data.StreamOptions.Contains(StreamOptionsFlag.LowLatencyV2)))
            {
                checkBoxLowLatency.Checked = true;
                radioButtonLowLatencyV2.Checked = MyLiveEvent.Data.StreamOptions.Contains(StreamOptionsFlag.LowLatencyV2);
            }

            // encoding settings
            if (MyLiveEvent.Data.Encoding.EncodingType == LiveEventEncodingType.PassthroughStandard || MyLiveEvent.Data.Encoding.EncodingType == LiveEventEncodingType.PassthroughBasic)
            {
                // Low latency v2 should be removed for pass through event
                radioButtonLowLatencyV2.Visible = false;
                radioButtonLowLatencyV1.Checked = true;

                textBoxEncodingKeyFrameInterval.Enabled = false;
                checkBoxEncodingKeyFrameInterval.Enabled = false;
                tabControlLiveEvent.TabPages.Remove(tabPageEncoding); // no encoding channel
            }

            if (MyLiveEvent.Data.Input != null && MyLiveEvent.Data.Input.IPAllowedIPs != null)
            {
                checkBoxInputSet.Checked = true;
                foreach (IPRange endpoint in MyLiveEvent.Data.Input.IPAllowedIPs)
                {
                    InputEndpointSettingList.Add(endpoint);
                }
            }

            dataGridViewInputIP.DataSource = InputEndpointSettingList;
            dataGridViewInputIP.DataError += new DataGridViewDataErrorEventHandler(dataGridView_DataError);

            if (MyLiveEvent.Data.Preview != null && MyLiveEvent.Data.Preview.IPAllowedIPs != null)
            {
                checkBoxPreviewSet.Checked = true;
                foreach (IPRange endpoint in MyLiveEvent.Data.Preview.IPAllowedIPs)
                {
                    PreviewEndpointSettingList.Add(endpoint);
                }
            }

            dataGridViewPreviewIP.DataSource = PreviewEndpointSettingList;
            dataGridViewPreviewIP.DataError += new DataGridViewDataErrorEventHandler(dataGridView_DataError);


            if (MyLiveEvent.Data.CrossSiteAccessPolicies != null)
            {
                if (MyLiveEvent.Data.CrossSiteAccessPolicies.ClientAccessPolicy != null)
                {
                    checkBoxclientpolicy.Checked = true;
                    textBoxClientPolicy.Text = MyLiveEvent.Data.CrossSiteAccessPolicies.ClientAccessPolicy;
                }
                if (MyLiveEvent.Data.CrossSiteAccessPolicies.CrossDomainPolicy != null)
                {
                    checkBoxcrossdomains.Checked = true;
                    textBoxCrossDomPolicy.Text = MyLiveEvent.Data.CrossSiteAccessPolicies.CrossDomainPolicy;
                }
            }
            textboxchannedesc.Text = MyLiveEvent.Data.Description;


            // Channel is stopped We can update some settings
            if (MyLiveEvent.Data.ResourceState == LiveEventResourceState.Stopped)
            {
                tabControlLiveEvent.Text += " (editable)";
                tabPageEncoding.Text += " (editable)";
                tabPageAdvanced.Text += " (editable)";
            }
            else if (MyLiveEvent.Data.ResourceState == LiveEventResourceState.Running)
            {
                groupBoxEncoding.Enabled = false;
                checkBoxIgnore708.Enabled = false;
                panelAdvanced.Enabled = false;
            }
            else // Channel is not stopped or running
            {
                labelLiveEventStoppedOrStartedSettings.Visible = true;
                buttonUpdateClose.Enabled = false;

                groupBoxEncoding.Enabled = false;
                checkBoxIgnore708.Enabled = false;
                panelAdvanced.Enabled = false;
            }


            // let's track when user edit a setting
            Modifications = new ExplorerLiveEventModifications
            {
                Description = false,
                ClientAccessPolicy = false,
                CrossDomainPolicy = false,
                InputIPAllowList = false,
                InputKeyFrameInterval = false,
                PreviewIPAllowList = false,
                SystemPreset = false,
                Ignore708Captions = false,
                LiveTranscription = false,
                LowLatency = false
            };
        }

        private void dataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

            MessageBox.Show("Wrong format");
        }

        private void contextMenuStripDG_Opening(object sender, CancelEventArgs e)
        {

        }

        private void LiveEventInformation_FormClosed(object sender, FormClosedEventArgs e)
        {
            // let's sure we dispose the webbrowser control
            //webBrowserPreview. = null;
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
        private async void LiveEventInformation_Shown(object sender, EventArgs e)
        {
            Telemetry.TrackPageView(this.Name);

            // We specify a env folder otherwise webview cannot create a cache in program files and crashes....
            var env = await CoreWebView2Environment.CreateAsync(null, Constants.webViewCachePath);
            await webBrowserPreview.EnsureCoreWebView2Async(env);
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
            checkInputKeyFrameValue();
            Modifications.InputKeyFrameInterval = true;
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
            PreviewEndpointSettingList.Clear();
            PreviewEndpointSettingList.Add(new IPRange() { Name = AMSExplorer.Properties.Resources.ChannelInformation_buttonAllowAllInputIP_Click_AllowAll, Address = IPAddress.Parse("0.0.0.0"), SubnetPrefixLength = 0 });
            checkBoxPreviewSet.Checked = true;
            Modifications.PreviewIPAllowList = true;
        }

        private async void tabPage4_Enter(object sender, EventArgs e)
        {
            if (MyLiveEvent.Data.ResourceState == LiveEventResourceState.Running && MyLiveEvent.Data.Preview != null && MyLiveEvent.Data.Preview.Endpoints.FirstOrDefault().Uri != null)
            {
                string myurl = await AssetTools.DoPlayBackWithStreamingEndpointAsync(
                            typeplayer: PlayerType.AzureMediaPlayerFrame,
                            path: MyLiveEvent.Data.Preview.Endpoints.FirstOrDefault().Uri.ToString(),
                            DoNotRewriteURL: true,
                            client: _client,
                            formatamp: AzureMediaPlayerFormats.Auto,
                            UISelectSEFiltersAndProtocols: false,
                            mainForm: MyMainForm,
                            //selectedBrowser: Constants.BrowserIE[1],
                            launchbrowser: false
                            );

                webBrowserPreview.Source = new Uri(myurl.Replace("https://", "http://"));
            }
        }

        private void tabPage4_Leave(object sender, EventArgs e)
        {
            webBrowserPreview.Source = new Uri("about:blank");
        }

        private void textBoxKeyFrame_TextChanged(object sender, EventArgs e)
        {
            checkInputKeyFrameValue();
            Modifications.InputKeyFrameInterval = true;
        }

        private void textBoxEncodingKeyFrameInterval_TextChanged(object sender, EventArgs e)
        {
            checkEncodingKeyFrameValue();
            Modifications.EncodingKeyFrameInterval = true;
        }

        private void checkInputKeyFrameValue()
        {
            if (checkBoxKeyFrameIntDefined.Checked && InputKeyframeIntervalSerialized == null)
            {
                errorProvider1.SetError(textBoxKeyFrame, AMSExplorer.Properties.Resources.ChannelInformation_checkKeyFrameValue_ValueIsNotValid);
            }
            else
            {
                errorProvider1.SetError(textBoxKeyFrame, string.Empty);
            }
        }

        private void checkEncodingKeyFrameValue()
        {
            if (checkBoxEncodingKeyFrameInterval.Checked && EncodingKeyframeInterval == null)
            {
                errorProvider1.SetError(textBoxEncodingKeyFrameInterval, AMSExplorer.Properties.Resources.ChannelInformation_checkKeyFrameValue_ValueIsNotValid);
            }
            else
            {
                errorProvider1.SetError(textBoxEncodingKeyFrameInterval, string.Empty);
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
                AMSEXPlorerLiveProfile.LiveProfile profileliveselected = AMSEXPlorerLiveProfile.Profiles.Where(p => p.Name == encodingprofile).FirstOrDefault();
                if (profileliveselected != null)
                {
                    dataGridViewVideoProf.DataSource = profileliveselected.Video;
                    List<AMSEXPlorerLiveProfile.LiveAudioProfile> profmultiaudio = new()
                    {
                        new AMSEXPlorerLiveProfile.LiveAudioProfile() { Language = "und", Bitrate = profileliveselected.Audio.Bitrate, Channels = profileliveselected.Audio.Channels, Codec = profileliveselected.Audio.Codec, SamplingRate = profileliveselected.Audio.SamplingRate }
                    };

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
            if (MyLiveEvent.Data.Encoding.EncodingType != LiveEventEncodingType.PassthroughStandard && MyLiveEvent.Data.Encoding.EncodingType != LiveEventEncodingType.PassthroughBasic)
            {
                return radioButtonCustomPreset.Checked ? textBoxCustomPreset.Text : defaultEncodingPreset;
            }
            else
            {
                return null;
            }
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

        private void webBrowserPreview_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void LiveEventInformation_DpiChanged(object sender, DpiChangedEventArgs e)
        {
            // for controls which are not using the default font
            // DpiUtils.UpdatedSizeFontAfterDPIChange(new List<Control> { labelLEName, contextMenuStripDG }, e, this);
        }

        private void checkBoxEncodingKeyFrameInterval_CheckedChanged(object sender, EventArgs e)
        {
            textBoxEncodingKeyFrameInterval.Enabled = checkBoxEncodingKeyFrameInterval.Checked;
            checkEncodingKeyFrameValue();
            Modifications.EncodingKeyFrameInterval = true;
        }

        private void checkBoxKeyFrameIntDefined_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxEnableLiveTranscript_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxLanguage.Enabled = checkBoxEnableLiveTranscript.Checked;
            Modifications.LiveTranscription = true;
        }

        private void comboBoxLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            Modifications.LiveTranscription = true;
        }

        private void checkBoxKeyFrameIntDefined_CheckedChanged_1(object sender, EventArgs e)
        {
            Modifications.InputKeyFrameInterval = true;
        }

        private void checkBoxLowLatency_CheckedChanged(object sender, EventArgs e)
        {
            Modifications.LowLatency = true;
            radioButtonLowLatencyV1.Enabled = radioButtonLowLatencyV2.Enabled = checkBoxLowLatency.Checked;
        }

        private void radioButtonLowLatencyV2_CheckedChanged(object sender, EventArgs e)
        {
            Modifications.LowLatency = true;
        }

        private void linkLabelLiveTranscript_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Send the URL to the operating system.
            var p = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = e.Link.LinkData as string,
                    UseShellExecute = true
                }
            };
            p.Start();
        }
    }

    public class ExplorerAudioStream
    {
        public int Index { get; set; }
        public string Language { get; set; }
        public string Code { get; set; }
    }

    public class ExplorerLiveEventModifications
    {
        public bool Description { get; set; }
        public bool InputKeyFrameInterval { get; set; }
        public bool EncodingKeyFrameInterval { get; set; }
        public bool SystemPreset { get; set; }
        public bool InputIPAllowList { get; set; }
        public bool PreviewIPAllowList { get; set; }
        public bool ClientAccessPolicy { get; set; }
        public bool CrossDomainPolicy { get; set; }
        public bool Ignore708Captions { get; set; }
        public bool LiveTranscription { get; set; }

        public bool LowLatency { get; set; }
    }
}
