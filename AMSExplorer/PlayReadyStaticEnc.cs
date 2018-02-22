//----------------------------------------------------------------------------------------------
//    Copyright 2018 Microsoft Corporation
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
using Microsoft.WindowsAzure.MediaServices.Client.ContentKeyAuthorization;
using Microsoft.WindowsAzure.MediaServices.Client.DynamicEncryption;
using Microsoft.WindowsAzure.MediaServices.Client;

namespace AMSExplorer
{
    public partial class PlayReadyStaticEnc : Form
    {

        private readonly string _PlayReadyTestLAURL = "http://playready-testserver.azurewebsites.net/rightsmanager.asmx";
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

        private CloudMediaContext _context;


        public PlayReadyStaticEnc(CloudMediaContext context)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            _context = context;
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
            moreinfotestserver.Links.Add(new LinkLabel.Link(0, moreinfotestserver.Text.Length, "http://playready.azurewebsites.net/"));
        }

        private void buttongenerateContentKey_Click(object sender, EventArgs e)
        {
            textBoxcontentkey.Text = Convert.ToBase64String(DynamicEncryption.GetRandomBuffer(16));
            textBoxkeyseed.Text = string.Empty;

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

        private void buttonAzureSettings_Click(object sender, EventArgs e)
        {
            IContentKey key = _context.ContentKeys.Where(k => k.ContentKeyType == ContentKeyType.CommonEncryption).FirstOrDefault();
            if (key != null)
            {
                try
                {
                    Uri myUri = key.GetKeyDeliveryUrl(ContentKeyDeliveryType.PlayReadyLicense);
                    if (myUri != null) textBoxLAurl.Text = myUri.ToString();
                }
                catch
                {

                }
            }
            textBoxkeyseed.Text = string.Empty;
            textBoxkeyid.Text = Guid.NewGuid().ToString();
            textBoxcontentkey.Text = Convert.ToBase64String(DynamicEncryption.GetRandomBuffer(16));
        }
    }
}
