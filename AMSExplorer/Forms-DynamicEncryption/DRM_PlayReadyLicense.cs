//----------------------------------------------------------------------------------------------
//    Copyright 2021 Microsoft Corporation
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
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace AMSExplorer
{
    public partial class DRM_PlayReadyLicense : Form
    {

        public ContentKeyPolicyPlayReadyConfiguration GetPlayReadyConfiguration
        {
            get
            {

                ContentKeyPolicyPlayReadyLicense objContentKeyPolicyPlayReadyLicense;

                /*
                objContentKeyPolicyPlayReadyLicense = new ContentKeyPolicyPlayReadyLicense
                {
                    AllowTestDevices = true,
                    BeginDate = new DateTime(2016, 1, 1),
                    ContentKeyLocation = new ContentKeyPolicyPlayReadyContentEncryptionKeyFromHeader(),
                    ContentType = ContentKeyPolicyPlayReadyContentType.UltraVioletStreaming,
                    LicenseType = ContentKeyPolicyPlayReadyLicenseType.Persistent,
                    PlayRight = new ContentKeyPolicyPlayReadyPlayRight
                    {
                        ImageConstraintForAnalogComponentVideoRestriction = true,
                        ExplicitAnalogTelevisionOutputRestriction = new ContentKeyPolicyPlayReadyExplicitAnalogTelevisionRestriction(true, 2),
                        AllowPassingVideoContentToUnknownOutput = ContentKeyPolicyPlayReadyUnknownOutputPassingOption.Allowed
                    }
                };
                */


                objContentKeyPolicyPlayReadyLicense = new ContentKeyPolicyPlayReadyLicense()
                {
                    AllowTestDevices = checkBoxAllowTestDevices.Checked,
                    ContentKeyLocation = new ContentKeyPolicyPlayReadyContentEncryptionKeyFromHeader(),

                };

                ContentKeyPolicyPlayReadyConfiguration objContentKeyPolicyPlayReadyConfiguration = new ContentKeyPolicyPlayReadyConfiguration
                {
                    Licenses = new List<ContentKeyPolicyPlayReadyLicense> { objContentKeyPolicyPlayReadyLicense }
                };

                if (comboBoxLicenseType.SelectedItem != null)
                {
                    if (comboBoxLicenseType.SelectedItem.ToString() == ContentKeyPolicyPlayReadyLicenseType.NonPersistent)
                    {
                        objContentKeyPolicyPlayReadyLicense.LicenseType = ContentKeyPolicyPlayReadyLicenseType.NonPersistent;
                    }
                    else if (comboBoxLicenseType.SelectedItem.ToString() == ContentKeyPolicyPlayReadyLicenseType.Persistent)
                    {
                        objContentKeyPolicyPlayReadyLicense.LicenseType = ContentKeyPolicyPlayReadyLicenseType.Persistent;
                    }
                    else
                    {
                        objContentKeyPolicyPlayReadyLicense.LicenseType = ContentKeyPolicyPlayReadyLicenseType.Unknown;
                    }
                }

                if (checkBoxStartDate.Checked)
                {
                    if (radioButtonStartDateAbsolute.Checked)
                    {
                        objContentKeyPolicyPlayReadyLicense.BeginDate = dateTimePickerStartDate.Value.ToUniversalTime();
                    }
                    else // Relative
                    {
                        objContentKeyPolicyPlayReadyLicense.RelativeBeginDate = new TimeSpan((int)numericUpDownStartDateDays.Value, (int)numericUpDownStartDateHours.Value, (int)numericUpDownStartDateMinutes.Value, 0);
                    }
                }

                if (checkBoxEndDate.Checked)
                {
                    if (radioButtonEndDateAbsolute.Checked)
                    {
                        objContentKeyPolicyPlayReadyLicense.ExpirationDate = dateTimePickerEndDate.Value.ToUniversalTime();
                    }
                    else // Relative
                    {
                        objContentKeyPolicyPlayReadyLicense.RelativeExpirationDate = new TimeSpan((int)numericUpDownEndDateDays.Value, (int)numericUpDownEndDateHours.Value, (int)numericUpDownEndDateMinutes.Value, 0);
                    }
                }

                // Generate PlayRight
                if (objContentKeyPolicyPlayReadyLicense.PlayRight == null)
                {
                    objContentKeyPolicyPlayReadyLicense.PlayRight = new ContentKeyPolicyPlayReadyPlayRight();
                }

                if (checkBoxFPExp.Checked)
                {
                    objContentKeyPolicyPlayReadyLicense.PlayRight.FirstPlayExpiration = new TimeSpan((int)numericUpDownFPExpDays.Value, (int)numericUpDownFPExpHours.Value, (int)numericUpDownFPExpMinutes.Value, 0);
                }

                if (checkBoxGrace.Checked)
                {
                    objContentKeyPolicyPlayReadyLicense.GracePeriod = new TimeSpan((int)numericUpDownGraceDays.Value, (int)numericUpDownGraceHours.Value, (int)numericUpDownGraceMin.Value, 0);
                }

                if (checkBoxCompressedDigitalAudioOPL.Checked)
                {
                    objContentKeyPolicyPlayReadyLicense.PlayRight.CompressedDigitalAudioOpl = (int)numericUpDownCompressedDigitalAudioOPL.Value;
                }

                if (checkBoxCompressedDigitalVideoOPL.Checked)
                {
                    objContentKeyPolicyPlayReadyLicense.PlayRight.CompressedDigitalVideoOpl = (int)numericUpDownCompressedDigitalVideoOPL.Value;
                }

                if (checkBoxUncompressedDigitalAudioOPL.Checked)
                {
                    objContentKeyPolicyPlayReadyLicense.PlayRight.UncompressedDigitalAudioOpl = (int)numericUpDownUncompressedDigitalAudioOPL.Value;
                }

                if (checkBoxUncompressedDigitalVideoOPL.Checked)
                {
                    objContentKeyPolicyPlayReadyLicense.PlayRight.UncompressedDigitalVideoOpl = (int)numericUpDownUncompressedDigitalVideoOPL.Value;
                }

                if (checkBoxAnalogVideoOPL.Checked)
                {
                    objContentKeyPolicyPlayReadyLicense.PlayRight.AnalogVideoOpl = (int)numericUpDownAnalogVideoOPL.Value;
                }

                objContentKeyPolicyPlayReadyLicense.PlayRight.DigitalVideoOnlyContentRestriction = checkBoxDigitalVideoOnlyContentRestriction.Checked;
                objContentKeyPolicyPlayReadyLicense.PlayRight.ImageConstraintForAnalogComponentVideoRestriction = checkBoxImageConstraintForAnalogComponentVideoRestriction.Checked;
                objContentKeyPolicyPlayReadyLicense.PlayRight.ImageConstraintForAnalogComputerMonitorRestriction = checkBoxImageConstraintForAnalogComponentVideoRestriction.Checked;

                if (comboBoxAllowPassingVideoContentUnknownOutput.SelectedItem?.ToString() == ContentKeyPolicyPlayReadyUnknownOutputPassingOption.Allowed.ToString())
                {
                    objContentKeyPolicyPlayReadyLicense.PlayRight.AllowPassingVideoContentToUnknownOutput = ContentKeyPolicyPlayReadyUnknownOutputPassingOption.Allowed;
                }
                else if (comboBoxAllowPassingVideoContentUnknownOutput.SelectedItem?.ToString() == ContentKeyPolicyPlayReadyUnknownOutputPassingOption.AllowedWithVideoConstriction)
                {
                    objContentKeyPolicyPlayReadyLicense.PlayRight.AllowPassingVideoContentToUnknownOutput = ContentKeyPolicyPlayReadyUnknownOutputPassingOption.AllowedWithVideoConstriction;
                }
                else if (comboBoxAllowPassingVideoContentUnknownOutput.SelectedItem?.ToString() == ContentKeyPolicyPlayReadyUnknownOutputPassingOption.NotAllowed)
                {
                    objContentKeyPolicyPlayReadyLicense.PlayRight.AllowPassingVideoContentToUnknownOutput = ContentKeyPolicyPlayReadyUnknownOutputPassingOption.NotAllowed;
                }
                else
                {
                    objContentKeyPolicyPlayReadyLicense.PlayRight.AllowPassingVideoContentToUnknownOutput = ContentKeyPolicyPlayReadyUnknownOutputPassingOption.Unknown;
                }

                if (comboBoxContentType.SelectedItem?.ToString() == ContentKeyPolicyPlayReadyContentType.UltraVioletDownload)
                {
                    objContentKeyPolicyPlayReadyLicense.ContentType = ContentKeyPolicyPlayReadyContentType.UltraVioletDownload;
                }
                else if (comboBoxContentType.SelectedItem?.ToString() == ContentKeyPolicyPlayReadyContentType.UltraVioletStreaming)
                {
                    objContentKeyPolicyPlayReadyLicense.ContentType = ContentKeyPolicyPlayReadyContentType.UltraVioletStreaming;
                }
                else if (comboBoxContentType.SelectedItem?.ToString() == ContentKeyPolicyPlayReadyContentType.Unknown)
                {
                    objContentKeyPolicyPlayReadyLicense.ContentType = ContentKeyPolicyPlayReadyContentType.Unknown;
                }
                else
                {
                    objContentKeyPolicyPlayReadyLicense.ContentType = ContentKeyPolicyPlayReadyContentType.Unspecified;
                }

                return objContentKeyPolicyPlayReadyConfiguration;

            }
        }

        public string PlayReadOptionName
        {
            get => string.IsNullOrWhiteSpace(textBoxPolicyName.Text) ? null : textBoxPolicyName.Text;
            set => textBoxPolicyName.Text = value;
        }


        public DRM_PlayReadyLicense(int step = -1, int option = -1, bool laststep = true)
        {
            InitializeComponent();
            Icon = Bitmaps.Azure_Explorer_ico;

            if (step > -1 && option > -1)
            {
                Text = string.Format(Text, step);
                labelstep.Text = string.Format(labelstep.Text, step, option);
            }
            if (!laststep)
            {
                buttonOk.Text = "Next";
                buttonOk.Image = null;
            }
            textBoxPolicyName.Text = $"PlayReady-Option-{option}";
        }


        private void Action_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData as string);
        }

        private void PlayReadyLicense_Load(object sender, EventArgs e)
        {
            DpiUtils.InitPerMonitorDpi(this);
            HighDpiHelper.AdjustControlImagesDpiScale(panel1);

            moreinfocompliance.Links.Add(new LinkLabel.Link(0, moreinfocompliance.Text.Length, Constants.LinkPlayReadyCompliance));

            comboBoxLicenseType.Items.Add(ContentKeyPolicyPlayReadyLicenseType.NonPersistent);
            comboBoxLicenseType.Items.Add(ContentKeyPolicyPlayReadyLicenseType.Persistent);
            //comboBoxLicenseType.Items.Add(ContentKeyPolicyPlayReadyLicenseType.Unknown);

            comboBoxAllowPassingVideoContentUnknownOutput.Items.Add(ContentKeyPolicyPlayReadyUnknownOutputPassingOption.Allowed);
            comboBoxAllowPassingVideoContentUnknownOutput.Items.Add(ContentKeyPolicyPlayReadyUnknownOutputPassingOption.AllowedWithVideoConstriction);
            comboBoxAllowPassingVideoContentUnknownOutput.Items.Add(ContentKeyPolicyPlayReadyUnknownOutputPassingOption.NotAllowed);
            //comboBoxAllowPassingVideoContentUnknownOutput.Items.Add(ContentKeyPolicyPlayReadyUnknownOutputPassingOption.Unknown);

            comboBoxContentType.Items.Add(ContentKeyPolicyPlayReadyContentType.Unspecified);
            comboBoxContentType.Items.Add(ContentKeyPolicyPlayReadyContentType.UltraVioletDownload);
            comboBoxContentType.Items.Add(ContentKeyPolicyPlayReadyContentType.UltraVioletStreaming);
            //comboBoxContentType.Items.Add(ContentKeyPolicyPlayReadyContentType.Unknown);

            comboBoxLicenseType.SelectedIndex = 0;
            comboBoxAllowPassingVideoContentUnknownOutput.SelectedIndex = 0;
            comboBoxContentType.SelectedIndex = 0;

        }

        private void CheckBoxStartDate_CheckedChanged(object sender, EventArgs e)
        {
            radioButtonStartDateAbsolute.Enabled = checkBoxStartDate.Checked;
            radioButtonStartDateRelative.Enabled = checkBoxStartDate.Checked;
            panelStartDateAbsolute.Enabled = checkBoxStartDate.Checked && radioButtonStartDateAbsolute.Checked;
            panelStartDateRelative.Enabled = checkBoxStartDate.Checked && radioButtonStartDateRelative.Checked;

            Value_SelectedIndexChanged(sender, e);
        }

        private void CheckBoxEndDate_CheckedChanged(object sender, EventArgs e)
        {
            radioButtonEndDateAbsolute.Enabled = checkBoxEndDate.Checked;
            radioButtonEndDateRelative.Enabled = checkBoxEndDate.Checked;
            panelEndDateAbsolute.Enabled = checkBoxStartDate.Checked && radioButtonStartDateAbsolute.Checked;
            panelEndDateRelative.Enabled = checkBoxStartDate.Checked && radioButtonStartDateRelative.Checked;

            Value_SelectedIndexChanged(sender, e);
        }

        private void DateTimePickerStartDate_ValueChanged(object sender, EventArgs e)
        {
            dateTimePickerStartTime.Value = dateTimePickerStartDate.Value;
            Value_SelectedIndexChanged(sender, e);
        }

        private void DateTimePickerStartTime_ValueChanged(object sender, EventArgs e)
        {
            dateTimePickerStartDate.Value = dateTimePickerStartTime.Value;
            Value_SelectedIndexChanged(sender, e);
        }

        private void DateTimePickerEndDate_ValueChanged(object sender, EventArgs e)
        {
            dateTimePickerEndTime.Value = dateTimePickerEndDate.Value;
            Value_SelectedIndexChanged(sender, e);
        }

        private void DateTimePickerEndTime_ValueChanged(object sender, EventArgs e)
        {
            dateTimePickerEndDate.Value = dateTimePickerEndTime.Value;
            Value_SelectedIndexChanged(sender, e);
        }

        private void Value_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool Error = false;
            try
            {
                ContentKeyPolicyPlayReadyConfiguration plt = GetPlayReadyConfiguration;
            }
            catch (Exception ex)
            {
                labelWarning.Text = Program.GetErrorMessage(ex);
                Error = true;
            }

            if (!Error)
            {
                labelWarning.Text = string.Empty;
            }
        }

        private void CheckBoxCompressedDigitalAudioOPL_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownCompressedDigitalAudioOPL.Enabled = checkBoxCompressedDigitalAudioOPL.Checked;
            Value_SelectedIndexChanged(sender, e);
        }

        private void CheckBoxCompressedDigitalVideoOPL_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownCompressedDigitalVideoOPL.Enabled = checkBoxCompressedDigitalVideoOPL.Checked;
            Value_SelectedIndexChanged(sender, e);
        }

        private void CheckBoxUncompressedDigitalAudioOPL_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownUncompressedDigitalAudioOPL.Enabled = checkBoxUncompressedDigitalAudioOPL.Checked;
            Value_SelectedIndexChanged(sender, e);
        }

        private void CheckBoxUncompressedDigitalVideoOPL_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownUncompressedDigitalVideoOPL.Enabled = checkBoxUncompressedDigitalVideoOPL.Checked;
            Value_SelectedIndexChanged(sender, e);
        }

        private void CheckBoxAnalogVideoOPL_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownAnalogVideoOPL.Enabled = checkBoxAnalogVideoOPL.Checked;
            Value_SelectedIndexChanged(sender, e);
        }

        private void ComboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxLicenseType.SelectedItem.ToString() == ContentKeyPolicyPlayReadyLicenseType.NonPersistent.ToString())  // Non persistent
            {
                groupBoxFirstPlay.Enabled = false;
                checkBoxFPExp.Checked = false;
                groupBoxEndDate.Enabled = false;
                checkBoxEndDate.Checked = false;
                groupBoxStartDate.Enabled = false;
                checkBoxStartDate.Checked = false;
            }
            else
            {
                groupBoxFirstPlay.Enabled = true;
                groupBoxEndDate.Enabled = true;
                groupBoxStartDate.Enabled = true;
            }
            Value_SelectedIndexChanged(sender, e);
        }

        private void CheckBoxFPExp_CheckedChanged(object sender, EventArgs e)
        {
            panelFirstPlayExpiration.Enabled = checkBoxFPExp.Checked;
            Value_SelectedIndexChanged(sender, e);
        }

        private void RadioButtonsStartDate_CheckedChanged(object sender, EventArgs e)
        {
            panelStartDateAbsolute.Enabled = radioButtonStartDateAbsolute.Checked;
            panelStartDateRelative.Enabled = radioButtonStartDateRelative.Checked;
        }

        private void RadioButtonsEndDate_CheckedChanged(object sender, EventArgs e)
        {
            panelEndDateAbsolute.Enabled = radioButtonEndDateAbsolute.Checked;
            panelEndDateRelative.Enabled = radioButtonEndDateRelative.Checked;
        }

        private void CheckBoxGrace_CheckedChanged(object sender, EventArgs e)
        {
            panelGrace.Enabled = checkBoxGrace.Checked;
            Value_SelectedIndexChanged(sender, e);
        }

        private void DRM_PlayReadyLicense_DpiChanged(object sender, DpiChangedEventArgs e)
        {
            DpiUtils.UpdatedSizeFontAfterDPIChange(labelstep, e);
            HighDpiHelper.AdjustControlImagesDpiScale(panel1);
        }
    }
}
