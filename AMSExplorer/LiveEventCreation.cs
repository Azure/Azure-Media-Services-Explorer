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
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

namespace AMSExplorer
{
    public partial class LiveEventCreation : Form
    {
        private bool EncodingTabDisplayed = false;
        private bool InitPhase = true;
        private BindingList<ExplorerAudioStream> audiostreams = new BindingList<ExplorerAudioStream>();
        private string defaultLanguageString = "und";
        private AMSClientV3 _client;

        public string LiveEventName
        {
            get { return textboxchannelname.Text; }
            set { textboxchannelname.Text = value; }
        }

        public string LiveEventDescription
        {
            get { return textBoxDescription.Text; }
            set { textBoxDescription.Text = value; }
        }

        public bool VanityUrl
        {
            get { return checkBoxVanityUrl.Checked; }
            set { checkBoxVanityUrl.Checked = value; }
        }

        public bool LowLatencyMode
        {
            get { return checkBoxLowLatency.Checked; }
            set { checkBoxLowLatency.Checked = value; }
        }

        public LiveEventEncoding Encoding
        {
            get
            {
                LiveEventEncodingType type = LiveEventEncodingType.None;
                if (radioButtonTranscodingStd.Checked)
                {
                    type = LiveEventEncodingType.Standard;
                }
                else if (radioButtonTranscodingPremium.Checked)
                {
                    type = LiveEventEncodingType.Premium1080p;
                }

                LiveEventEncoding encodingoption = new LiveEventEncoding()
                {
                    PresetName = radioButtonCustomPreset.Checked ? textBoxCustomPreset.Text : null, // default preset or custom
                    EncodingType = type
                };

                return encodingoption;
            }
        }


        public LiveEventInputProtocol Protocol
        {
            get
            {
                return (LiveEventInputProtocol)(comboBoxProtocolInput.SelectedItem as Item).Value;
            }
        }

        public string KeyframeInterval
        {
            get
            {
                TimeSpan ts;
                if (checkBoxKeyFrameIntDefined.Checked)
                {
                    try
                    {
                        ts = TimeSpan.Parse(textBoxKeyFrame.Text);
                        return XmlConvert.ToString(ts);
                    }
                    catch
                    {
                        return null;
                    }

                }
                else
                {
                    return null;
                }
            }
            set
            {
                textBoxKeyFrame.Text = TimeSpan.Parse(value).ToString();
            }
        }


