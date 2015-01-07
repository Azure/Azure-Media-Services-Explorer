//----------------------------------------------------------------------- 
// <copyright file="WatchFolder.cs" company="Microsoft">Copyright (c) Microsoft Corporation. All rights reserved.</copyright> 
// <license>
// Azure Media Services Explorer Ver. 3.1
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
using Microsoft.WindowsAzure.MediaServices.Client;

namespace AMSExplorer
{
    public partial class WatchFolder : Form
    {
        private CloudMediaContext _context;

        public string WatchFolderPath
        {
            get
            {
                return textBoxFolder.Text;
            }
            set
            {
                textBoxFolder.Text = value;
            }
        }
        public bool WatchOn
        {
            get
            {
                return radioButtonON.Checked;
            }
            set
            {
                radioButtonON.Checked = value;
            }
        }
        public bool WatchUseQueue
        {
            get
            {
                return checkBoxUseQueue.Checked;
            }
            set
            {
                checkBoxUseQueue.Checked = value;
            }
        }
        public bool WatchDeleteFile
        {
            get
            {
                return checkBoxDeleteFile.Checked;
            }
            set
            {
                checkBoxDeleteFile.Checked = value;
            }
        }
        public bool WatchRunJobTemplate
        {
            get
            {
                return checkBoxRunJobTemplate.Checked;
            }
        }
        public IJobTemplate WatchSelectedJobTemplate
        {
            get
            {
                return listViewTemplates.GetSelectedJobTemplate;
            }
        }

        public WatchFolder(CloudMediaContext context)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            _context = context;
        }

        private void checkBoxParallel_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void WatchFolder_Load(object sender, EventArgs e)
        {
            buttonOk.Enabled = string.IsNullOrWhiteSpace(textBoxFolder.Text) ? false : true;
        }

        private void buttonSelFolder_Click(object sender, EventArgs e)
        {

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBoxFolder.Text = folderBrowserDialog1.SelectedPath;

            }
        }

        private void WatchFolder_Shown(object sender, EventArgs e)
        {

        }

        private void textBoxFolder_TextChanged(object sender, EventArgs e)
        {
            buttonOk.Enabled = string.IsNullOrWhiteSpace(textBoxFolder.Text) ? false : true;
        }

        private void checkBoxRunJobTemplate_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxRunJobTemplate.Checked)
            {
                listViewTemplates.Enabled = true;
                listViewTemplates.LoadTemplates(_context);
            }
            else
            {
                listViewTemplates.Items.Clear();
                listViewTemplates.Enabled = false;
            }
        }
    }
}
