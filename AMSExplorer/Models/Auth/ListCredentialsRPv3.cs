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

using System.Collections.Generic;

namespace AMSExplorer
{
    public class ListCredentialsRPv3
    {
        public decimal Version = 3;
        public List<CredentialsEntryV4> MediaServicesAccounts = new();
    }

    public class ListCredentialsRPv4
    {
        public decimal Version = 4;
        public List<CredentialsEntryV4> MediaServicesAccounts = new();
    }

}
