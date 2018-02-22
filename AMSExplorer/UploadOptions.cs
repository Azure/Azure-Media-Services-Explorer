//----------------------------------------------------------------------------------------------
//    Copyright 2018 Microsoft Corporation
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


namespace AMSExplorer
{
    public partial class UploadOptions : Form
    {
        CloudMediaContext context;
        
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


        public AssetCreationOptions AssetCreationOptions
        {
            get
            {
                return checkBoxUseStorageEncryption.Checked ? AssetCreationOptions.StorageEncrypted : AssetCreationOptions.None;
            }
            set
            {
                checkBoxUseStorageEncryption.Checked = value == AssetCreationOptions.StorageEncrypted;
            }

        }


        public UploadOptions(CloudMediaContext myContext, bool multifilesMode)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            context = myContext;

            ControlsResetToDefault();

            if (multifilesMode)
            {
                groupBoxMultifiles.Visible = true;
            }
        }

        private void ControlsResetToDefault()
        {
            comboBoxStorage.Items.Clear();
            foreach (var storage in context.StorageAccounts)
            {
                comboBoxStorage.Items.Add(new Item(string.Format("{0} {1}", storage.Name, storage.IsDefault ? "(default)" : ""), storage.Name));
                if (storage.Name == context.DefaultStorageAccount.Name) comboBoxStorage.SelectedIndex = comboBoxStorage.Items.Count - 1;
            }

            checkBoxUseStorageEncryption.Checked = Properties.Settings.Default.useStorageEncryption;
        }


        private void UploadOptions_Load(object sender, EventArgs e)
        {

        }

    }

}
