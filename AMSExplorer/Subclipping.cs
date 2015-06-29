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
using System.Xml;
using System.Xml.Linq;
using System.IO;



namespace AMSExplorer
{
    public partial class Subclipping : Form
    {
        CloudMediaContext _context;
        private List<IAsset> _listAssets;
        private ManifestTimingData _parentassetmanifestdata;
        private long _timescale = TimeSpan.TicksPerSecond;

        public JobOptionsVar JobOptions
        {
            get
            {
                return buttonJobOptions.GetSettings();
            }
            set
            {
                buttonJobOptions.SetSettings(value);
            }
        }

        public Subclipping(CloudMediaContext context, List<IAsset> assetlist)
        {
            InitializeComponent();
            buttonJobOptions.Initialize(context);
            this.Icon = Bitmaps.Azure_Explorer_ico;
            _context = context;
            _parentassetmanifestdata = new ManifestTimingData();
            tabControl1.TabPages.Remove(tabPageTRRaw);
            _listAssets = assetlist;

            var myAsset = assetlist.FirstOrDefault();
            /////////////////////////////////////////////
            // New Asset Filter
            /////////////////////////////////////////////
            if (myAsset != null)
            {

                labelFilterTitle.Text = "Asset Filter";
                textBoxAssetName.Visible = true;
                labelassetname.Visible = true;
                textBoxAssetName.Text = _listAssets != null ? myAsset.Name : string.Empty;

                // let's try to read asset timing
                _parentassetmanifestdata = AssetInfo.GetManifestTimingData(myAsset);

                if (!_parentassetmanifestdata.Error)  // we were able to read asset timings and not live
                {
                    _timescale = timeControlStart.TimeScale = timeControlEnd.TimeScale = _parentassetmanifestdata.TimeScale;
                    timeControlStart.ScaledFirstTimestampOffset = timeControlEnd.ScaledFirstTimestampOffset = _parentassetmanifestdata.TimestampOffset;

                    textBoxOffset.Text = _parentassetmanifestdata.TimestampOffset.ToString();
                    labelOffset.Visible = textBoxOffset.Visible = true;

                    // let's disable trackbars if this is live (duration is not fixed)
                    timeControlStart.DisplayTrackBar = timeControlEnd.DisplayTrackBar = !_parentassetmanifestdata.IsLive;

                    if (!_parentassetmanifestdata.IsLive)  // Not a live content
                    {
                        timeControlStart.Max = timeControlEnd.Max = new TimeSpan(AssetInfo.ReturnTimestampInTicks(_parentassetmanifestdata.AssetDuration, _parentassetmanifestdata.TimeScale));
                        timeControlEnd.SetTimeStamp(timeControlEnd.Max);

                        labelassetduration.Visible = textBoxAssetDuration.Visible = true;
                        textBoxAssetDuration.Text = timeControlStart.Max.ToString(@"d\.hh\:mm\:ss");
                        // let set duration and active track bat
                        timeControlStart.ScaledTotalDuration = timeControlEnd.ScaledTotalDuration = _parentassetmanifestdata.AssetDuration;
                    }

                }

                else // not able to read asset timings
                {
                    timeControlStart.DisplayTrackBar = timeControlEnd.DisplayTrackBar = false;
                    timeControlStart.TimeScale = timeControlEnd.TimeScale = _timescale;
                    timeControlStart.Max = timeControlEnd.Max = TimeSpan.MaxValue;
                    timeControlEnd.SetTimeStamp(timeControlEnd.Max);
                    labelassetduration.Visible = textBoxAssetDuration.Visible = false;
                }
            }

            /////////////////////////////////////////////
            // Existing Asset Filter
            /////////////////////////////////////////////


            // Common code
            textBoxFilterTimeScale.Text = _timescale.ToString();
        }



        private void Subclipping_Load(object sender, EventArgs e)
        {
            moreinfoprofilelink.Links.Add(new LinkLabel.Link(0, moreinfoprofilelink.Text.Length, Constants.LinkHowIMoreInfoDynamicManifest));

            CheckIfErrorTimeControls();
        }

        /*
        public Filter GetFilter
        {
            get
            {
                _filter.Name = newfilter ? textBoxFilterName.Text : _filter.Name;

                if (checkBoxRawMode.Checked) // RAW Mode
                {
                    _filter.PresentationTimeRange.StartTimestamp = string.IsNullOrWhiteSpace(textBoxRawStart.Text) ? null : textBoxRawStart.Text;
                    _filter.PresentationTimeRange.EndTimestamp = string.IsNullOrWhiteSpace(textBoxRawEnd.Text) ? null : textBoxRawEnd.Text;
                    _filter.PresentationTimeRange.LiveBackoffDuration = string.IsNullOrWhiteSpace(textBoxRawBackoff.Text) ? null : textBoxRawBackoff.Text;
                    _filter.PresentationTimeRange.PresentationWindowDuration = string.IsNullOrWhiteSpace(textBoxRawDVR.Text) ? null : textBoxRawDVR.Text;
                    _filter.PresentationTimeRange.Timescale = string.IsNullOrWhiteSpace(textBoxRawTimescale.Text) ? null : textBoxRawTimescale.Text;
                }
                else  // Default mode
                {
                    _filter.PresentationTimeRange = GetFilterPresenTationTRDefaultMode;
                }

                if (_filter.Tracks.Count == 0) _filter.Tracks = null; // to make sure it is null to avoid puting data in JSON

                return _filter;
            }
        }
         * */

