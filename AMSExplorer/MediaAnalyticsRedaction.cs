//----------------------------------------------------------------------------------------------
//    Copyright 2016 Microsoft Corporation
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
using Microsoft.WindowsAzure.MediaServices.Client;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;

namespace AMSExplorer
{
    public partial class MediaAnalyticsRedaction : Form
    {
        private CloudMediaContext _context;
        private IMediaProcessor _processor;
        private bool _preview;
        private Image _processorImage;

        public string MIInputAssetName
        {
            get
            {
                return labelAssetName.Text;
            }
            set
            {
                labelAssetName.Text = value;
            }
        }

        public string MIOutputAssetName
        {
            get
            {
                return textboxoutputassetname.Text;
            }
            set
            {
                textboxoutputassetname.Text = value;
            }
        }

        public string MIJobName
        {
            get
            {
                return textBoxJobName.Text;
            }
            set
            {
                textBoxJobName.Text = value;
            }
        }

        public JobOptionsVar JobOptions
        {
            get
            {
                return buttonJobOptions.GetSettings();
            }
            set
            {
                buttonJobOptions.SetSettings(value);
            }
        }
        public string JsonConfig()
        {
            if (string.IsNullOrWhiteSpace(textBoxConfiguration.Text))
            {
                return JsonInternalConfig();
            }
            else
            {
                return textBoxConfiguration.Text;
            }
        }

        public string RedactionMode()
        {
            if (radioButtonCombined.Checked)
            {
                return Constants.FaceRedactionCombined;
            }
            else if (radioButtonFirstPass.Checked)
            {
                return Constants.FaceRedactionFirstPass;
            }
            else // if (radioButtonSecondPass.Checked)
            {
                return Constants.FaceRedactionSecondPass;
            }
        }

        private string JsonInternalConfig()
        {
            // Example of config :
            //  @"{'Version':'1.0', 'Options': {'AggregateEmotionWindowMs':'987','Mode':'AggregateEmotion','AggregateEmotionIntervalMs':'342'}}"

            dynamic obj = new JObject();
            obj.Version = "1.0";
            obj.Options = new JObject();
            obj.Options.Mode = RedactionMode();

            return JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.Indented);
        }

        public MediaAnalyticsRedaction(CloudMediaContext context, IMediaProcessor processor, Image processorImage, bool preview)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            _context = context;
            _processor = processor;
            _preview = preview;
            _processorImage = processorImage;
            buttonJobOptions.Initialize(_context);
        }

        private void MediaAnalyticsRedaction_Load(object sender, EventArgs e)
        {
            // we don't have yet link or picture for Video Analytics Greneric. Let's use Yammer group
            moreinfoprofilelink.Links.Add(new LinkLabel.Link(0, moreinfoprofilelink.Text.Length, Constants.LinkMoreInfoFaceDetection));
            moreinfoprofilelink.Visible = true;

            labelProcessorName.Text = _processor.Name;
            labelPreview.Visible = _preview;
            labelProcessorVersion.Text = string.Format(labelProcessorVersion.Text, _processor.Version);
            buttonOk.Image = _processorImage;
            this.Text = _processor.Name;
        }

        private void moreinfoprofilelink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Send the URL to the operating system.
            Process.Start(e.Link.LinkData as string);
        }

        private void radioButtonDetectionMode_CheckedChanged(object sender, EventArgs e)
        {
            /*
            if (radioButtonCombined.Checked)
            {
                pictureBox1.Image = global::AMSExplorer.Bitmaps._04_face_detection;
            }
            else if (radioButtonFirstPass.Checked)
            {
                pictureBox1.Image = global::AMSExplorer.Bitmaps._06_emotion;

            }
            else if (radioButtonSecondPass.Checked)
            {
                pictureBox1.Image = global::AMSExplorer.Bitmaps._06_emotion;
            }
            */

            UpdateJSONData();
        }

        private void control_changed(object sender, EventArgs e)
        {
            UpdateJSONData();
        }

        private void UpdateJSONData()
        {
            textBoxConfiguration.Text = JsonInternalConfig();
        }

        private void textBoxConfiguration_TextChanged(object sender, EventArgs e)
        {
            // let's normalize the line breaks
            textBoxConfiguration.Text = textBoxConfiguration.Text.Replace("\r\n", "\n").Replace("\r", "\n").Replace("\n", Environment.NewLine);

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
            else if (type == TypeConfig.XML) // XML 
            {
                try
                {
                    var xml = XElement.Load(new StringReader(textBoxConfiguration.Text));
                }
                catch (Exception ex)
                {
                    labelWarningJSON.Text = string.Format("Error in XML data: {0}", ex.Message);
                    Error = true;
                }
            }
            labelWarningJSON.Visible = Error;
        }
    }
}
