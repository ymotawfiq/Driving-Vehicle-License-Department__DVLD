using DVLD_Business_Logic_Tear;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Windows_Forms.Forms.InternationalDrivingLicense
{
    public partial class frmShowInternationalLicense : Form
    {
        int _LicenseID = -1;
        int _ILicenseID = -1;
        public frmShowInternationalLicense(int licenseID)
        {
            InitializeComponent();
            _LicenseID = licenseID;
            if (clsInternationalDrivingLicense.ExistsByLicenseID(licenseID))
            {
                _ILicenseID = clsInternationalDrivingLicense
                    .FindByLicenseID(licenseID).InternationalLicenseID;
            }
            
            ctrlDriverLicenseInfo1.LoadControlByLicenseID(_LicenseID, _ILicenseID);
        }

        private void frmShowInternationalLicense_Load(object sender, EventArgs e)
        {
            //ctrlDriverLicenseInfo1.LoadControlByLicenseID(_LicenseID, _ILicenseID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
