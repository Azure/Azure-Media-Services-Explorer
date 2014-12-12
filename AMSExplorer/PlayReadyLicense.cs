//----------------------------------------------------------------------- 
// <copyright file="PlayReadyLicense.cs" company="Microsoft">Copyright (c) Microsoft Corporation. All rights reserved.</copyright> 
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
using System.Diagnostics;
using Microsoft.WindowsAzure.MediaServices.Client.ContentKeyAuthorization;
using Microsoft.WindowsAzure.MediaServices.Client.DynamicEncryption;

namespace AMSExplorer
{
    public partial class PlayReadyLicense : Form
    {
        public PlayReadyLicenseTemplate GetLicenseTemplate
        {
            get
            {
                PlayReadyLicenseTemplate licenseTemplate = new PlayReadyLicenseTemplate()
                {
                    LicenseType = (PlayReadyLicenseType)(Enum.Parse(typeof(PlayReadyLicenseType), (string)comboBoxType.SelectedItem)),
                    AllowTestDevices = checkBoxAllowTestDevices.Checked,
                };

                if (checkBoxStartDate.Checked) licenseTemplate.BeginDate = (DateTime)dateTimePickerStartDate.Value.ToUniversalTime();
                if (checkBoxEndDate.Checked) licenseTemplate.ExpirationDate = (DateTime)dateTimePickerEndDate.Value.ToUniversalTime();
                if (checkBoxFPExp.Checked) licenseTemplate.PlayRight.FirstPlayExpiration = (TimeSpan)new TimeSpan((int)numericUpDownFPExpDays.Value, (int)numericUpDownFPExpHours.Value, (int)numericUpDownFPExpMinutes.Value, 0);

                if (licenseTemplate.PlayRight == null) licenseTemplate.PlayRight = new PlayReadyPlayRight();
                if (checkBoxCompressedDigitalAudioOPL.Checked) licenseTemplate.PlayRight.CompressedDigitalAudioOpl = (int)numericUpDownCompressedDigitalAudioOPL.Value;
                if (checkBoxCompressedDigitalVideoOPL.Checked) licenseTemplate.PlayRight.CompressedDigitalVideoOpl = (int)numericUpDownCompressedDigitalVideoOPL.Value;
                if (checkBoxUncompressedDigitalAudioOPL.Checked) licenseTemplate.PlayRight.UncompressedDigitalAudioOpl = (int)numericUpDownUncompressedDigitalAudioOPL.Value;
                if (checkBoxUncompressedDigitalVideoOPL.Checked) licenseTemplate.PlayRight.UncompressedDigitalVideoOpl = (int)numericUpDownUncompressedDigitalVideoOPL.Value;
                if (checkBoxAnalogVideoOPL.Checked) licenseTemplate.PlayRight.AnalogVideoOpl = (int)numericUpDownAnalogVideoOPL.Value;

                licenseTemplate.PlayRight.DigitalVideoOnlyContentRestriction = checkBoxDigitalVideoOnlyContentRestriction.Checked;
                licenseTemplate.PlayRight.ImageConstraintForAnalogComponentVideoRestriction = checkBoxImageConstraintForAnalogComponentVideoRestriction.Checked;
                licenseTemplate.PlayRight.ImageConstraintForAnalogComputerMonitorRestriction = checkBoxImageConstraintForAnalogComponentVideoRestriction.Checked;

                licenseTemplate.PlayRight.AllowPassingVideoContentToUnknownOutput = (UnknownOutputPassingOption)(Enum.Parse(typeof(UnknownOutputPassingOption), (string)comboBoxAllowPassingVideoContentUnknownOutput.SelectedItem));
                return licenseTemplate;
            }

        }



        public string PlayReadyPolicyName
        {
            get
            {
                return textBoxPolicyName.Text;
            }
            set
            {
                textBoxPolicyName.Text = value;
            }
        }



        public DateTime? PlayReadyLicStartDate
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



        public DateTime? PlayReadyLicEndDate
        {
            get
            {
                return (checkBoxEndDate.Checked) ? (DateTime)dateTimePickerEndDate.Value.ToUniversalTime() : (Nullable<DateTime>)null;
            }
            set
            {
                dateTimePickerEndDate.Value = (DateTime)value;
                dateTimePickerEndTime.Value = (DateTime)value;
            }
        }




        public PlayReadyLicense()
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
        }



