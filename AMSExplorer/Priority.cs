//----------------------------------------------------------------------------------------------
//    Copyright 2021 Microsoft Corporation
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
using System.Collections.Generic;
using System.Windows.Forms;

namespace AMSExplorer
{
    public partial class PriorityForm : Form
    {
        public Priority? JobPriority
        {
            get
            {
                Priority p;
                switch (comboBoxPriority.SelectedItem)
                {
                    case "Low":
                        p = Priority.Low;
                        break;

                    case "High":
                        p = Priority.High;
                        break;

                    default:
                        p = Priority.Normal;
                        break;
                }

                return p;
            }

            set => comboBoxPriority.SelectedIndex = comboBoxPriority.Items.IndexOf(value);
        }
        public PriorityForm()
        {
            InitializeComponent();
            Icon = Bitmaps.Azure_Explorer_ico;

            List<string> ep = new() { Priority.Low, Priority.Normal, Priority.High };
            comboBoxPriority.Items.AddRange(ep.ToArray());
            comboBoxPriority.SelectedIndex = 1;

        }

        private void Priority_Load(object sender, EventArgs e)
        {
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {

        }

        private void PriorityForm_DpiChanged(object sender, DpiChangedEventArgs e)
        {
            // DpiUtils.UpdatedSizeFontAfterDPIChange(label2, e);
        }
    }
}
