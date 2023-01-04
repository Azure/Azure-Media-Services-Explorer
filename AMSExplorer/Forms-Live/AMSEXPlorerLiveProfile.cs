//----------------------------------------------------------------------------------------------
//    Copyright 2023 Microsoft Corporation
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
using System.Collections.Generic;

namespace AMSExplorer
{
    public static partial class AMSEXPlorerLiveProfile
    {

        public static readonly List<LiveProfile> Profiles = new()
        {
            new LiveProfile()
            {
                Type = LiveEventEncodingType.Standard,
                Name = "default720p",
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
                    Codec = "AAC-LC",
                    Bitrate = 128,
                    SamplingRate = 48,
                    Channels = "Stereo"
                }
            },
            new LiveProfile()
            {
                Type = LiveEventEncodingType.Premium1080p,
                Name = "default1080p",
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
                    Codec = "AAC-LC",
                    Bitrate = 128,
                    SamplingRate = 48,
                    Channels = "Stereo"
                }
            }
        };
    }
}
