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
using Microsoft.Azure.Management.Media;

namespace AMSExplorer
{
    public partial class CreateLocator : Form
    {
        public DateTime? LocatorStartDate
        {
            get
            {
                return (checkBoxStartDate.Checked) ? (DateTime)dateTimePickerStartDate.Value.ToUniversalTime() : (Nullable<DateTime>)null;
            }
            set
            {
                dateTimePickerStartDate.Value = ((DateTime)value).ToLocalTime();
                dateTimePickerStartTime.Value = ((DateTime)value).ToLocalTime();
            }
        }

        public bool LocatorHasStartDate
        {
            get { return checkBoxStartDate.Checked; }
            set { checkBoxStartDate.Checked = value; }
        }

        public DateTime LocatorEndDate
        {
            get
            {
                if (radioButtonEndCustom.Checked) return dateTimePickerEndDate.Value.ToUniversalTime();
                else if (radioButtonEndYear.Checked) return DateTime.UtcNow.AddYears(1);
                else return DateTime.UtcNow.AddYears(100);
            }
            set
            {
                dateTimePickerEndDate.Value = ((DateTime)value).ToLocalTime();
                dateTimePickerEndTime.Value = ((DateTime)value).ToLocalTime();
            }
        }

    

        public string ForceLocatorGuid
        {
            get
            {
                if (radioButtonOrigin.Checked && checkBoxForLocatorGUID.Checked)
                {
                    return textBoxLocatorGUID.Text;
                }
                else
                {
                    return null;
                }
            }
        }

        public string LocAssetName
        {
            set
            {
                labelAssetName.Text = value;
            }
        }

        public string LocWarning
        {
            set
            {
                labelWarning.Text = value;
            }
        }

        public string StreamingPolicyName
        {
            get
            {
                return (string)comboBoxPolicyName.Text;
            }
        }

        public CreateLocator(bool extendlocator = false)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            if (extendlocator) // dialog box used to extend locator expiration date
            {
                buttonOk.Text = AMSExplorer.Properties.Resources.CreateLocator_CreateLocator_UpdateLocatorS;
                this.Text = AMSExplorer.Properties.Resources.CreateLocator_CreateLocator_UpdateLocators2;
                radioButtonSAS.Enabled = false; // only streaming locator can be updated
                groupBox2.Enabled = false; // do not propose to specificy start date
            }
        }

        private void dateTimePickerStartDate_ValueChanged(object sender, EventArgs e)
        {
            dateTimePickerStartTime.Value = dateTimePickerStartDate.Value;
        }

        private void dateTimePickerStartTime_ValueChanged(object sender, EventArgs e)
        {
            dateTimePickerStartDate.Value = dateTimePickerStartTime.Value;
        }

        private void dateTimePickerEndDate_ValueChanged(object sender, EventArgs e)
        {
            dateTimePickerEndTime.Value = dateTimePickerEndDate.Value;
        }

        private void dateTimePickerEndTime_ValueChanged(object sender, EventArgs e)
        {
            dateTimePickerEndDate.Value = dateTimePickerEndTime.Value;
        }

        private void checkBoxStartDate_CheckedChanged_1(object sender, EventArgs e)
        {
            dateTimePickerStartDate.Enabled = checkBoxStartDate.Checked;
            dateTimePickerStartTime.Enabled = checkBoxStartDate.Checked;
        }

        private void CreateLocator_Load(object sender, EventArgs e)
        {
            // as not implemented, we remove MultiDrm policies for now
            comboBoxPolicyName.Items.AddRange(typeof(PredefinedStreamingPolicy).GetFields().Where(field => !field.GetValue(field).ToString().Contains("MultiDrm")).Select(field => field.GetValue(field)).ToArray());
            comboBoxPolicyName.Text = PredefinedStreamingPolicy.ClearStreamingOnly;
        }

        private void radioButtonEndCustom_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePickerEndDate.Enabled = radioButtonEndCustom.Checked;
            dateTimePickerEndTime.Enabled = radioButtonEndCustom.Checked;
        }

        private void UpdateLocatorGUID_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxForceLocator.Visible = radioButtonOrigin.Checked;
            textBoxLocatorGUID.Enabled = checkBoxForLocatorGUID.Checked;
        }
    }
}
