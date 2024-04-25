using System;

using Newtonsoft.Json;

namespace AMSExplorer.Ravnur
{
    public class RavnurConfigurationOptions
    {
        [JsonProperty("AZURE_SUBSCRIPTION_ID")]
        public string SubscriptionId { get; set; }

        [JsonProperty("AZURE_RESOURCE_GROUP")]
        public string ResourceGroup { get; set; }

        [JsonProperty("RAVNUR_MEDIA_SERVICES_ACCOUNT_NAME")]
        public string AccountName { get; set; }

        [JsonProperty("RAVNUR_API_ENDPOINT")]
        public Uri ApiEndpoint { get; set; }

        [JsonProperty("RAVNUR_API_KEY")]
        public string ApiKey { get; set; }
    }
}
