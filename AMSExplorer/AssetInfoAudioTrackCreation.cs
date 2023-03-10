//----------------------------------------------------------------------------------------------
//    Copyright 2023 Microsoft Corporation
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


using Azure.ResourceManager.Media.Models;
using DocumentFormat.OpenXml.Office2013.Drawing.Chart;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AMSExplorer
{
    public partial class AssetInfoAudioTrackCreation : Form
    {
        private bool _editMode;
        private AudioTrack _audioTrack;

        public string LanguageDisplayName
        {
            get
            {
                return textBoxDisplayName.Text;
            }
        }

        public string LanguageCode
        {
            get
            {
                return checkBoxLanguage.Checked ? ((Item)comboBoxTexttrackLanguage.SelectedItem).Value : null;
            }
        }

        public string TrackName
        {
            get
            {
                return textBoxTrackName.Text;
            }
        }

        public string DashRole
        {
            get
            {
                return ((Item)comboBoxDashRole.SelectedItem).Value;
            }
        }

        public bool HLSDefaultTrack
        {
            get
            {
                return checkBoxHLSSetAsDefault.Checked;
            }
        }

        public bool HLSIsDescriptiveAudio
        {
            get
            {
                return checkBoxIsHLSDescriptiveAudio.Checked;
            }
        }

        public int? Mp4TrackId
        {
            get
            {
                int? result = null;

                if (!string.IsNullOrEmpty(textBoxTrackId.Text))
                {
                    int res;
                    if (int.TryParse(textBoxTrackId.Text, out res))
                    {
                        result = res;
                    }
                }
                return result;
            }
        }


        public AssetInfoAudioTrackCreation(string blobName, string trackName, AudioTrack audioTrack = null)
        {
            InitializeComponent();
            Icon = Bitmaps.Azure_Explorer_ico;
            _editMode = audioTrack != null;

            List<CultureInfo> cultures = CultureInfo.GetCultures(CultureTypes.AllCultures).ToList();

            foreach (CultureInfo culture in cultures)
            {
                comboBoxTexttrackLanguage.Items.Add(new Item(culture.EnglishName, culture.Name));
                if (!_editMode && culture.Name == "en-US")
                {
                    comboBoxTexttrackLanguage.SelectedIndex = comboBoxTexttrackLanguage.Items.Count - 1;
                }
            }


            comboBoxDashRole.Items.Add(new Item("", null));
            comboBoxDashRole.Items.Add(new Item("main", "main"));
            comboBoxDashRole.Items.Add(new Item("alternate", "alternate"));
            comboBoxDashRole.Items.Add(new Item("supplementary", "supplementary"));
            comboBoxDashRole.Items.Add(new Item("commentary", "commentary"));
            comboBoxDashRole.Items.Add(new Item("dub", "dub"));
            comboBoxDashRole.Items.Add(new Item("emergency", "emergency"));
            comboBoxDashRole.SelectedIndex = 0;

            labelBlobName.Text = blobName;
            textBoxTrackName.Text = trackName;

            _audioTrack = audioTrack;

        }

        private void AssetInfoTextTrackCreation_Load(object sender, EventArgs e)
        {
            if (_editMode)
            {
                buttonUpdate.Text = (string)buttonUpdate.Tag;
                textBoxTrackName.Enabled = false;
                textBoxTrackId.Text = _audioTrack.Mpeg4TrackId?.ToString();
                textBoxTrackId.Enabled = false; // Track Id cannot be edited after creation

                if (_audioTrack.LanguageCode != null)
                {
                    checkBoxLanguage.Checked = true;
                    comboBoxTexttrackLanguage.Items.Add(new Item(_audioTrack.LanguageCode, _audioTrack.LanguageCode));
                    comboBoxTexttrackLanguage.SelectedIndex = comboBoxTexttrackLanguage.Items.Count - 1;
                }
                checkBoxLanguage.Enabled = false;
                comboBoxTexttrackLanguage.Enabled = false;

                textBoxDisplayName.Text = _audioTrack.DisplayName;
                if (_audioTrack.DashRole != null)
                {
                    comboBoxDashRole.Items.Add(new Item(_audioTrack.DashRole, _audioTrack.DashRole));
                    comboBoxDashRole.SelectedIndex = comboBoxDashRole.Items.Count - 1;
                }
                if (_audioTrack.HlsSettings != null)
                {
                    if (_audioTrack.HlsSettings.IsDefault != null)
                    {
                        checkBoxHLSSetAsDefault.Checked = (bool)_audioTrack.HlsSettings.IsDefault;
                    }
                    if (_audioTrack.HlsSettings.Characteristics == "public.accessibility.describes-video")
                    {
                        checkBoxIsHLSDescriptiveAudio.Checked = true;
                    }
                }
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {

        }

        private void AssetInfoTextTrackCreation_DpiChanged(object sender, DpiChangedEventArgs e)
        {
            // DpiUtils.UpdatedSizeFontAfterDPIChange(label2, e);
        }

        private void AssetInfoTextTrackCreation_Shown(object sender, EventArgs e)
        {
            Telemetry.TrackPageView(this.Name);
        }


        private void checkBoxLanguage_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxTexttrackLanguage.Enabled = checkBoxLanguage.Checked;
        }
    }
}
