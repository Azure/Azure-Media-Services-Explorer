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
    public partial class ImportHttp : Form
    {
        public Uri GetURL
        {
            get
            {
                return new Uri(textBoxURL.Text);
            }

            set
            {
                textBoxURL.Text = value.ToString();
            }
        }

        public string GetAssetFileName
        {
            get
            {
                return textBoxAssetFileName.Text;
            }

        }

        public string GetAssetName
        {
            get
            {
                return textBoxAssetName.Text;
            }

        }


        public ImportHttp()
        {
            InitializeComponent();

        }

        private void ImportHttp_Load(object sender, EventArgs e)
        {
            labelURLFileNameWarning.Text = string.Empty;
        }

        private void textBoxURL_TextChanged(object sender, EventArgs e)
        {
            string filename = null;
            bool Error = false;
            try
            {
                filename = System.IO.Path.GetFileName(this.GetURL.LocalPath);
            }
            catch
            {
                Error = true;
                labelURLFileNameWarning.Text = "File name not found in the URL";
            }

            if (!Error)
            {
                labelURLFileNameWarning.Text = string.Empty;
                textBoxAssetName.Text = filename;
                textBoxAssetFileName.Text = filename;
            }
        }
    }
}
