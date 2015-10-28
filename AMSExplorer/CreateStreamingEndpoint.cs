﻿//----------------------------------------------------------------------------------------------
//    Copyright 2015 Microsoft Corporation
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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AMSExplorer
{
    public partial class CreateStreamingEndpoint : Form
    {
        public string StreamingEndpointName
        {
            get { return textboxSEName.Text; }
            set { textboxSEName.Text = value; }
        }
        public string StreamingEndpointDescription
        {
            get { return textBoxOriginDescription.Text; }
            set { textBoxOriginDescription.Text = value; }
        }

        public int scaleUnits
        {
            get { return Convert.ToInt32(numericUpDownRU.Value); }
            set { numericUpDownRU.Value = value; }
        }

        public bool EnableAzureCDN
        {
            get
            {
                return checkBoxEnableAzureCDN.Checked;
            }
        }

        public CreateStreamingEndpoint()
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
        }


        private void CreateStreamingEndpoint_Load(object sender, EventArgs e)
        {
            checkSEName();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDownRU_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDownRU.Value==0)
            {
                checkBoxEnableAzureCDN.Enabled = false;
                checkBoxEnableAzureCDN.Checked = false;
            }
            else
            {
                checkBoxEnableAzureCDN.Enabled = true;
            }
        }

        private void checkSEName()
        {
            TextBox tb = textboxSEName;

            if (!IsSENameValid(tb.Text))
            {
                errorProvider1.SetError(tb, "Streaming Endpoint name is not valid");
            }
            else
            {
                errorProvider1.SetError(tb, String.Empty);
            }
        }

        internal static bool IsSENameValid(string name)
        {
            Regex reg = new Regex(@"^[a-zA-Z0-9]([a-zA-Z0-9-]{0,30}[a-zA-Z0-9])?$", RegexOptions.Compiled);
            return (reg.IsMatch(name));
        }

        private void textboxSEName_TextChanged(object sender, EventArgs e)
        {
            checkSEName();
        }
    }
}
