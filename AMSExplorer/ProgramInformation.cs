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
using System.Diagnostics;
using Microsoft.WindowsAzure.MediaServices.Client;
using System.IO;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Blob.Protocol;
using System.Web;

namespace AMSExplorer
{
    public partial class ProgramInformation : Form
    {
        public IProgram MyProgram;
        private CloudMediaContext MyContext;
        private IEnumerable<Uri> ValidURIs;
        private IEnumerable<Uri> NotValidURIs;
        public IEnumerable<IStreamingEndpoint> MyStreamingEndpoints;
        private Mainform MyMainForm;
        public bool MultipleSelection = false;
        public ExplorerProgramModifications Modifications = new ExplorerProgramModifications();

        public string ProgramDescription
        {
            get { return textBoxDescription.Text; }
        }


        public TimeSpan archiveWindowLength
        {
            get
            {
                return new TimeSpan((int)numericUpDownArchiveHours.Value, (int)numericUpDownArchiveMinutes.Value, 0); ;
            }
        }


        public ProgramInformation(Mainform mainform, CloudMediaContext context)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            MyMainForm = mainform;
            MyContext = context;
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
            IAsset AssetToDisplayP = MyProgram.Asset;
            if (AssetToDisplayP != null)
            {
                AssetInformation form = new AssetInformation(MyMainForm, MyContext)
                {
                    myAsset = AssetToDisplayP,
                    myStreamingEndpoints = MyStreamingEndpoints // we want to keep the same sorting
                };
                DialogResult dialogResult = form.ShowDialog(this);
            }
        }




        private void ProgramInformation_Load(object sender, EventArgs e)
        {
            if (!MultipleSelection)
            {
                labelProgramName.Text += MyProgram.Name;
                DGChannel.ColumnCount = 2;

                // Program info
                DGChannel.Columns[0].DefaultCellStyle.BackColor = Color.Gainsboro;
                DGChannel.Rows.Add("Name", MyProgram.Name);
                DGChannel.Rows.Add("Id", MyProgram.Id);
                DGChannel.Rows.Add("State", (ChannelState)MyProgram.State);
                DGChannel.Rows.Add("Created", ((DateTime)MyProgram.Created).ToLocalTime().ToString("G"));
                DGChannel.Rows.Add("Last Modified", ((DateTime)MyProgram.LastModified).ToLocalTime().ToString("G"));
                DGChannel.Rows.Add("Description", MyProgram.Description);
                DGChannel.Rows.Add("Archive Window Length", MyProgram.ArchiveWindowLength);
                DGChannel.Rows.Add("Manifest Name", MyProgram.ManifestName);
                DGChannel.Rows.Add("Channel Name", MyProgram.Channel.Name);
                DGChannel.Rows.Add("Channel Id", MyProgram.ChannelId);
                DGChannel.Rows.Add("Asset Name", MyProgram.Asset.Name);
                DGChannel.Rows.Add("Asset Id", MyProgram.AssetId);

                ProgramInfo PI = new ProgramInfo(MyProgram, MyContext);
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
            }
            else
            {
                labelProgramName.Text = "(multiple programs have been selected)";
                tabControl1.TabPages.Remove(tabPageInfo); // no info as multiple
                buttonDisplayRelatedAsset.Visible = false;
            }

            textBoxDescription.Text = MyProgram.Description;

            numericUpDownArchiveHours.Value = Convert.ToInt16(MyProgram.ArchiveWindowLength.TotalHours);
            numericUpDownArchiveMinutes.Value = MyProgram.ArchiveWindowLength.Minutes;

            // let's track when user edit a setting
            Modifications = new ExplorerProgramModifications
            {
                Description = false,
                ArchiveWindow = false
            };
        }

        private void labelProgramName_Click(object sender, EventArgs e)
        {

        }


        private void ProgramInformation_Shown(object sender, EventArgs e)
        {

        }

        private void textBoxDescription_TextChanged(object sender, EventArgs e)
        {
            Modifications.Description = true;
        }

        private void numericUpDownArchiveHours_ValueChanged(object sender, EventArgs e)
        {
            Modifications.ArchiveWindow = true;
        }

        private void numericUpDownArchiveMinutes_ValueChanged(object sender, EventArgs e)
        {
            Modifications.ArchiveWindow = true;
        }
    }

    public class ExplorerProgramModifications
    {
        public bool Description { get; set; }
        public bool ArchiveWindow { get; set; }
    }
}
