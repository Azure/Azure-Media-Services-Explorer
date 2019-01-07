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
    public partial class HLSAESStatic : Form
    {

        public string HLSLabel
        {
            get
            {
                return labelAssetName.Text;
            }
            set
            {
                labelAssetName.Text = value;
            }
        }

        public bool HLSEncrypt
        {
            get
            {
                return checkBoxEncrypt.Checked;
            }
            set
            {
                checkBoxEncrypt.Checked = value;
            }
        }

        public string HLSKey
        {
            get
            {
                return textBoxkey.Text;
            }
            set
            {
                textBoxkey.Text = value;
            }
        }
        public string HLSKeyURL
        {
            get
            {
                return textBoxKeyURL.Text;
            }
            set
            {
                textBoxKeyURL.Text = value;
            }
        }
        public string HLSMaxBitrate
        {
            get
            {
                return textBoxMaxBitrate.Text;
            }
            set
            {
                textBoxMaxBitrate.Text = value;
            }
        }
        public string HLSServiceSegment
        {
            get
            {
                return textBoxServiceSegment.Text;
            }
            set
            {
                textBoxServiceSegment.Text = value;
            }
        }
        public string HLSOutputAssetName
        {
            get
            {
                return textboxoutputassetname.Text;
            }
            set
            {
                textboxoutputassetname.Text = value;
            }
        }
        public string HLSProcessorLabel
        {
            get
            {
                return processorlabel.Text;
            }
            set
            {
                processorlabel.Text = value;
            }
        }


        public HLSAESStatic()
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
        }

        private void buttonGenKeyID_Click(object sender, EventArgs e)
        {
            byte[] keyValue = GetRandomBuffer(16);
            textBoxkey.Text = Convert.ToBase64String(keyValue);
        }


        private void checkBoxEncrypt_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            buttonGenKey.Enabled = cb.Checked;
            textBoxkey.Enabled = cb.Checked;
            textBoxKeyURL.Enabled = cb.Checked;
        }

        private static byte[] GetRandomBuffer(int length)
        {
            var returnValue = new byte[length];

            using (var rng =
                new System.Security.Cryptography.RNGCryptoServiceProvider())
            {
                rng.GetBytes(returnValue);
            }

            return returnValue;
        }

    }
}
