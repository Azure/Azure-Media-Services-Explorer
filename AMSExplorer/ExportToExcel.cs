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

using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Azure.Management.Media;
using Microsoft.Azure.Management.Media.Models;
using Microsoft.Rest.Azure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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


        private async Task<(int?, Row)> ExportAssetExcelAsync(Asset asset, uint row, bool detailed, bool localtime, List<StreamingEndpoint> seList)
        {
            int? nbLocators = null;


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

            return (nbLocators, CreateNewRow(listContent));
        }

        private static DateTime returnDate(bool localtime, DateTime time)
        {
            return localtime ? time.ToLocalTime() : time;
        }

        private async Task<CsvLineResult> ExportAssetCSVLineAsync(Asset asset, bool detailed, bool localtime, List<StreamingEndpoint> seList)
        {
            int? nbLocators = null;

            List<string> linec = new List<string>
            {
                asset.Name,
                asset.Description,
                asset.AlternateId,
                asset.AssetId.ToString(),
                returnDate(localtime, asset.Created).ToString(),
                returnDate(localtime, asset.LastModified).ToString(),
                asset.StorageAccountName,
                asset.Container
            };

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


                string title;
                if (radioButtonAllAssets.Checked)
                {
                    title = string.Format(AMSExplorer.Properties.Resources.ExportToExcel_backgroundWorker1_DoWork_AllAssetsInformationMediaAccount0, _amsClient.credentialsEntry.AccountName);
                }
                else
                {
                    title = string.Format(AMSExplorer.Properties.Resources.ExportToExcel_backgroundWorker1_DoWork_SelectedAssetsInformationMediaAccount0, _amsClient.credentialsEntry.AccountName);
                }

                Row rowTitle = CreateNewRow(title, 3U);
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


                // HEADER

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
                Row rowHeader = CreateNewRow(listHeader, 2U);

                // END HEADER


                // Empty row
                var listEmpty = new List<object>();
                Row rowEmpty = CreateNewRow(listEmpty);


                // Let's build the sheet with the rows
                sheetData.Append(rowTitle);
                sheetData.Append(rowAMSENote);
                sheetData.Append(rowEmpty);
                sheetData.Append(rowHeader);
                sheetData.Append(Rows);


                worksheet.Append(sheetData);

                worksheetPart.Worksheet = worksheet;
                workbookpart.Workbook.Save();

                // Close the document.  
                spreadsheetDocument.Close();

                // Let's open the Excel file.
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

        private static Row CreateNewRow(string cellValue, UInt32Value styleIndex = null)
        {
            Row row = new Row() { Spans = new ListValue<StringValue>() };
            Cell cell = new Cell()
            {
                StyleIndex = styleIndex,
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
                        StyleIndex = styleIndex,
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
                        CellValue = new CellValue(oaValue.ToOADate().ToString(CultureInfo.InvariantCulture))
                    };
                }

                row.Append(cell);
            }
            return row;
        }

        /// <summary>
        /// Create a default style sheet. Needed to manage correctly the dates in the sheet
        /// </summary>
        /// <returns></returns>
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
            Fonts.Append(new Font()
            {
                FontName = new FontName() { Val = "Calibri" },
                FontSize = new FontSize() { Val = 20 },
                FontFamilyNumbering = new FontFamilyNumbering() { Val = 2 },
                Color = new Color { Rgb = "FF00008B" }
            });
            Fonts.Append(new Font()  // standard font but in bold
            {
                FontName = new FontName() { Val = "Calibri" },
                FontSize = new FontSize() { Val = 11 },
                FontFamilyNumbering = new FontFamilyNumbering() { Val = 2 },
                Bold = new Bold()
            });

            Fonts.Count = (uint)Fonts.ChildElements.Count;

            // Create "fills" node.
            var Fills = new Fills();
            Fills.Append(new Fill() // reserved
            {
                PatternFill = new PatternFill() { PatternType = PatternValues.None }
            });
            Fills.Append(new Fill() // reserved
            {
                PatternFill = new PatternFill() { PatternType = PatternValues.Gray125 }
            });
            Fills.Append(new Fill() // for the header columb titles
            {
                PatternFill = new PatternFill()
                {
                    PatternType = PatternValues.Solid,
                    ForegroundColor = new ForegroundColor() { Rgb = "FFADD8E6" },
                    BackgroundColor = new BackgroundColor() { Indexed = (UInt32Value)64U }
                }
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

            // A default style that works for everything but DateTime for the columns titles
            CellFormats.Append(new CellFormat()
            {
                BorderId = 0,
                FillId = 2,
                FontId = 2,
                NumberFormatId = 0,
                FormatId = 0,
                ApplyNumberFormat = true,
                ApplyFill = true
            });
            // A default style that works for everything but DateTime for the header
            CellFormats.Append(new CellFormat()
            {
                BorderId = 0,
                FillId = 0,
                FontId = 1,
                NumberFormatId = 0,
                FormatId = 0,
                ApplyNumberFormat = true,
                ApplyFill = true
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

                List<string> linec = new()
                {
                    "Asset name",
                    "Description",
                    "Alternate Id",
                    "Asset Id",
                    "Created time",
                    "Last modified time",
                    "Storage account",
                    "Storage container"
                };

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