using Azure;
using Azure.ResourceManager.Media;
using Azure.ResourceManager.Media.Models;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Rest.Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace AMSExplorer.ManifestGeneration
{
    public class ClientManifestUtils
    {

        public delegate void MyDelegate(string s, object o, bool b);

        public static async Task DoGenerateClientManifestForAllAssetsAsync(AMSClientV3 amsClient, MyDelegate TextBoxLogWriteLine)
        {
            Telemetry.TrackEvent("ClientManifestUtils DoGenerateClientManifestForAllAssetsAsync");

            if (MessageBox.Show("The tool will list the published assets and will create a client manifest when needed.", "Client manifest creation", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) != DialogResult.OK)
            {
                return;
            }

            /*
            ListContainerSasInput input = new()
            {
                Permissions = AssetContainerPermission.ReadWriteDelete,
                ExpiryTime = DateTime.Now.AddHours(2).ToUniversalTime()
            };
            */

            MediaAssetStorageContainerSasContent content = new()
            {
                Permissions = MediaAssetContainerPermission.Read,
                ExpireOn = DateTime.Now.AddHours(2).ToUniversalTime()
            };

            // Get a list of all of the locators and enumerate through them a page at a time.
            var locators = amsClient.AMSclient.GetStreamingLocators();

            foreach (var locator in locators)
            {
                TextBoxLogWriteLine("Inspecting locator {0}...", locator.Data.Name, false);

                // Get the asset associated with the locator.
                //Asset asset = amsClient.AMSclient.Assets.Get(amsClient.credentialsEntry.ResourceGroup, amsClient.credentialsEntry.AccountName, locator.AssetName);
                var asset = amsClient.AMSclient.GetMediaAsset(locator.Data.AssetName).Value;

                Pageable<Uri> response;
                try
                {
                    response = asset.GetStorageContainerUris(content);
                    //response = await amsClient.AMSclient.Assets.ListContainerSasAsync(amsClient.credentialsEntry.ResourceGroup, amsClient.credentialsEntry.AccountName, asset.Name, input.Permissions, input.ExpiryTime);
                }
                catch (Exception ex)
                {
                    TextBoxLogWriteLine("Error when listing blobs of asset '{0}'.", asset.Data.Name, true); // Warning
                    TextBoxLogWriteLine(Program.GetErrorMessage(ex), string.Empty, true); // Warning

                    //MessageBox.Show(Program.GetErrorMessage(ex), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                //string uploadSasUrl = response.AssetContainerSasUrls.First();
                //Uri sasUri = new(uploadSasUrl);
                CloudBlobContainer storageContainer = new(response.First());

                // Get a manifest file list from the Storage container.
                List<string> fileList = GetFilesListFromStorage(storageContainer);

                string ismcFileName = fileList.Where(a => a.ToLower().Contains(".ismc")).FirstOrDefault();
                string ismManifestFileName = fileList.Where(a => a.ToLower().EndsWith(".ism")).FirstOrDefault();
                // If there is no .ism then there's no reason to continue.  If there's no .ismc we need to add it.
                if (ismManifestFileName != null && ismcFileName == null)
                {
                    DialogResult dialog = MessageBox.Show($"Asset {asset.Data.Name} it does not have an ISMC file. Create one ?", "Client manifest creation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (dialog == DialogResult.Yes)
                    {
                        TextBoxLogWriteLine("Asset {0} : it does not have an ISMC file.", asset.Data.Name, false);

                        // let's try to read client manifest
                        XDocument manifest = null;
                        try
                        {
                            manifest = await AssetTools.TryToGetClientManifestContentUsingStreamingLocatorAsync(asset, amsClient, locator.Data.Name);
                        }
                        catch (Exception ex)
                        {
                            TextBoxLogWriteLine("Error when trying to read client manifest for asset '{0}'.", asset.Data.Name, true); // Warning
                            TextBoxLogWriteLine(Program.GetErrorMessage(ex), string.Empty, true); // Warning
                            return;
                        }

                        string ismcContentXml = manifest.ToString();
                        if (ismcContentXml.Length == 0)
                        {
                            TextBoxLogWriteLine("Asset {0} : client manifest is empty.", asset.Data.Name, true); // Warning

                            //error state, skip this asset
                            continue;
                        }
                        if (ismcContentXml.IndexOf("<Protection>") > 0)
                        {
                            TextBoxLogWriteLine("Asset {0} : content is encrypted. Removing the protection header from the client manifest.", asset.Data.Name, false);
                            //remove DRM from the ISCM manifest
                            ismcContentXml = XmlManifestUtils.RemoveXmlNode(ismcContentXml);
                        }
                        string newIsmcFileName = ismManifestFileName.Substring(0, ismManifestFileName.IndexOf(".")) + ".ismc";
                        CloudBlockBlob ismcBlob = WriteStringToBlob(ismcContentXml, newIsmcFileName, storageContainer);
                        TextBoxLogWriteLine("Asset {0} : client manifest created.", asset.Data.Name, false);

                        // Download the ISM so that we can modify it to include the ISMC file link.
                        string ismXmlContent = GetFileXmlFromStorage(storageContainer, ismManifestFileName);
                        ismXmlContent = XmlManifestUtils.AddIsmcToIsm(ismXmlContent, newIsmcFileName);
                        WriteStringToBlob(ismXmlContent, ismManifestFileName, storageContainer);
                        TextBoxLogWriteLine("Asset {0} : server manifest updated.", asset.Data.Name, false);

                        // update the ism to point to the ismc (download, modify, delete original, upload new)
                    }
                    else if (dialog == DialogResult.Cancel)
                    {
                        //cancel = true;
                        break;
                    }
                }
            }

            MessageBox.Show("Locator listing is complete.", "Client manifest creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            TextBoxLogWriteLine("Locator listing is complete.", string.Empty, false);
        }

        private static string GetFileXmlFromStorage(CloudBlobContainer storageContainer, string ismManifestFileName)
        {
            CloudBlockBlob blob = storageContainer.GetBlockBlobReference(ismManifestFileName);
            return blob.DownloadText();
        }

        private static CloudBlockBlob WriteStringToBlob(string ContentXml, string fileName, CloudBlobContainer storageContainer)
        {
            CloudBlockBlob newBlob = storageContainer.GetBlockBlobReference(fileName);
            newBlob.UploadText(ContentXml);
            return newBlob;
        }

        private static List<string> GetFilesListFromStorage(CloudBlobContainer storageContainer)
        {
            List<CloudBlockBlob> fullBlobList = storageContainer.ListBlobs().OfType<CloudBlockBlob>().ToList();
            // Filter the list to only contain .ism and .ismc files
            IEnumerable<string> filteredList = from b in fullBlobList
                                               where b.Name.ToLower().Contains(".ism")
                                               select b.Name;
            return filteredList.ToList();
        }
    }
}
