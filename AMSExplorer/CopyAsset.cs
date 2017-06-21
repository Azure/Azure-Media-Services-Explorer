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
using System.Collections.Specialized;
using System.Xml;
using System.Xml.Linq;
using System.Diagnostics;
using System.Configuration;
using Microsoft.WindowsAzure.MediaServices.Client;
using Newtonsoft.Json;

namespace AMSExplorer
{
    public partial class CopyAsset : Form
    {
        ListCredentials CredentialList = new ListCredentials();
        CredentialsEntry SelectedCredentials;
        private CloudMediaContext _context;
        private CopyAssetBoxMode Mode;
        bool ErrorConnectingAMS = false;
        bool ErrorConnectingStorage = false;
        private string _accountname;

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

        public bool UnpublishSourceAsset
        {
            get
            {
                return checkBoxUnPublishSourceAsset.Checked;
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


        public CopyAsset(CloudMediaContext context, int numberofobjectselected, CopyAssetBoxMode mode, string accountname)
        {
            InitializeComponent();
            _accountname = accountname;
            this.Icon = Bitmaps.Azure_Explorer_ico;
            _context = context;
            Mode = mode;

            switch (Mode)
            {
                case CopyAssetBoxMode.CopyAsset:
                    buttonOk.Text = this.Text = numberofobjectselected > 1 ? AMSExplorer.Properties.Resources.CopyAsset_CopyAsset_CopyAssets : AMSExplorer.Properties.Resources.CopyAsset_CopyAsset_CopyAsset;
                    labelinfo.Text = string.Format(numberofobjectselected > 1 ? AMSExplorer.Properties.Resources.CopyAsset_CopyAsset_0AssetsSelected : AMSExplorer.Properties.Resources.CopyAsset_CopyAsset_0AssetSelected, numberofobjectselected);
                    checkBoxDeleteSource.Text = numberofobjectselected > 1 ? AMSExplorer.Properties.Resources.CopyAsset_CopyAsset_DeleteSourceAssets : AMSExplorer.Properties.Resources.CopyAsset_CopyAsset_DeleteSourceAsset;
                    checkBoxTargetSingleAsset.Enabled = numberofobjectselected > 1;
                    checkBoxCopyDynEnc.Checked = false;
                    checkBoxCloneLocators.Checked = false;
                    checkBoxCloneAssetFilters.Checked = false;
                    labelCloneFilters.Visible = false; // option to clone filter is displayed but we don't want to display that start and end times are removed. This is not the case for asset copy.
                    break;

                case CopyAssetBoxMode.CloneChannel:
                    labelAssetCopy.Text = AMSExplorer.Properties.Resources.CopyAsset_CopyAsset_CloneChannel;
                    labelExplanation.Text = numberofobjectselected > 1 ? AMSExplorer.Properties.Resources.CopyAsset_CopyAsset_TheChannelsWillBeClonedWithTheSameNameAndSettingsToTheSelectedAccount : AMSExplorer.Properties.Resources.CopyAsset_CopyAsset_TheChannelWillBeClonedWithTheSameNameAndSettingsToTheSelectedAccount;
                    labelnewassetname.Visible = false;
                    copyassetname.Visible = false;
                    labelinfo.Text = string.Format(numberofobjectselected > 1 ? AMSExplorer.Properties.Resources.CopyAsset_CopyAsset_0ChannelsSelected : AMSExplorer.Properties.Resources.CopyAsset_CopyAsset_0ChannelSelected, numberofobjectselected);
                    buttonOk.Text = this.Text = numberofobjectselected > 1 ? AMSExplorer.Properties.Resources.CopyAsset_CopyAsset_CloneChannels : AMSExplorer.Properties.Resources.CopyAsset_CopyAsset_CloneChannel;
                    panelStorageAccount.Visible = false;
                    groupBoxOptions.Visible = false;
                    break;

                case CopyAssetBoxMode.CloneProgram:
                    labelAssetCopy.Text = "Clone Program";
                    labelExplanation.Text = numberofobjectselected > 1 ? AMSExplorer.Properties.Resources.CopyAsset_CopyAsset_TheProgramsWillBeClonedToTheSameChannelNameInTheSelectedAccount : AMSExplorer.Properties.Resources.CopyAsset_CopyAsset_TheProgramWillBeClonedToTheSameChannelNameInTheSelectedAccount;
                    labelinfo.Text = string.Format(numberofobjectselected > 1 ? AMSExplorer.Properties.Resources.CopyAsset_CopyAsset_0ProgramsSelected : AMSExplorer.Properties.Resources.CopyAsset_CopyAsset_0ProgramSelected, numberofobjectselected);
                    buttonOk.Text = this.Text = numberofobjectselected > 1 ? AMSExplorer.Properties.Resources.CopyAsset_CopyAsset_ClonePrograms : AMSExplorer.Properties.Resources.CopyAsset_CopyAsset_CloneProgram;
                    labelnewassetname.Visible = false;
                    copyassetname.Visible = false;
                    checkBoxDeleteSource.Visible = false;
                    checkBoxTargetSingleAsset.Visible = false;
                    checkBoxUnPublishSourceAsset.Visible = false;
                    labelCloneLocatorForPrograms.Visible = true;
                    labelCloneLocators.Visible = false;
                    break;

                default:
                    break;

            }
        }


        private void CopyAsset_Load(object sender, EventArgs e)
        {
            labelWarning.Text = "";
            labelWarningStorage.Text = "";

            CredentialList = (ListCredentials)JsonConvert.DeserializeObject(Properties.Settings.Default.LoginListJSON, typeof(ListCredentials));
            CredentialList.MediaServicesAccounts.ForEach(c => listBoxAccounts.Items.Add(AMSLogin.ReturnAccountName(c)));

            var entryWithSameName = CredentialList.MediaServicesAccounts.Where(c => AMSLogin.ReturnAccountName(c).ToLower().Trim() == _accountname.ToLower().Trim()).FirstOrDefault();
            if (entryWithSameName != null)
            {
                listBoxAccounts.SelectedIndex = CredentialList.MediaServicesAccounts.IndexOf(entryWithSameName);
            }
        }


        private void listBoxAcounts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxAccounts.SelectedIndex > -1) // one selected
            {
                int index = listBoxAccounts.SelectedIndex;
                SelectedCredentials = CredentialList.MediaServicesAccounts[index];

                labelDescription.Text = SelectedCredentials.Description;

                if (Mode == CopyAssetBoxMode.CopyAsset)
                {
                    labelWarning.Text = (string.IsNullOrEmpty(SelectedCredentials.DefaultStorageKey)) ? AMSExplorer.Properties.Resources.CopyAsset_listBoxAcounts_SelectedIndexChanged_StorageKeyIsEmpty : string.Empty;
                }
                radioButtonDefaultStorage.Checked = true;
                listBoxStorage.Items.Clear();

                if (SelectedCredentials.UseAADServicePrincipal) // not supported for now
                {
                    labelWarningStorage.Text = AMSExplorer.Properties.Resources.CopyAsset_listBoxAcounts_SelectedIndexChanged_ErrorWhenConnectingToAccount;
                    ErrorConnectingAMS = true;
                    return;
                }


                // let's check connection to account
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    CloudMediaContext newcontext = Program.ConnectAndGetNewContext(SelectedCredentials, true, false);
                    foreach (var storage in newcontext.StorageAccounts)
                    {
                        listBoxStorage.Items.Add(new Item(storage.Name + ((storage.Name == newcontext.DefaultStorageAccount.Name) ? AMSExplorer.Properties.Resources.CopyAsset_listBoxAcounts_SelectedIndexChanged_Default : string.Empty), storage.Name));
                    }
                    labelWarningStorage.Text = "";
                    ErrorConnectingAMS = false;
                }
                catch
                {
                    labelWarningStorage.Text = AMSExplorer.Properties.Resources.CopyAsset_listBoxAcounts_SelectedIndexChanged_ErrorWhenConnectingToAccount;
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
                if (SelectedCredentials.UseAADServicePrincipal) // not supported for now
                {
                    labelWarningStorage.Text = AMSExplorer.Properties.Resources.CopyAsset_listBoxAcounts_SelectedIndexChanged_ErrorWhenConnectingToAccount;
                    ErrorConnectingAMS = true;
                    return;
                }

                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    CloudMediaContext newcontext = Program.ConnectAndGetNewContext(SelectedCredentials, true, false);
                    foreach (var storage in newcontext.StorageAccounts)
                    {
                        listBoxStorage.Items.Add(new Item(storage.Name + ((storage.Name == newcontext.DefaultStorageAccount.Name) ? AMSExplorer.Properties.Resources.CopyAsset_listBoxAcounts_SelectedIndexChanged_Default : string.Empty), storage.Name));
                    }
                    ErrorConnectingAMS = false;
                }
                catch
                {
                    labelWarningStorage.Text = AMSExplorer.Properties.Resources.CopyAsset_listBoxAcounts_SelectedIndexChanged_ErrorWhenConnectingToAccount;
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
            checkBoxCopyDynEnc.Enabled = checkBoxRewriteURL.Enabled = checkBoxCloneAssetFilters.Enabled = checkBoxCloneLocators.Enabled = checkBoxUnPublishSourceAsset.Enabled = !checkBoxTargetSingleAsset.Checked;
        }

        private void checkBoxCloneLocators_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxUnPublishSourceAsset.Enabled = checkBoxCloneLocators.Checked;
        }
    }

    public enum CopyAssetBoxMode
    {
        CopyAsset = 0,
        CloneProgram,
        CloneChannel
    }
}
