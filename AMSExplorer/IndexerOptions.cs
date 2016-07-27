//----------------------------------------------------------------------------------------------
//    Copyright 2016 Microsoft Corporation
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


namespace AMSExplorer
{
    public partial class IndexerOptions : Form
    {

        public IndexerOptionsVar IndexerGenerationOptions
        {
            get
            {
                return new IndexerOptionsVar()
                {
                    AIB = checkBoxAIB.Checked,
                    Keywords = checkBoxKeywords.Checked,
                    SAMI = checkBoxSAMI.Checked,
                    TTML = checkBoxTTML.Checked,
                    WebVTT = checkBoxWEBVTT.Checked
                };
            }
            set
            {
                checkBoxAIB.Checked = value.AIB;
                checkBoxKeywords.Checked = value.Keywords;
                checkBoxSAMI.Checked = value.SAMI;
                checkBoxTTML.Checked = value.TTML;
                checkBoxWEBVTT.Checked = value.WebVTT;
            }
        }

        public IndexerOptions(bool IndexerV2 = false)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            if (IndexerV2) groupBoxOther.Visible = false;
        }


        private void IndexerOptions_Load(object sender, EventArgs e)
        {

        }
    }
}
