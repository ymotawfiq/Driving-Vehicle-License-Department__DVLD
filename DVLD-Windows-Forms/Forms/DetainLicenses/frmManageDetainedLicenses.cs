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

namespace DVLD_Windows_Forms.Forms.DetainLicenses
{
    public partial class frmManageDetainedLicenses : Form
    {
        public frmManageDetainedLicenses()
        {
            InitializeComponent();
        }

        private enum enFilter
        {
            None,
            FullName,
            Released,
            UnReleased,
            NationalNo,
            LicenseID,
            DetainedLicenseID,
            ApplicationID
        }

        private void _FillFilter()
        {
            cbFilter.Items.Add("None");
            cbFilter.Items.Add("FullName");
            cbFilter.Items.Add("Released");
            cbFilter.Items.Add("UnReleased");
            cbFilter.Items.Add("NationalNo");
            cbFilter.Items.Add("LicenseID");
            cbFilter.Items.Add("DetainedLicenseID");
            cbFilter.Items.Add("ApplicationID");
            cbFilter.SelectedIndex = 0;
        }

        private void _RefreshDetainedLicenses()
        {
            dgvDetainedLicenses.DataSource = clsDetainedLicenses.GetAll();
            _UpdateDetainedLicensesRecords();
        }

        private void _UpdateDetainedLicensesRecords()
        {
            lblRecords.Text = dgvDetainedLicenses.Rows
                .GetRowCount(DataGridViewElementStates.Visible).ToString();
        }

        private void frmManageDetainedLicenses_Load(object sender, EventArgs e)
        {
            _FillFilter();
            dgvDetainedLicenses.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _RefreshDetainedLicenses();
        }

        private void cmsShowPersonDetails_Click(object sender, EventArgs e)
        {
            int licenseID = (int)dgvDetainedLicenses.CurrentRow.Cells[1].Value;
            int personID = clsPerson.GetPersonIDByLicenseID(licenseID);
            new frmPersonInfo(personID).ShowDialog();
        }

        private void cmsShowLicenseDetails_Click(object sender, EventArgs e)
        {
            int licenseID = (int)dgvDetainedLicenses.CurrentRow.Cells[1].Value;
            frmShowLicense frm = new frmShowLicense();
            frm.LodaByLicenseID(licenseID);
            frm.ShowDialog();
        }

        private void cmsShowPersonLicensesHistory_Click(object sender, EventArgs e)
        {
            int licenseID = (int)dgvDetainedLicenses.CurrentRow.Cells[1].Value;
            int personID = clsPerson.GetPersonIDByLicenseID(licenseID);
            frmDriverLicensesHistory frm = new frmDriverLicensesHistory(personID);
            frm.ShowDialog();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            
        }

        private void cmsReleaseDetainedLicense_Click(object sender, EventArgs e)
        {
            int dlID = (int)dgvDetainedLicenses.CurrentRow.Cells[0].Value;
            new frmReleaseDetainedLicense(dlID).ShowDialog();
            _RefreshDetainedLicenses();
        }

        private void contextMenuStrip1_MouseHover(object sender, EventArgs e)
        {
            int licenseID = (int)dgvDetainedLicenses.CurrentRow.Cells[1].Value;
            if (clsDetainedLicenses.ExistsByLicenseID(licenseID))
            {
                cmsReleaseDetainedLicense.Enabled = true;
            }
            else
            {
                cmsReleaseDetainedLicense.Enabled = false;
            }
        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            new frmDetainLicense().ShowDialog();
            _RefreshDetainedLicenses();
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            new frmReleaseDetainedLicense().ShowDialog();
            _RefreshDetainedLicenses();
        }

        private void _FilterOnNone()
        {
            _RefreshDetainedLicenses();
        }

        private void _FilterByFullName()
        {
            dgvDetainedLicenses.DataSource = clsDetainedLicenses.GetAllByFullName(tbSearch.Text);
            _UpdateDetainedLicensesRecords();
        }

        private void _FilterByNationalNo()
        {
            dgvDetainedLicenses.DataSource = clsDetainedLicenses.GetAllByNationalNo(tbSearch.Text);
            _UpdateDetainedLicensesRecords();
        }

        private void _FilterByLicenseID()
        {
            dgvDetainedLicenses.DataSource = clsDetainedLicenses.GetAllByLicenseID(
                Convert.ToInt32(tbSearch.Text));
            _UpdateDetainedLicensesRecords();
        }

        private void _FilterByApplicationID()
        {
            dgvDetainedLicenses.DataSource = clsDetainedLicenses.GetByApplicationID(
                Convert.ToInt32(tbSearch.Text));
            _UpdateDetainedLicensesRecords();
        }

        private void _FilterByDetainedLicenseID()
        {
            dgvDetainedLicenses.DataSource = clsDetainedLicenses.GetByDetainedLicenseID(
                Convert.ToInt32(tbSearch.Text));
            _UpdateDetainedLicensesRecords();
        }

        private void _FilterByReleased()
        {
            dgvDetainedLicenses.DataSource = clsDetainedLicenses.GetAllReleasedLicenses();
            _UpdateDetainedLicensesRecords();
        }

        private void _FilterByUnReleased()
        {
            dgvDetainedLicenses.DataSource = clsDetainedLicenses.GetAllUnReleasedLicenses();
            _UpdateDetainedLicensesRecords();
        }


        private void tbSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(cbFilter.SelectedItem.ToString()==enFilter.ApplicationID.ToString()
                || cbFilter.SelectedItem.ToString()==enFilter.LicenseID.ToString()
                || cbFilter.SelectedItem.ToString() == enFilter.DetainedLicenseID.ToString()
                || cbFilter.SelectedItem.ToString() == enFilter.None.ToString())
            {
                e.Handled = !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar);
            }
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbFilter.SelectedItem.ToString()==enFilter.Released.ToString()
                || cbFilter.SelectedItem.ToString() == enFilter.UnReleased.ToString()
                || cbFilter.SelectedItem.ToString() == enFilter.None.ToString())
            {
                tbSearch.Visible = false;
            }
            else
            {
                tbSearch.Visible = true;
            }

            if(cbFilter.SelectedItem.ToString() == enFilter.Released.ToString())
                _FilterByReleased();

            if (cbFilter.SelectedItem.ToString() == enFilter.UnReleased.ToString())
                _FilterByUnReleased();

            if (cbFilter.SelectedItem.ToString() == enFilter.None.ToString())
                _FilterOnNone();

        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            if (cbFilter.SelectedItem.ToString() == enFilter.ApplicationID.ToString()) 
                _FilterByApplicationID();

            if (cbFilter.SelectedItem.ToString() == enFilter.DetainedLicenseID.ToString())
                _FilterByDetainedLicenseID();

            if (cbFilter.SelectedItem.ToString() == enFilter.FullName.ToString())
                _FilterByFullName();

            if (cbFilter.SelectedItem.ToString() == enFilter.LicenseID.ToString())
                _FilterByLicenseID();

            if (cbFilter.SelectedItem.ToString() == enFilter.NationalNo.ToString())
                _FilterByNationalNo();

            

        }
    }
}
