﻿//----------------------------------------------------------------------------------------------
//    Copyright 2023 Microsoft Corporation
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

using System.Windows.Forms;

namespace AMSExplorer
{
    public partial class AmsLoginServicePrincipal : Form
    {
        public string ClientId
        {
            get => textBoxClientId.Text;
            set => textBoxClientId.Text = value;
        }

        public string ClientSecret
        {
            get => textBoxClientSecret.Text;
            set => textBoxClientSecret.Text = value;
        }
        public AmsLoginServicePrincipal()
        {
            InitializeComponent();
            Icon = Bitmaps.Azure_Explorer_ico;

        }

        private void AmsLoginServicePrincipal_DpiChanged(object sender, DpiChangedEventArgs e)
        {
            // DpiUtils.UpdatedSizeFontAfterDPIChange(label2, e);
        }

        private void AmsLoginServicePrincipal_Load(object sender, System.EventArgs e)
        {
            // DpiUtils.InitPerMonitorDpi(this);
        }

        private void AmsLoginServicePrincipal_Shown(object sender, System.EventArgs e)
        {
            Telemetry.TrackPageView(this.Name);
        }
    }
}
