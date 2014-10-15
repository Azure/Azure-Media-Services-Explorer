//----------------------------------------------------------------------- 
// <copyright file="EncodingAMEAdv.cs" company="Microsoft">Copyright (c) Microsoft Corporation. All rights reserved.</copyright> 
// <license>
// Azure Media Services Explorer Ver. 3.0
// Licensed under the Apache License, Version 2.0 (the "License"); 
// you may not use this file except in compliance with the License. 
// You may obtain a copy of the License at 
//  
// http://www.apache.org/licenses/LICENSE-2.0 
//  
// Unless required by applicable law or agreed to in writing, software 
// distributed under the License is distributed on an "AS IS" BASIS, 
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
// See the License for the specific language governing permissions and 
// limitations under the License. 
// </license> 

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using Microsoft.WindowsAzure.MediaServices.Client;

namespace AMSExplorer
{
    public partial class EncodingAMEAdv : Form
    {
        public XDocument doc;
        public string EncodingAMEPresetXMLFiles;
        private bool xmlOpenedNotYetStiched = true; // true if xml has been opened and no stiching done yet
        private bool xmlOpenedNotYetNamedConvention = true; // true if xml has been opened and no naming convention done yet
        private bool xmlOpenedNotYetVSSRotation = true; // true if xml has been opened and no naming convention done yet

        private List<IMediaProcessor> Procs;
        public List<IAsset> SelectedAssets;
        private bool bMultiAssetMode = true;

        public string EncodingLabel
        {
            set
            {
                label.Text = value;
            }
        }

        public string EncodingJobName
        {
            get
            {
                return textBoxJobName.Text;
            }
            set
            {
                textBoxJobName.Text = value;
            }
        }


        public List<IMediaProcessor> EncodingProcessorsList
        {
            set
            {
                foreach (IMediaProcessor pr in value)
                    comboBoxProcessor.Items.Add(string.Format("{0} {1} Version {2} ({3})", pr.Vendor, pr.Name, pr.Version, pr.Description));
                comboBoxProcessor.SelectedIndex = 0;
                Procs = value;
            }
        }

        public IMediaProcessor EncodingProcessorSelected
        {
            get
            {

                return Procs[comboBoxProcessor.SelectedIndex];
            }
        }

        public string EncodingOutputAssetName
        {
            get
            {
                return textboxoutputassetname.Text;
            }
            set
            {
                textboxoutputassetname.Text = value;
            }
        }


        public string EncodingConfiguration
        {
            get
            {
                return textBoxConfiguration.Text;
            }
        }

        public int EncodingPriority
        {
            get
            {
                return (int)numericUpDownPriority.Value;
            }
            set
            {
                numericUpDownPriority.Value = value;
            }
        }


        public EncodingAMEAdv()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (Directory.Exists(this.EncodingAMEPresetXMLFiles))
                openFileDialogPreset.InitialDirectory = this.EncodingAMEPresetXMLFiles;
            if (openFileDialogPreset.ShowDialog() == DialogResult.OK)
            {
                this.EncodingAMEPresetXMLFiles = Path.GetDirectoryName(openFileDialogPreset.FileName); // let's save the folder
                bool Error = false;
                try
                {
                    doc = XDocument.Load(openFileDialogPreset.FileName);
                    textBoxConfiguration.Text = doc.ToString();
                    checkBoxNamingConvention.Enabled = true;
                    checkBoxVSS.Enabled = true;
                    tableLayoutPanelIAssets.Enabled = true;
                    if (bMultiAssetMode) // multi asset
                    {
                        DisableControls();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                    Error = true;
                }
                if (!Error)
                {   // useful if the stitching and times have been already done for another loaded preset before
                    xmlOpenedNotYetStiched = true;
                    xmlOpenedNotYetNamedConvention = true;
                    xmlOpenedNotYetVSSRotation = true;
                    UpdateStitchAndOverlaysInDoc();
                    AddNamingConventionToDoc();
                    AddVSSRotationToDoc();
                }
            }
        }

