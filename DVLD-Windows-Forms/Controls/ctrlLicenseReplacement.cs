using DVLD_Business_Logic_Tear;
using System;

using System.Windows.Forms;

namespace DVLD_Windows_Forms
{
    public partial class ctrlLicenseReplacement : UserControl
    {
        public ctrlLicenseReplacement()
        {
            InitializeComponent();
        }

        clsApplication _Application;
        clsLocalDrivingLicence.stApplicationBasicInfoCtrl _stApplication;
        public void LoadControl(int ldlAppID)
        {
            _stApplication = clsLocalDrivingLicence.FindApplicationInfoForCtrlBasicInfo(ldlAppID);
            if (_stApplication.LocalDrivingLicenseApplicationID == -1)
                return;
            llPersonInfo.Visible = true;
            
            double licenseFees = clsLicenceClasses.FindLicenceClassByID(
                clsLocalDrivingLicence.FindByID(ldlAppID).LicenseClassID).ClassFees;
            lblLicenseFees.Text = licenseFees.ToString();
        }

        public void LoadControlByLicenseID(int licenseID, int type=4, int newLicenseID = -1)
        {
            if (clsLicense.ExistsByID(licenseID))
            {
                int ldlAppID = clsLocalDrivingLicence.GetLDLAppIDByLicenseID(licenseID);
                if (ldlAppID != -1)
                {
                    LoadControl(ldlAppID);
                }
                lblOldLicenseID.Text = licenseID.ToString();
                if (newLicenseID != -1) lblNewLicenseID.Text = newLicenseID.ToString();
                if (type == 3) label.Text = clsApplicationTypes.FindByID(3).ApplicationFees.ToString();
                if (type == 4) label.Text = clsApplicationTypes.FindByID(4).ApplicationFees.ToString();
                lblUserName.Text = clsCurrentUser.CurrentUser.UserID.ToString();
            }
        }

        private void gbLicenseReplacement_Enter(object sender, EventArgs e)
        {

        }
    }
}
