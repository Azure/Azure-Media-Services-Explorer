//----------------------------------------------------------------------------------------------
//    Copyright 2021 Microsoft Corporation
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

using Microsoft.Azure.Management.Media;
using Microsoft.Azure.Management.Media.Models;
using Microsoft.Rest.Azure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml;
using System.Diagnostics;
using X14 = DocumentFormat.OpenXml.Office2010.Excel;
using System.Globalization;

namespace AMSExplorer
{
    public partial class ExportToExcel : Form
    {
        private readonly AMSClientV3 _amsClient;
        private readonly List<Asset> _selassets;
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

        /*
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
                var assetType = await AssetTools.GetAssetTypeAsync(asset.Name, _amsClient);
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
        */


        private async Task<(int?, Row)> ExportAssetExcelAsync(Asset asset, uint row, bool detailed, bool localtime, List<StreamingEndpoint> seList)
        {
            int? nbLocators = null;

            uint index = 1;



            var listContent = new List<object>() { asset.Name, asset.Description, asset.AlternateId, asset.AssetId.ToString(), returnDate(localtime, asset.Created), returnDate(localtime, asset.LastModified), asset.StorageAccountName, asset.Container };
            if (detailed)
            {
                var assetType = await AssetTools.GetAssetTypeAsync(asset.Name, _amsClient);
                listContent.Add(assetType.Type);
                listContent.Add((decimal)assetType.Size);
            }

            IList<AssetStreamingLocator> locators = (await _amsClient.AMSclient.Assets.ListStreamingLocatorsAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, asset.Name)).StreamingLocators;
            nbLocators = locators.Count();
            listContent.Add((decimal)nbLocators);

            if (detailed)
            {
                foreach (var locator in locators)
                {
                    var paths = _amsClient.AMSclient.StreamingLocators.ListPaths(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, locator.Name);
                    listContent.AddRange(new List<object>() { locator.Name, returnDate(localtime, locator.Created), returnDate(localtime, locator.StartTime), returnDate(localtime, locator.EndTime) });


                    foreach (var se in seList)
                    {
                        var listPaths = new List<string>();
                        foreach (var spath in paths.StreamingPaths)
                        {
                            listPaths.AddRange(spath.Paths.Select(p => "https://" + se.HostName + p));
                        }
                        listContent.Add(string.Join("\n", listPaths));
                    }
                }
            }

            //sheetData.Append(CreateNewRow(listContent));
            return (nbLocators, CreateNewRow(listContent));
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
                var assetType = await AssetTools.GetAssetTypeAsync(asset.Name, _amsClient);
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
                // Streaming endpoints
                Microsoft.Rest.Azure.IPage<StreamingEndpoint> streamingEndpoints = _amsClient.AMSclient.StreamingEndpoints.List(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName);
                var selist = streamingEndpoints.ToList();

                int numberMaxLocators = 0;
                var csvheader = new StringBuilder();
                var csv = new StringBuilder();
                bool detailed = radioButtonDetailledMode.Checked;

                // Create a spreadsheet document by supplying the fileName.  
                SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.
                    Create(filename, SpreadsheetDocumentType.Workbook);

                // Add a WorkbookPart to the document.  
                WorkbookPart workbookpart = spreadsheetDocument.AddWorkbookPart();
                workbookpart.Workbook = new Workbook();

                // stype sheet
                WorkbookStylesPart wbsp = workbookpart.AddNewPart<WorkbookStylesPart>();
                wbsp.Stylesheet = GetStylesheet();
                wbsp.Stylesheet.Save();

                // Add a WorksheetPart to the WorkbookPart.  
                WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                worksheetPart.Worksheet = new Worksheet(new SheetData());

                // Add Sheets to the Workbook.  
                Sheets sheets = spreadsheetDocument.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());

                // Append a new worksheet and associate it with the workbook.  
                Sheet sheet = new Sheet()
                {
                    Id = spreadsheetDocument.WorkbookPart.
                        GetIdOfPart(worksheetPart),
                    SheetId = 1,
                    Name = "mySheet"
                };

                sheets.Append(sheet);
                Worksheet worksheet = new Worksheet();
                SheetData sheetData = new SheetData();


                //// END INIT
                ///


