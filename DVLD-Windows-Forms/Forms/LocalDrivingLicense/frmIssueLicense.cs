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

namespace DVLD_Windows_Forms.Forms.LocalDrivingLicense.Test
{
    public partial class frmIssueLicense : Form
    {
        int _LDLAppID { get; set; }
        int _LicenceClassID { get; set; }
        public frmIssueLicense(int ldlAppID, int LicenceClassID)
        {
            InitializeComponent();
            _LDLAppID = ldlAppID;
            _LicenceClassID = LicenceClassID;
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            clsLocalDrivingLicence localDrivingLicence = clsLocalDrivingLicence.FindByID(_LDLAppID);
            int personID = clsPerson.GetPersonIDByLocalDrivingLicense(_LDLAppID);
            int userID = clsCurrentUser.CurrentUser.UserID;
            int driverID = -1;
            if(!clsDriver.IsDriverExistsByPersonID(personID))
            {
                driverID = clsDriver.AddNewDriver(personID, userID, DateTime.Now);
                if (driverID == -1)
                {
                    MessageBox.Show($"Failed To Isseue License", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            if(driverID == -1)
                driverID = clsDriver.FindByPersonID(personID).DriverID;
            int licenseID = clsLicense.AddNewLicense(localDrivingLicence.ApplicationID, driverID,
                _LicenceClassID, DateTime.Now, DateTime.Now.AddYears(10), tbNotes.Text, 0, true, 1,
                userID);
            if (licenseID != -1)
            {
                clsApplication application = clsApplication.FindByID(localDrivingLicence.ApplicationID);
                application.ApplicationStatus = 3;
                application.Mode = clsApplication.enMode.Update;
                if (application.Save())
                {
                    MessageBox.Show($"License Issued Successfully With ID: {licenseID}", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"Failed To Isseue License", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void frmIssueLicense_Load(object sender, EventArgs e)
        {
            ctrlApplicationBasicInfo1.LoadControl(_LDLAppID);
            ctrlDrivingLicenseApplicationInfo1.LoadControl(_LDLAppID);
        }
    }
}
