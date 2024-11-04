using DVLD_Business_Logic_Tear;
using System;

using System.Windows.Forms;

namespace DVLD_Windows_Forms.Controls
{
    public partial class ctrlApplicationBasicInfo : UserControl
    {
        public ctrlApplicationBasicInfo()
        {
            InitializeComponent();
        }

        int _PersonID = -1;
        clsApplication _Application;
        clsLocalDrivingLicence.stApplicationBasicInfoCtrl _stApplication;
        public void LoadControl(int ldlAppID)
        {
            _stApplication = clsLocalDrivingLicence.FindApplicationInfoForCtrlBasicInfo(ldlAppID);
            if (_stApplication.LocalDrivingLicenseApplicationID == -1)
                return;
            _PersonID = clsApplication.FindByID(_stApplication.ApplicationID).ApplicantPersonID;
            lblFees.Text = _stApplication.PaidFees.ToString();
            lblID.Text = _stApplication.LocalDrivingLicenseApplicationID.ToString();
            lblStatus.Text = _stApplication.ApplicationStatus.ToString();
            lblType.Text = _stApplication.ApplicationType.ToString();
            lblApplicant.Text = _stApplication.ApplicationPersonName.ToString();
            lblType.Text = _stApplication.ApplicationType;
            lblStatusDate.Text = _stApplication.LastStatusDate.ToShortDateString();
            lblDate.Text = _stApplication.ApplicationDate.ToShortDateString();
            lblCreatedBy.Text = _stApplication.CreatedByUserName;
            llPersonInfo.Visible = true;
            
            //lblNotValueApplicationID.Enabled = lblNotValueILID.Enabled = false;

        }

        public void LoadControlByLicenseID(int licenseID)
        {
            if (clsLicense.ExistsByID(licenseID))
            {
                int ldlAppID = clsLocalDrivingLicence.GetLDLAppIDByLicenseID(licenseID);
                if(ldlAppID != -1)
                {
                    LoadControl(ldlAppID);
                }   
            }
        }

        public void LoadControlByLicenseIDForInternationalDrivingLicense(int licenseID, int appID = -1,
            int ilID = -1)
        {
            if (clsLicense.ExistsByID(licenseID))
            {
                int ldlAppID = clsLocalDrivingLicence.GetLDLAppIDByLicenseID(licenseID);
                if (ldlAppID != -1)
                {
                    LoadControl(ldlAppID);
                    lblFees.Text = clsApplicationTypes.FindByID(6).ApplicationFees.ToString();
                }
                if (appID != -1)
                {
                    lblApplicationID.Text = appID.ToString();
                    
                }
                if(ilID != -1)
                {
                    lblILID.Text = ilID.ToString();
                }
            }
        }

        private void ctrlApplicationBasicInfo_Load(object sender, EventArgs e)
        {
            llPersonInfo.Visible = false;
        }

        private void llPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new frmPersonInfo(_PersonID).ShowDialog();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
