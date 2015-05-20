//----------------------------------------------------------------------- 
// <copyright file="ManagementREST.cs" company="Microsoft">Copyright (c) Microsoft Corporation. All rights reserved.</copyright> 
// <license>
// Azure Media Services Explorer Ver. 3.2
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
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Xml;
using System.Xml.Linq;
using System.Runtime.Serialization.Json;
using System.Globalization;


namespace AMSExplorer
{

    class ManagementRESTAPIHelper
    {
        public string Endpoint { get; set; }
        public string CertCertBody { get; set; }
        public string SubscriptionId { get; set; }

        public ManagementRESTAPIHelper(string endpoint, string certBody, string subscriptionID)
        {
            Endpoint = endpoint;
            CertCertBody = certBody;
            SubscriptionId = subscriptionID;
        }

        private X509Certificate2 GetClientCertificate()
        {
            return new X509Certificate2(Convert.FromBase64String(CertCertBody));
        }


        public string CreateMediaServiceAccountUsingXmlContentType(MediaServicesAccount accountInfo)
        {
            var clientCert = GetClientCertificate();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format("{0}/{1}/services/mediaservices/Accounts",
                Endpoint, SubscriptionId));


            // Create the request XML document
            XNamespace ns = "http://schemas.datacontract.org/2004/07/Microsoft.Cloud.Media.Management.ResourceProvider.Models";
            XDocument requestBody = new XDocument(
                new XElement(
                    ns + "AccountCreationRequest",
                    new XElement(ns + "AccountName", accountInfo.AccountName),
                    new XElement(ns + "BlobStorageEndpointUri", accountInfo.BlobStorageEndpointUri),
                    new XElement(ns + "Region", accountInfo.Region),
                    new XElement(ns + "StorageAccountKey", accountInfo.StorageAccountKey),
                    new XElement(ns + "StorageAccountName", accountInfo.StorageAccountName)
                    ));

            XDocument responseBody;
            responseBody = null;
            string requestId = String.Empty;

            request.Method = "POST";
            request.Headers.Add("x-ms-version", "2011-10-01");
            request.ClientCertificates.Add(clientCert);
            request.ContentType = "application/xml";
            request.Accept = "application/atom+xml";


            if (requestBody != null)
            {
                using (Stream requestStream = request.GetRequestStream())
                {
                    using (StreamWriter streamWriter = new StreamWriter(
                        requestStream, System.Text.UTF8Encoding.UTF8))
                    {
                        requestBody.Save(streamWriter, SaveOptions.DisableFormatting);
                    }
                }
            }

            HttpWebResponse response;
            HttpStatusCode statusCode = HttpStatusCode.Unused;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                // GetResponse throws a WebException for 4XX and 5XX status codes
                response = (HttpWebResponse)ex.Response;
            }

            try
            {
                statusCode = response.StatusCode;
                if (response.ContentLength > 0)
                {
                    if (response.ContentType.Contains("application/xml") || response.ContentType.Contains("application/atom+xml"))
                    {
                        using (XmlReader reader = XmlReader.Create(response.GetResponseStream()))
                        {
                            responseBody = XDocument.Load(reader);
                            Console.WriteLine(responseBody.ToString());

                        }
                    }
                    else
                    {
                        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                        {
                            Console.WriteLine(reader.ReadToEnd());
                        }
                    }

                }

                if (response.Headers != null)
                {
                    requestId = response.Headers["x-ms-request-id"];
                }
            }
            finally
            {
                response.Close();
            }

            if (!statusCode.Equals(HttpStatusCode.Created))
            {
                throw new ApplicationException(string.Format(
                    "Call to CreateAccount returned an error. Status Code: {0}",
                    statusCode
                    ));
            }

