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


using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace AMSExplorer
{
    public partial class AssetInfoTextTrackCreation : Form
    {
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

        public AssetInfoTextTrackCreation(string blobName, string trackName)
        {
            InitializeComponent();
            Icon = Bitmaps.Azure_Explorer_ico;

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
        }

        private void AssetInfoTextTrackCreation_Load(object sender, EventArgs e)
        {
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
