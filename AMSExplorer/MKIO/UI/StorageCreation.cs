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


using Microsoft.IdentityModel.Tokens;
using MK.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AMSExplorer
{
    public partial class StorageCreation : Form
    {
        public string AccessKey
        {
            get => textBoxAccessKey.Text;
        }

        public string StorageDescription
        {
            get => string.IsNullOrWhiteSpace(textBoxDescription.Text) ? null : textBoxDescription.Text;
            set => textBoxDescription.Text = value;
        }

        public int SASDurationInMonths
        {
            get => (int)numericUpDownSASValidity.Value;
            set => numericUpDownSASValidity.Value = value;
        }

        public string StorageName
        {
            set => textBoxStorage.Text = value;
        }

        public string StorageRegion
        {
            set => textBoxRegion.Text = value;
        }

        public StorageCreation()
        {
            InitializeComponent();
        }

        private void StorageCreation_Load(object sender, System.EventArgs e)
        {

        }



        private void StorageCreation_Shown(object sender, System.EventArgs e)
        {
            Telemetry.TrackPageView(this.Name);
        }

        private void buttonOk_Click(object sender, System.EventArgs e)
        {

        }

        private void textBoxAccessKey_TextChanged(object sender, System.EventArgs e)
        {
            buttonOk.Enabled = !string.IsNullOrWhiteSpace(textBoxAccessKey.Text);
        }
    }
}