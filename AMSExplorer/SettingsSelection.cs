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
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

namespace AMSExplorer
{
    public partial class SettingsSelection : Form
    {
        private readonly object _modifications;

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
            Icon = Bitmaps.Azure_Explorer_ico;
            label5.Text = string.Format(label5.Text, itemName);

            _modifications = modifications;

            Dictionary<string, bool> dico = new();

            IEnumerable<PropertyInfo> props = modifications.GetType().GetProperties();
            foreach (PropertyInfo info in props)
            {
                ListViewItem lvitem = new(info.Name);
                if ((bool)info.GetValue(modifications))
                {
                    lvitem.Checked = true;
                }
                listViewSettings.Items.Add(lvitem);
            }
        }

        private void SettingsSelection_Load(object sender, EventArgs e)
        {
            // DpiUtils.InitPerMonitorDpi(this);
        }



        private void buttonOk_Click(object sender, EventArgs e)
        {

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {

        }

        private void SettingsSelection_DpiChanged(object sender, DpiChangedEventArgs e)
        {
            // DpiUtils.UpdatedSizeFontAfterDPIChange(label5, e);
        }
    }
}
