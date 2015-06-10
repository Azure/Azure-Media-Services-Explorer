//----------------------------------------------------------------------- 
// <copyright file="DynManifestREST.cs" company="Microsoft">Copyright (c) Microsoft Corporation. All rights reserved.</copyright> 
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
using Microsoft.WindowsAzure.MediaServices.Client;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Xml;
using System.Xml.Linq;
using System.Runtime.Serialization.Json;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Web;


namespace AMSExplorer
{

    public class AcsToken
    {
        public string token_type
        {
            get;
            set;
        }

        public string access_token
        {
            get;
            set;
        }

        public int expires_in
        {
            get;
            set;
        }

        public string scope
        {
            get;
            set;
        }
    }


    public class MediaServiceContextForDynManifest
    {
        private string acsEndpoint = "https://wamsprodglobal001acs.accesscontrol.windows.net";

        private string acsRequestBodyFormat = "grant_type=client_credentials&client_id={0}&client_secret={1}&scope={2}";

        private string scope = "urn:WindowsAzureMediaServices";

        private string _accountName;

        private string _accountKey;

        private string _accessToken;

        private DateTime _accessTokenExpiry;

        private string _wamsEndpoint = "https://media.windows.net/";

        /// <summary>
        /// Creates a new instance of <see cref="MediaServiceContextForDynManifest"/>
        /// </summary>
        /// <param name="accountName">
        /// Media service account name.
        /// </param>
        /// <param name="accountKey">
        /// Media service account key.
        /// </param>
        public MediaServiceContextForDynManifest(CredentialsEntry credentials)
        {
            this._accountName = credentials.AccountName;
            this._accountKey = credentials.AccountKey;

            if (credentials.UsePartnerAPI == true.ToString())
            {
                _wamsEndpoint = CredentialsEntry.PartnerAPIServer;
                scope = CredentialsEntry.PartnerScope;
                acsEndpoint = CredentialsEntry.PartnerACSBaseAddress;
            }
            else if (credentials.UseOtherAPI == true.ToString())
            {
                _wamsEndpoint = credentials.OtherAPIServer;
                scope = credentials.OtherScope;
                acsEndpoint = credentials.OtherACSBaseAddress;
            }
            else
            {
                // Global Azure

            }
        }

