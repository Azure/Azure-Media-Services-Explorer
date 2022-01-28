//----------------------------------------------------------------------------------------------
//    Copyright 2022 Microsoft Corporation
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


using Microsoft.Azure.Management.Media;
using Microsoft.Azure.Management.Media.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AMSExplorer
{
    public partial class KeyDeliverySettings : Form
    {
        private IAzureMediaServicesClient mediaClient;
        private MediaService mediaService;
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

                mediaClient = _amsClient.AMSclient;
                // Set the polling interval for long running operations to 2 seconds.
                // The default value is 30 seconds for the .NET client SDK
                mediaClient.LongRunningOperationRetryTimeout = 2;

                mediaService = mediaClient.Mediaservices.Get(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error when connecting", MessageBoxButtons.OK, MessageBoxIcon.Error);
                buttonAttach.Enabled = false;
                return;
            }



            if (mediaService.KeyDelivery != null && mediaService.KeyDelivery.AccessControl != null)
            {
                if (mediaService.KeyDelivery.AccessControl.DefaultAction == DefaultAction.Allow)
                {
                    checkBoxUseIpList.Checked = false;
                }
                else
                {
                    checkBoxUseIpList.Checked = true;
                    mediaService.KeyDelivery.AccessControl.IpAllowList.ToList().ForEach(i => ipList.Add(new IpAddr() { IpAddress = i }));
                }
            }

            buttonAttach.Enabled = true;
            dataGridViewIP.DataSource = ipList;

        }


        public async Task UpdateKeyDeliveryConfigAsync()
        {
            KeyDelivery keyDel = new();  // To restrict the client access and delivery of your content keys, set the key delivery accessControl ipAllowList. 

            if (checkBoxUseIpList.Checked)
            {
                keyDel.AccessControl = new(
                    defaultAction: DefaultAction.Deny,  // Allow or Deny access from the ipAllowList. If this is set to Allow, the ipAllowList should be empty.
                    ipAllowList: ipList.Select(i => i.IpAddress).ToList()
                // List the IPv3 addresses to Allow or Deny based on the default action. 
                // "10.0.0.1/32", // you can use the CIDR IPv3 format,
                // "127.0.0.1"  or a single individual Ipv4 address as well.

                );
            }
            else // no restriction
            {
                keyDel.AccessControl = new(
                     defaultAction: DefaultAction.Allow,  // Allow or Deny access from the ipAllowList. If this is set to Allow, the ipAllowList should be empty.
                     ipAllowList: new List<string>()
                     {  // List the IPv3 addresses to Allow or Deny based on the default action. 
                        // "10.0.0.1/32", // you can use the CIDR IPv3 format,
                        // "127.0.0.1"  or a single individual Ipv4 address as well.
                     }
                 );
            }

            MediaServiceUpdate msUpdate = new() { KeyDelivery = keyDel };
            await mediaClient.Mediaservices.UpdateAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, msUpdate);
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