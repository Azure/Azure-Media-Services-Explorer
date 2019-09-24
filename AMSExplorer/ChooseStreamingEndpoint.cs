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

using Microsoft.Azure.Management.Media;
using Microsoft.Azure.Management.Media.Models;
using Microsoft.Rest.Azure;
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
        private readonly Asset _asset;
        private readonly string _filter;
        private readonly PlayerType _playertype;
        private string _path;
        private readonly bool _displayBrowserSelection;
        private readonly AMSClientV3 _amsClient;
        private IList<AssetStreamingLocator> _locators;

        public StreamingEndpoint SelectStreamingEndpoint
        {
            get
            {
                string val = (listBoxSE.SelectedItem as Item).Value as string;
                string seName = val.Split("|".ToCharArray())[0];
                _amsClient.RefreshTokenIfNeeded();
                return Task.Run(() => _amsClient.AMSclient.StreamingEndpoints.GetAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, seName)).GetAwaiter().GetResult();
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
                string val = (listBoxSE.SelectedItem as Item).Value as string;
                return val.Split("|".ToCharArray())[1];
            }
        }

        public string ReturnSelectedBrowser => (comboBoxBrowser.SelectedItem as Item).Value as string;

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



        public ChooseStreamingEndpoint(AMSClientV3 client, Asset asset, string path, string filter = null, PlayerType playertype = PlayerType.AzureMediaPlayer, bool displayBrowserSelection = false)
        {
            InitializeComponent();
            Icon = Bitmaps.Azure_Explorer_ico;
            _amsClient = client;
            _asset = asset;
            _filter = filter;
            _playertype = playertype;
            _path = path;
            _displayBrowserSelection = displayBrowserSelection;
        }


        private async void ChooseStreamingEndpoint_Load(object sender, EventArgs e)
        {
            DpiUtils.InitPerMonitorDpi(this);

            label.Text = string.Format(label.Text, _asset.Name);

            // SE List
            await _amsClient.RefreshTokenIfNeededAsync();

            // StreamingEndpoint BestSE = Task.Run(async () => await AssetInfo.GetBestStreamingEndpointAsync(_client)).Result;
            StreamingEndpoint BestSE = await AssetInfo.GetBestStreamingEndpointAsync(_amsClient);

           var myStreamingEndpoints = Task.Run(() => _amsClient.AMSclient.StreamingEndpoints.ListAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName)).GetAwaiter().GetResult();

            foreach (StreamingEndpoint se in myStreamingEndpoints)
            {
                listBoxSE.Items.Add(new Item(string.Format(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_012ScaleUnit, se.Name, se.ResourceState, StreamingEndpointInformation.ReturnTypeSE(se)), se.Name + "|" + se.HostName));
                if (se.Id == BestSE.Id)
                {
                    listBoxSE.SelectedIndex = listBoxSE.Items.Count - 1;
                }

                foreach (string custom in se.CustomHostNames)
                {
                    listBoxSE.Items.Add(new Item(string.Format(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_012ScaleUnitCustomHostname3, se.Name, se.ResourceState, StreamingEndpointInformation.ReturnTypeSE(se), custom), se.Name + "|" + custom));
                }
            }


            // Filters

            // asset filters
            Microsoft.Rest.Azure.IPage<AssetFilter> assetFilters = await _amsClient.AMSclient.AssetFilters.ListAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, _asset.Name);
            List<string> afiltersnames = assetFilters.Select(a => a.Name).ToList();

            listViewFilters.BeginUpdate();
            assetFilters.ToList().ForEach(f =>
            {
                ListViewItem lvitem = new ListViewItem(new string[] { AMSExplorer.Properties.Resources.ChooseStreamingEndpoint_ChooseStreamingEndpoint_Load_AssetFilter + f.Name, f.Name });
                if (_filter != null && f.Name == _filter)
                {
                    lvitem.Checked = true;
                }
                listViewFilters.Items.Add(lvitem);
            }
           );

            // account filters
            IPage<AccountFilter> acctFilters = await _amsClient.AMSclient.AccountFilters.ListAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName);

            acctFilters.ToList().ForEach(f =>
            {
                ListViewItem lvitem = new ListViewItem(new string[] { AMSExplorer.Properties.Resources.ChooseStreamingEndpoint_ChooseStreamingEndpoint_Load_GlobalFilter + f.Name, f.Name });
                if (_filter != null && f.Name == _filter && listViewFilters.CheckedItems.Count == 0) // only if not already selected (asset filter priority > account filter)
                {
                    lvitem.Checked = true;
                }
                if (afiltersnames.Contains(f.Name)) // global filter with same name than asset filter
                {
                    lvitem.ForeColor = Color.Gray;
                }
                listViewFilters.Items.Add(lvitem);
            }
           );
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

                comboBoxBrowser.Items.Add(new Item(Constants.BrowserIE[0], Constants.BrowserIE[1]));
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

            _amsClient.RefreshTokenIfNeeded();
            _locators =
            Task.Run(() =>
                        _amsClient.AMSclient.Assets.ListStreamingLocatorsAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, _asset.Name)
                        ).GetAwaiter().GetResult().StreamingLocators;

            foreach (AssetStreamingLocator locator in _locators.ToList())
            {
                int index = comboBoxPolicyLocators.Items.Add(new Item(locator.Name, locator.Name));
                if (_path.Contains(locator.StreamingLocatorId.ToString()))
                {
                    comboBoxPolicyLocators.SelectedIndex = index;
                }
            }
            comboBoxPolicyLocators.EndUpdate();
        }

        private static bool IsWindows10()
        {
            RegistryKey reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion");

            string productName = (string)reg.GetValue("ProductName");

            return productName.StartsWith("Windows 10");
        }

        private void radioButtonHLSv3_CheckedChanged(object sender, EventArgs e)
        {
            textBoxHLSAudioTrackName.Enabled = checkBoxNoAudioOnly.Enabled = labelaudiotrackname.Enabled = radioButtonHLSv3.Checked;
            UpdatePreviewUrl();
        }

        private void UpdatePreviewUrl()
        {
            try
            {
                textBoxPreviewURL.Text = AssetInfo.RW(_path, SelectStreamingEndpoint, SelectedFilters, ReturnHttps, ReturnSelectCustomHostName, ReturnStreamingProtocol, ReturnHLSAudioTrackName, ReturnHLSNoAudioOnlyMode).ToString();
            }
            catch
            {
                textBoxPreviewURL.Text = "Error";
            }
        }

        private void listBoxSE_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePreviewUrl();
        }

        private void radioButtonHttp_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePreviewUrl();
        }

        private void listBoxFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePreviewUrl();
        }

        private void textBoxHLSAudioTrackName_TextChanged(object sender, EventArgs e)
        {
            UpdatePreviewUrl();
        }

        private void checkBoxNoAudioOnly_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePreviewUrl();
        }

        private void radioButtonSmooth_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton checkb = (RadioButton)sender;

            if (checkb.Checked)  // to do it one time
            {
                UpdatePreviewUrl();
            }
        }



        private void listViewFilters_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            UpdatePreviewUrl();
        }

        private void toolStripMenuItemFilesCopyClipboard_Click(object sender, EventArgs e)
        {

        }

        private void contextMenuStripURL_MouseClick(object sender, MouseEventArgs e)
        {
            ContextMenuStrip contextmenu = (ContextMenuStrip)sender;
            TextBox textbox = (TextBox)contextmenu.SourceControl;

            if (!(string.IsNullOrEmpty(textbox.Text)))
            {
                System.Windows.Forms.Clipboard.SetText(textbox.Text);
            }
        }

        private void radioButtonHLSCMAF_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton checkb = (RadioButton)sender;

            if (checkb.Checked)  // to do it one time
            {
                UpdatePreviewUrl();
            }
        }

        private async void comboBoxPolicyLocators_SelectedIndexChanged(object sender, EventArgs e)
        {
            AssetStreamingLocator locator = _locators[comboBoxPolicyLocators.SelectedIndex];

            // _path = "/" + locator.StreamingLocatorId.ToString() + _path.Substring(_path.IndexOf('/', 2));

            _path = (await _amsClient.AMSclient.StreamingLocators.ListPathsAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, locator.Name))
            .StreamingPaths.Where(p => p.StreamingProtocol == StreamingPolicyStreamingProtocol.SmoothStreaming)
            .FirstOrDefault().Paths.FirstOrDefault();

            UpdatePreviewUrl();
        }

        private void ChooseStreamingEndpoint_DpiChanged(object sender, DpiChangedEventArgs e)
        {
            DpiUtils.UpdatedSizeFontAfterDPIChange(label5, e);
        }
    }
}