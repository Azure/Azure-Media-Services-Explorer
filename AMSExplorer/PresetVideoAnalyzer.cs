﻿//----------------------------------------------------------------------------------------------
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
using System.Diagnostics;
using System.Globalization;
using System.Windows.Forms;

namespace AMSExplorer
{
    public partial class PresetVideoAnalyzer : Form
    {
        private readonly string _unique;
        public readonly List<string> LanguagesIndexV2s = new List<string> { "en-US", "en-GB", "es-ES", "es-MX", "fr-FR", "it-IT", "ja-JP", "pt-BR", "zh-CN", "de-DE", "ar-EG", "ru-RU", "hi-IN" };


        public string Language => checkBoxAutoLanguage.Checked ? null : ((Item)comboBoxLanguage.SelectedItem).Value as string;

        public string TransformName => textBoxTransformName.Text;

        public string Description => string.IsNullOrWhiteSpace(textBoxDescription.Text) ? null : textBoxDescription.Text;

        public bool AudioOnlyMode => radioButtonAudioOnly.Checked;


        public PresetVideoAnalyzer()
        {
            InitializeComponent();
            Icon = Bitmaps.Azure_Explorer_ico;
            _unique = Guid.NewGuid().ToString().Substring(0, 13);
        }

        private void PresetVideoAnalyzer_Load(object sender, EventArgs e)
        {
            DpiUtils.InitPerMonitorDpi(this);

            // to scale the bitmap in the buttons
            HighDpiHelper.AdjustControlImagesDpiScale(panel1);

            LanguagesIndexV2s.ForEach(c => comboBoxLanguage.Items.Add(new Item((new CultureInfo(c)).DisplayName, c)));
            comboBoxLanguage.SelectedIndex = 0;
            moreinfoprofilelink.Links.Add(new LinkLabel.Link(0, moreinfoprofilelink.Text.Length, Constants.LinkMoreInfoVideoAnalyzer));
            UpdateTransformLabel();
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

        private void PresetVideoAnalyzer_DpiChanged(object sender, DpiChangedEventArgs e)
        {
            DpiUtils.UpdatedSizeFontAfterDPIChange(labelAVAnalyzer, e);

            // to scale the bitmap in the buttons
            HighDpiHelper.AdjustControlImagesDpiScale(panel1);
        }
    }
}