        // Some controls must be disabled
        private void DisableControls()
        {
            tableLayoutPanelIAssets.GetControlFromPosition(0, 0).Enabled = false; // not possible to go up for first row
            if (bMultiAssetMode)
            {
                tableLayoutPanelIAssets.GetControlFromPosition(4, 0).Enabled = false; // not possible to do overlay with first asset
                tableLayoutPanelIAssets.GetControlFromPosition(5, 0).Enabled = false; // not possible to do overlay with first asset
            }
            
            if (tableLayoutPanelIAssets.RowCount > 1)
            {
                for (int i = 1; i < tableLayoutPanelIAssets.RowCount; i++)
                {
                    tableLayoutPanelIAssets.GetControlFromPosition(0, i).Enabled = true; // button up
                    tableLayoutPanelIAssets.GetControlFromPosition(1, i).Enabled = true; // button down
                    tableLayoutPanelIAssets.GetControlFromPosition(4, i).Enabled = true; // checkbox overlay
                    tableLayoutPanelIAssets.GetControlFromPosition(5, i).Enabled = true; // checkbox overlay
                }
            }
            tableLayoutPanelIAssets.GetControlFromPosition(1, tableLayoutPanelIAssets.RowCount - 1).Enabled = false; // not possible to go down for last row
        }

        public void checkBoxStitch_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender; // get the checkbox object
            int tag = (int)(cb.Tag);

            xmlOpenedNotYetStiched = false;
            UpdateStitchAndOverlaysInDoc();

            foreach (Control c in this.tableLayoutPanelIAssets.Controls)
            {
                if ((c.Text == "Edit times") && ((int)c.Tag == tag))
                {
                    c.Enabled = cb.Checked; // enable or disable time checkbox
                    if (!cb.Checked) ((CheckBox)c).Checked = false; // enable or disable time checkbox
                    this.tableLayoutPanelIAssets.Controls[this.tableLayoutPanelIAssets.Controls.IndexOf(c) + 1].Enabled = cb.Checked; // start time code control
                    this.tableLayoutPanelIAssets.Controls[this.tableLayoutPanelIAssets.Controls.IndexOf(c) + 2].Enabled = cb.Checked; // start time code control
                }
            }
        }

        public void checkBoxTime_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender; // get the checkbox object

            int tag = (int)(cb.Tag);

