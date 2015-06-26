//----------------------------------------------------------------------------------------------
//    Copyright 2015 Microsoft Corporation
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
using System.Xml;
using System.Xml.Linq;
using System.IO;
using Microsoft.WindowsAzure.MediaServices.Client;
using System.Reflection;

namespace AMSExplorer
{
    public partial class EncodingAMEStandard : Form
    {
        public string EncodingAMEStdPresetXMLFilesUserFolder;
        public string EncodingAMEStdPresetXMLFilesFolder;

        private List<IMediaProcessor> Procs;
        public List<IAsset> SelectedAssets;
        private CloudMediaContext _context;

        private const string defaultprofile = "H264 Multiple Bitrate 720p";
        bool usereditmode = false;

        public string EncodingLabel
        {
            set
            {
                label.Text = value;
            }
        }

        public string EncodingJobName
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


        public List<IMediaProcessor> EncodingProcessorsList
        {
            set
            {
                foreach (IMediaProcessor pr in value)
                {
                    comboBoxProcessor.Items.Add(string.Format("{0} {1} Version {2} ({3})", pr.Vendor, pr.Name, pr.Version, pr.Description));
                }
                if (comboBoxProcessor.Items.Count > 0)
                {
                    comboBoxProcessor.SelectedIndex = 0;
                }
                Procs = value;
            }
        }

        public IMediaProcessor EncodingProcessorSelected
        {
            get
            {
                return Procs[comboBoxProcessor.SelectedIndex];
            }
        }

        public string EncodingOutputAssetName
        {
            get
            {
                return textboxoutputassetname.Text;
            }
            set
            {
                textboxoutputassetname.Text = value;
            }
        }


        public string EncodingConfiguration
        {
            get
            {
                return textBoxConfiguration.Text;
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


        public EncodingAMEStandard(CloudMediaContext context)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            _context = context;
            buttonJobOptions.Initialize(_context);
        }



        private void EncodingAMEStandard_Shown(object sender, EventArgs e)
        {
        }

        private void EncodingAMEStandard_Load(object sender, EventArgs e)
        {
            // presets list
            var filePaths = Directory.GetFiles(EncodingAMEStdPresetXMLFilesFolder, "*.xml").Select(f => Path.GetFileNameWithoutExtension(f));
            listboxPresets.Items.AddRange(filePaths.ToArray());
            listboxPresets.SelectedIndex = listboxPresets.Items.IndexOf(defaultprofile);
        }


        private void buttonLoadXML_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(this.EncodingAMEStdPresetXMLFilesUserFolder))
                openFileDialogPreset.InitialDirectory = this.EncodingAMEStdPresetXMLFilesUserFolder;
            
            if (openFileDialogPreset.ShowDialog() == DialogResult.OK)
            {
                this.EncodingAMEStdPresetXMLFilesUserFolder = Path.GetDirectoryName(openFileDialogPreset.FileName); // let's save the folder
                try
                {
                    StreamReader streamReader = new StreamReader(openFileDialogPreset.FileName);
                    textBoxConfiguration.Text = streamReader.ReadToEnd();
                    streamReader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void buttonSaveXML_Click(object sender, EventArgs e)
        {
            if (saveFileDialogPreset.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    File.WriteAllText(saveFileDialogPreset.FileName, textBoxConfiguration.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not save file to disk. Original error: " + ex.Message);
                }

            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void listboxPresets_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listboxPresets.SelectedItem!=null)
            {
                try
                {
                    string filePath = Path.Combine(EncodingAMEStdPresetXMLFilesFolder, listboxPresets.SelectedItem.ToString() + ".xml");
                    StreamReader streamReader = new StreamReader(filePath);
                    usereditmode = false;
                    textBoxConfiguration.Text = streamReader.ReadToEnd();
                    usereditmode = true;
                    streamReader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                    usereditmode = true;
                }

            }
        }

        private void textBoxConfiguration_TextChanged(object sender, EventArgs e)
        {
           if (usereditmode)  listboxPresets.SelectedIndex = -1;
        }
    }



}
