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
using System.Globalization;

namespace AMSExplorer
{
    public partial class IndexerV2 : Form
    {
        private CloudMediaContext _context;
        private IndexerOptions formOptions = new IndexerOptions(true);
        private IndexerOptionsVar optionsVar = new IndexerOptionsVar() { AIB = false, Keywords = false, SAMI = true, TTML = true, WebVTT = true };
        private string _version;

        /*
        public readonly List<Item> LanguagesIndexV2 = new List<Item> {
            new Item("English", "EnUs"),
            new Item("Spanish", "EsEs"),
            new Item("Mandarin Chinese", "ZhCn"),
            new Item("French", "FrFr"),
            new Item("German", "DeDe"),
            new Item("Italian", "ItIt"),
            new Item("Portuguese", "PtBr"),
            new Item("Arabic (Egyptian)", "ArEg"),
            new Item("Japanese", "JaJp")
        };
        */

        public readonly List<string> LanguagesIndexV2s = new List<string> { "EnUs", "EsEs", "ZhCn", "FrFr", "DeDe", "ItIt", "PtBr", "ArEg", "JaJp" };

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
                return checkBoxCopyToInput.Checked;
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

        public IndexerV2(CloudMediaContext context, string version)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            _context = context;
            _version = version;

            buttonJobOptions.Initialize(_context);
        }

        private void IndexerV2_Load(object sender, EventArgs e)
        {
            //comboBoxLanguage.Items.AddRange(LanguagesIndexV2.ToArray());
            LanguagesIndexV2s.ForEach(c => comboBoxLanguage.Items.Add(new Item((new CultureInfo(c.Substring(0, 2) + "-" + c.Substring(2, 2))).DisplayName, c)));
            comboBoxLanguage.SelectedIndex = 0;
            labelProcessorVersion.Text = string.Format(labelProcessorVersion.Text, _version);
            moreinfoprofilelink.Links.Add(new LinkLabel.Link(0, moreinfoprofilelink.Text.Length, Constants.LinkMoreInfoIndexerV2));
        }

        private void buttonGenOptions_Click(object sender, EventArgs e)
        {
            formOptions.IndexerGenerationOptions = optionsVar;
            if (formOptions.ShowDialog() == DialogResult.OK)
            {
                optionsVar = formOptions.IndexerGenerationOptions;
            }
        }

        public string JsonConfig()
        {
            // Example of config :
            /*
              	{
                      "Features": [{
                                      "Options": {
                                                      "Formats": ["WebVtt", "Sami", “TTML”],
                                                      "Language": "EnUs",
                                                      "Type": "RecoOptions"
                                      },
                                      "Type": "SpReco"
                      }],
                      "Version": 1.0
      }
             */

            dynamic obj = new JObject();
            obj.Version = "1.0";
            obj.Features = new JArray();
            dynamic Feature = new JObject();
            obj.Features.Add(Feature);
            dynamic Option = new JObject();
            Feature.Options = Option;
            dynamic Format = new JArray();

            var options = IndexerGenerationOptions;
            if (options.SAMI) Format.Add("Sami");
            if (options.WebVTT) Format.Add("WebVtt");
            if (options.TTML) Format.Add("TTML");

            Option.Formats = Format;
            Option.Language = ((Item)comboBoxLanguage.SelectedItem).Value as string;
            Option.Type = "RecoOptions";
            Feature.Type = "SpReco";

            return JsonConvert.SerializeObject(obj);
        }

        private void moreinfoprofilelink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Send the URL to the operating system.
            Process.Start(e.Link.LinkData as string);
        }
    }
}
