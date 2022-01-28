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
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace AMSExplorer
{
    public partial class EditorPresetJSON : Form
    {
        private string savedConfig;
        private readonly string _existingTransformName;

        public string TextData => textBoxConfiguration.Text;

        public string TransformName => textBoxTransformName.Text;

        public string TransformDescription => string.IsNullOrWhiteSpace(textBoxDescription.Text) ? null : textBoxDescription.Text;


        public EditorPresetJSON(string existingTransformName = null, string existingTransformDescription = null)
        {
            InitializeComponent();
            Icon = Bitmaps.Azure_Explorer_ico;
            buttonOk.Text = AMSExplorer.Properties.Resources.EditorXMLJSON_EditorXMLJSON_Save;
            buttonInsertSample.Visible = true;
            labelWarningJSON.Text = string.Empty;
            buttonFormat.Visible = true;

            if (existingTransformName != null)
            {
                textBoxTransformName.Text = existingTransformName;
                textBoxTransformName.Enabled = false;
            }
            if (existingTransformDescription != null)
            {
                textBoxDescription.Text = existingTransformDescription;
                //textBoxDescription.Enabled = false;
            }
            _existingTransformName = existingTransformName;

        }

        private void UpdateTransformLabel()
        {
            if (_existingTransformName != null)
            {
                textBoxTransformName.Text = _existingTransformName;
                textBoxTransformName.Enabled = false;
            }
            else
            {
                textBoxTransformName.Text = "CustomPreset";
            }
        }

        public DialogResult Display()
        {
            DialogResult DR = ShowDialog();
            if (DR == DialogResult.OK)
            {
                savedConfig = textBoxConfiguration.Text;
            }
            else // let's reset the controls to default
            {
                textBoxConfiguration.Text = savedConfig;
            }
            return DR;
        }

        private void textBoxConfiguration_TextChanged(object sender, EventArgs e)
        {
            // let's normalize the line breaks
            textBoxConfiguration.Text = textBoxConfiguration.Text.Replace("\r\n", "\n").Replace("\r", "\n").Replace("\n", Environment.NewLine);

            labelWarningJSON.Text = Program.AnalyzeTextAndReportSyntaxError(textBoxConfiguration.Text);
        }



        private void buttonInsertSample_Click(object sender, EventArgs e)
        {
            textBoxConfiguration.Text = File.ReadAllText(Path.Combine(Application.StartupPath, Constants.PathConfigFiles, "Preset.json"));
        }

        private void buttonCopyClipboard_Click(object sender, EventArgs e)
        {
            System.Threading.Thread MyThread = new(new ParameterizedThreadStart(DoCopyClipboard));
            MyThread.SetApartmentState(ApartmentState.STA);
            MyThread.IsBackground = true;
            MyThread.Start(textBoxConfiguration.Text);
        }

        public static void DoCopyClipboard(object text)
        {
            if (text != null)
            {
                string textS = (string)text;
                if (string.IsNullOrEmpty(textS))
                {
                    Clipboard.Clear();
                }
                else
                {
                    Clipboard.SetText(textS);
                }
            }
        }

        private void buttonFormat_Click(object sender, EventArgs e)
        {
            textBoxConfiguration.Text = Program.AnalyzeAndIndentXMLJSON(textBoxConfiguration.Text);
        }

        private void EditorPresetJSON_Load(object sender, EventArgs e)
        {
            // DpiUtils.InitPerMonitorDpi(this);
            UpdateTransformLabel();
        }

        private void EditorPresetJSON_DpiChanged(object sender, DpiChangedEventArgs e)
        {
            // DpiUtils.UpdatedSizeFontAfterDPIChange(textBoxConfiguration, e);
        }
    }
}
