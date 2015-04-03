//----------------------------------------------------------------------- 
// <copyright file="ChannelAdSlateControl.cs" company="Microsoft">Copyright (c) Microsoft Corporation. All rights reserved.</copyright> 
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
using System.Net;


using System.Collections.ObjectModel;
using System.Globalization;
using System.Text.RegularExpressions;


namespace AMSExplorer
{
    public partial class ChannelAdSlateControl : Form
    {
        public IChannel MyChannel;
        public CloudMediaContext MyContext;
        private Mainform MyMainForm;


        public ChannelAdSlateControl(Mainform mainform)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            MyMainForm = mainform;
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


        private void ChannelAdSlateControl_Load(object sender, EventArgs e)
        {
            labelChannelName.Text += MyChannel.Name;
            listViewJPG1.LoadJPGs(MyContext);
        }



        private void contextMenuStripDG_Opening(object sender, CancelEventArgs e)
        {

        }


        private void ChannelAdSlateControl_FormClosed(object sender, FormClosedEventArgs e)
        {

        }



        private void toolStripMenuItemFilesCopyClipboard_Click(object sender, EventArgs e)
        {

        }




        private void ChannelInformation_Shown(object sender, EventArgs e)
        {

        }



        private async void buttonUploadSlate_Click(object sender, EventArgs e)
        {
            if (openFileDialogSlate.ShowDialog() == DialogResult.OK)
            {
                IAsset asset;
                progressBarUpload.Value = 0;
                progressBarUpload.Visible = true;

                buttonUploadSlate.Enabled = false;
                string file = openFileDialogSlate.FileName;
                asset = await Task.Factory.StartNew(() => ProcessUploadFile(Path.GetFileName(file), file));
                progressBarUpload.Visible = false;

                buttonUploadSlate.Enabled = true;
                listViewJPG1.LoadJPGs(MyContext, asset);
            }
        }

        private IAsset ProcessUploadFile(string SafeFileName, string FileName, string storageaccount = null)
        {
            if (storageaccount == null) storageaccount = MyContext.DefaultStorageAccount.Name; // no storage account or null, then let's take the default one

            IAsset asset = null;
            IAccessPolicy policy = null;
            ILocator locator = null;

            try
            {
                asset = MyContext.Assets.Create(SafeFileName as string, storageaccount, AssetCreationOptions.None);
                IAssetFile file = asset.AssetFiles.Create(SafeFileName);
                policy = MyContext.AccessPolicies.Create(
                                       SafeFileName,
                                       TimeSpan.FromDays(30),
                                       AccessPermissions.Write | AccessPermissions.List);

                locator = MyContext.Locators.CreateLocator(LocatorType.Sas, asset, policy);
                file.UploadProgressChanged += file_UploadProgressChanged;
                file.Upload(FileName);
                AssetInfo.SetFileAsPrimary(asset, SafeFileName);

            }
            catch (Exception e)
            {
                asset = null;
            }
            finally
            {
                if (locator != null) locator.Delete();
                if (policy != null) policy.Delete();
            }
            return asset;
        }
        private void file_UploadProgressChanged(object sender, Microsoft.WindowsAzure.MediaServices.Client.UploadProgressChangedEventArgs e)
        {
            progressBarUpload.BeginInvoke(new Action(() => progressBarUpload.Value = (int)e.Progress), null);
        }

        private void buttonInsertAD_Click(object sender, EventArgs e)
        {

            InsertAd(false);
        }

        private void buttonInsertAdAndSlate_Click(object sender, EventArgs e)
        {
            InsertAd(true);
        }
        private async void InsertAd(bool showslate)
        {
            bool Error = false;

            try
            {
                TimeSpan.FromSeconds(Convert.ToDouble(textBoxADSignalDuration.Text));

            }
            catch (Exception e)
            {
                Error = true;
                MyMainForm.TextBoxLogWriteLine("Channel '{0}' : Error with AD duration input", MyChannel.Name, true);
                MyMainForm.TextBoxLogWriteLine(e);
            }

            if (!Error)
            {
                TimeSpan ts = TimeSpan.FromSeconds(Convert.ToDouble(textBoxADSignalDuration.Text)); ;
                int cueid = 0;
                if (!string.IsNullOrEmpty(textBoxCueId.Text))
                {
                    try
                    {
                        cueid = Convert.ToInt32(textBoxCueId.Text);

                    }
                    catch (Exception e)
                    {
                        Error = true;
                        MyMainForm.TextBoxLogWriteLine("Channel '{0}' : Error with CueID input", MyChannel.Name, true);
                        MyMainForm.TextBoxLogWriteLine(e);
                    }
                }
                if (!Error)
                {
                    MyMainForm.TextBoxLogWriteLine("Channel '{0}' : sending AD signal", MyChannel.Name);
    
                    try
                    {
                        await Task.Run(() => ChannelInfo.ChannelExecuteOperationAsync(MyChannel.SendStartAdvertisementOperationAsync, ts, cueid, showslate, MyChannel, "advertising " + cueid.ToString() + " sent", MyContext, MyMainForm));
                    }
                    catch (Exception e)
                    {
                        Error = true;
                        MyMainForm.TextBoxLogWriteLine("Channel '{0}' : Error when sending signal", MyChannel.Name, true);
                        MyMainForm.TextBoxLogWriteLine(e);
                    }
                }
            }
        }