                string title;
                if (radioButtonAllAssets.Checked)
                {
                    title = string.Format(AMSExplorer.Properties.Resources.ExportToExcel_backgroundWorker1_DoWork_AllAssetsInformationMediaAccount0, _amsClient.credentialsEntry.AccountName);
                }
                else
                {
                    title = string.Format(AMSExplorer.Properties.Resources.ExportToExcel_backgroundWorker1_DoWork_SelectedAssetsInformationMediaAccount0, _amsClient.credentialsEntry.AccountName);
                }

                Row rowTitle = CreateNewRow(title);



                uint index = 1;


                string AMSENote = string.Format("Exported with Azure Media Services Explorer v{0} on {1}. Dates are {2}.",
                   Assembly.GetExecutingAssembly().GetName().Version.ToString(),
                   checkBoxLocalTime.Checked ? DateTime.Now.ToString() : DateTime.UtcNow.ToString(),
                   checkBoxLocalTime.Checked ? "local" : "UTC based"
                   );
                Row rowAMSENote = CreateNewRow(AMSENote);



                // ASSET ROWS

                uint row = 4;
                List<Row> Rows = new List<Row>();

                if (radioButtonAllAssets.Checked)
                {
                    IPage<Asset> currentPage = null;
                    currentPage = _amsClient.AMSclient.Assets.List(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName);

                    while (true)
                    {
                        foreach (Asset asset in currentPage)
                        {
                            var output = Task.Run(async () => await ExportAssetExcelAsync(asset, row, detailed, checkBoxLocalTime.Checked, selist)).Result;
                            if (output.Item1 != null)
                                numberMaxLocators = Math.Max(numberMaxLocators, (int)output.Item1);
                            Rows.Add(output.Item2);

                            backgroundWorkerCSV.ReportProgress((int)index, DateTime.Now); //notify progress to main thread. We also pass time information in UserState to cover this property in the example.  
                                                                                          //if cancellation is pending, cancel work.  
                            if (backgroundWorkerExcel.CancellationPending)
                            {
                                ////////

                                worksheet.Append(sheetData);
                                worksheetPart.Worksheet = worksheet;
                                workbookpart.Workbook.Save();

                                // Close the document.  
                                spreadsheetDocument.Close();


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
                        var output = Task.Run(async () => await ExportAssetExcelAsync(asset, row, detailed, checkBoxLocalTime.Checked, selist)).Result;
                        if (output.Item1 != null)
                            numberMaxLocators = Math.Max(numberMaxLocators, (int)output.Item1);

                        Rows.Add(output.Item2);

                        backgroundWorkerCSV.ReportProgress((int)(100d * (double)index / total), DateTime.Now); //notify progress to main thread. We also pass time information in UserState to cover this property in the example.  
                                                                                                               //if cancellation is pending, cancel work.  
                        if (backgroundWorkerExcel.CancellationPending)
                        {
                            // Save the new worksheet.
                            worksheetPart.Worksheet.Save();

                            workbookpart.Workbook.Save();

                            // Close the document.
                            spreadsheetDocument.Close();
                            e.Cancel = true;
                            return;
                        }
                        index++;
                        row++;
                    }
                }


                // Header

                var listHeader = new List<object>() { "Asset name", "Description", "Alternate Id", "Asset Id", "Created time", "Last modified time", "Storage account", "Storage container" };
                if (detailed)
                {
                    listHeader.Add("Asset type");
                    listHeader.Add("Size");
                }
                listHeader.Add("Streaming locators count");
                if (detailed)
                {
                    for (int iloc = 0; iloc < numberMaxLocators; iloc++)
                    {
                        listHeader.AddRange(new List<string>() { string.Format("Locator name #{0}", iloc + 1), "Created time", "Start time", "End time" });
                        foreach (var se in selist)
                        {
                            listHeader.Add(string.Format("Streaming Urls with streaming endpoint #{0}", selist.IndexOf(se)));
                        }
                    }
                }
                Row rowHeader = CreateNewRow(listHeader, 3U);


                sheetData.Append(rowTitle);
                sheetData.Append(rowAMSENote);
                sheetData.Append(rowHeader);
                sheetData.Append(Rows);

                // END HEADER


                ////////

                worksheet.Append(sheetData);

                worksheetPart.Worksheet = worksheet;
                workbookpart.Workbook.Save();

                // Close the document.  
                spreadsheetDocument.Close();


                if (checkBoxOpenFileAfterExport.Checked)
                {
                    var p = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = filename,
                            UseShellExecute = true
                        }
                    };
                    p.Start();
                }

