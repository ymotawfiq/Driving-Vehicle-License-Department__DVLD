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
    public partial class frmDetainLicense : Form
    {

        clsLicense _License = new clsLicense();
        int _PersonID = -1;

        public frmDetainLicense()
        {
            InitializeComponent();
        }

        private void tbSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void llLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new frmDriverLicensesHistory(_PersonID).ShowDialog();
        }

        private void llLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new frmShowInternationalLicense(_License.LicenseID).ShowDialog();
        }

        private void frmDetainLicense_Load(object sender, EventArgs e)
        {
            btnDetain.Enabled = llLicenseHistory.Enabled = llLicenseInfo.Enabled = false;
            btnSearch.Enabled = false;
        }

        private void tbFineFees_KeyPress(object sender, KeyPressEventArgs e)
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
                lblFineFees.Visible = true;
                tbFineFees.Visible = false;
                lblFineFees.Text = clsDetainedLicenses.FindByLicenseID(_License.LicenseID)
                    .FineFees.ToString();
                lblDetainID.Text = clsDetainedLicenses.FindByLicenseID(_License.LicenseID)
                    .DetainID.ToString();
                btnDetain.Enabled = false;
            }
            else
            {
                lblFineFees.Visible = false;
                tbFineFees.Visible = true;
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
                if (!_License.IsActive)
                {
                    MessageBox.Show("License is not Active", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("License Not Found", "Error", MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Error);
            }
        }

        private void tbFineFees_TextChanged(object sender, EventArgs e)
        {
            if (tbFineFees.Text != "") btnDetain.Enabled = true;
            else btnDetain.Enabled = false;
        }

        private int _DetainLicense()
        {
            clsDetainedLicenses detainedLicense = new clsDetainedLicenses();
            detainedLicense.Mode = clsDetainedLicenses.enMode.AddNew;
            detainedLicense.CreatedByUserID = clsCurrentUser.CurrentUser.UserID;
            detainedLicense.DetainDate = DateTime.Now;
            detainedLicense.FineFees = Convert.ToDouble(tbFineFees.Text);
            detainedLicense.IsReleased = false;
            detainedLicense.ReleaseDate = default;
            detainedLicense.ReleasedByUserID = default;
            detainedLicense.ReleaseApplicationID = default;
            detainedLicense.LicenseID = _License.LicenseID;
            detainedLicense.Save();
            return detainedLicense.DetainID;
        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure you want to Detain License?", 
                "Detain", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                if (clsDetainedLicenses.ExistsByLicenseID(_License.LicenseID))
                {
                    MessageBox.Show($"License already Detained",
                        "Detain", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                int detainID = _DetainLicense();
                if(detainID != -1)
                {
                    MessageBox.Show($"License detained successfully with ID: {detainID}",
                        "Detain", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    gbFilter.Enabled = false;
                    btnDetain.Enabled = false;
                    lblDetainID.Text = detainID.ToString();
                }
                else
                {
                    MessageBox.Show($"Failed to Detain License",
                        "Detain", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
