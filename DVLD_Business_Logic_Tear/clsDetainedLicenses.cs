using DVLD_Data_Access_Tier;
using System;
using System.Collections.Generic;
using System.Data;


namespace DVLD_Business_Logic_Tear
{
    public class clsDetainedLicenses
    {
        public enum enMode { AddNew, Update}
        public enMode Mode { get; set; }

        public int DetainID { get; set; }
        public int LicenseID { get; set; }
        public DateTime DetainDate { get; set; }
        public double FineFees { get; set; }
        public int CreatedByUserID { get; set; }
        public bool IsReleased { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int ReleasedByUserID { get; set; }
        public int ReleaseApplicationID { get; set; }
        public clsDetainedLicenses()
        {
            DetainID = -1;
            LicenseID = -1;
            DetainDate = DateTime.Now;
            FineFees = 0;
            CreatedByUserID = -1;
            IsReleased = false;
            ReleaseDate = DateTime.Now;
            ReleasedByUserID = -1;
            ReleaseApplicationID = -1;
            Mode = enMode.AddNew;
        }

        private clsDetainedLicenses(int DetainID, int LicenseID, DateTime DetainDate,
            double FineFees,
            int CreatedByUserID, bool IsReleased, DateTime ReleaseDate, int ReleasedByUserID,
            int ReleaseApplicationID)
        {
            this.DetainID = DetainID;
            this.LicenseID = LicenseID;
            this.DetainDate = DetainDate;
            this.FineFees = FineFees;
            this.CreatedByUserID = CreatedByUserID;
            this.IsReleased = IsReleased;
            this.ReleaseDate = ReleaseDate;
            this.ReleasedByUserID = ReleasedByUserID;
            this.ReleaseApplicationID = ReleaseApplicationID;
        }

        private bool _AddDetainedLicense(int LicenseID, DateTime DetainDate, double FineFees,
            int CreatedByUserID, bool IsReleased, DateTime ReleaseDate, int ReleasedByUserID,
            int ReleaseApplicationID)
        {
            int detainID = clsDetainedLicensesAccessData.AddDetainedLicense(LicenseID, DetainDate, FineFees,
            CreatedByUserID, IsReleased, ReleaseDate, ReleasedByUserID,
            ReleaseApplicationID);
            if (detainID != -1)
            {
                this.DetainID = detainID;
                return true;
            }
            return false;
        }

        private bool _UpdateDetainedLicense(int DetainID, int LicenseID, DateTime DetainDate,
                    double FineFees,
                int CreatedByUserID, bool IsReleased, DateTime ReleaseDate, int ReleasedByUserID,
                int ReleaseApplicationID)
        {
            return clsDetainedLicensesAccessData.UpdateDetainedLicense(DetainID, LicenseID, 
                DetainDate, FineFees,
            CreatedByUserID, IsReleased, ReleaseDate, ReleasedByUserID,
            ReleaseApplicationID);
        }

        public bool Save()
        {
            if(Mode == enMode.AddNew)
            {
                bool isAdded = _AddDetainedLicense(LicenseID, DetainDate, FineFees,
                CreatedByUserID, IsReleased, ReleaseDate, ReleasedByUserID,
                ReleaseApplicationID);
                if (isAdded)
                {
                    Mode = enMode.Update;
                    return true;
                }
                return false;
            }
            return _UpdateDetainedLicense(DetainID, LicenseID, DetainDate, FineFees,
                CreatedByUserID, IsReleased, ReleaseDate, ReleasedByUserID,
                ReleaseApplicationID);
        }

        public static bool ExistsByID(int DetainID)
        {
            return clsDetainedLicensesAccessData.ExistsByID(DetainID);
        }

        public static bool ExistsByLicenseID(int LicenseID)
        {
            return clsDetainedLicensesAccessData.ExistsByLicenseID(LicenseID);
        }

        public static clsDetainedLicenses FindByID(int DetainID)
        {
            int LicenseID = -1;
            DateTime DetainDate = DateTime.Now;
            double FineFees = 0;
            int CreatedByUserID = -1;
            bool IsReleased = false;
            DateTime ReleaseDate = DateTime.Now;
            int ReleasedByUserID = -1;
            int ReleaseApplicationID = -1;

            bool isFound = clsDetainedLicensesAccessData.FindByID(DetainID, ref LicenseID, ref DetainDate,
                ref FineFees,
                ref CreatedByUserID, ref IsReleased, ref ReleaseDate, ref ReleasedByUserID,
                ref ReleaseApplicationID);

            if (isFound)
            {
                return new clsDetainedLicenses(DetainID, LicenseID, DetainDate, FineFees, CreatedByUserID,
                    IsReleased, ReleaseDate, ReleasedByUserID, ReleaseApplicationID);
            }
            return null;
        }

        public static clsDetainedLicenses FindByLicenseID(int LicenseID)
        {
            int DetainID = -1;
            DateTime DetainDate = DateTime.Now;
            double FineFees = 0;
            int CreatedByUserID = -1;
            bool IsReleased = false;
            DateTime ReleaseDate = DateTime.Now;
            int ReleasedByUserID = -1;
            int ReleaseApplicationID = -1;

            bool isFound = clsDetainedLicensesAccessData.FindByLicenseID(LicenseID, ref DetainID,
                ref DetainDate, ref FineFees,
                ref CreatedByUserID, ref IsReleased, ref ReleaseDate, ref ReleasedByUserID,
                ref ReleaseApplicationID);

            if (isFound)
            {
                return new clsDetainedLicenses(DetainID, LicenseID, DetainDate, FineFees, CreatedByUserID,
                    IsReleased, ReleaseDate, ReleasedByUserID, ReleaseApplicationID);
            }
            return null;
        }

        public static bool DeleteByID(int DetainID)
        {
            return clsDetainedLicensesAccessData.DeleteByID(DetainID);
        }

        public static DataTable GetAll()
        {
            return clsDetainedLicensesAccessData.GetAll();
        }

        public static DataTable GetAllByFullName(string fullName)
        {
            return clsDetainedLicensesAccessData.GetAllByFullName(fullName);
        }

        public static DataTable GetAllByNationalNo(string nationalNo)
        {
            return clsDetainedLicensesAccessData.GetAllByNationalNo(nationalNo);
        }

        public static DataTable GetAllReleasedLicenses()
        {
            return clsDetainedLicensesAccessData.GetAllReleasedLicenses();
        }

        public static DataTable GetAllUnReleasedLicenses()
        {
            return clsDetainedLicensesAccessData.GetAllUnReleasedLicenses();
        }

        public static DataTable GetAllByLicenseID(int licenseID)
        {
            return clsDetainedLicensesAccessData.GetAllByLicenseID(licenseID);
        }

        public static DataTable GetByApplicationID(int applicationID)
        {
            return clsDetainedLicensesAccessData.GetByApplicationID(applicationID);
        }

        public static DataTable GetByDetainedLicenseID(int detainLicenseID)
        {
            return clsDetainedLicensesAccessData.GetByDetainLicenseID(detainLicenseID);
        }


    }
}
