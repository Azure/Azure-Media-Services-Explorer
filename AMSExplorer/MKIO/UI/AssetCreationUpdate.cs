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


using Microsoft.IdentityModel.Tokens;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AMSExplorer
{
    public partial class AssetCreationUpdate : Form
    {
        public enum AssetCreationMode
        {
            Single,
            Multiple
        }

        private AssetCreationMode _mode;

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

        public AssetCreationUpdate(AssetCreationMode mode)
        {
            _mode = mode;

            InitializeComponent();

            //Adjust the UI for the single or multiple assets mode
            switch (_mode)
            {
                case AssetCreationMode.Single:
                    break;

                case AssetCreationMode.Multiple:
                    this.Text = "Create New Assets";
                    labelNewAsset.Text = "Create these assets in MK/IO";

                    lblContainer.Visible = false;
                    textBoxContainer.Visible = false;
                    lblStorage.Visible = false;
                    textBoxStorage.Visible = false;

                    textBoxInstructions.Text = @"Multiple assets have been selected and will be created. 

The tag, {Asset Name}, must be included and allows you to add a prefix or suffix to the new asset names in MK/IO. 

Leave just the tag to keep the same name.

Same behavior for {Asset Description}.";
                    textBoxInstructions.Visible = true;

                    break;
            }
        }

        private void AssetCreationUpdate_Load(object sender, System.EventArgs e)
        {

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

        private void buttonOk_Click(object sender, System.EventArgs e)
        {
            if (_mode == AssetCreationMode.Multiple && (AssetName.IsNullOrEmpty() || !AssetName.Contains("{Asset Name}")))
            {
                MessageBox.Show("Asset Name must contain the {Asset Name} tag for multiple asset creation.", "Creation Failed");
                this.DialogResult = DialogResult.None;
            }
            else if (_mode == AssetCreationMode.Single && textBoxAssetName.Text.IsNullOrEmpty())
            {
                MessageBox.Show("The asset must have an Asset Name.", "Creation Failed");
                this.DialogResult = DialogResult.None;
            }
        }
    }
}