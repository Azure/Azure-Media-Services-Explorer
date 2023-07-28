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


using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AMSExplorer.MKIO
{
    /// <summary>
    /// REST Base Client for MKIO
    /// https://io.mediakind.com
    /// 
    /// </summary>
    public partial class MKIOClientRest
    {
        private string baseUrl = "https://api.io.mediakind.com/";
        private string _MKIOSubscriptionName;
        private string _MKIOtoken;
        private HttpClient _httpClient;

        public MKIOClientRest(string MKIOSubscriptionName, string MKIOtoken)
        {
            _MKIOSubscriptionName = MKIOSubscriptionName;
            _MKIOtoken = MKIOtoken;

            _httpClient = new HttpClient();
            // Request headers
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private string GenerateApiUrl(string urlPath, string objectName)
        {
            return baseUrl + string.Format(urlPath, _MKIOSubscriptionName, objectName);
        }
        private string GenerateApiUrl(string urlPath)
        {
            return baseUrl + string.Format(urlPath, _MKIOSubscriptionName);
        }


        private async Task<string> GetObjectContentAsync(string url)
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(url),
                Method = HttpMethod.Get,
            };
            request.Headers.Add("x-mkio-token", _MKIOtoken);

            HttpResponseMessage amsRequestResult = await _httpClient.SendAsync(request).ConfigureAwait(false);

            string responseContent = await amsRequestResult.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (!amsRequestResult.IsSuccessStatusCode)
            {
                var error = JsonConvert.DeserializeObject<dynamic>(responseContent);
                throw new Exception((string)error.error.detail);
            }
            return responseContent;
        }

        private async Task<string> ObjectContentAsync(string url, HttpMethod httpMethod)
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(url),
                Method = httpMethod,
            };
            request.Headers.Add("x-mkio-token", _MKIOtoken);

            HttpResponseMessage amsRequestResult = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead).ConfigureAwait(false);
            string responseContent = await amsRequestResult.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (!amsRequestResult.IsSuccessStatusCode)
            {
                var error = JsonConvert.DeserializeObject<dynamic>(responseContent);
                throw new Exception((string)error.error.detail);
            }

            return responseContent;
        }


        private async Task<string> CreateObjectAsync(string url, string amsJSONObject)
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(url),
                Method = HttpMethod.Put,
            };
            request.Headers.Add("x-mkio-token", _MKIOtoken);
            request.Content = new StringContent(amsJSONObject, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage amsRequestResult = await _httpClient.SendAsync(request).ConfigureAwait(false);
            string responseContent = await amsRequestResult.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (!amsRequestResult.IsSuccessStatusCode)
            {
                var error = JsonConvert.DeserializeObject<dynamic>(responseContent);
                throw new Exception((string)error.error.detail);
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
                    HttpResponseMessage amsRequestResultWait = await _httpClient.GetAsync(monitorUrl).ConfigureAwait(false);
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