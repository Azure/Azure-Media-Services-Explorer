//----------------------------------------------------------------------- 
// <copyright file="ImportHttp.cs" company="Microsoft">Copyright (c) Microsoft Corporation. All rights reserved.</copyright> 
// <license>
// Azure Media Services Explorer Ver. 3.0
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

namespace AMSExplorer
{
    public partial class ImportHttp : Form
    {
        public Uri GetURL
        {
            get
            {
                return new Uri(textBoxURL.Text);
            }

            set
            {
                textBoxURL.Text = value.ToString();
            }
        }

        public string GetAssetFileName
        {
            get
            {
                return textBoxAssetFileName.Text;
            }

        }

        public string GetAssetName
        {
            get
            {
                return textBoxAssetName.Text;
            }

        }


        public ImportHttp()
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
        }

        private void ImportHttp_Load(object sender, EventArgs e)
        {
            labelURLFileNameWarning.Text = string.Empty;
        }

        private void textBoxURL_TextChanged(object sender, EventArgs e)
        {
            string filename = null;
            bool Error = false;
            try
            {
                Uri myUri = this.GetURL;
            }
            catch
            {
                Error = true;
                labelURLFileNameWarning.Text = "Error detected in the URL";
                buttonImport.Enabled = false;
                return;
            }
           
            buttonImport.Enabled = true;
            try
            {
                filename = System.IO.Path.GetFileName(this.GetURL.LocalPath);
            }
            catch
            {
                Error = true;
                labelURLFileNameWarning.Text = "File name not found in the URL";
            }

            if (!Error)
            {
                labelURLFileNameWarning.Text = string.Empty;
                textBoxAssetName.Text = filename;
                textBoxAssetFileName.Text = filename;
                
            }
        }
    }
}
