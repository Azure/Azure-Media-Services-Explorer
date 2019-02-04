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
using System.Diagnostics;
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
            AzureEnvType[] envs = new AzureEnvType[] { AzureEnvType.Azure, AzureEnvType.AzureChina, AzureEnvType.AzureUSGovernment, AzureEnvType.AzureGermany, AzureEnvType.DevTest };

            foreach (var env in envs)
            {
                comboBoxAADMappingList.Items.Add(new Item((new AzureEnvironmentV3(env)).DisplayName, env.ToString()));
            }

            comboBoxAADMappingList.SelectedIndex = 0;

           linkLabelAzCliDoc.Links.Add(new LinkLabel.Link(0, linkLabelAzCliDoc.Text.Length, Constants.LinkAMSAzCli));


        }

        public AzureEnvironmentV3 GetEnvironment()
        {
            return new AzureEnvironmentV3((AzureEnvType)Enum.Parse(typeof(AzureEnvType), (string)(comboBoxAADMappingList.SelectedItem as Item).Value));
        }

        private void radioButtonJsonCliOutput_CheckedChanged(object sender, EventArgs e)
        {
            panelEnv.Visible = !radioButtonJsonCliOutput.Checked;
        }

        private void linkLabelAzCliDoc_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData as string);
        }
    }

    public enum AddAccountMode
    {
        BrowseSubscriptions = 0,
        FromAzureCliJson,
        ManualEntry
    }
}
