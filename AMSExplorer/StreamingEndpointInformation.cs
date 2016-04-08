//----------------------------------------------------------------------------------------------
//    Copyright 2015 Microsoft Corporation
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
using Microsoft.WindowsAzure.MediaServices.Client;
using System.IO;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Blob.Protocol;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace AMSExplorer
{
    public partial class StreamingEndpointInformation : Form
    {
        public IStreamingEndpoint MyStreamingEndpoint;
        public bool MultipleSelection = false;
        public CloudMediaContext MyContext;
        private string MaxCacheAgeInitial;
        private BindingList<IPRange> endpointSettingList = new BindingList<IPRange>();
        private BindingList<AkamaiSignatureHeaderAuthenticationKey> AkamaiSettingList = new BindingList<AkamaiSignatureHeaderAuthenticationKey>();
        private BindingList<HostNameClass> CustomHostNamesList = new BindingList<HostNameClass>()
        {
            AllowNew = true
        };

        public int GetScaleUnits
        {
            get
            {
                return (int)numericUpDownRU.Value;
            }

        }


        public IList<IPRange> GetStreamingAllowList
        {
            get
            {
                return (checkBoxStreamingIPlistSet.Checked) ? endpointSettingList : null;
            }
        }

        public IList<AkamaiSignatureHeaderAuthenticationKey> GetStreamingAkamaiList
        {
            get
            {
                return (checkBoxAkamai.Checked) ? AkamaiSettingList : null;
            }
        }

        public IList<string> GetStreamingCustomHostnames
        {
            get
            {
                IList<string> mylist = new List<string>();
                foreach (var j in CustomHostNamesList)
                    mylist.Add(j.HostName);

                return mylist;
            }
        }

        public string GetOriginDescription
        {
            get
            {
                return textboxorigindesc.Text;
            }
        }


        public TimeSpan? MaxCacheAge
        {
            get
            {
                TimeSpan? ts;
                try
                {
                    ts = new TimeSpan(0, 0, Convert.ToInt32(textBoxMaxCacheAge.Text));
                }
                catch
                {
                    ts = null;
                }
                return ts;
            }

        }

        public IList<IPRange> IPv4AllowList
        {
            get
            {
                return endpointSettingList;
            }
        }


        public string GetOriginClientPolicy
        {
            get { return (checkBoxclientpolicy.Checked) ? textBoxClientPolicy.Text : null; }

        }

        public string GetOriginCrossdomaintPolicy
        {
            get { return (checkBoxcrossdomain.Checked) ? textBoxCrossDomPolicy.Text : null; }
        }

        public StreamingEndpointInformation()
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
        }


        private void StreamingEndpointInformation_Load(object sender, EventArgs e)
        {
            if (!MultipleSelection) // one SE
            {
                labelSEName.Text = string.Format(labelSEName.Text, MyStreamingEndpoint.Name);
                hostnamelink.Links.Add(new LinkLabel.Link(0, hostnamelink.Text.Length, "http://msdn.microsoft.com/en-us/library/azure/dn783468.aspx"));
                DGOrigin.ColumnCount = 2;

                // asset info
                DGOrigin.Columns[0].DefaultCellStyle.BackColor = Color.Gainsboro;
                DGOrigin.Rows.Add("Name", MyStreamingEndpoint.Name);
                DGOrigin.Rows.Add("Id", MyStreamingEndpoint.Id);
                DGOrigin.Rows.Add("State", (StreamingEndpointState)MyStreamingEndpoint.State);
                DGOrigin.Rows.Add("CDN Enabled", MyStreamingEndpoint.CdnEnabled);
                DGOrigin.Rows.Add("Created", ((DateTime)MyStreamingEndpoint.Created).ToLocalTime().ToString("G"));
                DGOrigin.Rows.Add("Last Modified", ((DateTime)MyStreamingEndpoint.LastModified).ToLocalTime().ToString("G"));
                DGOrigin.Rows.Add("Description", MyStreamingEndpoint.Description);
                DGOrigin.Rows.Add("Host name", MyStreamingEndpoint.HostName);
            }
            else
            {
                labelSEName.Text = "(multiple streaming endpoints have been selected)";
                tabControl1.TabPages.Remove( tabPageInfo); // no SE info page
                labeldesc.Visible = false; // description
                textboxorigindesc.Visible = false; // no description textbox
            }
            

            // Custom Hostnames binding to control
            if (MyStreamingEndpoint.CustomHostNames != null)
            {
                foreach (var hostname in MyStreamingEndpoint.CustomHostNames)
                {
                    CustomHostNamesList.Add(new HostNameClass() { HostName = hostname });
                }
            }
            dataGridViewCustomHostname.DataSource = CustomHostNamesList;

            // AZURE CDN
            panelCustomHostnames.Enabled = panelStreamingAllowedIP.Enabled = panelAkamai.Enabled = !MyStreamingEndpoint.CdnEnabled;
            labelcdn.Visible = MyStreamingEndpoint.CdnEnabled;
            numericUpDownRU.Minimum = MyStreamingEndpoint.CdnEnabled ? 1 : 0;

            if (MyStreamingEndpoint.ScaleUnits != null)
            {
                if (!MultipleSelection) DGOrigin.Rows.Add("Scale Units", MyStreamingEndpoint.ScaleUnits);
                if (numericUpDownRU.Maximum < MyStreamingEndpoint.ScaleUnits) numericUpDownRU.Maximum = (int)MyStreamingEndpoint.ScaleUnits * 2;
                numericUpDownRU.Value = (int)MyStreamingEndpoint.ScaleUnits;
            }

            if (MyStreamingEndpoint.CacheControl != null)
            {
                if (MyStreamingEndpoint.CacheControl.MaxAge != null)
                {
                    textBoxMaxCacheAge.Text = ((TimeSpan)MyStreamingEndpoint.CacheControl.MaxAge).TotalSeconds.ToString();
                }
                else
                {
                    textBoxMaxCacheAge.Text = string.Empty;
                }
            }
            else
            {
                textBoxMaxCacheAge.Text = string.Empty;
            }
            MaxCacheAgeInitial = textBoxMaxCacheAge.Text;


            if (MyStreamingEndpoint.AccessControl != null)
            {
                if (MyStreamingEndpoint.AccessControl.IPAllowList != null)
                {
                    checkBoxStreamingIPlistSet.Checked = true;
                    foreach (var endpoint in MyStreamingEndpoint.AccessControl.IPAllowList)
                    {
                        endpointSettingList.Add(endpoint);
                    }
                }
                if (MyStreamingEndpoint.AccessControl.AkamaiSignatureHeaderAuthenticationKeyList != null)
                {
                    checkBoxAkamai.Checked = true;
                    foreach (var setting in MyStreamingEndpoint.AccessControl.AkamaiSignatureHeaderAuthenticationKeyList)
                    {
                        AkamaiSettingList.Add(setting);
                    }
                }
            }
            dataGridViewIP.DataSource = endpointSettingList;
            dataGridViewAkamai.DataSource = AkamaiSettingList;
            dataGridViewIP.DataError += new DataGridViewDataErrorEventHandler(dataGridView_DataError);
            dataGridViewAkamai.DataError += new DataGridViewDataErrorEventHandler(dataGridView_DataError);

            if (MyStreamingEndpoint.CrossSiteAccessPolicies != null)
            {
                if (MyStreamingEndpoint.CrossSiteAccessPolicies.ClientAccessPolicy != null)
                {
                    checkBoxclientpolicy.Checked = true;
                    textBoxClientPolicy.Text = MyStreamingEndpoint.CrossSiteAccessPolicies.ClientAccessPolicy;
                }
                if (MyStreamingEndpoint.CrossSiteAccessPolicies.CrossDomainPolicy != null)
                {
                    checkBoxcrossdomain.Checked = true;
                    textBoxCrossDomPolicy.Text = MyStreamingEndpoint.CrossSiteAccessPolicies.CrossDomainPolicy;
                }
            }
            textboxorigindesc.Text = MyStreamingEndpoint.Description;

            checkMaxCacheAgeValue();
        }

        void dataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
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
        }

        private void buttonDelIP_Click(object sender, EventArgs e)
        {
            if (dataGridViewIP.SelectedRows.Count == 1)
            {
                endpointSettingList.RemoveAt(dataGridViewIP.SelectedRows[0].Index);
            }
        }



        private void OriginInformation_Shown(object sender, EventArgs e)
        {

        }

        private void buttonAddAkamai_Click(object sender, EventArgs e)
        {
            AkamaiSettingList.AddNew();
        }

        private void buttonDelAkamai_Click(object sender, EventArgs e)
        {

            if (dataGridViewAkamai.SelectedRows.Count == 1)
            {
                AkamaiSettingList.RemoveAt(dataGridViewAkamai.SelectedRows[0].Index);
            }
        }



        private void checkBoxclientpolicy_CheckedChanged_1(object sender, EventArgs e)
        {
            textBoxClientPolicy.Enabled = buttonAddExampleClientPolicy.Enabled = checkBoxclientpolicy.Checked;
        }

        private void checkBoxcrossdomains_CheckedChanged_1(object sender, EventArgs e)
        {
            textBoxCrossDomPolicy.Enabled = buttonAddExampleCrossDomainPolicy.Enabled = checkBoxcrossdomain.Checked;
        }

        private void checkBoxStreamingIPlistSet_CheckedChanged(object sender, EventArgs e)
        {
            dataGridViewIP.Enabled = buttonAddIP.Enabled = buttonDelIP.Enabled = checkBoxStreamingIPlistSet.Checked;
        }

        private void checkBoxAkamai_CheckedChanged(object sender, EventArgs e)
        {
            dataGridViewAkamai.Enabled = buttonAddAkamai.Enabled = buttonDelAkamai.Enabled = checkBoxAkamai.Checked;
        }

        private void buttonAddHostName_Click(object sender, EventArgs e)
        {
            CustomHostNamesList.AddNew();
        }

        private void buttonDelHostName_Click(object sender, EventArgs e)
        {
            if (dataGridViewCustomHostname.SelectedRows.Count == 1)
            {
                CustomHostNamesList.RemoveAt(dataGridViewCustomHostname.SelectedRows[0].Index);
            }
        }

        private void hostnamelink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData as string);
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


        }


        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void buttonAllowAllStreamingIP_Click(object sender, EventArgs e)
        {
            checkBoxStreamingIPlistSet.Checked = false;
            endpointSettingList.Clear();
        }

        private void textBoxMaxCacheAge_TextChanged(object sender, EventArgs e)
        {
            checkMaxCacheAgeValue();
        }

        private void checkMaxCacheAgeValue()
        {
            if (!string.IsNullOrWhiteSpace(textBoxMaxCacheAge.Text) && MaxCacheAge == null)
            {
                errorProvider1.SetError(textBoxMaxCacheAge, "Value is not valid");
            }
            else
            {
                errorProvider1.SetError(textBoxMaxCacheAge, String.Empty);
            }
        }
    }
}