        public List<Microsoft.Azure.Management.Media.Models.IPRange> inputIPAllow
        {
            get
            {
                List<Microsoft.Azure.Management.Media.Models.IPRange> ips = new List<Microsoft.Azure.Management.Media.Models.IPRange>();
                Microsoft.Azure.Management.Media.Models.IPRange ip;

                try
                {
                    if (checkBoxRestrictIngestIP.Checked)
                    {
                        ip = new Microsoft.Azure.Management.Media.Models.IPRange() { Name = AMSExplorer.Properties.Resources.CreateLiveChannel_inputIPAllow_Default, Address = IPAddress.Parse(textBoxRestrictIngestIP.Text).ToString() };
                    }
                    else
                    {
                        ip = new Microsoft.Azure.Management.Media.Models.IPRange() { Name = AMSExplorer.Properties.Resources.ChannelInformation_buttonAllowAllInputIP_Click_AllowAll, Address = IPAddress.Parse("0.0.0.0").ToString(), SubnetPrefixLength = 0 };
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

        public List<Microsoft.Azure.Management.Media.Models.IPRange> previewIPAllow
        {
            get
            {
                List<Microsoft.Azure.Management.Media.Models.IPRange> ips = new List<Microsoft.Azure.Management.Media.Models.IPRange>();

                if (checkBoxRestrictPreviewIP.Checked)
                {
                    try
                    {
                        Microsoft.Azure.Management.Media.Models.IPRange ip = new Microsoft.Azure.Management.Media.Models.IPRange() { Name = AMSExplorer.Properties.Resources.CreateLiveChannel_inputIPAllow_Default, Address = IPAddress.Parse(textBoxRestrictPreviewIP.Text).ToString() };
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

        public bool StartLiveEventNow
        {
            get { return checkBoxStartChannel.Checked; }
            set { checkBoxStartChannel.Checked = value; }
        }

        public string AccessToken
        {
            get { return string.IsNullOrWhiteSpace(textBoxToken.Text) ? null : textBoxToken.Text; }
            set { textBoxToken.Text = value; }
        }

        public LiveEventCreation(AMSClientV3 client)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            _client = client;
        }

        private void CreateLiveChannel_Load(object sender, EventArgs e)
        {
            FillComboProtocols();

            tabControlLiveChannel.TabPages.Remove(tabPageLiveEncoding);
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


        private void comboBoxProtocolInput_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateLabelSyntax();
        }

        private void UpdateLabelSyntax()
        {
            string url = string.Empty;
            if (Protocol == LiveEventInputProtocol.RTMP)
            {
                if (checkBoxVanityUrl.Checked)
                {
                    url = "rtmp(s)://<live event name>-<ams account name>-<region abbrev name>.channel.media.azure.net:<port>/live/<access token>";
                }
                else
                {
                    url = "rtmp(s)://<random 128bit hex string>.channel.media.azure.net:<port>/live/<access token>";
                }
            }
            else // smooth
            {
                if (checkBoxVanityUrl.Checked)
                {
                    url = "http(s)://<live event name>-<ams account name>-<region abbrev name>.channel.media.azure.net/<access token>/ingest.isml";
                }
                else
                {
                    url = "http(s)://<random 128bit hex string>.channel.media.azure.net/<access token>/ingest.isml";
                }
            }

            if (!string.IsNullOrWhiteSpace(AccessToken))
            {
                url = url.Replace("<access token>", AccessToken);
            }

            url = url.Replace("<live event name>", LiveEventName);
            url = url.Replace("<ams account name>", _client.credentialsEntry.AccountName);

            labelUrlSyntax.Text = url;
        }

        private void FillComboProtocols()
        {
            comboBoxProtocolInput.Items.Clear();
            comboBoxProtocolInput.Items.Add(new Item(nameof(LiveEventInputProtocol.FragmentedMP4), nameof(LiveEventInputProtocol.FragmentedMP4)));
            comboBoxProtocolInput.Items.Add(new Item(nameof(LiveEventInputProtocol.RTMP), nameof(LiveEventInputProtocol.RTMP)));
            comboBoxProtocolInput.SelectedIndex = 1;
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



        internal static bool IsLiveEventNameValid(string name)
        {
            Regex reg = new Regex(@"^[a-zA-Z0-9]([a-zA-Z0-9-]{0,30}[a-zA-Z0-9])?$", RegexOptions.Compiled);
            return (reg.IsMatch(name));
        }


        private void checkIPAddress(TextBox tb)
        {
            bool Error = false;
            try
            {
                Microsoft.Azure.Management.Media.Models.IPRange ip = new Microsoft.Azure.Management.Media.Models.IPRange() { Name = AMSExplorer.Properties.Resources.CreateLiveChannel_inputIPAllow_Default, Address = IPAddress.Parse(tb.Text).ToString() };
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


        private void UpdateProfileGrids()
        {
            bool displayEncProfile = false;
            // string encodingprofile = ReturnLiveEncodingProfile();
            var myEncoding = Encoding;
            //   if (encodingprofile != null)
            if (radioButtonDefaultPreset.Checked && myEncoding.EncodingType != LiveEventEncodingType.None)
            {
                var profileliveselected = AMSEXPlorerLiveProfile.Profiles.Where(p => p.Type == myEncoding.EncodingType).FirstOrDefault();
                if (profileliveselected != null)
                {
                    dataGridViewVideoProf.DataSource = profileliveselected.Video;
                    List<AMSEXPlorerLiveProfile.LiveAudioProfile> profmultiaudio = new List<AMSEXPlorerLiveProfile.LiveAudioProfile>();

                    profmultiaudio.Add(new AMSEXPlorerLiveProfile.LiveAudioProfile() { Language = defaultLanguageString, Bitrate = profileliveselected.Audio.Bitrate, Channels = profileliveselected.Audio.Channels, Codec = profileliveselected.Audio.Codec, SamplingRate = profileliveselected.Audio.SamplingRate });

                    dataGridViewAudioProf.DataSource = profmultiaudio;
                    panelDisplayEncProfile.Visible = true;
                    displayEncProfile = true;
                }
            }
            if (!displayEncProfile)
            {
                dataGridViewVideoProf.DataSource = null;
                dataGridViewAudioProf.DataSource = null;
                panelDisplayEncProfile.Visible = false;
            }
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
            UpdateLabelSyntax();
        }

        private void checkChannelName()
        {
            TextBox tb = textboxchannelname;

            if (!IsLiveEventNameValid(tb.Text))
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

        private void textBoxIP_TextChanged(object sender, EventArgs e)
        {
            checkIPAddress((TextBox)sender);
        }


        private void radioButtonDefaultPreset_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void buttonGenerateToken_Click(object sender, EventArgs e)
        {
            textBoxToken.Text = Guid.NewGuid().ToString().Replace("-", string.Empty);
        }

        private void checkBoxKeyFrameIntDefined_CheckedChanged(object sender, EventArgs e)
        {
            textBoxKeyFrame.Enabled = checkBoxKeyFrameIntDefined.Checked;
        }

        private void radioButtonTranscodingNone_CheckedChanged(object sender, EventArgs e)
        {

            UpdateUIBasedOnLEMode(sender as RadioButton);
        }

        private void UpdateUIBasedOnLEMode(RadioButton radio)
        {
            if (!InitPhase && radio.Checked)
            {
                moreinfoLiveEncodingProfilelink.Visible = !(Encoding.EncodingType == LiveEventEncodingType.None);
                moreinfoLiveStreamingProfilelink.Visible = !moreinfoLiveEncodingProfilelink.Visible;

                // let's display the encoding tab if encoding has been choosen
                if (Encoding.EncodingType == LiveEventEncodingType.None)
                {
                    if (EncodingTabDisplayed)
                    {
                        tabControlLiveChannel.TabPages.Remove(tabPageLiveEncoding);
                        EncodingTabDisplayed = false;
                    }
                    FillComboProtocols();
                }
                else
                {
                    FillComboProtocols();
                    UpdateProfileGrids();
                    if (!EncodingTabDisplayed)
                    {
                        tabControlLiveChannel.TabPages.Add(tabPageLiveEncoding);
                        EncodingTabDisplayed = true;
                    }
                }
            }
        }

        private void checkBoxVanityUrl_CheckedChanged(object sender, EventArgs e)
        {
            UpdateLabelSyntax();
        }

        private void textBoxToken_TextChanged(object sender, EventArgs e)
        {
            UpdateLabelSyntax();
        }
    }
}
