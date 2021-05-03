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


using Microsoft.Azure.Management.Media.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AMSExplorer.Rest
{
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