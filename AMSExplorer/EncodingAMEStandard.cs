using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using Microsoft.WindowsAzure.MediaServices.Client;
using System.Reflection;

namespace AMSExplorer
{
    public partial class EncodingAMEStandard : Form
    {
        public XDocument doc;
        public string EncodingAMEStdPresetXMLFiles;

        private List<IMediaProcessor> Procs;
        public List<IAsset> SelectedAssets;
        private CloudMediaContext _context;

        public string EncodingLabel
        {
            set
            {
                label.Text = value;
            }
        }

        public string EncodingJobName
        {
            get
            {
                return textBoxJobName.Text;
            }
            set
            {
                textBoxJobName.Text = value;
            }
        }


        public List<IMediaProcessor> EncodingProcessorsList
        {
            set
            {
                foreach (IMediaProcessor pr in value)
                {
                    comboBoxProcessor.Items.Add(string.Format("{0} {1} Version {2} ({3})", pr.Vendor, pr.Name, pr.Version, pr.Description));
                }
                if (comboBoxProcessor.Items.Count > 0)
                {
                    comboBoxProcessor.SelectedIndex = 0;
                }
                Procs = value;
            }
        }

        public IMediaProcessor EncodingProcessorSelected
        {
            get
            {
                return Procs[comboBoxProcessor.SelectedIndex];
            }
        }

        public string EncodingOutputAssetName
        {
            get
            {
                return textboxoutputassetname.Text;
            }
            set
            {
                textboxoutputassetname.Text = value;
            }
        }


        public string EncodingConfiguration
        {
            get
            {
                return textBoxConfiguration.Text;
            }
        }

        public JobOptionsVar JobOptions
        {
            get
            {
                return buttonJobOptions.GetSettings();
            }
            set
            {
                buttonJobOptions.SetSettings(value);
            }
        }


        public EncodingAMEStandard(CloudMediaContext context)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            _context = context;
            buttonJobOptions.Initialize(_context);
        }



        private void EncodingAMEStandard_Shown(object sender, EventArgs e)
        {
        }

        private void EncodingAMEStandard_Load(object sender, EventArgs e)
        {
          
        }




  

        private void buttonLoadXML_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(this.EncodingAMEStdPresetXMLFiles))
                openFileDialogPreset.InitialDirectory = this.EncodingAMEStdPresetXMLFiles;
            if (openFileDialogPreset.ShowDialog() == DialogResult.OK)
            {
                this.EncodingAMEStdPresetXMLFiles = Path.GetDirectoryName(openFileDialogPreset.FileName); // let's save the folder
                bool Error = false;
                try
                {
                    doc = XDocument.Load(openFileDialogPreset.FileName);
                    textBoxConfiguration.Text = doc.Declaration.ToString() + doc.ToString();
                    buttonSaveXML.Enabled = true;
                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                    Error = true;
                }
                
            }
        }

        private void buttonSaveXML_Click(object sender, EventArgs e)
        {
            if (saveFileDialogPreset.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var tempdoc = XDocument.Parse(textBoxConfiguration.Text);
                    tempdoc.Save(saveFileDialogPreset.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not save file to disk. Original error: " + ex.Message);
                }

            }
        }
    }



}
