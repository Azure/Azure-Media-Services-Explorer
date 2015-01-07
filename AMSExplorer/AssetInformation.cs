//----------------------------------------------------------------------- 
// <copyright file="AssetInformation.cs" company="Microsoft">Copyright (c) Microsoft Corporation. All rights reserved.</copyright> 
// <license>
// Azure Media Services Explorer Ver. 3.1
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

namespace AMSExplorer
{
    public partial class AssetInformation : Form
    {
        public IAsset MyAsset;
        private string MyAssetType;
        public CloudMediaContext MyContext;
        public IEnumerable<IStreamingEndpoint> MyStreamingEndpoints;
        private ILocator TempLocator = null;
        private ILocator TempMetadaLocator = null;
        private List<IContentKeyAuthorizationPolicy> MyPolicies = null;
        private Mainform MyMainForm;

        public AssetInformation(Mainform mainform)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            MyMainForm = mainform;
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

        private void toolStripMenuItemPlaybackFlash_Click(object sender, EventArgs e)
        {
            DoFlashPlayer();
        }

        private void DoFlashPlayer()
        {
            if (TreeViewLocators.SelectedNode != null)
            {
                // Root node's Parent property is null, so do check
                if (TreeViewLocators.SelectedNode.Parent != null)
                    switch (TreeViewLocators.SelectedNode.Parent.Text)
                    {
                        case AssetInfo._smooth_legacy:
                        case AssetInfo._smooth:
                            AssetInfo.DoPlayBack(PlayerType.FlashAzurePage, new Uri(TreeViewLocators.SelectedNode.Text));
                            break;
                        case AssetInfo._dash:
                            AssetInfo.DoPlayBack(PlayerType.DASHAzurePage, new Uri(TreeViewLocators.SelectedNode.Text));
                            break;


                        default:
                            break;
                    }

            }
        }

        private void toolStripMenuItemPlaybackSilverlight_Click(object sender, EventArgs e)
        {
            DoSLPlayer();

        }

