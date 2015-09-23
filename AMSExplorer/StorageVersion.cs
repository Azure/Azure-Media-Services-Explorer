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
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AMSExplorer
{
    public partial class StorageVersion : Form
    {
        const string noversion = "(not defined)";

        public string RequestedStorageVersion
        {
            get
            { 
                return (comboBoxVersion.Text == noversion) ? null:comboBoxVersion.Text;
            }

            set
            {
                comboBoxVersion.Text = value;
            }
        }
        public StorageVersion(string storageName, string currentVersion)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            textBoxVersion.Text = currentVersion;
            labelStorageAccount.Text = string.Format(labelStorageAccount.Text, storageName);
        }

        private void StorageVersion_Load(object sender, EventArgs e)
        {
            var list = new List<string>() { noversion, "2015-02-21", "2014-02-14", "2013-08-15", "2012-02-12", "2011-08-18", "2009-09-19", "2009-07-17", "2009-04-14" };
            comboBoxVersion.Items.AddRange(list.ToArray());
            comboBoxVersion.SelectedIndex = 4;

            moreinfoLiveStreamingProfilelink.Links.Add(new LinkLabel.Link(0, moreinfoLiveStreamingProfilelink.Text.Length, Constants.LinkMoreInfoStorageVersioning));
        }

        private void moreinfoLiveStreamingProfilelink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Send the URL to the operating system.
            Process.Start(e.Link.LinkData as string);
        }
    }
}
