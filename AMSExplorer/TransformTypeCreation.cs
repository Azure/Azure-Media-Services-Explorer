//----------------------------------------------------------------------------------------------
//    Copyright 2022 Microsoft Corporation
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
using System.Windows.Forms;

namespace AMSExplorer
{
    public enum simpleTransformType
    {
        encode = 0,
        analyze,
        facedetection,
        customJson
    }

    public partial class TransformTypeCreation : Form
    {
        private readonly bool _displayNewTransforMsg;

        public simpleTransformType TransformType
        {
            get
            {
                if (radioButtonEncoding.Checked)
                {
                    return simpleTransformType.encode;
                }
                else if (radioButtonAVAnalyze.Checked)
                {
                    return simpleTransformType.analyze;
                }
                else if (radioButtonFaceDetection.Checked)
                {
                    return simpleTransformType.facedetection;
                }
                else
                {
                    return simpleTransformType.customJson;
                }
            }
        }


        public TransformTypeCreation(bool displayNewTransforMsg = true)
        {
            InitializeComponent();
            Icon = Bitmaps.Azure_Explorer_ico;
            _displayNewTransforMsg = displayNewTransforMsg;
        }

        private void TransformTypeCreation_Load(object sender, EventArgs e)
        {
            // DpiUtils.InitPerMonitorDpi(this);
            labelNoAssetFilter.Visible = _displayNewTransforMsg;
        }

        private void TransformTypeCreation_DpiChanged(object sender, DpiChangedEventArgs e)
        {
            // DpiUtils.UpdatedSizeFontAfterDPIChange(labelTitle, e);
        }

        private void TransformTypeCreation_Shown(object sender, EventArgs e)
        {
            Telemetry.TrackPageView(this.Name);
        }
    }
}