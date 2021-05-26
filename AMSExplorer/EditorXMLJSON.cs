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

using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;

namespace AMSExplorer
{
    public partial class EditorXMLJSON : Form
    {
        private string savedConfig;
        private readonly ShowSampleMode _showSample;

        public string TextData => textBoxConfiguration.Text;

        public EditorXMLJSON(string title = null, string text = null, bool editMode = false, ShowSampleMode showSample = ShowSampleMode.None, bool DisplayFormatButton = true, string infoText = null)
        {
            InitializeComponent();
            Icon = Bitmaps.Azure_Explorer_ico;
            if (title != null)
            {
                Text = title;
            }

            textBoxConfiguration.Text = savedConfig = text ?? string.Empty;
            if (editMode)
            {
                buttonOk.Text = AMSExplorer.Properties.Resources.EditorXMLJSON_EditorXMLJSON_Save;
            }
            else // readonly mode
            {
                buttonCancel.Text = AMSExplorer.Properties.Resources.EditorXMLJSON_EditorXMLJSON_Close;
                buttonOk.Visible = false;
                textBoxConfiguration.ReadOnly = true;
            }

            buttonInsertSample.Visible = showSample != ShowSampleMode.None;
            _showSample = showSample;
            labelWarningJSON.Text = string.Empty;
            buttonFormat.Visible = DisplayFormatButton;

            if (infoText != null)
            {
                labelInfoText.Text = infoText;
                labelInfoText.Visible = true;
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
            if (_showSample == ShowSampleMode.Premium)
            {
                XDocument doc = XDocument.Load(Path.Combine(Application.StartupPath, Constants.PathConfigFiles, "SampleMPWESetRunTime.xml"));
                textBoxConfiguration.Text = doc.Declaration.ToString() + Environment.NewLine + doc.ToString();
            }
            else if (_showSample == ShowSampleMode.JsonPreset)
            {
                textBoxConfiguration.Text = File.ReadAllText(Path.Combine(Application.StartupPath, Constants.PathConfigFiles, "Preset.json"));
            }
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
            Clipboard.SetText((string)text);
        }

        private void buttonFormat_Click(object sender, EventArgs e)
        {
            textBoxConfiguration.Text = Program.AnalyzeAndIndentXMLJSON(textBoxConfiguration.Text);
        }

        private void EditorXMLJSON_Load(object sender, EventArgs e)
        {
            // DpiUtils.InitPerMonitorDpi(this);
        }

        private void EditorXMLJSON_DpiChanged(object sender, DpiChangedEventArgs e)
        {
            // DpiUtils.UpdatedSizeFontAfterDPIChange(textBoxConfiguration, e);
        }
    }

    internal class ButtonPremiumXMLData : Button
    {
        private EditorXMLJSON myPremiumXML;

        public ButtonPremiumXMLData()
        {
            Click += ButtonXML_Click;
        }

        public void Initialize()
        {
            myPremiumXML = new EditorXMLJSON(editMode: true, showSample: ShowSampleMode.Premium);
        }

        private void ButtonXML_Click(object sender, EventArgs e)
        {
            myPremiumXML.Display();
        }

        public string GetXML()
        {
            return myPremiumXML.TextData;
        }
    }

    public enum ShowSampleMode
    {
        None = 0,
        Premium,
        JsonPreset
    }
}
