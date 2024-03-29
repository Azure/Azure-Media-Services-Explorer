﻿//----------------------------------------------------------------------------------------------
//    Copyright 2023 Microsoft Corporation
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

using Azure.ResourceManager.Media.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace AMSExplorer
{
    public partial class DRM_PlayReadyLicense : Form
    {
        private bool _initialized = false;

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


                objContentKeyPolicyPlayReadyLicense = new ContentKeyPolicyPlayReadyLicense(
                    allowTestDevices: checkBoxAllowTestDevices.Checked,
                    licenseType: ContentKeyPolicyPlayReadyLicenseType.Unknown,
                    contentKeyLocation: new ContentKeyPolicyPlayReadyContentEncryptionKeyFromHeader(),
                    contentType: ContentKeyPolicyPlayReadyContentType.Unknown
                );

                ContentKeyPolicyPlayReadyConfiguration objContentKeyPolicyPlayReadyConfiguration = new(
                    new List<ContentKeyPolicyPlayReadyLicense> { objContentKeyPolicyPlayReadyLicense }
                );

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
                        objContentKeyPolicyPlayReadyLicense.BeginOn = dateTimePickerStartDate.Value.ToUniversalTime();
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
                        objContentKeyPolicyPlayReadyLicense.ExpireOn = dateTimePickerEndDate.Value.ToUniversalTime();
                    }
                    else // Relative
                    {
                        objContentKeyPolicyPlayReadyLicense.RelativeExpirationDate = new TimeSpan((int)numericUpDownEndDateDays.Value, (int)numericUpDownEndDateHours.Value, (int)numericUpDownEndDateMinutes.Value, 0);
                    }
                }

                // Generate PlayRight
                if (objContentKeyPolicyPlayReadyLicense.PlayRight == null)
                {
                    objContentKeyPolicyPlayReadyLicense.PlayRight = new ContentKeyPolicyPlayReadyPlayRight(
                        hasDigitalVideoOnlyContentRestriction: checkBoxDigitalVideoOnlyContentRestriction.Checked,
                        hasImageConstraintForAnalogComponentVideoRestriction: checkBoxImageConstraintForAnalogComponentVideoRestriction.Checked,
                        hasImageConstraintForAnalogComputerMonitorRestriction: checkBoxImageConstraintForAnalogComponentVideoRestriction.Checked,
                        allowPassingVideoContentToUnknownOutput: ContentKeyPolicyPlayReadyUnknownOutputPassingOption.Unknown
                        );
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
                    objContentKeyPolicyPlayReadyLicense.PlayRight.CompressedDigitalAudioOutputProtectionLevel = (int)numericUpDownCompressedDigitalAudioOPL.Value;
                }

                if (checkBoxCompressedDigitalVideoOPL.Checked)
                {
                    objContentKeyPolicyPlayReadyLicense.PlayRight.CompressedDigitalVideoOutputProtectionLevel = (int)numericUpDownCompressedDigitalVideoOPL.Value;
                }

                if (checkBoxUncompressedDigitalAudioOPL.Checked)
                {
                    objContentKeyPolicyPlayReadyLicense.PlayRight.UncompressedDigitalAudioOutputProtectionLevel = (int)numericUpDownUncompressedDigitalAudioOPL.Value;
                }

                if (checkBoxUncompressedDigitalVideoOPL.Checked)
                {
                    objContentKeyPolicyPlayReadyLicense.PlayRight.UncompressedDigitalVideoOutputProtectionLevel = (int)numericUpDownUncompressedDigitalVideoOPL.Value;
                }

                if (checkBoxAnalogVideoOPL.Checked)
                {
                    objContentKeyPolicyPlayReadyLicense.PlayRight.AnalogVideoOutputProtectionLevel = (int)numericUpDownAnalogVideoOPL.Value;
                }

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
            var p = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = e.Link.LinkData as string,
                    UseShellExecute = true
                }
            };
            p.Start();
        }

        private void PlayReadyLicense_Load(object sender, EventArgs e)
        {
            moreinfocompliance.Links.Add(new LinkLabel.Link(0, moreinfocompliance.Text.Length, Constants.LinkPlayReadyCompliance));

            comboBoxLicenseType.BeginUpdate();
            comboBoxLicenseType.Items.Add(ContentKeyPolicyPlayReadyLicenseType.NonPersistent);
            comboBoxLicenseType.Items.Add(ContentKeyPolicyPlayReadyLicenseType.Persistent);
            comboBoxLicenseType.SelectedIndex = 0;
            comboBoxLicenseType.EndUpdate();
            //comboBoxLicenseType.Items.Add(ContentKeyPolicyPlayReadyLicenseType.Unknown);

            comboBoxAllowPassingVideoContentUnknownOutput.BeginUpdate();
            comboBoxAllowPassingVideoContentUnknownOutput.Items.Add(ContentKeyPolicyPlayReadyUnknownOutputPassingOption.Allowed);
            comboBoxAllowPassingVideoContentUnknownOutput.Items.Add(ContentKeyPolicyPlayReadyUnknownOutputPassingOption.AllowedWithVideoConstriction);
            comboBoxAllowPassingVideoContentUnknownOutput.Items.Add(ContentKeyPolicyPlayReadyUnknownOutputPassingOption.NotAllowed);
            comboBoxAllowPassingVideoContentUnknownOutput.Items.Add(ContentKeyPolicyPlayReadyUnknownOutputPassingOption.Unknown);
            comboBoxAllowPassingVideoContentUnknownOutput.SelectedIndex = 0;
            comboBoxAllowPassingVideoContentUnknownOutput.EndUpdate();

            comboBoxContentType.BeginUpdate();
            comboBoxContentType.Items.Add(ContentKeyPolicyPlayReadyContentType.Unspecified);
            comboBoxContentType.Items.Add(ContentKeyPolicyPlayReadyContentType.UltraVioletDownload);
            comboBoxContentType.Items.Add(ContentKeyPolicyPlayReadyContentType.UltraVioletStreaming);
            //comboBoxContentType.Items.Add(ContentKeyPolicyPlayReadyContentType.Unknown);
            comboBoxContentType.SelectedIndex = 0;
            comboBoxContentType.EndUpdate();

            labelWarning.Text = string.Empty;
        }

        private void CheckBoxStartDate_CheckedChanged(object sender, EventArgs e)
        {
            if (!_initialized) return;

            radioButtonStartDateAbsolute.Enabled = checkBoxStartDate.Checked;
            radioButtonStartDateRelative.Enabled = checkBoxStartDate.Checked;
            panelStartDateAbsolute.Enabled = checkBoxStartDate.Checked && radioButtonStartDateAbsolute.Checked;
            panelStartDateRelative.Enabled = checkBoxStartDate.Checked && radioButtonStartDateRelative.Checked;

            Value_SelectedIndexChanged(sender, e);
        }

        private void CheckBoxEndDate_CheckedChanged(object sender, EventArgs e)
        {
            if (!_initialized) return;

            radioButtonEndDateAbsolute.Enabled = checkBoxEndDate.Checked;
            radioButtonEndDateRelative.Enabled = checkBoxEndDate.Checked;
            panelEndDateAbsolute.Enabled = checkBoxStartDate.Checked && radioButtonStartDateAbsolute.Checked;
            panelEndDateRelative.Enabled = checkBoxStartDate.Checked && radioButtonStartDateRelative.Checked;

            Value_SelectedIndexChanged(sender, e);
        }

        private void DateTimePickerStartDate_ValueChanged(object sender, EventArgs e)
        {
            if (!_initialized) return;

            dateTimePickerStartTime.Value = dateTimePickerStartDate.Value;
            Value_SelectedIndexChanged(sender, e);
        }

        private void DateTimePickerStartTime_ValueChanged(object sender, EventArgs e)
        {
            if (!_initialized) return;

            dateTimePickerStartDate.Value = dateTimePickerStartTime.Value;
            Value_SelectedIndexChanged(sender, e);
        }

        private void DateTimePickerEndDate_ValueChanged(object sender, EventArgs e)
        {
            if (!_initialized) return;

            dateTimePickerEndTime.Value = dateTimePickerEndDate.Value;
            Value_SelectedIndexChanged(sender, e);
        }

        private void DateTimePickerEndTime_ValueChanged(object sender, EventArgs e)
        {
            if (!_initialized) return;

            dateTimePickerEndDate.Value = dateTimePickerEndTime.Value;
            Value_SelectedIndexChanged(sender, e);
        }

        private void Value_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_initialized) return;

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
            if (!_initialized) return;

            numericUpDownCompressedDigitalAudioOPL.Enabled = checkBoxCompressedDigitalAudioOPL.Checked;
            Value_SelectedIndexChanged(sender, e);
        }

        private void CheckBoxCompressedDigitalVideoOPL_CheckedChanged(object sender, EventArgs e)
        {
            if (!_initialized) return;

            numericUpDownCompressedDigitalVideoOPL.Enabled = checkBoxCompressedDigitalVideoOPL.Checked;
            Value_SelectedIndexChanged(sender, e);
        }

        private void CheckBoxUncompressedDigitalAudioOPL_CheckedChanged(object sender, EventArgs e)
        {
            if (!_initialized) return;

            numericUpDownUncompressedDigitalAudioOPL.Enabled = checkBoxUncompressedDigitalAudioOPL.Checked;
            Value_SelectedIndexChanged(sender, e);
        }

        private void CheckBoxUncompressedDigitalVideoOPL_CheckedChanged(object sender, EventArgs e)
        {
            if (!_initialized) return;

            numericUpDownUncompressedDigitalVideoOPL.Enabled = checkBoxUncompressedDigitalVideoOPL.Checked;
            Value_SelectedIndexChanged(sender, e);
        }

        private void CheckBoxAnalogVideoOPL_CheckedChanged(object sender, EventArgs e)
        {
            if (!_initialized) return;

            numericUpDownAnalogVideoOPL.Enabled = checkBoxAnalogVideoOPL.Checked;
            Value_SelectedIndexChanged(sender, e);
        }

        private void ComboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_initialized) return;

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
            if (!_initialized) return;

            panelFirstPlayExpiration.Enabled = checkBoxFPExp.Checked;
            Value_SelectedIndexChanged(sender, e);
        }

        private void RadioButtonsStartDate_CheckedChanged(object sender, EventArgs e)
        {
            if (!_initialized) return;

            panelStartDateAbsolute.Enabled = radioButtonStartDateAbsolute.Checked;
            panelStartDateRelative.Enabled = radioButtonStartDateRelative.Checked;
        }

        private void RadioButtonsEndDate_CheckedChanged(object sender, EventArgs e)
        {
            if (!_initialized) return;

            panelEndDateAbsolute.Enabled = radioButtonEndDateAbsolute.Checked;
            panelEndDateRelative.Enabled = radioButtonEndDateRelative.Checked;
        }

        private void CheckBoxGrace_CheckedChanged(object sender, EventArgs e)
        {
            if (!_initialized) return;

            panelGrace.Enabled = checkBoxGrace.Checked;
            Value_SelectedIndexChanged(sender, e);
        }

        private void DRM_PlayReadyLicense_DpiChanged(object sender, DpiChangedEventArgs e)
        {
        }

        private void DRM_PlayReadyLicense_Shown(object sender, EventArgs e)
        {
            Telemetry.TrackPageView(this.Name);
            _initialized = true;
        }
    }
}
