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

namespace AMSExplorer
{
    public partial class StreamingEndpointInformation : Form
    {
        public IStreamingEndpoint MyOrigin;
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
            get { return (checkBoxcrossdomains.Checked) ? textBoxCrossDomPolicy.Text : null; }

        }

        public StreamingEndpointInformation()
        {
            InitializeComponent();
        }



        private void OriginInformation_Load(object sender, EventArgs e)
        {
            labelOriginName.Text += MyOrigin.Name;
            hostnamelink.Links.Add(new LinkLabel.Link(0, hostnamelink.Text.Length, "http://msdn.microsoft.com/en-us/library/azure/dn783468.aspx"));


            DGOrigin.ColumnCount = 2;


            // asset info

            DGOrigin.Columns[0].DefaultCellStyle.BackColor = Color.Gainsboro;
            DGOrigin.Rows.Add("Name", MyOrigin.Name);

            DGOrigin.Rows.Add("Id", MyOrigin.Id);
            DGOrigin.Rows.Add("State", (StreamingEndpointState)MyOrigin.State);
            DGOrigin.Rows.Add("Created", ((DateTime)MyOrigin.Created).ToLocalTime());
            DGOrigin.Rows.Add("Last Modified", ((DateTime)MyOrigin.LastModified).ToLocalTime());
            DGOrigin.Rows.Add("Description", MyOrigin.Description);
            DGOrigin.Rows.Add("Host name", MyOrigin.HostName);




            // Custom Hostnames binding to control
            if (MyOrigin.CustomHostNames != null)
            {
                foreach (var hostname in MyOrigin.CustomHostNames)
                {
                    CustomHostNamesList.Add(new HostNameClass() { HostName = hostname });
                }
            }
            dataGridViewCustomHostname.DataSource = CustomHostNamesList;


            if (MyOrigin.ScaleUnits != null)
            {
                DGOrigin.Rows.Add("Scale Units", MyOrigin.ScaleUnits);
                if (numericUpDownRU.Maximum < MyOrigin.ScaleUnits) numericUpDownRU.Maximum = (int)MyOrigin.ScaleUnits * 2;
                numericUpDownRU.Value = (int)MyOrigin.ScaleUnits;
            }

            if (MyOrigin.CacheControl != null)
            {
                if (MyOrigin.CacheControl.MaxAge != null)
                {
                    textBoxMaxCacheAge.Text = ((TimeSpan)MyOrigin.CacheControl.MaxAge).TotalSeconds.ToString();
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


            if (MyOrigin.AccessControl != null)
            {
                if (MyOrigin.AccessControl.IPAllowList != null)
                {
                    checkBoxStreamingIPlistSet.Checked = true;
                    foreach (var endpoint in MyOrigin.AccessControl.IPAllowList)
                    {
                        endpointSettingList.Add(endpoint);
                    }
                }
                if (MyOrigin.AccessControl.AkamaiSignatureHeaderAuthenticationKeyList != null)
                {
                    checkBoxAkamai.Checked = true;
                    foreach (var setting in MyOrigin.AccessControl.AkamaiSignatureHeaderAuthenticationKeyList)
                    {
                        AkamaiSettingList.Add(setting);
                    }
                }
            }
            dataGridViewIP.DataSource = endpointSettingList;
            dataGridViewAkamai.DataSource = AkamaiSettingList;
            dataGridViewIP.DataError += new DataGridViewDataErrorEventHandler(dataGridView_DataError);
            dataGridViewAkamai.DataError += new DataGridViewDataErrorEventHandler(dataGridView_DataError);


            if (MyOrigin.CrossSiteAccessPolicies != null)
            {
                if (MyOrigin.CrossSiteAccessPolicies.ClientAccessPolicy != null)
                {
                    checkBoxclientpolicy.Checked = true;
                    textBoxClientPolicy.Text = MyOrigin.CrossSiteAccessPolicies.ClientAccessPolicy;
                }
                if (MyOrigin.CrossSiteAccessPolicies.CrossDomainPolicy != null)
                {
                    checkBoxcrossdomains.Checked = true;
                    textBoxCrossDomPolicy.Text = MyOrigin.CrossSiteAccessPolicies.CrossDomainPolicy;
                }
            }
            textboxorigindesc.Text = MyOrigin.Description;
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
            textBoxClientPolicy.Enabled = checkBoxclientpolicy.Checked;
        }

        private void checkBoxcrossdomains_CheckedChanged_1(object sender, EventArgs e)
        {
            textBoxCrossDomPolicy.Enabled = checkBoxcrossdomains.Checked;
        }

        private void checkBoxStreamingIPlistSet_CheckedChanged(object sender, EventArgs e)
        {
            dataGridViewIP.Enabled = checkBoxStreamingIPlistSet.Checked;
            buttonAddIP.Enabled = checkBoxStreamingIPlistSet.Checked;
            buttonDelIP.Enabled = checkBoxStreamingIPlistSet.Checked;
        }

        private void checkBoxAkamai_CheckedChanged(object sender, EventArgs e)
        {
            dataGridViewAkamai.Enabled = checkBoxAkamai.Checked;
            buttonAddAkamai.Enabled = checkBoxAkamai.Checked;
            buttonDelAkamai.Enabled = checkBoxAkamai.Checked;
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
    }
}
