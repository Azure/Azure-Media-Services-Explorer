//----------------------------------------------------------------------- 
// <copyright file="CopyAsset.cs" company="Microsoft">Copyright (c) Microsoft Corporation. All rights reserved.</copyright> 
// <license>
// Azure Media Services Explorer Ver. 3.2
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
using System.Collections.Specialized;
using System.Xml;
using System.Xml.Linq;
using System.Diagnostics;
using System.Configuration;
using Microsoft.WindowsAzure.MediaServices.Client;

namespace AMSExplorer
{
    public partial class CopyAsset : Form
    {

        StringCollection CredentialsList;
        CredentialsEntry SelectedCredentials;
        private CloudMediaContext _context;

        public CredentialsEntry DestinationLoginCredentials
        {
            get
            {
                return SelectedCredentials;
            }
        }

        public string DestinationStorageAccount
        {
            get
            {
                string storage = null;

                if (radioButtonSpecifyStorage.Checked && listBoxStorage.SelectedItem != null)
                {
                    storage = ((Item)listBoxStorage.SelectedItem).Value;
                }
                return storage;
            }
        }

        public string CopyAssetName
        {
            get
            {
                return copyassetname.Text;
            }
            set
            {
                copyassetname.Text = value;
            }
        }

        public bool SingleDestinationAsset
        {
            get
            {
                return checkBoxTargetSingleAsset.Checked;
            }

        }

        public bool DeleteSourceAsset
        {
            get
            {
                return checkBoxDeleteSource.Checked;
            }

        }

        public bool EnableSingleDestinationAsset
        {
            set
            {
                checkBoxTargetSingleAsset.Enabled = value;
            }
        }




        public CopyAsset(CloudMediaContext context, int numberofassets)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            _context = context;
            labelinfo.Text = string.Format(labelinfo.Text, numberofassets, numberofassets > 1 ? "s" : "");
            buttonOk.Text = string.Format(buttonOk.Text, numberofassets > 1 ? "s" : "");
            checkBoxDeleteSource.Text = string.Format(checkBoxDeleteSource.Text, numberofassets > 1 ? "s" : "");
            checkBoxTargetSingleAsset.Enabled = numberofassets > 1;
            
        }


        private void CopyAsset_Load(object sender, EventArgs e)
        {
            CredentialsList = Properties.Settings.Default.LoginList;
            labelWarning.Text = "";
            labelWarningStorage.Text = "";

            if (CredentialsList != null)
            {
                for (int i = 0; i < (CredentialsList.Count / CredentialsEntry.StringsCount); i++)
                {
                    {
                        int index = listBoxAccounts.Items.Add(CredentialsList[i * CredentialsEntry.StringsCount]);
                        if (CredentialsList[i * CredentialsEntry.StringsCount] == _context.Credentials.ClientId)
                        {
                            listBoxAccounts.SelectedIndex = index;
                        }

                    }
                }
            }
            listBoxAccounts.SelectedItem = _context.DefaultStorageAccount.Name;

        }

        private string ReturnAzureEndpoint(string mystring)
        {
            return mystring.Split("|".ToCharArray())[0];

        }

        private string ReturnManagementPortal(string mystring)
        {
            string[] temp = mystring.Split("|".ToCharArray());
            return temp.Count() > 1 ? temp[1] : string.Empty;
        }


        private void listBoxAcounts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxAccounts.SelectedIndex > -1) // one selected
            {
                int index = listBoxAccounts.SelectedIndex * CredentialsEntry.StringsCount;
                string[] temp = CredentialsList[index + 9].Split("|".ToCharArray());
                SelectedCredentials = new CredentialsEntry(
                   CredentialsList[index],
                   CredentialsList[index + 1],
                   CredentialsList[index + 2],
                   CredentialsList[index + 3],
                   CredentialsList[index + 4],
                   CredentialsList[index + 5],
                   CredentialsList[index + 6],
                   CredentialsList[index + 7],
                   CredentialsList[index + 8],
                   ReturnAzureEndpoint(CredentialsList[index + 9]),
                   ReturnManagementPortal(CredentialsList[index + 9])
                    );

                labelDescription.Text = CredentialsList[listBoxAccounts.SelectedIndex * CredentialsEntry.StringsCount + 3];
                labelWarning.Text = (string.IsNullOrEmpty(SelectedCredentials.StorageKey)) ? "Storage key is empty !" : string.Empty;
                radioButtonDefaultStorage.Checked = true;
                listBoxStorage.Items.Clear();
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            listBoxStorage.Enabled = radioButtonSpecifyStorage.Checked;

            if (radioButtonSpecifyStorage.Checked)
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    CloudMediaContext newcontext = Program.ConnectAndGetNewContext(SelectedCredentials);
                    foreach (var storage in newcontext.StorageAccounts)
                    {
                        listBoxStorage.Items.Add(new Item(storage.Name + ((storage.Name == newcontext.DefaultStorageAccount.Name) ? " (default)" : string.Empty), storage.Name));
                    }

                }
                catch (Exception ex)
                {
                    labelWarningStorage.Text = "Erreur when connecting to account.";
                }
                finally
                {
                    this.Cursor = Cursors.Arrow;
                }


            }
            else  // default storage selected
            {
                listBoxStorage.Items.Clear();
            }
        }

        private void listBoxStorage_SelectedIndexChanged(object sender, EventArgs e)
        {



        }
    }
}
