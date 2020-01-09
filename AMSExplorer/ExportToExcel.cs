//----------------------------------------------------------------------------------------------
//    Copyright 2020 Microsoft Corporation
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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAzure.MediaServices.Client;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.WindowsAzure.MediaServices.Client.DynamicEncryption;
using System.Reflection;
using System.IO;

namespace AMSExplorer
{
    public partial class ExportToExcel : Form
    {
        private CloudMediaContext _context;
        private IEnumerable<IAsset> _selassets;
        private IEnumerable<IAsset> _visibleassets;
        private string filename;
        private string _accountname;

        public ExportToExcel(CloudMediaContext context, string accountname, IEnumerable<IAsset> selassets, IEnumerable<IAsset> visibleassets)
        {
            InitializeComponent();
            _accountname = accountname;
            this.Icon = Bitmaps.Azure_Explorer_ico;
            _context = context;
            _selassets = selassets;
            _visibleassets = visibleassets;

            backgroundWorkerExcel.WorkerReportsProgress = true;
            backgroundWorkerExcel.WorkerSupportsCancellation = true;
        }


        private void ExportToExcel_Load(object sender, EventArgs e)
        {
            string extension = radioButtonFormatExcel.Checked ? "xlsx" : "csv";
            textBoxExcelFile.Text = string.Format("{0}\\Export-{1}-{2}." + extension, Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), _accountname, DateTime.Now.ToString("dMMMyyyy"));

        }

        private void UpdateFilePathAndname()
        {
            string extension = radioButtonFormatExcel.Checked ? "xlsx" : "csv";
            string oldExtension = !radioButtonFormatExcel.Checked ? "xlsx" : "csv";

            if (textBoxExcelFile.Text.ToLower().EndsWith(oldExtension))
            {
                textBoxExcelFile.Text = textBoxExcelFile.Text.Substring(0, textBoxExcelFile.Text.Length - oldExtension.Length) + extension;
            }
            string filter = radioButtonFormatExcel.Checked ? "Excel files| *.xlsx | All files | *.*" : "CSV files| *.csv | All files | *.*";
            saveFileDialog1.Filter = filter;

        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            buttonOk.Enabled = false;
            filename = textBoxExcelFile.Text;

            if (radioButtonFormatExcel.Checked)
            {
                backgroundWorkerExcel.RunWorkerAsync();

            }
            else
            {
                backgroundWorkerCSV.RunWorkerAsync();

            }
        }

