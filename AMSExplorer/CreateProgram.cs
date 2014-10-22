//----------------------------------------------------------------------- 
// <copyright file="CreateProgram.cs" company="Microsoft">Copyright (c) Microsoft Corporation. All rights reserved.</copyright> 
// <license>
// Azure Media Services Explorer Ver. 3.0
// Licensed under the Apache License, Version 2.0 (the "License"); 
// you may not use this file except in compliance with the License. 
// You may obtain a copy of the License at 
//  
// http://www.apache.org/licenses/LICENSE-2.0 
//  
// Unless required by applicable law or agreed to in writing, software 
// distributed under the License is distributed on an "AS IS" BASIS, 
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
// See the License for the specific language governing permissions and 
// limitations under the License. 
// </license> 

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AMSExplorer
{
    public partial class CreateProgram : Form
    {
        public string ChannelName;

        public string ProgramName
        {
            get { return textboxprogramname.Text; }
            set { textboxprogramname.Text = value; }
        }

        public string ProgramDescription
        {
            get { return textBoxDescription.Text; }
            set { textBoxDescription.Text = value; }
        }


        public TimeSpan archiveWindowLength
        {
            get
            {
                return new TimeSpan((int)numericUpDownArchiveDays.Value, (int)numericUpDownArchiveHours.Value, (int)numericUpDownArchiveMinutes.Value, 0); ;
            }
            set
            {
                numericUpDownArchiveDays.Value = value.Days;
                numericUpDownArchiveHours.Value = value.Hours;
                numericUpDownArchiveMinutes.Value = value.Minutes;
            }
        }

        public bool ProposeScaleUnit
        {
            set
            {
                checkBoxAddScaleUnit.Checked = value;
                checkBoxAddScaleUnit.Visible = value;
            }
        }

        public bool ScaleUnit
        {
            get
            {
                return checkBoxAddScaleUnit.Checked;
            }
        }



        public string AssetName
        {
            get { return textBoxAssetName.Text; }
            set { textBoxAssetName.Text = value; }
        }

        public bool CreateLocator
        {
            get { return checkBoxCreateLocator.Checked; }
            set { checkBoxCreateLocator.Checked = value; }
        }


        public CreateProgram()
        {
            InitializeComponent();
        }

        private void CreateLocator_Load(object sender, EventArgs e)
        {
            this.Text = string.Format(this.Text, ChannelName);
            checkBoxCreateLocator.Text = string.Format(checkBoxCreateLocator.Text, Properties.Settings.Default.DefaultLocatorDurationDays);
        }
    }
}
