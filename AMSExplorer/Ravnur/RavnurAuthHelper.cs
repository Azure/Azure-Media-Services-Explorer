using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Newtonsoft.Json;

using static AMSExplorer.Ravnur.RavnurApiKeyCredentials;

namespace AMSExplorer.Ravnur
{
    public static class RavnurAuthHelper
    {
        private static readonly HttpClient _httpClinet = new();

        public static async Task<string> GetAccessToken(string subscriptionId, Uri apiEndpoint, string apiKey)
        {
            var tokenRequest = new GetTokenRequest
            {
                SubscriptionId = subscriptionId,
                ApiKey = apiKey,
            };

            var authUri = new Uri(apiEndpoint, "/auth/token");
            var authContent = new StringContent(JsonConvert.SerializeObject(tokenRequest), Encoding.UTF8, "application/json");

            HttpResponseMessage authResponse = await _httpClinet.PostAsync(authUri, authContent, CancellationToken.None);
            authResponse.EnsureSuccessStatusCode();

            return await authResponse.Content.ReadAsStringAsync();
        }
    }
}
