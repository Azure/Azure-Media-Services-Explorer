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


using AMSExplorer.Rest;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AMSExplorer.MKIO.Models
{

    public class MKIOStreamingEndpoint
    {
        public static MKIOStreamingEndpoint FromJson(string json)
        {
            return JsonConvert.DeserializeObject<MKIOStreamingEndpoint>(json, ConverterLE.Settings);
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, ConverterLE.Settings);
        }

        public MKIOStreamingEndpoint(string location, string description, MKIOStreamingEndpointSku sku, int scaleUnits, bool cdnEnabled)
        {
            Location = location;
            Properties = new MKIOStreamingEndpointProperties { Description = description, Sku = sku, ScaleUnits = scaleUnits, CdnEnabled = cdnEnabled };
            Tags = new Dictionary<string, string>();
        }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("tags")]
        public Dictionary<string, string> Tags { get; set; }

        [JsonProperty("properties")]
        public MKIOStreamingEndpointProperties Properties { get; set; }
    }
}