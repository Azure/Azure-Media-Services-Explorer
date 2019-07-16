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

using Microsoft.Azure.Management.Media;
using Microsoft.Azure.Management.Media.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AMSExplorer
{
    public partial class UploadOptions : Form
    {
        private AMSClientV3 _amsClientV3;

        public string StorageSelected
        {
            get
            {
                return ((Item)comboBoxStorage.SelectedItem).Value;
            }
        }

        public bool SingleAsset
        {
            get
            {
                return radioButtonSingleAsset.Checked;
            }
        }

        public int BlockSize
        {
            get
            {
                int x = 4;
                bool success = Int32.TryParse((string)comboBoxBlockSize.Text, out x);
                return success ? x : 4;
            }
        }


        public UploadOptions(AMSClientV3 amsClient, bool multifilesMode)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            _amsClientV3 = amsClient;

            ControlsResetToDefault();

            if (multifilesMode)
            {
                groupBoxMultifiles.Visible = true;
            }

            var listInt = new List<int>() { 1, 2, 4, 8, 16, 32, 64 };
            comboBoxBlockSize.Items.Clear();
            listInt.ForEach(l => comboBoxBlockSize.Items.Add(l.ToString()));
            comboBoxBlockSize.SelectedIndex = 3;
        }

        private void ControlsResetToDefault()
        {
            _amsClientV3.RefreshTokenIfNeeded();
            var storAccounts = _amsClientV3.AMSclient.Mediaservices.Get(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName).StorageAccounts;

            comboBoxStorage.Items.Clear();
            foreach (var storage in storAccounts)
            {
                string sname = AMSClientV3.GetStorageName(storage.Id);
                bool primary = (storage.Type == StorageAccountType.Primary);
                comboBoxStorage.Items.Add(new Item(string.Format("{0} {1}", sname, primary ? "(primary)" : string.Empty), sname));
                if (primary) comboBoxStorage.SelectedIndex = comboBoxStorage.Items.Count - 1;
            }
        }
    }
}