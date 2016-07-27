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
using Microsoft.WindowsAzure.MediaServices.Client;
using Microsoft.WindowsAzure.MediaServices.Client.ContentKeyAuthorization;
using Microsoft.WindowsAzure.MediaServices.Client.DynamicEncryption;

namespace AMSExplorer
{
    public partial class AddDynamicEncryptionFrame3_CENCDelivery : Form
    {
        private readonly string _PlayReadyTestLAURL = "http://playready.directtaps.net/pr/svc/rightsmanager.asmx?PlayRight=1&UseSimpleNonPersistentLicense=1";
        private readonly string _PlayReadyTestKeySeed = "XVBovsmzhP9gRIZxWfFta3VVRPzVEWmJsazEJ46I";

     
        public Uri PlayReadyLAurl
        {
            get
            {
                Uri myuri = null;
                try
                {
                    myuri = new Uri(textBoxPRLAurl.Text);
                }
                catch
                {

                }

                return myuri;
            }
            set
            {
                textBoxPRLAurl.Text = value.ToString();
            }
        }

        public bool PlayReadyLAurlEncodeForSL
        {
            get
            {
                return checkBoxEncodingSL.Enabled ? checkBoxEncodingSL.Checked : false;
            }

        }

        public string WidevineLAurl
        {
            get
            {
                string myuri = null;
                if (_WidevinePackagingEnabled)
                {
                    myuri = textBoxWVLAurl.Text.Trim();
                }
                return myuri;
            }
            set
            {
                textBoxWVLAurl.Text = value.ToString();
            }
        }



        public int GetNumberOfAuthorizationPolicyOptionsPlayReady // if 0, then no authorization policy. If > 0, then renturn the number of options
        {
            get
            {
                if (radioButtonExternalPRServer.Checked && radioButtonExternalPRServer.Checked || !_PlayReadyPackagingEnabled)
                {
                    return 0;
                }
                else
                {
                    return (int)numericUpDownNbOptionsPlayReady.Value;
                }
            }
        }

        public int GetNumberOfAuthorizationPolicyOptionsWidevine // if 0, then no authorization policy. If > 0, then renturn the number of options
        {
            get
            {
                if ((radioButtonExternalWVServer.Checked && radioButtonExternalWVServer.Checked) || !_WidevinePackagingEnabled)
                {
                    return 0;
                }
                else
                {
                    return (int)numericUpDownNbOptionsWidevine.Value;
                }
            }
        }

        public bool WidevineFinalLAurl
        {
            get
            {
                return checkBoxWidevineFinalExtURL.Checked;
            }
        }


        private CloudMediaContext _context;
        private bool _PlayReadyPackagingEnabled;
        private bool _WidevinePackagingEnabled;

        public AddDynamicEncryptionFrame3_CENCDelivery(CloudMediaContext context, bool PlayReadyPackagingEnabled, bool WidevinePackagingEnabled)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            _context = context;
            _PlayReadyPackagingEnabled = PlayReadyPackagingEnabled;
            _WidevinePackagingEnabled = WidevinePackagingEnabled;
        }


        private void AddDynamicEncryptionFrame3_Load(object sender, EventArgs e)
        {
            groupBoxPlayReady.Enabled = _PlayReadyPackagingEnabled;
            groupBoxWidevine.Enabled = _WidevinePackagingEnabled;
        }


        private void buttonOk_Click(object sender, EventArgs e)
        {

        }

        private void buttonPlayReadyTestSettings_Click(object sender, EventArgs e)
        {
            textBoxPRLAurl.Text = _PlayReadyTestLAURL;

        }

        private void radioButtonExternalPRServer_CheckedChanged(object sender, EventArgs e)
        {
            panelExternalPlayReady.Enabled = radioButtonExternalPRServer.Checked;
            numericUpDownNbOptionsPlayReady.Enabled = !radioButtonExternalPRServer.Checked;
           
        }

        private void radioButtonExternalWVServer_CheckedChanged(object sender, EventArgs e)
        {
            panelExternalWidevine.Enabled = radioButtonExternalWVServer.Checked;
            numericUpDownNbOptionsPlayReady.Enabled = !radioButtonExternalWVServer.Checked;
        }
    }
}
