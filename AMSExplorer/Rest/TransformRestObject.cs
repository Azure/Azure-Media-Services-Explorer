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

namespace AMSExplorer.Rest
{
    /// <summary>
    /// Rest call for transforms
    /// https://docs.microsoft.com/en-us/rest/api/media/transforms
    /// 
    /// </summary>
    public partial class AmsClientRest
    {
        private const string transformApiUrl = "subscriptions/{0}/resourceGroups/{1}/providers/Microsoft.Media/mediaservices/{2}/transforms/{3}?api-version=2018-07-01";

        public async Task<string> CreateTransformAsync(string transformName, TransformRestObject transformContent)
        {
            string URL = GenerateApiUrl(transformApiUrl, transformName);
            HttpClient client = GetHttpClient();

            string _requestContent = transformContent.ToJson();
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

        public TransformRestObject GetTransformContent(string transformName)
        {
            return GetTransformContentAsync(transformName).GetAwaiter().GetResult();
        }

        public async Task<TransformRestObject> GetTransformContentAsync(string transformName)
        {
            string URL = GenerateApiUrl(transformApiUrl, transformName);
            HttpClient client = GetHttpClient();

            HttpResponseMessage amsRequestResult = await client.GetAsync(URL).ConfigureAwait(false);

            string responseContent = await amsRequestResult.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (!amsRequestResult.IsSuccessStatusCode)
            {
                dynamic error = JsonConvert.DeserializeObject(responseContent);
                throw new Exception((string)error?.error?.message);
            }

            return TransformRestObject.FromJson(responseContent);
        }
    }


    public class TransformRestObject
    {
        public static TransformRestObject FromJson(string json)
        {
            return JsonConvert.DeserializeObject<TransformRestObject>(json, ConverterLE.Settings);
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, ConverterLE.Settings);
        }

        public TransformRestObject(string name, string description, IList<TransformRestOutput> outputs)
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

    public static class SerializeTransformForRest
    {
        public static string ToJson(this Transform self)
        {
            return JsonConvert.SerializeObject(self, ConverterLE.Settings);
        }
    }
}