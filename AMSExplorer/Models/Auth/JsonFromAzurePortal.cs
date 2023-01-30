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

namespace AMSExplorer
{
    public class JsonFromAzurePortal
    {
        [JsonProperty("AZURE_CLIENT_ID")]
        public string AadClientId { get; set; }

        [JsonProperty("AZURE_CLIENT_SECRET")]
        public string AadSecret { get; set; }

        [JsonProperty("AZURE_TENANT_DOMAIN")]
        public string AadTenantDomain { get; set; }

        [JsonProperty("AZURE_TENANT_ID")]
        public string AadTenantId { get; set; }

        [JsonProperty("AZURE_MEDIA_SERVICES_ACCOUNT_NAME")]
        public string AccountName { get; set; }

        [JsonProperty("AZURE_RESOURCE_GROUP")]
        public string ResourceGroup { get; set; }

        [JsonProperty("AZURE_SUBSCRIPTION_ID")]
        public string SubscriptionId { get; set; }

        [JsonProperty("AZURE_ARM_TOKEN_AUDIENCE")]
        public Uri ArmTokenAudience { get; set; }

        [JsonProperty("AZURE_ARM_ENDPOINT")]
        public Uri ArmEndpoint { get; set; }

        [JsonProperty("AZURE_AAD_ENDPOINT")]
        public Uri AadEndpoint { get; set; }
    }
}
