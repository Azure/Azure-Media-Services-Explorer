//----------------------------------------------------------------------- 
// <copyright file="AddDynamicEncryption.cs" company="Microsoft">Copyright (c) Microsoft Corporation. All rights reserved.</copyright> 
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
using Microsoft.WindowsAzure.MediaServices.Client.ContentKeyAuthorization;
using Microsoft.WindowsAzure.MediaServices.Client.DynamicEncryption;

namespace AMSExplorer
{
    public partial class AddDynamicEncryptionFrame2 : Form
    {
        public ContentKeyRestrictionType? GetKeyRestrictionType
        {
            get
            {
                if (radioButtonOpenAuthPolicy.Checked)
                {
                    return ContentKeyRestrictionType.Open;
                }
                else if (radioButtonTokenAuthPolicy.Checked)
                {
                    return ContentKeyRestrictionType.TokenRestricted;
                }
                else // PlayReady but no license delivery from Azure Media Services
                {
                    return null;
                }
            }
        }

        



        public Uri GetAudienceUri
        {
            get
            {
                return new Uri(textBoxAudience.Text);
            }
        }
        public Uri GetIssuerUri
        {
            get
            {
                return new Uri(textBoxIssuer.Text);
            }
        }

       

        private CloudMediaContext _context;

        public AddDynamicEncryptionFrame2(CloudMediaContext context, bool IsLiveAsset, bool IsAES)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            _context = context;
            if (IsAES)
            {
                radioButtonNoAuthPolicy.Visible = radioButtonNoAuthPolicy.Enabled = false;
            }
        }



        private void SetupDynEnc_Load(object sender, EventArgs e)
        {

        }



        private void radioButtonToken_CheckedChanged(object sender, EventArgs e)
        {
            panelAutPol.Enabled = radioButtonTokenAuthPolicy.Checked;

        }

        
        private void buttonOk_Click(object sender, EventArgs e)
        {

        }

        private void radioButtonOpen_CheckedChanged(object sender, EventArgs e)
        {

        }

     
        private void radioButtonNoAuthPolicy_CheckedChanged(object sender, EventArgs e)
        {
           // if (radioButtonNoAuthPolicy.Checked) radioButtonKeySpecifiedByUser.Checked = true;
        }

    }
}
