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

        public Subclipping(CloudMediaContext context, List<IAsset> assetlist)
        {
            InitializeComponent();
            buttonJobOptions.Initialize(context);
            this.Icon = Bitmaps.Azure_Explorer_ico;
            _context = context;
            _parentassetmanifestdata = new ManifestTimingData();
            _listAssets = assetlist;


            if (_listAssets.Count == 1 && _listAssets.FirstOrDefault() != null)
            {
                var myAsset = assetlist.FirstOrDefault();
                textBoxAssetName.Text = myAsset.Name;

                // let's try to read asset timing
                _parentassetmanifestdata = AssetInfo.GetManifestTimingData(myAsset);

                if (!_parentassetmanifestdata.Error)  // we were able to read asset timings and not live
                {
                    _timescale = timeControlStart.TimeScale = timeControlEnd.TimeScale = _parentassetmanifestdata.TimeScale;
                    timeControlStart.ScaledFirstTimestampOffset = timeControlEnd.ScaledFirstTimestampOffset = _parentassetmanifestdata.TimestampOffset;

                    textBoxOffset.Text = _parentassetmanifestdata.TimestampOffset.ToString();
                    labelOffset.Visible = textBoxOffset.Visible = true;

                    textBoxFilterTimeScale.Text = _timescale.ToString();
                    textBoxFilterTimeScale.Visible = labelAssetTimescale.Visible = true;

                    timeControlStart.Max = timeControlEnd.Max = new TimeSpan(AssetInfo.ReturnTimestampInTicks(_parentassetmanifestdata.AssetDuration, _parentassetmanifestdata.TimeScale));
                    timeControlEnd.SetTimeStamp(timeControlEnd.Max);

                    labelassetduration.Visible = textBoxAssetDuration.Visible = true;
                    textBoxAssetDuration.Text = timeControlStart.Max.ToString(@"d\.hh\:mm\:ss") + (_parentassetmanifestdata.IsLive ? " (LIVE)" : "");
                    // let set duration and active track bat
                    timeControlStart.ScaledTotalDuration = timeControlEnd.ScaledTotalDuration = _parentassetmanifestdata.AssetDuration;
                    timeControlStart.DisplayTrackBar = timeControlEnd.DisplayTrackBar = true;
                }

                else // one asset but not able to read asset timings
                {
                    timeControlStart.DisplayTrackBar = timeControlEnd.DisplayTrackBar = false;
                    timeControlStart.TimeScale = timeControlEnd.TimeScale = _timescale;
                    timeControlStart.Max = timeControlEnd.Max = TimeSpan.MaxValue;
                    //timeControlEnd.SetTimeStamp(timeControlEnd.Max);
                }
            }
            else // several assets
            {
                groupBoxTrimming.Enabled = panelAssetInfo.Visible = false; // no trimming and no asset info

                timeControlStart.DisplayTrackBar = timeControlEnd.DisplayTrackBar = false;
                timeControlStart.TimeScale = timeControlEnd.TimeScale = _timescale;
                timeControlStart.Max = timeControlEnd.Max = TimeSpan.MaxValue;
                //timeControlEnd.SetTimeStamp(timeControlEnd.Max);
            }
        }


        private void Subclipping_Load(object sender, EventArgs e)
        {
            moreinfoprofilelink.Links.Add(new LinkLabel.Link(0, moreinfoprofilelink.Text.Length, Constants.LinkHowIMoreInfoSubclipping));
            CheckIfErrorTimeControls();
        }


        private SubClipTrimmingData GetSubClipTrimmingData()
        {
            var trimmingdata = new SubClipTrimmingData();
            if (checkBoxTrimming.Checked)
            {
                trimmingdata.StartTime = AssetInfo.GetXMLSerializedTimeSpan(timeControlStart.GetTimeStampAsTimeSpanWithOffset());
                trimmingdata.Duration = AssetInfo.GetXMLSerializedTimeSpan(timeControlEnd.GetTimeStampAsTimeSpanWithOffset() - timeControlStart.GetTimeStampAsTimeSpanWithOffset());
            }
            return trimmingdata;
        }

        public SubClipConfiguration GetSubclippingConfiguration()
        {
            var config = GetSubclippingInternalConfiguration();
            if (!radioButtonClipWithReencode.Checked && !string.IsNullOrEmpty(textBoxConfiguration.Text))
            {
                config.Configuration = textBoxConfiguration.Text;
            }
            return config;
        }


        private SubClipConfiguration GetSubclippingInternalConfiguration()
        {
            if (radioButtonArchiveAllBitrate.Checked || radioButtonArchiveTopBitrate.Checked) // Archive, no reencoding
            {
                // Prepare the Subclipping xml
                XDocument doc = XDocument.Load(Path.Combine(Application.StartupPath + Constants.PathConfigFiles, "RenderedSubclip.xml"));
                XNamespace ns = "http://www.windowsazure.com/media/encoding/Preset/2014/03";

                var presetxml = doc.Element(ns + "Preset");
                var sourcexml = presetxml.Element(ns + "Sources").Element(ns + "Source");
                var streamsxml = sourcexml.Element(ns + "Streams");
                var output = presetxml.Element(ns + "Outputs").Element(ns + "Output"); ;

                string filter = radioButtonArchiveAllBitrate.Checked ? "*" : "TopBitrate";
                string mode = radioButtonArchiveAllBitrate.Checked ? "ArchiveAllBitrates" : "ArchiveTopBitrate";

                streamsxml.Add(new XElement(ns + "VideoStream", filter));
                streamsxml.Add(new XElement(ns + "AudioStream", filter));
                output.Attribute("FileName").SetValue(mode + "_{Basename}.mp4");

                if (checkBoxTrimming.Checked)
                {
                    var subdata = GetSubClipTrimmingData();
                    sourcexml.SetAttributeValue("StartTime", subdata.StartTime);
                    sourcexml.SetAttributeValue("Duration", subdata.Duration);
                }

                return new SubClipConfiguration()
                {
                    Configuration = doc.Declaration.ToString() + doc.ToString(),
                    Reencode = false,
                    Trimming = false
                };
            }
            else //  (radioButtonClipWithReencode.Checked) means Reencoding
            {
                var config = new SubClipConfiguration()
                {
                    Reencode = true,
                    Trimming = false
                };

                if (checkBoxTrimming.Checked)
                {
                    var subdata = GetSubClipTrimmingData();
                    config.Trimming = true;
                    config.StartTimeForReencode = subdata.StartTime;
                    config.DurationForReencode = subdata.Duration;
                }
                return config;
            }
        }


        private void moreinfoprofilelink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData as string);
        }


        private void CheckIfErrorTimeControls()
        {
            // time start control
            if (checkBoxTrimming.Checked && timeControlStart.GetTimeStampAsTimeSpanWitoutOffset() > timeControlEnd.GetTimeStampAsTimeSpanWitoutOffset())
            {
                errorProvider1.SetError(timeControlStart, "Start time must be lower than end time");
            }
            else
            {
                errorProvider1.SetError(timeControlStart, String.Empty);
            }

            // time end control
            if (checkBoxTrimming.Checked && timeControlEnd.GetTimeStampAsTimeSpanWitoutOffset() < timeControlStart.GetTimeStampAsTimeSpanWitoutOffset())
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
            ResetConfigXML();
        }


        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void timeControlStart_ValueChanged(object sender, EventArgs e)
        {
            CheckIfErrorTimeControls();
            ResetConfigXML();
        }

        private void checkBoxTrimming_CheckedChanged(object sender, EventArgs e)
        {
            timeControlStart.Enabled = checkBoxTrimming.Checked;
            timeControlEnd.Enabled = checkBoxTrimming.Checked;
            CheckIfErrorTimeControls();
            ResetConfigXML();
        }

        private void UpdateXMLData()
        {
            textBoxConfiguration.Text = GetSubclippingConfiguration().Configuration;
        }

        private void tabPageXML_Enter(object sender, EventArgs e)
        {
            UpdateXMLData();
        }

        private void radioButtonClipWithReencode_CheckedChanged(object sender, EventArgs e)
        {
            textBoxConfiguration.Enabled = panelJob.Visible = !radioButtonClipWithReencode.Checked; // if reencode, xml data is dsplayed in the next box
            buttonOk.Text = radioButtonClipWithReencode.Checked ? "Next" : (string)buttonOk.Tag;
            ResetConfigXML();
        }

        private void radioButtonArchiveTopBitrate_CheckedChanged(object sender, EventArgs e)
        {
            ResetConfigXML();
        }

        private void ResetConfigXML()
        {
            textBoxConfiguration.Text = string.Empty;
        }

        private void radioButtonArchiveAllBitrate_CheckedChanged(object sender, EventArgs e)
        {
            ResetConfigXML();
        }
    }
}
