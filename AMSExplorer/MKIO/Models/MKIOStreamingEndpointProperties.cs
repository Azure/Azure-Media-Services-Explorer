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
using System.Collections.Generic;

namespace AMSExplorer.MKIO.Models
{
    public class MKIOStreamingEndpointProperties
    {
        [JsonProperty("created")]
        public string Created { get; set; }

        [JsonProperty("lastModified")]
        public string LastModified { get; set; }

        [JsonProperty("provisioningState")]
        public string ProvisioningState { get; set; }

        [JsonProperty("resourceState")]
        public string ResourceState { get; set; }

        [JsonProperty("scaleUnits")]
        public int ScaleUnits { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("accessControl")]
        public object AccessControl { get; set; }

        [JsonProperty("cdnEnabled")]
        public bool CdnEnabled { get; set; }

        [JsonProperty("hostName")]
        public string HostName { get; set; }

        [JsonProperty("customHostNames")]
        public List<string> CustomHostNames { get; set; }

        [JsonProperty("maxCacheAge")]
        public int? MaxCacheAge { get; set; }

        [JsonProperty("crossSiteAccessPolicies")]
        public object CrossSiteAccessPolicies { get; set; }

        [JsonProperty("sku")]
        public MKIOStreamingEndpointSku Sku { get; set; }
    }
}