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

        public string OCRLanguage
        {
            get
            {
                return ((Item)comboBoxLanguage.SelectedItem).Value as string;
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

        public bool OutputTxt
        {
            get
            {
                return checkBoxTxt.Checked;
            }
        }

        public bool OutputXml
        {
            get
            {
                return checkBoxXml.Checked;
            }
        }

        public decimal TimeInterval
        {
            get
            {
                return numericUpDownTimeInterval.Value;
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

        public static string LoadAndUpdateVideoOCRConfiguration(string xmlFileName, string AssetTitle, string AssetDescription, string Language, decimal timeInterval, bool optionTXT, bool optionXML)
        {
            // Prepare the encryption task template
            XDocument doc = XDocument.Load(xmlFileName);

            var inputxml = doc.Element("configuration").Element("input");
            /*
            if (proposedfile != null)
            {
                inputxml.Add(new XAttribute("name", proposedfile));
            }
            if (!string.IsNullOrEmpty(AssetTitle)) inputxml.Add(new XElement("metadata", new XAttribute("key", "title"), new XAttribute("value", AssetTitle)));
            if (!string.IsNullOrEmpty(AssetDescription)) inputxml.Add(new XElement("metadata", new XAttribute("key", "description"), new XAttribute("value", AssetDescription)));
            */

            var settings = doc.Element("configuration").Element("features").Element("feature").Element("settings");
            settings.Add(new XElement("add", new XAttribute("key", "Language"), new XAttribute("value", Language)));
            settings.Add(new XElement("add", new XAttribute("key", "TimeInterval"), new XAttribute("value", timeInterval.ToString())));

            string separator = optionTXT && optionXML ? "|" : "";
            string cformats = optionTXT ? "txt" + separator : string.Empty;
            cformats += optionXML ? "xml" : string.Empty;
            settings.Add(new XElement("add", new XAttribute("key", "OutputFormats"), new XAttribute("value", cformats)));

            return doc.Declaration.ToString() + doc.ToString();
        }



        private void moreinfoprofilelink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Send the URL to the operating system.
            Process.Start(e.Link.LinkData as string);
        }
    }
}
