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

using System;
using System.Windows.Forms;

namespace AMSExplorer
{
    public partial class HttpSource : Form
    {
        public Uri GetURL
        {
            get => new Uri(textBoxURL.Text);

            set => textBoxURL.Text = value.ToString();
        }

        public HttpSource()
        {
            InitializeComponent();
            Icon = Bitmaps.Azure_Explorer_ico;
        }

        private void HttpSource_Load(object sender, EventArgs e)
        {
            labelURLFileNameWarning.Text = string.Empty;
        }

        private void textBoxURL_TextChanged(object sender, EventArgs e)
        {
            bool Error = false;
            try
            {
                Uri myUri = GetURL;
            }
            catch
            {
                Error = true;
                labelURLFileNameWarning.Text = AMSExplorer.Properties.Resources.ImportHttp_textBoxURL_TextChanged_ErrorDetectedInTheURL;
                buttonImport.Enabled = false;
                return;
            }

            buttonImport.Enabled = true;

            if (!Error)
            {
                labelURLFileNameWarning.Text = string.Empty;
            }
        }
    }
}