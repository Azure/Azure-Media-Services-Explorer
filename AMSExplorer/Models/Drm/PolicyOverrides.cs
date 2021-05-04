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
    /// Widevine PolicyOverrides class.
    /// </summary>
    public class PolicyOverrides
    {
        /// <summary>
        /// Gets or sets a value indicating whether playback of the content is allowed. Default is false.
        /// </summary>
        [JsonProperty("can_play")]
        public bool CanPlay { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the license might be persisted to nonvolatile storage for offline use. Default is false.
        /// </summary>
        [JsonProperty("can_persist")]
        public bool CanPersist { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether renewal of this license is allowed. If true, the duration of the license can be extended by heartbeat. Default is false.
        /// </summary>
        [JsonProperty("can_renew")]
        public bool CanRenew { get; set; }

        /// <summary>
        /// Gets or sets the time window while playback is permitted. A value of 0 indicates that there is no limit to the duration. Default is 0.
        /// </summary>
        [JsonProperty("rental_duration_seconds")]
        public int RentalDurationSeconds { get; set; }

        /// <summary>
        /// Gets or sets the viewing window of time after playback starts within the license duration. A value of 0 indicates that there is no limit to the duration. Default is 0.
        /// </summary>
        [JsonProperty("playback_duration_seconds")]
        public int PlaybackDurationSeconds { get; set; }

        /// <summary>
        /// Gets or sets the time window for this specific license. A value of 0 indicates that there is no limit to the duration. Default is 0.
        /// </summary>
        [JsonProperty("license_duration_seconds")]
        public int LicenseDurationSeconds { get; set; }
    }
}
