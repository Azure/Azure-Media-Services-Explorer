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


using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AMSExplorer.Rest
{
    public partial class AmsClientRest
    {
        private readonly AMSClientV3 _amsClient;

        public AmsClientRest(AMSClientV3 amsClient)
        {
            _amsClient = amsClient;
        }

        private string GenerateApiUrl(string url, string objectName)
        {
            return _amsClient.environment.ArmEndpoint
                                       + string.Format(url,
                                                          _amsClient.credentialsEntry.AzureSubscriptionId,
                                                          _amsClient.credentialsEntry.ResourceGroup,
                                                          _amsClient.credentialsEntry.AccountName,
                                                          objectName
                                                  );
        }

        private string GetToken()
        {
            return _amsClient.authResult != null ? _amsClient.authResult.AccessToken :
                 TokenCache.DefaultShared.ReadItems()
                     .Where(t => t.ClientId == _amsClient.credentialsEntry.ADSPClientId)
                     .OrderByDescending(t => t.ExpiresOn)
                     .First().AccessToken;
        }

        private HttpClient GetHttpClient()
        {
            HttpClient client = _amsClient.AMSclient.HttpClient;
            client.DefaultRequestHeaders.Remove("Authorization");
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + GetToken());
            return client;
        }

        private async Task<string> GetObjectContentAsync(string url)
        {
            HttpClient client = GetHttpClient();

            HttpResponseMessage amsRequestResult = await client.GetAsync(url).ConfigureAwait(false);

            string responseContent = await amsRequestResult.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (!amsRequestResult.IsSuccessStatusCode)
            {
                dynamic error = JsonConvert.DeserializeObject(responseContent);
                throw new Exception((string)error?.error?.message);
            }
            return responseContent;
        }


        private async Task<string> CreateObjectAsync(string url, string amsJSONObject)
        {
            HttpClient client = GetHttpClient();

            string _requestContent = amsJSONObject;
            StringContent httpContent = new(_requestContent, System.Text.Encoding.UTF8);
            httpContent.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/json; charset=utf-8");

            HttpResponseMessage amsRequestResult = await client.PutAsync(url, httpContent).ConfigureAwait(false);
            string responseContent = await amsRequestResult.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (!amsRequestResult.IsSuccessStatusCode)
            {
                dynamic error = JsonConvert.DeserializeObject(responseContent);
                throw new Exception((string)error?.error?.message);
            }

            if (amsRequestResult.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                // let's wait for the operation to complete
                var monitorUrl = amsRequestResult.Headers.Where(h => h.Key == "Azure-AsyncOperation").FirstOrDefault().Value.FirstOrDefault();
                int monitorDelay = 1000 * int.Parse(amsRequestResult.Headers.Where(h => h.Key == "Retry-After").FirstOrDefault().Value.FirstOrDefault());
                bool notComplete = true;
                do
                {
                    await Task.Delay(monitorDelay);
                    HttpResponseMessage amsRequestResultWait = await client.GetAsync(monitorUrl).ConfigureAwait(false);
                    string responseContentWait = await amsRequestResultWait.Content.ReadAsStringAsync().ConfigureAwait(false);
                    dynamic data = JsonConvert.DeserializeObject(responseContentWait);
                    notComplete = data.status == "InProgress";
                }
                while (notComplete);
            }
            return responseContent;
        }
    }
}