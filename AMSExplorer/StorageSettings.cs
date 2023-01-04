//----------------------------------------------------------------------------------------------
//    Copyright 2023 Microsoft Corporation
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


using Microsoft.Azure.Storage.Shared.Protocol;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace AMSExplorer
{
    public partial class StorageSettings : Form
    {
        public const string noversion = "(undefined)";
        private readonly ServiceProperties _serviceProperties;

        public string RequestedStorageVersion => (comboBoxVersion.Text == noversion) ? null : comboBoxVersion.Text;

        public StorageSettings(string storageName, string storageId, ServiceProperties serviceProperties)
        {
            InitializeComponent();
            Icon = Bitmaps.Azure_Explorer_ico;
            labelStorageAccount.Text = string.Format(labelStorageAccount.Text, storageName);
            _serviceProperties = serviceProperties;
            textBoxStorageId.Text = storageId;
        }

        private void StorageVersion_Load(object sender, EventArgs e)
        {
            // DpiUtils.InitPerMonitorDpi(this);

            List<string> list = new() { noversion, "2020-06-12", "2020-04-08", "2020-02-10", "2019-12-12", "2019-07-07", "2019-02-02", "2018-11-09", "2018-03-28", "2017-11-09", "2017-07-29", "2017-04-17", "2016-05-31", "2015-12-11", "2015-07-08", "2015-04-05", "2015-02-21", "2014-02-14", "2013-08-15", "2012-02-12", "2011-08-18", "2009-09-19", "2009-07-17", "2009-04-14" };
            comboBoxVersion.Items.AddRange(list.ToArray());
            comboBoxVersion.Text = _serviceProperties.DefaultServiceVersion ?? noversion;

            moreinfoLiveStreamingProfilelink.Links.Add(new LinkLabel.Link(0, moreinfoLiveStreamingProfilelink.Text.Length, Constants.LinkMoreInfoStorageVersioning));
        }

        private void moreinfoLiveStreamingProfilelink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Send the URL to the operating system.
            var p = new Process
            {
                StartInfo = new ProcessStartInfo { FileName = e.Link.LinkData as string, UseShellExecute = true }
            }; p.Start();
        }

        private void StorageSettings_DpiChanged(object sender, DpiChangedEventArgs e)
        {
            // DpiUtils.UpdatedSizeFontAfterDPIChange(labelStorageAccount, e);
        }

        private void StorageSettings_Shown(object sender, EventArgs e)
        {
            Telemetry.TrackPageView(this.Name);
        }
    }
}
