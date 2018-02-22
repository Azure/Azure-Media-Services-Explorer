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
using Microsoft.WindowsAzure.MediaServices.Client;

namespace AMSExplorer
{
    public partial class TimeRangeSelection : Form
    {

        public TimeRangeValue TimeRange
        {
            get
            {
                return new TimeRangeValue(TimeRangeStartDate, TimeRangeEndDate);
            }
            set
            {
                TimeRangeStartDate = value.StartDate;
                TimeRangeEndDate = value.EndDate;
            }
        }

        public DateTime TimeRangeStartDate
        {
            get
            {
                return (DateTime)dateTimePickerStartDate.Value.ToUniversalTime();
            }
            set
            {
                dateTimePickerStartDate.Value = ((DateTime)value).ToLocalTime();
                dateTimePickerStartTime.Value = ((DateTime)value).ToLocalTime();
            }
        }


        public DateTime? TimeRangeEndDate
        {
            get
            {
                if (radioButtonEndCustom.Checked) return dateTimePickerEndDate.Value.ToUniversalTime();
                else return (Nullable<DateTime>)null;  // now
            }
            set
            {
                if (value != null)
                {
                    dateTimePickerEndDate.Value = ((DateTime)value).ToLocalTime();
                    dateTimePickerEndTime.Value = ((DateTime)value).ToLocalTime();
                    radioButtonEndCustom.Checked = true;
                }
                else
                {
                    radioButtonEndNow.Checked = true;
                }
            }
        }


        public string LabelMain
        {
            set
            {
                labelMain.Text = value;
            }
        }


        public TimeRangeSelection()
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;

            dateTimePickerEndDate.Value = DateTime.Now.Date.AddDays(1);
            dateTimePickerEndTime.Value = DateTime.Now.Date.AddDays(1);
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
