//----------------------------------------------------------------------------------------------
//    Copyright 2020 Microsoft Corporation
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
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AMSExplorer
{
    public partial class CreateStreamingEndpoint : Form
    {
        public string StreamingEndpointName
        {
            get => textboxSEName.Text;
            set => textboxSEName.Text = value;
        }
        public string StreamingEndpointDescription
        {
            get => textBoxOriginDescription.Text;
            set => textBoxOriginDescription.Text = value;
        }

        public int scaleUnits
        {
            get
            {
                if (radioButtonPremium.Checked)
                {
                    return (int)numericUpDownUnits.Value;
                }
                else
                {
                    return 0;
                }
            }
        }

        public bool EnableAzureCDN => checkBoxEnableAzureCDN.Checked;

        public CreateStreamingEndpoint()
        {
            InitializeComponent();
            Icon = Bitmaps.Azure_Explorer_ico;
        }


        private void CreateStreamingEndpoint_Load(object sender, EventArgs e)
        {
            checkSEName();
            moreinfoSE.Links.Add(new LinkLabel.Link(0, moreinfoSE.Text.Length, Constants.LinkMoreInfoSE));

        }

        private void buttonOk_Click(object sender, EventArgs e)
        {

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {

        }


        private void checkSEName()
        {
            TextBox tb = textboxSEName;

            if (!IsSENameValid(tb.Text))
            {
                errorProvider1.SetError(tb, AMSExplorer.Properties.Resources.CreateStreamingEndpoint_checkSEName_StreamingEndpointNameIsNotValid);
            }
            else
            {
                errorProvider1.SetError(tb, string.Empty);
            }
        }

        internal static bool IsSENameValid(string name)
        {
            Regex reg = new Regex(@"^[a-zA-Z0-9]+(-*[a-zA-Z0-9])*$", RegexOptions.Compiled);
            return (name.Length > 0 && name.Length < 25 && reg.IsMatch(name));
        }

        private void textboxSEName_TextChanged(object sender, EventArgs e)
        {
            checkSEName();
        }

        private void radioButtonPremium_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownUnits.Enabled = radioButtonPremium.Checked;
        }

        private void moreinfoame_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData as string);

        }
    }
}
