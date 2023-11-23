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
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace AMSExplorer
{
    public partial class AssetInfoTextTrackCreation : Form
    {
        private const string stringTranscribesSpokenDialog = "public.accessibility.transcribes-spoken-dialog";
        private const string stringDescribesMusicSound = "public.accessibility.describes-music-and-sound";
        private const string stringEasyToRead = "public.easy-to-read";
        private bool _editMode;
        private TextTrack _textTrack;

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

        public bool HLSDefaultTrack
        {
            get
            {
                return checkBoxHLSSetAsDefault.Checked;
            }
        }

        public bool HLSSetForced
        {
            get
            {
                return checkBoxIsHLSSetForced.Checked;
            }
        }

        public bool VisibleInPlayer
        {
            get
            {
                return checkBoxPlayerVisible.Checked;
            }
        }

        public string HLSAccessibilityCharacteristics
        {
            get
            {
                List<string> tabS = new();
                if (checkBoxAccessTranscribe.Checked)
                {
                    tabS.Add(stringTranscribesSpokenDialog);
                }
                if (checkBoxAccessMusicSound.Checked)
                {
                    tabS.Add(stringDescribesMusicSound);
                }
                if (checkBoxAccessEditedEaseReading.Checked)
                {
                    tabS.Add(stringEasyToRead);
                }
                return tabS.Count == 0 ? null : string.Join(",", tabS);
            }
        }


        public AssetInfoTextTrackCreation(string blobName, string trackName, TextTrack textTrack = null)
        {
            InitializeComponent();
            Icon = Bitmaps.Azure_Explorer_ico;
            _editMode = textTrack != null;

            List<CultureInfo> cultures = CultureInfo.GetCultures(CultureTypes.AllCultures).ToList();

            foreach (CultureInfo culture in cultures)
            {
                comboBoxTexttrackLanguage.Items.Add(new Item(culture.EnglishName, culture.Name));
                if (culture.Name == "en-US")
                {
                    comboBoxTexttrackLanguage.SelectedIndex = comboBoxTexttrackLanguage.Items.Count - 1;
                }
            }

            labelBlobName.Text = blobName;
            textBoxTrackName.Text = trackName;
            _textTrack = textTrack;
        }

        private void AssetInfoTextTrackCreation_Load(object sender, EventArgs e)
        {
            if (_editMode)
            {
                buttonUpdate.Text = (string)buttonUpdate.Tag;
                textBoxTrackName.Enabled = false;

                if (_textTrack.LanguageCode != null)
                {
                    checkBoxLanguage.Checked = true;
                    comboBoxTexttrackLanguage.Items.Add(new Item(_textTrack.LanguageCode, _textTrack.LanguageCode));
                    comboBoxTexttrackLanguage.SelectedIndex = comboBoxTexttrackLanguage.Items.Count - 1;
                }
                checkBoxLanguage.Enabled = false;
                comboBoxTexttrackLanguage.Enabled = false;

                textBoxDisplayName.Text = _textTrack.DisplayName;

                if (_textTrack.PlayerVisibility != null)
                {
                    checkBoxPlayerVisible.Checked = _textTrack.PlayerVisibility == PlayerVisibility.Visible ? true : false;
                }

                if (_textTrack.HlsSettings != null)
                {
                    if (_textTrack.HlsSettings.IsDefault != null)
                    {
                        checkBoxHLSSetAsDefault.Checked = (bool)_textTrack.HlsSettings.IsDefault;
                    }
                    if (_textTrack.HlsSettings.IsForced != null)
                    {
                        checkBoxIsHLSSetForced.Checked = (bool)_textTrack.HlsSettings.IsForced;
                    }
                }

                if (_textTrack.HlsSettings.Characteristics != null)
                {
                    checkBoxAccessTranscribe.Checked = _textTrack.HlsSettings.Characteristics.Contains(stringTranscribesSpokenDialog);
                    checkBoxAccessEditedEaseReading.Checked = _textTrack.HlsSettings.Characteristics.Contains(stringEasyToRead);
                    checkBoxAccessMusicSound.Checked = _textTrack.HlsSettings.Characteristics.Contains(stringDescribesMusicSound);
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