        public string GetESubclippingConfiguration()
        {
            {
                
                string config = "";
                string xmlpreset = "";

                if (radioButtonArchiveTopBitrate.Checked)
                {
                    xmlpreset = "ArchiveTopBitrate.xml";
                }
                else if (radioButtonClipTopBitrate.Checked)
                {
                    xmlpreset = "ClipTopBitrate.xml";
                }
                else if (radioButtonClipAllBitrates.Checked)
                {
                    xmlpreset = "ClipAllBitrates.xml";
                }
                else if (radioButtonClipWithReencode.Checked)
                {
                    xmlpreset = null;
                }

                try
                {
                    StreamReader streamReader = new StreamReader(Path.Combine(Application.StartupPath + Constants.PathAMEStdSubclip, xmlpreset));
                    config = streamReader.ReadToEnd();
                    streamReader.Close();
                }
                catch (Exception ex)
                {

                }

                return config;
            }
        }

        private IFilterPresentationTimeRange GetFilterPresenTationTRDefaultMode
        {
            get
            {
                var ptr = new IFilterPresentationTimeRange()
                {
                    StartTimestamp = checkBoxStartTime.Checked ? timeControlStart.GetScaledTimeStampWithOffset() : null,
                    EndTimestamp = checkBoxEndTime.Checked ? timeControlEnd.GetScaledTimeStampWithOffset() : null,
                    Timescale = _timescale.ToString()
                };
                return ptr;
            }
        }

        private bool IsMax(string timestamp)
        {
            if (string.IsNullOrWhiteSpace(timestamp))
            {
                return false;
            }
            else
            {
                return Int64.MaxValue == Int64.Parse(timestamp);
            }
        }

        private bool IsMin(string timestamp)
        {
            if (string.IsNullOrWhiteSpace(timestamp))
            {
                return false;
            }
            else
            {
                return 0 == Int64.Parse(timestamp);
            }
        }


        private void textBoxFilterName_Validating(object sender, CancelEventArgs e)
        {
            TextBox tb = (TextBox)sender;

            if (string.IsNullOrEmpty(tb.Text))
            {
                errorProvider1.SetError(tb, "Please specify a filter name");
            }
            else
            {
                errorProvider1.SetError(tb, String.Empty);
            }
        }


        private void moreinfoprofilelink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData as string);
        }



        private void textBoxFilterName_TextChanged(object sender, EventArgs e)
        {
            buttonOk.Enabled = !string.IsNullOrWhiteSpace(textBoxFilterName.Text);
        }



        private void CheckIfErrorTimeControls()
        {

            // time start control
            if (checkBoxStartTime.Checked && checkBoxEndTime.Checked && timeControlStart.GetTimeStampAsTimeSpanWitoutOffset() > timeControlEnd.GetTimeStampAsTimeSpanWitoutOffset())
            {
                errorProvider1.SetError(timeControlStart, "Start time must be lower than end time");
            }
            else
            {
                errorProvider1.SetError(timeControlStart, String.Empty);
            }




            // time end control
            if (checkBoxEndTime.Checked && checkBoxStartTime.Checked && timeControlEnd.GetTimeStampAsTimeSpanWitoutOffset() < timeControlStart.GetTimeStampAsTimeSpanWitoutOffset())
            {
                errorProvider1.SetError(timeControlEnd, "End time must be higher than start time");
            }
            else
            {
                errorProvider1.SetError(timeControlEnd, String.Empty);
            }
        }



        private void timeControlEnd_ValueChanged(object sender, EventArgs e)
        {
            CheckIfErrorTimeControls();
        }



        private void checkBoxStartTime_CheckedChanged(object sender, EventArgs e)
        {
            timeControlStart.Enabled = checkBoxStartTime.Checked;
            labelStartTimeDefault.Text = checkBoxStartTime.Checked ? string.Empty : (string)labelStartTimeDefault.Tag;
            CheckIfErrorTimeControls();

        }

        private void checkBoxEndTime_CheckedChanged(object sender, EventArgs e)
        {
            timeControlEnd.Enabled = checkBoxEndTime.Checked;
            labelDefaultEnd.Text = checkBoxEndTime.Checked ? string.Empty : (string)labelDefaultEnd.Tag;
            CheckIfErrorTimeControls();
        }

        private void DynManifestFilter_Shown(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void checkBoxRawMode_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxRawMode.Checked)
            {
                var seltab = tabControl1.SelectedTab;
                tabControl1.TabPages.Insert(0, tabPageTRRaw);
                tabControl1.TabPages.Remove(tabPageTR);
                if (seltab == tabPageTR) tabControl1.SelectedTab = tabPageTRRaw;

                var ptr = GetFilterPresenTationTRDefaultMode;
                textBoxRawTimescale.Text = ptr.Timescale;
                textBoxRawStart.Text = ptr.StartTimestamp;
                textBoxRawEnd.Text = ptr.EndTimestamp;
                textBoxRawDVR.Text = ptr.PresentationWindowDuration;
                textBoxRawBackoff.Text = ptr.LiveBackoffDuration;
            }
            else
            {
                var seltab = tabControl1.SelectedTab;
                tabControl1.TabPages.Remove(tabPageTRRaw);
                tabControl1.TabPages.Insert(0, tabPageTR);
                if (seltab == tabPageTRRaw) tabControl1.SelectedTab = tabPageTR;
            }
        }

        private void timeControlStart_ValueChanged(object sender, EventArgs e)
        {
            CheckIfErrorTimeControls();
        }
    }
}
