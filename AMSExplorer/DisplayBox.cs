using System;
using System.Windows.Forms;

namespace AMSExplorer
{
    public partial class DisplayBox : Form
    {
        int nbsecond = 0;

        public DisplayBox(string title, string label, int? nbseconds = null)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;

            this.Text = title;
            label1.Text = label;
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
            this.Close();
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
    }
}
