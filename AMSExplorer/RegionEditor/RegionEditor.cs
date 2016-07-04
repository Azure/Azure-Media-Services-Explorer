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

namespace AMSExplorer
{
    public partial class RegionEditor : Form
    {
        string savedConfig;
        string defaultConfig;

        private bool _canDraw;
        private int _startX, _startY;
        private bool polygonalMode = false;
        private Point _point;

        private const string infoMouse = "{0}, {1}";
        private const string infoMouseDrawRectangle = "{0} x {1}";

        // toolStripStatusLabelMouseInfo


        public Image Picture
        {
            set
            {
                myPictureBox1.VideoImage = value;
                myPictureBox1.VideoImageOriginalWidth = value.Width;
                myPictureBox1.VideoImageOriginalHeight = value.Height;
            }
        }

        public RegionEditor(string title = null, string text = null, bool editMode = false, bool showSamplePremium = false, bool DisplayFormatButton = true, string infoText = null)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            if (title != null) this.Text = title;

            if (editMode)
            {
                buttonOk.Text = "Save";
            }
            else // readonly mode
            {
                buttonCancel.Text = "Close";
                buttonOk.Visible = false;

            }

            labelWarningJSON.Text = string.Empty;
            buttonClearLastRegion.Visible = DisplayFormatButton;

            if (infoText != null)
            {
                labelInfoText.Text = infoText;
                labelInfoText.Visible = true;
            }
        }

        private void EditorXMLJSON_Load(object sender, EventArgs e)
        {

        }



        private void buttonCopyClipboard_Click(object sender, EventArgs e)
        {

        }



        private void buttonFormat_Click(object sender, EventArgs e)
        {
            myPictureBox1.DeleteLastPolygone();

        }


