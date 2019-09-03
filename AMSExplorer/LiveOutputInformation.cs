//----------------------------------------------------------------------------------------------
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
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AMSExplorer
{
    public partial class LiveOutputInformation : Form
    {
        public IEnumerable<StreamingEndpoint> MyStreamingEndpoints;
        private readonly Mainform MyMainForm;
        private readonly AMSClientV3 _client;
        public bool MultipleSelection = false;
        public LiveOutput MyLiveOutput;

        public LiveOutputInformation(Mainform mainform, AMSClientV3 client)
        {
            InitializeComponent();
            Icon = Bitmaps.Azure_Explorer_ico;
            MyMainForm = mainform;
            _client = client;
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
            _client.RefreshTokenIfNeeded();
            Asset AssetToDisplayP = _client.AMSclient.Assets.Get(_client.credentialsEntry.ResourceGroup, _client.credentialsEntry.AccountName, MyLiveOutput.AssetName);
            if (AssetToDisplayP != null)
            {
                AssetInformation form = new AssetInformation(MyMainForm, _client)
                {
                    myAssetV3 = AssetToDisplayP,
                    myStreamingEndpoints = MyStreamingEndpoints // we want to keep the same sorting
                };
                DialogResult dialogResult = form.ShowDialog(this);
            }
            else
            {
                MessageBox.Show(string.Format("Asset '{0}' not found !", MyLiveOutput.AssetName), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LiveOutputInformation_Load(object sender, EventArgs e)
        {
            DpiUtils.InitPerMonitorDpi(this);

            if (!MultipleSelection)
            {
                labelProgramName.Text += MyLiveOutput.Name;
                DGLiveEvent.ColumnCount = 2;

                // Program info
                DGLiveEvent.Columns[0].DefaultCellStyle.BackColor = Color.Gainsboro;
                DGLiveEvent.Rows.Add("Name", MyLiveOutput.Name);
                DGLiveEvent.Rows.Add("Id", MyLiveOutput.Id);
                DGLiveEvent.Rows.Add("State", MyLiveOutput.ResourceState);
                DGLiveEvent.Rows.Add("Created", ((DateTime)MyLiveOutput.Created).ToLocalTime().ToString("G"));
                DGLiveEvent.Rows.Add("Last Modified", ((DateTime)MyLiveOutput.LastModified).ToLocalTime().ToString("G"));
                DGLiveEvent.Rows.Add("Description", MyLiveOutput.Description);
                DGLiveEvent.Rows.Add("Archive Window Length", MyLiveOutput.ArchiveWindowLength);
                DGLiveEvent.Rows.Add("Manifest Name", MyLiveOutput.ManifestName);
                DGLiveEvent.Rows.Add("Asset Name", MyLiveOutput.AssetName);
                DGLiveEvent.Rows.Add("Output snap time", MyLiveOutput.OutputSnapTime);
                DGLiveEvent.Rows.Add("Fragments Per Ts Segment", MyLiveOutput.Hls?.FragmentsPerTsSegment);

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
            }
            else
            {
                labelProgramName.Text = "(multiple programs have been selected)";
                tabControl1.TabPages.Remove(tabPageInfo); // no info as multiple
                buttonDisplayRelatedAsset.Visible = false;
            }


            numericUpDownArchiveHours.Value = Convert.ToInt16(MyLiveOutput.ArchiveWindowLength.TotalHours);
            numericUpDownArchiveMinutes.Value = MyLiveOutput.ArchiveWindowLength.Minutes;

        }

        private void labelProgramName_Click(object sender, EventArgs e)
        {

        }


        private void ProgramInformation_Shown(object sender, EventArgs e)
        {

        }

        private void LiveOutputInformation_DpiChanged(object sender, DpiChangedEventArgs e)
        {
            // for controls which are not using the default font
            DpiUtils.UpdatedSizeFontAfterDPIChange(new List<Control> { labelProgramName, contextMenuStripDG }, e, this);
        }
    }
}
