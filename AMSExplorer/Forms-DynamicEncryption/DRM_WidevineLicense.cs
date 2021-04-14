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
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace AMSExplorer
{
    public partial class DRM_WidevineLicense : Form
    {
        private string _labelWarningJSON;

        public string WidevinePolicyName
        {
            get => string.IsNullOrWhiteSpace(textBoxPolicyName.Text) ? null : textBoxPolicyName.Text;
            set => textBoxPolicyName.Text = value;
        }


        public ContentKeyPolicyWidevineConfiguration GetWidevineConfiguration => new ContentKeyPolicyWidevineConfiguration
        {
            WidevineTemplate = textBoxConfiguration.Text
        };

        public DRM_WidevineLicense(int step = -1, int option = -1, bool laststep = true)
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
            textBoxPolicyName.Text = $"Widevine-Option-{option}";
        }

        private void DRM_WidevineLicense_Load(object sender, EventArgs e)
        {
            DpiUtils.InitPerMonitorDpi(this);
            HighDpiHelper.AdjustControlImagesDpiScale(panel1);

            _labelWarningJSON = labelWarningJSON.Text;
            linkLabelWidevinePolicy.Links.Add(new LinkLabel.Link(0, linkLabelWidevinePolicy.Text.Length, Constants.LinkWidevineTemplateInfo));
            radioButtonBasic.Checked = true;
        }


        private void radioButtonAdvanced_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonAdvanced.Checked)
            {


                WidevineTemplate template = new WidevineTemplate()
                {
                    AllowedTrackTypes = "SD_HD",
                    ContentKeySpecs = new ContentKeySpec[]
            {
                    new ContentKeySpec()
                    {
                        TrackType = "SD",
                        SecurityLevel = 1,
                        RequiredOutputProtection = new OutputProtection()
                        {
                            HDCP = "HDCP_NONE"
                        }
                    }
            },
                    PolicyOverrides = new PolicyOverrides()
                    {
                        CanPlay = true,
                        CanPersist = true,
                        CanRenew = false,
                        RentalDurationSeconds = 2592000,
                        PlaybackDurationSeconds = 10800,
                        LicenseDurationSeconds = 604800,
                    }
                };

                textBoxConfiguration.Text = Newtonsoft.Json.JsonConvert.SerializeObject(template, Newtonsoft.Json.Formatting.Indented);
            }

            else
            {
                textBoxConfiguration.Text = "{}";
            }

        }

        private void textBoxConfiguration_TextChanged(object sender, EventArgs e)
        {
            bool Error = false;
            Program.TypeConfig type = Program.AnalyseConfigurationString(textBoxConfiguration.Text);
            if (type == Program.TypeConfig.JSON)
            {
                // Let's check JSON syntax and that it conforms to the widevine template class

                try
                {
                    JObject jo = JObject.Parse(textBoxConfiguration.Text);
                    WidevineTemplate jow = Newtonsoft.Json.JsonConvert.DeserializeObject<WidevineTemplate>(textBoxConfiguration.Text);

                }
                catch (Exception ex)
                {
                    labelWarningJSON.Text = string.Format(_labelWarningJSON, ex.Message);
                    Error = true;
                }
            }
            labelWarningJSON.Visible = Error;
        }


        private void linkLabelWidevinePolicy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var p = new Process(); p.StartInfo = new ProcessStartInfo { FileName = e.Link.LinkData as string, UseShellExecute = true }; p.Start();

        }

        private void DRM_WidevineLicense_DpiChanged(object sender, DpiChangedEventArgs e)
        {
            // for controls which are not using the default font
            DpiUtils.UpdatedSizeFontAfterDPIChange(new List<Control> { labelstep, textBoxConfiguration }, e, this);

            // to scale the bitmap in the buttons
            HighDpiHelper.AdjustControlImagesAfterDpiChange(panel1, e);
        }
    }
}
