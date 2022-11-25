using Azure.Core;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AMSClient
{
    public class BearerTokenCredential : TokenCredential
    {
        /// <summary>
        /// Bearer Token String
        /// </summary>
        private string Token { get; set; }

        /// <summary>
        /// Constructor that takes a Bearer Token
        /// </summary>
        /// <param name="token"/>
        public BearerTokenCredential(string token)
        {
            Token = token;
        }

        /// <summary>
        /// Return a Bearer Token
        /// </summary>
        /// <param name="requestContext"/>
        /// <param name="cancellationToken"/>
        /// <returns></returns>
        public override AccessToken GetToken(TokenRequestContext requestContext, CancellationToken cancellationToken)
        {
            return new AccessToken(Token, DateTimeOffset.Now.AddDays(1));
        }

        /// <summary>
        /// Returns a Bearer Token Asynchronously
        /// </summary>
        /// <param name="requestContext"/>
        /// <param name="cancellationToken"/>
        /// <returns></returns>
        public override ValueTask<AccessToken> GetTokenAsync(TokenRequestContext requestContext, CancellationToken cancellationToken)
        {
            return new ValueTask<AccessToken>(Task.FromResult(new AccessToken(Token, DateTimeOffset.Now.AddDays(1))));
        }
    }
}
