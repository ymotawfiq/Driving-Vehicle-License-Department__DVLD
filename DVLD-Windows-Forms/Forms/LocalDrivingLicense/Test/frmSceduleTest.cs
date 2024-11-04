using DVLD_Business_Logic_Tear;
using System;
using System.Collections.Generic;

using System.Windows.Forms;

namespace DVLD_Windows_Forms.Forms.LocalDrivingLicense.Test.VisionTest
{
    public partial class frmSceduleTest : Form
    {

        private enum enSceduleMode : int
        {
            AddNew = 1, Update = 2, TakeTest = 3, RetakeTest = 4
        }

        enum enTestTypes
        {
            VisionTest = 1,
            WrittenTest = 2,
            StreetTest = 3
        }

        enum enApplicationType
        {
            New = 1, Canceled = 2, Completed = 3
        }

        enSceduleMode _Mode;

        int _AppointmentID { get; set; }
        int _LDLAppID { get; set; }
        int _PersonID { get; set; }
        int _LicenceClassID { get; set; }
        int _ApplicationTypeID { get; set; }
        int _TestTypeID { get; set; }
        public frmSceduleTest(int LDLAppID, int ApplicationTypeID, int LicenceClassID,
            int PersonID, int TestTypeID, int Mode = (int)enSceduleMode.AddNew, int AppointmentID = -1)
        {
            InitializeComponent();
            _LDLAppID = LDLAppID;
            _ApplicationTypeID = ApplicationTypeID;
            _LicenceClassID = LicenceClassID;
            _PersonID = PersonID;
            _AppointmentID = AppointmentID;
            _Mode = (enSceduleMode)Mode;
            _TestTypeID = TestTypeID;
        }


        private void _CommonControlSetUp()
        {
            clsPerson person = clsPerson.FindByID(_PersonID);
            clsTestAppointment appointment = appointment = clsTestAppointment.FindByID(_AppointmentID);
            clsTestTypes type = clsTestTypes.FindTestTypeByID(_TestTypeID);
            clsLicenceClasses licenceClass = clsLicenceClasses.FindLicenceClassByID(_LicenceClassID);
            clsApplicationTypes applicationType = clsApplicationTypes.FindByID(_ApplicationTypeID);

            if(applicationType != null)
            {
                int trials = clsLocalDrivingLicence
                .GetCountOfTrialsByLocalDrivingLicenseAndTestType(_LDLAppID, _TestTypeID);
                pTest.Enabled = false;
                lblTitle.Text = applicationType.ApplicationTypeTitle;
                lblClassName.Text = licenceClass.ClassName;
                lblFees.Text = type.TestTypeFees.ToString();
                lblLDLAppID.Text = _LDLAppID.ToString();
                lblName.Text = person.FullName;
                lblTrials.Text = trials.ToString();
                gbRetakeTestInfo.Enabled = false;

                if (_AppointmentID != -1)
                    dtpDate.Value = appointment.AppointmentDate;
                else
                    dtpDate.Value = DateTime.Now;
            }

            
        }

        private void _NewFirstTimeScedultTest()
        {
            _CommonControlSetUp();
        }

        private void _UpdateScedultTest()
        {
            clsTestAppointment appointment = clsTestAppointment.FindByID(_AppointmentID);
            _CommonControlSetUp();
            if (appointment.IsLocked)
            {
                dtpDate.Enabled = false;
                lblTitle.Text = "Person already sad for the test, Appointment Locked";
            }
        }

        private void _TakeScedultTest()
        {
            _CommonControlSetUp();

            clsTestAppointment appointment = clsTestAppointment.FindByID(_AppointmentID);
            pTest.Enabled = true;
            dtpDate.Enabled = false;
            gbRetakeTestInfo.Enabled = false;
            gbVisionTest.Enabled = false;
            if (DateTime.Today.Day != appointment.AppointmentDate.Day)
            {
                pTest.Enabled = false;
                btnSave.Enabled = false;
            }
            
        }

