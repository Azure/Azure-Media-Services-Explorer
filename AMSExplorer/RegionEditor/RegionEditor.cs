//----------------------------------------------------------------------------------------------
//    Copyright 2016 Microsoft Corporation
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
using System.Diagnostics;
using Microsoft.WindowsAzure.MediaServices.Client;
using Newtonsoft.Json.Linq;
using System.Xml.Linq;
using System.IO;
using System.Threading;
using System.Net;

namespace AMSExplorer
{
    public partial class RegionEditor : Form
    {
        private bool _canDraw;
        private int _startX, _startY;
        private bool polygonalMode = false;

        private const string infoMouse = "{0}, {1}";
        private const string infoMouseDrawRectangle = "{0} x {1}";
        private const string infothumbnail = "thumbnail {0}/{1}";

        private List<Image> listPictures;
        private int pictureIndex = 0;
        private IAsset _asset;
        private int _nbOfRegionsMax;
        private bool _croppingMode;
        private bool _updateNumericUpDownByMouse = true;

        public List<Image> SetPictures
        {
            set
            {
                listPictures = value;
                SetCurrentPicture(listPictures.FirstOrDefault());
            }
        }

        private void SetCurrentPicture(Image picture)
        {
            myPictureBox1.VideoImage = picture;
            myPictureBox1.VideoImageOriginalWidth = picture.Width;
            myPictureBox1.VideoImageOriginalHeight = picture.Height;

            if (_croppingMode)
            {
                numericUpDownX.Maximum = picture.Width;
                numericUpDownY.Maximum = picture.Height;
                numericUpDownW.Maximum = picture.Width;
                numericUpDownH.Maximum = picture.Height;
            }
            toolStripStatusLabelImSize.Text = string.Format("Image size {0} x {1}", picture.Width, picture.Height);
            Refresh();
            UpdateLabelIndex();
        }

        public void DisplayNextPicture()
        {
            if (pictureIndex < listPictures.Count - 1)
            {
                pictureIndex++;
                SetCurrentPicture(listPictures[pictureIndex]);
            }
        }

        public void DisplayPreviousPicture()
        {
            if (pictureIndex > 0)
            {
                pictureIndex--;
                SetCurrentPicture(listPictures[pictureIndex]);
            }
        }

        public RegionEditor(IAsset asset, bool polygonsEnabled, int nbOfRegionsMax, bool croppingMode, string title = null, string text = null, string infoText = null)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            if (title != null) this.Text = title;

            labelWarningJSON.Text = string.Empty;

            if (infoText != null)
            {
                labelInfoText.Text = infoText;
                labelInfoText.Visible = true;
            }

            if (!polygonsEnabled)
            {
                groupBoxShape.Visible = false;
            }

            _asset = asset;
            _nbOfRegionsMax = nbOfRegionsMax;

            buttonClearAllRegions.Visible = buttonClearLastRegion.Visible = !croppingMode;
            _croppingMode = croppingMode;
            myPictureBox1.CroppingMode =  croppingMode;

        }

        private void UpdateLabelIndex()
        {
            labelIndexThumbnail.Text = string.Format(infothumbnail, pictureIndex + 1, listPictures.Count);
        }

