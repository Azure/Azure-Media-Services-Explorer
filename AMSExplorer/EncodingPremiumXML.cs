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
using System.Diagnostics;
using Microsoft.WindowsAzure.MediaServices.Client;
using Newtonsoft.Json.Linq;
using System.Xml.Linq;
using System.IO;

namespace AMSExplorer
{
    public partial class EncodingPremiumXML : Form
    {
        string savedConfig;
        string defaultConfig;

        public string PremiumXML
        {
            get
            {
                return textBoxConfiguration.Text;
            }
        }

        public EncodingPremiumXML(string title = null, string text = null, bool editMode=false)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            if (title != null) this.Text = title;
            textBoxConfiguration.Text = savedConfig = defaultConfig = (text == null ? string.Empty : text);
            if (editMode) buttonOk.Text = "Save";
        }

        private void JobOptions_Load(object sender, EventArgs e)
        {

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
            bool Error = false;
            var type = Program.AnalyseConfigurationString(textBoxConfiguration.Text);
            if (type == TypeConfig.JSON)
            {
                // Let's check JSON syntax

                try
                {
                    var jo = JObject.Parse(textBoxConfiguration.Text);
                }
                catch (Exception ex)
                {
                    labelWarningJSON.Text = string.Format((string)labelWarningJSON.Tag, ex.Message);
                    Error = true;
                }
            }
            else if (type == TypeConfig.XML) // XML 
            {
                try
                {
                    var xml = XElement.Load(new StringReader(textBoxConfiguration.Text));
                }
                catch (Exception ex)
                {
                    labelWarningJSON.Text = string.Format("Error in XML data: {0}", ex.Message);
                    Error = true;
                }
            }

            labelWarningJSON.Visible = Error;
        }
    }

    class ButtonPremiumXMLData : Button
    {
        EncodingPremiumXML myPremiumXML;

        public ButtonPremiumXMLData()
        {
            this.Click += ButtonXML_Click;
        }

        public void Initialize()
        {
            myPremiumXML = new EncodingPremiumXML();
        }

        void ButtonXML_Click(object sender, EventArgs e)
        {
            myPremiumXML.Display();
        }

        public string GetXML()
        {
            return myPremiumXML.PremiumXML;
        }
    }
}
