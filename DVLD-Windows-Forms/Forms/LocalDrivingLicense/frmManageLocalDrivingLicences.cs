using DVLD_Business_Logic_Tear;
using DVLD_Windows_Forms.Forms.LocalDrivingLicense;
using DVLD_Windows_Forms.Forms.LocalDrivingLicense.Test;
using System;
using System.Net.NetworkInformation;
using System.Windows.Forms;

namespace DVLD_Windows_Forms
{
    public partial class frmManageLocalDrivingLicences : Form
    {
        public frmManageLocalDrivingLicences()
        {
            InitializeComponent();
        }

        enum enFilter
        {
            AppID,
            NationalNo,
            Name,
            NewApplications,
            CanceledApplications,
            CompletedApplications,
            None
        }

        enum enApplicaionStatus
        {
            New=1,
            Canceled=2,
            Completed=3
        }

        enum enTestTypes
        {
            VisionTest = 1,
            WrittenTest = 2,
            StreetTest = 3
        }

        private void _UpdateRecordsCount()
        {
            lblTotalRecords.Text = dgvLocalDrivingLicenseApplications.Rows
                .GetRowCount(DataGridViewElementStates.Visible).ToString();
        }

        private void _RefreshLocalDrivingLicenses()
        {
            dgvLocalDrivingLicenseApplications.DataSource = clsLocalDrivingLicence.GetAllLocalDrivingLicences();
            _UpdateRecordsCount();
        }

        enApplicaionStatus _ApplicaionStatus;

        enFilter _Filter;

        private void frmManageLocalDrivingLicences_Load(object sender, EventArgs e)
        {
            cmsIssueDrivingLicenseFirstTime.Enabled = false;
            dgvLocalDrivingLicenseApplications.DataSource = clsLocalDrivingLicence
                .GetAllLocalDrivingLicences();
            dgvLocalDrivingLicenseApplications.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
            _UpdateTotalRecords();
            _FillFilterComboBox();
        }

        private void _UpdateTotalRecords()
        {
            lblTotalRecords.Text = 
                dgvLocalDrivingLicenseApplications.Rows
                .GetRowCount(DataGridViewElementStates.Visible).ToString();
        }

        private void _FillFilterComboBox()
        {
            cbFilter.Items.Add("None");
            cbFilter.Items.Add("AppID");
            cbFilter.Items.Add("NationalNo");
            cbFilter.Items.Add("Name");
            cbFilter.Items.Add("NewApplications");
            cbFilter.Items.Add("CanceledApplications");
            cbFilter.Items.Add("CompletedApplications");
            cbFilter.SelectedIndex = 0;
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbFilter.SelectedItem.ToString() == "None" 
                || cbFilter.SelectedItem.ToString() == "NewApplications"
                || cbFilter.SelectedItem.ToString() == "CanceledApplications"
                || cbFilter.SelectedItem.ToString() == "CompletedApplications")
            {
                tbSearch.Enabled = false;
            }
            else tbSearch.Enabled = true;
            if (enFilter.NewApplications.ToString() == cbFilter.SelectedItem.ToString())
                _FilterByNewApplications();
            if (enFilter.CanceledApplications.ToString() == cbFilter.SelectedItem.ToString())
                _FilterByCanceledApplications();
            if (enFilter.CompletedApplications.ToString() == cbFilter.SelectedItem.ToString())
                _FilterByCompletedApplications();
            if (enFilter.None.ToString() == cbFilter.SelectedItem.ToString()) _FilterByNone();
        }

        #region Filter

        private void _FilterByApplicationID()
        {
            dgvLocalDrivingLicenseApplications.DataSource =
                clsLocalDrivingLicence.GetAllLocalDrivingLicencesByApplicationID(Convert.ToInt32(tbSearch.Text));
            _UpdateTotalRecords();
        }

        private void _FilterByNone()
        {
            dgvLocalDrivingLicenseApplications.DataSource =
                clsLocalDrivingLicence.GetAllLocalDrivingLicences();
            _UpdateTotalRecords();
        }

        private void _FilterByFullName()
        {
            dgvLocalDrivingLicenseApplications.DataSource =
                clsLocalDrivingLicence.GetAllLocalDrivingLicencesByFullName(tbSearch.Text);
            _UpdateTotalRecords();
        }

