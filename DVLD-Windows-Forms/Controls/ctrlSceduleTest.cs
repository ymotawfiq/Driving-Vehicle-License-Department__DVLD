using DVLD_Business_Logic_Tear;
using System;
using System.Windows.Forms;

namespace DVLD_Windows_Forms.Controls
{
    public partial class ctrlSceduleTest : UserControl
    {
        public ctrlSceduleTest()
        {
            InitializeComponent();
        }


        private enum enSceduleMode : int
        {
            AddNew = 1, Update = 2, TakeTest = 3, RetakeTest = 4
        }


        enSceduleMode _Mode;

        public event Action<DateTime> OnDateChanged;

        protected virtual void PersonSelected(DateTime date)
        {
            Action<DateTime> handler = OnDateChanged;
            if (handler != null)
            {
                handler(date);
            }
        }

        private void ctrlSceduleTest_Load(object sender, EventArgs e)
        {

        }


        private void _CommonControlSetUp(int LDLAppID, int LicenseClassID, int PersonID,
            int TestTypeID, int ApplicationTypeID, int AppointmentID=-1, int Mode=(int)enSceduleMode.AddNew)
        {
            int trials = clsLocalDrivingLicence
                .GetCountOfTrialsByLocalDrivingLicenseAndTestType(LDLAppID, TestTypeID);
            clsPerson person = clsPerson.FindByID(PersonID);
            clsTestAppointment appointment = clsTestAppointment.FindByID(AppointmentID);
            clsTestTypes type = clsTestTypes.FindTestTypeByID(TestTypeID);
            clsLicenceClasses licenceClass = clsLicenceClasses.FindLicenceClassByID(LicenseClassID);
            clsApplicationTypes applicationType = clsApplicationTypes.FindByID(ApplicationTypeID);

            groupBox1.Enabled = false;
            lblTitle.Text = applicationType.ApplicationTypeTitle;
            lblClassName.Text = licenceClass.ClassName;
            lblFees.Text = type.TestTypeFees.ToString();
            lblLDLAppID.Text = LDLAppID.ToString();
            lblName.Text = person.FullName;
            dtpDate.Value = appointment.AppointmentDate;
            lblTrials.Text = trials.ToString();
            _Mode = (enSceduleMode)Mode;
        }

        private void _NewFirstTimeScedultTest(int LDLAppID, int LicenseClassID, int PersonID,
            int TestTypeID, int ApplicationTypeID, int AppointmentID = -1, int Mode=(int)enSceduleMode.AddNew)
        {
            _CommonControlSetUp(LDLAppID, LicenseClassID, PersonID, TestTypeID, 
                ApplicationTypeID, Mode);
        }

        private void _UpdateScedultTest(int LDLAppID, int LicenseClassID, int PersonID,
            int TestTypeID, int ApplicationTypeID, int AppointmentID=-1, int Mode=(int)enSceduleMode.Update)
        {
            clsTestAppointment appointment = clsTestAppointment.FindByID(AppointmentID);
            _CommonControlSetUp(LDLAppID, LicenseClassID, PersonID, AppointmentID, TestTypeID,
                ApplicationTypeID, Mode);
            if (appointment.IsLocked)
            {
                dtpDate.Visible = false;
                lblDate.Visible = true;
                lblDate.Text = dtpDate.Value.ToShortDateString();
                lblTitle.Text = "Person already sad for the test, Appointment Locked";
            }
        }

        private void _TakeScedultTest(int LDLAppID, int LicenseClassID, int PersonID,
            int TestTypeID, int ApplicationTypeID, int AppointmentID = -1, 
            int Mode = (int)enSceduleMode.TakeTest)
        {
            clsTestAppointment appointment = clsTestAppointment.FindByID(AppointmentID);
            _CommonControlSetUp(LDLAppID, LicenseClassID, PersonID, AppointmentID, TestTypeID,
                ApplicationTypeID, Mode);
            dtpDate.Visible = false;
            lblDate.Text = appointment.AppointmentDate.ToShortDateString();
        }

        private void _ReTakeScedultTest(int LDLAppID, int LicenseClassID, int PersonID,
            int TestTypeID, int ApplicationTypeID,
            int AppointmentID = -1, int Mode = (int)enSceduleMode.RetakeTest)
        {
            clsTestAppointment appointment = clsTestAppointment.FindByID(AppointmentID);
            clsApplicationTypes applicationType = clsApplicationTypes.FindByID(ApplicationTypeID);

            _CommonControlSetUp(LDLAppID, LicenseClassID, PersonID, AppointmentID, TestTypeID,
                ApplicationTypeID, Mode);
            groupBox1.Enabled = true;
            dtpDate.Visible = false;
            lblDate.Text = appointment.AppointmentDate.ToShortDateString();
            lblTotalFees.Text = (appointment.PaidFees + applicationType.ApplicationFees).ToString();
            //lblRetakeAppID.Text = ""; 
            lblAppFees.Text = clsTestTypes.FindTestTypeByID(1).TestTypeFees.ToString();
        }

        public void LoadControl(int LDLAppID, int LicenseClassID, int PersonID,
            int TestTypeID, int ApplicationTypeID,
            int AppointmentID = -1, int Mode = (int)enSceduleMode.AddNew)
        {
            if (Mode == (int)enSceduleMode.AddNew)
                _NewFirstTimeScedultTest(LDLAppID, LicenseClassID, PersonID, AppointmentID, TestTypeID,
                    ApplicationTypeID, Mode);

            else if (_Mode == enSceduleMode.Update)
                _UpdateScedultTest(LDLAppID, LicenseClassID, PersonID, AppointmentID, TestTypeID,
                    ApplicationTypeID, Mode);

            else if (_Mode == enSceduleMode.TakeTest)
                _TakeScedultTest(LDLAppID, LicenseClassID, PersonID, AppointmentID, TestTypeID,
                    ApplicationTypeID, Mode);

            else if (_Mode == enSceduleMode.RetakeTest)
                _ReTakeScedultTest(LDLAppID, LicenseClassID, PersonID, AppointmentID, TestTypeID,
                    ApplicationTypeID, Mode);
            

            if (OnDateChanged != null) OnDateChanged(dtpDate.Value);
        }

        private void ctrlSceduleTest_Load_1(object sender, EventArgs e)
        {
            if (OnDateChanged != null) OnDateChanged(dtpDate.Value);
        }

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            if (OnDateChanged != null) OnDateChanged(dtpDate.Value);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
