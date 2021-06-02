using System;
using System.Windows.Forms;

namespace AMSExplorer
{
    public partial class InputBox : Form
    {
        public string InputValue => textBoxInput.Text;
        public InputBox(string title, string promptText, string inputValue = null, bool passwordWildcard = false)
        {
            InitializeComponent();
            Icon = Bitmaps.Azure_Explorer_ico;

            Text = title;
            labelTitle.Text = promptText;
            textBoxInput.UseSystemPasswordChar = passwordWildcard;
            textBoxInput.Text = inputValue;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void InputBox_Load(object sender, EventArgs e)
        {
            // DpiUtils.InitPerMonitorDpi(this);
        }

        private void InputBox_DpiChanged(object sender, DpiChangedEventArgs e)
        {
            // DpiUtils.UpdatedSizeFontAfterDPIChange(labelTitle, e);
        }

        private void InputBox_Shown(object sender, EventArgs e)
        {
            Telemetry.TrackPageView(this.Name);
        }
    }
}
