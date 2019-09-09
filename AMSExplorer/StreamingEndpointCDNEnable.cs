﻿//----------------------------------------------------------------------------------------------
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
using System.Collections.Generic;
using System.Windows.Forms;


namespace AMSExplorer
{


    public partial class StreamingEndpointCDNEnable : Form
    {

        public static readonly List<Item> CDNProviders = new List<Item> {
            new Item("Standard Verizon",  "StandardVerizon"),
            new Item("Standard Akamai", "StandardAkamai"),
            new Item("Premium Verizon", "PremiumVerizon")
                   };

        public string ProviderSelected => ((Item)comboBoxProvider.SelectedItem).Value.ToString();

        public string ProviderSelectedString => ((Item)comboBoxProvider.SelectedItem).Value;


        public string Profile
        {
            get
            {
                if (string.IsNullOrWhiteSpace(textBoxProfile.Text))
                {
                    return null;
                }
                else
                {
                    return textBoxProfile.Text;
                }
            }
        }



        public StreamingEndpointCDNEnable()
        {
            InitializeComponent();
            Icon = Bitmaps.Azure_Explorer_ico;

            ControlsResetToDefault();


        }

        private void ControlsResetToDefault()
        {
            comboBoxProvider.Items.Clear();

            foreach (Item provider in CDNProviders)
            {
                comboBoxProvider.Items.Add(provider);
            }

            comboBoxProvider.SelectedIndex = 0;

        }


        private void StreamingEndpointCDNEnable_Load(object sender, EventArgs e)
        {
            DpiUtils.InitPerMonitorDpi(this);
        }

        private void StreamingEndpointCDNEnable_DpiChanged(object sender, DpiChangedEventArgs e)
        {
            DpiUtils.UpdatedSizeFontAfterDPIChange(labelCDNOptions, e);
        }
    }

}
