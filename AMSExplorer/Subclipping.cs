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
using System.Xml.Serialization;
using System.Runtime.Serialization;



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
                checkBoxTrimming.Enabled = _listAssets.Count == 1; // only trimming for one asset selected

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


        public SubClipTrimmingData GetSubClipTrimmingData()
        {

            var data = new SubClipTrimmingData();
            if (checkBoxRawMode.Checked) // RAW Mode
            {
                data.StartTime = string.IsNullOrWhiteSpace(textBoxRawStart.Text) ? null : textBoxRawStart.Text;
                // data.Duration = string.IsNullOrWhiteSpace(textBoxRawEnd.Text) ? null : textBoxRawEnd.Text;
                //                  _filter.PresentationTimeRange.Timescale = string.IsNullOrWhiteSpace(textBoxRawTimescale.Text) ? null : textBoxRawTimescale.Text;
            }
            else  // Default mode
            {
                data = GetSubClipTrimmingDataDefaultMode;
            }
            return data;
        }

        private SubClipTrimmingData GetSubClipTrimmingDataDefaultMode
        {
            get
            {
                var trimmingdata = new SubClipTrimmingData();
                if (checkBoxTrimming.Checked)
                {
                    trimmingdata.StartTime = AssetInfo.GetXMLSerializedTimeSpan(timeControlStart.GetTimeStampAsTimeSpanWithOffset());
                    trimmingdata.Duration = AssetInfo.GetXMLSerializedTimeSpan(timeControlEnd.GetTimeStampAsTimeSpanWithOffset() - timeControlStart.GetTimeStampAsTimeSpanWithOffset());
                }
                return trimmingdata;
            }
        }

        public SubClipConfiguration GetSubclippingConfiguration()
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

                string filter = "TopBitrate";
                string mode = "ArchiveTopBitrate";
                if (radioButtonArchiveAllBitrate.Checked)
                {
                    filter = "*";
                    mode = "ArchiveAllBitrates";
                }
                streamsxml.Add(new XElement(ns + "VideoStream", filter));
                streamsxml.Add(new XElement(ns + "AudioStream", filter));
                output.Attribute("FileName").SetValue(mode + "_{Basename}.mp4");

                if (checkBoxTrimming.Checked)
                {
                    var subdata = GetSubClipTrimmingData();
                    sourcexml.Add(new XAttribute("StartTime", subdata.StartTime));
                    sourcexml.Add(new XAttribute("Duration", subdata.Duration));
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
                var subdata = GetSubClipTrimmingData();
                var config = new SubClipConfiguration()
                {
                    Reencode = true,
                    Trimming = false
                };

                if (checkBoxTrimming.Checked)
                {
                    config.Trimming = true;
                    config.StartTimeForReencode = subdata.StartTime;
                    config.DurationForReencode = subdata.Duration;
                }
                return config;
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

                var ptr = GetSubClipTrimmingDataDefaultMode;
                //textBoxRawTimescale.Text = ptr.Timescale;
                textBoxRawStart.Text = ptr.StartTime;
                textBoxRawEnd.Text = ptr.Duration;
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

        private void checkBoxTrimming_CheckedChanged(object sender, EventArgs e)
        {
            timeControlStart.Enabled = checkBoxTrimming.Checked;
            timeControlEnd.Enabled = checkBoxTrimming.Checked;
            CheckIfErrorTimeControls();
        }
    }
    public struct MyTimeSpan : IXmlSerializable
    {
        TimeSpan value;

        public MyTimeSpan(TimeSpan timeSpan)
        {
            this.value = timeSpan;
        }

        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(System.Xml.XmlReader reader)
        {
            this.value = TimeSpan.Parse(reader.ReadElementContentAsString());
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            writer.WriteValue(this.value.ToString());
        }
    }


    /*
    [Serializable]
    public class MyClass
    {
        // Local Variable
        private TimeSpan m_TimeSinceLastEvent;

        // Public Property - XmlIgnore as it doesn't serialize anyway
        [XmlIgnore]
        public TimeSpan TimeSinceLastEvent
        {
            get { return m_TimeSinceLastEvent; }
            set { m_TimeSinceLastEvent = value; }
        }

        // Pretend property for serialization
        [XmlElement("TimeSinceLastEvent")]
        public long TimeSinceLastEventTicks
        {
            get { return m_TimeSinceLastEvent.Ticks; }
            set { m_TimeSinceLastEvent = new TimeSpan(value); }
        }
    }
     * */
}
