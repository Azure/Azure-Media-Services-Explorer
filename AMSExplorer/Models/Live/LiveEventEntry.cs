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

using Azure.ResourceManager.Media.Models;
using System;
using System.Drawing;

namespace AMSExplorer
{
    public class LiveEventEntry
    {
        //   select new { j.Name, j.Id, j.State, j.StartTime, j.EndTime, j.Tasks[0].PerfMessage, Progress=j.GetOverallProgress() };
        public string Name { get; set; }
        public LiveEventResourceState? State { get; set; }
        public DateTimeOffset? LastModifiedOn { get; set; }
        public string Description { get; set; }
        public string InputProtocol { get; set; }
        public Bitmap Encoding { get; set; }
        public string EncodingPreset { get; set; }
        public Uri InputUrl { get; set; }
        public Uri PreviewUrl { get; set; }
    }
}
