﻿//----------------------------------------------------------------------------------------------
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
using System.Net;
using System.Reflection;

namespace AMSExplorer
{
    public partial class SettingsSelection : Form
    {
        private object _modifications;
       
        public object SettingsObject // return the modifications object with changed done by user
        {
            get
            {
                object newmodif = _modifications;
                foreach (ListViewItem it in listViewSettings.Items)
                {
                    PropertyInfo propertyInfo = newmodif.GetType().GetProperty(it.Text);
                    propertyInfo.SetValue(newmodif, Convert.ChangeType(it.Checked, propertyInfo.PropertyType), null);
                }
                return newmodif;
            }
        }

        public SettingsSelection(string itemName, object modifications)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            label5.Text = string.Format(label5.Text, itemName);

            _modifications = modifications;

            var dico = new Dictionary<string, bool>();

            IEnumerable<PropertyInfo> props = modifications.GetType().GetProperties();
            foreach (PropertyInfo info in props)
            {
                var lvitem = new ListViewItem(info.Name);
                if ((bool)info.GetValue(modifications))
                {
                    lvitem.Checked = true;
                }
                listViewSettings.Items.Add(lvitem);
            }
        }

        private void SettingsSelection_Load(object sender, EventArgs e)
        {

        }

      

        private void buttonOk_Click(object sender, EventArgs e)
        {

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {

        }
    }
}
