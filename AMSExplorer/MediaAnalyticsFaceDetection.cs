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
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace AMSExplorer
{
    public partial class MediaAnalyticsFaceDetection : Form
    {
        private CloudMediaContext _context;
        private IMediaProcessor _processor;
        private bool _preview;
        private Image _processorImage;

        public string MIInputAssetName
        {
            get
            {
                return labelAssetName.Text;
            }
            set
            {
                labelAssetName.Text = value;
            }
        }
        public string MIOutputAssetName
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


        public string MIJobName
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


        public string JsonConfig()
        {
            // Example of config :
            //  @"{'Version':'1.0', 'Options': {'AggregateEmotionWindowMs':'987','Mode':'AggregateEmotion','AggregateEmotionIntervalMs':'342'}}"

            dynamic obj = new JObject();
            obj.Version = "1.0";
            obj.Options = new JObject();

            if (radioButtonFaceDetection.Checked)
            {
                obj.Options.Mode = Constants.FaceDetectionFaces;
            }
            else if (radioButtonPerFaceEmotionDetection.Checked)
            {
                obj.Options.Mode = Constants.FaceDetectionPerFaceEmotion;
            }
            else if (radioButtonAggregateEmotionDetection.Checked)
            {
                obj.Options.Mode = Constants.FaceDetectionAggregateEmotion;
                obj.Options.AggregateEmotionWindowMs = numericUpDownAggregateWindow.Value.ToString("F0");
                obj.Options.AggregateEmotionIntervalMs = numericUpDownAggregateInterval.Value.ToString("F0");
            }
           
            return JsonConvert.SerializeObject(obj);
        }

        public MediaAnalyticsFaceDetection(CloudMediaContext context, IMediaProcessor processor, Image processorImage, bool preview)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            _context = context;
            _processor = processor;
            _preview = preview;
            _processorImage = processorImage;
            buttonJobOptions.Initialize(_context);
        }


        private void MediaAnalyticsFaceDetection_Load(object sender, EventArgs e)
        {
            // we don't have yet link or picture for Video Analytics Greneric. Let's use Yammer group
            moreinfoprofilelink.Links.Add(new LinkLabel.Link(0, moreinfoprofilelink.Text.Length, Constants.LinkMoreYammerAMSPreview));
            moreinfoprofilelink.Visible = true;

            labelProcessorName.Text = _processor.Name;
            labelPreview.Visible = _preview;
            labelProcessorVersion.Text = string.Format(labelProcessorVersion.Text, _processor.Version);
            buttonOk.Image = _processorImage;
            this.Text = _processor.Name;
        }


        private void moreinfoprofilelink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Send the URL to the operating system.
            Process.Start(e.Link.LinkData as string);
        }

        private void radioButtonAggregateEmotionDetection_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxAggregateSettings.Enabled = radioButtonAggregateEmotionDetection.Checked;
        }

        private void numericUpDownAggregateWindow_ValueChanged(object sender, EventArgs e)
        {
            checkValues();
        }

        private void checkValues()
        {

            if (numericUpDownAggregateWindow.Value <= numericUpDownAggregateInterval.Value)
            {
                errorProvider1.SetError(numericUpDownAggregateInterval, "Window length must be larger than interval");
            }
            else
            {
                errorProvider1.SetError(numericUpDownAggregateInterval, String.Empty);
            }
        }

        private void numericUpDownAggregateInterval_ValueChanged(object sender, EventArgs e)
        {
            checkValues();

        }
    }
}
