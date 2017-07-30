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
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.IO;
using System.Text;
using System.Threading;
using Microsoft.WindowsAzure.MediaServices.Client;
using System.Globalization;
using System.Net;
using System.Web;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Outlook = Microsoft.Office.Interop.Outlook;
using System.Drawing;
using System.Diagnostics;
using Microsoft.WindowsAzure.MediaServices.Client.ContentKeyAuthorization;
using Microsoft.WindowsAzure.MediaServices.Client.DynamicEncryption;
using System.Xml.Linq;
using System.Collections;
using System.Reflection;
using System.Runtime.Serialization;
using Microsoft.Win32;
using System.ComponentModel;
using Newtonsoft.Json.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Net.Http;

namespace AMSExplorer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 

        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo(args[0], false);
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Mainform());
        }

        public static void dataGridViewV_Resize(object sender)
        {
            // let's resize the column name to fill the space
            DataGridView grid = (DataGridView)sender;
            int indexname = -1;
            for (int i = 0; i < grid.Columns.Count; i++)
            {
                if (grid.Columns[i].HeaderText == "Name")
                {
                    indexname = i;
                    break;
                }
            }

            if (indexname != -1)
            {
                grid.Columns[indexname].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                int colw = Math.Max(grid.Columns[indexname].Width, 100);
                grid.Columns[indexname].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                grid.Columns[indexname].Width = colw;
            }
        }

        public static CloudMediaContext ConnectAndGetNewContext(CredentialsEntry credentials, bool refreshToken = false, bool displayErrorMessageAndQuit = true)
        {
            CloudMediaContext myContext = null;
            if (credentials.UseAADInteract || credentials.UseAADServicePrincipal)
            {

                /*
                                string requestUrl = string.Format("https://login.microsoftonline.com/{0}/oauth2/logout?post_logout_redirect_uri={1}", credentials.ADTenantDomain, credentials.ADRestAPIEndpoint);
                                var client = new HttpClient();
                                var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
                                Task.Run(async () => { await client.SendAsync(request); }).Wait();
                */

                var env = credentials.ADCustomSettings == null ?
                    CredentialsEntry.ReturnADEnvironment(credentials.ADDeploymentName) : credentials.ADCustomSettings;

                AzureAdTokenProvider tokenProvider = null;

                if (credentials.UseAADInteract)
                {
                    var tokenCredentials1 = new AzureAdTokenCredentials(credentials.ADTenantDomain, env);
                    tokenProvider = new AzureAdTokenProvider(tokenCredentials1);
                }
                else // service principal
                {
                    AzureAdClientSymmetricKey clientSymmetricKey = new AzureAdClientSymmetricKey(credentials.ADSPClientId, credentials.ADSPClientSecret);
                    var tokenCredentials2 = new AzureAdTokenCredentials(credentials.ADTenantDomain, clientSymmetricKey, env);
                    tokenProvider = new AzureAdTokenProvider(tokenCredentials2);
                }
                myContext = new CloudMediaContext(new Uri(credentials.ADRestAPIEndpoint), tokenProvider);

            }
            else
            {
                if (credentials.UsePartnerAPI)
                {
                    // Get the service context for partner context.
                    try
                    {
                        Uri partnerAPIServer = new Uri(CredentialsEntry.PartnerAPIServer);
                        myContext = new CloudMediaContext(partnerAPIServer, credentials.AccountName, credentials.AccountKey, CredentialsEntry.PartnerScope, CredentialsEntry.PartnerACSBaseAddress);
                    }
                    catch (Exception e)
                    {
                        if (displayErrorMessageAndQuit)
                        {
                            MessageBox.Show("There is a credentials problem when connecting to Azure Media Services (custom API)." + Constants.endline + "Application will close. " + Constants.endline + e.Message);
                            Environment.Exit(0);
                        }
                        else
                        {
                            throw e;
                        }
                    }
                }
                else if (credentials.UseOtherAPI)
                {
                    try
                    {
                        Uri otherAPIServer = new Uri(credentials.OtherAPIServer);
                        myContext = new CloudMediaContext(otherAPIServer, credentials.AccountName, credentials.AccountKey, credentials.OtherScope, credentials.OtherACSBaseAddress);

                    }
                    catch (Exception e)
                    {
                        if (displayErrorMessageAndQuit)
                        {
                            MessageBox.Show("There is a credentials problem when connecting to Azure Media Services (Partner API)." + Constants.endline + "Application will close." + Constants.endline + e.Message);
                            Environment.Exit(0);
                        }
                        else
                        {
                            throw e;
                        }
                    }
                }
                else
                {
                    // Get the service context.
                    try
                    {
                        myContext = new CloudMediaContext(credentials.AccountName, credentials.AccountKey);
                    }
                    catch (Exception e)
                    {
                        if (displayErrorMessageAndQuit)
                        {
                            MessageBox.Show("There is a credentials problem when connecting to Azure Media Services." + Constants.endline + "Application will close." + Constants.endline + e.Message);
                            Environment.Exit(0);
                        }
                        else
                        {
                            throw e;
                        }
                    }
                }
            }

            if (false)//refreshToken)
            {
                try
                {
                    myContext.Credentials.RefreshToken(); // to force connection to WAMS
                }
                catch (Exception e)
                {
                    // Add useful information to the exception
                    if (displayErrorMessageAndQuit)
                    {
                        MessageBox.Show("There is a credentials problem when connecting to Azure Media Services." + Constants.endline + "Application will close." + Constants.endline + e.Message);
                        Environment.Exit(0);
                    }
                    else
                    {
                        throw e;
                    }
                }
            }

            myContext.NumberOfConcurrentTransfers = Properties.Settings.Default.NumberOfConcurrentTransfers;
            myContext.ParallelTransferThreadCount = Properties.Settings.Default.ParallelTransferThreadCount;

            return myContext;
        }

        public static string GetAPIServer(CredentialsEntry credentials)
        {
            if (credentials.UsePartnerAPI)
            {
                return CredentialsEntry.PartnerAPIServer;
            }
            else if (credentials.UseOtherAPI)
            {
                return credentials.OtherAPIServer;
            }
            else
            {
                return Constants.ProdAPIServer;
            }
        }

        public static string GetACSBaseAddress(CredentialsEntry credentials)
        {
            if (credentials.UsePartnerAPI)
            {
                return CredentialsEntry.PartnerACSBaseAddress;
            }
            else if (credentials.UseOtherAPI)
            {
                return credentials.OtherACSBaseAddress;
            }
            else
            {
                return Constants.ProdACSBaseAddress;
            }
        }


        public static bool ConnectToStorage(CloudMediaContext context, CredentialsEntry credentials)
        {
            try
            {
                CloudStorageAccount storageAccount = new CloudStorageAccount(new StorageCredentials(context.DefaultStorageAccount.Name, credentials.DefaultStorageKey), credentials.ReturnStorageSuffix(), true);
                CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
                cloudBlobClient.ListBlobs("testamseexplorer"); // just to test connection
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(string.Format("There is a problem when connecting to the Azure storage account.\r\nIs the storage key correct ?\r\n{0}", e.Message), "Storage Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        public static string GetErrorMessage(Exception e)
        {
            string s = "";

            while (e != null)
            {
                s = e.Message;
                e = e.InnerException;
            }
            return ParseXml(s);
        }

        public static string ParseXml(string strXml)
        {
            try
            {
                var message = XDocument
                    .Parse(strXml)
                    .Descendants()
                    .Where(d => d.Name.LocalName == "message")
                    .Select(d => d.Value)
                    .SingleOrDefault();

                return message;
            }
            catch
            {
                return strXml;
            }
        }

        // Return the new name of Media Reserved Unit
        public static string ReturnMediaReservedUnitName(ReservedUnitType unitType)
        {
            switch (unitType)
            {
                case ReservedUnitType.Basic:
                default:
                    return "S1";

                case ReservedUnitType.Standard:
                    return "S2";

                case ReservedUnitType.Premium:
                    return "S3";

            }
        }


        // Detect if this JSON or XML data or other and store in private var
        public static TypeConfig AnalyseConfigurationString(string config)
        {
            config = config.Trim();
            if (string.IsNullOrEmpty(config))
            {
                return TypeConfig.Empty;
            }
            if (config.StartsWith("<")) // XML data
            {
                return TypeConfig.XML;
            }
            else if (config.StartsWith("[") || config.StartsWith("{")) // JSON
            {
                return TypeConfig.JSON;
            }
            else // something else
            {
                return TypeConfig.Other;
            }
        }

        public static string AnalyzeTextAndReportSyntaxError(string myText)
        {
            string strReturn = string.Empty;
            var type = Program.AnalyseConfigurationString(myText);
            if (type == TypeConfig.JSON)
            {
                // Let's check JSON syntax
                try
                {
                    var jo = JObject.Parse(myText);
                }
                catch (Exception ex)
                {
                    strReturn = string.Format("JSON Syntax error: {0}", ex.Message);
                }
            }
            else if (type == TypeConfig.XML) // XML 
            {
                try
                {
                    var xml = XElement.Load(new StringReader(myText));
                }
                catch (Exception ex)
                {
                    strReturn = string.Format("XML Syntax error: {0}", ex.Message);
                }
            }

            return strReturn;
        }

        public static string FormatXml(string xml)
        {
            try
            {
                var doc = XDocument.Parse(xml);
                return doc.Declaration + Environment.NewLine + doc.ToString();
            }
            catch (Exception)
            {
                return xml;
            }
        }

        public static string AnalyzeAndIndentXMLJSON(string myText)
        {
            var type = Program.AnalyseConfigurationString(myText);
            if (type == TypeConfig.JSON)
            {
                // Let's check JSON syntax
                try
                {
                    dynamic obj = Newtonsoft.Json.JsonConvert.DeserializeObject(myText);
                    myText = Newtonsoft.Json.JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.Indented);
                }
                catch
                {
                }
            }
            else if (type == TypeConfig.XML) // XML 
            {
                try
                {
                    myText = FormatXml(myText);
                }
                catch
                {
                }
            }
            return myText;
        }

        public static ManifestGenerated LoadAndUpdateManifestTemplate(IAsset asset)
        {
            var mp4AssetFiles = asset.AssetFiles.ToList().Where(f => f.Name.EndsWith(".mp4", StringComparison.OrdinalIgnoreCase)).ToArray();
            var m4aAssetFiles = asset.AssetFiles.ToList().Where(f => f.Name.EndsWith(".m4a", StringComparison.OrdinalIgnoreCase)).ToArray();
            var mediaAssetFiles = asset.AssetFiles.ToList().Where(f => f.Name.EndsWith(".mp4", StringComparison.OrdinalIgnoreCase) || f.Name.EndsWith(".m4a", StringComparison.OrdinalIgnoreCase)).ToArray();

            if (mp4AssetFiles.Count() != 0 || m4aAssetFiles.Count() != 0)
            {
                // Prepare the manifest
                XDocument doc = XDocument.Load(Path.Combine(Application.StartupPath + Constants.PathManifestFile, @"Manifest.ism"));

                XNamespace ns = "http://www.w3.org/2001/SMIL20/Language";

                var bodyxml = doc.Element(ns + "smil");
                var body2 = bodyxml.Element(ns + "body");

                var switchxml = body2.Element(ns + "switch");

                // video tracks
                foreach (var file in mp4AssetFiles)
                {
                    switchxml.Add(new XElement(ns + "video", new XAttribute("src", file.Name)));
                }

                // audio tracks (m4a)
                foreach (var file in m4aAssetFiles)
                {
                    switchxml.Add(new XElement(ns + "audio", new XAttribute("src", file.Name), new XAttribute("title", Path.GetFileNameWithoutExtension(file.Name))));
                }

                if (m4aAssetFiles.Count() == 0)
                {
                    // audio track
                    var mp4AudioAssetFilesName = mp4AssetFiles.Where(f =>
                                                                (f.Name.ToLower().Contains("audio") && !f.Name.ToLower().Contains("video"))
                                                                ||
                                                                (f.Name.ToLower().Contains("aac") && !f.Name.ToLower().Contains("h264"))
                                                                );

                    var mp4AudioAssetFilesSize = mp4AssetFiles.OrderBy(f => f.ContentFileSize);

                    string mp4fileaudio = (mp4AudioAssetFilesName.Count() == 1) ? mp4AudioAssetFilesName.FirstOrDefault().Name : mp4AudioAssetFilesSize.FirstOrDefault().Name; // if there is one file with audio or AAC in the name then let's use it for the audio track
                    switchxml.Add(new XElement(ns + "audio", new XAttribute("src", mp4fileaudio), new XAttribute("title", "audioname")));
                }

                // manifest filename
                string name = CommonPrefix(mediaAssetFiles.Select(f => Path.GetFileNameWithoutExtension(f.Name)).ToArray());
                if (string.IsNullOrEmpty(name))
                {
                    name = "manifest";
                }
                else if (name.EndsWith("_") && name.Length > 1) // i string ends with "_", let's remove it
                {
                    name = name.Substring(0, name.Length - 1);
                }
                name = name + ".ism";

                return new ManifestGenerated() { Content = doc.Declaration.ToString() + Environment.NewLine + doc.ToString(), FileName = name };

            }
            else
            {
                return new ManifestGenerated() { Content = null, FileName = string.Empty }; // no mp4 in asset
            }
        }

        public class ManifestGenerated
        {
            public string FileName;
            public string Content;

        }

        private static string CommonPrefix(string[] ss)
        {
            if (ss.Length == 0)
            {
                return "";
            }

            if (ss.Length == 1)
            {
                return ss[0];
            }

            int prefixLength = 0;

            foreach (char c in ss[0])
            {
                foreach (string s in ss)
                {
                    if (s.Length <= prefixLength || s[prefixLength] != c)
                    {
                        return ss[0].Substring(0, prefixLength);
                    }
                }
                prefixLength++;
            }

            return ss[0]; // all strings identical
        }


        public static string ReturnS(int number)
        {
            return number > 1 ? "s" : "";
        }

        public static Uri AllReleaseNotesUrl = null;
        public static string MessageNewVersion = string.Empty;

        public static async void CheckAMSEVersion()
        {
            var webClient = new WebClient();
            webClient.DownloadStringCompleted += (sender, e) => DownloadVersionRequestCompleted(true, sender, e);
            //webClient.DownloadStringCompleted += DownloadVersionRequestCompleted;
            webClient.DownloadStringAsync(new Uri(Constants.GitHubAMSEVersionPrimary));
        }

        public static void DownloadVersionRequestCompleted(bool firsttry, object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                try
                {
                    Uri BinaryUrl = null;
                    Uri ReleaseNotesUrl = null;

                    var xmlversion = XDocument.Parse(e.Result);
                    Version versionAMSEGitHub = new Version(xmlversion.Descendants("Versions").Descendants("Production").Attributes("Version").FirstOrDefault().Value.ToString());
                    var RelNotesUrlXML = xmlversion.Descendants("Versions").Descendants("Production").Attributes("ReleaseNotesUrl").FirstOrDefault();
                    var AllRelNotesUrlXML = xmlversion.Descendants("Versions").Descendants("Production").Attributes("AllReleaseNotesUrl").FirstOrDefault();
                    var BinaryUrlXML = xmlversion.Descendants("Versions").Descendants("Production").Attributes("BinaryUrl").FirstOrDefault();

                    if (RelNotesUrlXML != null)
                    {
                        ReleaseNotesUrl = new Uri(RelNotesUrlXML.Value.ToString());
                    }
                    if (AllRelNotesUrlXML != null)
                    {
                        AllReleaseNotesUrl = new Uri(AllRelNotesUrlXML.Value.ToString());
                    }
                    if (BinaryUrlXML != null)
                    {
                        BinaryUrl = new Uri(BinaryUrlXML.Value.ToString());
                    }

                    Version versionAMSELocal = Assembly.GetExecutingAssembly().GetName().Version;
                    if (versionAMSEGitHub > versionAMSELocal)
                    {
                        MessageNewVersion = string.Format("A new version ({0}) is available on GitHub: {1}", versionAMSEGitHub, Constants.GitHubAMSEReleases);
                        var form = new SoftwareUpdate(ReleaseNotesUrl, versionAMSEGitHub, BinaryUrl);
                        form.ShowDialog();
                    }
                }
                catch
                {

                }
            }
            else if (firsttry)
            {
                var webClient = new WebClient();
                webClient.DownloadStringCompleted += (sender2, e2) => DownloadVersionRequestCompleted(false, sender2, e2);
                webClient.DownloadStringAsync(new Uri(Constants.GitHubAMSEVersionSecondary));
            }
        }



        public static Bitmap MakeRed(Bitmap original)
        {
            //make an empty bitmap the same size as original
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);

            for (int i = 0; i < original.Width; i++)
            {
                for (int j = 0; j < original.Height; j++)
                {
                    //get the pixel from the original image
                    Color originalColor = original.GetPixel(i, j);

                    //set the new image's pixel to the grayscale version
                    newBitmap.SetPixel(i, j, Color.FromArgb(originalColor.A, 255, originalColor.G, originalColor.B));
                }
            }
            return newBitmap;
        }

        public static Bitmap MakeBlue(Bitmap original)
        {
            //make an empty bitmap the same size as original
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);

            for (int i = 0; i < original.Width; i++)
            {
                for (int j = 0; j < original.Height; j++)
                {
                    //get the pixel from the original image
                    Color originalColor = original.GetPixel(i, j);

                    //set the new image's pixel to the grayscale version
                    newBitmap.SetPixel(i, j, Color.FromArgb(originalColor.A, originalColor.R, originalColor.G, 255));
                }
            }
            return newBitmap;
        }


        public static DialogResult InputBox(string title, string promptText, ref string value, bool passwordWildcard = false)
        {
            Button buttonOk = new Button()
            {
                Text = AMSExplorer.Properties.Resources.ButtonOK,
                DialogResult = DialogResult.OK,
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right
            };

            Button buttonCancel = new Button()
            {
                Text = AMSExplorer.Properties.Resources.ButtonCancel,
                DialogResult = DialogResult.Cancel,
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right
            };


            Form form = new Form()
            {
                ClientSize = new Size(396, 107),
                Text = title,
                StartPosition = FormStartPosition.CenterScreen,
                MinimizeBox = false,
                MaximizeBox = false,
                AcceptButton = buttonOk,
                CancelButton = buttonCancel,
                FormBorderStyle = FormBorderStyle.FixedDialog
            };

            Label label = new Label()
            {
                AutoSize = true,
                Text = promptText
            };
            TextBox textBox = new TextBox()
            {
                Text = value,
                UseSystemPasswordChar = passwordWildcard
            };

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }

        public static void SaveAndProtectUserConfig()
        {
            try
            {
                Properties.Settings.Default.Save();
            }
            catch
            {

            }

            /*
            try
            {
                string assemblyname = Assembly.GetExecutingAssembly().GetName().Name;
                System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal);
                ConfigurationSection connStrings = config.GetSection("userSettings/" + assemblyname + ".Properties.Settings");

                if (connStrings != null)
                {
                    if (!connStrings.SectionInformation.IsProtected)
                    {
                        if (!connStrings.ElementInformation.IsLocked)
                        {
                            connStrings.SectionInformation.ProtectSection("RsaProtectedConfigurationProvider");
                            connStrings.SectionInformation.ForceSave = true;
                            config.Save(ConfigurationSaveMode.Full);
                        }
                    }
                }
            }


            catch (Exception e)
            {
                MessageBox.Show("Step2 " + e.Message);
            }
             * */


            // let's decrypt as encryption create issues with some users
            try
            {
                string assemblyname = Assembly.GetExecutingAssembly().GetName().Name;
                System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal);
                ConfigurationSection connStrings = config.GetSection("userSettings/" + assemblyname + ".Properties.Settings");

                if (connStrings != null)
                {
                    if (connStrings.SectionInformation.IsProtected)
                    {
                        if (!connStrings.ElementInformation.IsLocked)
                        {
                            connStrings.SectionInformation.UnprotectSection();
                            connStrings.SectionInformation.ForceSave = true;
                            config.Save(ConfigurationSaveMode.Full);
                        }
                    }
                }
            }


            catch (Exception e)
            {
                MessageBox.Show("Error " + e.Message);
            }

        }


        public static bool CreateAndSendOutlookMail(string RecipientEmailAddress, string Subject, string Body)
        {
            // Let's create the email with Outlook
            try
            {
                Outlook.Application outlookApp = new Outlook.Application();
                Outlook.MailItem mailItem = (Outlook.MailItem)outlookApp.CreateItem(Outlook.OlItemType.olMailItem);

                mailItem.To = RecipientEmailAddress;
                mailItem.Subject = Subject;
                mailItem.HTMLBody = "<FONT Face=\"Courier New\">";
                mailItem.HTMLBody += Body.Replace(" ", "&nbsp;").Replace(Environment.NewLine, "<br />").ToString();
                mailItem.Display(false);
                mailItem.Send();
                return true;
            }
            catch
            {
                return false;
            }
        }


        public static HttpStatusCode WatchFolderCallApi(string error, string sourcefilename, WatchFolderSettings settings, IAsset sourceAsset = null, IAsset outputAsset = null, IJob job = null, ILocator locator = null, Uri publishUrl = null, string playbackUrl = null)
        {
            if (settings == null || settings.CallAPIUrl == null) return HttpStatusCode.BadRequest;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(settings.CallAPIUrl);
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";

            string body = settings.CallAPJson;

            if (job != null) // let's refresh the job
            {
                job = job.GetMediaContext().Jobs.Where(j => j.Id == job.Id).FirstOrDefault();
            }

            body = body.Replace("\"{Error}\"", error ?? "");
            body = body.Replace("\"{Source Asset Id}\"", sourceAsset != null ? JsonConvert.ToString(sourceAsset.Id) : "").Replace("\"{Source Asset Name}\"", sourceAsset != null ? JsonConvert.ToString(sourceAsset.Name) : "");
            body = body.Replace("\"{Source File Name}\"", sourcefilename != null ? JsonConvert.ToString(sourcefilename) : "");
            body = body.Replace("\"{Output Asset Id}\"", outputAsset != null ? JsonConvert.ToString(outputAsset.Id) : "").Replace("\"{Output Asset Name}\"", outputAsset != null ? JsonConvert.ToString(outputAsset.Name) : "");
            body = body.Replace("\"{Job Id}\"", job != null ? JsonConvert.ToString(job.Id) : "").Replace("\"{Job State}\"", job != null ? JsonConvert.ToString(job.State.ToString()) : "");
            body = body.Replace("\"{Locator Id}\"", locator != null ? JsonConvert.ToString(locator.Id) : "").Replace("\"{Publish Url}\"", publishUrl != null ? JsonConvert.ToString(publishUrl.ToString()) : "").Replace("\"{Playback Url}\"", publishUrl != null ? JsonConvert.ToString(playbackUrl) : "");

            /*
          {
  "Error": "{Error}",
  "SourceAssetId": "{Source Asset Id}",
  "SourceAssetName": "{Source Asset Name}",
  "SourceFileName": "{Source File Name}",
  "OutputAssetId": "{Output Asset Id}",
  "OutputAssetName": "{Output Asset Name}",
  "JobId": "{Job Id}",
  "JobState": "{Job State}",
  "LocatorId": "{Locator Id}",
  "PublishUrl": "{Publish Url}",
  "Playback Url": "{Playback Url}"
}
      */

            using (Stream requestStream = request.GetRequestStream())
            {
                var requestBytes = System.Text.Encoding.ASCII.GetBytes(body);
                requestStream.Write(requestBytes, 0, requestBytes.Length);
                requestStream.Close();
            }
            HttpWebResponse response = null;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch
            {
            }

            if (response != null)
            {
                return response.StatusCode;
            }
            else
            {
                return HttpStatusCode.BadRequest;
            }

        }

        public static string ReturnNameForProtocol(StreamingProtocol protocol)
        {
            string name = "";
            switch (protocol)
            {
                case StreamingProtocol.FragmentedMP4:
                    name = AMSExplorer.Properties.Resources.Program_ReturnNameForProtocol_FragmentedMP4Smooth;
                    break;

                case StreamingProtocol.RTMP:
                    name = AMSExplorer.Properties.Resources.Program_ReturnNameForProtocol_RTMP;
                    break;

                case StreamingProtocol.RTPMPEG2TS:
                    name = AMSExplorer.Properties.Resources.Program_ReturnNameForProtocol_RTPMPEG2TransportStream;
                    break;
            }
            return name;
        }


        // set WebBrowser features, more info: http://stackoverflow.com/a/18333982/1768303
        public static void SetWebBrowserFeatures()
        {
            // don't change the registry if running in-proc inside Visual Studio
            if (LicenseManager.UsageMode != LicenseUsageMode.Runtime)
                return;

            var appName = System.IO.Path.GetFileName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);

            var featureControlRegKey = @"HKEY_CURRENT_USER\Software\Microsoft\Internet Explorer\Main\FeatureControl\";

            Registry.SetValue(featureControlRegKey + "FEATURE_BROWSER_EMULATION",
                appName, GetBrowserEmulationMode(), RegistryValueKind.DWord);

            // enable the features which are "On" for the full Internet Explorer browser

            Registry.SetValue(featureControlRegKey + "FEATURE_ENABLE_CLIPCHILDREN_OPTIMIZATION",
                appName, 1, RegistryValueKind.DWord);

            Registry.SetValue(featureControlRegKey + "FEATURE_AJAX_CONNECTIONEVENTS",
                appName, 1, RegistryValueKind.DWord);

            Registry.SetValue(featureControlRegKey + "FEATURE_GPU_RENDERING",
                appName, 1, RegistryValueKind.DWord);

            Registry.SetValue(featureControlRegKey + "FEATURE_WEBOC_DOCUMENT_ZOOM",
                appName, 1, RegistryValueKind.DWord);

            Registry.SetValue(featureControlRegKey + "FEATURE_NINPUT_LEGACYMODE",
                appName, 0, RegistryValueKind.DWord);


        }


        static UInt32 GetBrowserEmulationMode()
        {
            int browserVersion = 0;
            using (var ieKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Internet Explorer",
                RegistryKeyPermissionCheck.ReadSubTree,
                System.Security.AccessControl.RegistryRights.QueryValues))
            {
                var version = ieKey.GetValue("svcVersion");
                if (null == version)
                {
                    version = ieKey.GetValue("Version");
                    if (null == version)
                        throw new ApplicationException("Microsoft Internet Explorer is required!");
                }
                int.TryParse(version.ToString().Split('.')[0], out browserVersion);
            }

            if (browserVersion < 7)
            {
                throw new ApplicationException("Unsupported version of Microsoft Internet Explorer!");
            }

            UInt32 mode = 11000; // Internet Explorer 11. Webpages containing standards-based !DOCTYPE directives are displayed in IE11 Standards mode. 

            switch (browserVersion)
            {
                case 7:
                    mode = 7000; // Webpages containing standards-based !DOCTYPE directives are displayed in IE7 Standards mode. 
                    break;
                case 8:
                    mode = 8000; // Webpages containing standards-based !DOCTYPE directives are displayed in IE8 mode. 
                    break;
                case 9:
                    mode = 9000; // Internet Explorer 9. Webpages containing standards-based !DOCTYPE directives are displayed in IE9 mode.                    
                    break;
                case 10:
                    mode = 10000; // Internet Explorer 10.
                    break;
            }

            return mode;
        }




    }

    public class Constants
    {
        public const string GitHubAMSEVersionPrimary = "https://amsexplorer.azureedge.net/release/version.xml";
        public const string GitHubAMSEVersionSecondary = "https://raw.githubusercontent.com/Azure/Azure-Media-Services-Explorer/master/version.xml";

        public const string GitHubAMSEReleases = "https://github.com/Azure/Azure-Media-Services-Explorer/releases";
        public const string GitHubAMSELink = "http://aka.ms/amse";

        public const string WindowsAzureMediaEncoder = "Windows Azure Media Encoder";
        public const string AzureMediaEncoder = "Azure Media Encoder";
        public const string AzureMediaEncoderStandard = "Media Encoder Standard";
        public const string AzureMediaEncoderPremiumWorkflow = "Media Encoder Premium Workflow";
        public const string AzureMediaIndexer = "Azure Media Indexer";
        public const string AzureMediaIndexer2Preview = "Azure Media Indexer 2 Preview";
        public const string AzureMediaHyperlapse = "Azure Media Hyperlapse";
        public const string AzureMediaFaceDetector = "Azure Media Face Detector";
        public const string AzureMediaRedactor = "Azure Media Redactor";
        public const string AzureMediaMotionDetector = "Azure Media Motion Detector";
        public const string AzureMediaStabilizer = "Azure Media Stabilizer";
        public const string AzureMediaVideoThumbnails = "Azure Media Video Thumbnails";
        public const string AzureMediaVideoOCR = "Azure Media OCR";
        public const string AzureMediaContentModerator = "Azure Media Content Moderator";
        public const string AzureMediaVideoAnnotator = "Azure Media Video Annotator";

        public const string NameconvInputasset = "{Input Asset Name}";
        public const string NameconvUploadasset = "{File Name}";
        public const string NameconvWorkflow = "{Workflow}";
        public const string NameconvTemplate = "{Template}";
        public const string NameconvAMEpreset = "{Preset}";
        public const string NameconvFormathls = "{Format}";
        public const string NameconvEncodername = "{Encoder}";
        public const string NameconvProcessorname = "{Processor}";
        public const string NameconvChannel = "{Channel}";
        public const string NameconvProgram = "{Program}";
        public const string NameconvProtocols = "{Protocols}";
        public const string NameconvContentKeyType = "{Content key type}";
        public const string NameconvManifestURL = "{manifest url}";
        public const string NameconvToken = "{token}";
        public const string NameconvAsset = "{Asset Name}";
        public const string NameconvRedactionMode = "{Redaction Mode}";
        public const string NameconvModerationMode = "{Moderation Mode}";


        public const string endline = "\r\n";

        public const string PathPremiumWorkflowFiles = @"\PremiumWorkflowSamples\";
        public const string PathAMEFiles = @"\AMEPresetFiles\";
        public const string PathMESFiles = @"\MESPresetFiles\";
        public const string PathConfigFiles = @"\configurations\";
        public const string PathManifestFile = @"\manifest\";
        public const string PathHelpFiles = @"\HelpFiles\";
        public const string PathDefaultSlateJPG = @"\SlateJPG\";

        public const string PathLicense = @"\license\Azure Media Services Explorer.rtf";

        public const string PlayerAMPinOptions = @"https://ampdemo.azureedge.net/azuremediaplayer.html?player=flash&format=smooth&url={0}";
        public const string PlayerAMP = @"https://aka.ms/azuremediaplayer";
        public const string PlayerAMPToLaunch = @"https://aka.ms/azuremediaplayer?url={0}";

        public const string PlayerAMPIFrameToLaunch = @"https://ampdemo.azureedge.net/azuremediaplayer_embed.html?autoplay=true&url={0}";
        public const string AMPprotectionsyntax = "&protection={0}";
        public const string AMPtokensyntax = "&token={0}";
        public const string AMPformatsyntax = "&format={0}";
        public const string AMPtechsyntax = "&tech={0}";
        public const string AMPPlayReady = "&playready={0}";
        public const string AMPPlayReadyToken = "&playreadytoken={0}";
        public const string AMPWidevine = "&widevine={0}";
        public const string AMPWidevineToken = "&widevinetoken={0}";
        public const string AMPAes = "&aes={0}";
        public const string AMPAesToken = "&aestoken={0}";
        public const string AMPSubtitles = "&subtitles={0}";

        public const string PlayerDASHIFList = @"http://dashif.org/reference/players/javascript/";
        public const string PlayerDASHIFToLaunch = @"http://dashif.org/reference/players/javascript/v2.2.0/samples/dash-if-reference-player/index.html?url={0}";

        public const string PlayerMP4AzurePage = @"https://ampdemo.azureedge.net/azuremediaplayer.html?player=html5&format=mp4&url={0}&mp4url={0}";

        public const string Player3IVXHLS = @"http://apps.microsoft.com/windows/en-us/app/3ivx-hls-player/f79ce7d0-2993-4658-bc4e-83dc182a0614";
        public const string PlayerOSMFRCst = @"http://wamsclient.cloudapp.net/release/setup.html";
        public const string PlayerInfoHTML5Video = @"http://www.w3schools.com/html/html5_video.asp";
        public const string PlayerJWPlayerPartnership = @"http://www.jwplayer.com/partners/azure/";
        public const string PlayerTHEOplayerPartnership = @"https://www.theoplayer.com/partners/azure";

        public const string DemoCaptionMaker = @"http://go.microsoft.com/fwlink/p/?LinkID=251121";
        public const string AMSSamples = @"https://github.com/AzureMediaServicesSamples";

        public const string LinkFeedbackAMS = "http://aka.ms/amsvoice";
        public const string LinkInfoMediaUnit = "https://docs.microsoft.com/en-us/azure/media-services/media-services-scale-media-processing-overview";

        public const string TemporaryWidevineLicenseServer = "https://thiswillbereplacedbytheAMSwidevineurl/?KID=00000000-0000-0000-0000-000000000000";

        public static readonly string[] BrowserEdge = { "Microsoft Edge", "microsoft-edge:" };
        public static readonly string[] BrowserIE = { "Internet Explorer", "iexplore.exe" };
        public static readonly string[] BrowserChrome = { "Google Chrome", "chrome.exe" };

        public const string LocatorIdPrefix = "nb:lid:UUID:";
        public const string AssetIdPrefix = "nb:cid:UUID:";
        public const string AssetFileIdPrefix = "nb:cid:UUID:";
        public const string ContentKeyIdPrefix = "nb:kid:UUID:";
        public const string JobIdPrefix = "nb:jid:UUID:";
        public const string TaskIdPrefix = "nb:tid:UUID:";
        public const string ChannelIdPrefix = "nb:chid:UUID:";
        public const string ProgramIdPrefix = "nb:pgid:UUID:";

        public const string ProdAPIServer = "https://media.windows.net";
        public const string ProdACSBaseAddress = "https://wamsprodglobal001acs.accesscontrol.windows.net";

        public const string Bearer = "Bearer ";
        public const string strUnits = "{0} unit{1}";

        public const string LinkMoreInfoAMSReleaseNotes = @"https://docs.microsoft.com/en-us/azure/media-services/media-services-release-notes";
        public const string LinkMoreInfoDocAMS = @"https://docs.microsoft.com/en-us/azure/media-services/";
        public const string LinkForumAMS = @"https://social.msdn.microsoft.com/Forums/azure/en-US/home?forum=MediaServices";
        public const string LinkBlogAMS = @"https://azure.microsoft.com/en-us/blog/topics/media-services-2/";

        public const string LinkMoreInfoAME = "https://docs.microsoft.com/en-us/azure/media-services/media-services-media-encoder-standard-formats";
        public const string LinkMorePresetsAME = "https://msdn.microsoft.com/library/azure/dn619392.aspx";
        public const string LinkMoreInfoMES = "http://azure.microsoft.com/blog/2015/07/16/announcing-the-general-availability-of-media-encoder-standard/";
        public const string LinkMorePresetsMES = "http://go.microsoft.com/fwlink/?LinkId=618336";
        public const string LinkThumbnailsMES = "https://docs.microsoft.com/en-us/azure/media-services/media-services-dotnet-generate-thumbnail-with-mes";
        public const string LinkPreserveResRotationMES = "https://msdn.microsoft.com/en-US/library/mt269962#PreserveResolutionAfterRotation";
        public const string LinkOverlayMES = "https://docs.microsoft.com/en-us/azure/media-services/media-services-advanced-encoding-with-mes#overlay";
        public const string LinkCroppingMES = "https://docs.microsoft.com/en-us/azure/media-services/media-services-crop-video";
        public const string LinkMESAdvFeatures = "https://docs.microsoft.com/en-us/azure/media-services/media-services-advanced-encoding-with-mes";
        public const string LinkMoreInfoSE = "https://docs.microsoft.com/en-us/azure/media-services/media-services-streaming-endpoints-overview";
        public const string LinkMESAutoGenPreset = "https://docs.microsoft.com/en-us/azure/media-services/media-services-autogen-bitrate-ladder-with-mes";

        public const string LinkMoreInfoAzCopy = "https://docs.microsoft.com/en-us/azure/storage/storage-use-azcopy";

        public const string LinkSigniantFlightMarketPlace = "https://azure.microsoft.com/en-us/marketplace/partners/signiant/flight/";
        public const string LinkSigniantFlightRequestTrialKey = "http://info.signiant.com/flight-Free-Trial_1.html";

        public const string LinkAspera = "https://azure.microsoft.com/en-us/marketplace/partners/aspera/sod/";

        public const string LinkMoreAMEAdvanced = "http://azure.microsoft.com/blog/2014/08/21/advanced-encoding-features-in-azure-media-encoder/";
        public const string LinkMoreInfoPremiumEncoder = "https://docs.microsoft.com/en-us/azure/media-services/media-services-encode-with-premium-workflow";
        public const string LinkMoreInfoHyperlapse = "https://docs.microsoft.com/en-us/azure/media-services/media-services-hyperlapse-content";
        public const string LinkHowItWorksHyperlapse = "http://research.microsoft.com/en-us/um/redmond/projects/hyperlapse/";
        public const string LinkMoreInfoIndexer = "https://docs.microsoft.com/en-us/azure/media-services/media-services-index-content";
        public const string LinkMoreInfoIndexerV2 = "https://docs.microsoft.com/en-us/azure/media-services/media-services-process-content-with-indexer2";
        public const string LinkMoreInfoVideoOCR = "https://docs.microsoft.com/en-us/azure/media-services/media-services-video-optical-character-recognition";
        public const string LinkHowIMoreInfoDynamicManifest = "http://azure.microsoft.com/blog/2015/05/28/dynamic-manifest/";
        public const string LinkHowIMoreInfoSubclipping = "http://azure.microsoft.com/blog/2015/04/14/dynamic-manifests-and-rendered-sub-clips/";
        public const string LinkMoreInfoSubClipAMSE = "https://azure.microsoft.com/en-us/blog/sub-clipping-and-live-archive-extraction-with-media-encoder-standard/";
        public const string LinkMoreInfoLiveEncoding = "https://docs.microsoft.com/en-us/azure/media-services/media-services-manage-live-encoder-enabled-channels";
        public const string LinkMoreInfoLiveStreaming = "https://docs.microsoft.com/en-us/azure/media-services/media-services-manage-channels-overview";
        public const string LinkMoreInfoPricing = "http://azure.microsoft.com/en-us/pricing/details/media-services/";
        public const string LinkMoreInfoStorageVersioning = "https://msdn.microsoft.com/en-us/library/azure/dd894041.aspx";
        public const string LinkMoreInfoStorageAnalytics = "https://msdn.microsoft.com/library/azure/hh343258.aspx";
        public const string LinkMoreInfoFairPlay = "https://docs.microsoft.com/en-us/azure/media-services/media-services-protect-hls-with-fairplay";
        public const string LinkMoreInfoTelemetry = "https://docs.microsoft.com/en-us/azure/media-services/media-services-telemetry-overview";

        public const string LinkMoreYammerAMSPreview = "https://www.yammer.com/azureadvisors/#/threads/inGroup?type=in_group&feedId=3165917";
        public const string LinkMoreInfoMotionDetection = "https://docs.microsoft.com/en-us/azure/media-services/media-services-motion-detection";
        public const string LinkMoreInfoFaceDetection = "https://docs.microsoft.com/en-us/azure/media-services/media-services-face-and-emotion-detection";
        public const string LinkMoreInfoFaceRedaction = "https://docs.microsoft.com/en-us/azure/media-services/media-services-face-redaction";
        public const string LinkMoreInfoVideoSummarization = "https://docs.microsoft.com/en-us/azure/media-services/media-services-video-summarization";
        public const string LinkMoreInfoContentModeration = "https://azure.microsoft.com/en-us/blog/content-moderator-azure-media-analytics/";

        public const string LinkPlayReadyTemplateInfo = "https://docs.microsoft.com/en-us/azure/media-services/media-services-playready-license-template-overview";
        public const string LinkPlayReadyCompliance = "http://www.microsoft.com/playready/licensing/compliance/";
        public const string LinkWidevineTemplateInfo = "https://docs.microsoft.com/en-us/azure/media-services/media-services-widevine-license-template-overview";

        public const string LinkAMSCreateAccount = "https://docs.microsoft.com/en-us/azure/media-services/media-services-portal-create-account";
        public const string LinkAMSAADAut = "https://docs.microsoft.com/en-us/azure/media-services/media-services-portal-get-started-with-aad";

        public const string LinkAMSE = "http://aka.ms/amse";
        public const string LinkMailtoAMSE = "mailto:amse@microsoft.com?subject=Azure Media Services Explorer - Question/Comment";

        public const string AzureNotificationNameWatchFolder = "explorer-watch-folder";

        public const long maxSlateJPGFileSize = 3 * 1024 * 1024; // Max 3 MB
        public const int maxSlateJPGHorizontalResolution = 1920;
        public const int maxSlateJPGVerticalResolution = 1080;
        public const double SlateJPGAspectRatio = 16d / 9d;
        public const string SlateJPGExtension = ".jpg";

        public const string stringNull = "(null)"; // To display null is textbox

        public const string FaceDetectionFaces = "Faces";
        public const string FaceDetectionAggregateEmotion = "AggregateEmotion";
        public const string FaceDetectionPerFaceEmotion = "PerFaceEmotion";

        public const string FaceRedactionCombined = "combined";
        public const string FaceRedactionFirstPass = "analyze";
        public const string FaceRedactionSecondPass = "redact";

        public const string ContentModerationBalance = "Balance";
        public const string ContentModerationQuality = "Quality";
        public const string ContentModerationSpeed = "Speed";


        public const string VideoThumbnailsOutputVideo = "video";
        public const string VideoThumbnailsOutputImage = "image";
        public const string VideoThumbnailsOutputBoth = "both";

        public const int MaxTransfersAsUnlimited = 5;
        public const string strTransfers = "{0} concurrent transfer{1}";
    }




    public class JobInfo
    {
        private List<IJob> SelectedJobs;
        private string _accountname;

        public JobInfo(IJob job, string accountname)
        {
            SelectedJobs = new List<IJob>();
            SelectedJobs.Add(job);
            _accountname = accountname;

        }
        public JobInfo(List<IJob> MySelectedJobs, string accountname)
        {
            SelectedJobs = MySelectedJobs;
            _accountname = accountname;
        }

        public void CreateOutlookMail()
        {
            Exception exception = null;
            try
            {
                StringBuilder SB = GetStats();

                // Let's create the email with Outlook
                Outlook.Application outlookApp = new Outlook.Application();
                Outlook.MailItem mailItem = (Outlook.MailItem)outlookApp.CreateItem(Outlook.OlItemType.olMailItem);
                if (SelectedJobs.Count == 1)
                {
                    string title = (SelectedJobs.FirstOrDefault().State == JobState.Error) ? "ERROR Report: Job '{0}'" : "Report: Job '{0}'";

                    mailItem.Subject = string.Format(title, SelectedJobs.FirstOrDefault().Name);
                }
                else
                {
                    mailItem.Subject = string.Format("Report: {0} jobs, {1} Error(s)", SelectedJobs.Count(), SelectedJobs.Where(j => j.State == JobState.Error).Count());
                }

                mailItem.HTMLBody = "<FONT Face=\"Courier New\">";
                mailItem.HTMLBody += SB.Replace(" ", "&nbsp;").Replace(Environment.NewLine, "<br />").ToString();
                mailItem.Display(false);
            }


            catch (System.Runtime.InteropServices.COMException ce)
            {
                // 0x80040154 Class not registered
                // This happen if outlook is not installed
                if (ce.HResult == unchecked((int)0x80040154))
                {
                    MessageBox.Show("Please install Office Outlook to use this functionality.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                exception = ce;
            }
            catch (Exception e)
            {
                exception = e;
            }

            if (exception != null)
            {
                MessageBox.Show("Exception while trying to compose the email." + exception, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void CopyStatsToClipBoard()
        {
            StringBuilder SB = GetStats();
            Clipboard.SetText((string)SB.ToString());
        }

        public static long GetInputFilesSize(ITask task) // return -1 if one asset has been deleted
        {
            // Loop through the HistoricalEvents associated with the Task to find Tasks that have Finished
            // Task.State only has the Conpleted State and based on that it is not possible to know whether the task had an error or did it finish successfully
            long lSize = 0;
            bool sizecanbecalculated = false;
            if (task.State == JobState.Finished)
            {
                lSize = 0;
                sizecanbecalculated = true;

                try
                {
                    foreach (IAsset asset in task.InputAssets)
                    {
                        if (asset.State == AssetState.Deleted)
                        {
                            sizecanbecalculated = false;
                        }
                        else
                        {
                            foreach (IAssetFile fileItem in asset.AssetFiles)
                            {
                                lSize += fileItem.ContentFileSize;
                            }
                        }
                    }
                }

                catch
                {
                    sizecanbecalculated = false;
                }
            }

            if (sizecanbecalculated)
            {
                return lSize;
            }
            else
            {
                return -1;
            }
        }

        public static long GetOutputFilesSize(ITask task) // return -1 if one asset has been deleted
        {
            // Loop through the HistoricalEvents associated with the Task to find Tasks that have Finished
            // Task.State only has the Conpleted State and based on that it is not possible to know whether the task had an error or did it finish successfully
            long lSize = 0;
            bool sizecanbecalculated = false;
            if (task.State == JobState.Finished)
            {
                lSize = 0;
                sizecanbecalculated = true;


                try
                {
                    foreach (IAsset asset in task.OutputAssets)
                    {
                        if (asset.State == AssetState.Deleted)
                        {
                            sizecanbecalculated = false;
                        }
                        else
                        {
                            foreach (IAssetFile fileItem in asset.AssetFiles)
                            {
                                lSize += fileItem.ContentFileSize;
                            }
                        }
                    }
                }

                catch
                {
                    sizecanbecalculated = false;
                }


            }
            if (sizecanbecalculated)
            {
                return lSize;
            }
            else
            {
                return -1;
            }
        }

        public static TaskSizeAndPrice CalculateJobSizeAndPrice(IJob job)
        {
            long totalinputsize = 0;
            long totalouputsize = 0;
            double totalprice = 0;
            bool inputsizecanbecalculated = true;
            bool outputsizecanbecalculated = true;
            bool pricecanbecalculated = true;

            foreach (ITask task in job.Tasks)
            {
                TaskSizeAndPrice taskinfo = CalculateTaskSizeAndPrice(task, (CloudMediaContext)job.GetMediaContext());
                if (taskinfo.InputSize != -1)
                {
                    totalinputsize += taskinfo.InputSize;
                }
                else inputsizecanbecalculated = false;

                if (taskinfo.OutputSize != -1)
                {
                    totalouputsize += taskinfo.OutputSize;
                }
                else outputsizecanbecalculated = false;

                if (taskinfo.Price != -1)
                {
                    totalprice += taskinfo.Price;
                }
                else pricecanbecalculated = false;
            }

            return new TaskSizeAndPrice
            {
                InputSize = inputsizecanbecalculated ? totalinputsize : -1,
                OutputSize = outputsizecanbecalculated ? totalouputsize : -1,
                Price = pricecanbecalculated ? totalprice : -1
            };
        }

        public static TaskSizeAndPrice CalculateTaskSizeAndPrice(ITask task, CloudMediaContext context)
        {
            IMediaProcessor processor = JobInfo.GetMediaProcessorFromId(task.MediaProcessorId, context);
            StringBuilder sb = new StringBuilder();

            long lSizeinput = -1;
            long lSizeoutput = -1;
            double pricetask = -1;

            if (task.State == JobState.Finished)
            {
                lSizeinput = JobInfo.GetInputFilesSize(task);
                lSizeoutput = JobInfo.GetOutputFilesSize(task);

                if (lSizeinput != -1 && lSizeoutput != -1)
                {
                    double lsizeinputprocessed = (double)lSizeinput / (1024 * 1024 * 1024);
                    double lsizeoutputprocessed = (double)lSizeoutput / (1024 * 1024 * 1024);

                    if (processor != null)
                    {

                        switch (processor.Name)
                        {
                            case (Constants.AzureMediaEncoder):
                            case (Constants.AzureMediaEncoderStandard):
                                // AME or Media Standard Encoding task
                                //pricetask = lsizeoutputprocessed * (double)Properties.Settings.Default.AMEPrice;
                                break;
                            case (Constants.AzureMediaEncoderPremiumWorkflow):
                                // Premium Workflow Encoding task
                                //pricetask = lsizeoutputprocessed * (double)Properties.Settings.Default.MEPremiumWorkflowPrice;
                                break;
                            case (MediaProcessorNames.StorageDecryption):
                            case (MediaProcessorNames.WindowsAzureMediaEncryptor):
                            case (MediaProcessorNames.WindowsAzureMediaPackager):
                                // No cost
                                pricetask = 0;
                                break;
                            case (Constants.AzureMediaIndexer):
                                // Indexing task
                                // TO DO: GET DURATION OF CONTENT
                                //pricetask = ?
                                break;
                            case (Constants.AzureMediaHyperlapse):
                                // Hyperlapse task
                                // TO DO when final cost 
                                //pricetask = ?
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            return new TaskSizeAndPrice()
            {
                InputSize = lSizeinput,
                OutputSize = lSizeoutput,
                Price = pricetask
            };

        }

        private static long ListFilesInAsset(IAsset asset, ref StringBuilder builder)
        {
            // Display the files associated with each asset. 

            long assetSize = 0;
            foreach (IAssetFile fileItem in asset.AssetFiles)
            {
                if (fileItem.IsPrimary)
                {
                    builder.AppendLine("   ------------(-P-R-I-M-A-R-Y-)------------------");
                }
                else
                {
                    builder.AppendLine("   -----------------------------------------------");
                }

                builder.AppendLine("   File Name           : " + fileItem.Name);
                builder.AppendLine("   Size                : " + fileItem.ContentFileSize + " Bytes");
                builder.AppendLine("   Last Modified (UTC) : " + fileItem.LastModified.ToString("G"));
                builder.AppendLine("");
                assetSize += fileItem.ContentFileSize;
            }
            return assetSize;
        }

        private static void ListAssetInfo(IAsset asset, ref StringBuilder builder)
        {
            // Display the info associated with asset. 
            builder.AppendLine("===============================================");
            builder.AppendLine("Asset Name          : " + asset.Name);
            builder.AppendLine("Asset Id            : " + asset.Id);
            builder.AppendLine("Last Modified (UTC) : " + asset.LastModified.ToString("G"));
        }

        public static IMediaProcessor GetMediaProcessorFromId(string processorID, CloudMediaContext _context)
        {
            bool Error = false;
            IMediaProcessor processor = null;
            try
            {
                var processorquery =
                  from p in _context.MediaProcessors
                  orderby p.Version descending
                  where p.Id == processorID
                  select p;
                // Reference the asset as an IAsset.
                processor = processorquery.FirstOrDefault(); //lastordefault returns an error so that's why we use first with sorting descending
            }
            catch
            {
                Error = true;
            }
            return Error ? null : processor;
        }

        public StringBuilder GetStats()
        {
            StringBuilder sb = new StringBuilder();
            const string cannotcalc = "cannot be calculated";

            const string section = "==============================================================================";
            if (SelectedJobs.Count > 0)
            {
                // Job Stats
                sb.AppendLine(section);

                foreach (IJob theJob in SelectedJobs)
                {

                    sb.AppendLine(" START OF JOB REPORT");
                    sb.AppendLine(section);
                    sb.AppendLine("");
                    sb.AppendLine("Job Name            : " + theJob.Name);
                    sb.AppendLine("Job ID              : " + theJob.Id);
                    sb.AppendLine("Job State           : " + theJob.State);
                    sb.AppendLine("Job Priority        : " + theJob.Priority);
                    sb.AppendLine("Job Template Id     : " + theJob.TemplateId);
                    sb.AppendLine("Job Created (UTC)   : " + theJob.Created.ToLongDateString() + " " + theJob.Created.ToLongTimeString());
                    if (theJob.StartTime != null)
                        sb.AppendLine("Job StartTime (UTC) : " + theJob.StartTime.Value.ToLongDateString() + " " + theJob.StartTime.Value.ToLongTimeString());
                    if (theJob.EndTime != null)
                        sb.AppendLine("Job EndTime (UTC)   : " + theJob.EndTime.Value.ToLongDateString() + " " + theJob.EndTime.Value.ToLongTimeString());
                    TimeSpan ts = theJob.RunningDuration;
                    string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}:{3:00}.{4:00}", ts.Days, ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                    sb.AppendLine("Job CPU runtime     : " + elapsedTime);

                    if ((theJob.StartTime != null) && (theJob.EndTime != null))
                    {
                        ts = ((DateTime)theJob.EndTime).Subtract((DateTime)theJob.StartTime);
                        elapsedTime = String.Format("{0:00}:{1:00}:{2:00}:{3:00}.{4:00}", ts.Days, ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                        sb.AppendLine("Job Duration        : " + elapsedTime);
                    }
                    sb.AppendLine("Number of tasks     : " + theJob.Tasks.Count);
                    sb.AppendLine("Media Account       : " + _accountname);
                    sb.AppendLine("");
                    sb.AppendLine(section);
                    foreach (ITask task in theJob.Tasks)
                    {
                        sb.AppendLine("Task Name           : " + task.Name);
                        sb.AppendLine(section);
                        sb.AppendLine("");
                        sb.AppendLine("Task ID             : " + task.Id);
                        sb.AppendLine("Task Priority       : " + task.Priority);
                        sb.AppendLine("Task State          : " + task.State);
                        sb.AppendLine("Task Options        : " + task.Options);

                        sb.AppendLine("Media Processor     : " + task.MediaProcessorId);
                        IMediaProcessor processor = JobInfo.GetMediaProcessorFromId(task.MediaProcessorId, (CloudMediaContext)theJob.GetMediaContext());
                        if (processor != null)
                            sb.AppendLine("Media Processor Name: " + processor.Name);

                        if (task.StartTime != null) // If not in queued state
                            sb.AppendLine("Task StartTime (UTC): " + task.StartTime.Value.ToLongDateString() + " " + task.StartTime.Value.ToLongTimeString());
                        if (task.EndTime != null) // If not completed yet
                            sb.AppendLine("Task EndTime (UTC)  : " + task.EndTime.Value.ToLongDateString() + " " + task.EndTime.Value.ToLongTimeString());
                        ts = task.RunningDuration;
                        elapsedTime = String.Format("{0:00}:{1:00}:{2:00}:{3:00}.{4:00}", ts.Days, ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                        sb.AppendLine("Task CPU runtime    : " + elapsedTime);

                        if ((task.StartTime != null) && (task.EndTime != null))
                        {
                            ts = ((DateTime)task.EndTime).Subtract((DateTime)task.StartTime);
                            elapsedTime = String.Format("{0:00}:{1:00}:{2:00}:{3:00}.{4:00}", ts.Days, ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                            sb.AppendLine("Task Duration       : " + elapsedTime);
                        }

                        sb.AppendLine("Task PerfMessage    : " + task.PerfMessage);
                        sb.AppendLine("Task Progress       : " + task.Progress);
                        // Task historical event?
                        foreach (TaskHistoricalEvent thEvent in task.HistoricalEvents)
                        {
                            sb.AppendLine(thEvent.TimeStamp.ToLongTimeString() + " :-  " + thEvent.Message);
                        }

                        sb.AppendLine("");

                        sb.AppendLine("Task Body           : ");
                        sb.AppendLine("=====================");
                        sb.AppendLine(task.TaskBody);
                        sb.AppendLine("");

                        sb.AppendLine("Task Configuration  : ");
                        sb.AppendLine("=====================");
                        if (task.Options == TaskOptions.None)
                        {
                            sb.AppendLine(task.Configuration);
                        }
                        else
                        {
                            sb.AppendLine("(Not displayed here as task configuration is protected. This data is visible in Job Information / Tasks)");
                        }
                        sb.AppendLine("");

                        sb.AppendLine("Input Assets        :");
                        sb.AppendLine("=====================");
                        sb.AppendLine("");

                        try
                        {
                            foreach (IAsset asset in task.InputAssets)
                            {
                                if (asset.State == AssetState.Deleted)
                                {
                                    sb.AppendLine("Asset Deleted");
                                }
                                else
                                {
                                    ListAssetInfo(asset, ref sb);
                                    sb.AppendLine("");
                                    ListFilesInAsset(asset, ref sb);
                                }
                            }
                        }
                        catch
                        {
                            sb.AppendLine("Asset(s) error. Deleted?");
                        }

                        sb.AppendLine("");
                        sb.AppendLine("Output Assets       :");
                        sb.AppendLine("=====================");
                        sb.AppendLine("");

                        try
                        {
                            foreach (IAsset asset in task.OutputAssets)
                            {
                                if (asset.State == AssetState.Deleted)
                                {
                                    sb.AppendLine("Asset Deleted");
                                }
                                else
                                {
                                    ListAssetInfo(asset, ref sb);
                                    sb.AppendLine("");
                                    ListFilesInAsset(asset, ref sb);
                                }
                            }
                        }
                        catch
                        {
                            sb.AppendLine("Asset(s) error. Deleted?");
                        }

                        sb.AppendLine("");

                        if (task.State == JobState.Error)
                        {
                            foreach (var errordetail in task.ErrorDetails)
                            {
                                sb.AppendLine("Error Message : " + errordetail.Message);
                                sb.AppendLine("Error Code    : " + errordetail.Code);
                            }
                        }

                        if (task.State == JobState.Finished)
                        {
                            TaskSizeAndPrice MyTaskSizePrice = CalculateTaskSizeAndPrice(task, (CloudMediaContext)theJob.GetMediaContext());

                            if (theJob.Tasks.Count > 1) // only display for the task if there are several tasks
                            {
                                sb.AppendLine("Input size processed by the task  : " + ((MyTaskSizePrice.InputSize != -1) ? AssetInfo.FormatByteSize(MyTaskSizePrice.InputSize) : cannotcalc));
                                sb.AppendLine("Output size processed by the task : " + ((MyTaskSizePrice.OutputSize != -1) ? AssetInfo.FormatByteSize(MyTaskSizePrice.OutputSize) : cannotcalc));
                                //sb.AppendLine("Total size processed by the task  : " + ((MyTaskSizePrice.InputSize != -1 && MyTaskSizePrice.OutputSize != -1) ? AssetInfo.FormatByteSize(MyTaskSizePrice.InputSize + MyTaskSizePrice.OutputSize) : cannotcalc));

                                if (MyTaskSizePrice.Price >= 0)
                                {
                                    sb.AppendLine(string.Format("Estimated cost of the task        : {0} {1:0.00}", Properties.Settings.Default.Currency, MyTaskSizePrice.Price));
                                }
                            }
                        }

                        sb.AppendLine("");
                        sb.AppendLine(section);
                    }
                    sb.AppendLine("");

                    TaskSizeAndPrice MyJobSizePrice = CalculateJobSizeAndPrice(theJob);

                    sb.AppendLine("Input size processed by the job  : " + ((MyJobSizePrice.InputSize != -1) ? AssetInfo.FormatByteSize(MyJobSizePrice.InputSize) : cannotcalc));
                    sb.AppendLine("Output size processed by the job : " + ((MyJobSizePrice.OutputSize != -1) ? AssetInfo.FormatByteSize(MyJobSizePrice.OutputSize) : cannotcalc));
                    sb.AppendLine("Total size processed by the job  : " + ((MyJobSizePrice.InputSize != -1 && MyJobSizePrice.OutputSize != -1) ? AssetInfo.FormatByteSize(MyJobSizePrice.InputSize + MyJobSizePrice.OutputSize) : cannotcalc));

                    /*
                    if (MyJobSizePrice.Price != -1)
                    {
                        sb.AppendLine(string.Format("Estimated cost of the job        : {0} {1:0.00}", Properties.Settings.Default.Currency, MyJobSizePrice.Price));
                    }
                    */

                    sb.AppendLine("");
                    sb.AppendLine(section);
                    sb.AppendLine(" END OF JOB REPORT");
                    sb.AppendLine(section);
                    sb.AppendLine("");
                }
            }
            return sb;
        }
    }

    public class AssetBitmapAndText
    {
        public Bitmap bitmap;
        public string MouseOverDesc;
    }


    public class AssetInfo
    {
        private List<IAsset> SelectedAssets;
        public const string Type_Empty = "(empty)";
        public const string Type_Workflow = "Workflow";
        public const string Type_Single = "Single Bitrate MP4";
        public const string Type_Multi = "Multi Bitrate MP4";
        public const string Type_Smooth = "Smooth Streaming";
        public const string Type_LiveArchive = "Live Archive";
        public const string Type_Fragmented = "Pre-fragmented";
        public const string Type_AMSHLS = "Media Services HLS";
        public const string Type_Thumbnails = "Thumbnails";
        public const string _prog_down_https_SAS = "Progressive Download URLs (SAS)";
        public const string _prog_down_http_streaming = "Progressive Download URLs (SE)";
        public const string _hls_v4 = "HLS v4  URL";
        public const string _hls_v3 = "HLS v3  URL";
        public const string _dash = "MPEG-DASH URL";
        public const string _smooth = "Smooth Streaming URL";
        public const string _smooth_legacy = "Smooth Streaming (legacy) URL";
        public const string _hls = "HLS URL";

        private const string format_smooth_legacy = "fmp4-v20";
        private const string format_hls_v4 = "m3u8-aapl";
        private const string format_hls_v3 = "m3u8-aapl-v3";
        private const string format_dash = "mpd-time-csf";
        private const string format_hds = "f4m-f4f";

        private const string format_url = "format={0}";
        private const string filter_url = "filter={0}";
        private const string audioTrack_url = "audioTrack={0}";

        private const string ManifestFileExtension = ".ism";

        public AssetInfo(List<IAsset> mySelectedAssets)
        {
            SelectedAssets = mySelectedAssets;
        }
        public AssetInfo(IAsset asset)
        {
            SelectedAssets = new List<IAsset>();
            SelectedAssets.Add(asset);
        }


        public IEnumerable<Uri> GetValidURIs()
        {
            var _context = SelectedAssets.FirstOrDefault().GetMediaContext();
            IEnumerable<Uri> ValidURIs;
            IAsset asset = SelectedAssets.FirstOrDefault();
            var ismFile = asset.AssetFiles.AsEnumerable().Where(f => f.Name.EndsWith(".ism")).OrderByDescending(f => f.IsPrimary).FirstOrDefault();
            if (ismFile != null)
            {
                var locators = asset.Locators.Where(l => l.Type == LocatorType.OnDemandOrigin && l.ExpirationDateTime > DateTime.UtcNow).OrderByDescending(l => l.ExpirationDateTime);

                var template = new UriTemplate("{contentAccessComponent}/{ismFileName}/manifest");
                ValidURIs = locators.SelectMany(l =>
                    _context
                        .StreamingEndpoints
                        .AsEnumerable()
                          .Where(o => (o.State == StreamingEndpointState.Running) && (StreamingEndpointInformation.CanDoDynPackaging(o)))
                          .OrderByDescending(o => o.CdnEnabled)
                        .Select(
                            o =>
                                template.BindByPosition(new Uri("http://" + o.HostName), l.ContentAccessComponent,
                                    ismFile.Name)))
                    .ToArray();

                return ValidURIs;
            }
            else
            {
                return null;
            }
        }

        public static IEnumerable<Uri> GetURIs(IAsset asset)
        {
            var _context = asset.GetMediaContext();
            IEnumerable<Uri> ValidURIs;
            var ismFile = asset.AssetFiles.AsEnumerable().Where(f => f.Name.EndsWith(".ism")).OrderByDescending(f => f.IsPrimary).FirstOrDefault();
            if (ismFile != null)
            {
                var locators = asset.Locators.Where(l => l.Type == LocatorType.OnDemandOrigin && l.ExpirationDateTime > DateTime.UtcNow).OrderByDescending(l => l.ExpirationDateTime);

                var template = new UriTemplate("{contentAccessComponent}/{ismFileName}/manifest");
                ValidURIs = locators.SelectMany(l =>
                    _context
                        .StreamingEndpoints
                        .AsEnumerable()
                          .OrderByDescending(o => o.CdnEnabled)
                        .Select(
                            o =>
                                template.BindByPosition(new Uri("http://" + o.HostName), l.ContentAccessComponent,
                                    ismFile.Name)))
                    .ToArray();

                return ValidURIs;
            }
            else
            {
                return null;
            }
        }


        public static ILocator CreatedTemporaryOnDemandLocator(IAsset asset)
        {
            ILocator tempLocator = null;

            try
            {
                var locatorTask = Task.Factory.StartNew(() =>
                {
                    try
                    {
                        tempLocator = asset.GetMediaContext().Locators.Create(LocatorType.OnDemandOrigin, asset, AccessPermissions.Read, TimeSpan.FromHours(1));

                    }
                    catch
                    {
                        throw;
                    }
                });
                locatorTask.Wait();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return tempLocator;
        }

        public static Uri GetValidOnDemandURI(IAsset asset)
        {
            var aivalidurls = new AssetInfo(asset).GetValidURIs();
            if (aivalidurls != null)
            {
                return aivalidurls.FirstOrDefault();
            }
            else
            {
                return null;
            }
        }


        public static IEnumerable<Uri> GetSmoothStreamingUris(ILocator originLocator, IStreamingEndpoint se = null, string filter = null, bool https = false, string customhostname = null, string audiotrack = null)
        {
            return GetStreamingUris(originLocator, se, filter, https, customhostname, AMSOutputProtocols.NotSpecified, audiotrack);
        }

        public static IEnumerable<Uri> GetSmoothStreamingLegacyUris(ILocator originLocator, IStreamingEndpoint se = null, string filter = null, bool https = false, string customhostname = null, string audiotrack = null)
        {
            return GetStreamingUris(originLocator, se, filter, https, customhostname, AMSOutputProtocols.SmoothLegacy, audiotrack);
        }

        /// <summary>
        /// Returns the HLS version 4 URL of the <paramref name="originLocator"/>; otherwise, null.
        /// </summary>
        /// <param name="originLocator">The <see cref="ILocator"/> instance.</param>
        /// <returns>A <see cref="System.Uri"/> representing the HLS version 4 URL of the <paramref name="originLocator"/>; otherwise, null.</returns>
        public static IEnumerable<Uri> GetHlsUris(ILocator originLocator, IStreamingEndpoint se = null, string filter = null, bool https = false, string customhostname = null, string audiotrack = null)
        {
            return GetStreamingUris(originLocator, se, filter, https, customhostname, AMSOutputProtocols.HLSv4, audiotrack);
        }

        /// <summary>
        /// Returns the HLS version 3 URL of the <paramref name="originLocator"/>; otherwise, null.
        /// </summary>
        /// <param name="originLocator">The <see cref="ILocator"/> instance.</param>
        /// <returns>A <see cref="System.Uri"/> representing the HLS version 3 URL of the <paramref name="originLocator"/>; otherwise, null.</returns>
        public static IEnumerable<Uri> GetHlsv3Uris(ILocator originLocator, IStreamingEndpoint se = null, string filter = null, bool https = false, string customhostname = null, string audiotrack = null)
        {
            return GetStreamingUris(originLocator, se, filter, https, customhostname, AMSOutputProtocols.HLSv3, audiotrack);
        }

        /// <summary>
        /// Returns the MPEG-DASH URL of the <paramref name="originLocator"/>; otherwise, null.
        /// </summary>
        /// <param name="originLocator">The <see cref="ILocator"/> instance.</param>
        /// <returns>A <see cref="System.Uri"/> representing the MPEG-DASH URL of the <paramref name="originLocator"/>; otherwise, null.</returns>
        public static IEnumerable<Uri> GetMpegDashUris(ILocator originLocator, IStreamingEndpoint se = null, string filter = null, bool https = false, string customhostname = null, string audiotrack = null)
        {
            return GetStreamingUris(originLocator, se, filter, https, customhostname, AMSOutputProtocols.Dash, audiotrack);
        }


        public static IEnumerable<IAssetFile> GetManifestAssetFiles(IAsset asset)
        {
            if (asset == null)
            {
                throw new ArgumentNullException("asset", "The asset cannot be null.");
            }


            return asset
                .AssetFiles
                .ToList()
                .Where(af => af.Name.EndsWith(ILocatorExtensions.ManifestFileExtension, StringComparison.OrdinalIgnoreCase))
                  .ToList();
        }

        private static IEnumerable<Uri> GetStreamingUris(ILocator originLocator, IStreamingEndpoint se = null, string filter = null, bool https = false, string customhostname = null, AMSOutputProtocols outputprotocol = AMSOutputProtocols.NotSpecified, string audiotrack = null)
        {
            const string BaseStreamingUrlTemplate = "{0}/{1}/manifest";

            if (originLocator == null)
            {
                throw new ArgumentNullException("locator", "The locator cannot be null.");
            }

            if (originLocator.Type != LocatorType.OnDemandOrigin)
            {
                throw new ArgumentException("The locator type must be on-demand origin.", "originLocator");
            }

            IEnumerable<Uri> smoothStreamingUri = null;
            IAsset asset = originLocator.Asset;
            IEnumerable<IAssetFile> manifestAssetFiles = GetManifestAssetFiles(asset);
            if (manifestAssetFiles != null)
            {
                smoothStreamingUri = manifestAssetFiles.Select(f => new Uri(
                        string.Format(
                            CultureInfo.InvariantCulture,
                            BaseStreamingUrlTemplate,
                            originLocator.Path.TrimEnd('/'),
                            f.Name
                            ),
                        UriKind.Absolute));
            }

            if (se != null)
            {
                string hostname = customhostname == null ? se.HostName : customhostname;
                smoothStreamingUri = smoothStreamingUri.Select(u => new UriBuilder()
                {
                    Host = hostname,
                    Scheme = https ? "https://" : "http://",
                    Path = AssetInfo.AddAudioTrackToUrlString(AssetInfo.AddProtocolFormatInUrlString(AssetInfo.AddFilterToUrlString(u.AbsolutePath, filter), outputprotocol), audiotrack)
                }.Uri);
            }

            return smoothStreamingUri;
        }



        public static string GetSmoothLegacy(string smooth_uri)
        {
            return string.Format("{0}(format={1})", smooth_uri, format_smooth_legacy);
        }
        public static string AddFilterToUrlString(string urlstr, string filter)
        {
            // add a filter
            if (filter != null)
            {
                return AddParameterToUrlString(urlstr, string.Format(AssetInfo.filter_url, filter));
            }
            else
            {
                return urlstr;
            }
        }

        public static string AddAudioTrackToUrlString(string urlstr, string trackname)
        {
            // add a track name
            if (trackname != null)
            {
                return AddParameterToUrlString(urlstr, string.Format(AssetInfo.audioTrack_url, trackname));
            }
            else
            {
                return urlstr;
            }
        }

        public static string AddHLSNoAudioOnlyModeToUrlString(string urlstr)
        {
            return AddParameterToUrlString(urlstr, "audio-only=false");
        }

        public static string AddProtocolFormatInUrlString(string urlstr, AMSOutputProtocols protocol = AMSOutputProtocols.NotSpecified)
        {
            switch (protocol)
            {
                case AMSOutputProtocols.Dash:
                    return AddParameterToUrlString(urlstr, string.Format(AssetInfo.format_url, AssetInfo.format_dash));

                case AMSOutputProtocols.HDS:
                    return AddParameterToUrlString(urlstr, string.Format(AssetInfo.format_url, AssetInfo.format_hds));

                case AMSOutputProtocols.HLSv3:
                    return AddParameterToUrlString(urlstr, string.Format(AssetInfo.format_url, AssetInfo.format_hls_v3));

                case AMSOutputProtocols.HLSv4:
                    return AddParameterToUrlString(urlstr, string.Format(AssetInfo.format_url, AssetInfo.format_hls_v4));

                case AMSOutputProtocols.SmoothLegacy:
                    return AddParameterToUrlString(urlstr, string.Format(AssetInfo.format_url, AssetInfo.format_smooth_legacy));

                case AMSOutputProtocols.NotSpecified:
                default:
                    return urlstr;
            }
        }

        public static string AddParameterToUrlString(string urlstr, string parameter)
        {
            // add a parameter (like "format=mpd-time-csf" or "filter=myfilter" or "audioTrack=name to urlstr

            const string querystr = "/manifest(";

            if (urlstr.Contains(querystr)) // there is already a parameter
            {
                int pos = urlstr.IndexOf(querystr, 0);
                urlstr = urlstr.Substring(0, pos + 10) + parameter + "," + urlstr.Substring(pos + 10);
            }
            else
            {
                urlstr += string.Format("({0})", parameter);
            }

            return urlstr;
        }

        // return the URL with hostname from streaming endpoint
        public static Uri RW(Uri url, IStreamingEndpoint se, string filters = null, bool https = false, string customHostName = null, AMSOutputProtocols protocol = AMSOutputProtocols.NotSpecified, string audiotrackname = null, bool HLSNoAudioOnly = false)
        {
            if (url != null)
            {
                string hostname = se.HostName;

                string path = AddFilterToUrlString(url.AbsolutePath, filters);
                path = AddProtocolFormatInUrlString(path, protocol);

                if (protocol == AMSOutputProtocols.HLSv3)
                {
                    path = AddAudioTrackToUrlString(path, audiotrackname);
                    if (HLSNoAudioOnly)
                    {
                        path = AddHLSNoAudioOnlyModeToUrlString(path);
                    }
                }

                UriBuilder urib = new UriBuilder()
                {
                    Host = customHostName == null ? hostname : customHostName,
                    Scheme = https ? "https://" : "http://",
                    Path = path,
                };
                return urib.Uri;
            }
            else return null;
        }



        public static string RW(string path, IStreamingEndpoint se, string filter = null, bool https = false, string customhostname = null, AMSOutputProtocols protocol = AMSOutputProtocols.NotSpecified, string audiotrackname = null)
        {
            return RW(new Uri(path), se, filter, https, customhostname, protocol, audiotrackname).AbsoluteUri;
        }

        public static long GetSize(IAsset asset)
        {
            long size = 0;
            foreach (IAssetFile objFile in asset.AssetFiles)
            { size += objFile.ContentFileSize; }
            return size;
        }

        public static string GetDynamicEncryptionType(IAsset asset)
        {
            if (asset.DeliveryPolicies.Count > 0)
            {
                string str = string.Empty;
                switch (asset.DeliveryPolicies.FirstOrDefault().AssetDeliveryPolicyType)
                {
                    case AssetDeliveryPolicyType.Blocked:
                        str = "Blocked";
                        break;
                    case AssetDeliveryPolicyType.DynamicCommonEncryption:
                        str = "CENC";
                        break;
                    case AssetDeliveryPolicyType.DynamicEnvelopeEncryption:
                        str = "AES";
                        break;
                    case AssetDeliveryPolicyType.NoDynamicEncryption:
                        str = "No";
                        break;
                    case AssetDeliveryPolicyType.None:
                    default:
                        str = string.Empty;
                        break;

                }
                return str;
            }
            else
            {
                return string.Empty;
            }
        }


        public PublishStatus GetPublishedStatus(LocatorType LocType)
        {
            PublishStatus LocPubStatus;

            // if there is one locato for this type
            if ((SelectedAssets.FirstOrDefault().Locators.Where(l => l.Type == LocType).Count() > 0))
            {
                if (!SelectedAssets.FirstOrDefault().Locators.Where(l => (l.Type == LocType)).All(l => (l.ExpirationDateTime < DateTime.UtcNow)))
                {// not all int the past
                    var query = SelectedAssets.FirstOrDefault().Locators.Where(l => ((l.Type == LocType) && (l.ExpirationDateTime > DateTime.UtcNow) && (l.StartTime != null)));
                    // if no locator are valid today but at least one will in the future
                    if (query.ToList().Count() > 0)
                    {
                        LocPubStatus = (query.All(l => (l.StartTime > DateTime.UtcNow))) ? PublishStatus.PublishedFuture : PublishStatus.PublishedActive;
                    }
                    else
                    {
                        LocPubStatus = PublishStatus.PublishedActive;
                    }
                }
                else      // if all locators are in the past
                {
                    LocPubStatus = PublishStatus.PublishedExpired;
                }
            }
            else
            {
                LocPubStatus = PublishStatus.NotPublished;
            }
            return LocPubStatus;
        }

        public static PublishStatus GetPublishedStatusForLocator(ILocator Locator)
        {
            PublishStatus LocPubStatus;
            if (!(Locator.ExpirationDateTime < DateTime.UtcNow))
            {// not in the past
             // if  locator is not valid today but will be in the future
                if (Locator.StartTime != null)
                {
                    LocPubStatus = (Locator.StartTime > DateTime.UtcNow) ? PublishStatus.PublishedFuture : PublishStatus.PublishedActive;
                }
                else
                {
                    LocPubStatus = PublishStatus.PublishedActive;
                }
            }
            else      // if locator is in the past
            {
                LocPubStatus = PublishStatus.PublishedExpired;
            }
            return LocPubStatus;
        }

        static public TimeSpan ReturnTimeSpanOnGOP(ManifestTimingData data, TimeSpan ts)
        {
            var response = ts;
            ulong timestamp = (ulong)(ts.TotalSeconds * data.TimeScale);

            int i = 0;
            foreach (var t in data.TimestampList)
            {
                if (t < timestamp && i < (data.TimestampList.Count - 1) && timestamp < data.TimestampList[i + 1])
                {
                    response = TimeSpan.FromSeconds((double)t / (double)data.TimeScale);
                    break;
                }
                i++;
            }
            return response;
        }


        static public ManifestTimingData GetManifestTimingData(IAsset asset)
        // Parse the manifest and get data from it
        {
            ManifestTimingData response = new ManifestTimingData() { IsLive = false, Error = false, TimestampOffset = 0, TimestampList = new List<ulong>(), DiscontinuityDetected = false };

            try
            {
                ILocator mytemplocator = null;
                Uri myuri = GetValidOnDemandURI(asset);

                if (myuri == null)
                {
                    mytemplocator = CreatedTemporaryOnDemandLocator(asset);
                    myuri = GetValidOnDemandURI(asset);
                }
                if (myuri != null)
                {
                    XDocument manifest = XDocument.Load(myuri.ToString());
                    var smoothmedia = manifest.Element("SmoothStreamingMedia");
                    var videotrack = smoothmedia.Elements("StreamIndex").Where(a => a.Attribute("Type").Value == "video");

                    // TIMESCALE
                    string timescalefrommanifest = smoothmedia.Attribute("TimeScale").Value;
                    if (videotrack.FirstOrDefault().Attribute("TimeScale") != null) // there is timescale value in the video track. Let's take this one.
                    {
                        timescalefrommanifest = videotrack.FirstOrDefault().Attribute("TimeScale").Value;
                    }
                    ulong timescale = ulong.Parse(timescalefrommanifest);
                    response.TimeScale = (timescale == TimeSpan.TicksPerSecond) ? null : (ulong?)timescale; // if 10000000 then null (default)

                    // Timestamp offset
                    if (videotrack.FirstOrDefault().Element("c").Attribute("t") != null)
                    {
                        response.TimestampOffset = ulong.Parse(videotrack.FirstOrDefault().Element("c").Attribute("t").Value);
                    }
                    else
                    {
                        response.TimestampOffset = 0; // no timestamp, so it should be 0
                    }

                    ulong totalduration = 0;
                    ulong durationpreviouschunk = 0;
                    ulong durationchunk;
                    int repeatchunk;
                    foreach (var chunk in videotrack.Elements("c"))
                    {
                        durationchunk = chunk.Attribute("d") != null ? ulong.Parse(chunk.Attribute("d").Value) : 0;
                        repeatchunk = chunk.Attribute("r") != null ? int.Parse(chunk.Attribute("r").Value) : 1;
                        totalduration += durationchunk * (ulong)repeatchunk;

                        if (chunk.Attribute("t") != null)
                        {
                            //totalduration = ulong.Parse(chunk.Attribute("t").Value) - response.TimestampOffset; // new timestamp, perhaps gap in live stream....
                            ulong tvalue = ulong.Parse(chunk.Attribute("t").Value);
                            response.TimestampList.Add(tvalue);
                            if (tvalue != response.TimestampOffset)
                            {
                                totalduration = tvalue - response.TimestampOffset; // Discountinuity ? We calculate the duration from the offset
                                response.DiscontinuityDetected = true; // let's flag it
                            }
                        }
                        else
                        {
                            response.TimestampList.Add(response.TimestampList[response.TimestampList.Count() - 1] + durationpreviouschunk);
                        }

                        for (int i = 1; i < repeatchunk; i++)
                        {
                            response.TimestampList.Add(response.TimestampList[response.TimestampList.Count() - 1] + durationchunk);
                        }

                        durationpreviouschunk = durationchunk;

                    }
                    response.TimestampEndLastChunk = response.TimestampList[response.TimestampList.Count() - 1] + durationpreviouschunk;

                    if (smoothmedia.Attribute("IsLive") != null && smoothmedia.Attribute("IsLive").Value == "TRUE")
                    { // Live asset.... No duration to read (but we can read scaling and compute duration if no gap)
                        response.IsLive = true;
                        response.AssetDuration = TimeSpan.FromSeconds((double)totalduration / ((double)timescale));
                    }
                    else
                    {
                        //totalduration = ulong.Parse(smoothmedia.Attribute("Duration").Value);
                        response.AssetDuration = TimeSpan.FromSeconds((double)totalduration / ((double)timescale));
                    }
                }
                else
                {
                    response.Error = true;
                }
                if (mytemplocator != null) mytemplocator.Delete();
            }
            catch (Exception ex)
            {
                response.Error = true;
            }
            return response;
        }


        public class ManifestSegmentData
        {
            public ulong timestamp;
            public bool calculated; // it means the timesatmp has been calculated from previous
            public bool timestamp_mismatch; // if there is a mismatch
        }

        public class ManifestSegmentsResponse
        {
            public List<ManifestSegmentData> videoSegments;
            public List<int> videoBitrates;
            public string videoName;
            public ManifestSegmentData[][] audioSegments;
            public int[][] audioBitrates;
            public string[] audioName;

            public ManifestSegmentsResponse()
            {
                this.videoSegments = new List<ManifestSegmentData>();
                this.videoBitrates = new List<int>();
            }
        }

        static public ManifestSegmentsResponse GetManifestSegmentsList(IAsset asset)
        // Parse the manifest and get data from it
        {
            ManifestSegmentsResponse response = new ManifestSegmentsResponse();

            try
            {
                ILocator mytemplocator = null;
                Uri myuri = AssetInfo.GetValidOnDemandURI(asset);
                if (myuri == null)
                {
                    mytemplocator = AssetInfo.CreatedTemporaryOnDemandLocator(asset);
                    myuri = AssetInfo.GetValidOnDemandURI(asset);
                }
                if (myuri != null)
                {
                    XDocument manifest = XDocument.Load(myuri.ToString());
                    var smoothmedia = manifest.Element("SmoothStreamingMedia");

                    ulong d = 0, r;
                    bool calc = true;
                    bool mismatch = false;
                    bool firstchunk = true;
                    ulong timeStamp = 0;

                    // video track
                    var videotrack = smoothmedia.Elements("StreamIndex").Where(a => a.Attribute("Type").Value == "video").FirstOrDefault();
                    response.videoBitrates = videotrack.Elements("QualityLevel").Select(e => int.Parse(e.Attribute("Bitrate").Value)).ToList();
                    response.videoName = videotrack.Attribute("Name").Value;

                    foreach (var chunk in videotrack.Elements("c"))
                    {
                        mismatch = false;
                        if (chunk.Attribute("t") != null)
                        {
                            var readtimeStamp = ulong.Parse(chunk.Attribute("t").Value);
                            mismatch = (!firstchunk && readtimeStamp != timeStamp);
                            timeStamp = readtimeStamp;
                            calc = false;
                            firstchunk = false;
                        }
                        else
                        {
                            calc = true;
                        }

                        d = chunk.Attribute("d") != null ? ulong.Parse(chunk.Attribute("d").Value) : 0;
                        r = chunk.Attribute("r") != null ? ulong.Parse(chunk.Attribute("r").Value) : 1;
                        for (ulong i = 0; i < r; i++)
                        {
                            response.videoSegments.Add(new ManifestSegmentData()
                            {
                                timestamp = timeStamp,
                                timestamp_mismatch = (i == 0) ? mismatch : false,
                                calculated = (i == 0) ? calc : true
                            });
                            timeStamp += d;
                        }
                    }

                    // audio track
                    var audiotracks = smoothmedia.Elements("StreamIndex").Where(a => a.Attribute("Type").Value == "audio");
                    response.audioBitrates = new int[audiotracks.Count()][];
                    response.audioSegments = new ManifestSegmentData[audiotracks.Count()][];
                    response.audioName = new string[audiotracks.Count()];


                    int a_index = 0;
                    foreach (var audiotrack in audiotracks)
                    {
                        response.audioBitrates[a_index] = audiotrack.Elements("QualityLevel").Select(e => int.Parse(e.Attribute("Bitrate").Value)).ToArray();
                        response.audioName[a_index] = audiotrack.Attribute("Name").Value;

                        var audiotracksegmentdata = new List<ManifestSegmentData>();

                        timeStamp = 0;
                        d = 0;
                        firstchunk = true;
                        foreach (var chunk in audiotrack.Elements("c"))
                        {
                            mismatch = false;
                            if (chunk.Attribute("t") != null)
                            {
                                var readtimeStamp = ulong.Parse(chunk.Attribute("t").Value);
                                mismatch = (!firstchunk && readtimeStamp != timeStamp);
                                timeStamp = readtimeStamp;
                                calc = false;
                                firstchunk = false;
                            }
                            else
                            {
                                calc = true;
                            }

                            d = chunk.Attribute("d") != null ? ulong.Parse(chunk.Attribute("d").Value) : 0;
                            r = chunk.Attribute("r") != null ? ulong.Parse(chunk.Attribute("r").Value) : 1;
                            for (ulong i = 0; i < r; i++)
                            {
                                audiotracksegmentdata.Add(new ManifestSegmentData()
                                {
                                    timestamp = timeStamp,
                                    timestamp_mismatch = (i == 0) ? mismatch : false,
                                    calculated = (i == 0) ? calc : true
                                });
                                timeStamp += d;
                            }

                        }
                        response.audioSegments[a_index] = audiotracksegmentdata.ToArray();
                        a_index++;
                    }
                }
                else
                {
                    // Error
                }
                if (mytemplocator != null) mytemplocator.Delete();
            }
            catch
            {
                // Error
            }
            return response;
        }

        public static long ReturnTimestampInTicks(ulong timestamp, ulong? timescale)
        {
            double timescale2 = timescale ?? TimeSpan.TicksPerSecond;
            return (long)((double)timestamp * (double)TimeSpan.TicksPerSecond / timescale2);
        }

        public static IAsset GetAsset(string assetId, CloudMediaContext _context)
        {
            IAsset asset;

            try
            {
                // Use a LINQ Select query to get an asset.
                var assetInstance =
                    from a in _context.Assets
                    where a.Id == assetId
                    select a;
                // Reference the asset as an IAsset.
                asset = assetInstance.FirstOrDefault();
            }
            catch
            {

                asset = null;
            }

            return asset;
        }

        public static string GetAssetType(IAsset asset)
        {
            string type = asset.AssetType.ToString();
            int assetfilescount = asset.AssetFiles.Count();
            var AssetFiles = asset.AssetFiles.ToList();
            int number = assetfilescount;

            switch (asset.AssetType)
            {
                case AssetType.MediaServicesHLS:
                    type = Type_AMSHLS;
                    break;

                case AssetType.MP4:
                    break;

                case AssetType.MultiBitrateMP4:
                    var mp4files = AssetFiles.Where(f => f.Name.EndsWith(".mp4", StringComparison.OrdinalIgnoreCase)).ToArray();
                    number = mp4files.Count();
                    type = number == 1 ? Type_Single : Type_Multi;
                    break;

                case AssetType.SmoothStreaming:
                    type = Type_Smooth;
                    var cfffiles = AssetFiles.Where(f => f.Name.EndsWith(".ismv", StringComparison.OrdinalIgnoreCase) || f.Name.EndsWith(".isma", StringComparison.OrdinalIgnoreCase)).ToArray();
                    number = cfffiles.Count();

                    if (number == 0
                        && AssetFiles.Where(f => f.Name.EndsWith(".ism", StringComparison.OrdinalIgnoreCase) || f.Name.EndsWith(".ismc", StringComparison.OrdinalIgnoreCase)).Count() == 2
                        )
                    {
                        var fragmentedFilesCount = AssetFiles.Where(f => f.AssetFileOptions == AssetFileOptions.Fragmented).Count();
                        if (fragmentedFilesCount == AssetFiles.Count - 2)
                        {
                            number = AssetFiles.Count - 2;  // tracks - 2 manifest files
                            type = Type_LiveArchive;
                        }
                        else if ((fragmentedFilesCount == AssetFiles.Count - 4)
                            &&
                            AssetFiles.Where(f => f.Name.EndsWith("_manifest.xml", StringComparison.OrdinalIgnoreCase) || f.Name.EndsWith("_metadata.xml", StringComparison.OrdinalIgnoreCase)).Count() == 2
                            )
                        {
                            number = AssetFiles.Count - 4;  // tracks - 4 manifest files
                            type = Type_Fragmented;
                        }
                    }
                    break;

                case AssetType.Unknown:
                    string ext;
                    string pr = string.Empty;

                    if (assetfilescount == 0) return Type_Empty;

                    if (assetfilescount == 1)
                    {
                        number = 1;
                        ext = Path.GetExtension(AssetFiles.FirstOrDefault().Name.ToUpper());
                        if (!string.IsNullOrEmpty(ext)) ext = ext.Substring(1);
                        switch (ext)
                        {
                            case "WORKFLOW":
                                type = Type_Workflow;
                                break;

                            default:
                                type = ext;
                                break;
                        }
                    }
                    else
                    { // multi files in asset
                        var ThumbnailsAssetFiles = AssetFiles.Where(f => f.Name.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) || f.Name.EndsWith(".png", StringComparison.OrdinalIgnoreCase) || f.Name.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase)).ToArray();
                        var XMLAssetFiles = AssetFiles.Where(f => f.Name.EndsWith(".xml", StringComparison.OrdinalIgnoreCase)).ToArray();
                        int nonThumbnailFilesCount = AssetFiles.Count - ThumbnailsAssetFiles.Count();

                        if ((ThumbnailsAssetFiles.Count() > 0) && ((nonThumbnailFilesCount == 0) || (XMLAssetFiles.Count() == 1)))
                        {
                            type = Type_Thumbnails;
                            number = ThumbnailsAssetFiles.Count();
                        }
                    }
                    break;

                default:
                    break;
            }
            return string.Format("{0} ({1})", type, number);
        }

        static public void SetISMFileAsPrimary(IAsset asset)
        {
            var ismAssetFiles = asset.AssetFiles.ToList().
                Where(f => f.Name.EndsWith(".ism", StringComparison.OrdinalIgnoreCase)).ToArray();

            if (ismAssetFiles.Count() != 0)
            {
                if (ismAssetFiles.Where(af => af.IsPrimary).ToList().Count == 0) // if there is a primary .ISM file
                {
                    try
                    {
                        ismAssetFiles.First().IsPrimary = true;
                        ismAssetFiles.First().Update();
                    }
                    catch
                    {
                        throw;
                    }
                }
            }
        }

        static public void SetAFileAsPrimary(IAsset asset)
        {
            var files = asset.AssetFiles.ToList();
            var ismAssetFiles = files.
                Where(f => f.Name.EndsWith(".ism", StringComparison.OrdinalIgnoreCase)).ToArray();

            var mp4AssetFiles = files.
            Where(f => f.Name.EndsWith(".mp4", StringComparison.OrdinalIgnoreCase)).ToArray();

            if (ismAssetFiles.Count() != 0)
            {
                if (ismAssetFiles.Where(af => af.IsPrimary).ToList().Count == 0) // if there is a primary .ISM file
                {
                    try
                    {
                        ismAssetFiles.First().IsPrimary = true;
                        ismAssetFiles.First().Update();
                    }
                    catch
                    {
                        throw;
                    }
                }
            }
            else if (mp4AssetFiles.Count() != 0)
            {
                if (mp4AssetFiles.Where(af => af.IsPrimary).ToList().Count == 0) // if there is a primary .ISM file
                {
                    try
                    {
                        mp4AssetFiles.First().IsPrimary = true;
                        mp4AssetFiles.First().Update();
                    }
                    catch
                    {
                        throw;
                    }
                }
            }
            else
            {
                if (files.Where(af => af.IsPrimary).ToList().Count == 0) // if there is a primary .ISM file
                {
                    try
                    {
                        files.First().IsPrimary = true;
                        files.First().Update();
                    }
                    catch
                    {
                        throw;
                    }
                }
            }
        }

        static public void SetFileAsPrimary(IAsset asset, string assetfilename)
        {
            var ismAssetFiles = asset.AssetFiles.ToList().
                Where(f => f.Name.Equals(assetfilename, StringComparison.OrdinalIgnoreCase)).ToArray();

            if (ismAssetFiles.Count() == 1)
            {
                try
                {
                    // let's remove primary attribute to another file if any
                    asset.AssetFiles.Where(af => af.IsPrimary).ToList().ForEach(af => { af.IsPrimary = false; af.Update(); });
                    ismAssetFiles.First().IsPrimary = true;
                    ismAssetFiles.First().Update();
                }
                catch
                {
                    throw;
                }
            }
        }


        public void CreateOutlookMail()
        {
            Exception exception = null;
            try
            {
                StringBuilder SB = GetStats();
                // Let's create the email with Outlook
                Outlook.Application outlookApp = new Outlook.Application();
                Outlook.MailItem mailItem = (Outlook.MailItem)outlookApp.CreateItem(Outlook.OlItemType.olMailItem);
                if (SelectedAssets.Count == 1)
                {
                    string title = "Report: Asset '{0}'";
                    mailItem.Subject = string.Format(title, SelectedAssets.FirstOrDefault().Name);
                }
                else
                {
                    mailItem.Subject = string.Format("Report: {0} Assets", SelectedAssets.Count());
                }
                mailItem.HTMLBody = "<FONT Face=\"Courier New\">";
                mailItem.HTMLBody += SB.Replace(" ", "&nbsp;").Replace(Environment.NewLine, "<br />").ToString();
                mailItem.Display(false);
            }
            catch (System.Runtime.InteropServices.COMException ce)
            {
                // 0x80040154 Class not registered
                // This happen if outlook is not installed
                if (ce.HResult == unchecked((int)0x80040154))
                {
                    MessageBox.Show("Please install Office Outlook to use this functionality.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                exception = ce;
            }
            catch (Exception e)
            {
                exception = e;
            }

            if (exception != null)
            {
                MessageBox.Show("Exception while trying to compose the email." + exception, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void CopyStatsToClipBoard()
        {
            StringBuilder SB = GetStats();
            Clipboard.SetText((string)SB.ToString());
        }

        private static long ListFilesInAsset(IAsset asset, ref StringBuilder builder)
        {
            // Display the files associated with each asset. 

            long assetSize = 0;
            foreach (IAssetFile fileItem in asset.AssetFiles)
            {
                if (fileItem.IsPrimary) builder.AppendLine("Primary");
                builder.AppendLine("Name: " + fileItem.Name);
                builder.AppendLine("Size: " + fileItem.ContentFileSize + " Bytes");
                assetSize += fileItem.ContentFileSize;
                builder.AppendLine("==============");
            }
            return assetSize;
        }

        public static String FormatByteSize(long? byteCountl)
        {
            if (byteCountl.HasValue == true)
            {
                long byteCount = (long)byteCountl;
                string[] suf = { "B", "KB", "MB", "GB", "TB", "PB", "EB" }; //Longs run out around EB
                if (byteCount == 0)
                    return "0 " + suf[0];
                long bytes = Math.Abs(byteCount);
                int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
                double num = Math.Round(bytes / Math.Pow(1024, place), 1);
                return (Math.Sign(byteCount) * num).ToString() + " " + suf[place];
            }
            else return null;
        }

        public static long? Inverse_FormatByteSize(string mystring)
        {
            var sizes = new List<unitSize> {
                  new unitSize() { unitn = "B", mult = (long)1 },
                  new unitSize(){ unitn = "KB", mult = (long)1024 },
                  new unitSize(){ unitn = "MB", mult = (long)1024*1024 },
                  new unitSize(){ unitn = "GB", mult = (long)1024*1024*1024 },
                  new unitSize(){ unitn = "TB", mult = (long)1024*1024*1024*1024 },
                  new unitSize(){ unitn = "PB", mult = (long)1024*1024*1024 *1024*1024 },
                  new unitSize(){ unitn = "EB", mult = (long)1024*1024*1024 *1024*1024*1024 }
                  };

            if (sizes.Any(s => mystring.EndsWith(" " + s.unitn)))
            {
                var val = mystring.Substring(0, mystring.Length - 2).Trim();
                try
                {
                    var valdouble = double.Parse(val);
                    var myunit = mystring.Substring(mystring.Length - 2, 2).Trim();
                    var mymult = sizes.Where(s => s.unitn == myunit).FirstOrDefault().mult;
                    return (long)(valdouble * mymult);
                }
                catch
                {
                    return null;
                }
            }
            return null;
        }

        public class unitSize
        {
            public string unitn { get; set; }
            public long mult { get; set; }
        }


        public static AssetProtectionType GetAssetProtection(IAsset MyAsset, CloudMediaContext _context)
        {
            AssetProtectionType type = AssetProtectionType.None;
            IAssetDeliveryPolicy policy = MyAsset.DeliveryPolicies.FirstOrDefault();

            if (policy != null)
            {
                switch (policy.AssetDeliveryPolicyType)
                {
                    case AssetDeliveryPolicyType.DynamicEnvelopeEncryption:
                        type = AssetProtectionType.AES;
                        break;

                    case AssetDeliveryPolicyType.DynamicCommonEncryption:
                        if (
                            policy.AssetDeliveryConfiguration.ContainsKey(AssetDeliveryPolicyConfigurationKey.PlayReadyLicenseAcquisitionUrl)
                            &&
                            (policy.AssetDeliveryConfiguration.ContainsKey(AssetDeliveryPolicyConfigurationKey.WidevineLicenseAcquisitionUrl) || policy.AssetDeliveryConfiguration.ContainsKey(AssetDeliveryPolicyConfigurationKey.WidevineBaseLicenseAcquisitionUrl))
                            )
                        {
                            type = AssetProtectionType.PlayReadyAndWidevine;
                        }
                        else if (policy.AssetDeliveryConfiguration.ContainsKey(AssetDeliveryPolicyConfigurationKey.WidevineLicenseAcquisitionUrl) || policy.AssetDeliveryConfiguration.ContainsKey(AssetDeliveryPolicyConfigurationKey.WidevineBaseLicenseAcquisitionUrl))
                        {
                            type = AssetProtectionType.Widevine;
                        }
                        else
                        {
                            type = AssetProtectionType.PlayReady;
                        }
                        break;

                    default:
                        break;
                }
            }
            else if (MyAsset.Options == AssetCreationOptions.CommonEncryptionProtected)
            {
                type = AssetProtectionType.PlayReady; // CENC Static protection
            }

            return type;
        }


        public StringBuilder GetStats()
        {
            StringBuilder sb = new StringBuilder();

            if (SelectedAssets.Count > 0)
            {
                // Asset Stats
                foreach (IAsset theAsset in SelectedAssets)
                {
                    sb.Append(GetStat(theAsset));
                }
            }
            return sb;
        }

        public static StringBuilder GetStat(IAsset MyAsset, IStreamingEndpoint SelectedSE = null)
        {
            StringBuilder sb = new StringBuilder();
            string MyAssetType = AssetInfo.GetAssetType(MyAsset);
            bool bfileinasset = (MyAsset.AssetFiles.Count() == 0) ? false : true;
            long size = -1;
            if (bfileinasset)
            {
                size = 0;
                foreach (IAssetFile file in MyAsset.AssetFiles)
                {
                    size += file.ContentFileSize;
                }
            }
            sb.AppendLine("Asset Name          : " + MyAsset.Name);
            sb.AppendLine("Asset Type          : " + MyAsset.AssetType);
            sb.AppendLine("Asset Id            : " + MyAsset.Id);
            sb.AppendLine("Alternate ID        : " + MyAsset.AlternateId);
            if (size != -1)
                sb.AppendLine("Size                : " + FormatByteSize(size));
            sb.AppendLine("State               : " + MyAsset.State);
            sb.AppendLine("Created (UTC)       : " + MyAsset.Created.ToLongDateString() + " " + MyAsset.Created.ToLongTimeString());
            sb.AppendLine("Last Modified (UTC) : " + MyAsset.LastModified.ToLongDateString() + " " + MyAsset.LastModified.ToLongTimeString());
            sb.AppendLine("Creations Options   : " + MyAsset.Options);

            if (MyAsset.State != AssetState.Deleted)
            {
                sb.AppendLine("IsStreamable        : " + MyAsset.IsStreamable);
                sb.AppendLine("SupportsDynEnc      : " + MyAsset.SupportsDynamicEncryption);
                sb.AppendLine("Uri                 : " + MyAsset.Uri.AbsoluteUri);
                sb.AppendLine("");
                sb.AppendLine("Storage Name        : " + MyAsset.StorageAccountName);
                sb.AppendLine("Storage Bytes used  : " + FormatByteSize(MyAsset.StorageAccount.BytesUsed));
                sb.AppendLine("Storage IsDefault   : " + MyAsset.StorageAccount.IsDefault);
                sb.AppendLine("");

                foreach (IAsset p_asset in MyAsset.ParentAssets)
                {
                    sb.AppendLine("Parent asset Name   : " + p_asset.Name);
                    sb.AppendLine("Parent asset Id     : " + p_asset.Id);
                }
                sb.AppendLine("");
                foreach (IContentKey key in MyAsset.ContentKeys)
                {
                    sb.AppendLine("Content key         : " + key.Name);
                    sb.AppendLine("Content key Id      : " + key.Id);
                    sb.AppendLine("Content key Type    : " + key.ContentKeyType);
                }
                sb.AppendLine("");
                foreach (var pol in MyAsset.DeliveryPolicies)
                {
                    sb.AppendLine("Deliv policy Name   : " + pol.Name);
                    sb.AppendLine("Deliv policy Id     : " + pol.Id);
                    sb.AppendLine("Deliv policy Type   : " + pol.AssetDeliveryPolicyType);
                    sb.AppendLine("Deliv pol Protocol  : " + pol.AssetDeliveryProtocol);
                }
                sb.AppendLine("");

                foreach (IAssetFile fileItem in MyAsset.AssetFiles)
                {
                    if (fileItem.IsPrimary)
                    {
                        sb.AppendLine("   ------------(-P-R-I-M-A-R-Y-)------------------");
                    }
                    else
                    {
                        sb.AppendLine("   -----------------------------------------------");
                    }
                    sb.AppendLine("   Name                 : " + fileItem.Name);
                    sb.AppendLine("   Id                   : " + fileItem.Id);
                    sb.AppendLine("   File size            : " + fileItem.ContentFileSize + " Bytes");
                    sb.AppendLine("   Mime type            : " + fileItem.MimeType);
                    sb.AppendLine("   Init vector          : " + fileItem.InitializationVector);
                    sb.AppendLine("   Created (UTC)        : " + fileItem.Created.ToString("G"));
                    sb.AppendLine("   Last modified (UTC)  : " + fileItem.LastModified.ToString("G"));
                    sb.AppendLine("   Encrypted            : " + fileItem.IsEncrypted);
                    sb.AppendLine("   EncryptionScheme     : " + fileItem.EncryptionScheme);
                    sb.AppendLine("   EncryptionVersion    : " + fileItem.EncryptionVersion);
                    sb.AppendLine("   Encryption key id    : " + fileItem.EncryptionKeyId);
                    sb.AppendLine("   InitializationVector : " + fileItem.InitializationVector);
                    sb.AppendLine("   ParentAssetId        : " + fileItem.ParentAssetId);
                    sb.AppendLine("");
                }
                sb.Append(GetDescriptionLocators(MyAsset, SelectedSE));
            }
            sb.AppendLine("");
            sb.AppendLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            sb.AppendLine("");

            return sb;
        }

        public static StringBuilder GetDescriptionLocators(IAsset MyAsset, IStreamingEndpoint SelectedSE = null)
        {
            StringBuilder sb = new StringBuilder();

            foreach (ILocator locator in MyAsset.Locators)
            {
                sb.AppendLine("Locator Name      : " + locator.Name);
                sb.AppendLine("Locator Type      : " + locator.Type.ToString());
                sb.AppendLine("Locator Id        : " + locator.Id);
                if (locator.StartTime != null) sb.AppendLine("Start Time        : " + ((DateTime)locator.StartTime).ToLongDateString() + " " + ((DateTime)locator.StartTime).ToLongTimeString());
                if (locator.ExpirationDateTime != null) sb.AppendLine("Expiration Time   : " + ((DateTime)locator.ExpirationDateTime).ToLongDateString() + " " + ((DateTime)locator.ExpirationDateTime).ToLongTimeString());

                if (locator.Type == LocatorType.OnDemandOrigin)
                {
                    sb.AppendLine("Locator Path      : " + locator.Path);
                    sb.AppendLine("");
                    sb.AppendLine(_prog_down_http_streaming + " : ");
                    foreach (IAssetFile IAF in MyAsset.AssetFiles) sb.AppendLine((new Uri(locator.Path + IAF.Name)).AbsoluteUri);
                    sb.AppendLine("");

                    if (MyAsset.AssetType == AssetType.MediaServicesHLS) // It is a static HLS asset, so let's propose only the standard HLS V3 locator
                    {
                        sb.AppendLine(AssetInfo._hls + " : ");
                        sb.AppendLine(locator.GetHlsUri().AbsoluteUri);
                        sb.AppendLine("");
                    }
                    else if (MyAsset.AssetType == AssetType.SmoothStreaming || MyAsset.AssetType == AssetType.MultiBitrateMP4 || MyAsset.AssetType == AssetType.Unknown) //later to change Unknown to live archive
                    {
                        // It's not Static HLS
                        // Smooth or multi MP4
                        if (locator.GetSmoothStreamingUri() != null)
                        {
                            foreach (var uri in AssetInfo.GetSmoothStreamingUris(locator, SelectedSE))
                            {
                                sb.AppendLine(AssetInfo._smooth + " : ");
                                sb.AppendLine(uri.AbsoluteUri);
                            }

                            foreach (var uri in AssetInfo.GetSmoothStreamingLegacyUris(locator, SelectedSE))
                            {
                                sb.AppendLine(AssetInfo._smooth_legacy + " : ");
                                sb.AppendLine(uri.AbsoluteUri);
                            }
                        }

                        if (locator.GetMpegDashUri() != null)
                        {
                            foreach (var uri in AssetInfo.GetMpegDashUris(locator, SelectedSE))
                            {
                                sb.AppendLine(AssetInfo._dash + " : ");
                                sb.AppendLine(uri.AbsoluteUri);
                            }
                        }

                        if (locator.GetHlsUri() != null)
                        {
                            foreach (var uri in AssetInfo.GetHlsUris(locator, SelectedSE))
                            {
                                sb.AppendLine(AssetInfo._hls_v4 + " : ");
                                sb.AppendLine(uri.AbsoluteUri);
                            }
                            foreach (var uri in AssetInfo.GetHlsv3Uris(locator, SelectedSE))
                            {
                                sb.AppendLine(AssetInfo._hls_v3 + " : ");
                                sb.AppendLine(uri.AbsoluteUri);
                            }
                            sb.AppendLine("");
                        }
                    }
                }
                if (locator.Type == LocatorType.Sas)
                {
                    sb.AppendLine("Container Path    : " + locator.Path);
                    sb.AppendLine("");

                    List<Uri> ProgressiveDownloadUris;
                    IEnumerable<IAssetFile> MyAssetFiles;
                    sb.AppendLine(AssetInfo._prog_down_https_SAS + " : ");
                    MyAssetFiles = MyAsset.AssetFiles.ToList();
                    // Generate the Progressive Download URLs for each file. 
                    ProgressiveDownloadUris = MyAssetFiles.Select(af => af.GetSasUri(locator)).ToList();
                    ProgressiveDownloadUris.ForEach(uri => sb.AppendLine(uri.AbsoluteUri));
                }
                sb.AppendLine("");
                sb.AppendLine("==============================================================================");
                sb.AppendLine("");
            }
            return sb;
        }



        public static string DoPlayBackWithStreamingEndpoint(PlayerType typeplayer, string Urlstr, CloudMediaContext context, Mainform mainForm,
            IAsset myasset = null, bool DoNotRewriteURL = false, string filter = null, AssetProtectionType keytype = AssetProtectionType.None,
            AzureMediaPlayerFormats formatamp = AzureMediaPlayerFormats.Auto,
            AzureMediaPlayerTechnologies technology = AzureMediaPlayerTechnologies.Auto, bool launchbrowser = true, bool UISelectSEFiltersAndProtocols = true, string selectedBrowser = "")
        {
            string FullPlayBackLink = null;

            if (!string.IsNullOrEmpty(Urlstr))
            {
                IStreamingEndpoint choosenSE = AssetInfo.GetBestStreamingEndpoint(context);

                // Let's ask for SE if several SEs or Custom Host Names or Filters
                if (!DoNotRewriteURL)
                {
                    if (true
                        //(myasset != null && UISelectSEFiltersAndProtocols)
                        //&&
                        //(context.StreamingEndpoints.Count() > 1 || (context.StreamingEndpoints.FirstOrDefault() != null && context.StreamingEndpoints.FirstOrDefault().CustomHostNames.Count > 0) || context.Filters.Count() > 0 || (myasset.AssetFilters.Count() > 0))
                        )
                    {
                        var form = new ChooseStreamingEndpoint(context, myasset, Urlstr, filter, typeplayer, true);
                        if (form.ShowDialog() == DialogResult.OK)
                        {
                            Urlstr = AssetInfo.RW(new Uri(Urlstr), form.SelectStreamingEndpoint, form.SelectedFilters, form.ReturnHttps, form.ReturnSelectCustomHostName, form.ReturnStreamingProtocol, form.ReturnHLSAudioTrackName, form.ReturnHLSNoAudioOnlyMode).ToString();
                            choosenSE = form.SelectStreamingEndpoint;
                            selectedBrowser = form.ReturnSelectedBrowser;
                        }
                        else
                        {
                            return string.Empty;
                        }
                    }
                    else // no UI but let's rw for filter
                    {
                        if (typeplayer == PlayerType.DASHIFRefPlayer)
                        {
                            Urlstr = AssetInfo.RW(new Uri(Urlstr), choosenSE, filter, false, null, AMSOutputProtocols.Dash).ToString();
                        }
                        else
                        {
                            Urlstr = RW(Urlstr, choosenSE, filter);
                        }
                    }
                }

                DynamicEncryption.TokenResult tokenresult = new DynamicEncryption.TokenResult();

                if (myasset != null)
                {
                    keytype = AssetInfo.GetAssetProtection(myasset, context); // let's save the protection scheme (use by azure player): AES, PlayReady, Widevine or PlayReadyAndWidevine

                    if (DynamicEncryption.IsAssetHasAuthorizationPolicyWithToken(myasset, context)) // dynamic encryption with token
                    {
                        // user wants perhaps to play an asset with a token, so let's try to generate it
                        switch (typeplayer)
                        {

                            case PlayerType.AzureMediaPlayer:
                            case PlayerType.AzureMediaPlayerFrame:
                            case PlayerType.CustomPlayer:
                                switch (keytype)
                                {
                                    case AssetProtectionType.None:
                                        break;
                                    case AssetProtectionType.AES:
                                    case AssetProtectionType.PlayReady:
                                    case AssetProtectionType.Widevine:
                                    case AssetProtectionType.PlayReadyAndWidevine:
                                        tokenresult = DynamicEncryption.GetTestToken(myasset, context, displayUI: true);
                                        if (!string.IsNullOrEmpty(tokenresult.TokenString))
                                        {
                                            tokenresult.TokenString = HttpUtility.UrlEncode(Constants.Bearer + tokenresult.TokenString);
                                            //tokenresult.TokenString = Constants.Bearer + tokenresult.TokenString;
                                        }
                                        break;
                                }
                                break;

                            default:
                                // no token enabled player
                                break;
                        }
                    }

                }


                // let's launch the player
                switch (typeplayer)
                {
                    case PlayerType.AzureMediaPlayer:
                    case PlayerType.AzureMediaPlayerFrame:
                    case PlayerType.AzureMediaPlayerClear:

                        string playerurl = "";

                        if (keytype != AssetProtectionType.None)
                        {
                            bool insertoken = !string.IsNullOrEmpty(tokenresult.TokenString);

                            if (insertoken)  // token. Let's analyse the token to find the drm technology used
                            {
                                switch (tokenresult.ContentKeyDeliveryType)
                                {
                                    case ContentKeyDeliveryType.BaselineHttp:
                                        playerurl += string.Format(Constants.AMPAes, true.ToString());
                                        playerurl += string.Format(Constants.AMPAesToken, tokenresult.TokenString);
                                        break;

                                    case ContentKeyDeliveryType.PlayReadyLicense:
                                        playerurl += string.Format(Constants.AMPPlayReady, true.ToString());
                                        playerurl += string.Format(Constants.AMPPlayReadyToken, tokenresult.TokenString);
                                        break;

                                    case ContentKeyDeliveryType.Widevine:
                                        playerurl += string.Format(Constants.AMPWidevine, true.ToString());
                                        playerurl += string.Format(Constants.AMPWidevineToken, tokenresult.TokenString);
                                        break;

                                    default:
                                        break;
                                }
                            }
                            else // No token. Open mode. Let's look to the key to know the drm technology
                            {
                                switch (keytype)
                                {
                                    case AssetProtectionType.AES:
                                        playerurl += string.Format(Constants.AMPAes, true.ToString());
                                        break;

                                    case AssetProtectionType.PlayReady:
                                        playerurl += string.Format(Constants.AMPPlayReady, true.ToString());
                                        break;

                                    case AssetProtectionType.Widevine:
                                        playerurl += string.Format(Constants.AMPWidevine, true.ToString());
                                        break;

                                    case AssetProtectionType.PlayReadyAndWidevine:
                                        playerurl += string.Format(Constants.AMPPlayReady, true.ToString());
                                        playerurl += string.Format(Constants.AMPWidevine, true.ToString());
                                        break;

                                    default:
                                        break;
                                }
                            }
                        }

                        if (formatamp != AzureMediaPlayerFormats.Auto)
                        {
                            switch (formatamp)
                            {
                                case AzureMediaPlayerFormats.Dash:
                                    playerurl += string.Format(Constants.AMPformatsyntax, "dash");
                                    break;

                                case AzureMediaPlayerFormats.Smooth:
                                    playerurl += string.Format(Constants.AMPformatsyntax, "smooth");
                                    break;

                                case AzureMediaPlayerFormats.HLS:
                                    playerurl += string.Format(Constants.AMPformatsyntax, "hls");
                                    break;

                                case AzureMediaPlayerFormats.VideoMP4:
                                    playerurl += string.Format(Constants.AMPformatsyntax, "video/mp4");
                                    break;

                                default: // auto or other
                                    break;
                            }
                            if (tokenresult.TokenString != null)
                            {
                                playerurl += string.Format(Constants.AMPtokensyntax, tokenresult);
                            }
                        }
                        else // format auto. If classic se, and asset is smooth, let's force to smooth (player cannot get the dash stream for example)
                        {
                            if (!StreamingEndpointInformation.CanDoDynPackaging(choosenSE) && myasset != null && myasset.AssetType == AssetType.SmoothStreaming)
                                playerurl += string.Format(Constants.AMPformatsyntax, "smooth");
                        }


                        if (technology != AzureMediaPlayerTechnologies.Auto)
                        {
                            switch (technology)
                            {
                                case AzureMediaPlayerTechnologies.Flash:
                                    playerurl += string.Format(Constants.AMPtechsyntax, "flash");
                                    break;

                                case AzureMediaPlayerTechnologies.JavaScript:
                                    playerurl += string.Format(Constants.AMPtechsyntax, "js");
                                    break;

                                case AzureMediaPlayerTechnologies.NativeHTML5:
                                    playerurl += string.Format(Constants.AMPtechsyntax, "html5");
                                    break;

                                case AzureMediaPlayerTechnologies.Silverlight:
                                    playerurl += string.Format(Constants.AMPtechsyntax, "silverlight");
                                    break;

                                default: // auto or other
                                    break;
                            }
                        }

                        if (myasset != null) // wtt subtitles files
                        {
                            var subtitles = myasset.AssetFiles.ToList().Where(f => f.Name.ToLower().EndsWith(".vtt")).ToList();
                            if (subtitles.Count > 0)
                            {
                                var urlasset = new Uri(Urlstr);
                                string baseurlwith = urlasset.GetLeftPart(UriPartial.Authority) + urlasset.Segments[0] + urlasset.Segments[1];
                                var listsub = new List<string>();
                                foreach (var s in subtitles)
                                {
                                    listsub.Add(Path.GetFileNameWithoutExtension(s.Name) + ",und," + HttpUtility.UrlEncode(baseurlwith + s.Name));
                                }
                                playerurl += string.Format(Constants.AMPSubtitles, string.Join(";", listsub));
                            }
                        }

                        string playerurlbase = "";
                        if (typeplayer == PlayerType.AzureMediaPlayer)
                        {
                            playerurlbase = Constants.PlayerAMPToLaunch;
                        }
                        else if (typeplayer == PlayerType.AzureMediaPlayerFrame)
                        {
                            playerurlbase = Constants.PlayerAMPIFrameToLaunch;
                        }
                        else if (typeplayer == PlayerType.AzureMediaPlayerClear)
                        {
                            playerurlbase = Constants.PlayerAMPToLaunch.Replace("https://", "http://");
                        }

                        FullPlayBackLink = string.Format(playerurlbase, HttpUtility.UrlEncode(Urlstr)) + playerurl;
                        break;

                    case PlayerType.DASHIFRefPlayer:
                        if (!Urlstr.Contains(string.Format(AssetInfo.format_url, AssetInfo.format_dash)))
                        {
                            Urlstr = AssetInfo.AddParameterToUrlString(Urlstr, string.Format(AssetInfo.format_url, AssetInfo.format_dash));
                        }
                        FullPlayBackLink = string.Format(Constants.PlayerDASHIFToLaunch, Urlstr);
                        break;

                    case PlayerType.MP4AzurePage:
                        FullPlayBackLink = string.Format(Constants.PlayerMP4AzurePage, HttpUtility.UrlEncode(Urlstr));
                        break;

                    case PlayerType.CustomPlayer:
                        string myurl = Properties.Settings.Default.CustomPlayerUrl;
                        FullPlayBackLink = myurl.Replace(Constants.NameconvManifestURL, HttpUtility.UrlEncode(Urlstr)).Replace(Constants.NameconvToken, tokenresult.TokenString);
                        break;
                }

                if (FullPlayBackLink != null && launchbrowser)
                {
                    try
                    {
                        if (string.IsNullOrEmpty(selectedBrowser))
                        {
                            Process.Start(FullPlayBackLink);
                        }
                        else
                        {
                            if (selectedBrowser.Contains("edge"))
                            {
                                Process.Start(selectedBrowser + FullPlayBackLink);
                            }
                            else
                            {
                                Process.Start(selectedBrowser, FullPlayBackLink);
                            }
                        }
                    }
                    catch
                    {
                        mainForm.TextBoxLogWriteLine("Error when launching the browser.", true);
                    }
                }
            }

            return FullPlayBackLink;
        }



        internal static IStreamingEndpoint GetBestStreamingEndpoint(CloudMediaContext _context)
        {
            IStreamingEndpoint SESelected = _context.StreamingEndpoints.AsEnumerable().Where(se => se.State == StreamingEndpointState.Running && StreamingEndpointInformation.CanDoDynPackaging(se)).OrderBy(se => se.CdnEnabled).OrderBy(se => se.ScaleUnits).LastOrDefault();
            if (SESelected == null) SESelected = _context.StreamingEndpoints.Where(se => se.Name == "default").FirstOrDefault();

            return SESelected;
        }

        // copy a directory of the same container to another container
        public static List<Task> CopyBlobDirectory(CloudBlobDirectory srcDirectory, CloudBlobContainer destContainer, string sourceblobToken, CancellationToken token)
        {

            List<Task> mylistresults = new List<Task>();

            var srcBlobList = srcDirectory.ListBlobs(
                useFlatBlobListing: true,
                blobListingDetails: BlobListingDetails.None).ToList();

            foreach (var src in srcBlobList)
            {
                var srcBlob = src as ICloudBlob;

                // Create appropriate destination blob type to match the source blob
                CloudBlob destBlob;
                if (srcBlob.Properties.BlobType == BlobType.BlockBlob)
                    destBlob = destContainer.GetBlockBlobReference(srcBlob.Name);
                else
                    destBlob = destContainer.GetPageBlobReference(srcBlob.Name);

                // copy using src blob as SAS
                mylistresults.Add(destBlob.StartCopyAsync(new Uri(srcBlob.Uri.AbsoluteUri + sourceblobToken), token));
            }

            return mylistresults;
        }


        public static string GetXMLSerializedTimeSpan(TimeSpan timespan)
        // return TimeSpan as a XML string: P28DT15H50M58.348S
        {
            DataContractSerializer serialize = new DataContractSerializer(typeof(TimeSpan));
            XNamespace ns = "http://schemas.microsoft.com/2003/10/Serialization/";

            using (MemoryStream ms = new MemoryStream())
            {
                serialize.WriteObject(ms, timespan);
                string xmlstart = Encoding.Default.GetString(ms.ToArray());
                // serialization is : <duration xmlns="http://schemas.microsoft.com/2003/10/Serialization/">P28DT15H50M58.348S</duration>
                return XDocument.Parse(xmlstart).Element(ns + "duration").Value.ToString();
            }
        }

        private static readonly List<string> InvalidFileNamePrefixList = new List<string>
                {
                    "CON",
                    "PRN",
                    "AUX",
                    "NUL",
                    "COM1",
                    "COM2",
                    "COM3",
                    "COM4",
                    "COM5",
                    "COM6",
                    "COM7",
                    "COM8",
                    "COM9",
                    "LPT1",
                    "LPT2",
                    "LPT3",
                    "LPT4",
                    "LPT5",
                    "LPT6",
                    "LPT7",
                    "LPT8",
                    "LPT9"
                };

        private static readonly char[] NtfsInvalidChars = System.IO.Path.GetInvalidFileNameChars();


        public static bool AssetFileNameIsOk(string filename)
        {
            // check if the filename is compatible with AMS
            // Validates if the asset file name conforms to the following requirements
            // AssetFile name must be a valid blob name.
            // AssetFile name must be a valid NTFS file name.
            // AssetFile name length must be limited to 248 characters. 
            // AssetFileName should not contain the following characters: + % and #
            // AssetFileName should not contain only space(s)
            // AssetFileName should not start with certain prefixes restricted by NTFS such as CON1, PRN ... 
            // An AssetFileName constructed using the above mentioned criteria shall be encoded, streamed and played back successfully.

            if (string.IsNullOrWhiteSpace(filename))
            {
                return false;
            }

            // let's make sure we exract the file name (without the path)
            filename = Path.GetFileName(filename);

            // white space
            if (string.IsNullOrWhiteSpace(filename))
            {
                return false;
            }

            if (filename.Length > 248)
            {
                return false;
            }

            if (filename.IndexOfAny(NtfsInvalidChars) > 0 || Regex.IsMatch(filename, @"[+%#]+"))
            {
                return false;
            }

            //// Invalid NTFS Filename prefix checks
            if (InvalidFileNamePrefixList.Any(x => filename.StartsWith(x + ".", StringComparison.OrdinalIgnoreCase)) ||
                InvalidFileNamePrefixList.Any(x => filename.Equals(x, StringComparison.OrdinalIgnoreCase)))
            {
                return false;
            }

            return true;
        }

        // Return the list of problematic filenames
        public static List<string> ReturnFilenamesWithProblem(List<string> filenames)
        {
            List<string> listreturn = new List<string>();
            foreach (string f in filenames)
            {
                if (!AssetFileNameIsOk(f))
                {
                    listreturn.Add(Path.GetFileName(f));
                }
            }
            return listreturn;
        }

        public static string FileNameProblemMessage(List<string> listpb)

        {
            if (listpb.Count == 1)
            {
                return "This file name is not compatible with Media Services :\n\n" + listpb.FirstOrDefault() + "\n\nFile name is restricted to 248 characters and should not contain the characters " + @"<>:""/\|?*+%#" + "\n\nOperation aborted.";

            }
            else
            {
                return "These file names are not compatible with Media Services :\n\n" + string.Join("\n", listpb) + "\n\nFile name is restricted to 248 characters and should not contain the characters " + @"<>:""/\|?*+%#" + "\n\nOperation aborted.";

            }
        }
    }



    public class TaskSizeAndPrice
    {
        public long InputSize { get; set; }
        public long OutputSize { get; set; }
        public double Price { get; set; }
    }

    public class JobEntry
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public int Tasks { get; set; }
        public int Priority { get; set; }
        public JobState State { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Duration { get; set; }
        public Double Progress { get; set; }
    }


    public class AssetEntry : INotifyPropertyChanged
    {
        public string _Name;
        public string Name
        {
            get
            { return _Name; }
            set
            {
                if (value != _Name)
                {
                    _Name = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string Id { get; set; }

        public string _AlternateId;
        public string AlternateId
        {
            get
            { return _AlternateId; }
            set
            {
                if (value != _AlternateId)
                {
                    _AlternateId = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string _Type;
        public string Type
        {
            get
            { return _Type; }
            set
            {
                if (value != _Type)
                {
                    _Type = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string _LastModified;
        public string LastModified
        {
            get
            { return _LastModified; }
            set
            {
                if (value != _LastModified)
                {
                    _LastModified = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string _Size;
        public string Size
        {
            get
            { return _Size; }
            set
            {
                if (value != _Size)
                {
                    _Size = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private long _SizeLong;
        public long SizeLong
        {
            get
            { return _SizeLong; }
            set
            {
                if (value != _SizeLong)
                {
                    _SizeLong = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string Storage { get; set; }

        public Bitmap _StaticEncryption = null;
        public Bitmap StaticEncryption
        {
            get
            { return _StaticEncryption; }
            set
            {
                if (value != _StaticEncryption)
                {
                    _StaticEncryption = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string _StaticEncryptionMouseOver;
        public string StaticEncryptionMouseOver
        {
            get
            { return _StaticEncryptionMouseOver; }
            set
            {
                if (value != _StaticEncryptionMouseOver)
                {
                    _StaticEncryptionMouseOver = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private Bitmap _DynamicEncryption;
        public Bitmap DynamicEncryption
        {
            get
            { return _DynamicEncryption; }
            set
            {
                if (value != _DynamicEncryption)
                {
                    _DynamicEncryption = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string _DynamicEncryptionMouseOver;
        public string DynamicEncryptionMouseOver
        {
            get
            { return _DynamicEncryptionMouseOver; }
            set
            {
                if (value != _DynamicEncryptionMouseOver)
                {
                    _DynamicEncryptionMouseOver = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private Bitmap _Publication = null;

        public Bitmap Publication
        {
            get
            { return _Publication; }
            set
            {
                if (value != _Publication)
                {
                    _Publication = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private Bitmap _Filters = null;
        public Bitmap Filters
        {
            get
            { return _Filters; }
            set
            {
                if (value != _Filters)
                {
                    _Filters = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string _FiltersMouseOver;
        public string FiltersMouseOver
        {
            get
            { return _FiltersMouseOver; }
            set
            {
                if (value != _FiltersMouseOver)
                {
                    _FiltersMouseOver = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string _PublicationMouseOver;
        public string PublicationMouseOver
        {
            get
            { return _PublicationMouseOver; }
            set
            {
                if (value != _PublicationMouseOver)
                {
                    _PublicationMouseOver = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string _LocatorExpirationDate;
        public string LocatorExpirationDate
        {
            get
            { return _LocatorExpirationDate; }
            set
            {
                if (value != _LocatorExpirationDate)
                {
                    _LocatorExpirationDate = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private bool _LocatorExpirationDateWarning;
        public bool LocatorExpirationDateWarning
        {
            get
            { return _LocatorExpirationDateWarning; }
            set
            {
                if (value != _LocatorExpirationDateWarning)
                {
                    _LocatorExpirationDateWarning = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private bool _AssetWarning;
        public bool AssetWarning
        {
            get
            { return _AssetWarning; }
            set
            {
                if (value != _AssetWarning)
                {
                    _AssetWarning = value;
                    NotifyPropertyChanged();
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String p = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(p));
            }
        }

    }

    public class ACSEndPointMapping
    {
        public string Name { get; set; }
        public string APIServer { get; set; }
        public string Scope { get; set; }
        public string ACSBaseAddress { get; set; }
        public string AzureEndpoint { get; set; }
        public string ManagementPortal { get; set; }
    }


    public class AADEndPointMapping
    {
        public string Name { get; set; }

        public string ManagementPortal { get; set; }
    }


    public class ExplorerOpenIDSample
    {
        public string Name { get; set; }
        public string Uri { get; set; }
    }

    public enum EndPointMappingName
    {
        AzureGlobal = 0,
        AzureChina,
        AzureGovernment
    }

    public class ListCredentials
    {
        public decimal Version = 1;
        public List<CredentialsEntry> MediaServicesAccounts = new List<CredentialsEntry>();
    }


    public class CredentialsEntry : IEquatable<CredentialsEntry>
    {
        public string AccountName { get; set; }
        public string AccountKey { get; set; }
        public string ADTenantDomain { get; set; }
        public string ADRestAPIEndpoint { get; set; }
        public string ADDeploymentName { get; set; }
        public AzureEnvironment ADCustomSettings { get; set; }
        [JsonIgnore] // In order to not export the SP credential
        public string ADSPClientId { get; set; }
        [JsonIgnore] // In order to not export the SP credential
        public string ADSPClientSecret { get; set; }
        public string DefaultStorageKey { get; set; }
        public string Description { get; set; }
        public bool UseAADInteract { get; set; }
        public bool UseAADServicePrincipal { get; set; }
        public bool UsePartnerAPI { get; set; }
        public bool UseOtherAPI { get; set; }
        public string OtherAPIServer { get; set; }
        public string OtherScope { get; set; }
        public string OtherACSBaseAddress { get; set; }
        public string OtherAzureEndpoint { get; set; }
        public string OtherManagementPortal { get; set; }


        public static readonly int StringsCount = 10; // number of strings
        public static readonly string PartnerAPIServer = "https://nimbuspartners.cloudapp.net/API/";
        public static readonly string PartnerScope = "urn:NimbusPartners";
        public static readonly string PartnerACSBaseAddress = "https://mediaservices.accesscontrol.windows.net";
        public static readonly string PartnerAzureEndpoint = "";

        public static readonly string CoreServiceManagement = "https://management.core."; // with Azure endpoint, that gives "https://management.core.windows.net" for Azure Global and "https://management.core.chinacloudapi.cn" for China
        public static readonly string CoreAttachStorageURL = "https://{0}.blob.core."; // with Azure endpoint, that gives "https://{0}.blob.core.windows.net" for Azure Global and "https://{0}.blob.core.chinacloudapi.cn/" for China
        public static readonly string CoreStorage = "core."; // with Azure endpoint, that gives "core.windows.net" for Azure Global and "core.chinacloudapi.cn" for China
        public static readonly string TableStorage = ".table.core."; // with Azure endpoint, that gives "core.windows.net" for Azure Global and "core.chinacloudapi.cn" for China

        public static readonly string GlobalAzureEndpoint = "windows.net";
        public static readonly string GlobalPortal = "http://portal.azure.com";


        public static readonly IList<ACSEndPointMapping> ACSMappings = new List<ACSEndPointMapping> {
            // Global
            new ACSEndPointMapping() {
                Name =AMSExplorer.Properties.Resources.AMSLogin_AzureGlobal,
                APIServer = "https://media.windows.net/API/",
                Scope = "urn:WindowsAzureMediaServices",
                ACSBaseAddress ="https://wamsprodglobal001acs.accesscontrol.windows.net",
                AzureEndpoint = "windows.net",
                ManagementPortal ="https://portal.azure.com"
            }, 
           
            
            // China
            new ACSEndPointMapping() {
                Name =AMSExplorer.Properties.Resources.AMSLogin_AzureInChina,
                APIServer = "https://wamsbjbclus001rest-hs.chinacloudapp.cn/API/",
                Scope = "urn:WindowsAzureMediaServices",
                ACSBaseAddress ="https://wamsprodglobal001acs.accesscontrol.chinacloudapi.cn",
                AzureEndpoint = "chinacloudapi.cn",
                ManagementPortal ="https://portal.azure.cn"
            }, 
           
            // US Government
            new ACSEndPointMapping() {
                Name =AMSExplorer.Properties.Resources.AMSLogin_AzureGovernment,
                APIServer = "https://ams-usge-1-hos-rest-1-1.usgovcloudapp.net/API/",
                Scope = "urn:WindowsAzureMediaServices",
                ACSBaseAddress ="https://ams-usge-0-acs-global-1-1.accesscontrol.usgovcloudapi.net",
                AzureEndpoint = "usgovcloudapi.net",
                ManagementPortal ="https://portal.azure.us"
            }
        };


        public static readonly IList<AADEndPointMapping> AADMappings = new List<AADEndPointMapping> {
        
            // Global
            new AADEndPointMapping() {
                Name = nameof(AzureEnvironments.AzureCloudEnvironment),
                ManagementPortal ="https://portal.azure.com"
            }, 
                       
            // China
            new AADEndPointMapping() {
                Name = nameof(AzureEnvironments.AzureChinaCloudEnvironment),
                ManagementPortal ="https://portal.azure.cn"
            }, 
           
            // US Government
            new AADEndPointMapping() {
                 Name = nameof(AzureEnvironments.AzureUsGovernmentEnvironment),
                ManagementPortal ="https://portal.azure.us"
            },

            // Germany
            new AADEndPointMapping() {
                 Name = nameof(AzureEnvironments.AzureGermanCloudEnvironment),
                ManagementPortal ="https://portal.microsoftazure.de"
            }
        };


        public CredentialsEntry(string accountname, string accountkey, string adtenantdomain, string adrestapiendpoint, string storagekey, string description, bool useaadinterative, bool useaadserviceprincipal, bool useacspartnerapi, bool useacsotherapi, string acsapiserver, string acsscope, string acsbaseaddress, string acsazureendpoint, string managementportal, string addeploymentname = null, AzureEnvironment adcustomsettings = null, string adspclientid = null, string adspclientsecret = null)
        {
            AccountName = accountname;
            AccountKey = accountkey;
            ADTenantDomain = string.IsNullOrEmpty(adtenantdomain) ? null : adtenantdomain;
            ADRestAPIEndpoint = string.IsNullOrEmpty(adrestapiendpoint) ? null : adrestapiendpoint;
            DefaultStorageKey = storagekey;
            Description = description;
            UseAADInteract = useaadinterative;
            UseAADServicePrincipal = useaadserviceprincipal;
            UsePartnerAPI = useacspartnerapi;
            UseOtherAPI = useacsotherapi;
            OtherAPIServer = string.IsNullOrEmpty(acsapiserver) ? null : acsapiserver;
            OtherScope = string.IsNullOrEmpty(acsscope) ? null : acsscope;
            OtherACSBaseAddress = string.IsNullOrEmpty(acsbaseaddress) ? null : acsbaseaddress;
            OtherAzureEndpoint = string.IsNullOrEmpty(acsazureendpoint) ? null : acsazureendpoint;
            OtherManagementPortal = string.IsNullOrEmpty(managementportal) ? null : managementportal;
            ADSPClientId = adspclientid;
            ADSPClientSecret = adspclientsecret;
            ADDeploymentName = addeploymentname;
            ADCustomSettings = adcustomsettings;
        }

        public bool Equals(CredentialsEntry other)
        {
            return
                (this.AccountKey ?? "") == (other.AccountKey ?? "")
                && (this.AccountName ?? "") == (other.AccountName ?? "")
                && (this.ADRestAPIEndpoint ?? "") == (other.ADRestAPIEndpoint ?? "")
                && (this.ADTenantDomain ?? "") == (other.ADTenantDomain ?? "")
                && this.UseAADInteract == other.UseAADInteract
                && this.UseAADServicePrincipal == other.UseAADServicePrincipal
                && (this.ADDeploymentName ?? "") == (other.ADDeploymentName ?? "")
                && (this.ADCustomSettings) == (other.ADCustomSettings)
                && this.UseOtherAPI == other.UseOtherAPI
                && this.UsePartnerAPI == other.UsePartnerAPI
                && (this.Description ?? "") == (other.Description ?? "")
                && (this.OtherACSBaseAddress ?? "") == (other.OtherACSBaseAddress ?? "")
                && (this.OtherAPIServer ?? "") == (other.OtherAPIServer ?? "")
                && (this.OtherAzureEndpoint ?? "") == (other.OtherAzureEndpoint ?? "")
                && (this.OtherManagementPortal ?? "") == (other.OtherManagementPortal ?? "")
                && (this.OtherScope ?? "") == (other.OtherScope ?? "")
                && (this.DefaultStorageKey ?? "") == (other.DefaultStorageKey ?? "")
                 ;
        }

        public string GetTableEndPoint(string mediaServicesStorageAccountName)
        {
            if (!UseAADInteract && !UseAADServicePrincipal) // ACS Mode
            {
                string SampleStorageURLTemplate = UseOtherAPI ?
                        CredentialsEntry.TableStorage + OtherAzureEndpoint : // ".table.core.chinacloudapi.cn/"
                        CredentialsEntry.TableStorage + CredentialsEntry.GlobalAzureEndpoint; // ".table.core.windows.net"

                return "https://" + mediaServicesStorageAccountName + SampleStorageURLTemplate;
            }
            else // AAD Mode
            {
                if (ADDeploymentName != null) // one of the default
                {
                    return "https://" + mediaServicesStorageAccountName + CredentialsEntry.TableStorage + ReturnHostNameTwoSegmentsRight(ReturnADEnvironment(ADDeploymentName).MediaServicesResource); // "https://accountname.table.core.cloudapi.de"
                }
                else if (ADCustomSettings != null)
                {
                    return "https://" + mediaServicesStorageAccountName + CredentialsEntry.TableStorage + ReturnHostNameTwoSegmentsRight(ADCustomSettings.MediaServicesResource); // "https://accountname.table.core.cloudapi.de"
                }
                else // Global
                {
                    return "https://" + mediaServicesStorageAccountName + CredentialsEntry.TableStorage + CredentialsEntry.GlobalAzureEndpoint; // "https://accountname.table.core.windows.net"

                }
            }
        }

        private string ReturnHostNameTwoSegmentsRight(string myUrl)
        {
            var hosts = (new Uri(myUrl)).Host.Split('.');
            int i = hosts.Count();
            return hosts[i - 2] + "." + hosts[i - 1];
        }

        public static AzureEnvironment ReturnADEnvironment(string ADDeploymentName) // Return the AzureEnvonment based on name
        {
            Type myType = typeof(AzureEnvironments);
            FieldInfo[] myFields = myType.GetFields(BindingFlags.Static | BindingFlags.Public);
            var found = myFields.Where(f => f.Name == ADDeploymentName).FirstOrDefault();
            if (found != null)
            {
                return (AzureEnvironment)myFields.Where(f => f.Name == ADDeploymentName).FirstOrDefault().GetValue(myType);

            }
            else // default
            {
                return AzureEnvironments.AzureCloudEnvironment;
            }
        }

        // return the storage suffix for China, Germany etc, or null for Global Azure
        public string ReturnStorageSuffix()
        {
            /*
            if (UseOtherAPI)
                return CoreStorage + OtherAzureEndpoint;
            else
                return null;
                */


            if (!UseAADInteract && !UseAADServicePrincipal) // ACS Mode
            {
                return UseOtherAPI ? CoreStorage + OtherAzureEndpoint : null;
            }
            else // AAD Mode
            {
                if (ADDeploymentName != null) // one of the default
                {
                    return ReturnHostNameTwoSegmentsRight(ReturnADEnvironment(ADDeploymentName).MediaServicesResource); // "https://accountname.table.core.cloudapi.de"
                }
                else if (ADCustomSettings != null)
                {
                    return ReturnHostNameTwoSegmentsRight(ADCustomSettings.MediaServicesResource); // "https://accountname.table.core.cloudapi.de"
                }
                else // Global
                {
                    return null;
                }
            }
        }
    }

    public enum PlayerType
    {
        AzureMediaPlayer = 0,
        AzureMediaPlayerFrame,
        AzureMediaPlayerClear,
        DASHIFRefPlayer,
        MP4AzurePage,
        CustomPlayer
    }

    public enum TaskJobCreationMode
    {
        OneJobPerInputAsset = 0,
        OneJobPerVisibleAsset,
        SingleJobForAllInputAssets
    }

    public enum PublishStatus
    {
        NotPublished = 0,
        PublishedActive,
        PublishedFuture,
        PublishedExpired
    }

    public enum AzureMediaPlayerFormats
    {
        Auto = 0,
        Smooth = 1,
        Dash = 2,
        HLS = 3,
        VideoMP4 = 4
    }

    public enum AMSOutputProtocols
    {
        NotSpecified = 0,
        Smooth,
        SmoothLegacy,
        Dash,
        HLSv3,
        HLSv4,
        HDS
    }

    public enum AzureMediaPlayerTechnologies
    {
        Auto = 0,
        JavaScript,
        Flash,
        Silverlight,
        NativeHTML5
    }


    public enum AssetProtectionType
    {
        None = 0,
        AES,
        PlayReady,
        Widevine,
        PlayReadyAndWidevine
    }

    public enum TypeInputExtraInput
    {
        None = 0,
        SelectedWorkflow,
        SelectedAssets
    }



    public class WatchFolderSettings
    {
        public string FolderPath { get; set; }
        public bool IsOn { get; set; }
        public bool DeleteFile { get; set; }
        public bool PublishOutputAssets { get; set; }
        public string SendEmailToRecipient { get; set; }
        public IJobTemplate JobTemplate { get; set; }
        public List<IAsset> ExtraInputAssets { get; set; }
        public TypeInputExtraInput TypeInputExtraInput { get; set; }
        public FileSystemWatcher Watcher { get; set; }
        public INotificationEndPoint NotificationEndPoint { get; set; }
        public bool ProcessRohzetXML { get; set; }
        public bool ProcessJSONSemaphore { get; set; }
        public string CallAPIUrl { get; set; }
        public string CallAPJson { get; set; }


        public WatchFolderSettings()
        {
            FolderPath = string.Empty;
            IsOn = false;
            DeleteFile = false;
            PublishOutputAssets = false;
            SendEmailToRecipient = null;
            JobTemplate = null;
            ExtraInputAssets = null;
            TypeInputExtraInput = TypeInputExtraInput.None;
            ProcessRohzetXML = false;
            CallAPIUrl = null;
            CallAPJson = null;
        }
    }

    public class ManifestTimingData
    {
        public TimeSpan AssetDuration { get; set; }
        public ulong TimestampOffset { get; set; }
        public ulong? TimeScale { get; set; }
        public bool IsLive { get; set; }
        public bool Error { get; set; }
        public List<ulong> TimestampList { get; set; }
        public ulong TimestampEndLastChunk { get; set; }
        public bool DiscontinuityDetected { get; set; }
    }

    public class SubClipTrimmingDataXMLSerialized
    {
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Duration { get; set; }
    }

    public class SubClipTrimmingDataTimeSpan
    {
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public TimeSpan Duration { get; set; }
        public TimeSpan Offset { get; set; }
    }


    public class FilterCreationInfo
    {
        public string Name { get; set; }  // contains the full configuration for subclipping
        public FirstQuality Firstquality { get; set; }
        public PresentationTimeRange Presentationtimerange { get; set; }
        public IList<FilterTrackSelectStatement> Trackconditions { get; set; }


    }
    public class SubClipConfiguration
    {
        public string Configuration { get; set; }  // contains the full configuration for subclipping
        public bool Reencode { get; set; }
        public bool Trimming { get; set; }
        public bool CreateAssetFilter { get; set; }

        public List<ExplorerEDLEntryInOut> InOutForReencode { get; set; }

        public TimeSpan OffsetForReencode { get; set; }
        public TimeSpan StartTimeForAssetFilter { get; set; }
        public TimeSpan EndTimeForAssetFilter { get; set; }

    }

    class HostNameClass
    {
        public string HostName { get; set; }
    }

    public class Item
    {
        public string Name;
        public string Value;
        public Item(string name, string value)
        {
            Name = name; Value = value;
        }
        public override string ToString()
        {
            // Generates the text shown in the combo box
            return Name;
        }
    }

    public class GenericTask
    {
        public IMediaProcessor Processor;
        public string ProcessorConfiguration;
        public TypeInputAssetGeneric InputAssetType;
        public string InputAsset;
        public JobOptionsVar TaskOptions;
    }

    public class GenericTaskAsset
    {
        public TypeInputAssetGeneric InputAssetType;
        public string InputAsset;
    }

    public enum TypeInputAssetGeneric
    {
        InputJobAssets = 0,
        SpecificAssetID,
        TaskOutputAsset
    }

    public enum SearchIn
    {
        AssetName = 0,
        AssetId,
        AssetAltId,
        AssetFileName,
        AssetFileId,
        LocatorId,
        JobName,
        JobId,
        TaskName,
        TaskId,
        TaskProcessorId,
        ChannelName,
        ChannelId,
        ProgramName,
        ProgramId
    }

    public enum DownloadToFolderOption
    {
        DoNotCreateSubfolder = 0,
        SubfolderAssetName,
        SubfolderAssetId
    }

    public class SearchObject
    {
        public string Text { get; set; }
        public SearchIn SearchType { get; set; }

    }


    public class LocalEncoder
    {
        public string Name { get; set; }
        public string Folder { get; set; }
        public string Command { get; set; }
        public StreamingProtocol? Protocol { get; set; }
        public Uri InstallURL { get; set; }
        public bool CanBeRunLocally { get; set; }
        public bool EnableSettings { get; set; }
        public string Comment { get; set; }
    }




    public class IndexerOptionsVar
    {
        public bool WebVTT { get; set; }
        public bool SAMI { get; set; }
        public bool TTML { get; set; }
        public bool AIB { get; set; }
        public bool Keywords { get; set; }
        public bool ForFullCaptions { get; set; }
    }


    public class JobOptionsVar
    {
        public int Priority { get; set; }
        public string StorageSelected { get; set; }
        public TaskOptions TasksOptionsSetting { get; set; }
        public bool TasksOptionsSettingReadOnly { get; set; }
        public AssetCreationOptions OutputAssetsCreationOptions { get; set; }
        public AssetFormatOption OutputAssetsFormatOption { get; set; }
    }



    public class ConfigTelemetryVar
    {
        public string StorageSelected { get; set; }
        public MonitoringLevel MonitorLevelChannel { get; set; }
        public MonitoringLevel MonitorLevelStreamingEndpoint { get; set; }
    }



    public sealed class FilterPropertyFourCCValue
    {
        public static readonly string mp4a = "mp4a";
        public static readonly string avc1 = "avc1";
        public static readonly string mp4v = "mp4v";
        public static readonly string ec3 = "ec-3";
    }


    public sealed class FilterProperty
    {
        public const string Type = "Type";
        public const string Name = "Name";
        public const string Language = "Language";
        public const string FourCC = "FourCC";
        public const string Bitrate = "Bitrate";
    }

    public class ExFilterTrack
    {
        public List<ExCondition> conditions { get; set; }
    }

    public class ExCondition
    {
        public string property { get; set; }
        public string oper { get; set; }
        public string value { get; set; }
    }
    static class JSONExtensions
    {
        public static JToken RemoveFields(this JToken token, string[] fields)
        {
            JContainer container = token as JContainer;
            if (container == null) return token;

            List<JToken> removeList = new List<JToken>();
            foreach (JToken el in container.Children())
            {
                JProperty p = el as JProperty;
                if (p != null && fields.Contains(p.Name))
                {
                    removeList.Add(el);
                }
                el.RemoveFields(fields);
            }

            foreach (JToken el in removeList)
            {
                el.Remove();
            }

            return token;
        }
    }

    public class ListViewItemComparer : IComparer
    {
        private int col;
        private SortOrder order;
        public ListViewItemComparer()
        {
            col = 0;
            order = SortOrder.Ascending;
        }
        public ListViewItemComparer(int column, SortOrder order)
        {
            col = column;
            this.order = order;
        }
        public int Compare(object x, object y)
        {
            int returnVal = 0;
            bool Error = false;

            string sx = ((ListViewItem)x).SubItems[col].Text;
            string sy = ((ListViewItem)y).SubItems[col].Text;
            // Determine whether the type being compared is a date type.

            // Inverse_FormatByteSize
            // let's compare if the field is a size format (512 B or 45 KB...)

            // Parse the two objects passed as a parameter as a DateTime.
            try
            {
                var firstSize = AssetInfo.Inverse_FormatByteSize(sx);
                var secondSize = AssetInfo.Inverse_FormatByteSize(sy);
                if (firstSize != null && secondSize != null)
                {
                    var firstSizel = (long)firstSize;
                    var secondSizel = (long)secondSize;
                    if (firstSizel < secondSizel)
                        returnVal = -1;
                    else if (firstSizel > secondSizel)
                        returnVal = 1;
                    else
                        returnVal = 0;
                }
                else
                {
                    Error = true;
                }
            }
            catch
            {
                Error = true;
            }

            if (Error)
            {
                /*
                try
                {
                    // Parse the two objects passed as a parameter as a DateTime.
                    System.DateTime firstDate =
                            DateTime.Parse(sx); 
                    System.DateTime secondDate =
                            DateTime.Parse(sy);
                    // Compare the two dates.
                    returnVal = DateTime.Compare(firstDate, secondDate);
                }
                // If neither compared object has a valid date format, compare
                // as a string.
                catch
                {
                    // Compare the two items as a string.
                    returnVal = String.Compare(sx, sy);
                }
                */

                // Parse the two objects passed as a parameter as a DateTime.
                DateTime firstDate, secondDate;
                if (DateTime.TryParse(sx, out firstDate) && DateTime.TryParse(sy, out secondDate))
                {
                    returnVal = DateTime.Compare(firstDate, secondDate);
                }
                else
                {
                    returnVal = String.Compare(sx, sy);
                }
            }

            // Determine whether the sort order is descending.
            if (order == SortOrder.Descending)
                // Invert the value returned by String.Compare.
                returnVal *= -1;
            return returnVal;
        }

        static public void ListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            ListView ThisListView = (ListView)sender;
            // Determine whether the column is the same as the last column clicked.
            if (e.Column != ((int)ThisListView.Tag))
            {
                // Set the sort column to the new column.
                ThisListView.Tag = e.Column;
                // Set the sort order to ascending by default.
                ThisListView.Sorting = SortOrder.Ascending;
            }
            else
            {
                // Determine what the last sort order was and change it.
                if (ThisListView.Sorting == SortOrder.Ascending)
                    ThisListView.Sorting = SortOrder.Descending;
                else
                    ThisListView.Sorting = SortOrder.Ascending;
            }

            // Call the sort method to manually sort.
            ThisListView.Sort();
            // Set the ListViewItemSorter property to a new ListViewItemComparer
            // object.
            ThisListView.ListViewItemSorter = new ListViewItemComparer(e.Column,
                                                              ThisListView.Sorting);
        }
    }

    public class ListViewItemComparerQuickNoDate : IComparer
    {
        private int col;
        private SortOrder order;
        public ListViewItemComparerQuickNoDate()
        {
            col = 0;
            order = SortOrder.Ascending;
        }
        public ListViewItemComparerQuickNoDate(int column, SortOrder order)
        {
            col = column;
            this.order = order;
        }
        public int Compare(object x, object y)
        {
            int returnVal = 0;
            bool Error = false;

            string sx = ((ListViewItem)x).SubItems[col].Text;
            string sy = ((ListViewItem)y).SubItems[col].Text;


            // Inverse_FormatByteSize
            // let's compare if the field is a size format (512 B or 45 KB...)

            // Parse the two objects passed as a parameter as a DateTime.
            try
            {
                var firstSize = AssetInfo.Inverse_FormatByteSize(sx);
                var secondSize = AssetInfo.Inverse_FormatByteSize(sy);
                if (firstSize != null && secondSize != null)
                {
                    var firstSizel = (long)firstSize;
                    var secondSizel = (long)secondSize;
                    if (firstSizel < secondSizel)
                        returnVal = -1;
                    else if (firstSizel > secondSizel)
                        returnVal = 1;
                    else
                        returnVal = 0;

                }
                else
                {
                    Error = true;
                }

            }
            catch
            {
                Error = true;
            }

            if (Error)
            {
                // Compare the two items as a string.
                returnVal = String.Compare(sx, sy);
            }


            // Determine whether the sort order is descending.
            if (order == SortOrder.Descending)
                // Invert the value returned by String.Compare.
                returnVal *= -1;
            return returnVal;
        }

        static public void ListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            ListView ThisListView = (ListView)sender;
            // Determine whether the column is the same as the last column clicked.
            if (e.Column != ((int)ThisListView.Tag))
            {
                // Set the sort column to the new column.
                ThisListView.Tag = e.Column;
                // Set the sort order to ascending by default.
                ThisListView.Sorting = SortOrder.Ascending;
            }
            else
            {
                // Determine what the last sort order was and change it.
                if (ThisListView.Sorting == SortOrder.Ascending)
                    ThisListView.Sorting = SortOrder.Descending;
                else
                    ThisListView.Sorting = SortOrder.Ascending;
            }

            // Call the sort method to manually sort.
            ThisListView.Sort();
            // Set the ListViewItemSorter property to a new ListViewItemComparer
            // object.
            ThisListView.ListViewItemSorter = new ListViewItemComparer(e.Column,
                                                              ThisListView.Sorting);
        }
    }

}
