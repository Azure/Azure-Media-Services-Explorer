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


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Blob.Protocol;
using System.Web;
using Microsoft.Azure.Management.Media.Models;
using Microsoft.Azure.Management.Media;

namespace AMSExplorer
{
    public partial class LiveOutputInformation : Form
    {
        private IEnumerable<Uri> ValidURIs;
        private IEnumerable<Uri> NotValidURIs;
        public IEnumerable<StreamingEndpoint> MyStreamingEndpoints;
        private Mainform MyMainForm;
        private AMSClientV3 _client;
        public bool MultipleSelection = false;
        public LiveOutput MyLiveOutput;

        public LiveOutputInformation(Mainform mainform, AMSClientV3 client)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
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
            var AssetToDisplayP = _client.AMSclient.Assets.Get(_client.credentialsEntry.ResourceGroup, _client.credentialsEntry.AccountName, MyLiveOutput.AssetName);
            if (AssetToDisplayP != null)
            {
                AssetInformation form = new AssetInformation(MyMainForm, _client)
                {
                    myAssetV3 = AssetToDisplayP,
                    myStreamingEndpoints = MyStreamingEndpoints // we want to keep the same sorting
                };
                DialogResult dialogResult = form.ShowDialog(this);
            }
        }


        private void LiveOutputInformation_Load(object sender, EventArgs e)
        {
            if (!MultipleSelection)
            {
                labelProgramName.Text += MyLiveOutput.Name;
                DGChannel.ColumnCount = 2;

                // Program info
                DGChannel.Columns[0].DefaultCellStyle.BackColor = Color.Gainsboro;
                DGChannel.Rows.Add("Name", MyLiveOutput.Name);
                DGChannel.Rows.Add("Id", MyLiveOutput.Id);
                DGChannel.Rows.Add("State", MyLiveOutput.ResourceState);
                DGChannel.Rows.Add("Created", ((DateTime)MyLiveOutput.Created).ToLocalTime().ToString("G"));
                DGChannel.Rows.Add("Last Modified", ((DateTime)MyLiveOutput.LastModified).ToLocalTime().ToString("G"));
                DGChannel.Rows.Add("Description", MyLiveOutput.Description);
                DGChannel.Rows.Add("Archive Window Length", MyLiveOutput.ArchiveWindowLength);
                DGChannel.Rows.Add("Manifest Name", MyLiveOutput.ManifestName);
                DGChannel.Rows.Add("Asset Name", MyLiveOutput.AssetName);
                DGChannel.Rows.Add("Output snapshot time", MyLiveOutput.OutputSnapTime);

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
    }
}
