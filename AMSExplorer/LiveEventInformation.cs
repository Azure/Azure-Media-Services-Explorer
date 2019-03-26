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
using System.Windows.Forms;
using System.Net;
using Microsoft.Azure.Management.Media.Models;
using Microsoft.Azure.Management.Media;

namespace AMSExplorer
{
    public partial class LiveEventInformation : Form
    {
        public LiveEvent MyLiveEvent;
        public bool MultipleSelection = false;
        public ExplorerLiveEventModifications Modifications = new ExplorerLiveEventModifications();
        private BindingList<IPRange> InputEndpointSettingList = new BindingList<IPRange>();
        private BindingList<IPRange> PreviewEndpointSettingList = new BindingList<IPRange>();
        private Mainform MyMainForm;
        private AMSClientV3 _client;
        private string defaultEncodingPreset = null;
        private BindingList<ExplorerAudioStream> audiostreams = new BindingList<ExplorerAudioStream>();
        private string _radioButtonDefaultPreset;

        public IPAccessControl GetInputAllowList
        {
            get
            {
                var ipac = new IPAccessControl(InputEndpointSettingList);
                return (checkBoxInputSet.Checked) ? ipac : null;
            }
        }

        public IPAccessControl GetPreviewAllowList
        {
            get
            {
                var ipac = new IPAccessControl(PreviewEndpointSettingList);
                return (checkBoxPreviewSet.Checked) ? ipac : null;
            }
        }

        public string GetLiveEventDescription
        {
            get
            {
                return textboxchannedesc.Text;
            }
        }

        public string GetLiveEventClientPolicy
        {
            get { return (checkBoxclientpolicy.Checked) ? textBoxClientPolicy.Text : null; }
        }

        public string GetLiveEventCrossdomainPolicy
        {
            get { return (checkBoxcrossdomains.Checked) ? textBoxCrossDomPolicy.Text : null; }
        }

        public string KeyframeInterval
        {
            get
            {
                string ts = null;
                if (checkBoxKeyFrameIntDefined.Checked)
                {
                    try
                    {
                        ts = textBoxKeyFrame.Text;
                    }
                    catch
                    {
                    }
                }
                return ts;
            }
        }


        public string PresetName
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



        public LiveEventInformation(Mainform mainform, AMSClientV3 client)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
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
            _radioButtonDefaultPreset = radioButtonDefaultPreset.Text;

