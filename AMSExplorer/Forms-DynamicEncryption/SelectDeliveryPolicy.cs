//----------------------------------------------------------------------------------------------
//    Copyright 2018 Microsoft Corporation
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


using Microsoft.WindowsAzure.Storage.Shared.Protocol;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAzure.MediaServices.Client;
using System.IO;
using Microsoft.WindowsAzure.MediaServices.Client.DynamicEncryption;
using Microsoft.WindowsAzure.MediaServices.Client.ContentKeyAuthorization;

namespace AMSExplorer
{
    public partial class SelectDeliveryPolicy : Form
    {
        private CloudMediaContext _context;
        private List<IAssetDeliveryPolicy> delPolicies;
        private AssetDeliveryPolicyType _delpoltype;

        public IAssetDeliveryPolicy SelectedPolicy
        {
            get
            {
                var policies = ReturnSelectedDeliveryPolicies();

                if (policies.Count > 0)
                {
                    return policies.FirstOrDefault();
                }
                else
                {
                    return null;
                }
            }
        }


        public SelectDeliveryPolicy(CloudMediaContext context, AssetDeliveryPolicyType delpoltype = AssetDeliveryPolicyType.None)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            _context = context;
            _delpoltype = delpoltype;
        }

        private void EncodingAMEStandardPickOverlay_Load(object sender, EventArgs e)
        {
            listViewPolicies.Tag = -1;
            listViewPolicies.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(ListViewItemComparerQuickNoDate.ListView_ColumnClick);

            ListPolicies();
        }

        private void ListPolicies()
        {
            this.Cursor = Cursors.WaitCursor;
            listViewPolicies.Items.Clear();
            DGDelPol.Rows.Clear();

            //  autPolicies = delPolicies = _context.AssetDeliveryPolicies.ToList().Where(p => p.AssetDeliveryPolicyType == _delpoltype).ToList();
            if (_delpoltype != AssetDeliveryPolicyType.None)
            {
                delPolicies = _context.AssetDeliveryPolicies.Where(p => p.AssetDeliveryPolicyType == _delpoltype).ToList();

            }
            else
            {
                delPolicies = _context.AssetDeliveryPolicies.ToList();
            }

            listViewPolicies.BeginUpdate();
            foreach (var pol in delPolicies)
            {
                ListViewItem item = new ListViewItem(pol.Name, 0);

                item.SubItems.Add(pol.AssetDeliveryPolicyType.ToString());
                item.SubItems.Add(pol.AssetDeliveryProtocol.ToString());
                item.SubItems.Add(pol.Id);
                listViewPolicies.Items.Add(item);
            }
            listViewPolicies.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            listViewPolicies.EndUpdate();
            this.Cursor = Cursors.Default;

        }


        private void listViewFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonDelete.Enabled = listViewPolicies.SelectedItems.Count > 0;
            buttonRename.Enabled = buttonSelect.Enabled = listViewPolicies.SelectedItems.Count == 1;
            DoDisplayDeliveryPolicyProperties();
        }



        private void DoDisplayDeliveryPolicyProperties()
        {
            var selection = ReturnSelectedDeliveryPolicies();

            if (selection.Count > 0)
            {
                var policy = selection.FirstOrDefault();
                DGDelPol.Rows.Clear();
                DGDelPol.ColumnCount = 2;
                DGDelPol.Columns[0].DefaultCellStyle.BackColor = Color.Gainsboro;

                DGDelPol.Rows.Add("Name", policy.Name);
                DGDelPol.Rows.Add("Id", policy.Id);
                DGDelPol.Rows.Add("Type", policy.AssetDeliveryPolicyType);
                DGDelPol.Rows.Add("Protocol", policy.AssetDeliveryProtocol);
                if (policy.AssetDeliveryConfiguration != null)
                {
                    int i = 0;
                    foreach (var conf in policy.AssetDeliveryConfiguration)
                    {
                        DGDelPol.Rows.Add(string.Format("Config #{0}, \"{1}\"", i, conf.Key), conf.Value);
                        i++;
                    }
                }
            }
        }

        private List<IAssetDeliveryPolicy> ReturnSelectedDeliveryPolicies()
        {
            List<IAssetDeliveryPolicy> Selection = new List<IAssetDeliveryPolicy>();

            foreach (int selectedindex in listViewPolicies.SelectedIndices)
            {
                IAssetDeliveryPolicy DP = delPolicies.Where(pol => pol.Id == listViewPolicies.Items[selectedindex].SubItems[3].Text).FirstOrDefault();

                if (DP != null)
                {
                    Selection.Add(DP);
                }
            }
            return Selection;
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            DoDeleteDelPol();
        }
        private void DoDeleteDelPol()
        {
            var policies = ReturnSelectedDeliveryPolicies();

            if (policies.Count > 0)
            {
                string question;
                int nbError = 0;
                string messagestr = "";

                if (policies.Count == 1)
                {
                    question = "Are you sure that you want to DELETE this policy from the Azure Media Services account ?";
                }
                else
                {
                    question = string.Format("Are you sure that you want to DELETE these {0} policies from the Azure Media Services account ?", policies.Count);
                }

                if (MessageBox.Show(question, "Delivery policy deletion", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    this.Cursor = Cursors.WaitCursor;
                    foreach (var DelPol in policies)
                    {
                        //IAssetDeliveryPolicy DelPol = delPolicies.Skip((int)ind).Take(1).FirstOrDefault();

                        if (DelPol != null)
                        {
                            try
                            {
                                DelPol.Delete();

                            }
                            catch (Exception e)
                            {
                                nbError++;
                                if (e.InnerException != null)
                                {
                                    messagestr = Program.GetErrorMessage(e);
                                }
                            }
                        }
                    }
                    this.Cursor = Cursors.Default;

                    if (nbError > 0)
                    {
                        messagestr = string.Format("Error when deleting {0} delivery policies.", nbError) + Constants.endline + messagestr;
                        MessageBox.Show(messagestr);
                    }

                    ListPolicies();

                }
            }
        }

        private void buttonRename_Click(object sender, EventArgs e)
        {
            DoMenuRenamePolicy();
        }

        private void DoMenuRenamePolicy()
        {
            var policies = ReturnSelectedDeliveryPolicies();

            if (policies.Count == 1)
            {
                IAssetDeliveryPolicy DelPol = policies.FirstOrDefault();

                string value = DelPol.Name;

                if (Program.InputBox("Policy rename", string.Format("Enter the new name for policy '{0}' :", DelPol.Name), ref value) == DialogResult.OK)
                {
                    try
                    {
                        DelPol.Name = value;
                        DelPol.Update();
                        ListPolicies();
                    }
                    catch
                    {

                        MessageBox.Show("There is a problem when renaming the policy.");
                        return;
                    }
                }
            }
        }
    }
}
