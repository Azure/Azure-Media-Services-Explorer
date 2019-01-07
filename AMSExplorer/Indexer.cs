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


namespace AMSExplorer
{
    public partial class Indexer : Form
    {
        private CloudMediaContext _context;
        private IndexerOptions formOptions = new IndexerOptions();
        private IndexerOptionsVar optionsVar = new IndexerOptionsVar() { AIB = true, Keywords = true, SAMI = true, TTML = true, WebVTT = true, ForFullCaptions = false };
        private string _version;

        public IndexerOptionsVar IndexerGenerationOptions
        {
            get
            {
                return optionsVar;
            }

        }

        public bool CopySubtitlesFilesToInputAsset
        {
            get
            {
                return checkBoxCopyToInput.Checked; ;
            }

        }

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

        public string IndexerLanguage
        {
            get
            {
                return comboBoxLanguage.Text;
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

        public string IndexerJobName
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

        public string IndexerTitle
        {
            get
            {
                return textBoxTitle.Text;
            }
            set
            {
                textBoxTitle.Text = value;
            }
        }
        public string IndexerDescription
        {
            get
            {
                return textBoxDescription.Text.Replace(Constants.endline, " ");
            }
            set
            {
                textBoxDescription.Text = value;
            }
        }

        public Indexer(CloudMediaContext context, string version)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            _context = context;
            _version = version;

            buttonJobOptions.Initialize(_context);
        }

        private void Indexer_Load(object sender, EventArgs e)
        {
            comboBoxLanguage.SelectedIndex = 0;
            labelProcessorVersion.Text = string.Format(labelProcessorVersion.Text, _version);
            moreinfoprofilelink.Links.Add(new LinkLabel.Link(0, moreinfoprofilelink.Text.Length, Constants.LinkMoreInfoIndexer));
        }

        private void buttonGenOptions_Click(object sender, EventArgs e)
        {
            formOptions.IndexerGenerationOptions = optionsVar;
            if (formOptions.ShowDialog() == DialogResult.OK)
            {
                optionsVar = formOptions.IndexerGenerationOptions;
            }
        }

        public static string LoadAndUpdateIndexerConfiguration(string xmlFileName, string AssetTitle, string AssetDescription, string Language, IndexerOptionsVar optionsVar, string proposedfile = null)
        {
            // Prepare the encryption task template
            XDocument doc = XDocument.Load(xmlFileName);

            var inputxml = doc.Element("configuration").Element("input");
            if (proposedfile != null)
            {
                inputxml.Add(new XAttribute("name", proposedfile));
            }
            if (!string.IsNullOrEmpty(AssetTitle)) inputxml.Add(new XElement("metadata", new XAttribute("key", "title"), new XAttribute("value", AssetTitle)));
            if (!string.IsNullOrEmpty(AssetDescription)) inputxml.Add(new XElement("metadata", new XAttribute("key", "description"), new XAttribute("value", AssetDescription)));

            var settings = doc.Element("configuration").Element("features").Element("feature").Element("settings");
            settings.Add(new XElement("add", new XAttribute("key", "Language"), new XAttribute("value", Language)));
            settings.Add(new XElement("add", new XAttribute("key", "GenerateAIB"), new XAttribute("value", optionsVar.AIB.ToString())));
            settings.Add(new XElement("add", new XAttribute("key", "GenerateKeywords"), new XAttribute("value", optionsVar.Keywords.ToString())));
            settings.Add(new XElement("add", new XAttribute("key", "ForceFullCaption"), new XAttribute("value", optionsVar.ForFullCaptions.ToString())));

            string cformats = optionsVar.TTML ? "ttml;" : string.Empty;
            cformats += optionsVar.SAMI ? "sami;" : string.Empty;
            cformats += optionsVar.WebVTT ? "webvtt" : string.Empty;
            settings.Add(new XElement("add", new XAttribute("key", "CaptionFormats"), new XAttribute("value", cformats)));

            return doc.Declaration.ToString() + Environment.NewLine + doc.ToString();
        }

        private void moreinfoprofilelink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Send the URL to the operating system.
            Process.Start(e.Link.LinkData as string);
        }
    }
}
