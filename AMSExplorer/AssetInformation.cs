//----------------------------------------------------------------------------------------------
//    Copyright 2019 Microsoft Corporation
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
using Microsoft.WindowsAzure.MediaServices.Client.ContentKeyAuthorization;
using Microsoft.WindowsAzure.MediaServices.Client.DynamicEncryption;
using Microsoft.WindowsAzure.MediaServices.Client.Metadata;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Azure.Management.Media;
using Microsoft.Azure.Management.Media.Models;
using Newtonsoft.Json;

namespace AMSExplorer
{
    public partial class AssetInformation : Form
    {
        public IAsset myAsset;
        public Asset myAssetV3;
        private AMSClientV3 _amsClient;
        public IEnumerable<StreamingEndpoint> myStreamingEndpoints;
        private ILocator tempLocator = null;
        private ILocator tempMetadaLocator = null;
        private Mainform myMainForm;
        private bool oktobuildlocator = false;
        private ManifestTimingData myassetmanifesttimingdata = null;
        private CloudBlobContainer container = null;
        private IEnumerable<IListBlobItem> blobs;

        public AssetInformation(Mainform mainform, AMSClientV3 amsClient)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            myMainForm = mainform;
            _amsClient = amsClient;
        }

        private void contextMenuStripDG_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void toolStripMenuItemCopy_Click(object sender, EventArgs e)
        {
            if (TreeViewLocators.SelectedNode != null)
            {
                // Root node's Parent property is null, so do check
                if (TreeViewLocators.SelectedNode.Parent != null)
                {
                    System.Windows.Forms.Clipboard.SetText(TreeViewLocators.SelectedNode.Text);
                }
            }
        }


        private void toolStripMenuItemDASHIF_Click(object sender, EventArgs e)
        {
            DoDASHIFPlayer();

        }

        private void DoDASHIFPlayer()
        {
            if (TreeViewLocators.SelectedNode != null)
            {
                // Root node's Parent property is null, so do check
                if (TreeViewLocators.SelectedNode.Parent != null)
                {
                    switch (TreeViewLocators.SelectedNode.Parent.Text)
                    {
                        case AssetInfo._dash_cmaf:
                        case AssetInfo.format_dash_csf:
                            AssetInfo.DoPlayBackWithStreamingEndpoint(typeplayer: PlayerType.DASHIFRefPlayer, path: TreeViewLocators.SelectedNode.Text, DoNotRewriteURL: true, client: _amsClient, mainForm: myMainForm);
                            break;

                        default:
                            break;
                    }
                }
            }
        }

        private void contextMenuStripLocators_Opening(object sender, CancelEventArgs e)
        {
            if (TreeViewLocators.SelectedNode != null)
            {
                if (TreeViewLocators.SelectedNode.Parent != null)
                {
                    toolStripMenuItemAzureMediaPlayer.Enabled = false;
                    toolStripMenuItemDASHIF.Enabled = false;
                    toolStripMenuItemPlaybackMP4.Enabled = false;
                    toolStripMenuItemOpen.Enabled = false;
                    deleteLocatorToolStripMenuItem.Enabled = false;

                    if (TreeViewLocators.SelectedNode.Parent.Text.Equals(AssetInfo._smooth) || TreeViewLocators.SelectedNode.Parent.Text.Contains(AssetInfo._smooth_legacy))
                    {
                        toolStripMenuItemAzureMediaPlayer.Enabled = true;
                        toolStripMenuItemDASHIF.Enabled = false;
                        toolStripMenuItemPlaybackMP4.Enabled = false;
                        toolStripMenuItemOpen.Enabled = false;
                    }
                    if (TreeViewLocators.SelectedNode.Parent.Text.Equals(AssetInfo._dash_csf) || (TreeViewLocators.SelectedNode.Parent.Text.Equals(AssetInfo._dash_cmaf)))
                    {
                        toolStripMenuItemAzureMediaPlayer.Enabled = true;
                        toolStripMenuItemDASHIF.Enabled = true;
                        toolStripMenuItemPlaybackMP4.Enabled = false;
                        toolStripMenuItemOpen.Enabled = false;
                    }
                    if (TreeViewLocators.SelectedNode.Parent.Text.Equals(AssetInfo._prog_down_https_SAS))
                    {
                        toolStripMenuItemAzureMediaPlayer.Enabled = (TreeViewLocators.SelectedNode.Text.ToLower().Contains(".mp4"));
                        toolStripMenuItemDASHIF.Enabled = false;
                        toolStripMenuItemPlaybackMP4.Enabled = false;
                        toolStripMenuItemOpen.Enabled = true;
                    }
                    if (TreeViewLocators.SelectedNode.Parent.Text.Equals(AssetInfo._prog_down_http_streaming))
                    {
                        toolStripMenuItemAzureMediaPlayer.Enabled = (TreeViewLocators.SelectedNode.Text.ToLower().Contains(".mp4"));
                        toolStripMenuItemDASHIF.Enabled = false;
                        toolStripMenuItemPlaybackMP4.Enabled = (TreeViewLocators.SelectedNode.Text.ToLower().Contains(".mp4"));
                        toolStripMenuItemOpen.Enabled = !(TreeViewLocators.SelectedNode.Text.ToLower().Contains(".ism"));
                    }
                }
                else
                {
                    deleteLocatorToolStripMenuItem.Enabled = true; // no parent, so we can propose the deletion
                }

            }
        }

        private void toolStripMenuItemPlaybackMP4_Click(object sender, EventArgs e)
        {
            DoHTMLPlayer();
        }



        private void DoHTMLPlayer()
        {
            if (TreeViewLocators.SelectedNode != null)
            {
                // Root node's Parent property is null, so do check
                if (TreeViewLocators.SelectedNode.Parent != null)
                {
                    AssetInfo.DoPlayBackWithStreamingEndpoint(typeplayer: PlayerType.MP4AzurePage, path: TreeViewLocators.SelectedNode.Text, DoNotRewriteURL: true, client: _amsClient, mainForm: myMainForm);
                }
            }
        }

        private void toolStripMenuItemOpen_Click(object sender, EventArgs e)
        {
            if (TreeViewLocators.SelectedNode != null)
            {
                // Root node's Parent property is null, so do check
                if (TreeViewLocators.SelectedNode.Parent != null)
                {
                    Process.Start(TreeViewLocators.SelectedNode.Text);
                }
            }
        }




        private void ListAssetBlobs()
        {
            if (container == null) //first time
            {
                ListContainerSasInput input = new ListContainerSasInput()
                {
                    Permissions = AssetContainerPermission.ReadWriteDelete,
                    ExpiryTime = DateTime.Now.AddHours(2).ToUniversalTime()
                };
                _amsClient.RefreshTokenIfNeeded();

                var response = Task.Run(async () => await _amsClient.AMSclient.Assets.ListContainerSasAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, myAssetV3.Name, input.Permissions, input.ExpiryTime)).Result;

                string uploadSasUrl = response.AssetContainerSasUrls.First();

                var sasUri = new Uri(uploadSasUrl);
                container = new CloudBlobContainer(sasUri);
            }

            /*
            var keys = _amsClient.GetStorageKeys(myAssetV3.StorageAccountName);

            CloudStorageAccount storageAccount;
            storageAccount = new CloudStorageAccount(new StorageCredentials(myAssetV3.StorageAccountName, keys.StorageAccountKeys.Key1), _amsClient.environment.ReturnStorageSuffix(), true);
            var cloudBlobClient = storageAccount.CreateCloudBlobClient();

            var container = cloudBlobClient.GetContainerReference(myAssetV3.Container);
            */

            blobs = container.ListBlobs(blobListingDetails: BlobListingDetails.Metadata);

            listViewFiles.Items.Clear();
            DGFiles.Rows.Clear();
            if (blobs.Count() > 0)
            {
                listViewFiles.BeginUpdate();
                foreach (var blob in blobs)
                {

                    if (blob.GetType() == typeof(CloudBlockBlob))
                    {
                        var bl = (CloudBlockBlob)blob;
                        //  bl.FetchAttributes();

                        ListViewItem item = new ListViewItem(bl.Name, 0);


                        if (bl.Properties.Length == 0)
                        {
                            item.ForeColor = Color.Red;
                        }
                        /*
                        if (file.AssetFileOptions == AssetFileOptions.Fragmented)
                        {
                            item.ForeColor = Color.DarkGoldenrod;
                        }
                        */
                        item.SubItems.Add(AssetInfo.FormatByteSize(bl.Properties.Length));

                        listViewFiles.Items.Add(item);
                        //size += file.ContentFileSize;
                    }
                    else if (blob.GetType() == typeof(CloudBlobDirectory))
                    {
                        var bl = (CloudBlobDirectory)blob;
                        ListViewItem item = new ListViewItem(bl.Prefix, 0)
                        {
                            ForeColor = Color.DarkGoldenrod
                        };
                        // let comment as it can be time expensive to the math
                        //item.SubItems.Add(AssetInfo.FormatByteSize(AssetInfo.GetSizeBlobDirectory(bl)));
                        listViewFiles.Items.Add(item);
                    }

                }
                listViewFiles.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                listViewFiles.Items[0].Selected = true;
                listViewFiles.EndUpdate();
            }

