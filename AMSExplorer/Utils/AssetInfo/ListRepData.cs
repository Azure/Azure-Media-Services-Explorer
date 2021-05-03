﻿//----------------------------------------------------------------------------------------------
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

using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMSExplorer
{
    public class ListRepData
    {
        public List<RepData> data;
        public ListRepData()
        {
            data = new List<RepData>();
        }
        public void Add(string label, string value)
        {
            data.Add(new RepData() { label = label, value = value ?? string.Empty });
        }

        public void Add(ListRepData listRepData)
        {
            data.AddRange(listRepData.data);
        }

        public void Add(string label)
        {
            data.Add(new RepData() { label = label, value = null });
        }

        public StringBuilder ReturnStringBuilder()
        {
            StringBuilder sb = new StringBuilder();
            // calculate padding max
            int maxLenghtStr = data.Where(d => d.value != null).Select(d => d.label.Length).Max();

            // build StringBuilder
            data.Select(d => d.value != null ? d.label + new string(' ', maxLenghtStr - d.label.Length) + ": " + d.value : d.label).ToList().ForEach(s => sb.AppendLine(s));

            return sb;
        }
    }

}
