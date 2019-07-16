//----------------------------------------------------------------------------------------------
//    Copyright 2019 Microsoft Corporation
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

using Microsoft.Azure.Management.Media.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Serialization;

namespace AMSExplorer
{

    public class LiveEventEntry
    {
        //   select new { j.Name, j.Id, j.State, j.StartTime, j.EndTime, j.Tasks[0].PerfMessage, Progress=j.GetOverallProgress() };
        public string Name { get; set; }
        public LiveEventResourceState? State { get; set; }
        public DateTime? LastModified { get; set; }
        public string Description { get; set; }
        public string InputProtocol { get; set; }
        public Bitmap Encoding { get; set; }
        public string EncodingPreset { get; set; }
        public string InputUrl { get; set; }
        public string PreviewUrl { get; set; }
    }

    public class LiveOutputEntry
    {
        public string Name { get; set; }
        public LiveOutputResourceState? State { get; set; }
        public DateTime? LastModified { get; set; }
        public string Description { get; set; }
        public TimeSpan? ArchiveWindowLength { get; set; }
        public Bitmap Published { get; set; }
        public string LiveEventName { get; set; } // Name of live event where the output is attached

    }


    public enum OperationType
    {
        Create = 0,
        Delete,
        Start,
        Stop,
        Edit,
        Scale,
        SettingsUpdate,
        ResetAsset,
        Reset
    }

    public class StatusInfo
    {
        public string EntityName { get; set; }
        public string ErrorMessage { get; set; }

        public OperationType OperationType { get; set; }

        public DateTime TimeStamp { get; set; }

        public StatusInfo()
        {
            TimeStamp = DateTime.Now;
        }
    }

    public class EndpointSettingInfo
    {
        public string Name { get; set; }
        public string IpV4 { get; set; }
    }



    public enum enumDisplayProgram
    {
        Selected = 0,
        Any,
        None
    }

    public static class AMSEXPlorerLiveProfile
    {
        public class LiveVideoProfile
        {
            public string Codec { get; set; }
            public int Bitrate { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }
            public int MaxFPS { get; set; }
            public string Profile { get; set; }
            public string OutputStreamName { get; set; }

        }

        public class LiveAudioProfile
        {
            public string Language { get; set; }
            public string Codec { get; set; }
            public int Bitrate { get; set; }
            public double SamplingRate { get; set; }
            public string Channels { get; set; }
        }



        public class LiveProfile
        {
            public string Name { get; set; }
            public LiveEventEncodingType Type { get; set; }
            public List<LiveVideoProfile> Video { get; set; }
            public LiveAudioProfile Audio { get; set; }
        }

        public static readonly List<LiveProfile> Profiles = new List<LiveProfile>
        {
            new LiveProfile()
            {
                Type = LiveEventEncodingType.Standard,
                Name ="default720p",
                Video = new List<LiveVideoProfile>()
                {
                    new LiveVideoProfile(){Codec = "H.264", Bitrate= 3500, Width= 1280, Height= 720, MaxFPS=30, Profile= "High", OutputStreamName= "Video_1280x720_3500kbps"},
                    new LiveVideoProfile(){Codec = "H.264", Bitrate= 2200, Width= 960, Height= 540, MaxFPS=30, Profile= "High", OutputStreamName= "Video_960x540_2200kbps"},
                    new LiveVideoProfile(){Codec = "H.264", Bitrate= 1350, Width= 704, Height= 396, MaxFPS=30, Profile= "High", OutputStreamName= "Video_704x396_1350kbps"},
                    new LiveVideoProfile(){Codec = "H.264", Bitrate= 850, Width= 512, Height= 288, MaxFPS=30, Profile= "High", OutputStreamName= "Video_512x288_850kbps"},
                    new LiveVideoProfile(){Codec = "H.264", Bitrate= 550, Width= 384, Height= 216, MaxFPS=30, Profile= "High", OutputStreamName= "Video_384x216_550kbps"},
                    new LiveVideoProfile(){Codec = "H.264", Bitrate= 200, Width= 340, Height= 192, MaxFPS=30, Profile= "High", OutputStreamName= "Video_340x192_200kbps"},
                        },
                Audio = new LiveAudioProfile()
                    {
                        Codec= "AAC-LC", Bitrate= 128, SamplingRate= 48, Channels= "Stereo"
                    }
            },
            new LiveProfile()
            {
                Type = LiveEventEncodingType.Premium1080p,
                Name ="default1080p",
                Video = new List<LiveVideoProfile>()
                {
                        new LiveVideoProfile(){Codec = "H.264", Bitrate= 5500, Width= 1920, Height= 1080, MaxFPS=30, Profile= "High", OutputStreamName= "Video_1920x1080_5500kbps"},
                        new LiveVideoProfile(){Codec = "H.264", Bitrate= 3000, Width= 1280, Height= 720, MaxFPS=30, Profile= "High", OutputStreamName= "Video_1280x720_3000kbps"},
                        new LiveVideoProfile(){Codec = "H.264", Bitrate= 1600, Width= 960, Height= 540, MaxFPS=30, Profile= "High", OutputStreamName= "Video_960x540_1600kbps"},
                        new LiveVideoProfile(){Codec = "H.264", Bitrate= 800, Width= 640, Height= 360, MaxFPS=30, Profile= "High", OutputStreamName= "Video_640x360_800kbps"},
                        new LiveVideoProfile(){Codec = "H.264", Bitrate= 400, Width= 480, Height= 270, MaxFPS=30, Profile= "High", OutputStreamName= "Video_480x270_400kbps"},
                        new LiveVideoProfile(){Codec = "H.264", Bitrate= 200, Width= 320, Height= 180, MaxFPS=30, Profile= "High", OutputStreamName= "Video_320x180_200kbps"},
                        },
                Audio = new LiveAudioProfile()
                    {
                        Codec= "AAC-LC", Bitrate= 128, SamplingRate= 48, Channels= "Stereo"
                    }
                }
        };
    }


    [DataContract]
    internal class OAuth2TokenResponse
    {
        [DataMember(Name = "access_token")]
        public string AccessToken { get; set; }
    }
}
