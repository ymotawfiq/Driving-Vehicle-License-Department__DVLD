using DVLD_Business_Logic_Tear;
using DVLD_Windows_Forms.Forms.InternationalDrivingLicense;
using DVLD_Windows_Forms.Forms.LocalDrivingLicense;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Windows_Forms.Forms.DetainLicenses
{
    public partial class frmReleaseDetainedLicense : Form
    {

        clsLicense _License = new clsLicense();
        int _PersonID = -1;
        int _DetainedLicenseID = -1;
        public frmReleaseDetainedLicense(int dlID=-1)
        {
            InitializeComponent();
            _DetainedLicenseID = dlID;
        }

        private void frmReleaseDetainedLicense_Load(object sender, EventArgs e)
        {
            if (_DetainedLicenseID != -1)
            {
                if (clsDetainedLicenses.ExistsByID(_DetainedLicenseID))
                {
                    clsDetainedLicenses detainedLicense = clsDetainedLicenses.FindByID(_DetainedLicenseID);
                    tbSearch.Text = detainedLicense.LicenseID.ToString();
                    gbFilter.Enabled = false;
                    ctrlDriverLicenseInfo1.LoadControlByLicenseID(detainedLicense.LicenseID);
                    _FillDetainInfo();
                    btnRelease.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Detained License Not Found", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                btnRelease.Enabled = llLicenseInfo.Enabled = llLicenseHistory.Enabled = false;
                btnSearch.Enabled = false;
            }
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tbSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar);
        }

        private void _FillDetainInfo()
        {
            lblCreatedBy.Text = clsCurrentUser.CurrentUser.UserName;
            lblDetainDate.Text = DateTime.Now.ToShortDateString();
            lblLicenseID.Text = _License.LicenseID.ToString();

            if (clsDetainedLicenses.ExistsByLicenseID(_License.LicenseID))
            {
                lblFineFees.Text = clsDetainedLicenses.FindByLicenseID(_License.LicenseID)
                    .FineFees.ToString();
                lblDetainID.Text = clsDetainedLicenses.FindByLicenseID(_License.LicenseID)
                    .DetainID.ToString();
                btnRelease.Enabled = true;
                lblApplicationFees.Text = clsApplicationTypes.FindByID(5).ApplicationFees.ToString();
            }
            else
            {
                btnRelease.Enabled = false;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (clsLicense.ExistsByID(Convert.ToInt32(tbSearch.Text)))
            {
                _License = clsLicense.FindByID(Convert.ToInt32(tbSearch.Text));
                _PersonID = clsPerson.GetPersonIDByLicenseID(_License.LicenseID);
                _FillDetainInfo();
                ctrlDriverLicenseInfo1.LoadControlByLicenseID(_License.LicenseID);
                llLicenseHistory.Enabled = llLicenseInfo.Enabled = true;
                if (!clsDetainedLicenses.ExistsByLicenseID(_License.LicenseID))
                {
                    MessageBox.Show("License is not Detained", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                    btnRelease.Enabled = false;
                }
                if (!_License.IsActive)
                {
                    MessageBox.Show("License is not Active", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                    btnRelease.Enabled = false;
                }
            }
            else
            {
                MessageBox.Show("License Not Found", "Error", MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Error);
                btnRelease.Enabled = llLicenseHistory.Enabled = llLicenseInfo.Enabled = false;
            }
        }

        private void llLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new frmDriverLicensesHistory(_PersonID).ShowDialog();
        }

        private void llLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new frmShowInternationalLicense(_License.LicenseID).ShowDialog();
        }

        private int _CreateApplicationToReleaseLicense()
        {
            clsApplication application = new clsApplication();
            application.ApplicantPersonID = _PersonID;
            application.ApplicationDate = DateTime.Now;
            application.ApplicationStatus = 3;
            application.CreatedByUserID = clsCurrentUser.CurrentUser.UserID;
            application.LastStatusDate = DateTime.Now;
            application.PaidFees = clsApplicationTypes.FindByID(2).ApplicationFees;
            application.Mode = clsApplication.enMode.AddNew;
            application.ApplicationTypeID = 5;

            application.Save();
            return application.ApplicationID;
        }

        private bool _ReleaseDetainedLicense(int appID)
        {
            try
            {
                clsDetainedLicenses detainedLicense = clsDetainedLicenses.FindByLicenseID(_License.LicenseID);
                detainedLicense.IsReleased = true;
                detainedLicense.ReleaseApplicationID = appID;
                detainedLicense.ReleaseDate = DateTime.Now;
                detainedLicense.ReleasedByUserID = clsCurrentUser.CurrentUser.UserID;
                detainedLicense.Mode = clsDetainedLicenses.enMode.Update;
                return detainedLicense.Save();
            }
            catch(Exception ex)
            {
                clsApplication.DeleteApplication(appID);
                throw new Exception(ex.Message);
            }
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to Release License?", "Release License",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                int appID = _CreateApplicationToReleaseLicense();
                if (appID != -1)
                {
                    if (_ReleaseDetainedLicense(appID))
                    {
                        MessageBox.Show("License Released Successfully", "Release License",
                        MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                        lblApplicationID.Text = appID.ToString();
                        btnRelease.Enabled = false;
                    }
                }
                else
                {
                    MessageBox.Show("Failed to create Application to Release License", "Release License",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            if (tbSearch.Text == "") btnSearch.Enabled = false;
            else btnSearch.Enabled = true;
        }
    }
}