        private void DoSLPlayer()
        {
            if (TreeViewLocators.SelectedNode != null)
            {
                // Root node's Parent property is null, so do check
                if (TreeViewLocators.SelectedNode.Parent != null)
                {
                    switch (TreeViewLocators.SelectedNode.Parent.Text)
                    {
                        case AssetInfo._smooth_legacy:
                        case AssetInfo._smooth:
                            AssetInfo.DoPlayBack(PlayerType.SilverlightMonitoring, new Uri(TreeViewLocators.SelectedNode.Text));
                            break;

                        default:
                            break;
                    }

                }
                else
                {

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
                        case AssetInfo._dash:
                            AssetInfo.DoPlayBack(PlayerType.DASHIFRefPlayer, new Uri(TreeViewLocators.SelectedNode.Text));
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
                    toolStripMenuItemDASHAzure.Enabled = false;
                    toolStripMenuItemDASHIF.Enabled = false;
                    toolStripMenuItemDASHLiveAzure.Enabled = false;
                    toolStripMenuItemPlaybackFlashAzure.Enabled = false;
                    toolStripMenuItemPlaybackSilverlightMonitoring.Enabled = false;
                    toolStripMenuItemPlaybackMP4.Enabled = false;
                    toolStripMenuItemOpen.Enabled = false;
                    deleteLocatorToolStripMenuItem.Enabled = false;

                    if (TreeViewLocators.SelectedNode.Parent.Text.Equals(AssetInfo._smooth) | TreeViewLocators.SelectedNode.Parent.Text.Contains(AssetInfo._smooth_legacy))
                    {
                        toolStripMenuItemDASHAzure.Enabled = false;
                        toolStripMenuItemDASHLiveAzure.Enabled = false;
                        toolStripMenuItemDASHIF.Enabled = false;
                        toolStripMenuItemPlaybackFlashAzure.Enabled = true;
                        toolStripMenuItemPlaybackSilverlightMonitoring.Enabled = true;
                        toolStripMenuItemPlaybackMP4.Enabled = false;
                        toolStripMenuItemOpen.Enabled = false;
                    }
                    if (TreeViewLocators.SelectedNode.Parent.Text.Equals(AssetInfo._dash))
                    {
                        toolStripMenuItemDASHAzure.Enabled = true;
                        toolStripMenuItemDASHLiveAzure.Enabled = true;
                        toolStripMenuItemDASHIF.Enabled = true;
                        toolStripMenuItemPlaybackFlashAzure.Enabled = true;
                        toolStripMenuItemPlaybackSilverlightMonitoring.Enabled = false;
                        toolStripMenuItemPlaybackMP4.Enabled = false;
                        toolStripMenuItemOpen.Enabled = false;
                    }
                    if (TreeViewLocators.SelectedNode.Parent.Text.Equals(AssetInfo._prog_down_https_SAS))
                    {
                        toolStripMenuItemDASHAzure.Enabled = false;
                        toolStripMenuItemDASHLiveAzure.Enabled = false;
                        toolStripMenuItemDASHIF.Enabled = false;
                        toolStripMenuItemPlaybackFlashAzure.Enabled = false;
                        toolStripMenuItemPlaybackSilverlightMonitoring.Enabled = false;
                        toolStripMenuItemPlaybackMP4.Enabled = false;
                        toolStripMenuItemOpen.Enabled = true;
                    }
                    if (TreeViewLocators.SelectedNode.Parent.Text.Equals(AssetInfo._prog_down_http_streaming))
                    {
                        toolStripMenuItemDASHAzure.Enabled = false;
                        toolStripMenuItemDASHLiveAzure.Enabled = false;
                        toolStripMenuItemDASHIF.Enabled = false;
                        toolStripMenuItemPlaybackFlashAzure.Enabled = false;
                        toolStripMenuItemPlaybackSilverlightMonitoring.Enabled = false;
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
                    AssetInfo.DoPlayBack(PlayerType.MP4AzurePage, new Uri(TreeViewLocators.SelectedNode.Text));
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


        private long ListAssetFiles()
        {
            long size = 0;
            bool bfileinasset = (MyAsset.AssetFiles.Count() == 0) ? false : true;
            listViewFiles.Items.Clear();
            DGFiles.Rows.Clear();
            if (bfileinasset)
            {
                listViewFiles.BeginUpdate();
                foreach (IAssetFile file in MyAsset.AssetFiles)
                {
                    ListViewItem item = new ListViewItem(file.Name, 0);
                    if (file.IsPrimary) item.ForeColor = Color.Blue;
                    item.SubItems.Add(AssetInfo.FormatByteSize(file.ContentFileSize));
                    listViewFiles.Items.Add(item);
                    size += file.ContentFileSize;
                }
                listViewFiles.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                listViewFiles.EndUpdate();
            }
            return size;
        }

        private long ListAssetKeys()
        {
            long size = 0;
            bool bkeyinasset = (MyAsset.ContentKeys.Count() == 0) ? false : true;
            listViewKeys.Items.Clear();
            dataGridViewKeys.Rows.Clear();
            listViewAutPol.Items.Clear();
            dataGridViewAutPol.Rows.Clear();
            buttonRemoveKey.Enabled = false;

            if (bkeyinasset)
            {
                listViewKeys.BeginUpdate();
                foreach (IContentKey key in MyAsset.ContentKeys)
                {
                    ListViewItem item;
                    if (key.Name != null)
                    {
                        item = new ListViewItem(key.Name, 0);
                    }
                    else
                    {
                        item = new ListViewItem("<no name>", 0);
                    }
                    listViewKeys.Items.Add(item);
                }
                listViewKeys.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                listViewKeys.EndUpdate();
            }
            return size;
        }

        private void ListAssetDeliveryPolicies()
        {
            listViewDelPol.Items.Clear();
            buttonRemovePol.Enabled = false;

            DGDelPol.Rows.Clear();
            listViewDelPol.BeginUpdate();
            foreach (var DelPol in MyAsset.DeliveryPolicies)
            {
                ListViewItem item = new ListViewItem(DelPol.Name, 0);
                listViewDelPol.Items.Add(item);
            }
            listViewDelPol.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            listViewDelPol.EndUpdate();
        }

        private void AssetInformation_Load(object sender, EventArgs e)
        {
            labelAssetNameTitle.Text += MyAsset.Name;
            buttonSetPrimary.ForeColor = Color.Blue;

            MyAssetType = AssetInfo.GetAssetType(MyAsset);

            DGAsset.ColumnCount = 2;
            DGFiles.ColumnCount = 2;
            DGFiles.Columns[0].DefaultCellStyle.BackColor = Color.Gainsboro;
            dataGridViewAutPol.ColumnCount = 2;
            dataGridViewAutPol.Columns[0].DefaultCellStyle.BackColor = Color.Gainsboro;
            DGDelPol.ColumnCount = 2;
            DGDelPol.Columns[0].DefaultCellStyle.BackColor = Color.Gainsboro;
            dataGridViewKeys.ColumnCount = 2;
            dataGridViewKeys.Columns[0].DefaultCellStyle.BackColor = Color.Gainsboro;

            // Files in asset: headers
            long size = -1;
            if (MyAsset.State != AssetState.Deleted)
            {
                size = ListAssetFiles();
                ListAssetDeliveryPolicies();
                ListAssetKeys();
            }

            // asset info
            DGAsset.Columns[0].DefaultCellStyle.BackColor = Color.Gainsboro;
            DGAsset.Rows.Add("Name", MyAsset.Name);
            DGAsset.Rows.Add("Type", MyAssetType);
            DGAsset.Rows.Add("AssetType", MyAsset.AssetType);
            DGAsset.Rows.Add("Id", MyAsset.Id);
            DGAsset.Rows.Add("AlternateId", MyAsset.AlternateId);
            if (size != -1) DGAsset.Rows.Add("Size", AssetInfo.FormatByteSize(size));
            DGAsset.Rows.Add("State", (AssetState)MyAsset.State);
            DGAsset.Rows.Add("Created", ((DateTime)MyAsset.Created).ToLocalTime());
            DGAsset.Rows.Add("Last Modified", ((DateTime)MyAsset.LastModified).ToLocalTime());
            DGAsset.Rows.Add("Creation Options", (AssetCreationOptions)MyAsset.Options);

            if (MyAsset.State != AssetState.Deleted)
            {
                DGAsset.Rows.Add("IsStreamable", MyAsset.IsStreamable);
                DGAsset.Rows.Add("SupportsDynamicEncryption", MyAsset.SupportsDynamicEncryption);
                DGAsset.Rows.Add("Uri", MyAsset.Uri);
                DGAsset.Rows.Add("Storage Account Name", MyAsset.StorageAccount.Name);
                DGAsset.Rows.Add("Storage Account Byte used", AssetInfo.FormatByteSize(MyAsset.StorageAccount.BytesUsed));
                DGAsset.Rows.Add("Storage Account Is Default", MyAsset.StorageAccount.IsDefault);

                foreach (IAsset p_asset in MyAsset.ParentAssets)
                {
                    DGAsset.Rows.Add("Parent asset", p_asset.Name);
                    DGAsset.Rows.Add("Parent asset Id", p_asset.Id);
                }

                int i;
                IStreamingEndpoint SESelected = AssetInfo.GetBestStreamingEndpoint(MyContext);

                foreach (var se in MyStreamingEndpoints)
                {
                    i = comboBoxStreamingEndpoint.Items.Add(new Item(string.Format("{0} ({1}, {2} scale unit{3})", se.Name, se.State, se.ScaleUnits, se.ScaleUnits > 0 ? "s" : string.Empty), se.HostName));
                    if (se.Name == SESelected.Name) comboBoxStreamingEndpoint.SelectedIndex = comboBoxStreamingEndpoint.Items.Count - 1;
                }
                BuildLocatorsTree();
                buttonUpload.Enabled = true;
            }
        }

        private IStreamingEndpoint ReturnSelectedStreamingEndpoint()
        {
            string hostname = ((Item)comboBoxStreamingEndpoint.SelectedItem).Value;
            return MyStreamingEndpoints.Where(se => se.HostName == hostname).FirstOrDefault();
        }


        private void BuildLocatorsTree()
        {
            // LOCATORS TREE

            IEnumerable<IAssetFile> MyAssetFiles;
            List<Uri> ProgressiveDownloadUris;
            IStreamingEndpoint SelectedSE = ReturnSelectedStreamingEndpoint();
            bool CurrentStreamingEndpointHasRUs = SelectedSE.ScaleUnits > 0;
            Color colornodeRU = CurrentStreamingEndpointHasRUs ? Color.Black : Color.Gray;

            TreeViewLocators.BeginUpdate();
            TreeViewLocators.Nodes.Clear();
            int indexloc = -1;
            foreach (ILocator locator in MyAsset.Locators)
            {
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
                if (SelectedSE.State != StreamingEndpointState.Running) colornode = Color.Red;

                TreeNode myLocNode = new TreeNode(string.Format("{0} ({1}{2}) {3}", locator.Type.ToString(), locatorstatus, (SelectedSE.State != StreamingEndpointState.Running) ? ", Endpoint Stopped" : string.Empty, locator.Name));
                myLocNode.ForeColor = colornode;

                TreeViewLocators.Nodes.Add(myLocNode);

                TreeViewLocators.Nodes[indexloc].Nodes.Add(new TreeNode("Locator information"));

                TreeViewLocators.Nodes[indexloc].Nodes[0].Nodes.Add(new TreeNode(
               string.Format("{0}", (locator.Id))
               ));

                TreeViewLocators.Nodes[indexloc].Nodes[0].Nodes.Add(new TreeNode(
                    string.Format("Name: {0}", locator.Name)
                    ));

                TreeViewLocators.Nodes[indexloc].Nodes[0].Nodes.Add(new TreeNode(
                    string.Format("Type: {0}", locator.Type.ToString())
                    ));

                if (locator.StartTime != null)
                    TreeViewLocators.Nodes[indexloc].Nodes[0].Nodes.Add(new TreeNode(
                       string.Format("Start time: {0}", (((DateTime)locator.StartTime).ToLocalTime().ToString()))
                       ));

                if (locator.ExpirationDateTime != null)
                    TreeViewLocators.Nodes[indexloc].Nodes[0].Nodes.Add(new TreeNode(
                     string.Format("Expiration date time: {0}", (((DateTime)locator.ExpirationDateTime).ToLocalTime().ToString()))
                     ));

                if (locator.Type == LocatorType.OnDemandOrigin)
                {
                    TreeViewLocators.Nodes[indexloc].Nodes[0].Nodes.Add(new TreeNode(
                 string.Format("Path: {0}", AssetInfo.rw(locator.Path, SelectedSE))
                 ));

                    int indexn = 1;

                    TreeViewLocators.Nodes[indexloc].Nodes.Add(new TreeNode(AssetInfo._prog_down_http_streaming) { ForeColor = colornodeRU });
                    foreach (IAssetFile IAF in MyAsset.AssetFiles)
                        TreeViewLocators.Nodes[indexloc].Nodes[indexn].Nodes.Add(new TreeNode(AssetInfo.rw(locator.Path, SelectedSE, checkBoxHttps.Checked) + IAF.Name) { ForeColor = colornodeRU });
                    indexn++;

                    if (MyAssetType.StartsWith("HLS"))
                    // It is a static HLS asset, so let's propose only the standard HLS V3 locator
                    {
                        TreeViewLocators.Nodes[indexloc].Nodes.Add(new TreeNode(AssetInfo._hls));
                        TreeViewLocators.Nodes[indexloc].Nodes[indexn].Nodes.Add(new TreeNode(AssetInfo.GetHLSv3(locator.GetHlsUri().ToString())));
                        indexn++;
                    }
                    else if (MyAsset.AssetType == AssetType.SmoothStreaming || MyAsset.AssetType == AssetType.MultiBitrateMP4 || MyAsset.AssetType == AssetType.Unknown) //later to change Unknown to live archive
                    // It's not Static HLS
                    // Smooth or multi MP4
                    {
                        if (locator.GetSmoothStreamingUri() != null)
                        {
                            Color ColorSmooth = ((MyAsset.AssetType == AssetType.SmoothStreaming) && !checkBoxHttps.Checked) ? Color.Black : colornodeRU; // if not RU but aset is smooth, we can display the smooth URL as OK. If user asked for https, it works only with RU
                            TreeViewLocators.Nodes[indexloc].Nodes.Add(new TreeNode(AssetInfo._smooth) { ForeColor = ColorSmooth });
                            TreeViewLocators.Nodes[indexloc].Nodes[indexn].Nodes.Add(new TreeNode(AssetInfo.rw(locator.GetSmoothStreamingUri(), SelectedSE, checkBoxHttps.Checked).ToString()) { ForeColor = ColorSmooth });
                            indexn++;

                            // legacy smooth streaming without repeat tag (manifest v2.0)
                            TreeViewLocators.Nodes[indexloc].Nodes.Add(new TreeNode(AssetInfo._smooth_legacy) { ForeColor = colornodeRU });
                            TreeViewLocators.Nodes[indexloc].Nodes[indexn].Nodes.Add(new TreeNode(AssetInfo.GetSmoothLegacy(AssetInfo.rw(locator.GetSmoothStreamingUri(), SelectedSE, checkBoxHttps.Checked).ToString())) { ForeColor = colornodeRU });
                            indexn++;
                        }
                        if (locator.GetMpegDashUri() != null)
                        {
                            TreeViewLocators.Nodes[indexloc].Nodes.Add(new TreeNode(AssetInfo._dash) { ForeColor = colornodeRU });
                            TreeViewLocators.Nodes[indexloc].Nodes[indexn].Nodes.Add(new TreeNode(AssetInfo.rw(locator.GetMpegDashUri(), SelectedSE, checkBoxHttps.Checked).ToString()) { ForeColor = colornodeRU });
                            indexn++;
                        }
                        if (locator.GetHlsUri() != null)
                        {
                            TreeViewLocators.Nodes[indexloc].Nodes.Add(new TreeNode(AssetInfo._hls_v4) { ForeColor = colornodeRU });
                            TreeViewLocators.Nodes[indexloc].Nodes[indexn].Nodes.Add(new TreeNode(AssetInfo.rw(locator.GetHlsUri(), SelectedSE, checkBoxHttps.Checked).ToString()) { ForeColor = colornodeRU });
                            TreeViewLocators.Nodes[indexloc].Nodes.Add(new TreeNode(AssetInfo._hls_v3) { ForeColor = colornodeRU });
                            TreeViewLocators.Nodes[indexloc].Nodes[indexn + 1].Nodes.Add(new TreeNode(AssetInfo.rw(locator.GetHlsv3Uri(), SelectedSE, checkBoxHttps.Checked).ToString()) { ForeColor = colornodeRU });
                            indexn = indexn + 2;
                        }
                    }
                }

                if (locator.Type == LocatorType.Sas)
                {
                    TreeViewLocators.Nodes[indexloc].Nodes[0].Nodes.Add(new TreeNode(
                 string.Format("Path: {0}", locator.Path)
                 ));

                    TreeViewLocators.Nodes[indexloc].Nodes.Add(new TreeNode(AssetInfo._prog_down_https_SAS));

                    MyAssetFiles = MyAsset
                 .AssetFiles
                 .ToList();

                    // Generate the Progressive Download URLs for each file. 
                    ProgressiveDownloadUris =
                        MyAssetFiles.Select(af => af.GetSasUri(locator)).ToList();
                    ProgressiveDownloadUris.ForEach(uri => TreeViewLocators.Nodes[indexloc].Nodes[1].Nodes.Add(new TreeNode(uri.ToString())));
                }
            }
            TreeViewLocators.EndUpdate();
        }



        private void DoDisplayFileProperties()
        {
            if (listViewFiles.SelectedItems.Count > 0)
            {
                IAssetFile AF = MyAsset.AssetFiles.Skip(listViewFiles.SelectedIndices[0]).Take(1).FirstOrDefault();
                DGFiles.Rows.Clear();
                DGFiles.Rows.Add("Name", AF.Name);
                DGFiles.Rows.Add("Id", AF.Id);
                DGFiles.Rows.Add("File size", AssetInfo.FormatByteSize(AF.ContentFileSize));
                DGFiles.Rows.Add("Mime type", AF.MimeType);
                DGFiles.Rows.Add("Created", AF.Created.ToLocalTime());
                DGFiles.Rows.Add("Last modified", AF.LastModified.ToLocalTime());
                DGFiles.Rows.Add("Primary file", AF.IsPrimary);
                DGFiles.Rows.Add("Encrypted", AF.IsEncrypted);
                DGFiles.Rows.Add("Encryption scheme", AF.EncryptionScheme);
                DGFiles.Rows.Add("Encryption version", AF.EncryptionVersion);
                DGFiles.Rows.Add("Encryption key id", AF.EncryptionKeyId);
                DGFiles.Rows.Add("Parent asset Id", AF.ParentAssetId);
            }
        }

        private void DoDisplayDeliveryPolicyProperties()
        {
            if (listViewDelPol.SelectedItems.Count > 0)
            {
                IAssetDeliveryPolicy ADP = MyAsset.DeliveryPolicies.Skip(listViewDelPol.SelectedIndices[0]).Take(1).FirstOrDefault();
                DGDelPol.Rows.Clear();
                DGDelPol.Rows.Add("Name", ADP.Name);
                DGDelPol.Rows.Add("Id", ADP.Id);
                DGDelPol.Rows.Add("Type", ADP.AssetDeliveryPolicyType);
                DGDelPol.Rows.Add("Protocol", ADP.AssetDeliveryProtocol);
                if (ADP.AssetDeliveryConfiguration != null)
                {
                    foreach (var conf in ADP.AssetDeliveryConfiguration)
                    {
                        DGDelPol.Rows.Add("Configuration, Key", conf.Key);
                        DGDelPol.Rows.Add("Configuration, Value", conf.Value);
                    }
                }
            }
        }

        private void DoDisplayKeyProperties()
        {
            if (listViewKeys.SelectedItems.Count > 0)
            {
                IContentKey key = MyAsset.ContentKeys.Skip(listViewKeys.SelectedIndices[0]).Take(1).FirstOrDefault();
                dataGridViewKeys.Rows.Clear();
                dataGridViewKeys.Rows.Add("Name", key.Name != null ? key.Name : "<no name>");
                /*
                if (key.Name != null)
                {
                    dataGridViewKeys.Rows.Add("Name", key.Name);
                }
                else
                {
                    dataGridViewKeys.Rows.Add("Name", "<no name>");
                }
                 * */

                dataGridViewKeys.Rows.Add("Id", key.Id);
                dataGridViewKeys.Rows.Add("Content key type", key.ContentKeyType);
                dataGridViewKeys.Rows.Add("Checksum", key.Checksum);
                dataGridViewKeys.Rows.Add("Created", key.Created.ToLocalTime());
                dataGridViewKeys.Rows.Add("Las modified", key.LastModified.ToLocalTime());
                dataGridViewKeys.Rows.Add("Protection key Id", key.ProtectionKeyId);
                dataGridViewKeys.Rows.Add("Protection key type", key.ProtectionKeyType);
                dataGridViewKeys.Rows.Add("GetClearKeyValue", Convert.ToBase64String(key.GetClearKeyValue()));
                if (key.AuthorizationPolicyId != null)
                {
                    dataGridViewKeys.Rows.Add("AuthorizationPolicyId", key.AuthorizationPolicyId);
                    MyPolicies = MyContext.ContentKeyAuthorizationPolicies.Where(p => p.Id == key.AuthorizationPolicyId).ToList();
                }
                else
                {
                    MyPolicies = null;
                }
                listViewAutPol.Items.Clear();
                dataGridViewAutPol.Rows.Clear();

                switch (key.ContentKeyType)
                {

                    case ContentKeyType.CommonEncryption:
                        string DelUrl;
                        try
                        {
                            DelUrl = key.GetKeyDeliveryUrl(ContentKeyDeliveryType.PlayReadyLicense).OriginalString;
                        }
                        catch (Exception e) // Perhaps PlayReady license delivery has been activated
                        {
                            if (e.InnerException == null)
                            {
                                DelUrl = e.Message;
                            }
                            else
                            {
                                DelUrl = string.Format("{0} ({1})", e.Message, Program.GetErrorMessage(e));
                            }
                        }
                        dataGridViewKeys.Rows.Add("GetkeyDeliveryUrl", DelUrl);
                        if (MyPolicies != null)
                        {
                            listViewAutPol.BeginUpdate();
                            foreach (var policy in MyPolicies)
                            {
                                ListViewItem item = new ListViewItem(policy.Name, 0);
                                listViewAutPol.Items.Add(item);
                            }
                            listViewAutPol.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                            listViewAutPol.EndUpdate();
                            if (listViewAutPol.Items.Count > 0) listViewAutPol.Items[0].Selected = true;
                        }

                        break;

                    case ContentKeyType.EnvelopeEncryption:
                        dataGridViewKeys.Rows.Add("GetkeyDeliveryUrl", key.GetKeyDeliveryUrl(ContentKeyDeliveryType.BaselineHttp).OriginalString);
                        if (MyPolicies != null)
                        {
                            listViewAutPol.BeginUpdate();
                            foreach (var policy in MyPolicies)
                            {
                                ListViewItem item = new ListViewItem(policy.Name, 0);
                                listViewAutPol.Items.Add(item);
                            }
                            listViewAutPol.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                            listViewAutPol.EndUpdate();
                            if (listViewAutPol.Items.Count > 0) listViewAutPol.Items[0].Selected = true;
                        }

                        break;


                    default:
                        break;

                }
            }
            else
            {
                MyPolicies = null;
            }
        }

        private static string FormatXmlString(string xmlString)
        {

            if (string.IsNullOrEmpty(xmlString))
            {

                return xmlString;

            }

            else
            {

                System.Xml.Linq.XElement element = System.Xml.Linq.XElement.Parse(xmlString);

                return element.ToString();

            }

        }



        private void AssetInformation_FormClosed(object sender, FormClosedEventArgs e)
        {
            // let's delete temporary locators if any
            if (TempLocator != null)
            {
                try
                {
                    var locatorTask = Task.Factory.StartNew(() =>
                   {
                       TempLocator.Delete();
                   });
                    locatorTask.Wait();
                }
                catch
                {

                }
            }
            if (TempMetadaLocator != null)
            {
                try
                {
                    var locatorTask = Task.Factory.StartNew(() =>
                    {
                        TempMetadaLocator.Delete();
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
            DoOpenFile();
        }

        private void DoOpenFile()
        {
            if (listViewFiles.SelectedItems.Count > 0)
            {
                if (listViewFiles.SelectedItems[0] != null)
                {
                    IAssetFile AF = null;
                    ILocator locator = GetTemporaryLocator();

                    try
                    {
                        if (locator != null)
                        {
                            AF = MyAsset.AssetFiles.Skip(listViewFiles.SelectedIndices[0]).Take(1).FirstOrDefault();
                            Process.Start(AF.GetSasUri(locator).ToString());
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Error when accessing temporary SAS locator");
                    }
                }
            }
        }

        private void toolStripMenuItemDownloadFile_Click(object sender, EventArgs e)
        {
            DoDownloadFile();
        }

        private void DoDownloadFile()
        {
            if (listViewFiles.SelectedItems.Count > 0)
            {
                IAssetFile AF = MyAsset.AssetFiles.Skip(listViewFiles.SelectedIndices[0]).Take(1).FirstOrDefault();
                if (AF == null) return;

                if (folderBrowserDialogDownload.ShowDialog() == DialogResult.OK)
                {
                    int index = MyMainForm.DoGridTransferAddItem(string.Format("Download of file '{0}' from asset '{1}'", AF.Name, MyAsset.Name), TransferType.DownloadToLocal, Properties.Settings.Default.useTransferQueue);

                    // Start a worker thread that does downloading.
                    MyMainForm.DoDownloadFileFromAsset(MyAsset, AF, folderBrowserDialogDownload.SelectedPath, index);
                }
            }
        }

        private void buttonCopyStats_Click(object sender, EventArgs e)
        {
            DoAssetStats();

        }

        private void DoAssetStats()
        {

            AssetInfo MyAssetReport = new AssetInfo(MyAsset);
            MyAssetReport.CopyStatsToClipBoard();
        }

        private void buttonCreateMail_Click(object sender, EventArgs e)
        {
            DoAssetCreateMail();
        }

        private void DoAssetCreateMail()
        {
            AssetInfo MyAssetReport = new AssetInfo(MyAsset);
            MyAssetReport.CreateOutlookMail();
        }

        private void makeItPrimaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MakeItAsPrimary();
        }

        private void MakeItAsPrimary()
        {
            if (listViewFiles.SelectedItems.Count > 0)
            {
                if (listViewFiles.SelectedItems[0] != null)
                {
                    try
                    {
                        MyAsset.AssetFiles.ToList().ForEach(af => { af.IsPrimary = false; af.Update(); });
                        IAssetFile AF = MyAsset.AssetFiles.Skip(listViewFiles.SelectedIndices[0]).Take(1).FirstOrDefault();
                        AF.IsPrimary = true;
                        AF.Update();
                    }
                    catch
                    {
                        MessageBox.Show("Error when making this file primary");
                    }
                    ListAssetFiles();
                    DoDisplayFileProperties();
                }
            }
        }

        private void buttonSetPrimary_Click(object sender, EventArgs e)
        {
            MakeItAsPrimary();
        }

        private void buttonDeleteFile_Click(object sender, EventArgs e)
        {
            DoDeleteFile();
        }

        private void DoDeleteFile()
        {
            if (listViewFiles.SelectedItems.Count > 0)
            {
                if (listViewFiles.SelectedItems[0] != null)
                {
                    try
                    {
                        IAssetFile AF = MyAsset.AssetFiles.Skip(listViewFiles.SelectedIndices[0]).Take(1).FirstOrDefault();
                        string question = "Delete the file " + AF.Name + " ?";

                        if (System.Windows.Forms.MessageBox.Show(question, "File deletion", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                        {
                            AF.Delete();
                            ListAssetFiles();
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Error when deleting this file");
                        ListAssetFiles();
                    }

                }
            }
        }

        private void deleteFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoDeleteFile();
        }

        private void buttonOpenFile_Click(object sender, EventArgs e)
        {
            DoOpenFile();
        }

        private void buttonDownloadFile_Click(object sender, EventArgs e)
        {
            DoDownloadFile();
        }

        private void listViewFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool bSelect = listViewFiles.SelectedItems.Count > 0 ? true : false;
            bool NonEncrypted = (MyAsset.Options == AssetCreationOptions.None);
            buttonDeleteFile.Enabled = bSelect;
            buttonSetPrimary.Enabled = bSelect;
            buttonDownloadFile.Enabled = bSelect;
            buttonOpenFile.Enabled = bSelect;
            buttonDuplicate.Enabled = bSelect & NonEncrypted;
            buttonUpload.Enabled = true;
            buttonFileMetadata.Enabled = bSelect;
            DoDisplayFileProperties();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DoDASHIFPlayer();
        }

        private void buttonFlash_Click(object sender, EventArgs e)
        {
            DoFlashPlayer();
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
                    buttonDashAzure.Enabled = false;
                    buttonDashLiveAzure.Enabled = false;
                    buttonFlash.Enabled = false;
                    buttonSLMonitor.Enabled = false;
                    buttonHTML.Enabled = false;
                    buttonOpen.Enabled = false;
                    buttonDel.Enabled = false;

                    switch (TreeViewLocators.SelectedNode.Parent.Text)
                    {
                        case AssetInfo._smooth:
                        case AssetInfo._smooth_legacy:

                            buttonDASH.Enabled = false;
                            buttonDashAzure.Enabled = false;
                            buttonDashLiveAzure.Enabled = false;
                            buttonFlash.Enabled = true;
                            buttonSLMonitor.Enabled = true;
                            buttonHTML.Enabled = false;
                            buttonOpen.Enabled = false;
                            break;

                        case AssetInfo._dash:
                            buttonDASH.Enabled = true;
                            buttonDashAzure.Enabled = true;
                            buttonDashLiveAzure.Enabled = true;
                            buttonFlash.Enabled = true;
                            buttonSLMonitor.Enabled = false;
                            buttonHTML.Enabled = false;
                            buttonOpen.Enabled = false;
                            break;

                        case AssetInfo._prog_down_https_SAS:
                            buttonDASH.Enabled = false;
                            buttonDashAzure.Enabled = false;
                            buttonDashLiveAzure.Enabled = false;
                            buttonFlash.Enabled = false;
                            buttonSLMonitor.Enabled = false;
                            buttonHTML.Enabled = false;
                            buttonOpen.Enabled = true;
                            break;

                        case AssetInfo._prog_down_http_streaming:
                            buttonDASH.Enabled = false;
                            buttonDashAzure.Enabled = false;
                            buttonDashLiveAzure.Enabled = false;
                            buttonFlash.Enabled = false;
                            buttonSLMonitor.Enabled = false;
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
            DoDashAzurePlayer();
        }

        private void DoDashAzurePlayer()
        {
            if (TreeViewLocators.SelectedNode != null)
            {
                // Root node's Parent property is null, so do check
                if (TreeViewLocators.SelectedNode.Parent != null)
                {
                    switch (TreeViewLocators.SelectedNode.Parent.Text)
                    {
                        case AssetInfo._dash:
                            AssetInfo.DoPlayBack(PlayerType.DASHAzurePage, new Uri(TreeViewLocators.SelectedNode.Text));
                            break;


                        default:
                            break;
                    }

                }
            }
        }

        private void buttonSLMonitor_Click(object sender, EventArgs e)
        {
            DoSLPlayer();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DoDashAzurePlayer();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            DoDuplicate();
        }

        private void DoDuplicate()
        {
            if (listViewFiles.SelectedItems.Count > 0)
            {
                if (listViewFiles.SelectedItems[0] != null)
                {
                    try
                    {
                        IAssetFile AF = MyAsset.AssetFiles.Skip(listViewFiles.SelectedIndices[0]).Take(1).FirstOrDefault();

                        if (!Mainform.havestoragecredentials)
                        { // No blob credentials.
                            MessageBox.Show("Please specify the account storage key in the login window.");
                        }
                        else
                        {
                            string newfilename = "Copy of " + AF.Name;
                            if (Mainform.InputBox("New name", "Enter the name of the new duplicate file:", ref newfilename) == DialogResult.OK)
                            {
                                IAssetFile AFDup = MyAsset.AssetFiles.Create(newfilename);
                                CloudMediaContext _context = Mainform._context;
                                CloudStorageAccount storageAccount;
                                storageAccount = new CloudStorageAccount(new StorageCredentials(_context.DefaultStorageAccount.Name, Mainform._credentials.StorageKey), true);
                                var cloudBlobClient = storageAccount.CreateCloudBlobClient();
                                IAccessPolicy writePolicy = _context.AccessPolicies.Create("writePolicy", TimeSpan.FromDays(1), AccessPermissions.Write);
                                ILocator destinationLocator = _context.Locators.CreateLocator(LocatorType.Sas, MyAsset, writePolicy);

                                // Get the asset container URI and copy blobs from mediaContainer to assetContainer.
                                Uri uploadUri = new Uri(destinationLocator.Path);
                                string assetTargetContainerName = uploadUri.Segments[1];
                                CloudBlobContainer assetTargetContainer = cloudBlobClient.GetContainerReference(assetTargetContainerName);
                                var mediaBlobContainer = assetTargetContainer; // same container

                                CloudBlockBlob sourceCloudBlob, destinationBlob;

                                sourceCloudBlob = mediaBlobContainer.GetBlockBlobReference(AF.Name);
                                sourceCloudBlob.FetchAttributes();

                                if (sourceCloudBlob.Properties.Length > 0)
                                {

                                    destinationBlob = assetTargetContainer.GetBlockBlobReference(AFDup.Name);

                                    destinationBlob.DeleteIfExists();
                                    destinationBlob.StartCopyFromBlob(sourceCloudBlob);

                                    CloudBlockBlob blob;
                                    blob = (CloudBlockBlob)assetTargetContainer.GetBlobReferenceFromServer(AFDup.Name);

                                    while (blob.CopyState.Status == CopyStatus.Pending)
                                    {
                                        Task.Delay(TimeSpan.FromSeconds(1d)).Wait();
                                    }
                                    destinationBlob.FetchAttributes();
                                    AFDup.ContentFileSize = sourceCloudBlob.Properties.Length;
                                    AFDup.Update();

                                    MyAsset.Update();

                                    destinationLocator.Delete();
                                    writePolicy.Delete();

                                    // Refresh the asset.
                                    MyAsset = _context.Assets.Where(a => a.Id == MyAsset.Id).FirstOrDefault();

                                }
                            }

                            ListAssetFiles();
                        }

                    }
                    catch
                    {
                        MessageBox.Show("Error when duplicating this file");
                        ListAssetFiles();
                    }

                }

            }
        }

        private async void DoUpload()
        {
            OpenFileDialog Dialog = new OpenFileDialog();
            Dialog.Multiselect = true;
            if (Dialog.ShowDialog() == DialogResult.OK)
            {
                progressBarUpload.Maximum = 100 * Dialog.FileNames.Count();
                progressBarUpload.Visible = true;
                buttonClose.Enabled = false;
                buttonUpload.Enabled = false;
                foreach (string file in Dialog.FileNames)
                {
                    await Task.Factory.StartNew(() => ProcessUploadFileToAsset(Path.GetFileName(file), file, MyAsset));
                }
                // Refresh the asset.
                MyAsset = Mainform._context.Assets.Where(a => a.Id == MyAsset.Id).FirstOrDefault();
                progressBarUpload.Visible = false;
                buttonClose.Enabled = true;
                buttonUpload.Enabled = true;
                ListAssetFiles();
            }
        }

        private void ProcessUploadFileToAsset(string SafeFileName, string FileName, IAsset MyAsset)
        {
            try
            {
                IAssetFile UploadedAssetFile = MyAsset.AssetFiles.Create(SafeFileName);
                UploadedAssetFile.UploadProgressChanged += MyUploadProgressChanged;
                UploadedAssetFile.Upload(FileName as string);
            }
            catch
            {
                MessageBox.Show("Error when uploading the file");
            }
        }

        private void MyUploadProgressChanged(object sender, UploadProgressChangedEventArgs e)
        {
            progressBarUpload.BeginInvoke(new Action(() => progressBarUpload.Value = (int)e.Progress), null);

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

                    if (System.Windows.Forms.MessageBox.Show("Are you sure that you want to delete this locator ?", "Locator deletion", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                    {
                        bool Error = false;
                        try
                        {
                            MyAsset.Locators[TreeViewLocators.SelectedNode.Index].Delete();
                        }

                        catch
                        {

                            MessageBox.Show("Error when trying to delete the locator.");
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
        }

        private void uploadASmallFileInTheAssetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoUpload();
        }

        private void comboBoxStreamingEndpoint_SelectedIndexChanged(object sender, EventArgs e)
        {
            BuildLocatorsTree();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void listViewDelPol_SelectedIndexChanged(object sender, EventArgs e)
        {

            bool bSelect = listViewDelPol.SelectedItems.Count > 0 ? true : false;
            buttonRemovePol.Enabled = bSelect;
            DoDisplayDeliveryPolicyProperties();
        }

        private void buttonRemovePol_Click(object sender, EventArgs e)
        {
            DoRemovePol();
        }

        private void DoRemovePol()
        {
            if (listViewDelPol.SelectedItems.Count > 0)
            {
                if (listViewDelPol.SelectedItems[0] != null)
                {
                    IAssetDeliveryPolicy DP = MyAsset.DeliveryPolicies.Skip(listViewDelPol.SelectedIndices[0]).Take(1).FirstOrDefault();
                    if (DP != null)
                    {
                        string DPid = DP.Id;
                        string question = string.Format("This will remove the policy '{0}' from the asset.\nDo you want to also DELETE the policy from the Azure Media Services account ?", DP.Name);
                        DialogResult DR = MessageBox.Show(question, "Delivery Policy removal", MessageBoxButtons.YesNoCancel);

                        if (DR == DialogResult.Yes || DR == DialogResult.No)
                        {
                            string step = "removing";

                            try
                            {
                                MyAsset.DeliveryPolicies.Remove(DP);

                                if (DR == DialogResult.Yes) // user wants also to delete the policy
                                {
                                    step = "deleting";
                                    IAssetDeliveryPolicy policyrefreshed = MyContext.AssetDeliveryPolicies.Where(p => p.Id == DPid).FirstOrDefault();
                                    if (policyrefreshed != null)
                                    {
                                        policyrefreshed.Delete();
                                    }
                                }
                            }
                            catch (Exception e)
                            {
                                string messagestr = string.Format("Error when {0} the delivery policy.", step);
                                if (e.InnerException != null)
                                {
                                    messagestr += Constants.endline + Program.GetErrorMessage(e);
                                }
                                MessageBox.Show(messagestr);
                            }
                            ListAssetDeliveryPolicies();
                        }
                    }
                }
            }

        }

        private void listViewKeys_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonRemoveKey.Enabled = listViewKeys.SelectedItems.Count > 0;
            DoDisplayKeyProperties();
        }


        private void listViewAutPol_SelectedIndexChanged(object sender, EventArgs e)
        {
            DoDisplayAuthorizationPolicyProperties();
        }

        private void DoDisplayAuthorizationPolicyProperties()
        {
            bool DisplayButGetToken = false;

            if (listViewAutPol.SelectedItems.Count > 0)
            {
                IContentKeyAuthorizationPolicy policy = MyPolicies.Skip(listViewAutPol.SelectedIndices[0]).Take(1).FirstOrDefault();

                dataGridViewAutPol.Rows.Clear();
                dataGridViewAutPol.Rows.Add("Name", policy.Name != null ? policy.Name : "<no name>");
                dataGridViewAutPol.Rows.Add("Id", policy.Id);
                IList<IContentKeyAuthorizationPolicyOption> objIList_option = policy.Options;

                foreach (var option in objIList_option)
                {
                    dataGridViewAutPol.Rows.Add("Option Name", option.Name);
                    dataGridViewAutPol.Rows.Add("Option KeyDeliveryConfiguration", FormatXmlString(option.KeyDeliveryConfiguration));
                    dataGridViewAutPol.Rows.Add("Option KeyDeliveryType", option.KeyDeliveryType);
                    List<ContentKeyAuthorizationPolicyRestriction> objList_restriction = option.Restrictions;
                    foreach (var restriction in objList_restriction)
                    {
                        dataGridViewAutPol.Rows.Add("Option restriction Name", restriction.Name);
                        dataGridViewAutPol.Rows.Add("Option restriction KeyRestrictionType", (ContentKeyRestrictionType)restriction.KeyRestrictionType);
                        if ((ContentKeyRestrictionType)restriction.KeyRestrictionType == ContentKeyRestrictionType.TokenRestricted) DisplayButGetToken = true;
                        dataGridViewAutPol.Rows.Add("Option restriction Requirements", FormatXmlString(restriction.Requirements));
                    }
                }
            }
            buttonGetTestToken.Enabled = DisplayButGetToken;
        }

        private void buttonGetTestToken_Click(object sender, EventArgs e)
        {
            DoGetTestToken();
        }

        private void DoGetTestToken()
        {
            if (listViewKeys.SelectedItems.Count > 0)
            {
                IContentKey key = MyAsset.ContentKeys.Skip(listViewKeys.SelectedIndices[0]).Take(1).FirstOrDefault();
                string testToken = AssetInfo.GetTestToken(MyAsset, key.ContentKeyType, MyContext);
                MyMainForm.TextBoxLogWriteLine("The authorization test token is :\n{0}", testToken);
                System.Windows.Forms.Clipboard.SetText(testToken);
                MessageBox.Show(string.Format("The test token below has been be copied to the log window and clipboard.\n\n{0}", testToken), "Test token copied");
            }
        }

        private void buttonDashLiveAzure_Click(object sender, EventArgs e)
        {
            DoDashLiveAzurePlayer();
        }

        private void DoDashLiveAzurePlayer()
        {
            if (TreeViewLocators.SelectedNode != null)
            {
                // Root node's Parent property is null, so do check
                if (TreeViewLocators.SelectedNode.Parent != null)
                {
                    switch (TreeViewLocators.SelectedNode.Parent.Text)
                    {
                        case AssetInfo._dash:
                            AssetInfo.DoPlayBack(PlayerType.DASHLiveAzure, new Uri(TreeViewLocators.SelectedNode.Text));
                            break;


                        default:
                            break;
                    }

                }
            }
        }

        private void checkBoxHttps_CheckedChanged(object sender, EventArgs e)
        {
            BuildLocatorsTree();
        }

        private void buttonAudioVideoAnalysis_Click(object sender, EventArgs e)
        {
            IEnumerable<AssetFileMetadata> manifestAssetFile = MyAsset.GetMetadata();

            IAssetFile metadatafile = MyContext.Files.Where(f => f.Name == MyAsset.Id.Replace(Constants.AssetIdPrefix, string.Empty) + "_metadata.xml").OrderBy(f => f.LastModified).FirstOrDefault();
            if (metadatafile != null)
            {
                bool Error = false;
                if (TempMetadaLocator == null)
                {
                    try
                    {
                        var locatorTask = Task.Factory.StartNew(() =>
                        {
                            TempMetadaLocator = MyContext.Locators.Create(LocatorType.Sas, metadatafile.Asset, AccessPermissions.Read, TimeSpan.FromHours(1));
                        });
                        locatorTask.Wait();
                    }
                    catch
                    {
                        Error = true;
                        MessageBox.Show("Error when creating the temporary SAS locator to the metadata file.");
                    }
                }

                try
                {
                    if (!Error)
                    {

                        AssetFileMetadata MyAssetMetada = metadatafile.GetMetadata(TempMetadaLocator);

                    }
                }
                catch
                {
                    MessageBox.Show("Error when accessing temporary SAS locator");
                }


            }
        }

        private void buttonFileMetadata_Click(object sender, EventArgs e)
        {
            ShowFileMetadata();
        }

        private void ShowFileMetadata()
        {
            if (listViewFiles.SelectedItems.Count > 0)
            {
                if (listViewFiles.SelectedItems[0] != null)
                {
                    IAssetFile AF = null;
                    ILocator locator = GetTemporaryLocator();

                    if (locator != null)
                    {
                        AssetFileMetadata manifestAssetFile = null;
                        try
                        {
                            AF = MyAsset.AssetFiles.Skip(listViewFiles.SelectedIndices[0]).Take(1).FirstOrDefault();
                            // Get the metadata for the asset file.
                            manifestAssetFile = AF.GetMetadata(locator);
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
                            MessageBox.Show("There is no metadata for this file.");
                        }
                    }
                }
            }
        }

        private ILocator GetTemporaryLocator()
        {
            if (TempLocator == null) // no temp locator, let's create it
            {
                try
                {
                    var locatorTask = Task.Factory.StartNew(() =>
                    {
                        TempLocator = MyContext.Locators.Create(LocatorType.Sas, MyAsset, AccessPermissions.Read, TimeSpan.FromHours(1));

                    });
                    locatorTask.Wait();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error when creating the temporary SAS locator." + ex.Message);
                }
            }
            return TempLocator;
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

        private void buttonDelKey_Click(object sender, EventArgs e)
        {
            DoDemoveKey();
        }

        private void DoDemoveKey()
        {
            if (listViewKeys.SelectedItems.Count > 0)
            {
                IContentKey key = MyAsset.ContentKeys.Skip(listViewKeys.SelectedIndices[0]).Take(1).FirstOrDefault();
                string keyid = key.Id;
                string question = string.Format("This will remove the key '{0}' from the asset.\nDo you want to also DELETE the key from the Azure Media Services account ?", key.Name);
                DialogResult DR = MessageBox.Show(question, "Key removal", MessageBoxButtons.YesNoCancel);

                if (DR == DialogResult.Yes || DR == DialogResult.No)
                {
                    string step = "removing";
                    try
                    {
                        MyAsset.ContentKeys.Remove(key);
                        if (DR == DialogResult.Yes) // user wants also to delete the key
                        {
                            step = "deleting";
                            IContentKey keyrefreshed = MyContext.ContentKeys.Where(k => k.Id == keyid).FirstOrDefault();
                            if (keyrefreshed != null)
                            {
                                keyrefreshed.Delete();
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        string messagestr = string.Format("Error when {0} the key", step);
                        if (e.InnerException != null)
                        {
                            messagestr += Constants.endline + Program.GetErrorMessage(e);
                        }
                        MessageBox.Show(messagestr);
                    }
                    ListAssetKeys();
                }
            }
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoDemoveKey();
        }

        private void getTestTokenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoGetTestToken();
        }

        private void removeDeliveryPolicyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoRemovePol();
        }

        private void contextMenuStripDelPol_Opening(object sender, CancelEventArgs e)
        {
            removeDeliveryPolicyToolStripMenuItem.Enabled = (listViewDelPol.SelectedItems.Count > 0);
        }

        private void contextMenuStripKey_Opening(object sender, CancelEventArgs e)
        {
            removeKeyToolStripMenuItem.Enabled = (listViewKeys.SelectedItems.Count > 0);

        }

        private void contextMenuStripAuthPol_Opening(object sender, CancelEventArgs e)
        {
            getTestTokenToolStripMenuItem.Enabled = (listViewAutPol.SelectedItems.Count > 0);
        }

        private void contextMenuStripFiles_Opening(object sender, CancelEventArgs e)
        {
            bool selected = (listViewFiles.SelectedItems.Count > 0);
            makeItPrimaryToolStripMenuItem.Enabled = selected;
            showMetadataToolStripMenuItem.Enabled = selected;
            toolStripMenuItemOpenFile.Enabled = selected;
            toolStripMenuItemDownloadFile.Enabled = selected;
            deleteFileToolStripMenuItem.Enabled = selected;
            duplicateFileToolStripMenuItem.Enabled = selected;
        }
    }
}
