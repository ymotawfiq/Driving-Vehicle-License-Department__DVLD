using DVLD_Data_Access_Tier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business_Logic_Tear
{
    public class clsDriver
    {
        public int DriverID { get; set; }
        public int PersonID { get; set; }
        public int CreatedByUserID { get; set; }
        public DateTime CreatedDate { get; set; }

        public clsDriver()
        {
            DriverID = -1;
            PersonID = -1;
            CreatedByUserID = -1;
            CreatedDate = DateTime.Now;
        }
        private clsDriver(int DriverID, int PersonID, int CreatedByUserID, DateTime CreatedDate)
        {
            this.DriverID = DriverID;
            this.PersonID = PersonID;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedDate = CreatedDate;
        }

        public static int AddNewDriver(int PersonID, int CreatedByUserID, DateTime CreatedDate)
        {
            return clsDriversAccessData.AddNewDriver(PersonID, CreatedByUserID, CreatedDate);
        }

        public static clsDriver FindByDriverID(int DriverID)
        {
            int PersonID = -1;
            int CreatedByUserID = -1;
            DateTime CreatedDate = DateTime.Now;
            bool isFound = clsDriversAccessData.FindByDriverID(DriverID, ref PersonID, ref CreatedByUserID,
                ref CreatedDate);
            if (isFound)
                return new clsDriver(DriverID, PersonID, CreatedByUserID, CreatedDate);
            return null;
        }

        public static clsDriver FindByPersonID(int PersonID)
        {
            int DriverID = -1;
            int CreatedByUserID = -1;
            DateTime CreatedDate = DateTime.Now;
            bool isFound = clsDriversAccessData.FindByPersonID(PersonID, ref DriverID, ref CreatedByUserID,
                ref CreatedDate);
            if (isFound)
                return new clsDriver(DriverID, PersonID, CreatedByUserID, CreatedDate);
            return null;
        }

        public static bool IsDriverExistsByDriverID(int driverID)
        {
            return clsDriversAccessData.IsDriverExistsByDriverID(driverID);
        }

        public static bool IsDriverExistsByPersonID(int personID)
        {
            return clsDriversAccessData.IsDriverExistsByPersonID(personID);
        }

        public static DataTable GetAllDrivers()
        {
            return clsDriversAccessData.GetAllDrivers();
        }

        public static DataTable GetAllDriversByID(int DriverID)
        {
            return clsDriversAccessData.GetAllDriversByID(DriverID);
        }

        public static DataTable GetAllDriversByPersonID(int PersonID)
        {
            return clsDriversAccessData.GetAllDriversByPersonID(PersonID);
        }

        public static DataTable GetAllDriversByNationalNo(string NationalNo)
        {
            return clsDriversAccessData.GetAllDriversByNationalNo(NationalNo);
        }

        public static DataTable GetAllDriversByFullName(string FullName)
        {
            return clsDriversAccessData.GetAllDriversByFullName(FullName);
        }

    }
}
