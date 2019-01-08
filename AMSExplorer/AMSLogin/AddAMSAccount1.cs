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
    public partial class AddAMSAccount1 : Form
    {

        public bool SelectUser
        {
            get
            {
                return checkBoxSelectUser.Checked;
            }
        }

        public AddAccountMode SelectedMode
        {
            get
            {
                return radioButtonAddAMSAccount.Checked ? AddAccountMode.BrowseSubscriptions : (radioButtonJsonCliOutput.Checked ? AddAccountMode.FromAzureCliJson : AddAccountMode.ManualEntry);
            }
        }


        public AddAMSAccount1()
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;

        }

        private void AddAMSAccount1_Load(object sender, EventArgs e)
        {
            comboBoxAADMappingList.Items.Add(new Item("Azure Global", nameof(AzureEnvType.Azure)));
            comboBoxAADMappingList.Items.Add(new Item("Azure China", nameof(AzureEnvType.AzureChina)));
            comboBoxAADMappingList.Items.Add(new Item("Azure US Government", nameof(AzureEnvType.AzureUSGovernment)));
            comboBoxAADMappingList.SelectedIndex = 0;

        }

        public AzureEnvironmentV3 GetEnvironment()
        {
            return new AzureEnvironmentV3((AzureEnvType)Enum.Parse(typeof(AzureEnvType), (string)(comboBoxAADMappingList.SelectedItem as Item).Value));
        }

        private void radioButtonJsonCliOutput_CheckedChanged(object sender, EventArgs e)
        {
            //comboBoxAADMappingList.Enabled = !radioButtonJsonCliOutput.Checked;
        }
    }

    public enum AddAccountMode
    {
        BrowseSubscriptions = 0,
        FromAzureCliJson,
        ManualEntry
    }
}
