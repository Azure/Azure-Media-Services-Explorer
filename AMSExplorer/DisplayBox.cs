using System;
using System.Windows.Forms;

namespace AMSExplorer
{
    public partial class DisplayBox : Form
    {
        private int nbsecond = 0;

        public DisplayBox(string title, string label, int? nbseconds = null)
        {
            InitializeComponent();
            Icon = Bitmaps.Azure_Explorer_ico;

            Text = title;
            labelTitle.Text = label;
            if (nbseconds != null)
            {
                nbsecond = (int)nbseconds;
                button1.Enabled = false;
                timer1.Interval = 1000;
                timer1.Tick += new EventHandler(MyTimer_Tick);
                timer1.Start();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MyTimer_Tick(object sender, EventArgs e)
        {
            nbsecond--;
            if (nbsecond <= 0)
            {
                button1.Enabled = true;
                button1.Text = "OK";
            }
            else
            {
                button1.Text = string.Format("OK ({0})", nbsecond);
            }
        }

        private void DisplayBox_Load(object sender, EventArgs e)
        {
            DpiUtils.InitPerMonitorDpi(this);
        }

        private void DisplayBox_DpiChanged(object sender, DpiChangedEventArgs e)
        {
            DpiUtils.UpdatedSizeFontAfterDPIChange(labelTitle, e);
        }
    }
}
