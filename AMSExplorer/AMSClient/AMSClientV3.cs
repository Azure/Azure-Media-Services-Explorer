using AMSExplorer.AMSLogin;
using Microsoft.Azure.Management.Media;
using Microsoft.Azure.Management.Media.Models;
using Microsoft.Identity.Client;
using Microsoft.Identity.Client.Extensibility;
using Microsoft.Rest;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace AMSExplorer
{
    public class AMSClientV3
    {
        public AzureMediaServicesClient AMSclient;
        public AuthenticationResult authResult;
        public CredentialsEntryV3 credentialsEntry;
        private Form _form;
        public TokenCredentials credentials;
        public AzureEnvironment environment;
        private readonly string _azureSubscriptionId;
        private readonly IPublicClientApplication _appInteract;
        private readonly IConfidentialClientApplication _appSP;
        private readonly string[] scopes, scopes2, scopes3;
        private bool firstTimeAuth = true;
        private readonly System.Timers.Timer TimerAutoRefreshAuthToken;


        public AMSClientV3(AzureEnvironment myEnvironment, string azureSubscriptionId, CredentialsEntryV3 myCredentialsEntry, Form form)
        {
            environment = myEnvironment;
            _azureSubscriptionId = azureSubscriptionId;
            credentialsEntry = myCredentialsEntry;
            _form = form;

            if (!credentialsEntry.UseSPAuth)
            {
                _appInteract = PublicClientApplicationBuilder.Create(environment.ClientApplicationId)
                  //.WithAuthority(AzureCloudInstance.AzurePublic, credentialsEntry.AadTenantId)
                  .WithAuthority(environment.AADSettings.AuthenticationEndpoint + string.Format("{0}", credentialsEntry.AadTenantId ?? "common"))
                  .WithRedirectUri("http://localhost")
                  .Build();
            }
            else // SP
            {
                _appSP = ConfidentialClientApplicationBuilder.Create(credentialsEntry.ADSPClientId)
                     .WithClientSecret(credentialsEntry.ClearADSPClientSecret)
                      .WithAuthority(environment.AADSettings.AuthenticationEndpoint + string.Format("{0}", credentialsEntry.AadTenantId ?? "common"), true)
                     .Build();
            }

            scopes = new[] { environment.AADSettings.TokenAudience.ToString() + "/user_impersonation" };
            scopes2 = new[] { environment.MediaServicesV2Resource + "/user_impersonation" };
            scopes3 = new[] { environment.AADSettings.TokenAudience.ToString() + "/.default" };


            // Timer Auto Refresh of Auth token
            TimerAutoRefreshAuthToken = new System.Timers.Timer() { AutoReset = false };
        }

        public void SetNewFormParent(Form form)
        {
            _form = form;
        }


        private async void OnTimedEventAuthRefresh(object sender, ElapsedEventArgs e)
        {
            await ConnectAndGetNewClientV3Async();
            if (authResult != null)
            {
                var interval = (authResult.ExpiresOn.ToUniversalTime() - DateTimeOffset.UtcNow.AddMinutes(3)).TotalMilliseconds;
                try
                {
                    // next refresh for the token : 3 minutes before it expires
                    TimerAutoRefreshAuthToken.Interval = (authResult.ExpiresOn.ToUniversalTime() - DateTimeOffset.UtcNow.AddMinutes(3)).TotalMilliseconds;
                    TimerAutoRefreshAuthToken.Start();
                }
                catch (Exception ex)
                {
                    Telemetry.TrackException(ex);
                }
            }
        }


        public async Task<AzureMediaServicesClient> ConnectAndGetNewClientV3Async(Form callerForm = null)
        {
            if (!credentialsEntry.UseSPAuth)
            {
                var accounts = await _appInteract.GetAccountsAsync();

                try
                {
                    authResult = await _appInteract.AcquireTokenSilent(scopes, accounts.FirstOrDefault()).ExecuteAsync().ConfigureAwait(false);
                }
#pragma warning disable CS0168 // Variable is declared but never used
                catch (MsalUiRequiredException ex)
#pragma warning restore CS0168 // Variable is declared but never used
                {
                    try
                    {
                        authResult = await _appInteract.AcquireTokenInteractive(scopes)
                             .WithPrompt(credentialsEntry.PromptUser ? Prompt.ForceLogin : Prompt.SelectAccount)
                             .WithCustomWebUi(new EmbeddedBrowserCustomWebUI(callerForm ?? _form))
                             .ExecuteAsync();

                    }
                    catch (MsalException maslException)
                    {
                        Debug.Print("MSAL interactive authentication exception !" + maslException.Message);
                    }
                }
                catch (MsalException maslException)
                {
                    Debug.Print("MSAL silent authentication exception !" + maslException.Message);
                }
            }

            else // Service Principal
            {
                if (firstTimeAuth)
                {
                    AmsLoginServicePrincipal form = new()
                    {
                        ClientId = credentialsEntry.ADSPClientId,
                        ClientSecret = credentialsEntry.ClearADSPClientSecret
                    };

                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        credentialsEntry.ADSPClientId = form.ClientId;
                        credentialsEntry.ClearADSPClientSecret = form.ClientSecret;
                    }
                    else
                    {
                        return null;
                    }
                }

                // IByRefreshToken appRt = app3 as IByRefreshToken;

                authResult = await _appSP.AcquireTokenForClient(scopes3)
                                                         .ExecuteAsync()
                                                         .ConfigureAwait(false);

            }

            credentials = new TokenCredentials(authResult.AccessToken, "Bearer");

            // Getting Media Services account...
            AMSclient = new AzureMediaServicesClient(environment.ArmEndpoint, credentials)
            {
                SubscriptionId = _azureSubscriptionId
            };

            if (firstTimeAuth)
            {
                // let's get info on mediaService, specifically to get the location (region)
                credentialsEntry.MediaService = await AMSclient.Mediaservices.GetAsync(credentialsEntry.ResourceGroup, credentialsEntry.AccountName);

                // let's refresh the token 3 minutes before it expires
                TimerAutoRefreshAuthToken.Interval = (authResult.ExpiresOn.ToUniversalTime() - DateTimeOffset.UtcNow.AddMinutes(3)).TotalMilliseconds;
                TimerAutoRefreshAuthToken.Elapsed += new ElapsedEventHandler(OnTimedEventAuthRefresh);
                TimerAutoRefreshAuthToken.Start();

                firstTimeAuth = false;
            }

            string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            AMSclient.SetUserAgent("AMSE", version);
            return AMSclient;
        }


        public static string GetStorageName(string storageId)
        {
            return storageId.Split('/').Last();
        }

        public static string GetStorageResourceName(string storageId)
        {
            string[] split = storageId.Split('/');
            return storageId.Split('/')[split.Length - 5];
        }

        public async Task<Asset> GetAssetAsync(string assetName, CancellationToken token = default)
        {
            return await AMSclient.Assets.GetAsync(credentialsEntry.ResourceGroup, credentialsEntry.AccountName, assetName, token).ConfigureAwait(false);
        }

        public async Task<Job> GetJobAsync(string transformName, string jobName)
        {
            return await AMSclient.Jobs.GetAsync(credentialsEntry.ResourceGroup, credentialsEntry.AccountName, transformName, jobName).ConfigureAwait(false);
        }

        public async Task<Transform> GetTransformAsync(string transformName)
        {
            return await AMSclient.Transforms.GetAsync(credentialsEntry.ResourceGroup, credentialsEntry.AccountName, transformName).ConfigureAwait(false);
        }

        public async Task<LiveEvent> GetLiveEventAsync(string liveEventName)
        {
            return await AMSclient.LiveEvents.GetAsync(credentialsEntry.ResourceGroup, credentialsEntry.AccountName, liveEventName).ConfigureAwait(false);
        }

        public async Task<LiveOutput> GetLiveOutputAsync(string liveEventName, string liveOutputName)
        {
            return await AMSclient.LiveOutputs.GetAsync(credentialsEntry.ResourceGroup, credentialsEntry.AccountName, liveEventName, liveOutputName).ConfigureAwait(false);
        }

        public async Task<StreamingEndpoint> GetStreamingEndpointAsync(string seName)
        {
            return await AMSclient.StreamingEndpoints.GetAsync(credentialsEntry.ResourceGroup, credentialsEntry.AccountName, seName).ConfigureAwait(false);
        }
    }
}
