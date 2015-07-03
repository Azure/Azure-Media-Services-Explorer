//----------------------------------------------------------------------------------------------
//    Copyright 2015 Microsoft Corporation
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
        private Int64 scaledoffset = 0;
        private TimeSpan min = new TimeSpan(0);
        private TimeSpan max = new TimeSpan(Int64.MaxValue);
        private bool donotfirechangeevent = false;
        private long _scaledTotalDuration = -1;
        private bool _dvrmode = false; // inversed mode for dvr
        private bool _displaytrackbar = false;


        public TimeControl()
        {
            InitializeComponent();
        }

        public event EventHandler ValueChanged;

        private void HandleValueChanged(object sender, EventArgs e)
        {
            if (GetTimeStampAsTimeSpanWitoutOffset() > max)
            {
                donotfirechangeevent = true;
                SetTimeStamp(max);
                donotfirechangeevent = false;
            }

            if (GetTimeStampAsTimeSpanWitoutOffset() < min)
            {
                donotfirechangeevent = true;
                SetTimeStamp(min);
                donotfirechangeevent = false;
            }

            this.OnNumValueChanged(EventArgs.Empty);
        }


        private void HandleTrackBarValueChanged(object sender, EventArgs e)
        {
            donotfirechangeevent = true;
            double scale = ((double)TimeSpan.TicksPerSecond) / ((double)timescale);

            if (_dvrmode)
            {
                SetTimeStamp(new TimeSpan(Convert.ToInt64((double)_scaledTotalDuration * scale * (1000d - (double)trackBarTime.Value) / 1000d)));

            }
            else
            {
                SetTimeStamp(new TimeSpan(Convert.ToInt64((double)_scaledTotalDuration * scale * ((double)trackBarTime.Value) / 1000d)));

            }
            donotfirechangeevent = false;
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

        public Int64 ScaledFirstTimestampOffset // timestamp of first video chunk in manifest
        {
            get { return scaledoffset; }
            set { scaledoffset = value; }
        }

        public Int64 ScaledTotalDuration
        {
            get { return _scaledTotalDuration; }
            set { _scaledTotalDuration = value; }
        }

        public string Label1
        {
            get { return label1.Text; }
            set { label1.Text = value; }
        }

        public string Label2
        {
            get { return label2.Text; }
            set { label2.Text = value; }
        }

        public bool DisplayTrackBar
        {
            get
            {
                return _displaytrackbar;
            }
            set
            {
                trackBarTime.Visible = value;
                trackBarTime.Enabled = value;
                _displaytrackbar = value;
            }
        }



        public bool DVRMode
        {
            get
            {
                return _dvrmode;
            }
            set
            {
                _dvrmode = value;
            }
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


        public string GetScaledTimeStamp()
        {
            TimeSpan ts = GetTimeStampAsTimeSpanWitoutOffset();
            return Math.Truncate(((double)ts.Ticks) * ((double)timescale / (double)TimeSpan.TicksPerSecond)).ToString();
        }

        public string GetScaledTimeStampWithOffset()
        {
            TimeSpan ts = GetTimeStampAsTimeSpanWithOffset();
            return Math.Truncate(((double)ts.Ticks) * ((double)timescale / (double)TimeSpan.TicksPerSecond)).ToString();
        }

        public void SetScaledTimeStamp(string value)
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
                if (timestamp == Int64.MaxValue || timestamp == 0)
                {
                    SetTimeStamp(new TimeSpan(timestamp));
                }
                else
                {
                    double scale = ((double)TimeSpan.TicksPerSecond) / ((double)timescale);
                    TimeSpan ts = new TimeSpan(Convert.ToInt64((((double)timestamp) - (double)ScaledFirstTimestampOffset) * scale));
                    SetTimeStamp(ts);
                }
            }
        }


        public TimeSpan GetTimeStampAsTimeSpanWitoutOffset()
        {

            return new TimeSpan((int)numericUpDownDays.Value, (int)numericUpDownHours.Value, (int)numericUpDownMinutes.Value, (int)Math.Truncate(numericUpDownSeconds.Value), (int)(1000 * (numericUpDownSeconds.Value - Math.Truncate(numericUpDownSeconds.Value))));

        }

        public TimeSpan GetTimeStampAsTimeSpanWithOffset()
        {
            return GetOffSetAsTimeSpan() + new TimeSpan((int)numericUpDownDays.Value, (int)numericUpDownHours.Value, (int)numericUpDownMinutes.Value, (int)Math.Truncate(numericUpDownSeconds.Value), (int)(1000 * (numericUpDownSeconds.Value - Math.Truncate(numericUpDownSeconds.Value))));
        }

        public TimeSpan GetOffSetAsTimeSpan()
        {
            return new TimeSpan((long)((double)TimeSpan.TicksPerSecond * (double)ScaledFirstTimestampOffset / ((double)timescale)));
        }

        public void SetTimeStamp(TimeSpan value)
        {
            donotfirechangeevent = true;
            // trackbar update
            if (this.DisplayTrackBar)
            {
                double scale = ((double)TimeSpan.TicksPerSecond) / ((double)timescale);
                double durationinticks = ((double)_scaledTotalDuration) * scale;
                if (_dvrmode)
                {
                    trackBarTime.Value = (int)(1000d - 1000d * value.TotalMilliseconds / (new TimeSpan(Convert.ToInt64(durationinticks))).TotalMilliseconds);
                }
                else
                {
                    trackBarTime.Value = (int)(1000d * value.TotalMilliseconds / (new TimeSpan(Convert.ToInt64(durationinticks))).TotalMilliseconds);
                }
            }

            numericUpDownDays.Value = value.Days;
            numericUpDownHours.Value = value.Hours;
            numericUpDownMinutes.Value = value.Minutes;
            donotfirechangeevent = false;
            numericUpDownSeconds.Value = Convert.ToDecimal(value.Seconds + value.Milliseconds / 1000d);



        }

        private void trackBarStart_Scroll(object sender, EventArgs e)
        {
            /*
            if (_dvrmode)
            {
                this.TimestampAsTimeSpan = new TimeSpan(Convert.ToInt64((double)_totalDuration * (1000d - (double)trackBarTime.Value) / 1000d));

            }
            else
            {
                this.TimestampAsTimeSpan = new TimeSpan(Convert.ToInt64((double)_totalDuration * ((double)trackBarTime.Value) / 1000d));

            }
             * */
        }


    }
}
