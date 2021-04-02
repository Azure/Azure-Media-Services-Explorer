//----------------------------------------------------------------------------------------------
//    Copyright 2020 Microsoft Corporation
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
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AMSExplorer.Rest
{
    /// <summary>
    /// Rest call for live transcription preview
    /// https://docs.microsoft.com/en-us/azure/media-services/latest/live-transcription
    /// 
    /// </summary>
    public partial class AmsClientRest
    {
        private const string liveEventApiUrl = "subscriptions/{0}/resourceGroups/{1}/providers/Microsoft.Media/mediaservices/{2}/liveEvents/{3}?api-version=2019-05-01-preview";

        public async Task<string> CreateLiveEventAsync(LiveEventRestObject liveEventSettings, bool startLiveEventNow)
        {
            string URL = GenerateApiUrl(liveEventApiUrl, liveEventSettings.Name) + string.Format("&autoStart={0}", startLiveEventNow.ToString());
            string responseContent = await CreateObjectAsync(URL, liveEventSettings.ToJson());
            return responseContent;
        }


        public LiveEventRestObject GetLiveEvent(string liveEventName)
        {
            Task<LiveEventRestObject> task = Task.Run<LiveEventRestObject>(async () => await GetLiveEventAsync(liveEventName));
            return task.Result;

            // return GetLiveEventAsync(liveEventName).GetAwaiter().GetResult();
        }


        public async Task<LiveEventRestObject> GetLiveEventAsync(string liveEventName)
        {
            string URL = GenerateApiUrl(liveEventApiUrl, liveEventName);
            string responseContent = await GetObjectContentAsync(URL);
            return LiveEventRestObject.FromJson(responseContent);
        }
    }


    public class LiveEventRestObject
    {
        public static LiveEventRestObject FromJson(string json)
        {
            return JsonConvert.DeserializeObject<LiveEventRestObject>(json, ConverterLE.Settings);
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, ConverterLE.Settings);
        }

        public LiveEventRestObject(string name, string location, string description, bool? vanityUrl, LiveEventEncoding encoding, LiveEventInput input, LiveEventPreview preview, IList<StreamOptionsFlag?> streamOptions, IList<LiveEventTranscription> transcriptions)
        {
            Name = name;
            Location = location;
            Properties = new PropertiesForRest { Description = description, VanityUrl = vanityUrl, Encoding = encoding, Input = input, Preview = preview, StreamOptions = streamOptions, Transcriptions = transcriptions };
        }

        [JsonProperty("properties")]
        public PropertiesForRest Properties { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

    }

    public class PropertiesForRest
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("input")]
        public LiveEventInput Input { get; set; }

        [JsonProperty("preview")]
        public LiveEventPreview Preview { get; set; }

        [JsonProperty("encoding")]
        public LiveEventEncoding Encoding { get; set; }

        [JsonProperty("transcriptions")]
        public IList<LiveEventTranscription> Transcriptions { get; set; }

        [JsonProperty("vanityUrl")]
        public bool? VanityUrl { get; set; }

        [JsonProperty("streamOptions")]
        public IList<StreamOptionsFlag?> StreamOptions { get; set; }
    }
}