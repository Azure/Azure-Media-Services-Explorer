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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.IO;
using Microsoft.Azure.Management.Media.Models;
using Microsoft.Azure.Management.Media;
using Microsoft.Rest.Azure;

namespace AMSExplorer
{
    public partial class ExportToExcel : Form
    {
        private AMSClientV3 _amsClient;
        private List<Asset> _selassets;
        private string filename;

        public ExportToExcel(AMSClientV3 amsClient, List<Asset> selassets)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            _amsClient = amsClient;
            _selassets = selassets;

            backgroundWorkerExcel.WorkerReportsProgress = true;
            backgroundWorkerExcel.WorkerSupportsCancellation = true;
        }


        private void ExportToExcel_Load(object sender, EventArgs e)
        {
            DpiUtils.InitPerMonitorDpi(this);

            string extension = radioButtonFormatExcel.Checked ? "xlsx" : "csv";
            textBoxExcelFile.Text = string.Format("{0}\\Export-{1}-{2}." + extension, Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), _amsClient.credentialsEntry.AccountName, DateTime.Now.ToString("yyyy-MM-dd"));
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
            
            if (File.Exists(textBoxExcelFile.Text))
            {
                if (MessageBox.Show($"File '{textBoxExcelFile.Text}' already exists.\nOk to overwrite ?", "File exists", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        File.Delete(textBoxExcelFile.Text);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    return;
                }
            }
            
            
            buttonOk.Enabled = false;
            filename = textBoxExcelFile.Text;

            if (radioButtonAllAssets.Checked)
            {
                progressBarExport.Style = ProgressBarStyle.Marquee;
            }
            else
            {
                progressBarExport.Style = ProgressBarStyle.Blocks;
            }

            progressBarExport.Visible = labelProgress.Visible = true;

            if (radioButtonFormatExcel.Checked)
            {
                backgroundWorkerExcel.RunWorkerAsync();
            }
            else
            {
                backgroundWorkerCSV.RunWorkerAsync();
            }
        }

        private async Task<int?> ExportAssetExcelAsync(Asset asset, Excel.Worksheet xlWorkSheet, int row, bool detailed, bool localtime, List<StreamingEndpoint> seList)
        {
            int? nbLocators = null;

            int index = 1;
            xlWorkSheet.Cells[row, index++] = asset.Name;
            xlWorkSheet.Cells[row, index++] = asset.Description;
            xlWorkSheet.Cells[row, index++] = asset.AlternateId;
            xlWorkSheet.Cells[row, index++] = asset.AssetId.ToString();
            xlWorkSheet.Cells[row, index++] = returnDate(localtime, asset.Created);
            xlWorkSheet.Cells[row, index++] = returnDate(localtime, asset.LastModified);
            xlWorkSheet.Cells[row, index++] = asset.StorageAccountName;
            xlWorkSheet.Cells[row, index++] = asset.Container;

            if (detailed)
            {
                var assetType = await AssetInfo.GetAssetTypeAsync(asset.Name, _amsClient);
                xlWorkSheet.Cells[row, index++] = assetType.Type;
                xlWorkSheet.Cells[row, index++] = assetType.Size;
            }

            IList<AssetStreamingLocator> locators = (await _amsClient.AMSclient.Assets.ListStreamingLocatorsAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, asset.Name)).StreamingLocators;
            nbLocators = locators.Count();
            xlWorkSheet.Cells[row, index++] = nbLocators;

