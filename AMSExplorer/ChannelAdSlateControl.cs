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
        private Dictionary<IAsset, ILocator> ListLocators = new Dictionary<IAsset, ILocator>(); // to store locators for JPEG files
        string labelSlatePreviewInfoText;

        public ChannelAdSlateControl(Mainform mainform)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            MyMainForm = mainform;

            labelSlatePreviewInfoText = labelSlatePreviewInfo.Text;
            labelSlatePreviewInfo.Text = "";
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
            listViewJPG1.LoadJPGs(MyContext, null, MyChannel.Slate);
            textBoxCueId.Text = GenerateRandomCueId();
        }

        private string GenerateRandomCueId()
        {
            return new Random().Next(int.MaxValue).ToString();
        }


        private void contextMenuStripDG_Opening(object sender, CancelEventArgs e)
        {

        }


        private void ChannelAdSlateControl_FormClosed(object sender, FormClosedEventArgs e)
        {
            ListLocators.ToList().ForEach(entry => DeleteSASLocator(entry.Value));
        }



        private void toolStripMenuItemFilesCopyClipboard_Click(object sender, EventArgs e)
        {

        }




        private void ChannelInformation_Shown(object sender, EventArgs e)
        {

        }



        private async void buttonUploadSlate_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(Properties.Settings.Default.DefaultSlateCurrentFolder))
            {
                openFileDialogSlate.InitialDirectory = Properties.Settings.Default.DefaultSlateCurrentFolder;
            }

            if (openFileDialogSlate.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.DefaultSlateCurrentFolder = Path.GetDirectoryName(openFileDialogSlate.FileName); // let's save the folder
                Program.SaveAndProtectUserConfig();

                string file = openFileDialogSlate.FileName;
                string errorString = ListViewSlateJPG.CheckSlateFile(file);
                if (!string.IsNullOrEmpty(errorString))
                {
                    MessageBox.Show(errorString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else // file has been validated
                {
                    IAsset asset;
                    progressBarUpload.Value = 0;
                    progressBarUpload.Visible = true;

                    buttonUploadSlate.Enabled = false;

                    asset = await Task.Factory.StartNew(() => ProcessUploadFile(file));
                    progressBarUpload.Visible = false;

                    buttonUploadSlate.Enabled = true;
                    listViewJPG1.LoadJPGs(MyContext, asset, MyChannel.Slate);
                }
            }
        }



        private IAsset ProcessUploadFile(string fileName, string storageAccount = null)
        {
            string safeFileName = Path.GetFileName(fileName);
            if (storageAccount == null) storageAccount = MyContext.DefaultStorageAccount.Name; // no storage account or null, then let's take the default one
            IAsset asset = null;
            try
            {
                asset = MyContext.Assets.CreateFromFile(
                                                      fileName,
                                                      storageAccount,
                                                      AssetCreationOptions.None,
                                                      (af, p) =>
                                                      {
                                                          progressBarUpload.BeginInvoke(new Action(() => progressBarUpload.Value = (int)p.Progress), null);
                                                      }
                                                      );
                AssetInfo.SetFileAsPrimary(asset, Path.GetFileName(safeFileName));
            }
            catch
            {
                asset = null;
            }
            return asset;
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
                    if (!Error) textBoxCueId.Text = GenerateRandomCueId();

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
                    string myurl = AssetInfo.DoPlayBackWithStreamingEndpoint(typeplayer: PlayerType.AzureMediaPlayerFrame, Urlstr: MyChannel.Preview.Endpoints.FirstOrDefault().Url.ToString(), DoNotRewriteURL: true, context: MyContext, formatamp: AzureMediaPlayerFormats.Smooth, technology: AzureMediaPlayerTechnologies.Silverlight, launchbrowser: false);
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
                    ILocator locator = GetOrCreateSASLocator(JPGAsset);
                    try
                    {
                        if (locator != null)
                        {
                            AF = JPGAsset.AssetFiles.FirstOrDefault();
                            Uri sasUri = BuildSasUri(AF, locator);
                            pictureBoxPreviewSlate.Load(sasUri.AbsoluteUri);

                            Image fileImage = pictureBoxPreviewSlate.Image;
                            double aspectRatioImage = (double)fileImage.Size.Width / (double)fileImage.Size.Height;
                            labelSlatePreviewInfo.Text = string.Format(labelSlatePreviewInfoText, fileImage.Width, fileImage.Height, aspectRatioImage);

                            if (
                                fileImage.Width > Constants.maxSlateJPGHorizontalResolution
                                || fileImage.Height > Constants.maxSlateJPGVerticalResolution
                                || !ListViewSlateJPG.AreClose(aspectRatioImage, Constants.SlateJPGAspectRatio)
                                )
                            {
                                labelSlatePreviewInfo.ForeColor = Color.Red;
                            }
                            else
                            {
                                labelSlatePreviewInfo.ForeColor = Color.Black;
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Error when accessing temporary SAS locator");
                    }
                }
            }
        }

        private static Uri BuildSasUri(IAssetFile assetFile, ILocator sasLocator)
        {
            UriBuilder builder = new UriBuilder(new Uri(sasLocator.Path, UriKind.Absolute));
            builder.Path = Path.Combine(builder.Path, assetFile.Name);
            return builder.Uri;
        }

        private ILocator GetOrCreateSASLocator(IAsset MyAsset)
        {
            if (!ListLocators.ContainsKey(MyAsset))
            {
                ListLocators.Add(MyAsset, CreateSASLocator(MyAsset));
            }
            return ListLocators[MyAsset];
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

        private void buttonDisregard_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBoxCueId_Validating(object sender, CancelEventArgs e)
        {
            bool Error = false;
            TextBox tb = (TextBox)sender;

            try
            {
                Convert.ToInt32(tb.Text);

            }
            catch
            {
                Error = true;
            }

            if (Error)
            {
                errorProvider1.SetError(tb, "Advertising Cue Id is not valid");
            }
            else
            {
                errorProvider1.SetError(tb, String.Empty);
            }
        }

        private void textBoxADSignalDuration_Validating(object sender, CancelEventArgs e)
        {
            bool Error = false;
            TextBox tb = (TextBox)sender;

            try
            {
                Convert.ToDouble(tb.Text);
            }
            catch
            {
                Error = true;
            }

            if (Error)
            {
                errorProvider1.SetError(tb, "Duration value is not valid");
            }
            else
            {
                errorProvider1.SetError(tb, String.Empty);
            }
        }

        private void checkBoxPreviewSlate_CheckedChanged(object sender, EventArgs e)
        {
            pictureBoxPreviewSlate.Visible = labelSlatePreviewInfo.Visible = checkBoxPreviewSlate.Checked;
            if (!checkBoxPreviewSlate.Checked)
            {
                pictureBoxPreviewSlate.Image = null;
                labelSlatePreviewInfo.Text = "";

            }
        }

        private void buttongenerateContentKey_Click(object sender, EventArgs e)
        {
            textBoxCueId.Text = GenerateRandomCueId();
        }

        private void splitContainer2_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void webBrowserPreview2_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }
    }
}
