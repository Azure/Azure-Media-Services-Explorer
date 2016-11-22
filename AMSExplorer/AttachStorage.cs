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
using System.Diagnostics;
using System.Xml;
using System.Xml.Linq;

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


        public string GetCertBody
        {
            get
            {
                return textBoxCertBody.Text;
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
            linkLabelAttach.Links.Add(new LinkLabel.Link(0, linkLabelAttach.Text.Length, "https://manage.windowsazure.com/publishsettings"));
            _credentials = credentials;


        }

        private void AttachStorage_Load(object sender, EventArgs e)
        {
            SampleStorageURLTemplate = (_credentials.UseOtherAPI) ?
                CredentialsEntry.CoreAttachStorageURL + _credentials.OtherAzureEndpoint : // "https://{0}.blob.core.chinacloudapi.cn/"
                CredentialsEntry.CoreAttachStorageURL + CredentialsEntry.GlobalAzureEndpoint; // "https://{0}.blob.core.windows.net"

            // let's poopulate the Azure Service Management URL field
            if (_credentials.UseOtherAPI)
            {
                textBoxServiceManagement.Text = CredentialsEntry.CoreServiceManagement + _credentials.OtherAzureEndpoint;
            }
            else if (_credentials.UsePartnerAPI)
            {
                textBoxServiceManagement.Text = AMSExplorer.Properties.Resources.AttachStorage_AttachStorage_Load_PleaseInsertAzureServiceManagementURLHere;
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

        private void buttonImportSubscriptionFile_Click(object sender, EventArgs e)
        {
            LoadSubscriptionFile();
        }

        private void LoadSubscriptionFile()
        {
            if (openFileDialogLoadSubFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var doc = new XDocument();
                    doc = XDocument.Load(openFileDialogLoadSubFile.FileName);
                    var subs = doc.Element("PublishData").Element("PublishProfile").Elements("Subscription");

                    if (subs.Count() == 0)
                    {
                        MessageBox.Show(AMSExplorer.Properties.Resources.AttachStorage_LoadSubscriptionFile_NoSubscriptionDataInTheFile, AMSExplorer.Properties.Resources.AMSLogin_buttonExport_Click_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        var subscription = doc.Element("PublishData").Element("PublishProfile").Element("Subscription");

                        if (subs.Count() > 1)
                        {
                            MessageBox.Show(string.Format(AMSExplorer.Properties.Resources.AttachStorage_LoadSubscriptionFile_ThereAreSeveralSubscriptionsDataInTheFileNNTheFirstEntry0WillBeUsed, subscription.Attribute("Name").Value), AMSExplorer.Properties.Resources.AttachStorage_LoadSubscriptionFile_SeveralSubscriptions, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        textBoxServiceManagement.Text = subscription.Attribute("ServiceManagementUrl").Value;
                        textBoxSubId.Text = subscription.Attribute("Id").Value;
                        textBoxCertBody.Text = subscription.Attribute("ManagementCertificate").Value;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(AMSExplorer.Properties.Resources.AttachStorage_LoadSubscriptionFile_ErrorWhenReadingTheFileOriginalError + ex.Message, AMSExplorer.Properties.Resources.AMSLogin_buttonExport_Click_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
