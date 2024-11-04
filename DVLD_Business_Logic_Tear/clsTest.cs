using DVLD_Data_Access_Tier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business_Logic_Tear
{
    public class clsTest
    {
        public int TestAppointmentID { get; set; }
        public bool TestResult { get; set; }
        public string Notes { get; set; }
        public int CreatedByUserID { get; set; }
        public int TestID { get; set; }

        public clsTest()
        {
            TestAppointmentID = -1;
            TestResult = false;
            Notes = "";
            CreatedByUserID = -1;
            TestID = -1;
        }

        private clsTest(int TestID, int TestAppointmentID, bool TestResult,
            string Notes, int UserID)
        {
            this.TestID = TestID;
            this.TestAppointmentID = TestAppointmentID;
            this.TestResult = TestResult;
            this.Notes = Notes;
            this.CreatedByUserID = UserID;
        }

        public static clsTest AddNewTest(int TestAppointmentID, bool TestResult, 
            string Notes, int UserID)
        {
            int testID = clsTestAccessData.AddNewTest(TestAppointmentID, TestResult, Notes, UserID);
            if(testID != 0)
            {
                return new clsTest(testID, TestAppointmentID, TestResult, Notes, UserID);
            }
            return null;
        }

        public static bool ExistsIfPassedTestByLDLAppIDAndTestTypeID(int LDLAppID, int TestTypeID)
        {
            return clsTestAccessData.ExistsIfPassedTestByLDLAppIDAndTestTypeID(LDLAppID, TestTypeID);
        }

        public static bool ExistsIfFailedTestByTestAppointment(int TestAppointmentID)
        {
            return clsTestAccessData.ExistsIfFailedTestByTestAppointment(TestAppointmentID);
        }

        public static clsTest FindByID(int id)
        {
            int TestAppointmentID = -1;
            bool TestResult = false;
            string Notes = "";
            int UserID = -1;
            bool isFound = clsTestAccessData.FindByID(id, ref TestAppointmentID, ref TestResult,
                ref Notes, ref UserID);
            if (isFound)
                return new clsTest(id, TestAppointmentID, TestResult, Notes, UserID);
            return null;
        }

        public static clsTest FindByTestAppointmentID(int TestAppointmentID)
        {
            int id = -1;
            bool TestResult = false;
            string Notes = "";
            int UserID = -1;
            bool isFound = clsTestAccessData.FindByTestAppointmentID(TestAppointmentID, ref id, ref TestResult,
                ref Notes, ref UserID);
            if (isFound)
                return new clsTest(id, TestAppointmentID, TestResult, Notes, UserID);
            return null;
        }

    }
}
