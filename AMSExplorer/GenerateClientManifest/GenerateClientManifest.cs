using Microsoft.Azure.Management.Media;
using Microsoft.Azure.Management.Media.Models;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Rest.Azure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AMSExplorer.GenerateClientManifest
{
    public class GenerateClientManifest
    {

        public delegate void MyDelegate(string s, object o, bool b);

        public static async Task DoGenerateClientManifestForAllAssetsAsync(AMSClientV3 amsClient, MyDelegate TextBoxLogWriteLine)
        {

            ListContainerSasInput input = new ListContainerSasInput()
            {
                Permissions = AssetContainerPermission.ReadWriteDelete,
                ExpiryTime = DateTime.Now.AddHours(2).ToUniversalTime()
            };
            await amsClient.RefreshTokenIfNeededAsync();


            // Get a list of all of the locators and enumerate through them a page at a time.
            IPage<StreamingLocator> firstPage = await amsClient.AMSclient.StreamingLocators.ListAsync(amsClient.credentialsEntry.ResourceGroup, amsClient.credentialsEntry.AccountName);
            IPage<StreamingLocator> currentPage = firstPage;

            do
            {
                bool always = false;
                foreach (StreamingLocator locator in currentPage)
                {
                    // Get the asset associated with the locator.
                    Asset asset = amsClient.AMSclient.Assets.Get(amsClient.credentialsEntry.ResourceGroup, amsClient.credentialsEntry.AccountName, locator.AssetName);

                    AssetContainerSas response;
                    try
                    {
                        response = await amsClient.AMSclient.Assets.ListContainerSasAsync(amsClient.credentialsEntry.ResourceGroup, amsClient.credentialsEntry.AccountName, asset.Name, input.Permissions, input.ExpiryTime);
                    }
                    catch (Exception ex)
                    {
                        //TextBoxLogWriteLine("Error when listing blobs of asset '{0}'.", asset.Name, true); // Warning

                        //MessageBox.Show(Program.GetErrorMessage(ex), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    string uploadSasUrl = response.AssetContainerSasUrls.First();
                    Uri sasUri = new Uri(uploadSasUrl);
                    CloudBlobContainer storageContainer = new CloudBlobContainer(sasUri);


                    // Get a manifest file list from the Storage container.
                    List<string> fileList = GetFilesListFromStorage(storageContainer);

                    string ismcFileName = fileList.Where(a => a.ToLower().Contains(".ismc")).FirstOrDefault();
                    string ismManifestFileName = fileList.Where(a => a.ToLower().EndsWith(".ism")).FirstOrDefault();
                    // If there is no .ism then there's no reason to continue.  If there's no .ismc we need to add it.
                    if (ismManifestFileName != null && ismcFileName == null)
                    {
                        Console.WriteLine("Asset {0} does not have an ISMC file.", asset.Name);

                        /*
                        if (!always)
                        {
                            //Console.WriteLine("Add the ISMC?  (y)es, (n)o, (a)lways, (q)uit");
                            //ConsoleKeyInfo response = Console.ReadKey();
                            string responseChar = response.Key.ToString();

                            if (responseChar.Equals("N"))
                                continue;
                            if (responseChar.Equals("A"))
                            {
                                always = true;
                            }
                            else if (!(responseChar.Equals("Y")))
                            {
                                break; // At this point anything other than a 'yes' should quit the loop/application.
                            }
                        }
                        */

                        // let's try to read client manifest
                        XDocument manifest = null;
                        try
                        {
                            manifest = await AssetInfo.TryToGetClientManifestContentUsingStreamingLocatorAsync(asset, amsClient, locator.Name);
                        }
                        catch (Exception ex)
                        {
                            //MessageBox.Show(Program.GetErrorMessage(ex), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }


                        string ismcContentXml = manifest.ToString();
                        if (ismcContentXml.Length == 0)
                        {
                            //error state, skip this asset
                            continue;
                        }
                        if (ismcContentXml.IndexOf("<Protection>") > 0)
                        {
                            Console.WriteLine("Content is encrypted. Removing the protection header from the client manifest.");
                            //remove DRM from the ISCM manifest
                            ismcContentXml = XmlManifest.RemoveXmlNode(ismcContentXml);
                        }
                        string newIsmcFileName = ismManifestFileName.Substring(0, ismManifestFileName.IndexOf(".")) + ".ismc";
                        CloudBlockBlob ismcBlob = WriteStringToBlob(ismcContentXml, newIsmcFileName, storageContainer);

                        // Download the ISM so that we can modify it to include the ISMC file link.
                        string ismXmlContent = GetFileXmlFromStorage(storageContainer, ismManifestFileName);
                        ismXmlContent = XmlManifest.AddIsmcToIsm(ismXmlContent, newIsmcFileName);
                        WriteStringToBlob(ismXmlContent, ismManifestFileName, storageContainer);
                        // update the ism to point to the ismc (download, modify, delete original, upload new)
                    }
                }
                // Continue on to the next page of locators.
                try
                {
                    currentPage = amsClient.AMSclient.StreamingLocators.ListNext(currentPage.NextPageLink);
                }
                catch (Exception)
                {
                    // we'll get here at the end of the page when the page is empty.  This is okay.
                }
            } while (currentPage.NextPageLink != null);
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


        private static string SendManifestRequest(Uri url)
        {
            string response = string.Empty;
            if (url.IsWellFormedOriginalString())
            {
                HttpWebRequest myHttpWebRequest = null;
                HttpWebResponse myHttpWebResponse = null;
                try
                {
                    // Creates an HttpWebRequest with the specified URL. 
                    myHttpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                    // Sends the HttpWebRequest and waits for the response.			
                    myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();

                    if (myHttpWebResponse.StatusCode == HttpStatusCode.OK)
                    {
                        using (Stream responseStream = myHttpWebResponse.GetResponseStream())
                        {
                            using (StreamReader reader = new StreamReader(responseStream))
                                response = reader.ReadToEnd();
                        }
                    }
                    myHttpWebResponse.Close();
                }
                catch (Exception e)
                {
                    //Console.WriteLine("Error: " + e.Message);
                }
            }
            return response;
        }

    }
}
