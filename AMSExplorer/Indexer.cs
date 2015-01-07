//----------------------------------------------------------------------- 
// <copyright file="Indexer.cs" company="Microsoft">Copyright (c) Microsoft Corporation. All rights reserved.</copyright> 
// <license>
// Azure Media Services Explorer Ver. 3.1
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
using System.Diagnostics;
using Microsoft.WindowsAzure.MediaServices.Client;


namespace AMSExplorer
{
    public partial class Indexer : Form
    {
        private CloudMediaContext _context;

        public string IndexerInputAssetName
        {
            get
            {
                return labelAssetName.Text;
            }
            set
            {
                labelAssetName.Text = value;
            }
        }
        public string IndexerOutputAssetName
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

        public string StorageSelected
        {
            get
            {
                return ((Item)comboBoxStorage.SelectedItem).Value;
            }
        }

        public string IndexerJobName
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

        public int IndexerJobPriority
        {
            get
            {
                return (int)numericUpDownPriority.Value;
            }
            set
            {
                numericUpDownPriority.Value = value;
            }
        }

        public string IndexerTitle
        {
            get
            {
                return textBoxTitle.Text;
            }
            set
            {
                textBoxTitle.Text = value;
            }
        }
        public string IndexerDescription
        {
            get
            {
                return textBoxDescription.Text;
            }
            set
            {
                textBoxDescription.Text = value;
            }
        }



        public string IndexerProcessorName
        {
            get
            {
                return processorlabel.Text;
            }
            set
            {
                processorlabel.Text = value;
            }
        }

        public Indexer(CloudMediaContext context)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            _context = context;
        }


        private void Indexer_Load(object sender, EventArgs e)
        {
            foreach (var storage in _context.StorageAccounts)
            {
                comboBoxStorage.Items.Add(new Item(string.Format("{0} {1}", storage.Name, storage.IsDefault ? "(default)" : ""), storage.Name));
                if (storage.Name == _context.DefaultStorageAccount.Name) comboBoxStorage.SelectedIndex = comboBoxStorage.Items.Count - 1;
            }
        }
    }
}
