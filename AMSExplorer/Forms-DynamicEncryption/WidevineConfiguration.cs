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

namespace AMSExplorer
{
    // <WidevineConfigurationClasses>
    public class PolicyOverrides
    {
        public bool CanPlay { get; set; }
        public bool CanPersist { get; set; }
        public bool CanRenew { get; set; }
        public int RentalDurationSeconds { get; set; }    //Indicates the time window while playback is permitted. A value of 0 indicates that there is no limit to the duration. Default is 0.
        public int PlaybackDurationSeconds { get; set; }  //The viewing window of time after playback starts within the license duration. A value of 0 indicates that there is no limit to the duration. Default is 0.
        public int LicenseDurationSeconds { get; set; }   //Indicates the time window for this specific license. A value of 0 indicates that there is no limit to the duration. Default is 0.
    }

    public class ContentKeySpec
    {
        public string TrackType { get; set; }
        public int SecurityLevel { get; set; }
        public OutputProtection RequiredOutputProtection { get; set; }
    }

    public class OutputProtection
    {
        public string HDCP { get; set; }
    }

    public class WidevineTemplate
    {
        public string AllowedTrackTypes { get; set; }
        public ContentKeySpec[] ContentKeySpecs { get; set; }
        public PolicyOverrides PolicyOverrides { get; set; }
    }
    // </WidevineConfigurationClasses>
}