                /*
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
                 */
            }
            catch (Exception ex)
            {
                MessageBox.Show(AMSExplorer.Properties.Resources.ExportToExcel_backgroundWorker1_DoWork_Error + ex.Message);
            }


        }

        private static Row CreateNewRow(string cellValue, UInt32Value styleIndex = null)
        {
            Row row = new Row() { /*RowIndex = 2U,*/ Spans = new ListValue<StringValue>() };
            Cell cell = new Cell()
            {
                StyleIndex = 0,//styleIndex,
                //CellReference = "A2",
                DataType = CellValues.String,
                CellValue = new CellValue(cellValue),

            };

            row.Append(cell);
            return row;
        }

        private static Row CreateNewRow(List<object> cellValues, UInt32Value styleIndex = null)
        {
            Row row = new Row() { Spans = new ListValue<StringValue>() };
            foreach (var value in cellValues)
            {
                Cell cell = null;

                if (value == null || typeof(string) == value.GetType())
                {
                    cell = new Cell()
                    {
                        StyleIndex = 0,//styleIndex,

                        //CellReference = "A2",
                        DataType = CellValues.String,
                        CellValue = new CellValue((string)value),

                    };
                }
                else if (typeof(decimal) == value.GetType())
                {
                    var iValue = (decimal)value;

                    cell = new Cell()
                    {
                        StyleIndex = 0,
                        //CellReference = "A2",
                        DataType = CellValues.Number,
                        CellValue = new CellValue(iValue)

                    };
                }
                else if (typeof(DateTime) == value.GetType())
                {
                    var oaValue = (DateTime)value;

                    cell = new Cell()
                    {
                        StyleIndex = 1,
                        //CellReference = "A2",
                        //DataType = CellValues.String,
                        CellValue = new CellValue(oaValue.ToOADate().ToString(CultureInfo.InvariantCulture))

                    };
                }

                row.Append(cell);
            }
            return row;
        }


        private static Stylesheet CreateStylesheet()
        {
            Stylesheet stylesheet1 = new Stylesheet() { MCAttributes = new MarkupCompatibilityAttributes() { Ignorable = "x14ac" } };
            stylesheet1.AddNamespaceDeclaration("mc", "http://schemas.openxmlformats.org/markup-compatibility/2006");
            stylesheet1.AddNamespaceDeclaration("x14ac", "http://schemas.microsoft.com/office/spreadsheetml/2009/9/ac");

            Fonts fonts1 = new Fonts() { Count = (UInt32Value)1U, KnownFonts = true };

            Font font1 = new Font();
            FontSize fontSize1 = new FontSize() { Val = 11D };
            Color color1 = new Color() { Theme = (UInt32Value)1U };
            FontName fontName1 = new FontName() { Val = "Calibri" };
            FontFamilyNumbering fontFamilyNumbering1 = new FontFamilyNumbering() { Val = 2 };
            FontScheme fontScheme1 = new FontScheme() { Val = FontSchemeValues.Minor };

            font1.Append(fontSize1);
            font1.Append(color1);
            font1.Append(fontName1);
            font1.Append(fontFamilyNumbering1);
            font1.Append(fontScheme1);

            fonts1.Append(font1);

            Fills fills1 = new Fills() { Count = (UInt32Value)5U };

            // FillId = 0
            Fill fill1 = new Fill();
            PatternFill patternFill1 = new PatternFill() { PatternType = PatternValues.None };
            fill1.Append(patternFill1);

            // FillId = 1
            Fill fill2 = new Fill();
            PatternFill patternFill2 = new PatternFill() { PatternType = PatternValues.Gray125 };
            fill2.Append(patternFill2);

            // FillId = 2,RED
            Fill fill3 = new Fill();
            PatternFill patternFill3 = new PatternFill() { PatternType = PatternValues.Solid };
            ForegroundColor foregroundColor1 = new ForegroundColor() { Rgb = "FFFF0000" };
            BackgroundColor backgroundColor1 = new BackgroundColor() { Indexed = (UInt32Value)64U };
            patternFill3.Append(foregroundColor1);
            patternFill3.Append(backgroundColor1);
            fill3.Append(patternFill3);

            // FillId = 3,BLUE
            Fill fill4 = new Fill();
            PatternFill patternFill4 = new PatternFill() { PatternType = PatternValues.Solid };
            ForegroundColor foregroundColor2 = new ForegroundColor() { Rgb = "FF0070C0" };
            BackgroundColor backgroundColor2 = new BackgroundColor() { Indexed = (UInt32Value)64U };
            patternFill4.Append(foregroundColor2);
            patternFill4.Append(backgroundColor2);
            fill4.Append(patternFill4);

            // FillId = 4,YELLO
            Fill fill5 = new Fill();
            PatternFill patternFill5 = new PatternFill() { PatternType = PatternValues.Solid };
            ForegroundColor foregroundColor3 = new ForegroundColor() { Rgb = "FFFFFF00" };
            BackgroundColor backgroundColor3 = new BackgroundColor() { Indexed = (UInt32Value)64U };
            patternFill5.Append(foregroundColor3);
            patternFill5.Append(backgroundColor3);
            fill5.Append(patternFill5);

            fills1.Append(fill1);
            fills1.Append(fill2);
            fills1.Append(fill3);
            fills1.Append(fill4);
            fills1.Append(fill5);

            Borders borders1 = new Borders() { Count = (UInt32Value)1U };

            Border border1 = new Border();
            LeftBorder leftBorder1 = new LeftBorder();
            RightBorder rightBorder1 = new RightBorder();
            TopBorder topBorder1 = new TopBorder();
            BottomBorder bottomBorder1 = new BottomBorder();
            DiagonalBorder diagonalBorder1 = new DiagonalBorder();

            border1.Append(leftBorder1);
            border1.Append(rightBorder1);
            border1.Append(topBorder1);
            border1.Append(bottomBorder1);
            border1.Append(diagonalBorder1);

            borders1.Append(border1);

            CellStyleFormats cellStyleFormats1 = new CellStyleFormats() { Count = (UInt32Value)1U };
            CellFormat cellFormat1 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)0U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U };

            cellStyleFormats1.Append(cellFormat1);

            CellFormats cellFormats1 = new CellFormats() { Count = (UInt32Value)4U };
            CellFormat cellFormat2 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)0U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U };
            CellFormat cellFormat3 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)0U, FillId = (UInt32Value)2U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFill = true };
            CellFormat cellFormat4 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)0U, FillId = (UInt32Value)3U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFill = true };
            CellFormat cellFormat5 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)0U, FillId = (UInt32Value)4U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFill = true };
            // for date time
            CellFormat cellFormat6 = new CellFormat() { NumberFormatId = (UInt32Value)14U, FontId = (UInt32Value)0U, FillId = (UInt32Value)5U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFill = true, ApplyNumberFormat = true };

            cellFormats1.Append(cellFormat2);
            cellFormats1.Append(cellFormat3);
            cellFormats1.Append(cellFormat4);
            cellFormats1.Append(cellFormat5);
            cellFormats1.Append(cellFormat6);

            CellStyles cellStyles1 = new CellStyles() { Count = (UInt32Value)1U };
            CellStyle cellStyle1 = new CellStyle() { Name = "Normal", FormatId = (UInt32Value)0U, BuiltinId = (UInt32Value)0U };

            cellStyles1.Append(cellStyle1);
            DifferentialFormats differentialFormats1 = new DifferentialFormats() { Count = (UInt32Value)0U };
            TableStyles tableStyles1 = new TableStyles() { Count = (UInt32Value)0U, DefaultTableStyle = "TableStyleMedium2", DefaultPivotStyle = "PivotStyleMedium9" };

            StylesheetExtensionList stylesheetExtensionList1 = new StylesheetExtensionList();

            StylesheetExtension stylesheetExtension1 = new StylesheetExtension() { Uri = "{EB79DEF2-80B8-43e5-95BD-54CBDDF9020C}" };
            stylesheetExtension1.AddNamespaceDeclaration("x14", "http://schemas.microsoft.com/office/spreadsheetml/2009/9/main");
            X14.SlicerStyles slicerStyles1 = new X14.SlicerStyles() { DefaultSlicerStyle = "SlicerStyleLight1" };

            stylesheetExtension1.Append(slicerStyles1);

            stylesheetExtensionList1.Append(stylesheetExtension1);

            stylesheet1.Append(fonts1);
            stylesheet1.Append(fills1);
            stylesheet1.Append(borders1);
            stylesheet1.Append(cellStyleFormats1);
            stylesheet1.Append(cellFormats1);
            stylesheet1.Append(cellStyles1);
            stylesheet1.Append(differentialFormats1);
            stylesheet1.Append(tableStyles1);
            stylesheet1.Append(stylesheetExtensionList1);
            return stylesheet1;
        }


        private static Stylesheet GetStylesheet()
        {
            var StyleSheet = new Stylesheet();

            // Create "fonts" node.
            var Fonts = new Fonts();
            Fonts.Append(new Font()
            {
                FontName = new FontName() { Val = "Calibri" },
                FontSize = new FontSize() { Val = 11 },
                FontFamilyNumbering = new FontFamilyNumbering() { Val = 2 },
            });

            Fonts.Count = (uint)Fonts.ChildElements.Count;

            // Create "fills" node.
            var Fills = new Fills();
            Fills.Append(new Fill()
            {
                PatternFill = new PatternFill() { PatternType = PatternValues.None }
            });
            Fills.Append(new Fill()
            {
                PatternFill = new PatternFill() { PatternType = PatternValues.Gray125 }
            });

            Fills.Count = (uint)Fills.ChildElements.Count;

            // Create "borders" node.
            var Borders = new Borders();
            Borders.Append(new Border()
            {
                LeftBorder = new LeftBorder(),
                RightBorder = new RightBorder(),
                TopBorder = new TopBorder(),
                BottomBorder = new BottomBorder(),
                DiagonalBorder = new DiagonalBorder()
            });

            Borders.Count = (uint)Borders.ChildElements.Count;

            // Create "cellStyleXfs" node.
            var CellStyleFormats = new CellStyleFormats();
            CellStyleFormats.Append(new CellFormat()
            {
                NumberFormatId = 0,
                FontId = 0,
                FillId = 0,
                BorderId = 0
            });

            CellStyleFormats.Count = (uint)CellStyleFormats.ChildElements.Count;

            // Create "cellXfs" node.
            var CellFormats = new CellFormats();

            // A default style that works for everything but DateTime
            CellFormats.Append(new CellFormat()
            {
                BorderId = 0,
                FillId = 0,
                FontId = 0,
                NumberFormatId = 0,
                FormatId = 0,
                ApplyNumberFormat = true
            });

            // A style that works for DateTime (just the date)
            CellFormats.Append(new CellFormat()
            {
                BorderId = 0,
                FillId = 0,
                FontId = 0,
                NumberFormatId = 14, // or 22 to include the time
                FormatId = 0,
                ApplyNumberFormat = true
            });

            CellFormats.Count = (uint)CellFormats.ChildElements.Count;

            // Create "cellStyles" node.
            var CellStyles = new CellStyles();
            CellStyles.Append(new CellStyle()
            {
                Name = "Normal",
                FormatId = 0,
                BuiltinId = 0
            });
            CellStyles.Count = (uint)CellStyles.ChildElements.Count;

            // Append all nodes in order.
            StyleSheet.Append(Fonts);
            StyleSheet.Append(Fills);
            StyleSheet.Append(Borders);
            StyleSheet.Append(CellStyleFormats);
            StyleSheet.Append(CellFormats);
            StyleSheet.Append(CellStyles);

            return StyleSheet;
        }

        private static Cell InsertTextInCell(WorksheetPart worksheetPart, SharedStringTablePart shareStringPart, string title, uint columnIndex, uint rowIndex, UInt32? styleIndex = null)
        {
            // Insert the text into the SharedStringTablePart.
            int index = InsertSharedStringItem(title, shareStringPart);

            // Insert cell A1 into the new worksheet.
            Cell cell = InsertCellInWorksheet(Convert.ToChar(64 + columnIndex).ToString(), rowIndex, worksheetPart);

            // Set the value of cell A1.
            cell.CellValue = new CellValue(index.ToString());
            cell.DataType = new EnumValue<CellValues>(CellValues.SharedString);
            cell.StyleIndex = styleIndex;
            return cell;
        }

        // Given text and a SharedStringTablePart, creates a SharedStringItem with the specified text 
        // and inserts it into the SharedStringTablePart. If the item already exists, returns its index.
        private static int InsertSharedStringItem(string text, SharedStringTablePart shareStringPart)
        {
            // If the part does not contain a SharedStringTable, create one.
            if (shareStringPart.SharedStringTable == null)
            {
                shareStringPart.SharedStringTable = new SharedStringTable();
            }

            int i = 0;

            // Iterate through all the items in the SharedStringTable. If the text already exists, return its index.
            foreach (SharedStringItem item in shareStringPart.SharedStringTable.Elements<SharedStringItem>())
            {
                if (item.InnerText == text)
                {
                    return i;
                }

                i++;
            }

            // The text does not exist in the part. Create the SharedStringItem and return its index.
            shareStringPart.SharedStringTable.AppendChild(new SharedStringItem(new DocumentFormat.OpenXml.Spreadsheet.Text(text)));
            shareStringPart.SharedStringTable.Save();

            return i;
        }

        // Given a WorkbookPart, inserts a new worksheet.
        private static WorksheetPart InsertWorksheet(WorkbookPart workbookPart)
        {
            // Add a new worksheet part to the workbook.
            WorksheetPart newWorksheetPart = workbookPart.AddNewPart<WorksheetPart>();
            newWorksheetPart.Worksheet = new Worksheet(new SheetData());
            newWorksheetPart.Worksheet.Save();

            Sheets sheets = workbookPart.Workbook.GetFirstChild<Sheets>();
            string relationshipId = workbookPart.GetIdOfPart(newWorksheetPart);

            // Get a unique ID for the new sheet.
            uint sheetId = 1;
            if (sheets.Elements<Sheet>().Count() > 0)
            {
                sheetId = sheets.Elements<Sheet>().Select(s => s.SheetId.Value).Max() + 1;
            }

            string sheetName = "Sheet" + sheetId;

            // Append the new worksheet and associate it with the workbook.
            Sheet sheet = new Sheet() { Id = relationshipId, SheetId = sheetId, Name = sheetName };
            sheets.Append(sheet);
            workbookPart.Workbook.Save();

            return newWorksheetPart;
        }

        // Given a column name, a row index, and a WorksheetPart, inserts a cell into the worksheet. 
        // If the cell already exists, returns it. 
        private static Cell InsertCellInWorksheet(string columnName, uint rowIndex, WorksheetPart worksheetPart)
        {
            Worksheet worksheet = worksheetPart.Worksheet;
            SheetData sheetData = worksheet.GetFirstChild<SheetData>();
            string cellReference = columnName + rowIndex;

            // If the worksheet does not contain a row with the specified row index, insert one.
            Row row;
            if (sheetData.Elements<Row>().Where(r => r.RowIndex == rowIndex).Count() != 0)
            {
                row = sheetData.Elements<Row>().Where(r => r.RowIndex == rowIndex).First();
            }
            else
            {
                row = new Row() { RowIndex = rowIndex };
                sheetData.Append(row);
            }

            // If there is not a cell with the specified column name, insert one.  
            if (row.Elements<Cell>().Where(c => c.CellReference.Value == columnName + rowIndex).Count() > 0)
            {
                return row.Elements<Cell>().Where(c => c.CellReference.Value == cellReference).First();
            }
            else
            {
                // Cells must be in sequential order according to CellReference. Determine where to insert the new cell.
                Cell refCell = null;
                foreach (Cell cell in row.Elements<Cell>())
                {
                    if (cell.CellReference.Value.Length == cellReference.Length)
                    {
                        if (string.Compare(cell.CellReference.Value, cellReference, true) > 0)
                        {
                            refCell = cell;
                            break;
                        }
                    }
                }

                Cell newCell = new Cell() { CellReference = cellReference };
                row.InsertBefore(newCell, refCell);

                worksheet.Save();
                return newCell;
            }
        }

        private void backgroundWorkerCSV_DoWork(object sender, DoWorkEventArgs e)
        {

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

                if (checkBoxOpenFileAfterExport.Checked)
                {
                    var p = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = filename,
                            UseShellExecute = true
                        }
                    };
                    p.Start();
                }
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

        }

        private void ExportToExcel_Shown(object sender, EventArgs e)
        {
            Telemetry.TrackPageView(this.Name);
        }
    }


    public class CsvLineResult
    {
        public int? locatorCount;
        public string line;
    }
}