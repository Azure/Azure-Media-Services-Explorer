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
using System.Net;
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
        public static MediaRUv2Answer FromJson(string json) => JsonConvert.DeserializeObject<MediaRUv2Answer>(json, AMSExplorer.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this MediaRUv2Answer self) => JsonConvert.SerializeObject(self, AMSExplorer.Converter.Settings);
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

        public MediaRU(AMSClientV3 _amsClientV3)
        {
            _client.DefaultRequestHeaders.Add("x-ms-version", "2.19");
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Add("DataServiceVersion", "3.0;NetFx");
            _client.DefaultRequestHeaders.Add("MaxDataServiceVersion", "3.0;NetFx");

            GetRestAPIEndpointforAccountv2(_amsClientV3).GetAwaiter().GetResult();
        }

        private async Task GetRestAPIEndpointforAccountv2(AMSClientV3 _amsClientV3)
        {
            // This method get the RESTApiEndpoint URL
            string token = _amsClientV3.accessToken?.AccessToken;

            // if SP
            if (_amsClientV3.accessToken == null && _amsClientV3.credentialsEntry.UseSPAuth)
            {
                // let's get the current token in Service Principal mode
                var accessTokenCache = TokenCache.DefaultShared.ReadItems()
                        .Where(t => t.ClientId == _amsClientV3.credentialsEntry.ADSPClientId)
                        .OrderByDescending(t => t.ExpiresOn)
                        .First();
                token = accessTokenCache?.AccessToken;
            }

            if (token == null) return;

            string URL = _amsClientV3.environment.ArmEndpoint + _amsClientV3.credentialsEntry.MediaService.Id.Substring(1) + "?api-version=2015-10-01";

            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Remove("Authorization");
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            HttpResponseMessage response = await client.GetAsync(URL);
            if (response.IsSuccessStatusCode)
            {
                var str = await response.Content.ReadAsStringAsync();
                dynamic json = JObject.Parse(str);
                _restEndpoint = json.properties.apiEndpoints[0].endpoint;
            }
        }

        private async Task GetRefreshTokenIfNeeded(AMSClientV3 _amsClientV3)
        {
            // If Service Principal mode, let's authenticate now (if token expired or first time)
            if (_amsClientV3.accessTokenForRestV2 == null && _tokenSPExpirationTime < DateTime.Now)
            {
                HttpClient client = new HttpClient();

                string URLAut = string.Format(_amsClientV3.environment.AADSettings.AuthenticationEndpoint + "/{0}/oauth2/token", _amsClientV3.credentialsEntry.AadTenantId);

                // if end user used the sp cli output then we don't know the MediaSercices Resource. Let's guess it.
                if (string.IsNullOrEmpty(_amsClientV3.environment.MediaServicesV2Resource))
                {
                    var names = Enum.GetNames(typeof(AzureEnvType));
                    var envs = names.Select(n => new AzureEnvironment((AzureEnvType)Enum.Parse(typeof(AzureEnvType), n)));
                    _amsClientV3.environment.MediaServicesV2Resource = envs.Where(e => e.ArmEndpoint == _amsClientV3.environment.ArmEndpoint).FirstOrDefault()?.MediaServicesV2Resource;
                }

                if (string.IsNullOrEmpty(_amsClientV3.environment.MediaServicesV2Resource))
                {
                    // not found the resource url
                    return;
                };

                var values = new Dictionary<string, string>
                                                            {
                                                                { "grant_type", "client_credentials" },
                                                                { "client_id", _amsClientV3.credentialsEntry.ADSPClientId },
                                                                { "client_secret", _amsClientV3.credentialsEntry.ClearADSPClientSecret },
                                                                { "resource", _amsClientV3.environment.MediaServicesV2Resource }
                                                            };

                var content = new FormUrlEncodedContent(values);
                var response = await client.PostAsync(URLAut, content);
                var responseStringAut = await response.Content.ReadAsStringAsync();
                dynamic json = JObject.Parse(responseStringAut);
                _tokenSP = json.access_token;
                string expirein = json.expires_in;
                _tokenSPExpirationTime = DateTime.Now.AddSeconds(double.Parse(expirein));
            }
        }

        public async Task<InfoMediaRU> GetInfoMediaRU(AMSClientV3 _amsClientV3)
        {

            await GetRefreshTokenIfNeeded(_amsClientV3);

            string URL = _restEndpoint + "EncodingReservedUnitTypes";

            string token = _amsClientV3.accessTokenForRestV2 != null ? _amsClientV3.accessTokenForRestV2.AccessToken : _tokenSP;

            _client.DefaultRequestHeaders.Remove("Authorization");
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            var responseString = await _client.GetStringAsync(URL);
            _myanswer = MediaRUv2Answer.FromJson(responseString).Value[0];
            return _myanswer;

        }

        public async Task SetMediaRU(AMSClientV3 _amsClientV3, int? number, int? reservedunitype)
        {
            if (_myanswer == null) throw new Exception();

            await GetRefreshTokenIfNeeded(_amsClientV3);

            string URL = _restEndpoint + "EncodingReservedUnitTypes(guid'" + _myanswer.AccountId.ToString() + "')";

            string token = _amsClientV3.accessTokenForRestV2 != null ? _amsClientV3.accessTokenForRestV2.AccessToken : _tokenSP;

            _client.DefaultRequestHeaders.Remove("Authorization");
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            var myObject = (dynamic)new JObject();
            if (number != null) myObject.CurrentReservedUnits = (int)number;
            if (reservedunitype != null) myObject.ReservedUnitType = (int)reservedunitype;

            var content = new StringContent(myObject.ToString(), Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(URL, content);

            if (!response.IsSuccessStatusCode)
            {
                string message = await response.Content.ReadAsStringAsync();
                dynamic json = JObject.Parse(message);
                throw new Exception(response.ReasonPhrase + " " + json?["odata.error"]?.message?.value);

            }
        }
    }
}