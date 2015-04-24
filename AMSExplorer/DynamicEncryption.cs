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
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.IdentityModel.Tokens;
using System.Windows.Forms;



namespace AMSExplorer
{
    public class MyTokenClaim
    {
        public string Type { get; set; }
        public string Value { get; set; }
    }

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
            asset.Update();

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




        static public IContentKeyAuthorizationPolicyOption AddOpenAuthorizationPolicyOption(IContentKey contentKey, ContentKeyDeliveryType contentkeydeliverytype, string keydeliveryconfig, CloudMediaContext _context)
        {
            // Create ContentKeyAuthorizationPolicy with Open restrictions 
            // and create authorization policy          
            List<ContentKeyAuthorizationPolicyRestriction> restrictions = new List<ContentKeyAuthorizationPolicyRestriction>
    {
        new ContentKeyAuthorizationPolicyRestriction 
        { 
            Name = "Open Authorization Policy", 
            KeyRestrictionType = (int)ContentKeyRestrictionType.Open, 
            Requirements = null
        }
    };

            IContentKeyAuthorizationPolicyOption policyOption =
                _context.ContentKeyAuthorizationPolicyOptions.Create(
                "Open option",
                contentkeydeliverytype,
                restrictions,
                keydeliveryconfig);

            return policyOption;
        }




        public static IContentKeyAuthorizationPolicyOption AddTokenRestrictedAuthorizationPolicyAES(IContentKey contentKey, Uri Audience, Uri Issuer, IList<TokenClaim> tokenclaimslist, bool AddContentKeyIdentifierClaim, TokenType tokentype, bool IsJWTKeySymmetric, TokenVerificationKey mytokenverificationkey, CloudMediaContext _context)
        {
            string tokenTemplateString = GenerateTokenRequirements(tokentype, Audience, Issuer, tokenclaimslist, AddContentKeyIdentifierClaim, mytokenverificationkey);

            string tname = ((IsJWTKeySymmetric) ? "Sym " : "Asym ") + ((tokentype == TokenType.SWT) ? "SWT " : "JWT ");

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
                    "Token option",
                    ContentKeyDeliveryType.BaselineHttp,
                    restrictions,
                    null  // no key delivery data is needed for HLS
                    );

