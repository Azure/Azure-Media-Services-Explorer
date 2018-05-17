using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSExplorer
{
    public class ConfigWrapper
    {
        private CredentialsEntry _credential;
        private string _subscriptionid;
        private string _resourcegroup;
        private string _AadTenantId;
        private Uri _ArmAadAudience;
        private string _region;
        private Uri _AadEndpoint;
        private Uri _ArmEndpoint;

        public ConfigWrapper(CredentialsEntry credential, string subscriptionid, string resourcegroup, string AadTenantId,Uri ArmAadAudience,string region,Uri AadEndpoint, Uri ArmEndpoint)
        {
            _credential = credential;
            _subscriptionid = subscriptionid;
            _resourcegroup = resourcegroup;
            _AadTenantId = AadTenantId;
            _ArmAadAudience = ArmAadAudience;
            _region = region;
            _AadEndpoint = AadEndpoint;
            _ArmEndpoint = ArmEndpoint;

        }

        public string SubscriptionId
        {
            get { return _subscriptionid; }
        }

        public string ResourceGroup
        {
            get { return _resourcegroup; }
        }

        public string AccountName
        {
            get { return _credential.ReturnAccountName(); }
        }

        public string AadTenantId
        {
            get { return _AadTenantId; }
        }

        public string AadClientId
        {
            get { return _credential.ADSPClientId; }
        }

        public string AadSecret
        {
            get { return _credential.ADSPClientSecret; }
        }

        public Uri ArmAadAudience
        {
            get { return _ArmAadAudience; }
        }

        public Uri AadEndpoint
        {
            get { return _AadEndpoint; }
        }

        public Uri ArmEndpoint
        {
            get { return _ArmEndpoint; }
        }

        public string Region
        {
            get { return _region; }
        }
    }
}
