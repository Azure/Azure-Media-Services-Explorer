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
using Newtonsoft.Json.Linq;
using System.Xml.Linq;
using System.IO;

namespace AMSExplorer
{
    public partial class SoftwareUpdate : Form
    {
        Uri _urlRelNotes;
        Version _newVersion;


        public SoftwareUpdate(Uri urlRelNotes, Version newVersion)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            _urlRelNotes = urlRelNotes;
            _newVersion = newVersion;
        }

        private void SoftwareUpdate_Load(object sender, EventArgs e)
        {
            if (_urlRelNotes != null) webBrowser1.Url = _urlRelNotes;
            linkLabelMoreInfoPrice.Links.Add(new LinkLabel.Link(0, linkLabelMoreInfoPrice.Text.Length, Constants.LinkAMSE));
            label5.Text = string.Format(label5.Text, _newVersion);
        }

        private void linkLabelMoreInfoPrice_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Send the URL to the operating system.
            Process.Start(e.Link.LinkData as string);
        }
    }





}
