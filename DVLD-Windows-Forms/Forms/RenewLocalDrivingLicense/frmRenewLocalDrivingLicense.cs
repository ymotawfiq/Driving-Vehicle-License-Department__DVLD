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

namespace DVLD_Windows_Forms.Forms.RenewLocalDrivingLicense
{
    public partial class frmRenewLocalDrivingLicense : Form
    {
        int _PersonID = -1;
        clsLicense _License = new clsLicense();
        string _Notes = "";
        public frmRenewLocalDrivingLicense()
        {
            InitializeComponent();
        }

        private void frmRenewLocalDrivingLicense_Load(object sender, EventArgs e)
        {
            btnIsseue.Enabled = false;
            llLicenseHistory.Enabled = llLicenseInfo.Enabled = false;
            btnSearch.Enabled = false;
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            if (tbSearch.Text == "") btnSearch.Enabled = false;
            else btnSearch.Enabled = true;
        }

        private void llLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new frmShowInternationalLicense(_License.LicenseID).ShowDialog();
        }

        private void llLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new frmDriverLicensesHistory(_PersonID).ShowDialog();
        }

        private void ctrlDriverLicenseInfo1_OnPersonIDChanged(int obj)
        {
            _PersonID = obj;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (clsLicense.ExistsByID(Convert.ToInt32(tbSearch.Text)))
            {
                _License = clsLicense.FindByID(Convert.ToInt32(tbSearch.Text));
                ctrlDriverLicenseInfo1.LoadControlByLicenseID(Convert.ToInt32(tbSearch.Text));
                ctrlRenewLicense1.LoadControlByLicenseIDForRenewDrivingLicense(
                    Convert.ToInt32(tbSearch.Text));
                _License = clsLicense.FindByID(Convert.ToInt32(tbSearch.Text));
                btnIsseue.Enabled = true;
                llLicenseHistory.Enabled = true;
                if(_License.ExpirationDate > DateTime.Now)
                {
                    MessageBox.Show("License is still active you can't renew it", 
                        "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                    btnIsseue.Enabled = false;
                    llLicenseInfo.Enabled = true;
                    gbFilter.Enabled = false;
                }
                else
                {
                    btnIsseue.Enabled = true;
                }
            }
            else
            {
                MessageBox.Show("License Not Found", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void tbSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
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
            application.ApplicationTypeID = 2;
            application.CreatedByUserID = clsCurrentUser.CurrentUser.UserID;
            application.LastStatusDate = DateTime.Now;
            application.PaidFees = clsApplicationTypes.FindByID(2).ApplicationFees;
            application.Mode = clsApplication.enMode.AddNew;
            application.Save();
            return application.ApplicationID;
        }

        private int _CreateLicense(int appID)
        {
            clsLicense license = new clsLicense();
            license.ApplicationID = appID;
            license.LicenseClass = _License.LicenseClass;
            license.PaidFees = _License.PaidFees;
            license.CreatedByUserID = clsCurrentUser.CurrentUser.UserID;
            license.DriverID = _License.DriverID;
            license.ExpirationDate = DateTime.Now.AddYears(10);
            license.IsActive = true;
            license.IssueDate = DateTime.Now;
            license.Notes = _Notes;
            license.IssueReason = 2;
            license.Mode = clsLicense.enMode.AddNew;
            license.Save();
            return license.LicenseID;
        }

        private void btnIsseue_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure you want to Renew License?", "Renew License",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                int appID = _CreateApplicationForLicense();
                if(appID != -1)
                {
                    int licenseID = -1;
                    try
                    {
                        licenseID = _CreateLicense(appID);
                    }
                    catch(Exception ex)
                    {
                        clsApplication.DeleteApplication(appID);
                        _ActivateOldLicense();
                        throw new Exception(ex.Message);
                    }
                    if(licenseID != -1)
                    {
                        MessageBox.Show($"License Created Successfully with ID {licenseID}",
                            "Renew License",
                            MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                        gbFilter.Enabled = false;
                        ctrlRenewLicense1.LoadControlByLicenseIDForRenewDrivingLicense(
                            _License.LicenseID, appID, licenseID);
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

        private void ctrlRenewLicense1_OnNotesChanged(string obj)
        {
            _Notes = obj;
        }
    }
}
