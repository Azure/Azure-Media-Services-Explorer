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


using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AMSExplorer
{
    public partial class AssetCreationUpdate : Form
    {
        public string AssetName
        {
            get => textBoxAssetName.Text;
            set => textBoxAssetName.Text = value;
        }

        public string AssetDescription
        {
            get => string.IsNullOrWhiteSpace(textBoxDescription.Text) ? null : textBoxDescription.Text;
            set => textBoxDescription.Text = value;
        }


        public string AssetContainer
        {
            set => textBoxContainer.Text = value;
        }

        public string AssetStorage
        {
            set => textBoxStorage.Text = value;
        }

        public AssetCreationUpdate()
        {
            InitializeComponent();

        }

        private void AssetCreationUpdate_DpiChanged(object sender, DpiChangedEventArgs e)
        {
            // DpiUtils.UpdatedSizeFontAfterDPIChange(labelNewAsset, e);
        }

        private void AssetCreationUpdate_Load(object sender, System.EventArgs e)
        {
            // DpiUtils.InitPerMonitorDpi(this);
        }

        private void TextBoxAssetName_TextChanged(object sender, System.EventArgs e)
        {

        }

        internal static bool IsAssetNameValid(string name)
        {
            Regex reg = new(@"[<>%&:\\?/*+.']", RegexOptions.Compiled);
            return (name.Length > 0 && name.Length < 261 && !reg.IsMatch(name));
        }

        private void TextBoxContainer_TextChanged(object sender, System.EventArgs e)
        {

        }

        private void AssetCreationUpdate_Shown(object sender, System.EventArgs e)
        {
            Telemetry.TrackPageView(this.Name);
        }
    }
}