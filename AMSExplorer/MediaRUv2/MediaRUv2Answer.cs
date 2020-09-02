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

using Newtonsoft.Json;
using System;

// These classes use REST v2 API to get and set the media reserved units

namespace AMSExplorer
{
    public partial class MediaRUv2Answer
    {
        [JsonProperty("odata.metadata")]
        public Uri OdataMetadata { get; set; }

        [JsonProperty("value")]
        public InfoMediaRU[] Value { get; set; }

        public static MediaRUv2Answer FromJson(string json)
        {
            return JsonConvert.DeserializeObject<MediaRUv2Answer>(json, AMSExplorer.Converter.Settings);
        }
    }
}