            return requestId;
        }

        public void AttachStorageAccountToMediaServiceAccount(MediaServicesAccount accountInfo, AttachStorageAccountRequest storageaccount)
        {
            try
            {
                var clientCert = GetClientCertificate();

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format("{0}/{1}/services/mediaservices/Accounts/{2}/StorageAccounts", Endpoint, SubscriptionId, accountInfo.AccountName));
                request.Method = "POST";
                request.ContentType = "application/json; charset=utf-8";
                request.Headers.Add("x-ms-version", "2011-10-01");
                request.Headers.Add("Accept-Encoding: gzip, deflate");
                request.ClientCertificates.Add(clientCert);

                string jsonString;

                using (MemoryStream ms = new MemoryStream())
                {
                    DataContractJsonSerializer serializer
                            = new DataContractJsonSerializer(typeof(AttachStorageAccountRequest));

                    serializer.WriteObject(ms, storageaccount);

                    jsonString = Encoding.Default.GetString(ms.ToArray());
                }

                using (Stream requestStream = request.GetRequestStream())
                {
                    var requestBytes = System.Text.Encoding.ASCII.GetBytes(jsonString);
                    requestStream.Write(requestBytes, 0, requestBytes.Length);
                    requestStream.Close();
                }

                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode == HttpStatusCode.NoContent)
                        Console.WriteLine("The primary key was regenerated.");
                }

            }
            catch (Exception ex)
            { throw ex; }

        }

        public void DeleteAccount(MediaServicesAccount accountInfo)
        {
            var clientCert = GetClientCertificate();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format("{0}/{1}/services/mediaservices/Accounts/{2}",
                Endpoint, SubscriptionId, accountInfo.AccountName));
            request.Method = "DELETE";
            request.Headers.Add("x-ms-version", "2011-10-01");
            request.Accept = "application/json";
            request.ContentType = "application/json";
            request.ClientCertificates.Add(clientCert);

            Console.WriteLine(string.Format("Try to delete account \"{0}\".", accountInfo.AccountName));
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                // "using" used just to dispose the response received.
            }

            try
            {
                AccountDetails detail = GetAccountDetails(accountInfo);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("(404) Not Found"))
                {
                    Console.WriteLine("Account[{0}] has been deleted successfully", accountInfo.AccountName);
                }
                else
                {
                    Console.WriteLine("Deleted Account[{0}] Failed", accountInfo.AccountName);
                }
            }
        }

        public List<SupportedRegion> ListAvailableRegions(MediaServicesAccount accountInfo)
        {
            var clientCert = GetClientCertificate();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format("{0}/{1}/services/mediaservices/SupportedRegions",
                Endpoint, SubscriptionId));

            request.Method = "GET";
            request.Headers.Add("x-ms-version", "2011-10-01");
            request.Accept = "application/json";
            request.ContentType = "application/json";
            request.ClientCertificates.Add(clientCert);

            List<SupportedRegion> regions;

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                var stream1 = response.GetResponseStream();
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(List<SupportedRegion>));
                regions = (List<SupportedRegion>)ser.ReadObject(stream1);

                foreach (var r in regions)
                {
                    Console.WriteLine(r.RegionName);
                }
            }

            return regions;
        }

        public AccountDetails GetAccountDetails(MediaServicesAccount accountInfo)
        {
            var clientCert = GetClientCertificate();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format("{0}/{1}/services/mediaservices/Accounts/{2}",
                Endpoint, SubscriptionId, accountInfo.AccountName));
            request.Method = "GET";
            request.Headers.Add("x-ms-version", "2011-10-01");
            request.Accept = "application/json";
            request.ContentType = "application/json";
            request.ClientCertificates.Add(clientCert);

            AccountDetails accountDetails = null;

            Console.WriteLine(string.Format("Try to get the details of accountName[{0}].", accountInfo.AccountName));
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                var stream1 = response.GetResponseStream();
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(AccountDetails));
                accountDetails = (AccountDetails)ser.ReadObject(stream1);
                Console.WriteLine("Deserialized back:");
                Console.WriteLine(accountDetails.AccountName);
                Console.WriteLine(accountDetails.StorageAccountName);
            }

            Console.WriteLine("Got account detail successfully");
            return accountDetails;
        }

        public void SynchronizeStorageAccountKey(MediaServicesAccount accountInfo)
        {
            var clientCert = GetClientCertificate();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format("{0}/{1}/services/mediaservices/Accounts/{2}/StorageAccounts/{3}/Key",
                Endpoint, SubscriptionId, accountInfo.AccountName, accountInfo.StorageAccountName));
            request.Method = "PUT";
            request.ContentType = "application/json; charset=utf-8";
            request.Headers.Add("x-ms-version", "2011-10-01");
            request.Headers.Add("Accept-Encoding: gzip, deflate");
            request.ClientCertificates.Add(clientCert);


            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write("\"");
                streamWriter.Write(accountInfo.StorageAccountKey);
                streamWriter.Write("\"");
                streamWriter.Flush();
            }

            AccountDetails details = GetAccountDetails(accountInfo);

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                string jsonResponse;
                Stream receiveStream = response.GetResponseStream();
                Encoding encode = Encoding.GetEncoding("utf-8");
                if (receiveStream != null)
                {
                    var readStream = new StreamReader(receiveStream, encode);
                    jsonResponse = readStream.ReadToEnd();
                }
            }
        }

        public List<StorageAccountDetails> ListStorageAccountDetails(MediaServicesAccount accountInfo)
        {
            var clientCert = GetClientCertificate();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format("{0}/{1}/services/mediaservices/Accounts/{2}/StorageAccounts",
                Endpoint, SubscriptionId, accountInfo.AccountName));
            request.Method = "GET";
            request.Headers.Add("x-ms-version", "2011-10-01");
            request.Accept = "application/json";
            request.ContentType = "application/json";
            request.ClientCertificates.Add(clientCert);

            List<StorageAccountDetails> storageAccounts;

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                var stream1 = response.GetResponseStream();
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(List<StorageAccountDetails>));
                storageAccounts = (List<StorageAccountDetails>)ser.ReadObject(stream1);

                foreach (var r in storageAccounts)
                {
                    Console.WriteLine("Account name: {0}", r.StorageAccountName);
                    Console.WriteLine("IsDefault: {0}", r.IsDefault);
                }
            }

            return storageAccounts;
        }


        public List<AzureMediaServicesResource> ListSubscriptionAccounts(MediaServicesAccount accountInfo)
        {
            var clientCert = GetClientCertificate();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format("{0}/{1}/services/mediaservices/Accounts",
                Endpoint, SubscriptionId));
            request.Method = "GET";
            request.Headers.Add("x-ms-version", "2011-10-01");
            request.Accept = "application/json";
            request.ContentType = "application/json";
            request.ClientCertificates.Add(clientCert);

            List<AzureMediaServicesResource> accounts;

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                var stream1 = response.GetResponseStream();
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(List<AzureMediaServicesResource>));
                accounts = (List<AzureMediaServicesResource>)ser.ReadObject(stream1);

                foreach (var r in accounts)
                {
                    Console.WriteLine("Name: {0}", r.Name);
                }
            }

            return accounts;
        }


        public void RegeneratePrimaryAccountKey(MediaServicesAccount accountInfo)
        {
            var clientCert = GetClientCertificate();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format("{0}/{1}/services/mediaservices/Accounts/{2}/AccountKeys/Primary/Regenerate",
                    Endpoint, SubscriptionId, accountInfo.AccountName));
            request.Method = "Post";
            request.ContentType = "application/json; charset=utf-8";
            request.Headers.Add("x-ms-version", "2011-10-01");
            request.Headers.Add("Accept-Encoding: gzip, deflate");
            request.Accept = "application/json";
            request.ClientCertificates.Add(clientCert);

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write("\"");
                streamWriter.Write(accountInfo.AccountName);
                streamWriter.Write("\"");
                streamWriter.Flush();
            }

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                if (response.StatusCode == HttpStatusCode.NoContent)
                    Console.WriteLine("The primary key was regenerated.");
            }
        }

        public void RegenerateSecondaryAccountKey(MediaServicesAccount accountInfo)
        {
            var clientCert = GetClientCertificate();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format("{0}/{1}/services/mediaservices/Accounts/{2}/AccountKeys/Secondary/Regenerate",
                Endpoint, SubscriptionId, accountInfo.AccountName));
            request.Method = "Post";
            request.ContentType = "application/json; charset=utf-8";
            request.Headers.Add("x-ms-version", "2011-10-01");
            request.Headers.Add("Accept-Encoding: gzip, deflate");
            request.Accept = "application/json";
            request.ClientCertificates.Add(clientCert);

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write("\"");
                streamWriter.Write(accountInfo.AccountName);
                streamWriter.Write("\"");
                streamWriter.Flush();
            }

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                if (response.StatusCode == HttpStatusCode.NoContent)
                    Console.WriteLine("The secondary key was regenerated.");
            }
        }
    }
    #region SerializationClasses

    public class AccountCreationResult
    {
        public Guid AccountId { get; set; }
        public string AccountName { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string Subscription { get; set; }
    }


    public class AttachStorageAccountRequest
    {
        public string StorageAccountName { get; set; }
        public string StorageAccountKey { get; set; }
        public string BlobStorageEndpointUri { get; set; }
    }

    public class AzureMediaServicesResource
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string State { get; set; }
        public Uri SelfLink { get; set; }
        public Uri ParentLink { get; set; }
        public Guid AccountId { get; set; }
    }

    public class SupportedRegion
    {
        public string RegionName { get; set; }
    }

    public class StorageAccountDetails
    {
        public string StorageAccountName { get; set; }
        public string BlobStorageEndPoint { get; set; }
        public bool IsDefault { get; set; }
    }

    public class AccountDetails
    {
        public string AccountName { get; set; }
        public string AccountKey { get; set; }
        public AccountKeys AccountKeys { get; set; }
        public string StorageAccountName { get; set; }
        public string AccountRegion { get; set; }
    }

    public class AccountKeys
    {
        public string Primary { get; set; }
        public string Secondary { get; set; }
    }

    public class MediaServicesAccount
    {
        public string AccountName { get; set; }
        public string Region { get; set; }
        public string StorageAccountName { get; set; }
        public string StorageAccountKey { get; set; }
        public string BlobStorageEndpointUri { get; set; }
    }


    public class ServiceQuotaUpdateRequest
    {
        public string ServiceType { get; set; }
        public int RequestedUnits { get; set; }
    }
    #endregion

}
