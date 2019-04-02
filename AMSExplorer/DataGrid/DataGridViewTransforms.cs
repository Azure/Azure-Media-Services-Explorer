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

using Microsoft.Azure.Management.Media;
using Microsoft.Azure.Management.Media.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AMSExplorer
{
    public class DataGridViewTransforms : DataGridView
    {
        private bool _initialized = false;

        private List<string> idsList = new List<string>();
        static AMSClientV3 _client;

        static BindingList<TransformEntryV3> _MyObservTransformsV3;

        public void Init(AMSClientV3 client)
        {
            _client = client;

            var transforms = _client.AMSclient.Transforms.List(_client.credentialsEntry.ResourceGroup, _client.credentialsEntry.AccountName).Select(a => new TransformEntryV3
            {
                Name = a.Name,
                Description = a.Description,
                LastModified = ((DateTime)a.LastModified).ToLocalTime().ToString("G")
            }
            );

            BindingList<TransformEntryV3> MyObservTransformthisPageV3 = new BindingList<TransformEntryV3>(transforms.ToList());
            this.DataSource = MyObservTransformthisPageV3;

            var myTask = Task.Factory.StartNew(() =>
            {
                this.BeginInvoke(new Action(() =>
                {
                    this.Columns["Name"].Width = 200;
                    this.Columns["Description"].Width = 150;
                    this.Columns["LastModified"].Width = 130;
                }));
            });

            _initialized = true;
        }


        public async Task RefreshTransformsAsync() // all transforms are refreshed
        {
            if (!_initialized) return;

            Debug.WriteLine("Refresh Transforms Start");

            this.BeginInvoke(new Action(() => this.FindForm().Cursor = Cursors.WaitCursor));

            _client.RefreshTokenIfNeeded();

            var transforms = (await _client.AMSclient.Transforms.ListAsync(_client.credentialsEntry.ResourceGroup, _client.credentialsEntry.AccountName)).Select(a => new TransformEntryV3
            {
                Name = a.Name,
                Description = a.Description,
                LastModified = ((DateTime)a.LastModified).ToLocalTime().ToString("G")
            }
          );

            _MyObservTransformsV3 = new BindingList<TransformEntryV3>(transforms.ToList());

            this.BeginInvoke(new Action(() => this.DataSource = _MyObservTransformsV3));

            Debug.WriteLine("RefreshTransforms End");

            this.BeginInvoke(new Action(() => this.FindForm().Cursor = Cursors.Default));
        }

        public List<Transform> ReturnSelectedTransforms()
        {
            _client.RefreshTokenIfNeeded();

            List<Transform> SelectedTransforms = new List<Transform>();
            foreach (DataGridViewRow Row in this.SelectedRows)
            {
                // sometimes, the transform can be null (if just deleted)
                var transform = _client.AMSclient.Transforms.Get(_client.credentialsEntry.ResourceGroup, _client.credentialsEntry.AccountName, Row.Cells[this.Columns["Name"].Index].Value.ToString());
                if (transform != null)
                {
                    SelectedTransforms.Add(transform);
                }
            }
            SelectedTransforms.Reverse();
            return SelectedTransforms;
        }
    }
}