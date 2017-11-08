﻿//----------------------------------------------------------------------------------------------
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

namespace AMSExplorer
{
    public partial class AssetInformation : Form
    {
        public IAsset myAsset;
        private string myAssetType;
        private CloudMediaContext myContext;
        public IEnumerable<IStreamingEndpoint> myStreamingEndpoints;
        private ILocator tempLocator = null;
        private ILocator tempMetadaLocator = null;
        private IContentKeyAuthorizationPolicy myAuthPolicy = null;
        private Mainform myMainForm;
        private bool oktobuildlocator = false;
        private ManifestTimingData myassetmanifesttimingdata = null;

        public AssetInformation(Mainform mainform, CloudMediaContext context)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            myMainForm = mainform;
            myContext = context;
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
                        case AssetInfo._dash:
                            AssetInfo.DoPlayBackWithStreamingEndpoint(typeplayer: PlayerType.DASHIFRefPlayer, Urlstr: TreeViewLocators.SelectedNode.Text, DoNotRewriteURL: true, context: myContext, mainForm: myMainForm);
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

                    if (TreeViewLocators.SelectedNode.Parent.Text.Equals(AssetInfo._smooth) | TreeViewLocators.SelectedNode.Parent.Text.Contains(AssetInfo._smooth_legacy))
                    {
                        toolStripMenuItemAzureMediaPlayer.Enabled = true;
                        toolStripMenuItemDASHIF.Enabled = false;
                        toolStripMenuItemPlaybackMP4.Enabled = false;
                        toolStripMenuItemOpen.Enabled = false;
                    }
                    if (TreeViewLocators.SelectedNode.Parent.Text.Equals(AssetInfo._dash))
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
                    AssetInfo.DoPlayBackWithStreamingEndpoint(typeplayer: PlayerType.MP4AzurePage, Urlstr: TreeViewLocators.SelectedNode.Text, DoNotRewriteURL: true, context: myContext, mainForm: myMainForm);
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
            bool bfileinasset = (myAsset.AssetFiles.Count() == 0) ? false : true;
            listViewFiles.Items.Clear();
            DGFiles.Rows.Clear();
            if (bfileinasset)
            {
                listViewFiles.BeginUpdate();
                foreach (IAssetFile file in myAsset.AssetFiles)
                {
                    ListViewItem item = new ListViewItem(file.Name, 0);
                    if (file.IsPrimary)
                    {
                        item.ForeColor = Color.Blue;
                    }
                    if (file.ContentFileSize == 0)
                    {
                        item.ForeColor = Color.Red;
                    }
                    if (file.AssetFileOptions == AssetFileOptions.Fragmented)
                    {
                        item.ForeColor = Color.DarkGoldenrod;
                    }
                    item.SubItems.Add(AssetInfo.FormatByteSize(file.ContentFileSize));
                    listViewFiles.Items.Add(item);
                    size += file.ContentFileSize;
                }
                listViewFiles.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                listViewFiles.Items[0].Selected = true;
                listViewFiles.EndUpdate();
            }

            // Generate manifest button
            var mp4AssetFiles = myAsset.AssetFiles.ToList().Where(f => f.Name.EndsWith(".mp4", StringComparison.OrdinalIgnoreCase) || f.Name.EndsWith(".m4a", StringComparison.OrdinalIgnoreCase));
            var ismAssetFiles = myAsset.AssetFiles.ToList().Where(f => f.Name.EndsWith(".ism", StringComparison.OrdinalIgnoreCase));
            buttonGenerateManifest.Enabled = (ismAssetFiles.Count() == 0 && mp4AssetFiles.Count() > 0);

            return size;
        }

