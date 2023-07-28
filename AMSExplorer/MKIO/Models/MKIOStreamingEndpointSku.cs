﻿//----------------------------------------------------------------------------------------------
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


using Newtonsoft.Json;

namespace AMSExplorer.MKIO.Models
{
    public class MKIOStreamingEndpointSku
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">Standard or Premium</param>
        /// <param name="capacity">600</param>
        public MKIOStreamingEndpointSku(string name, int capacity)
        {
            Name = name;
            Capacity = capacity;
        }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("capacity")]
        public int Capacity { get; set; }

    }
}