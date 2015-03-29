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
        private IJobTemplate _jobtemplateselected = null;
        private IAsset _workflowselected = null;

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
        public IJobTemplate WatchRunJobTemplate
        {
            get
            {
                return checkBoxRunJobTemplate.Checked ? listViewTemplates.GetSelectedJobTemplate : null;
            }

        }
        public IAsset WatchRunWorkflow
        {
            get
            {
                return checkBoxInsertWorkflowAsFirstAsset.Checked ? listViewWorkflows1.GetSelectedWorkflow.FirstOrDefault() : null;
            }

        }

        public string WatchSendEMail
        {
            get
            {
                return checkBoxSendEMail.Checked ? textBoxEMail.Text : null;
            }
            set
            {
                checkBoxSendEMail.Checked = value != null;
                textBoxEMail.Text = value;
            }
        }

        public bool WatchPublishOutputAssets
        {
            get
            {
                return checkBoxPublishOAssets.Checked;
            }
            set
            {
                checkBoxPublishOAssets.Checked = value;
            }
        }

        public WatchFolder(CloudMediaContext context, IJobTemplate jobtemplate, IAsset workflow)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            _context = context;
            _jobtemplateselected = jobtemplate;
            _workflowselected = workflow;
            checkBoxRunJobTemplate.Checked = (jobtemplate != null);
        }

        private void checkBoxParallel_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void WatchFolder_Load(object sender, EventArgs e)
        {
            buttonOk.Enabled = string.IsNullOrWhiteSpace(textBoxFolder.Text) ? false : true;
            checkBoxPublishOAssets.Text = string.Format(checkBoxPublishOAssets.Text, Properties.Settings.Default.DefaultLocatorDurationDays);
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
                listViewTemplates.LoadTemplates(_context, _jobtemplateselected);
            }
            else
            {
                listViewTemplates.Items.Clear();
                listViewTemplates.Enabled = false;
            }
        }

        private void buttonTestEmail_Click(object sender, EventArgs e)
        {
            Program.CreateAndSendOutlookMail(textBoxEMail.Text, "Explorer Watchfolder: Test Message", "test message body");
        }

        private void checkBoxSendEMail_CheckedChanged(object sender, EventArgs e)
        {
            textBoxEMail.Enabled = buttonTestEmail.Enabled = checkBoxSendEMail.Checked;
        }

        private void checkBoxInsertWorkflowAsFirstAsset_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxRunJobTemplate.Checked)
            {
                listViewWorkflows1.Enabled = true;
                listViewWorkflows1.LoadWorkflows(_context, _workflowselected);
            }
            else
            {
                listViewWorkflows1.Items.Clear();
                listViewWorkflows1.Enabled = false;
            }
        }

        private void listViewTemplates_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewTemplates.GetSelectedJobTemplate.NumberofInputAssets == 2)
            {
                MessageBox.Show("You selected a job template that requires two input assets. If this is for a Premium encoder task, please select a workflow file.");
                checkBoxInsertWorkflowAsFirstAsset.Checked = true;
            }
            else
            {
                checkBoxInsertWorkflowAsFirstAsset.Checked = false;
                if (listViewTemplates.GetSelectedJobTemplate.NumberofInputAssets < 2)
                {
                    MessageBox.Show("You selected a job template that requires more than two input assets. This is not supported for this feature.");
                }
            }
        }
    }
}
