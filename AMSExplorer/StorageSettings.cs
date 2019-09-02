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


using Microsoft.WindowsAzure.Storage.Shared.Protocol;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace AMSExplorer
{
    public partial class StorageSettings : Form
    {
        public const string noversion = "(undefined)";
        private readonly ServiceProperties _serviceProperties;

        public string RequestedStorageVersion => (comboBoxVersion.Text == noversion) ? null : comboBoxVersion.Text;

        public MetricsLevel RequestedMetricsLevel => (MetricsLevel)Enum.Parse(typeof(MetricsLevel), comboBoxMetrics.Text);

        public int? RequestedMetricsRetention => (numericUpDownRetention.Value == 0) ? null : (int?)numericUpDownRetention.Value;

        public StorageSettings(string storageName, ServiceProperties serviceProperties)
        {
            InitializeComponent();
            Icon = Bitmaps.Azure_Explorer_ico;
            labelStorageAccount.Text = string.Format(labelStorageAccount.Text, storageName);
            _serviceProperties = serviceProperties;
        }

        private void StorageVersion_Load(object sender, EventArgs e)
        {
            DpiUtils.InitPerMonitorDpi(this);

            List<string> list = new List<string>() { noversion, "2018-03-28", "2017-11-09", "2017-07-29", "2017-04-17", "2016-05-31", "2015-12-11", "2015-07-08", "2015-04-05", "2015-02-21", "2014-02-14", "2013-08-15", "2012-02-12", "2011-08-18", "2009-09-19", "2009-07-17", "2009-04-14" };
            comboBoxVersion.Items.AddRange(list.ToArray());
            comboBoxVersion.Text = _serviceProperties.DefaultServiceVersion ?? noversion;

            moreinfoLiveStreamingProfilelink.Links.Add(new LinkLabel.Link(0, moreinfoLiveStreamingProfilelink.Text.Length, Constants.LinkMoreInfoStorageVersioning));
            linkLabelStorageAnalytics.Links.Add(new LinkLabel.Link(0, linkLabelStorageAnalytics.Text.Length, Constants.LinkMoreInfoStorageAnalytics));

            comboBoxMetrics.Items.AddRange(Enum.GetNames(typeof(MetricsLevel)).ToArray()); // metrics level
            comboBoxMetrics.Text = _serviceProperties.HourMetrics.MetricsLevel.ToString();

            numericUpDownRetention.Value = _serviceProperties.HourMetrics.RetentionDays ?? 0;
        }

        private void moreinfoLiveStreamingProfilelink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Send the URL to the operating system.
            Process.Start(e.Link.LinkData as string);
        }

        private void StorageSettings_DpiChanged(object sender, DpiChangedEventArgs e)
        {
            DpiUtils.UpdatedSizeFontAfterDPIChange(labelStorageAccount, e);
        }
    }
}
