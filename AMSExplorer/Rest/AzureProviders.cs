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


using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AMSExplorer.Rest
{
    /// <summary>
    /// This class is used for the REST call on Azure provides. AMSE uses this to know the Azure locations which have availability zones for AMS account creation.
    /// </summary>
    public class AzureProviders
    {
        private string _managementUrl;

        public AzureProviders(Uri managementUrl)
        {
            _managementUrl = managementUrl.ToString() + "subscriptions/{0}/providers/{1}?api-version=2021-04-01";
        }

        private string GenerateApiUrl(string subscriptionId, string resourceProviderNamespace)
        {
            return string.Format(_managementUrl, subscriptionId, resourceProviderNamespace);
        }



        public async Task<AzureProvidersRestObject> GetProvidersAsync(string subscriptionId, string resourceProviderNamespace, string token)
        {
            string URL = GenerateApiUrl(subscriptionId, resourceProviderNamespace);
            string responseContent = await GetObjectContentAsync(URL, token);
            return AzureProvidersRestObject.FromJson(responseContent);
        }


        private HttpClient GetHttpClient(string token)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            return client;
        }

        private async Task<string> GetObjectContentAsync(string url, string token)
        {
            HttpClient client = GetHttpClient(token);

            HttpResponseMessage amsRequestResult = await client.GetAsync(url).ConfigureAwait(false);

            string responseContent = await amsRequestResult.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (!amsRequestResult.IsSuccessStatusCode)
            {
                dynamic error = JsonConvert.DeserializeObject(responseContent);
                throw new Exception((string)error?.error?.message);
            }
            return responseContent;
        }
    }
}