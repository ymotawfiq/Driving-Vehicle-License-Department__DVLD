using DVLD_Data_Access_Tier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business_Logic_Tear
{
    public class clsLicense
    {
        public int LicenseID { get; set; }
        public int ApplicationID { get; set; }
        public int DriverID { get; set; }
        public int LicenseClass { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Notes { get; set; }
        public double PaidFees { get; set; }
        public bool IsActive { get; set; }
        public int IssueReason { get; set; }
        public int CreatedByUserID { get; set; }

        public enum enMode
        {
            AddNew, Update
        }

        public enMode Mode { get; set; }

        public clsLicense()
        {
            LicenseID = -1;
            ApplicationID = -1;
            DriverID = -1;
            LicenseClass = -1;
            IssueDate = DateTime.Now;
            ExpirationDate = DateTime.Now;
            Notes = "";
            PaidFees = 0;
            IsActive = true;
            IssueReason = -1;
            CreatedByUserID = -1;
            Mode = enMode.AddNew;
        }

        public clsLicense(int LicenseID, int ApplicationID, int DriverID, int LicenseClass,
            DateTime IssueDate, DateTime ExpirationDate, string Notes, double PaidFees,
            bool IsActive, int IssueReason, int CreatedByUserID)
        {
            this.LicenseID = LicenseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.LicenseClass = LicenseClass;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.Notes = Notes;
            this.PaidFees = PaidFees;
            this.IsActive = IsActive;
            this.IssueReason = IssueReason;
            this.CreatedByUserID = CreatedByUserID;
        }

        private int _AddNewLicense(int ApplicationID, int DriverID, int LicenseClass,
            DateTime IssueDate, DateTime ExpirationDate, string Notes, double PaidFees,
            bool IsActive, int IssueReason, int CreatedByUserID)
        {
            int licenseID = clsLicenseAccessData.AddNewLicense(ApplicationID, DriverID, LicenseClass,
                IssueDate, ExpirationDate, Notes, PaidFees, IsActive, IssueReason, CreatedByUserID);
            if(licenseID != -1)
            {
                LicenseID = licenseID;
                return licenseID;
            }
            return -1;
        }

        private bool _UpdateLicense(int LicenseID, int ApplicationID, int DriverID, int LicenseClass,
            DateTime IssueDate, DateTime ExpirationDate, string Notes, double PaidFees,
            bool IsActive, int IssueReason, int CreatedByUserID)
        {
            return clsLicenseAccessData.UpdateLicense(LicenseID, ApplicationID, DriverID, LicenseClass,
                IssueDate, ExpirationDate, Notes, PaidFees, IsActive, IssueReason, CreatedByUserID);
        }

        public bool Save()
        {
            if(Mode == enMode.AddNew)
            {
                int licenseID =_AddNewLicense(ApplicationID, DriverID, LicenseClass,
                    IssueDate, ExpirationDate, Notes, PaidFees, IsActive, IssueReason, CreatedByUserID);
                if (licenseID != -1)
                {
                    Mode = enMode.Update;
                    return true;
                }
                return false;
            }
            return _UpdateLicense(LicenseID, ApplicationID, DriverID, LicenseClass,
                IssueDate, ExpirationDate, Notes, PaidFees, IsActive, IssueReason, CreatedByUserID);
        }

        public static int AddNewLicense(int ApplicationID, int DriverID, int LicenseClass,
            DateTime IssueDate, DateTime ExpirationDate, string Notes, double PaidFees,
            bool IsActive, int IssueReason, int CreatedByUserID)
        {
            return clsLicenseAccessData.AddNewLicense(ApplicationID, DriverID, LicenseClass,
                IssueDate, ExpirationDate, Notes, PaidFees, IsActive, IssueReason, CreatedByUserID);
        }

        public static clsLicense FindByLDLAppID(int LdLAppID)
        {
            int LicenseID = -1;
            int ApplicationID = -1;
            int DriverID = -1;
            int LicenseClass = -1;
            DateTime IssueDate = DateTime.Now;
            DateTime ExpirationDate = DateTime.Now;
            string Notes = "";
            double PaidFees = 0;
            bool IsActive = true;
            int IssueReason = -1;
            int CreatedByUserID = -1;
            if(clsLicenseAccessData.FindByLDLAppID(LdLAppID, ref LicenseID, ref ApplicationID, ref DriverID, 
                ref LicenseClass, ref IssueDate, ref ExpirationDate, ref Notes, ref PaidFees, 
                ref IsActive, ref IssueReason, ref CreatedByUserID))
            {
                return new clsLicense(LicenseID, ApplicationID, DriverID, LicenseClass,
                IssueDate, ExpirationDate, Notes, PaidFees, IsActive, IssueReason, CreatedByUserID);
            }
            return null;
        }

        public static clsLicense FindByID(int LicenseID)
        {
            int ApplicationID = -1;
            int DriverID = -1;
            int LicenseClass = -1;
            DateTime IssueDate = DateTime.Now;
            DateTime ExpirationDate = DateTime.Now;
            string Notes = "";
            double PaidFees = 0;
            bool IsActive = true;
            int IssueReason = -1;
            int CreatedByUserID = -1;

            bool isFound = clsLicenseAccessData.FindByID(LicenseID, ref ApplicationID, ref DriverID,
                ref LicenseClass, ref IssueDate, ref ExpirationDate, ref Notes, ref PaidFees,
                ref IsActive, ref IssueReason, ref CreatedByUserID);

            if (isFound)
            {
                return new clsLicense(LicenseID, ApplicationID, DriverID, LicenseClass,
                IssueDate, ExpirationDate, Notes, PaidFees, IsActive, IssueReason, CreatedByUserID);
            }
            return null;
        }

        public static bool ExistsByID(int LicenseID)
        {
            return clsLicenseAccessData.ExistsByID(LicenseID);
        }

    }
}
