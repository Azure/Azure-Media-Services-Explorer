//----------------------------------------------------------------------------------------------
//    Copyright 2021 Microsoft Corporation
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
using System.Windows.Forms;

namespace AMSExplorer
{
    public partial class PresetVideoAnalyzer : Form
    {
        private readonly string _unique;
        private readonly string _existingTransformName;
        private readonly string _existingTransformDesc;

        // langage codes supported : https://go.microsoft.com/fwlink/?linkid=2109463
        public readonly List<string> LanguagesIndexV2s = new List<string> { "en-US", "en-GB", "en-AU", "es-ES", "es-MX", "fr-FR", "fr-CA", "it-IT", "ja-JP", "pt-BR", "zh-CN", "de-DE", "ar-BH", "ar-EG", "ar-IQ", "ar-JO", "ar-KW", "ar-LB", "ar-OM", "ar-QA", "ar-SA", "ar-SY", "ru-RU", "hi-IN", "ko-KR", "da-DK", "nb-NO", "sv-SE", "fi-FI", "th-TH", "tr-TR" };

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
            DpiUtils.InitPerMonitorDpi(this);

            // to scale the bitmap in the buttons
            HighDpiHelper.AdjustControlImagesDpiScale(panel1);

            LanguagesIndexV2s.ForEach(c => comboBoxLanguage.Items.Add(new Item((new CultureInfo(c)).DisplayName, c)));
            comboBoxLanguage.SelectedIndex = 0;
            moreinfoprofilelink.Links.Add(new LinkLabel.Link(0, moreinfoprofilelink.Text.Length, Constants.LinkMoreInfoVideoAnalyzer));

            textBoxDescription.Text = _existingTransformDesc;

            UpdateTransformLabel();
        }


        private void moreinfoprofilelink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Send the URL to the operating system.
            var p = new Process(); p.StartInfo = new ProcessStartInfo { FileName = e.Link.LinkData as string, UseShellExecute = true }; p.Start();
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
            DpiUtils.UpdatedSizeFontAfterDPIChange(labelAVAnalyzer, e);

            // to scale the bitmap in the buttons
            HighDpiHelper.AdjustControlImagesDpiScale(panel1);
        }

        private void radioButtonAudioBasic_CheckedChanged(object sender, EventArgs e)
        {
            UpdateTransformLabel();
        }
    }
}
