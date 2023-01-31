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


using Azure.ResourceManager.Media;
using Azure.ResourceManager.Media.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AMSExplorer
{
    public partial class ChooseStreamingEndpoint : Form
    {
        private readonly MediaAssetResource _asset;
        private readonly string _filter;
        private readonly PlayerType _playertype;
        private string _path;
        private readonly bool _displayBrowserSelection;
        private readonly AMSClientV3 _amsClient;
        private IList<MediaAssetStreamingLocator> _locators = new List<MediaAssetStreamingLocator>();
        private readonly bool _emptyliveOutput;

        public StreamingEndpointResource SelectStreamingEndpoint
        {
            get
            {
                string val = (listBoxSE.SelectedItem as Item).Value;
                string seName = val.Split("|".ToCharArray())[0];

                return Task.Run(() => _amsClient.GetStreamingEndpointAsync(seName)).GetAwaiter().GetResult();
            }
        }

        public string SelectedFilters
        {
            get
            {
                string filters = string.Empty;
                bool first = true;
                foreach (object f in listViewFilters.CheckedItems)
                {
                    string v = (f as ListViewItem).SubItems[1].Text;
                    if (v != string.Empty)
                    {
                        filters += (first ? string.Empty : ";") + v;
                        first = false;
                    }
                }
                if (string.IsNullOrEmpty(filters))
                {
                    filters = null;
                }
                return filters;
            }
        }

        public string ReturnSelectCustomHostName
        {
            get
            {
                string val = (listBoxSE.SelectedItem as Item).Value;
                return val.Split("|".ToCharArray())[1];
            }
        }

        public string ReturnSelectedBrowser => (comboBoxBrowser?.SelectedItem as Item)?.Value;

        public bool ReturnHttps => radioButtonHttps.Checked;

        public string ReturnHLSAudioTrackName
        {
            get
            {
                if (radioButtonHLSv3.Checked && !string.IsNullOrWhiteSpace(textBoxHLSAudioTrackName.Text))
                {
                    return textBoxHLSAudioTrackName.Text;
                }
                else
                {
                    return null;
                }
            }
        }

        public bool ReturnHLSNoAudioOnlyMode => checkBoxNoAudioOnly.Checked;

        public AMSOutputProtocols ReturnStreamingProtocol
        {
            get
            {
                if (radioButtonDASHCSF.Checked)
                {
                    return AMSOutputProtocols.DashCsf;
                }
                else if (radioButtonSmoothLegacy.Checked)
                {
                    return AMSOutputProtocols.SmoothLegacy;
                }
                else if (radioButtonDASHCMAF.Checked)
                {
                    return AMSOutputProtocols.DashCmaf;
                }
                else if (radioButtonHLSv3.Checked)
                {
                    return AMSOutputProtocols.HLSv3;
                }
                else if (radioButtonHLSv4.Checked)
                {
                    return AMSOutputProtocols.HLSv4;
                }
                else if (radioButtonHLSCMAF.Checked)
                {
                    return AMSOutputProtocols.HLSCmaf;
                }

                return AMSOutputProtocols.Smooth;
            }
        }

        public string UpdatedPath => _path;



        public ChooseStreamingEndpoint(AMSClientV3 client, MediaAssetResource asset, string path, string filter = null, PlayerType playertype = PlayerType.AzureMediaPlayer, bool displayBrowserSelection = false, bool emptyliveOutput = false)
        {
            InitializeComponent();
            Icon = Bitmaps.Azure_Explorer_ico;
            _amsClient = client;
            _asset = asset;
            _filter = filter;
            _playertype = playertype;
            _path = path;
            _displayBrowserSelection = displayBrowserSelection;
            _emptyliveOutput = emptyliveOutput;
        }


        private async void ChooseStreamingEndpoint_Load(object sender, EventArgs e)
        {
            // DpiUtils.InitPerMonitorDpi(this);

            label.Text = string.Format(label.Text, _asset.Data.Name);

            // SE List
            // StreamingEndpoint BestSE = Task.Run(async () => await AssetInfo.GetBestStreamingEndpointAsync(_client)).Result;
            var BestSE = await AssetTools.GetBestStreamingEndpointAsync(_amsClient);

            var myStreamingEndpoints = _amsClient.AMSclient.GetStreamingEndpoints().GetAllAsync();
            //IPage<StreamingEndpoint> myStreamingEndpoints = Task.Run(() => _amsClient.AMSclient.getst.StreamingEndpoints.ListAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName)).GetAwaiter().GetResult();

            await foreach (var se in myStreamingEndpoints)
            {
                listBoxSE.Items.Add(new Item(string.Format(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_012ScaleUnit, se.Data.Name, se.Data.ResourceState, StreamingEndpointInformation.ReturnTypeSE(se)), se.Data.Name + "|" + se.Data.HostName));
                if (se.Id == BestSE.Id)
                {
                    listBoxSE.SelectedIndex = listBoxSE.Items.Count - 1;
                }

                foreach (string custom in se.Data.CustomHostNames)
                {
                    listBoxSE.Items.Add(new Item(string.Format(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_012ScaleUnitCustomHostname3, se.Data.Name, se.Data.ResourceState, StreamingEndpointInformation.ReturnTypeSE(se), custom), se.Data.Name + "|" + custom));
                }
            }


            // Filters

            // asset filters
            //  List<AssetFilter> assetFilters = new();
            // IPage<AssetFilter> assetFiltersPage = await _amsClient.AMSclient.AssetFilters.ListAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, _asset.Name);



            /*
            while (assetFiltersPage != null)
            {
                assetFilters.AddRange(assetFiltersPage);
                if (assetFiltersPage.NextPageLink != null)
                {
                    assetFiltersPage = await _amsClient.AMSclient.AssetFilters.ListNextAsync(assetFiltersPage.NextPageLink);
                }
                else
                {
                    assetFiltersPage = null;
                }
            }
            */

            // Asset filters
            var assetFilters = _asset.GetMediaAssetFilters().GetAllAsync();
            listViewFilters.BeginUpdate();
            List<string> afiltersnames = new();// assetFilters.Select(a => a.Data.Name).ToList();
            await foreach (var filter in assetFilters)
            {
                ListViewItem lvitem = new(new string[] { AMSExplorer.Properties.Resources.ChooseStreamingEndpoint_ChooseStreamingEndpoint_Load_AssetFilter + filter.Data.Name, filter.Data.Name });
                if (_filter != null && filter.Data.Name == _filter)
                {
                    lvitem.Checked = true;
                }
                listViewFilters.Items.Add(lvitem);
                afiltersnames.Add(filter.Data.Name);
            }

            // account filters
            var acctFilters = _amsClient.AMSclient.GetMediaServicesAccountFilters().GetAllAsync();
            await foreach (var filter in acctFilters)
            {
                ListViewItem lvitem = new(new string[] { AMSExplorer.Properties.Resources.ChooseStreamingEndpoint_ChooseStreamingEndpoint_Load_GlobalFilter + filter.Data.Name, filter.Data.Name });
                if (_filter != null && filter.Data.Name == _filter && listViewFilters.CheckedItems.Count == 0) // only if not already selected (asset filter priority > account filter)
                {
                    lvitem.Checked = true;
                }
                if (afiltersnames.Contains(filter.Data.Name)) // global filter with same name than asset filter
                {
                    lvitem.ForeColor = Color.Gray;
                }
                listViewFilters.Items.Add(lvitem);
            }
            listViewFilters.EndUpdate();

            if (_playertype == PlayerType.DASHIFRefPlayer)
            {
                radioButtonDASHCSF.Checked = true;
            }

            comboBoxBrowser.Items.Add(new Item(AMSExplorer.Properties.Resources.ChooseStreamingEndpoint_ChooseStreamingEndpoint_Load_DefaultBrowser, string.Empty));
            if (_displayBrowserSelection)
            { // let's add the browser options to lplayback the content (IE, Edge, Chrome...)
                if (IsWindows10())
                {
                    comboBoxBrowser.Items.Add(new Item(Constants.BrowserEdge[0], Constants.BrowserEdge[1]));
                }
                comboBoxBrowser.Items.Add(new Item(Constants.BrowserChrome[0], Constants.BrowserChrome[1]));
                comboBoxBrowser.SelectedIndex = 0;
            }
            comboBoxBrowser.Visible = _displayBrowserSelection;

            UpdatePreviewUrl();
            FillLocatorComboInPolicyTab();
        }


        private void FillLocatorComboInPolicyTab()
        {
            comboBoxPolicyLocators.Items.Clear();
            comboBoxPolicyLocators.BeginUpdate();


            /*  _locators =
              Task.Run(() =>
                          _amsClient.AMSclient.Assets.ListStreamingLocatorsAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, _asset.Name)
                          ).GetAwaiter().GetResult().StreamingLocators;*/

            var locators = _asset.GetStreamingLocators();
            _locators.Clear();
            foreach (var locator in locators)
            {
                _locators.Add(locator);
                int index = comboBoxPolicyLocators.Items.Add(new Item(locator.Name, locator.Name));
                if (_path.Contains(locator.StreamingLocatorId.ToString()))
                {
                    comboBoxPolicyLocators.SelectedIndex = index;
                }
            }

            if (comboBoxPolicyLocators.SelectedIndex == -1 && comboBoxPolicyLocators.Items.Count > 0)
            {
                comboBoxPolicyLocators.SelectedIndex = 0;
            }

            comboBoxPolicyLocators.EndUpdate();
        }

        private static bool IsWindows10()
        {
            RegistryKey reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion");

            string productName = (string)reg.GetValue("ProductName");

            return productName.StartsWith("Windows 10");
        }

        private void RadioButtonHLSv3_CheckedChanged(object sender, EventArgs e)
        {
            textBoxHLSAudioTrackName.Enabled = checkBoxNoAudioOnly.Enabled = labelaudiotrackname.Enabled = radioButtonHLSv3.Checked;
            UpdatePreviewUrl();
        }

        private void UpdatePreviewUrl()
        {
            try
            {
                textBoxPreviewURL.Text = AssetTools.RW(_path, SelectStreamingEndpoint, SelectedFilters, ReturnHttps, ReturnSelectCustomHostName, ReturnStreamingProtocol, ReturnHLSAudioTrackName, ReturnHLSNoAudioOnlyMode).ToString();
            }
            catch
            {
                textBoxPreviewURL.Text = "Error";
            }
        }

        private void ListBoxSE_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePreviewUrl();
        }

        private void RadioButtonHttp_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePreviewUrl();
        }

        private void ListBoxFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePreviewUrl();
        }

        private void TextBoxHLSAudioTrackName_TextChanged(object sender, EventArgs e)
        {
            UpdatePreviewUrl();
        }

        private void CheckBoxNoAudioOnly_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePreviewUrl();
        }

        private void RadioButtonSmooth_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton checkb = (RadioButton)sender;

            if (checkb.Checked)  // to do it one time
            {
                UpdatePreviewUrl();
            }
        }



        private void ListViewFilters_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            UpdatePreviewUrl();
        }


        private void ContextMenuStripURL_MouseClick(object sender, MouseEventArgs e)
        {
            ContextMenuStrip contextmenu = (ContextMenuStrip)sender;
            TextBox textbox = (TextBox)contextmenu.SourceControl;

            if (!(string.IsNullOrEmpty(textbox.Text)))
            {
                System.Windows.Forms.Clipboard.SetText(textbox.Text);
            }
        }

        private void RadioButtonHLSCMAF_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton checkb = (RadioButton)sender;

            if (checkb.Checked)  // to do it one time
            {
                UpdatePreviewUrl();
            }
        }

        private async void ComboBoxPolicyLocators_SelectedIndexChanged(object sender, EventArgs e)
        {
            var locator = _locators[comboBoxPolicyLocators.SelectedIndex];

            // _path = "/" + locator.StreamingLocatorId.ToString() + _path.Substring(_path.IndexOf('/', 2));

            if (!_emptyliveOutput)
            {
                var loc = (await _amsClient.AMSclient.GetStreamingLocatorAsync(locator.Name)).Value;
                _path = loc.GetStreamingPaths().Value.StreamingPaths.Where(p => p.StreamingProtocol == StreamingPolicyStreamingProtocol.SmoothStreaming)
            .FirstOrDefault().Paths.FirstOrDefault();
            }
            else // we should not use ListPaths as liveOutput is empty
            {
                UriBuilder uribuilder = new()
                {
                    Path = locator.StreamingLocatorId.ToString() + _path.Substring(_path.IndexOf("/", 1))
                };
                _path = uribuilder.Uri.PathAndQuery;
            }

            UpdatePreviewUrl();
        }

        private void ChooseStreamingEndpoint_DpiChanged(object sender, DpiChangedEventArgs e)
        {
            // DpiUtils.UpdatedSizeFontAfterDPIChange(label5, e);
        }

        private void ChooseStreamingEndpoint_Shown(object sender, EventArgs e)
        {
            Telemetry.TrackPageView(this.Name);
        }
    }
}