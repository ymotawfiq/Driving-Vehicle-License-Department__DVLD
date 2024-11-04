using DVLD_Business_Logic_Tear;
using System;

using System.Windows.Forms;

namespace DVLD_Windows_Forms.Controls
{
    public partial class ctrlDrivingLicenseApplicationInfo : UserControl
    {
        public ctrlDrivingLicenseApplicationInfo()
        {
            InitializeComponent();
        }

        private void ctrlDrivingLicenseApplicationInfo_Load(object sender, EventArgs e)
        {

        }

        public void LoadControl(int ldlAppID)
        {
            clsLocalDrivingLicence.stDrivingLicenseCtrlInfo drivingLicenseCtrlInfo = 
                clsLocalDrivingLicence.GetDataForLocalDrivingLicenseInfoControl(ldlAppID);
            if(drivingLicenseCtrlInfo.LocalDrivingLicenseAppID != -1)
            {
                lblldlAppID.Text = drivingLicenseCtrlInfo.LocalDrivingLicenseAppID.ToString();
                lblLicenseClassName.Text = drivingLicenseCtrlInfo.ClassName;
                lblPassedTests.Text = clsLocalDrivingLicence
                    .GetCountOfPassedTestsByLocalDrivingLicense(ldlAppID).ToString();
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
