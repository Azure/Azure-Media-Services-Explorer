//----------------------------------------------------------------------------------------------
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


using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AMSExplorer.MKIO.Models;

namespace AMSExplorer.MKIO
{
    /// <summary>
    /// REST Client for MKIO
    /// https://io.mediakind.com
    /// 
    /// </summary>
    public partial class MKIOClientRest
    {
        //
        // assets
        //
        private const string assetsApiUrl = "api/ams/{0}/assets";
        private const string assetApiUrl = assetsApiUrl + "/{1}";

        public List<MKIOAsset> ListAssets()
        {
            Task<List<MKIOAsset>> task = Task.Run<List<MKIOAsset>>(async () => await ListAssetsAsync());
            return task.GetAwaiter().GetResult();
        }

        public async Task<List<MKIOAsset>> ListAssetsAsync()
        {
            string URL = GenerateApiUrl(assetsApiUrl);
            string responseContent = await GetObjectContentAsync(URL);
            return MKIOListAssets.FromJson(responseContent).Value;
        }

        public MKIOAsset GetAsset(string assetName)
        {
            Task<MKIOAsset> task = Task.Run<MKIOAsset>(async () => await GetAssetAsync(assetName));
            return task.GetAwaiter().GetResult();
        }

        public async Task<MKIOAsset> GetAssetAsync(string assetName)
        {
            string URL = GenerateApiUrl(assetApiUrl, assetName);
            string responseContent = await GetObjectContentAsync(URL);
            return MKIOAsset.FromJson(responseContent);
        }

        public MKIOAsset CreateOrUpdateAsset(string assetName, MKIOAsset content)
        {
            Task<MKIOAsset> task = Task.Run<MKIOAsset>(async () => await CreateOrUpdateAssetAsync(assetName, content));
            return task.GetAwaiter().GetResult();
        }

        public async Task<MKIOAsset> CreateOrUpdateAssetAsync(string assetName, MKIOAsset content)
        {
            string URL = GenerateApiUrl(assetApiUrl, assetName);
            string responseContent = await CreateObjectAsync(URL, content.ToJson());
            return MKIOAsset.FromJson(responseContent);
        }

        public void DeleteAsset(string assetName)
        {
            Task.Run(async () => await DeleteAssetAsync(assetName));
        }

        public async Task DeleteAssetAsync(string assetName)
        {
            string URL = GenerateApiUrl(assetApiUrl, assetName);
            await ObjectContentAsync(URL, HttpMethod.Delete);
        }
    }
}