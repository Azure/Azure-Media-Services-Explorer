//----------------------------------------------------------------------- 
// <copyright file="ChangeEncodingRU.cs" company="Microsoft">Copyright (c) Microsoft Corporation. All rights reserved.</copyright> 
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
using System.Diagnostics;

namespace AMSExplorer
{
    public partial class ChangeEncodingRU : Form
    {
        public string GetAzureSubscriptionID
        {
            get
            {
                return textBoxSubId.Text;
            }
        }

      
        public string GetCertThumbprint
        {
            get
            {
                return textBoxCertThumbprint.Text;
            }
        }

        public int RUNumber
        {
            get
            {
                return (int) numericUpDownRUNumber.Value;
            }
            set
            {
                numericUpDownRUNumber.Value = value;
            }

        }

      


        public ChangeEncodingRU()
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            linkLabelAttach.Links.Add(new LinkLabel.Link(0, linkLabelAttach.Text.Length, "http://msdn.microsoft.com/en-US/library/azure/gg551722.aspx"));
        }

        private void AttachStorage_Load(object sender, EventArgs e)
        {
          
        }

        private void textBoxURL_TextChanged(object sender, EventArgs e)
        {
            /*
            string filename = null;
            bool Error = false;
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
                textBoxStorageName.Text = filename;
                textBoxStorageKey.Text = filename;
            }
             * */
        }

        private void linkLabelAttach_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData as string);
        }
    }
}
