//----------------------------------------------------------------------------------------------
//    Copyright 2021 Microsoft Corporation
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
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace AMSExplorer
{
    public partial class SoftwareUpdate : Form
    {
        private readonly Uri _urlRelNotes;
        private readonly Version _newVersion;
        private readonly Uri _binaryUrl;

        public SoftwareUpdate(Uri urlRelNotes, Version newVersion, Uri binaryUrl)
        {
            InitializeComponent();
            Icon = Bitmaps.Azure_Explorer_ico;
            _urlRelNotes = urlRelNotes;
            _newVersion = newVersion;
            _binaryUrl = binaryUrl;
        }

        private void SoftwareUpdate_Load(object sender, EventArgs e)
        {
            // DpiUtils.InitPerMonitorDpi(this);
            if (_urlRelNotes != null)
            {
                webBrowser1.Source = _urlRelNotes;
            }

            linkLabelMoreInfoPrice.Links.Add(new LinkLabel.Link(0, linkLabelMoreInfoPrice.Text.Length, Constants.LinkAMSE));
            labelTitle.Text = string.Format(labelTitle.Text, _newVersion);
        }

        private void linkLabelMoreInfoPrice_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Send the URL to the operating system.
            var p = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = e.Link.LinkData as string,
                    UseShellExecute = true
                }
            };
            p.Start();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            progressBar1.Visible = true;
            buttonOk.Enabled = false;
            WebClient webClientB = new();
            string filename = System.IO.Path.GetFileName(_binaryUrl.LocalPath);

            webClientB.DownloadFileCompleted += DownloadFileCompletedBinary(filename);
            webClientB.DownloadProgressChanged += WebClientB_DownloadProgressChanged;
            webClientB.DownloadFileAsync(_binaryUrl, Path.GetTempPath() + filename);
        }

        private void WebClientB_DownloadProgressChanged(object sender, System.Net.DownloadProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        public static AsyncCompletedEventHandler DownloadFileCompletedBinary(string filename)
        {
            void action(object sender, AsyncCompletedEventArgs e)
            {
                Telemetry.TrackEvent("SoftwareUpdate New update downloaded and started");

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

        private void SoftwareUpdate_DpiChanged(object sender, DpiChangedEventArgs e)
        {
            // DpiUtils.UpdatedSizeFontAfterDPIChange(labelTitle, e);
        }

        private void SoftwareUpdate_FormClosed(object sender, FormClosedEventArgs e)
        {
            webBrowser1.Dispose();
        }

        private void SoftwareUpdate_Shown(object sender, EventArgs e)
        {
            Telemetry.TrackPageView(this.Name);
        }
    }
}
