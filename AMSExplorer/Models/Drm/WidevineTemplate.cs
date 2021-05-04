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

using Newtonsoft.Json;

namespace AMSExplorer
{
    /// <summary>
    /// Widevine template.
    /// </summary>
    public class WidevineTemplate
    {
        /// <summary>
        /// Gets or sets the allowed track types.
        /// SD_ONLY or SD_HD.
        /// Controls which content keys are included in a license.
        /// </summary>
        [JsonProperty("allowed_track_types")]
        public string AllowedTrackTypes { get; set; }

        /// <summary>
        /// Gets or sets a finer-grained control on which content keys to return.
        /// For more information, see the section "Content key specs."
        /// Only one of the allowed_track_types and content_key_specs values can be specified.
        /// </summary>
        [JsonProperty("content_key_specs")]
#pragma warning disable CA1819 // Properties should not return arrays
        public ContentKeySpec[] ContentKeySpecs { get; set; }
#pragma warning restore CA1819 // Properties should not return arrays

        /// <summary>
        /// Gets or sets policy settings for the license.
        /// In the event this asset has a predefined policy, these specified values are used.
        /// </summary>
        [JsonProperty("policy_overrides")]
        public PolicyOverrides PolicyOverrides { get; set; }
    }
}