            if (!MultipleSelection) // one channel
            {
                labelLEName.Text += MyLiveEvent.Name;

                DGLiveEvent.ColumnCount = 2;

                // live event info
                DGLiveEvent.Columns[0].DefaultCellStyle.BackColor = Color.Gainsboro;
                DGLiveEvent.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_Name, MyLiveEvent.Name);
                DGLiveEvent.Rows.Add("Id", MyLiveEvent.Id);
                DGLiveEvent.Rows.Add("Location", MyLiveEvent.Location);
                DGLiveEvent.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_State, (LiveEventResourceState)MyLiveEvent.ResourceState);
                DGLiveEvent.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_Created, ((DateTime)MyLiveEvent.Created).ToLocalTime().ToString("G"));
                DGLiveEvent.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_LastModified, ((DateTime)MyLiveEvent.LastModified).ToLocalTime().ToString("G"));
                DGLiveEvent.Rows.Add(AMSExplorer.Properties.Resources.ChannelInformation_ChannelInformation_Load_Description, MyLiveEvent.Description);
                DGLiveEvent.Rows.Add(AMSExplorer.Properties.Resources.ChannelInformation_ChannelInformation_Load_InputProtocol, MyLiveEvent.Input.StreamingProtocol);

                if (MyLiveEvent.Encoding != null)
                {
                    DGLiveEvent.Rows.Add("Encoding Type", MyLiveEvent.Encoding.EncodingType);
                    DGLiveEvent.Rows.Add("Preset Name", MyLiveEvent.Encoding.PresetName);

                    //  DGChannel.Rows.Add(AMSExplorer.Properties.Resources.ChannelInformation_ChannelInformation_Load_SlateSettings, AMSExplorer.Properties.Resources.ChannelInformation_ChannelInformation_Load_None);
                }


                if (MyLiveEvent.Input.KeyFrameIntervalDuration != null)
                {
                    DGLiveEvent.Rows.Add("Key Frame Interval Duration", MyLiveEvent.Input.KeyFrameIntervalDuration);
                    checkBoxKeyFrameIntDefined.Checked = true;
                    textBoxKeyFrame.Text = MyLiveEvent.Input.KeyFrameIntervalDuration;
                }

                string[] stringnameurl = new string[] { AMSExplorer.Properties.Resources.ChannelInformation_ChannelInformation_Load_Primary, AMSExplorer.Properties.Resources.ChannelInformation_ChannelInformation_Load_Secondary };

                DGLiveEvent.Rows.Add("Vanity Url", MyLiveEvent.VanityUrl);

                int i = 0;
                foreach (var endpoint in MyLiveEvent.Input.Endpoints)
                {
                    DGLiveEvent.Rows.Add(string.Format(AMSExplorer.Properties.Resources.ChannelInformation_ChannelInformation_Load_0InputURL1, MyLiveEvent.Input.Endpoints.Count == 2 ? stringnameurl[i] : "", endpoint.Protocol), endpoint.Url);
                    if (MyLiveEvent.Input.StreamingProtocol == LiveEventInputProtocol.FragmentedMP4)
                    {
                        DGLiveEvent.Rows.Add(string.Format(AMSExplorer.Properties.Resources.ChannelInformation_ChannelInformation_Load_0InputURL1SSL, MyLiveEvent.Input.Endpoints.Count == 2 ? stringnameurl[i] : "", endpoint.Protocol), endpoint.Url.ToString().Replace("http://", "https://"));
                    }
                    i++;
                }
                if (i == 0)
                {
                    DGLiveEvent.Rows.Add("Input url(s)", "(None. Start the live event to get them ?)");
                }

                if (MyLiveEvent.Preview != null)
                {
                    foreach (var endpoint in MyLiveEvent.Preview.Endpoints)
                    {
                        DGLiveEvent.Rows.Add(string.Format(AMSExplorer.Properties.Resources.ChannelInformation_ChannelInformation_Load_PreviewURL0, endpoint.Protocol), endpoint.Url);
                    }
                }



                string mode = "Default";
                if (MyLiveEvent.StreamOptions != null && MyLiveEvent.StreamOptions.Contains(StreamOptionsFlag.LowLatency))
                {
                    mode = "Low latency";
                }
                DGLiveEvent.Rows.Add("Mode", mode);

            }
            else // multiselect
            {
                labelLEName.Text = AMSExplorer.Properties.Resources.ChannelInformation_ChannelInformation_Load_MultipleChannelsHaveBeenSelected;

                tabControl1.TabPages.Remove(tabPageLiveEventInfo); // no channel info page
                tabControl1.TabPages.Remove(tabPagePreview); // no channel info page

                if (MyLiveEvent.Input.KeyFrameIntervalDuration != null)
                {
                    checkBoxKeyFrameIntDefined.Checked = true;
                    textBoxKeyFrame.Text = MyLiveEvent.Input.KeyFrameIntervalDuration;
                }


            }

            // comon code - multiselect or only one channel selected

            // Encoding Settings
            if (false)//MyLiveEvent.Encoding != null && MyLiveEvent.Encoding.EncodingType != LiveEventEncodingType.None)
            {
                // default encoding profile name
                var profileliveselected = AMSEXPlorerLiveProfile.Profiles[0];// AMSEXPlorerLiveProfile.Profiles.Where(p => p.Type == MyChannel.EncodingType).FirstOrDefault();
                if (profileliveselected != null)
                {
                    defaultEncodingPreset = profileliveselected.Name;
                    radioButtonDefaultPreset.Text = string.Format(_radioButtonDefaultPreset, defaultEncodingPreset);
                }

                if (MyLiveEvent.Encoding != null && MyLiveEvent.Encoding.PresetName != null)
                {
                    if (MyLiveEvent.Encoding.PresetName == defaultEncodingPreset)
                    {
                        radioButtonDefaultPreset.Checked = true;
                    }
                    else
                    {
                        radioButtonCustomPreset.Checked = true;
                        textBoxCustomPreset.Text = MyLiveEvent.Encoding.PresetName;
                    }
                    //checkBoxIgnore708.Checked = MyChannel.Encoding..IgnoreCea708ClosedCaptions;
                }
                if (MyLiveEvent.ResourceState != LiveEventResourceState.Stopped)
                {
                    groupBoxEncoding.Enabled = false; // encoding settings cannot be edited
                    checkBoxIgnore708.Enabled = false;
                    labelLiveEventMustBeStopped.Visible = true;
                }


                UpdateProfileGrids();
            }
            else
            {
                tabControl1.TabPages.Remove(tabPageEncoding); // no encoding channel
            }


            if (MyLiveEvent.Input != null && MyLiveEvent.Input.AccessControl != null && MyLiveEvent.Input.AccessControl.Ip != null)
            {
                checkBoxInputSet.Checked = true;
                foreach (var endpoint in MyLiveEvent.Input.AccessControl.Ip.Allow)
                {
                    InputEndpointSettingList.Add(endpoint);
                }
            }

            dataGridViewInputIP.DataSource = InputEndpointSettingList;
            dataGridViewInputIP.DataError += new DataGridViewDataErrorEventHandler(dataGridView_DataError);

            if (MyLiveEvent.Preview != null && MyLiveEvent.Preview.AccessControl != null && MyLiveEvent.Preview.AccessControl.Ip != null)
            {
                checkBoxPreviewSet.Checked = true;
                foreach (var endpoint in MyLiveEvent.Preview.AccessControl.Ip.Allow)
                {
                    PreviewEndpointSettingList.Add(endpoint);
                }
            }

            dataGridViewPreviewIP.DataSource = PreviewEndpointSettingList;
            dataGridViewPreviewIP.DataError += new DataGridViewDataErrorEventHandler(dataGridView_DataError);


            if (MyLiveEvent.CrossSiteAccessPolicies != null)
            {
                if (MyLiveEvent.CrossSiteAccessPolicies.ClientAccessPolicy != null)
                {
                    checkBoxclientpolicy.Checked = true;
                    textBoxClientPolicy.Text = MyLiveEvent.CrossSiteAccessPolicies.ClientAccessPolicy;
                }
                if (MyLiveEvent.CrossSiteAccessPolicies.CrossDomainPolicy != null)
                {
                    checkBoxcrossdomains.Checked = true;
                    textBoxCrossDomPolicy.Text = MyLiveEvent.CrossSiteAccessPolicies.CrossDomainPolicy;
                }
            }
            textboxchannedesc.Text = MyLiveEvent.Description;

            // Channel is not stopped or running. We cannot update settings
            if (MyLiveEvent.ResourceState != LiveEventResourceState.Stopped && MyLiveEvent.ResourceState != LiveEventResourceState.Running)
            {
                labelLiveEventStoppedOrStartedSettings.Visible = true;
                buttonUpdateClose.Enabled = false;
            }

            // let's track when user edit a setting
            Modifications = new ExplorerLiveEventModifications
            {
                Description = false,
                ClientAccessPolicy = false,
                CrossDomainPolicy = false,
                InputIPAllowList = false,
                KeyFrameInterval = false,
                PreviewIPAllowList = false,
                SystemPreset = false,
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


        private void LiveEventInformation_FormClosed(object sender, FormClosedEventArgs e)
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


        private void LiveEventInformation_Shown(object sender, EventArgs e)
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


        private void buttonAllowAllInputIP_Click(object sender, EventArgs e)
        {
            InputEndpointSettingList.Clear();
            InputEndpointSettingList.Add(new IPRange() { Name = AMSExplorer.Properties.Resources.ChannelInformation_buttonAllowAllInputIP_Click_AllowAll, Address = IPAddress.Parse("0.0.0.0").ToString(), SubnetPrefixLength = 0 });
            checkBoxInputSet.Checked = true;
            Modifications.InputIPAllowList = true;
        }

        private void buttonAllowAllPreviewIP_Click(object sender, EventArgs e)
        {
            PreviewEndpointSettingList.Clear();
            PreviewEndpointSettingList.Add(new IPRange() { Name = AMSExplorer.Properties.Resources.ChannelInformation_buttonAllowAllInputIP_Click_AllowAll, Address = IPAddress.Parse("0.0.0.0").ToString(), SubnetPrefixLength = 0 });
            checkBoxPreviewSet.Checked = true;
            Modifications.PreviewIPAllowList = true;
        }

        private async void tabPage4_Enter(object sender, EventArgs e)
        {
            if (MyLiveEvent.ResourceState == LiveEventResourceState.Running && MyLiveEvent.Preview != null && MyLiveEvent.Preview.Endpoints.FirstOrDefault().Url != null)
            {
                string myurl = await AssetInfo.DoPlayBackWithStreamingEndpointAsync(
                            typeplayer: PlayerType.AzureMediaPlayerFrame,
                            path: MyLiveEvent.Preview.Endpoints.FirstOrDefault().Url,
                            DoNotRewriteURL: true,
                            client: _client ,
                            formatamp: AzureMediaPlayerFormats.Auto,
                            UISelectSEFiltersAndProtocols: false,
                            mainForm: MyMainForm,
                            //selectedBrowser: Constants.BrowserIE[1],
                            launchbrowser: false
                            );

                webBrowserPreview.Url = new Uri(myurl.Replace("https://", "http://"));
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

                    profmultiaudio.Add(new AMSEXPlorerLiveProfile.LiveAudioProfile() { Language = "und", Bitrate = profileliveselected.Audio.Bitrate, Channels = profileliveselected.Audio.Channels, Codec = profileliveselected.Audio.Codec, SamplingRate = profileliveselected.Audio.SamplingRate });

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
            if (MyLiveEvent.Encoding.EncodingType != LiveEventEncodingType.None)
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
        public bool KeyFrameInterval { get; set; }
        public bool SystemPreset { get; set; }

        public bool InputIPAllowList { get; set; }
        public bool PreviewIPAllowList { get; set; }
        public bool ClientAccessPolicy { get; set; }
        public bool CrossDomainPolicy { get; set; }
        public bool Ignore708Captions { get; set; }
    }
}
