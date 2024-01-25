using System.Threading.Tasks;

using Azure.ResourceManager;

namespace AMSExplorer.Ravnur
{
    public class RavnurClientFactory
    {
        public static ArmClient GetRavnurArmClient(CredentialsEntryV4 credentialsEntry)
        {
            var ravnurCredentials = new RavnurApiKeyCredentials(
                authorityUri: credentialsEntry.RavnurApiEndpoint,
                subscriptionId: credentialsEntry.SubscriptionId,
                apiKey: credentialsEntry.RavnurClearApiKey);

            var options = new ArmClientOptions
            {
                Environment = new ArmEnvironment(credentialsEntry.RavnurApiEndpoint, credentialsEntry.AccountName),
            };

            return new ArmClient(ravnurCredentials, credentialsEntry.SubscriptionId, options);
        }

        public static async Task<string> GetRavnurAccessToken(CredentialsEntryV4 credentialsEntry)
        {
            return await RavnurAuthHelper.GetAccessToken(
                credentialsEntry.SubscriptionId,
                credentialsEntry.RavnurApiEndpoint,
                credentialsEntry.RavnurClearApiKey);
        }
    }
}
