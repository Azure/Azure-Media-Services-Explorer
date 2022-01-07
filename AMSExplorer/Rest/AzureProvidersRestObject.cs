//----------------------------------------------------------------------------------------------
//    Copyright 2022 Microsoft Corporation
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

namespace AMSExplorer.Rest
{


    public class Authorization
    {
        [JsonProperty("applicationId")]
        public string ApplicationId { get; set; }

        [JsonProperty("roleDefinitionId")]
        public string RoleDefinitionId { get; set; }
    }

    public class ApiProfile
    {
        [JsonProperty("profileVersion")]
        public string ProfileVersion { get; set; }

        [JsonProperty("apiVersion")]
        public string ApiVersion { get; set; }
    }

    public class ZoneMapping
    {
        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("zones")]
        public List<string> Zones { get; set; }
    }

    public class ResourceTypeEntry
    {
        [JsonProperty("resourceType")]
        public string ResourceType { get; set; }

        [JsonProperty("locations")]
        public List<string> Locations { get; set; }

        [JsonProperty("apiVersions")]
        public List<string> ApiVersions { get; set; }

        [JsonProperty("defaultApiVersion")]
        public string DefaultApiVersion { get; set; }

        [JsonProperty("apiProfiles")]
        public List<ApiProfile> ApiProfiles { get; set; }

        [JsonProperty("capabilities")]
        public string Capabilities { get; set; }

        [JsonProperty("zoneMappings")]
        public List<ZoneMapping> ZoneMappings { get; set; }
    }

    public class AzureProvidersRestObject
    {
        public static AzureProvidersRestObject FromJson(string json)
        {
            return JsonConvert.DeserializeObject<AzureProvidersRestObject>(json, ConverterLE.Settings);
        }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("namespace")]
        public string Namespace { get; set; }

        [JsonProperty("authorizations")]
        public List<Authorization> Authorizations { get; set; }

        [JsonProperty("resourceTypes")]
        public List<ResourceTypeEntry> ResourceTypes { get; set; }

        [JsonProperty("registrationState")]
        public string RegistrationState { get; set; }

        [JsonProperty("registrationPolicy")]
        public string RegistrationPolicy { get; set; }
    }

}
