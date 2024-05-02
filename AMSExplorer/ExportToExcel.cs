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

using Azure.ResourceManager.Media;
using Azure.ResourceManager.Media.Models;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using MK.IO;
using MK.IO.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AMSExplorer
{
    public partial class ExportToExcel : Form
    {
        private readonly AMSClientV3 _amsClient;
        private readonly List<MediaAssetResource> _selassets;
        private readonly MKIOClient _mkioClient;
        private string filename;
        private CancellationTokenSource source = new CancellationTokenSource();

        public ExportToExcel(AMSClientV3 amsClient, List<MediaAssetResource> selassets, MKIOClient mkioClient)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            _amsClient = amsClient;
            _selassets = selassets;
            _mkioClient = mkioClient;
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

        private async void buttonOk_Click(object sender, EventArgs e)
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

            source.TryReset();
            if (radioButtonFormatExcel.Checked)
            {
                await Excel_DoWorkAsync(source.Token);
            }
            else
            {
                await CSV_DoWorkAsync(source.Token);
            }
            progressBarExport.Visible = labelProgress.Visible = false;
            buttonOk.Enabled = true;
        }


        private async Task<(int?, Row)> ExportAssetExcelAsync(MediaAssetResource asset, uint row, bool detailed, bool localtime, List<StreamingEndpointResource> seList, AssetSchema mkioAsset)
        {
            int? nbLocators = null;


            var listContent = new List<object>() {
                asset.Data.Name,
                asset.Data.Description?? "",
                asset.Data.AlternateId?? "",
                asset.Data.AssetId.ToString(),
                returnDate(localtime, asset.Data.CreatedOn),
                returnDate(localtime, asset.Data.LastModifiedOn),
                asset.Data.StorageAccountName,
                asset.Data.Container
            };
            if (detailed)
            {
                var assetType = await AssetTools.GetAssetTypeAsync(asset, _amsClient);
                listContent.Add(assetType.Type);
                listContent.Add((decimal)assetType.Size);
            }

            IList<MediaAssetStreamingLocator> locators = asset.GetStreamingLocators().ToList();
            nbLocators = locators.Count();
            listContent.Add((decimal)nbLocators);
            if (_mkioClient != null)
            {
                listContent.Add((mkioAsset != null).ToString());
            }

            if (detailed)
            {
                foreach (var locator in locators)
                {
                    var loc = (await _amsClient.AMSclient.GetStreamingLocatorAsync(locator.Name)).Value;
                    var paths = (await loc.GetStreamingPathsAsync()).Value;
                    listContent.AddRange(new List<object>() { locator.Name, returnDate(localtime, locator.CreatedOn), returnDate(localtime, locator.StartOn), returnDate(localtime, locator.EndOn) });

                    foreach (var se in seList)
                    {
                        var listPaths = new List<string>();
                        foreach (var spath in paths.StreamingPaths)
                        {
                            listPaths.AddRange(spath.Paths.Select(p => "https://" + se.Data.HostName + p));
                        }
                        listContent.Add(string.Join("\n", listPaths));
                    }
                }
            }

            return (nbLocators, CreateNewRow(listContent));
        }


        private static DateTime? returnDate(bool localtime, DateTimeOffset? time)
        {
            if (time == null) return null;

            return localtime ? ((DateTimeOffset)time?.ToLocalTime()).DateTime : ((DateTimeOffset)time).DateTime;
        }

        private async Task<CsvLineResult> ExportAssetCSVLineAsync(MediaAssetResource asset, bool detailed, bool localtime, List<StreamingEndpointResource> seList, AssetSchema mkioAsset)
        {
            int? nbLocators = null;

            List<string> linec = new()
            {
                asset.Data.Name,
                asset.Data.Description??"",
                asset.Data.AlternateId??"",
                asset.Data.AssetId.ToString(),
                returnDate(localtime, asset.Data.CreatedOn).ToString(),
                returnDate(localtime, asset.Data.LastModifiedOn).ToString(),
                asset.Data.StorageAccountName,
                asset.Data.Container
            };

            if (detailed)
            {
                var assetType = await AssetTools.GetAssetTypeAsync(asset, _amsClient);
                linec.Add(assetType.Type);
                linec.Add(assetType.Size.ToString());
            }

            IList<MediaAssetStreamingLocator> locators = asset.GetStreamingLocators().ToList();
            nbLocators = locators.Count();
            linec.Add(nbLocators.ToString());

            if (_mkioClient != null)
            {
                linec.Add((mkioAsset != null).ToString());
            }

            if (detailed)
            {
                foreach (var locator in locators)
                {
                    var loc = (await _amsClient.AMSclient.GetStreamingLocatorAsync(locator.Name)).Value;
                    var paths = (await loc.GetStreamingPathsAsync()).Value;
                    linec.Add(locator.Name);
                    linec.Add(returnDate(localtime, locator.CreatedOn).ToString());
                    linec.Add(returnDate(localtime, locator.StartOn).ToString());
                    linec.Add(returnDate(localtime, locator.EndOn).ToString());

                    foreach (var se in seList)
                    {
                        var listPaths = new List<string>();
                        foreach (var spath in paths.StreamingPaths)
                        {
                            listPaths.AddRange(spath.Paths.Select(p => "https://" + se.Data.HostName + p));
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

        private async Task Excel_DoWorkAsync(CancellationToken cancellationToken)
        {
            try
            {
                // Streaming endpoints
                var streamingEndpoints = _amsClient.AMSclient.GetStreamingEndpoints().GetAllAsync();
                var selist = streamingEndpoints.ToListAsync().Result;

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
                Sheet sheet = new()
                {
                    Id = spreadsheetDocument.WorkbookPart.
                        GetIdOfPart(worksheetPart),
                    SheetId = 1,
                    Name = "mySheet"
                };

                sheets.Append(sheet);
                Worksheet worksheet = new();
                SheetData sheetData = new();

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


                // MK.IO
                var allMKIOAssets = new List<AssetSchema>();

                if (_mkioClient != null)
                {
                    // list assets with pages
                    var mkioAssetsResult = await _mkioClient.Assets.ListAsPageAsync(null, 20);
                    while (true)
                    {
                        // do stuff here using mkioAssetsResult.Results
                        allMKIOAssets.AddRange(mkioAssetsResult.Results);
                        if (mkioAssetsResult.NextPageLink == null) break;
                        mkioAssetsResult = await _mkioClient.Assets.ListAsPageNextAsync(mkioAssetsResult.NextPageLink);
                    }
                }

                // ASSET ROWS

                uint row = 4;
                List<Row> Rows = new();

                if (radioButtonAllAssets.Checked)
                {
                    var allAssets = _amsClient.AMSclient.GetMediaAssets().GetAllAsync();

                    await foreach (MediaAssetResource asset in allAssets)
                    {
                        AssetSchema assetMKIO = null;
                        if (_mkioClient != null)
                        {
                            assetMKIO = allMKIOAssets.Where(a => a.Properties.StorageAccountName == asset.Data.StorageAccountName && a.Properties.Container == asset.Data.Container).FirstOrDefault();
                        }

                        var output = await ExportAssetExcelAsync(asset, row, detailed, checkBoxLocalTime.Checked, selist, assetMKIO);
                        if (output.Item1 != null)
                            numberMaxLocators = Math.Max(numberMaxLocators, (int)output.Item1);
                        Rows.Add(output.Item2);

                        //if cancellation is pending, cancel work.  
                        if (cancellationToken.IsCancellationRequested)
                        {
                            ////////
                            worksheet.Append(sheetData);
                            worksheetPart.Worksheet = worksheet;
                            workbookpart.Workbook.Save();

                            // Close the document.  
                            spreadsheetDocument.Dispose();

                            return;
                        }
                        index++;
                        row++;
                    }
                }
                else // Selected assets
                {
                    int total = _selassets.Count();

                    foreach (var asset in _selassets)
                    {
                        AssetSchema assetMKIO = null;
                        if (_mkioClient != null)
                        {
                            assetMKIO = allMKIOAssets.Where(a => a.Properties.StorageAccountName == asset.Data.StorageAccountName && a.Properties.Container == asset.Data.Container).FirstOrDefault();
                        }

                        var output = await ExportAssetExcelAsync(asset, row, detailed, checkBoxLocalTime.Checked, selist, assetMKIO);
                        if (output.Item1 != null)
                            numberMaxLocators = Math.Max(numberMaxLocators, (int)output.Item1);

                        Rows.Add(output.Item2);

                        //notify progress to main thread.
                        ProgressChanged((int)(100d * index / total));


                        //if cancellation is pending, cancel work.  
                        if (cancellationToken.IsCancellationRequested)
                        {
                            // Save the new worksheet.
                            worksheetPart.Worksheet.Save();

                            workbookpart.Workbook.Save();

                            // Close the document.
                            spreadsheetDocument.Dispose();

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
                if (_mkioClient != null)
                {
                    listHeader.Add("In MK.IO");
                }

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
                spreadsheetDocument.Dispose();

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
            Row row = new() { Spans = new ListValue<StringValue>() };
            Cell cell = new()
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
            Row row = new() { Spans = new ListValue<StringValue>() };
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
                        CellValue = new CellValue(oaValue.ToOADate().ToString())
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


        private async Task CSV_DoWorkAsync(CancellationToken cancellationToken)
        {

            int numberMaxLocators = 0;
            var csvheader = new StringBuilder();
            var csv = new StringBuilder();
            bool detailed = radioButtonDetailledMode.Checked;

            try
            {
                // Streaming endpoints
                var streamingEndpoints = _amsClient.AMSclient.GetStreamingEndpoints().GetAllAsync().ToListAsync();
                var selist = streamingEndpoints.Result;

                // MK.IO
                var allMKIOAssets = new List<AssetSchema>();

                if (_mkioClient != null)
                {
                    // list assets with pages
                    var mkioAssetsResult = _mkioClient.Assets.ListAsPage(null, 20);
                    while (true)
                    {
                        // do stuff here using mkioAssetsResult.Results
                        allMKIOAssets.AddRange(mkioAssetsResult.Results);
                        if (mkioAssetsResult.NextPageLink == null) break;
                        mkioAssetsResult = _mkioClient.Assets.ListAsPageNext(mkioAssetsResult.NextPageLink);
                    }
                }

                // Asset lines 
                int index = 1;

                if (radioButtonAllAssets.Checked)
                {
                    var assets = _amsClient.AMSclient.GetMediaAssets().GetAllAsync();

                    await foreach (var asset in assets)
                    {
                        AssetSchema assetMKIO = null;
                        if (_mkioClient != null)
                        {
                            assetMKIO = allMKIOAssets.Where(a => a.Properties.StorageAccountName == asset.Data.StorageAccountName && a.Properties.Container == asset.Data.Container).FirstOrDefault();
                        }

                        var res = await ExportAssetCSVLineAsync(asset, detailed, checkBoxLocalTime.Checked, selist, assetMKIO);
                        csv.AppendLine(res.line);
                        if (res.locatorCount != null)
                            numberMaxLocators = Math.Max(numberMaxLocators, (int)res.locatorCount);

                        //if cancellation is pending, cancel work.  
                        if (cancellationToken.IsCancellationRequested)
                        {
                            return;
                        }
                        index++;
                    }
                }
                else // Selected assets
                {
                    int total = _selassets.Count();

                    foreach (var asset in _selassets)
                    {
                        AssetSchema assetMKIO = null;
                        if (_mkioClient != null)
                        {
                            assetMKIO = allMKIOAssets.Where(a => a.Properties.StorageAccountName == asset.Data.StorageAccountName && a.Properties.Container == asset.Data.Container).FirstOrDefault();
                        }

                        var res = await ExportAssetCSVLineAsync(asset, detailed, checkBoxLocalTime.Checked, selist, assetMKIO);
                        csv.AppendLine(res.line);
                        if (res.locatorCount != null)
                            numberMaxLocators = Math.Max(numberMaxLocators, (int)res.locatorCount);


                        ProgressChanged((int)(100d * index / total));

                        //if cancellation is pending, cancel work.  
                        if (cancellationToken.IsCancellationRequested)
                        {
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

                if (_mkioClient != null)
                {
                    linec.Add("In MK.IO");
                }

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


        private void ProgressChanged(int percentage)
        {
            if (radioButtonSelectedAssets.Checked)
            {
                if (progressBarExport.InvokeRequired)
                {
                    progressBarExport.BeginInvoke(new Action(() =>
                    {
                        progressBarExport.Value = percentage; //update progress bar 
                    }));
                }
                else
                {
                    progressBarExport.Value = percentage; //update progress bar  
                }
            }
            else
            {
                string text = $"Progress ({percentage} assets)";
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

            source.Cancel();
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