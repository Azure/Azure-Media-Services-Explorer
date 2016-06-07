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
using System.Xml.Linq;
using System.Security;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace AMSExplorer
{
    public partial class MediaAnalyticsVideoOCR : Form
    {
        private CloudMediaContext _context;
        private IndexerOptions formOptions = new IndexerOptions(true);
        private string _version;

        public readonly List<Item> VideOCRLanguages = new List<Item> {
            new Item("Undefined", ""),
            new Item("Arabic", "Arabic"),
            new Item("Chinese Simplified", "Chinese Simplified"),
            new Item("Chinese Traditional", "ZhCn"),
            new Item("Czech", "Czech"),
            new Item("Danish", "Danish"),
            new Item("Dutch", "Dutch"),
            new Item("English", "English"),
            new Item("Finnish", "Finnish"),
            new Item("French", "French"),
            new Item("German", "German"),
            new Item("Greek", "Greek"),
            new Item("Hungarian", "Hungarian"),
            new Item("Italian", "Italian"),
            new Item("Japanese", "Japanese"),
            new Item("Korean", "Korean"),
            new Item("Norwegian", "Norwegian"),
            new Item("Polish", "Polish"),
            new Item("Portuguese", "Portuguese"),
            new Item("Romanian", "Romanian"),
            new Item("Russian", "Russian"),
            new Item("Serbian Cyrillic", "Serbian Cyrillic"),
            new Item("Serbian Latin", "Serbian Latin"),
            new Item("Slovak", "Slovak"),
            new Item("Spanish", "Spanish"),
            new Item("Swedish", "Swedish"),
            new Item("Turkish", "Turkish")
        };


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

        public MediaAnalyticsVideoOCR(CloudMediaContext context, string version)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            _context = context;
            _version = version;

            buttonJobOptions.Initialize(_context);
        }

        private void MediaAnalyticsVideoOCR_Load(object sender, EventArgs e)
        {
            comboBoxLanguage.Items.AddRange(VideOCRLanguages.ToArray());
            comboBoxLanguage.SelectedIndex = 0;
            labelProcessorVersion.Text = string.Format(labelProcessorVersion.Text, _version);
            moreinfoprofilelink.Links.Add(new LinkLabel.Link(0, moreinfoprofilelink.Text.Length, Constants.LinkMoreInfoVideoOCR));
        }



        public string JsonConfig()
        {
            // Example of config :

            /*
            {
              "Version": "1.0",
              "Options": {
                "Language": "English",
                "TimeInterval": "00:00:01.5",
                "DetectRegions": [
                  {
                    "Left": "1",
                    "Top": "1",
                    "Width": "1",
                    "Height": "1"
                  },
                  {
                    "Left": "2",
                    "Top": "2",
                    "Width": "2",
                    "Height": "2"
                  }
                ],
                "TextOrientation": "Up"
              }
            }
            */

            dynamic obj = new JObject();
            obj.Version = "1.0";

            string language = ((Item)comboBoxLanguage.SelectedItem).Value as string;
            if (!string.IsNullOrEmpty(language) || checkBoxTimeInterval.Checked)
            {
                obj.Options = new JObject();
                if (!string.IsNullOrEmpty(language))
                {
                    obj.Options.Language = language;
                }
                if (checkBoxTimeInterval.Checked)
                {
                    obj.Options.TimeInterval = TimeSpan.FromSeconds((double)numericUpDownTimeInterval.Value);
                }
            }
            if (checkBoxRestrictDetection.Checked)
            {
                obj.Options.DetectRegions = new JArray() as dynamic;
                dynamic region = new JObject();
                region.Left = numericUpDownRegionX.Value.ToString("F0");
                region.Top = numericUpDownRegionY.Value.ToString("F0");
                region.Width = numericUpDownRegionW.Value.ToString("F0");
                region.Height = numericUpDownRegionH.Value.ToString("F0");
                obj.Options.DetectRegions.Add(region);
            }

            return JsonConvert.SerializeObject(obj);
        }


        private void moreinfoprofilelink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Send the URL to the operating system.
            Process.Start(e.Link.LinkData as string);
        }

        private void checkBoxTimeInterval_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownTimeInterval.Enabled = checkBoxTimeInterval.Checked;
        }

        private void checkBoxOverlayResize_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownRegionX.Enabled = numericUpDownRegionY.Enabled = numericUpDownRegionH.Enabled = numericUpDownRegionW.Enabled = checkBoxRestrictDetection.Checked;
        }
    }
}
