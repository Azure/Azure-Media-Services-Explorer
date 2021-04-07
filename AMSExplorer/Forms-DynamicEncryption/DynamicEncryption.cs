//----------------------------------------------------------------------------------------------
//    Copyright 2021 Microsoft Corporation
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

using System;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

namespace AMSExplorer
{
    public class MyTokenClaim
    {
        public string Type { get; set; }
        public string Value { get; set; }
    }

    public enum ExplorerTokenType
    {
        NoToken = 0,
        JWTSym,
        JWTX509,
        JWTOpenID
    }

    internal class DynamicEncryption
    {


        public static byte[] GetRandomBuffer(int size)
        {
            byte[] randomBytes = new byte[size];
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(randomBytes);
            }

            return randomBytes;
        }




        public static PFXCertificate GetCertificateFromFile(bool informuser = false, X509KeyStorageFlags flags = X509KeyStorageFlags.DefaultKeySet)
        {
            X509Certificate2 cert = null;
            string password = string.Empty;


            if (informuser)
            {
                MessageBox.Show(
                    AMSExplorer.Properties.Resources.DynamicEncryption_GetCertificateFromFile_PleaseSelectACertificateFilePFXThatContainsBothPublicAndPrivateKeysPrivateKeyIsNeededToSignTheJWTTokenItIsRecommendedToUseTheSameCertifcateThatTheOneUsedDuringTheSetupOfDynamicEncryptionForThisAsset,
                    AMSExplorer.Properties.Resources.DynamicEncryption_GetCertificateFromFile_CertificateRequired,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                    );
            }

            OpenFileDialog openFileDialogCert = new OpenFileDialog()
            {
                DefaultExt = "PFX",
                Filter = AMSExplorer.Properties.Resources.DynamicEncryption_GetCertificateFromFile_PFXFilesPfxAllFiles
            };

            if (openFileDialogCert.ShowDialog() == DialogResult.OK)
            {

                if (Program.InputBox(AMSExplorer.Properties.Resources.DynamicEncryption_GetCertificateFromFile_PFXPassword, AMSExplorer.Properties.Resources.DynamicEncryption_GetCertificateFromFile_PleaseEnterThePasswordForThePFXFile, ref password, true) == DialogResult.OK)
                {
                    try
                    {
                        cert = new X509Certificate2(openFileDialogCert.FileName, password, flags);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(string.Format(AMSExplorer.Properties.Resources.DynamicEncryption_GetCertificateFromFile_ThereIsAnErrorWhenOpeningTheCertificateFileN0, e.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    if (cert != null)
                    {
                        if (!cert.HasPrivateKey)
                        {
                            MessageBox.Show(AMSExplorer.Properties.Resources.DynamicEncryption_GetCertificateFromFile_TheCertificateDoesNotContainAPrivateKey, AMSExplorer.Properties.Resources.DynamicEncryption_GetCertificateFromFile_NoPrivateKey, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            cert = null;
                        }
                    }
                }
            }
            return new PFXCertificate { Certificate = cert, Password = password };
        }


        public static byte[] HexStringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        public static string ByteArrayToHexString(byte[] bytes)
        {
            return string.Join(string.Empty, Array.ConvertAll(bytes, b => b.ToString("X2")));
        }



        public static byte[] GeneratePlayReadyContentKey(byte[] keySeed, Guid keyId)
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

    public class PFXCertificate
    {
        public string Password { get; set; }
        public X509Certificate2 Certificate { get; set; }
    }
}