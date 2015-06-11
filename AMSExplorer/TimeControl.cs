using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AMSExplorer
{
    public partial class TimeControl : UserControl
    {
        private Int64 timescale = TimeSpan.TicksPerSecond;
        private TimeSpan min = new TimeSpan(0);
        private TimeSpan max = new TimeSpan(Int64.MaxValue);
        private bool donotfirechangeevent = false;

        public TimeControl()
        {
            InitializeComponent();


        }

        public event EventHandler ValueChanged;

        private void HandleValueChanged(object sender, EventArgs e)
        {

            if (TimestampAsTimeSpan > max)
            {
                donotfirechangeevent = true;
                TimestampAsTimeSpan = max;
                donotfirechangeevent = false;
            }

            if (TimestampAsTimeSpan < min)
            {
                donotfirechangeevent = true;
                TimestampAsTimeSpan = min;
                donotfirechangeevent = false;
            }

            this.OnNumValueChanged(EventArgs.Empty);


        }

        protected virtual void OnNumValueChanged(EventArgs e)
        {
            EventHandler handler = this.ValueChanged;
            if (handler != null && !donotfirechangeevent)
            {
                handler(this, e);
            }
        }

        public Int64 TimeScale
        {
            get { return timescale; }
            set { timescale = value; }
        }

        public TimeSpan Min
        {
            get { return min; }
            set { min = value; }
        }

        public TimeSpan Max
        {
            get { return max; }
            set { max = value; }
        }

        public string Timestamp
        {
            get
            {
                TimeSpan ts = TimestampAsTimeSpan;
                return Math.Truncate(((double)ts.Ticks) * ((double)timescale / (double)TimeSpan.TicksPerSecond)).ToString();
            }
            set
            {
                bool Error = false;
                long timestamp = -1;
                try
                {
                    timestamp = long.Parse(value);
                }
                catch
                {
                    Error = true;
                }
                if (!Error)
                {
                    if (timestamp == Int64.MaxValue)
                    {
                        TimestampAsTimeSpan = new TimeSpan(Int64.MaxValue);
                    }
                    else
                    {
                        double scale = ((double)TimeSpan.TicksPerSecond) / ((double)timescale);
                        TimeSpan ts = new TimeSpan(Convert.ToInt64(((double)timestamp) * scale));
                        TimestampAsTimeSpan = ts;
                    }
                }
            }
        }

        public TimeSpan TimestampAsTimeSpan
        {
            get
            {
                if (checkBoxMax.Checked)
                {
                    return new TimeSpan(Int64.MaxValue);
                }
                else
                {
                    return new TimeSpan((int)numericUpDownDays.Value, (int)numericUpDownHours.Value, (int)numericUpDownMinutes.Value, (int)Math.Truncate(numericUpDownSeconds.Value), (int)(1000 * (numericUpDownSeconds.Value - Math.Truncate(numericUpDownSeconds.Value))));
                }

            }
            set
            {
                donotfirechangeevent = true;
                if (value == new TimeSpan(Int64.MaxValue))
                {
                    checkBoxMax.Checked = true;
                    numericUpDownDays.Value = numericUpDownHours.Value = numericUpDownMinutes.Value = numericUpDownSeconds.Value = 0;
                    numericUpDownDays.Enabled = numericUpDownHours.Enabled = numericUpDownMinutes.Enabled = numericUpDownSeconds.Enabled = false;
                }
                else
                {
                    numericUpDownDays.Enabled = numericUpDownHours.Enabled = numericUpDownMinutes.Enabled = numericUpDownSeconds.Enabled = true;
                    numericUpDownDays.Value = value.Days;
                    numericUpDownHours.Value = value.Hours;
                    numericUpDownMinutes.Value = value.Minutes;
                    numericUpDownSeconds.Value = Convert.ToDecimal(value.Seconds + value.Milliseconds / 1000d);
                    checkBoxMax.Checked = false;
                }
                donotfirechangeevent = false;
            }
        }
    }
}