            if (detailed)
            {
                foreach (var locator in locators)
                {
                    var paths = _amsClient.AMSclient.StreamingLocators.ListPaths(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, locator.Name);
                    xlWorkSheet.Cells[row, index++] = locator.Name;
                    xlWorkSheet.Cells[row, index++] = returnDate(localtime, locator.Created);
                    xlWorkSheet.Cells[row, index++] = returnDate(localtime, locator.StartTime);
                    xlWorkSheet.Cells[row, index++] = returnDate(localtime, locator.StartTime);
                    foreach (var se in seList)
                    {
                        var listPaths = new List<string>();
                        foreach (var spath in paths.StreamingPaths)
                        {
                            listPaths.AddRange(spath.Paths.Select(p => "https://" + se.HostName + p));
                        }
                        xlWorkSheet.Cells[row, index++] = string.Join("\n", listPaths);
                    }
                }
            }
            return nbLocators;
        }

        private static DateTime returnDate(bool localtime, DateTime time)
        {
            return localtime ? time.ToLocalTime() : time;
        }

        private async Task<CsvLineResult> ExportAssetCSVLineAsync(Asset asset, bool detailed, bool localtime, List<StreamingEndpoint> seList)
        {
            int? nbLocators = null;

            List<string> linec = new List<string>();
            linec.Add(asset.Name);
            linec.Add(asset.Description);
            linec.Add(asset.AlternateId);
            linec.Add(asset.AssetId.ToString());
            linec.Add(returnDate(localtime, asset.Created).ToString());
            linec.Add(returnDate(localtime, asset.LastModified).ToString());
            linec.Add(asset.StorageAccountName);
            linec.Add(asset.Container);

            if (detailed)
            {
                var assetType = await AssetInfo.GetAssetTypeAsync(asset.Name, _amsClient);
                linec.Add(assetType.Type);
                linec.Add(assetType.Size.ToString());
            }

            IList<AssetStreamingLocator> locators = (await _amsClient.AMSclient.Assets.ListStreamingLocatorsAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, asset.Name)).StreamingLocators;
            nbLocators = locators.Count();
            linec.Add(nbLocators.ToString());

            if (detailed)
            {
                foreach (var locator in locators)
                {
                    var paths = _amsClient.AMSclient.StreamingLocators.ListPaths(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, locator.Name);
                    linec.Add(locator.Name);
                    linec.Add(returnDate(localtime, locator.Created).ToString());
                    linec.Add(returnDate(localtime, locator.StartTime).ToString());
                    linec.Add(returnDate(localtime, locator.EndTime).ToString());

                    foreach (var se in seList)
                    {
                        var listPaths = new List<string>();
                        foreach (var spath in paths.StreamingPaths)
                        {
                            listPaths.AddRange(spath.Paths.Select(p => "https://" + se.HostName + p));
                        }
                        linec.Add(string.Join(", ", listPaths));
                    }
                }
            }

            return new CsvLineResult() { line = convertToCSVLine(linec), locatorCount = nbLocators };
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
                _amsClient.RefreshTokenIfNeeded();

                // Streaming endpoints
                Microsoft.Rest.Azure.IPage<StreamingEndpoint> streamingEndpoints = _amsClient.AMSclient.StreamingEndpoints.List(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName);
                var selist = streamingEndpoints.ToList();

                int numberMaxLocators = 0;
                var csvheader = new StringBuilder();
                var csv = new StringBuilder();
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
                    chartRange.FormulaR1C1 = string.Format(AMSExplorer.Properties.Resources.ExportToExcel_backgroundWorker1_DoWork_AllAssetsInformationMediaAccount0, _amsClient.credentialsEntry.AccountName);
                }
                else
                {
                    chartRange.FormulaR1C1 = string.Format(AMSExplorer.Properties.Resources.ExportToExcel_backgroundWorker1_DoWork_SelectedAssetsInformationMediaAccount0, _amsClient.credentialsEntry.AccountName);
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


                Excel.Range formatRange;
                formatRange = xlWorkSheet.get_Range("a4");
                formatRange.EntireRow.Font.Bold = true;
                formatRange.EntireRow.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightBlue);

                int row = 5;
                int index = 1;

                if (radioButtonAllAssets.Checked)
                {
                    IPage<Asset> currentPage = null;
                    currentPage = _amsClient.AMSclient.Assets.List(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName);

                    while (true)
                    {
                        foreach (Asset asset in currentPage)
                        {
                            var locatorCount = Task.Run(async () => await ExportAssetExcelAsync(asset, xlWorkSheet, row, detailed, checkBoxLocalTime.Checked, selist)).Result;
                            if (locatorCount != null)
                                numberMaxLocators = Math.Max(numberMaxLocators, (int)locatorCount);

                            backgroundWorkerCSV.ReportProgress(index, DateTime.Now); //notify progress to main thread. We also pass time information in UserState to cover this property in the example.  
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
                            index++;
                            row++;
                        }

                        if (currentPage.NextPageLink != null)
                        {
                            currentPage = _amsClient.AMSclient.Assets.ListNext(currentPage.NextPageLink);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                else // Selected assets
                {
                    int total = _selassets.Count();

                    foreach (Asset asset in _selassets)
                    {
                        var locatorCount = Task.Run(async () => await ExportAssetExcelAsync(asset, xlWorkSheet, row, detailed, checkBoxLocalTime.Checked, selist)).Result;
                        if (locatorCount != null)
                            numberMaxLocators = Math.Max(numberMaxLocators, (int)locatorCount);

                        backgroundWorkerCSV.ReportProgress(100 * index / total, DateTime.Now); //notify progress to main thread. We also pass time information in UserState to cover this property in the example.  
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
                        index++;
                        row++;
                    }
                }

                // Header
                row = 4;
                index = 1;
                xlWorkSheet.Cells[row, index++] = "Asset name";
                xlWorkSheet.Cells[row, index++] = "Description";
                xlWorkSheet.Cells[row, index++] = "Alternate Id";
                xlWorkSheet.Cells[row, index++] = "Asset Id";
                xlWorkSheet.Cells[row, index++] = "Created time";
                xlWorkSheet.Cells[row, index++] = "Last modified time";
                xlWorkSheet.Cells[row, index++] = "Storage account";
                xlWorkSheet.Cells[row, index++] = "Storage container";

                if (detailed)
                {
                    xlWorkSheet.Cells[row, index++] = "Asset type";
                    xlWorkSheet.Cells[row, index++] = "Size";
                }

                xlWorkSheet.Cells[row, index++] = "Streaming locators count";

                if (detailed)
                {
                    for (int iloc = 0; iloc < numberMaxLocators; iloc++)
                    {
                        xlWorkSheet.Cells[row, index++] = string.Format("Locator name #{0}", iloc + 1);
                        xlWorkSheet.Cells[row, index++] = "Created time";
                        xlWorkSheet.Cells[row, index++] = "Start time";
                        xlWorkSheet.Cells[row, index++] = "End time";
                        foreach (var se in selist)
                        {
                            xlWorkSheet.Cells[row, index++] = string.Format("Streaming Urls with streaming endpoint #{0}", selist.IndexOf(se));
                        }
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
            _amsClient.RefreshTokenIfNeeded();

            int numberMaxLocators = 0;
            var csvheader = new StringBuilder();
            var csv = new StringBuilder();
            bool detailed = radioButtonDetailledMode.Checked;

            try
            {
                // Streaming endpoints
                Microsoft.Rest.Azure.IPage<StreamingEndpoint> streamingEndpoints = _amsClient.AMSclient.StreamingEndpoints.List(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName);
                var selist = streamingEndpoints.ToList();

                // Asset lines 
                int index = 1;

                if (radioButtonAllAssets.Checked)
                {
                    IPage<Asset> currentPage = null;
                    currentPage = _amsClient.AMSclient.Assets.List(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName);

                    while (true)
                    {
                        foreach (Asset asset in currentPage)
                        {
                            var res = Task.Run(async () => await ExportAssetCSVLineAsync(asset, detailed, checkBoxLocalTime.Checked, selist)).Result;
                            csv.AppendLine(res.line);
                            if (res.locatorCount != null)
                                numberMaxLocators = Math.Max(numberMaxLocators, (int)res.locatorCount);

                            backgroundWorkerCSV.ReportProgress(index, DateTime.Now); //notify progress to main thread. We also pass time information in UserState to cover this property in the example.  
                                                                                     //if cancellation is pending, cancel work.  
                            if (backgroundWorkerCSV.CancellationPending)
                            {
                                e.Cancel = true;
                                return;
                            }
                            index++;
                        }

                        if (currentPage.NextPageLink != null)
                        {
                            currentPage = _amsClient.AMSclient.Assets.ListNext(currentPage.NextPageLink);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                else // Selected assets
                {
                    int total = _selassets.Count();

                    foreach (Asset asset in _selassets)
                    {
                        var res = Task.Run(async () => await ExportAssetCSVLineAsync(asset, detailed, checkBoxLocalTime.Checked, selist)).Result;
                        csv.AppendLine(res.line);
                        if (res.locatorCount != null)
                            numberMaxLocators = Math.Max(numberMaxLocators, (int)res.locatorCount);

                        backgroundWorkerCSV.ReportProgress(100 * index / total, DateTime.Now); //notify progress to main thread. We also pass time information in UserState to cover this property in the example.  
                                                                                               //if cancellation is pending, cancel work.  
                        if (backgroundWorkerCSV.CancellationPending)
                        {
                            e.Cancel = true;
                            return;
                        }
                        index++;
                    }
                }

                // Header
                if (radioButtonAllAssets.Checked)
                {
                    csvheader.AppendLine(checkStringForCSV(string.Format(AMSExplorer.Properties.Resources.ExportToExcel_backgroundWorker1_DoWork_AllAssetsInformationMediaAccount0, _amsClient.credentialsEntry.AccountName)));
                }
                else
                {
                    csvheader.AppendLine(checkStringForCSV(string.Format(AMSExplorer.Properties.Resources.ExportToExcel_backgroundWorker1_DoWork_SelectedAssetsInformationMediaAccount0, _amsClient.credentialsEntry.AccountName)));
                }

                csvheader.AppendLine(checkStringForCSV(string.Format("Exported with Azure Media Services Explorer v{0} on {1}. Dates are {2}.",
                    Assembly.GetExecutingAssembly().GetName().Version.ToString(),
                    checkBoxLocalTime.Checked ? DateTime.Now.ToString() : DateTime.UtcNow.ToString(),
                    checkBoxLocalTime.Checked ? "local" : "UTC based"
                    )));

                List<string> linec = new List<string>();
                linec.Add("Asset name");
                linec.Add("Description");
                linec.Add("Alternate Id");
                linec.Add("Asset Id");
                linec.Add("Created time");
                linec.Add("Last modified time");
                linec.Add("Storage account");
                linec.Add("Storage container");

                if (detailed)
                {
                    linec.Add("Asset type");
                    linec.Add("Size");
                }

                linec.Add("Streaming locators count");

                if (detailed)
                {
                    for (int iloc = 0; iloc < numberMaxLocators; iloc++)
                    {
                        linec.Add(string.Format("Locator name #{0}", iloc + 1));
                        linec.Add("Created time");
                        linec.Add("Start time");
                        linec.Add("End time");
                        foreach (var se in selist)
                        {
                            linec.Add(string.Format("Streaming Urls with streaming endpoint #{0}", selist.IndexOf(se)));
                        }
                    }
                }

                csvheader.AppendLine(convertToCSVLine(linec));
                csvheader.Append(csv);

                try
                {
                    File.WriteAllText(filename, csvheader.ToString());
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
            if (radioButtonSelectedAssets.Checked)
            {
                if (progressBarExport.InvokeRequired)
                {
                    progressBarExport.BeginInvoke(new Action(() =>
                    {
                        progressBarExport.Value = e.ProgressPercentage; //update progress bar 
                    }));
                }
                else
                {
                    progressBarExport.Value = e.ProgressPercentage; //update progress bar  
                }
            }
            else
            {
                string text = $"Progress ({e.ProgressPercentage} assets)";
                if (labelProgress.InvokeRequired)
                {
                    labelProgress.BeginInvoke(new Action(() =>
                    {
                        labelProgress.Text = text;
                    }));
                }
                else
                {
                    progressBarExport.Text = text;
                }
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (progressBarExport.InvokeRequired)
            {
                progressBarExport.BeginInvoke(new Action(() =>
                {
                    progressBarExport.Value = 0; //update progress bar
                    progressBarExport.Visible = false;
                }));
            }
            else
            {
                progressBarExport.Value = 0; //update progress bar 
                progressBarExport.Visible = false;
            }

            if (labelProgress.InvokeRequired)
            {
                labelProgress.BeginInvoke(new Action(() =>
                {
                    labelProgress.Visible = false;
                }));
            }
            else
            {
                labelProgress.Visible = false;
            }

            if (buttonOk.InvokeRequired)
            {
                buttonOk.BeginInvoke(new Action(() =>
                {
                    buttonOk.Enabled = true;
                }));
            }
            else
            {
                buttonOk.Enabled = true;
            }
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

        private string convertToCSVLine(List<string> linec)
        {
            var newlinec = linec.Select(s => checkStringForCSV(s));
            return string.Join(System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator, newlinec);
        }

        private void ExportToExcel_DpiChanged(object sender, DpiChangedEventArgs e)
        {
            DpiUtils.UpdatedSizeFontAfterDPIChange(label5, e);

        }
    }


    public class CsvLineResult
    {
        public int? locatorCount;
        public string line;
    }
}