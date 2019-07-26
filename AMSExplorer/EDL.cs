﻿//----------------------------------------------------------------------------------------------
//    Copyright 2019 Microsoft Corporation
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
using System.Linq;
using System.Windows.Forms;

namespace AMSExplorer
{
    public partial class EDL : Form
    {
        private BindingList<ExplorerEDLEntryInOut> TimeCodeList = new BindingList<ExplorerEDLEntryInOut>();

        public delegate void ChangedEventHandler(object sender, EventArgs e);

        // An event that clients can use to be notified whenever the
        // elements of the list change.
        public event ChangedEventHandler Changed;

        protected virtual void OnChanged(EventArgs e)
        {
            if (Changed != null)
            {
                Changed(this, e);
            }
        }

        public EDL()
        {
            InitializeComponent();
            Icon = Bitmaps.Azure_Explorer_ico;
        }

        public void EDL_Load(object sender, EventArgs e)
        {
            dataGridViewEDL.DataSource = TimeCodeList;

        }


        public void AddEDLEntry(ExplorerEDLEntryInOut entry)
        {
            TimeCodeList.Add(entry);
            OnChanged(EventArgs.Empty);
        }

        public List<ExplorerEDLEntryInOut> GetEDLEntries()
        {

            return TimeCodeList.ToList();
        }

        public void SetEDLEntries(List<ExplorerEDLEntryInOut> list)
        {

            TimeCodeList = new BindingList<ExplorerEDLEntryInOut>(list);

        }

        private void buttonUp_Click(object sender, EventArgs e)
        {
            if (dataGridViewEDL.SelectedRows.Count == 1 && dataGridViewEDL.SelectedRows[0].Index > 0)
            {
                int index = dataGridViewEDL.SelectedRows[0].Index;
                ExplorerEDLEntryInOut backup = TimeCodeList[index - 1];
                TimeCodeList[index - 1] = TimeCodeList[index];
                TimeCodeList[index] = backup;
                dataGridViewEDL.ClearSelection();
                dataGridViewEDL.Rows[index - 1].Selected = true;
                OnChanged(EventArgs.Empty);
            }
        }

        private void buttonDown_Click(object sender, EventArgs e)
        {
            if (dataGridViewEDL.SelectedRows.Count == 1 && dataGridViewEDL.SelectedRows[0].Index < TimeCodeList.Count - 1)
            {
                int index = dataGridViewEDL.SelectedRows[0].Index;
                ExplorerEDLEntryInOut backup = TimeCodeList[index + 1];
                TimeCodeList[index + 1] = TimeCodeList[index];
                TimeCodeList[index] = backup;
                dataGridViewEDL.ClearSelection();
                dataGridViewEDL.Rows[index + 1].Selected = true;
                OnChanged(EventArgs.Empty);
            }
        }

        private void buttonDelIP_Click(object sender, EventArgs e)
        {
            if (dataGridViewEDL.SelectedRows.Count == 1)
            {
                TimeCodeList.RemoveAt(dataGridViewEDL.SelectedRows[0].Index);
                OnChanged(EventArgs.Empty);
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void dataGridViewEDL_SelectionChanged(object sender, EventArgs e)
        {
            buttonDelEntry.Enabled = dataGridViewEDL.SelectedRows.Count > 0;
            buttonUp.Enabled = dataGridViewEDL.SelectedRows.Count > 0 && dataGridViewEDL.SelectedRows[0].Index > 0;
            buttonDown.Enabled = dataGridViewEDL.SelectedRows.Count > 0 && dataGridViewEDL.SelectedRows[0].Index < dataGridViewEDL.Rows.Count - 1;

        }

        private void EDL_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.UserClosing)
            {
                return;
            }

            e.Cancel = true;
            Hide();
        }
    }

    internal class ButtonEDL : Button
    {
        private EDL myEDL;

        //      public delegate void ChangedEventHandler(object sender, EventArgs e);

        //    public event ChangedEventHandler Changed;

        // Invoke the Changed event; called whenever list changes


        public event EDL.ChangedEventHandler EDLChanged
        {
            add { myEDL.Changed += new EDL.ChangedEventHandler(value); }
            remove { myEDL.Changed -= new EDL.ChangedEventHandler(value); }
        }



        public ButtonEDL()
        {
            Click += ButtonEDL_Click;

        }



        public void Initialize()
        {
            myEDL = new EDL();
        }

        public void ButtonEDL_Click(object sender, EventArgs e)
        {
            myEDL.Show();
        }

        public void AddEDLEntry(ExplorerEDLEntryInOut entry)
        {
            myEDL.AddEDLEntry(entry);
        }

        public List<ExplorerEDLEntryInOut> GetEDLEntries()
        {
            return myEDL.GetEDLEntries();
        }

        public void SetEDLEntries(List<ExplorerEDLEntryInOut> list)
        {
            myEDL.SetEDLEntries(list);
        }

        public TimeSpan Offset { get; set; }

    }

    public class ExplorerEDLEntryInOut
    {
        public TimeSpan? Start { get; set; }
        public TimeSpan? End { get; set; }

        public TimeSpan? Offset { get; set; }

        public TimeSpan? Duration
        {
            get
            {
                if (End != null)
                {
                    return End - Start;
                }
                else
                {
                    return null;
                }
            }
        }

        public string AssetID { get; set; }
    }

}
