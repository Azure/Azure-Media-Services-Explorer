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
using Microsoft.WindowsAzure.MediaServices.Client.ContentKeyAuthorization;
using Microsoft.WindowsAzure.MediaServices.Client.DynamicEncryption;
using System.Xml;
using System.IO;
using Microsoft.WindowsAzure.MediaServices.Client.Widevine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.WindowsAzure.MediaServices.Client;

namespace AMSExplorer
{
    public partial class AddDynamicEncryptionFrame5_FairplayLicense : Form
    {

        public bool EnablePersistent
        {
            get
            {
                return radioButtonPersistent.Checked;
            }
        }

        public uint RentalDuration
        {
            get
            {
               if (checkBoxLimited.Checked)
                {
                    return (uint) numericUpDownRentalHours.Value;
                }
               else
                {
                    return 0x9999;
                }
            }
        }


        public AddDynamicEncryptionFrame5_FairplayLicense(int step = -1, int option = -1, bool laststep = true)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;

            if (step > -1 && option > -1)
            {
                this.Text = string.Format(this.Text, step);
                labelstep.Text = string.Format(labelstep.Text, step, option);
            }
            if (!laststep)
            {
                buttonOk.Text = "Next";
                buttonOk.Image = null;
            }
        }

        private void AddDynamicEncryptionFrame5_FairplayLicense_Load(object sender, EventArgs e)
        {
        }


        private void checkBoxSecLevel_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void radioButtonPersistent_CheckedChanged(object sender, EventArgs e)
        {
            panelPersistent.Enabled = radioButtonPersistent.Checked;
        }

        private void checkBoxLimited_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownRentalHours.Enabled = checkBoxLimited.Checked;
        }
    }
}
