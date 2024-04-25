using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Azure.Core;
using Newtonsoft.Json;

namespace AMSExplorer.Ravnur
{
    public class RavnurApiKeyCredentials : TokenCredential
    {
        private readonly Uri _authorityUri;
        private readonly string _subscriptionId;
        private readonly string _apiKey;

        public RavnurApiKeyCredentials(Uri authorityUri, string subscriptionId, string apiKey)
        {
            _authorityUri = authorityUri;
            _subscriptionId = subscriptionId;
            _apiKey = apiKey;
        }

        public override AccessToken GetToken(TokenRequestContext requestContext, CancellationToken cancellationToken)
        {
            return GetAccessToken(requestContext, cancellationToken).GetAwaiter().GetResult();
        }

        public override ValueTask<AccessToken> GetTokenAsync(TokenRequestContext requestContext, CancellationToken cancellationToken)
        {
            return GetAccessToken(requestContext, cancellationToken);
        }

        private async ValueTask<AccessToken> GetAccessToken(TokenRequestContext requestContext, CancellationToken cancellationToken)
        {
            string token = await RavnurAuthHelper.GetAccessToken(_subscriptionId, _authorityUri, _apiKey);

            string tokenDataPart = token[(token.IndexOf('.') + 1)..];
            tokenDataPart = tokenDataPart[..tokenDataPart.IndexOf('.')];
            string tokenDataStr = Encoding.UTF8.GetString(Convert.FromBase64String(tokenDataPart.PadRight(4 * ((tokenDataPart.Length + 3) / 4), '=')));

            dynamic tokenData = JsonConvert.DeserializeObject<dynamic>(tokenDataStr);

            var tokenValidTo = DateTime.UnixEpoch.AddSeconds((int)tokenData.exp);

            return new AccessToken(token, tokenValidTo);
        }

        public class GetTokenRequest
        {
            public string SubscriptionId { get; set; }

            public string ApiKey { get; set; }
        }
    }
}
