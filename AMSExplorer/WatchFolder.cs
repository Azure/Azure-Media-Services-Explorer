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
using Microsoft.WindowsAzure.MediaServices.Client;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace AMSExplorer
{
    public partial class WatchFolder : Form
    {
        private CloudMediaContext _context;
        private IEnumerable<IAsset> _SelectedAssets;
        private WatchFolderSettings _WatchFolderSettings;

        public WatchFolderSettings WatchFolderGetSettings
        {
            get
            {
                WatchFolderSettings settings = new WatchFolderSettings()
                {
                    FolderPath = textBoxFolder.Text,
                    IsOn = radioButtonON.Checked,
                    DeleteFile = checkBoxDeleteFile.Checked,
                    JobTemplate = checkBoxRunJobTemplate.Checked ? listViewTemplates.GetSelectedJobTemplate : null,
                    SendEmailToRecipient = checkBoxSendEMail.Checked ? textBoxEMail.Text : null,
                    PublishOutputAssets = checkBoxPublishOAssets.Checked,
                };



                if (checkBoAddAssetsToInput.Checked)
                {
                    settings.ExtraInputAssets = new List<IAsset>();
                    if (radioButtonInsertWorkflowAsset.Checked)
                    {
                        settings.ExtraInputAssets.Add(listViewWorkflows1.GetSelectedWorkflow.FirstOrDefault());
                    }
                    else // selected assets
                    {
                        settings.ExtraInputAssets.AddRange(_SelectedAssets);
                    }
                }
                else
                {
                    settings.ExtraInputAssets = null;
                }


                if (checkBoAddAssetsToInput.Checked)
                {
                    settings.TypeInputExtraInput = (radioButtonInsertSelectedAssets.Checked) ? TypeInputExtraInput.SelectedAssets : TypeInputExtraInput.SelectedWorkflow;
                }
                else
                {
                    settings.TypeInputExtraInput = TypeInputExtraInput.None;
                }

                return settings;
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


        public WatchFolder(CloudMediaContext context, IEnumerable<IAsset> selectedassets, WatchFolderSettings watchfoldersettings)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            _context = context;
            _WatchFolderSettings = watchfoldersettings;
            _SelectedAssets = selectedassets;
           
        }

        private void checkBoxParallel_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void WatchFolder_Load(object sender, EventArgs e)
        {
        
            // folder
            textBoxFolder.Text = _WatchFolderSettings.FolderPath;
   
            // activation
            radioButtonON.Checked = _WatchFolderSettings.IsOn;

            // delete file
            checkBoxDeleteFile.Checked = _WatchFolderSettings.DeleteFile;

            // process asset
            checkBoxRunJobTemplate.Checked = (_WatchFolderSettings.JobTemplate != null);
         
            // add asset(s) to process
            if (_WatchFolderSettings.TypeInputExtraInput != TypeInputExtraInput.None)
            {
                checkBoAddAssetsToInput.Checked = true;
                if (_WatchFolderSettings.TypeInputExtraInput == TypeInputExtraInput.SelectedAssets)
                {
                    radioButtonInsertSelectedAssets.Checked = true;
                }
                else
                {
                    radioButtonInsertWorkflowAsset.Checked = true;
                }
            }

            // publish
            checkBoxPublishOAssets.Checked = _WatchFolderSettings.PublishOutputAssets;
            checkBoxPublishOAssets.Text = string.Format(checkBoxPublishOAssets.Text, Properties.Settings.Default.DefaultLocatorDurationDaysNew);
        
            // send email
            checkBoxSendEMail.Checked = _WatchFolderSettings.SendEmailToRecipient != null;
            textBoxEMail.Text = _WatchFolderSettings.SendEmailToRecipient;

            // other
            buttonOk.Enabled = string.IsNullOrWhiteSpace(textBoxFolder.Text) ? false : true;
            labelWarning.Text = string.Empty;
        }

        private void buttonSelFolder_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog openFolderDialog = new CommonOpenFileDialog() { IsFolderPicker = true, InitialDirectory = textBoxFolder.Text };
            if (openFolderDialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                textBoxFolder.Text = openFolderDialog.FileName;
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
                groupBoxProcess.Enabled = checkBoxPublishOAssets.Enabled = true;
                listViewTemplates.LoadTemplates(_context, _WatchFolderSettings.JobTemplate);
            }
            else
            {
                listViewTemplates.Items.Clear();
                groupBoxProcess.Enabled = checkBoxPublishOAssets.Enabled = false;
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



        private void listViewTemplates_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckCompatibilityTemplate();
        }

        private void CheckCompatibilityTemplate()
        {
            int selectedassetcount = _SelectedAssets != null ? _SelectedAssets.Count() : 0;
            int numberofinputassets = checkBoAddAssetsToInput.Checked ? (radioButtonInsertSelectedAssets.Checked ? selectedassetcount + 1 : 2) : 1;
            if (listViewTemplates.GetSelectedJobTemplate != null && listViewTemplates.GetSelectedJobTemplate.NumberofInputAssets != numberofinputassets)
            {
                labelWarning.Text = string.Format("The number of input assets in the template ({0}) is incompatible with the input assets ({1})", listViewTemplates.GetSelectedJobTemplate.NumberofInputAssets, numberofinputassets);
            }
            else
            {
                labelWarning.Text = string.Empty;
            }
        }

        private void radioButtonInsertWorkflowAsset_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonInsertWorkflowAsset.Checked)
            {
                listViewWorkflows1.Enabled = true;
                listViewWorkflows1.LoadWorkflows(_context, _WatchFolderSettings.TypeInputExtraInput == TypeInputExtraInput.SelectedWorkflow ? _WatchFolderSettings.ExtraInputAssets.FirstOrDefault() : null);
            }
            else
            {
                listViewWorkflows1.Items.Clear();
                listViewWorkflows1.Enabled = false;
            }
            CheckCompatibilityTemplate();
        }

        private void checkBoAddAssetsToInput_CheckedChanged(object sender, EventArgs e)
        {
            panelInsertAsset.Enabled = checkBoAddAssetsToInput.Checked;
            CheckCompatibilityTemplate();
        }

        private void radioButtonInsertSelectedAssets_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
