using DVLD_Business_Logic_Tear;
using System;

using System.Windows.Forms;

namespace DVLD_Windows_Forms.Forms.LocalDrivingLicense
{
    public partial class frmDriverLicensesHistory : Form
    {
        int _PersonID { get; set; }
        public frmDriverLicensesHistory(int PersonID)
        {
            InitializeComponent();
            _PersonID = PersonID;
        }

        private void _RefreshLocalDrivingLicenses()
        {
            dgvDriverLicenses.DataSource = clsPerson.GetAllLocalDrivingLicensesByPersonID(_PersonID);
            _UpdateRecordsCount();
        }

        private void _RefreshInternationalDrivingLicenses()
        {
            dgvDriverLicenses.DataSource = clsInternationalDrivingLicense.GetAllByPersonID(_PersonID);
            _UpdateRecordsCount();
        }

        private void _UpdateRecordsCount()
        {
            lblRecords.Text = dgvDriverLicenses.Rows
                .GetRowCount(DataGridViewElementStates.Visible).ToString();
        }

        private void frmDriverLicensesHistory_Load(object sender, EventArgs e)
        {
            ctrlPersonInfo1.LoadPersonWithID(_PersonID);
            dgvDriverLicenses.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _RefreshLocalDrivingLicenses();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLocalDrivingLicenses_Click(object sender, EventArgs e)
        {
            btnInternationalDrivingLicenses.SendToBack();
            btnLocalDrivingLicenses.BringToFront();
            _RefreshLocalDrivingLicenses();
        }

        private void btnInternationalDrivingLicenses_Click(object sender, EventArgs e)
        {
            btnLocalDrivingLicenses.SendToBack();
            btnInternationalDrivingLicenses.BringToFront();
            _RefreshInternationalDrivingLicenses();
        }

        private void showLicenseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int licenseID = (int)dgvDriverLicenses.CurrentRow.Cells[0].Value;
            frmShowLicense form = new frmShowLicense();
            form.LodaByLicenseID(licenseID);
            form.ShowDialog();
        }
    }
}
