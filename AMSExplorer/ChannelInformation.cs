﻿//----------------------------------------------------------------------------------------------
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
        private BindingList<IPRange> InputEndpointSettingList = new BindingList<IPRange>();
        private BindingList<IPRange> PreviewEndpointSettingList = new BindingList<IPRange>();
        private Mainform MyMainForm;
        private string defaultEncodingPreset = "";

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

        public string GetChannelCrossdomaintPolicy
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
            labelChannelName.Text += MyChannel.Name;
            DGChannel.ColumnCount = 2;

            // channel info
            DGChannel.Columns[0].DefaultCellStyle.BackColor = Color.Gainsboro;
            DGChannel.Rows.Add("Name", MyChannel.Name);
            DGChannel.Rows.Add("Id", MyChannel.Id);
            DGChannel.Rows.Add("State", (ChannelState)MyChannel.State);
            DGChannel.Rows.Add("Created", ((DateTime)MyChannel.Created).ToLocalTime().ToString("G"));
            DGChannel.Rows.Add("Last Modified", ((DateTime)MyChannel.LastModified).ToLocalTime().ToString("G"));
            DGChannel.Rows.Add("Description", MyChannel.Description);
            DGChannel.Rows.Add("Input protocol", MyChannel.Input.StreamingProtocol);
            DGChannel.Rows.Add("Encoding Type", MyChannel.EncodingType);

            if (MyChannel.Encoding != null)
            {
                DGChannel.Rows.Add("Encoding System Preset", MyChannel.Encoding.SystemPreset);
                DGChannel.Rows.Add("Encoding IgnoreCEA708", MyChannel.Encoding.IgnoreCea708ClosedCaptions);
                DGChannel.Rows.Add("Encoding Video Streams Count", MyChannel.Encoding.VideoStreams.Count);
                DGChannel.Rows.Add("Encoding Audio Streams Count", MyChannel.Encoding.AudioStreams.Count);
                DGChannel.Rows.Add("Encoding Ad Marker Source", (AdMarkerSource)MyChannel.Encoding.AdMarkerSource);

                if (MyChannel.Slate != null)
                {
                    DGChannel.Rows.Add("Default Slate Asset Id", MyChannel.Slate.DefaultSlateAssetId);
                    DGChannel.Rows.Add("Automatic Slate Insertion on AD signal", MyChannel.Slate.InsertSlateOnAdMarker);
                }
                else
                {
                    DGChannel.Rows.Add("Slate settings", "(none)");
                }
            }


            if (MyChannel.Input.KeyFrameInterval != null)
            {
                DGChannel.Rows.Add("Input KeyFrameInterval (s)", ((TimeSpan)MyChannel.Input.KeyFrameInterval).TotalSeconds);
                checkBoxKeyFrameIntDefined.Checked = true;
                textBoxKeyFrame.Text = ((TimeSpan)MyChannel.Input.KeyFrameInterval).TotalSeconds.ToString();
            }

            string[] stringnameurl = new string[] { "Primary ", "Secondary " };

            int i = 0;
            foreach (var endpoint in MyChannel.Input.Endpoints)
            {
                DGChannel.Rows.Add(string.Format("{0}Input URL ({1})", MyChannel.Input.Endpoints.Count == 2 ? stringnameurl[i] : "", endpoint.Protocol), endpoint.Url);
                if (MyChannel.Input.StreamingProtocol == StreamingProtocol.FragmentedMP4)
                {
                    DGChannel.Rows.Add(string.Format("{0}Input URL ({1}, SSL)", MyChannel.Input.Endpoints.Count == 2 ? stringnameurl[i] : "", endpoint.Protocol), endpoint.Url.ToString().Replace("http://", "https://"));
                }
                i++;
            }
            foreach (var endpoint in MyChannel.Preview.Endpoints)
            {
                DGChannel.Rows.Add(string.Format("Preview URL ({0})", endpoint.Protocol), endpoint.Url);
            }
            if (MyChannel.Output != null)
            {
                if (MyChannel.Output.Hls != null)
                {
                    if (MyChannel.Output.Hls.FragmentsPerSegment != null)
                    {
                        DGChannel.Rows.Add("Output HLS Fragments per segment", MyChannel.Output.Hls.FragmentsPerSegment);
                        checkBoxHLSFragPerSeg.Checked = true;
                        numericUpDownHLSFragPerSeg.Value = (int)MyChannel.Output.Hls.FragmentsPerSegment;
                    }
                }
            }



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
                }
                if (MyChannel.State != ChannelState.Stopped)
                {
                    groupBoxEncoding.Enabled = false; // encoding settings cannot be edited
                    labelChannelMustBeStopped.Visible = true;
                }
            }
            else
            {
                tabControl1.TabPages.Remove(tabPageEncoding); // no encoding channel
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
            if (MyChannel.State!= ChannelState.Stopped && MyChannel.State != ChannelState.Running) 
            {
                labelChannelStoppedOrStartedSettings.Visible = true;
                buttonUpdateClose.Enabled = false;
            }
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

        }



        private void toolStripMenuItemFilesCopyClipboard_Click(object sender, EventArgs e)
        {

        }

        private void buttonAddIngestIP_Click(object sender, EventArgs e)
        {
            InputEndpointSettingList.AddNew();
        }

        private void buttonAddPreviewIP_Click(object sender, EventArgs e)
        {
            PreviewEndpointSettingList.AddNew();
        }

        private void buttonDelIngestIP_Click(object sender, EventArgs e)
        {
            if (dataGridViewInputIP.SelectedRows.Count == 1)
            {
                InputEndpointSettingList.RemoveAt(dataGridViewInputIP.SelectedRows[0].Index);
            }
        }

        private void buttonDelPreviewIP_Click(object sender, EventArgs e)
        {
            if (dataGridViewPreviewIP.SelectedRows.Count == 1)
            {
                PreviewEndpointSettingList.RemoveAt(dataGridViewPreviewIP.SelectedRows[0].Index);
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
        }


        private void checkBoxclientpolicy_CheckedChanged_1(object sender, EventArgs e)
        {
            textBoxClientPolicy.Enabled = checkBoxclientpolicy.Checked;
        }

        private void checkBoxcrossdomains_CheckedChanged_1(object sender, EventArgs e)
        {
            textBoxCrossDomPolicy.Enabled = checkBoxcrossdomains.Checked;
        }

        private void dataGridViewInputIP_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {

        }

        private void checkBoxInputSet_CheckedChanged(object sender, EventArgs e)
        {
            dataGridViewInputIP.Enabled = checkBoxInputSet.Checked;
            buttonAddInputIP.Enabled = checkBoxInputSet.Checked;
            buttonDelInputIP.Enabled = checkBoxInputSet.Checked;
        }

        private void checkBoxKeyFrameIntDefined_CheckedChanged(object sender, EventArgs e)
        {
            textBoxKeyFrame.Enabled = checkBoxKeyFrameIntDefined.Checked;
            checkKeyFrameValue();
        }

        private void checkBoxHLSFragPerSeg_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownHLSFragPerSeg.Enabled = checkBoxHLSFragPerSeg.Checked;

        }

        private void buttonAllowAllInputIP_Click(object sender, EventArgs e)
        {
            InputEndpointSettingList.Clear();
            InputEndpointSettingList.Add(new IPRange() { Name = "Allow All", Address = IPAddress.Parse("0.0.0.0"), SubnetPrefixLength = 0 });
            checkBoxInputSet.Checked = true;
        }

        private void buttonAllowAllPreviewIP_Click(object sender, EventArgs e)
        {
            checkBoxPreviewSet.Checked = false;
            PreviewEndpointSettingList.Clear();
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
        }

        private void checkKeyFrameValue()
        {
            if (checkBoxKeyFrameIntDefined.Checked && KeyframeInterval == null)
            {
                errorProvider1.SetError(textBoxKeyFrame, "Value is not valid");
            }
            else
            {
                errorProvider1.SetError(textBoxKeyFrame, String.Empty);
            }
        }

        private void radioButtonCustomPreset_CheckedChanged(object sender, EventArgs e)
        {
            textBoxCustomPreset.Enabled = radioButtonCustomPreset.Checked;
        }


    }

}