            foreach (Control c in this.tableLayoutPanelIAssets.Controls)
            {
                if ((c.GetType() == typeof(TextBox)) && ((int)c.Tag == tag))
                {
                    c.Enabled = cb.Checked;
                }
            }
            UpdateStitchAndOverlaysInDoc();
        }



        private void UpdateStitchAndOverlaysInDoc()
        {

            bool Error = false;

            // Let's see if one stich button is enabled
            bool stich_on = false;
            bool voverlay_on = false;
            bool aoverlay_on = false;


            foreach (Control c in this.tableLayoutPanelIAssets.Controls)
            {
                if (c.Text == "Stitch")
                {
                    if (((CheckBox)c).Checked)
                    {
                        stich_on = true;
                        break;
                    }
                }
            }

            foreach (Control c in this.tableLayoutPanelIAssets.Controls)
            {
                if (c.Text == "Visual overlay")
                {
                    if (((CheckBox)c).Checked)
                    {
                        voverlay_on = true;
                        break;
                    }
                }
            }

            foreach (Control c in this.tableLayoutPanelIAssets.Controls)
            {
                if (c.Text == "Audio overlay")
                {
                    if (((CheckBox)c).Checked)
                    {
                        aoverlay_on = true;
                        break;
                    }
                }
            }




            if (stich_on | voverlay_on | aoverlay_on | !xmlOpenedNotYetStiched) // on stich checkbox is checked, or checkbox as been selected in the past for this file, so let's modify the xml doc
            {
                XDocument docbackup = doc;

                try
                {
                    var test = doc.Element("Presets");

                    if (test == null) // It's an "old" preset file without the Presets attribute
                    {
                        var presets = doc.Elements("Preset");
                        if (presets.Count() == 0) throw new Exception();

                        foreach (var preset in presets)
                        {
                            // WE DELETE ALL IN ORDER TO REBUILD
                            if ((preset.Descendants("Sources").Count()) > 0)
                            {
                                preset.Descendants("Sources").Remove();
                            }

                            if ((preset.Descendants("Sources").Count()) == 0)
                            {
                                preset
                                .Element("MediaFile")
                                .AddFirst(new XElement("Sources"));
                            }


                            for (int row = 0; row < tableLayoutPanelIAssets.RowCount; row++)
                            {
                                for (int col = 0; col < tableLayoutPanelIAssets.ColumnCount; col++)
                                {
                                    Control c = tableLayoutPanelIAssets.GetControlFromPosition(col, row);
                                    if (c.Text == "Stitch")
                                    {
                                        if (((CheckBox)c).Checked)
                                        {
                                            string f = string.Format("%{0}%", (int)c.Tag);

                                            // We should not add MediaFile attribute for asset #0
                                            if ((int)c.Tag == 0)
                                            {
                                                preset.Descendants("Sources").FirstOrDefault().Add(new XElement("Source", new XAttribute("AudioStreamIndex", 0)));

                                            }
                                            else
                                            {
                                                preset.Descendants("Sources").FirstOrDefault().Add(new XElement("Source", new XAttribute("AudioStreamIndex", 0), new XAttribute("MediaFile", f)));
                                            }

                                            CheckBox edittimes = (CheckBox)c.Parent.GetNextControl(c, true);
                                            if (edittimes.Checked)
                                            {
                                                preset.Descendants("Source").LastOrDefault().AddFirst(new XElement("Clips"));
                                                TextBox startime = (TextBox)c.Parent.GetNextControl(edittimes, true);
                                                TextBox endtime = (TextBox)c.Parent.GetNextControl(startime, true);
                                                preset.Descendants("Clips").LastOrDefault().Add(new XElement("Clip", new XAttribute("StartTime", startime.Text), new XAttribute("EndTime", endtime.Text)));
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else // new preset file with the Presets attribute
                    {

                        var presets = doc.Element("Presets").Elements("Preset");
                        if (presets.Count() == 0) throw new Exception();

                        foreach (var preset in presets)
                        {
                            // WE DELETE ALL IN ORDER TO REBUILD
                            if ((preset.Descendants("Sources").Count()) > 0)
                            {
                                preset.Descendants("Sources").Remove();
                            }

                            if ((preset.Descendants("Sources").Count()) == 0)
                            {
                                preset
                                .Element("MediaFile")
                                .AddFirst(new XElement("Sources"));
                            }
                            var mediafile = preset.Descendants("MediaFile");
                            if (mediafile.Attributes("OverlayFileName").Count() > 0) mediafile.Attributes("OverlayFileName").Remove();
                            if (mediafile.Attributes("OverlayRect").Count() > 0) mediafile.Attributes("OverlayRect").Remove();
                            if (mediafile.Attributes("OverlayOpacity").Count() > 0) mediafile.Attributes("OverlayOpacity").Remove();
                            if (mediafile.Attributes("OverlayFadeInDuration").Count() > 0) mediafile.Attributes("OverlayFadeInDuration").Remove();
                            if (mediafile.Attributes("OverlayFadeOutDuration").Count() > 0) mediafile.Attributes("OverlayFadeOutDuration").Remove();
                            if (mediafile.Attributes("OverlayLayoutMode").Count() > 0) mediafile.Attributes("OverlayLayoutMode").Remove();
                            if (mediafile.Attributes("OverlayStartTime").Count() > 0) mediafile.Attributes("OverlayStartTime").Remove();
                            if (mediafile.Attributes("OverlayEndTime").Count() > 0) mediafile.Attributes("OverlayEndTime").Remove();

                            if (mediafile.Attributes("AudioOverlayFileName").Count() > 0) mediafile.Attributes("AudioOverlayFileName").Remove();
                            if (mediafile.Attributes("AudioOverlayLoop").Count() > 0) mediafile.Attributes("AudioOverlayLoop").Remove();
                            if (mediafile.Attributes("AudioOverlayLoopingGap").Count() > 0) mediafile.Attributes("AudioOverlayLoopingGap").Remove();
                            if (mediafile.Attributes("AudioOverlayLayoutMode").Count() > 0) mediafile.Attributes("AudioOverlayLayoutMode").Remove();
                            if (mediafile.Attributes("AudioOverlayGainLevel").Count() > 0) mediafile.Attributes("AudioOverlayGainLevel").Remove();
                            if (mediafile.Attributes("AudioOverlayFadeInDuration").Count() > 0) mediafile.Attributes("AudioOverlayFadeInDuration").Remove();
                            if (mediafile.Attributes("AudioOverlayFadeOutDuration").Count() > 0) mediafile.Attributes("AudioOverlayFadeOutDuration").Remove();

                            //foreach (Control c in this.tableLayoutPanelIAssets.Controls)
                            for (int row = 0; row < tableLayoutPanelIAssets.RowCount; row++)
                            {
                                for (int col = 0; col < tableLayoutPanelIAssets.ColumnCount; col++)
                                {
                                    Control c = tableLayoutPanelIAssets.GetControlFromPosition(col, row);
                                    if (c.Text == "Stitch")
                                    {
                                        if (((CheckBox)c).Checked)
                                        {

                                            string f = string.Empty;
                                            if (bMultiAssetMode)
                                            {
                                                f = string.Format("%{0}%", (int)c.Tag);
                                                if ((int)c.Tag == 0) // We should not add MediaFile attribute for asset #0
                                                {
                                                    preset.Descendants("Sources").FirstOrDefault().Add(new XElement("Source", new XAttribute("AudioStreamIndex", 0)));
                                                }
                                                else
                                                {
                                                    preset.Descendants("Sources").FirstOrDefault().Add(new XElement("Source", new XAttribute("AudioStreamIndex", 0), new XAttribute("MediaFile", f)));
                                                }

                                            }
                                            else // mono asset mode
                                            {
                                                preset.Descendants("Sources").FirstOrDefault().Add(new XElement("Source", new XAttribute("AudioStreamIndex", 0), new XAttribute("MediaFile", SelectedAssets.FirstOrDefault().AssetFiles.Skip((int)c.Tag).Take(1).FirstOrDefault().Name)));
                                            }


                                            CheckBox edittimes = (CheckBox)c.Parent.GetNextControl(c, true);
                                            if (edittimes.Checked)
                                            {
                                                preset.Descendants("Source").LastOrDefault().AddFirst(new XElement("Clips"));
                                                TextBox startime = (TextBox)c.Parent.GetNextControl(edittimes, true);
                                                TextBox endtime = (TextBox)c.Parent.GetNextControl(startime, true);
                                                preset.Descendants("Clips").LastOrDefault().Add(new XElement("Clip", new XAttribute("StartTime", startime.Text), new XAttribute("EndTime", endtime.Text)));
                                            }
                                        }
                                    }
                                    if (c.Text == "Visual overlay")
                                    {
                                        if (((CheckBox)c).Checked)
                                        {
                                            string f = string.Empty;
                                            if (bMultiAssetMode)
                                            {
                                                f = string.Format("%{0}%", (int)c.Tag);
                                                if ((int)c.Tag == 0)
                                                {
                                                    mediafile.FirstOrDefault().Add(new XAttribute("OverlayFileName", 0));
                                                }
                                                else
                                                {
                                                    mediafile.FirstOrDefault().Add(new XAttribute("OverlayFileName", f));
                                                }

                                            }
                                            else // mono asset mode
                                            {
                                                mediafile.FirstOrDefault().Add(new XAttribute("OverlayFileName", SelectedAssets.FirstOrDefault().AssetFiles.Skip((int)c.Tag).Take(1).FirstOrDefault().Name));
                                            }

                                            mediafile.FirstOrDefault().Add(new XAttribute("OverlayRect", string.Format("{0}, {1}, {2}, {3}", numericUpDownVOverlayRectX.Value, numericUpDownVOverlayRectY.Value, numericUpDownVOverlayRectW.Value, numericUpDownVOverlayRectH.Value)));
                                            mediafile.FirstOrDefault().Add(new XAttribute("OverlayOpacity", numericUpDownVOverlayOpacity.Value));
                                            mediafile.FirstOrDefault().Add(new XAttribute("OverlayFadeInDuration", textBoxVOverlayFadeIn.Text));
                                            mediafile.FirstOrDefault().Add(new XAttribute("OverlayFadeOutDuration", textBoxVOverlayFadeOut.Text));
                                            mediafile.FirstOrDefault().Add(new XAttribute("OverlayLayoutMode", comboBoxVOverlayMode.SelectedItem));
                                            if ((string)comboBoxVOverlayMode.SelectedItem == "Custom")
                                            {
                                                mediafile.FirstOrDefault().Add(new XAttribute("OverlayStartTime", textBoxVOverlayStartTime.Text));
                                                mediafile.FirstOrDefault().Add(new XAttribute("OverlayEndTime", textBoxVOverlayEndTime.Text));
                                            }
                                        }

                                    }
                                    if (c.Text == "Audio overlay")
                                    {
                                        if (((CheckBox)c).Checked)
                                        {
                                            string f = string.Empty;
                                            if (bMultiAssetMode)
                                            {
                                                f = string.Format("%{0}%", (int)c.Tag);
                                                if ((int)c.Tag == 0)
                                                {
                                                    mediafile.FirstOrDefault().Add(new XAttribute("AudioOverlayFileName", 0));
                                                }
                                                else
                                                {
                                                    mediafile.FirstOrDefault().Add(new XAttribute("AudioOverlayFileName", f));
                                                }

                                            }
                                            else // mono asset mode
                                            {
                                                mediafile.FirstOrDefault().Add(new XAttribute("AudioOverlayFileName", SelectedAssets.FirstOrDefault().AssetFiles.Skip((int)c.Tag).Take(1).FirstOrDefault().Name));
                                            }


                                            mediafile.FirstOrDefault().Add(new XAttribute("AudioOverlayLoop", CheckBoxAOverlayLoop.Checked));
                                            if (CheckBoxAOverlayLoop.Checked) mediafile.FirstOrDefault().Add(new XAttribute("AudioOverlayLoopingGap", textBoxAOverlayGap.Text));
                                            mediafile.FirstOrDefault().Add(new XAttribute("AudioOverlayGainLevel", numericUpDownAOverlayGain.Value));
                                            mediafile.FirstOrDefault().Add(new XAttribute("AudioOverlayFadeInDuration", textBoxAOverlayFadeIn.Text));
                                            mediafile.FirstOrDefault().Add(new XAttribute("AudioOverlayFadeOutDuration", textBoxAOverlayFadeOut.Text));
                                            mediafile.FirstOrDefault().Add(new XAttribute("AudioOverlayLayoutMode", comboBoxAOverlayMode.SelectedItem));
                                            if ((string)comboBoxAOverlayMode.SelectedItem == "Custom")
                                            {
                                                mediafile.FirstOrDefault().Add(new XAttribute("AudioOverlayStartTime", textBoxAOverlayStart.Text));
                                                mediafile.FirstOrDefault().Add(new XAttribute("AudioOverlayEndTime", textBoxAOverlayEnd.Text));
                                            }
                                        }

                                    }
                                }
                            }
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Error when processing the XML preset. Is it a WAME preset?");
                    doc = docbackup;
                    Error = true;

                }
                textBoxConfiguration.Text = doc.ToString();
                if (!Error) xmlOpenedNotYetStiched = false;
            }
        }


        private void checkBoxNamingConvention_CheckedChanged(object sender, EventArgs e)
        {
            textBoxNamingConvention.Enabled = checkBoxNamingConvention.Checked;
            AddNamingConventionToDoc();
        }

        private void checkboxVisualOverlay_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender; // get the checkbox object

            int tag = (int)(cb.Tag);

            foreach (Control c in this.tableLayoutPanelIAssets.Controls)
            {

                if ((c.GetType() == typeof(CheckBox)) && (c.Text == "Visual overlay") && (c.Tag != cb.Tag) && ((!bMultiAssetMode) | (bMultiAssetMode && (int)c.Tag != 0)))
                {
                    c.Enabled = !cb.Checked;
                }
            }
            UpdateStitchAndOverlaysInDoc();
        }

        private void checkboxAudioOverlay_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender; // get the checkbox object

            int tag = (int)(cb.Tag);

            foreach (Control c in this.tableLayoutPanelIAssets.Controls)
            {
                if ((c.GetType() == typeof(CheckBox)) && (c.Text == "Audio overlay") && (c.Tag != cb.Tag) && ((!bMultiAssetMode) | (bMultiAssetMode && (int)c.Tag != 0)))
                {
                    c.Enabled = !cb.Checked;
                }
            }
            UpdateStitchAndOverlaysInDoc();
        }


        private void AddNamingConventionToDoc()
        {
            bool Error = false;
            // Let's see if one stich button is enabled
            if (checkBoxNamingConvention.Checked | !xmlOpenedNotYetNamedConvention) // name convention checkbox is checked, or checkbox as been selected in the past for this file, so let's modify the xml doc
            {
                XDocument docbackup = doc;
                try
                {
                    var test = doc.Element("Presets");
                    if (test == null) // It's an "old" preset file without the Presets attribute
                    {
                        var namingconvention = doc
                      .Element("Preset").Attributes()
                      .Where(p => p.Name == "DefaultMediaOutputFileName")
                      .FirstOrDefault();

                        if (checkBoxNamingConvention.Checked) // We add or update
                        {
                            if (namingconvention != null) // already exist
                            {
                                namingconvention.Value = textBoxNamingConvention.Text;
                            }
                            else // do not exist
                            {
                                doc.Element("Preset").Add(new XAttribute("DefaultMediaOutputFileName", textBoxNamingConvention.Text));
                            }
                        }
                        else // we delete it
                        {
                            if (namingconvention != null) namingconvention.Remove();
                        }
                    }
                    else // new preset with Presets atribute
                    {
                        var presets = doc.Element("Presets").Elements("Preset");

                        foreach (var preset in presets)
                        {
                            var namingconvention = preset.Attributes()
                                 .Where(p => p.Name == "DefaultMediaOutputFileName")
                                 .FirstOrDefault();


                            if (checkBoxNamingConvention.Checked) // We add or update
                            {
                                if (namingconvention != null) // already exist
                                {
                                    namingconvention.Value = textBoxNamingConvention.Text;
                                }
                                else // do not exist
                                {
                                    preset.Add(new XAttribute("DefaultMediaOutputFileName", textBoxNamingConvention.Text));
                                }
                            }
                            else // we delete it
                            {
                                if (namingconvention != null) namingconvention.Remove();
                            }
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Error when processing the XML preset. Is it a WAME preset?");
                    doc = docbackup;
                    Error = true;
                }
                textBoxConfiguration.Text = doc.ToString();
                if (!Error) xmlOpenedNotYetNamedConvention = false;
            }
        }


        private void AddVSSRotationToDoc()
        {
            bool Error = false;

            if (checkBoxVSS.Checked | !xmlOpenedNotYetVSSRotation) // name convention checkbox is checked, or checkbox as been selected in the past for this file, so let's modify the xml doc
            {
                XDocument docbackup = doc;

                try
                {
                    var rootpresets = doc.Element("Presets");
                    if (rootpresets != null) // It's an v5 preset with Presets attribute
                    {
                        // VSS Rotation
                        if (rootpresets.Attributes("Rotation").Count() == 0) rootpresets.Attributes("Rotation").Remove();
                        if (checkBoxVSS.Checked) rootpresets.Add(new XAttribute("Rotation", "Auto"));
                    }
                }
                catch
                {
                    MessageBox.Show("Error when processing the XML preset. Is it a WAME preset?");
                    doc = docbackup;
                    Error = true;
                }
                textBoxConfiguration.Text = doc.ToString();
                if (!Error) xmlOpenedNotYetVSSRotation = false;
            }
        }

        private void EncodingCustom_Load(object sender, EventArgs e)
        {

        }

        private void textBoxNamingConvention_TextChanged(object sender, EventArgs e)
        {
            AddNamingConventionToDoc();
        }


        public void textbase_TextChanged(object sender, EventArgs e)
        {
            UpdateStitchAndOverlaysInDoc();
        }

        private void EncodingCustom_Shown(object sender, EventArgs e)
        {
            BuildAssetsPanel();

        }

        private void BuildAssetsPanel()
        {
            tableLayoutPanelIAssets.Visible = false;
            tableLayoutPanelIAssets.ColumnCount += 3;
            foreach (ColumnStyle style in tableLayoutPanelIAssets.ColumnStyles)
            {
                style.SizeType = SizeType.Absolute;
                style.Width = 80;
            }
            tableLayoutPanelIAssets.ColumnStyles[0].SizeType = SizeType.Absolute;
            tableLayoutPanelIAssets.ColumnStyles[0].Width = 20;
            tableLayoutPanelIAssets.ColumnStyles[1].SizeType = SizeType.Absolute;
            tableLayoutPanelIAssets.ColumnStyles[1].Width = 20;
            tableLayoutPanelIAssets.ColumnStyles[2].SizeType = SizeType.Absolute;
            tableLayoutPanelIAssets.ColumnStyles[2].Width = 20;
            tableLayoutPanelIAssets.ColumnStyles[3].SizeType = SizeType.Percent;
            tableLayoutPanelIAssets.ColumnStyles[3].Width = 10;
            int i = 0;

            if (SelectedAssets.Count > 1) // Multi assets mode
            {
                bMultiAssetMode = true;
                tableLayoutPanelIAssets.RowCount = SelectedAssets.Count;
    
                foreach (IAsset asset in SelectedAssets)
                {
                    AddRowControls(i, asset.Name);
                    i++;
                }
                tableLayoutPanelIAssets.Refresh();
            }
            else // Mono asset mode
            {
                bMultiAssetMode = false;
                tableLayoutPanelIAssets.RowCount = SelectedAssets.FirstOrDefault().AssetFiles.Count();
                tabControl1.TabPages[0].Text = "Input files";

                foreach (IAssetFile assetfile in SelectedAssets.FirstOrDefault().AssetFiles)
                {
                    AddRowControls(i, assetfile.Name);
                    i++;
                }
            }
            tableLayoutPanelIAssets.Visible = true;
            comboBoxAOverlayMode.SelectedIndex = 0;
            comboBoxVOverlayMode.SelectedIndex = 0;
        }

        private void AddRowControls(int i, string itemName)
        {
            Button butUp = new Button() { Text = char.ConvertFromUtf32(8593), Tag = i, Width = 20 };
            butUp.Click += new System.EventHandler(butUp_Clicked);

            Button butDwn = new Button() { Text = char.ConvertFromUtf32(8595), Tag = i, Width = 20 };
            butDwn.Click += new System.EventHandler(butDwn_Clicked);

            Label Index = new Label()
            {
                Tag = i,
                AutoSize = true,
                Text = i.ToString()
            };

            Label label = new Label()
            {
                Tag = i,
                AutoSize = true,
                Text = itemName
            };


            CheckBox checkboxVisualOverlay = new CheckBox()
            {
                Text = "Visual overlay",
                Tag = i
            };
            checkboxVisualOverlay.CheckedChanged += new System.EventHandler(checkboxVisualOverlay_CheckedChanged);

            CheckBox checkboxAudioOverlay = new CheckBox()
            {
                Text = "Audio overlay",
                Tag = i
            };
            checkboxAudioOverlay.CheckedChanged += new System.EventHandler(checkboxAudioOverlay_CheckedChanged);

            CheckBox checkboxStitch = new CheckBox()
            {
                Text = "Stitch",
                Tag = i
            };
            checkboxStitch.CheckedChanged += new System.EventHandler(checkBoxStitch_CheckedChanged);

            CheckBox checkboxTime = new CheckBox()
            {
                Text = "Edit times",
                Tag = i,
                Enabled = false
            };
            checkboxTime.CheckedChanged += new System.EventHandler(checkBoxTime_CheckedChanged);

            TextBox textbaseStart = new TextBox()
            {
                Text = "00:00:00",
                Tag = i,
                Enabled = false
            };
            textbaseStart.TextChanged += new System.EventHandler(textbase_TextChanged);

            TextBox textbaseEnd = new TextBox()
            {
                Text = "00:00:05.3",
                Tag = i,
                Enabled = false
            };
            textbaseEnd.TextChanged += new System.EventHandler(textbase_TextChanged);

            tableLayoutPanelIAssets.Controls.Add(butUp, 0 /* Column Index */, i /* Row index */);
            tableLayoutPanelIAssets.Controls.Add(butDwn, 1 /* Column Index */, i /* Row index */);
            tableLayoutPanelIAssets.Controls.Add(Index, 2 /* Column Index */, i /* Row index */);
            tableLayoutPanelIAssets.Controls.Add(label, 3 /* Column Index */, i /* Row index */);
            tableLayoutPanelIAssets.Controls.Add(checkboxVisualOverlay, 4 /* Column Index */, i /* Row index */);
            tableLayoutPanelIAssets.Controls.Add(checkboxAudioOverlay, 5 /* Column Index */, i /* Row index */);
            tableLayoutPanelIAssets.Controls.Add(checkboxStitch, 6 /* Column Index */, i /* Row index */);
            tableLayoutPanelIAssets.Controls.Add(checkboxTime, 7 /* Column Index */, i /* Row index */);
            tableLayoutPanelIAssets.Controls.Add(textbaseStart, 8 /* Column Index */, i /* Row index */);
            tableLayoutPanelIAssets.Controls.Add(textbaseEnd, 9 /* Column Index */, i /* Row index */);
        }

        private void butDwn_Clicked(object sender, EventArgs e)
        {
            int mytag = (int)((Button)sender).Tag;
            SwapControls(mytag, mytag + 1);
            DisableControls();
            UpdateStitchAndOverlaysInDoc();
        }

        private void butUp_Clicked(object sender, EventArgs e)
        {
            int mytag = (int)((Button)sender).Tag;
            SwapControls(mytag, mytag - 1);
            DisableControls();
            UpdateStitchAndOverlaysInDoc();
        }

        private void SwapControls(int indexrow1, int indexrow2)
        {
            tableLayoutPanelIAssets.Visible = false;
            for (int col = 0; col < tableLayoutPanelIAssets.ColumnCount; col++)
            {
                if (col != 2) // col = 2 it's Visual Index column
                {
                    Control controw1 = tableLayoutPanelIAssets.GetControlFromPosition(col, indexrow1);
                    Control controw2 = tableLayoutPanelIAssets.GetControlFromPosition(col, indexrow2);
                    tableLayoutPanelIAssets.SetRow(controw1, indexrow2);
                    tableLayoutPanelIAssets.SetRow(controw2, indexrow1);
                    if (bMultiAssetMode) // if we have multiple assets as source, then let's exchange the assets and update the tag
                    {
                        SwapSelectedAssets(indexrow1, indexrow2); // SelectedAssets, 
                        controw1.Tag = indexrow2;
                        controw2.Tag = indexrow1;
                    }
                }
            }
            tableLayoutPanelIAssets.Visible = true;
        }


        private void SwapSelectedAssets(int index1, int index2)
        {
            // If nothing needs to be swapped, just return the original collection.
            if (index1 == index2)
                return ;

               // Swap the items.
            IAsset temp = SelectedAssets[index1];
            SelectedAssets[index1] = SelectedAssets[index2];
            SelectedAssets[index2] = temp;

     
        }

        private void VOverlaySetting_ValueChanged(object sender, EventArgs e)
        {
            UpdateStitchAndOverlaysInDoc();
        }

        private void AOverlaySetting_ValueChanged(object sender, EventArgs e)
        {
            UpdateStitchAndOverlaysInDoc();
        }

        private void CheckBoxAOverlayLoop_CheckedChanged(object sender, EventArgs e)
        {
            textBoxAOverlayGap.Enabled = ((CheckBox)sender).Checked;
            AOverlaySetting_ValueChanged(sender, e);
        }

        private void comboBoxAOverlayMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((string)((ComboBox)sender).SelectedItem == "Custom")
            {
                textBoxAOverlayStart.Enabled = true;
                textBoxAOverlayEnd.Enabled = true;
            }
            else
            {
                textBoxAOverlayStart.Enabled = false;
                textBoxAOverlayEnd.Enabled = false;
            }
            AOverlaySetting_ValueChanged(sender, e);
        }

        private void comboBoxVOverlayMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((string)((ComboBox)sender).SelectedItem == "Custom")
            {
                textBoxVOverlayStartTime.Enabled = true;
                textBoxVOverlayEndTime.Enabled = true;
            }
            else
            {
                textBoxVOverlayStartTime.Enabled = false;
                textBoxVOverlayEndTime.Enabled = false;
            }

            VOverlaySetting_ValueChanged(sender, e);
        }

        private void checkBoxVSS_CheckedChanged(object sender, EventArgs e)
        {
            AddVSSRotationToDoc();
        }
    }
}
