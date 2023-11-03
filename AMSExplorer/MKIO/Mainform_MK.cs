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

using Microsoft.Azure.Storage.Auth;
using Microsoft.Azure.Storage;
using MK.IO.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MK.IO;
using Newtonsoft.Json.Serialization;
using AMSExplorer.Rest;

namespace AMSExplorer
{
    public partial class Mainform : Form
    {
        // Placeholder for MK/IO code

        /// <summary>
        /// Content key policies creation in MK/IO
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private async Task DoMKIOCreateContentKeyPolicyAsync()
        {
            Telemetry.TrackEvent("DoMKIOCreateContentKeyPolicyAsync");

            var _restClient = new AmsClientRest(_amsClient);
            var ckpols = await ReturnSelectedCKPoliciessAsync();

            foreach (var ck in ckpols)
            {
                try
                {
                    var cknewprop = new MK.IO.ContentKeyPolicyProperties();

                    // use REST with AMS
                    var existingCkProp = await _restClient.GetContentKeyPolicyPropertiesWithSecretsAsync(ck.Data.Name);
                    //dynamic existingCkDyn = JsonConvert.DeserializeObject(existingCk);

                    var ckPolProp = JsonConvert.DeserializeObject<ContentKeyPolicyProperties>(existingCkProp);
                    var createdPol = await MKIOclient.ContentKeyPolicies.CreateAsync(ck.Data.Name, new ContentKeyPolicy(ckPolProp));
                    TextBoxLogWriteLine($"Succesfully created content key policy '{ck.Data.Name}' in MK/IO");
                }
                catch
                {
                    TextBoxLogWriteLine($"Error when creating content key policy '{ck.Data.Name}' in MK/IO", true);
                }
            }
        }

