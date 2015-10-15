//----------------------------------------------------------------------------------------------
//    Copyright 2015 Microsoft Corporation
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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAzure.MediaServices.Client;

namespace AMSExplorer
{
    public partial class ChooseStreamingEndpoint : Form
    {
        CloudMediaContext _context;
        IAsset _asset;
        string _filter;
        PlayerType _playertype;
        private string _url;

        public IStreamingEndpoint SelectStreamingEndpoint
        {
            get
            {
                string val = (listBoxSE.SelectedItem as Item).Value as string;
                string seid = val.Split("|".ToCharArray())[0];
                return _context.StreamingEndpoints.Where(se => se.Id == seid).FirstOrDefault();
            }
        }

        public string SelectedFilters
        {
            get
            {
                //string filters = string.Empty;
                //bool first = true;
                //foreach (var f in listBoxFilter.SelectedItems)
                //{
                //    string v = (f as Item).Value as string;
                //    if (v != null)
                //    {
                //        filters += (first ? "" : ";") + v;
                //        first = false;
                //    }
                //}
                //if (string.IsNullOrEmpty(filters))
                //{
                //    filters = null;
                //}
                //return filters;

                string filters = string.Empty;
                bool first = true;
                foreach (var f in listViewFilters.CheckedItems)
                {
                    var ff = f as ListViewItem;
                    string v = (f as ListViewItem).SubItems[1].Text;
                    if (v != "")
                    {
                        filters += (first ? "" : ";") + v;
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

        public bool ReturnHttps
        {
            get
            {
                return radioButtonHttps.Checked;
            }
        }

        public string ReturnHLSAudioTrackName
        {
            get
            {
                if (radioButtonHLSv3.Checked && !string.IsNullOrWhiteSpace(textBoxHLSAudioTrackName.Text))
                {
                    return textBoxHLSAudioTrackName.Text;
                }
                else
                    return null;
            }
        }

        public bool ReturnHLSNoAudioOnlyMode
        {
            get
            {
                return checkBoxNoAudioOnly.Checked;
            }
        }

        public AMSOutputProtocols ReturnStreamingProtocol
        {
            get
            {
                if (radioButtonDASH.Checked)
                { return AMSOutputProtocols.Dash; }
                else if (radioButtonSmoothLegacy.Checked)
                { return AMSOutputProtocols.SmoothLegacy; }
                else if (radioButtonHDS.Checked)
                { return AMSOutputProtocols.HDS; }
                else if (radioButtonHLSv3.Checked)
                { return AMSOutputProtocols.HLSv3; }
                else if (radioButtonHLSv4.Checked)
                { return AMSOutputProtocols.HLSv4; }

                return AMSOutputProtocols.Smooth;
            }
        }


        public ChooseStreamingEndpoint(CloudMediaContext context, IAsset asset, string Url, string filter = null, PlayerType playertype = PlayerType.AzureMediaPlayer)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            _context = context;
            _asset = asset;
            _filter = filter;
            _playertype = playertype;
            _url = Url;
        }


        private void ChooseStreamingEndpoint_Load(object sender, EventArgs e)
        {
            label.Text = string.Format(label.Text, _asset.Name);

            // SE List
            IStreamingEndpoint BestSE = AssetInfo.GetBestStreamingEndpoint(_context);
            foreach (var se in _context.StreamingEndpoints)
            {
                listBoxSE.Items.Add(new Item(string.Format("{0} ({1}, {2} scale unit{3})", se.Name, se.State, se.ScaleUnits, se.ScaleUnits > 1 ? "s" : string.Empty), se.Id + "|" + se.HostName));
                if (se.Id == BestSE.Id) listBoxSE.SelectedIndex = listBoxSE.Items.Count - 1;
                foreach (var custom in se.CustomHostNames)
                {
                    listBoxSE.Items.Add(new Item(string.Format("{0} ({1}, {2} scale unit{3}) Custom hostname : {4}", se.Name, se.State, se.ScaleUnits, se.ScaleUnits > 1 ? "s" : string.Empty, custom), se.Id + "|" + custom));
                }
            }

            // Filters

            // asset filters
            var afilters = _asset.AssetFilters.ToList();
            var afiltersnames = afilters.Select(a => a.Name).ToList();
            afilters.ForEach(f =>
            {
                var lvitem = new ListViewItem(new string[] { "asset filter : " + f.Name, f.Name });
                if (_filter != null && f.Name == _filter)
                {
                    lvitem.Checked = true;
                }
                listViewFilters.Items.Add(lvitem);
            }
            );

            // global filters
            _context.Filters.ToList().ForEach(f =>
           {
               var lvitem = new ListViewItem(new string[] { "global filter : " + f.Name, f.Name });
               if (_filter != null && f.Name == _filter && listViewFilters.CheckedItems.Count == 0) // only if not already selected (asset filter priority > global filter)
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



            if (_playertype == PlayerType.DASHIFRefPlayer || _playertype == PlayerType.DASHLiveAzure)
            {
                radioButtonDASH.Checked = true;
            }

            UpdatePreviewUrl();
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
                textBoxPreviewURL.Text = AssetInfo.RW(new Uri(_url), SelectStreamingEndpoint, SelectedFilters, ReturnHttps, ReturnSelectCustomHostName, ReturnStreamingProtocol, ReturnHLSAudioTrackName, ReturnHLSNoAudioOnlyMode).ToString();
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
            var checkb = (RadioButton)sender;

            if (checkb.Checked)  // to do it one time
                UpdatePreviewUrl();
        }



        private void listViewFilters_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            UpdatePreviewUrl();
        }
    }
}
