//----------------------------------------------------------------------------------------------
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
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using Microsoft.WindowsAzure.MediaServices.Client.ContentKeyAuthorization;
using Microsoft.WindowsAzure.MediaServices.Client.DynamicEncryption;
using System.Xml;
using System.IO;
using Microsoft.WindowsAzure.MediaServices.Client.Widevine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.WindowsAzure.MediaServices.Client;

namespace AMSExplorer
{
    public partial class AddDynamicEncryptionFrame6_WidevineLicense : Form
    {
        private string _widevineTempUrl;

        public string WidevinePolicyName
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
        public string GetWidevineConfiguration(string keyDeliveryUrl)
        {
            var jsonstr = textBoxConfiguration.Text;
            
            try // let's replace the Acquisition URL if needed
            {
                dynamic obj = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonstr);
                if (obj.policy_overrides != null && obj.policy_overrides.renewal_server_url !=null)
                {
                    obj.policy_overrides.renewal_server_url = keyDeliveryUrl;
                }
                jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.None);
            }
            catch
            {

            }
            return jsonstr;
        }

        private string GetWidevineConfiguration(Uri keyDeliveryUrl)
        {
            if (radioButtonBasic.Checked)
            {
                return "{}";
            }
            else
            {
                var template = new WidevineMessage
                {
                    content_key_specs = new[]
                                                  {
                                            new ContentKeySpecs
                                            {
                                                required_output_protection = new RequiredOutputProtection { hdcp = (Hdcp)(Enum.Parse(typeof(Hdcp), (string)comboBoxReqOutputProtection.SelectedItem))},
                                                track_type = textBoxTrackType.Text
                                            }
                                        },
                    policy_overrides = new
                    {
                        can_play = checkBoxCanPlay.Checked,
                        can_persist = checkBoxCanPersist.Checked,
                        can_renew = checkBoxCanRenew.Checked
                    }
                };

                if (checkBoxAllowTrackType.Checked)
                {
                    template.allowed_track_types = (AllowedTrackTypes)(Enum.Parse(typeof(AllowedTrackTypes), (string)comboBoxAllowedTrackTypes.SelectedItem));
                }

                if (checkBoxSecLevel.Checked)
                {
                    template.content_key_specs.FirstOrDefault().security_level = (int)numericUpDownSecLevel.Value;
                }

                if (checkBoxCanRenew.Checked)
                {
                    template.policy_overrides = new
                    {
                        can_play = checkBoxCanPlay.Checked,
                        can_persist = checkBoxCanPersist.Checked,
                        can_renew = checkBoxCanRenew.Checked,
                        renewal_server_url = keyDeliveryUrl.ToString()
                    };
                }

                return JsonConvert.SerializeObject(template, Newtonsoft.Json.Formatting.Indented);
            }
        }

        public AddDynamicEncryptionFrame6_WidevineLicense(string widevineTempUrl, int step = -1, int option = -1, bool laststep = true)
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
            _widevineTempUrl = widevineTempUrl;
        }

        private void AddDynamicEncryptionFrame6_WidevineLicense_Load(object sender, EventArgs e)
        {
            comboBoxAllowedTrackTypes.Items.AddRange(Enum.GetNames(typeof(AllowedTrackTypes)).ToArray());
            comboBoxAllowedTrackTypes.SelectedItem = Enum.GetName(typeof(AllowedTrackTypes), AllowedTrackTypes.SD_HD);

            comboBoxReqOutputProtection.Items.AddRange(Enum.GetNames(typeof(Hdcp)).ToArray());
            comboBoxReqOutputProtection.SelectedItem = Enum.GetName(typeof(Hdcp), Hdcp.HDCP_NONE);

            linkLabelWidevinePolicy.Links.Add(new LinkLabel.Link(0, linkLabelWidevinePolicy.Text.Length, Constants.LinkWidevineTemplateInfo));
        }


        private void checkBoxSecLevel_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownSecLevel.Enabled = checkBoxSecLevel.Checked;
            UpdateJsonConfig();
        }

        private void checkBoxTrackType_CheckedChanged(object sender, EventArgs e)
        {
            textBoxTrackType.Enabled = checkBoxTrackType.Checked;
            UpdateJsonConfig();
        }

        private void checkBoxAllowTrackType_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxAllowedTrackTypes.Enabled = checkBoxAllowTrackType.Checked;
            UpdateJsonConfig();
        }

        private void UpdateJsonConfig()
        {
            textBoxConfiguration.Text = this.GetWidevineConfiguration(new Uri(_widevineTempUrl));
        }

        private void radioButtonAdvanced_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxAdvLicense.Enabled = radioButtonAdvanced.Checked;
            UpdateJsonConfig();
        }

        private void textBoxConfiguration_TextChanged(object sender, EventArgs e)
        {
            bool Error = false;
            var type = Program.AnalyseConfigurationString(textBoxConfiguration.Text);
            if (type == TypeConfig.JSON)
            {
                // Let's check JSON syntax

                try
                {
                    var jo = JObject.Parse(textBoxConfiguration.Text);
                }
                catch (Exception ex)
                {
                    labelWarningJSON.Text = string.Format((string)labelWarningJSON.Tag, ex.Message);
                    Error = true;
                }
            }
            labelWarningJSON.Visible = Error;
        }

        private void StateChanged(object sender, EventArgs e)
        {
            UpdateJsonConfig();
        }

        private void linkLabelWidevinePolicy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData as string);

        }
    }
}
