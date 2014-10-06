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
using Microsoft.WindowsAzure.MediaServices.Client.ContentKeyAuthorization;
using Microsoft.WindowsAzure.MediaServices.Client.DynamicEncryption;

namespace AMSExplorer
{
    public partial class AddDynamicEncryption : Form
    {
        public ContentKeyRestrictionType GetKeyRestrictionType
        {
            get
            {
                if (radioButtonOpen.Checked)
                {
                    return ContentKeyRestrictionType.Open;
                }
                else
                {
                    return ContentKeyRestrictionType.TokenRestricted;
                }
            }
        }

        public AssetDeliveryPolicyType GetDeliveryPolicyType
        {
            get
            {
                if (radioButtonEnvelope.Checked)
                {
                    return AssetDeliveryPolicyType.DynamicEnvelopeEncryption;
                }
                else if (radioButtonCENCKey.Checked)
                {
                    return AssetDeliveryPolicyType.DynamicCommonEncryption;
                }
                else
                {
                    return AssetDeliveryPolicyType.NoDynamicEncryption;
                }
            }
        }

        public ContentKeyType GetContentKeyType
        {
            get
            {
                return radioButtonEnvelope.Checked ? ContentKeyType.EnvelopeEncryption : ContentKeyType.CommonEncryption;
            }
        }

        public bool ForceDeliveryPolicyCreation
        {
            get
            {
                return (checkBoxForceCreateDelPol.Checked);
            }
        }



        public bool ForceContentKeyCreation
        {
            get
            {
                return (checkBoxForceCreateContentKey.Checked);
            }
        }


        public AssetDeliveryProtocol GetAssetDeliveryProtocol
        {
            get
            {
                return ((checkBoxProtocolDASH.Checked ? AssetDeliveryProtocol.Dash : AssetDeliveryProtocol.None) | (checkBoxProtocolHLS.Checked ? AssetDeliveryProtocol.HLS : AssetDeliveryProtocol.None) | (checkBoxProtocolSmooth.Checked ? AssetDeliveryProtocol.SmoothStreaming : AssetDeliveryProtocol.None));
            }
        }



        public Uri GetAudienceUri
        {
            get
            {
                return new Uri(textBoxAudience.Text);
            }
        }
        public Uri GetIssuerUri
        {
            get
            {
                return new Uri(textBoxIssuer.Text);
            }
        }

        private CloudMediaContext _context;

        public AddDynamicEncryption(CloudMediaContext context)
        {
            InitializeComponent();
            _context = context;

        }



        private void SetupDynEnc_Load(object sender, EventArgs e)
        {

        }



        private void radioButtonToken_CheckedChanged(object sender, EventArgs e)
        {
            panelAutPol.Enabled = radioButtonToken.Checked;

        }



        private void radioButtonEnvelope_CheckedChanged(object sender, EventArgs e)
        {

        }




        private void buttonOk_Click(object sender, EventArgs e)
        {

        }

        private void radioButtonOpen_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxForceCreateDelPol_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButtonDecryptStorage_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxAuthPol.Enabled = !radioButtonDecryptStorage.Checked;
            checkBoxForceCreateContentKey.Enabled = !radioButtonDecryptStorage.Checked;
        }

    }
}
