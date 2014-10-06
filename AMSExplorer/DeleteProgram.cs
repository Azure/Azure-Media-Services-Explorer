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
    public partial class DeleteProgram : Form
    {
      


        public bool DeleteAsset
        {
            get
            {
                return checkBoxDeleteAsset.Checked;
            }
        }

        public DeleteProgram(string label)
        {
            InitializeComponent();
            labelmain.Text = label;
        }

        private void DeleteProgram_Load(object sender, EventArgs e)
        {
        }
    }
}
