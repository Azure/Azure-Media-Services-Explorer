using Newtonsoft.Json;

namespace AMSExplorer
{
    // <WidevineConfigurationClasses>
    public class PolicyOverrides
    {
        [JsonProperty("can_play")]
        public bool CanPlay { get; set; }
        [JsonProperty("can_persist")]
        public bool CanPersist { get; set; }
        [JsonProperty("can_renew")]
        public bool CanRenew { get; set; }
        [JsonProperty("rental_duration_seconds")]
        public int RentalDurationSeconds { get; set; }    //Indicates the time window while playback is permitted. A value of 0 indicates that there is no limit to the duration. Default is 0.
        [JsonProperty("playback_duration_seconds")]
        public int PlaybackDurationSeconds { get; set; }  //The viewing window of time after playback starts within the license duration. A value of 0 indicates that there is no limit to the duration. Default is 0.
        [JsonProperty("license_duration_seconds")]
        public int LicenseDurationSeconds { get; set; }   //Indicates the time window for this specific license. A value of 0 indicates that there is no limit to the duration. Default is 0.
    }

    public class ContentKeySpec
    {
        [JsonProperty("track_type")]
        public string TrackType { get; set; }
        [JsonProperty("security_level")]
        public int SecurityLevel { get; set; }
        [JsonProperty("required_output_protection")]
        public OutputProtection RequiredOutputProtection { get; set; }

    }

    public class OutputProtection
    {
        [JsonProperty("hdcp")]
        public string HDCP { get; set; }
    }

    public class WidevineTemplate
    {
        [JsonProperty("allowed_track_types")]
        public string AllowedTrackTypes { get; set; }
        [JsonProperty("content_key_specs")] 
        public ContentKeySpec[] ContentKeySpecs { get; set; }
        [JsonProperty("policy_overrides")]
        public PolicyOverrides PolicyOverrides { get; set; }
    }
    // </WidevineConfigurationClasses>
}