        private void _ReTakeScedultTest()
        {
            clsTestAppointment appointment = clsTestAppointment.FindByID(_AppointmentID);
            clsApplicationTypes applicationType = clsApplicationTypes.FindByID(_ApplicationTypeID);

            _CommonControlSetUp();
            dtpDate.Visible = true;
            lblTotalFees.Text = (appointment.PaidFees + applicationType.ApplicationFees).ToString();
            //lblRetakeAppID.Text = ""; 
            lblAppFees.Text = clsTestTypes.FindTestTypeByID(1).TestTypeFees.ToString();
        }

        private void _OnModeAddNew()
        {
            _NewFirstTimeScedultTest();
            pTest.Enabled = false;
        }

        private void _OnModeUpdate()
        {
            _UpdateScedultTest();
            pTest.Enabled = false;
            clsTestAppointment appointment = clsTestAppointment.FindByID(_AppointmentID);
            if (appointment.IsLocked)
            {
                btnSave.Enabled = false;
            }
        }

        private void _OnModeTakeTest()
        {
            _TakeScedultTest();
        }

        private void _OnModeReTakeTest()
        {
            _ReTakeScedultTest();
        }

        private void frmSceduleTest_Load(object sender, EventArgs e)
        {
            if (_Mode == enSceduleMode.AddNew)
                _OnModeAddNew();
            else if (_Mode == enSceduleMode.Update)
                _OnModeUpdate();
            else if (_Mode == enSceduleMode.TakeTest)
                _OnModeTakeTest();
            else
                _OnModeReTakeTest();  
        }

        private clsTestAppointment _FillAddNewAndSave()
        {
            clsTestTypes testType = clsTestTypes.FindTestTypeByID(_TestTypeID);
            clsTestAppointment appointment = new clsTestAppointment();

            appointment.Mode = clsTestAppointment.enMode.AddNew;
            appointment.IsLocked = false;
            appointment.LocalDrivingLicenseApplicationID = _LDLAppID;
            appointment.PaidFees = testType.TestTypeFees;
            appointment.AppointmentDate = dtpDate.Value;
            appointment.CreatedByUserID = clsCurrentUser.CurrentUser.UserID;
            appointment.TestTypeID = _TestTypeID;
            appointment = appointment.Save();
            return appointment;
        }

        private clsTestAppointment _FillUpdateAndSave(bool isLocked=false)
        {
            clsTestTypes testType = clsTestTypes.FindTestTypeByID(_TestTypeID);
            clsTestAppointment appointment = new clsTestAppointment();

            appointment.Mode = clsTestAppointment.enMode.Update;
            appointment.IsLocked = isLocked;
            appointment.LocalDrivingLicenseApplicationID = _LDLAppID;
            appointment.PaidFees = testType.TestTypeFees;
            appointment.AppointmentDate = dtpDate.Value;
            appointment.CreatedByUserID = clsCurrentUser.CurrentUser.UserID;
            appointment.TestTypeID = _TestTypeID;
            appointment.TestAppointmentID = _AppointmentID;
            appointment = appointment.Save();
            return appointment;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            clsTestAppointment appointment = new clsTestAppointment();
            if (_Mode == enSceduleMode.AddNew || _Mode == enSceduleMode.RetakeTest)
                appointment = _FillAddNewAndSave();
            
            else if(_Mode == enSceduleMode.Update)
                appointment = _FillUpdateAndSave();
            
            else
            {
                clsTest test = new clsTest();
                bool result = false;
                if (rbPass.Checked) result = true;
                test = clsTest.AddNewTest(_AppointmentID, result, tbNotes.Text, 
                    clsCurrentUser.CurrentUser.UserID);
                if (test != null)
                {
                    appointment = _FillUpdateAndSave(true);
                    if(appointment != null)
                    {
                        MessageBox.Show("Test Done", "Success", MessageBoxButtons.OK,
                         MessageBoxIcon.Information);
                        this.Close();
                        return;
                    }
                }
                else
                    MessageBox.Show("Failed to do test", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            if (appointment != null)
            {
                MessageBox.Show("Appointment Saved Successfully", "Success", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                this.Close();
            }
            else
                MessageBox.Show("Failed To Save Appointment", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
