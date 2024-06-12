//----------------------------------------------------------------------------------------------
//    Copyright 2023 Microsoft Corporation
//https://learn.microsoft.com/en-us
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
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AMSExplorer.Rest
{
    /// <summary>
    /// Rest call for live transcription preview
    /// https://learn.microsoft.com/en-us/azure/media-services/latest/live-event-live-transcription-how-to
    /// 
    /// </summary>
    public partial class AmsClientRest
    {

        //
        // live transcript part
        //

        private const string liveEventApiUrl = "subscriptions/{0}/resourceGroups/{1}/providers/Microsoft.Media/mediaservices/{2}/liveEvents/{3}?api-version=2021-11-01";

        public async Task<string> CreateLiveEventAsync(LiveEventRestObject liveEventSettings, bool startLiveEventNow)
        {
            string URL = GenerateApiUrl(liveEventApiUrl, liveEventSettings.Name) + string.Format("&autoStart={0}", startLiveEventNow.ToString());
            string responseContent = await CreateObjectAsync(URL, liveEventSettings.ToJson());
            return responseContent;
        }


        public LiveEventRestObject GetLiveEvent(string liveEventName)
        {
            Task<LiveEventRestObject> task = Task.Run<LiveEventRestObject>(async () => await GetLiveEventAsync(liveEventName));
            return task.Result;

            // return GetLiveEventAsync(liveEventName).GetAwaiter().GetResult();
        }


        public async Task<LiveEventRestObject> GetLiveEventAsync(string liveEventName)
        {
            string URL = GenerateApiUrl(liveEventApiUrl, liveEventName);
            string responseContent = await GetObjectContentAsync(URL);
            return LiveEventRestObject.FromJson(responseContent);
        }

        //
        // Transform part
        //

        private const string transformApiUrl = "subscriptions/{0}/resourceGroups/{1}/providers/Microsoft.Media/mediaservices/{2}/transforms/{3}?api-version=2021-11-01";

        public async Task<string> CreateTransformAsync(string transformName, TransformRestObject transformContent)
        {
            string URL = GenerateApiUrl(transformApiUrl, transformName);
            string responseContent = await CreateObjectAsync(URL, transformContent.ToJson());
            return responseContent;
        }

        public TransformRestObject GetTransformContent(string transformName)
        {
            Task<TransformRestObject> task = Task.Run<TransformRestObject>(async () => await GetTransformContentAsync(transformName));
            return task.Result;

            // return GetTransformContentAsync(transformName).GetAwaiter().GetResult();
        }

        public async Task<TransformRestObject> GetTransformContentAsync(string transformName)

        {
            string URL = GenerateApiUrl(transformApiUrl, transformName);
            string responseContent = await GetObjectContentAsync(URL);
            return TransformRestObject.FromJson(responseContent);
        }

        //
        // Content key policy
        //
        private const string ckpiUrl = "subscriptions/{0}/resourceGroups/{1}/providers/Microsoft.Media/mediaservices/{2}/contentKeyPolicies/{3}?api-version=2022-08-01";

        public async Task<string> GetContentKeyPolicyAsync(string contentKeyPolicyName)

        {
            string URL = GenerateApiUrl(ckpiUrl, contentKeyPolicyName);
            return await GetObjectContentAsync(URL);
        }

        private const string ckpiPropSecretsUrl = "subscriptions/{0}/resourceGroups/{1}/providers/Microsoft.Media/mediaservices/{2}/contentKeyPolicies/{3}/getPolicyPropertiesWithSecrets?api-version=2023-01-01";

        public async Task<string> GetContentKeyPolicyPropertiesWithSecretsAsync(string contentKeyPolicyName)

        {
            string URL = GenerateApiUrl(ckpiPropSecretsUrl, contentKeyPolicyName);
            return await PostObjectContentAsync(URL);
        }

        //
        // Streaming policy
        //
        private const string streamingUrl = "subscriptions/{0}/resourceGroups/{1}/providers/Microsoft.Media/mediaservices/{2}/streamingPolicies/{3}?api-version=2022-08-01";

        public async Task<string> GetStreamingPolicyAsync(string streamingPolicyName)

        {
            string URL = GenerateApiUrl(streamingUrl, streamingPolicyName);
            return await GetObjectContentAsync(URL);
        }


        //
        // account extension
        //

        private const string accountApiUrl = "subscriptions/{0}/resourceGroups/{1}/providers/Microsoft.Media/mediaservices/{2}?api-version=2023-01-01";
    

        public DateTime? GetAccountRetirementDate()
        {
            return Task.Run(async () => await GetAccountRetirementDateAsync()).GetAwaiter().GetResult();
        }

        public async Task<DateTime?> GetAccountRetirementDateAsync()
        {
            DateTime? retirementDate = null;
            string URL = GenerateApiUrl(accountApiUrl);
            var responseHeaders = await GetObjectContentAndHeaderAsync(URL);

            if (responseHeaders.Item2.TryGetValues("ams-retirement-date", out var values))
            {
                var headerValue = values.FirstOrDefault();
                // Use the header value as needed
                 retirementDate = DateTime.Parse(headerValue);
            }
            return retirementDate;
        }

        public HttpStatusCode ExtendAccount()
        {
            return Task.Run(async () => await ExtendAccountAsync()).GetAwaiter().GetResult();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>HttpStatusCode.Accepted is ok</returns>
        public async Task<HttpStatusCode> ExtendAccountAsync()
        {
            string URL = GenerateApiUrl(accountApiUrl);
            var response = await PatchObjectContentExtendAMSAsync(URL);
            
            return response.Item2;
        }
    }
}