        /// <summary>
        /// Storage accounts creation in MK/IO
        /// </summary>
        /// <returns></returns>
        private async Task DoMKIOStorageAddAsync()
        {
            Telemetry.TrackEvent("DoMKIOStorageAddAsync");

            var storage = ReturnSelectedStorage();
            if (storage == null) return;

            string storName = AMSClientV3.GetStorageName(storage.Id);

            migratedStorageAccountsToMKIO = await MKIOclient.StorageAccounts.ListAsync();
            var storageMKIOName = migratedStorageAccountsToMKIO.Where(s => s.Spec.Name == storName).FirstOrDefault();

            if (storageMKIOName != null && storageMKIOName.Spec.Name == storName)
            {
                MessageBox.Show($"Storage account {storName} is already migrated to MK/IO", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MKIOStorageCreation formStorageCreation = new()
            {
                SASDurationInMonths = 120,
                StorageName = storName,
                StorageRegion = _amsClient.AMSclient.Get().Value.Data.Location.Name
            };

            if (formStorageCreation.ShowDialog() == DialogResult.OK)
            {
                string sasSig = string.Empty;
                Uri blobEndpoint = null;

                try
                {
                    CloudStorageAccount storageAccount = new(new StorageCredentials(storName, formStorageCreation.AccessKey), _amsClient.environment.ReturnStorageSuffix(), true);

                    SharedAccessAccountPolicy pol = new()
                    {
                        Permissions = SharedAccessAccountPermissions.Read | SharedAccessAccountPermissions.Write | SharedAccessAccountPermissions.Delete | SharedAccessAccountPermissions.List | SharedAccessAccountPermissions.Add | SharedAccessAccountPermissions.Create | SharedAccessAccountPermissions.Update | SharedAccessAccountPermissions.ProcessMessages,
                        SharedAccessExpiryTime = DateTimeOffset.UtcNow.AddMonths(formStorageCreation.SASDurationInMonths),
                        Services = SharedAccessAccountServices.Blob,
                        ResourceTypes = SharedAccessAccountResourceTypes.Object | SharedAccessAccountResourceTypes.Container
                    };
                    Cursor = Cursors.WaitCursor;
                    sasSig = storageAccount.GetSharedAccessSignature(pol);
                    blobEndpoint = storageAccount.BlobEndpoint;
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message, "Error accessing the storage account", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    TextBoxLogWriteLine(ex);
                    Telemetry.TrackException(ex);
                    Cursor = Cursors.Arrow;
                    return;
                }

                try
                {
                    var storageMKIO = await MKIOclient.StorageAccounts.CreateAsync(new StorageRequestSchema
                    {
                        Spec = new StorageSchema
                        {
                            Name = storName,
                            Location = _amsClient.AMSclient.Get().Value.Data.Location.Name,
                            Description = formStorageCreation.StorageDescription,
                            AzureStorageConfiguration = new BlobStorageAzureProperties
                            {
                                Url = blobEndpoint.ToString() + sasSig
                            }
                        }
                    }
                    );

                    TextBoxLogWriteLine($"Storage account '{storName}' added to MK/IO");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error adding storage account to MK/IO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    TextBoxLogWriteLine(ex);
                    Telemetry.TrackException(ex);
                    Cursor = Cursors.Arrow;
                    return;
                }
            }

            await DoRefreshGridStorageVAsync(false);
            Cursor = Cursors.Arrow;
        }


        /// <summary>
        /// Remove storage account from MK/IO
        /// </summary>
        /// <returns></returns>
        private async Task DoMKIOStorageRemoveAsync()
        {
            Telemetry.TrackEvent("DoMKIOStorageRemoveAsync");

            var storage = ReturnSelectedStorage();
            if (storage == null || migratedStorageAccountsToMKIO.Count == 0) return;

            string storName = AMSClientV3.GetStorageName(storage.Id);

            var storageMKIOName = migratedStorageAccountsToMKIO.Where(s => s.Spec.Name == storName).FirstOrDefault();

            if (storageMKIOName == null)
            {
                MessageBox.Show($"Storage account {storName} is not migrated to MK/IO", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // dialogbox to ask if user wants to remove the storage account
            if (DialogResult.Yes == MessageBox.Show(string.Format("Are you sure you want to remove the storage account '{0}' ?", storName), "Storage account removal", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                Cursor = Cursors.WaitCursor;
                try
                {
                    await MKIOclient.StorageAccounts.DeleteAsync((Guid)storageMKIOName.Metadata.Id);
                    TextBoxLogWriteLine($"Storage account '{storName}' removed from MK/IO");
                }
                catch (Exception ex)
                {
                    TextBoxLogWriteLine($"Error when removing storage account '{storName}' from MK/IO", true);
                    TextBoxLogWriteLine(ex);
                    Telemetry.TrackException(ex);
                }
                await DoRefreshGridStorageVAsync(false);
                Cursor = Cursors.Arrow;
            }
        }

        /// <summary>
        /// Create/migrate asset(s) in MK/IO
        /// </summary>
        /// <returns></returns>
        private async Task MKIOCreateAssetAsync()
        {
            Telemetry.TrackEvent("MKIOCreateAssetAsync");
            if (MKIOclient == null)
            {
                MessageBox.Show("Can't Create", "MK/IO is not connected. Restart the application to connect.");
            }

            var assets = await ReturnSelectedAssetsAsync();
            if (assets.Count == 0) return;


            //let's verify that storage account is in MK/IO !
            var storageNames = assets.Select(a => a.Data.StorageAccountName).Distinct().ToList();
            var storageMKIONames = migratedStorageAccountsToMKIO.Select(s => s.Spec.Name);
            if (!(storageNames.Intersect(storageMKIONames).Count() == storageNames.Count()))
            {
                var nonintersect = storageNames.Except(storageMKIONames);

                if (nonintersect.Count() == 1)
                {
                    MessageBox.Show($"Storage account {nonintersect.First()} has not be added to MK/IO. Please do it before creating the asset(s) in MK/IO.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show($"Storage accounts {string.Join(",", nonintersect.ToArray())} have not be added to MK/IO. Please do it before creating the assets in MK/IO.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }

            var formAsset = new MKIOAssetCreationUpdate(assets.Count == 1 ? MKIOAssetCreationUpdate.AssetCreationMode.Single : MKIOAssetCreationUpdate.AssetCreationMode.Multiple)
            {
                AssetName = assets.Count == 1 ? assets.First().Data.Name : Constants.NameconvAsset,
                AssetDescription = assets.Count == 1 ? assets.First().Data.Description : Constants.NameconvAssetDesc,
                AssetContainer = assets.Count == 1 ? assets.First().Data.Container : string.Empty,
                AssetStorage = assets.Count == 1 ? assets.First().Data.StorageAccountName : string.Empty
            };

            if (formAsset.ShowDialog() == DialogResult.OK)
            {
                foreach (var asset in assets)
                {
                    string assetName = formAsset.AssetName.Replace(Constants.NameconvAsset, asset.Data.Name);

                    string assetDescription = null;
                    if (formAsset.AssetDescription != null)
                    {
                        assetDescription = formAsset.AssetDescription.Replace(Constants.NameconvAssetDesc, asset.Data.Description);
                    }

                    try
                    {
                        await MKIOclient.Assets.CreateOrUpdateAsync(assetName, asset.Data.Container, asset.Data.StorageAccountName, assetDescription);
                        TextBoxLogWriteLine($"Asset '{assetName}' created in MK/IO");
                    }
                    catch (Exception ex)
                    {
                        TextBoxLogWriteLine($"Error when creating asset '{assetName}' in MK/IO", true);
                        TextBoxLogWriteLine(ex);
                        Telemetry.TrackException(ex);
                    }

                    if (formAsset.CloneClearStreamingLocators)
                    {
                        var amsLocators = await asset.GetStreamingLocatorsAsync().ToListAsync();
                        foreach (var locator in amsLocators)
                        {
                            if (locator.StreamingPolicyName == PredefinedStreamingPolicy.ClearStreamingOnly
                                || locator.StreamingPolicyName == PredefinedStreamingPolicy.ClearKey
                                || locator.StreamingPolicyName == PredefinedStreamingPolicy.DownloadOnly
                                || locator.StreamingPolicyName == PredefinedStreamingPolicy.MultiDrmCencStreaming
                                || locator.StreamingPolicyName == PredefinedStreamingPolicy.MultiDrmCencStreaming
                                || locator.StreamingPolicyName == PredefinedStreamingPolicy.DownloadAndClearStreaming
                                )
                            {
                                var locatorRes = (await _amsClient.AMSclient.GetStreamingLocatorAsync(locator.Name)).Value;

                                List<MK.IO.Models.StreamingLocatorContentKey>? mkioContentKey = null;
                                if (formAsset.CloneKeys)
                                {
                                    if (locatorRes.Data.ContentKeys.Count > 0)
                                    {
                                        mkioContentKey = new();
                                        var contentKeysForCurrentLocator = await locatorRes.GetContentKeysAsync().ToListAsync();

                                        foreach (var k in contentKeysForCurrentLocator)
                                        {
                                            string tracksJson = JsonConvert.SerializeObject(k.Tracks, Newtonsoft.Json.Formatting.Indented);

                                            mkioContentKey.Add(new MK.IO.Models.StreamingLocatorContentKey
                                            {
                                                Id = k.Id.ToString(),
                                                PolicyName = k.PolicyName,
                                                LabelReferenceInStreamingPolicy = k.LabelReferenceInStreamingPolicy,
                                                Type = k.KeyType.ToString(),
                                                Value = k.Value,
                                                Tracks = JsonConvert.DeserializeObject<List<TrackSelection>>(tracksJson)
                                            });

                                        };
                                    }
                                }

                                //TextBoxLogWriteLine($"Asset '{assetName}' : locator '{locator.Name}' not created in MK/IO because there are attached content keys.", true);

                                var startT = locatorRes.Data.StartOn?.UtcDateTime.ToString("s");
                                var endT = locatorRes.Data.EndOn?.UtcDateTime.ToString("s");

                                try
                                {
                                    await MKIOclient.StreamingLocators.CreateAsync(locator.Name, new StreamingLocatorProperties
                                    {
                                        AssetName = assetName,
                                        StartTime = startT != null ? startT + "Z" : null,
                                        EndTime = endT != null ? endT + "Z" : null,
                                        StreamingPolicyName = locatorRes.Data.StreamingPolicyName,
                                        StreamingLocatorId = locatorRes.Data.StreamingLocatorId.ToString(),
                                        Filters = locatorRes.Data.Filters?.ToList(),
                                        DefaultContentKeyPolicyName = locatorRes.Data.DefaultContentKeyPolicyName,
                                        ContentKeys = mkioContentKey
                                    });
                                    TextBoxLogWriteLine($"Asset '{assetName}' : locator '{locator.Name}' created in MK/IO");
                                }
                                catch (Exception ex)
                                {
                                    TextBoxLogWriteLine($"Error when creating locator '{locator.Name}' for asset '{assetName}' in MK/IO", true);
                                    TextBoxLogWriteLine(ex);
                                    Telemetry.TrackException(ex);
                                }
                            }
                            else
                            {
                                TextBoxLogWriteLine($"Asset '{assetName}' : locator '{locator.Name}' not created in MK/IO because it does not use a built-in streaming policy.", true);
                            }
                        }
                    }
                }
            }
            DoRefreshGridAssetV(false);
        }
    }
}
