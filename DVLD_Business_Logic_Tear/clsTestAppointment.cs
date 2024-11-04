using DVLD_Data_Access_Tier;
using System;
using System.Data;

namespace DVLD_Business_Logic_Tear
{
    public class clsTestAppointment
    {
        public enum enMode
        {
            AddNew, Update, TakeTest
        }
        public enMode Mode;
        public int TestTypeID { get; set; }
        public int TestAppointmentID { get; set; }
        public int LocalDrivingLicenseApplicationID { get; set; }
        public int CreatedByUserID { get; set; }
        public double PaidFees { get; set; }
        public bool IsLocked { get; set; }
        public DateTime AppointmentDate { get; set; }

        public clsTestAppointment()
        {
            TestTypeID = -1;
            TestAppointmentID = -1;
            LocalDrivingLicenseApplicationID = -1;
            CreatedByUserID = -1;
            PaidFees = 0d;
            IsLocked = false;
            Mode = enMode.AddNew;
        }

        private clsTestAppointment(int TestAppointmentID, int TestTypeID, int LocalDrivingLicenseApplicationID,
            int CreatedByUserID, double PaidFees, bool IsLocked, DateTime AppointmentDate)
        {
            this.TestAppointmentID = TestAppointmentID;
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.CreatedByUserID = CreatedByUserID;
            this.PaidFees = PaidFees;
            this.IsLocked = IsLocked;
            this.TestTypeID = TestTypeID;
            this.AppointmentDate = AppointmentDate;
        }


        public static bool ExistsByTestTypeAndLDLAppID(int TestTypeID, int ldlAppID)
        {
            return clsTestAppointmentAccessData.ExistsByTestTypeAndLDLAppID(TestTypeID, ldlAppID);
        }

        public static DataTable GetAllTestAppointmentsByLocalDrivingLicenseAndTestType(
            int ldlAppID, int TestTypeID)
        {
            return clsTestAppointmentAccessData.GetAllTestAppointmentsByLocalDrivingLicenseAndTestType(
                ldlAppID, TestTypeID);
        }

        private clsTestAppointment _AddNewTestAppointment(int TestTypeID, 
            int LocalDrivingLicenseApplicationID, DateTime AppointmentDate, int CreatedByUserID, double PaidFees)
        {
            int AppointmentID = -1;
            bool isAdded = clsTestAppointmentAccessData.AddNewTestAppointment(TestTypeID,
                LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees, CreatedByUserID,
                ref AppointmentID);
            if (isAdded)
                return new clsTestAppointment(AppointmentID, TestTypeID, LocalDrivingLicenseApplicationID,
                    CreatedByUserID, PaidFees, false, AppointmentDate);
            return null;
        }

        private clsTestAppointment _UpdateTestAppointment(int AppointmentID, int TestTypeID,
            int LocalDrivingLicenseApplicationID, bool IsLocked,
            DateTime AppointmentDate, double PaidFees, int CreatedByUserID)
        {
            bool isUpdated = clsTestAppointmentAccessData.UpdateTestAppointment(AppointmentID,
                TestTypeID, LocalDrivingLicenseApplicationID, IsLocked, 
                AppointmentDate, PaidFees, CreatedByUserID);
            if (isUpdated)
                return new clsTestAppointment(AppointmentID, TestTypeID, LocalDrivingLicenseApplicationID,
                    CreatedByUserID, PaidFees, IsLocked, AppointmentDate);
            return null;
        }



        public clsTestAppointment Save()
        {
            clsTestAppointment appointment = new clsTestAppointment();
            if(Mode == enMode.AddNew)
            {
                appointment = _AddNewTestAppointment(TestTypeID, LocalDrivingLicenseApplicationID,
                    AppointmentDate, CreatedByUserID, PaidFees);
                if (appointment != null)
                {
                    Mode = enMode.Update;
                    return appointment;
                }
            }
            else
            {
                appointment = _UpdateTestAppointment(TestAppointmentID, TestTypeID,
                    LocalDrivingLicenseApplicationID,
                    IsLocked, AppointmentDate, PaidFees, CreatedByUserID);
                if (appointment != null)
                {
                    return appointment;
                }
            }
            return null;
        }

        public static bool IsTestAppointmentLocked(int testAppointmentID)
        {
            return clsTestAppointmentAccessData.IsTestAppointmentLocked(testAppointmentID);
        }
        
        public static clsTestAppointment FindByID(int id)
        {
            int TestTypeID=0, ldlAppID = 0, UserID = -1;
            DateTime AppointmentDate = DateTime.Now;
            double PaidFees = 0d;
            bool IsLocked = false;
            bool isFound = clsTestAppointmentAccessData.FindByID(id, ref TestTypeID, ref ldlAppID,
            ref AppointmentDate, ref PaidFees, ref UserID, ref IsLocked);
            if (isFound)
            {
                return new clsTestAppointment(id, TestTypeID, ldlAppID, UserID, PaidFees, 
                    IsLocked, AppointmentDate);
            }
            return null;

        }


    }
}
