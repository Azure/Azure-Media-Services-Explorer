//----------------------------------------------------------------------------------------------
//    Copyright 2020 Microsoft Corporation
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
using System.Windows.Forms;

namespace AMSExplorer
{
    public partial class TimeControl : UserControl
    {
        private long? timescale;
        private ulong scaledoffset = 0;
        private TimeSpan min = new TimeSpan(0);
        private TimeSpan max = new TimeSpan(long.MaxValue);
        private bool donotfirechangeevent = false;
        private TimeSpan _TotalDuration = TimeSpan.FromHours(24); // default max for DVR
        private bool _displaytrackbar = false;

        public TimeControl()
        {
            InitializeComponent();
        }

        public event EventHandler ValueChanged;

        private void HandleValueChanged(object sender, EventArgs e)
        {
            if (TimeStampWithoutOffset > max)
            {
                donotfirechangeevent = true;
                SetTimeStamp(max);
                donotfirechangeevent = false;
            }

            if (TimeStampWithoutOffset < min)
            {
                donotfirechangeevent = true;
                SetTimeStamp(min);
                donotfirechangeevent = false;
            }

            OnNumValueChanged(EventArgs.Empty);
        }


        private void HandleTrackBarValueChanged(object sender, EventArgs e)
        {
            donotfirechangeevent = true;
            double scale = (timescale == null) ? 1d : TimeSpan.TicksPerSecond / ((double)timescale);
            SetTimeStamp(TimeSpan.FromTicks((long)(_TotalDuration.Ticks * ((double)trackBarTime.Value) / 1000d)));
            donotfirechangeevent = false;
            OnNumValueChanged(EventArgs.Empty);
        }

        protected virtual void OnNumValueChanged(EventArgs e)
        {
            EventHandler handler = ValueChanged;
            if (handler != null && !donotfirechangeevent)
            {
                handler(this, e);
            }
        }

        public long? TimeScale
        {
            get => timescale;
            set => timescale = value;
        }

        public ulong ScaledFirstTimestampOffset // timestamp of first video chunk in manifest
        {
            get => scaledoffset;
            set => scaledoffset = value;
        }

        public TimeSpan TotalDuration
        {
            get => _TotalDuration;
            set => _TotalDuration = value;
        }

        public string Label1
        {
            get => label1.Text;
            set => label1.Text = value;
        }

        public string Label2
        {
            get => label2.Text;
            set => label2.Text = value;
        }

        public bool DisplayTrackBar
        {
            get => _displaytrackbar;
            set
            {
                trackBarTime.Visible = value;
                trackBarTime.Enabled = value;
                _displaytrackbar = value;
            }
        }

        public TimeSpan Min
        {
            get => min;
            set => min = value;
        }

        public TimeSpan Max
        {
            get => max;
            set => max = value;
        }


        public long ScaledTimeStampWithoutOffset
        {
            get
            {
                TimeSpan ts = TimeStampWithoutOffset;
                //double timescale2 = timescale ?? TimeSpan.TicksPerSecond;
                double timescale2 = timescale ?? TimeSpan.TicksPerSecond;
                return Convert.ToInt64(Math.Truncate(ts.Ticks * (timescale2 / TimeSpan.TicksPerSecond)));
            }
        }

        public long ScaledTimeStampWithOffset
        {
            get
            {
                TimeSpan ts = TimeStampWithOffset;
                // double timescale2 = timescale ?? TimeSpan.TicksPerSecond;
                double timescale2 = timescale ?? TimeSpan.TicksPerSecond;

                return Convert.ToInt64(Math.Truncate(ts.Ticks * (timescale2 / TimeSpan.TicksPerSecond)));
            }
        }


        public void SetScaledTimeStamp(long? value, long valueIfNull)
        {
            if (value == 0)
            {
                SetTimeStamp(new TimeSpan(0));
            }
            else
            {
                long valueToUse = value ?? valueIfNull;

                //double timescale2 = timescale ?? TimeSpan.TicksPerSecond;
                double timescale2 = timescale ?? TimeSpan.TicksPerSecond;

                double scale = TimeSpan.TicksPerSecond / (timescale2);
                TimeSpan ts = new TimeSpan(Convert.ToInt64((valueToUse - (double)ScaledFirstTimestampOffset) * scale));
                SetTimeStamp(ts);
            }
        }


        public TimeSpan TimeStampWithoutOffset => new TimeSpan((int)numericUpDownDays.Value, (int)numericUpDownHours.Value, (int)numericUpDownMinutes.Value, (int)Math.Truncate(numericUpDownSeconds.Value), (int)(1000 * (numericUpDownSeconds.Value - Math.Truncate(numericUpDownSeconds.Value))));


        public TimeSpan TimeStampWithOffset => GetOffSetAsTimeSpan() + new TimeSpan((int)numericUpDownDays.Value, (int)numericUpDownHours.Value, (int)numericUpDownMinutes.Value, (int)Math.Truncate(numericUpDownSeconds.Value), (int)(1000 * (numericUpDownSeconds.Value - Math.Truncate(numericUpDownSeconds.Value))));


        public TimeSpan GetOffSetAsTimeSpan()
        {
            //double timescale2 = timescale ?? TimeSpan.TicksPerSecond;
            double timescale2 = timescale ?? TimeSpan.TicksPerSecond;

            return new TimeSpan((long)(TimeSpan.TicksPerSecond * (double)ScaledFirstTimestampOffset / (timescale2)));
        }


        public void SetTimeStamp(TimeSpan value)
        {
            donotfirechangeevent = true;
            // trackbar update
            if (DisplayTrackBar)
            {
                double scale = (timescale == null) ? 1d : TimeSpan.TicksPerSecond / ((double)timescale);
                trackBarTime.Value = (int)(value.TotalMilliseconds / _TotalDuration.TotalMilliseconds * 1000d);
            }

            numericUpDownDays.Value = value.Days;
            numericUpDownHours.Value = value.Hours;
            numericUpDownMinutes.Value = value.Minutes;
            donotfirechangeevent = false;
            numericUpDownSeconds.Value = Convert.ToDecimal(value.Seconds + value.Milliseconds / 1000d);
        }
    }
}
