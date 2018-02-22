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
using System.Diagnostics;
using Microsoft.WindowsAzure.MediaServices.Client;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using System.IO;
using Newtonsoft.Json;

namespace AMSExplorer
{
    public partial class Hyperlapse : Form
    {
        private CloudMediaContext _context;
        private string _processorVersion;
        private string _labelWarningJSON;

        public string HyperlapseInputAssetName
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
        public string HyperlapseOutputAssetName
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


        public string HyperlapseJobName
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


        public int HyperlapseStartFrame
        {
            get
            {
                return (int)numericUpDownStartFrame.Value;
            }
            set
            {
                numericUpDownStartFrame.Value = value;
            }
        }

        public int HyperlapseNumFrames
        {
            get
            {
                return (int)numericUpDownNumFrames.Value;
            }
            set
            {
                numericUpDownNumFrames.Value = value;
            }
        }

        public int HyperlapseSpeed
        {
            get
            {
                return (int)numericUpDownSpeed.Value;
            }
            set
            {
                numericUpDownSpeed.Value = value;
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

        public Hyperlapse(CloudMediaContext context, string processorVersion)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            _context = context;
            _processorVersion = processorVersion;

            buttonJobOptions.Initialize(_context);
        }


        private void Hyperlapse_Load(object sender, EventArgs e)
        {
            _labelWarningJSON = labelWarningJSON.Text;

            moreinfoprofilelink.Links.Add(new LinkLabel.Link(0, moreinfoprofilelink.Text.Length, Constants.LinkMoreInfoHyperlapse));
            linkLabelHowItWorks.Links.Add(new LinkLabel.Link(0, moreinfoprofilelink.Text.Length, Constants.LinkHowItWorksHyperlapse));
            comboBoxFrameRate.SelectedIndex = 2;
            labelProcessorVersion.Text = string.Format(labelProcessorVersion.Text, _processorVersion);
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


        /*
        private string HyperlapseInternalConfiguration()
        {
            XNamespace ns = "http://www.windowsazure.com/media/encoding/Preset/2014/03";

            var presetxml = _templateXML.Element(ns + "Preset");

            var sourcexml = presetxml.Element(ns + "Sources").Element(ns + "Source");
            sourcexml.Attribute("StartFrame").SetValue(HyperlapseStartFrame);
            sourcexml.Attribute("NumFrames").SetValue(HyperlapseNumFrames);

            var speedxml = presetxml.Element(ns + "Options").Element(ns + "Speed");
            speedxml.SetValue(HyperlapseSpeed);

            return _templateXML.Declaration.ToString() + Environment.NewLine + _templateXML.ToString();
        }
        */

        private string JsonInternalConfig()
        {
            // Example of config
            /*
         {
    "Version":1.0,
    "Sources": [
        {
            "StartFrame":0,
            "NumFrames":2147483647
        }
    ],
    "Options": {
        "Speed":1,
        "Stabilize":false
    }
}
         */
            dynamic obj = new JObject();
            obj.Version = "1.0";

            dynamic Sources = new JArray() as dynamic;
            dynamic Source = new JObject();
            Source.StartFrame = HyperlapseStartFrame;
            if (checkBoxLimitNbFrames.Checked)
            {
                Source.NumFrames = HyperlapseNumFrames;
            }
           
            Sources.Add(Source);
            obj.Sources = Sources;

            dynamic Options = new JObject();
            Options.Speed = HyperlapseSpeed;
            if (!checkBoxStabilize.Checked)
            {
                Options.Stabilize = false;
            }
            obj.Options = Options;

            return JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.Indented);
        }

        private void moreinfoprofilelink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Send the URL to the operating system.
            Process.Start(e.Link.LinkData as string);
        }

        private void linkLabelHowItWorks_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Send the URL to the operating system.
            Process.Start(e.Link.LinkData as string);
        }

        private void DisplayTime()
        {
           
                double framerate = Convert.ToDouble(comboBoxFrameRate.Text);
                double speed = (double)numericUpDownSpeed.Value;

                TimeSpan tsstart = TimeSpan.FromSeconds(((double)numericUpDownStartFrame.Value) / framerate);
                TimeSpan tsduration = TimeSpan.FromSeconds(((double)numericUpDownNumFrames.Value) / framerate);
                TimeSpan tsoutputduration = TimeSpan.FromSeconds(tsduration.TotalSeconds / speed);

                textBoxSourceStartTime.Text = tsstart.ToString("g");
                textBoxSourceDurationTime.Text = tsduration.ToString("g");
                textBoxOutputDuration.Text = tsoutputduration.ToString("g");
        }

        private void control_ValueChanged(object sender, EventArgs e)
        {
            DisplayTime();
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
                    labelWarningJSON.Text = string.Format(_labelWarningJSON, ex.Message);
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
                    labelWarningJSON.Text = string.Format(AMSExplorer.Properties.Resources.EncodingMES_textBoxConfiguration_TextChanged_ErrorInXMLData0, ex.Message);
                    Error = true;
                }
            }
            labelWarningJSON.Visible = Error;
        }

        private void control_changed(object sender, EventArgs e)
        {
            UpdateJSONData();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
           numericUpDownNumFrames.Enabled = checkBoxLimitNbFrames.Checked;
            DisplayTime();
            UpdateJSONData();
        }
    }
}

