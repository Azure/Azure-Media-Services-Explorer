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
using System.ComponentModel;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AMSExplorer
{
    public partial class LiveOutputInformation : Form
    {
        private IEnumerable<StreamingEndpoint> _streamingEndpoints;
        private readonly Mainform MyMainForm;
        private readonly AMSClientV3 _amsClient;
        private bool _multipleSelection = false;
        private LiveOutput _liveOutput;
        private Asset _relatedAsset = null;

        public LiveOutputInformation(Mainform mainform, AMSClientV3 client, LiveOutput liveOutput, IEnumerable<StreamingEndpoint> streamingEndpoints, bool multipleSelection = false)
        {
            InitializeComponent();
            Icon = Bitmaps.Azure_Explorer_ico;
            MyMainForm = mainform;
            _amsClient = client;
            _liveOutput = liveOutput;
            _multipleSelection = multipleSelection;
            _streamingEndpoints = streamingEndpoints;
        }

        private void contextMenuStripDG_MouseClick(object sender, MouseEventArgs e)
        {
            ContextMenuStrip contextmenu = (ContextMenuStrip)sender;
            DataGridView DG = (DataGridView)contextmenu.SourceControl;

            if (DG.SelectedCells.Count == 1)
            {
                if (DG.SelectedCells[0].Value != null)
                {
                    System.Windows.Forms.Clipboard.SetText(DG.SelectedCells[0].Value.ToString());

                }
                else
                {
                    System.Windows.Forms.Clipboard.Clear();
                }
            }
        }


        private void contextMenuStripDG_Opening(object sender, CancelEventArgs e)
        {

        }


        private void buttonOpenAsset_Click(object sender, EventArgs e)
        {
            if (_relatedAsset != null)
            {
                AssetInformation form = new(
                    MyMainForm,
                    _amsClient,
                    _relatedAsset,
                    _streamingEndpoints // we want to keep the same sorting
                    );

                DialogResult dialogResult = form.ShowDialog(this);
            }
            else
            {
                MessageBox.Show(string.Format("Asset '{0}' not found !", _liveOutput.AssetName), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void LiveOutputInformation_Load(object sender, EventArgs e)
        {
            // DpiUtils.InitPerMonitorDpi(this);

            if (!_multipleSelection)
            {
                labelProgramName.Text += _liveOutput.Name;
                DGLiveEvent.ColumnCount = 2;

                // Program info
                DGLiveEvent.Columns[0].DefaultCellStyle.BackColor = Color.Gainsboro;
                DGLiveEvent.Rows.Add("Name", _liveOutput.Name);
                DGLiveEvent.Rows.Add("Id", _liveOutput.Id);
                DGLiveEvent.Rows.Add("State", _liveOutput.ResourceState);
                DGLiveEvent.Rows.Add("Created", ((DateTime)_liveOutput.Created).ToLocalTime().ToString("G"));
                DGLiveEvent.Rows.Add("Last Modified", ((DateTime)_liveOutput.LastModified).ToLocalTime().ToString("G"));
                DGLiveEvent.Rows.Add("Description", _liveOutput.Description);
                DGLiveEvent.Rows.Add("Archive Window Length", _liveOutput.ArchiveWindowLength);
                DGLiveEvent.Rows.Add("Manifest Name", _liveOutput.ManifestName);
                DGLiveEvent.Rows.Add("Asset Name", _liveOutput.AssetName);
                DGLiveEvent.Rows.Add("Output snap time", _liveOutput.OutputSnapTime);
                DGLiveEvent.Rows.Add("Fragments Per Ts Segment", _liveOutput.Hls?.FragmentsPerTsSegment);

                /*
                ProgramInfo PI = new ProgramInfo(MyLiveOutput, MyContext);
                ValidURIs = PI.GetValidURIs();
                NotValidURIs = PI.GetNotValidURIs();

                foreach (var t in ValidURIs)
                {
                    DGChannel.Rows.Add("Url", t.AbsoluteUri);
                }
                foreach (var t in NotValidURIs)
                {
                    int i = DGChannel.Rows.Add("Url", t.AbsoluteUri);
                    DGChannel.Rows[i].Cells[1].Style.ForeColor = Color.Red;
                }
                */

                _relatedAsset = await _amsClient.GetAssetAsync(_liveOutput.AssetName);
                if (_relatedAsset != null)
                {
                    var result = await AssetTools.GetValidOnDemandSmoothURIAsync(_relatedAsset, _amsClient, null, _liveOutput);
                    if (result.Item1 != null)
                    {
                        string text = "Output Url";
                        if (result.Item2)
                        {
                            text += " (not active yet)";
                        }
                        DGLiveEvent.Rows.Add(text, result.Item1);
                    }
                }
            }
            else
            {
                labelProgramName.Text = "(multiple programs have been selected)";
                tabControl1.TabPages.Remove(tabPageInfo); // no info as multiple
                buttonDisplayRelatedAsset.Visible = false;
            }


            numericUpDownArchiveHours.Value = Convert.ToInt16(_liveOutput.ArchiveWindowLength.TotalHours);
            numericUpDownArchiveMinutes.Value = _liveOutput.ArchiveWindowLength.Minutes;

        }

        private void labelProgramName_Click(object sender, EventArgs e)
        {

        }


        private void ProgramInformation_Shown(object sender, EventArgs e)
        {
            Telemetry.TrackPageView(this.Name);
        }

        private void LiveOutputInformation_DpiChanged(object sender, DpiChangedEventArgs e)
        {
            // for controls which are not using the default font
            // DpiUtils.UpdatedSizeFontAfterDPIChange(new List<Control> { labelProgramName, contextMenuStripDG }, e, this);
        }
    }
}
