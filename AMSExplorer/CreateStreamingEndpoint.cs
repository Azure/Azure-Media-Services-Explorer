using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AMSExplorer
{
    public partial class CreateStreamingEndpoint : Form
    {
        public string OriginName
        {
            get { return textboxoriginname.Text; }
            set { textboxoriginname.Text = value; }
        }
        public string OriginDescription
        {
            get { return textBoxOriginDescription.Text; }
            set { textBoxOriginDescription.Text = value; }
        }

        public int scaleUnits
        {
            get { return Convert.ToInt32(numericUpDownRU.Value); }
            set { numericUpDownRU.Value = value; }
        }

        public CreateStreamingEndpoint()
        {
            InitializeComponent();
        }


        private void CreateOrigin_Load(object sender, EventArgs e)
        {

        }

    }
}
