using DVLD_Data_Access_Tier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business_Logic_Tear
{
    public class clsLicenceClasses
    {
        public int LicenceClassID { get; set; }
        public int MiniumAllowedAge { get; set; }
        public int DefaultValidityLength { get; set; }
        public double ClassFees { get; set; }
        public string ClassName { get; set; }
        public string ClassDescription { get; set; }

        public clsLicenceClasses()
        {
            LicenceClassID = -1;
            MiniumAllowedAge = 0;
            DefaultValidityLength = 0;
            ClassFees = 0d;
            ClassName = "";
            ClassDescription = "";
        }

        private clsLicenceClasses(int LicenceClassID, string ClassName, string ClassDescription, 
            int MiniumAllowedAge, int DefaultValidityLength, double ClassFees)
        {
            this.LicenceClassID = LicenceClassID;
            this.ClassName = ClassName;
            this.ClassDescription = ClassDescription;
            this.MiniumAllowedAge = MiniumAllowedAge;
            this.DefaultValidityLength = DefaultValidityLength;
            this.ClassFees = ClassFees;
        }

        public static DataTable GetAllLicenceClasses()
        {
            return clsLicenceClassAccessData.GetAllLicenceClasses();
        }

        public static clsLicenceClasses FindLicenceClassByID(int id)
        {
            string className = "", classDescription = "";
            int miniumAllowedAge = 0, defaultValidityLength = 0;
            double classFees = 0d;
            if(clsLicenceClassAccessData.FindLicenceClassByID(id, ref className, ref classDescription,
                ref miniumAllowedAge, ref defaultValidityLength, ref classFees))
            {
                return new clsLicenceClasses(id, className, classDescription, miniumAllowedAge,
                    defaultValidityLength, classFees);
            }
            return null;
        }

        public static bool ExistsLicenceClassByID(int id)
        {
            return clsLicenceClassAccessData.ExistsLicenceClassByID(id);
        }

    }
}