        private void RegionEditor_Load(object sender, EventArgs e)
        {
        }

        
        static List<Image> ReturnOriginResolutionThumbnailsForAsset(IAsset asset) // null if not existing
        {
            List<Image> list = new List<Image>();

            string filename = asset.Id.Substring(Constants.AssetIdPrefix.Length) + "_OriginalRes_000001.jpg";
            string path = Path.GetTempPath();
            string pathandfile = Path.Combine(path, filename);
            //c7508b75-a451-4887-9435-4a5b39f88c5f_OriginalRes_000001.jpg
            var files = asset.GetMediaContext().Files.Where(f => f.Name == filename).OrderBy(f => f.LastModified);
            var file = files.FirstOrDefault();

            if (file != null)
            {
                ILocator saslocator = null;

                try
                {
                    // The duration for the locator's access policy.
                    var policy = asset.GetMediaContext().AccessPolicies.Create("AP AMSE", new TimeSpan(0, 15, 0), AccessPermissions.Read);
                    saslocator = asset.GetMediaContext().Locators.CreateLocator(LocatorType.Sas, file.Asset, policy, DateTime.UtcNow.AddMinutes(-5));
                    var assett = file.Asset;
                    IEnumerable<IAssetFile> Thumbnails = file.Asset.AssetFiles.ToList().Where(f => f.Name.StartsWith(asset.Id.Substring(Constants.AssetIdPrefix.Length) + "_OriginalRes_") && f.Name.EndsWith(".jpg")).OrderBy(f => f.Name);

                    // Generate the Progressive Download URLs for each file. 
                    List<Uri> ProgressiveDownloadUris = Thumbnails.Select(af => af.GetSasUri()).ToList();

                    foreach (var urli in ProgressiveDownloadUris)
                    {
                        var request = WebRequest.Create(urli.AbsoluteUri);

                        using (var response = request.GetResponse())
                        using (var stream = response.GetResponseStream())
                        {
                            list.Add(Bitmap.FromStream(stream));
                        }
                    }
                }
                catch (Exception e)
                { }


                try
                {
                    if (saslocator != null) saslocator.Delete();
                }

                catch (Exception e)
                { }
            }

            return list;
        }

        private void buttonFormat_Click(object sender, EventArgs e)
        {
            myPictureBox1.DeleteLastRegion();
        }


