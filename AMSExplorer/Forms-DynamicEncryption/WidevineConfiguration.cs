using System;
using System.Collections.Generic;
using System.Text;

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
