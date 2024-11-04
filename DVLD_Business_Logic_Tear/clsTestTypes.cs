using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business_Logic_Tear
{
    public class clsTestTypes
    {

        public int TestTypeID { get; set; }
        public double TestTypeFees { get; set; }
        public string TestTypeTitle { get; set; }
        public string TestTypeDescription { get; set; }

        public clsTestTypes()
        {
            TestTypeID = -1;
            TestTypeFees = 0d;
            TestTypeTitle = "";
            TestTypeDescription = "";
        }

        public clsTestTypes(int TestTypeID, string TestTypeTitle, string TestTypeDescription,
            double TestTypeFees)
        {
            this.TestTypeDescription = TestTypeDescription;
            this.TestTypeFees = TestTypeFees;
            this.TestTypeID = TestTypeID;
            this.TestTypeTitle = TestTypeTitle;
        }

        public static DataTable GetAllTestTypes()
        {
            return clsTestTypesAccessData.GetAllTestTypes();
        }

        public static clsTestTypes FindTestTypeByID(int id)
        {
            string title = "", description = "";
            double fees = 0d;
            if(clsTestTypesAccessData.FindTestTypeByID(id, ref title, ref description, ref fees))
            {
                return new clsTestTypes(id,title, description, fees);
            }
            return null;
        }

        public static bool UpdateTestType(int id,string title, string description, double fees)
        {
            return clsTestTypesAccessData.UpdateTestTypeByID(id, title, description, fees);

        }


    }
}
