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

        public Image Picture
        {
            set
            {
                myPictureBox1.VideoImage = value;
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
            buttonFormat.Visible = DisplayFormatButton;

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
            if (!_canDraw) return;

            //The x-value of our rectangle should be the minimum between the start x-value and the current x-position
            int x = Math.Min(_startX, e.X);
            //The y-value of our rectangle should also be the minimum between the start y-value and current y-value
            int y = Math.Min(_startY, e.Y);

            //The width of our rectangle should be the maximum between the start x-position and current x-position minus
            //the minimum of start x-position and current x-position
            int width = Math.Max(_startX, e.X) - Math.Min(_startX, e.X);

            //For the hight value, it's basically the same thing as above, but now with the y-values:
            int height = Math.Max(_startY, e.Y) - Math.Min(_startY, e.Y);
            myPictureBox1._rect = new RectangleDec(x - myPictureBox1.marginLeft, y - myPictureBox1.marginTop, width, height, myPictureBox1.VideoImageDisplayedWidth, myPictureBox1.VideoImageDisplayedHeight);
            //Refresh the form and draw the rectangle
            Refresh();


        }



        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            //The system is now allowed to draw rectangles
            _canDraw = true;
            //Initialize and keep track of the start position
            _startX = e.X;
            _startY = e.Y;


        }

        private void myPictureBox1_SizeChanged(object sender, EventArgs e)
        {
            Refresh();
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            //The system is no longer allowed to draw rectangles
            _canDraw = false;

            myPictureBox1._rectangles.Add(myPictureBox1._rect);

        }

        internal List<RectangleDec> GetRectanglesDecimalMode()
        {
            return myPictureBox1.GetRectanglesDecimalMode;
        }

        internal List<Rectangle> GetRectanglesResolutionMode()
        {
            return myPictureBox1.GetRectanglesResolutionMode;
        }
    }

    class RectangleDec
    {

        public decimal X;
        public decimal Y;
        public decimal Width;
        public decimal Height;

        public RectangleDec()
        {
            X = Y = Width = Height = 0;
        }

        public RectangleDec(Rectangle rect, int imageWidth, int imageheight)
        {
            X = ((decimal)rect.X / imageWidth);
            Y = ((decimal)rect.Y / imageheight);
            Width = ((decimal)rect.Width / imageWidth);
            Height = ((decimal)rect.Height / imageheight);
        }


        public RectangleDec(int x, int y, int width, int height, int imageWidth, int imageheight)
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

    }

    class myPictureBox : PictureBox
    {
        public List<RectangleDec> _rectangles = new List<RectangleDec>();
        public RectangleDec _rect;
        public Image VideoImage = null;
        public int marginLeft = 0;
        public int marginTop = 0;
        public int VideoImageDisplayedWidth = 0;
        public int VideoImageDisplayedHeight = 0;


        public List<RectangleDec> GetRectanglesDecimalMode
        {
            get
            {
                return _rectangles;
            }
        }

        public List<Rectangle> GetRectanglesResolutionMode
        {
            get
            {
                List<Rectangle> rectanglesDec = new List<Rectangle>();
                foreach (var rec in _rectangles)
                {
                    rectanglesDec.Add(rec.ToRectangle(VideoImage.Width, VideoImage.Height));
                }
                return rectanglesDec;
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
                Debug.Print(string.Format("controlwidth : {0}, controlheight : {1}", this.Width, this.Height));
                Debug.Print(string.Format("picturewidth : {0}, pictureheight : {1}", VideoImageDisplayedWidth, VideoImageDisplayedHeight));
                Debug.Print(string.Format("marginleft   : {0}, margintop     : {1}", marginLeft, marginTop));

                //Draw the rectangle on our form with the pen
                e.Graphics.Clear(Color.WhiteSmoke);
                e.Graphics.DrawImage(VideoImage, marginLeft, marginTop, VideoImageDisplayedWidth, VideoImageDisplayedHeight);
                foreach (var recdec in _rectangles)
                {
                    e.Graphics.DrawRectangle(pen, recdec.ToRectangle(VideoImageDisplayedWidth, VideoImageDisplayedHeight, marginLeft, marginTop));
                }
                if (_rect != null)
                {
                    var r = _rect.ToRectangle(VideoImageDisplayedWidth, VideoImageDisplayedHeight, marginLeft, marginTop);
                    e.Graphics.DrawRectangle(pen, _rect.ToRectangle(VideoImageDisplayedWidth, VideoImageDisplayedHeight, marginLeft, marginTop));
                }
            }
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

        public List<RectangleDec> GetRectanglesDecimalMode()
        {
            //return myRegionEditor.TextData;
            return myRegionEditor.GetRectanglesDecimalMode();
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