        private void _FilterByNationalNo()
        {
            dgvLocalDrivingLicenseApplications.DataSource =
                clsLocalDrivingLicence.GetAllLocalDrivingLicencesByNationalNo(tbSearch.Text);
            _UpdateTotalRecords();
        }

        private void _FilterByNewApplications()
        {
            dgvLocalDrivingLicenseApplications.DataSource =
                clsLocalDrivingLicence.GetAllNewLocalDrivingLicencesByFullName();
            _UpdateTotalRecords();
        }

        private void _FilterByCanceledApplications()
        {
            dgvLocalDrivingLicenseApplications.DataSource =
                clsLocalDrivingLicence.GetAllCanceledLocalDrivingLicencesByFullName();
            _UpdateTotalRecords();
        }

        private void _FilterByCompletedApplications()
        {
            dgvLocalDrivingLicenseApplications.DataSource =
                clsLocalDrivingLicence.GetAllCompletedLocalDrivingLicencesByFullName();
            _UpdateTotalRecords();
        }

        #endregion

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            if (enFilter.AppID.ToString() == cbFilter.SelectedItem.ToString()) _FilterByApplicationID();
            if (enFilter.NationalNo.ToString() == cbFilter.SelectedItem.ToString()) _FilterByNationalNo();
            if (enFilter.Name.ToString() == cbFilter.SelectedItem.ToString()) _FilterByFullName();
        }

        private void tbSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(cbFilter.SelectedItem.ToString() == "AppID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            new frmNewLocalDrivingLicenceApplication().ShowDialog();
            _FilterByNewApplications();
        }

        private void cancelApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int localDrivingLicenceID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
            if(clsTest.ExistsIfPassedTestByLDLAppIDAndTestTypeID(localDrivingLicenceID,
                (int)enTestTypes.VisionTest))
            {
                MessageBox.Show("Application Already In Test Process", "Error", MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Error);
                return;
            }
            int appID = clsLocalDrivingLicence.FindByID(localDrivingLicenceID).ApplicationID;
            if (clsApplication.CancelApplication(appID))
            {
                MessageBox.Show("Application Canceled Successfully", "Canceled", MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Information);
                _FilterByCanceledApplications();
                return;
            }
            MessageBox.Show("Failed to cancel application", "Error", MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Error);
        }

        private void sceduleToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void sceduleVisionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ldlAppID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
            int applicationTypeID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[2].Value;
            int licenseClassID = clsLocalDrivingLicence.FindByID(ldlAppID).LicenseClassID;
            int personID = clsPerson.GetPersonIDByLocalDrivingLicense(ldlAppID);

            Form form = new frmTestAppointment(ldlAppID, applicationTypeID, licenseClassID, 
                personID, (int)enTestTypes.VisionTest);
            form.ShowDialog();

            _RefreshLocalDrivingLicenses();
        }

        private void cmsWrittenTest_Click(object sender, EventArgs e)
        {
            int ldlAppID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
            int applicationTypeID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[2].Value;
            int licenseClassID = clsLocalDrivingLicence.FindByID(ldlAppID).LicenseClassID;
            int personID = clsPerson.GetPersonIDByLocalDrivingLicense(ldlAppID);

            Form form = new frmTestAppointment(ldlAppID, applicationTypeID, licenseClassID,
                personID, (int)enTestTypes.WrittenTest);
            form.ShowDialog();

            _RefreshLocalDrivingLicenses();
        }
        private void cmsStreetTest_Click(object sender, EventArgs e)
        {
            int ldlAppID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
            int applicationTypeID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[2].Value;
            int licenseClassID = clsLocalDrivingLicence.FindByID(ldlAppID).LicenseClassID;
            int personID = clsPerson.GetPersonIDByLocalDrivingLicense(ldlAppID);

            Form form = new frmTestAppointment(ldlAppID, applicationTypeID, licenseClassID,
                personID, (int)enTestTypes.StreetTest);
            form.ShowDialog();

            _RefreshLocalDrivingLicenses();
        }
        private void sceduleToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            int ldlAppID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;

            if (clsTest.ExistsIfPassedTestByLDLAppIDAndTestTypeID(ldlAppID, 
                (int)enTestTypes.VisionTest))
            {
                cmsWrittenTest.Enabled = true;
                cmsVisionTest.Enabled = cmsStreetTest.Enabled = false;
            }
            else if (clsTest.ExistsIfPassedTestByLDLAppIDAndTestTypeID(ldlAppID,
                (int)enTestTypes.WrittenTest))
            {
                cmsWrittenTest.Enabled = cmsVisionTest.Enabled = false;
                cmsStreetTest.Enabled = true;
            }
            else if(clsTest.ExistsIfPassedTestByLDLAppIDAndTestTypeID(ldlAppID,
                (int)enTestTypes.StreetTest))
            {
                cmsWrittenTest.Enabled = cmsVisionTest.Enabled = cmsStreetTest.Enabled = false;
            }
            else
            {
                cmsVisionTest.Enabled = true;
                cmsWrittenTest.Enabled = cmsStreetTest.Enabled = false;
            }
            
        }

        

        private void contextMenuStrip1_MouseHover(object sender, EventArgs e)
        {
            cmsShowLicense.Enabled = false;
            cmsLicenseHistory.Enabled = true;
            cmsDeleteApplication.Enabled = true;
            int ldlAppID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
            int passedTests = clsLocalDrivingLicence.GetCountOfPassedTestsByLocalDrivingLicense(ldlAppID);
            if (passedTests >= 3)
            {
                cmsSceduleTest.Enabled = false;
                cmsIssueDrivingLicenseFirstTime.Enabled = true;
                cmsShowLicense.Enabled = true;
                cmsEditApplication.Enabled = false;
                cmsDeleteApplication.Enabled = false;
                cmsCancelApplication.Enabled = false;
                cmsSceduleTest.Enabled = false;
            }
            else
            {
                cmsSceduleTest.Enabled = true;
                cmsDeleteApplication.Enabled = true;
                cmsEditApplication.Enabled = true;
            }
            

            byte appStatus = clsLocalDrivingLicence.GetApplicationStatus(ldlAppID);

            if (appStatus == (int)enApplicaionStatus.Completed)
            {
                cmsIssueDrivingLicenseFirstTime.Enabled = false;
                cmsLicenseHistory.Enabled = true;
                cmsDeleteApplication.Enabled = cmsEditApplication.Enabled = false;
            }
            
            if (appStatus == (int)enApplicaionStatus.Completed
                || appStatus == (int)enApplicaionStatus.Canceled)
            {
                cmsWrittenTest.Enabled = cmsVisionTest.Enabled = cmsStreetTest.Enabled = false;
                cmsSceduleTest.Enabled = false;
                cmsIssueDrivingLicenseFirstTime.Enabled = false;
                cmsIssueDrivingLicenseFirstTime.Enabled = false;
                cmsCancelApplication.Enabled = false;
            }
        }
        private void cmsIssueDrivingLicenseFirstTime_Click(object sender, EventArgs e)
        {
            int ldlAppID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
            int licenceClassID = clsLocalDrivingLicence.FindByID(ldlAppID).LicenseClassID;

            Form form = new frmIssueLicense(ldlAppID, licenceClassID);
            form.ShowDialog();

            _RefreshLocalDrivingLicenses();
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ldlAppID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;

            frmShowLicense form = new frmShowLicense();
            form.LodaByLDLicenseID(ldlAppID);
            form.ShowDialog();
        }

        private void showHistoryLicensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ldlAppID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
            int personID = clsPerson.GetPersonIDByLocalDrivingLicense(ldlAppID);

            Form form = new frmDriverLicensesHistory(personID);
            form.ShowDialog();

        }

        private void cmsDeleteApplication_Click(object sender, EventArgs e)
        {
            int ldlAppID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
            int passedTests = clsLocalDrivingLicence.GetCountOfPassedTestsByLocalDrivingLicense(ldlAppID);
            if(passedTests == 0)
            {
                if (clsLocalDrivingLicence.DeleteByLocalDrivingLicenseApplicationID(ldlAppID))
                {
                    MessageBox.Show("Local Driving License Deleted Successfully", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _RefreshLocalDrivingLicenses();
                    return;
                }
            }
            MessageBox.Show("Can't Delete Local Driving License ,because it's in process", "Failed",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
