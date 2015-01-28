//----------------------------------------------------------------------- 
// <copyright file="DynamicEncryption.cs" company="Microsoft">Copyright (c) Microsoft Corporation. All rights reserved.</copyright> 
// <license>
// Azure Media Services Explorer Ver. 3.1
// Licensed under the Apache License, Version 2.0 (the "License"); 
// you may not use this file except in compliance with the License. 
// You may obtain a copy of the License at 
//  
// http://www.apache.org/licenses/LICENSE-2.0 
//  
// Unless required by applicable law or agreed to in writing, software 
// distributed under the License is distributed on an "AS IS" BASIS, 
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
// See the License for the specific language governing permissions and 
// limitations under the License. 
// </license>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MediaServices.Client;
using Microsoft.WindowsAzure.MediaServices.Client.ContentKeyAuthorization;
using Microsoft.WindowsAzure.MediaServices.Client.DynamicEncryption;
using System.Configuration;
using System.IO;
using System.Threading;
using Microsoft.WindowsAzure;
using System.Security.Cryptography;
using System.Runtime.Serialization.Json;
using System.Net;
using System.Xml.Linq;
using System.Web;
using System.Globalization;
using System.Security;

namespace AMSExplorer
{

    class DynamicEncryption
    {
        

        /// <summary>
        /// Configures authorization policy. 
        /// Creates a content key. 
        /// Updates the PlayReady configuration XML file.
        /// </summary>
        /// <returns>The content key.</returns>
        public static IContentKey ConfigureKeyDeliveryServiceForPlayReady(CloudMediaContext _context, Guid keyId, byte[] keyValue, PlayReadyLicenseTemplate licenseTemplate,
            ContentKeyRestrictionType PlayReadyKeyRestriction, string PlayReadyPolicyName)
        {

            var contentKey = _context.ContentKeys.Create(keyId, keyValue, "key test", ContentKeyType.CommonEncryption);

            var restrictions = new List<ContentKeyAuthorizationPolicyRestriction>
                {
                    new ContentKeyAuthorizationPolicyRestriction { Requirements = null, Name = Enum.GetName(typeof(ContentKeyRestrictionType),PlayReadyKeyRestriction), 
                        KeyRestrictionType = (int)PlayReadyKeyRestriction }
                };

            IContentKeyAuthorizationPolicy contentKeyAuthorizationPolicy = _context.
                        ContentKeyAuthorizationPolicies.
                        CreateAsync("Deliver Common Content Key with no restrictions").
                        Result;

            // Configure PlayReady license template.

            PlayReadyLicenseResponseTemplate responseTemplate = new PlayReadyLicenseResponseTemplate();

            responseTemplate.LicenseTemplates.Add(licenseTemplate);

            string newLicenseTemplate = MediaServicesLicenseTemplateSerializer.Serialize(responseTemplate);

            IContentKeyAuthorizationPolicyOption policyOption =
                _context.ContentKeyAuthorizationPolicyOptions.Create(
                PlayReadyPolicyName,
                    ContentKeyDeliveryType.PlayReadyLicense,
                        restrictions, newLicenseTemplate);

            contentKeyAuthorizationPolicy.Options.Add(policyOption);

            // Associate the content key authorization policy with the content key
            contentKey.AuthorizationPolicyId = contentKeyAuthorizationPolicy.Id;
            contentKey = contentKey.UpdateAsync().Result;

            return contentKey;
        }



        static public byte[] GetRandomBuffer(int size)
        {
            byte[] randomBytes = new byte[size];
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(randomBytes);
            }

            return randomBytes;
        }

        static public IContentKey CreateCommonTypeContentKey(IAsset asset, CloudMediaContext _context)
        {
            // Create envelope encryption content key
            Guid keyId = Guid.NewGuid();
            byte[] contentKey = GetRandomBuffer(16);

            IContentKey key = _context.ContentKeys.Create(
                                    keyId,
                                    contentKey,
                                    "ContentKey CENC",
                                    ContentKeyType.CommonEncryption);

            // Associate the key with the asset.
            asset.ContentKeys.Add(key);

            return key;
        }

        static public IContentKey CreateCommonTypeContentKey(IAsset asset, CloudMediaContext _context, Guid keyId, byte[] contentKey)
        {

            IContentKey key = _context.ContentKeys.Create(
                                    keyId,
                                    contentKey,
                                    "ContentKey CENC",
                                    ContentKeyType.CommonEncryption);

            // Associate the key with the asset.
            asset.ContentKeys.Add(key);

            return key;
        }



