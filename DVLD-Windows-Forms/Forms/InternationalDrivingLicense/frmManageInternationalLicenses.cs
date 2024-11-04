using DVLD_Business_Logic_Tear;
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

namespace DVLD_Windows_Forms.Forms.InternationalDrivingLicense
{
    public partial class frmManageInternationalLicenses : Form
    {

        enum enFilter { None}

        private void _FillFilterComboBox()
        {
            cbFilter.Items.Add("None");
            cbFilter.SelectedIndex = 0;
        }

        public frmManageInternationalLicenses()
        {
            InitializeComponent();
        }
        private void _UpdateRecordsCount()
        {
            lblTotalRecords.Text = dgvInternationalDrivingLicenseApplications.Rows
                .GetRowCount(DataGridViewElementStates.Visible).ToString();
        }

        private void _RefreshInternationalDrivingLicenses()
        {
            dgvInternationalDrivingLicenseApplications.DataSource = 
                clsInternationalDrivingLicense.GetAll();
            _UpdateRecordsCount();
        }
        private void frmManageInternationalLicenses_Load(object sender, EventArgs e)
        {
            _FillFilterComboBox();
            dgvInternationalDrivingLicenseApplications.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
            _RefreshInternationalDrivingLicenses();
        }

        private void cmsShoPersonInfo_Click(object sender, EventArgs e)
        {
            int ILID = (int)dgvInternationalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
            int personID = clsPerson.GetPersonIDByInternationalLicenseID(ILID);
            if(personID != -1)
            {
                new frmPersonInfo(personID).ShowDialog();
            }
            else
            {
                MessageBox.Show("Failed to show person Info", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void cmsShowLicenseHistory_Click(object sender, EventArgs e)
        {
            int ILID = (int)dgvInternationalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
            int personID = clsPerson.GetPersonIDByInternationalLicenseID(ILID);
            if (personID != -1)
            {
                new frmDriverLicensesHistory(personID).ShowDialog();
            }
            else
            {
                MessageBox.Show("Failed to show license Info", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void cmsShowLicenseDetails_Click(object sender, EventArgs e)
        {
            int ILID = (int)dgvInternationalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
            int licenseID = clsInternationalDrivingLicense.FindByID(ILID).IssuedUsingLocalLicenseID;
            if(licenseID != -1)
            {
                new frmShowInternationalLicense(licenseID).ShowDialog();
            }
            else
            {
                MessageBox.Show("Failed to show license details", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void _FilterOnNone()
        {
            _RefreshInternationalDrivingLicenses();
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilter.SelectedItem.ToString() == enFilter.None.ToString()) _FilterOnNone();
        }
    }
}
