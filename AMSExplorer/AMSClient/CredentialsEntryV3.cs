﻿using Microsoft.Azure.Management.Media.Models;
using System;
using System.Security.Cryptography;
using System.Text;

namespace AMSExplorer
{
    public class CredentialsEntryV3 : IEquatable<CredentialsEntryV3>
    {
        public MediaService MediaService;

        public string ADSPClientId;

        //  A contract is used to ignore this property when exporting the entry
        public string EncryptedADSPClientSecret;

        public string AadTenantId;
        public AzureEnvironment Environment;
        public bool PromptUser;
        public bool ManualConfig = false;
        public bool UseSPAuth = false;
        public string Description;

        public CredentialsEntryV3(MediaService mediaService, AzureEnvironment environment, bool promptUser, bool useSPAuth = false, string tenantId = null, bool manualConfig = false, string adSPClientId = null, string clearADSPClientSecret = null)
        {
            MediaService = mediaService;
            Environment = environment;
            UseSPAuth = useSPAuth;
            PromptUser = promptUser;
            ManualConfig = manualConfig;
            AadTenantId = tenantId;
            if (useSPAuth)
            {
                ADSPClientId = adSPClientId;
                ClearADSPClientSecret = clearADSPClientSecret;
            }
        }

        public string AccountName => MediaService.Name;

        public string ResourceGroup
        {
            get
            {
                string[] idParts = MediaService.Id.Split('/');
                return idParts[4];
            }
        }

        public string AzureSubscriptionId
        {
            get
            {
                string[] idParts = MediaService.Id.Split('/');
                return idParts[2];
            }
        }

        //  A contract is used to ignore this property when saveing settings to disk
        public string ClearADSPClientSecret
        {
            get => EncryptedADSPClientSecret != null ? DecryptSecret(EncryptedADSPClientSecret) : null;
            set => EncryptedADSPClientSecret = (value != null) ? EncryptSecret(value) : null;
        }

        public bool Equals(CredentialsEntryV3 other)
        {
            return false;
            /* To implement
                (this.AccountKey ?? "") == (other.AccountKey ?? "")
                && (this.AccountName ?? "") == (other.AccountName ?? "")
                && (this.ADRestAPIEndpoint ?? "") == (other.ADRestAPIEndpoint ?? "")
                && (this.ADTenantDomain ?? "") == (other.ADTenantDomain ?? "")
                && this.UseAADInteract == other.UseAADInteract
                && this.UseAADServicePrincipal == other.UseAADServicePrincipal
                && (this.ADDeploymentName ?? "") == (other.ADDeploymentName ?? "")
                && (this.ADCustomSettings) == (other.ADCustomSettings)
                && this.UseOtherAPI == other.UseOtherAPI
                && this.UsePartnerAPI == other.UsePartnerAPI
                && (this.Description ?? "") == (other.Description ?? "")
                && (this.OtherACSBaseAddress ?? "") == (other.OtherACSBaseAddress ?? "")
                && (this.OtherAPIServer ?? "") == (other.OtherAPIServer ?? "")
                && (this.OtherAzureEndpoint ?? "") == (other.OtherAzureEndpoint ?? "")
                && (this.OtherManagementPortal ?? "") == (other.OtherManagementPortal ?? "")
                && (this.OtherScope ?? "") == (other.OtherScope ?? "")
                && (this.DefaultStorageKey ?? "") == (other.DefaultStorageKey ?? "")
                 ;
                 */
        }

        private string EncryptSecret(string clientSecretClear)
        {
            // Create the original data to be encrypted
            byte[] toEncrypt = UnicodeEncoding.ASCII.GetBytes(clientSecretClear);

            byte[] encryptedSecret = Protect(toEncrypt);

            return Convert.ToBase64String(encryptedSecret);
        }

        private string DecryptSecret(string clientSecretEncrypted)
        {
            // Create the original data to be encrypted (The data length should be a multiple of 16).
            byte[] toDecrypt = Convert.FromBase64String(clientSecretEncrypted);


            // Decrypt the data and store in a byte array.
            byte[] originalData = Unprotect(toDecrypt);
            return UnicodeEncoding.ASCII.GetString(originalData);

        }

        private static readonly byte[] s_aditionalEntropy = { 9, 1, 4, 5, 5 };

        public static byte[] Protect(byte[] data)
        {
            try
            {
                // Encrypt the data using DataProtectionScope.CurrentUser. The result can be decrypted
                //  only by the same current user.
                return ProtectedData.Protect(data, s_aditionalEntropy, DataProtectionScope.CurrentUser);
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("Data was not encrypted. An error occurred.");
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        public static byte[] Unprotect(byte[] data)
        {
            try
            {
                //Decrypt the data using DataProtectionScope.CurrentUser.
                return ProtectedData.Unprotect(data, s_aditionalEntropy, DataProtectionScope.CurrentUser);
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("Data was not decrypted. An error occurred.");
                Console.WriteLine(e.ToString());
                return null;
            }
        }
    }

}