        public DialogResult Display()
        {
            DialogResult DR = this.ShowDialog();
            /*
            if (DR == DialogResult.OK)
            {
                savedConfig = textBoxConfiguration.Text;
            }
            else // let's reset the controls to default
            {
                textBoxConfiguration.Text = savedConfig;
            }
            */
            return DR;
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

            if (!polygonalMode)
            {
                //The x-value of our rectangle should be the minimum between the start x-value and the current x-position
                int x = Math.Min(_startX, e.X);
                //The y-value of our rectangle should also be the minimum between the start y-value and current y-value
                int y = Math.Min(_startY, e.Y);

                //The width of our rectangle should be the maximum between the start x-position and current x-position minus
                //the minimum of start x-position and current x-position
                int width = Math.Max(_startX, e.X) - Math.Min(_startX, e.X);

                //For the hight value, it's basically the same thing as above, but now with the y-values:
                int height = Math.Max(_startY, e.Y) - Math.Min(_startY, e.Y);
                //Refresh the form and draw the rectangle

                // myPictureBox1.ScreenDrawingRectangle = new RectangleDec(x - myPictureBox1.marginLeft, y - myPictureBox1.marginTop, width, height, myPictureBox1.VideoImageDisplayedWidth, myPictureBox1.VideoImageDisplayedHeight);
                myPictureBox1.SetScreenDrawingRectangle(x, y, width, height);

                // let's update the textbox info
                toolStripStatusLabelMouseInfo.Text = string.Format(infoMouse, myPictureBox1.GetOriginalXValue(x), myPictureBox1.GetOriginalYValue(y));
                toolStripStatusLabelXYRect.Text = string.Format(infoMouseDrawRectangle, myPictureBox1.GetOriginalWidthValue(width), myPictureBox1.GetOriginalWidthValue(height));

            }
            else
            {
                myPictureBox1.UpdateScreenDrawingPolygone(e.X, e.Y);
            }



            Refresh();
        }



        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (!polygonalMode) // rectangle
            {
                //The system is now allowed to draw rectangles
                _canDraw = true;
                //Initialize and keep track of the start position
                _startX = e.X;
                _startY = e.Y;

                toolStripStatusLabelXYRect.Visible = true;
            }
            else
            {
                if (e.Button == MouseButtons.Left)
                {
                    _canDraw = true;
                    myPictureBox1.AddScreenPoint(e.X, e.Y);

                }
                else if (e.Button == MouseButtons.Right)
                {
                    EndOfPolygonalDrawing();

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

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (!polygonalMode)
            {
                //The system is no longer allowed to draw rectangles
                _canDraw = false;

                myPictureBox1.DrawingRectangleIsFinal();
                toolStripStatusLabelXYRect.Visible = false;
            }

        }

        internal List<PolygoneDecimalMode> GetPolygonesDecimalMode()
        {
            return myPictureBox1.GetPolygonesDecimalMode;
        }

        private void buttonClearAllRegions_Click(object sender, EventArgs e)
        {
            myPictureBox1.DeleteAllPolygones();
        }

        private void radioButtonPolygonal_CheckedChanged(object sender, EventArgs e)
        {
            polygonalMode = radioButtonPolygonal.Checked;
            myPictureBox1.ResetCurrentDrawings();
            _canDraw = false;
            Refresh();
        }

        private void myPictureBox1_DoubleClick(object sender, EventArgs e)
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

    class Polygone
    {
        public List<Point> points;

        public Polygone()
        {
            points = new List<Point>();
        }

        public void Add(Point point)
        {
            points.Add(point);
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

        public Polygone ToPolygone(int imageWidth, int imageheight, int addmarginleft = 0, int addmargintop = 0)
        {
            var poly = new Polygone();
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
        private List<PolygoneDecimalMode> _polygones = new List<PolygoneDecimalMode>();

        //private List<RectangleDecimalMode> _rectangles = new List<RectangleDecimalMode>();
        private RectangleDecimalMode _rect;

        private PolygoneDecimalMode _poly;

        public Image VideoImage = null;
        public int marginLeft = 0;
        public int marginTop = 0;
        public int VideoImageDisplayedWidth = 0;
        public int VideoImageDisplayedHeight = 0;
        public int VideoImageOriginalWidth = 0;
        public int VideoImageOriginalHeight = 0;

        public List<PolygoneDecimalMode> GetPolygonesDecimalMode
        {
            get
            {
                return _polygones;
            }
        }

        public List<Polygone> GetPolygonesResolutionMode
        {
            get
            {
                List<Polygone> polygonesDec = new List<Polygone>();
                foreach (var poly in _polygones)
                {
                    polygonesDec.Add(poly.ToPolygone(VideoImage.Width, VideoImage.Height));
                }
                return polygonesDec;
            }
        }



        public void SetScreenDrawingRectangle(int x, int y, int width, int height)
        {
            _rect = new RectangleDecimalMode(x - marginLeft, y - marginTop, width, height, VideoImageDisplayedWidth, VideoImageDisplayedHeight);
        }

        public void UpdateScreenDrawingPolygone(int x, int y)
        {
            _poly.SetCurrentPoint(new Point(x - marginLeft, y - marginTop), VideoImageDisplayedWidth, VideoImageDisplayedHeight);
        }



        public void DeleteAllPolygones()
        {
            _polygones = new List<PolygoneDecimalMode>();
            Refresh();
        }

        public void DeleteLastPolygone()
        {
            if (_polygones.Count > 0)
            {
                _polygones.RemoveAt(_polygones.Count - 1);
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
            using (Pen pen = new Pen(Color.Red, 2))
            {
                //Draw the rectangle on our form with the pen
                e.Graphics.Clear(SystemColors.Window);
                e.Graphics.DrawImage(VideoImage, marginLeft, marginTop, VideoImageDisplayedWidth, VideoImageDisplayedHeight);
                e.Graphics.DrawRectangle(new Pen(Color.Yellow, 1), marginLeft, marginTop, VideoImageDisplayedWidth - 1, VideoImageDisplayedHeight - 1);

                int index = 0;
                /*
                foreach (var recdec in _rectangles)
                {
                    var r = recdec.ToRectangle(VideoImageDisplayedWidth, VideoImageDisplayedHeight, marginLeft, marginTop);

                    Point[] p = { new Point(r.Left, r.Top), new Point(r.Right, r.Top), new Point(r.Right, r.Bottom), new Point(r.Left, r.Bottom) };
                    e.Graphics.DrawRectangle(pen, r);
                    //e.Graphics.DrawPolygon(pen, p);
                    e.Graphics.DrawString(index.ToString(), new Font("Segoe UI", 9), new SolidBrush(Color.Red), r);
                    index++;
                }
                */
                if (_rect != null)
                {
                    var rect = _rect.ToRectangle(VideoImageDisplayedWidth, VideoImageDisplayedHeight, marginLeft, marginTop);
                    e.Graphics.DrawRectangle(pen, rect);
                    e.Graphics.DrawString(index.ToString(), new Font("Segoe UI", 9), new SolidBrush(Color.Red), rect);
                }


                foreach (var polydec in _polygones)
                {
                    //var r = recdec.ToRectangle(VideoImageDisplayedWidth, VideoImageDisplayedHeight, marginLeft, marginTop);

                    //Point[] p = { new Point(r.Left, r.Top), new Point(r.Right, r.Top), new Point(r.Right, r.Bottom), new Point(r.Left, r.Bottom) };
                    //e.Graphics.DrawRectangle(pen, r);
                    var poly = polydec.ToPoints(VideoImageDisplayedWidth, VideoImageDisplayedHeight, marginLeft, marginTop);
                    e.Graphics.DrawPolygon(pen, poly);
                    e.Graphics.DrawString(index.ToString(), new Font("Segoe UI", 9), new SolidBrush(Color.Red), poly[0].X, poly[0].Y);
                    index++;
                }
                if (_poly != null)
                {
                    var poly = _poly.ToPoints(VideoImageDisplayedWidth, VideoImageDisplayedHeight, marginLeft, marginTop);
                    e.Graphics.DrawPolygon(pen, poly);
                    e.Graphics.DrawString(index.ToString(), new Font("Segoe UI", 9), new SolidBrush(Color.Red), poly[0].X, poly[0].Y);
                }
            }
        }

        public void DrawingRectangleIsFinal()
        {
            if (_rect != null) _polygones.Add(_rect.ToPolygone());
            _rect = null;
        }

        public void DrawingPolygoneIsFinal()
        {
            if (_poly != null && _poly.ToDecimalPoints().Count() > 3)
            {
                _poly.RemoveLastPoint();
                _polygones.Add(_poly);
            }

            _poly = null;
        }

        public int GetOriginalXValue(int screenX)
        {
            return (int)((decimal)VideoImageOriginalWidth * (screenX - marginLeft) / VideoImageDisplayedWidth);
        }

        public int GetOriginalYValue(int screenY)
        {
            return (int)((decimal)VideoImageOriginalHeight * (screenY - marginTop) / VideoImageDisplayedHeight);
        }

        public int GetOriginalWidthValue(int screenWidth)
        {
            return (int)((decimal)VideoImageOriginalWidth * (screenWidth) / VideoImageDisplayedWidth);
        }

        public int GetOriginalHeightValue(int screenHeight)
        {
            return (int)((decimal)VideoImageOriginalHeight * (screenHeight) / VideoImageDisplayedHeight);
        }

        public void AddScreenPoint(int x, int y)
        {
            if (_poly == null) _poly = new PolygoneDecimalMode();
            _poly.AddPoint(new Point(GetOriginalXValue(x), GetOriginalYValue(y)), VideoImageOriginalWidth, VideoImageOriginalHeight);
            _poly.AddPoint(new Point(GetOriginalXValue(x), GetOriginalYValue(y)), VideoImageOriginalWidth, VideoImageOriginalHeight); // for current point

        }

        public void ResetCurrentDrawings()
        {
            _poly = null;
            _rect = null;
        }
    }


    class ButtonRegionEditor : Button
    {
        RegionEditor myRegionEditor;

        public ButtonRegionEditor()
        {
            this.Click += ButtonXML_Click;
        }

        public void Initialize(IAsset asset)
        {
            var assetImage = ReturnOriginResolution(asset);
            this.Enabled = assetImage != null;
            myRegionEditor = new RegionEditor(editMode: true, showSamplePremium: true);

            if (assetImage != null)
            {
                myRegionEditor.Picture = assetImage;
            }
        }

        void ButtonXML_Click(object sender, EventArgs e)
        {
            myRegionEditor.Display();
        }

        public List<PolygoneDecimalMode> GetPolygonesDecimalMode()
        {
            //return myRegionEditor.TextData;
            return myRegionEditor.GetPolygonesDecimalMode();
        }

        static Image ReturnOriginResolution(IAsset asset) // null if not existing
        {
            Image im = null;

            string filename = asset.Id.Substring(Constants.AssetIdPrefix.Length) + "_OriginalRes_000001.jpg";
            string path = Path.GetTempPath();
            string pathandfile = Path.Combine(path, filename);
            //c7508b75-a451-4887-9435-4a5b39f88c5f_OriginalRes_000001.jpg
            var file = asset.GetMediaContext().Files.Where(f => f.Name == filename).FirstOrDefault();

            if (file != null)
            {
                try
                {
                    if (File.Exists(pathandfile)) File.Delete(pathandfile);
                    file.Download(pathandfile);
                    im = Image.FromFile(pathandfile);
                    File.Delete(pathandfile);
                }
                catch (Exception e)
                { }
            }

            return im;
        }

    }
}
