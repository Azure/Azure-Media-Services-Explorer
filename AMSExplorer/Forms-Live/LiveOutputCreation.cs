//----------------------------------------------------------------------------------------------
//    Copyright 2021 Microsoft Corporation
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


using Microsoft.Azure.Management.Media;
using Microsoft.Azure.Management.Media.Models;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AMSExplorer
{
    public partial class LiveOutputCreation : Form
    {
        public string LiveEventName;
        private readonly AMSClientV3 _client;

        public string LiveOutputName
        {
            get => textboxprogramname.Text;
            set => textboxprogramname.Text = value;
        }

        public string ProgramDescription
        {
            get => textBoxDescription.Text;
            set => textBoxDescription.Text = value;
        }

        public TimeSpan ArchiveWindowLength
        {
            get => new((int)numericUpDownArchiveHours.Value, (int)numericUpDownArchiveMinutes.Value, 0);
            set
            {
                numericUpDownArchiveHours.Value = value.Hours;
                numericUpDownArchiveMinutes.Value = value.Minutes;
            }
        }

        public string ManifestName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(textBoxManifestName.Text))
                {
                    return null;
                }
                else
                {
                    return textBoxManifestName.Text.Trim();
                }
            }
            set => textBoxManifestName.Text = value;
        }

        public short? HLSFragmentPerSegment
        {
            get => checkBoxHLSFragPerSegDefined.Checked ? (short?)numericUpDownHLSFragPerSeg.Value : null;
            set
            {
                if (value != null)
                {
                    numericUpDownHLSFragPerSeg.Value = (short)value;
                }
            }
        }

        public string AssetName
        {
            get => textBoxAssetName.Text;
            set => textBoxAssetName.Text = value;
        }

        public long? StartRecordTimestamp
        {
            get
            {
                if (string.IsNullOrWhiteSpace(textBoxStartRecordTimestamp.Text))
                {
                    return null;
                }
                else
                {
                    long? val = null;
                    try
                    {
                        val = long.Parse(textBoxStartRecordTimestamp.Text);
                    }
                    catch
                    {

                    }
                    return val;
                }
            }

            set => textBoxStartRecordTimestamp.Text = value.ToString();
        }

        public bool CreateLocator
        {
            get => checkBoxCreateLocator.Checked;
            set => checkBoxCreateLocator.Checked = value;
        }

        public string StorageSelected => ((Item)comboBoxStorage.SelectedItem).Value;

        public int MaxArchiveHours
        {
            set
            {
                labelExplainArch.Text = string.Format(labelExplainArch.Text, value);
                numericUpDownArchiveHours.Maximum = value;
            }
        }

        public LiveOutputCreation(AMSClientV3 client)
        {
            InitializeComponent();
            Icon = Bitmaps.Azure_Explorer_ico;
            _client = client;
        }

        private async void LiveOutputCreation_Load(object sender, EventArgs e)
        {
            // DpiUtils.InitPerMonitorDpi(this);

            Text = string.Format(Text, LiveEventName);
            //checkBoxCreateLocator.Text = string.Format(checkBoxCreateLocator.Text, Properties.Settings.Default.DefaultLocatorDurationDaysNew);


            System.Collections.Generic.IList<StorageAccount> storages = (await _client.AMSclient.Mediaservices.GetAsync(_client.credentialsEntry.ResourceGroup, _client.credentialsEntry.AccountName)).StorageAccounts;
            foreach (StorageAccount storage in storages)
            {
                bool primary = (storage.Type == StorageAccountType.Primary);

                comboBoxStorage.Items.Add(new Item(AMSClientV3.GetStorageName(storage.Id) + (primary ? " (primary)" : string.Empty), AMSClientV3.GetStorageName(storage.Id)));
                if (primary)
                {
                    comboBoxStorage.SelectedIndex = comboBoxStorage.Items.Count - 1;
                }
            }

            checkLiveOutputName();
        }

        internal static bool IsLiveOutputNameValid(string name)
        {
            Regex reg = new(@"^([a-zA-Z0-9])+(-*[a-zA-Z0-9])*$", RegexOptions.Compiled);
            return (name.Length > 0 && name.Length < 257 && reg.IsMatch(name));
        }

        private void checkLiveOutputName()
        {
            TextBox tb = textboxprogramname;

            if (!IsLiveOutputNameValid(tb.Text))
            {
                errorProvider1.SetError(tb, AMSExplorer.Properties.Resources.CreateProgram_checkProgramName_ProgramNameIsNotValid);
            }
            else
            {
                errorProvider1.SetError(tb, string.Empty);
            }
        }


        private void checkStartRecordingTimestamp()
        {
            TextBox tb = textBoxStartRecordTimestamp;

            try
            {
                if (!string.IsNullOrWhiteSpace(tb.Text))
                {
                    long.Parse(tb.Text);
                }

                errorProvider1.SetError(tb, string.Empty);
            }
            catch
            {
                errorProvider1.SetError(tb, "Error. Timestamp should be a long value");
            }
        }


        private void textboxprogramname_TextChanged(object sender, EventArgs e)
        {
            checkLiveOutputName();
        }

        private void checkBoxHLSFragPerSegDefined_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownHLSFragPerSeg.Enabled = checkBoxHLSFragPerSegDefined.Checked;
        }

        private void textBoxStartRecordTimestamp_TextChanged(object sender, EventArgs e)
        {
            checkStartRecordingTimestamp();
        }

        private void LiveOutputCreation_Shown(object sender, EventArgs e)
        {
            Telemetry.TrackPageView(this.Name);
        }
    }
}