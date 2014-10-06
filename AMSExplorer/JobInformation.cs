using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAzure.MediaServices.Client;


namespace AMSExplorer
{
    public partial class JobInformation : Form
    {

        public IJob MyJob;
        private CloudMediaContext _context;
        public JobInformation(CloudMediaContext context)
        {
            InitializeComponent();
            _context = context;
        }



        private void contextMenuStrip_MouseClick(object sender, MouseEventArgs e)
        {
            ContextMenuStrip contextmenu = (ContextMenuStrip)sender;
            DataGridView DG = (DataGridView)contextmenu.SourceControl;

            if (DG.SelectedCells.Count == 1)
            {
                if (DG.SelectedCells[0].Value != null)
                {
                    System.Windows.Forms.Clipboard.SetText(DG.SelectedCells[0].Value.ToString());

                }
                else
                {
                    System.Windows.Forms.Clipboard.Clear();
                }

            }

        }

        private void buttonCopyStats_Click(object sender, EventArgs e)
        {
            DoJobStats();
        }

        public void DoJobStats()
        {
            JobInfo JR = new JobInfo(MyJob);
            JR.CopyStatsToClipBoard();
        }

        private void JobInformation_Load(object sender, EventArgs e)
        {

            labelJobNameTitle.Text += MyJob.Name;

            DGJob.ColumnCount = 2;
            DGTasks.ColumnCount = 2;
            DGTasks.Columns[0].DefaultCellStyle.BackColor = Color.Gainsboro;


            DGErrors.ColumnCount = 3;
            DGErrors.Columns[0].HeaderText = "Task";
            DGErrors.Columns[1].HeaderText = "Error detail";
            DGErrors.Columns[2].HeaderText = "Code";

            DGJob.Columns[0].DefaultCellStyle.BackColor = Color.Gainsboro;
            DGJob.Rows.Add("Name", MyJob.Name);
            DGJob.Rows.Add("Id", MyJob.Id);
            DGJob.Rows.Add("State", MyJob.State);
            DGJob.Rows.Add("Priority", MyJob.Priority);
            DGJob.Rows.Add("Overall progress", MyJob.GetOverallProgress());

            if (MyJob.StartTime != null) DGJob.Rows.Add("Start Time", ((DateTime)MyJob.StartTime).ToLocalTime());
            if (MyJob.EndTime != null) DGJob.Rows.Add("End Time", ((DateTime)MyJob.EndTime).ToLocalTime());

            if ((MyJob.StartTime != null) && (MyJob.EndTime != null))
            {
                DGJob.Rows.Add("Job Duration", ((DateTime)MyJob.EndTime).Subtract((DateTime)MyJob.StartTime));
            }
            DGJob.Rows.Add("CPU Duration", MyJob.RunningDuration);
            DGJob.Rows.Add("Created", ((DateTime)MyJob.Created).ToLocalTime());
            DGJob.Rows.Add("Last Modified", ((DateTime)MyJob.LastModified).ToLocalTime());
            DGJob.Rows.Add("Template Id", MyJob.TemplateId);

            string sid = "";
            if (MyJob.InputMediaAssets.Count() > 1) sid = " #{0}"; else sid = "";
            for (int i = 0; i < MyJob.InputMediaAssets.Count(); i++)
            {
                DGJob.Rows.Add("Input asset" + string.Format(sid, i) + " Name", MyJob.InputMediaAssets[i].Name);
                DGJob.Rows.Add("Input asset" + string.Format(sid, i) + " Id", MyJob.InputMediaAssets[i].Id);
            }

            if (MyJob.OutputMediaAssets.Count() > 1) sid = " #{0}"; else sid = "";
            for (int i = 0; i < MyJob.OutputMediaAssets.Count(); i++)
            {
                DGJob.Rows.Add("Output asset" + string.Format(sid, i) + " Name", MyJob.OutputMediaAssets[i].Name);
                DGJob.Rows.Add("Output asset" + string.Format(sid, i) + " Id", MyJob.OutputMediaAssets[i].Id);
            }


            bool btaskinjob = (MyJob.Tasks.Count() > 0);


            if (btaskinjob)
            {
                foreach (ITask task in MyJob.Tasks)
                {
                    listBoxTasks.Items.Add(task.Name);

                    for (int i = 0; i < task.ErrorDetails.Count(); i++)
                    {
                        DGErrors.Rows.Add(task.Name, task.ErrorDetails[i].Message, task.ErrorDetails[i].Code);
                    }

                }
                listBoxTasks.SelectedIndex = 0;
            }
        }

