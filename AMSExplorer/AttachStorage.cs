//----------------------------------------------------------------------- 
// <copyright file="AttachStorage.cs" company="Microsoft">Copyright (c) Microsoft Corporation. All rights reserved.</copyright> 
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

namespace AMSExplorer
{
    public partial class AttachStorage : Form
    {
        private CredentialsEntry _credentials;
        private string SampleStorageURLTemplate;

        public string GetAzureSubscriptionID
        {
            get
            {
                return textBoxSubId.Text;
            }
        }


        public string GetCertThumbprint
        {
            get
            {
                return textBoxCertThumbprint.Text;
            }
        }

        public string GetAzureServiceManagementURL
        {
            get
            {
                return textBoxServiceManagement.Text;
            }
        }

        public string GetStorageKey
        {
            get
            {
                return textBoxStorageKey.Text;
            }

        }

        public string GetStorageName
        {
            get
            {
                return textBoxStorageName.Text;
            }

        }

        public string GetStorageEndpointURL
        {
            get
            {
                return textBoxStorageEndPoint.Text;
            }

        }


        public AttachStorage(CredentialsEntry credentials)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            linkLabelAttach.Links.Add(new LinkLabel.Link(0, linkLabelAttach.Text.Length, "http://msdn.microsoft.com/en-US/library/azure/gg551722.aspx"));
            _credentials = credentials;


        }

        private void AttachStorage_Load(object sender, EventArgs e)
        {
            SampleStorageURLTemplate = (_credentials.UseOtherAPI == true.ToString()) ?
                CredentialsEntry.CoreAttachStorageURL + _credentials.OtherAzureEndpoint.Split("|".ToCharArray())[0] : // "https://{0}.blob.core.chinacloudapi.cn/"
                CredentialsEntry.CoreAttachStorageURL + CredentialsEntry.GlobalAzureEndpoint; // "https://{0}.blob.core.windows.net"

            // let's poopulate the Azure Service Management URL field
            if (_credentials.UseOtherAPI == true.ToString())
            {
                textBoxServiceManagement.Text = CredentialsEntry.CoreServiceManagement + _credentials.OtherAzureEndpoint.Split("|".ToCharArray())[0];
            }
            else if (_credentials.UsePartnerAPI == true.ToString())
            {
                textBoxServiceManagement.Text = "Please insert Azure Service Management URL here";
            }
            else // Global Azure
            {
                textBoxServiceManagement.Text = CredentialsEntry.CoreServiceManagement + CredentialsEntry.GlobalAzureEndpoint;
            }

            UpdateEndPointURL();
        }


        private void linkLabelAttach_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData as string);
        }

        private void textBoxStorageName_TextChanged(object sender, EventArgs e)
        {
            UpdateEndPointURL();
            textBoxTXT_Validation(sender, e);
        }

        private void UpdateEndPointURL()
        {
            textBoxStorageEndPoint.Text = string.Format(SampleStorageURLTemplate, textBoxStorageName.Text);
        }


        private void textBoxURL_Validation(object sender, EventArgs e)
        {
            TextBox mytextbox = (TextBox)sender;
            mytextbox.BackColor = (Uri.IsWellFormedUriString(mytextbox.Text, UriKind.Absolute)) ? Color.White : Color.Pink;
        }

        private void textBoxTXT_Validation(object sender, EventArgs e)
        {
            TextBox mytextbox = (TextBox)sender;
            mytextbox.BackColor = (string.IsNullOrWhiteSpace(mytextbox.Text.Trim())) ? Color.Pink : Color.White;
        }
    }
}
