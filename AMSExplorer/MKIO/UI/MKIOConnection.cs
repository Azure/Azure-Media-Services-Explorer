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
using System.Diagnostics;
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
            Icon = Bitmaps.Azure_Explorer_ico;

            MKIOSubscriptionName = subscriptionName;
            MKIOToken = token;

            linkLabelMKIO.Links.Add(new LinkLabel.Link(0, linkLabelMKIO.Text.Length, Constants.LinkMKIOPortal));
            linkLabelMigration.Links.Add(new LinkLabel.Link(0, linkLabelMigration.Text.Length, Constants.LinkMKIOMigrationDoc));

            textInstructions.Text = @"Instructions:

You must have a MediaKind I/O subscription set up and use the MK/IO portal:
https://io.mediakind.com
The Subscription Name is the resource name in the MK/IO portal.

To get a MK/IO Token:
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

        private void linkLabelMKIO_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var p = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = e.Link.LinkData as string,
                    UseShellExecute = true
                }
            };
            p.Start();
        }
    }
}