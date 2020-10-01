//----------------------------------------------------------------------------------------------
//    Copyright 2020 Microsoft Corporation
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
using Newtonsoft.Json;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AMSExplorer
{
    public partial class CopyAsset : Form
    {
        private ListCredentialsRPv3 CredentialList = new ListCredentialsRPv3();
        private readonly CopyAssetBoxMode Mode;
        private bool ErrorConnectingAMS = false;
        private bool ErrorConnectingStorage = false;
        private readonly string _accountname;


        public string DestinationStorageAccount
        {
            get
            {
                if (listBoxStorage.SelectedItem != null)
                {
                    return ((Item)listBoxStorage.SelectedItem).Value;
                }
                else
                {
                    return null;
                }
            }
        }

        public string CopyAssetName
        {
            get => copyassetname.Text;
            set => copyassetname.Text = value;
        }

        public bool SingleDestinationAsset => checkBoxTargetSingleAsset.Checked;

        public bool DeleteSourceAsset => checkBoxDeleteSource.Checked;


        public AMSClientV3 DestinationAmsClient { get; private set; }
        public CredentialsEntryV3 DestinationLoginInfo { get; private set; }

        public CopyAsset(int numberofobjectselected, CopyAssetBoxMode mode, string accountname)
        {
            InitializeComponent();
            _accountname = accountname;
            Icon = Bitmaps.Azure_Explorer_ico;
            //_context = context;
            Mode = mode;

            switch (Mode)
            {
                case CopyAssetBoxMode.CopyAsset:
                    buttonOk.Text = Text = numberofobjectselected > 1 ? AMSExplorer.Properties.Resources.CopyAsset_CopyAsset_CopyAssets : AMSExplorer.Properties.Resources.CopyAsset_CopyAsset_CopyAsset;
                    labelinfo.Text = string.Format(numberofobjectselected > 1 ? AMSExplorer.Properties.Resources.CopyAsset_CopyAsset_0AssetsSelected : AMSExplorer.Properties.Resources.CopyAsset_CopyAsset_0AssetSelected, numberofobjectselected);
                    checkBoxDeleteSource.Text = numberofobjectselected > 1 ? AMSExplorer.Properties.Resources.CopyAsset_CopyAsset_DeleteSourceAssets : AMSExplorer.Properties.Resources.CopyAsset_CopyAsset_DeleteSourceAsset;
                    checkBoxTargetSingleAsset.Enabled = numberofobjectselected > 1;
                    break;

                case CopyAssetBoxMode.CloneLiveEvent:
                    labelAssetCopy.Text = AMSExplorer.Properties.Resources.CopyAsset_CopyAsset_CloneChannel;
                    labelExplanation.Text = numberofobjectselected > 1 ? AMSExplorer.Properties.Resources.CopyAsset_CopyAsset_TheChannelsWillBeClonedWithTheSameNameAndSettingsToTheSelectedAccount : AMSExplorer.Properties.Resources.CopyAsset_CopyAsset_TheChannelWillBeClonedWithTheSameNameAndSettingsToTheSelectedAccount;
                    labelnewassetname.Visible = false;
                    copyassetname.Visible = false;
                    labelinfo.Text = string.Format(numberofobjectselected > 1 ? AMSExplorer.Properties.Resources.CopyAsset_CopyAsset_0ChannelsSelected : AMSExplorer.Properties.Resources.CopyAsset_CopyAsset_0ChannelSelected, numberofobjectselected);
                    buttonOk.Text = Text = numberofobjectselected > 1 ? AMSExplorer.Properties.Resources.CopyAsset_CopyAsset_CloneChannels : AMSExplorer.Properties.Resources.CopyAsset_CopyAsset_CloneChannel;
                    panelStorageAccount.Visible = false;
                    groupBoxOptions.Visible = false;
                    break;

                case CopyAssetBoxMode.CloneLiveOutput:
                    labelAssetCopy.Text = "Clone Live Event";
                    labelExplanation.Text = numberofobjectselected > 1 ? AMSExplorer.Properties.Resources.CopyAsset_CopyAsset_TheProgramsWillBeClonedToTheSameChannelNameInTheSelectedAccount : AMSExplorer.Properties.Resources.CopyAsset_CopyAsset_TheProgramWillBeClonedToTheSameChannelNameInTheSelectedAccount;
                    labelinfo.Text = string.Format(numberofobjectselected > 1 ? AMSExplorer.Properties.Resources.CopyAsset_CopyAsset_0ProgramsSelected : AMSExplorer.Properties.Resources.CopyAsset_CopyAsset_0ProgramSelected, numberofobjectselected);
                    buttonOk.Text = Text = numberofobjectselected > 1 ? AMSExplorer.Properties.Resources.CopyAsset_CopyAsset_ClonePrograms : AMSExplorer.Properties.Resources.CopyAsset_CopyAsset_CloneProgram;
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

            copyassetname.Text = Constants.NameconvAsset + "-copy-" + Program.GetUniqueness();

            DpiUtils.InitPerMonitorDpi(this);

            // Add a dummy column     
            ColumnHeader header = new ColumnHeader
            {
                Text = "",
                Name = "col1"
            };
            listViewAccounts.Columns.Add(header);
            // Then
            listViewAccounts.Scrollable = true;
            listViewAccounts.View = System.Windows.Forms.View.Details;


            if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.LoginListRPv3JSON))
            {
                // JSon deserialize
                CredentialList = (ListCredentialsRPv3)JsonConvert.DeserializeObject(Properties.Settings.Default.LoginListRPv3JSON, typeof(ListCredentialsRPv3));


                // Display accounts in the list
                CredentialList.MediaServicesAccounts.ForEach(c =>
                    AddItemToListviewAccounts(c)
                );
            }
        }

        private void AddItemToListviewAccounts(CredentialsEntryV3 c)
        {
            ListViewItem item = listViewAccounts.Items.Add(c.MediaService.Name);
            listViewAccounts.Items[item.Index].ForeColor = Color.Black;
            listViewAccounts.Items[item.Index].ToolTipText = null;
        }


        private async void listViewAccounts_SelectedIndexChanged(object sender, EventArgs e)
        {
            ErrorConnectingAMS = false;
            ErrorConnectingStorage = false;

            listBoxStorage.Items.Clear();

            if (listViewAccounts.SelectedIndices.Count != 1)
            {
                return;
            }
            // code when used from pick-up
            DestinationLoginInfo = CredentialList.MediaServicesAccounts[listViewAccounts.SelectedIndices[0]];

            if (DestinationLoginInfo == null)
            {
                MessageBox.Show("Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //MessageBox.Show(string.Format("The {0} cannot be empty.", labelE1.Text), "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            Cursor = Cursors.WaitCursor;

            DestinationAmsClient = new AMSClientV3(DestinationLoginInfo.Environment, DestinationLoginInfo.AzureSubscriptionId, DestinationLoginInfo);

            AzureMediaServicesClient response = null;
            try
            {
                response = await DestinationAmsClient.ConnectAndGetNewClientV3Async();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Cursor = Cursors.Default;
                ErrorConnectingAMS = true;
                return;
            }


            if (response == null)
            {
                Cursor = Cursors.Default;
                ErrorConnectingAMS = true;
                return;
            }

            // let's save the credentials (SP) They may be updated by the user when connecting
            CredentialList.MediaServicesAccounts[listViewAccounts.SelectedIndices[0]] = DestinationAmsClient.credentialsEntry;
            //SaveCredentialsToSettings();

            try
            {   // let's refresh storage accounts
                DestinationAmsClient.credentialsEntry.MediaService.StorageAccounts = (await DestinationAmsClient.AMSclient.Mediaservices.GetAsync(DestinationAmsClient.credentialsEntry.ResourceGroup, DestinationAmsClient.credentialsEntry.AccountName)).StorageAccounts;

                foreach (StorageAccount storage in DestinationAmsClient.credentialsEntry.MediaService.StorageAccounts)
                {
                    string storageName = AMSClientV3.GetStorageName(storage.Id);

                    int index = listBoxStorage.Items.Add(new Item(storageName + ((storage.Type == StorageAccountType.Primary) ? AMSExplorer.Properties.Resources.CopyAsset_listBoxAcounts_SelectedIndexChanged_Default : string.Empty), storageName));
                    if (storage.Type == StorageAccountType.Primary)
                    {
                        listBoxStorage.SelectedIndex = index;
                    }
                }

                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Program.GetErrorMessage(ex), "Login error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Cursor = Cursors.Default;
                ErrorConnectingAMS = true;

                return;
            }

            //            DialogResult = DialogResult.OK;  // form will close with OK result
            // else --> form won't close...

            UpdateStatusButtonOk();

        }


        private void UpdateStatusButtonOk()
        {
            buttonOk.Enabled = !ErrorConnectingAMS && !ErrorConnectingStorage;
        }

        private void listBoxStorage_SelectedIndexChanged(object sender, EventArgs e)
        {


        }


        private void checkBoxTargetSingleAsset_CheckedChanged(object sender, EventArgs e)
        {

        }


        private void CopyAsset_DpiChanged(object sender, DpiChangedEventArgs e)
        {
            DpiUtils.UpdatedSizeFontAfterDPIChange(labelAssetCopy, e);
        }

        private void ScaleListViewColumns(ListView listview)
        {
            if (listview.Columns.Count > 0)
            {
                listview.Columns[0].Width = listview.Width - 4 - SystemInformation.VerticalScrollBarWidth;
            }
        }

        private void listBoxAccounts_DpiChangedAfterParent(object sender, EventArgs e)
        {
            ScaleListViewColumns(listViewAccounts);
        }

        private void listViewAccounts_DpiChangedAfterParent(object sender, EventArgs e)
        {
            ScaleListViewColumns(listViewAccounts);
        }

        private void CopyAsset_Shown(object sender, EventArgs e)
        {
            ScaleListViewColumns(listViewAccounts);
        }
    }

    public enum CopyAssetBoxMode
    {
        CopyAsset = 0,
        CloneLiveOutput,
        CloneLiveEvent
    }
}
