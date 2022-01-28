//----------------------------------------------------------------------------------------------
//    Copyright 2022 Microsoft Corporation
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

using Microsoft.Azure.Management.Media.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace AMSExplorer
{
    public partial class PresetVideoAnalyzer : Form
    {
        private readonly string _unique;
        private readonly string _existingTransformName;
        private readonly string _existingTransformDesc;

        // langage codes supported : https://go.microsoft.com/fwlink/?linkid=2109463
        public readonly List<string> LanguagesIndexV2s = new() { "ar-BH", "ar-EG", "ar-IQ", "ar-JO", "ar-KW", "ar-LB", "ar-OM", "ar-QA", "ar-SA", "ar-SY", "zh-CN", "da-DK", "en-US", "en-GB", "en-AU", "fi-FI", "fr-FR", "fr-CA", "de-DE", "he-IL", "hi-IN", "it-IT", "ko-KR", "ja-JP", "nb-NO", "fa-IR", "pt-BR", "pt-PT", "ru-RU", "es-ES", "es-MX", "sv-SE", "th-TH", "tr-TR" };

        public string Language => checkBoxAutoLanguage.Checked ? null : ((Item)comboBoxLanguage.SelectedItem).Value;

        public string TransformName => textBoxTransformName.Text;

        public string TransformDescription => string.IsNullOrWhiteSpace(textBoxDescription.Text) ? null : textBoxDescription.Text;

        public InsightsType InsightsMode => radioButtonAudioOnly.Checked ? InsightsType.AudioInsightsOnly : radioButtonVideoOnly.Checked ? InsightsType.VideoInsightsOnly : InsightsType.AllInsights;

        public AudioAnalysisMode AudioAnalysisMode => radioButtonAudioBasic.Checked ? AudioAnalysisMode.Basic : AudioAnalysisMode.Standard;


        public PresetVideoAnalyzer(string existingTransformName = null, string existingTransformDesc = null)
        {
            InitializeComponent();
            Icon = Bitmaps.Azure_Explorer_ico;
            _unique = Program.GetUniqueness();
            _existingTransformName = existingTransformName;
            _existingTransformDesc = existingTransformDesc;
        }

        private void PresetVideoAnalyzer_Load(object sender, EventArgs e)
        {
            // DpiUtils.InitPerMonitorDpi(this);

            LanguagesIndexV2s.ForEach(c => comboBoxLanguage.Items.Add(new Item((new CultureInfo(c)).DisplayName, c)));
            comboBoxLanguage.SelectedIndex = LanguagesIndexV2s.IndexOf("en-US");
            moreinfoprofilelink.Links.Add(new LinkLabel.Link(0, moreinfoprofilelink.Text.Length, Constants.LinkMoreInfoVideoAnalyzer));

            textBoxDescription.Text = _existingTransformDesc;

            UpdateTransformLabel();
        }


        private void moreinfoprofilelink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Send the URL to the operating system.
            var p = new Process
            {
                StartInfo = new ProcessStartInfo { FileName = e.Link.LinkData as string, UseShellExecute = true }
            };
            p.Start();
        }

        private void radioButtonAudioAndVideo_CheckedChanged(object sender, EventArgs e)
        {
            var radiobutton = ((RadioButton)sender);
            if (!radiobutton.Checked)
            {
                return;
            }
            ManageAudioGroup();
            UpdateTransformLabel();
        }

        private void ManageAudioGroup()
        {
            groupBoxAudioMode.Enabled = !radioButtonVideoOnly.Checked;
        }

        private void UpdateTransformLabel()
        {
            if (_existingTransformName != null)
            {
                textBoxTransformName.Text = _existingTransformName;
                textBoxTransformName.Enabled = false;
            }
            else
            {
                string audioMode = string.Empty;
                if (InsightsMode != InsightsType.VideoInsightsOnly)
                {
                    if (AudioAnalysisMode == AudioAnalysisMode.Standard)
                    {
                        audioMode = "-AudioStandard";
                    }
                    else // Basic
                    {
                        audioMode = "-AudioBasic";
                    }
                }

                if (InsightsMode == InsightsType.AudioInsightsOnly)
                {
                    textBoxTransformName.Text = "AudioAnalyzer" + audioMode + "-" + (Language ?? "Auto");
                }
                else
                {
                    textBoxTransformName.Text = "VideoAnalyzer" + audioMode + "-" + (Language ?? "Auto");
                }
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

        private void PresetVideoAnalyzer_DpiChanged(object sender, DpiChangedEventArgs e)
        {
            // DpiUtils.UpdatedSizeFontAfterDPIChange(labelAVAnalyzer, e);
        }

        private void radioButtonAudioBasic_CheckedChanged(object sender, EventArgs e)
        {
            UpdateTransformLabel();
        }

        private void PresetVideoAnalyzer_Shown(object sender, EventArgs e)
        {
            Telemetry.TrackPageView(this.Name);
        }
    }
}