        private void ExportAssetExcel(IAsset asset, Excel.Worksheet xlWorkSheet, int row, bool detailed, bool localtime)
        {
            int index = 1;
            xlWorkSheet.Cells[row, index++] = asset.Name;
            xlWorkSheet.Cells[row, index++] = asset.Id;
            xlWorkSheet.Cells[row, index++] = localtime ? asset.LastModified.ToLocalTime() : asset.LastModified;
            xlWorkSheet.Cells[row, index++] = AssetInfo.GetAssetType(asset);
            xlWorkSheet.Cells[row, index++] = AssetInfo.GetSize(asset);
            int backindex = index;
            var urls = AssetInfo.GetURIs(asset);
            if (urls != null)
            {
                foreach (var url in urls)
                {
                    xlWorkSheet.Cells[row, index++] = url != null ? url.ToString() : string.Empty;
                }

            }
            index = backindex + _context.StreamingEndpoints.Count();
            var streamlocators = asset.Locators.Where(l => l.Type == LocatorType.OnDemandOrigin);
            if (streamlocators.Any())
            {
                if (localtime)
                {
                    xlWorkSheet.Cells[row, index++] = (DateTime?)streamlocators.Max(l => l.ExpirationDateTime).ToLocalTime();
                }
                else
                {
                    xlWorkSheet.Cells[row, index++] = (DateTime?)streamlocators.Max(l => l.ExpirationDateTime);
                }
            }
            else
            {
                xlWorkSheet.Cells[row, index++] = string.Empty;
            }


            // SAS locator
            var saslocators = asset.Locators.Where(l => l.Type == LocatorType.Sas);
            var saslocator = saslocators.ToList().OrderByDescending(l => l.ExpirationDateTime).FirstOrDefault();
            if (saslocator != null && asset.AssetFiles.Count() > 0)
            {
                if (asset.AssetFiles.Count() == 1)
                {
                    var ProgressiveDownloadUri = asset.AssetFiles.FirstOrDefault().GetSasUri(saslocator);
                    xlWorkSheet.Cells[row, index++] = ProgressiveDownloadUri.AbsoluteUri;
                }
                else
                {
                    xlWorkSheet.Cells[row, index++] = saslocator.Path;
                }

                if (localtime)
                {
                    xlWorkSheet.Cells[row, index++] = saslocator.ExpirationDateTime.ToLocalTime();
                }
                else
                {
                    xlWorkSheet.Cells[row, index++] = saslocator.ExpirationDateTime;
                }
            }
            else
            {
                xlWorkSheet.Cells[row, index++] = string.Empty;
                xlWorkSheet.Cells[row, index++] = string.Empty;
            }


            if (detailed)
            {
                xlWorkSheet.Cells[row, index++] = asset.AlternateId;
                xlWorkSheet.Cells[row, index++] = asset.StorageAccount.Name;
                xlWorkSheet.Cells[row, index++] = asset.Uri == null ? string.Empty : asset.Uri.ToString();
                var streamingloc = asset.Locators.Where(l => l.Type == LocatorType.OnDemandOrigin);
                xlWorkSheet.Cells[row, index++] = streamingloc.Count();
                if (localtime)
                {
                    xlWorkSheet.Cells[row, index++] = streamingloc.Any() ? (DateTime?)streamingloc.Min(l => l.ExpirationDateTime).ToLocalTime() : null;
                    xlWorkSheet.Cells[row, index++] = streamingloc.Any() ? (DateTime?)streamingloc.Max(l => l.ExpirationDateTime).ToLocalTime() : null;
                }
                else
                {
                    xlWorkSheet.Cells[row, index++] = streamingloc.Any() ? (DateTime?)streamingloc.Min(l => l.ExpirationDateTime) : null;
                    xlWorkSheet.Cells[row, index++] = streamingloc.Any() ? (DateTime?)streamingloc.Max(l => l.ExpirationDateTime) : null;
                }

                // SAS
                xlWorkSheet.Cells[row, index++] = saslocators.Count();
                if (localtime)
                {
                    xlWorkSheet.Cells[row, index++] = saslocators.Any() ? (DateTime?)saslocators.Min(l => l.ExpirationDateTime).ToLocalTime() : null;
                    xlWorkSheet.Cells[row, index++] = saslocators.Any() ? (DateTime?)saslocators.Max(l => l.ExpirationDateTime).ToLocalTime() : null;
                }
                else
                {
                    xlWorkSheet.Cells[row, index++] = saslocators.Any() ? (DateTime?)saslocators.Min(l => l.ExpirationDateTime) : null;
                    xlWorkSheet.Cells[row, index++] = saslocators.Any() ? (DateTime?)saslocators.Max(l => l.ExpirationDateTime) : null;
                }

                xlWorkSheet.Cells[row, index++] = asset.GetEncryptionState(AssetDeliveryProtocol.SmoothStreaming | AssetDeliveryProtocol.HLS | AssetDeliveryProtocol.Dash).ToString();
                xlWorkSheet.Cells[row, index++] = asset.AssetFilters.Count().ToString();

            }
        }

