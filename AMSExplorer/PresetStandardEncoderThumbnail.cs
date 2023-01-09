//----------------------------------------------------------------------------------------------
//    Copyright 2023 Microsoft Corporation
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

using Azure.ResourceManager.Media.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace AMSExplorer
{
    public partial class PresetStandardEncoderThumbnail : Form
    {
        public StandardEncoderPreset CustomSpritePreset
        {
            get
            {
                IList<MediaCodecBase> listCodec = new List<MediaCodecBase>();
                IList<MediaFormatBase> listFormat = new List<MediaFormatBase>();

                string step = string.IsNullOrWhiteSpace(textBoxThTimeStepJPG.Text) ? null : textBoxThTimeStepJPG.Text;
                string range = string.IsNullOrWhiteSpace(textBoxThTimeRangeJPG.Text) ? null : textBoxThTimeRangeJPG.Text;

                if (radioButtonPNG.Checked) // PNG
                {
                    listCodec.Add(
                        // Generate a set of PNG thumbnails
                        new PngImage(textBoxThTimeStartJPG.Text)
                        {
                            Step = step,
                            Range = range,
                            Layers = {
                                new PngLayer()
                                {
                                    Width= widthSize,
                                    Height= heightSize
                                }
                                    }
                        }
                        );

                    listFormat.Add(
                        new PngFormat(filenamePattern: textBoxThFileNameJPG.Text)
                        );
                }
                else // JPG or Sprite JPG
                {
                    int? spriteC = radioButtonSprite.Checked ? (int?)numericUpDownSpriteColumn.Value : null;

                    listCodec.Add(
                        // Generate a set of PNG thumbnails
                        new JpgImage(textBoxThTimeStartJPG.Text)
                        {
                            SpriteColumn = spriteC,
                            Start = textBoxThTimeStartJPG.Text,
                            Step = step,
                            Range = range,
                            Layers =
                            {
                                new JpgLayer
                                {
                                    Width= widthSize,
                                    Height= heightSize
                                }
                            }
                        }
                        );

                    listFormat.Add(
                        new JpgFormat(textBoxThFileNameJPG.Text)
                        );
                }
                return new StandardEncoderPreset(codecs: listCodec, formats: listFormat);
            }
        }


        private string widthSize
        {
            get
            {
                return string.IsNullOrWhiteSpace(textBoxWidth.Text) ? null : textBoxWidth.Text;
            }
        }

        private string heightSize
        {
            get
            {
                return string.IsNullOrWhiteSpace(textBoxHeight.Text) ? null : textBoxHeight.Text;
            }
        }


        public PresetStandardEncoderThumbnail()
        {
            InitializeComponent();
            Icon = Bitmaps.Azure_Explorer_ico;
        }

        private void PresetStandardEncoderThumbnail_Load(object sender, EventArgs e)
        {

            moreinfoprofilelink.Links.Clear();
            moreinfoprofilelink.Links.Add(new LinkLabel.Link(0, moreinfoprofilelink.Text.Length, Constants.LinkMoreInfoMediaEncoderThumbnail));
        }

        private void moreinfoprofilelink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Send the URL to the operating system.
            var p = new Process
            {
                StartInfo = new ProcessStartInfo { FileName = e.Link.LinkData as string, UseShellExecute = true }
            }; p.Start();
        }


        private void PresetStandardEncoderThumbnail_DpiChanged(object sender, DpiChangedEventArgs e)
        {
            // DpiUtils.UpdatedSizeFontAfterDPIChange(labelMES, e);
        }


        private void radioButtonSprite_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownSpriteColumn.Enabled = radioButtonSprite.Checked;
            numericUpDownThQuality.Enabled = radioButtonSprite.Checked || radioButtonJPG.Checked;
        }

        private void PresetStandardEncoderThumbnail_Shown(object sender, EventArgs e)
        {
            Telemetry.TrackPageView(this.Name);
        }
    }
}