        /// <summary>
        /// Gets the access token. If access token is not yet fetched or the access token has expired,
        /// it gets a new access token.
        /// </summary>
        public string AccessToken
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_accessToken) || _accessTokenExpiry < DateTime.UtcNow)
                {
                    var tuple = FetchAccessToken();
                    _accessToken = tuple.Item1;
                    _accessTokenExpiry = tuple.Item2;
                }
                return _accessToken;
            }
        }

        /// <summary>
        /// Gets the endpoint for making REST API calls.
        /// </summary>
        public string WamsEndpoint
        {
            get
            {
                return _wamsEndpoint;
            }

        }

        /// <summary>
        /// This function makes the web request and gets the access token.
        /// </summary>
        /// <returns>
        /// <see cref="System.Tuple"/> containing 2 items - 
        /// 1. The access token. 
        /// 2. Token expiry date/time.
        /// </returns>
        private Tuple<string, DateTime> FetchAccessToken()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(acsEndpoint + "/v2/OAuth2-13");
            request.Method = HttpVerbs.Post;
            string requestBody = string.Format(CultureInfo.InvariantCulture, acsRequestBodyFormat, _accountName, HttpUtility.UrlEncode(_accountKey), HttpUtility.UrlEncode(scope));
            request.ContentLength = Encoding.UTF8.GetByteCount(requestBody);
            request.ContentType = "application/x-www-form-urlencoded";
            using (StreamWriter streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(requestBody);
            }
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (StreamReader streamReader = new StreamReader(response.GetResponseStream(), true))
                {
                    var returnBody = streamReader.ReadToEnd();
                    var acsToken = JsonConvert.DeserializeObject<AcsToken>(returnBody);
                    return new Tuple<string, DateTime>(acsToken.access_token, DateTime.UtcNow.AddSeconds(acsToken.expires_in));
                }
            }
        }

        /// <summary>
        /// This function checks if we need to redirect all WAMS requests.
        /// </summary>
        public void CheckForRedirection()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(WamsEndpoint);
            request.AllowAutoRedirect = false;
            request.Headers.Add(RequestHeaders.XMsVersion, RequestHeaderValues.XMsVersion);
            request.Headers.Add(RequestHeaders.Authorization, string.Format(CultureInfo.InvariantCulture, RequestHeaderValues.Authorization, AccessToken));
            request.Method = HttpVerbs.Get;

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                if (response.StatusCode == HttpStatusCode.Moved || response.StatusCode == HttpStatusCode.MovedPermanently)
                {
                    string newLocation = response.Headers["Location"];
                    if (!newLocation.Equals(_wamsEndpoint))
                    {
                        _wamsEndpoint = newLocation;
                        _accessToken = string.Empty;//So that we can force to get a new access token.
                        _accessTokenExpiry = DateTime.MinValue;
                    }
                }
            }
        }

        public List<Filter> ListFilters()
        {
            List<Filter> returnFilters = new List<Filter>();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format(CultureInfo.InvariantCulture, "{0}Filters", WamsEndpoint));
            request.Method = HttpVerbs.Get;
            request.ContentType = RequestContentType.Json;
            request.Accept = RequestContentType.Json;
            request.Headers.Add(RequestHeaders.XMsVersion, RequestHeaderValues.XMsVersion);
            request.Headers.Add(RequestHeaders.Authorization, string.Format(CultureInfo.InvariantCulture, RequestHeaderValues.Authorization, AccessToken));
            request.Headers.Add(RequestHeaders.DataServiceVersion, RequestHeaderValues.DataServiceVersion);
            request.Headers.Add(RequestHeaders.MaxDataServiceVersion, RequestHeaderValues.MaxDataServiceVersion);

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (StreamReader streamReader = new StreamReader(response.GetResponseStream(), true))
                {
                    var returnBody = streamReader.ReadToEnd();

                    JObject responseJsonObject = JObject.Parse(returnBody);
                    var value = responseJsonObject["value"];

                    var serializer = new JavaScriptSerializer();
                    returnFilters = (List<Filter>)serializer.Deserialize(value.ToString(), typeof(List<Filter>));
                    returnFilters.ForEach(f => f.SetContext(this));
                }
            }

            return returnFilters;
        }

        public List<AssetFilter> ListAssetFilters(IAsset asset)
        {
            List<AssetFilter> returnFilters = new List<AssetFilter>();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format(CultureInfo.InvariantCulture, "{0}Assets('{1}')/AssetFilters", WamsEndpoint, asset.Id));
            request.Method = HttpVerbs.Get;
            request.ContentType = RequestContentType.Json;
            request.Accept = RequestContentType.Json;
            request.Headers.Add(RequestHeaders.XMsVersion, RequestHeaderValues.XMsVersion);
            request.Headers.Add(RequestHeaders.Authorization, string.Format(CultureInfo.InvariantCulture, RequestHeaderValues.Authorization, AccessToken));
            request.Headers.Add(RequestHeaders.DataServiceVersion, RequestHeaderValues.DataServiceVersion);
            request.Headers.Add(RequestHeaders.MaxDataServiceVersion, RequestHeaderValues.MaxDataServiceVersion);

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (StreamReader streamReader = new StreamReader(response.GetResponseStream(), true))
                {
                    var returnBody = streamReader.ReadToEnd();

                    JObject responseJsonObject = JObject.Parse(returnBody);
                    var value = responseJsonObject["value"];

                    var serializer = new JavaScriptSerializer();
                    returnFilters = (List<AssetFilter>)serializer.Deserialize(value.ToString(), typeof(List<AssetFilter>));
                    returnFilters.ForEach(f => f.SetContext(this));
                }
            }

            return returnFilters;
        }

        public Filter GetFilter(string filterName)
        {
            Filter returnFilter = new Filter();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format(CultureInfo.InvariantCulture, "{0}Filters('{1}')", WamsEndpoint, filterName));
            request.Method = HttpVerbs.Get;
            request.ContentType = RequestContentType.Json;
            request.Accept = RequestContentType.Json;
            request.Headers.Add(RequestHeaders.XMsVersion, RequestHeaderValues.XMsVersion);
            request.Headers.Add(RequestHeaders.Authorization, string.Format(CultureInfo.InvariantCulture, RequestHeaderValues.Authorization, AccessToken));
            request.Headers.Add(RequestHeaders.DataServiceVersion, RequestHeaderValues.DataServiceVersion);
            request.Headers.Add(RequestHeaders.MaxDataServiceVersion, RequestHeaderValues.MaxDataServiceVersion);

            try
            {
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    using (StreamReader streamReader = new StreamReader(response.GetResponseStream(), true))
                    {
                        var returnBody = streamReader.ReadToEnd();

                        JObject responseJsonObject = JObject.Parse(returnBody);
                        // var value = responseJsonObject["Element"];

                        var serializer = new JavaScriptSerializer();
                        FilterWithMetadata returnFilterm = (FilterWithMetadata)serializer.Deserialize(responseJsonObject.ToString(), typeof(FilterWithMetadata));

                        returnFilter.Name = returnFilterm.Name;
                        returnFilter.PresentationTimeRange = returnFilterm.PresentationTimeRange;
                        returnFilter.Tracks = returnFilterm.Tracks;
                        returnFilter.SetContext(this);
                    }
                }
            }
            catch
            {
                returnFilter = null;
            }


            return returnFilter;
        }



        public AssetFilter GetAssetFilter(string filterId)
        {
            AssetFilter returnFilter = new AssetFilter();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format(CultureInfo.InvariantCulture, "{0}AssetFilters('{1}')", WamsEndpoint, HttpUtility.UrlEncode(filterId)));
            request.Method = HttpVerbs.Get;
            request.ContentType = RequestContentType.Json;
            request.Accept = RequestContentType.Json;
            request.Headers.Add(RequestHeaders.XMsVersion, RequestHeaderValues.XMsVersion);
            request.Headers.Add(RequestHeaders.Authorization, string.Format(CultureInfo.InvariantCulture, RequestHeaderValues.Authorization, AccessToken));
            request.Headers.Add(RequestHeaders.DataServiceVersion, RequestHeaderValues.DataServiceVersion);
            request.Headers.Add(RequestHeaders.MaxDataServiceVersion, RequestHeaderValues.MaxDataServiceVersion);
            request.Headers.Add(RequestHeaders.XMsClientRequestId, "00000000");


            try
            {
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    using (StreamReader streamReader = new StreamReader(response.GetResponseStream(), true))
                    {
                        var returnBody = streamReader.ReadToEnd();

                        JObject responseJsonObject = JObject.Parse(returnBody);
                        // var value = responseJsonObject["Element"];

                        var serializer = new JavaScriptSerializer();
                        AssetFilterWithMetadata returnFilterm = (AssetFilterWithMetadata)serializer.Deserialize(responseJsonObject.ToString(), typeof(AssetFilterWithMetadata));

                        returnFilter.Name = returnFilterm.Name;
                        returnFilter.PresentationTimeRange = returnFilterm.PresentationTimeRange;
                        returnFilter.Tracks = returnFilterm.Tracks;
                        returnFilter.Id = returnFilterm.Id;
                        returnFilter.ParentAssetId = returnFilterm.ParentAssetId;
                        returnFilter.SetContext(this);
                    }
                }
            }
            catch (Exception e)
            {
                returnFilter = null;
            }
            return returnFilter;
        }


    }



    internal static class RequestHeaderValues
    {
        /// <summary>
        /// DataServiceVersion request header (3.0)
        /// </summary>
        internal const string DataServiceVersion = "3.0";

        /// <summary>
        /// MaxDataServiceVersion request header (3.0)
        /// </summary>
        internal const string MaxDataServiceVersion = "3.0";

        /// <summary>
        /// x-ms-version request header (2.0)
        /// </summary>
        internal const string XMsVersion = "2.11";

        /// <summary>
        /// Authorization request header format
        /// </summary>
        internal const string Authorization = "Bearer {0}";



        /// <summary>
        /// Authorization request header format
        /// </summary>
        internal const string ZeroID = "00000000-0000-0000-0000-000000000000";


    }

    /// <summary>
    /// Request header names.
    /// </summary>
    internal static class RequestHeaders
    {
        /// <summary>
        /// DataServiceVersion request header
        /// </summary>
        internal const string DataServiceVersion = "DataServiceVersion";

        /// <summary>
        /// MaxDataServiceVersion request header
        /// </summary>
        internal const string MaxDataServiceVersion = "MaxDataServiceVersion";

        /// <summary>
        /// x-ms-version request header
        /// </summary>
        internal const string XMsVersion = "x-ms-version";

        /// <summary>
        /// Authorization request header
        /// </summary>
        internal const string Authorization = "Authorization";

        /// <summary>
        /// x-ms-version request header
        /// </summary>
        internal const string XMsClientRequestId = "x-ms-client-request-id";

    }

    /// <summary>
    /// HTTP Verbs
    /// </summary>
    internal static class HttpVerbs
    {
        /// <summary>
        /// POST HTTP verb
        /// </summary>
        internal const string Post = "POST";

        /// <summary>
        /// GET HTTP verb
        /// </summary>
        internal const string Get = "GET";

        /// <summary>
        /// MERGE HTTP verb
        /// </summary>
        internal const string Merge = "MERGE";

        /// <summary>
        /// DELETE HTTP verb
        /// </summary>
        internal const string Delete = "DELETE";
    }

    internal static class RequestContentType
    {
        internal const string Json = "application/json";

        internal const string Atom = "application/atom+xml";
    }

    [DataContract]
    public class IFilterPresentationTimeRange
    {
        [DataMember(Name = "StartTimestamp", IsRequired = false, EmitDefaultValue = false)]
        public string StartTimestamp
        {
            get;
            set;
        }

        [DataMember(Name = "EndTimestamp", IsRequired = false, EmitDefaultValue = false)]
        public string EndTimestamp
        {
            get;
            set;
        }

        [DataMember(Name = "PresentationWindowDuration", IsRequired = false, EmitDefaultValue = false)]
        public string PresentationWindowDuration
        {
            get;
            set;
        }

        [DataMember(Name = "LiveBackoffDuration", IsRequired = false, EmitDefaultValue = false)]
        public string LiveBackoffDuration
        {
            get;
            set;
        }

        [DataMember(Name = "Timescale", IsRequired = false, EmitDefaultValue = false)]
        public string Timescale
        {
            get;
            set;
        }
    }

    public class IFilterTrackSelect
    {
        [DataMember(Name = "PropertyConditions", IsRequired = false, EmitDefaultValue = false)]
        public List<FilterTrackPropertyCondition> PropertyConditions
        {
            get;
            set;
        }
    }

    public sealed class FilterProperty
    {
        public static readonly string Type = "Type";
        public static readonly string Name = "Name";
        public static readonly string Language = "Language";
        public static readonly string FourCC = "FourCC";
        public static readonly string Bitrate = "Bitrate";
    }

    public sealed class FilterPropertyTypeValue
    {
        public static readonly string video = "video";
        public static readonly string audio = "audio";
        public static readonly string text = "text";
    }


    public sealed class FilterPropertyFourCCValue
    {
        public static readonly string mp4a = "mp4a";
        public static readonly string avc1 = "avc1";
        public static readonly string mp4v = "mp4v";
        public static readonly string ec3 = "ec-3";
    }

    public sealed class IOperator
    {
        public static readonly string Equal = "Equal";
        public static readonly string notEqual = "notEqual";
    }
    [DataContract]
    public class FilterTrackPropertyCondition
    {
        [DataMember]
        public string Property
        {
            get;
            set;
        }

        [DataMember]
        public string Value
        {
            get;
            set;
        }

        [DataMember]
        public string Operator
        {
            get;
            set;
        }

    }

    public class FilterWithMetadata : Filter
    {
        [JsonProperty("odata.metadata")]
        public string Metadata { get; set; }
    }

    public class AssetFilterWithMetadata : AssetFilter
    {
        [JsonProperty("odata.metadata")]
        public string Metadata { get; set; }
    }


    [DataContract]
    public class Filter
    {
        internal MediaServiceContextForDynManifest _context;

        public Filter()
        {
        }

        public void SetContext(MediaServiceContextForDynManifest context)
        {
            _context = context;
        }


        // http://gauravmantri.com/2012/10/10/windows-azure-media-service-part-iii-managing-assets-via-rest-api/
        // https://azure.microsoft.com/en-us/documentation/articles/media-services-rest-dynamic-manifest/
        /// <summary>
        /// Friendly name for asset.
        /// </summary>
        /// 
        [DataMember(Name = "Name", IsRequired = true)]
        public string Name
        {
            get;
            set;
        }

        [DataMember(Name = "PresentationTimeRange", IsRequired = false, EmitDefaultValue = false)]
        public IFilterPresentationTimeRange PresentationTimeRange
        {
            get;
            set;
        }

        [DataMember(Name = "Tracks", IsRequired = false, EmitDefaultValue = false)]
        public List<IFilterTrackSelect> Tracks
        {
            get;
            set;
        }


        public void Create()  // return true if success
        {
            string serializedResult;
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(this.GetType());
            using (MemoryStream ms = new MemoryStream())
            {
                serializer.WriteObject(ms, this);
                serializedResult = Encoding.Default.GetString(ms.ToArray());
            }

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format(CultureInfo.InvariantCulture, "{0}Filters/", _context.WamsEndpoint));
            request.Method = HttpVerbs.Post;
            request.ContentType = RequestContentType.Json;
            request.Accept = RequestContentType.Json;
            request.Headers.Add(RequestHeaders.XMsVersion, RequestHeaderValues.XMsVersion);
            request.Headers.Add(RequestHeaders.Authorization, string.Format(CultureInfo.InvariantCulture, RequestHeaderValues.Authorization, _context.AccessToken));
            request.Headers.Add(RequestHeaders.DataServiceVersion, RequestHeaderValues.DataServiceVersion);
            request.Headers.Add(RequestHeaders.MaxDataServiceVersion, RequestHeaderValues.MaxDataServiceVersion);
            request.Headers.Add(RequestHeaders.XMsClientRequestId, RequestHeaderValues.ZeroID);
            request.ContentLength = Encoding.UTF8.GetByteCount(serializedResult);

            using (StreamWriter streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(serializedResult);
            }
            try
            {
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode == HttpStatusCode.Created)
                    {
                        // success
                    }

                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Update()  // return true if success
        {
            // Not used. Issue to remvoe tracks.....

            string serializedResult;
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(this.GetType());
            using (MemoryStream ms = new MemoryStream())
            {
                serializer.WriteObject(ms, this);
                serializedResult = Encoding.Default.GetString(ms.ToArray());
            }


            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format(CultureInfo.InvariantCulture, "{0}Filters('{1}')", _context.WamsEndpoint));
            request.Method = HttpVerbs.Merge;
            request.ContentType = RequestContentType.Json;
            request.Accept = RequestContentType.Json;
            request.Headers.Add(RequestHeaders.XMsVersion, RequestHeaderValues.XMsVersion);
            request.Headers.Add(RequestHeaders.Authorization, string.Format(CultureInfo.InvariantCulture, RequestHeaderValues.Authorization, _context.AccessToken));
            request.Headers.Add(RequestHeaders.DataServiceVersion, RequestHeaderValues.DataServiceVersion);
            request.Headers.Add(RequestHeaders.MaxDataServiceVersion, RequestHeaderValues.MaxDataServiceVersion);
            request.Headers.Add(RequestHeaders.XMsClientRequestId, RequestHeaderValues.ZeroID);
            request.ContentLength = Encoding.UTF8.GetByteCount(serializedResult);

            using (StreamWriter streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(serializedResult);
            }
            try
            {
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode == HttpStatusCode.NoContent)
                    {
                        // success
                    }

                }
            }
            catch (Exception e)
            {
                throw e;
            }


        }


        public void Delete()  // return true if success
        {

            var serializer = new JavaScriptSerializer();
            var serializedResult = serializer.Serialize(this);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format(CultureInfo.InvariantCulture, "{0}Filters('{1}')", _context.WamsEndpoint, Name));
            request.Method = HttpVerbs.Delete;
            request.ContentType = RequestContentType.Json;
            request.Accept = RequestContentType.Json;
            request.Headers.Add(RequestHeaders.XMsVersion, RequestHeaderValues.XMsVersion);
            request.Headers.Add(RequestHeaders.Authorization, string.Format(CultureInfo.InvariantCulture, RequestHeaderValues.Authorization, _context.AccessToken));
            request.Headers.Add(RequestHeaders.DataServiceVersion, RequestHeaderValues.DataServiceVersion);
            request.Headers.Add(RequestHeaders.MaxDataServiceVersion, RequestHeaderValues.MaxDataServiceVersion);
            request.Headers.Add(RequestHeaders.XMsClientRequestId, RequestHeaderValues.ZeroID);
            request.ContentLength = Encoding.UTF8.GetByteCount(serializedResult);

            using (StreamWriter streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(serializedResult);
            }
            try
            {
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode == HttpStatusCode.NoContent)
                    {
                        // success
                    }

                }
            }


            catch (Exception e)
            {
                throw e;
            }
        }


    }

    [DataContract]
    public class AssetFilter : Filter
    {

        [DataMember(Name = "Id", IsRequired = true)]
        public string Id
        {
            get;
            set;
        }

        [DataMember(Name = "ParentAssetId", IsRequired = true)]
        public string ParentAssetId
        {
            get;
            set;
        }

        public AssetFilter()
        {
        }

        public AssetFilter(IAsset asset)
        {
            ParentAssetId = asset.Id;
        }

        public void SetParentAsset(IAsset parentasset)
        {
            ParentAssetId = parentasset.Id;
        }


        public void Create()  // return true if success
        {
            string serializedResult;
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(this.GetType());
            using (MemoryStream ms = new MemoryStream())
            {
                serializer.WriteObject(ms, this);
                serializedResult = Encoding.Default.GetString(ms.ToArray());
            }

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format(CultureInfo.InvariantCulture, "{0}AssetFilters/", _context.WamsEndpoint));
            request.Method = HttpVerbs.Post;
            request.ContentType = RequestContentType.Json;
            request.Accept = RequestContentType.Json;
            request.Headers.Add(RequestHeaders.XMsVersion, RequestHeaderValues.XMsVersion);
            request.Headers.Add(RequestHeaders.Authorization, string.Format(CultureInfo.InvariantCulture, RequestHeaderValues.Authorization, _context.AccessToken));
            request.Headers.Add(RequestHeaders.DataServiceVersion, RequestHeaderValues.DataServiceVersion);
            request.Headers.Add(RequestHeaders.MaxDataServiceVersion, RequestHeaderValues.MaxDataServiceVersion);
            request.Headers.Add(RequestHeaders.XMsClientRequestId, RequestHeaderValues.ZeroID);
            request.ContentLength = Encoding.UTF8.GetByteCount(serializedResult);

            using (StreamWriter streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(serializedResult);
            }
            try
            {
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode == HttpStatusCode.Created)
                    {
                        // success
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Delete()  // return true if success
        {

            var serializer = new JavaScriptSerializer();
            var serializedResult = serializer.Serialize(this);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format(CultureInfo.InvariantCulture, "{0}AssetFilters('{1}')", _context.WamsEndpoint, HttpUtility.UrlEncode(Id)));
            request.Method = HttpVerbs.Delete;
            request.ContentType = RequestContentType.Json;
            request.Accept = RequestContentType.Json;
            request.Headers.Add(RequestHeaders.XMsVersion, RequestHeaderValues.XMsVersion);
            request.Headers.Add(RequestHeaders.Authorization, string.Format(CultureInfo.InvariantCulture, RequestHeaderValues.Authorization, _context.AccessToken));
            request.Headers.Add(RequestHeaders.DataServiceVersion, RequestHeaderValues.DataServiceVersion);
            request.Headers.Add(RequestHeaders.MaxDataServiceVersion, RequestHeaderValues.MaxDataServiceVersion);
            request.Headers.Add(RequestHeaders.XMsClientRequestId, RequestHeaderValues.ZeroID);
            request.ContentLength = Encoding.UTF8.GetByteCount(serializedResult);

            using (StreamWriter streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(serializedResult);
            }
            try
            {
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode == HttpStatusCode.NoContent)
                    {
                        // success
                    }

                }
            }


            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
