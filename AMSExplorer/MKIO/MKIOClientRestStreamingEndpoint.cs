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


using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AMSExplorer.MKIO.Models;

namespace AMSExplorer.MKIO
{
    /// <summary>
    /// REST Client for MKIO
    /// https://io.mediakind.com
    /// 
    /// </summary>
    public partial class MKIOClientRest
    {
        //
        // streaming endpoints
        //
        private const string streamingEndpointsApiUrl = "api/ams/{0}/streamingEndpoints";
        private const string streamingEndpointApiUrl = streamingEndpointsApiUrl + "/{1}";

        public List<MKIOStreamingEndpoint> ListStreamingEndpoints()
        {
            Task<List<MKIOStreamingEndpoint>> task = Task.Run<List<MKIOStreamingEndpoint>>(async () => await ListStreamingEndpointsAsync());
            return task.GetAwaiter().GetResult();
        }

        public async Task<List<MKIOStreamingEndpoint>> ListStreamingEndpointsAsync()
        {
            string URL = GenerateApiUrl(streamingEndpointsApiUrl);
            string responseContent = await GetObjectContentAsync(URL);
            return MKIOListStreamingEndpoint.FromJson(responseContent).Value;
        }

        public MKIOStreamingEndpoint GetStreamingEndpoint(string streamingEndpointName)
        {
            Task<MKIOStreamingEndpoint> task = Task.Run<MKIOStreamingEndpoint>(async () => await GetStreamingEndpointAsync(streamingEndpointName));
            return task.GetAwaiter().GetResult();
        }

        public async Task<MKIOStreamingEndpoint> GetStreamingEndpointAsync(string streamingEndpointName)
        {
            string URL = GenerateApiUrl(streamingEndpointApiUrl, streamingEndpointName);
            string responseContent = await GetObjectContentAsync(URL);
            return MKIOStreamingEndpoint.FromJson(responseContent);
        }

        public MKIOStreamingEndpoint CreateStreamingEndpoint(string streamingEndpointName, MKIOStreamingEndpoint content, bool autoStart = true)
        {
            Task<MKIOStreamingEndpoint> task = Task.Run<MKIOStreamingEndpoint>(async () => await CreateStreamingEndpointAsync(streamingEndpointName, content, autoStart));
            return task.GetAwaiter().GetResult();
        }

        public async Task<MKIOStreamingEndpoint> CreateStreamingEndpointAsync(string streamingEndpointName, MKIOStreamingEndpoint content, bool autoStart = true)
        {
            string URL = GenerateApiUrl(streamingEndpointApiUrl + "?autoStart=" + autoStart.ToString(), streamingEndpointName);
            string responseContent = await CreateObjectAsync(URL, content.ToJson());
            return MKIOStreamingEndpoint.FromJson(responseContent);
        }

        public void StopStreamingEndpoint(string streamingEndpointName)
        {
            Task.Run(async () => await StopStreamingEndpointAsync(streamingEndpointName));
        }

        public async Task StopStreamingEndpointAsync(string streamingEndpointName)
        {
            await StreamingEndpointOperationAsync(streamingEndpointName, "stop", HttpMethod.Post);
        }

        public void StartStreamingEndpoint(string streamingEndpointName)
        {
            Task.Run(async () => await StartStreamingEndpointAsync(streamingEndpointName));
        }

        public async Task StartStreamingEndpointAsync(string streamingEndpointName)
        {
            await StreamingEndpointOperationAsync(streamingEndpointName, "start", HttpMethod.Post);
        }

        public void DeleteStreamingEndpoint(string streamingEndpointName)
        {
            Task.Run(async () => await DeleteStreamingEndpointAsync(streamingEndpointName));
        }

        public async Task DeleteStreamingEndpointAsync(string streamingEndpointName)
        {
            await StreamingEndpointOperationAsync(streamingEndpointName, null, HttpMethod.Delete);
        }

        private async Task StreamingEndpointOperationAsync(string streamingEndpointName, string operation, HttpMethod httpMethod)
        {
            string URL = GenerateApiUrl(streamingEndpointApiUrl + (operation != null ? "/" + operation : string.Empty), streamingEndpointName);
            await ObjectContentAsync(URL, httpMethod);
        }
    }
}