            /*
            // Generate manifest button
            var mp4AssetFiles = myAsset.AssetFiles.ToList().Where(f => f.Name.EndsWith(".mp4", StringComparison.OrdinalIgnoreCase) || f.Name.EndsWith(".m4a", StringComparison.OrdinalIgnoreCase));
            var ismAssetFiles = myAsset.AssetFiles.ToList().Where(f => f.Name.EndsWith(".ism", StringComparison.OrdinalIgnoreCase));
            buttonGenerateManifest.Enabled = (ismAssetFiles.Count() == 0 && mp4AssetFiles.Count() > 0);

            return size;
            */
        }



        private void AssetInformation_Load(object sender, EventArgs e)
        {
            labelAssetNameTitle.Text += myAssetV3.Name;

            //myAssetType = AssetInfo.GetAssetType(myAsset);

            DGAsset.ColumnCount = 2;
            DGFiles.ColumnCount = 2;
            DGFiles.Columns[0].DefaultCellStyle.BackColor = Color.Gainsboro;

            // Files in asset: headers
            long size = -1;
            /*
            if (myAsset.State != AssetState.Deleted)
            {
                size = ListAssetFiles();
                ListAssetDeliveryPolicies();
                ListAssetKeys();
            }
            */

            // asset info
            DGAsset.Columns[0].DefaultCellStyle.BackColor = Color.Gainsboro;
            DGAsset.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_Name, myAssetV3.Name);
            DGAsset.Rows.Add("Description", myAssetV3.Description);
            //DGAsset.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_Type, myAssetType);
            //DGAsset.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_AssetType, myAssetV3.Type);
            DGAsset.Rows.Add("Id", myAssetV3.Id);
            DGAsset.Rows.Add("AlternateId", myAssetV3.AlternateId);
            DGAsset.Rows.Add("AssetId", myAssetV3.AssetId);
            DGAsset.Rows.Add("Container", myAssetV3.Container);
            DGAsset.Rows.Add("StorageAccountName", myAssetV3.StorageAccountName);
            DGAsset.Rows.Add("StorageEncryptionFormat", myAssetV3.StorageEncryptionFormat);
            DGAsset.Rows.Add("Type", myAssetV3.Type);

            if (size != -1) DGAsset.Rows.Add("Size", AssetInfo.FormatByteSize(size));
            DGAsset.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_Created, ((DateTime)myAssetV3.Created).ToLocalTime().ToString("G"));
            DGAsset.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_LastModified, ((DateTime)myAssetV3.LastModified).ToLocalTime().ToString("G"));


            foreach (var se in myStreamingEndpoints)
            {
                comboBoxStreamingEndpoint.Items.Add(new Item(string.Format(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_012ScaleUnit, se.Name, se.ResourceState, StreamingEndpointInformation.ReturnTypeSE(se)), se.HostName));
                if (se.Name == "default") comboBoxStreamingEndpoint.SelectedIndex = comboBoxStreamingEndpoint.Items.Count - 1;
                foreach (var custom in se.CustomHostNames)
                {
                    comboBoxStreamingEndpoint.Items.Add(new Item(string.Format(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_012ScaleUnitCustomHostname3, se.Name, se.ResourceState, StreamingEndpointInformation.ReturnTypeSE(se), custom), custom));
                }
            }
            // if no SE has been selected (there is no SE named "default") then let's select the fist in the list
            if (myStreamingEndpoints.Count() > 0 && comboBoxStreamingEndpoint.SelectedIndex == -1)
            {
                comboBoxStreamingEndpoint.SelectedIndex = 0;
            }

            oktobuildlocator = true;
            // BuildLocatorsTree();


            return;

           

            oktobuildlocator = true;
            BuildLocatorsTree();

            listViewFiles.Tag = -1;
            listViewFiles.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(ListViewItemComparerQuickNoDate.ListView_ColumnClick);
        }

        private void DisplayAssetFilters()
        {
            _amsClient.RefreshTokenIfNeeded();

            var assetFilters = _amsClient.AMSclient.AssetFilters.List(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, myAssetV3.Name);

            dataGridViewFilters.ColumnCount = 6;
            dataGridViewFilters.Columns[0].HeaderText = AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_Name;
            dataGridViewFilters.Columns[0].Name = AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_Name;
            dataGridViewFilters.Columns[1].HeaderText = AMSExplorer.Properties.Resources.AssetInformation_DisplayAssetFilters_TrackRules;
            dataGridViewFilters.Columns[1].Name = AMSExplorer.Properties.Resources.AssetInformation_DisplayAssetFilters_Rules;
            dataGridViewFilters.Columns[2].HeaderText = AMSExplorer.Properties.Resources.AssetInformation_DisplayAssetFilters_StartDHMS;
            dataGridViewFilters.Columns[2].Name = AMSExplorer.Properties.Resources.AssetInformation_DisplayAssetFilters_Start;
            dataGridViewFilters.Columns[3].HeaderText = AMSExplorer.Properties.Resources.AssetInformation_DisplayAssetFilters_EndDHMS;
            dataGridViewFilters.Columns[3].Name = AMSExplorer.Properties.Resources.AssetInformation_DisplayAssetFilters_End;
            dataGridViewFilters.Columns[4].HeaderText = AMSExplorer.Properties.Resources.AssetInformation_DisplayAssetFilters_DVRDHMS;
            dataGridViewFilters.Columns[4].Name = AMSExplorer.Properties.Resources.AssetInformation_DisplayAssetFilters_DVR;
            dataGridViewFilters.Columns[5].HeaderText = AMSExplorer.Properties.Resources.AssetInformation_DisplayAssetFilters_LiveBackoffDHMS;
            dataGridViewFilters.Columns[5].Name = AMSExplorer.Properties.Resources.AssetInformation_DisplayAssetFilters_LiveBackoff;

            dataGridViewFilters.Rows.Clear();

            if (assetFilters.Count() > 0 && myassetmanifesttimingdata == null)
            {
                myassetmanifesttimingdata = AssetInfo.GetManifestTimingData(myAssetV3, _amsClient);
            }

            foreach (var filter in assetFilters)
            {
                string s = null;
                string e = null;
                string d = null;
                string l = null;

                if (filter.PresentationTimeRange != null)
                {
                    var start = filter.PresentationTimeRange.StartTimestamp;
                    var end = filter.PresentationTimeRange.EndTimestamp;
                    var dvr = filter.PresentationTimeRange.PresentationWindowDuration;
                    var backoff = filter.PresentationTimeRange.LiveBackoffDuration;

                    double dscale = (filter.PresentationTimeRange.Timescale != null) ?
                        (double)filter.PresentationTimeRange.Timescale
                        : (double)TimeSpan.TicksPerSecond;

                    double dscaleoffset = (!myassetmanifesttimingdata.Error && myassetmanifesttimingdata.TimeScale != null) ?
                        (double)myassetmanifesttimingdata.TimeScale
                        : (double)TimeSpan.TicksPerSecond;

                    s = ReturnFilterTextWithOffSet(start, dscale, myassetmanifesttimingdata.TimestampOffset, dscaleoffset, "min");
                    e = ReturnFilterTextWithOffSet(end, dscale, myassetmanifesttimingdata.TimestampOffset, dscaleoffset, "max");
                    d = ReturnFilterTextWithOffSet(dvr, dscale, 0, dscaleoffset, "max");
                    l = ReturnFilterTextWithOffSet(backoff, dscale, 0, dscaleoffset, "min");

                    //d = ReturnFilterText(dvr, AMSExplorer.Properties.Resources.AssetInformation_DisplayAssetFilters_Max);
                    //l = ReturnFilterText(live, AMSExplorer.Properties.Resources.AssetInformation_DisplayAssetFilters_Min);
                }
                try
                {
                    var nbtrack = filter.Tracks.Count;
                    int rowi = dataGridViewFilters.Rows.Add(filter.Name, filter.Tracks.Count, s, e, d, l);
                }
                catch
                {
                    int rowi = dataGridViewFilters.Rows.Add(filter.Name, "Error", s, e, d, l);
                }
            }
        }

        private static string ReturnFilterTextWithOffSet(string value, ulong offset, double scale)
        {
            ulong valueint = ulong.Parse(value);
            if (valueint == ulong.MaxValue)
            {
                return "max";
            }
            else
            {
                valueint -= offset;
                return TimeSpan.FromTicks((long)(valueint / scale)).ToString(@"d\.hh\:mm\:ss");
            }
        }

        private static string ReturnFilterTextWithOffSet(long? value, double scalevalue, ulong offset, double scaleoffset, string defaultwhennull)
        {

            if (value == null)
            {
                return defaultwhennull;
            }
            else
            {
                var value2 = (double)value / scalevalue;
                var offset2 = (double)offset / scaleoffset;
                value2 -= offset2;
                return TimeSpan.FromSeconds(value2).ToString(@"d\.hh\:mm\:ss");
            }
        }

        private static string ReturnFilterText(TimeSpan? value, string defaultwhennull)
        {

            if (value == null)
            {
                return defaultwhennull;
            }
            else
            {
                return ((TimeSpan)value).ToString(@"d\.hh\:mm\:ss");
            }
        }

        private StreamingEndpoint ReturnSelectedStreamingEndpoint()
        {
            if (comboBoxStreamingEndpoint.SelectedItem != null)
            {
                string hostname = ((Item)comboBoxStreamingEndpoint.SelectedItem).Value;
                return myStreamingEndpoints.Where(se => se.HostName == hostname).FirstOrDefault();

            }
            else return null;
        }

        private string ReturnSelectedStreamingEndpointHostname()
        {
            if (comboBoxStreamingEndpoint.SelectedItem != null)
            {
                return ((Item)comboBoxStreamingEndpoint.SelectedItem).Value;
            }
            else return null;
        }


        private void BuildLocatorsTree()
        {
            // LOCATORS TREE
            if (!oktobuildlocator) return;

            // IEnumerable<IAssetFile> MyAssetFiles;
            // List<Uri> ProgressiveDownloadUris;
            StreamingEndpoint SelectedSE = ReturnSelectedStreamingEndpoint();
            string SelectedSEHostName = ReturnSelectedStreamingEndpointHostname();

            if (SelectedSE == null) return;

            UriBuilder uriBuilder = new UriBuilder
            {
                Scheme = checkBoxHttps.Checked ? "https" : "http",
                Host = SelectedSE.HostName
            };



            /*
            // delivery policies
            bool protocolDASH, protocolHLS, protocolSmooth, protocolProgressiveDownload;

            if (myAsset.DeliveryPolicies.Count > 0)
            { // some dynamic encryption, let's analyse the procotols

                protocolDASH = protocolHLS = protocolSmooth = protocolProgressiveDownload = false;

                foreach (var pol in myAsset.DeliveryPolicies)
                {
                    if ((pol.AssetDeliveryProtocol & AssetDeliveryProtocol.Dash) == AssetDeliveryProtocol.Dash)
                    {
                        protocolDASH = true;
                    }
                    if ((pol.AssetDeliveryProtocol & AssetDeliveryProtocol.HLS) == AssetDeliveryProtocol.HLS)
                    {
                        protocolHLS = true;
                    }
                    if ((pol.AssetDeliveryProtocol & AssetDeliveryProtocol.SmoothStreaming) == AssetDeliveryProtocol.SmoothStreaming)
                    {
                        protocolSmooth = true;
                    }
                    if ((pol.AssetDeliveryProtocol & AssetDeliveryProtocol.ProgressiveDownload) == AssetDeliveryProtocol.ProgressiveDownload)
                    {
                        protocolProgressiveDownload = true;
                    }
                }
            }
            else
            {
                protocolDASH = protocolHLS = protocolSmooth = protocolProgressiveDownload = true;
            }
            */

            if (SelectedSE != null)
            {
                Color colornodeRU = Color.Black;
                //string filter = ((Item)comboBoxLocatorsFilters.SelectedItem).Value;

                TreeViewLocators.BeginUpdate();
                TreeViewLocators.Nodes.Clear();
                int indexloc = -1;

                _amsClient.RefreshTokenIfNeeded();

                var locators = _amsClient.AMSclient.Assets.ListStreamingLocators(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, myAssetV3.Name).StreamingLocators;

                foreach (var locatorbase in locators)
                {
                    var locator = _amsClient.AMSclient.StreamingLocators.Get(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, locatorbase.Name);

                    var streamingPaths = _amsClient.AMSclient.StreamingLocators.ListPaths(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, locator.Name).StreamingPaths;
                    var downloadPaths = _amsClient.AMSclient.StreamingLocators.ListPaths(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, locator.Name).DownloadPaths;

                    indexloc++;
                    Color colornode;
                    string locatorstatus = string.Empty;
                    string SEstatus = string.Empty;

                    switch (AssetInfo.GetPublishedStatusForLocator(locator))
                    {
                        case PublishStatus.PublishedActive:
                            colornode = Color.Black;
                            locatorstatus = "Active";
                            break;
                        case PublishStatus.PublishedExpired:
                            colornode = Color.Red;
                            locatorstatus = "Expired";
                            break;
                        case PublishStatus.PublishedFuture:
                            colornode = Color.Blue;
                            locatorstatus = "Future";
                            break;
                        default:
                            colornode = Color.Black;
                            break;
                    }
                    if (SelectedSE.ResourceState != StreamingEndpointResourceState.Running) colornode = Color.Red;

                    TreeNode myLocNode = new TreeNode(locator.Name)
                    {
                        ForeColor = colornode
                    };

                    TreeViewLocators.Nodes.Add(myLocNode);

                    TreeViewLocators.Nodes[indexloc].Nodes.Add(new TreeNode(AMSExplorer.Properties.Resources.AssetInformation_BuildLocatorsTree_LocatorInformation));

                    TreeViewLocators.Nodes[indexloc].Nodes[0].Nodes.Add(new TreeNode(
                   string.Format("Id: {0}", (locator.StreamingLocatorId))
                   ));

                    TreeViewLocators.Nodes[indexloc].Nodes[0].Nodes.Add(new TreeNode(
                        string.Format(AMSExplorer.Properties.Resources.AssetInformation_BuildLocatorsTree_Name0, locator.Name)
                        ));

                    TreeViewLocators.Nodes[indexloc].Nodes[0].Nodes.Add(new TreeNode(
                        string.Format("Policy name: {0}", locator.StreamingPolicyName.ToString())
                        ));

                    if (locator.StartTime != null)
                        TreeViewLocators.Nodes[indexloc].Nodes[0].Nodes.Add(new TreeNode(
                           string.Format(AMSExplorer.Properties.Resources.AssetInformation_BuildLocatorsTree_StartTime0, (((DateTime)locator.StartTime).ToLocalTime().ToString("G")))
                           ));

                    if (locator.EndTime != null)
                        TreeViewLocators.Nodes[indexloc].Nodes[0].Nodes.Add(new TreeNode(
                         string.Format(AMSExplorer.Properties.Resources.AssetInformation_BuildLocatorsTree_ExpirationDateTime0, (((DateTime)locator.EndTime).ToLocalTime().ToString("G")))
                         ));



                    if (streamingPaths.Count > 0)//locator.Type == LocatorType.OnDemandOrigin)
                    {

                        int indexn = 1;
                        foreach (var path in streamingPaths)
                        {
                            TreeViewLocators.Nodes[indexloc].Nodes.Add(new TreeNode(path.StreamingProtocol.ToString()) { ForeColor = colornodeRU });
                            foreach (var p in path.Paths)
                            {
                                uriBuilder.Path = p;
                                TreeViewLocators.Nodes[indexloc].Nodes[indexn].Nodes.Add(new TreeNode(uriBuilder.ToString()) { ForeColor = colornodeRU });
                            }
                            indexn = indexn + 1;
                        }

                        /*

                        TreeViewLocators.Nodes[indexloc].Nodes[0].Nodes.Add(new TreeNode(
                     string.Format(AMSExplorer.Properties.Resources.AssetInformation_BuildLocatorsTree_Path0, AssetInfo.RW(locator.Path, SelectedSE, null, checkBoxHttps.Checked, SelectedSEHostName))
                     ));

                        int indexn = 1;

                        if (
                            (myAsset.Options == AssetCreationOptions.None && myAsset.DeliveryPolicies.Count == 0)
                            ||
                            (myAsset.Options == AssetCreationOptions.StorageEncrypted && protocolProgressiveDownload)
                            ) // if no dynamic encryption and asset clear, or asset storage encrypted with progressive download decryption
                        {
                            TreeViewLocators.Nodes[indexloc].Nodes.Add(new TreeNode(AssetInfo._prog_down_http_streaming) { ForeColor = colornodeRU });

                            foreach (IAssetFile IAF in myAsset.AssetFiles)
                                TreeViewLocators.Nodes[indexloc].Nodes[indexn].Nodes.Add(new TreeNode((new Uri(AssetInfo.RW(locator.Path, SelectedSE, null, checkBoxHttps.Checked, SelectedSEHostName) + IAF.Name)).AbsoluteUri) { ForeColor = colornodeRU });
                            indexn++;
                        }

                        if (myAsset.AssetType == AssetType.MediaServicesHLS)
                        // It is a static HLS asset, so let's propose only the standard HLS V3 locator
                        {
                            TreeViewLocators.Nodes[indexloc].Nodes.Add(new TreeNode(AssetInfo._hls));
                            TreeViewLocators.Nodes[indexloc].Nodes[indexn].Nodes.Add(new TreeNode(locator.GetHlsUri().AbsoluteUri));
                            indexn++;
                        }
                        else if (myAsset.AssetType == AssetType.SmoothStreaming || myAsset.AssetType == AssetType.MultiBitrateMP4 || myAsset.AssetType == AssetType.Unknown) //later to change Unknown to live archive
                        {
                            if (protocolSmooth && locator.GetSmoothStreamingUri() != null)
                            {
                                Color ColorSmooth = ((myAsset.AssetType == AssetType.SmoothStreaming) && !checkBoxHttps.Checked) ? Color.Black : colornodeRU; // if not RU but aset is smooth, we can display the smooth URL as OK. If user asked for https, it works only with RU
                                TreeViewLocators.Nodes[indexloc].Nodes.Add(new TreeNode(AssetInfo._smooth) { ForeColor = ColorSmooth });
                                foreach (var uri in AssetInfo.GetUrisForSpecificProtocol(locator, AMSOutputProtocols.NotSpecified, SelectedSE, filter, checkBoxHttps.Checked, SelectedSEHostName))
                                {
                                    TreeViewLocators.Nodes[indexloc].Nodes[indexn].Nodes.Add(new TreeNode(uri.AbsoluteUri) { ForeColor = ColorSmooth });
                                }

                                TreeViewLocators.Nodes[indexloc].Nodes.Add(new TreeNode(AssetInfo._smooth_legacy) { ForeColor = colornodeRU });
                                foreach (var uri in AssetInfo.GetUrisForSpecificProtocol(locator, AMSOutputProtocols.SmoothLegacy, SelectedSE, filter, checkBoxHttps.Checked, SelectedSEHostName))
                                {
                                    TreeViewLocators.Nodes[indexloc].Nodes[indexn + 1].Nodes.Add(new TreeNode(uri.AbsoluteUri) { ForeColor = colornodeRU });
                                }
                                indexn = indexn + 2;
                            }
                            if (protocolDASH && locator.GetMpegDashUri() != null)
                            {
                                TreeViewLocators.Nodes[indexloc].Nodes.Add(new TreeNode(AssetInfo._dash_csf) { ForeColor = colornodeRU });
                                foreach (var uri in AssetInfo.GetUrisForSpecificProtocol(locator, AMSOutputProtocols.DashCsf, SelectedSE, filter, checkBoxHttps.Checked, SelectedSEHostName))
                                {
                                    TreeViewLocators.Nodes[indexloc].Nodes[indexn].Nodes.Add(new TreeNode(uri.AbsoluteUri) { ForeColor = colornodeRU });
                                }
                                TreeViewLocators.Nodes[indexloc].Nodes.Add(new TreeNode(AssetInfo._dash_cmaf) { ForeColor = colornodeRU });
                                foreach (var uri in AssetInfo.GetUrisForSpecificProtocol(locator, AMSOutputProtocols.DashCmaf, SelectedSE, filter, checkBoxHttps.Checked, SelectedSEHostName))
                                {
                                    TreeViewLocators.Nodes[indexloc].Nodes[indexn + 1].Nodes.Add(new TreeNode(uri.AbsoluteUri) { ForeColor = colornodeRU });
                                }
                                indexn = indexn + 2;
                            }
                            if (protocolHLS && locator.GetHlsUri() != null)
                            {
                                TreeViewLocators.Nodes[indexloc].Nodes.Add(new TreeNode(AssetInfo._hls_cmaf) { ForeColor = colornodeRU });
                                foreach (var uri in AssetInfo.GetUrisForSpecificProtocol(locator, AMSOutputProtocols.HLSCmaf, SelectedSE, filter, checkBoxHttps.Checked, SelectedSEHostName))
                                {
                                    TreeViewLocators.Nodes[indexloc].Nodes[indexn].Nodes.Add(new TreeNode(uri.AbsoluteUri) { ForeColor = colornodeRU });
                                }
                                TreeViewLocators.Nodes[indexloc].Nodes.Add(new TreeNode(AssetInfo._hls_v4) { ForeColor = colornodeRU });
                                foreach (var uri in AssetInfo.GetUrisForSpecificProtocol(locator, AMSOutputProtocols.HLSv4, SelectedSE, filter, checkBoxHttps.Checked, SelectedSEHostName))
                                {
                                    TreeViewLocators.Nodes[indexloc].Nodes[indexn + 1].Nodes.Add(new TreeNode(uri.AbsoluteUri) { ForeColor = colornodeRU });
                                }
                                TreeViewLocators.Nodes[indexloc].Nodes.Add(new TreeNode(AssetInfo._hls_v3) { ForeColor = colornodeRU });
                                foreach (var uri in AssetInfo.GetUrisForSpecificProtocol(locator, AMSOutputProtocols.HLSv3, SelectedSE, filter, checkBoxHttps.Checked, SelectedSEHostName))
                                {
                                    TreeViewLocators.Nodes[indexloc].Nodes[indexn + 2].Nodes.Add(new TreeNode(uri.AbsoluteUri) { ForeColor = colornodeRU });
                                }
                                indexn = indexn + 3;
                            }
                        }
                        */
                    }

                    if (downloadPaths.Count > 0)//locator.Type == LocatorType.Sas)
                    {

                        foreach (var p in downloadPaths)
                        {
                            uriBuilder.Path = p;
                            TreeViewLocators.Nodes[indexloc].Nodes[1].Nodes.Add(new TreeNode(uriBuilder.ToString()));
                        }

                        /*
                        TreeViewLocators.Nodes[indexloc].Nodes[0].Nodes.Add(new TreeNode(
                     string.Format("Container Path: {0}", locator.Path.Replace("http://", "https://"))
                     ));

                        TreeViewLocators.Nodes[indexloc].Nodes.Add(new TreeNode(AssetInfo._prog_down_https_SAS));

                        MyAssetFiles = myAsset
                     .AssetFiles
                     .ToList();

                        // Generate the Progressive Download URLs for each file. 
                        ProgressiveDownloadUris =
                            MyAssetFiles.Select(af => af.GetSasUri(locator)).ToList();
                        ProgressiveDownloadUris.ForEach(uri => TreeViewLocators.Nodes[indexloc].Nodes[1].Nodes.Add(new TreeNode(uri.AbsoluteUri)));
                        */
                    }
                }
                TreeViewLocators.EndUpdate();
            }

        }

        private void DoDisplayFileProperties()
        {
            var SelectedfBlobs = ReturnSelectedBlobs();
            DGFiles.Rows.Clear();


            if (SelectedfBlobs.Count > 0)
            {
                if (SelectedfBlobs.FirstOrDefault().GetType() == typeof(CloudBlockBlob))
                {
                    var blob = (CloudBlockBlob)SelectedfBlobs.FirstOrDefault();

                    DGFiles.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_Name, blob.Name);
                    DGFiles.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_DoDisplayFileProperties_FileSize, AssetInfo.FormatByteSize(blob.Properties.Length));
                    DGFiles.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_DoDisplayFileProperties_LastModified, blob.Properties.LastModified != null ? ((DateTimeOffset)blob.Properties.LastModified).ToLocalTime().ToString("G") : null);
                    DGFiles.Rows.Add("Uri", blob.Uri);
                    DGFiles.Rows.Add("MD5", blob.Properties.ContentMD5);
                }
                else if (SelectedfBlobs.FirstOrDefault().GetType() == typeof(CloudBlobDirectory))
                {
                    var dir = (CloudBlobDirectory)SelectedfBlobs.FirstOrDefault();

                    DGFiles.Rows.Add("Prefix", dir.Prefix);
                    DGFiles.Rows.Add("Uri", dir.Uri);
                    DGFiles.Rows.Add("Size", AssetInfo.FormatByteSize(AssetInfo.GetSizeBlobDirectory(dir)));
                }
            }
        }


        private void AssetInformation_FormClosed(object sender, FormClosedEventArgs e)
        {
            // let's delete temporary locators if any
            if (tempLocator != null)
            {
                try
                {
                    var locatorTask = Task.Factory.StartNew(() =>
                   {
                       tempLocator.Delete();
                   });
                    locatorTask.Wait();
                }
                catch
                {

                }
            }
            if (tempMetadaLocator != null)
            {
                try
                {
                    var locatorTask = Task.Factory.StartNew(() =>
                    {
                        tempMetadaLocator.Delete();
                    });
                    locatorTask.Wait();
                }
                catch
                {

                }
            }
        }

        private void toolStripMenuItemOpenFile_Click(object sender, EventArgs e)
        {
            DoOpenFiles();
        }

        private void DoOpenFiles()
        {
            /*
            var SelectedAssetFiles = ReturnSelectedBlobs();

            if (SelectedAssetFiles.Count > 0)
            {
                ILocator locator = GetTemporaryLocator();
                if (locator != null)
                {
                    try
                    {
                        foreach (var assetfile in SelectedAssetFiles)
                        {
                            Process.Start(assetfile.GetSasUri(locator).ToString());
                        }
                    }
                    catch
                    {
                        MessageBox.Show(AMSExplorer.Properties.Resources.AssetInformation_DoOpenFiles_ErrorWhenAccessingTemporarySASLocator);
                    }
                }
            }
            */
        }

        private void toolStripMenuItemDownloadFile_Click(object sender, EventArgs e)
        {
            DoDownloadBlobs();
        }

        private async void DoDownloadBlobs()
        {

            var SelectedBlobs = ReturnSelectedBlobs(false);

            if (SelectedBlobs.Count > 0)
            {
                CommonOpenFileDialog openFolderDialog = new CommonOpenFileDialog() { IsFolderPicker = true, InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos) };
                if (openFolderDialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    // let's check if this overwites existing files
                    var listfiles = SelectedBlobs.ToList().Where(f => File.Exists(openFolderDialog.FileName + @"\\" + (f as CloudBlockBlob).Name)).Select(f => openFolderDialog.FileName + @"\\" + (f as CloudBlockBlob).Name).ToList();
                    if (listfiles.Count > 0)
                    {
                        string text;
                        if (listfiles.Count > 1)
                        {
                            text = string.Format(
                                                AMSExplorer.Properties.Resources.AssetInformation_DoDownloadFiles_TheFollowingFilesAreAlreadyInTheFolderSNN0NNOverwiteTheFiles,
                                                string.Join("\n", listfiles.Select(f => Path.GetFileName(f)).ToArray())
                                                );
                        }
                        else
                        {
                            text = string.Format(
                                                 AMSExplorer.Properties.Resources.AssetInformation_DoDownloadFiles_TheFollowingFileIsAlreadyInTheFolderNN0NNOverwiteTheFile,
                                                 string.Join("\n", listfiles.Select(f => Path.GetFileName(f)).ToArray())
                                                 );
                        }

                        if (MessageBox.Show(text, AMSExplorer.Properties.Resources.AssetInformation_DoDownloadFiles_FileSOverwrite, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK)
                        {
                            return;
                        }
                        try
                        {
                            listfiles.ForEach(f => File.Delete(f));
                        }
                        catch
                        {
                            MessageBox.Show(AMSExplorer.Properties.Resources.AssetInformation_DoDownloadFiles_ErrorWhenDeletingFiles, AMSExplorer.Properties.Resources.AMSLogin_buttonExport_Click_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    try
                    {
                        //foreach (var blob in SelectedBlobs)
                        {
                            var response = myMainForm.DoGridTransferAddItem(string.Format("Download of blob(s) from asset '{0}'", myAssetV3.Name), TransferType.DownloadToLocal, true);
                            // Start a worker thread that does downloading.
                            //myMainForm.DoDownloadFileFromAsset(myAsset, assetfile, openFolderDialog.FileName, response);
                            await myMainForm.DownloadOutputAssetAsync(_amsClient, myAssetV3.Name, openFolderDialog.FileName, response, DownloadToFolderOption.DoNotCreateSubfolder, false, SelectedBlobs.Select(f => (f as CloudBlockBlob).Name).ToList());
                        }
                        MessageBox.Show(AMSExplorer.Properties.Resources.AssetInformation_DoDownloadFiles_DownloadProcessHasBeenInitiatedSeeTheTransfersTabToCheckTheProgress);

                    }
                    catch
                    {
                        MessageBox.Show(AMSExplorer.Properties.Resources.AssetInformation_DoDownloadFiles_ErrorWhenDownloadingFileS);
                    }
                }
            }
        }

        private void buttonCopyStats_Click(object sender, EventArgs e)
        {
            DoDisplayAssetStats();
        }

        private void DoDisplayAssetStats()
        {
            AssetInfo MyAssetReport = new AssetInfo(myAssetV3, _amsClient);
            StringBuilder SB = MyAssetReport.GetStats();
            var tokenDisplayForm = new EditorXMLJSON(AMSExplorer.Properties.Resources.AssetInformation_DoDisplayAssetStats_AssetReport, SB.ToString(), false, false, false);
            tokenDisplayForm.Display();
        }

        private void buttonDeleteFile_Click(object sender, EventArgs e)
        {
            DoDeleteBlobs();
        }

        private void DoDeleteBlobs()
        {
            var SelectedBlobs = ReturnSelectedBlobs().Where(b => b.GetType() == typeof(CloudBlockBlob)).Select(b => (CloudBlockBlob)b);

            if (SelectedBlobs.Count() > 0)
            {
                string question = SelectedBlobs.Count() == 1 ? string.Format(AMSExplorer.Properties.Resources.AssetInformation_DoDeleteFiles_DeleteTheFile0, SelectedBlobs.FirstOrDefault().Name) : string.Format(AMSExplorer.Properties.Resources.AssetInformation_DoDeleteFiles_DeleteThese0Files, SelectedBlobs.Count());

                if (System.Windows.Forms.MessageBox.Show(question, AMSExplorer.Properties.Resources.AssetInformation_DoDeleteFiles_FileDeletion, System.Windows.Forms.MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    try
                    {
                        foreach (var blob in SelectedBlobs)
                        {
                            blob.Delete();
                        }
                        ListAssetBlobs();
                        //BuildLocatorsTree();

                    }
                    catch
                    {
                        MessageBox.Show(AMSExplorer.Properties.Resources.AssetInformation_DoDeleteFiles_ErrorWhenDeletingFileS);
                        ListAssetBlobs();
                        // BuildLocatorsTree();
                    }
                }
            }
        }

        private void deleteFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoDeleteBlobs();
            BuildLocatorsTree();
        }

        private void buttonOpenFile_Click(object sender, EventArgs e)
        {
            DoOpenFiles();
        }

        private void buttonDownloadFile_Click(object sender, EventArgs e)
        {
            DoDownloadBlobs();
        }

        private void listViewFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool bSelect = listViewFiles.SelectedItems.Count > 0;
            bool bMultiSelect = listViewFiles.SelectedItems.Count > 1;

            buttonDeleteFile.Enabled = bSelect;
            buttonDeleteAll.Enabled = true;
            buttonDownloadFile.Enabled = bSelect;
            buttonOpenFile.Enabled = bSelect;
            buttonDuplicate.Enabled = bSelect && !bMultiSelect;
            buttonUpload.Enabled = bSelect;
            buttonFileMetadata.Enabled = bSelect && !bMultiSelect;
            buttonEditOnline.Enabled = bSelect && !bMultiSelect;
            DoDisplayFileProperties();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DoDASHIFPlayer();
        }


        private void buttonHTML_Click(object sender, EventArgs e)
        {
            DoHTMLPlayer();
        }

        private void TreeViewLocators_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (TreeViewLocators.SelectedNode != null)
            {
                if (TreeViewLocators.SelectedNode.Parent != null)
                {
                    buttonDASH.Enabled = false;
                    buttonAzureMediaPlayer.Enabled = false;
                    buttonHTML.Enabled = false;
                    buttonOpen.Enabled = false;
                    buttonDel.Enabled = false;

                    switch (TreeViewLocators.SelectedNode.Parent.Text)
                    {
                        case AssetInfo._smooth:
                        case AssetInfo._smooth_legacy:
                            buttonDASH.Enabled = false;
                            buttonAzureMediaPlayer.Enabled = true;
                            buttonHTML.Enabled = false;
                            buttonOpen.Enabled = false;
                            break;

                        case AssetInfo._dash_csf:
                        case AssetInfo._dash_cmaf:
                            buttonDASH.Enabled = true;
                            buttonAzureMediaPlayer.Enabled = true;
                            buttonHTML.Enabled = false;
                            buttonOpen.Enabled = false;
                            break;

                        case AssetInfo._prog_down_https_SAS:
                            buttonDASH.Enabled = false;
                            buttonAzureMediaPlayer.Enabled = (TreeViewLocators.SelectedNode.Text.ToLower().EndsWith(".mp4"));
                            buttonHTML.Enabled = false;
                            buttonOpen.Enabled = true;
                            break;

                        case AssetInfo._prog_down_http_streaming:
                            buttonDASH.Enabled = false;
                            buttonAzureMediaPlayer.Enabled = (TreeViewLocators.SelectedNode.Text.ToLower().EndsWith(".mp4"));
                            buttonHTML.Enabled = (TreeViewLocators.SelectedNode.Text.ToLower().EndsWith(".mp4"));
                            buttonOpen.Enabled = !(TreeViewLocators.SelectedNode.Text.ToLower().EndsWith(".ism"));
                            break;

                        default:
                            break;
                    }
                }
                else
                {
                    buttonDel.Enabled = true; // parent is null, so we can propose to delete the locator
                }
            }
        }

        private void playbackWithToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoAzureMediaPlayer();
        }

        private void DoAzureMediaPlayer()
        {
            if (TreeViewLocators.SelectedNode != null)
            {
                // Root node's Parent property is null, so do check
                if (TreeViewLocators.SelectedNode.Parent != null)
                {
                    switch (TreeViewLocators.SelectedNode.Parent.Text)
                    {
                        case AssetInfo._dash_csf:
                            AssetInfo.DoPlayBackWithStreamingEndpoint(typeplayer: PlayerType.AzureMediaPlayer, path: TreeViewLocators.SelectedNode.Text, DoNotRewriteURL: true, client: _amsClient, formatamp: AzureMediaPlayerFormats.Dash, mainForm: myMainForm);

                            break;

                        case AssetInfo._smooth:
                        case AssetInfo._smooth_legacy:
                            AssetInfo.DoPlayBackWithStreamingEndpoint(typeplayer: PlayerType.AzureMediaPlayer, path: TreeViewLocators.SelectedNode.Text, DoNotRewriteURL: true, client: _amsClient, formatamp: AzureMediaPlayerFormats.Smooth, mainForm: myMainForm);
                            break;

                        case AssetInfo._hls_v4:
                        case AssetInfo._hls_v3:
                        case AssetInfo._hls:
                            AssetInfo.DoPlayBackWithStreamingEndpoint(typeplayer: PlayerType.AzureMediaPlayer, path: TreeViewLocators.SelectedNode.Text, DoNotRewriteURL: true, client: _amsClient, formatamp: AzureMediaPlayerFormats.HLS, mainForm: myMainForm);
                            break;

                        case AssetInfo._prog_down_http_streaming:
                        case AssetInfo._prog_down_https_SAS:
                            AssetInfo.DoPlayBackWithStreamingEndpoint(typeplayer: PlayerType.AzureMediaPlayer, path: TreeViewLocators.SelectedNode.Text, DoNotRewriteURL: true, client: _amsClient, formatamp: AzureMediaPlayerFormats.VideoMP4, mainForm: myMainForm);
                            break;

                        default:
                            break;
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DoAzureMediaPlayer();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            DoDuplicate();
        }

        private void DoDuplicate()
        {

            var SelectedAssetBlob = ReturnSelectedBlobs().FirstOrDefault();

            if (SelectedAssetBlob != null && SelectedAssetBlob.GetType() == typeof(CloudBlockBlob))
            {
                var sourceblob = (CloudBlockBlob)SelectedAssetBlob;
                try
                {

                    string newfilename = string.Format(AMSExplorer.Properties.Resources.AssetInformation_DoDuplicate_CopyOf0, sourceblob.Name);
                    if (Program.InputBox(AMSExplorer.Properties.Resources.AssetInformation_DoDuplicate_NewName, AMSExplorer.Properties.Resources.AssetInformation_DoDuplicate_EnterTheNameOfTheNewDuplicateFile, ref newfilename) == DialogResult.OK)
                    {
                        /*
                            IAssetFile AFDup = myAsset.AssetFiles.Create(newfilename);
                            CloudMediaContext _context = Mainform._context;
                            CloudStorageAccount storageAccount;
                            storageAccount = new CloudStorageAccount(new StorageCredentials(_context.DefaultStorageAccount.Name, Mainform._credentials.DefaultStorageKey), Mainform._credentials.ReturnStorageSuffix(), true);
                            var cloudBlobClient = storageAccount.CreateCloudBlobClient();
                            IAccessPolicy writePolicy = _context.AccessPolicies.Create("writePolicy", TimeSpan.FromDays(1), AccessPermissions.Write);
                            ILocator destinationLocator = _context.Locators.CreateLocator(LocatorType.Sas, myAsset, writePolicy);

                            // Get the asset container URI and copy blobs from mediaContainer to assetContainer.
                            Uri uploadUri = new Uri(destinationLocator.Path);
                            string assetTargetContainerName = uploadUri.Segments[1];
                            CloudBlobContainer assetTargetContainer = cloudBlobClient.GetContainerReference(assetTargetContainerName);
                            var mediaBlobContainer = assetTargetContainer; // same container
                            */

                        CloudBlockBlob sourceCloudBlob, destinationBlob;

                        sourceCloudBlob = container.GetBlockBlobReference(sourceblob.Name);
                        sourceCloudBlob.FetchAttributes();

                        if (sourceCloudBlob.Properties.Length > 0)
                        {

                            destinationBlob = container.GetBlockBlobReference(newfilename);

                            //destinationBlob.DeleteIfExists();
                            destinationBlob.StartCopy(sourceCloudBlob);

                            CloudBlockBlob blob;
                            blob = (CloudBlockBlob)container.GetBlobReferenceFromServer(newfilename);

                            while (blob.CopyState.Status == CopyStatus.Pending)
                            {
                                Task.Delay(TimeSpan.FromSeconds(1d)).Wait();
                                blob.FetchAttributes();
                            }
                            destinationBlob.FetchAttributes();
                        }
                    }
                    ListAssetBlobs();
                    BuildLocatorsTree();
                }
                catch
                {
                    MessageBox.Show(AMSExplorer.Properties.Resources.AssetInformation_DoDuplicate_ErrorWhenDuplicatingThisFile);
                    ListAssetBlobs();
                    BuildLocatorsTree();
                }
            }
        }

        private async void DoUpload()
        {
            OpenFileDialog Dialog = new OpenFileDialog
            {
                Multiselect = true
            };
            if (Dialog.ShowDialog() == DialogResult.OK)
            {
                progressBarUpload.Maximum = 100 * (Dialog.FileNames.Count() + 1);
                progressBarUpload.Value = 0;
                progressBarUpload.Visible = true;

                buttonClose.Enabled = false;
                buttonUpload.Enabled = false;

                CloudBlobContainer container = GetRWContainerOfAsset();

                int i = 1;
                foreach (string file in Dialog.FileNames)
                {
                    var blob = container.GetBlockBlobReference(Path.GetFileName(file));
                    if (file.ToLower().EndsWith(".mp4")) blob.Properties.ContentType = "video/mp4";
                    //Console.WriteLine("Uploading File to container: {0}", sasUri);

                    await Task.Factory.StartNew(() => blob.UploadFromFile(file));
                    //blob.UploadFromFile(file);
                    progressBarUpload.Value = (int)(100 * i);
                    i++;

                    //await Task.Factory.StartNew(() => ProcessUploadFileToAsset(Path.GetFileName(file), file, myAssetV3));
                }
                // Refresh the asset.
                //    myAsset = Mainform._context.Assets.Where(a => a.Id == myAsset.Id).FirstOrDefault();
                progressBarUpload.Visible = false;
                buttonClose.Enabled = true;
                buttonUpload.Enabled = true;
                ListAssetBlobs();
            }
        }

        private CloudBlobContainer GetRWContainerOfAsset()
        {
            ListContainerSasInput input = new ListContainerSasInput()
            {
                Permissions = AssetContainerPermission.ReadWrite,
                ExpiryTime = DateTime.Now.AddHours(2).ToUniversalTime()
            };
            _amsClient.RefreshTokenIfNeeded();

            var response = Task.Run(async () => await _amsClient.AMSclient.Assets.ListContainerSasAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, myAssetV3.Name, input.Permissions, input.ExpiryTime)).Result;

            string uploadSasUrl = response.AssetContainerSasUrls.First();
            var sasUri = new Uri(uploadSasUrl);
            CloudBlobContainer container = new CloudBlobContainer(sasUri);
            return container;
        }
      
        
        private void duplicateFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoDuplicate();
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            DoOpenFileLocator();
        }

        private void DoOpenFileLocator()
        {
            if (TreeViewLocators.SelectedNode != null)
            {
                // Root node's Parent property is null, so do check
                if (TreeViewLocators.SelectedNode.Parent != null)
                {
                    Process.Start(TreeViewLocators.SelectedNode.Text);

                }

            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            DoDelLocator();
        }

        private void DoDelLocator()
        {
            if (TreeViewLocators.SelectedNode != null)
            {

                if (TreeViewLocators.SelectedNode.Parent == null)   // user selected the locator title
                {

                    if (System.Windows.Forms.MessageBox.Show(AMSExplorer.Properties.Resources.AssetInformation_DoDelLocator_AreYouSureThatYouWantToDeleteThisLocator, AMSExplorer.Properties.Resources.AssetInformation_DoDelLocator_LocatorDeletion, System.Windows.Forms.MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        bool Error = false;
                        try
                        {
                            _amsClient.RefreshTokenIfNeeded();

                            var locators = _amsClient.AMSclient.Assets.ListStreamingLocators(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, myAssetV3.Name).StreamingLocators;
                            _amsClient.AMSclient.StreamingLocators.Delete(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, locators[TreeViewLocators.SelectedNode.Index].Name);
                            //myAsset.Locators[TreeViewLocators.SelectedNode.Index].Delete();
                        }

                        catch
                        {

                            MessageBox.Show(AMSExplorer.Properties.Resources.AssetInformation_DoDelLocator_ErrorWhenTryingToDeleteTheLocator);
                            Error = true;
                        }
                        if (!Error) BuildLocatorsTree();
                    }
                }

            }
        }

        private void deleteLocatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoDelLocator();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DoUpload();
            BuildLocatorsTree();
        }

        private void uploadASmallFileInTheAssetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoUpload();
            BuildLocatorsTree();
        }

        private void comboBoxStreamingEndpoint_SelectedIndexChanged(object sender, EventArgs e)
        {
            BuildLocatorsTree();
        }



        private void checkBoxHttps_CheckedChanged(object sender, EventArgs e)
        {
            BuildLocatorsTree();
        }

        /*
        private void buttonAudioVideoAnalysis_Click(object sender, EventArgs e)
        {
            IEnumerable<AssetFileMetadata> manifestAssetFile = myAsset.GetMetadata();

            IAssetFile metadatafile = myContext.Files.Where(f => f.Name == myAsset.Id.Replace(Constants.AssetIdPrefix, string.Empty) + "_metadata.xml").OrderBy(f => f.LastModified).FirstOrDefault();
            if (metadatafile != null)
            {
                bool Error = false;
                if (tempMetadaLocator == null)
                {
                    try
                    {
                        var locatorTask = Task.Factory.StartNew(() =>
                        {
                            tempMetadaLocator = myContext.Locators.Create(LocatorType.Sas, metadatafile.Asset, AccessPermissions.Read, TimeSpan.FromHours(1));
                        });
                        locatorTask.Wait();
                    }
                    catch
                    {
                        Error = true;
                        MessageBox.Show(AMSExplorer.Properties.Resources.AssetInformation_buttonAudioVideoAnalysis_Click_ErrorWhenCreatingTheTemporarySASLocatorToTheMetadataFile);
                    }
                }

                try
                {
                    if (!Error)
                    {

                        AssetFileMetadata MyAssetMetada = metadatafile.GetMetadata(tempMetadaLocator);
                    }
                }
                catch
                {
                    MessageBox.Show(AMSExplorer.Properties.Resources.AssetInformation_DoOpenFiles_ErrorWhenAccessingTemporarySASLocator);
                }
            }
        }
        */

        private void buttonFileMetadata_Click(object sender, EventArgs e)
        {
            ShowFileMetadata();
        }


        private List<IListBlobItem> ReturnSelectedBlobs(bool returnAlsoDirectory = true)
        {
            var Selection = new List<IListBlobItem>();

            foreach (int selectedindex in listViewFiles.SelectedIndices)
            {
                var AF = blobs.Where(af =>
                (af.GetType() == typeof(CloudBlockBlob) && ((CloudBlockBlob)af).Name == listViewFiles.Items[selectedindex].Text)
                ||
                (returnAlsoDirectory && (af.GetType() == typeof(CloudBlobDirectory) && ((CloudBlobDirectory)af).Prefix == listViewFiles.Items[selectedindex].Text))
                )
                .FirstOrDefault();

                if (AF != null)
                {
                    Selection.Add(AF);
                }
            }
            return Selection;
        }

        private void ShowFileMetadata()
        {
            /*
            var SelectedAssetFile = ReturnSelectedBlobs().FirstOrDefault();

            if (SelectedAssetFile != null)
            {
                ILocator locator = GetTemporaryLocator();

                if (locator != null)
                {
                    AssetFileMetadata manifestAssetFile = null;
                    try
                    {
                        // Get the metadata for the asset file.
                        manifestAssetFile = SelectedAssetFile.GetMetadata(locator);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error when accessing metadata." + ex.Message);
                    }

                    if (manifestAssetFile != null)
                    {
                        MetadataInformation form = new MetadataInformation(manifestAssetFile);
                        form.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show(AMSExplorer.Properties.Resources.AssetInformation_ShowFileMetadata_ThereIsNoMetadataForThisFile);
                    }
                }
            }
            */
        }


        private void contextMenuStripDG_MouseClick_1(object sender, MouseEventArgs e)
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

        private void showMetadataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowFileMetadata();
        }


        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }


        private void contextMenuStripKey_Opening(object sender, CancelEventArgs e)
        {
        }


        private void contextMenuStripFiles_Opening(object sender, CancelEventArgs e)
        {
            bool selected = listViewFiles.SelectedItems.Count > 0;
            bool bMultiSelect = listViewFiles.SelectedItems.Count > 1;
            bool NonEncrypted = (myAsset.Options == AssetCreationOptions.None || myAsset.Options == AssetCreationOptions.CommonEncryptionProtected);

            showMetadataToolStripMenuItem.Enabled = selected && !bMultiSelect;
            toolStripMenuItemOpenFile.Enabled = selected & NonEncrypted;
            toolStripMenuItemDownloadFile.Enabled = selected;
            deleteFileToolStripMenuItem.Enabled = selected;
            duplicateFileToolStripMenuItem.Enabled = selected & NonEncrypted && !bMultiSelect;

            deleteAllFilesToolStripMenuItem.Enabled = selected;
        }

        private void filterInfoupdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoFilterInfo();
        }
        private List<AssetFilter> ReturnSelectedFilters()
        {
            var SelectedFilters = new List<AssetFilter>();
            _amsClient.RefreshTokenIfNeeded();

            var afilters = _amsClient.AMSclient.AssetFilters.List(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, myAssetV3.Name);
            foreach (DataGridViewRow Row in dataGridViewFilters.SelectedRows)
            {
                string filterName = Row.Cells[dataGridViewFilters.Columns["Name"].Index].Value.ToString();
                AssetFilter myfilter = afilters.Where(f => f.Name == filterName).FirstOrDefault();
                if (myfilter != null)
                {
                    SelectedFilters.Add(myfilter);
                }
            }
            return SelectedFilters;
        }
        private void DoFilterInfo(AssetFilter filter = null)
        {
            var filters = ReturnSelectedFilters();
            if (filter != null || filters.Count == 1)
            {
                filter = filter ?? filters.FirstOrDefault();
                DynManifestFilter form = new DynManifestFilter(_amsClient, filter, myAssetV3);

                if (form.ShowDialog() == DialogResult.OK)
                {
                    FilterCreationInfo filtertoupdate = null;
                    _amsClient.RefreshTokenIfNeeded();

                    try
                    {
                        filtertoupdate = form.GetFilterInfo;
                        filter.PresentationTimeRange = filtertoupdate.Presentationtimerange;
                        filter.Tracks = filtertoupdate.Tracks;
                        filter.FirstQuality = filtertoupdate.Firstquality;
                        _amsClient.AMSclient.AssetFilters.Update(
                            _amsClient.credentialsEntry.ResourceGroup,
                            _amsClient.credentialsEntry.AccountName,
                            myAssetV3.Name,
                            filter.Name,
                            new AssetFilter(name: filtertoupdate.Name, presentationTimeRange: filtertoupdate.Presentationtimerange, firstQuality: filtertoupdate.Firstquality, tracks: filtertoupdate.Tracks)
                            );
                        myMainForm.TextBoxLogWriteLine(AMSExplorer.Properties.Resources.AssetInformation_DoFilterInfo_AssetFilter0HasBeenUpdated, filtertoupdate.Name);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(AMSExplorer.Properties.Resources.AssetInformation_DoFilterInfo_ErrorWhenUpdatingAssetFilter + Constants.endline + Program.GetErrorMessage(e), AMSExplorer.Properties.Resources.AMSLogin_buttonExport_Click_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        myMainForm.TextBoxLogWriteLine(AMSExplorer.Properties.Resources.AssetInformation_DoFilterInfo_ErrorWhenUpdatingAssetFilter0, filter.Name, true);
                        myMainForm.TextBoxLogWriteLine(e);
                    }
                    DisplayAssetFilters();
                }
            }
        }

        private void createAnAssetFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoCreateAssetFilter();
        }

        private void DoCreateAssetFilter()
        {
            DynManifestFilter form = new DynManifestFilter(_amsClient, null, myAssetV3);

            if (form.ShowDialog() == DialogResult.OK)
            {
                _amsClient.RefreshTokenIfNeeded();

                FilterCreationInfo filterinfo = null;
                try
                {
                    filterinfo = form.GetFilterInfo;


                    _amsClient.AMSclient.AssetFilters.CreateOrUpdate(
                        _amsClient.credentialsEntry.ResourceGroup,
                        _amsClient.credentialsEntry.AccountName,
                        myAssetV3.Name,
                        filterinfo.Name,
                        new AssetFilter(name: filterinfo.Name, presentationTimeRange: filterinfo.Presentationtimerange, firstQuality: filterinfo.Firstquality, tracks: filterinfo.Tracks)
    );

                    //myAsset.AssetFilters.Create(filterinfo.Name, filterinfo.Presentationtimerange, filterinfo.Tracks);
                    myMainForm.TextBoxLogWriteLine(AMSExplorer.Properties.Resources.AssetInformation_DoCreateAssetFilter_AssetFilter0HasBeenCreated, filterinfo.Name);
                }
                catch (Exception e)
                {
                    MessageBox.Show(AMSExplorer.Properties.Resources.AssetInformation_DoCreateAssetFilter_ErrorWhenCreatingAssetFilter + Constants.endline + Program.GetErrorMessage(e), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    myMainForm.TextBoxLogWriteLine(AMSExplorer.Properties.Resources.AssetInformation_DoCreateAssetFilter_ErrorWhenCreatingAssetFilter0, (filterinfo != null && filterinfo.Name != null) ? filterinfo.Name : AMSExplorer.Properties.Resources.AssetInformation_DoCreateAssetFilter_UnknownName, true);
                    myMainForm.TextBoxLogWriteLine(e);
                }
                DisplayAssetFilters();
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoDeleteAssetFilter();
        }

        private void DoDeleteAssetFilter()
        {
            var filters = ReturnSelectedFilters();
            _amsClient.RefreshTokenIfNeeded();

            try
            {
                filters.ForEach(f => _amsClient.AMSclient.AssetFilters.Delete(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, myAssetV3.Name, f.Name));
            }

            catch
            {
                MessageBox.Show(AMSExplorer.Properties.Resources.AssetInformation_DoDeleteAssetFilter_ErrorWhenDeletingAssetFilterS, AMSExplorer.Properties.Resources.AMSLogin_buttonExport_Click_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            DisplayAssetFilters();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DoDuplicateFilter();
        }

        private void DoDuplicateFilter()
        {
            var filters = ReturnSelectedFilters();
            if (filters.Count == 1)
            {
                var sourcefilter = filters.FirstOrDefault();

                string newfiltername = sourcefilter.Name + AMSExplorer.Properties.Resources.AssetInformation_DoDuplicateFilter_Copy;
                if (Program.InputBox(AMSExplorer.Properties.Resources.AssetInformation_DoDuplicate_NewName, AMSExplorer.Properties.Resources.AssetInformation_DoDuplicateFilter_EnterTheNameOfTheNewDuplicateFilter, ref newfiltername) == DialogResult.OK)
                {
                    _amsClient.RefreshTokenIfNeeded();

                    try
                    {
                        //myAsset.AssetFilters.Create(newfiltername, sourcefilter.PresentationTimeRange, sourcefilter.Tracks);

                        _amsClient.AMSclient.AssetFilters.CreateOrUpdate(
                            _amsClient.credentialsEntry.ResourceGroup,
                            _amsClient.credentialsEntry.AccountName,
                            myAssetV3.Name,
                            newfiltername,
                            sourcefilter
                            );

                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(AMSExplorer.Properties.Resources.AssetInformation_DoDuplicateFilter_ErrorWhenDuplicatingAssetFilter + Constants.endline + Program.GetErrorMessage(e), AMSExplorer.Properties.Resources.AMSLogin_buttonExport_Click_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    DisplayAssetFilters();
                }
            }
        }

        private void DoDeleteAllFiles()
        {
            try
            {
                string question = "Delete all blobs ?";
                if (System.Windows.Forms.MessageBox.Show(question, AMSExplorer.Properties.Resources.AssetInformation_DoDeleteFiles_FileDeletion, System.Windows.Forms.MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    var Array = blobs.ToArray();
                    for (int i = 0; i < Array.Count(); i++)
                    {
                        var blob = Array[i];
                        ((CloudBlockBlob)blob).Delete();
                    }
                    ListAssetBlobs();
                    BuildLocatorsTree();
                }
            }
            catch
            {
                MessageBox.Show(AMSExplorer.Properties.Resources.AssetInformation_DoDeleteAllFiles_ErrorWhenDeletingTheFiles);
                ListAssetBlobs();
                BuildLocatorsTree();
            }

        }

        private void duplicateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoDuplicateFilter();
        }

        private void dataGridViewFilters_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DoFilterInfo();
        }

        private void comboBoxLocatorsFilters_SelectedIndexChanged(object sender, EventArgs e)
        {
            BuildLocatorsTree();
        }

        private void button1_Click_3(object sender, EventArgs e)
        {
            DoFilterInfo();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            DoCreateAssetFilter();
        }

        private void buttonDeleteFilter_Click(object sender, EventArgs e)
        {
            DoDeleteAssetFilter();
        }

        private void button1_Click_4(object sender, EventArgs e)
        {
            DoPlayWithFilter();
        }

        private void DoPlayWithFilter()
        {
            myMainForm.DoPlaySelectedAssetsOrProgramsWithPlayer(PlayerType.AzureMediaPlayer, new List<Asset>() { myAssetV3 }, ReturnSelectedFilters().FirstOrDefault().Name);
        }

        private void playWithThisFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoPlayWithFilter();
        }

        private void buttonDeleteAll_Click(object sender, EventArgs e)
        {
            DoDeleteAllFiles();
        }

        private void deleteAllFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoDeleteAllFiles();
        }

        private void buttonEditOnline_Click(object sender, EventArgs e)
        {
            DoEditFile();
        }

        /// <summary>
        /// 
        /// </summary>
        private async void DoEditFile()
        {
            var SelectedBlobs = ReturnSelectedBlobs();

            if (SelectedBlobs.Count == 1 && SelectedBlobs.FirstOrDefault() != null && SelectedBlobs.FirstOrDefault().GetType() == typeof(CloudBlockBlob))
            {
                var blobtoedit = (CloudBlockBlob)SelectedBlobs.FirstOrDefault();

                if (blobtoedit.Properties.Length > 500 * 1000)
                {
                    MessageBox.Show(AMSExplorer.Properties.Resources.AssetInformation_DoEditFile_FileIsToLargeToEditItOnline);
                    return;
                }

                try
                {
                    progressBarUpload.Maximum = 100;
                    progressBarUpload.Visible = true;
                    string tempPath = System.IO.Path.GetTempPath();
                    string filePath = Path.Combine(tempPath, blobtoedit.Name);

                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                    string contentstring = await blobtoedit.DownloadTextAsync();

                    progressBarUpload.Visible = false;

                    var editform = new EditorXMLJSON(string.Format(AMSExplorer.Properties.Resources.AssetInformation_DoEditFile_OnlineEditOf0, blobtoedit.Name), contentstring, true, false);
                    if (editform.Display() == DialogResult.OK)
                    { // OK

                        progressBarUpload.Visible = true;
                        buttonClose.Enabled = false;
                        await blobtoedit.UploadTextAsync(editform.TextData);

                        progressBarUpload.Visible = false;
                        buttonClose.Enabled = true;
                        ListAssetBlobs();
                    }
                }

                catch
                {
                    MessageBox.Show(AMSExplorer.Properties.Resources.AssetInformation_DoEditFile_ErrorWhenAccessingEditingAssetFile);
                }

            }
        }

        private void toolStripMenuItemFilesCopyClipboard_Click(object sender, EventArgs e)
        {

        }

        private void buttonSeeClearKey_Click_5(object sender, EventArgs e)
        {

        }

        private void SeeClearKey(string key)
        {
            var editform = new EditorXMLJSON(AMSExplorer.Properties.Resources.AssetInformation_DoEditFile_Value, key.ToString(), false, false);
            editform.Display();
        }

        private void dataGridViewKeys_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (e.RowIndex >= 0 && senderGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].GetType() == typeof(DataGridViewButtonCell))
            {

                //TODO - Button Clicked - to see the key
                SeeClearKey(senderGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag.ToString());
            }

        }

        private void dataGridViewAutPolOption_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            if (e.RowIndex >= 0 && senderGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].GetType() == typeof(DataGridViewButtonCell))
            {
                SeeValueInEditor(senderGrid.Rows[e.RowIndex].Cells[0].Value.ToString(), senderGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag.ToString());
            }
        }

        private void SeeValueInEditor(string dataname, string key)
        {
            var editform = new EditorXMLJSON(dataname, key, false, false);
            editform.Display();
        }

        private void buttonGenerateManifest_Click(object sender, EventArgs e)
        {
            DoGenerateManifest();

        }

        private async void DoGenerateManifest()
        {
            try
            {
                var smildata = Program.LoadAndUpdateManifestTemplate(myAssetV3, _amsClient, container);

                var editform = new EditorXMLJSON(string.Format(AMSExplorer.Properties.Resources.AssetInformation_DoEditFile_OnlineEditOf0, smildata.FileName), smildata.Content, true, false, true,
                    AMSExplorer.Properties.Resources.AssetInformation_DoGenerateManifest_PleaseCheckCarefullyTheContentOfTheGeneratedManifestAsTheToolMakesGuesses);

                if (editform.Display() == DialogResult.OK)
                { // OK

                    string tempPath = System.IO.Path.GetTempPath();
                    string filePath = Path.Combine(tempPath, smildata.FileName);

                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }

                    var doc = XDocument.Parse(editform.TextData);
                    doc.Save(filePath);

                    progressBarUpload.Visible = true;
                    buttonClose.Enabled = false;

                    CloudBlobContainer container = GetRWContainerOfAsset();

                    var blob = container.GetBlockBlobReference(Path.GetFileName(filePath));

                    await Task.Factory.StartNew(() => blob.UploadFromFile(filePath));

                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }

                }
            }
            catch
            {

            }
            progressBarUpload.Visible = false;
            buttonClose.Enabled = true;
            ListAssetBlobs();
            BuildLocatorsTree();
        }


        private void tabPageBlobs_Enter(object sender, EventArgs e)
        {
            ListAssetBlobs();
        }

        private void tabPage6_Enter(object sender, EventArgs e)
        {
            DisplayAssetFilters();

        }

        private void tabPage3_Enter(object sender, EventArgs e)
        {
            BuildLocatorsTree();
        }

        private void dataGridViewFilters_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                _amsClient.RefreshTokenIfNeeded();

                var filter = _amsClient.AMSclient.AssetFilters.Get(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, myAssetV3.Name, dataGridViewFilters.Rows[e.RowIndex].Cells[dataGridViewFilters.Columns["Name"].Index].Value.ToString());
                DoFilterInfo(filter);
            }
        }

        private void tabPagePolicy_Enter(object sender, EventArgs e)
        {
            FillLocatorComboInPolicyTab();
        }

        private void FillLocatorComboInPolicyTab()
        {
            comboBoxPolicyLocators.Items.Clear();
            comboBoxPolicyLocators.BeginUpdate();

            _amsClient.RefreshTokenIfNeeded();
            var locators = _amsClient.AMSclient.Assets.ListStreamingLocators(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, myAssetV3.Name).StreamingLocators;

            locators.ToList().ForEach(l => comboBoxPolicyLocators.Items.Add(new Item(l.Name, l.Name)));
            if (comboBoxPolicyLocators.Items.Count > 0) comboBoxPolicyLocators.SelectedIndex = 0;

            comboBoxPolicyLocators.EndUpdate();
        }



        private void DisplayPolicy(string locatorName)
        {
            if (locatorName == null) return;

            var locator = _amsClient.AMSclient.StreamingLocators.Get(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, locatorName);

            var policy = _amsClient.AMSclient.StreamingPolicies.Get(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, locator.StreamingPolicyName);

            var policyJson = JsonConvert.SerializeObject(policy, Newtonsoft.Json.Formatting.Indented);
            textBoxPolicy.Text = policyJson;
        }


        private void comboBoxPolicyLocators_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxPolicy.Text = string.Empty;

            if (comboBoxPolicyLocators.SelectedItem != null)
                DisplayPolicy((comboBoxPolicyLocators.SelectedItem as Item).Value);
        }
    }

}

