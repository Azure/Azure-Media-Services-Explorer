//----------------------------------------------------------------------------------------------
//    Copyright 2019 Microsoft Corporation
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
        string savedConfig;
        string defaultConfig;

        public string TextData
        {
            get
            {
                return textBoxConfiguration.Text;
            }
        }

        public EditorXMLJSON(string title = null, string text = null, bool editMode = false, bool showSamplePremium = false, bool DisplayFormatButton = true, string infoText = null)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            if (title != null) this.Text = title;
            textBoxConfiguration.Text = savedConfig = defaultConfig = (text == null ? string.Empty : text);
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

            buttonInsertSample.Visible = showSamplePremium;
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
            DialogResult DR = this.ShowDialog();
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
            XDocument doc = XDocument.Load(Path.Combine(Application.StartupPath + Constants.PathConfigFiles, "SampleMPWESetRunTime.xml"));
            textBoxConfiguration.Text = doc.Declaration.ToString() + Environment.NewLine + doc.ToString();
        }

        private void buttonCopyClipboard_Click(object sender, EventArgs e)
        {
            System.Threading.Thread MyThread = new Thread(new ParameterizedThreadStart(DoCopyClipboard));
            MyThread.SetApartmentState(ApartmentState.STA);
            MyThread.IsBackground = true;
            MyThread.Start(textBoxConfiguration.Text);
        }

        public void DoCopyClipboard(object text)
        {
            Clipboard.SetText((string)text);
        }

        private void buttonFormat_Click(object sender, EventArgs e)
        {
            textBoxConfiguration.Text = Program.AnalyzeAndIndentXMLJSON(textBoxConfiguration.Text);
        }
    }

    class ButtonPremiumXMLData : Button
    {
        EditorXMLJSON myPremiumXML;

        public ButtonPremiumXMLData()
        {
            this.Click += ButtonXML_Click;
        }

        public void Initialize()
        {
            myPremiumXML = new EditorXMLJSON(editMode: true, showSamplePremium: true);
        }

        void ButtonXML_Click(object sender, EventArgs e)
        {
            myPremiumXML.Display();
        }

        public string GetXML()
        {
            return myPremiumXML.TextData;
        }
    }
}