        public DialogResult Display()
        {
            SetPictures = ReturnOriginResolutionThumbnailsForAsset(_asset);

            myPictureBox1.LoadSavedRegions();

            DialogResult DR = this.ShowDialog();

            if (DR == DialogResult.OK)
            {
                myPictureBox1.SaveRegions();
            }

            return DR;
        }



        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (myPictureBox1.RegionsCount == _nbOfRegionsMax)
            {
                if (_nbOfRegionsMax == 1) // cropping mode
                {
                    myPictureBox1.DeleteLastRegion();
                }
                else
                {
                    MessageBox.Show(string.Format("You reached the maximum number of regions allowed ({0}).", _nbOfRegionsMax), "Limit", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }

            _canDraw = true;
            //Initialize and keep track of the start position
            int currentX = myPictureBox1.ReturnXInsideVideo(e.X);
            int currentY = myPictureBox1.ReturnYInsideVideo(e.Y);

            _startX = currentX;
            _startY = currentY;
            toolStripStatusLabelXYRect.Visible = true;

            if (polygonalMode)
            {
                if (myPictureBox1.IsPointClosedToFirst(currentX, currentY) || myPictureBox1.ReachedMaxSegments())
                {
                    EndOfPolygonalDrawing();
                    Refresh();
                }
                else
                {
                    myPictureBox1.AddScreenPoint(currentX, currentY);
                }
            }

        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            //If we are not allowed to draw, simply return and disregard the rest of the code

            if (!_canDraw)
            {
                if (myPictureBox1.VideoImageDisplayedWidth > 0 && myPictureBox1.VideoImageDisplayedHeight > 0)
                {
                    toolStripStatusLabelMouseInfo.Text = string.Format(infoMouse, myPictureBox1.GetOriginalXValue(e.X), myPictureBox1.GetOriginalYValue(e.Y));
                }
                return;
            }

            int currentX = myPictureBox1.ReturnXInsideVideo(e.X);
            int currentY = myPictureBox1.ReturnYInsideVideo(e.Y);

            //The x-value of our rectangle should be the minimum between the start x-value and the current x-position
            int x = Math.Min(_startX, currentX);
            //The y-value of our rectangle should also be the minimum between the start y-value and current y-value
            int y = Math.Min(_startY, currentY);

            //The width of our rectangle should be the maximum between the start x-position and current x-position minus
            //the minimum of start x-position and current x-position
            int width = Math.Max(_startX, currentX) - Math.Min(_startX, currentX);

            //For the hight value, it's basically the same thing as above, but now with the y-values:
            int height = Math.Max(_startY, currentY) - Math.Min(_startY, currentY);

            if (!polygonalMode)
            {
                var rect = myPictureBox1.SetScreenDrawingRectangle(x, y, width, height);
                if (_croppingMode)
                {
                    _updateNumericUpDownByMouse = true;
                    numericUpDownX.Value = rect.X;
                    numericUpDownY.Value = rect.Y;
                    numericUpDownW.Value = rect.Width;
                    numericUpDownH.Value = rect.Height;
                    _updateNumericUpDownByMouse = false;
                }
            }
            else
            {
                myPictureBox1.UpdateScreenDrawingPolygone(currentX, currentY);
            }

            toolStripStatusLabelMouseInfo.Text = string.Format(infoMouse, myPictureBox1.GetOriginalXValue(currentX), myPictureBox1.GetOriginalYValue(currentY));
            toolStripStatusLabelXYRect.Text = string.Format(infoMouseDrawRectangle, myPictureBox1.GetOriginalWidthValue(width), myPictureBox1.GetOriginalWidthValue(height));

            Refresh();

        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (!polygonalMode)
            {
                //The system is no longer allowed to draw rectangles
                _canDraw = false;

                myPictureBox1.DrawingRectangleIsFinal();
                toolStripStatusLabelXYRect.Visible = false;
                Refresh();

                if (_croppingMode)
                {
                    var rect = myPictureBox1.LastRegionResolutionMode;
                    numericUpDownX.Value = rect.X;
                    numericUpDownY.Value = rect.Y;
                    numericUpDownW.Value = rect.Width;
                    numericUpDownH.Value = rect.Height;
                }
            }
        }

        private void EndOfPolygonalDrawing()
        {
            if (polygonalMode)
            {
                //The system is no longer allowed to draw rectangles
                _canDraw = false;

                myPictureBox1.DrawingPolygoneIsFinal();
                toolStripStatusLabelXYRect.Visible = false;
            }
        }

        private void myPictureBox1_SizeChanged(object sender, EventArgs e)
        {
            Refresh();
        }

        internal List<PolygoneDecimalMode> GetSavedPolygonesDecimalMode()
        {
            return myPictureBox1.GetSavedRegionsDecimalMode;
        }

        internal List<Polygon> GetSavedPolygonesResolutionMode()
        {
            return myPictureBox1.GetSavedRegionsResolutionMode;
        }

        private void buttonClearAllRegions_Click(object sender, EventArgs e)
        {
            myPictureBox1.DeleteAllRegions();
        }
        private void radioButtonPolygonal_CheckedChanged(object sender, EventArgs e)
        {
            polygonalMode = radioButtonPolygon.Checked;
            myPictureBox1.ResetCurrentDrawings();
            _canDraw = false;
            Refresh();
        }

        private void buttonPreviousImage_Click(object sender, EventArgs e)
        {
            DisplayPreviousPicture();
        }

        private void buttonNextImage_Click(object sender, EventArgs e)
        {
            DisplayNextPicture();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDownREct_ValueChanged(object sender, EventArgs e)
        {
            if (!_updateNumericUpDownByMouse)
            {
                myPictureBox1.LastRectangle = new Rectangle((int)numericUpDownX.Value, (int)numericUpDownY.Value, (int)numericUpDownW.Value, (int)numericUpDownH.Value);
                Refresh();
            }
            numericUpDownW.Maximum = myPictureBox1.VideoImageOriginalWidth - numericUpDownX.Value;
            numericUpDownH.Maximum = myPictureBox1.VideoImageOriginalHeight - numericUpDownY.Value;

        }

        private void buttonOk_Click(object sender, EventArgs e)
        {

        }
    }

    class RectangleDecimalMode
    {
        public decimal X;
        public decimal Y;
        public decimal Width;
        public decimal Height;

        public RectangleDecimalMode()
        {
            X = Y = Width = Height = 0;
        }

        public RectangleDecimalMode(Rectangle rect, int imageWidth, int imageheight)
        {
            X = ((decimal)rect.X / imageWidth);
            Y = ((decimal)rect.Y / imageheight);
            Width = ((decimal)rect.Width / imageWidth);
            Height = ((decimal)rect.Height / imageheight);
        }

        public RectangleDecimalMode(int x, int y, int width, int height, int imageWidth, int imageheight)
        {
            X = ((decimal)x / imageWidth);
            Y = ((decimal)y / imageheight);
            Width = ((decimal)width / imageWidth);
            Height = ((decimal)height / imageheight);
        }

        public Rectangle ToRectangle(int imageWidth, int imageheight, int addmarginleft = 0, int addmargintop = 0)
        {
            return new Rectangle()
            {
                X = (int)(X * imageWidth) + addmarginleft,
                Y = (int)(Y * imageheight) + addmargintop,
                Width = (int)(Width * imageWidth),
                Height = (int)(Height * imageheight)
            };
        }

        public PolygoneDecimalMode ToPolygone()
        {
            return new PolygoneDecimalMode(this);
        }
    }



    class Polygon
    {
        public List<Point> _points;

        public Polygon()
        {
            _points = new List<Point>();
        }

        public void Add(Point point)
        {
            _points.Add(point);
        }

        public Rectangle ToRectangle()
        {
            if (_points.Count == 4)
            {
                var xmin = Math.Min(Math.Min(_points[0].X, _points[1].X), Math.Min(_points[2].X, _points[3].X));
                var xmax = Math.Max(Math.Max(_points[0].X, _points[1].X), Math.Max(_points[2].X, _points[3].X));

                var ymin = Math.Min(Math.Min(_points[0].Y, _points[1].Y), Math.Min(_points[2].Y, _points[3].Y));
                var ymax = Math.Max(Math.Max(_points[0].Y, _points[1].Y), Math.Max(_points[2].Y, _points[3].Y));

                return new Rectangle(xmin, ymin, xmax - xmin, ymax - ymin);
            }
            else
            {
                return new Rectangle();
            }
        }

        public void RemoveLastPoint()
        {
            if (_points.Count > 0) _points.RemoveAt(_points.Count - 1);
        }

        public int PointsCount
        {
            get
            {
                return _points.Count;
            }
        }

        public Point[] ToPoints()
        {
            return _points.ToArray();
        }

        public void SetCurrentPoint(Point p)
        {
            _points[_points.Count - 1] = new Point(p.X, p.Y);
        }

        public PolygoneDecimalMode ToPolygoneDecimalMode(int imageWidth, int imageheight)
        {
            var poly = new PolygoneDecimalMode();
            foreach (var p in _points)
            {
                poly.AddPoint(new Point(p.X, p.Y), imageWidth, imageheight);
            }

            return poly;
        }
    }

    class PolygoneDecimalMode
    {
        private List<PointF> _points;


        public PolygoneDecimalMode()
        {
            _points = new List<PointF>();
        }

        public PolygoneDecimalMode(Point[] points, int imageWidth, int imageheight)
        {
            _points = new List<PointF>();
            foreach (var p in points)
            {
                float x = ((float)p.X / imageWidth);
                float y = ((float)p.Y / imageheight);
                _points.Add(new PointF(x, y));
            }
        }

        public PolygoneDecimalMode(RectangleDecimalMode rect)
        {
            PointF p1 = new PointF((float)rect.X, (float)rect.Y);
            PointF p2 = new PointF((float)(rect.X + rect.Width), (float)rect.Y);
            PointF p3 = new PointF((float)(rect.X + rect.Width), (float)(rect.Y + rect.Height));
            PointF p4 = new PointF((float)rect.X, (float)(rect.Y + rect.Height));
            _points = new List<PointF>() { p1, p2, p3, p4 };
        }

        public int PointsCount
        {
            get
            {
                return _points.Count;
            }
        }

        public Polygon ToPolygone(int imageWidth, int imageheight, int addmarginleft = 0, int addmargintop = 0)
        {
            var poly = new Polygon();
            foreach (var p in _points)
            {
                int x = (int)(p.X * imageWidth) + addmarginleft;
                int y = (int)(p.Y * imageheight) + addmargintop;

                poly.Add(new Point(x, y));
            }

            return poly;
        }

        public Point[] ToPoints(int imageWidth, int imageheight, int addmarginleft = 0, int addmargintop = 0)
        {
            var poly = new List<Point>();
            foreach (var p in _points)
            {
                int x = (int)(p.X * imageWidth) + addmarginleft;
                int y = (int)(p.Y * imageheight) + addmargintop;

                poly.Add(new Point(x, y));
            }

            return poly.ToArray();
        }

        public PointF[] ToDecimalPoints()
        {
            return _points.ToArray();
        }

        public void AddPoint(Point p, int imageWidth, int imageheight)
        {
            float x = ((float)p.X / imageWidth);
            float y = ((float)p.Y / imageheight);
            _points.Add(new PointF(x, y));
        }

        public void RemoveLastPoint()
        {
            if (_points.Count > 0) _points.RemoveAt(_points.Count - 1);
        }

        public void SetCurrentPoint(Point p, int imageWidth, int imageheight)
        {
            float x = ((float)p.X / imageWidth);
            float y = ((float)p.Y / imageheight);
            _points[_points.Count - 1] = new PointF(x, y);
        }
    }

    class myPictureBox : PictureBox
    {
        private List<Polygon> _savedPolygons = new List<Polygon>();
        private List<Polygon> _polygons = new List<Polygon>(); // working list
        private Rectangle _rect;
        private Polygon _poly;

        public Image VideoImage = null;
        public int marginLeft = 0;
        public int marginTop = 0;
        public int VideoImageDisplayedWidth = 0;
        public int VideoImageDisplayedHeight = 0;
        public int VideoImageOriginalWidth = 0;
        public int VideoImageOriginalHeight = 0;

        const int neighbour = 5; // 5 pixels or less
        private int _maxNumberSegments = 10; // 10 segments max per polygon


        public Rectangle LastRectangle
        {
            get
            {
                return (_polygons.Count > 0) ? _polygons.LastOrDefault().ToRectangle() : new Rectangle();
            }
            set
            {
                if (_polygons.Count > 0) _polygons[_polygons.Count - 1] = ToPolygon(value);
            }
        }

        public List<PolygoneDecimalMode> GetSavedRegionsDecimalMode
        {
            get
            {
                return _savedPolygons.Select(p => p.ToPolygoneDecimalMode(VideoImageOriginalWidth, VideoImageOriginalHeight)).ToList();
            }
        }

        public List<Polygon> GetSavedRegionsResolutionMode
        {
            get
            {
                return _savedPolygons;
            }
        }

        public Rectangle LastRegionResolutionMode
        {
            get
            {
                return _polygons.LastOrDefault().ToRectangle();
            }
        }

        public int RegionsCount
        {
            get
            {
                return _polygons.Count;
            }
        }

        public bool CroppingMode { get; internal set; }

        public Rectangle SetScreenDrawingRectangle(int x, int y, int width, int height)
        {
            _rect = new Rectangle(GetOriginalXValue(x), GetOriginalYValue(y), GetOriginalWidthValue(width), GetOriginalHeightValue(height));
            return _rect;
        }

        public void UpdateScreenDrawingPolygone(int x, int y)
        {
            _poly.SetCurrentPoint(new Point(GetOriginalXValue(x), GetOriginalYValue(y)));
        }

        public void DeleteAllRegions()
        {
            _polygons = new List<Polygon>();
            Refresh();
        }

        public void DeleteLastRegion()
        {
            if (_polygons.Count > 0)
            {
                _polygons.RemoveAt(_polygons.Count - 1);
                Refresh();
            }
        }

        public myPictureBox()
        {
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (VideoImage == null) return;

            decimal ratioImage = (decimal)VideoImage.Width / (decimal)VideoImage.Height;
            decimal ratioPictureBox = (decimal)this.Width / (decimal)this.Height;


            if (ratioImage < ratioPictureBox) // bar on the side
            {
                VideoImageDisplayedHeight = this.Height;
                VideoImageDisplayedWidth = (int)(ratioImage * this.Height);
                marginLeft = (this.Width - VideoImageDisplayedWidth) / 2;
                marginTop = 0;
            }
            else // bar on top
            {
                VideoImageDisplayedWidth = this.Width;
                VideoImageDisplayedHeight = (int)(this.Width / ratioImage);
                marginTop = (this.Height - VideoImageDisplayedHeight) / 2;
                marginLeft = 0;
            }

            //Create a new 'pen' to draw our rectangle with, give it the color red and a width of 2
            var penRed = new Pen(Color.Red, 2);
            var penGreen = new Pen(Color.Green, 2);

            //Draw the rectangle on our form with the pen
            e.Graphics.Clear(SystemColors.Window);
            e.Graphics.DrawImage(VideoImage, marginLeft, marginTop, VideoImageDisplayedWidth, VideoImageDisplayedHeight);
            e.Graphics.DrawRectangle(new Pen(Color.Yellow, 1), marginLeft, marginTop, VideoImageDisplayedWidth - 1, VideoImageDisplayedHeight - 1);

            int index = 0;
            if (_rect != null)
            {
                e.Graphics.DrawRectangle(penRed, GetScaledValue(_rect));
            }

            foreach (var polyg in _polygons)
            {
                var poly = GetScaledValue(polyg);
                e.Graphics.DrawPolygon(penGreen, poly);
                if (!CroppingMode) e.Graphics.DrawString(index.ToString(), new Font("Segoe UI", 9), new SolidBrush(Color.Green), poly[0].X, poly[0].Y);
                index++;
            }
            if (_poly != null)
            {
                e.Graphics.DrawLines(penRed, GetScaledValue(_poly));
            }
        }

        public void DrawingRectangleIsFinal()
        {
            if (_rect != null) _polygons.Add(ToPolygon(_rect));
            _rect = new Rectangle();
        }

        public void DrawingPolygoneIsFinal()
        {
            if (_poly != null && _poly.PointsCount > 3)
            {
                _poly.RemoveLastPoint();
                _polygons.Add(_poly);
            }

            _poly = null;
        }

        public Polygon ToPolygon(Rectangle rect)
        {
            var poly = new Polygon();

            poly.Add(new Point(rect.X, rect.Y));
            poly.Add(new Point(rect.X + rect.Width, rect.Y));
            poly.Add(new Point(rect.X + rect.Width, rect.Y + rect.Height));
            poly.Add(new Point(rect.X, rect.Y + rect.Height));

            return poly;
        }

        public Rectangle GetScaledValue(Rectangle r)
        {
            return new Rectangle(GetScaledXValue(r.X), GetScaledYValue(r.Y), GetScaledWidthValue(r.Width), GetScaledHeightValue(r.Height));
        }

        public Point[] GetScaledValue(Polygon poly)
        {
            return poly.ToPoints().Select(p => new Point(GetScaledXValue(p.X), GetScaledYValue(p.Y))).ToArray();
        }

        public int GetScaledXValue(int x)
        {
            return (int)(((decimal)x / VideoImageOriginalWidth) * VideoImageDisplayedWidth + marginLeft);
        }

        public int GetOriginalXValue(int screenX)
        {
            return (int)((decimal)VideoImageOriginalWidth * (screenX - marginLeft) / VideoImageDisplayedWidth);
        }
        public int GetScaledYValue(int y)
        {
            return (int)(((decimal)y / VideoImageOriginalHeight) * VideoImageDisplayedHeight + marginTop);
        }

        public int GetOriginalYValue(int screenY)
        {
            return (int)((decimal)VideoImageOriginalHeight * (screenY - marginTop) / VideoImageDisplayedHeight);
        }

        public int GetScaledWidthValue(int width)
        {
            return (int)(((decimal)width / VideoImageOriginalWidth) * VideoImageDisplayedWidth);
        }

        public int GetOriginalWidthValue(int screenWidth)
        {
            return (int)((decimal)VideoImageOriginalWidth * (screenWidth) / VideoImageDisplayedWidth);
        }
        public int GetScaledHeightValue(int height)
        {
            return (int)(((decimal)height / VideoImageOriginalHeight) * VideoImageDisplayedHeight);
        }

        public int GetOriginalHeightValue(int screenHeight)
        {
            return (int)((decimal)VideoImageOriginalHeight * (screenHeight) / VideoImageDisplayedHeight);
        }

        public void AddScreenPoint(int x, int y)
        {
            if (_poly == null) _poly = new Polygon();
            _poly.Add(new Point(GetOriginalXValue(x), GetOriginalYValue(y)));
            if (_poly.PointsCount == 1) _poly.Add(new Point(GetOriginalXValue(x), GetOriginalYValue(y))); // for first point
        }

        public void ResetCurrentDrawings()
        {
            _poly = null;
            _rect = new Rectangle();
        }

        internal bool IsPointClosedToFirst(int x, int y)
        {
            if (_poly != null)
            {
                var poly = GetScaledValue(_poly);
                if (poly.Count() > 0)
                {
                    if (Math.Abs(poly[0].X - x) < neighbour && (Math.Abs(poly[0].Y - y) < neighbour))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        internal bool IsInsideVideo(int x, int y)
        {
            return (GetOriginalXValue(x) >= 0 && GetOriginalXValue(x) < VideoImageOriginalWidth && GetOriginalYValue(y) >= 0 && GetOriginalYValue(y) < VideoImageOriginalHeight);
        }

        internal int ReturnXInsideVideo(int x)
        {
            if (x < marginLeft) return marginLeft;
            else if (x > marginLeft + VideoImageDisplayedWidth) return marginLeft + VideoImageDisplayedWidth;
            else return x;
        }

        internal int ReturnYInsideVideo(int y)
        {
            if (y < marginTop) return marginTop;
            else if (y > marginTop + VideoImageDisplayedHeight) return marginTop + VideoImageDisplayedHeight;
            else return y;
        }

        internal bool ReachedMaxSegments()
        {
            if (_poly != null)
            {
                var poly = GetScaledValue(_poly);
                if (poly.Count() == _maxNumberSegments + 1)
                {
                    return true;
                }
            }
            return false;
        }


        internal void SaveRegions()
        {
            _savedPolygons = new List<Polygon>();
            _savedPolygons.AddRange(_polygons);
        }

        internal void LoadSavedRegions()
        {
            _polygons = new List<Polygon>();
            _polygons.AddRange(_savedPolygons);
        }
    }


    class ButtonRegionEditor : Button
    {
        RegionEditor myRegionEditor;
        private IAsset _asset;
        private Mainform _main;
        private bool analysisJobSubmitted = false;

        public delegate void ChangedEventHandler(object sender, EventArgs e);
        // An event that clients can use to be notified whenever the
        // regions have been changed.
        public event ChangedEventHandler RegionsChanged;

        protected virtual void OnChanged(EventArgs e)
        {
            if (RegionsChanged != null)
                RegionsChanged(this, e);
        }

        public ButtonRegionEditor()
        {
            this.Click += ButtonXML_Click;
        }

        public void Initialize(IAsset asset, Mainform main, bool polygonsEnabled, int nbOfRegionsMax, bool croppingMode)
        {
            myRegionEditor = new RegionEditor(asset, polygonsEnabled, nbOfRegionsMax, croppingMode);
            _asset = asset;
            _main = main;
        }

        void ButtonXML_Click(object sender, EventArgs e)
        {
            bool thumbnailsexist = ThumbnailsAvailable(_asset);
            if (!thumbnailsexist)
            {
                if (analysisJobSubmitted)
                {
                    MessageBox.Show("Please wait for the analysis job to complete", "Thumbnails", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (MessageBox.Show("There is no detected thumbnails for this asset.\n\nDo you want to submit an analysis job now to generate the thumbnails and metadata ?", "No thumbnails", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _main.DoAnalyzeAssets(new List<IAsset>() { _asset }, false);
                    analysisJobSubmitted = true;
                }
                else
                {
                    return;
                }
            }
            else
            {
                analysisJobSubmitted = false;
                this.Cursor = Cursors.WaitCursor;
                myRegionEditor.Display();
                this.Cursor = Cursors.Arrow;
                OnChanged(EventArgs.Empty);
            }


        }

        public List<PolygoneDecimalMode> GetSavedPolygonesDecimalMode()
        {
            return myRegionEditor.GetSavedPolygonesDecimalMode();
        }

        public List<Rectangle> GetSavedPolygonesAsRectangleResolutionMode()
        {
            var polys = myRegionEditor.GetSavedPolygonesResolutionMode();
            List<Rectangle> listRect = new List<Rectangle>();
            foreach (var poly in polys)
            {
                listRect.Add(poly.ToRectangle());
            }
            return listRect;
        }

        static bool ThumbnailsAvailable(IAsset asset) // false
        {
            string filename = asset.Id.Substring(Constants.AssetIdPrefix.Length) + "_OriginalRes_000001.jpg";
            string path = Path.GetTempPath();
            string pathandfile = Path.Combine(path, filename);
            //c7508b75-a451-4887-9435-4a5b39f88c5f_OriginalRes_000001.jpg
            var files = asset.GetMediaContext().Files.Where(f => f.Name == filename).OrderBy(f => f.LastModified);
            var file = files.FirstOrDefault();

            if (file != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
