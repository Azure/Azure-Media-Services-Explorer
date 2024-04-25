using System;
using System.Windows.Forms;

namespace AMSExplorer.Forms_Login
{
    public partial class SelectEnvironment : Form
    {
        public SelectEnvironment()
        {
            InitializeComponent();
        }

        private void SelectEnvironment_Load(object sender, EventArgs e)
        {
            AzureEnvType[] envs = AzureEnvironments.GetEnvironments();

            foreach (AzureEnvType env in envs)
            {
                comboBoxAADMappingList.Items.Add(new Item((new AzureEnvironment(env)).DisplayName, env.ToString()));
            }

            comboBoxAADMappingList.SelectedIndex = 0;
        }

        public AzureEnvironment GetEnvironment()
        {
            return new AzureEnvironment((AzureEnvType)Enum.Parse(typeof(AzureEnvType), (comboBoxAADMappingList.SelectedItem as Item).Value));
        }
    }
}
