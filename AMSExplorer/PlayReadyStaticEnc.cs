
//----------------------------------------------------------------------- 
// <copyright file="PlayReadyStaticEnc.cs" company="Microsoft">Copyright (c) Microsoft Corporation. All rights reserved.</copyright> 
// <license>
// Azure Media Services Explorer Ver. 3.0
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
using Microsoft.WindowsAzure.MediaServices.Client.ContentKeyAuthorization;
using Microsoft.WindowsAzure.MediaServices.Client.DynamicEncryption;

namespace AMSExplorer
{
    public partial class PlayReadyStaticEnc : Form
    {

        private readonly string _PlayReadyTestLAURL = "http://playready.directtaps.net/pr/svc/rightsmanager.asmx";
        private readonly string _PlayReadyTestKeySeed = "XVBovsmzhP9gRIZxWfFta3VVRPzVEWmJsazEJ46I";

        public string PlayReadyAssetName
        {
            get
            {
                return labelAssetName.Text;
            }
            set
            {
                labelAssetName.Text = value;
            }
        }
        public string PlayReadyOutputAssetName
        {
            get
            {
                return textboxoutputassetname.Text;
            }
            set
            {
                textboxoutputassetname.Text = value;
            }
        }

        public string PlayReadyKeySeed
        {
            get
            {
                return textBoxkeyseed.Text;
            }
            set
            {
                textBoxkeyseed.Text = value;
            }
        }
        public string PlayReadyLAurl
        {
            get
            {
                return textBoxLAurl.Text;
            }
            set
            {
                textBoxLAurl.Text = value;
            }
        }
        public string PlayReadyContentKey
        {
            get
            {
                return textBoxcontentkey.Text;
            }
            set
            {
                textBoxcontentkey.Text = value;
            }
        }
        public Guid PlayReadyKeyId
        {
            get
            {
                return new Guid(textBoxkeyid.Text);
            }
            set
            {
                textBoxkeyid.Text = value.ToString();
            }
        }

        public bool PlayReadyUseSencBox
        {
            get
            {
                return checkBoxuseSencBox.Checked;
            }
            set
            {
                checkBoxuseSencBox.Checked = value;
            }
        }
        public bool PlayReadyAdjustSubSamples
        {
            get
            {
                return checkBoxadjustSubSamples.Checked;
            }
            set
            {
                checkBoxadjustSubSamples.Checked = value;
            }
        }

        public string PlayReadyServiceId
        {
            get
            {
                return textBoxServiceID.Text;
            }
            set
            {
                textBoxServiceID.Text = value;
            }
        }

        public string PlayReadyCustomAttributes
        {
            get
            {
                return textBoxCustomAttributes.Text;
            }
            set
            {
                textBoxCustomAttributes.Text = value;
            }
        }


        public string PlayReadyProcessorName
        {
            get
            {
                return processorlabel.Text;
            }
            set
            {
                processorlabel.Text = value;
            }
        }

        public bool PlayReadyConfigureLicenseDelivery
        {
            get
            {
                return checkBoxDeliverLicenses.Checked;
            }
            set
            {
                checkBoxDeliverLicenses.Checked = value;
            }
        }

        public ContentKeyRestrictionType GetPlayReadyKeyRestrictionType
        {

            get
            {
                return (ContentKeyRestrictionType)(Enum.Parse(typeof(ContentKeyRestrictionType), (string)comboBoxKeyRestriction.SelectedItem));
            }

        }




        public PlayReadyStaticEnc()
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
        }

        private void buttonPlayReadyTestSettings_Click(object sender, EventArgs e)
        {
            textBoxLAurl.Text = _PlayReadyTestLAURL;
            textBoxkeyseed.Text = _PlayReadyTestKeySeed;
            textBoxkeyid.Text = Guid.NewGuid().ToString();

            textBoxcontentkey.Text = string.Empty;
        }


        private void buttonGenKeyID_Click_1(object sender, EventArgs e)
        {
            textBoxkeyid.Text = Guid.NewGuid().ToString();
        }

        private void moreinfotestserver_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData as string);
        }

        private void PlayReadyStaticEnc_Load(object sender, EventArgs e)
        {
            moreinfotestserver.Links.Add(new LinkLabel.Link(0, moreinfotestserver.Text.Length, "http://playready.directtaps.net/"));

            comboBoxKeyRestriction.Items.AddRange(Enum.GetNames(typeof(ContentKeyRestrictionType)).ToArray()); // key restriction
            comboBoxKeyRestriction.SelectedItem = Enum.GetName(typeof(ContentKeyRestrictionType), ContentKeyRestrictionType.Open);
        }

        private void buttongenerateContentKey_Click(object sender, EventArgs e)
        {
            textBoxcontentkey.Text = Convert.ToBase64String(DynamicEncryption.GetRandomBuffer(16));
            textBoxkeyseed.Text = string.Empty;

        }

        private void checkBoxDeliverLicenses_CheckedChanged(object sender, EventArgs e)
        {
            buttonPlayReadyTestSettings.Enabled = !checkBoxDeliverLicenses.Checked;
            textBoxLAurl.Enabled = !checkBoxDeliverLicenses.Checked;
            comboBoxKeyRestriction.Enabled = checkBoxDeliverLicenses.Checked;

            if (checkBoxDeliverLicenses.Checked)
            {
                textBoxLAurl.Text = string.Empty;
                textBoxkeyseed.Text = string.Empty;

            }
            buttonOk.Text = checkBoxDeliverLicenses.Checked ? "Define the PlayReady template" : (string)buttonOk.Tag;



        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            bool validation = false;


            if ((textBoxkeyid.Text != string.Empty) && ((textBoxkeyseed.Text != string.Empty) || (textBoxcontentkey.Text != string.Empty)))
            {
                validation = true;
            }

            buttonOk.Enabled = validation;
        }




        private void buttonOk_Click(object sender, EventArgs e)
        {

        }
    }
}
