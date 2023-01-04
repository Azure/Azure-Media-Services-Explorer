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
using System;

namespace AMSExplorer
{
    public class StreamingEndpointEntry
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public StreamingEndpointResourceState State { get; set; }
        public StreamingEndpointInformation.StreamEndpointType Type { get; set; }
        public string CDN { get; set; }
        public string Description { get; set; }
        public string ScaleUnits { get; set; }
        public DateTime LastModified { get; set; }

    }
}