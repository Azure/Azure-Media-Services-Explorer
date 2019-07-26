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
using System.Windows.Forms;

namespace AMSExplorer
{
    public enum simpleTransformType
    {
        encode = 0,
        analyze,
        facedetection
    }

    public partial class TransformTypeCreation : Form
    {

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
                else
                {
                    return simpleTransformType.facedetection;
                }
            }
        }


        public TransformTypeCreation()
        {
            InitializeComponent();
            Icon = Bitmaps.Azure_Explorer_ico;
        }

        private void TransformTypeCreation_Load(object sender, EventArgs e)
        {

        }
    }
}