        static public IContentKey CreateEnvelopeTypeContentKey(IAsset asset)  // with key generated randomly
        {
            // Create envelope encryption content key
            byte[] contentKey = GetRandomBuffer(16);
            return CreateEnvelopeTypeContentKey(asset, contentKey);
        }

        static public IContentKey CreateEnvelopeTypeContentKey(IAsset asset, byte[] contentKey)
        {
            Guid keyId = Guid.NewGuid();

            IContentKey key = asset.GetMediaContext().ContentKeys.Create(
                                    keyId,
                                    contentKey,
                                    "ContentKey Envelope",
                                    ContentKeyType.EnvelopeEncryption);
            // Associate the key with the asset.
            asset.ContentKeys.Add(key);

            return key;
        }




        static public IContentKeyAuthorizationPolicy AddOpenAuthorizationPolicy(IContentKey contentKey, ContentKeyDeliveryType contentkeydeliverytype, string keydeliveryconfig, CloudMediaContext _context)
        {
            // Create ContentKeyAuthorizationPolicy with Open restrictions 
            // and create authorization policy          
            List<ContentKeyAuthorizationPolicyRestriction> restrictions = new List<ContentKeyAuthorizationPolicyRestriction>
    {
        new ContentKeyAuthorizationPolicyRestriction 
        { 
            Name = "Open", 
            KeyRestrictionType = (int)ContentKeyRestrictionType.Open, 
            Requirements = null
        }
    };

            IContentKeyAuthorizationPolicyOption policyOption =
                _context.ContentKeyAuthorizationPolicyOptions.Create(
                "policy",
                contentkeydeliverytype,
                restrictions,
                keydeliveryconfig);

            IContentKeyAuthorizationPolicy contentKeyAuthorizationPolicy = _context.
                                     ContentKeyAuthorizationPolicies.
                                     CreateAsync("Open Authorization Policy").Result;

            contentKeyAuthorizationPolicy.Options.Add(policyOption);

            // Associate the content key authorization policy with the content key.
            contentKey.AuthorizationPolicyId = contentKeyAuthorizationPolicy.Id;
            contentKey = contentKey.UpdateAsync().Result;

            return contentKeyAuthorizationPolicy;
        }




        public static string AddTokenRestrictedAuthorizationPolicyAES(IContentKey contentKey, Uri Audience, Uri Issuer, CloudMediaContext _context)
        {
            string tokenTemplateString = GenerateSWTTokenRequirements(Audience, Issuer);

            IContentKeyAuthorizationPolicy policy = _context.
                                    ContentKeyAuthorizationPolicies.
                                    CreateAsync("HLS token restricted authorization policy").Result;

            List<ContentKeyAuthorizationPolicyRestriction> restrictions =
                    new List<ContentKeyAuthorizationPolicyRestriction>();

            ContentKeyAuthorizationPolicyRestriction restriction =
                    new ContentKeyAuthorizationPolicyRestriction
                    {
                        Name = "Token Authorization Policy",
                        KeyRestrictionType = (int)ContentKeyRestrictionType.TokenRestricted,
                        Requirements = tokenTemplateString
                    };

            restrictions.Add(restriction);

            //You could have multiple options 
            IContentKeyAuthorizationPolicyOption policyOption =
                _context.ContentKeyAuthorizationPolicyOptions.Create(
                    "Token option for HLS",
                    ContentKeyDeliveryType.BaselineHttp,
                    restrictions,
                    null  // no key delivery data is needed for HLS
                    );

            policy.Options.Add(policyOption);

            // Add ContentKeyAutorizationPolicy to ContentKey
            contentKey.AuthorizationPolicyId = policy.Id;
            IContentKey updatedKey = contentKey.UpdateAsync().Result;
            Console.WriteLine("Adding Key to Asset: Key ID is " + updatedKey.Id);

            return tokenTemplateString;
        }

