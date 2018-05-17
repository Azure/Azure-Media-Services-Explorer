using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Rest;

namespace AMSExplorer
{
    /// <summary>
    /// Provides token credentials.
    /// </summary>
    /// <remarks>
    /// This class allows authentication to be implemented without depending on
    /// Microsoft.Rest.ClientRuntime.Azure.Authentication. The Azure.Authentication library
    /// depends on a 2.x version of Microsoft.IdentityModel.Clients.ActiveDirectory which
    /// causes problems during code analysis.
    /// </remarks>
    public class ArmClientCredentials : ServiceClientCredentials
    {
        private readonly AuthenticationContext _authenticationContext;
        private readonly Uri _customerArmAadAudience;
        private readonly ClientCredential _clientCredential;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArmClientCredentials"/> class.
        /// </summary>
        /// <param name="testSettings">The test settings.</param>
        public ArmClientCredentials(ConfigWrapper config)
        {
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            var authority = config.AadEndpoint.AbsoluteUri + config.AadTenantId;

            _authenticationContext = new AuthenticationContext(authority);
            _customerArmAadAudience = config.ArmAadAudience;
            _clientCredential = new ClientCredential(config.AadClientId, config.AadSecret);
        }

        /// <summary>
        /// Apply the credentials to the HTTP request.
        /// </summary>
        /// <param name="request">The HTTP request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>
        /// Task that will complete when processing has completed.
        /// </returns>
        public async override Task ProcessHttpRequestAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var token = await _authenticationContext.AcquireTokenAsync(_customerArmAadAudience.OriginalString, _clientCredential);
            request.Headers.Authorization = new AuthenticationHeaderValue(token.AccessTokenType, token.AccessToken);
            await base.ProcessHttpRequestAsync(request, cancellationToken);
        }
    }
}
