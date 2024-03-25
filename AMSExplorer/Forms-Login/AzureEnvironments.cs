namespace AMSExplorer.Forms_Login
{
    public class AzureEnvironments
    {
        public static AzureEnvType[] GetEnvironments()
        {
            return new AzureEnvType[]
            {
                AzureEnvType.Azure,
                AzureEnvType.AzureChina,
                AzureEnvType.AzureUSGovernment,
                AzureEnvType.AzureGermany,
                AzureEnvType.DevTest
            };
        }
    }
}