        private async void ShowSlate()
        {
            bool Error = false;

            try
            {
                TimeSpan.FromSeconds(Convert.ToDouble(textBoxSlateDuration.Text));

            }
            catch (Exception e)
            {
                Error = true;
                MyMainForm.TextBoxLogWriteLine("Channel '{0}' : Error with slate duration input", MyChannel.Name, true);
                MyMainForm.TextBoxLogWriteLine(e);
            }

            if (!Error)
            {
                TimeSpan ts = TimeSpan.FromSeconds(Convert.ToDouble(textBoxSlateDuration.Text));
                MyMainForm.TextBoxLogWriteLine("Channel '{0}' : sending show slate signal", MyChannel.Name);
    
                try
                {
                    string jpg_id = listViewJPG1.GetSelectedJPG.FirstOrDefault().Id;
                    await Task.Run(() => ChannelInfo.ChannelExecuteOperationAsync(MyChannel.SendShowSlateOperationAsync, ts, jpg_id, MyChannel, "slate shown", MyContext, MyMainForm));
                }
                catch (Exception e)
                {
                    Error = true;
                    MyMainForm.TextBoxLogWriteLine("Channel '{0}' : Error when showing slate", MyChannel.Name, true);
                    MyMainForm.TextBoxLogWriteLine(e);

                }
            }
        }

        private async void HideSlate()
        {
            MyMainForm.TextBoxLogWriteLine("Channel '{0}' : sending hide slate signal", MyChannel.Name);

            try
            {
                await Task.Run(() => ChannelInfo.ChannelExecuteOperationAsync(MyChannel.SendHideSlateOperationAsync, MyChannel, "slate hidden", MyContext, MyMainForm));
            }
            catch (Exception e)
            {
                MyMainForm.TextBoxLogWriteLine("Channel '{0}' : Error when hidding slate", MyChannel.Name, true);
                MyMainForm.TextBoxLogWriteLine(e);
            }
        }


        private void buttonShowSLate_Click(object sender, EventArgs e)
        {
            ShowSlate();
        }

        private void buttonHideSlate_Click(object sender, EventArgs e)
        {
            HideSlate();
        }


        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBoxJPGSearch_TextChanged(object sender, EventArgs e)
        {
            listViewJPG1.LoadJPGs(textBoxJPGSearch.Text);
        }

        private void progressBarUpload_Click(object sender, EventArgs e)
        {

        }

        private void checkBoxPreview_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxPreviewStream.Checked)
            {
                if (MyChannel.State == ChannelState.Running && MyChannel.Preview.Endpoints.FirstOrDefault().Url.AbsoluteUri != null)
                {
                    string myurl = AssetInfo.DoPlayBackWithBestStreamingEndpoint(typeplayer: PlayerType.AzureMediaPlayerFrame, Urlstr: MyChannel.Preview.Endpoints.FirstOrDefault().Url.ToString(), DoNotRewriteURL: true, context: MyContext, formatamp: AzureMediaPlayerFormats.Smooth, technology: AzureMediaPlayerTechnologies.Silverlight, launchbrowser: false);
                    webBrowserPreview2.Url = new Uri(myurl);
                }
            }
            else
            {
                webBrowserPreview2.Url = null;
            }
        }

        private void listViewJPG1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkBoxPreviewSlate.Checked)
            {
                IAsset JPGAsset = listViewJPG1.GetSelectedJPG.FirstOrDefault();
                if (JPGAsset != null)
                {
                    IAssetFile AF = null;
                    ILocator locator = CreateSASLocator(JPGAsset);
                    try
                    {
                        if (locator != null)
                        {
                            AF = JPGAsset.AssetFiles.FirstOrDefault();
                            pictureBoxPreviewSlate.Load(AF.GetSasUri(locator).ToString());
                            DeleteSASLocator(locator);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Error when accessing temporary SAS locator");
                    }
                }
            }
        }

        private ILocator CreateSASLocator(IAsset MyAsset)
        {
            ILocator newlocator = null;

            try
            {
                var locatorTask = Task.Factory.StartNew(() =>
                {
                    newlocator = MyContext.Locators.Create(LocatorType.Sas, MyAsset, AccessPermissions.Read, TimeSpan.FromHours(1));
                });
                locatorTask.Wait();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error when creating the temporary SAS locator." + ex.Message);
            }

            return newlocator;
        }

        private void DeleteSASLocator(ILocator locator)
        {
            if (locator != null)
            {
                try
                {
                    var locatorTask = Task.Factory.StartNew(() =>
                    {
                        locator.Delete();
                    });
                    locatorTask.Wait();
                }
                catch
                {

                }
            }
        }


    }
}
