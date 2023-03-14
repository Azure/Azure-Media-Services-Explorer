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

namespace AMSExplorer
{
    public class JobEntryV3
    {
        public string Name { get; set; }
        public string TransformName { get; set; }
        public string Description { get; set; }
        public int Outputs { get; set; }
        public MediaJobPriority? Priority { get; set; }
        public MediaJobState? State { get; set; }
        public string StartOn { get; set; }
        public string LastModified { get; set; }
        public string EndOn { get; set; }
        public string Duration { get; set; }
        public double Progress { get; set; }
    }
}
