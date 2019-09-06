﻿//----------------------------------------------------------------------------------------------
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

using Microsoft.Azure.Management.Media.Models;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace AMSExplorer
{
    public partial class PresetFaceDetector : Form
    {

        public string TransformName => textBoxTransformName.Text;

        public string Description => string.IsNullOrWhiteSpace(textBoxDescription.Text) ? null : textBoxDescription.Text;

        public AnalysisResolution AnalysisResolutionMode
        {
            get
            {
                if (radioButtonSource.Checked)
                {
                    return AnalysisResolution.SourceResolution;
                }
                else if (radioButtonStandard.Checked)
                {
                    return AnalysisResolution.StandardDefinition;
                }
                else
                {
                    return null;
                }

            }
        }


        public PresetFaceDetector()
        {
            InitializeComponent();
            Icon = Bitmaps.Azure_Explorer_ico;
        }

        private void PresetFaceDetector_Load(object sender, EventArgs e)
        {
            DpiUtils.InitPerMonitorDpi(this);

            // to scale the bitmap in the buttons
            HighDpiHelper.AdjustControlImagesDpiScale(panel1);
            
            moreinfoprofilelink.Links.Add(new LinkLabel.Link(0, moreinfoprofilelink.Text.Length, Constants.LinkMoreInfoVideoAnalyzer));
            UpdateTransformLabel();
        }


        private void moreinfoprofilelink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Send the URL to the operating system.
            Process.Start(e.Link.LinkData as string);
        }

        private void radioButtonAudioAndVideo_CheckedChanged(object sender, EventArgs e)
        {
            UpdateTransformLabel();
        }

        private void UpdateTransformLabel()
        {
            if (radioButtonSource.Checked)
            {
                textBoxTransformName.Text = "FaceDetector-SourceRes";

            }
            else if (radioButtonStandard.Checked)
            {
                textBoxTransformName.Text = "FaceDetector-StdRes";
            }
            else
            {
                textBoxTransformName.Text = "FaceDetector";
            }
        }

        private void PresetFaceDetector_DpiChanged(object sender, DpiChangedEventArgs e)
        {
            DpiUtils.UpdatedSizeFontAfterDPIChange(labelTitle, e);

            // to scale the bitmap in the buttons
            HighDpiHelper.AdjustControlImagesDpiScale(panel1);
        }
    }
}
