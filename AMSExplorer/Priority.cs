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
    public partial class Priority : Form
    {
        public int JobPriority
        {
            get
            {
                return (int)numericUpDownPriority.Value;
            }

            set
            {
                numericUpDownPriority.Value = value;
            }
        }
        public Priority()
        {
            InitializeComponent();
        }

        private void Priority_Load(object sender, EventArgs e)
        {

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {

        }
    }
}
