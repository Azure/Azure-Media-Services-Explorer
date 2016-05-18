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
        private CopyAssetBoxMode Mode;
        bool ErrorConnectingAMS = false;
        bool ErrorConnectingStorage = false;

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
        public bool CopyDynEnc
        {
            get
            {
                return checkBoxCopyDynEnc.Checked;
            }
        }

        public bool CloneLocators
        {
            get
            {
                return checkBoxCloneLocators.Checked;
            }
        }

        public bool RewriteLAURL
        {
            get
            {
                return checkBoxRewriteURL.Checked;
            }
        }

        public bool CloneAssetFilters
        {
            get
            {
                return checkBoxCloneAssetFilters.Checked;
            }
        }


        public CopyAsset(CloudMediaContext context, int numberofobjectselected, CopyAssetBoxMode mode)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            _context = context;
            Mode = mode;
            string s = numberofobjectselected > 1 ? "s" : "";

            switch (Mode)
            {
                case CopyAssetBoxMode.CopyAsset:
                    buttonOk.Text = string.Format(buttonOk.Text, s);
                    labelinfo.Text = string.Format(labelinfo.Text, numberofobjectselected, s);
                    checkBoxDeleteSource.Text = string.Format(checkBoxDeleteSource.Text, s);
                    checkBoxTargetSingleAsset.Enabled = numberofobjectselected > 1;
                    checkBoxCopyDynEnc.Checked = false;
                    checkBoxCloneLocators.Visible = false;
                    labelCloneLocators.Visible = false;
                    checkBoxCloneAssetFilters.Checked = false;
                    labelCloneFilters.Visible = false; // optioon to clone filter is displayed but we don't want to display that start and end times are removed. This is not the case for asset copy.
                    break;

                case CopyAssetBoxMode.CloneChannel:
                    labelAssetCopy.Text = "Clone Channel";
                    labelExplanation.Text = string.Format("The channel{0} will be cloned with the same name and settings to the selected account.", s);
                    labelnewassetname.Visible = false;
                    copyassetname.Visible = false;
                    labelinfo.Text = string.Format("{0} channel{1} selected", numberofobjectselected, s);
                    buttonOk.Text = this.Text = string.Format("Clone channel{0}", s);
                    panelStorageAccount.Visible = false;
                    groupBoxOptions.Visible = false;
                    break;

                case CopyAssetBoxMode.CloneProgram:
                    labelAssetCopy.Text = "Clone Program";
                    labelExplanation.Text = string.Format("The program{0} will be cloned to the same channel name in the selected account.", s);
                    labelinfo.Text = string.Format("{0} program{1} selected", numberofobjectselected, s);
                    buttonOk.Text = this.Text = string.Format("Clone program{0}", s);
                    labelnewassetname.Visible = false;
                    copyassetname.Visible = false;
                    checkBoxDeleteSource.Visible = false;
                    checkBoxTargetSingleAsset.Visible = false;
                    break;

                default:
                    break;

            }
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

                if (Mode == CopyAssetBoxMode.CopyAsset)
                {
                    labelWarning.Text = (string.IsNullOrEmpty(SelectedCredentials.StorageKey)) ? "Storage key is empty !" : string.Empty;
                }
                radioButtonDefaultStorage.Checked = true;
                listBoxStorage.Items.Clear();

                // let's check connection to account
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    CloudMediaContext newcontext = Program.ConnectAndGetNewContext(SelectedCredentials, true, false);
                    foreach (var storage in newcontext.StorageAccounts)
                    {
                        listBoxStorage.Items.Add(new Item(storage.Name + ((storage.Name == newcontext.DefaultStorageAccount.Name) ? " (default)" : string.Empty), storage.Name));
                    }
                    labelWarningStorage.Text = "";
                    ErrorConnectingAMS = false;
                }
                catch
                {
                    labelWarningStorage.Text = "Error when connecting to account.";
                    ErrorConnectingAMS = true;
                }
                finally
                {
                    this.Cursor = Cursors.Arrow;
                }
                UpdateStatusButtonOk();
            }
        }

        private void radioButtonSpecify_CheckedChanged(object sender, EventArgs e)
        {
            listBoxStorage.Enabled = radioButtonSpecifyStorage.Checked;

            if (radioButtonSpecifyStorage.Checked)
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    CloudMediaContext newcontext = Program.ConnectAndGetNewContext(SelectedCredentials, true, false);
                    foreach (var storage in newcontext.StorageAccounts)
                    {
                        listBoxStorage.Items.Add(new Item(storage.Name + ((storage.Name == newcontext.DefaultStorageAccount.Name) ? " (default)" : string.Empty), storage.Name));
                    }
                    ErrorConnectingAMS = false;
                }
                catch
                {
                    labelWarningStorage.Text = "Error when connecting to account.";
                    ErrorConnectingAMS = true;
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

            UpdateStatusButtonOk();
        }

        private void UpdateStatusButtonOk()
        {
            buttonOk.Enabled = !ErrorConnectingAMS && !ErrorConnectingStorage;
        }

        private void listBoxStorage_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        private void checkBoxCopyDynEnc_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxRewriteURL.Enabled = checkBoxCopyDynEnc.Checked;
        }

        private void checkBoxTargetSingleAsset_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxCopyDynEnc.Enabled = checkBoxRewriteURL.Enabled = checkBoxCloneAssetFilters.Enabled = !checkBoxTargetSingleAsset.Checked;
        }
    }

    public enum CopyAssetBoxMode
    {
        CopyAsset = 0,
        CloneProgram,
        CloneChannel
    }
}