        private void buttonCreateMail_Click(object sender, EventArgs e)
        {
            DoJobCreateMail();
        }

        private void DoJobCreateMail()
        {
            JobInfo JR = new JobInfo(MyJob);
            JR.CreateOutlookMail();
        }

        private void listBoxTasks_SelectedIndexChanged(object sender, EventArgs e)
        {
            ITask task = MyJob.Tasks.Skip(listBoxTasks.SelectedIndex).Take(1).FirstOrDefault();

            DGTasks.Rows.Clear();

            DGTasks.Rows.Add("Name", task.Name);
            DGTasks.Rows.Add("Id", task.Id);
            DGTasks.Rows.Add("State", task.State);
            DGTasks.Rows.Add("Priority", task.Priority);
            if (task.StartTime != null) DGTasks.Rows.Add("Start Time", ((DateTime)task.StartTime).ToLocalTime());
            if (task.EndTime != null) DGTasks.Rows.Add("End Time", ((DateTime)task.EndTime).ToLocalTime());
            DGTasks.Rows.Add("Progress", task.Progress);
            DGTasks.Rows.Add("Duration", task.RunningDuration);
            DGTasks.Rows.Add("Perf Message", task.PerfMessage);
            DGTasks.Rows.Add("Configuration", task.Configuration);
            DGTasks.Rows.Add("Encryption Key Id", task.EncryptionKeyId);
            DGTasks.Rows.Add("Encryption Scheme", task.EncryptionScheme);
            DGTasks.Rows.Add("Encryption Version", task.EncryptionVersion);

            // let's get the name of the processor
            bool Error = false;
            IMediaProcessor processor = null;
            try
            {
                var processorquery =
                  from p in _context.MediaProcessors
                  orderby p.Version descending
                  where p.Id == task.MediaProcessorId
                  select p;
                // Reference the asset as an IAsset.
                processor = processorquery.FirstOrDefault(); //lastordefault returns an error so that's why we use first with sorting descending
            }
            catch
            {
                Error = true;
            }
            if (!Error)
            {
                DGTasks.Rows.Add("Mediaprocessor Name", processor.Name);
                DGTasks.Rows.Add("Mediaprocessor Id", task.MediaProcessorId);
            }


            DGTasks.Rows.Add("Task Body", task.TaskBody);
            DGTasks.Rows.Add("Options", task.Options);
            DGTasks.Rows.Add("Initialization Vector", task.InitializationVector);

            string sid = "";
            if (task.InputAssets.Count() > 1) sid = " #{0}"; else sid = "";
            for (int i = 0; i < task.InputAssets.Count(); i++)
            {
                DGTasks.Rows.Add("Input asset" + string.Format(sid, i + 1) + " Name", task.InputAssets[i].Name);
                DGTasks.Rows.Add("Input asset" + string.Format(sid, i + 1) + " Id", task.InputAssets[i].Id);
            }

            if (task.OutputAssets.Count() > 1) sid = " #{0}"; else sid = "";
            for (int i = 0; i < task.OutputAssets.Count(); i++)
            {
                DGTasks.Rows.Add("Output asset" + string.Format(sid, i + 1) + " Name", task.OutputAssets[i].Name);
                DGTasks.Rows.Add("Output asset" + string.Format(sid, i + 1) + " Id", task.OutputAssets[i].Id);
            }


            long inputsize = JobInfo.GetInputFilesSize(task);
            long outputsize = JobInfo.GetOutputFilesSize(task);
            if ((inputsize != -1) && (outputsize != -1))
            {
                DGTasks.Rows.Add("Input size", AssetInfo.FormatByteSize(inputsize));
                DGTasks.Rows.Add("Output size", AssetInfo.FormatByteSize(outputsize));
                DGTasks.Rows.Add("Processed size", AssetInfo.FormatByteSize(inputsize + outputsize));
            }
            else
            {
                DGTasks.Rows.Add("Input/output size", "undefined, task did not finished");

            }
            for (int i = 0; i < task.ErrorDetails.Count(); i++)
            {
                DGTasks.Rows.Add("Error", task.ErrorDetails[i].Code + ": " + task.ErrorDetails[i].Message);

            }
        }
    }
}
