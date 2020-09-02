//----------------------------------------------------------------------------------------------
//    Copyright 2020 Microsoft Corporation
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

using Microsoft.Azure.Management.Media.Models;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AMSExplorer.TransformRest
{
    /// <summary>
    /// Rest call for transforms
    /// https://docs.microsoft.com/en-us/rest/api/media/transforms
    /// 
    /// </summary>
    public class AmsClientRestTransform
    {
        private const string transformApiUrl = "subscriptions/{0}/resourceGroups/{1}/providers/Microsoft.Media/mediaservices/{2}/transforms/{3}?api-version=2018-07-01";

        private readonly AMSClientV3 _amsClient;

        public AmsClientRestTransform(AMSClientV3 amsClient)
        {
            _amsClient = amsClient;
        }

        public async Task<string> CreateTransformAsync(string transformName, TransformForRest transformContent)
        {
            string URL = GenerateApiUrl(transformName);

            string token = _amsClient.accessToken != null ? _amsClient.accessToken.AccessToken :
                TokenCache.DefaultShared.ReadItems()
                    .Where(t => t.ClientId == _amsClient.credentialsEntry.ADSPClientId)
                    .OrderByDescending(t => t.ExpiresOn)
                    .First().AccessToken;

            HttpClient client = _amsClient.AMSclient.HttpClient;
            client.DefaultRequestHeaders.Remove("Authorization");
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            string _requestContent = transformContent.ToJson(); // Microsoft.Rest.Serialization.SafeJsonConvert.SerializeObject(liveEvent, serializationSettings);
            StringContent httpContent = new StringContent(_requestContent, System.Text.Encoding.UTF8);
            httpContent.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/json; charset=utf-8");

            HttpResponseMessage amsRequestResult = await client.PutAsync(URL, httpContent).ConfigureAwait(false);
            string responseContent = await amsRequestResult.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (!amsRequestResult.IsSuccessStatusCode)
            {
                dynamic error = JsonConvert.DeserializeObject(responseContent);
                throw new Exception((string)error?.error?.message);
            }

            return responseContent;
        }

        public TransformForRest GetTransformContent(string transformName)
        {
            return GetTransformContentAsync(transformName).GetAwaiter().GetResult();
        }

        public async Task<TransformForRest> GetTransformContentAsync(string transformName)
        {
            string URL = GenerateApiUrl(transformName);

            string token = _amsClient.accessToken != null ? _amsClient.accessToken.AccessToken :
                TokenCache.DefaultShared.ReadItems()
                    .Where(t => t.ClientId == _amsClient.credentialsEntry.ADSPClientId)
                    .OrderByDescending(t => t.ExpiresOn)
                    .First().AccessToken;

            HttpClient client = _amsClient.AMSclient.HttpClient;
            client.DefaultRequestHeaders.Remove("Authorization");
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            HttpResponseMessage amsRequestResult = await client.GetAsync(URL).ConfigureAwait(false);

            string responseContent = await amsRequestResult.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (!amsRequestResult.IsSuccessStatusCode)
            {
                dynamic error = JsonConvert.DeserializeObject(responseContent);
                throw new Exception((string)error?.error?.message);
            }

            return TransformForRest.FromJson(responseContent);
        }

        private string GenerateApiUrl(string transformName)
        {
            return _amsClient.environment.ArmEndpoint
                                       + string.Format(transformApiUrl,
                                                          _amsClient.credentialsEntry.AzureSubscriptionId,
                                                          _amsClient.credentialsEntry.ResourceGroup,
                                                          _amsClient.credentialsEntry.AccountName,
                                                          transformName
                                                  );
        }
    }
       

    public class TransformForRest
    {

        public static TransformForRest FromJson(string json)
        {
            return JsonConvert.DeserializeObject<TransformForRest>(json, ConverterLE.Settings);
        }

        public TransformForRest(string name, string description, IList<TransformRestOutput> outputs)
        {
            Name = name;
            Properties = new Properties { Description = description, Outputs = outputs };
        }


        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        [JsonProperty("properties", NullValueHandling = NullValueHandling.Ignore)]
        public Properties Properties { get; set; }
    }

    public class Properties
    {
        [JsonProperty("created", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? Created { get; set; }

        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        [JsonProperty("lastModified", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? LastModified { get; set; }

        [JsonProperty("outputs", NullValueHandling = NullValueHandling.Ignore)]
        public IList<TransformRestOutput> Outputs { get; set; }
    }

    public class TransformRestOutput
    {
        [JsonProperty("onError", NullValueHandling = NullValueHandling.Ignore)]
        public string OnError { get; set; }

        [JsonProperty("relativePriority", NullValueHandling = NullValueHandling.Ignore)]
        public string RelativePriority { get; set; }

        [JsonProperty("preset", NullValueHandling = NullValueHandling.Ignore)]
        public dynamic Preset { get; set; }
    }

    public static class SerializeForRest
    {
        public static string ToJson(this TransformForRest self)
        {
            return JsonConvert.SerializeObject(self, ConverterLE.Settings);
        }
    }

    internal static class ConverterLE
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}