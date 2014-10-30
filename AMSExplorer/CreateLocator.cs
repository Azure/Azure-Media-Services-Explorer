//----------------------------------------------------------------------- 
// <copyright file="CreateLocator.cs" company="Microsoft">Copyright (c) Microsoft Corporation. All rights reserved.</copyright> 
// <license>
// Azure Media Services Explorer Ver. 3.0
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
using Microsoft.WindowsAzure.MediaServices.Client;

namespace AMSExplorer
{
    public partial class CreateLocator : Form
    {

        public DateTime? LocStartDate
        {
            get
            {
                return (checkBoxStartDate.Checked) ? (DateTime)dateTimePickerStartDate.Value.ToUniversalTime() : (Nullable<DateTime>)null;
            }
            set
            {
                dateTimePickerStartDate.Value = (DateTime)value;
                dateTimePickerStartTime.Value = (DateTime)value;
            }
        }

        public bool LocHasStartDate
        {
            get { return checkBoxStartDate.Checked; }
            set { checkBoxStartDate.Checked = value; }
        }

        public DateTime LocEndDate
        {
            get
            {
                if (radioButtonEndCustom.Checked) return dateTimePickerEndDate.Value;
                else if (radioButtonEndYear.Checked) return DateTime.UtcNow.AddYears(1);
                else return DateTime.UtcNow.AddYears(100);
            }
            set
            {
                dateTimePickerEndDate.Value = value;
                dateTimePickerEndTime.Value = value;
            }
        }

        public LocatorType LocType
        {
            get
            {
                return (radioButtonOrigin.Checked) ? LocatorType.OnDemandOrigin : LocatorType.Sas;
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


        public CreateLocator(bool extendlocator = false)
        {
            InitializeComponent();
            if (extendlocator) // dialog box used to extend locator expiration date
            {
                buttonOk.Text = "Update locator(s)";
                this.Text = "Update locators";
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

        }

        private void radioButtonEndCustom_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePickerEndDate.Enabled = radioButtonEndCustom.Checked;
            dateTimePickerEndTime.Enabled = radioButtonEndCustom.Checked;
        }
    }
}
