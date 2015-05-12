//----------------------------------------------------------------------- 
// <copyright file="ProcessFromJobTemplate.cs" company="Microsoft">Copyright (c) Microsoft Corporation. All rights reserved.</copyright> 
// <license>
// Azure Media Services Explorer Ver. 3.2
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
using System.Diagnostics;
using Microsoft.WindowsAzure.MediaServices.Client;
using System.Collections;
using System.IO;

namespace AMSExplorer
{
    public partial class ProcessFromJobTemplate : Form
    {
        private Bitmap bitmap_multitasksinglejob = Bitmaps.modeltaskxenio1;
        private Bitmap bitmap_multitasksmultijobs = Bitmaps.modeltaskxenio2;
        private CloudMediaContext _context;

        private int sortColumn = -1;
        private int _numberselectedassets = 0;

        public IJobTemplate SelectedJobTemplate
        {
            get
            {
                return listViewTemplates.GetSelectedJobTemplate;

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

        public string ProcessingJobName
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


        public string ProcessingPromptText
        {
            set
            {
                label.Text = value;
            }
        }



        public ProcessFromJobTemplate(CloudMediaContext context, int numberselectedassets)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            _context = context;
            _numberselectedassets = numberselectedassets;
            labelWarning.Text = string.Empty;
            buttonJobOptions.Initialize(_context);
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {

        }

        private void listbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonOk.Enabled = listViewTemplates.SelectedItems.Count > 0;
            buttonDeleteTemplate.Enabled = listViewTemplates.SelectedItems.Count > 0;

            IJobTemplate jtemp = listViewTemplates.GetSelectedJobTemplate;
            if (jtemp != null)
            {
                labelWarning.Text = (jtemp.NumberofInputAssets == _numberselectedassets) ? string.Empty : "Warning: the number of selected assets is different from the number expected by the template.";
            }
        }

        private void ProcessFromJobTemplate_Load(object sender, EventArgs e)
        {
            listViewTemplates.LoadTemplates(_context);
        }



        private void label1_Click(object sender, EventArgs e)
        {

        }



        private void buttonDeleteTemplate_Click(object sender, EventArgs e)
        {

            listViewTemplates.DeleteSelectedTemplate();
        }
    }
}
