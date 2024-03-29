﻿//----------------------------------------------------------------------------------------------
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
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace AMSExplorer
{
    public partial class StreamingEndpointInformation : Form
    {
        public StreamingEndpointResource MySE;
        public bool MultipleSelection = false;
        public ExplorerSEModifications Modifications = new();

        private string MaxCacheAgeInitial;
        private readonly BindingList<IPRange> endpointSettingList = new();
        private readonly BindingList<AkamaiSignatureHeaderAuthenticationKey> AkamaiSettingList = new();
        private readonly BindingList<HostNameClass> CustomHostNamesList = new()
        {
            AllowNew = true
        };

        public int GetScaleUnits
        {
            get
            {
                if (radioButtonPremium.Checked)
                {
                    return (int)numericUpDownRU.Value;
                }
                else
                {
                    return 0;
                }
            }
        }

        public IList<IPRange> GetStreamingAllowList => (checkBoxStreamingIPlistSet.Checked) ? endpointSettingList : null;

        public IList<AkamaiSignatureHeaderAuthenticationKey> GetStreamingAkamaiList => (checkBoxAkamai.Checked) ? AkamaiSettingList : null;

        public IList<string> GetStreamingCustomHostnames
        {
            get
            {
                IList<string> mylist = new List<string>();
                foreach (HostNameClass j in CustomHostNamesList)
                {
                    mylist.Add(j.HostName);
                }

                return mylist;
            }
        }

        public string GetOriginDescription => textboxorigindesc.Text;


        public long? MaxCacheAge
        {
            get
            {
                long? val;
                try
                {
                    val = Convert.ToInt64(textBoxMaxCacheAge.Text);
                }
                catch
                {
                    val = null;
                }
                return val;
            }

        }

        public IList<IPRange> IPv4AllowList => endpointSettingList;


        public string GetOriginClientPolicy => (checkBoxclientpolicy.Checked) ? textBoxClientPolicy.Text : null;

        public string GetOriginCrossdomaintPolicy => (checkBoxcrossdomain.Checked) ? textBoxCrossDomPolicy.Text : null;

        public StreamingEndpointInformation(StreamingEndpointResource streamingEndpoint)
        {
            InitializeComponent();
            Icon = Bitmaps.Azure_Explorer_ico;
            MySE = streamingEndpoint;
        }


        private void StreamingEndpointInformation_Load(object sender, EventArgs e)
        {
            // DpiUtils.InitPerMonitorDpi(this);

            moreinfoSE.Links.Add(new LinkLabel.Link(0, moreinfoSE.Text.Length, Constants.LinkMoreInfoSE));
            linkLabelAutoscale.Links.Add(new LinkLabel.Link(0, linkLabelAutoscale.Text.Length, Constants.LinkAutoscalePremiumSE));

            if (!MultipleSelection) // one SE
            {
                labelSEName.Text = string.Format(labelSEName.Text, MySE.Data.Name);
                hostnamelink.Links.Add(new LinkLabel.Link(0, hostnamelink.Text.Length, "http://msdn.microsoft.com/en-us/library/azure/dn783468.aspx"));
                DGOrigin.ColumnCount = 2;

                // se info
                DGOrigin.Columns[0].DefaultCellStyle.BackColor = Color.Gainsboro;
                DGOrigin.Rows.Add("Name", MySE.Data.Name);
                DGOrigin.Rows.Add("ResourceState", MySE.Data.ResourceState);
                DGOrigin.Rows.Add("Description", MySE.Data.Description);
                DGOrigin.Rows.Add("HostName", MySE.Data.HostName);
                DGOrigin.Rows.Add("IsCdnEnabled", MySE.Data.IsCdnEnabled);
                DGOrigin.Rows.Add("CDNProfile", MySE.Data.CdnProfile ?? Constants.stringNull);
                DGOrigin.Rows.Add("CDNProvider", MySE.Data.CdnProvider ?? Constants.stringNull);
                DGOrigin.Rows.Add("FreeTrialEndOn", MySE.Data.FreeTrialEndOn?.ToLocalTime().ToString("G"));
                DGOrigin.Rows.Add("CreatedOn", MySE.Data.CreatedOn?.DateTime.ToLocalTime().ToString("G"));
                DGOrigin.Rows.Add("LastModifiedOn", MySE.Data.LastModifiedOn?.DateTime.ToLocalTime().ToString("G"));
                DGOrigin.Rows.Add("Id", MySE.Data.Id);
                DGOrigin.Rows.Add("Location", MySE.Data.Location.DisplayName);
                DGOrigin.Rows.Add("ProvisioningState", MySE.Data.ProvisioningState);
            }
            else
            {
                labelSEName.Text = "(multiple streaming endpoints have been selected)";
                tabControl1.TabPages.Remove(tabPageInfo); // no SE info page
            }


            // Custom Hostnames binding to control
            if (MySE.Data.CustomHostNames != null)
            {
                foreach (string hostname in MySE.Data.CustomHostNames)
                {
                    CustomHostNamesList.Add(new HostNameClass() { HostName = hostname });
                }
            }
            dataGridViewCustomHostname.DataSource = CustomHostNamesList;

            // AZURE CDN
            panelCustomHostnames.Enabled = panelStreamingAllowedIP.Enabled = panelAkamai.Enabled = !(bool)MySE.Data.IsCdnEnabled;
            labelcdn.Visible = (bool)MySE.Data.IsCdnEnabled;

            // Streaming units

            if (!MultipleSelection)
            {
                StreamEndpointType type = ReturnTypeSE(MySE);
                DGOrigin.Rows.Add("Type", type);
                DGOrigin.Rows.Add("ScaleUnits", MySE.Data.ScaleUnits);


                if (type == StreamEndpointType.Standard)
                {
                    radioButtonStandard.Checked = true;
                }
                else // Premium
                {
                    radioButtonPremium.Checked = true;
                    numericUpDownRU.Value = (int)MySE.Data.ScaleUnits;
                }
            }
            else
            {
                groupBoxTypeScale.Enabled = false;
            }
            // if (numericUpDownRU.Maximum < MySE.ScaleUnits) numericUpDownRU.Maximum = (int)MySE.ScaleUnits * 2;


            if (MySE.Data.MaxCacheAge != null)
            {
                textBoxMaxCacheAge.Text = ((long)MySE.Data.MaxCacheAge).ToString();
            }
            else
            {
                textBoxMaxCacheAge.Text = string.Empty;
            }
            MaxCacheAgeInitial = textBoxMaxCacheAge.Text;


            if (MySE.Data.AccessControl != null)
            {
                if (MySE.Data.AccessControl.AllowedIPs != null)
                {
                    checkBoxStreamingIPlistSet.Checked = true;
                    foreach (IPRange endpoint in MySE.Data.AccessControl.AllowedIPs)
                    {
                        endpointSettingList.Add(endpoint);
                    }
                }
                if (MySE.Data.AccessControl.AkamaiSignatureHeaderAuthenticationKeyList != null)
                {
                    checkBoxAkamai.Checked = true;
                    foreach (AkamaiSignatureHeaderAuthenticationKey setting in MySE.Data.AccessControl.AkamaiSignatureHeaderAuthenticationKeyList)
                    {
                        AkamaiSettingList.Add(setting);
                    }
                }
            }
            dataGridViewIP.DataSource = endpointSettingList;
            dataGridViewAkamai.DataSource = AkamaiSettingList;
            dataGridViewIP.DataError += new DataGridViewDataErrorEventHandler(dataGridView_DataError);
            dataGridViewAkamai.DataError += new DataGridViewDataErrorEventHandler(dataGridView_DataError);

            if (MySE.Data.CrossSiteAccessPolicies != null)
            {
                if (MySE.Data.CrossSiteAccessPolicies.ClientAccessPolicy != null)
                {
                    checkBoxclientpolicy.Checked = true;
                    textBoxClientPolicy.Text = MySE.Data.CrossSiteAccessPolicies.ClientAccessPolicy;
                }
                if (MySE.Data.CrossSiteAccessPolicies.CrossDomainPolicy != null)
                {
                    checkBoxcrossdomain.Checked = true;
                    textBoxCrossDomPolicy.Text = MySE.Data.CrossSiteAccessPolicies.CrossDomainPolicy;
                }
            }
            textboxorigindesc.Text = MySE.Data.Description;

            checkMaxCacheAgeValue();

            // let's track when user edit a setting
            Modifications = new ExplorerSEModifications
            {
                Description = false,
                ClientAccessPolicy = false,
                CrossDomainPolicy = false,
                AkamaiSignatureHeaderAuthentication = false,
                CustomHostNames = false,
                MaxCacheAge = false,
                StreamingAllowedIPAddresses = false,
                StreamingUnits = false
            };
        }

        public static StreamEndpointType ReturnTypeSE(StreamingEndpointResource mySE)
        {
            if (mySE.Data.ScaleUnits > 0)
            {
                return StreamEndpointType.Premium;
            }
            else
            {
                return StreamEndpointType.Standard;
            }
        }


        public static string ReturnDisplayedProvider(string cdnprovider)
        {
            if (cdnprovider == null)
            {
                return null;
            }

            Item cdnp = StreamingEndpointCDNEnable.CDNProviders.Where(p => p.Value == cdnprovider).FirstOrDefault();
            if (cdnp != null)
            {
                return cdnp.Name;
            }
            else
            {
                return null;
            }
        }

        public enum StreamEndpointType
        {
            Standard = 0,
            Premium
        }

        private void dataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Wrong format");
        }


        private void ChanneltInformation_FormClosed(object sender, FormClosedEventArgs e)
        {

        }



        private void contextMenuStripOI_MouseClick(object sender, MouseEventArgs e)
        {
            ContextMenuStrip contextmenu = (ContextMenuStrip)sender;
            DataGridView DG = (DataGridView)contextmenu.SourceControl;

            if (DG.SelectedCells.Count == 1)
            {
                if (DG.SelectedCells[0].Value != null)
                {
                    System.Windows.Forms.Clipboard.SetText(DG.SelectedCells[0].Value.ToString());

                }
                else
                {
                    System.Windows.Forms.Clipboard.Clear();
                }
            }
        }


        private void buttonAddIP_Click(object sender, EventArgs e)
        {
            endpointSettingList.AddNew();
            Modifications.StreamingAllowedIPAddresses = true;
        }

        private void buttonDelIP_Click(object sender, EventArgs e)
        {
            if (dataGridViewIP.SelectedRows.Count == 1)
            {
                endpointSettingList.RemoveAt(dataGridViewIP.SelectedRows[0].Index);
                Modifications.StreamingAllowedIPAddresses = true;
            }
        }



        private void OriginInformation_Shown(object sender, EventArgs e)
        {
            Telemetry.TrackPageView(this.Name);
        }

        private void buttonAddAkamai_Click(object sender, EventArgs e)
        {
            AkamaiSettingList.AddNew();
            Modifications.AkamaiSignatureHeaderAuthentication = true;
        }

        private void buttonDelAkamai_Click(object sender, EventArgs e)
        {

            if (dataGridViewAkamai.SelectedRows.Count == 1)
            {
                AkamaiSettingList.RemoveAt(dataGridViewAkamai.SelectedRows[0].Index);
                Modifications.AkamaiSignatureHeaderAuthentication = true;
            }
        }



        private void checkBoxclientpolicy_CheckedChanged_1(object sender, EventArgs e)
        {
            textBoxClientPolicy.Enabled = buttonAddExampleClientPolicy.Enabled = checkBoxclientpolicy.Checked;
            Modifications.ClientAccessPolicy = true;
        }

        private void checkBoxcrossdomains_CheckedChanged_1(object sender, EventArgs e)
        {
            textBoxCrossDomPolicy.Enabled = buttonAddExampleCrossDomainPolicy.Enabled = checkBoxcrossdomain.Checked;
            Modifications.CrossDomainPolicy = true;
        }

        private void checkBoxStreamingIPlistSet_CheckedChanged(object sender, EventArgs e)
        {
            dataGridViewIP.Enabled = buttonAddIP.Enabled = buttonDelIP.Enabled = checkBoxStreamingIPlistSet.Checked;
            Modifications.StreamingAllowedIPAddresses = true;
        }

        private void checkBoxAkamai_CheckedChanged(object sender, EventArgs e)
        {
            dataGridViewAkamai.Enabled = buttonAddAkamai.Enabled = buttonDelAkamai.Enabled = checkBoxAkamai.Checked;
            Modifications.AkamaiSignatureHeaderAuthentication = true;
        }

        private void buttonAddHostName_Click(object sender, EventArgs e)
        {
            CustomHostNamesList.AddNew();
            Modifications.CustomHostNames = true;
        }

        private void buttonDelHostName_Click(object sender, EventArgs e)
        {
            if (dataGridViewCustomHostname.SelectedRows.Count == 1)
            {
                CustomHostNamesList.RemoveAt(dataGridViewCustomHostname.SelectedRows[0].Index);
                Modifications.CustomHostNames = true;
            }
        }

        private void hostnamelink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process p = new()
            {
                StartInfo = new ProcessStartInfo { FileName = e.Link.LinkData as string, UseShellExecute = true }
            };
            p.Start();
        }

        private void buttonAddExampleClientPolicy_Click(object sender, EventArgs e)
        {
            textBoxClientPolicy.Text = File.ReadAllText(Path.Combine(Mainform._configurationXMLFiles, @"ClientAccessPolicy.xml"));
        }

        private void buttonAddExampleCrossDomainPolicy_Click(object sender, EventArgs e)
        {
            textBoxCrossDomPolicy.Text = File.ReadAllText(Path.Combine(Mainform._configurationXMLFiles, @"CrossDomainPolicy.xml"));
        }

        private void numericUpDownRU_ValueChanged(object sender, EventArgs e)
        {
            Modifications.StreamingUnits = true;
        }


        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void buttonAllowAllStreamingIP_Click(object sender, EventArgs e)
        {
            checkBoxStreamingIPlistSet.Checked = false;
            endpointSettingList.Clear();
            Modifications.StreamingAllowedIPAddresses = true;
        }

        private void textBoxMaxCacheAge_TextChanged(object sender, EventArgs e)
        {
            checkMaxCacheAgeValue();
            Modifications.MaxCacheAge = true;
        }

        private void checkMaxCacheAgeValue()
        {
            if (!string.IsNullOrWhiteSpace(textBoxMaxCacheAge.Text) && MaxCacheAge == null)
            {
                errorProvider1.SetError(textBoxMaxCacheAge, "Value is not valid");
            }
            else
            {
                errorProvider1.SetError(textBoxMaxCacheAge, string.Empty);
            }
        }

        private void textboxorigindesc_TextChanged(object sender, EventArgs e)
        {
            Modifications.Description = true;
        }

        private void textBoxClientPolicy_TextChanged(object sender, EventArgs e)
        {
            Modifications.ClientAccessPolicy = true;
        }

        private void textBoxCrossDomPolicy_TextChanged(object sender, EventArgs e)
        {
            Modifications.CrossDomainPolicy = true;
        }

        private void radioButtonPremium_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownRU.Enabled = radioButtonPremium.Checked;
            Modifications.StreamingUnits = true;

        }

        private void radioButtonClassic_CheckedChanged(object sender, EventArgs e)
        {
            Modifications.StreamingUnits = true;

        }

        private void radioButtonStandard_CheckedChanged(object sender, EventArgs e)
        {
            Modifications.StreamingUnits = true;

        }

        private void moreinfoSE_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var p = new Process
            {
                StartInfo = new ProcessStartInfo { FileName = e.Link.LinkData as string, UseShellExecute = true }
            }; p.Start();

        }

        private void StreamingEndpointInformation_DpiChanged(object sender, DpiChangedEventArgs e)
        {
            // for controls which are not using the default font
            // DpiUtils.UpdatedSizeFontAfterDPIChange(new List<Control> { textBoxClientPolicy, textBoxCrossDomPolicy, labelSEName, contextMenuStripOI }, e, this);
        }
    }

    public class ExplorerSEModifications
    {
        public bool Description { get; set; }
        public bool StreamingUnits { get; set; }
        public bool MaxCacheAge { get; set; }
        public bool StreamingAllowedIPAddresses { get; set; }
        public bool AkamaiSignatureHeaderAuthentication { get; set; }
        public bool CustomHostNames { get; set; }
        public bool ClientAccessPolicy { get; set; }
        public bool CrossDomainPolicy { get; set; }
    }
}
