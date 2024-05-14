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

using AMSClient;
using Azure.ResourceManager;
using Azure.ResourceManager.Media;
using Microsoft.Identity.Client;
using Microsoft.Identity.Client.Desktop;
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
        public MediaServicesAccountResource AMSclient;
        public AuthenticationResult authResult;
        public CredentialsEntryV4 credentialsEntry;
        private Form _form;
        public TokenCredentials credentials;
        public BearerTokenCredential credentialForArmClient;
        public AzureEnvironment environment;
        private readonly string _azureSubscriptionId;
        private readonly IPublicClientApplication _appInteract;
        private readonly IConfidentialClientApplication _appSP;
        private readonly string[] scopes, scopes2, scopes3;
        private bool firstTimeAuth = true;
        private readonly System.Timers.Timer TimerAutoRefreshAuthToken;
        public bool useMKIOConnection = false;


        public AMSClientV3(AzureEnvironment myEnvironment, string azureSubscriptionId, CredentialsEntryV4 myCredentialsEntry, Form form)
        {
            environment = myEnvironment;
            _azureSubscriptionId = azureSubscriptionId;
            credentialsEntry = myCredentialsEntry;
            _form = form;

            if (!credentialsEntry.UseSPAuth)
            {
                _appInteract = PublicClientApplicationBuilder.Create(environment.ClientApplicationId)

                  //.WithAuthority(AzureCloudInstance.AzurePublic, credentialsEntry.AadTenantId)
                  .WithAuthority(environment.AADSettings.AuthenticationEndpoint + string.Format("{0}", credentialsEntry.AadTenantId ?? "organizations"))
                  .WithDefaultRedirectUri()
                   //.WithRedirectUri("http://localhost")
                  .WithWindowsDesktopFeatures(new BrokerOptions(BrokerOptions.OperatingSystems.Windows))
                  .Build();
            }
            else // SP
            {
                _appSP = ConfidentialClientApplicationBuilder.Create(credentialsEntry.ADSPClientId)
                     .WithClientSecret(credentialsEntry.ClearADSPClientSecret)
                     .WithAuthority(environment.AADSettings.AuthenticationEndpoint + string.Format("{0}", credentialsEntry.AadTenantId ?? "organizations"), true)
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


        public async Task<MediaServicesAccountResource> ConnectAndGetNewClientV3Async(Form callerForm = null, bool connectToMKIO = true)
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
                        /*
                        authResult = await _appInteract.AcquireTokenInteractive(scopes)
                             .WithPrompt(credentialsEntry.PromptUser ? Prompt.ForceLogin : Prompt.SelectAccount)
                             .WithCustomWebUi(new EmbeddedBrowserCustomWebUI(callerForm ?? _form))
                             .ExecuteAsync();
                        */

                        if (callerForm != null) // if interactive and not refresh of token
                        {
                            authResult = await _appInteract.AcquireTokenInteractive(scopes)
                                                 .WithAccount(null)  // this already exists in MSAL, but it is more important for WAM
                                                 .WithParentActivityOrWindow(callerForm.Handle) // to be able to parent WAM's windows to your app (optional, but highly recommended; not needed on UWP)
                                                 .ExecuteAsync();
                        }


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

            if (firstTimeAuth && connectToMKIO)
            {
                // form for MK.IO
                MKIOConnection mkioConnectionForm = new(credentialsEntry.MKIOSubscriptionName, credentialsEntry.MKIOClearToken);

                if (mkioConnectionForm.ShowDialog() == DialogResult.OK)
                {
                    useMKIOConnection = true;
                    credentialsEntry.MKIOSubscriptionName = mkioConnectionForm.MKIOSubscriptionName;
                    credentialsEntry.MKIOClearToken = mkioConnectionForm.MKIOToken;
                }
            }

            credentials = new TokenCredentials(authResult.AccessToken, "Bearer");
            credentialForArmClient = new BearerTokenCredential(authResult.AccessToken);

            // new code
            var MediaServiceAccount = MediaServicesAccountResource.CreateResourceIdentifier(
                subscriptionId: _azureSubscriptionId,
                resourceGroupName: credentialsEntry.ResourceGroupName,
                accountName: credentialsEntry.AccountName
                );
            //var credential = new DefaultAzureCredential(includeInteractiveCredentials: true);
            var armClient = new ArmClient(credentialForArmClient);

            var amsClient = armClient.GetMediaServicesAccountResource(MediaServiceAccount);

            AMSclient = await amsClient.GetAsync();


            /*
            // Getting Media Services account...
            AMSclient = new AzureMediaServicesClient(environment.ArmEndpoint, credentials)
            {
                SubscriptionId = _azureSubscriptionId
            };
            */

            if (firstTimeAuth)
            {
                // let's get info on mediaService, specifically to get the location (region)
                // credentialsEntry.MediaService = await AMSclient.Mediaservices.GetAsync(credentialsEntry.ResourceGroup, credentialsEntry.AccountName);

                // let's refresh the token 3 minutes before it expires
                TimerAutoRefreshAuthToken.Interval = (authResult.ExpiresOn.ToUniversalTime() - DateTimeOffset.UtcNow.AddMinutes(3)).TotalMilliseconds;
                TimerAutoRefreshAuthToken.Elapsed += new ElapsedEventHandler(OnTimedEventAuthRefresh);
                TimerAutoRefreshAuthToken.Start();

                firstTimeAuth = false;
            }

            string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            //AMSclient.SetUserAgent("AMSE", version);
            firstTimeAuth = false;
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

        public async Task<MediaAssetResource> GetAssetAsync(string assetName, CancellationToken token = default)
        {
            return await AMSclient.GetMediaAssetAsync(assetName, token).ConfigureAwait(false);
        }

        public async Task<MediaJobResource> GetJobAsync(string transformName, string jobName)
        {
            var t = await GetTransformAsync(transformName);
            return await t.GetMediaJobAsync(jobName).ConfigureAwait(false);
        }

        public async Task<MediaTransformResource> GetTransformAsync(string transformName)
        {
            return await AMSclient.GetMediaTransformAsync(transformName).ConfigureAwait(false);
        }

        public async Task<MediaLiveEventResource> GetLiveEventAsync(string liveEventName)
        {
            return await AMSclient.GetMediaLiveEventAsync(liveEventName).ConfigureAwait(false);
        }

        public async Task<MediaLiveOutputResource> GetLiveOutputAsync(string liveEventName, string liveOutputName)
        {
            var o = await GetLiveEventAsync(liveEventName);
            return await o.GetMediaLiveOutputAsync(liveOutputName).ConfigureAwait(false);
        }

        public async Task<StreamingEndpointResource> GetStreamingEndpointAsync(string seName)
        {
            return await AMSclient.GetStreamingEndpointAsync(seName).ConfigureAwait(false);
        }
    }
}
