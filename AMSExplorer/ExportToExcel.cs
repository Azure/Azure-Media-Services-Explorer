//----------------------------------------------------------------------------------------------
//    Copyright 2015 Microsoft Corporation
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


namespace AMSExplorer
{
    public partial class ExportToExcel : Form
    {
        private CloudMediaContext _context;
        private IEnumerable<IAsset> _selassets;
        private IEnumerable<IAsset> _visibleassets;
        private string filename;


        public ExportToExcel(CloudMediaContext context, IEnumerable<IAsset> selassets, IEnumerable<IAsset> visibleassets)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            _context = context;
            _selassets = selassets;
            _visibleassets = visibleassets;

            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;
        }


        private void ExportToExcel_Load(object sender, EventArgs e)
        {
            textBoxExcelFile.Text = string.Format("{0}\\Export-{1}-{2}.xlsx", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), _context.Credentials.ClientId, DateTime.Now.ToString("dMMMyyyy"));
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            buttonOk.Enabled = false;
            filename = textBoxExcelFile.Text;

            backgroundWorker1.RunWorkerAsync();
        }

        private void ExportAssetExcel(IAsset asset, Excel.Worksheet xlWorkSheet, int row, bool detailed, bool localtime)
        {
            xlWorkSheet.Cells[row, 1] = asset.Name;
            xlWorkSheet.Cells[row, 2] = asset.Id;
            xlWorkSheet.Cells[row, 3] = localtime ? asset.LastModified.ToLocalTime() : asset.LastModified;
            xlWorkSheet.Cells[row, 4] = AssetInfo.GetAssetType(asset);
            xlWorkSheet.Cells[row, 5] = AssetInfo.GetSize(asset);
            var url = AssetInfo.GetValidOnDemandURI(asset);
            xlWorkSheet.Cells[row, 6] = url != null ? url.ToString() : string.Empty;

            if (localtime)
            {
                xlWorkSheet.Cells[row, 7] = asset.Locators.Any() ? (DateTime?)asset.Locators.Max(l => l.ExpirationDateTime).ToLocalTime() : null;
            }
            else
            {
                xlWorkSheet.Cells[row, 7] = asset.Locators.Any() ? (DateTime?)asset.Locators.Max(l => l.ExpirationDateTime) : null;
            }

            if (detailed)
            {
                xlWorkSheet.Cells[row, 8] = asset.AlternateId;
                xlWorkSheet.Cells[row, 9] = asset.StorageAccount.Name;
                var streamingloc = asset.Locators.Where(l => l.Type == LocatorType.OnDemandOrigin);
                xlWorkSheet.Cells[row, 10] = streamingloc.Count();
                if (localtime)
                {
                    xlWorkSheet.Cells[row, 11] = streamingloc.Any() ? (DateTime?)streamingloc.Min(l => l.ExpirationDateTime).ToLocalTime() : null;
                    xlWorkSheet.Cells[row, 12] = streamingloc.Any() ? (DateTime?)streamingloc.Max(l => l.ExpirationDateTime).ToLocalTime() : null;
                }
                else
                {
                    xlWorkSheet.Cells[row, 11] = streamingloc.Any() ? (DateTime?)streamingloc.Min(l => l.ExpirationDateTime) : null;
                    xlWorkSheet.Cells[row, 12] = streamingloc.Any() ? (DateTime?)streamingloc.Max(l => l.ExpirationDateTime) : null;
                }

                var sasloc = asset.Locators.Where(l => l.Type == LocatorType.Sas);
                xlWorkSheet.Cells[row, 13] = sasloc.Count();
                if (localtime)
                {
                    xlWorkSheet.Cells[row, 14] = sasloc.Any() ? (DateTime?)sasloc.Min(l => l.ExpirationDateTime).ToLocalTime() : null;
                    xlWorkSheet.Cells[row, 15] = sasloc.Any() ? (DateTime?)sasloc.Max(l => l.ExpirationDateTime).ToLocalTime() : null;
                }
                else
                {
                    xlWorkSheet.Cells[row, 14] = sasloc.Any() ? (DateTime?)sasloc.Min(l => l.ExpirationDateTime) : null;
                    xlWorkSheet.Cells[row, 15] = sasloc.Any() ? (DateTime?)sasloc.Max(l => l.ExpirationDateTime) : null;
                }

                xlWorkSheet.Cells[row, 16] = asset.GetEncryptionState(AssetDeliveryProtocol.SmoothStreaming | AssetDeliveryProtocol.HLS | AssetDeliveryProtocol.Dash).ToString();
            }
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
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
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

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
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
            chartRange.FormulaR1C1 = string.Format("{0} assets information, media account '{1}'", radioButtonAllAssets.Checked ? "All" : "Selected", _context.Credentials.ClientId);
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
            xlWorkSheet.Cells[row, 1] = "Asset Name";
            xlWorkSheet.Cells[row, 2] = "Id";
            xlWorkSheet.Cells[row, 3] = "Last Modified";
            xlWorkSheet.Cells[row, 4] = "Type";
            xlWorkSheet.Cells[row, 5] = "Size";
            xlWorkSheet.Cells[row, 6] = "Streaming URL";
            xlWorkSheet.Cells[row, 7] = "Expiration time";
            if (detailed)
            {
                xlWorkSheet.Cells[row, 8] = "Alternate Id";
                xlWorkSheet.Cells[row, 9] = "Storage Account";
                xlWorkSheet.Cells[row, 10] = "Streaming Locators Count";
                xlWorkSheet.Cells[row, 11] = "Streaming Min Expiration time";
                xlWorkSheet.Cells[row, 12] = "Streaming Max Expiration time";
                xlWorkSheet.Cells[row, 13] = "SAS Locators Count";
                xlWorkSheet.Cells[row, 14] = "SAS Min Expiration time";
                xlWorkSheet.Cells[row, 15] = "SAS Max Expiration time";
                xlWorkSheet.Cells[row, 16] = "Dynamic encryption";
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
                int index = 1;

                while (true)
                {
                    IQueryable _assetsCollectionQuery = _context.Assets.Skip(skipSize).Take(batchSize);
                    foreach (IAsset asset in _assetsCollectionQuery)
                    {
                        row++;
                        currentBatch++;
                        ExportAssetExcel(asset, xlWorkSheet, row, detailed, checkBoxLocalTime.Checked);

                        backgroundWorker1.ReportProgress(100 * index / total, DateTime.Now); //notify progress to main thread. We also pass time information in UserState to cover this property in the example.  
                        //if cancellation is pending, cancel work.  
                        if (backgroundWorker1.CancellationPending)
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
                        index++;
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
                int index = 1;

                foreach (IAsset asset in myassets)
                {
                    row++;
                    ExportAssetExcel(asset, xlWorkSheet, row, detailed, checkBoxLocalTime.Checked);
                    backgroundWorker1.ReportProgress(100 * index / total, DateTime.Now); //notify progress to main thread. We also pass time information in UserState to cover this property in the example.  
                    //if cancellation is pending, cancel work.  
                    if (backgroundWorker1.CancellationPending)
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
                    index++;
                }
            }

            // Set the range to fill.
            if (detailed)
            {
                var aRange = xlWorkSheet.get_Range("A4", "P100");
                aRange.EntireColumn.AutoFit();
            }
            else
            {
                var aRange = xlWorkSheet.get_Range("A4", "F100");
                aRange.EntireColumn.AutoFit();
            }
          
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
                MessageBox.Show("Error when saving the Excel file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (checkBoxOpenFileAfterExport.Checked) System.Diagnostics.Process.Start(filename);
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
            backgroundWorker1.CancelAsync();
        }
    }

    public enum AssetSelection
    {
        Selected = 0,
        Displayed,
        All
    }
}
