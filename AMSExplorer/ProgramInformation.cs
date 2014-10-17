//----------------------------------------------------------------------- 
// <copyright file="ProgramInformation.cs" company="Microsoft">Copyright (c) Microsoft Corporation. All rights reserved.</copyright> 
// <license>
// Azure Media Services Explorer Ver. 3.0
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

namespace AMSExplorer
{
    public partial class ProgramInformation : Form
    {
        public IProgram MyProgram;

        public CloudMediaContext MyContext;

        private IEnumerable<Uri> ValidURIs;
        private IEnumerable<Uri> NotValidURIs;

        public string ProgramDescription
        {
            get { return textBoxDescription.Text; }
        }


        public TimeSpan archiveWindowLength
        {
            get
            {
                return new TimeSpan((int)numericUpDownArchiveDays.Value, (int)numericUpDownArchiveHours.Value, (int)numericUpDownArchiveMinutes.Value, 0); ;
            }

        }


        public ProgramInformation()
        {
            InitializeComponent();
        }

        private void contextMenuStripDG_MouseClick(object sender, MouseEventArgs e)
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

        private void toolStripMenuItemCopy_Click(object sender, EventArgs e)
        {
            if (TreeViewLocators.SelectedNode != null)
            {
                // Root node's Parent property is null, so do check
                if (TreeViewLocators.SelectedNode.Parent != null)
                {
                    System.Windows.Forms.Clipboard.SetText(TreeViewLocators.SelectedNode.Text);
                }
                else
                {

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
                        case "Smooth Streaming URI":
                            AssetInfo.DoPlayBack(PlayerType.FlashAzurePage, new Uri(TreeViewLocators.SelectedNode.Text));
                            break;
                        case "MPEG-DASH URI":
                            AssetInfo.DoPlayBack(PlayerType.DASHAzurePage, new Uri(TreeViewLocators.SelectedNode.Text));
                            break;


                        default:
                            break;
                    }
                else
                {

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
                        case "Smooth Streaming URI":
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
                        case "MPEG-DASH URI":
                            AssetInfo.DoPlayBack(PlayerType.DASHIFRefPlayer, new Uri(TreeViewLocators.SelectedNode.Text));
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

        private void contextMenuStripLocators_Opening(object sender, CancelEventArgs e)
        {
            if (TreeViewLocators.SelectedNode != null)
            {
                if (TreeViewLocators.SelectedNode.Parent != null)
                {
                    toolStripMenuItemDASHAZURE.Enabled = false;
                    toolStripMenuItemDASHIF.Enabled = false;
                    toolStripMenuItemPlaybackFlashAzure.Enabled = false;
                    toolStripMenuItemPlaybackSilverlightMonitoring.Enabled = false;
                    toolStripMenuItemPlaybackMP4.Enabled = false;
                    toolStripMenuItemOpen.Enabled = false;

                    if (TreeViewLocators.SelectedNode.Parent.Text.Equals(AssetInfo._smooth) | TreeViewLocators.SelectedNode.Parent.Text.Contains(AssetInfo._smooth_legacy))
                    {
                        toolStripMenuItemDASHAZURE.Enabled = false;
                        toolStripMenuItemDASHIF.Enabled = false;
                        toolStripMenuItemPlaybackFlashAzure.Enabled = true;
                        toolStripMenuItemPlaybackSilverlightMonitoring.Enabled = true;
                        toolStripMenuItemPlaybackMP4.Enabled = false;
                        toolStripMenuItemOpen.Enabled = false;

                    }
                    if (TreeViewLocators.SelectedNode.Parent.Text.Equals(AssetInfo._dash))
                    {
                        toolStripMenuItemDASHAZURE.Enabled = true;
                        toolStripMenuItemDASHIF.Enabled = true;
                        toolStripMenuItemPlaybackFlashAzure.Enabled = true;
                        toolStripMenuItemPlaybackSilverlightMonitoring.Enabled = false;
                        toolStripMenuItemPlaybackMP4.Enabled = false;
                        toolStripMenuItemOpen.Enabled = false;

                    }
                    if (TreeViewLocators.SelectedNode.Parent.Text.Equals(AssetInfo._prog_down_https))
                    {
                        toolStripMenuItemDASHAZURE.Enabled = false;
                        toolStripMenuItemDASHIF.Enabled = false;
                        toolStripMenuItemPlaybackFlashAzure.Enabled = false;
                        toolStripMenuItemPlaybackSilverlightMonitoring.Enabled = false;
                        toolStripMenuItemPlaybackMP4.Enabled = false;
                        toolStripMenuItemOpen.Enabled = true;

                    }
                    if (TreeViewLocators.SelectedNode.Parent.Text.Equals(AssetInfo._prog_down_http))
                    {
                        toolStripMenuItemDASHAZURE.Enabled = false;
                        toolStripMenuItemDASHIF.Enabled = false;
                        toolStripMenuItemPlaybackFlashAzure.Enabled = false;
                        toolStripMenuItemPlaybackSilverlightMonitoring.Enabled = false;
                        toolStripMenuItemPlaybackMP4.Enabled = (TreeViewLocators.SelectedNode.Text.ToLower().Contains(".mp4"));
                        toolStripMenuItemOpen.Enabled = !(TreeViewLocators.SelectedNode.Text.ToLower().Contains(".ism"));

                    }
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




        private void BuildLocatorsTree()
        {

            // LOCATORS TREE
            IAsset MyAsset = MyContext.Assets.Where(a => a.Id == MyProgram.AssetId).Single();
            var ismFile = MyAsset.AssetFiles.AsEnumerable().FirstOrDefault(f => f.Name.EndsWith(".ism"));
            if (ismFile != null)
            {
                var locators = MyAsset.Locators.Where(l => l.Type == LocatorType.OnDemandOrigin);

                var template = new UriTemplate("{contentAccessComponent}/{ismFileName}/manifest");

                TreeViewLocators.BeginUpdate();
                TreeViewLocators.Nodes.Clear();
                foreach (ILocator locator in locators)
                {

                    Color colornode;
                    string locatorstatus = string.Empty;

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


                    TreeNode myLocNode = new TreeNode(string.Format("{0} ({1}) {2}", locator.Type.ToString(), locatorstatus, locator.Name));
                    myLocNode.ForeColor = colornode;

                    TreeViewLocators.Nodes.Add(myLocNode);

                    TreeViewLocators.Nodes[MyAsset.Locators.IndexOf(locator)].Nodes.Add(new TreeNode("Locator information"));


                    TreeViewLocators.Nodes[MyAsset.Locators.IndexOf(locator)].Nodes[0].Nodes.Add(new TreeNode(
                        string.Format("Name: {0}", locator.Name)
                        ));

                    TreeViewLocators.Nodes[MyAsset.Locators.IndexOf(locator)].Nodes[0].Nodes.Add(new TreeNode(
                        string.Format("Type: {0}", locator.Type.ToString())
                        ));

                    TreeViewLocators.Nodes[MyAsset.Locators.IndexOf(locator)].Nodes[0].Nodes.Add(new TreeNode(
                       string.Format("Path: {0}", (locator.Path))
                       ));

                    if (locator.StartTime != null)
                        TreeViewLocators.Nodes[MyAsset.Locators.IndexOf(locator)].Nodes[0].Nodes.Add(new TreeNode(
                           string.Format("Start time: {0}", (((DateTime)locator.StartTime).ToLocalTime().ToString()))
                           ));

                    if (locator.ExpirationDateTime != null)
                        TreeViewLocators.Nodes[MyAsset.Locators.IndexOf(locator)].Nodes[0].Nodes.Add(new TreeNode(
                         string.Format("Expiration date time: {0}", (((DateTime)locator.ExpirationDateTime).ToLocalTime().ToString()))
                         ));

                    TreeViewLocators.Nodes[MyAsset.Locators.IndexOf(locator)].Nodes[0].Nodes.Add(new TreeNode(
                   string.Format("ID: {0}", (locator.Id))
                   ));

                    if (locator.Type == LocatorType.OnDemandOrigin)
                    {
                        int indexn = 1;

                        if (locator.GetSmoothStreamingUri() != null)
                        {
                            TreeViewLocators.Nodes[MyAsset.Locators.IndexOf(locator)].Nodes.Add(new TreeNode(AssetInfo._smooth));
                            TreeViewLocators.Nodes[MyAsset.Locators.IndexOf(locator)].Nodes[indexn].Nodes.Add(new TreeNode(locator.GetSmoothStreamingUri().ToString()));
                            // legacy smooth streaming without repeat tag (manifest v2.0)
                            TreeViewLocators.Nodes[MyAsset.Locators.IndexOf(locator)].Nodes.Add(new TreeNode(AssetInfo._smooth_legacy));
                            TreeViewLocators.Nodes[MyAsset.Locators.IndexOf(locator)].Nodes[indexn + 1].Nodes.Add(new TreeNode(AssetInfo.GetSmoothLegacy(locator.GetSmoothStreamingUri().ToString())));
                            indexn = indexn + 2;

                        }
                        if (locator.GetMpegDashUri() != null)
                        {

                            TreeViewLocators.Nodes[MyAsset.Locators.IndexOf(locator)].Nodes.Add(new TreeNode(AssetInfo._dash));
                            TreeViewLocators.Nodes[MyAsset.Locators.IndexOf(locator)].Nodes[indexn].Nodes.Add(new TreeNode(locator.GetMpegDashUri().ToString()));
                            indexn++;
                        }
                        if (locator.GetHlsUri() != null)
                        {
                            TreeViewLocators.Nodes[MyAsset.Locators.IndexOf(locator)].Nodes.Add(new TreeNode(AssetInfo._hls_v4));
                            TreeViewLocators.Nodes[MyAsset.Locators.IndexOf(locator)].Nodes[indexn].Nodes.Add(new TreeNode(locator.GetHlsUri().ToString()));
                            TreeViewLocators.Nodes[MyAsset.Locators.IndexOf(locator)].Nodes.Add(new TreeNode(AssetInfo._hls_v3));
                            TreeViewLocators.Nodes[MyAsset.Locators.IndexOf(locator)].Nodes[indexn + 1].Nodes.Add(new TreeNode(locator.GetHlsUri().ToString().Replace("format=m3u8-aapl", "format=m3u8-aapl-v3")));
                            indexn = indexn + 2;
                        }


                    }
                }
                TreeViewLocators.EndUpdate();

            }
        }



        private void contextMenuStripDG_Opening(object sender, CancelEventArgs e)
        {

        }


        private void buttonCopyStats_Click(object sender, EventArgs e)
        {


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
                    buttonFlash.Enabled = false;
                    buttonSLMonitor.Enabled = false;
                    buttonHTML.Enabled = false;
                    buttonOpen.Enabled = false;

                    switch (TreeViewLocators.SelectedNode.Parent.Text)
                    {
                        case AssetInfo._smooth:
                        case AssetInfo._smooth_legacy:

                            buttonDASH.Enabled = false;
                            buttonDashAzure.Enabled = false;
                            buttonFlash.Enabled = true;
                            buttonSLMonitor.Enabled = true;
                            buttonHTML.Enabled = false;
                            buttonOpen.Enabled = false;
                            break;

                        case AssetInfo._dash:
                            buttonDASH.Enabled = true;
                            buttonDashAzure.Enabled = true;
                            buttonFlash.Enabled = true;
                            buttonSLMonitor.Enabled = false;
                            buttonHTML.Enabled = false;
                            buttonOpen.Enabled = false;
                            break;

                        case AssetInfo._prog_down_https:
                            buttonDASH.Enabled = false;
                            buttonDashAzure.Enabled = false;
                            buttonFlash.Enabled = false;
                            buttonSLMonitor.Enabled = false;
                            buttonHTML.Enabled = false;
                            buttonOpen.Enabled = true;
                            break;

                        case AssetInfo._prog_down_http:
                            buttonDASH.Enabled = false;
                            buttonDashAzure.Enabled = false;
                            buttonFlash.Enabled = false;
                            buttonSLMonitor.Enabled = false;
                            buttonHTML.Enabled = (TreeViewLocators.SelectedNode.Text.ToLower().EndsWith(".mp4"));
                            buttonOpen.Enabled = !(TreeViewLocators.SelectedNode.Text.ToLower().EndsWith(".ism"));
                            break;

                        default:
                            break;
                    }
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
                else
                {

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


        private void ProgramInformation_Load_1(object sender, EventArgs e)
        {

            labelProgramName.Text += MyProgram.Name;

            DGChannel.ColumnCount = 2;

            // asset info

            DGChannel.Columns[0].DefaultCellStyle.BackColor = Color.Gainsboro;
            DGChannel.Rows.Add("Name", MyProgram.Name);
            DGChannel.Rows.Add("Id", MyProgram.Id);
            DGChannel.Rows.Add("State", (ChannelState)MyProgram.State);
            DGChannel.Rows.Add("Created", ((DateTime)MyProgram.Created).ToLocalTime());
            DGChannel.Rows.Add("Last Modified", ((DateTime)MyProgram.LastModified).ToLocalTime());
            DGChannel.Rows.Add("Description", MyProgram.Description);
            DGChannel.Rows.Add("Asset Id", MyProgram.AssetId);
            DGChannel.Rows.Add("Channel Name", MyProgram.Channel.Name);
            DGChannel.Rows.Add("Channel Id", MyProgram.ChannelId);
            DGChannel.Rows.Add("Archive Window Length", MyProgram.ArchiveWindowLength);
            DGChannel.Rows.Add("Manifest Name", MyProgram.ManifestName);


            ProgramInfo PI = new ProgramInfo(MyProgram, MyContext);
            ValidURIs = PI.GetValidURIs();
            NotValidURIs = PI.GetNotValidURIs();

            foreach (var t in ValidURIs)
            {
                DGChannel.Rows.Add("Url", t.AbsoluteUri);
            }
            foreach (var t in NotValidURIs)
            {
                int i = DGChannel.Rows.Add("Url", t.AbsoluteUri);
                DGChannel.Rows[i].Cells[1].Style.ForeColor = Color.Red;
            }

            textBoxDescription.Text = MyProgram.Description;

            numericUpDownArchiveDays.Value = MyProgram.ArchiveWindowLength.Days;
            numericUpDownArchiveHours.Value = MyProgram.ArchiveWindowLength.Hours;
            numericUpDownArchiveMinutes.Value = MyProgram.ArchiveWindowLength.Minutes;

            BuildLocatorsTree();
        }

        private void labelProgramName_Click(object sender, EventArgs e)
        {

        }


        private void ProgramInformation_Shown(object sender, EventArgs e)
        {

        }




    }
}