        public static string AddTokenRestrictedAuthorizationPolicyPlayReady(IContentKey contentKey, Uri Audience, Uri Issuer, CloudMediaContext _context, string newLicenseTemplate)
        {
            string tokenTemplateString = GenerateSWTTokenRequirements(Audience, Issuer);

            IContentKeyAuthorizationPolicy policy = _context.
                                    ContentKeyAuthorizationPolicies.
                                    CreateAsync("HLS token restricted authorization policy").Result;

            List<ContentKeyAuthorizationPolicyRestriction> restrictions = new List<ContentKeyAuthorizationPolicyRestriction>
    {
        new ContentKeyAuthorizationPolicyRestriction 
        { 
            Name = "Token Authorization Policy", 
            KeyRestrictionType = (int)ContentKeyRestrictionType.TokenRestricted,
            Requirements = tokenTemplateString, 
        }
    };

            IContentKeyAuthorizationPolicyOption policyOption =
         _context.ContentKeyAuthorizationPolicyOptions.Create("Token option",
             ContentKeyDeliveryType.PlayReadyLicense,
                 restrictions, newLicenseTemplate);

            IContentKeyAuthorizationPolicy contentKeyAuthorizationPolicy = _context.
                        ContentKeyAuthorizationPolicies.
                        CreateAsync("Deliver Common Content Key with no restrictions").
                        Result;

            policy.Options.Add(policyOption);

            // Add ContentKeyAutorizationPolicy to ContentKey
            contentKeyAuthorizationPolicy.Options.Add(policyOption);

            // Associate the content key authorization policy with the content key
            contentKey.AuthorizationPolicyId = contentKeyAuthorizationPolicy.Id;
            contentKey = contentKey.UpdateAsync().Result;

            return tokenTemplateString;
        }


        static private string GenerateSWTTokenRequirements(Uri _sampleAudience, Uri _sampleIssuer)
        {
            TokenRestrictionTemplate template = new TokenRestrictionTemplate();

            template.PrimaryVerificationKey = new SymmetricVerificationKey();
            template.AlternateVerificationKeys.Add(new SymmetricVerificationKey());
            template.Audience = _sampleAudience;
            template.Issuer = _sampleIssuer;
            template.TokenType = TokenType.SWT;
            template.RequiredClaims.Add(TokenClaim.ContentKeyIdentifierClaim);

            return TokenRestrictionTemplateSerializer.Serialize(template);
        }

        static public IAssetDeliveryPolicy CreateAssetDeliveryPolicyAES(IAsset asset, IContentKey key, AssetDeliveryProtocol assetdeliveryprotocol, string name, CloudMediaContext _context)
        {
            Uri keyAcquisitionUri = key.GetKeyDeliveryUrl(ContentKeyDeliveryType.BaselineHttp);

            string envelopeEncryptionIV = Convert.ToBase64String(GetRandomBuffer(16));

            // The following policy configuration specifies: 
            //   key url that will have KID=<Guid> appended to the envelope and
            //   the Initialization Vector (IV) to use for the envelope encryption.
            Dictionary<AssetDeliveryPolicyConfigurationKey, string> assetDeliveryPolicyConfiguration =
                new Dictionary<AssetDeliveryPolicyConfigurationKey, string> 
            {
                {AssetDeliveryPolicyConfigurationKey.EnvelopeKeyAcquisitionUrl, keyAcquisitionUri.ToString()},
                {AssetDeliveryPolicyConfigurationKey.EnvelopeEncryptionIVAsBase64, envelopeEncryptionIV}
            };

            IAssetDeliveryPolicy assetDeliveryPolicy =
                _context.AssetDeliveryPolicies.Create(
                            name,
                            AssetDeliveryPolicyType.DynamicEnvelopeEncryption,
                            assetdeliveryprotocol,
                            assetDeliveryPolicyConfiguration);

            // Add AssetDelivery Policy to the asset
            asset.DeliveryPolicies.Add(assetDeliveryPolicy);
            return assetDeliveryPolicy;
        }

        static public IAssetDeliveryPolicy CreateAssetDeliveryPolicyNoDynEnc(IAsset asset, AssetDeliveryProtocol assetdeliveryprotocol, CloudMediaContext _context)
        {

            IAssetDeliveryPolicy assetDeliveryPolicy =
                _context.AssetDeliveryPolicies.Create(
                            "AssetDeliveryPolicy NoDynEnc",
                            AssetDeliveryPolicyType.NoDynamicEncryption,
                            assetdeliveryprotocol,
                            null); //  no dyn enc then no need for configuration

            // Add AssetDelivery Policy to the asset
            asset.DeliveryPolicies.Add(assetDeliveryPolicy);
            return assetDeliveryPolicy;
        }

