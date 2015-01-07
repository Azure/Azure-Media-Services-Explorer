//----------------------------------------------------------------------- 
// <copyright file="CreateLiveChannel.cs" company="Microsoft">Copyright (c) Microsoft Corporation. All rights reserved.</copyright> 
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
using Microsoft.WindowsAzure.MediaServices.Client;
using System.Net;

namespace AMSExplorer
{
    public partial class CreateLiveChannel : Form
    {
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

        public StreamingProtocol Protocol
        {
            get
            {
                return (StreamingProtocol)(Enum.Parse(typeof(StreamingProtocol), (string)comboBoxProtocol.SelectedItem));
            }
        }

        public short? HLSFragmentPerSegment
        {
            get
            {
                return checkBoxHLSFragPerSegDefined.Checked ? (short?) numericUpDownHLSFragPerSeg.Value : null;
            }
            set
            {
                if (value!=null)
                numericUpDownHLSFragPerSeg.Value = (short) value;
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
                    ip = new IPRange() { Name = "default", Address = IPAddress.Parse("0.0.0.0"), SubnetPrefixLength = 0 };
                }
                ips.Add(ip);
                return ips;
            }
        }


        public bool StartChannelNow
        {
            get { return checkBoxStartChannel.Checked; }
            set { checkBoxStartChannel.Checked = value; }
        }

        public CreateLiveChannel()
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
        }

        private void CreateLocator_Load(object sender, EventArgs e)
        {
            comboBoxProtocol.Items.AddRange(Enum.GetNames(typeof(StreamingProtocol)).ToArray()); // license type
            comboBoxProtocol.SelectedItem = Enum.GetName(typeof(StreamingProtocol), StreamingProtocol.FragmentedMP4);
            labelWarning.Text = string.Empty;
        }

        private void checkBoxRestrictIngestIP_CheckedChanged(object sender, EventArgs e)
        {
            textBoxRestrictIngestIP.Enabled = checkBoxRestrictIngestIP.Checked;
            if (!textBoxRestrictIngestIP.Enabled)
            {
                labelWarning.Text = string.Empty;
                buttonOk.Enabled = true;
            }
        }

        private void textBoxRestrictIngestIP_TextChanged(object sender, EventArgs e)
        {
            bool Error = false;

            try
            {
                IPRange ip = new IPRange() { Name = "default", Address = IPAddress.Parse(textBoxRestrictIngestIP.Text) };
            }
            catch
            {
                labelWarning.Text = "IP address incorrect";
                buttonOk.Enabled = false;
                Error = true;
            }
            if (!Error)
            {
                labelWarning.Text = string.Empty;
                buttonOk.Enabled = true;
            }
        }

        private void checkBoxHLSFragPerSegDefined_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownHLSFragPerSeg.Enabled = checkBoxHLSFragPerSegDefined.Checked;
        }

        private void checkBoxKeyFrameIntDefined_CheckedChanged(object sender, EventArgs e)
        {
            textBoxKeyFrame.Enabled = checkBoxKeyFrameIntDefined.Checked;
        }
    }
}
