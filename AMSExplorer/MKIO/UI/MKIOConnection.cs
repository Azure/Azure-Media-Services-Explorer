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


using Microsoft.IdentityModel.Tokens;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AMSExplorer
{
    public partial class MKIOConnection : Form
    {
        public string MKIOSubscriptionName
        {
            get => textSubscriptionName.Text;
            set => textSubscriptionName.Text = value;
        }

        public string MKIOToken
        {
            get => string.IsNullOrWhiteSpace(textMKToken.Text) ? null : textMKToken.Text;
            set => textMKToken.Text = value;
        }

        public MKIOConnection(string subscriptionName, string token)
        {
            InitializeComponent();

            MKIOSubscriptionName = subscriptionName;
            MKIOToken = token;

            textInstructions.Text = @"Instructions:

You must have a MediaKind I/O subscription set up and use the MediaKind I/O portal:
https://io.mediakind.com
- Set up a storage account in MKIO that has existing assets.
- This storage account's name must exactly match the Azure resource name, including case.
- The Subscription Name is the resource name in the MediaKind I/O portal.

To get a MKIO Token:
1) Log into https://io.mediakind.com with Microsoft SSO
2) Then use the URL: https://api.io.mediakind.com/auth/token/
3) Copy the token";
        }

        private void MKIOConnection_Load(object sender, System.EventArgs e)
        {

        }
        private void MKIOConnection_Shown(object sender, System.EventArgs e)
        {
            Telemetry.TrackPageView(this.Name);
        }

        private void buttonOk_Click(object sender, System.EventArgs e)
        {
            if (MKIOSubscriptionName.IsNullOrEmpty() || MKIOToken.IsNullOrEmpty())
            {
                this.DialogResult = DialogResult.None;
            }
        }
    }
}