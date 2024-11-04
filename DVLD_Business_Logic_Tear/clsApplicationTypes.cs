using DVLD_Data_Access_Tier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business_Logic_Tear
{
    public class clsApplicationTypes
    {
        public int ApplicationTypeID { get; set; }
        public string ApplicationTypeTitle { get; set; }
        public double ApplicationFees { get; set; }

        public clsApplicationTypes()
        {
            ApplicationTypeID = -1;
            ApplicationTypeTitle = "";
            ApplicationFees = 0d;
        }

        private clsApplicationTypes(int ApplicationTypeID, string ApplicationTypeTitle, double ApplicationFees)
        {
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationTypeTitle = ApplicationTypeTitle;
            this.ApplicationFees = ApplicationFees;
        }

        public static DataTable GetAllApplicationTypes()
        {
            return clsApplicationTypesAccessData.GetAllApplicationTypes();
        }

        public static bool UpdateApplicationType(int id, string Title, double fees)
        {
            return clsApplicationTypesAccessData.UpdateApplicationTypeByID(id, Title, fees);
        }

        public static clsApplicationTypes FindByID(int id)
        {
            string Title = "";
            double fees = 0.0d;
            bool isFound = clsApplicationTypesAccessData.FindApplicationTypeByID(id, ref Title, ref fees);
            if (isFound)
            {
                return new clsApplicationTypes(id, Title, fees);
            }
            return null;
        }

    }
}
