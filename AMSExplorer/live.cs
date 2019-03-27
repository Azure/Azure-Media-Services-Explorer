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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Security.Cryptography;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.MediaServices.Client;
using System.Configuration;
using System.IO;
using System.Threading;
using System.Globalization;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.Diagnostics;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Blob.Protocol;
using System.Collections.ObjectModel;
using System.Drawing.Drawing2D;
using Outlook = Microsoft.Office.Interop.Outlook;
using System.Collections.Specialized;
using System.Runtime.Serialization;
using System.Linq.Expressions;
using Microsoft.Azure.Management.Media;
using Microsoft.Azure.Management.Media.Models;

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



    public class ProgramInfo
    {
        private List<IProgram> SelectedPrograms;
        private CloudMediaContext _context;

        public ProgramInfo(IProgram program, CloudMediaContext context)
        {
            SelectedPrograms = new List<IProgram>();
            SelectedPrograms.Add(program);
            _context = context;

        }
        public ProgramInfo(List<IProgram> MySelectedPrograms, CloudMediaContext context)
        {
            SelectedPrograms = MySelectedPrograms;
            _context = context;
        }

        public IEnumerable<Uri> GetValidURIs()
        {
            IEnumerable<Uri> ValidURIs;
            IAsset asset = _context.Assets.Where(a => a.Id == SelectedPrograms.FirstOrDefault().AssetId).Single();
            var ismFile = asset.AssetFiles.AsEnumerable().FirstOrDefault(f => f.Name.EndsWith(".ism"));
            if (ismFile != null)
            {
                var locators = asset.Locators.Where(l => l.Type == LocatorType.OnDemandOrigin);

                var template = new UriTemplate("{contentAccessComponent}/{ismFileName}/manifest");
                ValidURIs = locators.SelectMany(l =>
                    _context
                        .StreamingEndpoints
                        .AsEnumerable()
                          .Where(o => (o.State == StreamingEndpointState.Running))
                          .OrderByDescending(o => o.CdnEnabled)
                        .Select(
                            o =>
                                template.BindByPosition(new Uri("http://" + o.HostName), l.ContentAccessComponent,
                                    ismFile.Name)))
                    .ToArray();

                return ValidURIs;
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<Uri> GetNotValidURIs()
        {
            IEnumerable<Uri> NotValidURIs;
            IAsset asset = _context.Assets.Where(a => a.Id == SelectedPrograms.FirstOrDefault().AssetId).Single();
            var ismFile = asset.AssetFiles.AsEnumerable().FirstOrDefault(f => f.Name.EndsWith(".ism"));
            if (ismFile != null)
            {
                var locators = asset.Locators.Where(l => l.Type == LocatorType.OnDemandOrigin);

                var template = new UriTemplate("{contentAccessComponent}/{ismFileName}/manifest");


                NotValidURIs = locators.SelectMany(l =>
                   _context
                       .StreamingEndpoints
                       .AsEnumerable()
                         .Where(o => (o.State != StreamingEndpointState.Running))
                       .Select(
                           o =>
                               template.BindByPosition(new Uri("http://" + o.HostName), l.ContentAccessComponent,
                                   ismFile.Name)))
                   .ToArray();

                return NotValidURIs;

            }
            else
            {
                return null;
            }
        }
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
                Type = LiveEventEncodingType.Basic,
                Name ="Default720p",
                Video = new List<LiveVideoProfile>()
                {
                    new LiveVideoProfile(){Codec = "H.264", Bitrate= 3500, Width= 1280, Height= 720, Profile= "High", OutputStreamName= "Video_1280x720_3500kbps"},
                    new LiveVideoProfile(){Codec = "H.264", Bitrate= 2200, Width= 960, Height= 540, Profile= "Main", OutputStreamName= "Video_960x540_2200kbps"},
                    new LiveVideoProfile(){Codec = "H.264", Bitrate= 1350, Width= 704, Height= 396, Profile= "Main", OutputStreamName= "Video_704x396_1350kbps"},
                    new LiveVideoProfile(){Codec = "H.264", Bitrate= 850, Width= 512, Height= 288, Profile= "Main", OutputStreamName= "Video_512x288_850kbps"},
                    new LiveVideoProfile(){Codec = "H.264", Bitrate= 550, Width= 384, Height= 216, Profile= "Main", OutputStreamName= "Video_384x216_550kbps"},
                    new LiveVideoProfile(){Codec = "H.264", Bitrate= 350, Width= 340, Height= 192, Profile= "Baseline", OutputStreamName= "Video_340x192_350kbps"},
                    new LiveVideoProfile(){Codec = "H.264", Bitrate= 200, Width= 340, Height= 192, Profile= "Baseline", OutputStreamName= "Video_340x192_200kbps"},
                        },
                Audio = new LiveAudioProfile()
                    {
                    Codec= "AAC-LC", Bitrate= 64, SamplingRate= 44.1, Channels= "Stereo"
                    }
            }
            /*,
             new LiveProfile()
            {
                Type = LiveEventEncodingType.Premium,
                Name ="Default1080p",
                Video = new List<LiveVideoProfile>()
                {
                    new LiveVideoProfile(){Codec = "H.264", Bitrate= 6000, Width= 1920, Height= 1080, Profile= "High", OutputStreamName= "Video_1920x1080_6000kbps"},
                    new LiveVideoProfile(){Codec = "H.264", Bitrate= 3500, Width= 1280, Height= 720, Profile= "High", OutputStreamName= "Video_1280x720_3500kbps"},
                    new LiveVideoProfile(){Codec = "H.264", Bitrate= 2200, Width= 960, Height= 540, Profile= "Main", OutputStreamName= "Video_960x540_2200kbps"},
                    new LiveVideoProfile(){Codec = "H.264", Bitrate= 1350, Width= 704, Height= 396, Profile= "Main", OutputStreamName= "Video_704x396_1350kbps"},
                    new LiveVideoProfile(){Codec = "H.264", Bitrate= 850, Width= 512, Height= 288, Profile= "Main", OutputStreamName= "Video_512x288_850kbps"},
                    new LiveVideoProfile(){Codec = "H.264", Bitrate= 550, Width= 384, Height= 216, Profile= "Main", OutputStreamName= "Video_384x216_550kbps"},
                    new LiveVideoProfile(){Codec = "H.264", Bitrate= 350, Width= 340, Height= 192, Profile= "Baseline", OutputStreamName= "Video_340x192_350kbps"},
                    new LiveVideoProfile(){Codec = "H.264", Bitrate= 200, Width= 340, Height= 192, Profile= "Baseline", OutputStreamName= "Video_340x192_200kbps"},
                        },
                Audio = new LiveAudioProfile()
                    {
                    Codec= "AAC-LC", Bitrate= 64, SamplingRate= 44.1, Channels= "Stereo"
                    }
            }
            */
        };
    }


    [DataContract]
    internal class OAuth2TokenResponse
    {
        [DataMember(Name = "access_token")]
        public string AccessToken { get; set; }
    }
}
