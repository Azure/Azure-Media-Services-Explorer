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


using Azure.ResourceManager.Media;
using Azure.ResourceManager.Media.Models;
using DocumentFormat.OpenXml.Drawing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AMSExplorer
{
    public partial class KeyDeliverySettings : Form
    {
        //private IAzureMediaServicesClient mediaClient;
        private readonly AMSClientV3 _amsClient;
        private BindingList<IpAddr> ipList = new();



        public KeyDeliverySettings(AMSClientV3 amsClient)
        {
            InitializeComponent();
            Icon = Bitmaps.Azure_Explorer_ico;
            _amsClient = amsClient;
        }

        private void KeyDeliverySettings_Load(object sender, EventArgs e)
        {
            // DpiUtils.InitPerMonitorDpi(this);

            try
            {
                //mediaClient = _amsClient.AMSclient;
                // Set the polling interval for long running operations to 2 seconds.
                // The default value is 30 seconds for the .NET client SDK
                mediaClient.LongRunningOperationRetryTimeout = 2;

                //mediaService =  mediaClient.Mediaservices.Get(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error when connecting", MessageBoxButtons.OK, MessageBoxIcon.Error);
                buttonAttach.Enabled = false;
                return;
            }

            if (_amsClient.AMSclient.Data.KeyDeliveryAccessControl != null)
            {
                if (_amsClient.AMSclient.Data.KeyDeliveryAccessControl.DefaultAction == IPAccessControlDefaultAction.Allow)
                {
                    checkBoxUseIpList.Checked = false;
                }
                else
                {
                    checkBoxUseIpList.Checked = true;
                    _amsClient.AMSclient.Data.KeyDeliveryAccessControl.IPAllowList.ToList().ForEach(i => ipList.Add(new IpAddr() { IpAddress=i.ToString() }));
                }
            }

            buttonAttach.Enabled = true;
            dataGridViewIP.DataSource = ipList;

        }


        public async Task UpdateKeyDeliveryConfigAsync()
        {
             // To restrict the client access and delivery of your content keys, set the key delivery accessControl ipAllowList. 

            MediaAccessControl maControl = new MediaAccessControl();

            if (checkBoxUseIpList.Checked)
            {
                // Allow or Deny access from the ipAllowList. If this is set to Allow, the ipAllowList should be empty.
                // List the IPv3 addresses to Allow or Deny based on the default action. 
                // "10.0.0.1/32", // you can use the CIDR IPv3 format,
                // "127.0.0.1"  or a single individual Ipv4 address as well.

                maControl.DefaultAction = IPAccessControlDefaultAction.Deny;
                foreach (var i in ipList)
                {
                    maControl.IPAllowList.Add(IPAddress.Parse(i.IpAddress));
                }
            }
            else // no restriction
            {
                // Allow or Deny access from the ipAllowList. If this is set to Allow, the ipAllowList should be empty.
                // List the IPv3 addresses to Allow or Deny based on the default action. 
                // "10.0.0.1/32", // you can use the CIDR IPv3 format,
                // "127.0.0.1"  or a single individual Ipv4 address as well.

                maControl.DefaultAction = IPAccessControlDefaultAction.Allow;               
            }


            MediaServicesAccountPatch patch = new MediaServicesAccountPatch()
            {
                KeyDeliveryAccessControl = maControl
            };
                            
            await _amsClient.AMSclient.UpdateAsync(Azure.WaitUntil.Completed, patch);
            // await mediaClient.Mediaservices.UpdateAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, msUpdate);
        }

        private void AttachStorage_DpiChanged(object sender, DpiChangedEventArgs e)
        {
            // DpiUtils.UpdatedSizeFontAfterDPIChange(labelAssetCopy, e);
        }

        private void AttachStorage_Shown(object sender, EventArgs e)
        {
            Telemetry.TrackPageView(this.Name);
        }

        private void buttonAddIP_Click(object sender, EventArgs e)
        {
            ipList.AddNew();
        }

        private void buttonDelIP_Click(object sender, EventArgs e)
        {
            if (dataGridViewIP.SelectedRows.Count == 1)
            {
                ipList.RemoveAt(dataGridViewIP.SelectedRows[0].Index);
            }
        }

        private void checkBoxUseIpList_CheckedChanged(object sender, EventArgs e)
        {
            dataGridViewIP.Enabled = buttonAddIP.Enabled = buttonDelIP.Enabled = checkBoxUseIpList.Checked;
        }
    }

    public class IpAddr
    {
        public string IpAddress { get; set; }
    }
}