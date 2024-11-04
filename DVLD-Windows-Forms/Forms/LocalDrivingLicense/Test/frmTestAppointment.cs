using DVLD_Business_Logic_Tear;
using DVLD_Windows_Forms.Forms.LocalDrivingLicense.Test.VisionTest;
using System;

using System.Windows.Forms;

namespace DVLD_Windows_Forms.Forms.LocalDrivingLicense.Test
{
    public partial class frmTestAppointment : Form
    {
        enum enTestTypes
        {
            VisionTest = 1,
            WrittenTest = 2,
            StreetTest = 3
        }

        private enum enSceduleMode
        {
            AddNew=1, Update=2, TakeTest=3, RetakeTest=4
        }

        enum enApplicationType
        {
            New = 1, Canceled = 2, Completed = 3
        }

        enSceduleMode _Mode;

        int _LDLAppID { get; set; }
        int _PersonID { get; set; }
        int _ClassID { get; set; }
        int _ApplicationTypeID { get; set; }
        int _TestTypeID { get; set; }
        public frmTestAppointment(int LDLAppID, int ApplicationTypeID, int ClassID,
            int PersonID, int TestTypeID)
        {
            InitializeComponent();
            _LDLAppID = LDLAppID;
            _ApplicationTypeID = ApplicationTypeID;
            _ClassID = ClassID;
            _PersonID = PersonID;
            _TestTypeID = TestTypeID;
        }

        private void _UpdateRecordsCount()
        {
            lblTotalRecords.Text = dgvPersonAppointments.Rows
                .GetRowCount(DataGridViewElementStates.Visible).ToString();
        }

        private int _AppointmentsCount()
        {
            return dgvPersonAppointments.RowCount;
        }

        private void _RefreshAppointments()
        {
            dgvPersonAppointments.DataSource = clsTestAppointment
                .GetAllTestAppointmentsByLocalDrivingLicenseAndTestType(_LDLAppID, _TestTypeID);
            _UpdateRecordsCount();
        }

        private void _SetTitle()
        {
            if (_TestTypeID == (int)enTestTypes.VisionTest)
                lblTitle.Text = "Scedule Vision Test";
            else if (_TestTypeID == (int)enTestTypes.WrittenTest)
                lblTitle.Text = "Scedule Written Test";
            else if (_TestTypeID == (int)enTestTypes.StreetTest)
                lblTitle.Text = "Scedule Street Test";
        }

        private void frmVisionTestAppointment_Load(object sender, EventArgs e)
        {
            _SetTitle();
            _RefreshAppointments();
            ctrlApplicationBasicInfo1.LoadControl(_LDLAppID);
            ctrlDrivingLicenseApplicationInfo1.LoadControl(_LDLAppID);
            dgvPersonAppointments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _UpdateRecordsCount();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private clsApplication _AddNewApplication(int appTypeID)
        {
            clsApplication application = new clsApplication();
            application.Mode = clsApplication.enMode.AddNew;
            application.ApplicantPersonID = _PersonID;
            application.ApplicationStatus = (int)enApplicationType.New;
            application.ApplicationTypeID = appTypeID;
            application.PaidFees = clsApplicationTypes.FindByID(1).ApplicationFees;
            application.CreatedByUserID = clsCurrentUser.CurrentUser.UserID;
            if (application.Save())
            {
                return clsApplication.FindByID(application.ApplicationID);
            }
            return null;
        }

        private void _OnRetakeTest()
        {
            clsApplicationTypes applicationTypes = clsApplicationTypes.FindByID(8);
            if (applicationTypes != null)
            {
                int appointmentID = (int)dgvPersonAppointments.CurrentRow.Cells[0].Value;
                int currentAppointmentsCount = _AppointmentsCount();

                Form form = new frmSceduleTest(_LDLAppID, 8, _ClassID, _PersonID, _TestTypeID,
                    (int)enSceduleMode.RetakeTest, appointmentID);
                form.ShowDialog();

                _RefreshAppointments();
                if(currentAppointmentsCount < _AppointmentsCount())
                {
                    _AddNewApplication(8);
                }
            }
        }

        private void _OnAddNewTest()
        {
            Form form = new frmSceduleTest(_LDLAppID, _ApplicationTypeID, _ClassID, _PersonID,
                _TestTypeID, (int)enSceduleMode.AddNew);
            form.ShowDialog();

            _RefreshAppointments();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int appointmentID = (int)dgvPersonAppointments.CurrentRow.Cells[0].Value;

            Form form = new frmSceduleTest(_LDLAppID, _ApplicationTypeID, _ClassID, _PersonID,
                _TestTypeID, (int)enSceduleMode.Update, appointmentID);
            form.ShowDialog();

            _RefreshAppointments();
        }

        private void cmsTakeTest_Click(object sender, EventArgs e)
        {
            int appointmentID = (int)dgvPersonAppointments.CurrentRow.Cells[0].Value;

            Form form = new frmSceduleTest(_LDLAppID, _ApplicationTypeID, _ClassID, _PersonID,
                _TestTypeID, (int)enSceduleMode.TakeTest, appointmentID);
            form.ShowDialog();

            _RefreshAppointments();
        }

        private void MenuStrip_MouseHover(object sender, EventArgs e)
        {
            int appointmentID = (int)dgvPersonAppointments.CurrentRow.Cells[0].Value;
            if (clsTestAppointment.IsTestAppointmentLocked(appointmentID))
            {
                cmsTakeTest.Enabled = false;
            }
        }

        private void btnNewAppointment_Click(object sender, EventArgs e)
        {
            if (clsTestAppointment.ExistsByTestTypeAndLDLAppID(_TestTypeID, _LDLAppID))
            {
                MessageBox.Show("Error while tring to book vision test appointment", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int appointmentID = 0;
            if (dgvPersonAppointments.CurrentRow != null)
                appointmentID = (int)dgvPersonAppointments.CurrentRow.Cells[0].Value;
            if (clsTest.ExistsIfPassedTestByLDLAppIDAndTestTypeID(_LDLAppID, _TestTypeID))
            {
                MessageBox.Show("You already passed this test", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            else
            {
                if (clsTest.ExistsIfFailedTestByTestAppointment(appointmentID))
                {
                    _OnRetakeTest();
                }
                else
                {
                    _OnAddNewTest();
                }
            }

        }

        private void ctrlApplicationBasicInfo1_Load(object sender, EventArgs e)
        {

        }

        private void ctrlDrivingLicenseApplicationInfo1_Load(object sender, EventArgs e)
        {

        }
    }
}
