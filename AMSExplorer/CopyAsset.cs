//----------------------------------------------------------------------- 
// <copyright file="CopyAsset.cs" company="Microsoft">Copyright (c) Microsoft Corporation. All rights reserved.</copyright> 
// <license>
// Azure Media Services Explorer Ver. 3.1
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
using System.Collections.Specialized;
using System.Xml;
using System.Xml.Linq;
using System.Diagnostics;
using System.Configuration;
using Microsoft.WindowsAzure.MediaServices.Client;

namespace AMSExplorer
{
    public partial class CopyAsset : Form
    {

        StringCollection CredentialsList;
        CredentialsEntry SelectedCredentials;

        public bool IsLiveAsset
        {
            get
            {
                return (radioButtonOnDemandAsset.Checked) ? false : true;
            }

        }

        public CredentialsEntry LoginCredentials
        {
            get
            {
                return SelectedCredentials;
            }
        }




        public CopyAsset()
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;

        }



        private void CopyAsset_Load(object sender, EventArgs e)
        {
            CredentialsList = Properties.Settings.Default.LoginList;

            if (CredentialsList != null)
            {
                for (int i = 0; i < (CredentialsList.Count / CredentialsEntry.StringsCount); i++)
                {
                    //if (!string.IsNullOrEmpty(CredentialsList[i * CredentialsEntry.StringsCount+2]))
                    {
                        listBoxAcounts.Items.Add(CredentialsList[i * CredentialsEntry.StringsCount]);

                    }

                }

            }
            else
            {

            }


        }


        private void UpdateLocatorGUID_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void listBoxAcounts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxAcounts.SelectedIndex > -1) // one selected
            {
                SelectedCredentials = new CredentialsEntry(
                   CredentialsList[listBoxAcounts.SelectedIndex * CredentialsEntry.StringsCount],
                   CredentialsList[listBoxAcounts.SelectedIndex * CredentialsEntry.StringsCount + 1],
                   CredentialsList[listBoxAcounts.SelectedIndex * CredentialsEntry.StringsCount + 2],
                   CredentialsList[listBoxAcounts.SelectedIndex * CredentialsEntry.StringsCount + 3],
                   CredentialsList[listBoxAcounts.SelectedIndex * CredentialsEntry.StringsCount + 4],
                   CredentialsList[listBoxAcounts.SelectedIndex * CredentialsEntry.StringsCount + 5],
                   CredentialsList[listBoxAcounts.SelectedIndex * CredentialsEntry.StringsCount + 6],
                   CredentialsList[listBoxAcounts.SelectedIndex * CredentialsEntry.StringsCount + 7],
                   CredentialsList[listBoxAcounts.SelectedIndex * CredentialsEntry.StringsCount + 8],
                   CredentialsList[listBoxAcounts.SelectedIndex * CredentialsEntry.StringsCount + 9]
                    );

                labelDescription.Text = CredentialsList[listBoxAcounts.SelectedIndex * CredentialsEntry.StringsCount + 3];
            }
        }
    }
}
