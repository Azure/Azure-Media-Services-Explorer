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
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AMSExplorer.Rest
{

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
}