        private string ExportAssetCSVLine(IAsset asset, bool detailed, bool localtime)
        {
            List<string> linec = new List<string>();
            linec.Add(asset.Name);
            linec.Add(asset.Id);
            linec.Add(localtime ? asset.LastModified.ToLocalTime().ToString() : asset.LastModified.ToString());
            linec.Add(AssetInfo.GetAssetType(asset));
            linec.Add(AssetInfo.GetSize(asset).ToString());
            var urls = AssetInfo.GetURIs(asset);
            if (urls != null && urls.Count() > 0)
            {
                foreach (var url in urls)
                {
                    linec.Add(url != null ? url.ToString() : string.Empty);
                }
            }
            else
            {
                for (int i = 0; i < _context.StreamingEndpoints.Count(); i++)
                {
                    linec.Add(string.Empty);
                }
            }

            var streamlocators = asset.Locators.Where(l => l.Type == LocatorType.OnDemandOrigin);
            if (streamlocators.Any())
            {
                if (localtime)
                {
                    linec.Add(streamlocators.Max(l => l.ExpirationDateTime).ToLocalTime().ToString());
                }
                else
                {
                    linec.Add(streamlocators.Max(l => l.ExpirationDateTime).ToString());
                }
            }
            else
            {
                linec.Add(string.Empty);
            }


            // SAS locator
            var saslocators = asset.Locators.Where(l => l.Type == LocatorType.Sas);
            var saslocator = saslocators.ToList().OrderByDescending(l => l.ExpirationDateTime).FirstOrDefault();
            if (saslocator != null && asset.AssetFiles.Count() > 0)
            {
                if (asset.AssetFiles.Count() == 1)
                {
                    var ProgressiveDownloadUri = asset.AssetFiles.FirstOrDefault().GetSasUri(saslocator);
                    linec.Add(ProgressiveDownloadUri.AbsoluteUri);
                }
                else
                {
                    linec.Add(saslocator.Path);
                }

                if (localtime)
                {
                    linec.Add(saslocator.ExpirationDateTime.ToLocalTime().ToString());
                }
                else
                {
                    linec.Add(saslocator.ExpirationDateTime.ToString());
                }
            }
            else
            {
                linec.Add(string.Empty);
                linec.Add(string.Empty);
            }


            if (detailed)
            {
                linec.Add(asset.AlternateId);
                linec.Add(asset.StorageAccount.Name);
                linec.Add(asset.Uri == null ? string.Empty : asset.Uri.ToString());
                var streamingloc = asset.Locators.Where(l => l.Type == LocatorType.OnDemandOrigin);
                linec.Add(streamingloc.Count().ToString());
                if (localtime)
                {
                    linec.Add(streamingloc.Any() ? streamingloc.Min(l => l.ExpirationDateTime).ToLocalTime().ToString() : null);
                    linec.Add(streamingloc.Any() ? streamingloc.Max(l => l.ExpirationDateTime).ToLocalTime().ToString() : null);
                }
                else
                {
                    linec.Add(streamingloc.Any() ? streamingloc.Min(l => l.ExpirationDateTime).ToString() : null);
                    linec.Add(streamingloc.Any() ? streamingloc.Max(l => l.ExpirationDateTime).ToString() : null);
                }

                // SAS
                linec.Add(saslocators.Count().ToString());
                if (localtime)
                {
                    linec.Add(saslocators.Any() ? saslocators.Min(l => l.ExpirationDateTime).ToLocalTime().ToString() : null);
                    linec.Add(saslocators.Any() ? saslocators.Max(l => l.ExpirationDateTime).ToLocalTime().ToString() : null);
                }
                else
                {
                    linec.Add(saslocators.Any() ? saslocators.Min(l => l.ExpirationDateTime).ToString() : null);
                    linec.Add(saslocators.Any() ? saslocators.Max(l => l.ExpirationDateTime).ToString() : null);
                }

                linec.Add(asset.GetEncryptionState(AssetDeliveryProtocol.SmoothStreaming | AssetDeliveryProtocol.HLS | AssetDeliveryProtocol.Dash).ToString());
                linec.Add(asset.AssetFilters.Count().ToString());

            }

            return convertCSVLine(linec);
        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show(string.Format(AMSExplorer.Properties.Resources.ExportToExcel_releaseObject_ExceptionOccuredWhileReleasingObject0, ex.ToString()));
            }
            finally
            {
                GC.Collect();
            }
        }

        private void buttonBrowseFile_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBoxExcelFile.Text = saveFileDialog1.FileName;
            }
        }

        private void backgroundWorkerExcel_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {

                bool detailed = radioButtonDetailledMode.Checked;
                Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

                if (xlApp == null)
                {
                    MessageBox.Show("Excel is not properly installed!!");
                    return;
                }

                Excel.Workbook xlWorkBook;
                Excel.Worksheet xlWorkSheet;
                object misValue = System.Reflection.Missing.Value;

                xlWorkBook = xlApp.Workbooks.Add(misValue);
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                xlWorkSheet.get_Range("a1", "f1").Merge(false);
                Excel.Range chartRange = xlWorkSheet.get_Range("a1", "f1");
                if (radioButtonAllAssets.Checked)
                {
                    chartRange.FormulaR1C1 = string.Format(AMSExplorer.Properties.Resources.ExportToExcel_backgroundWorker1_DoWork_AllAssetsInformationMediaAccount0, _accountname);
                }
                else
                {
                    chartRange.FormulaR1C1 = string.Format(AMSExplorer.Properties.Resources.ExportToExcel_backgroundWorker1_DoWork_SelectedAssetsInformationMediaAccount0, _accountname);
                }
                chartRange.VerticalAlignment = 3;
                chartRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkBlue);
                chartRange.Font.Size = 20;

                xlWorkSheet.get_Range("a2", "f2").Merge(false);
                Excel.Range chartRange2 = xlWorkSheet.get_Range("a2", "f2");
                chartRange2.FormulaR1C1 = string.Format("Exported with Azure Media Services Explorer v{0} on {1}. Dates are {2}.",
                    Assembly.GetExecutingAssembly().GetName().Version.ToString(),
                    checkBoxLocalTime.Checked ? DateTime.Now.ToString() : DateTime.UtcNow.ToString(),
                    checkBoxLocalTime.Checked ? "local" : "UTC based"
                    );
                chartRange2.VerticalAlignment = 3;
                chartRange2.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkBlue);
                chartRange2.Font.Size = 12;

                int row = 4;
                int index = 1;
                xlWorkSheet.Cells[row, index++] = AMSExplorer.Properties.Resources.BulkContainerInfo_DoDisplayAssetManifest_AssetName;
                xlWorkSheet.Cells[row, index++] = "Id";
                xlWorkSheet.Cells[row, index++] = AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_LastModified;
                xlWorkSheet.Cells[row, index++] = AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_Type;
                xlWorkSheet.Cells[row, index++] = AMSExplorer.Properties.Resources.ExportToExcel_backgroundWorker1_DoWork_Size;
                int backindex = index;
                _context.StreamingEndpoints.ToList().ForEach(se =>
                xlWorkSheet.Cells[row, index++] = AMSExplorer.Properties.Resources.ExportToExcel_backgroundWorker1_DoWork_StreamingURL
                );
                index = backindex + _context.StreamingEndpoints.Count();
                xlWorkSheet.Cells[row, index++] = AMSExplorer.Properties.Resources.ExportToExcel_backgroundWorker1_DoWork_StreamingExpirationTime;

                xlWorkSheet.Cells[row, index++] = AMSExplorer.Properties.Resources.ExportToExcel_backgroundWorker1_DoWork_SASURL;
                xlWorkSheet.Cells[row, index++] = AMSExplorer.Properties.Resources.ExportToExcel_backgroundWorker1_DoWork_SASExpirationTime;

                if (detailed)
                {
                    xlWorkSheet.Cells[row, index++] = AMSExplorer.Properties.Resources.ExportToExcel_backgroundWorker1_DoWork_AlternateId;
                    xlWorkSheet.Cells[row, index++] = AMSExplorer.Properties.Resources.ExportToExcel_backgroundWorker1_DoWork_StorageAccount;
                    xlWorkSheet.Cells[row, index++] = AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_StorageUrl;
                    xlWorkSheet.Cells[row, index++] = AMSExplorer.Properties.Resources.ExportToExcel_backgroundWorker1_DoWork_StreamingLocatorsCount;
                    xlWorkSheet.Cells[row, index++] = AMSExplorer.Properties.Resources.ExportToExcel_backgroundWorker1_DoWork_StreamingMinExpirationTime;
                    xlWorkSheet.Cells[row, index++] = AMSExplorer.Properties.Resources.ExportToExcel_backgroundWorker1_DoWork_StreamingMaxExpirationTime;
                    xlWorkSheet.Cells[row, index++] = AMSExplorer.Properties.Resources.ExportToExcel_backgroundWorker1_DoWork_SASLocatorsCount;
                    xlWorkSheet.Cells[row, index++] = AMSExplorer.Properties.Resources.ExportToExcel_backgroundWorker1_DoWork_SASMinExpirationTime;
                    xlWorkSheet.Cells[row, index++] = AMSExplorer.Properties.Resources.ExportToExcel_backgroundWorker1_DoWork_SASMaxExpirationTime;
                    xlWorkSheet.Cells[row, index++] = AMSExplorer.Properties.Resources.ExportToExcel_backgroundWorker1_DoWork_DynamicEncryption;
                    xlWorkSheet.Cells[row, index++] = AMSExplorer.Properties.Resources.ExportToExcel_backgroundWorker1_DoWork_AssetFiltersCount;
                }

                Excel.Range formatRange;
                formatRange = xlWorkSheet.get_Range("a4");
                formatRange.EntireRow.Font.Bold = true;
                formatRange.EntireRow.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightBlue);


                if (radioButtonAllAssets.Checked)
                {
                    int skipSize = 0;
                    int batchSize = 1000;
                    int currentBatch = 0;

                    int total = _context.Assets.Count();
                    int index2 = 1;

                    while (true)
                    {
                        IQueryable _assetsCollectionQuery = _context.Assets.Skip(skipSize).Take(batchSize);
                        foreach (IAsset asset in _assetsCollectionQuery)
                        {
                            row++;
                            currentBatch++;
                            ExportAssetExcel(asset, xlWorkSheet, row, detailed, checkBoxLocalTime.Checked);

                            backgroundWorkerExcel.ReportProgress(100 * index2 / total, DateTime.Now); //notify progress to main thread. We also pass time information in UserState to cover this property in the example.  
                                                                                                      //if cancellation is pending, cancel work.  
                            if (backgroundWorkerExcel.CancellationPending)
                            {
                                xlApp.DisplayAlerts = false;
                                xlWorkBook.Close();
                                xlApp.Quit();
                                releaseObject(xlWorkSheet);
                                releaseObject(xlWorkBook);
                                releaseObject(xlApp);
                                e.Cancel = true;
                                return;
                            }
                            index2++;
                        }

                        if (currentBatch == batchSize)
                        {
                            skipSize += batchSize;
                            currentBatch = 0;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                else // Selected or visible asets
                {
                    IEnumerable<IAsset> myassets;
                    if (radioButtonSelectedAssets.Checked)
                    {
                        myassets = _selassets;
                    }
                    else
                    {
                        myassets = _visibleassets;
                    }

                    int total = myassets.Count();
                    int index3 = 1;

                    foreach (IAsset asset in myassets)
                    {
                        row++;
                        ExportAssetExcel(asset, xlWorkSheet, row, detailed, checkBoxLocalTime.Checked);
                        backgroundWorkerExcel.ReportProgress(100 * index3 / total, DateTime.Now); //notify progress to main thread. We also pass time information in UserState to cover this property in the example.  
                                                                                                  //if cancellation is pending, cancel work.  
                        if (backgroundWorkerExcel.CancellationPending)
                        {
                            xlApp.DisplayAlerts = false;
                            xlWorkBook.Close();
                            xlApp.Quit();
                            releaseObject(xlWorkSheet);
                            releaseObject(xlWorkBook);
                            releaseObject(xlApp);
                            e.Cancel = true;
                            return;
                        }
                        index3++;
                    }
                }

                // Set the range to fill.

                var aRange = xlWorkSheet.get_Range("A4", "Z100");
                aRange.EntireColumn.AutoFit();

                try
                {
                    xlWorkBook.SaveAs(filename, Excel.XlFileFormat.xlWorkbookDefault, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                    xlWorkBook.Close(true, misValue, misValue);
                    xlApp.Quit();
                    releaseObject(xlWorkSheet);
                    releaseObject(xlWorkBook);
                    releaseObject(xlApp);
                }
                catch
                {
                    MessageBox.Show(AMSExplorer.Properties.Resources.ExportToExcel_backgroundWorker1_DoWork_ErrorWhenSavingTheExcelFile, AMSExplorer.Properties.Resources.AMSLogin_buttonExport_Click_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (checkBoxOpenFileAfterExport.Checked) System.Diagnostics.Process.Start(filename);
            }
            catch (Exception ex)
            {
                MessageBox.Show(AMSExplorer.Properties.Resources.ExportToExcel_backgroundWorker1_DoWork_Error + ex.Message);
            }
        }

        private void backgroundWorkerCSV_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {

                bool detailed = radioButtonDetailledMode.Checked;
                var csv = new StringBuilder();

                if (radioButtonAllAssets.Checked)
                {
                    csv.AppendLine(checkStringForCSV(string.Format(AMSExplorer.Properties.Resources.ExportToExcel_backgroundWorker1_DoWork_AllAssetsInformationMediaAccount0, _accountname)));
                }
                else
                {
                    csv.AppendLine(checkStringForCSV(string.Format(AMSExplorer.Properties.Resources.ExportToExcel_backgroundWorker1_DoWork_SelectedAssetsInformationMediaAccount0,_accountname)));
                }

                csv.AppendLine(checkStringForCSV(string.Format("Exported with Azure Media Services Explorer v{0} on {1}. Dates are {2}.",
                    Assembly.GetExecutingAssembly().GetName().Version.ToString(),
                    checkBoxLocalTime.Checked ? DateTime.Now.ToString() : DateTime.UtcNow.ToString(),
                    checkBoxLocalTime.Checked ? "local" : "UTC based"
                    )));

                List<string> linec = new List<string>();
                linec.Add(AMSExplorer.Properties.Resources.BulkContainerInfo_DoDisplayAssetManifest_AssetName);
                linec.Add("Id");

                linec.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_LastModified);
                linec.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_Type);
                linec.Add(AMSExplorer.Properties.Resources.ExportToExcel_backgroundWorker1_DoWork_Size);

                _context.StreamingEndpoints.ToList().ForEach(se =>
                linec.Add(AMSExplorer.Properties.Resources.ExportToExcel_backgroundWorker1_DoWork_StreamingURL));
                linec.Add(AMSExplorer.Properties.Resources.ExportToExcel_backgroundWorker1_DoWork_StreamingExpirationTime);

                linec.Add(AMSExplorer.Properties.Resources.ExportToExcel_backgroundWorker1_DoWork_SASURL);
                linec.Add(AMSExplorer.Properties.Resources.ExportToExcel_backgroundWorker1_DoWork_SASExpirationTime);

                if (detailed)
                {
                    linec.Add(AMSExplorer.Properties.Resources.ExportToExcel_backgroundWorker1_DoWork_AlternateId);
                    linec.Add(AMSExplorer.Properties.Resources.ExportToExcel_backgroundWorker1_DoWork_StorageAccount);
                    linec.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_StorageUrl);
                    linec.Add(AMSExplorer.Properties.Resources.ExportToExcel_backgroundWorker1_DoWork_StreamingLocatorsCount);
                    linec.Add(AMSExplorer.Properties.Resources.ExportToExcel_backgroundWorker1_DoWork_StreamingMinExpirationTime);
                    linec.Add(AMSExplorer.Properties.Resources.ExportToExcel_backgroundWorker1_DoWork_StreamingMaxExpirationTime);
                    linec.Add(AMSExplorer.Properties.Resources.ExportToExcel_backgroundWorker1_DoWork_SASLocatorsCount);
                    linec.Add(AMSExplorer.Properties.Resources.ExportToExcel_backgroundWorker1_DoWork_SASMinExpirationTime);
                    linec.Add(AMSExplorer.Properties.Resources.ExportToExcel_backgroundWorker1_DoWork_SASMaxExpirationTime);
                    linec.Add(AMSExplorer.Properties.Resources.ExportToExcel_backgroundWorker1_DoWork_DynamicEncryption);
                    linec.Add(AMSExplorer.Properties.Resources.ExportToExcel_backgroundWorker1_DoWork_AssetFiltersCount);
                }

                csv.AppendLine(convertCSVLine(linec));

                if (radioButtonAllAssets.Checked)
                {
                    int skipSize = 0;
                    int batchSize = 1000;
                    int currentBatch = 0;

                    int total = _context.Assets.Count();
                    int index2 = 1;

                    while (true)
                    {
                        IQueryable _assetsCollectionQuery = _context.Assets.Skip(skipSize).Take(batchSize);
                        foreach (IAsset asset in _assetsCollectionQuery)
                        {
                            currentBatch++;

                            csv.AppendLine(ExportAssetCSVLine(asset, detailed, checkBoxLocalTime.Checked));

                            backgroundWorkerCSV.ReportProgress(100 * index2 / total, DateTime.Now); //notify progress to main thread. We also pass time information in UserState to cover this property in the example.  
                                                                                                    //if cancellation is pending, cancel work.  
                            if (backgroundWorkerCSV.CancellationPending)
                            {

                                e.Cancel = true;
                                return;
                            }
                            index2++;
                        }

                        if (currentBatch == batchSize)
                        {
                            skipSize += batchSize;
                            currentBatch = 0;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                else // Selected or visible assets
                {
                    IEnumerable<IAsset> myassets;
                    if (radioButtonSelectedAssets.Checked)
                    {
                        myassets = _selassets;
                    }
                    else
                    {
                        myassets = _visibleassets;
                    }

                    int total = myassets.Count();
                    int index3 = 1;

                    foreach (IAsset asset in myassets)
                    {
                        csv.AppendLine(ExportAssetCSVLine(asset, detailed, checkBoxLocalTime.Checked));
                        backgroundWorkerCSV.ReportProgress(100 * index3 / total, DateTime.Now); //notify progress to main thread. We also pass time information in UserState to cover this property in the example.  
                                                                                                //if cancellation is pending, cancel work.  
                        if (backgroundWorkerCSV.CancellationPending)
                        {
                            e.Cancel = true;
                            return;
                        }
                        index3++;
                    }
                }

                try
                {
                    File.WriteAllText(filename, csv.ToString());
                }
                catch
                {
                    MessageBox.Show(AMSExplorer.Properties.Resources.ExportToExcel_backgroundWorker1_DoWork_ErrorWhenSavingTheExcelFile, AMSExplorer.Properties.Resources.AMSLogin_buttonExport_Click_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (checkBoxOpenFileAfterExport.Checked) System.Diagnostics.Process.Start(filename);
            }
            catch (Exception ex)
            {
                MessageBox.Show(AMSExplorer.Properties.Resources.ExportToExcel_backgroundWorker1_DoWork_Error + ex.Message);
            }
        }



        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBarExport.Value = e.ProgressPercentage; //update progress bar  
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            buttonOk.Enabled = true;
            progressBarExport.Value = 0;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            backgroundWorkerExcel.CancelAsync();
            backgroundWorkerCSV.CancelAsync();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            UpdateFilePathAndname();
        }

        private string checkStringForCSV(string s)
        {
            string c = "\"";
            if (s != null && s.Contains(System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator))
            {
                s = c + s + c;
            }
            return s;
        }

        private string convertCSVLine(List<string> linec)
        {
            var newlinec = linec.Select(s => checkStringForCSV(s));
            return string.Join(System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator, newlinec);
        }

    }

    public enum AssetSelection
    {
        Selected = 0,
        Displayed,
        All
    }
}
