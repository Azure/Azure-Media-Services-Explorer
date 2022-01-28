//----------------------------------------------------------------------------------------------
//    Copyright 2022 Microsoft Corporation
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

using Newtonsoft.Json;

namespace AMSExplorer
{
    /// <summary>
    /// OutputProtection Widevine class.
    /// </summary>
    public class OutputProtection
    {
        /// <summary>
        /// Gets or sets HDCP protection.
        /// Supported values : HDCP_NONE, HDCP_V1, HDCP_V2
        /// </summary>
        [JsonProperty("hdcp")]
        public string HDCP { get; set; }

        /// <summary>
        /// Gets or sets CGMS.
        /// </summary>
        [JsonProperty("cgms_flags")]
        public string CgmsFlags { get; set; }
    }
}
