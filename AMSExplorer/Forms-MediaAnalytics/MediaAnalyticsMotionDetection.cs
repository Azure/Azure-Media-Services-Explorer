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
using System.Diagnostics;
using Microsoft.WindowsAzure.MediaServices.Client;
using System.Xml.Linq;
using System.Security;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;

namespace AMSExplorer
{
    public partial class MediaAnalyticsMotionDetection : Form
    {
        private CloudMediaContext _context;
        private string _version;

        public readonly List<Item> SensitivityLevels = new List<Item> {
            new Item("Low", "low"),
            new Item("Medium", "medium"),
            new Item("High", "high")
        };
        private IAsset _firstAsset;
        private string _labelWarningJSON;

        public string IndexerInputAssetName
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

        public string IndexerOutputAssetName
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

        public string OCRJobName
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

        public string ProcessorConfiguration
        {
            get
            {
                return textBoxConfiguration.Text;
            }
        }

        public MediaAnalyticsMotionDetection(CloudMediaContext context, string version, IAsset firstAsset, Mainform main)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            _context = context;
            _version = version;
            _firstAsset = firstAsset;

            buttonRegionEditor.Initialize(_firstAsset, main, true, 8, false);
            buttonRegionEditor.RegionsChanged += buttonRegionEditor_RegionsChanged;

            buttonJobOptions.Initialize(_context);
        }

        private void buttonRegionEditor_RegionsChanged(object sender, EventArgs e)
        {
            UpdateJSONData();
        }

        private void MediaAnalyticsMotionDetection_Load(object sender, EventArgs e)
        {
            _labelWarningJSON = labelWarningJSON.Text;
            comboBoxSensitivity.Items.AddRange(SensitivityLevels.ToArray());
            comboBoxSensitivity.SelectedIndex = 1;
            labelProcessorVersion.Text = string.Format(labelProcessorVersion.Text, _version);
            moreinfoprofilelink.Links.Add(new LinkLabel.Link(0, moreinfoprofilelink.Text.Length, Constants.LinkMoreInfoMotionDetection));
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

        private string JsonInternalConfig()
        {
            // Example of config :

            /*
           {
  'Version': '1.0',
  'Options': {
    'SensitivityLevel': 'medium',
    'FrameSamplingValue': 3,
    'DetectLightChange': 'False',
    "MergeTimeThreshold":
    '00:00:05',
    'DetectionZones': [
      [
        {
          'x': 0,
          'y': 0
        },
        {
          'x': 0.5,
          'y': 0
        },
        {
          'x': 1,
          'y': 0
        },
        {
          'x': 1,
          'y': 0.5
        },
        {
          'x': 1,
          'y': 1
        },
        {
          'x': 0.5,
          'y': 1
        },
        {
          'x': 0,
          'y': 1
        },
        {
          'x': 0,
          'y': 0.5
        }
      ],
      [
        {
          'x': 0.3,
          'y': 0.3
        },
        {
          'x': 0.55,
          'y': 0.3
        },
        {
          'x': 0.8,
          'y': 0.3
        },
        {
          'x': 0.8,
          'y': 0.55
        },
        {
          'x': 0.8,
          'y': 0.8
        },
        {
          'x': 0.55,
          'y': 0.8
        },
        {
          'x': 0.3,
          'y': 0.8
        },
        {
          'x': 0.3,
          'y': 0.55
        }
      ]
    ]
  }
}
            */

            dynamic obj = new JObject();
            obj.Version = "1.0";
            obj.Options = new JObject();
            obj.Options.SensitivityLevel = ((Item)comboBoxSensitivity.SelectedItem).Value as string;
            obj.Options.FrameSamplingValue = (int)numericUpDownFrameSampling.Value;
            obj.Options.DetectLightChange = checkBoxDetectLightChange.Checked.ToString();
            obj.Options.MergeTimeThreshold = new TimeSpan((int)numericUpDownMergeTimeHours.Value, (int)numericUpDownMergeTimeMinutes.Value, (int)numericUpDownMergeTimeSeconds.Value).ToString("c");
            if (checkBoxRestrictDetection.Checked && buttonRegionEditor.GetSavedPolygonesDecimalMode().Count > 0)
            {
                obj.Options.DetectionZones = new JArray() as dynamic;
                foreach (var poly in buttonRegionEditor.GetSavedPolygonesDecimalMode())
                {
                    dynamic zone = new JArray() as dynamic;
                    foreach (var p in poly.ToDecimalPoints())
                    {
                        dynamic point = new JObject();
                        point.x = p.X;
                        point.y = p.Y;
                        zone.Add(point);
                    }
                    obj.Options.DetectionZones.Add(zone);
                }
            }

            return JsonConvert.SerializeObject(obj, Formatting.Indented);
        }


        private void moreinfoprofilelink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Send the URL to the operating system.
            Process.Start(e.Link.LinkData as string);
        }


        private void checkBoxRestrictDetection_CheckedChanged(object sender, EventArgs e)
        {
            UpdateJSONData();
            panelSelectRegions.Enabled = checkBoxRestrictDetection.Checked;
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
                    labelWarningJSON.Text = string.Format("Error in XML data: {0}", ex.Message);
                    Error = true;
                }
            }
            labelWarningJSON.Visible = Error;
        }

        private void control_changed(object sender, EventArgs e)
        {
            UpdateJSONData();
        }
    }
}
