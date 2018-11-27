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
using System.Xml.Linq;
using System.Security;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Globalization;

namespace AMSExplorer
{
    public partial class IndexerV2 : Form
    {
        private IndexerOptions formOptions = new IndexerOptions(true);
        private IndexerOptionsVar optionsVar = new IndexerOptionsVar() { AIB = false, Keywords = false, SAMI = true, TTML = true, WebVTT = true };

            public readonly List<string> LanguagesIndexV2s = new List<string> { "en-US", "en-GB", "es-ES", "es-MX", "fr-FR", "it-IT", "ja-JP", "pt-BR", "zh-CN" };


        public IndexerOptionsVar IndexerGenerationOptions
        {
            get
            {
                return optionsVar;
            }
        }
       

        public string Language
        {
            get
            {
                return  ((Item)comboBoxLanguage.SelectedItem).Value as string;
            }
        }


        public IndexerV2()
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;

           // buttonJobOptions.Initialize(_context);
        }

        private void IndexerV2_Load(object sender, EventArgs e)
        {
            //comboBoxLanguage.Items.AddRange(LanguagesIndexV2.ToArray());
            LanguagesIndexV2s.ForEach(c => comboBoxLanguage.Items.Add(new Item((new CultureInfo(c)).DisplayName, c)));
            comboBoxLanguage.SelectedIndex = 0;
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
