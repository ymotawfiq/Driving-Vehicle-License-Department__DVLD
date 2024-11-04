using DVLD_Business_Logic_Tear;
using DVLD_Windows_Forms.Forms.InternationalDrivingLicense;
using DVLD_Windows_Forms.Forms.LocalDrivingLicense;
using System;

using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_Windows_Forms.Forms.LicenseReplacement
{
    public partial class frmReplaceMentForDamagedOrLost : Form
    {
        int _PersonID = -1;
        clsLicense _License = new clsLicense();
        public frmReplaceMentForDamagedOrLost()
        {
            InitializeComponent();
        }

        private void frmReplaceMentForDamagedOrLost_Load(object sender, EventArgs e)
        {
            btnIsseue.Enabled = false;
            llLicenseHistory.Enabled = false;
            llLicenseInfo.Enabled = false;
            rbDamaged.Checked = true;
            btnSearch.Enabled = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
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
                _PersonID = clsPerson.GetPersonIDByLicenseID(_License.LicenseID);
                ctrlDriverLicenseInfo1.LoadControlByLicenseID(Convert.ToInt32(tbSearch.Text));
                ctrlLicenseReplacement1.LoadControlByLicenseID(Convert.ToInt32(tbSearch.Text), 3);
                if (_License.IsActive)
                {
                    btnIsseue.Enabled = true;
                    llLicenseHistory.Enabled = true;
                    llLicenseInfo.Enabled = true;
                    //gbFilter.Enabled = false;
                }
                else
                {
                    MessageBox.Show("License is not Active", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("License Not Found", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void llLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new frmShowInternationalLicense(_License.LicenseID).ShowDialog();
        }

        private void llLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new frmDriverLicensesHistory(_PersonID).ShowDialog();
        }



        private void _DeactivateOldLicense()
        {
            try
            {
                _License.IsActive = false;
                _License.Mode = clsLicense.enMode.Update;
                _License.Save();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void _ActivateOldLicense()
        {
            try
            {
                _License.IsActive = true;
                _License.Mode = clsLicense.enMode.Update;
                _License.Save();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private int _CreateApplicationForLicense()
        {
            _DeactivateOldLicense();
            clsApplication application = new clsApplication();
            application.ApplicantPersonID = _PersonID;
            application.ApplicationDate = DateTime.Now;
            application.ApplicationStatus = 3;
            application.CreatedByUserID = clsCurrentUser.CurrentUser.UserID;
            application.LastStatusDate = DateTime.Now;
            application.PaidFees = clsApplicationTypes.FindByID(2).ApplicationFees;
            application.Mode = clsApplication.enMode.AddNew;

            if (rbDamaged.Checked)
                application.ApplicationTypeID = 4;
            else
                application.ApplicationTypeID = 3;

            application.Save();
            return application.ApplicationID;
        }

        private int _CreateLicense(int appID)
        {
            clsLicense license = new clsLicense();
            license.ApplicationID = appID;
            license.LicenseClass = _License.LicenseClass;
            
            license.CreatedByUserID = clsCurrentUser.CurrentUser.UserID;
            license.DriverID = _License.DriverID;
            license.ExpirationDate = DateTime.Now.AddYears(10);
            license.IsActive = true;
            license.IssueDate = DateTime.Now;
            
            if (rbDamaged.Checked)
            {
                license.IssueReason = 4;
                license.Notes = "Damaged";
                license.PaidFees = clsApplicationTypes.FindByID(4).ApplicationFees;
            }
            else
            {
                license.IssueReason = 3;
                license.PaidFees = clsApplicationTypes.FindByID(3).ApplicationFees;
                license.Notes = "Lost";
            }

            license.Mode = clsLicense.enMode.AddNew;
            license.Save();
            return license.LicenseID;
        }

        private void btnIsseue_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to Replace License?", "Renew License",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                int appID = _CreateApplicationForLicense();
                if (appID != -1)
                {
                    int licenseID = -1;
                    try
                    {
                        licenseID = _CreateLicense(appID);
                    }
                    catch (Exception ex)
                    {
                        clsApplication.DeleteApplication(appID);
                        _ActivateOldLicense();
                        throw new Exception(ex.Message);
                    }
                    if (licenseID != -1)
                    {
                        MessageBox.Show($"License Created Successfully with ID {licenseID}",
                            "Renew License",
                            MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                        gbFilter.Enabled = false;
                        ctrlLicenseReplacement1.LoadControlByLicenseID(
                            _License.LicenseID, licenseID);
                        btnIsseue.Enabled = false;
                        _License = clsLicense.FindByID(licenseID);
                        llLicenseInfo.Enabled = true;
                    }
                }
            }
            else
            {
                MessageBox.Show($"Caneled Successfully",
                            "Canceled",
                            MessageBoxButtons.OKCancel, MessageBoxIcon.None);
            }
        }

        private void rbDamaged_CheckedChanged(object sender, EventArgs e)
        {
            ctrlLicenseReplacement1.LoadControlByLicenseID(_License.LicenseID, 3);
        }

        private void rbLost_CheckedChanged(object sender, EventArgs e)
        {
            ctrlLicenseReplacement1.LoadControlByLicenseID(_License.LicenseID, 4);
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            if (tbSearch.Text == "") btnSearch.Enabled = false;
            else btnSearch.Enabled = true;
        }
    }
}
