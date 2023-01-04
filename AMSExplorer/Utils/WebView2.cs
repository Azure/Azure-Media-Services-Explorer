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



using System.Reflection;
using System;

namespace AMSExplorer
{
    class WebView2
    {
        public static string TryGetWebView2LoaderFolder()
        {
            try
            {
                var assy = Assembly.GetExecutingAssembly();
                var codeBase = assy.Location;
                var folder = codeBase.Substring(0, codeBase.LastIndexOf('\\')); // drops the filename portion
                var loaderFolderUri = new Uri(folder + @"/runtimes/win-x64/native");
                return loaderFolderUri.LocalPath; // converts to file system syntax: c:\...\runtimes\win-x64\native
            }
            catch
            {
                throw;
            }
        }
    }
}
