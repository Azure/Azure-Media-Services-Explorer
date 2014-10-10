//----------------------------------------------------------------------- 
// <copyright file="CreateStreamingEndpoint.cs" company="Microsoft">Copyright (c) Microsoft Corporation. All rights reserved.</copyright> 
// <license>
// Azure Media Services Explorer Ver. 3.0
// Licensed under the Apache License, Version 2.0 (the "License"); 
// you may not use this file except in compliance with the License. 
// You may obtain a copy of the License at 
//  
// http://www.apache.org/licenses/LICENSE-2.0 
//  
// Unless required by applicable law or agreed to in writing, software 
// distributed under the License is distributed on an "AS IS" BASIS, 
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
// See the License for the specific language governing permissions and 
// limitations under the License. 
// </license> 

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AMSExplorer
{
    public partial class CreateStreamingEndpoint : Form
    {
        public string OriginName
        {
            get { return textboxoriginname.Text; }
            set { textboxoriginname.Text = value; }
        }
        public string OriginDescription
        {
            get { return textBoxOriginDescription.Text; }
            set { textBoxOriginDescription.Text = value; }
        }

        public int scaleUnits
        {
            get { return Convert.ToInt32(numericUpDownRU.Value); }
            set { numericUpDownRU.Value = value; }
        }

        public CreateStreamingEndpoint()
        {
            InitializeComponent();
        }


        private void CreateOrigin_Load(object sender, EventArgs e)
        {

        }

    }
}
