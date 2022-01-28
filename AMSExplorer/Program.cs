//----------------------------------------------------------------------------------------------
//    Copyright 2022 Microsoft Corporation
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

using Microsoft.Azure.Management.Media.Models;
using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace AMSExplorer
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 

        private const string languageparam = "/language:";

        [STAThread]
        private static void Main(string[] args)
        {
            if (Properties.Settings.Default.Telemetry)
            {
                Telemetry.StartTelemetry();
            }

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // BUG
            // fix a crash on non english version of Windows with some shortcut keys.
            // See https://github.com/dotnet/winforms/issues/5036
            // All shortcuts with Del have been removed. But the issue can see occurs with CTRL on German OS for example
            // For now, let's force english UI per default, except for Japanese

            try
            {
                if (Thread.CurrentThread.CurrentUICulture != new CultureInfo("ja-JA", false))
                {
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US", false);
                }
                // if the user forces the language using /language: parameter...
                if (args.Length > 0 && args.Any(a => a.StartsWith(languageparam)))
                {
                    string language = args.Where(a => a.StartsWith(languageparam)).FirstOrDefault().Substring(languageparam.Length);
                    System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo(language, false);
                }
            }

            catch (Exception ex)
            {
                Telemetry.TrackException(ex);
            }

            Application.Run(new Mainform(args));
        }

        public static void DataGridViewV_Resize(object sender)
        {
            return; // let's disable this code for now
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
                grid.Columns[indexname].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                int colw = Math.Max(grid.Columns[indexname].Width, 100);
                grid.Columns[indexname].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                grid.Columns[indexname].Width = colw;
            }
        }


        public static string GetErrorMessage(Exception e)
        {
            string s = string.Empty;

            while (e != null)
            {
                if (e is ErrorResponseException eApi)
                {
                    s = eApi.Body?.Error?.Message;
                }
                else
                {
                    s = e.Message;
                }

                e = e.InnerException;
            }
            return s;// ParseXml(s);
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

        public enum TypeConfig
        {
            JSON = 0,
            XML,
            Empty,
            Other
        }

        public static string AnalyzeTextAndReportSyntaxError(string myText)
        {
            string strReturn = string.Empty;
            TypeConfig type = Program.AnalyseConfigurationString(myText);
            if (type == TypeConfig.JSON)
            {
                // Let's check JSON syntax
                try
                {
                    JObject jo = JObject.Parse(myText);
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
                    XElement xml = XElement.Load(new StringReader(myText));
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
                XDocument doc = XDocument.Parse(xml);
                return doc.Declaration + Environment.NewLine + doc.ToString();
            }
            catch (Exception)
            {
                return xml;
            }
        }

        public static string AnalyzeAndIndentXMLJSON(string myText)
        {
            TypeConfig type = Program.AnalyseConfigurationString(myText);
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

        public static async Task CheckWebView2VersionAsync()
        {
            // https://docs.microsoft.com/en-us/microsoft-edge/webview2/concepts/distribution
            // HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Microsoft\EdgeUpdate\Clients\{F3017226-FE2A-4295-8BDF-00C3A9A7E4C5}
            // HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\EdgeUpdate\Clients\{F3017226-FE2A-4295-8BDF-00C3A9A7E4C5}

            try
            {
                string regPath;
                if (Environment.Is64BitOperatingSystem)
                {
                    regPath = Constants.Webview2RegPath64;
                }
                else
                {
                    regPath = Constants.Webview2RegPath32;
                }

                var webViewRegEntry = Registry.LocalMachine.OpenSubKey(regPath);

                if (webViewRegEntry == null || (string)webViewRegEntry.GetValue("pv") == null || new Version((string)webViewRegEntry.GetValue("pv")) < new Version(Constants.Webview2MinVersion))
                {
                    Telemetry.TrackEvent("Program CheckWebView2VersionAsync Webview2 update required");
                    if (MessageBox.Show("Microsoft Edge WebView2 runtime must be installed or updated. Launch the download and installation now ?", "Webview2 runtime", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        WebClient webClient = new();
                        string filename = "EdgeViewSetup.exe";
                        webClient.DownloadFileCompleted += DownloadWebview2VersionRequestCompleted(filename);
                        webClient.DownloadFileAsync(new Uri(Constants.Webview2Installer), Path.GetTempPath() + filename);
                    }
                }
            }
            catch (Exception ex)
            {
                Telemetry.TrackException(ex);
            }

        }

        public static AsyncCompletedEventHandler DownloadWebview2VersionRequestCompleted(string filename)
        {

            void action(object sender, AsyncCompletedEventArgs e)
            {
                Telemetry.TrackEvent("AMSLogin Microsoft Edge WebView2 downloaded and started");

                Properties.Settings.Default.DeleteInstallationFile = Path.GetTempPath() + filename;
                Properties.Settings.Default.Save();

                var p = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = Path.GetTempPath() + filename,
                        UseShellExecute = true
                    }
                };
                p.Start();

                Environment.Exit(0);
            }
            return new AsyncCompletedEventHandler(action);
        }



        public static string MessageNewVersion = string.Empty;

#pragma warning disable 1998
        public static async Task CheckAMSEVersionAsync()
#pragma warning restore 1998
        {
            WebClient webClient = new();
            webClient.DownloadStringCompleted += (sender, e) => DownloadVersionRequestCompletedV3(true, sender, e);
            webClient.DownloadStringAsync(new Uri(Constants.GitHubAMSEVersionPrimaryV3));
        }

        public static void DownloadVersionRequestCompletedV3(bool firsttry, object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                try
                {
                    dynamic data = JsonConvert.DeserializeObject(e.Result);
                    Version versionAMSEGitHub = new((string)data.Version);
                    Uri RelNotesUrl = new((string)data.ReleaseNotesUrl);
                    Uri AllRelNotesUrl = new((string)data.AllReleaseNotesUrl);
                    Uri BinaryUrl = new((string)data.BinaryUrl);
                    string Parameters = (string)data.Parameters;

                    Version versionAMSELocal = Assembly.GetExecutingAssembly().GetName().Version;
                    if (versionAMSEGitHub > versionAMSELocal)
                    {
                        MessageNewVersion = string.Format("A new version ({0}) is available on GitHub: {1}", versionAMSEGitHub, Constants.GitHubAMSEReleases);
                        SoftwareUpdate form = new(RelNotesUrl, versionAMSEGitHub, BinaryUrl, Parameters);
                        form.ShowDialog();
                    }
                }
                catch
                {

                }
            }
            else if (firsttry)
            {
                WebClient webClient = new();
                webClient.DownloadStringCompleted += (sender2, e2) => DownloadVersionRequestCompletedV3(false, sender2, e2);
                webClient.DownloadStringAsync(new Uri(Constants.GitHubAMSEVersionSecondaryV3));
            }
        }


        public static Bitmap MakeRed(Bitmap original)
        {
            //make an empty bitmap the same size as original
            Bitmap newBitmap = new(original.Width, original.Height);

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
            Bitmap newBitmap = new(original.Width, original.Height);

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
            InputBox inputForm = new(title, promptText, value, passwordWildcard);

            inputForm.ShowDialog();
            value = inputForm.InputValue;

            return inputForm.DialogResult;
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
        }

        /// <summary>
        /// Generate a short uniqueness of 10 characters
        /// </summary>
        /// <returns></returns>
        public static string GetUniqueness()
        {
            return Guid.NewGuid().ToString().Substring(0, 11).Replace("-", "");
        }

        public class LiveOutputExt
        {
            public LiveOutput LiveOutputItem { get; set; }
            public string LiveEventName { get; set; }
        }
    }


    public static class Extensions
    {
        public static void Invoke<TControlType>(this TControlType control, Action<TControlType> del)
            where TControlType : Control
        {
            if (control.InvokeRequired)
                control.Invoke(new Action(() => del(control)));
            else
                del(control);
        }
    }
}
