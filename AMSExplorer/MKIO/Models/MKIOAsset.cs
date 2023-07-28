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


using Newtonsoft.Json;

namespace AMSExplorer.MKIO.Models
{

    public class MKIOAsset
    {
        public static MKIOAsset FromJson(string json)
        {
            return JsonConvert.DeserializeObject<MKIOAsset>(json, ConverterLE.Settings);
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, ConverterLE.Settings);
        }

        public MKIOAsset(string name, string id, string assetId, string created, string lastModified, string description, string container, string storageAccountName, string storageEncryptionFormat, string encryptionScope)
        {
            Name = name;
            Id = id;
            Properties = new MKIOAssetProperties { AssetId = assetId, Created = created, LastModified = lastModified, Description = description, Container = container, StorageAccountName = storageAccountName, StorageEncryptionFormat = storageEncryptionFormat, EncryptionScope = encryptionScope };
        }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("properties")]
        public MKIOAssetProperties Properties { get; set; }
    }
}