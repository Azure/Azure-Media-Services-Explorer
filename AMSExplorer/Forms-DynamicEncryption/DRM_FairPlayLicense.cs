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

using Microsoft.Azure.Management.Media.Models;
using System;
using System.Windows.Forms;

namespace AMSExplorer
{
    public partial class DRM_FairPlayLicense : Form
    {
        public string FairPlayePolicyName
        {
            get => string.IsNullOrWhiteSpace(textBoxPolicyName.Text) ? null : textBoxPolicyName.Text;
            set => textBoxPolicyName.Text = value;
        }


        public long RentalDuration
        {
            get
            {
               if (checkBoxLimited.Checked)
                {
                    return (long) numericUpDownRentalHours.Value;
                }
               else
                {
                    return 0x9999;
                }
            }
        }

        public ContentKeyPolicyFairPlayRentalAndLeaseKeyType FairPlayRentalAndLeaseKeyType
        {
            get
            {
                if (radioButtonPersistent.Checked)
                {
                    if (checkBoxLimited.Checked)
                    {
                        return ContentKeyPolicyFairPlayRentalAndLeaseKeyType.PersistentLimited;
                    }
                    else
                    {
                        return ContentKeyPolicyFairPlayRentalAndLeaseKeyType.PersistentUnlimited;
                    }
                }
                else
                {
                    return ContentKeyPolicyFairPlayRentalAndLeaseKeyType.Undefined;
                }
            }
        }


        public DRM_FairPlayLicense(int step = -1, int option = -1, bool laststep = true)
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

            textBoxPolicyName.Text = $"FairPlay-Option-{option}";
        }

        private void DRM_FairPlayLicense_Load(object sender, EventArgs e)
        {
            DpiUtils.InitPerMonitorDpi(this);
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

        private void DRM_FairPlayLicense_DpiChanged(object sender, DpiChangedEventArgs e)
        {
            DpiUtils.UpdatedSizeFontAfterDPIChange(labelstep, e);
        }
    }
}
