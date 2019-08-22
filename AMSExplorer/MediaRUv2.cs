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

using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

// These classes use REST v2 API to get and set the media reserved units

namespace AMSExplorer
{

    public partial class MediaRUv2Answer
    {
        [JsonProperty("odata.metadata")]
        public Uri OdataMetadata { get; set; }

        [JsonProperty("value")]
        public InfoMediaRU[] Value { get; set; }
    }

    public partial class InfoMediaRU
    {
        [JsonProperty("AccountId")]
        public Guid AccountId { get; set; }

        [JsonProperty("ReservedUnitType")]
        public int ReservedUnitType { get; set; }

        [JsonProperty("MaxReservableUnits")]
        public int MaxReservableUnits { get; set; }

        [JsonProperty("CurrentReservedUnits")]
        public int CurrentReservedUnits { get; set; }
    }

    public partial class MediaRUv2Answer
    {
        public static MediaRUv2Answer FromJson(string json)
        {
            return JsonConvert.DeserializeObject<MediaRUv2Answer>(json, AMSExplorer.Converter.Settings);
        }
    }

    public static class Serialize
    {
        public static string ToJson(this MediaRUv2Answer self)
        {
            return JsonConvert.SerializeObject(self, AMSExplorer.Converter.Settings);
        }
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    public class MediaRU
    {
        private static readonly HttpClient _client = new HttpClient();
        private DateTime _tokenSPExpirationTime = DateTime.Now.AddMinutes(-1);
        private string _tokenSP = null;
        private InfoMediaRU _myanswer = null;
        private dynamic _restEndpoint = null;

        public MediaRU()
        {
            _client.DefaultRequestHeaders.Add("x-ms-version", "2.19");
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Add("DataServiceVersion", "3.0;NetFx");
            _client.DefaultRequestHeaders.Add("MaxDataServiceVersion", "3.0;NetFx");

        }

        private async Task GetRestAPIEndpointforAccountv2IfNeeded(AMSClientV3 AmsClientV3)
        {
            if (_restEndpoint != null) return; // we already know the restpoint, no need to get it

            // This method get the RESTApiEndpoint URL
            string token = AmsClientV3.accessToken?.AccessToken;

            // if SP
            if (AmsClientV3.accessToken == null && AmsClientV3.credentialsEntry.UseSPAuth)
            {
                // let's get the current token in Service Principal mode
                TokenCacheItem accessTokenCache = TokenCache.DefaultShared.ReadItems()
                        .Where(t => t.ClientId == AmsClientV3.credentialsEntry.ADSPClientId)
                        .OrderByDescending(t => t.ExpiresOn)
                        .First();
                token = accessTokenCache?.AccessToken;
            }

            if (token == null)
            {
                return;
            }

            string URL = AmsClientV3.environment.ArmEndpoint + AmsClientV3.credentialsEntry.MediaService.Id.Substring(1) + "?api-version=2015-10-01";

            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            HttpResponseMessage response = await client.GetAsync(URL);
            if (response.IsSuccessStatusCode)
            {
                string str = await response.Content.ReadAsStringAsync();
                dynamic json = JObject.Parse(str);
                _restEndpoint = json.properties.apiEndpoints[0].endpoint;
            }
        }

        private async Task GetRefreshTokenIfNeededAsync(AMSClientV3 AmsClientV3)
        {
            // If Service Principal mode, let's authenticate now (if token expired or first time)
            if (AmsClientV3.accessTokenForRestV2 == null && _tokenSPExpirationTime < DateTime.Now)
            {
                HttpClient client = new HttpClient();

                string URLAut = string.Format(AmsClientV3.environment.AADSettings.AuthenticationEndpoint + "/{0}/oauth2/token", AmsClientV3.credentialsEntry.AadTenantId);

                // if end user used the sp cli output then we don't know the MediaSercices Resource. Let's guess it.
                if (string.IsNullOrEmpty(AmsClientV3.environment.MediaServicesV2Resource))
                {
                    string[] names = Enum.GetNames(typeof(AzureEnvType));
                    IEnumerable<AzureEnvironment> envs = names.Select(n => new AzureEnvironment((AzureEnvType)Enum.Parse(typeof(AzureEnvType), n)));
                    AmsClientV3.environment.MediaServicesV2Resource = envs.Where(e => e.ArmEndpoint == AmsClientV3.environment.ArmEndpoint).FirstOrDefault()?.MediaServicesV2Resource;
                }

                if (string.IsNullOrEmpty(AmsClientV3.environment.MediaServicesV2Resource))
                {
                    // not found the resource url
                    return;
                };

                Dictionary<string, string> values = new Dictionary<string, string>
                                                            {
                                                                { "grant_type", "client_credentials" },
                                                                { "client_id", AmsClientV3.credentialsEntry.ADSPClientId },
                                                                { "client_secret", AmsClientV3.credentialsEntry.ClearADSPClientSecret },
                                                                { "resource", AmsClientV3.environment.MediaServicesV2Resource }
                                                            };

                FormUrlEncodedContent content = new FormUrlEncodedContent(values);
                HttpResponseMessage response = await client.PostAsync(URLAut, content);

                if (!response.IsSuccessStatusCode)
                {
                    string message = await response.Content.ReadAsStringAsync();
                    dynamic jsonMessage = JObject.Parse(message);
                    throw new Exception(response.ReasonPhrase + " " + jsonMessage?.error_description);
                }

                string responseStringAut = await response.Content.ReadAsStringAsync();
                dynamic json = JObject.Parse(responseStringAut);
                _tokenSP = json.access_token;
                string expirein = json.expires_in;
                _tokenSPExpirationTime = DateTime.Now.AddSeconds(double.Parse(expirein));
            }
        }

        public InfoMediaRU GetInfoMediaRU(AMSClientV3 AmsClientV3)
        {
            return Task.Run(() => GetInfoMediaRUAsync(AmsClientV3)).GetAwaiter().GetResult();
        }

        public async Task<InfoMediaRU> GetInfoMediaRUAsync(AMSClientV3 AmsClientV3)
        {

            await GetRefreshTokenIfNeededAsync(AmsClientV3);
            await GetRestAPIEndpointforAccountv2IfNeeded(AmsClientV3);

            string URL = _restEndpoint + "EncodingReservedUnitTypes";

            string token = AmsClientV3.accessTokenForRestV2 != null ? AmsClientV3.accessTokenForRestV2.AccessToken : _tokenSP;

            _client.DefaultRequestHeaders.Remove("Authorization");
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            string responseString = await _client.GetStringAsync(URL);
            _myanswer = MediaRUv2Answer.FromJson(responseString).Value[0];
            return _myanswer;

        }

        public void SetMediaRU(AMSClientV3 AmsClientV3, int? Number, int? ReservedUniType)
        {
            Task.Run(() => SetMediaRUAsync(AmsClientV3, Number, ReservedUniType)).GetAwaiter().GetResult();
        }

        public async Task SetMediaRUAsync(AMSClientV3 AmsClientV3, int? Number, int? ReservedUniType)
        {
            if (_myanswer == null)
            {
                throw new Exception();
            }

            await GetRefreshTokenIfNeededAsync(AmsClientV3);
            await GetRestAPIEndpointforAccountv2IfNeeded(AmsClientV3);

            string URL = _restEndpoint + "EncodingReservedUnitTypes(guid'" + _myanswer.AccountId.ToString() + "')";

            string token = AmsClientV3.accessTokenForRestV2 != null ? AmsClientV3.accessTokenForRestV2.AccessToken : _tokenSP;

            _client.DefaultRequestHeaders.Remove("Authorization");
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            dynamic myObject = (dynamic)new JObject();
            if (Number != null)
            {
                myObject.CurrentReservedUnits = (int)Number;
            }

            if (ReservedUniType != null)
            {
                myObject.ReservedUnitType = (int)ReservedUniType;
            }

            StringContent content = new StringContent(myObject.ToString(), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PutAsync(URL, content);

            if (!response.IsSuccessStatusCode)
            {
                string message = await response.Content.ReadAsStringAsync();
                dynamic json = JObject.Parse(message);
                throw new Exception(response.ReasonPhrase + " " + json?["odata.error"]?.message?.value);
            }
        }
    }
}