        private long ListAssetKeys()
        {
            long size = 0;
            bool bkeyinasset = (myAsset.ContentKeys.Count() == 0) ? false : true;
            listViewKeys.Items.Clear();
            dataGridViewKeys.Rows.Clear();
            listViewAutPolOptions.Items.Clear();
            dataGridViewAutPolOption.Rows.Clear();
            buttonRemoveKey.Enabled = false;

            if (bkeyinasset)
            {
                listViewKeys.BeginUpdate();
                foreach (IContentKey key in myAsset.ContentKeys)
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
            buttonRemoveDelPol.Enabled = false;

            DGDelPol.Rows.Clear();
            listViewDelPol.BeginUpdate();
            foreach (var DelPol in myAsset.DeliveryPolicies)
            {
                ListViewItem item = new ListViewItem(DelPol.Name, 0);
                listViewDelPol.Items.Add(item);
            }
            listViewDelPol.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            listViewDelPol.EndUpdate();
        }

        private void AssetInformation_Load(object sender, EventArgs e)
        {
            labelAssetNameTitle.Text += myAsset.Name;
            buttonSetPrimary.ForeColor = Color.Blue;

            myAssetType = AssetInfo.GetAssetType(myAsset);

            DGAsset.ColumnCount = 2;
            DGFiles.ColumnCount = 2;
            DGFiles.Columns[0].DefaultCellStyle.BackColor = Color.Gainsboro;
            dataGridViewAutPolOption.ColumnCount = 2;
            dataGridViewAutPolOption.Columns[0].DefaultCellStyle.BackColor = Color.Gainsboro;
            DGDelPol.ColumnCount = 2;
            DGDelPol.Columns[0].DefaultCellStyle.BackColor = Color.Gainsboro;
            dataGridViewKeys.ColumnCount = 2;
            dataGridViewKeys.Columns[0].DefaultCellStyle.BackColor = Color.Gainsboro;

            // Files in asset: headers
            long size = -1;
            if (myAsset.State != AssetState.Deleted)
            {
                size = ListAssetFiles();
                ListAssetDeliveryPolicies();
                ListAssetKeys();
            }

            // asset info
            DGAsset.Columns[0].DefaultCellStyle.BackColor = Color.Gainsboro;
            DGAsset.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_Name, myAsset.Name);
            DGAsset.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_Type, myAssetType);
            DGAsset.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_AssetType, myAsset.AssetType);
            DGAsset.Rows.Add("Id", myAsset.Id);
            DGAsset.Rows.Add("AlternateId", myAsset.AlternateId);
            if (size != -1) DGAsset.Rows.Add("Size", AssetInfo.FormatByteSize(size));
            DGAsset.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_State, (AssetState)myAsset.State);
            DGAsset.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_Created, ((DateTime)myAsset.Created).ToLocalTime().ToString("G"));
            DGAsset.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_LastModified, ((DateTime)myAsset.LastModified).ToLocalTime().ToString("G"));
            DGAsset.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_CreationOptions, (AssetCreationOptions)myAsset.Options);
            DGAsset.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_FormatOption, (AssetFormatOption)myAsset.FormatOption);

            var program = myContext.Programs.Where(p => p.AssetId == myAsset.Id).FirstOrDefault();
            if (program != null) // Asset is linked to a Program
            {
                DGAsset.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_ProgramId, program.Id);
            }

            if (myAsset.State != AssetState.Deleted)
            {
                DGAsset.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_IsStreamable, myAsset.IsStreamable);
                DGAsset.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_SupportsDynamicEncryption, myAsset.SupportsDynamicEncryption);
                DGAsset.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_StorageUrl, myAsset.Uri);
                DGAsset.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_StorageAccountName, myAsset.StorageAccount.Name);
                DGAsset.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_StorageAccountByteUsed, AssetInfo.FormatByteSize(myAsset.StorageAccount.BytesUsed));
                DGAsset.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_StorageAccountIsDefault, myAsset.StorageAccount.IsDefault);

                try
                {
                    foreach (IAsset p_asset in myAsset.ParentAssets)
                    {
                        DGAsset.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_ParentAsset, p_asset.Name);
                        DGAsset.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_ParentAssetId, p_asset.Id);
                    }
                }
                catch
                {
                    DGAsset.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_ParentAssetS, AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_ErrorDeleted);
                }


                IStreamingEndpoint SESelected = AssetInfo.GetBestStreamingEndpoint(myContext);

                foreach (var se in myStreamingEndpoints)
                {
                    comboBoxStreamingEndpoint.Items.Add(new Item(string.Format(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_012ScaleUnit, se.Name, se.State, StreamingEndpointInformation.ReturnTypeSE(se)), se.HostName));
                    if (se.Name == SESelected.Name) comboBoxStreamingEndpoint.SelectedIndex = comboBoxStreamingEndpoint.Items.Count - 1;

                    foreach (var custom in se.CustomHostNames)
                    {
                        comboBoxStreamingEndpoint.Items.Add(new Item(string.Format(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_012ScaleUnitCustomHostname3, se.Name, se.State, StreamingEndpointInformation.ReturnTypeSE(se), custom), custom));
                    }
                }
                buttonUpload.Enabled = true;
            }

            DisplayAssetFilters();
            oktobuildlocator = true;
            BuildLocatorsTree();

            listViewFiles.Tag = -1;
            listViewFiles.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(ListViewItemComparerQuickNoDate.ListView_ColumnClick);
        }

        private void DisplayAssetFilters()
        {
            // let's refresh the asset
            myAsset = myContext.Assets.Where(a => a.Id == myAsset.Id).FirstOrDefault();
            if (myAsset == null) return; // Error!

            dataGridViewFilters.ColumnCount = 7;
            dataGridViewFilters.Columns[0].HeaderText = AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_Name;
            dataGridViewFilters.Columns[0].Name = AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_Name;
            dataGridViewFilters.Columns[1].HeaderText = "Id";
            dataGridViewFilters.Columns[1].Name = "Id";
            dataGridViewFilters.Columns[2].HeaderText = AMSExplorer.Properties.Resources.AssetInformation_DisplayAssetFilters_TrackRules;
            dataGridViewFilters.Columns[2].Name = AMSExplorer.Properties.Resources.AssetInformation_DisplayAssetFilters_Rules;
            dataGridViewFilters.Columns[3].HeaderText = AMSExplorer.Properties.Resources.AssetInformation_DisplayAssetFilters_StartDHMS;
            dataGridViewFilters.Columns[3].Name = AMSExplorer.Properties.Resources.AssetInformation_DisplayAssetFilters_Start;
            dataGridViewFilters.Columns[4].HeaderText = AMSExplorer.Properties.Resources.AssetInformation_DisplayAssetFilters_EndDHMS;
            dataGridViewFilters.Columns[4].Name = AMSExplorer.Properties.Resources.AssetInformation_DisplayAssetFilters_End;
            dataGridViewFilters.Columns[5].HeaderText = AMSExplorer.Properties.Resources.AssetInformation_DisplayAssetFilters_DVRDHMS;
            dataGridViewFilters.Columns[5].Name = AMSExplorer.Properties.Resources.AssetInformation_DisplayAssetFilters_DVR;
            dataGridViewFilters.Columns[6].HeaderText = AMSExplorer.Properties.Resources.AssetInformation_DisplayAssetFilters_LiveBackoffDHMS;
            dataGridViewFilters.Columns[6].Name = AMSExplorer.Properties.Resources.AssetInformation_DisplayAssetFilters_LiveBackoff;

            dataGridViewFilters.Rows.Clear();
            comboBoxLocatorsFilters.Items.Clear(); //drop list in locator tab
            comboBoxLocatorsFilters.BeginUpdate();
            comboBoxLocatorsFilters.Items.Add(new Item(string.Empty, null));

            if (myAsset.AssetFilters.Count() > 0 && myassetmanifesttimingdata == null)
            {
                myassetmanifesttimingdata = AssetInfo.GetManifestTimingData(myAsset);
            }

            foreach (var filter in myAsset.AssetFilters)
            {
                string s = null;
                string e = null;
                string d = null;
                string l = null;

                if (filter.PresentationTimeRange != null)
                {
                    ulong? start = filter.PresentationTimeRange.StartTimestamp;
                    ulong? end = filter.PresentationTimeRange.EndTimestamp;
                    TimeSpan? dvr = filter.PresentationTimeRange.PresentationWindowDuration;
                    TimeSpan? live = filter.PresentationTimeRange.LiveBackoffDuration;

                    double dscale = (filter.PresentationTimeRange.Timescale != null) ?
                        (double)filter.PresentationTimeRange.Timescale
                        : (double)TimeSpan.TicksPerSecond;

                    double dscaleoffset = (myassetmanifesttimingdata.TimeScale != null) ?
                        (double)myassetmanifesttimingdata.TimeScale
                        : (double)TimeSpan.TicksPerSecond;

                    s = ReturnFilterTextWithOffSet(start, dscale, myassetmanifesttimingdata.TimestampOffset, dscaleoffset, "min");
                    e = ReturnFilterTextWithOffSet(end, dscale, myassetmanifesttimingdata.TimestampOffset, dscaleoffset, "max");
                    d = ReturnFilterText(dvr, AMSExplorer.Properties.Resources.AssetInformation_DisplayAssetFilters_Max);
                    l = ReturnFilterText(live, AMSExplorer.Properties.Resources.AssetInformation_DisplayAssetFilters_Min);
                }
                try
                {
                    var nbtrack = filter.Tracks.Count;
                    int rowi = dataGridViewFilters.Rows.Add(filter.Name, filter.Id, filter.Tracks.Count, s, e, d, l);
                }
                catch
                {
                    int rowi = dataGridViewFilters.Rows.Add(filter.Name, filter.Id, "Error", s, e, d, l);
                }

                // droplist
                comboBoxLocatorsFilters.Items.Add(new Item(AMSExplorer.Properties.Resources.AssetInformation_DisplayAssetFilters_AssetFilter + filter.Name, filter.Name));
            }


            myContext.Filters.ToList().ForEach(g => comboBoxLocatorsFilters.Items.Add(new Item(AMSExplorer.Properties.Resources.AssetInformation_DisplayAssetFilters_GlobalFilter + g.Name, g.Name)));
            comboBoxLocatorsFilters.SelectedIndex = 0;
            comboBoxLocatorsFilters.EndUpdate();
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

        private static string ReturnFilterTextWithOffSet(ulong? value, double scalevalue, ulong offset, double scaleoffset, string defaultwhennull)
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

        private IStreamingEndpoint ReturnSelectedStreamingEndpoint()
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

            IEnumerable<IAssetFile> MyAssetFiles;
            List<Uri> ProgressiveDownloadUris;
            IStreamingEndpoint SelectedSE = ReturnSelectedStreamingEndpoint();
            string SelectedSEHostName = ReturnSelectedStreamingEndpointHostname();


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


            if (SelectedSE != null)
            {
                bool CurrentStreamingEndpointCanDoDynPackaging = StreamingEndpointInformation.CanDoDynPackaging(SelectedSE);
                Color colornodeRU = CurrentStreamingEndpointCanDoDynPackaging ? Color.Black : Color.Gray;
                string filter = ((Item)comboBoxLocatorsFilters.SelectedItem).Value;

                TreeViewLocators.BeginUpdate();
                TreeViewLocators.Nodes.Clear();
                int indexloc = -1;
                foreach (ILocator locator in myAsset.Locators)
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

                    TreeViewLocators.Nodes[indexloc].Nodes.Add(new TreeNode(AMSExplorer.Properties.Resources.AssetInformation_BuildLocatorsTree_LocatorInformation));

                    TreeViewLocators.Nodes[indexloc].Nodes[0].Nodes.Add(new TreeNode(
                   string.Format("{0}", (locator.Id))
                   ));

                    TreeViewLocators.Nodes[indexloc].Nodes[0].Nodes.Add(new TreeNode(
                        string.Format(AMSExplorer.Properties.Resources.AssetInformation_BuildLocatorsTree_Name0, locator.Name)
                        ));

                    TreeViewLocators.Nodes[indexloc].Nodes[0].Nodes.Add(new TreeNode(
                        string.Format(AMSExplorer.Properties.Resources.AssetInformation_BuildLocatorsTree_Type0, locator.Type.ToString())
                        ));

                    if (locator.StartTime != null)
                        TreeViewLocators.Nodes[indexloc].Nodes[0].Nodes.Add(new TreeNode(
                           string.Format(AMSExplorer.Properties.Resources.AssetInformation_BuildLocatorsTree_StartTime0, (((DateTime)locator.StartTime).ToLocalTime().ToString("G")))
                           ));

                    if (locator.ExpirationDateTime != null)
                        TreeViewLocators.Nodes[indexloc].Nodes[0].Nodes.Add(new TreeNode(
                         string.Format(AMSExplorer.Properties.Resources.AssetInformation_BuildLocatorsTree_ExpirationDateTime0, (((DateTime)locator.ExpirationDateTime).ToLocalTime().ToString("G")))
                         ));

                    if (locator.Type == LocatorType.OnDemandOrigin)
                    {
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
                                                                                                                                                                             // It's not Static HLS
                                                                                                                                                                             // Smooth or multi MP4
                        {
                            if (protocolSmooth && locator.GetSmoothStreamingUri() != null)
                            {
                                Color ColorSmooth = ((myAsset.AssetType == AssetType.SmoothStreaming) && !checkBoxHttps.Checked) ? Color.Black : colornodeRU; // if not RU but aset is smooth, we can display the smooth URL as OK. If user asked for https, it works only with RU
                                TreeViewLocators.Nodes[indexloc].Nodes.Add(new TreeNode(AssetInfo._smooth) { ForeColor = ColorSmooth });
                                foreach (var uri in AssetInfo.GetSmoothStreamingUris(locator, SelectedSE, filter, checkBoxHttps.Checked, SelectedSEHostName))
                                {
                                    TreeViewLocators.Nodes[indexloc].Nodes[indexn].Nodes.Add(new TreeNode(uri.AbsoluteUri) { ForeColor = ColorSmooth });
                                }
                                indexn++;

                                TreeViewLocators.Nodes[indexloc].Nodes.Add(new TreeNode(AssetInfo._smooth_legacy) { ForeColor = colornodeRU });
                                foreach (var uri in AssetInfo.GetSmoothStreamingLegacyUris(locator, SelectedSE, filter, checkBoxHttps.Checked, SelectedSEHostName))
                                {
                                    TreeViewLocators.Nodes[indexloc].Nodes[indexn].Nodes.Add(new TreeNode(uri.AbsoluteUri) { ForeColor = colornodeRU });
                                }
                                indexn++;
                            }
                            if (protocolDASH && locator.GetMpegDashUri() != null)
                            {
                                TreeViewLocators.Nodes[indexloc].Nodes.Add(new TreeNode(AssetInfo._dash) { ForeColor = colornodeRU });
                                foreach (var uri in AssetInfo.GetMpegDashUris(locator, SelectedSE, filter, checkBoxHttps.Checked, SelectedSEHostName))
                                {
                                    TreeViewLocators.Nodes[indexloc].Nodes[indexn].Nodes.Add(new TreeNode(uri.AbsoluteUri) { ForeColor = colornodeRU });
                                }
                                indexn++;
                            }
                            if (protocolHLS && locator.GetHlsUri() != null)
                            {
                                TreeViewLocators.Nodes[indexloc].Nodes.Add(new TreeNode(AssetInfo._hls_v4) { ForeColor = colornodeRU });
                                foreach (var uri in AssetInfo.GetHlsUris(locator, SelectedSE, filter, checkBoxHttps.Checked, SelectedSEHostName))
                                {
                                    TreeViewLocators.Nodes[indexloc].Nodes[indexn].Nodes.Add(new TreeNode(uri.AbsoluteUri) { ForeColor = colornodeRU });
                                }
                                TreeViewLocators.Nodes[indexloc].Nodes.Add(new TreeNode(AssetInfo._hls_v3) { ForeColor = colornodeRU });
                                foreach (var uri in AssetInfo.GetHlsv3Uris(locator, SelectedSE, filter, checkBoxHttps.Checked, SelectedSEHostName))
                                {
                                    TreeViewLocators.Nodes[indexloc].Nodes[indexn + 1].Nodes.Add(new TreeNode(uri.AbsoluteUri) { ForeColor = colornodeRU });
                                }
                                indexn = indexn + 2;
                            }
                        }
                    }

                    if (locator.Type == LocatorType.Sas)
                    {
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
                    }
                }
                TreeViewLocators.EndUpdate();
            }

        }

        private void DoDisplayFileProperties()
        {
            var SelectedAssetFiles = ReturnSelectedAssetFiles();

            if (SelectedAssetFiles.Count > 0)
            {
                IAssetFile AF = SelectedAssetFiles.FirstOrDefault();
                if (AF == null)
                    return;

                DGFiles.Rows.Clear();
                DGFiles.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_Name, AF.Name);
                DGFiles.Rows.Add("Id", AF.Id);
                DGFiles.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_DoDisplayFileProperties_FileSize, AssetInfo.FormatByteSize(AF.ContentFileSize));
                DGFiles.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_DoDisplayFileProperties_MimeType, AF.MimeType);
                DGFiles.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_Created, AF.Created.ToLocalTime().ToString("G"));
                DGFiles.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_DoDisplayFileProperties_LastModified, AF.LastModified.ToLocalTime().ToString("G"));
                DGFiles.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_DoDisplayFileProperties_PrimaryFile, AF.IsPrimary);
                DGFiles.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_DoDisplayFileProperties_Options, AF.AssetFileOptions);
                DGFiles.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_DoDisplayFileProperties_Encrypted, AF.IsEncrypted);
                DGFiles.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_DoDisplayFileProperties_EncryptionScheme, AF.EncryptionScheme);
                DGFiles.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_DoDisplayFileProperties_EncryptionVersion, AF.EncryptionVersion);
                DGFiles.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_DoDisplayFileProperties_EncryptionKeyId, AF.EncryptionKeyId);
                DGFiles.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_ParentAssetId, AF.ParentAssetId);
            }
        }

        private void DoDisplayDeliveryPolicyProperties()
        {
            if (listViewDelPol.SelectedItems.Count > 0)
            {
                IAssetDeliveryPolicy ADP = myAsset.DeliveryPolicies.Skip(listViewDelPol.SelectedIndices[0]).Take(1).FirstOrDefault();
                DGDelPol.Rows.Clear();
                DGDelPol.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_Name, ADP.Name);
                DGDelPol.Rows.Add("Id", ADP.Id);
                DGDelPol.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_Type, ADP.AssetDeliveryPolicyType);
                DGDelPol.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_DoDisplayDeliveryPolicyProperties_Protocol, ADP.AssetDeliveryProtocol);
                if (ADP.AssetDeliveryConfiguration != null)
                {
                    int i = 0;
                    foreach (var conf in ADP.AssetDeliveryConfiguration)
                    {
                        DGDelPol.Rows.Add(string.Format("Config #{0}, \"{1}\"", i, conf.Key), conf.Value);
                        i++;
                    }
                }
            }
        }

        private void DoDisplayKeyPropertiesAndAutOptions()
        {
            buttonRemoveAuthPolOption.Enabled = false;
            buttonRemoveAuthPol.Enabled = false;
            buttonGetTestToken.Enabled = false;

            if (listViewKeys.SelectedItems.Count > 0)
            {
                IContentKey key = myAsset.ContentKeys.Skip(listViewKeys.SelectedIndices[0]).Take(1).FirstOrDefault();
                dataGridViewKeys.Rows.Clear();
                dataGridViewKeys.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_Name, key.Name != null ? key.Name : "<no name>");
                dataGridViewKeys.Rows.Add("Id", key.Id);
                dataGridViewKeys.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_DoDisplayKeyPropertiesAndAutOptions_ContentKeyType, key.ContentKeyType);
                dataGridViewKeys.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_DoDisplayKeyPropertiesAndAutOptions_Checksum, key.Checksum);
                dataGridViewKeys.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_Created, key.Created.ToLocalTime().ToString("G"));
                dataGridViewKeys.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_DoDisplayFileProperties_LastModified, key.LastModified.ToLocalTime().ToString("G"));
                dataGridViewKeys.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_DoDisplayKeyPropertiesAndAutOptions_ProtectionKeyId, key.ProtectionKeyId);
                dataGridViewKeys.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_DoDisplayKeyPropertiesAndAutOptions_ProtectionKeyType, key.ProtectionKeyType);
                int i = dataGridViewKeys.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_DoDisplayKeyPropertiesAndAutOptions_ClearKeyValue, AMSExplorer.Properties.Resources.AssetInformation_DoDisplayKeyPropertiesAndAutOptions_SeeClearKey);
                DataGridViewButtonCell btn = new DataGridViewButtonCell();
                dataGridViewKeys.Rows[i].Cells[1] = btn;
                dataGridViewKeys.Rows[i].Cells[1].Value = AMSExplorer.Properties.Resources.AssetInformation_DoDisplayKeyPropertiesAndAutOptions_SeeClearKey2;
                dataGridViewKeys.Rows[i].Cells[1].Tag = Convert.ToBase64String(key.GetClearKeyValue());

                listViewAutPolOptions.Items.Clear();
                dataGridViewAutPolOption.Rows.Clear();

                if (key.AuthorizationPolicyId != null)
                {
                    dataGridViewKeys.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_DoDisplayKeyPropertiesAndAutOptions_AuthorizationPolicyId, key.AuthorizationPolicyId);
                    myAuthPolicy = myContext.ContentKeyAuthorizationPolicies.Where(p => p.Id == key.AuthorizationPolicyId).FirstOrDefault();
                    if (myAuthPolicy != null)
                    {
                        buttonRemoveAuthPol.Enabled = true;
                        dataGridViewKeys.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_DoDisplayKeyPropertiesAndAutOptions_AuthorizationPolicyName, myAuthPolicy.Name);
                        listViewAutPolOptions.BeginUpdate();
                        foreach (var option in myAuthPolicy.Options)
                        {
                            ListViewItem item = new ListViewItem((string.IsNullOrEmpty(option.Name) ? AMSExplorer.Properties.Resources.AssetInformation_DoDisplayKeyPropertiesAndAutOptions_NoName : option.Name), 0);
                            listViewAutPolOptions.Items.Add(item);
                        }
                        listViewAutPolOptions.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                        listViewAutPolOptions.EndUpdate();
                        if (listViewAutPolOptions.Items.Count > 0) listViewAutPolOptions.Items[0].Selected = true;
                    }
                }
                else
                {
                    myAuthPolicy = null;
                }
            }
            else
            {
                myAuthPolicy = null;
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
            var SelectedAssetFiles = ReturnSelectedAssetFiles();

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
        }

        private void toolStripMenuItemDownloadFile_Click(object sender, EventArgs e)
        {
            DoDownloadFiles();
        }

        private void DoDownloadFiles()
        {
            var SelectedAssetFiles = ReturnSelectedAssetFiles();

            if (SelectedAssetFiles.Count > 0)
            {
                CommonOpenFileDialog openFolderDialog = new CommonOpenFileDialog() { IsFolderPicker = true, InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos) };
                if (openFolderDialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    // let's check if this overwites existing files
                    var listfiles = SelectedAssetFiles.ToList().Where(f => File.Exists(openFolderDialog.FileName + @"\\" + f.Name)).Select(f => openFolderDialog.FileName + @"\\" + f.Name).ToList();
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
                        foreach (var assetfile in SelectedAssetFiles)
                        {
                            var response = myMainForm.DoGridTransferAddItem(string.Format(AMSExplorer.Properties.Resources.AssetInformation_DoDownloadFiles_DownloadOfFile0FromAsset1, assetfile.Name, myAsset.Name), TransferType.DownloadToLocal, true);
                            // Start a worker thread that does downloading.
                            myMainForm.DoDownloadFileFromAsset(myAsset, assetfile, openFolderDialog.FileName, response);
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
            AssetInfo MyAssetReport = new AssetInfo(myAsset);
            StringBuilder SB = MyAssetReport.GetStats();
            var tokenDisplayForm = new EditorXMLJSON(AMSExplorer.Properties.Resources.AssetInformation_DoDisplayAssetStats_AssetReport, SB.ToString(), false, false, false);
            tokenDisplayForm.Display();
        }

        private void buttonCreateMail_Click(object sender, EventArgs e)
        {
            DoAssetCreateMail();
        }

        private void DoAssetCreateMail()
        {
            AssetInfo MyAssetReport = new AssetInfo(myAsset);
            MyAssetReport.CreateOutlookMail();
        }

        private void makeItPrimaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MakeItAsPrimary();
        }

        private void MakeItAsPrimary()
        {
            var SelectedAssetFile = ReturnSelectedAssetFiles().FirstOrDefault();

            if (SelectedAssetFile != null)
            {
                try
                {
                    myAsset.AssetFiles.ToList().ForEach(af => { af.IsPrimary = false; af.Update(); });
                    SelectedAssetFile.IsPrimary = true;
                    SelectedAssetFile.Update();
                }
                catch
                {
                    MessageBox.Show(AMSExplorer.Properties.Resources.AssetInformation_MakeItAsPrimary_ErrorWhenMakingThisFilePrimary);
                }
                ListAssetFiles();
                DoDisplayFileProperties();
            }
        }

        private void buttonSetPrimary_Click(object sender, EventArgs e)
        {
            MakeItAsPrimary();
        }

        private void buttonDeleteFile_Click(object sender, EventArgs e)
        {
            DoDeleteFiles();
        }

        private void DoDeleteFiles()
        {
            var SelectedAssetFiles = ReturnSelectedAssetFiles();

            if (SelectedAssetFiles.Count > 0)
            {
                string question = SelectedAssetFiles.Count == 1 ? string.Format(AMSExplorer.Properties.Resources.AssetInformation_DoDeleteFiles_DeleteTheFile0, SelectedAssetFiles.FirstOrDefault().Name) : string.Format(AMSExplorer.Properties.Resources.AssetInformation_DoDeleteFiles_DeleteThese0Files, SelectedAssetFiles.Count);

                if (System.Windows.Forms.MessageBox.Show(question, AMSExplorer.Properties.Resources.AssetInformation_DoDeleteFiles_FileDeletion, System.Windows.Forms.MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    try
                    {
                        foreach (var assetfile in SelectedAssetFiles)
                        {
                            assetfile.Delete();
                        }
                        ListAssetFiles();
                        BuildLocatorsTree();

                    }
                    catch
                    {
                        MessageBox.Show(AMSExplorer.Properties.Resources.AssetInformation_DoDeleteFiles_ErrorWhenDeletingFileS);
                        ListAssetFiles();
                        BuildLocatorsTree();
                    }
                }
            }
        }

        private void deleteFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoDeleteFiles();
            BuildLocatorsTree();
        }

        private void buttonOpenFile_Click(object sender, EventArgs e)
        {
            DoOpenFiles();
        }

        private void buttonDownloadFile_Click(object sender, EventArgs e)
        {
            DoDownloadFiles();
        }

        private void listViewFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool bSelect = listViewFiles.SelectedItems.Count > 0;
            bool bMultiSelect = listViewFiles.SelectedItems.Count > 1;
            bool NonEncrypted = (myAsset.Options == AssetCreationOptions.None || myAsset.Options == AssetCreationOptions.CommonEncryptionProtected);

            buttonDeleteFile.Enabled = bSelect;
            buttonDeleteAll.Enabled = true;
            buttonSetPrimary.Enabled = bSelect && !bMultiSelect;
            buttonDownloadFile.Enabled = bSelect;
            buttonOpenFile.Enabled = bSelect & NonEncrypted;
            buttonDuplicate.Enabled = bSelect & NonEncrypted && !bMultiSelect;
            buttonUpload.Enabled = true;
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

                        case AssetInfo._dash:
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
                        case AssetInfo._dash:
                            AssetInfo.DoPlayBackWithStreamingEndpoint(typeplayer: PlayerType.AzureMediaPlayer, Urlstr: TreeViewLocators.SelectedNode.Text, DoNotRewriteURL: true, context: myContext, formatamp: AzureMediaPlayerFormats.Dash, mainForm: myMainForm);

                            break;

                        case AssetInfo._smooth:
                        case AssetInfo._smooth_legacy:
                            AssetInfo.DoPlayBackWithStreamingEndpoint(typeplayer: PlayerType.AzureMediaPlayer, Urlstr: TreeViewLocators.SelectedNode.Text, DoNotRewriteURL: true, context: myContext, formatamp: AzureMediaPlayerFormats.Smooth, mainForm: myMainForm);
                            break;

                        case AssetInfo._hls_v4:
                        case AssetInfo._hls_v3:
                        case AssetInfo._hls:
                            AssetInfo.DoPlayBackWithStreamingEndpoint(typeplayer: PlayerType.AzureMediaPlayer, Urlstr: TreeViewLocators.SelectedNode.Text, DoNotRewriteURL: true, context: myContext, formatamp: AzureMediaPlayerFormats.HLS, mainForm: myMainForm);
                            break;

                        case AssetInfo._prog_down_http_streaming:
                        case AssetInfo._prog_down_https_SAS:
                            AssetInfo.DoPlayBackWithStreamingEndpoint(typeplayer: PlayerType.AzureMediaPlayer, Urlstr: TreeViewLocators.SelectedNode.Text, DoNotRewriteURL: true, context: myContext, formatamp: AzureMediaPlayerFormats.VideoMP4, mainForm: myMainForm);
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
            var SelectedAssetFile = ReturnSelectedAssetFiles().FirstOrDefault();

            if (SelectedAssetFile != null)
            {
                try
                {
                    if (!Mainform.havestoragecredentials)
                    { // No blob credentials.
                        MessageBox.Show(AMSExplorer.Properties.Resources.AssetInformation_DoDuplicate_PleaseSpecifyTheAccountStorageKeyInTheLoginWindow);
                    }
                    else
                    {
                        string newfilename = string.Format(AMSExplorer.Properties.Resources.AssetInformation_DoDuplicate_CopyOf0, SelectedAssetFile.Name);
                        if (Program.InputBox(AMSExplorer.Properties.Resources.AssetInformation_DoDuplicate_NewName, AMSExplorer.Properties.Resources.AssetInformation_DoDuplicate_EnterTheNameOfTheNewDuplicateFile, ref newfilename) == DialogResult.OK)
                        {
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

                            CloudBlockBlob sourceCloudBlob, destinationBlob;

                            sourceCloudBlob = mediaBlobContainer.GetBlockBlobReference(SelectedAssetFile.Name);
                            sourceCloudBlob.FetchAttributes();

                            if (sourceCloudBlob.Properties.Length > 0)
                            {

                                destinationBlob = assetTargetContainer.GetBlockBlobReference(AFDup.Name);

                                destinationBlob.DeleteIfExists();
                                destinationBlob.StartCopy(sourceCloudBlob);

                                CloudBlockBlob blob;
                                blob = (CloudBlockBlob)assetTargetContainer.GetBlobReferenceFromServer(AFDup.Name);

                                while (blob.CopyState.Status == CopyStatus.Pending)
                                {
                                    Task.Delay(TimeSpan.FromSeconds(1d)).Wait();
                                    blob.FetchAttributes();
                                }
                                destinationBlob.FetchAttributes();
                                AFDup.ContentFileSize = sourceCloudBlob.Properties.Length;
                                AFDup.Update();

                                myAsset.Update();

                                destinationLocator.Delete();
                                writePolicy.Delete();

                                // Refresh the asset.
                                myAsset = _context.Assets.Where(a => a.Id == myAsset.Id).FirstOrDefault();

                            }
                        }

                        ListAssetFiles();
                        BuildLocatorsTree();
                    }

                }
                catch
                {
                    MessageBox.Show(AMSExplorer.Properties.Resources.AssetInformation_DoDuplicate_ErrorWhenDuplicatingThisFile);
                    ListAssetFiles();
                    BuildLocatorsTree();
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
                    await Task.Factory.StartNew(() => ProcessUploadFileToAsset(Path.GetFileName(file), file, myAsset));
                }
                // Refresh the asset.
                myAsset = Mainform._context.Assets.Where(a => a.Id == myAsset.Id).FirstOrDefault();
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
            catch (Exception ex)
            {
                MessageBox.Show(AMSExplorer.Properties.Resources.AssetInformation_ProcessUploadFileToAsset_ErrorWhenUploadingTheFile + Constants.endline + Program.GetErrorMessage(ex));
            }
        }

        private void MyUploadProgressChanged(object sender, UploadProgressChangedEventArgs e)
        {
            try
            {
                progressBarUpload.BeginInvoke(new Action(() => progressBarUpload.Value = (int)e.Progress), null);
            }
            catch
            {

            }
        }

        private void ProcessDownloadFileToAsset(IAssetFile assetFile, string destfolderpath)
        {
            try
            {
                assetFile.DownloadProgressChanged += MyDownloadProgressChanged;
                assetFile.Download(destfolderpath);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void MyDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
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

                    if (System.Windows.Forms.MessageBox.Show(AMSExplorer.Properties.Resources.AssetInformation_DoDelLocator_AreYouSureThatYouWantToDeleteThisLocator, AMSExplorer.Properties.Resources.AssetInformation_DoDelLocator_LocatorDeletion, System.Windows.Forms.MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        bool Error = false;
                        try
                        {
                            myAsset.Locators[TreeViewLocators.SelectedNode.Index].Delete();
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void listViewDelPol_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool bSelect = listViewDelPol.SelectedItems.Count > 0 ? true : false;
            buttonRemoveDelPol.Enabled = bSelect;
            DoDisplayDeliveryPolicyProperties();
        }

        private void buttonRemovePol_Click(object sender, EventArgs e)
        {
            DoRemoveDeliveryPol();
        }

        private void DoRemoveDeliveryPol()
        {
            if (listViewDelPol.SelectedItems.Count > 0)
            {
                if (listViewDelPol.SelectedItems[0] != null)
                {
                    IAssetDeliveryPolicy DP = myAsset.DeliveryPolicies.Skip(listViewDelPol.SelectedIndices[0]).Take(1).FirstOrDefault();
                    if (DP != null)
                    {
                        string DPid = DP.Id;
                        string question = string.Format(AMSExplorer.Properties.Resources.AssetInformation_DoRemoveDeliveryPol_ThisWillRemoveThePolicy0FromTheAssetNDoYouWantToAlsoDELETEThePolicyFromTheAzureMediaServicesAccount, DP.Name);
                        DialogResult DR = MessageBox.Show(question, AMSExplorer.Properties.Resources.AssetInformation_DoRemoveDeliveryPol_DeliveryPolicyRemoval, MessageBoxButtons.YesNoCancel);

                        if (DR == DialogResult.Yes || DR == DialogResult.No)
                        {
                            string step = AMSExplorer.Properties.Resources.AssetInformation_DoRemoveDeliveryPol_Removing;

                            try
                            {
                                myAsset.DeliveryPolicies.Remove(DP);

                                if (DR == DialogResult.Yes) // user wants also to delete the policy
                                {
                                    step = AMSExplorer.Properties.Resources.AssetInformation_DoRemoveDeliveryPol_Deleting;
                                    IAssetDeliveryPolicy policyrefreshed = myContext.AssetDeliveryPolicies.Where(p => p.Id == DPid).FirstOrDefault();
                                    if (policyrefreshed != null)
                                    {
                                        policyrefreshed.Delete();
                                    }
                                }
                            }
                            catch (Exception e)
                            {
                                string messagestr = string.Format(AMSExplorer.Properties.Resources.AssetInformation_DoRemoveDeliveryPol_ErrorWhen0TheDeliveryPolicy, step);
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
            buttonRemoveKey.Enabled = buttonAddExistingAutPol.Enabled = listViewKeys.SelectedItems.Count > 0;
            buttonRemoveAuthPol.Enabled = buttonRemoveAuthPolOption.Enabled = buttonGetTestToken.Enabled = false;
            DoDisplayKeyPropertiesAndAutOptions();
        }


        private void listViewAutPolOption_SelectedIndexChanged(object sender, EventArgs e)
        {
            DoDisplayAuthorizationPolicyOption();
        }

        private void DoDisplayAuthorizationPolicyOption()
        {
            bool DisplayButGetToken = false;

            if (listViewAutPolOptions.SelectedItems.Count > 0 && myAuthPolicy != null)
            {
                dataGridViewAutPolOption.Rows.Clear();

                IContentKeyAuthorizationPolicyOption option = myAuthPolicy.Options.Skip(listViewAutPolOptions.SelectedIndices[0]).Take(1).FirstOrDefault();
                if (option != null) // Token option
                {
                    dataGridViewAutPolOption.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_Name, option.Name != null ? option.Name : AMSExplorer.Properties.Resources.AssetInformation_DoDisplayKeyPropertiesAndAutOptions_NoName);
                    dataGridViewAutPolOption.Rows.Add("Id", option.Id);

                    // Key delivery configuration

                    int i = dataGridViewAutPolOption.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_DoDisplayAuthorizationPolicyOption_KeyDeliveryConfiguration, AMSExplorer.Properties.Resources.AssetInformation_DoDisplayAuthorizationPolicyOption_Null);
                    if (option.KeyDeliveryConfiguration != null)
                    {
                        DataGridViewButtonCell btn = new DataGridViewButtonCell();
                        dataGridViewAutPolOption.Rows[i].Cells[1] = btn;
                        dataGridViewAutPolOption.Rows[i].Cells[1].Value = AMSExplorer.Properties.Resources.AssetInformation_DoDisplayAuthorizationPolicyOption_SeeValue;
                        dataGridViewAutPolOption.Rows[i].Cells[1].Tag = option.KeyDeliveryConfiguration;
                    }

                    dataGridViewAutPolOption.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_DoDisplayAuthorizationPolicyOption_KeyDeliveryType, option.KeyDeliveryType);

                    List<ContentKeyAuthorizationPolicyRestriction> objList_restriction = option.Restrictions;
                    foreach (var restriction in objList_restriction)
                    {
                        dataGridViewAutPolOption.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_DoDisplayAuthorizationPolicyOption_RestrictionName, restriction.Name);
                        dataGridViewAutPolOption.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_DoDisplayAuthorizationPolicyOption_RestrictionKeyRestrictionType, (ContentKeyRestrictionType)restriction.KeyRestrictionType);
                        if ((ContentKeyRestrictionType)restriction.KeyRestrictionType == ContentKeyRestrictionType.TokenRestricted)
                        {
                            DisplayButGetToken = true;
                        }
                        if (restriction.Requirements != null)
                        {
                            // Restriction Requirements
                            i = dataGridViewAutPolOption.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_DoDisplayAuthorizationPolicyOption_RestrictionRequirements, AMSExplorer.Properties.Resources.AssetInformation_DoDisplayAuthorizationPolicyOption_Null);
                            if (restriction.Requirements != null)
                            {
                                DataGridViewButtonCell btn2 = new DataGridViewButtonCell();
                                dataGridViewAutPolOption.Rows[i].Cells[1] = btn2;
                                dataGridViewAutPolOption.Rows[i].Cells[1].Value = AMSExplorer.Properties.Resources.AssetInformation_DoDisplayAuthorizationPolicyOption_SeeValue;
                                dataGridViewAutPolOption.Rows[i].Cells[1].Tag = restriction.Requirements;

                                TokenRestrictionTemplate tokenTemplate = TokenRestrictionTemplateSerializer.Deserialize(restriction.Requirements);
                                dataGridViewAutPolOption.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_DoDisplayAuthorizationPolicyOption_TokenType, tokenTemplate.TokenType);

                                i = dataGridViewAutPolOption.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_DoDisplayAuthorizationPolicyOption_PrimaryVerificationKey, AMSExplorer.Properties.Resources.AssetInformation_DoDisplayAuthorizationPolicyOption_Null);
                                if (tokenTemplate.PrimaryVerificationKey != null)
                                {
                                    dataGridViewAutPolOption.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_DoDisplayAuthorizationPolicyOption_TokenVerificationKeyType, (tokenTemplate.PrimaryVerificationKey.GetType() == typeof(SymmetricVerificationKey)) ? "Symmetric" : "Asymmetric (X509)");
                                    if (tokenTemplate.PrimaryVerificationKey.GetType() == typeof(SymmetricVerificationKey))
                                    {
                                        var verifkey = (SymmetricVerificationKey)tokenTemplate.PrimaryVerificationKey;
                                        btn2 = new DataGridViewButtonCell();
                                        dataGridViewAutPolOption.Rows[i].Cells[1] = btn2;
                                        dataGridViewAutPolOption.Rows[i].Cells[1].Value = AMSExplorer.Properties.Resources.AssetInformation_DoDisplayAuthorizationPolicyOption_SeeKeyValue;
                                        dataGridViewAutPolOption.Rows[i].Cells[1].Tag = Convert.ToBase64String(verifkey.KeyValue);
                                    }
                                }


                                foreach (var verifkey in tokenTemplate.AlternateVerificationKeys)
                                {
                                    i = dataGridViewAutPolOption.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_DoDisplayAuthorizationPolicyOption_AlternateVerificationKey, AMSExplorer.Properties.Resources.AssetInformation_DoDisplayAuthorizationPolicyOption_Null);
                                    if (verifkey != null)
                                    {
                                        dataGridViewAutPolOption.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_DoDisplayAuthorizationPolicyOption_TokenVerificationKeyType, (verifkey.GetType() == typeof(SymmetricVerificationKey)) ? AMSExplorer.Properties.Resources.AssetInformation_DoDisplayAuthorizationPolicyOption_Symmetric : AMSExplorer.Properties.Resources.AssetInformation_DoDisplayAuthorizationPolicyOption_AsymmetricX509);
                                        if (verifkey.GetType() == typeof(SymmetricVerificationKey))
                                        {
                                            var verifkeySym = (SymmetricVerificationKey)verifkey;
                                            btn2 = new DataGridViewButtonCell();
                                            dataGridViewAutPolOption.Rows[i].Cells[1] = btn2;
                                            dataGridViewAutPolOption.Rows[i].Cells[1].Value = AMSExplorer.Properties.Resources.AssetInformation_DoDisplayAuthorizationPolicyOption_SeeKeyValue;
                                            dataGridViewAutPolOption.Rows[i].Cells[1].Tag = Convert.ToBase64String(verifkeySym.KeyValue);
                                        }
                                    }
                                }

                                if (tokenTemplate.OpenIdConnectDiscoveryDocument != null)
                                {
                                    dataGridViewAutPolOption.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_DoDisplayAuthorizationPolicyOption_OpenIdConnectDiscoveryDocumentUri, tokenTemplate.OpenIdConnectDiscoveryDocument.OpenIdDiscoveryUri);
                                }
                                dataGridViewAutPolOption.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_DoDisplayAuthorizationPolicyOption_TokenAudience, tokenTemplate.Audience);
                                dataGridViewAutPolOption.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_DoDisplayAuthorizationPolicyOption_TokenIssuer, tokenTemplate.Issuer);
                                foreach (var claim in tokenTemplate.RequiredClaims)
                                {
                                    dataGridViewAutPolOption.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_DoDisplayAuthorizationPolicyOption_RequiredClaimType, claim.ClaimType);
                                    dataGridViewAutPolOption.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_DoDisplayAuthorizationPolicyOption_RequiredClaimValue, claim.ClaimValue);
                                }
                            }
                        }
                    }
                }
                buttonRemoveAuthPolOption.Enabled = true;
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
                IContentKey key = myAsset.ContentKeys.Skip(listViewKeys.SelectedIndices[0]).Take(1).FirstOrDefault();
                if (key != null)
                {
                    IContentKeyAuthorizationPolicy AutPol = myContext.ContentKeyAuthorizationPolicies.Where(a => a.Id == key.AuthorizationPolicyId).FirstOrDefault();
                    if (AutPol != null)
                    {
                        IContentKeyAuthorizationPolicyOption AutPolOption = AutPol.Options.Skip(listViewAutPolOptions.SelectedIndices[0]).FirstOrDefault();
                        if (AutPolOption != null)
                        {
                            DynamicEncryption.TokenResult testToken = DynamicEncryption.GetTestToken(myAsset, myContext, key.ContentKeyType, displayUI: true, optionid: AutPolOption.Id);
                            if (!string.IsNullOrEmpty(testToken.TokenString))
                            {
                                myMainForm.TextBoxLogWriteLine(AMSExplorer.Properties.Resources.AssetInformation_DoGetTestToken_TheAuthorizationTestTokenWithoutBearerIsN0, testToken);
                                myMainForm.TextBoxLogWriteLine(AMSExplorer.Properties.Resources.AssetInformation_DoGetTestToken_TheAuthorizationTestTokenWithBearerIsN0, Constants.Bearer + testToken);
                                System.Windows.Forms.Clipboard.SetText(Constants.Bearer + testToken.TokenString);
                                MessageBox.Show(string.Format(AMSExplorer.Properties.Resources.AssetInformation_DoGetTestToken_TheTestTokenBelowHasBeenBeCopiedToTheLogWindowAndClipboardNN0, Constants.Bearer + testToken.TokenString), "Test token copied");
                            }
                        }
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

        private void buttonFileMetadata_Click(object sender, EventArgs e)
        {
            ShowFileMetadata();
        }


        private List<IAssetFile> ReturnSelectedAssetFiles()
        {
            List<IAssetFile> Selection = new List<IAssetFile>();

            foreach (int selectedindex in listViewFiles.SelectedIndices)
            {
                IAssetFile AF = myAsset.AssetFiles.Where(af => af.Name == listViewFiles.Items[selectedindex].Text).FirstOrDefault();

                if (AF != null)
                {
                    Selection.Add(AF);
                }
            }
            return Selection;
        }

        private void ShowFileMetadata()
        {
            var SelectedAssetFile = ReturnSelectedAssetFiles().FirstOrDefault();

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
        }

        private ILocator GetTemporaryLocator()
        {
            if (tempLocator == null) // no temp locator, let's create it
            {
                try
                {
                    var locatorTask = Task.Factory.StartNew(() =>
                    {
                        tempLocator = myContext.Locators.Create(LocatorType.Sas, myAsset, AccessPermissions.Read, TimeSpan.FromHours(1));

                    });
                    locatorTask.Wait();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(AMSExplorer.Properties.Resources.AssetInformation_GetTemporaryLocator_ErrorWhenCreatingTheTemporarySASLocatorN + ex.Message);
                }
            }
            return tempLocator;
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
                IContentKey key = myAsset.ContentKeys.Skip(listViewKeys.SelectedIndices[0]).Take(1).FirstOrDefault();
                string keyid = key.Id;
                string question = string.Format(AMSExplorer.Properties.Resources.AssetInformation_DoDemoveKey_ThisWillRemoveTheKey0FromTheAssetNDoYouWantToAlsoDELETETheKeyFromTheAzureMediaServicesAccount, key.Name);
                DialogResult DR = MessageBox.Show(question, AMSExplorer.Properties.Resources.AssetInformation_DoDemoveKey_KeyRemoval, MessageBoxButtons.YesNoCancel);

                if (DR == DialogResult.Yes || DR == DialogResult.No)
                {
                    string step = AMSExplorer.Properties.Resources.AssetInformation_DoRemoveDeliveryPol_Removing;
                    try
                    {
                        myAsset.ContentKeys.Remove(key);
                        if (DR == DialogResult.Yes) // user wants also to delete the key
                        {
                            step = AMSExplorer.Properties.Resources.AssetInformation_DoRemoveDeliveryPol_Deleting;
                            IContentKey keyrefreshed = myContext.ContentKeys.Where(k => k.Id == keyid).FirstOrDefault();
                            if (keyrefreshed != null)
                            {
                                keyrefreshed.Delete();
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        string messagestr = string.Format(AMSExplorer.Properties.Resources.AssetInformation_DoDemoveKey_ErrorWhen0TheKey, step);
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
            DoRemoveDeliveryPol();
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
            getTestTokenToolStripMenuItem.Enabled = (listViewAutPolOptions.SelectedItems.Count > 0);
            removeOptionToolStripMenuItem.Enabled = (listViewAutPolOptions.SelectedItems.Count > 0);
            removeAuthorizationPolicyToolStripMenuItem.Enabled = (listViewAutPolOptions.Items.Count > 0);

        }

        private void contextMenuStripFiles_Opening(object sender, CancelEventArgs e)
        {
            bool selected = listViewFiles.SelectedItems.Count > 0;
            bool bMultiSelect = listViewFiles.SelectedItems.Count > 1;
            bool NonEncrypted = (myAsset.Options == AssetCreationOptions.None || myAsset.Options == AssetCreationOptions.CommonEncryptionProtected);

            makeItPrimaryToolStripMenuItem.Enabled = selected && !bMultiSelect;
            showMetadataToolStripMenuItem.Enabled = selected && !bMultiSelect;
            toolStripMenuItemOpenFile.Enabled = selected & NonEncrypted;
            toolStripMenuItemDownloadFile.Enabled = selected;
            deleteFileToolStripMenuItem.Enabled = selected;
            duplicateFileToolStripMenuItem.Enabled = selected & NonEncrypted && !bMultiSelect;

            deleteAllFilesToolStripMenuItem.Enabled = selected;
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            DoRemoveAuthPol();
        }

        private void DoRemoveAuthPol()
        {
            if (listViewKeys.SelectedItems.Count > 0)
            {
                if (listViewKeys.SelectedItems[0] != null)
                {
                    IContentKey key = myAsset.ContentKeys.Skip(listViewKeys.SelectedIndices[0]).Take(1).FirstOrDefault();
                    IContentKeyAuthorizationPolicy AuthPol = myContext.ContentKeyAuthorizationPolicies.Where(p => p.Id == key.AuthorizationPolicyId).FirstOrDefault();

                    if (AuthPol != null)
                    {
                        string AuthPolId = AuthPol.Id;
                        string question = string.Format(AMSExplorer.Properties.Resources.AssetInformation_DoRemoveAuthPol_ThisWillRemoveTheAuthorizationPolicy0FromTheKeyNDoYouWantToAlsoDELETEThePolicyFromTheAzureMediaServicesAccount, AuthPol.Name);
                        DialogResult DR = MessageBox.Show(question, AMSExplorer.Properties.Resources.AssetInformation_DoRemoveAuthPol_AuthorizationPolicyRemoval, MessageBoxButtons.YesNoCancel);

                        if (DR == DialogResult.Yes || DR == DialogResult.No)
                        {
                            string step = AMSExplorer.Properties.Resources.AssetInformation_DoRemoveDeliveryPol_Removing;
                            try
                            {
                                key.AuthorizationPolicyId = null;
                                key.Update();

                                if (DR == DialogResult.Yes) // user wants also to delete the auth policy
                                {
                                    step = AMSExplorer.Properties.Resources.AssetInformation_DoRemoveDeliveryPol_Deleting;
                                    AuthPol.Delete();
                                }
                            }
                            catch (Exception e)
                            {
                                string messagestr = string.Format(AMSExplorer.Properties.Resources.AssetInformation_DoRemoveAuthPol_ErrorWhen0TheAuthorizationPolicy, step);
                                if (e.InnerException != null)
                                {
                                    messagestr += Constants.endline + Program.GetErrorMessage(e);
                                }
                                MessageBox.Show(messagestr);
                            }

                            DoDisplayKeyPropertiesAndAutOptions();
                        }
                    }
                }
            }
        }

        private void DoRemoveAuthPolOption()
        {
            if (listViewAutPolOptions.SelectedItems.Count > 0)
            {
                if (listViewAutPolOptions.SelectedItems[0] != null)
                {

                    IContentKey key = myAsset.ContentKeys.Skip(listViewKeys.SelectedIndices[0]).Take(1).FirstOrDefault();
                    IContentKeyAuthorizationPolicy AuthPol = myContext.ContentKeyAuthorizationPolicies.Where(p => p.Id == key.AuthorizationPolicyId).FirstOrDefault();
                    var option = myAuthPolicy.Options.Skip(listViewAutPolOptions.SelectedIndices[0]).Take(1).FirstOrDefault();

                    if (option != null)
                    {
                        string AuthPolId = AuthPol.Id;
                        string question = string.Format(AMSExplorer.Properties.Resources.AssetInformation_DoRemoveAuthPolOption_ThisWillRemoveTheOption0FromTheAuthorizationPolicyNDoYouWantToAlsoDELETETheOptionFromTheAzureMediaServicesAccount, AuthPol.Name);
                        DialogResult DR = MessageBox.Show(question, AMSExplorer.Properties.Resources.AssetInformation_DoRemoveAuthPolOption_OptionRemoval, MessageBoxButtons.YesNoCancel);

                        if (DR == DialogResult.Yes || DR == DialogResult.No)
                        {
                            string step = AMSExplorer.Properties.Resources.AssetInformation_DoRemoveDeliveryPol_Removing;
                            try
                            {
                                AuthPol.Options.Remove(option);

                                if (DR == DialogResult.Yes) // user wants also to delete the option
                                {
                                    step = AMSExplorer.Properties.Resources.AssetInformation_DoRemoveDeliveryPol_Deleting;
                                    option.Delete();
                                }
                            }
                            catch (Exception e)
                            {
                                string messagestr = string.Format(AMSExplorer.Properties.Resources.AssetInformation_DoRemoveAuthPolOption_ErrorWhen0TheAuthorizationPolicyOption, step);
                                if (e.InnerException != null)
                                {
                                    messagestr += Constants.endline + Program.GetErrorMessage(e);
                                }
                                MessageBox.Show(messagestr);
                            }

                            DoDisplayKeyPropertiesAndAutOptions();
                        }
                    }
                }
            }
        }

        private void filterInfoupdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoFilterInfo();
        }
        private List<IStreamingAssetFilter> ReturnSelectedFilters()
        {

            List<IStreamingAssetFilter> SelectedFilters = new List<IStreamingAssetFilter>();
            foreach (DataGridViewRow Row in dataGridViewFilters.SelectedRows)
            {
                string filterid = Row.Cells[dataGridViewFilters.Columns["Id"].Index].Value.ToString();
                IStreamingAssetFilter myfilter = myAsset.AssetFilters.Where(f => f.Id == filterid).FirstOrDefault();
                if (myfilter != null)
                {
                    SelectedFilters.Add(myfilter);
                }
            }
            return SelectedFilters;
        }
        private void DoFilterInfo()
        {
            var filters = ReturnSelectedFilters();
            if (filters.Count == 1)
            {
                var filter = filters.FirstOrDefault();
                DynManifestFilter form = new DynManifestFilter(myContext, (IStreamingFilter)filter, myAsset);

                if (form.ShowDialog() == DialogResult.OK)
                {
                    FilterCreationInfo filtertoupdate = null;
                    try
                    {
                        filtertoupdate = form.GetFilterInfo;
                        filter.PresentationTimeRange = filtertoupdate.Presentationtimerange;
                        filter.Tracks = filtertoupdate.Trackconditions;
                        filter.FirstQuality = filtertoupdate.Firstquality;
                        filter.Update();
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
            DynManifestFilter form = new DynManifestFilter(myContext, null, myAsset);

            if (form.ShowDialog() == DialogResult.OK)
            {

                FilterCreationInfo filterinfo = null;
                try
                {
                    filterinfo = form.GetFilterInfo;
                    myAsset.AssetFilters.Create(filterinfo.Name, filterinfo.Presentationtimerange, filterinfo.Trackconditions);
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
            try
            {
                filters.ForEach(f => f.Delete());
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
                IStreamingAssetFilter sourcefilter = filters.FirstOrDefault();

                string newfiltername = sourcefilter.Name + AMSExplorer.Properties.Resources.AssetInformation_DoDuplicateFilter_Copy;
                if (Program.InputBox(AMSExplorer.Properties.Resources.AssetInformation_DoDuplicate_NewName, AMSExplorer.Properties.Resources.AssetInformation_DoDuplicateFilter_EnterTheNameOfTheNewDuplicateFilter, ref newfiltername) == DialogResult.OK)
                {
                    try
                    {
                        myAsset.AssetFilters.Create(newfiltername, sourcefilter.PresentationTimeRange, sourcefilter.Tracks);
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
                string question = "Delete all files ?";
                if (System.Windows.Forms.MessageBox.Show(question, AMSExplorer.Properties.Resources.AssetInformation_DoDeleteFiles_FileDeletion, System.Windows.Forms.MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    var assetArray = myAsset.AssetFiles.ToArray();
                    for (int i = 0; i < assetArray.Length; i++)
                    {
                        IAssetFile AF = assetArray[i];
                        AF.Delete();
                    }
                    ListAssetFiles();
                    BuildLocatorsTree();
                }
            }
            catch
            {
                MessageBox.Show(AMSExplorer.Properties.Resources.AssetInformation_DoDeleteAllFiles_ErrorWhenDeletingTheFiles);
                ListAssetFiles();
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
            myMainForm.DoPlaySelectedAssetsOrProgramsWithPlayer(PlayerType.AzureMediaPlayer, new List<IAsset>() { myAsset }, ReturnSelectedFilters().FirstOrDefault().Name);
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
            var SelectedAssetFiles = ReturnSelectedAssetFiles();

            if (SelectedAssetFiles.Count == 1 && SelectedAssetFiles.FirstOrDefault() != null)
            {
                IAssetFile assetFileToEdit = SelectedAssetFiles.FirstOrDefault();

                if (assetFileToEdit.ContentFileSize > 500 * 1024)
                {
                    MessageBox.Show(AMSExplorer.Properties.Resources.AssetInformation_DoEditFile_FileIsToLargeToEditItOnline);
                    return;
                }

                try
                {
                    progressBarUpload.Maximum = 100;
                    progressBarUpload.Visible = true;
                    string tempPath = System.IO.Path.GetTempPath();
                    string filePath = Path.Combine(tempPath, assetFileToEdit.Name);

                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                    await Task.Factory.StartNew(() => ProcessDownloadFileToAsset(assetFileToEdit, filePath));

                    progressBarUpload.Visible = false;

                    StreamReader streamReader = new StreamReader(filePath);
                    Encoding fileEncoding = streamReader.CurrentEncoding;
                    string datastring = streamReader.ReadToEnd();
                    streamReader.Close();

                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }

                    var editform = new EditorXMLJSON(string.Format(AMSExplorer.Properties.Resources.AssetInformation_DoEditFile_OnlineEditOf0, assetFileToEdit.Name), datastring, true, false);
                    if (editform.Display() == DialogResult.OK)
                    { // OK

                        StreamWriter outfile = new StreamWriter(filePath, false, fileEncoding);

                        outfile.Write(editform.TextData);
                        outfile.Close();

                        string assetFileName = assetFileToEdit.Name;
                        bool assetFilePrimary = assetFileToEdit.IsPrimary;
                        assetFileToEdit.Delete();

                        progressBarUpload.Visible = true;
                        buttonClose.Enabled = false;

                        await Task.Factory.StartNew(() => ProcessUploadFileToAsset(Path.GetFileName(filePath), filePath, myAsset));

                        if (File.Exists(filePath))
                        {
                            File.Delete(filePath);
                        }

                        if (assetFilePrimary)
                        {
                            AssetInfo.SetFileAsPrimary(myAsset, assetFileName);
                        }
                        // Refresh the asset.
                        myAsset = Mainform._context.Assets.Where(a => a.Id == myAsset.Id).FirstOrDefault();
                        progressBarUpload.Visible = false;
                        buttonClose.Enabled = true;
                        ListAssetFiles();
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
                var smildata = Program.LoadAndUpdateManifestTemplate(myAsset);

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

                    await Task.Factory.StartNew(() => ProcessUploadFileToAsset(Path.GetFileName(filePath), filePath, myAsset));

                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }

                    // Refresh the asset.
                    myAsset = Mainform._context.Assets.Where(a => a.Id == myAsset.Id).FirstOrDefault();
                    AssetInfo.SetFileAsPrimary(myAsset, smildata.FileName);
                }
            }
            catch
            {

            }
            progressBarUpload.Visible = false;
            buttonClose.Enabled = true;
            ListAssetFiles();
            BuildLocatorsTree();
        }

        private void button1_Click_5(object sender, EventArgs e)
        {
            DoRemoveAuthPolOption();
        }

        private void removeAuthorizationPolicyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoRemoveAuthPol();
        }

        private void removeOptionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoRemoveAuthPolOption();
        }

        private void removeAuthorizationPolicyToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            DoRemoveAuthPol();

        }

        private void buttonAddExistingDelPol_Click(object sender, EventArgs e)
        {
            DoAddExistingDelPol();
        }


        private void DoAddExistingDelPol()
        {

            var form = new SelectDeliveryPolicy(myContext);
            if (form.ShowDialog() == DialogResult.OK)
            {
                IAssetDeliveryPolicy DP = form.SelectedPolicy;
                if (DP != null)
                {

                    try
                    {
                        myAsset.DeliveryPolicies.Add(DP);
                    }

                    catch (Exception e)
                    {
                        string messagestr = string.Format(AMSExplorer.Properties.Resources.AssetInformation_DoAddExistingDelPol_ErrorWhenAttachingTheDeliveryPolicy);
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

        private void buttonAddExistingAutPol_Click(object sender, EventArgs e)
        {
            DoAddExistingAutPol();
        }

        private void DoAddExistingAutPol()
        {

            if (listViewKeys.SelectedItems.Count > 0)
            {
                if (listViewKeys.SelectedItems[0] != null)
                {
                    IContentKey key = myAsset.ContentKeys.Skip(listViewKeys.SelectedIndices[0]).Take(1).FirstOrDefault();

                    var form = new SelectAutPolicy(myContext, key.ContentKeyType);
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        var AutPol = form.SelectedPolicy;
                        if (AutPol != null)
                        {


                            try
                            {
                                key.AuthorizationPolicyId = AutPol.Id;
                                key.Update();
                            }

                            catch (Exception e)
                            {
                                string messagestr = string.Format(AMSExplorer.Properties.Resources.AssetInformation_DoAddExistingAutPol_ErrorWhenAttachingAnExistingAuthorizationPolicy);
                                if (e.InnerException != null)
                                {
                                    messagestr += Constants.endline + Program.GetErrorMessage(e);
                                }
                                MessageBox.Show(messagestr);
                            }

                            DoDisplayKeyPropertiesAndAutOptions();
                        }
                    }

                }
            }
        }
    }

}

