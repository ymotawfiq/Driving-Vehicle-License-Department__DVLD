using DVLD_Business_Logic_Tear;
using DVLD_Windows_Forms.Forms.LocalDrivingLicense;
using System;
using System.Windows.Forms;

namespace DVLD_Windows_Forms.Forms.InternationalDrivingLicense
{
    public partial class frmInternationalDrivingLicense : Form
    {
        int _PersonID = -1;
        int _ILicenseID = -1;
        clsLicense _License = new clsLicense();
        public frmInternationalDrivingLicense()
        {
            InitializeComponent();
        }

        private void tbSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (clsLicense.ExistsByID(Convert.ToInt32(tbSearch.Text)))
            {
                _License = clsLicense.FindByID(Convert.ToInt32(tbSearch.Text));
                ctrlDriverLicenseInfo1.LoadControlByLicenseID(Convert.ToInt32(tbSearch.Text));
                ctrlApplicationBasicInfo1.LoadControlByLicenseIDForInternationalDrivingLicense(
                    Convert.ToInt32(tbSearch.Text));
                btnIsseue.Enabled = true;
                llLicenseHistory.Enabled = true;
            }
            else
            {
                MessageBox.Show("License Not Found", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmInternationalDrivingLicense_Load(object sender, EventArgs e)
        {
            btnIsseue.Enabled = false;
            llLicenseHistory.Enabled = llLicenseInfo.Enabled = false;
        }

        private void lbLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new frmDriverLicensesHistory(_PersonID).ShowDialog();
        }

        private void ctrlDriverLicenseInfo1_OnPersonIDChanged(int obj)
        {
            _PersonID = obj;
        }

        private int _CreateApplicationForLicense()
        {
            clsApplication application = new clsApplication();
            application.ApplicantPersonID = _PersonID;
            application.ApplicationDate = DateTime.Now;
            application.ApplicationStatus = 3;
            application.ApplicationTypeID = 6;
            application.CreatedByUserID = clsCurrentUser.CurrentUser.UserID;
            application.LastStatusDate = DateTime.Now;
            application.PaidFees = clsApplicationTypes.FindByID(6).ApplicationFees;
            application.Mode = clsApplication.enMode.AddNew;
            application.Save();
            return application.ApplicationID;
        }

        private int _CreateLicense(int appID)
        {
            clsInternationalDrivingLicense license = new clsInternationalDrivingLicense();
            license.IssuedUsingLocalLicenseID = _License.LicenseID;
            license.ApplicationID = appID;
            license.CreatedByUserID = clsCurrentUser.CurrentUser.UserID;
            license.DriverID = _License.DriverID;
            license.ExpirationDate = DateTime.Now.AddYears(10);
            license.IsActive = true;
            license.IssueDate = DateTime.Now;
            license.Mode = clsInternationalDrivingLicense.enMode.AddNew;
            license.Save();
            return license.InternationalLicenseID;
        }

        private void _HandleExistingILicense()
        {
            clsInternationalDrivingLicense ILicense = clsInternationalDrivingLicense
                        .FindByLicenseID(_License.LicenseID);
            _ILicenseID = ILicense.InternationalLicenseID;
            llLicenseInfo.Enabled = true;
            llLicenseHistory.Enabled = true;
            gbFilter.Enabled = false;
            btnIsseue.Enabled = false;
            MessageBox.Show("You already has international License", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            ctrlApplicationBasicInfo1.LoadControlByLicenseIDForInternationalDrivingLicense(
                    _License.LicenseID, ILicense.ApplicationID, ILicense.InternationalLicenseID);
            ctrlDriverLicenseInfo1.LoadControlByLicenseID(_License.LicenseID, _ILicenseID);
        }

        private void btnIsseue_Click(object sender, EventArgs e)
        {
            if(_License.LicenseID != -1)
            {
                if (!clsInternationalDrivingLicense.ExistsByLicenseID(_License.LicenseID))
                {
                    int appID = _CreateApplicationForLicense();
                    if (appID != -1)
                    {
                        int iLicenseID = -1;
                        try
                        {
                            iLicenseID = _CreateLicense(appID);
                        }
                        catch (Exception ex)
                        {
                            clsApplication.DeleteApplication(appID);
                            throw new Exception(ex.Message);
                        }
                        if (iLicenseID != -1)
                        {
                            MessageBox.Show($"License Created With ID: {iLicenseID}", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btnIsseue.Enabled = false;
                            llLicenseInfo.Enabled = true;
                            gbFilter.Enabled = false;
                            ctrlApplicationBasicInfo1.LoadControlByLicenseIDForInternationalDrivingLicense(
                                _License.LicenseID, appID, iLicenseID);
                        }
                        else
                        {
                            MessageBox.Show("Failed To Create License", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Failed To Create Application To Create License", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    _HandleExistingILicense();
                }
            }
        }

        private void ctrlApplicationBasicInfo1_Load(object sender, EventArgs e)
        {

        }

        private void llLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new frmShowInternationalLicense(_License.LicenseID).ShowDialog();
        }
    }
}