            return policyOption;

        }

        public static IContentKeyAuthorizationPolicyOption AddTokenRestrictedAuthorizationPolicyPlayReady(IContentKey contentKey, Uri Audience, Uri Issuer, IList<TokenClaim> tokenclaimslist, bool AddContentKeyIdentifierClaim, TokenType tokentype, bool IsJWTKeySymmetric, TokenVerificationKey mytokenverificationkey, CloudMediaContext _context, string newLicenseTemplate)
        {
            string tokenTemplateString = GenerateTokenRequirements(tokentype, Audience, Issuer, tokenclaimslist, AddContentKeyIdentifierClaim, mytokenverificationkey);

            string tname = ((IsJWTKeySymmetric) ? "Sym " : "Asym ") + ((tokentype == TokenType.SWT) ? "SWT " : " JWT ");

            List<ContentKeyAuthorizationPolicyRestriction> restrictions = new List<ContentKeyAuthorizationPolicyRestriction>
    {
        new ContentKeyAuthorizationPolicyRestriction 
        { 
            Name = tname+ "Token Authorization Policy", 
            KeyRestrictionType = (int)ContentKeyRestrictionType.TokenRestricted,
            Requirements = tokenTemplateString, 
        }
    };

            IContentKeyAuthorizationPolicyOption policyOption =
         _context.ContentKeyAuthorizationPolicyOptions.Create(tname + "Token option",
             ContentKeyDeliveryType.PlayReadyLicense,
                 restrictions, newLicenseTemplate);

            IContentKeyAuthorizationPolicy contentKeyAuthorizationPolicy = _context.
                        ContentKeyAuthorizationPolicies.
                        CreateAsync("Deliver Common Content Key with no restrictions").
                        Result;


            return policyOption;
        }


        static private string GenerateTokenRequirements(TokenType mytokentype, Uri _sampleAudience, Uri _sampleIssuer, IList<TokenClaim> tokenclaimslist, bool AddContentKeyIdentifierClaim, TokenVerificationKey mytokenverificationkey)
        {
            TokenRestrictionTemplate TokenrestrictionTemplate = new TokenRestrictionTemplate(mytokentype);
            TokenrestrictionTemplate.PrimaryVerificationKey = mytokenverificationkey;
            TokenrestrictionTemplate.Audience = _sampleAudience.AbsoluteUri;
            TokenrestrictionTemplate.Issuer = _sampleIssuer.AbsoluteUri;
            if (AddContentKeyIdentifierClaim) TokenrestrictionTemplate.RequiredClaims.Add(TokenClaim.ContentKeyIdentifierClaim);
            foreach (var t in tokenclaimslist)
            {
                TokenrestrictionTemplate.RequiredClaims.Add(t);
            }
            return TokenRestrictionTemplateSerializer.Serialize(TokenrestrictionTemplate);
        }

        static public X509Certificate2 GetCertificateFromFile(bool informuser = false)
        {
            X509Certificate2 cert = null;

            if (informuser)
            {
                MessageBox.Show("Please select a certificate file (.PFX) that contains both public and private keys. Private key is needed to sign the JWT token. It is recommended to use the same certifcate that the one used during the setup of dynamic encryption for this asset.", "Certificate required", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            OpenFileDialog openFileDialogCert = new OpenFileDialog()
            {
                DefaultExt = "PFX",
                Filter = "PFX files|*.pfx|All files|*.*"
            };

            if (openFileDialogCert.ShowDialog() == DialogResult.OK)
            {
                string password = string.Empty;

                if (Program.InputBox("PFX Password", "Please enter the password for the PFX file :", ref password) == DialogResult.OK)
                {
                    try
                    {
                        cert = new X509Certificate2(openFileDialogCert.FileName, password);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(string.Format("There is an error when opening the certificate file.\n{0}", e.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    if (cert != null)
                    {
                        if (!cert.HasPrivateKey)
                        {
                            MessageBox.Show("The certificate does not contain a private key.", "No private key", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            cert = null;
                        }
                    }
                }
            }
            return cert;
        }

        public class TokenResult
        {
            public string TokenString { get; set; }
            public TokenType TokenType { get; set; }
            public bool IsTokenKeySymmetric { get; set; }
            public ContentKeyType ContentKeyType { get; set; }
        }

        public static bool IsAssetHasAuthorizationPolicyWithToken(IAsset MyAsset, CloudMediaContext _context)
        {
            var query = from key in MyAsset.ContentKeys
                        join autpol in _context.ContentKeyAuthorizationPolicies on key.AuthorizationPolicyId equals autpol.Id
                        select new { aupolid = autpol.Id };



            foreach (var key in query)
            {
                var queryoptions = _context.ContentKeyAuthorizationPolicies.Where(a => a.Id == key.aupolid).FirstOrDefault().Options;

                foreach (var option in queryoptions)
                {
                    if (option.Restrictions.FirstOrDefault().KeyRestrictionType == (int)ContentKeyRestrictionType.TokenRestricted)
                    {
                        return true;
                    }
                }
            }
            return false;
        }


        public static TokenResult GetTestToken(IAsset MyAsset, CloudMediaContext _context, ContentKeyType? keytype = null, SigningCredentials signingcredentials = null, string optionid = null, bool displayUI = false)
        {

            TokenResult MyResult = new TokenResult();

            /// WITH UI
            if (displayUI)
            {
                CreateTestToken form = new CreateTestToken(MyAsset, _context, keytype, optionid) { StartDate = DateTime.Now.AddMinutes(-5), EndDate = DateTime.Now.AddMinutes(Properties.Settings.Default.DefaultTokenDuration) };
                if (form.ShowDialog() == DialogResult.OK)
                {

                    if (form.GetOption != null)
                    {
                        string tokenTemplateString = form.GetOption.Restrictions.FirstOrDefault().Requirements;
                        if (!string.IsNullOrEmpty(tokenTemplateString))
                        {
                            Guid rawkey = EncryptionUtils.GetKeyIdAsGuid(form.GetContentKeyFromSelectedOption.Id);
                            TokenRestrictionTemplate tokenTemplate = TokenRestrictionTemplateSerializer.Deserialize(tokenTemplateString);

                            MyResult.TokenType = tokenTemplate.TokenType;
                            MyResult.IsTokenKeySymmetric = (tokenTemplate.PrimaryVerificationKey.GetType() == typeof(SymmetricVerificationKey));
                            MyResult.ContentKeyType = form.GetContentKeyFromSelectedOption.ContentKeyType;

                            if (tokenTemplate.TokenType == TokenType.SWT) //SWT
                            {
                                MyResult.TokenString = TokenRestrictionTemplateSerializer.GenerateTestToken(tokenTemplate, null, rawkey, form.EndDate);

                            }
                            else // JWT
                            {
                                IList<Claim> myclaims = null;
                                myclaims = form.GetTokenRequiredClaims;
                                if (form.PutContentKeyIdentifier)
                                    myclaims.Add(new Claim(TokenClaim.ContentKeyIdentifierClaimType, rawkey.ToString()));

                                if (tokenTemplate.PrimaryVerificationKey.GetType() == typeof(SymmetricVerificationKey))
                                {
                                    InMemorySymmetricSecurityKey tokenSigningKey = new InMemorySymmetricSecurityKey((tokenTemplate.PrimaryVerificationKey as SymmetricVerificationKey).KeyValue);
                                    signingcredentials = new SigningCredentials(tokenSigningKey, SecurityAlgorithms.HmacSha256Signature, SecurityAlgorithms.Sha256Digest);
                                }
                                else if (tokenTemplate.PrimaryVerificationKey.GetType() == typeof(X509CertTokenVerificationKey))
                                {
                                    X509Certificate2 cert = form.GetX509Certificate;
                                    if (cert != null) signingcredentials = new X509SigningCredentials(cert);
                                }
                                JwtSecurityToken token = new JwtSecurityToken(issuer: form.GetIssuerUri, audience: form.GetAudienceUri, notBefore: form.StartDate, expires: form.EndDate, signingCredentials: signingcredentials, claims: myclaims);
                                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                                MyResult.TokenString = handler.WriteToken(token);
                            }
                        }
                    }
                }
            }
            /////////////////////////////// NO UI
            else if (keytype != null)
            {

                IContentKey key = MyAsset.ContentKeys.Where(k => k.ContentKeyType == keytype).FirstOrDefault();
                if (key != null && key.AuthorizationPolicyId != null)
                {
                    IContentKeyAuthorizationPolicy policy = _context.ContentKeyAuthorizationPolicies.Where(p => p.Id == key.AuthorizationPolicyId).FirstOrDefault();
                    if (policy != null)
                    {
                        IContentKeyAuthorizationPolicyOption option = null;
                        if (optionid == null) // user does not want a specific option
                        {
                            option = policy.Options.Where(o => (ContentKeyRestrictionType)o.Restrictions.FirstOrDefault().KeyRestrictionType == ContentKeyRestrictionType.TokenRestricted).FirstOrDefault();
                        }
                        else
                        {
                            option = policy.Options.Where(o => o.Id == optionid).FirstOrDefault(); // user wants a token for a specific option
                        }

                        if (option != null) // && option.Restrictions.FirstOrDefault() != null && option.Restrictions.FirstOrDefault().KeyRestrictionType == (int)ContentKeyRestrictionType.TokenRestricted)
                        {
                            string tokenTemplateString = option.Restrictions.FirstOrDefault().Requirements;
                            if (!string.IsNullOrEmpty(tokenTemplateString))
                            {
                                Guid rawkey = EncryptionUtils.GetKeyIdAsGuid(key.Id);
                                TokenRestrictionTemplate tokenTemplate = TokenRestrictionTemplateSerializer.Deserialize(tokenTemplateString);

                                MyResult.TokenType = tokenTemplate.TokenType;
                                MyResult.IsTokenKeySymmetric = (tokenTemplate.PrimaryVerificationKey.GetType() == typeof(SymmetricVerificationKey));
                                MyResult.ContentKeyType = (ContentKeyType)keytype;

                                if (tokenTemplate.TokenType == TokenType.SWT) //SWT
                                {
                                    MyResult.TokenString = TokenRestrictionTemplateSerializer.GenerateTestToken(tokenTemplate, null, rawkey, DateTime.Now.AddMinutes(Properties.Settings.Default.DefaultTokenDuration));
                                }
                                else // JWT
                                {
                                    List<Claim> myclaims = null;
                                    myclaims = new List<Claim>();
                                    myclaims.Add(new Claim(TokenClaim.ContentKeyIdentifierClaimType, rawkey.ToString()));

                                    if (tokenTemplate.PrimaryVerificationKey.GetType() == typeof(SymmetricVerificationKey))
                                    {
                                        InMemorySymmetricSecurityKey tokenSigningKey = new InMemorySymmetricSecurityKey((tokenTemplate.PrimaryVerificationKey as SymmetricVerificationKey).KeyValue);
                                        signingcredentials = new SigningCredentials(tokenSigningKey, SecurityAlgorithms.HmacSha256Signature, SecurityAlgorithms.Sha256Digest);
                                    }
                                    else if (tokenTemplate.PrimaryVerificationKey.GetType() == typeof(X509CertTokenVerificationKey))
                                    {
                                        if (signingcredentials == null)
                                        {
                                            X509Certificate2 cert = DynamicEncryption.GetCertificateFromFile(true);
                                            if (cert != null) signingcredentials = new X509SigningCredentials(cert);
                                        }
                                    }
                                    JwtSecurityToken token = new JwtSecurityToken(issuer: tokenTemplate.Issuer, audience: tokenTemplate.Audience, notBefore: DateTime.Now.AddMinutes(-5), expires: DateTime.Now.AddMinutes(Properties.Settings.Default.DefaultTokenDuration), signingCredentials: signingcredentials, claims: myclaims);
                                    JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                                    MyResult.TokenString = handler.WriteToken(token);
                                }
                            }
                        }
                    }
                }
            }
            return MyResult;
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

        static public IAssetDeliveryPolicy CreateAssetDeliveryPolicyCENC(IAsset asset, IContentKey key, AssetDeliveryProtocol assetdeliveryprotocol, string name, CloudMediaContext _context, Uri acquisitionUrl = null, bool EncodeLAURLForSilverlight = false, string CustomAttributes = null)
        {
            string stringacquisitionUrl;
            if (EncodeLAURLForSilverlight && acquisitionUrl != null)
            {
                stringacquisitionUrl = acquisitionUrl.ToString().Replace("&", "%26");
            }
            else
            {
                if (acquisitionUrl == null) acquisitionUrl = key.GetKeyDeliveryUrl(ContentKeyDeliveryType.PlayReadyLicense);

                stringacquisitionUrl = System.Security.SecurityElement.Escape(acquisitionUrl.ToString());
            }
            Dictionary<AssetDeliveryPolicyConfigurationKey, string> assetDeliveryPolicyConfiguration = new Dictionary<AssetDeliveryPolicyConfigurationKey, string>
    {
        {AssetDeliveryPolicyConfigurationKey.PlayReadyLicenseAcquisitionUrl, stringacquisitionUrl},
         };
            if (CustomAttributes != null) // let's add custom attributes
            {
                assetDeliveryPolicyConfiguration.Add(AssetDeliveryPolicyConfigurationKey.PlayReadyCustomAttributes, CustomAttributes);
            }

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

        public static byte[] HexStringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        static public string ByteArrayToHexString(byte[] bytes)
        {
            return string.Join(string.Empty, Array.ConvertAll(bytes, b => b.ToString("X2")));
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
