using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Windows_Forms.Forms.LocalDrivingLicense
{
    public partial class frmShowLicense : Form
    {
        public frmShowLicense()
        {
            InitializeComponent();
        }

        public void LodaByLicenseID(int licenseID)
        {
            ctrlDriverLicenseInfo1.LoadControlByLicenseID(licenseID);
        }

        public void LodaByLDLicenseID(int ldlAppID)
        {
            ctrlDriverLicenseInfo1.LoadControlByLDLAppID(ldlAppID);
        }

        private void frmShowLicense_Load(object sender, EventArgs e)
        {
            //ctrlDriverLicenseInfo1.LoadControlByLDLAppID(_LDLAppID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
