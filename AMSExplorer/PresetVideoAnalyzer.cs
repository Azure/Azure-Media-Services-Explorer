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
    public partial class PresetVideoAnalyzer : Form
    {
        private IndexerOptions formOptions = new IndexerOptions(true);
        private IndexerOptionsVar optionsVar = new IndexerOptionsVar() { AIB = false, Keywords = false, SAMI = true, TTML = true, WebVTT = true };
        private string _unique;
        public readonly List<string> LanguagesIndexV2s = new List<string> { "en-US", "en-GB", "es-ES", "es-MX", "fr-FR", "it-IT", "ja-JP", "pt-BR", "zh-CN", "de-DE", "ar-EG", "ru-RU", "hi-IN" };


        public string Language
        {
            get
            {
                return checkBoxAutoLanguage.Checked ? null : ((Item)comboBoxLanguage.SelectedItem).Value as string;
            }
        }

        public string TransformName
        {
            get
            {
                return textBoxTransformName.Text;
            }
        }

        public string Description
        {
            get
            {
                return string.IsNullOrWhiteSpace(textBoxDescription.Text) ? null : textBoxDescription.Text;
            }
        }

        public bool AudioOnlyMode
        {
            get
            {
                return radioButtonAudioOnly.Checked;
            }
        }


        public PresetVideoAnalyzer()
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            _unique = Guid.NewGuid().ToString().Substring(0, 13);
        }

        private void PresetVideoAnalyzer_Load(object sender, EventArgs e)
        {
            LanguagesIndexV2s.ForEach(c => comboBoxLanguage.Items.Add(new Item((new CultureInfo(c)).DisplayName, c)));
            comboBoxLanguage.SelectedIndex = 0;
            moreinfoprofilelink.Links.Add(new LinkLabel.Link(0, moreinfoprofilelink.Text.Length, Constants.LinkMoreInfoVideoAnalyzer));
            UpdateTransformLabel();
        }

        private void buttonGenOptions_Click(object sender, EventArgs e)
        {
            formOptions.IndexerGenerationOptions = optionsVar;
            if (formOptions.ShowDialog() == DialogResult.OK)
            {
                optionsVar = formOptions.IndexerGenerationOptions;
            }
        }

        private void moreinfoprofilelink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Send the URL to the operating system.
            Process.Start(e.Link.LinkData as string);
        }

        private void radioButtonAudioAndVideo_CheckedChanged(object sender, EventArgs e)
        {
            UpdateTransformLabel();
        }

        private void UpdateTransformLabel()
        {
            if (AudioOnlyMode)
            {
                textBoxTransformName.Text = "AudioAnalyzer-" + (Language ?? "Auto");// + "-" + _unique;

            }
            else
            {
                textBoxTransformName.Text = "VideoAnalyzer-" + (Language ?? "Auto");// + "-" + _unique;
            }
        }

        private void comboBoxLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateTransformLabel();
        }

        private void checkBoxAutoLanguage_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxLanguage.Enabled = !checkBoxAutoLanguage.Checked;
            UpdateTransformLabel();
        }
    }
}