        static public IAssetDeliveryPolicy CreateAssetDeliveryPolicyCENC(IAsset asset, IContentKey key, AssetDeliveryProtocol assetdeliveryprotocol, string name, CloudMediaContext _context, Uri acquisitionUrl = null)
        {
            if (acquisitionUrl == null) acquisitionUrl = key.GetKeyDeliveryUrl(ContentKeyDeliveryType.PlayReadyLicense);
            string stringacquisitionUrl = System.Security.SecurityElement.Escape(acquisitionUrl.ToString());

            Dictionary<AssetDeliveryPolicyConfigurationKey, string> assetDeliveryPolicyConfiguration = new Dictionary<AssetDeliveryPolicyConfigurationKey, string>
    {
        {AssetDeliveryPolicyConfigurationKey.PlayReadyLicenseAcquisitionUrl, stringacquisitionUrl},
    };

            var assetDeliveryPolicy = _context.AssetDeliveryPolicies.Create(
                name,
                AssetDeliveryPolicyType.DynamicCommonEncryption,
                assetdeliveryprotocol,
                assetDeliveryPolicyConfiguration);

            // Add AssetDelivery Policy to the asset
            asset.DeliveryPolicies.Add(assetDeliveryPolicy);

            return assetDeliveryPolicy;
        }



        static public string ConfigurePlayReadyLicenseTemplate(PlayReadyLicenseTemplate licenseTemplate)
        {
            // The following code configures PlayReady License Template using .NET classes
            // and returns the XML string.

            PlayReadyLicenseResponseTemplate responseTemplate = new PlayReadyLicenseResponseTemplate();

            responseTemplate.LicenseTemplates.Add(licenseTemplate);

            return MediaServicesLicenseTemplateSerializer.Serialize(responseTemplate);
        }


        static public byte[] GeneratePlayReadyContentKey(byte[] keySeed, Guid keyId)
        {
            const int DRM_AES_KEYSIZE_128 = 16;
            byte[] contentKey = new byte[DRM_AES_KEYSIZE_128];
            //
            // Truncate the key seed to 30 bytes, key seed must be at least 30 bytes long.
            //
            byte[] truncatedKeySeed = new byte[30];
            Array.Copy(keySeed, truncatedKeySeed, truncatedKeySeed.Length);
            //
            // Get the keyId as a byte array
            //
            byte[] keyIdAsBytes = keyId.ToByteArray();
            //
            // Create sha_A_Output buffer. It is the SHA of the truncatedKeySeed and the keyIdAsBytes
            //
            SHA256Managed sha_A = new SHA256Managed();
            sha_A.TransformBlock(truncatedKeySeed, 0, truncatedKeySeed.Length, truncatedKeySeed, 0);
            sha_A.TransformFinalBlock(keyIdAsBytes, 0, keyIdAsBytes.Length);
            byte[] sha_A_Output = sha_A.Hash;
            //
            // Create sha_B_Output buffer. It is the SHA of the truncatedKeySeed, the keyIdAsBytes, and
            // the truncatedKeySeed again.
            //
            SHA256Managed sha_B = new SHA256Managed();
            sha_B.TransformBlock(truncatedKeySeed, 0, truncatedKeySeed.Length, truncatedKeySeed, 0);
            sha_B.TransformBlock(keyIdAsBytes, 0, keyIdAsBytes.Length, keyIdAsBytes, 0);
            sha_B.TransformFinalBlock(truncatedKeySeed, 0, truncatedKeySeed.Length);
            byte[] sha_B_Output = sha_B.Hash;
            //
            // Create sha_C_Output buffer. It is the SHA of the truncatedKeySeed, the keyIdAsBytes,
            // the truncatedKeySeed again, and the keyIdAsBytes again.
            //
            SHA256Managed sha_C = new SHA256Managed();
            sha_C.TransformBlock(truncatedKeySeed, 0, truncatedKeySeed.Length, truncatedKeySeed, 0);
            sha_C.TransformBlock(keyIdAsBytes, 0, keyIdAsBytes.Length, keyIdAsBytes, 0);
            sha_C.TransformBlock(truncatedKeySeed, 0, truncatedKeySeed.Length, truncatedKeySeed, 0);
            sha_C.TransformFinalBlock(keyIdAsBytes, 0, keyIdAsBytes.Length);
            byte[] sha_C_Output = sha_C.Hash;
            for (int i = 0; i < DRM_AES_KEYSIZE_128; i++)
            {
                contentKey[i] = Convert.ToByte(sha_A_Output[i] ^ sha_A_Output[i + DRM_AES_KEYSIZE_128]
                ^ sha_B_Output[i] ^ sha_B_Output[i + DRM_AES_KEYSIZE_128]
                ^ sha_C_Output[i] ^ sha_C_Output[i + DRM_AES_KEYSIZE_128]);
            }
            return contentKey;
        }


    }
}
