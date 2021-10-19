//----------------------------------------------------------------------------------------------
//    Copyright 2021 Microsoft Corporation
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

using Microsoft.Rest.Azure.Authentication;
using System;

namespace AMSExplorer
{
    public class AzureEnvironment
    {
        public string DisplayName { get; set; }
        //public string Authority { get; set; }
        public Uri ArmEndpoint { get; set; }
        public string ClientApplicationId { get; set; }
        public string MediaServicesV2Resource { get; set; }
        public ActiveDirectoryServiceSettings AADSettings { get; set; }


        public AzureEnvironment(AzureEnvType type)
        {
            switch (type)
            {
                case AzureEnvType.DevTest:
                    DisplayName = "Azure Dev/Test";
                    ArmEndpoint = new Uri("https://api-dogfood.resources.windows-int.net/");
                    ClientApplicationId = "04b07795-8ddb-461a-bbee-02f9e1bf7b46";
                    AADSettings = new ActiveDirectoryServiceSettings() { TokenAudience = new Uri("https://management.core.windows.net/"), ValidateAuthority = true, AuthenticationEndpoint = new Uri("https://login.windows-ppe.net/") };
                    MediaServicesV2Resource = "https://rest.media.azure-test.net";
                    break;

                case AzureEnvType.Azure:
                    DisplayName = "Azure";
                    ArmEndpoint = new Uri("https://management.azure.com/");
                    ClientApplicationId = "04b07795-8ddb-461a-bbee-02f9e1bf7b46";
                    AADSettings = ActiveDirectoryServiceSettings.Azure;
                    MediaServicesV2Resource = "https://rest.media.azure.net";
                    break;

                case AzureEnvType.AzureChina:
                    DisplayName = "Azure China";
                    ArmEndpoint = new Uri("https://management.chinacloudapi.cn/");
                    ClientApplicationId = "04b07795-8ddb-461a-bbee-02f9e1bf7b46";
                    AADSettings = ActiveDirectoryServiceSettings.AzureChina;
                    MediaServicesV2Resource = "https://rest.media.chinacloudapi.cn";
                    break;

                case AzureEnvType.AzureUSGovernment:
                    DisplayName = "Azure US Government";
                    ArmEndpoint = new Uri("https://management.usgovcloudapi.net/");
                    ClientApplicationId = "04b07795-8ddb-461a-bbee-02f9e1bf7b46";
                    AADSettings = ActiveDirectoryServiceSettings.AzureUSGovernment;
                    MediaServicesV2Resource = "https://rest.media.usgovcloudapi.net";
                    break;

                case AzureEnvType.AzureGermany:
                    DisplayName = "Azure Germany";
                    ArmEndpoint = new Uri("https://management.microsoftazure.de/");
                    ClientApplicationId = "04b07795-8ddb-461a-bbee-02f9e1bf7b46";
                    AADSettings = ActiveDirectoryServiceSettings.AzureGermany;
                    MediaServicesV2Resource = "https://rest.media.cloudapi.de";
                    break;

                case AzureEnvType.Custom:
                    DisplayName = "Custom";
                    ArmEndpoint = null;
                    ClientApplicationId = string.Empty;
                    AADSettings = new ActiveDirectoryServiceSettings();
                    MediaServicesV2Resource = null;
                    break;
            }
        }

        public string ReturnStorageSuffix()
        {
            return "core." + ReturnHostNameTwoSegmentsRight(AADSettings.TokenAudience.ToString()); // "core.cloudapi.de"
        }

        private static string ReturnHostNameTwoSegmentsRight(string myUrl)
        {
            string[] hosts = (new Uri(myUrl)).Host.Split('.');
            int i = hosts.Length;
            return hosts[i - 2] + "." + hosts[i - 1];
        }
    }
}