        private void moreinfotestserver_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData as string);
        }

        private void PlayReadyLicense_Load(object sender, EventArgs e)
        {
            moreinfocompliance.Links.Add(new LinkLabel.Link(0, moreinfocompliance.Text.Length, "http://www.microsoft.com/playready/licensing/compliance/"));

            comboBoxType.Items.AddRange(Enum.GetNames(typeof(PlayReadyLicenseType)).ToArray()); // license type
            comboBoxType.SelectedItem = Enum.GetName(typeof(PlayReadyLicenseType), PlayReadyLicenseType.Nonpersistent);


            comboBoxAllowPassingVideoContentUnknownOutput.Items.AddRange(Enum.GetNames(typeof(UnknownOutputPassingOption)).ToArray());
            comboBoxAllowPassingVideoContentUnknownOutput.SelectedItem = Enum.GetName(typeof(UnknownOutputPassingOption), UnknownOutputPassingOption.NotAllowed);

        }




        private void checkBoxStartDate_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePickerStartDate.Enabled = checkBoxStartDate.Checked;
            dateTimePickerStartTime.Enabled = checkBoxStartDate.Checked;
            value_SelectedIndexChanged(sender, e);
        }

        private void checkBoxEndDate_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePickerEndDate.Enabled = checkBoxEndDate.Checked;
            dateTimePickerEndTime.Enabled = checkBoxEndDate.Checked;
            value_SelectedIndexChanged(sender, e);
        }

        private void dateTimePickerStartDate_ValueChanged(object sender, EventArgs e)
        {
            dateTimePickerStartTime.Value = dateTimePickerStartDate.Value;
            value_SelectedIndexChanged(sender, e);
        }

        private void dateTimePickerStartTime_ValueChanged(object sender, EventArgs e)
        {
            dateTimePickerStartDate.Value = dateTimePickerStartTime.Value;
            value_SelectedIndexChanged(sender, e);
        }

        private void dateTimePickerEndDate_ValueChanged(object sender, EventArgs e)
        {
            dateTimePickerEndTime.Value = dateTimePickerEndDate.Value;
            value_SelectedIndexChanged(sender, e);
        }

        private void dateTimePickerEndTime_ValueChanged(object sender, EventArgs e)
        {
            dateTimePickerEndDate.Value = dateTimePickerEndTime.Value;
            value_SelectedIndexChanged(sender, e);
        }

        private void value_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool Error = false;
            try
            {
                PlayReadyLicenseTemplate plt = this.GetLicenseTemplate;
            }
            catch (Exception ex)
            {
                labelWarning.Text = Program.GetErrorMessage(ex);
                Error = true;
            }

            if (!Error) labelWarning.Text = string.Empty;

        }

        private void checkBoxCompressedDigitalAudioOPL_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownCompressedDigitalAudioOPL.Enabled = checkBoxCompressedDigitalAudioOPL.Checked;
            value_SelectedIndexChanged(sender, e);
        }

        private void checkBoxCompressedDigitalVideoOPL_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownCompressedDigitalVideoOPL.Enabled = checkBoxCompressedDigitalVideoOPL.Checked;
            value_SelectedIndexChanged(sender, e);
        }

        private void checkBoxUncompressedDigitalAudioOPL_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownUncompressedDigitalAudioOPL.Enabled = checkBoxUncompressedDigitalAudioOPL.Checked;
            value_SelectedIndexChanged(sender, e);
        }

        private void checkBoxUncompressedDigitalVideoOPL_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownUncompressedDigitalVideoOPL.Enabled = checkBoxUncompressedDigitalVideoOPL.Checked;
            value_SelectedIndexChanged(sender, e);
        }

        private void checkBoxAnalogVideoOPL_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownAnalogVideoOPL.Enabled = checkBoxAnalogVideoOPL.Checked;
            value_SelectedIndexChanged(sender, e);
        }

        private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((string)comboBoxType.SelectedItem == Enum.GetName(typeof(PlayReadyLicenseType), PlayReadyLicenseType.Nonpersistent))  // Non presistent
            {
                groupBoxTimeValues.Enabled = false;
            }
            else
            {
                groupBoxTimeValues.Enabled = true;
            }
            value_SelectedIndexChanged(sender, e);

        }

        private void checkBoxFPExp_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownFPExpDays.Enabled = checkBoxFPExp.Checked;
            numericUpDownFPExpHours.Enabled = checkBoxFPExp.Checked;
            numericUpDownFPExpMinutes.Enabled = checkBoxFPExp.Checked;
            value_SelectedIndexChanged(sender, e);
        }
    }
}
