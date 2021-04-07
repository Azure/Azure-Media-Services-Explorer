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

// Azure Management dependencies
using Microsoft.Azure.Management.Media;
using Microsoft.Azure.Management.Media.Models;
using Microsoft.Rest.Azure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AMSExplorer
{
    public partial class Mainform : Form
    {
        private async Task<List<AccountFilter>> ReturnSelectedAccountFiltersAsync()
        {
            List<AccountFilter> SelectedFilters = new List<AccountFilter>();
            await _amsClient.RefreshTokenIfNeededAsync();

            // account filters
            List<AccountFilter> acctFilters = new List<AccountFilter>();
            IPage<AccountFilter> acctFiltersPage = await _amsClient.AMSclient.AccountFilters.ListAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName);
            while (acctFiltersPage != null)
            {
                acctFilters.AddRange(acctFiltersPage);
                if (acctFiltersPage.NextPageLink != null)
                {
                    acctFiltersPage = await _amsClient.AMSclient.AccountFilters.ListNextAsync(acctFiltersPage.NextPageLink);
                }
                else
                {
                    acctFiltersPage = null;
                }
            }

            foreach (DataGridViewRow Row in dataGridViewFilters.SelectedRows)
            {
                string filtername = Row.Cells[dataGridViewFilters.Columns["Name"].Index].Value.ToString();
                AccountFilter myfilter = acctFilters.Where(f => f.Name == filtername).FirstOrDefault();
                if (myfilter != null)
                {
                    SelectedFilters.Add(myfilter);
                }
            }

            return SelectedFilters;
        }

        private List<string> ReturnSelectedAssetNames()
        {
            List<string> SelectedAssets = new List<string>();
            foreach (DataGridViewRow Row in dataGridViewAssetsV.SelectedRows)
            {
                SelectedAssets.Add(Row.Cells[dataGridViewAssetsV.Columns["Name"].Index].Value.ToString());
            }
            SelectedAssets.Reverse();
            return SelectedAssets;
        }

        private async Task<List<Asset>> ReturnSelectedAssetsAsync()
        {
            List<Asset> SelectedAssets = new List<Asset>();
            await _amsClient.RefreshTokenIfNeededAsync();

            try
            {
                foreach (string assetName in ReturnSelectedAssetNames())
                {
                    Asset asset = await GetAssetAsync(assetName);
                    if (asset != null)
                    {
                        SelectedAssets.Add(asset);
                    }
                }
            }
            catch (Exception ex)
            {
                // connection error ?
                TextBoxLogWriteLine(ex);
            }

            return SelectedAssets;
        }

        private async Task<List<JobExtension>> ReturnSelectedJobsV3Async()
        {
            List<JobExtension> SelectedJobs = new List<JobExtension>();
            foreach (DataGridViewRow Row in dataGridViewJobsV.SelectedRows)
            {
                Job job = await GetJobAsync(Row.Cells["TransformName"].Value.ToString(), Row.Cells["Name"].Value.ToString());
                SelectedJobs.Add(new JobExtension()
                {
                    Job = job,
                    TransformName = Row.Cells["TransformName"].Value.ToString()
                });
            }

            SelectedJobs.Reverse();
            return SelectedJobs;
        }

        private async Task<List<Transform>> ReturnSelectedTransformsAsync()
        {
            List<Transform> SelectedTransforms = new List<Transform>();
            await _amsClient.RefreshTokenIfNeededAsync();

            List<Transform> transforms = new List<Transform>();
            IPage<Transform> transformsPage = await _amsClient.AMSclient.Transforms.ListAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName);
            while (transformsPage != null)
            {
                transforms.AddRange(transformsPage);
                if (transformsPage.NextPageLink != null)
                {
                    transformsPage = await _amsClient.AMSclient.Transforms.ListNextAsync(transformsPage.NextPageLink);
                }
                else
                {
                    transformsPage = null;
                }
            }

            foreach (string transformName in ReturnSelectedTransformNames())
            {
                Transform myTransform = transforms.Where(f => f.Name == transformName).FirstOrDefault();
                if (myTransform != null)
                {
                    SelectedTransforms.Add(myTransform);
                }
            }
            return SelectedTransforms;
        }

        private List<string> ReturnSelectedTransformNames()
        {
            List<string> SelectedTransforms = new List<string>();

            foreach (DataGridViewRow Row in dataGridViewTransformsV.SelectedRows)
            {
                string transformName = Row.Cells[dataGridViewTransformsV.Columns["Name"].Index].Value.ToString();
                SelectedTransforms.Add(transformName);
            }
            return SelectedTransforms;
        }


        private async Task<StorageAccount> ReturnSelectedStorageAsync()
        {
            StorageAccount SelectedStorage = null;
            if (dataGridViewStorage.SelectedRows.Count == 1)
            {
                await _amsClient.RefreshTokenIfNeededAsync();

                DataGridViewRow row = dataGridViewStorage.SelectedRows[0];
                int index = dataGridViewStorage.Columns["Id"].Index;
                string storagename = AMSClientV3.GetStorageName(row.Cells[index].Value.ToString());
                SelectedStorage = (await _amsClient.AMSclient.Mediaservices.GetAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName))
                    .StorageAccounts.Where(s => AMSClientV3.GetStorageName(s.Id) == storagename).FirstOrDefault();
            }

            return SelectedStorage;
        }

        private async Task<List<ContentKeyPolicy>> ReturnSelectedCKPoliciessAsync()
        {
            List<ContentKeyPolicy> SelectedCKPolicies = new List<ContentKeyPolicy>();
            await _amsClient.RefreshTokenIfNeededAsync();

            Microsoft.Rest.Azure.IPage<ContentKeyPolicy> ckPolicies = await _amsClient.AMSclient.ContentKeyPolicies.ListAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName);
            foreach (DataGridViewRow Row in dataGridViewCKPolicies.SelectedRows)
            {
                string ckpolName = Row.Cells[dataGridViewFilters.Columns["Name"].Index].Value.ToString();
                ContentKeyPolicy myPolicy = ckPolicies.Where(f => f.Name == ckpolName).FirstOrDefault();
                if (myPolicy != null)
                {
                    SelectedCKPolicies.Add(myPolicy);
                }
            }

            return SelectedCKPolicies;
        }

        private async Task<List<Asset>> ReturnSelectedAssetsFromLiveOutputsOrAssetsAsync()
        {
            if (tabControlMain.SelectedTab.Text.StartsWith(AMSExplorer.Properties.Resources.TabAssets)) // we are in the asset tab
            {
                return await ReturnSelectedAssetsAsync();
            }
            else if (tabControlMain.SelectedTab.Text.StartsWith(AMSExplorer.Properties.Resources.TabLive)) // we are in the live tab
            {
                await _amsClient.RefreshTokenIfNeededAsync();

                return (await ReturnSelectedLiveOutputsAsync())
                        .Select(p =>
                            Task.Run(() =>
                                        _amsClient.AMSclient.Assets.GetAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, p.AssetName))
                                        .GetAwaiter().GetResult()
                        )
                        .ToList();
            }
            else
            {
                return null;
            }
        }

        private async Task<List<LiveEvent>> ReturnSelectedLiveEventsAsync()
        {
            List<LiveEvent> SelectedLiveEvents = new List<LiveEvent>();
            foreach (DataGridViewRow Row in dataGridViewLiveEventsV.SelectedRows)
            {
                string liveEventName = string.Empty;
                try
                {
                    liveEventName = Row.Cells[dataGridViewLiveEventsV.Columns["Name"].Index].Value.ToString();
                    LiveEvent liveEvent = await GetLiveEventAsync(liveEventName);
                    // sometimes, the live event can be null (if just deleted)
                    if (liveEvent != null)
                    {
                        SelectedLiveEvents.Add(liveEvent);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Error getting the live event : '{0}'.", liveEventName) + Constants.endline + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            SelectedLiveEvents.Reverse();
            return SelectedLiveEvents;
        }


        private async Task<List<StreamingEndpoint>> ReturnSelectedStreamingEndpointsAsync()
        {
            List<StreamingEndpoint> SelectedOrigins = new List<StreamingEndpoint>();

            foreach (DataGridViewRow Row in dataGridViewStreamingEndpointsV.SelectedRows)
            {
                string seName = Row.Cells[dataGridViewStreamingEndpointsV.Columns["Name"].Index].Value.ToString();
                StreamingEndpoint se = await GetStreamingEndpointAsync(seName);
                if (se != null)
                {
                    SelectedOrigins.Add(se);
                }
            }
            SelectedOrigins.Reverse();
            return SelectedOrigins;
        }

        private List<string> ReturnSelectedLiveOutputNames()
        {
            List<string> SelectedLiveOutputs = new List<string>();

            foreach (DataGridViewRow Row in dataGridViewLiveOutputV.SelectedRows)
            {
                string liveOutputName = Row.Cells[dataGridViewLiveOutputV.Columns["Name"].Index].Value.ToString();
                SelectedLiveOutputs.Add(liveOutputName);
            }
            SelectedLiveOutputs.Reverse();
            return SelectedLiveOutputs;
        }

        private async Task<List<LiveOutput>> ReturnSelectedLiveOutputsAsync()
        {
            List<LiveOutput> SelectedLiveOutputs = new List<LiveOutput>();

            foreach (DataGridViewRow Row in dataGridViewLiveOutputV.SelectedRows)
            {
                string liveOutputName = string.Empty;
                try
                {
                    string eventName = Row.Cells[dataGridViewLiveOutputV.Columns["LiveEventName"].Index].Value.ToString();
                    liveOutputName = Row.Cells[dataGridViewLiveOutputV.Columns["Name"].Index].Value.ToString();
                    LiveOutput liveOutput = await GetLiveOutputAsync(eventName, liveOutputName);
                    if (liveOutput != null)
                    {
                        SelectedLiveOutputs.Add(liveOutput);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Error getting the live output : '{0}'.", liveOutputName) + Constants.endline + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            SelectedLiveOutputs.Reverse();
            return SelectedLiveOutputs;
        }
    }
}
