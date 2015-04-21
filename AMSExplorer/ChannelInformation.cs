//----------------------------------------------------------------------- 
// <copyright file="ChannelInformation.cs" company="Microsoft">Copyright (c) Microsoft Corporation. All rights reserved.</copyright> 
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
using System.Diagnostics;
using Microsoft.WindowsAzure.MediaServices.Client;
using System.IO;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Blob.Protocol;
using System.Web;
using System.Net;

namespace AMSExplorer
{
    public partial class ChannelInformation : Form
    {
        public IChannel MyChannel;
        public CloudMediaContext MyContext;
        private BindingList<IPRange> InputEndpointSettingList = new BindingList<IPRange>();
        private BindingList<IPRange> PreviewEndpointSettingList = new BindingList<IPRange>();

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

        public ChannelInformation()
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
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
            DGChannel.Rows.Add("Created", ((DateTime)MyChannel.Created).ToLocalTime());
            DGChannel.Rows.Add("Last Modified", ((DateTime)MyChannel.LastModified).ToLocalTime());
            DGChannel.Rows.Add("Description", MyChannel.Description);
            DGChannel.Rows.Add("Input protocol", MyChannel.Input.StreamingProtocol);


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
                buttonApplyClose.Enabled = true;
            }
        }

        private void buttonDelPreviewIP_Click(object sender, EventArgs e)
        {
            if (dataGridViewPreviewIP.SelectedRows.Count == 1)
            {
                PreviewEndpointSettingList.RemoveAt(dataGridViewPreviewIP.SelectedRows[0].Index);
                buttonApplyClose.Enabled = true;
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
        }

        private void checkBoxHLSFragPerSeg_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownHLSFragPerSeg.Enabled = checkBoxHLSFragPerSeg.Checked;

        }

        private void buttonAllowAllInputIP_Click(object sender, EventArgs e)
        {
            //  ip = new IPRange() { Name = "default", Address = IPAddress.Parse("0.0.0.0"), SubnetPrefixLength = 0 };
            InputEndpointSettingList.Clear();
            InputEndpointSettingList.Add(new IPRange() { Name = "default", Address = IPAddress.Parse("0.0.0.0"), SubnetPrefixLength = 0 });
            checkBoxInputSet.Checked = true;
        }

        private void buttonAllowAllPreviewIP_Click(object sender, EventArgs e)
        {
            checkBoxPreviewSet.Checked = false;
            PreviewEndpointSettingList.Clear();
        }

        private void tabPage4_Click(object sender, EventArgs e)
        {
            webBrowserPreview.Url = new Uri(@"http://aka.ms/azuremediaplayeriframe?url=http%3A%2F%2Fxpouyatdemo.streaming.mediaservices.windows.net%2F9453a0f6-b59a-473c-9b82-5e1c9701b95b%2FWP_20121015_081924Z.ism%2Fmanifest&autoplay=false");
        }

        private void tabPage4_Enter(object sender, EventArgs e)
        {
            if (MyChannel.State == ChannelState.Running && MyChannel.Preview.Endpoints.FirstOrDefault().Url.AbsoluteUri != null)
            {
                string myurl = AssetInfo.DoPlayBackWithBestStreamingEndpoint(typeplayer: PlayerType.AzureMediaPlayerFrame, Urlstr: MyChannel.Preview.Endpoints.FirstOrDefault().Url.ToString(), DoNotRewriteURL: true, context: MyContext, formatamp: AzureMediaPlayerFormats.Smooth, technology: AzureMediaPlayerTechnologies.Silverlight, launchbrowser: false);
                webBrowserPreview.Url = new Uri(myurl);
            }
        }

        private void tabPage4_Leave(object sender, EventArgs e)
        {
            webBrowserPreview.Url = null;
        }
    }

}
