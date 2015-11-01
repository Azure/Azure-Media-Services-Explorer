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
using Microsoft.WindowsAzure.MediaServices.Client;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.WindowsAzure.MediaServices.Client.DynamicEncryption;
using System.Reflection;
using System.IO;

namespace AMSExplorer
{
    public partial class BulkCreateManifest : Form
    {
        private CloudMediaContext _context;
        private IIngestManifest _manifest;

        public string ManifestName
        {
            get
            {
                return textBoxManifestName.Text;
            }
        }

        public BulkCreateManifest(CloudMediaContext context)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            _context = context;
        }

        private void UploadBulk_Load(object sender, EventArgs e)
        {
        }


        private void buttonCancel_Click(object sender, EventArgs e)
        {
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {

        }
    }


}
